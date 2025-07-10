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
using System.Data.SqlClient;
using Ionic.Zip;
using System.Net;
using System.Net.Mail;
using System.Text;

public partial class Office_CriticalEqpShutdownReq : System.Web.UI.Page
{
    public static string _ServerName = ConfigurationManager.AppSettings["SMTPServerName"];
    public static int _Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
    public static MailAddress _FromAdd = new MailAddress(ConfigurationManager.AppSettings["FromAddress"]);
    public static string _UserName = ConfigurationManager.AppSettings["SMTPUserName"];
    public static string _Password = ConfigurationManager.AppSettings["SMTPUserPwd"];
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
                ShutdownId = Common.CastAsInt32(Request.QueryString["SD"].ToString());
                VesselCode = Request.QueryString["VC"].ToString();
                lblSd_VesselCode.Text = VesselCode;
                BindTime();
                GetComponent();
                ShowShutdownDetails();
                BindSDExtensions();
                EnableDisable();
                BindAttachments();
                
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

                if(dtReq.Rows[0]["Occasion"].ToString()=="1")
                    lblOccassion.Text="Routine";

                if (dtReq.Rows[0]["Occasion"].ToString() == "2")
                    lblOccassion.Text="Break Down";

                txtDefectNo.Text = dtReq.Rows[0]["DefectNo"].ToString();
                lblReqdt.Text = dtReq.Rows[0]["RequestDate"].ToString();
                lblMasterName.Text = dtReq.Rows[0]["MasterCEName"].ToString();
                lblJobDefect.Text = dtReq.Rows[0]["Job_Defect"].ToString();
                lblSd_PlannedSDHrs.Text = dtReq.Rows[0]["Pl_ShutDownTotalHrs"].ToString();


                string[] FromDate = dtReq.Rows[0]["Pl_FromDateTime"].ToString().Split(' ');
                string[] FromTime = FromDate.GetValue(1).ToString().Split(':');

                DateTime d1 = Convert.ToDateTime(dtReq.Rows[0]["Pl_FromDateTime"]);

                string[] ToDate = dtReq.Rows[0]["Pl_ToDateTime"].ToString().Split(' ');
                string[] ToTime = ToDate.GetValue(1).ToString().Split(':');

                DateTime d2 = Convert.ToDateTime(dtReq.Rows[0]["Pl_ToDateTime"]);


                lblSd_PlannedFromDate.Text = d1.ToString("dd-MMM-yyyy");
                lblSd_PlannedFromTime.Text = d1.Hour.ToString().PadLeft(2, '0') + ":" + d1.Minute.ToString().PadLeft(2, '0');

               
                lblSd_PlannedToDate.Text = d2.ToString("dd-MMM-yyyy");
                lblSd_PlannedToTime.Text = d2.Hour.ToString().PadLeft(2, '0') + ":" + d2.Minute.ToString().PadLeft(2, '0'); 

                ddlOccasion_SelectedIndexChanged(new object(), new EventArgs());

                if (dtReq.Rows[0]["Closed"].ToString() == "True")
                {
                    //string[] M_FromDate = dtReq.Rows[0]["Ma_CommencedDateTime"].ToString().Split(' ');
                    //string[] M_ToDate = dtReq.Rows[0]["Ma_CompletedDateTime"].ToString().Split(' ');
                 
                    DateTime D1 = Convert.ToDateTime(dtReq.Rows[0]["Ma_CommencedDateTime"]);
                    txtSd_MCommencedDate.Text = D1.ToString("dd-MMM-yyyy");
                    txtSd_MCommencedTime.Text = D1.Hour.ToString().PadLeft(2,'0') + ":" + D1.Minute.ToString().PadLeft(2,'0');


                    DateTime D2 = Convert.ToDateTime(dtReq.Rows[0]["Ma_CompletedDateTime"]);
                    txtSd_CompletedDate.Text = D2.ToString("dd-MMM-yyyy");
                    txtSd_CompletedTime.Text = D2.Hour.ToString().PadLeft(2,'0') + ":" + D2.Minute.ToString().PadLeft(2,'0');

                    txtSd_MaintenanceRemarks.Text = dtReq.Rows[0]["Maintenence_Remarks"].ToString();

                }

                //if (dtReq.Rows[0]["Approved"].ToString() != "")
                //{
                string SQL = "SELECT Approver_Name,Approver_Position,Approver_Remarks,ApprovedOn FROM VSL_CriticalShutdownApproval WHERE VesselCode = '" + VesselCode.Trim() + "' AND ShutdownId =" + ShutdownId;
                DataTable dtApprovedetails = Common.Execute_Procedures_Select_ByQuery(SQL);

                if (dtApprovedetails.Rows.Count > 0)
                {
                    txtSd_ApproverName.Text = dtApprovedetails.Rows[0]["Approver_Name"].ToString();
                    //txtSd_ApproverPosition.Text = dtApprovedetails.Rows[0]["Approver_Position"].ToString();
                    txtSd_ApproverRemarks.Text = dtApprovedetails.Rows[0]["Approver_Remarks"].ToString();
                    lbl_FReqApproved.Text = Common.ToDateString(dtApprovedetails.Rows[0]["ApprovedOn"]);
                    if (lbl_FReqApproved.Text == "")
                        ddlFollowup.SelectedValue = "1";


                    btnApprove.Visible = (lbl_FReqApproved.Text.Trim() == "");
                    trOfficeApproval.Visible = true;
                }

            }

        }
        

    }
    public void EnableDisable()
    {
        if (ShutdownId != 0)
        {
            //btnSave.Visible = false;
            //btnSaveExtension.Visible = true;

            string sql = "SELECT ISNULL(Closed, 0) AS Closed FROM VSL_CriticalEquipShutdownRequest WHERE VesselCode = '" + VesselCode.Trim() + "' AND ShutdownId = " + ShutdownId;
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
            ISClosed = Convert.ToBoolean(dt.Rows[0][0].ToString());
            if (ISClosed)
            {
                trExtension.Visible = true;
                trOfficeApproval.Visible = true;
                //trClosure.Visible = true;

                btnApprove.Visible = false;
                btnClosure.Visible = false;

                return;

            }
            if (Session["UserType"].ToString() == "S")
            {
                string SQL = "SELECT * FROM VSL_CriticalShutdownApproval WHERE ShutdownId =" + ShutdownId;
                DataTable dtApproved = Common.Execute_Procedures_Select_ByQuery(SQL);

                if (dtApproved.Rows.Count > 0)
                {
                    //trClosure.Visible = true;
                }
                else
                {
                    //trClosure.Visible = false;
                }

                trExtension.Visible = true;
                
            }
            else
            {
                trExtension.Visible = true;
                trOfficeApproval.Visible = true;
                //trClosure.Visible = false;

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
            btnSaveExtension.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "saf", "alert('Invalid component code.'); window.close();", true);
        }

    }
    public void BindTime()
    {
    //    for (int i = 0; i < 24; i++)
    //    {
    //        if (i < 10)
    //        {
    //            ddlSd_PlannedFromHr.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
    //            ddlSd_PlannedToHr.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));

    //            ddlExtFromHr.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
    //            ddlExtToHr.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));

    //            ddlSd_MCommencedHr.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
    //            ddlSd_MCompletedHr.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
    //        }
    //        else
    //        {
    //            ddlSd_PlannedFromHr.Items.Add(new ListItem(i.ToString(), i.ToString()));
    //            ddlSd_PlannedToHr.Items.Add(new ListItem(i.ToString(), i.ToString()));

    //            ddlExtFromHr.Items.Add(new ListItem(i.ToString(), i.ToString()));
    //            ddlExtToHr.Items.Add(new ListItem(i.ToString(), i.ToString()));

    //            ddlSd_MCommencedHr.Items.Add(new ListItem(i.ToString(), i.ToString()));
    //            ddlSd_MCompletedHr.Items.Add(new ListItem(i.ToString(), i.ToString()));
    //        }
    //    }

    //    for (int i = 0; i < 60; i++)
    //    {
    //        if (i < 10)
    //        {
    //            ddlSd_PlannedFromMin.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
    //            ddlSd_PlannedToMin.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));

    //            ddlExtFromMin.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
    //            ddlExtToMin.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));

    //            ddlSd_MCommencedMin.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
    //            ddlSd_MCompletedMin.Items.Add(new ListItem("0" + i.ToString(), "0" + i.ToString()));
    //        }
    //        else
    //        {
    //            ddlSd_PlannedFromMin.Items.Add(new ListItem(i.ToString(), i.ToString()));
    //            ddlSd_PlannedToMin.Items.Add(new ListItem(i.ToString(), i.ToString()));

    //            ddlExtFromMin.Items.Add(new ListItem(i.ToString(), i.ToString()));
    //            ddlExtToMin.Items.Add(new ListItem(i.ToString(), i.ToString()));

    //            ddlSd_MCommencedMin.Items.Add(new ListItem(i.ToString(), i.ToString()));
    //            ddlSd_MCompletedMin.Items.Add(new ListItem(i.ToString(), i.ToString()));

    //        }
    //    }
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
        if (lblOccassion.Text=="Break Down")
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
        //if (txtSd_MCommencedDate.Text.Trim() == "")
        //{
        //    txtSd_MCommencedDate.Focus();
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter Commenced date.');", true);
        //    return;

        //}
        //DateTime dt1;
        //if (!DateTime.TryParse(txtSd_MCommencedDate.Text.Trim(), out dt1))
        //{
        //    txtSd_MCommencedDate.Focus();
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter valid date.');", true);
        //    return;

        //}
        //if (txtSd_CompletedDate.Text.Trim() == "")
        //{
        //    txtSd_CompletedDate.Focus();
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter completed date.');", true);
        //    return;

        //}
        //DateTime dt2;
        //if (!DateTime.TryParse(txtSd_CompletedDate.Text.Trim(), out dt2))
        //{
        //    txtSd_CompletedDate.Focus();
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter valid date.');", true);
        //    return;

        //}

        //if (Convert.ToDateTime(txtSd_MCommencedDate.Text.Trim()) > Convert.ToDateTime(txtSd_CompletedDate.Text.Trim()))
        //{
        //    txtSd_MCommencedDate.Focus();
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('completed date can not be more than Commenced date.');", true);
        //    return;

        //}

        //if (txtSd_MaintenanceRemarks.Text.Trim() == "")
        //{
        //    txtSd_MaintenanceRemarks.Focus();
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter remarks.');", true);
        //    return;
        //}

        ////----------------------------------------------------------------------------------------
        ////string CommencedDateTime = txtSd_MCommencedDate.Text.Trim() + " " + ddlSd_MCommencedHr.SelectedValue.Trim() + ":" + ddlSd_MCommencedMin.SelectedValue.Trim();
        ////string CompletedDateTime = txtSd_CompletedDate.Text.Trim() + " " + ddlSd_MCompletedHr.SelectedValue.Trim() + ":" + ddlSd_MCompletedMin.SelectedValue.Trim();

        //string CommencedDateTime = txtSd_MCommencedDate.Text.Trim() + " " + txtSd_MCommencedTime.Text;
        //string CompletedDateTime = txtSd_CompletedDate.Text.Trim() + " " + txtSd_CompletedTime.Text;

        //try
        //{
        //    Common.Set_Procedures("sp_CriticalShutdownClosure");
        //    Common.Set_ParameterLength(5);
        //    Common.Set_Parameters(
        //        new MyParameter("@VesselCode", VesselCode),
        //        new MyParameter("@ShutdownId", ShutdownId),
        //        new MyParameter("@Ma_CommencedDateTime", CommencedDateTime.Trim()),
        //        new MyParameter("@Ma_CompletedDateTime", CompletedDateTime.Trim()),
        //        new MyParameter("@Maintenence_Remarks", txtSd_MaintenanceRemarks.Text.Trim())
        //        );

        //    DataSet dsClosure = new DataSet();
        //    dsClosure.Clear();
        //    Boolean res;
        //    res = Common.Execute_Procedures_IUD(dsClosure);

        //    if (res)
        //    {
        //        btnClosure.Visible = false;
        //        btnPrint.Visible = true;
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Record saved successfully.');", true);
        //    }

        //}
        //catch (Exception ex)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Unable to save record.Error :" + ex.Message + Common.getLastError() + "');", true);

        //}
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        if (ddlFollowup.SelectedIndex == 0)
        {
            ddlFollowup.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please select followup.');", true);
            return;
        }
        if (txtSd_ApproverRemarks.Text.Trim() == "")
        {
            txtSd_ApproverRemarks.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter remarks.');", true);
            return;

        }
        //if (txtSd_ApproverName.Text.Trim() == "")
        //{
        //    txtSd_ApproverName.Focus();
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter name.');", true);
        //    return;

        //}
        //if (txtSd_ApproverPosition.Text.Trim() == "")
        //{
        //    txtSd_ApproverPosition.Focus();
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter position.');", true);
        //    return;

        //}

        try
        {
            Common.Set_Procedures("sp_ApproveCriticalShutdown");
            Common.Set_ParameterLength(7);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", VesselCode),
                new MyParameter("@ShutdownApproveId", 0),
                new MyParameter("@ShutdownId", ShutdownId),
                new MyParameter("@Approver_Name", Session["FullName"].ToString()),
                new MyParameter("@Approver_Position", ""),
                new MyParameter("@Approver_Remarks", txtSd_ApproverRemarks.Text.Trim()),
                new MyParameter("@FollowUp", Common.CastAsInt32(ddlFollowup.SelectedValue)));

            DataSet dsApprove = new DataSet();
            dsApprove.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsApprove);

            if (res)
            {
                btnApprove.Visible = false;

                txtSd_ApproverName.Text = Session["FullName"].ToString();
                //lbl_FReqApproved.Text = DateTime.Today.Date.ToString("dd-MMM-yyyy");
                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Request approved successfully.');", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Unable to approve request.Error :" + ex.Message + Common.getLastError() + "');", true);

        }

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

    protected void btn_ApproveExtensions_Click(object sender, EventArgs e)
    {
        if (txt_ApproversRemarks.Text.Trim() == "")
        {
            txt_ApproversRemarks.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter remarks.');", true);
            return;

        }
        if (txt_ApproverName.Text.Trim() == "")
        {
            txt_ApproverName.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter name.');", true);
            return;

        }
        if (txt_ApproverPosition.Text.Trim() == "")
        {
            txt_ApproverPosition.Focus();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", "alert('Please enter position.');", true);
            return;

        }

        try
        {
            Common.Set_Procedures("sp_ApproveCriticalShutdownExtensions");
            Common.Set_ParameterLength(6);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", VesselCode),
                new MyParameter("@ExtensionApproveId", 0),
                new MyParameter("@ExtensionId", ExtensionId),
                new MyParameter("@Approver_Name", txt_ApproverName.Text.Trim()),
                new MyParameter("@Approver_Position", txt_ApproverPosition.Text.Trim()),
                new MyParameter("@Approver_Remarks", txt_ApproversRemarks.Text.Trim())
                );

            DataSet dsApprove = new DataSet();
            dsApprove.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(dsApprove);

            if (res)
            {
                btn_ApproveExtensions.Visible = false;
                ExtensionId = 0;
                BindSDExtensions();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Request approved successfully.');", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Unable to approve request.Error :" + ex.Message + Common.getLastError() + "');", true);

        }

    }
    protected void btnCloseApproveExtension_Click(object sender, EventArgs e)
    {
        dvApprovals.Visible = false;
        ExtensionId = 0;

        txt_ApproversRemarks.Text = "";
        txt_ApproverName.Text = "";
        txt_ApproverPosition.Text = "";
        
    }
    protected void imgbtn_Approve_Click(object sender, ImageClickEventArgs e)
    {
        ExtensionId = Common.CastAsInt32(((ImageButton)sender).CommandArgument.ToString());

        if (ExtensionId != 0)
        {
            dvApprovals.Visible = true;
            btn_ApproveExtensions.Visible = true;
        }
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "print", "printwindow('" + VesselCode + "','" + ShutdownId.ToString() + "');", true);
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


    public void BindAttachments()
    {
        string SQL = " select * from VSL_CriticalEquipShutdownRequestAttachments where VesselCode='"+VesselCode+"' and ShutdownId="+ShutdownId+" and Status='A'  ";
        DataTable dtExt = Common.Execute_Procedures_Select_ByQuery(SQL);

        rptAttachments.DataSource = dtExt;
        rptAttachments.DataBind();
    }

    protected void lnkDownloadAttachment_OnClick(object sender, EventArgs e)
    {
        LinkButton lnkBtn = (LinkButton)sender;
        HiddenField hfAttachmentID = (HiddenField)lnkBtn.Parent.FindControl("hfAttachmentID");

        string strReq = " select AttachmentName,Attachment from VSL_CriticalEquipShutdownRequestAttachments where VesselCode='" + VesselCode + "' and AttachmentID=" + hfAttachmentID.Value + "  ";
        DataTable dtReq = Common.Execute_Procedures_Select_ByQuery(strReq);

        string sAttachmentName = dtReq.Rows[0][0].ToString();
        string FileType = sAttachmentName.Substring(sAttachmentName.LastIndexOf(".") + 1);
        byte[] File = (byte[])dtReq.Rows[0][1];

        Response.AddHeader("Content-type", FileType);
        Response.AddHeader("Content-Disposition", "attachment; filename=" + sAttachmentName);
        Response.BinaryWrite(File);
        Response.Flush();
        Response.End();
    }

    public void btnExportToShip_OnClick(object sender, EventArgs e)
    {
        ClearTempFiles();
        string SaveTargetDir = Server.MapPath("~/Modules/PMS/TEMP/");

        ///  ------------------- CHECKS 3.  GET DATA TO BE EXPORTED FROM DATABASE

        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("select * from DBO.VSL_CriticalShutdownApproval WHERE VESSELCODE='" + VesselCode + "' AND ShutdownId =" + ShutdownId);
        DataSet dsExport = new DataSet();
        dt1.TableName = "VSL_CriticalShutdownApproval";
        dsExport.Tables.Add(dt1.Copy());
        
        ///  ------------------- CHECKS 4.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport == null)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }
        ///  ------------------- CHECKS 5.  IF SQL ERROR NO TABLE RETURNED 
        if (dsExport.Tables.Count <= 0)
        {
            lblMsg.Text = "Database not exists OR some critical error.";
            return;
        }

        ///  ------------------- CHECKS 7.  IF TABLES MATCHING BUT THERE IS NOTHING TO EXPORT
        bool DataExists = false;
        foreach (DataTable dt in dsExport.Tables)
        {
            if (dt.Rows.Count > 0)
            {
                DataExists = true;
                break;
            }
        }
        if (!DataExists)
        {
            lblMsg.Text = "No data available to send update.";
            return;
        }
        //===================================================================================
        //---------------- CHECKS DONE NOW GOING TO CREATE FILE 

        string SchemaFile = SaveTargetDir + "CRITEQSHUTDOWNREQ_O_Schema.xml";
        string DataFile = SaveTargetDir + "CRITEQSHUTDOWNREQ_O_Data.xml";

        try
        {
            string ZipData = SaveTargetDir + "CRIT_EQ_SH_REQ_O_" + VesselCode + "_" + ShutdownId + "_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".zip";
            dsExport.WriteXmlSchema(SchemaFile);
            dsExport.WriteXml(DataFile);
            bool FileDone = false;
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(SchemaFile);
                zip.AddFile(DataFile);
                zip.Save(ZipData);
                FileDone = true;
            }
            //--------------------------------
            if (FileDone)
            {
                string VesselEmail = "";
                DataTable dtVess = Common.Execute_Procedures_Select_ByQuery(" select Email from dbo.Vessel where vesselcode='"+VesselCode+"'");                
                if(dtVess.Rows.Count>0)
                {
                    VesselEmail = dtVess.Rows[0][0].ToString();
                }

                if (SendPacketMail(ZipData, VesselEmail) == "SENT")
                {
                    lblMsg.Text = "Mail sent successfully.";
                }
            }
            else
            {
                lblMsg.Text = "Unable to create Zip File.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = "Unable to export data. " + ex.Message;
        }
    }
    protected void ClearTempFiles()
    {
        string[] files = Directory.GetFiles(Server.MapPath("~/Modules/PMS/TEMP"));
        foreach (string fl in files)
            try { File.Delete(fl); }
            catch { }

        string[] folders = Directory.GetDirectories(Server.MapPath("~/Modules/PMS/TEMP"));
        foreach (string folder in folders)
            try { Directory.Delete(folder, true); }
            catch { }

    }
    public static string SendPacketMail(string Attachment, string VesselEmail)
    {
        try
        {


            MailMessage objMessage = new MailMessage();
            SmtpClient objSmtpClient = new SmtpClient();
            objSmtpClient.Host = _ServerName;
            objSmtpClient.Port = _Port;
            objSmtpClient.EnableSsl = true;
            objSmtpClient.UseDefaultCredentials = false;
            objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                 | SecurityProtocolType.Tls11
                                 | SecurityProtocolType.Tls12;
            objSmtpClient.Credentials = new NetworkCredential(_UserName, _Password);
            MailAddress objfromAddress = new MailAddress("emanager@energiossolutions.com");
            StringBuilder msgFormat = new StringBuilder();
            SetMails(objSmtpClient, objMessage, "emanager@energiossolutions.com");
            try
            {
                objMessage.To.Add(VesselEmail);
                objMessage.CC.Add("emanager@energiossolutions.com");
                objMessage.Body = "Dear captain, <br/>Please import the attached packet.<br/><br/>";

                objMessage.Subject = "PMS - Critical Equipment Shutdown Request Appoval";
                objMessage.IsBodyHtml = true;

                using (System.IO.MemoryStream ms = new System.IO.MemoryStream(System.IO.File.ReadAllBytes(Attachment)))
                {
                    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(ms, System.IO.Path.GetFileName(Attachment), System.Net.Mime.MediaTypeNames.Application.Zip);
                    objMessage.Attachments.Add(attach);

                    //Attachment attachFile = new Attachment(Attachment);
                    //objMessage.Attachments.Add(attachFile);

                    objMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                    objSmtpClient.Send(objMessage);
                }
                return "SENT";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }
    public static void SetMails(SmtpClient _SmtpClient, MailMessage _MailMessage, string _ReplyMailAddress)
    {
        string _ServerName = "smtp.office365.com";
        int _Port = 587;
        MailAddress _FromAdd = new MailAddress("emanager@energiossolutions.com");
        string _UserName = "emanager@energiossolutions.com";
        string _Password = "Huz41628";

        _SmtpClient.Host = _ServerName;
        _SmtpClient.Port = _Port;
        _SmtpClient.Credentials = new NetworkCredential(_UserName, _Password);
        _SmtpClient.EnableSsl = true;
        _MailMessage.From = _FromAdd;

        if (_ReplyMailAddress.Trim() == "emanager@energiossolutions.com")
        {
            _ReplyMailAddress = _FromAdd.Address;
        }
       // _ReplyMailAddress = _ReplyMailAddress.Replace("@energiosmaritime.com", "@energiossolutions.com");
        _MailMessage.ReplyTo = new MailAddress(_ReplyMailAddress);
    }
}