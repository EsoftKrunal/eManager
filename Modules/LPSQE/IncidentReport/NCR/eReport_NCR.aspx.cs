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
    string FormNo = "NCR";
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
            Common.Execute_Procedures_Select_ByQuery("UPDATE [dbo].[ER_G118_Report_Office] SET Locked='N' WHERE VESSELCODE='" + VesselCode + "' AND REPORTID=" + ReportId.ToString());
        }
        else
        {
            Common.Execute_Procedures_Select_ByQuery("UPDATE [dbo].[ER_G118_Report_Office] SET Locked='Y' WHERE VESSELCODE='" + VesselCode + "' AND REPORTID=" + ReportId.ToString());
        }
        ShowLockUnlock();
    }
    protected void ShowLockUnlock()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [DBO].[ER_G118_Report_Office] WHERE VESSELCODE='" + VesselCode + "' AND [ReportId] = " + ReportId.ToString());
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
        //lblMessage_ITP.Text = "";
        
        if (!IsPostBack)
        {
            VesselCode = Request.QueryString["VC"].ToString();
            UserName = Session["UserName"].ToString();

            BindDetails();
            BindRegisters();

            if (Request.QueryString["Key"] != null && Request.QueryString["Key"].ToString() != "")
            {
                ReportId = Common.CastAsInt32(Request.QueryString["Key"].ToString());
                ShowReportDetails();

                btnSaveDraft.Visible = (Request.QueryString["Type"].ToString() == "E");
                //btnExportToOffice.Visible = (Request.QueryString["Type"].ToString() == "E");                
                
                //tdTabs.Visible = (Request.QueryString["Type"].ToString() == "E");

                string[] Files = System.IO.Directory.GetFiles(Server.MapPath("~/Modules/LPSQE/IncidentReport/NCR"), "*.zip");
                foreach (string fl in Files)
                {
                    System.IO.File.Delete(fl);
                }
            }

            ShowNotice();
        }
        
    }
    private void BindDetails()
    {
        lblFormName.Text = "Non Conformance Report (NCR)";
        string SQL = "SELECT VESSELName FROM [DBO].Vessel WHERE VESSELCode ='" + VesselCode + "' ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        lblVesselName.Text = dt.Rows[0]["VESSELName"].ToString();        
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
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(" SELECT (Select [VersionNo] From [dbo].[ER_Master] WHERE [FormNo] = '" + FormNo + "') As VersionNo,R.*, " + 
                                                                " [OfficeComment],[OfficeCommentVarefiedBy], [OfficeCommentVarefiedDate],[VerifiedPersonPOS],[Is_Closed],[ClosureDate], " +
                                                                " [ClosureRemarks], [ClosureEvidence],CloserFile, [ClosedBy], [ClosedOn],[Is_Closure_notified], [NotifyDate],[Status],[Locked] " +  
                                                                " FROM [DBO].[ER_G118_Report] R " + 
                                                                " LEFT JOIN [dbo].[ER_G118_Report_Office] O ON O.[ReportId] = R.[ReportId] AND R.VesselCode = O.VesselCode " +
                                                                " WHERE R.VesselCode='" + VesselCode + "' and R.VesselCode in (Select v.VesselCode from UserVesselRelation vw with(nolock) inner join Vessel v on vw.VesselId = v.VesselId where vw.Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ")  and R.[ReportId] = " + ReportId.ToString());
       
        if (dt != null && dt.Rows.Count > 0)
        {
            //------------------ Main Details ----------------------
            lblReportNo.Text = dt.Rows[0]["ReportNo"].ToString();
            lblVersionNo.Text = dt.Rows[0]["VersionNo"].ToString();
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


            if (dt.Rows[0]["OhterRootCause"].ToString().ToUpper().Contains("OTHER"))
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

            radICyes.Checked = dt.Rows[0]["Is_Closed"].ToString() == "Y"; 
            radICno.Checked = dt.Rows[0]["Is_Closed"].ToString() == "N";

            radICyes_OnCheckedChanged(new object(), new EventArgs());

            txtCD.Text = Common.ToDateString(dt.Rows[0]["ClosureDate"]);
            txtCloserRemarks.Text = dt.Rows[0]["ClosureRemarks"].ToString();
            txtCB.Text = dt.Rows[0]["ClosedBy"].ToString();

            if (dt.Rows[0]["ClosureEvidence"].ToString().Trim() != "")
            {                   
                btnClip.Visible = true;
                btnClipText.Visible = true;
                btnClipText.Text = dt.Rows[0]["ClosureEvidence"].ToString();
            } 

            radCNyes.Checked = dt.Rows[0]["Is_Closure_notified"].ToString() == "Y";
            radCNno.Checked = dt.Rows[0]["Is_Closure_notified"].ToString() == "N";

            radCNyes_OnCheckedChanged(new object(), new EventArgs());

            txtNotifyDate.Text = Common.ToDateString(dt.Rows[0]["NotifyDate"]);

            ShowLockUnlock();
        }

    }
    
    //protected void rdoAudit_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (rdoEXAudit.Checked)
    //    {
    //        ddlEXAudit.Enabled = true;
    //        ddlIntAudit.Enabled = false;
    //    }

    //    if (rdoInternal.Checked)
    //    {
    //        ddlEXAudit.Enabled = false;
    //        ddlIntAudit.Enabled = true;
    //    }

    //    ddlEXAudit.SelectedIndex = 0;
    //    ddlIntAudit.SelectedIndex = 0;

    //    txtExtAuditOther.Visible = false;
    //    txtIntAuditOther.Visible = false;
    //}

    //protected void ddlAudit_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string str =  ((DropDownList)sender).SelectedItem.Text;
    //    if (str.Contains("Others"))
    //    {
    //        if (rdoEXAudit.Checked)
    //        {
    //            txtExtAuditOther.Visible = true;
    //        }
    //        else
    //        {
    //            txtIntAuditOther.Visible = true;
    //        }
    //    }
    //    else
    //    {
    //        txtExtAuditOther.Visible = false;
    //        txtIntAuditOther.Visible = false;
    //    }
    //}

    //protected void ddlAuditedArea_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //   string str =  ((DropDownList)sender).SelectedItem.Text;
    //   if (str.Contains("Other"))
    //   {
    //       txtAreaAuditedOtherVessel.Visible = true;
    //   }
    //   else
    //   {
    //       txtAreaAuditedOtherVessel.Text = "";
    //       txtAreaAuditedOtherVessel.Visible = false;
    //   }
    //}

    //protected void chklstRC_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (chklstRC.Items.FindByText("Other (Specify)").Selected)
    //    {
    //        txtRootCauseOther.Text = "";
    //        txtRootCauseOther.Visible = true;
    //    }
    //    else
    //    {
    //        txtRootCauseOther.Visible = false;
    //    }
    //}

    
    protected void radICyes_OnCheckedChanged(object sender, EventArgs e)
    {
        trIC.Visible = radICyes.Checked;
        if (!trIC.Visible)
        {
            txtCD.Text = txtCB.Text = "";
        }
    }
    protected void radCNyes_OnCheckedChanged(object sender, EventArgs e)
    {
        trVN.Visible = radCNyes.Checked;
        if (!trVN.Visible)
        {
            txtNotifyDate.Text = "";
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

        ShowNotice();
    }
    private void ShowNotice()
    {

        // ---------------------------------
        bool Error_btnDec = false;
        bool Error_btnCA = false;
        bool Error_btnRC = false;
        bool Error_btnOC = false;

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
        
        // Section 4

        if (txtOfficeComment.Text.Trim() == "" || txtOffCommVarefiedBy.Text.Trim() == "" || txtVarifiedPOS.Text.Trim() == "" || txtOffVarefiedDate.Text.Trim() == "" || (!radICyes.Checked && !radICno.Checked) || (radICyes.Checked && txtCloserRemarks.Text.Trim() == "" && txtCD.Text.Trim() == "" && txtCB.Text.Trim() == "" && btnClipText.Text.Trim() == "") || (!radCNyes.Checked && !radCNno.Checked) || (radCNyes.Checked && txtNotifyDate.Text.Trim() == ""))
            Error_btnOC = true;


        dv_Notice.Visible = Error_btnDec || Error_btnCA || Error_btnRC || Error_btnOC;

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
    protected void btnExportToShip_Click(object sender, EventArgs e)
    {
        
        string ReportNoSerial = lblReportNo.Text.Trim().Substring(9);

        DataTable ER_G118_Report_Office = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM [dbo].[ER_G118_Report_Office] WHERE VesselCode='" + VesselCode + "' AND REPORTID=" + ReportId.ToString());
        if (ER_G118_Report_Office.Rows.Count <= 0)
        {
            ProjectCommon.ShowMessage("Nothing to export. Please save record first.");
            return;
        }


        ER_G118_Report_Office.TableName = "ER_G118_Report_Office";
        

        DataSet ds = new DataSet();
        ds.Tables.Add(ER_G118_Report_Office.Copy());         

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
    
    protected void btnSaveDraft_Click(object sender, EventArgs e)
    {
        DateTime d;

        if (txtOfficeComment.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter office comments.";
            btn_Tab_Click(btnOC, e);
            txtOfficeComment.Focus();
            return;
        }

        if (txtOffCommVarefiedBy.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter verified by name.";
            btn_Tab_Click(btnOC, e);
            txtOffCommVarefiedBy.Focus();
            return;
        }

        if (txtVarifiedPOS.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter rank or position.";
            btn_Tab_Click(btnOC, e);
            txtVarifiedPOS.Focus();
            return;
        }

        if (txtOffVarefiedDate.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter verified date.";
            btn_Tab_Click(btnOC, e);
            txtVarifiedPOS.Focus();
            return;
        }

        if (!DateTime.TryParse(txtOffVarefiedDate.Text, out d))
        {
            lblMsg.Text = "Please enter valid date.";
            btn_Tab_Click(btnOC, e);
            txtOffVarefiedDate.Focus();
            return;
        }


        if (radICyes.Checked)
        {

            if (txtCloserRemarks.Text.Trim() == "")
            {
                lblMsg.Text = "Please enter closure remarks.";
                btn_Tab_Click(btnOC, e);
                txtCloserRemarks.Focus();
                return;
            }
            if (txtCD.Text.Trim() == "")
            {
                lblMsg.Text = "Please enter close date.";
                btn_Tab_Click(btnOC, e);
                txtCD.Focus();
                return;
            }
            if (!DateTime.TryParse(txtCD.Text, out d))
            {
                lblMsg.Text = "Please enter valid date.";
                btn_Tab_Click(btnOC, e);
                txtCD.Focus();
                return;
            }
            if (txtCB.Text.Trim() == "")
            {
                lblMsg.Text = "Please enter closed by.";
                btn_Tab_Click(btnOC, e);
                txtCB.Focus();
                return;
            }

            //-------------------------------------------
            if (btnClipText.Text.Trim() == "")
            {
                if (!flpUpload.HasFile)
                {
                    lblMsg.Text = "Please select a file to upload.";
                    flpUpload.Focus();
                    return;
                }
                if (flpUpload.HasFile)
                {
                    if (!flpUpload.FileName.EndsWith(".pdf"))
                    {
                        lblMsg.Text = "Please check.. ! Only pdf files are allowed.";
                        flpUpload.Focus();
                        return;
                    }
                }
            }

            if (!radCNyes.Checked && !radCNno.Checked)
            {
                lblMsg.Text = "Please select '<b> YES / NO for VESSEL NOTIFIED OF CLOSURE. </b>'.";
                btn_Tab_Click(btnOC, e);
                radCNyes.Focus();
                return;
            }

            if (radCNyes.Checked)
            {
                if (txtNotifyDate.Text.Trim() == "")
                {
                    lblMsg.Text = "Please enter notification date.";
                    btn_Tab_Click(btnOC, e);
                    txtNotifyDate.Focus();
                    return;
                }
                if (!DateTime.TryParse(txtNotifyDate.Text, out d))
                {
                    lblMsg.Text = "Please enter valid date.";
                    btn_Tab_Click(btnOC, e);
                    txtNotifyDate.Focus();
                    return;
                }
            }
        }


        string FileName = "";
        byte[] FileContent = new byte[0];

        if (flpUpload.HasFile)
        {
            string TempNameOnly = System.IO.Path.GetFileNameWithoutExtension(flpUpload.FileName);
            string TempExtOnly = System.IO.Path.GetExtension(flpUpload.FileName);
            if (btnClipText.Text.Trim() != "")
            {
                FileName = btnClipText.Text.Trim();
            }
            else
            {
                FileName = TempNameOnly + DateTime.Now.ToString("dd-mm-yyy-hh-mm-tt") + TempExtOnly;
            }

            FileContent = flpUpload.FileBytes;
        }
        
        try
        {

            Common.Set_Procedures("[DBO].[ER_G118_InsertUpdate_ER_G118_Report_Office]");
            Common.Set_ParameterLength(17);
            Common.Set_Parameters(
                new MyParameter("@FORMNO", FormNo),
                new MyParameter("@REPORTID", ReportId),
                new MyParameter("@VESSELCODE", VesselCode),
                new MyParameter("@OfficeComment", txtOfficeComment.Text.Trim()),
                new MyParameter("@OfficeCommentVarefiedBy", txtOffCommVarefiedBy.Text.Trim()),
                new MyParameter("@OfficeCommentVarefiedDate", (txtOffVarefiedDate.Text.Trim() == "") ? DBNull.Value : (object)txtOffVarefiedDate.Text.Trim()),
                new MyParameter("@VerifiedPersonPOS", txtVarifiedPOS.Text.Trim()),
                new MyParameter("@IS_CLOSED", (radICyes.Checked ? "Y" : (radICno.Checked ? "N" : ""))),
                new MyParameter("@ClosureDate", (txtCD.Text.Trim() == "") ? DBNull.Value : (object)txtCD.Text.Trim()),
                new MyParameter("@ClosureRemarks", txtCloserRemarks.Text.Trim()),
                new MyParameter("@ClosureEvidence", FileName),
                new MyParameter("@CloserFile", FileContent),
                new MyParameter("@CLOSEDBY", txtCB.Text.Trim()),
                new MyParameter("@IS_CLOSURE_NOTIFIED", (radCNyes.Checked ? "Y" : (radCNno.Checked ? "N" : ""))),
                new MyParameter("@NOTIFYDATE", (txtNotifyDate.Text.Trim() == "") ? DBNull.Value : (object)txtNotifyDate.Text.Trim()),
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
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT ClosureEvidence,CloserFile FROM [dbo].[ER_G118_Report_Office] WHERE VESSELCODE='" + VesselCode + "' AND ReportId =" + ReportId);
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
}