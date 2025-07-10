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

public partial class Reporting_PrintCrewListCrystal2 : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()),15);
        //==========
        showdata();
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    private void showdata()
    {
        int vesselid = Convert.ToInt32(Request.QueryString["VesselId"]);
        DataTable dt = PrintCrewList.selectCompanyDetails();
        DataTable dt1 = PrintCrewList.selectCrewListDetailsWithExperience(vesselid);

        if (dt1.Rows.Count > 0)
        {
            this.CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("PrintCrewListWithExperience.rpt"));
            rpt.SetDataSource(dt1);
            rpt.SetParameterValue(0, Convert.ToInt32(Session["VesselId"].ToString()));
            foreach (DataRow dr in dt.Rows)
            {
                rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
            }
            rpt.SetParameterValue("@Header", "Crew List As On : " + DateTime.Today.ToString("dd-MMM-yyyy"));
        }
    }
}
