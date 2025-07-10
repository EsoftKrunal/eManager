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

public partial class Reporting_TrainingReportSummary_Header : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 112);

        //==========
        showreport();
    }
    private void showreport()
    {
        DateTime fromdate, todate;
       
            fromdate = Convert.ToDateTime(Page.Request.QueryString["fromdate"].ToString());
        
            todate = Convert.ToDateTime(Page.Request.QueryString["todate"].ToString());
        
        DataTable dt = TrainingSummary.selectTrainingSummaryData(fromdate, todate);
        DataTable dt2 = PrintCrewList.selectCompanyDetails();
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("CrystalReportTrainingSummary.rpt"));
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
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
