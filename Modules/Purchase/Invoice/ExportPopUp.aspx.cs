using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Invoice_ExportPopUp : System.Web.UI.Page
{
    public int PaymentId
    {
        get { return Convert.ToInt32(ViewState["PaymentId"]); }
        set { ViewState["PaymentId"] = value; }
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
        lblMsg.Text = "";
        lblmsg2.Text = "";
        if (!Page.IsPostBack)
        {
            UserId = Common.CastAsInt32(Session["loginid"]);
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT Payment FROM POS_Invoice_mgmt where USERID=" + UserId);
            if (dt.Rows[0]["Payment"].ToString() == "True")
            {
                PaymentId = Common.CastAsInt32(Request.QueryString["PaymentId"]);
                UserId = Common.CastAsInt32(Session["loginid"]);
                loadaccountcode();
                showpaymentdetails();
            }
            else
            {
                Response.Redirect("~/NoPermission.aspx");
            }
        }
    }


    protected void btnExportNow_Click(object sender, EventArgs e)
    {
        int _PaymentId = Common.CastAsInt32(((Button)sender).CommandArgument);
        if(ddlacctcodes.SelectedValue.Length!=4 || ddlacctcodes.SelectedIndex<=0)
        {
            lblMsg.Text = "Please select bank account code to export.";
            return;
        }
        if (_PaymentId > 0)
        {
            Common.Set_Procedures(lblOwnerCode.Text + ".dbo.APExport_POS_DebitNote");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(
                new MyParameter("@PaymentId", _PaymentId),
                new MyParameter("@UserName", Session["loginid"].ToString().Replace(" ", "_")),
                new MyParameter("@ACTNO", ddlacctcodes.SelectedValue)
            );

            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                int Result = 0;
                String Message = "";
                Result = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);
                Message = ds.Tables[0].Rows[0][1].ToString();
                lblMsg.Text = Message;
                if (Result == 1) { btnExportNow.Visible = false; }
            }
            else
            {
                lblMsg.Text = Common.getLastError();
            }
        }
    }

    protected void btnChangeVendor_Click(object sender, EventArgs e)
    {
        dvChangeVendor.Visible = true;
        //---------------------
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select * from VW_PAYMENTVOUCHERS_001 where PTYPE='O' and PAYMENTID=" + PaymentId);
        if (dt.Rows.Count > 0)
        {
            lblVendorName2.Text = dt.Rows[0]["SupplierName"].ToString();
            lblVendorCode2.Text = dt.Rows[0]["Travid"].ToString();

            txtSupplier.Text = dt.Rows[0]["SupplierName"].ToString();
            hfdSupplier.Value = dt.Rows[0]["SupplierId"].ToString();
        }

        //------------------

    }
    public void loadaccountcode()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select * from VW_PAYMENTVOUCHERS_001 where PTYPE='O' and PAYMENTID=" + PaymentId);
        if (dt.Rows.Count > 0)
        {
            ddlacctcodes.Items.Clear();
            if (dt.Rows[0]["PaymentType"].ToString() == "U")
            {
                ddlacctcodes.Items.Add(new ListItem("SELECT", ""));
                ddlacctcodes.Items.Add(new ListItem("1910", "1910"));
                ddlacctcodes.Items.Add(new ListItem("1911", "1911"));
		ddlacctcodes.Items.Add(new ListItem("1918", "1918"));
            }
            else
            {
                ddlacctcodes.Items.Add(new ListItem("SELECT", ""));
                ddlacctcodes.Items.Add(new ListItem("1915", "1915"));
                ddlacctcodes.Items.Add(new ListItem("1916", "1916"));
		ddlacctcodes.Items.Add(new ListItem("1919", "1919"));
            }
        }
    }

    public void ddlacctcodes_OnSelectedIndexChanged(object sender,EventArgs e)
    {
        showpaymentdetails();
    }
    public void showpaymentdetails()
    {

        String AcctCode = "";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select * from VW_PAYMENTVOUCHERS_001 where PTYPE='O' and PAYMENTID=" + PaymentId);
        if (dt.Rows.Count > 0)
        {
            lblAccountType.Text = (dt.Rows[0]["PaymentType"].ToString() == "U") ? "USD" : ((dt.Rows[0]["PaymentType"].ToString() == "S") ? "SGD" : "INR");

            //AcctCode = (dt.Rows[0]["PaymentType"].ToString() == "U") ? "1910" : ((dt.Rows[0]["PaymentType"].ToString() == "S") ? "1915" : "");
            AcctCode = ddlacctcodes.SelectedValue;            

            lblAccountType1.Text = lblAccountType.Text;

            lblPVNO.Text = dt.Rows[0]["PVNO"].ToString();
            lblOwner.Text = dt.Rows[0]["OwnerName"].ToString();
            lblOwnerCode.Text = dt.Rows[0]["POSOWNERID"].ToString();
            lblBankTransDate.Text = Common.ToDateString(dt.Rows[0]["BankConfirmedOn"]);
            lblAmountPaid.Text = dt.Rows[0]["BankAmount"].ToString();
            Decimal bankchg = Common.CastAsDecimal(dt.Rows[0]["BankCharges"]);
            lblBankCharges.Text = bankchg.ToString();
            dvBC.Visible = bankchg != 0;

            lblVendorName.Text = dt.Rows[0]["SupplierName"].ToString();
            lblVendorCode.Text = dt.Rows[0]["Travid"].ToString();
            DataTable dtVendor = Common.Execute_Procedures_Select_ByQuery("select CURRENCYID from " + lblOwnerCode.Text + ".DBO.tblAPVendor WHERE VENDORID='" + lblVendorCode.Text + "'");
            if (dtVendor.Rows.Count > 0)
            {
                Decimal sgd_usd_exchrate = 1;
                Decimal calc_exchrate = 1;
                String RateDate = "";
                //DataTable dtExRate = Common.Execute_Procedures_Select_ByQuery("SELECT 1 AS EXCHRATE,GETDATE() AS EFFECTDATE ");
                DataTable dtExRate = Common.Execute_Procedures_Select_ByQuery("SELECT TOP 1 EXCHRATE,EFFECTDATE FROM SYS.DBO.tblSmExchRate WHERE EFFECTDATE <='" + lblBankTransDate.Text + "' AND CURRENCYFROM = 'US$'AND CURRENCYTO = 'S$' ORDER BY EFFECTDATE DESC");
                
                if (dtExRate.Rows.Count > 0)
                {
                    sgd_usd_exchrate = Common.CastAsDecimal(dtExRate.Rows[0]["EXCHRATE"]);
                    RateDate = Common.ToDateString(dtExRate.Rows[0]["EFFECTDATE"]);
                }

                lblVendorCurrency.Text = dtVendor.Rows[0]["CURRENCYID"].ToString();

                if (lblVendorCurrency.Text == "US$")
                {
                    lblExchRate.Text = "1.0";
                }
                else
                {
                    lblExchRate.Text = sgd_usd_exchrate + " as on " + RateDate;
                }

                if (lblAccountType.Text == "SGD")
                {
                    calc_exchrate = sgd_usd_exchrate;
                    lblExchDt.Text = RateDate;
                }

                DataTable dtBA = Common.Execute_Procedures_Select_ByQuery("SELECT BankChargesPayableTo,CreditAccountCode,DebitAccountCode FROM POS_INVOICE_PAYMENT WHERE PAYMENTID=" + PaymentId);
                lblBankChgsPayableTo.Text = dtBA.Rows[0]["BankChargesPayableTo"].ToString();
                txtCreditAccountCode.Text = dtBA.Rows[0]["CreditAccountCode"].ToString();
                txtDebitAccountCode.Text = dtBA.Rows[0]["DebitAccountCode"].ToString();

                lblDebitAmount.Text = String.Format("{0:0.00}", bankchg / calc_exchrate);
                lblCreditAmount.Text = String.Format("{0:0.00}", bankchg / calc_exchrate);


                DataTable dtdetails = Common.Execute_Procedures_Select_ByQuery("select row_number() over(order by ip.InvoiceId) as Srno,P.CURRENCY, p.INVVesselCode,InvNo as InvoiceNo, " +
                        "'" + AcctCode + "' + (select replace(str(VesselId, 4), ' ', '0') from Vessel with(nolock) where VesselCode = p.INVVesselCode) as AcctCode, " +
                        "InvDate as OnDate," + calc_exchrate + " as ExchRate,p.ApprovalAmount, BankAmount,Round((BankAmount/" + calc_exchrate + "),2) as TraverseAmount from POS_Invoice_Payment_Invoices ip inner join POS_Invoice p on ip.InvoiceId = p.InvoiceId where PaymentId =" + PaymentId + " order by ip.InvoiceId ");
                rptTravDetails.DataSource = dtdetails;
                rptTravDetails.DataBind();
                if (dtdetails.Rows.Count > 0)
                    lblInvoiceCurr.Text = dtdetails.Rows[0]["CURRENCY"].ToString();

                lblInvoiceTotal.Text = String.Format("{0:0.00}", Common.CastAsDecimal(dtdetails.Compute("SUM(ApprovalAmount)", "")));
                lblBankTotal.Text = String.Format("{0:0.00}", Common.CastAsDecimal(dtdetails.Compute("SUM(BankAmount)", "")));
                lblTraverseTotal.Text = String.Format("{0:0.00}", Common.CastAsDecimal(dtdetails.Compute("SUM(TraverseAmount)", "")));

                //btnExportNow.Visible = true;
                btnExportNow.Visible = true;
                btnExportNow.CommandArgument = PaymentId.ToString();
            }
            else
            {
                btnExportNow.Visible = false;
                lblMsg.Text = "Vendor not exist in the database.";
            }
        }
    }
   
    protected void btnchangevendorsave_Click(object sender, EventArgs e)
    {
        int sid = Common.CastAsInt32(hfdSupplier.Value);
        if (sid > 0)
        {
            Common.Set_Procedures("DBO.UpdatePVVendor");
            Common.Set_ParameterLength(2);
            Common.Set_Parameters(
                new MyParameter("@PaymentId", PaymentId),
                new MyParameter("@SupplierId", sid));

            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                lblmsg2.Text = "Vendor updated successfully.";
            }
            else
            {
                lblmsg2.Text = Common.getLastError();
            }
        }
        //----
    }
    protected void btnCloseNow1_Click(object sender, EventArgs e)
    {
        showpaymentdetails();
        dvChangeVendor.Visible = false;
    }
}