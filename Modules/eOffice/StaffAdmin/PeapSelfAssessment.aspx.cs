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

public partial class Emtm_PeapSelfAssessment : System.Web.UI.Page
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
    public int QId
    {
        get
        {
            return Common.CastAsInt32(ViewState["QId"]);
        }
        set
        {
            ViewState["QId"] = value;
        }
    }    
    public string Mode
    {
        get
        {
            return ""+ViewState["Mode"].ToString();
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
        lblMsg_Notify.Text = "";
        lblMsg.Text = "";

        if (!Page.IsPostBack)
        {
            if (Request.QueryString["PeapID"] != null || Request.QueryString["PeapID"].ToString() != "")
            {
                Mode = Request.QueryString["LoginMode"].ToString(); 
                PeapID = Common.CastAsInt32(Request.QueryString["PeapID"].ToString());
                EmpID = Common.CastAsInt32(Session["ProfileId"]);

                ShowRecord();
                BindSelfAssessment();
                BindSAQuestionWithAnswers();
                ShowNotify();
            }
        }
    }
    //------------------ Events
    //------------------ Function
    public void ShowRecord()
    {
        string strSQL = "SELECT PEAPID,CATEGORY As PeapLevel,CASE Occasion WHEN 'R' THEN 'Routine' WHEN 'I' THEN 'Interim' ELSE '' END AS Occasion, POSITIONNAME,EMPCODE,FIRSTNAME , FAMILYNAME,D.DeptName AS DepartmentName, Replace(Convert(varchar(11),PM.SAOnDt,106),' ','-') AS SAOnDt, " +
                        "Replace(Convert(Varchar,PM.PEAPPERIODFROM,106),' ','-') AS PEAPPERIODFROM ,Replace(Convert(Varchar,PM.PEAPPERIODTO,106),' ','-') AS PEAPPERIODTO,Replace(Convert(Varchar,PD.DJC,106),' ','-') AS DJC,PM.Status " +
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
            //lblPeapPeriod.Text = dt.Rows[0]["PEAPPERIODFROM"].ToString() + " To " + dt.Rows[0]["PEAPPERIODTO"].ToString();
            lblUpdatedOn.Text = "Updated On : " + dt.Rows[0]["SAOnDt"].ToString();
                        
            int PStatus=Common.CastAsInt32(dt.Rows[0]["Status"]);
            
            if (PStatus == 0 && Mode != "A")
            {
                btnNotify.Visible = true;
            }
            if (PStatus != 0)
            {
                tdQuestions.Visible = false;
            }
        }

    }
    //--------------- Bind All Grids --------------------------
    public void BindSelfAssessment()
    {
        string sql = "SELECT  Row_Number() over(order by QID)Srno,* FROM dbo.HR_EmployeePeapSADetails WHERE PEAPID=" + PeapID + " ORDER BY QID";
        DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        rptSAQuestions.DataSource = Dt;
        rptSAQuestions.DataBind();
        
    }

    public void BindSAQuestionWithAnswers()
    {
        string sql = "SELECT  Row_Number() over(order by QID)Srno,*,replace(Answer,'\r\n','<br>') AS Answer1 FROM dbo.HR_EmployeePeapSADetails WHERE PEAPID=" + PeapID + " AND Answer <> ''  ORDER BY QID ";
        DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        rptQuesAnswer.DataSource = Dt;
        rptQuesAnswer.DataBind();

    }

    //public Boolean ShowNotify()
    //{
    //    string sql = "SELECT (SELECT Count(*) FROM HR_EmployeePeapSADetails WHERE PEAPID= " + PeapID + " ) AS TotalQues ,(SELECT Count(*) FROM HR_EmployeePeapSADetails WHERE PEAPID= " + PeapID + " AND Answer <> '' ) AS TotalAnswered ";            
    //    DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);

    //    if (Dt.Rows[0]["TotalAnswered"].ToString().Trim() == "0")
    //    {
    //        lblMsg_Notify.Text = "Please answer all the questions before notify.";
    //        return false;
    //    }
    //    if (Dt.Rows[0]["TotalQues"].ToString().Trim() != Dt.Rows[0]["TotalAnswered"].ToString().Trim())
    //    {
    //        lblMsg_Notify.Text = "Please answer all the questions before notify.";
    //        return false;
    //    }

    //    return true;
    //}

    public void ShowNotify()
    {
        if (Mode.ToString() == "A")
        {
            btnNotify.Visible = false;
        }
        else
        {
            string checkSQL = "SELECT [Status] FROM HR_EmployeePeapMaster WHERE PeapId = " + PeapID.ToString() + " ";
            DataTable dtCheck = Common.Execute_Procedures_Select_ByQueryCMS(checkSQL);

            if (dtCheck.Rows[0]["Status"].ToString() == "0")
            {

                string sql = "SELECT (SELECT Count(*) FROM HR_EmployeePeapSADetails WHERE PEAPID= " + PeapID + " ) AS TotalQues ,(SELECT Count(*) FROM HR_EmployeePeapSADetails WHERE PEAPID= " + PeapID + " AND Answer <> '' ) AS TotalAnswered ";
                DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);

                if (Dt.Rows[0]["TotalQues"].ToString().Trim() == Dt.Rows[0]["TotalAnswered"].ToString().Trim())
                {
                    btnNotify.Visible = true;
                }
                else
                {
                    btnNotify.Visible = false;
                }
            }
            else
            {
                btnNotify.Visible = false;
            }
        }
    }

    protected void lbQuestion_Click(object sender, EventArgs e)
    {
        if (Mode != "A")
        {
            QId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);

            if (QId != 0)
            {
                string sql = "SELECT  *,(select guidance from HR_SelfAppraisal a where a.qid=p.qid) as guidance FROM dbo.HR_EmployeePeapSADetails p WHERE PEAPID=" + PeapID + " AND QId = " + QId.ToString() + " ";
                DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);

                lblSA_Question.Text = Dt.Rows[0]["Qtext"].ToString();
                txtSA_Answer.Text = Dt.Rows[0]["Answer"].ToString();
                lblGuidance.Text = Dt.Rows[0]["guidance"].ToString();

                dvSubmitAnswer.Visible = true;
                btnCancel.Text = "Cancel";
            }
        }
    }

    protected void btnSaveSA_Click(object sender, EventArgs e)
    {
        string FileName = "";

        if (txtSA_Answer.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter 'NONE' if no details available.";
            txtSA_Answer.Focus();
            return;
        }

        if (this.FileUpload1 != null && this.FileUpload1.FileContent.Length > 0)
        {
            HttpPostedFile file = FileUpload1.PostedFile;

            string ext = Path.GetExtension(FileUpload1.FileName);
            //if (ext.ToUpper() == ".JPG" || ext.ToUpper() == ".JPEG")
            //{ }
            //else
            //{
            //   lblMsg.Text = "Uploading file type should be jpg only.";
            //   return;
            //}

            FileName = "Peap_" + EmpID.ToString() + "_" + PeapID.ToString() + "_" + QId.ToString() + ext;

        }

        string strUpdate = "";

        if (this.FileUpload1 != null && this.FileUpload1.FileContent.Length > 0)
        {
            strUpdate = "UPDATE HR_EmployeePeapSADetails SET  Answer = '" + txtSA_Answer.Text.Trim().Replace("'","''") + "', FileName = '" + FileName + "' WHERE PeapId = " + PeapID.ToString() + " AND QID = " + QId.ToString() + " ; UPDATE HR_EmployeePeapMaster SET SAOnDt = GETDATE() WHERE PeapId = " + PeapID.ToString() + " ; SELECT -1; ";
        }
        else
        {
            strUpdate = "UPDATE HR_EmployeePeapSADetails SET  Answer = '" + txtSA_Answer.Text.Trim().Replace("'", "''") + "'  WHERE PeapId = " + PeapID.ToString() + " AND QID = " + QId.ToString() + " ; UPDATE HR_EmployeePeapMaster SET SAOnDt = GETDATE() WHERE PeapId = " + PeapID.ToString() + " ; SELECT -1; ";
        }

        DataTable dtResult = Common.Execute_Procedures_Select_ByQueryCMS(strUpdate);

        if (dtResult.Rows.Count > 0)
        {
            
                if (this.FileUpload1 != null && this.FileUpload1.FileContent.Length > 0)
                {
                    try
                         {
                             if (File.Exists(Server.MapPath("~/EMANAGERBLOB/Peap/" + FileName)))
                             {
                                 File.Delete(Server.MapPath("~/EMANAGERBLOB/Peap/" + FileName));
                             }
                            
                             FileUpload1.SaveAs(Server.MapPath("~/EMANAGERBLOB/Peap/" + FileName));

                            ShowRecord();
                            BindSelfAssessment();
                            BindSAQuestionWithAnswers();
                            ShowNotify();
                            lblMsg.Text = "Record saved successfully.";
                            btnCancel.Text = "Close";
                         }
                        catch (Exception ex)
                         {
                            lblMsg.Text = "Unable to Save Record.ERROR : " + ex.Message;
                         }

                }
                else
                {
                    ShowRecord();
                    BindSelfAssessment();
                    BindSAQuestionWithAnswers();
                    ShowNotify();
                    lblMsg.Text = "Record saved successfully.";
                    btnCancel.Text = "Close";
                }
        }
        
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtSA_Answer.Text = "";
        lblSA_Question.Text = "";
        QId = 0;

        dvSubmitAnswer.Visible = false;
    }

    protected void btnNotify_Click(object sender, EventArgs e)
    {
        //if (!ShowNotify())
        //{
        //    return;
        //}

        string Update = "UPDATE HR_EmployeePeapMaster SET Status = 1 WHERE PeapId = " + PeapID.ToString() + " ; SELECT -1;";
        DataTable Result = Common.Execute_Procedures_Select_ByQueryCMS(Update);

        if (Result.Rows.Count > 0)
        {
            string Mess = SendMail();
            lblMsg_Notify.Text = "Notified for forward successfully.";
            btnNotify.Visible = false;   
        }
        else
        {
            lblMsg_Notify.Text = "Unable to notify.";
        }

    }

    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Emtm_PeapSummary.aspx?PeapID=" + PeapID.ToString() + "&Mode=" + Mode);
    }

    public string SendMail()
    {
        string ReplyMess = "";
        string MailFrom = "", MailTo = "pankaj.v@esoftech.com";
        //Mail From
        //string sqlGetMailFrom = "SELECT pd.EmpID,C.Email FROM Hr_PersonalDetails pd LEFT OUTER JOIN USERLOGIN C ON pd.userid=C.LoginId " +
        //                        "WHERE pd.EmpID=" + EmpID;



        //DataTable dtGetMailFrom = Common.Execute_Procedures_Select_ByQueryCMS(sqlGetMailFrom);
        //if (dtGetMailFrom != null)
        //    if (dtGetMailFrom.Rows.Count > 0)
        //    {
        //        DataRow drGetMailFrom = dtGetMailFrom.Rows[0];
        //        MailFrom = drGetMailFrom["Email"].ToString();
        //    }

        //String EmpFullName = "", EmpPosition = "";
        //string sqlFullNameNPosition = "SELECT (select PositionName from Position P where P.PositionID=pd.Position)Position,(pd.FirstName+' '+pd.FamilyName )as UserName FROM Hr_PersonalDetails pd LEFT OUTER JOIN USERLOGIN C ON pd.userid=C.LoginId where pd.EmpID=" + EmpID + "";
        //DataTable dtNamePos = Common.Execute_Procedures_Select_ByQueryCMS(sqlFullNameNPosition);
        //if (dtNamePos != null)
        //    if (dtNamePos.Rows.Count > 0)
        //    {
        //        EmpPosition = dtNamePos.Rows[0][0].ToString();
        //        EmpFullName = dtNamePos.Rows[0][1].ToString();
        //    }



        //Mail To
        
        string sqlGetEmployeeInfo = "select pd.EmpCode,pd.FirstName +' ' + pd.MiddleName +' ' + pd.FamilyName as [Name], pd.Office,pd.Department,c.OfficeName,pd.Position,dept.DeptName,p.PositionName, " +
                                    "( SELECT REPLACE(CONVERT(VARCHAR(12),PeapPeriodFrom,106),' ','-') + ' - ' + REPLACE(CONVERT(VARCHAR(12),PeapPeriodTo,106),' ','-') FROM HR_EmployeePeapMaster WHERE PeapId = " + PeapID.ToString() + " ) AS Period " +
                                    "from Hr_PersonalDetails pd  " +
                                    "left outer join Position p on pd.Position=p.PositionId  " + 
                                    "Left Outer Join Office c on pd.Office= c.OfficeId  " +
                                    "Left Outer Join HR_Department dept on pd.Department=dept.DeptId " + 
                                    "WHERE pd.EmpId = " + EmpID.ToString() + " ";

        DataTable dtGetEmployeeInfo = Common.Execute_Procedures_Select_ByQueryCMS(sqlGetEmployeeInfo);
        if (dtGetEmployeeInfo != null)
            if (dtGetEmployeeInfo.Rows.Count > 0)
            {
                DataRow drGetEmployeeInfo = dtGetEmployeeInfo.Rows[0];
                //Sending mails
                char[] Sep = { ';' };
                string[] ToAdds = MailTo.ToString().Split(Sep);
                string[] CCAdds = MailTo.ToString().Split(Sep);
                string[] BCCAdds = "".Split(Sep);
                //----------------------
                String Subject = drGetEmployeeInfo["Name"].ToString() + " - Self Appraisal (" + drGetEmployeeInfo["Period"].ToString() + ")";
                String MailBody;

                //MailBody = "EmployeeName: " + drGetEmployeeInfo["Name"].ToString() + " || Position:" + drGetEmployeeInfo["PositionName"].ToString() + " || Department: " + drGetEmployeeInfo["DeptName"].ToString() + "";
                MailBody = "<br>Self appraisal is completed.";
                //MailBody = MailBody + "<br><br>Please forward to appraisers.";
                MailBody = MailBody + "<br><br>Thank You.";
                //MailBody = MailBody + "<br>" + drGetEmployeeInfo["Name"].ToString() + "<br><font color=000080 size=2 face=Century Gothic><strong>" + MailFrom.ToString() + "</strong></font>";
                //MailBody = MailBody + "<br>" + UppercaseWords(EmpFullName);
                //MailBody = MailBody + "<br>" + EmpPosition + "<br><font color=000080 size=2 face=Century Gothic><strong>" + MailFrom.ToString() + "</strong></font>";

                //------------------
                string AttachmentFilePath = "";
                //SendEmail.SendeMail(EmpID, MailFrom.ToString(), MailFrom.ToString(), ToAdds, CCAdds, BCCAdds, Subject, MailBody, out ReplyMess, AttachmentFilePath);
                SendeMail(MailFrom, ToAdds, CCAdds, Subject, MailBody, AttachmentFilePath, "");

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
            //objMessage.From = new MailAddress("pankaj.k@esoftech.com");
            //objMessage.To.Add("pankaj.k@esoftech.com");
            //objMessage.To.Add("asingh@energiossolutions.com");
            //objMessage.To.Add("asingh@energiossolutions.com");
            //objMessage.To.Add("asingh@energiossolutions.com");

            //objSmtpClient.Host = "smtp.gmail.com";
            //objSmtpClient.Port = 25;
            //objSmtpClient.EnableSsl = true;
            //objSmtpClient.Credentials = new NetworkCredential("pankaj.k@esoftech.com", "pankajesoft99");
            //}
            //else
            //{
            if (MailDetails == "Accident Notification Mail")
            {
                objMessage.Bcc.Add("asingh@energiossolutions.com");
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
            
            lblMsg_Notify.Text = "Notified for forward successfully.";
        }
        catch (System.Exception ex)
        {
           lblMsg_Notify.Text = "Error while sending mail." + ex.Message;
        }
    }
}
