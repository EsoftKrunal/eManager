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

public partial class JobMaster : System.Web.UI.Page
{
    public int JobId
    {
        set { ViewState["SelectedJobId"] = value; }
        get { return Common.CastAsInt32(ViewState["SelectedJobId"]); }
    }
    //public int JobTypeId
    //{
    //    set { ViewState["SelectedJobCode"] = value; }
    //    get { return Common.CastAsInt32(ViewState["SelectedJobCode"]);}
    //}    
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------

        #region --------- USER RIGHTS MANAGEMENT -----------
        try
        {
            AuthenticationManager auth = new AuthenticationManager(206, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
            if (!(auth.IsView))
            {
                Response.Redirect("~/NoPermission.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/NoPermission.aspx?Message=" + ex.Message);
        }
        #endregion ----------------------------------------

        if (!Page.IsPostBack)
        {
            Session["CurrentModule"] = 4;            
            BindJobTypes();            
        }
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvscroll_JobMaster');", true);
    }
    #region ------------------ USER DEFINED FUNCTIONS ----------------------------
    private void BindJobTypes()
    {
        DataTable dtJobs;
        String strJobs;
        strJobs = "SELECT JobId,JobCode,JobName FROM JobMaster ORDER BY JobCode";
        dtJobs = Common.Execute_Procedures_Select_ByQuery(strJobs);
        rptItems.DataSource = dtJobs;
        rptItems.DataBind();
    }
    private void Bindjobs()
    {
        String StrJobDetails = "SELECT JM.JobId,JM.JobCode,JM.JobId,isnull(JM.IsCritical,'N') as IsCritical,RM.RankCode,DM.DeptName, CASE JIM.IntervalName WHEN 'H' THEN CASE JM.IsFixed WHEN 0 THEN JIM.IntervalName + ' - Fixed' ELSE JIM.IntervalName + ' - Flexible' END ELSE JIM.IntervalName END AS IntervalName,JM.IsFixed,JM.DescrSh,JM.DescrM FROM JobMaster JM " +
                               "LEFT JOIN DeptMaster DM ON DM.DeptId = JM.DeptId " +
                               "LEFT JOIN Rank RM ON RM.RankId = JM.AssignTo " +
                               "LEFT JOIN JobIntervalMaster JIM ON JIM.IntervalId = JM.IntervalId " +
                               "WHERE JM.JobId =" + JobId.ToString();
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(StrJobDetails);
        if (dt.Rows.Count > 0)
        {
            rptJobs.DataSource = dt;
            rptJobs.DataBind();
        }
        else
        {
            rptJobs.DataSource = null;
            rptJobs.DataBind();
        }
    }
    #endregion

    #region ------------------- EVENTS -------------------------------------------    
    protected void btnJobName_Click(object sender, EventArgs e)
    {
        JobId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        String StrJobDetails = "SELECT JM.JobId,JM.JobCode,JM.JobId,RM.RankCode,DM.DeptName,CASE JIM.IntervalName WHEN 'H' THEN CASE CJM.IsFixed WHEN 0 THEN JIM.IntervalName + ' - Fixed' ELSE JIM.IntervalName + ' - Flexible' END ELSE JIM.IntervalName END AS IntervalName,CJM.IsFixed,CJM.DescrSh,CJM.DescrM,isnull(CJM.IsCritical,'N') as IsCritical FROM JobMaster JM " +
                               "LEFT JOIN ComponentsJobMapping CJM ON JM.JobId = CJM.JobId " +
                               "LEFT JOIN DeptMaster DM ON DM.DeptId = CJM.DeptId " +
                               "LEFT JOIN Rank RM ON RM.RankId = CJM.AssignTo " +
                               "LEFT JOIN JobIntervalMaster JIM ON JIM.IntervalId = CJM.IntervalId " +
                               "WHERE JM.JobId =" + JobId.ToString();
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(StrJobDetails);
        if (dt.Rows.Count > 0)
        {
            rptJobs.DataSource = dt;
            rptJobs.DataBind();
        }
        else
        {
            rptJobs.DataSource = null;
            rptJobs.DataBind();
        }
        BindJobTypes();  
    }
    protected void btnJobCode_Click(object sender, EventArgs e)
    {
        JobId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        Bindjobs();
    }
    protected void btnAddComponents_Click(object sender, ImageClickEventArgs e)
    {
        if (JobId != 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Add", "openaddwindow('" + JobId + "');", true);
        }
        else
        {
            MessageBox1.ShowMessage("Please select job.", true);
        }

    }
    protected void btnEditComponents_Click(object sender, ImageClickEventArgs e)
    {
        if (JobId != 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Edit", "openeditwindow('" + JobId + "','" + JobId + "');", true);
        }
        else
        {
            MessageBox1.ShowMessage("Please select a job.", true);
        }
    }
    protected void btnView_Click(object sender, ImageClickEventArgs e)
    {
        int JobId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "View", "openwindow('" + JobId + "','" + JobId + "');", true);
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        Bindjobs();
    }
    //protected void btnDelete_Click(object sender, EventArgs e)
    //{
    //    Common.Set_Procedures("sp_Delete_Office_Master");
    //    Common.Set_ParameterLength(3);
    //    Common.Set_Parameters(
    //       new MyParameter("@Param_CompId", 0),
    //       new MyParameter("@Param_JobId", strJobId),
    //       new MyParameter("@ItemType", 1)           
    //       );
    //    DataSet dsJobs = new DataSet();
    //    dsJobs.Clear();
    //    Boolean res;
    //    res = Common.Execute_Procedures_IUD(dsJobs);
    //    if (res)
    //    {
    //        MessageBox1.ShowMessage("Job Deleted Successfully.", false);            
    //        btnAdd_Click(sender, e);
    //    }
    //    else
    //    {
    //        MessageBox1.ShowMessage("Unable to delete Job.", true);
    //    }

    //}   
    //protected void btnAdd_Click(object sender, EventArgs e)
    //{
    //    txtJobCode.Text = "";
    //    txtJobName.Text = "";        
    //    ddlJobType.SelectedIndex = 0;          
    //    strJobId = "";
    //}
    #endregion
}
