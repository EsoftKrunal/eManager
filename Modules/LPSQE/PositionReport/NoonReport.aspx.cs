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

public partial class PositionReport_NoonReport : System.Web.UI.Page
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

                CurrentVessel = Request.QueryString["VesselCode"].ToString();
                Key = Common.CastAsInt32(Request.QueryString["Key"]);
            }
            catch 
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),"exp","alert('Session Expired !. Please login again.'); window.close();", true);
                
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
        int vesselType = 0;
        string sql = "Select VesselTypeId from Vessel with(nolock) where VesselCode = '" + CurrentVessel + "'";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt.Rows.Count > 0)
        {
            vesselType = Convert.ToInt32(dt.Rows[0]["VesselTypeId"].ToString());
        }
        if (vesselType == 28)
        { divContainerUnit.Visible = true; }
        else { divContainerUnit.Visible = false; }
        ddlLocation.SelectedValue = "2";
        pnlArr.Visible = false;
        pnlAnchoring.Visible = false;
        pnlBerthing.Visible = false;
        pnlDrifting.Visible = false;
        tblETBDate.Visible = false;
        dvPart.Visible = false;
        txtVoyageNumber.Enabled = false;
        lblReportType.Text = "";
        trSteaming.Visible = false;
        trGarbage.Visible = false;
        trBWTS.Visible = false;
        divVLSFO_ROB.Visible = false;
        divLSMGO_ROB.Visible = false;
        divMDO_ROB.Visible = false;
        divHeaderROB.Visible = false;
        // Added by Mehul 
        divHeaderFWE.Visible = false;
        divVLSFO_FWE.Visible = false;
        divLSMGO_FWE.Visible = false;
        divMDO_FWE.Visible = false;
        divFlowmeterFWE.Visible = false;
        divtankinfo.Visible = false;
        divLCFWE.Visible = false;
        divMECCFWE.Visible = false;
        divHYDFWE.Visible = false;
        divAECCFWE.Visible = false;
        divMECYLHighBNFWE.Visible = false;
        divMECYLFWE.Visible = false;
        divAnchorwaitTimeDepature.Visible = false;
        divAnchorAwaitTimeArrival.Visible = false;
        divFWEFressWater.Visible = false;
        divHeaderFWEFreshWater.Visible = false;
        // Added by Mehul 
        switch (ddlReportType.SelectedValue)
        {
            case "A":
                dvPart.Visible = true;
                pnlArr.Visible = true;
                trSteaming.Visible = true;
                lblReportType.Text = "Arrival Details";
                lblReportName.Text = "Arrival Report";
                txtSinceLastReport.Text = "";
                // Added by Mehul
                divHeaderFWE.Visible = true;
                divVLSFO_FWE.Visible = true;
                divLSMGO_FWE.Visible = true;
                divMDO_FWE.Visible = true;
                divFlowmeterFWE.Visible = true;
                divtankinfo.Visible = true;
                divLCFWE.Visible = true;
                divMECCFWE.Visible = true;
                divHYDFWE.Visible = true;
                divAECCFWE.Visible = true;
                divMECYLHighBNFWE.Visible = true;
                divMECYLFWE.Visible = true;
                divAnchorAwaitTimeArrival.Visible = true;
                divFWEFressWater.Visible = true;
                divHeaderFWEFreshWater.Visible = true;
                // Added by Mehul 
                break;
            case "N":
                ddlLocation.SelectedValue = "1";
                dvPart.Visible = false;
                trSteaming.Visible = true;
                lblReportType.Text = "Noon Details";
                lblReportName.Text = "Noon at Sea Report";
                txtSinceLastReport.Text = "";
                divAnchorwaitTimeDepature.Visible = true;
                break;
            case "D":
                dvPart.Visible = false;
                txtVoyageNumber.Enabled = true;
                trSteaming.Visible = false;
                lblReportType.Text = "Departure Details";
                lblReportName.Text = "Departure Report";
                trGarbage.Visible = true;
                trBWTS.Visible = true;
                divVLSFO_ROB.Visible = true;
                divLSMGO_ROB.Visible = true;
                divMDO_ROB.Visible = true;
                divHeaderROB.Visible = true;
                divAnchorwaitTimeDepature.Visible = true;
                break;
            case "PA":
                dvPart.Visible = true;
                pnlAnchoring.Visible = true;
                tblETBDate.Visible = true;
                trSteaming.Visible = false;
                lblReportType.Text = "Noon at Anchorage Details";
                lblReportName.Text = "Noon at Anchorage Report";
                txtSinceLastReport.Text = "";
                divAnchorwaitTimeDepature.Visible = true;
                break;
            case "PB":
                dvPart.Visible = true;
                pnlBerthing.Visible = true;
                trSteaming.Visible = false;
                lblReportType.Text = "Noon at Berthing Details";
                lblReportName.Text = "Noon at Berthing Report";
                txtSinceLastReport.Text = "";
                divAnchorwaitTimeDepature.Visible = true;
                break;
            case "PD":
                dvPart.Visible = true;
                pnlDrifting.Visible = true;
                tblETBDate.Visible = true;
                trSteaming.Visible = false;
                lblReportType.Text = "Noon at Drifiting Details";
                lblReportName.Text = "Noon at Drifiting Report";
                txtSinceLastReport.Text = "";
                divAnchorwaitTimeDepature.Visible = true;
                break;
            case "SH":
                dvPart.Visible = false;
                trSteaming.Visible = true;
                lblReportType.Text = "Shifting Details";
                lblReportName.Text = "Shifting Report";
                txtSinceLastReport.Text = "";
                divAnchorwaitTimeDepature.Visible = true;
                break;
        }

        //----------------------
        if (ReportsPK <= 0)
        {
            if (ddlReportType.SelectedValue == "D")
                RemoveAutoCopyData_Dueto_Departure();
            else
                CopyVoyageInformation();
        }

        //pnlArr.Visible = true;
        //pnlAnchoring.Visible = true;
        //pnlBerthing.Visible = true;
        //pnlDrifting.Visible = true;
        //tblETBDate.Visible = true;
    }

    public void RemoveAutoCopyData_Dueto_Departure()
    {
        txtVoyageNumber.Text = "";
        txtVoyInstructions.Text = "";
        ddlDepVoyCondition.SelectedIndex = 0;
        txtDepPort.Text = txtDepArrivalPort.Text;
        txtDepArrivalPort.Text = "";
        txtArrivalPortAgent.Text = "";
        txtPersonalIncharge.Text = "";
        txtAddressContactDetails.Text = "";

        txtPortCall1.Text = "";
        txtPortCallDate1.Text = "";

        txtPortCall2.Text = "";
        txtPortCallDate2.Text = "";

        txtPortCall3.Text = "";
        txtPortCallDate3.Text = "";

        txtCPS.Text = "";
        txtCPCons.Text = "";

        txtCOSPDate.Text = "";
        ddlCOSPHrs.SelectedIndex = 0;
        ddlCOSPMin.SelectedIndex = 0;

        txtArrivalPortETA.Text = "";
        ddlArrivalPortETAHH.SelectedIndex = 0;
        ddlArrivalPortETAMM.SelectedIndex = 0;
    }
    public void ShowRecord()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM VW_VSL_VPRNoonReport_New WHERE REPORTSPK=" + Key + " And VesselID='" + CurrentVessel + "'");
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            ReportsPK = Common.CastAsInt32(dt.Rows[0]["ReportsPK"]);

            //--------------------------

            DataTable dtMax = Common.Execute_Procedures_Select_ByQuery("SELECT isnull(MAX(ReportsPK),0) FROM VW_VSL_VPRNoonReport_New WHERE VESSELID='" + CurrentVessel + "'");
            int MaxReportId = Common.CastAsInt32(dtMax.Rows[0][0]);
            if (MaxReportId > 0 && MaxReportId != ReportsPK)
            {
               //ShowMessage("Note : This report can not modified ( Only last report can be modified ).", true);
               
            }

            ddlReportType.SelectedValue = dr["ACTIVITY_CODE"].ToString();
            ManageReportType();

            //--------------------------

            lblReportID.Text = dr["reportsPK"].ToString();
            txtVesselCode.Text=	dr["VesselID"].ToString();
            txtRDate.Text=Common.ToDateString(dr["ReportDate"]);

            ddlLocation.SelectedIndex = Common.CastAsInt32(dr["Location"]);

            txtRDate.Enabled = false;
            txtVoyageNumber.Text=	dr["VoyageNo"].ToString();

            txtVoyInstructions.Text=	dr["VoyInstructions"].ToString();

            ddlSteamingHours.SelectedValue=	dr["SteamingHrs"].ToString().PadLeft(2,'0');
            ddlSteamingMin.SelectedValue = dr["SteamingMin"].ToString().PadLeft(2, '0');

            txtDistanceMadeGood.Text=	dr["DistanceMadeGood"].ToString();
            txtAvgSpeed.Text=	dr["AvgSpeed"].ToString();
            ddlStoppages.SelectedIndex=Common.CastAsInt32(dr["Stoppages"]);
            if (Common.CastAsInt32(dr["Stoppages"]) == 2)
                trStopagesRemarks.Visible = false;
            else
                trStopagesRemarks.Visible = true;

            ddlStoppageTimeHH.SelectedValue = Common.CastAsInt32(dr["StoppagesHH"]).ToString().PadLeft(2, '0');
            ddlStoppageTimeMM.SelectedValue = Common.CastAsInt32(dr["StoppagesMM"]).ToString().PadLeft(2, '0');
            
            //ddlDisplacement.SelectedValue =  Common.CastAsInt32(dr["Displacement"]).ToString();

            Remarks.Text=	dr["Remarks"].ToString();

            ddlDepVoyCondition.SelectedIndex=	Common.CastAsInt32(dr["VoyCondition"]);
            txtDepPort.Text=	dr["DepPort"].ToString();
            txtDepArrivalPort.Text=	dr["DepArrivalPort"].ToString();

            txtCPS.Text = dr["CharterPartySpeed"].ToString();
            txtCPCons.Text = dr["CharterPartyCons"].ToString();

            txtCOSPDate.Text = Common.ToDateString(dr["COSPDate"]);
            ddlCOSPHrs.SelectedValue = dr["COSPHrs"].ToString().PadLeft(2, '0');
            ddlCOSPMin.SelectedValue = dr["COSPMin"].ToString().PadLeft(2, '0');

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

            txtRobMECC.Text = Common.CastAsInt32( dt.Rows[0]["ROBIFO45Lube"].ToString()).ToString();
            txtRobMECYL.Text = Common.CastAsInt32( dt.Rows[0]["ROBIFO1Lube"].ToString()).ToString();

            txtRobMECYLHighBN.Text = Common.CastAsInt32(dt.Rows[0]["ROBIFO1Lube_HighBN"].ToString()).ToString();

            txtRobAECC.Text = Common.CastAsInt32( dt.Rows[0]["ROBMGO5Lube"].ToString()).ToString();
            txtRobHYD.Text = Common.CastAsInt32(dt.Rows[0]["ROBMGO1Lube"].ToString()).ToString();
        
            hfdRobMECC_S.Value =  dt.Rows[0]["ROBIFO45Lube"].ToString();
            hfdRobMECYL_S.Value = dt.Rows[0]["ROBIFO1Lube"].ToString();

            hfdRobMECYL_SHighBN.Value = dt.Rows[0]["ROBIFO1Lube_HighBN"].ToString();

            //hfdRobMECYLHighBN.Value = dt.Rows[0]["ROBMECYL_HighBN"].ToString();

            hfdRobAECC_S.Value = dt.Rows[0]["ROBMGO5Lube"].ToString();
            hfdRobHYD_S.Value = dt.Rows[0]["ROBMGO1Lube"].ToString();

            // Start Added by Mehul

            txtMECCFWE.Text = Common.CastAsInt32(dt.Rows[0]["FWEIFO45Lube"].ToString()).ToString();
            hdnMECCFWE_S.Value = Common.CastAsInt32(dt.Rows[0]["FWEIFO45Lube"].ToString()).ToString();

            txtMECYLFWE.Text = Common.CastAsInt32(dt.Rows[0]["FWEIFO1Lube"].ToString()).ToString();
            hfdMECYLFWE_S.Value = Common.CastAsInt32(dt.Rows[0]["FWEIFO1Lube"].ToString()).ToString();

            txtMECYLHighBNFWE.Text = Common.CastAsInt32(dt.Rows[0]["FWEIFO1Lube_HighBN"].ToString()).ToString();
            hfdMECYLHighBNFWE_S.Value = Common.CastAsInt32(dt.Rows[0]["FWEIFO1Lube_HighBN"].ToString()).ToString();

            txtAECCFWE.Text = Common.CastAsInt32(dt.Rows[0]["FWEMGO5Lube"].ToString()).ToString();
            hfdAECCFWE_S.Value = Common.CastAsInt32(dt.Rows[0]["FWEMGO5Lube"].ToString()).ToString();

            txtHYDFWE.Text = Common.CastAsInt32(dt.Rows[0]["FWEMGO1Lube"].ToString()).ToString();
            hfdHYDFWE_S.Value = Common.CastAsInt32(dt.Rows[0]["FWEMGO1Lube"].ToString()).ToString();

            // END Added by Mehul 

            //----------------- Fresh Water -----------------------

            txtRobFesshWater.Text = dt.Rows[0]["ROBIFO45FreshWater"].ToString();
            hfdRobFesshWater_S.Value = dt.Rows[0]["ROBIFO45FreshWater"].ToString();

            // Added by Mehul 
            hfdFWEFesshWater_S.Value = dt.Rows[0]["FWEIFO45FreshWater"].ToString(); // Mehul
            txtFWEFesshWater.Text = dt.Rows[0]["FWEIFO45FreshWater"].ToString(); // Mehul
            // Added by Mehul 

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
            
            txtLubeFresh_MECC.Text=	Common.CastAsInt32( dr["MECCLube"].ToString()).ToString();
            txtLubeFresh_MECYL.Text=	Common.CastAsInt32( dr["MECYLLube"].ToString()).ToString();

            txtLubeFresh_MECYLHighBN.Text = Common.CastAsInt32(dr["MECYLLube_HighBN"].ToString()).ToString();

            txtLubeFresh_AECC.Text=Common.CastAsInt32( 	dr["AECCLube"].ToString()).ToString();
            txtLubeFresh_HYD.Text = Common.CastAsInt32(dr["HYDLube"].ToString()).ToString();


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
            txtTotalDisplacement.Text = dr["TotalDisplacement"].ToString();

            //------------------6/6/2016
            txtAuxTempMin1.Text = dr["AuxTempMin1"].ToString();
            txtAuxTempMin2.Text = dr["AuxTempMin2"].ToString();
            txtAuxTempMin3.Text = dr["AuxTempMin3"].ToString();
            txtAuxTempMin4.Text = dr["AuxTempMin4"].ToString();
            txtAuxTempMax1.Text = dr["AuxTempMax1"].ToString();
            txtAuxTempMax2.Text = dr["AuxTempMax2"].ToString();
            txtAuxTempMax3.Text = dr["AuxTempMax3"].ToString();
            txtAuxTempMax4.Text = dr["AuxTempMax4"].ToString();
            txtConsumptionRemark.Text = dr["ConsumptionRemark"].ToString();
            //------------------
            //----------------- fuel recv -----------------------

            txtRobIFO45Recv.Text = dt.Rows[0]["RECIFO45"].ToString();
            txtRobIFO1Recv.Text = dt.Rows[0]["RECIFO1"].ToString();
            // txtRobMGO5Recv.Text = dt.Rows[0]["ROBMGO5RECV"].ToString();
            txtRobMGO5Recv.Text = dt.Rows[0]["RECMGO5"].ToString();
            txtRobMGO1Recv.Text = dt.Rows[0]["RECMGO1"].ToString();
            txtRobMDORecv.Text = dt.Rows[0]["RECMDO"].ToString();


            txtRobMECCRecv.Text = Common.CastAsInt32( dt.Rows[0]["RECMECC"].ToString()).ToString();
            txtRobMECYLRecv.Text = Common.CastAsInt32(dt.Rows[0]["RECMECYL"].ToString()).ToString();

            txtRobMECYLRecvHighBN.Text = Common.CastAsInt32(dt.Rows[0]["RECMECYL_HighBN"].ToString()).ToString();

            txtRobAECCRecv.Text = Common.CastAsInt32(dt.Rows[0]["RECAECC"].ToString()).ToString();
            txtRobHYDRecv.Text = Common.CastAsInt32(dt.Rows[0]["RECHYD"].ToString()).ToString();
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
            ddlDriftingReason.SelectedValue =Convert.ToString(dr["DriftReason"]).Trim();
       
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

            txtSinceLastReport.Text = dr["SinceLastReport"].ToString();

            //- Comments-------------------------------------------------
            //chkMarineVerified.Visible = false;
            //chkTechVerified.Visible = false;

            //if (dr["MarineVerifiedBy"].ToString() != "")
            //{
            //    lblMsgCommentsMarineUpdateByOn.Text = dr["MarineVerifiedBy"].ToString() + "/" + Common.ToDateString(dr["MarineVerifiedOn"].ToString());
            //    hfdMBy.Value = dr["MarineVerifiedBy"].ToString();
            //    txtMarineRemarks.Text = dr["MarineComments"].ToString();
            //    btnSaveMarineRemarks.Visible = false;
            //}
            //else
            //    lblMsgCommentsMarineUpdateByOn.Text = "";
            //---------------------------------------
            //if(Convert.IsDBNull(dr["MarineVerified"]))
            //    chkMarineVerified.Visible = true;
            //else if (dr["MarineVerified"].ToString() == "False")
            //    chkMarineVerified.Visible = true;
            //---------------------------------------
            //if (dr["TechVerifiedBy"].ToString() != "")
            //{
            //    lblMsgCommentsTechnicalUpdateByOn.Text = dr["TechVerifiedBy"].ToString() + "/" + Common.ToDateString(dr["TechVerifiedOn"].ToString());
            //    hfdTBy.Value = dr["TechVerifiedBy"].ToString();
            //    txtTechnicalRemarks.Text = dr["TechnicalComments"].ToString();
            //    btnSaveTechnicalRemarks.Visible = false;
            //}
            //else
            //    lblMsgCommentsTechnicalUpdateByOn.Text = "";
            //---------------------------------------
            //if (Convert.IsDBNull(dr["TechVerified"]))
            //    chkTechVerified.Visible = true;
            //else if (dr["TechVerified"].ToString() == "False")
            //    chkTechVerified.Visible = true;


            //- Port ---------------------------------------
            txtPortCall1.Text = dr["Port1"].ToString();
            txtPortCallDate1.Text = Common.ToDateString(dr["PortETA1"].ToString());

            txtPortCall2.Text = dr["Port2"].ToString();
            txtPortCallDate2.Text = Common.ToDateString(dr["PortETA2"].ToString());

            txtPortCall3.Text = dr["Port3"].ToString();
            txtPortCallDate3.Text = Common.ToDateString(dr["PortETA3"].ToString());


            //-Laden * Enpty-----------------------------------------------
            txtLaden20Ft.Text = dr["Laden20Ft"].ToString();
            txtLaden40Ft.Text = dr["Laden40Ft"].ToString();
            txtLaden45Ft.Text = dr["Laden45Ft"].ToString();
            txtEmpty20Ft.Text = dr["Empty20Ft"].ToString();
            txtEmpty40Ft.Text = dr["Empty40Ft"].ToString();
            txtEmpty45Ft.Text = dr["Empty45Ft"].ToString();


            txtREFRLaden20Ft.Text = dr["REFRLaden20Ft"].ToString();
            txtREFRLaden40Ft.Text = dr["REFRLaden40Ft"].ToString();

            txtSludgelanded.Text = Common.CastAsDecimal(dr["Sludgelanded"].ToString()).ToString();
            txtOilyBilgWaterLanded.Text = Common.CastAsDecimal(dr["OilyBilgWaterLanded"].ToString()).ToString();
            txtBilgeWaterLanded.Text = Common.CastAsDecimal(dr["BilgeWaterLanded"].ToString()).ToString();
            txtSootLanded.Text = Common.CastAsDecimal(dr["SootLanded"].ToString()).ToString();

            txtBILGEWaterTankROB.Text = Common.CastAsDecimal(dr["BILGEWaterTankROB"].ToString()).ToString();
            txtOILYBILGETankROB.Text = Common.CastAsDecimal(dr["OILYBILGETankROB"].ToString()).ToString();
            txtSLUDGETANKROB.Text = Common.CastAsDecimal(dr["SLUDGETANKROB"].ToString()).ToString();

            txtOWSH.Text = Common.CastAsInt32(dr["OWSH"].ToString()).ToString();
            txtOWSM.Text = Common.CastAsInt32(dr["OWSM"].ToString()).ToString();
            txtINCINERATOR_H.Text = Common.CastAsInt32(dr["INCINERATOR_H"].ToString()).ToString();
            txtINCINERATOR_M.Text = Common.CastAsInt32(dr["INCINERATOR_M"].ToString()).ToString();
            txtGARBAGE_ROB.Text = Common.CastAsDecimal(dr["GARBAGE_ROB"].ToString()).ToString();

            txtQTYBallasted.Text = dr["QTYBallasted"].ToString();
            txtQTYDeBallasted.Text = dr["QTYDeBallasted"].ToString();
try{
            ddlBWTS.SelectedValue = dr["BWTS"].ToString();
}catch(Exception ex)
{
}
            txtBWTSHrs.Text = dr["BWTSHrs"].ToString();
            txtBWTSRemarks.Text = dr["BWTSRemarks"].ToString();

            try
            {
                ddlSeasonalLoadLines.SelectedValue = dr["SeasonalLoadLines"].ToString();
            }
            catch { }

            try
            {
                ddlMaximumCargoCapacity.SelectedValue = dr["MaximumCargoCapacity"].ToString();
            }
            catch { }

            try
            { ddlNextPortOperation.SelectedValue = dr["ARPort_Operation"].ToString(); }
            catch { }
            try
            {
                int Anchor_Aw_LT_H = 0;
                if (int.TryParse(dr["Anchor_Aw_LT_H"].ToString(), out Anchor_Aw_LT_H))
                {
                    ddlAnchorWaitTime_H.SelectedValue = Anchor_Aw_LT_H < 10 ? "0" + Anchor_Aw_LT_H.ToString() : Anchor_Aw_LT_H.ToString();
                }
            }
            catch { }
            try
            {

                int Anchor_Aw_LT_M = 0;
                if (int.TryParse(dr["Anchor_Aw_LT_M"].ToString(), out Anchor_Aw_LT_M))
                {
                    ddlAnchorWaitTime_M.SelectedValue = Anchor_Aw_LT_M < 10 ? "0" + Anchor_Aw_LT_M.ToString() : Anchor_Aw_LT_M.ToString();
                }

            }
            catch { }
            // Added by Mehul
            try
            {
                int Anchor_Aw_LT_Arrival_H = 0;
                if (int.TryParse(dr["Anchor_Aw_LT_Arrival_H"].ToString(), out Anchor_Aw_LT_Arrival_H))
                {
                    ddlAnchorWaitTimeArrive_H.SelectedValue = Anchor_Aw_LT_Arrival_H < 10 ? "0" + Anchor_Aw_LT_Arrival_H.ToString() : Anchor_Aw_LT_Arrival_H.ToString();
                }
            }
            catch { }

            try
            {
                int Anchor_Aw_LT_Arrival_M = 0;
                if (int.TryParse(dr["Anchor_Aw_LT_Arrival_H"].ToString(), out Anchor_Aw_LT_Arrival_M))
                {
                    ddlAnchorWaitTimeArrive_M.SelectedValue = Anchor_Aw_LT_Arrival_M < 10 ? "0" + Anchor_Aw_LT_Arrival_M.ToString() : Anchor_Aw_LT_Arrival_M.ToString();
                }
            }
            catch { }
            // Added by Mehul
            try
            {
                int SBE_LT_H = 0;
                if (int.TryParse(dr["SBE_LT_H"].ToString(), out SBE_LT_H))
                {
                    ddlSBELT_H.SelectedValue = SBE_LT_H < 10 ? "0" + SBE_LT_H.ToString() : SBE_LT_H.ToString();
                }

            }
            catch { }
            try
            {

                int SBE_LT_M = 0;
                if (int.TryParse(dr["SBE_LT_M"].ToString(), out SBE_LT_M))
                {
                    ddlSBELT_M.SelectedValue = SBE_LT_M < 10 ? "0" + SBE_LT_M.ToString() : SBE_LT_M.ToString();
                }

            }
            catch { }
            try
            {

                int Pilot_OB_H = 0;
                if (int.TryParse(dr["Pilot_OB_H"].ToString(), out Pilot_OB_H))
                {
                    ddlPilotOnBoard_H.SelectedValue = Pilot_OB_H < 10 ? "0" + Pilot_OB_H.ToString() : Pilot_OB_H.ToString();
                }

            }
            catch { }
            try
            {
                int Pilot_OB_M = 0;
                if (int.TryParse(dr["Pilot_OB_M"].ToString(), out Pilot_OB_M))
                {
                    ddlPilotOnBoard_M.SelectedValue = Pilot_OB_M < 10 ? "0" + Pilot_OB_M.ToString() : Pilot_OB_M.ToString();
                }

            }
            catch { }
            try
            {


                int Pilot_AWT_H = 0;
                if (int.TryParse(dr["Pilot_AWT_H"].ToString(), out Pilot_AWT_H))
                {
                    ddlPilotAwayTime_H.SelectedValue = Pilot_AWT_H < 10 ? "0" + Pilot_AWT_H.ToString() : Pilot_AWT_H.ToString();
                }


            }
            catch { }
            try
            {

                int Pilot_AWT_M = 0;
                if (int.TryParse(dr["Pilot_AWT_M"].ToString(), out Pilot_AWT_M))
                {
                    ddlPilotAwayTime_M.SelectedValue = Pilot_AWT_M < 10 ? "0" + Pilot_AWT_M.ToString() : Pilot_AWT_M.ToString();
                }

            }
            catch { }
            txtCargoQtyLoaded.Text = dr["CargoQtyLoaded"].ToString();
            txtBLQty.Text = dr["BLQty"].ToString();
            txtVLSFO_ROBSBE.Text = dr["VLSFO_ROBSBE"].ToString();
            txtLSMGO_ROBSBE.Text = dr["LSMGO_ROBSBE"].ToString();
            txtMDO_ROBSBE.Text = dr["MDO_ROBSBE"].ToString();
            // Added by Mehul 
            txtVLSFO_FWE.Text = dr["VLSFO_FWE"].ToString();
            txtLSMGO_FWE.Text = dr["LSMGO_FWE"].ToString();
            txtMDO_FWE.Text = dr["MDO_FWE"].ToString();
            txtMEFlowmeterReadingatFWE.Text = dr["MEFlowmeterReadingatFWE"].ToString();
            txtAEFlowmeterReadingatFWE.Text = dr["AEFlowmeterReadingatFWE"].ToString();
            txtBoilerFlowmeterReadingatFWE.Text = dr["BoilerFlowmeterReadingatFWE"].ToString();
            txtVLSFOFWE.Text = dr["VLSFOTankInfoFWE"].ToString();
            txtLSMGOFWE.Text = dr["LSMGOTankInfoFWE"].ToString();
            txtMDOFWE.Text = dr["MDOTankInfoFWE"].ToString();
            // Added by Mehul 
            txtMEFlowmeterReadingatCOSP.Text = dr["MEFlowmeterReadingatCOSP"].ToString();
            txtAEFlowmeterReadingatCOSP.Text = dr["AEFlowmeterReadingatCOSP"].ToString();
            txtBoilerFlowmeterReadingatCOSP.Text = dr["BoilerFlowmeterReadingatCOSP"].ToString();
            txtMEFlowmeterReadingatSBE.Text = dr["MEFlowmeterReadingatSBE"].ToString();
            txtAEFlowmeterReadingatSBE.Text = dr["AEFlowmeterReadingatSBE"].ToString();
            txtBoilerFlowmeterReadingatSBE.Text = dr["BoilerFlowmeterReadingatSBE"].ToString();
            txtVLSFOTankInUse.Text = dr["VLSFOTankInUse"].ToString();
            txtLSMGOTankInUse.Text = dr["LSMGOTankInUse"].ToString();
            txtMDOTankInUse.Text = dr["MDOTankInUse"].ToString();
            txtVLSFOTankDensity.Text = dr["VLSFOTankDensity"].ToString();
            txtLSMGOTankDensity.Text = dr["LSMGOTankDensity"].ToString();
            txtMDOTankDensity.Text = dr["MDOTankDensity"].ToString();
            txtVLSFO_ROBCOSP.Text = dr["VLSFO_ROBCOSP"].ToString();
            txtLSMGO_ROBCOSP.Text = dr["LSMGO_ROBCOSP"].ToString();
            txtMDO_ROBCOSP.Text = dr["MDO_ROBCOSP"].ToString();
            //---------
            if (!string.IsNullOrEmpty(dr["HFOPRH1"].ToString()))
            {
                txtHFOPRH1.Text = dr["HFOPRH1"].ToString();
            }
            else
            {
                txtHFOPRH1.Text = "0";
            }
            if (!string.IsNullOrEmpty(dr["HFOPRH2"].ToString()))
            {
                txtHFOPRH2.Text = dr["HFOPRH2"].ToString();
            }
            else
            {
                txtHFOPRH2.Text = "0";
            }
            if (!string.IsNullOrEmpty(dr["MELOPRH"].ToString()))
            {
                txtMELOPRH.Text = dr["MELOPRH"].ToString();
            }
            else
            {
                txtMELOPRH.Text = "0";
            }
            if (!string.IsNullOrEmpty(dr["AEPRH"].ToString()))
            {
                txtAEPRH.Text = dr["AEPRH"].ToString();
            }
            else
            {
                txtAEPRH.Text = "0";
            }
        }
    }
    
  

    protected void ddlReportType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ManageReportType();
    }
    protected void CopyVoyageInformation()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT TOP 1 VoyageNo,VoyCondition,* from VW_VSL_VPRNoonReport_New WHERE ACTIVITY_CODE IN ('D','N') ORDER BY ReportsPK DESC");
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];

            txtVoyageNumber.Text = dr["VoyageNo"].ToString();
            txtVoyInstructions.Text = dr["VoyInstructions"].ToString();
            ddlDepVoyCondition.SelectedIndex = Common.CastAsInt32(dr["VoyCondition"]);
            txtDepPort.Text = dr["DepPort"].ToString();
            txtDepArrivalPort.Text = dr["DepArrivalPort"].ToString();
            txtArrivalPortAgent.Text = dr["ArrivalPortAgent"].ToString();
            txtPersonalIncharge.Text = dr["PersonalIncharge"].ToString();
            txtAddressContactDetails.Text = dr["AddressContactDetails"].ToString();

            txtPortCall1.Text = dr["Port1"].ToString();
            txtPortCallDate1.Text = Common.ToDateString(dr["PortETA1"].ToString());

            txtPortCall2.Text = dr["Port2"].ToString();
            txtPortCallDate2.Text = Common.ToDateString(dr["PortETA2"].ToString());

            txtPortCall3.Text = dr["Port3"].ToString();
            txtPortCallDate3.Text = Common.ToDateString(dr["PortETA3"].ToString());

            txtCPS.Text = dr["CharterPartySpeed"].ToString();
            txtCPCons.Text = dr["CharterPartyCons"].ToString();

            txtCOSPDate.Text = Common.ToDateString(dr["COSPDate"]);
            ddlCOSPHrs.SelectedValue = dr["COSPHrs"].ToString().PadLeft(2, '0');
            ddlCOSPMin.SelectedValue = dr["COSPMin"].ToString().PadLeft(2, '0');

            txtArrivalPortETA.Text = Common.ToDateString(dr["ArrivalPortETA"]);
            ddlArrivalPortETAHH.SelectedValue = dr["ArrivalPortETAHrs"].ToString().PadLeft(2, '0');
            ddlArrivalPortETAMM.SelectedValue = dr["ArrivalPortETAMin"].ToString().PadLeft(2, '0');


            try
            { ddlSeasonalLoadLines.SelectedValue = dr["SeasonalLoadLines"].ToString(); }
            catch { }
            try
            { ddlMaximumCargoCapacity.SelectedValue = dr["MaximumCargoCapacity"].ToString(); }
            catch { }
            try
            { ddlNextPortOperation.SelectedValue = dr["ARPort_Operation"].ToString(); }
            catch { }
            try
            {
                int Anchor_Aw_LT_H = 0;
                if (int.TryParse(dr["Anchor_Aw_LT_H"].ToString(), out Anchor_Aw_LT_H))
                {
                    ddlAnchorWaitTime_H.SelectedValue = Anchor_Aw_LT_H < 10 ? "0" + Anchor_Aw_LT_H.ToString() : Anchor_Aw_LT_H.ToString();
                }
            }
            catch { }
            try
            {

                int Anchor_Aw_LT_M = 0;
                if (int.TryParse(dr["Anchor_Aw_LT_M"].ToString(), out Anchor_Aw_LT_M))
                {
                    ddlAnchorWaitTime_M.SelectedValue = Anchor_Aw_LT_M < 10 ? "0" + Anchor_Aw_LT_M.ToString() : Anchor_Aw_LT_M.ToString();
                }

            }
            catch { }
            // Added by Mehul
            try
            {
                int Anchor_Aw_LT_Arrival_H = 0;
                if (int.TryParse(dr["Anchor_Aw_LT_Arrival_H"].ToString(), out Anchor_Aw_LT_Arrival_H))
                {
                    ddlAnchorWaitTimeArrive_H.SelectedValue = Anchor_Aw_LT_Arrival_H < 10 ? "0" + Anchor_Aw_LT_Arrival_H.ToString() : Anchor_Aw_LT_Arrival_H.ToString();
                }
            }
            catch { }
            try
            {
                int Anchor_Aw_LT_Arrival_M = 0;
                if (int.TryParse(dr["Anchor_Aw_LT_Arrival_H"].ToString(), out Anchor_Aw_LT_Arrival_M))
                {
                    ddlAnchorWaitTimeArrive_M.SelectedValue = Anchor_Aw_LT_Arrival_M < 10 ? "0" + Anchor_Aw_LT_Arrival_M.ToString() : Anchor_Aw_LT_Arrival_M.ToString();
                }
            }
            catch { }
            // Added by Mehul
            try
            {
                int SBE_LT_H = 0;
                if (int.TryParse(dr["SBE_LT_H"].ToString(), out SBE_LT_H))
                {
                    ddlSBELT_H.SelectedValue = SBE_LT_H < 10 ? "0" + SBE_LT_H.ToString() : SBE_LT_H.ToString();
                }

            }
            catch { }
            try
            {

                int SBE_LT_M = 0;
                if (int.TryParse(dr["SBE_LT_M"].ToString(), out SBE_LT_M))
                {
                    ddlSBELT_M.SelectedValue = SBE_LT_M < 10 ? "0" + SBE_LT_M.ToString() : SBE_LT_M.ToString();
                }

            }
            catch { }
            try
            {

                int Pilot_OB_H = 0;
                if (int.TryParse(dr["Pilot_OB_H"].ToString(), out Pilot_OB_H))
                {
                    ddlPilotOnBoard_H.SelectedValue = Pilot_OB_H < 10 ? "0" + Pilot_OB_H.ToString() : Pilot_OB_H.ToString();
                }

            }
            catch { }
            try
            {
                int Pilot_OB_M = 0;
                if (int.TryParse(dr["Pilot_OB_M"].ToString(), out Pilot_OB_M))
                {
                    ddlPilotOnBoard_M.SelectedValue = Pilot_OB_M < 10 ? "0" + Pilot_OB_M.ToString() : Pilot_OB_M.ToString();
                }

            }
            catch { }
            try
            {


                int Pilot_AWT_H = 0;
                if (int.TryParse(dr["Pilot_AWT_H"].ToString(), out Pilot_AWT_H))
                {
                    ddlPilotAwayTime_H.SelectedValue = Pilot_AWT_H < 10 ? "0" + Pilot_AWT_H.ToString() : Pilot_AWT_H.ToString();
                }


            }
            catch { }
            try
            {

                int Pilot_AWT_M = 0;
                if (int.TryParse(dr["Pilot_AWT_M"].ToString(), out Pilot_AWT_M))
                {
                    ddlPilotAwayTime_M.SelectedValue = Pilot_AWT_M < 10 ? "0" + Pilot_AWT_M.ToString() : Pilot_AWT_M.ToString();
                }

            }
            catch { }

            txtTotalCargoWeight.Text = dr["TotalCargoWeight"].ToString();
            txtBallastWeight.Text = dr["BallastWeight"].ToString();
            txtTotalDisplacement.Text = dr["TotalDisplacement"].ToString();
            txtCargoQtyLoaded.Text = dr["CargoQtyLoaded"].ToString();
            txtBLQty.Text = dr["BLQty"].ToString();
            txtDepDraftFwd.Text = dr["DraftFwd"].ToString();
            txtDepDraftAft.Text = dr["DraftAft"].ToString();
            // Added by Mehul 
            txtVLSFO_FWE.Text = dr["VLSFO_FWE"].ToString();
            txtLSMGO_FWE.Text = dr["LSMGO_FWE"].ToString();
            txtMDO_FWE.Text = dr["MDO_FWE"].ToString();
            txtMEFlowmeterReadingatFWE.Text = dr["MEFlowmeterReadingatFWE"].ToString();
            txtAEFlowmeterReadingatFWE.Text = dr["AEFlowmeterReadingatFWE"].ToString();
            txtBoilerFlowmeterReadingatFWE.Text = dr["BoilerFlowmeterReadingatFWE"].ToString();
            txtVLSFOFWE.Text = dr["VLSFOTankInfoFWE"].ToString();
            txtLSMGOFWE.Text = dr["LSMGOTankInfoFWE"].ToString();
            txtMDOFWE.Text = dr["MDOTankInfoFWE"].ToString();
            // Added by Mehul 
        }
    }
    public bool SaveRecord(int isComplete)
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
        decimal ROBMECC = 0, ROBMECYL = 0, ROBMECYL_HighBN = 0, ROBAECC = 0, ROBHYD = 0;
        decimal ROBFESSHWATER = 0;
        decimal FWEFESSHWATER = 0; //-- Mehul
        decimal FWEMECC = 0, FWEMECYL = 0, FWEMECYL_HighBN = 0, FWEAECC = 0, FWEHYD = 0;
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
        // Added by Mehul 
        FWEFESSHWATER = Common.CastAsDecimal(hfdFWEFesshWater_S.Value);
        FWEMECC = Common.CastAsDecimal(hdnMECCFWE_S.Value);
        FWEMECYL = Common.CastAsDecimal(hfdMECYLFWE_S.Value);
        FWEMECYL_HighBN = Common.CastAsDecimal(hfdMECYLHighBNFWE_S.Value);
        FWEAECC = Common.CastAsDecimal(hfdAECCFWE_S.Value);
        FWEHYD = Common.CastAsDecimal(hfdHYDFWE_S.Value);
        // 
        bool res = false;
        Common.Set_Procedures("SHIP_VPR_InsertUpdate_VPRNoonReport_New");
        Common.Set_ParameterLength(228 + 26 + 3 + 1 + 21);
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
        new MyParameter("@Inert_Mins", Common.CastAsInt32(txt_InertM.Text)),
         new MyParameter("@CharterPartyCons", Common.CastAsDecimal(txtCPCons.Text)),
        new MyParameter("@TotalDisplacement", Common.CastAsDecimal(txtTotalDisplacement.Text)),

        new MyParameter("@ROBIFO1Lube_HighBN", ROBMECYL_HighBN),
        new MyParameter("@MECYLLube_HighBN", Common.CastAsDecimal(txtLubeFresh_MECYLHighBN.Text)),
         new MyParameter("@RECMECYL_HighBN", Common.CastAsDecimal(txtRobMECYLRecvHighBN.Text)),

        new MyParameter("@SinceLastReport", Common.CastAsDecimal(txtSinceLastReport.Text)),

         new MyParameter("@Laden20Ft", Common.CastAsInt32(txtLaden20Ft.Text)),
         new MyParameter("@Laden40Ft", Common.CastAsInt32(txtLaden40Ft.Text)),
         new MyParameter("@Laden45Ft", Common.CastAsInt32(txtLaden45Ft.Text)),
         new MyParameter("@Empty20Ft", Common.CastAsInt32(txtEmpty20Ft.Text)),
         new MyParameter("@Empty40Ft", Common.CastAsInt32(txtEmpty40Ft.Text)),
         new MyParameter("@Empty45Ft", Common.CastAsInt32(txtEmpty45Ft.Text)),


         new MyParameter("@AuxTempMin1", Common.CastAsDecimal(txtAuxTempMin1.Text)),
         new MyParameter("@AuxTempMin2", Common.CastAsDecimal(txtAuxTempMin2.Text)),
         new MyParameter("@AuxTempMin3", Common.CastAsDecimal(txtAuxTempMin3.Text)),
         new MyParameter("@AuxTempMin4", Common.CastAsDecimal(txtAuxTempMin4.Text)),
         new MyParameter("@AuxTempMax1", Common.CastAsDecimal(txtAuxTempMax1.Text)),
         new MyParameter("@AuxTempMax2", Common.CastAsDecimal(txtAuxTempMax2.Text)),
         new MyParameter("@AuxTempMax3", Common.CastAsDecimal(txtAuxTempMax3.Text)),
         new MyParameter("@AuxTempMax4", Common.CastAsDecimal(txtAuxTempMax4.Text)),
         new MyParameter("@ConsumptionRemark", txtConsumptionRemark.Text),

         //----------------------------
         new MyParameter("@Sludgelanded", Common.CastAsDecimal(txtSludgelanded.Text)),
         new MyParameter("@OilyBilgWaterLanded", Common.CastAsDecimal(txtOilyBilgWaterLanded.Text)),
         new MyParameter("@BilgeWaterLanded", Common.CastAsDecimal(txtBilgeWaterLanded.Text)),
         new MyParameter("@SootLanded", Common.CastAsDecimal(txtSootLanded.Text)),

         new MyParameter("@BILGEWaterTankROB", Common.CastAsDecimal(txtBILGEWaterTankROB.Text)),
         new MyParameter("@OILYBILGETankROB", Common.CastAsDecimal(txtOILYBILGETankROB.Text)),
         new MyParameter("@SLUDGETANKROB", Common.CastAsDecimal(txtSLUDGETANKROB.Text)),
         new MyParameter("@OWSH", Common.CastAsInt32(txtOWSH.Text)),
         new MyParameter("@OWSM", Common.CastAsInt32(txtOWSM.Text)),
         new MyParameter("@INCINERATOR_H", Common.CastAsInt32(txtINCINERATOR_H.Text)),
         new MyParameter("@INCINERATOR_M", Common.CastAsInt32(txtINCINERATOR_M.Text)),
         new MyParameter("@GARBAGE_ROB", Common.CastAsDecimal(txtGARBAGE_ROB.Text)),

         new MyParameter("@REFRLaden20Ft", Common.CastAsInt32(txtREFRLaden20Ft.Text)),
         new MyParameter("@REFRLaden40Ft", Common.CastAsInt32(txtREFRLaden40Ft.Text)),

         new MyParameter("@QTYBallasted", Common.CastAsDecimal(txtQTYBallasted.Text)),
         new MyParameter("@QTYDeBallasted", Common.CastAsDecimal(txtQTYDeBallasted.Text)),
         new MyParameter("@BWTS", ddlBWTS.SelectedValue),
         new MyParameter("@BWTSHrs", Common.CastAsDecimal(txtBWTSHrs.Text)),
         new MyParameter("@BWTSRemarks", txtBWTSRemarks.Text),

         new MyParameter("@SeasonalLoadLines", ddlSeasonalLoadLines.SelectedValue),
         new MyParameter("@MaximumCargoCapacity", ddlMaximumCargoCapacity.SelectedValue),
        //new fields added by zia
        new MyParameter("@ARPort_Operation", ddlNextPortOperation.SelectedValue),
        new MyParameter("@Anchor_Aw_LT_H", ddlAnchorWaitTime_H.SelectedValue),
        new MyParameter("@Anchor_Aw_LT_M", ddlAnchorWaitTime_M.SelectedValue),
        new MyParameter("@SBE_LT_H", ddlSBELT_H.SelectedValue),
        new MyParameter("@SBE_LT_M", ddlSBELT_M.SelectedValue),
        new MyParameter("@Pilot_OB_H", ddlPilotOnBoard_H.SelectedValue),
        new MyParameter("@Pilot_OB_M", ddlPilotOnBoard_M.SelectedValue),
        new MyParameter("@Pilot_AWT_H ", ddlPilotAwayTime_H.SelectedValue),
        new MyParameter("@Pilot_AWT_M", ddlPilotAwayTime_M.SelectedValue),
        new MyParameter("@CargoQtyLoaded", txtCargoQtyLoaded.Text),
        new MyParameter("@BLQty", Common.CastAsDecimal(txtBLQty.Text)),
        new MyParameter("@VLSFO_ROBSBE", Common.CastAsDecimal(txtVLSFO_ROBSBE.Text)),
        new MyParameter("@LSMGO_ROBSBE", Common.CastAsDecimal(txtLSMGO_ROBSBE.Text)),
        new MyParameter("@MDO_ROBSBE", Common.CastAsDecimal(txtMDO_ROBSBE.Text)),
        new MyParameter("@MEFlowmeterReadingatCOSP", Common.CastAsDecimal(txtMEFlowmeterReadingatCOSP.Text)),
        new MyParameter("@AEFlowmeterReadingatCOSP", Common.CastAsDecimal(txtAEFlowmeterReadingatCOSP.Text)),
        new MyParameter("@BoilerFlowmeterReadingatCOSP", Common.CastAsDecimal(txtBoilerFlowmeterReadingatCOSP.Text)),
        new MyParameter("@MEFlowmeterReadingatSBE", Common.CastAsDecimal(txtMEFlowmeterReadingatSBE.Text)),
        new MyParameter("@AEFlowmeterReadingatSBE", Common.CastAsDecimal(txtAEFlowmeterReadingatSBE.Text)),
        new MyParameter("@BoilerFlowmeterReadingatSBE", Common.CastAsDecimal(txtBoilerFlowmeterReadingatSBE.Text)),
        new MyParameter("@VLSFOTankInUse", Common.CastAsDecimal(txtVLSFOTankInUse.Text)),
        new MyParameter("@LSMGOTankInUse", Common.CastAsDecimal(txtLSMGOTankInUse.Text)),
        new MyParameter("@MDOTankInUse", Common.CastAsDecimal(txtMDOTankInUse.Text)),
        new MyParameter("@VLSFOTankDensity", Common.CastAsDecimal(txtVLSFOTankDensity.Text)),
        new MyParameter("@LSMGOTankDensity", Common.CastAsDecimal(txtLSMGOTankDensity.Text)),
        new MyParameter("@MDOTankDensity", Common.CastAsDecimal(txtMDOTankDensity.Text)),
        new MyParameter("@VLSFO_ROBCOSP", Common.CastAsDecimal(txtVLSFO_ROBCOSP.Text)),
        new MyParameter("@LSMGO_ROBCOSP", Common.CastAsDecimal(txtLSMGO_ROBCOSP.Text)),
        new MyParameter("@MDO_ROBCOSP", Common.CastAsDecimal(txtMDO_ROBCOSP.Text)),
         new MyParameter("@isComplete", isComplete),
        //------
        new MyParameter("@FWEIFO45FreshWater", FWEFESSHWATER),
        new MyParameter("@VLSFO_FWE", Common.CastAsDecimal(txtVLSFO_FWE.Text)),
        new MyParameter("@LSMGO_FWE", Common.CastAsDecimal(txtLSMGO_FWE.Text)),
        new MyParameter("@MDO_FWE", Common.CastAsDecimal(txtMDO_FWE.Text)),
        new MyParameter("@MEFlowmeterReadingatFWE", Common.CastAsDecimal(txtMEFlowmeterReadingatFWE.Text)),
        new MyParameter("@AEFlowmeterReadingatFWE", Common.CastAsDecimal(txtAEFlowmeterReadingatFWE.Text)),
        new MyParameter("@BoilerFlowmeterReadingatFWE", Common.CastAsDecimal(txtBoilerFlowmeterReadingatFWE.Text)),
        new MyParameter("@VLSFOTankInfoFWE", Common.CastAsDecimal(txtVLSFOFWE.Text)),
        new MyParameter("@LSMGOTankInfoFWE", Common.CastAsDecimal(txtLSMGOFWE.Text)),
        new MyParameter("@MDOTankInfoFWE", Common.CastAsDecimal(txtMDOFWE.Text)),
        new MyParameter("@FWEIFO45Lube", FWEMECC),
        new MyParameter("@FWEIFO1Lube", FWEMECYL),
        new MyParameter("@FWEIFO1Lube_HighBN", FWEMECYL_HighBN),
        new MyParameter("@FWEMGO5Lube", FWEAECC),
        new MyParameter("@FWEMGO1Lube", FWEHYD),
        new MyParameter("@Anchor_Aw_LT_Arrival_H", ddlAnchorWaitTimeArrive_H.SelectedValue),
        new MyParameter("@Anchor_Aw_LT_Arrival_M", ddlAnchorWaitTimeArrive_M.SelectedValue),
        //------
        new MyParameter("@HFOPRH1", Common.CastAsInt32(txtHFOPRH1.Text.Trim())),
        new MyParameter("@HFOPRH2", Common.CastAsInt32(txtHFOPRH2.Text.Trim())),
        new MyParameter("@MELOPRH", Common.CastAsInt32(txtMELOPRH.Text.Trim())),
        new MyParameter("@AEPRH", Common.CastAsInt32(txtAEPRH.Text.Trim()))
        );

         

        DataSet ds = new DataSet();
        res = Common.Execute_Procedures_IUD(ds);
        if (res)
        {
            Key = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);
            ShowRecord();
        }
        return res;
    }
    public void ShowMessage(string msg, bool error)
    {
        lblMessage.Text = msg;
        lblMessage.ForeColor = (error ? System.Drawing.Color.Red : System.Drawing.Color.Green);
    }
    protected void btnROB_Click(object sender, EventArgs e)
    {
        string sql = "EXEC VSL_VPR_GET_FUEL_ROB_New '" + CurrentVessel + "'," + ReportsPK;
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

            trStopagesRemarks.Visible = true;
        }
        else
        {
            ddlStoppageTimeHH.Enabled = false;
            ddlStoppageTimeMM.Enabled = false;

            ddlStoppageTimeHH.SelectedIndex = 0;
            ddlStoppageTimeMM.SelectedIndex = 0;

            RequiredFieldValidator7.Enabled = false;
            RequiredFieldValidator8.Enabled = false;

            trStopagesRemarks.Visible = false;
        }
    }
    
    //protected void btnSaveMarineRemarks_OnClick(object sender, EventArgs e)
    //{
    //    if(!chkMarineVerified.Checked )
    //    {
    //        lblMsgCommentsMarine.Text = "Please confirm to select the checkbox.";
    //        return;
    //    }
    //    if (txtMarineRemarks.Text.Trim()=="")
    //    {
    //        lblMsgCommentsMarine.Text = "Please enter marine remarks.";
    //        return;
    //    }

    //    string UpdateUser = Session["UserName"].ToString();
    //    if (hfdTBy.Value.Trim() != "")
    //    {
    //        if (UpdateUser.Trim() == hfdTBy.Value.Trim())
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "val", "alert('Technical and marine user can not be same.');", true);
    //            return;
    //        }
    //    }
    //    DataTable dt = Common.Execute_Procedures_Select_ByQuery(" update VSL_VPRNoonReport_New set MarineComments='" + txtMarineRemarks.Text.Replace("'", "`") + "', MarineVerified=1,MarineVerifiedBy='" + UpdateUser + "',MarineVerifiedOn=getdate()" +
    //                                     " where ReportsPK=" + ReportsPK + " and VesselID='" + CurrentVessel + "' ");
    //    ShowMessage("Record updated successfully", false);
    //    ShowRecord();
    //}
    //protected void btnSaveTechnicalRemarks_OnClick(object sender, EventArgs e)
    //{

    //    if (!chkTechVerified.Checked)
    //    {
    //        lblMsgCommentsTechnical.Text = "Please confirm to select the checkbox.";
    //        return;
    //    }
    //    if (txtTechnicalRemarks.Text.Trim() == "")
    //    {
    //        lblMsgCommentsTechnical.Text = "Please enter technical remarks.";
    //        return;
    //    }

    //    string UpdateUser = Session["UserName"].ToString();
    //    if (hfdMBy.Value.Trim() != "")
    //    {
    //        if (UpdateUser.Trim() == hfdMBy.Value.Trim())
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "val", "alert('Marine and technical user can not be same.');", true);
    //            return;
    //        }
    //    }
    //    DataTable dt = Common.Execute_Procedures_Select_ByQuery(" update VSL_VPRNoonReport_New set TechnicalComments='" + txtTechnicalRemarks.Text.Replace("'","`") + "', TechVerified=1,TechVerifiedBy='" + UpdateUser + "',TechVerifiedOn=getdate()" +
    //                             " where ReportsPK=" + ReportsPK + " and VesselID='" + CurrentVessel + "' ");
    //    ShowMessage("Record updated successfully", false);
    //    ShowRecord();
    //}

    //protected void btnSendToShipMarine_OnClick(object sender, EventArgs e)
    //{

    //    String VesselEmail = "";
    //    if (lblMsgCommentsMarineUpdateByOn.Text.Trim()=="")
    //    {
    //        lblMsgCommentsMarine.Text = "Please enter remarks and verify to send mail.";
    //        return;
    //    }
    //    if (txtMarineRemarks.Text.Trim() == "")
    //    {
    //        lblMsgCommentsMarine.Text = "Please enter remarks and verify to send mail.";
    //        return;
    //    }
    //    DataTable dt = Common.Execute_Procedures_Select_ByQuery(" select email from Vessel where VesselCode='" + CurrentVessel + "' ");
    //     if(dt.Rows.Count>0)
    //    {
    //        VesselEmail = dt.Rows[0][0].ToString();
    //    }
        
    //    string FromAddress = "noreply@mtmsm.com";
    //    string ReplyToAddress = "noreply@mtmsm.com";
    //    string[] ToAddress = { VesselEmail };
    //    string[] CCAddress = { ProjectCommon.getUserEmailByID(Session["loginid"].ToString()) };
    //    string[] BCCAddress = { "" };
    //    string Subject = "Office Remark[ "+lblReportName.Text.Trim()+" (ID : "+lblReportID.Text.Trim()+")]";
    //    string Message = MailMessage(txtMarineRemarks.Text.Trim(), Session["UserName"].ToString());
    //    string Error;
    //    string AttachmentFilePath = "";
        
    //    if(SendMail.SendeMail(FromAddress, ReplyToAddress, ToAddress, CCAddress, BCCAddress, Subject, Message,out Error, AttachmentFilePath))
    //    {
    //        lblMsgCommentsMarine.Text = "Mail send successfully.";
    //    }
    //    else
    //    {
    //        lblMsgCommentsMarine.Text = "Error while sending mail.";
    //    }
    //}

    //protected void btnSendToShipTechnical_OnClick(object sender, EventArgs e)
    //{

    //    String VesselEmail = "";
    //    if (lblMsgCommentsTechnicalUpdateByOn.Text.Trim() == "")
    //    {
    //        lblMsgCommentsTechnical.Text = "Please enter remarks and verify to send mail.";
    //        return;
    //    }
    //    if (txtTechnicalRemarks.Text.Trim() == "")
    //    {
    //        lblMsgCommentsTechnical.Text = "Please enter remarks and verify to send mail.";
    //        return;
    //    }
    //    DataTable dt = Common.Execute_Procedures_Select_ByQuery(" select email from Vessel where VesselCode='" + CurrentVessel + "' ");
    //    if (dt.Rows.Count > 0)
    //    {
    //        VesselEmail = dt.Rows[0][0].ToString();

    //    }


    //    string FromAddress = ConfigurationManager.AppSettings["FromAddress"];
    //    string ReplyToAddress = ConfigurationManager.AppSettings["FromAddress"];
    //    string[] ToAddress = { VesselEmail };
    //    string[] CCAddress = { ProjectCommon.getUserEmailByID(Session["loginid"].ToString()) };
    //    string[] BCCAddress = { "" };
    //    string Subject = "Office Remark[ " + lblReportName.Text.Trim() + " ( ID : " + lblReportID.Text.Trim() + " )]";
    //    string Message = MailMessage(txtTechnicalRemarks.Text.Trim(), Session["UserName"].ToString());
    //    string Error;
    //    string AttachmentFilePath = "";

    //    if (SendMail.SendeMail(FromAddress, ReplyToAddress, ToAddress, CCAddress, BCCAddress, Subject, Message, out Error, AttachmentFilePath))
    //    {
    //        lblMsgCommentsMarine.Text = "Mail send successfully.";
    //    }
    //    else
    //    {
    //        lblMsgCommentsMarine.Text = "Error while sending mail.";
    //    }
    //}
    public string MailMessage(string Message,string UserName)
    {
        StringBuilder msg = new StringBuilder();

        msg.Append(" <div style = 'font-weight:bold;color:#1f497d;margin-bottom:15px;'> ");
        msg.Append(" Dear Captain, ");
        msg.Append(" </div > ");
        msg.Append(" <div style = 'color:#1f497d;margin-bottom:15px;' > ");
        msg.Append(" You are requested to correct the subject position report as per below office remark. Ensure to export  report to office. ");
        msg.Append(" </div > ");

        msg.Append(" <div style = 'font-weight:bold;color:#1f497d;margin-bottom:0px;' > ");
        msg.Append(" Office Remark: ");
        msg.Append(" </div > ");

        msg.Append(" <div style = 'color:c2c2c2;font-weight:500;margin-bottom:35px;' > <i> ");
        msg.Append(Message );        
        msg.Append(" </i></div > ");

        

        msg.Append(" <div style = 'color:c2c2c2;font-weight:600;margin-bottom:15px;' > ");
        msg.Append(" Thanks,</br> ");
        msg.Append(UserName);
        msg.Append(" </div > ");

        return msg.ToString();
    }

}
