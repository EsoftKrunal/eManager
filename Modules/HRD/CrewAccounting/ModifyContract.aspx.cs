using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class ModifyContract : System.Web.UI.Page
{
    private int ContractId
    {
        get {return Common.CastAsInt32(ViewState["ContractId"]);}
        set { ViewState["ContractId"] = value; }
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
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!IsPostBack)
        {
            Load_wageScale();
            ContractId =Common.CastAsInt32(Request.QueryString["CID"]);
            Show_Contract();
        }
    }
    public void Show_Contract()
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
            txt_IssueDate.Text = Alerts.FormatDate(dt.Rows[0]["IssueDate"].ToString());
            txt_StartDate.Text = Alerts.FormatDate(dt.Rows[0]["StartDate"].ToString());
            Txt_Seniority.Text = dt.Rows[0]["Seniority"].ToString();
            txt_Other_Amount.Text = dt.Rows[0]["OtherAmount"].ToString();
            txtExtraOTRate.Text = dt.Rows[0]["ExtraOTRate"].ToString();
            ddlTravelPay.SelectedValue = dt.Rows[0]["TravelPayCriteria"].ToString();
            txtRemarks.Text = dt.Rows[0]["Remark"].ToString();
            this.dp_wagescale.SelectedValue = dt.Rows[0]["WageScaleId"].ToString();
            this.txt_contract_rank.Text = dt.Rows[0]["NewRankCode"].ToString();
            HiddenField_RankId.Value = dt.Rows[0]["NewRankId"].ToString();
            this.txt_contact_nationality.Text = dt.Rows[0]["Nationality"].ToString();
            HiddenField_NationalityId.Value = dt.Rows[0]["NationalityId"].ToString();
            txt_OtherAmount.Text = dt.Rows[0]["OtherAmount"].ToString(); ;
            if (txt_OtherAmount.Text == "")
            {
                txt_OtherAmount.Text = "0";
            }
            lblLockbyOn.Text = "";

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
        dt = CrewContract.bind_AssignedWages(ContractId);
        if (dt.Rows.Count > 0)
        {
            DataView dvEarnings = new DataView(dt);
            DataView dvDeducts = new DataView(dt);
            dvEarnings.RowFilter = " ComponentType Like '%Earning%' ";
            dvDeducts.RowFilter = " ComponentType Like '%Deduction%' ";
            Gd_AssignWages.DataSource = dvEarnings;
            Gd_AssignWages.DataBind();
            Gd_AssignWagesDeductions.DataSource = dvDeducts;
            Gd_AssignWagesDeductions.DataBind();

        }

        string sql = "Select (Select FirstName+' '+LastName  from UserLogin UL where UL.LoginID=CCL.LockedBy)LockedBy,replace(convert(varchar,CCL.LockedOn,106),' ','-')LockedOn  from CrewContractLocking CCL where ContractID=" + ContractId.ToString() + " ";
        DataTable DT = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (DT.Rows.Count > 0)
        {
            btnLock.Visible = false;
            btnUpdateWages.Visible = false;
            lblLockbyOn.Text ="Locked By/On : "+ DT.Rows[0]["LockedBy"].ToString() + " / " + DT.Rows[0]["LockedOn"].ToString();
        }
        


    }
    private void bindGd_AssignWages()
    {
        Double d1;
        DataTable dtGrid = CrewContract.bind_AssignedWages_by_StartDate(txt_StartDate.Text, Convert.ToInt32(Txt_Seniority.Text), Convert.ToInt32(HiddenField_NationalityId.Value), Convert.ToInt32(dp_wagescale.SelectedValue), Convert.ToInt32(HiddenField_RankId.Value), Convert.ToInt32((chk_SupCert.Checked) ? 1 : 0));
        Gd_AssignWages.DataSource = dtGrid;
        Gd_AssignWages.DataBind();
        if (dtGrid.Rows.Count <= 0)
        {
            return;
        }
        double earn = 0, dedc = 0, Net_earn = 0;
        foreach (DataRow dr in dtGrid.Rows)
        {
            if (dr["Componenttype"].ToString().ToUpper().Trim() == "EARNING")
            {
                earn = earn + Convert.ToDouble(dr["wagescaleComponentvalue"].ToString());
            }
            if (dr["Componenttype"].ToString().ToUpper().Trim() == "DEDUCTION")
            {
                dedc = dedc + Convert.ToDouble(dr["wagescaleComponentvalue"].ToString());
            }
        }
        d1 = 0;
        try
        {
            d1 = Convert.ToDouble(txt_OtherAmount.Text);
        }
        catch { }
        Net_earn = earn - dedc;
        Net_earn = Net_earn + d1;
    }
    protected void btn_Contract_Print_Click(object sender, EventArgs e)
    {
        DataTable dt;
        dt = CrewContract.Select_Contract_Details1(ContractId);
        Session.Add("Cont_id", ContractId.ToString() );
        Session.Add("NewRankId", Convert.ToInt32(dt.Rows[0]["NewRankId"].ToString()));
        Session.Add("ContractStartDate", txt_StartDate.Text);
        Session.Add("ContractSeniority", Txt_Seniority.Text);
        Session.Add("ContractNationality", Convert.ToInt32(dt.Rows[0]["nationalityid"].ToString()));
        Session.Add("ContractWageScaleId", Convert.ToInt32(dt.Rows[0]["wagescaleid"].ToString()));
        Response.Redirect("../Reporting/PrintContract.aspx");
    }
    protected void btnUpdateWages_OnClick(object sender, EventArgs e)
    {
        bool flagEarn = false;
        bool flagDeduct = false;
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

            }

        }
        if (flagEarn && flagDeduct)
        {
            lblMsgWages.Text = "Record updated successfully.";
        }
    }

    protected void btnLock_OnClick(object sender, EventArgs e)
    {
        string sql = "insert into CrewContractLocking(ContractID ,LockedBy,LockedOn) values(" + ContractId.ToString() + "," + Common.CastAsInt32(Session["loginid"]) + ",'"+System.DateTime.Now.ToString("dd-MMM-yyyy")+"')";
        DataTable DT = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        btnLock.Visible = false;
        btnUpdateWages.Visible = false;
        Page.RegisterStartupScript("", "alert('Contract has been closed.')");
    }
    
}
