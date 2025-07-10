<%@ WebHandler Language="C#" Class="UpdateQuote" %>

using System;
using System.Web;
using System.Data;

public class UpdateQuote : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string Type = context.Request.Form["Type"].ToString();
        if(Type == "QuoteQty")
        {
            decimal QuoteQty=Common.CastAsDecimal(context.Request.Form["QuoteQty"]);
            decimal Rate=Common.CastAsDecimal(context.Request.Form["Rate"]);
            decimal Disc=Common.CastAsDecimal(context.Request.Form["Disc"]);
            int RFQId = Common.CastAsInt32(context.Request.Form["RFQId"]);
            int DocketId = Common.CastAsInt32(context.Request.Form["DocketId"]);
            int DocketJobId = Common.CastAsInt32(context.Request.Form["DocketJobId"]);
            int DocketSubJobId = Common.CastAsInt32(context.Request.Form["DocketSubJobId"]);
        
            Common.Set_Procedures("DD_UpdateQuote");
            Common.Set_ParameterLength(7);
            Common.Set_Parameters(
               new MyParameter("@RFQId", RFQId),
               new MyParameter("@DocketId", DocketId),
               new MyParameter("@DocketJobId", DocketJobId),
               new MyParameter("@DocketSubJobId", DocketSubJobId),
               new MyParameter("@QuoteQty", QuoteQty),
               new MyParameter("@UnitPrice", Rate),
               new MyParameter("@DiscountPer", Disc)
               );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            decimal NetAmt;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                NetAmt = Common.CastAsDecimal(ds.Tables[0].Rows[0]["NetAmount"]);
                
                //DataTable dtTotal = Common.Execute_Procedures_Select_ByQuery("SELECT ISNULL(SUM(NetAmount),0) FROM DD_Docket_RFQ_SubJobs WHERE RFQID=" + RFQId + " AND DOCKETID=" + DocketId);
                DataTable dtTotal = Common.Execute_Procedures_Select_ByQuery("SELECT ISNULL(SUM(NetAmount),0) FROM DD_Docket_RFQ_SubJobs WHERE RFQID=" + RFQId + " AND DOCKETID=" + DocketId + " AND DocketJobId IN (SELECT DocketJobId FROM DD_Docket_RFQ_Jobs WHERE RFQID=" + RFQId + " AND DOCKETID=" + DocketId + "  AND CatId = (SELECT [CatId] FROM DD_Docket_RFQ_Jobs WHERE RFQID=" + RFQId + " AND DOCKETID=" + DocketId + "  AND DocketJobId = " + DocketJobId + "))");
                decimal Total=Common.CastAsDecimal(dtTotal.Rows[0][0]);

                DataTable dtYard = Common.Execute_Procedures_Select_ByQuery("SELECT ISNULL(SUM(NetAmount),0) FROM DD_Docket_RFQ_SubJobs WHERE RFQID=" + RFQId + " AND DOCKETID=" + DocketId + " AND DocketJobId IN (SELECT DocketJobId FROM DD_Docket_RFQ_Jobs WHERE RFQID=" + RFQId + " AND DOCKETID=" + DocketId + "  AND CatId = (SELECT [CatId] FROM DD_Docket_RFQ_Jobs WHERE RFQID=" + RFQId + " AND DOCKETID=" + DocketId + "  AND DocketJobId = " + DocketJobId + ")) AND ISNULL(CostCategory,'N') = 'Y' ");
                decimal YardCostTotal = Common.CastAsDecimal(dtYard.Rows[0][0]);

                DataTable dtNonYard = Common.Execute_Procedures_Select_ByQuery("SELECT ISNULL(SUM(NetAmount),0) FROM DD_Docket_RFQ_SubJobs WHERE RFQID=" + RFQId + " AND DOCKETID=" + DocketId + " AND DocketJobId IN (SELECT DocketJobId FROM DD_Docket_RFQ_Jobs WHERE RFQID=" + RFQId + " AND DOCKETID=" + DocketId + "  AND CatId = (SELECT [CatId] FROM DD_Docket_RFQ_Jobs WHERE RFQID=" + RFQId + " AND DOCKETID=" + DocketId + "  AND DocketJobId = " + DocketJobId + ")) AND ISNULL(CostCategory,'N') = 'N' ");
                decimal NonYardCostTotal = Common.CastAsDecimal(dtNonYard.Rows[0][0]);

                context.Response.Write(NetAmt.ToString() + "," + Total.ToString() + "," + YardCostTotal + "," + NonYardCostTotal); 
            }            
        }

        if (Type == "SUPQuoteQty")
        {
            decimal QuoteQty = Common.CastAsDecimal(context.Request.Form["QuoteQty"]);
            decimal Rate = Common.CastAsDecimal(context.Request.Form["Rate"]);
            decimal Disc = Common.CastAsDecimal(context.Request.Form["Disc"]);
            int RFQId = Common.CastAsInt32(context.Request.Form["RFQId"]);
            int DocketId = Common.CastAsInt32(context.Request.Form["DocketId"]);
            int DocketJobId = Common.CastAsInt32(context.Request.Form["DocketJobId"]);
            int DocketSubJobId = Common.CastAsInt32(context.Request.Form["DocketSubJobId"]);

            Common.Set_Procedures("DD_UpdateSUPQuote");
            Common.Set_ParameterLength(7);
            Common.Set_Parameters(
               new MyParameter("@RFQId", RFQId),
               new MyParameter("@DocketId", DocketId),
               new MyParameter("@DocketJobId", DocketJobId),
               new MyParameter("@DocketSubJobId", DocketSubJobId),
               new MyParameter("@QuoteQty", QuoteQty),
               new MyParameter("@UnitPrice", Rate),
               new MyParameter("@DiscountPer", Disc)
               );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            decimal NetAmt;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                NetAmt = Common.CastAsDecimal(ds.Tables[0].Rows[0]["SUPNetAmount_USD"]);
                DataTable dtTotal = Common.Execute_Procedures_Select_ByQuery("SELECT ISNULL(SUM(SUPNetAmount_USD),0) FROM DD_Docket_RFQ_SubJobs WHERE RFQID=" + RFQId + " AND DOCKETID=" + DocketId);
                decimal Total = Common.CastAsDecimal(dtTotal.Rows[0][0]);

                context.Response.Write(NetAmt.ToString() + "," + Total.ToString());
            }
        }

        if (Type == "ViewSubJobDescr")
        {

            //int RFQId = Common.CastAsInt32(context.Request.Form["RFQId"]);
            //int DocketId = Common.CastAsInt32(context.Request.Form["DocketId"]);
            //int DocketJobId = Common.CastAsInt32(context.Request.Form["DocketJobId"]);
            //int DocketSubJobId = Common.CastAsInt32(context.Request.Form["DocketSubJobId"]);

            //DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT SubJobName,LongDescr,CASE WHEN CostCategory = 'Y' THEN 'Yard Cost' WHEN CostCategory = 'N' THEN 'Non Yard Cost' ELSE '' END AS CostCategory ,CASE WHEN OutsideRepair = 'Y' THEN 'Yes' WHEN OutsideRepair = 'N' THEN 'No' ELSE '' END AS OutsideRepair FROM DD_Docket_RFQ_SubJobs  WHERE RFQId=" + RFQId + " AND DocketId=" + DocketId + " AND DocketJobId=" + DocketJobId + "  AND DocketSubJobId=" + DocketSubJobId);
            //string SubJobName = dt.Rows[0]["SubJobName"].ToString();
            //string LongDescr = dt.Rows[0]["LongDescr"].ToString();
            //string CostCategory = dt.Rows[0]["CostCategory"].ToString();
            //string OutsideRepair = dt.Rows[0]["OutsideRepair"].ToString();

            //string res = "{^sd^:^" + SubJobName + "^, ^ld^:^" + LongDescr + "^, ^cc^:^" + CostCategory + "^, ^osr^:^" + OutsideRepair + "^}";
            //res = res.Replace("\r\n", "#NEWLINE#");
            //res = res.Replace("\"", "`");
            //res = res.Replace("^", "\"");
            //context.Response.Write(res);     
            
            
            int RFQId = Common.CastAsInt32(context.Request.Form["RFQId"]);
            int DocketId = Common.CastAsInt32(context.Request.Form["DocketId"]);
            int DocketJobId = Common.CastAsInt32(context.Request.Form["DocketJobId"]);
            int DocketSubJobId = Common.CastAsInt32(context.Request.Form["DocketSubJobId"]);

            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT SubJobName,LongDescr,CASE WHEN CostCategory = 'Y' THEN 'Yard Cost' WHEN CostCategory = 'N' THEN 'Non Yard Cost' ELSE '' END AS CostCategory ,CASE WHEN OutsideRepair = 'Y' THEN 'Yes' WHEN OutsideRepair = 'N' THEN 'No' ELSE '' END AS OutsideRepair,CASE WHEN ISNULL(ReqForJobTrack, 'Y') = 'Y' THEN 'Yes' WHEN ISNULL(ReqForJobTrack, 'Y') = 'N' THEN 'No' ELSE '' END AS ReqForJobTrack FROM DD_Docket_RFQ_SubJobs  WHERE RFQId=" + RFQId + " AND DocketId=" + DocketId + " AND DocketJobId=" + DocketJobId + "  AND DocketSubJobId=" + DocketSubJobId);
            string SubJobName = dt.Rows[0]["SubJobName"].ToString();
            string LongDescr = dt.Rows[0]["LongDescr"].ToString();
            string CostCategory = dt.Rows[0]["CostCategory"].ToString();
            string OutsideRepair = dt.Rows[0]["OutsideRepair"].ToString();
            string ReqJT = dt.Rows[0]["ReqForJobTrack"].ToString();

            string res = "{^sd^:^" + SubJobName + "^, ^ld^:^" + LongDescr + "^, ^cc^:^" + CostCategory + "^, ^osr^:^" + OutsideRepair + "^,^JT^:^" + ReqJT + "^}"; 
            res = res.Replace("\r\n", "#NEWLINE#");
            res = res.Replace("\"", "`");
            res = res.Replace("^", "\"");
            context.Response.Write(res);             
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

        if (Type == "SUPRemarks")
        {
            int RFQId = Common.CastAsInt32(context.Request.Form["RFQId"]);
            int DocketId = Common.CastAsInt32(context.Request.Form["DocketId"]);
            int DocketJobId = Common.CastAsInt32(context.Request.Form["DocketJobId"]);
            int DocketSubJobId = Common.CastAsInt32(context.Request.Form["DocketSubJobId"]);

            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT VendorRemarks, SUPRemarks FROM DD_Docket_RFQ_SubJobs  WHERE RFQId=" + RFQId + " AND DocketId=" + DocketId + " AND DocketJobId=" + DocketJobId + "  AND DocketSubJobId=" + DocketSubJobId);
            string res = dt.Rows[0]["SUPRemarks"].ToString() + "^" + dt.Rows[0]["VendorRemarks"].ToString();
            context.Response.Write(res);
        }

        if (Type == "ViewJobDescr")
        {
            int RFQId = Common.CastAsInt32(context.Request.Form["RFQId"]);
            int DocketId = Common.CastAsInt32(context.Request.Form["DocketId"]);
            int DocketJobId = Common.CastAsInt32(context.Request.Form["DocketJobId"]);            

            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT JobDesc FROM DD_Docket_RFQ_Jobs WHERE RFQId=" + RFQId + " AND DocketId=" + DocketId + " AND DocketJobId=" + DocketJobId );
            string res = dt.Rows[0]["JobDesc"].ToString();           
            context.Response.Write(res);
        }
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}