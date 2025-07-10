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

public partial class CrewOperation_PortPlanner : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Session["PageName"] = " - Port Planner"; 
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 16);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy2.aspx");

        }
        //*******************
        if (!Page.IsPostBack)
        {
            RadioButtonList1.SelectedIndex = 0;
        }
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedIndex == 0)
        {
            MultiView1.ActiveViewIndex = 0;
        }
        else if (RadioButtonList1.SelectedIndex == 1)
        {
            MultiView1.ActiveViewIndex = 1;
        }
    }
}
