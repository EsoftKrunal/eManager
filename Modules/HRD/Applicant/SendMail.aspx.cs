using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Net.Mail;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using System.Text.RegularExpressions;



public partial class SendMail : System.Web.UI.Page
{
    public int CandidateID
    {
        set { ViewState["_CandidateID"] = value; }
        get { return Common.CastAsInt32(ViewState["_CandidateID"]); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!(IsPostBack))
        {
            CandidateID = int.Parse("0" + Request.QueryString["CandidateID"]);
            txtFrom.Text = ConfigurationManager.AppSettings["FromAddress"].ToString();
            //BindCCDropdown();
            LoadApplicantMail();  
        }
    }
    private void LoadApplicantMail()
    {
        string sql = " Select (FirstName+ ' ' +MiddleName+ ' ' +LastName)ApplicantName,DateOfBirth,PassportNo,AvailableFrom,ReqRemarks,AppSentOn,AppRemarks,AppOn,Status,RejRemarks,RejBy,RejOn,APPROVALID " +
                     " ,(Select Email from DBO.UserLogin UL Where UL.LoginID=CPD.AppSentBy)AppSentBy  "+
                     " ,(Select FirstName+' '+LastName from DBO.UserLogin UL Where UL.LoginID=CPD.AppSentBy)AppSentByName "+

                     " ,(Select FirstName+' '+LastName from DBO.UserLogin UL Where UL.LoginID=CPD.AppBy)AppBy " +
                     " ,(Select FirstName+' '+LastName from DBO.UserLogin UL Where UL.LoginID=CPD.RejBy)RejBy " +

                     " ,(Select RankCode from DBO.Rank R Where R.RankID=CPD.RankAppliedID )RankApplied "+
                     " ,(Select NationalityName  from DBO.Country C Where C.CountryID=CPD.NationalityID )Nationality " +
                     " from Dbo.candidatepersonaldetails CPD Where CandidateID="+CandidateID+"";

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        DataRow Dr = dt.Rows[0];
        //==============================================
        lblHeader.Text = "Applicant Approval/Rejection Notification Mail";
        txtTo.Text = Dr["AppSentBy"].ToString();
        txtBCC.Text = "";
        txtSubject.Text = "";

        StringBuilder MailContent = new StringBuilder();
        MailContent.Append("<table cellpadding='1' cellspacing='1' border='0' width='100%' style='text-align:left;font-family:Verdana;font-weight:bold; color:#606060;'><col width='150px' /><col width='5px' /><col />");
        MailContent.Append("<tr><td> Applicant Name </td><td>:</td><td> " + Dr["ApplicantName"].ToString().ToUpper() + " </td></tr>");
        MailContent.Append("<tr><td> DOB </td><td>:</td><td>" + Common.ToDateString(Dr["DateOfBirth"]).ToUpper() + "</td></tr>");
        MailContent.Append("<tr><td> Passport# </td><td>:</td><td> " + Dr["PassportNo"].ToString().ToUpper() + "</td></tr>");
        MailContent.Append("<tr><td> Rank Applied </td><td>:</td><td> " + Dr["RankApplied"].ToString().ToUpper() + "</td></tr>");
        MailContent.Append("<tr><td> Available From </td><td>:</td><td> " + Common.ToDateString(Dr["AvailableFrom"]).ToUpper() + "</td></tr>");
        MailContent.Append("<tr><td> Nationality </td><td>:</td><td> " + Dr["Nationality"].ToString().ToUpper() + "</td></tr>");

        //MailContent.Append("<tr><td> &nbsp; </td><td></td><td> </td></tr>");
        //MailContent.Append("<tr><td> &nbsp; </td><td></td><td> </td></tr>");

        //MailContent.Append("<tr><td> Request Comments </td><td>:</td><td> " + Dr["ReqRemarks"].ToString().ToUpper() + "</td></tr>");
        //MailContent.Append("<tr><td> Request By/On </td><td>:</td><td> " + Dr["AppSentByName"].ToString().ToUpper() + " / " + Common.ToDateString(Dr["AppSentOn"]).ToUpper() + "</td></tr>");

        MailContent.Append("<tr><td> &nbsp; </td><td></td><td> </td></tr>");
        MailContent.Append("<tr><td> &nbsp; </td><td></td><td> </td></tr>");

        if (Dr["Status"].ToString() == "3")
        {
            MailContent.Append("<tr><td> Approval Comments </td><td>:</td><td> " + Dr["AppRemarks"].ToString().ToUpper() + "</td></tr>");
            MailContent.Append("<tr><td> Approve By/On </td><td>:</td><td> " + Dr["AppBy"].ToString().ToUpper() + " / " + Common.ToDateString(Dr["AppOn"]).ToUpper() + "</td></tr>");
            txtSubject.Text = "Applicant Approved [" + Dr["ApplicantName"].ToString().ToUpper() + "] , Approval No : " + Dr["APPROVALID"].ToString().ToUpper();
        }
        else if (Dr["Status"].ToString() == "4" || Dr["Status"].ToString() == "5")
        {
            MailContent.Append("<tr><td> Rejection Comments </td><td>:</td><td> " + Dr["RejRemarks"].ToString().ToUpper() + "</td></tr>");
            MailContent.Append("<tr><td> Rejected By/On </td><td>:</td><td> " + Dr["RejBy"].ToString().ToUpper() + " / " + Common.ToDateString(Dr["RejOn"]).ToUpper() + "</td></tr>");
        }


        MailContent.Append("</table>");        
        //MailContent = MailContent.Replace("$RFQLINK$", URl);
        dvApplicantDetails.InnerHtml = MailContent.ToString();


    }
    private void ValidateEmail(string email)
    {
        
        Regex regex = new Regex(@"^((\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)\s*[;]{0,1}\s*)+$");
        Match match = regex.Match(email);
        if (! match.Success)
            lblMessage.Text = "Invalid CC Email Address";
        
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        if (txtTo.Text.Trim() == "")
        {
            lblMessage.Text = "Please fill To mail address field."; return;
        }
        if (txtSubject.Text.Trim() == "")
        {
            lblMessage.Text = "Please fill subject field."; return;
        }

        if (!string.IsNullOrEmpty(txtCC.Text))
        {
            ValidateEmail(txtCC.Text);
            txtCC.Focus();
        }

        string str = hfdMessage.Value;
        litMessage.Text = str;
        char[] Sep = { ';' };
        string[] ToAdds = txtTo.Text.Split(Sep);
        //string[] ToAdds = {"pankaj.k@esoftech.com","asingh@energiossolutions.com"};
        string[] CCAdds = txtCC.Text.Split(Sep);
        string[] BCCAdds = txtBCC.Text.Split(Sep);
        //------------------
        string ErrMsg = "";
        string AttachmentFilePath = "";

        
        //----------
        if (SendEmail.SendeMail(Common.CastAsInt32(Session["loginid"]), txtFrom.Text.Trim(), txtFrom.Text.Trim(), ToAdds, CCAdds, BCCAdds, txtSubject.Text.Trim(), litMessage.Text, out ErrMsg, AttachmentFilePath))
        {
           // Budget.getTable("UPDATE DBO.CANDIDATEPERSONALDETAILS SET NotificationContent='" + litMessage.Text.Trim().Replace("'", "''") + "' WHERE CANDIDATEID=" + CandidateID.ToString());
            lblMessage.Text = "Mail sent successfully.";
        }
        else
        {
            lblMessage.Text = "Unable to send Mail. Error : " + ErrMsg;
        }
    }
    //private void BindCCDropdown()
    //{
    //    string sql = "select distinct email from DBO.emtm_department where DEPTID in(8,15,22) And email is not null order by email";
    //    DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);

    //    ddlCC.DataSource = dt;
    //    ddlCC.DataTextField = "email";
    //    ddlCC.DataValueField = "email";
    //    ddlCC.DataBind();

    //    ddlCC.Items.Insert(0, new ListItem("None", ""));
    //}
}
