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
using System.IO;
using ShipSoft.CrewManager.Operational; 

using System.Net;
using System.Net.Mail; 
public partial class CrewRecord_SendCrewDocuments : System.Web.UI.Page
{
    string crewid;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (Page.IsPostBack == false)
        {
            
                crewid = Session["CrewId"].ToString();
                this.txtSender.Text = MailSend.LoginUserEmailId(Convert.ToInt32(Session["loginid"].ToString()));
               // this.txtSender.Text=ConfigurationManager.AppSettings["FromAddress"].ToString();

                DataTable dt = ((DataTable)Session["mailattachments"]);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr;
                    dr = dt.Rows[i];
                    UtilityManager um = new UtilityManager();
                    string str = um.DownloadFileFromServer(dr["imagepath"].ToString(), dr["mode"].ToString());
                    string str1 = dt.Rows[i]["doctype"].ToString() + "-" + dt.Rows[i]["document"].ToString();
                    this.lstattachment.Items.Add(new ListItem(str1, str));
                    
                }
            }
            
        }
    protected void bin_sendmail_Click(object sender, EventArgs e)
    {
       sendmail(); 
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
        _MailMessage.From = _FromAdd;
        _SmtpClient.EnableSsl = true;

        if (_ReplyMailAddress.Trim() == "emanager@energiossolutions.com")
        {
            _ReplyMailAddress = _FromAdd.Address;
        }
       // _ReplyMailAddress = _ReplyMailAddress.Replace("@energiosmaritime.com", "@energiossolutions.com");
        _MailMessage.ReplyTo = new MailAddress(_ReplyMailAddress);
    }


    private void sendmail()
    { 
           
        try
        {
            MailMessage message=new MailMessage(); 
            SmtpClient smtpclient=new SmtpClient();
           // MailAddress fromaddress = new MailAddress(this.txtSender.Text, ConfigurationManager.AppSettings["FromName"]);
            MailAddress fromaddress = new MailAddress(MailSend.LoginUserEmailId(Convert.ToInt32(Session["loginid"].ToString())));
            message.From = fromaddress;

            SetMails(smtpclient, message, fromaddress.Address);

            message.To.Add(this.txtReceiver.Text);
            message.Subject = this.txtSubject.Text ;
            for (int i=0;i<this.lstattachment.Items.Count;i++)
            {
            message.Attachments.Add(new Attachment(Server.MapPath(lstattachment.Items[i].Value.ToString())));
            }     
            message.IsBodyHtml = true;
            message.Body = txtBody.Text;
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
           
            smtpclient.Send(message);
            Label1.Text = "Mail Send Successfully";
            }
        catch(SystemException es)
        {
            string str= es.Message; 
        }
        
    
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        //Response.Redirect("CrewSearch.aspx");
        Response.Redirect("CrewDetails.aspx");
    }
}

