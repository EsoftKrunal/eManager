using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_OPEX_BudgetReports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //Alerts.SetHelp(imgHelp);
        if (!Page.IsPostBack)
        {
            Session["BudgetReports"] = "0";
        }
       
        btnPublishReport.CssClass = "btn1";
        btnPublished.CssClass = "btn1";
        btnFundsUpdate.CssClass = "btn1";
        if (Session["IsSuperUser"] != null && Session["IsSuperUser"].ToString() == "Y")
        {
            btnFundsUpdate.Visible = true;
        } 
        else
        {
            btnFundsUpdate.Visible = false;
        }

        if (Session["BudgetReports"] == null)
        {
            Session["BudgetReports"] = 0;
        }
        switch (Common.CastAsInt32(Session["BudgetReports"]))
        {
            case 0:
               
                btnPublishReport.CssClass = "selbtn";
                break;
            case 1:
                btnPublished.CssClass = "selbtn";
                break;
            case 2:
                btnFundsUpdate.CssClass = "selbtn";
                break;

            default:
                break;
        }
    }

    protected void Menu_Click(object sender, EventArgs e)
    {
        
        btnPublishReport.CssClass = "btn1";
        btnPublished.CssClass = "btn1";
        btnFundsUpdate.CssClass = "btn1";

        Button btn = (Button)sender;
        int i = Common.CastAsInt32(btn.CommandArgument);
        Session["BudgetReports"] = i;

        switch (i)
        {
            case 0:
               
                btnPublishReport.CssClass = "selbtn";
                Session["BudgetReports"] = 0;
                frm.Attributes.Add("src", "PublishReport.aspx");

                break;
            case 1:
                btnPublished.CssClass = "selbtn";
                Session["BudgetReports"] = 1;
                frm.Attributes.Add("src", "PublishBudget_CY.aspx");

                break;
            case 2:
                btnFundsUpdate.CssClass = "selbtn";
                Session["BudgetReports"] = 2;
                frm.Attributes.Add("src", "../OwnerAccount.aspx");

                break;
            default:
                break;
        }
    }
}