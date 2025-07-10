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

public partial class PB_PublicationSubTab : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        int UserId = 0;
        UserId = int.Parse(Session["loginid"].ToString());

        btnPublication.CssClass = "menu_2";
        btnRegister.CssClass = "menu_2";
        btnLocation.CssClass = "menu_2";
        btnCommuniction.CssClass = "menu_2";
        
        string Main = Convert.ToString(Session["MM"]).Trim();

        if (Main == "P")
        {
            btnPublication.CssClass = "selmenu_2";
        }
        else if (Main == "R")
        {
            btnRegister.CssClass = "selmenu_2";
        }
        else if (Main == "L")
        {
            btnLocation.CssClass = "selmenu_2";
        }
        else if (Main == "C")
        {
            btnCommuniction.CssClass = "selmenu_2";
        }
        
    }
    protected void btnPublication_OnClick(object sender, EventArgs e)
    {
        Session["MM"] = "P";
        Session["MS"] = "P";
        Response.Redirect("PB_Publication.aspx");
    }
    protected void btnRegister_OnClick(object sender, EventArgs e)
    {
        Session["MM"] = "R";
        Session["MS"] = "P";
        Response.Redirect("PB_PublicationType.aspx");
    }
    protected void btnLocation_OnClick(object sender, EventArgs e)
    {
        Session["MM"] = "L";
        Session["MS"] = "P";
        Response.Redirect("PB_LocationCustody.aspx");
    }
    protected void btnCommuniction_OnClick(object sender, EventArgs e)
    {
        Session["MM"] = "C";
        Session["MS"] = "P";
        Response.Redirect("PB_PublicationCommunication.aspx");
    }
    
}
