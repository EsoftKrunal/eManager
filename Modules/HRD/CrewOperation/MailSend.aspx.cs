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
using System.Net;
using System.Net.Mail;

public partial class CrewOperation_MailSend : System.Web.UI.Page
{
    int _id;
    String _MyMessageBody;
    String _strFromTo;
    String _strTo;

    public String MyMessageBody
    {
        get
        {
            return _MyMessageBody;
        }
        set
        {
            _MyMessageBody = value;
        }

    }
    public Int32 id
    {
        get { return _id; }
        set { _id = value; }
    }
    public String strFromTo
    {
        get { return _strFromTo; }
        set { _strFromTo = value; }
    }

    public  string _ServerName = ConfigurationManager.AppSettings["SMTPServerName"];
    public  int _Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
    public  MailAddress _FromAdd = new MailAddress(ConfigurationManager.AppSettings["FromAddress"]);
    public  string _UserName = ConfigurationManager.AppSettings["SMTPUserName"];
    public  string _Password = ConfigurationManager.AppSettings["SMTPUserPwd"];

    public void SetMails(SmtpClient _SmtpClient, MailMessage _MailMessage, string _ReplyMailAddress)
    {
        _SmtpClient.Host = _ServerName;
        _SmtpClient.Port = _Port;
        _SmtpClient.Credentials = new NetworkCredential(_UserName, _Password);
        _MailMessage.From = _FromAdd;
        _SmtpClient.EnableSsl = true;

        if (_ReplyMailAddress.Trim() == "emanager@energiossolutions.com")
        {
            _ReplyMailAddress = _FromAdd.Address;
        }
       // _ReplyMailAddress = _ReplyMailAddress.Replace("@energiosmaritime.com", "@energiossolutions.com");
        _MailMessage.ReplyTo = new MailAddress(_ReplyMailAddress);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        try
        {
            if (Page.IsPostBack == false)
            {
                txtCC.Text = Session["EmailAddress"].ToString();
                id = Convert.ToInt32(Request.QueryString["id"]);
                if (Request.QueryString["mode"] == "1")
                {
                    DataTable dt = MailSend.SelectPortAgentBooking_Mail_Header(id);
                    if (dt.Rows.Count > 0)
                    {
                        txtTo.Text = Convert.ToString(dt.Rows[0]["Mail"]);
                        hd1.Value = Convert.ToString(dt.Rows[0]["Mail"]);
                        row1.Style["display"] = "block";
                        row2.Style["display"] = "block";
                        txtAttn.Text = Convert.ToString(dt.Rows[0]["Mail"]);
                        txtSubject.Text = "Crew changes at" + " " + Convert.ToString(dt.Rows[0]["Portname"]) + " " + Convert.ToString(dt.Rows[0]["ETA"]);
                        MailMessageDetails_Port();
                        //ftb1.Text = MyMessageBody;
                        txtbody.InnerHtml = MyMessageBody;
                    }
                }
                else if (Request.QueryString["mode"] == "2")
                {
                    DataTable dt = MailSend.SelectTravelAgentBooking_Mail_Header(id);
                    if (dt.Rows.Count > 0)
                    {

                        txtTo.Text = Convert.ToString(dt.Rows[0]["Mail"]);
                        hd1.Value = Convert.ToString(dt.Rows[0]["Mail"]);
                        row1.Style["display"] = "none";
                        row2.Style["display"] = "none";
                        txtSubject.Text = Convert.ToString(dt.Rows[0]["VesselName"]) + " - " + Convert.ToString(dt.Rows[0]["FromAirPort"]) + " > " + Convert.ToString(dt.Rows[0]["ToAirPort"]);
                        strFromTo = Convert.ToString(dt.Rows[0]["FromAirPort"]) + " > " + Convert.ToString(dt.Rows[0]["ToAirPort"]);
                        MailMessageDetails_Travel();
                        //ftb1.Text = MyMessageBody;
                        txtbody.InnerHtml = MyMessageBody;
                    }
                }

            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void bin_sendmail_Click(object sender, EventArgs e)
    {
        SendMail();
        if (Convert.ToInt32(Request.QueryString["mode"]) == 1)
        {
            Response.Redirect("PortAgent.aspx");
        }
        else
        {
            Response.Redirect("TravelBooking.aspx");
        }

    }
    public void SendMail()
    {
        char[] c = { ';' };
        Array a = txtTo.Text.Split(c);
        try
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            MailAddress fromAddress = new MailAddress(MailSend.LoginUserEmailId(Convert.ToInt32(Session["loginid"].ToString())));
            message.From = fromAddress;
            for (int i = 0; i < a.Length; i++)
            {
                message.To.Add(a.GetValue(i).ToString());
            }

            if (Convert.ToString(txtCC.Text) != "")
            {
                message.CC.Add(txtCC.Text.ToString());
            }

            SetMails(smtp, message, fromAddress.Address);

            message.Body = txta.Text;
            message.Subject = txtSubject.Text.ToString();
            message.IsBodyHtml = true;
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            smtp.Send(message);

        }
        catch (Exception es)
        {
            throw es;
        }
    }

    public void MailMessageDetails_Port()
    {

        MyMessageBody = MyMessageBody + "<table border=0 width=500 align='left'>";
        MyMessageBody = MyMessageBody + "<tr><td colspan='2' class='InvoiceText' align='left'>";
        MyMessageBody = MyMessageBody + "Good Day, " + "<br><br>";
        MyMessageBody = MyMessageBody + "Please be advised that we will be arranging crew changes for above mentioned vessel at Santos when she calls on/about 22/June/2008.  Kindly find below the on/off-signers' particulars and flight details" + "<br></td></tr>";

        MyMessageBody = MyMessageBody + "<tr valign='top'>";
        MyMessageBody = MyMessageBody + "<td colspan='2' class='InvoiceText' align='left'><hr/><br/> <strong> On-signers :</strong><br>";
        MyMessageBody = MyMessageBody + "</td>";
        MyMessageBody = MyMessageBody + "</tr>";

        DataTable dtSignOnMembers = MailSend.SelectPortAgentBooking_Mail_SignOnMembers(id);

        for (int i = 0; i <= dtSignOnMembers.Rows.Count - 1; i++)
        {
            int x = i + 1;
            MyMessageBody = MyMessageBody + "<tr valign='top'>";
            MyMessageBody = MyMessageBody + "<td colspan='2' class='InvoiceText' align='left'>";
            MyMessageBody = MyMessageBody + "<table border=0 width=500 align='left' height='137'>";

            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='40%'>" + x + "." + Convert.ToString(dtSignOnMembers.Rows[i]["Rank"]) + " </td>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='50%'>" + Convert.ToString(dtSignOnMembers.Rows[i]["Name"]) + "</td>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='10%'></td>";
            MyMessageBody = MyMessageBody + "</tr>";


            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='10%'>Nationality</td>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='50%'>" + Convert.ToString(dtSignOnMembers.Rows[i]["Nationality"]) + "</td>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='40%'></td>";
            MyMessageBody = MyMessageBody + "</tr>";

            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='33%'>CDC:&nbsp;&nbsp;" + Convert.ToString(dtSignOnMembers.Rows[i]["CDC"]) + "</td>";
            if (dtSignOnMembers.Rows[i]["DOI"] == null)
            {
                MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='33%'>DOB:&nbsp;&nbsp;</td>";
            }
            else
            {
                MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='33%'>DOB: &nbsp;&nbsp;" + Convert.ToString(dtSignOnMembers.Rows[i]["DOB"]) + "</td>";
            }
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='33%'>POB:&nbsp;&nbsp;" + Convert.ToString(dtSignOnMembers.Rows[i]["POB"]) + "</td>";

            MyMessageBody = MyMessageBody + "</tr>";

            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='33%'>PP NO: &nbsp;&nbsp;" + Convert.ToString(dtSignOnMembers.Rows[i]["Passport"]) + "</td>";
            if (dtSignOnMembers.Rows[i]["DOI"] == null)
            {
                MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='33%'>DOI:&nbsp;&nbsp;</td>";
            }
            else
            {
                MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='33%'>DOI:&nbsp;&nbsp;" + Convert.ToString(dtSignOnMembers.Rows[i]["DOI"]) + "</td>";
            }
            if (dtSignOnMembers.Rows[i]["DOI"] == null)
            {
                MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='33%'>DOE:&nbsp;&nbsp;</td>";
            }
            else
            {
                MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='33%'>DOE:&nbsp;&nbsp;" + Convert.ToString(dtSignOnMembers.Rows[i]["DOE"]) + "</td>";
            }

            MyMessageBody = MyMessageBody + "</tr>";

            MyMessageBody = MyMessageBody + "</table>";


            MyMessageBody = MyMessageBody + "</td>";
            MyMessageBody = MyMessageBody + "</tr>";
        }

        MyMessageBody = MyMessageBody + "<tr valign='top'>";
        MyMessageBody = MyMessageBody + "<td colspan='2' class='InvoiceText' align='left'><br/>";
        MyMessageBody = MyMessageBody + "<b>Flight Details.</b></td>";
        MyMessageBody = MyMessageBody + "</tr>";

        MyMessageBody = MyMessageBody + "<tr valign='top'>";
        MyMessageBody = MyMessageBody + "<td colspan='2' class='InvoiceText' align='left'>";
        MyMessageBody = MyMessageBody + "Please send 'OK TO BOARD' to the respective airlines and cc copy to all the above mentioned parties asap.  Kindly meet them at the airport up their arrival, arrange entry visa and assist their joining accordingly.</td>";
        MyMessageBody = MyMessageBody + "</tr>";

        MyMessageBody = MyMessageBody + "<tr valign='top'>";
        MyMessageBody = MyMessageBody + "<td colspan='2' class='InvoiceText' align='left'><hr/><br/><strong> Off-signers :</strong><br>";
        MyMessageBody = MyMessageBody + "</td>";
        MyMessageBody = MyMessageBody + "</tr>";

        DataTable dtSignOffmembers = MailSend.SelectPortAgentBooking_Mail_SignOffMembers(id);

        for (int i = 0; i <= dtSignOffmembers.Rows.Count - 1; i++)
        {
            int x = i + 1;
            MyMessageBody = MyMessageBody + "<tr valign='top'>";
            MyMessageBody = MyMessageBody + "<td colspan='2' class='InvoiceText' align='left'>";

            MyMessageBody = MyMessageBody + "<table border=0 width=500 align='left' >";

            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='40%'>" + x + "." + Convert.ToString(dtSignOffmembers.Rows[i]["Rank"]) + "</td>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='50%'>" + Convert.ToString(dtSignOffmembers.Rows[i]["Name"]) + "</td>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='10%'></td>";
            MyMessageBody = MyMessageBody + "</tr>";


            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='10%'>Nationality</td>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='50%'>" + Convert.ToString(dtSignOffmembers.Rows[i]["Nationality"]) + "</td>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='40%'></td>";
            MyMessageBody = MyMessageBody + "</tr>";

            MyMessageBody = MyMessageBody + "<tr>";

            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left'>PP NO: &nbsp;&nbsp;" + Convert.ToString(dtSignOffmembers.Rows[i]["PassPort"]) + "</td>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left'>CDC:&nbsp;&nbsp;" + Convert.ToString(dtSignOffmembers.Rows[i]["CDC"]) + "</td>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left'></td>";
            MyMessageBody = MyMessageBody + "</tr>";

            MyMessageBody = MyMessageBody + "</table>";


            MyMessageBody = MyMessageBody + "</td>";
            MyMessageBody = MyMessageBody + "</tr>";
        }

        MyMessageBody = MyMessageBody + "<tr valign='top'>";
        MyMessageBody = MyMessageBody + "<td colspan='2' class='InvoiceText' align='left'>";
        //MyMessageBody = MyMessageBody + "<b>Flight Details.</b></td>";
        MyMessageBody = MyMessageBody + "</tr>";

        MyMessageBody = MyMessageBody + "<tr valign='top'>";
        MyMessageBody = MyMessageBody + "<td colspan='2' class='InvoiceText' align='left'>";
        MyMessageBody = MyMessageBody + "<br/><br/>Our travel agent has tentatively book and confirm the off-signer flight as above, they will send and advise E-ticket details in due course.</td>";
        MyMessageBody = MyMessageBody + "</tr>";

        MyMessageBody = MyMessageBody + "<tr valign='top'>";
        MyMessageBody = MyMessageBody + "<td colspan='2' class='InvoiceText' align='left'><br>";
        MyMessageBody = MyMessageBody + "Please confirm receipt our message in return email and assist both on/off-signers accordingly.</td>";
        MyMessageBody = MyMessageBody + "</tr>";

        MyMessageBody = MyMessageBody + "<tr valign='top'>";
        MyMessageBody = MyMessageBody + "<td colspan='2' class='InvoiceText' align='left'><br>";
        MyMessageBody = MyMessageBody + "Kindly send us the guarantee letter and OK TO BOARD for on-signer. Please advise ship berthing schedule</td>";
        MyMessageBody = MyMessageBody + "</tr>";

        MyMessageBody = MyMessageBody + "<tr valign='top'>";
        MyMessageBody = MyMessageBody + "<td colspan='2' class='InvoiceText' align='left'><br>";
        MyMessageBody = MyMessageBody + "Thanks<br><br><font color=000080 size=2 face=Century Gothic>Best Regards<font><br><font color=000080 size=2 face=Century Gothic>" + Session["UserName"].ToString() + "</font><br><font color=000080 size=2 face=Century Gothic><strong>ENERGIOS MARITIME PVT. LTD.</strong></font><br><font color=000080 size=2 face=Century Gothic>As owner's agent</font><br>";
        //MyMessageBody = MyMessageBody + "<font color=000080 size=2 face=Century Gothic>78 Shenton Way #13-01</font><br><font color=000080 size=2 face=Century Gothic>Lippo Centre Singapore 079120</font><br><font color=000080 size=2 face=Century Gothic>Board Number:  +65 - 63041770</font><br><font color=000080 size=2 face=Century Gothic>Direct Number:  +65 - 63041795</font><br><font color=000080 size=2 face=Century Gothic>Mobile Number: +65 - 81811795</font><br>";
        //MyMessageBody = MyMessageBody + "<font color=000080 size=2 face=Century Gothic>Fax Number:  +65 - 62207988</font><br>";
        MyMessageBody = MyMessageBody + "<a href='mailto:" + Session["EmailAddress"].ToString() + "' runat='server'><font color=000080 size=2 face=Century Gothic>E-mail: " + Session["EmailAddress"].ToString() + "</font></a>";


        MyMessageBody = MyMessageBody + "</tr>";

        MyMessageBody = MyMessageBody + "</table>";

    }

    public void MailMessageDetails_Travel()
    {

        MyMessageBody = MyMessageBody + "<table border=0 width=550 align='left' >";
        MyMessageBody = MyMessageBody + "<tr><td colspan='2' class='InvoiceText' align='left'>";
        MyMessageBody = MyMessageBody + "Hi , " + "<br><br>";
        MyMessageBody = MyMessageBody + "Please book and quote the airfare for the following:"+"<br></td></tr>";

        DataTable dtTravelBookingDetails = MailSend.SelectTravelAgentBooking_Mail_Details(id);

        for (int i = 0; i <= dtTravelBookingDetails.Rows.Count - 1; i++)
        {
            int x = i + 1;
            MyMessageBody = MyMessageBody + "<tr valign='top'>";
            MyMessageBody = MyMessageBody + "<td colspan='2' class='InvoiceText' align='left'>";
            MyMessageBody = MyMessageBody + "<table border=0 width=550 align='left' height='80' style='font-size:13px'>";

            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td align='left' width='30px'>" + x + "." +"</td>";
            MyMessageBody = MyMessageBody + "<td align='left' width='100px'>" + Convert.ToString(dtTravelBookingDetails.Rows[i]["RankCode"]) + "</td>";
            MyMessageBody = MyMessageBody + "<td align='left' width='250px'>" + Convert.ToString(dtTravelBookingDetails.Rows[i]["Name"]) + "</td>";
            MyMessageBody = MyMessageBody + "<td align='left' width='120px'></td>";
            MyMessageBody = MyMessageBody + "</tr>";


            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "<td >Nationality:</td>";
            MyMessageBody = MyMessageBody + "<td >" + Convert.ToString(dtTravelBookingDetails.Rows[i]["Nationality"]) + "</td>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "</tr>";

            //-----------
            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "<td >CDC:&nbsp;&nbsp;" + Convert.ToString(dtTravelBookingDetails.Rows[i]["CDC"]) + "</td>";
            if (dtTravelBookingDetails.Rows[i]["DOI"] == null)
            {
                MyMessageBody = MyMessageBody + "<td >DOB:&nbsp;&nbsp;</td>";
            }
            else
            {
                MyMessageBody = MyMessageBody + "<td >DOB: &nbsp;&nbsp;" + Convert.ToString(dtTravelBookingDetails.Rows[i]["DOB"]) + "</td>";
            }
            
            MyMessageBody = MyMessageBody + "<td >POB:&nbsp;&nbsp;" + Convert.ToString(dtTravelBookingDetails.Rows[i]["POB"]) + "</td>";

            MyMessageBody = MyMessageBody + "</tr>";

            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "<td >PP NO: &nbsp;&nbsp;" + Convert.ToString(dtTravelBookingDetails.Rows[i]["PPNo"]) + "</td>";
            if (dtTravelBookingDetails.Rows[i]["DOI"] == null)
            {
                MyMessageBody = MyMessageBody + "<td >DOI:&nbsp;&nbsp;</td>";
            }
            else
            {
                MyMessageBody = MyMessageBody + "<td >DOI:&nbsp;&nbsp;" + Convert.ToString(dtTravelBookingDetails.Rows[i]["DOI"]) + "</td>";
            }
            if (dtTravelBookingDetails.Rows[i]["DOI"] == null)
            {
                MyMessageBody = MyMessageBody + "<td >DOE:&nbsp;&nbsp;</td>";
            }
            else
            {
                MyMessageBody = MyMessageBody + "<td >DOE:&nbsp;&nbsp;" + Convert.ToString(dtTravelBookingDetails.Rows[i]["DOE"]) + "</td>";
            }

            MyMessageBody = MyMessageBody + "</tr>";
            //----------

            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "<td >Itinerary:</td>";
            MyMessageBody = MyMessageBody + "<td >" + strFromTo + "</td>";
            MyMessageBody = MyMessageBody + "</tr>";

            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "<td >Departure Date:</td>";
            MyMessageBody = MyMessageBody + "<td >" + Convert.ToString(dtTravelBookingDetails.Rows[i]["DDate"]) + "</td>";
            MyMessageBody = MyMessageBody + "</tr>";

            MyMessageBody = MyMessageBody + "</table>";


            MyMessageBody = MyMessageBody + "</td>";
            MyMessageBody = MyMessageBody + "</tr>";
        }


        MyMessageBody = MyMessageBody + "<tr valign='top'>";
        MyMessageBody = MyMessageBody + "<td colspan='2' class='InvoiceText' align='left'><br>";
        MyMessageBody = MyMessageBody + "Thanks<br><br><font color=000080 size=2 face=Century Gothic>Best Regards<font><br><font color=000080 size=2 face=Century Gothic>" + Session["UserName"].ToString() + "</font><br><font color=000080 size=2 face=Century Gothic><strong>ENERGIOS MARITIME PVT. LTD.</strong></font><br><font color=000080 size=2 face=Century Gothic>As owner's agent</font><br>";
        //MyMessageBody = MyMessageBody + "<font color=000080 size=2 face=Century Gothic>78 Shenton Way #13-01</font><br><font color=000080 size=2 face=Century Gothic>Lippo Centre Singapore 079120</font><br><font color=000080 size=2 face=Century Gothic>Board Number:  +65 - 63041770</font><br><font color=000080 size=2 face=Century Gothic>Direct Number:  +65 - 63041795</font><br><font color=000080 size=2 face=Century Gothic>Mobile Number: +65 - 81811795</font><br>";
        //MyMessageBody = MyMessageBody + "<font color=000080 size=2 face=Century Gothic>Fax Number:  +65 - 62207988</font><br>";
        MyMessageBody = MyMessageBody + "<a href='mailto:" + Session["EmailAddress"].ToString() + "' runat='server'><font color=000080 size=2 face=Century Gothic>E-mail: " + Session["EmailAddress"].ToString() + "</font></a>";

        
        MyMessageBody = MyMessageBody + "</tr>";

        MyMessageBody = MyMessageBody + "</table>";

    }
}
