using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Emtm_Inv_Category : System.Web.UI.UserControl
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
        Session["CurrentCat"] = ((ImageButton)sender).CommandArgument;
        RefreshMenu();
        RedirectToPage();
    }
    public void RefreshMenu()
    {
        btnMain.ImageUrl = btnMain.ImageUrl.Replace("_b", "_g");
        btnMid.ImageUrl = btnMid.ImageUrl.Replace("_b", "_g");
        btnMin.ImageUrl = btnMin.ImageUrl.Replace("_b", "_g");

        switch (Common.CastAsInt32(Session["CurrentCat"]))
        {
            case 1:
                btnMain.ImageUrl = btnMain.ImageUrl.Replace("_g", "_b");
                break;
            case 2:
                btnMid.ImageUrl = btnMid.ImageUrl.Replace("_g", "_b");
                break;
            case 3:
                btnMin.ImageUrl = btnMin.ImageUrl.Replace("_g", "_b");
                break;
            default:
                break;
        }
    }
    public void RedirectToPage()
    {
        switch (Common.CastAsInt32(Session["CurrentCat"]))
        {
            case 1:
                btnMain.ImageUrl = btnMain.ImageUrl.Replace("_g", "_b");
                Response.Redirect("Emtm_Inve_MainCategory.aspx");
                break;
            case 2:
                btnMid.ImageUrl = btnMid.ImageUrl.Replace("_g", "_b");
                Response.Redirect("Emtm_Inve_MidCategory.aspx");
                break;
            case 3:
                btnMin.ImageUrl = btnMin.ImageUrl.Replace("_g", "_b");
                Response.Redirect("Emtm_Inve_MinCategory.aspx");
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

