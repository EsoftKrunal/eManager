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

public partial class VSL_JobDesc : System.Web.UI.Page
{
    public int CompJobId
    {
        set { ViewState["CompJobId"] = value; }
        get { return Common.CastAsInt32(ViewState["CompJobId"]); }
    }
    public string VesselCode
    {
        set { ViewState["_VesselCode"] = value; }
        get { return Convert.ToString(ViewState["_VesselCode"]); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------

        if (Page.Request.QueryString["CompJobId"] != null)
        {
            CompJobId = Convert.ToInt32(Page.Request.QueryString["CompJobId"]);
            VesselCode = Convert.ToString(Page.Request.QueryString["VSL"]);
            ShowJobDescription();
            BindRepeater();
        }
        
    }
    public void BindRepeater()
    {
        string sql = "select ROW_NUMBER() OVER(ORDER BY AttachmentId) AS srno,* from JobExecAttachmentsMaster where compjobid=" + CompJobId;
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);

        rptFiles.DataSource = Dt;
        rptFiles.DataBind();
    }
    public void ShowJobDescription()
    {
        string sql = " SELECT  (LTRIM(RTRIM(CM.ComponentCode))+' : '+ CM.ComponentName) As CompName,CM.CriticalEquip AS IsCritical,DM.DeptName,RM.RankCode,JM.JobCode " +
                    " ,CJM.DescrSh AS JobName,JIM.IntervalName,VCJM.DescrM AS LongDescr,vcjm.JobCost from ComponentsJobMapping CJM  " +
                    " INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId "+
                    " INNER JOIN JobMaster JM ON JM.JobId = CJM.JobId  "+
                    " INNER JOIN Rank RM ON RM.RankId = CJM.AssignTo  "+
                    " INNER JOIN DeptMaster DM ON DM.DeptId = CJM.DeptId  "+
                    " INNER JOIN JobIntervalMaster JIM ON CJM.IntervalId = JIM.IntervalId  " +
                    " INNER JOIN VSL_VesselComponentJobMaster VCJM ON VCJM.VesselCode= '"+ VesselCode + "'  and VCJM.CompJobID=CJM.CompJobID" +
                    " WHERE CJM.CompJobId = " + CompJobId +"";
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
                lblLongDesc.Text = Dr["LongDescr"].ToString().Replace("\n","<br/>");
                lblJobCost.Text = Dr["JobCost"].ToString() + " (US$)";
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
        //---------
        sql = "SELECT * FROM [dbo].vw_JobSpareRequirement WHERE Vesselcode='" + VesselCode + "' and ForCompJobId=" + CompJobId +" order by sparename";
        DataTable dtSpares = Common.Execute_Procedures_Select_ByQuery(sql);
        rptSpares.DataSource = dtSpares;
        rptSpares.DataBind();
    }

}
