using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;    
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Configuration;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;  

public partial class BudgetForecastingNextYearNew : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
    }
    protected void btnUpdateBudget_Click(object sender,EventArgs e)
    {
        btnUpdateBudget.CssClass = "selbtn";
        btnReports.CssClass = "btn1";
        btnPublish.CssClass = "btn1";

        frm1.Attributes.Add("src","NextYearBudgetForecastEntry.aspx");
    }
    protected void btnReports_Click(object sender,EventArgs e)
    {
        btnUpdateBudget.CssClass = "btn1";
        btnReports.CssClass = "selbtn";
        btnPublish.CssClass = "btn1";

        frm1.Attributes.Add("src","NextYearBudgetReports.aspx");
    }
    protected void btnPublish_Click(object sender,EventArgs e)
    {
        btnUpdateBudget.CssClass = "btn1";
        btnReports.CssClass = "btn1";
        btnPublish.CssClass = "selbtn";

        frm1.Attributes.Add("src","NextYearBudgetPublish.aspx");
    }
}



