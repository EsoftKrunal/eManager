using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Text;
using System.Net.Mail;

public partial class Emtm_Profile_TalkToMD : System.Web.UI.Page
{
    bool MailActive = true;

    public int UserId
    {
        get { return Common.CastAsInt32(ViewState["UserId"]); }
        set { ViewState["UserId"] = value; }
    }
    public int ThreadID
    {
        get { return Common.CastAsInt32(ViewState["ThreadID"]); }
        set { ViewState["ThreadID"] = value; }
    }
    public int CommentId
    {
        get { return Common.CastAsInt32(ViewState["CommentId"]); }
        set { ViewState["CommentId"] = value; }
    }
    Random r = new Random();
    string[] TextColorOptions = { "#000000", "#FFA500", "#A52A2A", "#800000", "#008000", "#808000", "#FF00FF", "#800080", "#0000A0", "#0000FF", "#FF0000" }; 

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //--------------------
        try
        {
            string s = Session["loginid"].ToString();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "aa", "alert('Your session has been expired.Please login again.');window.close();", true);
        }
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        if (!Page.IsPostBack)
        {
            UserId = Common.CastAsInt32(Session["loginid"]);
            string sql = "SELECT * FROM OFFICE OFFICENAME";
            chkOffice.DataSource = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            chkOffice.DataValueField= "OFFICEID";
            chkOffice.DataTextField = "OFFICENAME";
            chkOffice.DataBind();

            BindCRM();
        }
    }
    protected void btnViewDiscussion_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        ThreadID = Common.CastAsInt32(btn.CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM HR_ThreadMaster WHERE THREADID=" + ThreadID);
        if (dt.Rows.Count > 0)
            lblTopic.Text = dt.Rows[0]["Topic"].ToString();
        ShowDiscussion();
        ShowClosure();
    }
    protected void btnClip_OnClick(object sender, EventArgs e)
    {
        int CommentId = Common.CastAsInt32(txtCommId.Text);
        DataTable RetValue = Common.Execute_Procedures_Select_ByQueryCMS("select * from HR_ThreadComments where CommentId=" + CommentId + "");
        if(RetValue.Rows.Count>0)
        {
            string FileName=RetValue.Rows[0]["AttachmentName"].ToString();
            byte[] ret = (byte[])RetValue.Rows[0]["AttachmentFile"];
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", FileName));
            Response.ContentType = "application/" + Path.GetExtension(FileName).Substring(1);
            Response.BinaryWrite(ret);
        }
    }
    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        BindCRM();
    }

    public void BindCRM()
    {
        string sql = "";

        if(Common.CastAsInt32(Session["loginid"])==1)
            sql="SELECT Row_Number() over(order by StartDate desc)RowNumber,TM.* ,U.FIRSTNAME + ' ' + U.LASTNAME AS ORIGIN,(case when closedon is null then 'Open' else 'Closed' end) as Status FROM dbo.HR_ThreadMaster TM  left join dbo.Userlogin u on StartByUserId=LoginId ";
        else
            sql = "SELECT Row_Number() over(order by StartDate desc)RowNumber,TM.* ,U.FIRSTNAME + ' ' + U.LASTNAME AS ORIGIN,(case when closedon is null then 'Open' else 'Closed' end) as Status FROM dbo.HR_ThreadMaster TM  left join dbo.Userlogin u on StartByUserId=LoginId Where ThreadId In (Select ThreadId from dbo.HR_ThreadUsers Where UserId=" + UserId + ") ";
        
        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql + " order by StartDate desc ");
        DataView dv = DT.DefaultView;
        string Filter = txtSearchField.Text.Replace("'", "''").Trim();
        if (txtSearchField.Text != "")
        {
            dv.RowFilter = " ( Topic Like '%" + Filter + "%' ) OR ( Category Like '%" + Filter + "%' ) OR ( ORIGIN Like '%" + Filter + "%' ) ";
        }

        if (DT.Rows.Count > 0)
        {
            lblNoRecords.Visible = false;
            rptCRM.DataSource = dv.ToTable();
            rptCRM.DataBind();
        }
        else
        {
            lblNoRecords.Text = "No Topics(s) Found.";
            lblNoRecords.Visible = true; 
            rptCRM.DataSource = DT;
            rptCRM.DataBind();
        }
        //ShowDiscussion();
    }
    public void ShowDiscussion()
    {
        StringBuilder sb = new StringBuilder();
        bool ThreadOpen = false;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select * FROM HR_ThreadMaster Where ThreadId=" + ThreadID + " And ClosedOn Is NULL");
        if (dt.Rows.Count > 0) 
            ThreadOpen = true;

        dt = Common.Execute_Procedures_Select_ByQueryCMS("select COMMENTID,COMMENTTEXT,COMMENTON,ATTACHMENTNAME,U.FIRSTNAME + ' ' + U.LASTNAME AS CommentBy from [dbo].[HR_ThreadComments] TC  left join dbo.Userlogin u on TC.UserId=LoginId  Where ThreadId=" + ThreadID + " and PrevCommentId=0");
        foreach (DataRow dr in dt.Rows)
        {
            int CommentId = Common.CastAsInt32(dr["CommentId"]);
            StartCommentBox(sb, CommentId,ThreadOpen, dr["CommentBy"], dr["CommentText"], dr["CommentOn"], 1, dr["AttachmentName"]);
            LoadChildComments(sb, ThreadID, CommentId, 1, ThreadOpen);
            EndCommentBox(sb);
        }
        litComments.Text = sb.ToString();
    }
    protected void LoadChildComments(StringBuilder sb, int _ThreadId, int _CommentId, int Level, bool ThreadOpen)
    {
        Level++;
        DataTable dtChilds = Common.Execute_Procedures_Select_ByQueryCMS("select COMMENTID,COMMENTTEXT,COMMENTON,ATTACHMENTNAME,U.FIRSTNAME + ' ' + U.LASTNAME AS CommentBy from [dbo].[HR_ThreadComments] TC  left join dbo.Userlogin u on TC.UserId=LoginId Where ThreadId=" + _ThreadId + " and PrevCommentId=" + _CommentId);
        foreach (DataRow dr in dtChilds.Rows)
        {
            int CommentId = Common.CastAsInt32(dr["CommentId"]);
            StartCommentBox(sb, CommentId,ThreadOpen, dr["CommentBy"], dr["CommentText"], dr["CommentOn"], Level, dr["AttachmentName"]);
            LoadChildComments(sb, _ThreadId, CommentId, Level, ThreadOpen);
            EndCommentBox(sb);
        }
    }
    public void StartCommentBox(StringBuilder sb, int CommentId,bool ThreadOpen, object CommentBy, object CommentText, object CommentOn, int Level, object AttachmentName)
    {
        //TextColorOptions[Common.CastAsInt32((TextColorOptions.Length - 1) * r.NextDouble())]
        string Data = "";
        Data = @"<div class='Comment_Box' style='background-color:hsla(236,45%," + (100 - Level * 10) + "%,0.10);color:" + TextColorOptions[8] + "'>" +
                "<div class='Comment_header'>" +
                        ((ThreadOpen)?"<span class='reply' commentid='" + CommentId + "'><img src='../../Images/reply.png' style='float:left; margin-right:5px;' />Reply</span>" : "") +
                        "<span class='commentby'><img src='../../Images/user.png' style='float:left'/>" + CommentBy.ToString() + "</span>" +
                        "<span class='commenton'>" + Convert.ToDateTime(CommentOn).ToString("dd-MMM-yyyy hh:mm tt") + "</span>" +
                        ((ThreadOpen) ? "<span class='addmember'><img src='../../Images/friend_add_small.png' style='float:left; margin-left:30px;' />Invite Member(s)</span>" : "") +
                "</div>" +
                "<div class='Comment_content'>" +
                    "<img src='../../Images/comments.png' style='float:left; margin-right:10px;' />" +
                    "<span class='commenttext' >" + CommentText.ToString() + "</span>" +
                    ((AttachmentName.ToString().Trim() != "") ? "<img class='attachment' src='../../Images/PaperClip.png' alt='FileName' commentid='" + CommentId + "' />" : "") +
                "</div>";


        sb.Append(Data);
    }
    public void EndCommentBox(StringBuilder sb)
    {
        string Data = "";
        Data = @"</div>";
        sb.Append(Data);
    }
    public void ShowClosure()
    {
        string sql = "select replace(convert(varchar,ClosedOn,106),' ','-')ClosedOn,ClosureComment from HR_ThreadMaster Where ThreadID=" + ThreadID.ToString() + " And ClosedOn IS NOT NULL ";
        DataTable DT = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (DT.Rows.Count > 0)
        {
            dvClosure.Visible = true;
            lblClosedByOn.Text = DT.Rows[0]["ClosedOn"].ToString();
            lblClosureRemarks.Text = DT.Rows[0]["ClosureComment"].ToString();
        }
        else
        {
            dvClosure.Visible = false;
        }
    }
    public string[] GetMails(DataTable dt_RecEmails)
    {
        List<string> lstMails = new List<string>();
        char[] sep = { ';', ',' };
        foreach (DataRow dr in dt_RecEmails.Rows)
        {
            string[] parts = dr["EMAIL"].ToString().Split(sep);
            foreach (string mailpart in parts)
            {
                if (mailpart.Trim() != "")
                {
                    try
                    {
                        MailAddress ma = new MailAddress(mailpart);
                        lstMails.Add(ma.Address);
                    }
                    catch { }
                }
            }
        }
        return lstMails.ToArray();
    }
    public string getInviteMembersList(int _ThreadId)
    {
        string mlist = "";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT FIRSTNAME + ' ' + LASTNAME AS USERNAME FROM USERLOGIN U WHERE U.LOGINID IN ( SELECT USERID FROM HR_ThreadUsers WHERE THREADID=" +  _ThreadId + ")");
        foreach (DataRow dr in dt.Rows)
        {
            mlist +=", "+ dr["USERNAME"].ToString();
        }

        if (mlist.StartsWith(","))
        {
            mlist = mlist.Substring(1);
            mlist = "<br><b>Invited Members : </b>" + mlist + "";
        }

        return mlist;
    }
  

    protected void btnClosure_OnClick(object sender, EventArgs e)
    {

    }
    protected void btnPrint_OnClick(object sender, EventArgs e)
    {
     //   ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "window.open('../Reports/CRM.aspx?Status=" + StatusID + "&Owner=" + lblPageheading.Text + "')", true);
    }
    protected void imgPrintDiscussion_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField hfThreadID = (HiddenField)btn.Parent.FindControl("hfThreadID");
        Session.Add("sThreadIDPrint", hfThreadID.Value);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "", "window.open('../Reports/CRM.aspx')", true);
    }
    
    // Create New Thread
    protected void btnAdd_OnClick(object sender, EventArgs e)
    {
        lblRRHeading.Text = "Create New Topic";
        txtTopic.Text = "";
        txtComment.Text = "";
        txtTopic.Focus();
        dvThread.Visible = true;
    }
    protected void btnCreate_OnClick(object sender, EventArgs e)
    {
        string FileName = "";
        byte[] Filebytes = {};
        if (flpUpload.HasFile)
        {
            FileName = Path.GetFileName(flpUpload.FileName);
            Filebytes = flpUpload.FileBytes;
        }
        Common.Set_Procedures("CreateThread");
        Common.Set_ParameterLength(5);

        Common.Set_Parameters(new MyParameter("@CreatedBy",UserId),
                              new MyParameter("@TopicName",txtTopic.Text.Trim().Replace("'","`")),
                              new MyParameter("@CommentText", txtComment.Text.Trim().Replace("'", "`")),
                              new MyParameter("@AttachmentName", FileName),
                              new MyParameter("@Attachment", Filebytes));
        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD_CMS(ds))
        {
            int _ThreadId = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);
            dvThread.Visible = false;
            BindCRM();
            SendMail_NewThread(_ThreadId);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "sd", "alert('Thread Created Successfully.');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "sd", "alert('Unable to create Thread.');", true);
        }
    }
    protected void btnClose_OnClick(object sender, EventArgs e)
    {
        dvThread.Visible = false;
    }
    public void SendMail_NewThread(int _ThreadId)
    {
        DataTable dt_Topic = Common.Execute_Procedures_Select_ByQueryCMS("select TOPIC FROM HR_ThreadMaster WHERE THREADID=" + _ThreadId);
        if (dt_Topic.Rows.Count > 0)
        {
            string TopicName = dt_Topic.Rows[0]["TOPIC"].ToString();
            DataTable dt_SenderEmail = Common.Execute_Procedures_Select_ByQueryCMS("select FIRSTNAME + ' ' + LASTNAME AS USERNAME,EMAIL from userlogin WHERE LOGINID=" + UserId.ToString());
            DataTable dt_RecEmails = Common.Execute_Procedures_Select_ByQueryCMS("select EMAIL from userlogin WHERE LOGINID in (SELECT USERID FROM HR_ThreadUsers WHERE THREADID=" + _ThreadId + ")");
            string SenderEmail = dt_SenderEmail.Rows[0]["EMAIL"].ToString();
            string SenderName = dt_SenderEmail.Rows[0]["USERNAME"].ToString();
            string[] ToMails = GetMails(dt_RecEmails);
            string[] CCAdds = { };
            string[] BCCAdds = { };
            string[] NOAdds = { };
            ////----------------------
            String Subject = "Hello MD : New Comment";
            String MailBody = "";
            MailBody = MailBody + "<br>";
            MailBody = "You have a new comment in Hello MD.";
            MailBody = MailBody + "<br><br>";
            MailBody = MailBody + "<br><b>From : </b>" + SenderName + "";
            MailBody = MailBody + "<br><b>Date : </b>" + DateTime.Today.ToString("dd-MMM-yyyy") + "";
            MailBody = MailBody + "<br><b>Topic : </b>" + TopicName + "";
            MailBody = MailBody + "<br>";
            MailBody = MailBody + getInviteMembersList(_ThreadId);
            MailBody = MailBody + "<br>";
            MailBody = MailBody + "<br><br>Topic can be accessed from ( EMTM -- Hello MD ) module";
            MailBody = MailBody + "<br>";
            MailBody = MailBody + "<br>Thanks & Best Regards";
            MailBody = MailBody + "<br>" + SenderName;
            ////------------------
            string ErrMsg = "";
            string AttachmentFilePath = "";

            if (MailActive)
                SendEmail.SendeMailAsync(UserId, SenderEmail, SenderEmail, ToMails, CCAdds, BCCAdds, Subject, MailBody, out ErrMsg, AttachmentFilePath);
            else
                SendEmail.SendeMailAsync_TEST(UserId, SenderEmail, SenderEmail, ToMails, CCAdds, BCCAdds, Subject, MailBody, out ErrMsg, AttachmentFilePath);
        }
    }

    // Reply Comments
    protected void btnReply_OnClick(object sender, EventArgs e)
    {
        int CommentId = Common.CastAsInt32(txtCommId.Text);
        dvReply.Visible = true;
        txtReply.Focus();
        DataTable RetValue = Common.Execute_Procedures_Select_ByQueryCMS("select * from HR_ThreadComments where CommentId=" + CommentId + "");
        if (RetValue.Rows.Count > 0)
        {
            dvOrgComment.InnerHtml = RetValue.Rows[0]["CommentText"].ToString();
        }
    }
    protected void btnSave_Reply_OnClick(object sender, EventArgs e)
    {
        string FileName = "";
        byte[] Filebytes = { };
        if (flpReply.HasFile)
        {
            FileName = Path.GetFileName(flpReply.FileName);
            Filebytes = flpReply.FileBytes;
        }
        Common.Set_Procedures("ReplyComment");
        Common.Set_ParameterLength(5);

        Common.Set_Parameters(new MyParameter("@CreatedBy", UserId),
                              new MyParameter("@CommentId", Common.CastAsInt32(txtCommId.Text)),
                              new MyParameter("@CommentText", txtReply.Text.Trim().Replace("'", "`")),
                              new MyParameter("@AttachmentName", FileName),
                              new MyParameter("@Attachment", Filebytes));
        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD_CMS(ds))
        {
            dvReply.Visible = false;
            SendMail_Reply(ThreadID, txtReply.Text.Trim().Replace("'", "`"));
            ShowDiscussion();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "sd", "alert('Comment Replied Successfully.');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "sd", "alert('Unable to Reply.');", true);
        }
    }
    protected void btnClose_Reply_OnClick(object sender, EventArgs e)
    {
        dvReply.Visible = false;
        
    }
    public void SendMail_Reply(int _ThreadId,string CommentText)
    {
        DataTable dt_Topic = Common.Execute_Procedures_Select_ByQueryCMS("select TOPIC FROM HR_ThreadMaster WHERE THREADID=" + _ThreadId);
        if (dt_Topic.Rows.Count > 0)
        {
            string TopicName = dt_Topic.Rows[0]["TOPIC"].ToString();
            
            DataTable dt_SenderEmail = Common.Execute_Procedures_Select_ByQueryCMS("select FIRSTNAME + ' ' + LASTNAME AS USERNAME,EMAIL from userlogin WHERE LOGINID=" + UserId.ToString());
            DataTable dt_RecEmails = Common.Execute_Procedures_Select_ByQueryCMS("select EMAIL from userlogin WHERE LOGINID in (SELECT USERID FROM HR_ThreadUsers WHERE THREADID=" + _ThreadId + ")");
            string SenderEmail = dt_SenderEmail.Rows[0]["EMAIL"].ToString();
            string SenderName = dt_SenderEmail.Rows[0]["USERNAME"].ToString();
            string[] ToMails = GetMails(dt_RecEmails);
            string[] CCAdds = { };
            string[] BCCAdds = { };
            string[] NOAdds = { };
            ////----------------------
            String Subject = "Hello MD";
            String MailBody = "";
            MailBody = MailBody + "<br>";
            MailBody = "You have a new comment in Hello MD.";
            MailBody = MailBody + "<br><br>";
            MailBody = MailBody + "<br><b>From : </b>" + SenderName + "";
            MailBody = MailBody + "<br><b>Date : </b>" + DateTime.Today.ToString("dd-MMM-yyyy") + "";
            MailBody = MailBody + "<br><b>Topic : </b>" + TopicName + "";
            MailBody = MailBody + "<br><b>Comments : </b>" + CommentText + "";
            MailBody = MailBody + "<br>";
            MailBody = MailBody + getInviteMembersList(_ThreadId);
            MailBody = MailBody + "<br>";
            MailBody = MailBody + "<br><br>Topic can be accessed from ( EMTM -- Hello MD ) module";
            MailBody = MailBody + "<br>";
            MailBody = MailBody + "<br>Thanks & Best Regards";
            MailBody = MailBody + "<br>" + SenderName;
            ////------------------
            string ErrMsg = "";
            string AttachmentFilePath = "";

            if (MailActive)
                SendEmail.SendeMailAsync(UserId, SenderEmail, SenderEmail, ToMails, CCAdds, BCCAdds, Subject, MailBody, out ErrMsg, AttachmentFilePath);
            else
                SendEmail.SendeMailAsync_TEST(UserId, SenderEmail, SenderEmail, ToMails, CCAdds, BCCAdds, Subject, MailBody, out ErrMsg, AttachmentFilePath);
        }
    }

    // Invite Members
    protected void btnInvite_OnClick(object sender, EventArgs e)
    {
        int CommentId = Common.CastAsInt32(txtCommId.Text);
        dvMembers.Visible = true;
        chkOffice.Focus();
        DataTable RetValue = Common.Execute_Procedures_Select_ByQueryCMS("select * from HR_ThreadComments where CommentId=" + CommentId + "");
        if (RetValue.Rows.Count > 0)
        {
            dvOrgComment.InnerHtml = RetValue.Rows[0]["CommentText"].ToString();
        }
        BindMembers();
    }
    protected void btnSaveInvite_OnClick(object sender, EventArgs e)
    {
        try
        {
            string UsersList="";
            foreach (RepeaterItem ri in rptMembers.Items)
            {
                CheckBox ch=(CheckBox)ri.FindControl("chkMember");
                if (ch.Checked)
                {
                    int UserId = Common.CastAsInt32(ch.CssClass);
                    UsersList+=","+UserId;
                    Common.Execute_Procedures_Select_ByQueryCMS("INSERT INTO HR_ThreadUsers(THREADID,USERID) VALUES(" + ThreadID + "," + UserId + ")");
                }
            }
            dvMembers.Visible = false;

            if(UsersList.StartsWith(","))
                UsersList=UsersList.Substring(1);

            SendMail_Invite(ThreadID, UsersList);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "sd", "alert('Selected members invited successfully.');", true);
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "sd", "alert('Unable to Invite.');", true);
        }
    }
    protected void btnCancelInvite_OnClick(object sender, EventArgs e)
    {
        dvMembers.Visible = false;
    }
    public void SendMail_Invite(int _ThreadId,string NewUserList)
    {
         DataTable dt_Topic = Common.Execute_Procedures_Select_ByQueryCMS("select TOPIC FROM HR_ThreadMaster WHERE THREADID=" + _ThreadId);
         if (dt_Topic.Rows.Count > 0 && NewUserList.Trim()!="")
         {
             string TopicName = dt_Topic.Rows[0]["TOPIC"].ToString();
             DataTable dt_SenderEmail = Common.Execute_Procedures_Select_ByQueryCMS("select FIRSTNAME + ' ' + LASTNAME AS USERNAME,EMAIL from userlogin WHERE LOGINID=" + UserId.ToString());
             DataTable dt_RecEmails = Common.Execute_Procedures_Select_ByQueryCMS("select EMAIL from userlogin WHERE LOGINID in ("+ NewUserList + ")");
             string SenderEmail = dt_SenderEmail.Rows[0]["EMAIL"].ToString();
             string SenderName = dt_SenderEmail.Rows[0]["USERNAME"].ToString();
             string[] ToMails = GetMails(dt_RecEmails);
             string[] CCAdds = { };
             string[] BCCAdds = { };
             string[] NOAdds = { };
             ////----------------------
             String Subject = "Hello MD";
             String MailBody = "";
             MailBody = MailBody + "<br>";
             MailBody = "You are invited to join.";
             MailBody = MailBody + "<br><br>";
             MailBody = MailBody + "<br><b>Topic : </b>" + TopicName + "";
             MailBody = MailBody + "<br>";
             MailBody = MailBody + getInviteMembersList(_ThreadId);
             MailBody = MailBody + "<br>";
             MailBody = MailBody + "<br><br>Topic can be accessed from ( EMTM -- Hello MD ) module";
             MailBody = MailBody + "<br>";
             MailBody = MailBody + "<br>Thanks & Best Regards";
             MailBody = MailBody + "<br>" + SenderName;
             ////------------------
             string ErrMsg = "";
             string AttachmentFilePath = "";

             if (MailActive)
                SendEmail.SendeMailAsync(UserId, SenderEmail, SenderEmail, ToMails, CCAdds, BCCAdds, Subject, MailBody, out ErrMsg, AttachmentFilePath);
             else
                SendEmail.SendeMailAsync_TEST(UserId, SenderEmail, SenderEmail, ToMails, CCAdds, BCCAdds, Subject, MailBody, out ErrMsg, AttachmentFilePath);
         }
    }

    // Close Topic
    protected void imgbtnClosure_OnClick(object sender, EventArgs e)
    {
        int _ThreadId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable RetValue = Common.Execute_Procedures_Select_ByQueryCMS("select * from HR_ThreadMaster where ThreadId=" + _ThreadId + "");
        if (RetValue.Rows.Count > 0)
        {
            dv_Topic.InnerHtml = RetValue.Rows[0]["Topic"].ToString();
        }
        dvClosureBox.Visible = true;
        btnCloseTopic.CommandArgument = _ThreadId.ToString();
        txtClosureComments.Focus();
    }
    protected void btnCloseTopic_OnClick(object sender, EventArgs e)
    {
        try
        {
            int _ThreadId = Common.CastAsInt32(btnCloseTopic.CommandArgument);
            Common.Execute_Procedures_Select_ByQueryCMS("UPDATE HR_ThreadMaster SET CLOSURECOMMENT='" + txtClosureComments.Text.Replace("'", "`") + "',CLOSEDON=GETDATE() WHERE THREADID=" + _ThreadId);
            BindCRM();
            ShowDiscussion();
            SendMail_Closure(_ThreadId);
            dvClosureBox.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "sd", "alert('Topic closed successfully.');", true);
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "sd", "alert('Unable to close Topic.');", true);
        }
    }
    protected void btnCancelClosure_OnClick(object sender, EventArgs e)
    {
        dvClosureBox.Visible = false;
    }
    public void SendMail_Closure(int _ThreadId)
    {
        DataTable dt_Topic = Common.Execute_Procedures_Select_ByQueryCMS("select TOPIC FROM HR_ThreadMaster WHERE THREADID=" + _ThreadId);
        if (dt_Topic.Rows.Count > 0)
        {
            string TopicName = dt_Topic.Rows[0]["TOPIC"].ToString();
            DataTable dt_SenderEmail = Common.Execute_Procedures_Select_ByQueryCMS("select FIRSTNAME + ' ' + LASTNAME AS USERNAME,EMAIL from userlogin WHERE LOGINID=" + UserId.ToString());
            DataTable dt_RecEmails = Common.Execute_Procedures_Select_ByQueryCMS("select EMAIL from userlogin WHERE LOGINID in (SELECT USERID FROM HR_ThreadUsers WHERE THREADID=" + _ThreadId + ")");
            string SenderEmail = dt_SenderEmail.Rows[0]["EMAIL"].ToString();
            string SenderName = dt_SenderEmail.Rows[0]["USERNAME"].ToString();
            string[] ToMails = GetMails(dt_RecEmails);
            string[] CCAdds = { };
            string[] BCCAdds = { };
            string[] NOAdds = { };
            ////----------------------
            String Subject = "Hello MD : Topic Closed.";
            String MailBody = "";
            MailBody = MailBody + "<br>";
            MailBody = "Following Topic has been clsoed for discussion.";
            MailBody = MailBody + "<br><br>";
            MailBody = MailBody + "<br><b>Topic : </b>" + TopicName + "";
            MailBody = MailBody + "<br>";
            MailBody = MailBody + getInviteMembersList(_ThreadId);
            MailBody = MailBody + "<br>";
            MailBody = MailBody + "<br><br>Topic can be accessed from ( EMTM -- Hello MD ) module";
            MailBody = MailBody + "<br>";
            MailBody = MailBody + "<br>Thanks & Best Regards";
            MailBody = MailBody + "<br>" + SenderName;
            ////------------------
            string ErrMsg = "";
            string AttachmentFilePath = "";

            if (MailActive)
                SendEmail.SendeMailAsync(UserId, SenderEmail, SenderEmail, ToMails, CCAdds, BCCAdds, Subject, MailBody, out ErrMsg, AttachmentFilePath);
            else
                SendEmail.SendeMailAsync_TEST(UserId, SenderEmail, SenderEmail, ToMails, CCAdds, BCCAdds, Subject, MailBody, out ErrMsg, AttachmentFilePath);
        }
    }
    
    // Update Category 
    protected void btnUpdateCat_OnClick(object sender, EventArgs e)
    {
        string Category = "";
        if (rad_P1.Checked)
            Category = rad_P1.Text;
        if (rad_P2.Checked)
            Category = rad_P2.Text;
        if (rad_P3.Checked)
            Category = rad_P3.Text;
        try
        {
            Common.Execute_Procedures_Select_ByQueryCMS("UPDATE HR_ThreadMaster SET Category='" + Category + "' WHERE THREADID=" + Common.CastAsInt32(txtThreadId.Text));
            BindCRM();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "sd", "alert('Catergory updated successfully.');", true);
        }
        catch
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "sd", "alert('Unable to updated Catergory.');", true);
        }
    }
    

    protected void chkOffice_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindMembers();
    }
    protected void BindMembers()
    {
        string officelist = "";
        foreach (ListItem Li in chkOffice.Items)
        {
            if (Li.Selected)
                officelist += ","+Li.Value;
        }
        if (officelist.StartsWith(","))
            officelist = officelist.Substring(1);

        string sql = "SELECT D.FIRSTNAME + ' ' + D.MIDDLENAME + ' ' + D.FAMILYNAME AS USERNAME,OFFICENAME,POSITIONNAME,U.LOGINID " +
                   "FROM " +
                   "DBO.Hr_PersonalDetails D  " +
                   "INNER JOIN OFFICE O ON D.OFFICE=O.OFFICEID " +
                   "INNER JOIN POSITION P ON P.POSITIONID=D.POSITION AND D.OFFICE=O.OFFICEID " +
                   "INNER JOIN USERLOGIN U ON U.LOGINID=D.USERID " +
                   "WHERE DRC IS NULL AND U.LOGINID NOT IN  " +
                   "( " +
                   "    SELECT USERID FROM HR_ThreadUsers WHERE THREADID=" + ThreadID +
                   ") " + ((officelist!="")?" AND D.OFFICE IN (" + officelist + ")":"") +
                   "ORDER BY OFFICENAME,USERNAME";
        DataTable RetValue = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptMembers.DataSource = RetValue;
        rptMembers.DataBind();
    }
    
}
