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

public partial class EditUnitJobs : System.Web.UI.Page
{
    public static int componentId;
    public static string VesselCode;
    public static string componentcode;
    public static string JobIds;
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

        //lblMessage.Text = "";
        #region --------- USER RIGHTS MANAGEMENT -----------
        try
        {
            AuthenticationManager auth = new AuthenticationManager(206, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
            if (!(auth.IsView))
            {
                Response.Redirect("~/NoPermission.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/NoPermission.aspx?Message=" + ex.Message);
        }
        #endregion ----------------------------------------

        if (!Page.IsPostBack)
        {
            if (Request.QueryString["CompCode"] != null && Request.QueryString["VC"] != null && Request.QueryString["JobIds"] != null)
            {
                GetComponent();
                VesselCode = Request.QueryString["VC"].ToString();
                JobIds = Request.QueryString["JobIds"].ToString();
            }
            BindComponentJobs();
        }
    }
   
    #region ----------- USER DEFINED FUNCTIONS --------------------

    private void BindComponentJobs()
    {
        string strSelectComponentJobs = "";
        DataTable dtComponentJobs = new DataTable();
        strSelectComponentJobs = "SELECT CJM.CompJobId ,JM.JobCode,CJM.DescrSh AS JobName,VM.AssignTo,VM.IntervalId,VM.Interval, CM.CriticalEquip AS IsCritical,CM.CriticalType,REPLACE(CONVERT(varchar(11), VM.StartDate,106),' ','-') AS StartDate,VM.IntervalId_H,VM.Interval_H,VM.DescrM,VM.Guidelines FROM VesselComponentJobMaster VM " +
                                 "INNER JOIN ComponentsJobMapping CJM ON CJM.ComponentId = VM.ComponentId AND CJM.CompJobId = VM.CompJobId " +                     
                                 "INNER JOIN JobMaster JM ON JM.JobId = CJM.JobId " +                                
                                 "INNER JOIN ComponentMaster CM ON CM.ComponentId = VM.ComponentId " +
                                 "WHERE CM.ComponentCode = '" + componentcode.Trim() + "' AND VM.CompJobId IN (" + JobIds + ") AND VesselCode = '" + VesselCode + "' ORDER BY JobCode";

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
        foreach (RepeaterItem rptJobItem in rptJobs.Items)
        {
            DropDownList ddlInterval = (DropDownList)rptJobItem.FindControl("ddlInterval");
            TextBox txtInterval = (TextBox)rptJobItem.FindControl("txtInterval");
            TextBox txtStartDate = (TextBox)rptJobItem.FindControl("txtStartDate");
            DropDownList ddlORInt = (DropDownList)rptJobItem.FindControl("ddlORInt");
            TextBox txtORInt = (TextBox)rptJobItem.FindControl("txtORInt");
            
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
                if (txtStartDate.Text == "")
                {
                    MessageBox1.ShowMessage("Please enter last done date.", true);
                    txtStartDate.Focus();
                    return false;
                }
                if (DateTime.Parse(txtStartDate.Text) > DateTime.Now.Date)
                {
                    MessageBox1.ShowMessage("Last done date can not be more than today.", true);
                    txtStartDate.Focus();
                    return false;
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
            txtStartDate.Text = "";
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
        int AssignTo, CompJobId, IntervalType, IntervalType_H;
        string Interval, Interval_H, StartDate, strSQL,Description;
        
        foreach (RepeaterItem rptJobItem in rptJobs.Items)
        {
            HiddenField hfJobId = (HiddenField)rptJobItem.FindControl("hfJobId");
            DropDownList ddlAssignTO = (DropDownList)rptJobItem.FindControl("ddlAssignTO");
            DropDownList ddlInterval = (DropDownList)rptJobItem.FindControl("ddlInterval");
            TextBox txtInterval = (TextBox)rptJobItem.FindControl("txtInterval");
            TextBox txtStartDate = (TextBox)rptJobItem.FindControl("txtStartDate");
            DropDownList ddlORInt = (DropDownList)rptJobItem.FindControl("ddlORInt");
            TextBox txtORInt = (TextBox)rptJobItem.FindControl("txtORInt");
            HiddenField hfDescr = (HiddenField)rptJobItem.FindControl("hfDescr");
            TextBox txtGuidelines = (TextBox)rptJobItem.FindControl("txtGuidelines");

            CompJobId = Common.CastAsInt32(hfJobId.Value);
            AssignTo = Common.CastAsInt32(ddlAssignTO.SelectedValue);
            IntervalType = Common.CastAsInt32(ddlInterval.SelectedValue);
            Interval = txtInterval.Text.Trim();
            if (ddlInterval.SelectedIndex == 0)
            {
                IntervalType_H = Common.CastAsInt32(ddlORInt.SelectedValue.Trim());
                Interval_H = txtORInt.Text.Trim();
                StartDate = "";
            }
            else
            {
                IntervalType_H = 0;
                Interval_H = "0";
                StartDate = txtStartDate.Text.Split(' ').GetValue(0).ToString();
            }
            Description = hfDescr.Value;
            //if (ddlInterval.SelectedIndex == 0)
            //{
            //    strSQL = "UPDATE VesselComponentJobMaster SET AssignTo =" + AssignTo + ", IntervalId =" + IntervalType + ", Interval =" + Interval + ", StartDate = NULL , IntervalId_H = " + IntervalType_H + ", Interval_H = '" + Interval_H + "'  WHERE VesselCode='" + VesselCode.Trim() + "' AND ComponentId = " + componentId + " AND CompJobId = " + CompJobId + "; SELECT -1 ";
            //}
            //else
            //{
            //    StartDate = txtStartDate.Text.Split(' ').GetValue(0).ToString();
            //    strSQL = "UPDATE VesselComponentJobMaster SET AssignTo =" + AssignTo + ", IntervalId =" + IntervalType + ", Interval =" + Interval + ", StartDate = '" + StartDate + "', IntervalId_H = " + IntervalType_H + ", Interval_H = '" + Interval_H + "'  WHERE VesselCode='" + VesselCode.Trim() + "' AND ComponentId = " + componentId + " AND CompJobId = " + CompJobId + "; SELECT -1 ";
            //}
            try
            {
                Common.Set_Procedures("sp_Update_Vessel_ComponentJobs");
                Common.Set_ParameterLength(11);
                Common.Set_Parameters(
                    new MyParameter("@VesselCode", VesselCode.Trim()),
                    new MyParameter("@ComponentId", componentId),
                    new MyParameter("@CompjobId", CompJobId),
                    new MyParameter("@AssignTo", AssignTo),
                    new MyParameter("@IntervalId", IntervalType),
                    new MyParameter("@Interval", Interval),
                    new MyParameter("@IntervalId_H", IntervalType_H),
                    new MyParameter("@Interval_H", Interval_H),
                    new MyParameter("@StartDate", StartDate),
                    new MyParameter("@DescrM", Description),
                    new MyParameter("@Guidelines", txtGuidelines.Text.Trim())
                    );
                DataSet dsJobPlanning = new DataSet();
                dsJobPlanning.Clear();
                Boolean res;
                res = Common.Execute_Procedures_IUD(dsJobPlanning);
                if (res)
                {
                    MessageBox1.ShowMessage("Component Jobs Saved Successfully.", false);
                }
                else
                {
                    MessageBox1.ShowMessage("Unable to Save Component Jobs.", true);
                }
            }
            catch (Exception ex)
            {
                MessageBox1.ShowMessage("Unable to Save Component Jobs.Error :" + ex.Message + Common.getLastError(), true);
            }

            //DataTable dt = Common.Execute_Procedures_Select_ByQuery(strSQL);
            //if (dt.Rows.Count > 0)
            //{                
            //    MessageBox1.ShowMessage("Component Jobs Saved Successfully.", false);
            //}
            //else
            //{                
            //    MessageBox1.ShowMessage("Unable to Save Component Jobs.", true);
            //    rptJobItem.Focus();
            //}
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "refreshonaddjobs();", true);
    }
    protected void imgEditDescr_Click(object sender, ImageClickEventArgs e)
    {
        DescJobId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "opendescrwin('S','" + VesselCode + "','" + DescJobId + "');", true);
    }
    protected void btnRefreshGrid_Click(object sender, EventArgs e)
    {
        if (Session["DescrM"] != null || Session["DescrM"].ToString() != "")
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
        Session["DescrM"] = null;

    }
    #endregion

}
