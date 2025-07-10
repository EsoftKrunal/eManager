using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class emtm_StaffAdmin_Emtm_Hr_TrainingMainMenu : System.Web.UI.UserControl
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
            Session["CurrentModule"] = null;
        }

    }
    protected void menu_Click(object sender, EventArgs e)
    {
        Session["CurrentModule"] = ((Button)sender).CommandArgument;
        RefreshMenu();
        RedirectToPage();
    }
    public void RefreshMenu()
    {
     
        switch (Common.CastAsInt32(Session["CurrentModule"]))
        {
            case 1:
                btnTraining.CssClass = "selbtn";
                btnRegisters.CssClass = "btn1";
                btnReports.CssClass = "btn1";
                btnTrainingMatrix.CssClass = "btn1";
                break;
            case 2:
                btnRegisters.CssClass = "selbtn";
                btnTraining.CssClass = "btn1";
                btnReports.CssClass = "btn1";
                btnTrainingMatrix.CssClass = "btn1";
                break;
            case 3:
                btnRegisters.CssClass = "btn1";
                btnTraining.CssClass = "btn1";
                btnReports.CssClass = "selbtn";
                btnTrainingMatrix.CssClass = "btn1";
                break;            
            case 4:
                btnTrainingMatrix.CssClass = "selbtn";
                btnRegisters.CssClass = "btn1";
                btnTraining.CssClass = "btn1";
                btnReports.CssClass = "btn1";
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
                btnTraining.CssClass = "selbtn";
                btnRegisters.CssClass = "btn1";
                btnReports.CssClass = "btn1";
                btnTrainingMatrix.CssClass = "btn1";
                Response.Redirect("Hr_TrainingManagement.aspx");
                break;
            case 2:
                btnRegisters.CssClass = "selbtn";
                btnTraining.CssClass = "btn1";
                btnReports.CssClass = "btn1";
                btnTrainingMatrix.CssClass = "btn1";
                Response.Redirect("Hr_TrainingRegisters.aspx");
                break;
            case 3:
                btnRegisters.CssClass = "btn1";
                btnTraining.CssClass = "btn1";
                btnTrainingMatrix.CssClass = "btn1";
                btnReports.CssClass = "selbtn";
                Response.Redirect("HR_TrainingReports.aspx");
                break;
            case 4:
                btnRegisters.CssClass = "btn1";
                btnTraining.CssClass = "btn1";
                btnReports.CssClass = "btn1";
                btnTrainingMatrix.CssClass = "selbtn";
                Response.Redirect("TrainingMatrix.aspx");
                break;
            default:
                break;
        }
    }
}
