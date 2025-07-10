using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Ionic.Zip;

public partial class eReports_S133_eReport_S133 : System.Web.UI.Page
{
    string FormNo = "D110";
    public int ReportId
    {
        get { return Common.CastAsInt32(ViewState["ReportId"]); }
        set { ViewState["ReportId"] = value; }
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
    protected void Page_Load(object sender, EventArgs e)
    {
        
        lblMsg.Text = "";
        lblMsgAttachments.Text = "";
        if (!IsPostBack)
        {
            VesselCode = Session["CurrentShip"].ToString();
            UserName = Session["FullName"].ToString();

            BindRank();
            BindDetails();
            //ShowReportDetails();
            
            //BindRanks();

            if (Request.QueryString["Key"] != null && Request.QueryString["Key"].ToString() != "")
            {
                ReportId = Common.CastAsInt32(Request.QueryString["Key"].ToString());
                ShowReportDetails();
                //btnSaveReport.Visible = (Request.QueryString["Type"].ToString() == "E");
                //btnExportToOffice.Visible = (Request.QueryString["Type"].ToString() == "E");
                tdTabs.Visible = (Request.QueryString["Type"].ToString() == "E");

                if (btnSaveReport.Visible)
                {
                    bool? Locked = null;
                    bool Exported = false;

                    DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT COUNT(*) FROM [dbo].[ER_D110_Report] WHERE VESSELCODE='" + VesselCode + "' AND REPORTID=" + ReportId.ToString() + " AND EXPORTEDON IS NOT NULL");
                    Exported = Common.CastAsInt32(dt.Rows[0][0]) > 0;
                    //DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("SELECT LOCKED FROM [dbo].[ER_S133_Report_Office] WHERE VESSELCODE='" + VesselCode + "' AND REPORTID=" + ReportId.ToString());
                    //if (dt1.Rows.Count <= 0)
                    //    Locked = null;
                    //else
                    //{
                    //    if (Convert.IsDBNull(dt1.Rows[0][0]))
                    //        Locked = null;
                    //    else
                    //        Locked = dt1.Rows[0][0].ToString().Trim().ToUpper() == "Y";
                    //}

                    //if (Locked == null)
                    //    btnSaveReport.Visible = !Exported;
                    //else
                    //    btnSaveReport.Visible = (Locked.Value == false);
                }
            }
        }
    }


    private void BindDetails()
    {
        lblFormName.Text = "Medical Report Form";
        string SQL = "SELECT ShipName FROM [DBO].Settings WHERE ShipCode ='" + VesselCode + "' ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        lblVesselName.Text = dt.Rows[0]["ShipName"].ToString();

        DataTable dtVersion = Common.Execute_Procedures_Select_ByQuery("Select [VersionNo] From [dbo].[ER_Master] WHERE [FormNo] = '" + FormNo + "'");
        if (dtVersion.Rows.Count > 0)
        {
            lblVersionNo.Text = dtVersion.Rows[0]["VersionNo"].ToString();
        }
    }
    private void BindRank()
    {
        string SQL = " Select RankID,RankName from MP_AllRank  order by RankName ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        ddlPopRank.DataSource = dt;
        ddlPopRank.DataTextField = "RankName";
        ddlPopRank.DataValueField= "RankID";
        ddlPopRank.DataBind();
        ddlPopRank.Items.Insert(0, new ListItem("Select", "0"));
    }
    private void BindAttachments()
    {
        string SQL = " Select * from ER_D110_Report_Attachments Where ReportID=" + ReportId+" ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        rptAttachments.DataSource = dt;
        rptAttachments.DataBind();
    }

    private void ShowReportDetails()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [DBO].[ER_D110_Report] WHERE [ReportId] = " + ReportId.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            BindAttachments();

            lblReportNo.Text = dr["ReportNo"].ToString();
            radProbType.SelectedValue = dr["INJURYILLNESS"].ToString();

            DateTime dtREPORTDATETIME = Convert.ToDateTime(dr["REPORTDATETIME"]);
            txtDateTimeOfReport.Text = Common.ToDateString(dtREPORTDATETIME);
            ddlDateTimeOfReportHour.SelectedValue = dtREPORTDATETIME.Hour.ToString();
            ddlDateTimeOfReportMinut.SelectedValue = dtREPORTDATETIME.Minute.ToString();

            if (dr["SHIPLOCATION"].ToString() == "I")
            {
                radInPort.Checked = true;
            }
            else
            {
                radAtSea.Checked = true;
            }
            InPortAtSeaEnableDisableControls();

            ddlLattitude1.SelectedValue = dr["LATTITUDE1"].ToString();
            ddlLattitude2.SelectedValue = dr["LATTITUDE2"].ToString();
            ddlLattitude3.SelectedValue = dr["LATTITUDE3"].ToString();


            ddlLongitude1.SelectedValue = dr["LONGITUDE1"].ToString();
            ddlLongitude2.SelectedValue = dr["LONGITUDE2"].ToString();
            ddlLongitude3.SelectedValue = dr["LONGITUDE3"].ToString();

            txtDestination.Text = dr["DESTINATION"].ToString();

            DateTime dtDESTINATIONETA = Convert.ToDateTime(dr["DESTINATIONETA"]);
            txtDestinationETA.Text = Common.ToDateString(dtDESTINATIONETA);
            ddlDestinationETAHour.SelectedValue = dtDESTINATIONETA.Hour.ToString();
            ddlDestinationETAMinut.SelectedValue = dtDESTINATIONETA.Minute.ToString();

            txtNameOfPort.Text = dr["PORTNAME"].ToString();
            txtEtaInport.Text = Common.ToDateString(dr["PORTETA"].ToString());
            txtOnShoreAgentName.Text = dr["SHOREAGENTNAME"].ToString();
            txtOnShoreAgentAddress.Text = dr["SHOREAGENTADDRESS"].ToString();

            txtPopFirstName.Text = dr["POPFIRSTNAME"].ToString();
            txtPopSirName.Text = dr["POPSURNAME"].ToString();
            txtPopSSID.Text = dr["POPSSID"].ToString();
            ddlPopRank.SelectedValue = dr["POPRANK"].ToString();
            txtPopDOB.Text = Common.ToDateString(dr["POPDOB"].ToString());
            txtPopNationality.Text = dr["POPNATIONALITY"].ToString();

            DateTime dtDateOfInjury = Convert.ToDateTime(dr["INJURYDATETIME"]);
            txtDateOfInjury.Text = Common.ToDateString(dtDateOfInjury);
            ddlHourOfInjury.SelectedValue = dtDateOfInjury.Hour.ToString();
            ddlMinuteOfInjury.SelectedValue = dtDateOfInjury.Minute.ToString();

            DateTime dtDateOfExaminationOnBoard = Convert.ToDateTime(dr["FIRSTTREATMENTDATETIME"]);
            txtDateOfExaminationOnBoard.Text = Common.ToDateString(dtDateOfExaminationOnBoard);
            ddlHourOfExaminationOnBoard.SelectedValue = dtDateOfExaminationOnBoard.Hour.ToString();
            ddlMinuteOfExaminationOnBoard.SelectedValue = dtDateOfExaminationOnBoard.Minute.ToString();

            txtShipLocationWhenInjuryOccurred.Text = dr["INJURYLOCATIONONBOARD"].ToString();
            txtCircumstancesOfIllenessOrInjury.Text = dr["CIRCUMSTANCESOFILLENESSORINJURY"].ToString();
            ddlIsRepearIllnessOrInjury.SelectedValue = (dr["REPEATILLNESSORINJURY"].ToString()=="True")?"1":"0";
            txtPhysicalExamintaion.Text = dr["FINDINGOFPHYSICALEXAMINATION"].ToString();
            ddlTratmentGivenOnboard.SelectedValue = (dr["TREATMENTGIVENONBOARD"].ToString()=="True")?"1":"0";
            DetailsOfTreatmentGiven.Text = dr["DETAILSOFTREATMENTONBOARD"].ToString();

            txtClinicalDiagnosis.Text = dr["CLINICALDIAGNOSIS"].ToString();
            txtDetailsOfTreatmentOrExamination.Text = dr["DETAILSOFTREATMENTONCLINIC"].ToString();
            ddlPatientIsDeclared.SelectedValue = dr["PATIENTISDECLARED"].ToString();
            txtUnfitFrom.Text = Common.ToDateString(dr["PATIENTUNFITFROM"].ToString());
            txtUnfitTo.Text = Common.ToDateString(dr["PATIENTUNFITTO"].ToString());
            txtNameOfConsultation.Text = dr["CLINICNAME"].ToString();
            txtAddressOfConsultation.Text = dr["CLINICADDRESS"].ToString();
            txtDateOfConsultation.Text = Common.ToDateString(dr["DATEOFCONSULTATION"].ToString());
            txtDoctotName.Text = dr["NAMEDOCTOR"].ToString();
            //---------------------------
            //", .Text + " " + ddlHourOfInjury.SelectedValue + ":" + ddlMinuteOfInjury.SelectedValue),
            //", .Text + " " + ddlHourOfExaminationOnBoard.SelectedValue + ":" + ddlMinuteOfExaminationOnBoard.SelectedValue),
        }

    }
    
    protected void btnDelDocument_Click(object sender, EventArgs e)
    {
        int DocId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        Common.Execute_Procedures_Select_ByQuery("UPDATE [DBO].ER_Report_Documents_Ship SET STATUS='D' WHERE VESSELCODE='" + VesselCode + "' AND FORMNO='" + FormNo + "' AND DOCID=" + DocId.ToString());
    }
    protected void radAtSea_OnCheckedChanged(object sender, EventArgs e)
    {
        InPortAtSeaEnableDisableControls();
    }

    private void InPortAtSeaEnableDisableControls()
    {
        ddlLattitude1.SelectedIndex = 0; ddlLattitude2.SelectedIndex = 0; ddlLattitude3.SelectedIndex = 0;
        ddlLongitude1.SelectedIndex = 0; ddlLongitude1.SelectedIndex = 0; ddlLongitude1.SelectedIndex = 0;
        txtDestination.Text = "";
        txtDestinationETA.Text = "";
        ddlDestinationETAHour.SelectedIndex = 0;
        ddlDestinationETAMinut.SelectedIndex = 0;

        txtNameOfPort.Text = ""; txtEtaInport.Text = ""; txtOnShoreAgentName.Text = ""; txtOnShoreAgentAddress.Text = "";

        if (radAtSea.Checked)
        {
            ddlLattitude1.Enabled = true; ddlLattitude2.Enabled = true; ddlLattitude3.Enabled = true;
            ddlLongitude1.Enabled = true; ddlLongitude2.Enabled = true; ddlLongitude3.Enabled = true;
            txtDestination.Enabled = true;
            txtDestinationETA.Enabled = true;
            ddlDestinationETAHour.Enabled = true;
            ddlDestinationETAMinut.Enabled = true;

            txtNameOfPort.Enabled = false; txtEtaInport.Enabled = false;
            txtOnShoreAgentName.Enabled = false; txtOnShoreAgentAddress.Enabled = false;

            txtNameOfPort.Text = ""; txtEtaInport.Text = "";
            txtOnShoreAgentName.Text = ""; txtOnShoreAgentAddress.Text = "";
        }
        else
        {
            ddlLattitude1.Enabled = false; ddlLattitude2.Enabled = false; ddlLattitude3.Enabled = false;
            ddlLongitude1.Enabled = false; ddlLongitude2.Enabled = false; ddlLongitude3.Enabled = false;
            txtDestination.Enabled = false;
            txtDestinationETA.Enabled = false;
            ddlDestinationETAHour.Enabled = false;
            ddlDestinationETAMinut.Enabled = false;

            ddlLattitude1.SelectedIndex = 0; ddlLattitude2.SelectedIndex = 0; ddlLattitude3.SelectedIndex = 0;
            ddlLongitude1.SelectedIndex = 0; ddlLongitude2.SelectedIndex = 0; ddlLongitude3.SelectedIndex = 0;
            txtDestination.Text = "";
            txtDestinationETA.Text = "";
            ddlDestinationETAHour.SelectedIndex = 0;
            ddlDestinationETAMinut.SelectedIndex = 0;

            txtNameOfPort.Enabled = true; txtEtaInport.Enabled = true;
            txtOnShoreAgentName.Enabled = true; txtOnShoreAgentAddress.Enabled = true;
        }
    }

    protected void btnExportToOffice_Click(object sender, EventArgs e)
    {
        if (ReportId <= 0)
        {
            lblMsg.Text = "Please first save the form to export.";
            return;
        }

        try
        {
            Common.Set_Procedures("[DBO].[sp_Insert_Communication_Export]");
            Common.Set_ParameterLength(5);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", VesselCode),
                new MyParameter("@RecordType", "eForm - D110"),
                new MyParameter("@RecordId", ReportId),
                new MyParameter("@RecordNo", lblReportNo.Text.Trim()),
                new MyParameter("@CreatedBy", Session["FullName"].ToString().Trim())
            );

            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    lblMsg.Text = ds.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    lblMsg.Text = "Sent for export successfully.";
                }
            }
            else
            {
                lblMsg.Text = "Unable to send for export.Error : " + Common.getLastError();

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to send for export.Error : " + ex.Message;
        }

        //DataTable ER_S133_Report = Common.Execute_Procedures_Select_ByQuery("select * from [dbo].[ER_S133_Report] WHERE REPORTID=" + ReportId.ToString());
        //DataTable ER_Report_UpdationDetails = Common.Execute_Procedures_Select_ByQuery("select * from [dbo].[ER_Report_UpdationDetails] WHERE FORMNO='" + FormNo + "' AND REPORTID=" + ReportId.ToString());

        //ER_S133_Report.TableName = "ER_S133_Report";
        //ER_Report_UpdationDetails.TableName = "ER_Report_UpdationDetails";

        //DataSet ds = new DataSet();
        //ds.Tables.Add(ER_S133_Report.Copy());
        //ds.Tables.Add(ER_Report_UpdationDetails.Copy());

        //string SchemaFile = Server.MapPath("SCHEMA_" + FormNo + ".xml");
        //string DataFile = Server.MapPath("DATA_" + FormNo + ".xml");

        //ds.WriteXml(DataFile);
        //ds.WriteXmlSchema(SchemaFile);

        ////string ZipData = Server.MapPath("ER_S_" + VesselCode + "_" + FormNo + ".zip");
        //string ZipData = Server.MapPath("ER_S_" + VesselCode + "_" + FormNo + ReportId.ToString().PadLeft(5, '0') + ".zip");
        //if (File.Exists(ZipData)) { File.Delete(ZipData); }

        //try
        //{
        //    using (ZipFile zip = new ZipFile())
        //    {
        //        zip.AddFile(SchemaFile);
        //        zip.AddFile(DataFile);
        //        zip.Save(ZipData);
        //        Response.Clear();
        //        Response.ContentType = "application/zip";
        //        Response.AddHeader("Content-Type", "application/zip");
        //        Response.AddHeader("Content-Disposition", "inline;filename=" + Path.GetFileName(ZipData));
        //        Response.WriteFile(ZipData);
        //        Common.Execute_Procedures_Select_ByQuery("UPDATE [dbo].[ER_S133_Report] SET EXPORTEDBY='" + UserName + "',EXPORTEDON=GETDATE() WHERE REPORTID=" + ReportId.ToString());
        //        Response.End();
        //    }
        //}
        //catch { }
    }
    protected void btnSaveReport_Click(object sender, EventArgs e)
    {

        // Validations --------------------
        if (radProbType.SelectedIndex < 0)
        {
            lblMsg.Text = " Please select Injury or Illness.";
            radProbType.Focus();
            return;
        }

        //if (txtDateTimeOfReport.Text.Trim()=="")
        //{
        //    lblMsg.Text = " Please enter report date.";
        //    txtDateTimeOfReport.Focus();
        //    return;
        //}
        if (txtDateTimeOfReport.Text.Trim() == "" || ddlDateTimeOfReportHour.SelectedIndex == 0 || ddlDateTimeOfReportMinut.SelectedIndex == 0)
        {
            lblMsg.Text = " Please enter report date and time.";
            txtDateTimeOfReport.Focus();
            return;
        }

        if (radAtSea.Checked==false && radInPort.Checked == false)
        {
            lblMsg.Text = " Please select Ship's Location.";            
            return;
        }
        if (radAtSea.Checked == true)
        {
            if (ddlLattitude1.SelectedIndex == 0 || ddlLattitude2.SelectedIndex == 0 || ddlLattitude3.SelectedIndex == 0)
            {
                lblMsg.Text = " Please select Latitude.";
                return;
            }
            if (ddlLongitude1.SelectedIndex == 0 || ddlLongitude2.SelectedIndex == 0 || ddlLongitude3.SelectedIndex == 0)
            {
                lblMsg.Text = " Please select Longitude.";
                return;
            }
            if (txtDestination.Text.Trim() == "")
            {
                lblMsg.Text = " Please select destination.";
                txtDestination.Focus();
                return;
            }
            //if (txtDestinationETA.Text.Trim() == "")
            //{
            //    lblMsg.Text = " Please select ETA At Sea.";
            //    txtDestinationETA.Focus();
            //    return;
            //}
            if (txtDestinationETA.Text.Trim() == "" || ddlDestinationETAHour.SelectedIndex == 0 || ddlDestinationETAMinut.SelectedIndex == 0)
            {
                lblMsg.Text = " Please select ETA At Sea date and time.";
                txtDestinationETA.Focus();
                return;
            }

        }
        if (radInPort.Checked == true)
        {
            if (txtNameOfPort.Text.Trim() == "")
            {
                lblMsg.Text = " Please enter name of In Port.";
                txtNameOfPort.Focus();
                return;
            }
            if (txtEtaInport.Text.Trim() == "")
            {
                lblMsg.Text = " Please select ETA In Port.";
                txtEtaInport.Focus();
                return;
            }
            if (txtOnShoreAgentName.Text.Trim() == "")
            {
                lblMsg.Text = " Please enter on shore agent name.";
                txtOnShoreAgentName.Focus();
                return;
            }
            if (txtOnShoreAgentAddress.Text.Trim() == "")
            {
                lblMsg.Text = " Please enter On shore agent address.";
                txtOnShoreAgentAddress.Focus();
                return;
            }
        }

        if (txtPopFirstName.Text.Trim() == "")
        {
            lblMsg.Text = " Please enter patient name.";
            txtPopFirstName.Focus();
            return;
        }
        if (txtPopSirName.Text.Trim() == "")
        {
            lblMsg.Text = " Please enter patient sur name.";
            txtPopSirName.Focus();
            return;
        }
        if (txtPopSSID.Text.Trim() == "")
        {
            lblMsg.Text = " Please enter SSID.";
            txtPopSSID.Focus();
            return;
        }
        if (ddlPopRank.SelectedIndex== 0)
        {
            lblMsg.Text = " Please select rank.";
            ddlPopRank.Focus();
            return;
        }
        if (txtPopDOB.Text.Trim() == "")
        {
            lblMsg.Text = " Please enter patient DOB.";
            txtPopDOB.Focus();
            return;
        }
        if (txtPopNationality.Text.Trim() == "")
        {
            lblMsg.Text = " Please enter Nationality.";
            txtPopNationality.Focus();
            return;
        }
        if (txtDateOfInjury.Text.Trim() == "" || ddlHourOfInjury.SelectedIndex==0|| ddlMinuteOfInjury.SelectedIndex == 0)
        {
            lblMsg.Text = " Please enter Hour and date of injury or onset of illness properly.";
            txtDateOfInjury.Focus();
            return;
        }
        if (txtDateOfExaminationOnBoard.Text.Trim() == "" || ddlHourOfExaminationOnBoard.SelectedIndex == 0 || ddlMinuteOfExaminationOnBoard.SelectedIndex == 0)
        {
            lblMsg.Text = " Please enter Hour and date of first examination/treatment on board properly.";
            txtDateOfExaminationOnBoard.Focus();
            return;
        }
        if (txtShipLocationWhenInjuryOccurred.Text.Trim() == "")
        {
            lblMsg.Text = " Please enter Location on board ship where injury occurred(if applicable).";
            txtShipLocationWhenInjuryOccurred.Focus();
            return;
        }
        if (txtCircumstancesOfIllenessOrInjury.Text.Trim() == "")
        {
            lblMsg.Text = " Please enter Circumstances Of Illeness Or Injury.";
            txtCircumstancesOfIllenessOrInjury.Focus();
            return;
        }

        if (ddlIsRepearIllnessOrInjury.SelectedIndex==0)
        {
            lblMsg.Text = " Please select Is this a repeate illness/injury.";
            ddlIsRepearIllnessOrInjury.Focus();
            return;
        }
        if (txtPhysicalExamintaion.Text.Trim() == "")
        {
            lblMsg.Text = " Please enter Findings of physical examination and symptoms.";
            txtPhysicalExamintaion.Focus();
            return;
        }
        if (ddlTratmentGivenOnboard.SelectedIndex == 0)
        {
            lblMsg.Text = " Please enter Tratment given on board.";
            ddlTratmentGivenOnboard.Focus();
            return;
        }
        if (ddlTratmentGivenOnboard.SelectedIndex == 1)
        {
            if (DetailsOfTreatmentGiven.Text.Trim()=="")
            {
                lblMsg.Text = " Please enter Details of treatment given.";
                DetailsOfTreatmentGiven.Focus();
                return;
            }
        }

        //---------------------------------------------------------------------------------------------------------

        try
        {
            string shipLocaiton = ((radAtSea.Checked) ? "S" : "I");
            string INJURYDATETIME = txtDateOfInjury.Text;

            object objFromDate;
            object objToDate;
            object objDateOfConsultation;
            
            if (txtUnfitFrom.Text.Trim() == "")
                objFromDate = DBNull.Value;
            else
                objFromDate = txtUnfitFrom.Text.Trim();


            if (txtUnfitTo.Text.Trim() == "")
                objToDate = DBNull.Value;
            else
                objToDate = txtUnfitTo.Text.Trim();

            if (txtDateOfConsultation.Text.Trim() == "")
                objDateOfConsultation = DBNull.Value;
            else
                objDateOfConsultation = txtDateOfConsultation.Text.Trim();
            //------------------------------------------------------------------
            Common.Set_Procedures("sp_IU_ER_D110_Report");
            Common.Set_ParameterLength(43);
            Common.Set_Parameters(
                                    new MyParameter("@FORMNO", FormNo),
                                    new MyParameter("@REPORTID", ReportId),
                                    new MyParameter("@VESSELCODE", VesselCode),
                                    new MyParameter("@INJURYILLNESS", radProbType.SelectedValue),
                                    
                                    //new MyParameter("@REPORTDATETIME", txtDateTimeOfReport.Text.Trim()),
                                    new MyParameter("@REPORTDATETIME", txtDateTimeOfReport.Text + " " + Common.CastAsInt32(ddlDateTimeOfReportHour.SelectedValue) + ":" + Common.CastAsInt32(ddlDateTimeOfReportMinut.SelectedValue)),

                                    new MyParameter("@SHIPLOCATION", shipLocaiton),
                                    new MyParameter("@LATTITUDE1", Common.CastAsInt32(ddlLattitude1.SelectedValue)),
                                    new MyParameter("@LATTITUDE2", Common.CastAsInt32(ddlLattitude2.SelectedValue)),
                                    new MyParameter("@LATTITUDE3", ddlLattitude3.SelectedValue),
                                    new MyParameter("@LONGITUDE1", Common.CastAsInt32(ddlLongitude1.SelectedValue)),
                                    new MyParameter("@LONGITUDE2", Common.CastAsInt32(ddlLongitude2.SelectedValue)),
                                    new MyParameter("@LONGITUDE3", ddlLongitude3.SelectedValue),
                                    new MyParameter("@DESTINATION", txtDestination.Text.Trim()),
                                    
                                    //new MyParameter("@DESTINATIONETA", txtDestinationETA.Text.Trim()),
                                    new MyParameter("@DESTINATIONETA", txtDestinationETA.Text + " " + Common.CastAsInt32(ddlDestinationETAHour.SelectedValue) + ":" + Common.CastAsInt32(ddlDestinationETAMinut.SelectedValue)),

                                    new MyParameter("@PORTNAME", txtNameOfPort.Text),
                                    new MyParameter("@PORTETA", txtEtaInport.Text),
                                    new MyParameter("@SHOREAGENTNAME", txtOnShoreAgentName.Text),
                                    new MyParameter("@SHOREAGENTADDRESS", txtOnShoreAgentAddress.Text),
                                    new MyParameter("@POPMRNO", 0), //???????
                                    new MyParameter("@POPFIRSTNAME", txtPopFirstName.Text),
                                    new MyParameter("@POPSURNAME", txtPopSirName.Text),
                                    new MyParameter("@POPSSID", txtPopSSID.Text),
                                    new MyParameter("@POPRANK", Common.CastAsInt32(ddlPopRank.SelectedValue)),
                                    new MyParameter("@POPDOB", txtPopDOB.Text),
                                    new MyParameter("@POPNATIONALITY", txtPopNationality.Text),

                                    new MyParameter("@INJURYDATETIME", txtDateOfInjury.Text+" "+ Common.CastAsInt32(ddlHourOfInjury.SelectedValue)+":"+ Common.CastAsInt32(ddlMinuteOfInjury.SelectedValue) ),
                                    new MyParameter("@FIRSTTREATMENTDATETIME", txtDateOfExaminationOnBoard.Text + " " + Common.CastAsInt32(ddlHourOfExaminationOnBoard.SelectedValue) + ":" + Common.CastAsInt32(ddlMinuteOfExaminationOnBoard.SelectedValue)),
                                    new MyParameter("@INJURYLOCATIONONBOARD", txtShipLocationWhenInjuryOccurred.Text),
                                    new MyParameter("@CIRCUMSTANCESOFILLENESSORINJURY", txtCircumstancesOfIllenessOrInjury.Text),

                                    new MyParameter("@REPEATILLNESSORINJURY", Common.CastAsInt32(ddlIsRepearIllnessOrInjury.SelectedValue)),
                                    new MyParameter("@FINDINGOFPHYSICALEXAMINATION", txtPhysicalExamintaion.Text),
                                    new MyParameter("@TREATMENTGIVENONBOARD", ddlTratmentGivenOnboard.SelectedValue),
                                    new MyParameter("@DETAILSOFTREATMENTONBOARD", DetailsOfTreatmentGiven.Text),

                                    new MyParameter("@CLINICALDIAGNOSIS", txtClinicalDiagnosis.Text),
                                    new MyParameter("@DETAILSOFTREATMENTONCLINIC", txtDetailsOfTreatmentOrExamination.Text),
                                    new MyParameter("@PATIENTISDECLARED", Common.CastAsInt32(ddlPatientIsDeclared.SelectedValue)),
                                    new MyParameter("@PATIENTUNFITFROM", objFromDate), // txtUnfitFrom.Text
                                    new MyParameter("@PATIENTUNFITTO", objToDate ),  //txtUnfitTo.Text
                                    new MyParameter("@CLINICNAME", txtNameOfConsultation.Text),
                                    new MyParameter("@CLINICADDRESS", txtAddressOfConsultation.Text),
                                    new MyParameter("@DATEOFCONSULTATION", objDateOfConsultation),
                                    new MyParameter("@NAMEDOCTOR", txtDoctotName.Text),
                                    new MyParameter("@UpdatedBy", UserName)
                                    

                );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                if (ReportId == 0)
                {
                    ReportId = Common.CastAsInt32(ds.Tables[0].Rows[0]["REPORTID"].ToString());
                    
                    DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT ReportNo FROM [DBO].[ER_D110_Report] WHERE [ReportId] = " + ReportId.ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        lblReportNo.Text = dt.Rows[0]["ReportNo"].ToString();
                    }
                }
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
        if (List != "")
            List = List.TrimEnd(',');

        return List;
    }

    protected void ddlTratmentGivenOnboard_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTratmentGivenOnboard.SelectedIndex != 1)
        {
            DetailsOfTreatmentGiven.Text = "";
        }
    }
    protected void btnAddFile_OnClick(object sender, EventArgs e)
    {
        if (ReportId <= 0)
        {
            lblMsgAttachments.Text = "Please save report first.";            
            return;
        }
        if (txtDocumentName.Text.Trim() == "")
        {
            lblMsgAttachments.Text = "Please enter Document name.";
            txtDocumentName.Focus();
            return;
        }

        if (fuFile.HasFile)
        {
            byte[] FileContent = fuFile.FileBytes;
            string FileName = fuFile.FileName;
            FileName = txtDocumentName.Text.Trim().Replace(" ","_")+"_"+FileName;

            try
            { 
            //------------------------------------------------------------------
            Common.Set_Procedures("sp_Insert_ER_D110_Report_Attachments");
            Common.Set_ParameterLength(4);
            Common.Set_Parameters(
                    new MyParameter("@ReportID", ReportId),
                    new MyParameter("@DocumentName", txtDocumentName.Text.Trim()),
                    new MyParameter("@FileName",FileName),                    
                    new MyParameter("@Attachment", FileContent)
                );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                    BindAttachments();
                    txtDocumentName.Text = "";
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
        else
        {
            lblMsgAttachments.Text = "Please select a file.";            
            return;
        }
    }
    protected void btnDownload_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;

        string sql = " Select * from ER_D110_Report_Attachments Where ID="+Common.CastAsInt32(btn.CommandArgument)+" ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt.Rows.Count > 0)
        {

            try
            {
                byte[] Content = (byte[])dt.Rows[0]["Attachment"];
                string FileName = dt.Rows[0]["FileName"].ToString();

                //MemoryStream ms = new MemoryStream();
                //TextWriter tw = new StreamWriter(ms);
                ////tw.WriteLine("YourString");
                ////tw.Flush();

                //ms.Close();
                Response.Clear();
                Response.ContentType = "application/force-download";
                Response.AddHeader("content-disposition", "attachment; filename=" + FileName + "");
                Response.BinaryWrite(Content);
                Response.End();
            }
            catch (Exception ex)
            {
                lblMsgAttachments.Text = "Unable to download file.";
            }
        }
    }


}