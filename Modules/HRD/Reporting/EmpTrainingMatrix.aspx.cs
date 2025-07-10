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

public partial class EmpTrainingMatrix : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    public int Year
    {
        get { return Convert.ToInt32("0" + ViewState["Year"].ToString()); }
        set { ViewState["Year"] = value.ToString(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!Page.IsPostBack)
        {
            Year = Common.CastAsInt32(Page.Request.QueryString["Year"]);
        }
        Showreport();
    }
    public void Showreport()
    {
        //string sql = " SELECT EPD.EmpID,TR.TRAININGID,TR.TrainingRecommID,A.TrainingPlanningID,EPD.EMPCODE,(ISNULL(EPD.FirstName,'')+ ' ' +ISNULL(EPD.FamilyName,'')) as EmpName,TM.TRAININGID,TM.TRAININGNAME,TR.DUEDATE,A.STARTDATE AS PLANSTART,A.ENDDATE AS PLANEND,A.STATUS,A.CANCELLEDON ,POS.PositionName, " +
        //    " (Case when (DUEDATE <=GETDATE() And (A.STATUS IS NULL OR A.STATUS='A' OR A.STATUS='C')) then 'OverDue' else ''end)IsOverDue," +
        //   " (CASE A.STATUS WHEN 'C' THEN 'Cancelled' WHEN 'E' THEN 'Completed' WHEN 'A' THEN 'Planned' ELSE 'Due'  END) StatusName , " +
        //   " (CASE A.STATUS WHEN 'C' THEN 'Cancelled' WHEN 'E' THEN 'Completed' WHEN 'A' THEN 'Planned' ELSE 'Due'  END) StatusCss , " +
        //   " A.STARTDATE1 AS CompStart,A.ENDDATE1 AS CompEnd " +
        //   " FROM dbo.HR_TrainingRecommended TR  " +
        //   " LEFT JOIN  " +
        //   "   ( " +
        //   "   SELECT PD.TRAININGPLANNINGID,PD.TRAININGRECOMMID,PM.STARTDATE,PM.ENDDATE,PM.STARTDATE1,PM.ENDDATE1,PM.CANCELLEDON,PM.STATUS FROM dbo.HR_TrainingPlanningDetails PD " +
        //   "   INNER JOIN dbo.HR_TrainingPlanning PM ON PM.TRAININGPLANNINGID=PD.TRAININGPLANNINGID   " +
        //   "   ) A ON TR.TRAININGRECOMMID=A.TRAININGRECOMMID " +
        //   "   INNER JOIN dbo.HR_TrainingMaster TM ON TM.TRAININGID=TR.TRAININGID " +
        //   "   INNER JOIN dbo.Hr_PersonalDetails EPD ON EPD.EMPID=TR.EMPID " +
        //   "   left JOIN dbo.HR_Department D ON D.DEPTID=EPD.DEPARTMENT " +
        //   "   left JOIN dbo.Position POS ON POS.PositionID=EPD.Position  " +
        //   "   Where Year(TR.DUEDATE)=" + Year.ToString();

        string sql = " SELECT * FROM vw_HR_TrainingMatrixData V WHERE Year(V.DUEDATE)=" + Year.ToString();
        DataSet DsMatrix = Budget.getTable(sql);
        
        //--------------------------------------
        this.CrystalReportViewer1.Visible = true;
        DsMatrix.Tables[0].TableName = "vw_HR_TrainingMatrixData";
        rpt.Load(Server.MapPath("EmtmTrainingMatrix.rpt"));
        CrystalReportViewer1.ReportSource = rpt;
        rpt.SetDataSource(DsMatrix);
        rpt.SetParameterValue("Year","Year : " +Year.ToString());
        DataTable dt1 = PrintCrewList.selectCompanyDetails();
    }
}
