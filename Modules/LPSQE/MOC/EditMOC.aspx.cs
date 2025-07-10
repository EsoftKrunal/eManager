using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class HSSQE_MOC_EditMOC : System.Web.UI.Page
{
    public int MOCId
    {
        get { return Common.CastAsInt32(ViewState["MOCId"]); }
        set { ViewState["MOCId"] = value; }
    }
    public int MOCStage
    {
        get { return Common.CastAsInt32(ViewState["MOCStage"]); }
        set { ViewState["MOCStage"] = value; }
    }
    public string MOCStatus
    {
        get { return ViewState["MOCStatus"].ToString(); }
        set { ViewState["MOCStatus"] = value; }
    }
    public int Responsibility
    {
        get { return Common.CastAsInt32(ViewState["Responsibility"]); }
        set { ViewState["Responsibility"] = value; }
    }
    public int UserId
    {
        get { return Common.CastAsInt32(ViewState["UserId"]); }
        set { ViewState["UserId"] = value; }
    }
    public string UserName
    {
        get { return ViewState["UserName"].ToString(); }
        set { ViewState["UserName"] = value; }
    }
    AuthenticationManager auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblMsg.Text = "";
        UserId = Common.CastAsInt32(Session["loginid"]);
        auth = new AuthenticationManager(314, UserId, ObjectType.Page);
        if (!IsPostBack)
        {
            MOCId = Common.CastAsInt32(Request.QueryString["MOCId"]);
            
            UserName = Session["UserName"].ToString();
            if (MOCId > 0)
            {
                rpt_Events.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT *  FROM EV_EventMaster ORDER BY EVENTNAME");
                rpt_Events.DataBind();

                BindVessel();
                BindOffice();
                BindResponsiblePersons();
                ShowRequest();
                txtTanel.Text = "0";// MOCStage.ToString();
                btnPost_Click(sender, e);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "aa", "alert('Invalid MOC#.');window.close();", true);
            }
        }
    }
    protected void BindVessel()
    {
        DataTable dtVessels = new DataTable();
        string strvessels = "SELECT VesselId,VesselCode,VesselName FROM VesselMaster VM WHERE EXISTS(SELECT TOP 1 COMPONENTID FROM VSL_ComponentMasterForVessel EVM WHERE VM.VesselCode = EVM.VesselCode ) ORDER BY VesselName ";
        dtVessels = Common.Execute_Procedures_Select_ByQuery(strvessels);
        if (dtVessels.Rows.Count > 0)
        {
            ddlVessel.DataSource = dtVessels;
            ddlVessel.DataTextField = "VesselName";
            ddlVessel.DataValueField = "VesselCode";
            ddlVessel.DataBind();

            ddlVessel.Items.Insert(0, new ListItem("< All >", "0"));
            ddlVessel.SelectedIndex = 0;
        }
    }
    public void BindOffice()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM ShipSoft.dbo.Office");

        if (dt.Rows.Count > 0)
        {
            ddlOffice.DataSource = dt;
            ddlOffice.DataTextField = "OfficeName";
            ddlOffice.DataValueField = "OfficeId";
            ddlOffice.DataBind();

            ddlOffice.Items.Insert(0, new ListItem("< All >", "0"));
        }
    }
    public void BindResponsiblePersons()
    {
        //DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT (FirstName + ' ' + MiddleName + ' ' + FamilyName) As NAme, UserId FROM Hr_PersonalDetails ORDER By Name");

        //ddlresponsibleperson.DataSource = dt;
        //ddlresponsibleperson.DataTextField = "Name";
        //ddlresponsibleperson.DataValueField = "UserId";
        //ddlresponsibleperson.DataBind();

        //ddlresponsibleperson.Items.Insert(0, new ListItem("< Select >", ""));

    }
    public string HandleStringforDB(string InText)
    {
        return InText.Replace("'", "''").Replace("\"", "`").Trim();
    }
    // First Tab ( Request )

    public void ShowRequest()
    {
        string SQL = "SELECT *,(select OFFICENAME from office where officeid=Moc_Request.officeid) AS OFFICENAME,(SELECT (FirstName + ' ' + MiddleName + ' ' + FamilyName) FROM [Hr_PersonalDetails] WHERE USERId= ReviewedBy) As ReviewedByName, (SELECT (FirstName + ' ' + MiddleName + ' ' + FamilyName) FROM [Hr_PersonalDetails] WHERE USERId= EndorsedBy) AS EndorsedByName, (SELECT (FirstName + ' ' + MiddleName + ' ' + FamilyName) FROM [Hr_PersonalDetails] WHERE USERId= Approved1By) AS Approved1ByName, " +
                     "(SELECT (FirstName + ' ' + MiddleName + ' ' + FamilyName) FROM [Hr_PersonalDetails] WHERE USERId= RequestedBy) AS RequestedByName, " +
                     //"(SELECT (FirstName + ' ' + MiddleName + ' ' + FamilyName) FROM [Hr_PersonalDetails] WHERE USERId= ApprovedBy) AS ApprovedByName, " +
                     "(SELECT (FirstName + ' ' + MiddleName + ' ' + FamilyName) FROM [Hr_PersonalDetails] WHERE USERId= AssesmentBy) AS AssesmentByName " +
                     "FROM Moc_Request WHERE MocRequestId=" + MOCId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt.Rows.Count > 0)
        {
            DataRow dr=dt.Rows[0];
            lblMOCNO.Text = dr["MOCNumber"].ToString();
            lblSource.Text = dr["Source"].ToString();
            if (lblSource.Text == "Vessel")
                lblLocation.Text = dr["VesselCode"].ToString();
            else
                lblLocation.Text = dr["OFFICENAME"].ToString();
            
            //txtMOCDate.Text =Common.ToDateString(dr["MOCdATE"]);
            hfMOCDate.Value = Common.ToDateString(dr["MOCdATE"]);
            txtTopic.Text = dr["Topic"].ToString();
            txtReasonforChange.Text = dr["ReasonForChange"].ToString();
            txtDescr.Text = dr["DescrOfChange"].ToString();
            txtPropTL.Text = Common.ToDateString(dr["ProposedTimeline"]);
            string Impact=dr["Impact"].ToString() + ",";
            foreach (ListItem li in cbImpact.Items)
            {
                if(Impact.Contains(li.Value + ","))
                {
                    li.Selected=true;
                }
            }

            lblRequestedBy.Text = dr["RequestedByName"].ToString();
            lblRequestedOn.Text = Common.ToDateString(dr["RequestedOn"]);
            MOCStage = Common.CastAsInt32(dr["Stage"]);
            MOCStatus = dr["Status"].ToString();
            
            //------------------- Risk Assessment ------------------------
            txtRiskRefNum.Text = dr["RiskNumber"].ToString();      
            hfRiskId.Value = dr["RiskKey"].ToString();
            lnkOpenRisk.Visible = (txtRiskRefNum.Text.Trim() != "" && hfRiskId.Value.Trim() != "");  
            
            if(hfRiskId.Value.Trim()!="")
            {
                if (hfRiskId.Value.Trim().StartsWith("S"))
                {
                    string VesselCode = hfRiskId.Value.Split('_').GetValue(1).ToString();
                    int _RiskId = Common.CastAsInt32(hfRiskId.Value.Trim().Split('_').GetValue(2));
                    DataTable dt11 = Common.Execute_Procedures_Select_ByQuery("SELECT EVENTNAME FROM EV_EventMaster M WHERE M.EVENTID IN ( SELECT EVENTID FROM EV_VSL_RISKMGMT_MASTER WHERE VesselCode='" + VesselCode + "' AND RISKID=" + _RiskId + ")");
                    if (dt11.Rows.Count > 0)
                        lblEventName.Text = " [ " + dt11.Rows[0]["EVENTNAME"].ToString() + " ] ";
                }
                else
                {
                    int OfficeId = Common.CastAsInt32(hfRiskId.Value.Trim().Split('_').GetValue(1));
                    int _RiskId = Common.CastAsInt32(hfRiskId.Value.Trim().Split('_').GetValue(2));

                    DataTable dt11 = Common.Execute_Procedures_Select_ByQuery("SELECT EVENTNAME FROM EV_EventMaster M WHERE M.EVENTID IN ( SELECT EVENTID FROM EV_Off_RISKMGMT_MASTER WHERE OFFICEID=" + OfficeId + " AND RISKID="  + _RiskId + ")");
                    if (dt11.Rows.Count > 0)
                        lblEventName.Text = " [ " + dt11.Rows[0]["EVENTNAME"].ToString() + " ] ";
                }
            }

            radR.Checked = (dr["RiskFileName"].ToString() != "");
            radNR.Checked = (dr["RiskFileName"].ToString() == "");

            radR_OnCheckedChanged(null, null);

            if (dr["RiskFileName"].ToString() != "")
            {
                btnClip.Visible = true;
                btnClipText.Text = dr["RiskFileName"].ToString();
                btnClipText.Visible = true;
            }
            

            txtCommunication.Text = dr["People_Communication"].ToString();
            txtTraining.Text = dr["People_Training"].ToString();
            txtSMSReview.Text = dr["Process_SMSReview"].ToString();
            txtDrawing.Text = dr["Process_Drawing"].ToString();
            txtDocumentation.Text = dr["Process_Documentation"].ToString();
            txtEquipment.Text = dr["Equipment"].ToString();

            lblAssessmentBy.Text = dr["AssesmentByName"].ToString();
            lblAssessmentOn.Text = Common.ToDateString(dr["AssesmentOn"]);

            //------------------- Approval ------------------------

            radYes.Checked = dr["Approved1"].ToString() == "Y";
            radNo.Checked = dr["Approved1"].ToString() == "N";

            radChangeT.Checked = dr["ChangeType"].ToString() == "T";
            radChangeP.Checked = dr["ChangeType"].ToString() == "P";

            txtReviewDate.Text = Common.ToDateString(dr["ReviewDate"]);
            if(txtReviewDate.Text.Trim()!="")
                lblReviewDate.Text=" ( To be reviewed on : " + txtReviewDate.Text + " ) ";

            lblApprovedBy.Text = dr["Approved1ByName"].ToString();
            lblApprovedOn.Text = Common.ToDateString(dr["Approved1On"]);
            txtApproverComments.Text = dr["Approved1Comments"].ToString();
            
            //------------------- Review ------------------------
           
            rdochagneneccearsyYes.Checked = dr["IsChangeEffective"].ToString() == "Y";
            rdochagneneccearsyNo.Checked = dr["IsChangeEffective"].ToString() == "N";
            
            lblReviewedBy.Text = dr["ReviewedByName"].ToString();
            lblReviewedOn.Text = Common.ToDateString(dr["ReviewedOn"]);
            txtReviewComments.Text = dr["ReviewComments"].ToString();

            //------------------- Endorse ------------------------

            lblEndorsedBy.Text = dr["EndorsedByName"].ToString();
            lblEndorsedOn.Text = Common.ToDateString(dr["EndorsedOn"]);
            txtSuggestionforimprovement.Text = dr["SuggestionForImprovement"].ToString();
        }
    }
    protected void btnSaveNew_Click(object sender, EventArgs e)
    {
        string Impact = "";
        
        if (txtTopic.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter Topic.";
            txtTopic.Focus();
            return;
        }

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
            lblMsg.Text = "Please select Impact.";
            cbImpact.Focus();
            return;
        }
        if (txtPropTL.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter proposed time line.";
            txtPropTL.Focus();
            return;
        }

        string SQL = "UPDATE SNQ.DBO.Moc_Request SET Topic='" + HandleStringforDB(txtTopic.Text) + "', Impact='" + Impact + "',ReasonForChange='" + HandleStringforDB(txtReasonforChange.Text) + "',ProposedTimeline='" + txtPropTL.Text.Trim() + "',DescrOfChange='" + HandleStringforDB(txtDescr.Text) + "'  WHERE MocRequestId =" + MOCId;
        Common.Execute_Procedures_Select_ByQuery(SQL);
        Common.Execute_Procedures_Select_ByQuery("UPDATE SNQ.DBO.Moc_Request SET Stage = 1 WHERE MocRequestId =" + MOCId);
        MOCStage = 1;
        btnPost_Click(sender, e);
        lblMsg.Text = " MOC updated successfully.";
    }
    
    // IIIrd Tab ( Risk Assesment )
    protected void lnkOpenRisk_Click(object sender, EventArgs e)
    {
        if (txtRiskRefNum.Text.Trim() != "" && hfRiskId.Value.Trim() != "")
        {
            string VesselCode = "";
            int OfficeId = 0;
            int _RiskId = 0;

            if (hfRiskId.Value.Trim().StartsWith("S"))
            {
                VesselCode = hfRiskId.Value.Trim().Split('_').GetValue(1).ToString();
                _RiskId = Common.CastAsInt32(hfRiskId.Value.Trim().Split('_').GetValue(2));
            }
            else
            {
                OfficeId = Common.CastAsInt32(hfRiskId.Value.Trim().Split('_').GetValue(1));
                _RiskId = Common.CastAsInt32(hfRiskId.Value.Trim().Split('_').GetValue(2));
            }



            if (VesselCode.Trim().Length == 3 && OfficeId == 0)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "edit", "window.open('../RiskManagement/ViewRisk.aspx?VesselCode=" + VesselCode + "&RiskId=" + _RiskId + "','');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "edit", "window.open('../RiskManagement/AddRisk.aspx?RiskId=" + _RiskId + "&OfficeId=" + OfficeId + "','');", true);
            }
        }
        else
        {
            lblMsg.Text = "Risk not selected.";
        }
    }
    protected void btnAddRisk_Click(object sender, EventArgs e)
    {
        dv_RiskTopics.Visible = true;
    }
    protected void btnCancelNewRisk_Click(object sender, EventArgs e)
    {
        dv_RiskTopics.Visible = false;
    }
    protected void btnSelectEvent_Click(object sender, EventArgs e)
    {
        int Key = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        dv_RiskTopics.Visible = false;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "new", "openRiskWindow(" + Key + ");", true);
    }
    protected void btnSelectRisk_Click(object sender, EventArgs e)
    {
        BindGrid();
        dv_RiskSelection.Visible = true;
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ddlVessel.SelectedIndex = 0;
        ddlOffice.SelectedIndex = 0;
        txtRefNo.Text = "";
        ddlStatus.SelectedValue = "O";
        dv_RiskSelection.Visible = false;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void BindGrid()
    {
        string WhereClause = "WHERE 1=1 ";

        if (ddlStatus.SelectedIndex > 0)
            WhereClause += " AND STATUS='" + ddlStatus.SelectedValue + "' ";

        if (ddlVessel.SelectedIndex > 0 && ddlOffice.SelectedIndex > 0)
            WhereClause += " AND (VESSELCODE='" + ddlVessel.SelectedValue + "'  OR OfficeId=" + ddlOffice.SelectedValue + " ) ";

        if (ddlVessel.SelectedIndex > 0 && ddlOffice.SelectedIndex == 0)
            WhereClause += " AND VESSELCODE='" + ddlVessel.SelectedValue + "' ";

        if (ddlOffice.SelectedIndex > 0 && ddlVessel.SelectedIndex == 0)
            WhereClause += " AND OfficeId=" + ddlOffice.SelectedValue + " ";

        if (txtRefNo.Text.Trim() != "")
            WhereClause += " AND RefNo Like '%" + txtRefNo.Text.Trim() + "%' ";

        

        string SQL = "SELECT * FROM ( " +
                     "SELECT RM.*,V.VESSELName,E.EVENTName,0 As OfficeId FROM EV_VSL_RISKMGMT_MASTER RM INNER JOIN [EV_EventMaster] E ON RM.EVENTID=E.EVENTID INNER JOIN VESSELMASTER V ON RM.VESSELCODE=V.VESSELCODE  " +
                     "UNION  " +
                     "SELECT [RISKID],[OfficeName],ORM.[EVENTID],[EVENTDATE],[REFNO],[RiskDescr],[ALTERNATEMETHODS],[Details],[HOD_NAME],[SAF_OFF_NAME],[MASTER_NAME],[STATUS],[OFFICE_COMMENTS],[OFFICECOMMENTBY],[DESIGNATION],[COMMENTDATE],CREATED_BY,CREATED_ON,Position,OfficeName AS VESSELName,OE.EVENTName, ORM.OfficeId " +
                     "FROM EV_Off_RISKMGMT_MASTER ORM  " +
                     "INNER JOIN [EV_EventMaster] OE ON ORM.EVENTID=OE.EVENTID  " +
                     "INNER JOIN Office O ON ORM.OfficeId=O.OfficeId  " +
                     ") a ";
        rptRisks.DataSource = Common.Execute_Procedures_Select_ByQuery(SQL + WhereClause);
        rptRisks.DataBind();
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        string RiskId = ((ImageButton)sender).CommandArgument.ToString().Trim();

        string VesselCode = ((ImageButton)sender).Attributes["VesselCode"];
        int OfficeId = Common.CastAsInt32(((ImageButton)sender).Attributes["OfficeId"]);
        txtRiskRefNum.Text = ((ImageButton)sender).Attributes["RefNo"].ToString().Trim();

        if (VesselCode.Trim() != "" && OfficeId == 0)
        {
            hfRiskId.Value = "S_" + VesselCode.Trim() + "_" + RiskId.Trim();
        }
        else
        {
            hfRiskId.Value = "O_" + OfficeId + "_" + RiskId.Trim();
        }

        lnkOpenRisk.Visible = true;

        btnClose_Click(sender, e);
    }
    protected void btnSelectRiskByAdd_Click(object sender, EventArgs e)
    {
        int RiskId = Common.CastAsInt32(hfdRiskId.Value);
        int OfficeId = Common.CastAsInt32(hfdOfficeId.Value);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT REFNO FROM dbo.EV_Off_RISKMGMT_MASTER WHERE RISKID=" + RiskId);
        if (dt.Rows.Count > 0)
        {
            txtRiskRefNum.Text = dt.Rows[0][0].ToString();
            hfRiskId.Value = "O_" + OfficeId + "_" + RiskId;
        }
        lnkOpenRisk.Visible = true;
    }

    protected void btnSaveRA_Click(object sender, EventArgs e)
    {
        string FileName = "";
        byte[] FileContent = new byte[0];

        
        if (radR.Checked)
        {
            if (!flpUpload.HasFile && btnClipText.Text.Trim() == "")
            {
                lblMsg.Text = "Please select attachment.";
                return;
            }
            

                if(flpUpload.HasFile)
                {
	            string TempNameOnly = System.IO.Path.GetFileNameWithoutExtension(flpUpload.FileName);
	            string TempExtOnly = System.IO.Path.GetExtension(flpUpload.FileName);
                    FileName = TempNameOnly + DateTime.Now.ToString("dd-mm-yyy-hh-mm-tt") + TempExtOnly;
		    FileContent = flpUpload.FileBytes;
                }                
          
        }
	if(FileName.Trim()!="" && FileContent.Length<=0)
	{
                lblMsg.Text = "Attachment is not valid.";
                return;
        }

        try
        {

            Common.Set_Procedures("SNQ.dbo.MOC_Update_RA_MocRequest");
            Common.Set_ParameterLength(12);
            Common.Set_Parameters(
                new MyParameter("@MocRequestId", MOCId),
                new MyParameter("@RiskNumber", txtRiskRefNum.Text.Trim()),
                new MyParameter("@RiskKey", hfRiskId.Value.Trim()),
                new MyParameter("@RiskAttachment", FileContent),
                new MyParameter("@FileName", FileName.Trim()),
                new MyParameter("@People_Communication", txtCommunication.Text.Trim()),
                new MyParameter("@People_Training", txtTraining.Text.Trim()),
                new MyParameter("@Process_SMSReview", txtSMSReview.Text.Trim()),
                new MyParameter("@Process_Drawing", txtDrawing.Text.Trim()),
                new MyParameter("@Process_Documentation", txtDocumentation.Text.Trim()),
                new MyParameter("@Equipment", txtEquipment.Text.Trim()),
                new MyParameter("@AssesmentBy", UserId)

                );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);

            if (res)
            {
                btnPost_Click(sender, e);
                lblMsg.Text = "Risk assessment saved successfully.";
            }
            else
            {
                lblMsg.Text = "Unable to save.Error :" + Common.getLastError();
            }

        }
        catch (Exception ex)
        {

            lblMsg.Text = "Unable to save.Error :" + ex.Message.ToString();
        }

    }
    protected void btnNotifyRA_Click(object sender, EventArgs e)
    {
        try
        {
            SendeMail(2,"True", 0);

            Common.Execute_Procedures_Select_ByQuery("UPDATE SNQ.DBO.Moc_Request SET Stage = 2  WHERE MocRequestId =" + MOCId);
            MOCStage = 2;
            btnPost_Click(sender, e);
            lblMsg.Text = " Notified successfully.";
        }
        catch (Exception ex)
        {
            lblMsg.Text = " Unable to Notify. Error : " + ex.Message;
        }
    }

    // IVth Tab ( Approval )
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        //string SQL = "UPDATE SNQ.DBO.Moc_Request SET Approved1='" + (radYes.Checked ? "Y" : "N") + "', ReviewDate='" + txtReviewDate.Text.Trim() + "', Approved1By= " + UserId + ", [Approved1On]=getdate(), [Approved1Comments]='" + txtApproverComments.Text.Trim().Replace("'", "`") + "'  WHERE MocRequestId =" + MOCId;
        string SQL = "UPDATE SNQ.DBO.Moc_Request SET Approved1='" + (radYes.Checked ? "Y" : "N") + "',ChangeType='" + (radChangeT.Checked ? "T" : "P") + "', ReviewDate='" + txtReviewDate.Text.Trim() + "', Approved1By= " + UserId + ", [Approved1On]=getdate(), [Approved1Comments]='" + HandleStringforDB(txtApproverComments.Text) + "'  WHERE MocRequestId =" + MOCId;
        Common.Execute_Procedures_Select_ByQuery(SQL);
        btnPost_Click(sender, e);
        lblMsg.Text = " Approved successfully.";
    }
    protected void btnNotifyApprove_Click(object sender, EventArgs e)
    {
        try
        {
            SendeMail(3,"True", 0);

            Common.Execute_Procedures_Select_ByQuery("UPDATE SNQ.DBO.Moc_Request SET Stage = 3  WHERE MocRequestId =" + MOCId);
            MOCStage = 3;
            btnPost_Click(sender, e);
            lblMsg.Text = " Notified successfully.";
        }
        catch (Exception ex)
        {
            lblMsg.Text = " Unable to Notify. Error : " + ex.Message;
        }
    }

    // Vth Tab ( Review )
    protected void btnSaveReview_Click(object sender, EventArgs e)
    {
        //DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT (SELECT PositionName FROM Position WHERE PositionId= Position AND OfficeId=Office) As Position FROM [Hr_PersonalDetails] WHERE USERId=" + UserId);
        if (!rdochagneneccearsyYes.Checked && !rdochagneneccearsyNo.Checked)
        {
            lblMsg.Text = "Please choose change necessary .";
            rdochagneneccearsyYes.Focus();
            return;
        }
        

        string SQL = "UPDATE Moc_Request SET IsChangeEffective='" + (rdochagneneccearsyYes.Checked ? "Y" : "N") + "', ReviewedOn=getdate(), [ReviewedBy]=" + UserId + ", [ReviewComments]='" + HandleStringforDB(txtReviewComments.Text) + "'  WHERE MocRequestId =" + MOCId;
        Common.Execute_Procedures_Select_ByQuery(SQL);
        btnPost_Click(sender, e);
        lblMsg.Text = " Review saved successfully.";
    }
    protected void btnNotifyReview_Click(object sender, EventArgs e)
    {
        try
        {
            SendeMail(4,"True", 0);

            Common.Execute_Procedures_Select_ByQuery("UPDATE SNQ.DBO.Moc_Request SET Stage = 4  WHERE MocRequestId =" + MOCId);
            MOCStage = 4;
            btnPost_Click(sender, e);
            lblMsg.Text = " Notified successfully.";
        }
        catch (Exception ex)
        {
            lblMsg.Text = " Unable to Notify. Error : " + ex.Message;
        }
    }

    // VIth Tab ( Endorsement )
    protected void btnSaveEndorsement_Click(object sender, EventArgs e)
    {
        //DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT (SELECT PositionName FROM Position WHERE PositionId= Position AND OfficeId=Office) As Position FROM [Hr_PersonalDetails] WHERE USERId=" + UserId);

        string SQL = "UPDATE Moc_Request SET Status='C',EndorsedOn=getdate(), [EndorsedBy]=" + UserId + ", [SuggestionForImprovement]='" + HandleStringforDB(txtSuggestionforimprovement.Text) + "'  WHERE MocRequestId =" + MOCId;
       
        Common.Execute_Procedures_Select_ByQuery(SQL);
        btnPost_Click(sender, e);
        lblMsg.Text = " Endorsed successfully.";
    }
    protected void btnClip_Click(object sender, ImageClickEventArgs e)
    {

        btnDownload_Click(sender,e);
    }
    protected void btnClipText_Click(object sender, EventArgs e)
    {
        btnDownload_Click(sender, e);
    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        DownloadFile();
    }
    protected void DownloadFile()
    {

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT RiskAttachment, RiskFileName FROM SNQ.DBO.Moc_Request WHERE MocRequestId=" + MOCId);
        if (dt.Rows.Count > 0)
        {

            string FileName = dt.Rows[0]["RiskFileName"].ToString();
            //string Path = Server.MapPath("~/UserUploadedDocuments/Circular/" + FileName);
            if (FileName.Trim() != "")
            {
                byte[] buff = (byte[])dt.Rows[0]["RiskAttachment"];
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                Response.BinaryWrite(buff);
                Response.Flush();
                Response.End();
            }
        }
    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        ShowRequest();
        for(int i=0;i<=4;i++)
        {
            this.FindControl("pnl_" +i.ToString()).Visible=false; 
        }
     
        for (int i = 0; i <= 4; i++)
        {
            string classname = (i < MOCStage) ? "wizardboxdone" : "wizardbox";
            ((HtmlControl)this.FindControl("dv_" + i.ToString())).Attributes.Add("class", classname);
        }

        this.FindControl("pnl_" + Common.CastAsInt32(txtTanel.Text)).Visible = true;
        
        //========================
        btnSaveNew.Visible = MOCStage == 0;
        //========================
        btnSaveRA.Visible = MOCStage == 1;
        btnSelectRisk.Visible = btnSaveRA.Visible;
        btnNotifyRA.Visible = MOCStage <= 2 && MOCStage >= 1 && lblAssessmentOn.Text.Trim() != "";
        //========================
        btnApprove.Visible = (auth.IsVerify || auth.IsVerify2) && MOCStage == 2;
        btnNotifyApprove.Visible = (auth.IsVerify || auth.IsVerify2) && MOCStage <= 3 && MOCStage >= 2 && lblApprovedOn.Text.Trim() != "";
        //========================
        
        btnSaveReview.Visible = (auth.IsVerify || auth.IsVerify2) && MOCStage == 3;

        if (btnSaveReview.Visible)
        {
            if (txtReviewDate.Text.Trim() == "")
                btnSaveReview.Visible = false;
            else
            {
                DateTime dtR = Convert.ToDateTime(txtReviewDate.Text);
                if (DateTime.Today.AddDays(7) >= dtR)
                    btnSaveReview.Visible = true;
                else
                    btnSaveReview.Visible = false;
            }
        }

        btnNotifyReview.Visible = (auth.IsVerify || auth.IsVerify2) && MOCStage <= 4 && MOCStage >= 3 && lblReviewedOn.Text.Trim() != "";
        //========================
        //btnSaveEndorsement.Visible = (auth.IsVerify || auth.IsVerify2) && MOCStage == 4;
        btnSaveEndorsement.Visible = (auth.IsVerify || auth.IsVerify2) && MOCStage == 4;
        if (MOCStatus=="C")
        {
            btnSaveEndorsement.Visible = false;
            ((HtmlControl)this.FindControl("dv_4")).Attributes.Add("class", "wizardboxdone");
        }
        //========================

        ((HtmlControl)this.FindControl("dv_" + Common.CastAsInt32(txtTanel.Text))).Attributes.Add("class", "wizardboxactive");         
    }
    private void SendeMail(int NewStatus,string Group, int RP)
    { 
        List<string> ToAddress = new List<string>();
        List<string> CCAddress = new List<string>();       
        
        string err = "";

        if (Group.Trim() != "" && RP == 0)
        {
            string SQL = "SELECT DISTINCT EMAIL FROM ( " +
                         "SELECT Email From ShipSoft_Admin.[dbo].[UserMaster] WHERE LoginId IN ( " +
                         "SELECT UserId FROM ShipSoft_Admin.[dbo].[UserPageRelation] WHERE PageId = 314 AND (IsVerify=1 OR IsVerify2 = 1 ) " +
                         "UNION " +
                         "SELECT LoginId From ShipSoft_Admin.[dbo].[UserMaster] WHERE ROLEID IN( " +
                         "SELECT RoleId FROM ShipSoft_Admin.[dbo].[RolePageRelation] WHERE PageId = 314 AND (IsVerify=1 OR IsVerify2 = 1 )))" + 
                         ") A";

            DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ToAddress.Add(dt.Rows[i]["Email"].ToString());
                }
            }
        }
        if (Group.Trim() == "" && RP != 0)
        {
            //DataTable dtEmail = Common.Execute_Procedures_Select_ByQuery("SELECT Email From ShipSoft_Admin.[dbo].[UserMaster] WHERE LoginId =" + ddlresponsibleperson.SelectedValue.Trim());
            //ToAddress.Add(dtEmail.Rows[0]["Email"].ToString());
        }

        CCAddress.Add(ProjectCommon.getUserEmailByID(UserId.ToString()));

        string UserName=ProjectCommon.getUserNameByID(UserId.ToString());
        string UserPosition=ProjectCommon.getUserPositionByID(UserId.ToString());
        string[] Noaddress = { };
        string subject =lblMOCNO.Text;;
        string msg = "";
        switch(NewStatus)
        {
            case 1:
                msg = "<br>Topic : " + txtTopic.Text + "<br><br>Following MOC has been requested. Please approve to continue.<br><br><br><br><b>" + UserName + "</b><br>" + UserPosition;
                break;
            //case 2:
            //    msg = "<br>Topic : " + txtTopic.Text + "<br><br>Approval Status : " + (radChangeyes.Checked ? "Yes" : "No") + "<br><br>(<i> " + txtATCRemarks.Text.Replace("'", "`").Trim() + " </i>)<br><br><br><br><b>" + UserName + "</b><br>" + UserPosition;
            //    break;
            case 2:
                msg = "<br>Topic : " + txtTopic.Text + "<br><br>RA and Scope of work is completed. It is ready for your review and approval.<br><br><br><br><b>" + UserName + "</b><br>" + UserPosition;
                break;
            case 3:
                msg = "<br>Topic : " + txtTopic.Text + "<br><br>Approval Status : " + (radYes.Checked ? "Yes" : "No") + "<br><br>(<i> " + txtApproverComments.Text.Trim().Replace("'", "`") + " </i>)<br><br><br><br><b>" + UserName + "</b><br>" + UserPosition;
                break;
            case 4:
                msg = "<br>Topic : " + txtTopic.Text + "<br><br>Above MOC is due for your review and endorsement on " + txtReviewDate.Text.Trim() + " . <br><br><br><br><b>" + UserName + "</b><br>" + UserPosition;
                break;
            default:
                break;
        }
        
        string FileName = "";
        SendMail.SendeMail("noreply@mtmsm.com", "noreply@mtmsm.com", ToAddress.ToArray(), CCAddress.ToArray(), Noaddress, subject, msg, out err, FileName);
    }
    protected void radR_OnCheckedChanged(object sender, EventArgs e)
    {
        dvRiskNo.Visible = radR.Checked;
        if (radNR.Checked)
        {
            txtRiskRefNum.Text = "";
            hfRiskId.Value = "";
            lnkOpenRisk.Visible = false;
        }
    }
    
}