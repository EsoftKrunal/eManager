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


public partial class ShipHome : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        Session["CurrentModule"] = 0;
        LoadCritical();
        LoadCommon();
        LoadNoNCritical();
    }
    public void LoadCritical()
    {
        string Today=DateTime.Today.ToString("dd-MMM-yyyy");
        string CalcDay = "";
        int CriticalDue=Common.CastAsInt32(txtCritical_Due.Text);
        int CriticalPlan = Common.CastAsInt32(txtCritical_Plan.Text);
        int CriticalDone = Common.CastAsInt32(txtCritical_Done.Text);
        // job due
        CalcDay = DateTime.Today.AddDays(CriticalDue + 1).ToString("dd-MMM-yyyy");
        string strCDue =   "SELECT COUNT(*) FROM VSL_VesselComponentJobMaster VCJM " +
                           "INNER JOIN Rank RM ON RM.RankId = VCJM.AssignTo  AND VCJM.Status = 'A' " +
                           "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId " +
                           "INNER JOIN  ( " +
                           "ComponentsJobMapping CJM " +
                           "INNER JOIN JobMaster JM  ON JM.JobId = CJM.JobId " +
                           "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId AND CriticalEquip=1 " +
                           "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCM.ComponentId = CM.ComponentId AND VCM.Status = 'A' " +
                           ") ON  VCJM.ComponentId = CJM.ComponentId AND VCJM.CompJobId = CJM.CompJobId AND VCJM.Status = 'A' AND VCM.VesselCode = VCJM.VesselCode " +
                           "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId " +
                           "LEFT JOIN VSL_PlanMaster PM ON PM.VesselCode = VCJM.VesselCode AND PM.ComponentId = VCJM.ComponentId AND PM.CompJobId = VCJM.CompJobId " +
                           "WHERE VCJMU.VesselCode = '" + Session["CurrentShip"].ToString() + "' AND dbo.getDatePart(VCJMU.NextDueDate) between '" + Today + "' AND '" + CalcDay + "' ";
        
        DataTable dtCDue = Common.Execute_Procedures_Select_ByQuery(strCDue);
        if (dtCDue.Rows.Count > 0)
        {
            lnkCritical_Due.Text = dtCDue.Rows[0][0].ToString();
        }
        // job planning
        CalcDay = DateTime.Today.AddDays(CriticalPlan + 1).ToString("dd-MMM-yyyy");

        string strNextMonth = "SELECT COUNT(*) FROM VSL_VesselComponentJobMaster VCJM " +
                               "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId  AND VCJM.Status = 'A' " +
                               "INNER JOIN  ( " +
                               "ComponentsJobMapping CJM " +
                               "INNER JOIN JobMaster JM  ON JM.JobId = CJM.JobId " +
                               "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId AND CriticalEquip=1 " +
                               "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCM.ComponentId = CM.ComponentId AND VCM.Status = 'A' " +
                               ") ON  VCJM.ComponentId = CJM.ComponentId AND VCJM.CompJobId = CJM.CompJobId AND VCJM.Status = 'A' AND VCM.VesselCode = VCJM.VesselCode " +
                               "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId " +
                               "INNER JOIN VSL_PlanMaster PM ON PM.VesselCode = VCJM.VesselCode AND PM.ComponentId = VCJM.ComponentId AND PM.CompJobId = VCJM.CompJobId " +
                               "INNER JOIN Rank RM ON RM.RankId = PM.AssignedTo " +
                               "WHERE VCJMU.VesselCode = '" + Session["CurrentShip"].ToString() + "' AND dbo.getDatePart(PM.PlanDate) BETWEEN '" + Today + "' AND '" + CalcDay + "' ";

        DataTable dtNextMonth = Common.Execute_Procedures_Select_ByQuery(strNextMonth);
        if (dtNextMonth.Rows.Count > 0)
        {
            lnkCritical_Plan.Text = dtNextMonth.Rows[0][0].ToString();
        }
        // job done
        CalcDay = DateTime.Today.AddDays(-CriticalDone).ToString("dd-MMM-yyyy");

        string strTotJobsDone = "SELECT COUNT(*) FROM VSL_VesselComponentJobMaster VCJM  " +
                                "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId  AND VCJM.Status = 'A' " +
                                "INNER JOIN  (  " +
                                "ComponentsJobMapping CJM  " +
                                "INNER JOIN JobMaster JM  ON JM.JobId = CJM.JobId  " +
                                "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId AND CriticalEquip=1 " +
                                "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCM.ComponentId = CM.ComponentId AND VCM.Status = 'A' " +
                                ")ON  VCJM.ComponentId = CJM.ComponentId AND VCJM.CompJobId = CJM.CompJobId AND VCJM.Status = 'A' AND VCM.VesselCode = VCJM.VesselCode  " +
                                "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId  " +
                                "INNER JOIN VSL_VesselJobUpdateHistory VJH ON VJH.VesselCode = VCJM.VesselCode AND VJH.ComponentId = VCJM.ComponentId AND VJH.CompJobId = VCJM.CompJobId  " +
                                "INNER JOIN Rank RM ON RM.RankId = VJH.DoneBy " +
                                "WHERE VCJMU.VesselCode = '" + Session["CurrentShip"].ToString() + "' AND [Action] = 'R' AND dbo.getDatePart(VJH.DoneDate) BETWEEN '" + CalcDay + "' AND '" + Today + "' ";

        DataTable dtTotJobsDone = Common.Execute_Procedures_Select_ByQuery(strTotJobsDone);
        if (dtTotJobsDone.Rows.Count > 0)
        {
            lnkCritical_Done.Text = dtTotJobsDone.Rows[0][0].ToString();
        }
    }
    public void LoadCommon()
    {
        // pending for verification
        //string strUnverified = "SELECT COUNT(*) FROM VSL_VesselJobUpdateHistory WHERE VesselCode = '" + Session["CurrentShip"].ToString() + "' AND ISNULL(Verified,0)= 0";
        string strUnverified = "SELECT COUNT(*) FROM vw_GetJobUpdateDataByPeriod WHERE VesselCode = '" + Session["CurrentShip"].ToString() + "' AND ISNULL(Verified,0)= 0 AND [Action] <> 'Postponed' ";
        DataTable dtVerify = Common.Execute_Procedures_Select_ByQuery(strUnverified);
        if (dtVerify.Rows.Count > 0)
        {
            lnk_Verify.Text = dtVerify.Rows[0][0].ToString();
        }


        string strATTACH =   "SELECT COUNT(HISTORYID) " +
                        "FROM " +
                        "( " +
                        "SELECT P.VESSELCODE,p.historyid, " +
                        "(select top 1 CreatedBy from tbl_Vessel_Communication C WHERE C.VesselCode=P.VesselCode AND C.RecordId=P.HistoryId AND C.RecordType='JOBHISTORY-ATTACHMENTS' ORDER BY TABLEID DESC) AS AttachmentCreatedBy, " +
                        "(select top 1 CreatedOn from tbl_Vessel_Communication C WHERE C.VesselCode=P.VesselCode AND C.RecordId=P.HistoryId AND C.RecordType='JOBHISTORY-ATTACHMENTS' ORDER BY TABLEID DESC) AS AttachmentCreatedOn " +
                        "FROM vw_GetJobUpdateDataByPeriod P WHERE (SELECT COUNT(*) FROM [dbo].[VSL_VesselJobUpdateHistoryAttachments] AT WHERE AT.HistoryId=P.HistoryId AND AT.VesselCode=P.VesselCode) >0" +
                        ") A WHERE A.VESSELCODE='" + Session["CurrentShip"].ToString() + "' AND AttachmentCreatedOn IS NULL ";

        DataTable dtaTTACHMENT = Common.Execute_Procedures_Select_ByQuery(strATTACH);
        if (dtaTTACHMENT.Rows.Count > 0)
        {
            lnk_Documents.Text = dtaTTACHMENT.Rows[0][0].ToString();
        }

        //---- post pone
        string strpostpone = "SELECT count(*) FROM [vw_PostPoneJobs] WHERE VesselCode='" + Session["CurrentShip"].ToString() + "' and ApprovalStatus='Postpone Requested'";

        DataTable dtpostpone = Common.Execute_Procedures_Select_ByQuery(strpostpone);
        if (dtpostpone.Rows.Count > 0)
        {
            lnk_Postpone.Text = dtpostpone.Rows[0][0].ToString();
        }

        // -- critical component shutdown request
        string strcompshutdown = "SELECT count(*) from vw_VSL_CriticalEquipShutdownRequest WHERE VesselCode='" + Session["CurrentShip"].ToString() + "' AND ApprovalStatus=0";

        DataTable dtcompshutdown = Common.Execute_Procedures_Select_ByQuery(strcompshutdown);
        lnk_CriticalShutdown.Text = dtcompshutdown.Rows[0][0].ToString();
        
    }
    public void LoadNoNCritical()
    {
        string Today = DateTime.Today.ToString("dd-MMM-yyyy");
        string CalcDay = "";
        int NoNCriticalDue = Common.CastAsInt32(txtNoNCritical_Due.Text);
        int NoNCriticalPlan = Common.CastAsInt32(txtNoNCritical_Plan.Text);
        int NoNCriticalPostponed = Common.CastAsInt32(txtNoNCritical_Postponed.Text);
        int NoNCriticalBreak_Up = Common.CastAsInt32(txtNoNCritical_B_UP.Text);
        int NoNCriticalDone = Common.CastAsInt32(txtNoNCritical_Done.Text);
        int NoNCriticalDoneAfterDue = Common.CastAsInt32(txtNoNCritical_DoneAfterDue.Text);
        int NoNCriticalDefectDue = Common.CastAsInt32(txtNoNCritical_DefectDue.Text);

        // job due
        CalcDay = DateTime.Today.AddDays(NoNCriticalDue + 1).ToString("dd-MMM-yyyy");

        string strDue = "SELECT COUNT(*) FROM VSL_VesselComponentJobMaster VCJM " +
                           "INNER JOIN Rank RM ON RM.RankId = VCJM.AssignTo  AND VCJM.Status = 'A' " +
                           "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId " +
                           "INNER JOIN  ( " +
                           "ComponentsJobMapping CJM " +
                           "INNER JOIN JobMaster JM  ON JM.JobId = CJM.JobId " +
                           "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId AND CriticalEquip<>1 " +
                           "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCM.ComponentId = CM.ComponentId AND VCM.Status = 'A' " +
                           ") ON  VCJM.ComponentId = CJM.ComponentId AND VCJM.CompJobId = CJM.CompJobId AND VCJM.Status = 'A' AND VCM.VesselCode = VCJM.VesselCode " +
                           "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId " +
                           "LEFT JOIN VSL_PlanMaster PM ON PM.VesselCode = VCJM.VesselCode AND PM.ComponentId = VCJM.ComponentId AND PM.CompJobId = VCJM.CompJobId " +
                           "WHERE VCJMU.VesselCode = '" + Session["CurrentShip"].ToString() + "' AND dbo.getDatePart(VCJMU.NextDueDate) between '" + Today + "' AND '" + CalcDay + "' ";

        DataTable dtDue = Common.Execute_Procedures_Select_ByQuery(strDue);
        if (dtDue.Rows.Count > 0)
        {
            lnkNoNCritical_Due.Text = dtDue.Rows[0][0].ToString();
        }

        // job over due
        //string strODue = "SELECT COUNT(*) FROM VSL_VesselComponentJobMaster VCJM " +
        //                  "INNER JOIN Rank RM ON RM.RankId = VCJM.AssignTo " +
        //                  "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId " +
        //                  "INNER JOIN  ( " +
        //                  "ComponentsJobMapping CJM " +
        //                  "INNER JOIN JobMaster JM  ON JM.JobId = CJM.JobId " +
        //                  "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId AND CriticalEquip<>1 " +
        //                  "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCM.ComponentId = CM.ComponentId AND VCM.Status = 'A' " +
        //                  ") ON  VCJM.ComponentId = CJM.ComponentId AND VCJM.CompJobId = CJM.CompJobId AND VCJM.Status = 'A' AND VCM.VesselCode = VCJM.VesselCode " +
        //                  "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId " +
        //                  "LEFT JOIN VSL_PlanMaster PM ON PM.VesselCode = VCJM.VesselCode AND PM.ComponentId = VCJM.ComponentId AND PM.CompJobId = VCJM.CompJobId " +
        //                  "WHERE VCJMU.VesselCode = '" + Session["CurrentShip"].ToString() + "' AND dbo.getDatePart(VCJMU.NextDueDate) < '" + Today + "'";

        string strODue = "SELECT COUNT(*) FROM VSL_VesselComponentJobMaster VCJM " +
                         "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId  AND VCJM.Status = 'A'  " +
                         "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCJM.VesselCode = VCM.VesselCode AND VCJM.ComponentId = VCM.ComponentId AND VCM.Status = 'A' " +
                         "INNER JOIN ComponentsJobMapping CJM ON VCJM.CompjobId=CJM.CompjobId  " +
                         "INNER JOIN ComponentMaster CM ON CM.ComponentId = VCJM.ComponentId AND CriticalEquip<>1 " +
                         "INNER JOIN JobMaster JM  ON JM.JobId = VCJM.JobId  " +
                         "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId  " +
                         "LEFT JOIN VSL_PlanMaster PM ON PM.VesselCode = VCJM.VesselCode AND PM.ComponentId = VCJM.ComponentId AND PM.CompJobId = VCJM.CompJobId  " +
                         "LEFT JOIN Rank RM ON RM.RankId = PM.AssignedTo " +
                          "WHERE VCJMU.VesselCode = '" + Session["CurrentShip"].ToString() + "' AND ( VCJMU.NextDueDate < CONVERT(SMALLDATETIME,CONVERT(VARCHAR,GETDATE())) OR VCJMU.NextDueDate IS NULL) ";


        DataTable dtODue = Common.Execute_Procedures_Select_ByQuery(strODue);
        if (dtODue.Rows.Count > 0)
        {
            lnkNoNCritical_OverDue.Text = dtODue.Rows[0][0].ToString();
        }

        // job planning
        CalcDay = DateTime.Today.AddDays(NoNCriticalPlan + 1).ToString("dd-MMM-yyyy");
        string strNextMonth = "SELECT COUNT(*) FROM VSL_VesselComponentJobMaster VCJM " +
                            "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId  AND VCJM.Status = 'A' " +
                            "INNER JOIN  ( " +
                            "ComponentsJobMapping CJM " +
                            "INNER JOIN JobMaster JM  ON JM.JobId = CJM.JobId " +
                            "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId AND CriticalEquip<>1 " +
                            "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCM.ComponentId = CM.ComponentId AND VCM.Status = 'A' " +
                            ") ON  VCJM.ComponentId = CJM.ComponentId AND VCJM.CompJobId = CJM.CompJobId AND VCJM.Status = 'A' AND VCM.VesselCode = VCJM.VesselCode " +
                            "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId " +
                            "INNER JOIN VSL_PlanMaster PM ON PM.VesselCode = VCJM.VesselCode AND PM.ComponentId = VCJM.ComponentId AND PM.CompJobId = VCJM.CompJobId " +
                            "INNER JOIN Rank RM ON RM.RankId = PM.AssignedTo " +
                            "WHERE VCJMU.VesselCode = '" + Session["CurrentShip"].ToString() + "' AND dbo.getDatePart(PM.PlanDate) BETWEEN '" + Today + "' AND '" + CalcDay + "' ";

        DataTable dtNextMonth = Common.Execute_Procedures_Select_ByQuery(strNextMonth);
        if (dtNextMonth.Rows.Count > 0)
        {
            lnkNoNCritical_Plan.Text = dtNextMonth.Rows[0][0].ToString();
        }

        // jobs postponed
        CalcDay = DateTime.Today.AddDays(-NoNCriticalPostponed).ToString("dd-MMM-yyyy");
        string strTotJobsPP = "SELECT COUNT(*) FROM VSL_VesselComponentJobMaster VCJM  " +
                        "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId  AND VCJM.Status = 'A' " +
                        "INNER JOIN  (  " +
                        "ComponentsJobMapping CJM  " +
                        "INNER JOIN JobMaster JM  ON JM.JobId = CJM.JobId  " +
                        "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId AND CriticalEquip<>1 " +
                        "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCM.ComponentId = CM.ComponentId AND VCM.Status = 'A' " +
                        ")ON  VCJM.ComponentId = CJM.ComponentId AND VCJM.CompJobId = CJM.CompJobId AND VCJM.Status = 'A' AND VCM.VesselCode = VCJM.VesselCode  " +
                        "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId  " +
                        "INNER JOIN VSL_VesselJobUpdateHistory VJH ON VJH.VesselCode = VCJM.VesselCode AND VJH.ComponentId = VCJM.ComponentId AND VJH.CompJobId = VCJM.CompJobId  " +
                        "INNER JOIN Rank RM ON RM.RankId = VJH.DoneBy " +
                        "WHERE VCJMU.VesselCode = '" + Session["CurrentShip"].ToString() + "' AND [Action] = 'P' AND dbo.getDatePart(VJH.PostPoneDate) BETWEEN '" + CalcDay + "' AND '" + Today + "' ";

        DataTable dtPP = Common.Execute_Procedures_Select_ByQuery(strTotJobsPP);
        if (dtPP.Rows.Count > 0)
        {
            lnkNoNCritical_Postponed.Text = dtPP.Rows[0][0].ToString();
        }

        // jobs break & unplanned
        CalcDay = DateTime.Today.AddDays(-NoNCriticalBreak_Up).ToString("dd-MMM-yyyy");
        string sqlbup = "SELECT COUNT(*) " +
                        "+ (select COUNT(*) from VSL_UnPlannedJobs WHERE VESSELCODE='" + Session["CurrentShip"].ToString() + "' AND DONEDATE BETWEEN '" + CalcDay + "' AND '" + Today + "' ) " +
                        "FROM Vsl_DefectDetailsMaster WHERE HistoryId <> 0 AND VESSELCODE='" + Session["CurrentShip"].ToString() + "' AND REPORTDT BETWEEN '" + CalcDay + "' AND '" + Today + "'";
        DataTable dtBUP = Common.Execute_Procedures_Select_ByQuery(sqlbup);
        if (dtBUP.Rows.Count > 0)
        {
            lnkNoNCritical_B_UP.Text = dtBUP.Rows[0][0].ToString();
        }

        // jobs done
        CalcDay = DateTime.Today.AddDays(-NoNCriticalDone).ToString("dd-MMM-yyyy");
        string strTotJobsDone = "SELECT COUNT(*) FROM VSL_VesselComponentJobMaster VCJM  " +
                            "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId  AND VCJM.Status = 'A' " +
                            "INNER JOIN  (  " +
                            "ComponentsJobMapping CJM  " +
                            "INNER JOIN JobMaster JM  ON JM.JobId = CJM.JobId  " +
                            "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId AND CriticalEquip<>1 " +
                            "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCM.ComponentId = CM.ComponentId AND VCM.Status = 'A' " +
                            ")ON  VCJM.ComponentId = CJM.ComponentId AND VCJM.CompJobId = CJM.CompJobId AND VCJM.Status = 'A' AND VCM.VesselCode = VCJM.VesselCode  " +
                            "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId  " +
                            "INNER JOIN VSL_VesselJobUpdateHistory VJH ON VJH.VesselCode = VCJM.VesselCode AND VJH.ComponentId = VCJM.ComponentId AND VJH.CompJobId = VCJM.CompJobId  " +
                            "INNER JOIN Rank RM ON RM.RankId = VJH.DoneBy " +
                            "WHERE VCJMU.VesselCode = '" + Session["CurrentShip"].ToString() + "' AND [Action] = 'R' AND dbo.getDatePart(VJH.DoneDate) BETWEEN '" + CalcDay + "' AND '" + Today + "' ";

        DataTable dtTotJobsDone = Common.Execute_Procedures_Select_ByQuery(strTotJobsDone);
        if (dtTotJobsDone.Rows.Count > 0)
        {
            lnkNoNCritical_Done.Text = dtTotJobsDone.Rows[0][0].ToString();
        }

        // jobs done after due 
        CalcDay = DateTime.Today.AddDays(-NoNCriticalDoneAfterDue).ToString("dd-MMM-yyyy");
        string strTotJobsDoneAfterDue = "SELECT COUNT(*) FROM VSL_VesselComponentJobMaster VCJM  " +
                       "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId  AND VCJM.Status = 'A' " +
                       "INNER JOIN  (  " +
                       "ComponentsJobMapping CJM  " +
                       "INNER JOIN JobMaster JM  ON JM.JobId = CJM.JobId  " +
                       "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId AND CriticalEquip<>1 " +
                       "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCM.ComponentId = CM.ComponentId AND VCM.Status = 'A' " +
                       ")ON  VCJM.ComponentId = CJM.ComponentId AND VCJM.CompJobId = CJM.CompJobId AND VCJM.Status = 'A' AND VCM.VesselCode = VCJM.VesselCode  " +
                       "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId  " +
                       "INNER JOIN VSL_VesselJobUpdateHistory VJH ON VJH.VesselCode = VCJM.VesselCode AND VJH.ComponentId = VCJM.ComponentId AND VJH.CompJobId = VCJM.CompJobId  " +
                       "INNER JOIN Rank RM ON RM.RankId = VJH.DoneBy " +
                       "WHERE VCJMU.VesselCode = '" + Session["CurrentShip"].ToString() + "' AND [Action] = 'R' AND dbo.getDatePart(VJH.DoneDate) BETWEEN '" + CalcDay + "' AND '" + Today + "' AND VJH.DoneDate > VJH.Lastduedate";

        DataTable dtTotJobsDoneAfterDue = Common.Execute_Procedures_Select_ByQuery(strTotJobsDoneAfterDue);
        if (dtTotJobsDoneAfterDue.Rows.Count > 0)
        {
            lnkNoNCritical_DoneAfterDue.Text = dtTotJobsDoneAfterDue.Rows[0][0].ToString();
        }

        // due job defect in 

        CalcDay = DateTime.Today.AddDays(NoNCriticalDefectDue).ToString("dd-MMM-yyyy");
        string strTotJobsDefectDue = "SELECT COUNT(*) from dbo.vw_DefectJobs WHERE COMPLETIONDT IS NULL " +
                                     "AND VesselCode = '" + Session["CurrentShip"].ToString() + "' AND dbo.getDatePart(TARGETDT) BETWEEN '" + Today + "' AND '" + CalcDay + "'";

        DataTable dtTotJobsDefectDue = Common.Execute_Procedures_Select_ByQuery(strTotJobsDefectDue);
        if (dtTotJobsDefectDue.Rows.Count > 0)
        {
            lnkNoNCritical_DefectDue.Text = dtTotJobsDefectDue.Rows[0][0].ToString();
        }
    }
    
    protected void lnkCritical_Due_Click(object sender, EventArgs e)
    {
        if (lnkCritical_Due.Text.ToString() != "0")
        {
            Session.Add("CPARAM", "0" + ":" + Session["CurrentShip"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "openreport('C_DUE','" + txtCritical_Due.Text + "');", true);
        }

    }
    protected void lnkCritical_Plan_Click(object sender, EventArgs e)
    {
        if (lnkCritical_Plan.Text.ToString() != "0")
        {
            Session.Add("CPARAM", "0" + ":" + Session["CurrentShip"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "openreport('C_PLAN','" + txtCritical_Plan.Text + "');", true);
        }

    }
    protected void lnkCritical_Done_Click(object sender, EventArgs e)
    {
        if (lnkCritical_Done.Text.ToString() != "0")
        {
            Session.Add("CPARAM", "0" + ":" + Session["CurrentShip"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "openreport('C_DONE','" + txtCritical_Done.Text + "');", true);
        }

    }

    protected void lnk_Verify_Click(object sender, EventArgs e)
    {
        if (lnk_Verify.Text.ToString() != "0")
        {
            Session.Add("C_VERIFY", "0" + ":" + Session["CurrentShip"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "window.open('JobDoneBetweenPeriod.aspx');", true);
        }

    }
    protected void lnk_CriticalShutdown_Click(object sender, EventArgs e)
    {
        //if (lnk_Verify.Text.ToString() != "0")
        //{
            Session.Add("C_VERIFY", "0" + ":" + Session["CurrentShip"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "window.open('CriticalComponentShutdownRequest_VSL.aspx');", true);
        //}

    }

    protected void lnkNoNCritical_Due_Click(object sender, EventArgs e)
    {
        if (lnkNoNCritical_Due.Text.ToString() != "0")
        {
            Session.Add("PARAM", "0" + ":" + Session["CurrentShip"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "openreport('DUE','" + txtNoNCritical_Due.Text + "');", true);
        }

    }
    protected void lnkNoNCritical_OverDue_Click(object sender, EventArgs e)
    {
        if (lnkNoNCritical_OverDue.Text.ToString() != "0")
        {
            Session.Add("PARAM", "0" + ":" + Session["CurrentShip"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "openreport('OVERDUE','0');", true);
        }

    }
    protected void lnkNoNCritical_Plan_Click(object sender, EventArgs e)
    {
        if (lnkNoNCritical_Plan.Text.ToString() != "0")
        {
            Session.Add("PARAM", "0" + ":" + Session["CurrentShip"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "openreport('PLAN','" + txtNoNCritical_Plan.Text + "');", true);
        }

    }
    protected void lnkNoNCritical_Postponed_Click(object sender, EventArgs e)
    {
        if (lnkNoNCritical_Postponed.Text.ToString() != "0")
        {
            Session.Add("PARAM", "0" + ":" + Session["CurrentShip"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "openreport('POSTPONED','" + txtNoNCritical_Postponed.Text + "');", true);
        }
    }
    protected void lnkNoNCritical_B_UP_Click(object sender, EventArgs e)
    {
        //if (lnkNoNCritical_B_UP.Text.ToString() != "0")
        //{
        //    Session.Add("PARAM", "0" + ":" + Session["CurrentShip"].ToString());
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "openreport('B_UP','" + txtNoNCritical_B_UP.Text + "');", true);
        //}
    }
    protected void lnkNoNCritical_Done_Click(object sender, EventArgs e)
    {
        if (lnkNoNCritical_Done.Text.ToString() != "0")
        {
            Session.Add("PARAM", "0" + ":" + Session["CurrentShip"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "openreport('DONE','" + txtNoNCritical_Done.Text + "');", true);
        }

    }
    protected void lnkNoNCritical_DoneAfterDue_Click(object sender, EventArgs e)
    {
        if (lnkNoNCritical_DoneAfterDue.Text.ToString() != "0")
        {
            Session.Add("PARAM", "0" + ":" + Session["CurrentShip"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "openreport('DONEAFTERDUE','" + txtNoNCritical_DoneAfterDue.Text + "');", true);
        }

    }
    protected void lnkNoNCritical_DefectDue_Click(object sender, EventArgs e)
    {
        if (lnkNoNCritical_DefectDue.Text.ToString() != "0")
        {
            Session.Add("PARAM", "0" + ":" + Session["CurrentShip"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "openreport('DEFECTDUE','" + txtNoNCritical_DefectDue.Text + "');", true);
        }
    }

    protected void Update_Critical(object sender, EventArgs e)
    {
        LoadCritical(); 
    }
    protected void Update_NonCritical(object sender, EventArgs e)
    {
        LoadNoNCritical();
    }

    protected void lnk_Documents_Click(object sender, EventArgs e)
    {
        //if (lnk_Documents.Text.ToString() != "0")
        //{
            Session.Add("PARAM", "0" + ":" + Session["CurrentShip"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "trq", "OpenAttachment();", true);
        //}

    }
    protected void lnk_Postpone_Click(object sender, EventArgs e)
    {
        //if (lnk_Documents.Text.ToString() != "0")
        //{
            Session.Add("PARAM", "0" + ":" + Session["CurrentShip"].ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr22", "OpenPostPone();", true);
        //}

    }

    
}
