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

public partial class JobPlanning : System.Web.UI.Page
{
    AuthenticationManager Auth;
    // current status 
    public int PageNo
    {
        set { ViewState["PageNo"] = value; }
        get { return int.Parse("0" + ViewState["PageNo"]); }
    }
    public int PagesSlot
    {
        set { ViewState["PagesSlot"] = value; }
        get { return int.Parse("0" + ViewState["PagesSlot"]); }
    }
    // size of slot/records
    public int PageSlotsCount = 20;
    public int PageRecordsCount = 25;
    // max no of slot/pages
    public int TotalPages
    {
        set { ViewState["TotalPages"] = value; }
        get { return int.Parse("0" + ViewState["TotalPages"]); }
    }
    public int TotalSlots
    {
        set { ViewState["TotalSlots"] = value; }
        get { return int.Parse("0" + ViewState["TotalSlots"]); }
    }

    string SessionDeleimeter = ",";
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        string temppath = Server.MapPath("~/TEMPUPLOAD");
        try
        {
            string[] iners = System.IO.Directory.GetDirectories(temppath);
            foreach (string s in iners)
            {
                System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(s);
                if (di.CreationTime.AddDays(1) < DateTime.Now)
                {
                    try
                    {
                        System.IO.Directory.Delete(s,true);
                    }
                    catch (Exception ex)
                    { }
                }
            }
        }
        catch (Exception ex)
        { }
        //***********Code to check page acessing Permission
        if (Session["UserType"].ToString() == "O")
        {
            Auth = new AuthenticationManager(1039, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
            if (!(Auth.IsView))
            {
                Response.Redirect("~/AuthorityError.aspx");

            }
            else
            {
                btnPring.Visible = Auth.IsPrint;
            }
        }
        //***********
        if (!Page.IsPostBack)
        {
            PageNo = 1;
            PagesSlot = 1;
            Session["CurrentModule"] = 1;
            BindJobTypes();
            BindRanks();
            //if ("" + Request.QueryString["Message"] != "")
            //{
            //    lblEMsg.Text = Request.QueryString["Message"].ToString();
            //    lblEMsg.Visible = true;
            //}

            btnNewPlan.Visible = (Session["UserType"].ToString() == "S");
            if (Session["UserType"].ToString() == "S")
            {
                if (Session["UserName"].ToString().ToUpper() == "DEMO" || Session["UserName"].ToString().ToUpper() == "MASTER" || Session["UserName"].ToString().ToUpper() == "CHENG" || Session["UserName"].ToString() == "CHOFF")
                {
                    btnNewPlan.Visible = true;
                }
                else
                {
                    btnNewPlan.Visible = false;
                }
            }

            if (Session["UserType"].ToString() == "O")
            {
                tdByVessel.Visible = true;
                tdFleet.Visible = true;
                BindFleet();
		        BindVessels();
            }
            else
            {
                tdByVessel.Visible = false;
                tdFleet.Visible = false;
            }
            DefaultPageSetting();
            
            if (Session["UserType"].ToString() == "S")
            {
                btnSearch_Click(sender, e);
            }
        }

    }

    #region ----------- UDF -----------------
    public void DefaultPageSetting()
    {
        if (Session["JobPlanningSearch"] != null)
        {
            LoadSession();
        }
        else
        {
            chkvjobs.Checked = true;
        }
        //btnSearch_Click(null, null);
    }
    public void BindFleet()
    {
        try
        {
            DataTable dtFleet = Common.Execute_Procedures_Select_ByQuery("SELECT FleetId,FleetName as Name FROM dbo.FleetMaster");
            this.ddlFleet.DataSource = dtFleet;
            this.ddlFleet.DataValueField = "FleetId";
            this.ddlFleet.DataTextField = "Name";
            this.ddlFleet.DataBind();
            ddlFleet.Items.Insert(0, new ListItem("< ALL Fleet >", "0"));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BindVessels()
    {
        DataTable dtVessels = new DataTable();
        string strvessels = "SELECT VesselId,VesselCode,VesselName FROM dbo.Vessel VM WHERE vesselstatusid=1  and EXISTS(SELECT TOP 1 COMPONENTID FROM  VSL_ComponentMasterForVessel EVM WHERE VM.VesselCode = EVM.VesselCode ) ORDER BY VesselName";
        dtVessels = Common.Execute_Procedures_Select_ByQuery(strvessels);
        if (dtVessels.Rows.Count > 0)
        {
            ddlVessels.DataSource = dtVessels;
            ddlVessels.DataTextField = "VesselName";
            ddlVessels.DataValueField = "VesselCode";
            ddlVessels.DataBind();
        }
        else
        {
            ddlVessels.DataSource = null;
            ddlVessels.DataBind();
        }
        ddlVessels.Items.Insert(0, new ListItem("< Vessel >",""));
    }
    public void BindRanks()
    {
        DataTable dtRanks = new DataTable();
        string strSQL = "SELECT RankId,RankCode FROM Rank ORDER BY RankLevel";
        dtRanks = Common.Execute_Procedures_Select_ByQuery(strSQL);
        lbAssignedTo.DataSource = dtRanks;
        lbAssignedTo.DataTextField = "RankCode";
        lbAssignedTo.DataValueField = "RankId";
        lbAssignedTo.DataBind();

    }
    public void BindJobTypes()
    {
        string strJobType = "SELECT JobId,JobCode,JobName FROM JobMaster Order By JobCode ";
        DataTable dtJobs = Common.Execute_Procedures_Select_ByQuery(strJobType);
        lbJobTypes.DataSource = dtJobs;
        lbJobTypes.DataTextField = "JobName";
        lbJobTypes.DataValueField = "JobId";
        lbJobTypes.DataBind();
    }
    public DataTable BindRanksForPlanning()
    {
        string Critical="";
        DataTable dtRanks = new DataTable();
        string strSQL ="";

        if(Critical.Trim()=="") 
            strSQL = "SELECT RankId,RankCode FROM Rank ORDER BY RankLevel";
        else
            strSQL = "SELECT RankId,RankCode FROM Rank where rankid in (1,2,12,15) ORDER BY RankLevel";
        
        dtRanks = Common.Execute_Procedures_Select_ByQuery(strSQL);
        DataRow dr;
        dr = dtRanks.NewRow();
        dr["RankId"] = "0";
        dr["RankCode"] = "Select";
        dtRanks.Rows.InsertAt(dr, 0);
        dtRanks.AcceptChanges();
        return dtRanks;
    }
    public Boolean IsValidated()
    {
        Boolean Ischecked = false;
        foreach (RepeaterItem rptItem in rptSearch.Items)
        {
            CheckBox chkSelect = (CheckBox)rptItem.FindControl("chkSelect");
            if (chkSelect.Checked)
            {
                Ischecked = true;
                break;
            }
        }
        if (Ischecked == false)
        {
            //MessageBox1.ShowMessage("Please select a job.", true);
            ProjectCommon.ShowMessage("Please select a job to plan.");
            return false;
        }
        else
        {
            foreach (RepeaterItem rptItem in rptSearch.Items)
            {
                CheckBox chkSelect = (CheckBox)rptItem.FindControl("chkSelect");
                DropDownList ddlAssignTO = (DropDownList)rptItem.FindControl("ddlAssignTO");
                TextBox txtPlanDate = (TextBox)rptItem.FindControl("txtPlanDate");
                HiddenField hfLastDone = (HiddenField)rptItem.FindControl("hfLastDone");
                if (chkSelect.Checked)
                {
                    if (ddlAssignTO.SelectedIndex == 0)
                    {
                        //MessageBox1.ShowMessage("Please select a rank.", true);
                        ProjectCommon.ShowMessage("Please select a rank to plan.");
                        ddlAssignTO.Focus();
                        return false;
                    }
                    if (txtPlanDate.Text == "")
                    {
                        //MessageBox1.ShowMessage("Please enter plan date.", true);
                        ProjectCommon.ShowMessage("Please enter plan date.");
                        txtPlanDate.Focus();
                        return false;
                    }
                    if (hfLastDone.Value != "")
                    {
                        if (DateTime.Parse(txtPlanDate.Text) < DateTime.Parse(hfLastDone.Value))
                        {
                            //MessageBox1.ShowMessage("Plan date should be more than last done date.", true);
                            ProjectCommon.ShowMessage("Plan date should be more than last done date.");
                            txtPlanDate.Focus();
                            return false;
                        }
                    }
                    if (DateTime.Parse(txtPlanDate.Text) < DateTime.Today.Date)
                    {
                        //MessageBox1.ShowMessage("Plan date can not be less than today.", true);
                        ProjectCommon.ShowMessage("Plan date can not be less than today.");
                        txtPlanDate.Focus();
                        return false;
                    }
                }
            }
        }
        return true;
    }
    private void LoadSession()
    {
        string[] Delemeters = { SessionDeleimeter };
        string values = "" + Session["JobPlanningSearch"];
        string[] ValueList = values.Split(Delemeters, StringSplitOptions.None);
        try
        {
            ddlFleet.SelectedValue = ValueList[0];
            ddlFleet_SelectedIndexChanged(new object(), new EventArgs());
            ddlVessels.SelectedValue = ValueList[1];
            txtCompCode.Text = ValueList[2];
            txtCompName.Text = ValueList[3];
            txtDueDays.Text = ValueList[4];
            ddlIntType.SelectedValue = ValueList[5];
            chkCritical.Checked = Convert.ToBoolean(ValueList[6]);
            chkOverdue.Checked = Convert.ToBoolean(ValueList[7]);
            chkClass.Checked = Convert.ToBoolean(ValueList[8]);
            txtClassCode.Text = ValueList[9];
            if (Session["JobTypes"] != null && Session["JobTypes"].ToString() != "")
            {
                string[] jobtype = Session["JobTypes"].ToString().Split(',');

                for (int i = 0; i < jobtype.Length; i++)
                {
                    foreach (ListItem lst in lbJobTypes.Items)
                    {
                        if (jobtype[i].ToString() == lst.Value)
                        {
                            lst.Selected = true;
                        }
                    }
                }
            }
            if (Session["rank"] != null && Session["rank"].ToString() != "")
            {
                string[] rank = Session["rank"].ToString().Split(',');

                for (int i = 0; i < rank.Length; i++)
                {
                    foreach (ListItem lst in lbAssignedTo.Items)
                    {
                        if (rank[i].ToString() == lst.Value)
                        {
                            lst.Selected = true;
                        }
                    }
                }
            }
            if (Session["jostatus"] != null && Session["jostatus"].ToString() != "")
            {
                string[] jostatus = Session["jostatus"].ToString().Split(',');

                for (int i = 0; i < jostatus.Length; i++)
                {
                    foreach (ListItem lst in lbWorkOrderStatus.Items)
                    {
                        if (jostatus[i].ToString() == lst.Value)
                        {
                            lst.Selected = true;
                        }
                    }
                }
            }
            chkvjobs.Checked = Convert.ToBoolean(ValueList[10]);
        }
        catch { }
    }
    private void WriteSession()
    {
        string critical = chkCritical.Checked ? "true" : "false";
        string overdue = chkOverdue.Checked ? "true" : "false";
        string ClassCode = chkClass.Checked ? "true" : "false";
        string verifyjobs = chkvjobs.Checked ? "true" : "false";
        
        string jobtypes = "";
        string rank = "";
        string jobstatus = "";
        foreach (ListItem lst in lbJobTypes.Items)
        {
            if (lst.Selected)
            {
                jobtypes = jobtypes + lst.Value + ",";
            }
        }
        if (jobtypes != "")
        {
            jobtypes = jobtypes.Remove(jobtypes.Length - 1);
        }
        foreach (ListItem lst in lbAssignedTo.Items)
        {
            if (lst.Selected)
            {
                rank = rank + lst.Value + ",";
            }
        }
        if (rank != "")
        {
            rank = rank.Remove(rank.Length - 1);
        }
        foreach (ListItem lst in lbWorkOrderStatus.Items)
        {
            if (lst.Selected)
            {
                jobstatus = jobstatus + lst.Value + ",";
            }
        }
        if (jobstatus != "")
        {
            jobstatus = jobstatus.Remove(jobstatus.Length - 1);
        }
        string values = ((ddlFleet.SelectedValue) + SessionDeleimeter +
                        ddlVessels.SelectedValue + SessionDeleimeter +
                        txtCompCode.Text.Trim() + SessionDeleimeter +
                        txtCompName.Text.Trim() + SessionDeleimeter +
                        txtDueDays.Text.Trim() + SessionDeleimeter +
                        ddlIntType.SelectedValue + SessionDeleimeter +
                        critical + SessionDeleimeter +
                        overdue + SessionDeleimeter +
                        ClassCode + SessionDeleimeter +
                        txtClassCode.Text.Trim() + SessionDeleimeter  + verifyjobs +SessionDeleimeter);
        Session["JobPlanningSearch"] = values;
        Session["JobTypes"] = jobtypes;
        Session["rank"] = rank;
        Session["jostatus"] = jobstatus;
    }
    private void ClearSession()
    {
        Session["JobPlanningSearch"] = null;
    }

    #endregion -------------------------------

    #region -------------- EVENTS ------------------------------
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvJP');", true);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        WriteSession();
        //--------- Fleet & Vessel Filter ---------    
        string VesselCodes = "";
        
        if (Session["UserType"].ToString() == "O")
        {
            if (ddlVessels.SelectedIndex == 0)
            {
                foreach (ListItem vsl in ddlVessels.Items)
                {
                    if (vsl.Value.Trim() != "")
                    {
                        VesselCodes += "," + vsl.Value;
                    }
                }
                if (VesselCodes.Trim() != "") { VesselCodes = VesselCodes.Substring(1); }
            }
            else
            {
                VesselCodes = ddlVessels.SelectedValue;
            }
            
        }
        else
        {
            VesselCodes = Session["CurrentShip"].ToString();
        }

        //--------- Job Category Filter ---------    
        string JobCats = "";
        if (lbJobTypes.SelectedIndex > -1)
        {
            foreach (ListItem jbt in lbJobTypes.Items)
            {
                if (jbt.Selected) { JobCats += "," + jbt.Value; }
            }
        }
        else
        {
            foreach (ListItem jbt in lbJobTypes.Items)
            {
                JobCats += "," + jbt.Value;
            }
        }
        
        if (JobCats.Trim() != "") { JobCats = JobCats.Substring(1); }
        
        //--------- Job Interval Type ---------
        string JobIntTypes = "";
        if (ddlIntType.SelectedIndex==0)
            JobIntTypes = "1,2,3,4,5";
        else if (ddlIntType.SelectedIndex == 1)
            JobIntTypes = "2,3,4,5";
        else
            JobIntTypes = "1";

        //--------- Assigned To ---------    
        string AssignedTo = "";
        if (lbAssignedTo.SelectedIndex > -1)
        {
            foreach (ListItem ast in lbAssignedTo.Items)
            {
                if (ast.Selected) { AssignedTo += "," + ast.Value; }
            }
        }
        else
        {
            foreach (ListItem ast in lbAssignedTo.Items)
            {
                AssignedTo += "," + ast.Value;
            }
            AssignedTo += ",0";
        }

        if (AssignedTo.Trim()!= "") { AssignedTo = AssignedTo.Substring(1); }

        //--------- Plan Status ---------    
        string PlanStatus = "";
        if (lbWorkOrderStatus.SelectedIndex > -1)
        {
            foreach (ListItem ps in lbWorkOrderStatus.Items)
            {
                if (ps.Selected) { PlanStatus += "," + ps.Value; }
            }
        }
        else
        {
            foreach (ListItem ps in lbWorkOrderStatus.Items)
            {
                PlanStatus += "," + ps.Value;
            }
        }

        int SMODE = 0;
        if (chkDefect.Checked && chkUnPlanned.Checked)
            SMODE = 3;
        else if (chkDefect.Checked)
            SMODE = 1;
        else if (chkUnPlanned.Checked)
            SMODE = 2;

        if (PlanStatus.Trim() != "") { PlanStatus = PlanStatus.Substring(1); }
        //---------------------
        string SQL = "EXEC DBO.sp_GETJOBPLANNING '" + VesselCodes + "','" + txtCompCode.Text.Trim() + "%','%" + txtCompName.Text.Trim() + "%','" + JobCats + "','" + JobIntTypes + "'," + ((chkCritical.Checked) ? "1" : "0") + "," + ((chkClass.Checked) ? "1" : "0") + ",'%" + txtClassCode.Text.Trim() + "%','" + AssignedTo + "','" + PlanStatus + "','" + txtDueDays.Text.Trim() + "'," + ((chkOverdue.Checked) ? "1" : "0") + "," + SMODE + "," + ((chkvjobs.Checked) ? "1" : "0");
        Session.Add("sSqlForPrint", SQL);
        //---------------------
        Refresh();
    }

    
    protected void btnClear_Click(object sender, EventArgs e)
    {
        if (Session["UserType"].ToString() == "O")
        {
            ddlFleet.SelectedIndex = 0;
            ddlVessels.SelectedIndex = 0;
        }
        txtCompCode.Text = "";
        txtCompName.Text = "";
        txtDueDays.Text = "";
        lbJobTypes.SelectedIndex = -1;
        ddlIntType.SelectedIndex = 0;
        chkOverdue.Checked = false;
        lbAssignedTo.SelectedIndex = -1;
        lbWorkOrderStatus.SelectedIndex = -1;
        chkvjobs.Checked = false;            
        ClearSession();
    }
    
    protected void btnStartDiscussion_Click(object sender, EventArgs e)
    {
        string vslcode = "";
        System.Collections.Generic.List<int> ids = new System.Collections.Generic.List<int>()   ;
        foreach (RepeaterItem rptItem in rptSearch.Items)
        {
            //CheckBox chkSelect = (CheckBox)rptItem.FindControl("chkSelect");
           
            HtmlInputRadioButton rbt = (HtmlInputRadioButton)rptItem.FindControl("rdbRadio");
            //if (chkSelect.Checked)
            //{
            //    ids.Add(Common.CastAsInt32(hfjobId.Value));
            //}
            //RadioButton rbt = (RadioButton)rptItem.FindControl("rdbRadio");
            if (rbt.Checked)
            {
                HiddenField hfVesselCode = (HiddenField)rptItem.FindControl("hfVesselCode");
                vslcode = hfVesselCode.Value;
                HiddenField hfjobId = (HiddenField)rptItem.FindControl("hfjobId");
                ids.Add(Common.CastAsInt32(hfjobId.Value));
                break;
            }
        }
        if(ids.Count!=1)
        {
            ProjectCommon.ShowMessage("Select Job to send comments to ship.");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "fds", "window.open('JobPlanningComments.aspx?key=" + vslcode + "|" + ids[0].ToString() + "','' );", true);
        }
    }
    protected void btnNewPlan_Click(object sender, EventArgs e)
    {
        if (!IsValidated())
        {
            return;
        }
        foreach (RepeaterItem rptItem in rptSearch.Items)
        {
            CheckBox chkSelect = (CheckBox)rptItem.FindControl("chkSelect");
            HiddenField hfCompId = (HiddenField)rptItem.FindControl("hfCompId");
            HiddenField hfVesselCode = (HiddenField)rptItem.FindControl("hfVesselCode");
            HiddenField hfjobId = (HiddenField)rptItem.FindControl("hfjobId");
            HiddenField hfLastDone = (HiddenField)rptItem.FindControl("hfLastDone");
            HiddenField hfLastHour = (HiddenField)rptItem.FindControl("hfLastHour");
            HiddenField hfNextDueDate = (HiddenField)rptItem.FindControl("hfNextDueDate");
            HiddenField hfNextHour = (HiddenField)rptItem.FindControl("hfNextHour");
            DropDownList ddlAssignTO = (DropDownList)rptItem.FindControl("ddlAssignTO");
            TextBox txtPlanDate = (TextBox)rptItem.FindControl("txtPlanDate");
            if (chkSelect.Checked)
            {
                Common.Set_Procedures("sp_InsertUpdateJobPlanning");
                Common.Set_ParameterLength(10);
                Common.Set_Parameters(
                    new MyParameter("@VesselCode", hfVesselCode.Value),
                    new MyParameter("@ComponentId", hfCompId.Value),
                    new MyParameter("@CompjobId", hfjobId.Value),
                    new MyParameter("@AssignedTo", ddlAssignTO.SelectedValue),
                    new MyParameter("@Status", 1),
                    new MyParameter("@NextDueDate", hfNextDueDate.Value),
                    new MyParameter("@NextDueHours", hfNextHour.Value),
                    new MyParameter("@LastDone", hfLastDone.Value),
                    new MyParameter("@LastHour", hfLastHour.Value),
                    new MyParameter("@PlanDate", txtPlanDate.Text.Trim())
                    );
                DataSet dsJobPlanning = new DataSet();
                dsJobPlanning.Clear();
                Boolean res;
                res = Common.Execute_Procedures_IUD(dsJobPlanning);
                if (res)
                {
                    ProjectCommon.ShowMessage("Jobs Saved Successfully.");
                }
                else
                {
                    ProjectCommon.ShowMessage("Unable to Save Jobs." + Common.ErrMsg);
                }
            }
        }
        btnSearch_Click(sender, e);
    }
    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFleet.SelectedIndex != 0)
        {
            DataTable dtVessels = new DataTable();
            string strvessels = "SELECT VesselId,VesselCode,VesselName FROM Vessel VM WHERE vesselstatusid=1 and EXISTS(SELECT TOP 1 COMPONENTID FROM  VSL_ComponentMasterForVessel EVM WHERE VM.VesselCode = EVM.VesselCode ) AND VesselCode IN (SELECT VesselCode FROM  dbo.Vessel WHERE FleetId = " + ddlFleet.SelectedValue + ") ORDER BY VesselName";
            dtVessels = Common.Execute_Procedures_Select_ByQuery(strvessels);
            ddlVessels.Items.Clear();
            if (dtVessels.Rows.Count > 0)
            {
                ddlVessels.DataSource = dtVessels;
                ddlVessels.DataTextField = "VesselName";
                ddlVessels.DataValueField = "VesselCode";
                ddlVessels.DataBind();
            }
            else
            {
                ddlVessels.DataSource = null;
                ddlVessels.DataBind();
            }
            ddlVessels.Items.Insert(0, "< SELECT >");
        }
        else
        {
            ddlVessels.Items.Clear();
            BindVessels();
        }
    }
    // Code By Umakant
    protected void btnPring_Click(object sener, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "PP", "window.open('Reports/PrintCrystal.aspx?ReportType=JobPlanning');", true);
    }
    // ****************** Paging & Binding**************************
    protected void lnkPrev20Pages_OnClick(object sender, EventArgs e)
    {
        PagesSlot--;
        PageNo = ((PagesSlot - 1) * PageSlotsCount) + 1;
        Refresh();
    }
    protected void lnkNext20Pages_OnClick(object sender, EventArgs e)
    {
        PagesSlot++;
        PageNo = ((PagesSlot - 1) * PageSlotsCount) + 1;

        Refresh();
    }
    protected void lnPageNumber_OnClick(object sender, EventArgs e)
    {
        LinkButton lnPageNumber = (LinkButton)sender;
        PageNo = Common.CastAsInt32(lnPageNumber.Text);
        Refresh();
    }
    protected void Refresh()
    {
        DataTable dtSearchData = Common.Execute_Procedures_Select_ByQuery(Session["sSqlForPrint"].ToString());
        BindRecordsGrid(dtSearchData);
    }
    protected void BindRecordsGrid(DataTable dtData)
    {
        if (dtData.Rows.Count <= 0)
        {
            TotalPages = 0;
            TotalSlots = 0;

            PageNo = 0;
            PagesSlot = 0;

            lnkPrev20Pages.Visible = false;
            lnkNext20Pages.Visible = false;

            rptSearch.DataSource = null;
            rptSearch.DataBind();
            lblRecordCount.Text = "No Records Found.";

            rptPageNumber.DataSource = null;
            rptPageNumber.DataBind();
        }
        else
        {

            if (PageNo == 0)
            {
                PageNo = 1;
            }
            lblRecordCount.Text = "(" + dtData.Rows.Count + ") Records Found.";

            TotalPages = Common.CastAsInt32(Math.Ceiling(Convert.ToDecimal(dtData.Rows.Count) / PageRecordsCount));
            TotalSlots = Common.CastAsInt32(Math.Ceiling(Convert.ToDecimal(TotalPages) / PageSlotsCount));

            int StartRow = (PageNo - 1) * PageRecordsCount + 1;
            int EndRow = StartRow + PageRecordsCount - 1;

            DataView dvFiltered = dtData.DefaultView;
            dvFiltered.RowFilter = "RowNumber>=" + StartRow + " and RowNumber<=" + EndRow + "";

            rptSearch.DataSource = dvFiltered; ;
            rptSearch.DataBind();

            BindPageSlot();
            BindPageRepeater();
        }
    }
    public void BindPageSlot()
    {
        //---------------
        lnkPrev20Pages.Visible = true;
        lnkNext20Pages.Visible = true;
        //---------------
        if (TotalSlots <= 1)
        {
            lnkPrev20Pages.Visible = false;
            lnkNext20Pages.Visible = false;
        }
        else
        {
            if (PagesSlot <= 1)
            {
                lnkPrev20Pages.Visible = false;
            }
            if (PagesSlot >= TotalSlots)
            {
                lnkNext20Pages.Visible = false;
            }
        }
    }
    public void BindPageRepeater()
    {
        if (PagesSlot == 0)
        {
            PagesSlot = 1;
        }
        int PageFrom = ((PagesSlot - 1) * PageSlotsCount) + 1;
        int PageTo = PageFrom + PageSlotsCount - 1;

        DataTable DtPages = new DataTable();
        DtPages.Columns.Add("PageNumber", typeof(int));

        for (int i = PageFrom; i <= PageTo && i <= TotalPages; i++)
        {
            DtPages.Rows.Add(i.ToString());
        }

        rptPageNumber.DataSource = DtPages;
        rptPageNumber.DataBind();
    }
    public bool ShowHidePostPoneBtn(string WOS,string CriticalType)
    {
        if (WOS == "Issued")
        {
            //if (CriticalType == "C")
            //{
                if (Session["UserName"].ToString() == "demo" || Session["UserName"].ToString() == "MASTER" || Session["UserName"].ToString ()== "CE")
                {   return true;}
                else
                    return false;
            //}
            //return true;
        }
        return false;
    }
    protected void rptSearch_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
            string ARank = ((HiddenField)e.Item.FindControl("hfdAssRank")).Value;
            string PRank=((HiddenField)e.Item.FindControl("hfdPRank")).Value ;
            string Critical = ((HiddenField)e.Item.FindControl("hfdCritical")).Value;
            DropDownList ddl = (DropDownList)e.Item.FindControl("ddlAssignTO");
            HiddenField hfdCompJobId = (HiddenField)e.Item.FindControl("hfdCompJobId");
            HtmlInputRadioButton rdbtn = (HtmlInputRadioButton)e.Item.FindControl("rdbRadio");
            HiddenField hdnJobCode = (HiddenField)e.Item.FindControl("hdnJobCode");
        DataTable dtRanks = new DataTable();
            string strSQL ="";

            try
            {
                strSQL = "SELECT RankId,RankCode FROM Rank WHERE RANKID IN (select rankid from [dbo].[ComponentJobMapping_OtherRanks] where compjobid=" + hfdCompJobId.Value + ") ORDER BY RankLevel ";
                dtRanks = Common.Execute_Procedures_Select_ByQuery(strSQL);
            }
            catch { }
            if (dtRanks == null)
            {
                //if (Critical.Trim() == "")
                    strSQL = "SELECT RankId,RankCode FROM Rank ORDER BY RankLevel";
                //else
                //    strSQL = "SELECT RankId,RankCode FROM Rank where rankid in (1,2,12,15) ORDER BY RankLevel";
            }
            else if (dtRanks.Rows.Count == 0)
            {
                //if (Critical.Trim() == "")
                    strSQL = "SELECT RankId,RankCode FROM Rank ORDER BY RankLevel";
                //else
                //    strSQL = "SELECT RankId,RankCode FROM Rank where rankid in (1,2,12,15) ORDER BY RankLevel";
            }

            dtRanks = Common.Execute_Procedures_Select_ByQuery(strSQL);

            DataRow dr;
            dr = dtRanks.NewRow();
            dr["RankId"] = "0";
            dr["RankCode"] = "Select";
            dtRanks.Rows.InsertAt(dr, 0);
            dtRanks.AcceptChanges();
            ddl.DataTextField = "RankCode";
            ddl.DataValueField= "RankId";
            ddl.DataSource= dtRanks;
            ddl.DataBind();

            if (PRank == "0" || PRank == null || PRank == "")
            {
                if (ARank != "")
                {
                    ddl.SelectedValue = ARank;
                    try { ddl.Items.FindByText(ARank).Selected = true; }
                    catch { }
                }
                
            }
            else
                ddl.SelectedValue = PRank;

            if (hdnJobCode.Value.ToString().ToLower() == "ren" || hdnJobCode.Value.ToString().ToLower() == "ovh" || hdnJobCode.Value.ToString().ToLower() == "sur" || hdnJobCode.Value.ToString().ToLower() == "cha")
        {
            rdbtn.Style.Add("display", "block");
           // rdbtn.Visible = true;
        }
            else
        {
            rdbtn.Style.Add("display", "none");
           // rdbtn.Visible = false;
        }
    }
    //protected void rptSearch_OnItemCreated(object sender, RepeaterItemEventArgs e)
    //{
    //    DropDownList ddl = (DropDownList)e.Item.FindControl("ddlAssignTO");
    //    if (ddl != null)
    //    {
    //        DataTable dtRanks = new DataTable();
    //        string strSQL = "SELECT RankId,RankCode FROM Rank ORDER BY RankLevel";
    //        dtRanks = Common.Execute_Procedures_Select_ByQuery(strSQL);
    //        DataRow dr;
    //        dr = dtRanks.NewRow();
    //        dr["RankId"] = "0";
    //        dr["RankCode"] = "Select";
    //        dtRanks.Rows.InsertAt(dr, 0);
    //        dtRanks.AcceptChanges();
    //        ddl.DataTextField = "RankCode";
    //        ddl.DataValueField= "RankId";
    //        ddl.DataSource= dtRanks;
    //        ddl.DataBind(); 
    //    }
    //}
    //public bool ShowHidePlanningCheckBox(string JobType)
    //{
    //    if (JobType == "C")
    //    {
    //        if (Session["UserName"].ToString().ToUpper() == "MASTER" || Session["UserName"].ToString().ToUpper() == "CE" || Session["UserName"].ToString().ToUpper() == "CO" || Session["UserName"].ToString().ToUpper() == "1AE")
    //        {
    //            return true;
    //        }
    //        return false;
    //    }
    //    return true;
    //}


    // ****************************************************
    #endregion --------------------------------------------------


        
}
