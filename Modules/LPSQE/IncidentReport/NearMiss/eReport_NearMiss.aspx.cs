using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Ionic.Zip;
using System.Configuration;
using System.Text;

public partial class eReports_S133_eReport_S133 : System.Web.UI.Page
{
    string FormNo = "NearMiss";
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
    protected void btnLockUnlock_Click(object sender, EventArgs e)
    {
        if (btnLockUnlock.Text.Trim().ToLower().Contains("unlock"))
        {
            Common.Execute_Procedures_Select_ByQuery("UPDATE [dbo].[ER_S133_Report_Office] SET Locked='N' WHERE VESSELCODE='" + VesselCode + "' AND REPORTID=" + ReportId.ToString());
        }
        else
        {
            Common.Execute_Procedures_Select_ByQuery("UPDATE [dbo].[ER_S133_Report_Office] SET Locked='Y' WHERE VESSELCODE='" + VesselCode + "' AND REPORTID=" + ReportId.ToString());
        }
        ShowLockUnlock();
    }
    protected void ShowLockUnlock()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [DBO].[ER_S133_Report_Office] WHERE VESSELCODE='" + VesselCode + "' AND [ReportId] = " + ReportId.ToString());
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
                //btnExportToShip.Visible = (Request.QueryString["Type"].ToString() == "E");

                lnkICause.Visible = btnSaveDraft.Visible;
                lnkRCause.Visible = btnSaveDraft.Visible;

                //tdTabs.Visible = (Request.QueryString["Type"].ToString() == "E");
            }

            string[] Files = System.IO.Directory.GetFiles(Server.MapPath("~/Modules/LPSQE/IncidentReport/NearMiss"), "*.zip");
            foreach (string fl in Files)
            {
                System.IO.File.Delete(fl);
            }
        }
    }
    private void BindDetails()
    {
        lblFormName.Text = "NEAR MISS (HAZARDOUS OCCURRENCE) REPORT";
        string SQL = "SELECT VESSELName FROM [DBO].Vessel WHERE VESSELCode ='" + VesselCode + "' and VesselId in (Select vw.VesselId from UserVesselRelation vw with(nolock)  where vw.Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ") ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        lblVesselName.Text = dt.Rows[0]["VESSELName"].ToString();
    }
    private void BindRegisters()
    {
        ProjectCommon.LoadRegisters(cblCategory, FormNo, 20);
        ProjectCommon.LoadRegisters(chklstsec13HA, "Accident", 13);
        ProjectCommon.LoadRegisters(chklstsec13Cond, "Accident", 14);
        ProjectCommon.LoadRegisters(chklstsec13HF, "Accident", 15);
        ProjectCommon.LoadRegisters(chklstsec13JF, "Accident", 16);
    }
    private void ShowReportDetails()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [DBO].[ER_S133_Report] WHERE VESSELCODE='" + VesselCode + "' AND [ReportId] = " + ReportId.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            //------------------ Main Details ----------------------
            lblReportNo.Text = dt.Rows[0]["ReportNo"].ToString();

            txtOccurenceDt.Text = Convert.ToDateTime(dt.Rows[0]["DateOfOccurence"]).ToString("dd-MMM-yyyy");
            txtFamilyName.Text = dt.Rows[0]["FamilyName"].ToString();
            txtFirstName.Text = dt.Rows[0]["FirstName"].ToString();
            ddlRank.SelectedValue = dt.Rows[0]["RankId"].ToString();
            txtCrewNo.Text = dt.Rows[0]["CrewNo"].ToString();
            ProjectCommon.SetCheckboxListData(dt.Rows[0]["Category"].ToString(), cblCategory);
            txt_SM_FamilyName.Text = dt.Rows[0]["SM_FamilyName"].ToString();
            txt_SM_FirstName.Text = dt.Rows[0]["SM_FirstName"].ToString();
            txt_SM_CrewNo.Text = dt.Rows[0]["SM_CrewNo"].ToString();

            //------------------------------------------------------

            //------------------  Section 1 : Severity & Classification ---------------

            txtEventDesc.Text = dt.Rows[0]["EventDescr"].ToString();

            //-------------------------------------------------------------------------

            //------------------  Section 2 : Event Description ---------------

            txtSuggestions.Text = dt.Rows[0]["Suggestions"].ToString();

            //-------------------------------------------------------------------------
        }

        dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [DBO].[ER_S133_Report_Office]  WHERE VESSELCODE='" + VesselCode + "' AND [ReportId] = " + ReportId.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            //------------------  Section 3 : Office Comments ---------------

            txtOfficeComments.Text = dt.Rows[0]["Remarks"].ToString();

            radsec15ICyes.Checked = (dt.Rows[0]["Is_Closed"].ToString() == "Y");
            radsec15ICno.Checked = ((dt.Rows[0]["Is_Closed"].ToString() == "N") || (dt.Rows[0]["Is_Closed"].ToString().Trim() == ""));

            radsec15ICyes_OnCheckedChanged(new object(), new EventArgs());
            txtsec15CD.Text = dt.Rows[0]["CloseDate"].ToString() == "" ? "" : Convert.ToDateTime(dt.Rows[0]["CloseDate"]).ToString("dd-MMM-yyyy");
            txtsec15CB.Text = dt.Rows[0]["ClosedBy"].ToString();

            radsec15CNyes.Checked = (dt.Rows[0]["Is_Closure_Notified"].ToString() == "Y");
            radsec15CNno.Checked = (dt.Rows[0]["Is_Closure_Notified"].ToString() == "N");
            radsec15CNyes_OnCheckedChanged(new object(), new EventArgs());
            txtsec15NotifyDate.Text = dt.Rows[0]["NotifyDate"].ToString() == "" ? "" : Convert.ToDateTime(dt.Rows[0]["NotifyDate"]).ToString("dd-MMM-yyyy");

            ddlsec15INMSeverity.SelectedValue = dt.Rows[0]["NMSeverity"].ToString();
            chksec15INMSeveritySignificant_CheckedChanged(new object(), new EventArgs());

            //-------------------------------------------------------------------------
            BindRepeaterActions_Conditions();
            //-------------------------------------------------------------------------
        }
        ShowLockUnlock();
    }

    //protected void btnDelDocument_Click(object sender, EventArgs e)
    //{
    //    int DocId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
    //    Common.Execute_Procedures_Select_ByQuery("UPDATE [DBO].ER_Report_Documents_Ship SET STATUS='D' WHERE VESSELCODE='" + VesselCode + "' AND FORMNO='" + FormNo + "' AND DOCID=" + DocId.ToString());
    //}
    private void BindRanks()
    {
        string SQL = "SELECT RankId, RankCode FROM [DBO].[Rank]";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        ddlRank.DataSource = dt;
        ddlRank.DataValueField = "RankId";
        ddlRank.DataTextField = "RankCode";
        ddlRank.DataBind();

        ddlRank.Items.Insert(0, new ListItem("<-- Select -->", "0"));
    }

    // --------------------------- SECTION 3 ( Documents )

    protected void btn_Tab_Click(object sender, EventArgs e)
    {
        Div1.Visible = false;
        Div2.Visible = false;
        Div3.Visible = false;

        btnEventDescription.CssClass = "color_tab";
        btnSuggestions.CssClass = "color_tab";
        btnOfficeComments.CssClass = "color_tab";

        Button b = ((Button)sender);
        this.FindControl(b.CommandArgument).Visible = true;

        b.CssClass = "color_tab_sel";
    }

    protected void btnExportToShip_Click(object sender, EventArgs e)
    {
        if (ReportId <= 0)
        {
            lblMsg.Text = "Please first save the form to export.";
            return;
        }

        DataTable dtVesselEmail = Common.Execute_Procedures_Select_ByQuery("select EMAIL, VesselEmailNew from DBO.VESSEL WHERE VESSELCODE='" + VesselCode + "'");
        string EmailAddress = "";
        List<string> CCMails = new List<string>();
        List<string> BCCMails = new List<string>();
        if (dtVesselEmail.Rows.Count > 0)
        {
            EmailAddress = dtVesselEmail.Rows[0]["VesselEmailNew"].ToString();
            if (!string.IsNullOrWhiteSpace(dtVesselEmail.Rows[0]["EMAIL"].ToString()))
            {
                BCCMails.Add(dtVesselEmail.Rows[0]["EMAIL"].ToString());
            }

            DataTable dtLoginUser = Common.Execute_Procedures_Select_ByQuery("Select Email from UserLogin with(nolock) where LoginId = " + Convert.ToInt32(Session["LoginId"].ToString()) + "");
            if (dtLoginUser.Rows.Count > 0 && !string.IsNullOrWhiteSpace(dtVesselEmail.Rows[0]["Email"].ToString()))
            {
                CCMails.Add(dtVesselEmail.Rows[0]["Email"].ToString());
            }
        }

        if (EmailAddress.Trim() != "")
        {

            string ReportNoSerial = lblReportNo.Text.Trim().Substring(9);
            DataTable ER_S133_Report_Office = Common.Execute_Procedures_Select_ByQuery("select * from [dbo].[ER_S133_Report_Office] WHERE VesselCode='" + VesselCode + "' AND REPORTID=" + ReportId.ToString());
            if (ER_S133_Report_Office.Rows.Count <= 0)
            {
                ProjectCommon.ShowMessage("Nothing to export. Please save record first.");
                return;
            }

            DataTable dtRN = Common.Execute_Procedures_Select_ByQuery("SELECT ReportNo FROM [DBO].[ER_S133_Report] WHERE VesselCode='" + VesselCode + "' AND REPORTID=" + ReportId.ToString());

           // string ReportNoSerial = dtRN.Rows[0]["ReportNo"].ToString().Trim().Substring(9); 
            ER_S133_Report_Office.TableName = "ER_S133_Report_Office";

            DataSet ds = new DataSet();
            ds.Tables.Add(ER_S133_Report_Office.Copy());

            string SchemaFile = Server.MapPath("../../Temp/SCHEMA_" + FormNo + ".xml");
            string DataFile = Server.MapPath("../../Temp/DATA_" + FormNo + ".xml");

            ds.WriteXml(DataFile);
            ds.WriteXmlSchema(SchemaFile);

            string ZipData = Server.MapPath("../../Temp/ER_O_" + VesselCode + "_" + ReportNoSerial + ".zip");
            if (File.Exists(ZipData)) { File.Delete(ZipData); }

            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);
            }

            string Subject = "[ " + dtRN.Rows[0]["ReportNo"].ToString() + " ] - Office Reply";

            StringBuilder sb = new StringBuilder();
            sb.Append("Dear Captain,<br /><br />");
            sb.Append("Attached please find the office reply for your Nearmiss.<br /><br />");
            //sb.Append("Please import it in the ship system from PMS communication Tools.<br /><br /><br />");
            sb.Append("Thank You,");

            string fromAddress = ConfigurationManager.AppSettings["FromAddress"].ToString();

            string result = SendMail.SendSimpleMail(fromAddress, EmailAddress, CCMails.ToArray(), BCCMails.ToArray(), Subject, sb.ToString(), ZipData);
            if (result == "SENT")
            {
                Common.Execute_Procedures_Select_ByQuery("UPDATE [DBO].[ER_S133_Report_Office] SET ExportedBy='" + Session["FullName"].ToString() + "', ExportedOn = getdate() WHERE VesselCode='" + VesselCode + "' AND REPORTID=" + ReportId.ToString());
               // BindList();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Exported successfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "abort", "alert('Unable to export. Error : " + result + "');", true);
            }

            //using (ZipFile zip = new ZipFile())
            //{
            //    zip.AddFile(SchemaFile);
            //    zip.AddFile(DataFile);
            //    zip.Save(ZipData);
            //    Response.Clear();
            //    Response.ContentType = "application/zip";
            //    Response.AddHeader("Content-Type", "application/zip");
            //    Response.AddHeader("Content-Disposition", "inline;filename=" + Path.GetFileName(ZipData));
            //    Response.WriteFile(ZipData);
            //    Response.End();
            //}
        }
    }
    protected void BindRepeaterActions_Conditions()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [DBO].[ER_S133_Report_Office] WHERE  [VesselCode]='" + VesselCode + "' AND [ReportId] = " + ReportId.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            string[] Parts = dt.Rows[0]["CA_HumanActions"].ToString().Split(',');
            foreach (string itm in Parts)
            {
                ListItem li = chklstsec13HA.Items.FindByValue(itm);
                if (li != null) { li.Selected = true; }
            }

            Parts = dt.Rows[0]["CA_Conditions"].ToString().Split(',');
            foreach (string itm in Parts)
            {
                ListItem li = chklstsec13Cond.Items.FindByValue(itm);
                if (li != null) { li.Selected = true; }
            }

            Parts = dt.Rows[0]["RC_HumanFactors"].ToString().Split(',');
            foreach (string itm in Parts)
            {
                ListItem li = chklstsec13HF.Items.FindByValue(itm);
                if (li != null) { li.Selected = true; }
            }

            Parts = dt.Rows[0]["RC_JobFactors"].ToString().Split(',');
            foreach (string itm in Parts)
            {
                ListItem li = chklstsec13JF.Items.FindByValue(itm);
                if (li != null) { li.Selected = true; }
            }

            string Part1 = dt.Rows[0]["CA_HumanActions"].ToString() + "," + dt.Rows[0]["CA_Conditions"].ToString();
            string Part2 = dt.Rows[0]["RC_HumanFactors"].ToString() + "," + dt.Rows[0]["RC_JobFactors"].ToString();

            Part1.Replace(",,", ",");
            Part2.Replace(",,", ",");

            if (Part1.StartsWith(","))
                Part1 = Part1.Substring(1);
            if (Part2.StartsWith(","))
                Part2 = Part2.Substring(1);

            if (Part1.EndsWith(","))
                Part1 = Part1.Substring(0, Part1.Length - 1);
            if (Part2.EndsWith(","))
                Part2 = Part2.Substring(0, Part2.Length - 1);

            if (Part1.Trim() != "")
                ProjectCommon.Bind_Registers(rptImm_Cause, "Accident", Part1);
            if (Part2.Trim() != "")
                ProjectCommon.Bind_Registers(rptRoot_Cause, "Accident", Part2);

            lblOther1.Text = dt.Rows[0]["CA_HumanActions_Other"].ToString().Trim();
            lblOther2.Text = dt.Rows[0]["CA_Conditions_Other"].ToString().Trim();

            lblOther21.Text = dt.Rows[0]["RC_HumanFactors_Other"].ToString().Trim();
            lblOther22.Text = dt.Rows[0]["RC_JobFactors_Other"].ToString().Trim();
        }
    }
    protected void btnSaveIRCause_Click(object sender, EventArgs e)
    {
        FinalSave();
        dvIRCause.Visible = false;
        BindRepeaterActions_Conditions();
    }
    protected void lnlIRCause_Click(object sender, EventArgs e)
    {
        dvIRCause.Visible = true;
        //BindRegisters();
    }
    protected void btnIRCauseWindow_Click(object sender, EventArgs e)
    {
        dvIRCause.Visible = false;
    }

    protected void FinalSave()
    {
        try
        {
            string HA = "";
            string Cond = "";
            string HF = "";
            string JF = "";

            if (ddlsec15INMSeverity.SelectedValue.Trim() == "S")
            {
                HA = getCheckedItems(chklstsec13HA);
                Cond = getCheckedItems(chklstsec13Cond);
                HF = getCheckedItems(chklstsec13HF);
                JF = getCheckedItems(chklstsec13JF);
            }

            Common.Set_Procedures("[DBO].[ER_S133_InsertUpdate_ER_S133_Report_Office]");
            Common.Set_ParameterLength(20);
            Common.Set_Parameters(
                new MyParameter("@FORMNO", FormNo),
                new MyParameter("@REPORTID", ReportId),
                new MyParameter("@VESSELCODE", VesselCode),
                new MyParameter("@REMARKS", txtOfficeComments.Text.Trim()),
                new MyParameter("@NMSEVERITY", ddlsec15INMSeverity.SelectedValue), //(chksec15INMSeveritySignificant.Checked) ? "S" : "M"),
                new MyParameter("@CA_HUMANACTIONS", HA),
                new MyParameter("@CA_HUMANACTIONS_OTHER", txtsec13HAOther.Text),
                new MyParameter("@CA_CONDITIONS", Cond),
                new MyParameter("@CA_CONDITIONS_OTHER", txtsec13CondOther.Text),
                new MyParameter("@RC_HUMANFACTORS", HF),
                new MyParameter("@RC_HUMANFACTORS_OTHER", txtsec13HFOther.Text),
                new MyParameter("@RC_JOBFACTORS", JF),
                new MyParameter("@RC_JOBFACTORS_OTHER", txtsec13JFOther.Text),
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
    protected void btnSaveDraft_Click(object sender, EventArgs e)
    {

        // Validations --------------------
        //DateTime d;
        if (txtOfficeComments.Text.Trim() != "")
        {
            if (txtOfficeComments.Text.Trim().Length > 500)
            {
                lblMsg.Text = "Please check! Remarks text can not more than 500 characters.";
                btn_Tab_Click(btnOfficeComments, e);
                txtOfficeComments.Focus();
                return;
            }
        }

        //if (radsec15ICyes.Checked)
        //{
        //if (txtsec15CD.Text.Trim() == "")
        //{
        //    lblMsg.Text = "Please enter close date.";
        //    btn_Tab_Click(btnOfficeComments, e);
        //    txtsec15CD.Focus();
        //    return;
        //}
        //if (!DateTime.TryParse(txtsec15CD.Text, out d))
        //{
        //    lblMsg.Text = "Please enter valid date.";
        //    btn_Tab_Click(btnOfficeComments, e);
        //    txtsec15CD.Focus();
        //    return;
        //}
        //if (txtsec15CB.Text.Trim() == "")
        //{
        //    lblMsg.Text = "Please enter closed by.";
        //    btn_Tab_Click(btnOfficeComments, e);
        //    txtsec15CB.Focus();
        //    return;
        //}

        //if (!radsec15CNyes.Checked && !radsec15CNno.Checked)
        //{
        //    lblMsg.Text = "Please select '<b> YES / NO for VESSEL NOTIFIED OF INCIDENT CLOSURE. </b>'.";
        //    btn_Tab_Click(btnOfficeComments, e);
        //    radsec15CNyes.Focus();
        //    return;
        //}

        //if (radsec15CNyes.Checked)
        //{
        //    if (txtsec15NotifyDate.Text.Trim() == "")
        //    {
        //        lblMsg.Text = "Please enter notification date.";
        //        btn_Tab_Click(btnOfficeComments, e);
        //        txtsec15NotifyDate.Focus();
        //        return;
        //    }
        //    if (!DateTime.TryParse(txtsec15NotifyDate.Text, out d))
        //    {
        //        lblMsg.Text = "Please enter valid date.";
        //        btn_Tab_Click(btnOfficeComments, e);
        //        txtsec15NotifyDate.Focus();
        //        return;
        //    }
        //}
        //}

        //---------------------

        FinalSave();
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
    protected void chksec15INMSeveritySignificant_CheckedChanged(object sender, EventArgs e)
    {
        IRCauseHeader.Visible = (ddlsec15INMSeverity.SelectedValue.Trim() == "S");
        IRCauseData.Visible = (ddlsec15INMSeverity.SelectedValue.Trim() == "S");
    }

    protected void radsec15ICyes_OnCheckedChanged(object sender, EventArgs e)
    {
        //trIC.Visible = radsec15ICyes.Checked;
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
}