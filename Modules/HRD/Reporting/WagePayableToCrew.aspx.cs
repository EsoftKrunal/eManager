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

public partial class Reporting_WagePayableToCrew : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        this.ddl_vessel.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + Button1.ClientID + "').focus();}");
        this.ddl_Month.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + Button1.ClientID + "').focus();}");
        this.ddl_year.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + Button1.ClientID + "').focus();}");
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 102);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");

        }
        //*******************

        if (!Page.IsPostBack)
        {
            bindCrewNameDDL();
            int current_year=DateTime.Now.Year;
            ddl_year.Items.Clear();
            ddl_Month.SelectedValue = Convert.ToString(DateTime.Now.Month);
            ddl_year.Items.Add(Convert.ToString(current_year - 1));
           ddl_year.Items.Add(Convert.ToString(current_year));
            ddl_year.Items.Add(Convert.ToString(current_year + 1));
            ddl_year.Items[1].Selected = true; 
      
        if (Page.Request.QueryString["vid"]!=null)
        {
            this.ddl_vessel.SelectedValue = Page.Request.QueryString["vid"].ToString();
            this.ddl_Month.SelectedValue = Page.Request.QueryString["wagemonth"].ToString();
            if (this.ddl_year.Items.IndexOf(this.ddl_year.Items.FindByText(Page.Request.QueryString["wageyear"].ToString())) < 0)
            {
                ddl_year.Items.Add(Convert.ToString(Page.Request.QueryString["wageyear"].ToString()));
            }
            this.ddl_year.SelectedValue = Page.Request.QueryString["wageyear"].ToString();
            Button1_Click(sender,e);
        }

    }
       
    }
    public void bindCrewNameDDL()
    {
        DataTable dt = WagePayableCrew.selectVesselNameDetails();
        this.ddl_vessel.DataValueField = "VesselId";
        this.ddl_vessel.DataTextField = "VesselName";
        this.ddl_vessel.DataSource = dt;
        this.ddl_vessel.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        IFRAME1.Attributes.Add("src", "WagePayableCrystal.aspx?VID=" +ddl_vessel.SelectedValue + "&Month=" + ddl_Month.SelectedValue + "&Year=" +ddl_year.SelectedValue + "&MthName=" + ddl_Month.SelectedItem.Text + "&YrValue=" + ddl_year.SelectedItem.Text );
    }
}