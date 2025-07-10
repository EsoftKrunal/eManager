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

public partial class CrewOperation_CrewMailSend : System.Web.UI.Page
{
    int _id;
    String _MyMessageBody;
    String _strFromTo;
    String _strTo;
    int _PortCallId;
    int _TicketReqPortCallId;
    string _CrewList = "";

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
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                       | SecurityProtocolType.Tls11
                                       | SecurityProtocolType.Tls12;
        _SmtpClient.Credentials = new NetworkCredential(_UserName, _Password);
        _SmtpClient.EnableSsl = true;
        _MailMessage.From = _FromAdd;
        _SmtpClient.EnableSsl = true;

        if (_ReplyMailAddress.Trim() == "emanager@energiossolutions.com")
        {
            _ReplyMailAddress = _FromAdd.Address;
        }
        //_ReplyMailAddress = _ReplyMailAddress.Replace("@energiosmaritime.com", "@energiossolutions.com");
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
                //--------------------------------
               
               

               if (Page.Request.QueryString["TicketReqPortCallId"] != null && Page.Request.QueryString["TicketReqPortCallId"].ToString() != "")
                {
                    _TicketReqPortCallId = Convert.ToInt32(Page.Request.QueryString["TicketReqPortCallId"].ToString());
                }
               else
                {
                    _PortCallId = Convert.ToInt32(Page.Request.QueryString["PortCallId"].ToString());
                }
                _CrewList = Page.Request.QueryString["CrewList"].ToString().Trim();
                //--------------------------------
                if (Request.QueryString["mode"] == "1")
                {
                    DataTable dtPort = Budget.getTable("select upper(replace(convert(varchar,eta,106),' ','-')) as ETA,p.portname from portcallheader pc inner join port p on p.portid=pc.portid where portcallid="  + _PortCallId.ToString()).Tables[0];
                    txtTo.Text = "";
                    hd1.Value = "";
                    row1.Style.Add("display","");
                    txtAttn.Text = "";
                    if (dtPort.Rows.Count > 0)
                    {
                        txtSubject.Text = "Crew changes at " + Convert.ToString(dtPort.Rows[0]["portname"]) + " On ( " + Convert.ToString(dtPort.Rows[0]["ETA"]) + " )";
                    }
                    else
                    {
                        txtSubject.Text = "";
                    }
                    MailMessageDetails_Port();
                    txtbody.InnerHtml = MyMessageBody;
                }
                else if (Request.QueryString["mode"] == "2")
                {
                        txtTo.Text = "";
                        hd1.Value = "";
                        row1.Style.Add("display", "none");
                        txtSubject.Text = "";// Convert.ToString(dt.Rows[0]["VesselName"]) + " - " + Convert.ToString(dt.Rows[0]["FromAirPort"]) + " > " + Convert.ToString(dt.Rows[0]["ToAirPort"]);
                        strFromTo = "";// Convert.ToString(dt.Rows[0]["FromAirPort"]) + " > " + Convert.ToString(dt.Rows[0]["ToAirPort"]);
                        MailMessageDetails_Travel();
                        txtbody.InnerHtml = MyMessageBody;
                }
                else if (Request.QueryString["mode"] == "4")
                {

                   
                    string sql = "SELECT top 1 TravelAgentEmail FROM TRAVELAGENT a with(nolock) inner join CrewPortCallSendTravelDetails b on a.TravelAgentId = b.TravelAgentId where b.crewid in (" + _CrewList + ") and b.PortCallid = " + _TicketReqPortCallId + " ";
                    DataTable dtSendTicketDetails = Budget.getTable(sql).Tables[0];
                    if (dtSendTicketDetails.Rows.Count > 0)
                    {
                        txtTo.Text = dtSendTicketDetails.Rows[0][0].ToString();
                    }
                    else
                    {
                        txtTo.Text = "";
                    }
                    string sql1 = "Select Email from UserLogin with(nolock) where LoginId = " + Convert.ToInt32(Session["loginid"].ToString());
                    DataTable dtCCMail = Budget.getTable(sql1).Tables[0];
                    if (dtCCMail.Rows.Count > 0)
                    {
                        txtCC.Text = dtCCMail.Rows[0][0].ToString();
                    }
                    else
                    {
                        txtCC.Text = "";
                    }

                    hd1.Value = "";
                    row1.Style.Add("display", "none");
                    txtSubject.Text = "Travel Booking Request";// Convert.ToString(dt.Rows[0]["VesselName"]) + " - " + Convert.ToString(dt.Rows[0]["FromAirPort"]) + " > " + Convert.ToString(dt.Rows[0]["ToAirPort"]);
                    strFromTo = "";// Convert.ToString(dt.Rows[0]["FromAirPort"]) + " > " + Convert.ToString(dt.Rows[0]["ToAirPort"]);
                   MailMessageDetails_TravelBookingRequest();
                    txtbody.InnerHtml = MyMessageBody;
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
        if (Convert.ToInt32(Request.QueryString["mode"]) == 4)
        {
            Response.Redirect("CrewPortcallSendTravelRequest.aspx");
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
            //MailAddress fromAddress = new MailAddress(MailSend.LoginUserEmailId(Convert.ToInt32(Session["loginid"].ToString())));
            MailAddress fromAddress = new MailAddress(ConfigurationManager.AppSettings["FromAddress"]);
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

        MyMessageBody = MyMessageBody + "<table border=0 width=680 align='left'>";
        MyMessageBody = MyMessageBody + "<tr><td colspan='2' class='InvoiceText' align='left'>";
        MyMessageBody = MyMessageBody + "Good Day, " + "<br><br>";
        MyMessageBody = MyMessageBody + "Please be advised that we will be arranging crew changes for above mentioned vessel at Santos when she calls on/about 22/June/2008.  Kindly find below the on/off-signers' particulars and flight details" + "<br></td></tr>";

        MyMessageBody = MyMessageBody + "<tr valign='top'>";
        MyMessageBody = MyMessageBody + "<td colspan='2' class='InvoiceText' align='left'><hr/><br/> <strong> On-signers :</strong><br>";
        MyMessageBody = MyMessageBody + "</td>";
        MyMessageBody = MyMessageBody + "</tr>";

        string Qry = "select CrewId,CrewFlag, " +
                    "( " +
                    "(select RankName from Rank where RankId in (select CurrentRankId from CrewPersonalDetails where CrewPersonalDetails.crewid=pcd.crewid))) as Rank, " +
                    "(select ranklevel from rank where rankid in (select CurrentRankId from CrewPersonalDetails where CrewPersonalDetails.crewid=pcd.crewid)) as ranklevel, " +
                    "(Select Firstname + ' ' + MiddleName + ' ' + Lastname from crewpersonaldetails where crewpersonaldetails.crewid=pcd.crewid ) as Name, " +
                    "(select CountryName from country where country.countryid in (select nationalityid from crewpersonaldetails where crewpersonaldetails.crewid=pcd.crewid)) as Nationality, " +
                    "(select Top 1 DocumentNumber from crewtraveldocument where crewtraveldocument.crewid=pcd.crewid and documentTypeid=2) as CDC, " +
                    "(select Top 1 dbo.[get_datefor_MailSend](Issuedate) from crewtraveldocument where crewtraveldocument.crewid=pcd.crewid and documentTypeid=2) as CDCI, " +
                    "(select Top 1 dbo.[get_datefor_MailSend](ExpiryDate) from crewtraveldocument where crewtraveldocument.crewid=pcd.crewid and documentTypeid=2) as CDCE, " +
                    "(select Top 1 DocumentNumber from crewtraveldocument where crewtraveldocument.crewid=pcd.crewid and documentTypeid=0) as Passport, " +
                    "(select Top 1 dbo.[get_datefor_MailSend](Issuedate) from crewtraveldocument where crewtraveldocument.crewid=pcd.crewid and documentTypeid=0) as DOI, " +
                    "(select Top 1 dbo.[get_datefor_MailSend](ExpiryDate) from crewtraveldocument where crewtraveldocument.crewid=pcd.crewid and documentTypeid=0) as DOE, " +
                    "(select Top 1 PLACEOFISSUE from crewtraveldocument where crewtraveldocument.crewid=pcd.crewid and documentTypeid=0) as PPlace, " +
                    "(select Top 1 PLACEOFISSUE from crewtraveldocument where crewtraveldocument.crewid=pcd.crewid and documentTypeid=2) as CPlace, " +
                    "(Select dbo.[get_datefor_MailSend](DateOfbirth) from crewpersonaldetails where crewpersonaldetails.crewid=pcd.crewid) as DOB, " +
                    "(Select PlaceOfbirth from crewpersonaldetails where crewpersonaldetails.crewid=pcd.crewid) as POB " +
                    "From PortCallDetail pcd where pcd.PortCallId=" + _PortCallId.ToString() + " and CrewFlag='I' " +
                    "and pcd.crewid in (" + _CrewList + ") order by ranklevel";
        DataTable dtSignOnMembers = Budget.getTable(Qry).Tables[0];

        for (int i = 0; i <= dtSignOnMembers.Rows.Count - 1; i++)
        {
            int x = i + 1;


            MyMessageBody = MyMessageBody + "<tr valign='top'>";
            MyMessageBody = MyMessageBody + "<td colspan='2' class='InvoiceText' align='left'>";
            MyMessageBody = MyMessageBody + "<table border=0 width='100%' align='left' height='145'>";

            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='3%'>" + x + ". </td>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='50%'>NAME - " + Convert.ToString(dtSignOnMembers.Rows[i]["Name"]) + "</td>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='10%'></td>";
            MyMessageBody = MyMessageBody + "</tr>";

            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left'></td>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left'>RANK - " + Convert.ToString(dtSignOnMembers.Rows[i]["Rank"]) + "</td>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left'></td>";
            MyMessageBody = MyMessageBody + "</tr>";

            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='2%'></td>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='50%'>NATIONALITY - " + Convert.ToString(dtSignOnMembers.Rows[i]["Nationality"]) + "</td>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='10%'></td>";
            MyMessageBody = MyMessageBody + "</tr>";

            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='2%'></td>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='50%'>DOB - " + Convert.ToString(dtSignOnMembers.Rows[i]["DOB"]) + "&nbsp;&nbsp;&nbsp; POB - " + Convert.ToString(dtSignOnMembers.Rows[i]["POB"]) + "</td>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='10%'></td>";
            MyMessageBody = MyMessageBody + "</tr>";

            //----------PASSPORT
            string DummyText = "";
            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td ></td>";

            DummyText = DummyText + "PP NO : " + Convert.ToString(dtSignOnMembers.Rows[i]["Passport"]) + "&nbsp;&nbsp;&nbsp;";
            if (dtSignOnMembers.Rows[i]["DOI"] == null)
            {
                DummyText = DummyText + "DOI : &nbsp;&nbsp;&nbsp;";
            }
            else
            {
                DummyText = DummyText + "DOI : " + Convert.ToString(dtSignOnMembers.Rows[i]["DOI"]) + "&nbsp;&nbsp;&nbsp;";
            }
            if (dtSignOnMembers.Rows[i]["DOE"] == null)
            {
                DummyText = DummyText + "DOE : ";
            }
            else
            {
                DummyText = DummyText + "DOE : " + Convert.ToString(dtSignOnMembers.Rows[i]["DOE"]) + "&nbsp;&nbsp;&nbsp;";
            }
            if (dtSignOnMembers.Rows[i]["PPLACE"] == null)
            {
                DummyText = DummyText + "PLACE ISSUED : &nbsp;&nbsp;&nbsp;";
            }
            else
            {
                DummyText = DummyText + "PLACE ISSUED : " + Convert.ToString(dtSignOnMembers.Rows[i]["PPLACE"]) + "</td>";
            }
            MyMessageBody = MyMessageBody + "<td >" + DummyText + "</td>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "</tr>";

            ////---------- CDC
            DummyText = "";
            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            DummyText = DummyText + "CDC NO : " + Convert.ToString(dtSignOnMembers.Rows[i]["CDC"]) + "&nbsp;&nbsp;&nbsp;";
            if (dtSignOnMembers.Rows[i]["CDCI"] == null)
            {
                DummyText = DummyText + "DOI : &nbsp;&nbsp;&nbsp;";
            }
            else
            {
                DummyText = DummyText + "DOI : " + Convert.ToString(dtSignOnMembers.Rows[i]["CDCI"]) + "&nbsp;&nbsp;&nbsp;";
            }
            if (dtSignOnMembers.Rows[i]["CDCE"] == null)
            {
                DummyText = DummyText + "DOE : ";
            }
            else
            {
                DummyText = DummyText + "DOE : " + Convert.ToString(dtSignOnMembers.Rows[i]["CDCE"]) + "&nbsp;&nbsp;&nbsp;";
            }

            if (dtSignOnMembers.Rows[i]["CPLACE"] == null)
            {
                DummyText = DummyText + "PLACE ISSUED : &nbsp;&nbsp;&nbsp;";
            }
            else
            {
                DummyText = DummyText + "PLACE ISSUED : " + Convert.ToString(dtSignOnMembers.Rows[i]["CPLACE"]) + "</td>";
            }

            MyMessageBody = MyMessageBody + "<td >" + DummyText + "</td>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "</tr>";

            ////---
            //MyMessageBody = MyMessageBody + "<tr>";
            //MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='33%'>CDC:&nbsp;&nbsp;" + Convert.ToString(dtSignOnMembers.Rows[i]["CDC"]) + "</td>";
            //if (dtSignOnMembers.Rows[i]["DOI"] == null)
            //{
            //    MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='33%'>DOB:&nbsp;&nbsp;</td>";
            //}
            //else
            //{
            //    MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='33%'>DOB: &nbsp;&nbsp;" + Convert.ToString(dtSignOnMembers.Rows[i]["DOB"]) + "</td>";
            //}
            //MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='33%'>POB:&nbsp;&nbsp;" + Convert.ToString(dtSignOnMembers.Rows[i]["POB"]) + "</td>";

            //MyMessageBody = MyMessageBody + "</tr>";

            //MyMessageBody = MyMessageBody + "<tr>";
            //MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='33%'>PP NO: &nbsp;&nbsp;" + Convert.ToString(dtSignOnMembers.Rows[i]["Passport"]) + "</td>";
            //if (dtSignOnMembers.Rows[i]["DOI"] == null)
            //{
            //    MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='33%'>DOI:&nbsp;&nbsp;</td>";
            //}
            //else
            //{
            //    MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='33%'>DOI:&nbsp;&nbsp;" + Convert.ToString(dtSignOnMembers.Rows[i]["DOI"]) + "</td>";
            //}
            //if (dtSignOnMembers.Rows[i]["DOI"] == null)
            //{
            //    MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='33%'>DOE:&nbsp;&nbsp;</td>";
            //}
            //else
            //{
            //    MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='33%'>DOE:&nbsp;&nbsp;" + Convert.ToString(dtSignOnMembers.Rows[i]["DOE"]) + "</td>";
            //}

            //MyMessageBody = MyMessageBody + "</tr>";

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

        string Qry1 = "select CrewId,CrewFlag, " +
            "(select RankName from Rank where RankId in (select currentrankid from crewpersonaldetails where crewpersonaldetails.crewid=pcd.crewid)) as Rank, " +
            "(select RankLevel from Rank where RankId in (select currentrankid from crewpersonaldetails where crewpersonaldetails.crewid=pcd.crewid)) as RankLevel,  " +
            "(Select Firstname + ' ' + MiddleName + ' ' + Lastname from crewpersonaldetails where crewpersonaldetails.crewid=pcd.crewid ) as Name, " +
            "(select CountryName from country where country.countryid in (select nationalityid from crewpersonaldetails where crewpersonaldetails.crewid=pcd.crewid)) as Nationality, " +
            "(select Top 1 DocumentNumber from crewtraveldocument where crewtraveldocument.crewid=pcd.crewid and documentTypeid=2) as CDC, " +
            "(select Top 1 dbo.[get_datefor_MailSend](Issuedate) from crewtraveldocument where crewtraveldocument.crewid=pcd.crewid and documentTypeid=2) as CDCI, " +
            "(select Top 1 dbo.[get_datefor_MailSend](ExpiryDate) from crewtraveldocument where crewtraveldocument.crewid=pcd.crewid and documentTypeid=2) as CDCE, " +
            "(select Top 1 DocumentNumber from crewtraveldocument where crewtraveldocument.crewid=pcd.crewid and documentTypeid=0) as Passport, " +
            "(select Top 1 dbo.[get_datefor_MailSend](Issuedate) from crewtraveldocument where crewtraveldocument.crewid=pcd.crewid and documentTypeid=0) as DOI, " +
            "(select Top 1 dbo.[get_datefor_MailSend](ExpiryDate) from crewtraveldocument where crewtraveldocument.crewid=pcd.crewid and documentTypeid=0) as DOE, " +
            "(select Top 1 PLACEOFISSUE from crewtraveldocument where crewtraveldocument.crewid=pcd.crewid and documentTypeid=0) as PPlace, " +
            "(select Top 1 PLACEOFISSUE from crewtraveldocument where crewtraveldocument.crewid=pcd.crewid and documentTypeid=2) as CPlace, " +
            "(Select dbo.[get_datefor_MailSend](DateOfbirth) from crewpersonaldetails where crewpersonaldetails.crewid=pcd.crewid) as DOB, " +
            "(Select PlaceOfbirth from crewpersonaldetails where crewpersonaldetails.crewid=pcd.crewid) as POB " +
            "From PortCallDetail pcd where pcd.PortCallId=" + _PortCallId.ToString() + " and CrewFlag='O' " +
            "and pcd.crewid in (" + _CrewList + ")  " +
            "order by RankLevel ";

        DataTable dtSignOffmembers = Budget.getTable(Qry1).Tables[0];

        for (int i = 0; i <= dtSignOffmembers.Rows.Count - 1; i++)
        {
            int x = i + 1;
            MyMessageBody = MyMessageBody + "<tr valign='top'>";
            MyMessageBody = MyMessageBody + "<td colspan='2' class='InvoiceText' align='left'>";

            MyMessageBody = MyMessageBody + "<table border=0 width='100%' align='left' height='150'>";

            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='3%'>" + x + ". </td>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='50%'>NAME - " + Convert.ToString(dtSignOffmembers.Rows[i]["Name"]) + "</td>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='10%'></td>";
            MyMessageBody = MyMessageBody + "</tr>";

            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left'></td>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left'>RANK - " + Convert.ToString(dtSignOffmembers.Rows[i]["Rank"]) + "</td>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left'></td>";
            MyMessageBody = MyMessageBody + "</tr>";

            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='2%'></td>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='50%'>NATIONALITY - " + Convert.ToString(dtSignOffmembers.Rows[i]["Nationality"]) + "</td>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='10%'></td>";
            MyMessageBody = MyMessageBody + "</tr>";

            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='2%'></td>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='50%'>DOB - " + Convert.ToString(dtSignOffmembers.Rows[i]["DOB"]) + "&nbsp;&nbsp;&nbsp; POB - " + Convert.ToString(dtSignOffmembers.Rows[i]["POB"]) + "</td>";
            MyMessageBody = MyMessageBody + "<td class='InvoiceText' align='left' width='10%'></td>";
            MyMessageBody = MyMessageBody + "</tr>";

            //----------PASSPORT
            string DummyText = "";
            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td ></td>";

            DummyText = DummyText + "PP NO : " + Convert.ToString(dtSignOffmembers.Rows[i]["Passport"]) + "&nbsp;&nbsp;&nbsp;";
            if (dtSignOffmembers.Rows[i]["DOI"] == null)
            {
                DummyText = DummyText + "DOI : &nbsp;&nbsp;&nbsp;";
            }
            else
            {
                DummyText = DummyText + "DOI : " + Convert.ToString(dtSignOffmembers.Rows[i]["DOI"]) + "&nbsp;&nbsp;&nbsp;";
            }
            if (dtSignOffmembers.Rows[i]["DOE"] == null)
            {
                DummyText = DummyText + "DOE : ";
            }
            else
            {
                DummyText = DummyText + "DOE : " + Convert.ToString(dtSignOffmembers.Rows[i]["DOE"]) + "&nbsp;&nbsp;&nbsp;";
            }
            if (dtSignOffmembers.Rows[i]["PPLACE"] == null)
            {
                DummyText = DummyText + "PLACE ISSUED : &nbsp;&nbsp;&nbsp;";
            }
            else
            {
                DummyText = DummyText + "PLACE ISSUED : " + Convert.ToString(dtSignOffmembers.Rows[i]["PPLACE"]) + "</td>";
            }
            MyMessageBody = MyMessageBody + "<td >" + DummyText + "</td>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "</tr>";

            ////---------- CDC
            DummyText = "";
            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            DummyText = DummyText + "CDC NO : " + Convert.ToString(dtSignOffmembers.Rows[i]["CDC"]) + "&nbsp;&nbsp;&nbsp;";
            if (dtSignOffmembers.Rows[i]["CDCI"] == null)
            {
                DummyText = DummyText + "DOI : &nbsp;&nbsp;&nbsp;";
            }
            else
            {
                DummyText = DummyText + "DOI : " + Convert.ToString(dtSignOffmembers.Rows[i]["CDCI"]) + "&nbsp;&nbsp;&nbsp;";
            }
            if (dtSignOffmembers.Rows[i]["CDCE"] == null)
            {
                DummyText = DummyText + "DOE : ";
            }
            else
            {
                DummyText = DummyText + "DOE : " + Convert.ToString(dtSignOffmembers.Rows[i]["CDCE"]) + "&nbsp;&nbsp;&nbsp;";
            }

            if (dtSignOffmembers.Rows[i]["CPLACE"] == null)
            {
                DummyText = DummyText + "PLACE ISSUED : &nbsp;&nbsp;&nbsp;";
            }
            else
            {
                DummyText = DummyText + "PLACE ISSUED : " + Convert.ToString(dtSignOffmembers.Rows[i]["CPLACE"]) + "</td>";
            }

            MyMessageBody = MyMessageBody + "<td >" + DummyText + "</td>";
            MyMessageBody = MyMessageBody + "<td ></td>";
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

        MyMessageBody = MyMessageBody + "<table border=0 width=680 align='left' >";
        MyMessageBody = MyMessageBody + "<tr><td colspan='2' class='InvoiceText' align='left'>";
        MyMessageBody = MyMessageBody + "Hi , " + "<br><br>";
        MyMessageBody = MyMessageBody + "Please book and quote the airfare for the following:"+"<br></td></tr>";

       string Qry= "select crewid, Firstname + ' ' + MiddleName + ' ' + Lastname as Name,Firstname,Lastname,CrewNumber, " +
        "(select RankName from Rank where RankId in (case when isnull(ContractId,0) > 0 then (select newRankid from CrewContractHeader where CrewContractHeader.Contractid=CrewPersonalDetails.ContractId) else Currentrankid end)) as RankCode, " +
        "(select Ranklevel from Rank where RankId in (case when isnull(ContractId,0) > 0 then (select newRankid from CrewContractHeader where CrewContractHeader.Contractid=CrewPersonalDetails.ContractId) else Currentrankid end)) as RankLevel, " +
        "(Select NationalityName from Country where Country.CountryId=crewpersonaldetails.Nationalityid) as Nationality, " +
        "(select Top 1 DocumentNumber from crewtraveldocument where crewtraveldocument.crewid=crewpersonaldetails.crewid and documentTypeid=2) as CDC, " +
        "(select Top 1 dbo.[get_datefor_MailSend](Issuedate) from crewtraveldocument where crewtraveldocument.crewid=crewpersonaldetails.crewid and documentTypeid=2) as CDCI, " +
        "(select Top 1 dbo.[get_datefor_MailSend](ExpiryDate) from crewtraveldocument where crewtraveldocument.crewid=crewpersonaldetails.crewid and documentTypeid=2) as CDCE, " +
        "dbo.[get_datefor_MailSend](DateOfbirth) as DOB, " +
        "PlaceOfBirth as POB, " +
        "(select Top 1 DocumentNumber from crewtraveldocument where crewtraveldocument.crewid=crewpersonaldetails.crewid and documentTypeid=0) as PPNo, " +
        "(select Top 1 PLACEOFISSUE from crewtraveldocument where crewtraveldocument.crewid=crewpersonaldetails.crewid and documentTypeid=0) as PPlace, " +
        "(select Top 1 PLACEOFISSUE from crewtraveldocument where crewtraveldocument.crewid=crewpersonaldetails.crewid and documentTypeid=2) as CPlace, " +
        "(select Top 1 dbo.[get_datefor_MailSend](Issuedate) from crewtraveldocument where crewtraveldocument.crewid=crewpersonaldetails.crewid and documentTypeid=0) as DOI, " +
        "(select Top 1 dbo.[get_datefor_MailSend](ExpiryDate) from crewtraveldocument where crewtraveldocument.crewid=crewpersonaldetails.crewid and documentTypeid=0) as DOE,'' as DDate " +
        "from crewpersonaldetails where crewpersonaldetails.crewid  " +
        "in(" + _CrewList + ") " +
        "order by RankLevel ";

        DataTable dtTravelBookingDetails = Budget.getTable(Qry).Tables[0];  

        for (int i = 0; i <= dtTravelBookingDetails.Rows.Count - 1; i++)
        {
            int x = i + 1;
            MyMessageBody = MyMessageBody + "<tr valign='top'>";
            MyMessageBody = MyMessageBody + "<td colspan='2' class='InvoiceText' align='left' style='font-size:11px;'>";
            MyMessageBody = MyMessageBody + "<table border=0 width='100%' align='left' height='137' style='font-size:13px'>";

            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td align='left' width='30px'>" + x + "." +"</td>";
            MyMessageBody = MyMessageBody + "<td align='left' width='350px'>FIRST NAME - " + Convert.ToString(dtTravelBookingDetails.Rows[i]["Firstname"]) + "</td>";
            MyMessageBody = MyMessageBody + "<td align='left' width='5px'></td>";
            MyMessageBody = MyMessageBody + "</tr>";

            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "<td >LAST NAME - " + Convert.ToString(dtTravelBookingDetails.Rows[i]["Lastname"]) + "</td>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "</tr>";

            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "<td >CREW # - " + Convert.ToString(dtTravelBookingDetails.Rows[i]["CrewNumber"]) + "</td>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "</tr>";

            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "<td >RANK - " + Convert.ToString(dtTravelBookingDetails.Rows[i]["RankCode"]) + "</td>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "</tr>";

            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "<td >NATIONALITY - " + Convert.ToString(dtTravelBookingDetails.Rows[i]["Nationality"]) + "</td>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "</tr>";

            string DummyText = "";
            //-----------
            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            
            if (dtTravelBookingDetails.Rows[i]["DOI"] == null)
            {
                DummyText = DummyText + "DOB : &nbsp;&nbsp;&nbsp;POB : " + Convert.ToString(dtTravelBookingDetails.Rows[i]["POB"]) + "";
            }
            else
            {
                DummyText = DummyText + "DOB : " + Convert.ToString(dtTravelBookingDetails.Rows[i]["DOB"]) + "&nbsp;&nbsp;&nbsp;POB : " + Convert.ToString(dtTravelBookingDetails.Rows[i]["POB"]) + "";
            }
            MyMessageBody = MyMessageBody + "<td >" + DummyText + "</td>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "</tr>";

            //----------PASSPORT
            DummyText = "";
            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td ></td>";

            DummyText = DummyText + "PP NO : " + Convert.ToString(dtTravelBookingDetails.Rows[i]["PPNo"]) + "&nbsp;&nbsp;&nbsp;";
            if (dtTravelBookingDetails.Rows[i]["DOI"] == null)
            {
                DummyText = DummyText + "DOI : &nbsp;&nbsp;&nbsp;";
            }
            else
            {
                DummyText = DummyText + "DOI : " + Convert.ToString(dtTravelBookingDetails.Rows[i]["DOI"]) + "&nbsp;&nbsp;&nbsp;";
            }
            if (dtTravelBookingDetails.Rows[i]["DOE"] == null)
            {
                DummyText = DummyText + "DOE : ";
            }
            else
            {
                DummyText = DummyText + "DOE : " + Convert.ToString(dtTravelBookingDetails.Rows[i]["DOE"]) + "&nbsp;&nbsp;&nbsp;";
            }
            if (dtTravelBookingDetails.Rows[i]["PPLACE"] == null)
            {
                DummyText = DummyText + "PLACE ISSUED : &nbsp;&nbsp;&nbsp;";
            }
            else
            {
                DummyText = DummyText + "PLACE ISSUED : " + Convert.ToString(dtTravelBookingDetails.Rows[i]["PPLACE"]) + "</td>";
            }
            MyMessageBody = MyMessageBody + "<td >" + DummyText + "</td>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "</tr>";

            //---------- CDC
            DummyText = "";
            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            DummyText = DummyText + "CDC NO : " + Convert.ToString(dtTravelBookingDetails.Rows[i]["CDC"]) + "&nbsp;&nbsp;&nbsp;";
            if (dtTravelBookingDetails.Rows[i]["CDCI"] == null)
            {
                DummyText = DummyText + "DOI : &nbsp;&nbsp;&nbsp;";
            }
            else
            {
                DummyText = DummyText + "DOI : " + Convert.ToString(dtTravelBookingDetails.Rows[i]["CDCI"]) + "&nbsp;&nbsp;&nbsp;";
            }
            if (dtTravelBookingDetails.Rows[i]["CDCE"] == null)
            {
                DummyText = DummyText + "DOE : ";
            }
            else
            {
                DummyText = DummyText + "DOE : " + Convert.ToString(dtTravelBookingDetails.Rows[i]["CDCE"]) + "&nbsp;&nbsp;&nbsp;";
            }

            if (dtTravelBookingDetails.Rows[i]["CPLACE"] == null)
            {
                DummyText = DummyText + "PLACE ISSUED : &nbsp;&nbsp;&nbsp;";
            }
            else
            {
                DummyText = DummyText + "PLACE ISSUED : " + Convert.ToString(dtTravelBookingDetails.Rows[i]["CPLACE"]) + "</td>";
            }

            MyMessageBody = MyMessageBody + "<td >" + DummyText + "</td>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "</tr>";


            //MyMessageBody = MyMessageBody + "<tr>";
            //MyMessageBody = MyMessageBody + "<td ></td>";
            //MyMessageBody = MyMessageBody + "<td >Itinerary:</td>";
            //MyMessageBody = MyMessageBody + "<td >" + strFromTo + "</td>";
            //MyMessageBody = MyMessageBody + "</tr>";

            //MyMessageBody = MyMessageBody + "<tr>";
            //MyMessageBody = MyMessageBody + "<td ></td>";
            //MyMessageBody = MyMessageBody + "<td >Departure Date:</td>";
            //MyMessageBody = MyMessageBody + "<td >" + Convert.ToString(dtTravelBookingDetails.Rows[i]["DDate"]) + "</td>";
            //MyMessageBody = MyMessageBody + "</tr>";

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


    public void MailMessageDetails_TravelBookingRequest()
    {
        string accountcompany = "";
        DataTable dtAccountCompany = Budget.getTable("Select ac.[Company Name] As AccountCompany from Vessel v with(nolock) Inner join AccountCompany ac with(nolock) on v.AccontCompany = ac.Company inner join  portcallheader pc with(nolock) on v.VesselId = pc.VesselId where pc.PortCallId=" + _PortCallId).Tables[0];
        if (dtAccountCompany.Rows.Count > 0)
        {
            accountcompany = dtAccountCompany.Rows[0]["AccountCompany"].ToString();
        }

        MyMessageBody = MyMessageBody + "<table border=0 width=680 align='left' >";
        MyMessageBody = MyMessageBody + "<tr><td colspan='2' class='InvoiceText' align='left'>";
        MyMessageBody = MyMessageBody + "Hi , " + "<br><br>";
        MyMessageBody = MyMessageBody + "Please provide booking and airfare for below crew members :" + "<br></td></tr>";

        string Qry = "select crewpersonaldetails.crewid, Firstname + ' ' + MiddleName + ' ' + Lastname as Name,Firstname,Lastname,CrewNumber, " +
         "(select RankName from Rank where RankId in (case when isnull(ContractId,0) > 0 then (select newRankid from CrewContractHeader where CrewContractHeader.Contractid=CrewPersonalDetails.ContractId) else Currentrankid end)) as RankCode, " +
         "(select Ranklevel from Rank where RankId in (case when isnull(ContractId,0) > 0 then (select newRankid from CrewContractHeader where CrewContractHeader.Contractid=CrewPersonalDetails.ContractId) else Currentrankid end)) as RankLevel, " +
         "(Select NationalityName from Country where Country.CountryId=crewpersonaldetails.Nationalityid) as Nationality, " +
         "(select Top 1 DocumentNumber from crewtraveldocument where crewtraveldocument.crewid=crewpersonaldetails.crewid and documentTypeid=2) as CDC, " +
         "(select Top 1 dbo.[get_datefor_MailSend](Issuedate) from crewtraveldocument where crewtraveldocument.crewid=crewpersonaldetails.crewid and documentTypeid=2) as CDCI, " +
         "(select Top 1 dbo.[get_datefor_MailSend](ExpiryDate) from crewtraveldocument where crewtraveldocument.crewid=crewpersonaldetails.crewid and documentTypeid=2) as CDCE, " +
         "dbo.[get_datefor_MailSend](DateOfbirth) as DOB, " +
         "PlaceOfBirth as POB, " +
         "(select Top 1 DocumentNumber from crewtraveldocument where crewtraveldocument.crewid=crewpersonaldetails.crewid and documentTypeid=0) as PPNo, " +
         "(select Top 1 PLACEOFISSUE from crewtraveldocument where crewtraveldocument.crewid=crewpersonaldetails.crewid and documentTypeid=0) as PPlace, " +
         "(select Top 1 PLACEOFISSUE from crewtraveldocument where crewtraveldocument.crewid=crewpersonaldetails.crewid and documentTypeid=2) as CPlace, " +
         "(select Top 1 dbo.[get_datefor_MailSend](Issuedate) from crewtraveldocument where crewtraveldocument.crewid=crewpersonaldetails.crewid and documentTypeid=0) as DOI, " +
         "(select Top 1 dbo.[get_datefor_MailSend](ExpiryDate) from crewtraveldocument where crewtraveldocument.crewid=crewpersonaldetails.crewid and documentTypeid=0) as DOE,'' as DDate,cpc.FromAirport As Origin,cpc.ToAirPort As Destination, REPLACE(CONVERT(CHAR(11), cpc.DeptDate, 106),' ',' - ')  As  DeptDate, cpc.Remarks " +
         "from crewpersonaldetails with(nolock) inner join  CrewPortCallSendTravelDetails cpc with(nolock) on crewpersonaldetails.crewid = cpc.crewid where crewpersonaldetails.crewid  " +
         "in (" + _CrewList + ") and cpc.PortCallid = " + _TicketReqPortCallId +
         "order by RankLevel ";

        DataTable dtTravelBookingDetails = Budget.getTable(Qry).Tables[0];

        for (int i = 0; i <= dtTravelBookingDetails.Rows.Count - 1; i++)
        {
            int x = i + 1;
            MyMessageBody = MyMessageBody + "<tr valign='top'>";
            MyMessageBody = MyMessageBody + "<td colspan='2' class='InvoiceText' align='left' style='font-size:11px;'>";
            MyMessageBody = MyMessageBody + "<table border=0 width='100%' align='left' height='137' style='font-size:13px'>";

            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td align='left' width='30px'>" + x + "." + "</td>";
            MyMessageBody = MyMessageBody + "<td align='left' width='350px'>FIRST NAME - " + Convert.ToString(dtTravelBookingDetails.Rows[i]["Firstname"]) + "</td>";
            MyMessageBody = MyMessageBody + "<td align='left' width='5px'></td>";
            MyMessageBody = MyMessageBody + "</tr>";

            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "<td >LAST NAME - " + Convert.ToString(dtTravelBookingDetails.Rows[i]["Lastname"]) + "</td>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "</tr>";

            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "<td >CREW # - " + Convert.ToString(dtTravelBookingDetails.Rows[i]["CrewNumber"]) + "</td>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "</tr>";

            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "<td >RANK - " + Convert.ToString(dtTravelBookingDetails.Rows[i]["RankCode"]) + "</td>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "</tr>";

            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "<td >NATIONALITY - " + Convert.ToString(dtTravelBookingDetails.Rows[i]["Nationality"]) + "</td>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "</tr>";

            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "<td >Origin Airport - <span style='color:red;'> " + Convert.ToString(dtTravelBookingDetails.Rows[i]["Origin"]) + "</span> </td>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "</tr>";

            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "<td >Destination Airport - <span style='color:red;'> " + Convert.ToString(dtTravelBookingDetails.Rows[i]["Destination"]) + "</span> </td>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "</tr>";

            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "<td >Departure Date - <span style='color:red;'> " + Convert.ToString(dtTravelBookingDetails.Rows[i]["DeptDate"]) + "</span> </td>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "</tr>";

            string DummyText = "";
            //-----------
            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td ></td>";

            if (dtTravelBookingDetails.Rows[i]["DOI"] == null)
            {
                DummyText = DummyText + "DOB : &nbsp;&nbsp;&nbsp;POB : " + Convert.ToString(dtTravelBookingDetails.Rows[i]["POB"]) + "";
            }
            else
            {
                DummyText = DummyText + "DOB : " + Convert.ToString(dtTravelBookingDetails.Rows[i]["DOB"]) + "&nbsp;&nbsp;&nbsp;POB : " + Convert.ToString(dtTravelBookingDetails.Rows[i]["POB"]) + "";
            }
            MyMessageBody = MyMessageBody + "<td >" + DummyText + "</td>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "</tr>";

            //----------PASSPORT
            DummyText = "";
            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td ></td>";

            DummyText = DummyText + "PP NO : " + Convert.ToString(dtTravelBookingDetails.Rows[i]["PPNo"]) + "&nbsp;&nbsp;&nbsp;";
            if (dtTravelBookingDetails.Rows[i]["DOI"] == null)
            {
                DummyText = DummyText + "DOI : &nbsp;&nbsp;&nbsp;";
            }
            else
            {
                DummyText = DummyText + "DOI : " + Convert.ToString(dtTravelBookingDetails.Rows[i]["DOI"]) + "&nbsp;&nbsp;&nbsp;";
            }
            if (dtTravelBookingDetails.Rows[i]["DOE"] == null)
            {
                DummyText = DummyText + "DOE : ";
            }
            else
            {
                DummyText = DummyText + "DOE : " + Convert.ToString(dtTravelBookingDetails.Rows[i]["DOE"]) + "&nbsp;&nbsp;&nbsp;";
            }
            if (dtTravelBookingDetails.Rows[i]["PPLACE"] == null)
            {
                DummyText = DummyText + "PLACE ISSUED : &nbsp;&nbsp;&nbsp;";
            }
            else
            {
                DummyText = DummyText + "PLACE ISSUED : " + Convert.ToString(dtTravelBookingDetails.Rows[i]["PPLACE"]) + "</td>";
            }
            MyMessageBody = MyMessageBody + "<td >" + DummyText + "</td>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "</tr>";

            //---------- CDC
            DummyText = "";
            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            DummyText = DummyText + "CDC NO : " + Convert.ToString(dtTravelBookingDetails.Rows[i]["CDC"]) + "&nbsp;&nbsp;&nbsp;";
            if (dtTravelBookingDetails.Rows[i]["CDCI"] == null)
            {
                DummyText = DummyText + "DOI : &nbsp;&nbsp;&nbsp;";
            }
            else
            {
                DummyText = DummyText + "DOI : " + Convert.ToString(dtTravelBookingDetails.Rows[i]["CDCI"]) + "&nbsp;&nbsp;&nbsp;";
            }
            if (dtTravelBookingDetails.Rows[i]["CDCE"] == null)
            {
                DummyText = DummyText + "DOE : ";
            }
            else
            {
                DummyText = DummyText + "DOE : " + Convert.ToString(dtTravelBookingDetails.Rows[i]["CDCE"]) + "&nbsp;&nbsp;&nbsp;";
            }

            if (dtTravelBookingDetails.Rows[i]["CPLACE"] == null)
            {
                DummyText = DummyText + "PLACE ISSUED : &nbsp;&nbsp;&nbsp;";
            }
            else
            {
                DummyText = DummyText + "PLACE ISSUED : " + Convert.ToString(dtTravelBookingDetails.Rows[i]["CPLACE"]) + "</td>";
            }

            MyMessageBody = MyMessageBody + "<td >" + DummyText + "</td>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "</tr>";

            DummyText = "";
            MyMessageBody = MyMessageBody + "<tr>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            DummyText = DummyText + "Remarks : " + Convert.ToString(dtTravelBookingDetails.Rows[i]["Remarks"]) + "&nbsp;&nbsp;&nbsp;";
            MyMessageBody = MyMessageBody + "<td >" + DummyText + "</td>";
            MyMessageBody = MyMessageBody + "<td ></td>";
            MyMessageBody = MyMessageBody + "</tr>";

            //MyMessageBody = MyMessageBody + "<tr>";
            //MyMessageBody = MyMessageBody + "<td ></td>";
            //MyMessageBody = MyMessageBody + "<td >Itinerary:</td>";
            //MyMessageBody = MyMessageBody + "<td >" + strFromTo + "</td>";
            //MyMessageBody = MyMessageBody + "</tr>";

            //MyMessageBody = MyMessageBody + "<tr>";
            //MyMessageBody = MyMessageBody + "<td ></td>";
            //MyMessageBody = MyMessageBody + "<td >Departure Date:</td>";
            //MyMessageBody = MyMessageBody + "<td >" + Convert.ToString(dtTravelBookingDetails.Rows[i]["DDate"]) + "</td>";
            //MyMessageBody = MyMessageBody + "</tr>";

            MyMessageBody = MyMessageBody + "</table>";


            MyMessageBody = MyMessageBody + "</td>";
            MyMessageBody = MyMessageBody + "</tr>";
        }


        MyMessageBody = MyMessageBody + "<tr valign='top'>";
        MyMessageBody = MyMessageBody + "<td colspan='2' class='InvoiceText' align='left'><br>";
        MyMessageBody = MyMessageBody + "Thanks<br><br><font color=000080 size=2 face=Century Gothic>Best Regards<font><br><font color=000080 size=2 face=Century Gothic>" + Session["UserName"].ToString() + "</font><br><font color=000080 size=2 face=Century Gothic><strong> " + accountcompany + "</strong></font><br><font color=000080 size=2 face=Century Gothic>As owner's agent</font><br>";
        //MyMessageBody = MyMessageBody + "<font color=000080 size=2 face=Century Gothic>78 Shenton Way #13-01</font><br><font color=000080 size=2 face=Century Gothic>Lippo Centre Singapore 079120</font><br><font color=000080 size=2 face=Century Gothic>Board Number:  +65 - 63041770</font><br><font color=000080 size=2 face=Century Gothic>Direct Number:  +65 - 63041795</font><br><font color=000080 size=2 face=Century Gothic>Mobile Number: +65 - 81811795</font><br>";
        //MyMessageBody = MyMessageBody + "<font color=000080 size=2 face=Century Gothic>Fax Number:  +65 - 62207988</font><br>";
        MyMessageBody = MyMessageBody + "<a href='mailto:" + Session["EmailAddress"].ToString() + "' runat='server'><font color=000080 size=2 face=Century Gothic>E-mail: " + Session["EmailAddress"].ToString() + "</font></a>";


        MyMessageBody = MyMessageBody + "</tr>";

        MyMessageBody = MyMessageBody + "</table>";

    }

    protected void btnGoBack_Click(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "travel", "window.open('CrewChange.aspx')", true);
        Response.Redirect("~/Modules/HRD/CrewOperation/CrewChange.aspx");
    }
}
