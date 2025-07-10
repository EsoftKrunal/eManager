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

public partial class UserControls_RegisterMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        int UserId = 0;
        UserId = int.Parse(Session["loginid"].ToString());

        btnManuals.Visible = new AuthenticationManager(1058, UserId, ObjectType.Page).IsView;
        //btnForms.Visible = new AuthenticationManager(304, UserId, ObjectType.Page).IsView;


        btnManuals.CssClass = "tab";
        btnForms.CssClass = "tab";
        btnManCat.CssClass = "tab";

        string Main = Convert.ToString(Session["MS"]).Trim();
        if (Main == "M")
        {
            btnManuals.CssClass = "activetab"; 
        }
        else if (Main == "F")
        {
            btnForms.CssClass = "activetab";
        }
        else if (Main == "A")
        {
            btnManCat.CssClass = "activetab";
        }
        else
        {
            Session["MM"] = "R";
            Session["MS"] = "M";
            btnManuals.CssClass = "activetab"; 
        }
    }

    protected void btnManuals_Click(object sender, EventArgs e)
    {
        Session["MM"] = "R";
        Session["MS"] = "M";
        Response.Redirect("SMS_ManualMaster.aspx");
    }
    protected void btnForms_Click(object sender, EventArgs e)
    {
        Session["MM"] = "R";
        Session["MS"] = "F";
        Response.Redirect("AddForms.aspx");
    }
    protected void btnManualCat_Click(object sender, EventArgs e)
    {
        Session["MM"] = "R";
        Session["MS"] = "A";
        Response.Redirect("SMS_ManualCat.aspx");
    }

    
}
