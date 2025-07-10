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

public partial class Reporting_ListOfExpiringDocumentsCrystal : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //========== Code to check report printing authority
        string Status = Request.QueryString["Status"];
        string DocType = Request.QueryString["DocType"];
        string RecOff = Request.QueryString["RecOff"];

        string Filter = "Where " + ((Request.QueryString["Exp"].Trim()=="1")?"ExpiryDate < getdate()": "(ExpiryDate >= getdate() AND ExpiryDate <= DATEADD(d,+60,GETDATE()))");

        Filter = Filter + ((Status.Trim() == "") ? "" : " And CrewStatusId=" + Status);
        Filter = Filter + ((DocType.Trim() == "") ? "" : " And MainCat='" + DocType + "'");
        Filter = Filter + ((RecOff.Trim() == "") ? "" : " And RecruitmentOfficeId=" + RecOff);

        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 15);
        DataTable dt = Budget.getTable("SELECT * FROM vw_CREW_DOCUMENTS " + Filter).Tables[0];
        DataTable dt2 = PrintCrewList.selectCompanyDetails();
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("CrystalReportExpiringDocuments.rpt"));
        rpt.SetDataSource(dt);
        foreach (DataRow dr in dt2.Rows)
        {
            rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
