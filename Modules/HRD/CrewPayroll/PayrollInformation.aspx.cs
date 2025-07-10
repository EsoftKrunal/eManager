using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_HRD_CrewPayroll_PayrollDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //Alerts.SetHelp(imgHelp);
        if (!Page.IsPostBack)
        {
            Session["PayrollTab"] = "0";
        }
        btnPortageBillSummary.CssClass = "btn1";
        btnPortageBillDetails.CssClass = "btn1";
        btnHomeAllotment.CssClass = "btn1";
        btnCashAdvance.CssClass = "btn1";
        btnBondedStores.CssClass = "btn1";
        btnPortageBillReport.CssClass = "btn1";
        btnCrewNetPayable.CssClass = "btn1";
        if (Session["PayrollTab"] == null)
        {
            Session["PayrollTab"] = 0;
        }
        switch (Common.CastAsInt32(Session["PayrollTab"]))
        {
            case 0:
                btnPortageBillSummary.CssClass = "selbtn";
                
                break;
            case 1:
                btnPortageBillDetails.CssClass = "selbtn";
                break;
            case 2:
                btnHomeAllotment.CssClass = "selbtn";
                break;
            case 3:
                btnCashAdvance.CssClass = "selbtn";
                break;
            case 4:
                btnBondedStores.CssClass = "selbtn";
                break;
            case 5:
                btnPortageBillReport.CssClass = "selbtn";
                break;
            case 6:
                btnCrewNetPayable.CssClass = "selbtn";
                break;
            default:
                break;
        }
    }

    protected void Menu_Click(object sender, EventArgs e)
    {
        btnPortageBillSummary.CssClass = "btn1";
        btnPortageBillDetails.CssClass = "btn1";
        btnHomeAllotment.CssClass = "btn1";
        btnCashAdvance.CssClass = "btn1";
        btnBondedStores.CssClass = "btn1";
        btnPortageBillReport.CssClass = "btn1";
        btnCrewNetPayable.CssClass = "btn1";
        Button btn = (Button)sender;
        int i = Common.CastAsInt32(btn.CommandArgument);
        Session["PayrollTab"] = i;
       
        switch (i)
        {
            case 0:
                btnPortageBillSummary.CssClass = "selbtn";
                Session["PayrollTab"] = 0;
                frm.Attributes.Add("src", "SummaryReportMonthWise.aspx");
                break;
            case 1:
                btnPortageBillDetails.CssClass = "selbtn";
                Session["PayrollTab"] = 1;
                frm.Attributes.Add("src", "Payroll.aspx");
               
                break;
            case 2:
                btnHomeAllotment.CssClass = "selbtn";
                Session["PayrollTab"] = 2;
                frm.Attributes.Add("src", "HomeAllotmentSummary.aspx");
                
                break;
            case 3:
                btnCashAdvance.CssClass = "selbtn";
                Session["PayrollTab"] = 3;
                frm.Attributes.Add("src", "CashAdvanceSummary.aspx");
               
                break;
            case 4:
                btnBondedStores.CssClass = "selbtn";
                Session["PayrollTab"] = 4;
                frm.Attributes.Add("src", "BondedStoreSummary.aspx");
             
                break;
            case 5:
                btnPortageBillReport.CssClass = "selbtn";
                Session["PayrollTab"] = 5;
                frm.Attributes.Add("src", "PortageBillReports.aspx");

                break;
            case 6:
                btnCrewNetPayable.CssClass = "selbtn";
                Session["PayrollTab"] = 6;
                frm.Attributes.Add("src", "CrewNetPayableReport.aspx");

                break;
            default:
                break;
        }
    }
}