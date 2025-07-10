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

public partial class Reporting_StaffNotRelievedOnTime : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 114);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");
        }
        //*******************
        if (!Page.IsPostBack)
        {
            this.txtfromdate.Text = System.DateTime.Today.Date.ToString("dd-MMM-yyyy");
            this.txttodate.Text = System.DateTime.Today.Date.ToString("dd-MMM-yyyy");
        }
    }
    protected void btn_show_Click(object sender, EventArgs e)
    {
        IFRAME1.Attributes.Add("src", "StaffnotRelievedonTime_Crystal.aspx?FromDate=" + txtfromdate.Text.Trim() + "&ToDate=" + txttodate.Text.Trim());
    }
}
