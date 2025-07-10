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

public partial class Reporting_MedicalTestContainer : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 15);
        //==========
        string vessel =Request.QueryString["vessel"], docname =Request.QueryString["docname"], doctype = Request.QueryString["doctype"], fdt = Request.QueryString["fdt"], tdt = Request.QueryString["tdt"];
        docname = docname.Replace("@", "&");  
        CrystalReportViewer1.Visible = true;
        DataTable dt = MedicalTestDoneByPeriod.selectMedicalDetailsData(int.Parse(vessel), int.Parse(doctype), fdt, tdt);
        DataTable dt2 = PrintCrewList.selectCompanyDetails();
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("MedicalTestReportByPeriod.rpt"));
        rpt.SetDataSource(dt);

        foreach (DataRow dr in dt2.Rows)
        {
            rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
            rpt.SetParameterValue("@Address", docname);
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
