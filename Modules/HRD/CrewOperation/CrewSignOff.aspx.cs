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
using System.Windows.Forms;
using System.Globalization;

public partial class CrewOperation_CrewSignOff : System.Web.UI.Page
{
    Authority Auth;
    int _PortCallId;
    int _CrewId;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Session["PageName"] = " - Sign Off"; 
        lb_msg.Text = "";
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 26);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy2.aspx");
        }
        _CrewId = Convert.ToInt32(Page.Request.QueryString["CrewId"].ToString());
        _PortCallId = Convert.ToInt32(Page.Request.QueryString["PortCallId"].ToString());
        //*******************
        if (!(IsPostBack))
        {
            bindcountrynameddl();
            ddlCountry_SelectedIndexChanged(sender,e);
            Signoff_Reason();
            Show_CrewData();
        }
    }
    #region NoChange
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
        lb_signon.Text = Convert.ToString(dt.Rows[0]["SignOnDate"]);
        lb_relief.Text = Convert.ToString(dt.Rows[0]["ReliefDueDate"]);
        lb_reliever.Text = dt.Rows[0]["RelieverName"].ToString();
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
    protected void Show_CrewData()
    {
        int Crewid = _CrewId, PortCallId=_PortCallId;
        Show_Details(PortCallId, Crewid);
        DataTable dt = Budget.getTable("select (select countryid from port p where p.portid=ph.portid) as countryid,portid from portcallheader ph where portcallid=" + _PortCallId.ToString()).Tables[0];   
        if(dt.Rows.Count >0)
        {
            ddlCountry.SelectedValue = dt.Rows[0][0].ToString();  
            Load_Port();
            dp_port.SelectedValue = dt.Rows[0][1].ToString();
        }
        btnvisasave.Enabled = true && Auth.isAdd;
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
            DateTime dt_val, dt_val1,dt_val2;
            try
            {
                // dt_val = Convert.ToDateTime(dr["SignOndate"]);
                dt_val =  DateTime.ParseExact(dr["SignOndate"].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                lb_signon.Text = String.Format("{0:dd-MMM-yyyy}", dt_val);
            }
            catch (Exception ex)
            {
                lb_msg.Text = ex.Message.ToString();
                lb_signon.Text = "";
            }
            lb_Rank.Text = dr["RankCode"].ToString();
            lb_vessel.Text = dr["VesselCode"].ToString();
            try
            {
                //dt_val1 = Convert.ToDateTime(dr["ReliefDueDate"]);
                dt_val1 = DateTime.ParseExact(dr["ReliefDueDate"].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                lb_relief.Text = String.Format("{0:dd-MMM-yyyy}", dt_val1);
            }
            catch (Exception ex)
            {
                lb_msg.Text = ex.Message.ToString();
                lb_relief.Text = "";
            }
            lb_reliever.Text = dr["RelieverName"].ToString();
            Clear_Controls();
            try
            {
                if (! string.IsNullOrEmpty(dr["AvailableFrom"].ToString()))
                {
                    //dt_val2 = Convert.ToDateTime(dr["AvailableFrom"]);
                    dt_val2 = DateTime.ParseExact(dr["AvailableFrom"].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    txt_DOA.Text = String.Format("{0:dd-MMM-yyyy}", dt_val2);
                }
               
            }
            catch (Exception ex)
            {
                lb_msg.Text = ex.Message.ToString();
                txt_DOA.Text = "";
            }
        }
    }
    protected void btnvisasave_Click(object sender, EventArgs e)
    {
        DateTime d1, d2;
        string dd_gt;
        DataTable dtdate;
        
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

        dtdate = CrewSignOff.Crw_signoffgetdate();
        dd_gt = dtdate.Rows[0]["gtdate"].ToString();
        d1 = Convert.ToDateTime(Convert.ToDateTime(txt_signoffdt.Text).ToShortDateString());
        d2 = Convert.ToDateTime(Convert.ToDateTime(dd_gt).ToShortDateString());
        if (DateTime.Compare(d1, d2) > 0)
        {
            lb_msg.Text = "SignOff Date Should Not greater Than Current Date";
            return;
        }
        if (!string.IsNullOrWhiteSpace(lb_signon.Text))
        {
            d2 = Convert.ToDateTime(Convert.ToDateTime(lb_signon.Text).ToShortDateString());
        }
        
        if (DateTime.Compare(d2, d1) > 0)
        {
            lb_msg.Text = "SignOff Date Must be greater Than Equal to Sign On Date";
            return;
        }

        try
        {
            CrewSignOff.insertdata(_CrewId, Convert.ToDateTime(txt_signoffdt.Text), Convert.ToInt32(dp_port.SelectedValue), Convert.ToInt32(dp_signreason.SelectedValue), txt_DOA.Text.Trim(), txt_remark.Text, Convert.ToInt32(Session["loginid"]), _PortCallId);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "dialog", "window.opener.document.getElementById('ctl00_ContentMainMaster_btnRefresh').click();alert('Crew Member signed off successfully.');window.close();", true);
            btnvisasave.Enabled = false;
        }
        catch(Exception ex){ lb_msg.Text = "Record Can't Saved. Error :" + ex.Message; }
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
