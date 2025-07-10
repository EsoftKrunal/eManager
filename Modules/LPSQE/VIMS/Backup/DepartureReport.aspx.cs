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

public partial class DepartureReport : System.Web.UI.Page
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
        lblMessage.Text = "";

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
                BindLatLong();
                if (CurrentVessel != "" && Key > 0)
                {
                    ShowReport();
                    btnROB_Click(sender, e);
                }
                else
                {
                    txtVessel.Text = CurrentVessel.Trim();
                    btnROB_Click(sender, e);
                    Copy_ROB_ForNewRecord();
                    CopyVoyageInformation();
                }
                CheckFirstDeparture();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error : " + ex.Message.ToString();
                btnSave.Visible = false; 
            }

        }
        else
        {
            if (! lblFirstMsg.Visible)
            {
               Copy_ROB();
            }
        }
    }
    protected void CopyVoyageInformation()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT TOP 1 VoyageNo,VoyCondition,* from VW_VSL_VPR_ALLREPORTS WHERE ReportTypeCode IN ('A') ORDER BY ReportsPK DESC");
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            txtVoyageNumber.Text = dr["VoyageNo"].ToString();
            txtDepPort.Text = dr["DepArrivalPort"].ToString();
        }
    }
    public void BindLatLong()
    {
        for (int i = 0; i <= 90; i++)
        {
            ddlLattitude1.Items.Add(new ListItem( i.ToString().PadLeft(2,'0') , i.ToString()));
        }

        for (int i = 0; i <= 180; i++)
        {
            ddlLongitude1.Items.Add(new ListItem(i.ToString().PadLeft(3, '0'), i.ToString()));
        }

        for (int i = 0; i < 60; i++)
        {
            ddlLattitude2.Items.Add(new ListItem(i.ToString().PadLeft(2, '0'), i.ToString()));
            ddlLongitude2.Items.Add(new ListItem(i.ToString().PadLeft(2, '0'), i.ToString()));
        }
    }
    public void CheckFirstDeparture()
    {
        lblFirstMsg.Visible = false;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT TOP 1 DEPARTUREID FROM VSL_VPRDepartureReport WHERE VESSELID='" + CurrentVessel + "' ORDER BY ReportsPK");
        if (dt.Rows.Count > 0)  
        {
            int DepKey = Common.CastAsInt32(dt.Rows[0]["DepartureId"]);
            if (DepKey == Key)
            {
                lblFirstMsg.Visible = true;
            }
        }
        else
            lblFirstMsg.Visible = true;

        if (lblFirstMsg.Visible)
        {
            txtRobIFO45.ReadOnly = false;
            txtRobIFO1.ReadOnly = false;
            txtRobMGO5.ReadOnly = false;
            txtRobMGO1.ReadOnly = false;
            txtRobMDO.ReadOnly = false;


            txtRobMECC.ReadOnly = false;
            txtRobMECYL.ReadOnly = false;
            txtRobAECC.ReadOnly = false;
            txtRobHYD.ReadOnly = false;
            txtRobMDOLube.ReadOnly = false;

            txtRobFesshWater.ReadOnly = false;
        }
    }
    public void ShowReport()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.VSL_VPRDepartureReport WHERE DepartureID=" + Key + " And VESSELID='" + CurrentVessel + "'");

        if (dt.Rows.Count > 0)
        {
            //----------------- Voyage Information -----------------------
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

                txtVessel.Text = dt.Rows[0]["VESSELID"].ToString(); 
                txtRDate.Text = Common.ToDateString(dt.Rows[0]["REPORTDATE"]);
                txtRDate.Enabled = false;
                txtVoyageNumber.Text = dt.Rows[0]["VOYAGENO"].ToString(); 
                ddlRestrictedArea.SelectedIndex = Common.CastAsInt32(dt.Rows[0]["RESTRICTEDAREA"]); 
                //string AreaName = dt.Rows[0]["AREANAME"].ToString().Trim();
                //chkECA.Checked = AreaName == "1";
                //chkCA.Checked = AreaName == "2";
                //chkEU.Checked = AreaName == "3"; 
                txtResArea.Text = Common.ToDateString(dt.Rows[0]["ETARESTRICTEDAREA"]);

                //----------------- Charter Party Details  -----------------------

                txtChartererName.Text = dt.Rows[0]["CHARTERERNAME"].ToString(); 
                txtCharterPartySpeed.Text = dt.Rows[0]["CHARTERPARTYSPEED"].ToString(); 
                txtVoyOrderSpeed.Text = dt.Rows[0]["VOYORDERSPEED"].ToString(); 
                txtVoyInstructions.Text = dt.Rows[0]["VOYINSTRUCTIONS"].ToString();

                //----------------- Departure Information  -----------------------
                
                ddlDepVoyCondition.SelectedIndex = Common.CastAsInt32(dt.Rows[0]["VOYCONDITION"]); 
                txtDepPort.Text = dt.Rows[0]["DEPPORT"].ToString(); 
                txtDepArrivalPort.Text = dt.Rows[0]["DEPARRIVALPORT"].ToString(); 
                txtCOSPDate.Text = Common.ToDateString(dt.Rows[0]["COSPDATE"]); 
                ddlCOSPHours.SelectedValue = dt.Rows[0]["COSPHRS"].ToString().PadLeft(2, '0');
                ddlCOSPMin.SelectedValue = dt.Rows[0]["COSPMIN"].ToString().PadLeft(2, '0');
                txtArrivalPortETA.Text = Common.ToDateString(dt.Rows[0]["ARRIVALPORTETA"]);
                ddlArrivalPortETAH.SelectedValue = dt.Rows[0]["ARRIVALPORTETAHRS"].ToString().PadLeft(2, '0'); 
                ddlArrivalPortETAMin.SelectedValue = dt.Rows[0]["ARRIVALPORTETAMIN"].ToString().PadLeft(2, '0');  
                txtDepDraftFwd.Text = dt.Rows[0]["DRAFTFWD"].ToString(); 
                txtDepDraftAft.Text = dt.Rows[0]["DRAFTAFT"].ToString(); 
                txtDepDistanceToGo.Text = dt.Rows[0]["DISTANCETOGO"].ToString(); 
                txtArrivalPortAgent.Text = dt.Rows[0]["ARRIVALPORTAGENT"].ToString(); 
                txtPersonalIncharge.Text = dt.Rows[0]["PERSONALINCHARGE"].ToString(); 
                txtAddressContactDetails.Text = dt.Rows[0]["ADDRESSCONTACTDETAILS"].ToString(); 
                txtPort1.Text = dt.Rows[0]["PORT1"].ToString(); 
                txtPortETA1.Text = Common.ToDateString(dt.Rows[0]["PORTETA1"]); 
                txtPort2.Text = dt.Rows[0]["PORT2"].ToString(); 
                txtPortETA2.Text = Common.ToDateString(dt.Rows[0]["PORTETA2"]); 
                txtPort3.Text = dt.Rows[0]["PORT3"].ToString(); 
                txtPortETA3.Text = Common.ToDateString(dt.Rows[0]["PORTETA3"]);

                //----------------- Weather Information -----------------------
            
                txtCource.Text = dt.Rows[0]["COURCET"].ToString(); 
                txtWindDirection.Text = dt.Rows[0]["WINDDIRECTIONT"].ToString(); 
                ddlWindForce.SelectedValue = dt.Rows[0]["WINDFORCE"].ToString(); 
                txtSeaDirection.Text = dt.Rows[0]["SEADIRECTION"].ToString(); 
                ddlSeaState.SelectedValue = dt.Rows[0]["SEASTATE"].ToString(); 
                txtCurrentDirection.Text = dt.Rows[0]["CURRENTDIRECTION"].ToString(); 
                txtCurrentStrength.Text = dt.Rows[0]["CURRENTSTRENGTH"].ToString(); 
                txtWeatherRemarks.Text = dt.Rows[0]["WEATHERREMARKS"].ToString();

                //----------------- Ship Position -----------------------
                
                ddlLattitude1.SelectedValue = dt.Rows[0]["LATTITUDE1"].ToString(); 
                ddlLattitude2.SelectedValue = dt.Rows[0]["LATTITUDE2"].ToString(); 
                ddlLattitude3.SelectedIndex = Common.CastAsInt32(dt.Rows[0]["LATTITUDE3"]); 
                ddlLongitude1.SelectedValue = dt.Rows[0]["LONGITUDE1"].ToString(); 
                ddlLongitude2.SelectedValue = dt.Rows[0]["LONGITUD2"].ToString();
                ddlLongitude3.SelectedIndex = Common.CastAsInt32(dt.Rows[0]["LONGITUD3"]); 
                txtLocationDescription.Text = dt.Rows[0]["LOCATIONDESCRIPTION"].ToString();

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

                txtRobIFO45Recv.Text = dt.Rows[0]["ROBIFO45RECV"].ToString();
                txtRobIFO1Recv.Text = dt.Rows[0]["ROBIFO1RECV"].ToString();
                txtRobMGO5Recv.Text = dt.Rows[0]["ROBMGO5RECV"].ToString();
                txtRobMGO1Recv.Text = dt.Rows[0]["ROBMGO1RECV"].ToString();
                txtRobMDORecv.Text = dt.Rows[0]["ROBMDORECV"].ToString();

                //----------------- Lube -----------------------
                
                txtRobMECC.Text = dt.Rows[0]["ROBMECC"].ToString(); 
                txtRobMECYL.Text = dt.Rows[0]["ROBMECYL"].ToString(); 
                txtRobAECC.Text = dt.Rows[0]["ROBAECC"].ToString(); 
                txtRobHYD.Text = dt.Rows[0]["ROBHYD"].ToString();
                txtRobMDOLube.Text = dt.Rows[0]["ROBMDOLUBE"].ToString();

                hfdRobMECC_S.Value = dt.Rows[0]["ROBMECC"].ToString();
                hfdRobMECYL_S.Value = dt.Rows[0]["ROBMECYL"].ToString();
                hfdRobAECC_S.Value = dt.Rows[0]["ROBAECC"].ToString();
                hfdRobHYD_S.Value = dt.Rows[0]["ROBHYD"].ToString();
                hfdRobMDOLube_S.Value = dt.Rows[0]["ROBMDOLUBE"].ToString();    

                txtRobMECCRecv.Text = dt.Rows[0]["ROBMECCRECV"].ToString();
                txtRobMECYLRecv.Text = dt.Rows[0]["ROBMECYLRECV"].ToString();
                txtRobAECCRecv.Text = dt.Rows[0]["ROBAECCRECV"].ToString();
                txtRobHYDRecv.Text = dt.Rows[0]["ROBHYDRECV"].ToString();
                txtRobMDOLubeRecv.Text = dt.Rows[0]["ROBMDOLUBERECV"].ToString();

                //----------------- Fresh Water -----------------------
                
                txtRobFesshWater.Text = dt.Rows[0]["ROBFESSHWATER"].ToString();
                hfdRobFesshWater_S.Value = dt.Rows[0]["ROBFESSHWATER"].ToString();

                txtRobFesshWaterRecv.Text = dt.Rows[0]["ROBFESSHWATERRECV"].ToString();

                //----------------- Cargo & Ballast Weight -----------------------
                
                txtTotalCargoWeight.Text = dt.Rows[0]["TCW"].ToString();
                txtBallastWeight.Text = dt.Rows[0]["BW"].ToString();

               //----------------- Cargo & Ballast Weight -----------------------

                txtTCU20L.Text = dt.Rows[0]["TCU20L"].ToString();
                txtTCU40L.Text = dt.Rows[0]["TCU40L"].ToString();
                txtTCU45L.Text = dt.Rows[0]["TCU45L"].ToString();

                txtTCU20E.Text = dt.Rows[0]["TCU20E"].ToString();
                txtTCU40E.Text = dt.Rows[0]["TCU40E"].ToString();
                txtTCU45E.Text = dt.Rows[0]["TCU45E"].ToString();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtVessel.Text.Trim() == "")
        {
            ShowMessage("Your session is expired. Close this window and login Again.", true);
            btnSave.Visible = false;
            //btnExport.Visible = false;
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
            if (!(ReportTypeCode == "PA" || ReportTypeCode == "PB" || ReportTypeCode == "PD"))
            {
                ShowMessage("Departure report can be created only after port report.", true);
                return;
            }

            if (Convert.ToDateTime(txtRDate.Text) < Convert.ToDateTime(dt.Rows[0]["ReportDate"]))
            {
                ShowMessage("Report date must be more or equal to last report date.", true);
                return;
            }
        }


        if (ddlRestrictedArea.SelectedValue == "Yes" && txtResArea.Text.Trim()=="")
        {
            ShowMessage("Please enter date transiting fuel restricted area.", true);
            txtResArea.Focus();
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
        if (Common.CastAsDecimal(txtDepDistanceToGo.Text.Trim()) <= 0)
        {
            ShowMessage("Please enter Distance to go.", true);
            txtDepDistanceToGo.Focus();
            return;
        }
        
        decimal ROBIFO45=0,ROBIFO1=0,ROBMGO5=0,ROBMGO1=0,ROBMDO=0;
        decimal ROBMECC=0,ROBMECYL=0,ROBAECC=0,ROBHYD=0,ROBMDOLUBE=0;
        decimal ROBFESSHWATER=0;
        if (lblFirstMsg.Visible)
        {
            ROBIFO45 = Common.CastAsDecimal(txtRobIFO45.Text);
            ROBIFO1 = Common.CastAsDecimal(txtRobIFO1.Text);
            ROBMGO5 = Common.CastAsDecimal(txtRobMGO5.Text);
            ROBMGO1 = Common.CastAsDecimal(txtRobMGO1.Text);
            ROBMDO = Common.CastAsDecimal(txtRobMDO.Text);
            ROBMECC = Common.CastAsDecimal(txtRobMECC.Text);
            ROBMECYL = Common.CastAsDecimal(txtRobMECYL.Text);
            ROBAECC = Common.CastAsDecimal(txtRobAECC.Text);
            ROBHYD = Common.CastAsDecimal(txtRobHYD.Text);
            ROBMDOLUBE = Common.CastAsDecimal(txtRobMDOLube.Text);
            ROBFESSHWATER = Common.CastAsDecimal(txtRobFesshWater.Text);
        }
        else
        {
            ROBIFO45 = Common.CastAsDecimal(hfdRobIFO45_S.Value);
            ROBIFO1 = Common.CastAsDecimal(hfdRobIFO1_S.Value);
            ROBMGO5 = Common.CastAsDecimal(hfdRobMGO5_S.Value);
            ROBMGO1 = Common.CastAsDecimal(hfdRobMGO1_S.Value);
            ROBMDO = Common.CastAsDecimal(hfdRobMDO_S.Value);
            ROBMECC = Common.CastAsDecimal(hfdRobMECC_S.Value);
            ROBMECYL = Common.CastAsDecimal(hfdRobMECYL_S.Value);
            ROBAECC = Common.CastAsDecimal(hfdRobAECC_S.Value);
            ROBHYD = Common.CastAsDecimal(hfdRobHYD_S.Value);
            ROBMDOLUBE = Common.CastAsDecimal(hfdRobMDOLube_S.Value);
            ROBFESSHWATER = Common.CastAsDecimal(hfdRobFesshWater_S.Value);
        }

        int AreaName = 0;
        //if (chkECA.Checked)
        //{
        //    AreaName = 1;
        //}
        //else if (chkCA.Checked)
        //{
        //    AreaName = 2;
        //}
        //else if (chkEU.Checked)
        //{
        //    AreaName = 3;
        //}
        //else
        //{
        //    AreaName = 0;
        //}

        try
        {
            Common.Set_Procedures("[dbo].[SHIP_VPR_InsertUpdate_VPRDepartureReport]");
            Common.Set_ParameterLength(77);
            Common.Set_Parameters(
               new MyParameter("@DepartureID", Key),
               new MyParameter("@VESSELID", txtVessel.Text.Trim()),
               new MyParameter("@REPORTDATE", (txtRDate.Text.Trim() == "" ? DBNull.Value : (object)txtRDate.Text)),
               new MyParameter("@VOYAGENO", txtVoyageNumber.Text.Trim()),
               new MyParameter("@RESTRICTEDAREA", ddlRestrictedArea.SelectedIndex),
               new MyParameter("@AREANAME", AreaName),
               new MyParameter("@ETARESTRICTEDAREA", (txtResArea.Text.Trim() == "" ? DBNull.Value : (object)txtResArea.Text)),
               new MyParameter("@CHARTERERNAME", txtChartererName.Text.Trim()),
               new MyParameter("@CHARTERPARTYSPEED", Common.CastAsDecimal(txtCharterPartySpeed.Text)),
               new MyParameter("@VOYORDERSPEED", Common.CastAsDecimal(txtVoyOrderSpeed.Text.Trim())),
               new MyParameter("@VOYINSTRUCTIONS", txtVoyInstructions.Text.Trim()),
               new MyParameter("@VOYCONDITION", ddlDepVoyCondition.SelectedIndex),
               new MyParameter("@DEPPORT", txtDepPort.Text.Trim()),
               new MyParameter("@DEPARRIVALPORT", txtDepArrivalPort.Text.Trim()),
               new MyParameter("@COSPDATE", (txtCOSPDate.Text.Trim() == "" ? DBNull.Value : (object)txtCOSPDate.Text)),
               new MyParameter("@COSPHRS", Common.CastAsInt32(ddlCOSPHours.SelectedValue)),
               new MyParameter("@COSPMIN", Common.CastAsInt32(ddlCOSPMin.SelectedValue)),
               new MyParameter("@ARRIVALPORTETA", (txtArrivalPortETA.Text.Trim() == "" ? DBNull.Value : (object)txtArrivalPortETA.Text)),
               new MyParameter("@ARRIVALPORTETAHRS", Common.CastAsInt32(ddlArrivalPortETAH.SelectedValue)),
               new MyParameter("@ARRIVALPORTETAMIN", Common.CastAsInt32(ddlArrivalPortETAMin.SelectedValue)),
               new MyParameter("@DRAFTFWD", Common.CastAsDecimal(txtDepDraftFwd.Text)),
               new MyParameter("@DRAFTAFT", Common.CastAsDecimal(txtDepDraftAft.Text)),
               new MyParameter("@DISTANCETOGO", Common.CastAsDecimal(txtDepDistanceToGo.Text)),
               new MyParameter("@ARRIVALPORTAGENT", txtArrivalPortAgent.Text.Trim()),
               new MyParameter("@PERSONALINCHARGE", txtPersonalIncharge.Text.Trim()),
               new MyParameter("@ADDRESSCONTACTDETAILS", txtAddressContactDetails.Text.Trim()),
               new MyParameter("@PORT1", txtPort1.Text.Trim()),
               new MyParameter("@PORTETA1", (txtPortETA1.Text.Trim() == "" ? DBNull.Value : (object)txtPortETA1.Text)),
               new MyParameter("@PORT2", txtPort2.Text.Trim()),
               new MyParameter("@PORTETA2", (txtPortETA2.Text.Trim() == "" ? DBNull.Value : (object)txtPortETA2.Text)),
               new MyParameter("@PORT3", txtPort3.Text.Trim()),
               new MyParameter("@PORTETA3", (txtPortETA3.Text.Trim() == "" ? DBNull.Value : (object)txtPortETA3.Text)),
               new MyParameter("@COURCET", Common.CastAsDecimal(txtCource.Text)),
               new MyParameter("@WINDDIRECTIONT", Common.CastAsDecimal(txtWindDirection.Text)),
               new MyParameter("@WINDFORCE", Common.CastAsInt32(ddlWindForce.SelectedValue)),
               new MyParameter("@SEADIRECTION", Common.CastAsDecimal(txtSeaDirection.Text)),
               new MyParameter("@SEASTATE", Common.CastAsInt32(ddlSeaState.SelectedValue)),
               new MyParameter("@CURRENTDIRECTION", Common.CastAsDecimal(txtCurrentDirection.Text)),
               new MyParameter("@CURRENTSTRENGTH", Common.CastAsDecimal(txtCurrentStrength.Text)),
               new MyParameter("@WEATHERREMARKS", txtWeatherRemarks.Text.Trim()),
               new MyParameter("@LATTITUDE1", Common.CastAsInt32(ddlLattitude1.SelectedValue)),
               new MyParameter("@LATTITUDE2", Common.CastAsInt32(ddlLattitude2.SelectedValue)),
               new MyParameter("@LATTITUDE3", ddlLattitude3.SelectedIndex),
               new MyParameter("@LONGITUDE1", Common.CastAsInt32(ddlLongitude1.SelectedValue)),
               new MyParameter("@LONGITUD2", Common.CastAsInt32(ddlLongitude2.SelectedValue)),
               new MyParameter("@LONGITUD3", ddlLongitude3.SelectedIndex),
               new MyParameter("@LOCATIONDESCRIPTION", txtLocationDescription.Text.Trim()),

               new MyParameter("@ROBIFO45", ROBIFO45),
               new MyParameter("@ROBIFO1",ROBIFO1),
               new MyParameter("@ROBMGO5",ROBMGO5),
               new MyParameter("@ROBMGO1",ROBMGO1),
               new MyParameter("@ROBMDO",ROBMDO),
               new MyParameter("@ROBMECC",ROBMECC),
               new MyParameter("@ROBMECYL",ROBMECYL),
               new MyParameter("@ROBAECC",ROBAECC),
               new MyParameter("@ROBHYD",ROBHYD),
               new MyParameter("@ROBMDOLUBE",ROBMDOLUBE),
               new MyParameter("@ROBFESSHWATER",ROBFESSHWATER),

               new MyParameter("@ROBIFO45RECV", Common.CastAsDecimal(txtRobIFO45Recv.Text)),
               new MyParameter("@ROBIFO1RECV", Common.CastAsDecimal(txtRobIFO1Recv.Text)),
               new MyParameter("@ROBMGO5RECV", Common.CastAsDecimal(txtRobMGO5Recv.Text)),
               new MyParameter("@ROBMGO1RECV", Common.CastAsDecimal(txtRobMGO1Recv.Text)),
               new MyParameter("@ROBMDORECV", Common.CastAsDecimal(txtRobMDORecv.Text)),
               new MyParameter("@ROBMECCRECV", Common.CastAsDecimal(txtRobMECCRecv.Text)),
               new MyParameter("@ROBMECYLRECV", Common.CastAsDecimal(txtRobMECYLRecv.Text)),
               new MyParameter("@ROBAECCRECV", Common.CastAsDecimal(txtRobAECCRecv.Text)),
               new MyParameter("@ROBHYDRECV", Common.CastAsDecimal(txtRobHYDRecv.Text)),
               new MyParameter("@ROBMDOLUBERECV", Common.CastAsDecimal(txtRobMDOLubeRecv.Text)),
               new MyParameter("@ROBFESSHWATERRECV", Common.CastAsDecimal(txtRobFesshWaterRecv.Text)),
               new MyParameter("@TCW", Common.CastAsDecimal(txtTotalCargoWeight.Text.Trim())),
               new MyParameter("@BW", Common.CastAsDecimal(txtBallastWeight.Text.Trim())),
               //new MyParameter("@TCU20L", "0"),
               //new MyParameter("@TCU40L", "0"),
               //new MyParameter("@TCU45L", "0"),
               //new MyParameter("@TCU20E", "0"),
               //new MyParameter("@TCU40E", "0"),
               //new MyParameter("@TCU45E", "0")
               new MyParameter("@TCU20L", Common.CastAsDecimal(txtTCU20L.Text)),
               new MyParameter("@TCU40L", Common.CastAsDecimal(txtTCU40L.Text)),
               new MyParameter("@TCU45L", Common.CastAsDecimal(txtTCU45L.Text)),
               new MyParameter("@TCU20E", Common.CastAsDecimal(txtTCU20E.Text)),
               new MyParameter("@TCU40E", Common.CastAsDecimal(txtTCU40E.Text)),
               new MyParameter("@TCU45E", Common.CastAsDecimal(txtTCU45E.Text))
               );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                Key = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);
                ShowReport();
                ShowMessage("Report saved successfully.", false);
            }
            else
            {
                ShowMessage("Unable to save Report. Error : " + Common.getLastError(), true);
            }
        }
        catch (Exception ex)
        {
            ShowMessage("Unable to save Report. Error : " + ex.Message.ToString(), true);
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

        txtRobMECC.Text =hfdRobMECC.Value; 
        txtRobMECYL.Text = hfdRobMECYL.Value; 
        txtRobAECC.Text = hfdRobAECC.Value;
        txtRobHYD.Text = hfdRobHYD.Value; 
        txtRobMDOLube.Text = hfdRobMDOLube.Value;

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
        hfdRobMDOLube_S.Value = hfdRobMDOLube.Value;

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
        txtRobMDOLube.Text = hfdRobMDOLube_S.Value;

        txtRobFesshWater.Text = hfdRobFesshWater_S.Value;

    }
  }
