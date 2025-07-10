using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Activities.Expressions;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.DataVisualization.Charting;
using System.EnterpriseServices.Internal;

public partial class Modules_Purchase_Invoice_PaymentList : System.Web.UI.Page
{

    public int PaymentId
    {
        get { return Convert.ToInt32(ViewState["PaymentId"]); }
        set { ViewState["PaymentId"] = value; }
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

    public int invoiceCount
    {
        get { return Convert.ToInt32(ViewState["invoiceCount"]); }
        set { ViewState["invoiceCount"] = value; }
    }

    public string RFPIds
    {
        get { return ViewState["RFPIds"].ToString(); }
        set { ViewState["RFPIds"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        lblMsgMain.Text = "";
        lblMsgPOP.Text = "";
        
        if (!Page.IsPostBack)
        {
            UserId = Common.CastAsInt32(Session["loginid"]);
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT Payment FROM POS_Invoice_mgmt where USERID=" + UserId);

            if ((dt.Rows.Count > 0 && dt.Rows[0]["Payment"].ToString() == "True") || UserId == 1)
            {
                bindOwnerddl();
                bindVesselNameddl(0);
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
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select COMPANY,COMPANY + ' - ' + [COMPANY NAME] AS 'COMPANY NAME'from [dbo].[AccountCompany] where active='Y' ORDER BY [COMPANY NAME]");
        ddlF_Owner.DataSource = dt;
        ddlF_Owner.DataValueField = "company";
        ddlF_Owner.DataTextField = "Company Name";
        ddlF_Owner.DataBind();
        ddlF_Owner.Items.Insert(0, new ListItem("All", "0"));
    }

    public void ShowInvoiceDetails(String Invoices)
    {
        string SQL = "SELECT * FROM vw_POS_Invoices_RFP I with(nolock) WHERE InvoiceId IN ( 0" + Invoices + " )";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        lblVendorName.Text = dt.Rows[0]["Vendor"].ToString();
        ViewState["SUPPLIERID"] = dt.Rows[0]["SUPPLIERID"].ToString();
        lblCurrency.Text = dt.Rows[0]["Currency"].ToString();
        lblOwnerCode.Text = dt.Rows[0]["Company"].ToString();
        lblPaymentType.Text = dt.Rows[0]["PaymentType"].ToString();
        lblOwnerName.Text = dt.Rows[0]["CompanyName"].ToString();
        lblPaymentMode.Text = dt.Rows[0]["PaymentCurr"].ToString();
        RptInvoices.DataSource = dt;
        RptInvoices.DataBind();
        invoiceCount = dt.Rows.Count;

        lbltotal.Text = Math.Round(Convert.ToDecimal(dt.Compute("SUM(ApprovalAmount)", "").ToString()) - Convert.ToDecimal(dt.Compute("SUM(TDSAmount)", "").ToString()),2).ToString();
        lblpayAmt.Text = Math.Round(Convert.ToDecimal(dt.Compute("SUM(PayAmount)", "").ToString()) - Convert.ToDecimal(dt.Compute("SUM(TDSAmount)", "").ToString()), 2).ToString();
        txtChequeTTAmt.Text = lblpayAmt.Text;
        hfdSupplier.Value = dt.Rows[0]["SupplierId"].ToString();

        string sql1 = "select dbo.fn_Get_VoucherNo_001('" + lblOwnerCode.Text + "','" + lblPaymentType.Text + "') As PVNo";
        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(sql1);
        if (dt1.Rows.Count > 0)
        {
            lblpvno.Text = dt1.Rows[0]["PVNo"].ToString();
        }
    }
    public void ShowPaymentDetails(int payId)
    {
        string SQL = "SELECT top 1 * FROM VW_PAYMENTVOUCHERS_001 I with(nolock) WHERE PaymentId =  " + payId + "";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt != null && dt.Rows.Count > 0)
        {
            lblpvno.Text = dt.Rows[0]["PVNO"].ToString();
            // lblPaymentMode.Text = dt.Rows[0]["PaymentCurr"].ToString();
            if (! string.IsNullOrWhiteSpace(dt.Rows[0]["BankName"].ToString()))
            {
                txtBankName.Text = dt.Rows[0]["BankName"].ToString();
            }
            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["BankCharges"].ToString()))
            {
                txtBankCharges.Text = dt.Rows[0]["BankCharges"].ToString();
            }
            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["ChequeTTAmount"].ToString()))
            {
                txtChequeTTAmt.Text = dt.Rows[0]["ChequeTTAmount"].ToString();
            }

            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["ChequeTTDt"].ToString()))
            {
                txtChequeTTDate.Text = Common.ToDateString(dt.Rows[0]["ChequeTTDt"].ToString());
            }
            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["ChequeTTNo"].ToString()))
            {
                txtChequeTTNo.Text = dt.Rows[0]["ChequeTTNo"].ToString();
            }
            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["CreditActNo"].ToString()))
            {
                txtCreditActNo.Text = dt.Rows[0]["CreditActNo"].ToString();
            }
            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["CreditActName"].ToString()))
            {
                txtCreditActName.Text = dt.Rows[0]["CreditActName"].ToString();
            }
            if (! string.IsNullOrWhiteSpace(dt.Rows[0]["Remarks"].ToString()))
            {
                txtComments.Text = dt.Rows[0]["Remarks"].ToString();
            }
            if (!string.IsNullOrWhiteSpace(dt.Rows[0]["PaymentStatus"].ToString()) && dt.Rows[0]["PaymentStatus"].ToString() == "P")
            {
                btn_Payment.Visible = false;
                btnPrint.Visible = true;
                btnAddUpdatePaymentDoc.Visible = true;

            }
           
        }
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

    protected void btn_Payment_Click(object sender, EventArgs e)
    {
        //if (PaymentId <= 0)
        //{
        //    return;
        //}

        try
        {
            decimal totalPayAmt = Common.CastAsDecimal(lblpayAmt.Text);
            decimal bankCharges = 0;
            if (! string.IsNullOrEmpty(txtBankCharges.Text.Trim()))
            {
                bankCharges = Common.CastAsDecimal(txtBankCharges.Text.Trim());
            }
            decimal totalChequeTTAmt = 0;
            if (!string.IsNullOrEmpty(txtChequeTTAmt.Text.Trim()))
            {
                totalChequeTTAmt = Common.CastAsDecimal(txtChequeTTAmt.Text.Trim());
            }

            decimal diffAmt = totalChequeTTAmt - (totalPayAmt + bankCharges);
            if (diffAmt > 0)
            {
                lblMsgPOP.Text = "Cheque Amount should not greater than Pay Amount and Bank Charges.";
                txtChequeTTAmt.Focus();
                return;
            }
            int i = 0;
            //foreach (RepeaterItem ri in RptInvoices.Items)
            //{
            //    int isNonPoEntrypending = Convert.ToInt32(((HiddenField)ri.FindControl("hdnAllowforNonpo")).Value);
            //    if (isNonPoEntrypending == 1)
            //    {
            //        lblMsgPOP.Text = "Account Code is missing for Non PO Invoice. Please update Non PO transaction. ";
            //        return;
            //    }

            //}
                foreach (RepeaterItem ri in RptInvoices.Items)
            {
                i = i + 1;
                int invoiceId = Common.CastAsInt32(((HiddenField)ri.FindControl("hfdInvId")).Value);
                int RFPId = Common.CastAsInt32(((HiddenField)ri.FindControl("hfdRFPId")).Value);
                int supplierId = Common.CastAsInt32(((HiddenField)ri.FindControl("hdnSupplierId")).Value);
                decimal ApprovalAmt = Common.CastAsDecimal(((HiddenField)ri.FindControl("hdnApprovlAmt")).Value);
                bool AdvPayment = Convert.ToBoolean(((HiddenField)ri.FindControl("hdnAdvPayment")).Value);
               // bool NonPo = Convert.ToBoolean(((HiddenField)ri.FindControl("hdnNonPo")).Value);
                TextBox txtPayAmount = (TextBox)ri.FindControl("txtPayAmt");
                TextBox txttdsAmt = (TextBox)ri.FindControl("txtTDSAmount");
                if (!string.IsNullOrWhiteSpace(txttdsAmt.Text))
                {
                    txttdsAmt.Text = String.Format("{0:0.00}", txttdsAmt.Text);
                    Common.Execute_Procedures_Select_ByQuery("Update [POS_Invoice] SET TDSAmount = " + Common.CastAsDecimal(txttdsAmt.Text) + " where InvoiceId= " + invoiceId);
                }

                if (!string.IsNullOrWhiteSpace(txtPayAmount.Text))
                {
                    txtPayAmount.Text = String.Format("{0:0.00}", txtPayAmount.Text);
                    //Common.Execute_Procedures_Select_ByQuery("Update [POS_Invoice] SET TDSAmount = " + Common.CastAsDecimal(txttdsAmt.Text) + " where InvoiceId= " + invoiceId);
                }

                try
                {
                    Common.Set_Procedures("Inv_InsertPaymentDetails");
                    Common.Set_ParameterLength(20);
                    Common.Set_Parameters(
                        new MyParameter("@PaymentId", 0),
                        new MyParameter("@PVNo", lblpvno.Text),
                        new MyParameter("@SupplierId", supplierId),
                        //new MyParameter("@OwnerId", ViewState["OwnerId"]),
                        new MyParameter("@Currency", lblCurrency.Text.Trim()),
                        new MyParameter("@BankName", txtBankName.Text.Trim()),
                        new MyParameter("@CreditActNo", txtCreditActNo.Text.Trim()),
                        new MyParameter("@CreditActName", txtCreditActName.Text.Trim()),
                        new MyParameter("@ChequeTTNo", txtChequeTTNo.Text.Trim()),
                        new MyParameter("@ChequeTTDt", txtChequeTTDate.Text.Trim()),
                        new MyParameter("@ChequeTTAmount", Common.CastAsDecimal(txtChequeTTAmt.Text)),
                        new MyParameter("@BankCharges", Common.CastAsDecimal(txtBankCharges.Text)),
                        new MyParameter("@VoucherNo", ""),
                        new MyParameter("@Remarks", txtComments.Text.Trim()),
                        new MyParameter("@PaidBy", Session["loginid"].ToString()),
                        new MyParameter("@PaymentType", lblPaymentType.Text),
                        new MyParameter("@POSOwnerId", lblOwnerCode.Text),
                        new MyParameter("@RFPId", RFPId),
                        new MyParameter("@InvoiceId", invoiceId),
                        new MyParameter("@PayAmount", Common.CastAsDecimal(txtPayAmount.Text)),
                        new MyParameter("@IsAdvPayment", AdvPayment ? 1 : 0)
                        //,new MyParameter("@IsNonPo", NonPo ? 1 : 0)
                        );
                    DataSet ds = new DataSet();
                    ds.Clear();
                    Boolean res;
                    res = Common.Execute_Procedures_IUD(ds);
                }
                catch(Exception ex)
                {
                    lblMsgPOP.Text = "Unable to save record. Error : " + Common.ErrMsg;
                }
              
            }
            if (invoiceCount == i)
            {
                lblMsgPOP.Text = "Payment Successfully.";
                btn_Payment.Visible = false;
                btnPrint.Visible = true;
                btnAddUpdatePaymentDoc.Visible = true;
                btn_Search_Click(sender, e);
                string sql = "Select top 1 PaymentId from Pos_Invoice_Payment with(nolock) where  PVNO = '" +lblpvno.Text+ "' Order by PaymentId ";
                DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
                if (dt.Rows.Count > 0)
                {
                    PaymentId = Convert.ToInt32(dt.Rows[0]["PaymentId"].ToString());
                }
            }
            else
            {
                lblMsgPOP.Text = "Unable to save record. Error : " + Common.ErrMsg;
            }
        }
        catch (Exception ex)
        {
            lblMsgPOP.Text = "Unable to save record." + ex.Message + Common.getLastError();
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtF_InvNo.Text = "";
        txtF_PVNo.Text = "";
        txtF_Vendor.Text = "";
        ddlF_Owner.SelectedIndex = 0;
        bindVesselNameddl(0);
    }

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        if (ddlStatus.SelectedValue == "" )
        {
            lblMsg.Text = "Please select payment status.";
            ddlStatus.Focus();
            return;
        }
        string SQL = "";
        string Where = "";

        if (ddlStatus.SelectedIndex > 0 && ddlStatus.SelectedValue == "U")
        {
            SQL = "select *, CASE WHEN ISNULL(vp.Amount,0) = 0 then InvoiceAmount ELSE Amount END As InvAmount,  CASE WHEN ISNULL(vp.Currency,'') = '' then InvoiceCurr ELSE vp.Currency END As InvCurr, pi.PVNo, (Select u.FirstName + ' ' + u.LastName from UserMaster u where u.LoginId =  pi.PaidBy) As PaidName, pi.PaidOn, pi.PaymentId,dbo.GET_Invoice_List(vp.RFPId) As InvoiceList FROM  VW_POSInvoiceRFPList vp with(nolock) LEFT OUTER JOIN POS_Invoice_Payment pi with(nolock) on vp.RFPId = pi.RFPId WHERE 1=1 AND PaymentStage = 2  ";
        }
        if (ddlStatus.SelectedIndex > 0 && ddlStatus.SelectedValue == "P")
        {
            SQL = "select vp.*, vp.Amount As InvAmount,  vp.Currency As InvCurr,dbo.GETInvoiceListFORPVNO(vp.PVNo) As InvoiceList FROM  VW_POS_PaymentVoucherDetails vp with(nolock)  WHERE 1=1 AND vp.PaymentStage = 2 ";
        }
        if (txtF_Vendor.Text.Trim() != "")
        {
            Where += " AND SupplierName LIKE '%" + txtF_Vendor.Text.Trim() + "%' ";
        }
      //  string StatusClause = "";
        //  string StatusClause1 = "";
        if (ddlStatus.SelectedIndex > 0)
        {
            Where += " AND  ( isnull(PaymentStatus,'P') ='" + ddlStatus.SelectedValue + "' ) ";
        }

        if (txtF_InvNo.Text.Trim() != "")
        {
            if (ddlStatus.SelectedIndex > 0 && ddlStatus.SelectedValue == "P")
            {
                Where += " AND ( vp.PVNo in ( Select pip.PVNo from POS_Invoice pi with(nolock) INNER JOIN POS_INVOICE_payment pip with(nolock)  on pi.InvoiceId = pip.InvoiceId where pi.InvNo = '" + txtF_InvNo.Text.Trim() + "' and pi.Status = 'P'))"; 
            }
            else
            {
                Where += " AND ( InvoiceId in ( Select InvoiceId from POS_Invoice with(nolock) where InvNo = '" + txtF_InvNo.Text.Trim() + "'))";
            }
          
        }


        //if (ddlStatus1.SelectedIndex > 0)
        //{
        //    if (ddlStatus1.SelectedValue == "P")
        //        StatusClause1 = " isnull(STATUS,'P') = 'A' AND BankConfirmedOn is NULL";
        //    else
        //        StatusClause1 = " isnull(STATUS,'P') = 'A' AND BankConfirmedOn is NOT NULL";
        //}
        //if (StatusClause != "")
        //    Where += " AND ( " + StatusClause + " ) ";
        //else if (StatusClause == "" && StatusClause1 == "")
        //    Where += "";
        //else if (StatusClause != "" || StatusClause1 != "")
        //    Where += " AND ( " + StatusClause + StatusClause1 + " ) ";
        if (ddl_Vessel.SelectedIndex > 0)
        {
            Where += " AND vp.VesselId ='" + ddl_Vessel.SelectedValue + "' ";
        }

        if (ddlStatus.SelectedValue == "U" && txtF_PVNo.Text.Trim() != "")
        {
            Where += " AND vp.RFPNo ='" + txtF_PVNo.Text.Trim() + "' ";
        }

        if (ddlStatus.SelectedValue == "P" && txtF_PVNo.Text.Trim() != "")
        {
            Where += " AND vp.PVNo ='" + txtF_PVNo.Text.Trim() + "' ";
        }

        if (ddlF_Owner.SelectedIndex > 0)
        {
            Where += " AND vp.POSOwnerId ='" + ddlF_Owner.SelectedValue + "' ";
        }

        if (txtF_D1.Text != "")
        {
            Where += " AND vp.PAIDON >='" + txtF_D1.Text + "' ";
        }

        if (txtF_D2.Text != "")
        {
            Where += " AND vp.PAIDON <='" + Convert.ToDateTime(txtF_D2.Text).AddDays(1).ToString("dd-MMM-yyyy") + "' ";
        }
        //if (ddlF_Vessel.SelectedIndex > 0)
        //{
        //    Where += " AND VesselId =" + ddlF_Vessel.SelectedValue + " ";
        //}
        DataTable dt = null;
        if (ddlStatus.SelectedIndex > 0 && ddlStatus.SelectedValue == "U")
        {
             dt = Common.Execute_Procedures_Select_ByQuery(SQL + Where + " Order By vp.RFPId Desc");
        }

        if (ddlStatus.SelectedIndex > 0 && ddlStatus.SelectedValue == "P")
        {
             dt = Common.Execute_Procedures_Select_ByQuery(SQL + Where + " Order By vp.PaidOn Desc ");
        }

            if (dt.Rows.Count > 0 )
        {
            if (ddlStatus.SelectedValue == "U")
            {
                RptMyInvoices.DataSource = dt;
                RptMyInvoices.DataBind();
                dvUnpaidStatus.Visible = true;
                dvPaid.Visible = false;
                rptPaidStatus.DataSource = null;
                rptPaidStatus.DataBind();
            }
            if (ddlStatus.SelectedValue == "P")
            {
                RptMyInvoices.DataSource = null;
                RptMyInvoices.DataBind();
                dvUnpaidStatus.Visible = false;
                dvPaid.Visible = true;
                rptPaidStatus.DataSource = dt;
                rptPaidStatus.DataBind();
            }
        }
        else
        {
            RptMyInvoices.DataSource = null;
            RptMyInvoices.DataBind();
            dvUnpaidStatus.Visible = false;
            dvPaid.Visible = false;
            rptPaidStatus.DataSource = null;
            rptPaidStatus.DataBind();
        }

      
    }

    protected DataTable GetData()
    {
        string SQL = "";
        string Where = "";

        SQL = "select *, CASE WHEN ISNULL(vp.Amount,0) = 0 then InvoiceAmount ELSE Amount END As InvAmount,  CASE WHEN ISNULL(vp.Currency,'') = '' then InvoiceCurr ELSE vp.Currency END As InvCurr, pi.PVNo, (Select u.FirstName + ' ' + u.LastName from UserMaster u where u.LoginId =  pi.PaidBy) As PaidName, pi.PaidOn, pi.PaymentId ,dbo.GET_Invoice_List(vp.RFPId) As InvoiceList FROM  VW_POSInvoiceRFPList vp with(nolock) LEFT OUTER JOIN POS_Invoice_Payment pi with(nolock) on vp.RFPId = pi.RFPId WHERE 1=1 AND PaymentStage = 2  ";

        if (txtF_Vendor.Text.Trim() != "")
        {
            Where += " AND SupplierName LIKE '%" + txtF_Vendor.Text.Trim() + "%' ";
        }
        //  string StatusClause = "";
        //  string StatusClause1 = "";
        if (ddlStatus.SelectedIndex > 0)
        {
            Where += " AND  ( isnull(PaymentStatus,'P') ='" + ddlStatus.SelectedValue + "' ) ";
        }

        if (txtF_InvNo.Text.Trim() != "")
        {
            Where += " AND ( InvoiceId in ( Select InvoiceId from POS_Invoice with(nolock) where InvNo = '" + txtF_InvNo.Text.Trim() + "'))";
        }

        if (ddl_Vessel.SelectedIndex > 0)
        {
            Where += " AND vp.VesselId ='" + ddl_Vessel.SelectedValue + "' ";
        }

        if (ddlStatus.SelectedValue == "U" && txtF_PVNo.Text.Trim() != "")
        {
            Where += " AND vp.RFPNo ='" + txtF_PVNo.Text.Trim() + "' ";
        }

        if (ddlStatus.SelectedValue == "P" && txtF_PVNo.Text.Trim() != "")
        {
            Where += " AND pi.PVNo ='" + txtF_PVNo.Text.Trim() + "' ";
        }

        if (ddlF_Owner.SelectedIndex > 0)
        {
            Where += " AND vp.POSOwnerId ='" + ddlF_Owner.SelectedValue + "' ";
        }

        if (txtF_D1.Text != "")
        {
            Where += " AND vp.PAIDON >='" + txtF_D1.Text + "' ";
        }

        if (txtF_D2.Text != "")
        {
            Where += " AND vp.PAIDON <='" + Convert.ToDateTime(txtF_D2.Text).AddDays(1).ToString("dd-MMM-yyyy") + "' ";
        }

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL + Where + " Order By vp.RFPId Desc");
        return dt;
    }

    protected void lbPVNo_Click(object sender, EventArgs e)
    {
        int _PaymentId = 0;
        //try
        //{
        //    _PaymentId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);            
        //}
        //catch
        //{
        //    _PaymentId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);          
        //}
        string pvno = "";
        try
        {
            pvno = ((LinkButton)sender).CommandArgument.ToString();
            _PaymentId =  Common.CastAsInt32(((LinkButton)sender).Attributes["paymentid"]);
        }
        catch
        {
            pvno = ((LinkButton)sender).CommandArgument.ToString();
            _PaymentId = Common.CastAsInt32(((LinkButton)sender).Attributes["paymentid"]);
        }
        //-------------------------
        if (pvno != "" && _PaymentId > 0)
        {
            dv_NewPV.Visible = true;
            PaymentId = _PaymentId;
            string Invoices = "";
            string SQL = "SELECT InvoiceId FROM POS_Invoice_Payment WHERE PVNo='" + pvno + "'";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
            if (dt.Rows.Count > 0)
            {
                lblpvno.Text = pvno.ToString();
                foreach (DataRow dr in dt.Rows)
                {
                    if (string.IsNullOrWhiteSpace(Invoices))
                    {
                        Invoices = dr["InvoiceId"].ToString();
                    }
                    else
                    {
                        Invoices = Invoices + "," + dr["InvoiceId"].ToString();
                    }
                }
                //btnAddInvoice.Visible = btn_Approve.Visible;
                // Invoices = dt.Rows[0]["LinkedInvoices"].ToString();
                ShowInvoiceDetails(Invoices);
                ShowPaymentDetails(PaymentId);
                btn_Payment.Visible = false;
                btnPrint.Visible = true;
                btnAddUpdatePaymentDoc.Visible = true;
            }
        }
    }

    protected void btnClose1_Click(object sender, EventArgs e)
    {
        dv_NewPV.Visible = false;
        clearPaymentDetails();
        btn_Search_Click(sender, e);
    }

    protected void clearPaymentDetails()
    {
        txtCreditActNo.Text = "";
        txtCreditActName.Text = "";
        txtChequeTTNo.Text = "";
        txtChequeTTDate.Text = "";
        txtChequeTTAmt.Text = "";
        txtBankName.Text = "";
        txtBankCharges.Text = "";
        RptInvoices.DataSource = null;
        RptInvoices.DataBind();
        
    }

    //protected void btnAskCancel_Click(object sender, EventArgs e)
    //{
    //    int _PaymentId = 0;
       
    //    try
    //    {
    //        _PaymentId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
           
    //    }
    //    catch
    //    {
    //        _PaymentId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
            
    //    }
    //    //-------------------------
    //    if (_PaymentId > 0)
    //    {

    //        string sql = "SELECT ISNULL(PVNO,'') FROM VW_PAYMENTVOUCHERS_001 WHERE PaymentId=" + _PaymentId.ToString();
    //        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
    //        if (dt.Rows.Count > 0)
    //        {
    //            string pvno = dt.Rows[0][0].ToString();
    //            if (pvno.Trim() == "") // PERMANANT DELETION
    //            {
                    
    //             Common.Execute_Procedures_Select_ByQuery("DELETE FROM DBO.POS_Invoice_Payment WHERE PAYMENTID=" + _PaymentId.ToString());
                   
    //                btn_Search_Click(sender, e);
    //            }
    //            else // virtual deletion
    //            {
    //                ViewState["_PaymentId"] = _PaymentId;
    //            }
    //        }
    //    }
    //}



    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            if (PaymentId > 0)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Edit", "PrintVoucherO(" + PaymentId + ");", true);
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Open", "window.open('PaymentVoucher.aspx?PaymentId=" + PaymentId + "&PaymentMode=O', 'asdf', '');", true);
            }
        }
       catch(Exception ex)
        {
            lblMsgPOP.Text = ex.Message.ToString();
        }
    }


    protected void txtTDSAmount_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Decimal totalPayAmount = 0;
            Decimal totalApprovalAmount = 0;
            Decimal totalTdsAmount = 0;
            foreach (RepeaterItem ri in RptInvoices.Items)
            {
                int invoiceId = Common.CastAsInt32(((HiddenField)ri.FindControl("hfdInvId")).Value);
                decimal ApprovalAmt = Common.CastAsDecimal(((HiddenField)ri.FindControl("hdnApprovlAmt")).Value);
                TextBox txtPayAmount = (TextBox)ri.FindControl("txtPayAmt");
                TextBox txttdsAmt = (TextBox)ri.FindControl("txtTDSAmount");
                if (!string.IsNullOrWhiteSpace(txttdsAmt.Text))
                {

                    txttdsAmt.Text = String.Format("{0:0.00}", txttdsAmt.Text);
                    //Common.Execute_Procedures_Select_ByQuery("Update [POS_Invoice] SET TDSAmount = " + Common.CastAsDecimal(txttdsAmt.Text) + " where InvoiceId= " + invoiceId);

                    totalTdsAmount = totalTdsAmount + Common.CastAsDecimal(txttdsAmt.Text);
                }

                if (!string.IsNullOrWhiteSpace(txtPayAmount.Text))
                {

                    txtPayAmount.Text = String.Format("{0:0.00}", txtPayAmount.Text);
                    //Common.Execute_Procedures_Select_ByQuery("Update [POS_Invoice] SET TDSAmount = " + Common.CastAsDecimal(txttdsAmt.Text) + " where InvoiceId= " + invoiceId);

                    totalPayAmount = totalPayAmount + Common.CastAsDecimal(txtPayAmount.Text);
                }
                totalApprovalAmount = totalApprovalAmount + ApprovalAmt;
            }
            lbltotal.Text = Math.Round(totalApprovalAmount - totalTdsAmount, 2).ToString();
            lblpayAmt.Text = Math.Round(totalPayAmount - totalTdsAmount, 2).ToString();
            txtChequeTTAmt.Text = lblpayAmt.Text;
        }
        catch (Exception ex)
        {
            lblMsgPOP.Text = "Unable to save record." + ex.Message + Common.getLastError();
        }
    }

    protected void txtPayAmt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Decimal totalPayAmount = 0;
            Decimal totalApprovalAmount = 0;
            Decimal totalTdsAmount = 0;
            foreach (RepeaterItem ri in RptInvoices.Items)
            {
                int invoiceId = Common.CastAsInt32(((HiddenField)ri.FindControl("hfdInvId")).Value);
                decimal ApprovalAmt = Common.CastAsDecimal(((HiddenField)ri.FindControl("hdnApprovlAmt")).Value);
                TextBox txtPayAmount = (TextBox)ri.FindControl("txtPayAmt");
                TextBox txttdsAmt = (TextBox)ri.FindControl("txtTDSAmount");
                if (!string.IsNullOrWhiteSpace(txttdsAmt.Text))
                {

                    txttdsAmt.Text = String.Format("{0:0.00}", txttdsAmt.Text);
                    //Common.Execute_Procedures_Select_ByQuery("Update [POS_Invoice] SET TDSAmount = " + Common.CastAsDecimal(txttdsAmt.Text) + " where InvoiceId= " + invoiceId);

                    totalTdsAmount = totalTdsAmount + Common.CastAsDecimal(txttdsAmt.Text);
                }

                if (!string.IsNullOrWhiteSpace(txtPayAmount.Text))
                {

                    txtPayAmount.Text = String.Format("{0:0.00}", txtPayAmount.Text);
                    //Common.Execute_Procedures_Select_ByQuery("Update [POS_Invoice] SET TDSAmount = " + Common.CastAsDecimal(txttdsAmt.Text) + " where InvoiceId= " + invoiceId);

                    totalPayAmount = totalPayAmount + Common.CastAsDecimal(txtPayAmount.Text);
                }
                totalApprovalAmount = totalApprovalAmount + ApprovalAmt;
            }
            lbltotal.Text = Math.Round(totalApprovalAmount - totalTdsAmount, 2).ToString();
            lblpayAmt.Text = Math.Round(totalPayAmount - totalTdsAmount, 2).ToString();
            txtChequeTTAmt.Text = lblpayAmt.Text;
        }
        catch (Exception ex)
        {
            lblMsgPOP.Text = "Unable to save record." + ex.Message + Common.getLastError();
        }
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
      try
        {
            
            DataTable dt = GetData();
            //Create a dummy GridView
            //GridView GridView1 = new GridView();
            GridView1.Visible = true;
            GridView1.AllowPaging = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            if (ddlStatus.SelectedValue == "P")
            {
                GridView1.Columns[11].Visible = true; 
                GridView1.Columns[12].Visible = true;
                GridView1.Columns[13].Visible = true;
            }
            
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=PaymentVoucherDetails.xls");
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
            GridView1.RenderControl(hw);
            //style to format numbers to string
            //string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            //Response.Write(style);
            Response.Output.Write(sw.ToString());
            GridView1.Visible = false;
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



    protected void btnMakePayment_Click(object sender, EventArgs e)
    {
        // check for same vendor and same currency
        //-----
        bool IsChecked = false;
         RFPIds = "";
        foreach (RepeaterItem ri in RptMyInvoices.Items)
        {
            CheckBox chk = (CheckBox)ri.FindControl("chkRFP");
            if (chk.Checked)
            {
                IsChecked = true;
                RFPIds += "," + chk.CssClass;
            }
        }

        if (!IsChecked)
        {
            ProjectCommon.ShowMessage("Please select an RFP Request to pay");
            return;
        }
        else
        {
            RFPIds = RFPIds.Substring(1);

            string sqlCheck = "SELECT distinct SupplierId FROM  VW_POSInvoiceRFPList WHERE [RFPId] IN(" + RFPIds + ")";
            DataTable dtVendor = Common.Execute_Procedures_Select_ByQuery(sqlCheck);

            if (dtVendor.Rows.Count != 1)
            {
                ProjectCommon.ShowMessage("Please select rfps for same vendor to pay.");
                return;
            }

            sqlCheck = "SELECT distinct Currency FROM  VW_POSInvoiceRFPList WHERE [RFPId] IN(" + RFPIds + ")";
            dtVendor = Common.Execute_Procedures_Select_ByQuery(sqlCheck);

            if (dtVendor.Rows.Count != 1)
            {
                ProjectCommon.ShowMessage("Please select rfps for same currency to pay.");
                return;
            }


            sqlCheck = "SELECT distinct COMPANY FROM VW_POSInvoiceRFPList where RFPId in (" + RFPIds + ")";
            dtVendor = Common.Execute_Procedures_Select_ByQuery(sqlCheck);

            if (dtVendor.Rows.Count != 1)
            {
                ProjectCommon.ShowMessage("Please select rfps for same owner to pay.");
                return;
            }

            //sqlCheck = "SELECT distinct isnull(ClsInvoice,0) FROM  vw_POS_Invoices_001 WHERE [InvoiceId] IN(" + RFPIds + ")";
            //dtVendor = Common.Execute_Procedures_Select_ByQuery(sqlCheck);

            //if (dtVendor.Rows.Count != 1)
            //{
            //    ProjectCommon.ShowMessage("Please select invoices in same category ( CLS Invoice / NON CLS Invoice ).");
            //    return;
            //}

            // Session.Add("InvoiceIds", InvIds);

            dv_NewPV.Visible = true;
           // PaymentId = _PaymentId;
            string Invoices = "";
            string SQL = "SELECT InvoiceId FROM vw_POS_Invoices_RFP with(nolock) WHERE RFPId in (" + RFPIds + ")";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
            if (dt.Rows.Count > 0)
            {
                //lblpvno.Text = dt.Rows[0]["PVNO"].ToString();
                foreach (DataRow dr in dt.Rows)
                {
                    if (string.IsNullOrWhiteSpace(Invoices))
                    {
                        Invoices = dr["InvoiceId"].ToString();
                    }
                    else
                    {
                        Invoices = Invoices +","+ dr["InvoiceId"].ToString();
                    }
                }

                //btnAddInvoice.Visible = btn_Approve.Visible;
                // Invoices = dt.Rows[0]["LinkedInvoices"].ToString();
                txtComments.Text = "";
                ShowInvoiceDetails(Invoices);
                btn_Payment.Visible = true;
              //  ShowPaymentDetails(PaymentId);
            }

        }
     }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStatus.SelectedValue == "U")
        {
            lblNo.Text = "RFP # :";
           
        }
        if (ddlStatus.SelectedValue == "P")
        {
            lblNo.Text = "PV # :";
        }
        txtF_PVNo.Text = "";
        RptMyInvoices.DataSource = null;
        RptMyInvoices.DataBind();
        dvUnpaidStatus.Visible = false;
        dvPaid.Visible = false;
        rptPaidStatus.DataSource = null;
        rptPaidStatus.DataBind();
        btn_Search_Click(sender, e);
    }

    protected void bindVesselNameddl(int flag)
    {
        string sql = "";
        if (flag == 0)
        {
           sql = "SELECT VesselNo,shipid + ' - ' + SHIPNAME AS SHIPNAME from VW_ACTIVEVESSELS  where VesselNo in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ")  ORDER BY SHIPNAME";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
            ddl_Vessel.DataSource = dt;
            ddl_Vessel.DataValueField = "VesselNo";
            ddl_Vessel.DataTextField = "SHIPNAME";
            ddl_Vessel.DataBind();
            ddl_Vessel.Items.Insert(0, new ListItem("< Select >", "0"));
        }
      else
        {
           if (ddlF_Owner.SelectedValue != "0")
            {
                sql = "SELECT VesselNo,shipid + ' - ' + SHIPNAME AS SHIPNAME from VW_ACTIVEVESSELS  where VesselNo in (Select VesselId from UserVesselRelation with(nolock) where  Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ") AND Company = '"+ ddlF_Owner.SelectedValue+"'  ORDER BY SHIPNAME";
                DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
                ddl_Vessel.DataSource = dt;
                ddl_Vessel.DataValueField = "VesselNo";
                ddl_Vessel.DataTextField = "SHIPNAME";
                ddl_Vessel.DataBind();
                ddl_Vessel.Items.Insert(0, new ListItem("< Select >", "0"));
            }
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (ddlStatus.SelectedValue == "P")
        {
            e.Row.Cells[11].Visible = true;
            e.Row.Cells[12].Visible = true;
            e.Row.Cells[13].Visible = true;
        }
        if (ddlStatus.SelectedValue == "U")
        {
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;
            e.Row.Cells[13].Visible = false;
        }
    }

    protected void ibSendMail_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgBtn = (ImageButton)sender;
        string pvno = imgBtn.CommandArgument;
        int PaymentId = Common.CastAsInt32(((ImageButton)sender).Attributes["PaymentId"]);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "abc", "window.open('SendMailforPaymentReceipt.aspx?pvno=" + pvno.ToString() + "&PaymentId="+ PaymentId.ToString()+ "');", true);
    }

    protected void btnAddUpdatePaymentDoc_Click(object sender, EventArgs e)
    {
        divPaymentAttachment.Visible = true;
        ShowAttachment();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (rptPaymentDocuments.Items.Count >= 1)
            {
                lblMsgPaymnetDoc.Text = "Maximum 1 documents allowed, 200KB each.";
                return;
            }
            //string FileName = "";
            //    byte[] ImageAttachment ;
            if (fu.HasFile)
            {
                if (fu.PostedFile.ContentLength > (1024 * 1024 * 0.2))
                {
                    lblMsgPaymnetDoc.Text = "File Size is Too big! Maximum Allowed is 200KB...";
                    fu.Focus();
                    return;
                }
                //else
                //{
                //    FileName = FU.FileName;
                //    // ImageAttachment = FU.FileBytes;
                //}

                string pvno = lblpvno.Text;
                string FileName = Path.GetFileName(fu.PostedFile.FileName);
                string fileContent = fu.PostedFile.ContentType;
                Stream fs = fu.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                byte[] bytes = br.ReadBytes((Int32)fs.Length);
                Common.Set_Procedures("[dbo].[Insert_PaymentDocuments]");
                Common.Set_ParameterLength(5);
                Common.Set_Parameters(
                    new MyParameter("@PVNo", pvno),
                    new MyParameter("@DocName", FileName),
                    new MyParameter("@Doc", bytes),
                    new MyParameter("@ContentType", fileContent),
                    new MyParameter("@Addedby", Convert.ToInt32(Session["loginid"].ToString()))
                    );
                DataSet ds = new DataSet();
                ds.Clear();
                Boolean res;
                res = Common.Execute_Procedures_IUD(ds);
                if (res)
                {
                    ProjectCommon.ShowMessage("Document saved Successfully.");
                    ShowAttachment();
                   // GetDocCount("SP", ddlvessel.SelectedValue, PRId);
                }
                else
                {
                    lblMsgPaymnetDoc.Text = "Unable to add Document.Error :" + Common.getLastError();
                }
            }
        }
        catch(Exception ex)
        {
            lblMsgPaymnetDoc.Text = "Unable to add Document.Error :" + Common.getLastError();
        }
    }

    public void ShowAttachment()
    {
        string sql = "";
        if (lblpvno.Text != "")
        {
            sql = "Select ID,PVNo,DocName,DocType,Doc from POS_Invoice_Payment_Document with(nolock) where PVNo = '" + lblpvno.Text + "'";
            DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
            rptPaymentDocuments.DataSource = DT;
            rptPaymentDocuments.DataBind();
        }
    }

    protected void btnPopupAttachment_Click(object sender, EventArgs e)
    {
        divPaymentAttachment.Visible = false;
    }

    protected void ImgDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int DocId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
            string sql = "";
            if (DocId > 0 && lblpvno.Text != "")
            {
                sql = "Delete from POS_Invoice_Payment_Document  WHERE PVNo ='" + lblpvno.Text + "' AND ID = " + DocId;
                DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
            }

            ShowAttachment();
        }
        catch (Exception ex)
        {
            ProjectCommon.ShowMessage(ex.Message.ToString());
        }

    }
    protected void imgClosePopup_Click(object sender, ImageClickEventArgs e)
    {
        divPaymentAttachment.Visible = false;
    }
    //protected void lbApEntries_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int NonPoId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
    //        if (NonPoId > 0)
    //        {
    //            dvNonPoEntry.Visible = true;
    //            BindDepartment();
    //            GetNonPoData(NonPoId);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ProjectCommon.ShowMessage(ex.Message.ToString());
    //    }
    //}
    //protected void GetNonPoData(int nonpoId)
    //{
    //    string SQL = "";
    //    SQL = "SELECT * FROM Vw_Pos_Invoice_NonPo I " +
    //          "WHERE  Status<>'Cancelled' AND NonPoId = " + nonpoId + " ";

    //    DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
    //    if (dt.Rows.Count > 0)
    //    {
    //        lblInvRefNo.Text = dt.Rows[0]["RefNo"].ToString();
    //        lblInvoiceNo.Text = dt.Rows[0]["InvNo"].ToString();
    //        lblInvAmount.Text = dt.Rows[0]["InvAmount"].ToString();
    //        lblInvCurrency.Text = dt.Rows[0]["Currency"].ToString();
    //        lblSupplierName.Text = dt.Rows[0]["Vendor"].ToString();
    //        lblVesselName.Text = dt.Rows[0]["ShipName"].ToString();
    //        hdnNonPoId.Value = dt.Rows[0]["NonPoId"].ToString();
    //        hdnInvoiceId.Value = dt.Rows[0]["InvoiceId"].ToString();
    //        lblNonPoId.Text = dt.Rows[0]["NonPoNum"].ToString();
    //    }
    //}

    //protected void btnNonPo_ClosePopup_Click(object sender, ImageClickEventArgs e)
    //{
    //    dvNonPoEntry.Visible = false;
    //    lblMsgApEntries.Text = "";
    //    ClearControls();
    //}

    //protected void ClearControls()
    //{
    //    lblInvRefNo.Text = "";
    //    lblInvoiceNo.Text = "";
    //    lblInvAmount.Text = "";
    //    lblInvCurrency.Text = "";
    //    lblSupplierName.Text = "";
    //    lblVesselName.Text = "";
    //    hdnNonPoId.Value = "0";
    //    hdnInvoiceId.Value = "0";
    //    lblNonPoId.Text = "";
    //    ddldepartment.SelectedIndex = 0;
    //    ddlAccount.SelectedIndex = 0;
    //}

    //protected void btnCloseApEntries_Click(object sender, EventArgs e)
    //{
    //    dvNonPoEntry.Visible = false;
    //    lblMsgApEntries.Text = "";
    //    ClearControls();
    //}

    //protected void btnSaveApEntries_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (hdnNonPoId.Value != "0")
    //        {
    //            int nonpoId = Common.CastAsInt32(hdnNonPoId.Value);
    //            int invoiceId = Common.CastAsInt32(hdnInvoiceId.Value);
    //            if (nonpoId > 0)
    //            {
    //                Common.Set_Procedures("Sp_InsertUpdateNonPoApEntries");
    //                Common.Set_ParameterLength(4);
    //                Common.Set_Parameters(
    //                    new MyParameter("@NonPoId", nonpoId),
    //                    new MyParameter("@InvoiceId", invoiceId),
    //                    new MyParameter("@AccountId", ddlAccount.SelectedValue.Trim()),
    //                    new MyParameter("@Remarks", txtNonPoRemarks.Text.Trim())
    //                    );

    //                DataSet ds = new DataSet();
    //                ds.Clear();
    //                Boolean res;
    //                res = Common.Execute_Procedures_IUD(ds);
    //                if (res)
    //                {

    //                    lblMsgApEntries.Text = "Record Successfully Saved.";
    //                    dvNonPoEntry.Visible = false;
    //                    // ShowMyInvoices(sender, e);
    //                    string Invoices = "";
    //                    string SQL = "SELECT InvoiceId FROM vw_POS_Invoices_RFP with(nolock) WHERE RFPId in (" + RFPIds + ")";
    //                    DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
    //                    if (dt.Rows.Count > 0)
    //                    {
    //                        //lblpvno.Text = dt.Rows[0]["PVNO"].ToString();
    //                        foreach (DataRow dr in dt.Rows)
    //                        {
    //                            if (string.IsNullOrWhiteSpace(Invoices))
    //                            {
    //                                Invoices = dr["InvoiceId"].ToString();
    //                            }
    //                            else
    //                            {
    //                                Invoices = Invoices + "," + dr["InvoiceId"].ToString();
    //                            }
    //                        }


    //                        string SQL1 = "SELECT * FROM vw_POS_Invoices_RFP I with(nolock) WHERE InvoiceId IN ( 0" + Invoices + " )";
    //                        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(SQL1);
    //                        RptInvoices.DataSource = dt1;
    //                        RptInvoices.DataBind();

    //                    }
    //                }
    //                else
    //                {
    //                    lblMsgApEntries.Text = "Unable to save record." + Common.getLastError();
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblMsgApEntries.Text = "Unable to save record." + ex.Message.ToString();
    //    }
    //}

    //protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindAccount();
    //}
    //public void BindDepartment()
    //{
    //    //string sql = "select dept,deptname from  VW_sql_tblSMDPRDept";
    //    string sql = "Select mid.MidCatID,mid.MidCat from tblAccountsMid mid with(nolock) Inner join sql_tblSMDPRAccounts acc with(nolock) on mid.MidCatID = acc.MidCatID \r\nwhere  acc.Active = 'Y'  Group by mid.MidCatID,mid.MidCat  order by Midcat asc";
    //    Common.Set_Procedures("ExecQuery");
    //    Common.Set_ParameterLength(1);
    //    Common.Set_Parameters(new MyParameter("@Query", sql));
    //    DataSet dsPrType = new DataSet();
    //    dsPrType.Clear();
    //    dsPrType = Common.Execute_Procedures_Select();

    //    ddldepartment.DataSource = dsPrType;
    //    //ddldepartment.DataTextField = "deptname";
    //    //ddldepartment.DataValueField = "dept";
    //    ddldepartment.DataTextField = "MidCat";
    //    ddldepartment.DataValueField = "MidCatID";
    //    ddldepartment.DataBind();
    //    ddldepartment.Items.Insert(0, new ListItem("<Select>", "0"));
    //    ddldepartment.SelectedIndex = 0;
    //}

    //public void BindAccount()
    //{

    //    string sql = "select * from (select (select convert(varchar, AccountNumber)+'-'+AccountName from VW_sql_tblSMDPRAccounts PA where DA.AccountID=PA.AccountID ) AccountNumber  ,(select AccountName from  VW_sql_tblSMDPRAccounts PA where DA.AccountID=PA.AccountID ) AccountName  ,AccountID from VW_sql_tblSMDPRAccounts DA where DA.MidCatID='" + ddldepartment.SelectedValue + "') dd where AccountNumber is not null";
    //    Common.Set_Procedures("ExecQuery");
    //    Common.Set_ParameterLength(1);
    //    Common.Set_Parameters(new MyParameter("@Query", sql));
    //    DataSet dsPrType = new DataSet();
    //    dsPrType.Clear();
    //    dsPrType = Common.Execute_Procedures_Select();

    //    ddlAccount.DataSource = dsPrType;
    //    ddlAccount.DataTextField = "AccountNumber";
    //    ddlAccount.DataValueField = "AccountID";
    //    ddlAccount.DataBind();
    //    ddlAccount.Items.Insert(0, new ListItem("<Select>", "0"));
    //    ddlAccount.SelectedIndex = 0;


    //}


    protected void ddlF_Owner_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindVesselNameddl(1);
        }
        catch(Exception ex)
        {

        }
    }
}