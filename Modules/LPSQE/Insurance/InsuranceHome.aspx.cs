using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_LPSQE_Insurance_InsuranceHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        // SessionManager.SessionCheck_New();
        //-----------------------------
        //Alerts.SetHelp(imgHelp);
        if (!Page.IsPostBack)
        {
            Session["Insurance"] = "0";
        }
        btnPolicies.CssClass = "btn1";
        btnDashboard.CssClass = "btn1";
        btnCaseManagement.CssClass = "btn1";
        

        if (Session["Insurance"] == null)
        {
            Session["Insurance"] = 0;
        }
        switch (Common.CastAsInt32(Session["Insurance"]))
        {
            case 0:
                btnDashboard.CssClass = "selbtn";
                break;
            case 1:
                btnPolicies.CssClass = "selbtn";
                break;
            case 2:
                btnCaseManagement.CssClass = "selbtn";
                break;
           
            default:
                break;
        }
        // ((Button)this.FindControl("b" + (Common.CastAsInt32(Session["lasttab"]) + 1).ToString())).CssClass = "activetab";
    }
    protected void Menu_Click(object sender, EventArgs e)
    {
        btnDashboard.CssClass = "btn1";
        btnPolicies.CssClass = "btn1";
        btnCaseManagement.CssClass = "btn1";
     

        Button btn = (Button)sender;
        int i = Common.CastAsInt32(btn.CommandArgument);
        Session["Insurance"] = i;

        switch (i)
        {
            case 0:
                btnDashboard.CssClass = "selbtn";
                Session["Insurance"] = 0;
                frm.Attributes.Add("src", "~/Modules/LPSQE/Insurance/Dashboard.aspx");
                break;
            case 1:
                btnPolicies.CssClass = "selbtn";
                Session["Insurance"] = 1;
                frm.Attributes.Add("src", "~/Modules/LPSQE/Insurance/Policies.aspx");
                break;
            case 2:
                btnCaseManagement.CssClass = "selbtn";
                Session["Insurance"] = 2;
                frm.Attributes.Add("src", "~/Modules/LPSQE/Insurance/Case.aspx");
                break;
            
            default:
                break;
        }
    }
}