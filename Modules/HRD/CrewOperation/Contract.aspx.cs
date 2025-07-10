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
using System.Data.SqlClient;
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;

public partial class CrewAccounting_Contract : System.Web.UI.Page
{
    int temp;
    Authority Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Session["PageName"] = " - Contract"; 
        temp = 0;
        Label1.Text = "";
        this.txt_EmpNo.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_Search.ClientID + "').focus();}");
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 19);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy2.aspx");
        }
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 2);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        if (!Page.IsPostBack)
        {
            Load_Nationality();
            Load_wageScale();
            bind_Rank();
            Bind_PendingGrid("CrewNumber");
            PrintCheck.Visible = Auth.isPrint;
            btn_CheckList.Visible = Auth.isAdd;
        }
    }
    #region PageControlLoader
    private void bind_Rank()
    {
        DataSet ds = cls_SearchReliever.getMasterData("Rank", "RankId", "RankCode");
        dp_Rank.DataSource = ds.Tables[0];
        dp_Rank.DataTextField = "RankCode";
        dp_Rank.DataValueField = "RankId";
        dp_Rank.DataBind();
        dp_Rank.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    private void bind_Rank(int RankID)
    {
        //DataTable dt = CrewContract.SelectRankOfSameOffgroup(RankID);
        //dp_Rank.DataSource = dt;
        //dp_Rank.DataTextField = "RankCode";
        //dp_Rank.DataValueField = "RankId";
        //dp_Rank.DataBind();
        //dp_Rank.Items.Insert(0, new ListItem("< Select >", "0"));

        ProcessSelectRank obj = new ProcessSelectRank();
        obj.Invoke();
        dp_Rank.DataSource = obj.ResultSet.Tables[0];
        dp_Rank.DataTextField = "RankName";
        dp_Rank.DataValueField = "RankId";
        dp_Rank.DataBind();
        //dp_Rank.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    private void Show_Contract()
    {
        String s = txt_EmpNo.Text; 
        DataTable dt;
        dt = CrewContract.Select_Contract(Convert.ToInt32(Session["loginid"]),s);
        gv_Contract.DataSource = dt;
        gv_Contract.DataBind();
    }
    private void bindGd_AssignWages()
    {
        DataTable dtGrid = CrewContract.bind_AssignedWages_by_StartDate(txt_startdt.Text, Convert.ToInt32(Txt_Seniority.Text), Convert.ToInt32(dp_nationality.SelectedValue), Convert.ToInt32(dp_wagescale.SelectedValue), Convert.ToInt32(dp_Rank.SelectedValue),Convert.ToInt32((chk_SupCert.Checked)?1:0));
        Gd_AssignWages.DataSource = dtGrid;
        Gd_AssignWages.DataBind();
        if (dtGrid.Rows.Count > 0)
        {
           //btn_Save.Enabled = true;
        }
        else
        {
            btn_Save.Enabled = false;
            lb_deduction.Text = "";
            lb_Gross.Text = "";
            lb_NewEarning.Text = "";
            return;
        }
        double earn=0, dedc=0, Net_earn=0;
        foreach (DataRow dr in dtGrid.Rows)
        {
            if (dr["Componenttype"].ToString().ToUpper().Trim() == "EARNING")
            {
                earn = earn + Convert.ToDouble(dr["wagescaleComponentvalue"].ToString());
            }
            if (dr["Componenttype"].ToString().ToUpper().Trim() == "DEDUCTION")
            {
                dedc = dedc + Convert.ToDouble(dr["wagescaleComponentvalue"].ToString());
            }
        }
        lb_Gross.Text = earn.ToString();
        lb_deduction.Text = dedc.ToString();
        Net_earn = earn - dedc;
        lb_NewEarning.Text = Net_earn.ToString();
    }
    private void Load_Nationality()
    {
        DataSet ds = cls_SearchReliever.getMasterData("Country", "CountryId", "CountryName");
        dp_nationality.DataSource = ds.Tables[0];
        dp_nationality.DataTextField = "CountryName";
        dp_nationality.DataValueField = "CountryId";
        dp_nationality.DataBind();
        dp_nationality.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    private void Load_wageScale()
    {
        DataSet ds = cls_SearchReliever.getMasterData("WageScale", "WageScaleId", "WageScaleName");
        dp_wagescale.DataSource = ds.Tables[0];
        dp_wagescale.DataTextField = "WageScaleName";
        dp_wagescale.DataValueField = "WageScaleId";
        dp_wagescale.DataBind();
        dp_wagescale.Items.Insert(0, new ListItem("< Select >", "0"));
    }
    #endregion
    public void Bind_PendingGrid(string SortBy)
    {
        int loginId;
        loginId = Convert.ToInt32(Session["loginid"]);
        DataTable dtPending = CrewContract.get_PendingCrewforContract(loginId);
        dtPending.DefaultView.Sort = SortBy;
        GridView1.DataSource = dtPending;
        GridView1.DataBind();
        GridView1.Attributes.Add("MySort", SortBy);
    }
    protected void on_Sorting(object sender, GridViewSortEventArgs e)
    {
        Bind_PendingGrid(e.SortExpression);
    }
    
    public void Show_Record_Crew(int CrewId,int PortCallId)
    {
        string Mess;
        Mess = "";
        DataTable dt11 = CrewContract.getCrewdetails(CrewId, PortCallId);
        foreach (DataRow dr in dt11.Rows)
        {
            btn_cancelContract.Enabled = false;
            btn_ContLetter.Enabled = false;
            lb_name.Text = dr["Name"].ToString();
            ViewState["Curr_VesselID"] = dr["PlannedVesselID"].ToString();
            ViewState["Rank_ID"] = dr["Rank"].ToString();
            bind_Rank(Convert.ToInt32(dr["Rank"].ToString()));
            Lb_PlanRank.Text = dr["RankName"].ToString();
            lb_PlanVessel.Text = dr["VesselName"].ToString();
            Mess = Mess + Alerts.Set_DDL_Value(dp_nationality, dr["Nationalityid"].ToString(), "Nationality");
            if (Mess.Length > 0)
            {
                this.Label1.Text = "The Status Of Following Has Been Changed To In-Active <br/>" + Mess.Substring(1);
                //this.Label1.Visible = true;
            }
            //****** Code add to check uservessel relationship
            DataTable dt = (SearchSignOff.getVessel(Convert.ToInt32(Session["loginid"].ToString()))).Tables[0];
            DataRow[] drr = dt.Select("vesselid='" + dr["PlannedVesselID"].ToString() + "'");
            if (drr.Length == 0)
            {
                Response.Redirect("dummy.aspx?mess=You Are not Authorized To Make Contract");
                return;
            }
            DataTable dt_wage = CrewContract.getWages(Convert.ToInt32(dr["PlannedVesselID"].ToString()));
            dp_wagescale.DataSource = dt_wage;
            dp_wagescale.DataTextField = "WageScaleName";
            dp_wagescale.DataValueField = "WageScaleid";
            dp_wagescale.DataBind();
            break;
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int Crewid, PortCallId, vessid;
        if (e.CommandName != "Sort")
        {
            HiddenField hfd, hfdvid;
            GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
            hfd = (HiddenField)row.FindControl("hfd_Crew");
            Crewid = Convert.ToInt32(hfd.Value);
       
            hfd = (HiddenField)row.FindControl("hfd_PortCallId");
            PortCallId = Convert.ToInt32(hfd.Value);
            hfdvid = (HiddenField)row.FindControl("Hiddenvesselid");
            vessid = Convert.ToInt32(hfdvid.Value);
            ViewState["VID"] = vessid.ToString();
            clearData();
            Show_Record_Crew(Crewid, PortCallId);
            hfdvid = (HiddenField)row.FindControl("hfd_RankId");
            if (dp_Rank.Items.FindByValue(hfdvid.Value) != null)
            {
                dp_Rank.SelectedValue = hfdvid.Value;
            }
            else
            {
                dp_Rank.SelectedIndex = 0;
            }
            btn_ShowWages.Enabled = true;
            GridView1.SelectedIndex = row.RowIndex;
        }
        gv_Contract.SelectedIndex = -1;
        Activeall();
        btn_Save.Enabled = false;
    }
    protected void btn_ShowWages_Click(object sender, EventArgs e)
    {
        bindGd_AssignWages();
    }

    protected void btn_print_CheckList_Click(object sender, EventArgs e)
    {
        try
        {
            HiddenField hfd;
            int CrewId, ContractId, Vesselid;
            if (gv_Contract.SelectedIndex >= 0)
            {
                CrewId = Convert.ToInt32(ViewState["CrewId"].ToString());
                Vesselid = Convert.ToInt32(ViewState["Vsl_id"].ToString());
                ContractId = Convert.ToInt32(ViewState["Cont_id"].ToString());
                if (gv_Contract.SelectedIndex >= 0)
                {
                    DataTable dt1 = ReportCheckList.selectCrewCheckListDetails(ContractId);
                    if (dt1.Rows.Count > 0)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "abc", "javascript:window.open('../Reporting/ReportCrewCheckLists.aspx?CrewId=" + CrewId.ToString() + "&Vesselid=" + Vesselid.ToString() + " &Contractid=" + ContractId.ToString() + "',null,'title=no,toolbars=no,scrollbars=yes,width=850,height=600,left=50,top=50,addressbar=no');", true);
                    }
                    else
                    {
                        Label1.Text = "CheckList not created.";
                    }
                }
            }
            else
            {
                Label1.Text = "Select Any Crew Member";
            }
            //}
        }
        catch
        {
            Label1.Text = "Select Any Crew Member";
        }
    }
    protected void btn_cancelContract_Click(object sender, EventArgs e)
    {
        int Result;
        if (ViewState["CrewId"] == null)
        {
            Label1.Text = "Please Select Atleast One Crew Member.";
            return;
        }
        else
        {
            ////*********** CODE TO CHECK FOR BRANCHID ***********
            //string xpc = Alerts.Check_BranchId(Convert.ToInt32(ViewState["CrewId"]));
            //if (xpc.Trim() != "")
            //{
            //    Label1.Text = xpc;
            //    return;
            //}
            ////************
            Result = CrewContract.UpdateContractDetails(Convert.ToInt32(ViewState["CrewId"]), Convert.ToInt32(ViewState["Cont_id"]));
            if (Result == 0)
            {
                clearData();
                Label1.Text = "Contract has been Cancelled.";
            }
            else
            {
                Label1.Text = "Contract Can't Cancelled Because the Crew Member is Sign On.";
            }
            Show_Contract();
            btn_Save.Enabled = false;
        }
    }
    protected void btn_CheckList_Click(object sender, EventArgs e)
    {
        HiddenField hfd;
        int CrewId, ContractId, Vesselid;
        if (dp_Rank.SelectedIndex == 0)
        {
            Label1.Text = "Please Select a Rank.";
            return;
        }
        if (GridView1.SelectedIndex >= 0)
        {
            hfd = (HiddenField)GridView1.Rows[GridView1.SelectedIndex].FindControl("hfd_Crew");
            CrewId = Convert.ToInt32(hfd.Value);
            hfd = (HiddenField)GridView1.Rows[GridView1.SelectedIndex].FindControl("Hiddenvesselid");
            Vesselid = Convert.ToInt32(hfd.Value);
            ContractId = -1;
            if (GridView1.SelectedIndex >= 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "abc", "javascript:window.open('CrewJoiningCheckList.aspx?Rank=" + dp_Rank.SelectedValue + "&CrewId=" + CrewId.ToString() + "&Vesselid=" + Vesselid.ToString() + " &Contractid=" + ContractId.ToString() + "',null,'title=no,toolbars=no,scrollbars=yes,width=850,height=600,left=50,top=50,addressbar=no');", true);
            }
        }
        else
        {
            Label1.Text = "Select Any Crew Member";
        }
    }
    private void savechecklist(int contid)
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adp = new SqlDataAdapter();
        SqlCommandBuilder cb = new SqlCommandBuilder(adp);
        DataSet dsjoin = new DataSet();
        DataRow dr;
        DataTable dtcon;
        con.ConnectionString = ConfigurationManager.ConnectionStrings["eMANAGER"].ToString();
        cmd.Connection = con;
        adp.SelectCommand = cmd;
        cmd.CommandText = "select * from CrewContractcheckList where 1=2";
        adp.Fill(dsjoin, "com");
        dr = dsjoin.Tables[0].NewRow();
        DataTable dt = ((DataTable)Session["CheckList"]);
        // dr = dt.Rows[0];
        dr["Contractid"] = contid;
        for (int i = 2; i < dt.Columns.Count - 2; i++)
        {
            dr[i] = dt.Rows[0][i];
        }
        dsjoin.Tables[0].Rows.Add(dr);
        adp.Update(dsjoin.Tables[0]);
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        double OAmount, ExtraOTRate;
        OAmount = 0;
        ExtraOTRate = 0;
        try
        {
            DateTime dtstart = DateTime.Parse(txt_startdt.Text);
        }
        catch 
        {
            Label1.Text = "Please Fill up Start Date.";
            return;
        }
        DataTable dttable1 = cls_SearchReliever.Check_CrewContractDate(Convert.ToDateTime(txt_startdt.Text), Convert.ToInt32(ViewState["VID"].ToString()));
        foreach (DataRow dr in dttable1.Rows)
        {
            if (Convert.ToInt32(dr[0].ToString()) > 0)
            {
                Label1.Text = "Start Date Can't be less than last Payroll Date.";
                return;
            }
        }
        int CrewId, PortCallId;
        double Amount;
        if (dp_Rank.SelectedValue == "0")
        {
            Label1.Text = "Please Select Rank.";
            return;
        }
        if (GridView1.SelectedIndex<0)
        {
            Label1.Text = "Please Select a Crew Member.";
            return;
        }
        HiddenField hfd;
        hfd = (HiddenField)GridView1.Rows[GridView1.SelectedIndex].FindControl("hfd_Crew");
        CrewId = Convert.ToInt32(hfd.Value);
        hfd = (HiddenField)GridView1.Rows[GridView1.SelectedIndex].FindControl("hfd_PortCallId");
        PortCallId = Convert.ToInt32(hfd.Value);
        DataTable dttable= CrewContract.Chk_ContractLicense(Convert.ToInt32(Session["CrewID_Planning"]), Convert.ToInt32(ViewState["Rank_ID"]), Convert.ToInt32(dp_Rank.SelectedValue));
           foreach (DataRow dr in dttable.Rows)
           {
               if ( Convert.ToInt32(dr[0].ToString()) > 0)
               {
                Label1.Text = "Can't Make contract for this Rank because some Licence is Missing.";
                return;
               }
           }
        DateTime dt;
        int x;
        x = Convert.ToInt32(Txt_ContractPeriod.Text);
        dt = Convert.ToDateTime(txt_startdt.Text);
        dt = dt.AddMonths(x);
        //CHECK FOR SHOW WAGES MUST BE CLICKED AND A VALID AMOUNT IS AVAILABLE OF THE NEW WAGES
        //----------------------------
        try { Amount = Convert.ToDouble((lb_NewEarning.Text)); }
        catch
        {
            Label1.Text = "Please Click on Show Wages.";
            return;
        }
        //if (Amount<=0)
        //{
        //    Label1.Text = "Total Wage Amount Can't be Negative.";
        //    //return;
        //}
        //-----------------------------
        try
        {
            OAmount = Convert.ToDouble(txt_Other_Amount.Text);
            ExtraOTRate = Convert.ToDouble(txtExtraOTRate.Text);
        }
        catch { }
        try
        {
            int contid;
            contid = CrewContract.InsertContractDetails(CrewId, Convert.ToInt32(ViewState["Rank_ID"]), Convert.ToInt32(Txt_Seniority.Text), Convert.ToDateTime(txt_issuedt.Text), Convert.ToDateTime(txt_startdt.Text), Convert.ToDateTime(dt.ToString("MM/dd/yyyy")), Convert.ToInt32(Txt_ContractPeriod.Text), Convert.ToInt32(dp_wagescale.SelectedValue), Convert.ToInt32(dp_nationality.SelectedValue), Convert.ToInt32(Session["loginid"]), txt_Remark.Text.Trim(), Convert.ToInt32(dp_Rank.SelectedValue), PortCallId, OAmount, Convert.ToInt32((chk_SupCert.Checked) ? 1 : 0), ExtraOTRate, Convert.ToInt32(ddlTravelPay.SelectedValue));
            savechecklist(contid);
            Label1.Text = "Record Saved Successfully.";
            clearData();
            btn_ShowWages.Enabled = false;
            btn_Save.Enabled = false;
        }
        catch
        {
            Label1.Text = "Record Not Saved.";
        }
        Show_Contract();
        Bind_PendingGrid("CrewNumber");
    }
    protected void btn_ContLetter_Click(object sender, EventArgs e)
    {
        try
        {
            Session.Add("Cont_id", ViewState["Cont_id"].ToString());
            Session.Add("NewRankId", Convert.ToInt32(dp_Rank.SelectedValue));
            Session.Add("ContractStartDate", Convert.ToDateTime(txt_startdt.Text));
            Session.Add("ContractSeniority", Convert.ToInt32(Txt_Seniority.Text));
            Session.Add("ContractNationality", Convert.ToInt32(dp_nationality.SelectedValue));
            Session.Add("ContractWageScaleId", Convert.ToInt32(dp_wagescale.SelectedValue));
            Response.Redirect("../Reporting/PrintContract.aspx");
        }
        catch
        {
        }
    }
    
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        Show_Contract();
    }
    protected void GridView_SelectIndexChanged(object sender, EventArgs e)
    {
        HiddenField hfd;
        hfd = (HiddenField)gv_Contract.Rows[gv_Contract.SelectedIndex].FindControl("hfd_ContractId");
        Show_Contract(Convert.ToInt32(hfd.Value));
        ////////////////////
        Session["ChecklistContractId"] = hfd.Value;
        /////////////////////
        btn_ShowWages.Enabled = false;
        btn_Save.Enabled = false;
        GridView1.SelectedIndex = -1;
    }
    public void Show_Contract(int ContractId)
    {
        string Mess;
        Mess = "";
        DataTable dt;
        double z;
        dt = CrewContract.Select_Contract_Details1(ContractId);
        if (dt.Rows.Count > 0)
        {
            ViewState["Cont_id"] = ContractId.ToString();
            ViewState["CrewId"] = dt.Rows[0]["CrewId"].ToString();
            ViewState["Vsl_id"] = dt.Rows[0]["VesselId"].ToString();
            lb_name.Text = dt.Rows[0]["Name"].ToString();
            lbl_RefNo.Text = dt.Rows[0]["RefNo"].ToString();
            Lb_PlanRank.Text = dt.Rows[0]["RankCode"].ToString();
            lb_PlanVessel.Text = dt.Rows[0]["VesselName"].ToString();
            Txt_ContractPeriod.Text = dt.Rows[0]["Duration"].ToString();
            try
            {
                txt_issuedt.Text = Convert.ToDateTime(dt.Rows[0]["IssueDate"].ToString()).ToString("dd-MMM-yyyy") ;
            }
            catch { txt_issuedt.Text = ""; } 
            try
            {
                txt_startdt.Text = Convert.ToDateTime(dt.Rows[0]["StartDate"].ToString()).ToString("dd-MMM-yyyy");
            }
            catch { txt_startdt.Text = ""; }
            Txt_Seniority.Text = dt.Rows[0]["Seniority"].ToString();
            txt_Remark.Text = dt.Rows[0]["Remark"].ToString();
            txt_Other_Amount.Text = dt.Rows[0]["OtherAmount"].ToString();
            txtExtraOTRate.Text = dt.Rows[0]["ExtraOTRate"].ToString();
            chk_SupCert.Checked =(Convert.ToInt16(dt.Rows[0]["STA"].ToString())==0)?false:true;
            dp_wagescale.Items.Clear();
            dp_wagescale.Items.Add(new ListItem(dt.Rows[0]["WageScaleName"].ToString(), dt.Rows[0]["WageScaleId"].ToString()));
            Mess = Mess + Alerts.Set_DDL_Value(dp_Rank, dt.Rows[0]["NewRankID"].ToString(), "Rank");
            Mess = Mess + Alerts.Set_DDL_Value(dp_nationality, dt.Rows[0]["Nationalityid"].ToString(), "Nationality");
            DeActiveall();
            lb_Gross.Text = Convert.ToString(Math.Round(double.Parse(dt.Rows[0]["ETotal"].ToString()), 2));
            lb_deduction.Text = Convert.ToString(Math.Round(double.Parse(dt.Rows[0]["DTotal"].ToString()), 2));
            z = Math.Round(double.Parse(lb_Gross.Text) - double.Parse(lb_deduction.Text), 2);
            lb_NewEarning.Text = z.ToString();
            ddlTravelPay.SelectedValue = dt.Rows[0]["TravelPayCriteria"].ToString();
        }
        //dt = CrewContract.bind_AssignedWages_by_StartDate(txt_startdt.Text, Convert.ToInt32(Txt_Seniority.Text), Convert.ToInt32(dp_nationality.SelectedValue), Convert.ToInt32(dp_wagescale.SelectedValue), Convert.ToInt32(dp_Rank.SelectedValue));
        dt = CrewContract.bind_AssignedWages_Total(ContractId);
        lb_deduction.Text = dt.Rows[0][1].ToString(); //ded
        lb_NewEarning.Text = dt.Rows[0][0].ToString();
        z = Math.Round(double.Parse(lb_NewEarning.Text) - double.Parse(txt_Other_Amount.Text), 2);
        lb_Gross.Text = z.ToString(); //ear
        dt = CrewContract.bind_AssignedWages(ContractId);
        Gd_AssignWages.DataSource = dt;
        Gd_AssignWages.DataBind();
        if (Mess.Length > 0)
        {
            this.Label1.Text = "The Status Of Following Has Been Changed To In-Active <br/>" + Mess.Substring(1);
            //this.Label1.Visible = true;
        }
    }

    public void DeActiveall()
    {
        txt_issuedt.Enabled = false;
        imgfrom.Enabled = false;
        txt_startdt.Enabled = false;
        Txt_ContractPeriod.Enabled = false;
        img2.Enabled = false;
        Txt_Seniority.Enabled = false;
        dp_wagescale.Enabled = false;
        dp_nationality.Enabled = false;
        dp_Rank.Enabled = false;
        btn_Save.Enabled = false;
        btn_cancel.Enabled = false;
        chk_SupCert.Enabled = false;
        txt_Other_Amount.Enabled = false; 
        btn_cancelContract.Enabled = true && Auth.isDelete;
        btn_ContLetter.Enabled = true;
        btn_ShowWages.Enabled = false;
        txt_Remark.Enabled = false;
    }
    public void Activeall()
    {
        txt_issuedt.Enabled = true;
        imgfrom.Enabled = true;
        txt_startdt.Enabled = true;
        Txt_ContractPeriod.Enabled = true;
        img2.Enabled = true;
        Txt_Seniority.Enabled = true;
        dp_wagescale.Enabled = true;
        dp_nationality.Enabled = true;
        dp_Rank.Enabled = true;
        btn_Save.Enabled = true && Auth.isAdd;
        btn_cancel.Enabled = true;
        btn_cancelContract.Enabled = false;
        btn_ContLetter.Enabled = false;
        btn_ShowWages.Enabled = true;
        chk_SupCert.Enabled = true;
        txt_Other_Amount.Enabled = true;
        txt_Remark.Enabled = true;
    }
    public void clearData()
    {
        Txt_ContractPeriod.Text = "";
        txt_issuedt.Text = "";
        txt_startdt.Text = "";
        Txt_Seniority.Text = "";
        dp_wagescale.Items.Clear();
        dp_nationality.SelectedIndex = 0;
        dp_Rank.SelectedIndex = 0;
        Gd_AssignWages.DataBind();
        lb_Gross.Text = "";
        lb_deduction.Text = "";
        lb_NewEarning.Text = "";
        txt_Remark.Text = "";
        txt_Other_Amount.Text = "";
    }
    protected void GridView1_PreRender(object sender, EventArgs e)
    {
        if (temp == 1)
        {
            GridView1.SelectedIndex = -1;
        }
    }
}
