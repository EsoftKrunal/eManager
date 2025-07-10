using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class PrintCurrBudget : System.Web.UI.Page
{
    #region Declarations
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    #endregion
    string FormatCurrency(object InValue)
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
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        string Year, Ship;
        Year = Request.QueryString["Year"];
        Ship = Request.QueryString["VSL"];
        CrystalReportViewer1.Visible = false;
        CrystalReportViewer1.ReportSource = rpt;
        //---------------------------------------
        ShowCurrentYearBudgetRPT();

        //Common.Set_Procedures("BUDGETFORECASTPRINT");
        //Common.Set_ParameterLength(2);
        //Common.Set_Parameters(new MyParameter("@YEAR", DateTime.Today.Year), new MyParameter("@VSLLIST", Request.QueryString["VSL"]));

        //DataSet Ds = Common.Execute_Procedures_Select();

        //if ("" + Request.QueryString["Mode"] != "PUB")
        //{
        //    CrystalReportViewer1.Visible = true;
        //    CrystalReportViewer1.ReportSource = rpt;
        //}
        
        //rpt.Load(Server.MapPath("~/Report/New_BudgetForecastReport.rpt"));
        //Ds.Tables[0].TableName = "TEMP_FORECAST_MASTERDATA";
        //Ds.Tables[1].TableName = "TEMP_FOREACST_ACCOUNTSUMMARYDATA";
        //Ds.Tables[2].TableName = "vw_BudgetForeCastComments";
        //Ds.Tables[3].TableName = "TEMP_FROECAST_ACCOUNTDATA";

        //for (int i = 0; i <= Ds.Tables[0].Rows.Count - 1; i++)
        //{
        //    string ship=Ds.Tables[0].Rows[i]["SHIPID"].ToString();
        //    DataTable dt=Common.Execute_Procedures_Select_ByQueryCMS("SELECT YEARBUILT,countryname from vessel left join country on countryid=flagstateid where vesselcode='" + ship + "'");
        //    if (dt.Rows.Count > 0)
        //    {
        //        Ds.Tables[0].Rows[i]["YearBuilt"] = dt.Rows[0][0].ToString() ;
        //        Ds.Tables[0].Rows[i]["Flag"] = dt.Rows[0][1].ToString();
        //    }
        //}
        //rpt.SetDataSource(Ds);
        ////-----------------
        //rpt.Subreports["CrystalReport_BudgetForecastSummary_New.rpt"].SetDataSource(Ds.Tables[1]);
        //rpt.Subreports["BudgetForecastComment.rpt"].SetDataSource(Ds.Tables[2]);
        //rpt.SetParameterValue("BottomContent", ""); 
        ////-----------------
        //if ("" + Request.QueryString["Mode"] == "PUB")
        //{
        //    DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select REPLACE(STR(ISNULL(MAX(SUBSTRING(version,10,LEN(version)-9)),0)+1,3),' ','0') from dbo.MW_BudgetForecast where left(version,9)='" + Request.QueryString["VSL"] + "_" + (DateTime.Today.Year+1).ToString() + "_'");
        //    string NextVersion = Request.QueryString["VSL"] + "_" + (DateTime.Today.Year + 1).ToString() + "_" + dt.Rows[0][0];
        //    string FileName = NextVersion + ".pdf";
        //    try
        //    {
        //        rpt.SetParameterValue("BottomContent", NextVersion);
        //        rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, ConfigurationManager.AppSettings["PublishedBudgetFiles"] + FileName);
        //        Common.Execute_Procedures_Select_ByQueryCMS("INSERT INTO dbo.MW_BudgetForecast(SHIPID,PYEAR,VERSION,UPLOADEDBY,UPLOADEDON) VALUES('" + Request.QueryString["VSL"] + "'," + (DateTime.Today.Year+1).ToString() + ",'" + NextVersion + "','" + Session["FullUserName"].ToString() + "',GETDATE())");
        //        Response.Clear();
        //        ScriptManager.RegisterStartupScript(Page, this.GetType(), "pub", "alert('Published Successfully.');window.close();", true);
        //    }
        //    catch(Exception ex) 
        //    {
        //        Response.Clear();
        //        Response.Write("<br/><br/><br/><center>Unable to Publish. Error : " + ex.Message + "</center>");
        //        Response.End();
        //    } 
        //}
    }
    public void ShowCurrentYearBudgetRPT()
    {
        // Query String parameter 
        string Comp, vess, BType, StartDate, EndDate, Year, Days;
        Comp = Page.Request.QueryString["Comp"].ToString();
        vess = Page.Request.QueryString["Vessel"].ToString();
        //BType = Page.Request.QueryString["BType"].ToString();
        string MajCatID = Page.Request.QueryString["MajCatID"].ToString();

        // if (BType == "< All >")
        BType = "All";
        StartDate = Page.Request.QueryString["StartDate"].ToString();
        EndDate = Page.Request.QueryString["EndDate"].ToString();
        Year = Page.Request.QueryString["year"].ToString();
        Days = Page.Request.QueryString["YearDays"].ToString();

        string sql = "SELECT v_BudgetForecastYear.AccountNumber,v_BudgetForecastYear.YearDays, v_BudgetForecastYear.AccountName, ROUND(ISNULL(v_BudgetForecastYear.ForeCast, 0), 0) AS Budget, " +
                   "v_BudgetForecastYear.ForeCast,v_BudgetForecastYear.YearDays," +
                   "Comment=isnull((select YearComment from [dbo].v_BudgetForecastYear V1 where V1.CoCode='" + Comp.Substring(0, 3) + "' AND V1.Vess='" + vess.Substring(0, 3) + "' and V1.AcctId=v_BudgetForecastYear.AcctId and V1.Year=" + (Common.CastAsInt32(Year) - 1).ToString() + "),''), " +
                   "v_BudgetForecastYear.MidCatID, v_BudgetForecastYear.MinCatID, " +
                   "    (SELECT     MidCat " +
                   "      FROM         dbo.tblAccountsMid AS G2 " +
                   "      WHERE      (MidCatID = v_BudgetForecastYear.MidCatID)) AS Group1, " +
                   "    (SELECT     MinorCat " +
                   "      FROM         dbo.tblAccountsMinor AS G3 " +
                   "      WHERE      (MinCatID = v_BudgetForecastYear.MinCatID)) AS Group2, ISNULL " +
                   "    ((SELECT     Amount " +
                   "        FROM         dbo.Add_v_BudgetForecastYear AS Addt " +
                   "        WHERE     (CoCode = v_BudgetForecastYear.CoCode) AND (AcctId = v_BudgetForecastYear.AcctID) AND (BYear = " + (Common.CastAsInt32(Year)).ToString() + ")), 0) AS AnnAmt,  " +
                   "v_BudgetForecastYear.YearComment as Comment," +
                   "v_BudgetForecastYear.Year, v_BudgetForecastYear.ForeCastYear,MajSeqNo,MidSeqNo,MinSeqNo,v_BudgetForecastYear.accountnumber,accountid " +
                   "FROM        dbo.v_BudgetForecastYear AS v_BudgetForecastYear INNER JOIN " +
                   "    (SELECT     RESULT AS CY " +
                   "FROM dbo.CSVtoTableStr('" + (Common.CastAsInt32(Year) - 1).ToString() + "', ',') AS CSVtoTableStr_1) AS tempYear ON v_BudgetForecastYear.Year = tempYear.CY WHERE left(v_BudgetForecastYear.AccountNumber,2)<>'17' And v_BudgetForecastYear.COCODE='" + Comp.Substring(0, 3) + "' AND v_BudgetForecastYear.VESS='" + vess.Substring(0, 3) + "' AND YEAR=" + (Common.CastAsInt32(Year) - 1).ToString();

        DataTable DtRpt = Common.Execute_Procedures_Select_ByQuery(sql);

        CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;

        rpt.Load(Server.MapPath("~/Modules/OPEX/Report/CurrentYearBudgetReport.rpt"));
        DtRpt.TableName = "vw_NewPR_GetCurrentYearBudgetRptStructure";
        rpt.SetDataSource(DtRpt);


        DataTable dtAccts = Common.Execute_Procedures_Select_ByQuery("select distinct midcatid,midcat,MajSeqNo,MidSeqNo from dbo.v_New_CurrYearBudgetHome WHERE MajCatId=6 ORDER BY MajSeqNo,MidSeqNo");
        DataTable dtAccts1 = Common.Execute_Procedures_Select_ByQuery("select distinct midcatid,midcat,MajSeqNo,MidSeqNo from dbo.v_New_CurrYearBudgetHome WHERE MajCatId<>6 ORDER BY MajSeqNo,MidSeqNo");
        DataTable dtDays = Common.Execute_Procedures_Select_ByQuery("select top 1 Days From dbo.v_New_CurrYearBudgetHome Where shipid='" + vess.Substring(0, 3) + "' AND year=" + (DateTime.Today.Year - 1).ToString() + " order by days desc");
        int DaysCnt = 1;
        if (dtDays != null)
        {
            if (dtDays.Rows.Count > 0)
            {
                DaysCnt = Common.CastAsInt32(dtDays.Rows[0][0]);
            }
        }
        int start = 2;
        decimal ColumnSum = 0;
        rpt.SetParameterValue("Param1", "Amount(US$) - " + Year + " ", "CurrentYearBudget_Summary.rpt");
        //----------------------------------------------
        for (int i = 0; i <= dtAccts.Rows.Count - 1; i++)
        {
            int RowSum = 0;
            DataTable dtShip = Common.Execute_Procedures_Select_ByQuery("select budget from dbo.v_New_CurrYearBudgetHome where shipid='" + vess.Substring(0, 3) + "' AND year=" + (DateTime.Today.Year - 1).ToString() + " and  midcatid=" + dtAccts.Rows[i][0].ToString());
            if (dtShip != null)
            {
                if (dtShip.Rows.Count > 0)
                {
                    ColumnSum += Common.CastAsInt32(dtShip.Rows[0][0]);
                    rpt.SetParameterValue("Param" + start.ToString(), FormatCurrency(dtShip.Rows[0][0]), "CurrentYearBudget_Summary.rpt");
                }
                else
                {
                    rpt.SetParameterValue("Param" + start.ToString(), "0", "CurrentYearBudget_Summary.rpt");
                }
            }
            else
            {
                rpt.SetParameterValue("Param" + start.ToString(), "0", "CurrentYearBudget_Summary.rpt");
            }
            start++;
        }
        //----------------------------------------------
        rpt.SetParameterValue("Param" + start.ToString(), FormatCurrency(ColumnSum), "CurrentYearBudget_Summary.rpt");
        start++;
        rpt.SetParameterValue("Param" + start.ToString(), FormatCurrency(Math.Round(ColumnSum / DaysCnt, 0)), "CurrentYearBudget_Summary.rpt");
        start++;
        //----------------------------------------------
//        for (int i = 0; i <= dtAccts1.Rows.Count - 1; i++)
        for (int i = 0; i <= 5; i++)
        {
	if(i>dtAccts1.Rows.Count-1)
	{
		rpt.SetParameterValue("Param" + start.ToString(), "0", "CurrentYearBudget_Summary.rpt");
	}
	else
	{
            DataTable dtShip = Common.Execute_Procedures_Select_ByQuery("select budget from dbo.v_New_CurrYearBudgetHome where shipid='" + vess.Substring(0, 3) + "' AND year=" + (DateTime.Today.Year - 1).ToString() + " and  midcatid=" + dtAccts1.Rows[i][0].ToString());
            if (dtShip != null)
            {
                if (dtShip.Rows.Count > 0)
                {
                    ColumnSum += Common.CastAsInt32(dtShip.Rows[0][0]);
                    rpt.SetParameterValue("Param" + start.ToString(), FormatCurrency(dtShip.Rows[0][0]), "CurrentYearBudget_Summary.rpt");
                }
                else
                {
                    rpt.SetParameterValue("Param" + start.ToString(), "0", "CurrentYearBudget_Summary.rpt");
                }
            }
            else
            {
                rpt.SetParameterValue("Param" + start.ToString(), "0", "CurrentYearBudget_Summary.rpt");
            }
           
	}
	start++;
        }
	
        rpt.SetParameterValue("Param" + start.ToString(), FormatCurrency(ColumnSum), "CurrentYearBudget_Summary.rpt");

        //----------------------------------------------
        rpt.SetParameterValue("Company", Comp);
        rpt.SetParameterValue("Vessel", vess);
        rpt.SetParameterValue("BudgetType", BType);
        rpt.SetParameterValue("Start Date", StartDate);
        rpt.SetParameterValue("End Date", EndDate);
        rpt.SetParameterValue("Year", Year);
        rpt.SetParameterValue("Days", Days);
        rpt.SetParameterValue("CYAmt", (Days == "365") ? "" : "Annual Amt(US $)");
        //----------------------------------------- final
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select REPLACE(STR(ISNULL(MAX(SUBSTRING(version,10,LEN(version)-9)),0)+1,3),' ','0') from dbo.MW_BudgetForecast where left(version,9)='" + vess.Substring(0, 3) + "_" + Year + "_'");
        string NextVersion = vess.Substring(0, 3) + "_" + Year + "_" + dt.Rows[0][0];
        string FileName = NextVersion + ".pdf";
        try
        {
            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/EMANAGERBLOB/OPEX/Publish/" + FileName));
            Common.Execute_Procedures_Select_ByQueryCMS("INSERT INTO dbo.MW_BudgetForecast(SHIPID,PYEAR,VERSION,UPLOADEDBY,UPLOADEDON) VALUES('" + vess.Substring(0, 3) + "'," + Year + ",'" + NextVersion + "','" + Session["UserFullName"].ToString() + "',GETDATE())");
            Response.Clear();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "pub", "alert('Published Successfully.');window.close();", true);
        }
        catch (Exception ex)
        {
            Response.Clear();
            Response.Write("<br/><br/><br/><center>Unable to Publish. Error : " + ex.Message + "</center>");
            Response.End();
        }
    }
    #region PageEvents
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    #endregion
}
