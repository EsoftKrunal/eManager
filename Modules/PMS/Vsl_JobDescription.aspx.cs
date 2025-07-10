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

public partial class Vsl_JobDescription : System.Web.UI.Page
{
    public string VesselCode
    {
        set { ViewState["VC"] = value; }
        get { return ViewState["VC"].ToString(); }
    }
    public int ComponentId
    {
        set { ViewState["CI"] = value; }
        get { return Common.CastAsInt32(ViewState["CI"]); }
    }
    public int JobId
    {
        set { ViewState["JI"] = value; }
        get { return Common.CastAsInt32(ViewState["JI"]); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------

        if (!Page.IsPostBack)
        {
            if (Request.QueryString["VC"] != null && Request.QueryString["CID"] != null && Request.QueryString["JID"] != null)
            {
                VesselCode = Request.QueryString["VC"].ToString();
                ComponentId = Common.CastAsInt32(Request.QueryString["CID"].ToString());
                JobId = Common.CastAsInt32(Request.QueryString["JID"].ToString());
                ShowCompJobDetails();
                BindJobLongDescription();
            }
        }
    }
    private void ShowCompJobDetails()
    {
        string strCompJobDetails = "SELECT (SELECT ShipName FROM Settings WHERE ShipCode = '" + VesselCode + "') AS VesselName, CM.ComponentCode,CM.ComponentName ,JM.JobCode,CJM.DescrSh AS JobName,JIM.IntervalName,VCJM.Interval FROM VSL_VesselComponentJobMaster VCJM " +
                                    "INNER JOIN ComponentMaster CM ON CM.ComponentId = VCJM.ComponentId " +
                                    "INNER JOIN ComponentsJobMapping CJM  ON VCJM.CompJobId = CJM.CompJobId " +
                                    "INNER JOIN JobMaster JM ON JM.JobId = CJM.JobId " +
                                    "INNER JOIN JobIntervalMaster JIM ON VCJM.IntervalId = JIM.IntervalId " +
                                    "WHERE VCJM.VesselCode = '" + VesselCode + "' AND VCJM.ComponentId =" + ComponentId + " AND VCJM.CompJobId=" + JobId + " ";
        DataTable dtCompJobDetails = Common.Execute_Procedures_Select_ByQuery(strCompJobDetails);
        
        lblDescVessel.Text = dtCompJobDetails.Rows[0]["VesselName"].ToString();
        lblDescComponent.Text = dtCompJobDetails.Rows[0]["ComponentCode"].ToString() + " - " + dtCompJobDetails.Rows[0]["ComponentName"].ToString();
        lblDescJob.Text = dtCompJobDetails.Rows[0]["JobCode"].ToString() + " - " + dtCompJobDetails.Rows[0]["JobName"].ToString();
        lblDescInterval.Text = dtCompJobDetails.Rows[0]["Interval"].ToString() + " - " + dtCompJobDetails.Rows[0]["IntervalName"].ToString();
    }

    private void BindJobLongDescription()
    {
        string strSQL = "SELECT CASE ISNULL(VJM.DescrM,'') WHEN '' THEN CJM.DescrM ELSE ISNULL(VJM.DescrM,'') END AS DescrM FROM VSL_VesselComponentJobMaster VJM " +
                        "INNER Join  ComponentsJobMapping CJM ON CJM.ComponentId = VJM.ComponentId AND CJM.CompJobId = VJM.CompJobId " +
                        "WHERE VJM.VesselCode = '" + VesselCode + "' AND VJM.ComponentId=" + ComponentId + " AND VJM.CompJobId = " + JobId;
        DataTable dtLongDescrJob = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dtLongDescrJob.Rows.Count > 0)
        {
            txtJobLongDescr.Text = dtLongDescrJob.Rows[0]["DescrM"].ToString();
        }
    }
}
