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
using System.Net.Mail; 

public partial class CrewOperation_SendPortAgentMail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (Page.IsPostBack == false)
        {
          //  this.txtSender.Text = ConfigurationManager.AppSettings["FromAddress"].ToString();
            this.txtSender.Text = MailSend.LoginUserEmailId(Convert.ToInt32(Session["loginid"].ToString()));
            this.txtReceiver.Text = Session["MailTo"].ToString();
            this.txtSubject.Text = Session["Subject"].ToString();
             txtbody.InnerHtml = Session["Body"].ToString();
  

        }
    }
    private void sendmail()
    {

        try
        {
            MailMessage message = new MailMessage();
            SmtpClient smtpclient = new SmtpClient();
            //*********** From address is of user 
            //MailAddress fromaddress = new MailAddress(this.txtSender.Text, ConfigurationManager.AppSettings["FromName"]);
            //message.From = fromaddress;
            MailAddress fromaddress = new MailAddress(MailSend.LoginUserEmailId(Convert.ToInt32(Session["loginid"].ToString())));
            message.From = fromaddress;
            //********
            message.To.Add(this.txtReceiver.Text);
            message.Subject = this.txtSubject.Text;
           
            message.IsBodyHtml = true;
            message.Body = txtbody.InnerText; 
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            smtpclient.Send(message);
            Label1.Text = "Mail Send Successfully";
        }
        catch (SystemException es)
        {
            string str = es.Message;
        }


    }

    protected void bin_sendmail_Click(object sender, EventArgs e)
    {
        sendmail();
    }
}
