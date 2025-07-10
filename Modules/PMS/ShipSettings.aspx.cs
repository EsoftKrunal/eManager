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


public partial class ShipSettings : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        if (!IsPostBack)
        {
            ShowDetails();
        }
    }
    
    public void ShowDetails()
    {
        string VesselCode = Session["CurrentShip"].ToString();
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("Select * From VSL_Settings Where VesselCode='" + VesselCode + "'");

        if (dt != null && dt.Rows.Count > 0)
        {
            txtVesselCode.Text = dt.Rows[0]["VesselCode"].ToString();
            txtVesselName.Text = dt.Rows[0]["VesselName"].ToString();
            txtVesselType.Text = dt.Rows[0]["VesselType"].ToString();
            txtFlagName.Text = dt.Rows[0]["FlagName"].ToString();
            txtOwnerName.Text = dt.Rows[0]["OwnerName"].ToString();
            txtOwnerAddress.Text = dt.Rows[0]["OwnerAddress"].ToString();
            txtManagementName.Text = dt.Rows[0]["ManagementName"].ToString();
            txtManagementAddress.Text = dt.Rows[0]["MangementAddress"].ToString();
            txtBuild.Text = Common.ToDateString(dt.Rows[0]["Build"]);
            txtBuilders.Text = dt.Rows[0]["Builder"].ToString();
            txtIMONo.Text = dt.Rows[0]["IMONumber"].ToString();
            txtCall_Sing.Text = dt.Rows[0]["CallSing"].ToString();
            txtOfficialNo.Text = dt.Rows[0]["OfficialNumber"].ToString();
            txt_MMSI_No.Text = dt.Rows[0]["MMSINumber"].ToString();
            txtFaxInmarsatC.Text = dt.Rows[0]["FaxInmarsatC"].ToString();
            txt_Phone_Inmarsat_FB.Text = dt.Rows[0]["PhoneInmarsatFB"].ToString();
            txtClass.Text = dt.Rows[0]["Class"].ToString();
            txt_Class_NK_No.Text = dt.Rows[0]["ClassNKNumber"].ToString();
            txtLengthOverall.Text = dt.Rows[0]["LengthOverAll"].ToString() == "0.000" ? "" : dt.Rows[0]["LengthOverAll"].ToString();
            txtLength_PP.Text = dt.Rows[0]["LengthBetweenPP"].ToString() == "0.000" ? "" : dt.Rows[0]["LengthBetweenPP"].ToString();
            txtDepth_Moulded.Text = dt.Rows[0]["DepthMoulded"].ToString() == "0.000" ? "" : dt.Rows[0]["DepthMoulded"].ToString();
            txtBreadthMoulded.Text = dt.Rows[0]["BreadthMoulded"].ToString() == "0.000" ? "" : dt.Rows[0]["BreadthMoulded"].ToString();
            txtDraughtS.Text = dt.Rows[0]["Draught_S"].ToString() == "0.000" ? "" : dt.Rows[0]["Draught_S"].ToString();
            txtDead_Weight_S.Text = dt.Rows[0]["Deadweight_S"].ToString() == "0.000" ? "" : dt.Rows[0]["Deadweight_S"].ToString();
            txtInt_Gross_Tonnage.Text = dt.Rows[0]["Int_Gross_Tonnage"].ToString() == "0.000" ? "" : dt.Rows[0]["Int_Gross_Tonnage"].ToString();
            txtInt_Net_Tonnage.Text = dt.Rows[0]["Int_Net_Tonnage"].ToString() == "0.000" ? "" : dt.Rows[0]["Int_Net_Tonnage"].ToString();
            txtSuez_Net_Reg_Tonnage.Text = dt.Rows[0]["SuezCanal_Net_Register_Tonnage"].ToString() == "0.000" ? "" : dt.Rows[0]["SuezCanal_Net_Register_Tonnage"].ToString();
            txtPanama_Net_Tonnage.Text = dt.Rows[0]["PanamaCanal_UMS_Net_Tonnage"].ToString() == "0.000" ? "" : dt.Rows[0]["PanamaCanal_UMS_Net_Tonnage"].ToString();
            txtLightShip.Text = dt.Rows[0]["LightShip"].ToString() == "0.000" ? "" : dt.Rows[0]["LightShip"].ToString();
            txtMain_Engine.Text = dt.Rows[0]["MainEngine"].ToString();
            txtTail_Shaft_Oil.Text = dt.Rows[0]["Tail_Shaft_Oil"].ToString();
            txtTrialSpeed.Text = dt.Rows[0]["Trial_Speed"].ToString() == "0.000" ? "" : dt.Rows[0]["Trial_Speed"].ToString();
            txtMain_Generator_Engine.Text = dt.Rows[0]["Main_Generator_Engine"].ToString();
            txtMainGenerators.Text = dt.Rows[0]["Main_Generators"].ToString();
            txtEmer_Generator_Engine.Text = dt.Rows[0]["Emergency_Generator_Engine"].ToString();
            txtEle_Eqp_Main_Switch_Board.Text = dt.Rows[0]["Electrical_Eqp_Main_Switch_Board"].ToString();
            txtPropellerSize.Text = dt.Rows[0]["Propeller_Size"].ToString();
            txtPropeller_Type.Text = dt.Rows[0]["Propeller_Type"].ToString();
            txtPropellerMaterial.Text = dt.Rows[0]["Propeller_Material"].ToString();
            txtBoiler_Maker.Text = dt.Rows[0]["Boiler_Maker"].ToString();
            txtBoiler_Model.Text = dt.Rows[0]["Boiler_Model"].ToString();
            txtBoiler_Type.Text = dt.Rows[0]["Boiler_Type"].ToString();
            txtBoiler_Max_Working_Pressure.Text = dt.Rows[0]["Boiler_MaxWorkPressure"].ToString();
            txtBoiler_Nor_Working_Pressure.Text = dt.Rows[0]["Boiler_NorWorkPressure"].ToString();
            txtBoiler_Steam_Evaporation.Text = dt.Rows[0]["Boiler_SteamEvap"].ToString();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Common.Set_Procedures("[dbo].[DD_InsertUpdateVesselSettings]");
            Common.Set_ParameterLength(45);
            Common.Set_Parameters(
               new MyParameter("@VesselCode", txtVesselCode.Text.Trim()),
               new MyParameter("@VesselName", txtVesselName.Text.Trim()),
               new MyParameter("@VesselType", txtVesselType.Text.Trim()),
               new MyParameter("@OwnerName", txtOwnerName.Text.Trim()),
               new MyParameter("@OwnerAddress", txtOwnerAddress.Text.Trim()),
               new MyParameter("@ManagementName", txtManagementName.Text.Trim()),
               new MyParameter("@MangementAddress", txtManagementAddress.Text.Trim()),
               new MyParameter("@FlagName", txtFlagName.Text.Trim()),
               new MyParameter("@Build", txtBuild.Text.Trim()),
               new MyParameter("@Builder", txtBuilders.Text.Trim()),
               new MyParameter("@IMONumber", txtIMONo.Text.Trim()),
               new MyParameter("@CallSing", txtCall_Sing.Text.Trim()),
               new MyParameter("@OfficialNumber", txtOfficialNo.Text.Trim()),
               new MyParameter("@MMSINumber", txt_MMSI_No.Text.Trim()),
               new MyParameter("@FaxInmarsatC", txtFaxInmarsatC.Text.Trim()),
               new MyParameter("@PhoneInmarsatFB", txt_Phone_Inmarsat_FB.Text.Trim()),
               new MyParameter("@Class", txtClass.Text.Trim()),
               new MyParameter("@ClassNKNumber", txt_Class_NK_No.Text.Trim()),
               new MyParameter("@LengthOverAll", Common.CastAsDecimal(txtLengthOverall.Text.Trim())),
               new MyParameter("@LengthBetweenPP", Common.CastAsDecimal(txtLength_PP.Text.Trim())),
               new MyParameter("@BreadthMoulded", Common.CastAsDecimal(txtBreadthMoulded.Text.Trim())),
               new MyParameter("@DepthMoulded", Common.CastAsDecimal(txtDepth_Moulded.Text.Trim())),
               new MyParameter("@Draught_S", Common.CastAsDecimal(txtDraughtS.Text.Trim())),
               new MyParameter("@Deadweight_S", Common.CastAsDecimal(txtDead_Weight_S.Text.Trim())),
               new MyParameter("@Int_Gross_Tonnage", Common.CastAsDecimal(txtInt_Gross_Tonnage.Text.Trim())),
               new MyParameter("@Int_Net_Tonnage", Common.CastAsDecimal(txtInt_Net_Tonnage.Text.Trim())),
               new MyParameter("@SuezCanal_Net_Register_Tonnage", Common.CastAsDecimal(txtSuez_Net_Reg_Tonnage.Text.Trim())),
               new MyParameter("@PanamaCanal_UMS_Net_Tonnage", Common.CastAsDecimal(txtPanama_Net_Tonnage.Text.Trim())),
               new MyParameter("@LightShip", Common.CastAsDecimal(txtLightShip.Text.Trim())),
               new MyParameter("@MainEngine", txtMain_Engine.Text.Trim()),
               new MyParameter("@Propeller_Size", txtPropellerSize.Text.Trim()),
               new MyParameter("@Propeller_Type", txtPropeller_Type.Text.Trim()),
               new MyParameter("@Propeller_Material", txtPropellerMaterial.Text.Trim()),
               new MyParameter("@Tail_Shaft_Oil", txtTail_Shaft_Oil.Text.Trim()),
               new MyParameter("@Trial_Speed", Common.CastAsDecimal(txtTrialSpeed.Text.Trim())),
               new MyParameter("@Main_Generator_Engine", txtMain_Generator_Engine.Text.Trim()),
               new MyParameter("@Main_Generators", txtMainGenerators.Text.Trim()),
               new MyParameter("@Emergency_Generator_Engine", txtEmer_Generator_Engine.Text.Trim()),
               new MyParameter("@Electrical_Eqp_Main_Switch_Board", txtEle_Eqp_Main_Switch_Board.Text.Trim()),
               new MyParameter("@Boiler_Maker", txtBoiler_Maker.Text.Trim()),
               new MyParameter("@Boiler_Model", txtBoiler_Model.Text.Trim()),
               new MyParameter("@Boiler_Type", txtBoiler_Type.Text.Trim()),
               new MyParameter("@Boiler_MaxWorkPressure", txtBoiler_Max_Working_Pressure.Text.Trim()),
               new MyParameter("@Boiler_NorWorkPressure", txtBoiler_Nor_Working_Pressure.Text.Trim()),
               new MyParameter("@Boiler_SteamEvap", txtBoiler_Steam_Evaporation.Text.Trim())
               );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "re", "alert('Saved successfully.');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "re", "alert('Unable to save.Error :" + ex.Message.ToString() + "');", true);
        }
    }
}
