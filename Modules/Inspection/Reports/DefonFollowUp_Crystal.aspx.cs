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

public partial class Reports_DefonFollowUp_Crystal : System.Web.UI.Page
{
    int InspId;
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        this.lblmessage.Text = "";
        try
        {
            InspId = int.Parse(Page.Request.QueryString["InspDueID"].ToString());
        }
        catch { }
        Show_Report();
    }
    private void Show_Report()
    {
        DataTable dt = DefonFollowUp.SelectDefFollowUpDetails(InspId);
        if (dt.Rows.Count > 0)
        {
            this.CrystalReportViewer1.Visible = true;

            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("RPT_DefFollowUp.rpt"));

            rpt.SetDataSource(dt);

            rpt.SetParameterValue("@InspectionName", "Inspection Name :  " + dt.Rows[0]["InspName"].ToString());
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
