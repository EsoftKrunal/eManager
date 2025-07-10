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

public partial class Reports_InspHistory_Crystal_Hist : System.Web.UI.Page
{
    string strInspId = "";
    int intOwnerId, intVesselId;
    string strFromDt = "";
    string strToDt = "";
    string strOwner = "";
    string strVessel = "";
    string strFrom = "";
    string strTo = "";
    string strVslId = "";
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        try
        {
            strInspId = Page.Request.QueryString["InspID"].ToString();
            intOwnerId = Convert.ToInt32(Page.Request.QueryString["OwnerID"].ToString());
            //intVesselId = Convert.ToInt32(Page.Request.QueryString["VesselID"].ToString());
            strVslId = Page.Request.QueryString["VesselID"].ToString();
            strFromDt = Page.Request.QueryString["FromDate"].ToString();
            strToDt = Page.Request.QueryString["ToDate"].ToString();
            strOwner = Page.Request.QueryString["OwnerName"].ToString();
            strVessel = Page.Request.QueryString["VesselName"].ToString();
            strFrom = Page.Request.QueryString["FromText"].ToString();
            strTo = Page.Request.QueryString["ToText"].ToString();
        }
        catch { }
        Show_Report();
    }
    private void Show_Report()
    {
        //DataTable dt = InspHistoryReport.SelectInspHistory_HistDetails(strInspId, intOwnerId, intVesselId, strFromDt, strToDt);
        DataTable dt = InspHistoryReport.SelectInspHistory_HistDetails(strInspId, intOwnerId, strVslId, strFromDt, strToDt);
        //DataTable dtsub1 = InspHistoryReport.SelectInspPlannedDetails(strInspId, intOwnerId, intVesselId, strFromDt, strToDt);
        DataTable dtsub1 = InspHistoryReport.SelectInspPlannedDetails(strInspId, intOwnerId, strVslId, strFromDt, strToDt);
        //DataTable dtsub2 = InspHistoryReport.SelectInspDoneButOpenDetails(strInspId, intOwnerId, intVesselId, strFromDt, strToDt);        
        DataTable dtsub2 = InspHistoryReport.SelectInspDoneButOpenDetails(strInspId, intOwnerId, strVslId, strFromDt, strToDt);        
        if (dt.Rows.Count > 0)
        {
            this.CrystalReportViewer1.Visible = true;

            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("RPT_InspHistPlanDone_Hist.rpt"));
            //rpt.SetDataSource(dt);

            CrystalDecisions.CrystalReports.Engine.ReportDocument rptsub1 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            rptsub1 = rpt.OpenSubreport("RPT_InspHistory_Hist.rpt");
            rptsub1.SetDataSource(dt);

            CrystalDecisions.CrystalReports.Engine.ReportDocument rptsub2 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            rptsub2 = rpt.OpenSubreport("RPT_InspPlanned.rpt");
            rptsub2.SetDataSource(dtsub1);

            CrystalDecisions.CrystalReports.Engine.ReportDocument rptsub3 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            rptsub3 = rpt.OpenSubreport("RPT_InspDoneButOpen.rpt");
            rptsub3.SetDataSource(dtsub2);

            rpt.SetParameterValue("@Header", "Owner : " + strOwner);
            rpt.SetParameterValue("@Header1", "As On : " + System.DateTime.Now.Date.ToString("dd-MMM-yyyy"));
            //if (strOwner != "All" && (strFrom == "" && strTo == ""))
            //    rpt.SetParameterValue("@Filters", "Owner : " + strOwner);
            //else if (strOwner != "All" || strFrom != "" || strTo != "")
            //    rpt.SetParameterValue("@Filters", "Owner : " + strOwner + "         From Date : " + strFrom + "         To Date : " + strTo);
            //else
            //    rpt.SetParameterValue("@Filters", "");
        }
        else
        {
            lblmessage.Text = "No Record Found.";
            this.CrystalReportViewer1.Visible = false;
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
