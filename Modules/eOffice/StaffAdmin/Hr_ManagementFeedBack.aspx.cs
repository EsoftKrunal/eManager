using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

public partial class emtm_StaffAdmin_Emtm_Hr_ManagementFeedBack : System.Web.UI.Page
{


    string MailClient = ConfigurationManager.AppSettings["SMTPServerName"].ToString();
    int Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
    string SenderAddress = ConfigurationManager.AppSettings["FromAddress"];
    string SenderUserName = ConfigurationManager.AppSettings["SMTPUserName"];
    string SenderPass = ConfigurationManager.AppSettings["SMTPUserPwd"];

    public AuthenticationManager auth;
    public string SessionDeleimeter = ConfigurationManager.AppSettings["SessionDelemeter"];
    public int PeapID
    {
        get
        {
            return Common.CastAsInt32(ViewState["PeapID"]);
        }
        set
        {
            ViewState["PeapID"] = value;
        }
    }
    public int EmpID
    {
        get
        {
            return Common.CastAsInt32(ViewState["EmpID"]);
        }
        set
        {
            ViewState["EmpID"] = value;
        }
    }
    public int ManagerId
    {
        get
        {
            return Common.CastAsInt32(ViewState["ManagerId"]);
        }
        set
        {
            ViewState["ManagerId"] = value;
        }
    }
    public string Mode
    {
        get
        {
            return "" + ViewState["Mode"].ToString();
        }
        set
        {
            ViewState["Mode"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMsg.Text = "";

        if (!Page.IsPostBack)
        {
            if (Request.QueryString["PeapID"] != null || Request.QueryString["PeapID"].ToString() != "")
            {
                PeapID = Common.CastAsInt32(Request.QueryString["PeapID"].ToString());
                Mode = Request.QueryString["LoginMode"].ToString();
                ManagerId = Common.CastAsInt32(Request.QueryString["MId"].ToString());

                if (Mode == "A")
                {
                    EmpID = Common.CastAsInt32(Session["EmpId"]);
                }
                else
                {
                    EmpID = Common.CastAsInt32(Session["ProfileId"]);
                }

                ShowRecord();
                
            }
        }

    }

    public void ShowRecord()
    {
        string strSQL = "SELECT PEAPID,CATEGORY As PeapLevel,CASE Occasion WHEN 'R' THEN 'Routine' WHEN 'I' THEN 'Interim' ELSE '' END AS Occasion, POSITIONNAME,EMPCODE,FIRSTNAME , FAMILYNAME,D.DeptName AS DepartmentName, " +
                        "Replace(Convert(Varchar,PM.PEAPPERIODFROM,106),' ','-') AS PEAPPERIODFROM ,Replace(Convert(Varchar,PM.PEAPPERIODTO,106),' ','-') AS PEAPPERIODTO,Replace(Convert(Varchar,PD.DJC,106),' ','-') AS DJC,PM.Status,(SELECT FIRSTNAME + ' ' + FAMILYNAME FROM Hr_PersonalDetails WHERE EMPID=" + ManagerId.ToString() + ") AS ManagerName " +
                        "FROM HR_EmployeePeapMaster PM  " +
                        "INNER JOIN dbo.Hr_PersonalDetails PD ON PM.EMPID=PD.EMPID  " +
                        "LEFT JOIN dbo.HR_PeapCategory PC ON PC.PID= PM.PeapCategory  " +
                        "LEFT JOIN POSITION P ON P.POSITIONID=PD.POSITION  " +
                        "LEFT JOIN HR_Department D ON D.DeptId=PD.DEPARTMENT  " +
                        "WHERE PM.PeapId = " + PeapID.ToString() + " ";

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);


        if (dt.Rows.Count > 0)
        {


            lblPeapLevel.Text = dt.Rows[0]["PeapLevel"].ToString();
            txtOccasion.Text = dt.Rows[0]["Occasion"].ToString();
            txtFirstName.Text = dt.Rows[0]["FIRSTNAME"].ToString();
            txtLastName.Text = dt.Rows[0]["FAMILYNAME"].ToString();
            lblAppraiserName.Text = "|  Manager Name : " + dt.Rows[0]["ManagerName"].ToString();
            ViewState["ManagerName"] = dt.Rows[0]["ManagerName"].ToString();

            int PStatus = Common.CastAsInt32(dt.Rows[0]["Status"]);
            switch (PStatus)
            {
                case -1:
                    lblPeapStatus.Text = "PEAP Cancelled";
                    break;
                case 0:
                    lblPeapStatus.Text = "Self Assessment";
                    break;
                case 1:
                    lblPeapStatus.Text = "Self Assessment";
                    break;
                case 2:
                    lblPeapStatus.Text = "Assessment by Appraiser";
                    break;
                case 3:
                    lblPeapStatus.Text = "Assessment by Management";
                    break;
                case 4:
                    lblPeapStatus.Text = "PEAP Closed";
                    break;
                case 5:
                    lblPeapStatus.Text = "Assessment by Management";
                    break;
                case 6:
                    lblPeapStatus.Text = "Assessment by Management";
                    break;
                default:
                    lblPeapStatus.Text = "NA";
                    break;
            }
            lblPeapStatus.Text = "Current Status : " + lblPeapStatus.Text;

            if (Mode == "A")
            {
                btnSave.Visible = false;
            }
            else
            {
                if (dt.Rows[0]["Status"].ToString() == "5" && EmpID.ToString() == ManagerId.ToString())
                {
                    btnSave.Visible = true;                    
                }
                else
                {
                    btnSave.Visible = false;
                }
            }

        }

        string MFBSQL = "SELECT ManagerRemarks, REPLACE(CONVERT(VARCHAR(12), ManagerAppOn,106),' ','-') AS ManagerAppOn " +
                        "FROM HR_EmployeePeap_ManagementFeedBack " +
                        "WHERE PeapId = " + PeapID.ToString() + " AND ManagerId = " + ManagerId.ToString() + " ";
        
        DataTable dtMFB = Common.Execute_Procedures_Select_ByQueryCMS(MFBSQL);

        txtMDRemarks.Text = dtMFB.Rows[0]["ManagerRemarks"].ToString();


    }

    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Emtm_PeapSummary.aspx?PeapID=" + PeapID.ToString() + "&Mode=" + Mode);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtMDRemarks.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter remark.";
            txtMDRemarks.Focus();
            return;
        }

        string SQL = "UPDATE HR_EmployeePeap_ManagementFeedBack SET ManagerRemarks = '" + txtMDRemarks.Text.Trim().Replace("'","''") + "' , ManagerAppOn = GETDATE() WHERE PeapId = " + PeapID.ToString() + " AND ManagerId = " + ManagerId.ToString() + " ; SELECT -1 ";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);

        if (dt.Rows.Count > 0)
        {
            Common.Set_Procedures("HR_Peap_CheckFilledManagers");
            Common.Set_ParameterLength(1);
            Common.Set_Parameters(new MyParameter("@PEAPID", PeapID));

            DataSet ds = new DataSet();

            if (Common.Execute_Procedures_IUD_CMS(ds))
            {
                ShowRecord();
                lblMsg.Text = "Remarks saved successfully.";
            }
            else
            {
                lblMsg.Text = "Unable to save remarks.";
                return;
            }
        }
        else
        {
            lblMsg.Text = "Unable to save remarks.";
            return;
        }

        string checkMailNotify = "SELECT [Status] FROM HR_EmployeePeapMaster WHERE PeapId =" + PeapID.ToString();
        DataTable dtCheckMail = Common.Execute_Procedures_Select_ByQueryCMS(checkMailNotify);

        if (dtCheckMail.Rows[0]["Status"].ToString() == "6")
        {
            string Mess = SendMail();
        }

    }

    //----------- Mail Code

    public string SendMail()
    {
        string ReplyMess = "";
        string MailFrom = "", MailTo = "emanager@energiossolutions.com";        

        string sqlGetEmployeeInfo = "select pd.EmpCode,pd.FirstName +' ' + pd.MiddleName +' ' + pd.FamilyName as [Name], pd.Office,pd.Department,c.OfficeName,pd.Position,dept.DeptName,p.PositionName, " +
                                    "( SELECT REPLACE(CONVERT(VARCHAR(12),PeapPeriodFrom,106),' ','-') + ' - ' + REPLACE(CONVERT(VARCHAR(12),PeapPeriodTo,106),' ','-') FROM HR_EmployeePeapMaster WHERE PeapId = " + PeapID.ToString() + " ) AS Period, " +
                                    "(SELECT PositionName FROM Position AP WHERE AP.PositionId = (SELECT Position FROM Hr_PersonalDetails APD WHERE APD.EmpId = " + ManagerId.ToString() + ")) AS AppPosition " +
                                    "from Hr_PersonalDetails pd  " +
                                    "left outer join Position p on pd.Position=p.PositionId  " +
                                    "Left Outer Join Office c on pd.Office= c.OfficeId  " +
                                    "Left Outer Join HR_Department dept on pd.Department=dept.DeptId " +
                                    "WHERE pd.EmpId = (SELECT EmpId FROM HR_EmployeePeapMaster WHERE PeapId = " + PeapID.ToString() + " ) ";

        DataTable dtGetEmployeeInfo = Common.Execute_Procedures_Select_ByQueryCMS(sqlGetEmployeeInfo);

        if (dtGetEmployeeInfo != null)
            if (dtGetEmployeeInfo.Rows.Count > 0)
            {
                DataRow drGetEmployeeInfo = dtGetEmployeeInfo.Rows[0];
                //Sending mails
                char[] Sep = { ';' };
                //string[] ToAdds = MailTo.ToString().Split(Sep);
                //string[] CCAdds = MailTo.ToString().Split(Sep);
                string[] BCCAdds = "".Split(Sep);
                //----------------------
                String Subject = "PEAP for " + drGetEmployeeInfo["Name"].ToString() + " - (" + drGetEmployeeInfo["Period"].ToString() + ") by Management";
                String MailBody;


                MailBody = "<br><br>PEAP for <b> " + drGetEmployeeInfo["Name"].ToString() + " /" + drGetEmployeeInfo["PositionName"].ToString() + "</b>  is completed by Management and ready to close.";
                MailBody = MailBody + "<br><br>Thank You.";
                MailBody = MailBody + "<br>____________________________";
                //MailBody = MailBody + "<br>" + drGetEmployeeInfo["Name"].ToString() + "<br><font color=000080 size=2 face=Century Gothic><strong>" + MailFrom.ToString() + "</strong></font>";
                MailBody = MailBody + "<br>" + UppercaseWords(ViewState["ManagerName"].ToString()) ;
                MailBody = MailBody + "<br>" + drGetEmployeeInfo["AppPosition"].ToString();

                //------------------
                string AttachmentFilePath = "";
                //SendEmail.SendeMail(EmpID, MailFrom.ToString(), MailFrom.ToString(), ToAdds, CCAdds, BCCAdds, Subject, MailBody, out ReplyMess, AttachmentFilePath);
                //SendeMail(MailFrom, ToAdds, CCAdds, Subject, MailBody, AttachmentFilePath, "");
                string[] ToAdds = { "emanager@energiossolutions.com" };
                SendEmail.SendeMail(Convert.ToInt32(Session["loginid"].ToString()), MailFrom.ToString(), MailFrom.ToString(), ToAdds, BCCAdds, BCCAdds, Subject, MailBody, out ReplyMess, AttachmentFilePath);

            }

        return ReplyMess;
    }
    static string UppercaseWords(string value)
    {
        char[] array = value.ToCharArray();
        // Handle the first letter in the string.
        if (array.Length >= 1)
        {
            if (char.IsLower(array[0]))
            {
                array[0] = char.ToUpper(array[0]);
            }
        }
        // Scan through the letters, checking for spaces.
        // ... Uppercase the lowercase letters following spaces.
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i - 1] == ' ')
            {
                if (char.IsLower(array[i]))
                {
                    array[i] = char.ToUpper(array[i]);
                }
            }
        }
        return new string(array);
    }
    public void SendeMail(string MailFrom, string[] ToAddresses, string[] CCAddresses, string Subject, string BodyContent, string AttachMentPath, string MailDetails)
    {
        MailMessage objMessage = new MailMessage();
        SmtpClient objSmtpClient = new SmtpClient();
        StringBuilder msgFormat = new StringBuilder();

        try
        {
            //if (chkTest.Checked)
            //{
            //objMessage.From = new MailAddress("emanager@energiossolutions.com");
            //objMessage.To.Add("emanager@energiossolutions.com");
            //objMessage.To.Add("emanager@energiossolutions.com");
            //objMessage.To.Add("emanager@energiossolutions.com");
            //objMessage.To.Add("emanager@energiossolutions.com");

            //objSmtpClient.Host = "smtp.gmail.com";
            //objSmtpClient.Port = 25;
            //objSmtpClient.EnableSsl = true;
            //objSmtpClient.Credentials = new NetworkCredential("abc", "xyz");
            //}
            //else
            //{
            if (MailDetails == "Accident Notification Mail")
            {
                objMessage.Bcc.Add("emanager@energiossolutions.com");
            }
            objMessage.From = new MailAddress(SenderAddress);
            objSmtpClient.Credentials = new NetworkCredential(SenderUserName, SenderPass);
            objSmtpClient.Host = MailClient;
            objSmtpClient.Port = Port;

            foreach (string add in ToAddresses)
            {
                objMessage.To.Add(add);
            }
            if (CCAddresses != null)
            {
                foreach (string add in CCAddresses)
                {
                    objMessage.CC.Add(add);
                }
            }
            //}
            
            objMessage.Body = BodyContent;
            objMessage.Subject = Subject;
            objMessage.IsBodyHtml = true;
            objMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            if (File.Exists(AttachMentPath))
                objMessage.Attachments.Add(new System.Net.Mail.Attachment(AttachMentPath));
            objSmtpClient.Send(objMessage);
            
        }
        catch (System.Exception ex)
        {

            lblMsg.Text = "Error while sending mail." + ex.Message;            
        }
    }
}