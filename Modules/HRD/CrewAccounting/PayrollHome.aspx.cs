using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;
using System.IO;
using System.Text;
using System.Xml;

public partial class Modules_HRD_CrewAccounting_PayrollHome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!IsPostBack)
        {
            btnWageScale_Click(sender, e);
        }
    }

    protected void btnWageScale_Click(object sender, EventArgs e)
    {
        btnWageScale.CssClass = "selbtn";
       // btnPayroll.CssClass = "btn1";
        btnPayrollNew.CssClass = "btn1";
        btnReports.CssClass = "btn1";
        //frm.Attributes.Add("src", "WageScaleMasterNew.aspx");

        frm.Attributes.Add("src", "~/Modules/HRD/CrewAccounting/WageScaleComponentSetting.aspx");
    }

    protected void btnPayroll_Click(object sender, EventArgs e)
    {
        btnWageScale.CssClass = "btn1";
       // btnPayroll.CssClass = "selbtn";
        btnPayrollNew.CssClass = "btn1";
        btnReports.CssClass = "btn1";
        frm.Attributes.Add("src", "~/Modules/HRD/CrewAccounting/Payroll.aspx");
    }

    protected void btnPayrollNew_Click(object sender, EventArgs e)
    {
        btnWageScale.CssClass = "btn1";
       // btnPayroll.CssClass = "btn1";
        btnPayrollNew.CssClass = "selbtn";
        btnReports.CssClass = "btn1";
        frm.Attributes.Add("src", "~/Modules/HRD/CrewPayroll/PayrollInformation.aspx");
    }

    

    protected void btnReports_Click(object sender, EventArgs e)
    {
        btnWageScale.CssClass = "btn1";
       // btnPayroll.CssClass = "btn1";
        btnPayrollNew.CssClass = "btn1";
        btnReports.CssClass = "selbtn";
        frm.Attributes.Add("src", "~/Modules/HRD/CrewPayroll/PayrollReports.aspx");
    }
}