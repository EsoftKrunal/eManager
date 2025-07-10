using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Modules_Purchase_Invoice_RFPApproval : System.Web.UI.Page
{
    public Int32 PaymentId
    {
        get { return Common.CastAsInt32(ViewState["PaymentId"]); }
        set { ViewState["PaymentId"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        try
        {
            if (!Page.IsPostBack)
            {

                PaymentId = Common.CastAsInt32(Request.QueryString["key"]);

                if (PaymentId > 0)
                {
                    string Invoices = "";
                    string SQL = "SELECT ISNULL(PVNO,'') AS PVNO,LinkedInvoices FROM POS_Invoice_Payment WHERE PaymentId=" + PaymentId;
                    DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
                    if (dt.Rows.Count > 0)
                    {
                        lblpvno.Text = dt.Rows[0]["PVNO"].ToString();
                        //btnAddInvoice.Visible = btn_Approve.Visible;
                        Invoices = dt.Rows[0]["LinkedInvoices"].ToString();
                        ShowInvoiceDetails(Invoices);
                        ShowPaymentDetails(PaymentId);
                    }
                }

            }
        }
        catch(Exception ex)
        {
            lbl_inv_Message.Text = ex.Message.ToString();
        } 
    }
    public void ShowInvoiceDetails(String Invoices)
    {
        string SQL = "SELECT * FROM vw_POS_Invoices_001 I WHERE InvoiceId IN ( 0" + Invoices + " )";
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
        string SQL = "SELECT * FROM VW_PAYMENTVOUCHERS_001 I with(nolock) WHERE PaymentId =  " + payId + "";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt != null && dt.Rows.Count > 0)
        {
            lblpvno.Text = dt.Rows[0]["PVNO"].ToString();
            lblPaymentMode.Text = dt.Rows[0]["PaymentCurr"].ToString();
            lblApprovalSubmittedBy.Text = dt.Rows[0]["ApprovalSubmittedBy"].ToString();
            lblApprovalSubmittedComments.Text = dt.Rows[0]["ApprovalSubmmitedComments"].ToString();
            lblApprovalSubmittedOn.Text = Common.ToDateString(dt.Rows[0]["ApprovalSubmittedOn"].ToString());
            if (Convert.ToInt32(dt.Rows[0]["PaymentStage"]) > 1)
            {
                btnApprove.Visible = false;
                btnBacktoStage1.Visible = false;
            }
        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        dvRFPApprove.Visible = true;
        lblRFPPVNo.Text = lblpvno.Text;
        txtRFPApprovalRemarks.Text = "";
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

            Common.Execute_Procedures_Select_ByQuery("Update POS_Invoice_Payment_Approvals SET ApprovedOn = GETDATE() , Comments = '" + txtRFPApprovalRemarks.Text.Trim().Replace("'", "`") + "' where PaymentId=" + PaymentId);

            Common.Execute_Procedures_Select_ByQuery("Update POS_Invoice_Payment SET RFPApprovedOn = GETDATE() , RFPApprovedComments = '" + txtRFPApprovalRemarks.Text.Trim().Replace("'", "`") + "',RFPApprovedBy="+ int.Parse(Session["loginid"].ToString()) + ",Stage=2  where PaymentId=" + PaymentId);

            Common.Execute_Procedures_Select_ByQuery("EXEC SP_InsertRFPApprovalHistory "+ PaymentId + ",2,"+ int.Parse(Session["loginid"].ToString()) + ", '" + txtRFPApprovalRemarks.Text.Trim().Replace("'", "`") + "'");
            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alet", "alert('RFP Request approved successfully.');", true);

            txtRFPApprovalRemarks.Text = "";
            btnRFPApprove.Visible = false;
            btnApprove.Visible = false;
            btnBacktoStage1.Visible = false;
            dvRFPApprove.Visible = false;
        }
        catch(Exception ex)
        {
            lblMsgRFPApprove.Text = ex.Message.ToString();
        }
        
    }
    protected void btnBacktoStage1_Click(object sender, EventArgs e)
    {
        dvSendBackToApproval.Visible = true;
        lblSendBackRFPPVNo.Text = lblpvno.Text;
        txtRFPSendBackComments.Text = "";
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

            Common.Execute_Procedures_Select_ByQuery("Delete from POS_Invoice_Payment where PaymentId=" + PaymentId);

            Common.Execute_Procedures_Select_ByQuery("Delete POS_Invoice_Payment_Approvals where PaymentId=" + PaymentId);

            Common.Execute_Procedures_Select_ByQuery("EXEC SP_InsertRFPApprovalHistory " + PaymentId + ",0," + int.Parse(Session["loginid"].ToString()) + ", '" + txtRFPSendBackComments.Text.Trim().Replace("'", "`") + "'");

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
}