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
        btnInvoicePayment.CssClass = "btn1";
        btnPOCommitment.CssClass = "btn1";
       
        btnVarienceReport.CssClass = "btn1";

        if (Session["BudgetReports"] == null)
        {
            Session["BudgetReports"] = 0;
        }
        switch (Common.CastAsInt32(Session["BudgetReports"]))
        {
            case 0:
                btnPOCommitment.CssClass = "selbtn";

                break;
            case 1:
                btnInvoicePayment.CssClass = "selbtn";
                break;
           
            case 4:
                btnVarienceReport.CssClass = "selbtn";
                break;
            default:
                break;
        }
    }

    protected void Menu_Click(object sender, EventArgs e)
    {
        btnInvoicePayment.CssClass = "btn1";
        btnPOCommitment.CssClass = "btn1";
      
        btnVarienceReport.CssClass = "btn1";

        Button btn = (Button)sender;
        int i = Common.CastAsInt32(btn.CommandArgument);
        Session["BudgetReports"] = i;

        switch (i)
        {
            case 0:
                btnPOCommitment.CssClass = "selbtn";
                Session["BudgetReports"] = 0;
                frm.Attributes.Add("src", "PoCommitmentReport.aspx");
                break;
                
            case 1:
                btnInvoicePayment.CssClass = "selbtn";
                Session["BudgetReports"] = 1;
                frm.Attributes.Add("src", "DetailActivityReport.aspx");

                break;
           
            case 4:
                btnVarienceReport.CssClass = "selbtn";
                Session["BudgetReports"] = 4;
                frm.Attributes.Add("src", "../ReportingAndAnalysis.aspx");

                break;
            default:
                break;
        }
    }
}