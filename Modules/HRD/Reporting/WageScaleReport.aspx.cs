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

public partial class Reporting_WageScaleReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 130);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");

        }

        //*******************
        this.ddl_WageScale.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_Show.ClientID + "').focus();}");
        this.ddl_Nationality.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_Show.ClientID + "').focus();}");
        this.ddl_Seniority.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_Show.ClientID + "').focus();}");
        this.ddl_History.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_Show.ClientID + "').focus();}");
        if (!Page.IsPostBack)
        {
            wage_MasterName();
            bind_country();
        }
    }
    private void wage_MasterName()
    {
        DataSet ds = cls_SearchReliever.getMasterData("WageScale", "WageScaleID", "WageScaleName");
        ddl_WageScale.DataSource = ds.Tables[0];
        ddl_WageScale.DataTextField = "WageScaleName";
        ddl_WageScale.DataValueField = "WageScaleID";
        ddl_WageScale.DataBind();
        ddl_WageScale.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    private void bind_country()
    {
        DataSet ds = cls_SearchReliever.getMasterData("Country", "CountryID", "CountryName");
        ddl_Nationality.DataSource = ds.Tables[0];
        ddl_Nationality.DataTextField = "CountryName";
        ddl_Nationality.DataValueField = "CountryID";
        ddl_Nationality.DataBind();
        ddl_Nationality.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    public void LoadHistory()
    {
        DataTable dt;
        int WageScaleId, Seniority, NationalityId;
        WageScaleId = Convert.ToInt32(ddl_WageScale.SelectedValue);
        Seniority = Convert.ToInt32(ddl_Seniority.SelectedValue);
        NationalityId = Convert.ToInt32(ddl_Nationality.SelectedValue);
        dt = WagesMaster.Get_Wage_Master_History(WageScaleId, NationalityId, Seniority);
        ddl_History.DataSource = dt;
        ddl_History.DataTextField = "wefdate";
        ddl_History.DataValueField = "wagescalerankid";
        ddl_History.DataBind();
        ddl_History.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    protected void ddl_WageScale_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadHistory();
    }
    protected void ddl_Nationality_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadHistory();
    }
    protected void ddl_Seniority_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadHistory();
        DataTable dt;
        int WageScaleId, Seniority, NationalityId;
        WageScaleId = Convert.ToInt32(ddl_WageScale.SelectedValue);
        Seniority = Convert.ToInt32(ddl_Seniority.Text);
        NationalityId = Convert.ToInt32(ddl_Nationality.SelectedValue);
        dt = WagesMaster.get_HeaderDetails(WageScaleId, NationalityId, Seniority);
        if (dt.Rows.Count > 0)
        {
            if (Convert.IsDBNull(dt.Rows[0][0]))
            {
                this.HiddenEffectiveDate.Value = "";
            }
            else
            {
                this.HiddenEffectiveDate.Value = Convert.ToDateTime(dt.Rows[0][0]).ToString("dd-MMM-yyyy");
            }
        }
        else
        {
            this.HiddenEffectiveDate.Value = "";
        }
    }
    protected void btn_Show_Click(object sender, EventArgs e)
    {
        IFRAME1.Attributes.Add("src", "WageScale_Crystal.aspx?WS=" + ddl_WageScale.SelectedValue + "&Nationality=" + ddl_Nationality.SelectedValue + "&Seniority=" + ddl_Seniority.SelectedValue + "&History=" + ddl_History.SelectedValue + "&EffectiveDate=" + HiddenEffectiveDate.Value + "&WageName=" + ddl_WageScale.SelectedItem.Text + "&OR=" + ddl_OR.SelectedValue);
    }
}
