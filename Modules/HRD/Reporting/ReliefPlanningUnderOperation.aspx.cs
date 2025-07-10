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

public partial class Reporting_ReliefPlanningUnderOperation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMsg.Text = "";
        this.ddl_vessel.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + Button1.ClientID + "').focus();}");
        this.txtdays.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + Button1.ClientID + "').focus();}");
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 15);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");

        }
        //*******************
        if (Page.IsPostBack == false)
        {
            LoadFleetDDL();
            LoadRank();
            bindddl_VesselName();
            BindRecruitingOffice();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string RankList = "";
        foreach (ListItem li in chkrank.Items)
        {
            if (li.Selected)
                RankList = RankList + "," + li.Value;
        }
        if (RankList.StartsWith(","))
            RankList = RankList.Substring(1);

        string ss = "ReliefPlanningUnderOperation_Header.aspx?VID=" + ddl_vessel.SelectedValue + "&Days=" + txtdays.Text + "&Fleet=" + ddlFleet.SelectedValue + "&RankList=" + RankList + "&RankG=" + DropDownList1.SelectedValue + "&RO_ID=" + ddlRecruitingOff.SelectedValue + "&PD=" + ((CheckBox1.Checked) ? "Y" : "N");
        IFRAME1.Attributes.Add("src", "ReliefPlanningUnderOperation_Header.aspx?VID=" + ddl_vessel.SelectedValue + "&Days=" + txtdays.Text + "&Fleet=" + ddlFleet.SelectedValue + "&RankList=" + RankList + "&RankG=" + DropDownList1.SelectedValue + "&RO_ID=" + ddlRecruitingOff.SelectedValue + "&PD=" + ((CheckBox1.Checked) ? "Y" : "N"));
    }
    public void LoadFleetDDL()
    {
        DataTable dt6 = Budget.getTable("SELECT * FROM FLEETMASTER ORDER BY FLEETNAME").Tables[0];
        this.ddlFleet.DataValueField = "FLEETID";
        this.ddlFleet.DataTextField = "FLEETNAME";
        this.ddlFleet.DataSource = dt6;
        this.ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new ListItem("All", "0"));
    }
    public void bindddl_VesselName()
    {


        DataSet dt8;
        if (ddlFleet.SelectedIndex == 0)
        {
            dt8 = Budget.getTable("SELECT VESSELID,VESSELNAME FROM VESSEL where VesselStatusid<>2 ORDER BY VESSELNAME");
        }
        else
        {
            dt8 = Budget.getTable("SELECT VESSELID,VESSELNAME FROM VESSEL where VesselStatusid<>2 and FleetId=" + ddlFleet.SelectedValue + " ORDER BY VESSELNAME");
        }
        this.ddl_vessel.DataSource = dt8;
        this.ddl_vessel.DataValueField = "VesselId";
        this.ddl_vessel.DataTextField = "VESSELNAME";
        this.ddl_vessel.DataSource = dt8;
        this.ddl_vessel.DataBind();
        ddl_vessel.Items.Insert(0, new ListItem("< All >", "0"));
    }
    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindddl_VesselName();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadRank();
    }
    public void LoadRank()
    {
        this.chkrank.DataTextField = "RankCode";
        this.chkrank.DataValueField = "RankId";
        if (DropDownList1.SelectedIndex != 0)
        {
            this.chkrank.DataSource = Budget.getTable("SELECT RANKID,RANKCODE FROM RANK WHERE OFFCREW='" + DropDownList1.SelectedValue + "' ORDER BY RANKLEVEL");
        }
        else
        {
            this.chkrank.DataSource = Budget.getTable("SELECT RANKID,RANKCODE FROM RANK ORDER BY RANKLEVEL");
        }
        this.chkrank.DataBind();
    }
    public void BindRecruitingOffice()
    {
        string sql = "select * from RecruitingOffice";
        DataSet DS = Budget.getTable(sql);
        if (DS != null)
        {
            ddlRecruitingOff.DataSource = DS;
            ddlRecruitingOff.DataTextField = "RecruitingOfficeName";
            ddlRecruitingOff.DataValueField = "RecruitingOfficeID";
            ddlRecruitingOff.DataBind();
            ddlRecruitingOff.Items.Insert(0, new ListItem("All", "0"));
        }

    }
}
