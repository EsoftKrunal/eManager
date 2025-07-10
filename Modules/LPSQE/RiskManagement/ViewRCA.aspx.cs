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

public partial class ViewRCA : System.Web.UI.Page
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

        if (!IsPostBack)
        {
            VesselCode = Request.QueryString["VesselCode"];
            UserName = Session["UserName"].ToString().Trim();
            CreateTables();
            ShowMasterDetails();
        }
    }
    public void ShowMasterDetails()
    {
        TemplateId = Common.CastAsInt32(Request.QueryString["TemplateId"]);
        RiskId = Common.CastAsInt32(Request.QueryString["RiskId"]);
        if (RiskId > 0) // LOAD DATA FROM EXISTING DATA
        {

            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT R.*, (SELECT TemplateCode FROM [dbo].[EV_TemplateMaster] WHERE TemplateId=R.TemplateId ) AS TemplateCode ,(SELECT OfficeApprovalRequired FROM [dbo].[EV_TemplateMaster] WHERE TEMPLATECODE+'/'+convert(varchar,REVISIONNO)=R.TemplateCode_Revision ) AS OfficeApprovalRequired FROM [dbo].vw_EV_VSL_RiskMaster R WHERE R.RiskId=" + RiskId + " And VesselCode='" + VesselCode + "'");
            if (dt != null && dt.Rows.Count > 0)
            {
                lblVesselCode.Text = dt.Rows[0]["VesselName"].ToString();
                lblRevNo.Text = dt.Rows[0]["TemplateCode_Revision"].ToString();
                lblRefNo.Text = dt.Rows[0]["REFNO"].ToString();
                txtEventDate.Text = Common.ToDateString(dt.Rows[0]["EVENTDATE"]);
                lblEventName.Text = dt.Rows[0]["EventCode"].ToString() + " : " + dt.Rows[0]["EventName"].ToString();
                string mode=dt.Rows[0]["ALTERNATEMETHODS"].ToString();
                lblAMW.Text=(mode=="Y")?"Yes":(mode=="N")?"No":"NA";
                txtDetails.Text = dt.Rows[0]["Details"].ToString();
                lblHODComments.Text = dt.Rows[0]["Comment"].ToString();
                lblHodName.Text = dt.Rows[0]["HOD_NAME"].ToString();
                lblHODDt.Text = Common.ToDateString(dt.Rows[0]["HODDate"]);
                TemplateId = Common.CastAsInt32(dt.Rows[0]["TemplateId"]);
		//lblOVMessage.Text="Office Approval " + ( (dt.Rows[0]["Verify_Needed"].ToString()=="Y")?"Required "  :"Not Required") ;


		string OfficeApprovalRequired = Convert.IsDBNull(dt.Rows[0]["OfficeApprovalRequired"]) ? "N" : dt.Rows[0]["OfficeApprovalRequired"].ToString();
                if(OfficeApprovalRequired=="N")
                { OfficeApprovalRequired = ((dt.Rows[0]["Verify_Needed"].ToString() == "Y") ? "Y" : "N"); }

                lblOVMessage.Text = "Office Approval " + ((OfficeApprovalRequired == "Y") ? "Required " : "Not Required");

		dtTasks = Common.Execute_Procedures_Select_ByQuery("SELECT *, 'A' As Status FROM [dbo].[EV_VSL_Risk_Tasks] WHERE VesselCode='" + VesselCode + "' AND RiskId=" + RiskId);
            dtHazards = Common.Execute_Procedures_Select_ByQuery("SELECT *, 'A' As Status FROM [dbo].[EV_VSL_Risk_Details] WHERE VesselCode='" + VesselCode + "' AND RiskId=" + RiskId);
            BindTasks();

            object Re_RiskLevel = dtHazards.Compute("MAX(Re_RiskLevel)", "");


            if (dt != null && dt.Rows.Count > 0 && OfficeApprovalRequired=="Y")
            {
                btnApprove.Visible = (Convert.IsDBNull(dt.Rows[0]["COMMENTDATE"]));

                lblApproverComments.Text = dt.Rows[0]["OFFICE_COMMENTS"].ToString();
                lblAppName.Text = dt.Rows[0]["OFFICECOMMENTBY"].ToString();
                lblAppDt.Text = Common.ToDateString(dt.Rows[0]["COMMENTDATE"]);
            }
            else
            {
                lblApproverComments.Text = dt.Rows[0]["ReviewComment"].ToString();
                lblAppName.Text = dt.Rows[0]["ReviewerName"].ToString();
                lblAppDt.Text = Common.ToDateString(dt.Rows[0]["ReviewDate"]);
            }

            }

            

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
    public DataTable BindHazards(int Task_TableId)
    {
        DataView dv = dtHazards.DefaultView;
        dv.RowFilter = "Status='A' AND Task_TableId=" + Task_TableId;
        return dv.ToTable();
    }
    public void ShowExtResRisk()
    {
        object RiskLevel = dtHazards.Compute("MAX(RiskLevel)", "");
        string ExtColor = GetCSSColor(RiskLevel);
        imgER.ImageUrl = "~/HSSQE/Images/" + ExtColor + ".png";
        lblExtAction.Text = GetAction(ExtColor);

        object Re_RiskLevel = dtHazards.Compute("MAX(Re_RiskLevel)", "");
        string ResColor = GetCSSColor(Re_RiskLevel);
        imgRR.ImageUrl = "~/HSSQE/Images/" + ResColor + ".png";
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
        dv_NewHazard.Visible = true;
    }
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
    protected void btnDelteTask_Click(object sender, EventArgs e)
    {
        int _Task_TableId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
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
        BindTasks();
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

        hfdHazardId.Value = "";

        dv_NewHazard.Visible = false;
    }
    protected void btnViewHazard_Click(object sender, EventArgs e)
    {
        int _TableId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataRow[] drs = dtHazards.Select("TableId=" + _TableId);
        foreach (DataRow dr in drs)
        {
            DataView dv = dtTasks.DefaultView;
            dv.RowFilter = "Task_TableId=" + dr["Task_TableId"];

            txtTask.Text = dv.ToTable().Rows[0]["TaskName"].ToString();
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

            dv_NewHazard.Visible = true;
        }

    }         
    protected void btnSaveSingle_Click(object sender, EventArgs e)
    {
        if (ddlReR11.SelectedValue == "" || ddlReR12.SelectedValue == "" || lblReR13.Text == "" || txtAddCM.Text == "" || (!rdoProceed_Y.Checked && !rdoProceed_N.Checked))
        {
            lblMess.Text = "All fields are manditory. Please fill the safety control measure and select severity, likelihood, Proceed.";
            return;
        }

        try
        {

            if (Common.CastAsInt32(hfdHazardId.Value) != 0)
            {
                DataRow[] dr1s = dtHazards.Select("TableId=" + hfdHazardId.Value);
                if (dr1s.Length > 0)
                {
                    DataRow dr1 = dr1s[0];
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
            else
            {
                int Task_TableId = 0;

                DataView dv = dtTasks.DefaultView;
                dv.RowFilter = "TaskName='" + txtTask.Text.Trim() + "' AND Status='A'";
                if (dv.ToTable().Rows.Count > 0)
                {
                    Task_TableId = Common.CastAsInt32(dv.ToTable().Rows[0]["Task_TableId"]);
                }
                else
                {
                    DataRow dr = dtTasks.NewRow();
                    Task_TableId = -(Common.CastAsInt32(dtTasks.Rows.Count) + 1);

                    dr["VesselCode"] = VesselCode;
                    dr["Task_TableId"] = Task_TableId;
                    dr["RiskId"] = 0;
                    dr["TaskId"] = 0;
                    dr["TaskCode"] = "";
                    dr["TaskName"] = txtTask.Text;
                    dr["Status"] = "A";

                    dtTasks.Rows.Add(dr);
                    dtTasks.AcceptChanges();
                }

                DataRow dr1 = dtHazards.NewRow();
                
                dr1["TableId"] = -(Common.CastAsInt32(dtHazards.Rows.Count) + 1);
                dr1["VesselCode"] = VesselCode;
                dr1["Task_TableId"] = Task_TableId;
                dr1["RiskId"] = 0;
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

            dtHazards.AcceptChanges();
            BindTasks();

            //--------------------------------- 
            if (Common.CastAsInt32(hfdHazardId.Value) == 0)
            {
                hfdHazardId.Value = "0";
                ddlR11.SelectedValue = "";
                ddlR12.SelectedValue = "";
                lblR13.Text = "";

                ddlReR11.SelectedValue = "";
                ddlReR12.SelectedValue = "";
                lblReR13.Text = "";
            }
            //--------------------------------- 
            lblMess.Text = "Record saved/ updated successfully.";
        }
        catch (Exception ex)
        {
            lblMess.Text = "Unable to add/ update. Error : " + ex.Message.ToString();
        }
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
            string SQL = "UPDATE [dbo].[EV_VSL_RiskMaster] SET [OFFICE_COMMENTS]='" + txtReviewerComments.Text.Replace("'", "`").ToString() + "', [OFFICECOMMENTBY] = '" + txtReviewerName.Text.Trim() + "', [COMMENTDATE]=GETDATE() WHERE RISKID= " + RiskId + " AND [VESSELCODE] = '" + VesselCode + "' ";
            Common.Execute_Procedures_Select_ByQuery(SQL);
            ShowMasterDetails();
            btnCloseApprove_Click(sender, e);
            lblMsg.Text = "Review saved successfully.";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "refresh", "RefreshParent();", true);

        }
        catch (Exception ex)
        {
            lblMsg_Approve.Text = "Unable to approve. Error : " + ex.Message;
        }
    }
    protected void btnCloseApprove_Click(object sender, EventArgs e)
    {
        txtReviewerComments.Text = "";
        txtReviewerName.Text = "";
        dv_Approve.Visible = false;
    }
    protected void rptHazards_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView dr = (DataRowView)e.Item.DataItem;
            int TaskId = 0;
            DataRow[] drs=dtTasks.Select("Task_TableId=" + Common.CastAsInt32(dr.Row["Task_TableId"]));
            if (drs.Length > 0)
                TaskId = Common.CastAsInt32(drs[0]["TaskId"]);

            int HazardId = Common.CastAsInt32(dr.Row["HazardId"]);

            string SQL = "select * from [dbo].[EV_TemplateDetails] D where D.HAZARDID="  + HazardId + " AND TASK_TABLEID IN (SELECT TASK_TABLEID FROM [dbo].[EV_Template_Tasks] T WHERE T.TemplateId=" + TemplateId + " AND T.TaskId=" + TaskId + ")"; 
            
            string ECM = "",S="",L="",R="";
            DataTable dtTask = Common.Execute_Procedures_Select_ByQuery(SQL);
            Image imgCM = (Image)e.Item.FindControl("imgCM");
            Image imgSev = (Image)e.Item.FindControl("imgSev");
            Image imgLik = (Image)e.Item.FindControl("imgLik");
            Image imgRisk = (Image)e.Item.FindControl("imgRisk");

            if (dtTask.Rows.Count > 0)
            {
                ECM = dtTask.Rows[0]["ControlMeasures"].ToString();
                S = dtTask.Rows[0]["Severity"].ToString();
                L = dtTask.Rows[0]["LikeliHood"].ToString();
                R = dtTask.Rows[0]["RiskLevel"].ToString();

                imgCM.Visible = (ECM != dr.Row["ControlMeasures"].ToString().Trim());
                imgSev.Visible = (S != dr.Row["Severity"].ToString().Trim());
                imgLik.Visible = (L != dr.Row["LikeliHood"].ToString().Trim());
                imgRisk.Visible = (R != dr.Row["RiskLevel"].ToString().Trim());
            }
            else
            {
                imgCM.Visible = false;
                imgSev.Visible =false;
                imgLik.Visible = false;
                imgRisk.Visible = false;
            }
            
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
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (RiskId > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "print", "window.open('RCAPrint.aspx?RId=" + RiskId + "&VSL=" + VesselCode + "','');", true);
        }
        else
        {
            lblMsg.Text = "Data does not exist.";
        }
    }

}