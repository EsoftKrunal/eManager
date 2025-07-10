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

public partial class ExceptionPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string s = Session["loginid"].ToString();
        }
        catch
        {
            if (Alerts.getLocation() == "S")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "abc", "window.parent.location='" + ConfigurationManager.AppSettings["VIMSLink"] + "';", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "abc", "window.parent.location='default.aspx?Exp=true';", true);
            }
        }

    }
    protected void btnHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("Homepage.aspx");
    }
}
