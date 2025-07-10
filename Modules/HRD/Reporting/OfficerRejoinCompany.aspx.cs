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

public partial class Reporting_OfficerRejoinCompany : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        this.ddl_FromMonth.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_show.ClientID + "').focus();}");
        this.ddlyear.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_show.ClientID + "').focus();}");
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 106);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");

        }
        if (Page.IsPostBack == false)
        {
            int yr;
            yr = (Convert.ToInt16(System.DateTime.Today.Year));
            for (int i = yr - 1; i <= yr + 1; i++)
            {
                this.ddlyear.Items.Add(i.ToString());
                // 
            }
            ddlyear.Items[1].Selected = true;
            ddl_FromMonth.SelectedValue = Convert.ToString(DateTime.Now.Month);
            //*******************
           
        }
        //==========
        lblmessage.Text = "";
        if (Page.IsPostBack == true)
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
        IFRAME1.Attributes.Add("src", "OfficerRejoinCompanyContainer.aspx?month=" + ddl_FromMonth.SelectedValue + "&year=" + ddlyear.SelectedValue);
    }
    protected void btn_show_Click(object sender, EventArgs e)
    {

    }
}
