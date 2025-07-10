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

public partial class Reporting_VesselSafeManning : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        this.ddvessel.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + Button1.ClientID + "').focus();}");
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 113);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");
        }
        //*******************
        if (Page.IsPostBack == false)
        {
            DataSet dt8 = SearchSignOff.getVessel(Convert.ToInt32(Session["loginid"].ToString()));
            this.ddvessel.DataSource = dt8;
            this.ddvessel.DataValueField = "VesselId";
            this.ddvessel.DataTextField = "Name";
            this.ddvessel.DataSource = dt8;
            this.ddvessel.DataBind();
            ddvessel.Items.Insert(0, new ListItem("< All >", "0"));
        }
        else
        {
            showreport();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        showreport();
    }
    private void showreport()
    {
        IFRAME1.Attributes.Add("src", "VesselSafeMContainer.aspx?vid=" + ddvessel.SelectedValue + "&vname=" + ddvessel.SelectedItem.Text);
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
