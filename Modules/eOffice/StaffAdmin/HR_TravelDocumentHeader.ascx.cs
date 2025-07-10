using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HR_TravelDocumentHeader : System.Web.UI.UserControl
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
        btntraveldoc.ImageUrl = btntraveldoc.ImageUrl.Replace("_b", "_g");
        btntravelvisa.ImageUrl = btntravelvisa.ImageUrl.Replace("_b", "_g");
        btntravelseaman.ImageUrl = btntravelseaman.ImageUrl.Replace("_b", "_g");

        switch (Common.CastAsInt32(Session["CurrentPage"]))
        {
            case 1:
                btntraveldoc.ImageUrl = btntraveldoc.ImageUrl.Replace("_g", "_b");
                break;
            case 2:
                btntravelvisa.ImageUrl = btntravelvisa.ImageUrl.Replace("_g", "_b");
                break;
            case 3:
                btntravelseaman.ImageUrl = btntravelseaman.ImageUrl.Replace("_g", "_b");
                break;
            case 4:
                btnFinNric.ImageUrl = btnFinNric.ImageUrl.Replace("_g", "_b");
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
                btntraveldoc.ImageUrl = btntraveldoc.ImageUrl.Replace("_g", "_b");
                Response.Redirect("HR_TravelDocs.aspx?var=xxx");
                break;
            case 2:
                btntravelvisa.ImageUrl = btntravelvisa.ImageUrl.Replace("_g", "_b");
                Response.Redirect("HR_VisaDocs.aspx");
                break;
            case 3:
                btntravelseaman.ImageUrl = btntravelseaman.ImageUrl.Replace("_g", "_b");
                Response.Redirect("HR_SeamanBook.aspx");
                break;
            case 4:
                btnFinNric.ImageUrl = btnFinNric.ImageUrl.Replace("_g", "_b");
                Response.Redirect("HR_FinNricDetails.aspx");
                break;
            default:
                break;
        }
    }
}
