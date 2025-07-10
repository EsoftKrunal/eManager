using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class emtm_StaffAdmin_Emtm_Hr_TrainingHeaderMenu : System.Web.UI.UserControl
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
            Session["RSubCurrentModule"] = null;
            Session["CurrentModule"] = 2;
        }

    }

    protected void menu_Click(object sender, EventArgs e)
    {
        Session["RSubCurrentModule"] = ((Button)sender).CommandArgument;
        RefreshMenu();
        RedirectToPage();
    }
    public void RefreshMenu()
    {

        switch (Common.CastAsInt32(Session["RSubCurrentModule"]))
        {
            case 1:
                btnTrainingGroup.CssClass = "selbtn";
                btnTrainingMaster.CssClass = "btn1";
                btnTrainingPositionGroup.CssClass = "btn1";
                break;
            case 2:
                btnTrainingGroup.CssClass = "btn1";
                btnTrainingMaster.CssClass = "selbtn";
                btnTrainingPositionGroup.CssClass = "btn1";
                break;
            case 3:
                btnTrainingGroup.CssClass = "btn1";
                btnTrainingMaster.CssClass = "btn1";
                btnTrainingPositionGroup.CssClass = "selbtn";
                break;
            default:
                break;
        }
    }
    public void RedirectToPage()
    {
        switch (Common.CastAsInt32(Session["RSubCurrentModule"]))
        {
            case 1:
                btnTrainingGroup.CssClass = "selbtn";
                btnTrainingMaster.CssClass = "btn1";
                btnTrainingPositionGroup.CssClass = "btn1";
                Response.Redirect("Hr_TrainingRegisters.aspx");
                break;
            case 2:
                btnTrainingGroup.CssClass = "btn1";
                btnTrainingMaster.CssClass = "selbtn";
                btnTrainingPositionGroup.CssClass = "btn1";
                Response.Redirect("Hr_TrainingMaster.aspx");
                break;
            case 3:
                btnTrainingGroup.CssClass = "btn1";
                btnTrainingMaster.CssClass = "btn1";
                btnTrainingPositionGroup.CssClass = "selbtn";
                Response.Redirect("Hr_TrainingPosGroup.aspx");
                break;
            default:
                break;
        }
    }
}
