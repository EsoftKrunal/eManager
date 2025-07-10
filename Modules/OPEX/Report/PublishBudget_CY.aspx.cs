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

public partial class PublishBudget_CY : System.Web.UI.Page
{
    #region Declarations
    AuthenticationManager authRecInv;
    static Random R = new Random();
    string PublishPath;
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    #endregion
    int PubYear;
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        PubYear = DateTime.Today.Year;
        #region --------- USER RIGHTS MANAGEMENT -----------
        try
        {
            authRecInv = new AuthenticationManager(1092, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
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
        BindVesselBYOwner();
    }
    public void BindVesselBYOwner()
    {
        string sql = "SELECT ROW_NUMBER() OVER (ORDER BY VW_sql_tblSMDPRVessels.ShipID) AS SNO,VW_sql_tblSMDPRVessels.ShipID as VesselCode, VW_sql_tblSMDPRVessels.Company, VW_sql_tblSMDPRVessels.ShipName as vesselName, " +
                    " (VW_sql_tblSMDPRVessels.ShipID+' - '+VW_sql_tblSMDPRVessels.ShipName)as ShipNameCode" +
                    " FROM VW_sql_tblSMDPRVessels " +
                    " WHERE (((VW_sql_tblSMDPRVessels.Company)='" + ddlCompany.SelectedValue + "')) AND ACTIVE='A' ORDER BY VW_sql_tblSMDPRVessels.ShipID";
        DataTable DtVessel = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtVessel != null)
        {
            ddlShip.DataSource = DtVessel;
            ddlShip.DataTextField = "ShipNameCode";
            ddlShip.DataValueField = "VesselCode";
            ddlShip.DataBind();
            ddlShip.Items.Insert(0, new System.Web.UI.WebControls.ListItem("<Select>", "0"));
        }

    }
    public void BindData()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT *,convert(varchar,PublishedOn,106) as PublishedOnF FROM DBO.MW_CompanyBudgetForeCast WHERE COMPCODE='" + ddlCompany.SelectedValue + "' and YEAR=" + (PubYear).ToString());
        rpt_Data.DataSource = dt;
        rpt_Data.DataBind();
    }
    protected void btnPublish_Click(object sender, EventArgs e)
    {
        string Year = (PubYear).ToString();
        string CompanyCode = ddlCompany.SelectedValue;
        string CompanyName = ddlCompany.SelectedItem.Text;

        PublishPath = Server.MapPath("~/EMANAGERBLOB/OPEX/Publish_CY/" + CompanyCode + "/");
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

        Export_Budget(Year,CompanyCode, CompanyName);
        Export_CompanySummary_ByVessel(Year,CompanyCode, CompanyName);
        //Export_CompanySummary(CompanyCode, CompanyName, Common.CastAsInt32(Year));

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT ISNULL(MAX(CAST(RIGHT( FILENAME ,3) AS INT)),0)+1 FROM DBO.MW_CompanyBudgetForeCast WHERE COMPCODE='" + CompanyCode + "'");
        string NewNo = dt.Rows[0][0].ToString().Trim().PadLeft(3, '0');

        string FileName = CompanyCode + "_" + Year + "_" + NewNo;

        string ZipData = Server.MapPath("~/Publish_NY/" + FileName + ".zip");
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
      
    }
    
    //--------------- EXPORT VESSEL BUDGET FORECAST ---------------------------------
    public void Export_Budget(string Year, string CompanyCode, string CompanyName)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select SHIPID,ShipName from [dbo].[VW_sql_tblSMDPRVessels] where company='" + CompanyCode + "' AND VESSELNO>0 AND ACTIVE='A'");
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                Export_Budget_Vessel(Year, CompanyCode, CompanyName, dr["SHIPID"].ToString(), dr["ShipName"].ToString());
            }
        }
    }
    public void Export_Budget_Vessel(string Year, string CompanyCode, string CompanyName, string VesselCode, string VesselName)
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

        Bind_BudgetRPT(CompanyPart, VesselPart, "All", StartDate, EndDate, Year, Days.ToString());

        string FileName = PublishPath + "\\" + "Vessel Budget [ " + VesselCode + "] .pdf";
        rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, FileName);
    }
    public void Bind_BudgetRPT(string Comp, string vess, string BType, string StartDate, string EndDate, string Year, string Days)
    {
        
        string sql = "SELECT v_BudgetForecastYear.AccountNumber,v_BudgetForecastYear.YearDays, v_BudgetForecastYear.AccountName, ROUND(ISNULL(v_BudgetForecastYear.ForeCast, 0), 0) AS Budget, " +
                   "v_BudgetForecastYear.ForeCast,v_BudgetForecastYear.YearDays," +
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
                   "        WHERE     (CoCode = v_BudgetForecastYear.CoCode) AND (AcctId = v_BudgetForecastYear.AcctID) AND (BYear = " + (Common.CastAsInt32(Year)).ToString() + ")), 0) AS AnnAmt,  " +
                   "v_BudgetForecastYear.YearComment as Comment," +
                   "v_BudgetForecastYear.Year, v_BudgetForecastYear.ForeCastYear,MajSeqNo,MidSeqNo,MinSeqNo,v_BudgetForecastYear.accountnumber,accountid " +
                   "FROM         dbo.v_BudgetForecastYear AS v_BudgetForecastYear INNER JOIN " +
                   "    (SELECT     RESULT AS CY " +
                   "FROM dbo.CSVtoTableStr('" + (Common.CastAsInt32(Year) - 1).ToString() + "', ',') AS CSVtoTableStr_1) AS tempYear ON v_BudgetForecastYear.Year = tempYear.CY WHERE v_BudgetForecastYear.COCODE='" + Comp.Substring(0, 3) + "' AND v_BudgetForecastYear.VESS='" + vess.Substring(0, 3) + "' AND YEAR=" + (Common.CastAsInt32(Year) - 1).ToString();

        DataTable DtRpt = Common.Execute_Procedures_Select_ByQuery(sql);

        rpt.Load(Server.MapPath("~/Modules/OPEX/Report/CurrentYearBudgetReport.rpt"));
        DtRpt.TableName = "vw_NewPR_GetCurrentYearBudgetRptStructure";
        rpt.SetDataSource(DtRpt);


        DataTable dtAccts = Common.Execute_Procedures_Select_ByQuery("select distinct midcatid,midcat,MajSeqNo,MidSeqNo from dbo.v_New_CurrYearBudgetHome WHERE MajCatId=6 ORDER BY MajSeqNo,MidSeqNo");
        DataTable dtAccts1 = Common.Execute_Procedures_Select_ByQuery("select * from ( select distinct midcatid,midcat,MajSeqNo,MidSeqNo from dbo.v_New_CurrYearBudgetHome WHERE MajCatId<>6 union select 26,'Pre-Delivery Mgmt Fees',550,171) A ORDER BY MajSeqNo,MidSeqNo");
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
        for (int i = 0; i <= dtAccts1.Rows.Count - 1; i++)
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
    


        //string sql = "SELECT v_BudgetForecastYear.AccountNumber,v_BudgetForecastYear.YearDays, v_BudgetForecastYear.AccountName, ROUND(ISNULL(v_BudgetForecastYear.ForeCast, 0), 0) AS Budget, " +
        //              "Forecast=isnull((select Amount from Add_v_BudgetForecastYear Addt where Addt.cocode=v_BudgetForecastYear.cocode and Addt.AcctId=v_BudgetForecastYear.AcctId and Addt.Byear=" + Year.ToString() + "),0)  , " +
        //              "Comment=isnull((select YearComment from [dbo].v_BudgetForecastYear V1 where V1.CoCode='" + Comp.Substring(0, 3) + "' AND V1.Vess='" + vess.Substring(0, 3) + "' and V1.AcctId=v_BudgetForecastYear.AcctId and V1.Year=" + (Common.CastAsInt32(Year) - 1).ToString() + "),''), " +
        //              "v_BudgetForecastYear.MidCatID, v_BudgetForecastYear.MinCatID, " +
        //              "    (SELECT     MidCat " +
        //              "      FROM          dbo.tblAccountsMid AS G2 " +
        //              "      WHERE      (MidCatID = v_BudgetForecastYear.MidCatID)) AS Group1, " +
        //              "    (SELECT     MinorCat " +
        //              "      FROM          dbo.tblAccountsMinor AS G3 " +
        //              "      WHERE      (MinCatID = v_BudgetForecastYear.MinCatID)) AS Group2, ISNULL " +
        //              "    ((SELECT     Amount " +
        //              "        FROM         dbo.Add_v_BudgetForecastYear AS Addt " +
        //              "        WHERE     (CoCode = v_BudgetForecastYear.CoCode) AND (AcctId = v_BudgetForecastYear.AcctID) AND (BYear = " + (Common.CastAsInt32(Year) - 1).ToString() + ")), 0) AS AnnAmt,  " +
        //              " Comment=isnull((select YearComment from [dbo].v_BudgetForecastYear V1 where V1.CoCode=v_BudgetForecastYear.Cocode AND V1.Vess=v_BudgetForecastYear.vess and V1.AcctId=v_BudgetForecastYear.AcctId and V1.Year=" + (Common.CastAsInt32(Year) - 1).ToString() + "),''), " +
        //              "v_BudgetForecastYear.Year, v_BudgetForecastYear.ForeCastYear,MajSeqNo,MidSeqNo,MinSeqNo,v_BudgetForecastYear.accountnumber,accountid " +
        //              "FROM         dbo.v_BudgetForecastYear AS v_BudgetForecastYear INNER JOIN " +
        //              "    (SELECT     RESULT AS CY " +
        //              "FROM dbo.CSVtoTableStr('" + (Common.CastAsInt32(Year) - 2).ToString() + "', ',') AS CSVtoTableStr_1) AS tempYear ON v_BudgetForecastYear.Year = tempYear.CY WHERE v_BudgetForecastYear.COCODE='" + Comp.Substring(0, 3) + "' AND v_BudgetForecastYear.VESS='" + vess.Substring(0, 3) + "' AND LEFT(v_BudgetForecastYear.ACCOUNTNUMBER,2)<>'17' AND v_BudgetForecastYear.ACCOUNTNUMBER<>8590 AND (v_BudgetForecastYear.ACCOUNTNUMBER < 8521 OR v_BudgetForecastYear.ACCOUNTNUMBER > 9827) AND YEAR=" + (Common.CastAsInt32(Year) - 2).ToString();

        //sql = "EXEC [dbo].[fn_NEW_GETCMBUDGETACTUAL_ForeCastPrint] '" + Comp.Substring(0, 3) + "'," + DateTime.Today.Month.ToString() + "," + (Common.CastAsInt32(Year) - 1).ToString() + ",'" + vess.Substring(0, 3) + " '";
        //DataTable DtRpt = Common.Execute_Procedures_Select_ByQuery(sql);

        //rpt.Load(Server.MapPath("~/Modules/OPEX/Report/BudgetForecastReport.rpt"));
        //DtRpt.TableName = "vw_NewPR_GetCurrentYearBudgetRptStructure";
        //rpt.SetDataSource(DtRpt);

        //DataTable dtChild = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM vw_BudgetForeCastComments where cocode='" + Comp.Substring(0, 3) + "' and shipid='" + vess.Substring(0, 3) + "' and forecastyear=" + (Common.CastAsInt32(Year)).ToString() + " order by midseqno");
        //rpt.Subreports["BudgetForecastComment.rpt"].SetDataSource(dtChild);


        ////----------------------------------------------
        //string Qry = "select FORECAST from dbo.tblsmdbudgetforecastyear where cocode='" + Comp.Substring(0, 3) + "' AND YEAR=" + (DateTime.Today.Year - 1).ToString() + " AND ACCOUNTNUMBER=5100 AND SHIPID='" + vess.Substring(0, 3) + "'";
        //DataTable dtheader = Common.Execute_Procedures_Select_ByQuery(Qry);
        //int Amt_5100 = 0;
        //if (dtheader.Rows.Count > 0)
        //{
        //    Amt_5100 = Common.CastAsInt32(dtheader.Rows[0][0]);
        //    if (Amt_5100 > 0)
        //        Amt_5100 = ProjectCommon.Get_ManningAmount(Comp.Substring(0, 3), vess.Substring(0, 3), DateTime.Today.Year);
        //}
        ////-------------------

        //DataTable dtAccts = Common.Execute_Procedures_Select_ByQuery("select distinct midcatid,midcat,MajSeqNo,MidSeqNo from dbo.v_New_CurrYearBudgetHome WHERE MajCatId=6 ORDER BY MajSeqNo,MidSeqNo");
        //DataTable dtAccts1 = Common.Execute_Procedures_Select_ByQuery("select distinct midcatid,midcat,MajSeqNo,MidSeqNo from dbo.v_New_CurrYearBudgetHome WHERE MajCatId<>6 And MidcatId not in (25,26) ORDER BY MajSeqNo,MidSeqNo");
        //DataTable dtDays = Common.Execute_Procedures_Select_ByQuery("select top 1 Days From dbo.v_New_CurrYearBudgetHome Where shipid='" + vess.Substring(0, 3) + "' AND year=" + (DateTime.Today.Year).ToString() + " order by days desc");
        //DataTable dtDaysLast = Common.Execute_Procedures_Select_ByQuery("select top 1 Days From dbo.v_New_CurrYearBudgetHome Where shipid='" + vess.Substring(0, 3) + "' AND year=" + (DateTime.Today.Year - 1).ToString() + " order by days desc");
        //DataTable dtActCom_Proj = Common.Execute_Procedures_Select_ByQuery("EXEC [dbo].[fn_NEW_GETCMBUDGETACTUAL_MIDCATWISE_FORDATE] '" + Comp.Substring(0, 3) + "'," + (DateTime.Today.Month).ToString() + "," + (DateTime.Today.Year).ToString() + "," + (DateTime.Today.Day).ToString() + ",'" + vess.Substring(0, 3) + "'");
        //DataView dv = dtActCom_Proj.DefaultView;
        //int DaysCnt = 1;
        //if (dtDays != null)
        //{
        //    if (dtDays.Rows.Count > 0)
        //    {
        //        DaysCnt = Common.CastAsInt32(dtDays.Rows[0][0]);
        //    }
        //}
        //int DaysCntLast = 1;
        //if (dtDaysLast != null)
        //{
        //    if (dtDaysLast.Rows.Count > 0)
        //    {
        //        DaysCntLast = Common.CastAsInt32(dtDaysLast.Rows[0][0]);
        //    }
        //}
        //int start = 2;

        //decimal ColumnSum = 0, ColumnSumLast = 0;
        //decimal ColumnSum2 = 0, ColumnSum3 = 0;

        //rpt.SetParameterValue("Param1", "Budget " + (Common.CastAsInt32(Year) - 1).ToString() + " ", "BudgetForecastReport_Summary.rpt");
        //rpt.SetParameterValue("Param101", "Budget " + Year + " ", "BudgetForecastReport_Summary.rpt"); // forecast

        //rpt.SetParameterValue("Param201", "Act. & Comm " + (Common.CastAsInt32(Year) - 1).ToString() + " ", "BudgetForecastReport_Summary.rpt");
        //rpt.SetParameterValue("Param301", "Projected " + (Common.CastAsInt32(Year) - 1).ToString() + " ", "BudgetForecastReport_Summary.rpt");

        //rpt.SetParameterValue("Param401", (Common.CastAsInt32(Year)).ToString() + "-Budget Var.% ", "BudgetForecastReport_Summary.rpt");

        //rpt.SetParameterValue("Param501", (Common.CastAsInt32(Year) - 1).ToString() + "-Var.%", "BudgetForecastReport_Summary.rpt");
        //rpt.SetParameterValue("Param601", "", "BudgetForecastReport_Summary.rpt");

        ////----------------------------------------------
        //for (int i = 0; i <= dtAccts.Rows.Count - 1; i++)
        //{
        //    int RowSum = 0;
        //    dv.RowFilter = "MidCatId=" + dtAccts.Rows[i][0].ToString();
        //    DataTable dt1 = dv.ToTable();
        //    decimal ActComm = 0, Projected = 0, Budget = 0, ForeCast = 0;

        //    if (i != 0) { Amt_5100 = 0; }

        //    if (Amt_5100 > 0)
        //    {
        //        ColumnSum3 += Amt_5100;
        //        ActComm = 0;
        //        Projected = Amt_5100;

        //        // OLD LINE
        //        //rpt.SetParameterValue("Param20" + start.ToString(), "0", "BudgetForecastReport_Summary.rpt");
        //        // NEW LINE
        //        rpt.SetParameterValue("Param20" + start.ToString(), FormatCurrency(dt1.Rows[0]["ACT_CONS"]), "BudgetForecastReport_Summary.rpt");
        //        ActComm = Common.CastAsInt32(dt1.Rows[0]["ACT_CONS"]);
        //        ColumnSum2 += ActComm;
        //        // END

        //        rpt.SetParameterValue("Param30" + start.ToString(), Amt_5100.ToString(), "BudgetForecastReport_Summary.rpt");
        //    }
        //    else
        //    {
        //        if (dt1.Rows.Count > 0)
        //        {
        //            rpt.SetParameterValue("Param20" + start.ToString(), FormatCurrency(dt1.Rows[0]["ACT_CONS"]), "BudgetForecastReport_Summary.rpt");
        //            rpt.SetParameterValue("Param30" + start.ToString(), FormatCurrency(dt1.Rows[0]["PROJECTED"]), "BudgetForecastReport_Summary.rpt");
        //            ColumnSum2 += Common.CastAsInt32(dt1.Rows[0]["ACT_CONS"]);
        //            ColumnSum3 += Common.CastAsInt32(dt1.Rows[0]["PROJECTED"]);

        //            ActComm = Common.CastAsDecimal(dt1.Rows[0]["ACT_CONS"]);
        //            Projected = Common.CastAsDecimal(dt1.Rows[0]["PROJECTED"]);
        //        }
        //        else
        //        {
        //            rpt.SetParameterValue("Param20" + start.ToString(), "0", "BudgetForecastReport_Summary.rpt");
        //            rpt.SetParameterValue("Param30" + start.ToString(), "0", "BudgetForecastReport_Summary.rpt");
        //        }
        //    }
        //    DataTable dtShip = Common.Execute_Procedures_Select_ByQuery("select budget from dbo.v_New_CurrYearBudgetHome where shipid='" + vess.Substring(0, 3) + "' AND year=" + (DateTime.Today.Year - 1).ToString() + " and  midcatid=" + dtAccts.Rows[i][0].ToString());
        //    if (dtShip != null)
        //    {
        //        if (dtShip.Rows.Count > 0)
        //        {
        //            ColumnSum += Common.CastAsInt32(dtShip.Rows[0][0]);
        //            rpt.SetParameterValue("Param" + start.ToString(), FormatCurrency(dtShip.Rows[0][0]), "BudgetForecastReport_Summary.rpt");
        //            Budget = Common.CastAsDecimal(dtShip.Rows[0][0]);
        //        }
        //        else
        //        {
        //            rpt.SetParameterValue("Param" + start.ToString(), "0", "BudgetForecastReport_Summary.rpt");
        //        }
        //    }
        //    else
        //    {
        //        rpt.SetParameterValue("Param" + start.ToString(), "0", "BudgetForecastReport_Summary.rpt");
        //    }

        //    DataTable dtShipLast = Common.Execute_Procedures_Select_ByQuery("select nextyearforecastamount from dbo.v_New_CurrYearBudgetHome where shipid='" + vess.Substring(0, 3) + "' AND year=" + (DateTime.Today.Year - 1).ToString() + " and  midcatid=" + dtAccts.Rows[i][0].ToString());
        //    if (dtShipLast != null)
        //    {
        //        if (dtShipLast.Rows.Count > 0)
        //        {
        //            ColumnSumLast += Common.CastAsInt32(dtShipLast.Rows[0][0]);
        //            rpt.SetParameterValue("Param10" + start.ToString(), FormatCurrency(dtShipLast.Rows[0][0]), "BudgetForecastReport_Summary.rpt");
        //            ForeCast = Common.CastAsDecimal(dtShipLast.Rows[0][0]);
        //        }
        //        else
        //        {
        //            rpt.SetParameterValue("Param10" + start.ToString(), "0", "BudgetForecastReport_Summary.rpt");
        //        }
        //    }
        //    else
        //    {
        //        rpt.SetParameterValue("Param10" + start.ToString(), "0", "BudgetForecastReport_Summary.rpt");
        //    }

        //    if (Budget > 0)
        //        ActComm = ((Projected - Budget) / Budget) * 100;
        //    else
        //        ActComm = 0;

        //    if (Projected > 0)
        //        Projected = ((ForeCast - Projected) / Projected) * 100;
        //    else
        //        Projected = 0;

        //    if (Budget > 0)
        //    {
        //        // ----- NEW LINE
        //        //Budget = (Budget / DaysCntLast) * DaysCnt;
        //        decimal dddddd1 = (Budget / DaysCntLast) * DaysCnt;
        //        Budget = ((ForeCast - dddddd1) / dddddd1) * 100;
        //        // ----- NEW LINE
        //        //Budget = ((ForeCast - Budget) / Budget) * 100;
        //    }
        //    else
        //        Budget = 0;

        //    rpt.SetParameterValue("Param40" + start.ToString(), Math.Round(ActComm, 0) + "%", "BudgetForecastReport_Summary.rpt");
        //    rpt.SetParameterValue("Param50" + start.ToString(), Math.Round(Projected, 0) + "%", "BudgetForecastReport_Summary.rpt");
        //    rpt.SetParameterValue("Param60" + start.ToString(), Math.Round(Budget, 0) + "%", "BudgetForecastReport_Summary.rpt");

        //    start++;
        //}
        ////----------------------------------------------
        //rpt.SetParameterValue("Param" + start.ToString(), FormatCurrency(ColumnSum), "BudgetForecastReport_Summary.rpt");
        //rpt.SetParameterValue("Param10" + start.ToString(), FormatCurrency(ColumnSumLast), "BudgetForecastReport_Summary.rpt");
        //rpt.SetParameterValue("Param20" + start.ToString(), FormatCurrency(ColumnSum2), "BudgetForecastReport_Summary.rpt");
        //rpt.SetParameterValue("Param30" + start.ToString(), FormatCurrency(ColumnSum3), "BudgetForecastReport_Summary.rpt");


        //rpt.SetParameterValue("Param40" + start.ToString(), Math.Round(((ColumnSum3 - ColumnSum) / ((ColumnSum == 0) ? 1 : ColumnSum)) * 100, 0) + "%", "BudgetForecastReport_Summary.rpt");
        //rpt.SetParameterValue("Param50" + start.ToString(), Math.Round(((ColumnSumLast - ColumnSum3) / ((ColumnSum3 == 0) ? 1 : ColumnSum3)) * 100, 0) + "%", "BudgetForecastReport_Summary.rpt");
        //// ----- NEW LINE
        //Decimal dddd1 = (ColumnSum / DaysCntLast) * DaysCnt;
        //// ----- NEW LINE
        //rpt.SetParameterValue("Param60" + start.ToString(), Math.Round(((ColumnSumLast - dddd1) / ((dddd1 == 0) ? 1 : dddd1)) * 100, 0) + "%", "BudgetForecastReport_Summary.rpt");
        ////rpt.SetParameterValue("Param60" + start.ToString(), Math.Round(((ColumnSumLast - ColumnSum) / ((ColumnSum == 0) ? 1 : ColumnSum)) * 100, 0) + "%", "BudgetForecastReport_Summary.rpt");

        //start++;

        //int DaysTillyearEnd = new DateTime(Common.CastAsInt32(Year) - 1, 12, 31).Subtract(DateTime.Today).Days;
        //int DaysTilltoday = DaysCntLast - DaysTillyearEnd;
        //if (DaysTilltoday > 365)
        //    DaysTilltoday = 365;

        //rpt.SetParameterValue("Param" + start.ToString(), FormatCurrency(Math.Round(ColumnSum / DaysCntLast, 0)), "BudgetForecastReport_Summary.rpt");
        //rpt.SetParameterValue("Param10" + start.ToString(), FormatCurrency(Math.Round(ColumnSumLast / DaysCnt, 0)), "BudgetForecastReport_Summary.rpt");
        //rpt.SetParameterValue("Param20" + start.ToString(), FormatCurrency(Math.Round(ColumnSum2 / DaysTilltoday, 0)), "BudgetForecastReport_Summary.rpt");
        //rpt.SetParameterValue("Param30" + start.ToString(), FormatCurrency(Math.Round(ColumnSum3 / DaysCntLast, 0)), "BudgetForecastReport_Summary.rpt");

        //rpt.SetParameterValue("Param40" + start.ToString(), "", "BudgetForecastReport_Summary.rpt");
        //rpt.SetParameterValue("Param50" + start.ToString(), "", "BudgetForecastReport_Summary.rpt");
        //rpt.SetParameterValue("Param60" + start.ToString(), "", "BudgetForecastReport_Summary.rpt");
        //start++;
        ////----------------------------------------------

        //for (int i = 0; i <= dtAccts1.Rows.Count - 1; i++)
        //{
        //    dv.RowFilter = "MidCatId=" + dtAccts1.Rows[i][0].ToString();
        //    decimal ActComm = 0, Projected = 0, Budget = 0, ForeCast = 0;
        //    DataTable dt1 = dv.ToTable();
        //    if (dt1.Rows.Count > 0)
        //    {
        //        rpt.SetParameterValue("Param20" + start.ToString(), FormatCurrency(dt1.Rows[0]["ACT_CONS"]), "BudgetForecastReport_Summary.rpt");
        //        rpt.SetParameterValue("Param30" + start.ToString(), FormatCurrency(dt1.Rows[0]["PROJECTED"]), "BudgetForecastReport_Summary.rpt");
        //        ColumnSum2 += Common.CastAsInt32(dt1.Rows[0]["ACT_CONS"]);
        //        ColumnSum3 += Common.CastAsInt32(dt1.Rows[0]["PROJECTED"]);

        //        ActComm = Common.CastAsDecimal(dt1.Rows[0]["ACT_CONS"]);
        //        Projected = Common.CastAsDecimal(dt1.Rows[0]["PROJECTED"]);
        //    }
        //    else
        //    {
        //        rpt.SetParameterValue("Param20" + start.ToString(), "0", "BudgetForecastReport_Summary.rpt");
        //        rpt.SetParameterValue("Param30" + start.ToString(), "0", "BudgetForecastReport_Summary.rpt");
        //    }

        //    DataTable dtShip = Common.Execute_Procedures_Select_ByQuery("select budget from dbo.v_New_CurrYearBudgetHome where shipid='" + vess.Substring(0, 3) + "' AND year=" + (DateTime.Today.Year - 1).ToString() + " and  midcatid=" + dtAccts1.Rows[i][0].ToString());
        //    if (dtShip != null)
        //    {
        //        if (dtShip.Rows.Count > 0)
        //        {
        //            ColumnSum += Common.CastAsInt32(dtShip.Rows[0][0]);
        //            rpt.SetParameterValue("Param" + start.ToString(), FormatCurrency(dtShip.Rows[0][0]), "BudgetForecastReport_Summary.rpt");
        //            Budget = Common.CastAsDecimal(dtShip.Rows[0][0]);
        //        }
        //        else
        //        {
        //            rpt.SetParameterValue("Param" + start.ToString(), "0", "BudgetForecastReport_Summary.rpt");
        //        }
        //    }
        //    else
        //    {
        //        rpt.SetParameterValue("Param" + start.ToString(), "0", "BudgetForecastReport_Summary.rpt");
        //    }

        //    DataTable dtShipLast = Common.Execute_Procedures_Select_ByQuery("select nextyearforecastamount from dbo.v_New_CurrYearBudgetHome where shipid='" + vess.Substring(0, 3) + "' AND year=" + (DateTime.Today.Year - 1).ToString() + " and  midcatid=" + dtAccts1.Rows[i][0].ToString());
        //    if (dtShipLast != null)
        //    {
        //        if (dtShipLast.Rows.Count > 0)
        //        {
        //            ColumnSumLast += Common.CastAsInt32(dtShipLast.Rows[0][0]);
        //            rpt.SetParameterValue("Param10" + start.ToString(), FormatCurrency(dtShipLast.Rows[0][0]), "BudgetForecastReport_Summary.rpt");
        //            ForeCast = Common.CastAsDecimal(dtShipLast.Rows[0][0]);
        //        }
        //        else
        //        {
        //            rpt.SetParameterValue("Param10" + start.ToString(), "0", "BudgetForecastReport_Summary.rpt");
        //        }
        //    }
        //    else
        //    {
        //        rpt.SetParameterValue("Param10" + start.ToString(), "0", "BudgetForecastReport_Summary.rpt");
        //    }

        //    if (Budget > 0)
        //        ActComm = ((Projected - Budget) / Budget) * 100;
        //    else
        //        ActComm = 0;

        //    if (Projected > 0)
        //        Projected = ((ForeCast - Projected) / Projected) * 100;
        //    else
        //        Projected = 0;

        //    if (Budget > 0)
        //    {
        //        // ----- NEW LINE
        //        decimal dddddd2 = (Budget / DaysCntLast) * DaysCnt;
        //        Budget = ((ForeCast - dddddd2) / dddddd2) * 100;
        //        // ----- NEW LINE
        //        //Budget = ((ForeCast - Budget) / Budget) * 100;
        //    }
        //    else
        //        Budget = 0;

        //    rpt.SetParameterValue("Param40" + start.ToString(), Math.Round(ActComm, 0) + "%", "BudgetForecastReport_Summary.rpt");
        //    rpt.SetParameterValue("Param50" + start.ToString(), Math.Round(Projected, 0) + "%", "BudgetForecastReport_Summary.rpt");
        //    rpt.SetParameterValue("Param60" + start.ToString(), Math.Round(Budget, 0) + "%", "BudgetForecastReport_Summary.rpt");

        //    start++;
        //}
        //rpt.SetParameterValue("Param" + start.ToString(), FormatCurrency(Math.Round(ColumnSum, 0)), "BudgetForecastReport_Summary.rpt");
        //rpt.SetParameterValue("Param10" + start.ToString(), FormatCurrency(ColumnSumLast), "BudgetForecastReport_Summary.rpt");
        //rpt.SetParameterValue("Param20" + start.ToString(), FormatCurrency(ColumnSum2), "BudgetForecastReport_Summary.rpt");
        //rpt.SetParameterValue("Param30" + start.ToString(), FormatCurrency(ColumnSum3), "BudgetForecastReport_Summary.rpt");

        //rpt.SetParameterValue("Param40" + start.ToString(), Math.Round(((ColumnSum3 - ColumnSum) / ((ColumnSum == 0) ? 1 : ColumnSum)) * 100, 0) + "%", "BudgetForecastReport_Summary.rpt");
        //rpt.SetParameterValue("Param50" + start.ToString(), Math.Round(((ColumnSumLast - ColumnSum3) / ((ColumnSum3 == 0) ? 1 : ColumnSum3)) * 100, 0) + "%", "BudgetForecastReport_Summary.rpt");
        //// ----- NEW LINE
        //decimal dddd2 = (ColumnSum / DaysCntLast) * DaysCnt;
        //// ----- NEW LINE
        //rpt.SetParameterValue("Param60" + start.ToString(), Math.Round(((ColumnSumLast - dddd2) / ((dddd2 == 0) ? 1 : dddd2)) * 100, 0) + "%", "BudgetForecastReport_Summary.rpt");
        ////rpt.SetParameterValue("Param60" + start.ToString(), Math.Round(((ColumnSumLast - ColumnSum) / ((ColumnSum == 0) ? 1 : ColumnSum)) * 100, 0) + "%", "BudgetForecastReport_Summary.rpt");

        //rpt.SetParameterValue("Param6016", (Common.CastAsInt32(Year) - 1).ToString() + "[B]", "BudgetForecastReport_Summary.rpt");
        //rpt.SetParameterValue("Param6017", (Common.CastAsInt32(Year) - 1).ToString() + "[P]", "BudgetForecastReport_Summary.rpt");

        //rpt.SetParameterValue("Byear", "[" + (Common.CastAsInt32(DaysCntLast)).ToString() + "]", "BudgetForecastReport_Summary.rpt");
        //rpt.SetParameterValue("Fyear", "[" + (Common.CastAsInt32(DaysCnt)).ToString() + "]", "BudgetForecastReport_Summary.rpt");
        ////----------------------------------------------

        //rpt.SetParameterValue("Company", Comp);
        //rpt.SetParameterValue("Vessel", vess);
        //rpt.SetParameterValue("BudgetType", BType);
        //rpt.SetParameterValue("Start Date", StartDate);
        //rpt.SetParameterValue("End Date", EndDate);
        //rpt.SetParameterValue("Year", Year);
        //rpt.SetParameterValue("Days", "[" + Days + "]");
        //rpt.SetParameterValue("LastDays", "[" + DtRpt.Rows[0]["YearDays"].ToString() + "]");

        //DataTable dt_flag = Common.Execute_Procedures_Select_ByQueryCMS("select yearbuilt,(select flagstatename from flagstate where flagstate.flagstateid=vessel.flagstateid) as Flag from vessel where vessel.vesselcode='" + vess.Substring(0, 3) + "'");
        //if (dt_flag != null)
        //{
        //    if (dt_flag.Rows.Count > 0)
        //    {
        //        rpt.SetParameterValue("YearBuilt", dt_flag.Rows[0]["yearbuilt"].ToString());
        //        rpt.SetParameterValue("Flag", dt_flag.Rows[0]["Flag"].ToString());
        //    }
        //    else
        //    {
        //        rpt.SetParameterValue("YearBuilt", "");
        //        rpt.SetParameterValue("Flag", "");
        //    }
        //}
        //else
        //{
        //    rpt.SetParameterValue("YearBuilt", "");
        //    rpt.SetParameterValue("Flag", "");
        //}

    }

    //--------------- EXPORT COMPANY SUMMARY BY VESSEL ---------------------------------
    public void Export_CompanySummary_ByVessel(string Year, string CompanyCode, string CompanyName)
    {
        DataTable DtForPDR = new DataTable();
        DtForPDR.Columns.Add("BudgetHead");
        int[] ColumnSum;
        int[] VesselDays;
        ColumnSum = new int[ddlShip.Items.Count - 1];
        VesselDays = new int[ddlShip.Items.Count - 1];

        StringBuilder sb = new StringBuilder();
        //StringBuilder sbLast = new StringBuilder();

        DataTable dtAccts = Common.Execute_Procedures_Select_ByQuery("select distinct midcatid,midcat,MajSeqNo,MidSeqNo from dbo.v_New_CurrYearBudgetHome WHERE MajCatId=6 ORDER BY MajSeqNo,MidSeqNo");
        DataTable dtAccts1 = Common.Execute_Procedures_Select_ByQuery("select distinct midcatid,midcat,MajSeqNo,MidSeqNo from dbo.v_New_CurrYearBudgetHome WHERE MajCatId<>6 ORDER BY MajSeqNo,MidSeqNo");
        sb.Append("<table cellpadding='4' cellspacing='0' width='100%' class='newformat' border='1' style='border-collapse:collapse' bordercolor='white'>");
        sb.Append("<thead>");
        sb.Append("<tr>");
        sb.Append("<td>Budget Head</td>");

        //Create datatable for print pdf-------------------------
        for (int i = 0; i < ddlShip.Items.Count; i++)
        {
            if (i != 0)
            {
                DtForPDR.Columns.Add(ddlShip.Items[i].Value);
            }
        }

        DtForPDR.Columns.Add("Total");
        DataRow DrPDF = DtForPDR.NewRow();
        DrPDF[0] = "Year Head";
        DrPDF["Total"] = "";
        //End creatint datatable for print pdf-------------------------
        for (int i = 0; i < ddlShip.Items.Count; i++)
        {
            if (i != 0)
            {
                sb.Append("<td style='width:80px;'><input type='radio' name='radVSL' value='" + ddlShip.Items[i].Value + "' id='rad" + ddlShip.Items[i].Value + "'><label for='rad" + ddlShip.Items[i].Value + "'>" + ddlShip.Items[i].Value + "</label>");
                DataTable dtDays = Common.Execute_Procedures_Select_ByQuery("select top 1 Days From dbo.v_New_CurrYearBudgetHome Where shipid='" + ddlShip.Items[i].Value + "' AND year=" + (DateTime.Today.Year - 1).ToString() + " order by days desc");
                if (dtDays != null)
                {
                    if (dtDays.Rows.Count > 0)
                    {
                        sb.Append("</br>" + dtDays.Rows[0][0].ToString() + " days");
                        VesselDays[i - 1] = Common.CastAsInt32(dtDays.Rows[0][0]);

                        DrPDF[ddlShip.Items[i].Value] = dtDays.Rows[0][0].ToString() + " days";
                    }
                }
                sb.Append("</td>");
            }
        }

        sb.Append("<td style='width:80px;'>Total</td>");
        sb.Append("</tr>");
        sb.Append("</thead>");
        sb.Append("</table>");
        DrPDF["Total"] = "Total";
        DtForPDR.Rows.Add(DrPDF);

        sb.Append("<table cellpadding='4' cellspacing='0' width='100%' class='newformat' border='1' style='border-collapse:collapse' bordercolor='white'>");
        sb.Append("<tbody>");

        //-----------------
        // DATA ROWS 
        for (int i = 0; i <= dtAccts.Rows.Count - 1; i++)
        {
            DataRow drPDF = DtForPDR.NewRow();
            int RowSum = 0;
            sb.Append("<tr onmouseover=''>");
            sb.Append("<td style='text-align:left;'>" + dtAccts.Rows[i][1].ToString() + "</td>");
            drPDF[0] = dtAccts.Rows[i][1].ToString();
            for (int j = 0; j < ddlShip.Items.Count; j++)
            {
                if (j != 0)
                {
                    DataTable dtShip = Common.Execute_Procedures_Select_ByQuery("select budget from dbo.v_New_CurrYearBudgetHome where shipid='" + ddlShip.Items[j].Value + "' AND year=" + (DateTime.Today.Year - 1).ToString() + " and  midcatid=" + dtAccts.Rows[i][0].ToString());
                    if (dtShip != null)
                    {
                        if (dtShip.Rows.Count > 0)
                        {
                            sb.Append("<td style='width:80px;text-align:right'>&nbsp;" + FormatCurrency(dtShip.Rows[0][0]) + "</td>");
                            ColumnSum[j - 1] += Common.CastAsInt32(dtShip.Rows[0][0]);
                            RowSum += Common.CastAsInt32(dtShip.Rows[0][0]);
                            drPDF[j] = FormatCurrency(dtShip.Rows[0][0]).ToString();
                        }
                        else
                        {
                            sb.Append("<td style='width:80px;'></td>");
                        }
                    }
                    else
                    {
                        sb.Append("<td style='width:80px;'></td>");
                    }
                }
            }
            sb.Append("<td style='width:80px;text-align:right'>" + FormatCurrency(RowSum) + "</td>");
            sb.Append("</tr>");
            drPDF["Total"] = FormatCurrency(RowSum);
            DtForPDR.Rows.Add(drPDF);
        }
        //---------------
        // TOTAL
        DataRow drPDFT = DtForPDR.NewRow();
        sb.Append("<tr style='background-color:#FFCC66'>");
        sb.Append("<td style='font-size:10px;text-align:right;'>Total(US$)</td>");
        drPDFT[0] = "Total(US$)";
        int GrossSum = 0;
        for (int i = 0; i < ddlShip.Items.Count; i++)
        {
            if (i != 0)
            {
                sb.Append("<td style='font-size:10px;text-align:right'>" + FormatCurrency(ColumnSum[i - 1]) + "</td>");
                GrossSum += ColumnSum[i - 1];
                drPDFT[i] = FormatCurrency(ColumnSum[i - 1]);
            }
        }
        sb.Append("<td style='font-size:10px;text-align:right'>" + FormatCurrency(GrossSum) + "</td>");
        sb.Append("</tr>");

        drPDFT["Total"] = FormatCurrency(GrossSum);
        DtForPDR.Rows.Add(drPDFT);


        // PER DAY CALC
        DataRow drPDFpt = DtForPDR.NewRow();
        sb.Append("<tr style='background-color:#FFCC66'>");
        sb.Append("<td style='font-size:10px;text-align:right;'>Avg Daily Cost(US$)</td>");
        drPDFpt[0] = "Avg Daily Cost(US$)";
        //int GrossSum = 0;
        for (int i = 0; i < ddlShip.Items.Count; i++)
        {
            if (i != 0)
            {
                try
                {
                    sb.Append("<td style='font-size:10px;text-align:right'>" + FormatCurrency((ColumnSum[i - 1] / VesselDays[i - 1])) + "</td>");
                    drPDFpt[i] = FormatCurrency((ColumnSum[i - 1] / VesselDays[i - 1])).ToString();
                }
                catch (DivideByZeroException ex)
                {
                    sb.Append("<td style='font-size:10px;text-align:right'>0</td>");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                //GrossSum += ColumnSum[i - 1];
            }
        }
        sb.Append("<td style='font-size:10px;text-align:right'></td>");
        sb.Append("</tr>");
        DtForPDR.Rows.Add(drPDFpt);

        // DATA ROWS  - 2
        for (int i = 0; i <= dtAccts1.Rows.Count - 1; i++)
        {
            DataRow drPDF = DtForPDR.NewRow();
            int RowSum = 0;
            sb.Append("<tr>");
            sb.Append("<td style='text-align:left;'>" + dtAccts1.Rows[i][1].ToString() + "</td>");
            drPDF[0] = dtAccts1.Rows[i][1].ToString();
            for (int j = 0; j < ddlShip.Items.Count; j++)
            {
                if (j != 0)
                {
                    DataTable dtShip = Common.Execute_Procedures_Select_ByQuery("select budget from dbo.v_New_CurrYearBudgetHome where shipid='" + ddlShip.Items[j].Value + "' AND year=" + (DateTime.Today.Year - 1).ToString() + " and  midcatid=" + dtAccts1.Rows[i][0].ToString());
                    if (dtShip != null)
                    {
                        if (dtShip.Rows.Count > 0)
                        {
                            sb.Append("<td style='text-align:right'>" + FormatCurrency(dtShip.Rows[0][0]) + "</td>");
                            ColumnSum[j - 1] += Common.CastAsInt32(dtShip.Rows[0][0]);
                            RowSum += Common.CastAsInt32(dtShip.Rows[0][0]);
                            drPDF[j] = FormatCurrency(dtShip.Rows[0][0]);
                        }
                        else
                        {
                            sb.Append("<td></td>");
                        }
                    }
                    else
                    {
                        sb.Append("<td></td>");
                    }
                }
            }
            sb.Append("<td style='text-align:right'>" + FormatCurrency(RowSum) + "</td>");
            sb.Append("</tr>");
            drPDF["Total"] = FormatCurrency(RowSum);
            DtForPDR.Rows.Add(drPDF);

        }

        // GROSS TOTAL
        DataRow drPDFgt = DtForPDR.NewRow();
        sb.Append("<tr style='background-color:#FFCC66'>");
        sb.Append("<td style='font-size:10px;text-align:right;'>Gross Total(US$)</td>");
        drPDFgt[0] = "Gross Total(US$)";
        //int GrossSum=0;
        for (int i = 0; i < ddlShip.Items.Count; i++)
        {
            if (i != 0)
            {
                sb.Append("<td style='font-size:10px;text-align:right'>" + FormatCurrency(ColumnSum[i - 1]) + "</td>");
                GrossSum += ColumnSum[i - 1];
                drPDFgt[i] = FormatCurrency(ColumnSum[i - 1]).ToString();

            }
        }
        sb.Append("<td style='font-size:10px;text-align:right'>" + FormatCurrency(GrossSum) + "</td>");
        sb.Append("</tr>");
        //sb.Append(sbLast.ToString()); 
        sb.Append("</table>");

        drPDFgt["Total"] = FormatCurrency(GrossSum);
        DtForPDR.Rows.Add(drPDFgt);
        
        if (DtForPDR != null)
        {
            ExportToPDF(ddlCompany.SelectedItem.Text + " ( Budget - " + System.DateTime.Now.ToString("dd-MMM-yyyy") + " )", DtForPDR);
        }
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
            string foot_Txt = "";
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
            iTextSharp.text.Font fCapText_5 = FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.BOLD);
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

            if (File.Exists(PublishPath + "\\Budget_Report.pdf"))
            {
                File.Delete(PublishPath + "\\Budget_Report.pdf");
            }

            FileStream fs = new FileStream(PublishPath + "\\Budget_Report.pdf", FileMode.Create);
            byte[] bb = msReport.ToArray();
            fs.Write(bb, 0, bb.Length);
            fs.Flush();
            fs.Close();
        }
        catch (System.Exception ex)
        {

        }
    }
    //----------------------------------------------------------------------------------
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
}
    
    
