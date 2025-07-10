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

public partial class CrewRecord_CrewDocument : System.Web.UI.Page
{
    Authority Auth;
    string Mode;
    //-------
    #region PAGE CONTROLS LOADER
    private void Load_Medical_Documents()
    {
        DataSet dt = cls_SearchReliever.getMasterData("MedicalDocuments", "MedicalDocumentId", "MedicalDocumentName");
        this.ddl_MedicalDocuments.DataTextField = "MedicalDocumentName";
        this.ddl_MedicalDocuments.DataValueField = "MedicalDocumentId";
        this.ddl_MedicalDocuments.DataSource = dt;
        this.ddl_MedicalDocuments.DataBind();
        ddl_MedicalDocuments.Items.Insert(0, new ListItem("< Select >","0")); 
    }

    private void Load_Training()
    {
        DataSet dt = cls_SearchReliever.getMasterData("Training", "TrainingId", "TrainingName");
        this.ddlTraining.DataTextField = "TrainingName";
        this.ddlTraining.DataValueField = "TrainingId";
        this.ddlTraining.DataSource = dt;
        this.ddlTraining.DataBind();
        ddlTraining.Items.Insert(0, new ListItem("< Select >", "0"));
    }

    private void Load_TrainingInstitute()
    {
        DataSet dt = cls_SearchReliever.getMasterData("TrainingInstitute", "InstituteId", "InstituteName");
        this.ddlInstitute.DataTextField = "InstituteName";
        this.ddlInstitute.DataValueField = "InstituteId";
        this.ddlInstitute.DataSource = dt;
        this.ddlInstitute.DataBind();
        ddlInstitute.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    private void getdata()
    {
        ProcessGetVessel getvessel = new ProcessGetVessel();
        ProcessGetPort getport = new ProcessGetPort();

        try
        {
            getvessel.Invoke();
            getport.Invoke();
        }
        catch (Exception se)
        {
            Response.Write(se.Message.ToString());
        }
        //ddmedicalvessel.DataTextField = "VesselName";
        //ddmedicalvessel.DataValueField = "VesselId";
        //ddmedicalvessel.DataSource = getvessel.ResultSet;
        //ddmedicalvessel.DataBind();

        //ddmedicalport.DataTextField = "PortName";
        //ddmedicalport.DataValueField = "PortId";
        //ddmedicalport.DataSource = getport.ResultSet;
        //ddmedicalport.DataBind();
    }
    //protected void bindVesselNameddl()
    //{
    //    //DataSet ds = cls_SearchReliever.getMasterData("Vessel", "VesselId", "VesselName");
    //    DataSet ds = SearchSignOff.getVessel(Convert.ToInt32(Session["loginid"].ToString()));
    //    ddmedicalvessel.DataSource = ds.Tables[0];
    //    ddmedicalvessel.DataValueField = "VesselId";
    //    ddmedicalvessel.DataTextField = "Name";
    //    ddmedicalvessel.DataBind();
    //    ddmedicalvessel.Items.Insert(0, new ListItem("< Select >", "0"));
    //}
    //private void bindcountrynameddl()
    //{
    //    DataTable dt3 = PortPlanner.selectCountryName();
    //    this.ddlCountry.DataValueField = "CountryId";
    //    this.ddlCountry.DataTextField = "CountryName";
    //    this.ddlCountry.DataSource = dt3;
    //    this.ddlCountry.DataBind();
    //}
    //private void BindPortDropDown()
    //{
    //    DataTable dt12 = PortAgent.selectPortName(Convert.ToInt32(ddlCountry.SelectedValue));
    //    this.ddmedicalport.DataValueField = "PortId";
    //    this.ddmedicalport.DataTextField = "PortName";
    //    this.ddmedicalport.DataSource = dt12;
    //    this.ddmedicalport.DataBind();
    //}
    private void getotherdoc()
    {
        CrewOtherDocumentsDetails crewotherdetails = new CrewOtherDocumentsDetails();
        crewotherdetails.CrewId = Convert.ToInt32(HiddenPK.Value);

        ProcessGetOtherDocumentsByCrewId crewotherdoc = new ProcessGetOtherDocumentsByCrewId();
        crewotherdoc.OtherDetails = crewotherdetails;
        try
        {
            crewotherdoc.Invoke();

        }
        catch (Exception se)
        {
            Response.Write(se.Message.ToString());
        }
        this.ddl_otherdoc_coursename.DataTextField = "DocumentName";
        this.ddl_otherdoc_coursename.DataValueField = "OtherDocumentId";
        this.ddl_otherdoc_coursename.DataSource = crewotherdoc.ResultSet;
        this.ddl_otherdoc_coursename.DataBind();

        this.dd_otherdoc_courseex.DataValueField = "Expires";
        this.dd_otherdoc_courseex.DataValueField = "OtherDocumentId";
        this.dd_otherdoc_courseex.DataSource = crewotherdoc.ResultSet;
        dd_otherdoc_courseex.DataBind();
    }
    private void BindCargonameDropDown()
    {
        ProcessSelectCargoName SelectCargoName = new ProcessSelectCargoName();
        SelectCargoName.Invoke();
        ddcargoname.DataValueField = "CargoId";
        ddcargoname.DataTextField = "CargoName";
        ddcargoname.DataSource = SelectCargoName.ResultSet;
        ddcargoname.DataBind();
    }
    private void BindFlagStateDropDown()
    {
        ProcessSelectFlagState SelectCargoName = new ProcessSelectFlagState();
        SelectCargoName.Invoke();
        ddl_Flag.DataValueField = "CountryId";
        ddl_Flag.DataTextField = "CountryName";
        ddl_Flag.DataSource = SelectCargoName.ResultSet;
        ddl_Flag.DataBind();
    }
    private void BindLicenseNationalityDropDown()
    {
        ProcessSelectNationality processSelectNationality = new ProcessSelectNationality();
        processSelectNationality.Invoke();
        ddl_License_country.DataValueField = "CountryId";
        ddl_License_country.DataTextField = "CountryName";
        ddl_License_country.DataSource = processSelectNationality.ResultSet;
        ddl_License_country.DataBind();
    }
    private void BindcargoNationalityDropDown()
    {
        ProcessSelectNationality processSelectNationality = new ProcessSelectNationality();
        processSelectNationality.Invoke();
        ddDCEnationality.DataValueField = "CountryId";
        ddDCEnationality.DataTextField = "CountryName";
        ddDCEnationality.DataSource = processSelectNationality.ResultSet;
        ddDCEnationality.DataBind();
    }
    private void getcertificatedata()
    {
        CrewCourseCertificateDetails crewCourseCertificateDetails = new CrewCourseCertificateDetails();
        crewCourseCertificateDetails.CrewId = Convert.ToInt32(Convert.ToInt32(HiddenPK.Value));
        ProcessGetCrewMemberCourses crewmembercourse = new ProcessGetCrewMemberCourses();
        crewmembercourse.CrewCourseCertificateDetails = crewCourseCertificateDetails;


        try
        {
           crewmembercourse.Invoke();

        }
        catch (Exception se)
        {
            Response.Write(se.Message.ToString());
        }
        this.ddl_certificate_course.DataTextField = "CourseName";
        this.ddl_certificate_course.DataValueField = "CourseCertificateId";
        this.ddl_certificate_course.DataSource = crewmembercourse.ResultSet;
        this.ddl_certificate_course.DataBind();

        this.ddcouseex.DataTextField = "Expires";
        this.ddcouseex.DataValueField = "CourseCertificateId";
        this.ddcouseex.DataSource = crewmembercourse.ResultSet.Tables[0];
        this.ddcouseex.DataBind();


    }
    private void getLicensedata()
    {
        CrewLicenseDetails crewlicencedetails = new CrewLicenseDetails();
        crewlicencedetails.CrewId = Convert.ToInt32(HiddenPK.Value);
        ProcessGetCrewLicence crewlicense = new ProcessGetCrewLicence();
        crewlicense.LicenseDetails = crewlicencedetails;


        try
        {
            crewlicense.Invoke();

        }
        catch (Exception se)
        {
            Response.Write(se.Message.ToString());
        }
        this.dd_License_licensename.DataTextField = "LicenseName";
        this.dd_License_licensename.DataValueField = "LicenseId";
        this.dd_License_licensename.DataSource = crewlicense.ResultSet;
        this.dd_License_licensename.DataBind();

        this.dd_license_licenseex.DataTextField = "Expires";
        this.dd_license_licenseex.DataValueField = "LicenseId";
        this.dd_license_licenseex.DataSource = crewlicense.ResultSet;
        this.dd_license_licenseex.DataBind();


    } 
    #endregion
    // General Code
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Alerts.SetHelp(imgHelp);   
        this.img_Crew.Attributes.Add("onclick", "javascript:Show_Image_Large(this);");
        this.img_Travel.Attributes.Add("onclick", "javascript:Show_Image_Large(this);");
        this.img_License.Attributes.Add("onclick", "javascript:Show_Image_Large(this);");
        this.img_cerificate.Attributes.Add("onclick", "javascript:Show_Image_Large(this);");

        this.Imagecargo.Attributes.Add("onclick", "javascript:Show_Image_Large(this);");
        this.img_otherdoc.Attributes.Add("onclick", "javascript:Show_Image_Large(this);");
        this.img_Medical.Attributes.Add("onclick", "javascript:Show_Image_Large(this);");
        this.Img_Training.Attributes.Add("onclick", "javascript:Show_Image_Large(this);");
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

        try
        {
            Mode = Session["Mode"].ToString();
            //***********Code to check page acessing Permission
            int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
            if (chpageauth <= 0)
            {
                Response.Redirect("../AuthorityError.aspx");

            }
            //*******************

            ProcessCheckAuthority Obj = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
            Obj.Invoke();
            Session["Authority"] = Obj.Authority;
            Auth = (Authority)Session["Authority"];
        }
        catch { Response.Redirect("CrewSearch.aspx"); }
        lbl_GridView_Academic.Text = "";
        lbl_GridView_Medical.Text = "";
        lbl_gvDocument.Text = "";
        lbl_GridView_cargo.Text = ""; 
        lbl_message.Text = "";
        lblMessage.Text = "";
        lbl_Grid_Training.Text = "";
        //=========== Code for Checking EOC Appraisal Alert

        DataSet ds = Budget.getTable("select CrewNumber,Firstname+ '' +  MiddleName + ' ' + LastName as FullName,  " + 
                                    "(select vesselname from vessel where vessel.vesselid=cpd.lastvesselid) as Vessel, " + 
                                    "(select top 1 ContractReferenceNumber from crewcontractheader cch where cch.crewid=cpd.crewid And Status='A' order by contractid desc) as RefNumber, " +
                                    "convert(Varchar,SignOffDate,101) as SignOffDate,crewstatusid " +
                                    "from crewpersonaldetails cpd where cpd.crewid=" + Session["CrewId"].ToString()  + " and cpd.signoffdate is not null and isnull(lastvesselid,0) >0 and not exists " +
                                    "(select top 1 crewid from crewappraisaldetails cad where cad.AppraisalOccasionId=10 and cad.createdon between cpd.signoffdate and dateadd(day,8,cpd.signoffdate)) ");
        DataTable  dt = ds.Tables[0];
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["crewstatusid"].ToString().Trim() == "5" || dt.Rows[0]["crewstatusid"].ToString().Trim() == "10") // Inactive OR NTBR
                lnkAppraisal.Visible = false;
        }
        else
        {
            lnkAppraisal.Visible = false; 
        }
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
        if (!(IsPostBack))
        {
            try
            {
                HiddenPK.Value = Session["CrewId"].ToString();
                if (Session["CrewId"].ToString().Trim() == "") { Response.Redirect("CrewSearch.aspx"); }
            }
            catch
            {
                Response.Redirect("CrewSearch.aspx");
            }
            //bindVesselNameddl();
            Load_Medical_Documents();
            //bindcountrynameddl();
            //ddlCountry_SelectedIndexChanged(sender,e);
            getdata();
            getotherdoc();
            BindFlagStateDropDown();
            getLicensedata();
            getapprasial();
            Load_Vesssel();
            //bindMedicalDocumentTypeDDL();
            getcertificatedata();
            BindCargonameDropDown();
            BindLicenseNationalityDropDown();
            BindcargoNationalityDropDown();
            BindMedicalDetailsGrid(Convert.ToInt32(HiddenPK.Value));
            Load_Training();
            Load_TrainingInstitute();
            HANDLE_AUTHORITY(0);
            RadioButtonList2.SelectedIndex = 0;
            bindDocumentGrid(Convert.ToInt32(HiddenPK.Value));
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
            txt_LastVessel.Text = Crew.CurrentVessel;
            txt_Status.Text = Crew.CrewStatus;
            txt_Passport.Text = Crew.PassportNo;
            lblName.Text = txt_FirstName.Text + " " + txt_MiddleName.Text + " " + txt_LastName.Text;
            lblCurrRank.Text = ddcurrentrank.Text;
            lblCurrentVessel.Text = txt_LastVessel.Text;
            lblStatus.Text = Crew.Status;
            lblAge.Text = Crew.Age;
            lblRankExp.Text = Crew.RankExp;
            UtilityManager um = new UtilityManager();
            if (Crew.Photopath.Trim() == "")
            {
                img_Crew.ImageUrl = um.DownloadFileFromServer("noimage.jpg", "C");
            }
            else
            {
                img_Crew.ImageUrl = um.DownloadFileFromServer(Crew.Photopath, "C");
            }
            Session["CrewDoc"] = "0";
        }
        b1.CssClass = "btn1";
        b2.CssClass = "btn1";
        b3.CssClass = "btn1";
        b4.CssClass = "btn1";
        b5.CssClass = "btn1";
        b8.CssClass = "btn1";
        b9.CssClass = "btn1";
        if (Session["CrewDoc"] == null)
        {
            Session["CrewDoc"] = 0;
        }
        switch (Common.CastAsInt32(Session["CrewDoc"]))
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
                b5.CssClass = "selbtn";
                break;
            case 5:
                b8.CssClass = "selbtn";
                break;
            case 6:
                b9.CssClass = "selbtn";
                break;
            default:
                break;
        }
    }
    protected void Menu1_MenuItemClick(object sender, EventArgs e)
    {
        b1.CssClass = "btn1";
        b2.CssClass = "btn1";
        b3.CssClass = "btn1";
        b4.CssClass = "btn1";
        b5.CssClass = "btn1";
        b8.CssClass = "btn1";
        b9.CssClass = "btn1";
        //b6.CssClass = "btn1";
        //b7.CssClass = "btn1";
        Button btn = (Button)sender;
        int i = Common.CastAsInt32(btn.CommandArgument);
        Session["CrewDoc"] = i;
        
        //for (; i < Menu1.Items.Count; i++)
        //{
        //    this.Menu1.Items[i].ImageUrl = this.Menu1.Items[i].ImageUrl.Replace("_a.gif", "_d.gif");
        //}
        //this.Menu1.Items[Int32.Parse(e.Item.Value)].ImageUrl = this.Menu1.Items[Int32.Parse(e.Item.Value)].ImageUrl.Replace("_d.gif", "_a.gif");
        if (i == 0)
        {
            this.MultiView1.ActiveViewIndex = i;
            b1.CssClass = "selbtn";
            //***********Code to check page acessing Permission
            int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
            if (chpageauth <= 0)
            {
                Response.Redirect("../AuthorityError.aspx");

            }
            //*******************
            HANDLE_AUTHORITY(0);
        }
        if (i == 1)
        {
            this.MultiView1.ActiveViewIndex = i;
            b2.CssClass = "selbtn";
            //***********Code to check page acessing Permission
            int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
            if (chpageauth <= 0)
            {
                Response.Redirect("../AuthorityError.aspx");

            }
            //*******************

              HANDLE_AUTHORITY(1);
              RadioButtonList1.SelectedIndex = 0; 
        }
        if (i == 2)
        {
            this.MultiView1.ActiveViewIndex = i;
            b3.CssClass = "selbtn";
            //***********Code to check page acessing Permission
            int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
            if (chpageauth <= 0)
            {
                Response.Redirect("../AuthorityError.aspx");

            }
            //*******************
            BindAcademicDetailsGrid(Convert.ToInt32(HiddenPK.Value));
            HANDLE_AUTHORITY(2);
        }
        if (i == 3)
        {
            this.MultiView1.ActiveViewIndex = i;
            b4.CssClass = "selbtn";
            //***********Code to check page acessing Permission
            int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
            if (chpageauth <= 0)
            {
                Response.Redirect("../AuthorityError.aspx");

            }
            //*******************
            HANDLE_AUTHORITY(3);
        }
        if (i == 4)
        {
            this.MultiView1.ActiveViewIndex = i;
            b5.CssClass = "selbtn";
            //***********Code to check page acessing Permission
            int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
            if (chpageauth <= 0)
            {
                Response.Redirect("../AuthorityError.aspx");

            }
            //*******************
            HANDLE_AUTHORITY(4);
        }
        if (i == 5)
        {
            this.MultiView1.ActiveViewIndex = i;
            b8.CssClass = "selbtn";
            //***********Code to check page acessing Permission
            int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
            if (chpageauth <= 0)
            {
                Response.Redirect("../AuthorityError.aspx");

            }
            //*******************
            HANDLE_AUTHORITY(5);
        }
        if (i == 6)
        {
            this.MultiView1.ActiveViewIndex = i;
            b9.CssClass = "selbtn";
            //***********Code to check page acessing Permission
            int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
            if (chpageauth <= 0)
            {
                Response.Redirect("../AuthorityError.aspx");

            }
            //*******************
            HANDLE_AUTHORITY(6);
        }

    }
    protected void Manage_Controls()
    {


        lbl_DocNo.Text = ((RadioButtonList2.SelectedIndex == 0) ? "Passport #:" : ((RadioButtonList2.SelectedIndex == 1) ? "Visa #:" : (RadioButtonList2.SelectedIndex == 2) ? "Seaman Book #:" : (RadioButtonList2.SelectedIndex == 3) ?  "INDOS Certificate #:"   : (RadioButtonList2.SelectedIndex == 4) ? "SID #:" : "Registration #:"));
        //lbl_VisaName.Text = ((RadioButtonList2.SelectedIndex == 1) ? "Visa Name:" : "Seaman Book Name:");
    }
    private void HANDLE_AUTHORITY(int index)
    {
        btn_Print.Visible = Auth.isPrint;
        btn_cargo_Print.Visible = Auth.isPrint;
        btn_Certificate_Print.Visible = Auth.isPrint;
        btn_License_Print.Visible = Auth.isPrint;
        btn_Otherdoc_Print.Visible = Auth.isPrint;
        //btn_PCI_Print.Visible = Auth.isPrint;
        btn_Medical_Print.Visible = Auth.isPrint;
        btn_Print_Academic.Visible = Auth.isPrint;
        btn_Apprasial_Print.Visible = Auth.isPrint;
        if (Mode == "New")
        {
            // PAGE LEVEL BUTTONS
            
            //TAB LEVEL BUTTONS

            //TAB-1(TRAVEL DETAILS)
            if (index == 0) 
            {
                btn_Add.Visible = ((Auth.isAdd || Auth.isEdit) && (Mode == "Edit" || Mode == "New"));
                btn_Save.Visible = false;
                btn_Cancel.Visible = false;
                pnl_Travel.Visible = false;
                RadioButtonList2.SelectedIndex = 0;
                RadioButtonList2_SelectedIndexChanged(RadioButtonList1, new EventArgs());
                Manage_Controls();
                if (RadioButtonList2.SelectedIndex == 0)
                {
                    btn_Add.Visible = false;
                }
            }

            //TAB-2 PROFESSIONAL DETAILS

            if (index == 1) 
            {
                pnl_Professional_1.Visible = true;
                pnl_Professional_2.Visible = false;
                pnl_Professional_3.Visible = false;
                pnl_Professional_4.Visible = false;
                chk_Verified.Enabled = Auth.isVerify;
                bindLicensedata(Convert.ToInt32(HiddenPK.Value));
                btn_License_cancel.Visible = false;
                btn_License_Save.Visible = false;
                btn_License_Add.Visible = ((Auth.isAdd || Auth.isEdit) && (Mode == "Edit" || Mode == "New"));
                trLicensefields.Visible = false;

            }

            //TAB-3 ACADEMIC DETAILS
            if (index == 2)
            {
                btn_Add_Academic.Visible = Auth.isEdit || Auth.isAdd;
                btn_Save_Academic.Visible = false;
                btn_Cancel_Academic.Visible = false;
                pnl_Academic_Details.Visible = false;
            }
            //TAB-4 MEDICAL DETAILS
            if (index == 3)
            {
                pnl_Medical_1.Visible = true;
                //pnl_Medical_2.Visible = false;
               // RadioButtonList3.SelectedIndex = 0;
                btn_Medical_Cancel.Visible = false;
                btn_Medical_Save.Visible = false;
                pnl_Medical_Details.Visible = false;
                btn_Medical_Add.Visible = (Auth.isAdd || Auth.isEdit) && (Mode == "Edit" || Mode == "New");
                BindMedicalDetailsGrid(Convert.ToInt32(HiddenPK.Value));

            }
            if (index == 4)
            {
                grdDocs.Visible = true;
                BindArchivedDocuments();
            }
            // TAB-6 Appriasal Documents
            if (index == 5)
            {
                this.btn_Apprasial_Save.Visible = false;
                this.btn_Apprasial_Cancel.Visible = false;
                this.btn_Apprasial_Add.Visible = ((Auth.isEdit || Auth.isAdd) && (Mode == "New" || Mode == "Edit"));
                this.trapprasial.Visible = false;
                bindapprasialdata(Convert.ToInt16(HiddenPK.Value));
            }

            // TAB Training Document
           
            if (index == 6)
            {
                pnl_Training_1.Visible = true;
                //pnl_Medical_2.Visible = false;
                // RadioButtonList3.SelectedIndex = 0;
                btnTrainingCancel.Visible = false;
                btnTrainingSave.Visible = false;
                pnl_Training_Details.Visible = false;
                btnTrainingAdd.Visible = (Auth.isAdd || Auth.isEdit) && (Mode == "Edit" || Mode == "New");
                BindTrainingDetailsGrid(Convert.ToInt32(HiddenPK.Value));

            }
        }
        else if (Mode == "Edit")
        {
            // PAGE LEVEL BUTTONS
            
            //TAB LEVEL BUTTONS

            //TAB-1(TRAVEL DETAILS)
            if (index == 0)
            {
                btn_Add.Visible = ((Auth.isAdd || Auth.isEdit) && (Mode == "Edit" || Mode == "New"));
                btn_Save.Visible = false;
                btn_Cancel.Visible = false;
                pnl_Travel.Visible = false;
                RadioButtonList2.SelectedIndex = 0;
                RadioButtonList2_SelectedIndexChanged(RadioButtonList1,new EventArgs());
                Manage_Controls();
                if (RadioButtonList2.SelectedIndex == 0)
                {
                    btn_Add.Visible = false;
                }
            }
            //TAB-2 PROFESSIONAL DETAILS

            if (index == 1)
            {
                pnl_Professional_1.Visible = true;
                pnl_Professional_2.Visible = false;
                pnl_Professional_3.Visible = false;
                pnl_Professional_4.Visible = false;
                chk_Verified.Enabled = Auth.isVerify;
                bindLicensedata(Convert.ToInt32(HiddenPK.Value));
                btn_License_cancel.Visible = false;
                btn_License_Save.Visible = false;
                btn_License_Add.Visible = ((Auth.isAdd || Auth.isEdit) && (Mode == "Edit" || Mode == "New"));
                trLicensefields.Visible = false;
            }

            //TAB-3 ACADEMIC DETAILS
            if (index == 2)
            {
                btn_Add_Academic.Visible = Auth.isEdit || Auth.isAdd;
                btn_Save_Academic.Visible = false;
                btn_Cancel_Academic.Visible = false;
                pnl_Academic_Details.Visible = false;
            }
            //TAB-4 MEDICAL DETAILS
            if (index == 3)
            {
                pnl_Medical_1.Visible = true;
                //pnl_Medical_2.Visible = false;
                //RadioButtonList3.SelectedIndex = 0;
                btn_Medical_Cancel.Visible = false;
                btn_Medical_Save.Visible = false;
                pnl_Medical_Details.Visible = false;
                btn_Medical_Add.Visible = (Auth.isAdd || Auth.isEdit) && (Mode == "Edit" || Mode == "New");
                BindMedicalDetailsGrid(Convert.ToInt32(HiddenPK.Value));

            }
            if (index == 4)
            {
                grdDocs.Visible = true;
                BindArchivedDocuments();
            }
            if (index == 5)
            {
                this.btn_Apprasial_Save.Visible = false;
                this.btn_Apprasial_Cancel.Visible = false;
                this.btn_Apprasial_Add.Visible = ((Auth.isEdit || Auth.isAdd) && (Mode == "New" || Mode == "Edit"));
                this.trapprasial.Visible = false;
                bindapprasialdata(Convert.ToInt16(HiddenPK.Value));
            }

            // TAB Training Document

            if (index == 6)
            {
                pnl_Training_1.Visible = true;
                //pnl_Medical_2.Visible = false;
                // RadioButtonList3.SelectedIndex = 0;
                btnTrainingCancel.Visible = false;
                btnTrainingSave.Visible = false;
                pnl_Training_Details.Visible = false;
                btnTrainingAdd.Visible = (Auth.isAdd || Auth.isEdit) && (Mode == "Edit" || Mode == "New");
                BindTrainingDetailsGrid(Convert.ToInt32(HiddenPK.Value));

            }

        }
        else // Mode=View
        {
            // PAGE LEVEL BUTTONS
            
            //TAB LEVEL BUTTONS

            //TAB-1(TRAVEL DETAILS)
            if (index == 0)
            {
                btn_Add.Visible = false;
                btn_Save.Visible = false;
                btn_Cancel.Visible = false;
                pnl_Travel.Visible = false;
                RadioButtonList2.SelectedIndex = 0;
                RadioButtonList2_SelectedIndexChanged(RadioButtonList1, new EventArgs());
                Manage_Controls();
                if (RadioButtonList2.SelectedIndex == 0)
                {
                    btn_Add.Visible = false;
                }
            }

            //TAB-2 PROFESSIONAL DETAILS

            if (index == 1)
            {
                pnl_Professional_1.Visible = true;
                pnl_Professional_2.Visible = false;
                pnl_Professional_3.Visible = false;
                pnl_Professional_4.Visible = false;
                chk_Verified.Enabled = Auth.isVerify;
                bindLicensedata(Convert.ToInt32(HiddenPK.Value));
                btn_License_cancel.Visible = false;
                btn_License_Save.Visible = false;
                btn_License_Add.Visible = false ;
                trLicensefields.Visible = false;
                
            }

            //TAB-3 ACADEMIC DETAILS
            if (index == 2)
            {
                btn_Add_Academic.Visible = false;
                btn_Save_Academic.Visible = false;
                btn_Cancel_Academic.Visible = false;
                pnl_Academic_Details.Visible = false;
            }
            //TAB-4 MEDICAL DETAILS
            if (index == 3)
            {
                pnl_Medical_1.Visible = true;
                //pnl_Medical_2.Visible = false;
                //RadioButtonList3.SelectedIndex = 0;
                btn_Medical_Cancel.Visible = false;
                btn_Medical_Save.Visible = false;
                pnl_Medical_Details.Visible = false;
                btn_Medical_Add.Visible = (Auth.isAdd || Auth.isEdit) && (Mode == "Edit" || Mode == "New");
                BindMedicalDetailsGrid(Convert.ToInt32(HiddenPK.Value));

            }

            if (index == 4)
            {
                grdDocs.Visible = true;
                BindArchivedDocuments();
            }

            // TAB Training Document

            if (index == 6)
            {
                pnl_Training_1.Visible = true;
                //pnl_Medical_2.Visible = false;
                // RadioButtonList3.SelectedIndex = 0;
                btnTrainingCancel.Visible = false;
                btnTrainingSave.Visible = false;
                pnl_Training_Details.Visible = false;
                btnTrainingAdd.Visible = (Auth.isAdd || Auth.isEdit) && (Mode == "Edit" || Mode == "New");
                BindTrainingDetailsGrid(Convert.ToInt32(HiddenPK.Value));
            }
        }
    }
    #region Activation & Deactivation
    private void Activate_All()
    {
        Activate_Professional();
        Activate_Academic();
        Activate_Medical();
    }
    private void Deactivate_All()
    {
        DeActivate_Professional();
        Deactivate_Academic();
        Deactivate_Medical();
        gvDocument.Columns[1].Visible = false;
        gvDocument.Columns[2].Visible = false;
    }
    //protected void gvDocuments_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    gvDocument.PageIndex = e.NewPageIndex;
    //    bindDocumentGrid();
    //}
    private void Activate_Professional()
    {

    }
    private void Activate_Academic()
    {

    }
    private void Activate_Medical()
    {

    }
    private void DeActivate_Professional()
    {

    }
    private void Deactivate_Academic()
    {

    }
    private void Deactivate_Medical()
    {

    }
    #endregion
    #region Tab-1 Travel Details
    public void bindDocumentGrid(int id)
    {
        ProcessSelectTravelDocumentDetailsById processselecttraveldetails = new ProcessSelectTravelDocumentDetailsById();
        try
        {
            TravelDocumentDetails obj = new TravelDocumentDetails();
            obj.CrewId = id;
            processselecttraveldetails.DocumentDetails = obj;
            obj.DocumentTypeId = RadioButtonList2.SelectedIndex;
            processselecttraveldetails.Invoke();
        }
        catch { }
        gvDocument.DataSource = processselecttraveldetails.ResultSet.Tables[0];
        gvDocument.Columns[4].Visible = (RadioButtonList2.SelectedIndex != 0);
        gvDocument.Columns[5].Visible = (RadioButtonList2.SelectedIndex == 1);
        gvDocument.Columns[10].Visible = (RadioButtonList2.SelectedIndex == 0);
        
        //gvDocument.Columns[5].HeaderText = (RadioButtonList2.SelectedIndex == 1) ? "Visa Name" : "Seaman Book Name";
        gvDocument.Columns[6].HeaderText = (RadioButtonList2.SelectedIndex == 0) ? "Passport #" : ((RadioButtonList2.SelectedIndex == 1) ? "Visa #" : (RadioButtonList2.SelectedIndex == 2) ? "Seaman Book #" : (RadioButtonList2.SelectedIndex == 3) ? "INDOS Certificate #" : (RadioButtonList2.SelectedIndex == 4) ? "SID #" : "Registration #");
        gvDocument.DataBind();
    }
    protected void ArchiveTravel(object sender, ImageClickEventArgs e)
    {
        
        int Pos=RadioButtonList2.SelectedIndex;
        
        string[,] DocsType = { 
                                {"Passport","~/EMANAGERBLOB/HRD/Documents/Travel/" },
                                {"Visa","~/EMANAGERBLOB/HRD/Documents/Travel/" },
                                {"SeamanBook","~/EMANAGERBLOB/HRD/Documents/Travel/" },
                                {"INDOSCertificate","~/EMANAGERBLOB/HRD/Documents/Travel/" },
                                {"SID","~/EMANAGERBLOB/HRD/Documents/Travel/" },
                                {"Registration","~/EMANAGERBLOB/HRD/Documents/Travel/" },
                             };
        
        int CrewId=Convert.ToInt32(Session["CrewId"].ToString());
        int LoginId=Convert.ToInt32(Session["loginid"].ToString());

        Control c=((ImageButton)sender).Parent;


        HiddenField hfd = (HiddenField)c.FindControl("HiddenId");
        string TravelDocId=hfd.Value;
                                                                     
        hfd=(HiddenField)c.FindControl("arc_DocumentNumber")   ;
        string DocumentNumber=hfd.Value;

        hfd=(HiddenField)c.FindControl("arc_IssueDate")   ;
        string IssueDate=hfd.Value;

        hfd=(HiddenField)c.FindControl("arc_ExpDate")   ;
        string ExpDate=hfd.Value;

        hfd = (HiddenField)c.FindControl("arc_FilePath");
        string FilePath=hfd.Value;

        string FullFilePath = Server.MapPath(DocsType[Pos,1]+ hfd.Value);

        byte[] Data = { };

        try
        {
            Data = System.IO.File.ReadAllBytes(FullFilePath);
        }
        catch { } 

        if (Alerts.ArchiveDocument(CrewId, "Travel", DocsType[Pos, 0],"", DocumentNumber, IssueDate.Trim(), ExpDate.Trim(), "", Data, LoginId))
        {
            Delete_Travel_Doc(TravelDocId); 
            lblMessage.Text = "Archived Successfully.";
        }
        else
        {
            lblMessage.Text = "Unable to Archive.";
        }
    }
    #region RECORD DISPLAY AREA
    protected void Show_Record(TravelDocumentDetails Document)
    {
        string Mess = "";
    
        HiddenTravel.Value = Document.TravelDocumentId.ToString();
        hfd_Image1.Value = Document.ImagePath.ToString(); 
        txtDocumentNo.Text = Document.DocumentNumber;
        txtIssueDate.Text =  Alerts.FormatDate(Document.IssueDate);
        txtExpiryDate.Text = Alerts.FormatDate(Document.ExpiryDate);
        txtPlaceofIssue.Text =Document.PlaceOfIssue;
        txt_VisaName.Text =Document.VisaName;
        chk_ECNR.Checked  = (Document.ECNR.Trim()=="Yes");

        //ddl_Flag.SelectedValue = Document.FlagStateId.ToString();
        Mess = Mess + Alerts.Set_DDL_Value(ddl_Flag, Document.FlagStateId.ToString(), "Country");

        pnl_Travel.Visible = true;
        UtilityManager um= new UtilityManager();
        if (Document.ImagePath.Trim() == "")
        {
            img_Travel.ImageUrl = um.DownloadFileFromServer("noimage.jpg", "T");
        }
        else
        {
            img_Travel.ImageUrl = um.DownloadFileFromServer(Document.ImagePath.Trim(), "T");
        }
        hfd_Image1.Value = Document.ImagePath.Trim();
        if (Mess.Length > 0)
        {
            this.lblMessage.Text = "The Status Of Following Has Been Changed To In-Active <br/>" + Mess.Substring(1);
        }
    }
    // VIEW THE RECORD
    protected void gvDocument_SelectedIndexChanged(object sender, EventArgs e)
    {
        HiddenField hfd,hfd11;
        TravelDocumentDetails documentdetails = new TravelDocumentDetails();
        ProcessSelectTravelDocumentDetailsById documentdetailsbyid = new ProcessSelectTravelDocumentDetailsById();
        hfd = (HiddenField)gvDocument.Rows[gvDocument.SelectedIndex].FindControl("HiddenId");
        hfd11 = (HiddenField)gvDocument.Rows[gvDocument.SelectedIndex].FindControl("Hiddenfd11");
        documentdetails.TravelDocumentId = Convert.ToInt32(hfd.Value.ToString());
        documentdetails.ImagePath = hfd11.Value.ToString();
        documentdetailsbyid.DocumentDetails = documentdetails;
        documentdetailsbyid.Invoke();
        pnl_Travel.Visible = true;
        Show_Record(documentdetailsbyid.DocumentDetails);
        btn_Add.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        btn_Save.Visible = false;
        btn_Cancel.Visible = true;
        if (RadioButtonList2.SelectedIndex == 0)
        {
            btn_Add.Visible = false;
        }
    }
    //EDIT THE RECORD
    //protected void Row_Editing(object sender, GridViewEditEventArgs e)
    //{
    //    HiddenField hfd, hfd11;
    //    TravelDocumentDetails documentdetails = new TravelDocumentDetails();
    //    ProcessSelectTravelDocumentDetailsById documentdetailsbyid = new ProcessSelectTravelDocumentDetailsById();
    //    hfd = (HiddenField)gvDocument.Rows[e.NewEditIndex].FindControl("HiddenId");
    //    hfd11 = (HiddenField)gvDocument.Rows[e.NewEditIndex].FindControl("Hiddenfd11");
    //    documentdetails.TravelDocumentId = Convert.ToInt32(hfd.Value.ToString());
    //    documentdetails.ImagePath = hfd11.Value.ToString();
    //    documentdetailsbyid.DocumentDetails = documentdetails;
    //    documentdetailsbyid.Invoke();
    //    pnl_Travel.Visible = true;
    //    Show_Record(documentdetailsbyid.DocumentDetails);
    //    btn_Save.Visible = true;
    //    btn_Add.Visible = (Auth.isAdd || Auth.isEdit) && (Mode=="Edit");
    //    btn_Cancel.Visible = true;
    //    if (RadioButtonList2.SelectedIndex == 0)
    //    {
    //        btn_Add.Visible = false;
    //    }
    //}
    // DELETE THE RECORD
    protected void Row_Deleting(object sender, GridViewDeleteEventArgs  e)
    {
        HiddenField hfd;
        hfd = (HiddenField)gvDocument.Rows[e.RowIndex].FindControl("HiddenId");
        Delete_Travel_Doc(hfd.Value);
    }
    protected void Delete_Travel_Doc(string TravelDocId)
    {
        TravelDocumentDetails documentdetails = new TravelDocumentDetails();
        ProcessDeleteCrewMemberTravelDetailsById traveldetailsbyid = new ProcessDeleteCrewMemberTravelDetailsById();

        documentdetails.TravelDocumentId = Convert.ToInt32(TravelDocId);
        traveldetailsbyid.DocumentDetails = documentdetails;
        traveldetailsbyid.Invoke();
        bindDocumentGrid(Convert.ToInt32(HiddenPK.Value));
        if (HiddenTravel.Value.ToString() == TravelDocId)
        {
            btn_Add_Click(new object(), new EventArgs());
        }
        if (RadioButtonList2.SelectedIndex == 0)
        {
            txt_Passport.Text = "";
        }
    }
    #endregion
    protected void DataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            gvDocument.Columns[1].Visible = false;
            gvDocument.Columns[2].Visible = false;
            // Can Add
            if (Auth.isAdd)
            {
            }

            // Can Modify
            if (Auth.isEdit)
            {
                gvDocument.Columns[1].Visible = ((Mode == "New") || (Mode == "Edit")) ? true : false;
            }

            // Can Delete
            if (Auth.isDelete)
            {
                gvDocument.Columns[2].Visible = ((Mode == "New") || (Mode == "Edit")) ? true : false;
            }

            // Can Print
            if (Auth.isPrint)
            {
            }
            Image img = ((Image)e.Row.FindControl("imgattach"));
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "ImagePath") == null)
                {
                    img.Visible = false;
                }
                else if (DataBinder.Eval(e.Row.DataItem, "ImagePath").ToString() == "")
                {
                    img.Visible = false;
                }
                else
                {
                    string appname = ConfigurationManager.AppSettings["AppName"].ToString();
                    img.Attributes.Add("onclick", "javascript:Show_Image_Large1('/"+ appname + "/EMANAGERBLOB/HRD/Documents/Travel/" + DataBinder.Eval(e.Row.DataItem, "ImagePath").ToString() + "');");
                    img.ToolTip = "Click to Preview";
                    img.Style.Add("cursor", "hand");  
                }
            }
        }
        catch
        {
            gvDocument.Columns[1].Visible = false;
            gvDocument.Columns[2].Visible = false;
        }
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        if (Mode == "New")
        {
           btn_Add.Visible = (Auth.isAdd || Auth.isEdit);
        }
        else if (Mode == "Edit")
        {
           btn_Add.Visible = (Auth.isAdd || Auth.isEdit);
        }
        btn_Cancel.Visible = false;
        pnl_Travel.Visible = false;
        btn_Save.Visible = false;
        gvDocument.SelectedIndex = -1;
        if (RadioButtonList2.SelectedIndex == 0)
        {
            btn_Add.Visible = false;
        }
    }
    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {
            pnl_Travel.Visible = false;
            btn_Cancel.Visible = false;
            
            bindDocumentGrid(Convert.ToInt32(HiddenPK.Value));

            lbl_VisaName.Visible=(RadioButtonList2.SelectedIndex ==1);
            txt_VisaName.Visible = (RadioButtonList2.SelectedIndex ==1);

            lbl_ECNR .Visible =(RadioButtonList2.SelectedIndex ==0);
            chk_ECNR.Visible = (RadioButtonList2.SelectedIndex == 0);

            lbl_FlagStateId.Visible = (RadioButtonList2.SelectedIndex !=0);
            ddl_Flag.Visible = (RadioButtonList2.SelectedIndex != 0);

            btn_Save.Visible = false;
            Manage_Controls();
            if (RadioButtonList2.SelectedIndex == 0)
            {
                btn_Add.Visible = false;
            }
            else
            {
                btn_Add.Visible = (Auth.isAdd || Auth.isEdit) && (Mode == "New" || Mode == "Edit");
            }
    }
    protected void btn_Add_Click(object sender, EventArgs e)
    {
        HiddenTravel.Value = "";
        txtDocumentNo.Text = "";
        txtExpiryDate.Text = "";
        txtIssueDate.Text = "";
        txtPlaceofIssue.Text = "";
        txt_VisaName.Text = "";
        chk_ECNR.Checked = false;
        ddl_Flag.SelectedIndex = 0;  
        ImageButton2.Visible = true;
        txtExpiryDate.Enabled = true;
        pnl_Travel.Visible = true;
        btn_Save.Visible = true;
        btn_Add.Visible = false;
        btn_Cancel.Visible = true;
        hfd_Image1.Value = "";
        UtilityManager um = new UtilityManager();
        img_Travel.ImageUrl = um.DownloadFileFromServer("noimage.jpg", "T");
        if (RadioButtonList2.SelectedIndex == 0)
        {
            btn_Add.Visible = false;
        }
    }
    protected void btn_Save_Click1(object sender, EventArgs e)
    {
        TravelDocumentDetails documentdetails = new TravelDocumentDetails();
        if (txtExpiryDate.Text.Trim() != "")
        {
            if (DateTime.Parse(txtIssueDate.Text) > DateTime.Parse(txtExpiryDate.Text))
            {
                lblMessage.Text = "Issue date must be less or equal to expiry date.";
                return;
            }
        }
        try
        {
            if (HiddenTravel.Value.ToString().Trim() == "")
            {
                documentdetails.CreatedBy = Convert.ToInt32(Session["loginid"].ToString());
            }
            else
            {
                documentdetails.TravelDocumentId = Convert.ToInt32(HiddenTravel.Value);
                documentdetails.ModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
            }
                documentdetails.CrewId =Convert.ToInt32(HiddenPK.Value);
                documentdetails.DocumentTypeId = RadioButtonList2.SelectedIndex;
                documentdetails.DocumentNumber = txtDocumentNo.Text;
                documentdetails.IssueDate = txtIssueDate.Text;
                documentdetails.ExpiryDate = txtExpiryDate.Text;
                documentdetails.PlaceOfIssue = txtPlaceofIssue.Text;
                documentdetails.VisaName = txt_VisaName.Text;
                documentdetails.ECNR = (chk_ECNR.Checked)?"Y":"N";
                documentdetails.FlagStateId = Convert.ToInt32(ddl_Flag.SelectedValue);  
                documentdetails.ImagePath = "";

                if (this.FileUpload_Travel != null && this.FileUpload_Travel.FileContent.Length > 0)
                {
                    HttpPostedFile file = FileUpload_Medical.PostedFile;
                    UtilityManager um = new UtilityManager();
                    string fileName;
                    fileName = um.UploadFileToServer(this.FileUpload_Travel.PostedFile, hfd_Image1.Value.Trim(), "T");
                    if (fileName.StartsWith("?"))
                    {
                        lblMessage.Text = fileName.Substring(1);
                        return;
                    }
                    documentdetails.ImagePath = fileName;
                }
                else
                {
                    documentdetails.ImagePath = hfd_Image1.Value; 
                }
            ProcessAddCrewMemberTravelDocumentDetails memberdocumentdetails = new ProcessAddCrewMemberTravelDocumentDetails();
            memberdocumentdetails.DocumentDetails = documentdetails;
            memberdocumentdetails.Invoke();
            bindDocumentGrid(Convert.ToInt32(HiddenPK.Value));
            if (RadioButtonList2.SelectedIndex == 0)
            {
                txt_Passport.Text = txtDocumentNo.Text;
            }
            btn_Add_Click(sender, e);
            lblMessage.Text = "Record Successfully Saved.";
        }
        catch (Exception se)
        {
            lblMessage.Text = "Record Not Saved.";
            //Response.Write(se.Message.ToString());
        }
        
    }
    protected void gvDocument_PreRender(object sender, EventArgs e)
    {
        if (gvDocument.Rows.Count <= 0) { lbl_gvDocument.Text = "No Records Found..!"; }
        HiddenField hfd;
        ImageButton imgbtn;
        int i;
        for (i = 0; i <= gvDocument.Rows.Count-1; i++)
        {
            hfd = (HiddenField)gvDocument.Rows[i].FindControl("hiddenAvalue");
            imgbtn = (ImageButton)gvDocument.Rows[i].FindControl("ibArchiveTravel");
            if (hfd != null)
            {
                int k = Convert.ToInt32(hfd.Value);
                if (k == 0)
                {
                    gvDocument.Rows[i].BackColor = System.Drawing.Color.FromName("#fcc2bc");
                    imgbtn.Enabled = true;
                }
                else
                {
                    imgbtn.Enabled = false;
                }
            } 
        }
    }
    #endregion
    #region Tab-2 Professional Details
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedIndex == 0)
        {
            bindLicensedata(Convert.ToInt32(HiddenPK.Value));
            pnl_Professional_1.Visible = true;
            pnl_Professional_2.Visible = false;
            pnl_Professional_3.Visible = false;
            pnl_Professional_4.Visible = false;

            btn_License_cancel.Visible = false;
            btn_License_Save.Visible = false;
            btn_License_Add.Visible = ((Auth.isAdd || Auth.isEdit) && (Mode == "Edit" || Mode == "New"));
            trLicensefields.Visible = false;
            
        }
        else if (RadioButtonList1.SelectedIndex == 1)
        {
            bindcertificatedata(Convert.ToInt32(HiddenPK.Value));
            pnl_Professional_1.Visible = false;
            pnl_Professional_2.Visible = true;
            pnl_Professional_3.Visible = false;
            pnl_Professional_4.Visible = false;

            btn_Certificate_Cancel.Visible = false;
            btn_Certifiacte_Save.Visible = false;
            btn_Certificate_Add.Visible =((Auth.isAdd || Auth.isEdit) && (Mode == "Edit" || Mode == "New"));
            trcertificatefields.Visible = false;
        }
        else if (RadioButtonList1.SelectedIndex == 2)
        {
            bindDCEGrid(Convert.ToInt32(HiddenPK.Value));
            pnl_Professional_1.Visible = false;
            pnl_Professional_2.Visible = false;
            pnl_Professional_3.Visible = true;
            pnl_Professional_4.Visible = false;
            btn_cargo_cancel.Visible = false;
            btn_cargo_save.Visible = false;
            btn_cargo_Add.Visible = ((Auth.isAdd || Auth.isEdit) && (Mode == "Edit" || Mode == "New"));
            panelDCE.Visible = false;

        }
        else
        {
            bindotherdocdata(Convert.ToInt32(HiddenPK.Value));
            pnl_Professional_1.Visible = false;
            pnl_Professional_2.Visible = false;
            pnl_Professional_3.Visible = false;
            pnl_Professional_4.Visible = true;

            btn_Otherdoc_Cancel.Visible = false;
            btn_Otherdoc_Save.Visible = false;
            btn_otherdoc_Add.Visible = ((Auth.isAdd || Auth.isEdit) && (Mode == "Edit" || Mode == "New"));
            trotherdoc.Visible = false;
        }
    }
    // Part-1(License)
    private void bindLicensedata(int i)
     {
        CrewLicenseDetails crewlicencedetails = new CrewLicenseDetails();
        crewlicencedetails.CrewId = i;

        ProcessCrewLicenseSelectData getlicensedetails = new ProcessCrewLicenseSelectData();
        getlicensedetails.LicenseDetails = crewlicencedetails;
        getlicensedetails.Invoke();
        this.gvLicense.DataSource = getlicensedetails.ResultSet;
        gvLicense.DataBind();
        gvLicense.SelectedIndex = -1;
    }
    #region RECORD DISPLAY AREA
    private void licenseshowdata(int i)
    {
        string Mess = "";

        HiddenField lblid = ((HiddenField)gvLicense.Rows[i].FindControl("HcourseId"));
        Mess = Mess + Alerts.Set_DDL_Value(dd_License_licensename, lblid.Value, "License");
        dd_License_licensename_SelectedIndexChanged(new object(), new EventArgs());
        this.txt_License_grade.Text = gvLicense.Rows[i].Cells[5].Text.Replace("&nbsp;", "").Replace("&amp;", "");
        this.txt_License_LicenseNo.Text = gvLicense.Rows[i].Cells[6].Text.Replace("&nbsp;","").Replace("&amp;","");
        this.txt_License_Issuedate.Text = Alerts.FormatDate(gvLicense.Rows[i].Cells[7]);
        this.txt_license_Expirydate.Text = Alerts.FormatDate(gvLicense.Rows[i].Cells[8]);
        this.txt_License_PlaceofIssue.Text = gvLicense.Rows[i].Cells[9].Text.Replace("&nbsp;", "").Replace("&amp;", "");
        HiddenField lblCountryId = ((HiddenField)gvLicense.Rows[i].FindControl("HcountryId"));
        
        Mess = Mess + Alerts.Set_DDL_Value(ddl_License_country, lblCountryId.Value, "Country(License)");
        
        HiddenField himgname = ((HiddenField)gvLicense.Rows[i].FindControl("himag"));
        this.h_License_File.Value = himgname.Value;

        himgname = ((HiddenField)gvLicense.Rows[i].FindControl("verified"));
        chk_Verified.Checked = (himgname.Value.Trim()=="Y") ;
        
        UtilityManager um = new UtilityManager();
        if (this.h_License_File.Value == "" || this.h_License_File.Value == null)
        {
            this.img_License.ImageUrl = um.DownloadFileFromServer("noimage.jpg", "P");
        }
        else
        {
            this.img_License.ImageUrl = um.DownloadFileFromServer(h_License_File.Value, "P");
        }
        licensesetenable();
        if (Mess.Length > 0)
        {
            this.lblMessage.Text = "The Status Of Following Has Been Changed To In-Active <br/>" + Mess.Substring(1);
        }
    }
    protected void gvLicense_SelectedIndexChanged(object sender, EventArgs e)
    {
        int i;
        i = gvLicense.SelectedIndex;
        licenseshowdata(i);
        this.trLicensefields.Visible = true;
        this.btn_License_Save.Visible = false;
        this.btn_License_cancel.Visible = true;
        this.btn_License_Add.Visible = ((Mode == "Edit") || (Mode == "New"));
        h_Licenceid.Value = gvLicense.DataKeys[i].Value.ToString();
    }
    //protected void gvLicense_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    this.trLicensefields.Visible = true;
    //    this.btn_License_Save.Visible = true;
    //    this.btn_License_cancel.Visible = true;
    //    licenseshowdata(e.NewEditIndex);
    //    this.btn_License_Add.Visible = ((Mode == "Edit") || (Mode == "New"));
    //    h_Licenceid.Value = gvLicense.DataKeys[e.NewEditIndex].Value.ToString();
    //}
    protected void gvLicense_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int LicenseId=Convert.ToInt32(this.gvLicense.DataKeys[e.RowIndex].Value.ToString());
        Delete_Other_Docs(0, LicenseId);
    }
    #endregion
    protected void btn_License_Add_Click(object sender, EventArgs e)
    {
        this.trLicensefields.Visible = true;
        this.btn_License_Save.Visible = true;
        this.btn_License_cancel.Visible = true;
        this.btn_License_Add.Visible = false;
        chk_Verified.Checked = false; 
        licensecleardata();

    }
    protected void btn_License_Save_Click(object sender, EventArgs e)
    {

        CrewLicenseDetails crewlicensedetails = new CrewLicenseDetails();
        if (txt_license_Expirydate.Text.Trim() != "")
        {
            if (DateTime.Parse(txt_License_Issuedate.Text) > DateTime.Parse(txt_license_Expirydate.Text))
            {
                lblMessage.Text = "Issue date must be less or equal to expiry date.";
                return;
            }
        }
        try
        {
            if (this.h_Licenceid.Value == "")
            {
                crewlicensedetails.CrewLicenseId = -1;
                crewlicensedetails.CreatedBy = Convert.ToInt32(Session["loginid"].ToString());
            }
            else
            {
                crewlicensedetails.CrewLicenseId = Convert.ToInt32(this.h_Licenceid.Value);
                crewlicensedetails.ModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
            }
            crewlicensedetails.CrewId = Convert.ToInt32(this.HiddenPK.Value);
            crewlicensedetails.LicenseId = Convert.ToInt32(this.dd_License_licensename.SelectedValue);
            crewlicensedetails.Grade = this.txt_License_grade.Text.Trim();
            crewlicensedetails.Number = this.txt_License_LicenseNo.Text.Trim();
            crewlicensedetails.IssueDate = this.txt_License_Issuedate.Text.Trim();
            crewlicensedetails.PlaceOfIssue = this.txt_License_PlaceofIssue.Text.Trim();
            crewlicensedetails.CountryId = Convert.ToInt32(this.ddl_License_country.SelectedValue);
            crewlicensedetails.ExpiryDate = this.txt_license_Expirydate.Text;
            crewlicensedetails.IsVerified = (chk_Verified.Checked) ? "Y" : "N";
            crewlicensedetails.VerifiedBy = Convert.ToInt32(Session["loginid"].ToString());
            string filename = "";
            if (this.License_FileUpload.HasFile == true)
            {

                UtilityManager uml = new UtilityManager();
                if (this.h_Licenceid.Value == "")
                {
                    filename = uml.UploadFileToServer(this.License_FileUpload.PostedFile, "", "P");
                }
                else
                {
                    filename = uml.UploadFileToServer(this.License_FileUpload.PostedFile, this.h_License_File.Value, "P");
                }
                if (filename.StartsWith("?"))
                {
                    lblMessage.Text = filename.Substring(1);
                    return;
                }
            }

            if (this.h_Licenceid.Value != "" && filename == "")
            {
                filename = this.h_License_File.Value;
            }
            crewlicensedetails.ImagePath = filename;
            ProcessCrewLicenseInsertData addlicensedata = new ProcessCrewLicenseInsertData();
            addlicensedata.Licensedetails = crewlicensedetails;
            addlicensedata.Invoke();
            bindLicensedata(Convert.ToInt32(HiddenPK.Value));
            btn_License_Add_Click( sender, e);
            lblMessage.Text = "Record Successfully Saved.";
            this.trLicensefields.Visible = false;
        }

        catch (Exception es)
        {
            lblMessage.Text = "Record Not Saved." + es.Message;
        }

        
    }
    protected void btn_License_Cancel_Click(object sender, EventArgs e)
    {
        this.btn_License_Save.Visible = false;
        this.btn_License_cancel.Visible = false;
        this.btn_License_Add.Visible = Auth.isAdd;
        trLicensefields.Visible = false;
    }
    protected void dd_License_licensename_SelectedIndexChanged(object sender, EventArgs e)
    {
        licensesetenable();
    }
    protected void gvLicense_PreRender(object sender, EventArgs e)
    {
        if (this.gvLicense.Rows.Count <= 0)
        {
            lbl_License_Message.Text = "No Records Found..!";
        }
        else
        {
            lbl_License_Message.Text = "";
        }
        HiddenField hfd;
        ImageButton imgbtn;
        int i;
        for (i = 0; i <= gvLicense.Rows.Count - 1; i++)
        {
            hfd = (HiddenField)gvLicense.Rows[i].FindControl("hiddenLvalue");
            imgbtn = (ImageButton)gvLicense.Rows[i].FindControl("ImageButton7");
            if (hfd != null)
            {
                int k = Convert.ToInt32(hfd.Value);
                if (k == 0)
                {
                    gvLicense.Rows[i].BackColor = System.Drawing.Color.FromName("#fcc2bc");
                    imgbtn.Enabled = true;
                }
                else
                {
                    imgbtn.Enabled = false;
                }
            }
        }
    }
    protected void gvLicense_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            gvLicense.Columns[1].Visible = false;
            gvLicense.Columns[2].Visible = false;
            // Can Add
            if (Auth.isAdd)
            {
            }

            // Can Modify
            if (Auth.isEdit)
            {
                this.gvLicense.Columns[1].Visible = ((Mode == "New") || (Mode == "Edit")) ? true : false;
            }

            // Can Delete
            if (Auth.isDelete)
            {
                gvLicense.Columns[2].Visible = ((Mode == "New") || (Mode == "Edit")) ? true : false;
            }

            // Can Print
            if (Auth.isPrint)
            {
            }
            Image img = ((Image)e.Row.FindControl("imgattach"));
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "ImagePath") == null)
                {

                    img.Visible = false;
                }
                else if (DataBinder.Eval(e.Row.DataItem, "ImagePath").ToString() == "")
                {

                    img.Visible = false;
                }
                else
                {
                    string appname = ConfigurationManager.AppSettings["AppName"].ToString();
                    img.Attributes.Add("onclick", "javascript:Show_Image_Large1('/"+ appname + "/EMANAGERBLOB/HRD/Documents/Professional/" + DataBinder.Eval(e.Row.DataItem, "ImagePath").ToString() + "');");
                    img.ToolTip = "Click to Preview";
                    img.Style.Add("cursor", "hand");
                }

            }
        }
        catch
        {

        }
    }
    private void licensecleardata()
    {
        this.dd_License_licensename.SelectedIndex = 0;
        this.txt_License_grade.Text = "";
        this.txt_License_LicenseNo.Text = "";
        this.txt_License_Issuedate.Text = "";
        this.txt_license_Expirydate.Text = "";
        this.ddl_License_country.SelectedIndex = 0;
        this.txt_License_PlaceofIssue.Text = "";
        this.img_License.ImageUrl = "";
        h_Licenceid.Value = "";
        h_License_File.Value = "";
        UtilityManager um = new UtilityManager();
        this.img_License.ImageUrl = um.DownloadFileFromServer("noimage.jpg", "P");
    }
    private void licensesetenable()
    {
        DataTable dt = Budget.getTable("select * from license where licenseid=" + dd_License_licensename.SelectedValue + " and COC='Y'").Tables[0];
        //this.dd_license_licenseex.SelectedValue = this.dd_License_licensename.SelectedValue;
        //if (dd_license_licenseex.SelectedItem.Text.ToString() == "Y")
        //{
        //    this.txt_license_Expirydate.Enabled = false;
        //    img_liccense_expirydate.Enabled = false;
        //}
        //else
        //{
        //    this.txt_license_Expirydate.Enabled = true;
        //    img_liccense_expirydate.Enabled = true;
        //}
        //this.txt_license_Expirydate.Text = "";
    }
    // Part-2(Courses & Certificates)
    private void bindcertificatedata(int i)
    {
        CrewCourseCertificateDetails crewCourseCertificateDetails = new CrewCourseCertificateDetails();
        crewCourseCertificateDetails.CrewId = Convert.ToInt32(HiddenPK.Value);

        ProcessCrewMemberCourseCrtificateDetailsSelectData getcertificatedetails = new ProcessCrewMemberCourseCrtificateDetailsSelectData();
        getcertificatedetails.CrewCourseCertificateDetails = crewCourseCertificateDetails;
        getcertificatedetails.Invoke();
        this.gvCourseCertificate.DataSource = getcertificatedetails.ResultSet;
        gvCourseCertificate.DataBind();
        gvCourseCertificate.SelectedIndex = -1;
    }
    private void clearcertificatedetails()
    {
        this.ddl_certificate_course.SelectedIndex = 0;
        this.txt_certificate_docno.Text = "";
        this.txt_certificate_issuedate.Text = "";
        this.txt_certificate_expirydate.Text = "";
        this.txtcertificate_issueBy.Text = "";
        hcertificateid.Value = "";
        UtilityManager um = new UtilityManager();
        this.img_cerificate.ImageUrl =um.DownloadFileFromServer("noimage.jpg","P") ;
    }
    private void set_certificate_enable()
    {
        //this.ddcouseex.SelectedValue = this.ddl_certificate_course.SelectedValue;
        //if (ddcouseex.SelectedItem.Text.ToString() != "Y")
        //{
        //    this.txt_certificate_expirydate.Enabled = false;
        //    img_certificate_expiry.Enabled = false;
        //}
        //else
        //{
        //    this.txt_certificate_expirydate.Enabled = true;
        //    img_certificate_expiry.Enabled = true;
        //}
        //this.txt_certificate_expirydate.Text = "";
    }
    #region RECORD DISPLAY AREA
    private void show_certificate_data(int i)
    {
        string Mess = "";
        HiddenField lblid = ((HiddenField)gvCourseCertificate.Rows[i].FindControl("HcourseId"));
        //this.ddl_certificate_course.SelectedValue = lblid.Value;
        Mess = Mess + Alerts.Set_DDL_Value(ddl_certificate_course, lblid.Value, "Course & Certificate");

        this.txt_certificate_docno.Text = gvCourseCertificate.Rows[i].Cells[5].Text;
        this.txt_certificate_issuedate.Text = Alerts.FormatDate(gvCourseCertificate.Rows[i].Cells[6]);
        this.txt_certificate_expirydate.Text = Alerts.FormatDate(gvCourseCertificate.Rows[i].Cells[7]);
        this.txtcertificate_issueBy.Text = gvCourseCertificate.Rows[i].Cells[8].Text;
        HiddenField himgname = ((HiddenField)gvCourseCertificate.Rows[i].FindControl("himag"));
        this.hfile.Value = himgname.Value;
        UtilityManager um = new UtilityManager();
        if (this.hfile.Value.Trim() == "" || this.hfile.Value == null)
        {
            this.img_cerificate.ImageUrl = um.DownloadFileFromServer("noimage.jpg", "P");
        }
        else
        {
            this.img_cerificate.ImageUrl = um.DownloadFileFromServer(hfile.Value, "P");
        }
        set_certificate_enable();

        if (Mess.Length > 0)
        {
            this.lblMessage.Text = "The Status Of Following Has Been Changed To In-Active <br/>" + Mess.Substring(1);
        }
    }
    protected void gvCourseCertificate_SelectedIndexChanged(object sender, EventArgs e)
    {
        int i;
        i = gvCourseCertificate.SelectedIndex;
        show_certificate_data(i);
        this.trcertificatefields.Visible = true;
        this.btn_Certifiacte_Save.Visible = false;
        this.btn_Certificate_Add.Visible = ((Mode == "Edit") || (Mode == "New"));
        this.btn_Certificate_Cancel.Visible = true;
        hcertificateid.Value = gvCourseCertificate.DataKeys[i].Value.ToString();
    }
    //protected void gvCourseCertificate_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    this.trcertificatefields.Visible = true;
    //    this.btn_Certifiacte_Save.Visible = true;
    //    this.btn_Certificate_Cancel.Visible = true;
    //    show_certificate_data(e.NewEditIndex);
    //    this.btn_Certificate_Add.Visible = ((Mode == "Edit") || (Mode == "New"));
    //    hcertificateid.Value = gvCourseCertificate.DataKeys[e.NewEditIndex].Value.ToString();

    //}
    protected void gvCourseCertificate_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int CCId=Convert.ToInt32(this.gvCourseCertificate.DataKeys[e.RowIndex].Value.ToString());
        Delete_Other_Docs(1, CCId);
    }
    #endregion
    protected void btn_Certificate_Add_Click(object sender, EventArgs e)
    {
        this.trcertificatefields.Visible = true;
        this.btn_Certifiacte_Save.Visible = true;
        this.btn_Certificate_Cancel.Visible = true;
        this.btn_Certificate_Add.Visible = false;
        clearcertificatedetails();

    }
    protected void btn_Certificate_Save_Click(object sender, EventArgs e)
    {
        CrewCourseCertificateDetails crewcertificatedetails = new CrewCourseCertificateDetails();
        if (txt_certificate_expirydate.Text.Trim() != "")
        {
            if (DateTime.Parse(txt_certificate_issuedate.Text) > DateTime.Parse(txt_certificate_expirydate.Text))
            {
                lblMessage.Text = "Issue date must be less or equal to expiry date.";
                return;
            }
        }
        try
        {

            if (this.hcertificateid.Value == "")
            {
                crewcertificatedetails.CourseCerficiateId = -1;
                crewcertificatedetails.CreatedBy = Convert.ToInt32(Session["loginid"].ToString());
            }
            else
            {
                crewcertificatedetails.CourseCerficiateId = Convert.ToInt32(this.hcertificateid.Value);
                crewcertificatedetails.ModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
            }
            crewcertificatedetails.CrewId = Convert.ToInt32(HiddenPK.Value);
            crewcertificatedetails.CourseCertificateId = Convert.ToInt32(this.ddl_certificate_course.SelectedValue);
            crewcertificatedetails.DocumentNumber = this.txt_certificate_docno.Text;
            crewcertificatedetails.DateOfIssue = this.txt_certificate_issuedate.Text;
            crewcertificatedetails.IssuedBy = this.txtcertificate_issueBy.Text;
            crewcertificatedetails.ExpiryDate = this.txt_certificate_expirydate.Text;

            string filename = "";
            if (this.CrewCertifiacateFileUpload.HasFile == true)
            {

                UtilityManager uml = new UtilityManager();
                if (this.hcertificateid.Value == "")
                {
                    filename = uml.UploadFileToServer(this.CrewCertifiacateFileUpload.PostedFile, "", "P");
                }
                else
                {
                    filename = uml.UploadFileToServer(this.CrewCertifiacateFileUpload.PostedFile, this.hfile.Value, "P");
                }
                if (filename.StartsWith("?"))
                {
                    lblMessage.Text = filename.Substring(1);
                    return;
                }
            }

            if (this.hcertificateid.Value != "" && filename == "")
            {
                filename = this.hfile.Value;
            }
            crewcertificatedetails.ImagePath = filename;
            ProcessCrewCourseCertificateInsertData addcerificatedata = new ProcessCrewCourseCertificateInsertData();
            addcerificatedata.certificatedetails = crewcertificatedetails;
            addcerificatedata.Invoke();
            lblMessage.Text = "Record Successfully Saved.";
        }


        catch (Exception es)
        {
            lblMessage.Text = "Record Not Saved.";
        }

        bindcertificatedata(Convert.ToInt32(HiddenPK.Value)); ;
        clearcertificatedetails();
        this.trcertificatefields.Visible = false;
        this.btn_Certifiacte_Save.Visible = false;
        this.btn_Certificate_Cancel.Visible = false;
        this.btn_Certificate_Add.Visible = true;

    }
    protected void btn_Certificate_Cancel_Click(object sender, EventArgs e)
    {
        this.btn_Certifiacte_Save.Visible = false;
        this.btn_Certificate_Cancel.Visible = false;
        // this.btn_Add.Visible = true;
        this.btn_Certificate_Add.Visible = ((Mode == "Edit") || (Mode == "New"));
        trcertificatefields.Visible = false;
    }
    protected void gvCourseCertificate_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            gvCourseCertificate.Columns[1].Visible = false;
            gvCourseCertificate.Columns[2].Visible = false;

            // Can Add
            if (Auth.isAdd)
            {
            }

            // Can Modify
            if (Auth.isEdit)
            {
                this.gvCourseCertificate.Columns[1].Visible = ((Mode == "New") || (Mode == "Edit")) ? true : false;
            }

            // Can Delete
            if (Auth.isDelete)
            {
                gvCourseCertificate.Columns[2].Visible = ((Mode == "New") || (Mode == "Edit")) ? true : false;
            }

            // Can Print
            if (Auth.isPrint)
            {
            }
            Image img = ((Image)e.Row.FindControl("imgattach"));
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "ImagePath") == null)
                {

                    img.Visible = false;
                }
                else if (DataBinder.Eval(e.Row.DataItem, "ImagePath").ToString() == "")
                {

                    img.Visible = false;
                }
                else
                {
                    string appname = ConfigurationManager.AppSettings["AppName"].ToString();
                    img.Attributes.Add("onclick", "javascript:Show_Image_Large1('/"+ appname + "/EMANAGERBLOB/HRD/Documents/Professional/" + DataBinder.Eval(e.Row.DataItem, "ImagePath").ToString() + "');");
                    img.ToolTip = "Click to Preview";
                    img.Style.Add("cursor", "hand");
                }

            }
        }
        catch
        {

        }
    }
    protected void gvCourseCertificate_PreRender(object sender, EventArgs e)
    {
        if (this.gvCourseCertificate.Rows.Count <= 0)
        {
            lbl_certificate_message.Text = "No Records Found..!";
        }
        else
        {
            lbl_certificate_message.Text = "";
        }
        HiddenField hfd;
        ImageButton imgbtn;
        int i;
        for (i = 0; i <= gvCourseCertificate.Rows.Count - 1; i++)
        {
            hfd = (HiddenField)gvCourseCertificate.Rows[i].FindControl("hiddenCvalue");
            imgbtn = (ImageButton)gvCourseCertificate.Rows[i].FindControl("ImageButton7");
            if (hfd != null)
            {
                int k = Convert.ToInt32(hfd.Value);
                if (k == 0)
                {
                    gvCourseCertificate.Rows[i].BackColor = System.Drawing.Color.FromName("#fcc2bc");
                    imgbtn.Enabled = true;
                }
                else
                {
                    imgbtn.Enabled = false;
                }
            }
        }
    }
    protected void ddl_certificate_course_SelectedIndexChanged(object sender, EventArgs e)
    {
        set_certificate_enable();

    }
        // Part-3(Dangerous Cargo Endorsements)
    private void bindDCEGrid(int id)
    {
        ProcessSelectCrewMemberDangerousCargoDetails selectcargodetails = new ProcessSelectCrewMemberDangerousCargoDetails();
        try
        {
            CrewCargoDetails obj = new CrewCargoDetails();
            obj.CrewId = id;
            selectcargodetails.CrewCargoDetails = obj;
            selectcargodetails.Invoke();
            GvDCE.DataSource = selectcargodetails.ResultSet.Tables[0];
            GvDCE.DataBind();
        }
        catch { }
      
    }
    #region RECORD DISPLAY AREA
    protected void Show_Record_DCE_Details(CrewCargoDetails CrewCargoDetails)
    {
        string Mess = "";
        Hidden_cargo.Value = CrewCargoDetails.DangerousCargoId.ToString();
        hiddencargoimage.Value = CrewCargoDetails.ImagePath.ToString();
        //ddcargoname.SelectedValue = CrewCargoDetails.CargoId.ToString();
        Mess = Mess + Alerts.Set_DDL_Value(ddcargoname, CrewCargoDetails.CargoId.ToString(), "Endoursement Name");

        txtnumber.Text = CrewCargoDetails.Number;
        //ddDCEnationality.SelectedValue = CrewCargoDetails.NationalityId.ToString();
        Mess = Mess + Alerts.Set_DDL_Value(ddDCEnationality, CrewCargoDetails.NationalityId.ToString(), "Country");

        txtgradelevel.Text = CrewCargoDetails.GradeLevel;
        txtcargopoi.Text = CrewCargoDetails.PlaceOfIssue;
        txtcargoDOI.Text = Alerts.FormatDate(CrewCargoDetails.DateOfIssue);
        txtcargoExpiry.Text = Alerts.FormatDate(CrewCargoDetails.ExpiryDate);
        UtilityManager um= new UtilityManager();
        if (CrewCargoDetails.ImagePath.Trim() == "")
        {
            Imagecargo.ImageUrl = um.DownloadFileFromServer("noimage.jpg", "P");
        }
        else
        {
            Imagecargo.ImageUrl = um.DownloadFileFromServer(CrewCargoDetails.ImagePath.Trim(), "P");
        }
        panelDCE.Visible = true;
        if (Mess.Length > 0)
        {
            this.lblMessage.Text = "The Status Of Following Has Been Changed To In-Active <br/>" + Mess.Substring(1);
        }

    }
    protected void GvDCE_SelectIndexChanged(object sender, EventArgs e)
    {
        HiddenField hfddce;
        HiddenField hfddceimage;
        CrewCargoDetails cargoDetails = new CrewCargoDetails();
        ProcessSelectCrewMemberDangerousCargoDetails selectcargodetails = new ProcessSelectCrewMemberDangerousCargoDetails();
        hfddce = (HiddenField)GvDCE.Rows[GvDCE.SelectedIndex].FindControl("HiddenIdcargo");
        hfddceimage = (HiddenField)GvDCE.Rows[GvDCE.SelectedIndex].FindControl("Hiddenimage");
        cargoDetails.DangerousCargoId = Convert.ToInt32(hfddce.Value.ToString());
        cargoDetails.ImagePath = hfddceimage.Value.ToString();
        selectcargodetails.CrewCargoDetails = cargoDetails;
        selectcargodetails.Invoke();

        //---------------------
        Show_Record_DCE_Details(selectcargodetails.CrewCargoDetails);
        btn_cargo_Add.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        btn_cargo_cancel.Visible = true;
        btn_cargo_save.Visible = false;
    }
    //protected void DCE_Row_Editing(object sender, GridViewEditEventArgs e)
    //{
    //    HiddenField hfddce;
    //    HiddenField hfddceimage;
    //    CrewCargoDetails cargoDetails = new CrewCargoDetails();
    //    ProcessSelectCrewMemberDangerousCargoDetails selectcargodetails = new ProcessSelectCrewMemberDangerousCargoDetails();
    //    hfddce = (HiddenField)GvDCE.Rows[e.NewEditIndex].FindControl("HiddenIdcargo");
    //    hfddceimage = (HiddenField)GvDCE.Rows[e.NewEditIndex].FindControl("Hiddenimage");
    //    cargoDetails.DangerousCargoId = Convert.ToInt32(hfddce.Value.ToString());
    //    cargoDetails.ImagePath = hfddceimage.Value.ToString();
    //    selectcargodetails.CrewCargoDetails = cargoDetails;
    //    selectcargodetails.Invoke();

    //    //--------------------
    //    Show_Record_DCE_Details(selectcargodetails.CrewCargoDetails);
    //    GvDCE.SelectedIndex = e.NewEditIndex;
    //    btn_cargo_cancel.Visible = true;
    //    btn_cargo_save.Visible = true;
    //    btn_cargo_Add.Visible = false;
    //}
    protected void DCE_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int DCEid=Convert.ToInt32(((HiddenField)GvDCE.Rows[e.RowIndex].FindControl("HiddenIdcargo")).Value);
        Delete_Other_Docs(2, DCEid);
    }
    #endregion
    protected void GvDCE_PreRender(object sender, EventArgs e)
    {
        HiddenField hfd;
        ImageButton imgbtn;
        int i;
        for (i = 0; i <= GvDCE.Rows.Count - 1; i++)
        {
            hfd = (HiddenField)GvDCE.Rows[i].FindControl("hiddenDvalue");
            imgbtn = (ImageButton)GvDCE.Rows[i].FindControl("ImageButton7");
            if (hfd != null)
            {
                int k = Convert.ToInt32(hfd.Value);
                if (k == 0)
                {
                    GvDCE.Rows[i].BackColor = System.Drawing.Color.FromName("#fcc2bc");
                    imgbtn.Enabled = true;
                }
                else
                {
                    imgbtn.Enabled = false;
                }
            }
        }
        if (GvDCE.Rows.Count <= 0) { lbl_GridView_cargo.Text = "No Records Found..!"; }
    }
    protected void GvDCE_DataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GvDCE.Columns[1].Visible = false;
            GvDCE.Columns[2].Visible = false;
            // Can Add
            if (Auth.isAdd)
            {
            }

            // Can Modify
            if (Auth.isEdit)
            {
                GvDCE.Columns[1].Visible = ((Mode == "New") || (Mode == "Edit")) ? true : false;
            }

            // Can Delete
            if (Auth.isDelete)
            {
                GvDCE.Columns[2].Visible = ((Mode == "New") || (Mode == "Edit")) ? true : false;
            }

            // Can Print
            if (Auth.isPrint)
            {
            }
            Image img = ((Image)e.Row.FindControl("imgattach"));
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "ImagePath") == null)
                {

                    img.Visible = false;
                }
                else if (DataBinder.Eval(e.Row.DataItem, "ImagePath").ToString() == "")
                {

                    img.Visible = false;
                }
                else
                {
                    string appname = ConfigurationManager.AppSettings["AppName"].ToString();
                    img.Attributes.Add("onclick", "javascript:Show_Image_Large1('/"+ appname + "/EMANAGERBLOB/HRD/Documents/Professional/" + DataBinder.Eval(e.Row.DataItem, "ImagePath").ToString() + "');");
                    img.ToolTip = "Click to Preview";
                    img.Style.Add("cursor", "hand");
                }

            }
        }
        catch
        {
            //GvDCE.Columns[1].Visible = false;
            //GvDCE.Columns[2].Visible = false;
        }
    }
    protected void btn_cargo_Add_Click(object sender, EventArgs e)
    {
        Hidden_cargo.Value = "";
        txtnumber.Text = "";
        ddDCEnationality.SelectedIndex = 0;
        txtgradelevel.Text = "";
        txtcargopoi.Text = "";
        txtcargoDOI.Text = "";
        txtcargoExpiry.Text = "";
        //---------------------
        btn_cargo_save.Visible = true;
        btn_cargo_cancel.Visible = true;
        btn_cargo_Add.Visible = false;
        panelDCE.Visible = true;
        GvDCE.SelectedIndex = -1;
        UtilityManager um= new UtilityManager();
        Imagecargo.ImageUrl = um.DownloadFileFromServer("noimage.jpg", "P");
     
    }
    protected void btn_cargo_save_Click(object sender, EventArgs e)
    {
        CrewCargoDetails cargoDetails = new CrewCargoDetails();
        if (txtcargoExpiry.Text.Trim() != "" && txtcargoDOI.Text.Trim() != "")
        {
            if (DateTime.Parse(txtcargoDOI.Text) > DateTime.Parse(txtcargoExpiry.Text))
            {
                lblMessage.Text = "Issue date must be less or equal to expiry date.";
                return;
            }
        }
        try
        {
            if (Hidden_cargo.Value.ToString().Trim() == "")
            {
                cargoDetails.CreatedBy = Convert.ToInt32(Session["loginid"].ToString());
            }
            else
            {
                cargoDetails.DangerousCargoId = Convert.ToInt32(Hidden_cargo.Value);
                cargoDetails.Modifiedby = Convert.ToInt32(Session["loginid"].ToString());
            }
            cargoDetails.CrewId = Convert.ToInt32(HiddenPK.Value);
            cargoDetails.CargoId = Convert.ToInt32(ddcargoname.SelectedValue);
            cargoDetails.Number = txtnumber.Text;
            cargoDetails.NationalityId = Convert.ToInt32(ddDCEnationality.SelectedValue);
            cargoDetails.GradeLevel = txtgradelevel.Text;
            cargoDetails.PlaceOfIssue = txtcargopoi.Text;
            cargoDetails.DateOfIssue = txtcargoDOI.Text;
            cargoDetails.ExpiryDate = txtcargoExpiry.Text;
            if (this.FileUploadcargo.PostedFile != null && this.FileUploadcargo.PostedFile.ContentLength > 0)
            {
                UtilityManager uml = new UtilityManager();
                string filename1;
                filename1 = uml.UploadFileToServer(this.FileUploadcargo.PostedFile, this.hiddencargoimage.Value, "P");
                if (filename1.StartsWith("?"))
                {
                    lblMessage.Text = filename1.Substring(1);
                    return;
                }
                cargoDetails.ImagePath = filename1;
            }
            else
            {
                cargoDetails.ImagePath = hiddencargoimage.Value;
            }
            ProcessAddCrewMemberDangerousCargoDetails adddetails = new ProcessAddCrewMemberDangerousCargoDetails();
            adddetails.CrewCargoDetails = cargoDetails;
            adddetails.Invoke();
            bindDCEGrid(Convert.ToInt32(HiddenPK.Value));
            btn_cargo_Add_Click(sender, e);
            lblMessage.Text = "Record Successfully Saved.";
            this.panelDCE.Visible = false;
            this.btn_cargo_save.Visible = false;
            this.btn_cargo_cancel.Visible = false;
            this.btn_cargo_Add.Visible = true;
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Record Not Saved.";
            //Response.Write(ex.Message.ToString());
        }
    }
    protected void btn_cargo_cancel_Click(object sender, EventArgs e)
    {
        if (Mode == "New")
        {
            btn_cargo_Add.Visible = (Auth.isAdd || Auth.isEdit);
        }
        else if (Mode == "Edit")
        {
            btn_cargo_Add.Visible = (Auth.isAdd || Auth.isEdit);
        }
        btn_cargo_cancel.Visible = false;
        panelDCE.Visible = false;
        btn_cargo_save.Visible = false;
        GvDCE.SelectedIndex = -1;
        btn_cargo_Add.Visible = true;
    }
        //Part-4 (Other Documents)
    private void bindotherdocdata(int i)
    {
        CrewOtherDocumentsDetails crewotherdetails = new CrewOtherDocumentsDetails();
        crewotherdetails.CrewId = Convert.ToInt32(HiddenPK.Value);
        ProcessCrewMemberOtherDocumentSelectData getotherdocdetails = new ProcessCrewMemberOtherDocumentSelectData();

        getotherdocdetails.OtherDocDetails = crewotherdetails;
        getotherdocdetails.Invoke();
        this.gvotherdocument.DataSource = getotherdocdetails.ResultSet;
        gvotherdocument.DataBind();
        gvotherdocument.SelectedIndex = -1;
    }
    //protected void gvotherdocument_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    this.trotherdoc.Visible = true;
    //    btn_Otherdoc_Save.Visible = true;
    //    btn_Otherdoc_Cancel.Visible = true;
    //    show_otherdoc_data(e.NewEditIndex);

    //    this.h_otherdoc_id.Value = gvotherdocument.DataKeys[e.NewEditIndex].Value.ToString();
    //}
    protected void gvotherdocument_SelectedIndexChanged(object sender, EventArgs e)
    {
        int i;
        i = gvotherdocument.SelectedIndex;
        show_otherdoc_data(i);
        this.trotherdoc.Visible = true;
        btn_Otherdoc_Save.Visible = false;
        this.btn_otherdoc_Add.Visible = ((Mode == "Edit") || (Mode == "New"));
        btn_Otherdoc_Cancel.Visible = true;
    }
    protected void gvotherdocument_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int Oid=Convert.ToInt32(this.gvotherdocument.DataKeys[e.RowIndex].Value.ToString());
        Delete_Other_Docs(3, Oid);
        
    }
    protected void btn_otherdoc_Add_Click(object sender, EventArgs e)
    {
        this.trotherdoc.Visible = true;
        btn_Otherdoc_Save.Visible = true;
        btn_Otherdoc_Cancel.Visible = true;
        clearotherdocumentdetails();
    }
    private void clearotherdocumentdetails()
    {
        this.ddl_otherdoc_coursename.SelectedIndex = 0;
        this.txt_otherdoc_docno.Text = "";
        this.txt_otherdoc_issuedate.Text = "";
        this.txt_otherdoc_expirtdate.Text = "";
        this.txt_otherdoc_issueby.Text = "";
        this.h_otherdoc_id.Value = "";
        this.img_otherdoc.ImageUrl = "";
        UtilityManager um = new UtilityManager();
        img_otherdoc.ImageUrl = um.DownloadFileFromServer("noimage.jpg", "P");
    }
    protected void btn_Otherdoc_Save_Click(object sender, EventArgs e)
    {
        CrewOtherDocumentsDetails crewotherdetails = new CrewOtherDocumentsDetails();
        if (txt_otherdoc_expirtdate.Text.Trim() != "")
        {
            if (DateTime.Parse(txt_otherdoc_issuedate.Text) > DateTime.Parse(txt_otherdoc_expirtdate.Text))
            {
                lblMessage.Text = "Issue date must be less or equal to expiry date.";
                return;
            }
        }
        try
        {

            if (this.h_otherdoc_id.Value == "")
            {
                crewotherdetails.CrewOtherDocId = -1;
                crewotherdetails.CreatedBy = Convert.ToInt32(Session["loginid"].ToString());
            }
            else
            {
                crewotherdetails.CrewOtherDocId = Convert.ToInt32(this.h_otherdoc_id.Value);
                crewotherdetails.ModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
            }
            crewotherdetails.CrewId = Convert.ToInt32(HiddenPK.Value);
            crewotherdetails.CourseId = Convert.ToInt32(this.ddl_otherdoc_coursename.SelectedValue);
            crewotherdetails.DocumentNumber = txt_otherdoc_docno.Text;
            crewotherdetails.DateOfIssue = txt_otherdoc_issuedate.Text;
            crewotherdetails.IssuedBy = this.txt_otherdoc_issueby.Text;
            crewotherdetails.ExpiryDate = this.txt_otherdoc_expirtdate.Text;

            string filename = "";
            if (this.otherdoc_fileupload.HasFile == true)
            {

                UtilityManager uml = new UtilityManager();
                if (this.h_otherdoc_id.Value == "")
                {
                    filename = uml.UploadFileToServer(this.otherdoc_fileupload.PostedFile, "", "P");
                }
                else
                {
                    filename = uml.UploadFileToServer(this.otherdoc_fileupload.PostedFile, this.h_otherdoc_hfile.Value, "P");
                }
                if (filename.StartsWith("?"))
                {
                    lblMessage.Text = filename.Substring(1);
                    return;
                }

            }

            if (this.h_otherdoc_id.Value != "" && filename == "")
            {
                filename = h_otherdoc_hfile.Value;
            }
            crewotherdetails.ImagePath = filename;
            if (chk_OtherDoc_IsActive.Checked == true)
            {
                crewotherdetails.IsActive = 'Y';

            }
            else
            {
                crewotherdetails.IsActive = 'N';
            }
            ProcessAddCrewMemberOtherDocumentDetails addotherdocdetails = new ProcessAddCrewMemberOtherDocumentDetails();

            addotherdocdetails.OtherDocDetails = crewotherdetails;
            addotherdocdetails.Invoke();
            lblMessage.Text = "Record Successfully Saved.";
        }


        catch (Exception es)
        {
            lblMessage.Text = "Record Not Saved.";
        }
        this.btn_Otherdoc_Save.Visible = false;
        bindotherdocdata(Convert.ToInt32(HiddenPK.Value)); ;
        clearotherdocumentdetails();
        this.trotherdoc.Visible = false;
        this.btn_Otherdoc_Cancel.Visible = false;
        this.btn_otherdoc_Add.Visible = true;
    }
    protected void btn_Otherdoc_Cancel_Click(object sender, EventArgs e)
    {
        btn_Otherdoc_Save.Visible = false;
        btn_Otherdoc_Cancel.Visible = false;
        // this.btn_Add.Visible = true;
        this.btn_otherdoc_Add.Visible = ((Mode == "Edit") || (Mode == "New"));
        trotherdoc.Visible = false;
    }
    private void show_otherdoc_data(int i)

    {
        string Mess = "";
        HiddenField lblid = ((HiddenField)gvotherdocument.Rows[i].FindControl("HdocId"));
        //this.ddl_otherdoc_coursename.SelectedValue = lblid.Value;
        Mess = Mess + Alerts.Set_DDL_Value(ddl_otherdoc_coursename, lblid.Value, "Document Name");

        txt_otherdoc_docno.Text = gvotherdocument.Rows[i].Cells[5].Text;
        txt_otherdoc_issuedate.Text = Alerts.FormatDate(gvotherdocument.Rows[i].Cells[6]);
        this.txt_otherdoc_expirtdate.Text =Alerts.FormatDate(gvotherdocument.Rows[i].Cells[7]);
        this.txt_otherdoc_issueby.Text = gvotherdocument.Rows[i].Cells[8].Text.Trim().Replace("&nbsp;","");
        HiddenField himgname = ((HiddenField)gvotherdocument.Rows[i].FindControl("himag"));
        this.h_otherdoc_hfile.Value = himgname.Value;
        if (this.h_otherdoc_hfile.Value != "" || this.h_otherdoc_hfile.Value == null)
        {
            UtilityManager um = new UtilityManager();
            this.img_otherdoc.ImageUrl = um.DownloadFileFromServer(h_otherdoc_hfile.Value, "P");
        }
        else
        {
            UtilityManager um = new UtilityManager();
            img_otherdoc.ImageUrl = um.DownloadFileFromServer("noimage.jpg", "P");
        }
        if (gvotherdocument.Rows[i].Cells[9].Text.ToUpper() != "NO")
        {
            this.chk_OtherDoc_IsActive.Checked = true;
        }
        else
        {
            this.chk_OtherDoc_IsActive.Checked = false;
        }
        set_other_enable();
        //----------------------
        if (Mess.Length > 0)
        {
            this.lblMessage.Text = "The Status Of Following Has Been Changed To In-Active <br/>" + Mess.Substring(1);
        }
    }
    protected void ddl_otherdoc_coursename_SelectedIndexChanged(object sender, EventArgs e)
    {
        set_other_enable();
    }
    private void set_other_enable()
    {
        //this.dd_otherdoc_courseex.SelectedValue = ddl_otherdoc_coursename.SelectedValue;
        //if (dd_otherdoc_courseex.SelectedItem.Text.ToString() == "Y")
        //{
        //    this.txt_otherdoc_expirtdate.Enabled = false;
        //    img_otherdoc_expiry.Enabled = false;
        //}
        //else
        //{
        //    this.txt_otherdoc_expirtdate.Enabled = true;
        //    img_otherdoc_expiry.Enabled = true;
        //}
        //this.txt_otherdoc_expirtdate.Text = "";
    }
    protected void gvotherdocument_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            gvotherdocument.Columns[1].Visible = false;
            gvotherdocument.Columns[2].Visible = false;

            // Can Add
            if (Auth.isAdd)
            {
            }

            // Can Modify
            if (Auth.isEdit)
            {
                this.gvotherdocument.Columns[1].Visible = ((Mode == "New") || (Mode == "Edit")) ? true : false;
            }

            // Can Delete
            if (Auth.isDelete)
            {
                gvotherdocument.Columns[2].Visible = ((Mode == "New") || (Mode == "Edit")) ? true : false;
            }

            // Can Print
            if (Auth.isPrint)
            {
            }
            Image img = ((Image)e.Row.FindControl("imgattach"));
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "ImagePath") == null)
                {

                    img.Visible = false;
                }
                else if (DataBinder.Eval(e.Row.DataItem, "ImagePath").ToString() == "")
                {

                    img.Visible = false;
                }
                else
                {
                    string appname = ConfigurationManager.AppSettings["AppName"].ToString();
                    img.Attributes.Add("onclick", "javascript:Show_Image_Large1('/"+ appname + "/EMANAGERBLOB/HRD/Documents/Professional/" + DataBinder.Eval(e.Row.DataItem, "ImagePath").ToString() + "');");
                    img.ToolTip = "Click to Preview";
                    img.Style.Add("cursor", "hand");
                }

            }
        }
        catch
        {

        }
    }
    protected void gvotherdocument_PreRender(object sender, EventArgs e)
    {
        HiddenField hfd;
        ImageButton imgbtn;
        int i;
        for (i = 0; i <= gvotherdocument.Rows.Count - 1; i++)
        {
            hfd = (HiddenField)gvotherdocument.Rows[i].FindControl("hiddenOvalue");
            imgbtn = (ImageButton)gvotherdocument.Rows[i].FindControl("ImageButton7");
            if (hfd != null)
            {
                int k = Convert.ToInt32(hfd.Value);
                if (k == 0)
                {
                    gvotherdocument.Rows[i].BackColor = System.Drawing.Color.FromName("#fcc2bc");
                    imgbtn.Enabled = true;
                }
                else
                {
                    imgbtn.Enabled = false;
                }
            }
        }
        if (this.gvotherdocument.Rows.Count <= 0)
        {
            lbl_otherdoc_message.Text = "No Records Found..!";
        }
        else
        {
            lbl_otherdoc_message.Text = "";
        }
    }
    #endregion
    # region  Tab-3 Academic Details
    private void BindAcademicDetailsGrid(int id)
    {
        //-----------------------------
        //txt_MemberId.Text = txt_MemberId.Text;
        //txt_LastVessel2.Text = (ddl_Nationality.SelectedIndex == 0) ? "" : ddl_Nationality.SelectedItem.ToString();
        //ddcurrentrank2.Text = ddcurrentrank.Text;
        //txt_LastVessel2.Text = txt_LastVessel.Text;
        //txt_Status2.Text = txt_Status.Text;
        //txt_FirstName2.Text = txt_FirstName.Text;
        //txt_MiddleName2.Text = txt_MiddleName.Text;
        //txt_LastName2.Text = txt_LastName.Text;

        ProcessSelectCrewMemberAcademicDetails processselectacademicdetails = new ProcessSelectCrewMemberAcademicDetails();
        try
        {
            AcademicDetails obj = new AcademicDetails();
            obj.CrewId = id;
            processselectacademicdetails.AcademicDetails = obj;
            processselectacademicdetails.Invoke();
        }
        catch { }
        GridView_Academic.DataSource = processselectacademicdetails.ResultSet.Tables[0];
        GridView_Academic.DataBind();

        txt_CertificateType_Academic.Text = "";
        txt_Institute_Academic.Text = "";
        txt_DurationFrom_Academic.Text = "";
        txt_DurationTo_Academic.Text = "";
        txt_Grade_Academic.Text = "";
    }
    #region RECORD DISPLAY AREA
    protected void Show_Record_Academic_Details(AcademicDetails AcademicDetails)
    {
        string path13, path23;
        Hidden_Academic.Value = AcademicDetails.AcademicDetailsId.ToString();
        hfd_Image3.Value = AcademicDetails.ImagePath.ToString();
        txt_CertificateType_Academic.Text = AcademicDetails.TypeOfCertificate;
        txt_Institute_Academic.Text = AcademicDetails.Institute;
        txt_DurationFrom_Academic.Text = Alerts.FormatDate(AcademicDetails.DurationForm);
        txt_DurationTo_Academic.Text = Alerts.FormatDate(AcademicDetails.DurationTo);
        txt_Grade_Academic.Text = AcademicDetails.Grade;
        UtilityManager um = new UtilityManager();
        if (AcademicDetails.ImagePath.Trim() == "")
        {
            img_Academic.ImageUrl = um.DownloadFileFromServer("noimage.jpg", "A");
        }
        else
        {
            img_Academic.ImageUrl = um.DownloadFileFromServer(AcademicDetails.ImagePath, "A");
        }
        hfd_Image3.Value= AcademicDetails.ImagePath;  
        pnl_Academic_Details.Visible = true;
    }
    // VIEW THE RECORD
    protected void GridView_Academic_SelectIndexChanged(object sender, EventArgs e)
    {
        HiddenField hfd, hfd22;
        AcademicDetails AcademicDetails = new AcademicDetails();
        ProcessSelectCrewMemberAcademicDetails selectacademicdetails = new ProcessSelectCrewMemberAcademicDetails();
        hfd = (HiddenField)GridView_Academic.Rows[GridView_Academic.SelectedIndex].FindControl("HiddenId");
        hfd22 = (HiddenField)GridView_Academic.Rows[GridView_Academic.SelectedIndex].FindControl("Hiddenfd22");
        AcademicDetails.AcademicDetailsId = Convert.ToInt32(hfd.Value.ToString());
        AcademicDetails.ImagePath = hfd22.Value.ToString();
        selectacademicdetails.AcademicDetails = AcademicDetails;
        selectacademicdetails.Invoke();

        //---------------------
        Show_Record_Academic_Details(selectacademicdetails.AcademicDetails);
        btn_Add_Academic.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        btn_Cancel_Academic.Visible = true;
        btn_Save_Academic.Visible = false;

    }
    // EDIT THE RECORD
    //protected void GridView_Academic_Row_Editing(object sender, GridViewEditEventArgs e)
    //{
    //    HiddenField hfd, hfd22;
    //    AcademicDetails AcademicDetails = new AcademicDetails();
    //    ProcessSelectCrewMemberAcademicDetails selectacademicdetails = new ProcessSelectCrewMemberAcademicDetails();
    //    hfd = (HiddenField)GridView_Academic.Rows[e.NewEditIndex].FindControl("HiddenId");
    //    hfd22 = (HiddenField)GridView_Academic.Rows[e.NewEditIndex].FindControl("Hiddenfd22");
    //    AcademicDetails.AcademicDetailsId = Convert.ToInt32(hfd.Value.ToString());
    //    AcademicDetails.ImagePath = hfd22.Value.ToString();
    //    selectacademicdetails.AcademicDetails = AcademicDetails;
    //    selectacademicdetails.Invoke();

    //    //--------------------
    //    Show_Record_Academic_Details(selectacademicdetails.AcademicDetails);
    //    GridView_Academic.SelectedIndex = e.NewEditIndex;
    //    btn_Save_Academic.Visible = true;
    //    btn_Add_Academic.Visible =  (Auth.isAdd || Auth.isEdit) && (Mode == "Edit" );  
    //    btn_Cancel_Academic.Visible = true;
    //}
    // DELETE THE RECORD
    protected void GridView_Academic_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        HiddenField hfd;
        AcademicDetails exp = new AcademicDetails();
        hfd = (HiddenField)GridView_Academic.Rows[e.RowIndex].FindControl("HiddenId");
        ProcessDeleteCrewMemberAcademicDetailsById obj = new ProcessDeleteCrewMemberAcademicDetailsById();
        exp.AcademicDetailsId = Convert.ToInt32(hfd.Value.ToString());
        obj.AcademicDetails = exp;
        obj.Invoke();
        //---------------------------------
        BindAcademicDetailsGrid(Convert.ToInt32(HiddenPK.Value));
    }
    #endregion
    protected void GridView_Academic_PreRender(object sender, EventArgs e)
    {
        if (GridView_Academic.Rows.Count <= 0) { lbl_GridView_Academic.Text = "No Records Found..!"; }
    }
    protected void GridView_Academic_DataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridView_Academic.Columns[1].Visible = false;
            GridView_Academic.Columns[2].Visible = false;
            // Can Add
            if (Auth.isAdd)
            {
            }

            // Can Modify
            if (Auth.isEdit)
            {
                GridView_Academic.Columns[1].Visible = ((Mode == "New") || (Mode == "Edit")) ? true : false;
            }

            // Can Delete
            if (Auth.isDelete)
            {
                GridView_Academic.Columns[2].Visible = ((Mode == "New") || (Mode == "Edit")) ? true : false;
            }

            // Can Print
            if (Auth.isPrint)
            {
            }
            Image img = ((Image)e.Row.FindControl("imgattach"));
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "ImagePath") == null)
                {

                    img.Visible = false;
                }
                else if (DataBinder.Eval(e.Row.DataItem, "ImagePath").ToString() == "")
                {

                    img.Visible = false;
                }
                else
                {
                    string appname = ConfigurationManager.AppSettings["AppName"].ToString();
                    img.Attributes.Add("onclick", "javascript:Show_Image_Large1('/"+ appname + "/EMANAGERBLOB/HRD/Documents/Academic/" + DataBinder.Eval(e.Row.DataItem, "ImagePath").ToString() + "');");
                    img.ToolTip = "Click to Preview";
                    img.Style.Add("cursor", "hand");
                }

            }
        }
        catch
        {
            GridView_Academic.Columns[1].Visible = false;
            GridView_Academic.Columns[2].Visible = false;
        }


    }
    protected void btn_Add_Academic_Click(object sender, EventArgs e)
    {
        Hidden_Academic.Value = "";
        txt_CertificateType_Academic.Text = "";
        txt_Institute_Academic.Text = "";
        txt_DurationFrom_Academic.Text = "";
        txt_DurationTo_Academic.Text = "";
        txt_Grade_Academic.Text = "";
        //---------------------
        btn_Save_Academic.Visible = true;
        btn_Add_Academic.Visible= false; 
        btn_Cancel_Academic.Visible = true;
        pnl_Academic_Details.Visible = true;
        GridView_Academic.SelectedIndex = -1;
        UtilityManager um = new UtilityManager();
        img_Academic.ImageUrl = um.DownloadFileFromServer("noimage.jpg", "A");
      
    }
    protected void btn_Save_Academic_Click(object sender, EventArgs e)
    {
        AcademicDetails AcademicDetails = new AcademicDetails();
        if (txt_DurationTo_Academic.Text.Trim() != "" && txt_DurationFrom_Academic.Text.Trim() != "")
        {
            if (DateTime.Parse(txt_DurationFrom_Academic.Text) > DateTime.Parse(txt_DurationTo_Academic.Text))
            {
                lblMessage.Text = "Issue date must be less or equal to expiry date.";
                return;
            }
        }
        try
        {
            if (Hidden_Academic.Value.ToString().Trim() == "")
            {
                AcademicDetails.CreatedBy = Convert.ToInt32(Session["loginid"].ToString());

            }
            else
            {
                AcademicDetails.AcademicDetailsId = Convert.ToInt32(Hidden_Academic.Value);
                AcademicDetails.Modifiedby = Convert.ToInt32(Session["loginid"].ToString());
            }
            AcademicDetails.CrewId = Convert.ToInt32(HiddenPK.Value);
            AcademicDetails.TypeOfCertificate = txt_CertificateType_Academic.Text; ;
            AcademicDetails.Institute = txt_Institute_Academic.Text;
            AcademicDetails.DurationForm = txt_DurationFrom_Academic.Text;
            AcademicDetails.DurationTo = txt_DurationTo_Academic.Text;
            AcademicDetails.Grade = txt_Grade_Academic.Text;
            AcademicDetails.ImagePath = "";

            if (this.upld_Image_Academic != null && this.upld_Image_Academic.FileContent.Length > 0)
            {
                HttpPostedFile file = upld_Image_Academic.PostedFile;
                UtilityManager um = new UtilityManager();
                string fileName;
                fileName = um.UploadFileToServer(this.upld_Image_Academic.PostedFile, this.hfd_Image3.Value.Trim(), "A");
                if (fileName.StartsWith("?"))
                {
                    lblMessage.Text = fileName.Substring(1);
                    return;
                }
                AcademicDetails.ImagePath = fileName;
            }
            else
            {
                AcademicDetails.ImagePath = hfd_Image3.Value;
            }

            ProcessAddCrewMemberAcademicDetails acddetails = new ProcessAddCrewMemberAcademicDetails();
            acddetails.AcademicDetails = AcademicDetails;
            acddetails.Invoke();
            BindAcademicDetailsGrid(Convert.ToInt32(HiddenPK.Value));
            btn_Add_Academic_Click(sender, e);
            lblMessage.Text = "Record Successfully Saved.";
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Record Not Saved.";
            //Response.Write(ex.Message.ToString());
        }
       
    }
    protected void btn_Cancel_Academic_Click(object sender, EventArgs e)
    {
        if (Mode == "New")
        {
            btn_Add_Academic.Visible = (Auth.isAdd || Auth.isEdit);
        }
        else if (Mode == "Edit")
        {
            btn_Add_Academic.Visible = (Auth.isAdd || Auth.isEdit);
        }
        btn_Cancel_Academic.Visible = false;
        pnl_Academic_Details.Visible = false;
        btn_Save_Academic.Visible = false;
        GridView_Academic.SelectedIndex = -1;
    }
    #endregion
    #region  Tab-4 Medical Details
    //protected void RadioButtonList3_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (RadioButtonList3.SelectedIndex == 0)
    //    {
    //        pnl_Medical_1.Visible = true;
    //        pnl_Medical_2.Visible = false;
    //        btn_Medical_Cancel.Visible = false;
    //        btn_Medical_Save.Visible = false;
    //        pnl_Medical_Details.Visible = false;
    //        btn_Medical_Add.Visible = (Auth.isAdd || Auth.isEdit) && (Mode == "Edit" || Mode == "New");
    //        BindMedicalDetailsGrid(Convert.ToInt32(HiddenPK.Value));
    //    }
    //    else
    //    {
    //        pnl_Medical_2.Visible = true;
    //        pnl_Medical_1.Visible = false;
    //        this.btn_PCI_Save.Visible = false;
    //        this.btn_PCI_Cancel.Visible = false;
    //        this.btn_PCI_Add.Visible = ((Auth.isEdit || Auth.isAdd) && (Mode == "New" || Mode == "Edit"));
    //        trfields.Visible = false;
    //        binddata(Convert.ToInt32(HiddenPK.Value));
    //    }
    //}
        // Part -1(Medical Details)
    public void BindMedicalDetailsGrid(int id)
    {
        ProcessSelectMedicalDocumentDetails processselectmedicaldetails = new ProcessSelectMedicalDocumentDetails();
        try
        {
            MedicalDetails obj = new MedicalDetails();
            obj.CrewId = id;
            processselectmedicaldetails.MedicalDetails = obj;
            obj.DocumentTypeId = 0;
            processselectmedicaldetails.Invoke();
            GridView_Medical.DataSource = processselectmedicaldetails.ResultSet.Tables[0];
            GridView_Medical.DataBind();
        }
        catch { }

    }
    #region RECORD DISPLAY AREA
    protected void Medical_Show_Record(MedicalDetails MDocument)
    {
        string Mess = "";
        Hidden_Medical.Value = MDocument.MedicalDetailsId.ToString();
        hfd_Image4.Value = MDocument.ImagePath.ToString();
        Mess=Alerts.Set_DDL_Value(ddl_MedicalDocuments,MDocument.DocumentTypeId.ToString(),"Medical Document");
        //ddl_MedicalDocuments.SelectedValue = MDocument.DocumentTypeId.ToString();
        txt_Medical_DocumentNumber.Text = MDocument.DocumentNumber;
        txt_Medical_IssueDate.Text = Alerts.FormatDate(MDocument.IssueDate);
        txt_Medical_ExpiryDate.Text =Alerts.FormatDate(MDocument.ExpiryDate);
        txt_Medical_PlaceOfIssue.Text = MDocument.PlaceOfIssue;
        txt_BloodGroup.Text = MDocument.BloodGroup;
        txt_DocumentName.Text = MDocument.DocumentName; 
        UtilityManager um=new UtilityManager ();
        if (MDocument.ImagePath.Trim() == "")
        {
        img_Medical.ImageUrl = um.DownloadFileFromServer("noimage.jpg", "M");
        }
        else
        {
        img_Medical.ImageUrl = um.DownloadFileFromServer(MDocument.ImagePath.Trim(), "M");
        }
        pnl_Medical_Details.Visible = true;
        if (Mess.Trim()!="")
        {
            this.lblMessage.Text = "The Status Of Following Has Been Changed To In-Active <br/>" + Mess.Substring(1);
            lblMessage.Visible = true; 
        }
   
    }
    // VIEW THE RECORD
    protected void GridView_Medical_SelectedIndexChanged(object sender, EventArgs e)
    {
        HiddenField hfd,hfd1;
        MedicalDetails medicaldetails = new MedicalDetails();
        ProcessSelectMedicalDocumentDetails selectmedicaldetails = new ProcessSelectMedicalDocumentDetails();
        hfd = (HiddenField)GridView_Medical.Rows[GridView_Medical.SelectedIndex].FindControl("HiddenId");
        hfd1 = (HiddenField)GridView_Medical.Rows[GridView_Medical.SelectedIndex].FindControl("Hiddenfd1");
        medicaldetails.MedicalDetailsId = Convert.ToInt32(hfd.Value.ToString());
        medicaldetails.ImagePath = hfd1.Value.ToString();
        selectmedicaldetails.MedicalDetails = medicaldetails;
        selectmedicaldetails.Invoke();
        pnl_Medical_Details.Visible = true;
        Medical_Show_Record(selectmedicaldetails.MedicalDetails);
        btn_Medical_Add.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        btn_Medical_Save.Visible = false;
        btn_Medical_Cancel.Visible = true;
    }
    //EDIT THE RECORD
    //protected void GridView_Medical_Row_Editing(object sender, GridViewEditEventArgs e)
    //{
    //    HiddenField hfd, hfd1;
    //    MedicalDetails medicaldetails = new MedicalDetails();
    //    ProcessSelectMedicalDocumentDetails selectmedicaldetails = new ProcessSelectMedicalDocumentDetails();
    //    hfd = (HiddenField)GridView_Medical.Rows[e.NewEditIndex].FindControl("HiddenId");
    //    hfd1 = (HiddenField)GridView_Medical.Rows[e.NewEditIndex].FindControl("Hiddenfd1");
    //    medicaldetails.MedicalDetailsId = Convert.ToInt32(hfd.Value.ToString());
    //    medicaldetails.ImagePath = hfd1.Value.ToString();
    //    selectmedicaldetails.MedicalDetails = medicaldetails;
    //    selectmedicaldetails.Invoke();
    //    pnl_Medical_Details.Visible = true;
    //    Medical_Show_Record(selectmedicaldetails.MedicalDetails);
    //    btn_Medical_Save.Visible = true;
    //    btn_Medical_Add.Visible = btn_Add.Visible = (Auth.isAdd || Auth.isEdit) && (Mode == "Edit");  
    //    btn_Medical_Cancel.Visible = true;
    //}
    // DELETE THE RECORD
    protected void GridView_Medical_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int Mid=Convert.ToInt32(((HiddenField)GridView_Medical.Rows[e.RowIndex].FindControl("HiddenId")).Value);
        Delete_Other_Docs(4, Mid);

    }
    #endregion
    protected void GridView_Medical_DataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridView_Medical.Columns[1].Visible = false;
            GridView_Medical.Columns[2].Visible = false;
            // Can Add
            if (Auth.isAdd)
            {
            }

            // Can Modify
            if (Auth.isEdit)
            {
                GridView_Medical.Columns[1].Visible = ((Mode == "New") || (Mode == "Edit")) ? true : false;
            }

            // Can Delete
            if (Auth.isDelete)
            {
                GridView_Medical.Columns[2].Visible = ((Mode == "New") || (Mode == "Edit")) ? true : false;
            }

            // Can Print
            if (Auth.isPrint)
            {
            }
            Image img = ((Image)e.Row.FindControl("imgattach"));
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "ImagePath") == null)
                {

                    img.Visible = false;
                }
                else if (DataBinder.Eval(e.Row.DataItem, "ImagePath").ToString() == "")
                {

                    img.Visible = false;
                }
                else
                {
                    string appname = ConfigurationManager.AppSettings["AppName"].ToString();
                    img.Attributes.Add("onclick", "javascript:Show_Image_Large1('/"+ appname + "/EMANAGERBLOB/HRD/Documents/Medical/" + DataBinder.Eval(e.Row.DataItem, "ImagePath").ToString() + "');");
                    img.ToolTip = "Click to Preview";
                    img.Style.Add("cursor", "hand");
                }
            }
        }
        catch
        {
            GridView_Medical.Columns[1].Visible = false;
            GridView_Medical.Columns[2].Visible = false;
        }
    }   
    protected void btn_Medical_Cancel_Click(object sender, EventArgs e)
    {
       
        if (Mode == "New")
        {
            btn_Medical_Add.Visible = (Auth.isAdd || Auth.isEdit);
        }
        else if (Mode == "Edit")
        {
            btn_Medical_Add.Visible = (Auth.isAdd || Auth.isEdit);
        }
        btn_Medical_Cancel.Visible = false;
        pnl_Medical_Details.Visible = false;
        btn_Medical_Save.Visible = false;
        GridView_Medical.SelectedIndex = -1;
    }
    protected void btn_Medical_Add_Click(object sender, EventArgs e)
    {
        Hidden_Medical.Value = "";
        ddl_MedicalDocuments.SelectedIndex = 0; 
        txt_DocumentName.Text = "";
        txt_Medical_DocumentNumber.Text = "";
        txt_Medical_ExpiryDate.Text = "";
        txt_Medical_IssueDate.Text = "";
        txt_Medical_PlaceOfIssue.Text = "";
        img_expirydate.Visible = true;
        txt_Medical_ExpiryDate.Enabled = true;
        pnl_Medical_Details.Visible = true;
        btn_Medical_Save.Visible = true;
        btn_Medical_Cancel.Visible = true;
        btn_Medical_Add.Visible = false;
        UtilityManager um=new UtilityManager ();
        img_Medical.ImageUrl = um.DownloadFileFromServer("noimage.jpg", "M");
    }
    protected void btn_Medical_Save_Click1(object sender, EventArgs e)
    {
        MedicalDetails medicaldetails = new MedicalDetails();
        if (txt_Medical_ExpiryDate.Text.Trim() != "")
        {
            if (DateTime.Parse(txt_Medical_IssueDate.Text) > DateTime.Parse(txt_Medical_ExpiryDate.Text))
            {
                lblMessage.Text = "Issue date must be less or equal to expiry date.";
                return;
            }
        }
        try
        {
            if (Hidden_Medical.Value.ToString().Trim() == "")
            {
                medicaldetails.CreatedBy = Convert.ToInt32(Session["loginid"].ToString());

            }
            else
            {
                medicaldetails.MedicalDetailsId = Convert.ToInt32(Hidden_Medical.Value);
                medicaldetails.ModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
            }
                medicaldetails.CrewId = Convert.ToInt32(HiddenPK.Value);
                medicaldetails.DocumentTypeId = Convert.ToInt32(ddl_MedicalDocuments.SelectedValue);
                medicaldetails.DocumentName = txt_DocumentName.Text;
                medicaldetails.BloodGroup = txt_BloodGroup.Text;   
                medicaldetails.DocumentNumber = txt_Medical_DocumentNumber.Text;
                medicaldetails.IssueDate = txt_Medical_IssueDate.Text;
                medicaldetails.ExpiryDate = txt_Medical_ExpiryDate.Text;
                medicaldetails.PlaceOfIssue = txt_Medical_PlaceOfIssue.Text;

                if (this.FileUpload_Medical != null && this.FileUpload_Medical.FileContent.Length > 0)
                {
                    HttpPostedFile file = FileUpload_Medical.PostedFile;
                    UtilityManager um = new UtilityManager();
                    string fileName;
                    fileName = um.UploadFileToServer(this.FileUpload_Medical.PostedFile, hfd_Image4.Value.Trim(), "M");
                    if (fileName.StartsWith("?"))
                    {
                        lblMessage.Text = fileName.Substring(1);
                        return;
                    }
                    medicaldetails.ImagePath = fileName;
                }
                else
                {
                    medicaldetails.ImagePath = hfd_Image4.Value;
                }
                
                ProcessAddCrewMemberMedicalDocumentDetails memberdocumentdetails = new ProcessAddCrewMemberMedicalDocumentDetails();
                memberdocumentdetails.MedicalDetails = medicaldetails;
                memberdocumentdetails.Invoke();
                BindMedicalDetailsGrid(Convert.ToInt32(HiddenPK.Value));
                btn_Medical_Add_Click(sender, e);
                lblMessage.Text = "Record Successfully Saved.";
        }
        catch (Exception se)
        {
            lblMessage.Text = "Record Not Saved.";
            //Response.Write(se.Message.ToString());
        }
    }
    protected void GridView_Medical_PreRender(object sender, EventArgs e)
    {
        if (GridView_Medical.Rows.Count <= 0) { lbl_GridView_Medical.Text = "No Records Found..!"; }
        HiddenField hfd;
        ImageButton imgbtn;
        int i;
        for (i = 0; i <= GridView_Medical.Rows.Count - 1; i++)
        {
            hfd = (HiddenField)GridView_Medical.Rows[i].FindControl("hiddenMvalue");
            imgbtn = (ImageButton)GridView_Medical.Rows[i].FindControl("ImageButton7");
            if (hfd != null)
            {
                int k = Convert.ToInt32(hfd.Value);
                if (k == 0)
                {
                    GridView_Medical.Rows[i].BackColor = System.Drawing.Color.FromName("#fcc2bc");
                    imgbtn.Enabled = true;
                }
                else
                {
                    imgbtn.Enabled = false;
                }
            }
        }
    }    
        // Part -2(PCI Medical History)
    private void binddata(int id)
    {
        CrewMemberMedicalHistory CrewMemberMedicalHistory = new CrewMemberMedicalHistory();
        CrewMemberMedicalHistory.CrewId = id;
        ProcessCrewMemberMedicalDetailsSelectData getmedicaldetails = new ProcessCrewMemberMedicalDetailsSelectData();
        getmedicaldetails.crewmedicalhistory = CrewMemberMedicalHistory;
        getmedicaldetails.Invoke();
        //gvMedical.DataSource = getmedicaldetails.ResultSet;
        //gvMedical.DataBind();
        //gvMedical.SelectedIndex = -1;
    }
    #region RECROD DISPLAY AREA
    //private void showdata(int i)
    //{
    //    string Mess = "";
    //    this.txtmedicalcasedate.Text = Alerts.FormatDate(this.gvMedical.Rows[i].Cells[3]);
    //    HiddenField hfvessel;
    //    hfvessel = ((HiddenField)this.gvMedical.Rows[i].FindControl("HvesselId"));

    //    HiddenField hfport;
    //    hfport = ((HiddenField)this.gvMedical.Rows[i].FindControl("Hportid"));
 
    //    //****** To Get Country According To Port
    //    DataTable dtcountry = PortPlanner.selectCountry(Convert.ToInt32(hfport.Value));
     
    //    foreach (DataRow drr in dtcountry.Rows)
    //    {
    //        //ddlCountry.SelectedValue = drr["CountryId"].ToString();
    //        Mess = Mess + Alerts.Set_DDL_Value(ddlCountry, drr["CountryId"].ToString(), "Country");
    //        BindPortDropDown();
    //    }


    //    //***********
    //    //this.ddmedicalport.SelectedValue = hfport.Value;
    //    Mess = Mess + Alerts.Set_DDL_Value(ddmedicalport, hfport.Value, "Port");

    //    //this.ddmedicalvessel.SelectedValue = hfvessel.Value;
    //    Mess = Mess + Alerts.Set_DDL_Value(ddmedicalvessel, hfvessel.Value, "Vessel");

    //    this.txtmedicalcasenumber.Text = this.gvMedical.Rows[i].Cells[6].Text;
    //    if (this.gvMedical.Rows[i].Cells[7].Text == "Open")
    //    {
    //        this.ddmedicalstatus.SelectedValue = "O";
    //    }
    //    else
    //    {
    //        this.ddmedicalstatus.SelectedValue = "C";
    //    }
    //    this.txtmedicalamount.Text = this.gvMedical.Rows[i].Cells[8].Text.Trim();
    //    this.txtmedicaldescription.Text = this.gvMedical.Rows[i].Cells[9].Text.Trim();
    //    //------------------
    //    if (Mess.Length > 0)
    //    {
    //        this.lblMessage.Text = "The Status Of Following Has Been Changed To In-Active <br/>" + Mess.Substring(1);
    //    }

    //}
    //protected void gvMedical_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    int j;
    //    j = gvMedical.SelectedIndex;
    //    showdata(j);
    //    this.btn_PCI_Save.Visible = false;
    //    this.btn_PCI_Cancel.Visible = true;
    //    this.btn_PCI_Add.Visible = ((Mode == "Edit") || (Mode == "New"));
    //    trfields.Visible = true;
    //    this.hmedicalid.Value = gvMedical.DataKeys[j].Value.ToString();
    //}
    //protected void gvMedical_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    int j;
    //    j = e.NewEditIndex;
    //    showdata(j);
    //    this.hmedicalid.Value = gvMedical.DataKeys[j].Value.ToString();
    //    this.btn_PCI_Save.Visible = true;
    //    this.btn_PCI_Cancel.Visible = true;
    //    trfields.Visible = true;
    //}
    //protected void gvMedical_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    CrewMemberMedicalHistory CrewMemberMedicalHistory = new CrewMemberMedicalHistory();
    //    CrewMemberMedicalHistory.MedicalCaseId = Convert.ToInt32(gvMedical.DataKeys[e.RowIndex].Value.ToString());
    //    ProcessDeleteCrewMembersMedicalHistoryData deletedata = new ProcessDeleteCrewMembersMedicalHistoryData();
    //    deletedata.CrewMemberMedicalHistory = CrewMemberMedicalHistory;
    //    deletedata.Invoke();
    //    binddata(Convert.ToInt32(HiddenPK.Value));
    //    if (hmedicalid.Value.ToString() == CrewMemberMedicalHistory.MedicalCaseId.ToString())
    //    {
    //        btn_PCI_Add_Click(sender, e);
    //    }
    //}

    #endregion
    //protected void btn_PCI_Save_Click(object sender, EventArgs e)
    //{
    //    CrewMemberMedicalHistory CrewMemberMedicalHistory = new CrewMemberMedicalHistory();
        
        
    //    // MAKE HERE THE CKECH FOR CASE DATE IS ONLY WHEN THE CREW IS ON BOARD

    //    try
    //    {
    //        if (this.hmedicalid.Value == "")
    //        {
    //            CrewMemberMedicalHistory.MedicalCaseId = -1;
    //            CrewMemberMedicalHistory.CreatedBy = Convert.ToInt32(Session["loginid"].ToString());
    //        }
    //        else
    //        {
    //            CrewMemberMedicalHistory.MedicalCaseId = Convert.ToInt32(this.hmedicalid.Value);
    //            CrewMemberMedicalHistory.Modifiedby = Convert.ToInt32(Session["loginid"].ToString());
    //        }
    //        CrewMemberMedicalHistory.CrewId = Convert.ToInt32(HiddenPK.Value);
    //        CrewMemberMedicalHistory.CaseDate = Convert.ToDateTime(this.txtmedicalcasedate.Text);
    //        CrewMemberMedicalHistory.VesselId = Convert.ToInt32(this.ddmedicalvessel.SelectedValue);
    //        CrewMemberMedicalHistory.PortId = Convert.ToInt32(this.ddmedicalport.SelectedValue);
    //        //CrewMemberMedicalHistory.VesselId = 1;
    //        //CrewMemberMedicalHistory.PortId = 1;
    //        CrewMemberMedicalHistory.CaseNumber = txtmedicalcasenumber.Text;
    //        CrewMemberMedicalHistory.CaseStatus = Convert.ToChar(this.ddmedicalstatus.SelectedValue);

    //        CrewMemberMedicalHistory.Amount = this.txtmedicalamount.Text;
    //        CrewMemberMedicalHistory.Description = this.txtmedicaldescription.Text;

    //        ProcessAddCrewMemberMedicalCaseHistory adddetails = new ProcessAddCrewMemberMedicalCaseHistory();
    //        adddetails.CrewMemberMedicalHistory = CrewMemberMedicalHistory;
    //        adddetails.Invoke();
    //        binddata(Convert.ToInt32(HiddenPK.Value));
    //        lblMessage.Text = "Record Successfully Saved.";
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMessage.Text = "Record Not Saved.";
    //        //Response.Write(ex.Message.ToString());
    //    }
        

    //    this.hmedicalid.Value = "";
    //    this.txtmedicalcasedate.Text = "";
    //    this.txtmedicalcasenumber.Text = "";
    //    this.txtmedicaldescription.Text = "";
    //    this.txtmedicalamount.Text = "";
    //    this.ddmedicalstatus.SelectedIndex = 0;
    //    this.ddmedicalvessel.SelectedIndex = 0;
    //    this.ddmedicalport.SelectedIndex = 0;
    //    this.ddlCountry.SelectedIndex = 0;
    //}
    //protected void btn_PCI_Add_Click(object sender, EventArgs e)
    //{
    //    this.hmedicalid.Value = "";
    //    this.txtmedicalcasedate.Text = "";
    //    this.txtmedicalcasenumber.Text = "";
    //    this.txtmedicaldescription.Text = "";
    //    this.txtmedicalamount.Text = "";
    //    this.ddmedicalstatus.SelectedIndex = 0;
    //    this.ddmedicalvessel.SelectedIndex = 0;
    //    this.ddlCountry.SelectedIndex = 0;
    //    this.ddmedicalport.SelectedIndex = 0;
    //    this.btn_PCI_Save.Visible = true;
    //    this.btn_PCI_Cancel.Visible = true;
    //    this.btn_PCI_Add.Visible = false;
    //    trfields.Visible = true;
    //}
    //protected void btn_PCI_Cancel_Click(object sender, EventArgs e)
    //{
    //    this.btn_PCI_Save.Visible = false;
    //    this.btn_PCI_Cancel.Visible = false;
    //    this.btn_PCI_Add.Visible = ((Mode == "Edit") || (Mode == "New"));
    //    trfields.Visible = false;
    //}
    //protected void gvMedical_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    try
    //    {
    //        gvMedical.Columns[1].Visible = false;
    //        gvMedical.Columns[2].Visible = false;
    //        // Can Add
    //        if (Auth.isAdd)
    //        {
    //        }

    //        // Can Modify
    //        if (Auth.isEdit)
    //        {
    //            gvMedical.Columns[1].Visible = ((Mode == "New") || (Mode == "Edit")) ? true : false;
    //        }

    //        // Can Delete
    //        if (Auth.isDelete)
    //        {
    //            gvMedical.Columns[2].Visible = ((Mode == "New") || (Mode == "Edit")) ? true : false;
    //        }

    //        // Can Print
    //        if (Auth.isPrint)
    //        {
    //        }

    //    }
    //    catch
    //    {

    //    }
    //}
    //protected void gvMedical_PreRender(object sender, EventArgs e)
    //{
       
    //    if (gvMedical.Rows.Count <= 0)
    //    {
    //        lbl_medical_message.Text = "No Records Found..!";
    //    }
    //    else
    //    {
    //        lbl_medical_message.Text = "";
    //    }

    //}
#endregion 
    #region Tab-5 Archived Documents
    protected void ArchiveOthers(object sender, ImageClickEventArgs e)
    {
        

        string[,] DocsType = { 
                                {"Professional","Licence","~/EMANAGERBLOB/HRD/Documents/Professional/" },
                                {"Professional","Course & Certificate","~/EMANAGERBLOB/HRD/Documents/Professional/" },
                                {"Professional","Dangerous Cargo Endorsement","~/EMANAGERBLOB/HRD/Documents/Professional/" },
                                {"Professional","Others Documents","~/EMANAGERBLOB/HRD/Documents/Professional/" },
                                {"Medical","Medical Certificate","~/EMANAGERBLOB/HRD/Documents/Medical/" }
                             };

        int CrewId = Convert.ToInt32(Session["CrewId"].ToString());
        int LoginId = Convert.ToInt32(Session["loginid"].ToString());

        Control c = ((ImageButton)sender).Parent;

        HiddenField hfd = (HiddenField)c.FindControl("arc_DocType");
        int Pos = Convert.ToInt32(hfd.Value);

        hfd = (HiddenField)c.FindControl("arc_DocumentID");
        int DocId = Convert.ToInt32(hfd.Value);

        hfd = (HiddenField)c.FindControl("arc_DocumentName");
        string DocumentName = hfd.Value;

        hfd = (HiddenField)c.FindControl("arc_DocumentNumber");
        string DocumentNumber = hfd.Value;

        hfd = (HiddenField)c.FindControl("arc_IssueDate");
        string IssueDate = hfd.Value;

        hfd = (HiddenField)c.FindControl("arc_ExpDate");
        string ExpDate = hfd.Value;

        hfd = (HiddenField)c.FindControl("arc_FilePath");
        string FilePath = hfd.Value;

        string FullFilePath = Server.MapPath(DocsType[Pos, 2] + hfd.Value);

        byte[] Data = { };

        string FileName = "";
        try
        {
            Data = System.IO.File.ReadAllBytes(FullFilePath);
            FileName = System.IO.Path.GetFileName(FullFilePath); 
        }
        catch { }

        if (Alerts.ArchiveDocument(CrewId, DocsType[Pos, 0], DocsType[Pos, 1], DocumentName, DocumentNumber, IssueDate.Trim(), ExpDate.Trim(), FileName, Data, LoginId))
        {
            Delete_Other_Docs(Pos, DocId);
            lblMessage.Text = "Archived Successfully.";
        }
        else
        {
            lblMessage.Text = "Unable to Archive.";
        }
    }
    protected void Delete_Other_Docs(int DocType,int DocId)
    {
        switch (DocType)
        {
            case 0: // Licence
                {
                    CrewLicenseDetails crewlicensedetails = new CrewLicenseDetails();
                    crewlicensedetails.CrewLicenseId = DocId;
                    ProcessDeleteCrewLicenceDetails deletedata = new ProcessDeleteCrewLicenceDetails();
                    deletedata.LicenseDetails = crewlicensedetails;
                    deletedata.Invoke();
                    bindLicensedata(Convert.ToInt32(HiddenPK.Value));
                    if (h_Licenceid.Value.Trim() == crewlicensedetails.CrewLicenseId.ToString())
                    {
                        licensecleardata();
                    }
                }
                break;
            case 1: // Course & Certificate
                {
                    CrewCourseCertificateDetails crewcoursedetails = new CrewCourseCertificateDetails();
                    crewcoursedetails.CourseCertificateId = DocId;
                    ProcessDeleteCrewCourseCertificateDetails deletedata = new ProcessDeleteCrewCourseCertificateDetails();
                    deletedata.CertificateDetails = crewcoursedetails;
                    deletedata.Invoke();
                    bindcertificatedata(Convert.ToInt32(HiddenPK.Value));
                    if (hcertificateid.Value.Trim() == crewcoursedetails.CourseCertificateId.ToString())
                    {
                        clearcertificatedetails();
                    }
                }
                break;
            case 2: // Dangerous Cargo Endorsement
                {
                    CrewCargoDetails exp = new CrewCargoDetails();
                    ProcessDeleteCrewMemberByDangerousCargoId obj = new ProcessDeleteCrewMemberByDangerousCargoId();
                    exp.DangerousCargoId = Convert.ToInt32(DocId.ToString());
                    obj.CrewCargoDetails = exp;
                    obj.Invoke();

                    //---------------------------------
                    bindDCEGrid(Convert.ToInt32(HiddenPK.Value));
                    if (Hidden_cargo.Value.Trim() == DocId.ToString())
                    {
                        btn_cargo_Add_Click(new object(), new EventArgs());
                    }
                }
                break;
            case 3: // Others Documents
                {
                    CrewOtherDocumentsDetails crewotherdetails = new CrewOtherDocumentsDetails();
                    crewotherdetails.CrewOtherDocId = DocId;
                    ProcessDeleteCrewMemberOtherDocuments deletedata = new ProcessDeleteCrewMemberOtherDocuments();
                    deletedata.OtherDocumentDetails = crewotherdetails;
                    deletedata.Invoke();
                    bindotherdocdata(Convert.ToInt32(HiddenPK.Value));
                    if (h_otherdoc_id.Value.Trim() == crewotherdetails.CrewOtherDocId.ToString())
                    {
                        clearotherdocumentdetails();
                    }
                }
                break;
            case 4: // Medical Certificate
                {
                    MedicalDetails medicaldetails = new MedicalDetails();
                    ProcessDeleteCrewMemberMedicalDetailsById medicaldetailsbyid = new ProcessDeleteCrewMemberMedicalDetailsById();
                    medicaldetails.MedicalDetailsId = Convert.ToInt32(DocId.ToString());
                    medicaldetailsbyid.MedicalDetails = medicaldetails;
                    medicaldetailsbyid.Invoke();
                    BindMedicalDetailsGrid(Convert.ToInt32(HiddenPK.Value));
                    if (Hidden_Medical.Value.ToString() == DocId.ToString())
                    {
                        btn_Medical_Add_Click(new object(), new EventArgs());
                    }
                }
                break;
            case 5: // Trainging Certificate
                {
                    TrainingDocumentDetails trainingdocumentdetails = new TrainingDocumentDetails();
                    ProcessDeleteCrewMemberTrainingDetailsById trainingdetailsbyid = new ProcessDeleteCrewMemberTrainingDetailsById();
                    trainingdocumentdetails.TrainingDocumentId = Convert.ToInt32(DocId.ToString());
                    trainingdetailsbyid.TrainingDocumentDetails = trainingdocumentdetails;
                    trainingdetailsbyid.Invoke();
                    BindTrainingDetailsGrid(Convert.ToInt32(HiddenPK.Value));
                    if (Hidden_Training.Value.ToString() == DocId.ToString())
                    {
                        btnTrainingAdd_Click(new object(), new EventArgs());
                    }
                }
                break;
            default:
                break;
        }
    }
    
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        string TableId=((ImageButton)sender).CommandArgument ;
        string strSQL = "SELECT [FileName],[Attachment] FROM DBO.CrewArchivedDocuments WHERE TableId = " + TableId.Trim();
        DataTable dtRemarkDetails = Common.Execute_Procedures_Select_ByQuery(strSQL);
        byte[] buff = (byte[])dtRemarkDetails.Rows[0]["Attachment"];
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + dtRemarkDetails.Rows[0]["FileName"].ToString());
        Response.BinaryWrite(buff);
        Response.Flush();
        Response.End();
    }
    public void BindArchivedDocuments()
    {
        try
        {
            grdDocs.DataSource = Alerts.getArchivedDocuments(Convert.ToInt32(Session["CrewId"].ToString()));
            grdDocs.DataBind();
        }
        catch { }
    }
    #endregion
    protected void imgbtn_Personal_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("CrewDetails.aspx");  
    }
    protected void imgbtn_CRM_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("CrewCRMDocument.aspx");  
    }
    protected void imgbtn_Search_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("CrewActivities.aspx");  
    }
    //protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindPortDropDown();
    //}



    protected void b6_Click(object sender, EventArgs e)
    {
        Response.Redirect("CrewDetails.aspx");
    }

    protected void b7_Click(object sender, EventArgs e)
    {
        Response.Redirect("CrewCRMDocument.aspx");
    }
    #region  Tab-6 Appraisal Details
    private void bindapprasialdata(int i)
    {
        CrewApprasialDetails crewappdetails = new CrewApprasialDetails();
        crewappdetails.CrewId = Convert.ToInt16(HiddenPK.Value);
        crewappdetails.CrewAppraisalId = -1;
        ProcessCrewMemberAppraisalSelectData getappdata = new ProcessCrewMemberAppraisalSelectData();

        getappdata.ApprasialDetails = crewappdetails;
        getappdata.Invoke();

        this.gvApprasial.DataSource = getappdata.ResultSet;
        gvApprasial.DataBind();
        gvApprasial.SelectedIndex = -1;
    }
    //protected void gvApprasial_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    gvTrainingsWithAppraisal.DataSource = null;
    //    gvTrainingsWithAppraisal.DataBind();

    //    this.trapprasial.Visible = true;
    //    btn_Apprasial_Save.Visible = true;
    //    btn_Apprasial_Cancel.Visible = true;
    //    show_apprasial_data(e.NewEditIndex);

    //    this.h_apprasial_id.Value = gvApprasial.DataKeys[e.NewEditIndex].Value.ToString();
    //    btn_ShowTrainings_Click(sender, e);

    //}
    protected void gvApprasial_SelectedIndexChanged(object sender, EventArgs e)
    {
        int i;
        i = gvApprasial.SelectedIndex;
        show_apprasial_data(i);
        this.trapprasial.Visible = true;
        btn_Apprasial_Save.Visible = false;
        this.btn_Apprasial_Add.Visible = ((Mode == "Edit") || (Mode == "New"));
        btn_Apprasial_Cancel.Visible = true;

        this.h_apprasial_id.Value = gvApprasial.DataKeys[i].Value.ToString();
        gvTrainingsWithAppraisal.DataSource = null;
        gvTrainingsWithAppraisal.DataBind();
        btn_ShowTrainings_Click(sender, e);
    }
    protected void gvApprasial_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        CrewApprasialDetails crewappdet = new CrewApprasialDetails();
        crewappdet.CrewAppraisalId = Convert.ToInt16(this.gvApprasial.DataKeys[e.RowIndex].Value.ToString());
        ProcessDeleteCrewApprasialDetails deletedata = new ProcessDeleteCrewApprasialDetails();
        deletedata.ApprasialDetails = crewappdet;
        deletedata.Invoke();
        bindapprasialdata(Convert.ToInt16(HiddenPK.Value)); ;
        clearapprasialdetails();
    }
    protected void btn_Apprasial_Add_Click(object sender, EventArgs e)
    {
        this.trapprasial.Visible = true;
        btn_Apprasial_Add.Visible = false;
        btn_Apprasial_Save.Visible = true;
        btn_Apprasial_Cancel.Visible = true;
        clearapprasialdetails();

    }
    private void clearapprasialdetails()
    {
        this.h_apprasial_id.Value = "";
        this.ddl_apprasial_occasion.SelectedIndex = 0;
        ddl_Vessel.SelectedIndex = 0;
        chk_Recommended.Checked = false;
        this.txt_Apprasial_Score.Text = "";
        this.txt_Apprasial_remarks.Text = "";
        this.txt_Apprasial_OfficeRemarks.Text = "";
        this.txt_Apprasiee_remarks.Text = "";
        this.txt_apprasial_from.Text = "";
        this.txt_Apprasial_To.Text = "";
        this.img_Apprasial.ImageUrl = "";
        txt_PerfScore.Text = "";
        txt_CompScore.Text = "";
        txt_ReportNo.Text = "";
        ddl_PromoRecomm.SelectedIndex = 0;
        ddl_TrainingReq.SelectedIndex = 0;
        ddl_PromoRecomm.SelectedValue = "";
        ddl_TrainingReq.SelectedValue = "";
        UtilityManager um = new UtilityManager();
        img_Apprasial.ImageUrl = um.DownloadFileFromServer("noimage.jpg", "P");
        gvTrainingsWithAppraisal.Visible = false;
    }
    protected void btn_Apprasial_Save_Click(object sender, EventArgs e)
    {
        CrewApprasialDetails crewappraisal = new CrewApprasialDetails();
        if (txt_Apprasial_To.Text.Trim() != "")
        {
            if (DateTime.Parse(txt_apprasial_from.Text) > DateTime.Parse(txt_Apprasial_To.Text))
            {
                lblMessage.Text = "Appraisal from must be less or equal to Todate.";
                return;
            }
        }
        try
        {

            if (this.h_apprasial_id.Value == "")
            {
                crewappraisal.CrewAppraisalId = -1;
                crewappraisal.CreatedBy = Convert.ToInt32(Session["loginid"].ToString());
            }
            else
            {
                crewappraisal.CrewAppraisalId = Convert.ToInt32(this.h_apprasial_id.Value);
                crewappraisal.ModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
            }
            crewappraisal.CrewId = Convert.ToInt16(HiddenPK.Value);
            crewappraisal.AppraisalOccasionId = Convert.ToInt16(this.ddl_apprasial_occasion.SelectedValue);
            try
            {
                crewappraisal.AverageMarks = Convert.ToDouble(this.txt_Apprasial_Score.Text);
            }
            catch { crewappraisal.AverageMarks = 0; }
            crewappraisal.ApprasialFrom = txt_apprasial_from.Text;
            crewappraisal.ApprasialTo = this.txt_Apprasial_To.Text;
            crewappraisal.AppraiserRemarks = this.txt_Apprasial_remarks.Text;
            crewappraisal.AppraiseeRemarks = this.txt_Apprasiee_remarks.Text;
            crewappraisal.OfficeRemarks = this.txt_Apprasial_OfficeRemarks.Text;
            crewappraisal.VesselId = Convert.ToInt32(ddl_Vessel.SelectedValue);
            crewappraisal.Recommended = (chk_Recommended.Checked) ? "Y" : "N";
            crewappraisal.N_Recommended = ddl_PromoRecomm.SelectedValue;
            crewappraisal.N_PerfScore = txt_PerfScore.Text; ;
            crewappraisal.N_CompScore = txt_CompScore.Text;
            crewappraisal.N_ReportNo = txt_ReportNo.Text;
            crewappraisal.N_TrainingRequired = ddl_TrainingReq.SelectedValue;

            string filename = "";
            if (this.Apprasial_fileupload.HasFile == true)
            {

                UtilityManager uml = new UtilityManager();
                if (this.h_apprasial_id.Value == "")
                {
                    filename = uml.UploadFileToServer(this.Apprasial_fileupload.PostedFile, "", "L");
                }
                else
                {
                    filename = uml.UploadFileToServer(this.Apprasial_fileupload.PostedFile, this.h_apprasial_hfile.Value, "L");
                }
                if (filename.StartsWith("?"))
                {
                    lblMessage.Text = filename.Substring(1);
                    return;
                }
            }

            if (this.h_apprasial_id.Value != "" && filename == "")
            {
                filename = h_apprasial_hfile.Value;
            }
            crewappraisal.ImagePath = filename;

            ProcessCrewMemberApprasialDetailsInsertData addapprasialdetails = new ProcessCrewMemberApprasialDetailsInsertData();

            addapprasialdetails.ApprasialDetails = crewappraisal;
            addapprasialdetails.Invoke();
            h_apprasial_id.Value = "";

            //*********PopUp****
            if (ddl_TrainingReq.SelectedValue == "Y")
            {
                Session.Add("N_CrewAppId", crewappraisal.CrewAppraisalId);
                ClientScript.RegisterClientScriptBlock(Page.GetType(), "TraingPopUp", "<script language='JavaScript'>window.open('TrainingEntryPopUp.aspx','hhhh','title=no,resizable=no,location=no,width=800px,height=300px,top=190px,left=260px,addressbar=no,status=yes,scrollbars=yes')</script>");
            }
            //******************
            lblMessage.Text = "Record Successfully Saved.";
        }


        catch (Exception es)
        {
            lblMessage.Text = "Record Not Saved.";
        }

        bindapprasialdata(Convert.ToInt16(HiddenPK.Value)); ;
        clearapprasialdetails();
    }
    protected void btn_Apprasial_Cancel_Click(object sender, EventArgs e)
    {
        btn_Apprasial_Save.Visible = false;
        btn_Apprasial_Cancel.Visible = false;
        // this.btn_Add.Visible = true;
        this.btn_Apprasial_Add.Visible = ((Mode == "Edit") || (Mode == "New"));
        trapprasial.Visible = false;
    }
    private void show_apprasial_data(int i)
    {
        HiddenField lblid = ((HiddenField)gvApprasial.Rows[i].FindControl("HdocId"));

        this.ddl_apprasial_occasion.SelectedValue = lblid.Value;

        CrewApprasialDetails crewappdetails = new CrewApprasialDetails();
        crewappdetails.CrewId = Convert.ToInt16(HiddenPK.Value);
        crewappdetails.CrewAppraisalId = Convert.ToInt16(gvApprasial.DataKeys[i].Value.ToString());
        ProcessCrewMemberAppraisalSelectData getappdata = new ProcessCrewMemberAppraisalSelectData();

        getappdata.ApprasialDetails = crewappdetails;
        getappdata.Invoke();
        ddl_apprasial_occasion.SelectedValue = Convert.ToString(crewappdetails.AppraisalOccasionId);
        txt_Apprasial_Score.Text = Convert.ToString(crewappdetails.AverageMarks);
        txt_apprasial_from.Text = Alerts.FormatDate(crewappdetails.ApprasialFrom);
        txt_Apprasial_To.Text = Alerts.FormatDate(crewappdetails.ApprasialTo);
        txt_Apprasial_OfficeRemarks.Text = crewappdetails.OfficeRemarks;
        txt_Apprasial_remarks.Text = crewappdetails.AppraiserRemarks;
        txt_Apprasiee_remarks.Text = crewappdetails.AppraiseeRemarks;
        ddl_Vessel.SelectedValue = crewappdetails.VesselId.ToString();
        chk_Recommended.Checked = (crewappdetails.Recommended == "Y");
        ddl_PromoRecomm.SelectedValue = crewappdetails.N_Recommended.Trim();
        txt_PerfScore.Text = crewappdetails.N_PerfScore;
        txt_CompScore.Text = crewappdetails.N_CompScore;
        txt_ReportNo.Text = crewappdetails.N_ReportNo;
        ddl_TrainingReq.SelectedValue = crewappdetails.N_TrainingRequired.Trim();
        lbl_DJC.Text = crewappdetails.N_DateJoinCompany;
        lbl_UpdatedBy.Text = crewappdetails.N_UpdatedBy;
        lbl_UpdatedOn.Text = Alerts.FormatDate(crewappdetails.N_UpdatedOn);

        HiddenField himgname = ((HiddenField)gvApprasial.Rows[i].FindControl("himag"));
        this.h_apprasial_hfile.Value = himgname.Value;
        UtilityManager um = new UtilityManager();
        if (this.h_apprasial_hfile.Value != "" || this.h_apprasial_hfile.Value == null)
        {

            this.img_Apprasial.ImageUrl = um.DownloadFileFromServer(h_apprasial_hfile.Value, "L");
        }
        else
        {
            img_Apprasial.ImageUrl = um.DownloadFileFromServer("noimage.jpg", "L");
        }
    }
    protected void gvApprasial_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            gvApprasial.Columns[1].Visible = false;
            gvApprasial.Columns[2].Visible = false;

            // Can Add
            if (Auth.isAdd)
            {
            }

            // Can Modify
            if (Auth.isEdit)
            {
                this.gvApprasial.Columns[1].Visible = ((Mode == "New") || (Mode == "Edit")) ? true : false;
            }

            // Can Delete
            if (Auth.isDelete)
            {
                gvApprasial.Columns[2].Visible = ((Mode == "New") || (Mode == "Edit")) ? true : false;
            }

            // Can Print
            if (Auth.isPrint)
            {
            }
            System.Web.UI.WebControls.Image img = ((System.Web.UI.WebControls.Image)e.Row.FindControl("imgattach"));
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "ImagePath") == null)
                {

                    img.Visible = false;
                }
                else if (DataBinder.Eval(e.Row.DataItem, "ImagePath").ToString() == "")
                {

                    img.Visible = false;
                }
                else
                {
                    string appname = ConfigurationManager.AppSettings["AppName"].ToString();
                    img.Attributes.Add("onclick", "javascript:Show_Image_Large1('/"+ appname + "/EMANAGERBLOB/HRD/Documents/Appraisal/" + DataBinder.Eval(e.Row.DataItem, "ImagePath").ToString() + "');");
                    img.ToolTip = "Click to Preview";
                    img.Style.Add("cursor", "hand");
                }

            }
        }
        catch
        {

        }
    }
    protected void gvApprasial_PreRender(object sender, EventArgs e)
    {
        if (this.gvApprasial.Rows.Count <= 0)
        {
            lbl_apprasial_message.Text = "No Records Found..!";
        }
        else
        {
            lbl_apprasial_message.Text = "";
        }
    }
    protected void btn_ShowTrainings_Click(object sender, EventArgs e)
    {
        if (h_apprasial_id.Value != "")
        {
            gvTrainingsWithAppraisal.Visible = true;
            DataTable dt311 = Training.selectDataTrainingDetailsByAppraisalId(Convert.ToInt32(HiddenPK.Value), Convert.ToInt32(h_apprasial_id.Value));
            if (dt311.Rows.Count > 0)
            {
                this.gvTrainingsWithAppraisal.DataSource = dt311;
                this.gvTrainingsWithAppraisal.DataBind();
            }
        }
    }
    private void getapprasial()
    {
        ProcessGetApprasialOccasion appocc = new ProcessGetApprasialOccasion();

        try
        {
            appocc.Invoke();

        }
        catch (Exception se)
        {
            //Response.Write(se.Message.ToString());
        }

        this.ddl_apprasial_occasion.DataTextField = "AppraisalOccasionName";
        this.ddl_apprasial_occasion.DataValueField = "AppraisalOccasionId";
        this.ddl_apprasial_occasion.DataSource = appocc.ResultSet;
        this.ddl_apprasial_occasion.DataBind();


    }
    private void Load_Vesssel()
    {
        DataSet ds = cls_SearchReliever.getMasterData("Vessel", "VesselId", "VesselName", Convert.ToInt32(Session["loginid"].ToString()));
        this.ddl_Vessel.DataTextField = "VesselName";
        this.ddl_Vessel.DataValueField = "VesselId";
        this.ddl_Vessel.DataSource = ds;
        this.ddl_Vessel.DataBind();
        ddl_Vessel.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< Select >", "0"));
    }
    #region Old Data


    #endregion
    #endregion

    protected void gvDocument_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int Rowindx = row.RowIndex;
            HiddenField hfd, hfd11;
            TravelDocumentDetails documentdetails = new TravelDocumentDetails();
            ProcessSelectTravelDocumentDetailsById documentdetailsbyid = new ProcessSelectTravelDocumentDetailsById();
            hfd = (HiddenField)gvDocument.Rows[Rowindx].FindControl("HiddenId");
            hfd11 = (HiddenField)gvDocument.Rows[Rowindx].FindControl("Hiddenfd11");
            documentdetails.TravelDocumentId = Convert.ToInt32(hfd.Value.ToString());
            documentdetails.ImagePath = hfd11.Value.ToString();
            documentdetailsbyid.DocumentDetails = documentdetails;
            documentdetailsbyid.Invoke();
            pnl_Travel.Visible = true;
            Show_Record(documentdetailsbyid.DocumentDetails);
            btn_Save.Visible = true;
            btn_Add.Visible = false;
            btn_Cancel.Visible = true;

            gvDocument.EditIndex = -1;
            bindDocumentGrid(Convert.ToInt32(HiddenPK.Value));
        }
    }

    protected void gvDocument_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvDocument.EditIndex = -1;
        bindDocumentGrid(Convert.ToInt32(HiddenPK.Value));
    }

    protected void gvDocument_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        gvDocument.EditIndex = -1;
        bindDocumentGrid(Convert.ToInt32(HiddenPK.Value));
    }



    protected void gvLicense_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int Rowindx = row.RowIndex;
            this.trLicensefields.Visible = true;
            this.btn_License_Save.Visible = true;
            this.btn_License_cancel.Visible = true;
            btn_License_Add.Visible = false;
            licenseshowdata(Rowindx);
           // this.btn_License_Add.Visible = ((Mode == "Edit") || (Mode == "New"));
            h_Licenceid.Value = gvLicense.DataKeys[Rowindx].Value.ToString();
        }
    }

    protected void gvLicense_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    protected void gvCourseCertificate_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int Rowindx = row.RowIndex;
            this.trcertificatefields.Visible = true;
            this.btn_Certifiacte_Save.Visible = true;
            this.btn_Certificate_Cancel.Visible = true;
            btn_Certificate_Add.Visible = false;
            show_certificate_data(Rowindx);
            hcertificateid.Value = gvCourseCertificate.DataKeys[Rowindx].Value.ToString();
            //this.btn_Certificate_Add.Visible = ((Mode == "Edit") || (Mode == "New"));
        }
    }

    protected void gvCourseCertificate_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    protected void GvDCE_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int Rowindx = row.RowIndex;
            HiddenField hfddce;
            HiddenField hfddceimage;
            CrewCargoDetails cargoDetails = new CrewCargoDetails();
            ProcessSelectCrewMemberDangerousCargoDetails selectcargodetails = new ProcessSelectCrewMemberDangerousCargoDetails();
            hfddce = (HiddenField)GvDCE.Rows[Rowindx].FindControl("HiddenIdcargo");
            hfddceimage = (HiddenField)GvDCE.Rows[Rowindx].FindControl("Hiddenimage");
            cargoDetails.DangerousCargoId = Convert.ToInt32(hfddce.Value.ToString());
            cargoDetails.ImagePath = hfddceimage.Value.ToString();
            selectcargodetails.CrewCargoDetails = cargoDetails;
            selectcargodetails.Invoke();

            //--------------------
            Show_Record_DCE_Details(selectcargodetails.CrewCargoDetails);
            GvDCE.SelectedIndex = Rowindx;
            btn_cargo_cancel.Visible = true;
            btn_cargo_save.Visible = true;
            btn_cargo_Add.Visible = false;
        }
    }

    protected void GvDCE_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    protected void gvotherdocument_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int Rowindx = row.RowIndex;
            this.trotherdoc.Visible = true;
            btn_Otherdoc_Save.Visible = true;
            btn_Otherdoc_Cancel.Visible = true;
            btn_otherdoc_Add.Visible = false;
            show_otherdoc_data(Rowindx);

            this.h_otherdoc_id.Value = gvotherdocument.DataKeys[Rowindx].Value.ToString();
        }
    }

    protected void gvotherdocument_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    protected void GridView_Academic_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int Rowindx = row.RowIndex;
            btn_Cancel_Academic_Click(btn_Cancel_Academic, null);
            HiddenField hfd, hfd22;
            AcademicDetails AcademicDetails = new AcademicDetails();
            ProcessSelectCrewMemberAcademicDetails selectacademicdetails = new ProcessSelectCrewMemberAcademicDetails();
            hfd = (HiddenField)GridView_Academic.Rows[Rowindx].FindControl("HiddenId");
            hfd22 = (HiddenField)GridView_Academic.Rows[Rowindx].FindControl("Hiddenfd22");
            AcademicDetails.AcademicDetailsId = Convert.ToInt32(hfd.Value.ToString());
            AcademicDetails.ImagePath = hfd22.Value.ToString();
            selectacademicdetails.AcademicDetails = AcademicDetails;
            selectacademicdetails.Invoke();

            //--------------------
            Show_Record_Academic_Details(selectacademicdetails.AcademicDetails);
            GridView_Academic.SelectedIndex = Rowindx;
            btn_Save_Academic.Visible = true;
            btn_Add_Academic.Visible = false;// (Auth.IsAdd || Auth.IsEdit) && (Mode == "Edit");
            btn_Cancel_Academic.Visible = true;
        }
    }

    protected void GridView_Academic_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    protected void GridView_Medical_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int Rowindx = row.RowIndex;
            HiddenField hfd, hfd1;
            MedicalDetails medicaldetails = new MedicalDetails();
            ProcessSelectMedicalDocumentDetails selectmedicaldetails = new ProcessSelectMedicalDocumentDetails();
            hfd = (HiddenField)GridView_Medical.Rows[Rowindx].FindControl("HiddenId");
            hfd1 = (HiddenField)GridView_Medical.Rows[Rowindx].FindControl("Hiddenfd1");
            medicaldetails.MedicalDetailsId = Convert.ToInt32(hfd.Value.ToString());
            medicaldetails.ImagePath = hfd1.Value.ToString();
            selectmedicaldetails.MedicalDetails = medicaldetails;
            selectmedicaldetails.Invoke();
            pnl_Medical_Details.Visible = true;
            Medical_Show_Record(selectmedicaldetails.MedicalDetails);
            btn_Medical_Save.Visible = true;
            btn_Medical_Add.Visible = false;// btn_Add.Visible = (Auth.IsAdd || Auth.IsEdit) && (Mode == "Edit");
            btn_Medical_Cancel.Visible = true;
        }
    }

    protected void GridView_Medical_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    protected void gvApprasial_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int Rowindx = row.RowIndex;
            gvTrainingsWithAppraisal.DataSource = null;
            gvTrainingsWithAppraisal.DataBind();

            this.trapprasial.Visible = true;
            btn_Apprasial_Save.Visible = true;
            btn_Apprasial_Cancel.Visible = true;
            show_apprasial_data(Rowindx);
            btn_Apprasial_Add.Visible = false;

            this.h_apprasial_id.Value = gvApprasial.DataKeys[Rowindx].Value.ToString();

            //btn_ShowTrainings_Click(sender, e);

            gvApprasial.EditIndex = -1;
            bindapprasialdata(Convert.ToInt16(HiddenPK.Value));
        }
    }

    protected void gvApprasial_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvApprasial.EditIndex = -1;
        bindapprasialdata(Convert.ToInt16(HiddenPK.Value));
    }



    protected void Gv_Training_PreRender(object sender, EventArgs e)
    {
        if (Gv_Training.Rows.Count <= 0) { lbl_Grid_Training.Text = "No Records Found..!"; }
        HiddenField hfd;
      //  ImageButton imgbtn;
        int i;
        for (i = 0; i <= Gv_Training.Rows.Count - 1; i++)
        {
            hfd = (HiddenField)Gv_Training.Rows[i].FindControl("hdnTrainginExpired");
            //imgbtn = (ImageButton)Gv_Training.Rows[i].FindControl("ImageButton7");
            if (hfd != null)
            {
                int k = Convert.ToInt32(hfd.Value);
                if (k == 0)
                {
                    Gv_Training.Rows[i].BackColor = System.Drawing.Color.FromName("#fcc2bc");
                    //imgbtn.Enabled = true;
                }
                else
                {
                   // imgbtn.Enabled = false;
                }
            }
        }
    }

    protected void Gv_Training_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }

    protected void Gv_Training_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Modify")
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int Rowindx = row.RowIndex;
            HiddenField hfd, hfd1;
            TrainingDocumentDetails trainingdocdetails = new TrainingDocumentDetails();
            ProcessSelectTrainingDocumentDetails selecttrainingdetails = new ProcessSelectTrainingDocumentDetails();
            hfd = (HiddenField)Gv_Training.Rows[Rowindx].FindControl("hdnTrainingDocid");
            hfd1 = (HiddenField)Gv_Training.Rows[Rowindx].FindControl("hdnDocPath");
            trainingdocdetails.TrainingDocumentId = Convert.ToInt32(hfd.Value.ToString());
            trainingdocdetails.ImagePath = hfd1.Value.ToString();
            selecttrainingdetails.TrainingDocumentDetails = trainingdocdetails;
            selecttrainingdetails.Invoke();
            pnl_Training_Details.Visible = true;
            Training_Show_Record(selecttrainingdetails.TrainingDocumentDetails);
            btnTrainingSave.Visible = true;
            btnTrainingAdd.Visible = false;// btn_Add.Visible = (Auth.IsAdd || Auth.IsEdit) && (Mode == "Edit");
            btnTrainingCancel.Visible = true;
        }
    }

    protected void Gv_Training_DataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            Gv_Training.Columns[1].Visible = false;
            Gv_Training.Columns[2].Visible = false;
            // Can Add
            if (Auth.isAdd)
            {
            }

            // Can Modify
            if (Auth.isEdit)
            {
                Gv_Training.Columns[1].Visible = ((Mode == "New") || (Mode == "Edit")) ? true : false;
            }

            // Can Delete
            if (Auth.isDelete)
            {
                Gv_Training.Columns[2].Visible = ((Mode == "New") || (Mode == "Edit")) ? true : false;
            }

            // Can Print
            if (Auth.isPrint)
            {
            }
            Image img = ((Image)e.Row.FindControl("imgattach"));
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (DataBinder.Eval(e.Row.DataItem, "ImagePath") == null)
                {

                    img.Visible = false;
                }
                else if (DataBinder.Eval(e.Row.DataItem, "ImagePath").ToString() == "")
                {

                    img.Visible = false;
                }
                else
                {
                    string appname = ConfigurationManager.AppSettings["AppName"].ToString();
                    img.Attributes.Add("onclick", "javascript:Show_Image_Large1('/" + appname + "/EMANAGERBLOB/HRD/Documents/TrainingCertificates/" + DataBinder.Eval(e.Row.DataItem, "ImagePath").ToString() + "');");
                    img.ToolTip = "Click to Preview";
                    img.Style.Add("cursor", "hand");
                }
            }
        }
        catch
        {
            Gv_Training.Columns[1].Visible = false;
            Gv_Training.Columns[2].Visible = false;
        }
    }

    protected void Gv_Training_Row_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int Mid = Convert.ToInt32(((HiddenField)Gv_Training.Rows[e.RowIndex].FindControl("hdnTrainingDocid")).Value);
        Delete_Other_Docs(5, Mid);
    }

    protected void Gv_Training_SelectedIndexChanged(object sender, EventArgs e)
    {
        HiddenField hfd, hfd1;
        TrainingDocumentDetails trainingDetails = new TrainingDocumentDetails();
        ProcessSelectTrainingDocumentDetails selecttrainingdetails = new ProcessSelectTrainingDocumentDetails();
        hfd = (HiddenField)Gv_Training.Rows[Gv_Training.SelectedIndex].FindControl("hdnTrainingDocid");
        hfd1 = (HiddenField)Gv_Training.Rows[Gv_Training.SelectedIndex].FindControl("hdnDocPath");
        trainingDetails.TrainingDocumentId = Convert.ToInt32(hfd.Value.ToString());
        trainingDetails.ImagePath = hfd1.Value.ToString();
        selecttrainingdetails.TrainingDocumentDetails = trainingDetails;
        selecttrainingdetails.Invoke();
        pnl_Medical_Details.Visible = true;
        Training_Show_Record(selecttrainingdetails.TrainingDocumentDetails);
        btn_Medical_Add.Visible = ((Mode == "Edit") || (Mode == "New")) ? true : false;
        btn_Medical_Save.Visible = false;
        btn_Medical_Cancel.Visible = true;
    }

    protected void btnTrainingAdd_Click(object sender, EventArgs e)
    {
        Hidden_Training.Value = "";
        ddlTraining.SelectedIndex = 0;
        ddlInstitute.SelectedIndex = 0;
        txtTrainingName.Text = "";
        txtInstiteName.Text = "";
        txtFromDate.Text = "";
        txtToDate.Text = "";
        txtExpiryDt.Text = "";
        imgExpiryDt.Visible = true;
        //imgFromDate.Visible = true;
        //imgToDate.Visible = true;
        txtExpiryDate.Enabled = true;
        pnl_Training_Details.Visible = true;
        btnTrainingSave.Visible = true;
        btnTrainingCancel.Visible = true;
        btnTrainingAdd.Visible = false;
        UtilityManager um = new UtilityManager();
        Img_Training.ImageUrl = um.DownloadFileFromServer("noimage.jpg", "TC");
    }

    protected void btnTrainingSave_Click(object sender, EventArgs e)
    {
        TrainingDocumentDetails trainingdetails = new TrainingDocumentDetails();
        if (txtExpiryDate.Text.Trim() != "")
        {
            if (DateTime.Parse(txtToDate.Text) > DateTime.Parse(txtExpiryDate.Text))
            {
                lblMessage.Text = "To date must be less or equal to expiry date.";
                return;
            }
        }
        try
        {
            if (Hidden_Training.Value.ToString().Trim() == "")
            {
                trainingdetails.CreatedBy = Convert.ToInt32(Session["loginid"].ToString());

            }
            else
            {
                trainingdetails.TrainingDocumentId = Convert.ToInt32(Hidden_Training.Value);
                trainingdetails.ModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
            }
            trainingdetails.CrewId = Convert.ToInt32(HiddenPK.Value);
            trainingdetails.TrainingId = Convert.ToInt32(ddlTraining.SelectedValue);
            trainingdetails.InstituteId = Convert.ToInt32(ddlInstitute.SelectedValue);
            trainingdetails.FromDate = txtFromDate.Text;
            trainingdetails.ToDate = txtToDate.Text;
            trainingdetails.ExpiryDate = txtExpiryDt.Text;
            if (this.FU_Training != null && this.FU_Training.FileContent.Length > 0)
            {
                HttpPostedFile file = FU_Training.PostedFile;
                UtilityManager um = new UtilityManager();
                string fileName;
                fileName = um.UploadFileToServer(this.FU_Training.PostedFile, hdn_TrainingImgPath.Value.Trim(), "TC");
                if (fileName.StartsWith("?"))
                {
                    lblMessage.Text = fileName.Substring(1);
                    return;
                }
                trainingdetails.ImagePath = fileName;
            }
            else
            {
                trainingdetails.ImagePath = hdn_TrainingImgPath.Value;
            }

            ProcessAddCrewMemberTraningDocumetDetails memberdocumentdetails = new ProcessAddCrewMemberTraningDocumetDetails();
            memberdocumentdetails.TrainingDocumentDetails = trainingdetails;
            memberdocumentdetails.Invoke();
            BindTrainingDetailsGrid(Convert.ToInt32(HiddenPK.Value));
            btnTrainingAdd_Click(sender, e);
            lblMessage.Text = "Record Successfully Saved.";
        }
        catch (Exception se)
        {
            lblMessage.Text = "Record Not Saved.";
            //Response.Write(se.Message.ToString());
        }
    }

    protected void btnTrainingCancel_Click(object sender, EventArgs e)
    {
        if (Mode == "New")
        {
            btnTrainingAdd.Visible = (Auth.isAdd || Auth.isEdit);
        }
        else if (Mode == "Edit")
        {
            btnTrainingAdd.Visible = (Auth.isAdd || Auth.isEdit);
        }
        btnTrainingCancel.Visible = false;
        pnl_Training_Details.Visible = false;
        btnTrainingSave.Visible = false;
        Gv_Training.SelectedIndex = -1;
    }

    protected void Training_Show_Record(TrainingDocumentDetails TDocument)
    {
        string Mess = "";
        Hidden_Training.Value = TDocument.TrainingDocumentId.ToString();
        hdn_TrainingImgPath.Value = TDocument.ImagePath.ToString();
        Mess = Alerts.Set_DDL_Value(ddlTraining, TDocument.TrainingId.ToString(), "Training Name");
        Mess = Alerts.Set_DDL_Value(ddlInstitute, TDocument.InstituteId.ToString(), "Institute Name");
        //ddl_MedicalDocuments.SelectedValue = MDocument.DocumentTypeId.ToString();

        txtFromDate.Text = Alerts.FormatDate(TDocument.FromDate);
        txtToDate.Text = Alerts.FormatDate(TDocument.ToDate);
        txtExpiryDt.Text = Alerts.FormatDate(TDocument.ExpiryDate);
     
        txtTrainingName.Text = TDocument.TrainingName;
        txtInstiteName.Text = TDocument.InstituteName;
        UtilityManager um = new UtilityManager();
        if (TDocument.ImagePath.Trim() == "")
        {
            Img_Training.ImageUrl = um.DownloadFileFromServer("noimage.jpg", "TC");
        }
        else
        {
            Img_Training.ImageUrl = um.DownloadFileFromServer(TDocument.ImagePath.Trim(), "TC");
        }
        pnl_Training_Details.Visible = true;
        if (Mess.Trim() != "")
        {
            this.lblMessage.Text = "The Status Of Following Has Been Changed To In-Active <br/>" + Mess.Substring(1);
            lblMessage.Visible = true;
        }

    }

    public void BindTrainingDetailsGrid(int id)
    {
        ProcessSelectTrainingDocumentDetails processselecttrainingdetails = new ProcessSelectTrainingDocumentDetails();
        try
        {
            TrainingDocumentDetails obj = new TrainingDocumentDetails();
            obj.CrewId = id;
            processselecttrainingdetails.TrainingDocumentDetails = obj;
            obj.TrainingDocumentId = 0;
            processselecttrainingdetails.Invoke();
            Gv_Training.DataSource = processselecttrainingdetails.ResultSet.Tables[0];
            Gv_Training.DataBind();
        }
        catch { }

    }
}
