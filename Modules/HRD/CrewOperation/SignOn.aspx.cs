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

public partial class CrewOperation_SignOn : System.Web.UI.Page
{
    int rank;
    int a;
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Session["PageName"] = " - Sign On"; 
        lbl_signon_message.Text = "";
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 2);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        //***********Code to check page acessing Permission
        string Mess;
        Mess = "";
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 20);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy2.aspx");
        }
        //*******************
        if (!Page.IsPostBack)
        {
            bindcountrynameddl();
            ddlCountry_SelectedIndexChanged(sender,e);
            bindddlManningAgent();
            if (Request.QueryString["Cid"] == null)
            {
                bindgrid("CrewNumber");
                this.btn_save.Enabled = false;
            }
            else
            {
                this.btn_save.Enabled = true;
                btn_save.Text = "Extend Relief Date";
                Show_Record_For_Extension(Convert.ToInt32(Request.QueryString["Cid"]), Convert.ToInt32(Request.QueryString["Conid"]));
            }
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
    }
    public void bindddlSignOnAs(int crewid)
    {
        DataTable dt2 = SignOn.selectDataSignOnAsDetails(crewid);
        this.ddl_SignOnas.DataValueField = "rankid";
        this.ddl_SignOnas.DataTextField = "rankcode";
        this.ddl_SignOnas.DataSource = dt2;
        this.ddl_SignOnas.DataBind();
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

        this.ddl_ManningAgent.DataValueField = "Manning_AgentId";
        this.ddl_ManningAgent.DataTextField = "Manning_AgentName";
        this.ddl_ManningAgent.DataSource = dt5;
        this.ddl_ManningAgent.DataBind();

    }
    #endregion
    private void bindgrid(string SortBy)
    {
        DataTable dtPending = SignOn.selectpendingSignOnCrew(Convert.ToInt32(Session["loginid"].ToString()));
        dtPending.DefaultView.Sort = SortBy;
        GridView1.DataSource = dtPending;
        GridView1.DataBind();
        GridView1.Attributes.Add("MySort", SortBy);
    }
    private void clearData()
    {
        lbl_EmpNo.Text = "";
        lbl_Name.Text = "";
        lbl_SignedOff.Text = "";
        lbl_LastVessel.Text = "";
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
        ddl_ManningAgent.SelectedIndex = 0;
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
                try
                {
                    lbl_SignedOff.Text = Convert.ToDateTime(dr["SignOffDate"].ToString()).ToString("dd-MMM-yyyy");
                }
                catch { lbl_SignedOff.Text = ""; } 
                lbl_LastVessel.Text = dr["LastVesselId"].ToString();
                lbl_Rank.Text = dr["rankname"].ToString();
                lbl_Vessel.Text = dr["CurrentVesselId"].ToString();
                //rank = Convert.ToInt32(dr["NewRankId"].ToString());
                //ddl_SignOnas.SelectedValue = dr["NewRankId"].ToString();
                HiddenFamilyMember.Value = dr["IsFamilyMember"].ToString();
                this.txt_Duration.Text = dr["duration"].ToString();
                lbl_RelieveesName.Text = dr["RelieveesName"].ToString();
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
                lbl_SignedOff.Text = dr["SignOffDate"].ToString();
                lbl_LastVessel.Text = dr["LastVesselName"].ToString();
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
                imgsignonas.Visible=false ;
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

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        bindgrid(e.SortExpression);
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
            //    clearData();
            //    lbl_signon_message.Text = xpc;
            //    return;
            //}
            ////************

            hfd = (HiddenField)row.FindControl("hfd_Po");
            if (hfd.Value.ToString().Trim() == "N")
            {
                lbl_signon_message.Text = "There is no PO Against this Port Call.";
                GridView1.SelectedIndex = -1;
                clearData();
                return;
            }

            //hfd = (HiddenField)row.FindControl("hfd_Crew");
            //Crewid = Convert.ToInt32(hfd.Value);
            hfd = (HiddenField)row.FindControl("hfd_PortCallId");
            PortCallId = Convert.ToInt32(hfd.Value);

            clearData();
            lblcrewid.Text = Crewid.ToString();
            lblportcallid.Text = PortCallId.ToString();
            Show_Record_SignOn(Crewid);

            //---------Country
            hfd = (HiddenField)row.FindControl("hfd_Country");
            ddlCountry.SelectedValue = hfd.Value.ToString().Trim();
            bindddlPort();
            
            //---------Port
            hfd = (HiddenField)row.FindControl("hfd_Port");
            ddl_Port.SelectedValue = hfd.Value.ToString().Trim();

            LinkButton lkCrew = (LinkButton)row.FindControl("lnk_Crew");

            if (lkCrew.Text.Trim().Substring(0, 1) == "F")
            {
                bindddlSignOnAs_FamilyMember();
            }
            else
            {
                bindddlSignOnAs(Crewid);
            }
            //DataTable dtfamily;
            //dtfamily = SignOn.chkfamilyorcrewmember(Crewid);
            //if (Convert.ToInt32(dtfamily.Rows[0][0].ToString()) == 0)
            //{
            //    bindddlSignOnAs_FamilyMember();
            //}
            //else
            //{
            //    bindddlSignOnAs(rank);
            //}

            GridView1.SelectedIndex = row.RowIndex;
            this.btn_save.Enabled = true & Auth.isAdd;
        }
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
                    this.btn_save.Enabled = false;
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
                DataTable dt44 = SignOn.checkrfqforsignon(Convert.ToInt32(lblcrewid.Text), Convert.ToInt32(lblportcallid.Text));
                foreach (DataRow dr in dt44.Rows)
                {
                    if (Convert.ToInt32(dr[0].ToString()) > 0)
                    {
                        lbl_signon_message.Text = "Can't SignOn this Crew because RFQ is Open.";
                        this.btn_save.Enabled = false;
                        return;
                    }
                }
            }
            int PortCallId;
            int crewid = Convert.ToInt32(this.lblcrewid.Text);
            int intduration = Convert.ToInt32(txt_Duration.Text);
            int ddlsignonas = Convert.ToInt32(ddl_SignOnas.SelectedValue);
            int ddlport = Convert.ToInt32(ddl_Port.SelectedValue);
            DateTime intsignondate = Convert.ToDateTime(txt_SignOnDate.Text);
            string strremarks = txt_Remarks.Text;
            DateTime rfdate = intsignondate.AddMonths(Convert.ToInt32(txt_Duration.Text));
            PortCallId = Convert.ToInt32(lblportcallid.Text);
            int ddlManningAgent = Convert.ToInt32(ddl_ManningAgent.SelectedValue);
            SignOn.updateSignOnDetails("UpdateSignOnDetails", crewid, ddlport, ddlsignonas, intduration, intsignondate, rfdate, strremarks, Convert.ToInt32(Session["loginid"].ToString()), PortCallId, ddlManningAgent);
            lbl_signon_message.Text = "Record Successfully Saved.";
            bindgrid("Crewnumber");
            clearData();
            this.btn_save.Enabled = false;
        }
    }
}