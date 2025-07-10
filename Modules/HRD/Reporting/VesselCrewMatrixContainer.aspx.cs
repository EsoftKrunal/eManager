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

public partial class Reporting_VesselCrewMatrixContainer : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //========== Code to Check Report Printing Authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 15);
        //==========
        string vesselname = Request.QueryString["vname"];
        string matrixname = Request.QueryString["matname"];
        DataTable dtvcm = Vessel_Crew_Matrix.selectVesselCrewMatrixDetails(Convert.ToInt32(Request.QueryString["vid"]), Convert.ToInt32(Request.QueryString["matid"]));
        if (dtvcm.Rows.Count > 0)
        {
            this.CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("VesselCrewMatrix.rpt"));
            rpt.SetDataSource(dtvcm);
            rpt.SetParameterValue("@Header", "Vessel Crew Matrix for Vessel : " + vesselname + " and Matrix : " + matrixname);

            DataTable dt3 = PrintCrewList.selectCompanyDetails();
            foreach (DataRow dr in dt3.Rows)
            {
                rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
            }
        }
        else
        {
            this.CrystalReportViewer1.Visible = false;
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
