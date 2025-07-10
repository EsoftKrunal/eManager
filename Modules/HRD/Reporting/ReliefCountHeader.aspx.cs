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

public partial class Reporting_ReliefCountHeader : System.Web.UI.Page
{
    public void bindddl_VesselName()
    {

        DataSet dt8 = SearchSignOff.getVessel(Convert.ToInt32(Session["loginid"].ToString()));
        this.ddl_Vessel.DataSource = dt8;
        this.ddl_Vessel.DataValueField = "VesselId";
        this.ddl_Vessel.DataTextField = "Name";
        this.ddl_Vessel.DataSource = dt8;
        this.ddl_Vessel.DataBind();
        ddl_Vessel.Items.Insert(0, new ListItem("< All >", "0"));
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 15);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");

        }
        //*******************

        if (!(IsPostBack))
        {
            bindddl_VesselName();
            txt_to.Text = DateTime.Today.ToString("dd-MMM-yyyy");     
            ddRank.DataSource = cls_SearchReliever.getMasterData("Rank", "RankId", "RankName");
            ddRank.DataTextField = "RankName";
            ddRank.DataValueField = "RankId";
            ddRank.DataBind();
            ddRank.Items.Insert(0, new ListItem("< All >", "0"));
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string datestr;
        //if (txt_to.Text.Trim()  == "")
        //{
        //    datestr = "01-Jan-1900";
        //}
        //else
        //{
        //    datestr = txt_from.Text  ;
        //}
        //this.lblMessage.Text = "";
        int days = 0;
        try
        {
            days = int.Parse(txtDays.Text);
        }
        catch { }
        IFRAME1.Attributes.Add("src", "ReliefCountFooter.aspx?VID=" + ddl_Vessel.SelectedValue + "&RID=" + ddRank.SelectedValue + "&FD=&TD=" + DateTime.Today.AddDays(days).ToString("dd-MMM-yyyy"));
    }
}
