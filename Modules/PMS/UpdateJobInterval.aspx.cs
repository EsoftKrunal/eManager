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

public partial class UpdateJobInterval : System.Web.UI.Page
{
    public int componentId
    {
        set { ViewState["CompId"] = value; }
        get { return Common.CastAsInt32(ViewState["CompId"]); }
    }
    public string VesselCode
    {
        set { ViewState["VC"] = value; }
        get { return ViewState["VC"].ToString(); }
    }
    public int componentJobId
    {
        set { ViewState["componentJobId"] = value; }
        get { return Common.CastAsInt32(ViewState["componentJobId"].ToString()); }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        AuthenticationManager Auth = new AuthenticationManager(284, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
        btnSave.Visible = Auth.IsUpdate || Auth.IsSuperUser;

        if (!Page.IsPostBack)
        {
            componentId = Common.CastAsInt32(Page.Request.QueryString["ComponentId"]);
            VesselCode = Page.Request.QueryString["VesselCode"].ToString();
            componentJobId = Common.CastAsInt32(Page.Request.QueryString["CJID"]);
            ShowDetails();
            ShowVessels();
        }
    }
    protected void btnSave_OnClick(object sender, EventArgs e)
    {
        bool anyselected = false;
        string VesselIDs = "";
        foreach (RepeaterItem item in rptVessels.Items)
        {
            CheckBox chk = (CheckBox)item.FindControl("chkvessl");            
            if (chk.Checked)
            {
                HiddenField hfdVesselID = (HiddenField)item.FindControl("hfdVesselID");
                VesselIDs = VesselIDs + "," + hfdVesselID.Value;
                anyselected = true;
            }
        }
        if (!anyselected)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "eeee", "alert('Please select vessel(s) to apply.');", true);
        }
        else
        {
            VesselIDs = VesselIDs.Substring(1);
            try
            {
                Common.Set_Procedures("sp_UpdateJobInterval_OtherShip");
                Common.Set_ParameterLength(3);
                Common.Set_Parameters(
                    new MyParameter("@CompJobid", componentJobId),
                    new MyParameter("@VesselCode", VesselCode),
                    new MyParameter("@VesselIds", VesselIDs));
                DataSet ds = new DataSet();
                ds.Clear();
                Boolean res=true;
                res = Common.Execute_Procedures_IUD(ds);
                if (res)
                {
                    lblMsg.Text = "Job details copied to selected vessels successfully.";
                    ShowVessels();
                }
                else
                {
                    lblMsg.Text = "Error while copying jobs.";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Error while copying jobs.";
            }
        }
    }

    public void ShowDetails()
    {
        string sql = "SELECT  VCJM.VesselCode,LTRIM(RTRIM(CM.ComponentCode)) As CompCode,CM.ComponentName,CM.CriticalEquip AS IsCritical,CM.CriticalType,DM.DeptName,RM.RankCode,JM.JobName AS JobCode ,CJM.CompJobId,CJM.DescrSh AS JobName,CASE VCJM.IntervalId WHEN 1 THEN CASE VCJM.IntervalId_H WHEN 0 THEN JIM.IntervalName + '-' + CAST(VCJM.Interval AS VARCHAR) ELSE JIM.IntervalName + '-' + CAST(VCJM.Interval AS Varchar)   + '  OR  ' + (SELECT IntervalName FROM JobIntervalMaster WHERE IntervalId = VCJM.IntervalId_H ) + '-' + CAST(VCJM.Interval_H AS VARCHAR) END ELSE JIM.IntervalName + '-' + CAST(VCJM.Interval AS VARCHAR) END AS Interval,JM.JobId,VCJM.ComponentId,cjm.AttachmentForm,cjm.RiskAssessment,VCJM.Guidelines " +
                    " ,dbo.GetJobAttachmentCount(cjm.CompJobId,VCJM.VesselCode,'S') as AttachmentCount,vcjm.JobCost " +
                    " from VSL_VesselComponentJobMaster VCJM " +
                     "INNER JOIN ComponentMaster CM ON CM.ComponentId = VCJM.ComponentId " +
                     "INNER JOIN ComponentsJobMapping CJM ON CJM.ComponentId = VCJM.ComponentId AND CJM.CompJobId = VCJM.CompJobId " +
                     "INNER JOIN JobMaster JM ON JM.JobId = CJM.JobId " +
                     "INNER JOIN Rank RM ON RM.RankId = VCJM.AssignTo " +
                     "INNER JOIN DeptMaster DM ON DM.DeptId = CJM.DeptId " +
                     "INNER JOIN JobIntervalMaster JIM ON VCJM.IntervalId = JIM.IntervalId " +
                     "WHERE VCJM.VesselCode = '" + VesselCode + "' AND CJM.componentId="+ componentId + " And  CJM.CompJobId=" + componentJobId+" AND VCJM.Status='A' ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            lblJobCat.Text = dr["JobCode"].ToString();
            lblJobName.Text = dr["JobName"].ToString();
            lblDepartment.Text = dr["DeptName"].ToString();
            lblRank.Text = dr["RankCode"].ToString();
            
        }
    }
    public void ShowVessels()
    {
        string sql = " SELECT V.vesselname,V.VESSELID,AB.* from DBO.VESSEL V   "+
                     "   LEFT JOIN " +
                     "   ( " +
                     "   SELECT VCJM.VESSELCODE, VCJM.COMPJOBID, CASE VCJM.IntervalId WHEN 1 THEN CASE VCJM.IntervalId_H WHEN 0 THEN JIM.IntervalName + '-' + CAST(VCJM.Interval AS VARCHAR) ELSE JIM.IntervalName + '-' + CAST(VCJM.Interval AS Varchar) + '  OR  ' + (SELECT IntervalName FROM JobIntervalMaster WHERE IntervalId = VCJM.IntervalId_H ) +'-' + CAST(VCJM.Interval_H AS VARCHAR) END ELSE JIM.IntervalName + '-' + CAST(VCJM.Interval AS VARCHAR) END AS Interval,vcjm.JobCost,rm.rankcode as AssignedRank,VCJM.STATUS " +
                     "   FROM VSL_VesselComponentJobMaster VCJM " +
                     "   INNER JOIN ComponentMaster CM ON CM.ComponentId = VCJM.ComponentId " +
                     "   INNER JOIN ComponentsJobMapping CJM ON CJM.ComponentId = VCJM.ComponentId AND CJM.CompJobId = VCJM.CompJobId " +
                     "   INNER JOIN JobMaster JM ON JM.JobId = CJM.JobId " +
                     "   INNER JOIN Rank RM ON RM.RankId = VCJM.AssignTo " +
                     "   INNER JOIN DeptMaster DM ON DM.DeptId = CJM.DeptId " +
                     "   INNER JOIN JobIntervalMaster JIM ON VCJM.IntervalId = JIM.IntervalId " +
                     "   WHERE VCJM.COMPJOBID = "+componentJobId+" " +
                     "   ) AB ON V.VESSELCODE = AB.VESSELCODE " +
                     "   WHERE V.VESSELCODE IN(select V1.VESSELCODE from dbo.vessel v1 where v1.vesselstatusid = 1 and v1.vesselcode in (select vm.vesselcode from dbo.Vessel vm where isexported = 1) and v1.vesselcode <> '" + VesselCode+ "') order by vesselname ";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        rptVessels.DataSource = dt;
        rptVessels.DataBind();
    }




}
