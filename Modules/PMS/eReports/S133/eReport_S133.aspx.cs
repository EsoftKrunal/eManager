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
    string FormNo = "S133";
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
        if (!IsPostBack)
        {
            VesselCode = Session["CurrentShip"].ToString();
            UserName = Session["FullName"].ToString();

            BindDetails();
            BindRegisters();
            BindRanks();

            if (Request.QueryString["Key"] != null && Request.QueryString["Key"].ToString() != "")
            {
                ReportId = Common.CastAsInt32(Request.QueryString["Key"].ToString());
                ShowReportDetails();
                btnSaveDraft.Visible = (Request.QueryString["Type"].ToString() == "E");
                btnExportToOffice.Visible = (Request.QueryString["Type"].ToString() == "E");
                tdTabs.Visible = (Request.QueryString["Type"].ToString() == "E");

                if (btnSaveDraft.Visible)
                {
                    bool? Locked = null;
                    bool Exported = false;

                    DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT COUNT(*) FROM [dbo].[ER_S133_Report] WHERE VESSELCODE='" + VesselCode + "' AND REPORTID=" + ReportId.ToString() + " AND EXPORTEDON IS NOT NULL");
                    Exported = Common.CastAsInt32(dt.Rows[0][0]) > 0;
                    DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("SELECT LOCKED FROM [dbo].[ER_S133_Report_Office] WHERE VESSELCODE='" + VesselCode + "' AND REPORTID=" + ReportId.ToString());
                    if (dt1.Rows.Count <= 0)
                        Locked = null;
                    else
                    {
                        if (Convert.IsDBNull(dt1.Rows[0][0]))
                            Locked = null;
                        else
                            Locked = dt1.Rows[0][0].ToString().Trim().ToUpper() == "Y";
                    }

                    if (Locked == null)
                        btnSaveDraft.Visible = !Exported;
                    else
                        btnSaveDraft.Visible = (Locked.Value == false);
                }
            }
        }
    }
    private void BindDetails()
    {
        lblFormName.Text = "NEAR MISS (HAZARDOUS OCCURRENCE) REPORT";
        string SQL = "SELECT ShipName FROM [DBO].Settings WHERE ShipCode ='" + VesselCode + "' ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        lblVesselName.Text = dt.Rows[0]["ShipName"].ToString();

        DataTable dtVersion = Common.Execute_Procedures_Select_ByQuery("Select [VersionNo] From [dbo].[ER_Master] WHERE [FormNo] = '" + FormNo + "'");
        if (dtVersion.Rows.Count > 0)
        {
            lblVersionNo.Text = dtVersion.Rows[0]["VersionNo"].ToString();
        }
    }
    private void BindRegisters()
    {
        ProjectCommon.LoadRegisters(cblCategory, FormNo, 20);
    }
    private void ShowReportDetails()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [DBO].[ER_S133_Report] WHERE [ReportId] = " + ReportId.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            //------------------ Main Details ----------------------
            lblReportNo.Text = dt.Rows[0]["ReportNo"].ToString();
            //lblVersionNo.Text = dt.Rows[0]["VersionNo"].ToString();

            lblLastExportedByOn.Text = dt.Rows[0]["ExportedBy"].ToString() + " / " + Common.ToDateString(dt.Rows[0]["ExportedOn"]);

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

        dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [DBO].[ER_S133_Report_Office] WHERE [ReportId] = " + ReportId.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            //------------------  Section 3 : Office Comments ---------------
            lblOfficeCommentBy.Text = dt.Rows[0]["CreatedBy"].ToString();
            lblOfficeCommentOn.Text = Common.ToDateString(dt.Rows[0]["CreatedOn"]);
            lblOfficeComments.Text = dt.Rows[0]["Remarks"].ToString();
            //-------------------------------------------------------------------------
            chksec15INMSeveritySignificant.Checked = (dt.Rows[0]["NMSeverity"].ToString() == "S");
            chksec15INMSeveritySignificant_CheckedChanged(new object(), new EventArgs());
            //-------------------------------------------------------------------------
            BindRepeaterActions_Conditions();
        }
    }
    protected void chksec15INMSeveritySignificant_CheckedChanged(object sender, EventArgs e)
    {
        IRCauseHeader.Visible = chksec15INMSeveritySignificant.Checked;
        IRCauseData.Visible = chksec15INMSeveritySignificant.Checked;
    }
   
    protected void BindRepeaterActions_Conditions()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [DBO].[ER_S133_Report_Office] WHERE [ReportId] = " + ReportId.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
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
                ProjectCommon.Bind_Registers(rptImm_Cause, "S115", Part1);
            if (Part2.Trim() != "")
                ProjectCommon.Bind_Registers(rptRoot_Cause, "S115", Part2);

            lblOther1.Text = dt.Rows[0]["CA_HumanActions_Other"].ToString().Trim();
            lblOther2.Text = dt.Rows[0]["CA_Conditions_Other"].ToString().Trim();

            lblOther21.Text = dt.Rows[0]["RC_HumanFactors_Other"].ToString().Trim();
            lblOther22.Text = dt.Rows[0]["RC_JobFactors_Other"].ToString().Trim();
        }
    }
    protected void btnDelDocument_Click(object sender, EventArgs e)
    {
        int DocId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        Common.Execute_Procedures_Select_ByQuery("UPDATE [DBO].ER_Report_Documents_Ship SET STATUS='D' WHERE VESSELCODE='" + VesselCode + "' AND FORMNO='" + FormNo + "' AND DOCID=" + DocId.ToString());
    }
    private void BindRanks()
    {
        string SQL = "SELECT RankId, RankCode FROM [DBO].[MP_AllRank]";
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
                new MyParameter("@RecordType", "eForm - S133"),
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
    protected void btnSaveDraft_Click(object sender, EventArgs e)
    {
        
        // Validations --------------------
        DateTime d;

        if (!DateTime.TryParse(txtOccurenceDt.Text, out d))
        {
            lblMsg.Text = "Please enter valid date.";
            txtOccurenceDt.Focus();
            return;
        }
        if (DateTime.Parse(txtOccurenceDt.Text) >= DateTime.Today.AddDays(1))
        {
            lblMsg.Text = "Date of occurence can not be more than today.";
            txtOccurenceDt.Focus();
            return;
        }
        string Category = getCheckedItems(cblCategory);

        if (Category == "")
        {
            lblMsg.Text = "Please select atleast one category";
            cblCategory.Focus();
            return;
        }

        //---------------------

        try
        {

            Common.Set_Procedures("[DBO].[ER_S133_InsertUpdate_ER_S133_Report]");
            Common.Set_ParameterLength(17);
            Common.Set_Parameters(
                new MyParameter("@FORMNO", FormNo),
                new MyParameter("@REPORTID", ReportId),
                new MyParameter("@REPORTNO", lblReportNo.Text.Trim()),
                new MyParameter("@VESSELCODE", VesselCode),
                new MyParameter("@DATEOFOCCURENCE", (txtOccurenceDt.Text.Trim() == "") ? DBNull.Value : (object)txtOccurenceDt.Text.Trim()),
                new MyParameter("@FAMILYNAME", txtFamilyName.Text.Trim()),
                new MyParameter("@FIRSTNAME", txtFirstName.Text.Trim()),
                new MyParameter("@RANKID", ddlRank.SelectedValue.Trim()),
                new MyParameter("@CREWNO", txtCrewNo.Text.Trim()),
                new MyParameter("@CATEGORY", Category),
                new MyParameter("@EVENTDESCR", txtEventDesc.Text.Trim()),
                new MyParameter("@SUGGESTIONS", txtSuggestions.Text.Trim()),
                new MyParameter("@SM_FAMILYNAME", txt_SM_FamilyName.Text.Trim()),
                new MyParameter("@SM_FIRSTNAME", txt_SM_FirstName.Text.Trim()),
                new MyParameter("@SM_CREWNO", txt_SM_CrewNo.Text.Trim()),
                new MyParameter("@CREATEDBY", UserName),
                new MyParameter("@UPDATEDBY", UserName)
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
                    
                    DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT ReportNo FROM [DBO].[ER_S133_Report] WHERE [ReportId] = " + ReportId.ToString());
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
    
}