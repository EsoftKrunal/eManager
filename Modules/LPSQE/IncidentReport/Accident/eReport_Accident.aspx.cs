using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Ionic.Zip;

public partial class eReports_S115_eReport_S115 : System.Web.UI.Page
{
    string FormNo = "Accident";
    public int ReportId
    {       
        get{ return Common.CastAsInt32(ViewState["ReportId"]);}
        set {ViewState["ReportId"]=value ;}
    }
    public string UserName
    {
        get { return ViewState["UserName"].ToString(); }
        set { ViewState["UserName"] = value; }
    }
    public string VesselCode
    {
        get { return ViewState["VesselCode"].ToString(); }
        set { ViewState["VesselCode"] = value; }
    }
    protected void btnLockUnlock_Click(object sender, EventArgs e)
    {
        if (btnLockUnlock.Text.Trim().ToLower().Contains("unlock"))
        {
            Common.Execute_Procedures_Select_ByQuery("UPDATE [dbo].[ER_S115_Report_Office] SET Locked='N' WHERE VESSELCODE='" + VesselCode + "' AND REPORTID=" + ReportId.ToString());
        }
        else
        {
            Common.Execute_Procedures_Select_ByQuery("UPDATE [dbo].[ER_S115_Report_Office] SET Locked='Y' WHERE VESSELCODE='" + VesselCode + "' AND REPORTID=" + ReportId.ToString());
        }
        ShowLockUnlock();
    }
    protected void ShowLockUnlock()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [DBO].[ER_S115_Report_Office] WHERE VESSELCODE='" + VesselCode + "' AND [ReportId] = " + ReportId.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            if (Convert.IsDBNull(dt.Rows[0]["Locked"]))
                btnLockUnlock.Text = "Lock for Ship";
            else
                btnLockUnlock.Text = (dt.Rows[0]["Locked"].ToString() == "Y") ? "Unlock for Ship" : "Lock for Ship";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblMsg.Text = "";
        lblMessage_ITP.Text = "";
        
        if (!IsPostBack)
        {
            VesselCode = Request.QueryString["VC"].ToString();
            
            UserName = Session["UserName"].ToString();

            BindDetails();
            BindRegisters();
            BindRanks();

            if (Request.QueryString["Key"] != null && Request.QueryString["Key"].ToString() != "")
            {
                ReportId = Common.CastAsInt32(Request.QueryString["Key"].ToString());
                ShowReportDetails();

                btnSaveDraft.Visible = (Request.QueryString["Type"].ToString() == "E");

                imgbtnUploadDocsShowPanel.Enabled = (Request.QueryString["Type"].ToString() == "E");
                btnAddInjuredCrew.Enabled = false;

                btnAddEditCrew.Visible = (Request.QueryString["Type"].ToString() == "E");
                //tdTabs.Visible = (Request.QueryString["Type"].ToString() == "E");
                
            }
            string[] Files = System.IO.Directory.GetFiles(Server.MapPath("~/Modules/LPSQE/IncidentReport/Accident"),"*.zip");
            foreach(string fl in Files)
            {
                System.IO.File.Delete(fl);
            }
            btnCOA.Visible = false;
            btnCA.Visible = false;
           // btnOC.Visible = false;
           btnRCA.Visible = false;
        }
        if (ReportId > 0)
        {
            //spn_Note.Visible = ReportId > 0 && dv_Notice.Visible;
            frmDocs.Attributes.Add("src", "../UploadDocuments.aspx?Key=" + FormNo + "&ReportId=" + ReportId.ToString() + "&VC="+VesselCode);
            imgbtnUploadDocsShowPanel.Visible = ReportId > 0;
        }
    }
    private void BindDetails()
    {
        lblFormName.Text = "Accident Report";
        string SQL = "SELECT VESSELName FROM [DBO].Vessel WHERE VESSELCode ='" + VesselCode + "' ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        lblVesselName.Text = dt.Rows[0]["VESSELName"].ToString();        
    }
    private void BindRegisters()
    {
        ProjectCommon.LoadRegisters(chksec1COA, FormNo, 1);
        ProjectCommon.LoadRegisters(rdosec3VA, FormNo, 2);
        ProjectCommon.LoadRegisters(radsec4AOO, FormNo, 3);
        ProjectCommon.LoadRegisters(rdosec4MIRF, FormNo, 4);
        ProjectCommon.LoadRegisters(chklstsec5CargoContaminant, FormNo, 5);
        ProjectCommon.LoadRegisters(chklstsec6, FormNo, 6);
        ProjectCommon.LoadRegisters(chklstsec7EP, FormNo, 7);
        ProjectCommon.LoadRegisters(chklstsecLTE, FormNo, 8);
        ProjectCommon.LoadRegisters(chklstsec8, FormNo, 9);
        ProjectCommon.LoadRegisters(chklstsec9EF, FormNo, 10);
        ProjectCommon.LoadRegisters(chklstsec11FE, FormNo, 11);
        ProjectCommon.LoadRegisters(chklstsec12, FormNo, 12);
        ProjectCommon.LoadRegisters(chklstsec13HA, FormNo, 13);
        ProjectCommon.LoadRegisters(chklstsec13Cond, FormNo, 14);
        ProjectCommon.LoadRegisters(chklstsec13HF, FormNo, 15);
        ProjectCommon.LoadRegisters(chklstsec13JF, FormNo, 16);
        ProjectCommon.LoadRegisters(chklstsec15HA, FormNo, 13);
        ProjectCommon.LoadRegisters(chklstsec15Cond, FormNo, 14);
        ProjectCommon.LoadRegisters(chklstsec15HF, FormNo, 15);
        ProjectCommon.LoadRegisters(chklstsec15JF, FormNo, 16);

        ProjectCommon.LoadRegisters(radsec4TOA, FormNo, 17);
        ProjectCommon.LoadRegisters(chklstsec10TP, FormNo, 18);
        ProjectCommon.LoadRegisters(chklstsec10VE, FormNo, 19);

        ddlLat1.Items.Add(new ListItem("",""));
        ddlLat2.Items.Add(new ListItem("", ""));
        for (int i = 0; i <= 90; i++)
        {
            string res=i.ToString().PadLeft(2,'0');
            ddlLat1.Items.Add(new ListItem(res, res));
        }
        for (int i = 0; i <= 59; i++)
        {
            string res=i.ToString().PadLeft(2,'0');
            ddlLat2.Items.Add(new ListItem(res, res));
        }

        ddlLong1.Items.Add(new ListItem("", ""));
        ddlLong2.Items.Add(new ListItem("", ""));
        for (int i = 0; i <= 180; i++)
        {
            string res = i.ToString().PadLeft(3, '0');
            ddlLong1.Items.Add(new ListItem(res, res));
        }
        for (int i = 0; i <= 59; i++)
        {
            string res = i.ToString().PadLeft(2, '0');
            ddlLong2.Items.Add(new ListItem(res, res));
        }

    }
    private void ShowReportDetails()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [DBO].[ER_S115_Report] WHERE VESSELCODE='" + VesselCode  + "' AND [ReportId] = " + ReportId.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            //------------------ Main Details ----------------------
            lblReportNo.Text = dt.Rows[0]["ReportNo"].ToString();
            txtPort.Text = dt.Rows[0]["Port"].ToString();
            char[] sep= {' '};
            try
            {
                string[] parts = dt.Rows[0]["Latitude"].ToString().Split(sep);
                ddlLat1.SelectedValue = parts[0];
                ddlLat2.SelectedValue = parts[1];
                ddlLat3.SelectedValue = parts[2];
            }
            catch { }
            try
            {
                string[] parts = dt.Rows[0]["Longitude"].ToString().Split(sep);
                ddlLong1.SelectedValue = parts[0];
                ddlLong2.SelectedValue = parts[1];
                ddlLong3.SelectedValue = parts[2];
            }
            catch { }

            txtReportDate.Text = Convert.ToDateTime(dt.Rows[0]["ReportDate"]).ToString("dd-MMM-yyyy");
            txtIncidentDate.Text = Convert.ToDateTime(dt.Rows[0]["IncidentDate"]).ToString("dd-MMM-yyyy HH:mm");

            //------------------------------------------------------

            //------------------  Section 1 : Severity & Classification ---------------

            //rdosec1SOA.SelectedValue = dt.Rows[0]["AccidentSeverity"].ToString();
            if (dt.Rows[0]["AccidentSeverity"].ToString() == "1")
                lblsec1SOA.Text = "Minor";
            else if (dt.Rows[0]["AccidentSeverity"].ToString() == "2")
                lblsec1SOA.Text = "Major";
            else if (dt.Rows[0]["AccidentSeverity"].ToString() == "3")
                lblsec1SOA.Text = "Severe";
            else
                lblsec1SOA.Text = "";
            ProjectCommon.SetCheckboxListData(dt.Rows[0]["AccidentClassification"].ToString(), chksec1COA);

            btnINJ.Visible = chksec1COA.Items.FindByValue("1").Selected;// 1
            btnCC.Visible = chksec1COA.Items.FindByValue("4").Selected;// 4
            btnNav.Visible = chksec1COA.Items.FindByValue("6").Selected;// 6
            btnPol.Visible = chksec1COA.Items.FindByValue("8").Selected; // 8
            btnMoor.Visible = chksec1COA.Items.FindByValue("2").Selected; // 2
            btnEF.Visible = chksec1COA.Items.FindByValue("5").Selected; // 5
            btnDP.Visible = chksec1COA.Items.FindByValue("7").Selected;// 7
            btnFire.Visible = chksec1COA.Items.FindByValue("9").Selected;// 9
            btnSec.Visible = chksec1COA.Items.FindByValue("3").Selected; // 3
            btnEmailBlast.Visible = (Common.CastAsInt32(dt.Rows[0]["AccidentSeverity"]) >= 2);
            //-------------------------------------------------------------------------

            //------------------  Section 2 : Event Description ---------------

            txtsec1EventDesc.Text = dt.Rows[0]["EventDescription"].ToString();

            //-------------------------------------------------------------------------

            //------------------  Section 3 : General Information ---------------

            radsec3PIyes.Checked = (dt.Rows[0]["Alcohol_Drug_Testing"].ToString() == "Y");
            radsec3PIno.Checked = (dt.Rows[0]["Alcohol_Drug_Testing"].ToString() == "N");

            rdosec3VA.SelectedValue = dt.Rows[0]["VesselActivity"].ToString();

            radsec3VDyes.Checked = (dt.Rows[0]["Is_Vessel_Delayed_GI"].ToString() == "Y");
            radsec3VDno.Checked = (dt.Rows[0]["Is_Vessel_Delayed_GI"].ToString() == "N");            

            radsec3BVyes.Checked = (dt.Rows[0]["Is_Bad_Weather"].ToString() == "Y");
            radsec3BVno.Checked = (dt.Rows[0]["Is_Bad_Weather"].ToString() == "N");

            radsec3RVyes.Checked = (dt.Rows[0]["Is_Restricted_Visibility"].ToString() == "Y");
            radsec3RVno.Checked = (dt.Rows[0]["Is_Restricted_Visibility"].ToString() == "N");

            radsec3BCyes.Checked = (dt.Rows[0]["Is_BreachOfCP"].ToString() == "Y");
            radsec3BCno.Checked = (dt.Rows[0]["Is_BreachOfCP"].ToString() == "N");

            //-------------------------------------------------------------------------

            //------------------  Section 4 : Injury to personnel ---------------
            if (btnINJ.Visible)
            {
                BindInjuryToPersons();
            }

            //-------------------------------------------------------------------------

            //------------------  Section 5 : Environment Pollution  ---------------

            if (btnCC.Visible)
            {

                radsec5ccdyes.Checked = (dt.Rows[0]["Cargo_Contamination_Damage"].ToString() == "Y");
                radsec5ccdno.Checked = (dt.Rows[0]["Cargo_Contamination_Damage"].ToString() == "N");

                txtsec5Nameofcharterer.Text = dt.Rows[0]["CCD_NameOfCharterer"].ToString();
                txtsec5TypeofCargo.Text = dt.Rows[0]["CCD_TypeOfCargo"].ToString();
                txtsec5TankHoldNo.Text = dt.Rows[0]["CCD_Tank_Hold_Nos"].ToString();
                ProjectCommon.SetCheckboxListData(dt.Rows[0]["CCD_TankCoating"].ToString(), chklstsec5TankCoating);
                txtsec5TankCoatingOther.Text = dt.Rows[0]["CCD_TankCoating_Other"].ToString();

                txtsec5LoadPort.Text = dt.Rows[0]["CCD_LoadPort"].ToString();
                txtsec5LPTerminalBerth.Text = dt.Rows[0]["CCD_LP_Terminal"].ToString();
                txtsec5LPTerminalDate.Text =  dt.Rows[0]["CCD_LP_Date"].ToString() == "" ? "" : Convert.ToDateTime(dt.Rows[0]["CCD_LP_Date"]).ToString("dd-MMM-yyyy");
                txtsec5DP.Text = dt.Rows[0]["CCD_DischargePort"].ToString();
                txtsec5DPTerminal.Text = dt.Rows[0]["CCD_DP_Terminal"].ToString();
                txtsec5DPTerminalDate.Text = dt.Rows[0]["CCD_DP_Date"].ToString() == "" ? "" : Convert.ToDateTime(dt.Rows[0]["CCD_DP_Date"]).ToString("dd-MMM-yyyy");
                radsec5stsyes.Checked = (dt.Rows[0]["CCD_STS_Operation"].ToString() == "Y");
                radsec5stsno.Checked = (dt.Rows[0]["CCD_STS_Operation"].ToString() == "N");

                radsec5cqdyes.Checked = (dt.Rows[0]["Cargo_Qty_InDispute"].ToString() == "Y");
                radsec5cqdno.Checked = (dt.Rows[0]["Cargo_Qty_InDispute"].ToString() == "N");
                ProjectCommon.SetCheckboxListData(dt.Rows[0]["CQD_CargoContaminant"].ToString(), chklstsec5CargoContaminant);
                chklstsec5CargoQty.SelectedValue = dt.Rows[0]["CQD_CargoQty"].ToString();
                radsec5TankInspectedyes.Checked = (dt.Rows[0]["CQD_TanksInspected"].ToString() == "Y");
                radsec5TankInspectedno.Checked = (dt.Rows[0]["CQD_TanksInspected"].ToString() == "N");
                radsec5WallWashyes.Checked = (dt.Rows[0]["CQD_WallWashTestDone"].ToString() == "Y");
                radsec5WallWashno.Checked = (dt.Rows[0]["CQD_WallWashTestDone"].ToString() == "N");
            }

            //-------------------------------------------------------------------------

            //------------------  Section 6 : Navigation ---------------
            
            if (btnNav.Visible)
            {
                ProjectCommon.SetCheckboxListData(dt.Rows[0]["Navigation"].ToString(), chklstsec6);
            }

            //-------------------------------------------------------------------------

            //------------------  Section 7 : Environment Pollution  ---------------

            if (btnPol.Visible)
            {

                ProjectCommon.SetCheckboxListData(dt.Rows[0]["Env_Pollution"].ToString(), chklstsec7EP);
                txtsec7OtherEP.Text = dt.Rows[0]["Env_Pollution_Other"].ToString();

                radsec7lossyes.Checked = (dt.Rows[0]["Is_LossToEnv"].ToString() == "Y");
                radsec7lossno.Checked = (dt.Rows[0]["Is_LossToEnv"].ToString() == "N");
                rblsec7LTE.SelectedValue = dt.Rows[0]["LE_SpillType"].ToString();
                ProjectCommon.SetCheckboxListData(dt.Rows[0]["LE_Category"].ToString(), chklstsecLTE);
                txtsec7OtherLTE.Text = dt.Rows[0]["LE_Category_Other"].ToString();

                txtsec7FO.Text = dt.Rows[0]["Incident_First_Obseved_Date"].ToString() == "" ? "" : Convert.ToDateTime(dt.Rows[0]["Incident_First_Obseved_Date"]).ToString("dd-MMM-yyyy HH:mm");
                txtsec7CO.Text = dt.Rows[0]["Ceased_Operation_Date"].ToString() == "" ? "" : Convert.ToDateTime(dt.Rows[0]["Ceased_Operation_Date"]).ToString("dd-MMM-yyyy HH:mm");
                radsec7AAyes.Checked = (dt.Rows[0]["Reported_To_Authority"].ToString() == "Y");
                radsec7AAno.Checked = (dt.Rows[0]["Reported_To_Authority"].ToString() == "N");
                txtsec7date1.Text = dt.Rows[0]["ReportingDate"].ToString() == "" ? "" : Convert.ToDateTime(dt.Rows[0]["ReportingDate"]).ToString("dd-MMM-yyyy HH:mm");
                txtsec7AutName.Text = dt.Rows[0]["AuthorityName"].ToString();
            }

            //-------------------------------------------------------------------------

            //------------------  Section 8 : Mooring   ---------------

            if (btnMoor.Visible)
            {
                ProjectCommon.SetCheckboxListData(dt.Rows[0]["Mooring"].ToString(), chklstsec8);
            }

            //-------------------------------------------------------------------------

            //------------------  Section 9 : Equipment Failure  ---------------

            if (btnEF.Visible)
            {

                radsec9CEyes.Checked = (dt.Rows[0]["Is_Equip_Affected"].ToString() == "Y");
                radsec9CEno.Checked = (dt.Rows[0]["Is_Equip_Affected"].ToString() == "N");

                ProjectCommon.SetCheckboxListData(dt.Rows[0]["Equipments"].ToString(), chklstsec9EF);

            }

            //-------------------------------------------------------------------------

            //------------------  Section 10 : Damage to Property  ---------------

            if (btnDP.Visible)
            {

                radsec10TPyes.Checked = (dt.Rows[0]["Is_DamageToThirdParty"].ToString() == "Y");
                radsec10TPno.Checked = (dt.Rows[0]["Is_DamageToThirdParty"].ToString() == "N");


                txtsec10OtherTP.Text = dt.Rows[0]["DamageToThirdParty_Other"].ToString();


                radsec10VEyes.Checked = (dt.Rows[0]["Is_DamageToOwnVessel"].ToString() == "Y");
                radsec10VEno.Checked = (dt.Rows[0]["Is_DamageToOwnVessel"].ToString() == "N");

                txtsec10OtherVE.Text = dt.Rows[0]["DamageToOwnVessel_Other"].ToString();

            }
            //-------------------------------------------------------------------------

            //------------------  Section 11 : Fire  ---------------
            if (btnFire.Visible)
            {
                radsec11Fireyes.Checked = (dt.Rows[0]["Is_Explosion"].ToString() == "Y");
                radsec11Fireno.Checked = (dt.Rows[0]["Is_Explosion"].ToString() == "N");

                ProjectCommon.SetCheckboxListData(dt.Rows[0]["ExplosionOn"].ToString(), chklstsec11FE);

                txtsec11OtherFE.Text = dt.Rows[0]["ExplosionOn_Other"].ToString();
            }
            //-------------------------------------------------------------------------

            //------------------  Section 12 : Security ---------------

            if (btnSec.Visible)
            {
            ProjectCommon.SetCheckboxListData(dt.Rows[0]["Security"].ToString(), chklstsec12);
            txtsec12Other.Text = dt.Rows[0]["Security_Other"].ToString();
            }
            //-------------------------------------------------------------------------

            //------------------  Section 13 : Causes Of Accident (as assessed by the vessel) ---------------
                        
            ProjectCommon.SetCheckboxListData(dt.Rows[0]["CA_HumanActions"].ToString(), chklstsec13HA);
            txtsec13HAOther.Text = dt.Rows[0]["CA_HumanActions_Other"].ToString();

            ProjectCommon.SetCheckboxListData(dt.Rows[0]["CA_Conditions"].ToString(), chklstsec13Cond);
            txtsec13CondOther.Text = dt.Rows[0]["CA_Conditions_Other"].ToString();

            ProjectCommon.SetCheckboxListData(dt.Rows[0]["RC_HumanFactors"].ToString(), chklstsec13HF);
            txtsec13HFOther.Text = dt.Rows[0]["RC_HumanFactors_Other"].ToString();

            ProjectCommon.SetCheckboxListData(dt.Rows[0]["RC_JobFactors"].ToString(), chklstsec13JF);
            txtsec13JFOther.Text = dt.Rows[0]["RC_JobFactors_Other"].ToString();

           
            //-------------------------------------------------------------------------


            //------------------  Section 14 : To be filled by vessel ---------------

            radsec14IAyes.Checked = (dt.Rows[0]["Is_ImmadiateActionTaken"].ToString() == "Y");
            radsec14IAno.Checked = (dt.Rows[0]["Is_ImmadiateActionTaken"].ToString() == "N");
            txtsec14IAYesDesc.Text = dt.Rows[0]["IA_Yes_Desc"].ToString();
            //txtsec14IANoReason.Text = dt.Rows[0]["IA_No_Reason"].ToString();

            radsec14PRyes.Checked = (dt.Rows[0]["Is_Action_Recommended"].ToString() == "Y");
            radsec14PRno.Checked = (dt.Rows[0]["Is_Action_Recommended"].ToString() == "N");
            txtsec14PrYesRADesc.Text = dt.Rows[0]["AR_Yes_Desc"].ToString();
            //txtsec14PrNoRAReason.Text = dt.Rows[0]["AR_No_Reason"].ToString();

            radsec14ITyes.Checked = (dt.Rows[0]["Is_ImmediateTraining"].ToString() == "Y");
            radsec14ITNo.Checked = (dt.Rows[0]["Is_ImmediateTraining"].ToString() == "N");
            txtsec14ITDetails.Text = dt.Rows[0]["IT_Details"].ToString();

            radsec14FTyes.Checked = (dt.Rows[0]["Is_FurtherTrainingReq"].ToString() == "Y");
            radsec14FTno.Checked = (dt.Rows[0]["Is_FurtherTrainingReq"].ToString() == "N");
            txtsec14FTDetails.Text = dt.Rows[0]["FT_Details"].ToString();

            radsec14SMyes.Checked = (dt.Rows[0]["Is_SafetyMeeting"].ToString() == "Y");
            radsec14SMno.Checked = (dt.Rows[0]["Is_SafetyMeeting"].ToString() == "N");
            txtsec14SMMeetingDate.Text = dt.Rows[0]["SM_Date"].ToString() == "" ? "" : Convert.ToDateTime(dt.Rows[0]["SM_Date"]).ToString("dd-MMM-yyyy");

            radsec14SIyes.Checked = (dt.Rows[0]["Is_SupplementaryInfo"].ToString() == "Y");
            radsec14SIno.Checked = (dt.Rows[0]["Is_SupplementaryInfo"].ToString() == "N");
            txtsec14SIyesDetails.Text = dt.Rows[0]["SI_Details"].ToString();

            
            //-------------------------------------------------------------------------

            //------------------  Section 15 : OFFICE USE ONLY  ---------------

            string offSQL = "SELECT * FROM [DBO].[ER_S115_Report_Office] WHERE [ReportId] = " + ReportId.ToString() + " AND [VesselCode] = '" + VesselCode + "' ";
            DataTable dtOff = Common.Execute_Procedures_Select_ByQuery(offSQL);

            if (dtOff != null && dtOff.Rows.Count > 0)
            {
                radsec15VDyes.Checked = (dtOff.Rows[0]["Is_Vessel_Delayed"].ToString() == "Y");
                radsec15VDno.Checked = (dtOff.Rows[0]["Is_Vessel_Delayed"].ToString() == "N");
                radsec15VDyes_OnCheckedChanged(new object(), new EventArgs());
                txtsec15VDdays.Text = dtOff.Rows[0]["VesselDelayed_Days"].ToString();
                txtsec15VDHrs.Text = dtOff.Rows[0]["VesselDelayed_Hours"].ToString();                


                rdosec15PPR.SelectedValue = dtOff.Rows[0]["Potential_Recurrence"].ToString();

                radsec15IDyes.Checked = (dtOff.Rows[0]["Is_InvestigationDone"].ToString() == "Y");
                radsec15IDno.Checked = (dtOff.Rows[0]["Is_InvestigationDone"].ToString() == "N");
                radsec15IDyes_OnCheckedChanged(new object(), new EventArgs());
                txtsec15InvDate.Text = dtOff.Rows[0]["InvestigationDate"].ToString() == "" ? "" : Convert.ToDateTime(dtOff.Rows[0]["InvestigationDate"]).ToString("dd-MMM-yyyy");
                txtsec15NameofPerson.Text = dtOff.Rows[0]["NameOfPerson_Org"].ToString();


                ProjectCommon.SetCheckboxListData(dtOff.Rows[0]["O_HumanActions"].ToString(), chklstsec15HA);
                txtsec15HAOther.Text = dtOff.Rows[0]["O_HumanActions_Other"].ToString();

                ProjectCommon.SetCheckboxListData(dtOff.Rows[0]["O_Conditions"].ToString(), chklstsec15Cond);
                txtsec15CondOther.Text = dtOff.Rows[0]["O_Conditions_Other"].ToString();

                ProjectCommon.SetCheckboxListData(dtOff.Rows[0]["O_HumanFactors"].ToString(), chklstsec15HF);
                txtsec15HFOther.Text = dtOff.Rows[0]["O_HumanFactors_Other"].ToString();

                ProjectCommon.SetCheckboxListData(dtOff.Rows[0]["O_JobFactors"].ToString(), chklstsec15JF);
                txtsec15JFOther.Text = dtOff.Rows[0]["O_JobFactors_Other"].ToString();

                txtsec15ReasonsForDiff.Text = dtOff.Rows[0]["ReasonDiffRCA"].ToString();

                radsec15FUyes.Checked = (dtOff.Rows[0]["Is_FollowUPReq"].ToString() == "Y");
                radsec15FUno.Checked = (dtOff.Rows[0]["Is_FollowUPReq"].ToString() == "N");
                radsec15FUyes_OnCheckedChanged(new object(), new EventArgs());
                txtsec15YesTargetDate.Text = dtOff.Rows[0]["TARGETDATE"].ToString() == "" ? "" : Convert.ToDateTime(dtOff.Rows[0]["TARGETDATE"]).ToString("dd-MMM-yyyy");
                txtsec15YesDetails.Text = dtOff.Rows[0]["FU_Details"].ToString();
                txtsec15FUPIC.Text = dtOff.Rows[0]["FU_PIC"].ToString();

                ProjectCommon.SetCheckboxListData(dtOff.Rows[0]["Suggestions"].ToString(), chksec15SFI);
                txtsec15Remarks.Text = dtOff.Rows[0]["Remarks"].ToString();

                radsec15ICyes.Checked = (dtOff.Rows[0]["Is_Closed"].ToString() == "Y");
                radsec15ICno.Checked = (dtOff.Rows[0]["Is_Closed"].ToString() == "N");
                radsec15ICyes_OnCheckedChanged(new object(), new EventArgs());
                txtsec15CD.Text = dtOff.Rows[0]["CloseDate"].ToString() == "" ? "" : Convert.ToDateTime(dtOff.Rows[0]["CloseDate"]).ToString("dd-MMM-yyyy");
                txtsec15CB.Text = dtOff.Rows[0]["ClosedBy"].ToString();

                radsec15CNyes.Checked = (dtOff.Rows[0]["Is_Closure_Notified"].ToString() == "Y");
                radsec15CNno.Checked = (dtOff.Rows[0]["Is_Closure_Notified"].ToString() == "N");
                radsec15CNyes_OnCheckedChanged(new object(), new EventArgs());
                txtsec15NotifyDate.Text = dtOff.Rows[0]["NotifyDate"].ToString() == "" ? "" : Convert.ToDateTime(dtOff.Rows[0]["NotifyDate"]).ToString("dd-MMM-yyyy");
                

            }
            ShowLockUnlock();
            //-------------------------------------------------------------------------

            //------------------  Section 16 : Documents  ---------------

            BindDocuments();

            //-------------------------------------------------------------------------

            BindEmailBlast();

        }

    }

    protected void BindDocuments()
    {
        string Mode = Request.QueryString["Type"].ToString();
        DataTable dtDocs = Common.Execute_Procedures_Select_ByQuery("SELECT Row_Number() OVER(order by DocId) As SrNo,'" + Mode + "' as Edit_Delete,* FROM " +
               "(SELECT 'Ship' AS Office_Ship,* FROM  [DBO].[ER_Report_Documents_Ship] WHERE ISNULL(Status, 'A') ='A' AND VesselCode='" + VesselCode + "' AND FormNo='" + FormNo + "' AND [ReportId] = " + ReportId.ToString() +
               " UNION SELECT 'Office' AS Office_Ship,* FROM  [DBO].[ER_Report_Documents_Office] WHERE ISNULL(Status, 'A')='A' AND VesselCode='" + VesselCode + "' AND FormNo='" + FormNo + "' AND [ReportId] = " + ReportId.ToString() + ") a");

        rptReportDocs.DataSource = dtDocs;
        rptReportDocs.DataBind();
    }
    protected void btnDelDocument_Click(object sender, EventArgs e)
    {
        int DocId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        Common.Execute_Procedures_Select_ByQuery("UPDATE [DBO].ER_Report_Documents_Ship SET STATUS='D' WHERE VesselCode='" + VesselCode + "' AND FormNo='" + FormNo + "' AND [ReportId] = " + ReportId.ToString() + " AND DOCID=" + DocId.ToString());
        BindDocuments();
    }
    private void BindRanks()
    {
        string SQL = "SELECT RankId, RankCode FROM [DBO].[MP_AllRank]";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        ddlsec4Rank.DataSource = dt;
        ddlsec4Rank.DataValueField = "RankId";
        ddlsec4Rank.DataTextField = "RankCode";
        ddlsec4Rank.DataBind();

        ddlsec4Rank.Items.Insert(0, new ListItem("<-- Select -->", "0"));
    }
    
    // Injury to Crew ---------------------------

    private void BindInjuryToPersons()
    {
        string SQL = "SELECT [TableId],[ReportId], [Is_Crew],IP_CrewNo,[IP_Name], Replace(Convert(varchar(11), [IP_DOB],106), ' ', '-') AS IP_DOB,[IP_Nationality_IDNo],R.RankCode AS IP_Rank_Rating,Replace(Convert(varchar(11), [IP_SignedOnDate],106), ' ', '-') AS IP_SignedOnDate,[IP_NoOfHrOnBoard], [OptionText] AS OCIMF " +
                     "FROM [DBO].[ER_S115_InjuryToPerson] IP " +
                     "INNER JOIN  [DBO].[ER_RegisterOptions] RO ON RO.[OptionId] = IP.[OCI_MI_Reporting] AND RegisterId = 4 " +
                     "LEFT JOIN [DBO].[MP_AllRank] R ON R.[RankId] = IP.[IP_Rank_Rating] " + 
                     "WHERE ISNULL(IP.Status, 'A') = 'A' AND [VesselCode]= '" + VesselCode + "' AND [ReportId] = " + ReportId.ToString();
       DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

       rptSec4.DataSource = dt;
       rptSec4.DataBind();

       string SQL1 = "SELECT MTC, LTI, (MTC + LTI + RWC) AS TRC FROM (SELECT " +
                     "(SELECT Count([OCI_MI_Reporting]) FROM [DBO].[ER_S115_InjuryToPerson] WHERE [VesselCode] = '" + VesselCode + "' AND ISNULL(Status, 'A') = 'A' AND [ReportId] = " + ReportId.ToString() + " AND [OCI_MI_Reporting] IN (24,26,28)) AS MTC, " +
                     "(SELECT Count([OCI_MI_Reporting]) FROM [DBO].[ER_S115_InjuryToPerson] WHERE [VesselCode] = '" + VesselCode + "' AND ISNULL(Status, 'A') = 'A' AND [ReportId] = " + ReportId.ToString() + " AND [OCI_MI_Reporting] IN (25,27,29,26)) AS LTI, " +
                     "(SELECT Count([OCI_MI_Reporting]) FROM [DBO].[ER_S115_InjuryToPerson] WHERE [VesselCode] = '" + VesselCode + "' AND ISNULL(Status, 'A') = 'A' AND [ReportId] = " + ReportId.ToString() + " AND [OCI_MI_Reporting] IN (28)) AS RWC " +
                     ")a ";                   
           
       DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(SQL1);

       txtsec4MTC.Text = dt1.Rows[0]["MTC"].ToString();
       txtsec4LTI.Text = dt1.Rows[0]["LTI"].ToString();
       txtsec4TRC.Text = dt1.Rows[0]["TRC"].ToString();
    }
   
    protected void btnAddInjuredCrew_Click(object sender, ImageClickEventArgs e)
    {
        dvAddEditSection4.Visible = true;
        btnAddEditCrew.Visible = true;
        hfCrewTableId.Value = "0";
    }
    protected void btnViewCrew_Click(object sender, ImageClickEventArgs e)
    {
        int TableId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        ShowInjuredCrewDetails(TableId);
        dvAddEditSection4.Visible = true;
        btnAddEditCrew.Visible = false;
    }
    protected void btnEditCrew_Click(object sender, ImageClickEventArgs e)
    {
        int TableId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        ShowInjuredCrewDetails(TableId);
        dvAddEditSection4.Visible = true;
        btnAddEditCrew.Visible = true;
    }
    protected void btnDeleteCrew_Click(object sender, ImageClickEventArgs e)
    {
        int TableId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        string SQL = "UPDATE [DBO].[ER_S115_InjuryToPerson] SET [Status] = 'D' WHERE [VesselCode]= '" + VesselCode + "' AND [ReportId] = " + ReportId.ToString() + " AND  [TableId] =" + TableId.ToString() + " ; Select -1 ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        if (dt.Rows.Count > 0)
        {
            BindInjuryToPersons();
            lblMsg.Text = "Crew/Contractor deleted successfully.";
        }
        else
        {
            lblMsg.Text = "Unable to delete Crew/Contractor.";
        }
    }
    protected void ShowInjuredCrewDetails(int TableId)
    {
        string SQL = "SELECT * FROM [DBO].[ER_S115_InjuryToPerson] WHERE [VesselCode]= '" + VesselCode + "' AND [ReportId] = " + ReportId.ToString() + " AND [TableId] = " + TableId.ToString();
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        if (dt.Rows.Count > 0)
        {
            hfCrewTableId.Value = dt.Rows[0]["TableId"].ToString();
            if (dt.Rows[0]["Is_Crew"].ToString() != "")
            {
                radsec4Crew.Checked = (dt.Rows[0]["Is_Crew"].ToString() == "Y");
                radsec4Contractor.Checked = (dt.Rows[0]["Is_Crew"].ToString() == "N");
            }

            txtsec4IpCrewNo.Enabled = radsec4Crew.Checked;
            txtsec4IpCrewNo.Text = dt.Rows[0]["IP_CrewNo"].ToString();
            txtsec4IpName.Text = dt.Rows[0]["IP_Name"].ToString();
            txtsec4DOB.Text = (dt.Rows[0]["IP_DOB"].ToString() == "" ? "" : Convert.ToDateTime(dt.Rows[0]["IP_DOB"]).ToString("dd-MMM-yyyy"));
            txtsec4Nat.Text = dt.Rows[0]["IP_Nationality_IDNo"].ToString();
            //txtsec4RR.Text = dt.Rows[0]["IP_Rank_Rating"].ToString();
            if (radsec4Crew.Checked)
            {
                ddlsec4Rank.SelectedValue = dt.Rows[0]["IP_Rank_Rating"].ToString();
            }
            ddlsec4Rank.Enabled = radsec4Crew.Checked;
            txtsec4SDate.Text = (dt.Rows[0]["IP_SignedOnDate"].ToString() == "" ? "" : Convert.ToDateTime(dt.Rows[0]["IP_SignedOnDate"]).ToString("dd-MMM-yyyy"));
            txtsec4NoOfHr.Text = dt.Rows[0]["IP_NoOfHrOnBoard"].ToString();
            txtsec4NoOfHr.Enabled = radsec4Contractor.Checked;
            if (dt.Rows[0]["IP_Is_VisitToDoctor_Necessary"].ToString() != "")
            {
                radsec4DNyes.Checked = (dt.Rows[0]["IP_Is_VisitToDoctor_Necessary"].ToString() == "Y");
                radsec4DNno.Checked = (dt.Rows[0]["IP_Is_VisitToDoctor_Necessary"].ToString() == "N");
            }
            RequiredFieldValidator4.Enabled = radsec4Crew.Checked;
            if (dt.Rows[0]["IP_Activity_OD"].ToString() != "")
            {
                radsec4OFD_od.Checked = (dt.Rows[0]["IP_Activity_OD"].ToString() == "Y");
                radsec4OFD_fd.Checked = (dt.Rows[0]["IP_Activity_OD"].ToString() == "N");
            }

            txtsec4NOH.Text = dt.Rows[0]["IP_ActivityHours"].ToString();
            if (dt.Rows[0]["IP_RestrictedWork"].ToString() != "")
            {
                radsec4RWyes.Checked = (dt.Rows[0]["IP_RestrictedWork"].ToString() == "Y");
                radsec4RWno.Checked = (dt.Rows[0]["IP_RestrictedWork"].ToString() == "N");
            }

            if (dt.Rows[0]["IP_MedicalTreatmentReqd"].ToString() != "")
            {
                radsec4MTRyes.Checked = (dt.Rows[0]["IP_MedicalTreatmentReqd"].ToString() == "Y");
                radsec4MTRno.Checked = (dt.Rows[0]["IP_MedicalTreatmentReqd"].ToString() == "N");
            }

            if (dt.Rows[0]["IP_AreaOfOperation"].ToString() != "")
            {
                try
                {
                    radsec4AOO.SelectedValue = dt.Rows[0]["IP_AreaOfOperation"].ToString();
                }
                catch { }
            }
            txtsec4AOOOther.Text = dt.Rows[0]["IP_AreaOfOperation_Other"].ToString();

            if (dt.Rows[0]["IP_TypeOfInjury"].ToString() != "")
            {
                try
                {
                    radsec4TOA.SelectedValue = dt.Rows[0]["IP_TypeOfInjury"].ToString();
                }
                catch { }
            }

            txtsec4TOAOther.Text = dt.Rows[0]["IP_TypeOfInjury_Other"].ToString();

            if (dt.Rows[0]["OCI_MI_Reporting"].ToString() != "")
            {
                try
                {
                rdosec4MIRF.SelectedValue = dt.Rows[0]["OCI_MI_Reporting"].ToString();
                }
                catch { }
            }
        }
    }
    protected void btnAddEditCrew_Click(object sender, EventArgs e)
    {
        DateTime d;

        if (!DateTime.TryParse(txtsec4DOB.Text, out d))
        {
            lblMessage_ITP.Text = "Please enter valid date.";
            txtsec4DOB.Focus();
            return;
        }

        TimeSpan ts = DateTime.Today - DateTime.Parse(txtsec4DOB.Text);
        DateTime Age = DateTime.MinValue + ts;        
        int Years = Age.Year - 1;        
        if (Years < 18 )
        {
            lblMessage_ITP.Text = "Please check! Age should be minimum 18 years.";
            txtsec4DOB.Focus();
            return;
        }

        try
        {
            Common.Set_Procedures("[DBO].ER_InsertUpdate_ER_S115_InjuredCrewDetails");
            Common.Set_ParameterLength(21);
            Common.Set_Parameters(
                new MyParameter("@TABLEID", hfCrewTableId.Value),
                new MyParameter("@REPORTID", ReportId),
                new MyParameter("@VESSELCODE", VesselCode),
                new MyParameter("@IS_CREW", (radsec4Crew.Checked ? "Y" : "N")),
                new MyParameter("@IP_CrewNo", txtsec4IpCrewNo.Text.Trim()),
                new MyParameter("@IP_NAME", txtsec4IpName.Text.Trim() ),
                new MyParameter("@IP_DOB", txtsec4DOB.Text.Trim()),
                new MyParameter("@IP_NATIONALITY_IDNO", txtsec4Nat.Text.Trim() ),
                new MyParameter("@IP_RANK_RATING", ddlsec4Rank.SelectedValue ),
                new MyParameter("@IP_SIGNEDONDATE", txtsec4SDate.Text.Trim() ),
                new MyParameter("@IP_NOOFHRONBOARD", Common.CastAsDecimal(txtsec4NoOfHr.Text) ),
                new MyParameter("@IP_IS_VISITTODOCTOR_NECESSARY", ((radsec4DNyes.Checked) ? "Y" : "N")),
                new MyParameter("@IP_ACTIVITY_OD", ((radsec4OFD_od.Checked) ? "Y" : "N")),
                new MyParameter("@IP_ACTIVITYHOURS", Common.CastAsDecimal(txtsec4NOH.Text)),
                new MyParameter("@IP_RESTRICTEDWORK",  ((radsec4RWyes.Checked)?"Y":"N") ),
                new MyParameter("@IP_MEDICALTREATMENTREQD", ((radsec4MTRyes.Checked)?"Y":"N")),
                new MyParameter("@IP_AREAOFOPERATION", radsec4AOO.SelectedValue ),
                new MyParameter("@IP_AREAOFOPERATION_OTHER", txtsec4AOOOther.Text.Trim() ),
                new MyParameter("@IP_TYPEOFINJURY", radsec4TOA.SelectedValue ),
                new MyParameter("@IP_TYPEOFINJURY_OTHER", txtsec4TOAOther.Text.Trim() ),
                new MyParameter("@OCI_MI_REPORTING", rdosec4MIRF.SelectedValue)
                );
            DataSet dsComponents = new DataSet();
            dsComponents.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsComponents);
            if (res)
            {
                ClearInjuryPersonellFields();
                BindInjuryToPersons();
                dvAddEditSection4.Visible = false;
            }
            else
            {
                lblMessage_ITP.Text = "Unable to add crew.Error :" + Common.getLastError();
            }
        }
        catch (Exception ex)
        {
            lblMessage_ITP.Text = "Unable to add crew.Error :" + ex.Message;
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ClearInjuryPersonellFields();
        BindInjuryToPersons();
        dvAddEditSection4.Visible = false;
    }

    // --------------------------- SECTION 4 ( Injury to personnel )
    protected void radsec4CC_OnCheckedChanged(object sender, EventArgs e)
    {
        txtsec4IpCrewNo.Enabled = ddlsec4Rank.Enabled = RequiredFieldValidator4.Enabled = radsec4Crew.Checked;
        txtsec4IpCrewNo.Text = "";
        ddlsec4Rank.SelectedIndex = 0;


        txtsec4NoOfHr.Enabled = radsec4Contractor.Checked;
        txtsec4NoOfHr.Text = "";
    }

    // --------------------------- SECTION 5 ( Cargo )
    protected void radsec5ccd_OnCheckedChanged(object sender, EventArgs e)
    {
        pnl_radsec5ccd.Enabled = radsec5ccdyes.Checked;
        ClearControlsIfDisable(pnl_radsec5ccd);
    }
    protected void radsec5cqd_OnCheckedChanged(object sender, EventArgs e)
    {
        pnl_radsec5cqd.Enabled = radsec5cqdyes.Checked;
        ClearControlsIfDisable(pnl_radsec5cqd);
    }
    protected void txtsec5LoadPort_OnTextChanged(object sender, EventArgs e)
    {
        if (txtsec5LoadPort.Text.Trim() == "")
        {
            txtsec5LPTerminalBerth.Enabled = false;
            txtsec5LPTerminalBerth.Text = "";

            txtsec5LPTerminalDate.Enabled = false;
             txtsec5LPTerminalDate.Text = "";
        }
        else
        {
            txtsec5LPTerminalBerth.Enabled = true;
            txtsec5LPTerminalBerth.Text = "";

            txtsec5LPTerminalDate.Enabled = true;
            txtsec5LPTerminalDate.Text = "";
        }
     }

    protected void txtsec5DP_OnTextChanged(object sender, EventArgs e)
    {
        if (txtsec5DP.Text.Trim() == "")
        {
            txtsec5LPTerminalBerth.Enabled = false;
            txtsec5LPTerminalBerth.Text = "";

            txtsec5LPTerminalDate.Enabled = false;
            txtsec5LPTerminalDate.Text = "";
        }
        else
        {
            txtsec5DPTerminal.Enabled = true;
            txtsec5DPTerminal.Text = "";

            txtsec5DPTerminalDate.Enabled = true;
            txtsec5DPTerminalDate.Text = "";
        }
    }
    

    // --------------------------- SECTION 8 ( Pollution )
    protected void radsec7loss_OnCheckedChanged(object sender, EventArgs e)
    {
        rblsec7LTE.Enabled = radsec7lossyes.Checked;
        rblsec7LTE.ClearSelection();
    }
    protected void radsec7AA_OnCheckedChanged(object sender, EventArgs e)
    {
        txtsec7date1.Enabled = radsec7lossyes.Checked;
        txtsec7date1.Text = "";

        txtsec7AutName.Enabled = radsec7lossyes.Checked;
        txtsec7AutName.Text = "";
    }

    // --------------------------- SECTION 9 ( Equipment Failure )
    protected void radsec9CE_OnCheckedChanged(object sender, EventArgs e)
    {
        chklstsec9EF.Enabled = radsec9CEyes.Checked;
        chklstsec9EF.ClearSelection();
    }

    // --------------------------- SECTION 10 ( Damage to Property )
    protected void radsec10VE_OnCheckedChanged(object sender, EventArgs e)
    {
        chklstsec10VE.Enabled = radsec10VEyes.Checked;
        txtsec10OtherVE.Enabled = radsec10VEyes.Checked;

        chklstsec10VE.ClearSelection();
        txtsec10OtherVE.Text = "";
    }
    protected void radsec10TP_OnCheckedChanged(object sender, EventArgs e)
    {
        chklstsec10TP.Enabled = radsec10TPyes.Checked;
        txtsec10OtherTP.Enabled = radsec10TPyes.Checked;

        chklstsec10TP.ClearSelection();
        txtsec10OtherTP.Text = "";
    }

    // --------------------------- SECTION 11 ( FIRE )
    protected void radsec11Fire_OnCheckedChanged(object sender, EventArgs e)
    {
        pnl_radsec11Fire.Enabled = radsec11Fireyes.Checked;
        ClearControlsIfDisable(pnl_radsec11Fire);
    }
    
    // --------------------------- SECTION 14 ( Corrective Actions )
    protected void radsec14SM_OnCheckedChanged(object sender, EventArgs e)
    {
        txtsec14SMMeetingDate.Enabled = radsec14SMyes.Checked;
        txtsec14SMMeetingDate.Text = "";
    }

    // --------------------------- SECTION 15 ( Office Use Only )
    protected void radsec15IDyes_OnCheckedChanged(object sender, EventArgs e)
    {
        trID.Visible = radsec15IDyes.Checked;
        if (!trID.Visible)
        {
            txtsec15InvDate.Text = txtsec15NameofPerson.Text = "";
        }
    }
    protected void radsec15VDyes_OnCheckedChanged(object sender, EventArgs e)
    {
        trVD.Visible = radsec15VDyes.Checked;
        if (!trVD.Visible)
        {
            txtsec15VDdays.Text = txtsec15VDHrs.Text = "";
        }
    }
    protected void radsec15FUyes_OnCheckedChanged(object sender, EventArgs e)
    {
        trFR.Visible = radsec15FUyes.Checked;
        if (!trFR.Visible)
        {
            txtsec15YesTargetDate.Text = txtsec15YesDetails.Text = txtsec15FUPIC.Text = "";
        }
    }
    protected void radsec15ICyes_OnCheckedChanged(object sender, EventArgs e)
    {
        trIC.Visible = radsec15ICyes.Checked;
        if (!trIC.Visible)
        {
            txtsec15CD.Text = txtsec15CB.Text = "";
        }
    }
    protected void radsec15CNyes_OnCheckedChanged(object sender, EventArgs e)
    {
        trVN.Visible = radsec15CNyes.Checked;
        if (!trVN.Visible)
        {
            txtsec15NotifyDate.Text = "";
        }
    }
    

    // --------------------------- SECTION 16 ( Documents )
    protected void imgbtnUploadDocsShowPanel_Click(object sender, ImageClickEventArgs e)
    {
        dvUploadDoc.Visible = true;

    }
    protected void btnCloseDocWindow_Click(object sender, EventArgs e)
    {
        
        BindDocuments();
        dvUploadDoc.Visible = false;
    }
    protected void imgbtnUpdateDocDescr_Click(object sender, ImageClickEventArgs e)
    {
        int DocId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        hfDocId.Value = DocId.ToString();

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT [Descr] FROM [DBO].[ER_S115_Report_Documents_Ship] WHERE [DocId] = " + DocId.ToString() + "  AND [ReportId] = " + ReportId.ToString());
        txtDocDescription.Text = dt.Rows[0]["Descr"].ToString();

        dvUpdateDocDescr.Visible = true;
    }
    protected void btnCloseDocUpdate_Click(object sender, ImageClickEventArgs e)
    {
        hfDocId.Value = "";
        txtDocDescription.Text = "";
        lblMsg_Doc.Text = "";
        dvUpdateDocDescr.Visible = false;
        BindDocuments();
    }
    protected void btnUpdateDescr_Click(object sender, EventArgs e)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("Update [DBO].ER_S115_Report_Documents_Ship SET Descr = '" + txtDocDescription.Text.Trim() + "' WHERE [DocId] = " + hfDocId.Value.ToString() + "  AND [ReportId] = " + ReportId.ToString() + "; Select -1");
        if (dt.Rows.Count > 0)
        {
            lblMsg_Doc.Text = "Description updated successfully.";
        }
        else
        {
            lblMsg_Doc.Text = "Unable to update description.";
        }

    }
    

    protected void ClearControlsIfDisable(Panel pnl)
    {
        if (!pnl.Enabled)
        {
            foreach (Control ctl in pnl.Controls)
            {
                if (ctl.GetType() == typeof(CheckBox))
                {
                    ((CheckBox)ctl).Checked = false;
                }
                if (ctl.GetType() == typeof(TextBox))
                {
                    ((TextBox)ctl).Text = "";
                }
                if (ctl.GetType() == typeof(RadioButton))
                {
                    ((RadioButton)ctl).Checked = false;
                }
                if (ctl.GetType() == typeof(CheckBoxList))
                {
                    ((CheckBoxList)ctl).ClearSelection();
                }
                if (ctl.GetType() == typeof(RadioButtonList))
                {
                    ((RadioButtonList)ctl).ClearSelection();
                }
            }
        }
    }
    
    private void ClearInjuryPersonellFields()
    {
        hfCrewTableId.Value = "0";
        radsec4Crew.Checked = false;
        radsec4Contractor.Checked = false;

        txtsec4IpCrewNo.Text = "";
        txtsec4IpName.Text = "";
        txtsec4DOB.Text = "";
        txtsec4Nat.Text = "";
        ddlsec4Rank.SelectedIndex = 0;
        txtsec4SDate.Text = "";
        txtsec4NoOfHr.Text = "";

        radsec4DNyes.Checked = false;
        radsec4DNno.Checked = false;

        radsec4OFD_od.Checked = false;
        radsec4OFD_fd.Checked = false;
        txtsec4NOH.Text = "";

        radsec4RWyes.Checked = false;
        radsec4RWno.Checked = false;

        radsec4MTRyes.Checked = false;
        radsec4MTRno.Checked = false;

        radsec4AOO.SelectedIndex = -1;
        txtsec4AOOOther.Text = "";

        radsec4TOA.SelectedIndex = -1;
        txtsec4TOAOther.Text = "";

        rdosec4MIRF.SelectedIndex = -1;
        txtsec4MTC.Text = "";
        txtsec4LTI.Text = "";
        txtsec4TRC.Text = "";

        txtsec4IpCrewNo.Enabled = txtsec4NoOfHr.Enabled = ddlsec4Rank.Enabled = false;
        RequiredFieldValidator4.Enabled = true;
    }
    protected void btn_Tab_Click(object sender, EventArgs e)
    {
        Div1.Visible = false;
        Div4.Visible = false;
        Div5.Visible = false;
        Div6.Visible = false;
        Div7.Visible = false;
        Div8.Visible = false;
        Div9.Visible = false;
        Div10.Visible = false;
        Div11.Visible = false;
        Div12.Visible = false;
        Div13.Visible = false;
        Div14.Visible = false;
        Div15.Visible = false;
        Div16.Visible = false;
        Div99.Visible = false;
        Div100.Visible = false;


        btnHome.CssClass="color_tab";
        btnCOA.CssClass="color_tab";
        btnCA.CssClass="color_tab";
        btnOC.CssClass="color_tab";
        btnINJ.CssClass="color_tab";
        btnCC.CssClass="color_tab";
        btnNav.CssClass="color_tab";
        btnPol.CssClass="color_tab";
        btnMoor.CssClass="color_tab";
        btnEF.CssClass="color_tab";
        btnDP.CssClass="color_tab";
        btnFire.CssClass="color_tab";
        btnSec.CssClass = "color_tab";
        btnDoc.CssClass = "color_tab";
        btnEmailBlast.CssClass = "color_tab";
        btnRCA.CssClass = "color_tab";

        Button b = ((Button)sender);
        this.FindControl(b.CommandArgument).Visible = true;

        b.CssClass = "color_tab_sel";

        //if (ReportId > 0)
        //{
        //    btnSaveDraft_Click(sender, e);
        //}
        //else
        //{
        //    ShowNotice();
        //}

        //ShowNotice();
    }
    private void ShowNotice()
    {

        // ---------------------------------
        bool Error_btnHome = false;
        bool Error_btnCOA = false;
        bool Error_btnCA = false;
        bool Error_btnINJ = false;
        bool Error_btnCC = false;
        bool Error_btnNav = false;
        bool Error_btnPol = false;
        bool Error_btnMoor = false;
        bool Error_btnEF = false;
        bool Error_btnDP = false;
        bool Error_btnFire = false;
        bool Error_btnSec = false;


        //-- Home ------------------------------------------------------------

        // Common & Section 1 & Section 2
        if (txtPort.Text.Trim() == "" || ddlLat1.SelectedIndex <= 0 || ddlLat2.SelectedIndex <= 0 || ddlLong1.SelectedIndex <= 0 || ddlLong2.SelectedIndex <= 0 || txtReportDate.Text.Trim() == "" || txtIncidentDate.Text.Trim() == "" || txtsec1EventDesc.Text.Trim() == "")
            Error_btnHome = true;

        //if (rdosec1SOA.SelectedIndex < 0)
        if (lblsec1SOA.Text.Trim() == "")
            Error_btnHome = true;

        Error_btnHome = IsINValid(chksec1COA);
        // Section 3
        if (!radsec3PIyes.Checked && !radsec3PIno.Checked)
            Error_btnHome = true;
        if (rdosec3VA.SelectedIndex < 0)
            Error_btnHome = true;
        if (!radsec3VDyes.Checked && !radsec3VDno.Checked)
            Error_btnHome = true;
        if (!radsec3BVyes.Checked && !radsec3BVno.Checked)
            Error_btnHome = true;
        if (!radsec3RVyes.Checked && !radsec3RVno.Checked)
            Error_btnHome = true;

        //-- 13 : Cause of Accident ------------------------------------------------------------

        if (IsINValid(chklstsec13HA, txtsec13HAOther) && IsINValid(chklstsec13Cond, txtsec13CondOther))
            Error_btnCOA = true;
        if (IsINValid(chklstsec13HF, txtsec13HFOther) && IsINValid(chklstsec13JF, txtsec13JFOther))
            Error_btnCOA = true;

        //-- 14 : Corrective Actions  ------------------------------------------------------------

        if (!radsec14IAyes.Checked && !radsec14IAno.Checked)
            Error_btnCA = true;
        if (txtsec14IAYesDesc.Text.Trim() == "")
            Error_btnCA = true;

        if (!radsec14PRyes.Checked && !radsec14PRno.Checked)
            Error_btnCA = true;
        if (txtsec14PrYesRADesc.Text.Trim() == "")
            Error_btnCA = true;

        if (!radsec14ITyes.Checked && !radsec14ITNo.Checked)
            Error_btnCA = true;
        if (txtsec14ITDetails.Text.Trim() == "")
            Error_btnCA = true;

        if (!radsec14FTyes.Checked && !radsec14FTno.Checked)
            Error_btnCA = true;
        if (txtsec14FTDetails.Text.Trim() == "")
            Error_btnCA = true;

        if (!radsec14SMyes.Checked && !radsec14SMno.Checked)
            Error_btnCA = true;

        if (radsec14SMyes.Checked)
        if (txtsec14SMMeetingDate.Text.Trim() == "")
            Error_btnCA = true;

        if (!radsec14SIyes.Checked && !radsec14SIno.Checked)
            Error_btnCA = true;
        if (txtsec14SIyesDetails.Text.Trim() == "")
            Error_btnCA = true;

        //-- 4 : Injury to Personal  ------------------------------------------------------------

        if (chksec1COA.Items.FindByValue("1").Selected)
        {
            string SQL = "SELECT * FROM [DBO].[ER_S115_InjuryToPerson] WHERE [VesselCode]= '" + VesselCode + "' AND [ReportId] = " + ReportId.ToString();
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
            if (dt.Rows.Count <= 0)
            {
                Error_btnINJ = true;
            }
        }

        //-- 5 : Cargo Contamination ------------------------------------------------------------

        if (chksec1COA.Items.FindByValue("4").Selected)
        {
            if (!radsec5ccdyes.Checked && !radsec5ccdno.Checked)
                Error_btnCC = true;

            if (!radsec5cqdyes.Checked && !radsec5cqdno.Checked)
                Error_btnCC = true;

            if (radsec5ccdyes.Checked)
            {
                if (txtsec5Nameofcharterer.Text.Trim() == "")
                    Error_btnCC = true;

                if (txtsec5TypeofCargo.Text.Trim() == "")
                    Error_btnCC = true;

                if (txtsec5TankHoldNo.Text.Trim() == "")
                    Error_btnCC = true;

                if (IsINValid(chklstsec5TankCoating, txtsec5TankCoatingOther))
                    Error_btnCC = true;

                if (txtsec5LoadPort.Text.Trim() == "" && txtsec5DP.Text.Trim() == "" && (!radsec5stsyes.Checked))
                    Error_btnCC = true;
            }
            if (radsec5cqdyes.Checked)
            {
                if (IsINValid(chklstsec5CargoContaminant))
                    Error_btnCC = true;

                if (chklstsec5CargoQty.SelectedIndex < 0)
                    Error_btnCC = true;

                if (!radsec5TankInspectedyes.Checked && !radsec5TankInspectedno.Checked)
                    Error_btnCC = true;

                if (!radsec5WallWashyes.Checked && !radsec5WallWashno.Checked)
                    Error_btnCC = true;

            }
        }

        //-- 6 : Navigation ------------------------------------------------------------

        if (chksec1COA.Items.FindByValue("6").Selected)
        {
            if (IsINValid(chklstsec6))
                Error_btnNav = true;
        }

        //-- 7 : Pollution ------------------------------------------------------------

        if (chksec1COA.Items.FindByValue("8").Selected)
        {
            if (IsINValid(chklstsec7EP, txtsec7OtherEP))
                Error_btnPol = true;

            if (!radsec7lossyes.Checked && !radsec7lossno.Checked)
                Error_btnPol = true;

            if (radsec7lossyes.Checked)
            {
                if (rblsec7LTE.SelectedIndex < 0)
                    Error_btnPol = true;
            }

            if (!radsec7AAyes.Checked && !radsec7AAno.Checked)
                Error_btnPol = true;

            if (radsec7AAyes.Checked)
            {
                if (txtsec7date1.Text.Trim() == "" || txtsec7AutName.Text.Trim() == "")
                    Error_btnPol = true;
            }
        }

        //-- 8 : Mooring ------------------------------------------------------------

        if (chksec1COA.Items.FindByValue("2").Selected)
        {
            if (IsINValid(chklstsec8))
                Error_btnMoor = true;
        }

        //-- 9 : Equipment Failure ------------------------------------------------------------

        if (chksec1COA.Items.FindByValue("5").Selected)
        {
            if (!radsec9CEyes.Checked && !radsec9CEno.Checked)
                Error_btnEF = true;

            if (radsec9CEyes.Checked)
                if (IsINValid(chklstsec9EF))
                    Error_btnEF = true;
        }

        //-- 10 : Damage to Property ------------------------------------------------------------

        if (chksec1COA.Items.FindByValue("7").Selected)
        {
            if (!radsec10TPyes.Checked && !radsec10TPno.Checked)
                Error_btnDP = true;

            if (radsec10TPyes.Checked)
                if (IsINValid(chklstsec10TP))
                    Error_btnDP = true;

            if (!radsec10VEyes.Checked && !radsec10VEno.Checked)
                Error_btnDP = true;

            if (radsec10VEyes.Checked)
                if (IsINValid(chklstsec10VE))
                    Error_btnDP = true;

        }

        //-- 11 : Fire ------------------------------------------------------------

        if (chksec1COA.Items.FindByValue("9").Selected)
        {
            if (!radsec11Fireyes.Checked && !radsec11Fireno.Checked)
                Error_btnFire = true;

            if (radsec11Fireyes.Checked)
                if (IsINValid(chklstsec11FE))
                    Error_btnFire = true;
        }

        //-- 12 : Security ------------------------------------------------------------

        if (chksec1COA.Items.FindByValue("3").Selected)
        {
            if (IsINValid(chklstsec12))
                Error_btnSec = true;

        }

        dv_Notice.Visible = Error_btnHome || Error_btnCOA || Error_btnCA || Error_btnINJ || Error_btnCC || Error_btnNav || Error_btnPol || Error_btnMoor || Error_btnEF || Error_btnDP || Error_btnFire || Error_btnSec;
        // ---------------------------------
    }
    protected bool IsINValid(CheckBoxList rbl)
    {
        bool AnyChecked = false;
        foreach (ListItem li in rbl.Items)
        {
            if (li.Selected)
            {
                AnyChecked = true;
                break;
            }
        }
        return !AnyChecked;
    }
    protected bool IsINValid(CheckBoxList rbl, TextBox txtOther)
    {
        bool AnyChecked = false;
        foreach (ListItem li in rbl.Items)
        {
            if (li.Selected)
            {
                AnyChecked = true;
                break;
            }
        }
        if (!AnyChecked)
        {
            if (txtOther.Text.Trim() != "")
            {
                AnyChecked = true;
            }
        }
        return !AnyChecked;
    }
    protected void btnSaveDraft_Click(object sender, EventArgs e)
    {
       
        //ShowNotice();
        // Validations --------------------
        DateTime d;
        if (!radsec15IDyes.Checked && !radsec15IDno.Checked)
        {
            lblMsg.Text = "Please select '<b> YES / NO for INVESTIGATION DONE. </b>'.";
            btn_Tab_Click(btnOC, e);
            radsec15IDyes.Focus();
            return;
        }

        if (radsec15IDyes.Checked)
        {
            if (txtsec15InvDate.Text.Trim() == "")
            {
                lblMsg.Text = "Please enter investigation date.";
                btn_Tab_Click(btnOC, e);
                txtsec15InvDate.Focus();
                return;
            }
            if (!DateTime.TryParse(txtsec15InvDate.Text, out d))
            {
                lblMsg.Text = "Please enter valid date.";
                btn_Tab_Click(btnOC, e);
                txtsec15InvDate.Focus();
                return;
            }
            if (DateTime.Parse(txtsec15InvDate.Text) > DateTime.Today)
            {
                lblMsg.Text = "Investigation date can not be more than today.";
                btn_Tab_Click(btnOC, e);
                txtsec15InvDate.Focus();
                return;
            }
            if (txtsec15NameofPerson.Text.Trim() == "")
            {
                lblMsg.Text = "Please enter name of the person.";
                btn_Tab_Click(btnOC, e);
                txtsec15NameofPerson.Focus();
                return;
            }
        }

        if (!radsec15VDyes.Checked && !radsec15VDno.Checked)
        {
            lblMsg.Text = "Please select '<b> YES / NO for Vessel delayed. </b>'.";
            btn_Tab_Click(btnOC, e);
            radsec15VDyes.Focus();
            return;
        }
        if (radsec15VDyes.Checked)
        {
            if (txtsec15VDdays.Text.Trim() == "")
            {
                lblMsg.Text = "Please enter days.";
                btn_Tab_Click(btnOC, e);
                txtsec15VDdays.Focus();
                return;
            }

            int i;

            if(!(int.TryParse(txtsec15VDdays.Text.Trim(), out i)))
            {
                lblMsg.Text = "Please enter valid days.";
                btn_Tab_Click(btnOC, e);
                txtsec15VDdays.Focus();
                return;
            }
            if (txtsec15VDHrs.Text.Trim() == "")
            {
                lblMsg.Text = "Please enter hrs.";
                btn_Tab_Click(btnOC, e);
                txtsec15VDHrs.Focus();
                return;
            }
            if(!(int.TryParse(txtsec15VDHrs.Text.Trim(), out i)))
            {
                lblMsg.Text = "Please enter valid hrs.";
                btn_Tab_Click(btnOC, e);
                txtsec15VDHrs.Focus();
                return;
            }
        }

        if (rdosec15PPR.SelectedIndex == -1)
        {
            lblMsg.Text = "Please select Potential for Recurrence.";
            btn_Tab_Click(btnOC, e);
            rdosec15PPR.Focus();
            return; 
        }

        if (IsINValid(chklstsec15HA) && txtsec15HAOther.Text=="")
        {
            lblMsg.Text = "Please select at least one option in '<b>Human Actions</b>'.";
            btn_Tab_Click(btnOC, e);
            chklstsec15HA.Focus();
            return; 
        }

        if (IsINValid(chklstsec15Cond) && txtsec15CondOther.Text == "")
        {
            lblMsg.Text = "Please select at least one option in '<b>Conditions</b>'.";
            btn_Tab_Click(btnOC, e);
            chklstsec15Cond.Focus();
            return; 
        }

        if (IsINValid(chklstsec15HF) && txtsec15HFOther.Text == "")
        {
            lblMsg.Text = "Please select at least one option in '<b>Human Factors</b>'.";
            btn_Tab_Click(btnOC, e);
            chklstsec15HF.Focus();
            return; 
        }

        if (IsINValid(chklstsec15JF) && txtsec15JFOther.Text == "")
        {
            lblMsg.Text = "Please select at least one option in '<b>Job Factors</b>'.";
            btn_Tab_Click(btnOC, e);
            chklstsec15JF.Focus();
            return; 
        }
        if (txtsec15ReasonsForDiff.Text.Trim() != "")
        {
            if (txtsec15ReasonsForDiff.Text.Trim().Length > 500)
            {
                lblMsg.Text = "Please check! Reasons for Ship/Office Difference in RCA text can not more than 500 characters.";
                btn_Tab_Click(btnOC, e);
                txtsec15ReasonsForDiff.Focus();
                return;
            }
        }
        if (!radsec15FUyes.Checked && !radsec15FUno.Checked)
        {
            lblMsg.Text = "Please select '<b> YES / NO for FOLLOW UP REQUIRED. </b>'.";
            btn_Tab_Click(btnOC, e);
            radsec15FUyes.Focus();
            return;
        }
        
        if(radsec15FUyes.Checked)
        {
            if (txtsec15YesTargetDate.Text.Trim() == "")
            {
                lblMsg.Text = "Please enter target date.";
                btn_Tab_Click(btnOC, e);
                txtsec15YesTargetDate.Focus();
                return;
            }
            if (!DateTime.TryParse(txtsec15YesTargetDate.Text, out d))
            {
                lblMsg.Text = "Please enter valid date.";
                btn_Tab_Click(btnOC, e);
                txtsec15YesTargetDate.Focus();
                return;
            }
            if (txtsec15FUPIC.Text.Trim() == "")
            {
                lblMsg.Text = "Please enter PIC.";
                btn_Tab_Click(btnOC, e);
                txtsec15FUPIC.Focus();
                return;
            }
            
        }
        if (IsINValid(chksec15SFI))
        {
            lblMsg.Text = "Please select at least one option in '<b>SUGGESTIONS FOR IMPROVEMENT</b>'.";
            btn_Tab_Click(btnOC, e);
            chksec15SFI.Focus();
            return;
        }
        if (txtsec15Remarks.Text.Trim() != "")
        {
            if (txtsec15Remarks.Text.Trim().Length > 500)
            {
                lblMsg.Text = "Please check! Remarks text can not more than 500 characters.";
                btn_Tab_Click(btnOC, e);
                txtsec15Remarks.Focus();
                return;
            }
        }
        if(radsec15ICyes.Checked)
        {
            if (txtsec15CD.Text.Trim() == "")
            {
                lblMsg.Text = "Please enter close date.";
                btn_Tab_Click(btnOC, e);
                txtsec15CD.Focus();
                return;
            }
            if (!DateTime.TryParse(txtsec15CD.Text, out d))
            {
                lblMsg.Text = "Please enter valid date.";
                btn_Tab_Click(btnOC, e);
                txtsec15CD.Focus();
                return;
            }
            if (txtsec15CB.Text.Trim() == "")
            {
                lblMsg.Text = "Please enter closed by.";
                btn_Tab_Click(btnOC, e);
                txtsec15CB.Focus();
                return;
            }

            if (!radsec15CNyes.Checked && !radsec15CNno.Checked)
            {
                lblMsg.Text = "Please select '<b> YES / NO for VESSEL NOTIFIED OF INCIDENT CLOSURE. </b>'.";
                btn_Tab_Click(btnOC, e);
                radsec15CNyes.Focus();
                return;
            }

            if(radsec15CNyes.Checked)
            {
                if (txtsec15NotifyDate.Text.Trim() == "")
                {
                    lblMsg.Text = "Please enter notification date.";
                    btn_Tab_Click(btnOC, e);
                    txtsec15NotifyDate.Focus();
                    return;
                }
                if (!DateTime.TryParse(txtsec15NotifyDate.Text, out d))
                {
                    lblMsg.Text = "Please enter valid date.";
                    btn_Tab_Click(btnOC, e);
                    txtsec15NotifyDate.Focus();
                    return;
                }
            }
        }

        //---------------------

        
        string HA = getCheckedItems(chklstsec15HA);
        string Cond = getCheckedItems(chklstsec15Cond);
        string HF = getCheckedItems(chklstsec15HF);
        string JF = getCheckedItems(chklstsec15JF);
        string Suggesations = getCheckedItems(chksec15SFI);
        try
        {

            Common.Set_Procedures("[DBO].[ER_S115_InsertUpdate_ER_S115_Report_Office]");
            Common.Set_ParameterLength(32);
            Common.Set_Parameters(
                new MyParameter("@FORMNO", FormNo),
                new MyParameter("@REPORTID", ReportId),
                new MyParameter("@VESSELCODE", VesselCode),
                new MyParameter("@IS_VESSEL_DELAYED", (radsec15VDyes.Checked ? "Y" : (radsec15VDno.Checked ? "N" : ""))),
                new MyParameter("@VESSELDELAYED_DAYS", txtsec15VDdays.Text.Trim()),
                new MyParameter("@VESSELDELAYED_HOURS", txtsec15VDHrs.Text.Trim()),
                new MyParameter("@POTENTIAL_RECURRENCE", ((rdosec15PPR.SelectedIndex == -1) ? "0" : rdosec15PPR.SelectedValue)),
                new MyParameter("@IS_INVESTIGATIONDONE", (radsec15IDyes.Checked ? "Y" : (radsec15IDno.Checked ? "N" : ""))),
                new MyParameter("@INVESTIGATIONDATE", (txtsec15InvDate.Text.Trim() == "") ? DBNull.Value : (object)txtsec15InvDate.Text.Trim()),
                new MyParameter("@NAMEOFPERSON_ORG", txtsec15NameofPerson.Text.Trim()),
                new MyParameter("@O_HUMANACTIONS", HA),
                new MyParameter("@O_HUMANACTIONS_OTHER", txtsec15HAOther.Text),
                new MyParameter("@O_CONDITIONS", Cond),
                new MyParameter("@O_CONDITIONS_OTHER", txtsec15CondOther.Text),
                new MyParameter("@O_HUMANFACTORS", HF),
                new MyParameter("@O_HUMANFACTORS_OTHER", txtsec15HFOther.Text),
                new MyParameter("@O_JOBFACTORS", JF),
                new MyParameter("@O_JOBFACTORS_OTHER", txtsec15JFOther.Text),
                new MyParameter("@ReasonDiffRCA", txtsec15ReasonsForDiff.Text.Trim()),
                new MyParameter("@IS_FOLLOWUPREQ", (radsec15FUyes.Checked ? "Y" : (radsec15FUno.Checked ? "N" : ""))),
                new MyParameter("@TARGETDATE", (txtsec15YesTargetDate.Text.Trim() == "") ? DBNull.Value : (object)txtsec15YesTargetDate.Text.Trim()),
                new MyParameter("@FU_DETAILS", txtsec15YesDetails.Text.Trim()),
                new MyParameter("@FU_PIC", txtsec15FUPIC.Text.Trim()),
                new MyParameter("@SUGGESTIONS", Suggesations),
                new MyParameter("@REMARKS", txtsec15Remarks.Text.Trim()),
                new MyParameter("@IS_CLOSED", (radsec15ICyes.Checked ? "Y" : (radsec15ICno.Checked ? "N" : ""))),
                new MyParameter("@CLOSEDATE", (txtsec15CD.Text.Trim() == "") ? DBNull.Value : (object)txtsec15CD.Text.Trim()),
                new MyParameter("@CLOSEDBY", txtsec15CB.Text.Trim()),
                new MyParameter("@IS_CLOSURE_NOTIFIED", (radsec15CNyes.Checked ? "Y" : (radsec15CNno.Checked ? "N" : ""))),
                new MyParameter("@NOTIFYDATE", (txtsec15NotifyDate.Text.Trim() == "") ? DBNull.Value : (object)txtsec15NotifyDate.Text.Trim()), 
                new MyParameter("@CREATEDBY", UserName),
                new MyParameter("@UPDATEDBY", UserName)                
                );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                //if (ReportId == 0)
                //{
                //    ReportId = Common.CastAsInt32(ds.Tables[0].Rows[0]["REPORTID"].ToString());
                //    imgbtnUploadDocsShowPanel.Visible = true;
                //    spn_Note.Visible = true && dv_Notice.Visible;
                //    frmDocs.Attributes.Add("src", "../UploadDocuments.aspx?Key=" + FormNo + "&ReportId=" + ReportId.ToString());

                //    DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT ReportNo FROM [DBO].[ER_S115_Report] WHERE [ReportId] = " + ReportId.ToString());
                //    if (dt != null && dt.Rows.Count > 0)
                //    {
                //        lblReportNo.Text = dt.Rows[0]["ReportNo"].ToString();
                //    }
                //}
            
                btnINJ.Visible = chksec1COA.Items.FindByValue("1").Selected;// 1
                btnCC.Visible = chksec1COA.Items.FindByValue("4").Selected;// 4
                btnNav.Visible = chksec1COA.Items.FindByValue("6").Selected;// 6
                btnPol.Visible = chksec1COA.Items.FindByValue("8").Selected; // 8
                btnMoor.Visible = chksec1COA.Items.FindByValue("2").Selected; // 2
                btnEF.Visible = chksec1COA.Items.FindByValue("5").Selected; // 5
                btnDP.Visible = chksec1COA.Items.FindByValue("7").Selected;// 7
                btnFire.Visible = chksec1COA.Items.FindByValue("9").Selected;// 9
                btnSec.Visible = chksec1COA.Items.FindByValue("3").Selected; // 3

                lblMsg.Text = "Record saved successfully.";
            }
            else
            {
                lblMsg.Text = "Unable to save record. " + Common.getLastError();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to save record." + ex.Message;
        }
    }
    
    private string getCheckedItems(CheckBoxList chk)
    {
        string List = "";
        foreach (ListItem li in chk.Items)
        {
            if (li.Selected)
            {
                List = List + li.Value + ",";
            }
        }
        if(List != "")
        List = List.TrimEnd(',');

        return List;
    }

    protected void btnExportToShip_Click(object sender, EventArgs e)
    {
        //ShowNotice();
        //if (ReportId <= 0)
        //{
        //    lblMsg.Text = "Please first save the form to export.";
        //    txtReportDate.Focus();
        //    return;
        //}
        string ReportNoSerial = lblReportNo.Text.Trim().Substring(9);
        
        DataTable ER_S115_Report_Office = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [dbo].[ER_S115_Report_Office] WHERE VesselCode='" + VesselCode + "' AND REPORTID=" + ReportId.ToString());
        if (ER_S115_Report_Office.Rows.Count <= 0)
        {
            ProjectCommon.ShowMessage("Nothing to export. Please save record first.");
            return; 
        }
        DataTable ER_Report_Documents_Office = Common.Execute_Procedures_Select_ByQuery("select * from [dbo].[ER_Report_Documents_Office] WHERE VesselCode='" + VesselCode + "' AND FormNo='" + FormNo + "' AND  REPORTID=" + ReportId.ToString());
        DataTable ER_S115_Report = Common.Execute_Procedures_Select_ByQuery("select * from [dbo].[ER_S115_Report] WHERE VesselCode='" + VesselCode + "' AND  REPORTID=" + ReportId.ToString());
        DataTable ER_S115_InjuryToPerson = Common.Execute_Procedures_Select_ByQuery("select * from [dbo].[ER_S115_InjuryToPerson] WHERE VesselCode='" + VesselCode + "' AND  REPORTID=" + ReportId.ToString());
        //DataTable ER_Report_UpdationDetails = Common.Execute_Procedures_Select_ByQuery("select * from [dbo].[ER_Report_UpdationDetails] WHERE FORMNO='" + FormNo + "' AND REPORTID=" + ReportId.ToString());

        ER_S115_Report_Office.TableName = "ER_S115_Report_Office";
        ER_Report_Documents_Office.TableName = "ER_Report_Documents_Office";
        ER_S115_Report.TableName = "ER_S115_Report";
        ER_S115_InjuryToPerson.TableName = "ER_S115_InjuryToPerson";
        //ER_Report_UpdationDetails.TableName = "ER_Report_UpdationDetails";

        DataSet ds = new DataSet();
        ds.Tables.Add(ER_S115_Report.Copy());
        ds.Tables.Add(ER_S115_InjuryToPerson.Copy());
        ds.Tables.Add(ER_S115_Report_Office.Copy());
        ds.Tables.Add(ER_Report_Documents_Office.Copy());
        //ds.Tables.Add(ER_Report_UpdationDetails.Copy());

        string SchemaFile = Server.MapPath("SCHEMA_" + FormNo + ".xml");
        string DataFile = Server.MapPath("DATA_" + FormNo + ".xml");

        ds.WriteXml(DataFile);
        ds.WriteXmlSchema(SchemaFile);

        string ZipData = Server.MapPath("ER_O_" + VesselCode + "_" + ReportNoSerial + ".zip");
        if (File.Exists(ZipData)) { File.Delete(ZipData); }

        using (ZipFile zip = new ZipFile())
        {
            zip.AddFile(SchemaFile);
            zip.AddFile(DataFile);
            zip.Save(ZipData);
            Response.Clear();
            Response.ContentType = "application/zip";
            Response.AddHeader("Content-Type", "application/zip");
            Response.AddHeader("Content-Disposition", "inline;filename=" + Path.GetFileName(ZipData));
            Response.WriteFile(ZipData);
            Response.End();
        }
    }
    protected void btnExportDocsToShip_Click(object sender, EventArgs e)
    {
        //ShowNotice();
        //if (ReportId <= 0)
        //{
        //    lblMsg.Text = "Please first save the form to export.";
        //    txtReportDate.Focus();
        //    return;
        //}
        string ReportNoSerial = lblReportNo.Text.Trim().Substring(9);

        string DocumentsPath = Server.MapPath("~\\Modules\\LPSQE\\" + FormNo + "\\Documents\\Office\\");
        DataTable ER_Report_Documents_Office = Common.Execute_Procedures_Select_ByQuery("select '" + DocumentsPath + "' + FILENAME from [dbo].[ER_Report_Documents_Office] WHERE REPORTID=" + ReportId.ToString());
        ER_Report_Documents_Office.TableName = "ER_Report_Documents_Office";

        string ZipData = Server.MapPath("ER_O_" + VesselCode + "_FILES_" + ReportNoSerial + ".zip");
        if (File.Exists(ZipData)) { File.Delete(ZipData); }

        using (ZipFile zip = new ZipFile())
        {
            foreach (DataRow dr in ER_Report_Documents_Office.Rows)
            {
                if (File.Exists(dr[0].ToString()))
                {
                    zip.AddFile(dr[0].ToString());
                }
            }
            zip.Save(ZipData);
            Response.Clear();
            Response.ContentType = "application/zip";
            Response.AddHeader("Content-Type", "application/zip");
            Response.AddHeader("Content-Disposition", "inline;filename=" + Path.GetFileName(ZipData));
            Response.WriteFile(ZipData);
            Response.End();
        }
    }

    // --------------------------- SECTION 99 ( EMAIL BLAST )

    protected void BindEmailBlast()
    {
        string sql = "SELECT ROW_NUMBER() OVER (ORDER BY VESSELNAME) AS SRNO ,VESSELNAME,PacketSentDate,PacketRecievedDate,Comments,(CASE WHEN AccidentHappen IS NULL THEN '' WHEN AccidentHappen=1 THEN 'Yes' ELSE 'No' END) AS 'CHOYV',(CASE WHEN CAPATaken IS NULL THEN '' WHEN CAPATaken=1 THEN 'Yes' ELSE 'No' END) AS 'CPD' " +
                   "FROM DBO.VESSEL V  " +
                   "LEFT JOIN DBO.ER_S115_Report_EmailBlast B ON B.ACCVSLCODE=V.VESSELCODE AND B.VESSELCODE='" + VesselCode + "' AND B.REPORTID=" + ReportId + " " +
                   "WHERE  V.VESSELSTATUSID=1 ORDER BY VESSELNAME ";

        //                   "LEFT JOIN DBO.ER_S115_Report_EmailBlast B ON B.VESSELCODE='" + VesselCode + "' AND B.ACCVSLCODE=V.VESSELCODE AND B.REPORTID=" + ReportId + " " +

        rpt_99.DataSource = Common.Execute_Procedures_Select_ByQuery(sql);
        rpt_99.DataBind();
    }
}