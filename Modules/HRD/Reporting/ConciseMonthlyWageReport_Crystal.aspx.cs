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

public partial class Reporting_ConciseMonthlyWageReport_Crystal : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        this.lblMessage.Text = "";
        this.CrystalReportViewer1.Visible = true;
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 131);
        //==========
        string empno = Request.QueryString["EmpNo"];
        DateTime from = Convert.ToDateTime(Request.QueryString["From"]);
        DateTime to = Convert.ToDateTime(Request.QueryString["To"]);
        
        int crewid = 0;
        DataTable dt1;
        dt1 = Concise_MonthlyWages_Report.getConciseMonthlyWagesDetails(empno, from, to);
        if (dt1.Rows.Count > 0)
        {
          this.CrystalReportViewer1.Visible = true;
          
          CrystalReportViewer1.ReportSource = rpt;
          rpt.Load(Server.MapPath("ConciseMonthlyWagesCrystalReport.rpt"));
          rpt.SetDataSource(dt1);

          DataSet ds = Concise_MonthlyWages_Report.getComponentName();
          for (int d = 0; d < 10; d++)
          {
             rpt.SetParameterValue("@D" + Convert.ToString(d+1), ds.Tables[0].Rows[d][0].ToString());
          }

          DataTable dt2 = PrintCrewList.selectCompanyDetails();
          foreach (DataRow dr in dt2.Rows)
          {
              rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
          }
        }
        else
        {
            this.lblMessage.Text = "No Record Found.";
            this.CrystalReportViewer1.Visible = false;
        }            
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
