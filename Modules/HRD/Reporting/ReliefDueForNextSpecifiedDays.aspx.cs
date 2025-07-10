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

public partial class Reporting_ReliefDueForNextSpecifiedDays : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblmessage.Text = "";
        lblMsg.Text = "";  
        this.ddl_Vessel.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_show.ClientID + "').focus();}");
        this.txt_days.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_show.ClientID + "').focus();}");
        if (Page.IsPostBack == false)
        {
            LoadFleetDDL();
            LoadRank();
            bindddl_VesselName();
            //***********Code to check page acessing Permission
            int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 15);
            if (chpageauth <= 0)
            {
                Response.Redirect("DummyReport.aspx");

            }
            this.lblmessage.Text = "";
            DataTable dt1 = ReliefDueForNextSpecifiedDays.selectReliefDueForNextSpecifiedDaysdetails(-1, -1, DropDownList1.SelectedValue,"N");
            Session.Add("rptsource2", dt1);
        }
        try
        {
            DataTable dt = PrintCrewList.selectCompanyDetails();
            DataTable dt1 = ((DataTable)Session["rptsource2"]);
            if (dt1.Rows.Count > 0)
            {
                rpt.Load(Server.MapPath("ReliefDueForNextSpecifiedDays.rpt"));
                rpt.SetDataSource(dt1);
                
                foreach (DataRow dr in dt.Rows)
                {
                    rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
                }
                rpt.SetParameterValue("@NoOfDays", txt_days.Text);
                rpt.SetParameterValue("@Rank", "For " + DropDownList1.SelectedItem.Text + " Off Crew");
            }
            else
            {
                
            }
        }
        catch
        {

        }
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
        this.ddl_Vessel.DataSource = dt8;
        this.ddl_Vessel.DataValueField = "VesselId";
        this.ddl_Vessel.DataTextField = "VESSELNAME";
        this.ddl_Vessel.DataSource = dt8;
        this.ddl_Vessel.DataBind();
        ddl_Vessel.Items.Insert(0, new ListItem("< All >", "0"));
    }
    protected void btn_show_Click(object sender, EventArgs e)
    {
        string RankList = "";
        foreach (ListItem li in chkrank.Items)
        {
            if (li.Selected)
                RankList = RankList + "," + li.Value;
        }
        if (RankList.StartsWith(","))
            RankList = RankList.Substring(1);

        if (txt_days.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter days.";
            return;
        }
 
        lblmessage.Text = "";
        IFRAME1.Attributes.Add("src", "ReliefDueDaywiseContainer.aspx?days=" + txt_days.Text + "&vessel=" + ddl_Vessel.SelectedValue + "&typename=" + DropDownList1.SelectedItem.Text + "&Fleet=" + ddlFleet.SelectedValue + "&RankList=" + RankList + "&type=" + DropDownList1.SelectedValue + "&chk=" + ((CheckBox1.Checked) ? "Y" : "N"));
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
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
}
