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
using System.Net;
using System.Net.Mail;
public partial class PostComment : System.Web.UI.Page
{
    int UserId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        if (Session["UserName"] == null)
        {
            Page.RegisterStartupScript("close", "<script>alert('Sorry! your session has been expired.');window.close();</script>");
            return; 
        }
        Session["Home Page"] = "Home Page";
        UserId = int.Parse(Session["UserId"].ToString());
        //----------------
        if (!IsPostBack)
        {
            txtMailFrom.Text  = "emanager@energiossolutions.com";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("select descr ,email from usersfeedback inner join usermaster on loginid=usersfeedback.postedby where feedbackid=" + ("" + Request.QueryString["FeedId"]));
            if (dt.Rows.Count > 0)
            {
                ViewState["FeedId"] = Request.QueryString["FeedId"];
                txtSubject.Text = "Admin Feedback Reply";
                txtMailTo.Text = dt.Rows[0]["email"].ToString();
                txtReqMess.InnerHtml = dt.Rows[0]["descr"].ToString();
            }
            else
            {
                Page.RegisterStartupScript("close", "<script>alert('Action not permitted ! Closing this window...');window.close();</script>");
                return; 
            }
        }
    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        try
        {
            SendMail();
            Common.Execute_Procedures_Select_ByQuery("Update usersfeedback set approved='Y' Where feedbackid=" + ("" + Request.QueryString["FeedId"]));
            lblMsg.ForeColor = System.Drawing.Color.Green;   
            lblMsg.Text = "Mail sent successfully.";
        }
        catch
        {
            lblMsg.ForeColor = System.Drawing.Color.Red ;
            lblMsg.Text = "Unable to sent mail.";
        }
    }
    public void SendMail()
    {
        string MessageContent="";
        MailMessage message = new MailMessage();
        SmtpClient smtp = new SmtpClient();
        message.From = new MailAddress(txtMailFrom.Text); 
        message.To.Add(txtMailTo.Text);
        message.Subject = txtSubject.Text.ToString();
        MessageContent = "<div style='font-family:verdana;font-size:12px;color:black'><strong> Request Mesage :</strong> <br />" + 
                "-------------------------------------------------<br />"  + 
                txtReqMess.InnerHtml + "<br />" +
                "<br /><br />";
        MessageContent = MessageContent +
                "<strong>Admin Reply :</strong> <br />" + 
                "-------------------------------------------------<br />"  +
                txtHMsg.Text + "<br />" +
                "<br /></div>";
        message.Body = MessageContent;
        message.IsBodyHtml = true;
        message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        //smtp.Send(message);
}
}
