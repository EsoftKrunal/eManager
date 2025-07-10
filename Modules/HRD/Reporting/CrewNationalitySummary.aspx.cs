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

public partial class Reporting_CrewNationalitySummaryVessel : System.Web.UI.Page
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
        DataSet ds= new DataSet();
        DataTable d1,d2;
        DataTable dt1 = PrintCrewList.selectCompanyDetails();
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("CrewNationalitySummary.rpt"));
        d1 = cls_CrewNationalitySummary.get_Tables();
        ds.Tables.Add(d1);
        d1.TableName = "ReportCrewNationalitySummary1;1";
        rpt.SetDataSource(d1);
        
        d2 = cls_CrewNationalitySummary.get_Tables1();
        ds.Tables.Add(d2);
        d2.TableName = "ReportCrewNationalitySummary2;1";
        CrystalDecisions.CrystalReports.Engine.ReportDocument rptsub1 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        rptsub1 = rpt.OpenSubreport("CrewNationalitySummary1.rpt");
        rptsub1.SetDataSource(d2);

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
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
