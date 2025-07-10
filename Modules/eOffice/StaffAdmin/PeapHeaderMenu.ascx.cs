using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Emtm_PeapHeaderMenu : System.Web.UI.UserControl
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
    protected void Peap_menu_Click(object sender, EventArgs e)
    {
        Session["CurrentPage"] = ((Button)sender).CommandArgument;
        RefreshMenu();
        RedirectToPage();
    }
    public void RefreshMenu()
    {
        switch (Common.CastAsInt32(Session["CurrentPage"]))
        {
            case 1:
                btnPeap.CssClass = "selbtn";
                btnRegister.CssClass = "btn1";
                break;
            case 2:
                    btnPeap.CssClass = "btn1";
                    btnRegister.CssClass = "selbtn";
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
                Response.Redirect("HR_Peap.aspx");
                break;
            case 2:
                Response.Redirect("PeapRegister.aspx");
                break;
            default:
                break;
        }
    }
}
