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

public partial class PopUpJobPlanningDescr : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["JID"] != null)
            {
                ShowDescr();
            }
        }
    }

    public void ShowDescr()
    {
        string strDescr = "SELECT LTRIM(RTRIM(CM.ComponentCode)) + ' - ' +CM.ComponentName AS Component,JM.JobCode + ' - ' + CJM.DescrSh AS Job ,CJM.DescrM FROM ComponentsJobMapping CJM " +
                          "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId " +
                          "INNER JOIN JobMaster JM ON JM.JobId = CJM.JobId " +
                          "WHERE CJM.CompJobId = " + Request.QueryString["JID"].ToString();
        DataTable dtDescr = Common.Execute_Procedures_Select_ByQuery(strDescr);
        if (dtDescr.Rows.Count > 0)
        {
            lblComponent.Text = dtDescr.Rows[0]["Component"].ToString();
            lblJob.Text = dtDescr.Rows[0]["Job"].ToString();
            txtDescr.Text = dtDescr.Rows[0]["DescrM"].ToString();
        }
    }
}
