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

public partial class UserControls_ManualMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btnRegister.CssClass = "tab";
        btnManual.CssClass = "tab";
        string Main = Convert.ToString(Session["MM"]).Trim();
        if (Main == "R")
        {
            btnRegister.CssClass = "activetab";
        }
        else if (Main == "M")
        {
            btnManual.CssClass = "activetab";
        }
        else if (Main == "E")
        {
            btnReadmanual.CssClass = "activetab";
        }
        else if (Main == "A")
        {
            btnApproval.CssClass = "activetab";
        }
        else if (Main == "C")
        {
            btnCommunication.CssClass = "activetab";
        }
        else if (Main == "I")
        {
            btnInviteComments.CssClass = "activetab";
        }
        else
        {
            Session["MM"] = "E";
            btnReadmanual.CssClass = "activetab";
        }
    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        Session["MM"] = "R";
        Session["MS"] = "M";
        Response.Redirect("SMS_ManualMaster.aspx");
    }
    protected void btnRM_Click(object sender, EventArgs e)
    {
        Session["MM"] = "M";
        Session["MS"] = "M";
        Response.Redirect("ViewManualHeadings.aspx");
    }
    protected void btnRead_Click(object sender, EventArgs e)
    {
        Session["MM"] = "E";
        Session["MS"] = "M";
        Response.Redirect("ReadManuals.aspx");
    }
    protected void btnApproval_Click(object sender, EventArgs e)
    {
        Session["MM"] = "A";
        Session["MS"] = "M";
        Response.Redirect("Approval.aspx");
    }
    protected void btnCommunication_Click(object sender, EventArgs e)
    {
        Session["MM"] = "C";
        Session["MS"] = "M";
        Response.Redirect("Communication.aspx");
    }
    protected void btnInviteComments_Click(object sender, EventArgs e)
    {
        Session["MM"] = "I";
        Session["MS"] = "M";
        Response.Redirect("InviteComments.aspx");
    }
    
}
