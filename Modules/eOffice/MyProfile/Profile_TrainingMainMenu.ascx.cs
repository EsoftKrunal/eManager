using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class emtm_Profile_Emtm_Hr_TrainingMainMenu : System.Web.UI.UserControl
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
    protected void menu_Click(object sender, EventArgs e)
    {
        Session["CurrentProfileModule"] = ((Button)sender).CommandArgument;
        RefreshMenu();
        RedirectToPage();
    }
    public void RefreshMenu()
    {

        switch (Common.CastAsInt32(Session["CurrentProfileModule"]))
        {
            case 1:
                btnTraining.CssClass = "MenuSelectedTab";
                break;
            default:
                break;
        }
    }
    public void RedirectToPage()
    {
        switch (Common.CastAsInt32(Session["CurrentProfileModule"]))
        {
            case 1:
                btnTraining.CssClass = "MenuSelectedTab";
                Response.Redirect("Profile_TrainingManagement.aspx");
                break;
            default:
                break;
        }
    }
}
