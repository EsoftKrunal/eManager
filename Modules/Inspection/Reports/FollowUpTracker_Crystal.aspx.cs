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

public partial class Reports_FollowUpTracker_Crystal : System.Web.UI.Page
{
    int VesselId;
    string strFollowUpCat = "", strFollowUpFDate = "", strFollowUpTDate = "", strDueInDays = "", strStatus = "", strCritical = "", strResponsiblity = "", strOverDue = "";
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        this.lblmessage.Text = "";
        try
        {
            VesselId = int.Parse(Page.Request.QueryString["VslID"].ToString());
            strFollowUpCat = Page.Request.QueryString["FlpCatId"].ToString();
            strFollowUpFDate = Page.Request.QueryString["FlpFDt"].ToString();
            strFollowUpTDate = Page.Request.QueryString["FlpTDt"].ToString();
            strDueInDays = Page.Request.QueryString["FlpDueDays"].ToString();
            strStatus = Page.Request.QueryString["FlpStatus"].ToString();
            strCritical = Page.Request.QueryString["FlpCritical"].ToString();
            strResponsiblity = Page.Request.QueryString["FlpResp"].ToString();
            strOverDue = Page.Request.QueryString["FlpOverDue"].ToString();
        }
        catch { }
        Show_Report();
    }
    private void Show_Report()
    {
        DataTable dt = FollowUp_Tracker.GetFollowUpDetailsById_Report(VesselId, strFollowUpCat, strFollowUpFDate, strFollowUpTDate, strDueInDays, strStatus, strCritical, strResponsiblity, strOverDue);
        if (dt.Rows.Count > 0)
        {
            this.CrystalReportViewer1.Visible = true;

            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("RPT_FR_FollowUpById.rpt"));

            rpt.SetDataSource(dt);
            rpt.SetParameterValue("@Header", dt.Rows[0]["VesselName"].ToString() + " - FollowUp List");
            rpt.SetParameterValue("@Header1", "Printed On : " + System.DateTime.Now.Date.ToString("dd-MMM-yyyy"));
            rpt.SetParameterValue("@TotalRecordCount", "Total Record : " + dt.Rows.Count.ToString());
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
