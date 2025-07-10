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

public partial class Reporting_FleetAgeProContainer : System.Web.UI.Page
{
    int crewid = 0;
    int selindex = 0;
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 177);
        string header;
        DataSet ds = new DataSet();
        this.CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("FleetAge.rpt"));
        ds = Budget.getTable("exec dbo.Report_FleetAge '" + Request.QueryString["offcrew"] + "'," + Request.QueryString["status"] + "," + Request.QueryString["recoff"]);
        rpt.SetDataSource(ds.Tables[0]);
        DataTable dt1 = PrintCrewList.selectCompanyDetails();
        foreach (DataRow dr in dt1.Rows)
        {
            rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
        }
        rpt.SetParameterValue("@HeaderText", "Fleet Age Profile Report");
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
