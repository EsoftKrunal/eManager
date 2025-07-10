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

public partial class Reporting_DateWiseCrewContract : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 111);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");

        }
        //------------
        if (Page.IsPostBack == false)
        {
            this.txtfromdate.Text = System.DateTime.Today.Date.ToString("dd-MMM-yyyy");
            this.txttodate.Text = System.DateTime.Today.Date.ToString("dd-MMM-yyyy");
        }
        else
        {
            showreport();
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    private void showreport()
    {
        IFRAME1.Attributes.Add("src", "DateWiseCrewContactContainer.aspx?fdt=" + txtfromdate.Text + "&tdt=" + txttodate.Text);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        showreport();
    }
}
