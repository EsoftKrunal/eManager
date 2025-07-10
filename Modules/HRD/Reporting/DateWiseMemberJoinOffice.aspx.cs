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

public partial class Reporting_DateWiseMemberJoinOffice : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        this.ddl_Vessel.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + Button1.ClientID + "').focus();}");
        this.txtfromdate.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + Button1.ClientID + "').focus();}");
        this.txttodate.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + Button1.ClientID + "').focus();}");
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 15);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");

        }
        //*******************
        if (!Page.IsPostBack)
        {
            ddl_Vessel.DataSource = cls_SearchReliever.getMasterData("Vessel", "VesselId", "VesselName", Convert.ToInt32(Session["loginid"].ToString()));
            ddl_Vessel.DataTextField = "VesselName";
            ddl_Vessel.DataValueField = "VesselId";
            ddl_Vessel.DataBind();
            ddl_Vessel.Items.Insert(0, new ListItem("< All >", "0"));
        }
        showreport();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        showreport();
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    private void showreport()
    {
        IFRAME1.Attributes.Add("src", "DatewisejoinofficeContainer.aspx?vessel=" + ddl_Vessel.SelectedValue + "&fdt=" + txtfromdate.Text + "&tdt=" + txttodate.Text);
    }
}
