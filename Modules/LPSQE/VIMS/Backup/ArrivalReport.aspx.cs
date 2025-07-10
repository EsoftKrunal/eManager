using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Ionic.Zip;
using System.IO;

public partial class VIMS_ArrivalReport : System.Web.UI.Page
{
    public string CurrentVessel
    {
        get { return ViewState["CurrentVessel"].ToString(); }
        set { ViewState["CurrentVessel"] = value; }
    }
    public int Key
    {
        get { return Common.CastAsInt32(ViewState["Key"]); }                                                   
        set { ViewState["Key"] = value; }
    }
    public int ReportsPK
    {
        get { return Common.CastAsInt32(ViewState["ReportPK"]); }
        set { ViewState["ReportPK"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                CurrentVessel = Session["CurrentShip"].ToString();
                Key = Common.CastAsInt32(Request.QueryString["Key"]);
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "exp", "alert('Session Expired !. Please login again.'); window.close();", true);
                btnSave.Visible = false;
                return;
            }

            try
            {
                

                if (CurrentVessel != "" && Key > 0)
                {
                    ShowRecord();
                    btnROB_Click(sender, e);
                }
                else
                {
                    txtVesselCode.Text = CurrentVessel.Trim();
                    btnROB_Click(sender, e);
                    Copy_ROB_ForNewRecord();
                    CopyVoyageInformation();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error : " + ex.Message.ToString();
            }
        }
        {
            Copy_ROB();
        }
    }

    public void ShowRecord()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.VSL_VPRArrivalReport WHERE ArrivalID=" + Key + " And VesselID='" + CurrentVessel + "'");

        

        if (dt.Rows.Count > 0)
        {

            DataRow dr = dt.Rows[0];
            ReportsPK = Common.CastAsInt32(dt.Rows[0]["ReportsPK"]);

            //----------------------------
            DataTable dtMax = Common.Execute_Procedures_Select_ByQuery("SELECT isnull(MAX(ReportsPK),0) FROM DBO.VW_VSL_VPR_ALLREPORTS WHERE VESSELID='" + CurrentVessel + "'");
            int MaxReportId = Common.CastAsInt32(dtMax.Rows[0][0]);
            if (MaxReportId > 0 && MaxReportId != ReportsPK)
            {
                ShowMessage("Note : This report can not modified ( Only last report can be modified ).", true);
                btnSave.Visible = false;
            }
            //----------------------------

            txtVesselCode.Text = dr["VesselID"].ToString();
            rptDate.Text = Common.ToDateString(dr["ReportDate"]);
            rptDate.Enabled = false;
            txtVoyageNumber.Text = dr["VoyageNo"].ToString();            

            ddlSteamingHours.SelectedValue = dr["SteamingHrs"].ToString().PadLeft(2, '0');
            ddlSteamingMin.SelectedValue = dr["SteamingMin"].ToString().PadLeft(2, '0');

            txtDistanceMadeGood.Text = dr["DistanceMadeGood"].ToString();
            txtAvgSpeed.Text = dr["AvgSpeed"].ToString();
            ddlStoppages.SelectedIndex = Common.CastAsInt32(dr["Stoppages"]);

            ddlStoppageTimeHH.SelectedValue = Common.CastAsInt32(dr["StoppagesHH"]).ToString().PadLeft(2, '0');
            ddlStoppageTimeMM.SelectedValue = Common.CastAsInt32(dr["StoppagesMM"]).ToString().PadLeft(2, '0'); 
            txtSTPPRemarks.Text = dr["Remarks"].ToString();

            ddlDepVoyCondition.SelectedIndex = Common.CastAsInt32(dr["VoyCondition"]);
            txtDepPort.Text = dr["DepPort"].ToString();
            txtDepArrivalPort.Text = dr["DepArrivalPort"].ToString();

            txtEOSPDate.Text = Common.ToDateString(dr["EOSPDate"]);
            ddlEOSPHours.SelectedValue = Common.CastAsInt32(dr["EOSPHrs"]).ToString().PadLeft(2, '0');
            ddlEOSPMin.SelectedValue = Common.CastAsInt32(dr["EOSPMin"]).ToString().PadLeft(2, '0');             
            
            txtDepDraftFwd.Text = dr["DraftFwd"].ToString();
            txtDepDraftAft.Text = dr["DraftAft"].ToString();
            
            txtArrivalPortAgent.Text = dr["ArrivalPortAgent"].ToString();
            txtPersonalIncharge.Text = dr["PersonalIncharge"].ToString();
            txtAddressContactDetails.Text = dr["AddressContactDetails"].ToString();
            
            txtCource.Text = dr["CourceT"].ToString();
            txtWindDirection.Text = dr["WindDirectionT"].ToString();
            ddlWindForce.SelectedValue = Common.CastAsInt32(dr["WindForce"]).ToString();
            txtSeaDirection.Text = dr["SeaDirection"].ToString();
            ddlSeaState.SelectedValue = Common.CastAsInt32(dr["SeaState"]).ToString();
            txtCurrentDirection.Text = dr["CurrentDirection"].ToString();
            txtCurrentStrength.Text = dr["CurrentStrength"].ToString();
            txtWeatherRemarks.Text = dr["WeatherRemarks"].ToString();
            
            ddlLattitude1.SelectedValue = Common.CastAsInt32(dr["Lattitude1"]).ToString();
            ddlLattitude2.SelectedValue = Common.CastAsInt32(dr["Lattitude2"]).ToString();
            ddlLattitude3.SelectedIndex = Common.CastAsInt32(dr["Lattitude3"]);
            ddlLongitude1.SelectedValue = Common.CastAsInt32(dr["Longitude1"]).ToString();
            ddlLongitude2.SelectedValue = Common.CastAsInt32(dr["Longitud2"]).ToString();
            ddlLongitude3.SelectedIndex = Common.CastAsInt32(dr["Longitud3"]);
            txtLocationDescription.Text = dr["LocationDescription"].ToString();

            txtRobIFO45.Text = dr["ROBIFO45Fuel"].ToString();
            txtRobIFO1.Text = dr["ROBIFO1Fuel"].ToString();
            txtRobMGO5.Text = dr["ROBMGO5Fuel"].ToString();
            txtRobMGO1.Text = dr["ROBMGO1Fuel"].ToString();
            txtRobMDO.Text = dr["ROBMDOFuel"].ToString();

            hfdRobIFO45_S.Value = dt.Rows[0]["ROBIFO45Fuel"].ToString();
            hfdRobIFO1_S.Value = dt.Rows[0]["ROBIFO1Fuel"].ToString();
            hfdRobMGO5_S.Value = dt.Rows[0]["ROBMGO5Fuel"].ToString();
            hfdRobMGO1_S.Value = dt.Rows[0]["ROBMGO1Fuel"].ToString();
            hfdRobMDO_S.Value = dt.Rows[0]["ROBMDOFuel"].ToString();
            
            txtRobMECC.Text = dr["ROBIFO45Lube"].ToString();
            txtRobMECYL.Text = dr["ROBIFO1Lube"].ToString();
            txtRobAECC.Text = dr["ROBMGO5Lube"].ToString();
            txtRobHYD.Text = dr["ROBMGO1Lube"].ToString();
            txtRobFesshWater.Text = dr["ROBFreshWater"].ToString();

            hfdRobMECC_S.Value = dt.Rows[0]["ROBIFO45Lube"].ToString();
            hfdRobMECYL_S.Value = dt.Rows[0]["ROBIFO1Lube"].ToString();
            hfdRobAECC_S.Value = dt.Rows[0]["ROBMGO5Lube"].ToString();
            hfdRobHYD_S.Value = dt.Rows[0]["ROBMGO1Lube"].ToString();
            hfdRobFesshWater_S.Value = dt.Rows[0]["ROBFreshWater"].ToString();
            
            txtME_IFO45.Text = dr["MEIFO45"].ToString();
            txtME_IFO1.Text = dr["MEIFO1"].ToString();
            txtME_MGO5.Text = dr["MEMGO5"].ToString();
            txtME_MGO10.Text = dr["MEMGO1"].ToString();
            txtME_MDO.Text = dr["MEMDO"].ToString();
            
            txtAE_IFO45.Text = dr["AEIFO45"].ToString();
            txtAE_IFO1.Text = dr["AEIFO1"].ToString();
            txtAE_MGO5.Text = dr["AEMGO5"].ToString();
            txtAE_MGO1.Text = dr["AEMGO1"].ToString();
            txtAE_MDO.Text = dr["AEMDO"].ToString();
            
            txtCargoHeating_IFO45.Text = dr["CargoHeatingIFO45"].ToString();
            txtCargoHeating_IFO1.Text = dr["CargoHeatingIFO1"].ToString();
            txtCargoHeating_MGO5.Text = dr["CargoHeatingMGO5"].ToString();
            txtCargoHeating_MGO1.Text = dr["CargoHeatingMGO1"].ToString();
            txtCargoHeating_MDO.Text = dr["CargoHeatingMDO"].ToString();
            
            txtLubeFresh_MECC.Text = dr["MECCLube"].ToString();
            txtLubeFresh_MECYL.Text = dr["MECYLLube"].ToString();
            txtLubeFresh_AECC.Text = dr["AECCLube"].ToString();
            txtLubeFresh_HYD.Text = dr["HYDLube"].ToString();
            
            txtLubeFresh_Generated.Text = dr["GeneratedFreshWater"].ToString();
            txtLubeFresh_Consumed.Text = dr["ConsumedFreshWater"].ToString();
            
            txtTotalCargoWeight.Text = dr["TotalCargoWeight"].ToString();
            txtBallastWeight.Text = dr["BallastWeight"].ToString();

            txtTCU20L.Text = dr["TCU20L"].ToString();
            txtTCU40L.Text = dr["TCU40L"].ToString();
            txtTCU45L.Text = dr["TCU45L"].ToString();

            txtTCU20E.Text = dr["TCU20E"].ToString();
            txtTCU40E.Text = dr["TCU40E"].ToString();
            txtTCU45E.Text = dr["TCU45E"].ToString();

            ddlNextActivity.SelectedValue = dr["NextActivity"].ToString(); 
            txtETBDate.Text = Common.ToDateString(dr["ETBDATE"]);
            ddlETBHours.SelectedValue = Common.CastAsInt32(dr["ETBHrs"]).ToString().PadLeft(2, '0');
            ddlETBMins.SelectedValue = Common.CastAsInt32(dr["ETBMin"]).ToString().PadLeft(2, '0'); 
            
        }
    }
    protected void CopyVoyageInformation()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT TOP 1 VoyageNo,VoyCondition,* from VW_VSL_VPR_ALLREPORTS WHERE ReportTypeCode IN ('D','N') ORDER BY ReportsPK DESC");
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];

            txtVoyageNumber.Text = dr["VoyageNo"].ToString();
            //txtVoyInstructions.Text = dr["VoyInstructions"].ToString();
            ddlDepVoyCondition.SelectedIndex = Common.CastAsInt32(dr["VoyConditionId"]);
            txtDepPort.Text = dr["DeparturePort"].ToString();
            txtDepArrivalPort.Text = dr["ArrivalPort"].ToString();
            txtArrivalPortAgent.Text = dr["ArrivalPortAgent"].ToString();
            txtPersonalIncharge.Text = dr["PersonalIncharge"].ToString();
            txtAddressContactDetails.Text = dr["AddressContactDetails"].ToString();
        }
    }
    public bool SaveRecord()
    {
        bool res = false;
        Common.Set_Procedures("DBO.SHIP_VPR_InsertUpdate_VPRArrivalReport");
        Common.Set_ParameterLength(85);
        Common.Set_Parameters(
        new MyParameter("@ArrivalID", Key),
        new MyParameter("@VesselID", txtVesselCode.Text.Trim()),
        new MyParameter("@ReportDate", (rptDate.Text.Trim() == "" ? DBNull.Value : (object)rptDate.Text)),
        new MyParameter("@VoyageNo", txtVoyageNumber.Text.Trim()),
        new MyParameter("@SteamingHrs", Common.CastAsInt32(ddlSteamingHours.SelectedValue)),
        new MyParameter("@SteamingMin", Common.CastAsInt32(ddlSteamingMin.SelectedValue)),
        new MyParameter("@DistanceMadeGood", Common.CastAsDecimal(txtDistanceMadeGood.Text)),
        new MyParameter("@AvgSpeed", Common.CastAsDecimal(txtAvgSpeed.Text)),
        new MyParameter("@Stoppages", ddlStoppages.SelectedIndex),
        new MyParameter("@StoppagesHH", Common.CastAsInt32(ddlStoppageTimeHH.SelectedValue)),
        new MyParameter("@StoppagesMM", Common.CastAsInt32(ddlStoppageTimeMM.SelectedValue)),
        new MyParameter("@Remarks", txtSTPPRemarks.Text.Trim()),
        new MyParameter("@VoyCondition", ddlDepVoyCondition.SelectedIndex),
        new MyParameter("@DepPort", txtDepPort.Text.Trim()),
        new MyParameter("@DepArrivalPort", txtDepArrivalPort.Text.Trim()),
        new MyParameter("@EOSPDate", (txtEOSPDate.Text.Trim() == "" ? DBNull.Value : (object)txtEOSPDate.Text)),
        new MyParameter("@EOSPHrs", Common.CastAsInt32(ddlEOSPHours.SelectedValue)),
        new MyParameter("@EOSPMin", Common.CastAsInt32(ddlEOSPMin.SelectedValue)),
        new MyParameter("@DraftFwd", Common.CastAsDecimal(txtDepDraftFwd.Text)),
        new MyParameter("@DraftAft", Common.CastAsDecimal(txtDepDraftAft.Text)),
        new MyParameter("@ArrivalPortAgent", txtArrivalPortAgent.Text.Trim()),
        new MyParameter("@PersonalIncharge", txtPersonalIncharge.Text.Trim()),
        new MyParameter("@AddressContactDetails", txtAddressContactDetails.Text.Trim()),
        
         new MyParameter("@CourceT", Common.CastAsDecimal(txtCource.Text)),
         new MyParameter("@WindDirectionT", Common.CastAsDecimal(txtWindDirection.Text)),
         new MyParameter("@WindForce", Common.CastAsInt32(ddlWindForce.SelectedValue)),
         new MyParameter("@SeaDirection", Common.CastAsDecimal(txtSeaDirection.Text)),
         new MyParameter("@SeaState", Common.CastAsInt32(ddlSeaState.SelectedValue)),
         new MyParameter("@CurrentDirection", Common.CastAsDecimal(txtCurrentDirection.Text)),
         new MyParameter("@CurrentStrength", Common.CastAsDecimal(txtCurrentStrength.Text)),
         new MyParameter("@WeatherRemarks", txtWeatherRemarks.Text.Trim()),
         
         new MyParameter("@Lattitude1", Common.CastAsInt32(ddlLattitude1.SelectedValue)),
         new MyParameter("@Lattitude2", Common.CastAsInt32(ddlLattitude2.SelectedValue)),
         new MyParameter("@Lattitude3", ddlLattitude3.SelectedIndex),
         new MyParameter("@Longitude1", Common.CastAsInt32(ddlLongitude1.SelectedValue)),
         new MyParameter("@Longitud2", Common.CastAsInt32(ddlLongitude2.SelectedValue)),
         new MyParameter("@Longitud3", ddlLongitude3.SelectedIndex),
         new MyParameter("@LocationDescription", txtLocationDescription.Text.Trim()),

         new MyParameter("@ROBIFO45Fuel", Common.CastAsDecimal(hfdRobIFO45_S.Value.Trim())),
         new MyParameter("@ROBIFO1Fuel", Common.CastAsDecimal(hfdRobIFO1_S.Value.Trim())),
         new MyParameter("@ROBMGO5Fuel", Common.CastAsDecimal(hfdRobMGO5_S.Value.Trim())),
         new MyParameter("@ROBMGO1Fuel", Common.CastAsDecimal(hfdRobMGO1_S.Value.Trim())),
         new MyParameter("@ROBMDOFuel", Common.CastAsDecimal(hfdRobMDO_S.Value.Trim())),

         new MyParameter("@ROBIFO45Lube", Common.CastAsDecimal(hfdRobMECC_S.Value.Trim())),
         new MyParameter("@ROBIFO1Lube", Common.CastAsDecimal(hfdRobMECYL_S.Value.Trim())),
         new MyParameter("@ROBMGO5Lube", Common.CastAsDecimal(hfdRobAECC_S.Value.Trim())),
         new MyParameter("@ROBMGO1Lube", Common.CastAsDecimal(hfdRobHYD_S.Value.Trim())),
         new MyParameter("@ROBMDOLube", 0),
         new MyParameter("@ROBFreshWater", Common.CastAsDecimal(hfdRobFesshWater_S.Value.Trim())),

         new MyParameter("@MEIFO45", Common.CastAsDecimal(txtME_IFO45.Text)),
         new MyParameter("@MEIFO1", Common.CastAsDecimal(txtME_IFO1.Text)),
         new MyParameter("@MEMGO5", Common.CastAsDecimal(txtME_MGO5.Text)),
         new MyParameter("@MEMGO1", Common.CastAsDecimal(txtME_MGO10.Text)),
         new MyParameter("@MEMDO", Common.CastAsDecimal(txtME_MDO.Text)),

         new MyParameter("@AEIFO45", Common.CastAsDecimal(txtAE_IFO45.Text)),
         new MyParameter("@AEIFO1", Common.CastAsDecimal(txtAE_IFO1.Text)),
         new MyParameter("@AEMGO5", Common.CastAsDecimal(txtAE_MGO5.Text)),
         new MyParameter("@AEMGO1", Common.CastAsDecimal(txtAE_MGO1.Text)),
         new MyParameter("@AEMDO", Common.CastAsDecimal(txtAE_MDO.Text)),

         new MyParameter("@CargoHeatingIFO45", Common.CastAsDecimal(txtCargoHeating_IFO45.Text)),
         new MyParameter("@CargoHeatingIFO1", Common.CastAsDecimal(txtCargoHeating_IFO1.Text)),
         new MyParameter("@CargoHeatingMGO5", Common.CastAsDecimal(txtCargoHeating_MGO5.Text)),
         new MyParameter("@CargoHeatingMGO1", Common.CastAsDecimal(txtCargoHeating_MGO1.Text)),
         new MyParameter("@CargoHeatingMDO", Common.CastAsDecimal(txtCargoHeating_MDO.Text)),
         
         

         new MyParameter("@MECCLube", Common.CastAsDecimal(txtLubeFresh_MECC.Text)),
         new MyParameter("@MECYLLube", Common.CastAsDecimal(txtLubeFresh_MECYL.Text)),
         new MyParameter("@AECCLube", Common.CastAsDecimal(txtLubeFresh_AECC.Text)),
         new MyParameter("@HYDLube", Common.CastAsDecimal(txtLubeFresh_HYD.Text)),
         new MyParameter("@MDOLube", 0),


         new MyParameter("@GeneratedFreshWater", Common.CastAsDecimal(txtLubeFresh_Generated.Text)),
         new MyParameter("@ConsumedFreshWater", Common.CastAsDecimal(txtLubeFresh_Consumed.Text)),


         new MyParameter("@TCW", Common.CastAsDecimal(txtTotalCargoWeight.Text)),
         new MyParameter("@BW", Common.CastAsDecimal(txtBallastWeight.Text)),

         new MyParameter("@TCU20L", Common.CastAsDecimal(txtTCU20L.Text)),
         new MyParameter("@TCU40L", Common.CastAsDecimal(txtTCU40L.Text)),
         new MyParameter("@TCU45L", Common.CastAsDecimal(txtTCU45L.Text)),
         new MyParameter("@TCU20E", Common.CastAsDecimal(txtTCU20E.Text)),
         new MyParameter("@TCU40E", Common.CastAsDecimal(txtTCU40E.Text)),
         new MyParameter("@TCU45E", Common.CastAsDecimal(txtTCU45E.Text)),
         new MyParameter("@NextActivity", ddlNextActivity.SelectedValue),
         new MyParameter("@ETBDATE", (txtETBDate.Text.Trim() == "" ? DBNull.Value : (object)txtETBDate.Text)),
         new MyParameter("@ETBHrs", Common.CastAsInt32(ddlETBHours.SelectedValue)),
         new MyParameter("@ETBMin", Common.CastAsInt32(ddlETBMins.SelectedValue)),

         new MyParameter("@TotalCargoWeight", Common.CastAsDecimal(txtTotalCargoWeight.Text)),
         new MyParameter("@BallastWeight", Common.CastAsDecimal(txtBallastWeight.Text))

         );

        DataSet ds = new DataSet();
        res = Common.Execute_Procedures_IUD(ds);
        if (res)
        {
            Key = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);
        }
        return res;
    }
    protected void btnSaveClick(object sender, EventArgs e)
    {
        if (txtVesselCode.Text.Trim() == "")
        {
            ShowMessage("Your session is expired. Close this window and login Again.", true);
            btnSave.Visible = false;
            return;
        }
        if (rptDate.Text.Trim() == "")
        {
            ShowMessage("Please enter report date.", true);
            rptDate.Focus();
            return;
        }        
        if (Convert.ToDateTime(rptDate.Text) > Convert.ToDateTime(DateTime.Today.Date))
        {
            ShowMessage("Report date can not be more than today.", true);
            rptDate.Focus();
            return;
        }
        if (txtVoyageNumber.Text.Trim() == "")
        {
            ShowMessage("Please enter voyage#.", true);
            txtVoyageNumber.Focus();
            return;
        }

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT TOP 1 ReportTypeCode, ReportDate FROM [dbo].[VW_VSL_VPR_ALLREPORTS] WHERE VESSELID='" + CurrentVessel + "' AND ReportsPK <> " + ReportsPK + " ORDER BY ReportsPK DESC");

        if (dt.Rows.Count > 0)
        {
            string ReportTypeCode = dt.Rows[0]["ReportTypeCode"].ToString().Trim();
            if (!(ReportTypeCode == "D" || ReportTypeCode == "N"))
            {
                ShowMessage("Arrival report can be created only after departure/noon report.", true);
                return;
            }

            if (Convert.ToDateTime(rptDate.Text) < Convert.ToDateTime(dt.Rows[0]["ReportDate"]))
            {
                ShowMessage("Report date must be more or equal to last report date.", true);
                return;
            }
        }
        else
        {
            ShowMessage("First report must be departure report.", true);            
            return;
        }

        if (Common.CastAsDecimal(txtDepDraftFwd.Text.Trim()) <= 0)
        {
            ShowMessage("Please enter Draft(Fwd).", true);
            txtDepDraftFwd.Focus();
            return;
        }
        if (Common.CastAsDecimal(txtDepDraftAft.Text.Trim()) <= 0)
        {
            ShowMessage("Please enter Draft(Aft).", true);
            txtDepDraftAft.Focus();
            return;
        }

        
        if (SaveRecord())
        {
            ShowRecord();
            ShowMessage("Report Saved successfully.", false);
        }
        else
        {
            ShowMessage("Report not Saved. Error : " + Common.ErrMsg, true);
        }

    }    

    public void ShowMessage(string msg, bool error)
    {
        lblMessage.Text = msg;
        lblMessage.ForeColor = (error ? System.Drawing.Color.Red : System.Drawing.Color.Green);
    }

    protected void btnROB_Click(object sender, EventArgs e)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("EXEC VSL_VPR_GET_FUEL_ROB '" + CurrentVessel + "'," + ReportsPK);
        if (dt.Rows.Count > 0)
            {
                hfdRobIFO45.Value = dt.Rows[0]["ROBIFO45"].ToString();
                hfdRobIFO1.Value = dt.Rows[0]["ROBIFO1"].ToString();
                hfdRobMGO5.Value = dt.Rows[0]["ROBMGO5"].ToString();
                hfdRobMGO1.Value = dt.Rows[0]["ROBMGO1"].ToString();
                hfdRobMDO.Value = dt.Rows[0]["ROBMDO"].ToString();

                hfdRobMECC.Value = dt.Rows[0]["ROBMECC"].ToString();
                hfdRobMECYL.Value = dt.Rows[0]["ROBMECYL"].ToString();
                hfdRobAECC.Value = dt.Rows[0]["ROBAECC"].ToString();
                hfdRobHYD.Value = dt.Rows[0]["ROBHYD"].ToString();
                
                hfdRobFesshWater.Value = dt.Rows[0]["ROBFESSHWATER"].ToString();

            }
    }
    protected void Copy_ROB_ForNewRecord()
    {

        txtRobIFO45.Text = hfdRobIFO45.Value;
        txtRobIFO1.Text = hfdRobIFO1.Value;
        txtRobMGO5.Text = hfdRobMGO5.Value;
        txtRobMGO1.Text = hfdRobMGO1.Value;
        txtRobMDO.Text = hfdRobMDO.Value;

        txtRobMECC.Text = hfdRobMECC.Value;
        txtRobMECYL.Text = hfdRobMECYL.Value;
        txtRobAECC.Text = hfdRobAECC.Value;
        txtRobHYD.Text = hfdRobHYD.Value;

        txtRobFesshWater.Text = hfdRobFesshWater.Value;

        hfdRobIFO45_S.Value = hfdRobIFO45.Value;
        hfdRobIFO1_S.Value = hfdRobIFO1.Value;
        hfdRobMGO5_S.Value = hfdRobMGO5.Value;
        hfdRobMGO1_S.Value = hfdRobMGO1.Value;
        hfdRobMDO_S.Value = hfdRobMDO.Value;

        hfdRobMECC_S.Value = hfdRobMECC.Value;
        hfdRobMECYL_S.Value = hfdRobMECYL.Value;
        hfdRobAECC_S.Value = hfdRobAECC.Value;
        hfdRobHYD_S.Value = hfdRobHYD.Value;

        hfdRobFesshWater_S.Value = hfdRobFesshWater.Value;

    }
    protected void Copy_ROB()
    {

        txtRobIFO45.Text = hfdRobIFO45_S.Value;
        txtRobIFO1.Text = hfdRobIFO1_S.Value;
        txtRobMGO5.Text = hfdRobMGO5_S.Value;
        txtRobMGO1.Text = hfdRobMGO1_S.Value;
        txtRobMDO.Text = hfdRobMDO_S.Value;

        txtRobMECC.Text = hfdRobMECC_S.Value;
        txtRobMECYL.Text = hfdRobMECYL_S.Value;
        txtRobAECC.Text = hfdRobAECC_S.Value;
        txtRobHYD.Text = hfdRobHYD_S.Value;

        txtRobFesshWater.Text = hfdRobFesshWater_S.Value;

    }

    protected void ddlStoppages_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStoppages.SelectedValue == "Yes")
        {
            ddlStoppageTimeHH.Enabled = true;
            ddlStoppageTimeMM.Enabled = true;

            RequiredFieldValidator7.Enabled = true;
            RequiredFieldValidator8.Enabled = true;
        }
        else
        {
            ddlStoppageTimeHH.Enabled = false;
            ddlStoppageTimeMM.Enabled = false;

            ddlStoppageTimeHH.SelectedIndex = 0;
            ddlStoppageTimeMM.SelectedIndex = 0;

            RequiredFieldValidator7.Enabled = false;
            RequiredFieldValidator8.Enabled = false;
        }
    }
}