using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class Popup_BreakDown1 : System.Web.UI.Page
{
	
    public string ComponentCode
    {
        set { ViewState["CC"] = value; }
        get { return ViewState["CC"].ToString(); }
    }
    public string ModifySpare
    {
        set { ViewState["ModifySpare"] = value; }
        get { return ViewState["ModifySpare"].ToString(); }
    }
    public int ComponentId
    {
        set { ViewState["CI"] = value; }
        get { return Common.CastAsInt32(ViewState["CI"]); }
    }
    public int HistoryId
    {
        set { ViewState["HistoryId"] = value; }
        get { return Common.CastAsInt32(ViewState["HistoryId"]); }
    }
    public string vesselCode
    {
        set { ViewState["vesselCode"] = value; }
        get { return Convert.ToString(ViewState["vesselCode"]); }
    }
    public bool closed
    {
        get  { return (txtCompletionDt.Text.Trim() != ""); }

    }
    public string UserType
    {
        set { ViewState["UserType"] = value; }
        get { return Convert.ToString(ViewState["UserType"]); }
    }

    public static DataTable dtSpareDetails001;

    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        string mode = "" + Request.QueryString["mode"];
        if (mode == "strict")
            UserType = "S";          
        else
            UserType = Session["UserType"].ToString();
        //------------------------------------------

        if (UserType == "O")
        {
            btnAddSpare.Enabled = false;
            btnRefresh.Enabled = false;
            btnSave.Enabled = false;
            btnClosureSave.Enabled = false;
            imgAddSpare.Enabled = false;
            btnSaveAttachment.Enabled = false;
            btnAddRemarks.Text = "Office Remarks";
            btnExport.Visible = false;
        }
        else
        {
            btnAddSpare.Enabled = true;
            btnRefresh.Enabled = true;
            btnSave.Enabled = true;
            btnClosureSave.Enabled = true;
            imgAddSpare.Enabled = true;
            btnSaveAttachment.Enabled = true;
            btnAddRemarks.Visible = false;            
        }

        ModifySpare =""+Request.QueryString["ModifySpare"];
        if (!Page.IsPostBack)
        {
            Session["SparesAdded"] = null;
            dtSpareDetails001 = null;
            if (Request.QueryString["DN"] != null)
            {
         
                lblNo.Text = Request.QueryString["DN"].ToString().Trim();
                vesselCode = lblNo.Text.Split('/').GetValue(0).ToString().Trim();
                BindBreakDownDetails();
                BindAttachment();
                btnPrint.Visible = true;
                trAttachments.Visible = true;
                btnSaveAttachment.Visible = true;
            }
            else
            {
                if (mode == "strict")
                    vesselCode = Request.QueryString["VSL"].ToString().Trim();
                else
                    vesselCode = Session["CurrentShip"].ToString().Trim();

                if (Request.QueryString["CC"] != null)
                {
                    ComponentCode = Request.QueryString["CC"].ToString();
                    ShowReportNo();
                    BindDetails();
                }
                if (Request.QueryString["EJ"] != null)
                {
                    trCompStatus.Visible = false;
                }
                else
                {
                    trCompStatus.Visible = true;
                }
            }
            OfficeRemark();
        }
  
    }
    protected void lblRCAFile_Click(object sender, EventArgs e)
    {
        string sql = " select rcano,RcaDocument from dbo.VSL_BreakDownRemarks where BreakDownNo='" + lblNo.Text + "' and VesselCode='" + vesselCode + "' ";
        DataTable dtfile = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dtfile.Rows.Count > 0)
        {
            string filename = dtfile.Rows[0]["rcano"].ToString();
            byte[] fileBytes = (byte[])dtfile.Rows[0][1];
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Type", "application/pdf");
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            Response.BinaryWrite(fileBytes);
            Response.Flush();
            Response.End();
        }

    }
    public void OfficeRemark()
    {
        string sql = "SELECT * FROM VSL_BreakDownRemarks WHERE VesselCode = '" + vesselCode.Trim() + "' AND BreakDownNo = '" + lblNo.Text + "' and Is_Closed='Y' ";
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (Dt.Rows.Count > 0)
        {
            pnlofficecomments.Visible = true;
            int classid = Common.CastAsInt32(Dt.Rows[0]["Classification"]);
            switch (classid)
            {
                case 1:
                    lblclassification.Text = "Minor";
                    break;
                case 2:
                    lblclassification.Text = "Major";
                    break;
                case 3:
                    lblclassification.Text = "Severe";
                    break;
                default:
                    break;
            }
            lblRCAFile.Text = Dt.Rows[0]["RCANo"].ToString();
        }
    }
    public void BindDetails()
    {
        string strSQL = "SELECT ComponentId,ComponentCode,ComponentName,ClassEquip,CriticalEquip FROM ComponentMaster WHERE ComponentCode = '" + ComponentCode.Trim() + "'";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dt.Rows.Count <= 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "fasfd", "alert('Invalid component code.');window.close();", true);
            btnSave.Visible = false;
            btnSaveAttachment.Visible = false;
        }
        else
        {
            lblCompCode.Text = ComponentCode.Trim();
            lblCompName.Text = dt.Rows[0]["ComponentName"].ToString().Trim();
            ComponentId = Common.CastAsInt32(dt.Rows[0]["ComponentId"].ToString());

            if (dt.Rows[0]["ClassEquip"].ToString() == "True")
            {
                lblCStatus.Text = "Class Equipment";
            }
            if (dt.Rows[0]["CriticalEquip"].ToString() == "True")
            {
                lblCStatus.Text += " | Critical Equipment";
            }
        }
    }
    public void BindBreakDownDetails()
    {
        string strSQL = "SELECT VDM.VesselCode,VDM.HistoryId,VDM.ComponentId,CM.ComponentCode,CM.ComponentName,CM.ClassEquip,CM.CriticalEquip,BreakDownNo,CASE REPLACE(CONVERT(VARCHAR(15), ReportDt,106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(15), ReportDt,106),' ','-') END  AS ReportDt , CASE REPLACE(CONVERT(VARCHAR(15), TargetDt,106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE  REPLACE(CONVERT(VARCHAR(15), TargetDt,106),' ','-') END AS TargetDt, CASE REPLACE(CONVERT(VARCHAR(15), CompletionDt,106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(15), CompletionDt,106),' ','-') END AS CompletionDt,Vessel,Spares,ShoreAssist,Drydock,Guarentee,DefectDetails,RepairsDetails,SparesOnBoard,RqnNo,CASE REPLACE(CONVERT(VARCHAR(15),RqnDate,106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(15),RqnDate,106),' ','-') END AS  RqnDate,SparesOrdered,SparesRequired,Supdt,ChiefOfficer,ChiefEngg,VDM.[Status],VDM.CompStatus,CJM.DescrSh, " +
                        "CASE WHEN (VDM.TargetDt < getdate() AND VDM.CompletionDt IS NULL) THEN 'OD' ELSE '' END AS [DueStatus] " +
                        "FROM VSL_BreakDownMaster VDM " +
                        "LEFT JOIN ComponentMaster CM ON CM.ComponentId = VDM.ComponentId " +
                        "LEFT JOIN VSL_VesselJobUpdateHistory JH ON VDM.VesselCode = JH.VesselCode AND VDM.ComponentId = JH.ComponentId AND VDM.HistoryId = JH.HistoryId " +
                        "LEFT JOIN ComponentsJobMapping CJM ON CJM.ComponentId = JH.ComponentId AND CJM.CompJobId = JH.CompJobId " +
                        "WHERE BreakDownNo = '" + lblNo.Text.Trim() + "' ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dt.Rows.Count > 0)
        {
            ComponentId = Common.CastAsInt32(dt.Rows[0]["ComponentId"].ToString());
            //lblNo.Text = dt.Rows[0]["DefectNo"].ToString();
            lblCompCode.Text = dt.Rows[0]["ComponentCode"].ToString();
            lblCompName.Text = dt.Rows[0]["ComponentName"].ToString();
            HistoryId = Common.CastAsInt32(dt.Rows[0]["HistoryId"]);

            lblCStatus.Text =(dt.Rows[0]["ClassEquip"].ToString()=="True")?"Class Equipment":"";
            lblCStatus.Text += (dt.Rows[0]["CriticalEquip"].ToString() == "True") ? " | Critical Equipment" : "";

            txtReportDt.Text = dt.Rows[0]["ReportDt"].ToString();
            txtTargetDt.Text = dt.Rows[0]["TargetDt"].ToString();
            txtCompletionDt.Text = dt.Rows[0]["CompletionDt"].ToString();
            lblCompDt.Text = txtCompletionDt.Text;
            chkVessel.Checked = (dt.Rows[0]["Vessel"].ToString() == "Y");
            chkSpares.Checked = (dt.Rows[0]["Spares"].ToString() == "Y");
            chkShAssist.Checked = (dt.Rows[0]["ShoreAssist"].ToString() == "Y");
            ChkDrydock.Checked = (dt.Rows[0]["Drydock"].ToString() == "Y");
            chkGuarantee.Checked = (dt.Rows[0]["Guarentee"].ToString() == "Y");
            txtDefectdetails.Text = dt.Rows[0]["DefectDetails"].ToString();
            txtRepairsCarriedout.Text = dt.Rows[0]["RepairsDetails"].ToString();
            ddlCompStatus.SelectedValue = (dt.Rows[0]["CompStatus"].ToString() == "W" ? "1" : "2");
            ddlSOB.SelectedValue = (dt.Rows[0]["SparesOnBoard"].ToString() == "Y" ? "1" : "2");
            txtRqnNo.Text = dt.Rows[0]["RqnNo"].ToString();
            txtRqnDt.Text = dt.Rows[0]["RqnDate"].ToString();
            ddlSparesReqd.SelectedValue = (dt.Rows[0]["SparesRequired"].ToString() == "Y" ? "1" : "2");
            txtSupdt.Text = dt.Rows[0]["Supdt"].ToString();
            txtCE.Text = dt.Rows[0]["ChiefEngg"].ToString();
            txtCO.Text = dt.Rows[0]["ChiefOfficer"].ToString();
            
            if (dt.Rows[0]["DueStatus"].ToString() == "OD")
            {
                txtTargetDt.Attributes.Add("class", "highlightrow");
            }

            ddlSparesReqd_SelectedIndexChanged(null, null);
            //if (ddlSparesReqd.SelectedValue == "1")
            //{
                BindSpares();
            //}

            //if (Request.QueryString["FM"] == null)
            //{
            setButtons();
            btnExport.Visible = true;
        }

        if(txtCompletionDt.Text.Trim()!="")
        {
            btnSave.Visible = false;
            btnOpenPopupCloseDefect.Visible = false;
        }
        else
        {
            btnSave.Visible = true;
            string SQL = "SELECT * FROM VSL_BreakDownMaster WHERE [BreakDownNo] = '" + lblNo.Text.Trim() + "' ";
            DataTable dt001 = Common.Execute_Procedures_Select_ByQuery(SQL);            
            btnOpenPopupCloseDefect.Visible = true && (dt001.Rows.Count > 0);
        }
    }
    public void setButtons()
    {
        if (UserType == "O")
        {
            ShowClosureRead();
            btnAddSpare.Enabled = false;
            btnRefresh.Enabled = false;
            btnSave.Enabled = false;
            btnClosureSave.Enabled = false;
            imgAddSpare.Enabled = false;

            ddlSparesReqd.Enabled = false;
            ddlSOB.Enabled = false;
            taddspates.Visible = false;
            btnSaveAttachment.Enabled = false;
        }
        else
        {           

            if (txtCompletionDt.Text != "") // closure done
            {
                ShowClosureRead();
                if (("" + Request.QueryString["ModifySpare"]) == "Y")
                    btnAddSpare.Enabled = true;
                else
                    btnAddSpare.Enabled = false;

                btnRefresh.Enabled = false;
                btnSave.Enabled = false;
                imgAddSpare.Enabled = false;
                btnSaveAttachment.Enabled = false;
                btnOpenPopupCloseDefect.Visible = false;
            }
            else
            {
                btnAddSpare.Enabled = true;
                btnRefresh.Enabled = true;
                btnSave.Enabled = true;
                imgAddSpare.Enabled = true;
                btnSaveAttachment.Enabled = true;
                btnOpenPopupCloseDefect.Visible = true;

            }
        }
    }
    public void DeleteSpares()
    {
        string strSQL = "DELETE FROM VSL_BreakDownSpareDetails WHERE BreakDownNo ='" + lblNo.Text.Trim() + "' ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(strSQL);

    }
    public void ShowReportNo()
    {

        string SQL = "SELECT '" + vesselCode + "/" + DateTime.Today.Year.ToString().Substring(2) + "/" + "' + CONVERT(VARCHAR, ISNULL(MAX(CONVERT(INT,SUBSTRING(BreakDownNo,8,7))),0) +1) FROM VSL_BreakDownMaster WHERE LEFT(BreakDownNo,7) = '" + vesselCode + "/" + DateTime.Today.Year.ToString().Substring(2) + "/" + "' ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        lblNo.Text = dt.Rows[0][0].ToString();
    }
    public void BindCompSpares()
    {
        
        ddlSparesList.Items.Clear();

        // NEW LINES - 30 DEC 2013
            string Parents = ProjectCommon.getParentComponents_Chain(ComponentId);
            string strSpares = "SELECT VESSELCODE+'#'+CONVERT(VARCHAR,COMPONENTID)+'#'+OFFICE_SHIP+'#'+CONVERT(VARCHAR,SPAREID) AS PKID ,SPARENAME + ' - ' + PARTNO AS SPARENAME from VSL_VesselSpareMaster WHERE VesselCode = '" + vesselCode + "' AND ComponentId IN (" + ComponentId + "," + Parents + ") order by SPARENAME ";
        // ==========

        DataTable dtspares = Common.Execute_Procedures_Select_ByQuery(strSpares);
        if (dtspares.Rows.Count > 0)
        {
            ddlSparesList.DataSource = dtspares;
            ddlSparesList.DataTextField = "SPARENAME";
            ddlSparesList.DataValueField = "PKID";
            ddlSparesList.DataBind();

            ddlSparesList_Pop.DataSource = dtspares;
            ddlSparesList_Pop.DataTextField = "SPARENAME";
            ddlSparesList_Pop.DataValueField = "PKID";
            ddlSparesList_Pop.DataBind();
        }
        ddlSparesList.Items.Insert(0, new ListItem("< SELECT >","0"));
        ddlSparesList_Pop.Items.Insert(0, new ListItem("< SELECT >", "0"));
    }
    public void BindSpares()
    {
        string strSQL = "SELECT VSM.VESSELCODE+'#'+CONVERT(VARCHAR,VSM.COMPONENTID)+'#'+VSM.OFFICE_SHIP+'#'+CONVERT(VARCHAR,VSM.SPAREID) AS RowId, VSM.VesselCode,VSM.ComponentId,VSM.Office_Ship,VSM.SpareId,VSM.SpareName,VSM.Maker,VSM.PartNo,VDD.QtyCons,VDD.QtyRob FROM VSL_BreakDownSpareDetails VDD " +
                        "INNER JOIN VSL_VesselSpareMaster VSM ON VSM.VesselCode = VDD.VesselCode AND VSM.ComponentId = VDD.ComponentId AND VSM.Office_Ship = VDD.Office_Ship AND VSM.SpareId = VDD.SpareId " +
                        "WHERE VDD.BreakDownNo = '" + lblNo.Text.Trim() + "' AND ISNULL(VDD.STATUS,'A')='A' ";
        dtSpareDetails001 = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dtSpareDetails001.Rows.Count > 0)
        {
            rptComponentSpares.DataSource = dtSpareDetails001;
            rptComponentSpares.DataBind();

            rptComponentSpares_Pop.DataSource = dtSpareDetails001;
            rptComponentSpares_Pop.DataBind();

            ddlSparesReqd.SelectedValue = "1";
            BindCompSpares();
            tblSpares.Visible = true;
            tdSpareConsumptionPopup.Visible = true;
        }
       

    }
    public void ShowComponentSpare()
    {
        rptComponentSpares.DataSource = null;
        rptComponentSpares.DataBind();

        rptComponentSpares_Pop.DataSource = null;
        rptComponentSpares_Pop.DataBind();

        dtSpareDetails001 = SparesDataTable();
            rptComponentSpares.DataSource = dtSpareDetails001;
            rptComponentSpares.DataBind();

        rptComponentSpares_Pop.DataSource = dtSpareDetails001;
        rptComponentSpares_Pop.DataBind();
    }    
    public Boolean IsValidated()
    {
        if (txtReportDt.Text.Trim() == "")
        {
            ProjectCommon.ShowMessage("Please enter report date.");
            txtReportDt.Focus();
            return false;
        }
        DateTime dt;
        if (!(DateTime.TryParse(txtReportDt.Text.Trim(), out dt)))
        {
            ProjectCommon.ShowMessage("Please enter valid report date.");
            txtReportDt.Focus();
            return false;

        }
        if (dt>DateTime.Today)
        {
            ProjectCommon.ShowMessage("Report date can't more than today.");
            txtReportDt.Focus();
            return false;

        }
        DateTime dt_target; 
        if (txtTargetDt.Text.Trim() != "")
        {
            if (!(DateTime.TryParse(txtTargetDt.Text.Trim(), out dt_target)))
            {
                ProjectCommon.ShowMessage("Please enter valid target date.");
                txtTargetDt.Focus();
                return false;

            }
            if (dt_target < dt)
            {
                ProjectCommon.ShowMessage("Target date should not be less than report date.");
                txtTargetDt.Focus();
                return false;
            }
        }
        DateTime dt_comp; 
        if (txtCompletionDt.Text.Trim() != "")
        {
            if (!(DateTime.TryParse(txtCompletionDt.Text.Trim(), out dt_comp)))
            {
                ProjectCommon.ShowMessage("Please enter valid completion date.");
                txtCompletionDt.Focus();
                return false;

            }
        }
        //if (txtDefectdetails.Text.Trim().Length > 1000)
        //{
        //    ProjectCommon.ShowMessage("Details can not be more than 1000 characters.");
        //    txtDefectdetails.Focus();
        //    return false;
        //}
        //if (txtRepairsCarriedout.Text.Trim().Length > 1000)
        //{
        //    ProjectCommon.ShowMessage("Repair details can not be more than 1000 characters.");
        //    txtRepairsCarriedout.Focus();
        //    return false;
        //}
        if (trCompStatus.Visible)
        {
            if (ddlCompStatus.SelectedValue == "0")
            {
                ProjectCommon.ShowMessage("Please select component status");
                ddlCompStatus.Focus();
                return false;
            }
        }
        if (ddlSparesReqd.SelectedValue == "1")
        {
            if (ddlSOB.SelectedValue == "2")
            {
                if (txtRqnNo.Text.Trim() == "")
                {
                    ProjectCommon.ShowMessage("Please enter rqn no.");
                    txtRqnNo.Focus();
                    return false;
                }
                if (txtRqnDt.Text.Trim() == "")
                {
                    ProjectCommon.ShowMessage("Please enter rqn date.");
                    txtRqnDt.Focus();
                    return false;
                }
            }
        }
        if (txtRqnDt.Text != "")
        {
            if (!(DateTime.TryParse(txtRqnDt.Text.Trim(), out dt)))
            {
                ProjectCommon.ShowMessage("Please enter valid date.");
                txtRqnDt.Focus();
                return false;
            }
        }

        return true;
    }
    //protected void btnAddSpares_Click(object sender, ImageClickEventArgs e)
    //{
    //    //if (ddlVessels.SelectedIndex != 0)
    //    //{
    //    //    if (tvComponents.SelectedNode != null)
    //    //    {
    //    //        if (tvComponents.SelectedNode.Value.ToString().Trim() == "" || tvComponents.SelectedNode.Value.ToString().Trim().Length == 3)
    //    //        {
    //    //            ProjectCommon.ShowMessage("Please Select a component.");
    //    //            return;
    //    //        }
    //    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "openaddsparewindow('" + tvComponents.SelectedNode.Value.ToString().Trim() + "','" + ddlVessels.SelectedValue.ToString().Trim() + "',' ');");
    //    //    }
    //    //    else
    //    //    {
    //    //        ProjectCommon.ShowMessage("Please Select a component.");
    //    //    }
    //    //}
    //    //else
    //    //{
    //    //    ProjectCommon.ShowMessage("Please select a Vessel.");
    //    //}
    //}
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!IsValidated())
        {
            return;
        }
        
            string compStatus = "";
            if (trCompStatus.Visible)
            {
                compStatus = ddlCompStatus.SelectedValue == "1" ? "W" : "N";
            }
            else
            {
                compStatus = " ";
            }
            object OReportDate , TargetDate , CompletionDt , RqnDate ;

            if (txtReportDt.Text.Trim() != "") OReportDate = (object)txtReportDt.Text; else OReportDate = DBNull.Value;
            if (txtTargetDt.Text.Trim() != "") TargetDate = (object)txtTargetDt.Text; else TargetDate = DBNull.Value;
            if (txtCompletionDt.Text.Trim() != "") CompletionDt = (object)txtCompletionDt.Text; else CompletionDt = DBNull.Value;
            if (txtRqnDt.Text.Trim() != "") RqnDate = (object)txtRqnDt.Text; else RqnDate = DBNull.Value;
            
            Common.Set_Procedures("sp_InsertBreakDown");
            Common.Set_ParameterLength(22);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", vesselCode),
                new MyParameter("@ComponentId", ComponentId),
                new MyParameter("@BreakDownNo", lblNo.Text.Trim()),
                new MyParameter("@HistoryId", HistoryId),
                new MyParameter("@ReportDt", (OReportDate.ToString()=="")?DBNull.Value:OReportDate),
                new MyParameter("@TargetDt", (TargetDate.ToString() == "") ? DBNull.Value : TargetDate),
                new MyParameter("@CompletionDt", (CompletionDt.ToString() == "") ? DBNull.Value : CompletionDt),
                new MyParameter("@Vessel", chkVessel.Checked ? "Y" : "N"),
                new MyParameter("@Spares", chkSpares.Checked ? "Y" : "N"),
                new MyParameter("@ShoreAssist", chkShAssist.Checked ? "Y" : "N"),
                new MyParameter("@Drydock", ChkDrydock.Checked ? "Y" : "N"),
                new MyParameter("@Guarentee", chkGuarantee.Checked ? "Y" : "N"),
                new MyParameter("@DefectDetails", txtDefectdetails.Text.Trim().Replace("'","`")),
                new MyParameter("@RepairsDetails", txtRepairsCarriedout.Text.Trim().Replace("'", "`")),
                new MyParameter("@SparesOnBoard", ddlSOB.SelectedValue == "1" ? "Y" : "N"),
                new MyParameter("@RqnNo", txtRqnNo.Text.Trim()),
                new MyParameter("@RqnDate", (RqnDate.ToString() == "") ? DBNull.Value : RqnDate),
                new MyParameter("@SparesRequired", ddlSparesReqd.SelectedValue == "1" ? "Y" : "N"),
                new MyParameter("@Supdt", txtSupdt.Text.Trim()),
                new MyParameter("@ChiefOfficer", txtCO.Text.Trim()),
                new MyParameter("@ChiefEngg", txtCE.Text.Trim()),
                new MyParameter("@CompStatus", compStatus)
                );

            DataSet dsComponents = new DataSet();
            dsComponents.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsComponents);

            if (res)
            {
                //DeleteSpares();

                foreach (RepeaterItem item in rptComponentSpares.Items)
                {
                    HiddenField hfdComponentId = (HiddenField)item.FindControl("hfdComponentId");
                    HiddenField hfOffice_Ship = (HiddenField)item.FindControl("hfOffice_Ship");
                    HiddenField hfSpareId = (HiddenField)item.FindControl("hfSpareId");

                    
                    Label lblQtyCons = (Label)item.FindControl("lblQtyCons");
                    Label lblQtyRob = (Label)item.FindControl("lblQtyRob");

                    Common.Set_Procedures("sp_InsertSpares_BreakDown1");
                    Common.Set_ParameterLength(7);
                    Common.Set_Parameters(
                        new MyParameter("@VesselCode", vesselCode),
                        new MyParameter("@ComponentId", hfdComponentId.Value),
                        new MyParameter("@Office_Ship", hfOffice_Ship.Value),
                        new MyParameter("@SpareId", hfSpareId.Value),
                        new MyParameter("@BreakDownNo", lblNo.Text.Trim()),
                        new MyParameter("@QtyCons", lblQtyCons.Text.Trim()),
                        new MyParameter("@QtyRob", lblQtyRob.Text.Trim())
                        );

                    DataSet dsComponentSpare = new DataSet();
                    dsComponentSpare.Clear();
                    Boolean result;
                    result = Common.Execute_Procedures_IUD(dsComponentSpare);
                }
                ProjectCommon.ShowMessage("Breakdown Added Successfully.");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "refreshparent();",true);
            }
            else
            {
                ProjectCommon.ShowMessage("Unable to Add Breakdown.Error :" + Common.getLastError());
            }        
    }
    protected void ddlSparesReqd_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlSparesReqd.SelectedValue == "1")
        //{
        //    txtRqnNo.Attributes.Add("required", "yes");
        //    txtRqnDt.Attributes.Add("required", "yes");
        //}
        //else
        //{
        //    txtRqnNo.Attributes.Remove("required");
        //    txtRqnDt.Attributes.Remove("required");
        //}

        if (ddlSparesReqd.SelectedValue == "1")
        {   
            BindCompSpares();
            //ShowComponentSpare();
            tblSpares.Visible = true;
            tdSpareConsumptionPopup.Visible = true;
        }
        else
        {
            ddlSOB.SelectedIndex = 0;
            tblSpares.Visible = false;
            tdSpareConsumptionPopup.Visible = false;
        }
        
    }
    protected void btnAddSpare_Click(object sender, EventArgs e)
    {
        bool Modifying = false;
        Modifying = Request.QueryString["DN"] != null;
        if (Modifying)
        {
            if (ddlSparesList.SelectedIndex == 0)
            {
                ProjectCommon.ShowMessage("Please select a spare.");
                ddlSparesList.Focus();
                return;
            }
            else if (txtQtyCon.Text.Trim() == "")
            {
                ProjectCommon.ShowMessage("Please enter consumed quantity.");
                txtQtyCon.Focus();
                return;
            }
            else if (txtQtyRob.Text.Trim() == "")
            {
                ProjectCommon.ShowMessage("Please enter rob quantity.");
                txtQtyRob.Focus();
                return;
            }
            else
            {
                string _VesselCode = "";
                int _ComponentId = 0;
                string _Office_Ship = "";
                int _SpareId = 0;
                ProjectCommon.setSpareKeys(ddlSparesList.SelectedValue, ref _VesselCode, ref _ComponentId, ref _Office_Ship, ref _SpareId);

                int qtycons, qtyrob;
                qtycons = Common.CastAsInt32(txtQtyCon.Text);
                qtyrob = Common.CastAsInt32(txtQtyRob.Text);
                Common.Execute_Procedures_Select_ByQuery("EXEC [dbo].[sp_InsertSpares_BreakDown1] '" + _VesselCode + "'," + _ComponentId + ",'" + _Office_Ship + "'," + _SpareId + ",'" + lblNo.Text.Trim() +"'," + qtycons + "," + qtyrob);
                BindSpares();
            }
        }
        else
        {
            if (dtSpareDetails001 == null)
            {
                dtSpareDetails001 = SparesDataTable();
            }
            if (ddlSparesList.SelectedIndex == 0)
            {
                ProjectCommon.ShowMessage("Please select a spare.");
                ddlSparesList.Focus();
                return;
            }
            if (dtSpareDetails001 != null)
            {
                foreach (DataRow row in dtSpareDetails001.Rows)
                {
                    if (row["RowId"].ToString() == ddlSparesList.SelectedValue.ToString())
                    {
                        ProjectCommon.ShowMessage("Selected spare already added.");
                        ddlSparesList.Focus();
                        return;
                    }
                }
            }
            if (txtQtyCon.Text.Trim() == "")
            {
                ProjectCommon.ShowMessage("Please enter consumed quantity.");
                txtQtyCon.Focus();
                return;
            }
            if (txtQtyRob.Text.Trim() == "")
            {
                ProjectCommon.ShowMessage("Please enter rob quantity.");
                txtQtyRob.Focus();
                return;
            }
            rptComponentSpares.DataSource = null;
            rptComponentSpares.DataBind();

            rptComponentSpares_Pop.DataSource = null;
            rptComponentSpares_Pop.DataBind();

            string _VesselCode = "";
            int _ComponentId = 0;
            string _Office_Ship = "";
            int _SpareId = 0;
            ProjectCommon.setSpareKeys(ddlSparesList.SelectedValue, ref _VesselCode, ref _ComponentId, ref _Office_Ship, ref _SpareId);
            string SQL = "SELECT VesselCode,ComponentId,Office_Ship,SpareId,SpareName,Maker,PartNo," + txtQtyCon.Text.Trim() + " AS QtyCons," + txtQtyRob.Text.Trim() + " AS QtyRob FROM VSL_VesselSpareMaster WHERE VesselCode = '" + _VesselCode + "' AND ComponentId = " + _ComponentId + " AND Office_Ship='" + _Office_Ship + "' AND SpareId =" + _SpareId;
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

            DataRow dr = dtSpareDetails001.NewRow();

            dr["RowId"] = ddlSparesList.SelectedValue;
            dr["VesselCode"] = dt.Rows[0]["VesselCode"].ToString();
            dr["ComponentId"] = dt.Rows[0]["ComponentId"].ToString();
            dr["Office_Ship"] = dt.Rows[0]["Office_Ship"].ToString();
            dr["SpareId"] = dt.Rows[0]["SpareId"].ToString();
            dr["SpareName"] = dt.Rows[0]["SpareName"].ToString();
            dr["Maker"] = dt.Rows[0]["Maker"].ToString();
            dr["PartNo"] = dt.Rows[0]["PartNo"].ToString();
            dr["QtyCons"] = dt.Rows[0]["QtyCons"].ToString();
            dr["QtyRob"] = dt.Rows[0]["QtyRob"].ToString();

            dtSpareDetails001.Rows.Add(dr);
            if (dtSpareDetails001.Rows[0]["SpareId"].ToString() == "")
            {
                dtSpareDetails001.Rows[0].Delete();
            }
            dtSpareDetails001.AcceptChanges();
            rptComponentSpares.DataSource = dtSpareDetails001;
            rptComponentSpares.DataBind();

            rptComponentSpares_Pop.DataSource = dtSpareDetails001;
            rptComponentSpares_Pop.DataBind();

            if (Request.QueryString["JID"] != null)
            {
                Session.Add("SparesAdded", dtSpareDetails001);
            }
        }
    }
    protected void imgDel_OnClick(object sender, EventArgs e)
    {
        string spareid = ((ImageButton)sender).CommandArgument.Trim();
        for (int i = 0; i <= dtSpareDetails001.Rows.Count - 1; i++)
        {
            if (dtSpareDetails001.Rows[i]["RowId"].ToString().Trim() == spareid)
            {
                dtSpareDetails001.Rows.RemoveAt(i);
                break;
            }
        }
        rptComponentSpares.DataSource = dtSpareDetails001;
        rptComponentSpares.DataBind();

        rptComponentSpares_Pop.DataSource = dtSpareDetails001;
        rptComponentSpares_Pop.DataBind();
    }
    protected void imgDeleteSpare_OnClick(object sender, EventArgs e)
    {

        string spareKEY = ((ImageButton)sender).CommandArgument.Trim();

        string _VesselCode = "";
        int _ComponentId = 0;
        string _Office_Ship = "";
        int _SpareId = 0;
        ProjectCommon.setSpareKeys(spareKEY, ref _VesselCode, ref _ComponentId, ref _Office_Ship, ref _SpareId);
        string sql = "UPDATE VSL_BreakDownSpareDetails SET STATUS='D' WHERE VesselCode = '" + _VesselCode + "' AND ComponentId =" + _ComponentId +" AND Office_Ship ='" + _Office_Ship + "' AND SpareId =" + _SpareId + " AND BreakDownNo = '" + lblNo.Text.Trim() + "'";
        Common.Execute_Procedures_Select_ByQuery(sql);        
        BindSpares();
    }
        
    public DataTable SparesDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("RowId");
        dt.Columns.Add("VesselCode");
        dt.Columns.Add("ComponentId");
        dt.Columns.Add("Office_Ship");
        dt.Columns.Add("SpareId");
        dt.Columns.Add("SpareName");
        dt.Columns.Add("Maker");
        dt.Columns.Add("PartNo");
        dt.Columns.Add("QtyCons");
        dt.Columns.Add("QtyRob");

        DataRow dr = dt.NewRow();
        dr["RowId"] = "";
        dr["VesselCode"] = "";
        dr["ComponentId"] = "";
        dr["Office_Ship"] = "";
        dr["SpareId"] = "";
        dr["SpareName"] = "";
        dr["Maker"] = "";
        dr["PartNo"] = "";
        dr["QtyCons"] = "";
        dr["QtyRob"] = "";

        dt.Rows.Add(dr);
        return dt;
    }    
    protected void ddlSOB_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlSOB.SelectedValue == "1")
        //{
        //    txtRqnNo.Attributes.Add("required", "yes");
        //    txtRqnDt.Attributes.Add("required", "yes");
        //}
        //else
        //{
        //    txtRqnNo.Attributes.Remove("required");
        //    txtRqnDt.Attributes.Remove("required");
        //}
    }
    protected void imgAddSpare_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "openaddsparewindow('" + lblCompCode.Text.Trim() + "','" + vesselCode + "',' ');",true);
    }
    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindCompSpares();
    }
    protected void ddlSparesList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSparesList.SelectedIndex > 0)
        {
            txtQtyRob.Text = ProjectCommon.getROB(ddlSparesList.SelectedValue,DateTime.Today).ToString();
        }
        else
        {
            txtQtyRob.Text = "";
        }
    }
    protected void btnClosure_Click(object sender, EventArgs e)
    {
        BindCompSpares();
        ShowClosureEdit(); 
    }
    protected void btnClosureSave_Click(object sender, EventArgs e)
    {
        DateTime dt;

        

        if (ddlCurrentComponentStatus.SelectedIndex == 0)
        {
            ProjectCommon.ShowMessage("Please select Component Working.");
            ddlCurrentComponentStatus.Focus();
            return;
        }
        if (ddlCurrentComponentStatus.SelectedIndex == 2)
        {
            ProjectCommon.ShowMessage("Closuer can not be done until component is working.");
            ddlCurrentComponentStatus.Focus();
            return;
        }
        if (txtCompletionDt.Text.Trim() == "")
        {
            ProjectCommon.ShowMessage("Please enter completion date.");
            txtCompletionDt.Focus();
            return;
        }

        if (!(DateTime.TryParse(txtCompletionDt.Text.Trim(), out dt)))
        {
            ProjectCommon.ShowMessage("Please enter valid date.");
            txtCompletionDt.Focus();
            return;
        }
        else if (dt > DateTime.Today)
        {
            ProjectCommon.ShowMessage("Completion date must be less than today.");
            txtCompletionDt.Focus();
            return;
        }

        if (txtRepairsCarriedout_CloseDefect.Text.Trim() == "")
        {
            ProjectCommon.ShowMessage("Please enter repairs carried out.");
            txtRepairsCarriedout_CloseDefect .Focus();
            return;
        }

        string strClosure = "UPDATE VSL_BreakDownMaster SET CompStatus='W',RepairsDetails='" + txtRepairsCarriedout_CloseDefect.Text.Trim().Replace("'","`") + "', CompletionDt = '" + txtCompletionDt.Text.Trim() + "', Updated = 1, UpdatedOn = getdate() WHERE BreakDownNo = '" + lblNo.Text.Trim() + "' ; SELECT -1 ";
        DataTable dtClosure = Common.Execute_Procedures_Select_ByQuery(strClosure);

        if (dtClosure.Rows.Count > 0)
        {
            ProjectCommon.ShowMessage("Closed successfully.");
            
            //btnClosure.Visible = false;
        }

    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "window.open('Reports/Office_BreakdownDefectReport1.aspx?DN=" + Request.QueryString["DN"].ToString() + "', '', '');", true);        
    }

    #region ATTACHMENT
    private void BindAttachment()
    {
        string strSQL = "SELECT AttachmentId,BreakDownNo,AttachmentText,[FileName] FROM VSL_BreakDown_Attachments WHERE VesselCode = '" + vesselCode + "' AND BreakDownNo = '" + lblNo.Text.Trim() + "' AND ISNULL([Status],'A') <> 'D'";
        DataTable dtAttachment = Common.Execute_Procedures_Select_ByQuery(strSQL);

        if (dtAttachment.Rows.Count > 0)
        {
            rptAttachment.DataSource = dtAttachment;
            rptAttachment.DataBind();
            //dvAttachment.Visible = true;
        }
        else
        {
            rptAttachment.DataSource = null;
            rptAttachment.DataBind();
        }
        if (rptAttachment.Items.Count > 0)
                btnExportAttachments.Visible = true;
    }
    protected void btnSaveAttachment_Click(object sender, EventArgs e)
    {
        //if (HistoryId.Trim() == "")
        //{
        //    mbUpdateJob.ShowMessage("First update the job.", true);
        //    return;
        //}

        string SQL = "SELECT * FROM VSL_BreakDownMaster WHERE VesselCode = '" + vesselCode.Trim() + "' AND BreakDownNo = '" + lblNo.Text.Trim() + "'";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        if (dt.Rows.Count > 0)
        {
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('Please add breakdown report before uploading docs.');", true);
            return;
        }

        if (txtAttachmentText.Text.Trim()=="")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('Please enter attachment description.');", true);
            return;
        }
        FileUpload img = (FileUpload)flAttachDocs;
        Byte[] imgByte = new Byte[0];
        string FileName = "";
        if (img.HasFile && img.PostedFile != null)
        {
            HttpPostedFile File = flAttachDocs.PostedFile;

            if (flAttachDocs.PostedFile.ContentLength > 100*1024)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('Please upload file size of max 100 KB.');", true);
                return;
            }

            FileName = vesselCode + "_bd_" +lblNo.Text.Trim().Replace("/","_").ToString() + "_" + DateTime.Now.ToString("dd-MMM-yyy") + "_" + DateTime.Now.TimeOfDay.ToString().Replace(":", "-").ToString() + Path.GetExtension(File.FileName);

            Common.Set_Procedures("sp_InsertBreakDownAttachments");
            Common.Set_ParameterLength(4);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", vesselCode.Trim()),
                new MyParameter("@BreakDownNo", lblNo.Text.Trim()),
                new MyParameter("@AttachmentText", txtAttachmentText.Text.Trim()),
                new MyParameter("@FileName", FileName)

                );

            DataSet dsAttachment = new DataSet();
            dsAttachment.Clear();
            Boolean result;
            result = Common.Execute_Procedures_IUD(dsAttachment);
            if (result)
            {

                string path = Server.MapPath(ProjectCommon.getUploadFolder(DateTime.Parse(txtReportDt.Text.Trim())));
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                flAttachDocs.SaveAs(path + FileName);

                trAttachments.Visible = true;
                BindAttachment();
                
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('Document uploaded successfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('Unable to upload document.');", true);
                //mbUpdateJob.ShowMessage("Unable to upload document.Error :" + Common.getLastError(), true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('Please select a file to upload.');", true);
            //mbUpdateJob.ShowMessage("Please select a file to upload.", true);
            img.Focus();
            return;
        }
    }
    protected void DeleteAttachment_OnClick(object sender, EventArgs e)
    {
        ImageButton imgbtn = (ImageButton)sender;
        string VesselCode = imgbtn.CssClass;
        string AttachmentId = imgbtn.CommandArgument;

        Common.Set_Procedures("sp_DeleteBreakDownAttachment");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(
            new MyParameter("@VesselCode", vesselCode.Trim()),
            new MyParameter("@BreakDownNo", lblNo.Text.Trim()),            
            new MyParameter("@AttachmentId", AttachmentId),
            new MyParameter("@Mode", "D")
            );
        DataSet ds1 = new DataSet();
        Boolean res = Common.Execute_Procedures_IUD(ds1);

        if (res)
        {
            string FileName = ds1.Tables[0].Rows[0][0].ToString();
            FileName = Server.MapPath(ProjectCommon.getUploadFolder(DateTime.Parse(txtReportDt.Text.Trim())) + FileName);
            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('Attachment deleted successfully.');", true);
            //mbUpdateJob.ShowMessage("Attachment deleted successfully.", false);
            BindAttachment();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "alert('Unable to remove attachment.');", true);
            //mbUpdateJob.ShowMessage("Unable to remove attachment.Error :" + Common.getLastError(), true);
        }
    }
    #endregion
    protected void ShowClosureRead()
    {
        trClosure.Visible = true;
        lblCompDt.Visible = true;
        txtCompletionDt.Visible = false ;
        btnClosureSave.Visible = false; 
      
    }
    protected void ShowClosureEdit()
    {
        trClosure.Visible = true;
        lblCompDt.Visible = false;
        txtCompletionDt.Visible = true;
        btnClosureSave.Visible = true;
        
    }
   
    protected void btnAddRemarks_Click(object sender, EventArgs e)
    {
        string SQL = "SELECT * FROM VSL_BreakDownMaster WHERE [BreakDownNo] = '" + lblNo.Text.Trim() + "' ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        if (dt.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "openremarkswindow('" + lblNo.Text.Trim() + "');", true);
        }
        else
        {
            ProjectCommon.ShowMessage("Please add breakdown first to enter remarks.");
        }

    }

    protected void btntab1_OnClick(object sender, EventArgs e)
    {
        ShowTab(1);
    }
    protected void btntab2_OnClick(object sender, EventArgs e)
    {
        ShowTab(2);
    }
    protected void btntab3_OnClick(object sender, EventArgs e)
    {
        ShowTab(3);
    }

    public void ShowTab(int i)
    {
        divTab1.Visible = false;divTab2.Visible = false;divTab3.Visible = false;
        btntab1.CssClass = "btn";
        btntab2.CssClass = "btn";
        btntab3.CssClass = "btn";
        btnExportAttachments.Visible = false;
        if (i == 1)
        {
            divTab1.Visible = true;
            btntab1.CssClass = "btnsel";
        }
        if (i == 2)
        {
            divTab2.Visible = true;
            btntab2.CssClass = "btnsel";
        }
        if (i == 3)
        {
            divTab3.Visible = true;
            btntab3.CssClass = "btnsel";
            if (rptAttachment.Items.Count > 0)
                btnExportAttachments.Visible = true;
        }

    }


    protected void btnOpenPopupCloseDefect_Click(object sender, EventArgs e)
    {
        string SelSql = " select RepairsDetails from VSL_BreakDownMaster where VesselCode = '" + vesselCode + "' AND BreakDownNo = '" + lblNo.Text.Trim() + "' ";
        DataTable dtRD = Common.Execute_Procedures_Select_ByQuery(SelSql);
        if (dtRD.Rows.Count > 0)
        {
            txtRepairsCarriedout_CloseDefect.Text = dtRD.Rows[0][0].ToString();
        }
        
        ddlCurrentComponentStatus.SelectedIndex = 0;
        txtCompletionDt.Text = "";
        dvCloseDefect.Visible = true;
    }
    protected void btnCloseDefect_Click(object sender, EventArgs e)
    {
        dvCloseDefect.Visible = false;
        setButtons();
    }
    protected void btnExport_OnClick(object sender, EventArgs e)
    {
        try
        {
            Common.Set_Procedures("[DBO].[sp_Insert_Communication_Export]");
            Common.Set_ParameterLength(5);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", vesselCode),
                new MyParameter("@RecordType", "BREAKDOWN"),
                new MyParameter("@RecordId", Request.QueryString["DN"].ToString()),
                new MyParameter("@RecordNo", Request.QueryString["DN"].ToString()),
                new MyParameter("@CreatedBy", Session["UserName"].ToString())
            );

            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('" + ds.Tables[0].Rows[0][0].ToString() + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Sent for export successfully.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Unable to send for export.Error : " + Common.getLastError() + "');", true);

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Unable to send for export.Error : " + ex.Message + "');", true);
        }
    }
    protected void btnSendAttachment_OnClick(object sender, EventArgs e)
    {
        try
        {
            Common.Set_Procedures("[DBO].[sp_Insert_Communication_Export]");
            Common.Set_ParameterLength(5);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", vesselCode),
                new MyParameter("@RecordType", "BREAKDOWN-ATTACHMENTS"),
                new MyParameter("@RecordId", Request.QueryString["DN"].ToString()),
                new MyParameter("@RecordNo", Request.QueryString["DN"].ToString()),
                new MyParameter("@CreatedBy", Session["UserName"].ToString())
            );

            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('" + ds.Tables[0].Rows[0][0].ToString() + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Sent for export successfully.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Unable to send for export.Error : " + Common.getLastError() + "');", true);

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Unable to send for export.Error : " + ex.Message + "');", true);
        }
    }

    // Spare popup ---------------------------------------------------------------------------------------
    protected void ddlSparesList_Pop_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSparesList_Pop.SelectedIndex > 0)
        {
            txtQtyRob_Pop.Text = ProjectCommon.getROB(ddlSparesList_Pop.SelectedValue, DateTime.Today).ToString();
        }
        else
        {
            txtQtyRob_Pop.Text = "";
        }
    }
    protected void imgAddSpare_Pop_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "openaddsparewindow('" + lblCompCode.Text.Trim() + "','" + vesselCode + "',' ');", true);
    }
    protected void btnAddSpare_Pop_Click(object sender, EventArgs e)
    {
        if (dtSpareDetails001 == null)
        {
            dtSpareDetails001 = SparesDataTable();
        }
        if (ddlSparesList_Pop.SelectedIndex == 0)
        {
            ProjectCommon.ShowMessage("Please select a spare.");
            ddlSparesList_Pop.Focus();
            return;
        }
        if (dtSpareDetails001 != null)
        {
            foreach (DataRow row in dtSpareDetails001.Rows)
            {
                if (row["RowId"].ToString() == ddlSparesList_Pop.SelectedValue.ToString())
                {
                    ProjectCommon.ShowMessage("Selected spare already added.");
                    ddlSparesList_Pop.Focus();
                    return;
                }
            }
        }
        if (txtQtyCon_Pop.Text.Trim() == "")
        {
            ProjectCommon.ShowMessage("Please enter consumed quantity.");
            txtQtyCon_Pop.Focus();
            return;
        }
        if (txtQtyRob_Pop.Text.Trim() == "")
        {
            ProjectCommon.ShowMessage("Please enter rob quantity.");
            txtQtyRob_Pop.Focus();
            return;
        }
        rptComponentSpares.DataSource = null;
        rptComponentSpares.DataBind();

        rptComponentSpares_Pop.DataSource = null;
        rptComponentSpares_Pop.DataBind();

        string _VesselCode = "";
        int _ComponentId = 0;
        string _Office_Ship = "";
        int _SpareId = 0;
        ProjectCommon.setSpareKeys(ddlSparesList_Pop.SelectedValue, ref _VesselCode, ref _ComponentId, ref _Office_Ship, ref _SpareId);
        string SQL = "SELECT VesselCode,ComponentId,Office_Ship,SpareId,SpareName,Maker,PartNo," + txtQtyCon_Pop.Text.Trim() + " AS QtyCons," + txtQtyRob_Pop.Text.Trim() + " AS QtyRob FROM VSL_VesselSpareMaster WHERE VesselCode = '" + _VesselCode + "' AND ComponentId = " + _ComponentId + " AND Office_Ship='" + _Office_Ship + "' AND SpareId =" + _SpareId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        DataRow dr = dtSpareDetails001.NewRow();

        dr["RowId"] = ddlSparesList_Pop.SelectedValue;
        dr["VesselCode"] = dt.Rows[0]["VesselCode"].ToString();
        dr["ComponentId"] = dt.Rows[0]["ComponentId"].ToString();
        dr["Office_Ship"] = dt.Rows[0]["Office_Ship"].ToString();
        dr["SpareId"] = dt.Rows[0]["SpareId"].ToString();
        dr["SpareName"] = dt.Rows[0]["SpareName"].ToString();
        dr["Maker"] = dt.Rows[0]["Maker"].ToString();
        dr["PartNo"] = dt.Rows[0]["PartNo"].ToString();
        dr["QtyCons"] = dt.Rows[0]["QtyCons"].ToString();
        dr["QtyRob"] = dt.Rows[0]["QtyRob"].ToString();

        dtSpareDetails001.Rows.Add(dr);
        if (dtSpareDetails001.Rows[0]["SpareId"].ToString() == "")
        {
            dtSpareDetails001.Rows[0].Delete();
        }
        dtSpareDetails001.AcceptChanges();
        rptComponentSpares.DataSource = dtSpareDetails001;
        rptComponentSpares.DataBind();

        rptComponentSpares_Pop.DataSource = dtSpareDetails001;
        rptComponentSpares_Pop.DataBind();

        if (Request.QueryString["JID"] != null)
        {
            Session.Add("SparesAdded", dtSpareDetails001);
        }
    }
    protected void btnRefresh_Pop_Click(object sender, EventArgs e)
    {
        BindCompSpares();
    }
}
