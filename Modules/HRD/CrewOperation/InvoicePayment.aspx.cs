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
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;

public partial class CrewAccounting_InvoicePayment : System.Web.UI.Page
{
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 28);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy3.aspx");

        }
        //*******************
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 8);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        if (!Page.IsPostBack)
        {
            this.lbl_VendorName.Text = Request.QueryString["vendorname"].ToString();
            bindinvoicepaymentgrid();
            DataTable dt;
            dt=PayInvoice.selectPayInvoice(Session["payinvoice"].ToString());
            double totamt=0;
            foreach (DataRow dr in dt.Rows)
            {
                totamt = totamt + Convert.ToDouble(dr["TotalInvoiceAmount"].ToString());
            }
            this.lbl_Amt.Text =Convert.ToString(Math.Round(totamt,2));
            this.Label1.Text = "";
            this.btn_Pay.Visible = Auth.isEdit;
        }
    }
    private void bindinvoicepaymentgrid()
    {
        gvinvoicepayment.DataSource = PayInvoice.selectPayInvoice(Session["payinvoice"].ToString());
        gvinvoicepayment.DataBind();
    }
    protected void btn_Pay_Click(object sender, EventArgs e)
    {
        int res=0;
        int id, createdby, modifiedby;
        string invoiceid="";
        string invoicename="";
        string invmaildetails;
        try
        {
            id=Convert.ToInt32(Page.Request.QueryString["vendorid"].ToString());
            createdby = Convert.ToInt32(Session["loginid"].ToString());
            invmaildetails = "<table><tr><td>Invoice No:</td><td>Invoice Date:</td><td>Invoice Amount</td><tr>";

            for (int i = 0; i < gvinvoicepayment.Rows.Count; i++)
            {
                invmaildetails = invmaildetails+ "<tr><td>" + this.gvinvoicepayment.Rows[i].Cells[1].Text + "</td><td>" + this.gvinvoicepayment.Rows[i].Cells[2].Text + "</td><td>" + this.gvinvoicepayment.Rows[i].Cells[4].Text + "</td><tr>";
                if (invoiceid == "")
                {
                    invoiceid = this.gvinvoicepayment.DataKeys[i].Value.ToString();
                    invoicename=this.gvinvoicepayment.Rows[i].Cells[1].Text; 
                }
                else
                {
                    invoiceid = invoiceid + "," + this.gvinvoicepayment.DataKeys[i].Value.ToString();
                    invoicename=invoicename+this.gvinvoicepayment.Rows[i].Cells[1].Text; 
                }
            }

            invmaildetails = invmaildetails + "</table>";
            res = PayInvoice.insertVoucherDetails("InsertVoucherDetails", id, this.txt_BankName.Text, this.txtaccountno.Text, this.txt_CreditAccNameNo.Text, this.txt_ChequeNo.Text, txt_ChequeTTAmt.Text, txt_BankCharges.Text, this.txt_ChequeTTDate.Text, createdby, invoiceid, Convert.ToInt32(ddl_Currency.SelectedValue),txtMtmVNo.Text);
            //res = PayInvoice.insertVoucherDetails("InsertVoucherDetails", id, this.txt_BankName.Text, this.txtaccountno.Text, this.txt_CreditAccNameNo.Text, this.txt_ChequeNo.Text, Convert.ToDouble(txt_ChequeTTAmt.Text), Convert.ToDouble(txt_BankCharges.Text), this.txt_ChequeTTDate.Text, createdby, invoiceid);
            Session["PrintVoucherId"] = res.ToString(); 
            this.Label1.Text = "Record Saved Successfully";
            //ddl_Currency.SelectedIndex = 0;
            btn_Pay.Enabled = false;
            btn_Print.Enabled = true && Auth.isPrint ;  
            if (this.chk_Email.Checked == true)
            {
                DataTable dt = InvoiceEntry.SelectInvoiceMailDetails(id);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    String MailTo = "asingh@energiossolutions.com";
                    String Subject = "Invoice Payment Confirmation";
                    String MailBody;
                    MailBody = "Hello,<br>" + dr["ContactPerson"].ToString() + "<br><br>" + "We have made the payment of following invoices, please find below the payment details <br><br><b>Bank Details</b> <br> Bank Name:-" + this.txt_BankName.Text + "<br> Credit Act.No:- " + this.txtaccountno.Text + "<br>Credit Act Name:-" + this.txt_CreditAccNameNo.Text + "<br>Cheque/TT No:-" + this.txt_ChequeNo.Text + "<br> Cheque/TT Amt:-" + this.txt_ChequeTTAmt.Text + "<br> Cheque/TT Date:-" + this.txt_ChequeTTDate.Text + "<br> Bank Charges:-" + this.txt_BankCharges.Text + "<br><b>Invoice Details</b><br>" + invmaildetails + "<br>";
                    //MailBody = "Hello,<br>" + dr["ContactPerson"].ToString() + "<br><br>" + "We have made the payment of following invoices, please find below the payment details <br><br><b>Bank Details</b> <br> Bank Name:-" + this.txt_BankName.Text + "<br> Credit Act.No:- " + this.txtaccountno.Text + "<br>Credit Act Name:-" + this.txt_CreditAccNameNo.Text + "<br>Cheque/TT No:-" + this.txt_ChequeNo.Text + "<br> Cheque/TT Amt:-" + this.txt_ChequeTTAmt.Text + "<br> Cheque/TT Date:-" + this.txt_ChequeTTDate.Text + "<br><br><b>Invoice Details</b><br>" + invmaildetails + "<br> Bank Charges:-" + this.txt_BankCharges.Text + "<br>";
                    MailBody = MailBody + "Thanks & Best Regards<br><font color=000080 size=2 face=Century Gothic>Accounts Dept.</font><br><br><font color=000080 size=2 face=Century Gothic><strong>ENERGIOS MARITIME PVT. LTD.</strong></font><br><font color=000080 size=2 face=Century Gothic>As owner's agent</font><br>";
                    //MailBody = MailBody + "Thanks<br><br><font color=000080 size=2 face=Century Gothic>Best Regards<font><br><font color=000080 size=2 face=Century Gothic>Agnes</font><br><font color=000080 size=2 face=Century Gothic><strong>ENERGIOS MARITIME PVT. LTD.</strong></font><br><font color=000080 size=2 face=Century Gothic>As owner's agent</font><br>";
                    MailBody = MailBody + "<font color=000080 size=2 face=Century Gothic>78 Shenton Way #13-01</font><br><font color=000080 size=2 face=Century Gothic>Lippo Centre Singapore 079120</font><br><font color=000080 size=2 face=Century Gothic>Board Number:  +65 - 63041770</font><br>";
                    //MailBody = MailBody + "<font color=000080 size=2 face=Century Gothic>78 Shenton Way #13-01</font><br><font color=000080 size=2 face=Century Gothic>Lippo Centre Singapore 079120</font><br><font color=000080 size=2 face=Century Gothic>Board Number:  +65 - 63041770</font><br><font color=000080 size=2 face=Century Gothic>Direct Number:  +65 - 63041795</font><br><font color=000080 size=2 face=Century Gothic>Mobile Number: +65 - 81811795</font><br>";
                    MailBody = MailBody + "<font color=000080 size=2 face=Century Gothic>Fax Number:  +65 - 62207988</font><br>";
                    //MailBody = MailBody + "<a href='mailto:noreply@energiossolutions.com' runat='server'><font color=000080 size=2 face=Century Gothic>E-mail: noreply@energiosmaritime.com</font></a>";
                    MailBody = MailBody + "<a href='mailto:noreply@energiosmaritime.com' runat='server'><font color=000080 size=2 face=Century Gothic>E-mail: noreply@energiosmaritime.com</font></a>";
                    //MailBody = MailBody + "<a href='mailto:noreply@energiossolutions.com.com' runat='server'><font color=000080 size=2 face=Century Gothic>E-mail: noreply@energiossolutions.com</font></a>";
                    SendMail.MailSend(MailTo, Subject, MailBody, MailSend.LoginUserEmailId(Convert.ToInt32(Session["loginid"].ToString())));
                }
            }
        }
        catch (Exception ex)
         {
           
        }
    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ApprovedInvoices.aspx");
    }
    protected void btn_Print_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Reporting/PaymentVoucher.aspx");
    }
}
