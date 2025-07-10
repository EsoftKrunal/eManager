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

public partial class Reports_InspExpense_Report : System.Web.UI.Page
{
    int intInspId;
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        try
        {
            intInspId = int.Parse(Page.Request.QueryString["InspId"].ToString());
        }
        catch { }
        if (!Page.IsPostBack)
        {
            IFRAME1.Attributes.Add("src", "InspExpenses_Crystal.aspx?InspDueID=" + intInspId);
        }
        else
        {
            IFRAME1.Attributes.Add("src", "InspExpenses_Crystal.aspx?InspDueID=" + intInspId);
        }
    }
}
