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

public partial class ArearReportHeader : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()),15);
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
            ddl_Vessel.Items.Insert(0, new ListItem("< Select >", "0"));
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Session["ArrearReportheader"] = "Arear Report for Vessel : " + ddl_Vessel.SelectedItem.ToString();  
        IFRAME1.Attributes.Add("src", "ArearReport.aspx?VesselId=" + ddl_Vessel.SelectedValue);  
    }
}
