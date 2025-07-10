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

public partial class ReportsMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        Alerts.SetHelp(imgHelp);  
    }
    protected void radMenu_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        Alerts.SetHelp(imgHelp);

        if (radMenu.SelectedIndex == 0)
        {
            Response.Redirect("~/Transactions/InspectionSearch.aspx");
        }
    }
}
