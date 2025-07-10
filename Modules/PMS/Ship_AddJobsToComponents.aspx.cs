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

public partial class Ship_AddJobsToComponents : System.Web.UI.Page
{
    public int componentId
    {
        set { ViewState["CompId"] = value; }
        get { return Common.CastAsInt32(ViewState["CompId"]); }
    }
    public string VesselCode
    {
        set { ViewState["VC"] = value; }
        get { return ViewState["VC"].ToString(); }
    }
    public string componentcode
    {
        set { ViewState["CompCode"] = value; }
        get { return ViewState["CompCode"].ToString(); }
    }
    public int DescJobId
    {
        set { ViewState["DesJID"] = value; }
        get { return Common.CastAsInt32(ViewState["DesJID"]); }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        
        #region --------- USER RIGHTS MANAGEMENT -----------
        //try
        //{
        //    AuthenticationManager auth = new AuthenticationManager(206, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
        //    if (!(auth.IsView))
        //    {
        //        Response.Redirect("~/NoPermission.aspx", false);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Response.Redirect("~/NoPermission.aspx?Message=" + ex.Message);
        //}
        #endregion ----------------------------------------

        if (!Page.IsPostBack)
        {
            if (Request.QueryString["CompCode"] != null && Request.QueryString["VC"] != null)
            {
                GetComponent();
                VesselCode = Request.QueryString["VC"].ToString();
            }
            BindComponentJobs();
        }

    }
    #region ----------- USER DEFINED FUNCTIONS --------------------

    private void BindComponentJobs()
    {
        string strSelectComponentJobs = "";
        DataTable dtComponentJobs = new DataTable();
        strSelectComponentJobs = "SELECT JM.JobId,JM.JobCode,CJM.CompJobId,CJM.DescrSh AS JobName,CJM.DescrM,CJM.AssignTo,CJM.IntervalId,CM.CriticalEquip AS IsCritical,CASE CJM.IntervalId WHEN 1 THEN CASE CJM.IsFixed WHEN 0 THEN 1 WHEN 1 THEN 0 ELSE -1 END ELSE -1 END AS  IsFixed,cast(0 as bit) as ClassJob FROM JobMaster JM " +
                                 "INNER JOIN ComponentsJobMapping CJM ON JM.JobId = CJM.JobId " +
                                 "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId " +
                                 "WHERE CM.ComponentCode = '" + componentcode.Trim() + "' " +
                                 "AND CJM.CompJobId NOT IN (SELECT DISTINCT CompJobId FROM VSL_VesselComponentJobMaster VM INNER JOIN ComponentMaster CM ON CM.ComponentId = VM.ComponentId WHERE VM.VesselCode = '" + VesselCode.Trim() + "'  AND CM.ComponentCode = '" + componentcode.Trim() + "' ) ORDER BY JobCode";


        dtComponentJobs = Common.Execute_Procedures_Select_ByQuery(strSelectComponentJobs);
        if (dtComponentJobs.Rows.Count > 0)
        {
            rptJobs.DataSource = dtComponentJobs;
            rptJobs.DataBind();
        }
        else
        {
            rptJobs.DataSource = null;
            rptJobs.DataBind();
        }
    }
    public DataTable BindRanks()
    {
        DataTable dtRanks = new DataTable();
        string strSQL = "SELECT RankId,RankCode FROM Rank ORDER BY RankCode";
        dtRanks = Common.Execute_Procedures_Select_ByQuery(strSQL);
        return dtRanks;
    }
    public DataTable BindInterval()
    {
        DataTable dtInterval = new DataTable();
        string strSQL = "SELECT IntervalId,IntervalName FROM JobIntervalMaster";
        dtInterval = Common.Execute_Procedures_Select_ByQuery(strSQL);
        return dtInterval;
    }
    public DataTable BindORInterval()
    {
        DataTable dtORInterval = new DataTable();
        string strSQL = "SELECT 0 AS IntervalId, '' AS IntervalName UNION SELECT IntervalId,IntervalName FROM JobIntervalMaster";
        dtORInterval = Common.Execute_Procedures_Select_ByQuery(strSQL);
        dtORInterval.Rows.RemoveAt(1);
        dtORInterval.AcceptChanges();
        return dtORInterval;
    }
    private Boolean IsValidJobs()
    {
        if (rptJobs.Items.Count <= 0)
        {
            MessageBox1.ShowMessage("No Job exist for the selected Component.", true);
            rptJobs.Focus();
            return false;
        }
        else
        {
            bool IsChecked = false;
            foreach (RepeaterItem rptJobItem in rptJobs.Items)
            {
                CheckBox chkSelect = (CheckBox)rptJobItem.FindControl("chkSelect");
                if (chkSelect.Checked)
                {
                    IsChecked = true;
                    break;
                }
            }
            if (!IsChecked)
            {
                MessageBox1.ShowMessage("Please Select a job.", true);
                return false;
            }
            foreach (RepeaterItem rptJobItem in rptJobs.Items)
            {
                CheckBox chkSelect = (CheckBox)rptJobItem.FindControl("chkSelect");
                TextBox txtInterval = (TextBox)rptJobItem.FindControl("txtInterval");
                DropDownList ddlInterval = (DropDownList)rptJobItem.FindControl("ddlInterval");
                TextBox txtStartDate = (TextBox)rptJobItem.FindControl("txtStartDate");
                DropDownList ddlORInt = (DropDownList)rptJobItem.FindControl("ddlORInt");
                TextBox txtORInt = (TextBox)rptJobItem.FindControl("txtORInt");
                if (chkSelect.Checked)
                {
                    if (txtInterval.Text.Trim() == "")
                    {
                        MessageBox1.ShowMessage("Please enter Interval.", true);
                        txtInterval.Focus();
                        return false;
                    }
                    if (Common.CastAsInt32(txtInterval.Text.Trim()) <= 0)
                    {
                        MessageBox1.ShowMessage("Please enter valid Interval.", true);
                        txtInterval.Focus();
                        return false;
                    }
                    if (ddlInterval.SelectedIndex == 0)
                    {
                        if (txtORInt.Text != "")
                        {
                            if (Common.CastAsInt32(txtORInt.Text.Trim()) <= 0)
                            {
                                MessageBox1.ShowMessage("Please enter valid Interval.", true);
                                txtORInt.Focus();
                                return false;
                            }
                            if (ddlORInt.SelectedIndex == 0)
                            {
                                MessageBox1.ShowMessage("Please select Interval.", true);
                                ddlORInt.Focus();
                                return false;
                            }
                        }
                        if (ddlORInt.SelectedIndex != 0)
                        {
                            if (txtORInt.Text == "")
                            {
                                MessageBox1.ShowMessage("Please enter Interval.", true);
                                txtORInt.Focus();
                                return false;
                            }
                        }
                    }
                    if (ddlInterval.SelectedIndex != 0)
                    {
                        //if (txtStartDate.Text == "")
                        //{
                        //    MessageBox1.ShowMessage("Please enter last done date.", true);
                        //    //txtStartDate.Focus();
                        //    return false;
                        //}
                        if (txtStartDate.Text != "" && DateTime.Parse(txtStartDate.Text) > DateTime.Now.Date)
                        {
                            MessageBox1.ShowMessage("Last done date can not be more than today.", true);
                            //txtStartDate.Focus();
                            return false;
                        }

                    }
                }

            }
        }
        return true;
    }
    private void GetComponent()
    {
        DataTable dtCompId = new DataTable();
        string strSQL = "SELECT ComponentId,ComponentCode,ComponentName FROM ComponentMaster WHERE ComponentCode = '" + Request.QueryString["CompCode"].ToString().Trim() + "' ";
        dtCompId = Common.Execute_Procedures_Select_ByQuery(strSQL);
        componentId = Common.CastAsInt32(dtCompId.Rows[0]["ComponentId"].ToString());
        componentcode = dtCompId.Rows[0]["ComponentCode"].ToString();
        lblComponent.Text = dtCompId.Rows[0]["ComponentCode"].ToString() + " : " + dtCompId.Rows[0]["ComponentName"].ToString();

    }
    #endregion ----------------------------------------------------

    #region ----------- EVENTS ------------------------------------
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvScroll');", true);
    }
    protected void ddlInterval_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlInterval = (DropDownList)((DropDownList)sender).Parent.FindControl("ddlInterval");
        TextBox txtStartDate = (TextBox)((DropDownList)sender).Parent.FindControl("txtStartDate");
        DropDownList ddlORInt = (DropDownList)((DropDownList)sender).Parent.FindControl("ddlORInt");
        TextBox txtORInt = (TextBox)((DropDownList)sender).Parent.FindControl("txtORInt");
        Label lblOR = (Label)((DropDownList)sender).Parent.FindControl("lblOR");
        if (ddlInterval.SelectedIndex == 0)
        {
            txtStartDate.Visible = false;
            ddlORInt.Visible = true;
            txtORInt.Visible = true;
            lblOR.Visible = true;
        }
        else
        {
            txtStartDate.Visible = true;
            ddlORInt.Visible = false;
            txtORInt.Visible = false;
            lblOR.Visible = false;
            ddlORInt.SelectedIndex = 0;
            txtORInt.Text = "";
        }
    }
    protected void btnAddJobs_Click(object sender, EventArgs e)
    {
        if (!IsValidJobs())
        {
            return;
        }
        int AssignTo, CompJobId, IntervalType, IntervalType_H, IsFixed;
        string Interval, Interval_H, StartDate, Description;
        foreach (RepeaterItem rptJobItem in rptJobs.Items)
        {
            CheckBox chkSelect = (CheckBox)rptJobItem.FindControl("chkSelect");
            CheckBox chkClass = (CheckBox)rptJobItem.FindControl("chkClass");
            HiddenField hfJobId = (HiddenField)rptJobItem.FindControl("hfJobId");
            DropDownList ddlAssignTO = (DropDownList)rptJobItem.FindControl("ddlAssignTO");
            DropDownList ddlInterval = (DropDownList)rptJobItem.FindControl("ddlInterval");
            DropDownList ddlORInt = (DropDownList)rptJobItem.FindControl("ddlORInt");
            TextBox txtInterval = (TextBox)rptJobItem.FindControl("txtInterval");
            TextBox txtORInt = (TextBox)rptJobItem.FindControl("txtORInt");
            TextBox txtStartDate = (TextBox)rptJobItem.FindControl("txtStartDate");
            TextBox txtGuidelines = (TextBox)rptJobItem.FindControl("txtGuidelines");
            HiddenField hfDescr = (HiddenField)rptJobItem.FindControl("hfDescr");
            TextBox txtjobcost = (TextBox)rptJobItem.FindControl("txtjobcost");
            if (chkSelect.Checked)
            {
                CompJobId = Common.CastAsInt32(hfJobId.Value);
                AssignTo = Common.CastAsInt32(ddlAssignTO.SelectedValue);
                IntervalType = Common.CastAsInt32(ddlInterval.SelectedValue);
                Interval = txtInterval.Text.Trim();
                if (ddlInterval.SelectedIndex == 0)
                {
                    IntervalType_H = Common.CastAsInt32(ddlORInt.SelectedValue.Trim());
                    Interval_H = txtORInt.Text.Trim();
                }
                else
                {
                    IntervalType_H = 0;
                    Interval_H = "0";
                }
                IsFixed = -1;
                if (ddlInterval.SelectedIndex == 0)
                {
                    StartDate = "";
                }
                else
                {
                    StartDate = txtStartDate.Text;
                }
                Description = hfDescr.Value;
                Common.Set_Procedures("sp_Insert_Ship_Component_Jobs");
                Common.Set_ParameterLength(14);
                Common.Set_Parameters(
                    new MyParameter("@VesselCode", VesselCode.Trim()),
                    new MyParameter("@ComponentCode", componentcode.Trim()),
                    new MyParameter("@CompJOBID", CompJobId),
                    new MyParameter("@AssignTo", AssignTo),
                    new MyParameter("@IntervalId", IntervalType),
                    new MyParameter("@Interval", Interval),
                    new MyParameter("@IsFixed", IsFixed),
                    new MyParameter("@StartDate", (StartDate.Trim()=="")?DBNull.Value:(object)StartDate),
                    new MyParameter("@DescrM", Description),
                    new MyParameter("@IntervalId_H", IntervalType_H),
                    new MyParameter("@Interval_H", Interval_H),
                    new MyParameter("@Guidelines", txtGuidelines.Text.Trim()),
                    new MyParameter("@JobCost", Common.CastAsDecimal(txtjobcost.Text.Trim())),
                    new MyParameter("@ClassJob", (chkClass.Checked)?1:0)
                    );
                DataSet dsComponentJobs = new DataSet();
                dsComponentJobs.Clear();
                Boolean result;
                result = Common.Execute_Procedures_IUD(dsComponentJobs);
                if (result)
                {
                    MessageBox1.ShowMessage("Component Jobs Saved Successfully.", false);
                }
                else
                {
                    MessageBox1.ShowMessage("Unable to Save Component Jobs.Error :" + Common.getLastError(), true);
                }
            }
        }
        BindComponentJobs();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "refreshonaddjobs();", true);
    }
    protected void imgEditDescr_Click(object sender, ImageClickEventArgs e)
    {
        DescJobId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "opendescrwin('V','" + VesselCode + "','" + DescJobId + "');", true);
    }
    protected void btnRefreshGrid_Click(object sender, EventArgs e)
    {
        if (Session["DescrM"] != null)
        {
            if (Session["DescrM"].ToString() != "")
            {
                foreach (RepeaterItem rpt in rptJobs.Items)
                {
                    HiddenField hfJobId = (HiddenField)rpt.FindControl("hfJobId");
                    if (Common.CastAsInt32(hfJobId.Value) == DescJobId)
                    {
                        HiddenField hfDescr = (HiddenField)rpt.FindControl("hfDescr");
                        hfDescr.Value = Session["DescrM"].ToString();
                    }
                }
            }
        }
        Session["DescrM"] = null;
    }
    #endregion
}
