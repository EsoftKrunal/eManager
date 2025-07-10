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

public partial class CrewRecord_CrewMissingDocuments : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if(!IsPostBack)
        {
            Load();
            ShowData(sender,e);
        }
    }
    protected void Load()
    {
        //DataTable dt=Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM DBO.RANK ORDER BY RANKLEVEL");
        //ddlRank.DataSource = dt;
        //ddlRank.DataTextField = "RANKCODE";
        //ddlRank.DataValueField = "RANKCODE";
        //ddlRank.DataBind();
        //ddlRank.Items.Insert(0, new ListItem("ALL", "0"));

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM DBO.VESSEL WHERE VESSELSTATUSID=1  ORDER BY VESSELCODE ");
        ddlVessel.DataSource = dt;
        ddlVessel.DataTextField = "VESSELCODE";
        ddlVessel.DataValueField = "VESSELCODE";
        ddlVessel.DataBind();
        //ddlVessel.Items.Insert(0, new ListItem("SELECT", "0"));

        //dt = Common.Execute_Procedures_Select_ByQueryCMS("select distinct CountryId,CountryName from vessel v inner join country c on c.countryid=v.flagstateid order by CountryName");
        //ddlFS.DataSource = dt;
        //ddlFS.DataValueField = "CountryName";
        //ddlFS.DataTextField = "CountryName";
        //ddlFS.DataBind();
        //ddlFS.Items.Insert(0, new ListItem("ALL", "0"));

    }
    public void ShowData(object sender, EventArgs e)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("exec DBO.GET_PENDING_REQ_DOCS '" + ddlVessel.SelectedValue +  "'");
        string Filter = "";
        //if (ddlRank.SelectedIndex > 0)
        //{
        //    Filter += "AND RANKCODE='" + ddlRank.SelectedValue + "' ";
        //}
        //if (ddlVessel.SelectedIndex > 0)
        //{
        //    Filter += "AND VESSELCODE='" + ddlVessel.SelectedValue + "' ";
        //}
        //if (ddlFS.SelectedIndex > 0)
        //{
        //    Filter += "AND COUNTRYNAME='" + ddlFS.SelectedValue + "' ";
        //}
        if (txtCrewNo.Text.Trim()!="")
        {
            Filter += "AND CREWNUMBER='" + txtCrewNo.Text.Trim() + "' ";
        }
        if (Filter.StartsWith("AND"))
        {
            Filter = Filter.Substring(4);
        }
        DataView dv = dt.DefaultView;
        dv.RowFilter = Filter;
        DataTable dt1 = dv.ToTable();
        rpt4.DataSource = dt1;
        lblRcount4.Text = " ( " + dt1.Rows.Count.ToString() + " Records )";
        rpt4.DataBind();
    }
}
