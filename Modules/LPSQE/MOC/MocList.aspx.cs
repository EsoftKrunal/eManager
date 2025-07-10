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
using System.Linq;
using System.IO;

public partial class HSSQE_MOC_MocList : System.Web.UI.Page
{
    public int StageId
    {
        get { return Common.CastAsInt32(ViewState["StageId"]); }
        set { ViewState["StageId"] = value; }
    }
    public int SelMocID
    {
        get { return Common.CastAsInt32(ViewState["_SelMocID"]); }
        set { ViewState["_SelMocID"] = value; }
    }
    public int TableID
    {
        get { return Common.CastAsInt32(ViewState["_TableID"]); }
        set { ViewState["_TableID"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblMsgMOCRequestApproval.Text = "";
        lblMsgStageApprovalOfMOC.Text = "";
        lblMsgDetailUpdate.Text = "";
        lblMsgPhase1.Text = "";
        lblMsgPhase2.Text = "";
        lblMsgCancelRequest.Text = "";
        if (!IsPostBack)
        {
            StageId = Common.CastAsInt32(Request.QueryString["StageId"]);
            lblStageName.Text = Request.QueryString["StageName"];
            BindMocRequest();
            Bind_ResponsiblePerson();
        }
    }
    public void BindMocRequest()
    {

        string SQL = " Select * from dbo.vw_MOC_RECORD where StageId=" + StageId + " order by MOCNumber";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        rptMOC.DataSource = dt;
        rptMOC.DataBind();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        int MocId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Edit", "window.open('MOCRecord.aspx?MOCId='+ " + MocId + ", '_blank', '');", true);
    }

    
    protected void btnOpenPopupUpdateStage_OnClick(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        HiddenField hfdMocID = (HiddenField)btn.Parent.FindControl("hfdMocID");
        HiddenField hfdStageID = (HiddenField)btn.Parent.FindControl("hfdStageID");
        Label lblTopicgrid=(Label)btn.Parent.FindControl("lblTopic");

        SelMocID = Common.CastAsInt32(hfdMocID.Value);
        StageId = Common.CastAsInt32(hfdStageID.Value);
        lblStagePopupHeading.Text = Page.Request.QueryString["StageName"].ToString();
        lblTopic.Text = lblTopicgrid.Text;
        dv_UpdateStage.Visible = true;

        if (StageId == 10)
        {
            divStage2.Visible = true;
        }
        else if (StageId == 15)
        {
            divStage3.Visible = true;
        }
        else if (StageId == 20)
        {
            divStage4.Visible = true;
        }
        else if (StageId == 25)
        {
            divStage5_Pahse1.Visible = true;
        }
        else if (StageId == 30)
        {
            divStage6_Pahse2.Visible = true;
        }
        else if (StageId == 35)
        {
            divStage7_Endorsement.Visible = true;
        }
        else if (StageId == 40)
        {
            divStage8_Review.Visible = true;
        }






    }
    protected void btnClosePopup_Click(object sender, EventArgs e)
    {
        dv_UpdateStage.Visible = false;
    }

    protected void btnOpenPopupCancelStage_Click(object sender, EventArgs e)
    {
        divClosurePopup.Visible = true;
    }
    //Stage 2----------------------------
    protected void btnSaveStageRequestApproval_Click(object sender, EventArgs e)
    {
        if (ddlDetailsChangeDefined.SelectedIndex==0)
        {
            lblMsgMOCRequestApproval.Text = " Please select details of change adequately defined.";
            return;
        }
        if (ddlReasonOfChangeDefined.SelectedIndex == 0)
        {
            lblMsgMOCRequestApproval.Text = "  Please select reasons of change adequately defined.";
            return;
        }
        if (ddlIsChangeNecessary.SelectedIndex == 0)
        {
            lblMsgMOCRequestApproval.Text = " Please select change necessary.";
            return;
        }
        if (ddlIsRecomendationGiven.SelectedIndex == 0)
        {
            lblMsgMOCRequestApproval.Text = "Please select recommendation given to proced with risk assessment.";
            return;
        }
        if (txtDateTobeCompleted.Text.Trim()=="")
        {
            lblMsgMOCRequestApproval.Text = "Please enter summary of Changes to be completed by.";
            return;
        }
        if (ddlResponsiblePerson.SelectedIndex==0)
        {
            lblMsgMOCRequestApproval.Text = "Please select responsible person.";
            return;
        }

        try
        {
            Common.Set_Procedures("dbo.MOC_Moc_RequestApproval");
            Common.Set_ParameterLength(10);
            Common.Set_Parameters(
                new MyParameter("@MocID", SelMocID),
                new MyParameter("@atc_DetailsOfChange", ddlDetailsChangeDefined.SelectedValue),
                new MyParameter("@atc_ReasionForChange", ddlReasonOfChangeDefined.SelectedValue),
                new MyParameter("@atc_ChangeNececssary", ddlIsChangeNecessary.SelectedValue),
                new MyParameter("@atc_RiskAssessment", ddlIsRecomendationGiven.SelectedValue),
                new MyParameter("@atc_ChangeCompletedTargetDate", txtDateTobeCompleted.Text.Trim()),
                new MyParameter("@atc_ChangeAssignedTo", ddlResponsiblePerson.SelectedValue),
                new MyParameter("@ClosedBy", Session["loginid"]),
                new MyParameter("@ClosureComments", txtForwardedByComments.Text.Trim()),
                new MyParameter("@WaitingBy", ddlResponsiblePerson.SelectedValue)
                );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);

            if (res)
            {
                
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "aa", "alert('MOC Request update successfully.')", true);
                BindMocRequest();
                ClearRequestForApprovalStage();
                dv_UpdateStage.Visible = false;
            }
            else
            {
                //lblMsg.Text = "Unable to create MOC Request.Error :" + Common.getLastError();
            }

        }
        catch (Exception ex)
        {

            //lblMsg.Text = "Unable to create MOC Request.Error :" + ex.Message.ToString();
        }
    }
    protected void btnCloseStageRequestApproval_Click(object sender, EventArgs e)
    {
        dv_UpdateStage.Visible = false;
    }

    public void Bind_ResponsiblePerson()
    {
        string SQL = " select LoginId,FirstName+' '+LastName as UserName from dbo.UserMaster order by UserId ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        ddlResponsiblePerson.DataSource = dt;
        ddlResponsiblePerson.DataTextField = "UserName";
        ddlResponsiblePerson.DataValueField = "LoginId";
        ddlResponsiblePerson.DataBind();
        ddlResponsiblePerson.Items.Insert(0,new ListItem("Select", ""));
        
    }
    public void ClearRequestForApprovalStage()
    {
        ddlDetailsChangeDefined.SelectedIndex = 0;
        ddlReasonOfChangeDefined.SelectedIndex = 0;
        ddlIsChangeNecessary.SelectedIndex = 0;
        ddlIsRecomendationGiven.SelectedIndex = 0;
        txtDateTobeCompleted.Text = "";
        ddlResponsiblePerson.SelectedIndex = 0;
        txtForwardedByComments.Text = "";
    }

    //Stage 3----------------------------
    protected void btnSaveStageDetailsUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlChangeType.SelectedIndex == 0)
            {
                lblMsgStageApprovalOfMOC.Text = "Please select change type";
                return;
            }
            if (txtDateOfRiskAssessment.Text.Trim()=="")
            {
                lblMsgStageApprovalOfMOC.Text = "Please enter date of risk assessment";
                return;
            }
            if (txtResultOfRiskAssessment.Text.Trim() == "")
            {
                lblMsgStageApprovalOfMOC.Text = "Please enter Result of risk assessment";
                return;
            }
            if (ddlImpaceOfChangeOnEnvironment.SelectedIndex == 0)
            {
                if (txtImpactOfChangeOnEnvironment.Text.Trim() == "")
                {
                    lblMsgStageApprovalOfMOC.Text = "Please enter Impact of change on environment.";
                    return;
                }
            }

            if (ddlImpaceOfChangeOnSafety.SelectedIndex==0)
            {
                if (txtImpactOfChangeOnSafety.Text.Trim() == "")
                {
                    lblMsgStageApprovalOfMOC.Text = "Please enter impact of change on safety.";
                    return;
                }
            }
            if (txtManualNeedAnUpdated.Text.Trim() == "")
            {
                lblMsgStageApprovalOfMOC.Text = "Please enter what Manuals/Procedures/Drawings will need an update.";
                return;
            }
            if (txtKeyMitigatingAction.Text.Trim() == "")
            {
                lblMsgStageApprovalOfMOC.Text = "Please enter list of key mitigating actions.";
                return;
            }
            if (txtEstimatedCostOfChange.Text.Trim() == "")
            {
                lblMsgStageApprovalOfMOC.Text = "Please enter estimated cost of change including all mitigating measures.";
                return;
            }
            if (txtIdentifyPersonEffectedByChange.Text.Trim() == "")
            {
                lblMsgStageApprovalOfMOC.Text = "Please enter person/teams/vessels effected by change.";
                return;
            }
            
            if (txtApprovalCommentsDetailsUpdate.Text.Trim() == "")
            {
                lblMsgStageApprovalOfMOC.Text = "Please enter Comments.";
                return;
            }
            if (txtTargetDateForCompletion.Text.Trim() == "")
            {
                lblMsgStageApprovalOfMOC.Text = "Please enter terget date for completion.";
                return;
            }

            Common.Set_Procedures("dbo.MOC_Moc_DetailsUpdate");
            Common.Set_ParameterLength(16);
            Common.Set_Parameters(
                new MyParameter("@MocID", SelMocID),
                new MyParameter("@ChangeType", ddlChangeType.SelectedValue),                
                new MyParameter("@soc_RiskAssessmentDate", txtDateOfRiskAssessment.Text.Trim()),
                new MyParameter("@soc_ResultOfRiskAssessment", txtResultOfRiskAssessment.Text.Trim()),
                new MyParameter("@soc_ImpactChangeSafety", ddlImpaceOfChangeOnSafety.SelectedValue),
                new MyParameter("@soc_ImpactChangeSafetyDetails", txtImpactOfChangeOnSafety.Text.Trim()),
                new MyParameter("@soc_ImpactChangesEnvironment", ddlImpaceOfChangeOnEnvironment.SelectedValue),
                new MyParameter("@soc_ImpactChangesEnvironmentDetails", txtImpactOfChangeOnEnvironment.Text.Trim()),
                new MyParameter("@soc_KeyMitigatingAction", txtKeyMitigatingAction.Text.Trim()),
                new MyParameter("@soc_ManualsForUpdate", txtManualNeedAnUpdated.Text.Trim()),
                new MyParameter("@soc_PersonOrVesselAffectedByChange", txtIdentifyPersonEffectedByChange.Text.Trim()),
                new MyParameter("@soc_EstimatedCostOfChange", txtEstimatedCostOfChange.Text.Trim()),
                new MyParameter("@soc_TargetDateForCompletion", txtTargetDateForCompletion.Text.Trim()),

                new MyParameter("@ClosedBy", Session["LoginID"]),
                new MyParameter("@ClosureComments", txtApprovalCommentsDetailsUpdate.Text.Trim()),
                new MyParameter("@WaitingBy", 0)

                );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);

            if (res)
            {

                ScriptManager.RegisterStartupScript(Page, this.GetType(), "aa", "alert('MOC Request updated successfully.')", true);
                BindMocRequest();
                ClearRequestForApprovalStage();
                dv_UpdateStage.Visible = false;
            }
            else
            {
                //lblMsg.Text = "Unable to create MOC Request.Error :" + Common.getLastError();
            }

        }
        catch (Exception ex)
        {

            //lblMsg.Text = "Unable to create MOC Request.Error :" + ex.Message.ToString();
        }
    }
    protected void ddlImpaceOfChangeOnSafety_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        txtImpactOfChangeOnSafety.Text = "";
        txtImpactOfChangeOnSafety.Visible = ddlImpaceOfChangeOnSafety.SelectedValue == "1";
    }
    protected void ddlImpaceOfChangeOnEnvironment_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        txtImpactOfChangeOnEnvironment.Text = "";
        txtImpactOfChangeOnEnvironment.Visible = ddlImpaceOfChangeOnEnvironment.SelectedValue=="1";
    }

    // Risk assessment Stage 4 ----------------------------
    protected void btnSaveRiskAssessment_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlRiskAssessmentRequired.SelectedIndex == 0)
            {
                if (txtRiskAssessmentNumber.Text.Trim() == "")
                {
                    lblMsgDetailUpdate.Text = "Please enter risk assessment number.";
                    return;
                }
                //if (!fuRiskAssessmentFile.HasFile)
                //{
                //    lblMsgDetailUpdate.Text = "Please select.";
                //    return;
                //}
            }
            string SQL = " SElect impact from dbo.MOC_RECORD where mocID=" + SelMocID;
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
            if (dt.Rows.Count > 0)
            {
                string[] impact = dt.Rows[0][0].ToString().Split(',');
                int cnt =  (impact.Where(a => a == "1")).Count(); // People
                if (cnt > 0)
                {
                    if (txtCommunication.Text.Trim() == "")
                    {
                        lblMsgDetailUpdate.Text = "Please enter communication.";
                        return;
                    }
                    if (txtTraining.Text.Trim() == "")
                    {
                        lblMsgDetailUpdate.Text = "Please enter training.";
                        return;
                    }
                }
                cnt = impact.Where(a => a == "2").Count(); // Process
                if (cnt > 0)
                {
                    if (txtSMSReview.Text.Trim() == "")
                    {
                        lblMsgDetailUpdate.Text = "Please enter SMS review.";
                        return;
                    }
                    if (txtDrawingsManuals.Text.Trim() == "")
                    {
                        lblMsgDetailUpdate.Text = "Please enter Drawings / Manuals .";
                        return;
                    }
                    if (txtDocumentation.Text.Trim() == "")
                    {
                        lblMsgDetailUpdate.Text = "Please enter documents.";
                        return;
                    }
                }
                cnt = impact.Where(a => a == "3").Count(); // Equipment
                if (cnt > 0)
                {
                    if (txtEquipment.Text.Trim() == "")
                    {
                        lblMsgDetailUpdate.Text = "Please enter equipments.";
                        return;
                    }
                }

            }
            
            //string FileName = "";
            //byte[] FileContent = new byte[0];
                 
            //if (fuRiskAssessmentFile.HasFile)
            //{
            //    FileName = fuRiskAssessmentFile.FileName;
            //    FileContent = fuRiskAssessmentFile.FileBytes;
            //}
            Common.Set_Procedures("dbo.MOC_Moc_ApprovalOfMOC");
            Common.Set_ParameterLength(12);
            Common.Set_Parameters(
                new MyParameter("@MocID", SelMocID),

                new MyParameter("@soc_RaRequired", ddlRiskAssessmentRequired.SelectedValue),
                new MyParameter("@soc_RaNumber", txtRiskAssessmentNumber.Text.Trim()),
                //new MyParameter("@soc_RaFileName", FileName),
                //new MyParameter("@soc_RaFileContent", FileContent),
                new MyParameter("@soc_Communication", txtCommunication.Text.Trim()),
                new MyParameter("@soc_Training", txtTraining.Text.Trim()),
                new MyParameter("@soc_SMSReview", txtSMSReview.Text.Trim()),
                new MyParameter("@soc_DrawingManuals", txtDrawingsManuals.Text.Trim()),
                new MyParameter("@soc_Documentation", txtDocumentation.Text.Trim()),
                new MyParameter("@soc_Equipments", txtEquipment.Text.Trim()),

                new MyParameter("@ClosedBy", Session["LoginID"]),
                new MyParameter("@ClosureComments", txtApprovalCommentsDetailsUpdate.Text.Trim()),
                new MyParameter("@WaitingBy", 0)

                );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);

            if (res)
            {

                ScriptManager.RegisterStartupScript(Page, this.GetType(), "aa", "alert('MOC Request updated successfully.')", true);
                BindMocRequest();
                ClearRequestForApprovalStage();
                dv_UpdateStage.Visible = false;
            }
            else
            {
                //lblMsg.Text = "Unable to create MOC Request.Error :" + Common.getLastError();
            }

        }
        catch (Exception ex)
        {

            //lblMsg.Text = "Unable to create MOC Request.Error :" + ex.Message.ToString();
        }
    }
    protected void lnkUploadFile_OnClick(object sender, EventArgs e)
    {
        try
        {
            string FileName = "";
            byte[] FileContent = new byte[0];

            if (fuRiskAssessmentFile.HasFile)
            {
                FileName = fuRiskAssessmentFile.FileName;
                if (!FileName.ToLower().EndsWith(".pdf"))
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "aa", "alert('Please select pdf file to upload.')", true);
                    return;
                }

                FileContent = fuRiskAssessmentFile.FileBytes;
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "aa", "alert('Please select pdf file to upload.')", true);
                return;
            }
            Common.Set_Procedures("dbo.MOC_MOC_ApprovalOfMOC_FileUpload");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(
                new MyParameter("@MocID", SelMocID),
                new MyParameter("@soc_RaFileName", FileName),
                new MyParameter("@soc_RaFileContent", FileContent)
                );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);

            if (res)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "aa", "alert('File uploaded successfully.')", true);
            }
            else
            {
                //lblMsg.Text = "Unable to create MOC Request.Error :" + Common.getLastError();
            }

        }
        catch (Exception ex)
        {

            //lblMsg.Text = "Unable to create MOC Request.Error :" + ex.Message.ToString();
        }
    }
    
    protected void ddlRiskAssessmentRequired_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        txtRiskAssessmentNumber.Text = "";
        if (ddlRiskAssessmentRequired.SelectedIndex == 0)
        {
            txtRiskAssessmentNumber.Enabled = true;
            fuRiskAssessmentFile.Enabled = true;
        }
        else
        {   
            txtRiskAssessmentNumber.Enabled = false;
            fuRiskAssessmentFile.Enabled = false;
        }

    }
    
    // Pahse 1  ----------------------------
    protected void btnSavePhase1_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtMitigationMeasure.Text.Trim() == "")
            {
                lblMsgPhase1.Text = "Please enter Mitigation measures commenced";
                return;
            }
            if (txtNotificationOfChange.Text.Trim() == "")
            {
                lblMsgPhase1.Text = "Please enter notification of Change completed to all concerned personnel";
                return;
            }
            if (txtAllAppropriateManualUpdated.Text.Trim() == "")
            {
                lblMsgPhase1.Text = "Please enter all appropriate Manuals/Procedures/Drawings updated";
                return;
            }
            if (txtAppropriateTrainingConducted.Text.Trim() == "")
            {
                lblMsgPhase1.Text = "Please enter appropriate training conducted";
                return;
            }
            if (txtDetailsOfChangeCommunicated.Text.Trim() == "")
            {
                lblMsgPhase1.Text = "Please enter details of change communicated to all concerned";
                return;
            }

            Common.Set_Procedures("dbo.MOC_MOC_ImplementationPhaseI");
            Common.Set_ParameterLength(9);
            Common.Set_Parameters(
                new MyParameter("@MocID", SelMocID),

                new MyParameter("@imp1_MitigationCommencedDate", txtMitigationMeasure.Text.Trim()),
                new MyParameter("@imp1_NotificationOfChangesCompleted", txtNotificationOfChange.Text.Trim()),
                new MyParameter("@imp1_AllappropriatemanualsUpdated", txtAllAppropriateManualUpdated.Text.Trim()),
                new MyParameter("@imp1_AppropriateTrainingConducted", txtAppropriateTrainingConducted.Text.Trim()),
                new MyParameter("@imp1_DetailsOfChangesCommunicated", txtDetailsOfChangeCommunicated.Text.Trim()),

                new MyParameter("@ClosedBy", Session["LoginID"]),
                new MyParameter("@ClosureComments", txtComments.Text.Trim()),
                new MyParameter("@WaitingBy", 0)

                );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);

            if (res)
            {

                ScriptManager.RegisterStartupScript(Page, this.GetType(), "aa", "alert('MOC Request updated successfully.')", true);
                BindMocRequest();
                ClearRequestForApprovalStage();
                dv_UpdateStage.Visible = false;
            }
            else
            {
                //lblMsg.Text = "Unable to create MOC Request.Error :" + Common.getLastError();
            }

        }
        catch (Exception ex)
        {

            //lblMsg.Text = "Unable to create MOC Request.Error :" + ex.Message.ToString();
        }

    }
    
    // Pahse 2  ----------------------------
    protected void btnSavePhase2_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtChangeProcessComenced.Text.Trim() == "")
            {
                lblMsgPhase2.Text = "Please enter Change process commenced";
                return;
            }
            if (txtChangeprocesscompleted.Text.Trim() == "")
            {
                lblMsgPhase2.Text = "Please enter Change process completed";
                return;
            }
            if (txtApprovalToTargetDate.Text.Trim() == "")
            {
                lblMsgPhase2.Text = "Please enter Approval to extended target date if applicable";
                return;
            }

            Common.Set_Procedures("dbo.MOC_MOC_ImplementationPhaseII");
            Common.Set_ParameterLength(9);
            Common.Set_Parameters(
                new MyParameter("@MocID", SelMocID),

                new MyParameter("@imp2_ChangesProcessedCommenced", txtChangeProcessComenced.Text.Trim()),
                new MyParameter("@imp2_ChangesProcessedCompleted", txtChangeprocesscompleted.Text.Trim()),
                new MyParameter("@imp2_RequestForExtentionDate", txtRequestForExtedntionToTargetDate.Text.Trim()),
                new MyParameter("@imp2_ApprovalToExtendedDate", txtApprovalToTargetDate.Text.Trim()),
                new MyParameter("@imp2_AdditionalRequirementToTargetDateCompleted", txtAdditionalRequirementToTargetDateCompleted.Text.Trim()),

                new MyParameter("@ClosedBy", Session["LoginID"]),
                new MyParameter("@ClosureComments", txtApprovalCommentsDetailsUpdate.Text.Trim()),
                new MyParameter("@WaitingBy", 0)

                );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);

            if (res)
            {

                ScriptManager.RegisterStartupScript(Page, this.GetType(), "aa", "alert('MOC Request updated successfully.')", true);
                BindMocRequest();
                ClearRequestForApprovalStage();
                dv_UpdateStage.Visible = false;
            }
            else
            {
                //lblMsg.Text = "Unable to create MOC Request.Error :" + Common.getLastError();
            }

        }
        catch (Exception ex)
        {

            //lblMsg.Text = "Unable to create MOC Request.Error :" + ex.Message.ToString();
        }

    }

    // Endorsment ----------------------------
    protected void btnSaveStage7_Endorsement_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCommentsEndorsment.Text.Trim() == "")
            {
                lblMsgEndorsment.Text = "Please enter comments";
                return;
            }
           
            Common.Set_Procedures("dbo.MOC_MOC_Endorsment");
            Common.Set_ParameterLength(4);
            Common.Set_Parameters(
                new MyParameter("@MocID", SelMocID),
                
                new MyParameter("@ClosedBy", Session["LoginID"]),
                new MyParameter("@ClosureComments", txtCommentsEndorsment.Text.Trim()),
                new MyParameter("@WaitingBy", 0)

                );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);

            if (res)
            {

                ScriptManager.RegisterStartupScript(Page, this.GetType(), "aa", "alert('MOC Request updated successfully.')", true);
                BindMocRequest();
                ClearRequestForApprovalStage();
                dv_UpdateStage.Visible = false;
            }
            else
            {
                //lblMsg.Text = "Unable to create MOC Request.Error :" + Common.getLastError();
            }

        }
        catch (Exception ex)
        {

            //lblMsg.Text = "Unable to create MOC Request.Error :" + ex.Message.ToString();
        }
    }
    
    // Review ----------------------------
    protected void btnSaveReviewComments_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCommentsReview.Text.Trim() == "")
            {
                lblMsgReview.Text = "Please enter comments";
                return;
            }

            Common.Set_Procedures("dbo.MOC_MOC_Review");
            Common.Set_ParameterLength(4);
            Common.Set_Parameters(
                new MyParameter("@MocID", SelMocID),

                new MyParameter("@ClosedBy", Session["LoginID"]),
                new MyParameter("@ClosureComments", txtCommentsReview.Text.Trim()),
                new MyParameter("@WaitingBy", 0)

                );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);

            if (res)
            {

                ScriptManager.RegisterStartupScript(Page, this.GetType(), "aa", "alert('MOC Request updated successfully.')", true);
                BindMocRequest();
                ClearRequestForApprovalStage();
                dv_UpdateStage.Visible = false;
            }
            else
            {
                //lblMsg.Text = "Unable to create MOC Request.Error :" + Common.getLastError();
            }

        }
        catch (Exception ex)
        {

            //lblMsg.Text = "Unable to create MOC Request.Error :" + ex.Message.ToString();
        }
    }


    // Closure----------------------------

    protected void btnCancelRequest_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtClosureComments.Text.Trim() == "")
            {
                lblMsgCancelRequest.Text = "Please enter comments";
                return;
            }
            Common.Set_Procedures("dbo.MOC_MOC_CancelChangeRequest");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(
                new MyParameter("@MocID", SelMocID),
                new MyParameter("@ClosureComments", txtClosureComments.Text.Trim()),
                new MyParameter("@ClosedBy", Session["LoginID"])
                );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);

            if (res)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "aa", "alert('MOC Request canceled successfully.')", true);
                BindMocRequest();
                divClosurePopup.Visible = false;
            }
            else
            {
                //lblMsg.Text = "Unable to create MOC Request.Error :" + Common.getLastError();
            }

        }
        catch (Exception ex)
        {

            //lblMsg.Text = "Unable to create MOC Request.Error :" + ex.Message.ToString();
        }
    }
    protected void btnCloseCancelPopup_Click(object sender, EventArgs e)
    {
        divClosurePopup.Visible = false;
    }

    
}