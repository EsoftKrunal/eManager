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

public partial class CrewOffStatus : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 110);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");

        }
        //*******************
       
        if (!(IsPostBack))
        {
            ddl_Nationality.DataSource = cls_SearchReliever.getMasterData("Country", "CountryId", "CountryName");
            ddl_Nationality.DataTextField = "CountryName";
            ddl_Nationality.DataValueField = "CountryId";
            ddl_Nationality.DataBind();
            ddl_Nationality.Items.Insert(0, new ListItem("< All >", "0"));


            ddlowner.DataSource = cls_SearchReliever.getMasterData("Owner", "ownerid", "OwnerName");
            ddlowner.DataTextField = "OwnerName";
            ddlowner.DataValueField = "OwnerId";
            ddlowner.DataBind();
            ddlowner.Items.Insert(0, new ListItem("< All >", "0"));


        }
        else
        {
            ShowData();
        }
    }
    public void ShowData()
    {
          IFRAME1.Attributes.Add("src", "CrewOffStatusContainer.aspx?nat=" + ddl_Nationality.SelectedValue + "&own=" + ddlowner.SelectedValue);  
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        ShowData();
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
