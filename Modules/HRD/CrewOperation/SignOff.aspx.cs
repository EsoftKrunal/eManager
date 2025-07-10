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
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;

public partial class CrewOperation_SignOff : System.Web.UI.Page
{
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Session["PageName"] = " - Sign Off"; 
        lb_msg.Text = "";
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 2);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 21);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy2.aspx");
        }
        //*******************
        if (!(IsPostBack))
        {
            bindcountrynameddl();
            ddlCountry_SelectedIndexChanged(sender,e);
            Signoff_Reason();
            Bind_PendingGrid("CrewNumber");
        }
    }

    #region NoChange
    public void Bind_PendingGrid(string SortBy)
    {
        int loginId;
        loginId = Convert.ToInt32(Session["loginid"]);
        DataTable dtPending = CrewSignOff.get_PendingCrewSignOff(loginId);
        dtPending.DefaultView.Sort = SortBy;
        GridView1.DataSource = dtPending;
        GridView1.DataBind();
        GridView1.Attributes.Add("MySort", SortBy);
    }
    private void bindcountrynameddl()
    {
        DataTable dt3 = PortPlanner.selectCountryName();
        this.ddlCountry.DataValueField = "CountryId";
        this.ddlCountry.DataTextField = "CountryName";
        this.ddlCountry.DataSource = dt3;
        this.ddlCountry.DataBind();
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Port();
    }
    private void Load_Port()
    {
        DataTable dt4 = PortPlanner.selectPortName(Convert.ToInt32(ddlCountry.SelectedValue));
        this.dp_port.DataValueField = "PortId";
        this.dp_port.DataTextField = "PortName";
        this.dp_port.DataSource = dt4;
        this.dp_port.DataBind();
    }
    private void Signoff_Reason()
    {
        DataSet ds = cls_SearchReliever.getMasterData("SignOffReason", "SignOffReasonId", "SignOffReason");
        dp_signreason.DataSource = ds.Tables[0];
        dp_signreason.DataTextField = "SignOffReason";
        dp_signreason.DataValueField = "SignOffReasonId";
        dp_signreason.DataBind();
        dp_signreason.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    private void BindData(int CrewId)
    {
        DataTable dt;
        dt = CrewSignOff.Crw_signoff(CrewId);
        lb_empno.Text = dt.Rows[0]["CrewNumber"].ToString();
        lb_name.Text = dt.Rows[0]["Name"].ToString();
        lb_Rank.Text = dt.Rows[0]["CurrentRank"].ToString();
        lb_vessel.Text = dt.Rows[0]["CurrentVessel"].ToString();
        lb_signon.Text = dt.Rows[0]["SignOnDate"].ToString();
        lb_relief.Text = dt.Rows[0]["ReliefDueDate"].ToString();
        lb_reliever.Text = dt.Rows[0]["RelieverName"].ToString();
        lb_crwid.Text = dt.Rows[0]["crewid"].ToString();
    }
    protected void txt_signoffdt_TextChanged(object sender, EventArgs e)
    {
        DateTime dt,dt1;
        try
        {
            dt = Convert.ToDateTime(txt_signoffdt.Text);
            dt = dt.AddDays(1);
            lb_lvstart.Text = dt.ToString("dd-MMM-yyyy");
            dt1 = Convert.ToDateTime(lb_lvstart.Text);
            dt1 = dt1.AddMonths(2);
            lb_lvcomplition.Text = dt1.ToString("dd-MMM-yyyy");
        }
        catch { }
    }
    #endregion
    protected void on_Sorting(object sender, GridViewSortEventArgs e)
    {
        Bind_PendingGrid(e.SortExpression);
    }
    protected void on_Sorted(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int Crewid, PortCallId;
        if (e.CommandName != "Sort")
        {
            HiddenField hfd;
            GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
            hfd = (HiddenField)row.FindControl("hfd_Crew");
            Crewid = Convert.ToInt32(hfd.Value);
            ////*********** CODE TO CHECK FOR BRANCHID ***********
            //string xpc = Alerts.Check_BranchId(Crewid);
            //if (xpc.Trim() != "")
            //{
            //    GridView1.SelectedIndex = -1;
            //    Clear_Controls();
            //    Clear_Controls2();
            //    lb_msg.Text = xpc;
            //    return;
            //}
            ////************
            hfd = (HiddenField)row.FindControl("hfd_Po");
            if (hfd.Value.ToString().Trim() == "N")
            {
                lb_msg.Text = "There is no PO Against this Port Call.";
                Clear_Controls2();
                GridView1.SelectedIndex = -1;
                lb_empno.Text = "";
                lb_name.Text = "";
                lb_Rank.Text = "";
                lb_vessel.Text = "";
                lb_signon.Text = "";
                lb_relief.Text = "";
                lb_reliever.Text = "";
                lb_crwid.Text = "";
                return;
            }
            //hfd = (HiddenField)row.FindControl("hfd_Crew");
            //Crewid = Convert.ToInt32(hfd.Value);
            hfd = (HiddenField)row.FindControl("hfd_PortCallId");
            PortCallId = Convert.ToInt32(hfd.Value);
            Show_Details(PortCallId, Crewid);

            //---------Country
            hfd = (HiddenField)row.FindControl("hfd_Country");
            ddlCountry.SelectedValue = hfd.Value.ToString().Trim();
            Load_Port();
       
            //---------Port
            hfd = (HiddenField)row.FindControl("hfd_Port");
            dp_port.SelectedValue = hfd.Value.ToString().Trim();

            GridView1.SelectedIndex = row.RowIndex;
            btnvisasave.Enabled = true && Auth.isAdd;
        }
    }

    public void Show_Details(int PortCallId, int CrewId)
    {
        String Mess = "";
        DataTable dtt;
        DataTable dt = CrewSignOff.Select_CrewMemberSignOffDetails(CrewId);
        foreach (DataRow dr in dt.Rows)
        {
            //****** To Get Country According To Port
            lb_empno.Text = dr["CrewNumber"].ToString();
            lb_name.Text = dr["Name"].ToString();
            lb_signon.Text = dr["SignOndate"].ToString();
            lb_Rank.Text = dr["RankCode"].ToString();
            lb_vessel.Text = dr["VesselCode"].ToString();
            lb_relief.Text = dr["ReliefDueDate"].ToString();
            lb_reliever.Text = dr["RelieverName"].ToString();
            Clear_Controls();
            txt_DOA.Text = dr["AvailableFrom"].ToString();
        }
    }
    protected void btnvisasave_Click(object sender, EventArgs e)
    {
        DateTime d1, d2;
        string dd_gt;
        DataTable dtdate;
        int PortCallId, CrewId;
        if (dp_port.SelectedIndex <= 0)
        {
            lb_msg.Text = "Please Select Port";
            return;
        }
        if (dp_signreason.SelectedIndex <= 0)
        {
            lb_msg.Text = "Please Select Sign off Reason.";
            return;
        }
        if (GridView1.SelectedIndex < 0)
        {
            lb_msg.Text = "Please Select a Crew Member.";
            return;
        }

        HiddenField hfd;
        hfd = (HiddenField)GridView1.Rows[GridView1.SelectedIndex].FindControl("hfd_Crew");
        CrewId = Convert.ToInt32(hfd.Value);

        hfd = (HiddenField)GridView1.Rows[GridView1.SelectedIndex].FindControl("hfd_PortCallId");
        PortCallId = Convert.ToInt32(hfd.Value);

        dtdate = CrewSignOff.Crw_signoffgetdate();
        dd_gt = dtdate.Rows[0]["gtdate"].ToString();
        d1 = Convert.ToDateTime(Convert.ToDateTime(txt_signoffdt.Text).ToShortDateString());
        d2 = Convert.ToDateTime(Convert.ToDateTime(dd_gt).ToShortDateString());
        if (DateTime.Compare(d1, d2) > 0)
        {
            lb_msg.Text = "SignOff Date Should Not greater Than Current Date";
            return;
        }
        d2 = Convert.ToDateTime(lb_signon.Text);
        if (DateTime.Compare(d2, d1) > 0)
        {
            lb_msg.Text = "SignOff Date Must be greater Than Equal to Sign On Date";
            return;
        }

        try
        {
            CrewSignOff.insertdata(CrewId, Convert.ToDateTime(txt_signoffdt.Text), Convert.ToInt32(dp_port.SelectedValue), Convert.ToInt32(dp_signreason.SelectedValue), txt_DOA.Text.Trim(), txt_remark.Text, Convert.ToInt32(Session["loginid"]), PortCallId);
            lb_msg.Text = ("Record Successfully Saved.");
            Bind_PendingGrid("CrewNumber");
            Clear_Controls2();
            btnvisasave.Enabled = false;
        }
        catch { lb_msg.Text = "Record Can't Saved."; }
    }
    public void Clear_Controls()
    {
        ddlCountry.SelectedIndex = 0;
        Load_Port();
        dp_signreason.SelectedIndex = 0;
        txt_remark.Text = "";
        txt_DOA.Text = ""; 
    }
    public void Clear_Controls2()
    {
        ddlCountry.SelectedIndex = 0;
        dp_port.SelectedIndex=0;
        dp_signreason.SelectedIndex = 0;
        txt_signoffdt.Text = "";
        lb_lvstart.Text = "";
        lb_lvcomplition.Text = "";
        txt_remark.Text = "";
        lb_reliever.Text = "";
        txt_DOA.Text = "";
        btnvisasave.Enabled = false;
    }
}
