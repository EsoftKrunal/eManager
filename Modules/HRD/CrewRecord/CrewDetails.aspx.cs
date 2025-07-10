using System;
using System.Data;
using System.Configuration;
using System.Reflection;
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


public partial class CrewDetails : System.Web.UI.Page
{
    public Authority Auth;
    public string Mode;
    public int CandidateId
    {
        set { ViewState["CandidateId"] = value; }
        get { return Common.CastAsInt32(ViewState["CandidateId"]); }
    }
    public int CrewFamilyId
    {
        set { ViewState["CrewFamilyId"] = value; }
        get { return Common.CastAsInt32(ViewState["CrewFamilyId"]); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //Alerts.SetHelp(imgHelp);
        
        btn_AddNew.Visible = false;
        doc_Alert.Text = "";
        crm_Alert.Text = "";
        this.img_Crew.Attributes.Add("onclick", "javascript:Show_Image_Large(this);");
        this.img_Crew1.Attributes.Add("onclick", "javascript:Show_Image_Large(this);");
        this.img_Crew2.Attributes.Add("onclick", "javascript:Show_Image_Large(this);");
        this.img_Crew3.Attributes.Add("onclick", "javascript:Show_Image_Large(this);");
        this.img_FamilyMember.Attributes.Add("onclick", "javascript:Show_Image_Large(this);");

        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
        if (chpageauth <= 0)
        {
            Response.Redirect("../AuthorityError.aspx");
        }
        //*******************

        //=========== Code for Checking the Critical Remark
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
        //===========

        //=========== Code for Checking the CRM Alert
        try
        {
            DataTable dtalert = Alerts.getCRMAlertofCrewMember(Session["CrewId"].ToString());
            foreach (DataRow dr in dtalert.Rows)
            {
                if (Convert.ToInt32(dr[0].ToString()) > 0)
                {
                    crm_Alert.Text = "Alert";
                }
            }
        }
        catch
        {
            crm_Alert.Text = "";
        }
        //===========

        //=========== Code for Checking the Document Alert
        try
        {
            DataTable dtalert2 = Alerts.getDocumentAlertofCrewMember(Session["CrewId"].ToString());
            if (dtalert2.Rows.Count > 0)
            {
                doc_Alert.Text = "Alert";
            }
            else
            {
                doc_Alert.Text = "";
            }
        }
        catch
        {
            doc_Alert.Text = "";
        }
        //===========

        // CODE FOR UDATING THE AUTHORITY
        ProcessCheckAuthority Obj = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
        Obj.Invoke();
        Session["Authority"] = Obj.Authority;
        Auth = (Authority)Session["Authority"];
        //--
        try
        {
            if (Request.QueryString["Mode"] != null)
            {
                Mode = Request.QueryString["Mode"].ToString();
            }
            else
            {
                try
                {
                    HiddenPK.Value = Session["CrewId"].ToString();
                    Mode = Session["Mode"].ToString();
                }
                catch
                {
                    if (Auth.isAdd)
                    {
                        Mode = "New";
                        HiddenPK.Value = "";
                    }
                    else
                    {
                        Response.Redirect("CrewSearch.aspx");
                    }
                }
            }        
        }
        catch
        {
            
        }

        #region GENERAL BLOCK
        lblMessage.Text = "";
        lbl_GridView3.Text = "";
        lbl_GridView3_1.Text = "";
        lbl_Gvvisa.Text = "";
        lbl_Gvfamily.Text = "";
        try
        {
            if (Session["UserName"] == "")
            {
                Response.Redirect("Login.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
            return;
        }
        if (!(IsPostBack))
        {

            BindSexTypeDropDown();
            BindNationalityDropDown();
            BindMaritalStatusDropDown();
            BindCountryOfBirth();
            BindRecruitingOffice();
            BindRankApplied();
            bindcountryname();
            BindQualification();
            BindShoeSizeDropDown();
            BindShirtSizeDropDown();
            BindVesselType();
            BindSignOffReason();
            BindBloodGroup();
            // Tab-3
            BindRelationDropDown();
            BindSexDropDown();
            BindTypeOfRemittanceDropDown();
            bindfamilycountryname();
            BindNationalityDropDown();
            BindBloodGroup();
            BindBankTypeOfRemittanceDropDown();
            //----
            #region AUTHORITY BLOCK
            HANDLE_AUTHORITY(0);
            UtilityManager um = new UtilityManager();
            img_Crew.ImageUrl = um.DownloadFileFromServer("noimage.jpg", "C");

            if (Mode != "New" && HiddenPK.Value.Trim() != "")
            {
                ProcessSelectCrewMemberPersonalDetailsById obj = new ProcessSelectCrewMemberPersonalDetailsById();
                CrewMember Member = new CrewMember();
                Member.Id = Convert.ToInt32(HiddenPK.Value.Trim());
                obj.CrueMember = Member;
                obj.Invoke();
                HANDLE_AUTHORITY(0);
                Show_Record(obj.CrueMember);
            }
            #endregion

            if (Mode == "New")
            {
                CandidateId = Common.CastAsInt32(Request.QueryString["CandidateId"]);
                DataTable dtCandidate = Budget.getTable("select FirstName,MiddleName,LastName,Gender,DateOfBirth,Nationalityid,PlaceOfBirth,PassportNo,QualificationId, address,city,ContactNo , ContactNo2,EmailId, IndosNo, MaritalStatusId, Height, Weight, Waist, BolierSuitSize, ShoeSizeId, ShirtSizeId, Photopath, Address2, Address3, CountryId, State, PINCode, MobileCountryId, NearestAirportCountryId,AvailableFrom,RankAppliedId  from DBO.CANDIDATEPERSONALDETAILS where candidateid=" + CandidateId.ToString()).Tables[0];
                if (dtCandidate.Rows.Count > 0)
                {
                    DataRow dr = dtCandidate.Rows[0];
                    txt_FirstName.Text = dr["firstname"].ToString();
                    txt_MiddleName.Text = dr["MiddleName"].ToString();
                    txt_LastName.Text = dr["LastName"].ToString();
                    txt_DOB.Text = Common.ToDateString(dr["DateOfBirth"]);
                    txtplaceofbirth.Text = dr["PlaceOfBirth"].ToString();
                    ddl_Sex.SelectedValue = dr["Gender"].ToString();
                    txt_Passport.Text = dr["PassportNo"].ToString();
                    ddl_Nationality.SelectedValue = dr["Nationalityid"].ToString();
                    ddcountryofbirth.SelectedValue = dr["Nationalityid"].ToString();
                    ddl_Academy_Quali.SelectedValue = dr["QualificationId"].ToString();
                    ddrankapp.SelectedValue = dr["RankAppliedId"].ToString();
                    txtdatefirstjoin.Text = Common.ToDateString(dr["AvailableFrom"]);
                    ddl_P_Country.SelectedValue = dr["Nationalityid"].ToString();
                    txt_P_Address.Text = dr["address"].ToString();
                    txt_P_Address1.Text = dr["Address2"].ToString();
                    txt_P_Address2.Text = dr["Address3"].ToString();
                    txt_P_City.Text = dr["city"].ToString();
                    txt_P_Number_Mobile.Text   = dr["ContactNo2"].ToString();
                    txt_P_Number_Tel.Text = dr["ContactNo"].ToString();
                    txt_P_EMail1.Text = dr["EmailId"].ToString();
                    txt_P_State.Text = dr["State"].ToString();
                    txt_P_City.Text = dr["city"].ToString();
                    txt_P_Pin.Text = dr["PINCode"].ToString();
                    if (!string.IsNullOrEmpty(dr["IndosNo"].ToString()))
                    {
                        txt_INDOS.Text = dr["IndosNo"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["MaritalStatusId"].ToString()))
                    {
                        ddmaritalstatus.SelectedValue = dr["MaritalStatusId"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["Height"].ToString()))
                    {
                        txtheight.Text = dr["Height"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["Weight"].ToString()))
                    {
                        txtweight.Text = dr["Weight"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["Waist"].ToString()))
                    {
                        txtwaist.Text = dr["Waist"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["BolierSuitSize"].ToString()))
                    {
                        txt_Bmi.Text = dr["BolierSuitSize"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["ShoeSizeId"].ToString()))
                    {
                        ddl_Shoes.SelectedValue = dr["ShoeSizeId"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dr["ShirtSizeId"].ToString()))
                    {
                        ddl_Shirt.SelectedValue = dr["ShirtSizeId"].ToString();
                    }
                    if (dr["Photopath"].ToString().Trim() == "")
                    {
                        img_Crew.ImageUrl = um.DownloadFileFromServer("noimage.jpg", "C");
                        hfd_fileName.Value = "";
                    }
                    else
                    {
                        img_Crew.ImageUrl = um.DownloadFileFromServer(dr["Photopath"].ToString().Trim(), "C");
                        hfd_fileName.Value = dr["Photopath"].ToString().Trim();
                    }

                    lblName.Text = txt_FirstName.Text + " " + txt_MiddleName.Text + " " + txt_LastName.Text;
                    txt_DOB_TextChanged(sender, e);
                    string Mess = "";
                    if (! string.IsNullOrWhiteSpace(dr["CountryId"].ToString()))
                    {
                        ddl_P_Country.SelectedValue = dr["CountryId"].ToString();
                        Mess = Mess + Alerts.Set_DDL_Value(ddl_P_Country, dr["CountryId"].ToString(), "Permanent Country");
                        LoadNearestAirport_P(Convert.ToInt32(ddl_P_Country.SelectedValue));
                        Mess = Mess + Alerts.Set_DDL_Value(ddl_P_Airport, dr["NearestAirportCountryId"].ToString(), "Permanent Nearest Airport");
                    }
                    ddl_P_CountryCode_Tel.Items.Clear();
                    ddl_P_CountryCode_Mobile.Items.Clear();
                    ddl_P_CountryCode_Fax.Items.Clear();
                    ProcessSelectCountryCode obj1 = new ProcessSelectCountryCode();
                    obj1.CountryId = Convert.ToInt32(ddl_P_Country.SelectedValue);
                    obj1.Invoke();
                    ddl_P_CountryCode_Tel.Items.Add(new ListItem(obj1.CountryCode, ddl_P_Country.SelectedValue));
                    ddl_P_CountryCode_Mobile.Items.Add(new ListItem(obj1.CountryCode, ddl_P_Country.SelectedValue));
                    ddl_P_CountryCode_Fax.Items.Add(new ListItem(obj1.CountryCode, ddl_P_Country.SelectedValue));
                    ddl_P_CountryCode_Mobile.SelectedValue = dr["MobileCountryId"].ToString();
                    txt_P_Number_Mobile.Text = dr["ContactNo"].ToString();  
                    txt_P_EMail1.Text = dr["EmailId"].ToString();
                }
            }
            Session["DtlSelectedTab"] = "0";
            
        }

        b1.CssClass = "btn1";
        b2.CssClass = "btn1";
        b3.CssClass = "btn1";
        b4.CssClass = "btn1";
       // btn_Print.CssClass = "btn1";
        if (Session["DtlSelectedTab"] == null)
        {
            Session["DtlSelectedTab"] = 0;
        }
        switch (Common.CastAsInt32(Session["DtlSelectedTab"]))
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
            case 4:
                btn_Print.CssClass = "selbtn";
                break;
            default:
                break;
        }

        #endregion
    }
    #region PAGE LOAD BINGINGS
    private void BindSexTypeDropDown()
    {
        FieldInfo[] thisEnumFields = typeof(SexType).GetFields();
        foreach (FieldInfo thisField in thisEnumFields)
        {
            if (!thisField.IsSpecialName && thisField.Name.ToLower() != "notset")
            {
                int thisValue = (int)thisField.GetValue(0);
                ddl_Sex.Items.Add(new ListItem(thisField.Name, thisValue.ToString()));
            }
        }
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
    private void BindNationalityDropDown()
    {
        ProcessSelectNationality obj = new ProcessSelectNationality();
        obj.Invoke();
        ddl_Nationality.DataSource = obj.ResultSet.Tables[0];
        ddl_Nationality.DataTextField = "CountryName";
        ddl_Nationality.DataValueField = "CountryId";
        ddl_Nationality.DataBind();

        ddnationality.DataValueField = "CountryId";
        ddnationality.DataTextField = "CountryName";
        ddnationality.DataSource = obj.ResultSet.Tables[0];
        ddnationality.DataBind();
    }
    private void BindCountryOfBirth()
    {
        ProcessGetCountry processgetcountry = new ProcessGetCountry();
        try
        {
            processgetcountry.Invoke();
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message.ToString());
        }

        ddcountryofbirth.DataValueField = "CountryId";
        ddcountryofbirth.DataTextField = "CountryName";
        ddcountryofbirth.DataSource = processgetcountry.ResultSet;
        ddcountryofbirth.DataBind();
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
    private void BindRecruitingOffice()
    {
        ProcessGetRecruitingOffice processgetrecruitingoffice = new ProcessGetRecruitingOffice();
        try
        {
            processgetrecruitingoffice.Invoke();
        }
        catch (Exception ex)
        {

        }
        ddrecruitingoff.DataValueField = "RecruitingOfficeId";
        ddrecruitingoff.DataTextField = "RecruitingOfficeName";
        ddrecruitingoff.DataSource = processgetrecruitingoffice.ResultSet;
        ddrecruitingoff.DataBind();
    }
    private void BindRankApplied()
    {
        ProcessGetRankApplied processgetrankapplied = new ProcessGetRankApplied();
        try
        {
            processgetrankapplied.Invoke();
        }
        catch (Exception ex)
        {

        }
        ddrankapp.DataValueField = "RankId";
        ddrankapp.DataTextField = "RankName";
        ddrankapp.DataSource = processgetrankapplied.ResultSet;
        ddrankapp.DataBind();

        // Tabp Page -4(Experience)
        ddl_Rank1.DataValueField = "RankId";
        ddl_Rank1.DataTextField = "RankName";
        ddl_Rank1.DataSource = processgetrankapplied.ResultSet;
        ddl_Rank1.DataBind();
    }
    private void BindQualification()
    {
        ProcessSelectQualification processgetrankapplied = new ProcessSelectQualification();
        try
        {
            processgetrankapplied.Invoke();
        }
        catch (Exception ex)
        {

        }
        ddl_Academy_Quali.DataValueField = "QualificationId";
        ddl_Academy_Quali.DataTextField = "QualificationName";
        ddl_Academy_Quali.DataSource = processgetrankapplied.ResultSet;
        ddl_Academy_Quali.DataBind();
    }
    private void BindVesselType()
    {
        ProcessSelectVesselType vesseletype = new ProcessSelectVesselType();
        try
        {
            vesseletype.Invoke();
        }
        catch (Exception ex)
        {

        }
        ddl_VesselType.DataValueField = "VesselTypeId";
        ddl_VesselType.DataTextField = "VesselTypeName";
        ddl_VesselType.DataSource = vesseletype.ResultSet.Tables[0];
        ddl_VesselType.DataBind();
    }
    private void BindSignOffReason()
    {
        ProcessSelectSignOffReason vesseletype = new ProcessSelectSignOffReason();
        try
        {
            vesseletype.Invoke();
        }
        catch (Exception ex)
        {

        }
        ddl_Sign_Off_Reason.DataValueField = "SignOffReasonId";
        ddl_Sign_Off_Reason.DataTextField = "SignOffReason";
        ddl_Sign_Off_Reason.DataSource = vesseletype.ResultSet.Tables[0];
        ddl_Sign_Off_Reason.DataBind();
    }
    private void BindBloodGroup()
    {
        ProcessGetBloodGroup objblood = new ProcessGetBloodGroup();
        objblood.Invoke();
        ddl_BloodGroup.DataSource = objblood.ResultSet.Tables[0];
        ddl_BloodGroup.DataTextField = "BloodGroupName";
        ddl_BloodGroup.DataValueField = "BloodGroupId";
        ddl_BloodGroup.DataBind();
    }
    // Tab-3
    private void BindRelationDropDown()
    {
        FieldInfo[] thisEnumFields = typeof(Relationship).GetFields();
        foreach (FieldInfo thisField in thisEnumFields)
        {
            if (!thisField.IsSpecialName && thisField.Name.ToLower() != "notset")
            {
                int thisValue = (int)thisField.GetValue(0);
                ddrelation.Items.Add(new ListItem(thisField.Name, thisValue.ToString()));
            }
        }
    }
    

    private void BindSexDropDown()
    {
        FieldInfo[] thisEnumFields = typeof(SexType).GetFields();
        foreach (FieldInfo thisField in thisEnumFields)
        {
            if (!thisField.IsSpecialName && thisField.Name.ToLower() != "notset")
            {
                int thisValue = (int)thisField.GetValue(0);
                ddsex.Items.Add(new ListItem(thisField.Name, thisValue.ToString()));
            }
        }
    }
    private void BindTypeOfRemittanceDropDown()
    {
        FieldInfo[] thisEnumFields = typeof(TypeOfRemittance).GetFields();
        foreach (FieldInfo thisField in thisEnumFields)
        {
            if (!thisField.IsSpecialName && thisField.Name.ToLower() != "notset")
            {
                int thisValue = (int)thisField.GetValue(0);
                ddtypeofremittance.Items.Add(new ListItem(thisField.Name, thisValue.ToString()));
            }
        }
    }

    private void bindfamilycountryname()
    {
        ProcessGetCountry getcountry = new ProcessGetCountry();
        getcountry.Invoke();
        ddcountryname.DataValueField = "CountryId";
        ddcountryname.DataTextField = "CountryName";
        ddcountryname.DataSource = getcountry.ResultSet;
        ddcountryname.DataBind();
    }
    #endregion
    protected void Menu1_MenuItemClick(object sender, EventArgs e)
    {
        // int i = 0;
        b1.CssClass = "btn1";
        b2.CssClass = "btn1";
        b3.CssClass = "btn1";
        b4.CssClass = "btn1";
        btnBank.CssClass = "btn1";
        btn_Print.CssClass = "btn1";
        if (HiddenPK.Value.Trim() == "")
        {
            lblMessage.Text = "Please Enter Personal Details First.";
            return;
        }
        Button btn = (Button)sender;
        int i = Common.CastAsInt32(btn.CommandArgument);
        Session["DtlSelectedTab"] = i;
       
        //---------------------------------

        //int iMenuCount = Menu1.Items.Count;

        //for (; i < iMenuCount; i++)
        //{
        //    this.Menu1.Items[i].ImageUrl = this.Menu1.Items[i].ImageUrl.Replace("_a.gif", "_d.gif");
        //}
        //this.Menu1.Items[Int32.Parse(e.Item.Value)].ImageUrl = this.Menu1.Items[Int32.Parse(e.Item.Value)].ImageUrl.Replace("_d.gif", "_a.gif");
        //---------------------------------

        if (i == 0)
        {
            this.MultiView1.Visible = true;
            this.MultiView1.ActiveViewIndex = i;
            b1.CssClass = "selbtn";
            //***********Code to check page acessing Permission
            int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
            if (chpageauth <= 0)
            {
                Response.Redirect("../AuthorityError.aspx");

            }
            //*******************

            ProcessSelectCrewMemberPersonalDetailsById obj = new ProcessSelectCrewMemberPersonalDetailsById();
            CrewMember Member = new CrewMember();
            Member.Id = Convert.ToInt32(HiddenPK.Value.Trim());
            obj.CrueMember = Member;
            obj.Invoke();
            HANDLE_AUTHORITY(0);
            Show_Record(obj.CrueMember);
        }
        else if (i == 1)
        {
            this.MultiView1.Visible = true;
            this.MultiView1.ActiveViewIndex = i;
            b2.CssClass = "selbtn";
            //***********Code to check page acessing Permission
            int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
            if (chpageauth <= 0)
            {
                Response.Redirect("../AuthorityError.aspx");

            }
            //*******************

            ProcessSelectCrewMemberContactDetailsById obj = new ProcessSelectCrewMemberContactDetailsById();
            CrewContact Member = new CrewContact();
            Member.CrewId = Convert.ToInt32(HiddenPK.Value.Trim());
            obj.ContactDetails = Member;
            obj.Invoke();
            Show_Record1(Member);
            HANDLE_AUTHORITY(1);
        }
        else if (i == 2)
        {
            this.MultiView1.Visible = true;
            this.MultiView1.ActiveViewIndex = i;
            b3.CssClass = "selbtn";
            //***********Code to check page acessing Permission
            int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
            if (chpageauth <= 0)
            {
                Response.Redirect("../AuthorityError.aspx");

            }
            //*******************
            BindGridfamily(Convert.ToInt32(HiddenPK.Value.Trim()));
            //Gvfamily.SelectedIndex = -1;

            HANDLE_AUTHORITY(2);
        }
        else if (i == 3)
        {
            this.MultiView1.Visible = true;
            this.MultiView1.ActiveViewIndex = i;
            b4.CssClass = "selbtn";
            //***********Code to check page acessing Permission
            int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
            if (chpageauth <= 0)
            {
                Response.Redirect("../AuthorityError.aspx");

            }
            //*******************
            BindExperienceDetailsGrid(GridView3_1.Attributes["MySort"], Convert.ToInt32(HiddenPK.Value.Trim()));
            GridView3_1.SelectedIndex = -1;
            pnl_Experience.Visible = false;
            HANDLE_AUTHORITY(3);
        }
        else if (i == 5)
        {
            this.MultiView1.Visible = false;
            btn_Print.CssClass = "selbtn";
        }
        else if (i == 4)
        {
            this.MultiView1.Visible = true;
            this.MultiView1.ActiveViewIndex = i;
            btnBank.CssClass = "selbtn";
            //***********Code to check page acessing Permission
            int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
            if (chpageauth <= 0)
            {
                Response.Redirect("../AuthorityError.aspx");

            }
            //*******************
            BindBankDetailsGrid( Convert.ToInt32(HiddenPK.Value.Trim()));
            GvBankDetails.SelectedIndex = -1;
            PnlBankDetails.Visible = false;
            HANDLE_AUTHORITY(4);
           // txtACNo.Attributes.Add("value", txtACNo.Text);
           // txtReAcNo.Attributes.Add("value", txtReAcNo.Text);
        }

        //else if (i == 4)
        //{
        //    BindCrewApprovals();
        //}
        //else if (i == 5)
        //{
        //    // BindCrewAssessments();    
        //}
    }
    private void HANDLE_AUTHORITY(int index)
    {
        //btn_Print.Visible = Auth.isPrint;
        //btn_Print1.Visible = Auth.isPrint;
        //btnfamilyPrint.Visible = Auth.isPrint;
        //btn_Experience_Print.Visible = Auth.isPrint;
        //btn_Experience_Print.Visible = Auth.isPrint;
        if (Mode == "New")
        {
            FileUpload1.Visible = true;
            // PAGE LEVEL BUTTONS
            btn_AddNew.Visible = (HiddenPK.Value.Trim() != "");
            // btn_Print.Visible = Auth.isPrint;
            //TAB LEVEL BUTTONS
            //TAB-1(PERSONAL DETAILS)
            if (index == 0) { btn_Save.Visible = true; }
            //TAB-1 CONTACT DETAILS
            if (index == 1) { btn_Save1.Visible = true; }
            //TAB-3 FAMILY DETAILS

            familypanel.Visible = false;
            panelvisa1.Visible = false;
            panelvisa2.Visible = false;

            btn_Family_Reset.Visible = Auth.isEdit || Auth.isAdd;
            btnvisaadd.Visible = false;

            btnfamilyCancel.Visible = false;
            btnvisacancel.Visible = false;

            btn_family_save.Visible = false;
            btnvisasave.Visible = false;


            //TAB-4 EXPERIENCE DETAILS
            if (index == 3)
            {
                btn_Add3.Visible = Auth.isEdit || Auth.isAdd;
                btn_Save3.Visible = false;
                btn_Experience_Cancel.Visible = false;
                pnl_Experience.Visible = false;
            }
            // Tab-7 Bank Details
            if (index == 4)
            {
                btnAddBankInfo.Visible = Auth.isEdit || Auth.isAdd;
                btnSaveBankInfo.Visible = false;
                btnCancelBankInfo.Visible = false;
                PnlBankDetails.Visible = false;
            }

        }
        else if (Mode == "Edit")
        {
            //Activate_All();
            // PAGE LEVEL BUTTONS
            btn_AddNew.Visible = false;
            FileUpload1.Visible = true;
            //btn_Print.Visible = Auth.isPrint;
            //TAB LEVEL BUTTONS

            //TAB-1(PERSONAL DETAILS)
            if (index == 0) { btn_Save.Visible = true; }
            //TAB-1 CONTACT DETAILS
            if (index == 1) { btn_Save1.Visible = true; }
            //TAB-3 FAMILY DETAILS

            familypanel.Visible = false;
            panelvisa1.Visible = false;
            panelvisa2.Visible = false;

            btn_Family_Reset.Visible = Auth.isEdit || Auth.isAdd;
            btnvisaadd.Visible = false;

            btnfamilyCancel.Visible = false;
            btnvisacancel.Visible = false;

            btn_family_save.Visible = false;
            btnvisasave.Visible = false;

            //TAB-4 EXPERIENCE DETAILS
            if (index == 3)
            {
                btn_Add3.Visible = Auth.isEdit || Auth.isAdd;
                btn_Save3.Visible = false;
                btn_Experience_Cancel.Visible = false;
                pnl_Experience.Visible = false;
            }

            // Tab-7 Bank Details
            if (index == 4)
            {
                btnAddBankInfo.Visible = Auth.isEdit || Auth.isAdd;
                btnSaveBankInfo.Visible = false;
                btnCancelBankInfo.Visible = false;
                PnlBankDetails.Visible = false;
            }

        }
        else // Mode=View
        {
            //Deactivate_All();
            // PAGE LEVEL BUTTONS
            FileUpload1.Visible = false;
            btn_AddNew.Visible = false;
            //btn_Print.Visible = false ;
            //TAB LEVEL BUTTONS

            //TAB-1(PERSONAL DETAILS)
            btn_Save.Visible = false;
            //TAB-1 CONTACT DETAILS
            btn_Save1.Visible = false;
            //TAB-3 FAMILY DETAILS

            familypanel.Visible = false;
            panelvisa1.Visible = false;
            panelvisa2.Visible = false;

            btn_Family_Reset.Visible = false;
            btnvisaadd.Visible = false;

            btnfamilyCancel.Visible = false;
            btnvisacancel.Visible = false;

            btn_family_save.Visible = false;
            btnvisasave.Visible = false;

            //TAB-4 EXPERIENCE DETAILS
            btn_Add3.Visible = false;
            btn_Save3.Visible = false;
            btn_Experience_Cancel.Visible = false;

            // TAB-5 BANK DETAILS

            btnAddBankInfo.Visible = false;
            btnSaveBankInfo.Visible = false;
            btnCancelBankInfo.Visible = false;
      
        }
        btn_AddNew.Visible = false;
    }
    protected void btn_AddNew_Click(object sender, EventArgs e)
    {
        CrewMember Member = new CrewMember();
        HiddenPK.Value = "";
        Show_Record(Member);
        Session["CrewId"] = null;
        btn_AddNew.Visible = false;
    }
    # region Tab-1 :Basic & Personal Details
    protected void SaveRatings()
    {
        //int Crewid = Common.CastAsInt32(Session["CrewId"]);
        //string Rating = " ";
        //if (rat_A.Checked)
        //{
        //    Rating = "A";
        //}
        //if (rat_B.Checked)
        //{
        //    Rating = "B";
        //}
        //if (rat_C.Checked)
        //{
        //    Rating = "C";
        //}
        //DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM CREWOTHERDETAILS WHERE CREWID=" + Crewid.ToString());
        //if (dt.Rows.Count > 0)
        //{
        //    Common.Execute_Procedures_Select_ByQueryCMS("UPDATE CREWOTHERDETAILS SET RATING='" + Rating + "' WHERE CREWID=" + Crewid.ToString());
        //}
        //else
        //{
        //    Common.Execute_Procedures_Select_ByQueryCMS("INSERT INTO CREWOTHERDETAILS(CREWID,RATING) VALUES(" + Crewid.ToString() + ",'" + Rating + "')");
        //}
    }
    protected void ShowAssessments()
    {
        int Crewid = Common.CastAsInt32(Session["CrewId"]);
        string sql = "SELECT CCB.CREWBONUSID,CCB.CREWID,CPD.CREWNUMBER,CCB.CONTRACTID,CCB.ContractRefNumber,CPD.FIRSTNAME + ' ' +CPD.MIDDLENAME + ' ' +CPD.LASTNAME AS CREWNAME ,CCB.RANKID,RANKCODE,CCB.VESSELID,V.VESSELNAME, BonusApproved, BonusAmount, " +
                       "(SELECT TOP 1 MailSent FROM CrewContractBonusDetails WHERE CrewBonusId = CCB.CREWBONUSID AND MailSent = 'Y') AS IsMailSent, " +
                       "(SELECT Grade FROM CrewContractBonusDetails WHERE CrewBonusId = CCB.CREWBONUSID AND MailUserMode = 1) As OwnerRep, " +
                       "(SELECT Grade FROM CrewContractBonusDetails WHERE CrewBonusId = CCB.CREWBONUSID AND MailUserMode = 2) As Charterer,  " +
                       "(SELECT Grade FROM CrewContractBonusDetails WHERE CrewBonusId = CCB.CREWBONUSID AND MailUserMode = 3) As TechSupdt,  " +
                       "(SELECT Grade FROM CrewContractBonusDetails WHERE CrewBonusId = CCB.CREWBONUSID AND MailUserMode = 4) As FleetMgr,  " +
                       "(SELECT Grade FROM CrewContractBonusDetails WHERE CrewBonusId = CCB.CREWBONUSID AND MailUserMode = 5) As MarineSupdt, " +
                       "(SELECT top 1 SENTON FROM CrewContractBonusDetails WHERE CrewBonusId = CCB.CREWBONUSID AND SENTON IS NOT NULL ORDER BY SENTON) As NotifyDt, " +
                       "CCB.SignOndate,CCB.SignOffDate, V.TechSupdt AS TechSupdtId,V.MarineSupdt AS MarineSupdtId,V.FleetManager AS FleetManagerId " +
                       "FROM CREWCONTRACTBONUSMASTER CCB " +
                       "INNER JOIN CREWPERSONALDETAILS CPD ON CCB.CREWID=CPD.CREWID " +
                       "INNER JOIN RANK R ON CCB.RANKID=R.RANKID " +
                       "INNER JOIN VESSEL V ON V.VESSELID=CCB.VESSELID " +
                       "WHERE STATUS='A' AND CCB.CREWID=" + Crewid.ToString();
        string Where = "";

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql + Where + " Order By V.VESSELNAME , CAST(CCB.ContractRefNumber AS BIGINT) DESC ");
        rprCrewAssessments.DataSource = dt;
        //if (dt.Rows.Count > 0)
        //{
            //lblVesselName.Text = dt.Rows[0]["VESSELNAME"].ToString();
            //string SS = "<a onclick='ShowRemarks(CREWBONUSID, CNT);' href='#' title='View Remarks' style='text-decoration: none;'>VAL</a>";

            //Div1.InnerText=dt.Rows[0]["OwnerRep"].ToString();
            //Div2.InnerText=dt.Rows[0]["Charterer"].ToString();
            //Div3.InnerText=dt.Rows[0]["TechSupdt"].ToString();
            //Div4.InnerText=dt.Rows[0]["FleetMgr"].ToString();
            //Div5.InnerText=dt.Rows[0]["MarineSupdt"].ToString();

            //Div1.InnerHtml = SS.Replace("CREWBONUSID", dt.Rows[0]["CREWBONUSID"].ToString()).Replace("CNT", "1").Replace("VAL", dt.Rows[0]["OwnerRep"].ToString());
            //Div2.InnerHtml = SS.Replace("CREWBONUSID", dt.Rows[0]["CREWBONUSID"].ToString()).Replace("CNT", "2").Replace("VAL", dt.Rows[0]["Charterer"].ToString());
            //Div3.InnerHtml = SS.Replace("CREWBONUSID", dt.Rows[0]["CREWBONUSID"].ToString()).Replace("CNT", "3").Replace("VAL", dt.Rows[0]["TechSupdt"].ToString());
            //Div4.InnerHtml = SS.Replace("CREWBONUSID", dt.Rows[0]["CREWBONUSID"].ToString()).Replace("CNT", "4").Replace("VAL", dt.Rows[0]["FleetMgr"].ToString());
            //Div5.InnerHtml = SS.Replace("CREWBONUSID", dt.Rows[0]["CREWBONUSID"].ToString()).Replace("CNT", "5").Replace("VAL", dt.Rows[0]["MarineSupdt"].ToString());

            //Div1.Attributes.Add("class", "Grade_" + dt.Rows[0]["OwnerRep"].ToString());
            //Div2.Attributes.Add("class", "Grade_" + dt.Rows[0]["Charterer"].ToString());
            //Div3.Attributes.Add("class", "Grade_" + dt.Rows[0]["TechSupdt"].ToString());
            //Div4.Attributes.Add("class", "Grade_" + dt.Rows[0]["FleetMgr"].ToString());
            //Div5.Attributes.Add("class", "Grade_" + dt.Rows[0]["MarineSupdt"].ToString());
       // }
        rprCrewAssessments.DataBind();
    }
    protected void ShowRatings(int Crewid)
    {

        //rat_NA.Checked = true;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("Select top 1 B.Grade As Rating from (Select ROW_NUMBER() OVER(ORDER BY Ratingcount Desc) As Row,Grade from (Select ccbd.Grade, count(ccbd.Grade) As Ratingcount from CrewContractBonusDetails ccbd with(nolock) Inner Join CREWCONTRACTBONUSMASTER ccb with(nolock) on ccbd.CrewBonusId = ccb.CrewBonusId where ccb.CrewId = " + Crewid.ToString() + " And Status = 'A'  Group By ccbd.Grade ) A ) B where B.row = 1 ");
        if (dt.Rows.Count > 0)
        {
            lblRating.Text = dt.Rows[0]["Rating"].ToString();
        }
        //{
        //    rat_A.Checked = dt.Rows[0]["Rating"].ToString() == "A";
        //    rat_B.Checked = dt.Rows[0]["Rating"].ToString() == "B";
        //    rat_C.Checked = dt.Rows[0]["Rating"].ToString() == "C";
        //    rat_NA.Checked = dt.Rows[0]["Rating"].ToString() == " ";
        //}
        //dv_Rads.Visible = false;
        //if ((Mode == "New") || (Mode == "Edit"))
        //{
        //    dv_Rads.Visible = true;
        //}
        //else
        //{
        //    l1.Visible = rat_A.Checked;
        //    l2.Visible = rat_B.Checked;
        //    l3.Visible = rat_C.Checked;
        //    l4.Visible = rat_NA.Checked;
        //}
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        string OldFName = "";
        int Duplicate = 0;
        DataTable dtpass = cls_SearchReliever.SelectPassportNoOfAllCrews();

        //if (ddrecruitingoff.SelectedValue.Trim() == "3")
        //{
        //    lblMessage.Text = "Please Select Non-Yangoon Recruiting Office.";
        //    return;
        //}

        foreach (DataRow dr in dtpass.Rows)
        {
            string passportno = dr["DocumentNumber"].ToString();
            string Crewid = dr["CrewId"].ToString();

            if (passportno.ToString().ToUpper().Trim() == txt_Passport.Text.ToUpper().Trim())
            {
                if (HiddenPK.Value.Trim() == "")
                {
                    lblMessage.Text = "Duplicate Passport No.";
                    Duplicate = 1;
                    break;
                }
                else if (HiddenPK.Value.ToUpper().Trim() != Crewid.ToString().ToUpper().Trim())
                {
                    lblMessage.Text = "Duplicate Passport No.";
                    Duplicate = 1;
                    break;
                }
            }
            else
            {
                lblMessage.Text = "";
            }
        }
        if (Duplicate == 0)
        {
            int cal_age;
            DateTime date_today;
            TimeSpan t1;
            date_today = System.DateTime.Now.Date;
            t1 = date_today - Convert.ToDateTime(txt_DOB.Text);
            cal_age = (Convert.ToInt32(t1.TotalDays) / 365);
            if (cal_age < 18)
            {
                lblMessage.Text = "Crew Must be at least 18 Years Old.";
            }
            else
            {
                CrewMember Crew = new CrewMember();
                ProcessAddCrewMemberPersonalDetails obj = new ProcessAddCrewMemberPersonalDetails();
                try
                {
                    if (HiddenPK.Value.ToString().Trim() == "")
                    {
                        Crew.CreatedBy = Convert.ToInt32(Session["loginid"].ToString());
                    }
                    else
                    {
                        Crew.Id = Convert.ToInt32(HiddenPK.Value);
                        Crew.Modifiedby = Convert.ToInt32(Session["loginid"].ToString());
                    }

                    Crew.FirstName = txt_FirstName.Text;
                    Crew.MiddleName = txt_MiddleName.Text;
                    Crew.LastName = txt_LastName.Text;
                    Crew.DOB = DateTime.Parse(txt_DOB.Text);
                    Crew.Bmi = txt_Bmi.Text;
                    Crew.Nationalty = Convert.ToInt32(ddl_Nationality.SelectedValue);
                    Crew.SexType = (SexType)Convert.ToInt32(ddl_Sex.SelectedValue);
                    //------------------------


                    Crew.Countryofbirth = Convert.ToInt32(ddcountryofbirth.SelectedValue);

                    Crew.Maritalstatusid = (MaritalStatus)Convert.ToInt32(ddmaritalstatus.SelectedValue);

                    Crew.Datefirstjoin = Convert.ToDateTime(txtdatefirstjoin.Text);
                    Crew.PlaceOfBirth = txtplaceofbirth.Text.Trim();
                    Crew.Rankappliedid = Convert.ToInt32(ddrankapp.SelectedValue);
                    Crew.Recruitmentofficeid = Convert.ToInt32(ddrecruitingoff.SelectedValue);
                    Crew.Height = txtheight.Text;
                    Crew.Weight = txtweight.Text;
                    Crew.Waist = txtwaist.Text;
                    Crew.Qualification = Convert.ToInt32(ddl_Academy_Quali.SelectedValue);
                    Crew.ShirtSize = (ShirtSize)Convert.ToInt32(ddl_Shirt.SelectedValue);
                    Crew.ShoeSize = (ShoeSize)Convert.ToInt32(ddl_Shoes.SelectedValue);
                    Crew.Photopath = hfd_fileName.Value;
                    Crew.BloodGroup = Convert.ToInt16(ddl_BloodGroup.SelectedValue);

                    if (FileUpload1.PostedFile != null && FileUpload1.FileContent.Length > 0)
                    {
                        String FileName = "";
                        HttpPostedFile file1 = FileUpload1.PostedFile;
                        UtilityManager um = new UtilityManager();
                        OldFName = hfd_fileName.Value;
                        FileName = um.UploadFileToServer(file1, hfd_fileName.Value, "C");
                        if (FileName.StartsWith("?"))
                        {
                            lblMessage.Text = FileName.Substring(1);
                            return;
                        }
                        Crew.Photopath = FileName;
                        img_Crew.ImageUrl = um.DownloadFileFromServer(Crew.Photopath, "C");
                        hfd_fileName.Value = FileName;
                    }
                    obj.CrewMember = Crew;
                    obj.Invoke();
                    txt_MemberId.Text = Crew.EmpNo;
                    Alerts.InsertCrewPassportNo(Crew.Id, txt_Passport.Text.Trim(), Convert.ToInt32(Session["loginid"].ToString()));
                    HiddenPK.Value = Crew.Id.ToString();
                    btn_AddNew.Visible = Auth.isAdd;
                    Session["CrewId"] = HiddenPK.Value;
                    //if ((Session["CrewId"].ToString() != "" || Session["CrewId"].ToString() != null) && Convert.ToInt32(Session["CrewId"].ToString()) > 0)
                    //{
                    //    CrewContact Member = new CrewContact();
                    //    Member.CreatedBy = Convert.ToInt32(Session["loginid"].ToString());
                    //    Member.CCreatedBy = Convert.ToInt32(Session["loginid"].ToString());
                    //    Member.ModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
                    //    Member.CModifiedBy = Convert.ToInt32(Session["loginid"].ToString());

                    //    Member.CrewId = Convert.ToInt32(Session["CrewId"].ToString());
                    //    Member.Address1 = txt_P_Address.Text;
                    //    Member.Address2 = txt_P_Address1.Text;
                    //    Member.Address3 = txt_P_Address2.Text;
                    //    Member.CountryId = Convert.ToInt32(ddl_P_Country.SelectedValue);
                    //    Member.State = txt_P_State.Text;
                    //    Member.City = txt_P_City.Text;
                    //    Member.PinCode = txt_P_Pin.Text;
                    //    LoadNearestAirport_P(Convert.ToInt32(ddl_P_Country.SelectedValue));
                    //    ddl_P_CountryCode_Tel.Items.Clear();
                    //    ddl_P_CountryCode_Mobile.Items.Clear();
                    //    ddl_P_CountryCode_Fax.Items.Clear();
                    //    ProcessSelectCountryCode obj2 = new ProcessSelectCountryCode();
                    //    obj2.CountryId = Convert.ToInt32(ddl_P_Country.SelectedValue);
                    //    obj2.Invoke();
                    //    ddl_P_CountryCode_Tel.Items.Add(new ListItem(obj2.CountryCode, ddl_P_Country.SelectedValue));
                    //    ddl_P_CountryCode_Mobile.Items.Add(new ListItem(obj2.CountryCode, ddl_P_Country.SelectedValue));
                    //    ddl_P_CountryCode_Fax.Items.Add(new ListItem(obj2.CountryCode, ddl_P_Country.SelectedValue));
                    //    Member.NearestAirportConuntryId = Convert.ToInt32(ddl_P_Airport.SelectedValue);
                    //    Member.LocalAirport = ddl_LocalAirportPermanent.Text;

                    //    Member.TelephoneConuntryId = Convert.ToInt32(ddl_P_CountryCode_Tel.SelectedValue);
                    //    Member.TelephonrAreaCode = txt_P_Area_Code_Tel.Text;
                    //    Member.TelephoneNumber = txt_P_Number_Tel.Text;

                    //    Member.MobileCountryId = Convert.ToInt32(ddl_P_CountryCode_Mobile.SelectedValue);
                    //    Member.MobileNumber = txt_P_Number_Mobile.Text;

                    //    Member.FaxCountryId = Convert.ToInt32(ddl_P_CountryCode_Fax.SelectedValue);
                    //    Member.FaxAreaCode = txt_P_Area_Code_Fax.Text;
                    //    Member.FaxNumber = txt_P_Number_Fax.Text;
                    //    Member.Email1 = txt_P_EMail1.Text;
                    //    Member.Email2 = txt_P_EMail2.Text;
                    //    /**** **/
                    //    //ProcessSelectCountryCode obj3 = new ProcessSelectCountryCode();
                    //    //obj3.CountryId = Convert.ToInt32(ddl_C_Country.SelectedValue);
                    //    //obj3.Invoke();
                    //    Member.CAddress1 = txt_C_Address.Text;
                    //    Member.CAddress2 = txt_C_Address1.Text;
                    //    Member.CAddress3 = txt_C_Address2.Text;
                    //    Member.CCountryId = Convert.ToInt32(ddl_C_Country.SelectedValue);
                    //    Member.CState = txt_C_State.Text;
                    //    Member.CCity = txt_C_City.Text;
                    //    Member.CPinCode = txt_C_Pin.Text;
                    //    LoadNearestAirport_C(Convert.ToInt32(ddl_C_Country.SelectedValue));
                    //    ddl_C_CountryCode_Tel.Items.Clear();
                    //    ddl_C_CountryCode_Mobile.Items.Clear();
                    //    ddl_C_CountryCode_Fax.Items.Clear();
                    //    ProcessSelectCountryCode obj3 = new ProcessSelectCountryCode();
                    //    obj3.CountryId = Convert.ToInt32(ddl_P_Country.SelectedValue);
                    //    obj3.Invoke();
                    //    ddl_C_CountryCode_Tel.Items.Add(new ListItem(obj3.CountryCode, ddl_C_Country.SelectedValue));
                    //    ddl_C_CountryCode_Mobile.Items.Add(new ListItem(obj3.CountryCode, ddl_C_Country.SelectedValue));
                    //    ddl_C_CountryCode_Fax.Items.Add(new ListItem(obj3.CountryCode, ddl_C_Country.SelectedValue));
                    //    Member.CNearestAirportConuntryId = Convert.ToInt32(ddl_C_Airport.SelectedValue);
                    //    Member.CLocalAirport = ddl_LocalAirportCorrespondance.Text;

                    //    Member.CTelephoneConuntryId = Convert.ToInt32(ddl_C_CountryCode_Tel.SelectedValue);
                    //    Member.CTelephonrAreaCode = txt_C_Area_Code_Tel.Text;
                    //    Member.CTelephoneNumber = txt_C_Number_Tel.Text;

                    //    Member.CMobileCountryId = Convert.ToInt32(ddl_C_CountryCode_Mobile.SelectedValue);
                    //    Member.CMobileNumber = txt_C_Number_Mobile.Text;

                    //    Member.CFaxCountryId = Convert.ToInt32(ddl_C_CountryCode_Fax.SelectedValue);
                    //    Member.CFaxAreaCode = txt_C_Area_Code_Fax.Text;
                    //    Member.CFaxNumber = txt_C_Number_Fax.Text;
                    //    Member.CEmail1 = txt_C_EMail1.Text;
                    //    Member.CEmail2 = txt_C_EMail2.Text;
                    //    ProcessAddCrewMemberContactDetails obj1 = new ProcessAddCrewMemberContactDetails();
                    //    obj1.CrewContact = Member;
                    //    obj1.Invoke();
                    //}
                    if (Mode=="New" && CandidateId > 0)
                    {
                        Common.Execute_Procedures_Select_ByQueryCMS("UPDATE CREWPERSONALDETAILS SET CandidateId=" + CandidateId + " WHERE CREWID=" + HiddenPK.Value);

                        //Common.Execute_Procedures_Select_ByQueryCMS("UPDATE ProposalToOwner SET PTO_StatusId='D' WHERE PTO_CandidateId=" + CandidateId + " And PTO_StatusId = 'A'");
                        Common.Execute_Procedures_Select_ByQueryCMS("EXEC InsertCrewContactDetail " + CandidateId + ", " + HiddenPK.Value + "," + Convert.ToInt32(Session["loginid"].ToString()) + " ");

                        Common.Execute_Procedures_Select_ByQueryCMS("EXEC InsertTravelDocument "+ CandidateId + ", "+ HiddenPK.Value + ","+ Convert.ToInt32(Session["loginid"].ToString()) +" ");

                        Common.Execute_Procedures_Select_ByQueryCMS("EXEC InsertOtherCompanyExp " + CandidateId + ", " + HiddenPK.Value + "," + Convert.ToInt32(Session["loginid"].ToString()) + " ");
                    }
                    SaveRatings();
                    lblMessage.Text = "Record Successfully Saved.";
                }
                catch (Exception ex)
                {
                    hfd_fileName.Value = OldFName;
                    lblMessage.Text = "Record Not Saved.";
                }
            }
        }
        btn_AddNew.Visible = false;
    }
    protected void Show_Record(CrewMember Crew)
    {
        string Mess;
        Mess = "";
        // If First Panel Selected
        if (Crew.Id > 0)
        {
            // txt_Passport.Enabled = false; 
            txt_MemberId.Text = Crew.EmpNo;
            txt_FirstName.Text = Crew.FirstName;
            txt_MiddleName.Text = Crew.MiddleName;
            txt_LastName.Text = Crew.LastName;
            lblName.Text = txt_FirstName.Text + " " + txt_MiddleName.Text + " " + txt_LastName.Text;
            if (!(Crew.DOB.Day == 1 && Crew.DOB.Month == 1 && Crew.DOB.Year == 1900))
            {
                txt_DOB.Text = Crew.DOB.ToString("dd-MMM-yyyy");
            }
            txtplaceofbirth.Text = Crew.PlaceOfBirth;
            txt_Age.Text = Crew.Age;
            lblAge.Text = Crew.Age;
            txt_Bmi.Text = Crew.Bmi;
            txt_Passport.Text = Crew.PassportNo;
            txt_INDOS.Text = Crew.INDOSCertificate;
            //***** 
            // ddl_Nationality.SelectedValue = Convert.ToString(Crew.Nationalty);
            Mess = Mess + Alerts.Set_DDL_Value(ddl_Nationality, Convert.ToString(Crew.Nationalty), "Nationality");
            ddl_Sex.SelectedValue = Convert.ToInt32(Crew.SexType).ToString();

            //ddcountryofbirth.SelectedValue = Crew.Countryofbirth.ToString();
            Mess = Mess + Alerts.Set_DDL_Value(ddcountryofbirth, Crew.Countryofbirth.ToString(), "COB");
            ddmaritalstatus.SelectedValue = Convert.ToInt32(Crew.Maritalstatusid).ToString();
            txtdatefirstjoin.Text = Crew.Datefirstjoin.ToString("dd-MMM-yyyy");
            // ddrankapp.SelectedValue = Crew.Rankappliedid.ToString();
            Mess = Mess + Alerts.Set_DDL_Value(ddrankapp, Crew.Rankappliedid.ToString(), "Rank Appled");
            //ddcurrentrank.Text = Crew.Currentrank.ToString();
            lblCurrRank.Text = Crew.Currentrank.ToString();
            //ddrecruitingoff.SelectedValue = Crew.Recruitmentofficeid.ToString();
            Mess = Mess + Alerts.Set_DDL_Value(ddrecruitingoff, Crew.Recruitmentofficeid.ToString(), "Recruiting Office");
            txtheight.Text = Crew.Height;
            txtweight.Text = Crew.Weight;
            txtwaist.Text = Crew.Waist;
            txt_Status.Text = Crew.CrewStatus;
            lblStatus.Text = Crew.Status;
            lblRankExp.Text = Crew.RankExp;
            //ddl_Academy_Quali.SelectedValue = Crew.Qualification.ToString();
            Mess = Mess + Alerts.Set_DDL_Value(ddl_Academy_Quali, Crew.Qualification.ToString(), "Qualification");
            ddl_Shirt.SelectedValue = Convert.ToInt32(Crew.ShirtSize).ToString();
            ddl_Shoes.SelectedValue = Convert.ToInt32(Crew.ShoeSize).ToString();
            lblCurrentVessel.Text = Crew.CurrentVessel;
            ddl_BloodGroup.SelectedValue = Crew.BloodGroup.ToString();
            UtilityManager um = new UtilityManager();
            if (Crew.Photopath.Trim() == "")
            {
                img_Crew.ImageUrl = um.DownloadFileFromServer("noimage.jpg", "C");
                hfd_fileName.Value = "";
            }
            else
            {
                img_Crew.ImageUrl = um.DownloadFileFromServer(Crew.Photopath, "C");
                hfd_fileName.Value = Crew.Photopath;
            }
            ShowRatings(Crew.Id);
            ShowAssessments();
            lblVerifiedBy.Text = "";
            string sql = " SElect VerifiedBy,VerifiedOn from CrewPersonalDetails  Where CrewId=" + Crew.Id;
            DataTable DT = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            if (DT.Rows.Count > 0)
            {
                lblVerifiedBy.Text = DT.Rows[0]["VerifiedBy"].ToString() + " / " + Common.ToDateString(DT.Rows[0]["VerifiedOn"]);
            }
        }
        else
        {
            lblVerifiedBy.Text = "";
            txt_Passport.Enabled = true;
            txt_Passport.Text = "";
            HiddenPK.Value = "";
            txt_MemberId.Text = "";
            txt_FirstName.Text = "";
            txt_MiddleName.Text = "";
            txt_LastName.Text = "";
            txt_DOB.Text = "";
            txtplaceofbirth.Text = "";
            txt_Age.Text = "";
            txt_Bmi.Text = "";
            UtilityManager um = new UtilityManager();
            img_Crew.ImageUrl = um.DownloadFileFromServer("noimage.jpg", "C");
            hfd_fileName.Value = "";

            ddl_Nationality.SelectedIndex = 0;
            ddl_Sex.SelectedIndex = 0;

            ddcountryofbirth.SelectedIndex = 0;
            ddmaritalstatus.SelectedIndex = 0;
            txtdatefirstjoin.Text = "";
            ddrankapp.SelectedIndex = 0;
            lblCurrRank.Text = "";
            ddrecruitingoff.SelectedIndex = 0;
            txtheight.Text = "";
            txtweight.Text = "";
            txtwaist.Text = "";
            txt_Status.Text = "";
            ddl_Shirt.SelectedIndex = 0;
            ddl_Shoes.SelectedIndex = 0;
            ddl_Academy_Quali.SelectedIndex = 0;
            lblCurrentVessel.Text = "";

        }
        if (Mess.Length > 0)
        {
            this.lblMessage.Text = "The Status Of Following Has Been Changed To In-Active <br/>" + Mess.Substring(1);
            this.lblMessage.Visible = true;
        }

    }
    protected void txtweight_TextChanged(object sender, EventArgs e)
    {
        double height, weight, Bmi;
        try
        {
            height = Convert.ToDouble(txtheight.Text);
            weight = Convert.ToDouble(txtweight.Text);
            Bmi = (weight * 10000) / (height * height);
            txt_Bmi.Text = Convert.ToString(Math.Round(Bmi, 2));
        }
        catch (Exception ex)
        {
            txt_Bmi.Text = "0";
        }

    }
    #endregion
    //--------------------
    #region Tab-2 :Contact Details
    public void bindcountryname()
    {
        ProcessGetCountry getcountry = new ProcessGetCountry();
        try
        {
            getcountry.Invoke();
        }
        catch (Exception er)
        {

        }


        ddl_P_Country.DataValueField = "CountryId";
        ddl_P_Country.DataTextField = "CountryName";
        ddl_P_Country.DataSource = getcountry.ResultSet;
        ddl_P_Country.DataBind();

        ddl_C_Country.DataValueField = "CountryId";
        ddl_C_Country.DataTextField = "CountryName";
        ddl_C_Country.DataSource = getcountry.ResultSet;
        ddl_C_Country.DataBind();

        ddlBankCountry.DataValueField = "CountryId";
        ddlBankCountry.DataTextField = "CountryName";
        ddlBankCountry.DataSource = getcountry.ResultSet;
        ddlBankCountry.DataBind();

    }
    protected void Show_Record1(CrewContact Crew)
    {
        string Mess;
        Mess = "";
        // If First Panel Selected
        img_Crew1.ImageUrl = img_Crew.ImageUrl;
        if (Crew.CrewId > 0)
        {
            //-----------------------------
            txt_MemberId1.Text = txt_MemberId.Text;
            txt_LastVessel1.Text = lblCurrentVessel.Text;
            txt_Status1.Text = txt_Status.Text;
            ddcurrentrank1.Text = lblCurrRank.Text;
            txt_FirstName1.Text = txt_FirstName.Text;
            txt_MiddleName1.Text = txt_MiddleName.Text;
            txt_LastName1.Text = txt_LastName.Text;
            txt_passport1.Text = txt_Passport.Text;
            //--------------------------
            HiddenPK.Value = Crew.CrewId.ToString();
            HiddenField2.Value = Crew.CrewContactId.ToString();
            txt_P_Address.Text = Crew.Address1;
            txt_P_Address1.Text = Crew.Address2;
            txt_P_Address2.Text = Crew.Address3;
            //ddl_P_Country.SelectedValue =Crew.CountryId.ToString() ;
            Mess = Mess + Alerts.Set_DDL_Value(ddl_P_Country, Crew.CountryId.ToString(), "Permanent Country");
            txt_P_State.Text = Crew.State;
            txt_P_City.Text = Crew.City;
            txt_P_Pin.Text = Crew.PinCode;
            LoadNearestAirport_P(Convert.ToInt32(ddl_P_Country.SelectedValue));
            //ddl_P_Airport.SelectedValue=Crew.NearestAirportConuntryId.ToString();
            Mess = Mess + Alerts.Set_DDL_Value(ddl_P_Airport, Crew.NearestAirportConuntryId.ToString(), "Permanent Nearest Airport");
            //Mess = Mess + Alerts.Set_DDL_Value(ddl_LocalAirportPermanent, Crew.LocalAirportId.ToString(), "Permanent Local Airport");
            ddl_LocalAirportPermanent.Text = Crew.LocalAirport;

            ddl_P_CountryCode_Tel.Items.Clear();
            ddl_P_CountryCode_Mobile.Items.Clear();
            ddl_P_CountryCode_Fax.Items.Clear();
            ProcessSelectCountryCode obj1 = new ProcessSelectCountryCode();
            obj1.CountryId = Convert.ToInt32(ddl_P_Country.SelectedValue);
            obj1.Invoke();
            ddl_P_CountryCode_Tel.Items.Add(new ListItem(obj1.CountryCode, ddl_P_Country.SelectedValue));
            ddl_P_CountryCode_Mobile.Items.Add(new ListItem(obj1.CountryCode, ddl_P_Country.SelectedValue));
            ddl_P_CountryCode_Fax.Items.Add(new ListItem(obj1.CountryCode, ddl_P_Country.SelectedValue));
            //          ddl_P_CountryCode_Tel.SelectedValue =Crew.TelephoneConuntryId.ToString() ;
            txt_P_Area_Code_Tel.Text = Crew.TelephonrAreaCode;
            txt_P_Number_Tel.Text = Crew.TelephoneNumber;
            //          ddl_P_CountryCode_Mobile.SelectedValue =Crew.MobileCountryId.ToString() ;
            txt_P_Number_Mobile.Text = Crew.MobileNumber;
            //          ddl_P_CountryCode_Fax.SelectedValue = Crew.FaxCountryId.ToString() ; 
            txt_P_Area_Code_Fax.Text = Crew.FaxAreaCode;
            txt_P_Number_Fax.Text = Crew.FaxNumber;
            txt_P_EMail1.Text = Crew.Email1;
            txt_P_EMail2.Text = Crew.Email2;

            //-----------------------------------

            txt_C_Address.Text = Crew.CAddress1;
            txt_C_Address1.Text = Crew.CAddress2;
            txt_C_Address2.Text = Crew.CAddress3;
            // ddl_C_Country.SelectedValue = Crew.CCountryId.ToString() ;
            Mess = Mess + Alerts.Set_DDL_Value(ddl_C_Country, Crew.CCountryId.ToString(), "Correspondence Country");
            txt_C_State.Text = Crew.CState;
            txt_C_City.Text = Crew.CCity;
            txt_C_Pin.Text = Crew.CPinCode;
            LoadNearestAirport_C(Convert.ToInt32(ddl_C_Country.SelectedValue));
            //ddl_C_Airport.SelectedValue =Crew.CNearestAirportConuntryId.ToString() ;
            Mess = Mess + Alerts.Set_DDL_Value(ddl_C_Airport, Crew.CNearestAirportConuntryId.ToString(), "Correspondence Nearest AirPort");
            //Mess = Mess + Alerts.Set_DDL_Value(ddl_LocalAirportCorrespondance, Crew.CLocalAirportId.ToString(), "Correspondence Local Airport");
            ddl_LocalAirportCorrespondance.Text = Crew.CLocalAirport;

            ddl_C_CountryCode_Tel.Items.Clear();
            ddl_C_CountryCode_Mobile.Items.Clear();
            ddl_C_CountryCode_Fax.Items.Clear();
            ProcessSelectCountryCode obj2 = new ProcessSelectCountryCode();
            obj2.CountryId = Convert.ToInt32(ddl_C_Country.SelectedValue);
            obj2.Invoke();
            ddl_C_CountryCode_Tel.Items.Add(new ListItem(obj2.CountryCode, ddl_C_Country.SelectedValue));
            ddl_C_CountryCode_Mobile.Items.Add(new ListItem(obj2.CountryCode, ddl_C_Country.SelectedValue));
            ddl_C_CountryCode_Fax.Items.Add(new ListItem(obj2.CountryCode, ddl_C_Country.SelectedValue));
            //          ddl_C_CountryCode_Tel.SelectedValue = Crew.CTelephoneConuntryId.ToString() ;
            txt_C_Area_Code_Tel.Text = Crew.CTelephonrAreaCode;
            txt_C_Number_Tel.Text = Crew.CTelephoneNumber;
            //          ddl_C_CountryCode_Mobile.SelectedValue = Crew.CMobileCountryId.ToString() ;
            txt_C_Number_Mobile.Text = Crew.CMobileNumber;
            //          ddl_C_CountryCode_Fax.SelectedValue = Crew.CFaxCountryId.ToString() ;
            txt_C_Area_Code_Fax.Text = Crew.CFaxAreaCode;
            txt_C_Number_Fax.Text = Crew.CFaxNumber;
            txt_C_EMail1.Text = Crew.CEmail1;
            txt_C_EMail2.Text = Crew.CEmail2;

        }
        else
        {
            HiddenPK.Value = Crew.CrewId.ToString();

            HiddenField2.Value = Crew.CrewContactId.ToString();
            txt_P_Address.Text = "";
            txt_P_Address1.Text = "";
            txt_P_Address2.Text = Crew.Address3;
            ddl_P_Country.SelectedValue = Crew.CountryId.ToString();
            txt_P_State.Text = Crew.State;
            txt_P_City.Text = Crew.City;
            txt_P_Pin.Text = Crew.PinCode;
            ddl_P_Airport.SelectedIndex = 0;
            ddl_LocalAirportPermanent.Text = "";

            ddl_P_CountryCode_Tel.SelectedIndex = 0;
            txt_P_Area_Code_Tel.Text = "";
            txt_P_Number_Tel.Text = "";

            ddl_P_CountryCode_Mobile.SelectedIndex = 0;
            txt_P_Number_Mobile.Text = "";

            ddl_P_CountryCode_Fax.SelectedIndex = 0;
            txt_P_Area_Code_Fax.Text = "";
            txt_P_Number_Fax.Text = "";
            txt_P_EMail1.Text = "";
            txt_P_EMail2.Text = "";

            //-----------------------------------

            txt_C_Address.Text = "";
            txt_C_Address1.Text = "";
            txt_C_Address2.Text = "";
            ddl_C_Country.SelectedIndex = 0;
            txt_C_State.Text = "";
            txt_C_City.Text = "";
            txt_C_Pin.Text = "";
            ddl_C_Airport.SelectedIndex = 0;
            ddl_LocalAirportCorrespondance.Text = "";


            ddl_C_CountryCode_Tel.SelectedIndex = 0;
            txt_C_Area_Code_Tel.Text = "";
            txt_C_Number_Tel.Text = "";

            ddl_C_CountryCode_Mobile.SelectedIndex = 0;
            txt_C_Number_Mobile.Text = "";

            ddl_C_CountryCode_Fax.SelectedIndex = 0;
            txt_C_Area_Code_Fax.Text = "";
            txt_C_Number_Fax.Text = "";
            txt_C_EMail1.Text = "";
            txt_C_EMail2.Text = "";

        }
        if (Mess.Length > 0)
        {
            this.lblMessage.Text = "The Status Of Following Has Been Changed To In-Active <br/>" + Mess.Substring(1);
            this.lblMessage.Visible = true;
        }

    }
    private void clear_C_Controls()
    {
        txt_C_Address.Text = "";
        txt_C_Address1.Text = "";
        txt_C_Address2.Text = "";
        ddl_C_Country.SelectedIndex = 0;
        txt_C_State.Text = "";
        txt_C_City.Text = "";
        txt_C_Pin.Text = "";
        ddl_C_Airport.SelectedIndex = 0;
        ddl_LocalAirportCorrespondance.Text = "";

        ddl_C_CountryCode_Tel.SelectedIndex = 0;
        txt_C_Area_Code_Tel.Text = "";
        txt_C_Number_Tel.Text = "";

        ddl_C_CountryCode_Mobile.SelectedIndex = 0;
        txt_C_Number_Mobile.Text = "";

        ddl_C_CountryCode_Fax.SelectedIndex = 0;
        txt_C_Area_Code_Fax.Text = "";
        txt_C_Number_Fax.Text = "";
        txt_C_EMail1.Text = "";
        txt_C_EMail2.Text = "";
    }
    private void disable_C_Controls()
    {
        txt_C_Address.Enabled = false;
        txt_C_Address1.Enabled = false;
        txt_C_Address2.Enabled = false;
        ddl_C_Country.Enabled = false;
        txt_C_State.Enabled = false;
        txt_C_City.Enabled = false;
        txt_C_Pin.Enabled = false;
        ddl_C_Airport.Enabled = false;
        ddl_LocalAirportCorrespondance.Enabled = false;

        ddl_C_CountryCode_Tel.Enabled = false;
        txt_C_Area_Code_Tel.Enabled = false;
        txt_C_Number_Tel.Enabled = false;

        ddl_C_CountryCode_Mobile.Enabled = false;
        txt_C_Number_Mobile.Enabled = false;

        ddl_C_CountryCode_Fax.Enabled = false;
        txt_C_Area_Code_Fax.Enabled = false;
        txt_C_Number_Fax.Enabled = false;
        txt_C_EMail1.Enabled = false;
        txt_C_EMail2.Enabled = false;
    }
    private void enable_C_Controls()
    {
        txt_C_Address.Enabled = true;
        txt_C_Address1.Enabled = true;
        txt_C_Address2.Enabled = true;
        ddl_C_Country.Enabled = true;
        txt_C_State.Enabled = true;
        txt_C_City.Enabled = true;
        txt_C_Pin.Enabled = true;
        ddl_C_Airport.Enabled = true;
        ddl_LocalAirportCorrespondance.Enabled = true;

        ddl_C_CountryCode_Tel.Enabled = true;
        txt_C_Area_Code_Tel.Enabled = true;
        txt_C_Number_Tel.Enabled = true;

        ddl_C_CountryCode_Mobile.Enabled = true;
        txt_C_Number_Mobile.Enabled = true;

        ddl_C_CountryCode_Fax.Enabled = true;
        txt_C_Area_Code_Fax.Enabled = true;
        txt_C_Number_Fax.Enabled = true;
        txt_C_EMail1.Enabled = true;
        txt_C_EMail2.Enabled = true;
    }
    private void copy_Data()
    {
        txt_C_Address.Text = txt_P_Address.Text;
        txt_C_Address1.Text = txt_P_Address1.Text;
        txt_C_Address2.Text = txt_P_Address2.Text;
        ddl_C_Country.SelectedValue = ddl_P_Country.SelectedValue;
        changeddl_c_country();
        txt_C_State.Text = txt_P_State.Text;
        txt_C_City.Text = txt_P_City.Text;
        txt_C_Pin.Text = txt_P_Pin.Text;
        ddl_C_Airport.SelectedValue = ddl_P_Airport.SelectedValue;
        ddl_LocalAirportCorrespondance.Text = ddl_LocalAirportPermanent.Text;

        ddl_C_CountryCode_Tel.SelectedValue = ddl_P_CountryCode_Tel.SelectedValue;
        txt_C_Area_Code_Tel.Text = txt_P_Area_Code_Tel.Text;
        txt_C_Number_Tel.Text = txt_P_Number_Tel.Text;

        ddl_C_CountryCode_Mobile.SelectedValue = ddl_P_CountryCode_Mobile.SelectedValue;
        txt_C_Number_Mobile.Text = txt_P_Number_Mobile.Text;

        ddl_C_CountryCode_Fax.SelectedValue = ddl_P_CountryCode_Fax.SelectedValue;
        txt_C_Area_Code_Fax.Text = txt_P_Area_Code_Fax.Text;
        txt_C_Number_Fax.Text = txt_P_Number_Fax.Text;
        txt_C_EMail1.Text = txt_P_EMail1.Text;
        txt_C_EMail2.Text = txt_P_EMail2.Text;
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked)
        {
            copy_Data();
            //------------------------------
            disable_C_Controls();

            RequiredFieldValidator17.Enabled = false;
            RangeValidator2.Enabled = false;
            RequiredFieldValidator18.Enabled = false;
            RequiredFieldValidator20.Enabled = false;
            RequiredFieldValidator21.Enabled = false;
        }
        else
        {
            enable_C_Controls();
            RequiredFieldValidator17.Enabled = true;
            RequiredFieldValidator18.Enabled = true;
            RangeValidator2.Enabled = true;
            RequiredFieldValidator20.Enabled = true;
            RequiredFieldValidator21.Enabled = true;
        }


    }
    protected void ddl_P_Country_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadNearestAirport_P(Convert.ToInt32(ddl_P_Country.SelectedValue));
        ddl_P_CountryCode_Tel.Items.Clear();
        ddl_P_CountryCode_Mobile.Items.Clear();
        ddl_P_CountryCode_Fax.Items.Clear();
        ProcessSelectCountryCode obj = new ProcessSelectCountryCode();
        obj.CountryId = Convert.ToInt32(ddl_P_Country.SelectedValue);
        obj.Invoke();
        ddl_P_CountryCode_Tel.Items.Add(new ListItem(obj.CountryCode, ddl_P_Country.SelectedValue));
        ddl_P_CountryCode_Mobile.Items.Add(new ListItem(obj.CountryCode, ddl_P_Country.SelectedValue));
        ddl_P_CountryCode_Fax.Items.Add(new ListItem(obj.CountryCode, ddl_P_Country.SelectedValue));
    }
    protected void ddl_C_Country_SelectedIndexChanged(object sender, EventArgs e)
    {
        changeddl_c_country();
        //LoadNearestAirport_C(Convert.ToInt32(ddl_C_Country.SelectedValue));
        //ddl_C_CountryCode_Tel.Items.Clear();
        //ddl_C_CountryCode_Mobile.Items.Clear();
        //ddl_C_CountryCode_Fax.Items.Clear();
        //ProcessSelectCountryCode obj = new ProcessSelectCountryCode();
        //obj.CountryId = Convert.ToInt32(ddl_C_Country.SelectedValue);
        //obj.Invoke();
        //ddl_C_CountryCode_Tel.Items.Add(new ListItem(obj.CountryCode,ddl_C_Country.SelectedValue));     
        //ddl_C_CountryCode_Mobile.Items.Add(new ListItem(obj.CountryCode,ddl_C_Country.SelectedValue));     
        //ddl_C_CountryCode_Fax.Items.Add(new ListItem(obj.CountryCode,ddl_C_Country.SelectedValue));     
    }
    private void changeddl_c_country()
    {
        LoadNearestAirport_C(Convert.ToInt32(ddl_C_Country.SelectedValue));
        ddl_C_CountryCode_Tel.Items.Clear();
        ddl_C_CountryCode_Mobile.Items.Clear();
        ddl_C_CountryCode_Fax.Items.Clear();
        ProcessSelectCountryCode obj = new ProcessSelectCountryCode();
        obj.CountryId = Convert.ToInt32(ddl_C_Country.SelectedValue);
        obj.Invoke();
        ddl_C_CountryCode_Tel.Items.Add(new ListItem(obj.CountryCode, ddl_C_Country.SelectedValue));
        ddl_C_CountryCode_Mobile.Items.Add(new ListItem(obj.CountryCode, ddl_C_Country.SelectedValue));
        ddl_C_CountryCode_Fax.Items.Add(new ListItem(obj.CountryCode, ddl_C_Country.SelectedValue));
    }
    private void LoadNearestAirport_C(int CountryId)
    {
        ProcessSelectNearestAirportById obj = new ProcessSelectNearestAirportById();
        obj.CountryId = CountryId;
        obj.Invoke();

        ddl_C_Airport.DataSource = obj.ResultSet.Tables[0];
        ddl_C_Airport.DataValueField = "NearestAirportId";
        ddl_C_Airport.DataTextField = "NearestAirportName";
        ddl_C_Airport.DataBind();

        //ddl_LocalAirportCorrespondance.DataSource = obj.ResultSet.Tables[0];
        //ddl_LocalAirportCorrespondance.DataValueField = "NearestAirportId";
        //ddl_LocalAirportCorrespondance.DataTextField = "NearestAirportName";
        //ddl_LocalAirportCorrespondance.DataBind();
    }
    private void LoadNearestAirport_P(int CountryId)
    {
        ProcessSelectNearestAirportById obj = new ProcessSelectNearestAirportById();
        obj.CountryId = CountryId;
        obj.Invoke();

        ddl_P_Airport.DataSource = obj.ResultSet.Tables[0];
        ddl_P_Airport.DataValueField = "NearestAirportId";
        ddl_P_Airport.DataTextField = "NearestAirportName";
        ddl_P_Airport.DataBind();

        //ddl_LocalAirportPermanent.DataSource = obj.ResultSet.Tables[0];
        //ddl_LocalAirportPermanent.DataValueField = "NearestAirportId";
        //ddl_LocalAirportPermanent.DataTextField = "NearestAirportName";
        //ddl_LocalAirportPermanent.DataBind();
    }
    protected void btn_Save_Click1(object sender, EventArgs e)
    {
        CrewContact Member = new CrewContact();
        try
        {
            if (HiddenField2.Value.ToString().Trim() != "")
            {
                Member.CrewContactId = Convert.ToInt32(HiddenField2.Value);
            }
            Member.CreatedBy = Convert.ToInt32(Session["loginid"].ToString());
            Member.CCreatedBy = Convert.ToInt32(Session["loginid"].ToString());
            Member.ModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
            Member.CModifiedBy = Convert.ToInt32(Session["loginid"].ToString());

            Member.CrewId = Convert.ToInt32(HiddenPK.Value.Trim());
            Member.Address1 = txt_P_Address.Text;
            Member.Address2 = txt_P_Address1.Text;
            Member.Address3 = txt_P_Address2.Text;
            Member.CountryId = Convert.ToInt32(ddl_P_Country.SelectedValue);
            Member.State = txt_P_State.Text;
            Member.City = txt_P_City.Text;
            Member.PinCode = txt_P_Pin.Text;
            Member.NearestAirportConuntryId = Convert.ToInt32(ddl_P_Airport.SelectedValue);
            Member.LocalAirport = ddl_LocalAirportPermanent.Text;

            Member.TelephoneConuntryId = Convert.ToInt32(ddl_P_CountryCode_Tel.SelectedValue);
            Member.TelephonrAreaCode = txt_P_Area_Code_Tel.Text;
            Member.TelephoneNumber = txt_P_Number_Tel.Text;

            Member.MobileCountryId = Convert.ToInt32(ddl_P_CountryCode_Mobile.SelectedValue);
            Member.MobileNumber = txt_P_Number_Mobile.Text;

            Member.FaxCountryId = Convert.ToInt32(ddl_P_CountryCode_Fax.SelectedValue);
            Member.FaxAreaCode = txt_P_Area_Code_Fax.Text;
            Member.FaxNumber = txt_P_Number_Fax.Text;
            Member.Email1 = txt_P_EMail1.Text;
            Member.Email2 = txt_P_EMail2.Text;

            //-----------------------------------
            if (CheckBox1.Checked)
            {
                Member.CAddress1 = txt_P_Address.Text;
                Member.CAddress2 = txt_P_Address1.Text;
                Member.CAddress3 = txt_P_Address2.Text;
                Member.CCountryId = Convert.ToInt32(ddl_P_Country.SelectedValue);
                Member.CState = txt_P_State.Text;
                Member.CCity = txt_P_City.Text;
                Member.CPinCode = txt_P_Pin.Text;
                Member.CNearestAirportConuntryId = Convert.ToInt32(ddl_P_Airport.SelectedValue);
                Member.CLocalAirport = ddl_LocalAirportPermanent.Text;

                Member.CTelephoneConuntryId = Convert.ToInt32(ddl_P_CountryCode_Tel.SelectedValue);
                Member.CTelephonrAreaCode = txt_P_Area_Code_Tel.Text;
                Member.CTelephoneNumber = txt_P_Number_Tel.Text;

                Member.CMobileCountryId = Convert.ToInt32(ddl_P_CountryCode_Mobile.SelectedValue);
                Member.CMobileNumber = txt_P_Number_Mobile.Text;

                Member.CFaxCountryId = Convert.ToInt32(ddl_P_CountryCode_Fax.SelectedValue);
                Member.CFaxAreaCode = txt_P_Area_Code_Fax.Text;
                Member.CFaxNumber = txt_P_Number_Fax.Text;
                Member.CEmail1 = txt_P_EMail1.Text;
                Member.CEmail2 = txt_P_EMail2.Text;

            }
            else
            {
                Member.CAddress1 = txt_C_Address.Text;
                Member.CAddress2 = txt_C_Address1.Text;
                Member.CAddress3 = txt_C_Address2.Text;
                Member.CCountryId = Convert.ToInt32(ddl_C_Country.SelectedValue);
                Member.CState = txt_C_State.Text;
                Member.CCity = txt_C_City.Text;
                Member.CPinCode = txt_C_Pin.Text;
                Member.CNearestAirportConuntryId = Convert.ToInt32(ddl_C_Airport.SelectedValue);
                Member.CLocalAirport = ddl_LocalAirportCorrespondance.Text;

                Member.CTelephoneConuntryId = Convert.ToInt32(ddl_C_CountryCode_Tel.SelectedValue);
                Member.CTelephonrAreaCode = txt_C_Area_Code_Tel.Text;
                Member.CTelephoneNumber = txt_C_Number_Tel.Text;

                Member.CMobileCountryId = Convert.ToInt32(ddl_C_CountryCode_Mobile.SelectedValue);
                Member.CMobileNumber = txt_C_Number_Mobile.Text;

                Member.CFaxCountryId = Convert.ToInt32(ddl_C_CountryCode_Fax.SelectedValue);
                Member.CFaxAreaCode = txt_C_Area_Code_Fax.Text;
                Member.CFaxNumber = txt_C_Number_Fax.Text;
                Member.CEmail1 = txt_C_EMail1.Text;
                Member.CEmail2 = txt_C_EMail2.Text;
            }
            ProcessAddCrewMemberContactDetails obj = new ProcessAddCrewMemberContactDetails();
            obj.CrewContact = Member;
            obj.Invoke();
            lblMessage.Text = "Record Successfully Saved.";
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Record Not Saved.";
        }
    }
    #endregion
    //--------------------
    #region Tab -3  Family, Visa & CDC Details
    private void BindGridfamily(int id)
    {
        //-----------------------------
        txt_MemberId_family.Text = txt_MemberId.Text;
        txt_CurrentRankFamily.Text = lblCurrRank.Text;
        txt_LastVesselFamily.Text = lblCurrentVessel.Text;
        txt_Status_Family.Text = txt_Status.Text;

        txt_FirstName_Family.Text = txt_FirstName.Text;
        txt_MiddleName_Family.Text = txt_MiddleName.Text;
        txt_LastNameFamily.Text = txt_LastName.Text;
        img_Crew2.ImageUrl = img_Crew.ImageUrl;
        txt_PassportFamily.Text = txt_Passport.Text;

        ProcessSelectFamilyDetails obj = new ProcessSelectFamilyDetails();
        FamilyDetails familydetail = new FamilyDetails();
        familydetail.CrewId = id;
        obj.FamilyDetails = familydetail;
        obj.Invoke();
        //Gvfamily.DataSource = obj.ResultSet;
        //Gvfamily.DataBind();

        rptfamily.DataSource = obj.ResultSet;
        rptfamily.DataBind();

        if (rptfamily.Items.Count <= 0)
        {
            lbl_Gvfamily.Text = "No Records Found..!";
        }
    }
    protected void Show_Record_family(FamilyDetails family)
    {
        string Mess;
        Mess = "";
        HiddenFamilyPK.Value = family.Crewfamilyid.ToString();
        txtfirstname.Text = family.Firstname;
        txtmiddlename.Text = family.Middlename;
        txtlastname.Text = family.Lastname;
        ddrelation.SelectedValue = (family.Relationshipid == 0) ? " " : family.Relationshipid.ToString();
        if (family.IsNok == 'Y')
        {
            chbox_isnok.Checked = true;
        }
        else
        {
            chbox_isnok.Checked = false;
        }
        ddsex.SelectedValue = family.SextypeId.ToString();
        txtpob.Text = family.Placeofbirth;
        ddnationality.SelectedValue = (family.Nationalityid == 0) ? "" : family.Nationalityid.ToString();

        txtaddress1.Text = family.Address1.ToString();
        txtaddress2.Text = family.Address2.ToString();
        txtaddress3.Text = family.Address3.ToString();
        // ddcountryname.SelectedValue = family.Countryid.ToString();
        Mess = Mess + Alerts.Set_DDL_Value(ddcountryname, family.Countryid.ToString(), "Country");

        txtstate.Text = family.State.ToString();
        txtcity.Text = family.City.ToString();
        txtpin.Text = family.Pin.ToString();
        LoadNearestAirport(Convert.ToInt32(ddcountryname.SelectedValue));
        //dd_nearest_airport.SelectedValue = family.NearestAirportid.ToString();
        Mess = Mess + Alerts.Set_DDL_Value(dd_nearest_airport, family.NearestAirportid.ToString(), "Nearest Airport");

        dd_tel_countrycode.Items.Clear();
        dd_mobile_countrycode.Items.Clear();
        dd_fax_country_code.Items.Clear();
        ProcessSelectCountryCode obj2 = new ProcessSelectCountryCode();
        obj2.CountryId = Convert.ToInt32(ddcountryname.SelectedValue);
        obj2.Invoke();
        dd_tel_countrycode.Items.Add(new ListItem(obj2.CountryCode, ddcountryname.SelectedValue));
        dd_mobile_countrycode.Items.Add(new ListItem(obj2.CountryCode, ddcountryname.SelectedValue));
        dd_fax_country_code.Items.Add(new ListItem(obj2.CountryCode, ddcountryname.SelectedValue));

        txt_tel_Area_Code.Text = family.TelAreaCode.ToString();
        txt_tel_Number.Text = family.Telno.ToString();
        txt_mobile_Number.Text = family.Mobileno.ToString();
        txt_fax_areacode.Text = family.FaxAreaCode.ToString();
        txt_fax_number.Text = family.Faxno.ToString();
        txt_EMail1.Text = family.Email1.ToString();
        txt_email2.Text = family.Email2.ToString();
        txtpassportno.Text = family.Passportno;

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT REPLACE(convert(varchar(25),IssueDate, 106),' ', '-') AS IssueDate, REPLACE(convert(varchar(25),ExpiryDate, 106),' ', '-') AS ExpiryDate,REPLACE(convert(varchar(25),DateOfBirth, 106),' ', '-') AS DateOfBirth from CrewFamilyDetails where CrewFamilyId=" + HiddenFamilyPK.Value);

        try
        {
            ////txtissuedate.Text = Convert.ToDateTime(family.Issuedate).ToString("dd-MMM-yyyy");
            //string strFormat = "MM-dd-yyyy";
            //DateTime dtIssuedate;
            //DateTime.TryParseExact(family.Issuedate.ToString(), strFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtIssuedate);
            //string strIssuedate = dtIssuedate.ToString("dd-MMM-yyyy");

            //txtissuedate.Text = strIssuedate;

            txtissuedate.Text = dt.Rows[0]["IssueDate"].ToString();

        }
        catch { txtissuedate.Text = ""; }
        try
        {
            ////txtexpirydate.Text = Convert.ToDateTime(family.Expirydate).ToString("dd-MMM-yyyy");
            //string strFormat = "MM-dd-yyyy";
            //DateTime dtExpirydate;
            //DateTime.TryParseExact(family.Expirydate.ToString(), strFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtExpirydate);
            //string strExpirydate = dtExpirydate.ToString("dd-MMM-yyyy");

            //txtexpirydate.Text = strExpirydate;

            txtexpirydate.Text = dt.Rows[0]["ExpiryDate"].ToString();
        }
        catch { txtexpirydate.Text = ""; }

        txtplaceofissue.Text = family.Placeofissue;
        txtbankname.Text = family.Bankname;
        txtbranch.Text = family.Branchname;
        txt_Beneficiary.Text = family.Beneficiary;
        txtaccountno.Text = family.Bankaccountno;
        txtpersonalcode.Text = family.Personalcode;
        txtswiftcode.Text = family.Swiftcode;
        txtibanno.Text = family.Ibanno;
        txtbankaddress.Text = family.Bankaddress;
        ddtypeofremittance.SelectedValue = family.Typeofremittanceid.ToString();
        if (family.IsInsurance == 'Y')
        {
            chkInsuCovered.Checked = true;
            txtInsuranceId.Enabled = true;
            txtInsuCompany.Enabled = true;
            txtInsuranceId.Text = family.InsuranceId.ToString();
            txtInsuCompany.Text = family.InsuranceCompany.ToString();
        }
        else
        {
            chkInsuCovered.Checked = false;
            txtInsuranceId.Enabled = false;
            txtInsuCompany.Enabled = false;
            txtInsuranceId.Text = "";
            txtInsuCompany.Text = "";
        }
        
        try
        {
            ////txt_DOB_Family.Text = DateTime.Parse(family.DOB.ToString()).ToString("dd-MMM-yyyy");                   

            ////string strFormat = "MM-dd-yyyy";
            ////DateTime dtDOB;
            ////DateTime.TryParseExact(family.DOB.ToString(), strFormat, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None, out dtDOB);
            ////string strDOB = dtDOB.ToString("dd-MMM-yyyy");

            ////txt_DOB_Family.Text = strDOB;

            ////ScriptManager.RegisterStartupScript(this, this.GetType(), "adasadsa", "alert('" + family.DOB + "')", true);

            //string[] DOB = family.DOB.Replace("/", "-").Split('-');
            //DateTime dtDOB = new DateTime(Common.CastAsInt32(DOB[2]), Common.CastAsInt32(DOB[0]), Common.CastAsInt32(DOB[1]));
            //txt_DOB_Family.Text = dtDOB.ToString("dd-MMM-yyyy");

            txt_DOB_Family.Text = dt.Rows[0]["DateOfBirth"].ToString();

        }
        catch { txt_DOB_Family.Text = ""; }

        UtilityManager um = new UtilityManager();
        if (family.PhotoPath.Trim() == "")
        {
            img_FamilyMember.ImageUrl = um.DownloadFileFromServer("noimage.jpg", "C");
            hfd_FamilyImage.Value = "";
        }
        else
        {
            img_FamilyMember.ImageUrl = um.DownloadFileFromServer(family.PhotoPath, "C");
        }
        if (Mess.Length > 0)
        {
            this.lbl_Gvfamily.Text = "The Status Of Following Has Been Changed To In-Active <br/>" + Mess.Substring(1);
            this.lbl_Gvfamily.Visible = true;
        }
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        CrewFamilyId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        FamilyDetails familyDetails = new FamilyDetails();
        ProcessSelectFamilyDetailsByFamilyId processSelectFamilyDetailsByFamilyId = new ProcessSelectFamilyDetailsByFamilyId();
        familyDetails.Crewfamilyid = CrewFamilyId;
        processSelectFamilyDetailsByFamilyId.FamilyDetails = familyDetails;
        processSelectFamilyDetailsByFamilyId.Invoke();
        Show_Record_family(processSelectFamilyDetailsByFamilyId.FamilyDetails);
        BindGridVisa();
        BindGridfamily(Convert.ToInt32(HiddenPK.Value.Trim()));
        CrewFamilyId = 0;
        btn_Family_Reset.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        familypanel.Visible = true;
        btnfamilyCancel.Visible = true;
        btn_family_save.Visible = false;
        //--
        panelvisa1.Visible = true;
        btnvisaadd.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;


    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        CrewFamilyId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        FamilyDetails familyDetails = new FamilyDetails();
        ProcessSelectFamilyDetailsByFamilyId processSelectFamilyDetailsByFamilyId = new ProcessSelectFamilyDetailsByFamilyId();
        familyDetails.Crewfamilyid = CrewFamilyId;
        processSelectFamilyDetailsByFamilyId.FamilyDetails = familyDetails;
        processSelectFamilyDetailsByFamilyId.Invoke();
        Show_Record_family(processSelectFamilyDetailsByFamilyId.FamilyDetails);
        //Gvfamily.SelectedIndex = e.NewEditIndex;
        BindGridVisa();
        BindGridfamily(Convert.ToInt32(HiddenPK.Value.Trim()));
        CrewFamilyId = 0;

        familypanel.Visible = true;
        btnfamilyCancel.Visible = true;
        btn_family_save.Visible = true;
        //---
        panelvisa1.Visible = true;
        btnvisaadd.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        CrewFamilyId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        FamilyDetails exp = new FamilyDetails();
        ProcessDeleteFamilyDetails obj = new ProcessDeleteFamilyDetails();
        exp.Crewfamilyid = CrewFamilyId;
        obj.FamilyDetails = exp;
        obj.Invoke();
        BindGridfamily(Convert.ToInt32(HiddenPK.Value.Trim()));
        CrewFamilyId = 0;
        try
        {
            if (Convert.ToInt32(HiddenFamilyPK.Value.Trim()) == exp.Crewfamilyid)
            {
                btn_Family_Reset_Click(sender, e);
            }
        }
        catch
        {
        }
    }
    protected void imgattach_ShowPreview(object sender, EventArgs e)
    {
        string ImgPath = ((ImageButton)sender).CommandArgument.ToString();
        string appname = ConfigurationManager.AppSettings["AppName"].ToString();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "preview", "Show_Image_Large1('/"+ appname + "/EMANAGERBLOB/HRD/CrewPhotos/" + ImgPath + "');", true);

    }

    //protected void Gvfamily_SelectIndexChanged(object sender, EventArgs e)
    //{
    //    HiddenField hfd3;
    //    FamilyDetails familyDetails = new FamilyDetails();
    //    ProcessSelectFamilyDetailsByFamilyId processSelectFamilyDetailsByFamilyId = new ProcessSelectFamilyDetailsByFamilyId();
    //    hfd3 = (HiddenField)Gvfamily.Rows[Gvfamily.SelectedIndex].FindControl("HiddenId3");
    //    familyDetails.Crewfamilyid = Convert.ToInt32(hfd3.Value.ToString());
    //    processSelectFamilyDetailsByFamilyId.FamilyDetails = familyDetails;
    //    processSelectFamilyDetailsByFamilyId.Invoke();
    //    Show_Record_family(processSelectFamilyDetailsByFamilyId.FamilyDetails);
    //    BindGridVisa();
    //    btn_Family_Reset.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
    //    familypanel.Visible = true;
    //    btnfamilyCancel.Visible = true;
    //    btn_family_save.Visible = false;
    //    //--
    //    panelvisa1.Visible = true;
    //    btnvisaadd.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;

    //}
    //protected void family_Row_Editing(object sender, GridViewEditEventArgs e)
    //{
    //    HiddenField hfd3;
    //    FamilyDetails familyDetails = new FamilyDetails();
    //    ProcessSelectFamilyDetailsByFamilyId processSelectFamilyDetailsByFamilyId = new ProcessSelectFamilyDetailsByFamilyId();
    //    hfd3 = (HiddenField)Gvfamily.Rows[e.NewEditIndex].FindControl("HiddenId3");
    //    familyDetails.Crewfamilyid = Convert.ToInt32(hfd3.Value.ToString());
    //    processSelectFamilyDetailsByFamilyId.FamilyDetails = familyDetails;
    //    processSelectFamilyDetailsByFamilyId.Invoke();
    //    Show_Record_family(processSelectFamilyDetailsByFamilyId.FamilyDetails);
    //    Gvfamily.SelectedIndex = e.NewEditIndex;
    //    BindGridVisa();

    //    familypanel.Visible = true;
    //    btnfamilyCancel.Visible = true;
    //    btn_family_save.Visible = true;
    //    //---
    //    panelvisa1.Visible = true;
    //    btnvisaadd.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;

    //}
    //protected void family_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    HiddenField hfddel;
    //    FamilyDetails exp = new FamilyDetails();
    //    hfddel = (HiddenField)Gvfamily.Rows[e.RowIndex].FindControl("HiddenId3");
    //    ProcessDeleteFamilyDetails obj = new ProcessDeleteFamilyDetails();
    //    exp.Crewfamilyid = Convert.ToInt32(hfddel.Value.ToString());
    //    obj.FamilyDetails = exp;
    //    obj.Invoke();
    //    BindGridfamily(Convert.ToInt32(HiddenPK.Value.Trim()));
    //    try
    //    {
    //        if (Convert.ToInt32(HiddenFamilyPK.Value.Trim()) == exp.Crewfamilyid)
    //        {
    //            btn_Family_Reset_Click(sender, e);
    //        }
    //    }
    //    catch
    //    {
    //    }
    //}
    protected void btn_Family_save_Click(object sender, EventArgs e)
    {
        string OldFName = "";
        FamilyDetails familyDetails = new FamilyDetails();

        if (txt_DOB_Family.Text.Trim() != "")
        {
            if (DateTime.Parse(txt_DOB_Family.Text) > DateTime.Today)
            {
                lblMessage.Text = "DOB should be less or equal today.";
                return;
            }
        }
        if (HiddenFamilyPK.Value.Trim() == "")
        {
            familyDetails.Createdby = Convert.ToInt32(Session["loginid"].ToString());
        }
        else
        {
            familyDetails.Crewfamilyid = Convert.ToInt32(HiddenFamilyPK.Value);
            familyDetails.Modifiedby = Convert.ToInt32(Session["loginid"].ToString());
        }
        familyDetails.CrewId = Convert.ToInt32(HiddenPK.Value.Trim());
        familyDetails.Firstname = txtfirstname.Text;
        familyDetails.Middlename = txtmiddlename.Text;
        familyDetails.Lastname = txtlastname.Text;
        try { familyDetails.Relationshipid = Convert.ToInt32(ddrelation.SelectedValue); }
        catch { familyDetails.Relationshipid = 0; }

        familyDetails.IsNok = (chbox_isnok.Checked) ? 'Y' : 'N';
        familyDetails.SextypeId = Convert.ToInt32(ddsex.SelectedValue);
        familyDetails.Placeofbirth = txtpob.Text;
        try { familyDetails.Nationalityid = Convert.ToInt32(ddnationality.SelectedValue); }
        catch { familyDetails.Nationalityid = 0; }

        familyDetails.Address1 = txtaddress1.Text;
        familyDetails.Address2 = txtaddress2.Text;
        familyDetails.Address3 = txtaddress3.Text;
        familyDetails.Countryid = Convert.ToInt32(ddcountryname.SelectedValue);
        familyDetails.State = txtstate.Text;
        familyDetails.City = txtcity.Text;
        familyDetails.Pin = txtpin.Text;

        try { familyDetails.NearestAirportid = Convert.ToInt32(dd_nearest_airport.SelectedValue); }
        catch { familyDetails.NearestAirportid = 0; }
        try { familyDetails.TelCountryid = Convert.ToInt32(dd_tel_countrycode.SelectedValue); }
        catch { familyDetails.TelCountryid = 0; }
        try { familyDetails.MobileCountryid = Convert.ToInt32(dd_mobile_countrycode.SelectedValue); }
        catch { familyDetails.MobileCountryid = 0; }
        try { familyDetails.FaxCountryid = Convert.ToInt32(dd_fax_country_code.SelectedValue); }
        catch { familyDetails.FaxCountryid = 0; }

        familyDetails.TelAreaCode = txt_tel_Area_Code.Text;
        familyDetails.Telno = txt_tel_Number.Text;
        familyDetails.Mobileno = txt_mobile_Number.Text;
        familyDetails.FaxAreaCode = txt_fax_areacode.Text;
        familyDetails.Faxno = txt_fax_number.Text;
        familyDetails.Email1 = txt_EMail1.Text;
        familyDetails.Email2 = txt_email2.Text;
        familyDetails.Passportno = txtpassportno.Text;
        familyDetails.Issuedate = txtissuedate.Text.Trim();
        familyDetails.Expirydate = txtexpirydate.Text.Trim();
        familyDetails.Placeofissue = txtplaceofissue.Text;
        familyDetails.Bankname = txtbankname.Text;
        familyDetails.Branchname = txtbranch.Text;
        familyDetails.Beneficiary = txt_Beneficiary.Text;
        familyDetails.Bankaccountno = txtaccountno.Text;
        familyDetails.Personalcode = txtpersonalcode.Text;
        familyDetails.Swiftcode = txtswiftcode.Text;
        familyDetails.Ibanno = txtibanno.Text;
        familyDetails.Bankaddress = txtbankaddress.Text;
        familyDetails.Typeofremittanceid = Convert.ToInt32(ddtypeofremittance.SelectedValue);
        familyDetails.DOB = txt_DOB_Family.Text.Trim();
        familyDetails.ECNR = (chk_ECNR.Checked) ? "Y" : "N";
        familyDetails.Status = "A";
        familyDetails.IsInsurance = (chkInsuCovered.Checked) ? 'Y' : 'N';
        if (chkInsuCovered.Checked)
        {
            familyDetails.InsuranceId = txtInsuranceId.Text.Trim();
            familyDetails.InsuranceCompany = txtInsuCompany.Text.Trim();
        }
        else
        {
            familyDetails.InsuranceId = "";
            familyDetails.InsuranceCompany = "";
        }
        if (FileUpload2.PostedFile != null && FileUpload2.FileContent.Length > 0)
        {
            String FileName = "";
            HttpPostedFile file1 = FileUpload2.PostedFile;
            UtilityManager um = new UtilityManager();
            OldFName = hfd_FamilyImage.Value;
            FileName = um.UploadFileToServer(file1, hfd_FamilyImage.Value, "C");
            if (FileName.StartsWith("?"))
            {
                lblMessage.Text = FileName.Substring(1);
                return;
            }
            familyDetails.PhotoPath = FileName;
            img_FamilyMember.ImageUrl = um.DownloadFileFromServer(familyDetails.PhotoPath, "C");
            hfd_FamilyImage.Value = FileName;
        }

        ProcessAddCrewMemberFamilyDetails obj = new ProcessAddCrewMemberFamilyDetails();
        obj.FamilyDetails = familyDetails;
        obj.Invoke();
        HiddenFamilyPK.Value = familyDetails.Crewfamilyid.ToString();
        CrewFamilyId = 0;
        BindGridfamily(Convert.ToInt32(HiddenPK.Value.Trim()));
        panelvisa1.Visible = true;
        BindGridVisa();
        btn_Family_Reset.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        btnvisaadd.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        //-----
        lblMessage.Text = "Record Successfully Saved.";
    }
    protected void btn_Family_Reset_Click(object sender, EventArgs e)
    {
        familypanel.Visible = true;
        btn_Family_Reset.Visible = false;
        btn_family_save.Visible = true;
        btnfamilyCancel.Visible = true;
        familyreset();
        panelvisa1.Visible = false;
        panelvisa2.Visible = false;
        //----

        //Gvfamily.SelectedIndex = -1;

        btnvisaadd.Visible = false;
        btnvisasave.Visible = false;
        btnvisacancel.Visible = false;
    }
    private void familyreset()
    {
        CrewFamilyId = 0;
        HiddenFamilyPK.Value = "";
        txtfirstname.Text = "";
        txtmiddlename.Text = "";
        txtlastname.Text = "";
        ddrelation.SelectedIndex = 0;
        chbox_isnok.Checked = false;
        ddsex.SelectedIndex = 0;
        txtpob.Text = "";
        ddnationality.SelectedIndex = 0;
        txtaddress1.Text = "";
        txtaddress2.Text = "";
        txtaddress3.Text = "";
        ddcountryname.SelectedIndex = 0;
        txtstate.Text = "";
        txtcity.Text = "";
        txtpin.Text = "";
        dd_nearest_airport.SelectedIndex = 0;
        //dd_tel_countrycode.SelectedIndex = 0;
        txt_tel_Area_Code.Text = "";
        txt_tel_Number.Text = "";
        //dd_mobile_countrycode.SelectedIndex = 0;
        txt_mobile_Number.Text = "";
        //dd_fax_country_code.SelectedIndex = 0;
        txt_fax_areacode.Text = "";
        txt_fax_number.Text = "";
        txt_EMail1.Text = "";
        txt_email2.Text = "";
        txtpassportno.Text = "";
        txtissuedate.Text = "";
        txtexpirydate.Text = "";
        txtplaceofissue.Text = "";
        txtbankname.Text = "";
        txtbranch.Text = "";
        txtaccountno.Text = "";
        txtpersonalcode.Text = "";
        txtswiftcode.Text = "";
        txtibanno.Text = "";
        txtbankaddress.Text = "";
        ddtypeofremittance.SelectedIndex = 0;
        txt_DOB_Family.Text = "";
        chk_ECNR.Checked = false;
        LoadNearestAirport(Convert.ToInt32(ddcountryname.SelectedValue));
        dd_tel_countrycode.Items.Clear();
        dd_mobile_countrycode.Items.Clear();
        dd_fax_country_code.Items.Clear();
        txt_Beneficiary.Text = "";
        UtilityManager um = new UtilityManager();
        img_FamilyMember.ImageUrl = um.DownloadFileFromServer("noimage.jpg", "C");
        hfd_FamilyImage.Value = "";
        BindGridfamily(Convert.ToInt32(HiddenPK.Value.Trim()));
        chkInsuCovered.Checked = false;
        txtInsuranceId.Enabled = false;
        txtInsuCompany.Enabled = false;
        txtInsuranceId.Text = "";
        txtInsuCompany.Text = "";
    }
    protected void btnfamilyCancel_Click(object sender, EventArgs e)
    {
        CrewFamilyId = 0;
        BindGridfamily(Convert.ToInt32(HiddenPK.Value.Trim()));
        familypanel.Visible = false;
        btnfamilyCancel.Visible = false;
        btn_Family_Reset.Visible = ((Mode == "Edit") || (Mode == "New"));
        btn_family_save.Visible = false;
        //Gvfamily.SelectedIndex = -1; 

        panelvisa1.Visible = false;
        panelvisa2.Visible = false;
        btnvisacancel.Visible = false;
        btnvisaadd.Visible = false;
        btnvisasave.Visible = false;
    }
    private void BindGridVisa()
    {
        ProcessSelectVisaDetails obj1 = new ProcessSelectVisaDetails();
        CrewFamilyDocumentDetails crewFamilyDocumentDetails = new CrewFamilyDocumentDetails();
        crewFamilyDocumentDetails.CrewFamilyId = Convert.ToInt32(HiddenFamilyPK.Value.ToString());
        obj1.CrewFamilyDocumentDetails = crewFamilyDocumentDetails;
        obj1.Invoke();
        Gvvisa.DataSource = obj1.ResultSet;
        Gvvisa.DataBind();
    }
    private void VisaClear()
    {
        Hiddenvisapk.Value = "";
        txtvisaname.Text = "";
        txtvisatype.Text = "";
        txtvisano.Text = "";
        txtvisapoi.Text = "";
        txtvisaissue.Text = "";
        txtvisaexpiry.Text = "";
    }
    protected void Gvvisa_SelectIndexChanged(object sender, EventArgs e)
    {
        HiddenField hfdvisa;
        CrewFamilyDocumentDetails crewFamilyDocumentDetails = new CrewFamilyDocumentDetails();
        ProcessSelectVisaDetailsByFamilyDocumentId obj = new ProcessSelectVisaDetailsByFamilyDocumentId();
        hfdvisa = (HiddenField)Gvvisa.Rows[Gvvisa.SelectedIndex].FindControl("HiddenIdvisa");
        crewFamilyDocumentDetails.Crewfamilydocumentid = Convert.ToInt32(hfdvisa.Value.ToString());
        obj.CrewFamilyDocumentDetails = crewFamilyDocumentDetails;
        obj.Invoke();
        panelvisa2.Visible = true;
        btnvisacancel.Visible = true;
        btnvisasave.Visible = false;
        Show_Record_visa(obj.CrewFamilyDocumentDetails);

    }
    //protected void visa_Row_Editing(object sender, GridViewEditEventArgs e)
    //{
    //    HiddenField hfdvisa;
    //    CrewFamilyDocumentDetails crewFamilyDocumentDetails = new CrewFamilyDocumentDetails();
    //    ProcessSelectVisaDetailsByFamilyDocumentId obj = new ProcessSelectVisaDetailsByFamilyDocumentId();
    //    hfdvisa = (HiddenField)Gvvisa.Rows[e.NewEditIndex].FindControl("HiddenIdvisa");
    //    crewFamilyDocumentDetails.Crewfamilydocumentid = Convert.ToInt32(hfdvisa.Value.ToString());
    //    obj.CrewFamilyDocumentDetails = crewFamilyDocumentDetails;
    //    obj.Invoke();
    //    panelvisa2.Visible = true;
    //    Show_Record_visa(obj.CrewFamilyDocumentDetails);
    //    Gvvisa.SelectedIndex = e.NewEditIndex;
    //    btnvisacancel.Visible = true;
    //    btnvisasave.Visible = true;
    //}
    protected void visa_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        HiddenField hfddel;
        CrewFamilyDocumentDetails crewFamilyDocumentDetails = new CrewFamilyDocumentDetails();
        hfddel = (HiddenField)Gvvisa.Rows[e.RowIndex].FindControl("HiddenIdvisa");
        ProcessDeleteVisaDetails obj = new ProcessDeleteVisaDetails();
        crewFamilyDocumentDetails.Crewfamilydocumentid = Convert.ToInt32(hfddel.Value.ToString());
        obj.CrewFamilyDocumentDetails = crewFamilyDocumentDetails;
        obj.Invoke();
        BindGridVisa();
        if (Hiddenvisapk.Value.Trim() == hfddel.Value.ToString())
        {
            VisaClear();
        }
    }
    protected void Show_Record_visa(CrewFamilyDocumentDetails visadetails)
    {
        Hiddenvisapk.Value = visadetails.Crewfamilydocumentid.ToString();
        txtvisaname.Text = visadetails.DocumentName;
        txtvisatype.Text = visadetails.DocumentType;
        txtvisano.Text = visadetails.DocumentNumber;
        txtvisapoi.Text = visadetails.Placeofissue;
        txtvisaissue.Text = Convert.ToDateTime(visadetails.Issuedate).ToString("dd-MMM-yyyy");
        txtvisaexpiry.Text = Convert.ToDateTime(visadetails.Expirydate).ToString("dd-MMM-yyyy");
    }
    protected void btnvisasave_Click(object sender, EventArgs e)
    {
        CrewFamilyDocumentDetails visadetails = new CrewFamilyDocumentDetails();
        if (Hiddenvisapk.Value.Trim() != "")
        {
            visadetails.Crewfamilydocumentid = Convert.ToInt32(Hiddenvisapk.Value);
        }
        visadetails.CrewFamilyId = Convert.ToInt32(HiddenFamilyPK.Value.ToString());
        visadetails.DocumentName = txtvisaname.Text;
        visadetails.DocumentType = txtvisatype.Text;
        visadetails.DocumentNumber = txtvisano.Text;
        visadetails.Placeofissue = txtvisapoi.Text;
        visadetails.Issuedate = txtvisaissue.Text.Trim();
        visadetails.Expirydate = txtvisaexpiry.Text.Trim();
        visadetails.DocumentFlag = 'V';
        ProcessAddCrewMemberFamilyVisaDetails obj = new ProcessAddCrewMemberFamilyVisaDetails();
        obj.CrewFamilyDocumentDetails = visadetails;
        obj.Invoke();
        BindGridVisa();
        VisaClear();
        lblMessage.Text = "Record Successfully Saved.";
    }
    protected void btnvisaadd_Click(object sender, EventArgs e)
    {

        panelvisa2.Visible = true;
        btnvisaadd.Visible = false;
        btnvisasave.Visible = true;
        btnvisacancel.Visible = true;
        Gvvisa.SelectedIndex = -1;
        //----
        VisaClear();
    }
    protected void btnvisacancel_Click(object sender, EventArgs e)
    {
        panelvisa2.Visible = false;
        btnvisacancel.Visible = false;
        btnvisaadd.Visible = ((Mode == "Edit") || (Mode == "New"));
        btnvisasave.Visible = false;
        Gvvisa.SelectedIndex = -1;
        VisaClear();
    }
    protected void Gvvisa_DataBound(object sender, EventArgs e)
    {
        try
        {
            Gvvisa.Columns[1].Visible = false;
            Gvvisa.Columns[2].Visible = false;
            // Can Add
            if (Auth.isAdd)
            {
            }

            // Can Modify
            if (Auth.isEdit)
            {
                Gvvisa.Columns[1].Visible = ((Mode == "New") || (Mode == "Edit")) ? true : false;
            }

            // Can Delete
            if (Auth.isDelete)
            {
                Gvvisa.Columns[2].Visible = ((Mode == "New") || (Mode == "Edit")) ? true : false;
            }

            // Can Print
            if (Auth.isPrint)
            {
            }

        }
        catch
        {
            Gvvisa.Columns[1].Visible = false;
            Gvvisa.Columns[2].Visible = false;
        }


    }
    protected void Gvvisa_PreRender(object sender, EventArgs e)
    {
        if (Gvvisa.Rows.Count <= 0)
        {
            lbl_Gvvisa.Text = "No Records Found..!";
        }
    }
    protected void ddcountryname_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadNearestAirport(Convert.ToInt32(ddcountryname.SelectedValue));
        dd_tel_countrycode.Items.Clear();
        dd_mobile_countrycode.Items.Clear();
        dd_fax_country_code.Items.Clear();
        ProcessSelectCountryCode obj = new ProcessSelectCountryCode();
        obj.CountryId = Convert.ToInt32(ddcountryname.SelectedValue);
        obj.Invoke();
        dd_tel_countrycode.Items.Add(new ListItem(obj.CountryCode, ddcountryname.SelectedValue));
        dd_mobile_countrycode.Items.Add(new ListItem(obj.CountryCode, ddcountryname.SelectedValue));
        dd_fax_country_code.Items.Add(new ListItem(obj.CountryCode, ddcountryname.SelectedValue));
    }
    private void LoadNearestAirport(int CountryId)
    {
        ProcessSelectNearestAirportById obj = new ProcessSelectNearestAirportById();
        obj.CountryId = CountryId;
        obj.Invoke();

        dd_nearest_airport.DataSource = obj.ResultSet.Tables[0];
        dd_nearest_airport.DataValueField = "NearestAirportId";
        dd_nearest_airport.DataTextField = "NearestAirportName";
        dd_nearest_airport.DataBind();
    }
    //protected void Gvfamily_DataBound(object sender, GridViewRowEventArgs e)
    //{
    //    try
    //    {
    //        Gvfamily.Columns[1].Visible = false;
    //        Gvfamily.Columns[2].Visible = false;
    //        // Can Add
    //        if (Auth.isAdd)
    //        {
    //        }

    //        // Can Modify
    //        if (Auth.isEdit)
    //        {
    //            Gvfamily.Columns[1].Visible = ((Mode == "New") || (Mode == "Edit")) ? true : false;
    //        }

    //        // Can Delete
    //        if (Auth.isDelete)
    //        {
    //            Gvfamily.Columns[2].Visible = ((Mode == "New") || (Mode == "Edit")) ? true : false;
    //        }

    //        // Can Print
    //        if (Auth.isPrint)
    //        {
    //        }
    //        Image img = ((Image)e.Row.FindControl("imgattach"));
    //        if (e.Row.RowType == DataControlRowType.DataRow)
    //        {
    //            if (DataBinder.Eval(e.Row.DataItem, "ImagePath") == null)
    //            {

    //                img.Visible = false;
    //            }
    //            else if (DataBinder.Eval(e.Row.DataItem, "ImagePath").ToString() == "")
    //            {

    //                img.Visible = false;
    //            }
    //            else
    //            {
    //                img.Attributes.Add("onclick", "javascript:Show_Image_Large1('/Home/EMANAGERBLOB/HRD/CrewPhotos/" + DataBinder.Eval(e.Row.DataItem, "ImagePath").ToString() + "');");
    //                img.ToolTip = "Click to Preview";
    //                img.Style.Add("cursor", "hand");
    //            }
    //        }
    //    }
    //    catch
    //    {
    //        Gvfamily.Columns[1].Visible = false;
    //        Gvfamily.Columns[2].Visible = false;
    //    }


    //}
    //protected void Gvfamily_PreRender(object sender, EventArgs e)
    //{
    //    if (Gvfamily.Rows.Count <= 0)
    //    {
    //        lbl_Gvfamily.Text = "No Records Found..!";
    //    }
    //}
    #endregion
    //--------------------
    #region TAB-4 EXPERIENCE DETAILS    
    private void BindExperienceDetailsGrid(String Sort, int id)
    {
        //-----------------------------
        img_Crew3.ImageUrl = img_Crew.ImageUrl;
        txt_MemberId2.Text = txt_MemberId.Text;
        txt_LastVessel2.Text = (ddl_Nationality.SelectedIndex == 0) ? "" : ddl_Nationality.SelectedItem.ToString();
        ddcurrentrank2.Text = lblCurrRank.Text;
        txt_LastVessel2.Text = lblCurrentVessel.Text;
        txt_Status2.Text = txt_Status.Text;
        txt_FirstName2.Text = txt_FirstName.Text;
        txt_MiddleName2.Text = txt_MiddleName.Text;
        txt_LastName2.Text = txt_LastName.Text;
        txtPassport2.Text = txt_Passport.Text;

        //--------------------------EXPERIENCE DETAILS(Company)
        ProcessSelectCrewMemberExperienceDetails_MTMSM processselectexperiencedetails_MTMSM = new ProcessSelectCrewMemberExperienceDetails_MTMSM();
        try
        {
            ExperienceDetails obj = new ExperienceDetails();
            obj.CrewId = id;
            processselectexperiencedetails_MTMSM.ExperienceDetails = obj;
            processselectexperiencedetails_MTMSM.Invoke();
            processselectexperiencedetails_MTMSM.ResultSet.Tables[0].DefaultView.Sort = Sort;
        }
        catch { }
        GridView3.DataSource = processselectexperiencedetails_MTMSM.ResultSet.Tables[0];
        GridView3.DataBind();
        GridView3.Attributes.Add("MySort", Sort);
        if (processselectexperiencedetails_MTMSM.ResultSet.Tables[0].Rows.Count <= 0) { lbl_GridView3.Text = "No Records Found..!"; }


        //--------------------------EXPERIENCE DETAILS(OTHER)
        ProcessSelectCrewMemberExperienceDetails processselectexperiencedetails = new ProcessSelectCrewMemberExperienceDetails();
        try
        {
            ExperienceDetails obj = new ExperienceDetails();
            obj.CrewId = id;
            processselectexperiencedetails.ExperienceDetails = obj;
            processselectexperiencedetails.Invoke();
            processselectexperiencedetails.ResultSet.Tables[0].DefaultView.Sort = Sort;
        }
        catch { }
        GridView3_1.DataSource = processselectexperiencedetails.ResultSet.Tables[0];
        GridView3_1.DataBind();
        GridView3_1.Attributes.Add("MySort", Sort);
        if (processselectexperiencedetails.ResultSet.Tables[0].Rows.Count <= 0) { lbl_GridView3_1.Text = "No Records Found..!"; }



        HiddenPK2.Value = "";
        txtcompname.Text = "";
        ddl_Rank1.SelectedIndex = 0;
        txtfromdate.Text = "";
        txttodate.Text = "";
        txtvesselname.Text = "";
        ddl_VesselType.SelectedIndex = 0;
        //txtregistry.Text = "";
        //txtdwt.Text = "";
        //txtgwt.Text = "";
        txtbhp.Text = "";
        txtbhp1.Text = "";
    }
    protected void on_Sorting(object sender, GridViewSortEventArgs e)
    {
        BindExperienceDetailsGrid(e.SortExpression, Convert.ToInt32(HiddenPK.Value.Trim()));
    }
    protected void on_Sorted(object sender, EventArgs e)
    {

    }

    protected void Show_Record3(ExperienceDetails Experience)
    {
        string Mess;
        Mess = "";

        HiddenPK2.Value = Experience.ExperienceId.ToString();
        txtcompname.Text = Experience.Companyname;
        //ddl_Rank1.SelectedValue = Experience.RankId.ToString();
        Mess = Mess + Alerts.Set_DDL_Value(ddl_Rank1, Experience.RankId.ToString(), "Rank");
        txtfromdate.Text = Experience.SignOnDate.ToString("dd-MMM-yyyy");
        txttodate.Text = Experience.SignOffDate.ToString("dd-MMM-yyyy");
        txtvesselname.Text = Experience.Vesselname;
        //ddl_Sign_Off_Reason.SelectedValue =Experience.SignOffReasonId.ToString();
        Mess = Mess + Alerts.Set_DDL_Value(ddl_Sign_Off_Reason, Experience.SignOffReasonId.ToString(), "Sign Off Reason");
        //ddl_VesselType.SelectedValue = Experience.VesseltypeId.ToString();   
        Mess = Mess + Alerts.Set_DDL_Value(ddl_VesselType, Experience.VesseltypeId.ToString(), "Vessel Type");
        //txtregistry.Text = Experience.Registry;
        Mess = Mess + Alerts.Set_DDL_Value(ddl_Rank1, Experience.RankId.ToString(), "Rank");

        //txtdwt.Text = Experience.Dwt;
        //txtgwt.Text = Experience.Gwt;
        txtbhp.Text = Experience.Bhp;
        txtbhp1.Text = Experience.Bhp1;
        pnl_Experience.Visible = true;
    }
    // VIEW THE RECORD
    protected void GridView3_SelectIndexChanged(object sender, EventArgs e)
    {
        HiddenField hfd;
        ExperienceDetails experiencedetails = new ExperienceDetails();
        ProcessSelectCrewMemberExperienceDetailsById selectexperiencedetails = new ProcessSelectCrewMemberExperienceDetailsById();
        hfd = (HiddenField)GridView3_1.Rows[GridView3_1.SelectedIndex].FindControl("HiddenId");
        experiencedetails.ExperienceId = Convert.ToInt32(hfd.Value.ToString());
        selectexperiencedetails.ExperienceDetails = experiencedetails;
        selectexperiencedetails.Invoke();
        Show_Record3(selectexperiencedetails.ExperienceDetails);
        //Deactivate_Experience();

        btn_Add3.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        btn_Experience_Cancel.Visible = true;
        btn_Save3.Visible = false;

    }
    // EDIT THE RECORD
    //protected void Row_Editing(object sender, GridViewEditEventArgs e)
    //{
    //    HiddenField hfd;
    //    ExperienceDetails experiencedetails = new ExperienceDetails();
    //    ProcessSelectCrewMemberExperienceDetailsById selectexperiencedetails = new ProcessSelectCrewMemberExperienceDetailsById();
    //    hfd = (HiddenField)GridView3_1.Rows[e.NewEditIndex].FindControl("HiddenId");
    //    experiencedetails.ExperienceId = Convert.ToInt32(hfd.Value.ToString());
    //    selectexperiencedetails.ExperienceDetails = experiencedetails;
    //    selectexperiencedetails.Invoke();
    //    Show_Record3(selectexperiencedetails.ExperienceDetails);
    //    GridView3_1.SelectedIndex = e.NewEditIndex;
    //    //Activate_Experience();
    //    btn_Save3.Visible = true;
    //    btn_Experience_Cancel.Visible = true;
    //}
    // DELETE THE RECORD
    protected void Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        HiddenField hfd;
        ExperienceDetails exp = new ExperienceDetails();
        hfd = (HiddenField)GridView3_1.Rows[e.RowIndex].FindControl("HiddenId");
        ProcessDeleteCrewMemberExperienceDetailsById obj = new ProcessDeleteCrewMemberExperienceDetailsById();
        exp.ExperienceId = Convert.ToInt32(hfd.Value.ToString());
        obj.ExperienceDetails = exp;
        obj.Invoke();
        BindExperienceDetailsGrid(GridView3_1.Attributes["MySort"], Convert.ToInt32(HiddenPK.Value.Trim()));
    }
    protected void DataBound(object sender, EventArgs e)
    {
        try
        {
            GridView3_1.Columns[1].Visible = false;
            GridView3_1.Columns[2].Visible = false;
            // Can Add
            if (Auth.isAdd)
            {
            }

            // Can Modify
            if (Auth.isEdit)
            {
                GridView3_1.Columns[1].Visible = ((Mode == "New") || (Mode == "Edit")) ? true : false;
            }

            // Can Delete
            if (Auth.isDelete)
            {
                GridView3_1.Columns[2].Visible = ((Mode == "New") || (Mode == "Edit")) ? true : false;
            }

            // Can Print
            if (Auth.isPrint)
            {
            }

        }
        catch
        {
            GridView3_1.Columns[1].Visible = false;
            GridView3_1.Columns[2].Visible = false;
        }


    }
    protected void btn_Save_Click3(object sender, EventArgs e)
    {
        if (DateTime.Parse(txtfromdate.Text) > DateTime.Parse(txttodate.Text))
        {
            lblMessage.Text = "From date must be less or equal to todate.";
            return;
        }
        ExperienceDetails experiencedetails = new ExperienceDetails();
        try
        {
            if (HiddenPK2.Value.ToString().Trim() == "")
            {
                experiencedetails.Createdby = Convert.ToInt32(Session["loginid"].ToString());

            }
            else
            {
                experiencedetails.ExperienceId = Convert.ToInt32(HiddenPK2.Value);
                experiencedetails.Modifiedby = Convert.ToInt32(Session["loginid"].ToString());
            }
            experiencedetails.CrewId = Convert.ToInt32(HiddenPK.Value.Trim());
            experiencedetails.Companyname = txtcompname.Text;
            experiencedetails.RankId = Convert.ToInt32(ddl_Rank1.SelectedValue);
            experiencedetails.SignOnDate = DateTime.Parse(txtfromdate.Text);
            experiencedetails.SignOffDate = DateTime.Parse(txttodate.Text);
            experiencedetails.Vesselname = txtvesselname.Text;
            experiencedetails.VesseltypeId = Convert.ToInt32(ddl_VesselType.SelectedValue);
            experiencedetails.SignOffReasonId = Convert.ToInt32(ddl_Sign_Off_Reason.SelectedValue);
            //experiencedetails.Registry = txtregistry.Text;
            //experiencedetails.Dwt = txtdwt.Text;
            //experiencedetails.Gwt = txtgwt.Text;
            experiencedetails.Bhp = txtbhp.Text;
            experiencedetails.Bhp1 = txtbhp1.Text;
            experiencedetails.Expflag = 'O';
            ProcessAddCrewMemberExperienceDetails expdetails = new ProcessAddCrewMemberExperienceDetails();
            expdetails.ExperienceDetails = experiencedetails;
            expdetails.Invoke();
            btn_Add3.Visible = false;// ((Mode == "Edit") || (Mode == "New")) ? true : false;
            lblMessage.Text = "Record Successfully Saved.";
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Record Not Saved.";
            //Response.Write(ex.Message.ToString());
        }
        BindExperienceDetailsGrid(GridView3.Attributes["MySort"], Convert.ToInt32(HiddenPK.Value.Trim()));
    }
    protected void btnadd_Click3(object sender, EventArgs e)
    {

        HiddenPK2.Value = "";
        txtcompname.Text = "";
        ddl_Rank1.SelectedIndex = 0;
        txtfromdate.Text = "";
        txttodate.Text = "";
        txtvesselname.Text = "";
        ddl_VesselType.SelectedIndex = 0;
        ddl_Sign_Off_Reason.SelectedIndex = 0;
        //txtregistry.Text = "";
        //txtdwt.Text = "";
        //txtgwt.Text = "";
        txtbhp.Text = "";
        txtbhp1.Text = "";

        btn_Save3.Visible = true;
        btn_Add3.Visible = false;
        btn_Experience_Cancel.Visible = true;
        pnl_Experience.Visible = true;
        GridView3_1.SelectedIndex = -1;
    }

    //private void Activate_Experience()
    //{

    //    txtcompname.ReadOnly = false ;
    //    ddl_Rank1.Enabled = true;
    //    txtfromdate.ReadOnly = false;
    //    txttodate.ReadOnly = false;
    //    txtvesselname.ReadOnly = false; 
    //    ddl_Sign_Off_Reason.Enabled = true;
    //    ddl_VesselType.Enabled = true;
    //    txtregistry.ReadOnly = false;
    //    txtdwt.ReadOnly = false;
    //    txtgwt.ReadOnly = false;
    //    txtbhp.ReadOnly = false;
    //    GridView3_1.Columns[1].Visible = true ;
    //    GridView3_1.Columns[2].Visible = true;      

    //}
    //private void Deactivate_Experience()
    //{
    //    txtcompname.ReadOnly = true;
    //    ddl_Rank1.Enabled = false;
    //    txtfromdate.ReadOnly = true;
    //    txttodate.ReadOnly = true;
    //    txtvesselname.ReadOnly = true; 
    //    ddl_Sign_Off_Reason.Enabled = false;
    //    ddl_VesselType.Enabled = false;
    //    txtregistry.ReadOnly = true;
    //    txtdwt.ReadOnly = true;
    //    txtgwt.ReadOnly = true;
    //    txtbhp.ReadOnly = true;
    //}
    protected void btn_Experience_Cancel_Click(object sender, EventArgs e)
    {
        if (Mode == "New")
        {
            btn_Add3.Visible = (Auth.isAdd || Auth.isEdit);
        }
        else if (Mode == "Edit")
        {
            btn_Add3.Visible = (Auth.isAdd || Auth.isEdit);
        }
        btn_Experience_Cancel.Visible = false;
        pnl_Experience.Visible = false;
        btn_Save3.Visible = false;
        btn_Add3.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        GridView3_1.SelectedIndex = -1;
    }
    protected void GridView3_PreRender(object sender, EventArgs e)
    {
        if (GridView3.Rows.Count <= 0) { lbl_GridView3.Text = "No Records Found..!"; }
    }
    protected void GridView3_1_PreRender(object sender, EventArgs e)
    {
        if (GridView3_1.Rows.Count <= 0) { lbl_GridView3_1.Text = "No Records Found..!"; }
        for (int i = 0; i < GridView3_1.Rows.Count - 1; i++)
        {
            //GridView3_1.Rows[i].Cells[2].FindControl  
        }
    }
    //------

    //private void Activate_Contact()
    //{

    //    txt_P_Address.ReadOnly = false;
    //    txt_P_Address1.ReadOnly = false;
    //    txt_P_Address2.ReadOnly = false;
    //    ddl_P_Country.Enabled = true;
    //    txt_P_State.ReadOnly = false;
    //    txt_P_City.ReadOnly = false;
    //    txt_P_Pin.ReadOnly = false;
    //    ddl_P_Airport.Enabled = true;

    //    ddl_P_CountryCode_Tel.Enabled = true;
    //    txt_P_Area_Code_Tel.ReadOnly = false;
    //    txt_P_Number_Tel.ReadOnly = false;

    //    ddl_P_CountryCode_Mobile.Enabled = true;
    //    txt_P_Number_Mobile.ReadOnly = false;

    //    ddl_P_CountryCode_Fax.Enabled = true;
    //    txt_P_Area_Code_Fax.ReadOnly = false;
    //    txt_P_Number_Fax.ReadOnly = false;
    //    txt_P_EMail1.ReadOnly = false;
    //    txt_P_EMail2.ReadOnly = false;  

    //    //-----------------------------------

    //    txt_C_Address.ReadOnly = false;  
    //    txt_C_Address1.ReadOnly = false;  
    //    txt_C_Address2.ReadOnly = false;
    //    ddl_C_Country.Enabled = true;
    //    txt_C_State.ReadOnly = false;  
    //    txt_C_City.ReadOnly = false;  
    //    txt_C_Pin.ReadOnly = false;
    //    ddl_C_Airport.Enabled = true;

    //    ddl_C_CountryCode_Tel.Enabled = true; 
    //    txt_C_Area_Code_Tel.ReadOnly = false;  
    //    txt_C_Number_Tel.ReadOnly = false;

    //    ddl_C_CountryCode_Mobile.Enabled = true;
    //    txt_C_Number_Mobile.ReadOnly = false;

    //    ddl_C_CountryCode_Fax.Enabled = true;
    //    txt_C_Area_Code_Fax.ReadOnly = false;  
    //    txt_C_Number_Fax.ReadOnly = false;  
    //    txt_C_EMail1.ReadOnly = false;  
    //    txt_C_EMail2.ReadOnly = false;  
    //}

    //private void Deactivate_Contact()
    //{
    //    txt_P_Address.ReadOnly = true;
    //    txt_P_Address1.ReadOnly = true;
    //    txt_P_Address2.ReadOnly = true;
    //    ddl_P_Country.Enabled = false ;
    //    txt_P_State.ReadOnly = true;
    //    txt_P_City.ReadOnly = true;
    //    txt_P_Pin.ReadOnly = true;
    //    ddl_P_Airport.Enabled = false;

    //    ddl_P_CountryCode_Tel.Enabled = false;
    //    txt_P_Area_Code_Tel.ReadOnly = true;
    //    txt_P_Number_Tel.ReadOnly = true;

    //    ddl_P_CountryCode_Mobile.Enabled = false;
    //    txt_P_Number_Mobile.ReadOnly = true;

    //    ddl_P_CountryCode_Fax.Enabled = false;
    //    txt_P_Area_Code_Fax.ReadOnly = true;
    //    txt_P_Number_Fax.ReadOnly = true;
    //    txt_P_EMail1.ReadOnly = true;
    //    txt_P_EMail2.ReadOnly = true;

    //    //-----------------------------------

    //    txt_C_Address.ReadOnly = true;
    //    txt_C_Address1.ReadOnly = true;
    //    txt_C_Address2.ReadOnly = true;
    //    ddl_C_Country.Enabled = false;
    //    txt_C_State.ReadOnly = true;
    //    txt_C_City.ReadOnly = true;
    //    txt_C_Pin.ReadOnly = true;
    //    ddl_C_Airport.Enabled = true;

    //    ddl_C_CountryCode_Tel.Enabled = false;
    //    txt_C_Area_Code_Tel.ReadOnly = true;
    //    txt_C_Number_Tel.ReadOnly = true;

    //    ddl_C_CountryCode_Mobile.Enabled = false;
    //    txt_C_Number_Mobile.ReadOnly = true;

    //    ddl_C_CountryCode_Fax.Enabled = false;
    //    txt_C_Area_Code_Fax.ReadOnly = true;
    //    txt_C_Number_Fax.ReadOnly = true;
    //    txt_C_EMail1.ReadOnly = true;
    //    txt_C_EMail2.ReadOnly = true;  
    //}
    //private void DeActivate_Personal()
    //{
    //    //txt_MemberId
    //    txt_FirstName.ReadOnly = true;
    //    txt_MiddleName.ReadOnly = true;
    //    txt_LastName.ReadOnly = true;
    //    txt_DOB.ReadOnly = true;
    //    txt_Age.ReadOnly = true;
    //    txt_Bmi.ReadOnly = true;
    //    ddl_Nationality.Enabled = false;
    //    ddl_Sex.Enabled = false;

    //    ddcountryofbirth.Enabled = false;
    //    ddmaritalstatus.Enabled = false;
    //    txtdatefirstjoin.ReadOnly = true;
    //    ddrankapp.Enabled = false;
    //    ddcurrentrank.ReadOnly = true;
    //    ddrecruitingoff.Enabled = false;
    //    txtheight.ReadOnly = true;
    //    txtweight.ReadOnly = true;
    //    txtwaist.ReadOnly = true;
    //    txt_Status.ReadOnly = true; 
    //    ddl_Academy_Quali.Enabled = false;
    //    ddl_Shirt.Enabled = false;
    //    ddl_Shoes.Enabled = false;
    //    txt_LastVessel.ReadOnly = true; 
    //}
    //private void Activate_Personal()
    //{

    //    //txt_MemberId
    //    txt_FirstName.ReadOnly = false;
    //    txt_MiddleName.ReadOnly = false;
    //    txt_LastName.ReadOnly = false;
    //    txt_DOB.ReadOnly = false;
    //    txt_Age.ReadOnly = false;
    //    txt_Bmi.ReadOnly = false;
    //    ddl_Nationality.Enabled = true;
    //    ddl_Sex.Enabled = true;

    //    ddcountryofbirth.Enabled = true;
    //    ddmaritalstatus.Enabled = true;
    //    txtdatefirstjoin.ReadOnly = false;
    //    ddrankapp.Enabled = true;
    //    ddcurrentrank.ReadOnly = false;
    //    ddrecruitingoff.Enabled = true;
    //    txtheight.ReadOnly = false;
    //    txtweight.ReadOnly = false;
    //    txtwaist.ReadOnly = false;
    //    txt_Status.ReadOnly = false;
    //    ddl_Academy_Quali.Enabled = true;
    //    ddl_Shirt.Enabled = true;
    //    ddl_Shoes.Enabled = true;
    //    txt_LastVessel.ReadOnly = false; 
    //}
    //public void bindcountrycode()
    //{
    //    ProcessGetCountry getcountrycode = new ProcessGetCountry();
    //    try
    //    {
    //        getcountrycode.Invoke();
    //    }
    //    catch (Exception er)
    //    {

    //    }

    //    ddl_P_CountryCode_Tel.DataTextField = "CountryCode";
    //    ddl_P_CountryCode_Tel.DataValueField = "CountryId";
    //    ddl_P_CountryCode_Tel.DataSource = getcountrycode.ResultSet;
    //    ddl_P_CountryCode_Tel.DataBind();

    //    ddl_P_CountryCode_Mobile.DataTextField = "CountryCode";
    //    ddl_P_CountryCode_Mobile.DataValueField = "CountryId";
    //    ddl_P_CountryCode_Mobile.DataSource = getcountrycode.ResultSet;
    //    ddl_P_CountryCode_Mobile.DataBind();

    //    ddl_P_CountryCode_Fax.DataTextField = "CountryCode";
    //    ddl_P_CountryCode_Fax.DataValueField = "CountryId";
    //    ddl_P_CountryCode_Fax.DataSource = getcountrycode.ResultSet;
    //    ddl_P_CountryCode_Fax.DataBind();


    //    ddl_C_CountryCode_Tel.DataTextField = "CountryCode";
    //    ddl_C_CountryCode_Tel.DataValueField = "CountryId";
    //    ddl_C_CountryCode_Tel.DataSource = getcountrycode.ResultSet;
    //    ddl_C_CountryCode_Tel.DataBind();


    //    ddl_C_CountryCode_Mobile.DataTextField = "CountryCode";
    //    ddl_C_CountryCode_Mobile.DataValueField = "CountryId";
    //    ddl_C_CountryCode_Mobile.DataSource = getcountrycode.ResultSet;
    //    ddl_C_CountryCode_Mobile.DataBind();

    //    ddl_C_CountryCode_Fax.DataTextField = "CountryCode";
    //    ddl_C_CountryCode_Fax.DataValueField = "CountryId";
    //    ddl_C_CountryCode_Fax.DataSource = getcountrycode.ResultSet;
    //    ddl_C_CountryCode_Fax.DataBind();

    //}
    #endregion
    //-----------------------
    public void BindCrewApprovals()
    {
        string str = "select * from DBO.vw_CrewApprovalCouterList_1 Where ApprovalStatus='Approved' and crewid=" + Session["CrewId"].ToString() + " order by approvedon desc";
        gv_CrewApproval.DataSource = Common.Execute_Procedures_Select_ByQueryCMS(str);
        gv_CrewApproval.DataBind();
    }
    public string getCssStatus(int Level, int PlanningID)
    {
        string css = "";
        //string sql = " select case when ApprovedOn is null then '../Images/red_circle.png' else '../Images/green_circle.gif' end css,Result from DBO.CrewPlanningApprovalComments where PlanningId=" + PlanningID + " and ApprovalLevel="+ Level + " ";
        string sql = " select ApprovedOn,Result from DBO.CrewPlanningApprovalComments where PlanningId=" + PlanningID + " and ApprovalLevel=" + Level + " ";
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (Dt.Rows.Count > 0)
        {
            if (Dt.Rows[0][1].ToString() == "A")
                css = "<img src='" + "../Images/green_circle.gif" + "' />";
            else if (Dt.Rows[0][1].ToString() == "R")
                css = "<img src='" + "../Images/exclamation-mark-yellow.png" + "' />";
            else
                css = "<img src='" + "../Images/red_circle.png" + "' />";
        }
        else
        {
            //css = "<img src='" + "../Images/exclamation-mark-yellow.png" + "' />" ;
            //css = "<img src='" + "../Images/red_circle.png" + "' />";
        }
        return css;
    }
    //protected void imgbtn_Documents_Click(object sender, ImageClickEventArgs e)
    //{
    //    Response.Redirect("CrewDocument.aspx");
    //}
    //protected void imgbtn_CRM_Click(object sender, ImageClickEventArgs e)
    //{

    //    Response.Redirect("CrewCRMDocument.aspx");
    //}
    //protected void imgbtn_Search_Click(object sender, ImageClickEventArgs e)
    //{
    //    Response.Redirect("CrewActivities.aspx");
    //}
    protected void cmdsendmail_Click(object sender, EventArgs e)
    {
        Response.Redirect("CrewDocumentsDetail.aspx");
    }
    protected void txt_DOB_TextChanged(object sender, EventArgs e)
    {
        int cal_age;
        DateTime date_today;
        TimeSpan t1;
        date_today = System.DateTime.Now.Date;
        t1 = date_today - Convert.ToDateTime(txt_DOB.Text);
        cal_age = (Convert.ToInt32(t1.TotalDays) / 365);
        txt_Age.Text = cal_age.ToString();
    }

    // ----------- Show Grading Remarks ---------------

    protected void btnShowRemarks_Click(object sender, EventArgs e)
    {
        int CrewBonusId = Common.CastAsInt32(hfCBId.Value);
        int MailUserMode = Common.CastAsInt32(hfMUM.Value);

        string SQL = "SELECT MailAddress,Comments FROM CrewContractBonusDetails WHERE CrewBonusId=" + CrewBonusId + " AND MailUserMode=" + MailUserMode;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        lblEmail.Text = dt.Rows[0]["MailAddress"].ToString();
        txtRemarks.Text = dt.Rows[0]["Comments"].ToString();

        dv_ViewRemarks.Visible = true;
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        hfCBId.Value = "";
        hfMUM.Value = "";
        lblEmail.Text = "";
        txtRemarks.Text = "";
        dv_ViewRemarks.Visible = false;
    }

    // ------------------------------------------------

    protected void imgTechSupdtEdit_Click(object sender, EventArgs e)
    {
        int CrewBonusId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        string SQL = "SELECT [GUID] FROM CrewContractBonusDetails WHERE CrewBonusId = " + CrewBonusId + " AND MailUserMode = 3 ";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "tech", "window.open('../Public/AssessmentMail.aspx?key=" + dt.Rows[0]["GUID"].ToString() + "', '_blank', '', '');", true);
    }
    protected void imgFleetMgrEdit_Click(object sender, EventArgs e)
    {
        int CrewBonusId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        string SQL = "SELECT [GUID] FROM CrewContractBonusDetails WHERE CrewBonusId = " + CrewBonusId + " AND MailUserMode = 4 ";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "tech", "window.open('../Public/AssessmentMail.aspx?key=" + dt.Rows[0]["GUID"].ToString() + "', '_blank', '', '');", true);
    }
    protected void imgMarineSupdtEdit_Click(object sender, EventArgs e)
    {
        int CrewBonusId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        string SQL = "SELECT [GUID] FROM CrewContractBonusDetails WHERE CrewBonusId = " + CrewBonusId + " AND MailUserMode = 5 ";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "tech", "window.open('../Public/AssessmentMail.aspx?key=" + dt.Rows[0]["GUID"].ToString() + "', '_blank', '', '');", true);
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        ShowAssessments();
    }
    protected void btnVerify_Click(object sender, EventArgs e)
    {
        string sql = " update CrewPersonalDetails  set VerifiedBy='" + Session["UserName"].ToString() + "' , VerifiedOn=getdate() Where CrewId =" + Session["CrewId"].ToString();

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
    }


    protected void b5_Click(object sender, EventArgs e)
    {
        Response.Redirect("CrewDocument.aspx");
    }

    protected void b6_Click(object sender, EventArgs e)
    {
        Response.Redirect("CrewCRMDocument.aspx");
    }




    protected void Gvvisa_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int Rowindx = row.RowIndex;
            HiddenField hfdvisa;
            CrewFamilyDocumentDetails crewFamilyDocumentDetails = new CrewFamilyDocumentDetails();
            ProcessSelectVisaDetailsByFamilyDocumentId obj = new ProcessSelectVisaDetailsByFamilyDocumentId();
            hfdvisa = (HiddenField)Gvvisa.Rows[Rowindx].FindControl("HiddenIdvisa");
            crewFamilyDocumentDetails.Crewfamilydocumentid = Convert.ToInt32(hfdvisa.Value.ToString());
            obj.CrewFamilyDocumentDetails = crewFamilyDocumentDetails;
            obj.Invoke();
            panelvisa2.Visible = true;
            Show_Record_visa(obj.CrewFamilyDocumentDetails);
            Gvvisa.SelectedIndex = Rowindx;
            btnvisacancel.Visible = true;
            btnvisasave.Visible = true;
        }
    }

    protected void Gvvisa_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    

    protected void btnEditVisa_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        HiddenField hfdvisa;
        CrewFamilyDocumentDetails crewFamilyDocumentDetails = new CrewFamilyDocumentDetails();
        ProcessSelectVisaDetailsByFamilyDocumentId obj = new ProcessSelectVisaDetailsByFamilyDocumentId();
        hfdvisa = (HiddenField)Gvvisa.Rows[Rowindx].FindControl("HiddenIdvisa");
        crewFamilyDocumentDetails.Crewfamilydocumentid = Convert.ToInt32(hfdvisa.Value.ToString());
        obj.CrewFamilyDocumentDetails = crewFamilyDocumentDetails;
        obj.Invoke();

        Show_Record_visa(obj.CrewFamilyDocumentDetails);
        panelvisa2.Visible = true;
        Gvvisa.SelectedIndex = Rowindx;
        btnvisacancel.Visible = true;
        btnvisasave.Visible = true;
        btnvisaadd.Visible = false;
    }

    protected void GridView3_1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int Rowindx = row.RowIndex;
            HiddenField hfd;
            ExperienceDetails experiencedetails = new ExperienceDetails();
            ProcessSelectCrewMemberExperienceDetailsById selectexperiencedetails = new ProcessSelectCrewMemberExperienceDetailsById();
            hfd = (HiddenField)GridView3_1.Rows[Rowindx].FindControl("HiddenId");
            experiencedetails.ExperienceId = Convert.ToInt32(hfd.Value.ToString());
            selectexperiencedetails.ExperienceDetails = experiencedetails;
            selectexperiencedetails.Invoke();
            Show_Record3(selectexperiencedetails.ExperienceDetails);
            GridView3_1.SelectedIndex = Rowindx;
            //Activate_Experience();
            btn_Save3.Visible = true;
            btn_Experience_Cancel.Visible = true;
        }
    }

    protected void GridView3_1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    protected void btnAddBankInfo_Click(object sender, EventArgs e)
    {

        hdnCrewBankId.Value = "";
        ddlAccountType.SelectedIndex = 0;
        //txtACHFirstName.Text = "";
        //txtACHLastName.Text = "";
        txtACNo.Text = "";
        txtReAcNo.Text = "";
        txtBName.Text = "";
        txtBBName.Text = "";
        ddlBankCountry.SelectedIndex = 0;
        txtIFSCCode.Text = "";
        txtBSwiftCode.Text = "";
        txtBIBAN.Text = "";
        txtBBeneficiary.Text = "";
        txtBAddress.Text = "";
        txtBBankCity.Text = "";
        chkActive.Checked = true;
        chkActive.Enabled = false;

        btnSaveBankInfo.Visible = true;
        btnAddBankInfo.Visible = false;
        btnCancelBankInfo.Visible = true;
        PnlBankDetails.Visible = true;
        GvBankDetails.SelectedIndex = -1;
    }

    protected void btnSaveBankInfo_Click(object sender, EventArgs e)
    {
        if (txtACNo.Text.Trim() != txtReAcNo.Text.Trim())
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ddd", "alert('A/C # do not match with Re-enter A/C #.')", true);
            txtReAcNo.Focus();
            return;
        }
        if (ddlBankCountry.SelectedItem.Text.Trim().ToUpper() == "INDIA" && string.IsNullOrWhiteSpace(txtIFSCCode.Text))
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ddd", "alert('IFSC Code is mandatory for Selected Country.')", true);
            txtIFSCCode.Focus();
            return;
        }
        int CrewBankId = 0;
        int IsActive = 0;
        if (chkActive.Checked)
        {
            IsActive = 1;
        }
        if (hdnCrewBankId.Value != "")
        {
            CrewBankId = Convert.ToInt32(hdnCrewBankId.Value);
        }
        DataTable dtIsActiveAc = Common.Execute_Procedures_Select_ByQueryCMS("EXEC DBO.isCheckActiveBankAccounttype " + Convert.ToInt32(HiddenPK.Value.Trim()) + ","+ IsActive + ",'"+ ddlAccountType.SelectedValue +"'");
        if (dtIsActiveAc.Rows.Count > 0)
        {
            int isActiveAc = Convert.ToInt32(dtIsActiveAc.Rows[0]["IsActiveAc"]);
            if (isActiveAc == 1)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "ddd", "alert('System is allow only single primary account.')", true);
                ddlAccountType.Focus();
                return;
            }
            if (isActiveAc == 2)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "ddd", "alert('System is allow only single active Secondary account.')", true);
                ddlAccountType.Focus();
                return;
            }
        }

        try
        {
            Common.Set_Procedures("InsertUpdateBankDetails");
            Common.Set_ParameterLength(16);
            Common.Set_Parameters(
                    new MyParameter("@CrewBankId", CrewBankId),
                    new MyParameter("@CrewId", Convert.ToInt32(HiddenPK.Value )),
                    new MyParameter("@AccountType", ddlAccountType.SelectedValue),
                    //new MyParameter("@FirstName", txtACHFirstName.Text.Trim()),
                    //new MyParameter("@LastName", txtACHLastName.Text.Trim()),
                    new MyParameter("@BankName", txtBBName.Text.Trim()),
                    new MyParameter("@BankaccountNo", ProjectCommon.Encrypt(txtACNo.Text.Trim(), "qwerty1235")),
                    new MyParameter("@BankAddress", txtBAddress.Text.Trim()),
                    new MyParameter("@BranchName", txtBBName.Text.Trim()),
                    new MyParameter("@CountryId", Convert.ToInt32(ddlBankCountry.SelectedValue)),
                    new MyParameter("@Beneficiary", txtBBeneficiary.Text.Trim()),
                    new MyParameter("@BankCity", txtBBankCity.Text.Trim()),
                    new MyParameter("@IFSCCode",txtIFSCCode.Text.Trim()),
                    new MyParameter("@SwiftCode",txtBSwiftCode.Text.Trim()),
                    new MyParameter("@IBAN", txtBIBAN.Text.Trim()),
                    new MyParameter("@TypeOfRemittanceId", ddlBTypeOfRemitteance.SelectedValue),
                    new MyParameter("@isActive", IsActive),
                    new MyParameter("@CreatedBy", Convert.ToInt32(Session["loginid"].ToString())));

            DataSet ds = new DataSet();
            bool res = Common.Execute_Procedures_IUD_CMS(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                btnAddBankInfo.Visible = false;// ((Mode == "Edit") || (Mode == "New")) ? true : false;
                                               // lblMessage.Text = "Record Successfully Saved.";
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "ddd", "alert('Record Successfully Saved.')", true);
            }
        }
        catch(Exception ex)
        {
            //lblMessage.Text = "Record Not Saved.";
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ddd", "alert('Record Not Saved.')", true);
        }
        BindBankDetailsGrid(Convert.ToInt32(HiddenPK.Value.Trim()));
    }

protected void btnCancelBankInfo_Click(object sender, EventArgs e)
    {
        if (Mode == "New")
        {
            btnAddBankInfo.Visible = (Auth.isAdd || Auth.isEdit);
        }
        else if (Mode == "Edit")
        {
            btnAddBankInfo.Visible = (Auth.isAdd || Auth.isEdit);
        }
        btnCancelBankInfo.Visible = false;
        PnlBankDetails.Visible = false;
        btnSaveBankInfo.Visible = false;
        btnAddBankInfo.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        GvBankDetails.SelectedIndex = -1;
    }

    private void BindBankDetailsGrid(int id)
    {
        //-----------------------------
        img_Crew3.ImageUrl = img_Crew.ImageUrl;
        txt_MemberId2.Text = txt_MemberId.Text;
        txt_LastVessel2.Text = (ddl_Nationality.SelectedIndex == 0) ? "" : ddl_Nationality.SelectedItem.ToString();
        ddcurrentrank2.Text = lblCurrRank.Text;
        txt_LastVessel2.Text = lblCurrentVessel.Text;
        txt_Status2.Text = txt_Status.Text;
        txt_FirstName2.Text = txt_FirstName.Text;
        txt_MiddleName2.Text = txt_MiddleName.Text;
        txt_LastName2.Text = txt_LastName.Text;
        txtPassport2.Text = txt_Passport.Text;

        //--------------------------BANK DETAILS
        GetCrewBankDetails(id);

        hdnCrewBankId.Value = "";
        ddlAccountType.SelectedIndex = 0;
        //txtACHFirstName.Text = "";
        //txtACHLastName.Text = "";
        txtACNo.Text = "";
        txtReAcNo.Text = "";
        txtBName.Text = "";
        txtBBName.Text = "";
        ddlBankCountry.SelectedIndex = 0;
        txtIFSCCode.Text = "";
        txtBSwiftCode.Text = "";
        txtBIBAN.Text = "";
        txtBBeneficiary.Text = "";
        txtBAddress.Text = "";
        chkActive.Checked = true;
        chkActive.Enabled = false;
        txtBBankCity.Text = "";
    }
    protected void GetCrewBankDetails(int crewId)
    {
        DataTable dt_Data = Common.Execute_Procedures_Select_ByQueryCMS("EXEC DBO.Get_CrewBankDetail " + crewId + "");
        if (dt_Data != null)
        {
            if (dt_Data.Rows.Count > 0)
            {
                GvBankDetails.DataSource = dt_Data;
                GvBankDetails.DataBind();
            }
            else
            {
                GvBankDetails.DataSource = null;
                GvBankDetails.DataBind();
                lbl_GVBankDetails.Text = "No Records Found..!";
            }
        }
        else
        {
            GvBankDetails.DataSource = null;
            GvBankDetails.DataBind();
            lbl_GVBankDetails.Text = "No Records Found..!";
        }
    }
    private void BindBankTypeOfRemittanceDropDown()
    {
        FieldInfo[] thisEnumFields = typeof(TypeOfRemittance).GetFields();
        foreach (FieldInfo thisField in thisEnumFields)
        {
            if (!thisField.IsSpecialName && thisField.Name.ToLower() != "notset")
            {
                int thisValue = (int)thisField.GetValue(0);
                ddlBTypeOfRemitteance.Items.Add(new ListItem(thisField.Name, thisValue.ToString()));
            }
        }
    }

    protected void GvBankDetails_PreRender(object sender, EventArgs e)
    {
        if (GvBankDetails.Rows.Count <= 0) { lbl_GVBankDetails.Text = "No Records Found..!"; }
    }

    protected void GvBankDetails_SelectIndexChanged(object sender, EventArgs e)
    {
        HiddenField hfd;
        hfd = (HiddenField)GvBankDetails.Rows[GvBankDetails.SelectedIndex].FindControl("hdnCrewBankId");
        ShowCrewBankDetailsbyId(Convert.ToInt32(hfd.Value.Trim()));
        btnAddBankInfo.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        PnlBankDetails.Visible = true;
        btnCancelBankInfo.Visible = true;
        btnSaveBankInfo.Visible = false;
    }
    protected void GvBankDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    protected void GvBankDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int Rowindx = row.RowIndex;
            HiddenField hfd;
            hfd = (HiddenField)GvBankDetails.Rows[Rowindx].FindControl("hdnCrewBankId");
            ShowCrewBankDetailsbyId(Convert.ToInt32(hfd.Value.Trim()));
            GvBankDetails.SelectedIndex = Rowindx;
            //Activate_Experience();
            btnSaveBankInfo.Visible = true;
            btnCancelBankInfo.Visible = true;
        }
    }

    protected void GvBankDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        HiddenField hfd;    
        hfd = (HiddenField)GvBankDetails.Rows[e.RowIndex].FindControl("hdnCrewBankId");
        DataTable dt_Data = Common.Execute_Procedures_Select_ByQueryCMS("Update CrewBankDetails SET Status = 'D' where CrewBankId = " + Convert.ToInt32(hfd.Value.ToString()) + "");
        BindBankDetailsGrid(Convert.ToInt32(HiddenPK.Value.Trim()));
    }

    protected void ShowCrewBankDetailsbyId(int CrewBankId)
    {
        DataTable dt_Data = Common.Execute_Procedures_Select_ByQueryCMS("EXEC DBO.Get_CrewBankDetailbyId " + CrewBankId + "");
        if (dt_Data != null)
        {
            if (dt_Data.Rows.Count > 0)
            {
                DataRow dr = dt_Data.Rows[0];
                String AcNo = ProjectCommon.Decrypt(dr["BankAccountNumber"].ToString().Trim(), "qwerty1235");
                ddlAccountType.SelectedValue = dr["AccountType"].ToString();
                ddlBTypeOfRemitteance.SelectedValue = dr["TypeOfRemittanceId"].ToString();
                ddlBankCountry.SelectedValue = dr["BankCountryId"].ToString();
                //txtACHFirstName.Text = dr["ACHFirstName"].ToString();
                //txtACHLastName.Text = dr["ACHLastName"].ToString();
                txtACNo.Text = AcNo;
                txtReAcNo.Text = AcNo;
                //txtACNo.Text = AcNo;
                //txtACNo.Attributes.Add("value", txtACNo.Text);
                //txtReAcNo.Text = AcNo;
                //txtReAcNo.Attributes.Add("value", txtReAcNo.Text);
                txtswiftcode.Text = dr["SwiftCode"].ToString();
                txtIFSCCode.Text = dr["IFSCCode"].ToString();
                txtBIBAN.Text = dr["IBANNumber"].ToString();
                txtBName.Text = dr["BankName"].ToString();
                txtBBName.Text = dr["BankBranchName"].ToString();
                txtBBeneficiary.Text = dr["Beneficiary"].ToString();
                txtBBankCity.Text = dr["BankCity"].ToString();
                txtBAddress.Text = dr["BankAddress"].ToString();
                if (Convert.ToInt32(dr["IsActive"]) == 1)
                {
                    chkActive.Checked = true; 
                }
                else
                {
                    chkActive.Checked = false;
                }
                if (dr["AccountType"].ToString() == "P")
                {
                    chkActive.Enabled = false;
                }
                else
                {
                    chkActive.Enabled = true;
                }
            }
            else
            {
                GvBankDetails.DataSource = null;
                GvBankDetails.DataBind();
                lbl_GVBankDetails.Text = "No Records Found..!";
            }
        }
        else
        {
            GvBankDetails.DataSource = null;
            GvBankDetails.DataBind();
            lbl_GVBankDetails.Text = "No Records Found..!";
        }
    }

    protected void ddlAccountType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAccountType.SelectedValue != "P")
        {
            chkActive.Enabled = true;
        }
        else
        {
            chkActive.Enabled = false;
            chkActive.Checked = true;
        }
    }

    protected void btnBankInfoEdit_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = (GridViewRow)(((ImageButton)sender).NamingContainer);
        int Rowindx = row.RowIndex;
        HiddenField hfdBankDetails;

        hfdBankDetails = (HiddenField)GvBankDetails.Rows[Rowindx].FindControl("hdnCrewBankId");
        ShowCrewBankDetailsbyId(Convert.ToInt32(hfdBankDetails.Value.Trim()));

        PnlBankDetails.Visible = true;
        GvBankDetails.SelectedIndex = Rowindx;
        btnCancelBankInfo.Visible = true;
        btnSaveBankInfo.Visible = true;
        btnAddBankInfo.Visible = false;
    }

    protected void GvBankDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string AcNo = string.Empty;
            AcNo = ProjectCommon.Decrypt(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "BankAccountNumber")), "qwerty1235");
            var result = new String('X', AcNo.Length - 4) + AcNo.Substring(AcNo.Length - 4);
            e.Row.Cells[6].Text = String.Format("{0:C}", result);
        }
        
    }

    protected void chkInsuCovered_CheckedChanged(object sender, EventArgs e)
    {
        if (chkInsuCovered.Checked)
        {
            txtInsuranceId.Enabled = true;
            txtInsuCompany.Enabled = true;
            
        }
        else
        {
            txtInsuranceId.Enabled = false;
            txtInsuCompany.Enabled = false;
          
        }
        txtInsuranceId.Text = "";
        txtInsuCompany.Text = "";
    }
}