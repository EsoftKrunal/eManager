using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Modules_Purchase_Invoice_BankDetails : System.Web.UI.Page
{
    private int PageSize = 50;

    private string strUSD = "USD";
    public string VesselNo
    {
        get { return ViewState["VesselNo"].ToString(); }
        set { ViewState["VesselNo"] = value; }
    }

    public string Invoices
    {
        get { return ViewState["Invoices"].ToString(); }
        set { ViewState["Invoices"] = value; }
    }

    //public string VesselName
    //{
    //    get { return ViewState["VesselName"].ToString(); }
    //    set { ViewState["VesselName"] = value; }
    //}

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

    public decimal XchangeRate
    {
        get { return Convert.ToDecimal(ViewState["XchangeRate"]); }
        set { ViewState["XchangeRate"] = value; }
    }

    public int GainLossAccountId
    {
        get { return Convert.ToInt32(ViewState["GainLossAccountId"]); }
        set { ViewState["GainLossAccountId"] = value; }
    }

    public int BankChargesAccountId
    {
        get { return Convert.ToInt32(ViewState["BankChargesAccountId"]); }
        set { ViewState["BankChargesAccountId"] = value; }
    }

    public bool IsAdvPayment
    {
        get { return Convert.ToBoolean(ViewState["IsAdvPayment"]); }
        set { ViewState["IsAdvPayment"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
      try
        {
            //---------------------------------------
            ProjectCommon.SessionCheck();
            //---------------------------------------
            lblMsgMain.Text = "";
            lblmsg1.Text = "";
            if (!Page.IsPostBack)
            {
                UserId = Common.CastAsInt32(Session["loginid"]);
                DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT Payment FROM POS_Invoice_mgmt where USERID=" + UserId);

                if ((dt.Rows.Count > 0 && dt.Rows[0]["Payment"].ToString() == "True") || UserId == 1)
                {
                    bindOwnerddl();
                    BindPaidInddl();
                    ShowPaymentDetailsforBank(0);
                }
                else
                {
                    Response.Redirect("~/NoPermission.aspx");
                }
            }
        }
        catch(Exception ex)
        {
            lblMsgMain.Text = "Error On page load - " + ex.Message.ToString();
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

    protected void BindPaidInddl()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("Select PaymentTypeId, Currency from PaymentType with(nolock) where StatusId = 'A' ");
        ddlPaidIn.DataSource = dt;
        ddlPaidIn.DataValueField = "PaymentTypeId";
        ddlPaidIn.DataTextField = "Currency";
        ddlPaidIn.DataBind();
        ddlPaidIn.Items.Insert(0, new ListItem("All", "0"));
    }

    protected void btn_Search_Click(object sender, EventArgs e)
    {
        ShowPaymentDetailsforBank(0);
    }

    protected void ShowPaymentDetailsforBank(int getPageNo)
    {
        string SQL = "";
        string Where = "";

        // SQL = "select * FROM VW_PAYMENTVOUCHERS_001 WHERE 1=1 AND PaymentStage = 2 and PaymentStatus = 'P' ";

        SQL = "Select *, Case when IsAdvPayment <> 1 then (Select SUM(Cast(AE.USDTotal as numeric(18,2))) As USDTotal from POS_Invoice_Payment pip with(nolock) inner join POS_Invoice_Payment_PO pipo with(nolock) on pip.InvoiceId = pipo.InvoiceId inner join tblApEntries AE with(nolock) on pipo.BidId = AE.bidid where pip.PaymentId = a.PaymentId and AE.ScreenName = 'INV' ) ELSE 0 END As PVAmountUSD from (select ROW_NUMBER() OVER(Partition by PVNO Order by paymentId Desc) As RowId,paymentId,PTYPE, SupplierId, PVNO, PaymentType,TravId,SupplierName,OwnerName ,CONVERT(varchar, PaidOn, 23) As PaidOn,POSOwnerId, PaidBy, PaidById,CURRENCY, Status , PaymentCurr, PaymentStatus, RFPApprovedBy, BankName,ChequeTTAmount As Amount,ChequeTTDt,ChequeTTNo,CreditActNo,CreditActName, VesselName,BankConfirmedOn,BankCharges,BankAmountLC, ISNULL(IsAdvPayment,0) As IsAdvPayment from VW_PAYMENTVOUCHERS_001 WHERE 1=1 AND PaymentStage = 2 and PaymentStatus = 'P' ";

        if (txtF_Vendor.Text.Trim() != "")
        {
            Where += " AND SupplierName LIKE '%" + txtF_Vendor.Text.Trim() + "%' ";
        }
        if (txtF_PVNo.Text.Trim() != "")
        {
            Where += " AND PVNo ='" + txtF_PVNo.Text.Trim() + "' ";
        }
        // string StatusClause = "";
        string StatusClause1 = "";
        if (ddlStatus1.SelectedIndex > 0)
        {
            if (ddlStatus1.SelectedValue == "P")
                StatusClause1 = " isnull(STATUS,'P') = 'A' AND BankConfirmedOn is NULL";
            else
                StatusClause1 = " isnull(STATUS,'P') = 'A' AND BankConfirmedOn is NOT NULL";
        }
        if (StatusClause1 != "")
        {
            Where += " AND ( " + StatusClause1 + " ) ";
        }
        if (ddlF_Owner.SelectedIndex > 0)
        {
            Where += " AND POSOwnerId ='" + ddlF_Owner.SelectedValue + "' ";
        }
        if (txtF_D1.Text != "")
        {
            Where += " AND PAIDON >='" + txtF_D1.Text + "' ";
        }
        if (txtF_D2.Text != "")
        {
            Where += " AND PAIDON <='" + Convert.ToDateTime(txtF_D2.Text).AddDays(1).ToString("dd-MMM-yyyy") + "' ";
        }
        if (ddlPaidIn.SelectedValue != "0")
        {
            Where += " AND PaymentType = '" + ddlPaidIn.SelectedValue + "' ";
        }
        //if (ddlF_Vessel.SelectedIndex > 0)
        //{
        //    Where += " AND VesselId =" + ddlF_Vessel.SelectedValue + " ";
        //}

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL + Where + "  ) a where RowId = 1 Order By paymentId Desc ");
        if (dt.Rows.Count > 0)
        {
            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = dt.DefaultView;
            pds.AllowPaging = true;
            pds.PageSize = 50;

            Session["CPNo"] = getPageNo;

            if (getPageNo > (pds.PageCount - 1))
                getPageNo = pds.PageCount - 1;
            if (getPageNo < 0)
                getPageNo = 0;

            pds.CurrentPageIndex = getPageNo;

            if (dt.Rows.Count > 0)
            {
                lblPaymentCount.Text = dt.Rows.Count.ToString();
                RptMyInvoices.DataSource = pds;
                RptMyInvoices.DataBind();
            }
            else
            {
                RptMyInvoices.DataSource = null;
                RptMyInvoices.DataBind();
            }


            //enable & disabled previous and Next button
            if (pds.IsLastPage)
                lbNext.Enabled = false;
            else
                lbNext.Enabled = true;

            if (pds.IsFirstPage)
                lbPrevious.Enabled = false;
            else
                lbPrevious.Enabled = true;

            // Step 3: Bind PagedDataSource to Repeater and set LinkButtons' behavior
            RptMyInvoices.DataSource = pds;
            RptMyInvoices.DataBind();
            //if (pds.IsLastPage)
            //    lbNext.Enabled = false;
            //else
            //    lbNext.Enabled = true;

            //if (pds.IsFirstPage)
            //    lbPrevious.Enabled = false;
            //else
            //    lbPrevious.Enabled = true;
            //int recordCount = Convert.ToInt32(dtSpareDetails.Rows.Count);
            //this.PopulatePager(recordCount, pageIndex);
        }
        else
        {
            RptMyInvoices.DataSource = null;
            RptMyInvoices.DataBind();
        }
       
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtF_InvNo.Text = "";
        txtF_PVNo.Text = "";
        txtF_Vendor.Text = "";

        ddlF_Owner.SelectedIndex = 0;
    }

    protected void imgUpdateAmountOpenPop_OnClick(object sender, ImageClickEventArgs e)
    {
       try
        {
            dvUpdate.Visible = true;
            XchangeRate = 0;
            ImageButton btn = (ImageButton)sender;
            HiddenField hfBankCharges = (HiddenField)btn.Parent.FindControl("hfBankCharges");
            HiddenField hfBankAmount = (HiddenField)btn.Parent.FindControl("hfBankAmount");
            HiddenField hfRecordType = (HiddenField)btn.Parent.FindControl("hfRecordType");
            HiddenField hdnPaymentId = (HiddenField)btn.Parent.FindControl("hdnPaymentId");
            hfhfRecordType_popup.Value = hfRecordType.Value;
            PaymentId = Convert.ToInt32(hdnPaymentId.Value);
            IsAdvPayment = false;
            BindRptAmount(hfRecordType.Value, PaymentId);
            txtBankChargesPopup.Text = "";
            lblDebitAmount.Text = "0.00";
            BankConfirmedOnPopup.Text = "";
            txtXchangeRate.Text = "0.00";
            txtBankChargesPopup.Enabled = false;
            txtBankAmount.Enabled = false;
            if (!string.IsNullOrEmpty(hfBankCharges.Value) && Convert.ToDecimal(hfBankCharges.Value) > 0)
            {
                txtBankChargesPopup.Text = hfBankCharges.Value;
                if (!string.IsNullOrEmpty(txtBankChargesPopup.Text) && Convert.ToDecimal(txtBankChargesPopup.Text) > 0)
                {
                    txtBankChargesPopup_TextChanged(sender, e);
                }
            }
            else
            {
                txtBankChargesPopup.Text = "";
            }

            //txtCreditAccountCode.Text = "";
            //txtDebitAccountCode.Text = "";
            txtBankAmount.Text = "";
            txtGainLossAmt.Text = "0.00";
            //btnOpenExport.Visible = false;
            btnUpdateAmount.Visible = true;
            if ((lblinvcurr.Text.Trim().ToUpper() != lblpaycurr.Text.Trim().ToUpper()) && !IsAdvPayment)
            {
                dvExchangeGainLoss.Visible = true;
            }
            else
            {
                dvExchangeGainLoss.Visible = false;
            }
            if (lblLCInvCurr.Text.Trim().ToUpper() == strUSD)
            {
                txtXchangeRate.ReadOnly = true;
            }
            else
            {
                txtXchangeRate.ReadOnly = false;
            }
        }
        catch(Exception ex)
        {
            lblmsg1.Text = "Error - Open Bank charges screen - " + ex.Message.ToString();
        }
    }
    protected void btnCloseUpdateAmountPop_OnClick(object sender, EventArgs e)
    {
        try
        {
            RefreshGrid();
        }
       catch(Exception ex)
        {
            lblmsg1.Text = "Error - click on Close button " + ex.Message.ToString();
        }
    }
    protected void RefreshGrid()
    {
        dvUpdate.Visible = false;
        PaymentId = 0;
        // btn_Search_Click(sender, e);
        Session["CPNo"] = (int.Parse(Session["CPNo"].ToString())).ToString();
        ShowPaymentDetailsforBank(int.Parse(Session["CPNo"].ToString()));
    }
    public void BindRptAmount(string RecordType, int PaymentId)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select top 1 * from VW_PAYMENTVOUCHERS_001 where PTYPE='" + RecordType + "' and PaymentId='" + PaymentId + "'");
        if (dt.Rows.Count > 0)
        {
            lblPVNO1.Text = dt.Rows[0]["PVNO"].ToString();
            lblVendorName.Text = dt.Rows[0]["SupplierName"].ToString();
            lblVendorCode.Text = dt.Rows[0]["Travid"].ToString();
            lblOwner.Text = dt.Rows[0]["OwnerName"].ToString();
            lblOwnerCode.Text = dt.Rows[0]["POSOWNERID"].ToString();
            //lblpaycurr.Text = (dt.Rows[0]["PaymentType"].ToString() == "U") ? "USD" : ((dt.Rows[0]["PaymentType"].ToString() == "S") ? "SGD" : "INR");
            lblpaycurr.Text = dt.Rows[0]["PaymentCurr"].ToString();
           // lblpaycurr1.Text = lblpaycurr.Text;
           // lblpaycurr2.Text = lblpaycurr.Text;
           // lblpaycurr3.Text = lblpaycurr.Text;
            lblGainLossCurr.Text = lblpaycurr.Text;
            lblBankChargesCurr.Text = lblpaycurr.Text;
            lblLCInvCurr.Text = lblpaycurr.Text;

        }

        string SQL = "";
        SQL = " SELECT Distinct I.INVVesselCode,P.InvoiceId as TableId,I.InvNo as InvoiceNo,I.InvDate as OnDate,I.ApprovalAmount as Amount, CASE WHEN ISNULL(I.IsAdvPayment,0) = 0 then Cast(ISNULL(AE.USDTotal,0) as numeric(18,2)) ELSE 0 END As PVAmountUSD,i.Currency,p.PaymentId, ISNULL(I.IsAdvPayment,0) As  IsAdvPayment " +
            " FROM POS_Invoice_Payment_Invoices P with(nolock) " +
            " INNER JOIN POS_Invoice I with(nolock) on P.InvoiceId=I.InvoiceId " +
            " INNER JOIN POS_Invoice_Payment pi with(nolock) on p.PaymentId = pi.PaymentId " +
            " LEFT OUTER JOIN POS_Invoice_Payment_PO pipo with(nolock) on p.InvoiceId = pipo.InvoiceId " +
            " LEFT OUTER JOIN (Select USDTotal,bidid from tblApEntries AE with(nolock) where AE.ScreenName = 'INV' ) As AE  on pipo.BidId = AE.bidid " +
            " WHERE pi.PVNo = (Select PVNO from POS_Invoice_Payment with(nolock) " +
            " WHERE PaymentId = " + PaymentId + ") ";
        dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        if (dt.Rows.Count > 0)
        {
            lblinvcurr.Text = dt.Rows[0]["Currency"].ToString();
            lblVesselCode.Text = dt.Rows[0]["INVVesselCode"].ToString();
        }

        if (lblVesselCode.Text != "")
        {
            DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("select REPLACE(STR(VESSELId,4),' ' ,'0') As VESSELId, VesselName   from Vessel with(nolock) WHERE VesselCode='" + lblVesselCode.Text + "'");
            if (dt1.Rows.Count > 0)
            {
                //txtCVesselNo.Text = dt.Rows[0][0].ToString();
                VesselNo = dt1.Rows[0]["VESSELId"].ToString();
                lblVesselName.Text = dt1.Rows[0]["VesselName"].ToString();
                // up11.Update();
                //up12.Update();
            }
        }

        rptAmount.DataSource = dt;
        rptAmount.DataBind();
        IsAdvPayment = false;
        if (dt.Rows.Count > 0)
        {
            foreach(DataRow dr in dt.Rows)
            {
                if (Convert.ToBoolean(dr["IsAdvPayment"]))
                {
                    IsAdvPayment = true;
                    break;
                }
            }
        }
        lblTotalInvAmount.Text = dt.Compute("sum(Amount)", "").ToString();
        lblTotalBankAmount.Text = dt.Compute("sum(PVAmountUSD)", "").ToString();

        //ddlVessels.DataTextField = "INVVesselCode";
        //ddlVessels.DataValueField = "INVVesselCode";
        //ddlVessels.DataSource = dt;
        //ddlVessels.DataBind();
        //ddlVessels_OnSelectedIndexChanged(new object(), new EventArgs());

        string sqlAccount = "Select (Select AccountNumber from sql_tblSMDPRAccounts with(nolock) where  AccountName = 'Bank Charges' ) As BankChargesAccount, (Select AccountNumber from sql_tblSMDPRAccounts with(nolock) where  AccountName = 'Exchange Gain/Loss' ) As GainLossAccount, (Select AccountId from sql_tblSMDPRAccounts with(nolock) where  AccountName = 'Bank Charges' ) As BankChargesAccountId, (Select AccountId from sql_tblSMDPRAccounts with(nolock) where  AccountName = 'Exchange Gain/Loss' ) As GainLossAccountId";
        DataTable dtAccount = Common.Execute_Procedures_Select_ByQuery(sqlAccount);
        if (dtAccount.Rows.Count > 0)
        {
          if (! string.IsNullOrEmpty(dtAccount.Rows[0]["GainLossAccount"].ToString()))
            {
                txtGainLossAccount.Text = dtAccount.Rows[0]["GainLossAccount"].ToString();
                txtGainLossAccountName.Text = "Exchange Gain/Loss";
            }
            if (!string.IsNullOrEmpty(dtAccount.Rows[0]["BankChargesAccount"].ToString()))
            {
                txtDebitAccountCode.Text = dtAccount.Rows[0]["BankChargesAccount"].ToString();
                txtDebitAccountName.Text = "Bank Charges";
            }
            if (!string.IsNullOrEmpty(dtAccount.Rows[0]["GainLossAccountId"].ToString()))
            {
                GainLossAccountId = Convert.ToInt32(dtAccount.Rows[0]["GainLossAccountId"].ToString());
            }
            if (!string.IsNullOrEmpty(dtAccount.Rows[0]["BankChargesAccountId"].ToString()))
            {
                BankChargesAccountId = Convert.ToInt32(dtAccount.Rows[0]["BankChargesAccountId"].ToString());
            }
        }

    }
    //protected void txtBankChargesPopup_OnTextChanged(object sender, EventArgs e)
    //{
    //  //  lblCreditAmount.Text = txtBankChargesPopup.Text;
    //    lblDebitAmount.Text = txtBankChargesPopup.Text;

    //}
   
    //protected void btnAskCancel_Click(object sender, EventArgs e)
    //{
    //    int _PaymentId = 0;
    //    string _VoucherType = "";
    //    try
    //    {
    //        _PaymentId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
    //        _VoucherType = ((ImageButton)sender).Attributes["VoucherType"];
    //    }
    //    catch
    //    {
    //        _PaymentId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
    //        _VoucherType = ((LinkButton)sender).Attributes["VoucherType"];
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
    //                if (_VoucherType == "N")
    //                {
    //                    Common.Execute_Procedures_Select_ByQuery("DELETE FROM DBO.POS_Payment WHERE PAYMENTID=" + _PaymentId.ToString());
    //                }
    //                else
    //                {
    //                    Common.Execute_Procedures_Select_ByQuery("DELETE FROM DBO.POS_Invoice_Payment WHERE PAYMENTID=" + _PaymentId.ToString());
    //                }
    //                btn_Search_Click(sender, e);
    //            }
    //            else // virtual deletion
    //            {
    //                ViewState["_PaymentId"] = _PaymentId;
    //                ViewState["_VoucherType"] = _VoucherType;

    //                //dvCancel.Visible = true;
    //                //lblCVno.Text = pvno;
    //            }
    //        }
    //    }
    //}
    protected void btnUpdateAmount_OnClick(object sender, EventArgs e)
    {
        try
        {
            lblmsg1.Text = "";
            btnUpdateAmount.Visible = false;
            if (BankConfirmedOnPopup.Text.Trim() == "")
            {
                lblmsg1.Text = "Please enter bank confirmation date.";
                BankConfirmedOnPopup.Focus();
                return;
            }
            if (txtXchangeRate.Text == "0.00")
            {
                lblmsg1.Text = "For Selected Bank Transation Date, Xchange rate is zero. Please check with system admin.";
                return;
            }
            if (dvExchangeGainLoss.Visible)
            {
                if (txtGainLossAmt.Text.Trim() == "")
                {
                    lblmsg1.Text = "Please enter Exchange Gain/Loss Amount. If no Amount you can fill zero.";
                    txtGainLossAmt.Focus();
                    return;
                }

                if (txtGainLossAccount.Text.Trim().Length != 4)
                {
                    lblmsg1.Text = "Please enter valid account code for Exchange Gain/Loss account.";
                    txtGainLossAccount.Focus();
                    return;
                }
            }

            else if (txtBankChargesPopup.Text.Trim() == "")
            {
                lblmsg1.Text = "Please enter bank charges. If no charges you can fill zero.";
                txtBankChargesPopup.Focus();
                return;
            }
            else if (Common.CastAsInt32(txtBankChargesPopup.Text.Trim()) != 0)
            {
                //if (txtCreditAccountCode.Text.Trim().Length != 4)
                //{
                //    lblmsg1.Text = "Please enter valid account code for credit account.";
                //    txtCreditAccountCode.Focus();
                //    return;
                //}
                if (txtDebitAccountCode.Text.Trim().Length != 4)
                {
                    lblmsg1.Text = "Please enter valid account code for debit account.";
                    txtDebitAccountCode.Focus();
                    return;
                }
            }
            //Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.POS_Invoice_Payment SET BankCharges=" + Common.CastAsDecimal(txtBankChargesPopup.Text.Trim()) + " ,BankConfirmedOn='" + BankConfirmedOnPopup.Text.Trim() + "',BankChargesPayableTo='" + ddlVessels.SelectedValue + "',CreditAccountCode='" + txtCreditAccountCode.Text + txtCVesselNo.Text + "',DebitAccountCode='" + txtDebitAccountCode.Text + txtDVesselNo.Text + "' WHERE PAYMENTID=" + PaymentId);
           // btnUpdateAmount.Visible = false;
            Invoices = "";
            foreach (RepeaterItem itm in rptAmount.Items)
            {
                //TextBox txtAmount = (TextBox)itm.FindControl("txtAmount");
                HiddenField hfdtableid = (HiddenField)itm.FindControl("hfdtableid");
                int TableId = Common.CastAsInt32(hfdtableid.Value);
                HiddenField hdnPaymentId = (HiddenField)itm.FindControl("hdnPaymentId");
                int PaymentId = Common.CastAsInt32(hdnPaymentId.Value);
                Label lblInvoice = (Label)itm.FindControl("lblInvoiceNo");
                if (! string.IsNullOrEmpty(lblInvoice.Text.Trim()))
                {
                    if (string.IsNullOrEmpty(Invoices))
                    {
                        Invoices = lblInvoice.Text.Trim();
                    }
                    else
                    {
                        Invoices = Invoices +";"+ lblInvoice.Text.Trim();
                    }
                }
                if (dvExchangeGainLoss.Visible)
                {
                    Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.POS_Invoice_Payment SET " +
                        "  BankCharges=" + Common.CastAsDecimal(lblDebitAmount.Text.Trim()) + " " +
                        " ,BankConfirmedOn='" + BankConfirmedOnPopup.Text.Trim() + "'" +
                        " ,BankChargesPayableTo='" + lblVesselCode.Text.Trim() + "'" +
                        " ,DebitAccountCode='" + txtDebitAccountCode.Text + VesselNo + "'" +
                        ", ExchangeGainLossAccountcode='" + txtGainLossAccount.Text + VesselNo + "'" +
                        ", ExchangeGainLossAmount = " + Common.CastAsDecimal(lblGainLossDiffCharges.Text.Trim()) + "" +
                        ", BankAmountLC = " + Common.CastAsDecimal(txtBankAmount.Text.Trim()) + "" +
                        ", BankAmountUSD = " + Common.CastAsDecimal(txtGainLossAmt.Text.Trim()) + " " +
                        ", BankChargesLC = " + Common.CastAsDecimal(txtBankChargesPopup.Text.Trim()) + " " +
                         ", ExchangeRate = " + Common.CastAsDecimal(txtXchangeRate.Text.Trim()) + " " +
                        " WHERE PAYMENTID=" + PaymentId + " AND InvoiceId = " + TableId);
                }
               else
                {
                    Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.POS_Invoice_Payment SET " +
                        " BankCharges=" + Common.CastAsDecimal(lblDebitAmount.Text.Trim()) + " " +
                        ", BankConfirmedOn='" + BankConfirmedOnPopup.Text.Trim() + "'" +
                        ", BankChargesPayableTo='" + lblVesselCode.Text.Trim() + "'" +
                        ", DebitAccountCode='" + txtDebitAccountCode.Text + VesselNo + "'" +
                        ", BankChargesLC = " + Common.CastAsDecimal(txtBankChargesPopup.Text.Trim()) + " " +
                        ", BankAmountLC = " + Common.CastAsDecimal(lblTotalInvAmount.Text.Trim()) + " " +
                        ", ExchangeRate = " + Common.CastAsDecimal(txtXchangeRate.Text.Trim()) + " " +
                        " WHERE PAYMENTID=" + PaymentId + " AND InvoiceId = " + TableId) ;
                }
                //Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.POS_Invoice_Payment_Invoices SET BankAmount=" + Common.CastAsDecimal(txtAmount.Text.Trim()) + " WHERE PAYMENTID=" + PaymentId + " AND INVOICEID=" + TableId);

            }
            bool saveExchangeGainLoss = false;
            bool SaveBankCharges = false;
            if (dvExchangeGainLoss.Visible)
            {
                if (Convert.ToDecimal(lblGainLossDiffCharges.Text.Trim()) != 0)
                {
                    AddGLEntry(1, lblVesselCode.Text.Trim(), lblPVNO1.Text.Trim(), GainLossAccountId, Convert.ToDecimal(lblGainLossDiffCharges.Text.Trim()), Invoices);
                    saveExchangeGainLoss = true;
                }
            }
            if (Convert.ToDecimal(lblDebitAmount.Text.Trim()) > 0)
            {
                AddGLEntry(2, lblVesselCode.Text.Trim(), lblPVNO1.Text.Trim(), BankChargesAccountId, Convert.ToDecimal(lblDebitAmount.Text.Trim()), Invoices);
                SaveBankCharges = true;
            }
            if (saveExchangeGainLoss & SaveBankCharges )
            {
                dvUpdate.Visible = false;
                // lblmsg1.Text = "Exchange Gain/Loss and Bank Charges updated successfully.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alet", "alert('Exchange Gain/Loss and Bank Charges updated successfully.');", true);
            }
            else if (!saveExchangeGainLoss & SaveBankCharges)
            {
                dvUpdate.Visible = false;
                // lblmsg1.Text = "Bank Charges updated successfully.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alet", "alert('Bank Charges updated successfully.');", true);
            }
            else if (saveExchangeGainLoss & !SaveBankCharges)
            {
                // lblmsg1.Text = "Exchange Gain/Loss updated successfully.";
                dvUpdate.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alet", "alert('Exchange Gain/Loss updated successfully.');", true);
                
            }
            btnCloseUpdateAmountPop_OnClick(sender, e);
           // RefreshGrid();
            // btnOpenExport.Visible = true;

            //lblmsg1.Text = "";
        }
        catch (Exception ex)
        {
            lblmsg1.Text = ex.Message;
            btnUpdateAmount.Visible = true;
          
        }

    }
    //protected void btnOpenExport_OnClick(object sender, EventArgs e)
    //{
    //    DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("SELECT CONVERT(BIT,ISNULL(EXPORTED,0)) AS EXPORTED FROM DBO.POS_Invoice_Payment WHERE PAYMENTID=" + PaymentId);
    //    if (dt1.Rows.Count > 0)
    //    {
    //        if (dt1.Rows[0][0].ToString() == "False") // Not Exported
    //        {
    //            //--------------------------
    //            frm1.Attributes.Add("src", "ExportPopUp.aspx?PaymentId=" + PaymentId);
    //            dvExport.Visible = true;
    //        }
    //    }

    //}

    protected void lnkbuttonNext_Click(object sender, EventArgs e)
    {
        Session["CPNo"] = (int.Parse(Session["CPNo"].ToString()) + 1).ToString();
        ShowPaymentDetailsforBank(int.Parse(Session["CPNo"].ToString()));
    }
    protected void lnkbuttonPrev_Click(object sender, EventArgs e)
    {
        Session["CPNo"] = (int.Parse(Session["CPNo"].ToString()) - 1).ToString();
        ShowPaymentDetailsforBank(int.Parse(Session["CPNo"].ToString()));
    }

    protected void txtGainLossAccount_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtGainLossAccount.Text.Length == 4)
            {
                string sql = "Select AccountName,AccountId from sql_tblSMDPRAccounts with(nolock) where  AccountNumber = " + txtGainLossAccount.Text.Trim() + " AND Active = 'Y' ";
                DataTable dtAccount = Common.Execute_Procedures_Select_ByQuery(sql);
                if (dtAccount.Rows.Count > 0)
                {
                    txtGainLossAccountName.Text = dtAccount.Rows[0]["AccountName"].ToString();
                    GainLossAccountId = Convert.ToInt32(dtAccount.Rows[0]["AccountId"].ToString());
                }
                else
                {
                    lblmsg1.Text = "Please enter correct Account Number for Exchange Gain/Loss.";
                    txtGainLossAccountName.Text = "";
                    GainLossAccountId = 0;
                    txtGainLossAccount.Focus();
                    return;
                }
            }
        }
        catch(Exception ex)
        {
            lblmsg1.Text = ex.Message.ToString();
        }
    }

    protected void txtDebitAccountCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtDebitAccountCode.Text.Length == 4)
            {
                string sql = "Select AccountName,AccountId from sql_tblSMDPRAccounts with(nolock) where  AccountNumber = " + txtDebitAccountCode.Text.Trim() + " AND Active = 'Y' ";
                DataTable dtAccount = Common.Execute_Procedures_Select_ByQuery(sql);
                if (dtAccount.Rows.Count > 0)
                {
                    txtDebitAccountName.Text = dtAccount.Rows[0]["AccountName"].ToString();
                    BankChargesAccountId = Convert.ToInt32(dtAccount.Rows[0]["AccountId"].ToString());
                }
                else
                {
                    lblmsg1.Text = "Please enter correct Account Number for Bank Charges.";
                    txtDebitAccountName.Text = "";
                    BankChargesAccountId = 0;
                    txtDebitAccountCode.Focus();
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            lblmsg1.Text = ex.Message.ToString();
        }
    }

    protected void txtBankAmount_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (! string.IsNullOrEmpty(txtBankAmount.Text.Trim()))
            {
                Decimal bankamount = Decimal.Parse(txtBankAmount.Text.Trim());
                if (bankamount > 0)
                {
                    CalculateAmount(1, bankamount, lblGainLossCurr.Text.Trim());
                }
            }
        }
        catch (Exception ex)
        {
            lblmsg1.Text = ex.Message.ToString();
        }
    }

    

    protected void txtBankChargesPopup_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtBankChargesPopup.Text.Trim()))
            {
                Decimal BankCharges = Decimal.Parse(txtBankChargesPopup.Text.Trim());
                if (BankCharges > 0)
                {
                    CalculateAmount(2, BankCharges, lblBankChargesCurr.Text.Trim());
                }
            }
        }
        catch (Exception ex)
        {
            lblmsg1.Text = ex.Message.ToString();
        }
    }
    protected void CalculateAmount(int flag, decimal amount, string currency)
    {

        try
        {
            if (flag == 1) // Exchange Gain/Loss
            {
                if (currency.Trim() == strUSD)
                {
                    txtGainLossAmt.Text = amount.ToString();
                    lblGainLossDiffCharges.Text = Math.Round(Convert.ToDecimal(txtGainLossAmt.Text) - Convert.ToDecimal(lblTotalBankAmount.Text), 2).ToString();
                }
                else
                {
                    if (XchangeRate != 0)
                        txtGainLossAmt.Text = Math.Round(amount / XchangeRate, 2).ToString();
                    lblGainLossDiffCharges.Text = Math.Round(Convert.ToDecimal(txtGainLossAmt.Text) - Convert.ToDecimal(lblTotalBankAmount.Text), 2).ToString();
                }
            }
            else
            {
                if (currency.Trim() == strUSD)
                {
                    //txtBankChargesPopup.Text = amount.ToString();
                    lblDebitAmount.Text = Math.Round(amount, 2).ToString();
                }
                else
                {
                    if (XchangeRate != 0)
                    {
                        lblDebitAmount.Text = Math.Round(amount / XchangeRate, 2).ToString();
                    }
                    else
                    {
                        lblDebitAmount.Text = "0.00";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblmsg1.Text = "Error : Currency calculation issue." + ex.Message.ToString();
        }
       
    }

    protected void AddGLEntry(int flag, string vesselCode, string PVno, int AccountId, decimal Amount, string Invoices)
    {
        try
        {
            string remarks = "";
        if (flag == 1)
        {
            remarks = "Exchange Gain/Losss for Vessel : "+lblVesselName.Text+", PV No : " + PVno.ToString() + " (Invoice # : "+ Invoices + ")";
        }
        else
        {
            remarks = "Bank Charges for Vessel : "+lblVesselName.Text+", PV No :  " + PVno.ToString() + " (Invoice # : " + Invoices + ")";
        }
        Common.Execute_Procedures_Select_ByQuery("EXEC InsertUpdateGLEntry  '" + vesselCode + "', '" + PVno + "', " + AccountId + ","+ Amount + ",'"+ remarks.ToString() + "', " + Convert.ToInt32(Session["LoginId"].ToString()) + " ");
        }
        catch (Exception ex)
        {
            lblmsg1.Text = "Error : GL Entry - " + ex.Message.ToString();
        }
    }

    protected void BankConfirmedOnPopup_TextChanged(object sender, EventArgs e)
    {
       try
        {
            lblmsg1.Text = "";
            if (!string.IsNullOrEmpty(BankConfirmedOnPopup.Text))
            {
                string strcurrrate = "Select top 1 EXC_RATE from XCHANGEDAILY with(nolock) where FOR_CURR = '" + lblGainLossCurr.Text + "' AND RATEDATE <='" + BankConfirmedOnPopup.Text.Trim() + "'  order by RATEDATE Desc";
                DataTable dtCurrRate = Common.Execute_Procedures_Select_ByQuery(strcurrrate);
                if (dtCurrRate.Rows.Count > 0)
                {
                    XchangeRate = Convert.ToDecimal(dtCurrRate.Rows[0]["EXC_RATE"]);
                    txtXchangeRate.Text = XchangeRate.ToString();
                }
                else
                {
                    txtXchangeRate.Text = "0.00";
                }
               
                txtBankAmount.Enabled = true;
                txtBankChargesPopup.Enabled = true;
                if (!string.IsNullOrEmpty(txtBankChargesPopup.Text.Trim()))
                {
                    Decimal BankCharges = Decimal.Parse(txtBankChargesPopup.Text.Trim());
                    if (BankCharges > 0)
                    {
                        CalculateAmount(2, BankCharges, lblBankChargesCurr.Text.Trim());
                    }
                }
            }
            else
            {
                txtBankAmount.Enabled = false;
                txtBankChargesPopup.Enabled = false;
                txtBankAmount.Text = "";
                txtBankChargesPopup.Text = "";
            }

        }
        catch(Exception ex)
        {
            lblmsg1.Text = "Error : Bank Transation Date selection - " + ex.Message.ToString();
        }
    }

    protected void txtXchangeRate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            XchangeRate = Convert.ToDecimal(txtXchangeRate.Text);
            if (dvExchangeGainLoss.Visible &&  !string.IsNullOrEmpty(txtBankAmount.Text.Trim()))
            {
                Decimal bankamount = Decimal.Parse(txtBankAmount.Text.Trim());
                if (bankamount > 0)
                {
                    CalculateAmount(1, bankamount, lblGainLossCurr.Text.Trim());
                }
            }
            if ( !string.IsNullOrEmpty(txtBankChargesPopup.Text.Trim()))
            {
                Decimal BankCharges = Decimal.Parse(txtBankChargesPopup.Text.Trim());
                if (BankCharges > 0)
                {
                    CalculateAmount(2, BankCharges, lblBankChargesCurr.Text.Trim());
                }
            }
        }
        catch(Exception ex)
        {
            lblmsg1.Text = "Error : Change Exchange Rate - " + ex.Message.ToString();
        }
    }
}