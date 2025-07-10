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

public partial class Transactions_InspectionCloser : System.Web.UI.Page
{
    /// <summary>
    /// Page Name            : InspectionCloser.aspx
    /// Purpose              : This is the Inspection Closing page
    /// Author               : Shobhita
    /// Developed on         : 3-Feb-2010
    /// </summary>
    #region "Declarations"
    int intLogin_Id;
    int intInspection_Id;
    string strInspStatus = "";
    string FrstAppBy = "";
    int FrstAppById = 0;
    int SecAppById = 0;
    Authority Auth;
    string strInsp_Status = "";
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
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1053);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
        this.Form.DefaultButton = this.btn_SaveApp.UniqueID.ToString();
        lblmessage.Text = "";
        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
            return; 
        }
        else
        {
            intLogin_Id = Convert.ToInt32(Session["loginid"].ToString());
        }
        if (Session["Insp_Id"] == null) { Session.Add("PgFlag", 1); Response.Redirect("InspectionSearch.aspx"); }
        try { intInspection_Id = int.Parse(Session["Insp_Id"].ToString()); }
        catch { }
        if (Session["Insp_Id"] == null)
            btn_SaveApp.Enabled = false;

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
                Alerts.HANDLE_AUTHORITY(11, btn_SaveApp, null, null, null, Auth);
            }
            catch
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
            }
            btn_SaveApp.Enabled = Auth.isEdit;
            chk_FirstApp.Enabled = Auth.isVerify;
            chk_SecApp.Enabled = Auth.isVerify2;
            if (Session["Insp_Id"] != null)
            {
                //****Code To Check Inspection Status
                DataTable dt22 = Inspection_TravelSchedule.SelectInspectionDetailsByInspId(intInspection_Id);
                if (dt22.Rows.Count > 0)
                {
                    strInspStatus = dt22.Rows[0]["Status"].ToString();
                }
                if ((strInspStatus == "Due") || (strInspStatus == "Closed"))
                {
                    chk_FirstApp.Enabled = false;
                    chk_SecApp.Enabled = false;
                    btn_SaveApp.Enabled = false;
                }
                else
                {
                    chk_FirstApp.Enabled = true;
                    chk_SecApp.Enabled = true;
                    btn_SaveApp.Enabled = true;
                    chk_FirstApp.Enabled = Auth.isVerify;
                    chk_SecApp.Enabled = Auth.isVerify2;
                    btn_SaveApp.Enabled = Auth.isEdit;

                }
                //if (strInspStatus == "Closed")
                //{
                //    btn_SaveRemark.Enabled = false;
                //    btn_SaveOpRem.Enabled = false;
                //}
                //else
                //{
                //    btn_SaveRemark.Enabled = true;
                //    btn_SaveOpRem.Enabled = true;
                //    btn_SaveRemark.Enabled = Auth.isEdit;
                //    btn_SaveOpRem.Enabled = Auth.isEdit;
                //}
                if (Session["Insp_Id"] != null)
                {
                    DataTable dt88 = Inspection_Planning.CheckInspectionStatus(int.Parse(Session["Insp_Id"].ToString()));
                    if (dt88.Rows.Count > 0)
                    {
                        strInsp_Status = dt88.Rows[0]["Status"].ToString();
                    }
                    if ((strInsp_Status != "Planned") && (strInsp_Status != "Executed") && (strInsp_Status != "Observation") && (strInsp_Status != "Response"))
                    {
                        btn_SaveApp.Enabled = false;

                    }
                    else
                    {
                        btn_SaveApp.Enabled = true;
                    }
                }
                //****Displaying Records In Uppermost Readonly Fields 
                Show_Header_Record();
                ////****Show Approval Records
                //Show_ApprovalDetails(intInspection_Id);
                if (Session["DueMode"] != null)
                {
                    if (Session["DueMode"].ToString() == "ShowFull")
                    {

                    }
                    if ((strInsp_Status == "Planned") || (strInsp_Status == "Executed") || (strInsp_Status == "Observation") || (strInsp_Status == "Response"))
                    {
                        try
                        {
                            Alerts.HANDLE_AUTHORITY(11, btn_SaveApp, null, null, null, Auth);
                            //******Accessing UserOnBehalf/Subordinate Right******
                            try
                            {
                                if (Session["Insp_Id"] != null)
                                {
                                    int useronbehalfauth = Alerts.UserOnBehalfRight(Convert.ToInt32(Session["loginid"].ToString()), Convert.ToInt32(Session["Insp_Id"].ToString()));
                                    if (useronbehalfauth <= 0)
                                    {
                                        btn_SaveApp.Enabled = false;


                                    }
                                    else
                                    {
                                        if ((strInspStatus != "Closed") && (strInspStatus != "Due"))
                                        {
                                            btn_SaveApp.Enabled = true;
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
                        //btn_SaveApp.Enabled = Auth.isEdit;
                        //chk_FirstApp.Enabled = Auth.isFirstApproval;
                        //chk_SecApp.Enabled = Auth.isSecondApproval;
                    }
                }
                else
                {
                }


                if (Session["DueMode"] != null)
                {
                    if (Session["DueMode"].ToString() == "ShowId")
                    {
                        btn_SaveApp.Enabled = Auth.isEdit;


                    }
                    if ((strInsp_Status == "Planned") || (strInsp_Status == "Executed") || (strInsp_Status == "Observation") || (strInsp_Status == "Response"))
                    {
                        try
                        {
                            Alerts.HANDLE_AUTHORITY(11, btn_SaveApp, null, null, null, Auth);
                            //******Accessing UserOnBehalf/Subordinate Right******
                            try
                            {
                                if (Session["Insp_Id"] != null)
                                {
                                    int useronbehalfauth = Alerts.UserOnBehalfRight(Convert.ToInt32(Session["loginid"].ToString()), Convert.ToInt32(Session["Insp_Id"].ToString()));
                                    if (useronbehalfauth <= 0)
                                    {
                                        btn_SaveApp.Enabled = false;

					
                                    }
                                    else
                                    {
                                        if ((strInspStatus != "Closed") && (strInspStatus != "Due"))
                                        {
                                            btn_SaveApp.Enabled = true;
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
                        //btn_SaveApp.Enabled = Auth.isEdit;
                        //chk_FirstApp.Enabled = Auth.isFirstApproval;
                        //chk_SecApp.Enabled = Auth.isSecondApproval;
                    }
                }

                //=============================
                DataTable dt78 = Inspection_Response.CheckRandomInsp(intInspection_Id);
                if (dt78.Rows.Count > 0)
                {
                    if (dt78.Rows[0]["RandomInspection"].ToString() == "Y")
                    {
                        txt_InspValidity.Visible=true;
                        lblInspVal.Visible = true;
                        ImageButton2.Visible = true;
                    }
                }
                //=============================
            }

            //***Check whether Inspection is On Hold or not*******
            DataTable dtchk = Inspection_TravelSchedule.CheckInspectionOnHold(Convert.ToInt32(Session["Insp_Id"].ToString()));
            if (dtchk.Rows.Count > 0)
            {
                if (dtchk.Rows[0][0].ToString() == "Y")
                {
                    btn_SaveApp.Enabled = false;
                }
            }

            //****************************************************
        }
        if (hdnInspectno.Value.Contains("/CDI/"))
        {
            if (btn_SaveApp.Enabled && btn_SaveApp.Visible)
            {
                btn_SaveApp.Enabled = false;
            }
            btnMarkEntry.Enabled = true;
        }
        else
        {
            btnMarkEntry.Enabled = false;
        }

        //---------------------
        chk_FirstApp.Enabled = chk_FirstApp.Enabled & Auth.isVerify;
        chk_SecApp.Enabled = chk_SecApp.Enabled & Auth.isVerify2;
    }
    #endregion

    #region "User Defined Functions"
    //****Show Records in Readonly Fields
    protected void Show_Header_Record()
    {
        DataTable dt1 = Inspection_TravelSchedule.SelectInspectionDetailsByInspId(intInspection_Id);
        foreach (DataRow dr in dt1.Rows)
        {
            ViewState["VesselId"] = dr["VesselId"].ToString();
            hdnInspectno.Value = dr["InspectionNo"].ToString();
           
            hdnDoneDate.Value = dr["DoneDt"].ToString();
           

            //****Approval Details
            chk_FirstApp.Checked = (dr["FirstApproval"].ToString() == "True");
            txt_FirstAppBy.Text = dr["FirstApprovedByName"].ToString();
            if (chk_FirstApp.Checked)
            {
                chk_FirstApp.Enabled = false;
                btn_SaveApp1.Enabled = false;
            }

            chk_SecApp.Checked = (dr["SecondApproval"].ToString() == "True");
            txt_SecAppBy.Text = dr["SecondApprovaedByName"].ToString();
            if (chk_SecApp.Checked)
            {
                chk_SecApp.Enabled = false;
                btn_SaveApp2.Enabled = false;
            }


            if (dr["InspectionCleared"].ToString() != "")
            {
                if (dr["InspectionCleared"].ToString() == "True") ddl_InspCleared.SelectedValue = "1"; else ddl_InspCleared.SelectedValue = "0";
            }
            else
            {
                ddl_InspCleared.SelectedValue = "NA";
            }
            if ((strInsp_Status != "Planned") && (strInsp_Status != "Executed") && (strInsp_Status != "Observation") && (strInsp_Status != "Response"))
            {  
                if (dr["InspectionValidityDt"].ToString() != "")
                {
                    txt_InspValidity.Text = dr["InspectionValidityDt"].ToString();
                }
                else
                {
                    txt_InspValidity.Text = "";
                }
            }

            chk_CloseInsp.Checked = ((dr["Status"].ToString() == "Due") || (dr["Status"].ToString() == "Closed"));
            if (chk_CloseInsp.Checked)
            {
                chk_CloseInsp.Enabled = false;
                btn_SaveApp.Enabled = false;
            }

            //txt_Remarks.Text = dr["ClosureRemarks"].ToString();
            //txt_OpRemarks.Text = dr["OperatorRemarks"].ToString();
            //********************
        }
        //DataTable dt11 = Inspection_Response.ResponseDetails(intInspectionId, "", "", "BYID");
        DataTable dt11 = Inspection_Response.ResponseDetails(intInspection_Id, 0, "", "", 0, 0, 0, 0, 0, "", "", "BYID", "", "", "", "");
        if (dt11.Rows.Count > 0)
        {
            foreach (DataRow dr1 in dt11.Rows)
            {
                //txtCreatedBy_Response.Text = dr1["Created_By"].ToString();
                //txtCreatedOn_Response.Text = dr1["Created_On"].ToString();
                //txtModifiedBy_Response.Text = dr1["Modified_By"].ToString();
                //txtModifiedOn_Response.Text = dr1["Modified_On"].ToString();
            }
        }
    }
    #endregion

    #region "Events"
    //****Updation Of Response On The Basis Of ObservationId
    protected void txt_FirstAppBy_TextChanged(object sender, EventArgs e)
    {
        //if (txt_FirstAppBy.Text.Trim() != "")
        //{
        //    DataTable dt1 = Inspection_Observation.CheckUserName(txt_FirstAppBy.Text,"");
        //    if (dt1.Rows[0][0].ToString() == "0")
        //    {
        //        lblmessage.Text = "Please enter correct Code.";
        //        txt_FirstAppBy.Focus();
        //        return;
        //    }
        //    else
        //    {
        //        FirstApprovedBy = int.Parse(dt1.Rows[0][0].ToString());
        //        txt_FirstAppBy.Text = dt1.Rows[0][1].ToString();
        //    }
        //}
    }
    protected void txt_SecAppBy_TextChanged(object sender, EventArgs e)
    {
        //if (txt_SecAppBy.Text.Trim() != "")
        //{
        //    DataTable dt1 = Inspection_Observation.CheckUserName(txt_SecAppBy.Text,"");
        //    if (dt1.Rows[0][0].ToString() == "0")
        //    {
        //        lblmessage.Text = "Please enter correct Code.";
        //        txt_SecAppBy.Focus();
        //        return;
        //    }
        //    else
        //    {
        //        SecondApprovedBy = int.Parse(dt1.Rows[0][0].ToString());
        //        txt_SecAppBy.Text = dt1.Rows[0][1].ToString();
        //    }
        //}
    }
    protected void chk_FirstApp_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_FirstApp.Checked == false)
        {
            txt_FirstAppBy.Text = "";
            return;
        }
        string currusername = Session["UserName"].ToString();
        string instype = getInspectionType();
        if (instype == "Internal")
        {
            if (chk_FirstApp.Checked && chk_SecApp.Checked)
            {
                if (currusername == txt_SecAppBy.Text.Trim())
                {
                    chk_FirstApp.Checked = false;
                    txt_FirstAppBy.Text = "";
                    lblmessage.Text = "Internal inspection needs 2 different person for close.";
                    return;
                }
            }
        }

        txt_FirstAppBy.Text = currusername;
    }
    protected void chk_SecApp_CheckedChanged(object sender, EventArgs e)
    {
        if(chk_SecApp.Checked==false)
        {
            txt_SecAppBy.Text = "";
            return;
        }
        string currusername = Session["UserName"].ToString();
        string instype = getInspectionType();
        if (instype == "Internal")
        {
            if (chk_SecApp.Checked && chk_FirstApp.Checked)
            {
                if (currusername == txt_FirstAppBy.Text.Trim())
                {
                    chk_SecApp.Checked = false;
                    txt_SecAppBy.Text = "";
                    lblmessage.Text = "Internal inspection needs 2 different person for close.";
                    return;
                }
            }
        }
        txt_SecAppBy.Text = currusername;
    }
    protected void btn_SaveApp1_Click(object sender, EventArgs e)
    {
        //-----------------
        if (!(chk_FirstApp.Checked))
        {
            lblmessage.Text = "Please check first approval to save.";
            chk_FirstApp.Focus();
            return;
        }
        else
        {
            Common.Execute_Procedures_Select_ByQuery("UPDATE t_InspectionDue SET FirstApproval=1,FirstApprovedBy=" + intLogin_Id + " WHERE Id=" + intInspection_Id);
            lblmessage.Text = "First approval done successfully.";
            Show_Header_Record();
        }
    }
    protected void btn_SaveApp2_Click(object sender, EventArgs e)
    {
        //-----------------
        if (!(chk_SecApp.Checked))
        {
            lblmessage.Text = "Please check second approval to save.";
            chk_SecApp.Focus();
            return;
        }
        else
        {
            Common.Execute_Procedures_Select_ByQuery("UPDATE t_InspectionDue SET SecondApproval=1,SecondApprovaedBy=" + intLogin_Id + " WHERE Id=" + intInspection_Id);
            lblmessage.Text = "Second approval done successfully.";
            Show_Header_Record();
        }
    }
    protected void btn_SaveApp_Click(object sender, EventArgs e)
    {
        if (Session["Insp_Id"] == null) { lblmessage.Text = "Please save Planning first."; return; }
        //----------------------
        if (chk_CloseInsp.Checked)
        {
            if (!(chk_FirstApp.Checked && chk_SecApp.Checked))
            {
                lblmessage.Text = "Both approval needs to be done before inspection closure.";
                return;
            }
        }
        //----------------------   
        if (hdnInspectno.Value.Contains("/CDI/"))
        {
            DataTable dt = Budget.getTable("SELECT * FROM T_CDIRESULT WHERE InsId=" + intInspection_Id.ToString()).Tables[0];
            if (dt.Rows.Count <= 0)
            {
                lblmessage.Text = "Please enter CDI marks to close the Inspections.";
                return;
            }
        }
        //----------------------
        bool randomInspection = false;
        DataTable dt78 = Inspection_Response.CheckRandomInsp(intInspection_Id);
        if (dt78.Rows.Count > 0)
            randomInspection = (dt78.Rows[0]["RandomInspection"].ToString() == "Y");
        
        if (!randomInspection && txt_InspValidity.Text == "")
        {
            lblmessage.Text = "Please Enter Inspection Validity Date.";
            txt_InspValidity.Focus();
            return;
        }
        //----------------------
        DataTable dt101 = Inspection_TravelSchedule.SelectInspectionDetailsByInspId(intInspection_Id);
        string Location = "", StartDate = "", ActDate = "", strDoneDt = "";

        StartDate = Common.ToDateString(dt101.Rows[0]["StartDate1"]);
        ActDate = Common.ToDateString(dt101.Rows[0]["DoneDt"]);
        Location = dt101.Rows[0]["PortDone"].ToString();

        if (Location.Trim() == "" || StartDate.Trim() == "" || Location.Trim() == "")
        {
            lblmessage.Text = "Please enter the observation details in order to close this Inspection.";
            txt_InspValidity.Focus();
            return;
        }

        DataTable dt54 = Inspection_TravelSchedule.SelectInspectionDetailsByInspId(intInspection_Id);
        if (dt54.Rows.Count > 0)
        {
            strDoneDt = Common.ToDateString(dt54.Rows[0]["DoneDt"]);
            if ((dt54.Rows[0]["FirstApprovedBy"].ToString()) != "")
                FrstAppById = int.Parse(dt54.Rows[0]["FirstApprovedBy"].ToString());
        }
        if ((strDoneDt != "") && (txt_InspValidity.Text != ""))
        {
            if (DateTime.Parse(strDoneDt) > DateTime.Parse(txt_InspValidity.Text))
            {
                lblmessage.Text = "Inspection Validity Date cannot be less than Inspection Done Date.";
                txt_InspValidity.Focus();
                return;
            }
        }

        //----------------------
        if (txt_InspValidity.Text.Trim() != "")
        {
            DataTable dt111 = Common.Execute_Procedures_Select_ByQuery("SELECT Interval FROM dbo.m_Inspection WHERE inspectiongroup=3 and Id IN (SELECT INSPECTIONID FROM dbo.T_INSPECTIONDUE T WHERE T.ID=" + intInspection_Id + ")");
            if (dt111.Rows.Count > 0)
            {
                DateTime InspValidityDate = Convert.ToDateTime(hdnDoneDate.Value).AddDays(Common.CastAsInt32(dt111.Rows[0]["Interval"]));

                DateTime InspValMax = InspValidityDate.AddDays(30);
                DateTime InspValMin = InspValidityDate.AddDays(-30);

                if (!(Convert.ToDateTime(txt_InspValidity.Text) >= InspValMin && Convert.ToDateTime(txt_InspValidity.Text) <= InspValMax))
                {
                    lblmessage.Text = "Inspection Validity Date should be beteeen " + InspValMin.ToString("dd-MMM-yyyy") + " and " + InspValMax.ToString("dd-MMM-yyyy");
                    txt_InspValidity.Focus();
                    return;
                }
            }
        }
        //----------------------

        string NewStatus = "Due";

        if ((dt78.Rows[0]["RandomInspection"].ToString() == "Y") && (txt_InspValidity.Text == ""))
            NewStatus = "Closed";

        if (ddl_InspCleared.SelectedValue != "NA")
        {
            Common.Execute_Procedures_Select_ByQuery("UPDATE t_InspectionDue SET InspClosedOn=getdate(),InspectionCleared =" + Convert.ToInt32(ddl_InspCleared.SelectedValue) + ",InspectionValidity = CASE WHEN '" + txt_InspValidity.Text.Trim() + "' = '' then NULL ELSE '" + txt_InspValidity.Text.Trim() + "' END ,Status = '" + NewStatus + "' where Id=" + intInspection_Id);
        }
        else
        {
            Common.Execute_Procedures_Select_ByQuery("UPDATE t_InspectionDue SET InspClosedOn=getdate(),InspectionValidity = CASE WHEN '" + txt_InspValidity.Text.Trim() + "' = '' then NULL ELSE '" + txt_InspValidity.Text.Trim() + "' END,Status = '" + NewStatus + "' where Id=" + intInspection_Id);
        }

        //*******************************************************************************************************************
       // Budget.getTable("EXEC [dbo].[KPI_MANAGE_36] " + intInspection_Id.ToString());
        lblmessage.Text = "Approval Details Saved Successfully.";
        Show_Header_Record();

    }
    protected void btn_SaveApp_Old_Click(object sender, EventArgs e)
    {
        string strInsStatus = "";
        string strDoneDt = "";
        int FirstAppById = 0;
        if (Session["Insp_Id"] == null) { lblmessage.Text = "Please save Planning first."; return; }
        try
        {
            if (chk_CloseInsp.Checked)
            {
                if(!(chk_FirstApp.Checked && chk_SecApp.Checked))
                {
                    lblmessage.Text = "Both approval needs to be done before inspection closure.";
                    return;
                }
            }

            //****Code To Check Inspection Status
            DataTable dt22 = Inspection_TravelSchedule.SelectInspectionDetailsByInspId(intInspection_Id);
            if (dt22.Rows.Count > 0)
            {
                strInspStatus = dt22.Rows[0]["Status"].ToString();
                if ((dt22.Rows[0]["SecondApprovaedBy"].ToString()) != "")
                {
                    SecAppById = int.Parse(dt22.Rows[0]["SecondApprovaedBy"].ToString());
                    //FirstAppById = int.Parse(dt22.Rows[0]["FirstApprovedBy"].ToString());
                }
            }
            if (SecAppById != 0)
            {
                if (chk_SecApp.Checked == false)
                {
                    lblmessage.Text = "Please check Second Approval.";
                    chk_SecApp.Focus();
                    return;
                }
            }
            if (chk_CloseInsp.Checked) // if going to close the inspection
            {
                if (hdnInspectno.Value.Contains("/CDI/"))
                {
                    DataTable dt = Budget.getTable("SELECT * FROM T_CDIRESULT WHERE InsId=" + intInspection_Id.ToString()).Tables[0];
                    if (dt.Rows.Count <= 0)
                    {
                        lblmessage.Text = "Please enter CDI marks to close the Inspections.";
                        return;
                    }
                }

                //*****Code To Check Whether Inspection is Random or Not (If Random Inspection, Inspection validity is not required.)
                DataTable dt78 = Inspection_Response.CheckRandomInsp(intInspection_Id);
                if (dt78.Rows.Count > 0)
                {
                    if (dt78.Rows[0]["RandomInspection"].ToString() != "Y")
                    {
                        if (txt_InspValidity.Text == "")
                        {
                            lblmessage.Text = "Please Enter Inspection Validity Date.";
                            txt_InspValidity.Focus();
                            return;
                        }
                    }
                }

            }
            if (chk_CloseInsp.Checked)
            {
                DataTable dt101 = Inspection_TravelSchedule.SelectInspectionDetailsByInspId(intInspection_Id);
                string Location="", StartDate="", ActDate = "";

                StartDate =Common.ToDateString(dt101.Rows[0]["StartDate1"]);
                ActDate = Common.ToDateString(dt101.Rows[0]["DoneDt"]);
                Location= dt101.Rows[0]["PortDone"].ToString();

                if (Location.Trim() == "" || StartDate.Trim() == "" || Location.Trim() == "")
                {
                    lblmessage.Text = "Please enter the observation details in order to close this Inspection.";
                    txt_InspValidity.Focus();
                    return;
                }

                DataTable dt54 = Inspection_TravelSchedule.SelectInspectionDetailsByInspId(intInspection_Id);
                if (dt54.Rows.Count > 0)
                {
                    strDoneDt = Common.ToDateString(dt54.Rows[0]["DoneDt"]);
                    if ((dt54.Rows[0]["FirstApprovedBy"].ToString()) != "")
                        FrstAppById = int.Parse(dt54.Rows[0]["FirstApprovedBy"].ToString());
                }
                if ((strDoneDt != "") && (txt_InspValidity.Text != ""))
                {
                    if (DateTime.Parse(strDoneDt) > DateTime.Parse(txt_InspValidity.Text))
                    {
                        lblmessage.Text = "Inspection Validity Date cannot be less than Inspection Done Date.";
                        txt_InspValidity.Focus();
                        return;
                    }
                }
                //else
                //{
                //    lblmessage.Text = "Done Date cannot be left blank.";
                //    return;
                //}

                //DataTable dt111 = Common.Execute_Procedures_Select_ByQuery("SELECT Interval FROM dbo.m_Inspection where id=" + intInspection_Id);

                if (txt_InspValidity.Text.Trim() != "")
                {
                    DataTable dt111 = Common.Execute_Procedures_Select_ByQuery("SELECT Interval FROM dbo.m_Inspection WHERE inspectiongroup=3 and Id IN (SELECT INSPECTIONID FROM dbo.T_INSPECTIONDUE T WHERE T.ID=" + intInspection_Id + ")");
                    if (dt111.Rows.Count > 0)
                    {
                        DateTime InspValidityDate = Convert.ToDateTime(hdnDoneDate.Value).AddDays(Common.CastAsInt32(dt111.Rows[0]["Interval"]));

                        DateTime InspValMax = InspValidityDate.AddDays(30);
                        DateTime InspValMin = InspValidityDate.AddDays(-30);

                        if (!(Convert.ToDateTime(txt_InspValidity.Text) >= InspValMin && Convert.ToDateTime(txt_InspValidity.Text) <= InspValMax))
                        {
                            lblmessage.Text = "Inspection Validity Date should be beteeen " + InspValMin.ToString("dd-MMM-yyyy") + " and " + InspValMax.ToString("dd-MMM-yyyy");
                            txt_InspValidity.Focus();
                            return;
                        }
                    }
                }
                
                //*****Code To Check If Inspection is Random & Inspection validity is not entered then update status as Closed
                DataTable dt78 = Inspection_Response.CheckRandomInsp(intInspection_Id);
                if (dt78.Rows.Count > 0)
                {
                    if ((dt78.Rows[0]["RandomInspection"].ToString() == "Y") && (txt_InspValidity.Text == ""))
                    {
                        if (ddl_InspCleared.SelectedValue != "NA")
                        {
                            Inspection_Response.ResponseDetails(intInspection_Id, 0, "", "", (chk_FirstApp.Checked) ? 1 : 0, (txt_FirstAppBy.Text != "") ? intLogin_Id : FrstAppById, (chk_SecApp.Checked) ? 1 : 0, (chk_SecApp.Checked) ? intLogin_Id : 0, Convert.ToInt32(ddl_InspCleared.SelectedValue), txt_InspValidity.Text, "Closed", "UPDATEAPP", "", "", "", "");
                        }
                        else
                        {
                            Inspection_Response.ResponseDetails(intInspection_Id, 0, "", "", (chk_FirstApp.Checked) ? 1 : 0, (txt_FirstAppBy.Text != "") ? intLogin_Id : FrstAppById, (chk_SecApp.Checked) ? 1 : 0, (chk_SecApp.Checked) ? intLogin_Id : 0, 0, txt_InspValidity.Text, "Closed", "RNDMINSP", "", "", "", "");
                        }
                    }
                    else
                    {
                        if (ddl_InspCleared.SelectedValue != "NA")
                        {
                            Inspection_Response.ResponseDetails(intInspection_Id, 0, "", "", (chk_FirstApp.Checked) ? 1 : 0, (txt_FirstAppBy.Text != "") ? intLogin_Id : FrstAppById, (chk_SecApp.Checked) ? 1 : 0, (chk_SecApp.Checked) ? intLogin_Id : 0, Convert.ToInt32(ddl_InspCleared.SelectedValue), txt_InspValidity.Text, "Due", "UPDATEAPP", "", "", "", "");
                        }
                        else
                        {
                            Inspection_Response.ResponseDetails(intInspection_Id, 0, "", "", (chk_FirstApp.Checked) ? 1 : 0, (txt_FirstAppBy.Text != "") ? intLogin_Id : FrstAppById, (chk_SecApp.Checked) ? 1 : 0, (chk_SecApp.Checked) ? intLogin_Id : 0, 0, txt_InspValidity.Text, "Due", "RNDMINSP", "", "", "", "");
                        }
                    }
                }
                //*******************************************************************************************************************
               // Budget.getTable("EXEC [dbo].[KPI_MANAGE_36] " + intInspection_Id.ToString());
                lblmessage.Text = "Approval Details Saved Successfully.";
                btn_SaveApp.Enabled = false;
            }
            else
            {
                DataTable dt11 = Inspection_TravelSchedule.SelectInspectionDetailsByInspId(intInspection_Id);
                if (dt11.Rows.Count > 0)
                {
                    strInsStatus = dt11.Rows[0]["Status"].ToString();
                    strDoneDt = Common.ToDateString(dt11.Rows[0]["DoneDt"]);
                    if ((dt11.Rows[0]["FirstApprovedBy"].ToString()) != "")
                        FrstAppById = int.Parse(dt11.Rows[0]["FirstApprovedBy"].ToString());
                }
                if (txt_InspValidity.Text != "")
                {
                    if (DateTime.Parse(strDoneDt) > DateTime.Parse(txt_InspValidity.Text))
                    {
                        lblmessage.Text = "Inspection Validity Date cannot be less than Inspection Done Date.";
                        txt_InspValidity.Focus();
                        return;
                    }
                }
                if (ddl_InspCleared.SelectedValue != "NA")
                {
                    Inspection_Response.ResponseDetails(intInspection_Id, 0, "", "", (chk_FirstApp.Checked) ? 1 : 0, (txt_FirstAppBy.Text != "") ? intLogin_Id : FrstAppById, (chk_SecApp.Checked) ? 1 : 0, (chk_SecApp.Checked) ? intLogin_Id : 0, Convert.ToInt32(ddl_InspCleared.SelectedValue), txt_InspValidity.Text, strInsStatus, "UPDATEAPP", "", "", "", "");
                }
                else
                {
                    Inspection_Response.ResponseDetails(intInspection_Id, 0, "", "", (chk_FirstApp.Checked) ? 1 : 0, (txt_FirstAppBy.Text != "") ? intLogin_Id : FrstAppById, (chk_SecApp.Checked) ? 1 : 0, (chk_SecApp.Checked) ? intLogin_Id : 0, 0, txt_InspValidity.Text, strInsStatus, "RNDMINSP", "", "", "", "");
                }
               // Budget.getTable("EXEC [dbo].[KPI_MANAGE_36] " + intInspection_Id.ToString());
                lblmessage.Text = "Approval Details Saved Successfully.";
            }

            
        }
        catch (Exception ex) { lblmessage.Text = ex.StackTrace.ToString(); }
    }
    public string getInspectionType()
    {
        DataTable dt111 = Common.Execute_Procedures_Select_ByQuery("select InspectionType from m_InspectionGroup g where g.id in ( SELECT InspectionGroup FROM DBO.m_Inspection m where m.Id in (SELECT inspectionid FROM DBO.T_INSPECTIONDUE i where i.Id=" + intInspection_Id + ") )");
        if (dt111.Rows.Count > 0)
            return dt111.Rows[0][0].ToString();
        else
            return "";
    }
   
     
    #endregion
    //protected void btn_SaveRemark_Click(object sender, EventArgs e)
    //{
    //    Inspection_Response.ResponseDetails(intInspection_Id, 0, "", "", 0, 0, 0, 0, 0, "", "", "CLOREMARK", "", txt_Remarks.Text.Trim(), "", "");
    //    lblmessage.Text = "Manager's Remarks Saved Successfully.";
    //}
    //protected void btn_SaveOpRem_Click(object sender, EventArgs e)
    //{
    //    Inspection_Response.ResponseDetails(intInspection_Id, 0, "", "", 0, 0, 0, 0, 0, "", "", "OPREMARK", "", "", txt_OpRemarks.Text.Trim(), "");
    //    lblmessage.Text = "Operator's Remarks Saved Successfully.";
    //}
}
