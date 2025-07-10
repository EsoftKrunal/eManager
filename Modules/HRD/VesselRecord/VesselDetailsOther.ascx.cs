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

public partial class VesselRecord_VesselDetailsOther : System.Web.UI.UserControl
{
    Authority Auth;
    string Mode;
    private int _VesselId;
    public int VesselId
    {
        get { return _VesselId; }
        set { _VesselId = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 37);
        if (chpageauth <= 0)
        {
            Response.Redirect("../AuthorityError.aspx");
        }
        //**********
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
            BindVesselManning();
            bindFlagStateNameDDL();
            bindStrokeTypeNameDDL();
            bindEngineMakerDDL();
            //LoadFleetDDL();
            btn_Save_VesselParticular2.Visible = ((Mode == "New") || (Mode == "Edit")) && ((Auth.isAdd) || (Auth.isEdit));
            Show_Record_VesselParticular2(VesselId);
            btn_Print_VesselParticular2.Visible = Auth.isPrint; 
        }
        try
        {
            txtVesselName.Text = Session["VesselName"].ToString();
            txtFormerVesselName.Text = Session["FormerName"].ToString();
            ddlFlagStateName.SelectedValue = Session["FlagStateId"].ToString();
        }
        catch(Exception se)
        {

        }
             
    }
    #region Page Loader
    public void bindEngineMakerDDL()
    {
        DataTable dt8 = VesselDetailsOther.selectDataEngineMakerNameDetails(1);
        this.ddlMainEngineMaker.DataValueField = "EngineMakerId";
        this.ddlMainEngineMaker.DataTextField = "EngineMakerName";
        this.ddlMainEngineMaker.DataSource = dt8;
        this.ddlMainEngineMaker.DataBind();
        DataTable dt9 = VesselDetailsOther.selectDataEngineMakerNameDetails(2);
        this.ddlAux1EngineMaker.DataValueField = "EngineMakerId";
        this.ddlAux1EngineMaker.DataTextField = "EngineMakerName";
        this.ddlAux1EngineMaker.DataSource = dt9;
        this.ddlAux1EngineMaker.DataBind();
        DataTable dt10 = VesselDetailsOther.selectDataEngineMakerNameDetails(3);
        this.ddlAux2EngineMaker.DataValueField = "EngineMakerId";
        this.ddlAux2EngineMaker.DataTextField = "EngineMakerName";
        this.ddlAux2EngineMaker.DataSource = dt10;
        this.ddlAux2EngineMaker.DataBind();
        DataTable dt11 = VesselDetailsOther.selectDataEngineMakerNameDetails(4);
        this.ddlAux3EngineMaker.DataValueField = "EngineMakerId";
        this.ddlAux3EngineMaker.DataTextField = "EngineMakerName";
        this.ddlAux3EngineMaker.DataSource = dt11;
        this.ddlAux3EngineMaker.DataBind();
    }
    public void bindFlagStateNameDDL()
    {
        DataTable dt1 = VesselDetailsOther.selectDataFalgStateNameDetails();
        this.ddlFlagStateName.DataValueField = "FlagStateId";
        this.ddlFlagStateName.DataTextField = "FlagStateName";
        this.ddlFlagStateName.DataSource = dt1;
        this.ddlFlagStateName.DataBind();
    }
   
    public void bindStrokeTypeNameDDL()
    {
        DataTable dt6 = VesselDetailsOther.selectDataStrokeTypeNameDetails();
        this.ddlMainStrokeType.DataValueField = "StrokeTypeId";
        this.ddlMainStrokeType.DataTextField = "StrokeTypeName";
        this.ddlMainStrokeType.DataSource = dt6;
        this.ddlMainStrokeType.DataBind();

        this.ddlAux1StrokeType.DataValueField = "StrokeTypeId";
        this.ddlAux1StrokeType.DataTextField = "StrokeTypeName";
        this.ddlAux1StrokeType.DataSource = dt6;
        this.ddlAux1StrokeType.DataBind();

        this.ddlAux2StrokeType.DataValueField = "StrokeTypeId";
        this.ddlAux2StrokeType.DataTextField = "StrokeTypeName";
        this.ddlAux2StrokeType.DataSource = dt6;
        this.ddlAux2StrokeType.DataBind();

        this.ddlAux3StrokeType.DataValueField = "StrokeTypeId";
        this.ddlAux3StrokeType.DataTextField = "StrokeTypeName";
        this.ddlAux3StrokeType.DataSource = dt6;
        this.ddlAux3StrokeType.DataBind();
    }
    
    #endregion
    protected void btn_Save_VesselParticular2_Click(object sender, EventArgs e)
    {
            int createdby = 0, modifiedby = 0;
            
            int intMainEngineStrokeTypeId=Convert.ToInt32(ddlMainStrokeType.SelectedValue);
            int intAux1EngineStrokeTypeId = Convert.ToInt32(ddlAux1StrokeType.SelectedValue);
            int intAux2EngineStrokeTypeId = Convert.ToInt32(ddlAux2StrokeType.SelectedValue);
            int intAux3EngineStrokeTypeId = Convert.ToInt32(ddlAux3StrokeType.SelectedValue);

            int intMainEngineDesignMakerId = Convert.ToInt32(ddlMainEngineMaker.SelectedValue);
            int intAux1EngineMakerId = Convert.ToInt32(ddlAux1EngineMaker.SelectedValue);
            int intAux2EngineMakerId = Convert.ToInt32(ddlAux2EngineMaker.SelectedValue);
            int intAux3EngineMakerId = Convert.ToInt32(ddlAux3EngineMaker.SelectedValue);
            
            string InmarsatTerminalType = txtinmarsatTerminal.Text;
            string CallSign = txtcallsign.Text;
            string Telephone1 = txttel1.Text;
            string Telephone2 = txttel2.Text;
            string Mobile = txtmobile.Text;
            string Fax = txtfax.Text;
            string Email = txtemail.Text;
            string Data = txtdata.Text;
            string HSD = txthsd.Text;
            string InmarsatC = txtinmarsat.Text;

            string strMainEngineModel = txtMainEngineModel.Text;
            string strAux1EngineModel = txtAux1EngineKW.Text;
            string strAux2EngineModel = txtAux2EngineModel.Text;
            string strAux3EngineModel = txtAux3EngineModel.Text;
            string strMainEngineKW = txtMainEngineKW.Text;
            string strAux1EngineKW = txtAux1EngineKW.Text;
            string strAux2EngineKW = txtAux2EngineKW.Text;
            string strAux3EngineKW = txtAux3EngineKW.Text;
            string strMainBHP = txtMainBHP.Text;
            string strAux1BHP = txtAux1BHP.Text;
            string strAux2BHP = txtAux2BHP.Text;
            string strAux3BHP = txtAux3BHP.Text;
            string strMainRPM = txtMainRPM.Text;
            string strAux1RPM = txtAux1RPM.Text;
            string strAux2RPM = txtAux2RPM.Text;
            string strAux3RPM = txtAux3RPM.Text;
            string AccCode = txtAccCode.Text.Trim();
            float TrainingFee =0;
            try
            {
                TrainingFee = float.Parse(txtTrainingFee.Text);
            }
            catch { }
            if (VesselId <= 0)
            {
                createdby = Convert.ToInt32(Session["loginid"].ToString());
            }
            else
            {
                modifiedby = Convert.ToInt32(Session["loginid"].ToString());
            }

            VesselDetailsOther.updateVesselParticulars2("UpdateVesselParticulars2Details",
                                          VesselId,
                                          InmarsatTerminalType,
                                          CallSign,
                                          Telephone1,
                                          Telephone2,
                                          Mobile,
                                          Fax,
                                          Email,
                                          Data,
                                          HSD,
                                          InmarsatC,
                                          intMainEngineDesignMakerId,
                                          intAux1EngineMakerId,
                                          intAux2EngineMakerId,
                                          intAux3EngineMakerId,
                                          intMainEngineStrokeTypeId,
                                          intAux1EngineStrokeTypeId,
                                          intAux2EngineStrokeTypeId,
                                          intAux3EngineStrokeTypeId,
                                          strMainEngineModel,
                                          strAux1EngineModel,
                                          strAux2EngineModel,
                                          strAux3EngineModel,
                                          strMainEngineKW,
                                          strAux1EngineKW,
                                          strAux2EngineKW,
                                          strAux3EngineKW,
                                          strMainBHP,
                                          strAux1BHP,
                                          strAux2BHP,
                                          strAux3BHP,
                                          strMainRPM,
                                          strAux1RPM,
                                          strAux2RPM,
                                          strAux3RPM,
                                          AccCode,
                                          TrainingFee,  
                                          modifiedby,
                                         Common.CastAsInt32(  ddlManningOffice.SelectedValue));

           // Budget.getTable("UPDATE VESSEL SET FLEETID=" + ddlFleet.SelectedValue + " WHERE VesselId=" + VesselId.ToString());
            Budget.getTable("UPDATE DBO.VESSEL SET INSPECTIONVALIDITYPERIOD=" + Common.CastAsInt32(txtInsDays.Text) + " WHERE VESSELID=" + VesselId.ToString());
            lbl_message_other.Visible = true;
    }
    protected void btn_Print_VesselParticular2_Click(object sender, EventArgs e)
    {

    }
    protected void Show_Record_VesselParticular2(int VesselId)
    {
        string Mess = "";
        DataTable dt7 = VesselDetailsOther.selectDataVesselParticulars2DetailsByVesselId(VesselId);
        if (dt7.Rows.Count > 0)
        {
            foreach (DataRow dr in dt7.Rows)
            {
                txtinmarsatTerminal.Text = dr["InmarsatTerminalType"].ToString();
                txtcallsign.Text = dr["CallSign"].ToString();
                txttel1.Text = dr["Telephone1"].ToString();
                txttel2.Text = dr["Telephone2"].ToString();
                txtmobile.Text = dr["Mobile"].ToString();
                txtfax.Text = dr["Fax"].ToString();
                txtemail.Text = dr["Email"].ToString();
                txtdata.Text = dr["Data"].ToString();
                txthsd.Text = dr["HSD"].ToString();
                txtinmarsat.Text = dr["InmarsatC"].ToString();
                txtAccCode.Text = dr["AccCode"].ToString();
                txtTrainingFee.Text = dr["TrainingFee"].ToString();

                ddlManningOffice.SelectedValue = dr["ManningOfficeID"].ToString();

                txtInsDays.Text = "0";
                DataTable dtd = Budget.getTable("SELECT isnull(INSPECTIONVALIDITYPERIOD,0) FROM DBO.VESSEL VM WHERE VM.VESSELID=" + VesselId.ToString()).Tables[0];  
                if(dtd.Rows.Count > 0)
                {
                    txtInsDays.Text = dtd.Rows[0][0].ToString();   
                }
                
                Mess = Mess + Alerts.Set_DDL_Value(ddlMainEngineMaker, dr["MainEngineMakerId"].ToString(), "EngineMaker(Main)");

                
                Mess = Mess + Alerts.Set_DDL_Value(ddlAux1EngineMaker, dr["Aux1EngineMakerId"].ToString(), "EngineMaker(Aux1)");

                
                Mess = Mess + Alerts.Set_DDL_Value(ddlAux2EngineMaker, dr["Aux2EngineMakerId"].ToString(), "EngineMaker(Aux2)");

                
                Mess = Mess + Alerts.Set_DDL_Value(ddlAux3EngineMaker, dr["Aux3EngineMakerId"].ToString(), "EngineMaker(Aux3)");

               
                Mess = Mess + Alerts.Set_DDL_Value(ddlMainStrokeType, dr["MainEngineStrokeTypeId"].ToString(), "StrokeType(Main)");

                
                Mess = Mess + Alerts.Set_DDL_Value(ddlAux1StrokeType, dr["Aux1EngineStrokeTypeId"].ToString(), "StrokeType(Aux1)");

                
                Mess = Mess + Alerts.Set_DDL_Value(ddlAux2StrokeType, dr["Aux2EngineStrokeTypeId"].ToString(), "StrokeType(Aux2)");

                
                Mess = Mess + Alerts.Set_DDL_Value(ddlAux3StrokeType, dr["Aux3EngineStrokeTypeId"].ToString(), "StrokeType(Aux3)");


                txtMainEngineModel.Text = dr["MainEngineModel"].ToString();
                txtAux1EngineModel.Text = dr["Aux1EngineModel"].ToString();
                txtAux2EngineModel.Text = dr["Aux2EngineModel"].ToString();
                txtAux3EngineModel.Text = dr["Aux3EngineModel"].ToString();
                txtMainEngineKW.Text = dr["MainEngineKW"].ToString();
                txtAux1EngineKW.Text = dr["Aux1EngineKW"].ToString();
                txtAux2EngineKW.Text = dr["Aux2EngineKW"].ToString();
                txtAux3EngineKW.Text = dr["Aux3EngineKW"].ToString();
                
                txtMainBHP.Text=dr["MainEngineBHP"].ToString();
                txtAux1BHP.Text=dr["Aux1EngineBHP"].ToString();
                txtAux2BHP.Text=dr["Aux2EngineBHP"].ToString();
                txtAux3BHP.Text = dr["Aux3EngineBHP"].ToString();
                txtMainRPM.Text = dr["MainEngineRPM"].ToString();
                txtAux1RPM.Text = dr["Aux1EngineRPM"].ToString();
                txtAux2RPM.Text = dr["Aux2EngineRPM"].ToString();
                txtAux3RPM.Text = dr["Aux3EngineRPM"].ToString();

                
            }
            //DataTable dtFleet = Budget.getTable("SELECT ISNULL(FLEETID,0) FROM VESSEL WHERE VESSELID=" + VesselId.ToString()).Tables[0];
            //if (dtFleet.Rows.Count > 0)
            //{
            //    ddlFleet.SelectedValue = dtFleet.Rows[0][0].ToString();    
            //}
        }
        else
        {
            txtVesselName.Text = "";
            txtFormerVesselName.Text = "";
            ddlFlagStateName.SelectedIndex = 0;
       
            txtinmarsatTerminal.Text = "";
            txtcallsign.Text = "";
            txttel1.Text = "";
            txttel2.Text = "";
            txtmobile.Text = "";
            txtfax.Text = "";
            txtemail.Text = "";
            txtdata.Text = "";
            txthsd.Text = "";
            txtinmarsat.Text = "";
         
            ddlMainEngineMaker.SelectedIndex = 0;
            ddlAux1EngineMaker.SelectedIndex = 0;
            ddlAux2EngineMaker.SelectedIndex = 0;
            ddlAux3EngineMaker.SelectedIndex = 0;
            ddlMainStrokeType.SelectedIndex = 0;
            ddlAux1StrokeType.SelectedIndex = 0;
            ddlAux2StrokeType.SelectedIndex = 0;
            ddlAux3StrokeType.SelectedIndex = 0;
            txtMainEngineKW.Text = "";
            txtMainEngineModel.Text = "";
            txtMainBHP.Text = "";
            txtMainRPM.Text = "";
            txtAux1EngineKW.Text = "";
            txtAux1EngineModel.Text = "";
            txtAux1BHP.Text = "";
            txtAux1RPM.Text = "";
            txtAux2EngineKW.Text = "";
            txtAux2EngineModel.Text = "";
            txtAux2BHP.Text = "";
            txtAux2RPM.Text = "";
            txtAux3EngineKW.Text = "";
            txtAux3EngineModel.Text = "";
            txtAux3RPM.Text = "";
            txtAux3BHP.Text = "";
            //ddlFleet.SelectedIndex = 0;  
        }
        if (Mess.Length > 0)
        {
            this.lbl_message_other.Text = "The Status Of Following Has Been Changed To In-Active <br/>" + Mess.Substring(1);
            this.lbl_message_other.Visible = true;
        }
    }

    //ddlVesselManning
    //  
    public void BindVesselManning()
    {
        DataSet dtd = Budget.getTable(" select OfficeId,OfficeName from  DBO.office  ");
        ddlManningOffice.DataSource = dtd;
        ddlManningOffice.DataTextField = "OfficeName";
        ddlManningOffice.DataValueField = "OfficeId";
        ddlManningOffice.DataBind();
        ddlManningOffice.Items.Insert(0, new ListItem("Select", ""));
    }

}
