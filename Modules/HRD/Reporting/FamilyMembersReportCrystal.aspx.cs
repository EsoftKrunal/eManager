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

public partial class Reporting_FamilyMembersReportCrystal : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()),120);
        //==========
        int agefrom = 0, ageto = 99, relation = 0, gender = 0, country = 0, status;
        string areacode = "";
        char nok = 'N';
        agefrom = Convert.ToInt32(Request.QueryString["AgeFrom"]);
        ageto = Convert.ToInt32(Request.QueryString["AgeTo"]);
        relation = Convert.ToInt32(Request.QueryString["Relation"]);
        gender = Convert.ToInt32(Request.QueryString["Gender"]);
        country = Convert.ToInt32(Request.QueryString["Country"]);
        areacode = Convert.ToString(Request.QueryString["AreaCode"]);
        nok = Convert.ToChar(Request.QueryString["NOK"]);
        status = Convert.ToInt32(Request.QueryString["Status"]);
        DataTable dt = CrewFamilyMemberReport.selectCrewFamilyMemberData(agefrom, ageto, relation,gender,country,areacode,nok,status);
        DataTable dt2 = PrintCrewList.selectCompanyDetails();
        CrystalReportViewer1.ReportSource = rpt;

        rpt.Load(Server.MapPath("FamilyMemberCrystalReport.rpt"));
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
