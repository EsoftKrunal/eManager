using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class CrewApproval_CrewAssessmentPrint : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        int CrewContactBonusId = Common.CastAsInt32(Request.QueryString["CCBId"].ToString());
        ShowReport(CrewContactBonusId);
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    protected void ShowReport(int CrewContactBonusId)
    {
        string SQL = "SELECT * FROM [dbo].[vw_CrewContactBonus_Print] WHERE CREWBONUSID =" + CrewContactBonusId + " Order By VESSELNAME,CREWNUMBER";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);

        string SQL1 = "SELECT * FROM [dbo].[vw_CrewContactBonusCalculation_Print] WHERE CREWBONUSID =" + CrewContactBonusId;
        DataTable dt1 = Common.Execute_Procedures_Select_ByQueryCMS(SQL1);

        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("CrewAssessmentPrint.rpt"));
        rpt.SetDataSource(dt);

        rpt.Subreports[0].SetDataSource(dt1);

        //rpt.Refresh();

    }
}