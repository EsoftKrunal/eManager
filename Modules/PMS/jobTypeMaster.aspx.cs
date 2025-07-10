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


public partial class jobTypeMaster : System.Web.UI.Page
{
    AuthenticationManager Auth;
    public string strJobId
    {
        set { ViewState["JobId"] = value; }
        get { return ViewState["JobId"].ToString(); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------

      //***********Code to check page acessing Permission

        Auth = new AuthenticationManager(1046, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
        if (!(Auth.IsView))
        {
            Response.Redirect("~/AuthorityError.aspx");
        }
        else
        {
            btnAdd.Visible = Auth.IsAdd;
            btnSave.Visible = Auth.IsAdd;
        }

      //*******************

        if (!Page.IsPostBack)
        {
            Session["CurrentModule"] = 3;
            strJobId = "";
            BindJobTypes();
        }

    }

    #region ------------------ USER DEFINED FUNCTIONS ----------------------------

    private void BindJobTypes()
    {
        DataTable dtJobs;
        String strJobs;
        strJobs = "SELECT JobId,JobCode,JobName FROM JobMaster ORDER BY JobCode";
        dtJobs = Common.Execute_Procedures_Select_ByQuery(strJobs);
        rptItems.DataSource = dtJobs;
        rptItems.DataBind();
    }
    private Boolean IsValidated()
    {
        if (txtJobTypeCode.Text == "")
        {
            MessageBox1.ShowMessage("Please enter Job Code.", true);
            txtJobTypeCode.Focus();
            return false;
        }
        if (txtJobTypeName.Text == "")
        {
            MessageBox1.ShowMessage("Please enter Job Name.", true);
            txtJobTypeName.Focus();
            return false;
        }
        return true;
    }
    #endregion

    #region ------------------- EVENTS -------------------------------------------
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvJM');", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!IsValidated())
        {
            return;
        }
        DataTable dtJobCode = new DataTable();
        string strJobCode = "SELECT JobCode FROM JobMaster WHERE JobCode ='" + txtJobTypeCode.Text.Trim() + "' AND JobId <>'" + strJobId + "'";
        dtJobCode = Common.Execute_Procedures_Select_ByQuery(strJobCode);
        if (dtJobCode.Rows.Count > 0)
        {
            MessageBox1.ShowMessage("Job Code already exists.", true);
            txtJobTypeCode.Focus();
            return;
        }
        DataTable dtJobName = new DataTable();
        string strJobName = "SELECT JobName FROM JobMaster WHERE JobName ='" + txtJobTypeName.Text.Trim() + "' AND JobId <>'" + strJobId + "'";
        dtJobName = Common.Execute_Procedures_Select_ByQuery(strJobName);
        if (dtJobName.Rows.Count > 0)
        {
            MessageBox1.ShowMessage("Job Name already exists.", true);
            txtJobTypeName.Focus();
            return;
        }

        Common.Set_Procedures("sp_InsertUpdateJobTypes");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(
           new MyParameter("@JobId", strJobId),
           new MyParameter("@JobCode", txtJobTypeCode.Text.Trim()),
           new MyParameter("@JobName", txtJobTypeName.Text.Trim())
           );
        DataSet dsJobs = new DataSet();
        dsJobs.Clear();
        Boolean res;
        res = Common.Execute_Procedures_IUD(dsJobs);
        if (res)
        {
            MessageBox1.ShowMessage("Job Added Successfully.", false);
            BindJobTypes();
            btnAdd_Click(sender, e);
        }
        else
        {
            MessageBox1.ShowMessage("Unable to update Job.Error :" + Common.getLastError(), true);
        }
    }
    protected void btnJobName_Click(object sender, EventArgs e)
    {
        int JobId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        String StrJobDetails = "SELECT JobId,JobCode,JobName FROM JobMaster " +
                               "WHERE JobId =" + JobId.ToString();
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(StrJobDetails);
        txtJobTypeCode.Text = dt.Rows[0]["JobCode"].ToString();
        txtJobTypeName.Text = dt.Rows[0]["JobName"].ToString();        
        strJobId = Convert.ToString(JobId);
        //btnDelete.Visible = true;
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //Common.Set_Procedures("sp_Delete_Office_Master");
        //Common.Set_ParameterLength(3);
        //Common.Set_Parameters(
        //   new MyParameter("@Param_CompId", 0),
        //   new MyParameter("@Param_JobId", strJobId),
        //   new MyParameter("@ItemType", 1)
        //   );
        //DataSet dsJobs = new DataSet();
        //dsJobs.Clear();
        //Boolean res;
        //res = Common.Execute_Procedures_IUD(dsJobs);
        //if (res)
        //{
        //    MessageBox1.ShowMessage("Job Type Deleted Successfully.", false);
        //    BindJobTypes();
        //    btnAdd_Click(sender, e);
        //}
        //else
        //{
        //    MessageBox1.ShowMessage("Unable to delete Job Type.Error :" + Common.getLastError(), true);
        //}
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        txtJobTypeCode.Text = "";
        txtJobTypeName.Text = "";        
        strJobId = "";
    }
    #endregion
}
