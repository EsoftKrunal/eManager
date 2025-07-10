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

public partial class Reporting_MonthlyWageContainer : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 101);
        //==========
        DataSet ds = MonthleWageToCrew.selectwage(int.Parse(Request.QueryString["crewid"]), int.Parse(Request.QueryString["month"]), int.Parse(Request.QueryString["year"]), Request.QueryString["monthname"]);
            if (ds.Tables[0].Rows.Count > 0)
            {
                CrystalReportViewer1.Visible = true;
                ds.Tables[0].TableName = "SelectMonthlyWagePayToCrew_new;1";
                ds.Tables[1].TableName = "command_1";
                ds.Tables[2].TableName = "command";
                DataTable dtearn = new DataTable();
               
                CrystalReportViewer1.ReportSource = rpt;
                rpt.Load(Server.MapPath("CrewMonthlyWageReport.rpt"));
                rpt.SetDataSource(ds.Tables[0]);

                CrystalDecisions.CrystalReports.Engine.ReportDocument rptsub1 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                rptsub1 = rpt.OpenSubreport("CrewEarning.rpt");
                rptsub1.SetDataSource(ds.Tables[1]);


                CrystalDecisions.CrystalReports.Engine.ReportDocument rptsub2 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                rptsub2 = rpt.OpenSubreport("Crewdeduction.rpt");
                rptsub2.SetDataSource(ds.Tables[2]);
                rpt.SetParameterValue("@EmpNo", Request.QueryString["crewnumber"]);


                DataTable dt1 = PrintCrewList.selectCompanyDetails();
                foreach (DataRow dr in dt1.Rows)
                {
                    rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
                }
            }
            else
            {
                CrystalReportViewer1.Visible = false;
            }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
