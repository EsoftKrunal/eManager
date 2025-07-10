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

public partial class UpdateJobDescription : System.Web.UI.Page
{
    public string JobId
    {
        set { ViewState["JId"] = value; }
        get { return ViewState["JId"].ToString(); }
    }
    public string VesselCode
    {
        set { ViewState["VesselCode"] = value; }
        get { return ViewState["VesselCode"].ToString(); }
    }
    public string Mode
    {
        set { ViewState["Mode"] = value; }
        get { return ViewState["Mode"].ToString(); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------

        if (!Page.IsPostBack)
        {
            if (Request.QueryString["JobId"] != null)
            {
                JobId = Request.QueryString["JobId"].ToString();
                VesselCode = Request.QueryString["VesselCode"].ToString();
                Mode = Request.QueryString["Mode"].ToString();
                ShowDesciption();
                BindRepeater();
            }
        }

    }
    public void BindRepeater()
    {
        string sql = "select ROW_NUMBER() OVER(ORDER BY AttachmentId) AS srno,* from JobExecAttachmentsMaster where compjobid=" + JobId;
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);

        rptFiles.DataSource = Dt;
        rptFiles.DataBind();
    }
    private void ShowDesciption()
    {
        string strSQL = "";
        if (Mode == "V")
        {
            strSQL = "SELECT DescrM FROM VSL_VesselComponentJobMaster WHERE VesselCode='" + VesselCode + "' AND CompJobId=" + JobId;
        }
        else
        {
            strSQL = "SELECT DescrM FROM VesselComponentJobMaster WHERE VesselCode='" + VesselCode + "' AND CompJobId=" + JobId;
        }
        DataTable dtDescrM = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dtDescrM.Rows.Count > 0)
        {
            txtDescr.Text = dtDescrM.Rows[0]["DescrM"].ToString();
        }

    }
    protected void btnOfMDescr_Click(object sender, EventArgs e)
    {
        txtDescr.Text = "";
        string strSQL = "SELECT DescrM FROM ComponentsJobMapping WHERE CompJobId=" + JobId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(strSQL);
        txtDescr.Text = dt.Rows[0]["DescrM"].ToString();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Session.Add("DescrM", txtDescr.Text.Trim());
        ScriptManager.RegisterStartupScript(this, this.GetType(), "close", "refresh();", true);
    }
}
