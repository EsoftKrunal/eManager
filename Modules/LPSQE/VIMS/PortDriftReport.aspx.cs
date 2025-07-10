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

public partial class PortDriftReport : System.Web.UI.Page
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
    public void ShowRecord()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.VSL_VPRPortDriftReport WHERE PortDriftId=" + Key + " And VesselID='" + CurrentVessel + "'");

        

        if (dt.Rows.Count > 0)
        {

            DataRow dr = dt.Rows[0];
            ReportsPK = Common.CastAsInt32(dt.Rows[0]["ReportsPK"]);

            //--------------------------
            DataTable dtMax = Common.Execute_Procedures_Select_ByQuery("SELECT isnull(MAX(ReportsPK),0) FROM DBO.VW_VSL_VPR_ALLREPORTS WHERE VESSELID='" + CurrentVessel + "'");
            int MaxReportId = Common.CastAsInt32(dtMax.Rows[0][0]);
            if (MaxReportId > 0 && MaxReportId != ReportsPK)
            {
                ShowMessage("Note : This report can not modified ( Only last report can be modified ).", true);
                btnSave.Visible = false;
            }
            //--------------------------


            txtVesselCode.Text=	dr["VesselID"].ToString();
            txtRDate.Text=Common.ToDateString(dr["ReportDate"]);
            txtRDate.Enabled = false;
            txtVoyageNumber.Text=	dr["VoyageNo"].ToString();
            txtArrivalPort.Text = dr["ArrivalPort"].ToString();
            txtArrivalPort.Text = dr["ArrivalPort"].ToString();
            txtCdDrifting.Text =Common.ToDateString(dr["ComDDDate"]);
            ddlCdDriftingHours.SelectedValue = Common.CastAsInt32(dr["DriftHour"]).ToString().PadLeft(2, '0');
            ddlCdDriftingMin.SelectedValue = Common.CastAsInt32(dr["DriftMin"]).ToString().PadLeft(2, '0');
            ddlDriftingReason.SelectedValue = dr["DriftReason"].ToString();
            txtETBDate.Text = Common.ToDateString(dr["ETBDT"]);
            ddlETBHours.SelectedValue = Common.CastAsInt32(dr["ETBHours"]).ToString().PadLeft(2, '0');
            ddlETBMins.SelectedValue = Common.CastAsInt32(dr["ETBMin"]).ToString().PadLeft(2, '0');

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

            txtTotalCargoWeight.Text = dr["TCW"].ToString();
            txtBallastWeight.Text = dr["BW"].ToString();

            //----------------- Fuel -----------------------

            txtRobIFO45.Text = dt.Rows[0]["ROBIFO45"].ToString();
            txtRobIFO1.Text = dt.Rows[0]["ROBIFO1"].ToString();
            txtRobMGO5.Text = dt.Rows[0]["ROBMGO5"].ToString();
            txtRobMGO1.Text = dt.Rows[0]["ROBMGO1"].ToString();
            txtRobMDO.Text = dt.Rows[0]["ROBMDO"].ToString();

            hfdRobIFO45_S.Value = dt.Rows[0]["ROBIFO45"].ToString();
            hfdRobIFO1_S.Value = dt.Rows[0]["ROBIFO1"].ToString();
            hfdRobMGO5_S.Value = dt.Rows[0]["ROBMGO5"].ToString();
            hfdRobMGO1_S.Value = dt.Rows[0]["ROBMGO1"].ToString();
            hfdRobMDO_S.Value = dt.Rows[0]["ROBMDO"].ToString();

            //----------------- Lube -----------------------

            txtRobMECC.Text = dt.Rows[0]["ROBMECC"].ToString();
            txtRobMECYL.Text = dt.Rows[0]["ROBMECYL"].ToString();
            txtRobAECC.Text = dt.Rows[0]["ROBAECC"].ToString();
            txtRobHYD.Text = dt.Rows[0]["ROBHYD"].ToString();

            hfdRobMECC_S.Value = dt.Rows[0]["ROBMECC"].ToString();
            hfdRobMECYL_S.Value = dt.Rows[0]["ROBMECYL"].ToString();
            hfdRobAECC_S.Value = dt.Rows[0]["ROBAECC"].ToString();
            hfdRobHYD_S.Value = dt.Rows[0]["ROBHYD"].ToString();

            //----------------- Fresh Water -----------------------

            txtRobFesshWater.Text = dt.Rows[0]["ROBFesshWater"].ToString();
            hfdRobFesshWater_S.Value = dt.Rows[0]["ROBFesshWater"].ToString();
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
            //ddlDepVoyCondition.SelectedIndex = Common.CastAsInt32(dr["VoyConditionId"]);
            //txtDepPort.Text = dr["DeparturePort"].ToString();
            txtArrivalPort.Text = dr["ArrivalPort"].ToString();
            //txtArrivalPortAgent.Text = dr["ArrivalPortAgent"].ToString();
            //txtPersonalIncharge.Text = dr["PersonalIncharge"].ToString();
            //txtAddressContactDetails.Text = dr["AddressContactDetails"].ToString();
        }
    }
    public bool SaveRecord()
    {
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
        Common.Set_Procedures("SHIP_VPR_InsertUpdate_PortDriftReport");
        Common.Set_ParameterLength(62);
        Common.Set_Parameters(
        new MyParameter("@PortDriftID", Key),
        new MyParameter("@VesselID", txtVesselCode.Text.Trim()),
        new MyParameter("@ReportDate", (txtRDate.Text.Trim() == "" ? DBNull.Value : (object)txtRDate.Text)),
        new MyParameter("@VoyageNo", txtVoyageNumber.Text.Trim()),
        new MyParameter("@ArrivalPort", txtArrivalPort.Text.Trim()),
        new MyParameter("@ComDDDate", (txtCdDrifting.Text.Trim() == "" ? DBNull.Value : (object)txtCdDrifting.Text)),
        new MyParameter("@DriftHour", Common.CastAsInt32(ddlCdDriftingHours.SelectedValue)),
        new MyParameter("@DriftMin", Common.CastAsInt32(ddlCdDriftingMin.SelectedValue)),
        new MyParameter("@DriftReason", ddlDriftingReason.SelectedValue),
        new MyParameter("@ETBDT", (txtETBDate.Text.Trim() == "" ? DBNull.Value : (object)txtETBDate.Text)),
        new MyParameter("@ETBHours", Common.CastAsInt32(ddlETBHours.SelectedValue)),
        new MyParameter("@ETBMin", Common.CastAsInt32(ddlETBMins.SelectedValue)),
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

         new MyParameter("@MECCLube", Common.CastAsDecimal(txtLubeFresh_MECC.Text)),
         new MyParameter("@MECYLLube", Common.CastAsDecimal(txtLubeFresh_MECYL.Text)),
         new MyParameter("@AECCLube", Common.CastAsDecimal(txtLubeFresh_AECC.Text)),
         new MyParameter("@HYDLube", Common.CastAsDecimal(txtLubeFresh_HYD.Text)),
         new MyParameter("@MDOLube", 0),

         new MyParameter("@GeneratedFreshWater", Common.CastAsDecimal(txtLubeFresh_Generated.Text)),
         new MyParameter("@ConsumedFreshWater", Common.CastAsDecimal(txtLubeFresh_Consumed.Text)),

         new MyParameter("@TCW", Common.CastAsDecimal(txtTotalCargoWeight.Text)),
         new MyParameter("@BW", Common.CastAsDecimal(txtBallastWeight.Text)) ,
         
         new MyParameter("@ROBIFO45", ROBIFO45),
         new MyParameter("@ROBIFO1", ROBIFO1),
         new MyParameter("@ROBMGO5", ROBMGO5),
         new MyParameter("@ROBMGO1", ROBMGO1),
         new MyParameter("@ROBMDO", ROBMDO),
         new MyParameter("@ROBMECC", ROBMECC),
         new MyParameter("@ROBMECYL", ROBMECYL),
         new MyParameter("@ROBAECC", ROBAECC),
         new MyParameter("@ROBHYD", ROBHYD),
         new MyParameter("@ROBMDOLube", 0),
         new MyParameter("@ROBFesshWater", ROBFESSHWATER)
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

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT TOP 1 ReportTypeCode, ReportDate FROM [dbo].[VW_VSL_VPR_ALLREPORTS] WHERE VESSELID='" + CurrentVessel + "' AND ReportsPK <> " + ReportsPK + " ORDER BY ReportsPK DESC");

        if (dt.Rows.Count > 0)
        {
            string ReportTypeCode = dt.Rows[0]["ReportTypeCode"].ToString().Trim();
            if (!(ReportTypeCode == "PA" || ReportTypeCode == "PB" || ReportTypeCode == "PD" || ReportTypeCode == "A"))
            {
                ShowMessage("Port report can be created only after arrival report.", true);
                return;
            }

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

        if (SaveRecord())
        {
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

  }
