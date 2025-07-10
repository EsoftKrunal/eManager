using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Ionic.Zip;

public partial class eReports_G118_eReport_G118 : System.Web.UI.Page
{
    string FormNo = "G118";
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

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        //lblMessage_ITP.Text = "";
        
        if (!IsPostBack)
        {
            VesselCode = Session["CurrentShip"].ToString();
            UserName = Session["FullName"].ToString();

            BindDetails();
            BindRegisters();

           
            if (Request.QueryString["Key"] != null && Request.QueryString["Key"].ToString() != "")
            {
                ReportId = Common.CastAsInt32(Request.QueryString["Key"].ToString());
                ShowReportDetails();

                //btnSaveDraft.Visible = (Request.QueryString["Type"].ToString() == "E");
                btnExportToOffice.Visible = (Request.QueryString["Type"].ToString() == "E");                
                
                tdTabs.Visible = (Request.QueryString["Type"].ToString() == "E");

                //if (btnSaveDraft.Visible)
                //{
                //    bool? Locked = null;
                //    bool Exported = false;

                //    DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT COUNT(*) FROM [dbo].[Accident_Report] WHERE VESSELCODE='" + VesselCode + "' AND REPORTID=" + ReportId.ToString() + " AND EXPORTEDON IS NOT NULL");
                //    Exported = Common.CastAsInt32(dt.Rows[0][0]) > 0;
                //    DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("SELECT LOCKED FROM [dbo].[Accident_Report_Office] WHERE VESSELCODE='" + VesselCode + "' AND REPORTID=" + ReportId.ToString());
                //    if (dt1.Rows.Count <= 0)
                //        Locked = null;
                //    else
                //    {
                //        if (Convert.IsDBNull(dt1.Rows[0][0]))
                //            Locked = null;
                //        else
                //            Locked = dt1.Rows[0][0].ToString().Trim().ToUpper() == "Y";
                //    }

                //    if (Locked == null)
                //        btnSaveDraft.Visible = !Exported;
                //    else
                //        btnSaveDraft.Visible = (Locked.Value == false);
                //}
            }
        }
        if (ReportId > 0)
        {
            ShowNotice();
            spn_Note.Visible = ReportId > 0 && dv_Notice.Visible;
            //frmDocs.Attributes.Add("src", "../UploadDocuments.aspx?Key=" + FormNo + "&ReportId=" + ReportId.ToString());
            ////frmDocs.Attributes.Add("src", "../AppletUploader.aspx?Key=" + FormNo + "&ReportId=" + ReportId.ToString());
            //imgbtnUploadDocsShowPanel.Visible = ReportId > 0;
        }
    }
    private void BindDetails()
    {
        lblFormName.Text = "Non Conformance Report (NCR)";
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
        ProjectCommon.LoadRegisters(ddlAuditedArea, FormNo, 21);
        ProjectCommon.LoadRegisters(ddlEXAudit, FormNo, 22);
        ProjectCommon.LoadRegisters(ddlIntAudit, FormNo, 23);
        ProjectCommon.LoadRegisters(chklstRC, FormNo, 24);

    }
    private void ShowReportDetails()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(" SELECT R.*, " + 
                                                                " [OfficeComment],[OfficeCommentVarefiedBy], [OfficeCommentVarefiedDate],[VerifiedPersonPOS],[Is_Closed],[ClosureDate], " + 
                                                                " [ClosureRemarks], [ClosureEvidence], [ClosedBy], [ClosedOn],[Is_Closure_notified], [NotifyDate],[Status],[Locked] " +  
                                                                " FROM [DBO].[ER_G118_Report] R " +
                                                                " LEFT JOIN [dbo].[Accident_Report_Office] O ON O.[ReportId] = R.[ReportId] AND R.VesselCode = O.VesselCode " +
                                                                " WHERE R.[ReportId] = " + ReportId.ToString());
       
        if (dt != null && dt.Rows.Count > 0)
        {
            //------------------ Main Details ----------------------
            lblReportNo.Text = dt.Rows[0]["ReportNo"].ToString();            
            lblLastExportedByOn.Text = dt.Rows[0]["ExportedBy"].ToString() + " / " + Common.ToDateString(dt.Rows[0]["ExportedOn"]);

            ddlAuditedArea.SelectedValue = dt.Rows[0]["AreaAudited"].ToString();
            if (ddlAuditedArea.SelectedItem.Text.Contains("Other"))
            {
                txtAreaAuditedOtherVessel.Visible = true;
                txtAreaAuditedOtherVessel.Text = dt.Rows[0]["AreaAuditedOther"].ToString();
            }

            if (dt.Rows[0]["AuditType"].ToString() == "EXT")
            {
                rdoEXAudit.Checked = true;
                ddlEXAudit.Enabled = true;
                ddlEXAudit.SelectedValue = dt.Rows[0]["AuditValue"].ToString();
                if (ddlEXAudit.SelectedItem.Text.Contains("Others"))
                {
                    txtExtAuditOther.Visible = true;
                    txtExtAuditOther.Text = dt.Rows[0]["AuditValueOther"].ToString();
                }
            }
            else
            {
                rdoInternal.Checked = true;
                ddlIntAudit.Enabled = true;
                ddlIntAudit.SelectedValue = dt.Rows[0]["AuditValue"].ToString();

                if (ddlIntAudit.SelectedItem.Text.Contains("Others"))
                {
                    txtIntAuditOther.Visible = true;
                    txtIntAuditOther.Text = dt.Rows[0]["AuditValueOther"].ToString();
                }
            }

            txtNCD.Text = dt.Rows[0]["NonConformanceDes"].ToString();

            txtNameMakingNCR.Text = dt.Rows[0]["NCRMaker"].ToString();
            txtRankNCR.Text = dt.Rows[0]["NCRMakerPos"].ToString();
            NCRDate.Text = Common.ToDateString(dt.Rows[0]["NCRCreatedDate"]);


            txtPersonReveNCR.Text = dt.Rows[0]["PersonRecvNCR"].ToString();
            txtRankReveNCR.Text = dt.Rows[0]["RankRecvNCR"].ToString();
            txtNameMasterGeneralManager.Text = dt.Rows[0]["NameofGeneralManager"].ToString();
            txtNCRTargetCompDate.Text = Common.ToDateString(dt.Rows[0]["NCRTargetCompDate"]);

            txtImmediateCorrectiveAction.Text = dt.Rows[0]["ImmediateCorrectiveAction"].ToString();
            txtICAName.Text = dt.Rows[0]["IMCName"].ToString();
            txtICAPositionOrRank.Text = dt.Rows[0]["IMCRanck"].ToString();
            txtICACorrectiveActionCompletedOn.Text = Common.ToDateString(dt.Rows[0]["IMCCompletedDate"]);

            ProjectCommon.SetCheckboxListData(dt.Rows[0]["RootCause"].ToString(), chklstRC);


            if (chklstRC.Items.FindByText("Other (Specify)").Selected)
            {
                txtRootCauseOther.Visible = true;                 
                txtRootCauseOther.Text = dt.Rows[0]["OhterRootCause"].ToString();
            }
            else
            {
                txtRootCauseOther.Visible = false;
                txtRootCauseOther.Text = "";
            }
            txtPreventyAction.Text = dt.Rows[0]["PreventivAction"].ToString();

            txtOfficeComment.Text = dt.Rows[0]["OfficeComment"].ToString();
            txtOffCommVarefiedBy.Text = dt.Rows[0]["OfficeCommentVarefiedBy"].ToString();
            txtVarifiedPOS.Text = dt.Rows[0]["VerifiedPersonPOS"].ToString();
            txtOffVarefiedDate.Text = Common.ToDateString(dt.Rows[0]["OfficeCommentVarefiedDate"]);

            lblIC.Text = (dt.Rows[0]["Is_Closed"].ToString() == "Y" ? "Yes" : (dt.Rows[0]["Is_Closed"].ToString() == "N" ? "No" : ""));

            trIC.Visible = (lblIC.Text == "Yes");
            if (!trIC.Visible)
            {
                txtCD.Text = txtCB.Text = "";
            }            

            txtCD.Text = Common.ToDateString(dt.Rows[0]["ClosureDate"]);
            txtCloserRemarks.Text = dt.Rows[0]["ClosureRemarks"].ToString();
            txtCB.Text = dt.Rows[0]["ClosedBy"].ToString();

            if (dt.Rows[0]["ClosureEvidence"].ToString().Trim() != "")
            {
                btnClip.Visible = true;
                btnClipText.Visible = true;
                btnClipText.Text = dt.Rows[0]["ClosureEvidence"].ToString();
            }

            lblCN.Text = (dt.Rows[0]["Is_Closure_notified"].ToString() == "Y" ? "Yes" : (dt.Rows[0]["Is_Closure_notified"].ToString() == "N" ? "No" : ""));

            trVN.Visible = (lblCN.Text == "Yes");
            if (!trVN.Visible)
            {
                txtNotifyDate.Text = "";
            }           

            txtNotifyDate.Text = Common.ToDateString(dt.Rows[0]["NotifyDate"]);

        }

    }

    protected void rdoAudit_CheckedChanged(object sender, EventArgs e)
    {
        if (rdoEXAudit.Checked)
        {
            ddlEXAudit.Enabled = true;
            ddlIntAudit.Enabled = false;
        }

        if (rdoInternal.Checked)
        {
            ddlEXAudit.Enabled = false;
            ddlIntAudit.Enabled = true;
        }

        ddlEXAudit.SelectedIndex = 0;
        ddlIntAudit.SelectedIndex = 0;

        txtExtAuditOther.Visible = false;
        txtIntAuditOther.Visible = false;
    }

    protected void ddlAudit_SelectedIndexChanged(object sender, EventArgs e)
    {
        string str =  ((DropDownList)sender).SelectedItem.Text;
        if (str.Contains("Others"))
        {
            if (rdoEXAudit.Checked)
            {
                txtExtAuditOther.Visible = true;
            }
            else
            {
                txtIntAuditOther.Visible = true;
            }
        }
        else
        {
            txtExtAuditOther.Visible = false;
            txtIntAuditOther.Visible = false;
        }
    }

    protected void ddlAuditedArea_SelectedIndexChanged(object sender, EventArgs e)
    {
       string str =  ((DropDownList)sender).SelectedItem.Text;
       if (str.Contains("Other"))
       {
           txtAreaAuditedOtherVessel.Visible = true;
       }
       else
       {
           txtAreaAuditedOtherVessel.Text = "";
           txtAreaAuditedOtherVessel.Visible = false;
       }
    }

    protected void chklstRC_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (chklstRC.Items.FindByText("Other (Specify)").Selected)
        {
            txtRootCauseOther.Text = "";
            txtRootCauseOther.Visible = true;
        }
        else
        {
            txtRootCauseOther.Visible = false;
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
    
    protected void btn_Tab_Click(object sender, EventArgs e)
    {
        Div1.Visible = false;
        Div2.Visible = false;
        Div3.Visible = false;
        Div4.Visible = false;
        

        btnDec.CssClass = "color_tab";         
        btnCA.CssClass="color_tab";
        btnRC.CssClass = "color_tab";        
        btnOC.CssClass="color_tab";
        

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

        if (b.ID == "btnOC")
        {
            btnSaveDraft.Visible = (Request.QueryString["Type"].ToString() == "E");

            if (btnSaveDraft.Visible)
            {
                bool? Locked = null;
                bool Exported = false;

                DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT COUNT(*) FROM [dbo].[Accident_Report] WHERE VESSELCODE='" + VesselCode + "' AND REPORTID=" + ReportId.ToString() + " AND EXPORTEDON IS NOT NULL");
                Exported = Common.CastAsInt32(dt.Rows[0][0]) > 0;
                DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("SELECT LOCKED FROM [dbo].[Accident_Report_Office] WHERE VESSELCODE='" + VesselCode + "' AND REPORTID=" + ReportId.ToString());
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

        ShowNotice();
    }
    private void ShowNotice()
    {

        // ---------------------------------
        bool Error_btnDec = false;
        bool Error_btnCA = false;
        bool Error_btnRC = false;

        //-- Home ------------------------------------------------------------

        // Section 
        if (ddlAuditedArea.SelectedIndex <= 0 || (!rdoEXAudit.Checked && !rdoInternal.Checked) || (rdoEXAudit.Checked && ddlEXAudit.SelectedIndex <= 0) || (rdoInternal.Checked && ddlIntAudit.SelectedIndex <= 0) || txtNCD.Text.Trim() == "" || txtNameMakingNCR.Text.Trim() == "" || txtRankNCR.Text.Trim() == "" || txtPersonReveNCR.Text.Trim() == "" || txtRankReveNCR.Text.Trim() == "" || txtNameMasterGeneralManager.Text.Trim() == "" || NCRDate.Text.Trim() == "" || txtNCRTargetCompDate.Text.Trim() == "")
            Error_btnDec = true;

        // Section 2
        if (txtImmediateCorrectiveAction.Text.Trim() == "" || txtICAName.Text.Trim() == "" || txtICAPositionOrRank.Text.Trim() == "" || txtICACorrectiveActionCompletedOn.Text.Trim() == "")
            Error_btnCA = true;

        // Section 3

        if (IsINValid(chklstRC) || txtPreventyAction.Text.Trim() == "")
            Error_btnRC = true;


        dv_Notice.Visible = Error_btnDec || Error_btnCA || Error_btnRC;

        spn_Note.Visible = ReportId > 0 && dv_Notice.Visible;
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
    protected void btnExportToOffice_Click(object sender, EventArgs e)
    {
        ShowNotice();
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
                new MyParameter("@RecordType", "eForm - G118"),
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



        //DataTable ER_G118_Report = Common.Execute_Procedures_Select_ByQuery("select * from [dbo].[Accident_Report] WHERE REPORTID=" + ReportId.ToString());        
        //DataTable ER_Report_UpdationDetails = Common.Execute_Procedures_Select_ByQuery("select * from [dbo].[ER_Report_UpdationDetails] WHERE FORMNO='" + FormNo + "' AND REPORTID=" + ReportId.ToString());

        //ER_G118_Report.TableName = "Accident_Report";        
        //ER_Report_UpdationDetails.TableName = "ER_Report_UpdationDetails";

        //DataSet ds = new DataSet();
        //ds.Tables.Add(Accident_Report.Copy());         
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
        //        Common.Execute_Procedures_Select_ByQuery("UPDATE [dbo].[Accident_Report] SET EXPORTEDBY='" + UserName + "',EXPORTEDON=GETDATE() WHERE REPORTID=" + ReportId.ToString());
        //        Response.End();
        //    }
        //}
        //catch { }
    }
    protected void btnExportDocsToOffice_Click(object sender, EventArgs e)
    {
        //ShowNotice();
        //if (ReportId <= 0)
        //{
        //    lblMsg.Text = "Please first save the form to export.";
        //    txtReportDate.Focus();
        //    return;
        //}

        //string DocumentsPath = Server.MapPath("~\\eReports\\" + FormNo + "\\Documents\\Ship\\");
        //DataTable ER_Report_Documents_Ship = Common.Execute_Procedures_Select_ByQuery("select '" + DocumentsPath + "' + FILENAME from [dbo].[ER_Report_Documents_Ship] WHERE REPORTID=" + ReportId.ToString());
        //ER_Report_Documents_Ship.TableName = "ER_Report_Documents_Ship";

        //string ZipData = Server.MapPath("ER_S_" + VesselCode + "_FILES_" + FormNo + ".zip");
        //if (File.Exists(ZipData)) { File.Delete(ZipData); }

        //using (ZipFile zip = new ZipFile())
        //{
        //    foreach (DataRow dr in ER_Report_Documents_Ship.Rows)
        //    {
        //        if (File.Exists(dr[0].ToString()))
        //        {
        //            zip.AddFile(dr[0].ToString());
        //        }
        //    }
        //    zip.Save(ZipData);
        //    Response.Clear();
        //    Response.ContentType = "application/zip";
        //    Response.AddHeader("Content-Type", "application/zip");
        //    Response.AddHeader("Content-Disposition", "inline;filename=" + Path.GetFileName(ZipData));
        //    Response.WriteFile(ZipData);
        //    Response.End();
        //}
    }
    
    protected void btnSaveDraft_Click(object sender, EventArgs e)
    {
        ShowNotice();
        // Validations --------------------

        if (ddlAuditedArea.SelectedIndex == 0)
        {
            lblMsg.Text = "Please select area audited.";
            ddlAuditedArea.Focus();
            return;
        }
        if (txtAreaAuditedOtherVessel.Visible && txtAreaAuditedOtherVessel.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter area audited other.";
            txtAreaAuditedOtherVessel.Focus();
            return;
        }
        if (!rdoEXAudit.Checked && !rdoInternal.Checked)
        {
            lblMsg.Text = "Please select audit type.";
            rdoEXAudit.Focus();
            return;
        }
        if (rdoEXAudit.Checked && ddlEXAudit.SelectedIndex == 0)
        {
            lblMsg.Text = "Please select external audit.";
            ddlEXAudit.Focus();
            return;
        }

        if (txtExtAuditOther.Visible && txtExtAuditOther.Text.Trim() == "")
        {
            lblMsg.Text = "Please specify other external audit.";
            txtExtAuditOther.Focus();
            return;
        }

        if (rdoInternal.Checked && ddlIntAudit.SelectedIndex == 0)
        {
            lblMsg.Text = "Please select internal audit.";
            ddlIntAudit.Focus();
            return;
        }

        if (txtIntAuditOther.Visible && txtIntAuditOther.Text.Trim() == "")
        {
            lblMsg.Text = "Please specify other internal audit.";
            txtIntAuditOther.Focus();
            return;
        }

        if (IsINValid(chklstRC))
        {
            lblMsg.Text = "Please select at least one option in '<b>Root Causes</b>'.";
            chklstRC.Focus();
            btn_Tab_Click(btnRC, e);
            return;
        }

        if (chklstRC.Items.FindByText("Other (Specify)").Selected && txtRootCauseOther.Text.Trim() == "")
        {
            lblMsg.Text = "Please specify other root cause.";
            txtRootCauseOther.Focus();
            btn_Tab_Click(btnRC, e);
            return;
        }

        // ----------------------------------------------------------------
        
        string RootCause = getCheckedItems(chklstRC);

        try
        {

            Common.Set_Procedures("[DBO].[Accident_InsertUpdate_Accident_Report]");
            Common.Set_ParameterLength(26);
            Common.Set_Parameters(
                new MyParameter("@FORMNO", FormNo),
                new MyParameter("@ReportId", ReportId),
                new MyParameter("@ReportNo", lblReportNo.Text.Trim()),
                new MyParameter("@VesselCode", VesselCode),
                new MyParameter("@AreaAudited", ddlAuditedArea.SelectedValue),
                new MyParameter("@AreaAuditedOther", txtAreaAuditedOtherVessel.Text.Trim()),
                new MyParameter("@AuditType", (rdoEXAudit.Checked ? "EXT" : "INT")),
                new MyParameter("@AuditValue", (rdoEXAudit.Checked ? ddlEXAudit.SelectedValue : ddlIntAudit.SelectedValue)),
                new MyParameter("@AuditValueOther", (txtExtAuditOther.Text.Trim() == "") ? ((txtIntAuditOther.Text.Trim() == "") ? DBNull.Value : (object)txtIntAuditOther.Text.Trim()) : txtExtAuditOther.Text.Trim()),
                new MyParameter("@NonConformanceDes", txtNCD.Text.Trim()),
                new MyParameter("@NCRMaker", txtNameMakingNCR.Text.Trim()),
                new MyParameter("@NCRCreatedDate", (NCRDate.Text.Trim() == "") ? DBNull.Value : (object)NCRDate.Text.Trim()),
                new MyParameter("@NCRMakerPos", txtRankNCR.Text.Trim()),
                new MyParameter("@PersonRecvNCR", txtPersonReveNCR.Text.Trim()),
                new MyParameter("@RankRecvNCR", txtRankReveNCR.Text.Trim()),
                new MyParameter("@NameofGeneralManager", txtNameMasterGeneralManager.Text.Trim()),
                new MyParameter("@NCRTargetCompDate", (txtNCRTargetCompDate.Text.Trim() == "") ? DBNull.Value : (object)txtNCRTargetCompDate.Text.Trim()),
                new MyParameter("@ImmediateCorrectiveAction", txtImmediateCorrectiveAction.Text.Trim()),
                new MyParameter("@IMCName", txtICAName.Text.Trim()),
                new MyParameter("@IMCCompletedDate", (txtICACorrectiveActionCompletedOn.Text.Trim() == "") ? DBNull.Value : (object)txtICACorrectiveActionCompletedOn.Text.Trim()),
                new MyParameter("@IMCRanck", txtICAPositionOrRank.Text.Trim()),
                new MyParameter("@RootCause", RootCause.Trim()),
                new MyParameter("@OhterRootCause", txtRootCauseOther.Text.Trim()),
                new MyParameter("@PreventivAction", txtPreventyAction.Text.Trim()),
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
                    //imgbtnUploadDocsShowPanel.Visible = true;
                    spn_Note.Visible = true && dv_Notice.Visible;
                    //frmDocs.Attributes.Add("src", "../UploadDocuments.aspx?Key=" + FormNo + "&ReportId=" + ReportId.ToString());

                    DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT (Select [VersionNo] From [dbo].[ER_Master] WHERE [FormNo] = '" + FormNo + "') As VersionNo, ReportNo FROM [DBO].[Accident_Report] WHERE [ReportId] = " + ReportId.ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        lblReportNo.Text = dt.Rows[0]["ReportNo"].ToString();
                        lblVersionNo.Text = dt.Rows[0]["VersionNo"].ToString();
                    }
                }
                
                tdTabs.Visible = (ReportId > 0);

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

    protected void btnClip_Click(object sender, ImageClickEventArgs e)
    {
        DownloadFile();
    }
    protected void btnClipText_Click(object sender, EventArgs e)
    {
        DownloadFile();
    }
    protected void DownloadFile()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT ClosureEvidence,CloserFile FROM [dbo].[Accident_Report_Office] WHERE ReportId =" + ReportId);
        if (dt.Rows.Count > 0)
        {
            string FileName = dt.Rows[0]["ClosureEvidence"].ToString();

            if (FileName.Trim() != "")
            {
                byte[] buff = (byte[])dt.Rows[0]["CloserFile"];
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                Response.BinaryWrite(buff);
                Response.Flush();
                Response.End();
            }
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        int div = Common.CastAsInt32(((Button)sender).CommandArgument);

        if (div == 2)
        {
            btn_Tab_Click(btnCA, e);             
        }

        if (div == 3)
        {
            btn_Tab_Click(btnRC, e);
        }

        if (div == 4)
        {
            btn_Tab_Click(btnOC, e);
        }
    }
    
}