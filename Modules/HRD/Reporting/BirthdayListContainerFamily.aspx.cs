using System;
using System.Data;

public partial class Reporting_BirthdayListContainerFamily : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        string Month = "";
        string offCrew, Status, Recroff;
        string sql = "select * from  vw_FamilyBirthdayList";


        string WhereClause = " Where 1=1 ";

        Status = Request.QueryString["CrewStatus"];
        Recroff = Request.QueryString["RecOff"];
        offCrew = Request.QueryString["OffRat"];

        Month = Request.QueryString["Month"];

        if(Status.Trim() != "") { WhereClause += " And CrewStatusId=" + Status; };
        if(Recroff.Trim() != "") { WhereClause +=" And RecruitmentOfficeId=" + Recroff; }
        if(offCrew.Trim() != "") { WhereClause +=" And OffCrew='" + offCrew + "'"; };

        //if (fdt.Trim() != "") { WhereClause += " And DateOfBirth>='" + fdt + "'"; };
        //if (tdt.Trim() != "") { WhereClause += " And DateOfBirth<='" + tdt + "'"; };

        WhereClause += " And month(DateOfBirth)=" + Month + " "; 
        
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 15);
        //==========
        this.CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("BirthdayListReportFamily.rpt"));
        DataTable dt = Budget.getTable(sql + WhereClause + " order by month(DateOfBirth),day(dateofbirth) ").Tables[0];
        rpt.SetDataSource(dt);
        DataTable dt3 = PrintCrewList.selectCompanyDetails();
        foreach (DataRow dr in dt3.Rows)
        {
            rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
            rpt.SetParameterValue("@HeaderText", "Crew Family Birthday List");
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
