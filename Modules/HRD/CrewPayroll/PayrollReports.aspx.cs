using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Modules_HRD_CrewPayroll_PayrollReports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!IsPostBack)
        {
            btnSummaryReport_Click(sender, e);
        }
    }

        protected void btnSummaryReport_Click(object sender, EventArgs e)
    {
        btnSummaryReport.CssClass = "selbtn";
        btnHomeAllotment.CssClass = "btn1";

        frm.Attributes.Add("src", "SummaryReportMonthWise.aspx");
    }

    protected void btnHomeAllotment_Click(object sender, EventArgs e)
    {
        btnHomeAllotment.CssClass = "selbtn";
        btnSummaryReport.CssClass = "btn1";

        frm.Attributes.Add("src", "HomeAllotmentReport.aspx");
    }
}