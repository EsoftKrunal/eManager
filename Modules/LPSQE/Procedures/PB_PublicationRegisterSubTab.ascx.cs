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

public partial class PB_PublicationRegisterSubTab : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        int UserId = 0;
        UserId = int.Parse(Session["loginid"].ToString());

        btnPublicationType.CssClass = "menu_2";
        btnMode.CssClass = "menu_2";
        btnPublisher.CssClass = "menu_2";
        btnRequiredBy.CssClass = "menu_2";

        string Main = Convert.ToString(Session["MS"]).Trim();
        if (Main == "P")
        {
            btnPublicationType.CssClass = "selmenu_2";
        }
        else if (Main == "M")
        {
            btnMode.CssClass = "selmenu_2";
        }
        else if (Main == "H")
        {
            btnPublisher.CssClass = "selmenu_2";
        }
        else if (Main == "B")
        {
            btnRequiredBy.CssClass = "selmenu_2";
        }
        
    }

    
    protected void btnPublicationType_OnClick(object sender, EventArgs e)
    {
        Session["MM"] = "R";
        Session["MS"] = "P";
        Response.Redirect("PB_PublicationType.aspx");
    }
    protected void btnLocation_OnClick(object sender, EventArgs e)
    {
        Session["MM"] = "R";
        Session["MS"] = "L";
        Response.Redirect("PB_PublicationLocation.aspx");
    }
    protected void btnMode_OnClick(object sender, EventArgs e)
    {
        Session["MM"] = "R";
        Session["MS"] = "M";
        Response.Redirect("PB_PublicationMode.aspx");
    }
    protected void btnPublisher_OnClick(object sender, EventArgs e)
    {
        Session["MM"] = "R";
        Session["MS"] = "H";
        Response.Redirect("PB_Publisher.aspx");
    }
    protected void btnRequiredBy_OnClick(object sender, EventArgs e)
    {
        Session["MM"] = "R";
        Session["MS"] = "B";
        Response.Redirect("PB_RequiredBy.aspx");
    }
}
