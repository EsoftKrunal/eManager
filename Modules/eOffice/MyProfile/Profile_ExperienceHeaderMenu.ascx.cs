using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Emtm_Profile_ExperienceHeaderMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginid"] == null || "" + Session["loginid"] == "")
        {
        }
        int UserId = Common.CastAsInt32("" + Session["loginid"]);
        if (!IsPostBack)
        {
            RefreshMenu();
        }
    }

    protected void Unnamed1_MenuItemClick(object sender, MenuEventArgs e)
    {
        e.Item.ImageUrl = e.Item.ImageUrl.Replace("_g", "_b");
    }

    protected void menu_Click(object sender, ImageClickEventArgs e)
    {
        Session["CurrentPage"] = ((ImageButton)sender).CommandArgument;
        RefreshMenu();
        RedirectToPage();
    }


    public void RefreshMenu()
    {
        btnshoreexp.ImageUrl = btnshoreexp.ImageUrl.Replace("_b", "_g");
        btnvesselexp.ImageUrl = btnvesselexp.ImageUrl.Replace("_b", "_g");
      
        switch (Common.CastAsInt32(Session["CurrentPage"]))
        {
            case 1:
                btnshoreexp.ImageUrl = btnshoreexp.ImageUrl.Replace("_g", "_b");
                break;
            case 2:
                btnvesselexp.ImageUrl = btnvesselexp.ImageUrl.Replace("_g", "_b");
                break;
            default:
                break;
        }
    }


    public void RedirectToPage()
    {
        switch (Common.CastAsInt32(Session["CurrentPage"]))
        {
            case 1:
                btnshoreexp.ImageUrl = btnshoreexp.ImageUrl.Replace("_g", "_b");
                Response.Redirect("Profile_Experiance.aspx");
                break;
            case 2:
                btnvesselexp.ImageUrl = btnvesselexp.ImageUrl.Replace("_g", "_b");
                Response.Redirect("Profile_ShipExperience.aspx");
                break;
            default:
                break;
        }
    }
}
