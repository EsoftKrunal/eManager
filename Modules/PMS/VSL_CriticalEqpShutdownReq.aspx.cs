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

public partial class VSL_CriticalEqpShutdownReq : System.Web.UI.Page
{
    public bool EditAllowed
    {
        get { return Convert.ToBoolean(ViewState["EditAllowed"]); }
        set { ViewState["EditAllowed"] = value; }
    }
  

    public int componentId
    {
        set { ViewState["CompId"] = value; }
        get { return Common.CastAsInt32(ViewState["CompId"]); }
    }
    public int ShutdownId
    {
        set { ViewState["ShutdownId"] = value; }
        get { return Common.CastAsInt32(ViewState["ShutdownId"]); }
    }
    public int ExtensionId
    {
        set { ViewState["ExtensionId"] = value; }
        get { return Common.CastAsInt32(ViewState["ExtensionId"]); }
    }
    public bool ISClosed
    {
        set { ViewState["ISClosed"] = value; }
        get { return Convert.ToBoolean(ViewState["ISClosed"]); }
    }
    public string VesselCode
    {
        set { ViewState["VC"] = value; }
        get { return ViewState["VC"].ToString(); }
    }
    public string componentcode
    {
        set { ViewState["CompCode"] = value; }
        get { return ViewState["CompCode"].ToString(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------

        if (!Page.IsPostBack)
        {
            if (Request.QueryString["CompCode"] != null && Request.QueryString["VC"] != null && Request.QueryString["SD"] != null)
            {
                EditAllowed = false;
                ShutdownId = Common.CastAsInt32(Request.QueryString["SD"].ToString());
                VesselCode = Request.QueryString["VC"].ToString();
                lblSd_VesselCode.Text = VesselCode;
                BindTime();
                GetComponent();
                ShowShutdownDetails();
                BindSDExtensions();
                EnableDisable();
                ShowAttachments();
                ShowCriticalShutdownApproval();
                if (Convert.ToInt32(Request.QueryString["SD"]) > 0 && lblApprovedBy.Text.Trim()!="")
                {
                    btnClosure.Visible = txtSd_CompletedDate.Text=="";
                }
                else
                    btnClosure.Visible = false;
            }
        }
    }

    public void ShowShutdownDetails()
    {
        if (ShutdownId != 0)
        {
            string strReq = "SELECT VesselCode,ComponentId,Occasion,DefectNo,FormNo,[Version],REPLACE(CONVERT(varchar(11), RequestDate,106),' ','-') AS RequestDate,MasterCEName,Job_Defect,Pl_ShutDownTotalHrs,Pl_FromDateTime, Pl_ToDateTime,Ma_CommencedDateTime,Ma_CompletedDateTime,Maintenence_Remarks,Closed,(SELECT Approver_Name + '/ ' + REPLACE(CONVERT(varchar(11), ApprovedOn,106),' ','-') FROM VSL_CriticalShutdownApproval WHERE VesselCode = '" + VesselCode.Trim() + "' AND ShutdownId = " + ShutdownId + ") AS Approved FROM VSL_CriticalEquipShutdownRequest WHERE VesselCode = '" + VesselCode.Trim() + "' AND ShutdownId =" + ShutdownId;
            DataTable dtReq = Common.Execute_Procedures_Select_ByQuery(strReq);

            if (dtReq.Rows.Count > 0)
            {
                btnPrint.Visible = true;

                btnAddExtension.Visible = true;
                ddlOccasion.SelectedValue = dtReq.Rows[0]["Occasion"].ToString();
                txtDefectNo.Text = dtReq.Rows[0]["DefectNo"].ToString();
                txtReqdt.Text = dtReq.Rows[0]["RequestDate"].ToString();
                txtMasterName.Text = dtReq.Rows[0]["MasterCEName"].ToString();
                txtJobDefect.Text = dtReq.Rows[0]["Job_Defect"].ToString();
                txtSd_PlannedSDHrs.Text = dtReq.Rows[0]["Pl_ShutDownTotalHrs"].ToString();


                string[] FromDate = dtReq.Rows[0]["Pl_FromDateTime"].ToString().Split(' ');
                string[] FromTime = FromDate.GetValue(1).ToString().Split(':');

                DateTime d1 = Convert.ToDateTime(dtReq.Rows[0]["Pl_FromDateTime"]);

                string[] ToDate = dtReq.Rows[0]["Pl_ToDateTime"].ToString().Split(' ');
                string[] ToTime = ToDate.GetValue(1).ToString().Split(':');

                DateTime d2 = Convert.ToDateTime(dtReq.Rows[0]["Pl_ToDateTime"]);


                txtSd_PlannedFromDate.Text = d1.ToString("dd-MMM-yyyy");

                ddlSd_PlannedFromHr.SelectedValue = d1.Hour.ToString().PadLeft(2,'0');
                ddlSd_PlannedFromMin.SelectedValue = d1.Minute.ToString().PadLeft(2, '0');

                txtSd_PlannedToDate.Text = d2.ToString("dd-MMM-yyyy");

                ddlSd_PlannedToHr.SelectedValue = d2.Hour.ToString().PadLeft(2, '0');
                ddlSd_PlannedToMin.SelectedValue = d2.Minute.ToString().PadLeft(2, '0');

                //ddlSd_PlannedFromMin.SelectedValue = FromTime.GetValue(1).ToString();

                //txtSd_PlannedToDate.Text = Convert.ToDateTime(ToDate.GetValue(0).ToString()).ToString("dd-MMM-yyyy");

                //ddlSd_PlannedToHr.SelectedValue = ToTime.GetValue(0).ToString();
                //ddlSd_PlannedToMin.SelectedValue = ToTime.GetValue(1).ToString();

                ddlOccasion_SelectedIndexChanged(new object(), new EventArgs());


                

                if (dtReq.Rows[0]["Closed"].ToString() == "True")
                {
                    string[] M_FromDate = dtReq.Rows[0]["Ma_CommencedDateTime"].ToString().Split(' ');
                    string[] M_FromTime = M_FromDate.GetValue(1).ToString().Split(':');

                    string[] M_ToDate = dtReq.Rows[0]["Ma_CompletedDateTime"].ToString().Split(' ');
                    string[] M_ToTime = M_ToDate.GetValue(1).ToString().Split(':');

                    txtSd_MCommencedDate.Text = Convert.ToDateTime(M_FromDate.GetValue(0).ToString()).ToString("dd-MMM-yyyy");

                    ddlSd_MCommencedHr.SelectedValue = M_FromTime.GetValue(0).ToString().PadLeft(2, '0');
                    ddlSd_MCommencedMin.SelectedValue = M_FromTime.GetValue(1).ToString().PadLeft(2, '0');

                    txtSd_CompletedDate.Text = Convert.ToDateTime(M_ToDate.GetValue(0).ToString()).ToString("dd-MMM-yyyy");

                    ddlSd_MCompletedHr.SelectedValue = M_ToTime.GetValue(0).ToString().PadLeft(2,'0');
                    ddlSd_MCompletedMin.SelectedValue = M_ToTime.GetValue(1).ToString().PadLeft(2, '0');

                    txtSd_MaintenanceRemarks.Text = dtReq.Rows[0]["Maintenence_Remarks"].ToString();

                    

                }

                if (dtReq.Rows[0]["Approved"].ToString() != "")
                {
                    string SQL = "SELECT Approver_Name,Approver_Position,Approver_Remarks,REPLACE(CONVERT(varchar(11), ApprovedOn,106),' ','-') AS ApprovedOn FROM VSL_CriticalShutdownApproval WHERE VesselCode = '" + VesselCode.Trim() + "' AND ShutdownId =" + ShutdownId;
                    DataTable dtApprovedetails = Common.Execute_Procedures_Select_ByQuery(SQL);

                    txtSd_ApproverName.Text = dtApprovedetails.Rows[0]["Approver_Name"].ToString();
                    txtSd_ApproverPosition.Text = dtApprovedetails.Rows[0]["Approver_Position"].ToString();
                    txtSd_ApproverRemarks.Text = dtApprovedetails.Rows[0]["Approver_Remarks"].ToString();
                    lbl_FReqApproved.Text = dtApprovedetails.Rows[0]["ApprovedOn"].ToString();
                    btnApprove.Visible = false;
                    trOfficeApproval.Visible = true;
                }

            }

        }
        else
        {
            btnAddExtension.Visible = false;
        }

    }
    public void ShowCriticalShutdownApproval()
    {
        if (ShutdownId != 0)
        {
            string strReq = " SELECT Approver_Name,Approver_Remarks FROM VSL_CriticalShutdownApproval WHERE VesselCode = '" + VesselCode.Trim() + "' AND ShutdownId = " + ShutdownId  ;
            DataTable dtReq = Common.Execute_Procedures_Select_ByQuery(strReq);
            if (dtReq.Rows.Count > 0)
            {
                lblApprovedBy.Text = dtReq.Rows[0]["Approver_Name"].ToString();
                lblApproverRemark.Text = dtReq.Rows[0]["Approver_Remarks"].ToString();
            }
        }

    }

    public void EnableDisable()
    {
        btnSave.Visible = false;
        btnSaveExtension.Visible = false;
        btnAddExtension.Visible = false;
        btnSaveAttachment.Visible = false;
        EditAllowed = false;

        if (ShutdownId == 0)
        {
            btnSave.Visible = true;
            btnSaveExtension.Visible = true;
            btnAddExtension.Visible = true;
            btnSaveAttachment.Visible = true;
            EditAllowed = true;
            
        }
        if (ShutdownId != 0)
        {
            string sql = "SELECT ISNULL(Closed, 0) AS Closed,IssueDate FROM VSL_CriticalEquipShutdownRequest WHERE VesselCode = '" + VesselCode.Trim() + "' AND ShutdownId = " + ShutdownId;
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
            ISClosed = Convert.ToBoolean(dt.Rows[0][0].ToString());
            DateTime? IssueDate = null;
            if (!Convert.IsDBNull(dt.Rows[0]["IssueDate"]))
                IssueDate = Convert.ToDateTime(dt.Rows[0]["IssueDate"]);
            if (IssueDate == null)
            {
                btnSave.Visible = true;
                btnSaveExtension.Visible = true;
                btnAddExtension.Visible = true;
                btnSaveAttachment.Visible = true;
                EditAllowed = true;
            }
        }
    }

    private void GetComponent()
    {
        DataTable dtCompId = new DataTable();
        string strSQL = "SELECT ComponentId,ComponentCode,ComponentName,dbo.getRootPath(ComponentCode) as Parent FROM ComponentMaster WHERE ComponentCode = '" + Request.QueryString["CompCode"].ToString().Trim() + "' ";
        dtCompId = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dtCompId.Rows.Count > 0)
        {
            componentId = Common.CastAsInt32(dtCompId.Rows[0]["ComponentId"].ToString());
            componentcode = dtCompId.Rows[0]["ComponentCode"].ToString();
            lblSd_CompCode.Text = dtCompId.Rows[0]["ComponentCode"].ToString();
            lblSd_CompName.Text = dtCompId.Rows[0]["ComponentName"].ToString();
            lblParent.Text = dtCompId.Rows[0]["Parent"].ToString();
        }
        else
        {
            btnSave.Visible = false;
            btnSaveExtension.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "saf", "alert('Invalid component code.'); window.close();", true);


        }

    }
    public void BindTime()
    {
        for (int i = 0; i < 24; i++)
        {
            if (i < 10)
            {
                ddlSd_PlannedFromHr.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
                ddlSd_PlannedToHr.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));

                ddlExtFromHr.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
                ddlExtToHr.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));

                ddlSd_MCommencedHr.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
                ddlSd_MCompletedHr.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
            }
            else
            {
                ddlSd_PlannedFromHr.Items.Add(new ListItem(i.ToString(), i.ToString()));
                ddlSd_PlannedToHr.Items.Add(new ListItem(i.ToString(), i.ToString()));

                ddlExtFromHr.Items.Add(new ListItem(i.ToString(), i.ToString()));
                ddlExtToHr.Items.Add(new ListItem(i.ToString(), i.ToString()));

                ddlSd_MCommencedHr.Items.Add(new ListItem(i.ToString(), i.ToString()));
                ddlSd_MCompletedHr.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }

        for (int i = 0; i < 60; i++)
        {
            if (i < 10)
            {
                ddlSd_PlannedFromMin.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
                ddlSd_PlannedToMin.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));

                ddlExtFromMin.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
                ddlExtToMin.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));

                ddlSd_MCommencedMin.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
                ddlSd_MCompletedMin.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
            }
            else
            {
                ddlSd_PlannedFromMin.Items.Add(new ListItem(i.ToString(), i.ToString()));
                ddlSd_PlannedToMin.Items.Add(new ListItem(i.ToString(), i.ToString()));

                ddlExtFromMin.Items.Add(new ListItem(i.ToString(), i.ToString()));
                ddlExtToMin.Items.Add(new ListItem(i.ToString(), i.ToString()));

                ddlSd_MCommencedMin.Items.Add(new ListItem(i.ToString(), i.ToString()));
                ddlSd_MCompletedMin.Items.Add(new ListItem(i.ToString(), i.ToString()));

            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string sql = "select 1 from vw_VSL_CriticalEquipShutdownRequest where VesselCode='" + VesselCode + "' and ComponentCode='"+componentcode+ "' and ApprovalStatus=0 and ShutdownId<>" + ShutdownId+"";
        DataTable dt_cpmp = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt_cpmp.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('You have one open shutdown request for this component.Can not create new.');", true);
            return;
        }
            if (ddlOccasion.SelectedIndex == 0)
        {
            ddlOccasion.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please select occasion.');", true);
            return;
        }

        if (ddlOccasion.SelectedValue.Trim() == "2")
        {
            if (txtDefectNo.Text.Trim() == "")
            {
                txtDefectNo.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter DefectNo.');", true);
                return;

            }

            string DefectcheckSQL = "SELECT DefectNo FROM VSL_DefectDetailsMaster WHERE VesselCode = '" + VesselCode + "' AND DefectNo='" + txtDefectNo.Text.Trim() + "' ";
            DataTable dtCheck = Common.Execute_Procedures_Select_ByQuery(DefectcheckSQL);

            if (dtCheck.Rows.Count <= 0)
            {
                txtDefectNo.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter valid DefectNo.');", true);
                return;
            }
        }

        if (txtReqdt.Text.Trim() == "")
        {
            txtReqdt.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter request date.');", true);
            return;

        }

        DateTime dt;
        if (!DateTime.TryParse(txtReqdt.Text.Trim(), out dt))
        {
            txtReqdt.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter valid date.');", true);
            return;

        }

        if (txtMasterName.Text.Trim() == "")
        {
            txtMasterName.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter Master/ CE name.');", true);
            return;

        }
        if (txtJobDefect.Text.Trim() == "")
        {
            txtJobDefect.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter Job/ Defect.');", true);
            return;

        }
        if (txtSd_PlannedSDHrs.Text.Trim() == "")
        {
            txtSd_PlannedSDHrs.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter Planned Shutdown (Total Hours).');", true);
            return;

        }
        int i;
        if (!int.TryParse(txtSd_PlannedSDHrs.Text.Trim(), out i))
        {
            txtSd_PlannedSDHrs.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter valid Hours.');", true);
            return;

        }
        if (txtSd_PlannedFromDate.Text.Trim() == "")
        {
            txtSd_PlannedFromDate.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter from date.');", true);
            return;

        }
        DateTime dt1;
        if (! DateTime.TryParse(txtSd_PlannedFromDate.Text.Trim(),out dt1))
        {
            txtSd_PlannedFromDate.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter valid date.');", true);
            return;

        }
        if (txtSd_PlannedToDate.Text.Trim() == "")
        {
            txtSd_PlannedToDate.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter to date.');", true);
            return;

        }
        DateTime dt2;
        if (!DateTime.TryParse(txtSd_PlannedToDate.Text.Trim(), out dt2))
        {
            txtSd_PlannedToDate.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter valid date.');", true);
            return;

        }

        if (Convert.ToDateTime(txtSd_PlannedFromDate.Text.Trim()) > Convert.ToDateTime(txtSd_PlannedToDate.Text.Trim()))
        {
            txtSd_PlannedFromDate.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('To date can not be less than From date.');", true);
            return;

        }

        string FromDateTime = txtSd_PlannedFromDate.Text.Trim() + " " + ddlSd_PlannedFromHr.SelectedValue.Trim() + ":" + ddlSd_PlannedFromMin.SelectedValue.Trim();
        string ToDateTime = txtSd_PlannedToDate.Text.Trim() + " " + ddlSd_PlannedToHr.SelectedValue.Trim() + ":" + ddlSd_PlannedToMin.SelectedValue.Trim();

        try
        {
            Common.Set_Procedures("sp_InsertUpdateCriticalShutdownRequest");
            Common.Set_ParameterLength(14);
            Common.Set_Parameters(
                new MyParameter("@ShutdownId", ShutdownId),
                new MyParameter("@VesselCode", VesselCode),
                new MyParameter("@ComponentId", componentId),
                new MyParameter("@Occasion", ddlOccasion.SelectedValue.Trim()),
                new MyParameter("@DefectNo", txtDefectNo.Text.Trim()),
                new MyParameter("@FormNo", "asdf"),
                new MyParameter("@Version", "0.0"),
                new MyParameter("@RequestDate", txtReqdt.Text.Trim()),
                new MyParameter("@MasterCEName", txtMasterName.Text.Trim()),
                new MyParameter("@Job_Defect", txtJobDefect.Text.Trim()),
                new MyParameter("@Pl_ShutDownTotalHrs", txtSd_PlannedSDHrs.Text.Trim()),
                new MyParameter("@Pl_FromDateTime", FromDateTime.Trim()),
                new MyParameter("@Pl_ToDateTime", ToDateTime.Trim()),
                new MyParameter("@IssuedBy", Session["loginid"].ToString())
                );

            DataSet dsUpdate = new DataSet();
            dsUpdate.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsUpdate);

            if (res)
            {
                ShutdownId = Common.CastAsInt32(dsUpdate.Tables[0].Rows[0][0].ToString());
                ShowShutdownDetails();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Record saved successfully. Send PMS update to office.');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "refresh", "refereshparent();", true);
                btnSave.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Unable to save record.Error :" + ex.Message + Common.getLastError() + "');", true);
        }
    }
    protected void btnSaveExtension_Click(object sender, EventArgs e)
    {
        if (ShutdownId == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please initiate request first to make extension.');", true);
            return;
        }
        if (txtExtSDHrs.Text.Trim() == "")
        {
            txtExtSDHrs.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter Planned Shutdown (Total Hours).');", true);
            return;

        }
        int i;
        if (!int.TryParse(txtExtSDHrs.Text.Trim(), out i))
        {
            txtExtSDHrs.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter valid Hours.');", true);
            return;

        }
        if (txtExtFromDate.Text.Trim() == "")
        {
            txtExtFromDate.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter from date.');", true);
            return;

        }
        DateTime dt1;
        if (!DateTime.TryParse(txtExtFromDate.Text.Trim(), out dt1))
        {
            txtExtFromDate.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter valid date.');", true);
            return;

        }
        if (txtExtToDate.Text.Trim() == "")
        {
            txtExtToDate.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter to date.');", true);
            return;

        }
        DateTime dt2;
        if (!DateTime.TryParse(txtExtToDate.Text.Trim(), out dt2))
        {
            txtExtToDate.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter valid date.');", true);
            return;

        }

        if (Convert.ToDateTime(txtExtFromDate.Text.Trim()) > Convert.ToDateTime(txtExtToDate.Text.Trim()))
        {
            txtExtFromDate.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('To date can not be less than From date.');", true);
            return;

        }


        string Ext_FromDateTime = txtExtFromDate.Text.Trim() + " " + ddlExtFromHr.SelectedValue.Trim() + ":" + ddlExtFromMin.SelectedValue.Trim();
        string Ext_ToDateTime = txtExtToDate.Text.Trim() + " " + ddlExtToHr.SelectedValue.Trim() + ":" + ddlExtToMin.SelectedValue.Trim();

        try
        {
            Common.Set_Procedures("sp_InsertUpdateCriticalShutdownExtension");
            Common.Set_ParameterLength(6);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", VesselCode),
                new MyParameter("@ExtensionId", ExtensionId),
                new MyParameter("@ShutdownId", ShutdownId),
                new MyParameter("@Ext_ShutDownTotalHrs", txtExtSDHrs.Text.Trim()),
                new MyParameter("@Ext_FromDateTime", Ext_FromDateTime.Trim()),
                new MyParameter("@Ext_ToDateTime", Ext_ToDateTime.Trim())
                );

            DataSet dsUpdate = new DataSet();
            dsUpdate.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsUpdate);

            if (res)
            {
                trExtension.Visible = true;
                BindSDExtensions();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Record saved successfully.');", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Unable to save record.Error :" + ex.Message + Common.getLastError() + "');", true);

        }
    }

    public void BindSDExtensions()
    {
        string SQL = "SELECT SDE.ExtensionId,ShutdownId,Ext_ShutDownTotalHrs,Ext_FromDateTime,Ext_ToDateTime, " +
                     "Approver_Name + '/ ' + REPLACE(CONVERT(VARCHAR(11), SDEA.ApprovedOn, 106),' ','-') AS Approved " +
                     "FROM VSL_CriticalShutdownExtensions SDE " +
                     "LEFT JOIN VSL_CriticalShutdownExtensionApproval SDEA ON SDEA.ExtensionId = SDE.ExtensionId " +
                     "WHERE SDE.VesselCode = '" + VesselCode.Trim() + "' AND SDE.ShutdownId = " + ShutdownId;
        DataTable dtExt = Common.Execute_Procedures_Select_ByQuery(SQL);

        rptShutdownExt.DataSource = dtExt;
        rptShutdownExt.DataBind();
    }
    protected void ddlOccasion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOccasion.SelectedValue.Trim() == "2")
        {
            sp_Defect.Visible = true;
            txtDefectNo.Visible = true;
        }
        else
        {
            sp_Defect.Visible = false;
            txtDefectNo.Visible = false;
            txtDefectNo.Text = "";
        }
    }
    protected void btnClosure_Click(object sender, EventArgs e)
    {
        if (txtSd_MCommencedDate.Text.Trim() == "")
        {
            txtSd_MCommencedDate.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter Commenced date.');", true);
            return;

        }
        DateTime dt1;
        if (!DateTime.TryParse(txtSd_MCommencedDate.Text.Trim(), out dt1))
        {
            txtSd_MCommencedDate.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter valid date.');", true);
            return;

        }
        if (txtSd_CompletedDate.Text.Trim() == "")
        {
            txtSd_CompletedDate.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter completed date.');", true);
            return;

        }
        DateTime dt2;
        if (!DateTime.TryParse(txtSd_CompletedDate.Text.Trim(), out dt2))
        {
            txtSd_CompletedDate.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter valid date.');", true);
            return;

        }

        if (Convert.ToDateTime(txtSd_MCommencedDate.Text.Trim()) > Convert.ToDateTime(txtSd_CompletedDate.Text.Trim()))
        {
            txtSd_MCommencedDate.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('completed date can not be more than Commenced date.');", true);
            return;

        }

        if (txtSd_MaintenanceRemarks.Text.Trim() == "")
        {
            txtSd_MaintenanceRemarks.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter remarks.');", true);
            return;
        }

        string CommencedDateTime = txtSd_MCommencedDate.Text.Trim() + " " + ddlSd_MCommencedHr.SelectedValue.Trim() + ":" + ddlSd_MCommencedMin.SelectedValue.Trim();
        string CompletedDateTime = txtSd_CompletedDate.Text.Trim() + " " + ddlSd_MCompletedHr.SelectedValue.Trim() + ":" + ddlSd_MCompletedMin.SelectedValue.Trim();

        try
        {
            Common.Set_Procedures("sp_CriticalShutdownClosure");
            Common.Set_ParameterLength(5);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", VesselCode),
                new MyParameter("@ShutdownId", ShutdownId),
                new MyParameter("@Ma_CommencedDateTime", CommencedDateTime.Trim()),
                new MyParameter("@Ma_CompletedDateTime", CompletedDateTime.Trim()),
                new MyParameter("@Maintenence_Remarks", txtSd_MaintenanceRemarks.Text.Trim())
                );

            DataSet dsClosure = new DataSet();
            dsClosure.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsClosure);

            if (res)
            {
                btnClosure.Visible = false;
                btnAddExtension.Visible = false;
                btnPrint.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Record saved successfully.');", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Unable to save record.Error :" + ex.Message + Common.getLastError() + "');", true);

        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (txtSd_ApproverRemarks.Text.Trim() == "")
        {
            txtSd_ApproverRemarks.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter remarks.');", true);
            return;

        }
        if (txtSd_ApproverName.Text.Trim() == "")
        {
            txtSd_ApproverName.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter name.');", true);
            return;

        }
        if (txtSd_ApproverPosition.Text.Trim() == "")
        {
            txtSd_ApproverPosition.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter position.');", true);
            return;

        }

        try
        {
            Common.Set_Procedures("sp_ApproveCriticalShutdown");
            Common.Set_ParameterLength(6);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", VesselCode),
                new MyParameter("@ShutdownApproveId", 0),
                new MyParameter("@ShutdownId", ShutdownId),
                new MyParameter("@Approver_Name", txtSd_ApproverName.Text.Trim()),
                new MyParameter("@Approver_Position", txtSd_ApproverPosition.Text.Trim()),
                new MyParameter("@Approver_Remarks", txtSd_ApproverRemarks.Text.Trim())
                );

            DataSet dsApprove = new DataSet();
            dsApprove.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsApprove);

            if (res)
            {
                btnApprove.Visible = false;
                lbl_FReqApproved.Text = DateTime.Today.Date.ToString("dd-MMM-yyyy");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Request approved successfully.');", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Unable to approve request.Error :" + ex.Message + Common.getLastError() + "');", true);

        }

    }

    protected void btnAddExtension_Click(object sender, EventArgs e)
    {
        dvExtensions.Visible = true;
    }
    protected void btnCloseExtension_Click(object sender, EventArgs e)
    {
        dvExtensions.Visible = false;

        txtExtSDHrs.Text = "";
        txtExtFromDate.Text = "";
        txtExtToDate.Text = "";
        ddlExtFromHr.SelectedIndex = 0;
        ddlExtFromMin.SelectedIndex = 0;
        ddlExtToHr.SelectedIndex = 0;
        ddlExtToMin.SelectedIndex = 0;
    }

    //protected void btn_ApproveExtensions_Click(object sender, EventArgs e)
    //{
    //    if (txt_ApproversRemarks.Text.Trim() == "")
    //    {
    //        txt_ApproversRemarks.Focus();
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter remarks.');", true);
    //        return;

    //    }
    //    if (txt_ApproverName.Text.Trim() == "")
    //    {
    //        txt_ApproverName.Focus();
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter name.');", true);
    //        return;

    //    }
    //    if (txt_ApproverPosition.Text.Trim() == "")
    //    {
    //        txt_ApproverPosition.Focus();
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter position.');", true);
    //        return;

    //    }

    //    try
    //    {
    //        Common.Set_Procedures("sp_ApproveCriticalShutdownExtensions");
    //        Common.Set_ParameterLength(6);
    //        Common.Set_Parameters(
    //            new MyParameter("@VesselCode", VesselCode),
    //            new MyParameter("@ExtensionApproveId", 0),
    //            new MyParameter("@ExtensionId", ExtensionId),
    //            new MyParameter("@Approver_Name", txt_ApproverName.Text.Trim()),
    //            new MyParameter("@Approver_Position", txt_ApproverPosition.Text.Trim()),
    //            new MyParameter("@Approver_Remarks", txt_ApproversRemarks.Text.Trim())
    //            );

    //        DataSet dsApprove = new DataSet();
    //        dsApprove.Clear();
    //        Boolean res;
    //        res = Common.Execute_Procedures_IUD(dsApprove);

    //        if (res)
    //        {
    //            btn_ApproveExtensions.Visible = false;
    //            ExtensionId = 0;
    //            BindSDExtensions();
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Request approved successfully.');", true);
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Unable to approve request.Error :" + ex.Message + Common.getLastError() + "');", true);

    //    }

    //}
    //protected void btnCloseApproveExtension_Click(object sender, EventArgs e)
    //{
    //    dvApprovals.Visible = false;
    //    ExtensionId = 0;

    //    txt_ApproversRemarks.Text = "";
    //    txt_ApproverName.Text = "";
    //    txt_ApproverPosition.Text = "";
        
    //}
    //protected void imgbtn_Approve_Click(object sender, ImageClickEventArgs e)
    //{
    //    ExtensionId = Common.CastAsInt32(((ImageButton)sender).CommandArgument.ToString());

    //    if (ExtensionId != 0)
    //    {
    //        dvApprovals.Visible = true;
    //        btn_ApproveExtensions.Visible = true;
    //    }
    //}
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "print", "printwindow('" + VesselCode + "','" + ShutdownId.ToString() + "');", true);
    }
    protected void txtSd_PlannedFromDate_TextChanged(object sender, EventArgs e)
    {
        DateTime dt;
        int i;
        if ((txtSd_PlannedFromDate.Text.Trim() != "") && (DateTime.TryParse(txtSd_PlannedFromDate.Text.Trim(), out dt)) && (txtSd_PlannedSDHrs.Text.Trim() != "") && (int.TryParse(txtSd_PlannedSDHrs.Text.Trim(), out i)))
        {
            string strFromDt = txtSd_PlannedFromDate.Text.Trim() + " " + ddlSd_PlannedFromHr.SelectedValue + " : " + ddlSd_PlannedFromMin.SelectedValue + " : 00";
            DateTime dtPlannedFrom = Convert.ToDateTime(strFromDt);
            DateTime dtPlannedTo = dtPlannedFrom.AddHours(Convert.ToDouble(txtSd_PlannedSDHrs.Text.Trim()));
            txtSd_PlannedToDate.Text = dtPlannedTo.ToString("dd-MMM-yyyy");

            ddlSd_PlannedToHr.SelectedValue = dtPlannedTo.Hour.ToString().PadLeft(2, '0');
            ddlSd_PlannedToMin.SelectedValue = dtPlannedTo.Minute.ToString().PadLeft(2, '0');
        }
        else
        {
            txtSd_PlannedToDate.Text = "";
        }

    }
    protected void txtExtFromDate_TextChanged(object sender, EventArgs e)
    {
        DateTime dt;
        int i;
        if ((txtExtFromDate.Text.Trim() != "") && (DateTime.TryParse(txtExtFromDate.Text.Trim(), out dt)) && (txtExtSDHrs.Text.Trim() != "") && (int.TryParse(txtExtSDHrs.Text.Trim(), out i)))
        {
            string strFromDt = txtExtFromDate.Text.Trim() + " " + ddlExtFromHr.SelectedValue + " : " + ddlExtFromMin.SelectedValue + " : 00";
            DateTime dtPlannedFrom = Convert.ToDateTime(strFromDt);
            DateTime dtPlannedTo = dtPlannedFrom.AddHours(Convert.ToDouble(txtExtSDHrs.Text.Trim()));

            txtExtToDate.Text = dtPlannedTo.ToString("dd-MMM-yyyy");

            ddlExtToHr.SelectedValue = dtPlannedTo.Hour.ToString().PadLeft(2, '0');
            ddlExtToMin.SelectedValue = dtPlannedTo.Minute.ToString().PadLeft(2, '0');
        }
        else
        {
            txtExtToDate.Text = "";
        }

    }

    protected void btnSaveAttachment_OnClick(object sender, EventArgs e)
    {
        if (txtDescription.Text.Trim() == "")
        {
            txtDescription.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter txtDescription.');", true);
            return;

        }
        if(!flpAttachment.HasFile)
        {
            flpAttachment.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please select a file.');", true);
            return;
        }



        try
        {
            string sAttachmentName = "";
            sAttachmentName = flpAttachment.FileName;



            Common.Set_Procedures("sp_IO_VSL_CriticalEquipShutdownRequestAttachments");
            Common.Set_ParameterLength(7);
            Common.Set_Parameters(
                new MyParameter("@AttachmentID", 0),
                new MyParameter("@VesselCode", VesselCode),
                new MyParameter("@ShutdownId", ShutdownId),
                new MyParameter("@Description", txtDescription.Text.Trim()),
                new MyParameter("@AttachmentName", sAttachmentName),
                new MyParameter("@Attachment", flpAttachment.FileBytes ),
                new MyParameter("@Status", "A")
                );

            DataSet dsApprove = new DataSet();
            dsApprove.Clear();
            Boolean res=false;
            res = Common.Execute_Procedures_IUD(dsApprove);

            if (res)
            {
                //btn_ApproveExtensions.Visible = false;
                ExtensionId = 0;
                BindSDExtensions();
                ShowAttachments();
                txtDescription.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Data saved successfully.');", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Unable to process request.Error :" + ex.Message + Common.getLastError() + "');", true);

        }
    }
    public void ShowAttachments()
    {
        string strReq = " select * from VSL_CriticalEquipShutdownRequestAttachments where VesselCode='" + VesselCode + "' and ShutdownId="+ShutdownId+" and  Status='A' order by AttachmentID desc ";
        DataTable dtReq = Common.Execute_Procedures_Select_ByQuery(strReq);

        rptAttachment.DataSource = dtReq;
        rptAttachment.DataBind();

    }
    protected void lnkDownloadAttachment_OnClick(object sender, EventArgs e)
    {
        LinkButton lnkBtn = (LinkButton)sender;
        HiddenField hfAttachmentID=(HiddenField)lnkBtn .Parent.FindControl("hfAttachmentID");

        string strReq = " select AttachmentName,Attachment from VSL_CriticalEquipShutdownRequestAttachments where VesselCode='" + VesselCode + "' and  AttachmentID=" + hfAttachmentID.Value + "  ";
        DataTable dtReq = Common.Execute_Procedures_Select_ByQuery(strReq);

        string sAttachmentName = dtReq.Rows[0][0].ToString();
        string FileType = sAttachmentName.Substring(sAttachmentName.LastIndexOf(".")+1);
        byte[] File = (byte[])dtReq.Rows[0][1];


        //Response.Clear();
        //Response.ContentType = "application/" + FileType ;
        //Response.AddHeader("Content-Type", "application/" + FileType);
        //Response.AppendHeader("Content-Disposition", "inline;filename=" + sAttachmentName);
        //Response.BinaryWrite(File);


        Response.AddHeader("Content-type", FileType);
        Response.AddHeader("Content-Disposition", "attachment; filename=" + sAttachmentName);
        Response.BinaryWrite(File);
        Response.Flush();
        Response.End();


        //Response.Buffer = true;
        //Response.Charset = "";
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //Response.ContentType = FileType;
        //Response.AddHeader("content-disposition", "attachment;filename=" + sAttachmentName);
        //Response.BinaryWrite(File);
        //Response.Flush();
        //Response.End();


        //Response.Clear();
        //Response.ClearHeaders();
        //Response.Buffer = true;
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //Response.ContentType = "application/" + FileType;
        //Response.AppendHeader("Content-Disposition", "attachment; filename=" + sAttachmentName);
        //Response.AppendHeader("Content-Length", File.Length.ToString());
        //this.Context.ApplicationInstance.CompleteRequest();

    }
    protected void btnDeleteAttachment_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField hfAttachmentID = (HiddenField)btn.Parent.FindControl("hfAttachmentID");
        string strReq = " update VSL_CriticalEquipShutdownRequestAttachments set Status='D' where VesselCode='" + VesselCode + "' and AttachmentID=" + hfAttachmentID.Value;
        DataTable dtReq = Common.Execute_Procedures_Select_ByQuery(strReq);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Data deleted successfully.');", true);
        ShowAttachments();
    }
     protected void btnExport_OnClick(object sender, EventArgs e)
    {
        if (ShutdownId <= 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Please save the request before export.');", true);
            return;
        }
        if (rptAttachment.Items.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Please attach risk assessment.');", true);
            return;
        }
        try
        {
            Common.Set_Procedures("[DBO].[sp_Insert_Communication_Export]");
            Common.Set_ParameterLength(5);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", VesselCode),
                new MyParameter("@RecordType", "CRITICALEQ-SHUTDOWN-REQ"),
                new MyParameter("@RecordId", ShutdownId),
                new MyParameter("@RecordNo", lblSd_CompCode.Text + " - " + txtReqdt.Text),
                new MyParameter("@CreatedBy", Session["UserName"].ToString())
            );

            Common.Execute_Procedures_Select_ByQuery("UPDATE [DBO].vw_VSL_CriticalEquipShutdownRequest SET IssueDate=GETDATE() WHERE VESSELCODE ='" + VesselCode + "' AND ShutdownId =" + ShutdownId);
            EnableDisable();

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
}