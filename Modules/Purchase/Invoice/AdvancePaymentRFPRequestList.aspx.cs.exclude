using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Modules_Purchase_Invoice_InvoiceRFPList : System.Web.UI.Page
{
    public int RFPId
    {
        get { return Convert.ToInt32(ViewState["RFPId"]); }
        set { ViewState["RFPId"] = value; }
    }
    public int TableId
    {
        get { return Convert.ToInt32(ViewState["TableId"]); }
        set { ViewState["TableId"] = value; }
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
        lblMsgMain.Text = "";
        //lblMsgPOP.Text = "";
        //lblmsg1.Text = "";
        if (!Page.IsPostBack)
        {
            UserId = Common.CastAsInt32(Session["loginid"]);
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT Payment FROM POS_Invoice_mgmt where USERID=" + UserId);

            if ((dt.Rows.Count > 0 && dt.Rows[0]["Payment"].ToString() == "True") || UserId == 1)
            {
               
                bindOwnerddl();
                btn_Search_Click(sender, e);
            }
            else
            {
                Response.Redirect("~/NoPermission.aspx");
            }
        }
    }
    protected void bindOwnerddl()
    {
        //DataTable dt = Common.Execute_Procedures_Select_ByQuery("select OwnerId,OwnerName from dbo.Owner where statusid='A' order by OwnerCode ");
        //ddlF_Owner.DataSource = dt;
        //ddlF_Owner.DataValueField = "OwnerId";
        //ddlF_Owner.DataTextField = "OwnerName";
        //ddlF_Owner.DataBind();
        //ddlF_Owner.Items.Insert(0, new ListItem("All", "0"));


        //ddlOwner1.DataSource = dt;
        //ddlOwner1.DataValueField = "OwnerId";
        //ddlOwner1.DataTextField = "OwnerName";
        //ddlOwner1.DataBind();
        //ddlOwner1.Items.Insert(0, new ListItem("All", ""));

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select COMPANY,COMPANY + ' - ' + [COMPANY NAME] AS 'COMPANY NAME'from [dbo].[AccountCompany] where active='Y' ORDER BY [COMPANY NAME]");
        ddlF_Owner.DataSource = dt;
        ddlF_Owner.DataValueField = "company";
        ddlF_Owner.DataTextField = "Company Name";
        ddlF_Owner.DataBind();
        ddlF_Owner.Items.Insert(0, new ListItem("All", "0"));

        //ddlOwner1.DataSource = dt;
        //ddlOwner1.DataValueField = "company";
        //ddlOwner1.DataTextField = "Company Name";
        //ddlOwner1.DataBind();
        //ddlOwner1.Items.Insert(0, new ListItem("All", ""));
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtF_InvNo.Text = "";
        txtF_PVNo.Text = "";
        txtF_Vendor.Text = "";

        ddlF_Owner.SelectedIndex = 0;
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        string SQL = "";
        string Where = "";

        //SQL = "select *,Case when (Status <> 'C' and PaymentStage <= 1 and ApprovalSentBy = "+UserId+") then 'True' Else 'False' END as AllowDel FROM VW_PAYMENTVOUCHERS_001 ";
        SQL = "select *,Case when (Status <> 'C' and PaymentStage <= 1 and (select Cancel from POS_Invoice_Mgmt where  UserId = " + UserId + ") =1) then 'True' Else 'False' END as AllowDel FROM VW_POSInvoiceRFPList with(nolock) ";

        if (UserId == 1)
        {
            SQL += " WHERE 1=1 ";
            if (Convert.ToInt32(ddlStatus.SelectedValue) == 1)
            {
                SQL += " AND PaymentStage = 1 and Status = 'A' ";
            }
            else if (Convert.ToInt32(ddlStatus.SelectedValue) == 2)
            {
                SQL += " AND PaymentStage = 2 and Status = 'A' ";
            }
        }
        else
        {
            SQL += " WHERE 1=1 ";
            if (Convert.ToInt32(ddlStatus.SelectedValue) == 1)
            {
                SQL += " AND PaymentStage = 1 and Status = 'A' ";
            }
            else if (Convert.ToInt32(ddlStatus.SelectedValue) == 2)
            {
                SQL += " AND PaymentStage = 2 and Status = 'A' ";
            }
        }

        if (txtF_Vendor.Text.Trim() != "")
        {
            Where += " AND SupplierName LIKE '%" + txtF_Vendor.Text.Trim() + "%' ";
        }


        if (txtF_PVNo.Text.Trim() != "")
        {
            Where += " AND RFPNo ='" + txtF_PVNo.Text.Trim() + "' ";
        }

        //string StatusClause = "";
        

        //if (Convert.ToInt32(ddlStatus.SelectedValue) > 0)
        //{
        //    StatusClause = " isnull(PaymentStage,0) =" + ddlStatus.SelectedValue + "  ";
        //}
       
        //if (! string.IsNullOrWhiteSpace(StatusClause))
        //{
        //    Where += " AND  " + StatusClause + " ";
        //}

        if (ddlF_Owner.SelectedIndex > 0)
        {
            Where += " AND POSOwnerId ='" + ddlF_Owner.SelectedValue + "' ";
        }

        if (txtF_D1.Text != "")
        {
            Where += " AND RFPSubmittedOn >='" + txtF_D1.Text + "' ";
        }

        if (txtF_D2.Text != "")
        {
            Where += " AND RFPSubmittedOn <='" + Convert.ToDateTime(txtF_D2.Text).AddDays(1).ToString("dd-MMM-yyyy") + "' ";
        }
        //if (ddlF_Vessel.SelectedIndex > 0)
        //{
        //    Where += " AND VesselId =" + ddlF_Vessel.SelectedValue + " ";
        //}

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL + Where + " Order By RFPId Desc");

        if (dt != null && dt.Rows.Count > 0)
        {
            RptMyInvoices.DataSource = dt;
            RptMyInvoices.DataBind();
        }
        else
        {
            RptMyInvoices.DataSource = null;
            RptMyInvoices.DataBind();
        }
       
    }

    protected void btnAskCancel_Click(object sender, EventArgs e)
    {
        int _RFPId = 0;
        try
        {
            _RFPId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
            if (_RFPId > 0)
            {

                string sql = "SELECT ISNULL(RFPNO,'') FROM VW_POSInvoiceRFPList WHERE RFPId=" + _RFPId.ToString();
                DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
                if (dt.Rows.Count > 0)
                {
                    string pvno = dt.Rows[0][0].ToString();
                    Common.Execute_Procedures_Select_ByQuery("DELETE FROM DBO.POS_Invoice_RFP_Approvals WHERE RFPId=" + _RFPId.ToString());
                    Common.Execute_Procedures_Select_ByQuery("EXEC InsertRFPApprovalHistory " + _RFPId + ",3," + UserId + ",'RFP Request Deleted'");
                    Common.Execute_Procedures_Select_ByQuery("DELETE FROM DBO.POS_Invoice_RFP WHERE RFPId=" + _RFPId.ToString());
                 
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alet", "alert('RFP Request deleted successfully.');", true);
                    btn_Search_Click(sender, e);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dialog", "window.opener.RefereshPage();", true);
                    // ScriptManager.RegisterStartupScript(this, this.GetType(), "alet", "alert('RFP Request deleted successfully.');", true);

                }
            }

        }
        catch (Exception ex)
        {
            lblMsgMain.Text = ex.Message.ToString();
           
        }
        //-------------------------
        
    }

    //protected void btnEdit_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        PaymentId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
    //    }
    //    catch
    //    {
    //        PaymentId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
    //    }

    //    string PTYPE = "";
    //    PTYPE = ((ImageButton)sender).Attributes["PTYPE"];

    //    ddlOwner1.Enabled = false;
    //    rad_USD.Enabled = false;
    //    rad_SGD.Enabled = false;
    //    rad_INR.Enabled = false;
    //    if (PTYPE == "N")
    //    {
    //        ShowRecord();
    //    }
    //    else
    //    {
    //        ShowPaymentRecord();
    //    }
        
    //    btnSaveVoucher.Visible = true;
    //    btnVoucherPrint.Visible = true;
    //    dv_NewPV.Visible = true;
    //    txtVendorName.Focus();
    //}
    

    

    

    
    

    protected void ShowMessage(Label l1, string Msg, bool error)
    {
        l1.Text = Msg;
        l1.ForeColor = (error) ? System.Drawing.Color.Red : System.Drawing.Color.Green;
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

    protected void lbPVNo_Click(object sender, EventArgs e)
    {
        int _RFPId = 0;
        try
        {
            _RFPId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
            if (_RFPId > 0)
            {
                dv_NewPV.Visible = true;
                RFPId = _RFPId;
                string Invoices = "";
                string SQL = "SELECT ISNULL(RFPNo,'') AS RFPNo,InvoiceId FROM POS_Invoice_RFP a with(nolock) inner join POS_Invoice_RFP_Mapping b with(nolock) on a.RFPId = b.RFPId   WHERE a.RFPId=" + RFPId;
                DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
                if (dt.Rows.Count > 0)
                {
                    lblpvno.Text = dt.Rows[0]["RFPNo"].ToString();
                    foreach (DataRow dr in dt.Rows)
                        {
                        if (Invoices == "")
                        {
                            Invoices = dr["InvoiceId"].ToString();
                        }
                        else
                        {
                            Invoices = Invoices +","+ dr["InvoiceId"].ToString();
                        }

                    }

                    ////btnAddInvoice.Visible = btn_Approve.Visible;
                    //Invoices = dt.Rows[0]["LinkedInvoices"].ToString();
                    ShowInvoiceDetails(Invoices);
                    ShowPaymentDetails(RFPId);
                }
            }
        }
         catch (Exception ex)
        {
            lblMsgMain.Text = ex.Message.ToString();
        }
    }
    public void ShowInvoiceDetails(String Invoices)
    {
        string SQL = "SELECT * FROM vw_POS_Invoices_RFP I WHERE InvoiceId IN ( 0" + Invoices + " )";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        lblVendorName.Text = dt.Rows[0]["Vendor"].ToString();
        ViewState["SUPPLIERID"] = dt.Rows[0]["SUPPLIERID"].ToString();
        lblCurrency.Text = dt.Rows[0]["Currency"].ToString();
        lblOwnerCode.Text = dt.Rows[0]["Company"].ToString();
        lblOwnerName.Text = dt.Rows[0]["CompanyName"].ToString();
        RptInvoices.DataSource = dt;
        RptInvoices.DataBind();

        lbltotal.Text = dt.Compute("SUM(ApprovalAmount)", "").ToString();
        hfdSupplier.Value = dt.Rows[0]["SupplierId"].ToString();
    }
    public void ShowPaymentDetails(int payId)
    {
        string SQL = "SELECT * FROM VW_POSInvoiceRFPList I with(nolock) WHERE RFPId =  " + payId + "";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt != null && dt.Rows.Count > 0)
        {
            lblpvno.Text = dt.Rows[0]["RFPNo"].ToString();
            lblPaymentMode.Text = dt.Rows[0]["PaymentCurr"].ToString();
            lblApprovalSubmittedBy.Text = dt.Rows[0]["RFPSubmittedBy"].ToString();
            lblApprovalSubmittedComments.Text = dt.Rows[0]["RFPSubmmitedComments"].ToString();
            lblApprovalSubmittedOn.Text = Common.ToDateString(dt.Rows[0]["RFPSubmittedOn"].ToString());
            if (Convert.ToInt32(dt.Rows[0]["PaymentStage"]) > 1)
            {
                btnApprove.Visible = false;
                btnBacktoStage1.Visible = false;
            }
            else
            {
                string SQL1= "Select Approval4 from POS_Invoice_Mgmt where UserId =   " + int.Parse(Session["loginid"].ToString()) + "";
                DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(SQL1);
                if (dt1 != null && dt1.Rows.Count > 0 && Convert.ToInt32(dt1.Rows[0]["Approval4"]) == 1)
                {
                btnApprove.Visible = true;
                btnBacktoStage1.Visible = true;
                    
                }
                else
                {
                    btnApprove.Visible = false;
                    btnBacktoStage1.Visible = false;
                }

            }
        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        dvRFPApprove.Visible = true;
        lblRFPPVNo.Text = lblpvno.Text;
        txtRFPApprovalRemarks.Text = "";
        btnRFPApprove.Visible = true;
    }
    protected void btnCloseRFPApprove_Click(object sender, EventArgs e)
    {
        dvRFPApprove.Visible = false;
        txtRFPApprovalRemarks.Text = "";
        lblRFPPVNo.Text = "";
        lblMsgRFPApprove.Text = "";
    }
    protected void btnRFPApprove_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txtRFPApprovalRemarks.Text))
            {
                lblMsgRFPApprove.Text = "Please enter comments.";
                txtRFPApprovalRemarks.Focus();
                return;
            }

            Common.Execute_Procedures_Select_ByQuery("Update POS_Invoice_RFP_Approvals SET ApprovedOn = GETDATE() , Comments = '" + txtRFPApprovalRemarks.Text.Trim().Replace("'", "`") + "' where RFPId=" + RFPId);

            Common.Execute_Procedures_Select_ByQuery("Update POS_Invoice_RFP SET RFPApprovedOn = GETDATE() , RFPApprovedComments = '" + txtRFPApprovalRemarks.Text.Trim().Replace("'", "`") + "',RFPApprovedBy=" + int.Parse(Session["loginid"].ToString()) + ",Stage=2  where RFPId=" + RFPId);

            Common.Execute_Procedures_Select_ByQuery("EXEC InsertRFPApprovalHistory " + RFPId + ",2," + int.Parse(Session["loginid"].ToString()) + ", '" + txtRFPApprovalRemarks.Text.Trim().Replace("'", "`") + "'");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alet", "alert('RFP Request approved successfully.');", true);

            txtRFPApprovalRemarks.Text = "";
            btnRFPApprove.Visible = false;
            btnApprove.Visible = false;
            btnBacktoStage1.Visible = false;
            dvRFPApprove.Visible = false;
        }
        catch (Exception ex)
        {
            lblMsgRFPApprove.Text = ex.Message.ToString();
        }

    }
    protected void btnBacktoStage1_Click(object sender, EventArgs e)
    {
        dvSendBackToApproval.Visible = true;
        lblSendBackRFPPVNo.Text = lblpvno.Text;
        txtRFPSendBackComments.Text = "";
        btnSave.Visible = true;
    }

    protected void btnApproveRFPRequest_ClosePopup_Click(object sender, ImageClickEventArgs e)
    {
        dvRFPApprove.Visible = false;
        txtRFPApprovalRemarks.Text = "";
        lblRFPPVNo.Text = "";
        lblMsgRFPApprove.Text = "";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txtRFPSendBackComments.Text))
            {
                lblMsgSendBackRFP.Text = "Please enter comments.";
                txtRFPSendBackComments.Focus();
                return;
            }

            Common.Execute_Procedures_Select_ByQuery("Delete from POS_Invoice_RFP  where RFPId=" + RFPId);

            Common.Execute_Procedures_Select_ByQuery("Delete POS_Invoice_RFP_Approvals where RFPId=" + RFPId);

            Common.Execute_Procedures_Select_ByQuery("EXEC InsertRFPApprovalHistory " + RFPId + ",0," + int.Parse(Session["loginid"].ToString()) + ", '" + txtRFPSendBackComments.Text.Trim().Replace("'", "`") + "'");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alet", "alert('RFP Request rejected successfully.');", true);

            txtRFPSendBackComments.Text = "";
            btnSave.Visible = false;
            btnApprove.Visible = false;
            btnBacktoStage1.Visible = false;
            dvSendBackToApproval.Visible = false;

        }
        catch (Exception ex)
        {
            lblMsgRFPApprove.Text = ex.Message.ToString();
        }
    }
    protected void btnSendBackClose_Click(object sender, EventArgs e)
    {
        dvSendBackToApproval.Visible = false;
        lblMsgSendBackRFP.Text = "";
        txtRFPSendBackComments.Text = "";
        lblSendBackRFPPVNo.Text = "";
    }
    protected void btnSendBackRFP_ClosePopup_Click_Click(object sender, ImageClickEventArgs e)
    {
        dvSendBackToApproval.Visible = false;
        lblMsgSendBackRFP.Text = "";
        txtRFPSendBackComments.Text = "";
        lblSendBackRFPPVNo.Text = "";
    }

    protected void btnCloseRFPapproval_Click(object sender, EventArgs e)
    {
        dv_NewPV.Visible = false;
        btn_Search_Click(sender, e);
    }
}