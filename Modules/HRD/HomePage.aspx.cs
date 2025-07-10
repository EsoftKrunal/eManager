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

public partial class HomePage : System.Web.UI.Page
{
    public int LoginId;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        LoginId = Convert.ToInt32(Session["loginid"].ToString());
        if (Alerts.isAutorized(LoginId, 1) <= 0)
        {
            //Message.Text = "You have not Authority on Crew Particular Module.";
            return;
        }
        if (Session["AP"] != null)
        {
            Session["AP"] = null;
            Response.Redirect("CrewRecord/CrewSearch.aspx?showcrewdetails=1");
        }
        else
        {
            Response.Redirect("CrewRecord/CrewSearch.aspx");
        }
    }
}
