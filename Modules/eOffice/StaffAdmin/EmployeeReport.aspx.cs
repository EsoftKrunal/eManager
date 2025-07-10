using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class emtm_StaffAdmin_EmployeeReport : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("../../Reporting/EmployeeReport.rpt"));
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(Session["EmplyeeReport"].ToString());
        rpt.SetDataSource(dt);
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}