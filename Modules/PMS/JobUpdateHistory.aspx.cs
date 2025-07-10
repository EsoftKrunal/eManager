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

public partial class JobUpdateHistory : System.Web.UI.Page
{
    public string UserType
    {
        set { ViewState["UserType"] = value; }
        get { return ViewState["UserType"].ToString(); }
    }
    public string VesselCode
    {
        set { ViewState["VC"] = value; }
        get { return ViewState["VC"].ToString(); }
    }
    public int ComponentId
    {
        set { ViewState["CI"] = value; }
        get { return Common.CastAsInt32(ViewState["CI"]); }
    }
    public int JobId
    {
        set { ViewState["JI"] = value; }
        get { return Common.CastAsInt32(ViewState["JI"]); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------

        if (!Page.IsPostBack)
        {
            UserType = Session["UserType"].ToString();
            if (Request.QueryString["VC"] != null && Request.QueryString["CID"] != null && Request.QueryString["JID"] != null)
            {
                VesselCode = Request.QueryString["VC"].ToString();
                ComponentId = Common.CastAsInt32(Request.QueryString["CID"].ToString());
                JobId = Common.CastAsInt32(Request.QueryString["JID"].ToString());
                ShowCompJobDetails();
                ShowJobUpdateHistory();
            }
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvHistory');", true);
    }
    private void ShowCompJobDetails()
    {
        string strCompJobDetails = "SELECT (SELECT ShipName FROM Settings WHERE ShipCode = '" + VesselCode + "') AS VesselName, CM.ComponentCode,CM.ComponentName ,JM.JobCode,CJM.DescrSh AS JobName,JIM.IntervalName,VCJM.Interval FROM VSL_VesselComponentJobMaster VCJM " +
                                    "INNER JOIN ComponentMaster CM ON CM.ComponentId = VCJM.ComponentId " +
                                    "INNER JOIN ComponentsJobMapping CJM  ON VCJM.CompJobId = CJM.CompJobId " +
                                    "INNER JOIN JobMaster JM ON JM.JobId = CJM.JobId " +
                                    "INNER JOIN JobIntervalMaster JIM ON VCJM.IntervalId = JIM.IntervalId " +
                                    "WHERE VCJM.VesselCode = '" + VesselCode + "' AND VCJM.ComponentId =" + ComponentId + " AND VCJM.CompJobId=" + JobId + " ";
        DataTable dtCompJobDetails = Common.Execute_Procedures_Select_ByQuery(strCompJobDetails);
        lblJhVessel.Text = dtCompJobDetails.Rows[0]["VesselName"].ToString();
        lblJhComponent.Text = dtCompJobDetails.Rows[0]["ComponentCode"].ToString() + " - " + dtCompJobDetails.Rows[0]["ComponentName"].ToString();
        lblJhJob.Text = dtCompJobDetails.Rows[0]["JobCode"].ToString() + " - " + dtCompJobDetails.Rows[0]["JobName"].ToString();
        lblJhInterval.Text = dtCompJobDetails.Rows[0]["Interval"].ToString() + " - " + dtCompJobDetails.Rows[0]["IntervalName"].ToString();
       
    }
    private void ShowJobUpdateHistory()
    {
        string strJobHistorySQL = "(SELECT VesselCode,HistoryId,CASE [Action] WHEN 'R' THEN 'REPORT' WHEN 'P' THEN 'POSTPONE' WHEN 'C' THEN 'CANCEL' END AS [Action],CASE [Action] WHEN 'R' THEN REPLACE(Convert(Varchar, DoneDate,106),' ','-') WHEN 'P' THEN REPLACE(Convert(Varchar, PostPoneDate,106),' ','-') WHEN 'C' THEN REPLACE(Convert(Varchar, CancellationDate,106),' ','-') END AS ACTIONDATE,CASE [Action] WHEN 'R' THEN DoneHour ELSE '' END  AS DoneHour,CASE [Action] WHEN 'R' THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(JH.LastDueDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE DATEDiff(dd,JH.DoneDate,JH.LastDueDate) END ELSE '' END AS [Difference],CASE [Action] WHEN 'R' THEN RM.RankCode WHEN 'P' THEN RMP.RankCode WHEN 'C' THEN RMC.RankCode END AS DoneBy,CASE [Action] WHEN 'R' THEN DoneBy_Code WHEN 'P' THEN P_DoneBy_Code WHEN 'C' THEN C_DoneBy_Code END AS EmpNo,CASE [Action] WHEN 'R' THEN DoneBy_Name WHEN 'P' THEN P_DoneBy_Name WHEN 'C' THEN C_DoneBy_Name END AS EmpName,ISNULL(REPLACE(Convert(Varchar, LastDueDate,106),' ','-'),'') AS DueDate,CASE [Action] WHEN 'R' THEN LastDueHours ELSE '' END AS DueHour,CASE [Action] WHEN 'R' THEN CASE UpdateRemarks WHEN 1 THEN 'Planned Job' WHEN 2 THEN CASE WHEN LEN(Specify) > 45 THEN SUBSTRING(Specify,0,45) + '...' ELSE Specify END WHEN 3 THEN 'BREAK DOWN' END WHEN 'P' THEN CASE PostPoneReason WHEN 1 THEN 'Equipment in working condition' WHEN 2 THEN 'Waiting for spares' WHEN 3 THEN 'Dry docking' END WHEN 'C' THEN CASE WHEN LEN(CancellationReason)> 45 THEN SUBSTRING(CancellationReason,0,45) + '...' ELSE CancellationReason END END AS Remarks,[FileName],FileImage,ConditionAfter,Nextduedate FROM VSL_VesselJobUpdateHistory JH " +
                                  "LEFT JOIN Rank RM ON RM.RankId = JH.DoneBy " +
                                  "LEFT JOIN Rank RMP ON RMP.RankId = JH.P_DoneBy " +
                                  "LEFT JOIN Rank RMC ON RMC.RankId = JH.C_DoneBy " +
                                  "WHERE JH.VesselCode = '" + VesselCode + "' AND JH.ComponentId =" + ComponentId + " AND JH.CompJobId=" + JobId + " ) ORDER BY HistoryId DESC ";
                                  //"UNION ALL " +
                                  //"(SELECT VesselCode,HistoryId,'' AS [Action],getdate() AS ACTIONDATE, '' AS DoneHour,'' AS [Difference],'' AS DoneBy,'' AS EmpNo,'' AS EmpName,'' AS DueDate,'' AS DueHour,'BREAK DOWN' AS Remarks,'' AS [FileName],'' AS FileImage FROM  dbo.Vsl_DefectDetailsMaster WHERE VesselCode = 'LIG' AND ComponentId =84 AND  HistoryId = 0) ORDER BY HistoryId DESC "; 
        DataTable dtJobHistory = Common.Execute_Procedures_Select_ByQuery(strJobHistorySQL);
        if (dtJobHistory.Rows.Count > 0)
        {
            rptJobHistory.DataSource = dtJobHistory;
            rptJobHistory.DataBind();
        }
        else
        {
            rptJobHistory.DataSource = null;
            rptJobHistory.DataBind();
        }
    }
}
