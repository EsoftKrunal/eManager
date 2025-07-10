using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class emtm_Emtm_HR_DocsMenu : System.Web.UI.UserControl
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
        Session["Current"] = ((ImageButton)sender).CommandArgument;
        RefreshMenu();
        RedirectToPage();
    }

    public void RefreshMenu()
    {
        btntravel.ImageUrl = btntravel.ImageUrl.Replace("_b", "_g");
        btncertificates.ImageUrl = btncertificates.ImageUrl.Replace("_b", "_g");
        btnmedical.ImageUrl = btnmedical.ImageUrl.Replace("_b", "_g");
        btnpeap.ImageUrl = btnpeap.ImageUrl.Replace("_b", "_g");
        //btnvesselsetup.ImageUrl = btnvesselsetup.ImageUrl.Replace("_b", "_g");

        switch (Common.CastAsInt32(Session["Current"]))
        {
            case 1:
                btntravel.ImageUrl = btntravel.ImageUrl.Replace("_g", "_b");
                break;
            case 2:
                btncertificates.ImageUrl = btncertificates.ImageUrl.Replace("_g", "_b");
                break;
            case 3:
                btnmedical.ImageUrl = btnmedical.ImageUrl.Replace("_g", "_b");
                break;
            case 4:
                btnpeap.ImageUrl = btnpeap.ImageUrl.Replace("_g", "_b");
                break;
            default:
                break;
        }
    }

    public void RedirectToPage()
    {
        switch (Common.CastAsInt32(Session["Current"]))
        {
            case 1:
                btntravel.ImageUrl = btntravel.ImageUrl.Replace("_g", "_b");
                Response.Redirect("HR_PersonalDetail.aspx");
                break;
            case 2:
                btncertificates.ImageUrl = btncertificates.ImageUrl.Replace("_g", "_b");
                Response.Redirect("Contacts.aspx");
                break;
            case 3:
                btnmedical.ImageUrl = btnmedical.ImageUrl.Replace("_g", "_b");
                Response.Redirect("Familydetails.aspx");
                break;
            case 4:
                btnpeap.ImageUrl = btnpeap.ImageUrl.Replace("_g", "_b");
                Response.Redirect("Experiance.aspx");
                break;
            default:
                break;
        }
    }


}
