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

public partial class Reporting_DateWisePromotedMemberList : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 109);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");

        }
        //*******************
       
        showreport();
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        showreport();
    }
    private void showreport()
    {
        if (this.txtfromdate.Text != "" && this.txttodate.Text != "")
        {
                IFRAME1.Attributes.Add("src", "DateWisePromotedContainer.aspx?fdt=" + txtfromdate.Text + "&tdt=" + txttodate.Text);  
        }
        else
        {
            IFRAME1.Attributes.Add("src", "");
        }
    }
}
