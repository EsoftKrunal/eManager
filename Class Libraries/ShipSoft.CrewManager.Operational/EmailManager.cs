using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mail; 
using System.Configuration;
using System.IO;
using System.Web;
using System.Data;

namespace ShipSoft.CrewManager.Operational
{
    public class EmailManager
    {
        //private bool _issent;

        //public EmailManager()
        //{

        //}

        //public void Send(EmailContents emailcontents)
        //{
        //    MailMessage mail = new MailMessage(); 
        //    mail.To = emailcontents.To;
        //    mail.From = FromAddress;
        //    mail.Subject = emailcontents.Subject;
        //    mail.Body = emailcontents.Body;

        //    MailAttachment attachment = null; 
			
        //    // Attaches a new attachment contained in the List
            
        //    attachment = new MailAttachment("");
        //    mail.Attachments.Add(attachment);
						
        //            }
										
        //        }
				            			
        //        //mail.Attachments.Add(attachment); 
        //        SmtpMail.SmtpServer = "localhost"; 
        //        SmtpMail.Send(mail); 
            
        //    SmtpClient client = new SmtpClient(SMTPServerName);
        //    client.UseDefaultCredentials = true;
        //    MailAddress from = new MailAddress(FromAddress, FromName);
        //    MailAddress to = new MailAddress(emailcontents.To);

        //    MailMessage message = new MailMessage(from, to);

        //    message.Subject = emailcontents.Subject;
        //    message.Body = emailcontents.Body;
        //    message.IsBodyHtml = true;

        //    try
        //    {
        //        client.Send(message);
        //        IsSent = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public bool IsSent
        //{
        //    get { return _issent; }
        //    set { _issent = value; }
        //}

        //private string SMTPServerName
        //{
        //    get { return ConfigurationManager.AppSettings["SMTPServer"]; }
        //}

        //private string FromName
        //{
        //    get { return ConfigurationManager.AppSettings["FromName"]; }
        //}

        //private string FromAddress
        //{
        //    get { return ConfigurationManager.AppSettings["FromAddress"]; }
        //}
    }

    public struct EmailContents
    {
        public string To;
        public string Subject;
        public string Body;
        public string AttachedFileName;
    }
}
