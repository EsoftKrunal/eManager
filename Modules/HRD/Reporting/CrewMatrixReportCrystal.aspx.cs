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

public partial class Reporting_CrewMatrixReportCrystal : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 84);
        //==========
        int crewid = Convert.ToInt32(Request.QueryString["CrewID"]);
        DataTable dtsub1 = CrewMatrixReports.selectCrewMatrix1Details(crewid);
        DataTable dtsub2 = CrewMatrixReports.selectCrewMatrix2Details(crewid);
        DataTable dtsub3 = CrewMatrixReports.selectCrewMatrix3Details(crewid);
        DataTable dt10 = CrewMatrixReports.selectheaderDetails(crewid);
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("CrewMatrix.rpt"));
        rpt.SetDataSource(dt10);
        rpt.Refresh();
        this.CrystalReportViewer1.Visible = true;
        CrystalDecisions.CrystalReports.Engine.ReportDocument rptsub1 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        rptsub1 = rpt.OpenSubreport("CrewMatrix1.rpt");
        rptsub1.SetDataSource(dtsub1);

        CrystalDecisions.CrystalReports.Engine.ReportDocument rptsub2 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        rptsub2 = rpt.OpenSubreport("CrewMatrix2.rpt");
        rptsub2.SetDataSource(dtsub2);

        CrystalDecisions.CrystalReports.Engine.ReportDocument rptsub3 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        rptsub3 = rpt.OpenSubreport("CrewMatrix3.rpt");
        rptsub3.SetDataSource(dtsub3);
       DataTable dt2 = PrintCrewList.selectCompanyDetails();
       foreach (DataRow dr in dt2.Rows)
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
