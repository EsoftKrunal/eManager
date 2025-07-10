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

public partial class CrewOperation_CrewSignOn : System.Web.UI.Page
{
    int rank;
    int a;
    Authority Auth;
    int _VesselId=0;
    int _PortCallId=0;
    int _CrewId=0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Session["PageName"] = " - Sign On"; 
        lbl_signon_message.Text = "";
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        //***********Code to check page acessing Permission
        string Mess;
        Mess = "";
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 26);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy2.aspx");
        }
        _CrewId = Convert.ToInt32(Page.Request.QueryString["CrewId"].ToString());
        if (Page.Request.QueryString["PortCallId"] != null)
        {
            _PortCallId = Convert.ToInt32(Page.Request.QueryString["PortCallId"].ToString());
            DataTable dt = Budget.getTable("SELECT VESSELID FROM PORTCALLHEADER WHERE PORTCALLID=" + _PortCallId.ToString()).Tables[0];
            _VesselId = Convert.ToInt32(dt.Rows[0][0]);
        }
        if (Page.Request.QueryString["PromotionSignOnId"] != null)
        {
            int PromotionSignOnId = Convert.ToInt32(Page.Request.QueryString["PromotionSignOnId"].ToString());
            DataTable dt = Budget.getTable("SELECT VESSELID FROM PromotionSignOn WHERE PromotionSignOnId=" + PromotionSignOnId.ToString()).Tables[0];
            _VesselId = Convert.ToInt32(dt.Rows[0][0]);
        }

        if (Page.Request.QueryString["ContractRevisionId"] != null)
        {
            int ContractRevisionId = Convert.ToInt32(Page.Request.QueryString["ContractRevisionId"].ToString());
            DataTable dt = Budget.getTable("SELECT VESSELID FROM CrewContractRevision WHERE ContractRevisionId=" + ContractRevisionId.ToString()).Tables[0];
            _VesselId = Convert.ToInt32(dt.Rows[0][0]);
        }
        //*******************
        if (!Page.IsPostBack)
        {
            bindcountrynameddl();
            bindddlManningAgent();
            ddlCountry_SelectedIndexChanged(sender,e);

            if (Request.QueryString["Cid"] == null)
            {
                this.btn_save.Enabled = false;
            }
            else
            {
                this.btn_save.Enabled = true;
                btn_save.Text = "Extend Relief Date";
                Show_Record_For_Extension(Convert.ToInt32(Request.QueryString["Cid"]), Convert.ToInt32(Request.QueryString["Conid"]));
            }
            Show_CrewRecord();
        }
    }
    # region Page Control Loader
    public void bindddlSignOnAs_FamilyMember()
    {
        DataTable dt8 = SignOn.selectDataSignOnAsFamilyMember();
        this.ddl_SignOnas.DataValueField = "RankId";
        this.ddl_SignOnas.DataTextField = "RankName";
        this.ddl_SignOnas.DataSource = dt8;
        this.ddl_SignOnas.DataBind();
        lblSignOnAs.Text = dt8.Rows[0]["RankName"].ToString();
    }
    public void bindddlSignOnAs(int crewid)
    {
        DataTable dt2 = SignOn.selectDataSignOnAsDetails(crewid);
        this.ddl_SignOnas.DataValueField = "rankid";
        this.ddl_SignOnas.DataTextField = "rankcode";
        this.ddl_SignOnas.DataSource = dt2;
        this.ddl_SignOnas.DataBind();
        lblSignOnAs.Text = dt2.Rows[0]["rankcode"].ToString();
    }
    private void bindcountrynameddl()
    {
        DataTable dt3 = PortPlanner.selectCountryName();
        this.ddlCountry.DataValueField = "CountryId";
        this.ddlCountry.DataTextField = "CountryName";
        this.ddlCountry.DataSource = dt3;
        this.ddlCountry.DataBind();
    }
    public void bindddlPort()
    {
        DataTable dt4 = PortPlanner.selectPortName(Convert.ToInt32(ddlCountry.SelectedValue));

        this.ddl_Port.DataValueField = "PortId";
        this.ddl_Port.DataTextField = "PortName";
        this.ddl_Port.DataSource = dt4;
        this.ddl_Port.DataBind();

    }

    public void bindddlManningAgent()
    {
        DataTable dt5 = ManningAgent.GetAllManningAgent();
        this.ddl_ManningAgent.Items.Clear();
        //this.ddl_ManningAgent.Items.Add(new ListItem(" < Select > ", "0"));
        this.ddl_ManningAgent.DataValueField = "Manning_AgentId";
        this.ddl_ManningAgent.DataTextField = "Manning_AgentName";
        this.ddl_ManningAgent.DataSource = dt5;
        this.ddl_ManningAgent.DataBind();
    }
    #endregion
    private void clearData()
    {
        lbl_EmpNo.Text = "";
        lbl_Name.Text = "";
        //lbl_SignedOff.Text = "";
        //lbl_LastVessel.Text = "";
        lbl_Rank.Text = "";
        lbl_Vessel.Text = "";
        lblportcallid.Text = "";
        HiddenFamilyMember.Value = "";
        this.txt_Duration.Text = "";
        this.lblcrewid.Text = "";
        ddl_Port.SelectedIndex = 0;
        ddl_SignOnas.Items.Clear();
        ddlCountry.SelectedIndex = 0;
        btn_save.Enabled = false;
    }
    protected void Show_Record_SignOn(int CrewId)
    {
        DataTable dt1 = SignOn.selectDataSignOnDetailsById(CrewId);
        if (dt1.Rows.Count > 0)
        {
            foreach (DataRow dr in dt1.Rows)
            {
                lbl_EmpNo.Text = dr["CrewNumber"].ToString();
                lbl_Name.Text = dr["FirstName"].ToString() + " " + dr["LastName"].ToString();
                //try
                //{
                //    lbl_SignedOff.Text = Convert.ToDateTime(dr["SignOffDate"].ToString()).ToString("dd-MMM-yyyy");
                //}
                //catch { lbl_SignedOff.Text = ""; } 
                //lbl_LastVessel.Text = dr["LastVesselId"].ToString();
                lbl_Rank.Text = dr["rankname"].ToString();
                lbl_Vessel.Text = dr["CurrentVesselId"].ToString();
                //rank = Convert.ToInt32(dr["NewRankId"].ToString());
                //ddl_SignOnas.SelectedValue = dr["NewRankId"].ToString();
                HiddenFamilyMember.Value = dr["IsFamilyMember"].ToString();
                if (lbl_Rank.Text.Trim() == "SUPERNUMERARY" || lbl_Rank.Text.Trim() == "SUPERINTENDENT")
                {
                    this.txt_Duration.Text = "0";
                    this.txt_Duration.Enabled = true;
                }
                else
                {
                    this.txt_Duration.Text = dr["duration"].ToString();
                }
                //----------------------
                if (lbl_EmpNo.Text.ToUpper().StartsWith("FS") || lbl_EmpNo.Text.ToUpper().StartsWith("FY"))
                {
                    DataSet dsParentConract = Budget.getTable("SELECT DURATION FROM CREWCONTRACTHEADER WHERE CONTRACTID IN  " +
                                                            "( " +
	                                                        "    SELECT CONTRACTID FROM CREWPERSONALDETAILS WHERE CREWID IN  " +
                                                            "    (SELECT CREWID FROM CREWFAMILYDETAILS WHERE FAMILYEMPLOYEENUMBER='" + lbl_EmpNo.Text + "') " +
                                                            ")") ;
                    if(dsParentConract!=null)
                        if(dsParentConract.Tables.Count >0)
                            if(dsParentConract.Tables[0].Rows.Count >0)
                                this.txt_Duration.Text = dsParentConract.Tables[0].Rows[0][0].ToString();
                }
                //----------------------
               // lbl_RelieveesName.Text = dr["RelieveesName"].ToString();
            }
        }
    }
    protected void Show_Record_For_Extension(int CrewId,int ContractID)
    {
        DataTable dt1 = SignOn.getSignOnDetailsForExtension(CrewId);
        if (dt1.Rows.Count > 0)
        {
            foreach (DataRow dr in dt1.Rows)
            {
                lbl_EmpNo.Text = dr["CrewNumber"].ToString();
                lbl_Name.Text = dr["Fullname"].ToString();
                //lbl_SignedOff.Text = dr["SignOffDate"].ToString();
                //lbl_LastVessel.Text = dr["LastVesselName"].ToString();
                lbl_Rank.Text = dr["CurrentRankName"].ToString();
                lbl_Vessel.Text = dr["CurrentVesselName"].ToString();
                //---------Country
                ddlCountry.SelectedValue = dr["CountryId"].ToString(); ;
                bindddlPort();
                //---------Port
                ddl_Port.SelectedValue = dr["PortId"].ToString();
                //---------Rank
                bindddlSignOnAs(CrewId);
                ddl_SignOnas.SelectedValue = dr["CurrentRankId"].ToString();
                lblSignOnAs.Text = dr["CurrentRankId"].ToString();
                try
                {
                    txt_SignOnDate.Text = Convert.ToDateTime(dr["SignOnDate"].ToString()).ToString("dd-MMM-yyyy");
                }
                catch { txt_SignOnDate.Text = ""; }
                txt_Duration.Text = dr["DurationInMonths"].ToString();
                txt_ReliefDate.Text = Convert.ToDateTime(dr["ReliefDueDate"].ToString()).ToString("dd-MMM-yyyy");
                txt_Remarks.Text = dr["Remarks"].ToString();
                lbl_UpdatedBy.Text = dr["ExtUser"].ToString();
                try
                {
                    lbl_UpdatedOn.Text = Convert.ToDateTime(dr["ExtDate"].ToString()).ToString("dd-MMM-yyyy");
                }
                catch { lbl_UpdatedOn.Text = ""; }
                //---------------------------------------------------
                ddl_SignOnas.Enabled = false;
                ddlCountry .Enabled = false;
                ddl_Port.Enabled = false;
                txt_SignOnDate.Enabled = false;
                txt_Duration.Enabled = false;
                txt_ReliefDate.Enabled = true;
                txt_ReliefDate.ReadOnly = false;
                CalendarExtender1.Enabled = true;
               // imgsignonas.Visible=false ;
                imgSignOffDate.Visible = true;
                ViewState["Cid"] = CrewId;
                ViewState["Conid"] = ContractID;
            }
        }
    }
    protected void txt_Duration_TextChanged(object sender, EventArgs e)
    {
        DateTime rdate;
        try
        {
            rdate = Convert.ToDateTime(txt_SignOnDate.Text);
            rdate = rdate.AddMonths(Convert.ToInt32(txt_Duration.Text));
            txt_ReliefDate.Text = rdate.ToString("dd-MMM-yyyy");
        }
        catch { }
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindddlPort();
    }    
    protected void Show_CrewRecord()
    {
            clearData();
            lblcrewid.Text = _CrewId.ToString();
            lblportcallid.Text = _PortCallId.ToString();
            Show_Record_SignOn(_CrewId);
            //--------------------------
            DataTable dt = Budget.getTable("select (select countryid from port p where p.portid=ph.portid) as countryid,portid from portcallheader ph where portcallid=" + _PortCallId.ToString()).Tables[0];
            if (dt.Rows.Count > 0)
            {
                ddlCountry.SelectedValue = dt.Rows[0][0].ToString();
                bindddlPort();
                ddl_Port.SelectedValue = dt.Rows[0][1].ToString();
            }
            //--------------------------
            DataTable dt_Crew = Budget.getTable("select crewnumber from crewpersonaldetails where crewid=" + _CrewId.ToString()).Tables[0];
            if (dt_Crew.Rows[0][0].ToString().Trim().Substring(0, 1) == "F")
            {
                bindddlSignOnAs_FamilyMember();
            }
            else
            {
                bindddlSignOnAs(_CrewId);
            }
            this.btn_save.Enabled = true & Auth.isAdd;
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (ViewState["Cid"] != null)
        {
            //--------------------------------------CREW EXTENSION PART
            try
            {
                SignOn.UpdateSignOnDetailsForExtension(ViewState["Cid"].ToString(),ViewState["Conid"].ToString(),Session["loginid"].ToString(),  txt_ReliefDate.Text, txt_Remarks.Text.Trim());
            }
            catch { lbl_signon_message.Text = "Extension Not Completed "; }
            lbl_signon_message.Text = "Extension Completed Successfully.";
        }
        else
        {
            //--------------------------------------SIGN ON PART
            DataTable dt55 = SignOn.checkContractstartdate(Convert.ToDateTime(txt_SignOnDate.Text), Convert.ToInt32(lblcrewid.Text));
            foreach (DataRow dr in dt55.Rows)
            {
                if (Convert.ToInt32(dr[0].ToString()) > 0)
                {
                    lbl_signon_message.Text = "Start Date Can't be less than Contract Start Date.";
                    return;
                }
            }
            //
            if (this.lblcrewid.Text == "")
            {
                lbl_signon_message.Text = "Select Any Employee";
                this.btn_save.Enabled = false;
                return;
            }
            //
            if (Convert.ToInt32(lblportcallid.Text) > 0)
            {
                if (this.ddlCountry.SelectedIndex == 0)
                {
                    lbl_signon_message.Text = "Select Any Country.";
                    this.btn_save.Enabled = false;
                    return;
                }
                if (this.ddl_Port.SelectedIndex == 0)
                {
                    lbl_signon_message.Text = "Select Any Port.";
                    this.btn_save.Enabled = false;
                    return;
                }
            }
            int PortCallId;
            int crewid = Convert.ToInt32(this.lblcrewid.Text);
            int intduration = Convert.ToInt32(txt_Duration.Text);
            int ddlsignonas = Convert.ToInt32(ddl_SignOnas.SelectedValue);
            int ddlport = Convert.ToInt32(ddl_Port.SelectedValue);
            int ddlManningAgent = Convert.ToInt32(ddl_ManningAgent.SelectedValue);
            DateTime intsignondate = Convert.ToDateTime(Convert.ToDateTime(txt_SignOnDate.Text).ToShortDateString());

            if (intsignondate > DateTime.Today)
            {
                lbl_signon_message.Text = "SignOndate can not be more than current date.";
                txt_SignOnDate.Focus();
                return;
            }

            string strremarks = txt_Remarks.Text;
            DateTime rfdate = intsignondate.AddMonths(Convert.ToInt32(txt_Duration.Text));
            rfdate = Convert.ToDateTime(Convert.ToDateTime(rfdate).ToShortDateString());
            PortCallId = Convert.ToInt32(lblportcallid.Text);
            SignOn.updateSignOnDetails("UpdateSignOnDetails", crewid, ddlport, ddlsignonas, intduration, intsignondate, rfdate, strremarks, Convert.ToInt32(Session["loginid"].ToString()), PortCallId, ddlManningAgent);
            clearData();
            this.btn_save.Enabled = false;
            lbl_signon_message.Text = "Crew Member Signed-On successfully.";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "dialog", "window.opener.document.getElementById('ctl00_ContentMainMaster_btnRefresh').click();alert('Crew Member signed on successfully.');window.close();", true);
        }
    }
}