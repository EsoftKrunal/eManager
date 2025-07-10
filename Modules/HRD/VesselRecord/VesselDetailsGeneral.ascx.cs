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
using System.IO;

public partial class VesselRecord_VesselDetailsGeneral : System.Web.UI.UserControl
{
    Authority Auth;
    public Label ErrorLabel;
    string Mode;
    private int _vesselid;
    public int VesselId
    {
        get { return _vesselid; }
        set { _vesselid = value; }
    }
    public void show_Message(string mess)
    {
        try
        {
            ErrorLabel.Text = mess;
        }
        catch { }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 36);
        if (chpageauth <= 0)
        {
            Response.Redirect("../AuthorityError.aspx");
        }
        //**********
        lbl_message_general.Text = "";
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 4);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        try
        {
            Mode = Session["VMode"].ToString();
        }
        catch { Mode = "New"; }
        if (!Page.IsPostBack)
        {
            RangeValidator6.MaximumValue = Convert.ToString(System.DateTime.Now.Year);
            BindFlagNameDropDown();
            BindMgmtTypeDropDown();
            BindOwnerDropDown();
            BindAccountCompanyDropDown();
            BindChartererDropDown();
            BindShipManagerDropDown();
            BindOwnerAgentDropDown();
            BindMLCOwnerDropDown();
            BindManningAgentDropDown();
            BindPIClubDropDown();
            BindCrewNationalityDropDown();
            BindStatusDropDown();
            bindVesselTypeNameDDL();
            bindPortOfRegistryNameDDL();
            bindCurrentClassNameDDL();
            LoadFleetDDL();
            btn_vessel_general_save.Visible = ((Mode == "New") || (Mode == "Edit")) && ((Auth.isAdd) || (Auth.isEdit));
            btn_Print_vessel_general.Visible = Auth.isPrint; 
            Show_Record_Vessel_General( VesselId);
        }
    }
    private void Show_Record_Vessel_General(int VesselId)
    {
        string Mess= "";
        DataTable dt11 = VesselDetailsGeneral.selectDataVesselNameByVesselHistory(VesselId);
        foreach (DataRow dr in dt11.Rows)
        {
            txtformaer_vessel.Text = dr["VesselName"].ToString();
        }
       
        DataTable dt10 = VesselDetailsGeneral.selectDataVesselGeneralByVesselId(VesselId);
        foreach (DataRow dr in dt10.Rows)
        {

            txtvesselname_general.Text = dr["VesselName"].ToString();
            
            Mess = Mess + Alerts.Set_DDL_Value(ddflag_general, dr["FlagStateId"].ToString(), "FlagState");

            txtvessel_code.Text = dr["VesselCode"].ToString();
            
            
            Mess = Mess + Alerts.Set_DDL_Value(ddmgmttype, dr["ManagementTypeId"].ToString(), "ManagementType");
            
            
            Mess = Mess + Alerts.Set_DDL_Value(ddowner, dr["OwnerId"].ToString(), "Owner");
            
            
            Mess = Mess + Alerts.Set_DDL_Value(ddcharterer, dr["ChartererId"].ToString(), "Charterer");
            
           
            Mess = Mess + Alerts.Set_DDL_Value(ddship_manager, dr["ShipManagerId"].ToString(), "ShipManager");
            Mess = Mess + Alerts.Set_DDL_Value(ddl_ManningAgent, dr["ManningAgentId"].ToString(), "ManningAgent");
            Mess = Mess + Alerts.Set_DDL_Value(ddlOwnerAgent, dr["OwnerAgentId"].ToString(), "OwnerAgent");
            Mess = Mess + Alerts.Set_DDL_Value(ddl_MLCOwner, dr["MLCOwnerId"].ToString(), "MLCOwner");

            Mess = Mess + Alerts.Set_DDL_Value(ddp_i_club, dr["PNIClubId"].ToString(), "PNIClub");

            
            //Mess = Mess + Alerts.Set_DDL_Value(ddcrew_nationality, dr["NationalityGroupId"].ToString(), "NationalityGroup(Officer)");
            
            
            //Mess = Mess + Alerts.Set_DDL_Value(ddcrew_nationality1, dr["NationalityGroupIdRat"].ToString(), "NationalityGroupId(Rating)");
            
            
            Mess = Mess + Alerts.Set_DDL_Value(dd_status_general, dr["VesselStatusId"].ToString(), "VesselStatus");
            
            
            Mess = Mess + Alerts.Set_DDL_Value(ddvesselType_General, dr["VesselTypeId"].ToString(), "VesselType");

            Mess = Mess + Alerts.Set_DDL_Value(ddlAccountCompany, dr["AccontCompany"].ToString(), "CompanyName"); 
            txtLRIMONumber.Text = dr["LRIMONumber"].ToString();
            
            
            Mess = Mess + Alerts.Set_DDL_Value(ddlPortOfRegistry, dr["PortOfRegistryId"].ToString(), "PortOfRegistry");
            txtOfficialNumber.Text = dr["OfficialNumber"].ToString();
            txtMMSINumber.Text = dr["MMSINumber"].ToString();
            
            
            Mess = Mess + Alerts.Set_DDL_Value(ddlCurrentClassName, dr["CurrentClassId"].ToString(), "CurrentClass");
            Mess = Mess + Alerts.Set_DDL_Value(ddlFleet, dr["FleetId"].ToString(), "FleetMaster");

            foreach (ListItem li in ddcrew_nationality.Items)
            {
                if (dr["NationalityGroupId"].ToString().Contains(li.Value))
                    li.Selected = true;
            }

            foreach (ListItem li in ddcrew_nationality1.Items)
            {
                if (dr["NationalityGroupIdRat"].ToString().Contains(li.Value))
                    li.Selected = true;
            }

            txtYearBuilt.Text = dr["YearBuilt"].ToString();
            txtAge.Text = dr["Age"].ToString();
            txtYard.Text = dr["Yard"].ToString();
            txtLDT.Text = dr["LDT"].ToString();
            txtTEU.Text = dr["TEU"].ToString();
            txtDraught.Text = dr["Draught"].ToString();
            txt_EffectiveDate.Text = dr["EffectiveDate"].ToString();
            txtLength.Text = dr["Length"].ToString();
            txtBreath.Text = dr["Breath"].ToString();
            txtDepth.Text = dr["Depth"].ToString();
            txtCallSign.Text = dr["CallSign"].ToString();
           // txtShipOperator.Text = dr["ShipOperator"].ToString();
            //txtVEmail.Text = dr["GroupEmail"].ToString();  
            cklwagescale.DataSource = VesselDetailsGeneral.selectWageScale(VesselId);
            cklwagescale.DataTextField = "WageScaleName";
            cklwagescale.DataValueField = "WageScaleId";
            cklwagescale.DataBind();

            chkVettingRequired.Checked = (dr["VettingRequired"].ToString() == "True");

            chkIsMultiAccount.Checked = (dr["IsMultiAccount"].ToString() == "True");
            Session.Add("VesselID",VesselId.ToString());
            Session["VesselName"] = txtvesselname_general.Text;
            Session["FormerName"] = txtformaer_vessel.Text;
            Session["FlagStateId"] = ddflag_general.SelectedValue;
            BindAccountCompanyHeader(chkIsMultiAccount.Checked);
            if (chkIsMultiAccount.Checked)
            {
                //chkPOIssingCompany.Items.Clear();
                foreach (ListItem li in chkPOIssingCompany.Items)
                {
                    if (dr["POIssuingCompanyId"].ToString().Contains(li.Value))
                        li.Selected = true;
                }
            }
            else
            {
                Mess = Mess + Alerts.Set_DDL_Value(ddlPOIssuingCompany, dr["POIssuingCompanyId"].ToString(), "AccountCompanyHeader");
            }

        }

        if (Mess.Length > 0)
        {
            this.lbl_message_general.Text = "The Status Of Following Has Been Changed To In-Active <br/>" + Mess.Substring(1);
            this.lbl_message_general.Visible = true;
        }
    }
    #region Page Loader
    private void BindFlagNameDropDown()
    {
        DataTable dt1 = VesselDetailsGeneral.selectDataFlag();
        this.ddflag_general.DataValueField = "FlagStateId";
        this.ddflag_general.DataTextField = "FlagStateName";
        this.ddflag_general.DataSource = dt1;
        this.ddflag_general.DataBind();
    }
    private void BindMgmtTypeDropDown()
    {
        DataTable dt2 = VesselDetailsGeneral.selectDataMgmtType();
        this.ddmgmttype.DataValueField = "ManagementTypeId";
        this.ddmgmttype.DataTextField = "ManagementTypeName";
        this.ddmgmttype.DataSource = dt2;
        this.ddmgmttype.DataBind();
    }
    private void BindOwnerDropDown()
    {
        DataTable dt3 = VesselDetailsGeneral.selectDataOwnerName();
        this.ddowner.DataValueField = "OwnerId";
        this.ddowner.DataTextField = "OwnerShortName";
        this.ddowner.DataSource = dt3;
        this.ddowner.DataBind();
    }
    private void BindAccountCompanyDropDown()
    {
        DataTable dtAccountCompany = VesselDetailsGeneral.selectDataAccountCompany();
        this.ddlAccountCompany.DataValueField = "Company";
        this.ddlAccountCompany.DataTextField = "CompanyName";
        this.ddlAccountCompany.DataSource = dtAccountCompany;
        this.ddlAccountCompany.DataBind();
    }
    private void BindChartererDropDown()
    {
        DataTable dt4 = VesselDetailsGeneral.selectDataCharterer();
        this.ddcharterer.DataValueField = "ChartererId";
        this.ddcharterer.DataTextField = "ChartererName";
        this.ddcharterer.DataSource = dt4;
        this.ddcharterer.DataBind();
    }
   
    private void BindPIClubDropDown()
    {
        DataTable dt6 = VesselDetailsGeneral.selectDataPIClub();
        this.ddp_i_club.DataValueField = "PNIClubId";
        this.ddp_i_club.DataTextField = "PNIClubName";
        this.ddp_i_club.DataSource = dt6;
        this.ddp_i_club.DataBind();
    }
    private void BindCrewNationalityDropDown()
    {
        String sql = "SELECT NationalityGroupId,NationalityGroupName from NationalityGroup where statusid='A'";
        DataTable dt7 =  Budget.getTable(sql).Tables[0];
        this.ddcrew_nationality.DataValueField = "NationalityGroupId";
        this.ddcrew_nationality.DataTextField = "NationalityGroupName";
        this.ddcrew_nationality.DataSource = dt7;
        this.ddcrew_nationality.DataBind();

        this.ddcrew_nationality1.DataValueField = "NationalityGroupId";
        this.ddcrew_nationality1.DataTextField = "NationalityGroupName";
        this.ddcrew_nationality1.DataSource = dt7;
        this.ddcrew_nationality1.DataBind();

    }
    private void BindManningAgentDropDown()
    {
        String sql = "select 0 as CompanyId,'<Select>' as CompanyName UNION Select CompanyId,CompanyName from ContractCompanyMaster with(nolock) where StatusId='A'  and IsManningAgent = 1";
        DataTable dt7 = Budget.getTable(sql).Tables[0];
        this.ddl_ManningAgent.DataValueField = "CompanyId";
        this.ddl_ManningAgent.DataTextField = "CompanyName";
        this.ddl_ManningAgent.DataSource = dt7;
        this.ddl_ManningAgent.DataBind();
    }
    private void BindOwnerAgentDropDown()
    {
        String sql = "select 0 as CompanyId,'<Select>' as CompanyName UNION Select CompanyId,CompanyName from ContractCompanyMaster with(nolock) where StatusId='A'  and IsOwnerAgent = 1";
        DataTable dt7 = Budget.getTable(sql).Tables[0];
        this.ddlOwnerAgent.DataValueField = "CompanyId";
        this.ddlOwnerAgent.DataTextField = "CompanyName";
        this.ddlOwnerAgent.DataSource = dt7;
        this.ddlOwnerAgent.DataBind();
    }
    private void BindMLCOwnerDropDown()
    {
        String sql = "select 0 as CompanyId,'<Select>' as CompanyName UNION Select CompanyId,CompanyName from ContractCompanyMaster with(nolock) where StatusId='A'  and IsMLCAgent = 1";
        DataTable dt7 = Budget.getTable(sql).Tables[0];
        this.ddl_MLCOwner.DataValueField = "CompanyId";
        this.ddl_MLCOwner.DataTextField = "CompanyName";
        this.ddl_MLCOwner.DataSource = dt7;
        this.ddl_MLCOwner.DataBind();
    }

    private void BindShipManagerDropDown()
    {
        //DataTable dt5 = VesselDetailsGeneral.selectDataShipManager();
        String sql = "select 0 as CompanyId,'<Select>' as CompanyName UNION Select CompanyId,CompanyName from ContractCompanyMaster with(nolock) where StatusId='A'  and IsShipManager = 1";
        DataTable dt7 = Budget.getTable(sql).Tables[0];
        this.ddship_manager.DataValueField = "CompanyId";
        this.ddship_manager.DataTextField = "CompanyName";
        this.ddship_manager.DataSource = dt7;
        this.ddship_manager.DataBind();
    }
    private void BindWageScaleDropDown()
    {
       
    }
    private void BindStatusDropDown()
    {
        DataTable dt9 = VesselDetailsGeneral.selectDataVesselStatus();
        this.dd_status_general.DataValueField = "VesselStatusId";
        this.dd_status_general.DataTextField = "VesselStatusName";
        this.dd_status_general.DataSource = dt9;
        this.dd_status_general.DataBind();
    }
    public void bindVesselTypeNameDDL()
    {
        DataTable dt12 = VesselDetailsGeneral.selectDataVesselTypeName();
        this.ddvesselType_General.DataValueField = "VesselTypeId";
        this.ddvesselType_General.DataTextField = "VesselTypeName";
        this.ddvesselType_General.DataSource = dt12;
        this.ddvesselType_General.DataBind();
    }
    public void bindPortOfRegistryNameDDL()
    {
        DataTable dt13 = VesselDetailsGeneral.selectDataPortOfRegistryDetails();
        this.ddlPortOfRegistry.DataValueField = "PortOfRegistryId";
        this.ddlPortOfRegistry.DataTextField = "PortOfRegistryName";
        this.ddlPortOfRegistry.DataSource = dt13;
        this.ddlPortOfRegistry.DataBind();
    }
    public void bindCurrentClassNameDDL()
    {
        DataTable dt14 = VesselDetailsGeneral.selectDataCurrentClassDetails();
        this.ddlCurrentClassName.DataValueField = "CurrentClassId";
        this.ddlCurrentClassName.DataTextField = "CurrentClassName";
        this.ddlCurrentClassName.DataSource = dt14;
        this.ddlCurrentClassName.DataBind();
    }

    public void BindAccountCompanyHeader(bool IsMultiPoAccount)
    {
        DataTable dt = VesselDetailsGeneral.selectDataPOIssuingCompany();
        if (IsMultiPoAccount)
        {
            this.chkPOIssingCompany.DataValueField = "CompanyId";
            this.chkPOIssingCompany.DataTextField = "CompanyName";
            this.chkPOIssingCompany.DataSource = dt;
            this.chkPOIssingCompany.DataBind();
            this.divPOIssingCompany.Visible = true;
            this.ddlPOIssuingCompany.Visible = false;
        }
        else
        {
            this.ddlPOIssuingCompany.DataValueField = "CompanyId";
            this.ddlPOIssuingCompany.DataTextField = "CompanyName";
            this.ddlPOIssuingCompany.DataSource = dt;
            this.ddlPOIssuingCompany.DataBind();
            ddlPOIssuingCompany.Items.Insert(0, new ListItem("< Select >", "0"));
            this.divPOIssingCompany.Visible = false;
            this.ddlPOIssuingCompany.Visible = true;
        }
    }

    public void LoadFleetDDL()
    {
        DataTable dt6 = Budget.getTable("SELECT * FROM FLEETMASTER ORDER BY FLEETNAME").Tables[0];
        this.ddlFleet.DataValueField = "FLEETID";
        this.ddlFleet.DataTextField = "FLEETNAME";
        this.ddlFleet.DataSource = dt6;
        this.ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    #endregion
    protected void btn_vessel_general_save_Click(object sender, EventArgs e)
    {
        int Duplicate = 0;

        if (FileUpload1.HasFile)
        {
            int inx=FileUpload1.PostedFile.FileName.LastIndexOf(".");
            string ext="";
            ext =FileUpload1.PostedFile.FileName.Substring(inx).ToLower();
            if (ext != ".jpg")
            {
                lbl_message_general.Text = "Please upload only jpg files.";
                lbl_message_general.Visible = true;
                return; 
            }
            FileUpload1.SaveAs(Server.MapPath("~/VesselRecord/VesselImages/" + "vessel_" + this.VesselId.ToString() + ".jpg"));    
        }

        DataTable dt2 = VesselSearch.selectDataVesselDetails(0,0,0,0,0,0);
        foreach (DataRow dr in dt2.Rows)
        {
            string hfd;
            int hfd1;
            hfd = dr["VesselName"].ToString();
            hfd1 =Convert.ToInt32(dr["VesselId"].ToString());
            if (hfd.ToUpper().Trim() == txtvessel_code.Text.ToUpper().Trim())
            {
                if (VesselId == null || VesselId == -1)
                {
                    Label2.Visible = true;
                    Duplicate = 1;
                    break;
                }
                else if (VesselId != hfd1)
                {
                    Label2.Visible = true;
                    Duplicate = 1;
                    break;
                }
            }
            else
            {
                Label2.Visible = false;
            }
        }

        if (Duplicate == 0)
        {
            int NewId;
            int createdby = 0, modifiedby = 0;
            int FlagStateId = Convert.ToInt32(ddflag_general.SelectedValue);
            int ManagementTypeId = Convert.ToInt32(ddmgmttype.SelectedValue);
            int OwnerId = Convert.ToInt32(ddowner.SelectedValue);
            int ChartererId = Convert.ToInt32(ddcharterer.SelectedValue);
            int ShipManagerId = Convert.ToInt32(ddship_manager.SelectedValue);
            int OwnerAgentId = Convert.ToInt32(ddlOwnerAgent.SelectedValue);
            int MLCOwnerId = Convert.ToInt32(ddl_MLCOwner.SelectedValue);
            int ManningAgentId = Convert.ToInt32(ddl_ManningAgent.SelectedValue);
            int PNIClubId = Convert.ToInt32(ddp_i_club.SelectedValue);
           // int NationalityGroupId = Convert.ToInt32(ddcrew_nationality.SelectedValue);
           // int NationalityGroupIdRat = Convert.ToInt32(ddcrew_nationality1.SelectedValue);
            int VesselStatusId = Convert.ToInt32(dd_status_general.SelectedValue);
            int VesselTypeId = Convert.ToInt32(ddvesselType_General.SelectedValue);
            int intPortOfRegistryId = Convert.ToInt32(ddlPortOfRegistry.SelectedValue);
            int intCurrentClassId = Convert.ToInt32(ddlCurrentClassName.SelectedValue);
            string AccountCompany = Convert.ToString(ddlAccountCompany.SelectedValue);

            string NationalityGroupId = "";
            
            foreach (ListItem li in ddcrew_nationality.Items)
            {
                if (li.Selected)
                    NationalityGroupId = NationalityGroupId + "," + li.Value;
            }
            if (NationalityGroupId.StartsWith(","))
            {
                NationalityGroupId = NationalityGroupId.Substring(1);
            }
            string NationalityGroupIdRat = "";
            foreach (ListItem li in ddcrew_nationality1.Items)
            {
                if (li.Selected)
                    NationalityGroupIdRat = NationalityGroupIdRat + "," + li.Value;
            }
            if (NationalityGroupIdRat.StartsWith(","))
            {
                NationalityGroupIdRat = NationalityGroupIdRat.Substring(1);
            }
            string strLRIMONumber = txtLRIMONumber.Text;
            string strAge = txtAge.Text;
            string strofficialNumber = txtOfficialNumber.Text;
            string strMMISMNumber = txtMMSINumber.Text;
            string strYearBuilt = txtYearBuilt.Text;
            string strYard = txtYard.Text;
            string strLtd = txtLDT.Text;
            string strTeu = txtTEU.Text;
            string strLength = txtLength.Text;
            string strBreath = txtBreath.Text;
            string strDepth = txtDepth.Text;
            string strDraught = txtDraught.Text;
            string streffectiveDate = txt_EffectiveDate.Text;

            string VesselName = txtvesselname_general.Text;
            string VesselCode = txtvessel_code.Text;
            //string strShipOperator = txtShipOperator.Text.Trim();
            string strCallSign = txtCallSign.Text.Trim();
           
            if (VesselId <= 0)
            {
                createdby = Convert.ToInt32(Session["loginid"].ToString());
            }
            else
            {
                modifiedby = Convert.ToInt32(Session["loginid"].ToString());
            }

            if (VesselStatusId == 2)
            {
                DataTable dt15 = VesselDetailsGeneral.selectDataCrewOnVessel(VesselId);
                if (dt15.Rows.Count > 0)
                {
                    lbl_message_general.Visible = false;
                    Label1.Visible = true;
                    return;
                }
            }
            bool IsMultiAccount = chkIsMultiAccount.Checked;
            int FleetId = Convert.ToInt32(ddlFleet.SelectedValue);
            string POIssuingAccountCompany = "";
            if (IsMultiAccount && divPOIssingCompany.Visible)
            {
                foreach (ListItem li in chkPOIssingCompany.Items)
                {
                    if (li.Selected)
                         POIssuingAccountCompany =  POIssuingAccountCompany +"," + li.Value;
                }
                if (POIssuingAccountCompany.StartsWith(","))
                {
                    POIssuingAccountCompany = POIssuingAccountCompany.Substring(1);
                }
            }
            else
            {
               if (ddlPOIssuingCompany.Visible)
                {
                    POIssuingAccountCompany = ddlPOIssuingCompany.SelectedValue;
                }
               
            }
            VesselDetailsGeneral.insertUpdateVesselGeneralDetails("InsertUpdateVesselGeneralDetails",
                                                                     out NewId,
                                                                     VesselId,
                                                                     VesselName,
                                                                     FlagStateId,
                                                                     VesselCode,
                                                                     ManagementTypeId,
                                                                     OwnerId,
                                                                     ChartererId,
                                                                     ShipManagerId,
                                                                     PNIClubId,
                                                                     NationalityGroupId,
                                                                     NationalityGroupIdRat,
                                                                     VesselStatusId,
                                                                     VesselTypeId,
                                                                     strLRIMONumber,
                                                                     intPortOfRegistryId,
                                                                     strofficialNumber,
                                                                     strMMISMNumber,
                                                                     intCurrentClassId,
                                                                     strYearBuilt,
                                                                     strAge,
                                                                     strYard,
                                                                     strLtd,
                                                                     strTeu,
                                                                     strLength,
                                                                     strBreath,
                                                                     strDepth,
                                                                     strDraught,
                                                                     streffectiveDate,
                                                                     createdby,
                                                                  modifiedby,"",chkVettingRequired.Checked, strCallSign, OwnerAgentId, MLCOwnerId, ManningAgentId, AccountCompany, IsMultiAccount, FleetId, POIssuingAccountCompany);

            lbl_message_general.Visible = true;
            Label1.Visible = false;
            VesselId = NewId;
            Session["VesselId"] = NewId;
            Session["VesselName"] = VesselName;
            Session["FormerName"] = txtformaer_vessel.Text;
            Session["FlagStateId"] = ddflag_general.SelectedValue;
        }
    }
    protected void btn_Print_vessel_general_Click(object sender, EventArgs e)
    {

    }


    protected void chkIsMultiAccount_CheckedChanged(object sender, EventArgs e)
    {
       try
        {
            BindAccountCompanyHeader(chkIsMultiAccount.Checked);
        }
        catch(Exception ex)
        {

        }
       
    }
}
