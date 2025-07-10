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

public partial class HSSQE_MOC_MocRecord : System.Web.UI.Page
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
    public int CurrStageID
    {
        get { return Common.CastAsInt32(ViewState["_CurrStageID"]); }
        set { ViewState["_CurrStageID"] = value; }
    }
    public bool IsAssessmentRequired
    {
        get { return Convert.ToBoolean(ViewState["_IsAssessmentRequired"]); }
        set { ViewState["_IsAssessmentRequired"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        lblMsgSedBack.Text = "";
        lblMsgPopup.Text = "";
        //------------------------------------
        if (!IsPostBack)
        {
            MocId = Common.CastAsInt32(Request.QueryString["MocId"]);
            
            ShowMocRecord();
            BindStages();
            Bind_ResponsiblePerson();


        }
    }
    
    public void ShowRequiredSection(int StageID)
    {
        if (StageID >= 10)
        {
            div1MocRequest.Visible = true;            
        }
        if (StageID >= 15)
        {
            div2_MOCRequestApproval.Visible = true;
            
        }
        if (StageID >= 20)
        {
            div3_MOCDetailUpdate.Visible = true;
            
        }
        if (StageID >= 25)
        {
            div4_ApprovalOfMoc.Visible = true;
            
        }
        if (StageID >= 30)
        {
            div5_ImplementationPahse1.Visible = true;
            
        }
        if (StageID >= 35)
        {
            div6_ImplementationPahse2.Visible = true;
            
        }
        if (StageID >= 40)
        {
            div7_EndorsementByHOD.Visible = true;
            
        }
        if (StageID >= 45)
        {
            div8_ReviewByDPA.Visible = true;
            
        }

    }
    public void BindStages()
    {
        string SQL = "SELECT ROW_NUMBER() OVER(ORDER BY S.stageid) AS SNO,S.*,SS.* "+
                        ",case when D.TableID is null then 'pending' else  ( case when StageClosedOn is null then 'inprogeress' else 'complete' end) end as Stagecss " +
                        " ,P1.FirstName + ' ' + p1.LastName as ForwaredByName,P2.FirstName + ' '  + P2.LastName as ClosedByName " +
                        " ,P3.FirstName + ' ' + P3.LastName as WaitingByName " +
                        "from DBO.MOC_Stages S LEFT JOIN " +
                        "(SELECT MOCID, STAGEID, MAX(TABLEID) AS TABLEID FROM DBO.MOC_RECORD_STAGES WHERE MOCID = " + MocId.ToString() + " and Isnull(StageAnswer,'Y')='Y' GROUP BY MOCID, STAGEID) D ON S.STAGEID = D.STAGEID " +
                        "LEFT JOIN DBO.MOC_RECORD_STAGES SS ON D.TABLEID = SS.TABLEID " +
                        "LEFT JOIN DBO.UserMaster P1 ON SS.ForwardedBy = P1.LoginId " +
                        "LEFT JOIN DBO.UserMaster P2 ON SS.StageClosedBy = P2.LoginId " +
                        "LEFT JOIN DBO.UserMaster P3 ON SS.WaitingBy = P3.LoginId " +
                        "order by S.stageid ";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        rptMOCStages.DataSource = dt;
        rptMOCStages.DataBind();        
    }
    public void ShowMocRecord()
    {
        //< asp:ListItem Text = "People" Value = "1" ></ asp:ListItem >       
        //< asp:ListItem Text = "Process" Value = "2" ></ asp:ListItem >              
        //< asp:ListItem Text = "Equipment" Value = "3" ></ asp:ListItem>                     
        //< asp:ListItem Text = "Safety" Value = "4" ></ asp:ListItem >                            
        //< asp:ListItem Text = "Environment" Value = "5" ></ asp:ListItem >
        string[] ImpactList = { "People", "Process", "Equipment", "Safety", "Environment" };

                                           string SQL = " select * from dbo.vw_MOC_RECORD where MocID="+ MocId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt.Rows.Count > 0)
        {
            DataRow Dr = dt.Rows[0];

            hfdTopic.Value = Dr["Topic"].ToString();
            StatusID = Common.CastAsInt32(Dr["StatusID"].ToString());

            IsAssessmentRequired = (Dr["atc_RiskAssessment"].ToString() == "True");

            CurrStageID = Common.CastAsInt32(Dr["StageID"]);
            if (IsAssessmentRequired)
            {
                //lblRaRecord.Text = "Yes";
                tblRaRecords.Visible = true;
                tblRaRecords1.Visible = true;
                lblRaNumber.Text = Dr["RANumber"].ToString();
                lblRaFileName.Text = Dr["RAFileName"].ToString();
            }
            else
            {
                //lblRaRecord.Text = "No";
                tblRaRecords.Visible = false;
                tblRaRecords1.Visible = false;
            }


            if (Dr["MocNumber"].ToString()!="")
                lblMocNumber.Text = " [ "+Dr["MocNumber"].ToString()+" ] ";

            string SelImpact = "";
            foreach (string im in Dr["impact"].ToString().Split(','))
            {
                if (im != "")
                {
                    SelImpact = SelImpact + ", "+ImpactList[Common.CastAsInt32(im) - 1];
                }
            }
            if (SelImpact != "")
                SelImpact = SelImpact.Substring(1);

            ShowRequiredSection(Common.CastAsInt32(Dr["StageID"]));
            lblSource.Text = Dr["Source"].ToString();
            lblVesselOffice.Text = Dr["Location"].ToString();
            lblTopic.Text = Dr["Topic"].ToString();

            lblImpact.Text = SelImpact;

            

            lblReasionForChange.Text = Dr["ReasonForChange"].ToString();
            lblBriefDescription.Text = Dr["DescriptionForChange"].ToString();
            lblTimeLine.Text =Common.ToDateString( Dr["TargetDate"].ToString());
            

            //---------------------------------------------------------------------------------
            lblCommunication.Text = Dr["Communication"].ToString();
            lblTraining.Text = Dr["Training"].ToString();
            lblSMSReview.Text = Dr["SMSReview"].ToString();
            lblDrawing.Text = Dr["DrawingsManuals"].ToString();
            lblDocumentation.Text = Dr["Documentation"].ToString();
            //---------------------------------------------------------------------------------
            lblDetailsChangeDefined.Text = Dr["atc_DetailsOfChangeT"].ToString();
            lblReasonOfChangeDefined.Text = Dr["atc_ReasionForChangeT"].ToString();
            lblIsChangeNecessary.Text = Dr["atc_ChangeNececssaryT"].ToString();
            lblIsRecomendationGiven.Text = Dr["atc_RiskAssessmentT"].ToString();
            lblDateTobeCompleted.Text = Common.ToDateString(Dr["atc_ChangeCompletedTargetDate"]);
            
            lblResponsiblePersonaName.Text = Dr["atc_ChangeAssignedToT"].ToString();
            lblResponsiblePosition.Text = Dr["atc_ChangeAssignedToPosition"].ToString();
            
            lblApprovalDate.Text = Common.ToDateString(Dr["atc_ChangeAssignedOn"]);

            //Risk assessment ---------------------------------------------------------------------------------
            lblRaNumber.Text = Dr["soc_RaNumber"].ToString();
            lblRaFileName.Text = Dr["soc_RaFileName"].ToString();
            lblCommunication.Text = Dr["soc_Communication"].ToString();
            lblTraining.Text = Dr["soc_Training"].ToString();
            lblSMSReview.Text = Dr["soc_SMSReview"].ToString();
            lblDrawing.Text = Dr["soc_DrawingManuals"].ToString();
            lblDocumentation.Text = Dr["soc_Documentation"].ToString();
            lblEquipment.Text = Dr["soc_Equipments"].ToString();

            //Summary of Change ---------------------------------------------------------------------------------

            lblChangeType.Text = Dr["changeTypeT"].ToString();
            //lblDateOfRiskAssessment.Text = Common.ToDateString(Dr["soc_RiskAssessmentDate"]);
            //lblResultOfRiskAssessment.Text = Dr["soc_ResultOfRiskAssessment"].ToString();
            lblImpactOfChangeOnSafety.Text = Dr["soc_ImpactChangeSafetyDetailsT"].ToString();
            lblImpactOfChangeOnEnvironment.Text = Dr["soc_ImpactChangesEnvironmentDetailsT"].ToString();
            //lblKeyMitigatingAction.Text = Dr["soc_KeyMitigatingAction"].ToString();
            //lblManualNeedAnUpdated.Text = Dr["soc_ManualsForUpdate"].ToString();
            lblIdentifyPersonEffectedByChange.Text = Dr["soc_PersonOrVesselAffectedByChange"].ToString();
            lblEstimatedCostOfChange.Text = Dr["soc_EstimatedCostOfChange"].ToString();
            lblTargetDateForCompletion.Text = Common.ToDateString(Dr["soc_TargetDateForCompletion"]);

            //Phase 1 ---------------------------------------------------------------------------------
            lblMitigationMeasure.Text = Common.ToDateString(Dr["imp1_MitigationCommencedDate"].ToString());
            lblNotificationOfChange.Text = Common.ToDateString(Dr["imp1_NotificationOfChangesCompleted"].ToString());
            lblAllAppropriateManualUpdated.Text = Common.ToDateString(Dr["imp1_AllappropriatemanualsUpdated"].ToString());
            lblAppropriateTrainingConducted.Text = Common.ToDateString(Dr["imp1_AppropriateTrainingConducted"].ToString());
            lblDetailsOfChangeCommunicated.Text = Common.ToDateString(Dr["imp1_DetailsOfChangesCommunicated"].ToString());

            //Phase 2 ---------------------------------------------------------------------------------
            lblChangeProcessComenced.Text = Common.ToDateString(Dr["imp2_ChangesProcessedCommenced"].ToString());
            lblChangeprocesscompleted.Text = Common.ToDateString(Dr["imp2_ChangesProcessedCompleted"].ToString());
            if(Dr["imp2_RequestForExtentionDate"].ToString().ToLower()=="False")
                lblRequestForExtedntionToTargetDate.Text = "No";
            else
                lblRequestForExtedntionToTargetDate.Text = Common.ToDateString(Dr["imp2_RequestForExtentionDate"].ToString());

            if (Dr["imp2_ApprovalToExtendedTargetDate"].ToString().ToLower() == "False")
                lblApprovalToTargetDate.Text = "No";
            else
                lblApprovalToTargetDate.Text = Common.ToDateString(Dr["imp2_ApprovalToExtendedDate"].ToString());

            lblAdditionalRequirementToTargetDateCompleted.Text = Dr["imp2_AdditionalRequirementToTargetDateCompleted"].ToString();

        }

    }
    public string GetApprovalDate()
    {
        string ret = "";
        string sql = " SElect convert(varchar,MAX(StageClosedOn) ,106) from dbo.MOC_RECORD_STAGES where mociD=" + MocId+" and StageID=10 ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt.Rows.Count > 0)
        {
            ret =dt.Rows[0][0].ToString();
        }
        return ret;
    }

    //--------------------------------------------------------------------------------------------------------
    protected void lnkUPdate_OnClick(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        HiddenField hfStageID = (HiddenField)btn.Parent.FindControl("hfdStageID");
        HiddenField hfdStagename = (HiddenField)btn.Parent.FindControl("hfdStagename");
        

        int StageId = Common.CastAsInt32(hfStageID.Value);
        lblStagePopupHeading.Text = hfdStagename.Value + ((lblMocNumber.Text.Trim() != "")?" "+ lblMocNumber.Text + " ":"");
        lblTopicPopup.Text = hfdTopic.Value;
        
        dv_UpdateStage.Visible = true;

        divStage2.Visible = false;divStage3.Visible = false;divStage4.Visible = false;
        divStage5_Pahse1.Visible = false;divStage6_Pahse2.Visible = false;divStage7_Endorsement.Visible = false;divStage8_Review.Visible = false;
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
        else if (StageId == 45)
        {
            divSendBack.Visible = true;
        }



    }
    protected void btnSendBack_OnClick(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        HiddenField hfStageID = (HiddenField)btn.Parent.FindControl("hfdStageID");
        HiddenField hfdStagename = (HiddenField)btn.Parent.FindControl("hfdStagename");

        int StageId = Common.CastAsInt32(hfStageID.Value);
        lblStagePopupHeading.Text = "Send Back";
        lblTopicPopup.Text = hfdTopic.Value;

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
        //else
        //{
        //    try
        //    {
        //        if (Convert.ToDateTime(txtDateTobeCompleted.Text.Trim()) < DateTime.Today)
        //        {
        //            lblMsgMOCRequestApproval.Text = "Please enter date more than or equal to current date.";
        //            return;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMsgMOCRequestApproval.Text = "Please enter valid date.";
        //        return;
        //    }
        //}
        if (ddlResponsiblePerson.SelectedIndex == 0)
        {
            lblMsgMOCRequestApproval.Text = "Please select responsible person.";
            return;
        }

        try
        {
            Common.Set_Procedures("dbo.MOC_Moc_RequestApproval");
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
                BindStages();
                ShowMocRecord();
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
        string SQL = " select LoginId,FirstName+' '+LastName as UserName from dbo.UserMaster order by UserID ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        ddlResponsiblePerson.DataSource = dt;
        ddlResponsiblePerson.DataTextField = "UserName";
        ddlResponsiblePerson.DataValueField = "LoginId";
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
                lblMsgStageApprovalOfMOC.Text = "Please select impact of change on environment.";
                return;
            }
             else if (ddlImpaceOfChangeOnEnvironment.SelectedIndex == 1)
            {
                if (txtImpactOfChangeOnEnvironment.Text.Trim() == "")
                {
                    lblMsgStageApprovalOfMOC.Text = "Please enter Impact of change on environment.";
                    return;
                }
            }

            if (ddlImpaceOfChangeOnSafety.SelectedIndex == 0)
            {
                lblMsgStageApprovalOfMOC.Text = "Please select impact of change on safety.";
                return;
            }
            else if (ddlImpaceOfChangeOnSafety.SelectedIndex == 1)
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
                lblMsgStageApprovalOfMOC.Text = "Please enter target date for completion.";
                return;
            }
            //else
            //{
            //    try
            //    {
            //        if (Convert.ToDateTime(txtTargetDateForCompletion.Text.Trim()) < DateTime.Today)
            //        {
            //            lblMsgStageApprovalOfMOC.Text = "Please enter date more than or equal to current date.";
            //            return;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        lblMsgStageApprovalOfMOC.Text = "Please enter valid date.";
            //        return;
            //    }
            //}

            Common.Set_Procedures("dbo.MOC_Moc_DetailsUpdate");
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
            string SQL = " SElect impact from dbo.MOC_RECORD where mocID=" + MocId;
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
            Common.Set_Procedures("dbo.MOC_Moc_ApprovalOfMOC");
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
                BindStages();
                ShowMocRecord();
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
                lblMsgPhase1.Text = "Please enter \"Mitigation measures commenced\" date.";
                return;
            }
            //else
            //{
            //    try
            //    {
            //        if (Convert.ToDateTime(txtMitigationMeasure.Text.Trim()) > DateTime.Today )
            //        {
            //            lblMsgPhase1.Text = "\"Mitigation measures commenced\" date should be less than or equal to current date.";
            //            return;
            //        }
            //        if ( Convert.ToDateTime(txtMitigationMeasure.Text.Trim()) < Convert.ToDateTime(GetApprovalDate()))
            //        {
            //            lblMsgPhase1.Text = "\"Mitigation measures commenced\" date should be more than or equal to approval date.";
            //            return;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        lblMsgPhase1.Text = "\"Mitigation measures commenced\" date is invalid.";
            //        return;
            //    }
            //}
            //----------------------------------------------------------------------------------------------
            if (txtNotificationOfChange.Text.Trim() == "")
            {
                lblMsgPhase1.Text = "Please enter  \"Notification of Change completed to all concerned personnel\" date.";
                return;
            }
            //else
            //{
            //    try
            //    {
            //        if (Convert.ToDateTime(txtNotificationOfChange.Text.Trim()) > DateTime.Today)
            //        {
            //            lblMsgPhase1.Text = " \"Notification of Change completed to all concerned personnel\" date should be less than or equal to current date.";
            //            return;
            //        }
            //        if (Convert.ToDateTime(txtNotificationOfChange.Text.Trim()) < Convert.ToDateTime(GetApprovalDate()))
            //        {
            //            lblMsgPhase1.Text = " \"Notification of Change completed to all concerned personnel\" date should be more than or equal to commenced date.";
            //            return;
            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //        lblMsgPhase1.Text = "Please enter valid date.";
            //        lblMsgPhase1.Text = " \"Notification of Change completed to all concerned personnel\" date is invalid.";
            //        return;
            //    }
            //}
            //----------------------------------------------------------------------------------------------
            if (txtAllAppropriateManualUpdated.Text.Trim() == "")
            {
                lblMsgPhase1.Text = "Please enter \"All appropriate Manuals / Procedures / Drawings updated\" date.";
                return;
            }
            //else
            //{
            //    try
            //    {
            //        if (Convert.ToDateTime(txtAllAppropriateManualUpdated.Text.Trim()) > DateTime.Today)
            //        {
            //            lblMsgPhase1.Text = " \"All appropriate Manuals / Procedures / Drawings updated\" date should be less than or equal to current date.";
            //            return;
            //        }
            //        if (Convert.ToDateTime(txtAllAppropriateManualUpdated.Text.Trim()) < Convert.ToDateTime(GetApprovalDate()))
            //        {
            //            lblMsgPhase1.Text = " \"All appropriate Manuals / Procedures / Drawings updated\" date should be more than or equal to commenced date.";
            //            return;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        lblMsgPhase1.Text = "Please enter valid date.";
            //        lblMsgPhase1.Text = " \"All appropriate Manuals / Procedures / Drawings updated\" date is invalid.";
            //        return;
            //    }
            //}
            //----------------------------------------------------------------------------------------------
            if (txtAppropriateTrainingConducted.Text.Trim() == "")
            {
                lblMsgPhase1.Text = "Please enter \"Appropriate training conducted\" date.";
                return;
            }
            //else
            //{
            //    try
            //    {
            //        if (Convert.ToDateTime(txtAppropriateTrainingConducted.Text.Trim()) > DateTime.Today)
            //        {
            //            lblMsgPhase1.Text = " \"Appropriate training conducted\" date should be less than or equal to current date.";
            //            return;
            //        }
            //        if (Convert.ToDateTime(txtAppropriateTrainingConducted.Text.Trim()) < Convert.ToDateTime(GetApprovalDate()))
            //        {
            //            lblMsgPhase1.Text = " \"Appropriate training conducted\" date should be more than or equal to commenced date.";
            //            return;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        lblMsgPhase1.Text = " \"Appropriate training conducted\" date is invalid.";                    
            //        return;
            //    }
            //}
            //----------------------------------------------------------------------------------------------
            if (txtDetailsOfChangeCommunicated.Text.Trim() == "")
            {
                lblMsgPhase1.Text = "Please enter \"Details of change communicated to all concerned\" date.";
                return;
            }
            //else
            //{
            //    try
            //    {
            //        if (Convert.ToDateTime(txtDetailsOfChangeCommunicated.Text.Trim()) > DateTime.Today)
            //        {
            //            lblMsgPhase1.Text = " \"Details of change communicated to all concerned\" date should be more than or equal to current date.";
            //            return;
            //        }
            //        if (Convert.ToDateTime(txtDetailsOfChangeCommunicated.Text.Trim()) < Convert.ToDateTime(GetApprovalDate()))
            //        {
            //            lblMsgPhase1.Text = " \"Details of change communicated to all concerned\" date should be more than or equal to commenced date.";
            //            return;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        lblMsgPhase1.Text = " \"Details of change communicated to all concerned\" date is invalid.";
            //        return;
            //    }
            //}
            
            //----------------------------------------------------------------------------------------------
            Common.Set_Procedures("dbo.MOC_MOC_ImplementationPhaseI");
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
                ShowMocRecord();
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
                lblMsgPhase2.Text = "Please enter \"Change process commenced\" date.";
                return;
            }
            //else
            //{
            //    try
            //    {
            //        if (Convert.ToDateTime(txtChangeProcessComenced.Text.Trim()) > DateTime.Today)
            //        {
            //            lblMsgPhase2.Text = " \"Change process commenced\" date should be less than or equal to current date.";
            //            return;
            //        }
            //        if (Convert.ToDateTime(txtChangeProcessComenced.Text.Trim()) < Convert.ToDateTime(lblDetailsOfChangeCommunicated.Text.Trim()))
            //        {
            //            lblMsgPhase2.Text = " \"Change process commenced\" date should be more than or equal to \"Details of change communicated to all concerned\" ";
            //            return;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        lblMsgPhase2.Text = " \"Change process commenced\" date is invalid.";
            //        return;
            //    }
            //}
            //------------------------------------------------------------------------------------------------------------
            if (txtChangeprocesscompleted.Text.Trim() == "")
            {
                lblMsgPhase2.Text = "Please enter \"Change process completed\" date.";
                return;
            }
            //else
            //{
            //    try
            //    {
            //        if (Convert.ToDateTime(txtChangeprocesscompleted.Text.Trim()) > DateTime.Today)
            //        {
            //            lblMsgPhase2.Text = "\"Change process completed\" date should be less than or equal to current date.";
            //            return;
            //        }
            //        if (Convert.ToDateTime(txtChangeprocesscompleted.Text.Trim()) < Convert.ToDateTime(lblDetailsOfChangeCommunicated.Text.Trim()))
            //        {
            //            lblMsgPhase2.Text = "\"Change process completed\" date should be more than or equal to \"Details of change communicated to all concerned\".";
            //            return;
            //        }
                    
            //    }
            //    catch (Exception ex)
            //    {
            //        lblMsgPhase2.Text = "\"Change process completed\" date is invalid.";
            //        return;
            //    }
            //}

            //------------------------------------------------------------------------------------------------
            //txtRequestForExtedntionToTargetDate
            try
            {
                if (txtRequestForExtedntionToTargetDate.Text.Trim() != "")
                {
                    if (Convert.ToDateTime(txtRequestForExtedntionToTargetDate.Text.Trim()) > DateTime.Today)
                    {
                        //lblMsgPhase2.Text = "\"Request for extension to target date\" date should be less than or equal to current date.";
                        //return;
                    }
                    if (Convert.ToDateTime(txtRequestForExtedntionToTargetDate.Text.Trim()) < Convert.ToDateTime(lblDetailsOfChangeCommunicated.Text.Trim()))
                    {
                        //lblMsgPhase2.Text = "\"Request for extension to target date\" date should be more than or equal to \"Details of change communicated to all concerned\".";
                        //return;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                lblMsgPhase2.Text = "\"Request for extension to target date\" date is invalid.";
                return;
            }

            //--------------------------------------------------------------------------------------------------------------------------------
            try
            {
                if (txtApprovalToTargetDate.Text.Trim() != "")
                {
                    if (Convert.ToDateTime(txtApprovalToTargetDate.Text.Trim()) > DateTime.Today)
                    {
                        //lblMsgPhase2.Text = "\"Approval to extended target \" date should be less than or equal to current date.";
                        //return;
                    }
                    if (Convert.ToDateTime(txtApprovalToTargetDate.Text.Trim()) < Convert.ToDateTime(lblDetailsOfChangeCommunicated.Text.Trim()))
                    {
                        //lblMsgPhase2.Text = "\"Approval to extended target \" date should be more than or equal to \"Details of change communicated to all concerned\".";
                        //return;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsgPhase2.Text = "\"Approval to extended target \" date is invalid.";
                return;
            }
            //-----------------------------------------------------------------------------------------------------------------------------


            Common.Set_Procedures("dbo.MOC_MOC_ImplementationPhaseII");
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
                ShowMocRecord();
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
                lblMsgEndorsment.Text = "Please enter remarks";
                return;
            }

            Common.Set_Procedures("dbo.MOC_MOC_Endorsment");
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
                ShowMocRecord();
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

            Common.Set_Procedures("dbo.MOC_MOC_SendBack");
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
                Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
                //BindStages();
                //ShowMocRecord();
                //ClearRequestForApprovalStage();
                //dv_UpdateStage.Visible = false;
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
            Common.Set_Procedures("dbo.MOC_MOC_CancelChangeRequest");
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

            Common.Set_Procedures("dbo.MOC_MOC_Review");
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
                ShowMocRecord();
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
    protected void btnDownloadAttachment_OnClick(object sender, EventArgs e)
    {
        string FileName = "";
        byte[] FileContent = new byte[] { };
        string sql = " Select soc_RaFileName,soc_RaFileContent from DBO.MOC_RECORD Where MocID="+MocId;
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

    // Edit MOC Record----------------------------
    protected void btnEditMoc_OnClick(object sender, EventArgs e)
    {
        div_EditNewMOC.Visible = true;
        Load_ForwardedTo();
        ShowMocDataForEdit();
    }
    protected void btnSaveNew_Click(object sender, EventArgs e)
    {
        if (ddlSource.SelectedIndex == 0)
        {
            lblMsgPopup.Text = "Please select source.";
            return;
        }
        if (ddlVessel_Office.SelectedIndex == 0)
        {
            lblMsgPopup.Text = "Please select VSL/Office.";
            return;
        }
        if (txtTopic.Text.Trim() == "")
        {
            lblMsgPopup.Text = "Please enter topic.";
            return;
        }
        string Impact = "";
        foreach (ListItem lst in cbImpact.Items)
        {
            if (lst.Selected)
            {
                Impact += lst.Value + ",";
            }
        }

        if (Impact.Trim().Length > 0)
        {
            Impact = Impact.Remove(Impact.LastIndexOf(','));
        }

        if (Impact.Trim() == "")
        {
            lblMsgPopup.Text = "Please select impact.";
            cbImpact.Focus();
            return;
        }
        if (txtReasonforChange.Text.Trim() == "")
        {
            lblMsgPopup.Text = "Please enter reason for change.";
            return;
        }
        if (txtDescr.Text.Trim() == "")
        {
            lblMsgPopup.Text = "Please enter brief description of change.";
            return;
        }
        DateTime dtPTL;
        if (DateTime.TryParse(txtPropTL.Text.Trim(), out dtPTL))
        {
            if (dtPTL < DateTime.Today)
            {
                //lblMsgPopup.Text = "Proposed TimeLine must be more than today.";
                //return;
            }
        }
        else
        {
            lblMsgPopup.Text = "Please enter proposed TimeLine for completion of change.";
            return;
        }

        if (ddlForwardedTo.SelectedIndex == 0)
        {
            lblMsgPopup.Text = "Please select forwarded to.";
            return;
        }

        try
        {
            Common.Set_Procedures("dbo.MOC_Edit_MOC_Request");
            Common.Set_ParameterLength(12);
            Common.Set_Parameters(
                new MyParameter("@MOCID", MocId),
                new MyParameter("@OfficeVessel", ddlSource.SelectedValue),
                new MyParameter("@VesselId", (ddlSource.SelectedValue == "S") ? ddlVessel_Office.SelectedValue : "0"),
                new MyParameter("@OfficeId", (ddlSource.SelectedValue == "O") ? ddlVessel_Office.SelectedValue : "0"),
                new MyParameter("@Topic", txtTopic.Text.Trim()),
                new MyParameter("@Impact", Impact),
                new MyParameter("@ReasonForChange", txtReasonforChange.Text.Trim()),
                new MyParameter("@DescriptionForChange", txtDescr.Text.Trim()),
                new MyParameter("@ProposedTimeline", txtPropTL.Text.Trim()),
                new MyParameter("@ClosedBy", Session["loginid"].ToString()),
                new MyParameter("@ClosureComments", txtForwardedComments.Text.Trim()),
                new MyParameter("@WaitingBy", ddlForwardedTo.SelectedValue)
                );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);

            if (res)
            {
                //ScriptManager.RegisterStartupScript(Page, this.GetType(), "aa", "alert('')", true);
                lblMsgPopup.Text = "MOC record updated successfully.";
                Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
            }
            else
            {
                lblMsgPopup.Text = "Unable to update MOC Request.Error :" + Common.getLastError();
            }

        }
        catch (Exception ex)
        {

            lblMsgPopup.Text = "Unable to update MOC Request.Error :" + ex.Message.ToString();
        }
    }
    protected void btnCloseEditMoc_Click(object sender, EventArgs e)
    {
        div_EditNewMOC.Visible = false;
    }
    protected void ddlSource_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSource.SelectedValue == "S")
        {
            Load_vessel();
        }
        else if (ddlSource.SelectedValue == "O")
        {
            Load_office();
        }
        else
        {
            ddlVessel_Office.Items.Clear();
            ddlVessel_Office.Items.Insert(0, new ListItem(" < All Office >", "0"));
        }
    }

    private void ShowMocDataForEdit()
    {
        DataSet dt = Budget.getTable(" SElect * from dbo.MOC_RECORD where mociD="+MocId);
        if (dt.Tables[0].Rows.Count > 0)
        {
            DataRow dr = dt.Tables[0].Rows[0];
            ddlSource.SelectedValue = dr["OfficeVessel"].ToString();
            if (ddlSource.SelectedValue == "S")
            {
                Load_vessel();
                ddlVessel_Office.SelectedValue = dr["VesselId"].ToString();
            }
            else
            {
                Load_office();
                ddlVessel_Office.SelectedValue = dr["OfficeId"].ToString();
            }


            string[] ImpactArray = dr["Impact"].ToString().Split(',');
            
            foreach (ListItem item in cbImpact.Items)
            {
                if (ImpactArray.Where(s => s == item.Value).Count() > 0)
                {
                    item.Selected = true;

                }
            }


            txtTopic.Text = dr["Topic"].ToString();
            txtReasonforChange.Text = dr["ReasonForChange"].ToString();
            txtDescr.Text = dr["DescriptionForChange"].ToString();
            txtPropTL.Text = Common.ToDateString( dr["TargetDate"]).ToString();
            txtForwardedComments.Text = dr["StageClosedComments"].ToString();
            //ddlForwardedTo.SelectedValue=  dr["WaitingBy"].ToString();
        }
        
        
    }
    private void Load_vessel()
    {
        DataSet dt = Budget.getTable("Select * from dbo.vessel where vesselstatusid<>2  order by vesselname");
        ddlVessel_Office.DataSource = dt;
        ddlVessel_Office.DataTextField = "VesselName";
        ddlVessel_Office.DataValueField = "VesselID";
        ddlVessel_Office.DataBind();
        ddlVessel_Office.Items.Insert(0, new ListItem(" < Select Vessel >", "0"));
    }
    private void Load_office()
    {
        DataSet dt = Budget.getTable("SELECT OfficeId, OfficeName, OfficeCode FROM [dbo].[Office] Order By OfficeName");
        ddlVessel_Office.DataSource = dt;
        ddlVessel_Office.DataTextField = "OfficeName";
        ddlVessel_Office.DataValueField = "OfficeId";
        ddlVessel_Office.DataBind();
        ddlVessel_Office.Items.Insert(0, new ListItem(" < Select Office>", "0"));
    }
    private void Load_ForwardedTo()
    {
        int LoginId = Common.CastAsInt32(Session["loginid"]);
        DataSet dt = Budget.getTable("select * from [dbo].[GET_REPORTING_CHAIN_INCLUDE_SELF_UserID] (" + LoginId + ")");
        ddlForwardedTo.DataSource = dt;
        ddlForwardedTo.DataTextField = "EmpName";
        ddlForwardedTo.DataValueField = "LoginId";
        ddlForwardedTo.DataBind();
        ddlForwardedTo.Items.Insert(0, new ListItem(" Select ", "0"));

    }
}
