using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class emtm_Emtm_HR_Leave_Header : System.Web.UI.UserControl
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
        btnleavestatus.ImageUrl = btnleavestatus.ImageUrl.Replace("_b", "_g");
        btnleavehistory.ImageUrl = btnleavehistory.ImageUrl.Replace("_b", "_g");
     
        switch (Common.CastAsInt32(Session["CurrentModule"]))
        {
            case 1:
                btnleavestatus.ImageUrl = btnleavestatus.ImageUrl.Replace("_g", "_b");
                break;
            case 2:
                btnleavehistory.ImageUrl = btnleavehistory.ImageUrl.Replace("_g", "_b");
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
                btnleavestatus.ImageUrl = btnleavestatus.ImageUrl.Replace("_g", "_b");
                Response.Redirect("HR_Leave_Status.aspx");
                break;
            case 2:
                btnleavehistory.ImageUrl = btnleavehistory.ImageUrl.Replace("_g", "_b");
                Response.Redirect("HR_LeaveHistory.aspx");
                break;
              default:
                break;
        }
    }

}
