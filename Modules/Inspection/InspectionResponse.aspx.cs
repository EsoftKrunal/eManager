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
//using System.Data.SqlClient;

public partial class Transactions_InspectionResponse : System.Web.UI.Page
{    /// <summary>
    /// Page Name            : InspectionResponse.aspx
    /// Purpose              : This is the Response page
    /// Author               : Shobhita
    /// Developed on         : 23-Oct-2009
    /// </summary>

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
    
    #region "Declarations"
    int intLogin_Id;
    int intInspection_Id;
    int intObsId = 0;
    string strInspStatus = "";
    string FrstAppBy = "";
    Authority Auth;
    string strInsp_Status = "";
    string strInspType = "";
    #endregion

    #region "User Defined Properties"
    public int FirstApprovedBy
    {
        get
        {
            return int.Parse(ViewState["_FirstAppBy"].ToString());
        }
        set
        {
            ViewState["_FirstAppBy"] = value;
        }
    }
    public int SecondApprovedBy
    {
        get
        {
            return int.Parse(ViewState["_SecondAppBy"].ToString());
        }
        set
        {
            ViewState["_SecondAppBy"] = value;
        }
    }
    #endregion

    #region "PageLoad"

    public int DocID
    {
        set { ViewState["DocID"] = value; }
        get { return Common.CastAsInt32(ViewState["DocID"]); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblMsgDeficiency.Text = "";
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1053);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
        this.Form.DefaultButton = this.btn_Save.UniqueID.ToString();  
        lblmessage.Text = "";
        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
        }
        else
        {
            intLogin_Id = Convert.ToInt32(Session["loginid"].ToString());
        }
        if (Session["Insp_Id"] == null) { Session.Add("PgFlag", 1); Response.Redirect("InspectionSearch.aspx"); }
        try { intInspection_Id = int.Parse(Session["Insp_Id"].ToString()); } catch { }
        HiddenField_InspId.Value = Session["Insp_Id"].ToString();
        if (Session["loginid"] != null)
        {
            ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 7);
            OBJ.Invoke();
            Session["Authority"] = OBJ.Authority;
            Auth = OBJ.Authority;
        }
        if (!Page.IsPostBack)
        {
            try
            {
                Alerts.HANDLE_AUTHORITY(10, btn_Save, btn_Edit, btn_Delete, btn_Print, Auth);
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
            }
            if (Session["Insp_Id"] != null)
            {
                DataTable dt22 = Inspection_TravelSchedule.SelectInspectionDetailsByInspId(intInspection_Id);
                if (dt22.Rows.Count > 0)
                {
                    ViewState["VersionId"] = dt22.Rows[0]["VersionId"].ToString();
                    strInspStatus = dt22.Rows[0]["Status"].ToString();

                    DateTime dd=new DateTime();
                    if(!(DateTime.TryParse(dt22.Rows[0]["ActualDate"].ToString(),out dd)))
                    {
                        Response.Redirect("InspectionTravelSchedule.aspx?Message=Please execute the inspection first.",true);
                    }
               }
                if ((strInspStatus == "Due") || (strInspStatus == "Closed"))
                {
                    chk_RespUp.Enabled = false;
                }
                else
                {
                    DataSet ds22 = Inspection_Response.CheckInspectionGroupResponse(int.Parse(Session["Insp_Id"].ToString()));
                    if (ds22.Tables[0].Rows[0]["Col"].ToString().ToUpper() == "YES")
                        chk_RespUp.Enabled = true;
                    else
                        chk_RespUp.Enabled = false;
                }
                if (Session["Insp_Id"] != null)
                {
                    DataTable dt88 = Inspection_Planning.CheckInspectionStatus(int.Parse(Session["Insp_Id"].ToString()));
                    if (dt88.Rows.Count > 0)
                    {
                        strInsp_Status = dt88.Rows[0]["Status"].ToString();
                    }
                    if ((strInsp_Status != "Planned") && (strInsp_Status != "Executed") && (strInsp_Status != "Observation") && (strInsp_Status != "Response"))
                    {
                        btn_Save.Enabled = false;
                        btn_Edit.Enabled = false;
                        btn_Delete.Enabled = false;
                    }
                    else
                    {
                        btn_Edit.Enabled = true;
                        btn_Delete.Enabled = true;
                        btn_Edit.Enabled = Auth.isEdit;
                        btn_Delete.Enabled = Auth.isDelete;
                    }
                }
                //****Displaying Records In Uppermost Readonly Fields 
               // Show_Header_Record(intInspection_Id);
                //***************************************************

                //****Code to check whether inspection is Internal or External
                try
                {
                    if (Session["Insp_Id"] != null)
                    {
                        DataTable dt56 = Inspection_Observation.CheckInspType(int.Parse(Session["Insp_Id"].ToString()));
                        if (dt56.Rows.Count > 0)
                        {
                            strInspType = dt56.Rows[0]["InspectionType"].ToString();
                        }
                        if (strInspType == "Internal")
                        {
                            //lblmessage.Text = "Entry is not allowed as inspection is Internal.";
                            //chk_FollowupItem.Enabled = false;
                            //btn_Save.Enabled = false;
                            //btn_Edit.Enabled = false;
                            //btn_Delete.Enabled = false;
                            //btnNotify.Enabled = false;
                            //return;
                        }
                        else
                        {
                            chk_FollowupItem.Enabled = true;
                            btn_Save.Enabled = true;
                            btn_Edit.Enabled = true;
                            btn_Delete.Enabled = true;
                            btnNotify.Enabled = true;
                        }
                    }
                }
                catch (Exception ex) { throw ex; }
                //************************************************************
                if (Session["DueMode"] != null)
                {
                    if (Session["DueMode"].ToString() == "ShowFull")
                    {
                        //****Binding List Of Observations List
                        BindListofObservations(intInspection_Id);
                        DataTable dt55 = Inspection_Response.ResponseDetails(0, intInspection_Id, "", "", 0, 0, 0, 0, 0, "", "", "INSPDUEID", "", "", "", "");
                        if (dt55.Rows.Count > 0)
                        {
                            if (dt55.Rows[0]["ResponseUploaded"].ToString() == "Y")
                                chk_RespUp.Checked = true;
                            else
                                chk_RespUp.Checked = false;
                        }
                    }
                    if ((strInsp_Status == "Planned") || (strInsp_Status == "Executed") || (strInsp_Status == "Observation") || (strInsp_Status == "Response"))
                    {
                        try
                        {
                            Alerts.HANDLE_AUTHORITY(10, btn_Save, btn_Edit, btn_Delete, btn_Print, Auth);
                            //******Accessing UserOnBehalf/Subordinate Right******
                            try
                            {
                                if (Session["Insp_Id"] != null)
                                {
                                    int useronbehalfauth = Alerts.UserOnBehalfRight(Convert.ToInt32(Session["loginid"].ToString()), Convert.ToInt32(Session["Insp_Id"].ToString()));
                                    if (useronbehalfauth <= 0)
                                    {
                                        btn_Save.Enabled = false;
                                        btn_Edit.Enabled = false;
                                        btn_Delete.Enabled = false;
                                    }
                                    else
                                    {
                                        if ((strInspStatus != "Closed") && (strInspStatus != "Due"))
                                        {
                                            btn_Save.Enabled = true;
                                            btn_Edit.Enabled = true;
                                            btn_Delete.Enabled = true;
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
                            }
                            //****************************************************
                        }
                        catch
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
                        }
                    }
                }
                else
                {
                    BindListofObservations(intInspection_Id);
                    DataTable dt55 = Inspection_Response.ResponseDetails(0, intInspection_Id, "", "", 0, 0, 0, 0, 0, "", "", "INSPDUEID", "", "", "", "");
                    if (dt55.Rows.Count > 0)
                    {
                        if (dt55.Rows[0]["ResponseUploaded"].ToString() == "Y")
                            chk_RespUp.Checked = true;
                        else
                            chk_RespUp.Checked = false;
                    }
                }
                if (lst_Observation.Items.Count > 0)
                {
                    lst_Observation.Items[0].Selected = true;
                    lst_Observation_SelectedIndexChanged(sender, e);
                }
                else
                {
                    lblmessage.Text = "No Observations."; 
                }
                CheckObservations(intInspection_Id);
                if (Session["DueMode"] != null)
                {
                    if (Session["DueMode"].ToString() == "ShowId")
                    {
                        btn_Edit.Enabled = true;
                        btn_Delete.Enabled = true;
                        btn_Edit.Enabled = Auth.isEdit;
                        btn_Delete.Enabled = Auth.isDelete;
                    }
                    if ((strInsp_Status == "Planned") || (strInsp_Status == "Executed") || (strInsp_Status == "Observation") || (strInsp_Status == "Response"))
                    {
                        try
                        {
                            Alerts.HANDLE_AUTHORITY(10, btn_Save, btn_Edit, btn_Delete, btn_Print, Auth);
                            //******Accessing UserOnBehalf/Subordinate Right******
                            try
                            {
                                if (Session["Insp_Id"] != null)
                                {
                                    int useronbehalfauth = Alerts.UserOnBehalfRight(Convert.ToInt32(Session["loginid"].ToString()), Convert.ToInt32(Session["Insp_Id"].ToString()));
                                    if (useronbehalfauth <= 0)
                                    {
                                        btn_Save.Enabled = false;
                                        btn_Edit.Enabled = false;
                                        btn_Delete.Enabled = false;
                                    }
                                    else
                                    {
                                        if ((strInspStatus != "Closed") && (strInspStatus != "Due"))
                                        {
                                            btn_Save.Enabled = true;
                                            btn_Edit.Enabled = true;
                                            btn_Delete.Enabled = true;
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
                            }
                            //****************************************************
                        }
                        catch
                        {
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
                        }
                    }
                }
            }
            DataSet ds23 = Inspection_Response.CheckInspectionGroupResponse(int.Parse(Session["Insp_Id"].ToString()));
            if (ds23.Tables[0].Rows[0]["Col"].ToString().ToUpper() == "YES")
            {
                DataTable dt62 = Inspection_TravelSchedule.SelectInspectionDetailsByInspId(intInspection_Id);
                if (dt62.Rows.Count > 0)
                {
                    strInspStatus = dt62.Rows[0]["Status"].ToString();
                }
                if ((strInspStatus == "Due") || (strInspStatus == "Closed"))
                {
                    chk_RespUp.Enabled = false;
                }
                else
                    chk_RespUp.Enabled = true;
            }
            else
                chk_RespUp.Enabled = false;
            //***Check whether Inspection is On Hold or not*******
            DataTable dtchk = Inspection_TravelSchedule.CheckInspectionOnHold(Convert.ToInt32(Session["Insp_Id"].ToString()));
            if (dtchk.Rows.Count > 0)
            {
                if (dtchk.Rows[0][0].ToString() == "Y")
                {
                    btn_Save.Enabled = false;
                    btn_Edit.Enabled = false;
                    btn_Delete.Enabled = false;
                }
            }
            //****************************************************
            if (lst_Observation.Items.Count > 0)
            {
                lst_Observation.Items[0].Selected = true;
                lst_Observation_SelectedIndexChanged(sender, e);
            }
        }

        // HIDE THE UPDATE OBSERVATION BUTTON IF AT LEASR ONE REPONSE HAS BEEN FILLED
        DataTable DtUpdObs = Common.Execute_Procedures_Select_ByQuery("SELECT COUNT(*) FROM t_Observations where inspectiondueid=" + Session["Insp_Id"].ToString() + " AND LTRIM(RTRIM(RESPONSE))<>''");
        if (Common.CastAsInt32(DtUpdObs.Rows[0][0]) > 0)
        {
            btnUpdateObservation.Enabled = false;
            //btnUpdateCrewList.Enabled = false;
        }
        else
        {
            DtUpdObs = Common.Execute_Procedures_Select_ByQuery("SELECT master FROM t_InspectionDue where id=" + Session["Insp_Id"].ToString());
            btnUpdateObservation.Enabled = true;
            if (DtUpdObs.Rows.Count<0)
            {
                btnUpdateCrewList.Enabled = true;
            }
            else
            {
                if (Common.CastAsInt32(DtUpdObs.Rows[0][0]) > 0)
                {
                    //btnUpdateCrewList.Enabled = false;
                    if (strInspType == "Internal")
                    {
                        btnTechReport.Visible = false;
                    }
                }
                else
                {
                    btnUpdateCrewList.Enabled = true;
                }
            }
        }
        //-----------------------------------
        if ((strInspStatus == "Due") || (strInspStatus == "Closed"))
        {
            btnUpdateCrewList.Enabled = false;
            
            btn_Save.Enabled = false;
            btn_Edit.Enabled = false;
            btn_Delete.Enabled = false;
        }
        //else
        //{
        //    btnUpdateCrewList.Enabled = true;
        //}
        btnUpdateCrewList.Visible = false;
        btnUpdateObservation1.Enabled = btnUpdateObservation.Enabled;
        btnUpdateObservation1.Visible = btnUpdateObservation.Visible;

        DataTable Dt_insgrp= Common.Execute_Procedures_Select_ByQuery("select (select inspectiongroup from dbo.m_Inspection where id=inspectionid) from dbo.t_inspectiondue where id=" + Session["Insp_Id"].ToString());
        if (Common.CastAsInt32(Dt_insgrp.Rows[0][0]) == 3 || Common.CastAsInt32(Dt_insgrp.Rows[0][0]) == 5) // mtm inspections
        {
            tr_Normal.Visible = false;
            tr_Normal1.Visible = false;

            tr_MTM.Visible = true;

            Show_Detail_Record_MTM(intInspection_Id);
            btnUpdateObservation.Visible = false;
            btnUpdateObservation1.Visible = false;
        }
        else
        {
            tr_Normal.Visible = true;
            tr_Normal1.Visible = true;

            tr_MTM.Visible = false;
        }
        
    }
    #endregion

    #region "User Defined Functions"
    protected void Show_Detail_Record_MTM(int InspectionId)
    {
        string SQL = "Select Row_Number() over(order by TableId) as SrNo, TableId,Deficiency,CorrActions,TCLDate,Closure=Case When Closure=1 then 'Yes' else 'No' end,ClosedBy,ClosedOn,ClosureRemarks from t_observationsnew Where InspDue_Id=" + InspectionId.ToString() + " order by TableId";
        rptList.DataSource = Budget.getTable(SQL);
        rptList.DataBind();
    }
    //****Show Records in Readonly Fields
    //protected void Show_Header_Record(int intInspectionId)
    //{
    //    DataTable dt1 = Inspection_TravelSchedule.SelectInspectionDetailsByInspId(intInspectionId);
    //    foreach (DataRow dr in dt1.Rows)
    //    {
    //        txt_InspNo.Text = dr["InspectionNo"].ToString();
    //        txt_VesselName.Text = dr["VesselName"].ToString();
    //        txt_InspName.Text = dr["Name"].ToString();
    //        txt_DoneDate.Text = dr["DoneDt"].ToString();
    //        txt_PortDone.Text = dr["ActualLocation"].ToString();
    //        txt_Master.Text = dr["MasterName"].ToString();
    //        txt_ChiefOff.Text = dr["ChiefOfficerName"].ToString();
    //        txt_SecOff.Text = dr["SecondOfficeName"].ToString();
    //        txt_ChiefEng.Text = dr["ChiefEngineerName"].ToString();
    //        txt_FirstAssEng.Text = dr["FirstAssistantEnggName"].ToString();
    //        txt_Inspector.Text = dr["InspectorName"].ToString();
    //        //DataTable dt3 = Inspection_Planning.AddInspectors(0, int.Parse(Session["Insp_Id"].ToString()), 0, 0, DateTime.Now, 0, 0, DateTime.Now, "", 0, 0, "CHKATT");
    //        DataTable dt3 = Inspection_Planning.AddInspectors(0, int.Parse(Session["Insp_Id"].ToString()), 0, 0, DateTime.Now, 0, 0, DateTime.Now, "", 0, 0, "SELECT");
    //        txt_MTMSup.Text = "";
    //        txt_MTMSup.ReadOnly = true;
    //        foreach (DataRow Drchk in dt3.Rows)
    //        {
    //            if (Drchk["Attending"].ToString() == "True")
    //            {
    //                txt_MTMSup.Text = Drchk["Name"].ToString();
    //                break;
    //            }
    //            else if (Drchk["Attending"].ToString() == "False")
    //            {
    //                txt_MTMSup.Text = Drchk["Name"].ToString();
    //            }
    //        }
    //        //if (dt3.Rows.Count > 0)
    //        //{
    //        //    if (dt3.Rows[0][0].ToString() == "NO")
    //        //    {
    //        //        txt_MTMSup.Text = "";
    //        //    }
    //        //    else
    //        //    {
    //        //        txt_MTMSup.Text = dr["Supt"].ToString();
    //        //    }
    //        //}
    //        HiddenField_Supt.Value = txt_MTMSup.Text;
    //        if (Session["UserName"] != null)
    //        {
    //            FrstAppBy = dr["FirstApprovedByName"].ToString();
    //            if (FrstAppBy != "")
    //            {
    //                ////if (Session["UserName"].ToString() == FrstAppBy)
    //                ////    txt_FirstAppBy.Text = Session["UserName"].ToString();
    //                ////else
    //                ////    chk_FirstApp.Enabled = false;
    //            }
    //        }
    //        txt_Status.Text = dr["Status"].ToString();
    //    }
    //    DataTable dt11 = Inspection_Response.ResponseDetails(intInspectionId, 0, "", "", 0, 0, 0, 0, 0, "", "", "BYID", "", "", "", "");
    //    if (dt11.Rows.Count > 0)
    //    {
    //        foreach (DataRow dr1 in dt11.Rows)
    //        {
    //            txtCreatedBy_Response.Text = dr1["Created_By"].ToString();
    //            txtCreatedOn_Response.Text = dr1["Created_On"].ToString();
    //            txtModifiedBy_Response.Text = dr1["Modified_By"].ToString();
    //            txtModifiedOn_Response.Text = dr1["Modified_On"].ToString();
    //        }
    //    }
    //}
    //Bind ListOfObservation ListBox By InspectionDueId
    public void BindListofObservations(int intInspDueId)
    {
        DataTable dtLstObs = Inspection_Response.ResponseDetails(intInspDueId, 0, "", "", 0, 0, 0, 0, 0, "", "", "GETOBS", "", "", "", "");
        //DataTable dtLstObs = Inspection_Response.ResponseDetails(intInspDueId, "", "", "GETOBS");
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
    //Checking Whether the Observations are Closed or Not
    public void CheckObservations(int intInsp_DueId)
    {
        //DataTable dtchk = Inspection_Response.ResponseDetails(intInsp_DueId, "", "", "CHKOBS");
        //DataTable dtchk = Inspection_Response.ResponseDetails(intInsp_DueId, 0, "", "", 0, 0, 0, 0, 0, "", "", "CHKOBS");
        //if (dtchk.Rows[0][0].ToString() != "0")
        //{
        //    btn_SaveApp.Enabled = true;
        //}
        //else
        //    btn_SaveApp.Enabled = false;
    }
    #endregion

    #region "Events"
    //****Retrieving Question Details From Observation
    protected void lst_Observation_SelectedIndexChanged(object sender, EventArgs e)
    {
        intObsId = Convert.ToInt32(lst_Observation.SelectedValue);
        ViewState["intObsId"] = intObsId;
        HiddenField_ObsId.Value = intObsId.ToString();
        DataTable dtQues = Inspection_Response.ResponseDetails(intObsId, 0, "","", 0, 0, 0, 0, 0, "", "", "QUESBYOID", "", "", "", "");
        if (dtQues.Rows.Count > 0)
        {
            foreach (DataRow dr in dtQues.Rows)
            {
                txt_Question.Text = dr["QuesNum"].ToString();
                txt_Quest.Text = dr["Descptn"].ToString();
                txt_Deficiency.Text = dr["Deficiency"].ToString();
                Session.Add("DefText", dr["Deficiency"].ToString());
                txt_Response.Text = dr["Response"].ToString();
                Session.Add("RespMgmtText", dr["Response"].ToString());
                HiddenField_QuesId.Value = dr["QuestionId"].ToString();

                //------------
                string chkSql = "select Count(*) from (select * from t_InspectionDue Where InspectionID in (Select ID from m_Inspection where InspectionGroup=4) )a where a.ID=" + intInspection_Id.ToString();
                DataTable chkDt = Common.Execute_Procedures_Select_ByQuery(chkSql);
                if (Common.CastAsInt32(chkDt.Rows[0][0]) == 0)
                {
                    divPscCode.Visible = false;
                }
                else
                {
                    divPscCode.Visible = true;
                    lblPscCode.Text = GetPSCCodeByID(Common.CastAsInt32(dr["PscCode"]).ToString());
                }
                //------------

                if (dr["FollowUpItem"].ToString() == "Y")
                {
                    chk_FollowupItem.Checked = true;
                    
                    if (dr["Closed"].ToString() != "")
                    {
                        if (dr["Closed"].ToString() == "False")
                        {
                            chk_FollowupItem.BackColor = System.Drawing.Color.Red;
                            chk_FollowupItem.ForeColor = System.Drawing.Color.White;
                        }
                        else
                        {
                            chk_FollowupItem.BackColor = System.Drawing.Color.DarkGreen;
                            chk_FollowupItem.ForeColor = System.Drawing.Color.White;
                        }
                    }
                }
                else
                {
                    chk_FollowupItem.Checked = false;
                    chk_FollowupItem.ForeColor = System.Drawing.Color.Black;
                    chk_FollowupItem.BackColor = System.Drawing.Color.White;
                }


                

                Session["FU"] = chk_FollowupItem.Checked;


                if (dr["HighRisk"].ToString() == "True")
                    chkHighRisk.Checked = true;
                else
                    chkHighRisk.Checked = false;

                if (dr["Response"].ToString() == "")
                {
                    btn_Save.Enabled = Auth.isAdd;
                    btn_Edit.Enabled = false;
                    btn_Delete.Enabled = false;
                }
                else
                {
                    btn_Save.Enabled = false;
                    btn_Edit.Enabled = Auth.isEdit;
                    btn_Delete.Enabled = Auth.isDelete;
                }
            }
        }
    }
    //****Retrieving Description From Question Number (In PopUp Window)
    protected void btn_ForDesc_Click(object sender, EventArgs e)
    {
        Boolean blSelFlag = false;
        for (int i = 0; i <= lst_Observation.Items.Count - 1; i++)
        {
            if (lst_Observation.Items[i].Selected == true) { blSelFlag = true; }
        }
        if (blSelFlag == false)
        {
            lblmessage.Text = "Please select an Observation.";
            lst_Observation.Focus();
            return;
        }
    }
    //****Updation Of Response On The Basis Of ObservationId
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        //if (Session["Insp_Id"] == null) { lblmessage.Text = "Please save Planning first."; return; }
        //try
        //{
        //    Inspection_Response.ResponseDetails(int.Parse(ViewState["intObsId"].ToString()), intInspection_Id, "", txt_Response.Text, 0, 0, 0, 0, 0, "", "", "MODIFY", (chk_RespUp.Checked) ? "Y" : "N", "", "", (chk_FollowupItem.Checked) ? "Y" : "N");
        //    //Inspection_Response.ResponseDetails(int.Parse(ViewState["intObsId"].ToString()), "", txt_Response.Text, "MODIFY");
        //    //SendMail.Mail("Response", "Response", "Response is Updated");
        //    lblmessage.Text = "Response Saved Successfully.";
        //    btn_Save.Enabled = false;
        //    btn_Edit.Enabled = true;
        //    btn_Delete.Enabled = true;
        //    btnNotify.Enabled = true;
        //    //if (chk_FollowupItem.Checked)
        //    //{
        //    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "<script>OpenFollowUpItemsWindow();</script>", false);
        //    //}
        //}
        //catch(Exception ex) {lblmessage.Text=ex.StackTrace.ToString(); }
    }
    protected void btn_Print_Click(object sender, EventArgs e)
    {
        
    }
    protected void btn_Edit_Click(object sender, EventArgs e)
    {
        //btn_Save.Enabled = true;
        //btn_Edit.Enabled = false;
        //btn_Delete.Enabled = true;
        //btn_Save.Enabled = Auth.isEdit;
        //btn_Delete.Enabled = Auth.isDelete;
    }
    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        try
        {
            Inspection_Response.ResponseDetails(int.Parse(ViewState["intObsId"].ToString()), intInspection_Id, "", "", 0, 0, 0, 0, 0, "", "", "MODIFY", "N", "", "", "N");
            lblmessage.Text = "Response Deleted Successfully.";
            btn_Save.Enabled = true;
            btn_Edit.Enabled = false;
            btn_Delete.Enabled = false;
            btnNotify.Enabled = true;
            btn_Save.Enabled = Auth.isAdd;
        }
        catch (Exception ex) { lblmessage.Text = ex.StackTrace.ToString(); }

        //txt_Response.Text = "";
        //btn_Save.Enabled = true;
        //btn_Edit.Enabled = false;
        //btn_Delete.Enabled = false;
        //btn_Save.Enabled = Auth.isEdit;
    }    
    protected void chk_SecApp_CheckedChanged(object sender, EventArgs e)
    {
        //if (chk_SecApp.Checked)
        //{
        //    if (Session["UserName"] != null)
        //        txt_SecAppBy.Text = Session["UserName"].ToString();
        //}
        //else
        //{
        //    txt_SecAppBy.Text = "";
        //}
    }

    protected void chkHighRisk_OnCheckedChanged(object sender, EventArgs e)
    {
        string SQL = "Update t_Observations set HighRisk="+((chkHighRisk.Checked)?"1":"0")+" Where ID=" + lst_Observation.SelectedValue + "";
        DataTable chkDt = Common.Execute_Procedures_Select_ByQuery(SQL);

    }

    //PopUp Documents
    protected void btnUploadDoc_OnClick(object sender, EventArgs e)
    {
        int Obsid = Common.CastAsInt32(ViewState["intObsId"]);
        if (Obsid > 0)
        {
            DivOtherDocs.Visible = true;
            frmDocs.Attributes.Add("src", "UploadObservationDocs.aspx?ObservationId=" + Obsid.ToString());
        }
    }

    protected void btnCloseDocuments_OnClick(object sender, EventArgs e)
    {
        DivOtherDocs.Visible = false;
    }
    //protected void btnSaveDoc_Click(object sender, EventArgs e)
    //{
    //    int intObsId = Common.CastAsInt32(ViewState["intObsId"]);
    //    //if (CaseID == 0)
    //    //{
    //    //    lblMessage.Text = "Please save the case first.";
    //    //    return;
    //    //}
    //    if (DocID == 0)
    //        if (!flAttachDocs.HasFile)
    //        {
    //            lblMsgDoc.Text = "Please select a file.";
    //            flAttachDocs.Focus();
    //            return;
    //        }
    //    FileUpload img = (FileUpload)flAttachDocs;
    //    Byte[] imgByte = new Byte[0];
    //    string FileName = "";

    //    if (img.HasFile && img.PostedFile != null)
    //    {
    //        HttpPostedFile File = flAttachDocs.PostedFile;

    //        if (Path.GetExtension(File.FileName).ToString() == ".pdf" || Path.GetExtension(File.FileName).ToString() == ".txt" || Path.GetExtension(File.FileName).ToString() == ".doc" || Path.GetExtension(File.FileName).ToString() == ".docx" || Path.GetExtension(File.FileName).ToString() == ".xls" || Path.GetExtension(File.FileName).ToString() == ".xlsx" || Path.GetExtension(File.FileName).ToString() == ".ppt" || Path.GetExtension(File.FileName).ToString() == ".pptx")
    //        {
    //            FileName = "Observation" + "_" + DateTime.Now.ToString("dd-MMM-yyyy") + "_" + DateTime.Now.TimeOfDay.ToString().Replace(":", "-").ToString() + Path.GetExtension(File.FileName);


    //            string path = "../EMANAGERBLOB/Inspection/Observation/";
    //            flAttachDocs.SaveAs(Server.MapPath(path) + FileName);
    //        }
    //        else
    //        {
    //            lblMsgDoc.Text = "File type not supported. Only pdf and microsoft ofiice files accepted.";
    //        }


    //    }
    //    string strSQL = "EXEC sp_InsertUpdatet_ObservationDocs " + DocID + "," + intObsId + ",'" + txt_Desc.Text.Trim().Replace("'", "`") + "','" + FileName + "','" + Convert.ToInt32(Session["loginid"].ToString()) + "'";
    //    DataTable dtDocs = Budget.getTable(strSQL).Tables[0];
    //    if (dtDocs.Rows.Count > 0)
    //    {
    //        lblMsgDoc.Text = "Record Successfully Saved.";
    //        txt_Desc.Text = "";
    //        BindDocs();
    //        DocID = 0;
    //    }
    //}
    //protected void imgEditDoc_OnClick(object sender, EventArgs e)
    //{
    //    ImageButton ImgEdit = (ImageButton)sender;
    //    HiddenField hfDocID = (HiddenField)ImgEdit.Parent.FindControl("hfDocID");
    //    DocID = Common.CastAsInt32(hfDocID.Value);
    //    Label lblDesc = (Label)ImgEdit.Parent.FindControl("lblDesc");
    //    txt_Desc.Text = lblDesc.Text;

    //}
    //protected void imgDelDoc_OnClick(object sender, EventArgs e)
    //{
    //    ImageButton ImgDel = (ImageButton)sender;
    //    HiddenField hfDocID = (HiddenField)ImgDel.Parent.FindControl("hfDocID");
    //    DocID = Common.CastAsInt32(hfDocID.Value);

    //    string sql = "delete from t_ObservationDocs  where DocID=" + hfDocID.Value + "";
    //    DataSet dtGroups = Budget.getTable(sql);
    //    BindDocs();
    //    DocID = 0;
    //}
    //public void BindDocs()
    //{
    //    int intObsId = Common.CastAsInt32(ViewState["intObsId"]);
    //    string strSQL = "select replace(convert(varchar,UploadedOn ,106) ,' ','-')UploadedDate,(select Firstname+' '+Lastname from dbo.UserLogin UL where UL.LoginID= CS.UploadedBy)UploadedBy,* from t_ObservationDocs CS where ObservationID=" + intObsId ;
    //    DataTable dtDocs = Budget.getTable(strSQL).Tables[0];
    //    if (dtDocs.Rows.Count > 0)
    //    {
    //        rptDocs.DataSource = dtDocs;
    //        rptDocs.DataBind();
    //    }
    //    else
    //    {
    //        rptDocs.DataSource = null;
    //        rptDocs.DataBind();
    //    }
    //}
    public string GetPSCCodeByID(string PSCID)
    {
        try
        {
            string sql = "select PscCode from dbo.m_psccode Where ID =" + PSCID;
            DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
            if (Dt.Rows.Count > 0)
            {
                return Dt.Rows[0][0].ToString();
            }
            else
                return "";
        }
        catch
        {
            return "";
        }
    }
    


    #endregion
    protected void btnNotify_Click(object sender, EventArgs e)
    {
        string strRInspNum = "", strRDoneDt = "", strRPortDone = "";
        DataTable dtmail = Inspection_TravelSchedule.SelectInspectionDetailsByInspId(int.Parse(Session["Insp_Id"].ToString()));
        if (dtmail.Rows.Count > 0)
        {
            strRInspNum = dtmail.Rows[0]["InspectionNo"].ToString();
            strRDoneDt = dtmail.Rows[0]["DoneDt"].ToString();
            strRPortDone = dtmail.Rows[0]["PortDone"].ToString();
        }
        try
        {
            SendMail.Mail("Response", strRInspNum, strRDoneDt, strRPortDone, "Response has been uploaded.");
            lblmessage.Text = "Mail Send Successfully.";
        }
        catch { }
    }
    protected void chk_FollowupItem_CheckedChanged(object sender, EventArgs e)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM t_Observations WHERE ID=" + HiddenField_ObsId.Value);
        if (chk_FollowupItem.Checked)
        {
            try
            {
                DateTime dts = DateTime.Parse(dt.Rows[0]["TargetCloseDt"].ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "window.open('FollowUpPopUp.aspx?InspId=" + intInspection_Id.ToString() + "&ObsId=" + HiddenField_ObsId.Value + "');", true);
            }
            catch
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "<script>OpenFollowUpItemsWindow();</script>", false);
            }
            
        }
    }
    protected void chk_RespUp_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            Inspection_Response.ResponseDetails(0, intInspection_Id, "", "", 0, 0, 0, 0, 0, "", "", "UPRESPUP", "Y", "", "", "");
            lblmessage.Text = "Response Uploaded Saved Successfully.";
        }
        catch (Exception ex) { lblmessage.Text = ex.StackTrace.ToString(); }
    }

    protected void btnEditDeficiency_OnClick(object sender, EventArgs e)
    {
        LinkButton imgEdit = (LinkButton)sender;
        DefTableID = Common.CastAsInt32(imgEdit.CommandArgument);
        dvAddUpdateDeficiency.Visible = true;
        Show_DeficiencyRecord();
    }
    protected void btnSaveDeficiency_OnClick(object sender, EventArgs e)
    {
        string strResponsibility = "";
        if (txtDeficiency.Text.Trim() == "")
        {
            lblMsgDeficiency.Text = "Please enter deficiency."; return;
        }
        if (txtCorrAction.Text.Trim() == "")
        {
            lblMsgDeficiency.Text = "Please enter corrective action."; return;
        }
        if (txtTCD.Text.Trim() == "")
        {
            lblMsgDeficiency.Text = "Please enter target closure date."; return;
        }

        for (int k = 0; k < chklst_Respons.Items.Count; k++)
        {
            if (chklst_Respons.Items[k].Selected == true)
            {
                if (strResponsibility == "")
                {
                    strResponsibility = chklst_Respons.Items[k].Value;
                }
                else
                {
                    strResponsibility = strResponsibility + "," + chklst_Respons.Items[k].Value;
                }
            }
        }

        Common.Set_Procedures("dbo.sp_IU_t_ObservationsNew");
        Common.Set_ParameterLength(9);
        Common.Set_Parameters(
                new MyParameter("@TABLEID", DefTableID),
                new MyParameter("@INSPDUE_ID", intInspection_Id.ToString()),
                new MyParameter("@DEFICIENCY", txtDeficiency.Text.Trim()),
                new MyParameter("@CORRACTIONS", txtCorrAction.Text.Trim()),
                new MyParameter("@TCLDATE", txtTCD.Text.Trim()),
                new MyParameter("@Responsibility", strResponsibility),
                new MyParameter("@CreatedBy", intLogin_Id),
                new MyParameter("@QuestionNo", ""),
                new MyParameter("@MasterComments", hfdMC.Value.Trim())
            );
        Boolean res;
        DataSet ds = new DataSet();
        res = Common.Execute_Procedures_IUD(ds);
        if (res)
        {
            if (DefTableID <= 0) // add mode
            {
                txtDeficiency.Text = "";
                txtCorrAction.Text = "";

                txtACreatedBy.Text = "";
                txtACreatedOn.Text = "";
                txtAModifiedBy.Text = "";
                txtAModifiedOn.Text = "";
                hfdMC.Value = "";

                chklst_Respons.SelectedIndex = -1;
            }
            lblMsgDeficiency.Text = "Record saved successfully.";
        }
    }
    protected void btnCloseDeficiency_OnClick(object sender, EventArgs e)
    {
        dvAddUpdateDeficiency.Visible = false;
        DefTableID = 0;
        Show_Detail_Record_MTM(intInspection_Id);
    }

    // Closure
    protected void btnDeleteOB_OnClick(object sender, EventArgs e)
    {
        LinkButton imgEdit = (LinkButton)sender;
        DefTableID = Common.CastAsInt32(imgEdit.CommandArgument);
        string sql = "DELETE from dbo.t_ObservationsNew Where TableID=" + DefTableID.ToString() + "";
        Common.Execute_Procedures_Select_ByQuery(sql);
        DefTableID = 0;
        Show_Detail_Record_MTM(intInspection_Id);
    }
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
    protected void Show_DeficiencyRecord()
    {
        string SQL = "Select Deficiency,CorrActions,TCLDate,Responsibility,MasterComments,(select FirstName + ' ' + LastName from dbo.userlogin where loginid=t_ObservationsNew.CreatedBy) as CreatedBy,(select FirstName + ' ' + LastName from dbo.userlogin where loginid=t_ObservationsNew.ModifiedBy) as ModifiedBy,CreatedOn,ModifiedOn from t_ObservationsNew Where TableID=" + DefTableID.ToString() + "";
        DataSet DS = Budget.getTable(SQL);
        if (DS != null)
        {
            DataRow Dr = DS.Tables[0].Rows[0];
            txtDeficiency.Text = Dr["Deficiency"].ToString();
            txtCorrAction.Text = Dr["CorrActions"].ToString();
            txtTCD.Text = Common.ToDateString(Dr["TCLDate"].ToString());

            txtACreatedBy.Text = Dr["CreatedBy"].ToString();
            txtACreatedOn.Text = Common.ToDateString(Dr["CreatedOn"]);
            txtAModifiedBy.Text = Dr["ModifiedBy"].ToString();
            txtAModifiedOn.Text = Common.ToDateString(Dr["ModifiedOn"]);
            hfdMC.Value = Dr["MasterComments"].ToString();

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
    }
    protected void btnDeficiencyPopup_OnClick(object sender, EventArgs e)
    {
        dvAddUpdateDeficiency.Visible = true;
        txtDeficiency.Text = "";
        txtCorrAction.Text = "";

        txtACreatedBy.Text = "";
        txtACreatedOn.Text = "";
        txtAModifiedBy.Text = "";
        txtAModifiedOn.Text = "";
        hfdMC.Value = "";

        chklst_Respons.SelectedIndex = -1;
    }
    protected void btnPrint_OnClick(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "print", "window.open('../Inspection/Reports/FollowUpTracker_Manual_Crystal.aspx?Inspid=" + intInspection_Id.ToString() + "');", true);         
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
        a_file_C.Visible = false;
        for (int j = 0; j <= rdbflaws_C.Items.Count - 1; j++)
        {
            rdbflaws_C.Items[j].Selected = false;
        }
        a_file_C.HRef = "";

        string sql = "Select Deficiency,CorrActions,TCLDate,Responsibility,(select FirstName + ' ' + LastName from dbo.userlogin where loginid=t_ObservationsNew.CreatedBy) as CreatedBy,(select FirstName + ' ' + LastName from dbo.userlogin where loginid=t_ObservationsNew.ModifiedBy) as ModifiedBy,CreatedOn,ModifiedOn from dbo.t_ObservationsNew Where TableID=" + DefTableID.ToString() + "";
        DataTable DS = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DS != null)
        {
            DataRow Dr = DS.Rows[0];
            txtDeficiency_C.Text = Dr["Deficiency"].ToString();
            txtCorrAction_C.Text = Dr["CorrActions"].ToString();
            txtTCD_C.Text = Common.ToDateString(Dr["TCLDate"].ToString());

            if (Dr["Responsibility"].ToString() != "")
            {
                char[] resp = { ',' };
                Array rs = Dr["Responsibility"].ToString().Split(resp);
                for (int l = 0; l < chklst_Respons_C.Items.Count; l++)
                {
                    chklst_Respons_C.Items[l].Selected = false;
                }
                for (int m = 0; m < rs.Length; m++)
                {
                    if (rs.GetValue(m).ToString() == "Vessel")
                        chklst_Respons_C.Items[0].Selected = true;
                    if (rs.GetValue(m).ToString() == "Office")
                        chklst_Respons_C.Items[1].Selected = true;
                }
            }
            else
                chklst_Respons_C.SelectedIndex = -1;

            txtACreatedBy_C.Text = DS.Rows[0]["CreatedBy"].ToString();
            txtACreatedOn_C.Text = Common.ToDateString(DS.Rows[0]["CreatedOn"]);
            txtAModifiedBy_C.Text = DS.Rows[0]["ModifiedBy"].ToString();
            txtAModifiedOn_C.Text = Common.ToDateString(DS.Rows[0]["ModifiedOn"]);
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
            txtACreatedBy_C.Text = DS.Rows[0]["CreatedBy"].ToString();
            txtACreatedOn_C.Text = Common.ToDateString(DS.Rows[0]["CreatedOn"]);
            txtAModifiedBy_C.Text = DS.Rows[0]["ModifiedBy"].ToString();
            txtAModifiedOn_C.Text = Common.ToDateString(DS.Rows[0]["ModifiedOn"]);

            txt_ClosedDate.Text = Common.ToDateString(DS.Rows[0]["ClosedOn"].ToString());
            txt_ClosedRemarks.Text = DS.Rows[0]["ClosureRemarks"].ToString().Replace("''", "'");
            txt_ClosedBy.Text = DS.Rows[0]["ClosedBy"].ToString();
            txt_ClosedOn.Text = Common.ToDateString(DS.Rows[0]["ClosedOn"].ToString());
            a_file_C.Visible = false;
            if (DS.Rows[0]["FileType"].ToString().Trim() != "")
            {
                a_file_C.HRef = "~/EMANAGERBLOB/Inspection/FollowUpClosure/DeficiencyClosure_" + DefTableID + DS.Rows[0]["FileType"].ToString();
                a_file_C.Visible = true;

            }

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
            txtDeficiency_C.Text = Dr["Deficiency"].ToString();
            txtCorrAction_C.Text = Dr["CorrActions"].ToString();
            txtTCD_C.Text = Common.ToDateString(Dr["TCLDate"].ToString());

            if (Dr["Responsibility"].ToString() != "")
            {
                char[] resp = { ',' };
                Array rs = Dr["Responsibility"].ToString().Split(resp);
                for (int l = 0; l < chklst_Respons_C.Items.Count; l++)
                {
                    chklst_Respons_C.Items[l].Selected = false;
                }
                for (int m = 0; m < rs.Length; m++)
                {
                    if (rs.GetValue(m).ToString() == "Vessel")
                        chklst_Respons_C.Items[0].Selected = true;
                    if (rs.GetValue(m).ToString() == "Office")
                        chklst_Respons_C.Items[1].Selected = true;
                }
            }
            else
                chklst_Respons_C.SelectedIndex = -1;

        }

        DefTableID = 0;
    }
    protected void btnSaveClosure_Click(object sender, EventArgs e)
    {
        try
        {
            string strflaws = "";
            if (txt_ClosedDate.Text == "")
            {
                Label1.Text = "Please enter Closed Date.";
                txt_ClosedDate.Focus();
                return;
            }
            if (txt_ClosedRemarks.Text == "")
            {
                Label1.Text = "Please enter Closed Remarks.";
                txt_ClosedRemarks.Focus();
                return;
            }
            if (fu_ClosureEvidence.PostedFile == null && fu_ClosureEvidence.FileContent.Length<= 0)
            {
                Label1.Text = "Please select closure evidance.";
                fu_ClosureEvidence.Focus();
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
                    Label1.Text = "Invalid File Type. (Valid Files Are .doc, .docx, .xls, .xlsx, .pdf)";
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
        Show_Detail_Record_MTM(intInspection_Id);
        DefTableID = 0;
    }
    
    protected void btnOpenCheckList_OnClick(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "window.open('CheckList.aspx?InspId=" + intInspection_Id.ToString() + "');", true);
    }
    //protected void FollowUpPopUp_Click(object sender, EventArgs e)
    //{
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "window.open('FollowUpPopUp.aspx?InspId=" + intInspection_Id.ToString() + "&ObsId=" + HiddenField_ObsId.Value + "');", true);
    //}

}