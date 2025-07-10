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
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using ShipSoft.CrewManager.Operational;

public partial class Reports_SafetyInsp_Report_WithoutImage : System.Web.UI.Page
{
    int intInspId=0, intChildTblId=0;
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblmessage.Text = "";
        try
        {
            intInspId = int.Parse(Page.Request.QueryString["InspId"].ToString());
        }
        catch { }
        if (!Page.IsPostBack)
        {
            showreport();
        }
        else
        {
            showreport();
        }
    }
    private void showreport()
    {
        string path = "";
        int srnum = 0, inspdueid=0;
        
        DataTable dt = SafetyInspectionReport.SelectSafetyInspectionReportDetails(intInspId);
        if (dt.Rows.Count > 0)
        {
            this.CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("RPT_SafetyInspection_WithoutImage.rpt"));
            rpt.SetDataSource(dt);

            rpt.SetParameterValue("@Header", dt.Rows[0]["InspName"].ToString() + " - " + dt.Rows[0]["PortPlace"].ToString() + " - " + dt.Rows[0]["InspDontDt"].ToString());
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
