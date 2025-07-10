using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class Print : System.Web.UI.Page
{

    public int BidId
    {
        set { ViewState["BidId"] = value; }
        get { return int.Parse("0" + ViewState["BidId"]); }
    }
    public int PRID
    {
        set { ViewState["PRId"] = value; }
        get { return int.Parse("0" + ViewState["PRId"]); }
    }
    public int PRType
    {
        set { ViewState["PRType"] = value; }
        get { return int.Parse("0" + ViewState["PRType"]); }
    }

    #region Declarations
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        if (Page.Request.QueryString["BidID"] != null)
        {
            BidId = Convert.ToInt32(Page.Request.QueryString["BidID"]);
            ShowCrystalReportForRFQ();
        }
        if (Page.Request.QueryString["BidID2"] != null)
        {
            BidId = Convert.ToInt32(Page.Request.QueryString["BidID2"]);
            ShowCrystalReportForRFQ2();
        }
        else if (Page.Request.QueryString["PRID"] != null)
        {
            PRID = Convert.ToInt32(Page.Request.QueryString["PRID"]);
            PRType = Convert.ToInt32(Page.Request.QueryString["PRType"]);
            ShowCrystalReportForPR();
        }
        else if (Page.Request.QueryString["POID"] != null)
        {
            BidId = Convert.ToInt32(Page.Request.QueryString["POID"]);
            ShowCrystalReportForPO();
        }
        else if (Page.Request.QueryString["Comment"] != null)
        {
            ShowBudgetSummaryReport();
        }
        else if (Page.Request.QueryString["VessNum"] != null)
        {
            ShowActivityReport();
        }
        else if (Page.Request.QueryString["CYBudget"] != null)
        {
            ShowCurrentYearBudgetRPT();
        }
        else if (Page.Request.QueryString["YearBudget"] != null)
        {
            ShowYearBudgetRPT();
        }
        else if (Page.Request.QueryString["BudgetForeCast"] != null)
        {
            ShowBudgetForeCastRPT();
        }
        else if (Page.Request.QueryString["LumpSum"] != null)
        {
            ShowLumpSumReport();
        }
        else if (Page.Request.QueryString["SOAMonthQuery"] != null)
        {
            ShowSoaMonthReport();
        }
        else if (Page.Request.QueryString["SOAYTDQuery"] != null)
        {
            ShowSoaYTDReport();
        }
        else if (Page.Request.QueryString["SOASummary"] != null)
        {
            ShowSoaSummary();
        }
        else if (Page.Request.QueryString["BudgetSummary"] != null)
        {
            ShowBudgetSummaryReport();
        }
        else if (Page.Request.QueryString["ACVESSEL"] != null)
        {
            ACCOUNT_STATEMENT_VESSEL();
        }
        else if (Page.Request.QueryString["ACFLEET"] != null)
        {
            ACCOUNT_STATEMENT_FLEET();
        }
        else if (Page.Request.QueryString["VARVESSEL"] != null)
        {
            VARBUDGET_STATEMENT_VESSEL();
        }
        else if (Page.Request.QueryString["VARFLEET"] != null)
        {
            VARBUDGET_STATEMENT_FLEET();
        }
        else if (Page.Request.QueryString["VARACCOUNT"] != null)
        {
            VARBUDGET_STATEMENT_ACCOUNT();
        }
        else if (Page.Request.QueryString["DETACTRPT"] != null)
        {
            DETAIL_ACTIVITY_VESSEL();
        }
        else if (Page.Request.QueryString["APENTRIES"] != null)
        {
            SHOW_APENTRIES();
        }
        else if (Page.Request.QueryString["AccountMapping"] != null)
        {
            SHOW_AccountMapping();
        }
        else if (Page.Request.QueryString["Vendor"] != null)
        {
            ShowVendorReport();
        }
        else if (Page.Request.QueryString["CurrentYearProjection"] != null)
        {
            CurrentYearProjection();
        }
            
        //PRID
    }
    public void ShowVendorReport()
    {

        string Vendor = Page.Request.QueryString["Vendor"].ToString();
        string Port = Page.Request.QueryString["Port"].ToString();
        string Active = Page.Request.QueryString["Active"].ToString();
        string AppType = Page.Request.QueryString["AppType"].ToString();

        string sql = "SELECT top 100 tblSMDSuppliers.SupplierID, tblSMDSuppliers.SupplierName, tblSMDSuppliers.SupplierPort, replace(tblSMDSuppliers.SupplierTel,';','</br>')SupplierTel " +
           " , tblSMDSuppliers.SupplierFax, tblSMDSuppliers.SupplierContact, tblSMDSuppliers.Active, tblSMDSuppliers.ApprovalType, tblSMDSuppliers.ServiceType,  " +
           " replace(tblSMDSuppliers.SupplierEmail,';','</br>')SupplierEmail, tblSMDSuppliers.Preferred, tblSMDSuppliers.TravID, tblSMDSuppliers.MultiCurr, tblSMDSuppliers.SGD_ID,  " +
           " tblSMDSuppliers.USD_ID " +
           " FROM DBO.tblSMDSuppliers ";

        string WhereClause = " where 1=1 ";
        if (Vendor != "")
        {
            WhereClause = WhereClause + " and SupplierName ='" + Vendor + "'";
        }
        if (Port != "")
        {
            WhereClause = WhereClause + " and SupplierPort ='" + Port + "'";
        }
        if (Active != "0")
        {
            WhereClause = WhereClause + " and active=" + Active + "";
        }
        if (AppType != "")
        {
            WhereClause = WhereClause + " and ApprovalType ='" + AppType + "'";
        }
        DataTable DTVendor = Common.Execute_Procedures_Select_ByQuery(sql);

        CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
        //rpt.Load(Server.MapPath("~/Report/PrintRFQ.rpt"));
        rpt.Load(Server.MapPath("~/Report/Vendor.rpt"));
        rpt.SetDataSource(DTVendor);


    }
    public void ShowCrystalReportForRFQ()
    {

        Common.Set_Procedures("sp_NewPR_getRFQMasterByBidId");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(
            new MyParameter("@BidId ", BidId)
            );
        DataSet DsRFQMaster;
        DsRFQMaster = Common.Execute_Procedures_Select();

        //Bidexchrate
        decimal Bidexchrate = 0;
        if (DsRFQMaster != null)
        {
            if (DsRFQMaster.Tables[0].Rows.Count > 0)
            {
                Bidexchrate = Common.CastAsDecimal(DsRFQMaster.Tables[0].Rows[0]["Bidexchrate"]);
            }
        }

        decimal TotalLC = 0;
        decimal TotalGSTLC = 0;

        Common.Set_Procedures("sp_NewPR_getRFQDetailsByBidId");
        Common.Set_ParameterLength(2);
        Common.Set_Parameters(
            new MyParameter("@BidId ", BidId),
            new MyParameter("@ExchRate ", Bidexchrate)
            );
        DataSet DsRFQDetail;
        DsRFQDetail = Common.Execute_Procedures_Select();

        if (DsRFQDetail != null)
        {
            foreach (DataRow dr in DsRFQDetail.Tables[0].Rows)
            {
                TotalLC = Common.CastAsDecimal(TotalLC + Common.CastAsDecimal(dr["LCPoTotal"]));
                TotalGSTLC = Common.CastAsDecimal(TotalGSTLC + Common.CastAsDecimal(dr["GSTTaxAmtLC"]));
            }
        }

        TotalLC = TotalLC + TotalGSTLC  + Common.CastAsDecimal(DsRFQMaster.Tables[0].Rows[0]["ESTSHIPPINGFOR"]) - Common.CastAsDecimal(DsRFQMaster.Tables[0].Rows[0]["TotalDiscount"]);


        DataTable dtnew = new DataTable();
        dtnew.Load(DsRFQDetail.Tables[0].CreateDataReader());
        DsRFQMaster.Tables.Add(dtnew);

        CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
        //rpt.Load(Server.MapPath("~/Report/PrintRFQ.rpt"));
        rpt.Load(Server.MapPath("~/Report/PrintOfficeRFQ.rpt"));
        DsRFQMaster.Tables[0].TableName = "sp_NewPR_getRFQMasterByBidId;1";
        DsRFQMaster.Tables[1].TableName = "sp_NewPR_getRFQDetailsByBidId;1";
        rpt.SetDataSource(DsRFQMaster);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT SHIPNAME FROM VW_tblSMDVessels WHERE SHIPID='" + DsRFQMaster.Tables[0].Rows[0]["SHIPID"].ToString() + "'");
        string VslName = "";
        if (dt.Rows.Count > 0)
        {
            VslName = dt.Rows[0][0].ToString();
        }
        rpt.SetParameterValue("VSLName", VslName);
        rpt.SetParameterValue("QuoteTotal", TotalLC);


    }
    public void ShowCrystalReportForRFQ2()
    {

        Common.Set_Procedures("sp_NewPR_getRFQMasterByBidId");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(
            new MyParameter("@BidId ", BidId)
            );
        DataSet DsRFQMaster;
        DsRFQMaster = Common.Execute_Procedures_Select();

        //Bidexchrate
        decimal Bidexchrate = 0;
        if (DsRFQMaster != null)
        {
            if (DsRFQMaster.Tables[0].Rows.Count > 0)
            {
                Bidexchrate = Common.CastAsDecimal(DsRFQMaster.Tables[0].Rows[0]["Bidexchrate"]);
            }
        }

        decimal TotalLC = 0;
        decimal TotalGSTLC = 0;
        Common.Set_Procedures("sp_NewPR_getRFQDetailsByBidId");
        Common.Set_ParameterLength(2);
        Common.Set_Parameters(
            new MyParameter("@BidId ", BidId),
            new MyParameter("@ExchRate ", Bidexchrate)
            );
        DataSet DsRFQDetail;
        DsRFQDetail = Common.Execute_Procedures_Select();

        if (DsRFQDetail != null)
        {
            foreach (DataRow dr in DsRFQDetail.Tables[0].Rows)
            {
                TotalLC = Common.CastAsDecimal(TotalLC + Common.CastAsDecimal(dr["extpricelc"]));
                TotalGSTLC = Common.CastAsDecimal(TotalGSTLC + Common.CastAsDecimal(dr["GSTTaxAmtLC"]));
            }
        }

        TotalLC = TotalLC + TotalGSTLC  + Common.CastAsDecimal(DsRFQMaster.Tables[0].Rows[0]["ESTSHIPPINGFOR"])  - Common.CastAsDecimal(DsRFQMaster.Tables[0].Rows[0]["TotalDiscount"]);


        DataTable dtnew = new DataTable();
        dtnew.Load(DsRFQDetail.Tables[0].CreateDataReader());
        DsRFQMaster.Tables.Add(dtnew);

        CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
        //rpt.Load(Server.MapPath("~/Report/PrintRFQ.rpt"));
        rpt.Load(Server.MapPath("~/Modules/Purchase/Report/PrintOfficeRFQ2.rpt"));
        DsRFQMaster.Tables[0].TableName = "sp_NewPR_getRFQMasterByBidId;1";
        DsRFQMaster.Tables[1].TableName = "sp_NewPR_getRFQDetailsByBidId;1";
        rpt.SetDataSource(DsRFQMaster);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT SHIPNAME FROM VW_tblSMDVessels WHERE SHIPID='" + DsRFQMaster.Tables[0].Rows[0]["SHIPID"].ToString() + "'");
        string VslName = "";
        if (dt.Rows.Count > 0)
        {
            VslName = dt.Rows[0][0].ToString();
        }
        rpt.SetParameterValue("VSLName", VslName);
        rpt.SetParameterValue("QuoteTotal", TotalLC);
    }
    public void ShowCrystalReportForPO()
    {

        Common.Set_Procedures("sp_NewPR_getRFQMasterByBidId");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(
            new MyParameter("@BidId ", BidId)
            );
        DataSet DsRFQMaster;
        DsRFQMaster = Common.Execute_Procedures_Select();

        //Bidexchrate
        decimal Bidexchrate = 0;
       // string RFQNo = "";
        string POAccountCompany = "";
        string ReportName = "";
        if (DsRFQMaster != null)
        {
            if (DsRFQMaster.Tables[0].Rows.Count > 0)
            {
                Bidexchrate = Common.CastAsDecimal(DsRFQMaster.Tables[0].Rows[0]["Bidexchrate"]);
               // RFQNo = DsRFQMaster.Tables[0].Rows[0]["RFQNO"].ToString();
                if (!string.IsNullOrEmpty(DsRFQMaster.Tables[0].Rows[0]["POAccountCompany"].ToString()))
                {
                    POAccountCompany = DsRFQMaster.Tables[0].Rows[0]["POAccountCompany"].ToString();
                }
                if (!string.IsNullOrEmpty(DsRFQMaster.Tables[0].Rows[0]["ReportName"].ToString()))
                {
                    ReportName = DsRFQMaster.Tables[0].Rows[0]["ReportName"].ToString();
                }
            }
        }

        decimal TotalLC = 0;
        decimal TotalUSD = 0;
        decimal TotalGSTLC = 0;
        decimal TotalGSTUSD = 0;
        Common.Set_Procedures("sp_NewPR_getRFQDetailsByBidId_ProductAccepted");
        Common.Set_ParameterLength(2);
        Common.Set_Parameters(
            new MyParameter("@BidId ", BidId),
            new MyParameter("@ExchRate ", Bidexchrate)
            );
        DataSet DsRFQDetail;
        DsRFQDetail = Common.Execute_Procedures_Select();
        
        if (DsRFQDetail != null)
        {
            foreach (DataRow dr in DsRFQDetail.Tables[0].Rows)
            {
                TotalLC = Common.CastAsDecimal(TotalLC + Common.CastAsDecimal(dr["LCPoTotal"]));
                TotalUSD = Common.CastAsDecimal(TotalUSD + Common.CastAsDecimal(dr["UsdPoTotal"]));
                TotalGSTLC = Common.CastAsDecimal(TotalGSTLC + Common.CastAsDecimal(dr["GSTTaxAmtLC"]));
                TotalGSTUSD = Common.CastAsDecimal(TotalGSTUSD + Common.CastAsDecimal(dr["GSTTaxAmtLC"]));
            }
        }

        TotalLC = TotalLC + TotalGSTLC  + Common.CastAsDecimal(DsRFQMaster.Tables[0].Rows[0]["ESTSHIPPINGFOR"]) - Common.CastAsDecimal(DsRFQMaster.Tables[0].Rows[0]["TotalDiscount"]);
        TotalUSD = TotalUSD + TotalGSTUSD  + Common.CastAsDecimal(DsRFQMaster.Tables[0].Rows[0]["ESTSHIPPINGUSD"]) - Common.CastAsDecimal(DsRFQMaster.Tables[0].Rows[0]["TotalDiscountUSD"]);


        DataTable dtnew = new DataTable();
        dtnew.Load(DsRFQDetail.Tables[0].CreateDataReader());
        DsRFQMaster.Tables.Add(dtnew);

        CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
        string dbname = "";
        dbname = ConfigurationManager.AppSettings["DBName"].ToUpper().ToString();

        switch (dbname)
        {
            case "RPLEMANAGER":
                rpt.Load(Server.MapPath("~/Modules/Purchase/Report/PrintRFQForMailRPL.rpt"));
                break;
            case "NSIPLEMANAGER":
                rpt.Load(Server.MapPath("~/Modules/Purchase/Report/PrintRFQForMailNSIPL.rpt"));
                break;
            case "BRAVOEMANAGER":
                //rpt.Load(Server.MapPath("~/Modules/Purchase/Report/PrintRFQForMailBRAVO.rpt"));
                //break;
                string ReportPath = "~/Modules/Purchase/Report/" + ReportName + "";
                rpt.Load(Server.MapPath(ReportPath));
                break;
            default:
                rpt.Load(Server.MapPath("~/Modules/Purchase/Report/PrintRFQForMail.rpt"));
                break;
        }
        DsRFQMaster.Tables[0].TableName = "sp_NewPR_getRFQMasterByBidId;1";
        DsRFQMaster.Tables[1].TableName = "sp_NewPR_getRFQDetailsByBidId;1";
        rpt.SetDataSource(DsRFQMaster);
        string ImageURL = "";
        string Address = "";
        string company = "";
        string email = "";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT SHIPNAME FROM VW_tblSMDVessels WHERE SHIPID='" + DsRFQMaster.Tables[0].Rows[0]["SHIPID"].ToString() + "'");
        string VslName = "";
        if (dt.Rows.Count > 0)
        {
            VslName = dt.Rows[0][0].ToString();
        }
        DataTable dt1 = new DataTable();
        if (dbname == "BRAVOEMANAGER" || dbname == "NSIPLEMANAGER" )
        {
            string AccountCompany = "";
            string AccountComAddress = "";
            DataTable dt2 = new DataTable();
            if (!string.IsNullOrEmpty(POAccountCompany))
            {
                dt1 = Common.Execute_Procedures_Select_ByQuery("Select a.Address,a.[Company Name] As Company, a.ImageURL,a.Email from AccountCompanyHeader a with(nolock) where a.Company ='" + POAccountCompany.Trim() + "'");
                if (dt1.Rows.Count > 0)
                {
                    ImageURL = dt1.Rows[0]["ImageURL"].ToString();
                    Address = dt1.Rows[0]["Address"].ToString();
                    company = dt1.Rows[0]["Company"].ToString();
                    email = dt1.Rows[0]["Email"].ToString();
                }

                dt2 = Common.Execute_Procedures_Select_ByQuery("Select a.Address,a.[Company Name] As Company, a.ImageURL,a.Email from Accountcompany a with(nolock) Inner Join Vessel v with(nolock) on a.Company = v.AccontCompany where VesselCode ='" + DsRFQMaster.Tables[0].Rows[0]["SHIPID"].ToString() + "'");
                if (dt2.Rows.Count > 0)
                {
                    AccountCompany = dt2.Rows[0]["Company"].ToString();
                    AccountComAddress = dt2.Rows[0]["Address"].ToString();
                }
            }
            else
            {

                dt2 = Common.Execute_Procedures_Select_ByQuery("Select a.Address,a.[Company Name] As Company, a.ImageURL,a.Email from AccountCompanyHeader a with(nolock) where a.CompanyId = (Select top 1 v.POIssuingCompanyId from vessel v with(nolock) where v.VesselCode ='" + DsRFQMaster.Tables[0].Rows[0]["SHIPID"].ToString() + "')");
                if (dt2.Rows.Count > 0)
                {
                    AccountCompany = dt2.Rows[0]["Company"].ToString();
                    AccountComAddress = dt2.Rows[0]["Address"].ToString();
                    ImageURL = dt2.Rows[0]["ImageURL"].ToString();
                    Address = dt2.Rows[0]["Address"].ToString();
                    company = dt2.Rows[0]["Company"].ToString();
                    email = dt2.Rows[0]["Email"].ToString();
                }
            }
            string applicationurl = ConfigurationManager.AppSettings["ApplicationURL"].ToString();
            string ImageLocation = applicationurl + ImageURL;
            rpt.SetParameterValue("@BidId", BidId);
            rpt.SetParameterValue("@ExchRate", Bidexchrate);
            rpt.SetParameterValue("VSLName", VslName);
            rpt.SetParameterValue("QuoteTotal", TotalLC);
            rpt.SetParameterValue("ImageURL", ImageLocation);
            rpt.SetParameterValue("Address", Address);
            rpt.SetParameterValue("Company", company);
            rpt.SetParameterValue("Email", email);
            rpt.SetParameterValue("AccCompany", AccountCompany);
            rpt.SetParameterValue("AccComAddress", AccountComAddress);
            rpt.SetParameterValue("QuoteTotalUSD", TotalUSD);
        }
        else
        {

            dt1 = Common.Execute_Procedures_Select_ByQuery("Select a.Address,a.[Company Name] As Company, a.ImageURL,a.Email from AccountCompanyHeader a with(nolock) where a.CompanyId = (Select top 1 v.POIssuingCompanyId from vessel v with(nolock) where v.VesselCode ='" + DsRFQMaster.Tables[0].Rows[0]["SHIPID"].ToString() + "')");
            if (dt1.Rows.Count > 0)
            {
                ImageURL = dt1.Rows[0]["ImageURL"].ToString();
                Address = dt1.Rows[0]["Address"].ToString();
                company = dt1.Rows[0]["Company"].ToString();
                email = dt1.Rows[0]["Email"].ToString();
            }
            string applicationurl = ConfigurationManager.AppSettings["ApplicationURL"].ToString();
            string ImageLocation = applicationurl + ImageURL;
            //CrystalDecisions.CrystalReports.Engine.ReportDocument rptsubreport1 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            //rptsubreport1 = rpt.OpenSubreport("PrintRFQForMail2.rpt");

            //string ImagePath = HttpContext.Current.Server.MapPath(ImageURL);
            rpt.SetParameterValue("VSLName", VslName);
            rpt.SetParameterValue("QuoteTotal", TotalLC);
            rpt.SetParameterValue("ImageURL", ImageLocation);
            rpt.SetParameterValue("Address", Address);
            rpt.SetParameterValue("Company", company);
            rpt.SetParameterValue("Email", email);
        }
        //rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/EMANAGERBLOB/Purchase/TempPoFiles/" + RFQNo + ".pdf"));
        //CrystalDecisions.CrystalReports.Engine.ReportDocument rptsubreport1 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        //rptsubreport1 = rpt.OpenSubreport("PrintRFQForMail2.rpt");
        //rptsubreport1.SetDataSource(DsRFQMaster);

    }
    public void ShowCrystalReportForPR()
    {
        Common.Set_Procedures("sp_NewPR_getPRMaster");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(
            new MyParameter("@PRId ", PRID)
            );
        DataSet DsPRMaster;
        DsPRMaster = Common.Execute_Procedures_Select();


        Common.Set_Procedures("sp_NewPR_getPRDetail");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(
            new MyParameter("@PRId ", PRID)
            );
        DataSet DsPRDetail;
        DsPRDetail = Common.Execute_Procedures_Select();
        DataTable dtnew = new DataTable();
        dtnew.Load(DsPRDetail.Tables[0].CreateDataReader());
        DsPRMaster.Tables.Add(dtnew);

        CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("~/Modules/Purchase/Report/PRReport.rpt"));
        DsPRMaster.Tables[0].TableName = "sp_NewPR_getPRMaster;1";
        DsPRMaster.Tables[1].TableName = "sp_NewPR_getPRDetail;1";
        rpt.SetDataSource(DsPRMaster);
        if (PRType == 1)
        {
            rpt.SetParameterValue("@PRId", PRID);
            rpt.SetParameterValue("Header", "Store Purchase Request");
            rpt.SetParameterValue("Field1", "Issa/Impa");
        }
        else if (PRType == 2)
        {
            rpt.SetParameterValue("@PRId", PRID);
            rpt.SetParameterValue("Header", "Spare Purchase Request");
            rpt.SetParameterValue("Field1", "Part No");
        }
        else if (PRType == 3)
        {
            rpt.SetParameterValue("@PRId", PRID);
            rpt.SetParameterValue("Header", "Landed Goods Purchase Request");
            rpt.SetParameterValue("Field1", "Part No");
        }


    }
    public void ShowActivityReport()
    {
        //SP_NEW_GetactivityReport
        string sql = "Exec [dbo].SP_NEW_GetactivityReport " + Page.Request.QueryString["VessNum"].ToString() + ", '" + Page.Request.QueryString["CoCode"].ToString() + "'," + Page.Request.QueryString["Month"].ToString() + "," + Page.Request.QueryString["ToMonth"].ToString() + "," + Page.Request.QueryString["Year"].ToString() + "";
        DataTable DtActivity = Common.Execute_Procedures_Select_ByQuery(sql);

        CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
        //rpt.Load(Server.MapPath("~/Report/PrintRFQ.rpt"));
        rpt.Load(Server.MapPath("~/Modules/Purchase/Report/ActivityReport.rpt"));
        DtActivity.TableName = "SP_NEW_GetactivityReport;1";
        rpt.SetDataSource(DtActivity);
    }
    public void ShowBudgetForeCastRPT()
    {



        // Query String parameter 
        string Comp, vess, BType, StartDate, EndDate, Year, Days;
        Comp = Page.Request.QueryString["Comp"].ToString();
        vess = Page.Request.QueryString["Vessel"].ToString();
        BType = Page.Request.QueryString["BType"].ToString();
        string MajCatID = Page.Request.QueryString["MajCatID"].ToString();

        if (BType == "< All >")
            BType = "All";
        StartDate = Page.Request.QueryString["StartDate"].ToString();
        EndDate = Page.Request.QueryString["EndDate"].ToString();
        Year = Page.Request.QueryString["year"].ToString();
        Days = Page.Request.QueryString["YearDays"].ToString();

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

        CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
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

            if(Amt_5100>0)
                Amt_5100 = ProjectCommon.Get_ManningAmount(Comp.Substring(0, 3), vess.Substring(0, 3), DateTime.Today.Year);
        }
        //-------------------

        DataTable dtAccts = Common.Execute_Procedures_Select_ByQuery("select distinct midcatid,midcat,MajSeqNo,MidSeqNo from dbo.v_New_CurrYearBudgetHome WHERE MajCatId=6 ORDER BY MajSeqNo,MidSeqNo");
        DataTable dtAccts1 = Common.Execute_Procedures_Select_ByQuery("select distinct midcatid,midcat,MajSeqNo,MidSeqNo from dbo.v_New_CurrYearBudgetHome WHERE MajCatId<>6 And MidcatId not in (25,26) ORDER BY MajSeqNo,MidSeqNo");
        DataTable dtDays = Common.Execute_Procedures_Select_ByQuery("select top 1 Days From dbo.v_New_CurrYearBudgetHome Where shipid='" + vess.Substring(0, 3) + "' AND year=" + (DateTime.Today.Year).ToString() + " order by days desc");
        DataTable dtDaysLast = Common.Execute_Procedures_Select_ByQuery("select top 1 Days From dbo.v_New_CurrYearBudgetHome Where shipid='" + vess.Substring(0, 3) + "' AND year=" + (DateTime.Today.Year - 1).ToString() + " order by days desc");
        DataTable dtActCom_Proj = Common.Execute_Procedures_Select_ByQuery("EXEC [dbo].[fn_NEW_GETCMBUDGETACTUAL_MIDCATWISE_FORDATE] '" + Comp.Substring(0, 3) + "'," + (DateTime.Today.Month).ToString() + "," + (DateTime.Today.Year).ToString() + "," + (DateTime.Today.Day).ToString()  + ",'" + vess.Substring(0, 3) + "'");
//Response.Write("EXEC [dbo].[fn_NEW_GETCMBUDGETACTUAL_MIDCATWISE_FORDATE] '" + Comp.Substring(0, 3) + "'," + (DateTime.Today.Month).ToString() + "," + (DateTime.Today.Year).ToString() + "," + (DateTime.Today.Day).ToString()  + ",'" + vess.Substring(0, 3) + "'");        
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

if(start==5)
{
//	Response.Write(FormatCurrency(ActComm));
//	Response.Write(FormatCurrency(Projected));
//	Response.End();
}

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

        
        int DaysTillyearEnd = new DateTime(Common.CastAsInt32(Year)-1,12,31).Subtract(DateTime.Today).Days;
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
                decimal dddddd2= (Budget / DaysCntLast) * DaysCnt;
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
        rpt.SetParameterValue("Param" + start.ToString(), FormatCurrency(Math.Round(ColumnSum,0)), "BudgetForecastReport_Summary.rpt");
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
    public void ShowCurrentYearBudgetRPT()
    {
        // Query String parameter 
        string Comp, vess, BType, StartDate, EndDate, Year, Days;
        Comp = Page.Request.QueryString["Comp"].ToString();
        vess = Page.Request.QueryString["Vessel"].ToString();
        BType = Page.Request.QueryString["BType"].ToString();
        string MajCatID = Page.Request.QueryString["MajCatID"].ToString();


        if (BType == "< All >")
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

        CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;

        rpt.Load(Server.MapPath("~/Report/CurrentYearBudgetReport.rpt"));
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
    }
    public void ShowYearBudgetRPT()
    {
        // Query String parameter 
        string Comp, vess, BType, StartDate, EndDate, Year, Days;
        Comp = Page.Request.QueryString["Comp"].ToString();
        vess = Page.Request.QueryString["Vessel"].ToString();
        Year = Page.Request.QueryString["BudgetYear"].ToString();
        BType = "All";
        string MajCatID = "0";
        string sql1 = "select top 1 convert(varchar,vessstart,106) as vessstart,convert(varchar,vessend,106) as vessend,yeardays from dbo.v_BudgetForecastYear where COCODE='" + Comp.Substring(0, 3) + "' AND VESS='" + vess.Substring(0, 3) + "' AND forecastYEAR=" + Year + " order by vessstart desc";
        DataTable dt001 = Common.Execute_Procedures_Select_ByQuery(sql1);
        if (dt001.Rows.Count > 0)
        {
            StartDate = dt001.Rows[0][0].ToString();
            EndDate = dt001.Rows[0][1].ToString();
            Days = dt001.Rows[0][2].ToString();
        }
        else
            return;       

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

        CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;

        rpt.Load(Server.MapPath("~/Report/CurrentYearBudgetReport.rpt"));
        DtRpt.TableName = "vw_NewPR_GetCurrentYearBudgetRptStructure";
        rpt.SetDataSource(DtRpt);


        DataTable dtAccts = Common.Execute_Procedures_Select_ByQuery("select distinct midcatid,midcat,MajSeqNo,MidSeqNo from dbo.v_New_CurrYearBudgetHome WHERE MajCatId=6 ORDER BY MajSeqNo,MidSeqNo");
        DataTable dtAccts1 = Common.Execute_Procedures_Select_ByQuery("select * from ( select distinct midcatid,midcat,MajSeqNo,MidSeqNo from dbo.v_New_CurrYearBudgetHome WHERE MajCatId<>6 union select 26,'Pre-Delivery Mgmt Fees',550,171) A ORDER BY MajSeqNo,MidSeqNo");
        DataTable dtDays = Common.Execute_Procedures_Select_ByQuery("select top 1 Days From dbo.v_New_CurrYearBudgetHome Where shipid='" + vess.Substring(0, 3) + "' AND year=" + (Common.CastAsInt32(Year) - 1).ToString() + " order by days desc");
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
            DataTable dtShip = Common.Execute_Procedures_Select_ByQuery("select budget from dbo.v_New_CurrYearBudgetHome where shipid='" + vess.Substring(0, 3) + "' AND year=" + (Common.CastAsInt32(Year) - 1).ToString() + " and  midcatid=" + dtAccts.Rows[i][0].ToString());
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
            DataTable dtShip = Common.Execute_Procedures_Select_ByQuery("select budget from dbo.v_New_CurrYearBudgetHome where shipid='" + vess.Substring(0, 3) + "' AND year=" + (Common.CastAsInt32(Year) - 1).ToString() + " and  midcatid=" + dtAccts1.Rows[i][0].ToString());
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
    }
    
    public void ShowLumpSumReport()
    {
        Common.Set_Procedures("sp_NewPR_GetLumpSumReport");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(
            new MyParameter("@Company", Page.Request.QueryString["CoCode"].ToString()),
            new MyParameter("@Period", Page.Request.QueryString["Period"].ToString()),
            new MyParameter("@Year", Page.Request.QueryString["Year"].ToString())
            );
        DataSet DsPRMaster = new DataSet();
        DsPRMaster = Common.Execute_Procedures_Select();

        DataView dvLumpSum = DsPRMaster.Tables[0].DefaultView;
        dvLumpSum.RowFilter = "Shipid='" + Page.Request.QueryString["VessCode"].ToString() + "'";

        CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("~/Report/CrewingLumpSumReport.rpt"));
        DsPRMaster.Tables[0].TableName = "sp_NewPR_GetLumpSumReport;1";
        rpt.SetDataSource(dvLumpSum.ToTable());
        rpt.SetParameterValue("Company", Page.Request.QueryString["CompanyName"].ToString());
        rpt.SetParameterValue("Ship", Page.Request.QueryString["VesselName"].ToString());
        rpt.SetParameterValue("YearAndPeriod", Page.Request.QueryString["Year"].ToString() + " / Period " + Page.Request.QueryString["Period"].ToString());
    }
    public void ShowSoaMonthReport()
    {
        DataSet DsSoaMonth;
        int Year, Month;
        string VessCode, VessName, CompCode, CompName;

        string[] QueryStr = Page.Request.QueryString["SOAMonthQuery"].ToString().Split('`');
        Year = Common.CastAsInt32(QueryStr[0]);
        Month = Common.CastAsInt32(QueryStr[1]);

        CompCode = QueryStr[2].ToString();
        CompName = QueryStr[3].ToString();

        VessCode = QueryStr[4].ToString();
        VessName = QueryStr[5].ToString();


        Common.Set_Procedures("GetSoaMonthReport");
        Common.Set_ParameterLength(5);
        Common.Set_Parameters(
            new MyParameter("@Company ", CompCode),
            new MyParameter("@ShipID ", VessCode),
            new MyParameter("@Year ", Year),
            new MyParameter("@Period ", Month),
            new MyParameter("@ReportType ", "0")
            );
        DsSoaMonth = Common.Execute_Procedures_Select();

        CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
        //rpt.Load(Server.MapPath("~/Report/PrintRFQ.rpt"));
        rpt.Load(Server.MapPath("~/Report/SOAReportMonthly.rpt"));
        DsSoaMonth.Tables[0].TableName = "GetSoaMonthReport;1";
        rpt.SetDataSource(DsSoaMonth);

        rpt.SetParameterValue("Statement", "STATEMENT OF ACCOUNT FOR " + getStatement(Month, Year));
        rpt.SetParameterValue("DateStr", getRPTDate(Month, Year));
        rpt.SetParameterValue("DatePrevMonth", getRPTPrevDate(Month, Year));

    }
    public void ShowSoaYTDReport()
    {
        DataSet DsSoaMonth;
        int Year, Month;
        string VessCode, VessName, CompCode, CompName;

        string[] QueryStr = Page.Request.QueryString["SOAYTDQuery"].ToString().Split('`');
        Year = Common.CastAsInt32(QueryStr[0]);
        Month = Common.CastAsInt32(QueryStr[1]);

        CompCode = QueryStr[2].ToString();
        CompName = QueryStr[3].ToString();

        VessCode = QueryStr[4].ToString();
        VessName = QueryStr[5].ToString();


        Common.Set_Procedures("GetSoaMonthReport");
        Common.Set_ParameterLength(5);
        Common.Set_Parameters(
           new MyParameter("@Company ", CompCode),
           new MyParameter("@ShipID ", VessCode),
           new MyParameter("@Year ", Year),
           new MyParameter("@Period ", Month),
           new MyParameter("@ReportType ", "0")
           );
        DsSoaMonth = Common.Execute_Procedures_Select();

        CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
        //rpt.Load(Server.MapPath("~/Report/PrintRFQ.rpt"));
        rpt.Load(Server.MapPath("~/Report/SOAReportYTD.rpt"));
        DsSoaMonth.Tables[0].TableName = "GetSoaMonthReport;1";
        rpt.SetDataSource(DsSoaMonth);

        rpt.SetParameterValue("Statement", "YTD STATEMENT OF ACCOUNT FOR " + getStatement(Month, Year));
        rpt.SetParameterValue("DateStr", getRPTDate(Month, Year));
        rpt.SetParameterValue("DatePrevMonth", getRPTPrevDate(Month, Year));

    }
    public void ShowSoaSummary()
    {
        DataSet DsSoaMonth;
        int Year, Month;
        string VessCode, VessName, CompCode, CompName;

        string[] QueryStr = Page.Request.QueryString["SOASummary"].ToString().Split('`');
        Year = Common.CastAsInt32(QueryStr[0]);
        Month = Common.CastAsInt32(QueryStr[1]);

        CompCode = QueryStr[2].ToString();
        CompName = QueryStr[3].ToString();

        VessCode = QueryStr[4].ToString();
        VessName = QueryStr[5].ToString();


        Common.Set_Procedures("GetSoaReportSummary");
        Common.Set_ParameterLength(2);
        Common.Set_Parameters(
            new MyParameter("@rptYear", Year),
            new MyParameter("@rptPeriod", Month)
            );
        DsSoaMonth = Common.Execute_Procedures_Select();

        CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
        //rpt.Load(Server.MapPath("~/Report/PrintRFQ.rpt"));
        rpt.Load(Server.MapPath("~/Report/SoaSummary.rpt"));
        DsSoaMonth.Tables[0].TableName = "GetSoaReportSummary;1";
        rpt.SetDataSource(DsSoaMonth);

        rpt.SetParameterValue("Statement", "YTD STATEMENT OF ACCOUNT FOR " + getStatement(Month, Year).ToUpper());
        //rpt.SetParameterValue("Shipname", VessName);
        //rpt.SetParameterValue("Company", CompName);

        //rpt.SetParameterValue("DateStr", getRPTDate(Month, Year));
        //rpt.SetParameterValue("DatePrevMonth", getRPTPrevDate(Month, Year));

    }
    public void SHOW_APENTRIES()
    {
        DataSet DsSoaMonth;
        string VessCode, PrNo, RecordStatus;

        string[] QueryStr = Page.Request.QueryString["APENTRIES"].ToString().Split('`');
        VessCode = QueryStr[0];
        PrNo = QueryStr[1];
        RecordStatus = QueryStr[2];

        Common.Set_Procedures("sp_NewPR_GetApEntries");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(new MyParameter("@Vessel", VessCode),
            new MyParameter("@PrNo", PrNo),
            new MyParameter("@Mode", RecordStatus)
            );
        DsSoaMonth = Common.Execute_Procedures_Select();

        CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("~/Report/ApEntries.rpt"));
        rpt.SetDataSource(DsSoaMonth.Tables[0]);
    }

    //AccountMapping
    public void SHOW_AccountMapping()
    {
        string AccTypeText = Page.Request.QueryString["AccTypeText"].ToString();
        string AccDepText = Page.Request.QueryString["AccDepText"].ToString();

        string AccType = Page.Request.QueryString["AccType"].ToString();
        string AccDep = Page.Request.QueryString["AccDep"].ToString();

        string StrWhereClause = " where 1=1";
        if (AccType != "0")
            StrWhereClause = StrWhereClause + " and tblSMDDeptAccounts.PRType=" + AccType + "";
        if (AccDep != "0")
            StrWhereClause = StrWhereClause + " and tblSMDDeptAccounts.Dept='" + AccDep + "' ";

        //string sql = " SELECT Lk_tblSMDPRAccounts.AccountNumber " +
        //            " FROM  tblSMDDeptAccounts Lk_tblSMDDeptAccounts  " +
        //            " INNER JOIN 	dbo.sql_tblSMDPRDept Lk_tblSMDPRDept ON Lk_tblSMDDeptAccounts.Dept = Lk_tblSMDPRDept.Dept " +
        //            " INNER JOIN dbo.sql_tblSMDPRAccounts Lk_tblSMDPRAccounts ON Lk_tblSMDDeptAccounts.AccountID = Lk_tblSMDPRAccounts.AccountID " +
        //            " INNER JOIN dbo.sql_tblSMDPRTypes Lk_tblSMDPRTypes ON Lk_tblSMDDeptAccounts.PRType = Lk_tblSMDPRTypes.PrType " +

        //            " " + StrWhereClause;
        //" ORDER BY Lk_tblSMDPRDept.DeptName, Lk_tblSMDPRAccounts.AccountNumber; ";
        //DataTable DtSelectedAcc = Common.Execute_Procedures_Select_ByQuery(sql);
        //string sql1 = "select * from VW_AccountReport where AccountNumber in (" + sql + ")";

        //--------------------------------------------------------------------

        string sql = "";
        if (StrWhereClause == " where 1=1")
        {
            sql = "SELECT VW_AccountReport.* " +
               " FROM   " +
                " VW_AccountReport ";
        }
        else
        {
            sql = "SELECT VW_AccountReport.* " +
               " FROM   " +
                " VW_AccountReport where AccountID in (select AccountID from tblSMDDeptAccounts " + StrWhereClause + " )";
        }

        DataTable dtAccts = Common.Execute_Procedures_Select_ByQuery(sql);

        CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("~/Report/AccountMappingReport.rpt"));
        rpt.SetDataSource(dtAccts);
        rpt.SetParameterValue("Department", ((AccDepText == "< Select >") ? "All" : AccDepText));
        rpt.SetParameterValue("Type", ((AccTypeText == "< Select >") ? "All" : AccTypeText));
    }


    public void ShowBudgetSummaryReport()
    {
        int Year = 0, Month = 0, MajCatId;
        string CoCode, VSL, VessName;
        VSL = Request.QueryString["VessCode"];
        CoCode = Request.QueryString["CoCode"];
        Year = Common.CastAsInt32(Request.QueryString["year"]);
        Month = Common.CastAsInt32(Request.QueryString["Period"]);
        //MajCatId = 6;
        VessName = Request.QueryString["VesselName"];

        DataTable DTinner;
        string[] comments;
        string ModString = "";

        //string sql = "select '" + VessName + "' as ShipName,CommPer,CommMidID,(select majorcat from [dbo].tblAccountsMajor where majcatid=6) as ComMajName, " +
        //            "(select midcat from [dbo].tblAccountsMid where midcatid=tblBudgetLevelComments.CommMidID) as ComMidName, " +
        //            "Budget,Actual+Comm as Consumed,Comment  " +
        //            "from [dbo].tblBudgetLevelComments  " +
        //            "where CommYear=" + Year.ToString() + " AND COMMSHIPID='" + VSL + "' and CommCo='" + CoCode + "' and commPer=" + Month + " and CommMajID=" + MajCatId.ToString() + " order by (select midseqno from [dbo].tblAccountsMid where midcatid=tblBudgetLevelComments.CommMidID)";

        //   CHANGE IN SQL BECAUSE IT WAS SHOWING RECORD FOR MAJCATID=6 ONLY

        string sql = "select '" + VessName + "' as ShipName,CommPer,CommMidID,(select majorcat from [dbo].tblAccountsMajor where majcatid=tblBudgetLevelComments.CommMajID) as ComMajName, " +
                    "(select midcat from [dbo].tblAccountsMid where midcatid=tblBudgetLevelComments.CommMidID) as ComMidName, " +
                    "Budget,Actual+Comm as Consumed,Comment  " +
                    "from [dbo].tblBudgetLevelComments  " +
                    "where CommYear=" + Year.ToString() + " AND COMMSHIPID='" + VSL + "' and CommCo='" + CoCode + "' and commPer=" + Month + " order by (select midseqno from [dbo].tblAccountsMid where midcatid=tblBudgetLevelComments.CommMidID)";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        DataTable dt1 = new DataTable();
        dt1.Columns.Add(new DataColumn("PlainText", typeof(String)));
        dt1.Columns.Add(new DataColumn("RTFText", typeof(String)));
        dt1.Columns.Add(new DataColumn("HTMLText", typeof(String)));
        dt1.Rows.Add(dt1.NewRow());
        int MidCat = 0;
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ModString = "";
                    MidCat = Common.CastAsInt32(dr["CommMidID"]);
                    comments = dr["Comment"].ToString().Split('`');
                    DTinner = Common.Execute_Procedures_Select_ByQuery("SELECT distinct (select minorcat from VW_tblAccountsMinor b where b.mincatid=a.mincatid) as ChildAccounts FROM vw_sql_tblSMDPRAccounts a WHERE MidCatID =" + MidCat + "");

                    for (int i = 0; i < comments.Length; i++)
                    {
                        if (i == 0)
                        {
                            comments[i] = "</br><b>General Comments </b></br>" + comments[i].Replace("\n", "</br>") + "</br>";
                        }
                        else
                        {
                            if (comments[i].Trim() != "")
                            {
                                comments[i] = "</br> </br><b>" + DTinner.Rows[i - 1][0].ToString() + "</b></br>" + comments[i].Replace("\n", "</br>");
                            }
                        }

                    }
                    for (int i = 0; i < comments.Length; i++)
                    {
                        ModString = ModString + comments[i].ToString();
                    }
                    //dr["Comment"]="<STRONG>This </STRONG><U><FONT color='#ff3333'>is </FONT></U><EM>HTML </EM><FONT color='#3366cc' size='6'>Text!!!! </FONT>";
                    dt1.Rows[0][0] = "<html><strong>pankaj</strong></html>";
                    dt1.Rows[0][1] = "<html><strong>pankaj</strong></html>";
                    dt1.Rows[0][2] = "<html><strong>pankaj</strong></html>";
                    dr["Comment"] = ModString;

                }
            }
        }

        CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("~/Report/BudgetCommentReport.rpt"));
        dt.TableName = "Newreport;1";
        rpt.SetDataSource(dt);
        //rpt.SetParameterValue("VesselName", VessName);
        rpt.SetParameterValue("PYear", Year.ToString());
    }
    public string getStatement(int m, int y)
    {
        try
        {
            DateTime date = Convert.ToDateTime(m.ToString() + "/1" + "/" + y.ToString());
            string str = date.ToString("MMMM yyyy");
            return str;
        }
        catch (Exception e)
        {
            return e.ToString();
        }
    }
    public string getRPTDate(int m, int y)
    {
        try
        {
            DateTime date = Convert.ToDateTime(m.ToString() + "/1" + "/" + y.ToString());
            string str = date.ToString("MMMM yyyy");
            return str;
        }
        catch (Exception e)
        {
            return e.ToString();
        }
    }
    public string getRPTPrevDate(int m, int y)
    {
        try
        {
            DateTime date = Convert.ToDateTime(m.ToString() + "/1/" + y.ToString());
            date = date.AddDays(-1);
            string str = date.ToString("dd MMM yyyy");
            return str;
        }
        catch (Exception e)
        {
            return e.ToString();
        }
    }
    /* Publish Report - Preview */

    // STATEMENT OF ACCOUNT - BY VESSEL
    public void ACCOUNT_STATEMENT_VESSEL()
    {
        DataSet DsSoaMonth;
        int Year, Month;
        string VessCode, VessName, CompCode, CompName;

        string[] QueryStr = Page.Request.QueryString["ACVESSEL"].ToString().Split('`');

        Month = Common.CastAsInt32(QueryStr[0]);
        Year = Common.CastAsInt32(QueryStr[1]);
        CompCode = QueryStr[2].ToString();
        CompName = QueryStr[3].ToString();

        Common.Set_Procedures("GetSoaMonthReport");
        Common.Set_ParameterLength(5);
        Common.Set_Parameters(
           new MyParameter("@Company ", CompCode),
           new MyParameter("@ShipID ", "0"),
           new MyParameter("@Year ", Year),
           new MyParameter("@Period ", Month),
           new MyParameter("@ReportType ", "1")
           );
        DsSoaMonth = Common.Execute_Procedures_Select();

        CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
        //rpt.Load(Server.MapPath("~/Report/PrintRFQ.rpt"));
        rpt.Load(Server.MapPath("~/Report/SOAReportMonthly.rpt"));
        DsSoaMonth.Tables[0].TableName = "GetSoaMonthReport;1";
        rpt.SetDataSource(DsSoaMonth);

        rpt.SetParameterValue("Statement", "STATEMENT OF ACCOUNT FOR " + getStatement(Month, Year));
        rpt.SetParameterValue("DateStr", getRPTDate(Month, Year));
        rpt.SetParameterValue("DatePrevMonth", getRPTPrevDate(Month, Year));

    }
    // STATEMENT OF ACCOUNT - BY FLEET
    public void ACCOUNT_STATEMENT_FLEET()
    {
        DataSet DsSoaMonth;
        int Year, Month;
        string VessCode, VessName, CompCode, CompName;

        string[] QueryStr = Page.Request.QueryString["ACFLEET"].ToString().Split('`');
        Month = Common.CastAsInt32(QueryStr[0]);
        Year = Common.CastAsInt32(QueryStr[1]);

        CompCode = QueryStr[2].ToString();
        CompName = QueryStr[3].ToString();

        Common.Set_Procedures("GetSoaMonthReport");
        Common.Set_ParameterLength(5);
        Common.Set_Parameters(
           new MyParameter("@Company ", CompCode),
           new MyParameter("@ShipID ", "0"),
           new MyParameter("@Year ", Year),
           new MyParameter("@Period ", Month),
           new MyParameter("@ReportType ", "2")
           );
        DsSoaMonth = Common.Execute_Procedures_Select();

        CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
        //rpt.Load(Server.MapPath("~/Report/PrintRFQ.rpt"));
        rpt.Load(Server.MapPath("~/Report/SOAReportMonthly.rpt"));
        DsSoaMonth.Tables[0].TableName = "GetSoaMonthReport;1";
        rpt.SetDataSource(DsSoaMonth);

        rpt.SetParameterValue("Statement", "STATEMENT OF ACCOUNT FOR " + getStatement(Month, Year));
        rpt.SetParameterValue("DateStr", getRPTDate(Month, Year));
        rpt.SetParameterValue("DatePrevMonth", getRPTPrevDate(Month, Year));

    }
    // VARIANCE BUDGET REPORT - BY VESSEL
    public void VARBUDGET_STATEMENT_VESSEL()
    {
        DataSet DsSoaMonth;
        int Year, Month;
        string CompCode, CompName;

        string[] QueryStr = Page.Request.QueryString["VARVESSEL"].ToString().Split('`');
        Month = Common.CastAsInt32(QueryStr[0]);
        Year = Common.CastAsInt32(QueryStr[1]);

        CompCode = QueryStr[2].ToString();
        CompName = QueryStr[3].ToString();

        Common.Set_Procedures("POS_ExportVarianceReport_Vessel");
        Common.Set_ParameterLength(4);
        Common.Set_Parameters(
           new MyParameter("@COMPCODE", CompCode),
           new MyParameter("@MNTH", Month),
           new MyParameter("@YR", Year),
           new MyParameter("@VSLCODE", "")
           );
        DsSoaMonth = Common.Execute_Procedures_Select();
        DsSoaMonth.Tables[0].TableName = "getVarianceRepportStructure;1";

        CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
        //rpt.Load(Server.MapPath("~/Report/PrintRFQ.rpt"));
        rpt.Load(Server.MapPath("~/Report/Export_VarianceReportBudgetSummary.rpt"));
        rpt.SetDataSource(DsSoaMonth.Tables[0]);
        rpt.SetParameterValue("Header", "VARIANCE REPORT FOR " + ProjectCommon.GetMonthName(Month.ToString()).ToUpper() + " " + Year.ToString());
    }
    // VARIANCE BUDGET REPORT - BY FLEET
    public void VARBUDGET_STATEMENT_FLEET()
    {
        DataSet DsSoaMonth;
        int Year, Month;
        string CompCode, CompName;

        string[] QueryStr = Page.Request.QueryString["VARFLEET"].ToString().Split('`');
        Month = Common.CastAsInt32(QueryStr[0]);
        Year = Common.CastAsInt32(QueryStr[1]);

        CompCode = QueryStr[2].ToString();
        CompName = QueryStr[3].ToString();

        Common.Set_Procedures("POS_ExportVarianceReport_Vessel");
        Common.Set_ParameterLength(4);
        Common.Set_Parameters(
           new MyParameter("@COMPCODE ", CompCode),
           new MyParameter("@MNTH ", Month),
           new MyParameter("@YR ", Year),
           new MyParameter("@VSLCODE ", "ALL")
           );
        DsSoaMonth = Common.Execute_Procedures_Select();

        CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
        //rpt.Load(Server.MapPath("~/Report/PrintRFQ.rpt"));
        rpt.Load(Server.MapPath("~/Report/Export_VarianceReportBudgetSummary.rpt"));
        rpt.SetDataSource(DsSoaMonth.Tables[0]);
        rpt.SetParameterValue("Header", "VARIANCE REPORT FOR " + ProjectCommon.GetMonthName(Month.ToString()).ToUpper() + " " + Year.ToString());
    }
    // VARIANCE BUDGET REPORT - BY ACCOUNT
    public void VARBUDGET_STATEMENT_ACCOUNT()
    {
        DataSet DsSoaMonth;
        int Year, Month;
        string CompCode, CompName;

        string[] QueryStr = Page.Request.QueryString["VARACCOUNT"].ToString().Split('`');
        Month = Common.CastAsInt32(QueryStr[0]);
        Year = Common.CastAsInt32(QueryStr[1]);

        CompCode = QueryStr[2].ToString();
        CompName = QueryStr[3].ToString();

        Common.Set_Procedures("POS_ExportVarianceReport_Account");
        Common.Set_ParameterLength(4);
        Common.Set_Parameters(
           new MyParameter("@COMPCODE ", CompCode),
           new MyParameter("@MNTH ", Month),
           new MyParameter("@YR ", Year),
           new MyParameter("@VSLCODE ", "")
           );
        DsSoaMonth = Common.Execute_Procedures_Select();

        CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
        //rpt.Load(Server.MapPath("~/Report/PrintRFQ.rpt"));
        rpt.Load(Server.MapPath("~/Report/Export_VarianceReportAccountDetails.rpt"));
        rpt.SetDataSource(DsSoaMonth.Tables[0]);
        rpt.SetParameterValue("Header", "VARIANCE REPORT FOR " + ProjectCommon.GetMonthName(Month.ToString()).ToUpper() + " " + Year.ToString());
    }
    // DETAIL ACTIVITY REPORT - BY VESSEL
    public void DETAIL_ACTIVITY_VESSEL()
    {
        DataSet DsSoaMonth;
        int Year, Month;
        string CompCode, CompName;

        string[] QueryStr = Page.Request.QueryString["DETACTRPT"].ToString().Split('`');
        Month = Common.CastAsInt32(QueryStr[0]);
        Year = Common.CastAsInt32(QueryStr[1]);

        CompCode = QueryStr[2].ToString();
        CompName = QueryStr[3].ToString();

        Common.Set_Procedures("DBO.POS_DetailActivityReport_Vessel");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(
           new MyParameter("@cocode ", CompCode),
           new MyParameter("@Month ", Month),
           new MyParameter("@Year ", Year));
        DsSoaMonth = Common.Execute_Procedures_Select();

        CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
        //rpt.Load(Server.MapPath("~/Report/PrintRFQ.rpt"));
        rpt.Load(Server.MapPath("~/Report/ActivityReport.rpt"));
        rpt.SetDataSource(DsSoaMonth.Tables[0]);
    }

    // Current Year projection
    public void CurrentYearProjection()
    {
        DataTable dt = new DataTable();
         dt = (DataTable)Session["sDT"];
        
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("~/Report/CurrentYearProjection1.rpt"));
        rpt.SetDataSource(dt);
        rpt.SetParameterValue("cYear" ,DateTime.Today.Year);
        rpt.SetParameterValue("Company", Page.Request.QueryString["Company"]);
        rpt.SetParameterValue("Vessel", Page.Request.QueryString["Vessel"]);
        rpt.SetParameterValue("CMonth", ProjectCommon.GetMonthName(Page.Request.QueryString["Month"]));

        if (Page.Request.QueryString["Mode"] != null)
        {
            rpt.SetParameterValue("PublishedBy", "Published By : " + Session["FullName"].ToString());
            rpt.SetParameterValue("PublishedOn", "Published On : " + System.DateTime.Today.ToString("dd-MMM-yyyy"));
            

            string FileName = Page.Request.QueryString["VessCode"] + "_" + Page.Request.QueryString["Month"] +"_"+ DateTime.Today.Year.ToString() + ".pdf";
            Common.Set_Procedures("sp_InsertUpdateRuningCostProjection");
            Common.Set_ParameterLength(7);
            Common.Set_Parameters(
                    new MyParameter("@VesselCode", Page.Request.QueryString["VessCode"]),
                    new MyParameter("@VesselName", Page.Request.QueryString["Vessel"]),
                    new MyParameter("@PYear", DateTime.Today.Year),
                    new MyParameter("@PMonth", Page.Request.QueryString["Month"]),
                    new MyParameter("@FileName", FileName),
                    new MyParameter("@PublishedBy", Session["FullName"].ToString()),
                    new MyParameter("@PublishedOn",System.DateTime.Today.ToString("dd-MMM-yyyy"))
                );
            DataSet Ds=new DataSet();
            Boolean Res;
            Res = Common.Execute_Procedures_IUD(Ds);
            if (Res)
            {
                CrystalReportViewer1.Visible = false;
                //rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("Report/" + FileName));
                rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat,@"C:\inetpub\wwwroot\SHIPSOFT\MTMWEBSITE\OWNERPORTAL\MTMWeb\CYProjection\" + FileName);
                Page.RegisterStartupScript("", "<script> alert('File exported successfully.');window.close(); </script>");
            }

        }
        else
        {
            string sql = "select * from RuningCostProjection  WHERE VesselCode='" + Page.Request.QueryString["Vessel"].Substring(0, 3) + "' AND PYear=" + DateTime.Today.Year.ToString() + " AND PMonth=" + Page.Request.QueryString["Month"] + "";
            DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
            if (Dt.Rows.Count > 0)
            {
                rpt.SetParameterValue("PublishedBy", "Published By : " + Dt.Rows[0]["PublishedBy"].ToString());
                rpt.SetParameterValue("PublishedOn", "Published On : " + Convert.ToDateTime(Dt.Rows[0]["PublishedOn"]).ToString("dd-MMM-yyyy"));
            }
            else
            {
                rpt.SetParameterValue("PublishedBy", "");
                rpt.SetParameterValue("PublishedOn", "");
            }
            CrystalReportViewer1.Visible = true;
        }
    }
    public void ShowPublishedByOn()
    {
        
    }

    /* Publish Report */
    #region PageEvents
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    #endregion
}
