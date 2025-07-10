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

public partial class Reporting_WagePayableCrystal : System.Web.UI.Page
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
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 102);
        //==========
        int Vesselid = Convert.ToInt32(Request.QueryString["VID"]);
        int month = Convert.ToInt32(Request.QueryString["Month"]);
        int year = Convert.ToInt32(Request.QueryString["Year"]);
        string monthname = Request.QueryString["MthName"];
        string yearvalue = Request.QueryString["YrValue"];
        
        DataTable dt1 = WagePayableCrew.selectWagesPayableToCrewData1(Vesselid, month, year);
        DataTable dt2 = PrintCrewList.selectCompanyDetails();
        if (dt1.Rows.Count > 0)
        {
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("WagePayableCrystalReport.rpt"));
            rpt.SetDataSource(dt1);
            DataSet ds = cls_SearchReliever.getMasterData("WageScaleComponents", "WageScaleComponentId", "ComponentName");
            for (int p = 0; p < 12; p++)
            {
                rpt.SetParameterValue("@P" + Convert.ToString(p+1),ds.Tables[0].Rows[p][1].ToString());
            }
            rpt.SetParameterValue("@Header", "Portage Bill for the Month :  " + monthname + "  & Year :  " + yearvalue);
            foreach (DataRow dr in dt2.Rows)
            {
                rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
            }
        }
        else
        {
            lblMessage.Text = "No records found.";
            this.CrystalReportViewer1.Visible = false;
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}