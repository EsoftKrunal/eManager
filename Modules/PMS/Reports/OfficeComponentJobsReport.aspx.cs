using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Reports_OfficeComponentJobsReport : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
         

        if (Request.QueryString["Mode"] == "Office")
        {
            string strComponentJobdetails = "SELECT * FROM Vw_ComponentJobs";
            DataTable dtComponentJobs = Common.Execute_Procedures_Select_ByQuery(strComponentJobdetails);
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("Off_ComponentJobsList.rpt"));
            rpt.SetDataSource(dtComponentJobs);
            rpt.SetParameterValue("@Header", "Office Component Job Master");
        }
        if (Request.QueryString["Mode"] == "Vessel")
        {
            string strComponentJobdetails = "SELECT AA.*,CAST(ASD.interval AS INT) AS interval,RM.RANKCODE,CASE ASD.IntervalId WHEN 1 THEN CASE ASD.IntervalId_H WHEN 0 THEN JIM.IntervalName ELSE JIM.IntervalName + '  OR  ' + (SELECT IntervalName FROM JobIntervalMaster WHERE IntervalId = ASD.IntervalId_H ) END ELSE JIM.IntervalName END AS IntervalName, " +
                                            "LastdoneDate=( CASE WHEN JIM.IntervalId=1 THEN (SELECT STARTDATE FROM VesselRunningHourMaster VRH WHERE VRH.VESSELCODE= ASD.VesselCode AND VRH.COMPONENTID=ASD.COMPONENTID) ELSE (SELECT STARTDATE FROM VesselComponentJobMaster VCJMU WHERE VCJMU.VESSELCODE=ASD.VesselCode AND VCJMU.COMPJOBID=AA.CompJobId) END ), " +
                                            "LastdoneHour=( CASE WHEN JIM.IntervalId=1 THEN (SELECT STARTUPHOUR FROM VesselRunningHourMaster VRH WHERE VRH.VESSELCODE= ASD.VesselCode AND VRH.COMPONENTID=ASD.COMPONENTID) ELSE '' END ) " +
                                            "FROM  " +
                                            "( " +
	                                            "SELECT DISTINCT dbo.ComponentMaster.ComponentCode,ComponentsJobMapping.COMPJOBID, dbo.ComponentMaster.ComponentName, dbo.JobMaster.JobCode,  " +
                                                "dbo.ComponentsJobMapping.DescrSh, dbo.DeptMaster.DeptName,dbo.ComponentMaster.CriticalEquip,dbo.ComponentMaster.CriticalType,JobIntervalMaster.IntervalId " +
	                                            "FROM   	dbo.ComponentMaster  " +
	                                            "LEFT OUTER JOIN  dbo.ComponentsJobMapping  " +
	                                            "INNER JOIN dbo.JobMaster ON dbo.ComponentsJobMapping.JobId = dbo.JobMaster.JobId  " +
	                                            "INNER JOIN dbo.DeptMaster ON dbo.ComponentsJobMapping.DeptId = dbo.DeptMaster.DeptId  " +
	                                            "INNER JOIN dbo.JobIntervalMaster ON dbo.JobIntervalMaster.IntervalId = dbo.ComponentsJobMapping.IntervalId ON dbo.ComponentMaster.ComponentId = dbo.ComponentsJobMapping.ComponentId " +
	                                            "INNER JOIN  " +
	                                            "( " +
                                                "SELECT CM.ComponentCode,COMPJOBID " +
	                                            "from VesselComponentJobMaster VCJM iNNER JOIN ComponentMaster CM ON CM.ComponentId = VCJM.ComponentId  " +
                                                "WHERE VCJM.VesselCode = '" + Request.QueryString["Vessel"].ToString() + "' " +
	                                            ")  " +
                                                "VDATA ON LEFT(VDATA.ComponentCode,LEN(ComponentMaster.ComponentCode))=ComponentMaster.ComponentCode WHERE ComponentsJobMapping.COMPJOBID = VDATA.COMPJOBID OR ComponentsJobMapping.COMPJOBID IS NULL " +
                                            ") AA  " +
                                            "LEFT JOIN VesselComponentJobMaster ASD ON ASD.COMPJOBID = AA.COMPJOBID AND ASD.VesselCode = '" + Request.QueryString["Vessel"].ToString() + "' " +
                                            "LEFT JOIN Rank RM ON RM.RANKID = ASD.AssignTo " +
                                            "LEFT JOIN JobIntervalMaster JIM ON JIM.IntervalId = ASD.IntervalId " +
                                            "ORDER BY ComponentCode ";
            DataTable dtComponentJobs = Common.Execute_Procedures_Select_ByQuery(strComponentJobdetails);
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("VSL_ComponentJobsList.rpt"));
            rpt.SetDataSource(dtComponentJobs);
            rpt.SetParameterValue("@Header", "Vessel Component Job Master");
            rpt.SetParameterValue("Text1", "Vessel : " + Page.Request.QueryString["VesselName"].ToString());
        }
        if (Request.QueryString["Mode"] == "Ship")
        {
            dvFilter.Visible = true;
            dvmargin.Visible = true;
            CrystalReportViewer1.ToolbarStyle.CssClass = "FixedExpensesToolbar1";
            string CompCodeFilter = "";
            int FilterLen = txtCompCode.Text.Trim().Length;
            if (txtCompCode.Text.Trim() != "")
                CompCodeFilter = " WHERE LEFT(AA.ComponentCode," + FilterLen + ")='" + txtCompCode.Text.Trim() + "' ";
            string strComponentJobdetails = "SELECT AA.*,CAST(ASD.interval AS INT) AS interval,RM.RANKCODE,CASE ASD.IntervalId WHEN 1 THEN CASE ASD.IntervalId_H WHEN 0 THEN JIM.IntervalName ELSE JIM.IntervalName + '  OR  ' + (SELECT IntervalName FROM JobIntervalMaster WHERE IntervalId = ASD.IntervalId_H ) END ELSE JIM.IntervalName END AS IntervalName, " +
                                       "(SELECT LASTDONE FROM VSL_VesselComponentJobMaster_Updates VCJMU WHERE VCJMU.VESSELCODE=ASD.VesselCode AND VCJMU.COMPJOBID=AA.CompJobId) AS LastdoneDate, " +

                                       " (SELECT NextDueDate FROM VSL_VesselComponentJobMaster_Updates VCJMU WHERE VCJMU.VESSELCODE=ASD.VesselCode AND VCJMU.COMPJOBID=AA.CompJobId) AS NextDueDate, " +

                                       "(SELECT LASTHOUR FROM VSL_VesselComponentJobMaster_Updates VCJMU WHERE VCJMU.VESSELCODE=ASD.VesselCode AND VCJMU.COMPJOBID=AA.CompJobId) AS LastdoneHour " + 
                                       "FROM  " +
                                       "( " +
                                           "SELECT DISTINCT dbo.ComponentMaster.ComponentCode,ComponentsJobMapping.COMPJOBID, dbo.ComponentMaster.ComponentName, dbo.JobMaster.JobCode,  " +
                                           "dbo.ComponentsJobMapping.DescrSh, dbo.DeptMaster.DeptName,dbo.ComponentMaster.CriticalEquip,dbo.ComponentMaster.CriticalType " +
                                           "FROM dbo.ComponentMaster  " +
                                           "LEFT OUTER JOIN  dbo.ComponentsJobMapping  " +
                                           "INNER JOIN dbo.JobMaster ON dbo.ComponentsJobMapping.JobId = dbo.JobMaster.JobId  " +
                                           "INNER JOIN dbo.DeptMaster ON dbo.ComponentsJobMapping.DeptId = dbo.DeptMaster.DeptId  " +
                                           "INNER JOIN dbo.JobIntervalMaster ON dbo.JobIntervalMaster.IntervalId = dbo.ComponentsJobMapping.IntervalId ON dbo.ComponentMaster.ComponentId = dbo.ComponentsJobMapping.ComponentId " +
                                           "INNER JOIN  " +
                                           "( " +

                                               "SELECT CM.ComponentCode,COMPJOBID " +
                                               "from VSL_VesselComponentJobMaster VCJM " +
                                               "INNER JOIN ComponentMaster CM ON CM.ComponentId = VCJM.ComponentId AND VCJM.Status='A' " +
                                               "INNER JOIN dbo.VSL_ComponentMasterForVessel VCMV ON VCMV.VESSELCODE=VCJM.VESSELCODE AND VCMV.ComponentId = VCJM.ComponentId AND VCMV.Status='A' " +
                                               "WHERE VCJM.VesselCode = '" + Request.QueryString["Vessel"].ToString() + "' " +

                                               //"SELECT CM.ComponentCode,COMPJOBID " +
                                               //"from VSL_VesselComponentJobMaster VCJM iNNER JOIN ComponentMaster CM ON CM.ComponentId = VCJM.ComponentId  " +
                                               //"WHERE VCJM.VesselCode = '" + Request.QueryString["Vessel"].ToString() + "' And Status='A' " +

                                           ")  " +
                                           "VDATA ON LEFT(VDATA.ComponentCode,LEN(ComponentMaster.ComponentCode))=ComponentMaster.ComponentCode WHERE ComponentsJobMapping.COMPJOBID = VDATA.COMPJOBID OR ComponentsJobMapping.COMPJOBID IS NULL " +
                                       ") AA  " +
                                       "LEFT JOIN VSL_VesselComponentJobMaster ASD ON ASD.COMPJOBID = AA.COMPJOBID AND ASD.VesselCode = '" + Request.QueryString["Vessel"].ToString() + "' " +
                                       "LEFT JOIN Rank RM ON RM.RANKID = ASD.AssignTo " +
                                       "LEFT JOIN JobIntervalMaster JIM ON JIM.IntervalId = ASD.IntervalId " + CompCodeFilter +
                                       "ORDER BY ComponentCode ";
            DataTable dtComponentJobs = Common.Execute_Procedures_Select_ByQuery(strComponentJobdetails);
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("VSL_ComponentJobsList.rpt"));
            rpt.SetDataSource(dtComponentJobs);
            rpt.SetParameterValue("@Header", "Vessel Component Job Master");
            rpt.SetParameterValue("Text1", "Vessel : " + GetVesselNameByCode(Request.QueryString["Vessel"].ToString()));
        }

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    public string GetVesselNameByCode(string VelCode)
    {
        string sql = "select ShipName from Settings where ShipCode='" + VelCode + "'";
        DataTable dtVessName = Common.Execute_Procedures_Select_ByQuery(sql);
        return dtVessName.Rows[0][0].ToString();
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        Page_Load(sender,e);
    }
}
