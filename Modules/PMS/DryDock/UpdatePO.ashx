<%@ WebHandler Language="C#" Class="UpdatePO" %>

using System;
using System.Web;
using System.Data;
using System.Text;

public class UpdatePO : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        
        context.Response.ContentType = "text/plain";
        string Type = Convert.ToString(context.Request.QueryString["Type"]);

        if (Type == "" || Type == null)
            Type = context.Request.Form["Type"].ToString(); 
        
        if (Type == "POQty")
        {
            decimal POQty=Common.CastAsDecimal(context.Request.Form["POQty"]);
            decimal Rate=Common.CastAsDecimal(context.Request.Form["Rate"]);
            decimal PODisc=Common.CastAsDecimal(context.Request.Form["PODisc"]);
            int RFQId = Common.CastAsInt32(context.Request.Form["RFQId"]);
            int DocketId = Common.CastAsInt32(context.Request.Form["DocketId"]);
            int DocketJobId = Common.CastAsInt32(context.Request.Form["DocketJobId"]);
            int DocketSubJobId = Common.CastAsInt32(context.Request.Form["DocketSubJobId"]);

            Common.Set_Procedures("DD_UpdatePO");
            Common.Set_ParameterLength(7);
            Common.Set_Parameters(
               new MyParameter("@RFQId", RFQId),
               new MyParameter("@DocketId", DocketId),
               new MyParameter("@DocketJobId", DocketJobId),
               new MyParameter("@DocketSubJobId", DocketSubJobId),
               new MyParameter("@POQty", POQty),
               new MyParameter("@UnitPrice", Rate),
               new MyParameter("@PODiscountPer", PODisc)
               );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            decimal NetAmt;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                NetAmt = Common.CastAsDecimal(ds.Tables[0].Rows[0]["PONetAmount"]);
                decimal Total = Common.CastAsDecimal(ds.Tables[0].Rows[0]["PONetAmount_Total"]);
                context.Response.Write(NetAmt.ToString() + "," + Total.ToString()); 
            }            
        }

        if (Type == "SUPQuoteQty")
        {
            decimal QuoteQty = Common.CastAsDecimal(context.Request.Form["QuoteQty"]);
            decimal Rate = Common.CastAsDecimal(context.Request.Form["Rate"]);
            decimal Disc = Common.CastAsDecimal(context.Request.Form["Disc"]);
            int RFQId = Common.CastAsInt32(context.Request.Form["RFQId"]);
            string subjobcode = context.Request.Form["subjobcode"].ToString();
            string Remarks = context.Request.Form["Remark"].ToString();
            
            Common.Set_Procedures("DD_UpdateSUPQuote");
            Common.Set_ParameterLength(6);
            Common.Set_Parameters(
               new MyParameter("@RFQId", RFQId),
               new MyParameter("@SUBJOBCODE", subjobcode),
               new MyParameter("@QuoteQty", QuoteQty),
               new MyParameter("@UnitPrice", Rate),
               new MyParameter("@DiscountPer", Disc),
               new MyParameter("@Remarks", Remarks)
               );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            decimal NetAmt=0, TotalJobSum=0, TotalCatsSum=0, Total=0;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
               Common.Set_Procedures("ExecQuery");
               Common.Set_ParameterLength(1);
               Common.Set_Parameters(new MyParameter("@Query"," EXEC dbo.DD_UpdateSUPQuote_GETDATA " + RFQId + ",'" + subjobcode + "'"));
               ds = Common.Execute_Procedures_Select(); 
                if(ds.Tables[0].Rows.Count>0)
                    NetAmt = Common.CastAsDecimal(ds.Tables[0].Rows[0][0]);
                if (ds.Tables[1].Rows.Count > 0)
                    TotalJobSum = Common.CastAsDecimal(ds.Tables[1].Rows[0][0]);
                if (ds.Tables[2].Rows.Count > 0)
                    TotalCatsSum = Common.CastAsDecimal(ds.Tables[2].Rows[0][0]);
                if (ds.Tables[3].Rows.Count > 0)
                    Total = Common.CastAsDecimal(ds.Tables[3].Rows[0][0]);               

                context.Response.Write(NetAmt.ToString() + "," + TotalJobSum.ToString() + "," + TotalCatsSum.ToString() + "," + Total.ToString());
            }
        }
        
        if (Type == "ViewJobDescr")
        {
            int RFQId = Common.CastAsInt32(context.Request.Form["RFQId"]);
            int DocketId = Common.CastAsInt32(context.Request.Form["DocketId"]);
            int DocketJobId = Common.CastAsInt32(context.Request.Form["DocketJobId"]);
            int DocketSubJobId = Common.CastAsInt32(context.Request.Form["DocketSubJobId"]);

            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT SubJobName FROM DD_Docket_RFQ_SubJobs  WHERE RFQId=" + RFQId + " AND DocketId=" + DocketId + " AND DocketJobId=" + DocketJobId + "  AND DocketSubJobId=" + DocketSubJobId);
            string res = dt.Rows[0]["SubJobName"].ToString();
            context.Response.Write(res);             
        }
        //if (Type == "PartialPlan")
        //{
        //    int RFQId = Common.CastAsInt32(context.Request.QueryString["RFQId"]);
        //    int DocketId = Common.CastAsInt32(context.Request.QueryString["DocketId"]);
        //    int DocketJobId = Common.CastAsInt32(context.Request.QueryString["DocketJobId"]);
        //    //
        //    Common.Execute_Procedures_Select_ByQuery("update [DD_Docket_RFQ_Jobs_Planning] set status='D' where RfqId=" + RFQId + " and DocketJobId=" + DocketJobId + " and DocketId=" + DocketId);

        //    char[] sep = { ',' };
        //    string Dates = Convert.ToString(context.Request.Form["dates"]);
        //    if (Dates != null)
        //        if (Dates.Trim() != "")
        //        {
        //            string[] parts = Dates.Split(sep);

        //            foreach (string stdt in parts)
        //            {
        //                if (stdt.Trim() != "")
        //                    Common.Execute_Procedures_Select_ByQuery("dbo.DD_InsertUpdate_PlanningDate " + RFQId + "," + DocketJobId + "," + DocketId + ",'" + stdt + "'");
        //            }
        //        }
        //}
        if (Type == "TimeLine")
        {
            int RFQId = Common.CastAsInt32(context.Request.QueryString["RFQId"]);
            int DocketId = Common.CastAsInt32(context.Request.QueryString["DocketId"]);
            int DocketJobId = Common.CastAsInt32(context.Request.QueryString["DocketJobId"]);
            

            DateTime Ex_StartDate = new DateTime();
            DateTime Ex_EndDate = new DateTime();

            DataTable dtRFQ = Common.Execute_Procedures_Select_ByQuery("SELECT ExecFrom,ExecTo FROM DD_Docket_RFQ_Master WHERE RFQId=" + RFQId);
            if (dtRFQ.Rows.Count > 0)
            {
                Ex_StartDate = Convert.ToDateTime(dtRFQ.Rows[0]["ExecFrom"]);
                Ex_EndDate = Convert.ToDateTime(dtRFQ.Rows[0]["ExecTo"]);
            }

            DataTable dtDates = Common.Execute_Procedures_Select_ByQuery("select FOR_DATE,PER,REMARK from [DD_Docket_RFQ_Jobs_Planning] WHERE RFQId=" + RFQId + " AND DocketId=" + DocketId + " AND DOCKETJOBID=" + DocketJobId );
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT JobCode,PlanFrom,PlanTo,StartFrom,StartTo,ExecPerDate,ExecPer,JobCode FROM DD_Docket_RFQ_Jobs  WHERE RFQId=" + RFQId + " AND DocketId=" + DocketId + " AND DocketJobId=" + DocketJobId);

            DataTable dtMaxPer1 = Common.Execute_Procedures_Select_ByQuery("select top 1 FOR_DATE,PER from [DD_Docket_RFQ_Jobs_Planning] WHERE RFQId=" + RFQId + " AND DocketId=" + DocketId + " AND DOCKETJOBID=" + DocketJobId + " and FOR_DATE is not null AND ISNULL(PER,0) > 0 ORDER BY CAST(PER AS INT) DESC ");
            DateTime? MaxPerDate1 = null;
            if (dtMaxPer1.Rows.Count > 0)
            {
                MaxPerDate1 = Convert.ToDateTime(dtMaxPer1.Rows[0]["FOR_DATE"]);
            }
            
            
            DateTime? PlanDate=null;
            DateTime? PlanDate1 = null;
            DateTime? StartDate = null;
            DateTime? ExecPerDate = null;
            DateTime? StartDate1 = null;
            int ExecPer=Common.CastAsInt32(dt.Rows[0]["ExecPer"]) ;
            if (dt.Rows.Count > 0)
            {
                try
                {
                    PlanDate = Convert.ToDateTime(dt.Rows[0]["PlanFrom"]);
                    PlanDate1 = Convert.ToDateTime(dt.Rows[0]["PlanTo"]);
                    StartDate = Convert.ToDateTime(dt.Rows[0]["StartFrom"]);
                    ExecPerDate = Convert.ToDateTime(dt.Rows[0]["ExecPerDate"]);
                    StartDate1 = Convert.ToDateTime(dt.Rows[0]["StartTo"]);
                }catch(Exception ex){}
            }   
            StringBuilder sb = new StringBuilder();
            
            int selallowed = 0;
            if(StartDate==null)
                selallowed = 1;
            
            sb.Append("<ol class='selectableMe' bindto='ctl_" + dt.Rows[0]["JobCode"].ToString() + "'  style='width:100%;'>");
           

            int c=1;
            bool PerOnFound = false;
            bool NextBlocked = false;

            bool Completed = Common.CastAsInt32(dt.Rows[0]["ExecPer"]) >= 100;

                
            while (Ex_StartDate <= Ex_EndDate)
            {
                bool startblock = false;
                DataRow[] drs= dtDates.Select("FOR_DATE='" + Ex_StartDate.ToString("dd-MMM-yyyy") + "'");
                string PerOnDate = "";
                string Remark = "";

                string Classs = "";

                if (Ex_StartDate >= PlanDate && Ex_StartDate <= PlanDate1)
                    if (Completed)
                        Classs = "ui-selectedg";
                    else
                        Classs = "ui-selected";
                else
                    Classs = "ui-blocked";
                
                
                //if (MaxPerDate1 == null) { MaxPerDate1 = DateTime.Today; }
                //else{MaxPerDate1 = (MaxPerDate1 > DateTime.Today) ? MaxPerDate1 : DateTime.Today;}
                
                //if (Ex_StartDate.Date < (MaxPerDate1.Value).Date)
                //{
                //    if (drs.Length > 0)
                //    {
                //        Classs = "ui-selectednone";
                //    }
                //    else
                //    {
                //        Classs = "ui-blocked";
                //    }

                //    if (drs.Length > 0)
                //    {
                //        if (Common.CastAsInt32(drs[0]["PER"]) > 0)
                //        {
                //            PerOnDate = drs[0]["PER"].ToString() + " %";
                //            Remark = drs[0]["REMARK"].ToString().Replace("'", "`");
                //            Classs = "ui-selectedg";
                //            if (Common.CastAsInt32(drs[0]["PER"]) == 100)
                //                startblock = true;
                //        }
                //    }
                //}
                //else
                //{
                //    if (drs.Length > 0)
                //    {
                //        if (Common.CastAsInt32(drs[0]["PER"]) > 0)
                //        {
                //            PerOnDate = drs[0]["PER"].ToString() + " %";
                //            Remark = drs[0]["REMARK"].ToString().Replace("'", "`");
                //            Classs = "ui-selectedg";
                //            if (Common.CastAsInt32(drs[0]["PER"]) == 100)
                //                startblock = true;
                //        }
                //        else
                //        {
                //            Classs = "ui-selected";
                //        }
                //    }
                
                //}

                
                if (NextBlocked)
                {
                    Classs = "ui-blocked";
                }

                if (startblock)
                {
                    NextBlocked = startblock;
                }
               
                
                
                DateTime dtCalc = Ex_StartDate;
                
                sb.Append("<li id='td_" + dt.Rows[0]["JobCode"] + "_" + c.ToString() + "' for_date='" + dtCalc.ToString("dd-MMM-yyyy") + "' class='" + Classs + "' style='height:17px;' title='" + Remark + "'>" + PerOnDate + "&nbsp;</li>");
                Ex_StartDate = Ex_StartDate.AddDays(1);
                c++;
            }
            
            sb.Append("</ol>");
            context.Response.Write(sb.ToString());
        }
        if (Type == "ViewRemarks")
        {
            int RFQId = Common.CastAsInt32(context.Request.Form["RFQId"]);
            int DocketId = Common.CastAsInt32(context.Request.Form["DocketId"]);
            int DocketJobId = Common.CastAsInt32(context.Request.Form["DocketJobId"]);
            int DocketSubJobId = Common.CastAsInt32(context.Request.Form["DocketSubJobId"]);

            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT VendorRemarks FROM DD_Docket_RFQ_SubJobs  WHERE RFQId=" + RFQId + " AND DocketId=" + DocketId + " AND DocketJobId=" + DocketJobId + "  AND DocketSubJobId=" + DocketSubJobId);
            string res = dt.Rows[0]["VendorRemarks"].ToString();
            context.Response.Write(res);
        }

        if (Type == "ESTAMT")
        {
            int RFQId = Common.CastAsInt32(context.Request.Form["RFQId"]);
            int DocketId = Common.CastAsInt32(context.Request.Form["DocketId"]);
            int DocketJobId = Common.CastAsInt32(context.Request.Form["DocketJobId"]);
            int DocketSubJobId = Common.CastAsInt32(context.Request.Form["DocketSubJobId"]);
            string SubJobCode = context.Request.Form["SubJobCode"];
            decimal EstUnitPrice = Common.CastAsDecimal(context.Request.Form["EstUnitPrice"]);
            decimal EstQty = Common.CastAsDecimal(context.Request.Form["EstQty"]);
            decimal EstDiscPer = Common.CastAsDecimal(context.Request.Form["EstDiscPer"]);
            string Remarks = context.Request.Form["Remarks"];

            Common.Set_Procedures("DD_UpdateCostTracking");
            Common.Set_ParameterLength(9);
            Common.Set_Parameters(
               new MyParameter("@RFQId", RFQId),
               new MyParameter("@DocketId", DocketId),
               new MyParameter("@DocketJobId", DocketJobId),
               new MyParameter("@DocketSubJobId", DocketSubJobId),
               new MyParameter("@EstUnitPrice", EstUnitPrice),
               new MyParameter("@SubJobCode", SubJobCode),
               new MyParameter("@EstQty", EstQty),
               new MyParameter("@EstDiscPer", EstDiscPer),
               new MyParameter("@Remarks", Remarks)
               );
            
            Boolean res;
            decimal CatSumNetAmt, CatSumEstAmt, CatSumVarAmt, CatSumVarPer;
            decimal JobSumNetAmt, jobSumEstAmt, jobSumVarAmt, jobSumVarPer;
            decimal VarAmt = 0;
            decimal VarPer=0;            

            decimal NetAmt_T, EstAmt_T, AmtYC, AmtNY, TotalVar, TotalVarPer, EstAmt_USD;
            
            DataSet ds1 = new DataSet();
            res = Common.Execute_Procedures_IUD(ds1);
            if (res)
            {
                Common.Set_Procedures("DD_GETCostTracking_Data");
                Common.Set_ParameterLength(5);
                Common.Set_Parameters(
                   new MyParameter("@RFQId", RFQId),
                   new MyParameter("@DocketId", DocketId),
                   new MyParameter("@DocketJobId", DocketJobId),
                   new MyParameter("@DocketSubJobId", DocketSubJobId),
                   new MyParameter("@SubJobCode", SubJobCode)
                   );
                
                DataSet ds=Common.Execute_Procedures_Select();
                //DataSet ds = Common.Execute_Procedures_Select("exec dbo.DD_GETCostTracking_Data " + RFQId + "," + DocketId + "," + DocketJobId + "," + DocketSubJobId + ",'" + SubJobCode + "'");
                    
                CatSumNetAmt = Common.CastAsDecimal(ds.Tables[0].Rows[0]["PONetAmount_USD"]);
                CatSumEstAmt = Common.CastAsDecimal(ds.Tables[0].Rows[0]["EstAmount_USD"]);
                CatSumVarAmt = Common.CastAsDecimal(ds.Tables[0].Rows[0]["VarAmt"]);
                CatSumVarPer = Common.CastAsDecimal(ds.Tables[0].Rows[0]["VarPer"]);

                JobSumNetAmt = Common.CastAsDecimal(ds.Tables[1].Rows[0]["PONetAmount_USD"]);
                jobSumEstAmt = Common.CastAsDecimal(ds.Tables[1].Rows[0]["EstAmount_USD"]);
                jobSumVarAmt = Common.CastAsDecimal(ds.Tables[1].Rows[0]["VarAmt"]);
                jobSumVarPer = Common.CastAsDecimal(ds.Tables[1].Rows[0]["VarPer"]);
                
                VarAmt = Common.CastAsDecimal(ds.Tables[2].Rows[0]["VarAmt"]);
                VarPer = Common.CastAsDecimal(ds.Tables[2].Rows[0]["VarPer"]);
                EstAmt_USD = Common.CastAsDecimal(ds.Tables[2].Rows[0]["EstAmount_USD"]);

                NetAmt_T = Common.CastAsDecimal(ds.Tables[3].Rows[0]["PONetAmount_USD_Total"]);
                EstAmt_T = Common.CastAsDecimal(ds.Tables[3].Rows[0]["EstAmount_USD_Total"]);
                AmtYC = Common.CastAsDecimal(ds.Tables[3].Rows[0]["EstAmount_USD_TotalYC"]);
                AmtNY = Common.CastAsDecimal(ds.Tables[3].Rows[0]["EstAmount_USD_TotalNY"]);

                TotalVar = (EstAmt_T - NetAmt_T);
                TotalVarPer = ((TotalVar / (NetAmt_T == 0 ? 1 : NetAmt_T)) * 100);

                context.Response.Write(FormatCurency(CatSumNetAmt) + "|" + FormatCurency(CatSumEstAmt) + "|" + FormatCurency(CatSumVarAmt) + "|" + FormatCurency(CatSumVarPer) + "|" + FormatCurency(JobSumNetAmt) + "|" + FormatCurency(jobSumEstAmt) + "|" + FormatCurency(jobSumVarAmt) + "|" + FormatCurency(jobSumVarPer) + "|" + FormatCurency(VarAmt) + "|" + FormatCurency(VarPer) + "|" + FormatCurr_WithComma(NetAmt_T) + "|" + FormatCurr_WithComma(EstAmt_T) + "|" + FormatCurr_WithComma(AmtYC) + "|" + FormatCurr_WithComma(AmtNY) + "|" + FormatCurr_WithComma(TotalVar) + "|" + FormatCurency(TotalVarPer) + "|" + FormatCurency(EstAmt_USD));
            }
        }

        if (Type == "UpdateCostCat")
        {
            int RFQId = Common.CastAsInt32(context.Request.Form["RFQId"]);
            int DocketId = Common.CastAsInt32(context.Request.Form["DocketId"]);
            int DocketJobId = Common.CastAsInt32(context.Request.Form["DocketJobId"]);
            int DocketSubJobId = Common.CastAsInt32(context.Request.Form["DocketSubJobId"]);
            string SubJobCode = context.Request.Form["SubJobCode"];
            string CostCat = context.Request.Form["CostCat"].ToString();

            Common.Execute_Procedures_Select_ByQuery("UPDATE DD_Docket_RFQ_SubJobs SET [CostCategory] = '" + CostCat + "' WHERE RFQId=" + RFQId + " AND DOCKETID= " + DocketId + " AND DocketJobId= " + DocketJobId + " AND DocketSubJobId =" + DocketSubJobId);

            decimal AmtYC, AmtNY;
            
            Common.Set_Procedures("DD_GETCostTracking_Data");
            Common.Set_ParameterLength(5);
            Common.Set_Parameters(
               new MyParameter("@RFQId", RFQId),
               new MyParameter("@DocketId", DocketId),
               new MyParameter("@DocketJobId", DocketJobId),
               new MyParameter("@DocketSubJobId", DocketSubJobId),
               new MyParameter("@SubJobCode", SubJobCode)
               );

            DataSet ds = Common.Execute_Procedures_Select();

            AmtYC = Common.CastAsDecimal(ds.Tables[3].Rows[0]["EstAmount_USD_TotalYC"]);
            AmtNY = Common.CastAsDecimal(ds.Tables[3].Rows[0]["EstAmount_USD_TotalNY"]);

            context.Response.Write(FormatCurr_WithComma(AmtYC) + "|" + FormatCurr_WithComma(AmtNY));
            
        }
        
    }
    public string FormatCurr_WithComma(object in_p)
    {
        return String.Format("{0:#,###,###.##}", Common.CastAsDecimal(in_p));
    }
    public string FormatCurency(decimal data)
    {
       return string.Format("{0:0.00}",Math.Round(data,2));
    }
    public bool IsReusable {
        get {
            return false;
        }
    }

}