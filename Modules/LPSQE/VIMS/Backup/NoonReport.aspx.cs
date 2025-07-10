using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlClient;
using System.Text;
using Ionic.Zip;
using System.IO;

public partial class NoonReport : System.Web.UI.Page
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
                ScriptManager.RegisterStartupScript(this, this.GetType(),"exp","alert('Session Expired !. Please login again.'); window.close();", true);
                btnSave.Visible = false;
                return;
            }
            
            if (CurrentVessel != "")
            {
                
                txtVesselCode.Text = CurrentVessel;

                if (Key > 0)
                {
                    ShowRecord();
                    btnROB_Click(sender, e);
                }
                else
                {
                    CopyVoyageInformation();
                    btnROB_Click(sender, e);
                    Copy_ROB_ForNewRecord();
                }
            }
        }
        else
        {
            Copy_ROB();
        }
    }
    public void ManageReportType()
    {
        pnlArr.Visible = false;
        pnlAnchoring.Visible = false;
        pnlBerthing.Visible = false;
        pnlDrifting.Visible = false;
        tblETBDate.Visible = false;
        dvPart.Visible = false;
        txtVoyageNumber.Enabled = false;
        lblReportType.Text = "";
        switch (ddlReportType.SelectedValue)
        {
            case "A":
                dvPart.Visible = true;
                pnlArr.Visible = true;
                lblReportType.Text = "Arrival Details";
                break;
            case "N":
                dvPart.Visible = false;
                lblReportType.Text = "Noon Details";
                break;
            case "D":
                dvPart.Visible = false;
                txtVoyageNumber.Enabled = true;
                lblReportType.Text = "Departure Details";
                break;
            case "PA":
                dvPart.Visible = true;
                pnlAnchoring.Visible = true;
                tblETBDate.Visible = true;
                lblReportType.Text = "Port Arrival Details";
                break;
            case "PB":
                dvPart.Visible = true;
                pnlBerthing.Visible = true;
                lblReportType.Text = "Port Berthing Details";
                break;
            case "PD":
                dvPart.Visible = true;
                pnlDrifting.Visible = true;
                tblETBDate.Visible = true;
                lblReportType.Text = "Port Drifiting Details";
                break;
            case "SH":
                dvPart.Visible = false;
                break;
        }

        //pnlArr.Visible = true;
        //pnlAnchoring.Visible = true;
        //pnlBerthing.Visible = true;
        //pnlDrifting.Visible = true;
        //tblETBDate.Visible = true;
    }
    public void ShowRecord()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.VW_VSL_VPRNoonReport WHERE REPORTSPK=" + Key + " And VesselID='" + CurrentVessel + "'");
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            ReportsPK = Common.CastAsInt32(dt.Rows[0]["ReportsPK"]);

            //--------------------------

            DataTable dtMax = Common.Execute_Procedures_Select_ByQuery("SELECT isnull(MAX(ReportsPK),0) FROM DBO.VW_VSL_VPRNoonReport WHERE VESSELID='" + CurrentVessel + "'");
            int MaxReportId = Common.CastAsInt32(dtMax.Rows[0][0]);
            if (MaxReportId > 0 && MaxReportId != ReportsPK)
            {
               ShowMessage("Note : This report can not modified ( Only last report can be modified ).", true);
               btnSave.Visible = true;
            }

            ddlReportType.SelectedValue = dr["ACTIVITY_CODE"].ToString();
            ManageReportType();

            //--------------------------

            txtVesselCode.Text=	dr["VesselID"].ToString();
            txtRDate.Text=Common.ToDateString(dr["ReportDate"]);
            txtRDate.Enabled = false;
            txtVoyageNumber.Text=	dr["VoyageNo"].ToString();

            txtVoyInstructions.Text=	dr["VoyInstructions"].ToString();

            ddlSteamingHours.SelectedValue=	dr["SteamingHrs"].ToString().PadLeft(2,'0');
            ddlSteamingMin.SelectedValue = dr["SteamingMin"].ToString().PadLeft(2, '0');

            txtDistanceMadeGood.Text=	dr["DistanceMadeGood"].ToString();
            txtAvgSpeed.Text=	dr["AvgSpeed"].ToString();
            ddlStoppages.SelectedIndex=Common.CastAsInt32(dr["Stoppages"]);

            ddlStoppageTimeHH.SelectedValue = Common.CastAsInt32(dr["StoppagesHH"]).ToString().PadLeft(2, '0');
            ddlStoppageTimeMM.SelectedValue = Common.CastAsInt32(dr["StoppagesMM"]).ToString().PadLeft(2, '0');
            
            //ddlDisplacement.SelectedValue =  Common.CastAsInt32(dr["Displacement"]).ToString();

            Remarks.Text=	dr["Remarks"].ToString();

            ddlDepVoyCondition.SelectedIndex=	Common.CastAsInt32(dr["VoyCondition"]);
            txtDepPort.Text=	dr["DepPort"].ToString();
            txtDepArrivalPort.Text=	dr["DepArrivalPort"].ToString();
 
            txtArrivalPortETA.Text=	Common.ToDateString(dr["ArrivalPortETA"]);
            ddlArrivalPortETAHH.SelectedValue = dr["ArrivalPortETAHrs"].ToString().PadLeft(2, '0');
            ddlArrivalPortETAMM.SelectedValue = dr["ArrivalPortETAMin"].ToString().PadLeft(2, '0');
            txtDepDraftFwd.Text=	dr["DraftFwd"].ToString();
            txtDepDraftAft.Text=	dr["DraftAft"].ToString();
            txtDepDistanceToGo.Text=	dr["DistanceToGo"].ToString();
            txtArrivalPortAgent.Text=	dr["ArrivalPortAgent"].ToString();
            txtPersonalIncharge.Text=	dr["PersonalIncharge"].ToString();
            txtAddressContactDetails.Text=	dr["AddressContactDetails"].ToString();
            txtCource.Text=	dr["CourceT"].ToString();
            txtWindDirection.Text=	dr["WindDirectionT"].ToString();
            ddlWindForce.SelectedValue=	Common.CastAsInt32(dr["WindForce"]).ToString();
            txtSeaDirection.Text=	dr["SeaDirection"].ToString();
            ddlSeaState.SelectedValue = Common.CastAsInt32(dr["SeaState"]).ToString();
            txtCurrentDirection.Text=	dr["CurrentDirection"].ToString();
            txtCurrentStrength.Text=	dr["CurrentStrength"].ToString();
            txtWeatherRemarks.Text=	dr["WeatherRemarks"].ToString();

            ddlLattitude1.SelectedValue =Common.CastAsInt32(dr["Lattitude1"]).ToString();
            ddlLattitude2.SelectedValue = Common.CastAsInt32(dr["Lattitude2"]).ToString();
            ddlLattitude3.SelectedIndex = Common.CastAsInt32(dr["Lattitude3"]);
            ddlLongitude1.SelectedValue = Common.CastAsInt32(dr["Longitude1"]).ToString();
            ddlLongitude2.SelectedValue = Common.CastAsInt32(dr["Longitud2"]).ToString();
            ddlLongitude3.SelectedIndex = Common.CastAsInt32(dr["Longitud3"]);

            txtLocationDescription.Text=	dr["LocationDescription"].ToString();

            //----------------- Fuel -----------------------

            txtRobIFO45.Text = dt.Rows[0]["ROBIFO45Fuel"].ToString();
            txtRobIFO1.Text = dt.Rows[0]["ROBIFO1Fuel"].ToString();
            txtRobMGO5.Text = dt.Rows[0]["ROBMGO5Fuel"].ToString();
            txtRobMGO1.Text = dt.Rows[0]["ROBMGO1Fuel"].ToString();
            txtRobMDO.Text = dt.Rows[0]["ROBMDOFuel"].ToString();

            hfdRobIFO45_S.Value = dt.Rows[0]["ROBIFO45Fuel"].ToString();
            hfdRobIFO1_S.Value = dt.Rows[0]["ROBIFO1Fuel"].ToString();
            hfdRobMGO5_S.Value = dt.Rows[0]["ROBMGO5Fuel"].ToString();
            hfdRobMGO1_S.Value = dt.Rows[0]["ROBMGO1Fuel"].ToString();
            hfdRobMDO_S.Value = dt.Rows[0]["ROBMDOFuel"].ToString();

            //----------------- Lube -----------------------

            txtRobMECC.Text = dt.Rows[0]["ROBIFO45Lube"].ToString();
            txtRobMECYL.Text = dt.Rows[0]["ROBIFO1Lube"].ToString();
            txtRobAECC.Text = dt.Rows[0]["ROBMGO5Lube"].ToString();
            txtRobHYD.Text = dt.Rows[0]["ROBMGO1Lube"].ToString();
        
            hfdRobMECC_S.Value = dt.Rows[0]["ROBIFO45Lube"].ToString();
            hfdRobMECYL_S.Value = dt.Rows[0]["ROBIFO1Lube"].ToString();
            hfdRobAECC_S.Value = dt.Rows[0]["ROBMGO5Lube"].ToString();
            hfdRobHYD_S.Value = dt.Rows[0]["ROBMGO1Lube"].ToString();
        
            //----------------- Fresh Water -----------------------

            txtRobFesshWater.Text = dt.Rows[0]["ROBIFO45FreshWater"].ToString();
            hfdRobFesshWater_S.Value = dt.Rows[0]["ROBIFO45FreshWater"].ToString();

            txtME_IFO45.Text=	dr["MEIFO45"].ToString();
            txtME_IFO1.Text=	dr["MEIFO1"].ToString();
            txtME_MGO5.Text=	dr["MEMGO5"].ToString();
            txtME_MGO10.Text=	dr["MEMGO1"].ToString();
            txtME_MDO.Text=	dr["MEMDO"].ToString();
            txtAE_IFO45.Text=	dr["AEIFO45"].ToString();
            txtAE_IFO1.Text=	dr["AEIFO1"].ToString();
            txtAE_MGO5.Text=	dr["AEMGO5"].ToString();
            txtAE_MGO1.Text=	dr["AEMGO1"].ToString();
            txtAE_MDO.Text=	dr["AEMDO"].ToString();
            txtCargoHeating_IFO45.Text=	dr["CargoHeatingIFO45"].ToString();
            txtCargoHeating_IFO1.Text=	dr["CargoHeatingIFO1"].ToString();
            txtCargoHeating_MGO5.Text=	dr["CargoHeatingMGO5"].ToString();
            txtCargoHeating_MGO1.Text=	dr["CargoHeatingMGO1"].ToString();
            txtCargoHeating_MDO.Text=	dr["CargoHeatingMDO"].ToString();
            txtLubeFresh_MECC.Text=	dr["MECCLube"].ToString();
            txtLubeFresh_MECYL.Text=	dr["MECYLLube"].ToString();
            txtLubeFresh_AECC.Text=	dr["AECCLube"].ToString();
            txtLubeFresh_HYD.Text=	dr["HYDLube"].ToString();
            txtLubeFresh_Generated.Text=	dr["GeneratedFreshWater"].ToString();
            txtLubeFresh_Consumed.Text=	dr["ConsumedFreshWater"].ToString();
            txtExhTempMin.Text=	dr["ExhTempMin"].ToString();
            txtExhTempMax.Text=	dr["ExhTempMax"].ToString();
            txtMERPM.Text=	dr["MERPM"].ToString();
            txtEngineDistance.Text=	dr["EngineDistance"].ToString();
            txtSlip.Text=	dr["Slip"].ToString();
            txtMEOutput.Text=	dr["MEOutput"].ToString();
            txtMEThermalLoad.Text=	dr["METhermalLoad"].ToString();
            txtMeLoadIindicator.Text = dr["MELoadIndicator"].ToString();

            txtME1.Text=	dr["METCNo1RPM"].ToString();
            txtME2.Text=	dr["METCNo2RPM"].ToString();
            txtMESCAVPressure.Text=	dr["MESCAVPressure"].ToString();
            txtSCAVTEMP.Text=	dr["SCAVTEMP"].ToString();
            txtLubeOilPressure.Text=	dr["LubeOilPressure"].ToString();
            txtSeaWaterTemp.Text=	dr["SeaWaterTemp"].ToString();
            txtEngRoomTemp.Text=	dr["EngRoomTemp"].ToString();
            txtBligPump.Text=	dr["BligPump"].ToString();

            AUX_1_Load.Text=	dr["AUX1Load"].ToString();
            AUX_2_Load.Text=	dr["AUX2Load"].ToString();
            AUX_3_Load.Text=	dr["AUX3Load"].ToString();
            AUX_4_Load.Text=	dr["AUX4Load"].ToString();
            lblTotAuxiliary.Text=	dr["TotalAUXLoad"].ToString();
            txtA_ENo_1.Text=	dr["AENo1"].ToString();
            txtA_ENo_2.Text=	dr["AENo2"].ToString();
            txtA_ENo_3.Text=	dr["AENo3"].ToString();
            txtA_ENo_4.Text= dr["AENo4"].ToString();
            txtAEFOInlinetTemp.Text= dr["AEFOInletTemp"].ToString();
            txtTotalCargoWeight.Text= dr["TotalCargoWeight"].ToString();
            txtBallastWeight.Text= dr["BallastWeight"].ToString();


            //----------------- fuel recv -----------------------

            txtRobIFO45Recv.Text = dt.Rows[0]["RECIFO45"].ToString();
            txtRobIFO1Recv.Text = dt.Rows[0]["RECIFO1"].ToString();
            //txtRobMGO5Recv.Text = dt.Rows[0]["ROBMGO5RECV"].ToString();
            txtRobMGO1Recv.Text = dt.Rows[0]["RECMGO1"].ToString();
            txtRobMDORecv.Text = dt.Rows[0]["RECMDO"].ToString();


            txtRobMECCRecv.Text = dt.Rows[0]["RECMECC"].ToString();
            txtRobMECYLRecv.Text = dt.Rows[0]["RECMECYL"].ToString();
            txtRobAECCRecv.Text = dt.Rows[0]["RECAECC"].ToString();
            txtRobHYDRecv.Text = dt.Rows[0]["RECHYD"].ToString();
            //txtRobMDOLubeRecv.Text = dt.Rows[0]["ROBMDOLUBERECV"].ToString();

            txtRobFesshWaterRecv.Text = dt.Rows[0]["RECFRESHWATER"].ToString();


            //----------------- berthing columns -----------------------

            txtBerthTerminalName.Text = dr["BERTHTERMINALNAME"].ToString();

            txtFirstLineDate.Text = Common.ToDateString(dr["FIRSTLINEDATE"]);
            ddlFirstLineHH.SelectedValue = Common.CastAsInt32(dr["FIRSTLINEHH"]).ToString().PadLeft(2, '0');
            ddlFirstLineMM.SelectedValue = Common.CastAsInt32(dr["FIRSTLINEMM"]).ToString().PadLeft(2, '0');

            txtAllFastDate.Text = Common.ToDateString(dr["ALLFASTDATE"]);
            ddlAllFastHH.SelectedValue = Common.CastAsInt32(dr["ALLFASTHH"]).ToString().PadLeft(2, '0');
            ddlAllFastMM.SelectedValue = Common.CastAsInt32(dr["ALLFASTMM"]).ToString().PadLeft(2, '0');

            txtNoOfTUGSUSED.Text = dr["NOOFTUGSUSED"].ToString();

            txtETDDateTime.Text = Common.ToDateString(dr["ETDDT"]);
            ddlETDDateTimeHH.SelectedValue = Common.CastAsInt32(dr["ETDHOURS"]).ToString().PadLeft(2, '0');
            ddlETDDateTimeMM.SelectedValue = Common.CastAsInt32(dr["ETDMIN"]).ToString().PadLeft(2, '0');

            //----------------- anchoring columns -----------------------

            txtPOB.Text = dr["POB"].ToString();
            txtLetGoAnchorage.Text = Common.ToDateString(dr["LETGOANCHORAGE"]);
            ddlLetGoAnchorageHH.SelectedValue = Common.CastAsInt32(dr["LETGOANCHORAGEHH"]).ToString().PadLeft(2, '0');
            ddlLetGoAnchorageMM.SelectedValue = Common.CastAsInt32(dr["LETGOANCHORAGEMM"]).ToString().PadLeft(2, '0');

            ddlpilotAwayHH.SelectedValue = Common.CastAsInt32(dr["PILOTAWAYHH"]).ToString().PadLeft(2, '0');
            ddlpilotAwayMM.SelectedValue = Common.CastAsInt32(dr["PILOTAWAYMM"]).ToString().PadLeft(2, '0');
            txtAnchoragereasion.Text = dr["ANCHORAGEREASON"].ToString();

            txtETBDate.Text = Common.ToDateString(dr["ETBDT"]);
            ddlETBHours.SelectedValue = Common.CastAsInt32(dr["ETBHours"]).ToString().PadLeft(2, '0');
            ddlETBMins.SelectedValue = Common.CastAsInt32(dr["ETBMin"]).ToString().PadLeft(2, '0');

            //----------------- drifting columns -----------------------

            txtCdDrifting.Text = Common.ToDateString(dr["ComDDDate"]);
            ddlCdDriftingHours.SelectedValue = Common.CastAsInt32(dr["DriftHour"]).ToString().PadLeft(2, '0');
            ddlCdDriftingMin.SelectedValue = Common.CastAsInt32(dr["DriftMin"]).ToString().PadLeft(2, '0');
            ddlDriftingReason.SelectedValue = dr["DriftReason"].ToString();
       
            //----------------- dep columns -----------------------

            //----------------- arr columns -----------------------

            txtEOSPDate.Text = Common.ToDateString(dr["EOSPDate"]);
            ddlEOSPHours.SelectedValue = Common.CastAsInt32(dr["EOSPHrs"]).ToString().PadLeft(2, '0');
            ddlEOSPMin.SelectedValue = Common.CastAsInt32(dr["EOSPMin"]).ToString().PadLeft(2, '0');             
     

            txtSGH.Text = dr["Shaft_Gen_Hrs"].ToString();
            txtSGM.Text = dr["Shaft_Gen_Mins"].ToString();
            txtTCH.Text = dr["Tank_Cleaning_Hrs"].ToString();
            txtTCM.Text = dr["Tank_Cleaning_Mins"].ToString();
            txtCHH.Text = dr["Cargo_Heating_Hrs"].ToString();
            txtCHMin.Text = dr["Cargo_Heating_Mins"].ToString();
            txt_InertH.Text = dr["Inert_Hrs"].ToString();
            txt_InertM.Text = dr["Inert_Mins"].ToString();

        }
    }
    protected void ddlReportType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ManageReportType();
    }
    protected void CopyVoyageInformation()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT TOP 1 VoyageNo,VoyCondition,* from VW_VSL_VPR_ALLREPORTS WHERE ReportTypeCode IN ('D','N') ORDER BY ReportsPK DESC");
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];

            txtVoyageNumber.Text = dr["VoyageNo"].ToString();
            txtVoyInstructions.Text = dr["VoyInstructions"].ToString();
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
        //==================================
        decimal Distance = Common.CastAsDecimal(txtDistanceMadeGood.Text);
        decimal Time_Hrs = Common.CastAsDecimal(ddlSteamingHours.SelectedItem) + ( Common.CastAsDecimal(ddlSteamingMin.SelectedItem) / 60 );
        decimal AvgSpeed = 0;
        if(Time_Hrs!=0)
            AvgSpeed=Distance / Time_Hrs;
        //==================================
        decimal Slip = Common.CastAsDecimal(txtSlip.Text);
        decimal EngineDistance = Common.CastAsDecimal(txtEngineDistance.Text);
        if (EngineDistance==0) 
        {
              Slip= 0;
        }
        else
        {
            decimal CalSlip = ((EngineDistance-Distance) / EngineDistance) * 100;
            Slip= Math.Round(CalSlip, 2);
        }
        //==================================
        decimal TotalAUXLoad=Common.CastAsDecimal(AUX_1_Load.Text) + Common.CastAsDecimal(AUX_2_Load.Text) + Common.CastAsDecimal(AUX_3_Load.Text) + Common.CastAsDecimal(AUX_4_Load.Text);
        //==================================
        decimal ROBIFO45 = 0, ROBIFO1 = 0, ROBMGO5 = 0, ROBMGO1 = 0, ROBMDO = 0;
        decimal ROBMECC = 0, ROBMECYL = 0, ROBAECC = 0, ROBHYD = 0;
        decimal ROBFESSHWATER = 0;

        ROBIFO45 = Common.CastAsDecimal(hfdRobIFO45_S.Value);
        ROBIFO1 = Common.CastAsDecimal(hfdRobIFO1_S.Value);
        ROBMGO5 = Common.CastAsDecimal(hfdRobMGO5_S.Value);
        ROBMGO1 = Common.CastAsDecimal(hfdRobMGO1_S.Value);
        ROBMDO = Common.CastAsDecimal(hfdRobMDO_S.Value);
        ROBMECC = Common.CastAsDecimal(hfdRobMECC_S.Value);
        ROBMECYL = Common.CastAsDecimal(hfdRobMECYL_S.Value);
        ROBAECC = Common.CastAsDecimal(hfdRobAECC_S.Value);
        ROBHYD = Common.CastAsDecimal(hfdRobHYD_S.Value);
        ROBFESSHWATER = Common.CastAsDecimal(hfdRobFesshWater_S.Value);

        bool res = false;
        Common.Set_Procedures("DBO.SHIP_VPR_InsertUpdate_VPRNoonReport_New");
        Common.Set_ParameterLength(186);
        Common.Set_Parameters(
        new MyParameter("@ReportsPK", Key),
        new MyParameter("@VesselID", txtVesselCode.Text.Trim()),
        new MyParameter("@ReportDate", (txtRDate.Text.Trim() == "" ? DBNull.Value : (object)txtRDate.Text)),
        new MyParameter("@Location", 0),
        new MyParameter("@VoyageNo", txtVoyageNumber.Text.Trim()),
        new MyParameter("@RestrictedArea", 0),
        new MyParameter("@AreaName", "0"),
        new MyParameter("@ETARestrictedArea", DBNull.Value),
        new MyParameter("@ChartererName", ""),
        new MyParameter("@CharterPartySpeed", 0),
        new MyParameter("@VoyOrderSpeed", 0),
        new MyParameter("@VoyInstructions", txtVoyInstructions.Text.Trim()),
        new MyParameter("@SteamingHrs", Common.CastAsInt32(ddlSteamingHours.SelectedValue)),
        new MyParameter("@SteamingMin", Common.CastAsInt32(ddlSteamingMin.SelectedValue)),
        new MyParameter("@DistanceMadeGood", Common.CastAsDecimal(txtDistanceMadeGood.Text)),
        new MyParameter("@AvgSpeed", Math.Round(AvgSpeed,2)),
        new MyParameter("@Stoppages", ddlStoppages.SelectedIndex),
        new MyParameter("@StoppagesHH",  ddlStoppageTimeHH.SelectedValue),
        new MyParameter("@StoppagesMM", ddlStoppageTimeMM.SelectedValue),
        new MyParameter("@Displacement", 0),
        new MyParameter("@Remarks", Remarks.Text.Trim()),

        new MyParameter("@VoyCondition", ddlDepVoyCondition.SelectedIndex),
        new MyParameter("@DepPort", txtDepPort.Text.Trim()),
        new MyParameter("@DepArrivalPort", txtDepArrivalPort.Text.Trim()),
        new MyParameter("@COSPDate", DBNull.Value),
        new MyParameter("@COSPHrs", 0),
        new MyParameter("@COSPMin", 0),
        new MyParameter("@TimeZone", ""),
        new MyParameter("@ArrivalPortETA", (txtArrivalPortETA.Text.Trim() == "" ? DBNull.Value : (object)txtArrivalPortETA.Text)),
        new MyParameter("@ArrivalPortETAHrs", Common.CastAsInt32(ddlArrivalPortETAHH.SelectedValue)),
        new MyParameter("@ArrivalPortETAMin", Common.CastAsInt32(ddlArrivalPortETAMM.SelectedValue)),
        new MyParameter("@DraftFwd", Common.CastAsDecimal(txtDepDraftFwd.Text)),
        new MyParameter("@DraftAft", Common.CastAsDecimal(txtDepDraftAft.Text)),
        new MyParameter("@DistanceToGo", Common.CastAsDecimal(txtDepDistanceToGo.Text)),
        new MyParameter("@ArrivalPortAgent", txtArrivalPortAgent.Text.Trim()),
        new MyParameter("@PersonalIncharge", txtPersonalIncharge.Text.Trim()),
        new MyParameter("@AddressContactDetails", txtAddressContactDetails.Text.Trim()),
        new MyParameter("@Port1", ""),
        new MyParameter("@PortETA1", DBNull.Value),
        new MyParameter("@Port2", ""),
        new MyParameter("@PortETA2", DBNull.Value),
        new MyParameter("@Port3", ""),
        new MyParameter("@PortETA3", DBNull.Value),
        new MyParameter("@CourceT", Common.CastAsDecimal(txtCource.Text)),

         new MyParameter("@WindDirectionT", Common.CastAsDecimal(txtWindDirection.Text)),
         new MyParameter("@WindForce", ddlWindForce.SelectedValue),
         new MyParameter("@SeaDirection", Common.CastAsDecimal(txtSeaDirection.Text)),
         new MyParameter("@SeaState", ddlSeaState.SelectedValue),
         new MyParameter("@CurrentDirection", Common.CastAsDecimal(txtCurrentDirection.Text)),
         new MyParameter("@CurrentStrength", Common.CastAsDecimal(txtCurrentStrength.Text)),
         new MyParameter("@WeatherRemarks", txtWeatherRemarks.Text.Trim()),
         new MyParameter("@Lattitude1", ddlLattitude1.SelectedValue),
         new MyParameter("@Lattitude2", ddlLattitude2.SelectedValue),
         new MyParameter("@Lattitude3", ddlLattitude3.SelectedIndex),
         new MyParameter("@Longitude1", ddlLongitude1.SelectedValue),
         new MyParameter("@Longitud2", ddlLongitude2.SelectedValue),
         new MyParameter("@Longitud3", ddlLongitude3.SelectedIndex),
         new MyParameter("@LocationDescription", txtLocationDescription.Text.Trim()),

         new MyParameter("@ROBIFO45Fuel", ROBIFO45),
         new MyParameter("@ROBIFO1Fuel", ROBIFO1),
         new MyParameter("@ROBMGO5Fuel", ROBMGO5),
         new MyParameter("@ROBMGO1Fuel", ROBMGO1),
         new MyParameter("@ROBMDOFuel", ROBMDO),
         new MyParameter("@ROBIFO45Lube", ROBMECC),
         new MyParameter("@ROBIFO1Lube", ROBMECYL),
         new MyParameter("@ROBMGO5Lube", ROBAECC),
         new MyParameter("@ROBMGO1Lube", ROBHYD),
         new MyParameter("@ROBIFO45FreshWater", ROBFESSHWATER),


         new MyParameter("@MEIFO45", Common.CastAsDecimal(txtME_IFO45.Text)),
         new MyParameter("@MEIFO1",Common.CastAsDecimal(txtME_IFO1.Text)),
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
         new MyParameter("@TankCleaningIFO45", 0),
         new MyParameter("@TankCleaningIFO1", 0),
         new MyParameter("@TankCleaningMGO5", 0),
         new MyParameter("@TankCleaningMGO1", 0),
         new MyParameter("@TankCleaningMDO", 0),
         new MyParameter("@GasFreeingIFO45", 0),
         new MyParameter("@GasFreeingIFO1", 0),
         new MyParameter("@GasFreeingMGO5", 0),
         new MyParameter("@GasFreeingMGO1", 0),
         new MyParameter("@GasFreeingMDO", 0),
         new MyParameter("@IGSIFO45", 0),
         new MyParameter("@IGSIFO1", 0),
         new MyParameter("@IGSMGO5", 0),
         new MyParameter("@IGSMGO1", 0),
         new MyParameter("@IGSMDO", 0),

         new MyParameter("@Total_IFO45", 0),
         new MyParameter("@Total_IFO1", 0),
         new MyParameter("@Total_MGO5", 0),
         new MyParameter("@Total_MGO1", 0),
         new MyParameter("@Total_MDO", 0),

         new MyParameter("@MECCLube", Common.CastAsDecimal(txtLubeFresh_MECC.Text)),
         new MyParameter("@MECYLLube", Common.CastAsDecimal(txtLubeFresh_MECYL.Text)),
         new MyParameter("@AECCLube", Common.CastAsDecimal(txtLubeFresh_AECC.Text)),
         new MyParameter("@HYDLube", Common.CastAsDecimal(txtLubeFresh_HYD.Text)),


         new MyParameter("@GeneratedFreshWater", Common.CastAsDecimal(txtLubeFresh_Generated.Text)),
         new MyParameter("@ConsumedFreshWater", Common.CastAsDecimal(txtLubeFresh_Consumed.Text)),

         new MyParameter("@ExhTempMin", Common.CastAsDecimal(txtExhTempMin.Text)),
         new MyParameter("@ExhTempMax", Common.CastAsDecimal(txtExhTempMax.Text)),
         new MyParameter("@MERPM", Common.CastAsDecimal(txtMERPM.Text)),
         new MyParameter("@EngineDistance", Common.CastAsDecimal(txtEngineDistance.Text)),

         new MyParameter("@Slip", Slip),

         new MyParameter("@MEOutput", Common.CastAsDecimal(txtMEOutput.Text)),
         new MyParameter("@METhermalLoad", Common.CastAsDecimal(txtMEThermalLoad.Text)),
         new MyParameter("@MELoadIndicator", Common.CastAsDecimal(txtMeLoadIindicator.Text)),

         new MyParameter("@METCNo1RPM", Common.CastAsDecimal(txtME1.Text)),
         new MyParameter("@METCNo2RPM", Common.CastAsDecimal(txtME2.Text)),
         new MyParameter("@MESCAVPressure", Common.CastAsDecimal(txtMESCAVPressure.Text)),
         new MyParameter("@SCAVTEMP", Common.CastAsDecimal(txtSCAVTEMP.Text)),
         new MyParameter("@LubeOilPressure", Common.CastAsDecimal(txtLubeOilPressure.Text)),
         new MyParameter("@SeaWaterTemp", Common.CastAsDecimal(txtSeaWaterTemp.Text)),
         new MyParameter("@EngRoomTemp", Common.CastAsDecimal(txtEngRoomTemp.Text)),
         new MyParameter("@BligPump", Common.CastAsDecimal(txtBligPump.Text)),

         new MyParameter("@AUX1Load", Common.CastAsDecimal(AUX_1_Load.Text)),
         new MyParameter("@AUX2Load", Common.CastAsDecimal(AUX_2_Load.Text)),
         new MyParameter("@AUX3Load", Common.CastAsDecimal(AUX_3_Load.Text)),
         new MyParameter("@AUX4Load", Common.CastAsDecimal(AUX_4_Load.Text)),

         new MyParameter("@TotalAUXLoad", TotalAUXLoad),

         new MyParameter("@AENo1", Common.CastAsDecimal(txtA_ENo_1.Text)),
         new MyParameter("@AENo2", Common.CastAsDecimal(txtA_ENo_2.Text)),
         new MyParameter("@AENo3", Common.CastAsDecimal(txtA_ENo_3.Text)),
         new MyParameter("@AENo4", Common.CastAsDecimal(txtA_ENo_4.Text)),
         new MyParameter("@AEFOInletTemp", Common.CastAsDecimal(txtAEFOInlinetTemp.Text)),
         new MyParameter("@TotalCargoWeight", Common.CastAsDecimal(txtTotalCargoWeight.Text)),
         new MyParameter("@BallastWeight", Common.CastAsDecimal(txtBallastWeight.Text)),


         new MyParameter("@ACTIVITY_CODE", ddlReportType.SelectedValue.Trim()),


         new MyParameter("@RECIFO45", Common.CastAsDecimal(txtRobIFO45Recv.Text)),
         new MyParameter("@RECIFO1", Common.CastAsDecimal(txtRobIFO1Recv.Text)),
         new MyParameter("@RECMGO5", Common.CastAsDecimal(txtRobMGO5Recv.Text)),
         new MyParameter("@RECMGO1", Common.CastAsDecimal(txtRobMGO1Recv.Text)),
         new MyParameter("@RECMDO", Common.CastAsDecimal(txtRobMDORecv.Text)),

         new MyParameter("@RECMECC", Common.CastAsDecimal(txtRobMECCRecv.Text)),
         new MyParameter("@RECMECYL", Common.CastAsDecimal(txtRobMECYLRecv.Text)),
         new MyParameter("@RECAECC", Common.CastAsDecimal(txtRobAECCRecv.Text)),
         new MyParameter("@RECHYD", Common.CastAsDecimal(txtRobHYDRecv.Text)),
         new MyParameter("@RECMDOLUBE", 0),

         new MyParameter("@RECFRESHWATER", Common.CastAsDecimal(txtRobFesshWaterRecv.Text)),

    
         //---------- berthing columns

         new MyParameter("@BERTHTERMINALNAME", txtBerthTerminalName.Text.Trim()),
         new MyParameter("@PLACEOFBERTH", ""),

        new MyParameter("@FIRSTLINEDATE", (txtFirstLineDate.Text.Trim() == "" ? DBNull.Value : (object)txtFirstLineDate.Text)),
        new MyParameter("@FIRSTLINEHH", Common.CastAsInt32(ddlFirstLineHH.SelectedValue)),
        new MyParameter("@FIRSTLINEMM", Common.CastAsInt32(ddlFirstLineMM.SelectedValue)),

        new MyParameter("@ALLFASTDATE", (txtAllFastDate.Text.Trim() == "" ? DBNull.Value : (object)txtAllFastDate.Text)),
        new MyParameter("@ALLFASTHH", Common.CastAsInt32(ddlAllFastHH.SelectedValue)),
        new MyParameter("@ALLFASTMM", Common.CastAsInt32(ddlAllFastMM.SelectedValue)),

        new MyParameter("@NOOFTUGSUSED", txtNoOfTUGSUSED.Text.Trim()),

        new MyParameter("@ETBDT", (txtETBDate.Text.Trim() == "" ? DBNull.Value : (object)txtETBDate.Text)),
        new MyParameter("@ETBHOURS", Common.CastAsInt32(ddlETBHours.SelectedValue)),
        new MyParameter("@ETBMIN", Common.CastAsInt32(ddlETBMins.SelectedValue)),

        new MyParameter("@ETDDT", (txtETDDateTime.Text.Trim() == "" ? DBNull.Value : (object)txtETDDateTime.Text)),
        new MyParameter("@ETDHOURS", Common.CastAsInt32(ddlETDDateTimeHH.SelectedValue)),
        new MyParameter("@ETDMIN", Common.CastAsInt32(ddlETDDateTimeMM.SelectedValue)),

        //---------- anchoring columns

        new MyParameter("@POB", txtPOB.Text.Trim()),
        new MyParameter("@LETGOANCHORAGE", (txtLetGoAnchorage.Text.Trim() == "" ? DBNull.Value : (object)txtLetGoAnchorage.Text)),
        new MyParameter("@LETGOANCHORAGEHH", Common.CastAsInt32(ddlLetGoAnchorageHH.SelectedValue)),
        new MyParameter("@LETGOANCHORAGEMM", Common.CastAsInt32(ddlLetGoAnchorageMM.SelectedValue)),
        new MyParameter("@PILOTAWAYHH", Common.CastAsInt32(ddlpilotAwayHH.SelectedValue)),
        new MyParameter("@PILOTAWAYMM", Common.CastAsInt32(ddlpilotAwayMM.SelectedValue)),
        new MyParameter("@ANCHORAGEREASON", txtAnchoragereasion.Text.Trim()),
        //new MyParameter("@ETBDT1", (txtETBDate.Text.Trim() == "" ? DBNull.Value : (object)txtETBDate.Text)),
        //new MyParameter("@ETBHours1", Common.CastAsInt32(ddlETBHours.SelectedValue)),
        //new MyParameter("@ETBMin1", Common.CastAsInt32(ddlETBMins.SelectedValue)),

        //---------- drifting columns

        new MyParameter("@ComDDDate", (txtCdDrifting.Text.Trim() == "" ? DBNull.Value : (object)txtCdDrifting.Text)),
        new MyParameter("@DriftHour", Common.CastAsInt32(ddlCdDriftingHours.SelectedValue)),
        new MyParameter("@DriftMin", Common.CastAsInt32(ddlCdDriftingMin.SelectedValue)),
        new MyParameter("@DriftReason", ddlDriftingReason.SelectedValue),
        //new MyParameter("@ETBDT2", (txtETBDate.Text.Trim() == "" ? DBNull.Value : (object)txtETBDate.Text)),
        //new MyParameter("@ETBHours2", Common.CastAsInt32(ddlETBHours.SelectedValue)),
        //new MyParameter("@ETBMin2", Common.CastAsInt32(ddlETBMins.SelectedValue)),

        //---------- dep columns

        //---------- arr columns

        new MyParameter("@EOSPDate", (txtEOSPDate.Text.Trim() == "" ? DBNull.Value : (object)txtEOSPDate.Text)),
        new MyParameter("@EOSPHrs", Common.CastAsInt32(ddlEOSPHours.SelectedValue)),
        new MyParameter("@EOSPMin", Common.CastAsInt32(ddlEOSPMin.SelectedValue)),

        new MyParameter("@Shaft_Gen_Hrs", Common.CastAsInt32(txtSGH.Text)),
        new MyParameter("@Shaft_Gen_Mins", Common.CastAsInt32(txtSGM.Text)),

        new MyParameter("@Tank_Cleaning_Hrs", Common.CastAsInt32(txtTCH.Text)),
        new MyParameter("@Tank_Cleaning_Mins", Common.CastAsInt32(txtTCM.Text)),

        new MyParameter("@Cargo_Heating_Hrs", Common.CastAsInt32(txtCHH.Text)),
        new MyParameter("@Cargo_Heating_Mins", Common.CastAsInt32(txtCHMin.Text)),

        new MyParameter("@Inert_Hrs", Common.CastAsInt32(txt_InertH.Text)),
        new MyParameter("@Inert_Mins", Common.CastAsInt32(txt_InertM.Text))
 
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
        //----------------------------------
        if (txtVesselCode.Text.Trim() == "")
        {
            ShowMessage("Your session is expired. Close this window and login Again.", true);
            btnSave.Visible = false;
            return;
        }
        if (txtRDate.Text.Trim() == "")
        {
            ShowMessage("Please enter report date.", true);
            txtRDate.Focus();
            return;
        }
        if (Convert.ToDateTime(txtRDate.Text) > Convert.ToDateTime(DateTime.Today.Date))
        {
            ShowMessage("Report date can not be more than today.", true);
            txtRDate.Focus();
            return;
        }
        if (txtVoyageNumber.Text.Trim() == "")
        {
            ShowMessage("Please enter voyage#.", true);
            txtVoyageNumber.Focus();
            return;
        }

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT TOP 1 ReportDate FROM [dbo].[VW_VSL_VPRNoonReport] WHERE VESSELID='" + CurrentVessel + "' AND ReportsPK <> " + ReportsPK + " ORDER BY ReportsPK DESC");

        if (dt.Rows.Count > 0)
        {
            if (Convert.ToDateTime(txtRDate.Text) < Convert.ToDateTime(dt.Rows[0]["ReportDate"]))
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

        if (Common.CastAsDecimal(txtDepDistanceToGo.Text.Trim()) <= 0)
        {
            ShowMessage("Please enter Distance to go.", true);
            txtDepDistanceToGo.Focus();
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
        //----------------------------------
        if (SaveRecord())
        {
            ShowMessage("Report Saved successfully.", false);
        }
        else
        {
            ShowMessage("Report not Saved. Error : " + Common.ErrMsg, true);
        }
        //----------------------------------
    }
    public void ShowMessage(string msg, bool error)
    {
        lblMessage.Text = msg;
        lblMessage.ForeColor = (error ? System.Drawing.Color.Red : System.Drawing.Color.Green);
    }
    protected void btnROB_Click(object sender, EventArgs e)
    {
        string sql="EXEC VSL_VPR_GET_FUEL_ROB '" + CurrentVessel + "'," + ReportsPK;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        //txtArrivalPortAgent.Text = sql;
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
            //hfdRobMDOLube.Value = dt.Rows[0]["ROBMDOLUBE"].ToString();

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
        //txtRobMDOLube.Text = hfdRobMDOLube.Value;

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
        //hfdRobMDOLube_S.Value = hfdRobMDOLube.Value;

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
        //txtRobMDOLube.Text = hfdRobMDOLube_S.Value;

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
