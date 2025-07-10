using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Emtm_Inv_HeaderMenu : System.Web.UI.UserControl
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
        Session["CurrentModule"] = ((ImageButton)sender).CommandArgument;
        RefreshMenu();
        RedirectToPage();
    }
    public void RefreshMenu()
    {
        btnRegister.ImageUrl = btnRegister.ImageUrl.Replace("_b", "_g");
        btncontact.ImageUrl = btncontact.ImageUrl.Replace("_b", "_g");        
        
        switch (Common.CastAsInt32(Session["CurrentModule"]))
        {
            case 1:
                btnRegister.ImageUrl = btnRegister.ImageUrl.Replace("_g", "_b");
                break;
            case 2:
                btncontact.ImageUrl = btncontact.ImageUrl.Replace("_g", "_b");
                break;
            default:
                break;
        }
    }
    public void RedirectToPage()
    {
        switch (Common.CastAsInt32(Session["CurrentModule"]))
        {
            case 1:
                btnRegister.ImageUrl = btnRegister.ImageUrl.Replace("_g", "_b");
                Response.Redirect("Emtm_Inventory.aspx");
                break;
            case 2:
                btncontact.ImageUrl = btncontact.ImageUrl.Replace("_g", "_b");
                Response.Redirect("Emtm_Inv_ItemsEntry.aspx");
                break;
            default:
                break;
        }
    }
    protected void imgbtn_Documents_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("Emtm_Profile_TravelDocs.aspx");
    }
    protected void imgbtn_Search_Click(object sender, ImageClickEventArgs e)
    {

    }
  
}

