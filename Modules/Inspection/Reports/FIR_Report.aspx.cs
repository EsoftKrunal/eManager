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

public partial class FIR_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 163);
        if (chpageauth <= 0)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Login.aspx';", true);
        }
        lblMessage.Text = "";
        if (!Page.IsPostBack)
        {
            ddlFleet.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT FLEETID,FLEETNAME FROM DBO.FLEETMASTER");
            ddlFleet.DataTextField = "FLEETNAME";
            ddlFleet.DataValueField = "FLEETNAME";
            ddlFleet.DataBind();
            ddlFleet.Items.Insert(0, new ListItem("< All >", ""));

            btn_Show_Click(sender, e);
        }
    }
    protected void btn_Show_Click(object sender, EventArgs e)
    {
        string Filter = " where 1=1 ";
        Filter += (ddlFleet.SelectedIndex > 0) ? " And FleetNo='" + ddlFleet.SelectedValue + "'" : "";
        Filter += (txtfromdate.Text.Trim() != "") ? " And FIRDate >='" + ddlFleet.SelectedValue + "'" : "";
        Filter += (txttodate.Text.Trim() != "") ? " And FIRDate <='" + ddlFleet.SelectedValue + "'" : "";

        Grd_FIR.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT TableId,FleetNo,replace(convert(varchar,FIRDate,106),' ','-') as FIRDate,FileName FROM FIRREPORT " + Filter + " order by CONVERT(SMALLDATETIME,FIRDate) desc");
        Grd_FIR.DataBind();
    }
    protected void btn_Clear_Click(object sender, EventArgs e)
    {
        ddlFleet.SelectedIndex = 0;
        txtfromdate.Text = "";
        txttodate.Text = "";
    }
    protected void Grd_NearMiss_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grd_FIR.PageIndex = e.NewPageIndex;
        btn_Show_Click(sender, e);
    }
}
