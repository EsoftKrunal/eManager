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

public partial class PositionReport_Popup : System.Web.UI.Page
{
    public int UserId
    {
        get { return Common.CastAsInt32(ViewState["UserId"]); }
        set { ViewState["UserId"] = value; }
    }
    public int Mail_CId 
    {
        get { return Common.CastAsInt32(ViewState["Mail_CId"]); }
        set { ViewState["Mail_CId"] = value; }
    }
    public string UserName
    {
        get { return ViewState["UserName"].ToString(); }
        set { ViewState["UserName"] = value; }
    }
    public string CurrentVessel
    {
        get { return ViewState["CurrentVessel"].ToString(); }
        set { ViewState["CurrentVessel"] = value; }
    }
   
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            CurrentVessel = Page.Request.QueryString["CurrentShip"].ToString();
            UserId = Common.CastAsInt32(Session["loginid"]);
            UserName = Session["UserName"].ToString();
            lblVessel.Text = Page.Request.QueryString["VslName"].ToString(); 
            txtVoyageNo.Text = GetLatestVogNo();
            Bindgrid();
        }
    }
   
    protected void Filter_Cir(object sender, EventArgs e)
    {
       
        Bindgrid();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        int ReportId = Common.CastAsInt32(((ImageButton)sender).CommandArgument.Split('~').GetValue(0));
        string VesselId = ((ImageButton)sender).CommandArgument.Split('~').GetValue(1).ToString();
        string ReportPage = "";
       
        ReportPage = "NoonReport.aspx";
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Show Report", "window.open('" + ReportPage + "?Key=" + ReportId + "&VesselCode=" + VesselId + "', '_blank', '');", true);

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Bindgrid();
    }
    protected void Bindgrid()
    {
        int StartReportPK = 0;
        string Filter = " ";
        if (ddlReportType.SelectedIndex != 0)
        {
            Filter += " AND ACTIVITY_CODE='" + ddlReportType.SelectedValue + "'";
        }
        if (txtVoyageNo.Text.Trim() != "")
        {
            Filter += " AND VoyageNo LIKE '%" + txtVoyageNo.Text.Trim() + "%'";
        }

        if (txtFDate.Text.Trim() != "")
        {
            Filter += " AND ReportDate >='" + txtFDate.Text.Trim() + "'";
        }
        if (txtTDate.Text.Trim() != "")
        {
            Filter += " AND ReportDate < '" + DateTime.Parse(txtTDate.Text.Trim()).AddDays(1).ToString("dd-MMM-yyyy") + "'";
        }

        if (ddlLocation.SelectedIndex != 0)
        {
            if (ddlLocation.SelectedValue == "A")
            {
                Filter += " AND ACTIVITY_CODE = 'N'";
            }

            if (ddlLocation.SelectedValue == "I")
            {
                Filter += " AND ReportTypeCode <> 'N'";
            }
        }

        string SQL = "SELECT * FROM VW_VSL_VPRNoonReport_New WHERE VesselId = '" + CurrentVessel + "' " + Filter;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL + " ORDER BY ReportsPK Desc ");
        rptPR.DataSource = dt;
        rptPR.DataBind();

        

        //------------------- bind summary

        string SQL1 = "SELECT SUM(RECIFO45) AS RECIFO45,SUM(RECMGO1) AS RECMGO1,SUM(RECMDO) AS RECMDO,SUM(RECMECC) AS RECMECC,SUM(RECMECYL) AS RECMECYL,SUM(RECMECYL_HighBn) AS RECMECYL_HighBn, SUM(RECAECC) AS RECAECC,SUM(RECHYD) AS RECHYD,SUM(RECFRESHWATER) as RECFRESHWATER,SUM(MEIFO45+AEIFO45+CargoHeatingIFO45) as IFOTOAL,SUM(MEMGO1+AEMGO1+CargoHeatingMGO1) AS MGOTOTAL,SUM(MEMDO+AEMDO+CargoHeatingMDO) AS MDOTOTAL,SUM(MECCLube) AS SUMLUBE1,SUM(MECYLLube) AS SUMLUBE2   ,SUM(MECYLLube_HighBn) AS SUMLUBE21,SUM(AECCLube) AS SUMLUBE3,SUM(HYDLube) AS SUMLUBE4,SUM(ConsumedFreshWater) SUMWATER,SUM(GeneratedFreshWater) SUMWATERGEN FROM VW_VSL_VPRNoonReport_New WHERE VesselId = '" + CurrentVessel + "' " + Filter;
        DataTable dtSum = Common.Execute_Procedures_Select_ByQuery(SQL1);
        //----------------------------------------------------

        

        lblIFORec.Text = dtSum.Rows[0]["RECIFO45"].ToString();
        lblMGORec.Text = dtSum.Rows[0]["RECMGO1"].ToString();
        lblMDORec.Text = dtSum.Rows[0]["RECMDO"].ToString();

        lblMECCRec.Text = dtSum.Rows[0]["RECMECC"].ToString();
        lblMECYLRec.Text = dtSum.Rows[0]["RECMECYL"].ToString();

        //-----
        lblMECYLRec_HighBn.Text = dtSum.Rows[0]["RECMECYL_HighBn"].ToString();

        lblAECCRec.Text = dtSum.Rows[0]["RECAECC"].ToString();
        lblHYDRec.Text = dtSum.Rows[0]["RECHYD"].ToString();

        lblFWRec.Text = dtSum.Rows[0]["RECFRESHWATER"].ToString();

        //----------------------------------------------------

        //lblIFO_OB.Text = "??";
        //lblMGO_OP.Text =  "??";
        //lblMDO_OP.Text = "??";
        //lblMECC_OB.Text = "??";
        //lblMECY_OB.Text = "??";
        //lblAECC_OB.Text = "??";
        //lblHYD_OB.Text = "??";

        lblIFOTotal.Text = dtSum.Rows[0]["IFOTOAL"].ToString();
        //lblIFOTotal.Text = dt.Compute("SUM(MEIFO1+AEIFO1+CargoHeatingIFO1)", "").ToString();
        //lblMGOTotal.Text = dt.Compute("SUM(MEMGO5+AEMGO5+CargoHeatingMGO5)", "").ToString();
        lblMGOTotal.Text = dtSum.Rows[0]["MGOTOTAL"].ToString();
        lblMDOTotal.Text = dtSum.Rows[0]["MDOTOTAL"].ToString();


        lblMECCTotal.Text = dtSum.Rows[0]["SUMLUBE1"].ToString();
        lblMECYLTotal.Text = dtSum.Rows[0]["SUMLUBE2"].ToString();

        //------
        lblMECYLTotal_HighBn.Text = dtSum.Rows[0]["SUMLUBE21"].ToString();

        lblAECCTotal.Text = dtSum.Rows[0]["SUMLUBE3"].ToString();
        lblHYDTotal.Text = dtSum.Rows[0]["SUMLUBE4"].ToString();

        lblFreshWaterTotal.Text = dtSum.Rows[0]["SUMWATER"].ToString();
        lblFWGenerated.Text = dtSum.Rows[0]["SUMWATERGEN"].ToString();
        //----------------------------------------------------
        //string SQLROB = "SELECT TOP 1 * FROM VW_VSL_VPRNoonReport WHERE VesselId = '" + CurrentVessel + "' AND ReportDate <='" + txtTDate.Text + "' " + Filter;
        //DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQLROB + " ORDER BY ReportsPK Desc ");
        if (dt.Rows.Count > 0)
        {
            StartReportPK = Common.CastAsInt32(dt.Rows[dt.Rows.Count - 1]["reportsPK"]);
            lblROBIFOTotal.Text = dt.Rows[0]["ROBIFO45Fuel"].ToString();
            //lblIFOTotal.Text = dt.Compute("SUM(MEIFO1+AEIFO1+CargoHeatingIFO1)", "").ToString();
            //lblMGOTotal.Text = dt.Compute("SUM(MEMGO5+AEMGO5+CargoHeatingMGO5)", "").ToString();
            lblROBMGOTotal.Text = dt.Rows[0]["ROBMGO1Fuel"].ToString();
            lblROBMDOTotal.Text = dt.Rows[0]["ROBMDOFuel"].ToString();


            lblROBMECCTotal.Text = dt.Rows[0]["ROBIFO45Lube"].ToString();
            lblROBMECYLTotal.Text = dt.Rows[0]["ROBIFO1Lube"].ToString();

            //----
            lblROBMECYLTotal_HighBn.Text = dt.Rows[0]["ROBIFO1Lube_HighBN"].ToString();

            lblROBAECCTotal.Text = dt.Rows[0]["ROBMGO5Lube"].ToString();
            lblROBHYDTotal.Text = dt.Rows[0]["ROBMGO1Lube"].ToString();

            lblROBFreshWaterTotal.Text = dt.Rows[0]["ROBIFO45FreshWater"].ToString();
            //lblROBFreshWaterTotal.Text = dt.Rows[dt.Rows.Count - 1]["ROBIFO45FreshWater"].ToString();
        }

        //----------------------------------------------------
        if (StartReportPK == 0)
            StartReportPK = 999999999;

        string SQLOpen = " SELECT top 1 * FROM VW_VSL_VPRNoonReport_New WHERE VesselId = '" + CurrentVessel + "' " + " and reportsPK<" + StartReportPK + "";

        DataTable dtOpen = Common.Execute_Procedures_Select_ByQuery(SQLOpen + " ORDER BY ReportsPK Desc ");
        if (dtOpen.Rows.Count > 0)
        {
            lblIFO_OB.Text = dtOpen.Rows[0]["ROBIFO45Fuel"].ToString();
            lblMGO_OP.Text = dtOpen.Rows[0]["ROBMGO1Fuel"].ToString();
            lblMDO_OP.Text = dtOpen.Rows[0]["ROBMDOFuel"].ToString();


            lblMECC_OB.Text = dtOpen.Rows[0]["ROBIFO45Lube"].ToString();

            lblMECY_OB.Text = dtOpen.Rows[0]["ROBIFO1Lube"].ToString();

            //----
            lblMECY_OB_HighBn.Text = dtOpen.Rows[0]["ROBIFO1Lube_HighBn"].ToString();

            lblAECC_OB.Text = dtOpen.Rows[0]["ROBMGO5Lube"].ToString();
            lblHYD_OB.Text = dtOpen.Rows[0]["ROBMGO1Lube"].ToString();

            lblFW_OB.Text = dtOpen.Rows[0]["ROBIFO45FreshWater"].ToString();

        }
        else
        {
            SQLOpen = " SELECT top 1 * FROM VSL_VPRNoonReport_New_OpBal WHERE VesselCode = '" + CurrentVessel + "'";
            dtOpen = Common.Execute_Procedures_Select_ByQuery(SQLOpen);
            if (dtOpen.Rows.Count > 0)
            {
                lblIFO_OB.Text = dtOpen.Rows[0]["FuelIFO"].ToString();
                lblMGO_OP.Text = dtOpen.Rows[0]["FuelMGO"].ToString();
                lblMDO_OP.Text = dtOpen.Rows[0]["FuelMDO"].ToString();

                lblMECC_OB.Text = dtOpen.Rows[0]["LubeMECC"].ToString();
                lblMECY_OB.Text = dtOpen.Rows[0]["LubeMECYLLowBN"].ToString();
                lblMECY_OB_HighBn.Text = dtOpen.Rows[0]["LubeMECYLHighBN"].ToString();
                lblAECC_OB.Text = dtOpen.Rows[0]["LubeAECC"].ToString();
                lblHYD_OB.Text = dtOpen.Rows[0]["LubeHYD"].ToString();

                lblFW_OB.Text = dtOpen.Rows[0]["Water"].ToString();
            }
        }

        ShowPerformance();
    }
   
    public string GetLongitude(object i)
    {
        try
        {
            int Lat = Convert.ToInt32(i);
            if (Lat == 1)
            {
                return "E";
            }
            else if (Lat == 2)
            {
                return "W";
            }
            else
            {
                return "";
            }
        }
        catch
        {
            return "";
        }
    }
    public string GetLattitude(object i)
    {
        try
        {
            int Lon = Convert.ToInt32(i);
            if (Lon == 1)
            {
                return "N";
            }
            else if (Lon == 2)
            {
                return "S";
            }
            else
            {
                return "";
            }
        }
        catch
        {
            return "";
        }
    }

    
    protected string GetLatestVogNo()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(" select top 1 VoyageNo  from VW_VSL_VPRNoonReport_New where VesselID='" + CurrentVessel + "' order by ReportDate desc ");
        if (dt.Rows.Count > 0)
            return dt.Rows[0][0].ToString();
        else
            return "";
    }
    protected void ShowPerformance()
    {
        //string Filter = " ";
        //if (ddlReportType.SelectedIndex != 0)
        //{
        //    Filter += " AND ACTIVITY_CODE='" + ddlReportType.SelectedValue + "'";
        //}
        //if (txtVoyageNo.Text.Trim() != "")
        //{
        //    Filter += " AND VoyageNo LIKE '%" + txtVoyageNo.Text.Trim() + "%'";
        //}
        //if (txtFDate.Text.Trim() != "")
        //{
        //    Filter += " AND ReportDate >='" + txtFDate.Text.Trim() + "'";
        //}
        //if (txtTDate.Text.Trim() != "")
        //{
        //    Filter += " AND ReportDate < '" + DateTime.Parse(txtTDate.Text.Trim()).AddDays(1).ToString("dd-MMM-yyyy") + "'";
        //}

        //if (ddlLocation.SelectedIndex != 0)
        //{
        //    if (ddlLocation.SelectedValue == "A")
        //    {
        //        Filter += " AND ACTIVITY_CODE = 'N'";
        //    }

        //    if (ddlLocation.SelectedValue == "I")
        //    {
        //        Filter += " AND ReportTypeCode <> 'N'";
        //    }
        //}

        //string SQL = "SELECT convert(numeric(10,2) ,avg(ExhTempMin)) as ExhTempMin,convert(numeric(10,2) ,avg(ExhTempMax)) as ExhTempMax,convert(numeric(10,2) ,avg(MERPM)) as MERPM,convert(numeric(10,2) ,sum(EngineDistance)) as EngineDistance,sum(DistanceMadeGood) as DistanceMadeGood, case when SUM(EngineDistance)=0 then null else convert(numeric(10,2) ,((sum(EngineDistance)-SUM(DistanceMadeGood))/SUM(EngineDistance))*100) end as Slip ,convert(numeric(10,2) ,avg(MEOutput)) as MEOutput,convert(numeric(10,2) ,avg(METhermalLoad)) as METhermalLoad,convert(numeric(10,2) ,avg(MELoadIndicator)) as MELoadIndicator,convert(numeric(10,2) ,avg(METCNo1RPM)) as METCNo1RPM,convert(numeric(10,2) ,avg(METCNo2RPM)) as METCNo2RPM,convert(numeric(10,2) ,avg(MESCAVPressure)) as MESCAVPressure,convert(numeric(10,2) ,avg(SCAVTEMP)) as SCAVTEMP,convert(numeric(10,2) ,avg(LubeOilPressure)) as LubeOilPressure,convert(numeric(10,2) ,avg(SeaWaterTemp)) as SeaWaterTemp,convert(numeric(10,2) ,avg(EngRoomTemp)) as EngRoomTemp,convert(numeric(10,2) ,sum(BligPump)) as BligPump,convert(numeric(10,2) ,avg(AUX1Load)) as AUX1Load,convert(numeric(10,2) ,avg(AUX2Load)) as AUX2Load,convert(numeric(10,2) ,avg(AUX3Load)) as AUX3Load,convert(numeric(10,2) ,avg(AUX4Load)) as AUX4Load,convert(numeric(10,2) ,avg(TotalAUXLoad)) as TotalAUXLoad,convert(numeric(10,2) ,sum(AENo1)) as AENo1,convert(numeric(10,2) ,sum(AENo2)) as AENo2,convert(numeric(10,2) ,sum(AENo3)) as AENo3,convert(numeric(10,2) ,sum(AENo4)) as AENo4,convert(numeric(10,2) ,avg(AEFOInletTemp)) as AEFOInletTemp,convert(numeric(10,2) ,sum(Shaft_Gen_Hrs)) as Shaft_Gen_Hrs,convert(numeric(10,2) ,sum(Shaft_Gen_Mins)) as Shaft_Gen_Mins,convert(numeric(10,2) ,sum(Tank_Cleaning_Hrs)) as Tank_Cleaning_Hrs,convert(numeric(10,2) ,sum(Tank_Cleaning_Mins)) as Tank_Cleaning_Mins,convert(numeric(10,2) ,sum(Cargo_Heating_Hrs)) as Cargo_Heating_Hrs,convert(numeric(10,2) ,sum(Cargo_Heating_Mins)) as Cargo_Heating_Mins,convert(numeric(10,2) ,sum(Inert_Hrs)) as Inert_Hrs,convert(numeric(10,2) ,sum(Inert_Mins)) as Inert_Mins  "+
        //     " ,convert(numeric(10,2) ,avg(AuxTempMin1)) as AuxTempMin1,convert(numeric(10,2) ,avg(AuxTempMin2)) as AuxTempMin2,convert(numeric(10,2) ,avg(AuxTempMin3)) as AuxTempMin3,convert(numeric(10,2) ,avg(AuxTempMin4)) as AuxTempMin4 " +
        //     " ,convert(numeric(10,2) ,avg(AuxTempMax1)) as AuxTempMax1,convert(numeric(10,2) ,avg(AuxTempMax2)) as AuxTempMax2,convert(numeric(10,2) ,avg(AuxTempMax3)) as AuxTempMax3,convert(numeric(10,2) ,avg(AuxTempMax4)) as AuxTempMax4 " +
        //    "FROM VW_VSL_VPRNoonReport_New WHERE VesselId = '" + CurrentVessel + "' " + Filter;

        
        string SQL = " exec VPR_VesselPositionReportSummary '" + CurrentVessel + "', '" + ddlReportType.SelectedValue + "','" + txtVoyageNo.Text.Trim() + "', " + ((txtFDate.Text.Trim() == "") ? "null" : "'" + txtFDate.Text.Trim() + "'" ) + "," + ((txtTDate.Text.Trim() == "") ? "null" : "'" + txtTDate.Text.Trim() + "'") + ",'" + ddlLocation.SelectedValue + "'";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            lblExchTempMin.Text = dr["ExhTempMin"].ToString();
            lblExchTempMax.Text = dr["ExhTempMax"].ToString();
            lblMERPM.Text = dr["MERPM"].ToString();
            lblEGDist.Text = dr["EngineDistance"].ToString();

            lblDistanceMadeGoods.Text = dr["DistanceMadeGood"].ToString();

            lblSlip.Text = dr["Slip"].ToString();
            lblMEOutPut.Text = dr["MEOutput"].ToString();
            //lblThermalLoad.Text = dr["METhermalLoad"].ToString();
            lblMELoadIndicator.Text = dr["MELoadIndicator"].ToString();

            lblTC1.Text = dr["METCNo1RPM"].ToString();
            lblTC2.Text = dr["METCNo2RPM"].ToString();


            lblMEScav.Text = dr["MESCAVPressure"].ToString();
            lblScavTemp.Text = dr["SCAVTEMP"].ToString();
            lblLubeOilPressure.Text = dr["LubeOilPressure"].ToString();
            lblSeaWaterTemp.Text = dr["SeaWaterTemp"].ToString();
            lblEngineRoomTemp.Text = dr["EngRoomTemp"].ToString();
            lblBilgePump.Text = dr["BligPump"].ToString();

            lblAux1Load.Text = dr["AUX1Load"].ToString();
            lblAux2Load.Text = dr["AUX2Load"].ToString();
            lblAux3Load.Text = dr["AUX3Load"].ToString();
            lblAux4Load.Text = dr["AUX4Load"].ToString();
            //lblTotAuxLoad.Text = dr["TotalAUXLoad"].ToString();


            lblAE1.Text = dr["AENo1"].ToString();
            lblAE2.Text = dr["AENo2"].ToString();
            lblAE3.Text = dr["AENo3"].ToString();
            lblAE4.Text = dr["AENo4"].ToString();
            lblAETEMP.Text = dr["AEFOInletTemp"].ToString();

            lblShaftGenHrs.Text = dr["Shaft_Gen_Hrs"].ToString();
            lblTCHrs.Text = dr["Tank_Cleaning_Hrs"].ToString();
            lblCHHrs.Text = dr["Cargo_Heating_Hrs"].ToString();
            lblIHrs.Text = dr["Inert_Hrs"].ToString();


            //-------------------------------------------------
            lblExtTempMin1.Text = dr["AuxTempMin1"].ToString();
            lblExtTempMin2.Text = dr["AuxTempMin2"].ToString();
            lblExtTempMin3.Text = dr["AuxTempMin3"].ToString();
            lblExtTempMin4.Text = dr["AuxTempMin4"].ToString();

            lblExtTempMax1.Text = dr["AuxTempMax1"].ToString();
            lblExtTempMax2.Text = dr["AuxTempMax2"].ToString();
            lblExtTempMax3.Text = dr["AuxTempMax3"].ToString();
            lblExtTempMax4.Text = dr["AuxTempMax4"].ToString();

        }
        else
        {
            lblExchTempMin.Text = "";
            lblExchTempMax.Text = "";
            lblMERPM.Text = "";
            lblEGDist.Text = "";
            lblSlip.Text = "";
            lblMEOutPut.Text = "";
            //lblThermalLoad.Text = "";
            lblMELoadIndicator.Text = "";

            lblTC1.Text = "";
            lblTC2.Text = "";


            lblMEScav.Text = "";
            lblScavTemp.Text = "";
            lblLubeOilPressure.Text = "";
            lblSeaWaterTemp.Text = "";
            lblEngineRoomTemp.Text = "";
            lblBilgePump.Text = "";

            lblAux1Load.Text = "";
            lblAux2Load.Text = "";
            lblAux3Load.Text = "";
            lblAux4Load.Text = "";
            //lblTotAuxLoad.Text = "";


            lblAE1.Text = "";
            lblAE2.Text = "";
            lblAE3.Text = "";
            lblAE4.Text = "";
            lblAETEMP.Text = "";

            lblShaftGenHrs.Text = "";
            lblTCHrs.Text = "";
            lblCHHrs.Text = "";
            lblIHrs.Text = "";

            lblExtTempMin1.Text = "";
            lblExtTempMin2.Text = "";
            lblExtTempMin3.Text = "";
            lblExtTempMin4.Text = "";
                                  
            lblExtTempMax1.Text = "";
            lblExtTempMax2.Text = "";
            lblExtTempMax3.Text = "";
            lblExtTempMax4.Text = "";
        }
    }
    protected string ShowTotalOpeningBalance()
    {
        string SQL = "SELECT convert(numeric(10,2) ,min(ExhTempMin)) as ExhTempMin,convert(numeric(10,2) ,max(ExhTempMin)) as ExhTempMin,convert(numeric(10,2) ,avg(MERPM)) as MERPM,convert(numeric(10,2) ,sum(EngineDistance)) as EngineDistance,sum(DistanceMadeGood) as DistanceMadeGood, case when SUM(EngineDistance)=0 then null else convert(numeric(10,2) ,((sum(EngineDistance)-SUM(DistanceMadeGood))/SUM(EngineDistance))*100) end as Slip ,convert(numeric(10,2) ,avg(MEOutput)) as MEOutput,convert(numeric(10,2) ,avg(METhermalLoad)) as METhermalLoad,convert(numeric(10,2) ,max(MELoadIndicator)) as MELoadIndicator,convert(numeric(10,2) ,avg(METCNo1RPM)) as METCNo1RPM,convert(numeric(10,2) ,avg(METCNo2RPM)) as METCNo2RPM,convert(numeric(10,2) ,max(MESCAVPressure)) as MESCAVPressure,convert(numeric(10,2) ,max(SCAVTEMP)) as SCAVTEMP,convert(numeric(10,2) ,max(LubeOilPressure)) as LubeOilPressure,convert(numeric(10,2) ,max(SeaWaterTemp)) as SeaWaterTemp,convert(numeric(10,2) ,max(EngRoomTemp)) as EngRoomTemp,convert(numeric(10,2) ,sum(BligPump)) as BligPump,convert(numeric(10,2) ,avg(AUX1Load)) as AUX1Load,convert(numeric(10,2) ,avg(AUX2Load)) as AUX2Load,convert(numeric(10,2) ,avg(AUX3Load)) as AUX3Load,convert(numeric(10,2) ,avg(AUX4Load)) as AUX4Load,convert(numeric(10,2) ,avg(TotalAUXLoad)) as TotalAUXLoad,convert(numeric(10,2) ,sum(AENo1)) as AENo1,convert(numeric(10,2) ,sum(AENo2)) as AENo2,convert(numeric(10,2) ,sum(AENo3)) as AENo3,convert(numeric(10,2) ,sum(AENo4)) as AENo4,convert(numeric(10,2) ,max(AEFOInletTemp)) as AEFOInletTemp,convert(numeric(10,2) ,sum(Shaft_Gen_Hrs)) as Shaft_Gen_Hrs,convert(numeric(10,2) ,sum(Shaft_Gen_Mins)) as Shaft_Gen_Mins,convert(numeric(10,2) ,sum(Tank_Cleaning_Hrs)) as Tank_Cleaning_Hrs,convert(numeric(10,2) ,sum(Tank_Cleaning_Mins)) as Tank_Cleaning_Mins,convert(numeric(10,2) ,sum(Cargo_Heating_Hrs)) as Cargo_Heating_Hrs,convert(numeric(10,2) ,sum(Cargo_Heating_Mins)) as Cargo_Heating_Mins,convert(numeric(10,2) ,sum(Inert_Hrs)) as Inert_Hrs,convert(numeric(10,2) ,sum(Inert_Mins)) as Inert_Mins  FROM VW_VSL_VPRNoonReport_New ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt.Rows.Count > 0)
        {
            return "??";
        }
        return "??";
    }


    
}

