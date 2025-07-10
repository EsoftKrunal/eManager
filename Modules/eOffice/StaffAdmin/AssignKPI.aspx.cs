using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
public partial class emtm_StaffAdmin_AssignKPI : System.Web.UI.Page
{
    public int JRId
    {
        get { return Common.CastAsInt32(ViewState["JRId"]); }
        set { ViewState["JRId"] = value; }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            JRId = Common.CastAsInt32(Request.QueryString["JRId"]);
            lblJRName.Text = Request.QueryString["JRName"] + " ( " + Request.QueryString["Cat"] +  " ) ";
            lblPositionName.Text = Request.QueryString["PositionName"];
            lblPeapLevelName.Text = Request.QueryString["PeapLevelName"];
            BindRemainingKPI();
            BindKLinkedPI();
        }
    }
    
    public void BindRemainingKPI()
    {
//	"select isnull(KPIKEY,-1) from HR_JobResponsibility_KPI WHERE JSID=" + JRId + "

        string Query = "select entryid,SNO,KPINAME from dbo.kpi_entry WHERE ENTRYID NOT IN ( " +
    "select isnull(KPIKEY,-1) from HR_JobResponsibility_KPI k1 WHERE k1.JSID in (select j1.jsid from HR_JobResponsibility j inner join HR_JobResponsibility j1 on j.positionid=j1.positionid and j.qcat=j1.qcat WHERE j.JSID=" + JRId + ")" +
	") order by kpiname ";
        DataTable dtKPI = Common.Execute_Procedures_Select_ByQueryCMS(Query);
        rptRemainingKPI.DataSource = dtKPI;
        rptRemainingKPI.DataBind();
    }
    public void BindKLinkedPI()
    {
        string Query = "select entryid,SNO,KPINAME from dbo.kpi_entry WHERE ENTRYID IN (select isnull(KPIKEY,-1) from HR_JobResponsibility_KPI WHERE JSID=" + JRId + ") order by kpiname ";
        DataTable dtKPI = Common.Execute_Procedures_Select_ByQueryCMS(Query);
        rptLinkedKPI.DataSource = dtKPI;
        rptLinkedKPI.DataBind();
    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem ri in rptRemainingKPI.Items)
        {
            CheckBox ch = (CheckBox)ri.FindControl("chkkpiid");
            if(ch.Checked)
            {
                string Query = "INSERT INTO DBO.HR_JobResponsibility_KPI(JSID,KPIKEY) VALUES(" + JRId + "," + ch.CssClass + ")";
                Common.Execute_Procedures_Select_ByQueryCMS(Query);                
            }
        }
        BindRemainingKPI();
        BindKLinkedPI();
    }
    protected void btnRevoke_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem ri in rptLinkedKPI.Items)
        {
            CheckBox ch = (CheckBox)ri.FindControl("chkkpiid");
            if (ch.Checked)
            {
                string Query = "DELETE FROM DBO.HR_JobResponsibility_KPI WHERE JSID=" + JRId + " AND KPIKEY=" + ch.CssClass ;
                Common.Execute_Procedures_Select_ByQueryCMS(Query);
            }
        }
        BindRemainingKPI();
        BindKLinkedPI();
    }
        
}