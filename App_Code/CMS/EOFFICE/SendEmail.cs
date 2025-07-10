using System;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Data.SqlClient;
using System.Web.Security;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ShipSoft.CrewManager.DataAccessLayer;
using System.Configuration;
using MailMessage = System.Net.Mail.MailMessage;

/// <summary>
/// Summary description for SendEmail
/// </summary>
public class SendEmail
{
    public static string _ServerName = ConfigurationManager.AppSettings["SMTPServerName"];
    public static int _Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
    public static MailAddress _FromAdd = new MailAddress(ConfigurationManager.AppSettings["FromAddress"]);
    public static string _UserName = ConfigurationManager.AppSettings["SMTPUserName"];
    public static string _Password = ConfigurationManager.AppSettings["SMTPUserPwd"];
    
    public static void SetMails(SmtpClient _SmtpClient, MailMessage _MailMessage, string _ReplyMailAddress)
    {
        _SmtpClient.Host = _ServerName;
        _SmtpClient.Port = _Port;
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                      | SecurityProtocolType.Tls11
                                      | SecurityProtocolType.Tls12;
        _SmtpClient.Credentials = new NetworkCredential(_UserName, _Password, _ServerName);
        _MailMessage.From = _FromAdd;
        _SmtpClient.EnableSsl = true;

        if (_ReplyMailAddress.Trim() == "emanager@energiossolutions.com")
        {
            _ReplyMailAddress = _FromAdd.Address;
        }
       // _ReplyMailAddress = _ReplyMailAddress.Replace("@energiosmaritime.com", "@energiossolutions.com");
        _MailMessage.ReplyTo = new MailAddress(_ReplyMailAddress);
    }

    public SendEmail()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static string LoginUserEmailId(int _LoginId)
    {
        string Loginuseremail = "";
        string procedurename = "SelectUserLoginDetailsByLoginId";
        DataTable dt = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);
        objDatabase.AddInParameter(objDbCommand, "@Id", DbType.Int32, _LoginId);

        using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
        {
            dt.Load(dr);
            Loginuseremail = dt.Rows[0]["Email"].ToString();
        }

        return Loginuseremail;
        //return dt;
    }
    public static bool SendeMail(int LoginId , string FromAddress, string ReplyToAddress, string[] ToAddress, string[] CCAddress, string[] BCCAddress, string Subject, string Message, out string Error, string AttachmentFilePath)
    {
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                      | SecurityProtocolType.Tls11
                                      | SecurityProtocolType.Tls12;
        bool MailSend = false;
        try
        {

            MailMessage objMessage = new MailMessage();
            //SmtpClient objSmtpClient = new SmtpClient(_ServerName, _Port);
            //objSmtpClient.UseDefaultCredentials = false;
            //objSmtpClient.Credentials = new NetworkCredential(_UserName, _Password, _ServerName);
            objMessage.From = new MailAddress(FromAddress);
           
            objMessage.ReplyTo = new MailAddress(ReplyToAddress);

            foreach (string Address in ToAddress)
            {
                if (Address.Trim() != "")
                {
                    MailAddress ma = new MailAddress(Address);
                    objMessage.To.Add(ma);
                }
            }
            foreach (string Address in CCAddress)
            {
                if (Address.Trim() != "")
                {
                    MailAddress ma = new MailAddress(Address);
                    objMessage.CC.Add(ma);
                }
            }
            foreach (string Address in BCCAddress)
            {
                if (Address.Trim() != "")
                {
                    MailAddress ma = new MailAddress(Address);
                    objMessage.CC.Add(ma);
                }
            }

            //SetMails(objSmtpClient, objMessage, FromAddress); 
            objMessage.Body = Message;
            objMessage.Subject = Subject;
            objMessage.IsBodyHtml = true;
            Attachment Att = null;
            if (AttachmentFilePath.Trim() != "")
            {
                Att = new Attachment(AttachmentFilePath);
                objMessage.Attachments.Add(Att);
            }
            objMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            using (SmtpClient smtpclient = new SmtpClient())
            {
                smtpclient.Host = _ServerName;
                smtpclient.Port = _Port;
                smtpclient.EnableSsl = true;
                smtpclient.UseDefaultCredentials = false;
                smtpclient.DeliveryMethod = SmtpDeliveryMethod.Network;
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                     | SecurityProtocolType.Tls11
                                     | SecurityProtocolType.Tls12;
                smtpclient.Credentials = new NetworkCredential(_UserName, _Password);
                smtpclient.Send(objMessage);
            }
           // objSmtpClient.Send(objMessage);
            MailSend = true;
            Error = "";
           // Att.Dispose();
        }
        catch (Exception ex) { Error = ex.Message; }
        return MailSend;
    }
    public static bool SendeMailAsync(int LoginId, string FromAddress, string ReplyToAddress, string[] ToAddress, string[] CCAddress, string[] BCCAddress, string Subject, string Message, out string Error, string AttachmentFilePath)
    {
        bool MailSend = false;
        try
        {
            string Mailfrom;
            Mailfrom = "";
            MailMessage objMessage = new MailMessage();
            SmtpClient objSmtpClient = new SmtpClient(_ServerName, _Port);
            objSmtpClient.Credentials = new NetworkCredential(_UserName, _Password);
            objMessage.From = new MailAddress(FromAddress);
            objMessage.ReplyTo = new MailAddress(ReplyToAddress);

            foreach (string Address in ToAddress)
            {
                if (Address.Trim() != "")
                {
                    MailAddress ma = new MailAddress(Address);
                    objMessage.To.Add(ma);
                }
            }
            foreach (string Address in CCAddress)
            {
                if (Address.Trim() != "")
                {
                    MailAddress ma = new MailAddress(Address);
                    objMessage.CC.Add(ma);
                }
            }
            foreach (string Address in BCCAddress)
            {
                if (Address.Trim() != "")
                {
                    MailAddress ma = new MailAddress(Address);
                    objMessage.CC.Add(ma);
                }
            }
            SetMails(objSmtpClient, objMessage, FromAddress);

            objMessage.Body = Message;
            objMessage.Subject = Subject;
            objMessage.IsBodyHtml = true;
            if (AttachmentFilePath.Trim() != "")
            {
                objMessage.Attachments.Add(new Attachment(AttachmentFilePath));
            }
            objMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            object ob =new object(); 
            objSmtpClient.SendAsync(objMessage,ob);
            MailSend = true;
            Error = "";
        }
        catch (Exception ex) { Error = ex.Message; }
        return MailSend;
    }
    public static bool SendeMailAsync_TEST(int LoginId, string FromAddress, string ReplyToAddress, string[] ToAddress, string[] CCAddress, string[] BCCAddress, string Subject, string Message, out string Error, string AttachmentFilePath)
    {
        bool MailSend = false;
        try
        {
            string Mailfrom;
            Mailfrom = "";
            MailMessage objMessage = new MailMessage();
            SmtpClient objSmtpClient = new SmtpClient(_ServerName, _Port);
            objMessage.From = new MailAddress(FromAddress);
            objMessage.ReplyTo = new MailAddress(ReplyToAddress);

            string strToAddress = string.Join(";", ToAddress);
            string strCcAddress = string.Join(";", CCAddress);
            string strBccAddress = string.Join(";", BCCAddress);

            objMessage.To.Add(" ");
            objMessage.To.Add(" ");
           
            SetMails(objSmtpClient, objMessage, FromAddress);

            objMessage.Body = "<br>ToAddress : " + strToAddress + "<br>" + "CcAddress : " + strCcAddress + "<br>" + "BccAddress : " + strBccAddress + "<br>=========================<br>" + Message;

            objMessage.Subject = Subject;
            objMessage.IsBodyHtml = true;
            if (AttachmentFilePath.Trim() != "")
            {
                objMessage.Attachments.Add(new Attachment(AttachmentFilePath));
            }
            objMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            object ob = new object();
            objSmtpClient.SendAsync(objMessage, ob);
            MailSend = true;
            Error = "";
        }
        catch (Exception ex) { Error = ex.Message; }
        return MailSend;
    }
    public static bool SendeMail_TEST(int LoginId, string FromAddress, string ReplyToAddress, string[] ToAddress, string[] CCAddress, string[] BCCAddress, string Subject, string Message, out string Error, string AttachmentFilePath)
    {
        bool MailSend = false;
        try
        {
            string Mailfrom = "pankaj.k@esoftech.com";
            MailMessage objMessage = new MailMessage();
            SmtpClient objSmtpClient = new SmtpClient(_ServerName, _Port);
            objSmtpClient.EnableSsl = true;
            objSmtpClient.Credentials = new NetworkCredential("pankaj.k@esoftech.com","pankajesoft99");
            objMessage.From = new MailAddress(FromAddress);
            objMessage.ReplyTo = new MailAddress(ReplyToAddress);

            objMessage.To.Add("asingh@energiossolutions.com");
            objMessage.To.Add("asingh@energiossolutions.com");
            objMessage.To.Add("asingh@energiossolutions.com");

            string strToAddress = string.Join(";",ToAddress);
            string strCcAddress = string.Join(";", CCAddress);
            string strBccAddress = string.Join(";", BCCAddress);

            objMessage.Body ="ToAddress : " + strToAddress +"</br>"+"CcAddress : " + strCcAddress +"</br>"+"BccAddress : " + strBccAddress +"</br>=========================" +  Message;

            objMessage.Subject = Subject;
            objMessage.IsBodyHtml = true;
            if (AttachmentFilePath.Trim() != "")
            {
                objMessage.Attachments.Add(new Attachment(AttachmentFilePath));
            }
            objMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            objSmtpClient.Send(objMessage);
            MailSend = true;
            Error = "";
        }
        catch (Exception ex) { 
            Error = ex.Message; 
        }
        return MailSend;
    }
    public void mailsends(object sender,System.ComponentModel.AsyncCompletedEventArgs e)
    {
        int i = 10;
        i=1+20;
    }
    public static bool SendeMail_TEST_UK(string FromAddress, string[] ToAddress, string[] CCAddress, string[] BCCAddress, string Subject, string Message, out string Error, string AttachmentFilePath)
    {
        bool MailSend = false;
        try
        {
            FromAddress = "pankaj.k@esoftech.com";
            MailMessage objMessage = new MailMessage();
            SmtpClient objSmtpClient = new SmtpClient("smtp.gmail.com", 25);
            objSmtpClient.EnableSsl = true;
            objSmtpClient.Credentials = new NetworkCredential("pankaj.k@esoftech.com", "pankajesoft99");
            objMessage.From = new MailAddress(FromAddress);
            //objMessage.ReplyTo = new MailAddress(ReplyToAddress);

            foreach (string Address in ToAddress)
            {
                if (Address.Trim() != "")
                {
                    MailAddress ma = new MailAddress(Address);
                    objMessage.To.Add(ma);
                }
            }
            foreach (string Address in CCAddress)
            {
                if (Address.Trim() != "")
                {
                    MailAddress ma = new MailAddress(Address);
                    objMessage.CC.Add(ma);
                }
            }
            foreach (string Address in BCCAddress)
            {
                if (Address.Trim() != "")
                {
                    MailAddress ma = new MailAddress(Address);
                    objMessage.CC.Add(ma);
                }
            }


            objMessage.Body = Message;
            objMessage.Subject = Subject;
            objMessage.IsBodyHtml = true;
            if (AttachmentFilePath.Trim() != "")
            {
                objMessage.Attachments.Add(new Attachment(AttachmentFilePath));
            }
            objMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            objSmtpClient.Send(objMessage);
            MailSend = true;
            Error = "";
        }
        catch (Exception ex)
        {
            Error = ex.Message;
        }
        return MailSend;
    }
}
