using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class emtm_StaffAdmin_Emtm_Hr_TrainingMenu : System.Web.UI.UserControl
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
        Session["TSubCurrentModule"] = ((Button)sender).CommandArgument;
        RefreshMenu();
        RedirectToPage();
    }
    public void RefreshMenu()
    {

        switch (Common.CastAsInt32(Session["TSubCurrentModule"]))
        {
            case 1:
                btnAssignTraining.CssClass = "MenuSelectedTab";
                btnSchudeleTraining.CssClass = "MenuTab";
                btnExecuteTrainining.CssClass = "MenuTab";
                break;
            case 2:
                btnAssignTraining.CssClass = "MenuTab";
                btnSchudeleTraining.CssClass = "MenuSelectedTab";
                btnExecuteTrainining.CssClass = "MenuTab";
                break;
            case 3:
                btnAssignTraining.CssClass = "MenuTab";
                btnSchudeleTraining.CssClass = "MenuTab";
                btnExecuteTrainining.CssClass = "MenuSelectedTab";
                break;
            default:
                break;
        }
    }
    public void RedirectToPage()
    {
        switch (Common.CastAsInt32(Session["TSubCurrentModule"]))
        {
            case 1:
                btnAssignTraining.CssClass = "MenuSelectedTab";
                btnSchudeleTraining.CssClass = "MenuTab";
                btnExecuteTrainining.CssClass = "MenuTab";
                Response.Redirect("Hr_AssignTraining.aspx");
                break;
            case 2:
                btnAssignTraining.CssClass = "MenuTab";
                btnSchudeleTraining.CssClass = "MenuSelectedTab";
                btnExecuteTrainining.CssClass = "MenuTab";
                //Response.Redirect("Hr_TrainingMaster.aspx");
                break;
            case 3:
                btnAssignTraining.CssClass = "MenuTab";
                btnSchudeleTraining.CssClass = "MenuTab";
                btnExecuteTrainining.CssClass = "MenuSelectedTab";
                //Response.Redirect("Hr_TrainingMaster.aspx");
                break;
            default:
                break;
        }
    }
}
