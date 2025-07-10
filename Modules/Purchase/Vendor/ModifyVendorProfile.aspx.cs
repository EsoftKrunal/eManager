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
    //-------------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        ClearMessage();
        if (!IsPostBack)
        {
                DataTable dtCountry = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.COUNTRY ORDER BY COUNTRYNAME");
                ddlCountry.DataSource = dtCountry;
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataValueField = "CountryId";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem(" < Select Country > ", "0"));

                ddlbank_counrty.DataSource = dtCountry;
                ddlbank_counrty.DataTextField = "CountryName";
                ddlbank_counrty.DataValueField = "CountryId";
                ddlbank_counrty.DataBind();
                ddlbank_counrty.Items.Insert(0, new ListItem(" < Select Country > ", "0"));

                //for binding vendor list----added by laxmi on 21-sep-2015
                DataTable dtVendorList = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.tblVendorBusinessesList ORDER BY Vendorlistname");
                chkVendorbusinesseslist.DataSource = dtVendorList;
                chkVendorbusinesseslist.DataTextField = "Vendorlistname";
                chkVendorbusinesseslist.DataValueField = "Vendorlistid";
                chkVendorbusinesseslist.DataBind();

                //end

                RequestId = Common.CastAsInt32(Request.QueryString["KeyId"]);
                PageNo = 1;
                ShowRecord();
        }
        else
        {

        }
    }
    protected void Move(bool Next)
    {
        if (Next)
            Move_Next();
        else
            Move_Back();
    }
    protected void Reset_navigation()
    {
        btnPrev.Visible = true;
        btnPrev1.Visible = true;
        btnNext.Visible = true;
        btnNext1.Visible = true;
        btnSubmit.Visible = false;

        if (PageNo == 1)
        {
            btnPrev.Visible = false;
            btnPrev1.Visible = false;

        }
        if (PageNo == 3)
        {
            btnNext.Visible = false;
            btnNext1.Visible = false;
            btnSubmit.Visible = true && (RequestAprovalStatus < 3);
        }
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
        if (Current == 2)
        {
            string str_businesseslist = GetCheckBoxListSelections(chkVendorbusinesseslist);
            if (str_businesseslist.Contains("22"))
            {
                if (txt_Services_Specify.Text.Trim() == "")
                {
                    ShowMessage("Please specify the services on description page.", true);
                    txt_Services_Specify.Focus();
                    return;
                }
            }
        }
        

        HtmlControl hc = (HtmlControl)this.FindControl("stg_" + Current);
        hc.Attributes.Add("class", "stage");
        this.FindControl("dv_Page_" + Current).Visible = false;

        this.FindControl("dv_Page_" + Next).Visible = true;
        hc = (HtmlControl)this.FindControl("stg_" + Next);
        hc.Attributes.Add("class", "stage active");

        PageNo = Next;
        Reset_navigation();

        //------------------------------- UPDATE FOR ALL SECTIONS
        //switch (PageNo)
        //{
        //    case 1:
        //        Update_Profile();
        //        break;
        //    case 2:
        //        Update_Description();
        //        break;
        //    case 3:
        //        Update_Size();
        //        break;
        //    case 4:
        //        Update_FinancialStatus();
        //        break;
        //    case 5:
        //        Update_Documents();
        //        break;
        //    case 6:
        //        Update_Reputation();
        //        break;
        //    case 7:
        //        Update_BankDetails();
        //        break;
        //}

    }

    protected void btnPageNo_Click(object sender, EventArgs e)
    {
        int _PageNo = Common.CastAsInt32(hid_PageNo.Value);
        Move_ToPage(PageNo, _PageNo);
        Reset_navigation();
    }
    protected void btnPrev_Click(object sender, EventArgs e)
    {
        Move(false);
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Move(true);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        
        Save_Data(2);
    }
    //protected void btnSaveAsDraft_Click(object sender, EventArgs e)
    //{

    //    Save_Data(2);
    //}
    protected void Save_Data(int REQUESTSTATUSVaue)
    {
        string str_businesseslist=GetCheckBoxListSelections(chkVendorbusinesseslist);
        if (str_businesseslist.Contains("22"))
        {
            if (txt_Services_Specify.Text.Trim() == "")
            {
                ShowMessage("Please specify the services on description page.",true);
                txt_Services_Specify.Focus();
                return;
            }
        }

        if (!string.IsNullOrWhiteSpace(ddlbank_counrty.SelectedItem.Text) && ddlbank_counrty.SelectedItem.Text.ToString().ToUpper() == "INDIA" && string.IsNullOrWhiteSpace(txtIFSCCode.Text))
        {
            ShowMessage("IFSC code is mandatory for India Country.", true);
            txtIFSCCode.Focus();
            return;
        }
        //for whole page
        Common.Set_Procedures("sp_UpdateVendorSupplierProfile_DataOnly");
        Common.Set_ParameterLength(84);
        Common.Set_Parameters(
            //profile

        new MyParameter("@VRID", Common.CastAsInt32(RequestId)),
        new MyParameter("@COMPANYNAME", txt_NoC.Text.Trim().Replace("'", "`")),
        new MyParameter("@COMPANYREGNO", txt_company_reg_no.Text.Trim().Replace("'", "`")),
        new MyParameter("@BUSINESSYEARS", Common.CastAsInt32(txt_no_of_year_business.Text)),
        new MyParameter("@TAXREGNO", txt_tax_reg_no.Text.Trim().Replace("'", "`")),
        new MyParameter("@ADDRESSLINE1", txt_address1.Text.Trim().Replace("'", "`")),
        new MyParameter("@ADDRESSLINE2", txt_address2.Text.Trim().Replace("'", "`")),
        new MyParameter("@CITY_STATE", txt_city_state.Text.Trim().Replace("'", "`")),
        new MyParameter("@ZIP_POSTALCODE", txt_zipcode.Text.Trim().Replace("'", "`")),
        new MyParameter("@COUNTRY", Common.CastAsInt32(ddlCountry.SelectedValue)),
        new MyParameter("@PhoneNo", txt_phone_no.Text.Trim() + "-" + txt_phone_no1.Text.Trim() + "-" + txt_phone_no2.Text.Trim()),
        new MyParameter("@FaxNo", txt_fax_no.Text.Trim() + "-" + txt_fax_no1.Text.Trim() + "-" + txt_fax_no2.Text.Trim()),
        new MyParameter("@EmailAddress", txt_email.Text.Trim().Replace("'", "`")),
        new MyParameter("@WebSite", txt_website.Text.Trim().Replace("'", "`")),
        new MyParameter("@ContactPersonName", txt_Contact_Person.Text.Trim().Replace("'", "`")),
        new MyParameter("@CP_Position", txt_Contact_Person_Position.Text.Trim().Replace("'", "`")),
        new MyParameter("@CP_DirectPhoneNo", txt_Contact_Person_DirectPhone.Text.Trim() + "-" + txt_Contact_Person_DirectPhone1.Text.Trim() + "-" + txt_Contact_Person_DirectPhone2.Text.Trim()),
        new MyParameter("@CP_MobileNo", txt_cp_mobile.Text.Trim() + "-" + txt_cp_mobile1.Text.Trim() + "-" + txt_cp_mobile2.Text.Trim()),
        new MyParameter("@CP_FaxNo", txt_Contact_Person_FaxNo.Text.Trim() + "-" + txt_Contact_Person_FaxNo1.Text.Trim() + "-" + txt_Contact_Person_FaxNo2.Text.Trim()),
        new MyParameter("@CP_Email", txt_Contact_Person_Email.Text.Trim().Replace("'", "`")),
        new MyParameter("@Remarks", ""),
            //new MyParameter("@REQUESTSTATUS", 2),
        new MyParameter("@HEAD_COMPANY", txt_head_of_company.Text.Trim().Replace("'", "`")),
        new MyParameter("@HC_POSITION", txt_head_of_company_postion.Text.Trim().Replace("'", "`")),
        new MyParameter("@HC_CONTACTNO", txt_head_of_company_no.Text.Trim() + "-" + txt_head_of_company_no1.Text.Trim() + "-" + txt_head_of_company_no2.Text.Trim()),
        new MyParameter("@HC_EMAIL", txt_head_of_company_email.Text.Trim().Replace("'", "`")),
        new MyParameter("@HEAD_FINANCE_COMPANY", txt_head_of_finance.Text.Trim().Replace("'", "`")),
        new MyParameter("@HF_CONTACTNO", txt_head_of_finance_no.Text.Trim() + "-" + txt_head_of_finance_no1.Text.Trim() + "-" + txt_head_of_finance_no2.Text.Trim()),
        new MyParameter("@HF_EMAIL", txt_head_of_finance_email.Text.Trim().Replace("'", "`")),
        new MyParameter("@HEAD_QUALITY_COMPANY", txt_head_of_quality.Text.Trim().Replace("'", "`")),
        new MyParameter("@HQ_CONTACTNO", txt_head_of_quality_no.Text.Trim() + "-" + txt_head_of_quality_no1.Text.Trim() + "-" + txt_head_of_quality_no2.Text.Trim()),
        new MyParameter("@HQ_EMAIL", txt_head_of_quality_email.Text.Trim().Replace("'", "`")),
        new MyParameter("@HO_ParentCompany", txt_head_office_name.Text.Trim().Replace("'", "`")),

        new MyParameter("@HO_City_State", txt_HO_City_State.Text.Trim().Replace("'", "`")),
        new MyParameter("@HO_Zip_Postal_Code", txt_HO_Zip_Postal_Code.Text.Trim().Replace("'", "`")),
        new MyParameter("@HO_Country", ddl_HO_Country.SelectedValue),
        new MyParameter("@HO_PhoneNo", txt_HO_PhoneNo.Text.Trim() + "-" + txt_HO_PhoneNo1.Text.Trim() + "-" + txt_HO_PhoneNo2.Text.Trim()),
        new MyParameter("@HO_FaxNo", txt_HO_FaxNo.Text.Trim() + "-" + txt_HO_FaxNo1.Text.Trim() + "-" + txt_HO_FaxNo2.Text.Trim()),
        new MyParameter("@HO_Email", txt_HO_Email.Text.Trim().Replace("'", "`")),
        new MyParameter("@HO_WebSite", txt_HO_WebSite.Text.Trim().Replace("'", "`")),

        new MyParameter("@HO_BusinessYears", Common.CastAsInt32(txt_no_of_year_business_HQPC.Text)),
        new MyParameter("@CompanyOwnerStruct", txt_CompanyOwnerStruct.Text.Trim().Replace("'", "`")),
        new MyParameter("@RequestApprovalStatus", 2),//when vedor request is finally subbmitted 

        //description
        new MyParameter("@COMPANYBUSINESSES", str_businesseslist),
        new MyParameter("@COMPANYBUSINESSES_OTHERS", txt_description_other.Text.Trim().Replace("'", "`")),
        new MyParameter("@AdditionalService_Specify", txt_Services_Specify.Text.Trim().Replace("'", "`")),
        new MyParameter("@AdditionalDescription", txt_Add_Description.Text.Trim().Replace("'", "`")),

        ////size
        //new MyParameter("@MGMT_EMPS", Common.CastAsInt32(txt_management.Text)),
        //new MyParameter("@ADMIN_EMPS", Common.CastAsInt32(txt_administrative.Text)),
        //new MyParameter("@PROF_EMPS", Common.CastAsInt32(txt_professional.Text)),
        //new MyParameter("@SKILLED_WORKERS", Common.CastAsInt32(txt_skilled_worker_with_c.Text)),
        //new MyParameter("@Skilled_Workers_without_certificate", Common.CastAsInt32(txt_skilled_worker_without_c.Text)),
        //new MyParameter("@UNSKILLED_LABOR", Common.CastAsInt32(txt_unskilled_worker.Text)),
        //new MyParameter("@OFFICE", Common.CastAsDecimal(txt_office.Text)),
        //new MyParameter("@WORKSHOP", Common.CastAsDecimal(txt_workshop.Text)),
        //new MyParameter("@WAREHOUSE", Common.CastAsDecimal(txt_wharehouse.Text)),
        //new MyParameter("@OTHERAREA", Common.CastAsDecimal(txt_other_area.Text)),
        //new MyParameter("@OWN", Common.CastAsInt32(txt_own.Text)),
        //new MyParameter("@AGENTS", Common.CastAsInt32(txt_agents_partners.Text)),
        //new MyParameter("@TOTALEMPS_WORLD", Common.CastAsInt32(txt_permanent_emp_worldwide.Text)),
        //new MyParameter("@IFSPC", rd_IsCompany_transact.SelectedValue),
        //new MyParameter("@EXPANSIONPLAN_LOCAL", txt_locally.Text.Trim().Replace("'", "`")),
        //new MyParameter("@EXPANSIONPLAN_GEO", txt_geographically.Text.Trim().Replace("'", "`")),

        ////reputation
        //new MyParameter("@COMPLY_HAS_COMP", rd_CompanyHasCompliance.SelectedValue),
        //new MyParameter("@COMPLY_IF_YES", txt_outcome.Text.Trim().Replace("'", "`")),
        //new MyParameter("@COMPLY_DOES_THE", rd_FCPA_requirements.SelectedValue),
        //new MyParameter("@Prod_Liability", rd_product_liability.SelectedValue),
        //new MyParameter("@LIAB_IF_YES", txt_insurance_company_name.Text.Trim().Replace("'", "`")),
        //new MyParameter("@AMOUNT", txt_insurance_company_amount.Text.Trim().Replace("'", "`")),
        //new MyParameter("@SERVICE_LIAB", rd_service_liability.SelectedValue),
        //new MyParameter("@IF_YES", txt_insurance_company_name_service.Text.Trim().Replace("'", "`")),
        //new MyParameter("@SER_AMOUNT", txt_insurance_company_amount_service.Text.Trim().Replace("'", "`")),
        //new MyParameter("@INSURANCE_ATT_SHIP", rd_emp_attending.SelectedValue),

        //size
        new MyParameter("@MGMT_EMPS", 0),
        new MyParameter("@ADMIN_EMPS", 0),
        new MyParameter("@PROF_EMPS", 0),
        new MyParameter("@SKILLED_WORKERS", 0),
        new MyParameter("@Skilled_Workers_without_certificate", 0),
        new MyParameter("@UNSKILLED_LABOR", 0),
        new MyParameter("@OFFICE", 0),
        new MyParameter("@WORKSHOP", 0),
        new MyParameter("@WAREHOUSE", 0),
        new MyParameter("@OTHERAREA", 0),
        new MyParameter("@OWN", 0),
        new MyParameter("@AGENTS", 0),
        new MyParameter("@TOTALEMPS_WORLD", 0),
        new MyParameter("@IFSPC", 0),
        new MyParameter("@EXPANSIONPLAN_LOCAL", ""),
        new MyParameter("@EXPANSIONPLAN_GEO", ""),

        //reputation
        new MyParameter("@COMPLY_HAS_COMP", 0),
        new MyParameter("@COMPLY_IF_YES", ""),
        new MyParameter("@COMPLY_DOES_THE", 0),
        new MyParameter("@Prod_Liability", 0),
        new MyParameter("@LIAB_IF_YES", ""),
        new MyParameter("@AMOUNT", ""),
        new MyParameter("@SERVICE_LIAB", 0),
        new MyParameter("@IF_YES", ""),
        new MyParameter("@SER_AMOUNT", ""),
        new MyParameter("@INSURANCE_ATT_SHIP", 0),

        //bank
        new MyParameter("@BANKNAME", txt_bank_name.Text.Trim().Replace("'", "`")),
        new MyParameter("@BANKADDRESSLINE1", txt_bank_address1.Text.Trim().Replace("'", "`")),
        new MyParameter("@BANKADDRESSLINE2", txt_bank_address2.Text.Trim().Replace("'", "`")),
        new MyParameter("@BANKPOSTCODE", txt_bank_postcode.Text.Trim().Replace("'", "`")),
        new MyParameter("@BANKCITY", txt_bank_city.Text.Trim().Replace("'", "`")),
        new MyParameter("@BANKCOUNTRY", Common.CastAsInt32(ddlbank_counrty.SelectedValue)),
        new MyParameter("@BANKACCOUNTNO", txt_bank_account_no.Text.Trim().Replace("'", "`")),
        new MyParameter("@BANKSWIFTCODE", txt_bank_swift_code.Text.Trim().Replace("'", "`")),
        new MyParameter("@BANKIBAN", txt_bank_IBAN.Text.Trim().Replace("'", "`")),
        new MyParameter("@BankIFSCCode", txtIFSCCode.Text.Trim().Replace("'", "`")),
        new MyParameter("@BANK_SINGLE_CURR", txt_pref_single.Text.Trim().Replace("'", "`")),
        new MyParameter("@MDPP", chkaccept.Checked?"Y":"")
        );
        DataSet dsprofile = new DataSet();
        if (Common.Execute_Procedures_IUD(dsprofile))
        {
            ShowMessage("Record Saved successfully.", false);
          
        }
        else
        {
            ShowMessage("Unable to save record. Error : " + Common.ErrMsg, true);
        }
    }

    public void ClearMessage()
    {       
        lblMessage.InnerHtml = "";
    }

    //for getting comma saperated values of checkbox--added by laxmi on 21-sept-2015
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
    //end
    protected void ShowRecord()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM dbo.tbl_VenderRequest WHERE VRID=" + RequestId + "");
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            //------------------------------------------
            //profile 

            txt_NoC.Text = dr["COMPANYNAME"].ToString();
            txt_company_reg_no.Text = dr["CompanyRegNo"].ToString();
            txt_no_of_year_business.Text = dr["BusinessYears"].ToString();
            txt_tax_reg_no.Text = dr["TaxRegNo"].ToString();
            txt_address1.Text = dr["AddressLine1"].ToString();
            txt_address2.Text = dr["AddressLine2"].ToString();
            txt_city_state.Text = dr["City_State"].ToString();
            txt_zipcode.Text = dr["ZIP_PostalCode"].ToString();
            ddlCountry.SelectedValue = dr["Country"].ToString();
            char[] sep = { '-' };

            string[] parts = dr["PhoneNo"].ToString().Split(sep);
            if (sep.Length > 0)
                try { txt_phone_no.Text = parts[0]; txt_phone_no1.Text = parts[1]; txt_phone_no2.Text = parts[2]; }
                catch { }

            parts = dr["FaxNo"].ToString().Split(sep);
            if (sep.Length > 0)
                try { txt_fax_no.Text = parts[0]; txt_fax_no1.Text = parts[1]; txt_fax_no2.Text = parts[2]; }
                catch { }

            txt_email.Text = dr["EmailAddress"].ToString();
            txt_website.Text = dr["WebSite"].ToString();
            txt_Contact_Person.Text = dr["ContactPersonName"].ToString();
            txt_Contact_Person_Position.Text = dr["CP_Position"].ToString();

            parts = dr["CP_DirectPhoneNo"].ToString().Split(sep);
            if (sep.Length > 0)
                try { txt_Contact_Person_DirectPhone.Text = parts[0]; txt_Contact_Person_DirectPhone1.Text = parts[1]; txt_Contact_Person_DirectPhone2.Text = parts[2]; }
                catch { }

            parts = dr["CP_MobileNo"].ToString().Split(sep);
            if (sep.Length > 0)
                try { txt_cp_mobile.Text = parts[0]; txt_cp_mobile1.Text = parts[1]; txt_cp_mobile2.Text = parts[2]; }
                catch { }

            parts = dr["CP_FaxNo"].ToString().Split(sep);
            if (sep.Length > 0)
                try { txt_Contact_Person_FaxNo.Text = parts[0]; txt_Contact_Person_FaxNo1.Text = parts[1]; txt_Contact_Person_FaxNo2.Text = parts[2]; }
                catch { }

            txt_Contact_Person_Email.Text = dr["CP_Email"].ToString();

            txt_head_of_company.Text = dr["Head_Company"].ToString();
            txt_head_of_company_postion.Text = dr["HC_Position"].ToString();

            parts = dr["HC_ContactNo"].ToString().Split(sep);
            if (sep.Length > 0)
                try { txt_head_of_company_no.Text = parts[0]; txt_head_of_company_no1.Text = parts[1]; txt_head_of_company_no2.Text = parts[2]; }
                catch { }

            txt_head_of_company_email.Text = dr["HC_Email"].ToString();
            txt_head_of_finance.Text = dr["Head_Finance_Company"].ToString();

            parts = dr["HF_ContactNo"].ToString().Split(sep);
            if (sep.Length > 0)
                try { txt_head_of_finance_no.Text = parts[0]; txt_head_of_finance_no1.Text = parts[1]; txt_head_of_finance_no2.Text = parts[2]; }
                catch { }

            txt_head_of_finance_email.Text = dr["HF_Email"].ToString();
            txt_head_of_quality.Text = dr["Head_Quality_Company"].ToString();

            parts = dr["HQ_ContactNo"].ToString().Split(sep);
            if (sep.Length > 0)
                try { txt_head_of_quality_no.Text = parts[0]; txt_head_of_quality_no1.Text = parts[1]; txt_head_of_quality_no2.Text = parts[2]; }
                catch { }

            txt_head_of_quality_email.Text = dr["HQ_Email"].ToString();
            txt_no_of_year_business.Text = dr["BusinessYears"].ToString();
            txt_head_office_name.Text = dr["HO_ParentCompany"].ToString();

            txt_HO_City_State.Text = dr["HO_City_State"].ToString();
            txt_HO_Zip_Postal_Code.Text = dr["HO_Zip_Postal_Code"].ToString();
            ddl_HO_Country.Text = dr["HO_Country"].ToString();

            parts = dr["HO_PhoneNo"].ToString().Split(sep);
            if (sep.Length > 0)
                try { txt_HO_PhoneNo.Text = parts[0]; txt_HO_PhoneNo1.Text = parts[1]; txt_HO_PhoneNo2.Text = parts[2]; }
                catch { }

            parts = dr["HO_FaxNo"].ToString().Split(sep);
            if (sep.Length > 0)
                try { txt_HO_FaxNo.Text = parts[0]; txt_HO_FaxNo1.Text = parts[1]; txt_HO_FaxNo2.Text = parts[2]; }
                catch { }


            txt_HO_Email.Text = dr["HO_Email"].ToString();
            txt_HO_WebSite.Text = dr["HO_WebSite"].ToString();
            txt_no_of_year_business_HQPC.Text = dr["HO_BusinessYears"].ToString();
            txt_CompanyOwnerStruct.Text = dr["CompanyOwnerStruct"].ToString();
            //---------------------------------------------------------------------

            //description
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

            
            //bank
            txt_bank_name.Text = dr["BANKNAME"].ToString();
            txt_bank_address1.Text = dr["BANKADDRESSLINE1"].ToString();
            txt_bank_address2.Text = dr["BANKADDRESSLINE2"].ToString();
            txt_bank_postcode.Text = dr["BANKPOSTCODE"].ToString();
            txt_bank_city.Text = dr["BANKCITY"].ToString();
            ddlbank_counrty.SelectedValue = dr["BANKCOUNTRY"].ToString();
            //txt_bank_account_no.Text = dr["BANKACCOUNTNO"].ToString();
            txt_bank_account_no.Attributes.Add("value", dr["BANKACCOUNTNO"].ToString());
            txt_bank_swift_code.Text = dr["BANKSWIFTCODE"].ToString();
            txt_bank_IBAN.Text = dr["BANKIBAN"].ToString();
            txt_pref_single.Text = dr["BANK_SINGLE_CURR"].ToString();
            txtIFSCCode.Text = dr["BankIFSCCode"].ToString();
            chkaccept.Checked = Convert.ToString(dr["MDPP"]) == "Y";
            if (chkaccept.Checked)
            {
                lblacceptedon.Text = " Accepted On : " + Common.ToDateString(dr["MDPPAcceptedOn"]);
            }
            //-----------------------------------------------------------------------  

            if (dr["SupplierId"].ToString() != "")
            {
                txt_NoC.Enabled = false;
                txt_email.Enabled = false;
            }

        }
    }
    public void ShowMessage(string Msg, bool Error)
    {
        lblMessage.Visible = true;
        if(Error)
            lblMessage.InnerHtml= Msg + " Error :" + Common.ErrMsg;
        else
            lblMessage.InnerHtml = Msg;
        lblMessage.Attributes.Add("class", (Error) ? "msgbox error" : "msgbox success"); 
    }
}
