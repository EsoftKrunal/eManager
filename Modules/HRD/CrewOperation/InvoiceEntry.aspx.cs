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

public partial class CrewOperation_InvoiceEntry : System.Web.UI.Page
{
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        this.lbl_inv_Message.Text = "";
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 8);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        if (!Page.IsPostBack)
        {
            txt_DueDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");     
            btn_Cancel.Visible = false;
            bindVesselNameddl();
            bindCurrencyddl();
            bindForwardToddl();
            BindVendorDropDown();
            int mode = Convert.ToInt32(Request.QueryString["mode"].ToString());
            if (mode == 0)
            {
                btn_Save.Visible = false;
            }
            else
            {
                btn_Save.Visible = true && Auth.isAdd;
            }
            int InvoiceId=Convert.ToInt32(Request.QueryString["InvoiceId"].ToString());
            if (InvoiceId != 0)
            {
                showdata(InvoiceId);
                InvoiceStatus1.InoviceId = InvoiceId; 
            }
            else
            {
                HiddenInvoiceEntry.Value = "";
            }
        }
    }
    protected void showdata(int Invoiceid)
    {
        string Mess = "";
        //lbl_inv_Message.Visible = false;
        HiddenInvoiceEntry.Value = Invoiceid.ToString();
        DataTable dt3 = InvoiceEntry.selectDataInvoiceDetailsByInvoiceId(Invoiceid);
        foreach (DataRow dr in dt3.Rows)
        {
            txt_RefNo.Text = dr["RefNo"].ToString();
            Mess = Mess + Alerts.Set_DDL_Value(ddl_Vendor, dr["VendorId"].ToString(), "Vendor");
            Mess = Mess + Alerts.Set_DDL_Value(ddCurrency, dr["CurrencyId"].ToString(), "Currency");
            txt_InvNo.Text = dr["InvNo"].ToString();
            double dbl = Convert.ToDouble(dr["InvoiceAmount"].ToString());
            txt_InvAmount.Text = dbl.ToString("F");
            double dbl2 = Convert.ToDouble(dr["GST"].ToString());
            try
            {
                txt_GST.Text = Convert.ToString(Math.Round(dbl2, 2));
                txt_TotalInvAmount.Text = Convert.ToString((Convert.ToInt32(dr["InvoiceAmount"])));
            }
            catch { }
            txt_InvDate.Text = dr["InvDate"].ToString();
            txt_DueDate.Text =Convert.ToDateTime(dr["DueDate"].ToString()).ToString("dd-MMM-yyyy");
             
            Mess = Mess + Alerts.Set_DDL_Value(ddl_Vessel, dr["VesselId"].ToString(), "Vessel");
        
        
            Mess = Mess + Alerts.Set_DDL_Value(ddl_ForwardTo, dr["Verify1To"].ToString(), "User (Forwarded To)");
        
            txt_CreatedBy.Text = dr["CreatedBy"].ToString();
            txt_CreatedOn.Text = dr["CreatedOn"].ToString();
        }
        if (Mess.Length > 0)
        {
            //this.lbl_inv_Message.Visible = true;
            this.lbl_inv_Message.Text = "The Status Of Following Has Been Changed To In-Active <br/>" + Mess.Substring(1);
        }
    }
    protected void bindVesselNameddl()
    {
        DataSet ds =cls_SearchReliever.getMasterData("Vessel","VesselId","VesselName", Convert.ToInt32(Session["loginid"].ToString()));
        ddl_Vessel.DataSource = ds.Tables[0];
        ddl_Vessel.DataValueField = "VesselId";
        ddl_Vessel.DataTextField = "VesselName";
        ddl_Vessel.DataBind();
        ddl_Vessel.Items.Insert(0, new ListItem("<Select>","0"));
    }
    protected void bindCurrencyddl()
    {
        DataTable dt11 = InvoiceEntry.selectCurrencyDD();
        this.ddCurrency.DataValueField = "CurrencyId";
        this.ddCurrency.DataTextField = "CurrencyName";
        this.ddCurrency.DataSource = dt11;
        this.ddCurrency.DataBind();
    }
    protected void bindForwardToddl()
    {
        DataTable dt1 = InvoiceEntry.selectUserNames(4); //- Verification department users
        this.ddl_ForwardTo.DataValueField = "LoginId";
        this.ddl_ForwardTo.DataTextField = "UserName";
        this.ddl_ForwardTo.DataSource = dt1;
        this.ddl_ForwardTo.DataBind();
    }
    private void BindVendorDropDown()
    {
        DataTable dt = InvoiceSearchScreen.selectDataVendor();
        this.ddl_Vendor.DataValueField = "VendorId";
        this.ddl_Vendor.DataTextField = "VendorName";
        this.ddl_Vendor.DataSource = dt;
        this.ddl_Vendor.DataBind();
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        int checkdup = Convert.ToInt32(Request.QueryString["duplicacy"].ToString());
        int Duplicate = 0;
        DataTable dttbl = InvoiceEntry.CheckDuplicateInvoice(Convert.ToInt32(ddl_Vendor.SelectedValue), txt_InvNo.Text.Trim(), Convert.ToInt32("0"+HiddenInvoiceEntry.Value));
        foreach (DataRow dr in dttbl.Rows)
        {
            if (Convert.ToInt32(dr[0].ToString()) > 0)
            {
                lbl_inv_Message.Text = "Duplicate Invoice Entry.";
                Duplicate = 1;
                btn_Cancel_Click(sender, e);
                break;
            }
        }
        if (Duplicate == 0)
        {
            btn_Cancel.Visible = true && Auth.isAdd;
            int InvoiceId = -1;
            int createdby = 0, modifiedby = 0;
            int vendorId = Convert.ToInt32(ddl_Vendor.SelectedValue);
            string InvNo = txt_InvNo.Text.Trim();
            double Invamt = Convert.ToDouble(txt_InvAmount.Text);
            double gst = Convert.ToDouble(txt_GST.Text);
            string Invdate = txt_InvDate.Text;
            string due_date = txt_DueDate.Text;
            int currencyId = Convert.ToInt32(ddCurrency.SelectedValue);
            int vesselId = Convert.ToInt32(ddl_Vessel.SelectedValue);
            int forwardto = Convert.ToInt32(ddl_ForwardTo.SelectedValue);
            if (HiddenInvoiceEntry.Value.Trim() == "")
            {
                createdby = Convert.ToInt32(Session["loginid"].ToString());
            }
            else
            {
                InvoiceId = Convert.ToInt32(HiddenInvoiceEntry.Value);
                modifiedby = Convert.ToInt32(Session["loginid"].ToString());
            }
            int refno;
            InvoiceEntry.insertUpdateInvoiceDetails("InsertUpdateInvoiceDetails",
                                                        out refno,
                                                        InvoiceId,
                                                        vendorId,
                                                        InvNo,
                                                        Invamt,
                                                        currencyId,
                                                        gst,
                                                        Invdate,
                                                        due_date,
                                                        vesselId,
                                                        forwardto,
                                                        createdby,
                                                        modifiedby);
            if (this.ck_Email.Checked == true)
            {
                sendmail(refno.ToString());
            }
            RegisterClientScriptBlock("onClick", "<script language=javascript>alert('Your Ref. No. is:" + refno.ToString() + "')</script>");
            btn_Cancel_Click(sender, e);
            lbl_inv_Message.Text = "Record Successfully Saved.";
        }
    }
    private void sendmail(string refno)
    {
        DataTable dt = InvoiceEntry.SelectInvoiceMailDetails(Convert.ToInt32(this.ddl_Vendor.SelectedValue));
        for(int i=0;i<dt.Rows.Count;i++)
        {
            DataRow dr=dt.Rows[i];
            String MailTo = "asingh@energiossolutions.com";
            String Subject = "Invoice Recevied Confirmation";
            String MailBody;
            MailBody = "Hello ,<br>" + dr["ContactPerson"].ToString() + "<br><br>Your Invoice# " + txt_InvNo.Text.ToString() + " has been recd.Will process it soon.<br>For any query in future for this invoice please mention the Invoice ref # "+ refno  +"<br><br>";
            MailBody = MailBody + "Thanks & Best Regards<br><font color=000080 size=2 face=Century Gothic>Accounts Dept.</font><br><br><font color=000080 size=2 face=Century Gothic><strong>ENERGIOS MARITIME PVT. LTD.</strong></font><br><font color=000080 size=2 face=Century Gothic>As owner's agent</font><br>";
            //MailBody = MailBody + "Thanks<br><br><font color=000080 size=2 face=Century Gothic>Best Regards<font><br><font color=000080 size=2 face=Century Gothic>Agnes</font><br><font color=000080 size=2 face=Century Gothic><strong>ENERGIOS MARITIME PVT. LTD.</strong></font><br><font color=000080 size=2 face=Century Gothic>As owner's agent</font><br>";
            MailBody = MailBody + "<font color=000080 size=2 face=Century Gothic>78 Shenton Way #13-01</font><br><font color=000080 size=2 face=Century Gothic>Lippo Centre Singapore 079120</font><br><font color=000080 size=2 face=Century Gothic>Board Number:  +65 - 63041770</font><br>";
            //MailBody = MailBody + "<font color=000080 size=2 face=Century Gothic>78 Shenton Way #13-01</font><br><font color=000080 size=2 face=Century Gothic>Lippo Centre Singapore 079120</font><br><font color=000080 size=2 face=Century Gothic>Board Number:  +65 - 63041770</font><br><font color=000080 size=2 face=Century Gothic>Direct Number:  +65 - 63041795</font><br><font color=000080 size=2 face=Century Gothic>Mobile Number: +65 - 81811795</font><br>";
            MailBody = MailBody + "<font color=000080 size=2 face=Century Gothic>Fax Number:  +65 - 62207988</font><br>";
            MailBody = MailBody + "<a href='mailto:asingh@energiossolutions.com' runat='server'><font color=000080 size=2 face=Century Gothic>E-mail: asingh@energiossolutions.com</font></a>";
            //MailBody = MailBody + "<a href='mailto:asingh@energiossolutions.com' runat='server'><font color=000080 size=2 face=Century Gothic>E-mail: asingh@energiossolutions.com</font></a>";

            
            SendMail.MailSend(MailTo, Subject, MailBody,MailSend.LoginUserEmailId(Convert.ToInt32(Session["loginid"].ToString())));
        }

    }
    protected void btn_Cancel_Click(object sender, EventArgs e)
    {
        HiddenInvoiceEntry.Value = "";
        txt_RefNo.Text = "";
        ddl_Vendor.SelectedIndex = 0;
        ddCurrency.SelectedIndex = 0;
        txt_InvNo.Text = "";
        txt_InvAmount.Text = "";
        txt_GST.Text = "0";
        txt_TotalInvAmount.Text = "";
        txt_InvDate.Text = "";
        txt_DueDate.Text = "";
        ddl_Vessel.SelectedIndex = 0;
        ddl_ForwardTo.SelectedIndex = 0;
        txt_CreatedBy.Text = "";
        txt_CreatedOn.Text = "";
        //lbl_inv_Message.Visible = false;
    }
    protected void txt_InvAmount_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //lbl_inv_Message.Visible = false;
            double tot_amt;
            if (txt_GST.Text != "")
            {
                tot_amt = Convert.ToDouble(txt_InvAmount.Text) + Convert.ToDouble(txt_GST.Text);
            }
            else
            {
                tot_amt = Convert.ToDouble(txt_InvAmount.Text);
            }
            txt_TotalInvAmount.Text = tot_amt.ToString();
        }
        catch
        {
        }
    }
    protected void txt_GST_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //lbl_inv_Message.Visible = false;
            double tot_amt;
            if (txt_InvAmount.Text != "")
            {
                tot_amt = Convert.ToDouble(txt_InvAmount.Text) + Convert.ToDouble(txt_GST.Text);
            }
            else
            {
                tot_amt = Convert.ToDouble(txt_GST.Text);
            }
            txt_TotalInvAmount.Text = tot_amt.ToString();
        }
        catch
        {
        }
    }
    protected void txt_InvDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime dt = Convert.ToDateTime(txt_InvDate.Text);
            txt_DueDate.Text = dt.AddDays(30).ToString("MM/dd/yyyy") ;
        }
        catch 
        {
        }
    }
    protected void btn_Back_Approval_Click(object sender, EventArgs e)
    { int i=0;
        try
        {
            i = Convert.ToInt32(Request.QueryString["backpage"]);
        } 
        catch 
        {
        }
        if (i == 2)
        { Response.Redirect("InvoiceList1.aspx"); }
        if (i == 1)
        { Response.Redirect("InvoiceListforVerification1.aspx?mode=0"); }
    }
    }
