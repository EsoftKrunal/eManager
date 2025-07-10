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
using System.Text;
using Exl = Microsoft.Office.Interop.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;


public partial class Modules_HRD_CrewRecord_CrewContractHistory : System.Web.UI.Page
{
    public Authority Auth;
    public AuthenticationManager auth;
  //  public string Mode;
    DataTable dtUser;
    public bool IsSuperYear
    {
        set { ViewState["IsSuperYear"] = value; }
        get { return Convert.ToBoolean(ViewState["IsSuperYear"]); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {


            //-----------------------------
            SessionManager.SessionCheck_New();
            //-----------------------------

            lblMsgWages.Text = "";
            lblLockbyOn.Text = "";

            string SQL = "";
            ProcessCheckAuthority Obj = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
            Obj.Invoke();
            Session["Authority"] = Obj.Authority;
            Auth = Obj.Authority;
            auth = new AuthenticationManager(29, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
            if (!IsPostBack)
            {
                IsSuperYear = false;
                SQL = " Select top 1 * from dbo.UserMaster where LoginId = " + Convert.ToInt32(Session["loginid"].ToString());
                dtUser = Common.Execute_Procedures_Select_ByQuery(SQL);

                if (dtUser.Rows.Count > 0)
                {
                    if (dtUser.Rows[0]["SuperUser"] != null && dtUser.Rows[0]["SuperUser"].ToString() == "Y")
                    {
                        ViewState["IsSuperYear"] = true;
                        btnUpdateWages.Enabled = true;
                        // btnConRevision.Enabled = true;
                    }
                    else
                    {
                        ViewState["IsSuperYear"] = false;
                       

                        if (auth.IsVerify || auth.IsVerify2)
                        {
                            btnUpdateWages.Enabled = true;
                            btnConRevision.Enabled = true;
                            btnUpdateWages.CssClass = "btn";
                            btnConRevision.CssClass = "btn";
                        }
                        else
                        {
                            btnUpdateWages.Enabled = false;
                            btnConRevision.Enabled = false;
                            btnUpdateWages.CssClass = "";
                            btnConRevision.CssClass = "";
                        }
                    }
                }
            }
        }
        catch(Exception ex)
        {

        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
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
            
            
            //===========
            //if (!(IsPostBack))
            //{
            try
            {
                HiddenPK.Value = Session["CrewId"].ToString();
                if (Session["CrewId"].ToString().Trim() == "") { return; }
            }
            catch
            {
                Response.Redirect("CrewSearch.aspx");
            }
            try
            {
                HANDLE_AUTHORITY();
                BindCrewContractTemplate();
                Load_wageScale();
            }
            catch
            {
               
            }
        }
        else
        {
            tbltr.Visible = false;
            lblCrewMemberMsg.Text = "Please enter valid Crew Member Number.";
        }
    }
    private void Load_wageScale()
    {
        DataSet ds = cls_SearchReliever.getMasterData("WageScale", "WageScaleId", "WageScaleName");
        dp_wagescale.DataSource = ds.Tables[0];
        dp_wagescale.DataTextField = "WageScaleName";
        dp_wagescale.DataValueField = "WageScaleId";
        dp_wagescale.DataBind();
        dp_wagescale.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< Select >", "0"));
    }
    private void HANDLE_AUTHORITY()
    {

        lblContractTemplate.Visible = Auth.isPrint;
        ddl_ContractTemplate.Visible = Auth.isPrint;
        btn_License_Print.Visible = Auth.isPrint;
        this.hfcontractid.Value = "";
        this.trcontractdetails.Visible = false;
        Show_Contract_crew(Convert.ToInt16(HiddenPK.Value));
        pnl_history_1.Visible = true;
      //  auth = new AuthenticationManager(29, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
        if (auth.IsVerify || auth.IsVerify2)
        {
            btnUpdateWages.Enabled = true;
            btnConRevision.Enabled = true;
            btnUpdateWages.CssClass = "btn";
            btnConRevision.CssClass = "btn";
        }
        else
        {
            btnUpdateWages.Enabled = false;
            btnConRevision.Enabled = false;
            btnUpdateWages.CssClass = "";
            btnConRevision.CssClass = "";
        }
        btn_ContractSave.Visible = false;
        gv_Contract.Columns[1].Visible = (Auth.isEdit || Auth.isAdd);
    }
    private void Show_Contract_crew(int _crewid)
    {
        DataTable dt;
        dt = CrewContract.Select_Contract_Crew(_crewid);
        gv_Contract.DataSource = dt;
        gv_Contract.DataBind();
    }
    public void Show_Contract(int ContractId)
    {
        DataTable dt;
        Double d1, d2;
        d1 = 0; d2 = 0;
        double z;
        HiddenField hfd7;
        dt = CrewContract.Select_Contract_Details1(ContractId);
        if (dt.Rows.Count > 0)
        {
            ViewState["Cont_id"] = ContractId.ToString();
            lb_name.Text = dt.Rows[0]["Name"].ToString();
            lbl_RefNo.Text = dt.Rows[0]["RefNo"].ToString();
            Lb_PlanRank.Text = dt.Rows[0]["RankCode"].ToString();
            lb_PlanVessel.Text = dt.Rows[0]["VesselName"].ToString();
            Txt_ContractPeriod.Text = dt.Rows[0]["Duration"].ToString();
            //txt_issuedt.Text = dt.Rows[0]["IssueDate"].ToString();
            txt_IssueDate.Text = Alerts.FormatDate(dt.Rows[0]["IssueDate"].ToString());
            //txt_startdt.Text = dt.Rows[0]["StartDate"].ToString();
            txt_StartDate.Text = Alerts.FormatDate(dt.Rows[0]["StartDate"].ToString());
            Txt_Seniority.Text = dt.Rows[0]["Seniority"].ToString();
            //txt_Remark.Text = dt.Rows[0]["Remark"].ToString();
            //TextBox1.Text = dt.Rows[0]["Remark"].ToString();
            txt_Other_Amount.Text = dt.Rows[0]["OtherAmount"].ToString();
            txtExtraOTRate.Text = dt.Rows[0]["ExtraOTRate"].ToString();
            ddlTravelPay.SelectedValue = dt.Rows[0]["TravelPayCriteria"].ToString();
            txtRemarks.Text = dt.Rows[0]["Remark"].ToString();
            //this.txt_wagescale.Text = dt.Rows[0]["WageScaleName"].ToString();
            this.dp_wagescale.SelectedValue = dt.Rows[0]["WageScaleId"].ToString();
            this.txt_contract_rank.Text = dt.Rows[0]["NewRankCode"].ToString();
            HiddenField_RankId.Value = dt.Rows[0]["NewRankId"].ToString();
            this.txt_contact_nationality.Text = dt.Rows[0]["Nationality"].ToString();
            HiddenField_NationalityId.Value = dt.Rows[0]["NationalityId"].ToString();
            hfd7 = (HiddenField)gv_Contract.Rows[gv_Contract.SelectedIndex].FindControl("hfd_OtherAmount");
            //lbl_OtherAmount.Text = hfd7.Value;
            txt_OtherAmount.Text = hfd7.Value;
            //if (lbl_OtherAmount.Text == "")
            //{
            //    lbl_OtherAmount.Text = "0";
            //}
            if (txt_OtherAmount.Text == "")
            {
                txt_OtherAmount.Text = "0";
            }
            hfd_vesselid.Value = dt.Rows[0]["VesselId"].ToString();
            if (dt.Rows[0]["STA"].ToString() == "0")
            {
                chk_SupCert.Checked = false;
            }
            else
            {
                chk_SupCert.Checked = true;
            }
        }

        dt = CrewContract.bind_AssignedWages_Total(ContractId);
        if (dt.Rows.Count > 0)
        {
            lb_NewEarning1.Text = Math.Round((double.Parse(dt.Rows[0][0].ToString())) - double.Parse(dt.Rows[0][1].ToString()), 2).ToString();
        }

        //lb_deduction.Text = dt.Rows[0][1].ToString(); //ded


        //lb_NewEarning.Text = dt.Rows[0][0].ToString();
        //z = Math.Round(double.Parse(lb_NewEarning.Text) - double.Parse(txt_OtherAmount.Text), 2);
        //lb_Gross.Text = z.ToString(); //ear
        string currency = string.Empty;
        string sql = "Select Case when LTRIM(RTRIM(f.FlagStateName)) = 'INDIA' Then 'INR' ELSE 'USD'  END As Currency  from Vessel v with(nolock) Inner Join FlagState f with(nolock) on v.FlagStateId = f.FlagStateId where v.VesselId = " + Convert.ToInt32(hfd_vesselid.Value);

        DataTable dtcurrency = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dtcurrency != null && dtcurrency.Rows.Count > 0)
        {
            currency = dtcurrency.Rows[0]["Currency"].ToString();
        }
        dt = CrewContract.bind_AssignedWages(ContractId);
        if (dt.Rows.Count > 0)
        {
            DataView dvEarnings = new DataView(dt);
            DataView dvDeducts = new DataView(dt);
            dvEarnings.RowFilter = " ComponentType Like '%Earning%' ";
            dvDeducts.RowFilter = " ComponentType Like '%Deduction%' ";
            if (dvEarnings != null && dvEarnings.Count > 0)
            {
                Gd_AssignWages.DataSource = dvEarnings;
                Gd_AssignWages.DataBind();
                Gd_AssignWages.HeaderRow.Cells[2].Text = "Amount (" + currency + ")";
            }
            if (dvDeducts != null & dvDeducts.Count > 0)
            {
                Gd_AssignWagesDeductions.DataSource = dvDeducts;
                Gd_AssignWagesDeductions.DataBind();
                Gd_AssignWagesDeductions.HeaderRow.Cells[2].Text = "Amount (" + currency + ")";
            }
        }

        SetWagesButtonVisibility(ContractId);

    }

    public void SetWagesButtonVisibility(int ContractId)
    {
        //Contract has been closed or not
        string sql = "Select (Select FirstName+' '+LastName  from UserLogin UL where UL.LoginID=CCL.LockedBy) LockedBy,replace(convert(varchar,CCL.LockedOn,106),' ','-') LockedOn  from CrewContractLocking CCL where ContractID=" + ContractId.ToString() + " ";
        DataTable DT = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (DT.Rows.Count > 0)
        {
            btnUpdateWages.Visible = false;
            // btnConRevision.Visible = false;
            btn_ContractSave.Visible = false;
            lblLockbyOn.Text = "Locked By/On : " + DT.Rows[0]["LockedBy"].ToString() + " / " + DT.Rows[0]["LockedOn"].ToString();
        }
        //if (IsSuperYear)
        //{
        //    btnUpdateWages.Enabled = true;
        //    btnConRevision.Enabled = true; 
        //}
        //else
        //{
        //     //auth = new AuthenticationManager(29, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
        //    if (auth.IsVerify || auth.IsVerify2)
        //    {
        //        btnUpdateWages.Enabled = true;
        //        btnConRevision.Enabled = true;
        //        btnUpdateWages.CssClass = "btn";
        //        btnConRevision.CssClass = "btn";
        //    }
        //    else
        //    {
        //        btnUpdateWages.Enabled = false;
        //        btnConRevision.Enabled = false;
        //        btnUpdateWages.CssClass = "";
        //        btnConRevision.CssClass = "";
        //    }
        //}

    }
    protected void gv_Contract_PreRender(object sender, EventArgs e)
    {
        if (this.gv_Contract.Rows.Count <= 0)
        {
            this.lbl_contract_message.Text = "No Record Found";
        }
        else
        {
            this.lbl_contract_message.Text = "";
        }
    }

    protected void gv_Contract_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    protected void gv_Contract_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int Rowindx = row.RowIndex;
            //Label lblstatus = (Label)gv_Contract.Rows[Rowindx].FindControl("lblStatus");
            //if (lblstatus.Text.ToLower() == "open")
            //{

           // this.lblMessage.Text = "";
            HiddenField hfd, hfContractStauts;
            gv_Contract.SelectedIndex = Rowindx;
            hfd = (HiddenField)gv_Contract.Rows[Rowindx].FindControl("hfd_ContractId");
            hfContractStauts = (HiddenField)gv_Contract.Rows[gv_Contract.SelectedIndex].FindControl("hfContractStauts");
            if (Convert.ToString(hfContractStauts.Value.ToUpper()) == "OPEN")
            {
                if (auth.IsVerify || auth.IsVerify2)
                {
                    btnConRevision.Enabled = true;
                    btnConRevision.CssClass = "btn";
                    btnUpdateWages.Enabled = true;
                    btnUpdateWages.CssClass = "btn";
                }
                else
                {
                    btnConRevision.Enabled = false;
                    btnConRevision.CssClass = "";
                    btnUpdateWages.Enabled = false;
                    btnUpdateWages.CssClass = "";
                }

            }
            else
            {
                btnConRevision.Enabled = false;
                btnConRevision.CssClass = "";
                btnUpdateWages.Enabled = false;
                btnUpdateWages.CssClass = "";

            }
            this.hfcontractid.Value = hfd.Value;
            Show_Contract(Convert.ToInt32(hfd.Value));
            this.trcontractdetails.Visible = true;
            //txt_startdt.Enabled = true;
            //ImageButton1.Enabled = true;
            btn_ContractSave.Visible = false;// do not open  Auth.IsAdd || Auth.IsEdit; 
            SetWagesButtonVisibility(Common.CastAsInt32(hfd.Value));
            //}
            //else
            //{
            //    ProjectCommon.ShowMessage("You cannot edit this contract because it is already closed!!");
            //    return;
            //}
            //this.lblMessage.Text = "";
            //HiddenField hfd;
            //gv_Contract.SelectedIndex = Rowindx;
            //hfd = (HiddenField)gv_Contract.Rows[Rowindx].FindControl("hfd_ContractId");
            //this.hfcontractid.Value = hfd.Value;
            //Show_Contract(Convert.ToInt32(hfd.Value));
            //this.trcontractdetails.Visible = true;
            ////txt_startdt.Enabled = true;
            ////ImageButton1.Enabled = true;
            //btn_ContractSave.Visible = false;// do not open  Auth.IsAdd || Auth.IsEdit; 
            //SetWagesButtonVisibility(Common.CastAsInt32(hfd.Value));
        }
    }

    protected void gv_Contract_SelectedIndexChanged(object sender, EventArgs e)
    {
       // this.lblMessage.Text = "";
        HiddenField hfd, hfContractStauts;

        hfd = (HiddenField)gv_Contract.Rows[gv_Contract.SelectedIndex].FindControl("hfd_ContractId");

        hfContractStauts = (HiddenField)gv_Contract.Rows[gv_Contract.SelectedIndex].FindControl("hfContractStauts");
        if (Convert.ToString(hfContractStauts.Value.ToUpper()) == "OPEN")
        {
           if (auth.IsVerify || auth.IsVerify2)
            {
                btnConRevision.Enabled = true;
                btnConRevision.CssClass = "btn";
                btnUpdateWages.Enabled = true;
                btnUpdateWages.CssClass = "btn";
            }
           else
            {
                btnConRevision.Enabled = false;
                btnConRevision.CssClass = "";
                btnUpdateWages.Enabled = false;
                btnUpdateWages.CssClass = "";
            }
           
           // btnUpdateWages.
        }
        else
        {
            btnConRevision.Enabled = false;
            btnConRevision.CssClass = "";
            btnUpdateWages.Enabled = false;
            btnUpdateWages.CssClass = "";
        }
    

        this.hfcontractid.Value = hfd.Value;
        Show_Contract(Convert.ToInt32(hfd.Value));
        this.trcontractdetails.Visible = true;
    }

    public void BindCrewContractTemplate()
    {
        String sql;
        sql = "Select 0 as CCT_Id, ' < Select > ' As CCT_Name Union  Select CCT_Id, CCT_Name from CrewContract_Template with(nolock) where CCT_StatusId = 'A' order by CCT_Id ";

        DataTable dtCrewContractTemplate = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        if (dtCrewContractTemplate.Rows.Count > 0)
        {
            ddl_ContractTemplate.DataValueField = "CCT_Id";
            ddl_ContractTemplate.DataTextField = "CCT_Name";
            ddl_ContractTemplate.DataSource = dtCrewContractTemplate;
            ddl_ContractTemplate.DataBind();
        }
    }



    protected void btnConRevision_Click(object sender, EventArgs e)
    {

        lblContractRevisionmessage.Text = "";
        dvContractRevision.Visible = true;
        try
        {
            LoadCrewPromotionDetails();
        }
        catch (Exception ex)
        {
            lblContractRevisionmessage.Text = ex.Message.ToString();
        }
    }

    protected void btnUpdateWages_OnClick(object sender, EventArgs e)
    {
        try
        {
            bool flagEarn = false;
            bool flagDeduct = false;
            int contractId = 0;
            if (ddlTravelPay.SelectedValue == "0")
            {
                lblMsgWages.Text = "Please select Travel Pay Criteria.";
                ddlTravelPay.Focus();
            }


            foreach (GridViewRow Gr in Gd_AssignWages.Rows)
            {
                TextBox txtAmount = (TextBox)Gr.FindControl("txtAmount");
                HiddenField hfdCompId = (HiddenField)Gr.FindControl("hfdCompId");
                HiddenField hfContractID = (HiddenField)Gr.FindControl("hfContractID");

                Common.Set_Procedures("DBO.sp_UpdateCrewContractWages ");
                Common.Set_ParameterLength(9);
                Common.Set_Parameters(
                        new MyParameter("@CONTRACTID", hfContractID.Value),
                        new MyParameter("@WAGWSCALECOMPONENTID", hfdCompId.Value),
                        new MyParameter("@AMOUNT", Common.CastAsDecimal(txtAmount.Text)),
                        new MyParameter("@OtherAmount", Common.CastAsDecimal(txt_Other_Amount.Text)),
                        new MyParameter("@Remark", txtRemarks.Text),
                        new MyParameter("@ExtraOTRate", Common.CastAsDecimal(txtExtraOTRate.Text)),
                        new MyParameter("@TravelPayCriteria", Common.CastAsInt32(ddlTravelPay.SelectedValue)),
                        new MyParameter("@COMPONENTTYPE", "E"),
                        new MyParameter("@ModifiedBy", Convert.ToInt32(Session["LoginId"].ToString()))

                    );
                Boolean Res;
                DataSet Ds = new DataSet();
                Res = Common.Execute_Procedures_IUD(Ds);
                if (Res)
                {
                    flagEarn = true;
                    contractId = Convert.ToInt32(hfContractID.Value);
                }

            }

            foreach (GridViewRow Gr in Gd_AssignWagesDeductions.Rows)
            {
                TextBox txtAmount = (TextBox)Gr.FindControl("txtAmount");
                HiddenField hfdCompId = (HiddenField)Gr.FindControl("hfdCompId");
                HiddenField hfContractID = (HiddenField)Gr.FindControl("hfContractID");

                Common.Set_Procedures("DBO.sp_UpdateCrewContractWages ");
                Common.Set_ParameterLength(9);
                Common.Set_Parameters(
                        new MyParameter("@CONTRACTID", hfContractID.Value),
                        new MyParameter("@WAGWSCALECOMPONENTID", hfdCompId.Value),
                        new MyParameter("@AMOUNT", Common.CastAsDecimal(txtAmount.Text)),
                        new MyParameter("@OtherAmount", Common.CastAsDecimal(txt_Other_Amount.Text)),
                        new MyParameter("@Remark", txtRemarks.Text),
                        new MyParameter("@ExtraOTRate", Common.CastAsDecimal(txtExtraOTRate.Text)),
                        new MyParameter("@TravelPayCriteria", Common.CastAsInt32(ddlTravelPay.SelectedValue)),
                        new MyParameter("@COMPONENTTYPE", "D"),
                        new MyParameter("@ModifiedBy", Convert.ToInt32(Session["LoginId"].ToString()))
                    );
                Boolean Res;
                DataSet Ds = new DataSet();
                Res = Common.Execute_Procedures_IUD(Ds);
                if (Res)
                {
                    flagDeduct = true;
                    contractId = Convert.ToInt32(hfContractID.Value);
                }

            }

            //Common.Execute_Procedures_Select_ByQueryCMS("UPDATE CrewContractheader SET OtherAmount=" + Common.CastAsDecimal(txt_Other_Amount.Text)) + ",Remark ='" + txtRemarks.Text.Replace("'", "`") + "',ExtraOTRate=" + Common.CastAsDecimal(txtExtraOTRate.Text) + ",TravelPayCriteria = Case when " + Common.CastAsInt32(ddlTravelPay.SelectedValue) + "  > 0 then " + Common.CastAsInt32(ddlTravelPay.SelectedValue) + " ELSE NULL END WHERE CONTRACTID=" + Convert.ToInt32(hfContractID.Value));


            if (flagEarn && flagDeduct)
            {
                if (contractId > 0)
                {
                    Show_Contract(contractId);
                }
                lblMsgWages.Text = "Record updated successfully.";
            }
        }
        catch (Exception ex)
        {
            lblMsgWages.Text = ex.Message.ToString();
        }
    }

    protected void btn_contract_cancel_Click(object sender, EventArgs e)
    {
        this.hfcontractid.Value = "";
        this.trcontractdetails.Visible = false;
        btn_ContractSave.Visible = false;
    }

    protected void btn_License_Print_Click(object sender, EventArgs e)
    {
        if (this.hfcontractid.Value == "")
        {
            this.lbl_contract_message.Text = "Select Any Contract To Print";
        }
        else
        {
            try
            {
                if (Convert.ToInt32(ddl_ContractTemplate.SelectedValue) > 0)
                {
                    //String sql;
                    //sql = "Select CCT_PageURL from CrewContract_Template with(nolock) where CCT_Id = " + ddl_ContractTemplate.SelectedValue + "";
                    //DataTable dtCrewContractTemplate = Common.Execute_Procedures_Select_ByQueryCMS(sql);
                    //if (dtCrewContractTemplate.Rows.Count > 0)
                    //{
                    //    string ImageURL;
                    //    DataRow row = dtCrewContractTemplate.Rows[0];
                    //    ImageURL = row["CCT_PageURL"].ToString();
                    //    DataTable dt;
                    //    dt = CrewContract.Select_Contract_Details1(Convert.ToInt32(this.hfcontractid.Value.ToString()));
                    Session.Add("Cont_id", this.hfcontractid.Value.ToString());
                    //Session.Add("NewRankId", Convert.ToInt32(dt.Rows[0]["NewRankId"].ToString()));
                    //Session.Add("ContractStartDate", txt_StartDate.Text);
                    //Session.Add("ContractSeniority", Txt_Seniority.Text);
                    //Session.Add("ContractNationality", Convert.ToInt32(dt.Rows[0]["nationalityid"].ToString()));
                    //Session.Add("ContractWageScaleId", Convert.ToInt32(dt.Rows[0]["wagescaleid"].ToString()));
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "a", "window.open('../Reporting/PrintCrewContract.aspx?ContractId=" + this.hfcontractid.Value + "&TemplateId=" + ddl_ContractTemplate.SelectedValue + "');", true);
                    //Response.Redirect("PrintCrewContract.aspx?ContractId="+ this.hfcontractid.Value + "&TemplateId=" + ddl_ContractTemplate.SelectedValue);
                    //}
                }
                else
                {
                    this.lbl_contract_message.Text = "Select any Contract template for Print.";
                    ddl_ContractTemplate.Focus();
                }
            }
            catch
            {

            }
        }
    }

    protected void LoadCrewPromotionDetails()
    {
        int CrewId = int.Parse(Session["CrewId"].ToString());
        DataTable dt1 = Promotion.SelectPromotionDetailsById(CrewId);
        trContractRevision.Visible = true;
        foreach (DataRow dr in dt1.Rows)
        {
            txtPEmpNo.Text = dr["CrewNumber"].ToString();
            lblPName.Text = dr["EmpName"].ToString();
            lblPPresentRank.Text = dr["rankname"].ToString();
            lblPStatus.Text = dr["Status"].ToString();
            int status = Convert.ToInt32(dr["StatusId"].ToString());
            if (status == 3)
            {
                int RankId = int.Parse(dr["CurrentRankId"].ToString());
                //if (RankId == 1 || RankId == 12)
                //{
                //    trPromotion.Visible = false;
                //    lbl_promotion_message.Text = "Sorry ! There is no higher rank available.";
                //    return;
                //}
                lblPVessel.Text = dr["CurrentVesselId"].ToString();
                ViewState["P_Vessel"] = dr["Currentvessel_id"].ToString();
                ViewState["P_VSLType"] = dr["VesselTypeId_C"].ToString();
            }
            else
            {
                trContractRevision.Visible = false;
                lblContractRevisionmessage.Text = "Must be on board.";
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
            { lblPSignedOff.Text = Convert.ToDateTime(dr["SignedOff"].ToString()).ToString("dd-MMM-yyyy"); }
            catch
            { lblPSignedOff.Text = ""; }
            try
            { lblPAvailableDate.Text = Convert.ToDateTime(dr["AvailableDate"].ToString()).ToString("dd-MMM-yyyy"); }
            catch
            { lblPAvailableDate.Text = ""; }
            ViewState["P_CurrRank"] = dr["CurrentRankId"].ToString();
            ViewState["P_SignONDate"] = dr["SignOnDate"].ToString();
        }
    }

    protected void btn_SaveContractRevision_Click(object sender, EventArgs e)
    {
        int companyid = 1;
        //int promotionrank;
        int createdby = Convert.ToInt32(Session["loginid"].ToString());
        int modifiedby = 0;
        string crewnumber = txtPEmpNo.Text.Trim();
        //promotionrank = Convert.ToInt32(ddlPRank.SelectedValue);
        DateTime promotiondate = Convert.ToDateTime(txt_ContRevisionDt.Text);

        int vesseltypeid;
        if (ViewState["P_VSLType"].ToString() == "")
        {
            vesseltypeid = 0;
        }
        else
        {
            vesseltypeid = Convert.ToInt32(ViewState["P_VSLType"].ToString());
        }

        Promotion.InsertPromotionDetails("InsertCrewContractRevision",
                                             crewnumber,
                                             companyid,
                                             Convert.ToInt32(ViewState["P_CurrRank"].ToString()),
                                             Convert.ToInt32(ViewState["P_CurrRank"].ToString()),
                                             promotiondate,
                                             Convert.ToInt32(ViewState["P_Vessel"].ToString()),
                                             vesseltypeid,
                                             createdby,
                                             modifiedby);
        lblContractRevisionmessage.Text = "Record Successfully Saved.";
        btn_SaveContractRevision.Enabled = false;
    }

    protected void btnCloseContractRevision_Click(object sender, EventArgs e)
    {
        dvContractRevision.Visible = false;
        Show_Contract_crew(Convert.ToInt16(HiddenPK.Value));
    }

    protected void btn_close_Click(object sender, EventArgs e)
    {
        if (gv_Contract.SelectedIndex < 0)
        {
            lbl_contract_message.Text = "Plese select a Contract to Close.";
            return;
        }

        int CId, Loginid, res;
        HiddenField hfd;
        hfd = (HiddenField)gv_Contract.Rows[gv_Contract.SelectedIndex].FindControl("hfd_ContractId");
        Loginid = Convert.ToInt32(Session["loginid"].ToString());
        CId = Convert.ToInt32(hfd.Value);
        res = CrewContract.Close_Contract(CId, Loginid);
        if (res == 0)
        {
            lbl_contract_message.Text = "Contract is already closed.";
        }
        else
        {
            lbl_contract_message.Text = "Contract closed succesfully.";
        }
    }

    protected void btn_contract_Save_Click(object sender, EventArgs e)
    {
        int contractid;
        int res;
        contractid = Convert.ToInt32(this.hfcontractid.Value);

        DataTable dttable = cls_SearchReliever.Check_CrewContractDate(Convert.ToDateTime(txt_StartDate.Text), Convert.ToInt32(hfd_vesselid.Value));
        foreach (DataRow dr in dttable.Rows)
        {
            if (Convert.ToInt32(dr[0].ToString()) > 0)
            {
                lbl_contract_message.Text = "Start Date Can't be less than last Payroll Date.";
                return;
            }
        }

        try
        {
            if (Convert.ToInt32(Txt_Seniority.Text) <= 0)
            {
                lbl_contract_message.Text = "Seniotity Must be Greater Than Zero(0).";
                return;

            }
            res = Alerts.Update_ContractDetails(contractid, txt_IssueDate.Text.Trim(), txt_StartDate.Text.Trim(), Convert.ToInt32(dp_wagescale.SelectedValue), txt_OtherAmount.Text, Convert.ToInt32((chk_SupCert.Checked) ? 1 : 0), "");
            if (res == 0)
            {
                lbl_contract_message.Text = "Contract Updated Successfully.";
            }
            else
            {
                lbl_contract_message.Text = "Contract has been Closed.";
            }
            Show_Contract_crew(Convert.ToInt16(HiddenPK.Value));
        }
        catch
        {
            lbl_contract_message.Text = "Contract can't Updated.";
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

    protected void btn_CrewExt_Click(object sender, EventArgs e)
    {
        if (gv_Contract.SelectedIndex < 0)
        {
            lbl_contract_message.Text = "Plese select a Contract For Extension.";
            return;
        }
        if (gv_Contract.Rows[gv_Contract.SelectedIndex].Cells[10].Text.Trim() == "Closed")
        {
            lbl_contract_message.Text = "Plese select an Active Contract For Extension.";
            return;
        }
        int CId;
        HiddenField hfd;
        hfd = (HiddenField)gv_Contract.Rows[gv_Contract.SelectedIndex].FindControl("hfd_ContractId");
        CId = Convert.ToInt32(hfd.Value);
        Response.Redirect("../CrewOperation/SignOn.aspx?cid=" + Convert.ToInt32(HiddenPK.Value.Trim()) + "&ConId=" + CId);
    }
}