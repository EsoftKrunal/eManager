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

public partial class Reports_NCRTracker_Crystal : System.Web.UI.Page
{
    int VesselId;
    string strNCRCat = "", strNCRFDate = "", strNCRTDate = "", strDueInDays = "", strStatus = "", strResponsiblity = "", strOverDue = "";
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        this.lblmessage.Text = "";
        try
        {
            VesselId = int.Parse(Page.Request.QueryString["NCRVslID"].ToString());
            strNCRCat = Page.Request.QueryString["NCRCatId"].ToString();
            strNCRFDate = Page.Request.QueryString["NCRFDt"].ToString();
            strNCRTDate = Page.Request.QueryString["NCRTDt"].ToString();
            strDueInDays = Page.Request.QueryString["NCRDueDays"].ToString();
            strStatus = Page.Request.QueryString["NCRStatus"].ToString();
            strResponsiblity = Page.Request.QueryString["NCRResp"].ToString();
            strOverDue = Page.Request.QueryString["NCROverDue"].ToString();
        }
        catch { }
        Show_Report();
    }
    private void Show_Report()
    {
        DataTable dt = NCR.SelectVesselWiseNCRDetails(VesselId, strNCRCat, strNCRFDate, strNCRTDate, strDueInDays, strStatus, strResponsiblity, strOverDue);
        if (dt.Rows.Count > 0)
        {
            this.CrystalReportViewer1.Visible = true;

            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("RPT_FR_NCRById.rpt"));

            rpt.SetDataSource(dt);
            rpt.SetParameterValue("@Header", dt.Rows[0]["VesselName"].ToString() + " - NCR List");
            rpt.SetParameterValue("@Header1", "Printed On : " + System.DateTime.Now.Date.ToString("dd-MMM-yyyy"));
            rpt.SetParameterValue("@TotalRecordCount", "Total Record : " + dt.Rows.Count.ToString());
            rpt.SetParameterValue("@Header2", "NON CONFORMANCE REPORT(NCR)/CORRECTIVE ACTION REQUEST(CAR) STATUS LOG");
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
