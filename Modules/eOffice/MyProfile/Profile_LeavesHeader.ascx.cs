using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class emtm_MyProfile_Emtm_Profile_LeavesHeader : System.Web.UI.UserControl
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
        btnLeaveStatus.ImageUrl = btnLeaveStatus.ImageUrl.Replace("_b", "_g");
        btnLeaveApproval.ImageUrl = btnLeaveApproval.ImageUrl.Replace("_b", "_g");
        switch (Common.CastAsInt32(Session["CurrentPage"]))
        {
            case 1:
                btnLeaveStatus.ImageUrl = btnLeaveStatus.ImageUrl.Replace("_g", "_b");
                break;
            case 2:
                btnLeaveApproval.ImageUrl = btnLeaveApproval.ImageUrl.Replace("_g", "_b");
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
                btnLeaveStatus.ImageUrl = btnLeaveStatus.ImageUrl.Replace("_g", "_b");
                Response.Redirect("Profile_LeaveStatus.aspx");
                break;
            case 2:
                btnLeaveApproval.ImageUrl = btnLeaveApproval.ImageUrl.Replace("_g", "_b");
                Response.Redirect("Profile_LeaveApproval.aspx");
                break;
            default:
                break;
        }
    }
}
