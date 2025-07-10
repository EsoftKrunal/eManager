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
using System.IO;
using Ionic.Zip;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.Activities.Expressions;

/// <summary>
/// Page Name            : ModifyVendorProfile.aspx
/// Purpose              : Editing vendor Request
/// Author               : Laxmi Verma
/// Developed on         : 24 September 2015
/// </summary>


public partial class Docket_ModifyVendorProfile : System.Web.UI.Page
{
    Random r = new Random();
    public int RequestId
    {
        get { return Common.CastAsInt32(ViewState["RequestId"]); }
        set { ViewState["RequestId"] = value; }
    }
    //by laxmi on 18-oct-2015
    public int VenSortBy
    {
        get { return Convert.ToInt32("0" + ViewState["VenSortBy"]); }
        set { ViewState["VenSortBy"] = value.ToString(); }
    }
    public int PortSortBy
    {
        get { return Convert.ToInt32("0" + ViewState["PortSortBy"]); }
        set { ViewState["PortSortBy"] = value.ToString(); }
    }
    public int SecondedTo
    {
        get { return Convert.ToInt32("0" + ViewState["SecondedTo"]); }
        set { ViewState["SecondedTo"] = value.ToString(); }
    }
    public int SelectedPoId
    {
        get { return Convert.ToInt32("0" + hfSID.Value); }
        set { hfSID.Value = value.ToString(); }
    }
    public int RequestAprovalStatus
    {
        get { return Common.CastAsInt32(ViewState["RequestAprovalStatus"]); }
        set { ViewState["RequestAprovalStatus"] = value; }
    }
    public int PageNo
    {
        get { return Common.CastAsInt32(ViewState["PageNo"]); }
        set { ViewState["PageNo"] = value; }
    }
    public int ApprovalStageDone
    {
        get { return Common.CastAsInt32(ViewState["ApprovalStageDone"]); }
        set { ViewState["ApprovalStageDone"] = value; }
    }

    //public int VRID_Edit
    //{
    //    get { return Common.CastAsInt32(ViewState["VRID_Edit"]); }
    //    set { ViewState["VRID_Edit"] = value; }
    //}
    #region Declarations
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    #endregion

    //-------------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------

        ClearMessage();
        if (!IsPostBack)
        {
            int VRID_Edit = Common.CastAsInt32(Request.QueryString["KeyId"].ToString());
            frmdoc.Attributes.Add("src", "VendorDocuments.aspx?KeyId=" + VRID_Edit);
            
            string TemplateDBName = "CV4";


            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT VRID FROM dbo.tbl_VenderRequest WHERE VRID='" + VRID_Edit + "'");
            if (dt != null)
            {
                chkFleets.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.FLEETMASTER ORDER BY FLEETNAME");
                chkFleets.DataTextField = "FleetName";
                chkFleets.DataValueField = "FleetId";
                chkFleets.DataBind();

                chkVessels.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.VESSEL WHERE VESSELSTATUSID=1  ORDER BY VESSELNAME");
                chkVessels.DataTextField = "VesselName";
                chkVessels.DataValueField = "VesselId";
                chkVessels.DataBind();

                DataTable dtCountry = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.COUNTRY ORDER BY COUNTRYNAME");
                DataTable dtVendorList = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.tblVendorBusinessesList ORDER BY Vendorlistname");
                chkVendorbusinesseslist.DataSource = dtVendorList;
                chkVendorbusinesseslist.DataTextField = "Vendorlistname";
                chkVendorbusinesseslist.DataValueField = "Vendorlistid";
                chkVendorbusinesseslist.DataBind();

                chkBusiness.DataSource = dtVendorList;
                chkBusiness.DataTextField = "Vendorlistname";
                chkBusiness.DataValueField = "Vendorlistid";
                chkBusiness.DataBind();

                ddlCountry.DataSource = Common.Execute_Procedures_Select_ByQuery(" SELECT Distinct NationalityCode As country FROM DBO.COUNTRY where NationalityCode is not null ORDER BY NationalityCode");
                ddlCountry.DataTextField = "COUNTRY";
                ddlCountry.DataValueField = "COUNTRY";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("", ""));

                //ddlTermsCode.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT TERMSCODE,[Desc] AS TERMSNAME FROM dbo.tblApTermsCode");
                //ddlTermsCode.DataTextField = "TERMSNAME";
                //ddlTermsCode.DataValueField = "TERMSCODE";
                //ddlTermsCode.DataBind();
                //ddlTermsCode.Items.Insert(0, new ListItem("", ""));
                //ddlTermsCode.SelectedValue="30";

                //ddlTaxGroupCode.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT TaxGrpID,[Desc] TAXNAME FROM dbo.tblSmTaxGroup");
                //ddlTaxGroupCode.DataTextField = "TAXNAME";
                //ddlTaxGroupCode.DataValueField = "TaxGrpID";
                //ddlTaxGroupCode.DataBind();
                //ddlTaxGroupCode.Items.Insert(0, new ListItem("", ""));
                //ddlTaxGroupCode.SelectedValue="No Tax";

                //ddlDistributionCode.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT DISTCODE, DISTCODE + ' - ' + [Desc] AS DISTNAME FROM dbo.tblApDistCode");
                //ddlDistributionCode.DataTextField = "DISTNAME";
                //ddlDistributionCode.DataValueField = "DISTCODE";
                //ddlDistributionCode.DataBind();
                //ddlDistributionCode.Items.Insert(0, new ListItem("", ""));
                //ddlDistributionCode.SelectedValue="USD";

                ddlCurrencyId.DataSource = Common.Execute_Procedures_Select_ByQuery("Select distinct CurrencyName As currencyid from Currency with(nolock) where StatusId = 'A' order by CurrencyName ");
                ddlCurrencyId.DataTextField = "currencyid";
                ddlCurrencyId.DataValueField = "currencyid";
                ddlCurrencyId.DataBind();
                ddlCurrencyId.Items.Insert(0, new ListItem("", ""));
                ddlCurrencyId.SelectedValue="USD";

                RequestId = Common.CastAsInt32(dt.Rows[0]["VRID"]);
                PageNo = 1;
                ShowRecord();
            }
            else
            {

            }

        }
    }
    protected void Move(bool Next)
    {
        if (Next)
            Move_Next();
        else
            Move_Back();
    }
    protected void Move_Next()
    {
        Move_ToPage(PageNo, PageNo + 1);
    }
    protected void Move_Back()
    {
        Move_ToPage(PageNo, PageNo - 1);
    }
    protected void Move_ToPage(int Current, int Next)
    {

        //if (Next == 9)
        //{
        //    DataTable dt_test = Common.Execute_Procedures_Select_ByQuery("select departmentid from dbo.userlogin where loginid=" + Common.CastAsInt32(Session["loginid"]) + " and departmentid+',' like '%4,%'");
        //    if (dt_test.Rows.Count <= 0)
        //    {
        //        ShowMessage("Sorry! Your have permission to access this page.", true);
        //        return;
        //    }
        //}

        this.FindControl("dv_Page_" + Current).Visible = false;
        this.FindControl("dv_Page_" + Next).Visible = true;

        if (Next == 15)
            appfrm.Attributes.Add("src", "ModifyVendorProfile_Approval.aspx?VRID=" + RequestId);

        PageNo = Next;
        ShowRecord();
    }

    protected void btnPageNo_Click(object sender, EventArgs e)
    {
        int _PageNo = Common.CastAsInt32(hid_PageNo.Value);
        Move_ToPage(PageNo, _PageNo);
    }
    //for justification vendor
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT MDPP FROM DBO.tbl_VenderRequest WHERE VRID=" + RequestId);
        if(Convert.ToString(dt.Rows[0][0])!="Y")
        {
            ShowMessage("Can't submit for approval. Vendor is not agreed with 'Company Data Protection Policy.", true);
            return;
        }     
        bool AnyChecked = false;
        bool ContactedVendors = false;
        foreach (System.Web.UI.WebControls.ListItem li in chk_justificationVendors.Items)
        {
            if (li.Selected)
            {
                AnyChecked = true;
                if (li.Value == "2")
                    ContactedVendors = true;
            }


        }
        if (ContactedVendors)
        {
            if (rpt_VendorsNameForJustification.Items.Count <= 0)
            {
                ShowMessage("Please select which vendor you contacted for supply.", true);
                return;
            }
        }
        //=====================================
        if (AnyChecked)
        {
            Save_Data("1P");

            //DataTable dt = Common.Execute_Procedures_Select_ByQuery("select email from dbo.userlogin where loginid=" + ddl_SecondedTo.SelectedValue);
            //if (dt.Rows.Count > 0)
            //{
            //    string message = "Please accept the IInded user in the POS System";
            //    //SendMail("IInded User Approval", message, dt.Rows[0]["email"].ToString());
            //    ShowMessage("Record saved successfully.", false);
            //}
        }
        else
        {
            ShowMessage("Please select Justification for new vendor.", true);
        }
    }
    //for 2nd approval
    //protected void btnSave_IIndProposer_Click(object sender, EventArgs e)
    //{
    //    if (chk_SecondedTo_save.Checked == true)
    //    {
    //        Save_Data("2P");
    //    }
    //    else
    //    {
    //        ShowMessage("Please check the checkbox (I Agree) to continue.", true);
    //    }
    //}
    //for 
    protected void btnSave_IstApproval_Click(object sender, EventArgs e)
    {
        Save_Data("1A");
    }
    //for second approval
    protected void btnSave_IIndApproval_Click(object sender, EventArgs e)
    {
        Save_Data("2A");
    }
    protected void btnNotifyByProposar_Click(object sender, EventArgs e)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select email from dbo.userlogin where loginid in ( SELECT FirstAppFwdTo FROM tbl_VenderRequest where vrid=" + RequestId + " )");
        if (dt.Rows.Count > 0)
        {
            string message = "Please approve the requested vendor in the EMANAGER System.<br/>-------------------<br/> Vendor Name : " + lblVendorName.Text + "<br/>Country : " + lblCompanyCountry.Text + "<br/>Services : " + lblServices.Text + "<br/><br/>Proposed By : " + txt_ProposedBy.Text + "<br/>Position : " + txt_proposedPosition.Text + "<br/>Remarks : " + txtPropRemarks.Text + "<br/>-------------------<br/>";
            string mailaddress = dt.Rows[0][0].ToString();
            if (mailaddress.Trim() != "")
            {
                string[] ToAdds = { mailaddress };
                string[] CCAdds = { "emanager@energiossolutions.com" };

                DataTable dtuser = Common.Execute_Procedures_Select_ByQuery("Select Email from UserLogin with(nolock) where LoginId = " + Common.CastAsInt32(Session["loginid"]) + "");
                if (dtuser.Rows.Count > 0)
                {
                    CCAdds[0] = dtuser.Rows[0]["Email"].ToString();
                }


		//string[] ToAdds = { "emanager@energiossolutions.com" };
                //string[] CCAdds = { "" };

                string[] NoFiles = {  };
                ProjectCommon.SendMailAsync(ToAdds, CCAdds, "Vendor Approval Request", message, NoFiles);
                ShowMessage("Notified Successfully.", false);
            }
        }

    }
    protected void btnNotifyByApprover1_Click(object sender, EventArgs e)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select email from dbo.userlogin where loginid in ( SELECT App2FwdTo FROM tbl_VenderRequest where vrid=" + RequestId + " )");
        if (dt.Rows.Count > 0)
        {
            string message = "Please approve the requested vendor in the POS System";
            string mailaddress = dt.Rows[0][0].ToString();
            if (mailaddress.Trim() != "")
            {
                string[] ToAdds = { mailaddress };
                string[] NoAdds = { };
                //string[] CCAdds = { "emanager@energiossolutions.com" };

                //DataTable dtuser = Common.Execute_Procedures_Select_ByQuery("Select Email from UserLogin with(nolock) where LoginId = " + Common.CastAsInt32(Session["loginid"]) + "");
                //if (dtuser.Rows.Count > 0)
                //{
                //    CCAdds[0] = dtuser.Rows[0]["Email"].ToString();
                //}
                ProjectCommon.SendMailAsync(ToAdds, NoAdds, "Vendor Approval Request", message, NoAdds);
                ShowMessage("Notified Successfully.", false);
            }
        }
    }
    protected void btnNotifyByApprover2_Click(object sender, EventArgs e)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select email from dbo.userlogin where loginid in ( SELECT AccountFwdTo FROM tbl_VenderRequest where vrid=" + RequestId + " )");
        if (dt.Rows.Count > 0)
        {
            string message = "Please approve the requested vendor in the POS System";
            string mailaddress = dt.Rows[0][0].ToString();
            if (mailaddress.Trim() != "")
            {
                string[] ToAdds = { mailaddress };
                string[] NoAdds = { };
                ProjectCommon.SendMailAsync(ToAdds, NoAdds, "Vendor Approval Request", message, NoAdds);
                ShowMessage("Notified Successfully.", false);
            }
        }
    }
    protected void Save_Data(string status)
    {
        if (status == "1P")
        {
            //for vendor proposal saving
            Common.Set_Procedures("sp_ProposalSubmission");
            Common.Set_ParameterLength(11);
            Common.Set_Parameters(
            new MyParameter("@VRID", Common.CastAsInt32(RequestId)),
            new MyParameter("@Justification_New_Vendor", GetCheckBoxListSelections(chk_justificationVendors)),
            new MyParameter("@SupplierIds_UnavailableToDeliver", GetRepeatorListSupplier(rpt_VendorsNameForJustification)),
            new MyParameter("@ProposedById", Common.CastAsInt32(Session["loginid"])),
            new MyParameter("@ProposedByName", Session["FullName"].ToString()),
            new MyParameter("@ProposedByPosition","" ), //Session["PositionName"].ToString()
            new MyParameter("@ProposedOn", System.DateTime.Now),
            new MyParameter("@SecondedUserId", 0), //  Common.CastAsInt32(ddl_SecondedTo.SelectedValue)
            new MyParameter("@PropRemarks", txtPropRemarks.Text.Trim()),
            new MyParameter("@FirstAppFwdTo", ddlFwdAppTo.SelectedValue),
            new MyParameter("@App2FwdTo", ddlFwdApp2.SelectedValue));

            DataSet dsprofile = new DataSet();
            if (Common.Execute_Procedures_IUD(dsprofile))
            {
                btnNotifyByProposar.Visible = true;
                ShowMessage("Record Saved successfully.", false);
            }
            else
            {
                ShowMessage("Unable to save record. Error : " + Common.ErrMsg, true);
            }
        }
        //if (status == "2P")
        //{
        //    if (Common.CastAsInt32(Session["loginid"]) == Common.CastAsInt32(ddl_SecondedTo.SelectedValue))
        //    {
        //        //for vendor proposal saving
        //        Common.Set_Procedures("sp_SecenodedToSubmission");
        //        Common.Set_ParameterLength(5);
        //        Common.Set_Parameters(
        //        new MyParameter("@VRID", Common.CastAsInt32(RequestId)),
        //        new MyParameter("@SecondedBy", Session["FullName"].ToString()),
        //        new MyParameter("@SecondedPosition", Session["PositionName"].ToString()),
        //        new MyParameter("@SecondedOn", System.DateTime.Now.Date),
        //        new MyParameter("@SecRemarks", txtSecRemarks.Text.Trim())
        //        );
        //        DataSet dsprofile = new DataSet();
        //        if (Common.Execute_Procedures_IUD(dsprofile))
        //        {
        //            ShowMessage("Record Saved successfully.", false);
        //        }
        //        else
        //        {
        //            ShowMessage("Unable to save record. Error : " + Common.ErrMsg, true);
        //        }
        //    }
        //}
        if (status == "1A")
        {
            string AppServices = GetCheckBoxListSelections(chkAppServices);
            Common.Set_Procedures("sp_FirstApproval");
            Common.Set_ParameterLength(8);
            Common.Set_Parameters(
               new MyParameter("@VRID", Common.CastAsInt32(RequestId)),
               new MyParameter("@ApprovalStatus", rd_ApprovalAcrion.SelectedValue),
               new MyParameter("@FirstApprovedBy", Session["FullName"].ToString()),
               new MyParameter("@FirstApprovedPosition",""), // Session["PositionName"].ToString()
               new MyParameter("@Validity1", txt_ValidityDate.Text),
               new MyParameter("@FirstApprovalType", Common.CastAsInt32(ddlApprovalType.SelectedValue)),
               new MyParameter("@FirstApprovedRemarks", txt_ApprovedRemakrs.Text.Trim().Replace("'", "`")),
               new MyParameter("@ApprovedBusinesses", AppServices));
            DataSet dsprofile = new DataSet();
            if (Common.Execute_Procedures_IUD(dsprofile))
            {
                if (rd_ApprovalAcrion.SelectedValue == "A")
                {
                    btnNotifyByApprover1.Visible = true;
                    ShowMessage("Record Saved successfully.", false);
                }
            }
            else
            {
                ShowMessage("Unable to save record. Error : " + Common.ErrMsg, true);
            }
        }
        if (status == "2A")
        {

            Common.Set_Procedures("sp_SecondApproval");
            Common.Set_ParameterLength(7);
            Common.Set_Parameters(
               new MyParameter("@VRID", Common.CastAsInt32(RequestId)),
               new MyParameter("@ApprovalStatus", rd_ApprovalAcrion_2.SelectedValue),
               new MyParameter("@SecondApprovedBy", Session["FullName"].ToString()),
               new MyParameter("@SecondApprovedPosition",""), //Session["PositionName"].ToString()
               new MyParameter("@Validity2nd", txt_ValidityDate_2.Text),
               new MyParameter("@SecondApporvalType", Common.CastAsInt32(ddlApprovalType_2.SelectedValue)),
               new MyParameter("@SecondAppprovedRemarks", txt_ApprovedRemakrs_2.Text.Trim().Replace("'", "`"))
               );
            DataSet dsprofile = new DataSet();
            if (Common.Execute_Procedures_IUD(dsprofile))
            {
                if (rd_ApprovalAcrion_2.SelectedValue == "A")
                {
                    btnNotifyByApprover2.Visible = true;
                    string sql = "SELECT '8V' + RIGHT('000'+CAST(MAX(REPLACE(LTRIM(RTRIM(VENDORID)), LEFT(VENDORID, 2), '')) + 1 AS varchar(6)),4) AS NEWVENDORID FROM DBO.V_VENDORS WHERE LEFT(VENDORID, 2) = '8V'";
                    DataTable dt11 = Common.Execute_Procedures_Select_ByQuery(sql);
                    if (dt11.Rows.Count > 0)
                    {
                        txtTravCode.Text = dt11.Rows[0][0].ToString();
                       
                        string sql1 = " SELECT Distinct NationalityCode As country FROM DBO.COUNTRY where NationalityCode is not null and (LTRIM(RTRIM(CountryName)) = '"+lblCompanyCountry.Text+ "' OR NationalityCode = '"+lblCompanyCountry.Text+ "') ORDER BY NationalityCode";
                        DataTable dtCountry = Common.Execute_Procedures_Select_ByQuery(sql1);
                        {
                            ddlCountry.SelectedValue = dtCountry.Rows[0]["country"].ToString();
                        }
                        updateAccountDetails(Common.CastAsInt32(RequestId), txtTravCode.Text);
                    }
                }
                ShowMessage("Record Saved successfully.", false);
            }
            else
            {
                ShowMessage("Unable to save record. Error : " + Common.ErrMsg, true);
            }
        }

        ShowRecord();

    }
    private string GetCheckBoxListSelections(CheckBoxList chklist)
    {
        string[] cblItems;
        ArrayList cblSelections = new ArrayList();
        foreach (ListItem item in chklist.Items)
        {
            if (item.Selected)
            {
                cblSelections.Add(item.Value);
            }
        }

        cblItems = (string[])cblSelections.ToArray(typeof(string));
        return string.Join(",", cblItems);
    }
    //protected void SendMailToAccount()
    //{
    //    string[] ToAdd = { "emanager@energiossolutions.com" };
    //    string[] NoAdd = { "" };
    //    string[] BccAdd = { "emanager@energiossolutions.com" };
    //    string Message = "Dear Account Department,<br><br>Please process this vendor in traverse.<br>Vendor Name :" + lblVendorName.Text + "<br>E-mail Address : " + lblEmailAddress + "<br><br><br>Thanks<br>";
    //    ProjectCommon.SendMailAsync(ToAdd, NoAdd, "Vendor Approved", Message, NoAdd);
    //}
    //protected void ddlTravVendor_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    lblVenCode.Text = ddlTravVendor.SelectedValue;
    //}
    public void ClearMessage()
    {
        lblMessage.InnerHtml = "";
        lblMessage.Visible = false;
    }

    //Service Liability
    //protected void rd_service_liability_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (rd_service_liability.SelectedValue == "Y")
    //    {
    //        div_insurance_company_name_service.Visible = true;
    //        div_insurance_company_amount_service.Visible = true;
    //    }
    //    else if (rd_service_liability.SelectedValue == "N")
    //    {
    //        div_insurance_company_name_service.Visible = false;
    //        div_insurance_company_amount_service.Visible = false;
    //    }
    //}
    ////Product Liability
    //protected void rd_product_liability_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (rd_product_liability.SelectedValue == "Y")
    //    {
    //        div_insurance_company_name_Pliability.Visible = true;
    //        div_insurance_company_amount_Pliability.Visible = true;
    //    }
    //    else if (rd_product_liability.SelectedValue == "N")
    //    {
    //        div_insurance_company_name_Pliability.Visible = false;
    //        div_insurance_company_amount_Pliability.Visible = false;
    //    }
    //}
    ////Has the Company been investigated by any Government Agency within last five years?
    //protected void rd_CompanyHasCompliance_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (rd_CompanyHasCompliance.SelectedValue == "Y")
    //    {
    //        div_outcome_complaince.Visible = true;
    //    }
    //    else if (rd_CompanyHasCompliance.SelectedValue == "N")
    //    {
    //        div_outcome_complaince.Visible = false;
    //    }
    //}
    //for getting comma saperated values of checkbox--added by laxmi on 21-sept-2015
    private string GetRepeatorListSupplier(Repeater rpt_Vlist)
    {
        string[] rptlItems;
        ArrayList cblSelections = new ArrayList();
        foreach (RepeaterItem item in rpt_Vlist.Items)
        {
            HiddenField hdsupid = (HiddenField)item.FindControl("hdn_SupplierID");
            cblSelections.Add(hdsupid.Value);
        }

        rptlItems = (string[])cblSelections.ToArray(typeof(string));
        return string.Join(",", rptlItems);
    }

    //end
    protected void ShowRecord()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(" SELECT *  FROM dbo.tbl_VenderRequest WHERE VRID=" + RequestId + "");
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            int _SuppId = Common.CastAsInt32(dr["SUPPLIERID"]);
            //liaccount.Visible = (_SuppId <= 0);
            //btnEvaluation.Visible = (_SuppId > 0);
            DataTable dtValid = Common.Execute_Procedures_Select_ByQuery("SELECT ValidityDate FROM dbo.vw_VendorRequest WHERE VRID=" + RequestId + "");
            if (dtValid.Rows.Count > 0)
            {
                DateTime? dtExp = null;
                if (!Convert.IsDBNull(dtValid.Rows[0][0]))
                    dtExp = Convert.ToDateTime(dtValid.Rows[0][0]);
                //if(dtExp.HasValue)
                    liEvaluation.Visible = (_SuppId > 0);// && (dtExp.Value < DateTime.Today.AddMonths(2));
            }
            //------------------------------------------
            //profile 
            lblCompanyname.Text = dr["COMPANYNAME"].ToString();
            if (dr["Blacklist2"].ToString() == "True")
            {
                lblBlackList.Text = " [ Blacklisted ]";
            }
            else
            {
                lblBlackList.Text = " ";
            }
            if (PageNo == 1)
            {
                #region Code

                DataTable dt101 = Common.Execute_Procedures_Select_ByQuery("SELECT DBO.getSelectedVendorServices(" + RequestId + ")");
                if (dt101.Rows.Count > 0)
                {
                    lblServices.Text = dt101.Rows[0][0].ToString();
                }

                txt_NoC.Text = dr["COMPANYNAME"].ToString();
                lblVendorName.Text = dr["COMPANYNAME"].ToString();

                txt_company_reg_no.Text = dr["CompanyRegNo"].ToString();
                txt_no_of_year_business.Text = dr["BusinessYears"].ToString();
                txt_tax_reg_no.Text = dr["TaxRegNo"].ToString();
                txt_address1.Text = dr["AddressLine1"].ToString();
                txt_address2.Text = dr["AddressLine2"].ToString();
                txt_city_state.Text = dr["City_State"].ToString();
                txt_zipcode.Text = dr["ZIP_PostalCode"].ToString();

                DataTable dtCompanyCountry = Common.Execute_Procedures_Select_ByQuery("SELECT countryid,countryname  FROM DBO.COUNTRY WHERE countryid=" + Common.CastAsInt32(dr["Country"].ToString()) + "");
                if (dtCompanyCountry.Rows.Count > 0)
                {
                    lblCompanyCountry.Text = dtCompanyCountry.Rows[0]["countryname"].ToString();
                }

                txt_phone_no.Text = dr["PhoneNo"].ToString().Replace("--", "-");
                lblTelephone.Text = dr["PhoneNo"].ToString().Replace("--", "-");

                txt_fax_no.Text = dr["FaxNo"].ToString().Replace("--", "-");
                lblFax.Text = dr["FaxNo"].ToString().Replace("--", "-");

                txt_email.Text = dr["EmailAddress"].ToString();
                lblEmailAddress.Text = dr["EmailAddress"].ToString();

                txt_website.Text = dr["WebSite"].ToString();

                txt_Contact_Person.Text = dr["ContactPersonName"].ToString();
                lblContact.Text = dr["ContactPersonName"].ToString();

                txt_Contact_Person_Position.Text = dr["CP_Position"].ToString();
                txt_Contact_Person_DirectPhone.Text = dr["CP_DirectPhoneNo"].ToString().Replace("--", "-");

                txt_cp_mobile.Text = dr["CP_MobileNo"].ToString().Replace("--", "-");


                txt_Contact_Person_FaxNo.Text = dr["CP_FaxNo"].ToString().Replace("--", "-");


                txt_Contact_Person_Email.Text = dr["CP_Email"].ToString();

                txt_head_of_company.Text = dr["Head_Company"].ToString();
                txt_head_of_company_postion.Text = dr["HC_Position"].ToString();

                txt_head_of_company_no.Text = dr["HC_ContactNo"].ToString().Replace("--", "-");

                txt_head_of_company_email.Text = dr["HC_Email"].ToString();
                txt_head_of_finance.Text = dr["Head_Finance_Company"].ToString();

                txt_head_of_finance_no.Text = dr["HF_ContactNo"].ToString().Replace("--", "-");

                txt_head_of_finance_email.Text = dr["HF_Email"].ToString();
                txt_head_of_quality.Text = dr["Head_Quality_Company"].ToString();

                txt_head_of_quality_no.Text = dr["HQ_ContactNo"].ToString().Replace("--", "-");


                txt_head_of_quality_email.Text = dr["HQ_Email"].ToString();
                txt_no_of_year_business.Text = dr["BusinessYears"].ToString();
                txt_head_office_name.Text = dr["HO_ParentCompany"].ToString();

                txt_HO_City_State.Text = dr["HO_City_State"].ToString();
                txt_HO_Zip_Postal_Code.Text = dr["HO_Zip_Postal_Code"].ToString();

                DataTable dtCPCountry = Common.Execute_Procedures_Select_ByQuery("SELECT countryid,countryname  FROM DBO.COUNTRY WHERE countryid=" + Common.CastAsInt32(dr["HO_Country"].ToString()) + "");
                if (dtCPCountry.Rows.Count > 0)
                {
                    lbl_HO_Country.Text = dtCPCountry.Rows[0]["countryname"].ToString();
                }


                txt_HO_PhoneNo.Text = dr["HO_PhoneNo"].ToString().Replace("--", "-");

                txt_HO_FaxNo.Text = dr["HO_FaxNo"].ToString().Replace("--", "-");


                txt_HO_Email.Text = dr["HO_Email"].ToString();
                txt_HO_WebSite.Text = dr["HO_WebSite"].ToString();
                txt_no_of_year_business_HQPC.Text = dr["HO_BusinessYears"].ToString();
                txt_CompanyOwnerStruct.Text = dr["CompanyOwnerStruct"].ToString();
                #endregion
            }
            //---------------------------------------------------------------------

            //description
            if (PageNo == 2)
            {
                #region Code
                chkVendorbusinesseslist.ClearSelection();

                string strDescription = "," + dr["COMPANYBUSINESSES"].ToString() + ",";
                foreach (ListItem li in chkVendorbusinesseslist.Items)
                {
                    if (strDescription.Contains("," + li.Value + ","))
                    {
                        li.Selected = true;
                    }
                }
                txt_description_other.Text = dr["CompanyBusinesses_Others"].ToString();
                txt_Services_Specify.Text = dr["AdditionalService_Specify"].ToString();
                txt_Add_Description.Text = dr["AdditionalDescription"].ToString();


                #endregion
            }
           

            
           
           
            //bank
            if (PageNo == 3)
            {
                #region Code
                txt_bank_name.Text = dr["BANKNAME"].ToString();
                txt_bank_address1.Text = dr["BANKADDRESSLINE1"].ToString();
                txt_bank_address2.Text = dr["BANKADDRESSLINE2"].ToString();
                txt_bank_postcode.Text = dr["BANKPOSTCODE"].ToString();
                txt_bank_city.Text = dr["BANKCITY"].ToString();

                DataTable dtBankCountry = Common.Execute_Procedures_Select_ByQuery("SELECT countryid,countryname  FROM DBO.COUNTRY WHERE countryid=" + Common.CastAsInt32(dr["BANKCOUNTRY"].ToString()) + "");
                if (dtBankCountry.Rows.Count > 0)
                {
                    lbl_bankCountry.Text = dtBankCountry.Rows[0]["countryname"].ToString();
                }

                string ba = dr["BANKACCOUNTNO"].ToString();

                if (ba.Length > 4)
                    txt_bank_account_no.Text = "XXXXXXXXXXXX" + ba.Substring(ba.Length - 4);
                else
                    txt_bank_account_no.Text = "XXXX";

                //txt_bank_swift_code.Text = dr["BANKSWIFTCODE"].ToString();
                txt_bank_IBAN.Text = dr["BANKIBAN"].ToString();
                txtIFSCCode.Text = dr["BankIFSCCode"].ToString();
                txt_pref_single.Text = dr["BANK_SINGLE_CURR"].ToString();
                lblaccepted.Text = "";
                if (Convert.ToString(dr["MDPP"]) == "Y")
                {
                    lblaccepted.Text = "Yes";
                    lblaccepted.Text = " Accepted On : " + Common.ToDateString(dr["MDPPAcceptedOn"]);                    
                }        
                        
                //-----------------------------------------------------------------------            
                #endregion
            }
            if (PageNo == 4)
            {
                #region Code
                DataTable dtbusiness = Common.Execute_Procedures_Select_ByQuery("exec DBO.getBusiness " + _SuppId);
                CrystalReportViewer1.ReportSource = rpt;
                //CrystalReportViewer1.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;
                rpt.Load(Server.MapPath("~/Modules/Purchase/Report/BusinessChart.rpt"));
                rpt.SetDataSource(dtbusiness);
                #endregion
            }
            if (PageNo == 5)
            {
                #region Code
                DataTable dtComments = Common.Execute_Procedures_Select_ByQuery("SELECT *,RatingText=case when Rating=-2 then 'Poor' when Rating=-1 then 'Below Avg.' when Rating=0 then 'Average' when Rating=1 then 'Good' when Rating=2 then 'OutStanding' else '' END  FROM BidSupplerComments WHERE BIDID IN (select bidid from dbo.tblSMDPOMasterBid WHERE SupplierID=" + _SuppId.ToString() + ") and Rating is not null");
                rptComments.DataSource = dtComments;
                rptComments.DataBind();

                DataTable dtComments_Sum = Common.Execute_Procedures_Select_ByQuery("SELECT RATING,COUNT(*) AS NOTIMES FROM BidSupplerComments WHERE BIDID IN (select bidid from dbo.tblSMDPOMasterBid WHERE SupplierID=" + _SuppId.ToString() + ") and Rating is not null GROUP BY RATING");
                int TotalData = Common.CastAsInt32(dtComments_Sum.Compute("SUM(NOTIMES)", ""));
                if (TotalData > 0)
                {
                    foreach (DataRow dr1 in dtComments_Sum.Rows)
                    {
                        int ThisData = Common.CastAsInt32(dr1["NOTIMES"]);
                        object ob = this.FindControl("Label" + dr1["RATING"].ToString().Replace("-", "_"));
                        if (ob != null)
                        {
                            Label ll = (Label)ob;

                            ll.Text = Math.Round((ThisData * 100.0 / TotalData), 1).ToString() + " %";

                        }
                    }
                }
                #endregion
            }

            // New approval
            if (PageNo == 6)
            {
                Bind_Vendors();

                //for proposed by   
                #region Proposal

                //for justification
                if (dr["COMPANYBUSINESSES"].ToString().Trim() != "")
                {
                    DataTable dtVendorList1 = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.tblVendorBusinessesList where vendorlistid in(" + dr["COMPANYBUSINESSES"].ToString() + ") ORDER BY Vendorlistname");
                    chkAppServices.DataSource = dtVendorList1;
                    chkAppServices.DataTextField = "Vendorlistname";
                    chkAppServices.DataValueField = "Vendorlistid";
                    chkAppServices.DataBind();
                }

 		lbl_services_other.Text = dr["CompanyBusinesses_Others"].ToString();
                lbl_services_specify.Text = dr["AdditionalService_Specify"].ToString();
                lbl_services_add.Text = dr["AdditionalDescription"].ToString();


                //SecondedTo = Common.CastAsInt32(dr["SecondedUserId"].ToString());
                chk_justificationVendors.ClearSelection();
                string strJustification = "," + dr["Justification_New_Vendor"].ToString() + ",";
                foreach (ListItem li in chk_justificationVendors.Items)
                {
                    if (strJustification.Contains("," + li.Value + ","))
                    {
                        li.Selected = true;
                    }
                }

                if (!Convert.IsDBNull(dr["ProposedOn"]))
                {
                    //ddlFwdAppTo.SelectedValue = dr["FirstAppFwdTo"].ToString();
                    //ddlFwdApp2.SelectedValue = dr["App2FwdTo"].ToString();
                    txt_ProposedBy.Text = dr["ProposedByName"].ToString();
                    txt_ProposedOn.Text = Common.ToDateString(dr["ProposedOn"]);
                    txt_proposedPosition.Text = dr["ProposedByPosition"].ToString();
                    //ddl_SecondedTo.SelectedValue = dr["SecondedUserId"].ToString();
                    txtPropRemarks.Text = dr["PropRemarks"].ToString();
                    ApprovalStageDone = 1;
                }
                #endregion

                // fro IInded Person
                //if (!Convert.IsDBNull(dr["SecondedOn"]))
                //{
                //    lblSecondedBy.Text = dr["SecondedBy"].ToString();
                //    lblSecondedByPos.Text = dr["SecondedPosition"].ToString();
                //    lblSecondedOn.Text = Common.ToDateString(dr["SecondedOn"]);
                //    txtSecRemarks.Text = dr["SecRemarks"].ToString();
                //    ApprovalStageDone = 2;
                //}

                //for first approval    
                # region First Approval
                if (!Convert.IsDBNull(dr["FirstApprovalResult"]))
                {
                    rd_ApprovalAcrion.SelectedValue = dr["FirstApprovalResult"].ToString();
                    txt_ApprovedBy.Text = dr["FirstApprovedBy"].ToString() + " / " + Common.ToDateString(dr["FirstApprovedOn"]);
                    txt_ApprovedPosition.Text = dr["FirstApprovedPosition"].ToString();
                    txt_ValidityDate.Text = Common.ToDateString(dr["ValidityDate"]);
                    ddlApprovalType.SelectedValue = dr["FirstApprovalType"].ToString();
                    txt_ApprovedRemakrs.Text = dr["FirstApprovedRemarks"].ToString();
                    ddlApprovalType_OnSelectedIndexChanged(new object(), new EventArgs());
                    ApprovalStageDone = 3;

                    string ApprovedBusinesses = dr["ApprovedBusinesses"].ToString();
                    chkAppServices.ClearSelection();
                    string strDescription = "," + ApprovedBusinesses + ",";
                    foreach (ListItem li in chkAppServices.Items)
                    {
                        if (strDescription.Contains("," + li.Value + ","))
                        {
                            li.Selected = true;
                        }
                    }

                }
                #endregion

                //for 2nd approval
                # region Second Approval
                if (!Convert.IsDBNull(dr["SecondApprovalResult"]))
                {
                    txt_ApprovedBy_2.Text = dr["SecondApprovedBy"].ToString() + " / " + Common.ToDateString(dr["SecondAppprovedOn"]);

                    txt_ValidityDate_2.Text = Common.ToDateString(dr["ValidityDate"]);
                    txt_ApprovedPosition_2.Text = dr["SecondApprovedPosition"].ToString();
                    rd_ApprovalAcrion_2.SelectedValue = dr["SecondApprovalResult"].ToString();
                    ddlApprovalType_2.SelectedValue = dr["SecondApporvalType"].ToString();
                    txt_ApprovedRemakrs_2.Text = dr["SecondAppprovedRemarks"].ToString();
                    ddlApprovalType_2_OnSelectedIndexChanged(new object(), new EventArgs());
                    ApprovalStageDone = 4;
                }
                #endregion



                int RequestApprovalStatus = Common.CastAsInt32(dr["RequestApprovalStatus"]);

                btnSubmit.Visible = false;
                //btnSave_IIndProposer.Visible = false;
                btnNotifyByProposar.Visible = false;
                btnSave_IstApproval.Visible = false;
                btnSave_IIndApproval.Visible = false;

                //pnlA1.Visible = false;
                pnlA2.Visible = false;
                pnlA3.Visible = false;
                pnlA3.Visible = false;

                btn_selctvendors.Visible = false;

                if (RequestApprovalStatus == 1)
                    RequestApprovalStatus = 2;

                switch (RequestApprovalStatus)
                {
                    case 2: // Record Submitted Waiting for proposar
                        btn_selctvendors.Visible = true;
                        btnSubmit.Visible = true;
                        break;
                    case 3: // Waiting for Ist Approval
                        btnNotifyByProposar.Visible = true;
                        bool visible = (Common.CastAsInt32(Session["loginid"]) == Common.CastAsInt32(dr["FirstAppFwdTo"]));
                        if (visible)
                        {
                            hid_TabNo.Value = "2";
                            pnlA1.Visible = false;
                            pnlA2.Visible = true;
                            btnSave_IstApproval.Visible = true;
                        }
                        break;
                    case 4: // Waiting for IInd Approval
                        btnNotifyByApprover1.Visible = true;
                        bool visible1 = (Common.CastAsInt32(Session["loginid"]) == Common.CastAsInt32(dr["App2FwdTo"]));
                        if (visible1)
                        {
                            hid_TabNo.Value = "3";
                            pnlA1.Visible = false;
                            pnlA3.Visible = true;
                            btnSave_IIndApproval.Visible = true;
                        }
                        break;
                    case 5: // Accounts
                        btnNotifyByApprover2.Visible = true;
                        //bool visible5 = (Common.CastAsInt32(dr["SUPPLIERID"]) <= 0) && (Common.CastAsInt32(Session["loginid"]) == Common.CastAsInt32(dr["AccountFwdTo"]));
                        bool visible5 = (Common.CastAsInt32(dr["SUPPLIERID"]) <= 0) && (Common.CastAsInt32(Session["loginid"]) == 299 || Common.CastAsInt32(Session["loginid"]) == 215 || Common.CastAsInt32(Session["loginid"]) == Common.CastAsInt32(dr["AccountFwdTo"]));
                        if (visible5)
                        {
                            hid_TabNo.Value = "4";
                            pnlA1.Visible = false;
                            pnlA4.Visible = true;
                            btnSave_Account.Visible = true;
                        }
                        break;
                    case 6: // rejected
                        break;
                    default:
                        break;

                }
                int CurrentUserId = Common.CastAsInt32(Session["loginid"]);
                int ProposerId = Common.CastAsInt32(dr["ProposedById"]);
                if (ProposerId > 0)
                    CurrentUserId = ProposerId;


                //DataTable dtAppTo = Common.Execute_Procedures_Select_ByQuery("select *,(SELECT top 1 USERID FROM DBO.Hr_PersonalDetails K WHERE K.EmpId=R.EMPID) AS USERID from  dbo.GET_REPORTING_CHAIN_INCLUDE_SELF((SELECT EMPID FROM DBO.Hr_PersonalDetails WHERE USERID=" + CurrentUserId + ")) R");

		DataTable dtAppTo = Common.Execute_Procedures_Select_ByQuery("SELECT LOGINID,FIRSTNAME + ' ' + LASTNAME AS EMPNAME from UserLogin u with(nolock) Inner Join POS_invoice_mgmt pim with(nolock) On u.LoginId = pim.UserId Where VendorApproval1 = 1 and u.StatusId = 'A' and u.LoginId <> 1 ORDER BY EMPNAME");
                ddlFwdAppTo.DataSource = dtAppTo;
                ddlFwdAppTo.DataTextField = "EMPNAME";
                ddlFwdAppTo.DataValueField = "LOGINID";
                ddlFwdAppTo.DataBind();
                ddlFwdAppTo.Items.Insert(0, new ListItem(" < Select > ", "0"));
		try
                    {
                if (ProposerId > 0)
                    ddlFwdAppTo.SelectedValue = dr["FirstAppFwdTo"].ToString();
		}catch{}


                //DataTable dtApp2 = Common.Execute_Procedures_Select_ByQuery("select userid ,FirstName+' '+FamilyName as empname from dbo.Hr_PersonalDetails where drc is null and position in (1,4,89) order by empname");

		DataTable dtApp2 = Common.Execute_Procedures_Select_ByQuery("SELECT LOGINID,FIRSTNAME + ' ' + LASTNAME AS EMPNAME from UserLogin u with(nolock) Inner Join POS_invoice_mgmt pim with(nolock) On u.LoginId = pim.UserId Where VendorApproval2 = 1 and u.StatusId = 'A' and u.LoginId <> 1 ORDER BY EMPNAME");
                
                ddlFwdApp2.DataSource = dtApp2;
                ddlFwdApp2.DataTextField = "EMPNAME";
                ddlFwdApp2.DataValueField = "LOGINID";
                ddlFwdApp2.DataBind();
                ddlFwdApp2.Items.Insert(0, new ListItem(" < Select > ", "0"));

                if (RequestApprovalStatus > 2)
                {
                    try
                    {
                        ddlFwdApp2.SelectedValue = dr["App2FwdTo"].ToString();
                    }catch{}
                }


                btnSaveNomination.Visible = btnSave_IstApproval.Visible || btnSave_IIndApproval.Visible;

                //if (!chk_SecondedTo_save.Checked)
                //    chk_SecondedTo_save.Checked = (RequestApprovalStatus > 2);
                //======================================
                if (RequestApprovalStatus >= 2)
                {
                    foreach (RepeaterItem rptv in rpt_VendorsNameForJustification.Items)
                    {
                        ImageButton imgbutton = rptv.FindControl("imgBtnDelete") as ImageButton;
                        imgbutton.Visible = false;
                    }

                }
                rd_ApprovalAcrion_SelectedIndexChanged(this, new EventArgs());
                rd_ApprovalAcrion_2_SelectedIndexChanged(this, new EventArgs());

                // Account tab

                if (Common.CastAsInt32(dr["SUPPLIERID"]) > 0)
                {
                    DataTable dtSupplier = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM dbo.tblSMDSuppliers WHERE supplierid=" + _SuppId.ToString());
                    if (dtSupplier.Rows.Count > 0)
                    {
                        txtTravCode.Text = dtSupplier.Rows[0]["TravID"].ToString();
                        
                        if (Convert.IsDBNull(dtSupplier.Rows[0]["MultiCurr"]))
                            ddlMultiCurr.SelectedIndex = 0;
                        else if (dtSupplier.Rows[0]["MultiCurr"].ToString() == "True")
                            ddlMultiCurr.SelectedValue = "1";
                        else
                            ddlMultiCurr.SelectedValue = "0";




                        //txtPortName.Text = dtSupplier.Rows[0]["SupplierPort"].ToString();

                        //if (Convert.IsDBNull(dtSupplier.Rows[0]["Preferred"]))
                        //    ddlPreferred.SelectedIndex = 0;
                        //else if (dtSupplier.Rows[0]["Preferred"].ToString() == "True")
                        //    ddlPreferred.SelectedValue = "1";
                        //else
                        //    ddlPreferred.SelectedValue = "0";

                        //txtService.Text = dtSupplier.Rows[0]["ServiceType"].ToString();
                        lblApprovalType.Text = dtSupplier.Rows[0]["ApprovalType"].ToString();
                    }



                }
                else
                {
                    int SecondApporvalType = Common.CastAsInt32(dr["SecondApporvalType"]);
                    switch (SecondApporvalType)
                    {
                        case 1:
                            lblApprovalType.Text = "Nominated / Contracted";
                            break;
                        case 2:
                            lblApprovalType.Text = "OTA";
                            break;
                        case 3:
                            lblApprovalType.Text = "Other";
                            break;
                        case 6:
                            lblApprovalType.Text = "Owner's Recommendation";
                            break;
			case 7:
                            lblApprovalType.Text = "Maker";
                            break;
                        default:
                            break;
                    }

                }

            }
            if (PageNo == 7)
            {
                appfrm.Attributes.Add("src", "ModifyVendorProfile_Approval.aspx?VRID=" + RequestId);
                //txt_description_other.Text = dr["CompanyBusinesses_Others"].ToString();
                //txt_Services_Specify.Text = dr["AdditionalService_Specify"].ToString();
                //txt_Add_Description.Text = dr["AdditionalDescription"].ToString();
            }
            // accounts
            //if (PageNo == 16)
            //{
            //    if (_SuppId > 0)
            //    {
            //        DataTable dtSupplier = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM dbo.tblSMDSuppliers WHERE supplierid=" + _SuppId.ToString());
            //        if (dtSupplier.Rows.Count > 0)
            //        {
            //            try
            //            {
            //                ddlTravVendor.SelectedValue = dtSupplier.Rows[0]["TravID"].ToString();
            //            }catch(Exception ex){}

            //            if (Convert.IsDBNull(dtSupplier.Rows[0]["MultiCurr"]))
            //                ddlMultiCurr.SelectedIndex = 0;
            //            else if (dtSupplier.Rows[0]["MultiCurr"].ToString() == "True")
            //                ddlMultiCurr.SelectedValue = "1";
            //            else
            //                ddlMultiCurr.SelectedValue = "0";

            //            //txtPortName.Text = dtSupplier.Rows[0]["SupplierPort"].ToString();

            //            //if (Convert.IsDBNull(dtSupplier.Rows[0]["Preferred"]))
            //            //    ddlPreferred.SelectedIndex = 0;
            //            //else if (dtSupplier.Rows[0]["Preferred"].ToString() == "True")
            //            //    ddlPreferred.SelectedValue = "1";
            //            //else
            //            //    ddlPreferred.SelectedValue = "0";

            //            //txtService.Text = dtSupplier.Rows[0]["ServiceType"].ToString();
            //            lblApprovalType.Text = dtSupplier.Rows[0]["ApprovalType"].ToString();
            //        }



            //    }
            //    else
            //    {
            //        btnEvaluation.Visible = false;
            //        int SecondApporvalType = Common.CastAsInt32(dr["SecondApporvalType"]);
            //        switch (SecondApporvalType)
            //        {
            //            case 1:
            //                lblApprovalType.Text = "Nominated";
            //                break;
            //            case 2:
            //                lblApprovalType.Text = "OTA";
            //                break;
            //            case 3:
            //                lblApprovalType.Text = "Other";
            //                break;
            //            default:
            //                break;
            //        }

            //    }
            //}
            // evaluation

            if (PageNo == 8)
            {
                //------------------------
                if (dr["COMPANYBUSINESSES"].ToString().Trim() != "")
                {
                    DataTable dtVendorList1 = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.tblVendorBusinessesList where vendorlistid in(" + dr["COMPANYBUSINESSES"].ToString() + ") ORDER BY Vendorlistname");
                    chkAppServices1.DataSource = dtVendorList1;
                    chkAppServices1.DataTextField = "Vendorlistname";
                    chkAppServices1.DataValueField = "Vendorlistid";
                    chkAppServices1.DataBind();
                }
                //------------------------
                try
                {
                    ddlEvalType_1.SelectedValue = dr["FirstApprovalType"].ToString();
                    ddlEvalType_1_OnSelectedIndexChanged(new object(), new EventArgs());
                }
                catch { }
                try
                {
                    ddlEvalType_2.SelectedValue = dr["SecondApporvalType"].ToString();
                    ddlEvalType_2_OnSelectedIndexChanged(new object(), new EventArgs());
                }
                catch { }

                int LoginID = Common.CastAsInt32(Session["loginid"]); 
                bool UserIsInList = (LoginID == 1 || LoginID == 215 || LoginID == 270 || LoginID == 299);
                bool IsUserMD = (LoginID == 19);
                bool IsBlackListed = dr["Blacklist2"].ToString() == "True";
                bool AllowEval= (IsBlackListed) ? IsUserMD : UserIsInList;

                if (Convert.IsDBNull(dr["Eval1On"]))
                {
                    btnEvaluate1.Visible = true && AllowEval;
                    btnEvaluate2.Visible = false;
                }
                else
                {
                    btnEvaluate1.Visible = false;
                    radEval1.SelectedValue = dr["Eval1Result"].ToString();
                    radEval1_OnSelectIndexChanged(new object(), new EventArgs());
                    lblEval1BY.Text = dr["Eval1By"].ToString();
                    lblEval1On.Text = Common.ToDateString(dr["Eval1On"]);
                    txtValidTillDate1.Text = Common.ToDateString(dr["Eval1ValidTill"]);
                    txtEvalRemarks1.Text = dr["Eval1Remarks"].ToString();

                    
                    if (Convert.IsDBNull(dr["Eval2On"]))
                    {
                        btnEvaluate2.Visible = true && AllowEval;
                    }
                    else
                    {
                        btnEvaluate2.Visible = false;
                        radEval2.SelectedValue = dr["Eval2Result"].ToString();
                        radEval2_OnSelectIndexChanged(new object(), new EventArgs());
                        lblEval2By.Text = dr["Eval2By"].ToString();
                        lblEval2On.Text = Common.ToDateString(dr["Eval2On"]);
                        txtValidTillDate2.Text = Common.ToDateString(dr["Eval2ValidTill"]);
                        txtEvalRemarks2.Text = dr["Eval2Remarks"].ToString();
                        
                    }
                    chkBlackList1.Checked = dr["Blacklist1"].ToString() == "True";
                    chkBlacklist2.Checked = dr["Blacklist2"].ToString() == "True";

                    string ApprovedBusinesses = dr["ApprovedBusinesses"].ToString();
                    chkAppServices1.ClearSelection();
                    string strDescription = "," + ApprovedBusinesses + ",";
                    foreach (ListItem li in chkAppServices1.Items)
                    {
                        if (strDescription.Contains("," + li.Value + ","))
                        {
                            li.Selected = true;
                        }
                    }


                }

                //==============================================

                btnSaveNomination.Visible = btnSaveNomination.Visible || btnEvaluate1.Visible || btnEvaluate2.Visible;

                btnEvaluate2.Visible = (!(btnEvaluate1.Visible) && Convert.IsDBNull(dr["Eval2On"]) && _SuppId > 0);

		// force evaluation allwoed

		btnEvaluate2.Visible = (_SuppId > 0) && AllowEval;

            }


            if (_SuppId > 0)
            {
                btnSave_IIndApproval.Visible = false;
                btnSave_Account.Visible = false;
                btnSave_IstApproval.Visible = false;
                btnSubmit.Visible = false;

                btnNotifyByProposar.Visible = false;
                btnNotifyByApprover1.Visible = false;
                btnNotifyByApprover2.Visible = false;
            }

            if (btnSave_Account.Visible)
            {
                //string sql = "SELECT '8V' + REPLACE(STR(ISNULL(MAX(RIGHT(LTRIM(RTRIM(VENDORID)),4)),0)+1,4),' ','0') AS NEWVENDORID FROM DBO.V_VENDORS WHERE LEFT(VENDORID,2)='8V'";
                string sql = "SELECT '8V' + RIGHT('000'+CAST(MAX(REPLACE(LTRIM(RTRIM(VENDORID)), LEFT(VENDORID, 2), '')) + 1 AS varchar(6)),4) AS NEWVENDORID FROM DBO.V_VENDORS WHERE LEFT(VENDORID, 2) = '8V'";
                DataTable dt11 = Common.Execute_Procedures_Select_ByQuery(sql);
                if(dt11.Rows.Count>0)
                {
                    txtTravCode.Text = dt11.Rows[0][0].ToString();
                }
            }
        }
    }
    public string GetRating(object rat)
    {
        switch (Common.CastAsInt32(rat))
        {
            case -2:
                return "Poor";
            case -1:
                return "Below Avg.";
            case 0:
                return "Average";
            case 1:
                return "Good";
            case 2:
                return "Outstanding";
            default:
                return "";
        }
    }
    
   
    protected void Bind_Vendors()
    {
        //Vendors
        DataTable dtVendors = Common.Execute_Procedures_Select_ByQuery(" select sj.VRID,s.SupplierID, s.SupplierName ,s.SupplierTel,s.SupplierEmail ,s.SupplierFax " +
                                                                        " ,s.SupplierContact from dbo.tbl_SelectedVendors_Forjustification sj " +
                                                                        " left join DBO.tblSMDSuppliers s on s.supplierid=sj.supplierid where sj.vrid=" + RequestId.ToString());
        DataView dv = dtVendors.DefaultView;

        rpt_VendorsNameForJustification.DataSource = dv.ToTable();
        rpt_VendorsNameForJustification.DataBind();
    }
   
    public void ShowMessage(string Msg, bool Error)
    {
        lblMessage.Visible = true;
        if (Error && Common.ErrMsg != "")
            lblMessage.InnerHtml = Msg + " Error :" + Common.ErrMsg;
        else
            lblMessage.InnerHtml = Msg;
        lblMessage.Attributes.Add("class", (Error) ? "msgbox error" : "msgbox success");
    }
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        int TableId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dtFiles = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM dbo.tbl_VendorRequestDocuments WHERE TABLEID=" + TableId);
        if (dtFiles.Rows.Count > 0)
        {
            string FileName = dtFiles.Rows[0]["FileName"].ToString();
            string ExtFileName = Path.GetExtension(FileName).Substring(1);
            Response.ContentType = "application/" + ExtFileName;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
            byte[] buffer = (byte[])dtFiles.Rows[0]["FileContents"];
            Response.BinaryWrite(buffer);
            Response.Flush();
            //Response.Close();
            //Response.End();
        }
    }
    protected void lnkbtnSelectServices_Click(object sender,EventArgs e)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT CompanyBusinesses,AdditionalService_Specify FROM DBO.TBL_VENDERREQUEST WHERE VRID=" + RequestId);
        if (dt.Rows.Count > 0)
        {
            string items = dt.Rows[0][0].ToString();
            foreach (ListItem item in chkBusiness.Items)
            {
                if ((","+items+",").Contains(","+item.Value+","))
                {
                    item.Selected = true;
                }
                else
                    item.Selected = false;
            }
            txtOtherName.Text = dt.Rows[0]["AdditionalService_Specify"].ToString();
        }
        
        dvSelectVendors.Visible = true;
    }
    protected void btnSaveBusiness_Click(object sender, EventArgs e)
    {
        if (chkBusiness.SelectedIndex < 0)
        {
            ScriptManager.RegisterStartupScript(this,this.GetType(),"tet","alert('Please select description.');",true);
            return;
        }
        foreach(ListItem item in chkBusiness.Items)
        {
            if (item.Value == "22" && txtOtherName.Text.Trim()=="" && item.Selected)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "tet", "alert('Please specify the services.');", true);
                return;
            }
        }

        string str_businesseslist = GetCheckBoxListSelections(chkBusiness);
        if (str_businesseslist.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tet", "alert('Please select description.');", true);
            return;
        }
        else
        {
            Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.TBL_VENDERREQUEST SET CompanyBusinesses='" + str_businesseslist + "',AdditionalService_Specify='" + txtOtherName.Text.Trim() + "' WHERE VRID=" + RequestId);
            dvSelectVendors.Visible = false;

            DataTable dt101 = Common.Execute_Procedures_Select_ByQuery("SELECT DBO.getSelectedVendorServices(" + RequestId + ")");
            if (dt101.Rows.Count > 0)
            {
                lblServices.Text = dt101.Rows[0][0].ToString();
            }
        }
    }
    protected void btnCloseBusiness_Click(object sender, EventArgs e)
    {
        dvSelectVendors.Visible = false;
    }

    //for justification vendors
    protected void btn_selectvendors_Click(object sender, EventArgs e)
    {
        BindVendor();
        modalBox.Visible = true;
        modalframe_SelectVendors.Visible = true;
    }
    protected void btn_savemodel3_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem i in rpt_VendorList.Items)
        {
            //Retrieve the state of the CheckBox
            CheckBox cb = (CheckBox)i.FindControl("chk_VendorList");
            if (cb.Checked)
            {
                //Retrieve the value associated with that CheckBox
                HiddenField hdnsupplierid = (HiddenField)i.FindControl("hdn_SupplierID");
                Label lblsuppliername = (Label)i.FindControl("lblSupplierName");

                //----------------------------------------------      
                Common.Set_Procedures("sp_InsertUpdateVendorForJustification");
                Common.Set_ParameterLength(3);
                Common.Set_Parameters(new MyParameter("@VRID", Common.CastAsInt32(RequestId)),
                    new MyParameter("@SupplierID", Common.CastAsInt32(hdnsupplierid.Value)),
                    new MyParameter("@SupplierName", lblsuppliername.Text.Trim().Replace("'", "`"))
                    );
                DataSet ds = new DataSet();
                Common.Execute_Procedures_IUD(ds);
                Bind_Vendors();
            }
        }

        modalBox.Visible = false;
        modalframe_SelectVendors.Visible = false;
    }
    protected void btnCloseModal3_Click(object sender, EventArgs e)
    {
        modalBox.Visible = false;
        modalframe_SelectVendors.Visible = false;
    }
    public void BindVendor()
    {
        DataTable DTVendor = GetSuplierData("", "", "");
        if (DTVendor != null)
        {
            rpt_VendorList.DataSource = DTVendor;
            rpt_VendorList.DataBind();
        }
    }
    protected void rpt_VendorsNameForJustification_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            DataTable dtCountry = Common.Execute_Procedures_Select_ByQuery("delete FROM tbl_SelectedVendors_Forjustification where SupplierId=" + Common.CastAsInt32(e.CommandArgument));
            Bind_Vendors();
        }
    }
    public DataTable GetSuplierData(string SortBy, string SortType, string WhereClause)
    {
        string sql = "SELECT Row_Number() over(order by <SORTKEY>) as srno,tblSMDSuppliers.SupplierID, tblSMDSuppliers.SupplierName, tblSMDSuppliers.SupplierPort, replace(tblSMDSuppliers.SupplierTel,';','</br>')SupplierTel " +
            " , tblSMDSuppliers.SupplierEmail, tblSMDSuppliers.SupplierContact,tblSMDSuppliers.TravID FROM DBO.tblSMDSuppliers ";


        if (WhereClause != "")
            sql = sql + WhereClause;
        else
            sql = sql + "WHERE tblSMDSuppliers.Active=1 ";

        if (SortBy != "")
        {
            sql = sql + " ORDER BY " + SortBy + " " + SortType + "";
            sql = sql.Replace("<SORTKEY>", SortBy + " " + SortType);
        }
        else
        {
            sql = sql + " ORDER BY tblSMDSuppliers.SupplierName";
            sql = sql.Replace("<SORTKEY>", "tblSMDSuppliers.SupplierName");
        }


        DataTable DTVendor = Common.Execute_Procedures_Select_ByQuery(sql);
        return DTVendor;
    }

    protected void rd_ApprovalAcrion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rd_ApprovalAcrion.SelectedValue == "A")
        {
            tr_apprvalType.Visible = true;
            tr_Validity1.Visible = true;
        }
        else if (rd_ApprovalAcrion.SelectedValue == "R")
        {
            tr_apprvalType.Visible = false;
            tr_Validity1.Visible = false;
        }
    }
    protected void rd_ApprovalAcrion_2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rd_ApprovalAcrion_2.SelectedValue == "A")
        {
            tr_apprvalType_2.Visible = true;
            tr_validity_2.Visible = true;
        }
        else if (rd_ApprovalAcrion.SelectedValue == "R")
        {
            tr_apprvalType_2.Visible = false;
            tr_validity_2.Visible = false;
        }

    }
    protected void ddlApprovalType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        lnkApp1.Visible = ddlApprovalType.SelectedValue == "6";
    }
    protected void ddlApprovalType_2_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        lnkApp2.Visible = ddlApprovalType_2.SelectedValue == "6";

    }

    protected void btn_MoveTraverse_Click(object sender, EventArgs e)
    {
        //Common.Set_Procedures("TRANSFER_VENDOR_TO_POS");
        //Common.Set_ParameterLength(5);
        //Common.Set_Parameters
        //    (
        //        new MyParameter("@VRID", RequestId.ToString()),
        //        new MyParameter("@SupplierPort", ""),
        //        new MyParameter("@ApprovalType", lblApprovalType.Text),
        //        new MyParameter("@TravID", ddlTravVendor.SelectedValue),
        //        new MyParameter("@MultiCurr", ddlMultiCurr.SelectedValue));
        //DataSet ResDS = new DataSet();
        //Boolean Res = false;
        //Res = Common.Execute_Procedures_IUD(ResDS);
        //if (Res)
        //{
        //    int _SUPPLIERID = Common.CastAsInt32(ResDS.Tables[0].Rows[0][0]);
        //    btnSave_Account.Visible = false;
        //    ShowMessage("Record saved successfully.", false);
        //}
        //else
        //    ShowMessage("Record can not be saved.", true);


        Common.Set_Procedures("CREATEVENDOR");
        Common.Set_ParameterLength(8);
        Common.Set_Parameters
            (
                new MyParameter("@VRID", RequestId.ToString()),
                new MyParameter("@VENDORID", txtTravCode.Text),
                new MyParameter("@COUNTRY", ddlCountry.SelectedValue),
                new MyParameter("@TERMSCODE",""),
                new MyParameter("@DISCTCODE", ""),
                new MyParameter("@CURRENCYID", ddlCurrencyId.SelectedValue),
                new MyParameter("@TAXGRPID", ""),
                new MyParameter("@MuliCurr", ddlMultiCurr.SelectedValue));
        
        DataSet ResDS = new DataSet();
        Boolean Res = false;
        Res = Common.Execute_Procedures_IUD(ResDS);
        if (Res)
        {
            int _SUPPLIERID = Common.CastAsInt32(ResDS.Tables[0].Rows[0][0]);
            btnSave_Account.Visible = false;
            ShowMessage("Record saved successfully.", false);
        }
        else
            ShowMessage("Record can not be saved. Error : " + Common.ErrMsg, true);
    }
    protected void btnEvaluate1_Click(object sender, EventArgs e)
    {
        //----------------------------------------------
        if (radEval1.SelectedIndex < 0)
        {
            ShowMessage("Please select status.", true);
            radEval1.Focus();
            return;
        }
        else
        {
            if (radEval1.SelectedValue == "A")
            {
                if (txtValidTillDate1.Text.Trim() == "")
                {
                    ShowMessage("Please select status.", true);
                    radEval1.Focus();
                    return;
                }
            }
        }
        if (txtEvalRemarks1.Text.Trim() == "")
        {
            txtEvalRemarks1.Focus();
            ShowMessage("Please enter remarks.", true);
            return;
        }

        string AppServices = GetCheckBoxListSelections(chkAppServices1);

        Common.Set_Procedures("sp_Evaluation");
        Common.Set_ParameterLength(10);
        Common.Set_Parameters
            (
                new MyParameter("@VRID", RequestId.ToString()),
                new MyParameter("@MODE", 1),
                new MyParameter("@EvalResult", radEval1.SelectedValue),
                new MyParameter("@EvalBy", Session["FullName"].ToString()),
                new MyParameter("@EvalOn", DateTime.Today.ToString("dd-MMM-yyyy")),
                new MyParameter("@ApprovalType", ddlEvalType_1.SelectedValue),
                new MyParameter("@EvalValidTill", txtValidTillDate1.Text),
                new MyParameter("@EvalRemarks", txtEvalRemarks1.Text),
                new MyParameter("@ApprovedBusinesses", AppServices),
                new MyParameter("@Blacklist", chkBlackList1.Checked)

            );

        DataSet ResDS = new DataSet();
        Boolean Res = false;
        Res = Common.Execute_Procedures_IUD(ResDS);
        if (Res)
        {
            //int _SUPPLIERID = Common.CastAsInt32(ResDS.Tables[0].Rows[0][0]);
            ShowMessage("Record saved successfully.", false);
        }
        else
            ShowMessage("Record can not be saved.", true);
    }
    protected void btnEvaluate2_Click(object sender, EventArgs e)
    {
        //----------------------------------------------
        if (radEval2.SelectedIndex < 0)
        {
            ShowMessage("Please select status.", true);
            radEval2.Focus();
            return;
        }
        else
        {
            if (radEval2.SelectedValue == "A")
            {
                if (txtValidTillDate2.Text.Trim() == "")
                {
                    ShowMessage("Please select status.", true);
                    radEval2.Focus();
                    return;
                }
            }
        }
        if (txtEvalRemarks2.Text.Trim() == "")
        {
            txtEvalRemarks2.Focus();
            ShowMessage("Please enter remarks.", true);
            return;
        }
        Common.Set_Procedures("sp_Evaluation");
        Common.Set_ParameterLength(10);
        Common.Set_Parameters
            (
                new MyParameter("@VRID", RequestId.ToString()),
                new MyParameter("@MODE", 2),
                new MyParameter("@EvalResult", radEval2.SelectedValue),
                new MyParameter("@EvalBy", Session["FullName"].ToString()),
                new MyParameter("@EvalOn", DateTime.Today.ToString("dd-MMM-yyyy")),
                new MyParameter("@ApprovalType", ddlEvalType_2.SelectedValue),
                new MyParameter("@EvalValidTill", txtValidTillDate2.Text),
                new MyParameter("@EvalRemarks", txtEvalRemarks2.Text),
                new MyParameter("@ApprovedBusinesses", (object)DBNull.Value),
                new MyParameter("@Blacklist", chkBlacklist2.Checked)
            );

        DataSet ResDS = new DataSet();
        Boolean Res = false;
        Res = Common.Execute_Procedures_IUD(ResDS);
        if (Res)
        {
            //int _SUPPLIERID = Common.CastAsInt32(ResDS.Tables[0].Rows[0][0]);
            ShowMessage("Record saved successfully.", false);
        }
        else
            ShowMessage("Record can not be saved.", true);
    }
    //public void SendMail(string Title,string message, string emailid)
    //{
    //    string[] ToAdd = { emailid };
    //    string[] NoAdd = { "" };
    //    string[] BccAdd = { "emanager@energiossolutions.com" };
    //    string Message = "Dear User,<br><br>" + message + ".<br><br><br>Thanks<br>";
    //    //ProjectCommon.SendMailAsync(ToAdd, NoAdd, "MTM Ship Management : " + Title, Message, NoAdd);
    //}
    protected void radEval1_OnSelectIndexChanged(object sender, EventArgs e)
    {
        tr1.Visible = radEval1.SelectedIndex == 1;
        tr11.Visible = radEval1.SelectedIndex == 1;

        trBlackList1.Visible = radEval1.SelectedIndex == 2;
        chkBlackList1.Checked = false;
    }
    protected void radEval2_OnSelectIndexChanged(object sender, EventArgs e)
    {
        tr2.Visible = radEval2.SelectedIndex == 1;
        tr22.Visible = radEval2.SelectedIndex == 1;

        trBlacklist2.Visible = radEval2.SelectedIndex == 2;
        chkBlacklist2.Checked = false;
    }

    protected void lnkApp1_Click(object sender, EventArgs e)
    {
        modalBox.Visible = true;
        chkFleets.ClearSelection();
        chkVessels.ClearSelection();
        hfdNominationStage.Value = "1";
        dvNomination.Visible = true;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM tblVendorNominations WHERE VRID=" + RequestId + " AND ApprovalMode=1");
        if (dt.Rows.Count > 0)
        {
            string Fleets = dt.Rows[0]["Fleets"].ToString() + ",";
            string Vessels = dt.Rows[0]["Vessels"].ToString();
            foreach (ListItem li in chkFleets.Items)
            {
                if (Fleets.Contains(li.Value + ","))
                    li.Selected = true;
            }
            foreach (ListItem li in chkVessels.Items)
            {
                if (Vessels.Contains(li.Value + ","))
                    li.Selected = true;
            }
        }

        btnSaveNomination.Visible = btnSave_IstApproval.Visible || btnSave_IIndApproval.Visible;
    }
    protected void lnkApp2_Click(object sender, EventArgs e)
    {
        modalBox.Visible = true;
        chkFleets.ClearSelection();
        chkVessels.ClearSelection();
        hfdNominationStage.Value = "2";
        dvNomination.Visible = true;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM tblVendorNominations WHERE VRID=" + RequestId + " AND ApprovalMode=2");
        if (dt.Rows.Count > 0)
        {
            string Fleets = dt.Rows[0]["Fleets"].ToString() + ",";
            string Vessels = dt.Rows[0]["Vessels"].ToString();
            foreach (ListItem li in chkFleets.Items)
            {
                if (Fleets.Contains(li.Value + ","))
                    li.Selected = true;
            }
            foreach (ListItem li in chkVessels.Items)
            {
                if (Vessels.Contains(li.Value + ","))
                    li.Selected = true;
            }
        }
    }

    protected void btnSaveNomination_Click(object sender, EventArgs e)
    {
        if (hfdNominationStage.Value == "1")
        {
            string Fleets = "";
            foreach (ListItem li in chkFleets.Items)
                if (li.Selected)
                    Fleets += "," + li.Value;

            string Vessels = "";
            foreach (ListItem li in chkVessels.Items)
                if (li.Selected)
                    Vessels += "," + li.Value;

            if (Fleets.StartsWith(","))
                Fleets = Fleets.Substring(1);

            if (Vessels.StartsWith(","))
                Vessels = Vessels.Substring(1);

            Common.Execute_Procedures_Select_ByQuery("Exec dbo.Update_Vemndor_Nominations " + RequestId + ",1,'" + Fleets + "','" + Vessels + "'");
        }
        if (hfdNominationStage.Value == "2")
        {
            string Fleets = "";
            foreach (ListItem li in chkFleets.Items)
                if (li.Selected)
                    Fleets += "," + li.Value;

            string Vessels = "";
            foreach (ListItem li in chkVessels.Items)
                if (li.Selected)
                    Vessels += "," + li.Value;

            if (Fleets.StartsWith(","))
                Fleets = Fleets.Substring(1);

            if (Vessels.StartsWith(","))
                Vessels = Vessels.Substring(1);


            Common.Execute_Procedures_Select_ByQuery("Exec dbo.Update_Vemndor_Nominations " + RequestId + ",2,'" + Fleets + "','" + Vessels + "'");
        }
        modalBox.Visible = false;
        dvNomination.Visible = false;

        ShowMessage("Record saved successfully.", false);
    }
    protected void btnCloseNomination_Click(object sender, EventArgs e)
    {
        modalBox.Visible = false;
        dvNomination.Visible = false;
    }
    protected void ddlEvalType_1_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        lnkEval1.Visible = ddlEvalType_1.SelectedValue == "6";
    }
    protected void ddlEvalType_2_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        lnkEval2.Visible = ddlEvalType_2.SelectedValue == "6";
    }
    protected void lnkEval1_Click(object sender, EventArgs e)
    {
        modalBox.Visible = true;
        chkFleets.ClearSelection();
        chkVessels.ClearSelection();
        hfdNominationStage.Value = "1";
        dvNomination.Visible = true;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM tblVendorNominations WHERE VRID=" + RequestId + " AND ApprovalMode=1");
        if (dt.Rows.Count > 0)
        {
            string Fleets = dt.Rows[0]["Fleets"].ToString() + ",";
            string Vessels = dt.Rows[0]["Vessels"].ToString();
            foreach (ListItem li in chkFleets.Items)
            {
                if (Fleets.Contains(li.Value + ","))
                    li.Selected = true;
            }
            foreach (ListItem li in chkVessels.Items)
            {
                if (Vessels.Contains(li.Value + ","))
                    li.Selected = true;
            }
        }
    }
    protected void lnkEval2_Click(object sender, EventArgs e)
    {
        modalBox.Visible = true;
        chkFleets.ClearSelection();
        chkVessels.ClearSelection();
        hfdNominationStage.Value = "2";
        dvNomination.Visible = true;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM tblVendorNominations WHERE VRID=" + RequestId + " AND ApprovalMode=2");
        if (dt.Rows.Count > 0)
        {
            string Fleets = dt.Rows[0]["Fleets"].ToString() + ",";
            string Vessels = dt.Rows[0]["Vessels"].ToString();
            foreach (ListItem li in chkFleets.Items)
            {
                if (Fleets.Contains(li.Value + ","))
                    li.Selected = true;
            }
            foreach (ListItem li in chkVessels.Items)
            {
                if (Vessels.Contains(li.Value + ","))
                    li.Selected = true;
            }
        }
    }
    protected void btntest_Click(object sender, EventArgs e)
    {
        modalBox.Visible = true;
        modalframe_SelectVendors.Visible = true;
    }

    protected void btnTabNo_Click(object sender, EventArgs e)
    {
        int _PageNo = Common.CastAsInt32(hid_TabNo.Value);

        pnlA1.Visible = false;
        pnlA2.Visible = false;
        pnlA3.Visible = false;
        pnlA4.Visible = false;

        ((Panel)this.FindControl("pnlA" + _PageNo)).Visible = true;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "te", "SetTab2();SetActiveStageTab(" + ApprovalStageDone + ");", true);
    }
    protected void fd1_OnPreRender(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "te", "SetActiveStageTab(" + ApprovalStageDone + ");", true);
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        RestTopMenu();
    }
    protected void RestTopMenu()
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "te", "SetTab2();SetActiveStageTab(" + ApprovalStageDone + ");", true);
    }

    protected void updateAccountDetails(int RequestId, string travCode)
    {
        Common.Set_Procedures("CREATEVENDOR");
        Common.Set_ParameterLength(8);
        Common.Set_Parameters
            (
                new MyParameter("@VRID", RequestId.ToString()),
                new MyParameter("@VENDORID", travCode),
                new MyParameter("@COUNTRY", ddlCountry.SelectedValue),
                new MyParameter("@TERMSCODE", ""),
                new MyParameter("@DISCTCODE", ""),
                new MyParameter("@CURRENCYID", ddlCurrencyId.SelectedValue),
                new MyParameter("@TAXGRPID", ""),
                new MyParameter("@MuliCurr", ddlMultiCurr.SelectedValue));

        DataSet ResDS = new DataSet();
        Boolean Res = false;
        Res = Common.Execute_Procedures_IUD(ResDS);
        if (Res)
        {
            int _SUPPLIERID = Common.CastAsInt32(ResDS.Tables[0].Rows[0][0]);
            btnSave_Account.Visible = false;
           // ShowMessage("Record saved successfully.", false);
        }
        else
            ShowMessage("Record can not be saved. Error : " + Common.ErrMsg, true);
    }

}
