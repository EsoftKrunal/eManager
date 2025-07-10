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

public partial class ModifyRCA : System.Web.UI.Page
{
    public int RiskId
    {
        set { ViewState["RiskId"] = value; }
        get { return Common.CastAsInt32(ViewState["RiskId"]); }
    }
    public int TemplateId
    {
        set { ViewState["TemplateId"] = value; }
        get { return Common.CastAsInt32(ViewState["TemplateId"]); }
    }
    public string UserName
    {
        set { ViewState["UserName"] = value; }
        get { return ViewState["UserName"].ToString(); }
    }
    public string VesselCode
    {
        set { ViewState["VesselCode"] = value; }
        get { return ViewState["VesselCode"].ToString(); }
    }
    public bool CanEdit
    {
        set { ViewState["CanEdit"] = value; }
        get { return Convert.ToBoolean(ViewState["CanEdit"]); }
    }
    
    public DataTable dtTasks
    {
        get
        {
            object o = ViewState["dtTasks"];
            return (DataTable)o;
        }
        set
        {
            ViewState["dtTasks"] = value;
        }
    }
    public DataTable dtHazards
    {
        get
        {
            object o = ViewState["dtHazards"];
            return (DataTable)o;
        }
        set
        {
            ViewState["dtHazards"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblMess.Text = "";
        lblMsg_Approve.Text = "";
        lblAddTaskMsg.Text = "";
        if (!IsPostBack)
        {
            //---------------------------
            CanEdit = false;
            ViewState["Task_TableId"] = "0";
            ViewState["TableId"] = "0";
            //---------------------------
            VesselCode = Session["CurrentShip"].ToString();
            UserName = Session["UserName"].ToString().Trim().ToUpper();
            CreateTables();
            TemplateId = Common.CastAsInt32(Request.QueryString["TemplateId"]);
            RiskId = Common.CastAsInt32(Request.QueryString["RiskId"]);
            ShowMasterDetails();
        }
    }
    public void ShowMasterDetails()
    {
      
        if (TemplateId > 0) // LOAD DATA FROM TEMPLATE
        {
            
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT *, (select shipname from settings where shipcode='" + VesselCode + "') AS VesselName  FROM [dbo].[EV_TemplateMaster] WHERE TemplateId=" + TemplateId);
            if (dt != null && dt.Rows.Count > 0)
            {
                lblVesselCode.Text = dt.Rows[0]["VesselName"].ToString();
                lblRevNo.Text = dt.Rows[0]["TemplateCode"].ToString() + "/" +  dt.Rows[0]["RevisionNo"].ToString();
                lblEventName.Text = dt.Rows[0]["EventCode"].ToString() + " : " + dt.Rows[0]["EventName"].ToString();
            }

            DataTable _dtTasks = Common.Execute_Procedures_Select_ByQuery("SELECT *, 'A' As Status FROM [dbo].[EV_Template_Tasks] WHERE TemplateId=" + TemplateId);
            DataTable _dtHazards = Common.Execute_Procedures_Select_ByQuery("SELECT *, 'A' As Status FROM [dbo].[EV_TemplateDetails] WHERE TemplateId=" + TemplateId);
            AddTaskFromDB(_dtTasks, _dtHazards);
            CanEdit = true;
            BindTasks();
        }
        //-------------------------------------------------
        if (RiskId > 0) // LOAD DATA FROM EXISTING DATA
        {
            string OfficeApprovalRequired = "N";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT R.*,(SELECT OfficeApprovalRequired FROM [dbo].[EV_TemplateMaster] WHERE TemplateId=R.TemplateId ) AS OfficeApprovalRequired, (SELECT TemplateCode FROM [dbo].[EV_TemplateMaster] WHERE TemplateId=R.TemplateId ) AS TemplateCode, (select shipname from settings where shipcode=R.VESSELCODE) AS VesselName,Verify_Needed=case when exists(SELECT 1 FROM [dbo].[EV_VSL_Risk_Details] d where d.riskid=r.riskid and d.vesselcode=r.vesselcode and Re_RiskLevel> 10)  then 'Y' else 'N' end FROM[dbo].[EV_VSL_RiskMaster] R WHERE R.RiskId=" + RiskId);
            if (dt != null && dt.Rows.Count > 0)
            {
                lblVesselCode.Text = dt.Rows[0]["VesselName"].ToString();
                lblRevNo.Text = dt.Rows[0]["TemplateCode_Revision"].ToString();
                lblRefNo.Text = dt.Rows[0]["REFNO"].ToString();
                txtEventDate.Text = Common.ToDateString(dt.Rows[0]["EVENTDATE"]);
                lblEventName.Text = dt.Rows[0]["EventCode"].ToString() + " : " + dt.Rows[0]["EventName"].ToString();
                ddlAMW.SelectedValue = dt.Rows[0]["ALTERNATEMETHODS"].ToString();
                ddlAMW_SelectedIndexChanged(new object(), new EventArgs());
                txtDetails.Text = dt.Rows[0]["Details"].ToString();
                txtHodComments.Text = dt.Rows[0]["Comment"].ToString();
                txtHodName.Text = dt.Rows[0]["HOD_NAME"].ToString();
                lblHODDate.Text = Common.ToDateString(dt.Rows[0]["CREATED_ON"]);
                OfficeApprovalRequired = Convert.IsDBNull(dt.Rows[0]["OfficeApprovalRequired"]) ? "N" : dt.Rows[0]["OfficeApprovalRequired"].ToString();
                if(OfficeApprovalRequired=="N")
                { OfficeApprovalRequired = ((dt.Rows[0]["Verify_Needed"].ToString() == "Y") ? "Y" : "N"); }

                lblOVMessage.Text = "Office Approval " + ((OfficeApprovalRequired == "Y") ? "Required " : "Not Required");
            }
            dtTasks = Common.Execute_Procedures_Select_ByQuery("SELECT *, 'A' As Status FROM [dbo].[EV_VSL_Risk_Tasks] WHERE VesselCode='" + VesselCode + "' AND RiskId=" + RiskId);
            dtHazards = Common.Execute_Procedures_Select_ByQuery("SELECT *, 'A' As Status FROM [dbo].[EV_VSL_Risk_Details] WHERE VesselCode='" + VesselCode + "' AND RiskId=" + RiskId);

            int Re_RiskLevel = Common.CastAsInt32(dtHazards.Compute("MAX(Re_RiskLevel)", ""));
            
            //if (dt != null && dt.Rows.Count > 0 && Re_RiskLevel > 10)

            if(OfficeApprovalRequired=="Y")
            {
                txtApproverComments.Text = dt.Rows[0]["OFFICE_COMMENTS"].ToString();
                lblApproverName.Text = dt.Rows[0]["OFFICECOMMENTBY"].ToString();
                lblReviewDate.Text = Common.ToDateString(dt.Rows[0]["COMMENTDATE"]);

            }
            else
            {
                txtApproverComments.Text = dt.Rows[0]["ReviewComment"].ToString();
                lblApproverName.Text = dt.Rows[0]["ReviewerName"].ToString();
                lblReviewDate.Text = Common.ToDateString(dt.Rows[0]["ReviewDate"]);
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                CanEdit = (Convert.IsDBNull(dt.Rows[0]["ReviewDate"]) && Convert.IsDBNull(dt.Rows[0]["COMMENTDATE"]));
                btnApprove.Visible = CanEdit && (UserName == "MASTER" || UserName == "CE" || UserName == "DEMO");
                btnSave.Visible = CanEdit;
                btnAddTask.Visible = CanEdit;
            }
            BindTasks();
        }

    }
    public void CreateTables()
    {
        dtTasks = new DataTable();

        dtTasks.Columns.Add("VesselCode", typeof(string));
        dtTasks.Columns.Add("Task_TableId", typeof(int));
        dtTasks.Columns.Add("RiskId", typeof(int));
        dtTasks.Columns.Add("TaskId", typeof(int));
        dtTasks.Columns.Add("TaskCode", typeof(string));
        dtTasks.Columns.Add("TaskName", typeof(string));
        dtTasks.Columns.Add("Status", typeof(string));


        dtTasks.AcceptChanges();

        dtHazards = new DataTable();


        dtHazards.Columns.Add("TableId", typeof(int));
        dtHazards.Columns.Add("VesselCode", typeof(string));
        dtHazards.Columns.Add("Task_TableId", typeof(int));
        dtHazards.Columns.Add("RISKID", typeof(int));
        dtHazards.Columns.Add("HazardId", typeof(int));
        dtHazards.Columns.Add("HazardCode", typeof(string));
        dtHazards.Columns.Add("HazardName", typeof(string));
        dtHazards.Columns.Add("ControlMeasures", typeof(string));
        dtHazards.Columns.Add("Severity", typeof(int));
        dtHazards.Columns.Add("LikeliHood", typeof(int));
        dtHazards.Columns.Add("RiskLevel", typeof(int));
        dtHazards.Columns.Add("ADD_Control_Measures", typeof(string));
        dtHazards.Columns.Add("Re_Severity", typeof(int));
        dtHazards.Columns.Add("Re_LikeliHood", typeof(int));
        dtHazards.Columns.Add("Re_RiskLevel", typeof(int));
        dtHazards.Columns.Add("Proceed", typeof(string));
        dtHazards.Columns.Add("AGREED_TIME", typeof(string));
        dtHazards.Columns.Add("PIC_NAME", typeof(string));
        dtHazards.Columns.Add("Status", typeof(string));

        dtHazards.AcceptChanges();
    }
    public void BindTasks()
    {
        DataView dv = dtTasks.DefaultView;
        dv.RowFilter = "Status='A'";
        rptTasks.DataSource = dv.ToTable();
        rptTasks.DataBind();
        ShowExtResRisk();

    }
    public DataTable BindHazards(object _TaskTableId)
    {
        DataView dv = dtHazards.DefaultView;
        dv.RowFilter = "Task_TableId=" + _TaskTableId.ToString() + " AND Status='A'";
        return dv.ToTable();
    }

    public void ShowExtResRisk()
    {
        object RiskLevel = dtHazards.Compute("MAX(RiskLevel)", "");
        string ExtColor = GetCSSColor(RiskLevel);
        imgER.ImageUrl = "~/Images/" + ExtColor + ".png";
        lblExtAction.Text = GetAction(ExtColor);
        
        object Re_RiskLevel = dtHazards.Compute("MAX(Re_RiskLevel)", "");
        string ResColor =GetCSSColor(Re_RiskLevel);
        imgRR.ImageUrl = "~/Images/" + ResColor + ".png";

        lblResAction.Text = GetAction(ResColor); 
    } 
    public string GetTaskName(object TableId)
    {
        string ret = "";
        DataRow[] drs = dtHazards.Select("TableId=" + TableId);
        if (drs.Length > 0)
        {
            drs = dtTasks.Select("Task_TableId=" + drs[0]["Task_TableId"].ToString());
            if (drs.Length > 0)
                ret = drs[0]["TaskName"].ToString();
        }
        return ret;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        ////------------------------------------
        //if (("" + Request.QueryString["TC"]) == "")
        //{
        //    DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.EV_TemplateMaster WHERE EVENTID=" + EventId + " and TemplateId<>" + TemplateId);
        //    if (dt.Rows.Count > 0)
        //    {
        //        lblMsg.Text = "Template already exists for same event.";
        //        return;
        //    }
        //}
        ////------------------------------------

        if (txtEventDate.Text.Trim() == "")
        {
            txtEventDate.Focus();
            lblMsg.Text = "Please enter event date.";
            return;

        }
       
        DateTime dts;
        
        if (!DateTime.TryParse(txtEventDate.Text, out dts))
        {
            txtEventDate.Focus();
            lblMsg.Text = "Please enter valid date.";
            return;
        }

        if (ddlAMW.SelectedValue.Trim() == "")
        {
            ddlAMW.Focus();
            lblMsg.Text = "Please select alternate method.";
            return;

        }

        if (txtDetails.Enabled && txtDetails.Text.Trim() == "")
        {
            txtDetails.Focus();
            lblMsg.Text = "Please enter details.";
            return;
        }


        if (txtHodName.Text.Trim() == "")
        {
            txtHodName.Focus();
            lblMsg.Text = "Please HOD Name.";
            return;

        }

        try
        {
            Common.Set_Procedures("[dbo].[EV_VSL_InsertUpdateRiskMaster]");
            Common.Set_ParameterLength(13);
            Common.Set_Parameters(
               new MyParameter("@RISKID", RiskId),
               new MyParameter("@VESSELCODE", VesselCode),
               new MyParameter("@TemplateId", TemplateId),
               new MyParameter("@TemplateCode_Revision", lblRevNo.Text.Trim()),               
               new MyParameter("@EventCode", lblEventName.Text.Split(':').GetValue(0).ToString().Trim()),
               new MyParameter("@EventName", lblEventName.Text.Split(':').GetValue(1).ToString().Trim()),
               new MyParameter("@EVENTDATE", txtEventDate.Text.Trim()),
               new MyParameter("@ALTERNATEMETHODS", ddlAMW.SelectedValue),
               new MyParameter("@Details", txtDetails.Text.Trim()),
               new MyParameter("@Comment", txtHodComments.Text.Trim()),
               new MyParameter("@HOD_NAME", txtHodName.Text.Trim()),
               new MyParameter("@HODDate", DateTime.Today.Date.ToString("dd-MMM-yyyy")),
               new MyParameter("@CREATED_BY", UserName)
               );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                RiskId = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);

                foreach (DataRow dr in dtTasks.Rows)
                {
                    Common.Set_Procedures("[dbo].[EV_VSL_InsertUpdateRiskTasks]");
                    Common.Set_ParameterLength(7);
                    Common.Set_Parameters(
                       new MyParameter("@VesselCode", dr["VesselCode"].ToString()),
                       new MyParameter("@Task_TableId", Common.CastAsInt32(dr["Task_TableId"])),
                       new MyParameter("@RiskId", RiskId),
                       new MyParameter("@TaskId", Common.CastAsInt32(dr["TaskId"])),
                       new MyParameter("@TaskCode", dr["TaskCode"].ToString()),
                       new MyParameter("@TaskName", dr["TaskName"].ToString()),
                       new MyParameter("@Status", dr["Status"].ToString())
                       );
                    DataSet ds1 = new DataSet();
                    Boolean res1;
                    res1 = Common.Execute_Procedures_IUD(ds1);

                    if (res1)
                    {
                        if (dr["Status"].ToString() == "A")
                        {
                            int Task_TableId = Common.CastAsInt32(ds1.Tables[0].Rows[0][0]);

                            DataView dv = dtHazards.DefaultView;
                            dv.RowFilter = "RISKID=" + dr["RISKID"] + " AND Task_TableId=" + dr["Task_TableId"];
                            foreach (DataRow dr1 in dv.ToTable().Rows)
                            {
                                Common.Set_Procedures("[dbo].[EV_VSL_InsertUpdateRiskDetails]");
                                Common.Set_ParameterLength(19);
                                Common.Set_Parameters(
                                   new MyParameter("@TableId", Common.CastAsInt32(dr1["TableId"])),
                                   new MyParameter("@VesselCode", dr1["VesselCode"].ToString()),
                                   new MyParameter("@Task_TableId", Task_TableId),
                                   new MyParameter("@RISKID", RiskId),
                                   new MyParameter("@HazardId", Common.CastAsInt32(dr1["HazardId"])),
                                   new MyParameter("@HazardCode", dr1["HazardCode"].ToString()),
                                   new MyParameter("@HazardName", dr1["HazardName"].ToString()),
                                   new MyParameter("@ControlMeasures", dr1["ControlMeasures"].ToString()),
                                   new MyParameter("@Severity", Common.CastAsInt32(dr1["Severity"])),
                                   new MyParameter("@LikeliHood", Common.CastAsInt32(dr1["LikeliHood"])),
                                   new MyParameter("@RiskLevel", Common.CastAsInt32(dr1["RiskLevel"])),
                                   new MyParameter("@ADD_CONTROL_MEASURES", dr1["ADD_Control_Measures"].ToString()),
                                   new MyParameter("@Re_Severity", Common.CastAsInt32(dr1["Re_Severity"])),
                                   new MyParameter("@Re_LikeliHood", Common.CastAsInt32(dr1["Re_LikeliHood"])),
                                   new MyParameter("@Re_RiskLevel", Common.CastAsInt32(dr1["Re_RiskLevel"])),
                                   new MyParameter("@Proceed", dr1["Proceed"].ToString()),
                                   new MyParameter("@AGREED_TIME", dr1["AGREED_TIME"].ToString()),
                                   new MyParameter("@PIC_NAME", dr1["PIC_NAME"].ToString()),
                                   new MyParameter("@Status", dr1["Status"].ToString())
                                   );
                                DataSet ds2 = new DataSet();
                                Boolean res2;
                                res2 = Common.Execute_Procedures_IUD(ds2);
                            }
                        }
                    }
                }
                
                ShowMasterDetails();
                lblMsg.Text = "RA added/ updated successfully.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "refresh", "RefreshParent();", true);
            }
            else
            {
                lblMsg.Text = "Unable to add/ update RA.Error : " + Common.getLastError();
            }

        }
        catch (Exception ex)
        {

            lblMsg.Text = "Unable to add/ update RA.Error : " + ex.Message.ToString();
        }
    }
    protected void btnSelectTask_Click(object sender, EventArgs e)
    {
        //int TaskId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        //string TaskName = ((ImageButton)sender).Attributes["TaskName"].ToString();
        //try
        //{
        //    DataView dv = dtTasks.DefaultView;
        //    dv.RowFilter = "TemplateId=" + TemplateId + " AND TaskId=" + TaskId + " AND Status='A'";
        //    if (dv.ToTable().Rows.Count > 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Please Check! Task already exists.');", true);
        //        return;
        //    }

        //    DataRow dr = dtTasks.NewRow();

        //    dr["Task_TableId"] = -(Common.CastAsInt32(dtTasks.Rows.Count));
        //    dr["TemplateId"] = TemplateId;
        //    dr["TaskId"] = TaskId;
        //    dr["TaskName"] = TaskName;
        //    dr["Status"] = "A";

        //    dtTasks.Rows.Add(dr);
        //    dtTasks.AcceptChanges();

        //    BindTasks();

        //}
        //catch (Exception ex)
        //{
        //    lblMsg.Text = "Unable to add. Error : " + ex.Message.ToString();
        //}

    }

    protected void btnAddTask_Click(object sender, EventArgs e)
    {
        ViewState["Task_TableId"] = "0";
        txtTaskName.Text = "";
        txtTaskName.Focus();
        dv_NewEditTask.Visible = true;
    }
    protected void btnEditTask_Click(object sender, EventArgs e)
    {
        ViewState["Task_TableId"]=Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        int Key = Common.CastAsInt32(ViewState["Task_TableId"]);
        if (Key != 0)
        {
            DataRow[] drs = dtTasks.Select("Task_TableId=" + Key.ToString());
            if (drs.Length > 0)
            {
                btnDeleteTask.CommandArgument = Key.ToString();
                txtTaskName.Text = drs[0]["TaskName"].ToString();
                dv_NewEditTask.Visible = true;
            }
        }
    }
    protected void btnSaveTask_Click(object sender, EventArgs e)
    {
        if (txtTaskName.Text.Trim() == "")
        {
            lblAddTaskMsg.Text = "Please enter valid task name.";
            return;
        }
        int Key = Common.CastAsInt32(ViewState["Task_TableId"]);
        if (Key == 0)
        {
            DataRow dr = dtTasks.NewRow();
            dr["VesselCode"] = VesselCode;
            dr["Task_TableId"] = -(Common.CastAsInt32(dtTasks.Rows.Count) + 1);
            dr["RiskId"] = 0;
            dr["TaskId"] = 0;
            dr["TaskCode"] = "";
            dr["TaskName"] = txtTaskName.Text;
            dr["Status"] = "A";
            dtTasks.Rows.Add(dr);
            dtTasks.AcceptChanges();
        }
        else
        {
            DataRow[] drs = dtTasks.Select("Task_TableId=" + Key.ToString());
            foreach(DataRow dr in drs)
            {
                dr["TaskName"] = txtTaskName.Text.Trim();
            }
            dtTasks.AcceptChanges();
        }
        BindTasks();
        dv_NewEditTask.Visible = false;
    }
    protected void btnCloseTask_Click(object sender, EventArgs e)
    {dv_NewEditTask.Visible = false;}  

    protected void FillResidual_Click(object sender, EventArgs e)
    {
        int Level = (Common.CastAsInt32(ddlR11.SelectedValue) * Common.CastAsInt32(ddlR12.SelectedValue));
        lblR13.Text = ((Level == 0)? "" : Level.ToString());

        tdRisk.Attributes.Add("class", GetCSSColor(lblR13.Text));

        lblDetails1.Text = GetSeverityText(Common.CastAsInt32(ddlR11.SelectedValue));
        lblDetails2.Text = GetLikelihoodText(Common.CastAsInt32(ddlR12.SelectedValue));
        lblDetails3.Text = GetAction(GetCSSColor(lblR13.Text));
    }
    protected void FillReResidual_Click(object sender, EventArgs e)
    {
        int Level = (Common.CastAsInt32(ddlReR11.Text) * Common.CastAsInt32(ddlReR12.Text));
        lblReR13.Text = ((Level == 0) ? "" : Level.ToString());

        tdReRisk.Attributes.Add("class", GetCSSColor(lblReR13.Text));

        lblDetails11.Text = GetSeverityText(Common.CastAsInt32(ddlReR11.SelectedValue));
        lblDetails12.Text = GetLikelihoodText(Common.CastAsInt32(ddlReR12.SelectedValue));
        lblDetails13.Text = GetAction(GetCSSColor(lblReR13.Text));
    }
    protected void btnDeleteTask_Click(object sender, EventArgs e)
    {
        int _Task_TableId = Common.CastAsInt32(((Button)sender).CommandArgument);
        DataRow[] drs = dtTasks.Select("Task_TableId=" + _Task_TableId);
        foreach (DataRow dr in drs)
        {
            dr["Status"] = "D";
            DataRow[] drs1 = dtHazards.Select("Task_TableId=" + dr["Task_TableId"].ToString());
            foreach (DataRow dr1 in drs)
            {
                dr1["Status"] = "D";
            }
        }
        dtTasks.AcceptChanges();
        dtHazards.AcceptChanges();

        BindTasks();
    }
    protected void btnDelteHazard_Click(object sender, EventArgs e)
    {
        int _TableId = Common.CastAsInt32(((Button)sender).CommandArgument);
        int Task_TableId = 0;
        DataRow[] drs1 = dtHazards.Select("TableId=" + _TableId);
        foreach (DataRow dr1 in drs1)
        {
            Task_TableId = Common.CastAsInt32(dr1["Task_TableId"]);
            dr1["Status"] = "D";
        }

        DataView dv = dtHazards.DefaultView;
        dv.RowFilter = "Task_TableId=" + Task_TableId + " AND Status='A'";

        if (dv.ToTable().Rows.Count <= 0)
        {
            DataRow[] drs = dtTasks.Select("Task_TableId=" + Task_TableId);

            foreach (DataRow dr in drs)
            {
                dr["Status"] = "D";
            }
        }

        BindTasks();

        dv_NewHazard.Visible = false;
    }
    
    protected void btnAddHazard_Click(object sender, EventArgs e)
    {
        ViewState["Task_TableId"]=Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        int Key = Common.CastAsInt32(ViewState["Task_TableId"]);
        if (Key!=0)
        {
            DataRow[] drs = dtTasks.Select("Task_TableId=" + Key.ToString());
            foreach (DataRow dr in drs)
            {
                txtTask.Text = dr["TaskName"].ToString();
            }
            
            dv_NewHazard.Visible = true;
        }
    }
    protected void btnEditHazard_Click(object sender, EventArgs e)
    {
        ViewState["TableId"] = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        btnDelteHazard.CommandArgument = ViewState["TableId"].ToString();
        DataRow[] drs = dtHazards.Select("TableId=" + ViewState["TableId"].ToString());
        foreach (DataRow dr in drs)
        {
            DataView dv = dtTasks.DefaultView;
            dv.RowFilter = "Task_TableId=" + dr["Task_TableId"];

            txtTask.Text = dv.ToTable().Rows[0]["TaskName"].ToString();
            txtHazard.Text = dr["HazardName"].ToString();
            txtStdCM.Text = dr["ControlMeasures"].ToString();
            ddlR11.SelectedValue = dr["Severity"].ToString();
            ddlR12.SelectedValue = dr["LikeliHood"].ToString();
            lblR13.Text = dr["RiskLevel"].ToString();
            FillResidual_Click(sender, e);
            txtAddCM.Text = dr["ADD_Control_Measures"].ToString();
            ddlReR11.SelectedValue = dr["Re_Severity"].ToString();
            ddlReR12.SelectedValue = dr["Re_LikeliHood"].ToString();
            lblReR13.Text = dr["Re_RiskLevel"].ToString();
            FillReResidual_Click(sender, e);
            rdoProceed_Y.Checked = (dr["Proceed"].ToString() == "Y");
            rdoProceed_N.Checked = (dr["Proceed"].ToString() == "N");
            txtAgreedtime.Text = dr["AGREED_TIME"].ToString();
            txtPIC.Text = dr["PIC_NAME"].ToString();

            btnDelteHazard.Visible = true;
            dv_NewHazard.Visible = true;
        }

    }
    protected void btnCancelHazard_Click(object sender, EventArgs e)
    {
        txtTask.Text = "";
        txtHazard.Text = "";
        txtStdCM.Text = "";
        ddlR11.SelectedIndex = 0;
        ddlR12.SelectedIndex = 0;
        lblR13.Text = "";
        txtAddCM.Text = "";
        ddlReR11.SelectedIndex = 0;
        ddlReR12.SelectedIndex = 0;
        lblReR13.Text = "";
        rdoProceed_Y.Checked = false;
        rdoProceed_N.Checked = false;
        txtAgreedtime.Text = "";
        txtPIC.Text = "";
        ViewState["TableId"] = "0";

        btnSaveHazard.Visible = true;
        btnDelteHazard.Visible = false;

        dv_NewHazard.Visible = false;
    }
    protected void btnViewHazard_Click(object sender, EventArgs e)
    {
        ViewState["TableId"] = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        btnDelteHazard.CommandArgument = ViewState["TableId"].ToString();
        DataRow[] drs = dtHazards.Select("TableId=" + ViewState["TableId"].ToString());
        foreach (DataRow dr in drs)
        {
            DataView dv = dtTasks.DefaultView;
            dv.RowFilter = "Task_TableId=" + dr["Task_TableId"];

            txtTask.Text = dv.ToTable().Rows[0]["TaskName"].ToString();
            txtTask.Enabled = false;
            txtHazard.Text = dr["HazardName"].ToString();
            txtStdCM.Text = dr["ControlMeasures"].ToString();
            ddlR11.Text = dr["Severity"].ToString();
            ddlR12.Text = dr["LikeliHood"].ToString();
            lblR13.Text = dr["RiskLevel"].ToString();
            FillResidual_Click(sender, e);
            txtAddCM.Text = dr["ADD_Control_Measures"].ToString();
            ddlReR11.SelectedValue = dr["Re_Severity"].ToString();
            ddlReR12.SelectedValue = dr["Re_LikeliHood"].ToString();
            lblReR13.Text = dr["Re_RiskLevel"].ToString();
            FillReResidual_Click(sender, e);
            rdoProceed_Y.Checked = (dr["Proceed"].ToString() == "Y");
            rdoProceed_N.Checked = (dr["Proceed"].ToString() == "N");
            txtAgreedtime.Text = dr["AGREED_TIME"].ToString();
            txtPIC.Text = dr["PIC_NAME"].ToString();

            btnSaveHazard.Visible = false;
            btnDelteHazard.Visible = false;
            dv_NewHazard.Visible = true;
        }

    }
    protected void btnSaveHazard_Click(object sender, EventArgs e)
    {
        if (Common.CastAsInt32(lblReR13.Text) > 10 && rdoProceed_Y.Checked)
        {
            lblMess.Text = "If risk level is 10 or more after addition control measures then decision to proceed with work select No.";
            return;
        }
        if ( txtHazard.Text=="" ||  ddlR11.SelectedIndex == 0 || ddlR12.SelectedIndex == 0 || lblR13.Text == "" || txtStdCM.Text == "" || ddlReR11.SelectedIndex == 0 || ddlReR12.SelectedIndex == 0 || lblReR13.Text == "" || txtAddCM.Text == "" || (!rdoProceed_Y.Checked && !rdoProceed_N.Checked))
        {
            lblMess.Text = "All fields are manditory. Please fill the safety control measure and select severity, likelihood, Proceed.";
            return;
        }
        try
        {
            int Key = Common.CastAsInt32(ViewState["TableId"]);
            if (Key == 0)
            {
                DataRow dr1 = dtHazards.NewRow();
                dr1["TableId"] = -(Common.CastAsInt32(dtHazards.Rows.Count) + 1);
                dr1["VesselCode"] = VesselCode;
                dr1["Task_TableId"] = Common.CastAsInt32(ViewState["Task_TableId"]);
                dr1["RiskId"] = RiskId;
                dr1["HazardId"] = 0;
                dr1["HazardCode"] = "";
                dr1["HazardName"] = txtHazard.Text;
                dr1["ControlMeasures"] = txtStdCM.Text.Trim();
                dr1["Severity"] = Common.CastAsInt32(ddlR11.Text);
                dr1["LikeliHood"] = Common.CastAsInt32(ddlR12.Text);
                dr1["RiskLevel"] = Common.CastAsInt32(lblR13.Text);
                dr1["ADD_Control_Measures"] = txtAddCM.Text.Trim();
                dr1["Re_Severity"] = Common.CastAsInt32(ddlReR11.Text);
                dr1["Re_LikeliHood"] = Common.CastAsInt32(ddlReR12.Text);
                dr1["Re_RiskLevel"] = Common.CastAsInt32(lblReR13.Text);
                dr1["Proceed"] = (rdoProceed_Y.Checked ? "Y" : "N");
                dr1["PIC_NAME"] = txtPIC.Text.Trim();
                dr1["AGREED_TIME"] = txtAgreedtime.Text.Trim();
                dr1["Status"] = "A";
                dtHazards.Rows.Add(dr1);
            }
            else
            {
                DataRow[] dr1s = dtHazards.Select("TableId=" + Key);
                if (dr1s.Length > 0)
                {
                    DataRow dr1 = dr1s[0];
                    dr1["HazardName"] = txtHazard.Text;
                    dr1["ControlMeasures"] = txtStdCM.Text.Trim();
                    dr1["Severity"] = Common.CastAsInt32(ddlR11.Text);
                    dr1["LikeliHood"] = Common.CastAsInt32(ddlR12.Text);
                    dr1["RiskLevel"] = Common.CastAsInt32(lblR13.Text);                   
                    dr1["ADD_Control_Measures"] = txtAddCM.Text.Trim();
                    dr1["Re_Severity"] = Common.CastAsInt32(ddlReR11.Text);
                    dr1["Re_LikeliHood"] = Common.CastAsInt32(ddlReR12.Text);
                    dr1["Re_RiskLevel"] = Common.CastAsInt32(lblReR13.Text);
                    dr1["Proceed"] = (rdoProceed_Y.Checked ? "Y" : "N");
                    dr1["PIC_NAME"] = txtPIC.Text.Trim();
                    dr1["AGREED_TIME"] = txtAgreedtime.Text.Trim();
                    dr1["Status"] = "A";
                    
                }
            }
            
            dtHazards.AcceptChanges();
            BindTasks();

            //--------------------------------- 
            if (Key == 0)
            {
                btnCancelHazard_Click(sender, e);
            }
            //--------------------------------- 
            lblMess.Text = "Record saved/ updated successfully.";
        }
        catch (Exception ex)
        {
            lblMess.Text = "Unable to add/ update. Error : " + ex.Message.ToString();
        }
    }


    public void AddTaskFromDB(DataTable _dtTasks, DataTable _dtHazards)
    {
        foreach (DataRow drt in _dtTasks.Rows)
        {
            DataRow dr = dtTasks.NewRow();
            int _Task_TableId = -(Common.CastAsInt32(dtTasks.Rows.Count) + 1);

            dr["VesselCode"] = VesselCode;
            dr["Task_TableId"] = _Task_TableId;
            dr["RiskId"] = 0;
            dr["TaskId"] = drt["TaskId"].ToString();
            dr["TaskCode"] = drt["TaskCode"].ToString();
            dr["TaskName"] = drt["TaskName"].ToString();
            dr["Status"] = "A";

            dtTasks.Rows.Add(dr);
            DataRow[] drHazs = _dtHazards.Select("Task_TableId=" + drt["Task_TableId"].ToString());
            foreach (DataRow drt1 in drHazs)
            {
                DataRow dr1 = dtHazards.NewRow();


                dr1["TableId"] = -(Common.CastAsInt32(dtHazards.Rows.Count) + 1);
                dr1["VesselCode"] = VesselCode;
                dr1["Task_TableId"] = _Task_TableId;
                dr1["RiskId"] = 0;
                dr1["HazardId"] = drt1["HazardId"];
                dr1["HazardCode"] = drt1["HazardCode"];
                dr1["HazardName"] = drt1["HazardName"];
                dr1["ControlMeasures"] = drt1["ControlMeasures"];
                dr1["Severity"] = drt1["Severity"];
                dr1["LikeliHood"] = drt1["LikeliHood"];
                dr1["RiskLevel"] = drt1["RiskLevel"];

                dr1["ADD_Control_Measures"] = drt1["ADD_Control_Measures"];
                dr1["Re_Severity"] = drt1["Re_Severity"];
                dr1["Re_LikeliHood"] = drt1["Re_LikeliHood"];
                dr1["Re_RiskLevel"] = drt1["Re_RiskLevel"];

                dr1["Proceed"] = drt1["Proceed"];
                dr1["AGREED_TIME"] = drt1["AGREED_TIME"];
                dr1["PIC_NAME"] = drt1["PIC_NAME"];

                //dr1["Proceed"] = DBNull.Value;
                //dr1["AGREED_TIME"] = DBNull.Value;
                //dr1["PIC_NAME"] = DBNull.Value;

                dr1["Status"] = "A";
                dtHazards.Rows.Add(dr1);
            }
        }
        dtTasks.AcceptChanges();
    }     
    public string GetCSSColor(object RiskLevel)
    {
        int Level = Common.CastAsInt32(RiskLevel);
        string Color = "";

        if (Level == 0)
        {
            return Color;
        }

        if (Level >= 16)
        {
            Color = "r";
        }
        else if (Level >= 12)
        {
            Color = "a";
        }
        else if (Level >= 8)
        {
            Color = "b";
        }
        else
        {
            Color = "g";
        }

        return Color;
    }
    protected void btnCloseGL_Click(object sender, EventArgs e)
    {
        dv_GuideLines.Visible = false;
    }
    protected void btnOpenGL_Click(object sender, EventArgs e)
    {
        dv_GuideLines.Visible = true;
    }
    protected void ddlAMW_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDetails.Enabled = (ddlAMW.SelectedValue.Trim() == "Y");
        txtDetails.Text = "";
    }
    public string GetAction(string Color)
    {
        string Action = "";

        if (Color == "")
        {
            return Action;
        }

        if (Color == "r")
        {
            Action = "Do not undertake task.If operation is already in progress, abort and inform office.";
        }
        else if (Color == "a")
        {
            Action = "Job to be under taken only with office Approval.";
        }
        else if (Color == "b")
        {
            Action = "Job can be under taken by ship staff with direct supervision of Master and/ or Chief Engineer.";
        }
        else
        {
            Action = "Job can be under taken by ship staff.";
        }

        return Action;
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        object Re_RiskLevel = dtHazards.Compute("MAX(Re_RiskLevel)", "");
        if (Common.CastAsInt32(Re_RiskLevel) <= 10)
        {
            dvApproveDetails.Visible = true;
            btnSaveApprove.Visible = true;
        }
        else
        {
            string ResColor = GetCSSColor(Re_RiskLevel);
            btnSaveApprove.Visible = false;
            lblMsg_Approve.Text = GetAction(ResColor);
            dvApproveDetails.Visible = false;
        }
        dv_Approve.Visible = true;
    }
    protected void btnSaveApprove_Click(object sender, EventArgs e)
    {
        if (txtReviewerComments.Text.Trim() == "")
        {
            txtReviewerComments.Focus();
            lblMsg_Approve.Text = "Please enter comments.";
            return;
        }
        if (txtReviewerName.Text.Trim() == "")
        {
            txtReviewerName.Focus();
            lblMsg_Approve.Text = "Please enter name.";
            return;
        }

        try
        {
            string SQL = "UPDATE [dbo].[EV_VSL_RiskMaster] SET [ReviewComment]='" + txtReviewerComments.Text.Replace("'", "`").ToString() + "', [ReviewerName] = '" + txtReviewerName.Text.Trim() + "', [ReviewDate]=GETDATE() WHERE RISKID= " + RiskId + " AND [VESSELCODE] = '" + VesselCode + "' ";
            Common.Execute_Procedures_Select_ByQuery(SQL);
            ShowMasterDetails();
            btnCloseApprove_Click(sender, e);
            lblMsg.Text = "Review saved successfully.";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "refresh", "RefreshParent();", true);

        }
        catch (Exception ex)
        {
            lblMsg_Approve.Text = "Unable to save review. Error : " + ex.Message;
        }
    }
    protected void btnCloseApprove_Click(object sender, EventArgs e)
    {
        txtReviewerComments.Text = "";
        txtReviewerName.Text = "";
        dv_Approve.Visible = false;
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (RiskId > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "report", "window.open('../Reports/RCAPrint.aspx?RId=" + RiskId + "','');", true);
        }
        else
        {
            lblMsg.Text = "Please save data before printing report.";
        }
    }


    public string GetSeverityText(int Severity)
    {
        string Text = "";

        switch (Severity)
        {
            case 1: Text = " No effect on reputation<br /> Negligible economic loss which can be restored <br />* Nill to sea : contained onboard";
                break;
            case 2: Text = " Small reduction of reputation in the short run<br /> Economic loss upto US$10,000 which can be restored <br />* Sheen on sea : evidance of loss to sea";
                break;
            case 3: Text = " Reduction of reputation that may influence trust and respect<br /> Economic loss between US$10,000 and US$100,000 which can be restored <br />* Less than 1 m3 to sea";
                break;
            case 4: Text = " Serious loss of reputation that will influence trust and respect for a long time<br /> Lagre economic loss more than US$100,000 that can be restored <br />* 1 to 5 m3 to sea";
                break;
            case 5: Text = " Serious loss of reputation which is devastating for trust and respect<br /> Considerable economic loss which can not be restored<br />* More than 5 m3 to sea   ";
                break;
            default: Text = "";
                break;

        }

        return Text;
    }
    public string GetLikelihoodText(int Likelihood)
    {
        string Text = "";

        switch (Likelihood)
        {
            case 1: Text = " Never heard within the industry ";
                break;
            case 2: Text = " Occurs less than 0.1% of the time/ cases ";
                break;
            case 3: Text = " Occurs between 0.1% and 1% of the time/ cases ";
                break;
            case 4: Text = " Occurs between 1% and 10% of the time/ cases ";
                break;
            case 5: Text = " More frequently than 10% of the time/ cases ";
                break;
            default: Text = "";
                break;

        }

        return Text;
    }
    
}