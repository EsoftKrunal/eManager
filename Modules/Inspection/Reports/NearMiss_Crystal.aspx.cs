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

public partial class Reports_NearMiss_Crystal : System.Web.UI.Page
{
    int NearMissId = 0;
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblmessage.Text = "";
        if (Page.Request.QueryString["NearId"].ToString() != "")
            NearMissId = int.Parse(Page.Request.QueryString["NearId"].ToString());
        ShowReport();
    }
    private void ShowReport()
    {
        DataTable dt = NearMissReport.SelectNearMissDetails(NearMissId);
        DataTable dt1 = NearMissReport.SelectNearMissImmCauseDetails(NearMissId);
        DataTable dt2 = NearMissReport.SelectNearMissRootCauseDetails(NearMissId);
        if (dt.Rows.Count > 0)
        {
            this.CrystalReportViewer1.Visible = true;

            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("RPT_NearMiss.rpt"));

            rpt.SetDataSource(dt);

            rpt.Refresh();
            this.CrystalReportViewer1.Visible = true;
            CrystalDecisions.CrystalReports.Engine.ReportDocument rptsub1 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            rptsub1 = rpt.OpenSubreport("RPT_NearMiss_ImmCause.rpt");
            rptsub1.SetDataSource(dt1);

            rpt.Refresh();
            this.CrystalReportViewer1.Visible = true;
            CrystalDecisions.CrystalReports.Engine.ReportDocument rptsub2 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            rptsub2 = rpt.OpenSubreport("RPT_NearMiss_RootCause.rpt");
            rptsub2.SetDataSource(dt2);

            rpt.SetParameterValue("@Header", "Near Miss Report");
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
