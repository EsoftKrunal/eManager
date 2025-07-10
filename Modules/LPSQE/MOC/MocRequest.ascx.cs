using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class HSSQE_MOC_MocRequest : System.Web.UI.UserControl
{
    public int MocId
    {
        get { return Common.CastAsInt32(ViewState["MocId"]); }
        set { ViewState["MocId"] = value; }
    }    
    public int StatusID
    {
        get { return Common.CastAsInt32(ViewState["_StatusID"]); }
        set { ViewState["_StatusID"] = value; }
    }
    
    public string MOCNumber
    {
        get { return Convert.ToString(ViewState["_MOCNumber"]); }
        set { ViewState["_MOCNumber"] = value; }
    }
    public string Topic
    {
        get { return Convert.ToString(ViewState["_Topic"]); }
        set { ViewState["_Topic"] = value; }
    }
    public bool IsAssessmentRequired
    {
        get { return Convert.ToBoolean(ViewState["_IsAssessmentRequired"]); }
        set { ViewState["_IsAssessmentRequired"] = value; }
    }

    public int StageID
    {
        get { return Common.CastAsInt32(ViewState["_StageID"]); }
        set { ViewState["_StageID"] = value; }
    }
    public string StageName
    {
        get { return Convert.ToString(ViewState["_StageName"]); }
        set { ViewState["_StageName"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        GetMOCDetails();
        GetMOCDetails();
        GetStageName();
    }

    public void BindStages()
    {
        string SQL = "SELECT ROW_NUMBER() OVER(ORDER BY S.stageid) AS SNO,S.*,SS.* " +
                        ",case when D.TableID is null then 'pending' else  ( case when StageClosedOn is null then 'inprogeress' else 'complete' end) end as Stagecss " +
                        " ,P1.FirstName + ' ' + p1.MiddleName + ' ' + p1.FamilyName as ForwaredByName,P2.FirstName + ' ' + P2.MiddleName + ' ' + P2.FamilyName as ClosedByName " +
                        " ,P3.FirstName + ' ' + P3.MiddleName + ' ' + P3.FamilyName as WaitingByName " +
                        "from MOC_Stages S LEFT JOIN " +
                        "(SELECT MOCID, STAGEID, MAX(TABLEID) AS TABLEID FROM MOC_RECORD_STAGES WHERE MOCID = " + MocId.ToString() + " GROUP BY MOCID, STAGEID) D ON S.STAGEID = D.STAGEID " +
                        "LEFT JOIN MOC_RECORD_STAGES SS ON D.TABLEID = SS.TABLEID " +
                        "LEFT JOIN Hr_PersonalDetails P1 ON SS.ForwardedBy = P1.USERID " +
                        "LEFT JOIN Hr_PersonalDetails P2 ON SS.StageClosedBy = P2.USERID " +
                        "LEFT JOIN Hr_PersonalDetails P3 ON SS.WaitingBy = P3.USERID " +
                        " where s.StageId=" + StageID.ToString() +
                        "order by S.stageid ";

        SQL = " select * from  " +
             "  ( " +
             "  SELECT ROW_NUMBER() OVER(ORDER BY S.stageid) AS SNO, S.StageName, SS.* " +
             "  ,case when D.TableID is null then 'pending' else  ( case when StageClosedOn is null then 'inprogeress' else 'complete' end) end as Stagecss " +
             "      ,P1.FirstName + ' ' + p1.MiddleName + ' ' + p1.FamilyName as ForwaredByName,P2.FirstName + ' ' + P2.MiddleName + ' ' + P2.FamilyName as ClosedByName " +
             "      ,P3.FirstName + ' ' + P3.MiddleName + ' ' + P3.FamilyName as WaitingByName " +
             "  from MOC_Stages S LEFT JOIN " +
             "  (SELECT MOCID, STAGEID, MAX(TABLEID) AS TABLEID FROM MOC_RECORD_STAGES WHERE MOCID = " + MocId.ToString() + " GROUP BY MOCID, STAGEID) D ON S.STAGEID = D.STAGEID " +
             "  LEFT JOIN MOC_RECORD_STAGES SS ON D.TABLEID = SS.TABLEID " +
             "  LEFT JOIN Hr_PersonalDetails P1 ON SS.ForwardedBy = P1.USERID " +
             "  LEFT JOIN Hr_PersonalDetails P2 ON SS.StageClosedBy = P2.USERID " +
             "  LEFT JOIN Hr_PersonalDetails P3 ON SS.WaitingBy = P3.USERID " +
             "  )T " +
             "  where T.StageId =" + StageID.ToString() +
             "  order by T.stageid  ";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            string StageCSS = dr["Stagecss"].ToString();
            divStage.Attributes.Add("class", " "+ StageCSS); //statusbox
            lblStageName.Text = dr["SNO"] + ". " + dr["StageName"].ToString();
            lblStageClosedComments.Text =  dr["StageClosedComments"].ToString();

            lblStageClosedByPosition.Text = dr["StageClosedByPosition"].ToString();
            lblStageClosedOn.Text = dr["StageClosedOn"].ToString();
            lblWaitingByName.Text = dr["WaitingByName"].ToString();
            lblForwardedOn.Text = dr["ForwardedOn"].ToString();


            if (StatusID == 15)
                divAction.Visible = false;
            else
                divAction.Visible = true;
            if (StageCSS.ToLower() == "inprogeress")
            {
                lnkUPdate.Visible = true;
                btnSendBack.Visible = true;
            }
            else
            {
                lnkUPdate.Visible = false;
                btnSendBack.Visible = false;
            }


        }
    }
    public void GetMOCDetails()
    {
        string SQL = " SElect StatusID,MocNumber,Topic,* from snq.dbo.MOC_RECORD  where MOCID=" + MocId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            StatusID = Common.CastAsInt32(dr["StatusID"]);
            MOCNumber = dr["MOCNumber"].ToString();
            Topic = dr["Topic"].ToString();

            IsAssessmentRequired = (dr["atc_RiskAssessment"].ToString() == "True");
            
        }
    }
    public void GetStageName()
    {
        string SQL = " SElect * from snq.dbo.MOC_STAGES where StageID=" + StageID;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            StageName = dr["StageName"].ToString();
        }
    }

    //--------------------------------------------------------------------------------------------------------
    protected void lnkUPdate_OnClick(object sender, EventArgs e)
    {
        Bind_ResponsiblePerson();
        //Button btn = (Button)sender;
        //HiddenField hfStageID = (HiddenField)btn.Parent.FindControl("hfdStageID");
        //HiddenField hfdStagename = (HiddenField)btn.Parent.FindControl("hfdStagename");


        int StageId = StageID;
        lblStagePopupHeading.Text = StageName + ((MOCNumber!= "") ? " " + MOCNumber + " " : "");
        lblTopicPopup.Text = Topic;

        dv_UpdateStage.Visible = true;

        divStage2.Visible = false; divStage3.Visible = false; divStage4.Visible = false;
        divStage5_Pahse1.Visible = false; divStage6_Pahse2.Visible = false; divStage7_Endorsement.Visible = false; divStage8_Review.Visible = false;
        divSendBack.Visible = false;
        if (StageId == 10)
        {
            divStage2.Visible = true;
        }
        else if (StageId == 15)
        {
            divStage3.Visible = true;
            if (IsAssessmentRequired)
            {
                trRiskAssessmentNo.Visible = true;

                //txtRiskAssessmentNumber.Enabled = true;
                //fuRiskAssessmentFile.Enabled = true;
            }
            else
            {
                trRiskAssessmentNo.Visible = false;
                //txtRiskAssessmentNumber.Enabled = false;
                //fuRiskAssessmentFile.Enabled = false;
            }
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
    protected void btnSendBack_OnClick(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        HiddenField hfStageID = (HiddenField)btn.Parent.FindControl("hfdStageID");
        HiddenField hfdStagename = (HiddenField)btn.Parent.FindControl("hfdStagename");

        int StageId = StageID;
        lblStagePopupHeading.Text = "Send Back";
        lblTopicPopup.Text = Topic;

        dv_UpdateStage.Visible = true;

        divStage2.Visible = false; divStage3.Visible = false; divStage4.Visible = false;
        divStage5_Pahse1.Visible = false; divStage6_Pahse2.Visible = false; divStage7_Endorsement.Visible = false; divStage8_Review.Visible = false;

        divSendBack.Visible = true;



    }
    protected void btnClosePopup_Click(object sender, EventArgs e)
    {
        dv_UpdateStage.Visible = false;
    }

    //Stage 2----------------------------
    protected void btnSaveStageRequestApproval_Click(object sender, EventArgs e)
    {
        if (ddlDetailsChangeDefined.SelectedIndex == 0)
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
        if (txtDateTobeCompleted.Text.Trim() == "")
        {
            lblMsgMOCRequestApproval.Text = "Please enter summary of Changes to be completed by.";
            return;
        }
        else
        {
            try
            {
                if (Convert.ToDateTime(txtDateTobeCompleted.Text.Trim()) < DateTime.Today)
                {
                    lblMsgMOCRequestApproval.Text = "Please enter date more than or equal to current date.";
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMsgMOCRequestApproval.Text = "Please enter valid date.";
                return;
            }
        }
        if (ddlResponsiblePerson.SelectedIndex == 0)
        {
            lblMsgMOCRequestApproval.Text = "Please select responsible person.";
            return;
        }

        try
        {
            Common.Set_Procedures("SNQ.dbo.MOC_Moc_RequestApproval");
            Common.Set_ParameterLength(10);
            Common.Set_Parameters(
                new MyParameter("@MocID", MocId),
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
                //BindStages();
                //ShowMocRecord();
                Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
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
        string SQL = " select UserID,FirstName+' '+MiddleName+' '+FamilyName as UserName from ShipSoft.dbo.Hr_PersonalDetails order by UserName ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        ddlResponsiblePerson.DataSource = dt;
        ddlResponsiblePerson.DataTextField = "UserName";
        ddlResponsiblePerson.DataValueField = "UserID";
        ddlResponsiblePerson.DataBind();
        ddlResponsiblePerson.Items.Insert(0, new ListItem("Select", ""));

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
            //if (txtDateOfRiskAssessment.Text.Trim() == "")
            //{
            //    lblMsgStageApprovalOfMOC.Text = "Please enter date of risk assessment";
            //    return;
            //}
            //if (txtResultOfRiskAssessment.Text.Trim() == "")
            //{
            //    lblMsgStageApprovalOfMOC.Text = "Please enter Result of risk assessment";
            //    return;
            //}
            if (ddlImpaceOfChangeOnEnvironment.SelectedIndex == 0)
            {
                if (txtImpactOfChangeOnEnvironment.Text.Trim() == "")
                {
                    lblMsgStageApprovalOfMOC.Text = "Please enter Impact of change on environment.";
                    return;
                }
            }

            if (ddlImpaceOfChangeOnSafety.SelectedIndex == 0)
            {
                if (txtImpactOfChangeOnSafety.Text.Trim() == "")
                {
                    lblMsgStageApprovalOfMOC.Text = "Please enter impact of change on safety.";
                    return;
                }
            }
            //if (txtManualNeedAnUpdated.Text.Trim() == "")
            //{
            //    lblMsgStageApprovalOfMOC.Text = "Please enter what Manuals/Procedures/Drawings will need an update.";
            //    return;
            //}
            //if (txtKeyMitigatingAction.Text.Trim() == "")
            //{
            //    lblMsgStageApprovalOfMOC.Text = "Please enter list of key mitigating actions.";
            //    return;
            //}
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
                lblMsgStageApprovalOfMOC.Text = "Please enter remarks.";
                return;
            }
            if (txtTargetDateForCompletion.Text.Trim() == "")
            {
                lblMsgStageApprovalOfMOC.Text = "Please enter terget date for completion.";
                return;
            }
            else
            {
                try
                {
                    if (Convert.ToDateTime(txtTargetDateForCompletion.Text.Trim()) < DateTime.Today)
                    {
                        lblMsgStageApprovalOfMOC.Text = "Please enter date more than or equal to current date.";
                        return;
                    }
                }
                catch (Exception ex)
                {
                    lblMsgStageApprovalOfMOC.Text = "Please enter valid date.";
                    return;
                }
            }

            Common.Set_Procedures("SNQ.dbo.MOC_Moc_DetailsUpdate");
            Common.Set_ParameterLength(12);
            Common.Set_Parameters(
                new MyParameter("@MocID", MocId),
                new MyParameter("@ChangeType", ddlChangeType.SelectedValue),
                //new MyParameter("@soc_RiskAssessmentDate", txtDateOfRiskAssessment.Text.Trim()),
                //new MyParameter("@soc_ResultOfRiskAssessment", txtResultOfRiskAssessment.Text.Trim()),
                new MyParameter("@soc_ImpactChangeSafety", ddlImpaceOfChangeOnSafety.SelectedValue),
                new MyParameter("@soc_ImpactChangeSafetyDetails", txtImpactOfChangeOnSafety.Text.Trim()),
                new MyParameter("@soc_ImpactChangesEnvironment", ddlImpaceOfChangeOnEnvironment.SelectedValue),
                new MyParameter("@soc_ImpactChangesEnvironmentDetails", txtImpactOfChangeOnEnvironment.Text.Trim()),
                //new MyParameter("@soc_KeyMitigatingAction", txtKeyMitigatingAction.Text.Trim()),
                //new MyParameter("@soc_ManualsForUpdate", txtManualNeedAnUpdated.Text.Trim()),
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
                Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
                //BindStages();
                //ShowMocRecord();
                //ClearRequestForApprovalStage();
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
        txtImpactOfChangeOnEnvironment.Visible = ddlImpaceOfChangeOnEnvironment.SelectedValue == "1";
    }

    // Risk assessment Stage 4 ----------------------------
    protected void btnSaveRiskAssessment_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsAssessmentRequired)
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
            string SQL = " SElect impact from snq.dbo.MOC_RECORD where mocID=" + MocId;
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
            if (dt.Rows.Count > 0)
            {
                string[] impact = dt.Rows[0][0].ToString().Split(',');
                int cnt = (impact.Where(a => a == "1")).Count(); // People
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
            Common.Set_Procedures("SNQ.dbo.MOC_Moc_ApprovalOfMOC");
            Common.Set_ParameterLength(11);
            Common.Set_Parameters(
                new MyParameter("@MocID", MocId),

                //new MyParameter("@soc_RaRequired", ddlRiskAssessmentRequired.SelectedValue),
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
                //BindStages();
                //ShowMocRecord();
                ClearRequestForApprovalStage();
                dv_UpdateStage.Visible = false;
                Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
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
            Common.Set_Procedures("SNQ.dbo.MOC_MOC_ApprovalOfMOC_FileUpload");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(
                new MyParameter("@MocID", MocId),
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
            else
            {
                try
                {
                    if (Convert.ToDateTime(txtMitigationMeasure.Text.Trim()) < DateTime.Today)
                    {
                        lblMsgPhase1.Text = "Please enter date more than or equal to current date.";
                        return;
                    }
                }
                catch (Exception ex)
                {
                    lblMsgPhase1.Text = "Please enter valid date.";
                    return;
                }
            }

            if (txtNotificationOfChange.Text.Trim() == "")
            {
                lblMsgPhase1.Text = "Please enter notification of Change completed to all concerned personnel";
                return;
            }
            else
            {
                try
                {
                    if (Convert.ToDateTime(txtNotificationOfChange.Text.Trim()) < DateTime.Today)
                    {
                        lblMsgPhase1.Text = "Please enter date more than or equal to current date.";
                        return;
                    }
                }
                catch (Exception ex)
                {
                    lblMsgPhase1.Text = "Please enter valid date.";
                    return;
                }
            }
            if (txtAllAppropriateManualUpdated.Text.Trim() == "")
            {
                lblMsgPhase1.Text = "Please enter all appropriate Manuals/Procedures/Drawings updated";
                return;
            }
            else
            {
                try
                {
                    if (Convert.ToDateTime(txtAllAppropriateManualUpdated.Text.Trim()) < DateTime.Today)
                    {
                        lblMsgPhase1.Text = "Please enter date more than or equal to current date.";
                        return;
                    }
                }
                catch (Exception ex)
                {
                    lblMsgPhase1.Text = "Please enter valid date.";
                    return;
                }
            }

            if (txtAppropriateTrainingConducted.Text.Trim() == "")
            {
                lblMsgPhase1.Text = "Please enter appropriate training conducted";
                return;
            }
            else
            {
                try
                {
                    if (Convert.ToDateTime(txtAppropriateTrainingConducted.Text.Trim()) < DateTime.Today)
                    {
                        lblMsgPhase1.Text = "Please enter date more than or equal to current date.";
                        return;
                    }
                }
                catch (Exception ex)
                {
                    lblMsgPhase1.Text = "Please enter valid date.";
                    return;
                }
            }

            if (txtDetailsOfChangeCommunicated.Text.Trim() == "")
            {
                lblMsgPhase1.Text = "Please enter details of change communicated to all concerned";
                return;
            }

            Common.Set_Procedures("SNQ.dbo.MOC_MOC_ImplementationPhaseI");
            Common.Set_ParameterLength(9);
            Common.Set_Parameters(
                new MyParameter("@MocID", MocId),

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
                BindStages();
                //ShowMocRecord();
                ClearRequestForApprovalStage();
                dv_UpdateStage.Visible = false;
                Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
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
            else
            {
                try
                {
                    if (Convert.ToDateTime(txtChangeProcessComenced.Text.Trim()) < DateTime.Today)
                    {
                        lblMsgPhase2.Text = "Please enter date more than or equal to current date.";
                        return;
                    }
                }
                catch (Exception ex)
                {
                    lblMsgPhase2.Text = "Please enter valid date.";
                    return;
                }
            }

            if (txtChangeprocesscompleted.Text.Trim() == "")
            {
                lblMsgPhase2.Text = "Please enter Change process completed";
                return;
            }
            else
            {
                try
                {
                    if (Convert.ToDateTime(txtChangeprocesscompleted.Text.Trim()) < DateTime.Today)
                    {
                        lblMsgPhase2.Text = "Please enter date more than or equal to current date.";
                        return;
                    }
                }
                catch (Exception ex)
                {
                    lblMsgPhase2.Text = "Please enter valid date.";
                    return;
                }
            }

            //txtRequestForExtedntionToTargetDate
            try
            {
                if (Convert.ToDateTime(txtRequestForExtedntionToTargetDate.Text.Trim()) < DateTime.Today)
                {
                    lblMsgPhase2.Text = "Please enter date more than or equal to current date.";
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMsgPhase2.Text = "Please enter valid date.";
                return;
            }

            if (txtApprovalToTargetDate.Text.Trim() == "")
            {
                lblMsgPhase2.Text = "Please enter Approval to extended target date if applicable";
                return;
            }
            else
            {
                try
                {
                    if (Convert.ToDateTime(txtApprovalToTargetDate.Text.Trim()) < DateTime.Today)
                    {
                        lblMsgPhase2.Text = "Please enter date more than or equal to current date.";
                        return;
                    }
                }
                catch (Exception ex)
                {
                    lblMsgPhase2.Text = "Please enter valid date.";
                    return;
                }
            }


            Common.Set_Procedures("SNQ.dbo.MOC_MOC_ImplementationPhaseII");
            Common.Set_ParameterLength(9);
            Common.Set_Parameters(
                new MyParameter("@MocID", MocId),

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
                BindStages();
                //ShowMocRecord();
                ClearRequestForApprovalStage();
                dv_UpdateStage.Visible = false;
                Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
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
                lblMsgEndorsment.Text = "Please enter remarks";
                return;
            }

            Common.Set_Procedures("SNQ.dbo.MOC_MOC_Endorsment");
            Common.Set_ParameterLength(4);
            Common.Set_Parameters(
                new MyParameter("@MocID", MocId),

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
                BindStages();
                //ShowMocRecord();
                ClearRequestForApprovalStage();
                dv_UpdateStage.Visible = false;
                Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
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
    protected void btnSaveStage8_Endorsement_Click(object sender, EventArgs e)
    {
    }
    // Review ----------------------------
    protected void BtnSaveSendBack_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCommentsSendBack.Text.Trim() == "")
            {
                lblMsgSedBack.Text = "Please enter remarks";
                return;
            }

            Common.Set_Procedures("SNQ.dbo.MOC_MOC_SenBack");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(
                new MyParameter("@MocID", MocId),
                new MyParameter("@SendBy", Session["LoginID"]),
                new MyParameter("@Comments", txtCommentsSendBack.Text.Trim())
                );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);

            if (res)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "aa", "alert('MOC Request send back successfully.')", true);
                BindStages();
                //ShowMocRecord();
                ClearRequestForApprovalStage();
                dv_UpdateStage.Visible = false;
                Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
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
    protected void lnlOpenCancelMOC_OnClick(object sender, EventArgs e)
    {
        divClosurePopup.Visible = true;
    }
    protected void btnCancelRequest_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtClosureComments.Text.Trim() == "")
            {
                lblMsgCancelRequest.Text = "Please enter remarks";
                return;
            }
            Common.Set_Procedures("SNQ.dbo.MOC_MOC_CancelChangeRequest");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(
                new MyParameter("@MocID", MocId),
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
                Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
                divClosurePopup.Visible = false;
                Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
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


    protected void btnSaveReviewComments_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCommentsReview.Text.Trim() == "")
            {
                lblMsgReview.Text = "Please enter remarks";
                return;
            }

            Common.Set_Procedures("SNQ.dbo.MOC_MOC_Review");
            Common.Set_ParameterLength(4);
            Common.Set_Parameters(
                new MyParameter("@MocID", MocId),

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
                BindStages();
                //ShowMocRecord();
                ClearRequestForApprovalStage();
                dv_UpdateStage.Visible = false;
                Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
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
    protected void btnDownloadAttachment_OnClick(object sender, EventArgs e)
    {
        string FileName = "";
        byte[] FileContent = new byte[] { };
        string sql = " Select soc_RaFileName,soc_RaFileContent from SNQ.DBO.MOC_RECORD Where MocID=" + MocId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt.Rows.Count > 0)
        {
            FileName = dt.Rows[0]["soc_RaFileName"].ToString();
            FileContent = (byte[])dt.Rows[0]["soc_RaFileContent"];
        }

        //HttpContext.Current.Response.Clear();
        //HttpContext.Current.Response.AddHeader("Content-Disposition","attachment; filename=" + FileName);
        //HttpContext.Current.Response.AddHeader("Content-Length",Convert.ToString(FileContent.Length));
        //HttpContext.Current.Response.ContentType = "application/octet-stream";
        //HttpContext.Current.Response.Write(FileContent);
        //HttpContext.Current.Response.End();
        Response.Clear();
        Response.AddHeader("Cache-Control", "no-cache, must-revalidate, post-check=0, pre-check=0");
        Response.AddHeader("Pragma", "no-cache");
        Response.AddHeader("Content-Description", "File Download");
        Response.AddHeader("Content-Type", "application/force-download");
        Response.AddHeader("Content-Transfer-Encoding", "binary\n");
        Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
        Response.BinaryWrite(FileContent);
        Response.End();
    }


}