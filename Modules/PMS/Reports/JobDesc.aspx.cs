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

public partial class JobDesc : System.Web.UI.Page
{
    public int CompJobId
    {
        set { ViewState["CompJobId"] = value; }
        get { return Common.CastAsInt32(ViewState["CompJobId"]); }
    }
   
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------

        if (Page.Request.QueryString["CompJobId"] != null)
        {
            CompJobId = Convert.ToInt32(Page.Request.QueryString["CompJobId"]);
            ShowJobDescription();
        }
        
    }
    public void ShowJobDescription()
    {
        string sql = " SELECT  (LTRIM(RTRIM(CM.ComponentCode))+' : '+ CM.ComponentName) As CompName,CM.CriticalEquip AS IsCritical,DM.DeptName,RM.RankCode,JM.JobCode " +
                    " ,CJM.DescrSh AS JobName,JIM.IntervalName,CJM.DescrM AS LongDescr from ComponentsJobMapping CJM  "+
                    " INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId "+
                    " INNER JOIN JobMaster JM ON JM.JobId = CJM.JobId  "+
                    " INNER JOIN Rank RM ON RM.RankId = CJM.AssignTo  "+
                    " INNER JOIN DeptMaster DM ON DM.DeptId = CJM.DeptId  "+
                    " INNER JOIN JobIntervalMaster JIM ON CJM.IntervalId = JIM.IntervalId  " +
                    " WHERE CJM.CompJobId = "+ CompJobId +"";
        DataTable dtJobDesc = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dtJobDesc != null)
        {
            if (dtJobDesc.Rows.Count > 0)
            {
                DataRow Dr = dtJobDesc.Rows[0];

                lblCompName.Text = Dr["CompName"].ToString();

                lblJobType.Text = Dr["JobCode"].ToString();
                lblShortDesc.Text = Dr["JobName"].ToString();
                lblDepartment.Text = Dr["DeptName"].ToString();
                lblAssignedFor.Text = Dr["RankCode"].ToString();
                lblIntervalType.Text = Dr["IntervalName"].ToString();                
                lblLongDesc.Text = Dr["LongDescr"].ToString();
            }
        }

        sql = "SELECT RankCode FROM Rank WHERE RANKID IN (select rankid from [dbo].[ComponentJobMapping_OtherRanks] where compjobid=" + CompJobId + ") ORDER BY RankLevel ";
        string lst = "";
        DataTable  dtRanks = Common.Execute_Procedures_Select_ByQuery(sql);
        foreach (DataRow dr in dtRanks.Rows)
        {
            lst +="," + dr["RankCode"].ToString();
        }
        if (lst.StartsWith(","))
            lst = lst.Substring(1);
        lblAssignedForOther.Text = lst;
    }
    
}
