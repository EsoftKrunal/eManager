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


public partial class PostComment : System.Web.UI.Page
{
    int UserId = 0;
    int FeedId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        spnBack.Visible = false; 
        pnlReply.Visible = false;
        pnlNewPost.Visible = true;
        if (Session["UserName"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        Session["Home Page"] = "Home Page";
        UserId = int.Parse(Session["UserId"].ToString());
        //----------------
        if (!IsPostBack)
        {
            ddl_Modules.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT APPLICATIONNAME,ApplicationId FROM APPLICATIONMASTER");
            ddl_Modules.DataTextField = "APPLICATIONNAME";
            ddl_Modules.DataValueField = "ApplicationId";
            ddl_Modules.DataBind();
        }
    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        if (txtMess.Text.Trim().Length > 500 || txtMess.Text.Trim().Length < 1)
        {
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = "Message length must be in between (1-500) Chars.";
            return;
        }
        try
        {
            if (FeedId > 0)
            {
                Common.Execute_Procedures_Select_ByQuery("Insert Into UsersFeedback (FeedbackId,FType,CType,ModuleId,RefId,Descr,PostedBy,PostedOn,Approved) values(dbo.getMaxFeedId(),'A','O',0," + FeedId.ToString() + ",'" + txtMess.Text.Trim() + "'," + UserId.ToString() + ",getdate(),'N')");
            }
            else
            {
                Common.Execute_Procedures_Select_ByQuery("Insert Into UsersFeedback (FeedBackId,FType,CType,ModuleId,RefId,Descr,PostedBy,PostedOn,Approved) values(dbo.getMaxFeedId(),'Q','" + rad_FType.SelectedValue + "'," + ddl_Modules.SelectedValue + ",dbo.getMaxFeedId(),'" + txtMess.Text.Trim() + "'," + UserId.ToString() + ",getdate(),'N')");
            }
            lblMsg.ForeColor = System.Drawing.Color.Green;   
            lblMsg.Text = "Message posted successfully.";
            txtMess.Text = "";
            //spnBack.Visible = true;
        }
        catch
        {
            lblMsg.ForeColor = System.Drawing.Color.Red ;
            lblMsg.Text = "Unable to post message.";
        }
    }
}
