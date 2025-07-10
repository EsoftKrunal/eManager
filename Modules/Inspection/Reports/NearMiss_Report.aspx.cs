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

public partial class Reports_NearMiss_Report : System.Web.UI.Page
{
    int intNMissId = 0;
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblmessage.Text = "";
        if (Page.Request.QueryString["NearMissId"].ToString() != "")
            intNMissId = int.Parse(Page.Request.QueryString["NearMissId"].ToString());
        if (!Page.IsPostBack)
            Show_Report();
        else
            Show_Report();
    }
    protected void Show_Report()
    {
        DataTable dt = NearMissReport.SelectNearMissDetails(intNMissId);
        DataTable dt1 = NearMissReport.SelectNearMissImmCauseDetails(intNMissId);
        DataTable dt2 = NearMissReport.SelectNearMissRootCauseDetails(intNMissId);
        DataTable dt3 = Budget.getTable("SELECT FORMNUMBER,FORMNAME FROM TBL_FORMID WHERE FORMID=1").Tables[0];
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

            //rpt.SetParameterValue("@Header", "NEAR MISS REPORT");
            if (dt3.Rows.Count > 0)
            {
                rpt.SetParameterValue("@Header", dt3.Rows[0]["FORMNAME"].ToString());
                rpt.SetParameterValue("@FormNumber", dt3.Rows[0]["FORMNUMBER"].ToString());
            }
            else
            {
                rpt.SetParameterValue("@Header", "NEAR MISS (HAZARDOUS OCCURRENCE) REPORT");
                rpt.SetParameterValue("@FormNumber", "");
            }
            rpt.SetParameterValue("@IdentifiedBy", dt.Rows[0]["IdentifiedByCrew"].ToString() + " - " + dt.Rows[0]["IdentifiedByName"].ToString());
            rpt.SetParameterValue("@MasterName", dt.Rows[0]["SignMasterCrew"].ToString() + " - " + dt.Rows[0]["SignMasterName"].ToString());
        }
        else
        {
            lblmessage.Text = "No Record Found.";
            this.CrystalReportViewer1.Visible = false;
        }
        //IFRAME1.Attributes.Add("src", "NearMiss_Crystal.aspx?NearId=" + intNMissId);
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
