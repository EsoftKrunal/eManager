using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PositionReport_help : System.Web.UI.Page
{
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
    {"(case when VoyCondition=1 then 'Laden' when stoppages=2 then 'Ballast' else '' end)","[VoyCondition]","S"},
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
    {"[AuxTempMax4]","[AuxTempMax4]","N"}
    //{"[ConsumptionRemark]","[ConsumptionRemark]","S"}

    };
    protected void Page_Load(object sender, EventArgs e)
    {
        string[] newcol = new string[TableColumns.GetLength(0)];

        for (int i = 0; i < TableColumns.GetLength(0); i++)
        {
            string sss = (string)TableColumns.GetValue(i, 0) ;
            sss = sss.Substring(1);
            sss = sss.Substring(0, sss.Length - 1);
            newcol[i] = sss;
        }

        Array.Sort(newcol);

        foreach (string ss in newcol)
        {
            Response.Write( "\'"+ss+"\'," );
            //Response.Write(ss+"</br>");
        }
    }
}

//,'AllFastDate','AllFastHH','AllFastMM','AnchorageReason','ArrivalPortAgent','BilgeWaterLanded','BILGEWaterTankROB','BWTS','BWTSHrs','ChartererName','CourceT'
//,'CurrentDirection','CurrentStrength','DistanceToGo','GARBAGE_ROB','Lattitude1','Lattitude2','Lattitude3','Longitud2'
//,'Longitud3','Longitude1','OILYBILGETankROB','OilyBilgWaterLanded','QTYBallasted','QTYDeBallasted','REFRLaden20Ft','REFRLaden40Ft'
//,'SeaDirection','SeasonalLoadLines','Sludgelanded','SLUDGETANKROB','SootLanded','WindDirectionT'

