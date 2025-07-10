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

public partial class Reporting_CrewOffStatusContainer : System.Web.UI.Page
{
    int crewid = 0;
    int selindex = 0;
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 110);
        //==========
        int NationalityId;
        DataTable dt;
        this.CrystalReportViewer1.Visible = true;
        NationalityId = Convert.ToInt32(Request.QueryString["nat"]);
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("CrewOffStatus.rpt"));

        DataTable dt1 = PrintCrewList.selectCompanyDetails();
        dt = cls_CrewOffStatus.fn_CrewOffStatus(NationalityId, Convert.ToInt32(Request.QueryString["own"]));
        rpt.SetDataSource(dt);

        foreach (DataRow dr in dt1.Rows)
        {
            rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
        }
        rpt.SetParameterValue("@OnDate", DateTime.Today.ToString("dd-MMM-yyyy"));
        rpt.SetParameterValue("@HeaderText", "Status of Officers/Crew Onboard as on " + DateTime.Today.ToString("dd-MMM-yyyy") + " & Joiners in the month of " + DateTime.Today.ToString("MMM  yyyy"));
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
