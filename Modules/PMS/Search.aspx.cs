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

public partial class Search : System.Web.UI.Page
{
    string SessionDeleimeter = ",";
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------

        lblEMsg.Text = "";
        lblEMsg.Visible = false;
        if (!Page.IsPostBack)
        {
            Session["CurrentModule"] = 7;
            
            BindJobTypes();
            BindRanks();
            if ("" + Request.QueryString["Message"] != "")
            {
               lblEMsg.Text = Request.QueryString["Message"].ToString();
               lblEMsg.Visible = true;
            }

            //btnNewPlan.Visible = (Session["UserType"].ToString()=="S");
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
        }
    }

    #region ----------- UDF -----------------
    public void DefaultPageSetting()
    {
        txtFromDt.Text = string.Format("{0:dd-MMM-yyyy}", DateTime.Today);
        txtToDt.Text =  string.Format("{0:dd-MMM-yyyy}",DateTime.Today.AddDays(30));
        
        if (Session["MaintenanceSearch"] != null)
        {
            LoadSession();
        }
        btnSearch_Click(null, null);
        
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
            ddlFleet.Items.Insert(0, new ListItem("< ALL Fleet >","0"));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BindVessels()
    {
        DataTable dtVessels = new DataTable();
        string strvessels = "SELECT VesselId,VesselCode,VesselName FROM dbo.Vessel WHERE ISNULL(IsExported,0) = 1 ORDER BY VesselName";
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
        ddlVessels.Items.Insert(0, "< Vessel >");
    }
    public void BindRanks()
    {
        DataTable dtRanks = new DataTable();
        string strSQL = "SELECT RankId,RankCode FROM Rank ORDER BY RankCode ";
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
        DataTable dtRanks = new DataTable();
        string strSQL = "SELECT RankId,RankCode FROM Rank ORDER BY RankCode";
        dtRanks = Common.Execute_Procedures_Select_ByQuery(strSQL);
        
        DataRow dr;
        dr = dtRanks.NewRow();
        dr["RankId"] = "0";
        dr["RankCode"] = "Select";
        dtRanks.Rows.InsertAt(dr,0);
        dtRanks.AcceptChanges();
        return dtRanks;
    }
    public Boolean IsValidated()
    {
        Boolean Ischecked = false;
        foreach (RepeaterItem rptItem in rptSearch.Items)
        {
            RadioButton rdoSelect = (RadioButton)rptItem.FindControl("rdoSelect");
            if (rdoSelect.Checked)
            {
                Ischecked = true;
                break;
            }
        }
        if (Ischecked == false)
        {
            MessageBox1.ShowMessage("Please select a job.", true);
            return false;
        }
        else
        {
            foreach (RepeaterItem rptItem in rptSearch.Items)
            {
                RadioButton rdoSelect = (RadioButton)rptItem.FindControl("rdoSelect");
                DropDownList ddlAssignTO = (DropDownList)rptItem.FindControl("ddlAssignTO");
                TextBox txtPlanDate = (TextBox)rptItem.FindControl("txtPlanDate");
                HiddenField hfLastDone = (HiddenField)rptItem.FindControl("hfLastDone");
                if (rdoSelect.Checked)
                {
                    if (ddlAssignTO.SelectedIndex == 0)
                    {
                        MessageBox1.ShowMessage("Please select a rank.", true);
                        ddlAssignTO.Focus();
                        return false;
                    }
                    if (txtPlanDate.Text == "")
                    {
                        MessageBox1.ShowMessage("Please enter plan date.", true);
                        txtPlanDate.Focus();
                        return false;
                    }
                    if (DateTime.Parse(txtPlanDate.Text) < DateTime.Parse(hfLastDone.Value))
                    {
                        MessageBox1.ShowMessage("Plan date should be more than last done date.", true);
                        txtPlanDate.Focus();
                        return false;
                    }
                    if (DateTime.Parse(txtPlanDate.Text) < DateTime.Today.Date)
                    {
                        MessageBox1.ShowMessage("Plan date can not be less than today.", true);
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
        string values = "" + Session["MaintenanceSearch"];
        string[] ValueList = values.Split(Delemeters, StringSplitOptions.None);
        try
        {
            ddlFleet.SelectedValue = ValueList[0];
            ddlVessels.SelectedValue = ValueList[1];
            txtCompCode.Text = ValueList[2];
            txtCompName.Text = ValueList[3];
            txtFromDt.Text = ValueList[4];
            txtToDt.Text = ValueList[5];
            ddlIntType.SelectedValue = ValueList[6];
            chkCritical.Checked = Convert.ToBoolean(ValueList[7]);
            chkOverdue.Checked = Convert.ToBoolean(ValueList[8]);

        }
        catch { }
    }
    private void WriteSession()
    {
        string critical = chkCritical.Checked ? "true" : "false";
        string overdue = chkOverdue.Checked ? "true" : "false";
        string values = ((ddlFleet.SelectedValue) + SessionDeleimeter +
                        ddlVessels.SelectedValue + SessionDeleimeter +
                        txtCompCode.Text.Trim() + SessionDeleimeter +
                        txtCompName.Text.Trim() + SessionDeleimeter +
                        txtFromDt.Text.Trim() + SessionDeleimeter +
                        txtToDt.Text.Trim() + SessionDeleimeter +
                        ddlIntType.SelectedValue + SessionDeleimeter +
                        critical + SessionDeleimeter +
                        overdue + SessionDeleimeter);
        Session["MaintenanceSearch"] = values;
    }
    private void ClearSession()
    {
        Session["MaintenanceSearch"] = null;
    }

    #endregion -------------------------------

    #region -------------- EVENTS ------------------------------
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvSearch');", true);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        WriteSession();
        string strSearch = "SELECT VCJM.VesselCode,CM.ComponentId,CM.ComponentCode,CM.ComponentName ,JM.JobCode,CASE WHEN LEN(CJM.DescrSh) > 15 THEN Substring(CJM.DescrSh,0,15) + '...' ELSE CJM.DescrSh END AS JobName,VCJM.CompJobId,JIM.IntervalName ,RM.RankCode,VCJM.Interval,replace(convert(varchar(15),VCJM.NextDueDate,106),' ','-') As NextDueDate,CASE WHEN VCJM.NextDueDate > getdate() THEN 'DUE' ELSE 'OVER DUE' END AS DueStatus , CASE PM.Status WHEN 1 THEN 'Issued' WHEN 2 THEN 'Completed' WHEN 3 THEN 'Postponed' WHEN 4 THEN 'Cancelled' ELSE '' END AS WorkOrderStatus,VCJM.NextHour,REPLACE(CONVERT(VARCHAR(15),VCJM.LastDone,106),' ','-') AS LastDone,VCJM.LastHour,DATEDiff(dd,PM.DoneDate,PM.LastDueDate) AS Difference,CASE PM.Status WHEN 4 THEN '' ELSE ISNULL(RM.RankCode,'') END AS PlannedRank,CASE PM.Status WHEN 1 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PlanDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PlanDate,''),106),' ','-') END WHEN 2 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.DoneDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.DoneDate,''),106),' ','-') END WHEN 3 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PostPoneDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PostPoneDate,''),106),' ','-') END  ELSE '' END AS PlanDate FROM " +
                           "VSL_VesselComponentJobMaster VCJM  " +
                           "INNER JOIN Rank RM ON RM.RankId = VCJM.AssignTo  " +
                           "INNER JOIN  " +
	                       "( " +
	                       "ComponentsJobMapping CJM INNER JOIN JobMaster JM  ON JM.JobId = CJM.JobId INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId " +
                           "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCM.ComponentId = CM.ComponentId AND VCM.Status = 'A' " +
                           ") ON VCJM.ComponentId = CJM.ComponentId AND VCJM.CompJobId = CJM.CompJobId AND VCJM.Status = 'A' AND VCM.VesselCode = VCJM.VesselCode " +
                           "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId  " +
                           "LEFT JOIN VSL_PlanMaster PM ON PM.VesselCode = VCJM.VesselCode AND PM.ComponentId = VCJM.ComponentId AND PM.CompJobId = VCJM.CompJobId ";

        string WhereCondition = "WHERE  1=1 ";

        if (Session["UserType"].ToString() == "O")
        {
            if (ddlFleet.SelectedIndex != 0)
            {
                if (ddlVessels.SelectedIndex == 0)
                {
                    string Vessels = "";
                    for (int i = 0; i < ddlVessels.Items.Count; i++)
                    {
                        if (i != 0)
                        {
                            Vessels = Vessels + "'" + ddlVessels.Items[i].Text + "'" + ",";
                        }
                    }
                    if (Vessels.Length > 0)
                    {
                        Vessels = Vessels.Remove(Vessels.Length - 1);
                    }
                    WhereCondition = WhereCondition + "AND VCJM.VesselCode IN (SELECT VesselCode FROM  dbo.Vessel WHERE FleetId = " + ddlFleet.SelectedValue + ")";
                }
                else
                {
                    WhereCondition = WhereCondition + "AND VCJM.VesselCode = '" + ddlVessels.SelectedValue.ToString() + "' ";
                }
            }
            else if (ddlVessels.SelectedIndex != 0)
            {
                WhereCondition = WhereCondition + "AND VCJM.VesselCode = '" + ddlVessels.SelectedValue.ToString() + "' ";
            }
        }
        else
        {
            WhereCondition = WhereCondition + "AND VCJM.VesselCode = '" + Session["CurrentShip"].ToString() + "' ";
        }
        if (txtCompCode.Text != "")
        {
            WhereCondition = WhereCondition + " AND  CM.ComponentCode LIKE '%" + txtCompCode.Text.Trim().ToString() + "%' ";
        }
        if (txtCompName.Text != "")
        {
            WhereCondition = WhereCondition + " AND CM.ComponentName LIKE '%" + txtCompName.Text.Trim().ToString() + "%' ";
        }
        if (lbJobTypes.SelectedIndex != -1)
        {
            string JobTypes = "";
            foreach (ListItem lst in lbJobTypes.Items)
            {
                if (lst.Selected)
                {
                    JobTypes = JobTypes + lst.Value + ",";
                }
            }
            JobTypes = JobTypes.Remove(JobTypes.Length - 1);
            WhereCondition = WhereCondition + " AND JM.JobId IN ( " + JobTypes + " ) ";
        }
        if (chkCritical.Checked)
        {
            WhereCondition = WhereCondition + " AND CM.CriticalEquip = 1 ";
        }
        if (ddlIntType.SelectedIndex != 0)
        {
            if (ddlIntType.SelectedValue == "1")
            {
                WhereCondition = WhereCondition + " AND JIM.IntervalId <> 1 ";
            }
            else
            {
                WhereCondition = WhereCondition + " AND JIM.IntervalId = 1 ";
            }
        }
        if (txtFromDt.Text != "" && txtToDt.Text != "")
        {
            WhereCondition = WhereCondition + " AND VCJM.NextDueDate <= '" + txtToDt.Text.Trim() + "' "; //" AND ( VCJM.NextDueDate Between '" + txtFromDt.Text.Trim() + "' AND '" + txtToDt.Text.Trim() + "' ) ";
        }
        else
        {
            if (txtFromDt.Text != "")
            {
                WhereCondition = WhereCondition + " AND VCJM.NextDueDate >= '" + txtFromDt.Text.Trim() + "' ";
            }
            if (txtToDt.Text != "")
            {
                WhereCondition = WhereCondition + " AND VCJM.NextDueDate <= '" + txtToDt.Text.Trim() + "' ";
            }
        }
        if (chkOverdue.Checked)
        {
            WhereCondition = WhereCondition + " AND VCJM.NextDueDate < CAST(CONVERT(VARCHAR,getdate(),106) AS DATETIME) ";
        }
        //else
        //{
        //    WhereCondition = WhereCondition + " OR VCJM.NextDueDate < DATEADD(dd,1,getdate()) ";
        //}
        if (lbAssignedTo.SelectedIndex != -1)
        {
            string Assigned = "";
            foreach (ListItem lst in lbAssignedTo.Items)
            {
                if (lst.Selected)
                {
                    Assigned = Assigned + lst.Value + ",";
                }
            }
            Assigned = Assigned.Remove(Assigned.Length - 1);
            WhereCondition = WhereCondition + " AND VCJM.AssignTo IN (" + Assigned + ") ";
        }
        //if (lbWorkOrderStatus.SelectedIndex != -1)
        //{
        //    string woStatus = "";
        //    foreach (ListItem lst in lbWorkOrderStatus.Items)
        //    {
        //        if (lst.Selected)
        //        {
        //            woStatus = woStatus + lst.Value + ",";
        //        }
        //    }
        //    woStatus = woStatus.Remove(woStatus.Length - 1);
        //    WhereCondition = WhereCondition + " AND PM.Status IN (" + woStatus + ") ";
        //}
        WhereCondition = WhereCondition + "AND PM.Status = 1 ";
        DataTable dtSearchData = Common.Execute_Procedures_Select_ByQuery(strSearch + WhereCondition );
        if (dtSearchData.Rows.Count > 0)
        {
            rptSearch.DataSource = dtSearchData;
            rptSearch.DataBind();
            lblRecordCount.Text = "Records Found : " + dtSearchData.Rows.Count;
        }
        else
        {
            rptSearch.DataSource = null;
            rptSearch.DataBind();
            lblRecordCount.Text = "No Record Found.";
        }
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
        txtFromDt.Text = "";
        txtToDt.Text = "";
        lbJobTypes.SelectedIndex = -1;
        ddlIntType.SelectedIndex = 0;
        chkOverdue.Checked = false;
        lbAssignedTo.SelectedIndex = -1;
        //lbWorkOrderStatus.SelectedIndex = -1;
        ClearSession();
    }
    //protected void btnNewPlan_Click(object sender, EventArgs e)
    //{
    //    if (!IsValidated())
    //    {
    //        return;
    //    }
    //    foreach (RepeaterItem rptItem in rptSearch.Items)
    //    {
    //        RadioButton rdoSelect = (RadioButton)rptItem.FindControl("rdoSelect");
    //        HiddenField hfCompId = (HiddenField)rptItem.FindControl("hfCompId");
    //        HiddenField hfVesselCode = (HiddenField)rptItem.FindControl("hfVesselCode");
    //        HiddenField hfjobId = (HiddenField)rptItem.FindControl("hfjobId");
    //        HiddenField hfLastDone = (HiddenField)rptItem.FindControl("hfLastDone");
    //        HiddenField hfLastHour = (HiddenField)rptItem.FindControl("hfLastHour");            
    //        HiddenField hfNextDueDate = (HiddenField)rptItem.FindControl("hfNextDueDate");
    //        HiddenField hfNextHour = (HiddenField)rptItem.FindControl("hfNextHour");
    //        DropDownList ddlAssignTO = (DropDownList)rptItem.FindControl("ddlAssignTO");
    //        TextBox txtPlanDate = (TextBox)rptItem.FindControl("txtPlanDate");
    //        if (rdoSelect.Checked)
    //        {
    //            Common.Set_Procedures("sp_InsertUpdateJobPlanning");
    //            Common.Set_ParameterLength(10);
    //            Common.Set_Parameters(                    
    //                new MyParameter("@VesselCode", hfVesselCode.Value),
    //                new MyParameter("@ComponentId", hfCompId.Value),
    //                new MyParameter("@CompjobId", hfjobId.Value),
    //                new MyParameter("@AssignedTo", ddlAssignTO.SelectedValue),
    //                new MyParameter("@Status", 1),
    //                new MyParameter("@NextDueDate", hfNextDueDate.Value),
    //                new MyParameter("@NextDueHours", hfNextHour.Value),
    //                new MyParameter("@LastDone", hfLastDone.Value),
    //                new MyParameter("@LastHour", hfLastHour.Value),
    //                new MyParameter("@PlanDate", txtPlanDate.Text.Trim())
    //                );
    //            DataSet dsJobPlanning = new DataSet();
    //            dsJobPlanning.Clear();
    //            Boolean res;
    //            res = Common.Execute_Procedures_IUD(dsJobPlanning);
    //            if (res)
    //            {
    //                MessageBox1.ShowMessage("Job Saved Successfully.", false);
    //            }
    //            else
    //            {
    //                MessageBox1.ShowMessage("Unable to Save Job.", true);
    //            }
    //        }
    //    }
    //    btnSearch_Click(sender, e);
    //}
    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFleet.SelectedIndex != 0)
        {
            DataTable dtVessels = new DataTable();
            string strvessels = "SELECT VesselId,VesselCode,VesselName FROM Vessel WHERE ISNULL(IsExported,0) = 1 AND VesselCode IN (SELECT VesselCode FROM  dbo.Vessel WHERE FleetId = " + ddlFleet.SelectedValue + ") ORDER BY VesselName";
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
    //protected void rdoSelect_CheckedChanged(object sender, EventArgs e)
    //{
    //    HiddenField hfVesselCode = (HiddenField)((RadioButton)sender).Parent.FindControl("hfVesselCode");
    //    HiddenField hfCompId = (HiddenField)((RadioButton)sender).Parent.FindControl("hfCompId");
    //    HiddenField hfjobId = (HiddenField)((RadioButton)sender).Parent.FindControl("hfjobId");
    //    Label woStatus = (Label)((RadioButton)sender).Parent.FindControl("woStatus");
    //    string SelectedDesc = hfVesselCode.Value + "," + hfCompId.Value + "," + hfjobId.Value + "," + woStatus.Text ;
    //    Session.Add("Selectedjob", SelectedDesc);
    //    //if (woStatus.Text == "" || woStatus.Text == "Issued")
    //    //{
    //    //    btnNewPlan.Enabled = true;
    //    //}
    //    //else
    //    //{
    //    //    btnNewPlan.Enabled = false;
    //    //}
       
    //}
    protected void OverDue_Changed(object sender, EventArgs e)
    {
        if (chkOverdue.Checked)
        {
            txtFromDt.Text = "";
            txtToDt.Text = "";
            txtFromDt.Enabled = false;
            txtToDt.Enabled = false;
        }
        else
        {
            txtFromDt.Enabled = true;
            txtToDt.Enabled = true;
        }
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        Session.Add("Selectedjob", txtSelect.Text.Trim());
    }
    #endregion --------------------------------------------------
}
