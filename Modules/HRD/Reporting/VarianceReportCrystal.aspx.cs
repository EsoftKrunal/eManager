using System;
using System.Data;

using System.Web.UI;


public partial class Reporting_VarianceReportCrystal : System.Web.UI.Page
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
        showreport();
    }
    private void showreport()
    {
        try
        {
            lblMessage.Text = "";
            string Month1, Monthid,vesselname,ownername,budgetname="";
            int budgetyear, vesselid, ownerid, budgetid;

            decimal budget;
            budget = 0;
            
            vesselid = Convert.ToInt16(Page.Request.QueryString["VID"].ToString());
            ownerid = Convert.ToInt16(Page.Request.QueryString["OID"].ToString());
            budgetid = Convert.ToInt32(Page.Request.QueryString["BID"].ToString());
            budgetyear = System.DateTime.Today.Date.Year;
            Monthid = System.DateTime.Today.Date.Month.ToString();
            Month1 = System.DateTime.Today.Date.ToString("dd/MMM/yyyy").Substring(3, 3);
            vesselname=Page.Request.QueryString["VName"];
            ownername=Page.Request.QueryString["OName"];
            if (budgetid == 1)
            {
                budgetname = "Cost Plus";
            }
            else if (budgetid == 2)
            {
                budgetname = "CLS";
            }
            else
            {
                budgetname = "< Select >";
            }

            DataTable dt = VarianceReport.selectMonthlydata(ownerid, vesselid, budgetyear, Monthid, budgetid);
            
            if (dt.Rows.Count > 0)
            {
                this.CrystalReportViewer1.Visible = true;
                CrystalReportViewer1.ReportSource = rpt;
                rpt.Load(Server.MapPath("VarianceReport.rpt"));
                rpt.SetDataSource(dt);
                rpt.SetParameterValue("@MonthName",Month1);

                rpt.SetParameterValue("@Header", "Variance Report for Owner : " + ownername + " , Vessel : " + vesselname + " & Budget Type : " + budgetname);
       
                DataTable dt3 = PrintCrewList.selectCompanyDetails();
                foreach (DataRow dr in dt3.Rows)
                {
                    rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
                }
            }
            else
            {
                lblMessage.Text = "No Record To Show";
            }
        }
        catch(SystemException es)
        {

        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
