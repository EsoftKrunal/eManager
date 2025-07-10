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

public partial class BirthdayListContainer : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        string Month = "";
        string offCrew, Status, Recroff;
        string sql = "SELECT * FROM vw_CrewMembersList";
        string WhereClause = " Where 1=1 ";

        Status = Request.QueryString["CrewStatus"];
        Recroff = Request.QueryString["RecOff"];
        offCrew = Request.QueryString["OffRat"];

        Month = Request.QueryString["Month"];
        
        if(Status.Trim() != "") { WhereClause += " And CrewStatusId=" + Status; };
        if(Recroff.Trim() != "") { WhereClause +=" And RecruitmentOfficeId=" + Recroff; }
        if(offCrew.Trim() != "") { WhereClause +=" And OffCrew='" + offCrew + "'"; };

        WhereClause += " And month(DateOfBirth) =" + Month + " "; 
        
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 15);
        //==========
        this.CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("BirthdayListReport.rpt"));
        
        //Response.Write(sql + WhereClause + " order by month(DateOfBirth),day(dateofbirth) ");
        //return;
        DataTable dt = Budget.getTable(sql + WhereClause + " order by month(DateOfBirth),day(dateofbirth) ").Tables[0];
        rpt.SetDataSource(dt);
        DataTable dt3 = PrintCrewList.selectCompanyDetails();
        foreach (DataRow dr in dt3.Rows)
        {
            rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
            rpt.SetParameterValue("@HeaderText", "Crew Birthday List");
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
