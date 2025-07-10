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
    public string VesselCode
    {
        get { return ViewState["VSL"].ToString(); }
        set { ViewState["VSL"] = value; } 
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["JID"] != null)
            {
                VesselCode = Request.QueryString["VSL"];
                ShowDescr();
            }
        }
    }
    public void ShowDescr()
    {
        string strDescr = "SELECT LTRIM(RTRIM(CM.ComponentCode)) + ' - ' +CM.ComponentName AS Component,JM.JobCode + ' - ' + CJM.DescrSh AS Job ,CJM.DescrM, AttachmentForm, RiskAssessment FROM ComponentsJobMapping CJM " +
                          "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId " +
                          "INNER JOIN JobMaster JM ON JM.JobId = CJM.JobId " +
                          "WHERE CJM.CompJobId = " + Request.QueryString["JID"].ToString();
        DataTable dtDescr = Common.Execute_Procedures_Select_ByQuery(strDescr);
        DataTable dtLargeDescr = Common.Execute_Procedures_Select_ByQuery("select DescrM,Guidelines from VSL_VesselComponentJobMaster where VesselCode ='" + VesselCode + "' AND CompJobId=" + Request.QueryString["JID"].ToString());
        if (dtDescr.Rows.Count > 0)
        {
            lblComponent.Text = dtDescr.Rows[0]["Component"].ToString();
            lblJob.Text = dtDescr.Rows[0]["Job"].ToString();

            if (dtLargeDescr.Rows.Count>0)
                txtDescr.Text = dtLargeDescr.Rows[0]["DescrM"].ToString();
            else
                txtDescr.Text = "";

            string AttachmentSql = "select row_number() over(order by TableId) as Sno ,* from ( " +
                                     " select ('ShipDoc_'+Convert(varchar,CompJobID)+'_'+Convert(varchar,TableID)+'.'+DocumentType)UpFileName ,row_number() over(order by TableId) as Sno,VesselCode,TableId,CompJobId,Descr,DocumentType,Status  from VSL_VesselComponentJobMaster_Attachments " +
                                     " WHERE STATUS='A' AND VesselCode='" + VesselCode + "' and CompJobID=" + Request.QueryString["JID"].ToString() + "" +
                                     " Union " +
                                     " select ('OfficeDoc_'+Convert(varchar,CompJobID)+'_'+Convert(varchar,TableID)+'.'+DocumentType)UpFileName,row_number() over(order by TableId) as Sno,'' as VesselCode,TableId,CompJobId,Descr,DocumentType,Status  from ComponentsJobMapping_attachments where CompJobID=" + Request.QueryString["JID"].ToString() + " AND STATUS='A' " +
                                     " )tbl";
            rptFiles.DataSource = Common.Execute_Procedures_Select_ByQuery(AttachmentSql);
            rptFiles.DataBind();
        }
    }
    protected void btnPring_Click(object sener, EventArgs e)
    {
        string Script = "";
        if (Request.QueryString["JID"] != null)
        {
            Script = "window.open('Reports/PopUpJobPlanningDescr.aspx?JID=" + Request.QueryString["JID"].ToString() + "','','resizable=1, status=1,scrollbars=0,toolbar=0,menubar=0,width=580,height=345');";
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "PP", Script, true);
            //window.open('Print/PopUpJobPlanningDescr.aspx?JobDesc=1');
        }
        
    }
}
