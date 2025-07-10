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

public partial class Reporting_CrewOnVesselHeader : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 99);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");

        }
        //*******************

        if (!(IsPostBack))
        {
            ddl_Vessel.DataSource = cls_SearchReliever.getMasterData("Vessel", "VesselId", "VesselName", Convert.ToInt32(Session["loginid"].ToString()));
            ddl_Vessel.DataTextField = "VesselName";
            ddl_Vessel.DataValueField = "VesselId";
            ddl_Vessel.DataBind();
            ddl_Vessel.Items.Insert(0, new ListItem("< All >", "0"));
            txt_from.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            txt_to.Text = DateTime.Today.ToString("dd-MMM-yyyy");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Convert.ToDateTime(txt_from.Text) > Convert.ToDateTime(txt_to.Text))
        {
            this.lblMessage.Text = "From Date Should be less than To Date.";
            IFRAME1.Attributes.Add("src", "");
        }
        else
        {
            this.lblMessage.Text = "";
            IFRAME1.Attributes.Add("src", "CrewOnVessel.aspx?VID=" + ddl_Vessel.SelectedValue + "&FD=" + txt_from.Text + "&TD=" + txt_to.Text);
        }
    }
}
