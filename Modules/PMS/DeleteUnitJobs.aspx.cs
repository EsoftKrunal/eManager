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

public partial class DeleteUnitJobs : System.Web.UI.Page
{
    public static int componentId;
    public static string VesselCode;
    public static string componentcode;
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        lblMessage.Text = "";
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
        if (componentcode.Trim().Length == 4)
        {
            strSelectComponentJobs = "SELECT JM.JobId,JM.JobCode,JM.Descr,JM.JobName,CJM.AssignTo FROM JobMaster JM " +
                                     "INNER JOIN ComponentsJobMapping CJM ON JM.JobId = CJM.JobId " +
                                     "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId " +
                                     "WHERE CM.ComponentCode = '" + componentcode.Trim() + "' " +
                                     "AND JM.JobId IN (SELECT JobId FROM VesselComponentJobMaster VM INNER JOIN ComponentMaster M ON M.ComponentId = VM.VesselComponentId  WHERE VM.VesselCode = '" + VesselCode.Trim() + "' AND M.ComponentCode ='" + componentcode.Trim() + "') ORDER BY JobName";
        }
        if (componentcode.Trim().Length == 6)
        {
            strSelectComponentJobs = "SELECT JM.JobId,JM.JobCode,JM.Descr,JM.JobName,CJM.AssignTo FROM JobMaster JM " +
                                     "INNER JOIN ComponentsJobMapping CJM ON JM.JobId = CJM.JobId " +
                                     "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId " +
                                     "WHERE CM.ComponentCode = '" + componentcode.Trim() + "' " +
                                     "AND JM.JobId IN (SELECT DISTINCT JobId FROM VesselComponentJobMaster VM INNER JOIN VesselComponentMaster VCM ON VCM.VesselComponentId = VM.VesselComponentId AND VCM.VesselCode = VM.VesselCode INNER JOIN ComponentMaster CM ON CM.ComponentId = VCM.ComponentId WHERE VM.VesselCode = '" + VesselCode.Trim() + "' AND CM.ComponentCode = '" + componentcode.Trim() + "') ORDER BY JobName";
        }

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
    private Boolean IsValidJobs()
    {
        if (rptJobs.Items.Count <= 0)
        {
            lblMessage.Text = "No Job exist for the selected Component.";
            rptJobs.Focus();
            return false;
        }
        else
        {
            int count = 0;
            foreach (RepeaterItem rptJobItem in rptJobs.Items)
            {
                CheckBox chkSelect = (CheckBox)rptJobItem.FindControl("chkSelect");
                if (chkSelect.Checked)
                {
                    count = count + 1;
                }
            }
            if (count == 0)
            {
                lblMessage.Text = "Please Select a job.";
                return false;
            }
            //foreach (RepeaterItem rptJobItem in rptJobs.Items)
            //{
            //    CheckBox chkSelect = (CheckBox)rptJobItem.FindControl("chkSelect");
            //    TextBox txtInterval = (TextBox)rptJobItem.FindControl("txtInterval");
            //    TextBox txtDescr = (TextBox)rptJobItem.FindControl("txtDescr");
            //    if (chkSelect.Checked)
            //    {
            //        if (txtInterval.Text.Trim() == "")
            //        {
            //            lblMessage.Text = "Please enter Interval.";
            //            txtInterval.Focus();
            //            return false;
            //        }
            //        if (Common.CastAsInt32(txtInterval.Text.Trim()) < 0)
            //        {
            //            lblMessage.Text = "Please enter valid Interval.";
            //            txtInterval.Focus();
            //            return false;
            //        }
            //        if (txtDescr.Text.Trim().Length > 255)
            //        {
            //            lblMessage.Text = "Descr. should not be greater than 255 characters.";
            //            txtDescr.Focus();
            //            return false;
            //        }
            //    }
            //}
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

    #region ----------- USER DEFINED FUNCTIONS --------------------

    protected void btnDeleteJobs_Click(object sender, EventArgs e)
    {
        if (!IsValidJobs())
        {
            return;
        }
        int JobId;
        foreach (RepeaterItem rptItem in rptJobs.Items)
        {
            CheckBox chkSelect = (CheckBox)rptItem.FindControl("chkSelect");
            HiddenField hfJobId = (HiddenField)rptItem.FindControl("hfJobId");            
            if (chkSelect.Checked)
            {
                JobId = Common.CastAsInt32(hfJobId.Value);
                string strSQL = "DELETE FROM VesselComponentJobMaster FROM VesselComponentJobMaster WHERE VesselCode='" + VesselCode.Trim() + "' AND JobId = " + JobId + " AND VesselComponentId in " +
                                "(SELECT VesselComponentId FROM VesselComponentMaster WHERE VesselCode='" + VesselCode.Trim() + "' AND ComponentId = " + componentId + ")";
                Common.Execute_Procedures_Select_ByQuery(strSQL);
            }
        }
        lblMessage.Text = "Jobs Deleted Successfully.";
        BindComponentJobs();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "refresh()", true);
    }

    #endregion ----------------------------------------------------
}
