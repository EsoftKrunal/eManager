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
using Exl=Microsoft.Office.Interop.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

public partial class CrewCRMDocument : System.Web.UI.Page
{
    public Authority Auth;
    public string Mode;
    DataTable dtUser;
    //-------
    public bool TrainingDelete
    {
        set { ViewState["TrainingDelete"] = value; }
        get { return Convert.ToBoolean(ViewState["TrainingDelete"]); }
    }
    public System.Data.DataTable TrainingMatrix
    {
        get {
            DataTable dt=null;
            try
            {
                dt = (DataTable)Session["TrainingMatrix"];
            }catch 
            {}
            if (dt == null)
            {
                dt = new DataTable();
                dt.Columns.Add(new DataColumn("SNo", typeof(int)));
                dt.Columns.Add(new DataColumn("TReqId", typeof(int)));
                dt.Columns.Add(new DataColumn("TrainingId", typeof(int)));
                dt.Columns.Add(new DataColumn("TrainingName", typeof(string)));
                dt.Columns.Add(new DataColumn("NextDue", typeof(DateTime)));
                
            }
            Session["TrainingMatrix"] = dt;
            return dt;
        }
        set { Session["TrainingMatrix"] = value; }
    }

    #region PAGE CONTROLS LOADER
    private void gettrainingdata()
    {
        ProcessGetTrainingIdName gettraining = new ProcessGetTrainingIdName();
        try
        {
            gettraining.Invoke();
        }
        catch (Exception se)
        {
            //Response.Write(se.Message.ToString());
        }

        this.ddl_TrainingReq_Training.DataTextField = "TrainingName";
        this.ddl_TrainingReq_Training.DataValueField = "TrainingId";
        this.ddl_TrainingReq_Training.DataSource = gettraining.ResultSet;
        this.ddl_TrainingReq_Training.DataBind();
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
    private void getcrmcategory()
    {
        ProcessGetCRMCategory getcate = new ProcessGetCRMCategory();

        try
        {
            getcate.Invoke();

        }
        catch (Exception se)
        {
            //Response.Write(se.Message.ToString());
        }

        this.ddl_crm_category.DataTextField = "CRMCategoryName";
        this.ddl_crm_category.DataValueField = "CRMCategory";
        this.ddl_crm_category.DataSource = getcate.ResultSet;
        this.ddl_crm_category.DataBind();

    }
    private void Load_Vesssel()
    {
        DataSet ds = cls_SearchReliever.getMasterData("Vessel", "VesselId", "VesselName", Convert.ToInt32(Session["loginid"].ToString()));
        this.ddl_Vessel.DataTextField = "VesselName";
        this.ddl_Vessel.DataValueField = "VesselId";
        this.ddl_Vessel.DataSource = ds;
        this.ddl_Vessel.DataBind();
        ddl_Vessel.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< Select >","0")); 
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
    #endregion
    // General Code
    public string ContractStatus
    {
        set
        {
            ViewState["ContractStatus"] = value;
        }
        get
        {
            return ViewState["ContractStatus"].ToString();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMsg.Text = "";
        //Alerts.SetHelp(imgHelp);   
        this.img_Crew.Attributes.Add("onclick", "javascript:Show_Image_Large(this);");
        this.img_Apprasial.Attributes.Add("onclick", "javascript:Show_Image_Large(this);");

        lblMessage.Text = "";
        lblMsgWages.Text = "";
        lblLockbyOn.Text = "";
        lbl_crm.Text = "";
        string SQL = "";

        try
        {
            Mode = Session["Mode"].ToString();
            ProcessCheckAuthority Obj = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
            Obj.Invoke();
            Session["Authority"] = Obj.Authority;
            Auth = (Authority)Session["Authority"];

            //SQL = " Select * from dbo.UserMaster where LoginId = " + Convert.ToInt32(Session["loginid"].ToString());
            //dtUser = Common.Execute_Procedures_Select_ByQuery(SQL);

            //if (dtUser.Rows.Count > 0)
            //{
            //    foreach( DataRow dr in dtUser.Rows)
            //    {
            //        if (dr["SuperUser"] != null && dr["SuperUser"].ToString() == "Y")
            //        {
            //            btnUpdateWages.Enabled = true;
            //           // btnConRevision.Enabled = true;
            //        }
            //        else
            //        {
            //            btnUpdateWages.Enabled = false;
            //           // btnConRevision.Enabled = false;
            //        }
            //    }
            //}

        }
        catch { Response.Redirect("CrewSearch.aspx"); }
        //---------------
        try
        {
            ProcessCheckAuthority Obj = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
            Obj.InvokeByPage();
            btnNewVisit.Visible = Obj.Authority.isAdd;
            radD.Visible = Common.CastAsInt32(Session["loginid"].ToString()) == 1;

            AuthenticationManager auth = new AuthenticationManager(11,Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
            TrainingDelete = auth.IsDelete;
        }
        catch { }
        //---------------
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

        ////=========== Code for Checking the CRM Alert
        //try
        //{
        //    DataTable dtalert = Alerts.getCRMAlertofCrewMember(Session["CrewId"].ToString());
        //    foreach (DataRow dr in dtalert.Rows)
        //    {
        //        if (Convert.ToInt32(dr[0].ToString()) > 0)
        //        {
        //            crm_Alert.Text = "Alert";
        //        }
        //    }
        //}
        //catch
        //{
        //    crm_Alert.Text = "";
        //}
        ////===========

        //=========== Code for Checking the Document Alert
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
            BindCuisuine();
            BindTrainingInstitite();
            BindCrewContractTemplate();
            MultiView1.ActiveViewIndex = 0;
            HANDLE_AUTHORITY(0);
            bindgridCRM();
            Load_Vesssel();
            Load_wageScale();
            getapprasial();
            getcrmcategory();
            Show_Header();
            BindTrainingMatrix();
            ProcessSelectCrewMemberPersonalDetailsById obj = new ProcessSelectCrewMemberPersonalDetailsById();
            CrewMember Crew = new CrewMember();
            Crew.Id = Convert.ToInt32(HiddenPK.Value.Trim());
            obj.CrueMember = Crew;
            obj.Invoke();

            UtilityManager um = new UtilityManager();
            if (Crew.Photopath.Trim() == "")
            {
                img_Crew.ImageUrl = um.DownloadFileFromServer("noimage.jpg", "C");
            }
            else
            {
                img_Crew.ImageUrl = um.DownloadFileFromServer(Crew.Photopath, "C");
            }
            p1.Visible = true;
            p2.Visible = false;
            //ddl_TrainingReq_Training.Items.Clear();
            //ddl_TrainingReq_Training.Items.Insert(0, new ListItem("<Select>", "0"));
            Session["CRMCrewDoc"] = "0";
        }
        btn_CrewExt.Visible = (Alerts.getLocation() == "S") && ((Auth.isEdit || Auth.isAdd) && (Mode == "New" || Mode == "Edit"));
        BindTrainingMatrix();
        b1.CssClass = "btn1";
        b2.CssClass = "btn1";
        b3.CssClass = "btn1";
        //b6.CssClass = "btn1";
        if (Session["CRMCrewDoc"] == null)
        {
            Session["CRMCrewDoc"] = 0;
        }
        switch (Common.CastAsInt32(Session["CRMCrewDoc"]))
        {
            case 0:
                b1.CssClass = "selbtn";
                break;
            //case 1:
            //    b2.CssClass = "selbtn";
            //    break;
            case 2:
                b2.CssClass = "selbtn";
                break;
            case 3:
                b3.CssClass = "selbtn";
                break;
            default:
                break;
        }
    }
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
        txt_LastVessel.Text = Crew.CurrentVessel;
        txt_Status.Text = Crew.CrewStatus;
        txt_Passport.Text = Crew.PassportNo;
        lblName.Text = txt_FirstName.Text + " " + txt_MiddleName.Text + " " + txt_LastName.Text;
        lblCurrRank.Text = ddcurrentrank.Text;
        lblCurrentVessel.Text = txt_LastVessel.Text;
        lblStatus.Text = Crew.Status;
        lblAge.Text = Crew.Age;
        lblRankExp.Text = Crew.RankExp;

    }

    protected void btnManageTraining_Click(object sender, EventArgs e)
    {
	ScriptManager.RegisterStartupScript(Page, this.GetType(), "a", "window.open('../CrewOperation/ManageTraining1_Crew.aspx?crew=" + HiddenPK.Value.Trim() + "');", true);
    }
    protected void Menu1_MenuItemClick(object sender, EventArgs e)
    {
        b1.CssClass = "btn1";
        b2.CssClass = "btn1";
        b3.CssClass = "btn1";

        Button btn = (Button)sender;
        int i = Common.CastAsInt32(btn.CommandArgument);
        Session["CRMCrewDoc"] = i;
       // this.MultiView1.ActiveViewIndex = i;
        //for (; i < Menu1.Items.Count; i++)
        //{
        //    this.Menu1.Items[i].ImageUrl = this.Menu1.Items[i].ImageUrl.Replace("_a.", "_d.");
        //}
        //e.Item.ImageUrl = e.Item.ImageUrl.Replace("_d.", "_a.");
        if (i == 0)
        {
            this.MultiView1.ActiveViewIndex = i;
            b1.CssClass = "selbtn";
            HANDLE_AUTHORITY(0);
            bindgridCRM();
        }
        //if (i == 1)
        //{
        //    HANDLE_AUTHORITY(1);
        //}
        if (i == 2)
        {
            this.MultiView1.ActiveViewIndex = i;
            b2.CssClass = "selbtn";
            HANDLE_AUTHORITY(2);
        }
        if (i == 3)
        {
            this.MultiView1.ActiveViewIndex = i;
            b3.CssClass = "selbtn";
            HANDLE_AUTHORITY(3);
            rbhistory.SelectedIndex = 0;
           
        }
        //if (i == 5)
        //{

        //    HANDLE_AUTHORITY(5);

        //}
    }
    private void HANDLE_AUTHORITY(int index)
    {
        btn_Apprasial_Print.Visible = Auth.isPrint;
        lblContractTemplate.Visible = Auth.isPrint;
        ddl_ContractTemplate.Visible = Auth.isPrint;
        btn_License_Print.Visible = Auth.isPrint;
        btn_TrainingReq_Print.Visible = Auth.isPrint;
        btn_crm_Print.Visible = Auth.isPrint;
        if (Mode == "New")
        {
            //TAB-1(TRAVEL DETAILS)
            if (index == 0)
            {
                btn_crm_add.Visible = ((Auth.isEdit || Auth.isAdd) && (Mode == "New" || Mode == "Edit"));
                btn_CRM_save.Visible = false;
                btn_crm_Cancel.Visible = false;
                crmpanel.Visible = false;
            }

            //TAB-2 PROFESSIONAL DETAILS

            if (index == 1)
            {
                bindtrainingdata(Convert.ToInt16(HiddenPK.Value));
                gettrainingdata();
                BindTrainingTypeDropDown();
                this.btn_TrainingReq_Save.Visible = false;
                this.btn_TrainingReq_Cancel.Visible = false;
                this.btn_TrainingReq_Add.Visible = ((Auth.isEdit || Auth.isAdd) && (Mode == "New" || Mode == "Edit"));
                trtrainingreqfields.Visible = false;

            }

            //TAB-3 ACADEMIC DETAILS
            if (index == 2)
            {
                this.btn_Apprasial_Save.Visible = false;
                this.btn_Apprasial_Cancel.Visible = false;
                this.btn_Apprasial_Add.Visible = ((Auth.isEdit || Auth.isAdd) && (Mode == "New" || Mode == "Edit"));
                this.trapprasial.Visible = false;
                bindapprasialdata(Convert.ToInt16(HiddenPK.Value));
            }
            //TAB-4 History Tab
            if (index == 3)
            {
              //  this.hfcontractid.Value = "";
              //  this.trcontractdetails.Visible = false;
              //  Show_Contract_crew(Convert.ToInt16(HiddenPK.Value));
                Show_ExperienceSummary_crew(Convert.ToInt16(HiddenPK.Value));
                pnl_history_1.Visible = false;
                pnl_history_2.Visible = false;
                pnl_history_3.Visible = true;
                pnl_history_4.Visible = false;
              //  this.btnConRevision.Visible = Auth.isEdit ;
              //  btn_ContractSave.Visible = false;
              //  gv_Contract.Columns[1].Visible = ((Auth.isEdit || Auth.isAdd) && (Mode == "New" || Mode == "Edit")) && (Alerts.getLocation() == "S");
            }

        }
        else if (Mode == "Edit")
        {
            //TAB-1(TRAVEL DETAILS)
            if (index == 0)
            {
                btn_crm_add.Visible = ((Auth.isEdit || Auth.isAdd) && (Mode == "New" || Mode == "Edit"));
                btn_CRM_save.Visible = false;
                btn_crm_Cancel.Visible = false;
                crmpanel.Visible = false;

            }
            //TAB-2 PROFESSIONAL DETAILS

            if (index == 1)
            {
                bindtrainingdata(Convert.ToInt16(HiddenPK.Value));
                gettrainingdata();
                BindTrainingTypeDropDown();
                this.btn_TrainingReq_Save.Visible = false;
                this.btn_TrainingReq_Cancel.Visible = false;
                this.btn_TrainingReq_Add.Visible = ((Auth.isEdit || Auth.isAdd) && (Mode == "New" || Mode == "Edit"));
                trtrainingreqfields.Visible = false;
            }

            //TAB-3 ACADEMIC DETAILS
            if (index == 2)
            {
                this.btn_Apprasial_Save.Visible = false;
                this.btn_Apprasial_Cancel.Visible = false;
                this.btn_Apprasial_Add.Visible = ((Auth.isEdit || Auth.isAdd) && (Mode == "New" || Mode == "Edit"));
                this.trapprasial.Visible = false;
                bindapprasialdata(Convert.ToInt16(HiddenPK.Value));
            }
            //TAB-4 History Tab
            if (index == 3)
            {
                //this.hfcontractid.Value = "";
                //this.trcontractdetails.Visible = false;
                //Show_Contract_crew(Convert.ToInt16(HiddenPK.Value));
                Show_ExperienceSummary_crew(Convert.ToInt16(HiddenPK.Value));
                pnl_history_1.Visible = false;
                pnl_history_2.Visible = false;
                pnl_history_3.Visible = true;
                pnl_history_4.Visible = false;

                //btn_ContractSave.Visible = false;
                //gv_Contract.Columns[1].Visible = ((Auth.isEdit || Auth.isAdd) && (Mode == "New" || Mode == "Edit")) && (Alerts.getLocation() == "S");
            }
   
        }
        else 
        {
            

            //TAB LEVEL BUTTONS

            //TAB-1(TRAVEL DETAILS)
            if (index == 0)
            {
                btn_crm_add.Visible = false;
                btn_CRM_save.Visible = false;
                btn_crm_Cancel.Visible = false;
                crmpanel.Visible = false;
            }

            //TAB-2 PROFESSIONAL DETAILS

            if (index == 1)
            {
                bindtrainingdata(Convert.ToInt16(HiddenPK.Value));
                gettrainingdata();
                BindTrainingTypeDropDown();
                this.btn_TrainingReq_Save.Visible = false;
                this.btn_TrainingReq_Cancel.Visible = false;
                this.btn_TrainingReq_Add.Visible = false;
                trtrainingreqfields.Visible = false;
            }

            //TAB-3 ACADEMIC DETAILS
            if (index == 2)
            {
                this.btn_Apprasial_Save.Visible = false;
                this.btn_Apprasial_Cancel.Visible = false;
                this.btn_Apprasial_Add.Visible = ((Auth.isEdit || Auth.isAdd) && (Mode == "New" || Mode == "Edit"));
                this.trapprasial.Visible = false;
                bindapprasialdata(Convert.ToInt16(HiddenPK.Value));
            }
            //TAB-4 History Tab
            if (index == 3)
            {
                //this.hfcontractid.Value = "";
                //this.trcontractdetails.Visible = false;
                //Show_Contract_crew(Convert.ToInt16(HiddenPK.Value));
                Show_ExperienceSummary_crew(Convert.ToInt16(HiddenPK.Value));
                pnl_history_1.Visible = false;
                pnl_history_2.Visible = false;
                pnl_history_3.Visible = true;
                pnl_history_4.Visible = false;

                //btn_ContractSave.Visible = false;
                //gv_Contract.Columns[1].Visible =false;
            }
    
        }
    }
    //--------
    #region Tab-1 CRM Details
    private void bindgridCRM()
    {
        ProcessSelectCRMDetails obj = new ProcessSelectCRMDetails();
        CRMDetails crmdetail = new CRMDetails();
        crmdetail.CrewId = Convert.ToInt32(HiddenPK.Value);
        obj.CRMDetails = crmdetail;
        obj.Invoke();
        gvcrm.DataSource = obj.ResultSet;
        gvcrm.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("SELECT CREWCRMID,Description as 'Description', " +
            "ShowInAlert =case " +
            "when ShowInAlert = 'Y' then 'Yes' " +
            "when ShowInAlert = 'N' then 'No' " +
            "else '-' end, " +
            "convert(varchar, AlertExpiryDate, 101) as AlertExpiryDate, " +
            "(select case when convert(varchar, AlertExpiryDate, 101) < getdate() then '0' else '1' end) as AValue, " +
            "isnull(CRMCategory, '') as CRMCategory, " +
            "(Select CRMCategoryName from CrmCategory where CRMCategory.CRMCategory = CrewCRMDetails.CRMCategory) as CRMCategoryName, " +
            "(SELECT FirstName + ' ' + LastName from UserLogin where LoginId = CrewCRMDetails.CreatedBy) as CreatedBy, " +
            "CrewCRMDetails.CreatedBy as CreatedByID, " +
            "CreatedOn,Filename,FileContent FROM CrewCRMDetails where CrewId=" + Convert.ToInt32(HiddenPK.Value).ToString() + " order by CreatedOn desc");
        gvcrm.DataBind();
        DataTable dt = CrewSignOff.Select_CrewMemberSignOffDetails(crmdetail.CrewId);
        foreach (DataRow dr in dt.Rows)
        {
            txt_AvailableDt.Text = Alerts.FormatDate(dr["AvailableFrom"].ToString());
            Button2.Visible = Auth.isAdd || Auth.isEdit;

            dt = Budget.getTable("SELECT AvalRemark FROM CREWPERSONALDETAILS CPD WHERE CPD.CREWID=" + crmdetail.CrewId.ToString()).Tables[0];
            if(dt!=null )
            {
                if (dt.Rows.Count > 0)
                    txtAvlRem.Text = dt.Rows[0][0].ToString(); 
            }

        }
        Button2.Visible = (Mode == "New" || Mode == "Edit");
    }
     
    protected void imgAtt_OnClick(object sender, EventArgs e)
    {
        int pk = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dt = Budget.getTable("SELECT FileName,FileContent FROM CrewCRMDetails where CrewCRMid=" + pk).Tables[0];
        if (dt != null)
            if (dt.Rows.Count>0)
            {
                string filename = dt.Rows[0][0].ToString();
                byte[] filecontent = (byte[])dt.Rows[0][1];
                Response.Clear();
                Response.ContentType = "application/" + Path.GetExtension(filename).Substring(1);
                Response.AddHeader("Content-Disposition","Attachment;filename=" + filename);
                Response.BinaryWrite(filecontent);
                Response.End();
                Response.Flush();
            }
    }
    protected void gvcrm_OnViewClick(object sender, EventArgs e)
    {
        int pk = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        CRMDetails crmdetail = new CRMDetails();
        ProcessSelectCRMDetails obj = new ProcessSelectCRMDetails();
        crmdetail.CrewCRMId = Convert.ToInt32(pk);
        obj.CRMDetails = crmdetail;
        obj.Invoke();
        Show_Record_crm(crmdetail);

        crmpanel.Visible = true;
        btn_crm_Cancel.Visible = true;
        btn_CRM_save.Visible = false;
        btn_crm_add.Visible = false ;
    }
    protected void Show_Record_crm(CRMDetails crm)
    {
        string Mess;
        Mess = "";

        HiddenCRMPK.Value = crm.CrewCRMId.ToString();
        txtdescription.Text = crm.Description;
        if (Convert.ToChar(crm.ShowInAlert) == 'Y')
        {
            chk_show_alert.Checked = true;
            txt_crm_expirydate.CssClass = "required_box";
        }
        else
        {
            chk_show_alert.Checked = false;
            txt_crm_expirydate.CssClass = "input_box";
        }
        txt_crm_expirydate.Text = Alerts.FormatDate(crm.AlertExpiryDate);
        if (crm.CRMCategory.ToString() == " ")
        {
            ddl_crm_category.SelectedIndex = 0;
        }
        else
        {
    
            Mess = Mess + Alerts.Set_DDL_Value(ddl_crm_category, crm.CRMCategory.ToString(), "CRM Category");
        }
        if (Mess.Length > 0)
        {
            this.lblMessage.Text = "The Status Of Following Has Been Changed To In-Active <br/>" + Mess.Substring(1);
            this.lblMessage.Visible = true;
        }
    }
    protected void gvcrm_OnEditClick(object sender, EventArgs e)
    {
        int pk = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        CRMDetails crmdetail = new CRMDetails();
        ProcessSelectCRMDetails obj = new ProcessSelectCRMDetails();
        DataTable dt = Budget.getTable("SELECT CreatedBy FROM CrewCRMDetails where CrewCRMid=" + pk).Tables[0];
        if (dt != null)
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt32(Session["loginid"].ToString()) != Convert.ToInt32(dt.Rows[0][0]) && Convert.ToInt32(Session["loginid"].ToString())!=1)
                {
                    lblMessage.Text = "You have not permission to Edit this Record.";
                    return;
                }
                else
                {
                    crmdetail.CrewCRMId = Convert.ToInt32(pk);
                    obj.CRMDetails = crmdetail;
                    obj.Invoke();
                    Show_Record_crm(obj.CRMDetails);
                    if (chk_show_alert.Checked == true)
                    {
                        btn_CRM_save.CausesValidation = true;
                    }
                    else
                    {
                        btn_CRM_save.CausesValidation = false;
                    }
                    crmpanel.Visible = true;
                    btn_crm_Cancel.Visible = true;
                    btn_CRM_save.Visible = true;
                    btn_crm_add.Visible = (Auth.isAdd || Auth.isEdit) && (Mode == "Edit"); ;
                }
            }
    }
    protected void gvcrm_OnDeleteClick(object sender, EventArgs e)
    {
        int pk = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        CRMDetails exp = new CRMDetails();
        ProcessDeleteCRMDetails obj = new ProcessDeleteCRMDetails();
        DataTable dt = Budget.getTable("SELECT CreatedBy FROM CrewCRMDetails where CrewCRMid=" + pk).Tables[0];
        if (dt != null)
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt32(Session["loginid"].ToString()) != Convert.ToInt32(dt.Rows[0][0]) && Convert.ToInt32(Session["loginid"].ToString()) != 1)
                {
                    lblMessage.Text = "You have not permission to delete this Record.";
                    return;
                }
                else
                {   
                    exp.CrewCRMId = Convert.ToInt32(pk);
                    obj.CRMDetails = exp;
                    obj.Invoke();
                    bindgridCRM();
                }
            }
    }
    
    
    protected void chk_show_alert_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_show_alert.Checked == true)
        {
            txt_crm_expirydate.Enabled = true;
            btn_CRM_save.CausesValidation = true;
            txt_crm_expirydate.CssClass = "required_box";
            img_crm_expiry.Enabled = true;
        }
        else
        {
            btn_CRM_save.CausesValidation = false;
            txt_crm_expirydate.Enabled = false;
            txt_crm_expirydate.Style.Remove("class");
            txt_crm_expirydate.CssClass = "input_box";
            img_crm_expiry.Enabled = false;
        }
    }
    protected void btn_CRM_save_Click(object sender, EventArgs e)
    {
        string filename="";
        byte[] filecontent=new byte[0];
        if(flpcrm.HasFile)
        {
            filename = Path.GetFileName(flpcrm.FileName);
            filecontent = flpcrm.FileBytes;
		if(!(filename.EndsWith(".pdf")))
		{
			lblMessage.Text = "Only pdf attachments are allowed.";
		        flpcrm.Focus();  
		        return;
		}
        }
        if (txtdescription.Text.Trim() == "")
        {
            lblMessage.Text = "Please enter Description.";
            txtdescription.Focus();  
            return; 
        }
        if (ddl_crm_category.SelectedIndex<=0)
        {
            lblMessage.Text = "Please select CRM category.";
            ddl_crm_category.Focus();
            return;
        }
        CRMDetails obj = new CRMDetails();
        if (HiddenCRMPK.Value.Trim() == "")
        {
            obj.Createdby = Convert.ToInt32(Session["loginid"].ToString());
        }
        else
        {
            obj.CrewCRMId = Convert.ToInt32(HiddenCRMPK.Value);
            obj.Modifiedby = Convert.ToInt32(Session["loginid"].ToString());
        }
        obj.CrewId = Convert.ToInt32(HiddenPK.Value);
        obj.Description = txtdescription.Text;
        if (chk_show_alert.Checked == true)
        {

            obj.ShowInAlert = 'Y'.ToString();
            obj.AlertExpiryDate = txt_crm_expirydate.Text;
        }
        else
        {
            obj.ShowInAlert = 'N'.ToString();
        }
        if (this.ddl_crm_category.SelectedIndex != 0)
        {
            obj.CRMCategory = Convert.ToChar(this.ddl_crm_category.SelectedValue);
        }
        else
        {
            obj.CRMCategory = Convert.ToChar(" ");
        }

        //ProcessAddCRMDetails obj2 = new ProcessAddCRMDetails();
        //obj2.CRMDetails = obj;
        //obj2.Invoke();
        //HiddenCRMPK.Value = obj.CrewCRMId.ToString();

        Object alertexpdate = DBNull.Value;
        if(txt_crm_expirydate.Text.Trim()!="")
        {
            alertexpdate = txt_crm_expirydate.Text.Trim();
        }
        int _UserId = Common.CastAsInt32(Session["loginid"].ToString());
        

        Common.Set_Procedures("DBO.InsertUpdateCrewMemberCRMDetails ");
        Common.Set_ParameterLength(10);
        Common.Set_Parameters(
                new MyParameter("@CrewCRMId", Common.CastAsInt32(HiddenCRMPK.Value)),
                new MyParameter("@CrewId", Common.CastAsInt32(HiddenPK.Value)),
                new MyParameter("@Description", txtdescription.Text.Trim().Replace("'", "`")),
                new MyParameter("@ShowInAlert", ((chk_show_alert.Checked) ? "Y" : "N")),
                new MyParameter("@AlertExpiryDate", alertexpdate),
                new MyParameter("@CreatedBy", _UserId),
                new MyParameter("@ModifiedBy", _UserId),
                new MyParameter("@CRMCategory", Convert.ToChar(this.ddl_crm_category.SelectedValue).ToString()),
                new MyParameter("@FileName", filename),
                new MyParameter("@FileContent", filecontent)
            );
        Boolean Res;
        DataSet Ds = new DataSet();
        Res = Common.Execute_Procedures_IUD(Ds);
        if (Res)
        {
            bindgridCRM();
            btn_crm_Cancel_Click(sender, e);
            lblMessage.Text = "Record updated successfully.";
        }
        else
        {
            lblMessage.Text = "Can't insert/update record. Error :" + Common.ErrMsg ;
        }
    }
    protected void btn_crm_add_Click(object sender, EventArgs e)
    {
        crmpanel.Visible = true;
        HiddenCRMPK.Value = "";
        txtdescription.Text = "";
        ddl_crm_category.SelectedIndex = 0;
        txt_crm_expirydate.Text = "";
        chk_show_alert.Checked = false;
        btn_crm_add.Visible = false;
        btn_crm_Cancel.Visible = true;
        btn_CRM_save.Visible = true;
        txt_crm_expirydate.CssClass = "input_box";
        txt_crm_expirydate.Enabled = false;
        img_crm_expiry.Enabled = false;
            btn_CRM_save.CausesValidation = false;
            txt_crm_expirydate.Enabled = false;
            txt_crm_expirydate.Style.Remove("class");
            txt_crm_expirydate.CssClass = "input_box";
            img_crm_expiry.Enabled = false;
    }
    protected void btn_crm_Cancel_Click(object sender, EventArgs e)
    {
        btn_crm_Cancel.Visible = false;
        crmpanel.Visible = false;
        btn_CRM_save.Visible = false;
        btn_crm_add.Visible = true;
    }
    //protected void r1_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (r1.SelectedIndex == 0)
    //    {
    //        p1.Visible = true;
    //        p2.Visible = false;
    //    }
    //    else
    //    {
    //        p1.Visible = false;
    //        p2.Visible = true;
    //    }
    //}
    protected void btn_Update_AvailabelDate_Click(object sender, EventArgs e)
    {
        if (txt_AvailableDt.Text.Trim() != "")
        {
            if (DateTime.Today >= DateTime.Parse(txt_AvailableDt.Text))
            {
                lblMessage.Text = "Available date must be more than today.";
                return;
            }
        }
        try
        {
            Alerts.Update_AvailableDate(Convert.ToInt32(HiddenPK.Value), DateTime.Parse(txt_AvailableDt.Text), txtAvlRem.Text, Convert.ToInt32(Session["loginid"].ToString()));
            lblMessage.Text = "Updated Successfully.";
            Show_Header();
        }
        catch 
        {
            lblMessage.Text = "Cant Updated.";
        }
        
    }
    #endregion
    //--------
    #region Tab-2 Training Details
    private void bindtrainingdata(int i)
    {
        CrewTrainingRequirement crewtrainreq = new CrewTrainingRequirement();
        crewtrainreq.CrewId = Convert.ToInt16(HiddenPK.Value);
        ProcessSelectCrewTrainingRequirementData gettrainreq = new ProcessSelectCrewTrainingRequirementData();
        gettrainreq.TrainingRequirement = crewtrainreq;
        gettrainreq.Invoke();
        gvtrainingrequirement.DataSource = gettrainreq.ResultSet;
        gvtrainingrequirement.DataBind();

        lblTotalTraining.Text = gvtrainingrequirement.Rows.Count.ToString();
        lblTotalAttended.Text = "0";
        lblTotalRemaining.Text = "0";
    }
    protected void gvtrainingrequirement_PreRender(object sender, EventArgs e)
    {
        if (this.gvtrainingrequirement.Rows.Count <= 0)
        {
            this.lbl_trainingrequirement_message.Text = "No Records Found..!";
        }
        else
        {
            this.lbl_trainingrequirement_message.Text = "";
        }

    }
    protected void gvtrainingrequirement_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            gvtrainingrequirement.Columns[1].Visible = false;
            gvtrainingrequirement.Columns[2].Visible = false;

            // Can Add
            if (Auth.isAdd)
            {
            }

            // Can Modify
            if (Auth.isEdit)
            {
                this.gvtrainingrequirement.Columns[1].Visible = ((Mode == "New") || (Mode == "Edit")) ? true : false;
            }

            // Can Delete
            if (Auth.isDelete)
            {
                gvtrainingrequirement.Columns[2].Visible = ((Mode == "New") || (Mode == "Edit")) ? true : false;
            }

            // Can Print
            if (Auth.isPrint)
            {
            }

        }
        catch
        {

        }
    }
    protected void gvtrainingrequirement_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        CrewTrainingRequirement crewtrainreq = new CrewTrainingRequirement();
        crewtrainreq.TrainingRequirementId = Convert.ToInt16(this.gvtrainingrequirement.DataKeys[e.RowIndex].Value.ToString());
        if (this.gvtrainingrequirement.Rows[e.RowIndex].Cells[6].Text.Trim().ToLower() == "yes")
        {
            lblMessage.Text = "Unable to remove. This crewmember has atteneded the training.";
            return;
        }
        else
        {
            ProcessDeleteCrewTrainingRequirement deletedta = new ProcessDeleteCrewTrainingRequirement();
            deletedta.TrainingRequirement = crewtrainreq;
            deletedta.Invoke();
            bindtrainingdata(Convert.ToInt16(HiddenPK.Value));

            //if (crewtrainreq.TrainingRequirementId == Convert.ToInt32(htrainreqid.Value))
            //{
            //    btn_TrainingReq_Add_Click(sender, e);
            //}
        }
    }
    protected void gvtrainingrequirement_SelectedIndexChanged(object sender, EventArgs e)
    {
        int i;
        i = gvtrainingrequirement.SelectedIndex;
        show_trainreq_data(i);
        this.trtrainingreqfields.Visible = true;
        this.btn_TrainingReq_Save.Visible = false;
        this.btn_TrainingReq_Add.Visible = ((Mode == "Edit") || (Mode == "New"));
        this.btn_TrainingReq_Cancel.Visible = true;

    }
    protected void gvtrainingrequirement_RowEditing(object sender, GridViewEditEventArgs e)
    {
        trtrainingreqfields.Visible = true;
        btn_TrainingReq_Save.Visible = true;
        this.btn_TrainingReq_Cancel.Visible = true;
        show_trainreq_data(e.NewEditIndex);
        htrainreqid.Value = gvtrainingrequirement.DataKeys[e.NewEditIndex].Value.ToString();
    }
    protected void btn_TrainingReq_Add_Click(object sender, EventArgs e)
    {
        trtrainingreqfields.Visible = true;
        btn_TrainingReq_Add.Visible = false;
        this.btn_TrainingReq_Save.Visible = true;
        this.btn_TrainingReq_Cancel.Visible = true;
        cleartrainingreqdetails();
        this.htrainreqid.Value = "";
    }
    protected void btn_TrainingReq_Save_Click(object sender, EventArgs e)
    {
        CrewTrainingRequirement crewtrainreq = new CrewTrainingRequirement();
        try
        {
            if (chk_TrainingVerified.Checked && txt_Remark.Text.Trim() == "")
            {
                lblMessage.Text = "Please enter Remark.";
                txt_Remark.Focus();
                return;
            }
            if (this.htrainreqid.Value == "")
            {
                crewtrainreq.TrainingRequirementId = -1;
                crewtrainreq.CreatedBy = Convert.ToInt32(Session["loginid"].ToString());
                crewtrainreq.N_CrewTrainingStatus = "O";  
            }
            else
            {
                crewtrainreq.TrainingRequirementId = Convert.ToInt32(this.htrainreqid.Value);
                crewtrainreq.ModifiedBy = Convert.ToInt32(Session["loginid"].ToString());
            }
            crewtrainreq.CrewId = Convert.ToInt16(HiddenPK.Value);
            crewtrainreq.TrainingId = Convert.ToInt16(this.ddl_TrainingReq_Training.SelectedValue);
            crewtrainreq.Remark = this.txt_Remark.Text;
            crewtrainreq.N_DueDate = txt_DueDate.Text;
            crewtrainreq.N_CrewVerified = chk_TrainingVerified.Checked?"Y":"N";  


            ProcessCrewTrainingRequirementInsertData addreqdata = new ProcessCrewTrainingRequirementInsertData();
            addreqdata.TrainingRequirement = crewtrainreq;
            addreqdata.Invoke();
            lblMessage.Text = "Record Successfully Saved.";
        }


        catch (Exception es)
        {
            lblMessage.Text = "Record Not Saved.";
        }

        bindtrainingdata(Convert.ToInt16(HiddenPK.Value)); 
        cleartrainingreqdetails();
    }
    protected void btn_TrainingReq_Cancel_Click(object sender, EventArgs e)
    {
        trtrainingreqfields.Visible = false;
        this.btn_TrainingReq_Save.Visible = false;
        this.btn_TrainingReq_Cancel.Visible = false;
        this.btn_TrainingReq_Add.Visible = ((Mode == "Edit") || (Mode == "New"));

    }
    private void show_trainreq_data(int i)
    {
        string Mess;
        Mess = "";
        HiddenField hvessel = ((HiddenField)gvtrainingrequirement.Rows[i].FindControl("hvesselid"));
        HiddenField hrank = ((HiddenField)gvtrainingrequirement.Rows[i].FindControl("hrankid"));
        HiddenField htraining = ((HiddenField)gvtrainingrequirement.Rows[i].FindControl("htrainingid"));
        // this.ddl_TrainingReq_Training.SelectedValue = htraining.Value;
        
        ddl_TrainingType.SelectedValue = ((HiddenField)gvtrainingrequirement.Rows[i].FindControl("HiddenTrainingType")).Value;
        ddl_TrainingType_SelectedIndexChanged(new object(), new EventArgs());

        Mess = Mess + Alerts.Set_DDL_Value(ddl_TrainingReq_Training, htraining.Value, "Training Name");

        htraining = ((HiddenField)gvtrainingrequirement.Rows[i].FindControl("HiddenRemark"));


        txt_DueDate.Text = Alerts.FormatDate(((HiddenField)gvtrainingrequirement.Rows[i].FindControl("HiddenDueDate")).Value);
        lbl_TrainingStatus.Text = ((HiddenField)gvtrainingrequirement.Rows[i].FindControl("HiddenTrainingStatus")).Value;
        chk_TrainingVerified.Checked = ((HiddenField)gvtrainingrequirement.Rows[i].FindControl("HiddenTrainingVerified")).Value.ToString() == "Y";

        lblVerifiedBy.Text = ((HiddenField)gvtrainingrequirement.Rows[i].FindControl("HiddenVerifiedBy")).Value.ToString();
        lblverifiedOn.Text = Alerts.FormatDate(((HiddenField)gvtrainingrequirement.Rows[i].FindControl("HiddenVerifiedOn")).Value.ToString());

        this.txt_Remark.Text = htraining.Value;

        

        if (Mess.Length > 0)
        {
            this.lbl_trainingrequirement_message.Text = "The Status Of Following Has Been Changed To In-Active <br/>" + Mess.Substring(1);
            this.lbl_trainingrequirement_message.Visible = true;
        }
    }
    private void cleartrainingreqdetails()
    {
        htrainreqid.Value = "";
        try
        {
            ddl_TrainingType.SelectedIndex = 0;
            ddl_TrainingType_SelectedIndexChanged(new object(), new EventArgs());   
            this.ddl_TrainingReq_Training.SelectedIndex = 0;
            chk_TrainingVerified.Checked = false;
            lbl_TrainingStatus.Text = "";
            txt_DueDate.Text = "";  
        }
        catch { }
        this.txt_Remark.Text = "";
    }
    private void BindTrainingTypeDropDown()
    {
        DataTable dt114 = Training.selectDataTrainingTypeId();
        this.ddl_TrainingType.DataValueField = "TrainingTypeId";
        this.ddl_TrainingType.DataTextField = "TrainingTypeName";
        this.ddl_TrainingType.DataSource = dt114;
        this.ddl_TrainingType.DataBind();
        ddl_TrainingType_SelectedIndexChanged(new object(), new EventArgs());
    }
    protected void ddl_TrainingType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt133 = Training.selectDataTrainingDetailsByTrainingTypeId(Convert.ToInt32(ddl_TrainingType.SelectedValue));
        this.ddl_TrainingReq_Training.DataValueField = "TrainingId";
        this.ddl_TrainingReq_Training.DataTextField = "TrainingName";
        this.ddl_TrainingReq_Training.DataSource = dt133;
        this.ddl_TrainingReq_Training.DataBind();
        if (ddl_TrainingReq_Training.Items.Count <= 0)
        {
            ddl_TrainingReq_Training.Items.Insert(0, new System.Web.UI.WebControls.ListItem("<Select>", "0"));
        }
    }
    #endregion
    //---------
    # region  Tab-3 Appraisal Details
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
    protected void gvApprasial_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvTrainingsWithAppraisal.DataSource = null;
        gvTrainingsWithAppraisal.DataBind(); 

        this.trapprasial.Visible = true;
        btn_Apprasial_Save.Visible = true;
        btn_Apprasial_Cancel.Visible = true;
        show_apprasial_data(e.NewEditIndex);

        this.h_apprasial_id.Value = gvApprasial.DataKeys[e.NewEditIndex].Value.ToString();
        btn_ShowTrainings_Click(sender,e);

    }
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
        this.h_apprasial_id.Value  = "";
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
            catch {crewappraisal.AverageMarks =0; }
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
    #region Old Data
    
    
    #endregion
    #endregion
    protected void imgbtn_Personal_Click(object sender, EventArgs e)
    {
        Response.Redirect("CrewDetails.aspx");
    }
    protected void imgbtn_Document_Click(object sender, EventArgs e)
    {
        Response.Redirect("CrewDocument.aspx");
    }
    //protected void imgbtn_Search_Click(object sender, ImageClickEventArgs e)
    //{
    //    Response.Redirect("CrewActivities.aspx");
    //}
    # region Tab-4 Hisory
    protected void rbhistory_SelectedIndexChanged(object sender, EventArgs e)
    {
        //this.hfcontractid.Value = "";
        //if (rbhistory.SelectedIndex == 0)
        //{
        //    //bindLicensedata(Convert.ToInt16(HiddenPK.Value));
        //    this.trcontractdetails.Visible = false;
        //    pnl_history_1.Visible = true;
        //    pnl_history_2.Visible = false;
        //    pnl_history_3.Visible = false;
        //    pnl_history_4.Visible = false;
        //    Show_Contract_crew(Convert.ToInt16(this.HiddenPK.Value));
        //    Show_ExperienceSummary_crew(Convert.ToInt16(this.HiddenPK.Value));
        //}
        //else if (rbhistory.SelectedIndex == 1)
        //{

        //    pnl_history_1.Visible = false;
        //    pnl_history_2.Visible = true;
        //    pnl_history_3.Visible = false;
        //    pnl_history_4.Visible = false;
        //    showrecoverablehead(Convert.ToInt16(this.HiddenPK.Value));
        //}
        if (rbhistory.SelectedIndex == 0)
        {
            pnl_history_1.Visible = false;
            pnl_history_2.Visible = false;
            pnl_history_3.Visible = true;
            pnl_history_4.Visible = false;
            Show_ExperienceSummary_crew(Convert.ToInt16(this.HiddenPK.Value));
        }
        else if (rbhistory.SelectedIndex == 1)
        {
            pnl_history_1.Visible = false;
            pnl_history_2.Visible = false;
            pnl_history_3.Visible = false;
            pnl_history_4.Visible = true;
            Show_Office_Visit_Details(Convert.ToInt32(this.HiddenPK.Value));
        }

    }
    public void Show_Office_Visit_Details(int CrewId)
    {
        string Query = "SELECT ROW_NUMBER() OVER(ORDER BY FROMDATE ) AS SrNo,convert(int,VM.CreatedOn) as IntCreatedOn, CASE CATEGORY WHEN 1 THEN 'PRE- JOINING' WHEN 2 THEN 'POST-SIGNOFF' WHEN 3 THEN 'OTHER' END AS Occasion,V.VesselName,(SELECT count(FP_FollowUp) from dbo.FR_FollowUpList WHERE dbo.FR_FollowUpList.FP_VisitorId = VM.Id) AS TotalFollowUp, (SELECT count(*) from dbo.FR_FollowUpList WHERE dbo.FR_FollowUpList.FP_VisitorId = VM.Id AND FP_Closed='False' ) AS OpenFollowUp, VM.* FROM DBO.OV_VisitMaster VM " +
                       "INNER JOIN CrewPersonalDetails CPD ON CPD.CrewNumber = VM.CREWNUM " +
                       "INNER JOIN VESSEL V ON V.VesselId = VM.VesselId " +
                       "WHERE CPD.CrewId =" + CrewId;
        DataTable dt = Budget.getTable(Query).Tables[0];
        if (dt.Rows.Count > 0)
        {
            grdOfficeVisit.DataSource = dt;
            grdOfficeVisit.DataBind();
            lblTotRecord.Text = "Total Records : " + dt.Rows.Count;
        }
        else
        {
            grdOfficeVisit.DataSource = null;
            grdOfficeVisit.DataBind();
            lblTotRecord.Text = "No Record Found.";
        }

    }
    protected void btn_refresh_Click(object sender, EventArgs e)
    {
        Show_Office_Visit_Details(Convert.ToInt32(this.HiddenPK.Value));
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
            txt_IssueDate.Text =Alerts.FormatDate(dt.Rows[0]["IssueDate"].ToString());
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
        string sql = "Select (Select FirstName+' '+LastName  from UserLogin UL where UL.LoginID=CCL.LockedBy)LockedBy,replace(convert(varchar,CCL.LockedOn,106),' ','-')LockedOn  from CrewContractLocking CCL where ContractID=" + ContractId.ToString() + " ";
        DataTable DT = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (DT.Rows.Count > 0)
        {
            btnUpdateWages.Visible = false;
           // btnConRevision.Visible = false;
            btn_ContractSave.Visible = false;
            lblLockbyOn.Text = "Locked By/On : " + DT.Rows[0]["LockedBy"].ToString() + " / " + DT.Rows[0]["LockedOn"].ToString();
        }
        if (dtUser.Rows.Count > 0)
        {
            foreach (DataRow dr in dtUser.Rows)
            {
                if (dr["SuperUser"] != null && dr["SuperUser"].ToString() == "Y")
                {
                    btnUpdateWages.Enabled = true;
                    //btnConRevision.Enabled = true;
                }
                else
                {
                    btnUpdateWages.Enabled = false;
                   // btnConRevision.Enabled = false;
                }
            }
        }

    }
    protected void gv_Contract_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.lblMessage.Text = "";
        HiddenField hfd, hfContractStauts;
        
        hfd = (HiddenField)gv_Contract.Rows[gv_Contract.SelectedIndex].FindControl("hfd_ContractId");
        
        hfContractStauts = (HiddenField)gv_Contract.Rows[gv_Contract.SelectedIndex].FindControl("hfContractStauts");
        if (Convert.ToString(hfContractStauts.Value.ToUpper()) == "OPEN")
        {
            btnConRevision.Enabled = true;
        }
        else
        {
            btnConRevision.Enabled = false;
        }

        this.hfcontractid.Value = hfd.Value;
        Show_Contract(Convert.ToInt32(hfd.Value));
        this.trcontractdetails.Visible = true;
        //txt_startdt.Enabled = false;
        //ImageButton1.Enabled = false;
    }
    private void Show_Contract_crew(int _crewid)
    {
        DataTable dt;
        dt = CrewContract.Select_Contract_Crew(_crewid);
        gv_Contract.DataSource = dt;
        gv_Contract.DataBind();
    }
    private void bindGd_AssignWages()
    {
        Double d1;
        DataTable dtGrid = CrewContract.bind_AssignedWages_by_StartDate(txt_StartDate.Text, Convert.ToInt32(Txt_Seniority.Text), Convert.ToInt32(HiddenField_NationalityId.Value), Convert.ToInt32(dp_wagescale.SelectedValue), Convert.ToInt32(HiddenField_RankId.Value), Convert.ToInt32((chk_SupCert.Checked) ? 1 : 0));
        Gd_AssignWages.DataSource = dtGrid;
        Gd_AssignWages.DataBind();
        if (dtGrid.Rows.Count > 0)
        {
            //btn_Save.Enabled = true;
        }
        else
        {
            //btn_ContractSave.Enabled = false;
            //lb_deduction.Text = "";
            //lb_Gross.Text = "";
            //lb_NewEarning.Text = "";
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
        //lb_Gross.Text = earn.ToString();
        //lb_deduction.Text = dedc.ToString();
        Net_earn = earn - dedc;
        Net_earn = Net_earn + d1; 
        //lb_NewEarning.Text = Net_earn.ToString();
        
    }
    //protected void btn_ShowWages_Click(object sender, EventArgs e)
    //{
    //    bindGd_AssignWages();
    //}
    //****************
    protected void btn_contract_cancel_Click(object sender, EventArgs e)
    {
        this.hfcontractid.Value = "";
        this.trcontractdetails.Visible = false;
        btn_ContractSave.Visible = false;
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
    protected void btn_License_Print_Click(object sender, EventArgs e)
    {
        if (this.hfcontractid.Value == "")
        {
            this.lblMessage.Text = "Select Any Contract To Print";
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
                    this.lblMessage.Text = "Select any Contract template for Print.";
                    ddl_ContractTemplate.Focus();
                } 
            }
            catch
            {

            }
        }
    }
    private void Show_ExperienceSummary_crew(int _crewid)
    {
        DataTable dt22;
        dt22 = CrewContract.Select_ExperienceSummary_Crews(_crewid);
        ViewState.Add("tot", "0");
        GVSummaryExperience.DataSource = dt22;
        GVSummaryExperience.DataBind();

    }
    protected void GVSummaryExperience_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int tot=0;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblsr = new Label();
            lblsr = (Label)e.Row.FindControl("lbsrno");
            lblsr.Text = Convert.ToString(Convert.ToInt32(e.Row.RowIndex.ToString()) + 1);
            Label lbexp = new Label();
            lbexp = (Label)e.Row.FindControl("lbexperience");
            if (lbexp.Text == "")
            {
                lbexp.Text = "0";
            }
            tot =Convert.ToInt32(ViewState["tot"].ToString())+ Convert.ToInt32(lbexp.Text);
            ViewState.Add("tot", tot);
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbtotalexp1 = new Label();
            lbtotalexp1 = (Label)e.Row.FindControl("lbtotalexp");
            lbtotalexp1.Text = "Total Experience:   " + ViewState["tot"].ToString();
        }
    }
    #endregion
    protected void btn_close_Click(object sender, EventArgs e)
    {
        if (gv_Contract.SelectedIndex < 0)
        {
            lblMessage.Text = "Plese select a Contract to Close.";
            return;
        }

        int CId, Loginid,res;
        HiddenField hfd;
        hfd = (HiddenField)gv_Contract.Rows[gv_Contract.SelectedIndex].FindControl("hfd_ContractId");
        Loginid = Convert.ToInt32(Session["loginid"].ToString());
        CId = Convert.ToInt32(hfd.Value);  
        res=CrewContract.Close_Contract(CId,Loginid);
        if (res == 0)
        {
            lblMessage.Text = "Contract is already closed.";
        }
        else
        {
            lblMessage.Text = "Contract closed succesfully.";
        }

    }
    //protected void gv_Contract_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    this.lblMessage.Text = "";
    //    HiddenField hfd;
    //    gv_Contract.SelectedIndex = e.NewEditIndex; 
    //    hfd = (HiddenField)gv_Contract.Rows[e.NewEditIndex].FindControl("hfd_ContractId");
    //    this.hfcontractid.Value = hfd.Value;
    //    Show_Contract(Convert.ToInt32(hfd.Value));
    //    this.trcontractdetails.Visible = true;
    //    //txt_startdt.Enabled = true;
    //    //ImageButton1.Enabled = true;
    //    btn_ContractSave.Visible = true;
    //    SetWagesButtonVisibility(Common.CastAsInt32( hfd.Value));
    //}
    protected void btn_contract_Save_Click(object sender, EventArgs e)
    {
        int contractid;
        int res;
        contractid = Convert.ToInt32(this.hfcontractid.Value);

        DataTable dttable = cls_SearchReliever.Check_CrewContractDate(Convert.ToDateTime(txt_StartDate.Text),Convert.ToInt32(hfd_vesselid.Value));
        foreach(DataRow dr in dttable.Rows)
        {
            if(Convert.ToInt32(dr[0].ToString()) > 0)
            {
                lblMessage.Text = "Start Date Can't be less than last Payroll Date.";
                return;
            }
        }

        try
        {
            if (Convert.ToInt32(Txt_Seniority.Text) <= 0)
            {
                lblMessage.Text = "Seniotity Must be Greater Than Zero(0).";
                return;

            }
            res=Alerts.Update_ContractDetails(contractid, txt_IssueDate.Text.Trim(), txt_StartDate.Text.Trim(), Convert.ToInt32(dp_wagescale.SelectedValue), txt_OtherAmount.Text, Convert.ToInt32((chk_SupCert.Checked) ? 1 : 0), "");
            if (res == 0)
            {
                lblMessage.Text = "Contract Updated Successfully.";
            }
            else
            {
                lblMessage.Text = "Contract has been Closed.";
            }
            Show_Contract_crew(Convert.ToInt16(HiddenPK.Value));
        }
        catch
        {
            lblMessage.Text = "Contract can't Updated.";
        }
    }
    public void showrecoverablehead(int crewid)
    {
        DataTable dt;
        dt = CrewContract.Select_RecoverableExpenses(crewid);
       gv_RecoverableExpenses.DataSource = dt;
       gv_RecoverableExpenses.DataBind();
    }
    protected void gv_RecoverableExpenses_PreRender(object sender, EventArgs e)
    {
        if (gv_RecoverableExpenses.Rows.Count <= 0)
        {
            lblrecoverableexpenses.Text = "No Record To Show";
        }
        else
        {
            lblrecoverableexpenses.Text = "";
        }
    }
    protected void btn_CrewExt_Click(object sender, EventArgs e)
    {
        if (gv_Contract.SelectedIndex < 0)
        {
            lblMessage.Text = "Plese select a Contract For Extension.";
            return;
        }
        if (gv_Contract.Rows[gv_Contract.SelectedIndex].Cells[10].Text.Trim() == "Closed")
        {
            lblMessage.Text = "Plese select an Active Contract For Extension.";
            return;
        }
        int CId;
        HiddenField hfd;
        hfd = (HiddenField)gv_Contract.Rows[gv_Contract.SelectedIndex].FindControl("hfd_ContractId");
        CId = Convert.ToInt32(hfd.Value);
        Response.Redirect("../CrewOperation/SignOn.aspx?cid=" + Convert.ToInt32(HiddenPK.Value.Trim()) + "&ConId=" + CId);  
    }
    protected void btnUpdateWages_OnClick(object sender, EventArgs e)
    {
        try
        {
            bool flagEarn = false;
            bool flagDeduct = false;
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

            //Common.Execute_Procedures_Select_ByQueryCMS("UPDATE CrewContractheader SET OtherAmount=" + Common.CastAsDecimal(txt_Other_Amount.Text)) + ",Remark ='" + txtRemarks.Text.Replace("'", "`") + "',ExtraOTRate=" + Common.CastAsDecimal(txtExtraOTRate.Text) + ",TravelPayCriteria = Case when " + Common.CastAsInt32(ddlTravelPay.SelectedValue) + "  > 0 then " + Common.CastAsInt32(ddlTravelPay.SelectedValue) + " ELSE NULL END WHERE CONTRACTID=" + Convert.ToInt32(hfContractID.Value));


            if (flagEarn && flagDeduct)
            {
                lblMsgWages.Text = "Record updated successfully.";
            }
        }
        catch(Exception ex)
        {
            lblMsgWages.Text = ex.Message.ToString();
        }
        
    }
    //--------
    # region Tab-5 Training Matrix
    protected void BindTrainingInstitite()
    {
        DropDownList1.DataSource = Budget.getTable("Select InstituteId,InstituteName from TrainingInstitute");
        DropDownList1.DataTextField = "InstituteName";
        DropDownList1.DataValueField = "InstituteId";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< Select >", ""));
    }

    protected string GetSimilarTrainings(string Tid)
    {
        //------------------------------ GET SIMILER TRAININGS
        string SimilerTrainings = " select trainingid,TrainingName,(select TrainingTypeName from trainingtype tt where tt.trainingtypeid=t1.typeoftraining) as TrainingTypeName from Training T1 where T1.trainingid= " + Tid +
                                 "Union select trainingid,TrainingName,(select TrainingTypeName from trainingtype tt where tt.trainingtypeid=t.typeoftraining) as TrainingTypeName from Training t " +
                                 "where t.trainingid in (select d.SimilerTrainingId from TrainingSimiler d where d.TrainingId=" + Tid + ") " +
                                 "union select trainingid,TrainingName,(select TrainingTypeName from trainingtype tt where tt.trainingtypeid=t.typeoftraining) as TrainingTypeName from Training t  " +
                                 "where t.trainingid in (select d.TrainingId from TrainingSimiler d where d.SimilerTrainingId=" + Tid + ")";
        DataTable dtSimiller = Common.Execute_Procedures_Select_ByQueryCMS(SimilerTrainings);
       
        string SimilerTrainingsName = "";
        foreach (DataRow drs in dtSimiller.Rows)
        {
            SimilerTrainingsName += "," + drs[1].ToString() + " [<i style='color:Blue;'>" + drs[2].ToString() + "</i>]";
        }
        if (SimilerTrainingsName != "")
            SimilerTrainingsName = SimilerTrainingsName.Substring(1);
        SimilerTrainingsName = SimilerTrainingsName.Replace(",", "<span style='color:red'> OR </span><br/>");
        return SimilerTrainingsName;
         
    }
    protected void ShowTrainings(object sender, EventArgs e)
    {
        // DUE TRAININGS
        string WhereClause;
        DataTable dtDue = new DataTable();
        dv_Due.Visible = false;
        dvDone.Visible = false;
        if (radA.Checked)
        {
            WhereClause = "";

            dtDue = Common.Execute_Procedures_Select_ByQueryCMS(
            "SELECT ROW_NUMBER() OVER(ORDER BY T.TRAININGNAME,CTR.TODATE DESC) AS SNO,CTR.CREWID,T.TRAININGNAME + ' <span style=''color:green''>[' + TRAININGTYPENAME + ']</span>' AS TRAININGNAME,CTR.TRAININGID,CTR.TODATE,CASE WHEN ISNULL(SOURCE,0)=1 THEN 'ASSIGNED' WHEN ISNULL(SOURCE,0)=2 THEN 'MATRIX' ELSE 'PEAP' END AS SOURCE,ISNULL(VESSELCODE,OFFICENAME) AS LOCATION,TrainingRequirementId, " +
            "(SELECT COUNT(*) FROM CREWTRAININGREQUIREMENT CTR_IN WHERE CTR_IN.CREWID=CTR.CREWID AND CTR_IN.FROMDATE=CTR.FROMDATE AND CTR_IN.TRAININGID=CTR.TRAININGID) AS DUPLICATE " +
            "from CREWTRAININGREQUIREMENT CTR " +
            "INNER JOIN TRAINING T ON CTR.TRAININGID=T.TRAININGID " +
            "INNER JOIN TRAININGTYPE TT ON TT.TRAININGTYPEID=T.TYPEOFTRAINING " +
            "LEFT JOIN VESSEL V ON V.VESSELID=CTR.VESSELID " +
            "LEFT JOIN OFFICE O ON O.OFFICEID=CTR.OFFICEID " +
            "WHERE CREWID=" + Session["CrewId"].ToString() + " AND N_CREWTRAININGSTATUS='C' ORDER BY T.TRAININGNAME,CTR.TODATE DESC");

            rpt_Done.DataSource = dtDue;
            rpt_Done.DataBind();
            lblDoneCnt.Text= " ( " + dtDue.Rows.Count .ToString() + " ) Records found.";
            dvDone.Visible = true;
        }
        else
        {
            if (radR.Checked)
            {
                if (txt_Status.Text.Trim().Contains("Onboard"))
                {
                    DataTable DTONBOARDATE = Common.Execute_Procedures_Select_ByQueryCMS("SELECT ReliefDueDate FROM CREWPERSONALDETAILS WHERE CREWID=" + Session["CrewId"].ToString());
                    if (DTONBOARDATE.Rows.Count > 0)
                    {
                        WhereClause = "AND DueDate<='" + Common.ToDateString(Convert.ToDateTime(DTONBOARDATE.Rows[0][0]).AddMonths(1)) + "'";
                    }
                    else
                    {
                        WhereClause = "";
                    }
                }
                else
                {
                    WhereClause = "AND DueDate<='" + DateTime.Today.ToString("dd-MMM-yyyy") + "'";
                }
            }
            else
            {
                WhereClause = "";
            }
           
            dtDue = Common.Execute_Procedures_Select_ByQueryCMS("SELECT ROW_NUMBER() OVER(ORDER BY DueDate) AS SNO,* FROM ( SELECT DISTINCT dbo.fn_getSimilerTrainingsName(A.TRAININGID) AS TrainingName, " +
           "[dbo].[sp_getLastDone](A.CREWID,A.TRAININGID) AS LastDoneDate, " +
           "[dbo].[sp_getNextDue_DB](A.CREWID,A.TRAININGID) AS DueDate," +
           "[dbo].[sp_getNextDueSource_DB](A.CREWID,A.TRAININGID) AS Source," +
           "[dbo].[sp_getNextPlanDate](A.CREWID,A.TRAININGID) AS PlanDate " +
           "FROM " +
           "( " +
              "SELECT DISTINCT CREWID,TRAININGID from CREWTRAININGREQUIREMENT WHERE CREWID=" + Session["CrewId"].ToString() +
           ") A ) B WHERE DueDate IS NOT NULL " + WhereClause + " ORDER BY DueDate");

           rpt_Due.DataSource = dtDue;
           rpt_Due.DataBind();
           lblDueCnt.Text = " ( " + dtDue.Rows.Count.ToString() + " ) Records found.";
           dv_Due.Visible = true;
        }

      
        

        //"(SELECT DISTINCT CREWID,TRAININGID,NextDueComputed FROM DBO.CREWTRAININGREQUIREMENT WHERE CREWID=" + Session["CrewId"].ToString() + ") A  " +
    }
    protected void BindTrainingMatrix()
    {
        ShowTrainings(new object(), new EventArgs());
        return;

        TrainingMatrix = null;
        StringBuilder TBL = new StringBuilder();  
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("EXEC sp_getTrainingMatrix " + Session["CrewId"].ToString());
        TBL.Append("<table border='1' cellspacing='0' cellpadding='2' style='border-collapse:collapse;' width='1050px'>");
        TBL.Append("<tr style='font-weight:bold;height:20px;'>");
        TBL.Append("<td class='hd' style='width:300px;'>Training Name</td>");
        TBL.Append("<td class='hd' style='width:60px;'>Schedule</td>");
        TBL.Append("<td class='hd' style='width:80px;text-align:center;'>Source</td>");
        TBL.Append("<td class='hd' style='width:80px;text-align:center;'>Next Due Dt</td>");
        TBL.Append("<td class='hd' style=''>Records of Training Done</td>");
        TBL.Append("</tr>");
        TBL.Append("</table>");
        TBL.Append("<div style='width:1050px; height:205px; overflow-x:hidden;overflow-y:scroll;border:solid 1px gray;'>");
        TBL.Append("<table border='1' cellspacing='0' cellpadding='2' style='border-collapse:collapse;' width='1050px'>");
        foreach (DataRow dr in dt.Rows)
        {
            string SCH = dr["SCHCOUNT"].ToString() + "-" + dr["SCHTYPE"].ToString();
            if (SCH.Trim() == "0-")
                SCH = "";
          
            string color = "green";
            DateTime dtND;
           
            try
            {
                dtND=Convert.ToDateTime(dr["NEXTDUE"]);
                if (dtND < DateTime.Today)
                {
                    color = "red";
                }
                //if (Convert.ToDateTime(dtND.ToString("dd-MMM-yyyy")) <= DateTime.Today.AddYears(1))
                //{
                    DataRow dr1 = TrainingMatrix.NewRow();
                    dr1["SNo"] = TrainingMatrix.Rows.Count+1;
                    dr1["TReqId"] = dr["PK"].ToString();
                    dr1["TrainingId"] = dr["TRAININGID"].ToString();
                    dr1["TrainingName"] = dr["trainingname"].ToString();
                    dr1["NextDue"] = dtND.ToString("dd-MMM-yyyy");
                    TrainingMatrix.Rows.Add(dr1);
                //}
            }
            catch {}
            
            //------------------------------ GET SIMILER TRAININGS
            string SimilerTrainings = "select trainingid,TrainingName,(select TrainingTypeName from trainingtype tt where tt.trainingtypeid=t.typeoftraining) as TrainingTypeName from Training t " +
                                     "where t.trainingid in (select d.SimilerTrainingId from TrainingSimiler d where d.TrainingId=" + dr["TRAININGID"].ToString() + ") " +
                                     "union select trainingid,TrainingName,(select TrainingTypeName from trainingtype tt where tt.trainingtypeid=t.typeoftraining) as TrainingTypeName from Training t  " +
                                     "where t.trainingid in (select d.TrainingId from TrainingSimiler d where d.SimilerTrainingId=" + dr["TRAININGID"].ToString() + ")";
            DataTable dtSimiller = Common.Execute_Procedures_Select_ByQueryCMS(SimilerTrainings);
            SimilerTrainings = dr["TRAININGID"].ToString();
            foreach (DataRow drs in dtSimiller.Rows)
            {
                SimilerTrainings += "," + drs[0].ToString();
            }
            //-----
            string SimilerTrainingsName = "";
            foreach (DataRow drs in dtSimiller.Rows)
            {
                SimilerTrainingsName += "," + drs[1].ToString() + " [<i style='color:Blue;'>" + drs[2].ToString() + "</i>]";
            }
            //if (SimilerTrainingsName != "")
            //    SimilerTrainingsName = SimilerTrainingsName.Substring(1);
            SimilerTrainingsName = SimilerTrainingsName.Replace(",", "<span style='color:red'> OR </span><br/>");
            //------------------------------------
            TBL.Append("<tr>");
            TBL.Append("<td class='hd' style='width:300px;'>" + dr["trainingname"].ToString() + " [<i style='color:Blue;'>" + dr["TRAININGTYPENAME"].ToString() + "</i>]" + SimilerTrainingsName + "</td>");
            TBL.Append("<td class='hd' style='text-align:center;width:60px;'>" + SCH + "</td>");
            TBL.Append("<td class='hd' style='text-align:center;width:80px;'>" + dr["source"].ToString() + "</td>");
            TBL.Append("<td style='width:80px;text-align:center;color:white;background-color:" + color + "'>" + Common.ToDateString(dr["NEXTDUE"]) + "</td>");

            DataTable dtdone = Common.Execute_Procedures_Select_ByQueryCMS("SELECT TRAININGREQUIREMENTID,REPLACE(convert(varchar,TODATE,106),' ','-') AS DONEDATE,N_CrewTrainingStatus,REPLACE(ISNULL(CONVERT(VARCHAR,PlannedFor,106),''),' ','-') AS PlannedFor " +
                    " FROM CREWTRAININGREQUIREMENT WHERE CREWID=" + Session["CrewId"].ToString() + " AND TRAININGID IN(" + SimilerTrainings + ") AND (N_CREWTRAININGSTATUS='C' OR PlannedFor IS NOT NULL) AND STATUSID='A' ORDER BY TODATE desc");
            string str = "";
            foreach (DataRow dr1 in dtdone.Rows)
            {
                string Mode = ((dr1["N_CrewTrainingStatus"].ToString() == "C") ? "D" : "P");
                str += "<div onclick=\"showUpdate(this," + dr1["TRAININGREQUIREMENTID"].ToString() + "," + dr["TRAININGID"].ToString() + ",'" + Common.ToDateString(dr["NEXTDUE"]) + "','" + Mode + "'," + dr1["TRAININGREQUIREMENTID"].ToString() + ")\" style='cursor:pointer;margin:1px;border: solid 1px black;" + ((Mode == "P") ? "background-color:yellow" : "") + ";width:80px;float:left;text-align:center;'>" + ((Mode == "D") ? dr1["DONEDATE"].ToString() : dr1["PlannedFor"].ToString()) + "&nbsp;</div>";
            }
            TBL.Append("<td>" + str + "</td>");
            TBL.Append("</tr>");
        }
        TBL.Append("</table>");
        TBL.Append("</div>");
        litTraining.Text = TBL.ToString();   
    }
    protected void Load_Lit(object sender, EventArgs e)
    {
        litSummary.Text = getTrainingSummary(true, txtTrId.Text);
    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        Common.Execute_Procedures_Select_ByQueryCMS("DELETE FROM CREWTRAININGREQUIREMENT WHERE TrainingRequirementId=" + ((ImageButton)sender).CommandArgument);
        ShowTrainings(sender, e);
    }
    
    protected string getTrainingSummary(bool Update, string PK)
    {
        StringBuilder sb = new StringBuilder();
        string str = "select TRAININGTYPENAME,TRAININGNAME,N_DUEDATE,PLANNEDFOR,UL.FIRSTNAME+ ' '+ UL.LASTNAME AS PLANNEDBY,PLANNEDON,TI.InstituteName AS PlanInstitute,FROMDATE,TODATE,TI1.InstituteName AS DoneInstitute,Remark from " +
                   "CREWTRAININGREQUIREMENT CTR  " +
                   "INNER JOIN TRAINING T ON CTR.TRAININGID=T.TRAININGID " +
                   "INNER JOIN TRAININGTYPE TT ON TT.TRAININGTYPEID=T.TYPEOFTRAINING " +
                   "LEFT JOIN TRAININGINSTITUTE TI ON TI.InstituteId=CTR.PlannedInstitute " +
                   "LEFT JOIN TRAININGINSTITUTE TI1 ON TI.InstituteId=CTR.TrainingPlanningId " +
                   "LEFT JOIN USERLOGIN UL ON UL.LOGINID=CTR.PLANNEDBY where TRAININGREQUIREMENTID=" + PK;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(str);
        if (dt.Rows.Count > 0)
        {
            sb.Append("<table cellpadding='2' cellspacing='0' border='0' width='100%' style='border-collapse:collapse;text-align:right'>");
            sb.Append("<col align='right' width='130px;' />");
            sb.Append("<col align='left' />");
            sb.Append("<col align='right' width='130px;'/>");
            sb.Append("<col align='left' />");
            sb.Append("<tr>");
            sb.Append("<td colspan='4' style='text-align:center'><b>" + dt.Rows[0]["TRAININGNAME"].ToString() + "</b></td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td>Due On : </td>");
            sb.Append("<td style='text-align:left'>" + Common.ToDateString(dt.Rows[0]["N_DUEDATE"]) + "</td>");
            sb.Append("<td>Planned For : </td>");
            sb.Append("<td style='text-align:left'>" + Common.ToDateString(dt.Rows[0]["PLANNEDFOR"]) + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td>Planned Institute : </td>");
            sb.Append("<td style='text-align:left'>" + dt.Rows[0]["PlanInstitute"].ToString() + "</td>");
            sb.Append("<td>Planned By/On : </td>");
            sb.Append("<td style='text-align:left'>" + dt.Rows[0]["PLANNEDBY"].ToString() + " / " + dt.Rows[0]["PLANNEDON"].ToString() + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td>From Date : </td>");
            sb.Append("<td style='text-align:left'>" + Common.ToDateString(dt.Rows[0]["FROMDATE"]) + "</td>");
            sb.Append("<td>To Date : </td>");
            sb.Append("<td style='text-align:left'>" + Common.ToDateString(dt.Rows[0]["TODATE"]) + "</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td>Institute Done: </td>");
            sb.Append("<td colspan='3' style='text-align:left'>" + dt.Rows[0]["DoneInstitute"].ToString() + "</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
        }
        return sb.ToString();
    }

    protected String GetdataRow(string Sno, string TID, string Tname, string NextDue)
    {
        string DataRow = "<tr id='tr" + Sno + "'> " +
                        "<td> <label name='lblSrNo'>" + Sno + "</label> </td> " +
                        "<td>  " +
                           "<label name='lblTrainingName' >" + GetSimilarTrainings(TID) + "</label>  " +
                           "<input type='hidden' id='hfTrainingID" + Sno + "' value='" + TID + "' /> " +
                        "</td> " +
                        "<td> <label id='lblDueDate" + Sno + "' >" + NextDue + "</label> </td> " +
                        "<td> " +
                           "<input type='text' style='width:80px; font-weight:bold;' id='lblFromDate" + Sno + "' maxlength='10' onfocus=\"showCalendar('',this,this,'','holder1',5,22,1)\" title='Click here to enter date ' /> " +
                        "</td> " +
                        "<td> " +
                           "<input type='text' style='width:80px; font-weight:bold;' id='lblToDate" + Sno + "' maxlength='10' onfocus=\"showCalendar('',this,this,'','holder1',5,22,1)\" title='Click here to enter date '  /> " +
                        "</td> " +
                        "<td> " +
                            "<select style='width:130px; font-weight:bold;' id='ddlInstitute" + Sno + "' > " +
                                "<option value='0'  >Select</option> " +
                                "<option value='1'  >MTM YGM</option> " +
                                "<option value='2'  >DMA YGN(JV)</option> " +
                                "<option value='3' selected='selected' >ONBOARD</option> " +
                                "<option value='4'  >MMMC YGN(JV)</option> " +
                                "<option value='5'  >MOSA YGN(JV)</option> " +
                                "<option value='6'  >MTM INDIA</option> " +
                            "</select> " +
                        "</td> " +
                    "</tr>     ";
        return DataRow;
    }
    protected void TrainingExcel_Click(object sender, EventArgs e)
    {
        string DataRows = "";
        string HTMLFile = File.ReadAllText(Server.MapPath("~/CrewOperation/CrewTraining.htm"));  

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select crewnumber,firstname + ' ' + middlename + ' ' + lastname as CrewName , (select vesselcode from vessel where vesselid=currentvesselid) as vessel, (select rankcode from rank where rankid=currentrankid) as rank from crewpersonaldetails where crewid=" + Session["CrewId"].ToString());
        if (dt.Rows.Count > 0)
        {
            HTMLFile = HTMLFile.Replace("$VESSELCODE$", dt.Rows[0]["vessel"].ToString());
            HTMLFile = HTMLFile.Replace("$CREWNUMBER$", dt.Rows[0]["crewnumber"].ToString());
            HTMLFile = HTMLFile.Replace("$CREWNAME$", dt.Rows[0]["CrewName"].ToString());
            HTMLFile = HTMLFile.Replace("$RANK$", dt.Rows[0]["rank"].ToString());
        }
        int SrNo = 1;
        for (int i = 0; i <= TrainingMatrix.Rows.Count - 1; i++)
        {
            //if (Convert.ToDateTime(TrainingMatrix.Rows[i]["NextDue"].ToString()) <= DateTime.Today.AddDays(60))
            //{
                DataRows = DataRows + GetdataRow(SrNo.ToString(), TrainingMatrix.Rows[i]["TrainingId"].ToString(), TrainingMatrix.Rows[i]["TrainingName"].ToString(), Convert.ToDateTime(TrainingMatrix.Rows[i]["NextDue"].ToString()).ToString("dd-MMM-yyyy"));
                SrNo = SrNo + 1;
            //}
        }
        HTMLFile = HTMLFile.Replace("$MaxRowCount$", TrainingMatrix.Rows.Count.ToString());
        HTMLFile = HTMLFile.Replace("$DATAROWS$", DataRows);

        File.WriteAllText(Server.MapPath("~/CrewOperation/CrewTrainingRequirement.htm"), HTMLFile);
        aDownLoadFile.Visible = true;
        aDownLoadFile.Attributes.Add("href", "~/CrewOperation/CrewTrainingRequirement.htm");

       
    }
    private static void releaseObject(object obj)
    {
        try
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
            obj = null;
        }
        catch
        {
            obj = null;
        }
        finally
        {
            GC.Collect();
        }
    }
    protected void Export_PDF(object sender, EventArgs e)
    {
        try
        {
            Document document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4.Rotate(), 10, 10, 10, 10);
            System.IO.MemoryStream msReport = new System.IO.MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, msReport);
            document.AddAuthor("eMANAGER");
            document.AddSubject("Monthly Owner’s Technical & Operating Report (MOTOR)");
            //'Adding Header in Document
            iTextSharp.text.Image logoImg = default(iTextSharp.text.Image);
            logoImg = iTextSharp.text.Image.GetInstance(Server.MapPath("~\\Images\\Logo\\logo.png"));
            Chunk chk = new Chunk(logoImg, 0, 0, true);
            //Phrase p1 = new Phrase();
            //p1.Add(chk);
            Font f_head = FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.BOLD);
            Font f_7 = FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.BOLD);

            iTextSharp.text.Table tb_header = new iTextSharp.text.Table(1);
            tb_header.Width = 100;
            tb_header.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tb_header.DefaultCellBorder = iTextSharp.text.Rectangle.NO_BORDER;
            tb_header.DefaultHorizontalAlignment = Element.ALIGN_LEFT;
            tb_header.BorderWidth = 0;
            tb_header.BorderColor = iTextSharp.text.Color.WHITE;
            tb_header.Cellspacing = 1;
            tb_header.Cellpadding = 1;

            Phrase p2 = new Phrase();
            p2.Add(new Phrase("\n\nMTM SHIP MANAGEMENT PTE LTD " + "", FontFactory.GetFont("ARIAL", 18, iTextSharp.text.Font.BOLD)));
            Font BlackText = FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLACK);
            Font BlueText = FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.BLUE);

            Font RedText = FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.RED);

            Cell c2 = new Cell(p2);
            c2.HorizontalAlignment = Element.ALIGN_CENTER;
            tb_header.AddCell(c2);

            Phrase p3 = new Phrase();
            p3.Add(new Phrase("CREW TRAINING MATRIX " + "\n", FontFactory.GetFont("ARIAL", 15, iTextSharp.text.Font.BOLD)));
            Cell c3 = new Cell(p3);
            c3.HorizontalAlignment = Element.ALIGN_CENTER;
            tb_header.AddCell(c3);

            HeaderFooter header = new HeaderFooter(new Phrase(""), false);
            document.Header = header;

            ////header.Border = iTextSharp.text.Rectangle.NO_BORDER;
            string foot_Txt = "";
            foot_Txt = foot_Txt + "                                                                                                                ";
            foot_Txt = foot_Txt + "                                                                                                                ";
            foot_Txt = foot_Txt + "";
            HeaderFooter footer = new HeaderFooter(new Phrase(foot_Txt, FontFactory.GetFont("VERDANA", 6, iTextSharp.text.Color.DARK_GRAY)), true);
            footer.Border = iTextSharp.text.Rectangle.NO_BORDER;
            footer.Alignment = Element.ALIGN_LEFT;
            document.Footer = footer;
            //'-----------------------------------
            document.Open();
            document.Add(tb_header);

            iTextSharp.text.Table tb_crew = new iTextSharp.text.Table(6);
            tb_crew.Width = 100;
            tb_crew.Border = iTextSharp.text.Rectangle.NO_BORDER;
            tb_crew.DefaultCellBorder = iTextSharp.text.Rectangle.NO_BORDER;
            tb_crew.DefaultHorizontalAlignment = Element.ALIGN_LEFT;
            tb_crew.BorderWidth = 0;
            tb_crew.BorderColor = iTextSharp.text.Color.WHITE;
            tb_crew.DefaultVerticalAlignment = Element.ALIGN_TOP;
            tb_crew.Cellspacing = 1;
            tb_crew.Cellpadding = 1;

            DataTable dt_crew = Common.Execute_Procedures_Select_ByQueryCMS("select CrewNumber,firstname + ' ' + middlename + ' ' + lastname as CrewName,(select rankcode from rank where rankid=currentrankid) as Rank, (select vesselname from vessel where vesselid=currentvesselid) as Vessel,CrewStatusName,'" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt") + "' as PrintedOn from crewpersonaldetails inner join crewstatus cs on cs.crewstatusid=crewpersonaldetails.crewstatusid where crewid=" + Session["CrewId"].ToString());
            string[] Cap = { "Crew # : ", "Crew Name : ", "Rank : ", "Vessel : ", "Crew Status : ", "Printed On : " };
            for (int i = 0; i <= 5; i++)
            {
                Cell c_1 = new Cell(new Phrase(Cap[i]));
                c_1.HorizontalAlignment = Element.ALIGN_RIGHT;
                c_1.VerticalAlignment = Element.ALIGN_TOP;
                tb_crew.AddCell(c_1);

                Cell c_2 = new Cell(new Phrase(dt_crew.Rows [0][i].ToString()));
                c_2.HorizontalAlignment = Element.ALIGN_LEFT;
                c_1.VerticalAlignment = Element.ALIGN_TOP;
                tb_crew.AddCell(c_2);
            }
            document.Add(tb_crew);
            document.Add(new Phrase("\n")); 
            //// ================================= DATA =================================
            DataTable dt_data = Common.Execute_Procedures_Select_ByQueryCMS("EXEC sp_getTrainingMatrix " + Session["CrewId"].ToString());

            iTextSharp.text.Table tb_DATA = new iTextSharp.text.Table(6);
            tb_DATA.Width = 100;
            float[] ws = { 25, 7, 7, 7, 7, 57 };
            tb_DATA.Widths = ws;
            //tb_crew.Border = iTextSharp.text.Rectangle.NO_BORDER;
            //tb_crew.DefaultCellBorder = iTextSharp.text.Rectangle.NO_BORDER;
            tb_DATA.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
            tb_DATA.BorderWidth = 0;
            tb_DATA.BorderColor = iTextSharp.text.Color.BLACK;
            tb_DATA.Cellspacing = 0;
            tb_DATA.Cellpadding = 1;
            string[] Cap1 = { "Training Name", "Schedule", "Source", "Next Due Dt.", "Plan Dt.", "Records of Training Done" };
            for (int i = 0; i <= 5;i++ )
            {
                Cell c_1 = new Cell(new Phrase(Cap1[i], f_7));
                c_1.BackgroundColor = Color.LIGHT_GRAY; 
                c_1.HorizontalAlignment = Element.ALIGN_CENTER;
                tb_DATA.AddCell(c_1);
            }
            for (int i = 0; i <= dt_data.Rows.Count - 1; i++)
            {
                

                //------------------------------ GET SIMILER TRAININGS
                string SimilerTrainings = "select trainingid,TrainingName,(select TrainingTypeName from trainingtype tt where tt.trainingtypeid=t.typeoftraining) as TrainingTypeName from Training t " +
                                         "where t.trainingid in (select d.SimilerTrainingId from TrainingSimiler d where d.TrainingId=" + dt_data.Rows[i]["TRAININGID"].ToString() + ") " +
                                         "union select trainingid,TrainingName,(select TrainingTypeName from trainingtype tt where tt.trainingtypeid=t.typeoftraining) as TrainingTypeName from Training t  " +
                                         "where t.trainingid in (select d.TrainingId from TrainingSimiler d where d.SimilerTrainingId=" + dt_data.Rows[i]["TRAININGID"].ToString() + ")";
                DataTable dtSimiller = Common.Execute_Procedures_Select_ByQueryCMS(SimilerTrainings);
                SimilerTrainings = dt_data.Rows[i]["TRAININGID"].ToString();
                foreach (DataRow drs in dtSimiller.Rows)
                {
                    SimilerTrainings += "," + drs[0].ToString();
                }
                //-----
                string SimilerTrainingsName = "";
                foreach (DataRow drs in dtSimiller.Rows)
                {
                    SimilerTrainingsName += "," + drs[1].ToString() + " [ " + drs[2].ToString() + " ]";
                }
                if (SimilerTrainingsName != "")
                    SimilerTrainingsName = SimilerTrainingsName.Substring(1);
                SimilerTrainingsName = SimilerTrainingsName.Replace(",", " - OR - \n");
                //------------------------------------

                Cell c_1 = new Cell(new Phrase(dt_data.Rows[i][1].ToString() + " - OR - \n" + SimilerTrainingsName, f_7));
                c_1.HorizontalAlignment = Element.ALIGN_LEFT;
                tb_DATA.AddCell(c_1);

                string SCH = dt_data.Rows[i]["schcount"].ToString() + "-" + dt_data.Rows[i]["schtype"].ToString();
                if (SCH.Trim() == "0-")
                    SCH = "";

                Cell c_2 = new Cell(new Phrase(SCH, f_7));
                c_2.HorizontalAlignment = Element.ALIGN_CENTER;
                tb_DATA.AddCell(c_2);

                Cell c_21 = new Cell(new Phrase(dt_data.Rows[i][2].ToString(), f_7));
                c_21.HorizontalAlignment = Element.ALIGN_CENTER;
                tb_DATA.AddCell(c_21);

                DateTime dtp = Convert.ToDateTime(dt_data.Rows[i]["NEXTDUE"]);
                Cell c_3;
                if (dtp < DateTime.Today.AddYears(4))
                    c_3 = new Cell(new Phrase(Common.ToDateString(dt_data.Rows[i]["NEXTDUE"]), RedText));
                else
                    c_3 = new Cell(new Phrase(Common.ToDateString(dt_data.Rows[i]["NEXTDUE"]), f_7));

                c_3.HorizontalAlignment = Element.ALIGN_CENTER;
                tb_DATA.AddCell(c_3);

                string Plandate = "";
                DataTable dt_Plan = Common.Execute_Procedures_Select_ByQueryCMS("select dbo.sp_getNextPlanDate(" + Session["CrewId"].ToString() + "," + dt_data.Rows[i]["TRAININGID"].ToString() + ")");
                if (dt_Plan.Rows.Count > 0)
                {
                    try
                    {
                        Plandate = Convert.ToDateTime(dt_Plan.Rows[0][0]).ToString("dd-MMM-yyyy");
                    }
                    catch { }
                }

                Cell c_31 = new Cell(new Phrase(Plandate, f_7));
                c_31.HorizontalAlignment = Element.ALIGN_CENTER;
                tb_DATA.AddCell(c_31);

                //------------------------------------
                DataTable dtdone = Common.Execute_Procedures_Select_ByQueryCMS("SELECT REPLACE(convert(varchar,TODATE,106),' ','-') AS DONEDATE,N_CrewTrainingStatus,REPLACE(ISNULL(CONVERT(VARCHAR,PlannedFor,106),''),' ','-') AS PlannedFor " +
                    " FROM CREWTRAININGREQUIREMENT WHERE CREWID=" + Session["CrewId"].ToString() + " AND TRAININGID IN(" + SimilerTrainings + ") AND (N_CREWTRAININGSTATUS='C' OR PlannedFor IS NOT NULL) AND STATUSID='A' ORDER BY TODATE desc");
                string Res = "";
                Cell c_4 = new Cell();
                c_4.HorizontalAlignment = Element.ALIGN_LEFT;
                bool start = true;
                if (dtdone.Rows.Count > 0)
                {
                    foreach (DataRow drd in dtdone.Rows)
                    {
                        string Mode = ((drd[1].ToString() == "C") ? "D" : "P");
                        Res = ((start) ? "" : ",") + ((Mode == "P") ? drd[2].ToString() : drd[0].ToString());
                        if (Mode == "P")
                            c_4.Add(new Phrase(Res, BlackText));
                        else
                            c_4.Add(new Phrase(Res, BlackText));
                        start = false;
                    }
                }
                tb_DATA.AddCell(c_4);
            }
            document.Add(tb_DATA);

         
            // ==========================================================================================================
            document.NewPage();
            document.Close();
            if (File.Exists(Server.MapPath("~/UserUploadedDocuments/CrewTrainings.pdf")))
            {
                File.Delete(Server.MapPath("~/UserUploadedDocuments/CrewTrainings.pdf"));
            }

            FileStream fs = new FileStream(Server.MapPath("~/UserUploadedDocuments/CrewTrainings.pdf"), FileMode.Create);
            byte[] bb = msReport.ToArray();
            fs.Write(bb, 0, bb.Length);
            fs.Flush();
            fs.Close();
            Random r = new Random();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "a", "window.open('../UserUploadedDocuments/CrewTrainings.pdf?rand" + r.Next().ToString() + "');", true);
        }
        catch (System.Exception ex)
        {
            //lblmessage.Text = ex.Message.ToString();
        }
    }
    protected void btn_UpdateTraining_Click(object sender, EventArgs e)
    {
        int PkId = Common.CastAsInt32(hfdPKId.Value);
        int Tid = Common.CastAsInt32(hfdTId.Value);
        string DD = hfdDD.Value;
        string sql = "EXEC DBO.Update_Training " + PkId.ToString() + "," + Session["CrewId"].ToString() + "," + Tid.ToString() + ",'" + DD + "','" + txt_FromDate.Text + "','" + txt_ToDate.Text + "'," + DropDownList1.SelectedValue + "," + Session["LoginId"].ToString();
        Common.Execute_Procedures_Select_ByQueryCMS(sql);
        txt_FromDate.Text = "";
        txt_ToDate.Text = "";
        DropDownList1.SelectedIndex = 0;
        BindTrainingMatrix();
    }
    # endregion
    //--------
    # region Tab-6 Training Matrix
    protected void BindCuisuine()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM CREWCUISINEDETAILS WHERE CREWID=" + Session["CrewId"].ToString());
        rpt_Menus.DataSource = dt;
        rpt_Menus.DataBind();
    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        int MenuId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        Common.Execute_Procedures_Select_ByQueryCMS("DELETE FROM CREWCUISINEDETAILS WHERE MENUID=" + MenuId.ToString());
        BindCuisuine();
    }
    protected void lnkViewContracts_Click(object sender, EventArgs e)
    {
        int MenuId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        hfdMenuId.Value = MenuId.ToString();
        dvContracts.Visible = true;

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select CONTRACTREFERENCENUMBER,MENUNAME,STARTDATE,ENDDATE from " +
                                                                    "CrewContractCuisuineDetails d inner join crewcontractheader c on c.contractid=d.contractid " + 
                                                                    "inner join dbo.CrewCuisineDetails m on m.menuid=d.menuid where m.Menuid=" + MenuId.ToString()); 
        if(dt.Rows.Count >0)
        {
            rptContracts.DataSource = dt;
            rptContracts.DataBind();
        }
    }
    protected void btnClose1_Click(object sender, EventArgs e)
    {
        dvContracts.Visible = false;
    }
    protected void lnkView_Click(object sender, EventArgs e)
    {
        int MenuId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        hfdMenuId.Value = MenuId.ToString();
        dvAddEditMenu.Visible = true;

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM CrewCuisineDetails WHERE MENUID=" + MenuId.ToString()); 
        if(dt.Rows.Count >0)
        {
            hfdMenuId.Value = dt.Rows[0]["MenuId"].ToString();
            txtMenuName.Text = dt.Rows[0]["MenuName"].ToString();
            txtMethod.Text = dt.Rows[0]["Method"].ToString();
            txtIng.Text = dt.Rows[0]["Ingredints"].ToString();
            txtSize.Text = dt.Rows[0]["ServingSize"].ToString();

            btnMenuSave.Visible = false; 
        }
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        int MenuId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        hfdMenuId.Value = MenuId.ToString();
        dvAddEditMenu.Visible = true;

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM CrewCuisineDetails WHERE MENUID=" + MenuId.ToString()); 
        if(dt.Rows.Count >0)
        {
            hfdMenuId.Value = dt.Rows[0]["MenuId"].ToString();
            txtMenuName.Text = dt.Rows[0]["MenuName"].ToString();
            txtMethod.Text = dt.Rows[0]["Method"].ToString();
            txtIng.Text = dt.Rows[0]["Ingredints"].ToString();
            txtSize.Text = dt.Rows[0]["ServingSize"].ToString();
        }
    }
    protected void btnAddMenu_Click(object sender, EventArgs e)
    {
        hfdMenuId.Value = "";
        txtMenuName.Text = "";
        txtMethod.Text = "";
        txtIng.Text = "";
        txtSize.Text = "";
        dvAddEditMenu.Visible = true; 
    }
    protected void btnAddCancel_Click(object sender, EventArgs e)
    {
       
        dvAddEditMenu.Visible = false; 
    }
    protected void btnMenuSave_Click(object sender, EventArgs e)
    {
        if (txtMenuName.Text.Trim() == "")
        {
            txtMenuName.Focus();
            lblMsg.Text = "Please enter Menu Name.";
            return; 
        }

        if (txtIng.Text.Trim() == "")
        {
            txtIng.Focus();
            lblMsg.Text = "Please enter Ingredients.";
            return;
        }
        try
        {
            int CrewId=Common.CastAsInt32(Session["CrewId"]);
            int MenuId = Common.CastAsInt32(hfdMenuId.Value);
            if (MenuId <= 0)
            {
                Common.Execute_Procedures_Select_ByQueryCMS("INSERT INTO CrewCuisineDetails(CrewId,MenuName,Ingredints,Method,ServingSize) VALUES(" + CrewId.ToString() + ",'" + txtMenuName.Text.Replace("'", "`") + "','" + txtIng.Text.Replace("'", "`") + "','" + txtMethod.Text.Replace("'", "`") + "','" + txtSize.Text.Replace("'", "`") + "')");
            }
            else
            {
                Common.Execute_Procedures_Select_ByQueryCMS("UPDATE CrewCuisineDetails SET MenuName='" + txtMenuName.Text.Replace("'", "`") + "',Ingredints='" + txtIng.Text.Replace("'", "`") + "',Method='" + txtMethod.Text.Replace("'", "`") + "',ServingSize='" + txtSize.Text.Replace("'", "`") + "' WHERE MENUID=" + MenuId.ToString());
            }
            BindCuisuine();
            dvAddEditMenu.Visible = false; 
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to save recrod. Error : " + Common.ErrMsg;
        }
    }
    #endregion


    protected void gvtrainingrequirement_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void gvtrainingrequirement_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
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

                this.lblMessage.Text = "";
                HiddenField hfd, hfContractStauts;
                gv_Contract.SelectedIndex = Rowindx;
                hfd = (HiddenField)gv_Contract.Rows[Rowindx].FindControl("hfd_ContractId");
            hfContractStauts = (HiddenField)gv_Contract.Rows[gv_Contract.SelectedIndex].FindControl("hfContractStauts");
            if (Convert.ToString(hfContractStauts.Value.ToUpper()) == "OPEN")
            {
                btnConRevision.Enabled = true;
            }
            else
            {
                btnConRevision.Enabled = false;
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

    protected void gv_Contract_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

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

    protected void btnCloseContractRevision_Click(object sender, EventArgs e)
    {
        dvContractRevision.Visible = false;
        Show_Contract_crew(Convert.ToInt16(HiddenPK.Value));
    }

    

    protected void btnConRevision_Click(object sender, EventArgs e)
    {
        lblContractRevisionmessage.Text = "";
        dvContractRevision.Visible = true;
        try
        {
            LoadCrewPromotionDetails();
        }
        catch(Exception ex)
        {
            lblContractRevisionmessage.Text = ex.Message.ToString();
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

    
}