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

public partial class Reports_LTI_ReportCrystal : System.Web.UI.Page
{
    string strLTIVsl = "", strLTIVName = "";
    int intLTIYear = 0, intLTIVCount = 0;
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblmessage.Text = "";
        try
        {
            if (Page.Request.QueryString["LTIVSLID"].ToString() != "")
                strLTIVsl = Page.Request.QueryString["LTIVSLID"].ToString();
            if (Page.Request.QueryString["LTIYEAR"].ToString() != "")
                intLTIYear = int.Parse(Page.Request.QueryString["LTIYEAR"].ToString());
            if (Page.Request.QueryString["LTIYRNAME"].ToString() != "")
                strLTIVName = Page.Request.QueryString["LTIYRNAME"].ToString();
            if (Page.Request.QueryString["LTIVSLCNT"].ToString() != "")
                intLTIVCount = int.Parse(Page.Request.QueryString["LTIVSLCNT"].ToString());
        }
        catch { }
        Show_Report();
    }
    private void Show_Report()
    {
        try
        {
            DataTable dt1 = LTIReport.SelectLTIReportDetails(strLTIVsl, intLTIYear);
            if (dt1.Rows.Count > 0)
            {
                this.CrystalReportViewer1.Visible = true;

                CrystalReportViewer1.ReportSource = rpt;
                rpt.Load(Server.MapPath("RPT_LTIReport.rpt"));

                rpt.SetDataSource(dt1);

                rpt.SetParameterValue("@Header", "Lost Time Incidents - LTI REPORTING\nBased on OCIMF Criteria & Guidelines");
                rpt.SetParameterValue("@VslParameter", intLTIVCount.ToString());
                rpt.SetParameterValue("@YearParameter", strLTIVName);
            }
            else
            {
                lblmessage.Text = "No Record Found.";
                this.CrystalReportViewer1.Visible = false;
            }
        }
        catch { }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
