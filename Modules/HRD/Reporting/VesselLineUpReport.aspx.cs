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

public partial class Reporting_VesselLineUpReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        this.ddl_Owner.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + Button1.ClientID + "').focus();}");
        this.ddvessel.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + Button1.ClientID + "').focus();}");
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 134);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");
        }
        //*******************

        this.lblMessage.Text = "";
        if (Page.IsPostBack == false)
        {
            this.ddl_Owner.DataTextField = "OwnerName";
            this.ddl_Owner.DataValueField = "OwnerId";
            this.ddl_Owner.DataSource = cls_SearchReliever.getMasterData("Owner", "OwnerId", "OwnerName");
            this.ddl_Owner.DataBind();
            this.ddl_Owner.Items.Insert(0, new ListItem("All", "0"));
            //bindvesselnameddl();
        }
    }
    public void bindvesselnameddl()
    {
        this.ddvessel.DataTextField = "VesselName";
        this.ddvessel.DataValueField = "VesselId";
        this.ddvessel.DataSource = cls_SearchReliever.getMasterData("Vessel", "VesselId", "VesselName", Convert.ToInt32(Session["loginid"].ToString()));
        this.ddvessel.DataBind();
        this.ddvessel.Items.Insert(0, new ListItem("All", "0"));
    }
    protected void ddl_Owner_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_Owner.SelectedIndex > 0)
        {
            DataTable dt9 = VarianceReport.getVesselAccordingtoOwner(Convert.ToInt32(ddl_Owner.SelectedValue));
            this.ddvessel.DataSource = dt9;
            this.ddvessel.DataValueField = "VesselId";
            this.ddvessel.DataTextField = "VesselName";
            this.ddvessel.DataBind();
            ddvessel.Items.Insert(0, new ListItem("All", "0"));
        }
        else
        {
            this.ddvessel.Items.Clear();
            this.ddvessel.Items.Insert(0, new ListItem("All", "0"));
            //bindvesselnameddl();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Iframe2.Attributes.Add("src", "VesselLineUpReportCrystal.aspx?VID=" + ddvessel.SelectedValue + "&OID=" + ddl_Owner.SelectedValue + "&VName=" + ddvessel.SelectedItem.Text + "&OName=" + ddl_Owner.SelectedItem.Text);
    }
}
