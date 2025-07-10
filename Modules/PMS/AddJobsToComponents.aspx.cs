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
using System.IO;
using System.Threading;

public partial class AddJobsToComponents : System.Web.UI.Page
{
    public int componentId
    {
        set { ViewState["CompId"] = value; }
        get { return Common.CastAsInt32(ViewState["CompId"]); }
    }
    public string compcode
    {
        set { ViewState["CompCode"] = value; }
        get { return ViewState["CompCode"].ToString(); }
    }
    public int CompJobId
    {
        set { ViewState["CompJobId"] = value; }
        get { return Common.CastAsInt32(ViewState["CompJobId"]); }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        #region --------- USER RIGHTS MANAGEMENT -----------
        try
        {
            AuthenticationManager auth = new AuthenticationManager(1043, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
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
            if (Request.QueryString["CompCode"] != null)
            {
                GetComponent();
                
                if (Request.QueryString["Type"] != null)
                {
                    BindRanks();
                    BindDepartments();
                    BindIntervalType();
                    BindJobTypes();
                    
                    //chkIsCritical.Enabled = false;
                    if (Request.QueryString["Type"].ToString() == "ADD")
                    {
                        lblPageTitle.Text = "Add Jobs to Component";
                        if (compcode.Trim().Length == 9)
                        {
                            trIntParent.Visible = true;
                        }
                        else
                        {
                            trIntParent.Visible = false;
                        }
                    }
                    if (Request.QueryString["Type"].ToString() == "EDIT" && Request.QueryString["JobId"] != null && Request.QueryString["CJId"] != null)
                    {
                        lblPageTitle.Text = "Edit Jobs to Component";
                        CompJobId = Common.CastAsInt32(Request.QueryString["CJId"].ToString());
                        BindDocRepeater();
                        ddlJobType.SelectedValue = Request.QueryString["JobId"].ToString();
                        //ddlJobType.Enabled = false;
                        ddlJobType_SelectedIndexChanged(sender, e);
                        trIntParent.Visible = false;
                    }
                }
            }
        }
    }

    #region ----------- USER DEFINED FUNCTIONS --------------------
    
    public void BindRanks()
    {

        DataTable dts = Common.Execute_Procedures_Select_ByQuery("select * from Rank with(nolock) where statusid = 'A' and OffGroup = 'D' and rankcode not in ('SUPT','SUPY')  order by ranklevel");
        chkDeckRank.DataSource = dts;
        chkDeckRank.DataTextField = "RANKCODE";
        chkDeckRank.DataValueField= "RANKID";
        chkDeckRank.DataBind();

        DataTable dtEngine = Common.Execute_Procedures_Select_ByQuery("select * from Rank with(nolock) where statusid = 'A' and OffGroup = 'E' and rankcode not in ('SUPT','SUPY')  order by ranklevel");
        chkEngineRank.DataSource = dtEngine;
        chkEngineRank.DataTextField = "RANKCODE";
        chkEngineRank.DataValueField = "RANKID";
        chkEngineRank.DataBind();

        DataTable dtRanks = new DataTable();
        string strSQL = "SELECT RankId,RankCode FROM Rank with(nolock) where statusid = 'A' and rankcode not in ('SUPT','SUPY') ORDER BY RankCode";
        dtRanks = Common.Execute_Procedures_Select_ByQuery(strSQL);
        ddlAssignTo.DataSource = dtRanks;
        ddlAssignTo.DataTextField = "RankCode";
        ddlAssignTo.DataValueField = "RankId";
        ddlAssignTo.DataBind();
        ddlAssignTo.Items.Insert(0, new ListItem("< SELECT >", "0"));
    }
    public void BindIntervalType()
    {
        DataTable dtIntervalType = new DataTable();
        string strSQL = "SELECT 0 AS IntervalId,'< SELECT >' AS IntervalName UNION SELECT IntervalId, IntervalName FROM JobIntervalMaster ORDER BY IntervalName";
        dtIntervalType = Common.Execute_Procedures_Select_ByQuery(strSQL);
        ddlIntervalType.DataSource = dtIntervalType;
        ddlIntervalType.DataTextField = "IntervalName";
        ddlIntervalType.DataValueField = "IntervalId";
        ddlIntervalType.DataBind();

    }
    public void BindDepartments()
    {
        DataTable dtDepartments = new DataTable();
        string strSQL = "SELECT 0 AS DeptId,'< SELECT >' AS DeptName UNION SELECT DeptId,DeptName FROM DeptMaster ORDER BY DeptName";
        dtDepartments = Common.Execute_Procedures_Select_ByQuery(strSQL);
        ddlDepartment.DataSource = dtDepartments;
        ddlDepartment.DataTextField = "DeptName";
        ddlDepartment.DataValueField = "DeptId";
        ddlDepartment.DataBind();
    }
    private void BindJobTypes()
    {
        string strSQL = "";
        if (Request.QueryString["Type"].ToString() == "ADD")
        {
            //strSQL = "SELECT JobId,JobCode,JobName FROM JobMaster WHERE JobId NOT IN (SELECT JobId FROM ComponentsJobMapping WHERE ComponentId = " + componentId + ")";
            strSQL = "SELECT JobId,JobCode,JobName FROM JobMaster ";
        }
        else
        {
            strSQL = "SELECT JobId,JobCode,JobName FROM JobMaster ";
        }
        
        DataTable dtJobTypes = Common.Execute_Procedures_Select_ByQuery(strSQL);
        ddlJobType.DataSource = dtJobTypes;
        ddlJobType.DataTextField = "JobName";
        ddlJobType.DataValueField = "JobId";
        ddlJobType.DataBind();
        ddlJobType.Items.Insert(0,"< Select >");
    }
    private Boolean IsValidated()
    {
        if (ddlJobType.SelectedIndex == 0)
        {
            MessageBox1.ShowMessage("Please select a job category.", true);
            ddlJobType.Focus();
            return false;
        }
        if (txtDescrSh.Text == "")
        {
            MessageBox1.ShowMessage("Please enter Short description.", true);
            txtDescrSh.Focus();
            return false;
        }
        if (txtDescrSh.Text.Trim().Length > 250)
        {
            MessageBox1.ShowMessage("Short description should not exceed 250 characters.", true);
            txtDescrSh.Focus();
            return false;
        }
        if (ddlDepartment.SelectedIndex == 0)
        {
            MessageBox1.ShowMessage("Please select department.", true);
            ddlDepartment.Focus();
            return false;
        }
        if (ddlAssignTo.SelectedIndex == 0)
        {
            MessageBox1.ShowMessage("Please select Rank.", true);
            ddlAssignTo.Focus();
            return false;
        }
        if (ddlIntervalType.SelectedIndex == 0)
        {
            MessageBox1.ShowMessage("Please select IntervalType.", true);
            ddlIntervalType.Focus();
            return false;
        }
        //if (ddlIntervalType.SelectedIndex == 2)
        //{
        //    if (rblFixed.SelectedIndex == -1)
        //    {
        //        MessageBox1.ShowMessage("Please select job schedule.", true);
        //        rblFixed.Focus();
        //        return false;
        //    }
        //}
        if (txtDescrM.Text.Trim().Length > 8000)
        {
            MessageBox1.ShowMessage("Long description should not exceed 8000 characters.", true);
            txtDescrM.Focus();
            return false;
        }
        return true;
    }

    public void BindDocRepeater()
    {
        string sql = "select row_number() over(order by TableId) as Sno,*, FileName As UpFileName from ComponentsJobMapping_attachments cj with(nolock) inner join SMS_FORMS sf with(nolock) on cj.FormId = sf.FormId where cj.CompJobID=" + CompJobId.ToString() + " and Status='A' order by TableId";
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rptFiles.DataSource = Dt;
        rptFiles.DataBind();
    }

    //protected void lnlViewVersion_OnClick(object sender, EventArgs e)
    //{
    //    LinkButton lnk = (LinkButton)sender;
    //    //dvFormVersion.Visible = true;
    //    //DataTable dt = Forms.getFormsList(lnk.CommandArgument);
    //    //rptFormsVersion.DataSource = dt;



    //    int FormId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
    //    string FormName = ((LinkButton)sender).ToolTip;
    //    DownloadAttachment(FormId, FormName);


    //    rptFiles.DataBind();
    //}
    protected void btnDownLoadFile_OnClick(object sender, EventArgs e)
    {
        int FormID = Common.CastAsInt32(hfFormID.Value);
        string FileName = hfFileName.Value;
        if (FormID > 0)
        {
            DownloadAttachment(FormID, FileName);
        }
    }
    public void DownloadAttachment(int FormId, string FileName)
    {
        try
        {
            string extension = Path.GetExtension(FileName).Substring(1);
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", FileName));
            Response.ContentType = "application/" + extension;
            Response.BinaryWrite(Forms.getFormAttachment(FormId));
            Response.End();
        }
        catch { }
    }

    //private void BindJobs()
    //{
    //    String strJobs;
    //    strJobs = "SELECT JM.JobId,JTM.JobTypeCode AS JobCode,JTM.JobTypeName AS JobName,0 AS RankId ,0 AS DeptId, 0 AS IntervalId, 0 AS IsFixed,'' As Descr,'ADD' AS TYPE FROM JOBMASTER JM " + 
    //              "INNER JOIN JobTypeMaster JTM ON JTM.JobTypeId = JM.JobTypeId " +
    //              "WHERE JobId NOT IN (SELECT JobId FROM ComponentsJobMapping WHERE ComponentId = " + componentId + " ) ORDER BY JobCode";
    //    DataTable dtJobs;
    //    dtJobs = Common.Execute_Procedures_Select_ByQuery(strJobs);
    //    rptJobs.DataSource = dtJobs;
    //    rptJobs.DataBind();
    //    if (!Page.IsPostBack)
    //    {
    //        if (rptJobs.Items.Count <= 0)
    //        {
    //            lblMessage.Text = "All Jobs has been assigned to " + lblComponent.Text;
    //            tdJobs.Visible = false;               
    //            lblComponent.Visible = false;
    //        }
    //    }
    //}
    //private void BindJobsForEdit()
    //{
    //    String strJobs;
    //    //strJobs = "SELECT JobId,JobCode,JobName,Descr,'EDIT' AS TYPE FROM JobMaster WHERE JobId IN (" + Request.QueryString["JobIds"].ToString() + " ) ORDER BY JobCode";
    //    strJobs = "SELECT JM.JobId,JTM.JobTypeCode AS JobCode,JTM.JobTypeName AS JobName,JM.AssignTo AS RankId,JM.DeptId,JM.IntervalId,JM.IsFixed,JM.DescrSh AS Descr,'EDIT' AS TYPE FROM JobMaster JM " +
    //              "INNER JOIN JobTypeMaster JTM ON JTM.JobTypeId = JM.JobTypeId " +
    //              "INNER JOIN  ComponentsJobMapping CJM ON JM.JobId = CJM.JobId AND CJM.ComponentId = " + componentId +
    //              "WHERE JM.JobId IN (" + Request.QueryString["JobIds"].ToString() + " ) ORDER BY JobCode";
    //    DataTable dtJobs;
    //    dtJobs = Common.Execute_Procedures_Select_ByQuery(strJobs);
    //    rptJobs.DataSource = dtJobs;
    //    rptJobs.DataBind();       
    //}
    //public DataTable BindRanks()
    //{
    //    DataTable dtRanks = new DataTable();
    //    string strSQL = "SELECT 0 AS RankId,'< SELECT >' AS RankCode UNION SELECT RankId,RankCode FROM Rank ORDER BY RankCode";
    //    dtRanks = Common.Execute_Procedures_Select_ByQuery(strSQL);
    //    return dtRanks;
    //}
    //public DataTable BindIntervalType()
    //{
    //    DataTable dtIntervalType = new DataTable();
    //    string strSQL = "SELECT 0 AS IntervalId,'< SELECT >' AS IntervalName UNION SELECT IntervalId, IntervalName FROM JobIntervalMaster ORDER BY IntervalName";
    //    dtIntervalType = Common.Execute_Procedures_Select_ByQuery(strSQL);
    //    return dtIntervalType;       
    //}
    //public DataTable BindDepartments()
    //{
    //    DataTable dtDepartments = new DataTable();
    //    string strSQL = "SELECT 0 AS DeptId,'< SELECT >' AS DeptName UNION SELECT DeptId,DeptName FROM DeptMaster ORDER BY DeptName";
    //    dtDepartments = Common.Execute_Procedures_Select_ByQuery(strSQL);
    //    return dtDepartments;
    //}
    //private Boolean IsValidated()
    //{
    //   int count = 0;
    //   foreach(RepeaterItem rptItem in rptJobs.Items)
    //   {
    //       CheckBox chkSelect = (CheckBox)rptItem.FindControl("chkSelect");
    //       if (chkSelect.Checked)
    //       {
    //           count = count + 1;
    //       }
    //   }
    //   if (count == 0)
    //   {
    //       MessageBox1.ShowMessage("Please Select a Job",true);
    //       return false;
    //   }
    //   foreach (RepeaterItem rptItem in rptJobs.Items)
    //   {
    //       CheckBox chkSelect = (CheckBox)rptItem.FindControl("chkSelect");
    //       DropDownList ddlAssignTo = (DropDownList)rptItem.FindControl("ddlAssignTo");
    //       DropDownList ddlDepartment = (DropDownList)rptItem.FindControl("ddlDepartment");
    //       DropDownList ddlIntervalType = (DropDownList)rptItem.FindControl("ddlIntervalType");
    //       TextBox txtJobDesc = (TextBox)rptItem.FindControl("txtJobDesc");
    //       RadioButtonList rblFixed = (RadioButtonList)rptItem.FindControl("rblFixed");
    //       if (chkSelect.Checked)
    //       {
    //           if (ddlDepartment.SelectedIndex == 0)
    //           {
    //               MessageBox1.ShowMessage("Please select a department.",true);
    //               ddlDepartment.Focus();
    //               return false;
    //           }
    //           if (ddlAssignTo.SelectedIndex == 0)
    //           {
    //               MessageBox1.ShowMessage("Please select a rank to assign job.",true);
    //               ddlAssignTo.Focus();
    //               return false;
    //           }               
    //           if (ddlIntervalType.SelectedIndex == 0)
    //           {
    //               MessageBox1.ShowMessage("Please select Interval Type.", true);
    //               ddlIntervalType.Focus();
    //               return false;
    //           }              
    //           if (txtJobDesc.Text.Trim().Length > 500)
    //           {
    //               MessageBox1.ShowMessage("Description should not be more than 500 characters.", true);
    //               txtJobDesc.Focus();
    //               return false;
    //           }
    //       }
    //   }
    //   return true;
    //}
    //private Boolean IsValidForEdit()
    //{
    //    foreach (RepeaterItem rptItem in rptJobs.Items)
    //    {
    //        DropDownList ddlAssignTo = (DropDownList)rptItem.FindControl("ddlAssignTo");
    //        DropDownList ddlDepartment = (DropDownList)rptItem.FindControl("ddlDepartment");
    //        DropDownList ddlIntervalType = (DropDownList)rptItem.FindControl("ddlIntervalType");
    //        TextBox txtJobDesc = (TextBox)rptItem.FindControl("txtJobDesc");
    //        RadioButtonList rblFixed = (RadioButtonList)rptItem.FindControl("rblFixed");
    //        if (ddlDepartment.SelectedIndex == 0)
    //        {
    //            MessageBox1.ShowMessage("Please select a department.",true);
    //            ddlDepartment.Focus();
    //            return false;
    //        }
    //        if (ddlAssignTo.SelectedIndex == 0)
    //        {
    //            MessageBox1.ShowMessage("Please select a rank to assign job.",true);
    //            ddlAssignTo.Focus();
    //            return false;
    //        }
    //        if (ddlIntervalType.SelectedIndex == 0)
    //        {
    //            MessageBox1.ShowMessage("Please select Interval Type.", true);
    //            ddlIntervalType.Focus();
    //            return false;
    //        }            
    //        if (txtJobDesc.Text.Trim().Length > 500)
    //        {
    //            MessageBox1.ShowMessage("Description should not be more than 500 characters.", true);
    //            txtJobDesc.Focus();
    //            return false;
    //        }           
    //    }
    //    return true;
    //}
    //private void BindAllJobs()
    //{
    //    string strSelectedJobs = "SELECT JM.*,CJM.*,DM.DeptName,RM.RankCode,CASE JIM.IntervalName WHEN 'H' THEN CASE CJM.IsFixed WHEN 0 THEN JIM.IntervalName + ' - Fixed' ELSE JIM.IntervalName + ' - Flexible' END ELSE JIM.IntervalName END AS IntervalName  FROM JobMaster JM " +
    //                             "INNER JOIN ComponentsJobMapping CJM ON CJM.JobId = JM.JobId " +
    //                             "INNER JOIN DeptMaster DM ON DM.DeptId = CJM.DeptId " +
    //                             "INNER JOIN Rank RM ON RM.RankId = CJM.AssignTo " +
    //                             "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = CJM.IntervalId " +
    //                             "WHERE JM.JobId IN (SELECT JobId FROM ComponentsJobMapping WHERE ComponentId = " + componentId + " ) ";
    //    DataTable dtJobsSelected = Common.Execute_Procedures_Select_ByQuery(strSelectedJobs);
    //    if (dtJobsSelected.Rows.Count > 0)
    //    {
    //        rptJobstoassign.DataSource = dtJobsSelected;
    //        rptJobstoassign.DataBind();
    //    }
    //    else
    //    {
    //        rptJobstoassign.DataSource = null;
    //        rptJobstoassign.DataBind();
    //    }
    //}
    private void GetComponent()
    {
        DataTable dtCompId = new DataTable();
        string strSQL = "SELECT ComponentId,ComponentCode,ComponentName FROM ComponentMaster WHERE ComponentCode = '" + Request.QueryString["CompCode"].ToString().Trim() + "' ";
        dtCompId = Common.Execute_Procedures_Select_ByQuery(strSQL);
        componentId = Common.CastAsInt32(dtCompId.Rows[0]["ComponentId"].ToString());
        compcode = dtCompId.Rows[0]["ComponentCode"].ToString();
        lblComponent.Text = dtCompId.Rows[0]["ComponentCode"].ToString() + " : " + dtCompId.Rows[0]["ComponentName"].ToString();
    }
   
    #endregion ----------------------------------------------------

    #region ----------- EVENTS ------------------------------------
    //protected void btnAddJobs_Click(object sender, EventArgs e)
    //{
    //    //if (!IsValidated())
    //    //{
    //    //    return;
    //    //}
    //    if (compcode.Trim().Length == 6)
    //    {
    //        DataTable dt = new DataTable();
    //        string strSQL = "SELECT * FROM ComponentMaster WHERE ComponentCode = '" + compcode + "' AND  InheritParentJobs = 1";
    //        dt = Common.Execute_Procedures_Select_ByQuery(strSQL);
    //        if (dt.Rows.Count > 0)
    //        {
    //            string SQL = "SELECT * FROM ComponentMaster WHERE LEN(ComponentCode) = 8 AND LEFT(ComponentCode,6) = '" + compcode + "'";
    //            DataTable dtUnits = Common.Execute_Procedures_Select_ByQuery(SQL);
    //            if (dtUnits.Rows.Count > 0)
    //            {
    //                foreach (DataRow dr in dtUnits.Rows)
    //                {
    //                    foreach (RepeaterItem rptItem in rptJobs.Items)
    //                    {
    //                        CheckBox chkSelect = (CheckBox)rptItem.FindControl("chkSelect");
    //                        int compId = Convert.ToInt32(dr["ComponentId"].ToString());
    //                        DropDownList ddlAssignTo = (DropDownList)rptItem.FindControl("ddlAssignTo");
    //                        DropDownList ddlDepartment = (DropDownList)rptItem.FindControl("ddlDepartment");
    //                        DropDownList ddlIntervalType = (DropDownList)rptItem.FindControl("ddlIntervalType");
    //                        TextBox txtJobDesc = (TextBox)rptItem.FindControl("txtJobDesc");
    //                        RadioButtonList rblFixed = (RadioButtonList)rptItem.FindControl("rblFixed");
    //                        HiddenField hfJobId = (HiddenField)rptItem.FindControl("hfJobId");
    //                        if (chkSelect.Checked)
    //                        {
    //                            int JobId = Common.CastAsInt32(hfJobId.Value);
    //                            DataTable dtJobsexist = new DataTable();
    //                            string strJobs = "SELECT * FROM ComponentsJobMapping WHERE ComponentId = " + compId + " AND JobId = " + JobId + " ";

    //                            dtJobsexist = Common.Execute_Procedures_Select_ByQuery(strJobs);
    //                            if (dtJobsexist.Rows.Count > 0)
    //                            {
    //                            }
    //                            else
    //                            {
    //                                Common.Set_Procedures("sp_InsertDeleteComponentJobs");
    //                                Common.Set_ParameterLength(8);
    //                                Common.Set_Parameters(
    //                                    new MyParameter("@ComponentId", compId),
    //                                    new MyParameter("@JobId", JobId),
    //                                    new MyParameter("@Descr", txtJobDesc.Text.Trim()),
    //                                    new MyParameter("@AssignTo", ddlAssignTo.SelectedValue),
    //                                    new MyParameter("@DeptId", ddlDepartment.SelectedValue),
    //                                    new MyParameter("@IntervalId", ddlIntervalType.SelectedValue),
    //                                    new MyParameter("@IsFixed", rblFixed.SelectedIndex),
    //                                    new MyParameter("@Type", "ADD")
    //                                    );
    //                                DataSet dsJobs = new DataSet();
    //                                dsJobs.Clear();
    //                                Boolean res;
    //                                res = Common.Execute_Procedures_IUD(dsJobs);
    //                                if (res)
    //                                {
    //                                    //lblMessage.Text = "Job Added Successfully.";
    //                                }
    //                                else
    //                                {
    //                                    //lblMessage.Text = "Unable to Save Job.";
    //                                    MessageBox1.ShowMessage("Unable to Save Job.", true);
    //                                }
    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }

    //    foreach (RepeaterItem rptItem in rptJobs.Items)
    //    {
    //        CheckBox chkSelect = (CheckBox)rptItem.FindControl("chkSelect");
    //        DropDownList ddlAssignTo = (DropDownList)rptItem.FindControl("ddlAssignTo");
    //        DropDownList ddlDepartment = (DropDownList)rptItem.FindControl("ddlDepartment");
    //        DropDownList ddlIntervalType = (DropDownList)rptItem.FindControl("ddlIntervalType");
    //        TextBox txtJobDesc = (TextBox)rptItem.FindControl("txtJobDesc");
    //        RadioButtonList rblFixed = (RadioButtonList)rptItem.FindControl("rblFixed");
    //        HiddenField hfJobId = (HiddenField)rptItem.FindControl("hfJobId");
    //        if (chkSelect.Checked)
    //        {
    //            int JobId = Common.CastAsInt32(hfJobId.Value);
    //            Common.Set_Procedures("sp_InsertDeleteComponentJobs");
    //            Common.Set_ParameterLength(8);
    //            Common.Set_Parameters(
    //                new MyParameter("@ComponentId", componentId),
    //                new MyParameter("@JobId", JobId),
    //                new MyParameter("@Descr", txtJobDesc.Text.Trim()),
    //                new MyParameter("@AssignTo", ddlAssignTo.SelectedValue),
    //                new MyParameter("@DeptId", ddlDepartment.SelectedValue),
    //                new MyParameter("@IntervalId", ddlIntervalType.SelectedValue),
    //                new MyParameter("@IsFixed", rblFixed.SelectedIndex),
    //                new MyParameter("@Type", "ADD")
    //                );
    //            DataSet dsJobs = new DataSet();
    //            dsJobs.Clear();
    //            Boolean res;
    //            res = Common.Execute_Procedures_IUD(dsJobs);
    //            if (res)
    //            {
    //                MessageBox1.ShowMessage("Job Saved Successfully.", false);
    //            }
    //            else
    //            {
    //                MessageBox1.ShowMessage("Unable to Save Job.", true);
    //            }
    //        }
    //    }
    //    BindJobs();
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "refreshonadd();", true);
    //}
    // protected void btnEdit_Click(object sender, EventArgs e)
    // {
    //     if (!IsValidForEdit())
    //     {
    //         return;
    //     }
    //     if (compcode.Trim().Length == 6)
    //     {
    //         DataTable dt = new DataTable();
    //         string strSQL = "SELECT * FROM ComponentMaster WHERE ComponentCode = '" + compcode + "' AND  InheritParentJobs = 1";
    //         dt = Common.Execute_Procedures_Select_ByQuery(strSQL);
    //         if (dt.Rows.Count > 0)
    //         {
    //             string SQL = "SELECT * FROM ComponentMaster WHERE LEN(ComponentCode) = 8 AND LEFT(ComponentCode,6) = '" + compcode + "'";
    //             DataTable dtUnits = Common.Execute_Procedures_Select_ByQuery(SQL);
    //             if (dtUnits.Rows.Count > 0)
    //             {
    //                 foreach (DataRow dr in dtUnits.Rows)
    //                 {
    //                     foreach (RepeaterItem rptItem in rptJobs.Items)
    //                     {
    //                         //CheckBox chkSelect = (CheckBox)rptItem.FindControl("chkSelect");
    //                         int compId = Convert.ToInt32(dr["ComponentId"].ToString());
    //                         DropDownList ddlAssignTo = (DropDownList)rptItem.FindControl("ddlAssignTo");
    //                         DropDownList ddlDepartment = (DropDownList)rptItem.FindControl("ddlDepartment");
    //                         DropDownList ddlIntervalType = (DropDownList)rptItem.FindControl("ddlIntervalType");
    //                         TextBox txtJobDesc = (TextBox)rptItem.FindControl("txtJobDesc");
    //                         RadioButtonList rblFixed = (RadioButtonList)rptItem.FindControl("rblFixed");
    //                         HiddenField hfJobId = (HiddenField)rptItem.FindControl("hfJobId");
    //                         //if (chkSelect.Checked)
    //                         //{
    //                         int JobId = Common.CastAsInt32(hfJobId.Value);
    //                         DataTable dtJobsexist = new DataTable();
    //                         string strJobs = "SELECT * FROM ComponentsJobMapping WHERE ComponentId = " + compId + " AND JobId = " + JobId + " ";                                              
    //                         dtJobsexist = Common.Execute_Procedures_Select_ByQuery(strJobs);
    //                         if (dtJobsexist.Rows.Count > 0)
    //                         {
    //                             Common.Set_Procedures("sp_InsertDeleteComponentJobs");
    //                             Common.Set_ParameterLength(8);
    //                             Common.Set_Parameters(
    //                                 new MyParameter("@ComponentId", compId),
    //                                 new MyParameter("@JobId", JobId),
    //                                 new MyParameter("@Descr", txtJobDesc.Text.Trim()),
    //                                 new MyParameter("@AssignTo", ddlAssignTo.SelectedValue),
    //                                 new MyParameter("@DeptId", ddlDepartment.SelectedValue),
    //                                 new MyParameter("@IntervalId", ddlIntervalType.SelectedValue),
    //                                 new MyParameter("@IsFixed", rblFixed.SelectedIndex),
    //                                 new MyParameter("@Type", "EDIT")
    //                                 );
    //                             DataSet dsJobs = new DataSet();
    //                             dsJobs.Clear();
    //                             Boolean res;
    //                             res = Common.Execute_Procedures_IUD(dsJobs);
    //                             if (res)
    //                             {
    //                                 //lblMessage.Text = "Job Added Successfully.";
    //                             }
    //                             else
    //                             {
    //                                 MessageBox1.ShowMessage("Unable to Save Job.", true);
    //                             }
    //                         }
    //                         //}
    //                     }
    //                 }
    //             }
    //         }
    //     }         
    //     foreach (RepeaterItem rptItem in rptJobs.Items)
    //     {
    //         DropDownList ddlAssignTo = (DropDownList)rptItem.FindControl("ddlAssignTo");
    //         DropDownList ddlDepartment = (DropDownList)rptItem.FindControl("ddlDepartment");
    //         DropDownList ddlIntervalType = (DropDownList)rptItem.FindControl("ddlIntervalType");
    //         TextBox txtJobDesc = (TextBox)rptItem.FindControl("txtJobDesc");
    //         RadioButtonList rblFixed = (RadioButtonList)rptItem.FindControl("rblFixed");
    //         HiddenField hfJobId = (HiddenField)rptItem.FindControl("hfJobId");
    //         int JobId = Common.CastAsInt32(hfJobId.Value);
    //         Common.Set_Procedures("sp_InsertDeleteComponentJobs");
    //         Common.Set_ParameterLength(8);
    //         Common.Set_Parameters(
    //             new MyParameter("@ComponentId", componentId),
    //             new MyParameter("@JobId", JobId),
    //             new MyParameter("@Descr", txtJobDesc.Text.Trim()),
    //             new MyParameter("@AssignTo", ddlAssignTo.SelectedValue),
    //             new MyParameter("@DeptId", ddlDepartment.SelectedValue),
    //             new MyParameter("@IntervalId", ddlIntervalType.SelectedValue),
    //             new MyParameter("@IsFixed", rblFixed.SelectedIndex),
    //             new MyParameter("@Type", "EDIT")
    //             );
    //         DataSet dsJobs = new DataSet();
    //         dsJobs.Clear();
    //         Boolean res;
    //         res = Common.Execute_Procedures_IUD(dsJobs);
    //         if (res)
    //         {
    //             MessageBox1.ShowMessage("Job Saved Successfully.",false);
    //         }
    //         else
    //         {
    //             MessageBox1.ShowMessage("Unable to Save Job.",true);
    //         }
    //     }         
    //     ScriptManager.RegisterStartupScript(this, this.GetType(), "", "refreshonadd();", true);
    // }
     //protected void ddlIntervalType_SelectedIndexChanged(object sender, EventArgs e)
     //{         
     //    foreach (RepeaterItem rptItem in rptJobs.Items)
     //    {
     
     //        DropDownList ddlIntervalType = (DropDownList)rptItem.FindControl("ddlIntervalType");
     //        RadioButtonList rblFixed = (RadioButtonList)rptItem.FindControl("rblFixed");
             
     //            if (ddlIntervalType.SelectedIndex == 2)
     //            {
     //                rblFixed.Visible = true;
     //            }
     //            else
     //            {
     //                rblFixed.Visible = false;
     //            }                   
     //    }        
     //}
    protected void ddlJobType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlJobType.SelectedIndex != 0)
        {
            string strSelectJob = "";
            //string strCritical = "";
            //strCritical = "SELECT * from ComponentMaster WHERE ComponentId = " + componentId + " AND CriticalEquip = 1 " ;
            //DataTable dtCritical = Common.Execute_Procedures_Select_ByQuery(strCritical);
            //if (dtCritical.Rows.Count > 0)
            //{
            //    chkIsCritical.Checked = true;
            //}
            //else
            //{
            //    chkIsCritical.Checked = false;
            //}
            if (Request.QueryString["Type"].ToString() == "ADD")
            {
                strSelectJob = "SELECT JM.*,'0' AS IsFixed ,'' AS DescrSh,'' AS DescrM, '0' AS DeptId,'0' AS AssignTo,'0' AS IntervalId,'' AS AttachmentForm,'' AS RiskAssessment,'False' AS RatingRequired FROM JobMaster JM " +
                               "WHERE JM.JobId = " + ddlJobType.SelectedValue ;

            }
            else
            {
                strSelectJob = "SELECT JM.*,ISNULL(CJM.IsFixed,'0') AS IsFixed ,ISNULL(CJM.DescrSh,'') AS DescrSh,ISNULL(CJM.DescrM,'') AS DescrM,ISNULL(CJM.DeptId,'0') AS DeptId,ISNULL(CJM.AssignTo,'0') AS AssignTo,ISNULL(CJM.IntervalId,'0') AS IntervalId,CJM.AttachmentForm,CJM.RiskAssessment,CJM.RatingRequired FROM JobMaster JM " +
                               "LEFT JOIN ComponentsJobMapping CJM ON CJM.JobId = JM.JobId " +
                               "LEFT JOIN DeptMaster DM ON DM.DeptId = CJM.DeptId " +
                               "LEFT JOIN Rank RM ON RM.RankId = CJM.AssignTo " +
                               "LEFT JOIN JobIntervalMaster JIM ON JIM.IntervalId = CJM.IntervalId " +                               
                               "WHERE CJM.componentId =" + componentId + " AND CJM.JobId = " + ddlJobType.SelectedValue + " AND CJM.CompJobId=" + CompJobId;
            }            
                                  
            DataTable dtJobs = Common.Execute_Procedures_Select_ByQuery(strSelectJob);
            if (dtJobs.Rows.Count > 0)
            {
                ddlDepartment.SelectedValue = dtJobs.Rows[0]["DeptId"].ToString();
                ddlAssignTo.SelectedValue = dtJobs.Rows[0]["AssignTo"].ToString();
                ddlIntervalType.SelectedValue = dtJobs.Rows[0]["IntervalId"].ToString();
                //if (ddlIntervalType.SelectedItem.Text == "H")
                //{
                //    rblFixed.SelectedIndex = Common.CastAsInt32(dtJobs.Rows[0]["IsFixed"].ToString());
                //    trJobSchedule.Visible = true;
                //}
                //else
                //{
                //    trJobSchedule.Visible = false;
                //}
                //chkIsCritical.Checked = Convert.ToBoolean(dtJobs.Rows[0]["Critical"].ToString());
                
                txtDescrSh.Text = dtJobs.Rows[0]["DescrSh"].ToString();
                txtDescrM.Text = dtJobs.Rows[0]["DescrM"].ToString();
                txtAttachForm.Text = dtJobs.Rows[0]["AttachmentForm"].ToString();
                txtRiskAssessment.Text = dtJobs.Rows[0]["RiskAssessment"].ToString();
                if (dtJobs.Rows[0]["RatingRequired"].ToString() == "True")
                {
                    chkRatingReqd.Checked = true;
                }
                //else
                //{
                //    chkRatingReqd.Checked = false;
                //}

                foreach (ListItem li in chkDeckRank.Items)
                {
                    DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.ComponentJobMapping_OtherRanks WHERE COMPJOBID=" + CompJobId.ToString() + " AND RANKID=" + li.Value);
                    li.Selected = (dt1.Rows.Count > 0);
                }

                foreach (ListItem li in chkEngineRank.Items)
                {
                    DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.ComponentJobMapping_OtherRanks WHERE COMPJOBID=" + CompJobId.ToString() + " AND RANKID=" + li.Value);
                    li.Selected = (dt1.Rows.Count > 0);
                }
            }
            //else
            //{
            //    rptJobs.DataSource = null;
            //    rptJobs.DataBind();
            //}
           //BindAllJobs();
        }
    }
    protected void ddlIntervalType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlIntervalType.SelectedIndex == 2)
        //{
        //    trJobSchedule.Visible = true;
        //}
        //else
        //{
        //    trJobSchedule.Visible = false;
        //}

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!IsValidated())
        {
            return;
        }
            
                Common.Set_Procedures("sp_InsertDeleteComponentJobs");
                Common.Set_ParameterLength(13);
                Common.Set_Parameters(
                    new MyParameter("@ComponentId", componentId),
                    new MyParameter("@JobId", ddlJobType.SelectedValue),
                    new MyParameter("@CompJobId", CompJobId),
                    new MyParameter("@AssignTo", ddlAssignTo.SelectedValue),
                    new MyParameter("@DeptId", ddlDepartment.SelectedValue),
                    new MyParameter("@IntervalId", ddlIntervalType.SelectedValue),
                    new MyParameter("@IsFixed", rblFixed.SelectedIndex),
                    new MyParameter("@DescrSh", txtDescrSh.Text.Trim()),
                    new MyParameter("@DescrM", txtDescrM.Text.Trim()),
                    new MyParameter("@Type", Request.QueryString["Type"].ToString()),
                    new MyParameter("@AttachmentForm", txtAttachForm.Text.Trim()),
                    new MyParameter("@RiskAssessment", txtRiskAssessment.Text.Trim()),
                    new MyParameter("@RatingRequired", chkRatingReqd.Checked ? 1 : 0)
                    );
                DataSet dsAddJobs = new DataSet();
                dsAddJobs.Clear();
                Boolean res1;
                res1 = Common.Execute_Procedures_IUD(dsAddJobs);
                if (res1)
                {
                    MessageBox1.ShowMessage("Job Saved Successfully.", false);
                    
                    if (Request.QueryString["Type"].ToString() == "ADD")
                    {
                        if (chkInhParent.Checked)
                        {

                            if (compcode.Trim().Length == 9)
                            {
                                //DataTable dt = new DataTable();
                                //string strSQL = "SELECT * FROM ComponentMaster WHERE ComponentCode = '" + compcode + "' AND  InheritParentJobs = 1";
                                //dt = Common.Execute_Procedures_Select_ByQuery(strSQL);
                                //if (dt.Rows.Count > 0)
                                //{
                                string SQL = "SELECT * FROM ComponentMaster WHERE LEN(ComponentCode) = 12 AND LEFT(ComponentCode,9) = '" + compcode + "'";
                                DataTable dtUnits = Common.Execute_Procedures_Select_ByQuery(SQL);
                                if (dtUnits.Rows.Count > 0)
                                {
                                    foreach (DataRow dr in dtUnits.Rows)
                                    {
                                        int compId = Convert.ToInt32(dr["ComponentId"].ToString());


                                        //DataTable dtJobsexist = new DataTable();
                                        //string strJobs = "SELECT * FROM ComponentsJobMapping WHERE ComponentId = " + compId + " AND JobId = " + ddlJobType.SelectedValue + " ";

                                        //dtJobsexist = Common.Execute_Procedures_Select_ByQuery(strJobs);
                                        //if (dtJobsexist.Rows.Count > 0)
                                        //{
                                        //}
                                        //else
                                        //{
                                            Common.Set_Procedures("sp_InsertDeleteComponentJobs");
                                            Common.Set_ParameterLength(13);
                                            Common.Set_Parameters(
                                                new MyParameter("@ComponentId", compId),
                                                new MyParameter("@JobId", ddlJobType.SelectedValue),
                                                new MyParameter("@CompJobId", CompJobId),
                                                new MyParameter("@AssignTo", ddlAssignTo.SelectedValue),
                                                new MyParameter("@DeptId", ddlDepartment.SelectedValue),
                                                new MyParameter("@IntervalId", ddlIntervalType.SelectedValue),
                                                new MyParameter("@IsFixed", rblFixed.SelectedIndex),
                                                new MyParameter("@DescrSh", txtDescrSh.Text.Trim()),
                                                new MyParameter("@DescrM", txtDescrM.Text.Trim()),
                                                new MyParameter("@Type", Request.QueryString["Type"].ToString()),
                                                new MyParameter("@AttachmentForm", txtAttachForm.Text.Trim()),
                                                new MyParameter("@RiskAssessment", txtRiskAssessment.Text.Trim()),
                                                new MyParameter("@RatingRequired", chkRatingReqd.Checked ? 1 : 0)
                                                );
                                            DataSet dsJobs = new DataSet();
                                            dsJobs.Clear();
                                            Boolean res;
                                            res = Common.Execute_Procedures_IUD(dsJobs);
                                            if (res)
                                            {
                                            }
                                            else
                                            {
                                                MessageBox1.ShowMessage("Unable to Save Job.Error :" + Common.getLastError(), true);
                                            }
                                        //}
                                    }
                                }
                                //}
                            }
                        }
                        CompJobId = Common.CastAsInt32(dsAddJobs.Tables[0].Rows[0][0]);
                        BindJobTypes();
                    }
                    //------------------
                    Common.Execute_Procedures_Select_ByQuery("DELETE FROM DBO.ComponentJobMapping_OtherRanks WHERE COMPJOBID=" + CompJobId.ToString());
                    foreach (ListItem li in chkDeckRank.Items)
                    {
                        if (li.Selected)
                        {
                            Common.Execute_Procedures_Select_ByQuery("INSERT INTO DBO.ComponentJobMapping_OtherRanks(COMPJOBID,RANKID) VALUES(" + CompJobId.ToString() + "," + li.Value + ")");
                        }
                    }
                    foreach (ListItem li in chkEngineRank.Items)
                    {
                        if (li.Selected)
                        {
                            Common.Execute_Procedures_Select_ByQuery("INSERT INTO DBO.ComponentJobMapping_OtherRanks(COMPJOBID,RANKID) VALUES(" + CompJobId.ToString() + "," + li.Value + ")");
                        }
                    }
        }
                else
                {
                    MessageBox1.ShowMessage("Unable to Save Job.Error :" + Common.getLastError(), true);
                }
            
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "refreshonadd();", true);
    }

    //protected void MoveSelectedToRight_Click(object sender, EventArgs e)
    //{
    //    if (compcode.Trim().Length == 6)
    //    {
    //        DataTable dt = new DataTable();
    //        string strSQL = "SELECT * FROM ComponentMaster WHERE ComponentCode = '" + compcode + "' AND  InheritParentJobs = 1";
    //        dt = Common.Execute_Procedures_Select_ByQuery(strSQL);
    //        if (dt.Rows.Count > 0)
    //        {
    //            string SQL = "SELECT * FROM ComponentMaster WHERE LEN(ComponentCode) = 8 AND LEFT(ComponentCode,6) = '" + compcode + "'";
    //            DataTable dtUnits = Common.Execute_Procedures_Select_ByQuery(SQL);
    //            if (dtUnits.Rows.Count > 0)
    //            {
    //                foreach (DataRow dr in dtUnits.Rows)
    //                {
    //                    foreach (RepeaterItem rptItem in rptJobs.Items)
    //                    {
    //                        CheckBox chkSelect = (CheckBox)rptItem.FindControl("chkSelect");
    //                        int compId = Convert.ToInt32(dr["ComponentId"].ToString());
    //                        HiddenField hfJobId = (HiddenField)rptItem.FindControl("hfJobId");
    //                        if (chkSelect.Checked)
    //                        {
    //                            int JobId = Common.CastAsInt32(hfJobId.Value);
    //                            DataTable dtJobsexist = new DataTable();
    //                            string strJobs = "SELECT * FROM ComponentsJobMapping WHERE ComponentId = " + compId + " AND JobId = " + JobId + " ";

    //                            dtJobsexist = Common.Execute_Procedures_Select_ByQuery(strJobs);
    //                            if (dtJobsexist.Rows.Count > 0)
    //                            {
    //                            }
    //                            else
    //                            {
    //                                string strAddJobsToChilds = "INSERT INTO ComponentsJobMapping (ComponentId,JobId) VALUES (" + compId + "," + JobId + " ); SELECT -1";
    //                                DataTable dtAddJobsToChilds = Common.Execute_Procedures_Select_ByQuery(strAddJobsToChilds);
    //                                if (dtAddJobsToChilds.Rows.Count > 0)
    //                                {

    //                                }
    //                                else
    //                                {

    //                                    MessageBox1.ShowMessage("Unable to Save Job.", true);
    //                                }
    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }

    //    foreach (RepeaterItem rptItem in rptJobs.Items)
    //    {
    //        CheckBox chkSelect = (CheckBox)rptItem.FindControl("chkSelect");
    //        HiddenField hfJobId = (HiddenField)rptItem.FindControl("hfJobId");
    //        if (chkSelect.Checked)
    //        {
    //            string strAddJobs = "INSERT INTO ComponentsJobMapping (ComponentId,JobId) VALUES (" + componentId + "," + hfJobId.Value + " ); SELECT -1";
    //            DataTable dtAddJobs = Common.Execute_Procedures_Select_ByQuery(strAddJobs);
    //            //int JobId = Common.CastAsInt32(hfJobId.Value);
    //            //Common.Set_Procedures("sp_InsertDeleteComponentJobs");
    //            //Common.Set_ParameterLength(8);
    //            //Common.Set_Parameters(
    //            //    new MyParameter("@ComponentId", componentId),
    //            //    new MyParameter("@JobId", JobId),
    //            //    new MyParameter("@Descr", txtJobDesc.Text.Trim()),
    //            //    new MyParameter("@AssignTo", ddlAssignTo.SelectedValue),
    //            //    new MyParameter("@DeptId", ddlDepartment.SelectedValue),
    //            //    new MyParameter("@IntervalId", ddlIntervalType.SelectedValue),
    //            //    new MyParameter("@IsFixed", rblFixed.SelectedIndex),
    //            //    new MyParameter("@Type", "ADD")
    //            //    );
    //            //DataSet dsJobs = new DataSet();
    //            //dsJobs.Clear();
    //            //Boolean res;
    //            //res = Common.Execute_Procedures_IUD(dsJobs);
    //            if (dtAddJobs.Rows.Count > 0)
    //            {
    //                MessageBox1.ShowMessage("Job Saved Successfully.", false);
    //            }
    //            else
    //            {
    //                MessageBox1.ShowMessage("Unable to Save Job.", true);
    //            }
    //        }
    //    }
    //    ddlJobType_SelectedIndexChanged(sender, e);
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "refreshonadd();", true);
    //}
    //protected void MoveSelectedToLeft_Click(object sender, EventArgs e)
    //{
    //    foreach (RepeaterItem rptItem in rptJobstoassign.Items)
    //    {
    //        CheckBox chkSelect = (CheckBox)rptItem.FindControl("chkSelectJobs");
    //        HiddenField hfJobId = (HiddenField)rptItem.FindControl("hfJobId");
    //        if (chkSelect.Checked)
    //        {
    //            string strDeleteJobs = "DELETE FROM ComponentsJobMapping WHERE ComponentId = " + componentId + " AND JobId =" + hfJobId.Value + " ; SELECT -1";
    //            DataTable dtDeleteJobs = Common.Execute_Procedures_Select_ByQuery(strDeleteJobs);
    //            if (dtDeleteJobs.Rows.Count > 0)
    //            {
    //                MessageBox1.ShowMessage("Job Saved Successfully.", false);
    //            }
    //            else
    //            {
    //                MessageBox1.ShowMessage("Unable to Save Job.", true);
    //            }
    //        }
    //    }
    //    ddlJobType_SelectedIndexChanged(sender, e);
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "refreshonadd();", true);
    //}
    #endregion




    //protected void rptFiles_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    if (!(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem))
    //    {
    //        return;
    //    }
    //    LinkButton linkButton = (LinkButton)e.Item.FindControl("lnlViewVersion");
    //    var scriptManager = ScriptManager.GetCurrent(this.Page);
    //    if (scriptManager != null)
    //    {
    //        scriptManager.RegisterPostBackControl(linkButton);
    //    }
    //}
}
