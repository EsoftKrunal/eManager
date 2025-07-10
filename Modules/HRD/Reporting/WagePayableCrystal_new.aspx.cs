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

public partial class Reporting_WagePayableCrystal_new : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()),33);
        //==========
        lblMessage.Visible = true;
        lblMessage.Text = "";        
       
        int vesselid = 0;
        int month = Convert.ToInt32(Request.QueryString["Month"]);
        int year = Convert.ToInt32(Request.QueryString["Year"]);
        int crewid = Convert.ToInt32(Request.QueryString["CrewId"]);
        string mnthname=Request.QueryString["MnthNm"];
        
        DataTable dt = MonthleWageToCrew.selectwage1(vesselid, month, year, crewid); ;

        if (dt.Rows.Count > 0)
        {
            CrystalReportViewer1.Visible = true;

            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("WagePayableCrystalReport.rpt"));
            rpt.SetDataSource(dt);

            DataSet ds = cls_SearchReliever.getMasterData("WageScaleComponents", "WageScaleComponentId", "ComponentName");
            for (int p = 0; p < 12; p++)
            {
                rpt.SetParameterValue("@P" + Convert.ToString(p + 1), ds.Tables[0].Rows[p][1].ToString());
            }
            rpt.SetParameterValue("@Header","Monthly Wage Pay to Crew for the Month : " + mnthname);
            DataTable dt1 = PrintCrewList.selectCompanyDetails();
            foreach (DataRow dr in dt1.Rows)
            {
                rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
            }
        }
        else
        {
            lblMessage.Text = "No Record Found";
            CrystalReportViewer1.Visible = false;
        }
    }
}
