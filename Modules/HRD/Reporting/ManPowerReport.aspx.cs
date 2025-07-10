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

public partial class Reporting_ManPowerReport : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 103);
        //==========
        ShowData();
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    public void ShowData()
    {
        DataSet ds = new DataSet();
        DataTable d1, d2, d3;
        this.CrystalReportViewer1.Visible = true;
        
        rpt.Load(Server.MapPath("ManPowerReport.rpt"));
        DataTable dt1 = PrintCrewList.selectCompanyDetails();
        d1 = cls_ManPowerReport.Report_ManPower_Rank(Request.QueryString["OffCrew"]);
        d1.TableName = "Rank";
        d2 = cls_ManPowerReport.Report_ManPower_Country();
        d2.TableName = "Country";
        d3 = cls_ManPowerReport.Report_ManPower_CrewPersonalDetails(Convert.ToInt32(Request.QueryString["StatusID"]));
        d3.TableName = "CrewPersonalDetails";
        ds.Tables.Add(d1);
        ds.Tables.Add(d2);
        ds.Tables.Add(d3);

        rpt.SetDataSource(ds);
        CrystalReportViewer1.ReportSource = rpt;
            foreach (DataRow dr in dt1.Rows)
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
