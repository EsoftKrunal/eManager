using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;
using System.IO;
using Ionic.Zip;   

public partial class NextYearBudgetPublish : System.Web.UI.Page
{
    #region Declarations
    AuthenticationManager authRecInv;
    static Random R = new Random();
    string PublishPath;
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    #endregion
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
       ProjectCommon.SessionCheck();
        //---------------------------------------
        #region --------- USER RIGHTS MANAGEMENT -----------
        try
        {
            authRecInv = new AuthenticationManager(271, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
            if (!(authRecInv.IsView))
            {
                Response.Redirect("~/NoPermissionBudget.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/NoPermissionBudget.aspx?Message=" + ex.Message);
        }

        #endregion ----------------------------------------
        if (!IsPostBack)
        {
            btnPublish.Visible = authRecInv.IsVerify2;
            BindCompany();
            BindData();
        }
    }
    public void BindCompany()
    {
        string sql = "SELECT VW_sql_tblSMDPRCompany.Company, VW_sql_tblSMDPRCompany.ReportCo " +
            " ,(VW_sql_tblSMDPRCompany.Company + '-' + VW_sql_tblSMDPRCompany.[Company Name]) as CompName" +
        " FROM VW_sql_tblSMDPRCompany WHERE (((VW_sql_tblSMDPRCompany.InAccts)=1)) and (((VW_sql_tblSMDPRCompany.Active)='Y'))";
        DataTable DtCompany = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtCompany != null)
        {
            ddlCompany.DataSource = DtCompany;
            ddlCompany.DataTextField = "CompName";
            ddlCompany.DataValueField = "Company";
            ddlCompany.DataBind();
            ddlCompany.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< -- Select Company -- >", ""));
            ddlCompany.SelectedIndex = 0;
        }
    }
    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
    public void BindData()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT *,convert(varchar,PublishedOn,106) as PublishedOnF FROM DBO.MW_CompanyBudgetForeCast WHERE COMPCODE='" + ddlCompany.SelectedValue + "' and YEAR=" + (DateTime.Today.Year + 1).ToString());
        rpt_Data.DataSource = dt;
        rpt_Data.DataBind();
    }
    protected void btnPublish_Click(object sender, EventArgs e)
    {
        string Year = (DateTime.Today.Year+1).ToString();
        string CompanyCode = ddlCompany.SelectedValue;
        string CompanyName = ddlCompany.SelectedItem.Text;

        PublishPath = Server.MapPath("~/EMANAGERBLOB/OPEX/Publish_NY/" + CompanyCode + "/");
        if (!(Directory.Exists(PublishPath)))
        {
            Directory.CreateDirectory(PublishPath);
        }
        else
        {
            string[] Files = Directory.GetFiles(PublishPath);
            foreach (string SFile in Files)
            {
                File.Delete(SFile);
            }
        }

        Export_BF(Year,CompanyCode, CompanyName);
        Export_CompanySummary_ByVessel(Year,CompanyCode, CompanyName);
        Export_CompanySummary(CompanyCode, CompanyName, Common.CastAsInt32(Year));

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT ISNULL(MAX(CAST(RIGHT( FILENAME ,3) AS INT)),0)+1 FROM DBO.MW_CompanyBudgetForeCast WHERE COMPCODE='" + CompanyCode + "'");
        string NewNo = dt.Rows[0][0].ToString().Trim().PadLeft(3, '0');

        string FileName = CompanyCode + "_" + Year + "_" + NewNo;
        
        string ZipData = Server.MapPath("~/EMANAGERBLOB/OPEX/Publish_NY/" + FileName + ".zip");
        if (File.Exists(ZipData)) { File.Delete(ZipData); }
        using (ZipFile zip = new ZipFile())
        {
            string[] Files = Directory.GetFiles(PublishPath);
            foreach (string SFile in Files)
            {
                zip.AddFile(SFile);
            }
            zip.Save(ZipData);
        }
        string UserName = Session["FullName"].ToString();
        Common.Execute_Procedures_Select_ByQuery("INSERT INTO DBO.MW_CompanyBudgetForeCast(COMPCODE,YEAR,FILENAME,PUBLISHEDBY,PUBLISHEDON) VALUES('" + CompanyCode + "'," + Year + ",'" + FileName + "','" + UserName + "',GETDATE())");
        BindData();

        //Response.Clear();
        //Response.ContentType = "application/zip";
        //Response.AddHeader("Content-Type", "application/zip");
        //Response.AddHeader("Content-Disposition", "inline;filename=" + ddlVessels.SelectedValue + ".zip");
        //Response.WriteFile(ZipData);
        //Response.End();
    }
    //--------------- EXPORT COMPANY SUMMARY ---------------------------------
    protected void BinddATA(String CoCode, string VeselCode, int BYear, ref int retDaysCnt, ref int retDateCntLast, ref DataTable Dt1, ref DataTable Dt2)
    {
        int LastMonth = 12;
        int TodayYear = BYear;
        string Qry = "select FORECAST from dbo.tblsmdbudgetforecastyear where cocode='" + CoCode + "' AND YEAR=" + (BYear - 2).ToString() + " AND ACCOUNTNUMBER=5100 AND SHIPID='" + VeselCode + "'";
        DataTable dtheader = Common.Execute_Procedures_Select_ByQuery(Qry);
        int Amt_5100 = 0;
        if (dtheader.Rows.Count > 0)
        {
            Amt_5100 = Common.CastAsInt32(dtheader.Rows[0][0]);
            if (Amt_5100 > 0)
                Amt_5100 = ProjectCommon.Get_ManningAmount(CoCode, VeselCode, BYear);
        }

        //---------------------------
        DataTable dt = new DataTable();
        DataTable dt_1 = new DataTable();
        dt.Columns.Add("MidCatId");
        dt.Columns.Add("AccountHead");
        dt.Columns.Add("ActComm");
        dt.Columns.Add("Proj");
        dt.Columns.Add("Bud");
        dt.Columns.Add("Fcast");
        dt.Columns.Add("Var1");
        dt.Columns.Add("Var2");
        dt.Columns.Add("Var3");
        dt.Columns.Add("Comments");
        //---------------------------
        dt_1.Columns.Add("MidCatId");
        dt_1.Columns.Add("AccountHead");
        dt_1.Columns.Add("ActComm");
        dt_1.Columns.Add("Proj");
        dt_1.Columns.Add("Bud");
        dt_1.Columns.Add("Fcast");
        dt_1.Columns.Add("Var1");
        dt_1.Columns.Add("Var2");
        dt_1.Columns.Add("Var3");
        dt_1.Columns.Add("Comments");
        //---------------------------
        string[] Names = { "Manning :", "Consumables :", "Lube Oils :", "Spare, Maintenance & Repair :", "General Expenses :", "Insurance :", "Management & Admin Fee:" };
        string[] Names1 = { "Damage / Repairs :", "Principle Controlled Expenses :", "Capital Expenditure :", "Dry Docking :" };
        int start = 2;

        decimal ColumnSum = 0, ColumnSumLast = 0;
        decimal ColumnSum2 = 0, ColumnSum3 = 0;
        //---------------------------
        DataTable dtDays = Common.Execute_Procedures_Select_ByQuery("select top 1 Days From dbo.v_New_CurrYearBudgetHome Where shipid='" + VeselCode + "' AND year=" + TodayYear.ToString() + " order by days desc");
        DataTable dtDaysLast = Common.Execute_Procedures_Select_ByQuery("select top 1 Days From dbo.v_New_CurrYearBudgetHome Where shipid='" + VeselCode + "' AND year=" + (TodayYear - 1).ToString() + " order by days desc");

        int DaysCnt = 1;
        if (dtDays != null)
        {
            if (dtDays.Rows.Count > 0)
            {
                DaysCnt = Common.CastAsInt32(dtDays.Rows[0][0]);
            }
        }
        int DaysCntLast = 1;
        if (dtDaysLast != null)
        {
            if (dtDaysLast.Rows.Count > 0)
            {
                DaysCntLast = Common.CastAsInt32(dtDaysLast.Rows[0][0]);
            }
        }
        retDaysCnt = DaysCnt;
        retDateCntLast = DaysCntLast;
        //---------------------------
        DataTable dtActCom_Proj = Common.Execute_Procedures_Select_ByQuery("EXEC [dbo].[fn_NEW_GETCMBUDGETACTUAL_MIDCATWISE] '" + CoCode + "'," + DateTime.Today.Month.ToString() + "," + (TodayYear).ToString() + ",'" + VeselCode + "'");
        DataView dv = dtActCom_Proj.DefaultView;
        DataTable dtAccts = Common.Execute_Procedures_Select_ByQuery("select distinct midcatid,midcat,MajSeqNo,MidSeqNo from dbo.v_New_CurrYearBudgetHome WHERE MajCatId=6 ORDER BY MajSeqNo,MidSeqNo");
        DataTable dtAccts1 = Common.Execute_Procedures_Select_ByQuery("select distinct midcatid,midcat,MajSeqNo,MidSeqNo from dbo.v_New_CurrYearBudgetHome WHERE MajCatId<>6 And MidcatId not in (25,26) ORDER BY MajSeqNo,MidSeqNo");
        //---------------------------
        for (int i = 0; i <= dtAccts.Rows.Count - 1; i++)
        {
            int RowSum = 0;
            dv.RowFilter = "MidCatId=" + dtAccts.Rows[i][0].ToString();
            DataTable dt1 = dv.ToTable();
            decimal ActComm = 0, Projected = 0, Budget = 0, ForeCast = 0;

            if (i != 0) { Amt_5100 = 0; }
            if (Amt_5100 > 0)
            {
                ColumnSum3 += Amt_5100;

                ActComm = 0;
                ColumnSum2 += Common.CastAsInt32(dt1.Rows[0]["ACT_CONS"]);
                ActComm = Common.CastAsDecimal(dt1.Rows[0]["ACT_CONS"]);

                Projected = Amt_5100;
            }
            else
            {
                if (dt1.Rows.Count > 0)
                {
                    ColumnSum2 += Common.CastAsInt32(dt1.Rows[0]["ACT_CONS"]);
                    ColumnSum3 += Common.CastAsInt32(dt1.Rows[0]["PROJECTED"]);

                    ActComm = Common.CastAsDecimal(dt1.Rows[0]["ACT_CONS"]);
                    Projected = Common.CastAsDecimal(dt1.Rows[0]["PROJECTED"]);
                }
                else
                {
                    ActComm = 0;
                    Projected = 0;
                }
            }
            DataTable dtShip = Common.Execute_Procedures_Select_ByQuery("select budget from dbo.v_New_CurrYearBudgetHome where shipid='" + VeselCode + "' AND year=" + (TodayYear - 1).ToString() + " and  midcatid=" + dtAccts.Rows[i][0].ToString());
            if (dtShip != null)
            {
                if (dtShip.Rows.Count > 0)
                {
                    ColumnSum += Common.CastAsInt32(dtShip.Rows[0][0]);
                    Budget = Common.CastAsDecimal(dtShip.Rows[0][0]);
                }
                else
                {
                    Budget = 0;
                }
            }
            else
            {
                Budget = 0;
            }

            DataTable dtShipLast = Common.Execute_Procedures_Select_ByQuery("select nextyearforecastamount from dbo.v_New_CurrYearBudgetHome where shipid='" + VeselCode + "' AND year=" + (TodayYear - 1).ToString() + " and  midcatid=" + dtAccts.Rows[i][0].ToString());
            if (dtShipLast != null)
            {
                if (dtShipLast.Rows.Count > 0)
                {
                    ColumnSumLast += Common.CastAsInt32(dtShipLast.Rows[0][0]);
                    ForeCast = Common.CastAsDecimal(dtShipLast.Rows[0][0]);
                }
                else
                {
                    ForeCast = 0;
                }
            }
            else
            {
                ForeCast = 0;
            }

            DataRow dr = dt.NewRow();
            dr["MidcatId"] = dtAccts.Rows[i][0].ToString();
            dr["AccountHead"] = Names[i];
            dr["ActComm"] = ActComm.ToString();
            dr["Proj"] = Projected.ToString();
            dr["Bud"] = Budget.ToString();
            dr["Fcast"] = ForeCast.ToString();

            if (Budget > 0)
                ActComm = ((Projected - Budget) / Budget) * 100;
            else
                ActComm = 0;

            if (Projected > 0)
                Projected = ((ForeCast - Projected) / Projected) * 100;
            else
                Projected = 0;

            if (Budget > 0)
                Budget = ((ForeCast - Budget) / Budget) * 100;
            else
                Budget = 0;

            start++;

            dr["Var1"] = Math.Round(ActComm, 0);
            dr["Var2"] = Math.Round(Budget, 0);
            dr["Var3"] = Math.Round(Projected, 0);
            dt.Rows.Add(dr);
        }

        start++;
        //----------------------------------------------
        for (int i = 0; i <= dtAccts1.Rows.Count - 1; i++)
        {
            int RowSum = 0;
            dv.RowFilter = "MidCatId=" + dtAccts1.Rows[i][0].ToString();
            DataTable dt1 = dv.ToTable();
            decimal ActComm = 0, Projected = 0, Budget = 0, ForeCast = 0;

            if (dt1.Rows.Count > 0)
            {
                ColumnSum2 += Common.CastAsInt32(dt1.Rows[0]["ACT_CONS"]);
                ColumnSum3 += Common.CastAsInt32(dt1.Rows[0]["PROJECTED"]);

                ActComm = Common.CastAsDecimal(dt1.Rows[0]["ACT_CONS"]);
                Projected = Common.CastAsDecimal(dt1.Rows[0]["PROJECTED"]);
            }
            else
            {
                ActComm = 0;
                Projected = 0;
            }
            DataTable dtShip = Common.Execute_Procedures_Select_ByQuery("select budget from dbo.v_New_CurrYearBudgetHome where shipid='" + VeselCode + "' AND year=" + (TodayYear - 1).ToString() + " and  midcatid=" + dtAccts1.Rows[i][0].ToString());
            if (dtShip != null)
            {
                if (dtShip.Rows.Count > 0)
                {
                    ColumnSum += Common.CastAsInt32(dtShip.Rows[0][0]);
                    Budget = Common.CastAsDecimal(dtShip.Rows[0][0]);
                }
                else
                {
                    Budget = 0;
                }
            }
            else
            {
                Budget = 0;
            }

            DataTable dtShipLast = Common.Execute_Procedures_Select_ByQuery("select nextyearforecastamount from dbo.v_New_CurrYearBudgetHome where shipid='" + VeselCode + "' AND year=" + (TodayYear - 1).ToString() + " and  midcatid=" + dtAccts1.Rows[i][0].ToString());
            if (dtShipLast != null)
            {
                if (dtShipLast.Rows.Count > 0)
                {
                    ColumnSumLast += Common.CastAsInt32(dtShipLast.Rows[0][0]);
                    ForeCast = Common.CastAsDecimal(dtShipLast.Rows[0][0]);
                }
                else
                {
                    ForeCast = 0;
                }
            }
            else
            {
                ForeCast = 0;
            }

            DataRow dr = dt_1.NewRow();
            dr["MidcatId"] = dtAccts1.Rows[i][0].ToString();
            dr["AccountHead"] = Names1[i];
            dr["ActComm"] = ActComm.ToString();
            dr["Proj"] = Projected.ToString();
            dr["Bud"] = Budget.ToString();
            dr["Fcast"] = ForeCast.ToString();


            if (Budget > 0)
                ActComm = ((Projected - Budget) / Budget) * 100;
            else
                ActComm = 0;

            if (Projected > 0)
                Projected = ((ForeCast - Projected) / Projected) * 100;
            else
                Projected = 0;

            if (Budget > 0)
                Budget = ((ForeCast - Budget) / Budget) * 100;
            else
                Budget = 0;

            start++;

            dr["Var1"] = Math.Round(ActComm, 0);
            dr["Var2"] = Math.Round(Budget, 0);
            dr["Var3"] = Math.Round(Projected, 0);
            dt_1.Rows.Add(dr);
        }
        Dt1 = dt;
        Dt2 = dt_1;
    }
    protected void Export_CompanySummary(string CompanyCode,string CompanyName, int PubYear)
    {
        int DaysTillyearEnd = new DateTime(DateTime.Today.Year, 12, 31).Subtract(DateTime.Today).Days;
        
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        DataTable dt = new DataTable();
        DataTable dt_1 = new DataTable();
        dt.Columns.Add("MidCatId");
        dt.Columns.Add("AccountHead");
        dt.Columns.Add("ActComm");
        dt.Columns.Add("Proj");
        dt.Columns.Add("Bud");
        dt.Columns.Add("Fcast");
        dt.Columns.Add("Var1");
        dt.Columns.Add("Var2");
        dt.Columns.Add("Var3");
        dt.Columns.Add("Comments");
        //---------------------------
        dt_1.Columns.Add("MidCatId");
        dt_1.Columns.Add("AccountHead");
        dt_1.Columns.Add("ActComm");
        dt_1.Columns.Add("Proj");
        dt_1.Columns.Add("Bud");
        dt_1.Columns.Add("Fcast");
        dt_1.Columns.Add("Var1");
        dt_1.Columns.Add("Var2");
        dt_1.Columns.Add("Var3");
        dt_1.Columns.Add("Comments");
        //---------------------------

        string[] NamesId = { "12 :", "4:", "27", "14", "28", "11", "22" };
        string[] NamesId1 = { "5", "13", "2", "6" };

        string[] Names = { "Manning :", "Consumables :", "Lube Oils :", "Spare, Maintenance & Repair :", "General Expenses :", "Insurance :", "Management & Admin Fee:" };
        string[] Names1 = { "Damage / Repairs :", "Principle Controlled Expenses :", "Capital Expenditure :", "Dry Docking :" };

        for (int i = 0; i <= Names.Length - 1; i++)
        {
            dt.Rows.Add(dt.NewRow());
            dt.Rows[i][0] = NamesId[i].ToString();
            dt.Rows[i][1] = Names[i].ToString();
        }

        for (int i = 0; i <= Names1.Length - 1; i++)
        {
            dt_1.Rows.Add(dt_1.NewRow());
            dt_1.Rows[i][0] = NamesId1[i].ToString();
            dt_1.Rows[i][1] = Names1[i].ToString();
        }

        DataTable Temp1 = new DataTable();
        DataTable Temp2 = new DataTable();

        string CoCode = CompanyCode;
        string VesselCode = "";
        
        DataTable dtShips = Common.Execute_Procedures_Select_ByQuery("Select VesselCode As ShipID,  AccontCompany As Company from Vessel with(nolock)  WHERE AccontCompany='" + CoCode + "' AND VESSELNO>0 ");

        int retDaysCnt_Total = 0;
        int retDaysCntLast_Total = 0;
        int Temp_retDaysCnt = 0;
        int Temp_retDaysCntLast = 0;
        int Temp_DaysTilltoday = 0;

        foreach (DataRow dr in dtShips.Rows)
        {
            CoCode = dr["COMPANY"].ToString();
            VesselCode = dr["SHIPID"].ToString();

            int LastYear = PubYear - 1;
            BinddATA(CoCode, VesselCode, LastYear, ref Temp_retDaysCnt, ref Temp_retDaysCntLast, ref Temp1, ref Temp2);
            //BinddATA(CoCode, VesselCode, 2012, ref Temp_retDaysCnt, ref Temp_retDaysCntLast, ref Temp1, ref Temp2);
            retDaysCnt_Total += Temp_retDaysCnt;
            retDaysCntLast_Total += Temp_retDaysCntLast;

            //------- New Line
            int DaysTilltoday = Temp_retDaysCntLast - DaysTillyearEnd;
            if (DaysTilltoday > 365)
                DaysTilltoday = 365;

            Temp_DaysTilltoday += DaysTilltoday;
            //-------------------------

            for (int i = 0; i <= Temp1.Rows.Count - 1; i++)
            {
                dt.Rows[i][2] = Common.CastAsInt32(dt.Rows[i][2]) + Common.CastAsInt32(Temp1.Rows[i][2]);
                dt.Rows[i][3] = Common.CastAsInt32(dt.Rows[i][3]) + Common.CastAsInt32(Temp1.Rows[i][3]);
                dt.Rows[i][4] = Common.CastAsInt32(dt.Rows[i][4]) + Common.CastAsInt32(Temp1.Rows[i][4]);
                dt.Rows[i][5] = Common.CastAsInt32(dt.Rows[i][5]) + Common.CastAsInt32(Temp1.Rows[i][5]);
            }

            for (int i = 0; i <= Temp2.Rows.Count - 1; i++)
            {
                dt_1.Rows[i][2] = Common.CastAsInt32(dt_1.Rows[i][2]) + Common.CastAsInt32(Temp2.Rows[i][2]);
                dt_1.Rows[i][3] = Common.CastAsInt32(dt_1.Rows[i][3]) + Common.CastAsInt32(Temp2.Rows[i][3]);
                dt_1.Rows[i][4] = Common.CastAsInt32(dt_1.Rows[i][4]) + Common.CastAsInt32(Temp2.Rows[i][4]);
                dt_1.Rows[i][5] = Common.CastAsInt32(dt_1.Rows[i][5]) + Common.CastAsInt32(Temp2.Rows[i][5]);
            }
        }
        //-------------------------- CALCULATING VARIANCE
        string Var1 = "", Var2 = "", Var3 = "";
        decimal ColumnSum_ActComm = 0;
        decimal ColumnSum_Budget = 0;
        decimal ColumnSum_Projected = 0;
        decimal ColumnSum_ForeCast = 0;

        foreach (DataRow dr in dt.Rows)
        {
            decimal ActComm = Common.CastAsDecimal(dr["ActComm"]);
            decimal Budget = Common.CastAsDecimal(dr["Bud"]);
            decimal Projected = Common.CastAsDecimal(dr["Proj"]);
            decimal ForeCast = Common.CastAsDecimal(dr["Fcast"]);

            ColumnSum_ActComm += ActComm;
            ColumnSum_Budget += Budget;
            ColumnSum_Projected += Projected;
            ColumnSum_ForeCast += ForeCast;
            //5696

            Calculate_Variance(ActComm, Budget, Projected, ForeCast, ref Var1, ref Var2, ref Var3);
            dr["Var1"] = Var1;
            dr["Var2"] = Var2;
            dr["Var3"] = Var3;
        }
        //--------- TOTAL 
        dt.Rows.Add(dt.NewRow());
        dt.Rows[dt.Rows.Count - 1][0] = "";
        dt.Rows[dt.Rows.Count - 1][1] = "Total (US$): ";
        dt.Rows[dt.Rows.Count - 1]["ActComm"] = ColumnSum_ActComm.ToString();
        dt.Rows[dt.Rows.Count - 1]["Bud"] = ColumnSum_Budget.ToString();
        dt.Rows[dt.Rows.Count - 1]["Proj"] = ColumnSum_Projected.ToString();
        dt.Rows[dt.Rows.Count - 1]["Fcast"] = ColumnSum_ForeCast.ToString();
        Calculate_Variance(ColumnSum_ActComm, ColumnSum_Budget, ColumnSum_Projected, ColumnSum_ForeCast, ref Var1, ref Var2, ref Var3);
        dt.Rows[dt.Rows.Count - 1]["Var1"] = Var1;
        dt.Rows[dt.Rows.Count - 1]["Var2"] = Var2;
        dt.Rows[dt.Rows.Count - 1]["Var3"] = Var3;
        //--------- AVERAGE 
        dt.Rows.Add(dt.NewRow());
        dt.Rows[dt.Rows.Count - 1][0] = "";
        dt.Rows[dt.Rows.Count - 1][1] = "Avg Daily Cost(US$): ";
        dt.Rows[dt.Rows.Count - 1]["ActComm"] = Math.Round(ColumnSum_ActComm / Temp_DaysTilltoday).ToString();
        dt.Rows[dt.Rows.Count - 1]["Bud"] = Math.Round(ColumnSum_Budget / retDaysCntLast_Total).ToString();
        dt.Rows[dt.Rows.Count - 1]["Proj"] = Math.Round(ColumnSum_Projected / retDaysCntLast_Total).ToString();
        dt.Rows[dt.Rows.Count - 1]["Fcast"] = Math.Round(ColumnSum_ForeCast / retDaysCnt_Total).ToString();

        //---------  IIND TABLE
        foreach (DataRow dr in dt_1.Rows)
        {
            decimal ActComm = Common.CastAsDecimal(dr["ActComm"]);
            decimal Budget = Common.CastAsDecimal(dr["Bud"]);
            decimal Projected = Common.CastAsDecimal(dr["Proj"]);
            decimal ForeCast = Common.CastAsDecimal(dr["Fcast"]);

            ColumnSum_ActComm += ActComm;
            ColumnSum_Budget += Budget;
            ColumnSum_Projected += Projected;
            ColumnSum_ForeCast += ForeCast;

            Calculate_Variance(ActComm, Budget, Projected, ForeCast, ref Var1, ref Var2, ref Var3);
            dr["Var1"] = Var1;
            dr["Var2"] = Var2;
            dr["Var3"] = Var3;
        }
        //--------- TOTAL 
        dt_1.Rows.Add(dt_1.NewRow());
        dt_1.Rows[dt_1.Rows.Count - 1][0] = "";
        dt_1.Rows[dt_1.Rows.Count - 1][1] = "Gross Total (US$): ";
        dt_1.Rows[dt_1.Rows.Count - 1]["ActComm"] = ColumnSum_ActComm.ToString();
        dt_1.Rows[dt_1.Rows.Count - 1]["Bud"] = ColumnSum_Budget.ToString();
        dt_1.Rows[dt_1.Rows.Count - 1]["Proj"] = ColumnSum_Projected.ToString();
        dt_1.Rows[dt_1.Rows.Count - 1]["Fcast"] = ColumnSum_ForeCast.ToString();
        Calculate_Variance(ColumnSum_ActComm, ColumnSum_Budget, ColumnSum_Projected, ColumnSum_ForeCast, ref Var1, ref Var2, ref Var3);
        dt_1.Rows[dt_1.Rows.Count - 1]["Var1"] = Var1;
        dt_1.Rows[dt_1.Rows.Count - 1]["Var2"] = Var2;
        dt_1.Rows[dt_1.Rows.Count - 1]["Var3"] = Var3;
        //--------- 

        Session["d11"] = dt;
        Session["d22"] = dt_1;
        Session["DaysCnt"] = retDaysCnt_Total;
        Session["DaysCntLast"] = retDaysCntLast_Total;

        dt.Columns.RemoveAt(0);
        dt_1.Columns.RemoveAt(0);

        dt.Columns.RemoveAt(dt.Columns.Count-1);
        dt_1.Columns.RemoveAt(dt_1.Columns.Count - 1);

        ExportToPDF_CompanySummary(CompanyName,dt,dt_1,PubYear);
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "a", "window.open('ComapnyBFCSummaryReport.aspx?Comp=" + ddlCompany.SelectedItem.Text + "&Year=" + lblyear.Text + "');", true);
    }
    private void ExportToPDF_CompanySummary(string CompanyName,DataTable dt, DataTable dt_1,int ForeCastYear)
    {
        try
        {
            //Document document = new Document(PageSize.LETTER.Rotate, 10, 10, 30, 10);
            Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 10, 10, 30, 10);

            System.IO.MemoryStream msReport = new System.IO.MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, msReport);
            document.AddAuthor("MTMSM");
            document.AddSubject("Fleet Summary");
            //'Adding Header in Document
            iTextSharp.text.Image logoImg = default(iTextSharp.text.Image);
            logoImg = iTextSharp.text.Image.GetInstance(Server.MapPath("~\\Images\\MTMMLogo.jpg"));
            Chunk chk = new Chunk(logoImg, 0, 0, true);
            //Phrase p1 = new Phrase();
            //p1.Add(chk);

            iTextSharp.text.Table tb_header = new iTextSharp.text.Table(1);
            tb_header.Width = 100;
            tb_header.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tb_header.DefaultCellBorder = iTextSharp.text.Rectangle.NO_BORDER;
            tb_header.DefaultHorizontalAlignment = Element.ALIGN_LEFT;
            tb_header.BorderWidth = 0;
            tb_header.BorderColor = iTextSharp.text.Color.WHITE;
            tb_header.Cellspacing = 1;
            tb_header.Cellpadding = 1;

            Cell c1 = new Cell(chk);
            c1.HorizontalAlignment = Element.ALIGN_LEFT;
            tb_header.AddCell(c1);

            Phrase p2 = new Phrase();
            p2.Add(new Phrase("Fleet Budget Summary" + "\n" + CompanyName + "\n" + "Year-"+ ForeCastYear.ToString() + "\n", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            Cell c2 = new Cell(p2);
            c2.HorizontalAlignment = Element.ALIGN_CENTER;
            tb_header.AddCell(c2);

            //Chunk ch = new Chunk();

            HeaderFooter header = new HeaderFooter(new Phrase(""), false);
            document.Header = header;

            //header.Alignment = Element.ALIGN_LEFT;
            header.Border = iTextSharp.text.Rectangle.NO_BORDER;
            //'Adding Footer in document
            string foot_Txt = "Published On : " + DateTime.Today.ToString("dd-MMM-yyyy");
            foot_Txt = foot_Txt + "                                                                                                                ";
            foot_Txt = foot_Txt + "                                                                                                                ";
            foot_Txt = foot_Txt + "";
            HeaderFooter footer = new HeaderFooter(new Phrase(foot_Txt, FontFactory.GetFont("VERDANA", 6, iTextSharp.text.Color.DARK_GRAY)), true);
            footer.Border = iTextSharp.text.Rectangle.NO_BORDER;
            footer.Alignment = Element.ALIGN_LEFT;
            document.Footer = footer;
            //'-----------------------------------
            document.Open();
            document.Add(tb_header);
            //------------ TABLE HEADER FONT 
            iTextSharp.text.Font fCapText = FontFactory.GetFont("ARIAL", 9, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font fCapText_5 = FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL);
            //------------ TABLE HEADER ROW 
            int ColumnsCount = dt.Columns.Count;
            iTextSharp.text.Table tb1 = new iTextSharp.text.Table(ColumnsCount);
            tb1.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            tb1.Width = 100;

            //float[] ws = new float[ColumnsCount];
            //ws[0] = 15;
            //for (int i = 2; i <= ws.Length - 1; i++)
            //    ws[i] = 75 / (ws.Length - 1);

            //ws[ws.Length - 2] = ws[ws.Length - 2] + 5;
            //ws[ws.Length - 1] = ws[ws.Length - 1] + 5;
            float[] ws = { 20,10,10,10,10,10,15,15};
            tb1.Widths = ws;

            tb1.Alignment = Element.ALIGN_CENTER;
            tb1.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tb1.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
            tb1.BorderColor = iTextSharp.text.Color.WHITE;
            tb1.Cellspacing = 1;
            tb1.Cellpadding = 1;

            string[] colnamames = {   "",
                                      "[" + (ForeCastYear-1).ToString() + "]",
                                      "[" + (ForeCastYear-1).ToString() + "]",
                                      "[" + (ForeCastYear-1).ToString() + "]",
                                      "[" + ForeCastYear.ToString() + "]",
                                      (ForeCastYear-1).ToString() + "- Var % ",
                                      ForeCastYear.ToString() + " - Forecast Var%",
                                      ForeCastYear.ToString() + " - Forecast Var%"
                                   }; 

            string[] colnamames1 = {  "",
                                      "",
                                      "",
                                      "",
                                      "",
                                      "",
                                      "v/s",
                                      "v/s",
                                   };
            string[] colnamames2 = {  "AccountHead",
                                      "Act & Comm",
                                      "Projected",
                                      "Budget",
                                      "Forecast",
                                      "[ B v/s P ]",
                                      (ForeCastYear-1).ToString() + " - [B]",
                                      (ForeCastYear-1).ToString() + " - [P]",
                                   };

            for (int i = 0; i <= colnamames.Length - 1; i++)
            {
                Cell tc = new Cell(new Phrase(colnamames[i], fCapText));
                tb1.AddCell(tc);
            }
            for (int i = 0; i <= colnamames1.Length - 1; i++)
            {
                Cell tc = new Cell(new Phrase(colnamames1[i], fCapText));
                tb1.AddCell(tc);
            }
            for (int i = 0; i <= colnamames2.Length - 1; i++)
            {
                Cell tc = new Cell(new Phrase(colnamames2[i], fCapText));
                tb1.AddCell(tc);
            }

            //DataRow dr_yb = dt.Rows[0];
            //for (int i = 0; i <= dt.Columns.Count - 1; i++)
            //{
            //    Cell tc = new Cell(new Phrase(dr_yb[i].ToString(), fCapText_5));
            //    tb1.AddCell(tc);
            //}

            document.Add(tb1);
            //------------ TABLE DATA ROW 
            // data rows
            iTextSharp.text.Table tbdata = new iTextSharp.text.Table(ColumnsCount);
            tbdata.Width = 100;
            tbdata.Widths = ws;
            tbdata.Alignment = Element.ALIGN_CENTER;
            tbdata.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tbdata.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
            tbdata.BorderColor = iTextSharp.text.Color.GRAY;
            tbdata.Cellspacing = 1;
            tbdata.Cellpadding = 1;
            for (int k = 0; k <= dt.Rows.Count - 1; k++)
            {
                DataRow dr = dt.Rows[k];
                for (int i = 0; i <= dt.Columns.Count - 1; i++)
                {
                    Cell tc = new Cell(new Phrase(dr[i].ToString(), fCapText_5));
                    if (k == 7 || k == 8)
                    {
                        tc.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
                    }
                    //if (i == 0)
                    //    tc.HorizontalAlignment = Element.ALIGN_LEFT;
                    //else
                    tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tbdata.AddCell(tc);
                }
            }
            //--------------
            for (int k = 0; k <= dt_1.Rows.Count - 1; k++)
            {
                DataRow dr = dt_1.Rows[k];
                for (int i = 0; i <= dt_1.Columns.Count - 1; i++)
                {
                    Cell tc = new Cell(new Phrase(dr[i].ToString(), fCapText_5));
                    if (k == 4)
                    {
                        tc.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
                    }
                    //if (i == 0)
                    //    tc.HorizontalAlignment = Element.ALIGN_LEFT;
                    //else
                    tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tbdata.AddCell(tc);
                }
            }

            document.Add(tbdata);
            ////------------ TABLE FOOTER ROW 
            //iTextSharp.text.Table tb2 = new iTextSharp.text.Table(ColumnsCount);
            //tb2.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            //tb2.Width = 100;
            //tb2.Widths = ws;
            //tb2.Alignment = Element.ALIGN_CENTER;
            //tb2.Border = iTextSharp.text.Rectangle.NO_BORDER;
            //tb2.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
            //tb2.BorderColor = iTextSharp.text.Color.WHITE;
            //tb2.Cellspacing = 1;
            //tb2.Cellpadding = 1;

            //DataRow drF = dt.Rows[dt.Rows.Count - 1];

            //for (int i = 0; i <= dt.Columns.Count - 1; i++)
            //{
            //    Cell tc = new Cell(new Phrase(drF[i].ToString(), fCapText_5));
            //    if (i == 0)
            //        tc.HorizontalAlignment = Element.ALIGN_LEFT;
            //    else
            //        tc.HorizontalAlignment = Element.ALIGN_RIGHT;

            //    tb2.AddCell(tc);
            //}
            //------------------------------------
            //document.Add(tb2);
            document.Close();

            string FileName = PublishPath + "\\" + "Company Summary.pdf";
            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }

            FileStream fs = new FileStream(FileName, FileMode.Create);
            byte[] bb = msReport.ToArray();
            fs.Write(bb, 0, bb.Length);
            fs.Flush();
            fs.Close();
        }
        catch (System.Exception ex)
        {

        }
    }
    protected void Calculate_Variance(decimal ActComm, decimal Budget, decimal Projected, decimal ForeCast, ref string var1, ref string var2, ref string var3)
    {
        if (Budget > 0)
            ActComm = ((Projected - Budget) / Budget) * 100;
        else
            ActComm = 0;

        if (Budget > 0)
            Budget = ((ForeCast - Budget) / Budget) * 100;
        else
            Budget = 0;

        if (Projected > 0)
            Projected = ((ForeCast - Projected) / Projected) * 100;
        else
            Projected = 0;

        var1 = Math.Round(ActComm, 0).ToString() + "%";
        var2 = Math.Round(Budget, 0).ToString() + "%";
        var3 = Math.Round(Projected, 0).ToString() + "%";
    }

    //--------------- EXPORT VESSEL BUDGET FORECAST ---------------------------------
    public void Export_BF(string Year,string CompanyCode,string CompanyName)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select SHIPID,ShipName from [dbo].[VW_sql_tblSMDPRVessels] where company='" + CompanyCode + "' AND VESSELNO>0 AND ACTIVE='A'");
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                Export_BF_Vessel(Year, CompanyCode, CompanyName, dr["SHIPID"].ToString(), dr["ShipName"].ToString());
            }
        }
    }
    public void Export_BF_Vessel(string Year, string CompanyCode, string CompanyName, string VesselCode, string VesselName)
    {
        rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        string StartDate = "", EndDate = "";
        int Days = 0;
        //---------------------------------            
        string Qry = "select YearDays,approvedby,replace(convert(varchar,approvedon,106),' ','-') as ApprovedOn,UpdatedBy, " +
                           "Importedby,replace(convert(varchar,ImportedOn,106),' ','-') as ImportedOn,replace(convert(varchar,UpdatedOn,106),' ','-') as UpdatedOn," +
                           "replace(convert(varchar,vessstart,106),' ','-') as vessstart,replace(convert(varchar,vessend,106),' ','-') as vessend " +
                           "from dbo.tblsmdbudgetforecastyear where cocode='" + CompanyCode + "' and shipid='" + VesselCode + "' and [year]=" + (Common.CastAsInt32(Year) - 1).ToString();
        DataTable dtheader = Common.Execute_Procedures_Select_ByQuery(Qry);
        if (dtheader.Rows.Count > 0)
        {
            StartDate = dtheader.Rows[0]["VessStart"].ToString();
            EndDate = dtheader.Rows[0]["VessEnd"].ToString();
            Days = Common.CastAsInt32(dtheader.Rows[0]["YearDays"]);
        }
        //---------------------------------
        string VesselPart = VesselCode + " - " + VesselName;
        string CompanyPart = CompanyCode + " - " + CompanyName;
        ///http://localhost:49797/MTMPOS/Print2.aspx?BudgetForeCast=true&Comp=CID-Cido Shipping&Vessel=LIG - M.T. LIME GALAXY&BType=Operating Expenditure&StartDate=&EndDate=&year=2014&YearDays=&MajCatID=6
        Bind_BudgetForeCastRPT(CompanyPart, VesselPart, "All", StartDate, EndDate, Year, Days.ToString());

        string FileName = PublishPath + "\\" + "Vessel Budget ForeCast [ " + VesselCode + "] .pdf";
        rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, FileName);
    }
    public void Bind_BudgetForeCastRPT(string Comp, string vess, string BType, string StartDate, string EndDate, string Year, string Days)
    {
        string sql = "SELECT v_BudgetForecastYear.AccountNumber,v_BudgetForecastYear.YearDays, v_BudgetForecastYear.AccountName, ROUND(ISNULL(v_BudgetForecastYear.ForeCast, 0), 0) AS Budget, " +
                      "Forecast=isnull((select Amount from Add_v_BudgetForecastYear Addt where Addt.cocode=v_BudgetForecastYear.cocode and Addt.AcctId=v_BudgetForecastYear.AcctId and Addt.Byear=" + Year.ToString() + "),0)  , " +
                      "Comment=isnull((select YearComment from [dbo].v_BudgetForecastYear V1 where V1.CoCode='" + Comp.Substring(0, 3) + "' AND V1.Vess='" + vess.Substring(0, 3) + "' and V1.AcctId=v_BudgetForecastYear.AcctId and V1.Year=" + (Common.CastAsInt32(Year) - 1).ToString() + "),''), " +
                      "v_BudgetForecastYear.MidCatID, v_BudgetForecastYear.MinCatID, " +
                      "    (SELECT     MidCat " +
                      "      FROM          dbo.tblAccountsMid AS G2 " +
                      "      WHERE      (MidCatID = v_BudgetForecastYear.MidCatID)) AS Group1, " +
                      "    (SELECT     MinorCat " +
                      "      FROM          dbo.tblAccountsMinor AS G3 " +
                      "      WHERE      (MinCatID = v_BudgetForecastYear.MinCatID)) AS Group2, ISNULL " +
                      "    ((SELECT     Amount " +
                      "        FROM         dbo.Add_v_BudgetForecastYear AS Addt " +
                      "        WHERE     (CoCode = v_BudgetForecastYear.CoCode) AND (AcctId = v_BudgetForecastYear.AcctID) AND (BYear = " + (Common.CastAsInt32(Year) - 1).ToString() + ")), 0) AS AnnAmt,  " +
                      " Comment=isnull((select YearComment from [dbo].v_BudgetForecastYear V1 where V1.CoCode=v_BudgetForecastYear.Cocode AND V1.Vess=v_BudgetForecastYear.vess and V1.AcctId=v_BudgetForecastYear.AcctId and V1.Year=" + (Common.CastAsInt32(Year) - 1).ToString() + "),''), " +
                      "v_BudgetForecastYear.Year, v_BudgetForecastYear.ForeCastYear,MajSeqNo,MidSeqNo,MinSeqNo,v_BudgetForecastYear.accountnumber,accountid " +
                      "FROM         dbo.v_BudgetForecastYear AS v_BudgetForecastYear INNER JOIN " +
                      "    (SELECT     RESULT AS CY " +
                      "FROM dbo.CSVtoTableStr('" + (Common.CastAsInt32(Year) - 2).ToString() + "', ',') AS CSVtoTableStr_1) AS tempYear ON v_BudgetForecastYear.Year = tempYear.CY WHERE v_BudgetForecastYear.COCODE='" + Comp.Substring(0, 3) + "' AND v_BudgetForecastYear.VESS='" + vess.Substring(0, 3) + "' AND LEFT(v_BudgetForecastYear.ACCOUNTNUMBER,2)<>'17' AND v_BudgetForecastYear.ACCOUNTNUMBER<>8590 AND (v_BudgetForecastYear.ACCOUNTNUMBER < 8521 OR v_BudgetForecastYear.ACCOUNTNUMBER > 9827) AND YEAR=" + (Common.CastAsInt32(Year) - 2).ToString();

        sql = "EXEC [dbo].[fn_NEW_GETCMBUDGETACTUAL_ForeCastPrint] '" + Comp.Substring(0, 3) + "'," + DateTime.Today.Month.ToString() + "," + (Common.CastAsInt32(Year) - 1).ToString() + ",'" + vess.Substring(0, 3) + " '";
        DataTable DtRpt = Common.Execute_Procedures_Select_ByQuery(sql);

        rpt.Load(Server.MapPath("~/Report/BudgetForecastReport.rpt"));
        DtRpt.TableName = "vw_NewPR_GetCurrentYearBudgetRptStructure";
        rpt.SetDataSource(DtRpt);

        DataTable dtChild = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM vw_BudgetForeCastComments where cocode='" + Comp.Substring(0, 3) + "' and shipid='" + vess.Substring(0, 3) + "' and forecastyear=" + (Common.CastAsInt32(Year)).ToString() + " order by midseqno");
        rpt.Subreports["BudgetForecastComment.rpt"].SetDataSource(dtChild);

	DataTable DtBudgetTracking = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM tbl_BudgetTracking where Company='" + Comp.Substring(0, 3) + "' and VesselCode='" + vess.Substring(0, 3) + "' and budgetYear=" + (Common.CastAsInt32(Year)).ToString() + "");
        rpt.Subreports["BudgetTaskList"].SetDataSource(DtBudgetTracking);

        //----------------------------------------------
        string Qry = "select FORECAST from dbo.tblsmdbudgetforecastyear where cocode='" + Comp.Substring(0, 3) + "' AND YEAR=" + (DateTime.Today.Year - 1).ToString() + " AND ACCOUNTNUMBER=5100 AND SHIPID='" + vess.Substring(0, 3) + "'";
        DataTable dtheader = Common.Execute_Procedures_Select_ByQuery(Qry);
        int Amt_5100 = 0;
        if (dtheader.Rows.Count > 0)
        {
            Amt_5100 = Common.CastAsInt32(dtheader.Rows[0][0]);
            if (Amt_5100 > 0)
                Amt_5100 = ProjectCommon.Get_ManningAmount(Comp.Substring(0, 3), vess.Substring(0, 3), DateTime.Today.Year);
        }
        //-------------------

        DataTable dtAccts = Common.Execute_Procedures_Select_ByQuery("select distinct midcatid,midcat,MajSeqNo,MidSeqNo from dbo.v_New_CurrYearBudgetHome WHERE MajCatId=6 ORDER BY MajSeqNo,MidSeqNo");
        DataTable dtAccts1 = Common.Execute_Procedures_Select_ByQuery("select distinct midcatid,midcat,MajSeqNo,MidSeqNo from dbo.v_New_CurrYearBudgetHome WHERE MajCatId<>6 And MidcatId not in (25,26) ORDER BY MajSeqNo,MidSeqNo");
        DataTable dtDays = Common.Execute_Procedures_Select_ByQuery("select top 1 Days From dbo.v_New_CurrYearBudgetHome Where shipid='" + vess.Substring(0, 3) + "' AND year=" + (DateTime.Today.Year).ToString() + " order by days desc");
        DataTable dtDaysLast = Common.Execute_Procedures_Select_ByQuery("select top 1 Days From dbo.v_New_CurrYearBudgetHome Where shipid='" + vess.Substring(0, 3) + "' AND year=" + (DateTime.Today.Year - 1).ToString() + " order by days desc");
        DataTable dtActCom_Proj = Common.Execute_Procedures_Select_ByQuery("EXEC [dbo].[fn_NEW_GETCMBUDGETACTUAL_MIDCATWISE_FORDATE] '" + Comp.Substring(0, 3) + "'," + (DateTime.Today.Month).ToString() + "," + (DateTime.Today.Year).ToString() + "," + (DateTime.Today.Day).ToString() + ",'" + vess.Substring(0, 3) + "'");
        DataView dv = dtActCom_Proj.DefaultView;
        int DaysCnt = 1;
        if (dtDays != null)
        {
            if (dtDays.Rows.Count > 0)
            {
                DaysCnt = Common.CastAsInt32(dtDays.Rows[0][0]);
            }
        }
        int DaysCntLast = 1;
        if (dtDaysLast != null)
        {
            if (dtDaysLast.Rows.Count > 0)
            {
                DaysCntLast = Common.CastAsInt32(dtDaysLast.Rows[0][0]);
            }
        }
        int start = 2;

        decimal ColumnSum = 0, ColumnSumLast = 0;
        decimal ColumnSum2 = 0, ColumnSum3 = 0;

        rpt.SetParameterValue("Param1", "Budget " + (Common.CastAsInt32(Year) - 1).ToString() + " ", "BudgetForecastReport_Summary.rpt");
        rpt.SetParameterValue("Param101", "Budget " + Year + " ", "BudgetForecastReport_Summary.rpt"); // forecast

        rpt.SetParameterValue("Param201", "Act. & Comm " + (Common.CastAsInt32(Year) - 1).ToString() + " ", "BudgetForecastReport_Summary.rpt");
        rpt.SetParameterValue("Param301", "Projected " + (Common.CastAsInt32(Year) - 1).ToString() + " ", "BudgetForecastReport_Summary.rpt");

        rpt.SetParameterValue("Param401", (Common.CastAsInt32(Year)).ToString() + "-Budget Var.% ", "BudgetForecastReport_Summary.rpt");

        rpt.SetParameterValue("Param501", (Common.CastAsInt32(Year) - 1).ToString() + "-Var.%", "BudgetForecastReport_Summary.rpt");
        rpt.SetParameterValue("Param601", "", "BudgetForecastReport_Summary.rpt");

        //----------------------------------------------
        for (int i = 0; i <= dtAccts.Rows.Count - 1; i++)
        {
            int RowSum = 0;
            dv.RowFilter = "MidCatId=" + dtAccts.Rows[i][0].ToString();
            DataTable dt1 = dv.ToTable();
            decimal ActComm = 0, Projected = 0, Budget = 0, ForeCast = 0;

            if (i != 0) { Amt_5100 = 0; }

            if (Amt_5100 > 0)
            {
                ColumnSum3 += Amt_5100;
                ActComm = 0;
                Projected = Amt_5100;

                // OLD LINE
                //rpt.SetParameterValue("Param20" + start.ToString(), "0", "BudgetForecastReport_Summary.rpt");
                // NEW LINE
                rpt.SetParameterValue("Param20" + start.ToString(), FormatCurrency(dt1.Rows[0]["ACT_CONS"]), "BudgetForecastReport_Summary.rpt");
                ActComm = Common.CastAsInt32(dt1.Rows[0]["ACT_CONS"]);
                ColumnSum2 += ActComm;
                // END

                rpt.SetParameterValue("Param30" + start.ToString(), Amt_5100.ToString(), "BudgetForecastReport_Summary.rpt");
            }
            else
            {
                if (dt1.Rows.Count > 0)
                {
                    rpt.SetParameterValue("Param20" + start.ToString(), FormatCurrency(dt1.Rows[0]["ACT_CONS"]), "BudgetForecastReport_Summary.rpt");
                    rpt.SetParameterValue("Param30" + start.ToString(), FormatCurrency(dt1.Rows[0]["PROJECTED"]), "BudgetForecastReport_Summary.rpt");
                    ColumnSum2 += Common.CastAsInt32(dt1.Rows[0]["ACT_CONS"]);
                    ColumnSum3 += Common.CastAsInt32(dt1.Rows[0]["PROJECTED"]);

                    ActComm = Common.CastAsDecimal(dt1.Rows[0]["ACT_CONS"]);
                    Projected = Common.CastAsDecimal(dt1.Rows[0]["PROJECTED"]);
                }
                else
                {
                    rpt.SetParameterValue("Param20" + start.ToString(), "0", "BudgetForecastReport_Summary.rpt");
                    rpt.SetParameterValue("Param30" + start.ToString(), "0", "BudgetForecastReport_Summary.rpt");
                }
            }
            DataTable dtShip = Common.Execute_Procedures_Select_ByQuery("select budget from dbo.v_New_CurrYearBudgetHome where shipid='" + vess.Substring(0, 3) + "' AND year=" + (DateTime.Today.Year - 1).ToString() + " and  midcatid=" + dtAccts.Rows[i][0].ToString());
            if (dtShip != null)
            {
                if (dtShip.Rows.Count > 0)
                {
                    ColumnSum += Common.CastAsInt32(dtShip.Rows[0][0]);
                    rpt.SetParameterValue("Param" + start.ToString(), FormatCurrency(dtShip.Rows[0][0]), "BudgetForecastReport_Summary.rpt");
                    Budget = Common.CastAsDecimal(dtShip.Rows[0][0]);
                }
                else
                {
                    rpt.SetParameterValue("Param" + start.ToString(), "0", "BudgetForecastReport_Summary.rpt");
                }
            }
            else
            {
                rpt.SetParameterValue("Param" + start.ToString(), "0", "BudgetForecastReport_Summary.rpt");
            }

            DataTable dtShipLast = Common.Execute_Procedures_Select_ByQuery("select nextyearforecastamount from dbo.v_New_CurrYearBudgetHome where shipid='" + vess.Substring(0, 3) + "' AND year=" + (DateTime.Today.Year - 1).ToString() + " and  midcatid=" + dtAccts.Rows[i][0].ToString());
            if (dtShipLast != null)
            {
                if (dtShipLast.Rows.Count > 0)
                {
                    ColumnSumLast += Common.CastAsInt32(dtShipLast.Rows[0][0]);
                    rpt.SetParameterValue("Param10" + start.ToString(), FormatCurrency(dtShipLast.Rows[0][0]), "BudgetForecastReport_Summary.rpt");
                    ForeCast = Common.CastAsDecimal(dtShipLast.Rows[0][0]);
                }
                else
                {
                    rpt.SetParameterValue("Param10" + start.ToString(), "0", "BudgetForecastReport_Summary.rpt");
                }
            }
            else
            {
                rpt.SetParameterValue("Param10" + start.ToString(), "0", "BudgetForecastReport_Summary.rpt");
            }

            if (Budget > 0)
                ActComm = ((Projected - Budget) / Budget) * 100;
            else
                ActComm = 0;

            if (Projected > 0)
                Projected = ((ForeCast - Projected) / Projected) * 100;
            else
                Projected = 0;

            if (Budget > 0)
            {
                // ----- NEW LINE
                //Budget = (Budget / DaysCntLast) * DaysCnt;
                decimal dddddd1 = (Budget / DaysCntLast) * DaysCnt;
                Budget = ((ForeCast - dddddd1) / dddddd1) * 100;
                // ----- NEW LINE
                //Budget = ((ForeCast - Budget) / Budget) * 100;
            }
            else
                Budget = 0;

            rpt.SetParameterValue("Param40" + start.ToString(), Math.Round(ActComm, 0) + "%", "BudgetForecastReport_Summary.rpt");
            rpt.SetParameterValue("Param50" + start.ToString(), Math.Round(Projected, 0) + "%", "BudgetForecastReport_Summary.rpt");
            rpt.SetParameterValue("Param60" + start.ToString(), Math.Round(Budget, 0) + "%", "BudgetForecastReport_Summary.rpt");

            start++;
        }
        //----------------------------------------------
        rpt.SetParameterValue("Param" + start.ToString(), FormatCurrency(ColumnSum), "BudgetForecastReport_Summary.rpt");
        rpt.SetParameterValue("Param10" + start.ToString(), FormatCurrency(ColumnSumLast), "BudgetForecastReport_Summary.rpt");
        rpt.SetParameterValue("Param20" + start.ToString(), FormatCurrency(ColumnSum2), "BudgetForecastReport_Summary.rpt");
        rpt.SetParameterValue("Param30" + start.ToString(), FormatCurrency(ColumnSum3), "BudgetForecastReport_Summary.rpt");


        rpt.SetParameterValue("Param40" + start.ToString(), Math.Round(((ColumnSum3 - ColumnSum) / ((ColumnSum == 0) ? 1 : ColumnSum)) * 100, 0) + "%", "BudgetForecastReport_Summary.rpt");
        rpt.SetParameterValue("Param50" + start.ToString(), Math.Round(((ColumnSumLast - ColumnSum3) / ((ColumnSum3 == 0) ? 1 : ColumnSum3)) * 100, 0) + "%", "BudgetForecastReport_Summary.rpt");
        // ----- NEW LINE
        Decimal dddd1 = (ColumnSum / DaysCntLast) * DaysCnt;
        // ----- NEW LINE
        rpt.SetParameterValue("Param60" + start.ToString(), Math.Round(((ColumnSumLast - dddd1) / ((dddd1 == 0) ? 1 : dddd1)) * 100, 0) + "%", "BudgetForecastReport_Summary.rpt");
        //rpt.SetParameterValue("Param60" + start.ToString(), Math.Round(((ColumnSumLast - ColumnSum) / ((ColumnSum == 0) ? 1 : ColumnSum)) * 100, 0) + "%", "BudgetForecastReport_Summary.rpt");

        start++;

        int DaysTillyearEnd = new DateTime(Common.CastAsInt32(Year) - 1, 12, 31).Subtract(DateTime.Today).Days;
        int DaysTilltoday = DaysCntLast - DaysTillyearEnd;
        if (DaysTilltoday > 365)
            DaysTilltoday = 365;

        rpt.SetParameterValue("Param" + start.ToString(), FormatCurrency(Math.Round(ColumnSum / DaysCntLast, 0)), "BudgetForecastReport_Summary.rpt");
        rpt.SetParameterValue("Param10" + start.ToString(), FormatCurrency(Math.Round(ColumnSumLast / DaysCnt, 0)), "BudgetForecastReport_Summary.rpt");
        rpt.SetParameterValue("Param20" + start.ToString(), FormatCurrency(Math.Round(ColumnSum2 / DaysTilltoday, 0)), "BudgetForecastReport_Summary.rpt");
        rpt.SetParameterValue("Param30" + start.ToString(), FormatCurrency(Math.Round(ColumnSum3 / DaysCntLast, 0)), "BudgetForecastReport_Summary.rpt");

        rpt.SetParameterValue("Param40" + start.ToString(), "", "BudgetForecastReport_Summary.rpt");
        rpt.SetParameterValue("Param50" + start.ToString(), "", "BudgetForecastReport_Summary.rpt");
        rpt.SetParameterValue("Param60" + start.ToString(), "", "BudgetForecastReport_Summary.rpt");
        start++;
        //----------------------------------------------

        for (int i = 0; i <= dtAccts1.Rows.Count - 1; i++)
        {
            dv.RowFilter = "MidCatId=" + dtAccts1.Rows[i][0].ToString();
            decimal ActComm = 0, Projected = 0, Budget = 0, ForeCast = 0;
            DataTable dt1 = dv.ToTable();
            if (dt1.Rows.Count > 0)
            {
                rpt.SetParameterValue("Param20" + start.ToString(), FormatCurrency(dt1.Rows[0]["ACT_CONS"]), "BudgetForecastReport_Summary.rpt");
                rpt.SetParameterValue("Param30" + start.ToString(), FormatCurrency(dt1.Rows[0]["PROJECTED"]), "BudgetForecastReport_Summary.rpt");
                ColumnSum2 += Common.CastAsInt32(dt1.Rows[0]["ACT_CONS"]);
                ColumnSum3 += Common.CastAsInt32(dt1.Rows[0]["PROJECTED"]);

                ActComm = Common.CastAsDecimal(dt1.Rows[0]["ACT_CONS"]);
                Projected = Common.CastAsDecimal(dt1.Rows[0]["PROJECTED"]);
            }
            else
            {
                rpt.SetParameterValue("Param20" + start.ToString(), "0", "BudgetForecastReport_Summary.rpt");
                rpt.SetParameterValue("Param30" + start.ToString(), "0", "BudgetForecastReport_Summary.rpt");
            }

            DataTable dtShip = Common.Execute_Procedures_Select_ByQuery("select budget from dbo.v_New_CurrYearBudgetHome where shipid='" + vess.Substring(0, 3) + "' AND year=" + (DateTime.Today.Year - 1).ToString() + " and  midcatid=" + dtAccts1.Rows[i][0].ToString());
            if (dtShip != null)
            {
                if (dtShip.Rows.Count > 0)
                {
                    ColumnSum += Common.CastAsInt32(dtShip.Rows[0][0]);
                    rpt.SetParameterValue("Param" + start.ToString(), FormatCurrency(dtShip.Rows[0][0]), "BudgetForecastReport_Summary.rpt");
                    Budget = Common.CastAsDecimal(dtShip.Rows[0][0]);
                }
                else
                {
                    rpt.SetParameterValue("Param" + start.ToString(), "0", "BudgetForecastReport_Summary.rpt");
                }
            }
            else
            {
                rpt.SetParameterValue("Param" + start.ToString(), "0", "BudgetForecastReport_Summary.rpt");
            }

            DataTable dtShipLast = Common.Execute_Procedures_Select_ByQuery("select nextyearforecastamount from dbo.v_New_CurrYearBudgetHome where shipid='" + vess.Substring(0, 3) + "' AND year=" + (DateTime.Today.Year - 1).ToString() + " and  midcatid=" + dtAccts1.Rows[i][0].ToString());
            if (dtShipLast != null)
            {
                if (dtShipLast.Rows.Count > 0)
                {
                    ColumnSumLast += Common.CastAsInt32(dtShipLast.Rows[0][0]);
                    rpt.SetParameterValue("Param10" + start.ToString(), FormatCurrency(dtShipLast.Rows[0][0]), "BudgetForecastReport_Summary.rpt");
                    ForeCast = Common.CastAsDecimal(dtShipLast.Rows[0][0]);
                }
                else
                {
                    rpt.SetParameterValue("Param10" + start.ToString(), "0", "BudgetForecastReport_Summary.rpt");
                }
            }
            else
            {
                rpt.SetParameterValue("Param10" + start.ToString(), "0", "BudgetForecastReport_Summary.rpt");
            }

            if (Budget > 0)
                ActComm = ((Projected - Budget) / Budget) * 100;
            else
                ActComm = 0;

            if (Projected > 0)
                Projected = ((ForeCast - Projected) / Projected) * 100;
            else
                Projected = 0;

            if (Budget > 0)
            {
                // ----- NEW LINE
                decimal dddddd2 = (Budget / DaysCntLast) * DaysCnt;
                Budget = ((ForeCast - dddddd2) / dddddd2) * 100;
                // ----- NEW LINE
                //Budget = ((ForeCast - Budget) / Budget) * 100;
            }
            else
                Budget = 0;

            rpt.SetParameterValue("Param40" + start.ToString(), Math.Round(ActComm, 0) + "%", "BudgetForecastReport_Summary.rpt");
            rpt.SetParameterValue("Param50" + start.ToString(), Math.Round(Projected, 0) + "%", "BudgetForecastReport_Summary.rpt");
            rpt.SetParameterValue("Param60" + start.ToString(), Math.Round(Budget, 0) + "%", "BudgetForecastReport_Summary.rpt");

            start++;
        }
        rpt.SetParameterValue("Param" + start.ToString(), FormatCurrency(Math.Round(ColumnSum, 0)), "BudgetForecastReport_Summary.rpt");
        rpt.SetParameterValue("Param10" + start.ToString(), FormatCurrency(ColumnSumLast), "BudgetForecastReport_Summary.rpt");
        rpt.SetParameterValue("Param20" + start.ToString(), FormatCurrency(ColumnSum2), "BudgetForecastReport_Summary.rpt");
        rpt.SetParameterValue("Param30" + start.ToString(), FormatCurrency(ColumnSum3), "BudgetForecastReport_Summary.rpt");

        rpt.SetParameterValue("Param40" + start.ToString(), Math.Round(((ColumnSum3 - ColumnSum) / ((ColumnSum == 0) ? 1 : ColumnSum)) * 100, 0) + "%", "BudgetForecastReport_Summary.rpt");
        rpt.SetParameterValue("Param50" + start.ToString(), Math.Round(((ColumnSumLast - ColumnSum3) / ((ColumnSum3 == 0) ? 1 : ColumnSum3)) * 100, 0) + "%", "BudgetForecastReport_Summary.rpt");
        // ----- NEW LINE
        decimal dddd2 = (ColumnSum / DaysCntLast) * DaysCnt;
        // ----- NEW LINE
        rpt.SetParameterValue("Param60" + start.ToString(), Math.Round(((ColumnSumLast - dddd2) / ((dddd2 == 0) ? 1 : dddd2)) * 100, 0) + "%", "BudgetForecastReport_Summary.rpt");
        //rpt.SetParameterValue("Param60" + start.ToString(), Math.Round(((ColumnSumLast - ColumnSum) / ((ColumnSum == 0) ? 1 : ColumnSum)) * 100, 0) + "%", "BudgetForecastReport_Summary.rpt");

        rpt.SetParameterValue("Param6016", (Common.CastAsInt32(Year) - 1).ToString() + "[B]", "BudgetForecastReport_Summary.rpt");
        rpt.SetParameterValue("Param6017", (Common.CastAsInt32(Year) - 1).ToString() + "[P]", "BudgetForecastReport_Summary.rpt");

        rpt.SetParameterValue("Byear", "[" + (Common.CastAsInt32(DaysCntLast)).ToString() + "]", "BudgetForecastReport_Summary.rpt");
        rpt.SetParameterValue("Fyear", "[" + (Common.CastAsInt32(DaysCnt)).ToString() + "]", "BudgetForecastReport_Summary.rpt");
        //----------------------------------------------

        rpt.SetParameterValue("Company", Comp);
        rpt.SetParameterValue("Vessel", vess);
        rpt.SetParameterValue("BudgetType", BType);
        rpt.SetParameterValue("Start Date", StartDate);
        rpt.SetParameterValue("End Date", EndDate);
        rpt.SetParameterValue("Year", Year);
        rpt.SetParameterValue("Days", "[" + Days + "]");
        rpt.SetParameterValue("LastDays", "[" + DtRpt.Rows[0]["YearDays"].ToString() + "]");

        DataTable dt_flag = Common.Execute_Procedures_Select_ByQueryCMS("select yearbuilt,(select flagstatename from flagstate where flagstate.flagstateid=vessel.flagstateid) as Flag from vessel where vessel.vesselcode='" + vess.Substring(0, 3) + "'");
        if (dt_flag != null)
        {
            if (dt_flag.Rows.Count > 0)
            {
                rpt.SetParameterValue("YearBuilt", dt_flag.Rows[0]["yearbuilt"].ToString());
                rpt.SetParameterValue("Flag", dt_flag.Rows[0]["Flag"].ToString());
            }
            else
            {
                rpt.SetParameterValue("YearBuilt", "");
                rpt.SetParameterValue("Flag", "");
            }
        }
        else
        {
            rpt.SetParameterValue("YearBuilt", "");
            rpt.SetParameterValue("Flag", "");
        }

    }

    //--------------- EXPORT COMPANY SUMMARY BY VESSEL ---------------------------------
    protected void Export_CompanySummary_ByVessel(string Year,string CompanyCode, string CompanyName)
    {
        //------------------------------
        DateTime FromDate = new DateTime((DateTime.Today.Year + 1), 1, 1);
        DateTime ToDate = new DateTime((DateTime.Today.Year + 1), 12, 31);
        string Days = "[" + (ToDate.Subtract(FromDate).Days + 1).ToString() + "] days";
        //---------------------------------
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select SHIPID from [dbo].[VW_sql_tblSMDPRVessels] where company='" + CompanyCode + "' AND VESSELNO>0  AND ACTIVE='A'");
        string VesselList = "";
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                VesselList +=","+ dr["SHIPID"].ToString();
            }
            if(VesselList.StartsWith(","))
                VesselList=VesselList.Substring(1);

            DataTable dts = BindFleetView(VesselList);
            ExportToPDF("Fleet Summary By Vessel\n" + CompanyName + " ( Budget - " + Year + " )\n" + Days, dts);
        }
        //---------------------------------
    }
    protected DataTable BindFleetView(string VesselList)
    {
        DataTable dt_Result = new DataTable();
        string[] VesselArray;
        char[] sep = { ',' };
        VesselArray = VesselList.Split(sep);

        int[] ColumnSum;
        int[] VesselDays;

        ColumnSum = new int[VesselArray.Length];
        VesselDays = new int[VesselArray.Length];

        StringBuilder sb = new StringBuilder();
        DataTable dtAccts = Common.Execute_Procedures_Select_ByQuery("select distinct midcatid,midcat,MajSeqNo,MidSeqNo from dbo.v_New_CurrYearBudgetHome WHERE MajCatId=6 ORDER BY MajSeqNo,MidSeqNo");
        DataTable dtAccts1 = Common.Execute_Procedures_Select_ByQuery("select distinct midcatid,midcat,MajSeqNo,MidSeqNo from dbo.v_New_CurrYearBudgetHome WHERE MajCatId<>6 ORDER BY MajSeqNo,MidSeqNo");
        sb.Append("<table border='1' width='100%' cellpadding='3' height='390' style='font-size:10px;'");
        sb.Append("<tr>");
        sb.Append("<td class='header' style='font-size:10px;'>Budget Head<br/>Year Built</td>");
        dt_Result.Columns.Add("BudgetHead");
        string Ships = "";
        for (int i = 0; i < VesselArray.Length; i++)
        {
            Ships = Ships + ((Ships.Trim() == "") ? "" : ",") + VesselArray[i];
            //if (i != 0)
            //{
            sb.Append("<td class='header' style='font-size:10px;'><input type='radio' name='radVSL' value='" + VesselArray[i] + "' id='rad" + VesselArray[i] + "'><label for='rad" + VesselArray[i] + "'>" + VesselArray[i] + "</label>");
            DataTable dtDays = Common.Execute_Procedures_Select_ByQuery("select top 1 Days From dbo.v_New_CurrYearBudgetHome Where shipid='" + VesselArray[i] + "' AND year=" + (DateTime.Today.Year).ToString() + " order by days desc");
            if (dtDays != null)
            {
                if (dtDays.Rows.Count > 0)
                {
                    VesselDays[i] = Common.CastAsInt32(dtDays.Rows[0][0]);
                }
            }

            DataTable dtYB = Common.Execute_Procedures_Select_ByQueryCMS("select yearbuilt from dbo.vessel Where vesselcode='" + VesselArray[i] + "'");
            if (dtYB != null)
            {
                if (dtYB.Rows.Count > 0)
                {
                    if (dtYB.Rows[0][0].ToString().Trim() != "")
                    {
                        sb.Append("</br> [" + dtYB.Rows[0][0].ToString() + "]");
                    }
                }
            }

            sb.Append("</td>");
            dt_Result.Columns.Add(VesselArray[i]);
            //}
        }
        //hfd_Ships.Value = Ships;
        sb.Append("<td class='header' style='font-size:10px;'>Total</td>");
        dt_Result.Columns.Add("Total");
        sb.Append("</tr>");
        // YEAR BUILT
        //-----------------
        DataRow dr_yb = dt_Result.NewRow();
        dr_yb[0] = "Year Built";
        for (int j = 0; j < VesselArray.Length; j++)
        {
            //if (j != 0)
            //{
            DataTable dt_yb = Common.Execute_Procedures_Select_ByQueryCMS("select yearbuilt from vessel where vesselcode='" + VesselArray[j] + "'");
            if (dt_yb.Rows.Count > 0)
            {
                dr_yb[VesselArray[j]] = "[ " + dt_yb.Rows[0][0].ToString() + " ]";
            }
            //}
        }
        dr_yb[dt_Result.Columns.Count - 1] = "";
        dt_Result.Rows.Add(dr_yb);
        //-----------------
        // DATA ROWS 
        for (int i = 0; i <= dtAccts.Rows.Count - 1; i++)
        {
            int RowSum = 0;
            DataRow dr = dt_Result.NewRow();
            sb.Append("<tr>");
            sb.Append("<td>" + dtAccts.Rows[i][1].ToString() + "</td>");
            dr[0] = dtAccts.Rows[i][1].ToString();
            for (int j = 0; j < VesselArray.Length; j++)
            {
                //if (j != 0)
                //{
                DataTable dtShip = Common.Execute_Procedures_Select_ByQuery("select NEXTYEARFORECASTAMOUNT from dbo.v_New_CurrYearBudgetHome where shipid='" + VesselArray[j] + "' AND year=" + (DateTime.Today.Year - 1).ToString() + " and  midcatid=" + dtAccts.Rows[i][0].ToString());
                if (dtShip != null)
                {
                    if (dtShip.Rows.Count > 0)
                    {
                        sb.Append("<td style='text-align:right'>" + FormatCurrency(dtShip.Rows[0][0]) + "</td>");
                        ColumnSum[j] += Common.CastAsInt32(dtShip.Rows[0][0]);
                        RowSum += Common.CastAsInt32(dtShip.Rows[0][0]);
                        dr[VesselArray[j]] = FormatCurrency(dtShip.Rows[0][0]);
                    }
                    else
                    {
                        sb.Append("<td></td>");
                        dr[VesselArray[j]] = "0";
                    }
                }
                else
                {
                    sb.Append("<td></td>");
                    dr[VesselArray[j]] = "0";
                }
                //}
            }
            sb.Append("<td style='text-align:right'>" + FormatCurrency(RowSum) + "</td>");
            dr[dt_Result.Columns.Count - 1] = FormatCurrency(RowSum);
            sb.Append("</tr>");
            dt_Result.Rows.Add(dr);
        }
        //---------------
        // TOTAL
        int VSLhaving_TotalMoreThan0 = 0;
        DataRow dr1 = dt_Result.NewRow();
        sb.Append("<tr class='header' style='background-color :#C2C2C2;color:Black'>");
        sb.Append("<td style='font-size:10px;text-align:right;'>Total(US$)</td>");
        dr1[0] = "Total(US$)";
        int GrossSum = 0;
        for (int i = 0; i < VesselArray.Length; i++)
        {
            //if (i != 0)
            //{
            sb.Append("<td style='font-size:10px;text-align:right'>" + FormatCurrency(ColumnSum[i]) + "</td>");
            GrossSum += ColumnSum[i];
            dr1[i + 1] = FormatCurrency(ColumnSum[i]); //====================================================
            if (ColumnSum[i] > 0)
            {
                VSLhaving_TotalMoreThan0++;
            }
            //}
        }
        sb.Append("<td style='font-size:10px;text-align:right'>" + FormatCurrency(GrossSum) + "</td>");
        dr1[dt_Result.Columns.Count - 1] = FormatCurrency(GrossSum);
        sb.Append("</tr>");
        dt_Result.Rows.Add(dr1);

        // PER DAY CALC
        DataRow dr2 = dt_Result.NewRow();
        sb.Append("<tr class='header' style='background-color :#C2C2C2;color:Black'>");
        sb.Append("<td style='font-size:10px;text-align:right;'>Avg Daily Cost(US$)</td>");
        dr2[0] = "Avg Daily Cost(US$)";
        //int GrossSum = 0;
        for (int i = 0; i < VesselArray.Length; i++)
        {
            //if (i != 0)
            //{
            try
            {
                sb.Append("<td style='font-size:10px;text-align:right'>" + FormatCurrency((ColumnSum[i] / VesselDays[i])) + "</td>");
                dr2[i + 1] = FormatCurrency((ColumnSum[i] / VesselDays[i])); //=================================================
            }
            catch (DivideByZeroException ex)
            {
                sb.Append("<td style='font-size:10px;text-align:right'>0</td>");
                dr2[i + 1] = "0";//=================================================
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //GrossSum += ColumnSum[i - 1];
            //}
        }
        if (VSLhaving_TotalMoreThan0 > 0)
        {
            sb.Append("<td style='font-size:10px;text-align:right'>" + FormatCurrency((GrossSum / 366) / VSLhaving_TotalMoreThan0) + "</td>");
            dr2[dt_Result.Columns.Count - 1] = FormatCurrency((GrossSum / 366) / VSLhaving_TotalMoreThan0);
        }
        else
        {
            sb.Append("<td style='font-size:10px;text-align:right'>" + FormatCurrency((GrossSum / 366)) + "</td>");
            dr2[dt_Result.Columns.Count - 1] = FormatCurrency((GrossSum / 366));
        }
        sb.Append("</tr>");
        dt_Result.Rows.Add(dr2);

        // DATA ROWS  - 2
        for (int i = 0; i <= dtAccts1.Rows.Count - 1; i++)
        {
            int RowSum = 0;
            DataRow dr = dt_Result.NewRow();
            sb.Append("<tr>");
            sb.Append("<td>" + dtAccts1.Rows[i][1].ToString() + "</td>");
            dr[0] = dtAccts1.Rows[i][1].ToString();
            for (int j = 0; j < VesselArray.Length; j++)
            {
                //if (j != 0)
                //{
                DataTable dtShip = Common.Execute_Procedures_Select_ByQuery("select NEXTYEARFORECASTAMOUNT from dbo.v_New_CurrYearBudgetHome where shipid='" + VesselArray[j] + "' AND year=" + (DateTime.Today.Year - 1).ToString() + " and  midcatid=" + dtAccts1.Rows[i][0].ToString());
                if (dtShip != null)
                {
                    if (dtShip.Rows.Count > 0)
                    {
                        sb.Append("<td style='text-align:right'>" + FormatCurrency(dtShip.Rows[0][0]) + "</td>");
                        ColumnSum[j] += Common.CastAsInt32(dtShip.Rows[0][0]);
                        RowSum += Common.CastAsInt32(dtShip.Rows[0][0]);
                        dr[VesselArray[j]] = FormatCurrency(dtShip.Rows[0][0]);
                    }
                    else
                    {
                        sb.Append("<td></td>");
                        dr[VesselArray[j]] = "0";
                    }
                }
                else
                {
                    sb.Append("<td></td>");
                    dr[VesselArray[j]] = "0";
                }
                //}
            }
            sb.Append("<td style='text-align:right'>" + FormatCurrency(RowSum) + "</td>");
            sb.Append("</tr>");
            dr[dt_Result.Columns.Count - 1] = FormatCurrency(RowSum);
            dt_Result.Rows.Add(dr);
        }

        // GROSS TOTAL
        DataRow dr3 = dt_Result.NewRow();
        sb.Append("<tr class='header' style='background-color :#C2C2C2;color:Black'>");
        sb.Append("<td style='font-size:10px;text-align:right;'>Gross Total(US$)</td>");
        dr3[0] = "Gross Total(US$)";
        GrossSum = 0;
        for (int i = 0; i < VesselArray.Length; i++)
        {
            //if (i != 0)
            //{
            sb.Append("<td style='font-size:10px;text-align:right'>" + FormatCurrency(ColumnSum[i]) + "</td>");
            GrossSum += ColumnSum[i];
            dr3[i + 1] = FormatCurrency(ColumnSum[i]);//=================================================
            //}
        }
        sb.Append("<td style='font-size:10px;text-align:right'>" + FormatCurrency(GrossSum) + "</td>");
        dr3[dt_Result.Columns.Count - 1] = FormatCurrency(GrossSum);
        sb.Append("</tr>");
        dt_Result.Rows.Add(dr3);
        sb.Append("</table>");
        //--------------
        return dt_Result;
    }
    private void ExportToPDF(string Company, DataTable dt)
    {
        try
        {
            //Document document = new Document(PageSize.LETTER.Rotate, 10, 10, 30, 10);
            Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 10, 10, 30, 10);

            System.IO.MemoryStream msReport = new System.IO.MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, msReport);
            document.AddAuthor("MTMSM");
            document.AddSubject("Fleet Summary");
            //'Adding Header in Document
            iTextSharp.text.Image logoImg = default(iTextSharp.text.Image);
            logoImg = iTextSharp.text.Image.GetInstance(Server.MapPath("~\\Images\\MTMMLogo.jpg"));
            Chunk chk = new Chunk(logoImg, 0, 0, true);
            //Phrase p1 = new Phrase();
            //p1.Add(chk);

            iTextSharp.text.Table tb_header = new iTextSharp.text.Table(1);
            tb_header.Width = 100;
            tb_header.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tb_header.DefaultCellBorder = iTextSharp.text.Rectangle.NO_BORDER;
            tb_header.DefaultHorizontalAlignment = Element.ALIGN_LEFT;
            tb_header.BorderWidth = 0;
            tb_header.BorderColor = iTextSharp.text.Color.WHITE;
            tb_header.Cellspacing = 1;
            tb_header.Cellpadding = 1;

            Cell c1 = new Cell(chk);
            c1.HorizontalAlignment = Element.ALIGN_LEFT;
            tb_header.AddCell(c1);

            Phrase p2 = new Phrase();
            p2.Add(new Phrase(Company + "\n" + "\n", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            Cell c2 = new Cell(p2);
            c2.HorizontalAlignment = Element.ALIGN_CENTER;
            tb_header.AddCell(c2);

            //Chunk ch = new Chunk();

            HeaderFooter header = new HeaderFooter(new Phrase(""), false);
            document.Header = header;

            //header.Alignment = Element.ALIGN_LEFT;
            header.Border = iTextSharp.text.Rectangle.NO_BORDER;
            //'Adding Footer in document
            string foot_Txt = "Published On : " + DateTime.Today.ToString("dd-MMM-yyyy");
            foot_Txt = foot_Txt + "                                                                                                                ";
            foot_Txt = foot_Txt + "                                                                                                                ";
            foot_Txt = foot_Txt + "";
            HeaderFooter footer = new HeaderFooter(new Phrase(foot_Txt, FontFactory.GetFont("VERDANA", 6, iTextSharp.text.Color.DARK_GRAY)), true);
            footer.Border = iTextSharp.text.Rectangle.NO_BORDER;
            footer.Alignment = Element.ALIGN_LEFT;
            document.Footer = footer;
            //'-----------------------------------
            document.Open();
            document.Add(tb_header);
            //------------ TABLE HEADER FONT 
            iTextSharp.text.Font fCapText = FontFactory.GetFont("ARIAL", 9, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font fCapText_5 = FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.NORMAL);
            //------------ TABLE HEADER ROW 
            int ColumnsCount = dt.Columns.Count;
            iTextSharp.text.Table tb1 = new iTextSharp.text.Table(ColumnsCount);
            tb1.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            tb1.Width = 100;

            float[] ws = new float[ColumnsCount];
            ws[0] = 15;
            for (int i = 1; i <= ws.Length - 1; i++)
                ws[i] = 80 / (ws.Length - 1);

            ws[ws.Length - 1] = ws[ws.Length - 1] + 5;
            tb1.Widths = ws;

            tb1.Alignment = Element.ALIGN_CENTER;
            tb1.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tb1.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
            tb1.BorderColor = iTextSharp.text.Color.WHITE;
            tb1.Cellspacing = 1;
            tb1.Cellpadding = 1;

            for (int i = 0; i <= dt.Columns.Count - 1; i++)
            {
                Cell tc = new Cell(new Phrase(dt.Columns[i].ColumnName, fCapText));
                tb1.AddCell(tc);
            }

            DataRow dr_yb = dt.Rows[0];
            for (int i = 0; i <= dt.Columns.Count - 1; i++)
            {
                Cell tc = new Cell(new Phrase(dr_yb[i].ToString(), fCapText_5));
                tb1.AddCell(tc);
            }

            document.Add(tb1);
            //------------ TABLE DATA ROW 
            // data rows
            iTextSharp.text.Table tbdata = new iTextSharp.text.Table(ColumnsCount);
            tbdata.Width = 100;
            tbdata.Widths = ws;
            tbdata.Alignment = Element.ALIGN_CENTER;
            tbdata.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tbdata.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
            tbdata.BorderColor = iTextSharp.text.Color.GRAY;
            tbdata.Cellspacing = 1;
            tbdata.Cellpadding = 1;
            for (int k = 1; k < dt.Rows.Count - 1; k++)
            {
                DataRow dr = dt.Rows[k];
                for (int i = 0; i <= dt.Columns.Count - 1; i++)
                {
                    Cell tc = new Cell(new Phrase(dr[i].ToString(), fCapText_5));
                    if (k == 8 || k == 9)
                    {
                        tc.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
                    }
                    if (i == 0)
                        tc.HorizontalAlignment = Element.ALIGN_LEFT;
                    else
                        tc.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tbdata.AddCell(tc);
                }
            }
            document.Add(tbdata);
            //------------ TABLE FOOTER ROW 
            iTextSharp.text.Table tb2 = new iTextSharp.text.Table(ColumnsCount);
            tb2.BackgroundColor = iTextSharp.text.Color.LIGHT_GRAY;
            tb2.Width = 100;
            tb2.Widths = ws;
            tb2.Alignment = Element.ALIGN_CENTER;
            tb2.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tb2.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
            tb2.BorderColor = iTextSharp.text.Color.WHITE;
            tb2.Cellspacing = 1;
            tb2.Cellpadding = 1;
            DataRow drF = dt.Rows[dt.Rows.Count - 1];
            for (int i = 0; i <= dt.Columns.Count - 1; i++)
            {
                Cell tc = new Cell(new Phrase(drF[i].ToString(), fCapText_5));
                if (i == 0)
                    tc.HorizontalAlignment = Element.ALIGN_LEFT;
                else
                    tc.HorizontalAlignment = Element.ALIGN_RIGHT;

                tb2.AddCell(tc);
            }
            //------------------------------------
            document.Add(tb2);
            document.Close();

            string FileName = PublishPath + "\\" + "Fleet Summary By Vessel.pdf";
            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }

            FileStream fs = new FileStream(FileName, FileMode.Create);
            byte[] bb = msReport.ToArray();
            fs.Write(bb, 0, bb.Length);
            fs.Flush();
            fs.Close();
        }
        catch (System.Exception ex)
        {

        }
    }
    public string FormatCurrency(object InValue)
    {
        string StrIn = InValue.ToString();
        string OutValue = "";
        int Len = StrIn.Length;
        while (Len > 3)
        {
            if (OutValue.Trim() == "")
                OutValue = StrIn.Substring(Len - 3);
            else
                OutValue = StrIn.Substring(Len - 3) + "," + OutValue;

            StrIn = StrIn.Substring(0, Len - 3);
            Len = StrIn.Length;
        }
        OutValue = StrIn + "," + OutValue;
        if (OutValue.EndsWith(",")) { OutValue = OutValue.Substring(0, OutValue.Length - 1); }
        return OutValue;
    }
    //----------------------------------------------------------------------------------
}
    
    
