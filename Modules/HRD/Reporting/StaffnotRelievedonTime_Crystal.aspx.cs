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

public partial class Reporting_StaffnotRelievedonTime_Crystal : System.Web.UI.Page
{
    DataTable dt1;
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 114);
        //*******************
        this.lblMessage.Text = "";
        show_report();   
    }
    private void show_report()
    {
        DateTime fd, td;
        fd = Convert.ToDateTime(Page.Request.QueryString["FromDate"]);
        td = Convert.ToDateTime(Page.Request.QueryString["ToDate"]);
        dt1 = StaffNotRelievedOnTime.selectstaffnotrelievedontimedetails(fd,td);

        if (dt1.Rows.Count > 0)
        {
            this.CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("StaffNotRelievedOnTime.rpt"));

            rpt.SetDataSource(dt1);

            DataTable dt = PrintCrewList.selectCompanyDetails();
            foreach (DataRow dr in dt.Rows)
            {
                rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
            }
        }
        else
        {
            this.lblMessage.Text = "No Records Found.";
            this.CrystalReportViewer1.Visible = false;
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
