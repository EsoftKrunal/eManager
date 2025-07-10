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


public partial class CrewAccounting_CrewContract : System.Web.UI.Page
{
    Authority Auth;
    int _VesselId=0;
    int _PortCallId=0;
    int _CrewId=0;
    int _IsContractRevision = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Session["PageName"] = " - Contract"; 
        Label1.Text = "";
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
            _IsContractRevision = 1;
        }


        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 15);
        if (chpageauth <= 0)
        {
            Response.Redirect("Dummy2.aspx");
        }
        ProcessCheckAuthority OBJ = new ProcessCheckAuthority(Convert.ToInt32(Session["loginid"].ToString()), 1);
        OBJ.Invoke();
        Session["Authority"] = OBJ.Authority;
        Auth = OBJ.Authority;
        if (!Page.IsPostBack)
        {
            Load_Nationality();
            Load_wageScale();
            bind_Rank();
            Show_CrewData();
            //Show_Prev_Contracts();
            if (Request.QueryString["ContractId"] != null)
            {
                Show_Contract(Common.CastAsInt32(Request.QueryString["ContractId"])); 
            }
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
    //private void Show_Prev_Contracts()
    //{
    //    String EmpNo="";
    //    DataTable dt = Budget.getTable("select crewnumber from crewpersonaldetails where crewid=" + _CrewId.ToString()).Tables[0];
    //    if (dt.Rows.Count > 0)
    //    {
    //        EmpNo = dt.Rows[0][0].ToString();
    //    }
    //    dt = CrewContract.Select_Contract(Convert.ToInt32(Session["loginid"]), EmpNo);
    //    gv_Contract.DataSource = dt;
    //    gv_Contract.DataBind();
    //}
    private void bindGd_AssignWages()
    {
        string currency = string.Empty;
        string sql = "Select Case when LTRIM(RTRIM(f.FlagStateName)) = 'INDIA' Then 'INR' ELSE 'USD'  END As Currency  from Vessel v with(nolock) Inner Join FlagState f with(nolock) on v.FlagStateId = f.FlagStateId where v.VesselId = " + _VesselId;

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dt!= null && dt.Rows.Count > 0)
        {
            currency = dt.Rows[0]["Currency"].ToString();
        }
        DataTable dtGrid = CrewContract.bind_AssignedWages_by_StartDate(txt_startdt.Text, Convert.ToInt32(Txt_Seniority.Text), 0, Convert.ToInt32(dp_wagescale.SelectedValue), Convert.ToInt32(dp_Rank.SelectedValue),0);
        
        if (dtGrid.Rows.Count > 0)
        {


            DataView dvEarnings = new DataView(dtGrid);
            DataView dvDeducts = new DataView(dtGrid);
            dvEarnings.RowFilter = " ComponentType Like '%Earning%' ";
            dvDeducts.RowFilter = " ComponentType Like '%Deduction%' ";
            if (dvEarnings != null && dvEarnings.Count > 0)
            {
                Gd_AssignWages.DataSource = dvEarnings;
                Gd_AssignWages.DataBind();
                Gd_AssignWages.HeaderRow.Cells[2].Text = "Amount (" + currency + ")";
            }
            else
            {
                Gd_AssignWages.DataSource = null;
                Gd_AssignWages.DataBind();
            }
            if (dvDeducts != null && dvDeducts.Count > 0)
            {
                Gd_AssingWagesDeduction.DataSource = dvDeducts;
                Gd_AssingWagesDeduction.DataBind();
                Gd_AssingWagesDeduction.HeaderRow.Cells[2].Text = "Amount (" + currency + ")";
            }
            else
            {
                Gd_AssingWagesDeduction.DataSource = null;
                Gd_AssingWagesDeduction.DataBind();
            }
            
            ViewState["WageScaleRankId"] = dtGrid.Rows[0]["WageScaleRankId"];  
           //btn_Save.Enabled = true;
        }
        else
        {
            btn_Save.Enabled = false;
            lb_deduction.Text = "";
            lb_Gross.Text = "";
            lb_NewEarning.Text = "";
            txtExtraOtRate.Text = "";
            ddlTravelPay.SelectedIndex = 0;
            return;
        }
        decimal earn=0, dedc=0, Net_earn=0,totalEarning=0, totalDeduction=0;
        foreach (DataRow dr in dtGrid.Rows)
        {
            //if( Common.CastAsInt32(dr["WageScaleComponentId"])!=12)
            //{
            if (Convert.ToDouble(dr["wagescaleComponentvalue"]) > 0)
            {
                if (dr["Componenttype"].ToString().ToUpper().Trim() == "EARNING")
                {
                    earn = earn + Common.CastAsDecimal(dr["wagescaleComponentvalue"].ToString());
                }
                if (dr["Componenttype"].ToString().ToUpper().Trim() == "DEDUCTION")
                {
                    dedc = dedc + Common.CastAsDecimal(dr["wagescaleComponentvalue"].ToString());
                }
            }       
            //}
        }

        GetExtraOTRate(Convert.ToInt32(dp_wagescale.SelectedValue), Convert.ToInt32(dp_Rank.SelectedValue));
        earn = Math.Round(earn, 2);
        dedc = Math.Round(dedc, 2);
        lb_Gross.Text = Math.Round(Common.CastAsDecimal(earn.ToString()), 2).ToString(); 
        lb_deduction.Text = Math.Round(Common.CastAsDecimal(dedc.ToString()), 2).ToString(); 
        if (! string.IsNullOrWhiteSpace(txt_Other_Amount.Text))
        {
            totalEarning = Math.Round(earn + Common.CastAsDecimal(txt_Other_Amount.Text.ToString()),2);
        }
        else
        {
            totalEarning = Math.Round(earn,2);
        }
        
        totalDeduction = Math.Round(dedc,2);
        lblTotalEarning.Text = Math.Round(Common.CastAsDecimal(totalEarning.ToString()), 2).ToString(); 
        lblTotalDeduction.Text = Math.Round(Common.CastAsDecimal(totalDeduction.ToString()), 2).ToString();
        Net_earn = earn - dedc;
        lb_NewEarning.Text = Math.Round(Common.CastAsDecimal(Net_earn.ToString()), 2).ToString();
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
    public void Show_Record_Crew(int CrewId,int VesselId)
    {
        string Mess;
        Mess = "";
        DataTable dt11; 
        if (Session["PCMode"].ToString().Trim() == "1") // general portcall
        {
            dt11 = CrewContract.getCrewdetails(CrewId, _PortCallId);
        }
        else if (Session["PCMode"].ToString().Trim() == "3") // Contract Revision
        {
            string sql = "select ccr.crewid,crewnumber,firstname + ' ' + middlename + ' ' + lastname as [name],ccr.vesselid as PlannedVesselID,currentrankid as rank,rankname,vesselname,nationalityid  from CrewContractRevision ccr inner join crewpersonaldetails cpd on ccr.crewid = cpd.crewid inner join rank r on r.rankid = cpd.currentrankid inner join vessel v on v.vesselid = ccr.vesselid where ccr.crewid=" + CrewId.ToString() + " and ccr.vesselid=" + _VesselId.ToString();
            dt11 = Budget.getTable(sql).Tables[0];
        }
        else
        {
            string sql = "select ps.crewid,crewnumber,firstname + ' ' + middlename + ' ' + lastname as [name],ps.vesselid as PlannedVesselID,currentrankid as rank,rankname,vesselname,nationalityid  " +
                   "from promotionsignon ps inner join crewpersonaldetails cpd on ps.crewid=cpd.crewid inner join rank r on r.rankid=cpd.currentrankid inner join vessel v on v.vesselid=ps.vesselid where ps.crewid=" + CrewId.ToString() + " and ps.vesselid=" + _VesselId.ToString();
            dt11 = Budget.getTable(sql).Tables[0]; 
        }
        //---------------------------
        foreach (DataRow dr in dt11.Rows)
        {
            btn_cancelContract.Enabled = false;
            btn_ContLetter.Enabled = false;
            
            lb_name.Text = dr["Name"].ToString();
            ViewState["Curr_VesselID"] = dr["PlannedVesselID"].ToString();
           
            ViewState["Rank_ID"] = dr["Rank"].ToString();
            bind_Rank(Convert.ToInt32(dr["Rank"].ToString()));
            Lb_PlanRank.Text = dr["RankName"].ToString();

            ////DataTable dtr1 = Budget.getTable("select rankid,( select rankcode from rank r where r.rankid=c.rankid ) as RankCode from crewcontractheader c where contractid=" + ViewState["Cont_id"].ToString()).Tables[0];

            ////int _ContractId = Convert.ToInt32(ViewState["Cont_id"].ToString());
            ////if (_ContractId > 0)
            ////{
            ////    ViewState["Rank_ID"] = dtr1.Rows[0]["rankid"].ToString();
            ////    bind_Rank(Convert.ToInt32(dtr1.Rows[0]["rankid"].ToString()));
            ////    Lb_PlanRank.Text = dtr1.Rows[0]["RankCode"].ToString();
            ////}
            ////else
            ////{
            ////    ViewState["Rank_ID"] = dr["Rank"].ToString();
            ////    bind_Rank(Convert.ToInt32(dr["Rank"].ToString()));
            ////    Lb_PlanRank.Text = dr["RankName"].ToString();
            ////}

            lb_PlanVessel.Text = dr["VesselName"].ToString();
            Mess = Mess + Alerts.Set_DDL_Value(dp_nationality, dr["Nationalityid"].ToString(), "Nationality");
            if (Mess.Length > 0)
            {
                this.Label1.Text = "The Status Of Following Has Been Changed To In-Active <br/>" + Mess.Substring(1);
                this.Label1.Visible = true;
            }
            
            DataTable dt_wage = CrewContract.getWages(Convert.ToInt32(dr["PlannedVesselID"].ToString()));
            dp_wagescale.DataSource = dt_wage;
            dp_wagescale.DataTextField = "WageScaleName";
            dp_wagescale.DataValueField = "WageScaleid";
            dp_wagescale.DataBind();
            break;
        }
    }
    protected void Show_CrewData()
    {
        ViewState["VID"] = _VesselId;
        clearData();
        Show_Record_Crew(_CrewId, _VesselId);
        DataTable dt= Budget.getTable("select currentrankid from crewpersonaldetails where crewid=" + _CrewId).Tables[0];
        if (dp_Rank.Items.FindByValue(ViewState["Rank_ID"].ToString()) != null)
        {
            dp_Rank.SelectedValue = ViewState["Rank_ID"].ToString();
        }
        else
        {
            dp_Rank.SelectedIndex = 0;
        }
        btn_ShowWages.Enabled = true;
       // gv_Contract.SelectedIndex = -1;
        Activeall();
    }
    protected void btn_ShowWages_Click(object sender, EventArgs e)
    {
        bindGd_AssignWages();
    }
    //protected void btn_print_CheckList_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        HiddenField hfd;
    //        int CrewId, ContractId, Vesselid;
    //        if (gv_Contract.SelectedIndex >= 0)
    //        {
    //            CrewId = Convert.ToInt32(ViewState["CrewId"].ToString());
    //            Vesselid = Convert.ToInt32(ViewState["Vsl_id"].ToString());
    //            ContractId = Convert.ToInt32(ViewState["Cont_id"].ToString());
    //            if (gv_Contract.SelectedIndex >= 0)
    //            {
    //                DataTable dt1 = ReportCheckList.selectCrewCheckListDetails(ContractId);
    //                if (dt1.Rows.Count > 0)
    //                {
    //                    Page.ClientScript.RegisterStartupScript(this.GetType(), "abc", "javascript:window.open('../Reporting/ReportCrewCheckLists.aspx?CrewId=" + CrewId.ToString() + "&Vesselid=" + Vesselid.ToString() + " &Contractid=" + ContractId.ToString() + "',null,'title=no,toolbars=no,scrollbars=yes,width=850,height=600,left=50,top=50,addressbar=no');", true);
    //                }
    //                else
    //                {
    //                    Label1.Text = "CheckList not created.";
    //                }
    //            }
    //        }
    //        else
    //        {
    //            Label1.Text = "Please select any Contract.";
    //        }
    //        //}
    //    }
    //    catch
    //    {
    //        Label1.Text = "Please select any Contract.";
    //    }
    //}
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
            //Show_Prev_Contracts();
            btn_Save.Enabled = false;
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
        double OAmount = 0, ExtraOTRate = 0;
       
        try
        {
            DateTime dtstart = DateTime.Parse(txt_startdt.Text);
        }
        catch 
        {
            Label1.Text = "Please enter start date.";
            return;
        }
        DataTable dttable1 = cls_SearchReliever.Check_CrewContractDate(Convert.ToDateTime(txt_startdt.Text), Convert.ToInt32(ViewState["VID"].ToString()));
        foreach (DataRow dr in dttable1.Rows)
        {
            if (Convert.ToInt32(dr[0].ToString()) > 0)
            {
                Label1.Text = "Start date can't be less than last payroll date.";
                return;
            }
        }
        double Amount = 0;
        if (dp_Rank.SelectedValue == "0")
        {
            Label1.Text = "Please select rank.";
            return;
        }
        DateTime dt;
        int x;
        x = Convert.ToInt32(Txt_ContractPeriod.Text);
        dt = Convert.ToDateTime(txt_startdt.Text);
        dt = dt.AddMonths(x);
        //CHECK FOR SHOW WAGES MUST BE CLICKED AND A VALID AMOUNT IS AVAILABLE OF THE NEW WAGES
        //----------------------------
        try
        { 
            if (!string.IsNullOrWhiteSpace(lb_NewEarning.Text) && Convert.ToDouble(lb_NewEarning.Text) > 0)
            {
                Amount = Convert.ToDouble((lb_NewEarning.Text));
            }
            
        }
        catch
        {
            Label1.Text = "Please click on show wages.";
            return;
        }

        try
        {
           if (! string.IsNullOrWhiteSpace(txt_Other_Amount.Text) && Convert.ToDouble(txt_Other_Amount.Text) > 0)
            {
                OAmount = Convert.ToDouble(txt_Other_Amount.Text);
            }
           
           if (!string.IsNullOrWhiteSpace(txtExtraOtRate.Text) && Convert.ToDouble(txtExtraOtRate.Text) > 0)
            {
                ExtraOTRate = Convert.ToDouble(txtExtraOtRate.Text);
            }
        }
        catch {
            Label1.Text = "Please click on Extra Other Amount or Extra OT Rate.";
            return;
        }
        try
        {
            int contid;
            int _PortCallId = 0;
            if (Session["PCMode"].ToString().Trim() == "1") // general portcall
            {
                _PortCallId = Common.CastAsInt32(Session["Planned_PortOnCallId"]);
            }
            contid = CrewContract.InsertContractDetails(_CrewId, Convert.ToInt32(dp_Rank.SelectedValue), Convert.ToInt32(Txt_Seniority.Text), Convert.ToDateTime(txt_issuedt.Text), Convert.ToDateTime(txt_startdt.Text), Convert.ToDateTime(dt.ToString()), Convert.ToInt32(Txt_ContractPeriod.Text), Convert.ToInt32(dp_wagescale.SelectedValue), Convert.ToInt32(dp_nationality.SelectedValue), Convert.ToInt32(Session["loginid"]), txt_Remark.Text.Trim(), Convert.ToInt32(dp_Rank.SelectedValue), _PortCallId, OAmount,0, ExtraOTRate, Convert.ToInt32(ddlTravelPay.SelectedValue), _IsContractRevision);
            if(contid  > 0) // save child details
            {
                for(int i=0;i<=Gd_AssignWages.Rows.Count -1;i++)
                {
                    int Wid = Common.CastAsInt32(((HiddenField)Gd_AssignWages.Rows[i].FindControl("hfdCompId")).Value);
                    decimal Amt = Common.CastAsDecimal(((TextBox)Gd_AssignWages.Rows[i].FindControl("txtAmount")).Text);
                    Budget.getTable("EXEC dbo.InsertContractDetails_Details " + contid.ToString() + "," + Wid.ToString() + "," + Amt.ToString());  
                }

                for (int i = 0; i <= Gd_AssingWagesDeduction.Rows.Count - 1; i++)
                {
                    int Wid = Common.CastAsInt32(((HiddenField)Gd_AssingWagesDeduction.Rows[i].FindControl("hfdCompId")).Value);
                    decimal Amt = Common.CastAsDecimal(((TextBox)Gd_AssingWagesDeduction.Rows[i].FindControl("txtAmount")).Text);
                    Budget.getTable("EXEC dbo.InsertContractDetails_Details " + contid.ToString() + "," + Wid.ToString() + "," + Amt.ToString());
                }

                Budget.getTable("UPDATE CrewContractCheckList SET CONTRACTID=" + contid.ToString() + " WHERE CONTRACTID=-" + _CrewId.ToString());
                Budget.getTable("exec UpdateContractWageScaleRankDetails " + contid.ToString() + "," + ViewState["WageScaleRankId"]);
                Label1.Text = "Contract Details Saved successfully.";
            }
           
            

            Page.ClientScript.RegisterStartupScript(this.GetType(), "dialog", "window.opener.document.getElementById('ctl00_ContentMainMaster_btnRefresh').click();window.close();", true);
            btn_ShowWages.Enabled = false;
            btn_Save.Enabled = false;
            if (contid > 0)
            {
                Show_Contract(Common.CastAsInt32(contid));
            }
        }
        catch(Exception ex)
        {
            Label1.Text = "Record not saved. Error :" + ex.Message;
        }
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
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "cont", "window.open('../Reporting/PrintContract.aspx?ContractId=" + Request.QueryString["ContractId"].ToString() + "&mode=2');", true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "cont1", "window.open('../Reporting/EmpContractReport.aspx?Cont_ID=" + Request.QueryString["ContractId"].ToString() + "');", true);  
        }
        catch
        {
        }
    }
    //protected void GridView_SelectIndexChanged(object sender, EventArgs e)
    //{
    //    HiddenField hfd;
    //    hfd = (HiddenField)gv_Contract.Rows[gv_Contract.SelectedIndex].FindControl("hfd_ContractId");
    //    Show_Contract(Convert.ToInt32(hfd.Value));
    //    ////////////////////
    //    Session["ChecklistContractId"] = hfd.Value;
    //    /////////////////////
    //    btn_ShowWages.Enabled = false;
    //    btn_Save.Enabled = false;
    //}
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
            txtExtraOtRate.Text = dt.Rows[0]["ExtraOTRate"].ToString();
            dp_wagescale.Items.Clear();
            dp_wagescale.Items.Add(new ListItem(dt.Rows[0]["WageScaleName"].ToString(), dt.Rows[0]["WageScaleId"].ToString()));
            Mess = Mess + Alerts.Set_DDL_Value(dp_Rank, dt.Rows[0]["NewRankID"].ToString(), "Rank");
            Mess = Mess + Alerts.Set_DDL_Value(dp_nationality, dt.Rows[0]["Nationalityid"].ToString(), "Nationality");
            DeActiveall();
            lb_Gross.Text = Convert.ToString(Math.Round(double.Parse(dt.Rows[0]["ETotal"].ToString()), 2));
            lb_deduction.Text = Convert.ToString(Math.Round(double.Parse(dt.Rows[0]["DTotal"].ToString()), 2));
            z = Math.Round(mydoubleparse(lb_Gross.Text) - mydoubleparse(lb_deduction.Text), 2);
            lb_NewEarning.Text = z.ToString();
            ddlTravelPay.SelectedValue = dt.Rows[0]["TravelPayCriteria"].ToString();

        }
        //dt = CrewContract.bind_AssignedWages_by_StartDate(txt_startdt.Text, Convert.ToInt32(Txt_Seniority.Text), Convert.ToInt32(dp_nationality.SelectedValue), Convert.ToInt32(dp_wagescale.SelectedValue), Convert.ToInt32(dp_Rank.SelectedValue));
        dt = CrewContract.bind_AssignedWages_Total(ContractId);
        lb_deduction.Text = dt.Rows[0][1].ToString(); //ded
        lb_NewEarning.Text = dt.Rows[0][0].ToString();
        z = Math.Round(mydoubleparse(lb_NewEarning.Text) - mydoubleparse(txt_Other_Amount.Text), 2);
        lb_Gross.Text = z.ToString(); //ear
        dt = CrewContract.bind_AssignedWages(ContractId);
        if (dt.Rows.Count > 0)
        {
            DataView dvEarnings = new DataView(dt);
            DataView dvDeducts = new DataView(dt);
            dvEarnings.RowFilter = " ComponentType Like '%Earning%' ";
            dvDeducts.RowFilter = " ComponentType Like '%Deduction%' ";

            Gd_AssignWages.DataSource = dvEarnings;
            Gd_AssignWages.DataBind();
            Gd_AssingWagesDeduction.DataSource = dvDeducts;
            Gd_AssingWagesDeduction.DataBind();
        }
        
        if (Mess.Length > 0)
        {
            this.Label1.Text = "The Status Of Following Has Been Changed To In-Active <br/>" + Mess.Substring(1);
            //this.Label1.Visible = true;
        }
    }
    public double mydoubleparse(object inp)
    {
        double d=0;
        try{
            double.Parse(inp.ToString());
        }
        catch {
        d=0;
        }
        return d;
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
        Gd_AssingWagesDeduction.DataBind();
        lb_Gross.Text = "";
        lb_deduction.Text = "";
        lb_NewEarning.Text = "";
        txt_Remark.Text = "";
        txt_Other_Amount.Text = "";
    }
    protected void GetExtraOTRate(int wageScaleId, int rank)
    {
        string sql = "Select OTRate from WagescaleOTRates with(nolock) where WageScaleRankId = (Select WageScaleRankId from Wagescalerank with(nolock) where WageScaleId = " + wageScaleId + ") And RankId = " + rank + "";
        DataTable dtOTRate = Budget.getTable(sql).Tables[0];
        if (dtOTRate.Rows.Count > 0)
        {
            txtExtraOtRate.Text = Math.Round(double.Parse(dtOTRate.Rows[0][0].ToString()), 2).ToString();
        }
    }
}
