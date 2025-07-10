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

public partial class Reports_HomeReport : System.Web.UI.Page
{
    int Days = 999; 
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void ShowCritical()
    {
        Days = Common.CastAsInt32(Request.QueryString["Days"]);
        string Today = DateTime.Today.ToString("dd-MMM-yyyy");
        string NextDate = DateTime.Today.AddDays(Days).ToString("dd-MMM-yyyy");
        string LastDate = DateTime.Today.AddDays(-Days).ToString("dd-MMM-yyyy");

        //--------------------------- CRITICAL DUE -------------------------------------------

        if (Request.QueryString["Mode"] == "C_DUE")
        {
            string strCDue = "SELECT VCJM.VesselCode,(SELECT ShipName FROM Settings WHERE ShipCode = VCJM.VesselCode) AS VesselName,CM.ComponentId,CM.ComponentCode,CM.ComponentName ,CM.CriticalType,JM.JobCode,JM.JobName AS JobType,CJM.DescrSh AS JobName,VCJM.CompJobId,JIM.IntervalName ,RM.RankCode,VCJM.Interval,replace(convert(varchar(15),VCJMU.NextDueDate,106),' ','-') As NextDueDate,CASE WHEN VCJMU.NextDueDate > getdate() THEN 'DUE' ELSE 'OVER DUE' END AS DueStatus ,CASE PM.Status WHEN 1 THEN 'Issued' WHEN 2 THEN 'Completed' WHEN 3 THEN 'Postponed' WHEN 4 THEN 'Cancelled' ELSE '' END AS WorkOrderStatus,VCJMU.NextHour,REPLACE(CONVERT(VARCHAR(15),VCJMU.LastDone,106),' ','-') AS LastDone,VCJMU.LastHour,CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.LastDueDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE DATEDiff(dd,PM.DoneDate,PM.LastDueDate) END AS Difference,(ISNULL(PM.DoneHour,0) - ISNULL(PM.LastDueHours,0)) AS DiffHour,CASE PM.Status WHEN 4 THEN 0 ELSE ISNULL(PM.AssignedTo,0) END AS PlannedRank,CASE PM.Status WHEN 1 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PlanDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PlanDate,''),106),' ','-') END WHEN 2 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.DoneDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.DoneDate,''),106),' ','-') END WHEN 3 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PostPoneDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PostPoneDate,''),106),' ','-') END  ELSE '' END AS PlanDate,CASE ISNULL(PM.Status,0) WHEN 4 THEN '' WHEN 0 THEN '' ELSE ISNULL(RM.RankCode,'') END AS Rank,Row_Number() OVER(ORDER BY CM.ComponentCode) AS RowNumber, CASE CM.ClassEquip WHEN 'true' THEN 'Class Equipment - ' + VCM.ClassEquipCode ELSE '' END AS ClassEquip,CASE CM.CriticalEquip WHEN 'true' THEN 'Critical Equipment' ELSE '' END AS CriticalEquip  FROM VSL_VesselComponentJobMaster VCJM " +
                            "INNER JOIN Rank RM ON RM.RankId = VCJM.AssignTo  AND VCJM.Status = 'A' " +
                            "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId " +
                            "INNER JOIN  ( " +
                            "ComponentsJobMapping CJM " +
                            "INNER JOIN JobMaster JM  ON JM.JobId = CJM.JobId " +
                            "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId AND CriticalEquip=1 " +
                            "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCM.ComponentId = CM.ComponentId AND VCM.Status = 'A' " +
                            ") ON  VCJM.ComponentId = CJM.ComponentId AND VCJM.CompJobId = CJM.CompJobId AND VCJM.Status = 'A' AND VCM.VesselCode = VCJM.VesselCode " +
                            "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId " +
                            "LEFT JOIN VSL_PlanMaster PM ON PM.VesselCode = VCJM.VesselCode AND PM.ComponentId = VCJM.ComponentId AND PM.CompJobId = VCJM.CompJobId ";

            string WhereCDue = "WHERE ( dbo.getDatePart(VCJMU.NextDueDate) BETWEEN '" + Today + "' AND '" + NextDate + "' )";

            string FleetId = Session["CPARAM"].ToString().Split(':').GetValue(0).ToString().Trim();
            string VesselCode = Session["CPARAM"].ToString().Split(':').GetValue(1).ToString().Trim();
            if (VesselCode.Trim() == "< All >")
            {
                VesselCode = "0";
            }

            if (FleetId.Trim() != "0")
            {
                if (VesselCode.Trim() == "0")
                {
                    WhereCDue = WhereCDue + "AND VCJMU.VesselCode IN (SELECT VesselCode FROM  dbo.Vessel WHERE FleetId = " + FleetId.Trim() + ")";
                }
                else
                {
                    WhereCDue = WhereCDue + "AND VCJMU.VesselCode = '" + VesselCode.Trim() + "' ";
                }
            }
            else if (VesselCode.Trim() != "0")
            {
                WhereCDue = WhereCDue + "AND VCJMU.VesselCode = '" + VesselCode.Trim() + "' ";
            }
            DataTable dtToday = Common.Execute_Procedures_Select_ByQuery(strCDue + WhereCDue + " ORDER BY RankCode ");
            int dr = dtToday.Rows.Count;
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("JobPlanningHomeReport.rpt"));
            rpt.SetDataSource(dtToday);
            rpt.SetParameterValue("@Header", "Jobs Due in Next " + Days.ToString() + " Days");

        }


        //--------------------------- CRITICAL PLANNING -------------------------------------------

        if (Request.QueryString["Mode"] == "C_PLAN")
        {

            string strCPlan = "SELECT VCJM.VesselCode,(SELECT ShipName FROM Settings WHERE ShipCode = VCJM.VesselCode) AS VesselName,CM.ComponentId,CM.ComponentCode,CM.ComponentName ,CM.CriticalType,JM.JobCode,JM.JobName AS JobType,CJM.DescrSh AS JobName,VCJM.CompJobId,JIM.IntervalName ,RM.RankCode,VCJM.Interval,replace(convert(varchar(15),VCJMU.NextDueDate,106),' ','-') As NextDueDate,CASE WHEN VCJMU.NextDueDate > getdate() THEN 'DUE' ELSE 'OVER DUE' END AS DueStatus ,CASE PM.Status WHEN 1 THEN 'Issued' WHEN 2 THEN 'Completed' WHEN 3 THEN 'Postponed' WHEN 4 THEN 'Cancelled' ELSE '' END AS WorkOrderStatus,VCJMU.NextHour,REPLACE(CONVERT(VARCHAR(15),VCJMU.LastDone,106),' ','-') AS LastDone,VCJMU.LastHour,CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.LastDueDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE DATEDiff(dd,PM.DoneDate,PM.LastDueDate) END AS Difference,(ISNULL(PM.DoneHour,0) - ISNULL(PM.LastDueHours,0)) AS DiffHour,CASE PM.Status WHEN 4 THEN 0 ELSE ISNULL(PM.AssignedTo,0) END AS PlannedRank,CASE PM.Status WHEN 1 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PlanDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PlanDate,''),106),' ','-') END WHEN 2 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.DoneDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.DoneDate,''),106),' ','-') END WHEN 3 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PostPoneDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PostPoneDate,''),106),' ','-') END  ELSE '' END AS PlanDate,CASE ISNULL(PM.Status,0) WHEN 4 THEN '' WHEN 0 THEN '' ELSE ISNULL(RM.RankCode,'') END AS Rank,Row_Number() OVER(ORDER BY CM.ComponentCode) AS RowNumber, CASE CM.ClassEquip WHEN 'true' THEN 'Class Equipment - ' + VCM.ClassEquipCode ELSE '' END AS ClassEquip,CASE CM.CriticalEquip WHEN 'true' THEN 'Critical Equipment' ELSE '' END AS CriticalEquip FROM VSL_VesselComponentJobMaster VCJM " +
                            "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId  AND VCJM.Status = 'A' " +
                            "INNER JOIN  ( " +
                            "ComponentsJobMapping CJM " +
                            "INNER JOIN JobMaster JM  ON JM.JobId = CJM.JobId " +
                            "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId AND CriticalEquip=1 " +
                            "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCM.ComponentId = CM.ComponentId AND VCM.Status = 'A' " +
                            ") ON  VCJM.ComponentId = CJM.ComponentId AND VCJM.CompJobId = CJM.CompJobId AND VCJM.Status = 'A' AND VCM.VesselCode = VCJM.VesselCode " +
                            "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId " +
                            "INNER JOIN VSL_PlanMaster PM ON PM.VesselCode = VCJM.VesselCode AND PM.ComponentId = VCJM.ComponentId AND PM.CompJobId = VCJM.CompJobId " +
                            "INNER JOIN Rank RM ON RM.RankId = PM.AssignedTo ";

            string WhereCPlan = "WHERE ( dbo.getDatePart(PM.PlanDate) BETWEEN '" + Today + "' AND '" + NextDate + "' ) ";

            string FleetId = Session["CPARAM"].ToString().Split(':').GetValue(0).ToString().Trim();
            string VesselCode = Session["CPARAM"].ToString().Split(':').GetValue(1).ToString().Trim();
            if (VesselCode.Trim() == "< All >")
            {
                VesselCode = "0";
            }

            if (FleetId.Trim() != "0")
            {
                if (VesselCode.Trim() == "0")
                {
                    WhereCPlan = WhereCPlan + "AND PM.VesselCode IN (SELECT VesselCode FROM  dbo.Vessel WHERE FleetId = " + FleetId.Trim() + ")";
                }
                else
                {
                    WhereCPlan = WhereCPlan + "AND PM.VesselCode = '" + VesselCode.Trim() + "' ";
                }
            }
            else if (VesselCode.Trim() != "0")
            {
                WhereCPlan = WhereCPlan + "AND PM.VesselCode = '" + VesselCode.Trim() + "' ";
            }
            DataTable dtToday = Common.Execute_Procedures_Select_ByQuery(strCPlan + WhereCPlan + " ORDER BY RankCode ");
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("JobPlanningHomeReport.rpt"));
            rpt.SetDataSource(dtToday);
            rpt.SetParameterValue("@Header", "Jobs Planned For Next " + Days.ToString() + " Days");
        }

        //--------------------------- CRITICAL DONE -------------------------------------------

        if (Request.QueryString["Mode"] == "C_DONE")
        {
            string strCDone = "SELECT VCJM.VesselCode,(SELECT ShipName FROM Settings WHERE ShipCode = VCJM.VesselCode) AS VesselName,CM.ComponentId,CM.ComponentCode,CM.ComponentName ,JM.JobCode,JM.JobName AS JobType,CJM.DescrSh AS JobName,VCJM.CompJobId,JIM.IntervalName ,RM.RankCode,VCJM.Interval,replace(convert(varchar(15),VCJMU.NextDueDate,106),' ','-') As NextDueDate,CASE WHEN VCJMU.NextDueDate > getdate() THEN 'DUE' ELSE 'OVER DUE' END AS DueStatus ,'' AS WorkOrderStatus,VCJMU.NextHour,REPLACE(CONVERT(VARCHAR(15),VCJMU.LastDone,106),' ','-') AS LastDone,VCJMU.LastHour,'' AS Difference,'' AS DiffHour,0 AS PlannedRank,'' AS PlanDate,RM.RankCode AS Rank,Row_Number() OVER(ORDER BY CM.ComponentCode) AS RowNumber, CASE CM.ClassEquip WHEN 'true' THEN 'Class Equipment - ' + VCM.ClassEquipCode ELSE '' END AS ClassEquip,CASE CM.CriticalEquip WHEN 'true' THEN 'Critical Equipment' ELSE '' END AS CriticalEquip,VJH.DoneDate,VJH.DoneHour,VJH.DoneBy_Code + ' - ' + VJH.DoneBy_Name AS DoneBy,VJH.ServiceReport FROM VSL_VesselComponentJobMaster VCJM  " +
                                    "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId  AND VCJM.Status = 'A' " +
                                    "INNER JOIN  (  " +
                                    "ComponentsJobMapping CJM  " +
                                    "INNER JOIN JobMaster JM  ON JM.JobId = CJM.JobId  " +
                                    "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId AND CriticalEquip=1 " +
                                    "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCM.ComponentId = CM.ComponentId AND VCM.Status = 'A' " +
                                    ")ON  VCJM.ComponentId = CJM.ComponentId AND VCJM.CompJobId = CJM.CompJobId AND VCJM.Status = 'A' AND VCM.VesselCode = VCJM.VesselCode  " +
                                    "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId  " +
                                    "INNER JOIN VSL_VesselJobUpdateHistory VJH ON VJH.VesselCode = VCJM.VesselCode AND VJH.ComponentId = VCJM.ComponentId AND VJH.CompJobId = VCJM.CompJobId  " +
                                    "INNER JOIN Rank RM ON RM.RankId = VJH.DoneBy ";

            string WhereCDone = "WHERE VJH.[Action] = 'R' AND ( dbo.getDatePart(VJH.DoneDate) BETWEEN '" + LastDate + "'  AND '" + Today + "' ) ";

            string FleetId = Session["CPARAM"].ToString().Split(':').GetValue(0).ToString().Trim();
            string VesselCode = Session["CPARAM"].ToString().Split(':').GetValue(1).ToString().Trim();
            if (VesselCode.Trim() == "< All >")
            {
                VesselCode = "0";
            }


            if (FleetId.Trim() != "0")
            {
                if (VesselCode.Trim() == "0")
                {
                    WhereCDone = WhereCDone + "AND VJH.VesselCode IN (SELECT VesselCode FROM  dbo.Vessel WHERE FleetId = " + FleetId.Trim() + ")";
                }
                else
                {
                    WhereCDone = WhereCDone + "AND VJH.VesselCode = '" + VesselCode.Trim() + "' ";
                }
            }
            else if (VesselCode.Trim() != "0")
            {
                WhereCDone = WhereCDone + "AND VJH.VesselCode = '" + VesselCode.Trim() + "' ";
            }
            DataTable dtToday = Common.Execute_Procedures_Select_ByQuery(strCDone + WhereCDone + " ORDER BY RankCode ");
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("JobComplitation.rpt"));
            rpt.SetDataSource(dtToday);
            rpt.SetParameterValue("@Header", "Jobs done in Last " + Days.ToString() + " Days");
        }


        ////--------------------------- OVER DUE -------------------------------------------
        if (Request.QueryString["Mode"] == "C_OverDue")
        {
            //string strNCODue = "SELECT VCJM.VesselCode,(SELECT ShipName FROM Settings WHERE ShipCode = VCJM.VesselCode) AS VesselName,CM.ComponentId,CM.ComponentCode,CM.ComponentName ,CM.CriticalType,JM.JobCode,JM.JobName AS JobType,CJM.DescrSh AS JobName,VCJM.CompJobId,JIM.IntervalName ,RM.RankCode,VCJM.Interval,replace(convert(varchar(15),VCJMU.NextDueDate,106),' ','-') As NextDueDate,CASE WHEN VCJMU.NextDueDate > getdate() THEN 'DUE' ELSE 'OVER DUE' END AS DueStatus ,CASE PM.Status WHEN 1 THEN 'Issued' WHEN 2 THEN 'Completed' WHEN 3 THEN 'Postponed' WHEN 4 THEN 'Cancelled' ELSE '' END AS WorkOrderStatus,VCJMU.NextHour,REPLACE(CONVERT(VARCHAR(15),VCJMU.LastDone,106),' ','-') AS LastDone,VCJMU.LastHour,CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.LastDueDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE DATEDiff(dd,PM.DoneDate,PM.LastDueDate) END AS Difference,(ISNULL(PM.DoneHour,0) - ISNULL(PM.LastDueHours,0)) AS DiffHour,CASE PM.Status WHEN 4 THEN 0 ELSE ISNULL(PM.AssignedTo,0) END AS PlannedRank,CASE PM.Status WHEN 1 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PlanDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PlanDate,''),106),' ','-') END WHEN 2 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.DoneDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.DoneDate,''),106),' ','-') END WHEN 3 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PostPoneDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PostPoneDate,''),106),' ','-') END  ELSE '' END AS PlanDate,CASE ISNULL(PM.Status,0) WHEN 4 THEN '' WHEN 0 THEN '' ELSE ISNULL(RM.RankCode,'') END AS Rank,Row_Number() OVER(ORDER BY CM.ComponentCode) AS RowNumber, CASE CM.ClassEquip WHEN 'true' THEN 'Class Equipment - ' + VCM.ClassEquipCode ELSE '' END AS ClassEquip,CASE CM.CriticalEquip WHEN 'true' THEN 'Critical Equipment' ELSE '' END AS CriticalEquip  FROM VSL_VesselComponentJobMaster VCJM " +
            //                "INNER JOIN Rank RM ON RM.RankId = VCJM.AssignTo " +
            //                "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId " +
            //                "INNER JOIN  ( " +
            //                "ComponentsJobMapping CJM " +
            //                "INNER JOIN JobMaster JM  ON JM.JobId = CJM.JobId " +
            //                "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId AND CriticalEquip<>1 " +
            //                "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCM.ComponentId = CM.ComponentId AND VCM.Status = 'A' " +
            //                ") ON  VCJM.ComponentId = CJM.ComponentId AND VCJM.CompJobId = CJM.CompJobId AND VCJM.Status = 'A' AND VCM.VesselCode = VCJM.VesselCode " +
            //                "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId " +
            //                "LEFT JOIN VSL_PlanMaster PM ON PM.VesselCode = VCJM.VesselCode AND PM.ComponentId = VCJM.ComponentId AND PM.CompJobId = VCJM.CompJobId ";

            string strCODue = "SELECT VCJM.VesselCode,(SELECT ShipName FROM Settings WHERE ShipCode = VCJM.VesselCode) AS VesselName,CM.ComponentId,CM.ComponentCode,CM.ComponentName ,CM.CriticalType,JM.JobCode,JM.JobName AS JobType,CJM.DescrSh AS JobName,VCJM.CompJobId,JIM.IntervalName ,RM.RankCode,VCJM.Interval,replace(convert(varchar(15),VCJMU.NextDueDate,106),' ','-') As NextDueDate,CASE WHEN VCJMU.NextDueDate > getdate() THEN 'DUE' ELSE 'OVER DUE' END AS DueStatus ,CASE PM.Status WHEN 1 THEN 'Issued' WHEN 2 THEN 'Completed' WHEN 3 THEN 'Postponed' WHEN 4 THEN 'Cancelled' ELSE '' END AS WorkOrderStatus,VCJMU.NextHour,REPLACE(CONVERT(VARCHAR(15),VCJMU.LastDone,106),' ','-') AS LastDone,VCJMU.LastHour,CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.LastDueDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE DATEDiff(dd,PM.DoneDate,PM.LastDueDate) END AS Difference,(ISNULL(PM.DoneHour,0) - ISNULL(PM.LastDueHours,0)) AS DiffHour,CASE PM.Status WHEN 4 THEN 0 ELSE ISNULL(PM.AssignedTo,0) END AS PlannedRank,CASE PM.Status WHEN 1 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PlanDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PlanDate,''),106),' ','-') END WHEN 2 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.DoneDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.DoneDate,''),106),' ','-') END WHEN 3 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PostPoneDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PostPoneDate,''),106),' ','-') END  ELSE '' END AS PlanDate,CASE ISNULL(PM.Status,0) WHEN 4 THEN '' WHEN 0 THEN '' ELSE ISNULL(RM.RankCode,'') END AS Rank,Row_Number() OVER(ORDER BY CM.ComponentCode) AS RowNumber, CASE CM.ClassEquip WHEN 'true' THEN 'Class Equipment - ' + VCM.ClassEquipCode ELSE '' END AS ClassEquip,CASE CM.CriticalEquip WHEN 'true' THEN 'Critical Equipment' ELSE '' END AS CriticalEquip FROM VSL_VesselComponentJobMaster VCJM " +
                                "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId  AND VCJM.Status = 'A' " +
                                "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCJM.VesselCode = VCM.VesselCode AND VCJM.ComponentId = VCM.ComponentId AND VCM.Status = 'A' " +
                                "INNER JOIN ComponentsJobMapping CJM ON VCJM.CompjobId=CJM.CompjobId " +
                                "INNER JOIN ComponentMaster CM ON CM.ComponentId = VCJM.ComponentId AND CriticalEquip=1 " +
                                "INNER JOIN JobMaster JM  ON JM.JobId = VCJM.JobId " +
                                "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId " +
                                "LEFT JOIN VSL_PlanMaster PM ON PM.VesselCode = VCJM.VesselCode AND PM.ComponentId = VCJM.ComponentId AND PM.CompJobId = VCJM.CompJobId " +
                                "LEFT JOIN Rank RM ON RM.RankId = PM.AssignedTo ";

            string WhereCODue = " WHERE (VCJMU.NextDueDate < CONVERT(SMALLDATETIME,CONVERT(VARCHAR,GETDATE())) OR VCJMU.NextDueDate IS NULL) ";

            string FleetId = Session["CPARAM"].ToString().Split(':').GetValue(0).ToString().Trim();
            string VesselCode = Session["CPARAM"].ToString().Split(':').GetValue(1).ToString().Trim();
            if (VesselCode.Trim() == "< All >")
            {
                VesselCode = "0";
            }

            if (FleetId.Trim() != "0")
            {
                if (VesselCode.Trim() == "0")
                {
                    WhereCODue = WhereCODue + "AND VCJMU.VesselCode IN (SELECT VesselCode FROM  dbo.Vessel WHERE FleetId = " + FleetId.Trim() + ")";
                }
                else
                {
                    WhereCODue = WhereCODue + "AND VCJMU.VesselCode = '" + VesselCode.Trim() + "' ";
                }
            }
            else if (VesselCode.Trim() != "0")
            {
                WhereCODue = WhereCODue + "AND VCJMU.VesselCode = '" + VesselCode.Trim() + "' ";
            }
            DataTable dtToday = Common.Execute_Procedures_Select_ByQuery(strCODue + WhereCODue + " ORDER BY RankCode ");
            int dr = dtToday.Rows.Count;
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("JobPlanningHomeReport.rpt"));
            rpt.SetDataSource(dtToday);
            rpt.SetParameterValue("@Header", "Jobs Critical Over Due on " + Today);

        }
        //if (Request.QueryString["Mode"] == "TOD")
        //{
        //    string strTotOD = "SELECT VCJM.VesselCode,(SELECT ShipName FROM Settings WHERE ShipCode = VCJM.VesselCode) AS VesselName,CM.ComponentId,CM.ComponentCode,CM.ComponentName ,CM.CriticalType,JM.JobCode,JM.JobName AS JobType,CJM.DescrSh AS JobName,VCJM.CompJobId,JIM.IntervalName ,RM.RankCode,VCJM.Interval,replace(convert(varchar(15),VCJMU.NextDueDate,106),' ','-') As NextDueDate,CASE WHEN VCJMU.NextDueDate > getdate() THEN 'DUE' ELSE 'OVER DUE' END AS DueStatus ,CASE PM.Status WHEN 1 THEN 'Issued' WHEN 2 THEN 'Completed' WHEN 3 THEN 'Postponed' WHEN 4 THEN 'Cancelled' ELSE '' END AS WorkOrderStatus,VCJMU.NextHour,REPLACE(CONVERT(VARCHAR(15),VCJMU.LastDone,106),' ','-') AS LastDone,VCJMU.LastHour,CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.LastDueDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE DATEDiff(dd,PM.DoneDate,PM.LastDueDate) END AS Difference,(ISNULL(PM.DoneHour,0) - ISNULL(PM.LastDueHours,0)) AS DiffHour,CASE PM.Status WHEN 4 THEN 0 ELSE ISNULL(PM.AssignedTo,0) END AS PlannedRank,CASE PM.Status WHEN 1 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PlanDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PlanDate,''),106),' ','-') END WHEN 2 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.DoneDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.DoneDate,''),106),' ','-') END WHEN 3 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PostPoneDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PostPoneDate,''),106),' ','-') END  ELSE '' END AS PlanDate,CASE ISNULL(PM.Status,0) WHEN 4 THEN '' WHEN 0 THEN '' ELSE ISNULL(RM.RankCode,'') END AS Rank,Row_Number() OVER(ORDER BY CM.ComponentCode) AS RowNumber, CASE CM.ClassEquip WHEN 'true' THEN 'Class Equipment - ' + VCM.ClassEquipCode ELSE '' END AS ClassEquip,CASE CM.CriticalEquip WHEN 'true' THEN 'Critical Equipment' ELSE '' END AS CriticalEquip  FROM VSL_VesselComponentJobMaster VCJM " +
        //                    "INNER JOIN Rank RM ON RM.RankId = VCJM.AssignTo " +
        //                    "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId " +
        //                    "INNER JOIN  ( " +
        //                    "ComponentsJobMapping CJM " +
        //                    "INNER JOIN JobMaster JM  ON JM.JobId = CJM.JobId " +
        //                    "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId " +
        //                    "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCM.ComponentId = CM.ComponentId AND VCM.Status = 'A' " +
        //                    ") ON  VCJM.ComponentId = CJM.ComponentId AND VCJM.CompJobId = CJM.CompJobId AND VCJM.Status = 'A' AND VCM.VesselCode = VCJM.VesselCode " +
        //                    "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId " +
        //                    "LEFT JOIN VSL_PlanMaster PM ON PM.VesselCode = VCJM.VesselCode AND PM.ComponentId = VCJM.ComponentId AND PM.CompJobId = VCJM.CompJobId ";

        //    string WhereTotOD = "WHERE ( VCJMU.NextDueDate IS NULL OR convert(SMALLDATETIME,REPLACE(CONVERT(VARCHAR(15),VCJMU.NextDueDate,106),' ','-')) < convert(SMALLDATETIME,REPLACE(CONVERT(VARCHAR(15),getdate(),106),' ','-')) ) ";

        //    string FleetId = Session["TOD"].ToString().Split(':').GetValue(0).ToString().Trim();
        //    string VesselCode = Session["TOD"].ToString().Split(':').GetValue(1).ToString().Trim();
        //    if (VesselCode.Trim() == "< All >")
        //    {
        //        VesselCode = "0";
        //    }


        //    if (FleetId.Trim() != "0")
        //    {
        //        if (VesselCode.Trim() == "0")
        //        {
        //            WhereTotOD = WhereTotOD + "AND VCJMU.VesselCode IN (SELECT VesselCode FROM  dbo.Vessel WHERE FleetId = " + FleetId.Trim() + ")";
        //        }
        //        else
        //        {
        //            WhereTotOD = WhereTotOD + "AND VCJMU.VesselCode = '" + VesselCode.Trim() + "' ";
        //        }
        //    }
        //    else if (VesselCode.Trim() != "0")
        //    {
        //        WhereTotOD = WhereTotOD + "AND VCJMU.VesselCode = '" + VesselCode.Trim() + "' ";
        //    }
        //    DataTable dtToday = Common.Execute_Procedures_Select_ByQuery(strTotOD + WhereTotOD + " ORDER BY RankCode ");
        //    int dr = dtToday.Rows.Count;
        //    CrystalReportViewer1.ReportSource = rpt;
        //    rpt.Load(Server.MapPath("JobPlanningHomeReport.rpt"));
        //    rpt.SetDataSource(dtToday);
        //    rpt.SetParameterValue("@Header", "Overdue Jobs");

        //}

        //if (Request.QueryString["Mode"] == "TCOD")
        //{
        //    string strTotOD = "SELECT VCJM.VesselCode,(SELECT ShipName FROM Settings WHERE ShipCode = VCJM.VesselCode) AS VesselName,CM.ComponentId,CM.ComponentCode,CM.ComponentName,CM.CriticalType ,JM.JobCode,JM.JobName AS JobType,CJM.DescrSh AS JobName,VCJM.CompJobId,JIM.IntervalName ,RM.RankCode,VCJM.Interval,replace(convert(varchar(15),VCJMU.NextDueDate,106),' ','-') As NextDueDate,CASE WHEN VCJMU.NextDueDate > getdate() THEN 'DUE' ELSE 'OVER DUE' END AS DueStatus ,CASE PM.Status WHEN 1 THEN 'Issued' WHEN 2 THEN 'Completed' WHEN 3 THEN 'Postponed' WHEN 4 THEN 'Cancelled' ELSE '' END AS WorkOrderStatus,VCJMU.NextHour,REPLACE(CONVERT(VARCHAR(15),VCJMU.LastDone,106),' ','-') AS LastDone,VCJMU.LastHour,CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.LastDueDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE DATEDiff(dd,PM.DoneDate,PM.LastDueDate) END AS Difference,(ISNULL(PM.DoneHour,0) - ISNULL(PM.LastDueHours,0)) AS DiffHour,CASE PM.Status WHEN 4 THEN 0 ELSE ISNULL(PM.AssignedTo,0) END AS PlannedRank,CASE PM.Status WHEN 1 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PlanDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PlanDate,''),106),' ','-') END WHEN 2 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.DoneDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.DoneDate,''),106),' ','-') END WHEN 3 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PostPoneDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PostPoneDate,''),106),' ','-') END  ELSE '' END AS PlanDate,CASE ISNULL(PM.Status,0) WHEN 4 THEN '' WHEN 0 THEN '' ELSE ISNULL(RM.RankCode,'') END AS Rank,Row_Number() OVER(ORDER BY CM.ComponentCode) AS RowNumber, CASE CM.ClassEquip WHEN 'true' THEN 'Class Equipment - ' + VCM.ClassEquipCode ELSE '' END AS ClassEquip,CASE CM.CriticalEquip WHEN 'true' THEN 'Critical Equipment' ELSE '' END AS CriticalEquip FROM VSL_VesselComponentJobMaster VCJM " +
        //                      "INNER JOIN Rank RM ON RM.RankId = VCJM.AssignTo " +
        //                      "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId " +
        //                      "INNER JOIN  ( " +
        //                      "ComponentsJobMapping CJM " +
        //                      "INNER JOIN JobMaster JM  ON JM.JobId = CJM.JobId " +
        //                      "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId " +
        //                      "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCM.ComponentId = CM.ComponentId AND VCM.Status = 'A' " +
        //                      ") ON  VCJM.ComponentId = CJM.ComponentId AND VCJM.CompJobId = CJM.CompJobId AND VCJM.Status = 'A' AND VCM.VesselCode = VCJM.VesselCode " +
        //                      "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId " +
        //                      "LEFT JOIN VSL_PlanMaster PM ON PM.VesselCode = VCJM.VesselCode AND PM.ComponentId = VCJM.ComponentId AND PM.CompJobId = VCJM.CompJobId ";

        //    string WhereTotOD = "WHERE CM.CriticalEquip = 1 AND ( VCJMU.NextDueDate IS NULL OR convert(SMALLDATETIME,REPLACE(CONVERT(VARCHAR(15),VCJMU.NextDueDate,106),' ','-')) < convert(SMALLDATETIME,REPLACE(CONVERT(VARCHAR(15),getdate(),106),' ','-')) ) ";

        //    string FleetId = Session["TCOD"].ToString().Split(':').GetValue(0).ToString().Trim();
        //    string VesselCode = Session["TCOD"].ToString().Split(':').GetValue(1).ToString().Trim();
        //    if (VesselCode.Trim() == "< All >")
        //    {
        //        VesselCode = "0";
        //    }


        //    if (FleetId.Trim() != "0")
        //    {
        //        if (VesselCode.Trim() == "0")
        //        {
        //            WhereTotOD = WhereTotOD + "AND VCJMU.VesselCode IN (SELECT VesselCode FROM  dbo.Vessel WHERE FleetId = " + FleetId.Trim() + ")";
        //        }
        //        else
        //        {
        //            WhereTotOD = WhereTotOD + "AND VCJMU.VesselCode = '" + VesselCode.Trim() + "' ";
        //        }
        //    }
        //    else if (VesselCode.Trim() != "0")
        //    {
        //        WhereTotOD = WhereTotOD + "AND VCJMU.VesselCode = '" + VesselCode.Trim() + "' ";
        //    }
        //    DataTable dtToday = Common.Execute_Procedures_Select_ByQuery(strTotOD + WhereTotOD + " ORDER BY RankCode ");
        //    CrystalReportViewer1.ReportSource = rpt;
        //    rpt.Load(Server.MapPath("JobPlanningHomeReport.rpt"));
        //    rpt.SetDataSource(dtToday);
        //    rpt.SetParameterValue("@Header", "Overdue Critical Jobs");

        //}

    }
    protected void ShowNONCritical()
    { 
        Days = Common.CastAsInt32(Request.QueryString["Days"]);
        string Today = DateTime.Today.ToString("dd-MMM-yyyy");
        string NextDate = DateTime.Today.AddDays(Days+1).ToString("dd-MMM-yyyy");
        string LastDate = DateTime.Today.AddDays(-Days).ToString("dd-MMM-yyyy");

        #region ------------------------ NON CRITICAL --------------------

        //--------------------------- NON CRITICAL DUE -------------------------------------------

        if (Request.QueryString["Mode"] == "DUE")
        {
            string strNCDue = "SELECT VCJM.VesselCode,(SELECT ShipName FROM Settings WHERE ShipCode = VCJM.VesselCode) AS VesselName,CM.ComponentId,CM.ComponentCode,CM.ComponentName ,CM.CriticalType,JM.JobCode,JM.JobName AS JobType,CJM.DescrSh AS JobName,VCJM.CompJobId,JIM.IntervalName ,RM.RankCode,VCJM.Interval,replace(convert(varchar(15),VCJMU.NextDueDate,106),' ','-') As NextDueDate,CASE WHEN VCJMU.NextDueDate > getdate() THEN 'DUE' ELSE 'OVER DUE' END AS DueStatus ,CASE PM.Status WHEN 1 THEN 'Issued' WHEN 2 THEN 'Completed' WHEN 3 THEN 'Postponed' WHEN 4 THEN 'Cancelled' ELSE '' END AS WorkOrderStatus,VCJMU.NextHour,REPLACE(CONVERT(VARCHAR(15),VCJMU.LastDone,106),' ','-') AS LastDone,VCJMU.LastHour,CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.LastDueDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE DATEDiff(dd,PM.DoneDate,PM.LastDueDate) END AS Difference,(ISNULL(PM.DoneHour,0) - ISNULL(PM.LastDueHours,0)) AS DiffHour,CASE PM.Status WHEN 4 THEN 0 ELSE ISNULL(PM.AssignedTo,0) END AS PlannedRank,CASE PM.Status WHEN 1 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PlanDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PlanDate,''),106),' ','-') END WHEN 2 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.DoneDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.DoneDate,''),106),' ','-') END WHEN 3 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PostPoneDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PostPoneDate,''),106),' ','-') END  ELSE '' END AS PlanDate,CASE ISNULL(PM.Status,0) WHEN 4 THEN '' WHEN 0 THEN '' ELSE ISNULL(RM.RankCode,'') END AS Rank,Row_Number() OVER(ORDER BY CM.ComponentCode) AS RowNumber, CASE CM.ClassEquip WHEN 'true' THEN 'Class Equipment - ' + VCM.ClassEquipCode ELSE '' END AS ClassEquip,CASE CM.CriticalEquip WHEN 'true' THEN 'Critical Equipment' ELSE '' END AS CriticalEquip  FROM VSL_VesselComponentJobMaster VCJM " +
                            "INNER JOIN Rank RM ON RM.RankId = VCJM.AssignTo  AND VCJM.Status = 'A' " +
                            "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId " +
                            "INNER JOIN  ( " +
                            "ComponentsJobMapping CJM " +
                            "INNER JOIN JobMaster JM  ON JM.JobId = CJM.JobId " +
                            "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId AND CriticalEquip<>1 " +
                            "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCM.ComponentId = CM.ComponentId AND VCM.Status = 'A' " +
                            ") ON  VCJM.ComponentId = CJM.ComponentId AND VCJM.CompJobId = CJM.CompJobId AND VCJM.Status = 'A' AND VCM.VesselCode = VCJM.VesselCode " +
                            "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId " +
                            "LEFT JOIN VSL_PlanMaster PM ON PM.VesselCode = VCJM.VesselCode AND PM.ComponentId = VCJM.ComponentId AND PM.CompJobId = VCJM.CompJobId ";

            string WhereNCDue = "WHERE ( dbo.getDatePart(VCJMU.NextDueDate) BETWEEN '" + Today + "' AND '" + NextDate + "' )";

            string FleetId = Session["PARAM"].ToString().Split(':').GetValue(0).ToString().Trim();
            string VesselCode = Session["PARAM"].ToString().Split(':').GetValue(1).ToString().Trim();
            if (VesselCode.Trim() == "< All >")
            {
                VesselCode = "0";
            }

            if (FleetId.Trim() != "0")
            {
                if (VesselCode.Trim() == "0")
                {
                    WhereNCDue = WhereNCDue + "AND VCJMU.VesselCode IN (SELECT VesselCode FROM  dbo.Vessel WHERE FleetId = " + FleetId.Trim() + ")";
                }
                else
                {
                    WhereNCDue = WhereNCDue + "AND VCJMU.VesselCode = '" + VesselCode.Trim() + "' ";
                }
            }
            else if (VesselCode.Trim() != "0")
            {
                WhereNCDue = WhereNCDue + "AND VCJMU.VesselCode = '" + VesselCode.Trim() + "' ";
            }
            DataTable dtToday = Common.Execute_Procedures_Select_ByQuery(strNCDue + WhereNCDue + " ORDER BY RankCode ");
            int dr = dtToday.Rows.Count;
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("JobPlanningHomeReport.rpt"));
            rpt.SetDataSource(dtToday);
            rpt.SetParameterValue("@Header", "Jobs Due in Next " + Days.ToString() + " Days");

        }
        //--------------------------- NON CRITICAL OVER DUE -------------------------------------------

        if (Request.QueryString["Mode"] == "OVERDUE")
        {
            //string strNCODue = "SELECT VCJM.VesselCode,(SELECT ShipName FROM Settings WHERE ShipCode = VCJM.VesselCode) AS VesselName,CM.ComponentId,CM.ComponentCode,CM.ComponentName ,CM.CriticalType,JM.JobCode,JM.JobName AS JobType,CJM.DescrSh AS JobName,VCJM.CompJobId,JIM.IntervalName ,RM.RankCode,VCJM.Interval,replace(convert(varchar(15),VCJMU.NextDueDate,106),' ','-') As NextDueDate,CASE WHEN VCJMU.NextDueDate > getdate() THEN 'DUE' ELSE 'OVER DUE' END AS DueStatus ,CASE PM.Status WHEN 1 THEN 'Issued' WHEN 2 THEN 'Completed' WHEN 3 THEN 'Postponed' WHEN 4 THEN 'Cancelled' ELSE '' END AS WorkOrderStatus,VCJMU.NextHour,REPLACE(CONVERT(VARCHAR(15),VCJMU.LastDone,106),' ','-') AS LastDone,VCJMU.LastHour,CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.LastDueDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE DATEDiff(dd,PM.DoneDate,PM.LastDueDate) END AS Difference,(ISNULL(PM.DoneHour,0) - ISNULL(PM.LastDueHours,0)) AS DiffHour,CASE PM.Status WHEN 4 THEN 0 ELSE ISNULL(PM.AssignedTo,0) END AS PlannedRank,CASE PM.Status WHEN 1 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PlanDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PlanDate,''),106),' ','-') END WHEN 2 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.DoneDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.DoneDate,''),106),' ','-') END WHEN 3 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PostPoneDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PostPoneDate,''),106),' ','-') END  ELSE '' END AS PlanDate,CASE ISNULL(PM.Status,0) WHEN 4 THEN '' WHEN 0 THEN '' ELSE ISNULL(RM.RankCode,'') END AS Rank,Row_Number() OVER(ORDER BY CM.ComponentCode) AS RowNumber, CASE CM.ClassEquip WHEN 'true' THEN 'Class Equipment - ' + VCM.ClassEquipCode ELSE '' END AS ClassEquip,CASE CM.CriticalEquip WHEN 'true' THEN 'Critical Equipment' ELSE '' END AS CriticalEquip  FROM VSL_VesselComponentJobMaster VCJM " +
            //                "INNER JOIN Rank RM ON RM.RankId = VCJM.AssignTo " +
            //                "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId " +
            //                "INNER JOIN  ( " +
            //                "ComponentsJobMapping CJM " +
            //                "INNER JOIN JobMaster JM  ON JM.JobId = CJM.JobId " +
            //                "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId AND CriticalEquip<>1 " +
            //                "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCM.ComponentId = CM.ComponentId AND VCM.Status = 'A' " +
            //                ") ON  VCJM.ComponentId = CJM.ComponentId AND VCJM.CompJobId = CJM.CompJobId AND VCJM.Status = 'A' AND VCM.VesselCode = VCJM.VesselCode " +
            //                "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId " +
            //                "LEFT JOIN VSL_PlanMaster PM ON PM.VesselCode = VCJM.VesselCode AND PM.ComponentId = VCJM.ComponentId AND PM.CompJobId = VCJM.CompJobId ";

            string strNCODue = "SELECT VCJM.VesselCode,(SELECT ShipName FROM Settings WHERE ShipCode = VCJM.VesselCode) AS VesselName,CM.ComponentId,CM.ComponentCode,CM.ComponentName ,CM.CriticalType,JM.JobCode,JM.JobName AS JobType,CJM.DescrSh AS JobName,VCJM.CompJobId,JIM.IntervalName ,RM.RankCode,VCJM.Interval,replace(convert(varchar(15),VCJMU.NextDueDate,106),' ','-') As NextDueDate,CASE WHEN VCJMU.NextDueDate > getdate() THEN 'DUE' ELSE 'OVER DUE' END AS DueStatus ,CASE PM.Status WHEN 1 THEN 'Issued' WHEN 2 THEN 'Completed' WHEN 3 THEN 'Postponed' WHEN 4 THEN 'Cancelled' ELSE '' END AS WorkOrderStatus,VCJMU.NextHour,REPLACE(CONVERT(VARCHAR(15),VCJMU.LastDone,106),' ','-') AS LastDone,VCJMU.LastHour,CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.LastDueDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE DATEDiff(dd,PM.DoneDate,PM.LastDueDate) END AS Difference,(ISNULL(PM.DoneHour,0) - ISNULL(PM.LastDueHours,0)) AS DiffHour,CASE PM.Status WHEN 4 THEN 0 ELSE ISNULL(PM.AssignedTo,0) END AS PlannedRank,CASE PM.Status WHEN 1 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PlanDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PlanDate,''),106),' ','-') END WHEN 2 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.DoneDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.DoneDate,''),106),' ','-') END WHEN 3 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PostPoneDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PostPoneDate,''),106),' ','-') END  ELSE '' END AS PlanDate,CASE ISNULL(PM.Status,0) WHEN 4 THEN '' WHEN 0 THEN '' ELSE ISNULL(RM.RankCode,'') END AS Rank,Row_Number() OVER(ORDER BY CM.ComponentCode) AS RowNumber, CASE CM.ClassEquip WHEN 'true' THEN 'Class Equipment - ' + VCM.ClassEquipCode ELSE '' END AS ClassEquip,CASE CM.CriticalEquip WHEN 'true' THEN 'Critical Equipment' ELSE '' END AS CriticalEquip FROM VSL_VesselComponentJobMaster VCJM " +
                                "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId  AND VCJM.Status = 'A' " +
                                "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCJM.VesselCode = VCM.VesselCode AND VCJM.ComponentId = VCM.ComponentId AND VCM.Status = 'A' " +
                                "INNER JOIN ComponentsJobMapping CJM ON VCJM.CompjobId=CJM.CompjobId " +
                                "INNER JOIN ComponentMaster CM ON CM.ComponentId = VCJM.ComponentId AND CriticalEquip<>1 " +
                                "INNER JOIN JobMaster JM  ON JM.JobId = VCJM.JobId " +
                                "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId " +
                                "LEFT JOIN VSL_PlanMaster PM ON PM.VesselCode = VCJM.VesselCode AND PM.ComponentId = VCJM.ComponentId AND PM.CompJobId = VCJM.CompJobId " +
                                "LEFT JOIN Rank RM ON RM.RankId = PM.AssignedTo ";

            string WhereNCODue = " WHERE (VCJMU.NextDueDate < CONVERT(SMALLDATETIME,CONVERT(VARCHAR,GETDATE())) OR VCJMU.NextDueDate IS NULL) ";

            string FleetId = Session["PARAM"].ToString().Split(':').GetValue(0).ToString().Trim();
            string VesselCode = Session["PARAM"].ToString().Split(':').GetValue(1).ToString().Trim();
            if (VesselCode.Trim() == "< All >")
            {
                VesselCode = "0";
            }

            if (FleetId.Trim() != "0")
            {
                if (VesselCode.Trim() == "0")
                {
                    WhereNCODue = WhereNCODue + "AND VCJMU.VesselCode IN (SELECT VesselCode FROM  dbo.Vessel WHERE FleetId = " + FleetId.Trim() + ")";
                }
                else
                {
                    WhereNCODue = WhereNCODue + "AND VCJMU.VesselCode = '" + VesselCode.Trim() + "' ";
                }
            }
            else if (VesselCode.Trim() != "0")
            {
                WhereNCODue = WhereNCODue + "AND VCJMU.VesselCode = '" + VesselCode.Trim() + "' ";
            }
            DataTable dtToday = Common.Execute_Procedures_Select_ByQuery(strNCODue + WhereNCODue + " ORDER BY RankCode ");
            int dr = dtToday.Rows.Count;
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("JobPlanningHomeReport.rpt"));
            rpt.SetDataSource(dtToday);
            rpt.SetParameterValue("@Header", "Jobs Over Due on " + Today);

        }

        //--------------------------- NON CRITICAL PLANNING -------------------------------------------

        if (Request.QueryString["Mode"] == "PLAN")
        {

            string strNCPlan = "SELECT VCJM.VesselCode,(SELECT ShipName FROM Settings WHERE ShipCode = VCJM.VesselCode) AS VesselName,CM.ComponentId,CM.ComponentCode,CM.ComponentName ,CM.CriticalType,JM.JobCode,JM.JobName AS JobType,CJM.DescrSh AS JobName,VCJM.CompJobId,JIM.IntervalName ,RM.RankCode,VCJM.Interval,replace(convert(varchar(15),VCJMU.NextDueDate,106),' ','-') As NextDueDate,CASE WHEN VCJMU.NextDueDate > getdate() THEN 'DUE' ELSE 'OVER DUE' END AS DueStatus ,CASE PM.Status WHEN 1 THEN 'Issued' WHEN 2 THEN 'Completed' WHEN 3 THEN 'Postponed' WHEN 4 THEN 'Cancelled' ELSE '' END AS WorkOrderStatus,VCJMU.NextHour,REPLACE(CONVERT(VARCHAR(15),VCJMU.LastDone,106),' ','-') AS LastDone,VCJMU.LastHour,CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.LastDueDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE DATEDiff(dd,PM.DoneDate,PM.LastDueDate) END AS Difference,(ISNULL(PM.DoneHour,0) - ISNULL(PM.LastDueHours,0)) AS DiffHour,CASE PM.Status WHEN 4 THEN 0 ELSE ISNULL(PM.AssignedTo,0) END AS PlannedRank,CASE PM.Status WHEN 1 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PlanDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PlanDate,''),106),' ','-') END WHEN 2 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.DoneDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.DoneDate,''),106),' ','-') END WHEN 3 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PostPoneDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PostPoneDate,''),106),' ','-') END  ELSE '' END AS PlanDate,CASE ISNULL(PM.Status,0) WHEN 4 THEN '' WHEN 0 THEN '' ELSE ISNULL(RM.RankCode,'') END AS Rank,Row_Number() OVER(ORDER BY CM.ComponentCode) AS RowNumber, CASE CM.ClassEquip WHEN 'true' THEN 'Class Equipment - ' + VCM.ClassEquipCode ELSE '' END AS ClassEquip,CASE CM.CriticalEquip WHEN 'true' THEN 'Critical Equipment' ELSE '' END AS CriticalEquip FROM VSL_VesselComponentJobMaster VCJM " +
                            "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId  AND VCJM.Status = 'A' " +
                            "INNER JOIN  ( " +
                            "ComponentsJobMapping CJM " +
                            "INNER JOIN JobMaster JM  ON JM.JobId = CJM.JobId " +
                            "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId AND CriticalEquip<>1 " +
                            "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCM.ComponentId = CM.ComponentId AND VCM.Status = 'A' " +
                            ") ON  VCJM.ComponentId = CJM.ComponentId AND VCJM.CompJobId = CJM.CompJobId AND VCJM.Status = 'A' AND VCM.VesselCode = VCJM.VesselCode " +
                            "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId " +
                            "INNER JOIN VSL_PlanMaster PM ON PM.VesselCode = VCJM.VesselCode AND PM.ComponentId = VCJM.ComponentId AND PM.CompJobId = VCJM.CompJobId " +
                            "INNER JOIN Rank RM ON RM.RankId = PM.AssignedTo ";

            string WhereNCPlan = "WHERE ( dbo.getDatePart(PM.PlanDate) BETWEEN '" + Today + "' AND '" + NextDate + "' ) ";

            string FleetId = Session["PARAM"].ToString().Split(':').GetValue(0).ToString().Trim();
            string VesselCode = Session["PARAM"].ToString().Split(':').GetValue(1).ToString().Trim();
            if (VesselCode.Trim() == "< All >")
            {
                VesselCode = "0";
            }

            if (FleetId.Trim() != "0")
            {
                if (VesselCode.Trim() == "0")
                {
                    WhereNCPlan = WhereNCPlan + "AND PM.VesselCode IN (SELECT VesselCode FROM  dbo.Vessel WHERE FleetId = " + FleetId.Trim() + ")";
                }
                else
                {
                    WhereNCPlan = WhereNCPlan + "AND PM.VesselCode = '" + VesselCode.Trim() + "' ";
                }
            }
            else if (VesselCode.Trim() != "0")
            {
                WhereNCPlan = WhereNCPlan + "AND PM.VesselCode = '" + VesselCode.Trim() + "' ";
            }
            DataTable dtToday = Common.Execute_Procedures_Select_ByQuery(strNCPlan + WhereNCPlan + " ORDER BY RankCode ");
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("JobPlanningHomeReport.rpt"));
            rpt.SetDataSource(dtToday);
            rpt.SetParameterValue("@Header", "Jobs Planned For Next " + Days.ToString() + " Days");
        }
        //--------------------------- NON CRITICAL POSTPONED -------------------------------------------

        if (Request.QueryString["Mode"] == "POSTPONED")
        {
            string strNCDone = "SELECT VCJM.VesselCode,(SELECT ShipName FROM Settings WHERE ShipCode = VCJM.VesselCode) AS VesselName,CM.ComponentId,CM.ComponentCode,CM.ComponentName ,JM.JobCode,JM.JobName AS JobType,CJM.DescrSh AS JobName,VCJM.CompJobId,JIM.IntervalName ,RM.RankCode,VCJM.Interval,replace(convert(varchar(15),VCJMU.NextDueDate,106),' ','-') As NextDueDate,CASE WHEN VCJMU.NextDueDate > getdate() THEN 'DUE' ELSE 'OVER DUE' END AS DueStatus ,'' AS WorkOrderStatus,VCJMU.NextHour,REPLACE(CONVERT(VARCHAR(15),VCJMU.LastDone,106),' ','-') AS LastDone,VCJMU.LastHour,'' AS Difference,'' AS DiffHour,0 AS PlannedRank,'' AS PlanDate,RM.RankCode AS Rank,Row_Number() OVER(ORDER BY CM.ComponentCode) AS RowNumber, CASE CM.ClassEquip WHEN 'true' THEN 'Class Equipment - ' + VCM.ClassEquipCode ELSE '' END AS ClassEquip,CASE CM.CriticalEquip WHEN 'true' THEN 'Critical Equipment' ELSE '' END AS CriticalEquip,VJH.DoneDate,VJH.DoneHour,VJH.DoneBy_Code + ' - ' + VJH.DoneBy_Name AS DoneBy,VJH.ServiceReport FROM VSL_VesselComponentJobMaster VCJM  " +
                                    "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId  AND VCJM.Status = 'A' " +
                                    "INNER JOIN  (  " +
                                    "ComponentsJobMapping CJM  " +
                                    "INNER JOIN JobMaster JM  ON JM.JobId = CJM.JobId  " +
                                    "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId AND CriticalEquip<>1 " +
                                    "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCM.ComponentId = CM.ComponentId AND VCM.Status = 'A' " +
                                    ")ON  VCJM.ComponentId = CJM.ComponentId AND VCJM.CompJobId = CJM.CompJobId AND VCJM.Status = 'A' AND VCM.VesselCode = VCJM.VesselCode  " +
                                    "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId  " +
                                    "INNER JOIN VSL_VesselJobUpdateHistory VJH ON VJH.VesselCode = VCJM.VesselCode AND VJH.ComponentId = VCJM.ComponentId AND VJH.CompJobId = VCJM.CompJobId  " +
                                    "INNER JOIN Rank RM ON RM.RankId = VJH.DoneBy ";

            string WhereNCDone = "WHERE VJH.[Action] = 'P' AND ( dbo.getDatePart(VJH.PostPoneDate) BETWEEN '" + LastDate + "'  AND '" + Today + "' ) ";

            string FleetId = Session["PARAM"].ToString().Split(':').GetValue(0).ToString().Trim();
            string VesselCode = Session["PARAM"].ToString().Split(':').GetValue(1).ToString().Trim();
            if (VesselCode.Trim() == "< All >")
            {
                VesselCode = "0";
            }


            if (FleetId.Trim() != "0")
            {
                if (VesselCode.Trim() == "0")
                {
                    WhereNCDone = WhereNCDone + "AND VJH.VesselCode IN (SELECT VesselCode FROM  dbo.Vessel WHERE FleetId = " + FleetId.Trim() + ")";
                }
                else
                {
                    WhereNCDone = WhereNCDone + "AND VJH.VesselCode = '" + VesselCode.Trim() + "' ";
                }
            }
            else if (VesselCode.Trim() != "0")
            {
                WhereNCDone = WhereNCDone + "AND VJH.VesselCode = '" + VesselCode.Trim() + "' ";
            }
            DataTable dtToday = Common.Execute_Procedures_Select_ByQuery(strNCDone + WhereNCDone + " ORDER BY RankCode ");
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("JobComplitation.rpt"));
            rpt.SetDataSource(dtToday);
            rpt.SetParameterValue("@Header", "Jobs postponed in Last " + Days.ToString() + " Days");
        }
        //--------------------------- NON CRITICAL BREAKDOWN & UNPLANNED -------------------------------------------

        if (Request.QueryString["Mode"] == "B_UP")
        {


        }
        //--------------------------- NON CRITICAL DONE -------------------------------------------

        if (Request.QueryString["Mode"] == "DONE")
        {
            string strNCDone = "SELECT VCJM.VesselCode,(SELECT ShipName FROM Settings WHERE ShipCode = VCJM.VesselCode) AS VesselName,CM.ComponentId,CM.ComponentCode,CM.ComponentName ,JM.JobCode,JM.JobName AS JobType,CJM.DescrSh AS JobName,VCJM.CompJobId,JIM.IntervalName ,RM.RankCode,VCJM.Interval,replace(convert(varchar(15),VCJMU.NextDueDate,106),' ','-') As NextDueDate,CASE WHEN VCJMU.NextDueDate > getdate() THEN 'DUE' ELSE 'OVER DUE' END AS DueStatus ,'' AS WorkOrderStatus,VCJMU.NextHour,REPLACE(CONVERT(VARCHAR(15),VCJMU.LastDone,106),' ','-') AS LastDone,VCJMU.LastHour,'' AS Difference,'' AS DiffHour,0 AS PlannedRank,'' AS PlanDate,RM.RankCode AS Rank,Row_Number() OVER(ORDER BY CM.ComponentCode) AS RowNumber, CASE CM.ClassEquip WHEN 'true' THEN 'Class Equipment - ' + VCM.ClassEquipCode ELSE '' END AS ClassEquip,CASE CM.CriticalEquip WHEN 'true' THEN 'Critical Equipment' ELSE '' END AS CriticalEquip,VJH.DoneDate,VJH.DoneHour,VJH.DoneBy_Code + ' - ' + VJH.DoneBy_Name AS DoneBy,VJH.ServiceReport FROM VSL_VesselComponentJobMaster VCJM  " +
                                    "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId  AND VCJM.Status = 'A' " +
                                    "INNER JOIN  (  " +
                                    "ComponentsJobMapping CJM  " +
                                    "INNER JOIN JobMaster JM  ON JM.JobId = CJM.JobId  " +
                                    "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId AND CriticalEquip<>1 " +
                                    "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCM.ComponentId = CM.ComponentId AND VCM.Status = 'A' " +
                                    ")ON  VCJM.ComponentId = CJM.ComponentId AND VCJM.CompJobId = CJM.CompJobId AND VCJM.Status = 'A' AND VCM.VesselCode = VCJM.VesselCode  " +
                                    "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId  " +
                                    "INNER JOIN VSL_VesselJobUpdateHistory VJH ON VJH.VesselCode = VCJM.VesselCode AND VJH.ComponentId = VCJM.ComponentId AND VJH.CompJobId = VCJM.CompJobId  " +
                                    "INNER JOIN Rank RM ON RM.RankId = VJH.DoneBy ";

            string WhereNCDone = "WHERE VJH.[Action] = 'R' AND ( dbo.getDatePart(VJH.DoneDate) BETWEEN '" + LastDate + "'  AND '" + Today + "' ) ";

            string FleetId = Session["PARAM"].ToString().Split(':').GetValue(0).ToString().Trim();
            string VesselCode = Session["PARAM"].ToString().Split(':').GetValue(1).ToString().Trim();
            if (VesselCode.Trim() == "< All >")
            {
                VesselCode = "0";
            }


            if (FleetId.Trim() != "0")
            {
                if (VesselCode.Trim() == "0")
                {
                    WhereNCDone = WhereNCDone + "AND VJH.VesselCode IN (SELECT VesselCode FROM  dbo.Vessel WHERE FleetId = " + FleetId.Trim() + ")";
                }
                else
                {
                    WhereNCDone = WhereNCDone + "AND VJH.VesselCode = '" + VesselCode.Trim() + "' ";
                }
            }
            else if (VesselCode.Trim() != "0")
            {
                WhereNCDone = WhereNCDone + "AND VJH.VesselCode = '" + VesselCode.Trim() + "' ";
            }
            DataTable dtToday = Common.Execute_Procedures_Select_ByQuery(strNCDone + WhereNCDone + " ORDER BY RankCode ");
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("JobComplitation.rpt"));
            rpt.SetDataSource(dtToday);
            rpt.SetParameterValue("@Header", "Jobs done in Last " + Days.ToString() + " Days");
        }

        //--------------------------- NON CRITICAL DONE AFTER DUE -------------------------------------------

        if (Request.QueryString["Mode"] == "DONEAFTERDUE")
        {
            string strNCDone = "SELECT VCJM.VesselCode,(SELECT ShipName FROM Settings WHERE ShipCode = VCJM.VesselCode) AS VesselName,CM.ComponentId,CM.ComponentCode,CM.ComponentName ,JM.JobCode,JM.JobName AS JobType,CJM.DescrSh AS JobName,VCJM.CompJobId,JIM.IntervalName ,RM.RankCode,VCJM.Interval,replace(convert(varchar(15),VCJMU.NextDueDate,106),' ','-') As NextDueDate,CASE WHEN VCJMU.NextDueDate > getdate() THEN 'DUE' ELSE 'OVER DUE' END AS DueStatus ,'' AS WorkOrderStatus,VCJMU.NextHour,REPLACE(CONVERT(VARCHAR(15),VCJMU.LastDone,106),' ','-') AS LastDone,VCJMU.LastHour,'' AS Difference,'' AS DiffHour,0 AS PlannedRank,'' AS PlanDate,RM.RankCode AS Rank,Row_Number() OVER(ORDER BY CM.ComponentCode) AS RowNumber, CASE CM.ClassEquip WHEN 'true' THEN 'Class Equipment - ' + VCM.ClassEquipCode ELSE '' END AS ClassEquip,CASE CM.CriticalEquip WHEN 'true' THEN 'Critical Equipment' ELSE '' END AS CriticalEquip,VJH.DoneDate,VJH.DoneHour,VJH.DoneBy_Code + ' - ' + VJH.DoneBy_Name AS DoneBy,VJH.ServiceReport FROM VSL_VesselComponentJobMaster VCJM  " +
                                    "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId  AND VCJM.Status = 'A' " +
                                    "INNER JOIN  (  " +
                                    "ComponentsJobMapping CJM  " +
                                    "INNER JOIN JobMaster JM  ON JM.JobId = CJM.JobId  " +
                                    "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId AND CriticalEquip<>1 " +
                                    "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCM.ComponentId = CM.ComponentId AND VCM.Status = 'A' " +
                                    ")ON  VCJM.ComponentId = CJM.ComponentId AND VCJM.CompJobId = CJM.CompJobId AND VCJM.Status = 'A' AND VCM.VesselCode = VCJM.VesselCode  " +
                                    "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId  " +
                                    "INNER JOIN VSL_VesselJobUpdateHistory VJH ON VJH.VesselCode = VCJM.VesselCode AND VJH.ComponentId = VCJM.ComponentId AND VJH.CompJobId = VCJM.CompJobId  " +
                                    "INNER JOIN Rank RM ON RM.RankId = VJH.DoneBy ";

            string WhereNCDone = "WHERE VJH.[Action] = 'R' AND ( dbo.getDatePart(VJH.DoneDate) BETWEEN '" + LastDate + "'  AND '" + Today + "' AND VJH.DoneDate > VJH.Lastduedate ) ";

            string FleetId = Session["PARAM"].ToString().Split(':').GetValue(0).ToString().Trim();
            string VesselCode = Session["PARAM"].ToString().Split(':').GetValue(1).ToString().Trim();
            if (VesselCode.Trim() == "< All >")
            {
                VesselCode = "0";
            }
            if (FleetId.Trim() != "0")
            {
                if (VesselCode.Trim() == "0")
                {
                    WhereNCDone = WhereNCDone + "AND VJH.VesselCode IN (SELECT VesselCode FROM  dbo.Vessel WHERE FleetId = " + FleetId.Trim() + ")";
                }
                else
                {
                    WhereNCDone = WhereNCDone + "AND VJH.VesselCode = '" + VesselCode.Trim() + "' ";
                }
            }
            else if (VesselCode.Trim() != "0")
            {
                WhereNCDone = WhereNCDone + "AND VJH.VesselCode = '" + VesselCode.Trim() + "' ";
            }
            DataTable dtToday = Common.Execute_Procedures_Select_ByQuery(strNCDone + WhereNCDone + " ORDER BY RankCode ");
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("JobComplitation.rpt"));
            rpt.SetDataSource(dtToday);
            rpt.SetParameterValue("@Header", "Jobs done in Last " + Days.ToString() + " Days");
        }

        if (Request.QueryString["Mode"] == "DEFECTDUE")
        {
            string strDDPending = "SELECT * FROM vw_DefectJobs VJH ";
            string WhereDDPending = " WHERE COMPLETIONDT IS NULL AND dbo.getDatePart(TARGETDT) BETWEEN '" + Today + "' AND '" + NextDate + "'";

            string FleetId = Session["PARAM"].ToString().Split(':').GetValue(0).ToString().Trim();
            string VesselCode = Session["PARAM"].ToString().Split(':').GetValue(1).ToString().Trim();
            if (VesselCode.Trim() == "< All >")
            {
                VesselCode = "0";
            }
            if (FleetId.Trim() != "0")
            {
                if (VesselCode.Trim() == "0")
                {
                    WhereDDPending = WhereDDPending + "AND VJH.VesselCode IN (SELECT VesselCode FROM  dbo.Vessel WHERE FleetId = " + FleetId.Trim() + ")";
                }
                else
                {
                    WhereDDPending = WhereDDPending + "AND VJH.VesselCode = '" + VesselCode.Trim() + "' ";
                }
            }
            else if (VesselCode.Trim() != "0")
            {
                WhereDDPending = WhereDDPending + "AND VJH.VesselCode = '" + VesselCode.Trim() + "' ";
            }
            DataTable dtToday = Common.Execute_Procedures_Select_ByQuery(strDDPending + WhereDDPending + " ORDER BY VESSELCODE,COMPONENTCODE");
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("DefectJobs.rpt"));
            rpt.SetDataSource(dtToday);
            rpt.SetParameterValue("@Header", "Defect Jobs Due in next " + Days.ToString() + " Days");
        }
        

        #endregion
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        ShowCritical();
        ShowNONCritical();

        #region ---------- Maintenance in Last 30 Days ---------------------
        if (Request.QueryString["Mode"] == "TBD")
        {
            string strTotBDJobs = "SELECT VCJM.VesselCode,(SELECT ShipName FROM Settings WHERE ShipCode = VCJM.VesselCode) AS VesselName,CM.ComponentId,CM.ComponentCode,CM.ComponentName ,CM.CriticalType,JM.JobCode,JM.JobName AS JobType,CJM.DescrSh AS JobName,VCJM.CompJobId,JIM.IntervalName ,RM.RankCode,VCJM.Interval,replace(convert(varchar(15),VCJMU.NextDueDate,106),' ','-') As NextDueDate,CASE WHEN VCJMU.NextDueDate > getdate() THEN 'DUE' ELSE 'OVER DUE' END AS DueStatus ,'' AS WorkOrderStatus,VCJMU.NextHour,REPLACE(CONVERT(VARCHAR(15),VCJMU.LastDone,106),' ','-') AS LastDone,VCJMU.LastHour,'' AS Difference,'' AS DiffHour,0 AS PlannedRank,'' AS PlanDate,RM.RankCode AS Rank,Row_Number() OVER(ORDER BY CM.ComponentCode) AS RowNumber, CASE CM.ClassEquip WHEN 'true' THEN 'Class Equipment - ' + VCM.ClassEquipCode ELSE '' END AS ClassEquip,CASE CM.CriticalEquip WHEN 'true' THEN 'Critical Equipment' ELSE '' END AS CriticalEquip FROM VSL_VesselComponentJobMaster VCJM  " +
                                    "INNER JOIN Rank RM ON RM.RankId = VCJM.AssignTo " +
                                    "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId " +
                                    "INNER JOIN  (  " +
                                    "ComponentsJobMapping CJM  " +
                                    "INNER JOIN JobMaster JM  ON JM.JobId = CJM.JobId  " +
                                    "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId  " +
                                    "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCM.ComponentId = CM.ComponentId AND VCM.Status = 'A' " +
                                    ")ON  VCJM.ComponentId = CJM.ComponentId AND VCJM.CompJobId = CJM.CompJobId AND VCJM.Status = 'A' AND VCM.VesselCode = VCJM.VesselCode  " +
                                    "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId  " +
                                    "INNER JOIN VSL_VesselJobUpdateHistory VJH ON VJH.VesselCode = VCJM.VesselCode AND VJH.ComponentId = VCJM.ComponentId AND VJH.CompJobId = VCJM.CompJobId " +
                                    "INNER JOIN Vsl_DefectDetailsMaster VDM ON VDM.VesselCode = VDM.VesselCode AND VDM.HistoryId = VJH.HistoryId ";

            string WhereCondition = "WHERE (VJH.DoneDate BETWEEN DATEADD(dd,-30,getdate()) AND getdate()) AND VDM.HistoryId <> 0 ";

            string FleetId = Session["TBD"].ToString().Split(':').GetValue(0).ToString().Trim();
            string VesselCode = Session["TBD"].ToString().Split(':').GetValue(1).ToString().Trim();
            if (VesselCode.Trim() == "< All >")
            {
                VesselCode = "0";
            }


            if (FleetId.Trim() != "0")
            {
                if (VesselCode.Trim() == "0")
                {
                    WhereCondition = WhereCondition + "AND VDM.VesselCode IN (SELECT VesselCode FROM  dbo.Vessel WHERE FleetId = " + FleetId.Trim() + ")";
                }
                else
                {
                    WhereCondition = WhereCondition + "AND VDM.VesselCode = '" + VesselCode.Trim() + "' ";
                }
            }
            else if (VesselCode.Trim() != "0")
            {
                WhereCondition = WhereCondition + "AND VDM.VesselCode = '" + VesselCode.Trim() + "' ";
            }
            DataTable dtTotBD = Common.Execute_Procedures_Select_ByQuery(strTotBDJobs + WhereCondition + " ORDER BY RankCode ");
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("JobPlanningHomeReport.rpt"));
            rpt.SetDataSource(dtTotBD);
            rpt.SetParameterValue("@Header", "Breakdown Jobs in Last 30 Days");

        }


        if (Request.QueryString["Mode"] == "TJD")
        {
            string strTotJobsDone = "SELECT VCJM.VesselCode,(SELECT ShipName FROM Settings WHERE ShipCode = VCJM.VesselCode) AS VesselName,CM.ComponentId,CM.ComponentCode,CM.ComponentName ,JM.JobCode,JM.JobName AS JobType,CJM.DescrSh AS JobName,VCJM.CompJobId,JIM.IntervalName ,RM.RankCode,VCJM.Interval,replace(convert(varchar(15),VCJMU.NextDueDate,106),' ','-') As NextDueDate,CASE WHEN VCJMU.NextDueDate > getdate() THEN 'DUE' ELSE 'OVER DUE' END AS DueStatus ,'' AS WorkOrderStatus,VCJMU.NextHour,REPLACE(CONVERT(VARCHAR(15),VCJMU.LastDone,106),' ','-') AS LastDone,VCJMU.LastHour,'' AS Difference,'' AS DiffHour,0 AS PlannedRank,'' AS PlanDate,RM.RankCode AS Rank,Row_Number() OVER(ORDER BY CM.ComponentCode) AS RowNumber, CASE CM.ClassEquip WHEN 'true' THEN 'Class Equipment - ' + VCM.ClassEquipCode ELSE '' END AS ClassEquip,CASE CM.CriticalEquip WHEN 'true' THEN 'Critical Equipment' ELSE '' END AS CriticalEquip,VJH.DoneDate,VJH.DoneHour,VJH.DoneBy_Code + ' - ' + VJH.DoneBy_Name AS DoneBy,VJH.ServiceReport FROM VSL_VesselComponentJobMaster VCJM  " +
                                    
                                    "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId " +
                                    "INNER JOIN  (  " +
                                    "ComponentsJobMapping CJM  " +
                                    "INNER JOIN JobMaster JM  ON JM.JobId = CJM.JobId  " +
                                    "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId  " +
                                    "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCM.ComponentId = CM.ComponentId AND VCM.Status = 'A' " +
                                    ")ON  VCJM.ComponentId = CJM.ComponentId AND VCJM.CompJobId = CJM.CompJobId AND VCJM.Status = 'A' AND VCM.VesselCode = VCJM.VesselCode  " +
                                    "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId  " +
                                    "INNER JOIN VSL_VesselJobUpdateHistory VJH ON VJH.VesselCode = VCJM.VesselCode AND VJH.ComponentId = VCJM.ComponentId AND VJH.CompJobId = VCJM.CompJobId  " +
                                    "INNER JOIN Rank RM ON RM.RankId = VJH.DoneBy ";
            
            string WhereCondition = "WHERE VJH.[Action] = 'R' AND (VJH.DoneDate BETWEEN DATEADD(dd,-30,getdate()) AND getdate()) ";

            string FleetId = Session["TJD"].ToString().Split(':').GetValue(0).ToString().Trim();
            string VesselCode = Session["TJD"].ToString().Split(':').GetValue(1).ToString().Trim();
            if (VesselCode.Trim() == "< All >")
            {
                VesselCode = "0";
            }


            if (FleetId.Trim() != "0")
            {
                if (VesselCode.Trim() == "0")
                {
                    WhereCondition = WhereCondition + "AND VJH.VesselCode IN (SELECT VesselCode FROM  dbo.Vessel WHERE FleetId = " + FleetId.Trim() + ")";
                }
                else
                {
                    WhereCondition = WhereCondition + "AND VJH.VesselCode = '" + VesselCode.Trim() + "' ";
                }
            }
            else if (VesselCode.Trim() != "0")
            {
                WhereCondition = WhereCondition + "AND VJH.VesselCode = '" + VesselCode.Trim() + "' ";
            }
            DataTable dtToday = Common.Execute_Procedures_Select_ByQuery(strTotJobsDone + WhereCondition + " ORDER BY RankCode ");
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("JobComplitation.rpt"));
            rpt.SetDataSource(dtToday);
            rpt.SetParameterValue("@Header", "Jobs done in Last 30 Days");

        }

        if (Request.QueryString["Mode"] == "TJDD")
        {
            string strTotJobsDoneOD = "SELECT VCJM.VesselCode,(SELECT ShipName FROM Settings WHERE ShipCode = VCJM.VesselCode) AS VesselName,CM.ComponentId,CM.ComponentCode,CM.ComponentName ,JM.JobCode,JM.JobName AS JobType,CJM.DescrSh AS JobName,VCJM.CompJobId,JIM.IntervalName ,RM.RankCode,VCJM.Interval,replace(convert(varchar(15),VCJMU.NextDueDate,106),' ','-') As NextDueDate,CASE WHEN VCJMU.NextDueDate > getdate() THEN 'DUE' ELSE 'OVER DUE' END AS DueStatus ,'' AS WorkOrderStatus,VCJMU.NextHour,REPLACE(CONVERT(VARCHAR(15),VCJMU.LastDone,106),' ','-') AS LastDone,VCJMU.LastHour,'' AS Difference,'' AS DiffHour,0 AS PlannedRank,'' AS PlanDate,RM.RankCode AS Rank,Row_Number() OVER(ORDER BY CM.ComponentCode) AS RowNumber, CASE CM.ClassEquip WHEN 'true' THEN 'Class Equipment - ' + VCM.ClassEquipCode ELSE '' END AS ClassEquip,CASE CM.CriticalEquip WHEN 'true' THEN 'Critical Equipment' ELSE '' END AS CriticalEquip FROM VSL_VesselComponentJobMaster VCJM  " +

                                      "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId " +
                                      "INNER JOIN  (  " +
                                      "ComponentsJobMapping CJM  " +
                                      "INNER JOIN JobMaster JM  ON JM.JobId = CJM.JobId  " +
                                      "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId  " +
                                      "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCM.ComponentId = CM.ComponentId AND VCM.Status = 'A' " +
                                      ")ON  VCJM.ComponentId = CJM.ComponentId AND VCJM.CompJobId = CJM.CompJobId AND VCJM.Status = 'A' AND VCM.VesselCode = VCJM.VesselCode  " +
                                      "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId  " +
                                      "INNER JOIN VSL_VesselJobUpdateHistory VJH ON VJH.VesselCode = VCJM.VesselCode AND VJH.ComponentId = VCJM.ComponentId AND VJH.CompJobId = VCJM.CompJobId  " +
                                      "INNER JOIN Rank RM ON RM.RankId = VJH.DoneBy ";
           
            string WhereCondition = "WHERE VJH.[Action] = 'R' AND (VJH.DoneDate BETWEEN DATEADD(dd,-30,getdate()) AND getdate())  AND VJH.DoneDate > VJH.NextDueDate ";

            string FleetId = Session["TJDD"].ToString().Split(':').GetValue(0).ToString().Trim();
            string VesselCode = Session["TJDD"].ToString().Split(':').GetValue(1).ToString().Trim();
            if (VesselCode.Trim() == "< All >")
            {
                VesselCode = "0";
            }


            if (FleetId.Trim() != "0")
            {
                if (VesselCode.Trim() == "0")
                {
                    WhereCondition = WhereCondition + "AND VJH.VesselCode IN (SELECT VesselCode FROM  dbo.Vessel WHERE FleetId = " + FleetId.Trim() + ")";
                }
                else
                {
                    WhereCondition = WhereCondition + "AND VJH.VesselCode = '" + VesselCode.Trim() + "' ";
                }
            }
            else if (VesselCode.Trim() != "0")
            {
                WhereCondition = WhereCondition + "AND VJH.VesselCode = '" + VesselCode.Trim() + "' ";
            }
            DataTable dtToday = Common.Execute_Procedures_Select_ByQuery(strTotJobsDoneOD + WhereCondition + " ORDER BY RankCode ");
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("JobPlanningHomeReport.rpt"));
            rpt.SetDataSource(dtToday);
            rpt.SetParameterValue("@Header", "Jobs done after Due Date in Last 30 Days");

        } 


        #endregion
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
