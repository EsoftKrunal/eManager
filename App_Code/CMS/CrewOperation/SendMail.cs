using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Data.Common;
using System.Configuration;
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Text;


/// <summary>
/// Summary description for SendMail
/// </summary>
public class SendMail
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
        _SmtpClient.Credentials = new NetworkCredential(_UserName, _Password);
        _SmtpClient.EnableSsl = true;
        _MailMessage.From = _FromAdd;

        if (_ReplyMailAddress.Trim() == "emanager@energiossolutions.com")
        {
            _ReplyMailAddress = _FromAdd.Address;
        }
       // _ReplyMailAddress = _ReplyMailAddress.Replace("@abc.com", "@abcd.com");
       // _MailMessage.ReplyTo = new MailAddress(_ReplyMailAddress);
    }

    public SendMail()
    {
        
        //
        // TODO: Add constructor logic here
        //
    }
    public static void MailSend(String _Mailto,String _mailSubject, String _MailBody,string _Mailfrom)
    {

        try
        {
            MailMessage message = new MailMessage();
            SmtpClient smtpclient = new SmtpClient();
            //***** from email address of login user
           // MailAddress fromaddress = new MailAddress(ConfigurationManager.AppSettings["FromAddress"].ToString(), ConfigurationManager.AppSettings["FromName"]);
            MailAddress fromaddress = new MailAddress(_Mailfrom);
            message.From = fromaddress;
            message.To.Add(_Mailto.ToString());
            message.Subject = _mailSubject; 
            message.IsBodyHtml = true;
            message.Body = _MailBody;
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            smtpclient.Send(message);
            
        }
        catch (SystemException es)
        {
            string str = es.Message;
        }


    }
    public static void MailSendNewVisit(String _Mailto,String _mailSubject, String _MailBody, string _Mailfrom)
    {

        try
        {
            MailMessage message = new MailMessage();
            SmtpClient _SmtpClient = new SmtpClient();
            _SmtpClient.Host = _ServerName;
            _SmtpClient.Port = _Port;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                          | SecurityProtocolType.Tls11
                                          | SecurityProtocolType.Tls12;
            _SmtpClient.Credentials = new NetworkCredential(_UserName, _Password);
            _SmtpClient.EnableSsl = true;
            //***** from email address of login user
            // MailAddress fromaddress = new MailAddress(ConfigurationManager.AppSettings["FromAddress"].ToString(), ConfigurationManager.AppSettings["FromName"]);
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                      | SecurityProtocolType.Tls11
                                      | SecurityProtocolType.Tls12;
            MailAddress fromaddress = new MailAddress(_Mailfrom);
            message.From = fromaddress;
            message.To.Add(_Mailto.ToString());
            message.CC.Add(new MailAddress("emanager@energiossolutions.com"));
            message.CC.Add(new MailAddress("emanager@energiossolutions.com"));
            message.Subject = _mailSubject;
            message.IsBodyHtml = true;
            message.Body = _MailBody;
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            _SmtpClient.Send(message);

        }
        catch (SystemException es)
        {
            string str = es.Message;
        }


    }

    public static bool SendeMailAsync(string FromAddress, string ReplyToAddress, string[] ToAddress, string[] CCAddress, string[] BCCAddress, string Subject, string Message, out string Error, string AttachmentFilePath)
    {
        bool MailSend = false;
        try
        {
            MailMessage objMessage = new MailMessage();
            SmtpClient objSmtpClient = new SmtpClient();
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
            objSmtpClient.SendAsync(objMessage, null);
            MailSend = true;
            Error = "";
        }
        catch (Exception ex) { Error = ex.Message; }
        return MailSend;
    }
    # region "Follow Up"
    public static void SendFollowUpMails(string fromAddress, string toAddress, string tocc, string mailInspNum, string mailQuesNum, string mailDeficiency, string mailCorrectActions, string mailResponsibility, string mailTargClDate, bool isBodyHTML)
    {
        MailMessage objMessage = new MailMessage();
        SmtpClient objSmtpClient = new SmtpClient();
        MailAddress objfromAddress = new MailAddress(fromAddress);
        StringBuilder msgFormat = new StringBuilder();
        SetMails(objSmtpClient, objMessage, fromAddress);
        try
        {
            objMessage.To.Add(toAddress);
            if (tocc != "")
            {
                objMessage.CC.Add(tocc);
            }
            msgFormat.Append("<html><head></head><body>" + "\n");
            msgFormat.Append("<table width=100% align=center> " + "\n");

            msgFormat.Append("<tr><td width=100% height=20 style=\"text-align: left\">" + "</td></tr>" + "\n");

            msgFormat.Append("<tr><td width=100% style=\"text-align: left\"><strong>" + "Deficiency : " + "</strong><span style=\"font-size: 10 pt; font-family:Arial\">" + mailDeficiency + "</span>" + "</td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: left\">" + "</td></tr>" + "\n");

            msgFormat.Append("<tr><td width=100% style=\"text-align: left\"><strong>" + "Corrective Action : " + "</strong><span style=\"font-size: 10 pt; font-family:Arial\">" + mailCorrectActions + "</span>" + "</td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: left\">" + "</td></tr>" + "\n");

            msgFormat.Append("<tr><td width=100% style=\"text-align: left\"><strong>" + "Responsibilty : " + "</strong><span style=\"font-size: 10 pt; font-family:Arial\">" + mailResponsibility + "</span>" + "</td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: left\">" + "</td></tr>" + "\n");

            msgFormat.Append("<tr><td width=100% style=\"text-align: left\"><strong>" + "Target Closure Date : " + "</strong><span style=\"font-size: 10 pt; font-family:Arial\">" + mailTargClDate + "</span>" + "</td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: left\">" + "</td></tr>" + "\n");

            msgFormat.Append("<tr><td width=100% height=20 style=\"text-align: left\">" + "</td></tr>" + "\n");

            msgFormat.Append("</table>" + "\n");

            msgFormat.Append("</body></html>" + "\n");
            objMessage.Body = msgFormat.ToString();
            objMessage.Subject = "Inspection# : " + mailInspNum + " ; FollowUp Item : " + mailQuesNum;
            objMessage.IsBodyHtml = isBodyHTML;
            objMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            objSmtpClient.Send(objMessage);
        }
        catch (Exception e)
        {

        }
    }
    public static void FollowUpMail(string InspDueStatus, string strInspNo, string strQuesNo, string strDef, string strCorrAct, string strResp, string strTargClDt)
    {
        string procedurename = "PR_GetEmailId";
        DataTable dt44 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        objDatabase.AddInParameter(objDbCommand, "@InspDueStatus", DbType.String, InspDueStatus);

        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt44.Load(dr);
                if (dt44.Rows.Count > 0)
                {
                    SendFollowUpMails(HttpContext.Current.Session["EmailAddress"].ToString(), dt44.Rows[0][0].ToString(), "", strInspNo, strQuesNo, strDef, strCorrAct, strResp, strTargClDt, true);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDbCommand.Dispose();
        }
    }
    # endregion
    public static bool SendeMail(string FromAddress, string ReplyToAddress, string[] ToAddress, string[] CCAddress, string[] BCCAddress, string Subject, string Message, out string Error, string AttachmentFilePath)
    {
        bool MailSend = false;
        try
        {
            MailMessage objMessage = new MailMessage();
            SmtpClient objSmtpClient = new SmtpClient();
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
            objSmtpClient.Send(objMessage);
            MailSend = true;
            Error = "";
        }
        catch (Exception ex) { Error = ex.Message; }
        return MailSend;
    }
    public static void Mail(string InspDueStatus, string strSubject, string strInspNum, string strPortVoyage, string strFromPort, string strToPort, string strPlanDate, string strSuptDetail, string strRemarks, string strSuptEmailId)
    {
        string procedurename = "PR_GetEmailId";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        objDatabase.AddInParameter(objDbCommand, "@InspDueStatus", DbType.String, InspDueStatus);

        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt1.Load(dr);
                if (dt1.Rows.Count > 0)
                {
                    SendPlanningMails(HttpContext.Current.Session["EmailAddress"].ToString(), strSuptEmailId, dt1.Rows[0][0].ToString(), strSubject, strInspNum, strPortVoyage, strFromPort, strToPort, strPlanDate, strSuptDetail, strRemarks, true, strSuptEmailId);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDbCommand.Dispose();
        }
    }
    #region "Planning Mail"
    public static void SendPlanningMails(string fromAddress, string toAddress, string tocc, string mailsubject, string mailInspNum, string mailPortVoy, string mailFromPort, string mailToPort, string mailPlanDate, string mailSuptDetails, string mailRemarks, bool isBodyHTML, string mailSuptMail)
    {
        char[] c = { ',' };
        Array a = toAddress.Split(c);
        MailMessage objMessage = new MailMessage();
        SmtpClient objSmtpClient = new SmtpClient();
        MailAddress objfromAddress = new MailAddress(fromAddress);
        StringBuilder msgFormat = new StringBuilder();

        SetMails(objSmtpClient, objMessage, fromAddress);
        try
        {
            for (int i = 0; i < a.Length; i++)
            {
                objMessage.To.Add(a.GetValue(i).ToString());
            }
            if (tocc != "")
            {
                objMessage.CC.Add(tocc);
            }
            msgFormat.Append("<html><head></head><body>" + "\n");
            msgFormat.Append("<table width=100% align=center> " + "\n");

            msgFormat.Append("<tr><td width=100% height=20 style=\"text-align: left\">" + "</td></tr>" + "\n");

            msgFormat.Append("<tr><td width=100% style=\"text-align: left\"><strong>" + "Inspection# : " + "</strong><span style=\"font-size: 10 pt; font-family:Arial\">" + mailInspNum + "</span>&nbsp;&nbsp;&nbsp;&nbsp;<strong>" + "Port/Voyage : " + "</strong><span style=\"font-size: 10 pt; font-family:Arial\">" + mailPortVoy + "</span>&nbsp;&nbsp;&nbsp;&nbsp;<strong>" + "From Port : " + "</strong><span style=\"font-size: 10 pt; font-family:Arial\">" + mailFromPort + "</span>&nbsp;&nbsp;&nbsp;&nbsp;<strong>" + "To Port : " + "</strong><span style=\"font-size: 10 pt; font-family:Arial\">" + mailToPort + "</span>" + "</td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: left\">" + "</td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% style=\"text-align: left\"><strong>" + "Plan Date : " + "</strong><span style=\"font-size: 10 pt; font-family:Arial\">" + mailPlanDate + "</span>" + "</td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% height=5 style=\"text-align: left\">" + "</td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% style=\"text-align: left\">" + mailSuptDetails + "</td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% height=5 style=\"text-align: left\">" + "</td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% style=\"text-align: left\"><strong>" + "Planning Remarks : " + "</strong></td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% style=\"text-align: left\">" + "<span style=\"font-size: 10 pt; font-family:Arial\">" + mailRemarks + "</span>" + "</td></tr>" + "\n");

            msgFormat.Append("<tr><td width=100% height=20 style=\"text-align: left\">" + "</td></tr>" + "\n");

            msgFormat.Append("</table>" + "\n");

            msgFormat.Append("</body></html>" + "\n");
            //objMessage.Body = msgContent;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                      | SecurityProtocolType.Tls11
                                      | SecurityProtocolType.Tls12;
            objMessage.Body = msgFormat.ToString();
            objMessage.Subject = mailsubject;
            objMessage.IsBodyHtml = isBodyHTML;
            
            objMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            objSmtpClient.Send(objMessage);
        }
        catch (Exception e)
        {

        }
    }



    #endregion
    #region ""
    # region "Observation & Response"
    public static void SendGEmails(string fromAddress, string toAddress, string tocc, string mailsubject, string mailDoneDate, string mailPortDone, string mailMessage, bool isBodyHTML)
    {
        MailMessage objMessage = new MailMessage();
        SmtpClient objSmtpClient = new SmtpClient();
        MailAddress objfromAddress = new MailAddress(fromAddress);
        StringBuilder msgFormat = new StringBuilder();
        try
        {
            SetMails(objSmtpClient, objMessage, fromAddress);

            objMessage.To.Add(toAddress);
            if (tocc != "")
            {
                objMessage.CC.Add(tocc);
            }
            msgFormat.Append("<html><head></head><body>" + "\n");
            msgFormat.Append("<table width=100% align=center> " + "\n");

            msgFormat.Append("<tr><td width=100% height=20 style=\"text-align: left\">" + "</td></tr>" + "\n");

            msgFormat.Append("<tr><td width=100% style=\"text-align: left\"><strong>" + "Done Date : " + "</strong><span style=\"font-size: 10 pt; font-family:Arial\">" + mailDoneDate + "</span>" + "</td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: left\">" + "</td></tr>" + "\n");

            msgFormat.Append("<tr><td width=100% style=\"text-align: left\"><strong>" + "Port Done : " + "</strong><span style=\"font-size: 10 pt; font-family:Arial\">" + mailPortDone + "</span>" + "</td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: left\">" + "</td></tr>" + "\n");

            msgFormat.Append("<tr><td width=100% style=\"text-align: left\">" + "<span style=\"font-size: 10 pt; font-family:Arial\">" + mailMessage + "</span>" + "</td></tr>" + "\n");

            msgFormat.Append("<tr><td width=100% height=20 style=\"text-align: left\">" + "</td></tr>" + "\n");

            msgFormat.Append("</table>" + "\n");

            msgFormat.Append("</body></html>" + "\n");
            objMessage.Body = msgFormat.ToString();
            objMessage.Subject = "Inspection# : " + mailsubject;
            objMessage.IsBodyHtml = isBodyHTML;
            objMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            objSmtpClient.Send(objMessage);
        }
        catch (Exception e)
        {

        }
    }
    public static void Mail(string InspDueStatus, string strSubject, string strDoneDt, string strPortDn, string strMessage)
    {
        string procedurename = "PR_GetEmailId";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        objDatabase.AddInParameter(objDbCommand, "@InspDueStatus", DbType.String, InspDueStatus);

        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt1.Load(dr);
                if (dt1.Rows.Count > 0)
                {
                    SendGEmails(HttpContext.Current.Session["EmailAddress"].ToString(), dt1.Rows[0][0].ToString(), "", strSubject, strDoneDt, strPortDn, strMessage, true);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDbCommand.Dispose();
        }
    }
    public static void SendObservationNotifyGEmails(string fromAddress, string toAddress, string tocc, string mailsubject, string mailMessage, bool isBodyHTML)
    {
        MailMessage objMessage = new MailMessage();
        SmtpClient objSmtpClient = new SmtpClient();
        MailAddress objfromAddress = new MailAddress(fromAddress);
        StringBuilder msgFormat = new StringBuilder();
        SetMails(objSmtpClient, objMessage, fromAddress);
        try
        {
            objMessage.To.Add(toAddress);
            if (tocc != "")
            {
                objMessage.CC.Add(tocc);
            }

            msgFormat.Append("<html><head></head><body >" + "\n");
            msgFormat.Append("<table width=100% align='center' style='font-family:Arial'> " + "\n");
            msgFormat.Append("<tr><td height=20 style=\"text-align: left\">" + "</td></tr>" + "\n");
            msgFormat.Append("<tr><td style=\"text-align: left\"> " + mailMessage + "</td></tr>" + "\n");
            msgFormat.Append("</table>" + "\n");
            msgFormat.Append("</body></html>" + "\n");
            objMessage.Body = msgFormat.ToString();
            objMessage.Subject = mailsubject;
            objMessage.IsBodyHtml = isBodyHTML;
            objMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            objSmtpClient.Send(objMessage);
        }
        catch (Exception e)
        {

        }
    }
    public static void ObservationNotifyMail(int intInspId, string strSubject, string strMessage)
    {
        string procedurename = "PR_GetEmailId";
        DataTable dt1 = new DataTable();

        Database objDatabase = DatabaseFactory.CreateDatabase();
        DbCommand objDbCommand = objDatabase.GetStoredProcCommand(procedurename);

        objDatabase.AddInParameter(objDbCommand, "@InspDueStatus", DbType.String, "Observation");
        try
        {
            using (IDataReader dr = objDatabase.ExecuteReader(objDbCommand))
            {
                dt1.Load(dr);
                if (dt1.Rows.Count > 0)
                {
                    SendObservationNotifyGEmails(HttpContext.Current.Session["EmailAddress"].ToString(), dt1.Rows[0][0].ToString(), "", strSubject, strMessage, true);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objDbCommand.Dispose();
        }
    }
    #endregion
    #endregion

    #region "Circulare Mail"
    public static string SendCircularMails(string FromMail, string toAddress, string CC, string CircularNumber, string Topic, string Attachment, bool isBodyHTML)
    {
        try
        {
            MailMessage objMessage = new MailMessage();
            SmtpClient objSmtpClient = new SmtpClient();
            MailAddress objfromAddress = new MailAddress(FromMail);
            StringBuilder msgFormat = new StringBuilder();

            SetMails(objSmtpClient, objMessage, FromMail);
            try
            {
                objMessage.To.Add(toAddress);
                objMessage.CC.Add(CC);

                msgFormat.Append("<html><head></head><body>" + "\n");
                msgFormat.Append("<table width=100% align=center> " + "\n");
                msgFormat.Append("<tr><td width=100% height=20 style=\"text-align: left\">" + "</td></tr>" + "\n");

                msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: left\">" + "</td></tr>" + "\n");
                //msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: center;font-weight:bold;text-decoration:underline;\"> Circular Topic:&nbsp;" + Topic + " " + "</td></tr>" + "\n");
                msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: center;font-weight:bold;text-decoration:underline;\"> Circular Topic:&nbsp; " + Topic + "" + "</td></tr>" + "\n");
                msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: left\">" + "</td></tr>" + "\n");
                msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: left\"> Attached please find the  subject circular. Please acknowledge safe receipt of attachment <br /> to HSQE department .<br /><br /><br /><br /><br /><br /><br />Thank You, <br /> For MTM Ship Management Pte. Ltd.<br />(HSQE DEPT.)" + "</td></tr>" + "\n");

                msgFormat.Append("<tr><td width=100% height=20 style=\"text-align: left\">" + "</td></tr>" + "\n");
                msgFormat.Append("</table>" + "\n");
                msgFormat.Append("</body></html>" + "\n");
                objMessage.Body = msgFormat.ToString();


                objMessage.Body = msgFormat.ToString();
                objMessage.Subject = "Circular : " + CircularNumber;
                objMessage.IsBodyHtml = isBodyHTML;
                //objSmtpClient.Credentials = new NetworkCredential("noreply", "Mtm1234");

                Attachment attachFile = new Attachment(Attachment);
                objMessage.Attachments.Add(attachFile);

                objMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                objSmtpClient.Send(objMessage);
                return "Mail send";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }
    #endregion
    #region "VIQ Mail to Vessel"
    public static string SendVIQMails(string FromMail, string toAddress, string VIQNO, string SenderName, string Attachment, bool isBodyHTML)
    {
        try
        {
            MailMessage objMessage = new MailMessage();
            SmtpClient objSmtpClient = new SmtpClient();
            MailAddress objfromAddress = new MailAddress(FromMail);
            StringBuilder msgFormat = new StringBuilder();

            SetMails(objSmtpClient, objMessage, FromMail);
            try
            {
                objMessage.To.Add(toAddress);
                objMessage.CC.Add(FromMail);

                msgFormat.Append("<html><head></head><body>" + "\n");
                msgFormat.Append("<table width=100% align=center> " + "\n");
                msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: left\">" + "</td></tr>" + "\n");
                msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: left;\"> Dear Captain, </td></tr>" + "\n");
                msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: left;\"> Attached is the vetting preparation questionnaire which please import into vetting module of SHIPSOFT PMS. </td></tr>" + "\n");
                msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: left;\"> All questionnaire must be completed with proof of work done before the assigned due date. </td></tr>" + "\n");
                msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: left;\"> In case of any query , please contact your marine superintendent in charge of your vessel. </td></tr>" + "\n");
                msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: left;\"> Please confirm to Marine department after successful import of this packet. </td></tr>" + "\n");
                msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: left\"> <br /><br />Thank You, <br /> " + SenderName + "<br />(" + FromMail + ")" + "</td></tr>" + "\n");

                msgFormat.Append("<tr><td width=100% height=20 style=\"text-align: left\">" + "</td></tr>" + "\n");
                msgFormat.Append("</table>" + "\n");
                msgFormat.Append("</body></html>" + "\n");
                objMessage.Body = msgFormat.ToString();


                objMessage.Body = msgFormat.ToString();
                objMessage.Subject = "Vetting Preparation # : " + VIQNO;
                objMessage.IsBodyHtml = isBodyHTML;

                Attachment attachFile = new Attachment(Attachment);
                objMessage.Attachments.Add(attachFile);

                objMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                objSmtpClient.Send(objMessage);
                return "SENT";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }
    # endregion

    # region "VIQ Mail to Vessel"
    public static string SendRiskAssesmentTemplateMailToVesssel(string FromMail, string toAddress, string CCAddress, string Attachment)
    {
        try
        {
            MailMessage objMessage = new MailMessage();
            SmtpClient objSmtpClient = new SmtpClient();
            MailAddress objfromAddress = new MailAddress(FromMail);
            StringBuilder msgFormat = new StringBuilder();

            SetMails(objSmtpClient, objMessage, FromMail);
            try
            {
                objMessage.To.Add(toAddress);
                //objMessage.CC.Add(FromMail);
                objMessage.CC.Add(CCAddress);

                msgFormat.Append("<html><head></head><body>" + "\n");
                msgFormat.Append("<table width=100% align=center> " + "\n");
                msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: left\">" + "</td></tr>" + "\n");
                msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: left;\"> Dear Captain, </td></tr>" + "\n");
                msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: left;\"> Attached is the risk assessment template packet.</td></tr>" + "\n");
                msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: left;\"> Please import it in your system and send acknowledgement which will be created just after you import.</td></tr>" + "\n");
                msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: left;\"> System will prompt you to download the acknowledgement file. Save it and send to emanager@energiossolutions.com.</td></tr>" + "\n");

                msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: left\"> <br /><br />Thank You</td></tr>" + "\n");
                msgFormat.Append("<tr><td width=100% height=20 style=\"text-align: left\">" + "</td></tr>" + "\n");
                msgFormat.Append("</table>" + "\n");
                msgFormat.Append("</body></html>" + "\n");
                objMessage.Body = msgFormat.ToString();


                objMessage.Body = msgFormat.ToString();
                objMessage.Subject = "Risk Assesment Template : " + System.IO.Path.GetFileName(Attachment);
                objMessage.IsBodyHtml = true;

                Attachment attachFile = new Attachment(Attachment);
                objMessage.Attachments.Add(attachFile);

                objMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                objSmtpClient.Send(objMessage);

                return "SENT";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }
    # endregion
    # region "LFI / FC Mail to Vessel"
    public static string SendLFIMail(string FromMail, string toAddress, string[] BCCAddresses, string LFINumber, string BodyText, string Attachment)
    {
        try
        {
            MailMessage objMessage = new MailMessage();
            SmtpClient objSmtpClient = new SmtpClient("192.168.1.13", 25);
            MailAddress objfromAddress = new MailAddress(FromMail);
            StringBuilder msgFormat = new StringBuilder();
            SetMails(objSmtpClient, objMessage, FromMail);
            try
            {
                objMessage.To.Add(toAddress);
                objMessage.Body = BodyText;
                objMessage.Subject = "LFI Mail : " + LFINumber;
                objMessage.IsBodyHtml = true;

                foreach (string adrs in BCCAddresses)
                {
                    objMessage.Bcc.Add(adrs);
                }

                using (System.IO.MemoryStream ms = new System.IO.MemoryStream(System.IO.File.ReadAllBytes(Attachment)))
                {
                    System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(ms, System.IO.Path.GetFileName(Attachment), System.Net.Mime.MediaTypeNames.Application.Zip);
                    objMessage.Attachments.Add(attach);

                    //Attachment attachFile = new Attachment(Attachment);
                    //objMessage.Attachments.Add(attachFile);

                    objMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                    objSmtpClient.Send(objMessage);
                }
                return "SENT";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }
    public static string SendFocusCampMail(string FromMail, string toAddress, string[] BCCAddresses, string FocusCampNumber, string BodyText, string Attachment)
    {
        try
        {
            MailMessage objMessage = new MailMessage();
            SmtpClient objSmtpClient = new SmtpClient();
            MailAddress objfromAddress = new MailAddress(FromMail);
            StringBuilder msgFormat = new StringBuilder();
            SetMails(objSmtpClient, objMessage, FromMail);
            try
            {
                objMessage.To.Add(toAddress);
                objMessage.Body = BodyText;
                objMessage.Subject = "Focussed Campaign Mail : " + FocusCampNumber;
                objMessage.IsBodyHtml = true;
                foreach (string adrs in BCCAddresses)
                {
                    objMessage.Bcc.Add(adrs);
                }
                Attachment attachFile = new Attachment(Attachment);
                objMessage.Attachments.Add(attachFile);

                objMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                objSmtpClient.Send(objMessage);
                return "SENT";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }
    # endregion
    # region "Send Simple eMail"

    public static string SendSimpleMail(string FromMail, string toAddress, string[] CCAddresses, string[] BCCAddresses, string Subject, string BodyText, string Attachment)
    {
        try
        {
            MailMessage objMessage = new MailMessage();
            SmtpClient objSmtpClient = new SmtpClient();
            MailAddress objfromAddress = new MailAddress(FromMail);
            StringBuilder msgFormat = new StringBuilder();
            SetMails(objSmtpClient, objMessage, FromMail);
            try
            {
                objMessage.To.Add(toAddress);
                objMessage.Body = BodyText;
                objMessage.Subject = Subject;
                objMessage.IsBodyHtml = true;

                foreach (string CCadrs in CCAddresses)
                {
                    objMessage.CC.Add(CCadrs);
                }

                foreach (string adrs in BCCAddresses)
                {
                    objMessage.Bcc.Add(adrs);
                }
                Attachment attachFile = new Attachment(Attachment);
                objMessage.Attachments.Add(attachFile);

                objMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                objSmtpClient.Send(objMessage);
                return "SENT";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    #endregion
    public static string SendInviteCommentsMail(string fromAddress, string toAddress, string[] tocc, string mailsubject, string Comments, string Section, string FilePath, string Link, string UserName, string Position, bool isBodyHTML)
    {
        MailMessage objMessage = new MailMessage();
        SmtpClient objSmtpClient = new SmtpClient();
        MailAddress objfromAddress = new MailAddress(fromAddress);
        StringBuilder msgFormat = new StringBuilder();
        //Attachment Atcmt = new Attachment(FilePath);

        SetMails(objSmtpClient, objMessage, fromAddress);
        try
        {
            objMessage.To.Add(toAddress);
            foreach (string Address in tocc)
            {
                if (Address.Trim() != "")
                {
                    MailAddress ma = new MailAddress(Address);
                    objMessage.CC.Add(ma);
                }
            }

            msgFormat.Append("<html><head></head><body>" + "\n");
            msgFormat.Append("<table width=100% align=left> " + "\n");
            msgFormat.Append("<tr><td width=100% height=20 style=\"text-align: left\">" + "</td></tr>" + "\n");

            msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: left\">" + "</td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% height=10 > <b>MANUAL :</b> &nbsp; " + Comments + "" + "</td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% height=10 > <b>SECTION :</b> &nbsp; " + Section + "" + "</td></tr>" + "\n");

            msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: center;font-weight:bold;text-decoration:underline;\"> " + "</td></tr>" + "\n");

            msgFormat.Append("<tr><td width=100% height=10> Please access the changes in contents of above manual/section from below link and provide your valuable comments for finalization." + "</td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% height=10 ><a href='" + System.Configuration.ConfigurationManager.AppSettings["PublicAction"].ToString() + "?" + Link + "' >" + System.Configuration.ConfigurationManager.AppSettings["PublicAction"].ToString() + "?" + Link + "</a></td></tr>" + "\n");

            msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: left\">" + "</td></tr>" + "\n\n\n");
            msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: left\"> Thanks & Regards, </td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: left\"> " + UserName + " </td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: left\"> " + Position + " </td></tr>" + "\n");

            msgFormat.Append("</table>" + "\n");
            msgFormat.Append("</body></html>" + "\n");

            objMessage.Body = msgFormat.ToString();
            objMessage.Subject = mailsubject;
            objMessage.IsBodyHtml = isBodyHTML;
            //objMessage.Attachments.Add(Atcmt);
            //objSmtpClient.Credentials = new NetworkCredential("noreply", "Mtm1234");
            objMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            objSmtpClient.Send(objMessage);
            return "True";
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    public static void SafetyAlertNotificationMail(string fromAddress, string toAddress, string[] tocc, string mailsubject, string Topic, string FilePath, bool isBodyHTML)
    {
        //*****************
        string strOverDueInspections = "";
        //*****************

        MailMessage objMessage = new MailMessage();
        SmtpClient objSmtpClient = new SmtpClient();
        MailAddress objfromAddress = new MailAddress(fromAddress);
        StringBuilder msgFormat = new StringBuilder();
        Attachment Atcmt = new Attachment(FilePath);

        SetMails(objSmtpClient, objMessage, fromAddress);
        try
        {
            objMessage.To.Add(toAddress);
            //if (tocc != "")
            //{
            //    objMessage.CC.Add(tocc);
            //}
            foreach (string Address in tocc)
            {
                if (Address.Trim() != "")
                {
                    MailAddress ma = new MailAddress(Address);
                    objMessage.CC.Add(ma);
                }
            }

            msgFormat.Append("<html><head></head><body>" + "\n");
            msgFormat.Append("<table width=100% align=center> " + "\n");
            msgFormat.Append("<tr><td width=100% height=20 style=\"text-align: left\">" + "</td></tr>" + "\n");

            msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: left\">" + "</td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: center;font-weight:bold;font-size:20px;text-decoration:underline;\"> Safety Alert " + "</td></tr>" + "\n\n");
            msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: center;font-weight:bold;text-decoration:underline;\"> Topic:&nbsp; " + Topic + "" + "</td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: left\">" + "</td></tr>" + "\n");
            msgFormat.Append("<tr><td width=100% height=10 style=\"text-align: left\">Attached please find the subject Safety Alert. Please acknowledge safe receipt of attachment <br /> to HSQE department .<br /><br /><br /><br /><br /><br /><br />Thank You, <br /> <br />(HSQE DEPT.)" + "</td></tr>" + "\n");

            msgFormat.Append("<tr><td width=100% height=20 style=\"text-align: left\">" + "</td></tr>" + "\n");
            msgFormat.Append("</table>" + "\n");
            msgFormat.Append("</body></html>" + "\n");

            objMessage.Body = msgFormat.ToString();
            objMessage.Subject = mailsubject;
            objMessage.IsBodyHtml = isBodyHTML;
            objMessage.Attachments.Add(Atcmt);
            objSmtpClient.Send(objMessage);
        }
        catch (Exception e)
        {

        }
    }

    public static string SendCirMail(string FromMail, string toAddress, string[] BCCAddresses, string LFINumber, string BodyText, string Attachment)
    {
        try
        {
            MailMessage objMessage = new MailMessage();
            SmtpClient objSmtpClient = new SmtpClient();
            MailAddress objfromAddress = new MailAddress(FromMail);
            StringBuilder msgFormat = new StringBuilder();
            SetMails(objSmtpClient, objMessage, FromMail);
            try
            {
                objMessage.To.Add(toAddress);
                objMessage.Body = BodyText;
                objMessage.Subject = "Circular Mail : " + LFINumber;
                objMessage.IsBodyHtml = true;
                foreach (string adrs in BCCAddresses)
                {
                    objMessage.Bcc.Add(adrs);
                }
                Attachment attachFile = new Attachment(Attachment);
                objMessage.Attachments.Add(attachFile);

                objMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                objSmtpClient.Send(objMessage);
                return "SENT";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    # region "Regulation Mail to Vessel"
    public static string SendRegulationMail(string FromMail, string toAddress, string[] BCCAddresses, string REGNumber, string BodyText, string Attachment)
    {
        try
        {
            MailMessage objMessage = new MailMessage();
            SmtpClient objSmtpClient = new SmtpClient();
            MailAddress objfromAddress = new MailAddress(FromMail);
            StringBuilder msgFormat = new StringBuilder();
            SetMails(objSmtpClient, objMessage, FromMail);
            try
            {
                objMessage.To.Add(toAddress);
                objMessage.Body = BodyText;
                objMessage.Subject = "Regulation Mail : " + REGNumber;
                objMessage.IsBodyHtml = true;
                if (BCCAddresses.Length > 0)
                {
                    foreach (string adrs in BCCAddresses)
                    {
                        objMessage.Bcc.Add(adrs);
                    }
                }
                
                Attachment attachFile = new Attachment(Attachment);
                objMessage.Attachments.Add(attachFile);

                objMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                objSmtpClient.Send(objMessage);
                return "SENT";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }
    # endregion

}
