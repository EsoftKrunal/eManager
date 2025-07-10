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

public partial class Reporting_DateWisePromotedContainer : System.Web.UI.Page
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
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 109);
        //==========
        DataTable dt = OfficerRejoin.selectPromotedMember(Convert.ToDateTime(Request.QueryString["fdt"]), Convert.ToDateTime(Request.QueryString["tdt"]));
        this.CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("DateWisePromotedOfficerList.rpt"));

        Session.Add("rptsource", dt);
        rpt.SetDataSource(dt);

        DataTable dt1 = PrintCrewList.selectCompanyDetails();
        foreach (DataRow dr in dt1.Rows)
        {
            rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
