using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ReportViewer : System.Web.UI.Page
{
    AuthenticationManager Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------

        //***********Code to check page acessing Permission
        if (Session["UserType"].ToString() == "O")
        {
            Auth = new AuthenticationManager(1045, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
            if (!(Auth.IsView))
            {
                Response.Redirect("~/AuthorityError.aspx");

            }
        }
        //***********
        if (!Page.IsPostBack)
        {
            Session["CurrentModule"] = 6;

            if (Session["UserType"].ToString() == "O")
            {
                ancPMSStatus.Visible = true;
                
            }
            //else
            //{
            //    ancPMSStatus.Visible = false;
            //    tdShipMaintenanceKPI.Visible = true;
            //}
        }
    }
}
