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

public partial class UserControls_SMSSubTab : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int UserId = 0;
        UserId = int.Parse(Session["loginid"].ToString());

        btnManual.Visible = (new AuthenticationManager(6, UserId, ObjectType.Module).IsView && new AuthenticationManager(1057, UserId, ObjectType.Page).IsView);
        btnForms.Visible = (new AuthenticationManager(6, UserId, ObjectType.Module).IsView && new AuthenticationManager(1057, UserId, ObjectType.Page).IsView);

        btnManual.CssClass = "selbtn";
        btnForms.CssClass = "btn1";
        //btnSearch.CssClass = "menu_2";
        
        string Main = Convert.ToString(Session["MM"]).Trim();
        if (Main == "M")
        {
            btnReports.CssClass = "btn1";
            btnForms.CssClass = "btn1";
            btnManual.CssClass = "selbtn";
        }
        else if (Main == "S")
        {
            //btnSearch.CssClass = "selmenu_2";
        }
        else if (Main == "A")
        {
            btnReports.CssClass = "btn1";
            btnForms.CssClass = "selbtn";
            btnManual.CssClass = "btn1";
        }
        else if (Main == "R")
        {
            btnReports.CssClass = "selbtn";
            btnForms.CssClass = "btn1";
            btnManual.CssClass = "btn1";
        }
        else
        {
            //btnSearch.CssClass = "selmenu_2";
        }
    }

    //protected void btnSearch_OnClick(object sender, EventArgs e)
    //{
    //    Session["MM"] = "S";
    //    Session["MS"] = "M";
    //    Response.Redirect("SearchManuals.aspx");
    //}

    protected void btnManual_OnClick(object sender, EventArgs e)
    {
        Session["MM"] = "M";
        Session["MS"] = "M";
        Response.Redirect("ReadManuals.aspx");
    }
    
    protected void btnForms_OnClick(object sender, EventArgs e)
    {
        Session["MM"] = "A";
        Session["MS"] = "M";
        Response.Redirect("ReadForms.aspx");
    }
    protected void btnReports_OnClick(object sender, EventArgs e)
    {
        Session["MM"] = "R";
        Session["MS"] = "M";
        Response.Redirect("ManualReports.aspx");
    }

}
