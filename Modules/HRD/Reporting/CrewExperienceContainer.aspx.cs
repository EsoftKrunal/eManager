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

public partial class Reporting_CrewExperienceContainer : System.Web.UI.Page
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
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()),84);
        //*******************
        selindex = int.Parse (Request.QueryString["selindex"]);
        crewid = int.Parse (Request.QueryString["crewid"]);
        this.CrystalReportViewer1.Visible = true;
        if (selindex == 0 || selindex == 1)
        {
            int radioid = 1;
            if (selindex == 0)
            {
                radioid = 1;
            }
            else if (selindex == 1)
            {
                radioid = 2;
            }
            DataTable dt1 = CrewExperienceReport.selectCrewExperienceDetailsData(crewid, radioid);
            DataTable dt2 = PrintCrewList.selectCompanyDetails();
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("CrystalReportCrewExperienceDetails.rpt"));
            rpt.SetDataSource(dt1);

            foreach (DataRow dr in dt2.Rows)
            {
                rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
            }
        }
        else if (selindex == 2 || selindex == 3)
        {
            int radioid = 1;
            if (selindex == 2)
            {
                radioid = 1;
            }
            else if (selindex == 3)
            {
                radioid = 2;
            }
            DataTable dt3 = CrewExperienceReport.selectCrewExperienceOwnerDetailsData(crewid, radioid);
            DataTable dt2 = PrintCrewList.selectCompanyDetails();
            CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("CrystalReportCrewExperienceOwnerDetails.rpt"));
            rpt.SetDataSource(dt3);

            foreach (DataRow dr in dt2.Rows)
            {
                rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
            }
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
