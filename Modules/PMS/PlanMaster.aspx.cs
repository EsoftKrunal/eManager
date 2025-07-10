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

public partial class PlanMaster : System.Web.UI.Page
{
    public static string VesselCode = "";
    //public string VesselCode
    //{
    //    set { ViewState["VC"] = value; }
    //    get { return  ""+ ViewState["VC"].ToString(); }
    //}
    public string CompId
    {
        set { ViewState["CompId"] = value; }
        get { return ViewState["CompId"].ToString(); }
    }
    public string JobId
    {
        set { ViewState["JobId"] = value; }
        get { return ViewState["JobId"].ToString(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------

        if (!Page.IsPostBack)
        {
            VesselCode = "";
            if (Request.QueryString["VC"] != null && Request.QueryString["CID"] != null && Request.QueryString["JID"] != null)
            {
                lblPageTitle.Text = "Job Planning";
                //VesselCode = Request.QueryString["VC"].ToString();
                GetStringVesselCode();
                CompId = Request.QueryString["CID"].ToString();
                JobId = Request.QueryString["JID"].ToString();
                ShowDetails();
            }
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvScroll');", true);
    }
    public void GetStringVesselCode()
    {
        string[] VCode = Request.QueryString["VC"].ToString().Split(',');
        for (int i = 0; i <= VCode.Length - 1; i++)
        {
            VesselCode = VesselCode + "'" + VCode[i] + "'" + ",";
        }
        VesselCode = VesselCode.Remove(VesselCode.Length - 1);
    }
    public DataTable BindRanks()
    {
        DataTable dtRanks = new DataTable();
        string strSQL = "SELECT RankId,RankCode FROM Rank ORDER BY RankCode";
        dtRanks = Common.Execute_Procedures_Select_ByQuery(strSQL);
        return dtRanks;
    }
    //public void BindDepartments()
    //{
    //    DataTable dtDepartments = new DataTable();
    //    string strSQL = "SELECT 0 AS DeptId,'< SELECT >' AS DeptName UNION SELECT DeptId,DeptName FROM DeptMaster ORDER BY DeptName";
    //    dtDepartments = Common.Execute_Procedures_Select_ByQuery(strSQL);
    //    ddlDept.DataSource = dtDepartments;
    //    ddlDept.DataTextField = "DeptName";
    //    ddlDept.DataValueField = "DeptId";
    //    ddlDept.DataBind();
    //}
    public void ShowDetails()
    {
        //string strSQL = "SELECT CM.ComponentId,CM.ComponentCode,CM.ComponentName,CJM.CompJobId As JobId,JM.JobCode,CJM.DescrSh AS JobName,JIM.IntervalName ,VCJM.Interval,replace(convert(varchar(15),VCJM.NextDueDate,106),' ','-') As NextDueDate,VCJM.NextHour,CASE WHEN VCJM.NextDueDate > getdate() THEN 'DUE' ELSE 'OVER DUE' END AS DueStatus ,JIM.IntervalId ,RM.RankId,CJM.DeptId FROM JobMaster JM " +
        //                "INNER JOIN ComponentsJobMapping CJM  ON JM.JobId = CJM.JobId " +
        //                "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId " +
        //                "INNER JOIN VSL_VesselComponentJobMaster VCJM ON VCJM.VesselComponentId = CJM.ComponentId AND VCJM.JobId = CJM.CompJobId " +
        //                "INNER JOIN Rank RM ON RM.RankId = VCJM.AssignTo " +
        //                "INNER Join JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId " +
        string strSQL = "SELECT VCJM.VesselCode,CM.ComponentId,CM.ComponentCode,CM.ComponentName ,JM.JobCode,CJM.DescrSh AS JobName,VCJM.JobId,JIM.IntervalName ,RM.RankId,RM.RankCode,VCJM.Interval,replace(convert(varchar(15),VCJM.NextDueDate,106),' ','-') As NextDueDate,CASE WHEN VCJM.NextDueDate > getdate() THEN 'DUE' ELSE 'OVER DUE' END AS DueStatus ,CASE PM.WorkOrderStatus WHEN 1 THEN 'Issued' WHEN 2 THEN 'Completed' WHEN 3 THEN 'Postponed' WHEN 4 THEN 'Cancelled' ELSE '' END AS WorkOrderStatus, VCJM.NextHour,REPLACE(CONVERT(VARCHAR(15),VCJM.LastDone,106),' ','-') AS LastDone,VCJM.LastHour FROM JobMaster JM " +
                        "INNER JOIN ComponentsJobMapping CJM  ON JM.JobId = CJM.JobId " +
                        "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId " +
                        "INNER JOIN VSL_VesselComponentJobMaster VCJM ON VCJM.VesselComponentId = CJM.ComponentId AND VCJM.JobId = CJM.CompJobId " +
                        "INNER JOIN Rank RM ON RM.RankId = VCJM.AssignTo " +
                        "INNER Join JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId " +  
                        "LEFT JOIN VSL_PlanMaster PM ON PM.VesselCode = VCJM.VesselCode AND PM.ComponentId = VCJM.VesselComponentId AND PM.jobId = VCJM.JobId " +
                        "WHERE VCJM.VesselCode IN (" + VesselCode + ") AND VCJM.VesselComponentId IN (" + CompId + ") AND VCJM.JOBId IN (" + JobId + ")";
        DataTable dtDetails = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dtDetails.Rows.Count > 0)
        {
            //lblVesselCode.Text = VesselCode;
            //lblCompCode.Text = dtDetails.Rows[0]["ComponentCode"].ToString();
            //lblCompName.Text = dtDetails.Rows[0]["ComponentName"].ToString();
            //lblJobCode.Text = dtDetails.Rows[0]["JobCode"].ToString();
            //lblDescription.Text = dtDetails.Rows[0]["JobName"].ToString();
            //lblIntType.Text = dtDetails.Rows[0]["IntervalName"].ToString();
            //lblInterval.Text = dtDetails.Rows[0]["Interval"].ToString();
            //lblStatus.Text = dtDetails.Rows[0]["DueStatus"].ToString();
            //lblNextDueDt.Text = dtDetails.Rows[0]["NextDueDate"].ToString();
            //lblNextHour.Text = dtDetails.Rows[0]["NextHour"].ToString() == "0" ? "" : dtDetails.Rows[0]["NextHour"].ToString();
            //ddlDept.SelectedValue = dtDetails.Rows[0]["DeptId"].ToString();
            //ddlRank.SelectedValue = dtDetails.Rows[0]["RankId"].ToString();
            //hfdIntervalId.Value = dtDetails.Rows[0]["IntervalId"].ToString();
            rptPlanJobs.DataSource = dtDetails;
            rptPlanJobs.DataBind();
        }
        else
        {
            rptPlanJobs.DataSource = null;
            rptPlanJobs.DataBind();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem rptItem in rptPlanJobs.Items)
        {
            HiddenField hfCompId = (HiddenField)rptItem.FindControl("hfCompId");
            HiddenField hfjobId = (HiddenField)rptItem.FindControl("hfjobId");
            HiddenField hfVesselCode = (HiddenField)rptItem.FindControl("hfVesselCode");
            DropDownList ddlAssignTO = (DropDownList)rptItem.FindControl("ddlAssignTO");
            TextBox txtForDate = (TextBox)rptItem.FindControl("txtForDate");

            string strSQL = "SELECT ISNULL(MAX(PlanId),0)+1 AS PlanId FROM VSL_PlanMaster ";
            DataTable dtPlanId = Common.Execute_Procedures_Select_ByQuery(strSQL);
            int NextPlanId = Common.CastAsInt32(dtPlanId.Rows[0]["PlanId"].ToString());
            //string strInsertSQL = "INSERT INTO VSL_PlanMaster VALUES (" + NextPlanId + ",'" + hfVesselCode.Value + "'," + hfCompId.Value + "," + hfjobId.Value + "," + ddlAssignTO.SelectedValue + ",1,replace(Convert(varchar(15),getDate(),106),' ','-')); SELECT -1 ";
            string strInsertSQL = "INSERT INTO VSL_PlanMaster VALUES (" + NextPlanId + ",'" + hfVesselCode.Value + "'," + hfCompId.Value + "," + hfjobId.Value + "," + ddlAssignTO.SelectedValue + ",1,'" + txtForDate.Text.Trim() + "'); SELECT -1 ";
            DataTable dtInsert = Common.Execute_Procedures_Select_ByQuery(strInsertSQL);
            if (dtInsert.Rows.Count > 0)
            {
                MessageBox1.ShowMessage("Plan saved successfully.", false);
            }
            else
            {
                MessageBox1.ShowMessage("Unable to save plan.", true);
            }
        }

    }
}
