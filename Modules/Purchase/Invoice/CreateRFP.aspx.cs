using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Modules_Purchase_Invoice_CreateRFP : System.Web.UI.Page
{
    public Int32 RFPId 
    {
        get { return Common.CastAsInt32(ViewState["RFPId"]); }
        set { ViewState["RFPId"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        if (!Page.IsPostBack)
        {
            btnAddInvoice.Visible = false;
            RFPId = Common.CastAsInt32(Request.QueryString["key"]);
            BindRadioListPaymentMode();
            string Invoices = "";
            if (RFPId <= 0)
            {
                Invoices = Session["InvoiceIds"].ToString();
                btnAddInvoice.Visible = true;
            }
            else
            {
                string SQL = "SELECT ISNULL(a.RFPNO,'') AS RFPNO,b.InvoiceId FROM POS_Invoice_RFP a with(nolock) Inner Join POS_Invoice_RFP_Mapping b with(nolock) on a.RFPId = b.RFPId WHERE a.RFPId=" + RFPId;
                DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
                if (dt.Rows.Count > 0)
                {
                    lblpvno.Text = dt.Rows[0]["RFPNO"].ToString();
                    
                    foreach(DataRow dr in dt.Rows)
                    {
                        if (Invoices == "")
                        {
                            Invoices = dr["InvoiceId"].ToString(); 
                        }
                        else
                        {
                            Invoices = Invoices + ","+ dr["InvoiceId"].ToString();
                        }
                        //btnAddInvoice.Visible = btn_Approve.Visible;
                    }
                }
            }
            bindPayment_ForwardToddl();
            ShowInvoiceDetails(Invoices);
        }
    }
    protected void BindRadioListPaymentMode()
    {
        string sqlPaymentMode = "Select PaymentTypeId, Currency from PaymentType  where StatusId = 'A'";
        DataTable dtPaymentMode = Common.Execute_Procedures_Select_ByQuery(sqlPaymentMode);
        if (dtPaymentMode.Rows.Count > 0)
        {
            rblPaymentMode.DataSource = dtPaymentMode;
            rblPaymentMode.DataTextField = "Currency";
            rblPaymentMode.DataValueField = "PaymentTypeId";
            rblPaymentMode.DataBind();
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

    public string getInvoices(int ExcludeId)
    {
        List<string> invoices = new List<string>();
        foreach (RepeaterItem i in RptInvoices.Items)
        {
            HiddenField hfd = (HiddenField)i.FindControl("hfdInvId");
            if (ExcludeId != Common.CastAsInt32(hfd.Value))
                invoices.Add(hfd.Value);
        }
        return String.Join(",", invoices.ToArray());
    }

    protected void btnSubmitForApproval_Click(object sender, EventArgs e)
    {
        string paymentType = rblPaymentMode.SelectedValue;
        if (paymentType == "")
        {
            lbl_inv_Message.Text = "Please select payment mode.";
            return;
        }

        if (Convert.ToInt32(ddlApprovalForwardTo.SelectedValue) <=0 )
        {
            lbl_inv_Message.Text = "Please select Approval.";
            ddlApprovalForwardTo.Focus();
            return;
        }
        try
        {
            // Common.Set_Procedures("Inv_InsertPaymentDetailsforRFP");
           
            Common.Set_Procedures("InsertRFPDetailsforInvoice");
            Common.Set_ParameterLength(9);
            Common.Set_Parameters(
                new MyParameter("@RFPId", RFPId),
                new MyParameter("@InvIds", getInvoices(0)),
                new MyParameter("@SupplierId", hfdSupplier.Value.Trim()),
                new MyParameter("@Currency", lblCurrency.Text.Trim()),
                new MyParameter("@PaymentType", paymentType),
                new MyParameter("@POSOwnerId", lblOwnerCode.Text),
                new MyParameter("@Stage", 1),
                new MyParameter("@RFPSubmittedBy", Session["loginid"].ToString()),
                new MyParameter("@RFPSubmittedRemarks", txtComments.Text.Trim())
                );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
               RFPId = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);
                lbl_inv_Message.Text = "Record Successfully Saved.";
                int App1 = Common.CastAsInt32(ddlApprovalForwardTo.SelectedValue);
                Common.Execute_Procedures_Select_ByQuery("EXEC POS_INVOICE_RFP_SENDFORAPPROVAL " + RFPId + "," + App1);
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

    protected void bindPayment_ForwardToddl()
    {
        DataTable dt1;

        string SQL = "select (FirstName + ' ' + LastName ) AS UserName, LoginId from dbo.usermaster where loginid in (select userid from pos_invoice_mgmt where Approval4 = 1) AND statusId='A' Order By UserName";
        dt1 = Common.Execute_Procedures_Select_ByQuery(SQL);
        this.ddlApprovalForwardTo.DataValueField = "LoginId";
        this.ddlApprovalForwardTo.DataTextField = "UserName";
        this.ddlApprovalForwardTo.DataSource = dt1;
        this.ddlApprovalForwardTo.DataBind();
        ddlApprovalForwardTo.Items.Insert(0, new ListItem("< Select User >", "0"));
    }

    protected void btnAddInvoice_Click(object sender, EventArgs e)
    {
        string sql = "SELECT isnull(ClsInvoice,0) FROM dbo.[POS_Invoice] WHERE InvoiceId NOT IN (0" + getInvoices(0) + " )";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt.Rows.Count > 0)
        {
            int ClsMode = Common.CastAsInt32(dt.Rows[0][0]);
            dv_OtherInvoices.Visible = true;
            RptAddInvoices.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.POS_INVOICE WHERE STATUS='U' AND isnull(ClsInvoice,0)=" + ClsMode + " AND InvoiceId NOT IN (SELECT InvoiceId FROM POS_Invoice_Payment_Invoices) AND SupplierId=" + ViewState["SUPPLIERID"].ToString() + " AND Currency='" + lblCurrency.Text + "' AND InvoiceId NOT IN ( 0" + getInvoices(0) + " )");
            RptAddInvoices.DataBind();
        }
    }

    protected void btnCloseAddPV_Click(object sender, EventArgs e)
    {
        dv_OtherInvoices.Visible = false;
    }

    protected void btnDeleteRow_Click(object sender, EventArgs e)
    {
        if (RptInvoices.Items.Count > 1)
        {
            int _TableId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
            if (RFPId > 0)
            {
                string invs = getInvoices(_TableId);
                Common.Execute_Procedures_Select_ByQuery("EXEC Update_Pos_Invoice_RFP_Mapping "+ RFPId + ",'" + invs + "'");
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

    protected void rad_Inv_OnCheckedChanged(object sender, EventArgs e)
    {
        int Invid = Common.CastAsInt32(((RadioButton)sender).CssClass);
        string invs = "";
        if (RFPId > 0)
        {

            DataTable DT = Common.Execute_Procedures_Select_ByQuery("select InvoiceId from POS_Invoice_RFP_Mapping with(nolock) WHERE RFPId=" + RFPId);
            if (DT.Rows.Count > 0)
            {
                foreach (DataRow dr in DT.Rows)
                {
                    if (invs == "")
                    {
                        invs = dr["InvoiceId"].ToString();
                    }
                    else
                    {
                        invs += "," + dr["InvoiceId"].ToString();
                    }
                }
                Common.Execute_Procedures_Select_ByQuery("EXEC Update_Pos_Invoice_RFP_Mapping " + RFPId + ",'" + invs + "'");
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