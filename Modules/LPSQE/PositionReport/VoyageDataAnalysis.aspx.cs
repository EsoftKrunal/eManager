using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.IO;
using System.Collections.Generic;

public partial class VPR_VoyageDataAnalysis : System.Web.UI.Page
{


    //    string[,] TableColumns
    //    ={
    //{"[ReportsPK]","[Reports PK]","F_COND"},
    //{"[VoyageNo]","[VoyageNo]","S"},

    //{"SNQ.dbo.GET_FORMATTED_DATE([COSPDATE])","[Departure Date]","D"},
    //{"SNQ.dbo.GET_FORMATTED_DATE([EOSPDATE])","[Arrival Date]","D"},
    //{"SNQ.dbo.GET_FORMATTED_DATE([ALLFASTDATE])","[Berthing Date]","D"},

    //{"[CharterPartySpeed]","[CharterPartySpeed]","N"},
    //{"[VoyOrderSpeed]","[VoyOrderSpeed]","N"},
    //{"[SteamingTime]","[SteamingTime]","N"},
    //{"[DistanceMadeGood]","[DistanceMadeGood]","N"},
    //{"[AvgSpeed]","[AvgSpeed]","N"},
    //{"(case when Stoppages=1 then 'Yes' when stoppages=2 then 'No' else '' end)","[Stoppages]","B"},
    //{"[DepPort]","[DepPort]","S"},
    //{"[DepArrivalPort]","[ArrivalPort]","S"},

    //{"[DraftFwd]","[DraftFwd]","N"},
    //{"[DraftAft]","[DraftAft]","N"},
    //{"[MeanDraft]","[MeanDraft]","N"},
    //{"[WindForce]","[WindForce]","N"},
    //{"[SeaState]","[SeaState]","N"},

    //{"[CourceT]","[CourceT]","N"},
    //{"[WindDirectionT]","[WindDirectionT]","N"},
    //{"[SeaDirection]","[SeaDirection]","N"},
    //{"[CurrentDirection]","[CurrentDirection]","N"},
    //{"[CurrentStrength]","[CurrentStrength]","N"},

    //{"[MEIFO45]","[MECons IFO3.5]","N"},
    ////{"[MEIFO1]","[MECons IFO1.0]","N"},
    //{"[MEMGO5]","[MECons MGO0.1]","N"},
    ////{"[MEMGO1]","[MECons MGO1.0]","N"},
    //{"[MEMDO]","[MECons MDO]","N"},

    //{"[AEIFO45]","[AECons IFO3.5]","N"},
    ////{"[AEIFO1]","[AECons IFO1.0]","N"},
    //{"[AEMGO5]","[AECons MGO0.1]","N"},
    ////{"[AEMGO1]","[AECons MGO1.0]","N"},
    //{"[AEMDO]","[AECons MDO]","N"},

    //{"[CargoHeatingIFO45]","[BoilerCons IFO3.5]","N"},
    ////{"[CargoHeatingIFO1]","[BoilerCons IFO1.0]","N"},
    //{"[CargoHeatingMGO5]","[BoilerCons MGO0.1]","N"},
    ////{"[CargoHeatingMGO1]","[BoilerCons MGO1.0]","N"},
    //{"[CargoHeatingMDO]","[BoilerCons MDO]","N"},

    //{"[MECCLube]","[LubeCons MECC]","N"},
    //{"[MECYLLube]","[LubeCons MECYL]","N"},
    //{"[AECCLube]","[LubeCons AECC]","N"},
    //{"[HYDLube]","[LubeCons HYD]","N"},

    //{"[GeneratedFreshWater]","[GeneratedFreshWater]","N"},
    //{"[ConsumedFreshWater]","[ConsumedFreshWater]","N"},

    //{"[MEIFO45_24]","[MECons IFO3.5 (/24 Hrs)]","N"},
    ////{"[MEIFO1_24]","[MECons IFO1.0 (/24 Hrs)]","N"},
    //{"[MEMGO5_24]","[MECons MGO0.1 (/24 Hrs)]","N"},
    ////{"[MEMGO1_24]","[MECons MGO1.0 (/24 Hrs)]","N"},
    //{"[MEMDO_24]","[MECons MDO (/24 Hrs)]","N"},

    //{"[AEIFO45_24]","[AECons IFO3.5 (/24 Hrs)]","N"},
    ////{"[AEIFO1_24]","[AECons IFO1.0 (/24 Hrs)]","N"},
    //{"[AEMGO5_24]","[AECons MGO0.1 (/24 Hrs)]","N"},
    ////{"[AEMGO1_24]","[AECons MGO1.0 (/24 Hrs)]","N"},
    //{"[AEMDO_24]","[AECons MDO (/24 Hrs)]","N"},

    //{"[CargoHeatingIFO45_24]","[BoilerCons IFO3.5 (/24 Hrs)]","N"},
    ////{"[CargoHeatingIFO1_24]","[BoilerCons IFO1.0 (/24 Hrs)]","N"},
    //{"[CargoHeatingMGO5_24]","[BoilerCons MGO0.1 (/24 Hrs)]","N"},
    ////{"[CargoHeatingMGO1_24]","[BoilerCons MGO1.0 (/24 Hrs)]","N"},
    //{"[CargoHeatingMDO_24]","[BoilerCons MDO (/24 Hrs)]","N"},

    //{"[MECCLube_24]","[LubeCons MECC (/24 Hrs)]","N"},
    //{"[MECYLLube_24]","[LubeCons MECYL (/24 Hrs)]","N"},
    //{"[AECCLube_24]","[LubeCons AECC (/24 Hrs)]","N"},
    //{"[HYDLube_24]","[LubeCons HYD (/24 Hrs)]","N"},

    //{"[MERPM]","[MERPM]","N"},
    //{"[EngineDistance]","[EngineDistance]","N"},
    //{"[Slip]","[Slip(%)]","N"},
    //{"[MEOutPut]","[MEOutPut(% MCR)]","N"},
    //{"[METhermalLoad]","[METhermalLoad(%)]","N"},

    //{"[MELoadIndicator]","[MELoadIndicator]","N"},
    //{"[METCNo1RPM]","[METCNo1RPM]","N"},
    //{"[METCNo2RPM]","[METCNo2RPM]","N"},
    //{"[MESCAVPressure]","[MESCAVPressure(BAR)]","N"},
    //{"[SCAVTEMP]","[SCAVTEMP]","N"},
    //{"[LubeOilPressure]","[LubeOilPressure(BAR)]","N"},
    //{"[SeaWaterTemp]","[SeaWaterTemp]","N"},
    //{"[EngRoomTemp]","[EngRoomTemp]","N"},
    //{"[BligPump]","[BligPump]","N"},
    //{"[AEFOInletTemp]","[AEFOInletTemp]","N"},

    //{"[AUX1Load]","[AUX1Load]","N"},
    //{"[AUX2Load]","[AUX2Load]","N"},
    //{"[AUX3Load]","[AUX3Load]","N"},
    //{"[AUX4Load]","[AUX4Load]","N"},


    //{"[AENo1]","[Usage-AENo1(HRS)]","N"},
    //{"[AENo2]","[Usage-AENo2(HRS)]","N"},
    //{"[AENo3]","[Usage-AENo3(HRS)]","N"},
    //{"[AENo4]","[Usage-AENo4(HRS)]","N"},

    //{"[TotalCargoWeight]","[TotalCargoWeight]","N"},
    //{"[BallastWeight]","[BallastWeight]","N"},
    //{"[TCU20L]","[Laden-TEU]","N"},
    //{"[TCU40L]","[Laden-FEU]","N"},
    //{"[TCUL]","[Total Laden-TEU]","N"},
    //{"[TCU20E]","[Emply-TEU]","N"},
    //{"[TCU40E]","[Empty-FEU]","N"},
    //{"[TCUE]","[Total Empty-TEU]","N"}
    //};


    string[,] TableColumns
    ={
    //{"[ReportsPK]","[ReportsPK]","F_COND"},
    //{"[NoonID]","[NoonID]","N"},
    //{"[VesselID]","[VesselID]","S"},    
    {"(case when Location=1 then 'At Sea' when Location=2 then 'In Port' else '' end)","[Location]","S"},    
    //{"(case when ACTIVITY_CODE='D' then 'Departure COSP' when ACTIVITY_CODE='N' then 'Noon at Sea'  when ACTIVITY_CODE='A' then 'Arrival' when ACTIVITY_CODE='PA' then 'Noon at Anchor' when ACTIVITY_CODE='PB' then 'Noon at Berth' when ACTIVITY_CODE='PD' then 'Noon at Drift'  when ACTIVITY_CODE='SH' then 'Departure Berth' else '' end)","[Report Type]","N"},
    {"[VoyageNo]","[VoyageNo]","S"},
    //{"[RestrictedArea]","[RestrictedArea]","N"},
    //{"[AreaName]","[AreaName]","N"},
    //{"[ETARestrictedArea]","[ETARestrictedArea]","D"},
    //{"[ChartererName]","[ChartererName]","S"},
    {"[CharterPartySpeed]","[CharterPartySpeed]","N"},
    {"[VoyOrderSpeed]","[VoyOrderSpeed]","N"},
    //{"[VoyInstructions]","[VoyInstructions]","S"},
    {"[SteamingHrs]","[SteamingHrs]","N"},
    {"[SteamingMin]","[SteamingMin]","N"},
    {"[DistanceMadeGood]","[DistanceMadeGood]","N"},
    {"[AvgSpeed]","[AvgSpeed]","N"},
    {"(case when Stoppages=1 then 'Yes' when stoppages=2 then 'No' else '' end)","[Stoppages]","B"},
    {"[StoppagesHH]","[StoppagesHH]","N"},
    {"[StoppagesMM]","[StoppagesMM]","N"},
    {"[Displacement]","[Displacement]","N"},
    //{"[Remarks]","[Remarks]","S"},
    {"(case when VoyCondition=1 then 'Laden' when VoyCondition=2 then 'Ballast' else '' end)","[VoyCondition]","S"},
    {"[DepPort]","[DepPort]","S"},
    {"[DepArrivalPort]","[ArrivalPort]","S"},
    {"[COSPDate]","[COSPDate]","D"},
    //{"[COSPHrs]","[COSPHrs]","N"},
    //{"[COSPMin]","[COSPMin]","N"},
    //{"[TimeZone]","[TimeZone]","S"},
    //{"[ArrivalPortETA]","[ArrivalPortETA]","D"},
    //{"[ArrivalPortETAHrs]","[ArrivalPortETAHrs]","N"},
    //{"[ArrivalPortETAMin]","[ArrivalPortETAMin]","N"},
    {"[DraftFwd]","[DraftFwd]","N"},
    {"[DraftAft]","[DraftAft]","N"},
    //{"[DistanceToGo]","[DistanceToGo]","N"},
    //{"[ArrivalPortAgent]","[ArrivalPortAgent]","S"},
    //{"[PersonalIncharge]","[PersonalIncharge]","S"},
    //{"[AddressContactDetails]","[AddressContactDetails]","S"},
    //{"[Port1]","[Port1]","S"},
    //{"[PortETA1]","[PortETA1]","D"},
    //{"[Port2]","[Port2]","S"},
    //{"[PortETA2]","[PortETA2]","D"},
    //{"[Port3]","[Port3]","S"},
    //{"[PortETA3]","[PortETA3]","D"},
    //{"[CourceT]","[CourceT]","N"},
    //{"[WindDirectionT]","[WindDirectionT]","N"},
    {"[WindForce]","[WindForce]","N"},
    //{"[SeaDirection]","[SeaDirection]","N"},
    {"[SeaState]","[SeaState]","N"},
    //{"[CurrentDirection]","[CurrentDirection]","N"},
    //{"[CurrentStrength]","[CurrentStrength]","N"},
    //{"[WeatherRemarks]","[WeatherRemarks]","S"},
    //{"[Lattitude1]","[Lattitude1]","N"},
    //{"[Lattitude2]","[Lattitude2]","N"},
    //{"[Lattitude3]","[Lattitude3]","N"},
    //{"[Longitude1]","[Longitude1]","N"},
    //{"[Longitud2]","[Longitud2]","N"},
    //{"[Longitud3]","[Longitud3]","N"},
    //{"[LocationDescription]","[LocationDescription]","S"},


    // REC
	
    {"[RECIFO45]","[REC IFO 3.5]","N"},
    {"[RECMGO5]","[REC MGO]","N"},
    {"[RECMDO]","[RECMDO]","N"},

    //{"[RECIFO1]","[RECIFO1]","N"},
    //{"[RECMGO1]","[RECMGO1]","N"},
 
    // ME CONS

    {"[MEIFO45]","[MECons IFO3.5]","N"},
    {"[MEMGO5]","[MECons MGO]","N"},
    {"[MEMDO]","[MECons MDO]","N"},

    // AE CONS

    {"[AEIFO45]","[AECons IFO3.5]","N"},
    {"[AEMGO5]","[AECons MGO0.1]","N"},
    {"[AEMDO]","[AECons MDO]","N"},

	// BOILER
    
    {"[CargoHeatingIFO45]","[BoilerCons IFO3.5]","N"},
    {"[CargoHeatingMGO5]","[BoilerCons MGO0.1]","N"},
    {"[CargoHeatingMDO]","[BoilerCons MDO]","N"},

  // ROB

    {"[ROBIFO45Fuel]","[ROB IFO3.5]","N"},
    {"[ROBMGO1Fuel]","[ROB MGO]","N"},
    {"[ROBMDOFuel]","[ROB MDO]","N"},


	// REC
 
    {"[RECMECC]","[REC MECC]","N"},
    {"[RECMECYL]","[REC MECYL LOWBN]","N"},
    {"[RECMECYL_HighBN]","[RECMECYL HighBN]","N"},
    {"[RECAECC]","[REC AECC]","N"},
    {"[RECHYD]","[REC HYD]","N"},


  	// CONS

    {"[MECCLube]","[CONS MECC]","N"},
    {"[MECYLLube]","[CONS MECYL LOWBN]","N"},
    {"[MECYLLube_HighBN]","[MECYLLube HighBN]","N"},
    {"[AECCLube]","[CONS AECC]","N"},
    {"[HYDLube]","[CONS_HYD]","N"},

	// ROB
	
    {"[ROBIFO45Lube]","[ROB MECC]","N"},
    {"[ROBIFO1Lube]","[ROB MECYL LOWBN]","N"},
    {"[ROBIFO1Lube_HighBN]","[ROBIFO1Lube HighBN]","N"},
    {"[ROBMGO5Lube]","[ROB AECC]","N"},
    {"[ROBMGO1Lube]","[ROB HYD]","N"},


    
    


// FW

    {"[RECFRESHWATER]","[REC FW]","N"},
    {"[GeneratedFreshWater]","[GeneratedFreshWater]","N"},
    {"[ConsumedFreshWater]","[ConsumedFreshWater]","N"},
    {"[ROBIFO45FreshWater]","[ROB FW]","N"},

   // {"[ROBIFO1Fuel]","[ROBIFO1Fuel]","N"},
//    {"[ROBMDOLube]","[ROBMDOLube]","N"},
 
 //   {"[MEIFO1]","[MEIFO1]","N"},
//    {"[MEMGO5]","[MECons MGO0.1]","N"},
//    {"[AEIFO1]","[AEIFO1]","N"},

    

//    {"[CargoHeatingMGO1]","[CargoHeatingMGO1]","N"},
  //  {"[CargoHeatingIFO1]","[CargoHeatingIFO1]","N"},

	//{"[AEMGO1]","[AEMGO1]","N"},
    

    //{"[TankCleaningIFO45]","[TankCleaningIFO45]","N"},
    //{"[TankCleaningIFO1]","[TankCleaningIFO1]","N"},
    //{"[TankCleaningMGO5]","[TankCleaningMGO5]","N"},
    //{"[TankCleaningMGO1]","[TankCleaningMGO1]","N"},
    //{"[TankCleaningMDO]","[TankCleaningMDO]","N"},
    //{"[GasFreeingIFO45]","[GasFreeingIFO45]","N"},
    //{"[GasFreeingIFO1]","[GasFreeingIFO1]","N"},
    //{"[GasFreeingMGO5]","[GasFreeingMGO5]","N"},
    //{"[GasFreeingMGO1]","[GasFreeingMGO1]","N"},
    //{"[GasFreeingMDO]","[GasFreeingMDO]","N"},
    //{"[IGSIFO45]","[IGSIFO45]","N"},
    //{"[IGSIFO1]","[IGSIFO1]","N"},
    //{"[IGSMGO5]","[IGSMGO5]","N"},
    //{"[IGSMGO1]","[IGSMGO1]","N"},
    //{"[IGSMDO]","[IGSMDO]","N"},
    //{"[Total_IFO45]","[Total_IFO45]","N"},
    //{"[Total_IFO1]","[Total_IFO1]","N"},
    //{"[Total_MGO5]","[Total_MGO5]","N"},
    //{"[Total_MGO1]","[Total_MGO1]","N"},
    //{"[Total_MDO]","[Total_MDO]","N"},
    //{"[MECCLube]","[LubeCons MECC]","N"},
    //{"[MECYLLube]","[LubeCons MECYL]","N"},
    //{"[AECCLube]","[LubeCons AECC]","N"},
    //{"[HYDLube]","[LubeCons HYD]","N"},
    //{"[MDOLube]","[MDOLube]","N"},

   
    {"[ExhTempMin]","[ExhTempMin]","N"},
    {"[ExhTempMax]","[ExhTempMax]","N"},
    {"[MERPM]","[MERPM]","N"},
    {"[EngineDistance]","[EngineDistance]","N"},
    {"[Slip]","[Slip(%)]","N"},
    {"[MEOutput]","[MEOutput]","N"},
    {"[METhermalLoad]","[METhermalLoad(%)]","N"},
    {"[MELoadIndicator]","[MELoadIndicator]","N"},
    {"[METCNo1RPM]","[METCNo1RPM]","N"},
    {"[METCNo2RPM]","[METCNo2RPM]","N"},
    {"[MESCAVPressure]","[MESCAVPressure(BAR)]","N"},
    {"[SCAVTEMP]","[SCAVTEMP]","N"},
    {"[LubeOilPressure]","[LubeOilPressure(BAR)]","N"},
    {"[SeaWaterTemp]","[SeaWaterTemp]","N"},
    {"[EngRoomTemp]","[EngRoomTemp]","N"},
    {"[BligPump]","[BligPump]","N"},
    {"[AUX1Load]","[AUX1Load]","N"},
    {"[AUX2Load]","[AUX2Load]","N"},
    {"[AUX3Load]","[AUX3Load]","N"},
    {"[AUX4Load]","[AUX4Load]","N"},
    {"[TotalAUXLoad]","[TotalAUXLoad]","N"},
    {"[AENo1]","[Usage-AENo1(HRS)]","N"},
    {"[AENo2]","[Usage-AENo2(HRS)]","N"},
    {"[AENo3]","[Usage-AENo3(HRS)]","N"},
    {"[AENo4]","[Usage-AENo4(HRS)]","N"},
    {"[AEFOInletTemp]","[AEFOInletTemp]","N"},
    {"[TotalCargoWeight]","[TotalCargoWeight]","N"},
    {"[BallastWeight]","[BallastWeight]","N"},
 //   {"[ACTIVITY_CODE]","[ACTIVITY_CODE]","S"},

   

    //{"[BerthTerminalName]","[BerthTerminalName]","S"},
    //{"[PlaceOfBerth]","[PlaceOfBerth]","S"},
    //{"[FirstLineDate]","[FirstLineDate]","D"},
    //{"[FirstLineHH]","[FirstLineHH]","N"},
    //{"[FirstLineMM]","[FirstLineMM]","N"},
    //{"[AllFastDate]","[AllFastDate]","D"},
    //{"[AllFastHH]","[AllFastHH]","N"},
    //{"[AllFastMM]","[AllFastMM]","N"},
    //{"[NoOfTUGSUSED]","[NoOfTUGSUSED]","S"},
    //{"[ETBDT]","[ETBDT]","D"},
    //{"[ETBHours]","[ETBHours]","N"},
    //{"[ETBMin]","[ETBMin]","N"},
    //{"[ETDDT]","[ETDDT]","D"},
    //{"[ETDHours]","[ETDHours]","N"},
    //{"[ETDMin]","[ETDMin]","N"},
    //{"[POB]","[POB]","S"},
    //{"[LetGoAnchorage]","[LetGoAnchorage]","D"},
    //{"[LetGoAnchorageHH]","[LetGoAnchorageHH]","N"},
    //{"[LetGoAnchorageMM]","[LetGoAnchorageMM]","N"},
    //{"[pilotAwayHH]","[pilotAwayHH]","N"},
    //{"[pilotAwayMM]","[pilotAwayMM]","N"},
    //{"[AnchorageReason]","[AnchorageReason]","S"},
    //{"[ComDDDate]","[ComDDDate]","D"},
    //{"[DriftHour]","[DriftHour]","N"},
    //{"[DriftMin]","[DriftMin]","N"},
    //{"[DriftReason]","[DriftReason]","S"},
    {"[EOSPDate]","[EOSPDate]","D"},
    //{"[EOSPHrs]","[EOSPHrs]","N"},
    //{"[EOSPMin]","[EOSPMin]","N"},
    {"[Shaft_Gen_Hrs]","[Shaft_Gen_Hrs]","N"},
    {"[Shaft_Gen_Mins]","[Shaft_Gen_Mins]","N"},
    {"[Tank_Cleaning_Hrs]","[Tank_Cleaning_Hrs]","N"},
    {"[Tank_Cleaning_Mins]","[Tank_Cleaning_Mins]","N"},
    {"[Cargo_Heating_Hrs]","[Cargo_Heating_Hrs]","N"},
    {"[Cargo_Heating_Mins]","[Cargo_Heating_Mins]","N"},
    {"[Inert_Hrs]","[Inert_Hrs]","N"},
    {"[Inert_Mins]","[Inert_Mins]","N"},
    //{"[MarineVerified]","[MarineVerified]","?????"},
    //{"[MarineVerifiedBy]","[MarineVerifiedBy]","S"},
    //{"[MarineVerifiedOn]","[MarineVerifiedOn]","D"},
    //{"[TechVerified]","[TechVerified]","?????"},
    //{"[TechVerifiedBy]","[TechVerifiedBy]","S"},
    //{"[TechVerifiedOn]","[TechVerifiedOn]","D"},
    //{"[MarineComments]","[MarineComments]","S"},
    //{"[TechnicalComments]","[TechnicalComments]","S"},
    {"[CharterPartyCons]","[CharterPartyCons]","N"},
    {"[TotalDisplacement]","[TotalDisplacement]","N"},
   
    {"[SinceLastReport]","[SinceLastReport]","N"},
    {"[Laden20Ft]","[Laden20Ft]","N"},
    {"[Laden40Ft]","[Laden40Ft]","N"},
    {"[Laden45Ft]","[Laden45Ft]","N"},
    {"[Empty20Ft]","[Empty20Ft]","N"},
    {"[Empty40Ft]","[Empty40Ft]","N"},
    {"[Empty45Ft]","[Empty45Ft]","N"},
    {"[AuxTempMin1]","[AuxTempMin1]","N"},
    {"[AuxTempMin2]","[AuxTempMin2]","N"},
    {"[AuxTempMin3]","[AuxTempMin3]","N"},
    {"[AuxTempMin4]","[AuxTempMin4]","N"},
    {"[AuxTempMax1]","[AuxTempMax1]","N"},
    {"[AuxTempMax2]","[AuxTempMax2]","N"},
    {"[AuxTempMax3]","[AuxTempMax3]","N"},
    {"[AuxTempMax4]","[AuxTempMax4]","N"},


    //{"[ConsumptionRemark]","[ConsumptionRemark]","S"}
    {"[REFRLaden20Ft]","[REFRLaden20Ft]","N"},
    {"[REFRLaden40Ft]","[REFRLaden40Ft]","N"},
    {"[QTYBallasted]","[QTYBallasted]","N"},
    {"[QTYDeBallasted]","[QTYDeBallasted]","N"},
    //{"[BWTS]","[BWTS]","N"},

    {"(case when BWTS='Y' then 'Yes' when BWTS='N' then 'No' WHEN BWTS='A' then 'NA'  else '' end)","[BWTS]","S"},

    {"[BWTSHrs]","[BWTSHrs]","N"},
    
    {"(([DraftFwd]+[DraftAft])/2)" , "[MidDraft]","N"},
        //----- 
    {"[BILGEWaterTankROB]","[BILGEWaterTankROB]","N"},
    {"[OILYBILGETankROB]","[OILYBILGETankROB]","N"},
    {"[SLUDGETANKROB]","[SLUDGETANKROB]","N"},
    {"[GARBAGE_ROB]","[GARBAGE_ROB]","N"},

    //-24-08-2017------------------------
    {"[AllFastDate]","[AllFastDate]","D"},
    {"[ArrivalPortAgent]","[ArrivalPortAgent]","S"},
    {"[BilgeWaterLanded]","[BilgeWaterLanded]","N"},
    {"[ChartererName]","[ChartererName]","S"},
    {"[CourceT]","[CourceT]","N"},
    {"[CurrentDirection]","[CurrentDirection]","N"},
    {"[CurrentStrength]","[CurrentStrength]","N"},
    {"[DistanceToGo]","[DistanceToGo]","N"},
    {"[Lattitude1]","[Lattitude1]","N"},
    {"[Lattitude2]","[Lattitude2]","N"},

    
    {"(case when Lattitude3=1 then 'N' when Lattitude3=2 then 'S' else '' end)","[Lattitude3]","N"},
    //{"[Lattitude3]","[Lattitude3]","N"},
    {"[Longitud2]","[Longitud2]","N"},

    {"(case when Longitud3=1 then 'N' when Longitud3=2 then 'S' else '' end)","[Longitud3]","N"},
    //{"[Longitud3]","[Longitud3]","N"},
    {"[Longitude1]","[Longitude1]","N"},
    {"[OilyBilgWaterLanded]","[OilyBilgWaterLanded]","N"},
    {"[SeaDirection]","[SeaDirection]","N"},

    {"(case when SeasonalLoadLines='Y' then 'Yes' when SeasonalLoadLines='N' then 'No' else '' end)","[SeasonalLoadLines]","N"},
    //{"[SeasonalLoadLines]","[SeasonalLoadLines]","S"},
    {"[Sludgelanded]","[Sludgelanded]","N"},
    {"[SootLanded]","[SootLanded]","N"},
    {"[WindDirectionT]","[WindDirectionT]","N"}
    };

    protected void Page_PreRender(object sender, EventArgs e)
{
    ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvscroll_A');SetLastFocus('dvscroll_S');SetLastFocus('dvscroll_D');", true);
}
    public DataTable SelectedColumns
    {
        get { return (DataTable)Session["SelectedColumns"]; }
        set { Session["SelectedColumns"] = value; }
    }
    public DataTable Filters
    {
        get { return (DataTable)Session["Filters"]; }
        set { Session["Filters"] = value; }
    }
    public int MaxFilterRows
    {
        get {return Common.CastAsInt32(Session["MaxFilterRows"]); }
        set {Session["MaxFilterRows"]=value; }
    }
    public string VesselId
    {
        get { return ViewState["VesselId"].ToString(); }
        set { ViewState["VesselId"] = value; }
    }
    public void ResetFilters()
    {
        object[] Arr = { "", "", "", "", "", "", "" };

        DataTable dtFilters = InintalizeFilterTable();
        int i = 0;
        for (i = 0; i < TableColumns.GetLength(0) - 1; i++)
        {
            if (dtFilters.Rows.Find(TableColumns[i, 0]) == null)
            {
                if (i >= 1)
                {
                    Arr[0] = TableColumns[i, 0]; // PK ( Column Code)
                    Arr[1] = TableColumns[i, 1].Replace("[", "").Replace("]", ""); // PK ( Column Name)
                    Arr[2] = TableColumns[i, 2]; // DataType
                    Arr[3] = ""; // Operator
                    Arr[4] = ""; // DDl Value
                    Arr[5] = ""; // Text Value 1
                    Arr[6] = ""; // Text Value 2

                    dtFilters.Rows.Add(Arr);
                }
            }

        }

        Filters = dtFilters;
        BindFiltersRepeater();
    }
   
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (Session["UserName"] == null)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr1", "alert('Sesion Expired. Please login again.');", true);
            return;
        }

        if (!IsPostBack)
        {
            BindFiltersRepeater();
            BindAvlColumns();
            BindSelColumns();
            ResetFilters();
            
            //string FieldName = ddlFilterFields.SelectedValue;
            //string DataType = getColumnData(ddlFilterFields.SelectedValue, 2);

            //LoadOpeartors(DataType);
            //LoadOperands(FieldName, DataType);
        }
        VesselId = Page.Request.QueryString["CurrentShip"].ToString().Trim(); 
    }

    
    
    protected void btnVDA_OnClick(object sender, EventArgs e)
    {
        //Response.Redirect("VPR_ReportWriter1.aspx");
    }
    protected void btnEEOI_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("VPR_EEOI.aspx?" + Request.QueryString);
    }
    protected DataTable InintalizeTable()
    {
        DataColumn PK = new DataColumn("ColumnCode");
        DataColumn[] PKArray = { PK };
        DataTable dtAvlColumns = new DataTable();
        dtAvlColumns.Columns.Add(PK);
        dtAvlColumns.Columns.Add(new DataColumn("ColumnName"));
        dtAvlColumns.PrimaryKey = PKArray;
        return dtAvlColumns;  
    }
    protected DataTable InintalizeFilterTable()
    {
        DataColumn PK = new DataColumn("PK");
        DataColumn[] PKArray = { PK };
        DataTable dtAvlColumns = new DataTable();
        dtAvlColumns.Columns.Add(PK);
        dtAvlColumns.Columns.Add(new DataColumn("ColumnName"));
        dtAvlColumns.Columns.Add(new DataColumn("DataType"));
        dtAvlColumns.Columns.Add(new DataColumn("Opearator"));
        dtAvlColumns.Columns.Add(new DataColumn("DDLValue"));
        dtAvlColumns.Columns.Add(new DataColumn("TEXTValues1"));
        dtAvlColumns.Columns.Add(new DataColumn("TEXTValues2"));
        dtAvlColumns.PrimaryKey = PKArray;
        return dtAvlColumns;
    }
    private void BindAvlColumns()
    {
        DataTable dtSelectedCols = SelectedColumns;
        if (dtSelectedCols == null)
            dtSelectedCols = InintalizeTable(); 

        DataTable dtAvlColumns = InintalizeTable();
        object[] Arr={"VoyageNo","VoyageNo"};

        int i = 0;
        for (i = 0; i <= TableColumns.GetLength(0) - 1; i++)
        {
            if (dtSelectedCols.Rows.Find(TableColumns[i, 0]) == null)
            {
                Arr[0] = TableColumns[i, 0];
                Arr[1] = TableColumns[i, 1].Replace("[", "").Replace("]", "");
                dtAvlColumns.Rows.Add(Arr);
            }

        }

        chkAvlColumns.DataSource = dtAvlColumns;
        chkAvlColumns.DataTextField = "ColumnName";
        chkAvlColumns.DataValueField = "ColumnCode";
        chkAvlColumns.DataBind(); 
    }
    private void BindSelColumns()
    {
        chkSelColumns.DataSource = SelectedColumns;
        chkSelColumns.DataTextField = "ColumnName";
        chkSelColumns.DataValueField = "ColumnCode";
        chkSelColumns.DataBind(); 
    }
    //public void ExportDatatable(DataTable dt)
    //{
    //    Response.Clear();
    //    Response.AddHeader("content-disposition", "attachment;filename=" + "Vessel_Performance" + ".xls");
    //    Response.ContentType = "application/vnd.xls";
    //}
    public void ExportDatatable(DataTable dt, String FileName)
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".xls");
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        System.Web.UI.WebControls.DataGrid dg = new System.Web.UI.WebControls.DataGrid();
        dg.DataSource = dt;
        dg.DataBind();
        dg.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        //lblSQL.Text = ""; 
        ////ddl_F_VoyCond.SelectedIndex = 0;
        ////txtFromDate.Text = "";
        ////txtToDate.Text = ""; 
        //Filters = null;
        //BindFiltersRepeater();
        lblSQL.Visible = false;  
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(lblSQL.Text.ToString().Replace("ReportId,ReportTypeCode,",""));
        ExportDatatable(dt,"VoyageDataAnalysis"); 
        
    }
    protected void btnGB_Click(object sender, EventArgs e)
    {
        dvGrid.Visible = false;
        dvFilter.Visible = true;
        trVessel.Visible = true;
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        lblSQL.Text = ""; 
        //ddl_F_VoyCond.SelectedIndex = 0;
        //txtFromDate.Text = "";
        //txtToDate.Text = ""; 
        ResetFilters();
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        VesselId = Page.Request.QueryString["CurrentShip"].ToString(); 
        StringBuilder SQL=new StringBuilder();
        //SQL.Append(" SELECT ReportId,ReportTypeCode,ReportType,LEFT(DATENAME(M,ReportDate),3) AS ReportMonth, Replace(Convert(varchar,ReportDate,106),' ','-') as ReportDate");
        SQL.Append(" SELECT VesselID, Replace(Convert(varchar,ReportDate,106),' ','-') as ReportDate,Activity_Name as [Report Type]");
        //ReportsPK,NoonID,VesselID, Replace(Convert(varchar, ReportDate, 106), ' ', '-') as ReportDate

        foreach (ListItem li in chkSelColumns.Items)
        {
            SQL.Append("," + li.Value + " AS " + "[" + li.Text + "]") ;
        }

        SQL.Append(" FROM VW_VPR_ALLREPORTS_1_new ");
        string WhereClause = "WHERE VESSELID='" + VesselId + "' ";

        if(ddl_F_VoyCond.SelectedIndex >0)
        {
            WhereClause += " AND " + ddl_F_VoyCond.SelectedValue;
        }
        if(ddlLoc.SelectedIndex >0)
        {
            WhereClause +=" AND " + ddlLoc.SelectedValue;
        }
        if( txtFromDate.Text.Trim()!="")
            WhereClause +="AND ReportDate>='" + txtFromDate.Text.Trim() + "'";

        if (txtToDate.Text.Trim() != "")
            WhereClause +="AND ReportDate<='" + txtToDate.Text.Trim() + "'";
        
        if(ddlReprotType.SelectedIndex!=0)
            WhereClause += "AND Activity_Code='" + ddlReprotType .SelectedValue+ "'";

        foreach (RepeaterItem ri in rptFilter.Items)
        {
            DropDownList ddlO = ((DropDownList)ri.FindControl("ddlFilterOperators"));
            if (ddlO.SelectedIndex > 0)
            {
                HiddenField hfdFieldCode = (HiddenField)ri.FindControl("hfdPK");
                HiddenField hfdDataType = (HiddenField)ri.FindControl("hfdDataType");

                DropDownList ddlV = (DropDownList)ri.FindControl("ddlValue1");
                TextBox txtT1 = (TextBox)ri.FindControl("txtValue1");
                TextBox txtT2 = (TextBox)ri.FindControl("txtValue2");
                    
                WhereClause += " AND " + getSQL(hfdFieldCode.Value, ddlO, hfdDataType.Value, ddlV, txtT1, txtT2);
            }
        }
        

        SQL.Append(WhereClause);
        SQL.Append( " Order by convert(smalldatetime,ReportDate)" );
        lblSQL.Text = SQL.ToString();
        DataTable dt=new DataTable(); 
        try
        {
            dt = Common.Execute_Procedures_Select_ByQuery(SQL.ToString());
            lblSQL.ForeColor = System.Drawing.Color.Green;
        }
        catch
        {
            lblSQL.ForeColor = System.Drawing.Color.Red;
        }

//lblSQL.Text=SQL.ToString();
//Response.End();

        StringBuilder sbHeader=new StringBuilder();
        StringBuilder sbDetails = new StringBuilder();

        sbHeader.Append("<table style='width:" + Convert.ToString(dt.Columns.Count * 160) + "px; background-color:#e2e2e2;font-size:13px; border-collapse:collapse;' cellspacing='0' cellpadding='2' border='1'><tr>");
        sbDetails.Append("<table style='width:" + Convert.ToString(dt.Columns.Count * 160) + "px; border-collapse:collapse;' cellspacing='0' cellpadding='2' border='1'><tr>");
        foreach (DataColumn dc in dt.Columns)
        {
            if (dc.ColumnName.Trim() != "ReportId" && dc.ColumnName.Trim() != "ReportTypeCode")
            {
                sbHeader.Append("<td style='width:150px;'><b>" + dc.ColumnName + "</b></td>");
            }
        }
        foreach (DataRow dr in dt.Rows)
        {
            sbDetails.Append("<tr>");
            foreach (DataColumn dc in dt.Columns)
            {
                if (dc.ColumnName.Trim() != "ReportId" && dc.ColumnName.Trim() != "ReportTypeCode")
                {
                    if (dc.ColumnName.Trim() == "ReportDate")
                    {
                        //string wrap = "<a href='NoonReport.aspx?ReportType=" + dr["ReportTypeCode"].ToString() + "&PKID=" + dr["ReportId"].ToString() + "' target='_blank'>";
                        string wrap = "";
                        sbDetails.Append("<td style='width:150px;'>" + wrap + Convert.ToString(dr[dc]) + "</a></td>");
                    }
                    else
                    {
                        sbDetails.Append("<td style='width:150px;'>" + Convert.ToString(dr[dc]) + "</td>");
                    }
                }
            }
            sbDetails.Append("</tr>");
        }
        sbDetails.Append("</tr></table>");
        sbHeader.Append("</tr></table>");
        litHeader.Text = sbHeader.ToString();
        litData.Text = sbDetails.ToString();
   
        dvFilter.Visible = false;
        dvGrid.Visible = true;
        trVessel.Visible = false; 

        //ScriptManager.RegisterStartupScript(this, this.GetType(), "a", "document.getElementById('dh').style.width = document.documentElement.clientWidth-20;", true);
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "b", "document.getElementById('dd').style.width = document.documentElement.clientWidth-20;", true);
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable dt = SelectedColumns;
         if (dt == null)
        {
            dt=InintalizeTable();
        }
        foreach (ListItem li in chkAvlColumns.Items)
        {
            if(li.Selected)
            {
                try
                {
                    object[] Arr = {li.Value, li.Text};
                    dt.Rows.Add(Arr);
                }
                catch { } 
            }
        }
        SelectedColumns=dt;
        //------------------
        BindAvlColumns();
        BindSelColumns();
    }
    protected void btnRem_Click(object sender, EventArgs e)
    {
        DataTable dt = SelectedColumns;
        if (dt != null)
        {
            foreach (ListItem li in chkSelColumns.Items)
            {
                if(li.Selected)
                {
                    DataRow dr=SelectedColumns.Rows.Find(li.Value);
                    SelectedColumns.Rows.Remove(dr);           
                }
            }
            //------------------
            BindAvlColumns();
            BindSelColumns();    
        }
    }

    // --================== FILTER SECTION
    private void BindFiltersDropDown()
    {
        DataTable dtFilterColumns = InintalizeTable();
        object[] Arr = { "VoyageNo", "VoyageNo" };

        int i = 0;
        for (i = 0; i < TableColumns.GetLength(0) - 1; i++)
        {
            if (dtFilterColumns.Rows.Find(TableColumns[i, 0]) == null)
            {
                Arr[0] = TableColumns[i, 0];
                Arr[1] = TableColumns[i, 1].Replace("[", "").Replace("]", "");
                dtFilterColumns.Rows.Add(Arr);
            }

        }
    }
    private void BindFiltersRepeater()
    {
        rptFilter.DataSource = Filters;
        rptFilter.DataBind();
    }
    public DataTable LoadOpeartors(object DataType)
    {
        DataTable dtOpeartors = new DataTable();
        dtOpeartors.Columns.Add("Opeartor_Code");
        dtOpeartors.Columns.Add("Opeartor_SQL");

        dtOpeartors.Rows.Add("NA", "");

        switch (DataType.ToString())
        {
            case "N":
                dtOpeartors.Rows.Add("Equals To", "=$P1");
                dtOpeartors.Rows.Add("More Than", ">$P1");
                dtOpeartors.Rows.Add("More Than Equal to", ">=$P1");
                dtOpeartors.Rows.Add("Less Than", "<$P1");
                dtOpeartors.Rows.Add("Less Than Equal to", "<=$P1");
                dtOpeartors.Rows.Add("Between", " BETWEEN $P1 AND $P2 ");
                break; 
            case "D":
                dtOpeartors.Rows.Add("is Today", "=GETDATE()");
                dtOpeartors.Rows.Add("is in Last Week", "BETWEEN DATEADD(DAY,-7,getdate()) AND GETDATE()");
                dtOpeartors.Rows.Add("is in Last Month", "BETWEEN DATEADD(DAY,-30,getdate()) AND GETDATE()");
                dtOpeartors.Rows.Add("in Between Dates", "BETWEEN '$P1' AND '$P2'");
                break;
            case "S":
                dtOpeartors.Rows.Add("Equals To", "='$P1'");
                dtOpeartors.Rows.Add("Contains", "Like '%$P1%'");
                dtOpeartors.Rows.Add("Starts With", "Like '$P1%'");
                dtOpeartors.Rows.Add("Ends With", "Like '%$P1'");
                break;
            case "F_COND":
                dtOpeartors.Rows.Add("Equals To", "='$P1'");
                break;
            case "T":  
                dtOpeartors.Rows.Add("More Than", ">=$P1");
                dtOpeartors.Rows.Add("Less Than", "<=$P1");
                dtOpeartors.Rows.Add("Between", " BETWEEN $P1 AND $P2 ");
                break;
            case "B":
                dtOpeartors.Rows.Add("Equals To", "=$P1");
                break;
            case "F_LATDIR":
                dtOpeartors.Rows.Add("Equals To", "='$P1'");
                break; 
            case "F_LONGDIR":
                dtOpeartors.Rows.Add("Equals To", "='$P1'");
                break;
            case "ReportType":
                dtOpeartors.Rows.Add("Equals To", "='$P1'");
                break;
        }
        return dtOpeartors; 
    }
    private void LoadOperands(string FieldName, string Operator, string DataType, DropDownList ddlValue1, TextBox txtValue1, TextBox txtValue2)
    {
        ddlValue1.Visible = false;
        txtValue1.Visible = false;
        txtValue2.Visible = false;

        txtValue1.Text = "";
        txtValue2.Text = "";

        txtValue1.Attributes.Add("onfocus", "");
        txtValue2.Attributes.Add("onfocus", "");

        ddlValue1.Items.Clear();
        if (DataType == "D")
        {
            txtValue1.Attributes.Add("onfocus", "showCalendar('',this,this,'','holder1',5,22,1);");
            txtValue2.Attributes.Add("onfocus", "showCalendar('',this,this,'','holder1',5,22,1);");
        }
        switch (Operator)
        {
            case "More Than":
                txtValue1.Visible = true; 
                break;
            case "More Than Equal to":
                txtValue1.Visible = true;
                break;
            case "Less Than":
                txtValue1.Visible = true;
                break;
            case "Less Than Equal to":
                txtValue1.Visible = true;
                break;
            case "Between":
                txtValue1.Visible = true;
                txtValue2.Visible = true;
                break;
            case "Equals To":
                switch (DataType)
                {
                    case "F_COND":
                        ddlValue1.Visible = true;
                        ddlValue1.Items.Clear();
                        ddlValue1.Items.Add(new ListItem("Laden", "1"));
                        ddlValue1.Items.Add(new ListItem("Ballast", "2"));
                    break;
                    case "B":
                        ddlValue1.Visible = true;
                        ddlValue1.Items.Clear();
                        ddlValue1.Items.Add(new ListItem("Yes", "1"));
                        ddlValue1.Items.Add(new ListItem("No", "0"));
                        break;
                    case "F_LATDIR":
                        ddlValue1.Visible = true;
                        ddlValue1.Items.Clear();
                        ddlValue1.Items.Add(new ListItem("N", "N"));
                        ddlValue1.Items.Add(new ListItem("S", "S"));
                        break;
                    case "F_LONGDIR":
                        ddlValue1.Visible = true;
                        ddlValue1.Items.Clear();
                        ddlValue1.Items.Add(new ListItem("E", "E"));
                        ddlValue1.Items.Add(new ListItem("W", "W"));
                    break;
                    default:
                    txtValue1.Visible = true;
                    break;
                }

                break;
            case "Contains":
                txtValue1.Visible = true;
                break;            
            case "Starts With":
                txtValue1.Visible = true;
                break;            
            case "Ends With":
                txtValue1.Visible = true;
                break;
            case "in Between Dates":
                txtValue1.Visible = true;
                txtValue2.Visible = true;
                break;
        }
    }
    private string getColumnData(string ColumnCode, int index)
    {
        string Result = "";
        for (int i = 0; i < TableColumns.GetLength(0) - 1; i++)
        {
            if (ColumnCode == TableColumns[i, 0].ToString())
            {
                Result = TableColumns[i, index].ToString();
            }
        }
        return Result;
    }
    protected void ddlFilterFields_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string FieldName = ddlFilterFields.SelectedValue;
        //string DataType = getColumnData(ddlFilterFields.SelectedValue, 2);
        //LoadOpeartors(DataType);
        //LoadOperands(FieldName, DataType);
    }
    protected void ddlFilterOperators_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        RepeaterItem ri =(RepeaterItem)((DropDownList)sender).Parent.Parent.Parent;

        DropDownList ddlO = ((DropDownList)sender);
        HiddenField hfdFieldCode=(HiddenField)ri.FindControl("hfdPK"); 
        HiddenField hfdDataType=(HiddenField)ri.FindControl("hfdDataType"); 

        DropDownList ddlV = (DropDownList)ri.FindControl("ddlValue1");
        TextBox txtT1 = (TextBox)ri.FindControl("txtValue1");
        TextBox txtT2 = (TextBox)ri.FindControl("txtValue2");

        LoadOperands(hfdFieldCode.Value,ddlO.SelectedItem.Text,  hfdDataType.Value, ddlV, txtT1, txtT2); 

        //string FieldName = ddlFilterFields.SelectedValue;
        //string DataType = getColumnData(ddlFilterFields.SelectedValue, 2);
        //LoadOperands(FieldName,DataType); 
    }

    protected string getSQL(string FilterFieldText, DropDownList ddlFilterOperators, string DataType, DropDownList ddlValue1, TextBox txtValue1, TextBox txtValue2)
    {
        DataTable dtFilters = Filters;
        if (dtFilters == null)
            dtFilters = InintalizeFilterTable();

        string ViewSQL = ddlFilterOperators.SelectedValue;
        string SQL = ddlFilterOperators.SelectedValue;

        bool Once = false;

        if (ddlValue1.Visible)
        {
            ViewSQL = ddlValue1.SelectedValue;

            if (DataType == "N")
                SQL = SQL.Replace("$P1", Common.CastAsDecimal(ddlValue1.SelectedValue).ToString());
            else
                SQL = SQL.Replace("$P1", ddlValue1.SelectedValue);

            Once = true;
        }

        if (txtValue1.Visible)
        {
            ViewSQL = txtValue1.Text;

            if (DataType == "N")
                SQL = SQL.Replace("$P1", Common.CastAsDecimal(txtValue1.Text).ToString());
            else
                SQL = SQL.Replace("$P1", txtValue1.Text);

            Once = true;
        }

        if (txtValue2.Visible)
        {
            if (Once)
                ViewSQL = "BETWEEN " + ViewSQL + " AND " + txtValue2.Text;

            if (DataType == "N")
                SQL = SQL.Replace("$P2", Common.CastAsDecimal(txtValue2.Text).ToString());
            else
                SQL = SQL.Replace("$P2", txtValue2.Text);

            Once = true;
        }
        //---------------------------------------------------------------

        if (DataType == "D")
        {
            if (ddlFilterOperators.SelectedItem.Text.Trim() == "in Between Dates")
            {
                ViewSQL = FilterFieldText + " " + ViewSQL;
                SQL = FilterFieldText + " " + SQL;
            }
            else
            {
                ViewSQL = FilterFieldText + " " + ddlFilterOperators.SelectedItem.Text;
                SQL = FilterFieldText + " " + SQL;
            }

        }
        else if (DataType == "F_COND")
        {
            ViewSQL = FilterFieldText + " " + ddlFilterOperators.SelectedItem.Text + " " + ddlValue1.SelectedItem.Text;
            SQL = FilterFieldText + " " + SQL;
        }
        else
        {
            if (ddlFilterOperators.SelectedItem.Text == "Between")
                ViewSQL = FilterFieldText + " " + ViewSQL;
            else
                ViewSQL = FilterFieldText + " " + ddlFilterOperators.SelectedItem.Text + " " + ViewSQL;

            SQL = FilterFieldText + " " + SQL;
        }
        //---------------------------------------------------------------
        //MaxFilterRows++;
        //object[] Arr = { MaxFilterRows.ToString(), ViewSQL, SQL };
        //dtFilters.Rows.Add(Arr);

        return SQL;
    }
    protected void tmgbtnDeletedFilter_Click(object sender, EventArgs e)
    {
        List<int> Rem_Ids = new List<int>();
        int PK =Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        if (PK > 0)
        {
            DataTable dt = Filters;
            if (dt != null)
            {
                for( int i=0;i<=dt.Rows.Count-1;i++)
                {
                    if (Common.CastAsInt32(dt.Rows[i]["PK"]) == PK)
                        Rem_Ids.Add(i);
                }
                foreach(int index in Rem_Ids)
                {
                    dt.Rows.RemoveAt(index); 
                }

                Filters=dt;
                BindFiltersRepeater();
            }
        }
 
    }
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem li in chkAvlColumns.Items)
        {
            li.Selected = chkAll.Checked; 
        }
    }
    protected void chkAll1_CheckedChanged(object sender, EventArgs e)
    {
        foreach (ListItem li in chkSelColumns.Items)
        {
            li.Selected = chkAll1.Checked;
        }
    }
}
