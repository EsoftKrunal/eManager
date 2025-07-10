using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Invoice_FindInvoice : System.Web.UI.Page
{
    public bool CancelAllowed
    {
        get { return Convert.ToBoolean(ViewState["CancelAllowed"]); }
        set { ViewState["CancelAllowed"] = value; }
    }
    public int UserId
    {
        get { return Convert.ToInt32(ViewState["UserId"]); }
        set { ViewState["UserId"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        if (!Page.IsPostBack)
        {
            UserId = Common.CastAsInt32(Session["loginid"]);
            if (UserId == 1)
            {
                CancelAllowed = true;
            }
            else
            {
                DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT CANCEL FROM POS_Invoice_mgmt with(nolock) where USERID=" + UserId);
                if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString() == "True")
                    CancelAllowed = true;
            }
            bindOwnerddl();
            bindVesselNameddl();
           // bindUser();
            bindVessel();
            bindCurrencyddl();
        }
    }
    protected void btnDownloadFile_Click(object sender, EventArgs e)
    {
        int InvId = Common.CastAsInt32(hfInvId.Value);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT AttachmentName,Attachment FROM POS_Invoice WHERE InvoiceId=" + InvId.ToString());
        if (dt.Rows.Count > 0)
        {
            string FileName = dt.Rows[0]["AttachmentName"].ToString();
            if (FileName.Trim() != "")
            {
                byte[] buff = (byte[])dt.Rows[0]["Attachment"];
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                Response.BinaryWrite(buff);
                Response.Flush();
                Response.End();
            }
        }
    }
    //protected void bindUser()
    //{
    //    string SQL = "select (FirstName + ' ' + LastName ) AS UserName, LoginId  from dbo.usermaster where loginid in (select userid from pos_invoice_mgmt where Entry=1) AND statusId='A' Order By UserName";
    //    DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(SQL);
    //    ddlUser_E.DataValueField = "LoginId";
    //    ddlUser_E.DataTextField = "UserName";
    //    ddlUser_E.DataSource = dt1;
    //    ddlUser_E.DataBind();
    //    ddlUser_E.Items.Insert(0, new ListItem("All", "0"));

    //    SQL = "select (FirstName + ' ' + LastName ) AS UserName, LoginId  from dbo.usermaster where loginid in (select userid from pos_invoice_mgmt where Approval=1) AND statusId='A' Order By UserName";
    //    dt1 = Common.Execute_Procedures_Select_ByQuery(SQL);
    //    ddlUser_A.DataValueField = "LoginId";
    //    ddlUser_A.DataTextField = "UserName";
    //    ddlUser_A.DataSource = dt1;
    //    ddlUser_A.DataBind();
    //    ddlUser_A.Items.Insert(0, new ListItem("All", "0"));

    //    SQL = "select (FirstName + ' ' + LastName ) AS UserName, LoginId  from dbo.usermaster where loginid in (select userid from pos_invoice_mgmt where Verification=1) AND statusId='A' Order By UserName";
    //    dt1 = Common.Execute_Procedures_Select_ByQuery(SQL);
    //    ddlUser_V.DataValueField = "LoginId";
    //    ddlUser_V.DataTextField = "UserName";
    //    ddlUser_V.DataSource = dt1;
    //    ddlUser_V.DataBind();
    //    ddlUser_V.Items.Insert(0, new ListItem("All", "0"));

    //    SQL = "select (FirstName + ' ' + LastName ) AS UserName, LoginId  from dbo.usermaster where loginid in (select userid from pos_invoice_mgmt where Payment=1) AND statusId='A' Order By UserName";
    //    dt1 = Common.Execute_Procedures_Select_ByQuery(SQL);
    //    ddlUser_P.DataValueField = "LoginId";
    //    ddlUser_P.DataTextField = "UserName";
    //    ddlUser_P.DataSource = dt1;
    //    ddlUser_P.DataBind();
    //    ddlUser_P.Items.Insert(0, new ListItem("All", "0"));
    //}
    protected void bindVessel()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT shipid,shipid + ' - ' + SHIPNAME AS SHIPNAME from VW_ACTIVEVESSELS ORDER BY SHIPNAME");
        ddl_Vessel.DataSource = dt;
        ddl_Vessel.DataValueField = "shipid";
        ddl_Vessel.DataTextField = "shipname";
        ddl_Vessel.DataBind();
        ddl_Vessel.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    protected void bindCurrencyddl()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT Curr FROM [dbo].[VW_tblWebCurr]");
        this.ddCurrency.DataValueField = "Curr";
        this.ddCurrency.DataTextField = "Curr";
        this.ddCurrency.DataSource = dt;
        this.ddCurrency.DataBind();
        ddCurrency.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    protected void bindVesselNameddl()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT shipid,shipid + ' - ' + SHIPNAME AS SHIPNAME from VW_ACTIVEVESSELS where VesselNo in (Select VesselId from UserVesselRelation with(nolock) where Loginid = "+ Convert.ToInt32(Session["loginid"].ToString())+") ORDER BY SHIPNAME");
        ddlF_Vessel.DataSource = dt;
        ddlF_Vessel.DataValueField = "shipid";
        ddlF_Vessel.DataTextField = "SHIPNAME";
        ddlF_Vessel.DataBind();
        ddlF_Vessel.Items.Insert(0, new ListItem("All", "0"));
    }
    protected void bindOwnerddl()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select COMPANY,COMPANY + ' - ' + [COMPANY NAME] AS 'COMPANY NAME' from [dbo].[AccountCompany] where active='Y' ORDER BY [COMPANY NAME]");
        ddlF_Owner.DataSource = dt;
        ddlF_Owner.DataValueField = "company";
        ddlF_Owner.DataTextField = "Company Name";
        ddlF_Owner.DataBind();
        ddlF_Owner.Items.Insert(0, new ListItem("All", "0"));
    }

    //protected void btnShowCancelInv_Click(object sender, EventArgs e)
    //{
    //    int InvoiceId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
    //    ViewState["InvoiceId"] = InvoiceId;
    //    dv_Cancel.Visible = true;
    //}
    protected void btnClosePOP_Click(object sender, EventArgs e)
    {
        dv_Cancel.Visible = false;
    }

    protected void btnCancelInvoice_Click(object sender, EventArgs e)
    {
        if (txtComments.Text.Trim() == "")
        {
            ProjectCommon.ShowMessage("Please enter comments.");
            return;
        }
        try
        {
            int InvoiceId = Common.CastAsInt32(ViewState["InvoiceId"]);
            Common.Execute_Procedures_Select_ByQuery("UPDATE dbo.Pos_Invoice SET Status='C',CancelledOn=getdate(),CancelledBy=" + UserId + ",StageComments='" + txtComments.Text.Trim().Replace("'", "`") + "',CancelRemarks='" + txtComments.Text.Trim().Replace("'", "`") + "' where InvoiceId=" + InvoiceId);
            dv_Cancel.Visible = false;
            btn_Search_Click(sender, e);
            ProjectCommon.ShowMessage("Cancelled Successfully");
        }
        catch (Exception ex)
        { ProjectCommon.ShowMessage("Unable to cancel Invoice."); }
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {

        DataTable dt = GetData();
        RptMyInvoices.DataSource = dt;
        RptMyInvoices.DataBind();
        lblRecordCount.Text = " ( " + dt.Rows.Count.ToString() + " ) Records found.";
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        txt_FDate1.Text = "";
        txt_FDate2.Text = "";
        txtF_InvNo.Text = "";
        txtF_RefNo.Text = "";
        txtF_Vendor.Text = "";

        ddlF_Owner.SelectedIndex = 0;
        ddlF_Stage.SelectedIndex = 0;
        ddlF_Status.SelectedIndex = 0;
        ddlF_Vessel.SelectedIndex = 0;
        chkAdvPayment.Checked = false;
        chkNonPo.Checked = false;
        chkAdvPayment.Enabled = true;
        chkNonPo.Enabled = true;
        //ddlUser_A.SelectedIndex = 0;
        //ddlUser_E.SelectedIndex = 0;
        //ddlUser_P.SelectedIndex = 0;
        //ddlUser_V.SelectedIndex = 0;
    }

    //protected void imgBtnShowUpdateUser_Click(object sender, EventArgs e)
    //{
    //    int InvoiceId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
    //    ViewState["InvoiceId"] = InvoiceId;
    //    DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM vw_POS_Invoices_001 WHERE InvoiceId=" + InvoiceId.ToString());
    //    if (dt.Rows.Count > 0)
    //    {
    //        lblRefNo.Text = dt.Rows[0]["RefNo"].ToString();
    //        txtSupplier_Update.Text = dt.Rows[0]["Vendor"].ToString();
    //        hfdSupplierId_Update.Value = dt.Rows[0]["SupplierId"].ToString();
    //        lblInvNo.Text = dt.Rows[0]["InvNo"].ToString();
    //        lblInvDate.Text = Common.ToDateString(dt.Rows[0]["InvDate"]);
    //        txt_InvAmount.Text = dt.Rows[0]["InvoiceAmount"].ToString();
    //        txt_ApprovedAmount.Text = (dt.Rows[0]["ApprovalAmount"] == null) ? "" : dt.Rows[0]["ApprovalAmount"].ToString();
    //        ddl_Vessel.SelectedValue = dt.Rows[0]["INVVESSELCODE"].ToString();
    //        ddCurrency.SelectedValue = dt.Rows[0]["Currency"].ToString();
    //        lblStage.Text = dt.Rows[0]["StageText"].ToString();
    //        lblStatus.Text = dt.Rows[0]["Status"].ToString();

    //        if (lblStatus.Text == "UnPaid" && lblStage.Text != "Entry")
    //        {
    //            //int Stage = Common.CastAsInt32(dt.Rows[0]["Stage"]);
    //            //int CurrentUserId = Common.CastAsInt32(dt.Rows[0]["CurrentUserId"]);
    //            //ViewState["Stage"] = Stage;
    //            //DropDownList FromList = null;
    //            //ddlCurrUser.Items.Clear();
    //            //if (Stage == 1) // Approval
    //            //{
    //            //    FromList = ddlUser_A;
    //            //}
    //            //else if (Stage == 2) // Verfication
    //            //{
    //            //    FromList = ddlUser_V;
    //            //}
    //            //else if (Stage == 3) // Payment
    //            //{
    //            //    FromList = ddlUser_P;
    //            //}

    //            //foreach (ListItem li in FromList.Items)
    //            //{
    //            //    if (li.Value != "0")
    //            //    {
    //            //        ddlCurrUser.Items.Add(new ListItem(li.Text,li.Value));
    //            //    }
    //            //}
    //            //ddlCurrUser.SelectedValue = CurrentUserId.ToString();
    //            dvUpdateUser.Visible = true;
    //        }
    //    }
    //}
    protected void btnUpdateUserSave_Click(object sender, EventArgs e)
    {
        int InvoiceId = Common.CastAsInt32(ViewState["InvoiceId"]);
        int Stage = Common.CastAsInt32(ViewState["Stage"]);
        //string UpdateUserColumnName="";
        //if (Stage == 1)
        //{
        //    UpdateUserColumnName = "ApprovalFwdTo";
        //}
        //else if (Stage == 2)
        //{
        //    UpdateUserColumnName = "VerificationFwdTo";
        //}
        //else if (Stage == 3)
        //{
        //    UpdateUserColumnName = "PaidFwdTo";
        //}

        if (lblInvNo.Text.Trim() == "")
        {
            ProjectCommon.ShowMessage("Please enter invoice #.");
            lblInvNo.Focus();
            return;
        }

        if (lblInvDate.Text.Trim() == "")
        {
            ProjectCommon.ShowMessage("Please enter invoice date.");
            lblInvDate.Focus();
            return;
        }

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT SupplierName FROM [dbo].[VW_tblSMDSuppliers] WHERE SupplierName='" + txtSupplier_Update.Text.Trim() + "'");
        if (dt.Rows.Count <= 0)
        {
            ProjectCommon.ShowMessage("Please select valid vendor");
            txtSupplier_Update.Focus();
            return;
        }

        dt = Common.Execute_Procedures_Select_ByQuery("SELECT InvoiceId, RefNo FROM [dbo].[POS_Invoice] WHERE SupplierId=" + hfdSupplierId_Update.Value.Trim() + " AND LTRIM(RTRIM(InvNo)) = '" + lblInvNo.Text.Trim() + "' AND InvoiceId <> " + InvoiceId);
        if (dt.Rows.Count > 0)
        {
            ProjectCommon.ShowMessage("Duplicate invoice. Already entered with Ref#: " + dt.Rows[0]["RefNo"].ToString());
            lblInvNo.Focus();
            return;
        }
        DataTable dt005 = Common.Execute_Procedures_Select_ByQuery("select 1 from dbo.[tblApEntries] b where b.intrav = 1 and b.bidid in (select a.bidid from pos_invoice_payment_po a where a.InvoiceId = " + InvoiceId + ")");
        if (dt005.Rows.Count > 0)
        {
            ProjectCommon.ShowMessage("Sorry ! Invoice modification is not allowed. Linked PO is already exported to traverse.");
            return;
        }
        try
        {
            object oApprovedAmount = null;
            if (txt_ApprovedAmount.Text.Trim() != "")
                oApprovedAmount = "  ,ApprovalAmount= " + Common.CastAsDecimal(txt_ApprovedAmount.Text.Trim());
            else
                oApprovedAmount = "  ,ApprovalAmount=null ";

            Common.Execute_Procedures_Select_ByQuery("UPDATE [DBO].TBLSMDPOMASTERBID SET InvoiceNo='" + lblInvNo.Text + "',BidInvoiceDate='" + lblInvDate.Text + "' WHERE BidID=(select top 1 a.bidid from pos_invoice_payment_po a where a.InvoiceId = " + InvoiceId + ")");
            string sql = "UPDATE dbo.Pos_Invoice SET InvNo='" + lblInvNo.Text + "',InvDate='" + lblInvDate.Text + "',SupplierId = " + hfdSupplierId_Update.Value.Trim() + ", InvoiceAmount = " + Common.CastAsDecimal(txt_InvAmount.Text.Trim()) + oApprovedAmount + ", Currency = '" + ddCurrency.SelectedValue.Trim() + "', INVVESSELCODE = '" + ddl_Vessel.SelectedValue.Trim() + "'  WHERE INVOICEID = " + InvoiceId;
            //Common.Execute_Procedures_Select_ByQuery("UPDATE dbo.Pos_Invoice SET SupplierId = " + hfdSupplierId_Update.Value.Trim() + ", ApprovalAmount= " + txt_ApprovedAmount.Text.Trim() + ", Currency = '" + ddCurrency.SelectedValue.Trim() + "', VesselId = " + ddl_Vessel.SelectedValue.Trim() + ",  " + UpdateUserColumnName + "=" + ddlCurrUser.SelectedValue + " WHERE INVOICEID=" + InvoiceId);
            Common.Execute_Procedures_Select_ByQuery(sql);
            dvUpdateUser.Visible = false;
            btn_Search_Click(sender, e);
            ProjectCommon.ShowMessage("Updated Successfully");
        }
        catch (Exception ex)
        { ProjectCommon.ShowMessage("Unable to update user. Error: " + ex.Message); }
    }
    protected void btnUpdateUserCancel_Click(object sender, EventArgs e)
    {
        dvUpdateUser.Visible = false;
    }

    protected void imgChangeStage_Click(object sender, EventArgs e)
    {
        ViewState["CHInvoiceId"] = Common.CastAsInt32(((ImageButton)sender).CommandArgument); ;
        dv_InvoiceStage.Visible = true;
    }
    protected void btnSaveInvoiceStage_Click(object sender, EventArgs e)
    {
        int InvoiceId = Common.CastAsInt32(ViewState["CHInvoiceId"]);
        Common.Execute_Procedures_Select_ByQuery("UPDATE dbo.POS_Invoice SET STAGE=1,ApprovalOn=NULL,VerificationFwdTo=NULL,PaidFwdTo=NULL,StageComments=NULL,ApprovalComments=NULL,ApprovalAmount=NULL WHERE INVOICEID=" + InvoiceId);
        Common.Execute_Procedures_Select_ByQuery("DELETE FROM dbo.POS_Invoice_Approvals WHERE INVOICEID=" + InvoiceId);
        dv_InvoiceStage.Visible = false;
        btn_Search_Click(sender, e);
    }
    protected void btnCloseInvoiceStage_Click(object sender, EventArgs e)
    {
        dv_InvoiceStage.Visible = false;
    }


    protected void chkAdvPayment_CheckedChanged(object sender, EventArgs e)
    {
        if(chkAdvPayment.Checked)
        {
            chkNonPo.Enabled = false;
            chkNonPo.Checked = false;
        }
        else
        {
            chkNonPo.Enabled = true;
            chkNonPo.Checked = false;
        }
    }

    protected void chkNonPo_CheckedChanged(object sender, EventArgs e)
    {
        if (chkNonPo.Checked)
        {
            chkAdvPayment.Enabled = false;
            chkAdvPayment.Checked = false;
        }
        else
        {
            chkAdvPayment.Enabled = true;
            chkAdvPayment.Checked = false;
        }
    }

    protected DataTable GetData()
    {
        string SQL = "";
        string Where = "";

        SQL = "SELECT * FROM vw_POS_Invoices_001 I " +
              "WHERE 1=1 AND I.INVVesselCode in (Select v.VesselCode from UserVesselRelation uvr with(nolock) inner join Vessel v with(nolock) on uvr.VesselId = v.VesselId where uvr.LoginId = "+ Common.CastAsInt32(Session["loginid"]) + ") ";

        if (txtF_Vendor.Text.Trim() != "")
        {
            Where += " AND Vendor LIKE '%" + txtF_Vendor.Text.Trim() + "%' ";
        }
        if (txtF_InvNo.Text.Trim() != "")
        {
            Where += " AND ( LTRIM(RTRIM(InvNo)) Like '" + txtF_InvNo.Text.Trim() + "%' OR PONO='" + txtF_InvNo.Text.Trim() + "' )";
        }
        if (txtF_PONo.Text.Trim() != "")
        {
            Where += " AND I.INVOICEID IN (SELECT INVOICEID FROM POS_Invoice_Payment_PO WHERE PONO='" + txtF_PONo.Text.Trim() + "')";
        }
        if (txtF_PVNo.Text.Trim() != "")
        {
            Where += " AND I.INVOICEID IN (SELECT InvoiceId FROM POS_Invoice_Payment_Invoices WHERE PaymentId IN (SELECT PaymentId FROM POS_INVOICE_PAYMENT WHERE PVNO='" + txtF_PVNo.Text.Trim() + "'))";
        }
        if (txtF_RefNo.Text.Trim() != "")
        {
            Where += " AND RefNo ='" + txtF_RefNo.Text.Trim() + "' ";
        }
        if (txt_FDate1.Text.Trim() != "")
        {
            Where += " AND INVDATE >='" + txt_FDate1.Text.Trim() + "' ";
        }
        if (txt_FDate2.Text.Trim() != "")
        {
            Where += " AND INVDATE <='" + txt_FDate2.Text.Trim() + "' ";
        }
        if (ddlF_Stage.SelectedIndex > 0)
        {
            Where += " AND Stage =" + ddlF_Stage.SelectedValue + " ";
        }
        if (ddlF_Status.SelectedIndex > 0)
        {
            Where += " AND Status ='" + ddlF_Status.SelectedValue + "' ";
        }
        if (ddlF_Owner.SelectedIndex > 0)
        {
            Where += " AND Company ='" + ddlF_Owner.SelectedValue + "' ";
        }
        if (ddlF_Vessel.SelectedIndex > 0)
        {
            Where += " AND INVVesselCode ='" + ddlF_Vessel.SelectedValue + "' ";
        }
        if (chkAdvPayment.Checked)
        {
            Where += " AND IsAdvPayment = 1 ";
        }
        if (chkNonPo.Checked)
        {
            Where += " AND IsNonPo = 1 ";
        }
        //if (ddlUser_E.SelectedIndex > 0)
        //{
        //    Where += " AND EntertedBy =" + ddlUser_E.SelectedValue + " ";
        //}
        //if (ddlUser_A.SelectedIndex > 0)
        //{
        //    Where += " AND ApprovalFwdTo=" + ddlUser_A.SelectedValue + " ";
        //    //Where += " AND ApprovalFwdTo=" + ddlUser_A.SelectedValue + " AND STAGE=2 ";
        //}
        //if (ddlUser_V.SelectedIndex > 0)
        //{
        //    Where += " AND VerificationFwdTo=" + ddlUser_V.SelectedValue + " ";
        //    //Where += " AND VerificationFwdTo=" + ddlUser_V.SelectedValue + " AND STAGE=3 ";
        //}
        //if (ddlUser_P.SelectedIndex > 0)
        //{
        //    Where += " AND PaidFwdTo =" + ddlUser_P.SelectedValue + " ";
        //    //Where += " AND PaidFwdTo =" + ddlUser_P.SelectedValue + " AND STATUS='Paid' ";
        //}


        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL + Where + " Order By InvoiceId Desc");
        return dt;
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        try
        {

            DataTable dt = GetData();
            //Create a dummy GridView
            //GridView GridView1 = new GridView();
            GvMyInvoices.Visible = true;
            GvMyInvoices.AllowPaging = false;
            GvMyInvoices.DataSource = dt;
            GvMyInvoices.DataBind();
           

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=FindInvoiceList.xls");
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Charset = "UTF-8";
            Response.ContentType = "application/vnd.ms-excel.12";

            //Response.ContentType = "application/vnd.ms-excel";
            ////Response.ContentType = "application/vnd.ms-excel";
            //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            //for (int i = 0; i < GridView1.Rows.Count; i++)
            //{
            //    //Apply text style to each Row
            //    GridView1.Rows[i].Attributes.Add("class", "textmode");
            //}
            GvMyInvoices.RenderControl(hw);
            //style to format numbers to string
            //string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            //Response.Write(style);
            Response.Output.Write(sw.ToString());
            GvMyInvoices.Visible = false;
            Response.Flush();
            Response.End();
        }
        catch (Exception ex)
        {

        }

    }
    public override void VerifyRenderingInServerForm(Control control)

    {

    }
}