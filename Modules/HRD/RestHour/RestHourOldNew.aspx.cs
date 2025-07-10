using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Mail;

public partial class RestHourOldNew : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SessionManager.SessionCheck_New();        
        if (!Page.IsPostBack)
        {  

        }
    }

    protected void menu_OnClick(object sender, EventArgs e)
    {
        btnRestHourOld.CssClass = "btn1";
        btnRestHourNew.CssClass = "btn1";
        btnRestHourStatus.CssClass = "btn1";

        Button btn = (Button)sender;
        btn.CssClass = "selbtn";
        if (btn.CommandArgument == "1")
        {
            iframe.Attributes.Add("src", "CrewList_N.aspx");
        }
        if (btn.CommandArgument == "2")
        { 
            iframe.Attributes.Add("src", "CrewList.aspx");
        }
        if (btn.CommandArgument == "3")
        {
            iframe.Attributes.Add("src", "RestHourStatus.aspx");
        }
    }
    


}
