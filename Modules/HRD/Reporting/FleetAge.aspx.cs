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

public partial class Reporting_FleetAge : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    int VesselId;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 177);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");

        }
        //*******************
        VesselId = 0;
        try
        {
            VesselId = Convert.ToInt32(Request.QueryString["VesselId"]);  
        }
        catch { }
        if (IsPostBack)
        {
           
            ShowData();
        }
        else
        {
            ddlRecOff.DataSource = RecruitingOffice.selectDataRecruitingOfficeDetails();
            ddlRecOff.DataTextField = "RecruitingOfficeName";
            ddlRecOff.DataValueField = "RecruitingOfficeId";
            ddlRecOff.DataBind();
            ddlRecOff.Items.Insert(0, new ListItem(" All ","0"));
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    public void ShowData()
    {
        IFRAME1.Attributes.Add("src", "FleetAgeProContainer.aspx?status=" + ddlCrewType.SelectedValue + "&offcrew=" + ddlRankType.SelectedValue + "&recoff=" + ddlRecOff.SelectedValue);
    }
    protected void Show_Click(object sender, EventArgs e)
    {
     
    }
}
