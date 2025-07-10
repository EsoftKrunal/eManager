using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Invoice_InvoicePayment : System.Web.UI.Page
{
    public Int32 PaymentId
    { get { return Common.CastAsInt32(ViewState["PaymentId"]); }
        set { ViewState["PaymentId"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        if (!Page.IsPostBack)
        {
            btnAddInvoice.Visible = false;
            PaymentId = Common.CastAsInt32(Request.QueryString["key"]);
            string Invoices = "";
            if (PaymentId <= 0)
            {
                Invoices = Session["InvoiceIds"].ToString();
                btnAddInvoice.Visible = true;
            }
            else
            {
                string SQL = "SELECT ISNULL(PVNO,'') AS PVNO,LinkedInvoices FROM POS_Invoice_Payment WHERE PaymentId=" + PaymentId;
                DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
                if (dt.Rows.Count > 0)
                {
                    lblpvno.Text = dt.Rows[0]["PVNO"].ToString();
                    btn_Email.Visible = true;
                    btn_Approve.Visible = lblpvno.Text.Trim() == "";
                    btnAddInvoice.Visible = btn_Approve.Visible;
                    Invoices = dt.Rows[0]["LinkedInvoices"].ToString();
                    ShowInvoiceMaster();
                }
            }
            ShowInvoiceDetails(Invoices);
        }
    }
    public void ShowInvoiceMaster()
    {
        string SQL = "SELECT * FROM  POS_Invoice_Payment WHERE PaymentId=" + PaymentId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt.Rows.Count > 0)
        {
            lblpvno.Text = dt.Rows[0]["PVNO"].ToString();
            rad_SGD.Checked = dt.Rows[0]["PaymentType"].ToString() == "S";
            rad_USD.Checked = dt.Rows[0]["PaymentType"].ToString() == "U";
            rad_INR.Checked = dt.Rows[0]["PaymentType"].ToString() == "R";
            
            lblCurrency.Text = dt.Rows[0]["Currency"].ToString();
            txtBankName.Text = dt.Rows[0]["BankName"].ToString();
            txtCreditActNo.Text = dt.Rows[0]["CreditActNo"].ToString();
            txtCreditActName.Text = dt.Rows[0]["CreditActName"].ToString();
            txtChequeTTNo.Text = dt.Rows[0]["ChequeTTNo"].ToString();
            txtChequeTTDate.Text =Common.ToDateString(dt.Rows[0]["ChequeTTDt"]);
            txtChequeTTAmt.Text= dt.Rows[0]["ChequeTTAmount"].ToString();
            txtBankCharges.Text = dt.Rows[0]["BankCharges"].ToString();
            txtVoucherNo.Text = dt.Rows[0]["VoucherNo"].ToString();
            txtComments.Text = dt.Rows[0]["Remarks"].ToString();
        }
    }
    public void ShowInvoiceDetails(String Invoices)
    {
        string SQL = "SELECT * FROM vw_POS_Invoices_001 I WHERE InvoiceId IN ( 0" + Invoices + " )";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

        lblVendorName.Text = dt.Rows[0]["Vendor"].ToString();
        ViewState["SUPPLIERID"]= dt.Rows[0]["SUPPLIERID"].ToString();
        lblCurrency.Text = dt.Rows[0]["Currency"].ToString();
        lblOwnerCode.Text = dt.Rows[0]["Company"].ToString();
        RptInvoices.DataSource = dt;
        RptInvoices.DataBind();

        lbltotal.Text = dt.Compute("SUM(ApprovalAmount)","").ToString();
        hfdSupplier.Value = dt.Rows[0]["SupplierId"].ToString();         
    }
    public string getInvoices(int ExcludeId)
    {
        List<string> invoices = new List<string>();
        foreach (RepeaterItem i in RptInvoices.Items)
        {
            HiddenField hfd = (HiddenField)i.FindControl("hfdInvId");
            if(ExcludeId!=Common.CastAsInt32(hfd.Value))
                invoices.Add(hfd.Value);
        }
        return String.Join(",",invoices.ToArray());
    }
   
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (!(rad_SGD.Checked || rad_USD.Checked || rad_INR.Checked))
        {
            lbl_inv_Message.Text = "Please select payment mode.";  
            return;
        }

        try
        {
            Common.Set_Procedures("Inv_InsertPaymentDetails_001");
            Common.Set_ParameterLength(16);
            Common.Set_Parameters(
                new MyParameter("@PaymentId", PaymentId),
                new MyParameter("@InvIds", getInvoices(0)),
                new MyParameter("@SupplierId", hfdSupplier.Value.Trim()),
                //new MyParameter("@OwnerId", ViewState["OwnerId"]),
                new MyParameter("@Currency", lblCurrency.Text.Trim()),
                new MyParameter("@BankName", txtBankName.Text.Trim()),
                new MyParameter("@CreditActNo", txtCreditActNo.Text.Trim()),
                new MyParameter("@CreditActName", txtCreditActName.Text.Trim()),
                new MyParameter("@ChequeTTNo", txtChequeTTNo.Text.Trim()),
                new MyParameter("@ChequeTTDt", txtChequeTTDate.Text.Trim()),
                new MyParameter("@ChequeTTAmount", Common.CastAsDecimal(txtChequeTTAmt.Text)),
                new MyParameter("@BankCharges",Common.CastAsDecimal(txtBankCharges.Text)),
                new MyParameter("@VoucherNo", txtVoucherNo.Text.Trim()),
                new MyParameter("@Remarks", txtComments.Text.Trim()),
                new MyParameter("@PaidBy", Session["loginid"].ToString()),
                new MyParameter("@PaymentType", ((rad_SGD.Checked) ? "S" : ((rad_USD.Checked) ? "U" : "R"))),
                new MyParameter("@POSOwnerId", lblOwnerCode.Text)
                );

            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                PaymentId = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);
                lbl_inv_Message.Text = "Record Successfully Saved.";
                btn_Email.Visible = true;
                btn_Approve.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "Refresh();", true);
            }
            else
            {
                lbl_inv_Message.Text = "Unable to save record. Error : " + Common.ErrMsg;
            }
        }
        catch (Exception ex)
        {
            lbl_inv_Message.Text = "Unable to save record." + ex.Message + Common.getLastError();
        }
    }
    
    protected void btn_Approve_Click(object sender, EventArgs e)
    {
        if (PaymentId<=0)
        {
            lbl_inv_Message.Text = "Please save voucher first.";
            return;
        }

        try
        {
            Common.Set_Procedures("Inv_InsertPaymentDetails_Approval_001");
            Common.Set_ParameterLength(5);
            Common.Set_Parameters(
                new MyParameter("@PaymentId", PaymentId),
                new MyParameter("@InvIds", getInvoices(0)),
                new MyParameter("@ApprovedBy", Session["UserFullName"].ToString()),
                new MyParameter("@ApprovedOn", DateTime.Today),
                new MyParameter("@Remarks", txtComments.Text.Trim())                
                );

            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                int Result=0;
                String Message = "";
                Result = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);
                Message = ds.Tables[0].Rows[0][1].ToString();
                if (Result != 0)
                {
                    lbl_inv_Message.Text = Message;
                }
                else
                {
                    lbl_inv_Message.Text = "Invoice Approved Successfully.";
                    btn_Save.Visible = false;
                    lblpvno.Text = Message;

                    string SQL = "SELECT LinkedInvoices FROM POS_Invoice_Payment WHERE PaymentId=" + PaymentId;
                    DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
                    if (dt.Rows.Count > 0)
                    {
                        ShowInvoiceDetails(dt.Rows[0][0].ToString());
                    }
                
                    btn_Approve.Visible = false;
                    btnAddInvoice.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "Refresh();", true);
                }
            }
            else
            {
                lbl_inv_Message.Text = "Unable to save record. Error : " + Common.ErrMsg;
            }
        }
        catch (Exception ex)
        {
            lbl_inv_Message.Text = "Unable to save record." + ex.Message + Common.getLastError();
        }
    }
    protected void btnDeleteRow_Click(object sender, EventArgs e)
    {
        if (RptInvoices.Items.Count > 1)
        {
            int _TableId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
            if (PaymentId > 0)
            { 
                string invs = getInvoices(_TableId);
                Common.Execute_Procedures_Select_ByQuery("UPDATE POS_Invoice_Payment SET LinkedInvoices='" + invs + "' WHERE PaymentId=" + PaymentId);
                ShowInvoiceDetails(invs);
            }
            else
            {
                string Invoices = Session["InvoiceIds"].ToString();
                char[] sep = { ',' };
                List<string> lstd = new List<string>(Invoices.Split(sep));
                lstd.Remove(_TableId.ToString());
                string invs = String.Join(",", lstd.ToArray());
                Session["InvoiceIds"] = invs;
                ShowInvoiceDetails(invs);
            }
        }
        else
        {
            lbl_inv_Message.Text = "Can not delete this invoice. There should be at least 1 invoice in the voucher.";
        }
    }
    protected void btn_Email_Click(object sender, EventArgs e)
    {

    }
    protected void btnAddInvoice_Click(object sender, EventArgs e)
    {
        string sql= "SELECT isnull(ClsInvoice,0) FROM dbo.[POS_Invoice] WHERE InvoiceId NOT IN (0" + getInvoices(0) + " )";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt.Rows.Count > 0)
        {
            int ClsMode = Common.CastAsInt32(dt.Rows[0][0]);
            dv_OtherInvoices.Visible = true;
            RptAddInvoices.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.POS_INVOICE WHERE STATUS='U' AND isnull(ClsInvoice,0)=" + ClsMode  + " AND InvoiceId NOT IN (SELECT InvoiceId FROM POS_Invoice_Payment_Invoices) AND SupplierId=" + ViewState["SUPPLIERID"].ToString() + " AND Currency='" + lblCurrency.Text + "' AND InvoiceId NOT IN ( 0" + getInvoices(0) + " )");
            RptAddInvoices.DataBind();
        }
    }
    protected void btnAddInPV_Click(object sender, EventArgs e)
    {
        dv_OtherInvoices.Visible = false;
    }
    protected void btnCloseAddPV_Click(object sender, EventArgs e)
    {
        dv_OtherInvoices.Visible = false;
    }
    protected void rad_Inv_OnCheckedChanged(object sender, EventArgs e)
    {
        int Invid = Common.CastAsInt32(((RadioButton)sender).CssClass);
        string invs = "";
        if (PaymentId > 0)           
        {
            
            DataTable DT=Common.Execute_Procedures_Select_ByQuery("select LinkedInvoices from POS_Invoice_Payment WHERE PaymentId=" + PaymentId);
            if (DT.Rows.Count > 0)
            {
                invs = DT.Rows[0][0].ToString();
                invs += "," + Invid.ToString();
                Common.Execute_Procedures_Select_ByQuery("UPDATE POS_Invoice_Payment SET LinkedInvoices='" + invs + "' WHERE PaymentId=" + PaymentId);
                ShowInvoiceDetails(invs);
            }
        }
        else
        {
            string Invoices = Session["InvoiceIds"].ToString();
            char[] sep = { ',' };
            List<string> lstd = new List<string>(Invoices.Split(sep));
            lstd.Add(Invid.ToString());
            invs = String.Join(",", lstd.ToArray());
            Session["InvoiceIds"] = invs;
            ShowInvoiceDetails(invs);

        }
            dv_OtherInvoices.Visible = false;
    }

    
}