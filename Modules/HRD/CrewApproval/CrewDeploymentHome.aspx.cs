using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_HRD_CrewApproval_CrewDeploymentHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //Alerts.SetHelp(imgHelp);
        if (!Page.IsPostBack)
        {
            Session["DeploymentTab"] = "0";
        }
        btnDeploymentApproval.CssClass = "btn1";
        btnDeploymentOperation.CssClass = "btn1";
       

        if (Session["DeploymentTab"] == null)
        {
            Session["DeploymentTab"] = 0;
        }
        switch (Common.CastAsInt32(Session["DeploymentTab"]))
        {
            case 0:
                btnDeploymentApproval.CssClass = "selbtn";

                break;
            case 1:
                btnDeploymentOperation.CssClass = "selbtn";
                break;
            

            default:
                break;
        }
    }

    protected void Menu_Click(object sender, EventArgs e)
    {
        btnDeploymentApproval.CssClass = "btn1";
        btnDeploymentOperation.CssClass = "btn1";
       
        Button btn = (Button)sender;
        int i = Common.CastAsInt32(btn.CommandArgument);
        Session["DeploymentTab"] = i;

        switch (i)
        {
            case 0:
                btnDeploymentApproval.CssClass = "selbtn";
                Session["DeploymentTab"] = 0;
                frm.Attributes.Add("src", "../CrewApproval/CrewApprovalScreen.aspx");
                break;
            case 1:
                btnDeploymentOperation.CssClass = "selbtn";
                Session["DeploymentTab"] = 1;
                frm.Attributes.Add("src", "../CrewOperation/CrewChange.aspx");

                break;
            

            default:
                break;
        }
    }
}