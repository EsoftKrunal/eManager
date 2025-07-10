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

public partial class Print_PrintCrystal : System.Web.UI.Page
{
    #region Declarations
    public string ReportType
    {
        set { ViewState["ReportType"] = value; }
        get { return ViewState["ReportType"].ToString(); }
    }
    public string ComponentName
    {
        set { ViewState["ComponentName"] = value; }
        get { return ViewState["ComponentName"].ToString(); }
    }
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------

        if (Page.Request.QueryString["ComponentName"] != null)
            ComponentName = Page.Request.QueryString["ComponentName"].ToString();


        if (Page.Request.QueryString["ReportType"] != null)
        {
            ReportType = Page.Request.QueryString["ReportType"].ToString();
            switch (ReportType )
            {
                case "JobPlanning":
                    ShowJobPlanning();
                break;
                case "RuningHour":
                    string From = Page.Request.QueryString["FD"].ToString().Trim();
                    string TODate = Page.Request.QueryString["TD"].ToString().Trim();
                    ShowRuningHour(From, TODate);
                break;
                case "ShipMasterJobs":
                ShowShipMasterJobs();
                break;
                case "Spare":
                ShowSpare();
                break;
                case "OfficeMasterJobs":
                ShowOfficeMasterJobs();
                break;
                default :
                break;
            }
        }
    }
    public void ShowJobPlanning()
    {
        DataTable dtJP = Common.Execute_Procedures_Select_ByQuery(Session["sSqlForPrint"].ToString().Replace("CASE WHEN LEN(CJM.DescrSh) > 15 THEN Substring(CJM.DescrSh,0,15) + '...' ELSE CJM.DescrSh END AS JobName", "CJM.DescrSh AS JobName"));
        CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("~/Modules/PMS/Reports/JobPlanning.rpt"));
        rpt.SetDataSource(dtJP);
        //rpt.SetParameterValue("VSLName", "dgsdfgsd");
    }
    public void ShowRuningHour(string FromDate,string ToDate)
    {
        DataTable dtVessel;
        string ShipCode = Session["sSqlForPrint"].ToString();
        if (Session["UserType"].ToString() == "S")
        {
            dtVessel = Common.Execute_Procedures_Select_ByQuery("Select Shipname from Settings where ShipCode='" + ShipCode + "'");
        }
        else
        {
            dtVessel = Common.Execute_Procedures_Select_ByQuery("Select VesselName from Vessel where VesselCode='" + ShipCode + "'");
        }
        string Filter = " where VesselCode='" + Session["sSqlForPrint"].ToString() + "' ";
        if (FromDate != "" && ToDate != "")
        {
            Filter += " and StartDate>='" + FromDate + "' and StartDate<='" + ToDate + "' ";
        }
        else if (FromDate != "")
        {
            Filter += " and  StartDate>='" + FromDate + "'";
        }
        else if (ToDate != "")
        {
            Filter += " and StartDate<='" + ToDate + "'";
        }

        DataTable dtRH = Common.Execute_Procedures_Select_ByQuery("select * from vw_PrintRunningHourHistory "+Filter+" order by ComponentCode asc ,startDate asc");
     
        CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("~/Modules/PMS/Reports/RunningHour.rpt"));
        rpt.SetDataSource(dtRH);
        if (dtVessel.Rows.Count>0)
            rpt.SetParameterValue("VesselName", dtVessel.Rows[0][0].ToString());
        else
        rpt.SetParameterValue("VesselName", "");
    }
    public void ShowShipMasterJobs()
    {
        DataTable dtRH = Common.Execute_Procedures_Select_ByQuery(Session["sSqlForPrint"].ToString());
        CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("~/Modules/PMS/Reports/ShipMasterJobs.rpt"));
        rpt.SetDataSource(dtRH);
        //rpt.SetParameterValue("Component", ComponentName);
    }
    public void ShowOfficeMasterJobs()
    {
        DataTable dtRH = Common.Execute_Procedures_Select_ByQuery(Session["sSqlForPrint"].ToString());
        CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("~/Modules/PMS/Reports/OfficeMasterJobs.rpt"));
        rpt.SetDataSource(dtRH);
        rpt.SetParameterValue("Component", ComponentName);
    }
    public void ShowSpare()
    {
        DataTable dtRH = Common.Execute_Procedures_Select_ByQuery(Session["sSqlForPrint"].ToString());
        CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("~/Modules/PMS/Reports/SpareReport.rpt"));
        rpt.SetDataSource(dtRH);
        rpt.SetParameterValue("ComponentName", ComponentName);
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
