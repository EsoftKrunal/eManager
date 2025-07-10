using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_OPEX_BudgetCurrentandNextYear : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //Alerts.SetHelp(imgHelp);
        if (!Page.IsPostBack)
        {
            Session["BudgetCurrentandNextYear"] = "0";
        }
        btnCurrentYear.CssClass = "btn1";
        btnNextYear.CssClass = "btn1";


        if (Session["BudgetCurrentandNextYear"] == null)
        {
            Session["BudgetCurrentandNextYear"] = 0;
        }
        switch (Common.CastAsInt32(Session["BudgetCurrentandNextYear"]))
        {
            case 0:
                btnCurrentYear.CssClass = "selbtn";

                break;
            case 1:
                btnNextYear.CssClass = "selbtn";
                break;
            
            default:
                break;
        }
    }

    protected void Menu_Click(object sender, EventArgs e)
    {
        btnCurrentYear.CssClass = "btn1";
        btnNextYear.CssClass = "btn1";
       
        Button btn = (Button)sender;
        int i = Common.CastAsInt32(btn.CommandArgument);
        Session["BudgetCurrentandNextYear"] = i;

        switch (i)
        {
            case 0:
                  btnCurrentYear.CssClass = "selbtn";
                Session["BudgetCurrentandNextYear"] = 0;
                frm.Attributes.Add("src", "UpdateBudget_CY.aspx");
                break;
            case 1:
                btnNextYear.CssClass = "selbtn";
                Session["BudgetCurrentandNextYear"] = 1;
                frm.Attributes.Add("src", "NextYearBudgetForecastEntry.aspx");

                break;
            
            default:
                break;
        }
    }
}