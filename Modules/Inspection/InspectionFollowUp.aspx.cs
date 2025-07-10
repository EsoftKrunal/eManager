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
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;
using System.IO;

public partial class Transactions_InspectionFollowUp : System.Web.UI.Page
{
    Authority Auth;
    string strflaws = "";
    string strResponsibility = "";
    int imgtemp = 0;
    public int DefTableID
    {
        get
        {
            return Common.CastAsInt32(ViewState["_DefTableID"]);
        }
        set
        {
            ViewState["_DefTableID"] = value;
        }
    }
    public int DocID
    {
        set { ViewState["DocID"] = value; }
        get { return Common.CastAsInt32(ViewState["DocID"]); }
    }
    #region "User Defined Properties"
    public int Inspection_Id
    {
        get
        {
            return int.Parse(ViewState["_Inspection_Id"].ToString());
        }
        set
        {
            ViewState["_Inspection_Id"] = value;
        }
    }
    #endregion

    #region "Page Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------

        lblMs.Text = "";
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1053);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
        this.Form.DefaultButton = this.btnSave.UniqueID.ToString();      
        lblmessage.Text = "";
        if ((Session["loginid"] == null) || (Session["UserName"]==null))
        {
            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "window.parent.parent.location='../Login.aspx'", true);
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
        }
        //if (Session["Mode"].ToString() != "Add")
        //{
            if (Session["Insp_Id"] == null) { Session.Add("PgFlag", 1); Response.Redirect("InspectionSearch.aspx"); }
        //}
        //else { if (Session["Insp_Id"] == null) { Session.Add("NwInspFlag", 2); Response.Redirect("InspectionSearch.aspx"); } }
            HiddenField_InspId.Value = Session["Insp_Id"].ToString();
            if (Session["loginid"] != null)
            {
                ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 7);
                OBJ.Invoke();
                Session["Authority"] = OBJ.Authority;
                Auth = OBJ.Authority;
            }
        //if (Session["Insp_Id"] != null)
        //{
        //    string strInsp_Status = "";
        //    DataTable dt88 = Inspection_Planning.CheckInspectionStatus(int.Parse(Session["Insp_Id"].ToString()));
        //    if (dt88.Rows.Count > 0)
        //    {
        //        strInsp_Status = dt88.Rows[0]["Status"].ToString();
        //    }
        //    if ((strInsp_Status != "Planned") && (strInsp_Status != "Executed") && (strInsp_Status != "Observation") && (strInsp_Status != "Response") && ((strInsp_Status != "Pass FollowUp") && (strInsp_Status != "Failed FollowUp")))
        //    {
        //        btnSave.Enabled = false;
        //    }
        //    else
        //    {
        //        btnSave.Enabled = true;
        //    }
        //    //****Code To Check Inspection Status
        //    string strInspStatus = "";
        //    DataTable dt22 = Inspection_TravelSchedule.SelectInspectionDetailsByInspId(int.Parse(Session["Insp_Id"].ToString()));
        //    if (dt22.Rows.Count > 0)
        //    {
        //        strInspStatus = dt22.Rows[0]["Status"].ToString();
        //    }
        //    if (strInspStatus == "Closed")
        //        btnSave.Enabled = false;
        //    else
        //        btnSave.Enabled = true;
        //}
            Inspection_Id = int.Parse(Session["Insp_Id"].ToString());
        if (!Page.IsPostBack)
        {
            try
            {
                Alerts.HANDLE_AUTHORITY(8, null, btnSave, null, btn_Print, Auth);
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
            }
            //******Accessing UserOnBehalf/Subordinate Right******
            try
            {
                if (Session["Insp_Id"] != null)
                {
                    int useronbehalfauth = Alerts.UserOnBehalfRight(Convert.ToInt32(Session["loginid"].ToString()), Convert.ToInt32(Session["Insp_Id"].ToString()));
                    if (useronbehalfauth <= 0)
                    {
                        btnSave.Enabled = false;
                    }
                    else
                    {
                        btnSave.Enabled = true;
                    }
                }
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
            }
            //****************************************************
            if (Session["Insp_Id"] != null)
            {
                try
                {
                    if (Session["DueMode"] != null)
                    {
                        if (Session["DueMode"].ToString() == "ShowFull")
                        {
                            //Inspection_Id = int.Parse(Session["Insp_Id"].ToString());
                            DataTable Dt = Inspection_FollowUp.InspectionObservation(Inspection_Id, "", "", "", "", 0, "", "CLOSED", "", 0, 0, 0);
                            if (Dt.Rows.Count > 0)
                            {
                                if (Dt.Rows[0][0].ToString() == "0")
                                {
                                    BindListofObservations(Inspection_Id);
                                    lblopen.Text = lst_Observation.Items.Count.ToString();
                                }
                                else
                                    lblopen.Text = Dt.Rows[0][0].ToString();
                            }
                        }
                    }
                    //lblopen.Text = Dt.Rows[0][0].ToString();    
                    ViewState["intObsId"] = "";
                    Show_Header_Record1(Inspection_Id);
                    if (Session["DueMode"] != null)
                    {
                        if (Session["DueMode"].ToString() == "ShowFull")
                        {
                            BindListofObservations(Inspection_Id);
                        }
                    }
                    else
                    {
                        BindListofObservations(Inspection_Id);
                    }
                    if (lst_Observation.Items.Count > 0)
                    {
                        lst_Observation.Items[0].Selected = true;
                        lst_Observation_SelectedIndexChanged(sender, e);
                    }
                    if (Session["DueMode"] != null)
                    {
                        if (Session["DueMode"].ToString() == "ShowId")
                        {
                            btnSave.Enabled = true;
                            btnSave.Enabled = Auth.isEdit;
                            btn_Print.Enabled = Auth.isPrint;
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblmessage.Text = ex.StackTrace.ToString();
                }
            }
            //***Check whether Inspection is On Hold or not*******
            DataTable dtchk = Inspection_TravelSchedule.CheckInspectionOnHold(Convert.ToInt32(Session["Insp_Id"].ToString()));
            if (dtchk.Rows.Count > 0)
            {
                if (dtchk.Rows[0][0].ToString() == "Y")
                {
                    btnSave.Enabled = false;
                    btnNotify.Enabled = false;
                }
            }
            //****************************************************
        }
        ScriptManager.GetCurrent(this).RegisterPostBackControl(btnSave);

        DataTable Dt_insgrp = Common.Execute_Procedures_Select_ByQuery("select (select inspectiongroup from dbo.m_Inspection where id=inspectionid) from dbo.t_inspectiondue where id=" + Session["Insp_Id"].ToString());
        if (Common.CastAsInt32(Dt_insgrp.Rows[0][0]) == 3) // mtm inspections
        {
            tr_Normal.Visible = false;
        
            tr_MTM.Visible = true;

            Show_Detail_Record_MTM(Inspection_Id);

        }
        else
        {
            tr_Normal.Visible = true;

            tr_MTM.Visible = false;
        }
     }
    #endregion
    
    #region "User Defined Functions"
    public void BindListofObservations(int intInspDueId)
    {
        try
        {
            string strInspType = "";
            DataTable dtLstObs = new DataTable();
            if (Session["Insp_Id"] != null)
            {
                DataTable dt56 = Inspection_Observation.CheckInspType(int.Parse(Session["Insp_Id"].ToString()));
                if (dt56.Rows.Count > 0)
                {
                    strInspType = dt56.Rows[0]["InspectionType"].ToString();
                }
                if (strInspType == "Internal")
                {
                    try
                    {
                        dtLstObs = Inspection_Response.ResponseDetails(intInspDueId, 0, "", "", 0, 0, 0, 0, 0, "", "", "GETOBS", "", "", "", "");
                        if (dtLstObs.Rows.Count > 0)
                        {
                            lst_Observation.DataTextField = "Observ";
                            lst_Observation.DataValueField = "Id";
                            lst_Observation.DataSource = dtLstObs;
                            lst_Observation.DataBind();
                            lbl_TotalDef.Text = lst_Observation.Items.Count.ToString();
                        }
                        else { lbl_TotalDef.Text = "0"; }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    try
                    {
                        dtLstObs = Inspection_FollowUp.GetFollowUpItems(intInspDueId);
                        if (dtLstObs.Rows.Count > 0)
                        {
                            lst_Observation.DataTextField = "Observ";
                            lst_Observation.DataValueField = "Id";
                            lst_Observation.DataSource = dtLstObs;
                            lst_Observation.DataBind();
                            lbl_TotalDef.Text = lst_Observation.Items.Count.ToString();
                        }
                        else { lbl_TotalDef.Text = "0"; }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
        catch (Exception ex) { throw ex; }
    }
    protected void Show_Header_Record1(int intInspectionId)
    {
        try
        {
            DataTable dt1 = new DataTable();
            dt1 = Inspection_TravelSchedule.SelectInspectionDetailsByInspId(intInspectionId);
            foreach (DataRow dr in dt1.Rows)
            {
                txtinspno.Text = dr["InspectionNo"].ToString();
                txtvessel.Text = dr["VesselName"].ToString();
                txtinspector.Text = dr["InspectorName"].ToString();
                //txtplanneddate.Text = dr["Plan_Date"].ToString();
                // txtplannedport.Text = dr["Planned_Port"].ToString();
                txtmaster.Text = dr["MasterName"].ToString();
                txtchiefofficer.Text = dr["ChiefOfficerName"].ToString();
                txtsecofficer.Text = dr["SecondOfficeName"].ToString();
                txtchiefengg.Text = dr["ChiefEngineerName"].ToString();
                txtfirstassistant.Text = dr["FirstAssistantEnggName"].ToString();
                DataTable dt3 = Inspection_Planning.AddInspectors(0, int.Parse(Session["Insp_Id"].ToString()), 0, 0, DateTime.Now, 0, 0, DateTime.Now, "", 0, 0, "CHKATT");
                if (dt3.Rows.Count > 0)
                {
                    if (dt3.Rows[0][0].ToString() == "NO")
                    {
                        txtmtmsupt.Text = "";
                    }
                    else
                    {
                        txtmtmsupt.Text = dr["Supt"].ToString();
                    }
                }
                //txtmtmsupt.Text = dr["Supt"].ToString();
                txtdonedt.Text = dr["DoneDt"].ToString();
                txtportdone.Text = dr["PortDone"].ToString();
                //  txtresponseduedt.Text = dr["ResponseDueDate"].ToString();
                txtinspname.Text = dr["Name"].ToString();
                txt_Status.Text = dr["Status"].ToString();
                //txtinspector.Text=dr["Inspector"].ToString();
                ///Master = int.Parse(dr["Master"].ToString());
                // ChiefEngineer = int.Parse(dr["ChiefEngineer"].ToString());
                // AssistantEngineer = int.Parse(dr["AssistantEngineer"].ToString());
                // ChiefOfficer = int.Parse(dr["ChiefOfficer"].ToString());
                // SecondOffice = int.Parse(dr["SecondOffice"].ToString());
                // Inspector = dr["Inspector"].ToString();
                //txtCreatedBy_TravelShd.Text = dr["Created_By"].ToString();
                //txtCreatedOn_TravelShd.Text = dr["Created_On"].ToString();
                //txtModifiedBy_TravelShd.Text = dr["Modified_By"].ToString();
                //txtModifiedOn_TravelShd.Text = dr["Modified_On"].ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void RefreshControls()
    {
        rdbflaws.SelectedIndex = -1;
        txtcorrective.Text = "";
        txttargetclosedt.Text = "";
        rdbclosed.SelectedIndex = -1;
        txtcloseddt.Text = "";
        txtremark.Text = "";
    }
    #endregion
    #region "Events"
    public bool chk_FileExtension(string str)
    {
        string extension = str;
        switch (extension)
        {
            case ".doc":
                return true;
            case ".docx":
                return true;
            case ".xls":
                return true;
            case ".xlsx":
                return true;
            case ".pdf":
                return true;
            default:
                return false;
                break;
        }
    }
    protected void lst_Observation_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int ObsId = int.Parse(lst_Observation.SelectedValue);
            ViewState["intObsId"] = ObsId;
            DataTable Dt = new DataTable();
            Dt = Inspection_FollowUp.InspectionObservation(ObsId, "", "", "", "", 0, "", "SELECT", "", 0, 0, 0);
            if (Dt.Rows.Count > 0)
            {

                DataTable dt1 = Budget.getTable("SELECT * FROM t_Observations WHERE Id=" + ObsId.ToString()).Tables[0];
                if (dt1.Rows.Count > 0)
                {
                    a_file.Visible = (dt1.Rows[0]["F_ClosureFile"].ToString().Trim() != "");
                    a_file.HRef = "..\\EMANAGERBLOB\\Inspection\\FollowUpClosure\\" + dt1.Rows[0]["F_ClosureFile"].ToString().Trim();
                }
                else
                {
                    a_file.Visible = false;
                }


                txtremark.Text = Dt.Rows[0]["Remarks"].ToString();
                txt_Observation.Text = Dt.Rows[0]["Deficiency"].ToString();
                if (Dt.Rows[0]["TargetCloseDt"].ToString() != "")
                    //txttargetclosedt.Text = DateTime.Parse(Dt.Rows[0]["TargetCloseDt"].ToString()).ToString("MM/dd/yyyy");
                    txttargetclosedt.Text = Dt.Rows[0]["TargetCloseDt"].ToString();
                else
                    txttargetclosedt.Text = "";
                txtcorrective.Text = Dt.Rows[0]["CorrectiveActions"].ToString();
                if (Dt.Rows[0]["TargetCloseDt"].ToString() != "")
                    //txtcloseddt.Text = DateTime.Parse(Dt.Rows[0]["ClosedDate"].ToString()).ToString("MM/dd/yyyy");
                    txtcloseddt.Text = Dt.Rows[0]["ClosedDate"].ToString();
                else
                    txtcloseddt.Text = "";
                if (Dt.Rows[0]["Flaws"].ToString() != "")
                {
                    //rdbflaws.SelectedValue = Dt.Rows[0]["Flaws"].ToString();
                    char[] c = { ',' };
                    Array a = Dt.Rows[0]["Flaws"].ToString().Split(c);
                    for (int j = 0; j <= rdbflaws.Items.Count - 1; j++)
                    {
                        rdbflaws.Items[j].Selected = false;
                    }
                    for (int r = 0; r < a.Length; r++)
                    {
                        if (a.GetValue(r).ToString() == "People")
                            rdbflaws.Items[0].Selected = true;
                        if (a.GetValue(r).ToString() == "Process")
                            rdbflaws.Items[1].Selected = true;
                        if (a.GetValue(r).ToString() == "Technology" || a.GetValue(r).ToString() == "Equipment")
                            rdbflaws.Items[2].Selected = true;
                    }
                }
                else
                    rdbflaws.SelectedIndex = -1;
                if (Dt.Rows[0]["Closed"].ToString() != "")
                {
                    if (Dt.Rows[0]["Closed"].ToString() == "False")
                    {
                        rdbclosed.SelectedValue = "0";
                        btnSave.Enabled = true;
                    }
                    else
                    {
                        rdbclosed.SelectedValue = "1";
                        btnSave.Enabled = false;
                    }
                }
                else
                {
                    rdbclosed.SelectedIndex = -1;
                    btnSave.Enabled = true;
                }
                if (Dt.Rows[0]["Responsibilty"].ToString() != "")
                {
                    char[] resp = { ',' };
                    Array rs = Dt.Rows[0]["Responsibilty"].ToString().Split(resp);
                    for (int l = 0; l < chklst_Responsibility.Items.Count; l++)
                    {
                        chklst_Responsibility.Items[l].Selected = false;
                    }
                    for (int m = 0; m < rs.Length; m++)
                    {
                        if (rs.GetValue(m).ToString() == "Vessel")
                            chklst_Responsibility.Items[0].Selected = true;
                        if (rs.GetValue(m).ToString() == "Office")
                            chklst_Responsibility.Items[1].Selected = true;
                    }
                }
                else
                    chklst_Responsibility.SelectedIndex = -1;
               
            }
            DataTable Dt11 = Inspection_FollowUp.InspectionObservation(Convert.ToInt32(ViewState["intObsId"].ToString()), "", "", "", "", 0, "", "SELECT", "", 0, 0, 0);
            if (Dt11.Rows.Count > 0)
            {
                //txtCreatedBy_DocumentType.Text = Dt11.Rows[0]["Created_By"].ToString();
                //txtCreatedOn_DocumentType.Text = Dt11.Rows[0]["Created_On"].ToString();
                txtCreatedBy_DocumentType.Text = Dt11.Rows[0]["F_CreatedBy"].ToString();
                txtCreatedOn_DocumentType.Text = Dt11.Rows[0]["F_CreatedOn"].ToString();
                //txtModifiedBy_DocumentType.Text = Dt11.Rows[0]["Modified_By"].ToString();
                //txtModifiedOn_DocumentType.Text = Dt11.Rows[0]["Modified_On"].ToString();
                txtModifiedBy_DocumentType.Text = Dt11.Rows[0]["F_Modified_By"].ToString();
                txtModifiedOn_DocumentType.Text = Dt11.Rows[0]["F_Modified_On"].ToString();
            }
        }
        catch(Exception ex)
        {
            lblmessage.Text = ex.StackTrace.ToString();    
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Session["Insp_Id"] == null) { lblmessage.Text = "Please save Planning first."; return; }
        try
        {
            if (ViewState["intObsId"].ToString() != "")
            {
                if (rdbflaws.SelectedValue == "")
                {
                    lblmessage.Text = "Please select Cause.";
                    return;
                }
                for (int cnt = 0; cnt <= rdbflaws.Items.Count - 1; cnt++)
                {
                    if (rdbflaws.Items[cnt].Selected == true)
                    {
                        if (strflaws == "")
                        {
                            strflaws = rdbflaws.Items[cnt].Value;
                        }
                        else
                        {
                            strflaws = strflaws + "," + rdbflaws.Items[cnt].Value;
                        }
                    }
                }
                if (txtcorrective.Text.Trim() == "")
                {
                    lblmessage.Text = "Please enter Corrective Actions.";
                    return;
                }
                if ((txttargetclosedt.Text != "") && (txtdonedt.Text != ""))
                {
                    if (DateTime.Parse(txttargetclosedt.Text) < DateTime.Parse(txtdonedt.Text))
                    {
                        lblmessage.Text = "Target Closed Date cannot be less than Inspection Done Date.";
                        return;
                    }
                }
                if (rdbclosed.SelectedValue == "")
                {
                    lblmessage.Text = "Please select Closed.";
                    return;
                }
                if (rdbclosed.SelectedValue == "1")
                {
                    if (txtcloseddt.Text == "")
                    {
                        lblmessage.Text = "Please enter Closed Date.";
                        return;
                    }
                }
                if ((txtcloseddt.Text != "") && (txtdonedt.Text != ""))
                {
                    if (DateTime.Parse(txtcloseddt.Text) < DateTime.Parse(txtdonedt.Text))
                    {
                        lblmessage.Text = "Closed Date cannot be less than Inspection Done Date.";
                        return;
                    }
                }
                if (rdbclosed.SelectedValue == "1")
                {
                    if (txtremark.Text.Trim() == "")
                    {
                        lblmessage.Text = "Please enter Remarks.";
                        return;
                    }
                }
               
                for (int k = 0; k < chklst_Responsibility.Items.Count; k++)
                {
                    if (chklst_Responsibility.Items[k].Selected == true)
                    {
                        if (strResponsibility == "")
                        {
                            strResponsibility = chklst_Responsibility.Items[k].Value;
                        }
                        else
                        {
                            strResponsibility = strResponsibility + "," + chklst_Responsibility.Items[k].Value;
                        }
                    }
                }

                //----------------------------------------------------
                string FileName = "";

                if(flp_COCUpload.PostedFile != null && flp_COCUpload.FileContent.Length > 0)
                {
                    string strfilename = flp_COCUpload.FileName;
                    HttpPostedFile file1 = flp_COCUpload.PostedFile;
                    UtilityManager um = new UtilityManager();
                    if (chk_FileExtension(Path.GetExtension(flp_COCUpload.FileName).ToLower()) == true)
                    {
                        string strDoc = "EMANAGERBLOB/Inspection/FollowUpClosure/" + flp_COCUpload.FileName.Trim();
                        FileName = um.UploadFileToServer(file1, strfilename, "", "FollowUpClosure");
                        if (FileName.StartsWith("?"))
                        {
                            lblmessage.Text = FileName.Substring(1);
                            imgtemp = 1;
                            return;
                        }
                    }
                    else
                    {
                        lblmessage.Text = "Invalid File Type. (Valid Files Are .doc, .docx, .xls, .xlsx, .pdf)";
                        flp_COCUpload.Focus();
                        imgtemp = 1;
                        return;
                    }
                }
                if (imgtemp != 1)
                {
                    DataTable dt = Inspection_FollowUp.InspectionObservation(int.Parse(ViewState["intObsId"].ToString()), txtcorrective.Text, strflaws, txtremark.Text, txttargetclosedt.Text, int.Parse(rdbclosed.SelectedValue), txtcloseddt.Text, "MODIFY", strResponsibility, int.Parse(Session["loginid"].ToString()), int.Parse(Session["loginid"].ToString()), 0);
                    Budget.getTable("UPDATE t_Observations SET F_ClosureFile='" + FileName + "' WHERE Id=" + ViewState["intObsId"].ToString());

                    lblmessage.Text = "FollowUp Updated Sucessfully.";
                    lblopen.Text = dt.Rows[0][0].ToString();
                    btnSave.Enabled = false;
                    btnNotify.Enabled = true;
                }
            }
            else
            {
                lblmessage.Text = "Select an Observation.";
            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = ex.StackTrace.ToString();
        }
    }



    //PopUp Documents
    protected void btnColsureEvidence_OnClick(object sender, EventArgs e)
    {
        if (lst_Observation.Items.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "msg", "alert('No deficiency available.')", true);
            return;
         }
        DivClosureDocs.Visible = true;
        BindDocs();
    }

    protected void btnCloseDocuments_OnClick(object sender, EventArgs e)
    {
        DivClosureDocs.Visible = false;
    }
    protected void btnSaveDoc_Click(object sender, EventArgs e)
    {
        
        int ObsId = Common.CastAsInt32(ViewState["intObsId"]);
        //if (CaseID == 0)
        //{
        //    lblMessage.Text = "Please save the case first.";
        //    return;
        //}
        if (DocID == 0)
            if (!flAttachDocs.HasFile)
            {
                lblMsgDoc.Text = "Please select a file.";
                flAttachDocs.Focus();
                return;
            }
        FileUpload img = (FileUpload)flAttachDocs;
        Byte[] imgByte = new Byte[0];
        string FileName = "";

        if (img.HasFile && img.PostedFile != null)
        {
            HttpPostedFile File = flAttachDocs.PostedFile;

            if (Path.GetExtension(File.FileName).ToString() == ".pdf" || Path.GetExtension(File.FileName).ToString() == ".txt" || Path.GetExtension(File.FileName).ToString() == ".doc" || Path.GetExtension(File.FileName).ToString() == ".docx" || Path.GetExtension(File.FileName).ToString() == ".xls" || Path.GetExtension(File.FileName).ToString() == ".xlsx" || Path.GetExtension(File.FileName).ToString() == ".ppt" || Path.GetExtension(File.FileName).ToString() == ".pptx")
            {
                FileName = "Observation" + "_" + DateTime.Now.ToString("dd-MMM-yyyy") + "_" + DateTime.Now.TimeOfDay.ToString().Replace(":", "-").ToString() + Path.GetExtension(File.FileName);


                string path = "../EMANAGERBLOB/Inspection/Observation/";
                flAttachDocs.SaveAs(Server.MapPath(path) + FileName);
            }
            else
            {
                lblMsgDoc.Text = "File type not supported. Only pdf and microsoft ofiice files accepted.";
            }


        }
        string strSQL = "EXEC sp_InsertUpdatet_FollowUP " + DocID + "," + ObsId + ",'" + txt_Desc.Text.Trim().Replace("'", "`") + "','" + FileName + "','" + Convert.ToInt32(Session["loginid"].ToString()) + "'";
        DataTable dtDocs = Budget.getTable(strSQL).Tables[0];
        if (dtDocs.Rows.Count > 0)
        {
            lblMsgDoc.Text = "Record Successfully Saved.";
            txt_Desc.Text = "";
            BindDocs();
            DocID = 0;
        }
    }
    protected void imgEditDoc_OnClick(object sender, EventArgs e)
    {
        ImageButton ImgEdit = (ImageButton)sender;
        HiddenField hfDocID = (HiddenField)ImgEdit.Parent.FindControl("hfDocID");
        DocID = Common.CastAsInt32(hfDocID.Value);
        Label lblDesc = (Label)ImgEdit.Parent.FindControl("lblDesc");
        txt_Desc.Text = lblDesc.Text;

    }
    protected void imgDelDoc_OnClick(object sender, EventArgs e)
    {
        ImageButton ImgDel = (ImageButton)sender;
        HiddenField hfDocID = (HiddenField)ImgDel.Parent.FindControl("hfDocID");
        DocID = Common.CastAsInt32(hfDocID.Value);

        string sql = "delete from t_FollowUP  where DocID=" + hfDocID.Value + "";
        DataSet dtGroups = Budget.getTable(sql);
        BindDocs();
        DocID = 0;
    }
    public void BindDocs()
    {
        int intObsId = Common.CastAsInt32(ViewState["intObsId"]);
        string strSQL = "select replace(convert(varchar,UploadedOn ,106) ,' ','-')UploadedDate,(select Firstname+' '+Lastname from dbo.UserLogin UL where UL.LoginID= CS.UploadedBy)UploadedBy,* from t_FollowUP CS where DeficiencyID=" + intObsId;
        DataTable dtDocs = Budget.getTable(strSQL).Tables[0];
        if (dtDocs.Rows.Count > 0)
        {
            rptDocs.DataSource = dtDocs;
            rptDocs.DataBind();
        }
        else
        {
            rptDocs.DataSource = null;
            rptDocs.DataBind();
        }
    }
    
    

    #endregion
    protected void btnNotify_Click(object sender, EventArgs e)
    {
        string strInspNum = "", strQuestNum = "", strDeficiency = "", strCorrectiveActions = "", strResponsibility = "", strTargetCloseDt = "";
        try
        {
            DataTable dt1 = Inspection_FollowUp.GetFollowUpMailDetails(int.Parse(Session["Insp_Id"].ToString()), int.Parse(ViewState["intObsId"].ToString()));
            if (dt1.Rows.Count > 0)
            {
                strInspNum = dt1.Rows[0]["InspectionNo"].ToString();
                strQuestNum = dt1.Rows[0]["QuestionNo"].ToString();
                strDeficiency = dt1.Rows[0]["Deficiency"].ToString();
                strCorrectiveActions = dt1.Rows[0]["CorrectiveActions"].ToString();
                strResponsibility = dt1.Rows[0]["Responsibilty"].ToString();
                strTargetCloseDt = dt1.Rows[0]["TargetCloseDate"].ToString();
            }
        }
        catch { }
        try
        {
            SendMail.FollowUpMail("FollowUp", strInspNum, strQuestNum, strDeficiency, strCorrectiveActions, strResponsibility, strTargetCloseDt);
            lblmessage.Text = "Mail Send Successfully.";
        }
        catch { }
    }
    protected void Show_Detail_Record_MTM(int InspectionId)
    {
        string SQL = "Select Row_Number() over(order by Deficiency) as SrNo, TableId,Deficiency,CorrActions,TCLDate,Closure=Case When Closure=1 then 'Yes' else 'No' end,ClosedBy,ClosedOn,ClosureRemarks from t_observationsnew Where InspDue_Id=" + InspectionId.ToString();
        rptList.DataSource = Budget.getTable(SQL);
        rptList.DataBind();
    }
    protected void btnClosurePopup_OnClick(object sender, EventArgs e)
    {
        LinkButton imgEdit = (LinkButton)sender;
        DefTableID = Common.CastAsInt32(imgEdit.CommandArgument);
        dvClosure.Visible = true;

        btnSaveClosure.Visible = true;
        txt_ClosedBy.Text = "";
        txt_ClosedDate.Text = "";
        txt_ClosedOn.Text = "";
        txt_ClosedRemarks.Text = "";
        for (int j = 0; j <= rdbflaws.Items.Count - 1; j++)
        {
            rdbflaws.Items[j].Selected = false;
        }
        a_file.HRef = "";

        string sql = "Select Deficiency,CorrActions,TCLDate,Responsibility,(select FirstName + ' ' + LastName from dbo.userlogin where loginid=t_ObservationsNew.CreatedBy) as CreatedBy,(select FirstName + ' ' + LastName from dbo.userlogin where loginid=t_ObservationsNew.ModifiedBy) as ModifiedBy,CreatedOn,ModifiedOn from dbo.t_ObservationsNew Where TableID=" + DefTableID.ToString() + "";
        DataTable DS = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DS != null)
        {
            DataRow Dr = DS.Rows[0];
            txtDeficiency.Text = Dr["Deficiency"].ToString();
            txtCorrAction.Text = Dr["CorrActions"].ToString();
            txtTCD.Text = Common.ToDateString(Dr["TCLDate"].ToString());

            if (Dr["Responsibility"].ToString() != "")
            {
                char[] resp = { ',' };
                Array rs = Dr["Responsibility"].ToString().Split(resp);
                for (int l = 0; l < chklst_Respons.Items.Count; l++)
                {
                    chklst_Respons.Items[l].Selected = false;
                }
                for (int m = 0; m < rs.Length; m++)
                {
                    if (rs.GetValue(m).ToString() == "Vessel")
                        chklst_Respons.Items[0].Selected = true;
                    if (rs.GetValue(m).ToString() == "Office")
                        chklst_Respons.Items[1].Selected = true;
                }
            }
            else
                chklst_Respons.SelectedIndex = -1;

            txtACreatedBy.Text = DS.Rows[0]["CreatedBy"].ToString();
            txtACreatedOn.Text = Common.ToDateString(DS.Rows[0]["CreatedOn"]);
            txtAModifiedBy.Text = DS.Rows[0]["ModifiedBy"].ToString();
            txtAModifiedOn.Text = Common.ToDateString(DS.Rows[0]["ModifiedOn"]);
        }
        rdbflaws_C.SelectedIndex = -1;
    }
    protected void btnViewClosure_OnClick(object sender, EventArgs e)
    {
        LinkButton imgEdit = (LinkButton)sender;
        DefTableID = Common.CastAsInt32(imgEdit.CommandArgument);
        dvClosure.Visible = true;

        btnSaveClosure.Visible = false;
        string sql = "Select Closure,ClosedBy,ClosedOn,ClosureRemarks,Flaws,FileType,(select FirstName + ' ' + LastName from dbo.userlogin where loginid=t_ObservationsNew.CreatedBy) as CreatedBy,(select FirstName + ' ' + LastName from dbo.userlogin where loginid=t_ObservationsNew.ModifiedBy) as ModifiedBy,CreatedOn,ModifiedOn from dbo.t_ObservationsNew Where TableID=" + DefTableID.ToString() + "";
        DataTable DS = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DS != null)
        {
            txtACreatedBy.Text = DS.Rows[0]["CreatedBy"].ToString();
            txtACreatedOn.Text = Common.ToDateString(DS.Rows[0]["CreatedOn"]);
            txtAModifiedBy.Text = DS.Rows[0]["ModifiedBy"].ToString();
            txtAModifiedOn.Text = Common.ToDateString(DS.Rows[0]["ModifiedOn"]);

            txt_ClosedDate.Text = Common.ToDateString(DS.Rows[0]["ClosedOn"].ToString());
            txt_ClosedRemarks.Text = DS.Rows[0]["ClosureRemarks"].ToString().Replace("''", "'");
            txt_ClosedBy.Text = DS.Rows[0]["ClosedBy"].ToString();
            txt_ClosedOn.Text = Common.ToDateString(DS.Rows[0]["ClosedOn"].ToString());
            if (DS.Rows[0]["FileType"].ToString().Trim() != "")
                a_file.HRef = "~/EMANAGERBLOB/Inspection/DeficiencyClosure_" + DefTableID + DS.Rows[0]["FileType"].ToString();

            if (DS.Rows[0]["Flaws"].ToString() != "")
            {
                char[] c = { ',' };
                Array a = DS.Rows[0]["Flaws"].ToString().Split(c);
                for (int j = 0; j <= rdbflaws_C.Items.Count - 1; j++)
                {
                    rdbflaws_C.Items[j].Selected = false;
                }
                for (int r = 0; r < a.Length; r++)
                {
                    if (a.GetValue(r).ToString() == "People")
                        rdbflaws_C.Items[0].Selected = true;
                    if (a.GetValue(r).ToString() == "Process")
                        rdbflaws_C.Items[1].Selected = true;
                    if (a.GetValue(r).ToString() == "Equipment")
                        rdbflaws_C.Items[2].Selected = true;
                }
            }
            else
                rdbflaws_C.SelectedIndex = -1;
        }


        string sql1 = "Select Deficiency,CorrActions,TCLDate,Responsibility,(select FirstName + ' ' + LastName from dbo.userlogin where loginid=t_ObservationsNew.CreatedBy) as CreatedBy,(select FirstName + ' ' + LastName from dbo.userlogin where loginid=t_ObservationsNew.ModifiedBy) as ModifiedBy,CreatedOn,ModifiedOn from dbo.t_ObservationsNew Where TableID=" + DefTableID.ToString() + "";
        DS = Common.Execute_Procedures_Select_ByQuery(sql1);
        if (DS != null)
        {
            DataRow Dr = DS.Rows[0];
            txtDeficiency.Text = Dr["Deficiency"].ToString();
            txtCorrAction.Text = Dr["CorrActions"].ToString();
            txtTCD.Text = Common.ToDateString(Dr["TCLDate"].ToString());

            if (Dr["Responsibility"].ToString() != "")
            {
                char[] resp = { ',' };
                Array rs = Dr["Responsibility"].ToString().Split(resp);
                for (int l = 0; l < chklst_Respons.Items.Count; l++)
                {
                    chklst_Respons.Items[l].Selected = false;
                }
                for (int m = 0; m < rs.Length; m++)
                {
                    if (rs.GetValue(m).ToString() == "Vessel")
                        chklst_Respons.Items[0].Selected = true;
                    if (rs.GetValue(m).ToString() == "Office")
                        chklst_Respons.Items[1].Selected = true;
                }
            }
            else
                chklst_Respons.SelectedIndex = -1;

        }
        rdbflaws_C.SelectedIndex = -1;

        DefTableID = 0;

    }
    protected void btnSaveClosure_Click(object sender, EventArgs e)
    {
        try
        {
            string strflaws = "";
            if (txt_ClosedDate.Text == "")
            {
                lblmessage.Text = "Please enter Closed Date.";
                txt_ClosedDate.Focus();
                return;
            }
            if (txt_ClosedRemarks.Text == "")
            {
                lblmessage.Text = "Please enter Closed Remarks.";
                txt_ClosedRemarks.Focus();
                return;
            }

            for (int cnt = 0; cnt <= rdbflaws_C.Items.Count - 1; cnt++)
            {
                if (rdbflaws_C.Items[cnt].Selected == true)
                {
                    if (strflaws == "")
                    {
                        strflaws = rdbflaws_C.Items[cnt].Value;
                    }
                    else
                    {
                        strflaws = strflaws + "," + rdbflaws_C.Items[cnt].Value;
                    }
                }
            }
            //----------------------------------------------------
            string FileName = "";
            string FileType = "";
            if (fu_ClosureEvidence.PostedFile != null && fu_ClosureEvidence.FileContent.Length > 0)
            {
                HttpPostedFile file1 = fu_ClosureEvidence.PostedFile;
                UtilityManager um = new UtilityManager();
                if (chk_FileExtension(Path.GetExtension(fu_ClosureEvidence.FileName).ToLower()) == true)
                {
                    FileName = "DeficiencyClosure_" + DefTableID.ToString() + Path.GetExtension(fu_ClosureEvidence.FileName).ToLower();
                    FileType = Path.GetExtension(fu_ClosureEvidence.FileName).ToLower();

                    if (File.Exists(Server.MapPath("~/EMANAGERBLOB/Inspection/FollowUpClosure/" + FileName)))
                        File.Delete(Server.MapPath("~/EMANAGERBLOB/Inspection/FollowUpClosure/" + FileName));
                    fu_ClosureEvidence.SaveAs(Server.MapPath("~/EMANAGERBLOB/Inspection/FollowUpClosure/" + FileName));
                }
                else
                {
                    lblmessage.Text = "Invalid File Type. (Valid Files Are .doc, .docx, .xls, .xlsx, .pdf)";
                    fu_ClosureEvidence.Focus();
                    return;
                }
            }
            string sql = "Update dbo.t_ObservationsNew set Closure=1,ClosedBy='" + Session["UserName"].ToString() + "',ClosedOn='" + txt_ClosedDate.Text.Trim() + "',ClosureRemarks='" + txt_ClosedRemarks.Text.Replace("'", "''").Trim() + "',flaws='" + strflaws + "',FileType='" + FileType + "' Where TableID=" + DefTableID.ToString() + "";
            DataTable DS = Common.Execute_Procedures_Select_ByQuery(sql);
            lblMs.Text = " Closed successfully. ";
        }
        catch (Exception ex) { throw ex; }
    }
    protected void btnCloseClosure_Click(object sender, EventArgs e)
    {
        dvClosure.Visible = false;
        Show_Detail_Record_MTM(Inspection_Id);
        DefTableID = 0;
    }

    
}