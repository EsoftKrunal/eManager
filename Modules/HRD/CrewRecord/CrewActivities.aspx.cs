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
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;
//using System.Windows.Forms;

public partial class CrewActivities : System.Web.UI.Page
{
    Authority Auth;
    string Mode;
    //-------
    #region PAGE CONTROLS LOADER
    public void Show_Header()
    {
        ProcessSelectCrewMemberPersonalDetailsById obj = new ProcessSelectCrewMemberPersonalDetailsById();
        CrewMember Crew = new CrewMember();
        Crew.Id = Convert.ToInt32(HiddenPK.Value.Trim());
        obj.CrueMember = Crew;
        obj.Invoke();
        txt_MemberId.Text = Crew.EmpNo;
        txt_FirstName.Text = Crew.FirstName;
        txt_MiddleName.Text = Crew.MiddleName;
        txt_LastName.Text = Crew.LastName;
        ddcurrentrank.Text = Crew.Currentrank.ToString();
        txt_LastVessel.Text = Crew.LastVessel;
        txt_Status.Text = Crew.CrewStatus;
        txt_Passport.Text = Crew.PassportNo;
        lblName.Text = txt_FirstName.Text + " " + txt_MiddleName.Text + " " + txt_LastName.Text;
        lblCurrRank.Text = ddcurrentrank.Text;
        lblLastVessel.Text = txt_LastVessel.Text;
        lblStatus.Text = Crew.Status;
        lblAge.Text = Crew.Age;
    }
    //-------
    public void bindRank()
    {
        ProcessSelectRank obj = new ProcessSelectRank();
        obj.Invoke();
        ddlPRank.DataSource = obj.ResultSet.Tables[0];
        ddlPRank.DataTextField = "RankName";
        ddlPRank.DataValueField = "RankId";
        ddlPRank.DataBind();
        ddlPRank.Items.RemoveAt(0);
    }
    //-------
    private void BinddropdownReason()
    {
        DataSet ds = cls_SearchReliever.getMasterData("NTBRReason", "NTBRReasonId", "NTBRReasonName");
        ddNNTBRReason.DataSource = ds.Tables[0];
        ddNNTBRReason.DataTextField = "NTBRReasonName";
        ddNNTBRReason.DataValueField = "NTBRReasonId";
        ddNNTBRReason.DataBind();
        ddNNTBRReason.Items.RemoveAt(ddNNTBRReason.Items.Count - 1);
        ddNNTBRReason.Items.RemoveAt(ddNNTBRReason.Items.Count - 1);
        ddNNTBRReason.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    //-------
    private void BinddropdownReasonAI()
    {
        DataSet ds = cls_SearchReliever.getMasterData("NTBRReason", "NTBRReasonId", "NTBRReasonName");
        ddlAIReason.DataSource = ds.Tables[0];
        ddlAIReason.DataTextField = "NTBRReasonName";
        ddlAIReason.DataValueField = "NTBRReasonId";
        ddlAIReason.DataBind();
        ddlAIReason.Items.RemoveAt(ddlAIReason.Items.Count - 2);
        ddlAIReason.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    //-------
    public void bindddlSignOnAs(int crewid)
    {
        DataTable dt2 = SignOn.selectDataSignOnAsDetails(crewid);
        this.ddl_SignOnas.DataValueField = "rankid";
        this.ddl_SignOnas.DataTextField = "rankcode";
        this.ddl_SignOnas.DataSource = dt2;
        this.ddl_SignOnas.DataBind();
    }
    public void bindRankforExtension()
    {
        ProcessSelectRank obj = new ProcessSelectRank();
        obj.Invoke();
        ddl_SignOnas.DataSource = obj.ResultSet.Tables[0];
        ddl_SignOnas.DataTextField = "RankName";
        ddl_SignOnas.DataValueField = "RankId";
        ddl_SignOnas.DataBind();
        ddl_SignOnas.Items.RemoveAt(0);
    }
    #endregion
    //-------
    protected void Page_Load(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        //-----------------
        lbl_promotion_message.Text = "";
        lbl_ntbr_message.Text = "";
        lbl_AI_message.Text = "";
        lbl_ext_message.Text="";
        lblCrewMemberMsg.Text = "";
        //-----------------
       //l Alerts.SetHelp(imgHelp);   
        this.img_Crew.Attributes.Add("onclick", "javascript:Show_Image_Large(this);");
        
        lblMessage.Text = "";
        # region --- Authority Check ----
        try
        {
            Session["Mode"] = "Edit";
            Mode = Session["Mode"].ToString();
            ProcessCheckAuthority Obj = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
            Obj.Invoke();
            Session["Authority"] = Obj.Authority;
            Auth = (Authority)Session["Authority"];
            if (!IsPostBack)
            {
                btnActivity_Click(sender, e);
            }
            //btnChecklist.Visible = Session["loginid"].ToString() == "1";
        }
        catch { Response.Redirect("CrewSearch.aspx"); }
        #endregion
           }
    protected void Menu1_MenuItemClick(object sender, EventArgs e)
    {
        if (HiddenPK.Value == "")
        {
            tbltr.Visible = false;
            txtMemberNo.Focus();
            lblCrewMemberMsg.Text = "Please enter valid Crew Member Number and search.";
            return ;
        }
        b1.CssClass = "btn1";
        b2.CssClass = "btn1";
        b3.CssClass = "btn1";
        b4.CssClass = "btn1";
        ProcessSelectCrewMemberPersonalDetailsById obj = new ProcessSelectCrewMemberPersonalDetailsById();
        CrewMember Crew = new CrewMember();
        Crew.Id = Convert.ToInt32(HiddenPK.Value.Trim());
        obj.CrueMember = Crew;
        obj.Invoke();
        Button btn = (Button)sender;
        int i = Common.CastAsInt32(btn.CommandArgument);
        Session["Activitybtn"] = i;

        this.MultiView1.ActiveViewIndex = i;
        //for (; i < Menu1.Items.Count; i++)
        //{
        //    this.Menu1.Items[i].ImageUrl = this.Menu1.Items[i].ImageUrl.Replace("_a.gif", "_d.gif");
        //}
        //this.Menu1.Items[Int32.Parse(e.Item.Value)].ImageUrl = this.Menu1.Items[Int32.Parse(e.Item.Value)].ImageUrl.Replace("_d.gif", "_a.gif");
        if (i == 0)
        {
            b1.CssClass = "selbtn";
            HANDLE_AUTHORITY(0);
            //--------- TAB - 1
            txtPEmpNo.Text = Crew.EmpNo;
            txtPEmpNo.Enabled = false;
            LoadCrewPromotionDetails();
        }
        if (i == 1)
        {
            b2.CssClass = "selbtn";
            HANDLE_AUTHORITY(1);
            //--------- TAB - 2
            txtNEmpNo.Text = Crew.EmpNo;
            txtNEmpNo.Enabled = false;
            LoadCrewDetailsNTBR();
        }
        if (i == 2)
        {
            b3.CssClass = "selbtn";
            HANDLE_AUTHORITY(2);
            //--------- TAB - 3
            txtAIEmpNo.Text = Crew.EmpNo;
            txtAIEmpNo.Enabled = false;
            LoadCrewDetailsActive();
        }
        if (i == 3)
        {
            b4.CssClass = "selbtn";
            HANDLE_AUTHORITY(3);
            tdExt.Visible = false;   
            //--------- TAB - 4
            DataTable dt = Budget.getTable("select contractid from CrewPersonalDetails where crewid=" + Crew.Id.ToString()).Tables[0];
            if (dt.Rows.Count > 0)
            {
                int OpneContractid = int.Parse("0" + dt.Rows[0][0].ToString());
                if (OpneContractid > 0)
                {
                    Show_Record_For_Extension(Crew.Id, OpneContractid);
                    tdExt.Visible = true;
                }
                else
                {
                    lbl_ext_message.Text = "Must be on board.";
                    btn_early.Enabled = false;
                    btn_save_ext.Enabled = false;
                }
            }
            else
            {
                lbl_ext_message.Text = "Must be on board.";
                btn_early.Enabled = false;
                btn_save_ext.Enabled = false;
            }
            ShowActivity();
        }
    }
    private void HANDLE_AUTHORITY(int index)
    {
        if (Mode == "New")
        {
            //TAB-1 Promotion
            if (index == 0)
            {
                btn_SavePromotion.Visible = (Auth.isEdit || Auth.isAdd);
            }
            //TAB-2 NTBR/De-NTBR
            if (index == 1)
            {
                btn_save_Ntbr.Visible = (Auth.isEdit || Auth.isAdd);
            }
            //TAB-3 ACADEMIC DETAILS
            if (index == 2)
            {
                btnSaveAI.Visible = (Auth.isEdit || Auth.isAdd);
            }
            //TAB-4 History Tab
            if (index == 3)
            {
                btn_save_ext.Visible = (Auth.isEdit || Auth.isAdd);
                btn_early.Visible = (Auth.isEdit || Auth.isAdd);
            }
        }
        else if (Mode == "Edit")
        {
            //TAB-1 Promotion
            if (index == 0)
            {
                btn_SavePromotion.Visible = (Auth.isEdit || Auth.isAdd);
            }
            //TAB-2 NTBR/De-NTBR
            if (index == 1)
            {
                btn_save_Ntbr.Visible = (Auth.isEdit || Auth.isAdd);
            }
            //TAB-3 ACADEMIC DETAILS
            if (index == 2)
            {
                btnSaveAI.Visible = (Auth.isEdit || Auth.isAdd);
            }
            //TAB-4 History Tab
            if (index == 3)
            {
                btn_save_ext.Visible = (Auth.isEdit || Auth.isAdd);
                btn_early.Visible = (Auth.isEdit || Auth.isAdd);
            }
        }
        else
        {
            //TAB-1 Promotion
            if (index == 0)
            {
                btn_SavePromotion.Visible = false;
            }
            //TAB-2 NTBR/De-NTBR
            if (index == 1)
            {
                btn_save_Ntbr.Visible = false;
            }
            //TAB-3 ACADEMIC DETAILS
            if (index == 2)
            {
                btnSaveAI.Visible = false;
            }
            //TAB-4 History Tab
            if (index == 3)
            {
                btn_save_ext.Visible = false; 
                btn_early.Visible = false;

            }
        }
    }
    //--------
    #region Tab-1 Promotion Details
    protected void LoadCrewPromotionDetails()
    {
        int CrewId = int.Parse(Session["CrewId"].ToString());
        DataTable dt1 = Promotion.SelectPromotionDetailsById(CrewId);
        trPromotion.Visible=true; 
        foreach (DataRow dr in dt1.Rows)
        {
            txtPEmpNo.Text = dr["CrewNumber"].ToString();
            lblPName.Text = dr["EmpName"].ToString();
            lblPPresentRank.Text = dr["rankname"].ToString();
            lblPStatus.Text = dr["Status"].ToString();
            int status=Convert.ToInt32(dr["StatusId"].ToString());
            if (status == 3)
            {
                int RankId = int.Parse(dr["CurrentRankId"].ToString());
                if (RankId == 1 || RankId == 12)
                { 
                trPromotion.Visible = false;
                lbl_promotion_message.Text = "Sorry ! There is no higher rank available.";
                return;
                }
                lblPVessel.Text = dr["CurrentVesselId"].ToString();
                ViewState["P_Vessel"] = dr["Currentvessel_id"].ToString();
                ViewState["P_VSLType"] = dr["VesselTypeId_C"].ToString();
            }
            else
            {
                trPromotion.Visible = false;
                lbl_promotion_message.Text = "Must be on board.";
                return;
            }
            //------------------
            if (status == 1 || status == 2)
            {
                lblPVessel.Text = dr["LastVesselId"].ToString();
                ViewState["P_Vessel"] = dr["LastVessel_Id"].ToString();
                ViewState["P_VSLType"] = dr["VesselTypeId_L"].ToString();
            }
            try
            {lblPSignedOff.Text = Convert.ToDateTime(dr["SignedOff"].ToString()).ToString("dd-MMM-yyyy");}
            catch
            { lblPSignedOff.Text = ""; }
            try
            {lblPAvailableDate.Text = Convert.ToDateTime(dr["AvailableDate"].ToString()).ToString("dd-MMM-yyyy");}
            catch
            { lblPAvailableDate.Text = ""; }
            ViewState["P_CurrRank"] = dr["CurrentRankId"].ToString();
            ViewState["P_SignONDate"] = dr["SignOnDate"].ToString();
        }
    }
    protected void btn_SavePromotion_Click(object sender, EventArgs e)
    {
        int companyid = 1;
        int promotionrank;
        int createdby = Convert.ToInt32(Session["loginid"].ToString());
        int modifiedby = 0;
        string crewnumber = txtPEmpNo.Text.Trim();
        promotionrank = Convert.ToInt32(ddlPRank.SelectedValue);
        DateTime promotiondate = Convert.ToDateTime(txt_PPromotionDt.Text);
        // ------------------ CHECKING PROMOTION IS POSSIBLE OR NOT
        DataTable dttable = Promotion.Chk_ContractLicense(txtPEmpNo.Text.Trim(), Convert.ToInt32(ViewState["P_CurrRank"].ToString()), Convert.ToInt32(ddlPRank.SelectedValue));
        foreach (DataRow dr in dttable.Rows)
        {
            if (Convert.ToInt32(dr[0].ToString()) > 0)
            {
                lbl_promotion_message.Text = "Do not Possess Adequate Licenses.";
                return;
            }
        }
        //--------------------------------------------------------------
        int vesseltypeid;
        if (ViewState["P_VSLType"].ToString() == "")
        {
            vesseltypeid = 0;
        }
        else
        {
            vesseltypeid = Convert.ToInt32(ViewState["P_VSLType"].ToString());
        }

        Promotion.InsertPromotionDetails("InsertPromotionDetails",
                                             crewnumber,
                                             companyid,
                                             Convert.ToInt32(ViewState["P_CurrRank"].ToString()),
                                             promotionrank,
                                             promotiondate,
                                             Convert.ToInt32(ViewState["P_Vessel"].ToString()),
                                             vesseltypeid,
                                             createdby,
                                             modifiedby);
        lbl_promotion_message.Text = "Record Successfully Saved.";
        btn_SavePromotion.Enabled = false;
    }
    private void cleardata()
    {
        lblPName.Text = "";
        lblPPresentRank.Text = "";
        lblPStatus.Text = "";
        lblPVessel.Text = "";
        lblPSignedOff.Text = "";
        lblPAvailableDate.Text = "";
        txt_PPromotionDt.Text = "";
        txtPEmpNo.Focus();
        btn_SavePromotion.Enabled = false;
    }
    #endregion
    //--------
    #region Tab-2 NTBR/De-NTBR 
    protected void LoadCrewDetailsNTBR()
    {
        DataTable dt = CrewNTBRDetails.selectNTBRDetailsByEmpNo(Convert.ToString(txtNEmpNo.Text));
        if (dt.Rows.Count > 0)
        {
            btn_save_Ntbr.Enabled = true && Auth.isAdd;
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToInt32(dr["CrewStatusId"].ToString()) == 3)
                {
                    lbl_ntbr_message.Text = "Crew Member is On Board.";
                    btn_save_Ntbr.Enabled = false;
                    return;
                }
                Session["CrewID_Planning"] = dr["Crewid"].ToString();
                txtNName.Text = dr["CrewName"].ToString();
                txtNNationality.Text = dr["nationality"].ToString();
                txtNPresentRank.Text = dr["rankname"].ToString();
                txt_LastVessel.Text = dr["VesselName"].ToString();
                try
                {
                    txtNSignedOff.Text = Convert.ToDateTime(dr["SignOffDate"].ToString()).ToString("dd-MMM-yyyy");
                }
                catch { txtNSignedOff.Text = ""; }
                try
                {
                    txtNAvailableDate.Text = Convert.ToDateTime(dr["AvailableFrom"].ToString()).ToString("dd-MMM-yyyy");
                }
                catch { txtNAvailableDate.Text = ""; }
                try
                {
                    txtNNTBRDate.Text = Convert.ToDateTime(dr["NTBRDate"].ToString()).ToString("dd-MMM-yyyy");
                }
                catch { txtNNTBRDate.Text = ""; }

                ddNNTBRReason.SelectedValue = dr["NTBRReasonid"].ToString();
                txtNRemarks.Text = dr["remarks"].ToString();
                if (dr["NTBRFlag"].ToString() == "N")
                {
                    ddlNNTBR.SelectedValue = Convert.ToString(1);
                }
                else if (dr["NTBRFlag"].ToString() == "D")
                {
                    ddNNTBRReason.Enabled = false;
                    RangeValidator2.Enabled = false;
                    ddlNNTBR.SelectedValue = Convert.ToString(2);
                }
            }
        }
        else
        {
            lbl_ntbr_message.Text = "Crew Member does not exist";
            callclearNTBR();
            btn_save_Ntbr.Enabled = false;
        }
    }
    protected void ddntbr_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlNNTBR.SelectedValue) == 1)
        {
            ddNNTBRReason.Enabled = true;
            RangeValidator2.Enabled = true;
        }
        else if (Convert.ToInt32(ddlNNTBR.SelectedValue) == 2)
        {
            ddNNTBRReason.SelectedValue = Convert.ToString(0);
            ddNNTBRReason.Enabled = false;
            RangeValidator2.Enabled = false;
        }
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        char NorD;
        int crewId = 0;
        try
        {
            crewId = Convert.ToInt32(Session["CrewID_Planning"].ToString());
        }
        catch
        {

        }
        string Ntbrdate = txtNNTBRDate.Text;
        int Ntbrreasonid = Convert.ToInt32(ddNNTBRReason.SelectedValue);
        int login_id = Convert.ToInt32(Session["loginid"].ToString());
        if (Convert.ToInt32(ddlNNTBR.SelectedValue) == 1)
        {
            NorD = 'N';
        }
        else
        {
            NorD = 'D';
            Ntbrreasonid = 0;
        }
        string remarks = txtNRemarks.Text;
        try
        {
            CrewNTBRDetails.insertUpdateCrewNTBRDetails("InsertUpdateCrewNTBRDetailsByCrewId",
                                                              crewId,
                                                              Ntbrdate,
                                                              Ntbrreasonid,
                                                              remarks,
                                                              NorD,
                                                              login_id);
            lbl_ntbr_message.Text = "Record Saved Successfully.";
        }
        catch
        {
            lbl_ntbr_message.Text = "Record Not Saved.";
        }
    }
    private void callclearNTBR()
    {
        txtNEmpNo.Text = "";
        txtNName.Text = "";
        txtNNationality.Text = "";
        txtNPresentRank.Text = "";
        txt_LastVessel.Text = "";
        txtNSignedOff.Text = "";
        txtNAvailableDate.Text = "";
        txtNNTBRDate.Text = "";
        ddNNTBRReason.SelectedIndex = 0;
        ddlNNTBR.SelectedIndex = 0;
        txtNRemarks.Text = "";
        txtNEmpNo.Focus();
        btn_save_Ntbr.Enabled = false;
    }
    #endregion
    //--------
    #region Tab-2 Active/InActive
    protected void LoadCrewDetailsActive()
    {
        DataTable dt = CrewNTBRDetails.selectNTBRDetailsByEmpNo(txtAIEmpNo.Text);
        if (dt.Rows.Count > 0)
        {
            btnSaveAI.Enabled = true && Auth.isAdd;
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToInt32(dr["CrewStatusId"].ToString()) == 3)
                {
                   lbl_AI_message.Text = "Crew Member is On Board.";
                   btnSaveAI.Enabled = false;
                   return;
                }
                Session["CrewID_Planning"] = dr["Crewid"].ToString();
                txtAEmpName.Text = dr["CrewName"].ToString();
                txtANationality.Text = dr["nationality"].ToString();
                txtAPresentRank.Text = dr["rankname"].ToString();
                txtALastVsl.Text = dr["VesselName"].ToString();
                try
                {
                    txtASignedOff.Text = Convert.ToDateTime(dr["SignOffDate"].ToString()).ToString("dd-MMM-yyyy");
                }
                catch { txtNSignedOff.Text = ""; }
                try
                {
                    txtAAvlDate.Text = Convert.ToDateTime(dr["AvailableFrom"].ToString()).ToString("dd-MMM-yyyy");
                }
                catch { txtAAvlDate.Text = ""; }

                int sid = int.Parse(dr["CrewStatusId"].ToString());
                lblALastStatus.Text = (sid == 1) ? "New" : (sid == 2) ? "On Leave" : (sid == 3) ? "On Board" : (sid == 4) ? "In Active" : "NTBR";
                DataTable dt1 = Budget.getTable("Select Top 1 * From CrewInActiveDetails Where CrewId In (select crewid from crewpersonaldetails cpd where cpd.crewnumber='" + txtAIEmpNo.Text + "') Order By InActiveid Desc").Tables[0];
                if (dt1 == null || dt1.Rows.Count <= 0)
                {
                    txtAIDate.Text = "";
                    ddlAIReason.SelectedIndex = 0;
                }
                else
                {
                    txtAIDate.Text = Convert.ToDateTime(dt1.Rows[0]["InActiveDate"].ToString()).ToString("dd-MMM-yyyy");
                    ddlAIReason.SelectedValue = dt1.Rows[0]["InActiveReasonid"].ToString();
                }
                lblALastReason.Text = (ddlAIReason.SelectedIndex == 0) ? "" : ddlAIReason.SelectedItem.Text;
                if (sid == 4)
                {
                    ddlAIReason.SelectedValue = "2";
                }
                else
                {
                    ddlAIReason.SelectedValue = "4";
                }
                ddlAIReason.Enabled = false;
                ddlAIReason.SelectedIndex = 0;
            }
        }
        else
        {
            lbl_AI_message.Visible = true;
            lbl_AI_message.Text = "Crew Member does not exist";
            callclearActive();
            btnSaveAI.Enabled = false;
        }
    }
    protected void ddlANewStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlANewStatus.SelectedValue) == 4)
        {
            ddlAIReason.Enabled = true;
            RangeValidator4.Enabled = true;
        }
        else if (Convert.ToInt32(ddlANewStatus.SelectedValue) == 2)
        {
            ddlAIReason.SelectedValue = Convert.ToString(0);
            ddlAIReason.Enabled = false;
            RangeValidator4.Enabled = false;
        }
    }
    protected void btn_saveAI_Click(object sender, EventArgs e)
    {
        int crewId = int.Parse(HiddenPK.Value);
        string Adate = txtAIDate.Text;
       
        if (DateTime.Parse(Adate) > DateTime.Today)
        {
            lbl_AI_message.Text = "Inactive date must be less or equal today.";
            return;
        }
        if (ddlANewStatus.SelectedIndex!=2)
        {
            if (ddlAIReason.SelectedIndex <= 0)
            {
                lbl_AI_message.Text = "Please select a reason for Inactive Crew.";
                return;
            }
        }

        int AIreasonid = Convert.ToInt32(ddlAIReason.SelectedValue);
        int login_id = Convert.ToInt32(Session["loginid"].ToString());
        string remarks = txtARemarks.Text;
        try
        {
            Budget.getTable("exec InsertUpdateCrewInactiveDetails " + crewId.ToString() + ",'" + Adate + "'," + ddlANewStatus.SelectedValue + "," + AIreasonid + ",'" + remarks + "'," + login_id);
            lbl_AI_message.Text = "Record Saved Successfully.";
        }
        catch
        {
            lbl_AI_message.Text = "Record Not Saved.";
        }
    }
    private void callclearActive()
    {
        txtAIEmpNo.Text = "";
        txtAEmpName.Text = "";
        txtANationality.Text = "";
        txtAPresentRank.Text = "";
        txtALastVsl.Text = "";
        txtASignedOff.Text = "";
        txtAAvlDate.Text = "";
        txtAIDate.Text = "";
        ddlAIReason.SelectedIndex = 0;
        txtARemarks.Text = "";
        txtAIEmpNo.Focus();
        btnSaveAI.Enabled = false;
    }

    #endregion
    //--------
    #region Tab-2 Crew Extension
    
    public void ShowActivity()
    {
        int CrewId = int.Parse(Session["CrewId"].ToString());
        rptCrewActivity.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM vw_crewactivityhistory WHERE CREWID=" + CrewId + " Order by TableId Desc");
        rptCrewActivity.DataBind();
    }
    protected void Show_Record_For_Extension(int CrewId, int ContractID)
    {
        DataTable dt1 = SignOn.getSignOnDetailsForExtension(CrewId);
        if (dt1.Rows.Count > 0)
        {
            foreach (DataRow dr in dt1.Rows)
            {
                //---------Rank
                ddl_SignOnas.SelectedValue = dr["CurrentRankId"].ToString();
                try
                {
                    txt_SignOnDate.Text = Convert.ToDateTime(dr["SignOnDate"].ToString()).ToString("dd-MMM-yyyy");
                }
                catch { txt_SignOnDate.Text = ""; }
                txt_Duration.Text = dr["DurationInMonths"].ToString();
                txt_ReliefDate.Text = Convert.ToDateTime(dr["ReliefDueDate"].ToString()).ToString("dd-MMM-yyyy");
                txt_Remarks.Text = dr["Remarks"].ToString();
                lbl_UpdatedBy.Text = dr["ExtUser"].ToString();
                try
                {
                    lbl_UpdatedOn.Text = Convert.ToDateTime(dr["ExtDate"].ToString()).ToString("dd-MMM-yyyy");
                }
                catch { lbl_UpdatedOn.Text = ""; }
                //---------------------------------------------------
                ddl_SignOnas.Enabled = false;
                txt_SignOnDate.Enabled = false;
                txt_Duration.Enabled = false;
                txt_ReliefDate.Enabled = true;
                txt_ReliefDate.ReadOnly = false;
                CalendarExtender1.Enabled = true;
                imgSignOffDate.Visible = true;
                ViewState["Conid"] = ContractID;
            }
        }
    }
    protected void txt_Duration_TextChanged(object sender, EventArgs e)
    {
        DateTime rdate;
        try
        {
            rdate = Convert.ToDateTime(txt_SignOnDate.Text);
            rdate = rdate.AddMonths(Convert.ToInt32(txt_Duration.Text));
            txt_ReliefDate.Text = rdate.ToString("dd-MMM-yyyy");
        }
        catch { }
    }
    protected void btn_Extend_Click(object sender, EventArgs e)
    {
        try
        {
            int CrewId = int.Parse(Session["CrewId"].ToString());
            Common.Execute_Procedures_Select_ByQueryCMS("InserCrewactivityhistory " + CrewId + ",'" + txt_ReliefDate.Text + "','X','" + txt_Remarks.Text.Trim().Replace("'", "`") + "','" + Session["UserName"].ToString() + "'");
            SignOn.UpdateSignOnDetailsForExtension(HiddenPK.Value, ViewState["Conid"].ToString(), Session["loginid"].ToString(), txt_ReliefDate.Text, txt_Remarks.Text.Trim());
        }
        catch { lbl_ext_message.Text = "Extension Not Completed "; }
        lbl_ext_message.Text = "Extension Completed Successfully.";
    }
    protected void radCheck_OnCheckedChanged(object sender, EventArgs e)
    {
        tabExtension.Visible = radExtension.Checked;
        tabEarly.Visible = radEarly.Checked;
    }
    
    #endregion
    //--------
    protected void imgbtn_Personal_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("CrewDetails.aspx");
    }
    protected void imgbtn_Document_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("CrewDocument.aspx");
    }
    protected void imgbtn_CRM_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("CrewCRMDocument.aspx");
    }
    protected void btn_EarlyRelief_Click(object sender, EventArgs e)
    {
        DateTime dt = new DateTime();
        if (DateTime.TryParse(txtExpectedReliefDt.Text.Trim(), out dt))
        {
            if(dt<DateTime.Today)
            {
                lblmsg.Text = "Sorry ! expected relief date can not less than today date.";
                return;
            }
            int CrewId = int.Parse(Session["CrewId"].ToString());
            string sql = "SELECT RELIEFDUEDATE FROM DBO.CREWPERSONALDETAILS WHERE CREWID=" + CrewId;
            DataTable rdate = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            DateTime CurrentRDate;
            if (DateTime.TryParse(Convert.ToString(rdate.Rows[0][0]), out CurrentRDate))
            {
                if(dt>CurrentRDate)
                {
                    lblmsg.Text = "Early relief is only allow to reduce the relief due date. Current relief due dt. is " + Common.ToDateString(CurrentRDate);
                    return;
                }
                else
                {
                    Common.Execute_Procedures_Select_ByQueryCMS("InserCrewactivityhistory " + CrewId + ",'" + Common.ToDateString(dt) + "','R','" + txtERRemarks.Text.Trim().Replace("'","`") + "','" + Session["UserName"].ToString() + "'");
                    sql = "UPDATE DBO.CREWPERSONALDETAILS SET RELIEFDUEDATE='" + Common.ToDateString(dt) + "' WHERE CREWID=" + CrewId;
                    Common.Execute_Procedures_Select_ByQueryCMS(sql);
                    lblmsg.Text = "Expected relief date is updated successfully.";
                }
            }
        }
        else
        {
            lblmsg.Text = "Please enter the expected relief date.";
            return;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        lbl_promotion_message.Text = "";
        lbl_ntbr_message.Text = "";
        lbl_AI_message.Text = "";
        lbl_ext_message.Text = "";
        lblMessage.Text = "";
        lblCrewMemberMsg.Text = "";
        Session["CrewId"] = null;
        if (txtMemberNo.Text.Trim() == "")
        {
            return;
        }
        ProcessSelectCrewMemberPrimaryDetails_Search obj = new ProcessSelectCrewMemberPrimaryDetails_Search();
        BoCrewSearch Member = new BoCrewSearch();
        Member.LoginId = Convert.ToInt32(Session["loginid"].ToString());
        Member.CrewNumber = txtMemberNo.Text;
        Member.FirstName = "";
        //Member.LastName = "";
        Member.Nationality = 0;
        Member.CrewStatusId = 0;
        Member.Rank = 0;
        Member.PassportNo = "";
        Member.RecOffId = 0;

        Member.FromDate = "";
        Member.ToDate = "";

        Member.VesselId = 0;
        Member.VesselType = 0;
        Member.Owner = 0;
        // Member.ReliefDue = txt_ReliefDue.Text;  

        Member.USvisa = 0;
        Member.SchengenVisa = 0;
        Member.FamilyMember = 0;

        Member.AgeFrom = 0;
        Member.AgeTo = 0;
        Member.ExpFrom = 0;
        Member.ExpTo = 0;
        obj.CrewSearch = Member;
        obj.Invoke();
        if (obj.ResultSet.Tables[0].Rows.Count > 0)
        {
            Session["CrewId"] = obj.ResultSet.Tables[0].Rows[0]["CrewId"].ToString();
        }

        if (Session["CrewId"] != null)
        {
            tbltr.Visible = true;
            //=========== Code for Checking the Critical Remark
            #region --- Critical Remarks ----
            try
            {
                DataTable dtcrtrmk = Alerts.getCriticalRemarkofCrewMember(Session["CrewId"].ToString());
                if (dtcrtrmk.Rows.Count > 0)
                {
                    lnkPopUp.Visible = true;
                }
                else
                {
                    lnkPopUp.Visible = false;
                }
            }
            catch
            {
            }
            try
            {
                DataTable dtalert2 = Alerts.getDocumentAlertofCrewMember(Session["CrewId"].ToString());
                if (dtalert2.Rows.Count > 0)
                {
                    doc_Alert.Text = "Alert";
                }
            }
            catch
            {
                doc_Alert.Text = "";
            }
            #endregion
            //===========
            //if (!(IsPostBack))
            //{
            try
            {
                HiddenPK.Value = Session["CrewId"].ToString();
                if (Session["CrewId"].ToString().Trim() == "") { Response.Redirect("CrewSearch.aspx"); }
            }
            catch
            {
                Response.Redirect("CrewSearch.aspx");
            }
            try
            {

                #region --- Load Header  & Crew Image ----
                // -------------- Header
                Show_Header();
                // -------------- Crew Image 
                ProcessSelectCrewMemberPersonalDetailsById objsearch = new ProcessSelectCrewMemberPersonalDetailsById();
                CrewMember Crew = new CrewMember();
                Crew.Id = Convert.ToInt32(HiddenPK.Value.Trim());
                objsearch.CrueMember = Crew;
                objsearch.Invoke();
                UtilityManager um = new UtilityManager();
                if (Crew.Photopath.Trim() == "")
                { img_Crew.ImageUrl = um.DownloadFileFromServer("noimage.jpg", "C"); }
                else
                { img_Crew.ImageUrl = um.DownloadFileFromServer(Crew.Photopath, "C"); }
                #endregion
                bindRank();
                BinddropdownReason();
                BinddropdownReasonAI();
                bindRankforExtension();
                MultiView1.ActiveViewIndex = 0;
                HANDLE_AUTHORITY(0);
                //--------- TAB - 1
                txtPEmpNo.Text = Crew.EmpNo;
                txtPEmpNo.Enabled = false;
                LoadCrewPromotionDetails();
                Session["Activitybtn"] = "0";
                if (Crew.CrewStatus == "New")
                {
                    lblMessMain.Text = "Sorry ! you can not perform any activity on this crew due to its `New' Status.";
                    tdWhole.Visible = false;
                    return;
                }

                //}
                b1.CssClass = "btn1";
                b2.CssClass = "btn1";
                b3.CssClass = "btn1";
                b4.CssClass = "btn1";
                if (Session["Activitybtn"] == null)
                {
                    Session["Activitybtn"] = 0;
                }
                switch (Common.CastAsInt32(Session["Activitybtn"]))
                {
                    case 0:
                        b1.CssClass = "selbtn";
                        break;
                    case 1:
                        b2.CssClass = "selbtn";
                        break;
                    case 2:
                        b3.CssClass = "selbtn";
                        break;
                    case 3:
                        b4.CssClass = "selbtn";
                        break;
                    default:
                        break;
                }
            }
            catch
            {
                Response.Redirect("CrewSearch.aspx");
            }
        }
        else
        {
            tbltr.Visible = false;
            lblCrewMemberMsg.Text = "Please enter valid Crew Member Number.";
        }

        
    }

    protected void txtMemberNo_TextChanged(object sender, EventArgs e)
    {
        if (txtMemberNo.Text.Length == 0)
        {
            tbltr.Visible = false;
            Session["CrewId"] = null;
        }
    }

    protected void btnCrewDocs_Click(object sender, EventArgs e)
    {
        btnActivity.CssClass = "btn1";
        btnCrewDocuments.CssClass = "selbtn";
        btnCheckListMaster.CssClass = "btn1";
        btnCrewContractHistory.CssClass = "btn1";
        tblActivity.Visible = false;
        divfrm.Visible = true;

        frm.Attributes.Add("src", "~/Modules/HRD/CrewApproval/CrewDocuments.aspx");
    }

    protected void btnCheckListMaster_Click(object sender, EventArgs e)
    {
        btnActivity.CssClass = "btn1";
        btnCrewDocuments.CssClass = "btn1";
        btnCheckListMaster.CssClass = "selbtn";
        btnCrewContractHistory.CssClass = "btn1";
        tblActivity.Visible = false;
        divfrm.Visible = true;

        frm.Attributes.Add("src", "~/Modules/HRD/CrewApproval/CheckList.aspx");
    }


    //protected void btnChecklist_Click(object sender, EventArgs e)
    //{
    //    btnActivity.CssClass = "btn1";
    //    btnCrewDocuments.CssClass = "btn1";
    //    btnChecklist.CssClass = "selbtn";
    //    tblActivity.Visible = false;
    //    divfrm.Visible = true;

    //    frm.Attributes.Add("src", "~/Modules/HRD/CrewApproval/CheckList.aspx");
    //}

    protected void btnActivity_Click(object sender, EventArgs e)
    {
        btnActivity.CssClass = "selbtn";
        btnCrewDocuments.CssClass = "btn1";
        btnCheckListMaster.CssClass = "btn1";
        btnCrewContractHistory.CssClass = "btn1";
        tblActivity.Visible = true;
        tbltr.Visible = false;
        divfrm.Visible = false;   
    }

    protected void btnCrewContractHistory_Click(object sender, EventArgs e)
    {
        btnActivity.CssClass = "btn1";
        btnCrewDocuments.CssClass = "btn1";
        btnCheckListMaster.CssClass = "btn1";
        btnCrewContractHistory.CssClass = "selbtn";
        tblActivity.Visible = false;
        divfrm.Visible = true;

        frm.Attributes.Add("src", "~/Modules/HRD/CrewRecord/CrewContractHistory.aspx");
    }
}