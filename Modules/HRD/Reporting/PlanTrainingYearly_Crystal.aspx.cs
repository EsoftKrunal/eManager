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

public partial class Reporting_PlanTrainingYearly_Crystal : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 92);
        //==========
        showreport();
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    public void showreport()
    {
        int year = 2000;
        if (Page.Request.QueryString.ToString() != "")
        {
            year = Convert.ToInt32(Request.QueryString["Year"]);
        }
        //if (txt_year.Text != "")
        //{
        //    year = Convert.ToInt32(txt_year.Text);
        //}
        DataTable dt = PlannedTrainingYearly.selectPlannedTrainingData(year);
        DataTable dt2 = PrintCrewList.selectCompanyDetails();
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("CrystalReportPlannedTraining.rpt"));
        rpt.SetDataSource(dt);

        foreach (DataRow dr in dt2.Rows)
        {
            rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
            //rpt.SetParameterValue("@Address", dr["Address"].ToString());
            //rpt.SetParameterValue("@TelePhone", dr["TelephoneNumber"].ToString());
            //rpt.SetParameterValue("@Fax", dr["Faxnumber"].ToString());
            //rpt.SetParameterValue("@RegistrationNo", dr["RegistrationNo"].ToString());
            //rpt.SetParameterValue("@Email", dr["Email1"].ToString());
            //rpt.SetParameterValue("@Website", dr["Website"].ToString());
        }
    }
}
