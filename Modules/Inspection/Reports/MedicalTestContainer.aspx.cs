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
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        string vessel =Request.QueryString["vessel"], docname =Request.QueryString["docname"], doctype = Request.QueryString["doctype"], fdt = Request.QueryString["fdt"], tdt = Request.QueryString["tdt"];
        docname = docname.Replace("@", "&");  
        CrystalReportViewer1.Visible = true;
        DataTable dt = MedicalTestDoneByPeriod.selectMedicalDetailsData(int.Parse(vessel), int.Parse(doctype), fdt, tdt);
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("MedicalTestReportByPeriod.rpt"));
        rpt.SetDataSource(dt);
        rpt.SetParameterValue("@Company", "M.T.M. SHIP MANAGEMENT PTE. LTD.");
        rpt.SetParameterValue("@Address", "VESSEL D&A TEST");
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
