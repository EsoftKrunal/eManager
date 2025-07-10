using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Reflection;
using System.IO;
using System.Data.SqlClient;
using ShipSoft.CrewManager.DataAccessLayer;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.Operational;

public partial class Modules_HRD_Applicant_ShipjobAppform : System.Web.UI.Page
{
    public int SelectedDisc
    {
        get
        {
            return Common.CastAsInt32(ViewState["SelComm"]);
        }
        set
        {
            ViewState["SelComm"] = value;
        }
    }
    public int candidateid
    {
        get { return Common.CastAsInt32(ViewState["candidateid"]); }
        set { ViewState["candidateid"] = value; }
    }
    int LoginId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
       // lblDateMess.Visible = false;
        this.lbl_info.Text = "";
       
        try
        {
            LoginId = Convert.ToInt32(Session["loginid"].ToString());
        }
        catch
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "alert('Your session has been expired.\\n Please login again.');window.close();", true);
        }
        try
        {
            if (!IsPostBack)
            {

                CalendarExtender1.StartDate = DateTime.Now;
                //CalendarExtender11.StartDate = DateTime.Now;
                //CalendarExtender11.EndDate = DateTime.Now;
                //DataTable DT = Common.Execute_Procedures_Select_ByQuery(" Select top 1 CompanyName,LogoImageName from Company where DefaultCompany = 'Y'");
                //if (DT != null && DT.Rows.Count > 0)
                //{
                //    //lblCompanyName.Text = DT.Rows[0]["CompanyName"].ToString();
                //    string imgName = DT.Rows[0]["LogoImageName"].ToString();
                //    string logoPath = ConfigurationManager.AppSettings["Logopath"].ToString();
                //    string imgFullpath = logoPath + imgName;
                //    imgbtn.ImageUrl = imgFullpath;
                //}
                BindddlNationality();
                BindddlRank();
                BindVesselType();
                BindShoeSizeDropDown();
                BindShirtSizeDropDown();
                BindMaritalStatusDropDown();
            }
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "alert('Error : "+ex.Message.ToString()+"');window.close();", true);
        }
    }

    protected void BindddlNationality()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT convert(varchar,CountryId) as CountryId,NationalityName as CountryName from Country where statusid='A'  \r\n order by CountryName");
        this.ddlNat.DataTextField = "CountryName";
        this.ddlNat.DataValueField = "CountryId";
        this.ddlNat.DataSource = dt;
        this.ddlNat.DataBind();
        ddlNat.Items.Insert(0, new ListItem("< Select >", "0"));


        DataTable dtCountry = Common.Execute_Procedures_Select_ByQuery(" SELECT CountryId,CountryName,CountryCode from Country where statusid='A' order by CountryName");
        this.ddl_P_Country.DataTextField = "CountryName";
        this.ddl_P_Country.DataValueField = "CountryId";
        this.ddl_P_Country.DataSource = dtCountry;
        this.ddl_P_Country.DataBind();
        ddl_P_Country.Items.Insert(0, new ListItem("< Select >", "0"));
    }

    protected void BindddlRank()
    {
        this.ddlRank.DataTextField = "RankCode";
        this.ddlRank.DataValueField = "RankId";
        this.ddlRank.DataSource = Common.Execute_Procedures_Select_ByQuery("Select RankId,LTRIM(RTRIM(RankName)) +' ('+ LTRIM(RTRIM(RankCode)) + ')' As RankCode from DBO.Rank with(nolock) where statusid='A' and RankId Not In (48,49) order By RankLevel");
        this.ddlRank.DataBind();
        ddlRank.Items.Insert(0, new ListItem("< Select >", "0"));
    }

    protected void BindVesselType()
    {
        string vesseltypes = ConfigurationManager.AppSettings["VesselType"].ToString();
        this.chkVeselType.DataTextField = "VesselTypeName";
        this.chkVeselType.DataValueField = "VesselTypeId";
        this.chkVeselType.DataSource = Common.Execute_Procedures_Select_ByQuery("Select VesselTypeId,VesselTypeName from DBO.VesselType Where VesselTypeid in (" + vesseltypes + ") order By VesselTypeName");
        this.chkVeselType.DataBind();
    }

    protected void BindNearestAirport(int countryid)
    {
        this.dd_nearest_airport.DataTextField = "NearestAirportName";
        this.dd_nearest_airport.DataValueField = "NearestAirportId";
        this.dd_nearest_airport.DataSource = Common.Execute_Procedures_Select_ByQuery(" SELECT NearestAirportId,NearestAirportName from NearestAirport where CountryId=" + countryid + " and statusid='A'");
        this.dd_nearest_airport.DataBind();

    }

    protected void BindCountryCode(int countryid)
    {
        this.ddl_P_CountryCode_Mobile.DataTextField = "CountryCode";
        this.ddl_P_CountryCode_Mobile.DataValueField = "CountryId";
        this.ddl_P_CountryCode_Mobile.DataSource = Common.Execute_Procedures_Select_ByQuery(" SELECT CountryId,CountryCode from Country where CountryId=" + countryid + " and statusid='A'");
        this.ddl_P_CountryCode_Mobile.DataBind();

    }

    private void BindShoeSizeDropDown()
    {
        FieldInfo[] thisEnumFields = typeof(ShoeSize).GetFields();
        foreach (FieldInfo thisField in thisEnumFields)
        {
            if (!thisField.IsSpecialName && thisField.Name.ToLower() != "notset")
            {
                int thisValue = (int)thisField.GetValue(0);
                ddl_Shoes.Items.Add(new ListItem(thisField.Name, thisValue.ToString()));
            }
        }
    }

    private void BindShirtSizeDropDown()
    {
        FieldInfo[] thisEnumFields = typeof(ShirtSize).GetFields();
        foreach (FieldInfo thisField in thisEnumFields)
        {
            if (!thisField.IsSpecialName && thisField.Name.ToLower() != "notset")
            {
                int thisValue = (int)thisField.GetValue(0);
                ddl_Shirt.Items.Add(new ListItem(thisField.Name, thisValue.ToString()));
            }
        }
    }

    private void BindMaritalStatusDropDown()
    {
        FieldInfo[] thisEnumFields = typeof(MaritalStatus).GetFields();
        foreach (FieldInfo thisField in thisEnumFields)
        {
            if (!thisField.IsSpecialName && thisField.Name.ToLower() != "notset")
            {
                int thisValue = (int)thisField.GetValue(0);
                ddmaritalstatus.Items.Add(new ListItem(thisField.Name, thisValue.ToString()));
            }
        }
    }

    protected void ddl_P_Country_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindNearestAirport(Convert.ToInt32(ddl_P_Country.SelectedValue));
        ddl_P_CountryCode_Mobile.Items.Clear();
        BindCountryCode(Convert.ToInt32(ddl_P_Country.SelectedValue));
        if (ddl_P_Country.SelectedItem.Text.ToUpper().Trim() == "INDIA")
        {
            trIndos.Visible = true;
            rfvIndocNo.Visible = true;
            rfvIndocIssueDt.Visible = true;
            rfvIndocIssuePlace.Visible = true;
           // rfvIndocExpiry.Visible = true;
            //revCDCExpiry.Visible = true;
            //revCDCIssueDt.Visible = true;
            revIndocExpiry.Visible = true;
        }
        else
        {
            trIndos.Visible = false;
            rfvIndocNo.Visible = false;
            rfvIndocIssueDt.Visible = false;
            rfvIndocIssuePlace.Visible = false;
           // rfvIndocExpiry.Visible = false;
            //revCDCExpiry.Visible = false;
            //revCDCIssueDt.Visible = false;
            revIndocExpiry.Visible = false;
        }
    }
    //protected void ValidateCaptcha(object sender, ServerValidateEventArgs e)
    //{
    //    e.IsValid = false;
    //    if (!string.IsNullOrEmpty(txtCaptcha.Text.Trim()))
    //    {
    //        cptCaptcha.ValidateCaptcha(txtCaptcha.Text.Trim());
    //        e.IsValid = cptCaptcha.UserValidated;
           
    //    }
    //}

    public static string getLocation()
    {
        DataAccessBase dataAccessBase = new DataAccessBase();
        DataBaseHelper dataBaseHelper = new DataBaseHelper("ExecQuery");
        SqlParameter[] parameters = new SqlParameter[1]
        {
                new SqlParameter("Query", "Select BranchName from Branch Where Self=1")
        };
        object obj = dataBaseHelper.RunScalar(dataAccessBase.ConnectionString, parameters);
        return obj.ToString().Substring(0, 1);
    }

    protected void btn_Upload_Click(object sender, EventArgs e)
    {
        try
        {
            if (FileUpload1.PostedFile != null && FileUpload1.FileContent.Length > 0)
            {
                if (FileUpload1.PostedFile.ContentLength > 102400) //--- : 2 Mb = 2097152 bytes
                {
                    lbl_info.Text = "File Size is Too big! Maximum Allowed is 100Kb...";
                    return;
                }
                //updFileName = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
                //updFileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
                //saveFile = updFileName + DateTime.Now.ToString("ddMMyyyy_HHmmss") + updFileExt;
                //updFilePath = Server.MapPath("~\\EMANAGERBLOB\\HRD\\Applicant\\CV\\" + saveFile);
                //FileUpload1.SaveAs(updFilePath);
                string text = "";
                text = getLocation() + "C" + "_" + Path.GetRandomFileName() + ".jpg";
                string SaveImgLocation = Server.MapPath("~\\EMANAGERBLOB\\HRD\\CrewPhotos\\");
                string SaveImgPath = SaveImgLocation + text;
                //string text2 = Server.MapPath("~\\EMANAGERBLOB\\HRD\\Applicant\\CV\\");
                //string filename = Path.Combine(text2, text);
                FileInfo fileInfo2 = new FileInfo(SaveImgPath);
                if (fileInfo2.Exists)
                {
                    fileInfo2.Delete();
                }
                FileUpload1.SaveAs(SaveImgPath);
                UtilityManager um = new UtilityManager();
                if (! string.IsNullOrEmpty(text))
                {
                    img_Crew.ImageUrl = um.DownloadFileFromServer(text, "C");
                    ViewState["TempPhotoPath"] = text;
                }
                else
                {
                    img_Crew.ImageUrl = um.DownloadFileFromServer("noimage.jpg", "C");
                }
               
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "dfsd", "alert('Please upload photo first.');", true);
                FileUpload1.Focus();
                return;
            }
        }
        catch (Exception ex)
        {
            lbl_info.Text = ex.Message.ToString();
        }
    }

    protected void clearControls()
    {
        candidateid = 0;
        ddlRank.SelectedIndex = 0;
        txtAvalFrom.Text = "";
        txtFirstName.Text = "";
        txtMiddleName.Text = "";
        txtLastName.Text = "";
        txtDOB.Text = "";
        txtPOB.Text = "";
        ddlNat.SelectedIndex = 0;
        txtAddressPE1.Text = "";
        txtAddressPE2.Text = "";
        txtAddressPE3.Text = "";
        txtCityPE.Text = "";
        txtPincode.Text = "";
        txtStatePE.Text = "";
        txtHeight.Text = "";
        txtWeight.Text = "";
        txtwaist.Text = "";
        txtSuitSize.Text = "";
        ddl_P_Country.SelectedIndex = 0;
        ddl_P_CountryCode_Mobile.SelectedIndex = 0;
        ddl_Shirt.SelectedIndex = 0;
        ddl_Shoes.SelectedIndex = 0;
        ddmaritalstatus.SelectedIndex = 0;
        dd_nearest_airport.SelectedIndex = 0;
        txt_P_EMail1.Text = "";
        ViewState["TempPhotoPath"] = "";
        img_Crew.ImageUrl = "";
        txtIndocNo.Text = "";
        txtIndocExpiry.Text = "";
        txtIndocIssueDt.Text = "";
        txtIndocIssuePlace.Text = "";
        txtPassportNo.Text = "";
        txtPassportExpiry.Text = "";
        txtPassportIssueDt.Text = "";
        txtPassportIssuePlace.Text = "";
        txtMobileNoPE.Text = "";
        txtCdcNo.Text = "";
        txtCDCExpiry.Text = "";
        txtCDCIssueDate.Text = "";
        txtCDCIssuePlace.Text = "";
        radSex.SelectedValue = "1";
        chkVeselType.Items.Clear();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtPassportNo.Text.Trim() == "")
            {
                // lbl_info.Text = "Please enter Passport#.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "dfsd", "alert('Please enter Passport#.');", true);
                txtPassportNo.Focus();
                return;
            }

            if (ViewState["TempPhotoPath"] == null || ViewState["TempPhotoPath"].ToString() == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "dfsd", "alert('Please upload photo.');", true);
                FileUpload1.Focus();
                return;
            }

            //Validation for Passport Check
            string PassNo = "";
            candidateid = 0;
            if (candidateid <= 0)
                PassNo = "select PassportNo from DBO.CandidatePersonalDetails with(nolock) where PassportNo ='" + txtPassportNo.Text.Trim() + "'";
            else
                PassNo = "select PassportNo from DBO.CandidatePersonalDetails with(nolock) where PassportNo ='" + txtPassportNo.Text.Trim() + "' and CandidateID <>" + candidateid + "";

            DataTable dtPassNo = Common.Execute_Procedures_Select_ByQuery(PassNo);
            if (dtPassNo.Rows.Count > 0)
            {
                //lbl_info.Text = "Passport# already exists.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "dfsd", "alert('Passport# already exists.');", true);
                // txtPassportNo.Text = "";
                txtPassportNo.Focus();
                return;
            }

            //Validation for email Check

            string Email = "";
            if (candidateid <= 0)
                Email = "select EmailId from DBO.CandidatePersonalDetails with(nolock) where EmailId ='" + txt_P_EMail1.Text.Trim() + "'";
            else
                Email = "select EmailId from DBO.CandidatePersonalDetails with(nolock) where EmailId ='" + txt_P_EMail1.Text.Trim() + "' and CandidateID <>" + candidateid + "";

            DataTable dtEmail = Common.Execute_Procedures_Select_ByQuery(Email);
            if (dtEmail.Rows.Count > 0)
            {
                // lbl_info.Text = "Email address already exists.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "dfsd", "alert('Email address already exists.');", true);
                txt_P_EMail1.Focus();
                return;
            }

            //Validation for contact Check

            //string ContactNo = "";
            //if (candidateid <= 0)
            //    ContactNo = "select ContactNo from DBO.CandidatePersonalDetails where ContactNo ='" + txtPhone.Text.Trim() + "'";
            //else
            //    ContactNo = "select ContactNo from DBO.CandidatePersonalDetails where ContactNo ='" + txtPhone.Text.Trim() + "' and CandidateID <>" + candidateid + "";

            //DataTable dtContactNo = Budget.getTable(ContactNo).Tables[0];
            //if (dtContactNo.Rows.Count > 0)
            //{
            //    lbl_info.Text = "ContactNo already exists.";
            //    return;
            //}
            //-----------------------------------------------------------------

            if (txtMobileNoPE.Text.Trim() == "")
            {
                // lbl_info.Text = "Please enter Mobile#.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "dfsd", "alert('Please enter Mobile#.');", true);
                txtMobileNoPE.Focus();
                return;
            }
            //--------------------
            string Vesseltypes = "";
            foreach (ListItem li in chkVeselType.Items)
            {
                if (li.Selected)
                    Vesseltypes = Vesseltypes + "," + li.Value;
            }
            if (Vesseltypes.StartsWith(","))
            {
                Vesseltypes = Vesseltypes.Substring(1);
            }
            if (string.IsNullOrEmpty(Vesseltypes))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "dfsd", "alert('Please select at least One Vessel Experience.');", true);
                chkVeselType.Focus();
                return;
            }
            //------------------------
            string updFileName = "", updFileExt = "", updFilePath = "", saveFile = "";
            if (FileUpload2.PostedFile != null && FileUpload2.FileContent.Length > 0)
            {
                if (FileUpload2.PostedFile.ContentLength > 2097152) //--- : 2 Mb = 2097152 bytes
                {
                    lbl_info.Text = "Max Attachment Size is 2 Mb.";
                    return;
                }

                updFileName = System.IO.Path.GetFileNameWithoutExtension(FileUpload2.FileName);
                updFileExt = System.IO.Path.GetExtension(FileUpload2.FileName);
                saveFile = updFileName + DateTime.Now.ToString("ddMMyyyy_HHmmss") + updFileExt;
                if (saveFile.Length > 49)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "dfsd", "alert('Upload CV Attachment Name is too large.');", true);
                    return;
                }
                updFilePath = Server.MapPath("~\\EMANAGERBLOB\\HRD\\Applicant\\CV\\" + saveFile);
             //   string cvpath = ConfigurationManager.AppSettings["CVPath"].ToString();
             //   updFilePath = updFilePath + saveFile;
                FileUpload2.SaveAs(updFilePath);
            }
           


            //------------------------
            string STR = "";
            if (candidateid > 0)
            {
                if (updFileName.Trim() == "")
                {
                    STR = "UPDATE DBO.CandidatePersonalDetails " +
                    "SET " +
                        "FIRSTNAME='" + txtFirstName.Text.Trim() + "', " +
                        "MIDDLENAME='" + txtMiddleName.Text.Trim() + "', " +
                        "LASTNAME='" + txtLastName.Text.Trim() + "', " +
                        "NATIONALITYID=" + ddlNat.SelectedValue + ", " +
                        "DATEOFBIRTH='" + txtDOB.Text.Trim() + "', " +
                        "RANKAPPLIEDID=" + ddlRank.SelectedValue + ", " +
                        "Gender='" + radSex.SelectedValue + "', " +
                        "CONTACTNO='" + txtMobileNoPE.Text.Trim() + "', " +
                        "State='" + txtStatePE.Text.Trim() + "', " +
                        "PINCode='" + txtPincode.Text.Trim() + "', " +
                        "EMAILID='" + txt_P_EMail1.Text.Trim() + "', " +
                        "ADDRESS='" + txtAddressPE1.Text.Trim() + "', " +
                        "Address2='" + txtAddressPE2.Text.Trim() + "', " +
                        "Address3='" + txtAddressPE3.Text.Trim() + "', " +
                        "CITY='" + txtCityPE.Text.Trim() + "', " +
                        "CountryId=" + ddl_P_Country.SelectedValue.Trim() + ", " +
                        "MobileCountryId=" + ddl_P_CountryCode_Mobile.SelectedValue.Trim() + ", " +
                        "Height='" + txtHeight.Text.Trim() + "', " +
                        "Weight='" + txtWeight.Text.Trim() + "', " +
                        "Waist='" + txtwaist.Text.Trim() + "', " +
                        "BolierSuitSize='" + txtSuitSize.Text.Trim() + "', " +
                         "ShoeSizeId=" + ddl_Shoes.SelectedValue.Trim() + ", " +
                         "ShirtSizeId=" + ddl_Shirt.SelectedValue.Trim() + ", " +
                          "NearestAirportCountryId=" + dd_nearest_airport.SelectedValue.Trim() + ", " +
                          "PASSPORTNO='" + txtPassportNo.Text.Trim() + "', " +
                           "IssueDate='" + txtPassportIssueDt.Text.Trim() + "', " +
                           "ExpiryDate='" + txtPassportExpiry.Text.Trim() + "', " +
                          "PassportIssuePlace='" + txtPassportIssuePlace.Text.Trim() + "', " +
                          "CDCNo='" + txtCdcNo.Text.Trim() + "', " +
                          "CDCIssueDate='" + txtCDCIssueDate.Text.Trim() + "', " +
                          "CDCExpiryDate='" + txtCDCExpiry.Text.Trim() + "', " +
                          "CDCIssuePlace='" + txtCDCIssuePlace.Text.Trim() + "', " +
                           "IndosNo='" + txtIndocNo.Text.Trim() + "', " +
                          "IndosIssueDate='" + txtIndocIssueDt.Text.Trim() + "', " +
                          "IndosExpiryDate='" + txtIndocExpiry.Text.Trim() + "', " +
                          "IndosIssuePlace='" + txtIndocIssuePlace.Text.Trim() + "', " +
                        "VESSELTYPES='" + Vesseltypes + "', " +
                        "[ModifiedById] = '" + Common.CastAsInt32(Session["loginid"]).ToString() + "', " +
                        "[ModifiedBy] = '" + Session["UserName"].ToString() + "', " +
                        "[ModifiedOn] = GETDATE(), " +
                        "[MaritalStatusId] = '" + ddmaritalstatus.SelectedValue + "' " +
                        "WHERE CANDIDATEID=" + candidateid.ToString() + " ; SELECT " + candidateid.ToString();
                }
                else
                {
                    STR = "UPDATE DBO.CandidatePersonalDetails " +
                    "SET " +
                        "FIRSTNAME='" + txtFirstName.Text.Trim() + "', " +
                        "MIDDLENAME='" + txtMiddleName.Text.Trim() + "', " +
                        "LASTNAME='" + txtLastName.Text.Trim() + "', " +
                        "NATIONALITYID=" + ddlNat.SelectedValue + ", " +
                        "DATEOFBIRTH='" + txtDOB.Text.Trim() + "', " +
                        "RANKAPPLIEDID=" + ddlRank.SelectedValue + ", " +
                        "Gender='" + radSex.SelectedValue + "', " +
                        "CONTACTNO='" + txtMobileNoPE.Text.Trim() + "', " +
                        "State='" + txtStatePE.Text.Trim() + "', " +
                        "PINCode='" + txtPincode.Text.Trim() + "', " +
                        "EMAILID='" + txt_P_EMail1.Text.Trim() + "', " +
                        "ADDRESS='" + txtAddressPE1.Text.Trim() + "', " +
                        "Address2='" + txtAddressPE2.Text.Trim() + "', " +
                        "Address3='" + txtAddressPE3.Text.Trim() + "', " +
                        "CITY='" + txtCityPE.Text.Trim() + "', " +
                        "CountryId=" + ddl_P_Country.SelectedValue.Trim() + ", " +
                        "MobileCountryId=" + ddl_P_CountryCode_Mobile.SelectedValue.Trim() + ", " +
                        "Height='" + txtHeight.Text.Trim() + "', " +
                        "Weight='" + txtWeight.Text.Trim() + "', " +
                        "Waist='" + txtwaist.Text.Trim() + "', " +
                        "BolierSuitSize='" + txtSuitSize.Text.Trim() + "', " +
                         "ShoeSizeId=" + ddl_Shoes.SelectedValue.Trim() + ", " +
                         "ShirtSizeId=" + ddl_Shirt.SelectedValue.Trim() + ", " +
                          "NearestAirportCountryId=" + dd_nearest_airport.SelectedValue.Trim() + ", " +
                           "PASSPORTNO='" + txtPassportNo.Text.Trim() + "', " +
                           "IssueDate='" + txtPassportIssueDt.Text.Trim() + "', " +
                           "ExpiryDate='" + txtPassportExpiry.Text.Trim() + "', " +
                          "PassportIssuePlace='" + txtPassportIssuePlace.Text.Trim() + "', " +
                          "CDCNo='" + txtCdcNo.Text.Trim() + "', " +
                          "CDCIssueDate='" + txtCDCIssueDate.Text.Trim() + "', " +
                          "CDCExpiryDate='" + txtCDCExpiry.Text.Trim() + "', " +
                          "CDCIssuePlace='" + txtCDCIssuePlace.Text.Trim() + "', " +
                           "IndosNo='" + txtIndocNo.Text.Trim() + "', " +
                          "IndosIssueDate='" + txtIndocIssueDt.Text.Trim() + "', " +
                          "IndosExpiryDate='" + txtIndocExpiry.Text.Trim() + "', " +
                          "IndosIssuePlace='" + txtIndocIssuePlace.Text.Trim() + "', " +
                        "VESSELTYPES='" + Vesseltypes + "', " +
                        "FILENAME='" + saveFile.Trim() + "' " +
                        "[ModifiedById] = '" + Common.CastAsInt32(Session["loginid"]).ToString() + "', " +
                        "[ModifiedBy] = '" + Session["UserName"].ToString() + "', " +
                        "[ModifiedOn] = GETDATE()," +
                        "[MaritalStatusId] = '" + ddmaritalstatus.SelectedValue + "' " +
                        "WHERE CANDIDATEID=" + candidateid.ToString() + " ; SELECT " + candidateid.ToString();
                }

            }
            else
            {
                STR = "INSERT INTO DBO.CandidatePersonalDetails( " +
                        "[RankAppliedId] " +
                        ",[AvailableFrom] " +
                        ",[FirstName] " +
                        ",[MiddleName] " +
                        ",[LastName] " +
                        ",[Gender] " +
                        ",[DateOfBirth] " +
                        ",[PlaceOfBirth] " +
                        ",[NationalityId] " +
                        ",[EmailId] " +
                        ",[ContactNo] " +
                        ",[ContactNo2] " +
                        ",[CreatedBy] " +
                        ",[CreatedOn] " +
                        ",[VesselTypes] " +
                        ",[FileName] " +
                        ",[Status] " +
                        ",[Address] " +
                        ",[Address2] " +
                        ",[Address3] " +
                        ",[CountryId] " +
                        ",[State] " +
                        ",[City] " +
                        ",[PINCode] " +
                        ",[MobileCountryId] " +
                        ",[NearestAirportCountryId] " +
                        ",[PassportNo] " +
                        ",[IssueDate] " +
                        ",[ExpiryDate] " +
                        ",[PassportIssuePlace] " +
                        ",[IndosNo] " +
                        ",[IndosIssueDate] " +
                        ",[IndosExpiryDate] " +
                        ",[IndosIssuePlace] " +
                        ",[CDCNo] " +
                        ",[CDCIssueDate] " +
                        ",[CDCExpiryDate] " +
                        ",[CDCIssuePlace] " +
                        ",[Height] " +
                        ",[Weight] " +
                        ",[Waist] " +
                        ",[BolierSuitSize] " +
                        ",[ShoeSizeId] " +
                        ",[ShirtSizeId] " +
                        ",[PhotoPath] " +
                        ",[MaritalStatusId] " +
                        ",[ModifiedById] " +
                        ",[ModifiedBy] " +
                        ",[ModifiedOn] " +
                        ",[IsUpdated] )" +
                          " VALUES(" + ddlRank.SelectedValue.Trim() + ",'" + txtAvalFrom.Text.Trim() + "','" + txtFirstName.Text.Trim()
                           + "','" + txtMiddleName.Text.Trim() + "','" + txtLastName.Text.Trim()
                           + "'," + radSex.SelectedValue + ",'" + txtDOB.Text.Trim() + "','" + txtPOB.Text.Trim() + "'," + ddlNat.SelectedValue
                           + ",'" + txt_P_EMail1.Text.Trim() + "','" + txtMobileNoPE.Text.Trim() + "','" + txtMobileNoPE.Text.Trim()
                           + "',"+ Convert.ToInt32(Session["loginid"].ToString()) + ",GETDATE(),'" + Vesseltypes
                           + "','" + saveFile.Trim() + "',1,'" + txtAddressPE1.Text.Trim() + "','" + txtAddressPE2.Text.Trim()
                           + "','" + txtAddressPE3.Text.Trim() + "'," + ddl_P_Country.SelectedValue + ",'" + txtStatePE.Text.Trim()
                           + "','" + txtCityPE.Text.Trim() + "','" + txtPincode.Text.Trim() + "'," + ddl_P_CountryCode_Mobile.SelectedValue
                           + "," + dd_nearest_airport.SelectedValue
                           + ",'" + txtPassportNo.Text.Trim() + "','" + txtPassportIssueDt.Text.Trim() + "','" + txtPassportExpiry.Text.Trim()
                           + "','" + txtPassportIssuePlace.Text.Trim()
                           + "','" + txtIndocNo.Text.Trim() + "','" + txtIndocIssueDt.Text.Trim() + "','" + txtIndocExpiry.Text.Trim()
                           + "','" + txtIndocIssuePlace.Text.Trim()
                           + "','" + txtCdcNo.Text.Trim() + "','" + txtCDCIssueDate.Text.Trim() + "','" + txtCDCExpiry.Text.Trim()
                           + "','" + txtCDCIssuePlace.Text.Trim()
                           + "','" + txtHeight.Text.Trim() + "','" + txtWeight.Text.Trim() + "','" + txtwaist.Text.Trim()
                           + "','" + txtSuitSize.Text.Trim() + "'," + ddl_Shoes.SelectedValue + ",'" + ddl_Shirt.SelectedValue
                           + "','" + ViewState["TempPhotoPath"] + "'," + ddmaritalstatus.SelectedValue
                           + ",'" + Common.CastAsInt32(Session["loginid"]).ToString() + "','" + Session["UserName"].ToString() + "', GETDATE(), 0); SELECT MAX(CANDIDATEID) FROM DBO.CandidatePersonalDetails with(nolock) ";
            }
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(STR);
            if (dt != null && dt.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "dfsd", "alert('New Applicant saved successfully.');RefreshParent();window.close();", true);
                clearControls();
            }

            //if(dt!=null )
            //    if (dt.Rows.Count > 0)
            //    {
            //        candidateid = Common.CastAsInt32(dt.Rows[0][0].ToString());

            //        Budget.getTable("EXEC DBO.GENEREATEAPPROVALID " + candidateid + " "); 
            //        lbl_info.Text = "Applicant Created successfully.";
            //    }
        }
        catch (Exception ex)
        {
            lbl_info.Text = "Unable to update record. System Messge : " + ex.Message;
        }
    }
    public void RefreshParent()
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ref", "window.opener.refreshPage();", true);
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            clearControls();
        }
        catch(Exception ex)
        {
            lbl_info.Text = ex.Message.ToString();
        }
    }
}