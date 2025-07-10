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

public partial class Modules_PMS_Dashboard : System.Web.UI.Page
{
    System.Drawing.Font fnt_ChartHeading = new System.Drawing.Font("Verdana", 13, System.Drawing.FontStyle.Bold);
    //int indiaid = 70;
    //int myanmarid = 101;
    //int philid = 116;
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        Session["CurrentModule"] = 0;
        if (!Page.IsPostBack)
        {
           //ddlTypesOfJobs.SelectedIndex = 0;
            BindFleet();
            BindVessels();
            ddlVessels.SelectedIndex = 1;         
            GetCount();          
            if (ddlVessels.SelectedValue != "0")
            {
                divChart.Visible = true;
                ShowReport(ddlVessels.SelectedValue);
            }
            else
            {
                divChart.Visible = false;
            }
        }
    }

    public void BindFleet()
    {
        try
        {
            DataTable dtFleet = Common.Execute_Procedures_Select_ByQuery("SELECT FleetId,FleetName as Name FROM dbo.FleetMaster");
            this.ddlFleet.DataSource = dtFleet;
            this.ddlFleet.DataValueField = "FleetId";
            this.ddlFleet.DataTextField = "Name";
            this.ddlFleet.DataBind();
            ddlFleet.Items.Insert(0, new ListItem("< All >", "0"));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BindVessels()
    {
        DataTable dtVessels = new DataTable();
        string strvessels = "SELECT VesselId,VesselCode,VesselName FROM dbo.Vessel VM WHERE VESSELSTATUSID=1 AND EXISTS(SELECT TOP 1 COMPONENTID FROM  VSL_ComponentMasterForVessel EVM WHERE VM.VesselCode = EVM.VesselCode ) ORDER BY VesselName";
        //string strvessels = "SELECT VesselId,VesselCode,VesselName FROM dbo.Vessel WHERE ISNULL(IsExported,0) = 1 ORDER BY VesselName";
        dtVessels = Common.Execute_Procedures_Select_ByQuery(strvessels);
        if (dtVessels.Rows.Count > 0)
        {
            ddlVessels.DataSource = dtVessels;
            ddlVessels.DataTextField = "VesselName";
            ddlVessels.DataValueField = "VesselCode";
            ddlVessels.DataBind();
            
        }
        else
        {
            ddlVessels.DataSource = null;
            ddlVessels.DataBind();
        }
        ddlVessels.Items.Insert(0, "< Select >");
    }

    protected void ddlTypesOfJobs_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlVessels.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Please select Vessel first.')", true);
            ddlVessels.Focus();
            return;
        }
        //if (Convert.ToInt32(ddlTypesOfJobs.SelectedValue) > 0)
        //{
            GetCount();
       // }
        //else
        //{
        //    divCritical.Visible = false;
        //    divMaintenance.Visible = false;
        //    divNonCritical.Visible = false;
        //}
        if (ddlVessels.SelectedValue != "0")
        {
            divChart.Visible = true;
            ShowReport(ddlVessels.SelectedValue);
        }
        else
        {
            divChart.Visible = false;
        }
    }

    protected void ddlVessels_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlVessels.SelectedIndex == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "alert('Please select Vessel first.')", true);
            ddlVessels.Focus();
            return;
        }
        //if (Convert.ToInt32(ddlTypesOfJobs.SelectedValue) > 0)
        //{
            GetCount();     
       // }
        if (ddlVessels.SelectedValue != "0")
        {
            divChart.Visible = true;
            ShowReport(ddlVessels.SelectedValue);
        }
        else
        {
            divChart.Visible = false;
        }
    }
    public void LoadCritical()
    {
        lnkCritical_Due.Text = "0";
        lnkCritical_Plan.Text = "0";
        lnkCritical_Done.Text = "0";
        lnkCritical_Overdue.Text = "0";

        string Today = DateTime.Today.ToString("dd-MMM-yyyy");
        string CalcDay = "";
        int CriticalDue = Common.CastAsInt32(txtCritical_Due.Text);
        int CriticalPlan = Common.CastAsInt32(txtCritical_Plan.Text);
        int CriticalDone = Common.CastAsInt32(txtCritical_Done.Text);

        string ShipWhereClause = "";
        if (ddlFleet.SelectedIndex != 0)
        {
            //if (ddlVessels.SelectedIndex == 0)
            //{
            //    string Vessels = "";
            //    for (int i = 0; i < ddlVessels.Items.Count; i++)
            //    {
            //        if (i != 0)
            //        {
            //            Vessels = Vessels + "'" + ddlVessels.Items[i].Text + "'" + ",";
            //        }
            //    }
            //    if (Vessels.Length > 0)
            //    {
            //        Vessels = Vessels.Remove(Vessels.Length - 1);
            //    }
            //    ShipWhereClause = ShipWhereClause + "AND VCJM.VesselCode IN (SELECT VesselCode FROM  dbo.Vessel WHERE FleetId = " + ddlFleet.SelectedValue + ")";

            //}
            //else
            if (ddlVessels.SelectedIndex > 0 )
            {
                ShipWhereClause = ShipWhereClause + "AND VCJM.VesselCode = '" + ddlVessels.SelectedValue.ToString() + "' ";
            }
        }
        else if (ddlVessels.SelectedIndex != 0)
        {
            ShipWhereClause = ShipWhereClause + "AND VCJM.VesselCode = '" + ddlVessels.SelectedValue.ToString() + "' ";
        }

        // job due
        CalcDay = DateTime.Today.AddDays(CriticalDue).ToString("dd-MMM-yyyy");
        string strCDue = "SELECT COUNT(*) FROM VSL_VesselComponentJobMaster VCJM " +
                           "INNER JOIN Rank RM ON RM.RankId = VCJM.AssignTo AND VCJM.Status = 'A' " +
                           "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId " +
                           "INNER JOIN  ( " +
                           "ComponentsJobMapping CJM " +
                           "INNER JOIN JobMaster JM  ON JM.JobId = CJM.JobId " +
                           "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId AND CriticalEquip=1 " +
                           "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCM.ComponentId = CM.ComponentId AND VCM.Status = 'A' " +
                           ") ON  VCJM.ComponentId = CJM.ComponentId AND VCJM.CompJobId = CJM.CompJobId AND VCJM.Status = 'A' AND VCM.VesselCode = VCJM.VesselCode " +
                           "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId " +
                           "LEFT JOIN VSL_PlanMaster PM ON PM.VesselCode = VCJM.VesselCode AND PM.ComponentId = VCJM.ComponentId AND PM.CompJobId = VCJM.CompJobId " +
                           "WHERE 1=1 " + ShipWhereClause + " AND dbo.getDatePart(VCJMU.NextDueDate) between '" + Today + "' AND '" + CalcDay + "' ";

        DataTable dtCDue = Common.Execute_Procedures_Select_ByQuery(strCDue);
        if (dtCDue.Rows.Count > 0)
        {
            lnkCritical_Due.Text = dtCDue.Rows[0][0].ToString();
        }
        // job planning
        CalcDay = DateTime.Today.AddDays(CriticalPlan + 1).ToString("dd-MMM-yyyy");

        string strNextMonth = "SELECT COUNT(*) FROM VSL_VesselComponentJobMaster VCJM " +
                               "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId AND VCJM.Status = 'A' " +
                               "INNER JOIN  ( " +
                               "ComponentsJobMapping CJM " +
                               "INNER JOIN JobMaster JM  ON JM.JobId = CJM.JobId " +
                               "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId AND CriticalEquip=1 " +
                               "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCM.ComponentId = CM.ComponentId AND VCM.Status = 'A' " +
                               ") ON  VCJM.ComponentId = CJM.ComponentId AND VCJM.CompJobId = CJM.CompJobId AND VCJM.Status = 'A' AND VCM.VesselCode = VCJM.VesselCode " +
                               "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId " +
                               "INNER JOIN VSL_PlanMaster PM ON PM.VesselCode = VCJM.VesselCode AND PM.ComponentId = VCJM.ComponentId AND PM.CompJobId = VCJM.CompJobId " +
                               "INNER JOIN Rank RM ON RM.RankId = PM.AssignedTo " +
                               "WHERE 1=1 " + ShipWhereClause + "  AND dbo.getDatePart(PM.PlanDate) BETWEEN '" + Today + "' AND '" + CalcDay + "' ";

        DataTable dtNextMonth = Common.Execute_Procedures_Select_ByQuery(strNextMonth);
        if (dtNextMonth.Rows.Count > 0)
        {
            lnkCritical_Plan.Text = dtNextMonth.Rows[0][0].ToString();
        }
        // job done
        CalcDay = DateTime.Today.AddDays(-CriticalDone).ToString("dd-MMM-yyyy");

        string strTotJobsDone = "SELECT COUNT(*) FROM VSL_VesselComponentJobMaster VCJM  " +
                                "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId AND VCJM.Status = 'A' " +
                                "INNER JOIN  (  " +
                                "ComponentsJobMapping CJM  " +
                                "INNER JOIN JobMaster JM  ON JM.JobId = CJM.JobId  " +
                                "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId AND CriticalEquip=1 " +
                                "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCM.ComponentId = CM.ComponentId AND VCM.Status = 'A' " +
                                ")ON  VCJM.ComponentId = CJM.ComponentId AND VCJM.CompJobId = CJM.CompJobId AND VCJM.Status = 'A' AND VCM.VesselCode = VCJM.VesselCode  " +
                                "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId  " +
                                "INNER JOIN VSL_VesselJobUpdateHistory VJH ON VJH.VesselCode = VCJM.VesselCode AND VJH.ComponentId = VCJM.ComponentId AND VJH.CompJobId = VCJM.CompJobId  " +
                                "INNER JOIN Rank RM ON RM.RankId = VJH.DoneBy " +
                                "WHERE 1=1 " + ShipWhereClause + "  AND [Action] = 'R' AND dbo.getDatePart(VJH.DoneDate) BETWEEN '" + CalcDay + "' AND '" + Today + "' ";

        DataTable dtTotJobsDone = Common.Execute_Procedures_Select_ByQuery(strTotJobsDone);
        if (dtTotJobsDone.Rows.Count > 0)
        {
            lnkCritical_Done.Text = dtTotJobsDone.Rows[0][0].ToString();
        }

        string strCODue = "SELECT VCJM.VesselCode,(SELECT ShipName FROM Settings WHERE ShipCode = VCJM.VesselCode) AS VesselName,CM.ComponentId,CM.ComponentCode,CM.ComponentName ,CM.CriticalType,JM.JobCode,JM.JobName AS JobType,CJM.DescrSh AS JobName,VCJM.CompJobId,JIM.IntervalName ,RM.RankCode,VCJM.Interval,replace(convert(varchar(15),VCJMU.NextDueDate,106),' ','-') As NextDueDate,CASE WHEN VCJMU.NextDueDate > getdate() THEN 'DUE' ELSE 'OVER DUE' END AS DueStatus ,CASE PM.Status WHEN 1 THEN 'Issued' WHEN 2 THEN 'Completed' WHEN 3 THEN 'Postponed' WHEN 4 THEN 'Cancelled' ELSE '' END AS WorkOrderStatus,VCJMU.NextHour,REPLACE(CONVERT(VARCHAR(15),VCJMU.LastDone,106),' ','-') AS LastDone,VCJMU.LastHour,CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.LastDueDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE DATEDiff(dd,PM.DoneDate,PM.LastDueDate) END AS Difference,(ISNULL(PM.DoneHour,0) - ISNULL(PM.LastDueHours,0)) AS DiffHour,CASE PM.Status WHEN 4 THEN 0 ELSE ISNULL(PM.AssignedTo,0) END AS PlannedRank,CASE PM.Status WHEN 1 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PlanDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PlanDate,''),106),' ','-') END WHEN 2 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.DoneDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.DoneDate,''),106),' ','-') END WHEN 3 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PostPoneDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PostPoneDate,''),106),' ','-') END  ELSE '' END AS PlanDate,CASE ISNULL(PM.Status,0) WHEN 4 THEN '' WHEN 0 THEN '' ELSE ISNULL(RM.RankCode,'') END AS Rank,Row_Number() OVER(ORDER BY CM.ComponentCode) AS RowNumber, CASE CM.ClassEquip WHEN 'true' THEN 'Class Equipment - ' + VCM.ClassEquipCode ELSE '' END AS ClassEquip,CASE CM.CriticalEquip WHEN 'true' THEN 'Critical Equipment' ELSE '' END AS CriticalEquip FROM VSL_VesselComponentJobMaster VCJM " +
                                "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId AND VCJM.Status = 'A' " +
                                "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCJM.VesselCode = VCM.VesselCode AND VCJM.ComponentId = VCM.ComponentId AND VCM.Status = 'A' " +
                                "INNER JOIN ComponentsJobMapping CJM ON VCJM.CompjobId=CJM.CompjobId " +
                                "INNER JOIN ComponentMaster CM ON CM.ComponentId = VCJM.ComponentId AND CriticalEquip=1 " +
                                "INNER JOIN JobMaster JM  ON JM.JobId = VCJM.JobId " +
                                "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId " +
                                "LEFT JOIN VSL_PlanMaster PM ON PM.VesselCode = VCJM.VesselCode AND PM.ComponentId = VCJM.ComponentId AND PM.CompJobId = VCJM.CompJobId " +
                                "LEFT JOIN Rank RM ON RM.RankId = PM.AssignedTo " +
                                "WHERE 1=1 " + ShipWhereClause + " AND (VCJMU.NextDueDate < CONVERT(SMALLDATETIME,CONVERT(VARCHAR,GETDATE())) OR VCJMU.NextDueDate IS NULL)  ";

        DataTable dtCriticalODue = Common.Execute_Procedures_Select_ByQuery(strCODue);
        if (dtCriticalODue.Rows.Count > 0)
        {
            lnkCritical_Overdue.Text = dtCriticalODue.Rows.Count.ToString();
        }
    }
    public void LoadNoNCritical()
    {
        lnkNoNCritical_Due.Text = "0";
        lnkNoNCritical_OverDue.Text = "0";
        lnkNoNCritical_Plan.Text = "0";
        lnkNoNCritical_Postponed.Text = "0";
        lnkNoNCritical_B_UP.Text = "0";
        lnkNoNCritical_Done.Text = "0";
        lnkNoNCritical_DoneAfterDue.Text = "0";
        lnkNoNCritical_DefectDue.Text = "0";

        string Today = DateTime.Today.ToString("dd-MMM-yyyy");
        string CalcDay = "";
        int NoNCriticalDue = Common.CastAsInt32(txtNoNCritical_Due.Text);
        int NoNCriticalPlan = Common.CastAsInt32(txtNoNCritical_Plan.Text);
        int NoNCriticalPostponed = Common.CastAsInt32(txtNoNCritical_Postponed.Text);
        int NoNCriticalBreak_Up = Common.CastAsInt32(txtNoNCritical_B_UP.Text);
        int NoNCriticalDone = Common.CastAsInt32(txtNoNCritical_Done.Text);
        int NoNCriticalDoneAfterDue = Common.CastAsInt32(txtNoNCritical_DoneAfterDue.Text);
        int NoNCriticalDefectDue = Common.CastAsInt32(txtNoNCritical_DefectDue.Text);

        string ShipWhereClause = "";
        if (ddlFleet.SelectedIndex != 0)
        {
            //if (ddlVessels.SelectedIndex == 0)
            //{
            //    string Vessels = "";
            //    for (int i = 0; i < ddlVessels.Items.Count; i++)
            //    {
            //        if (i != 0)
            //        {
            //            Vessels = Vessels + "'" + ddlVessels.Items[i].Text + "'" + ",";
            //        }
            //    }
            //    if (Vessels.Length > 0)
            //    {
            //        Vessels = Vessels.Remove(Vessels.Length - 1);
            //    }
            //    ShipWhereClause = ShipWhereClause + "AND VCJM.VesselCode IN (SELECT VesselCode FROM  dbo.Vessel WHERE FleetId = " + ddlFleet.SelectedValue + ")";

            //}
            //else
            if (ddlVessels.SelectedIndex > 0)
            {
                ShipWhereClause = ShipWhereClause + "AND VCJM.VesselCode = '" + ddlVessels.SelectedValue.ToString() + "' ";
            }
        }
        else if (ddlVessels.SelectedIndex != 0)
        {
            ShipWhereClause = ShipWhereClause + "AND VCJM.VesselCode = '" + ddlVessels.SelectedValue.ToString() + "' ";
        }

        // job due
        CalcDay = DateTime.Today.AddDays(NoNCriticalDue + 1).ToString("dd-MMM-yyyy");

        string strDue = "SELECT COUNT(*) FROM VSL_VesselComponentJobMaster VCJM " +
                           "INNER JOIN Rank RM ON RM.RankId = VCJM.AssignTo AND VCJM.Status = 'A' " +
                           "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId " +
                           "INNER JOIN  ( " +
                           "ComponentsJobMapping CJM " +
                           "INNER JOIN JobMaster JM  ON JM.JobId = CJM.JobId " +
                           "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId AND CriticalEquip<>1 " +
                           "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCM.ComponentId = CM.ComponentId AND VCM.Status = 'A' " +
                           ") ON  VCJM.ComponentId = CJM.ComponentId AND VCJM.CompJobId = CJM.CompJobId AND VCJM.Status = 'A' AND VCM.VesselCode = VCJM.VesselCode " +
                           "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId " +
                           "LEFT JOIN VSL_PlanMaster PM ON PM.VesselCode = VCJM.VesselCode AND PM.ComponentId = VCJM.ComponentId AND PM.CompJobId = VCJM.CompJobId " +
                           "WHERE 1=1 " + ShipWhereClause + "  AND dbo.getDatePart(VCJMU.NextDueDate) between '" + Today + "' AND '" + CalcDay + "' ";

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
        //                  "WHERE 1=1 " + ShipWhereClause + " AND ( dbo.getDatePart(VCJMU.NextDueDate) < '" + Today + "' OR VCJMU.NextDueDate IS NULL )";

        string strODue = "SELECT VCJM.VesselCode,(SELECT ShipName FROM Settings WHERE ShipCode = VCJM.VesselCode) AS VesselName,CM.ComponentId,CM.ComponentCode,CM.ComponentName ,CM.CriticalType,JM.JobCode,JM.JobName AS JobType,CJM.DescrSh AS JobName,VCJM.CompJobId,JIM.IntervalName ,RM.RankCode,VCJM.Interval,replace(convert(varchar(15),VCJMU.NextDueDate,106),' ','-') As NextDueDate,CASE WHEN VCJMU.NextDueDate > getdate() THEN 'DUE' ELSE 'OVER DUE' END AS DueStatus ,CASE PM.Status WHEN 1 THEN 'Issued' WHEN 2 THEN 'Completed' WHEN 3 THEN 'Postponed' WHEN 4 THEN 'Cancelled' ELSE '' END AS WorkOrderStatus,VCJMU.NextHour,REPLACE(CONVERT(VARCHAR(15),VCJMU.LastDone,106),' ','-') AS LastDone,VCJMU.LastHour,CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.LastDueDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE DATEDiff(dd,PM.DoneDate,PM.LastDueDate) END AS Difference,(ISNULL(PM.DoneHour,0) - ISNULL(PM.LastDueHours,0)) AS DiffHour,CASE PM.Status WHEN 4 THEN 0 ELSE ISNULL(PM.AssignedTo,0) END AS PlannedRank,CASE PM.Status WHEN 1 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PlanDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PlanDate,''),106),' ','-') END WHEN 2 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.DoneDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.DoneDate,''),106),' ','-') END WHEN 3 THEN CASE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PostPoneDate,''),106),' ','-') WHEN '01-Jan-1900' THEN '' ELSE REPLACE(CONVERT(VARCHAR(11), ISNULL(PM.PostPoneDate,''),106),' ','-') END  ELSE '' END AS PlanDate,CASE ISNULL(PM.Status,0) WHEN 4 THEN '' WHEN 0 THEN '' ELSE ISNULL(RM.RankCode,'') END AS Rank,Row_Number() OVER(ORDER BY CM.ComponentCode) AS RowNumber, CASE CM.ClassEquip WHEN 'true' THEN 'Class Equipment - ' + VCM.ClassEquipCode ELSE '' END AS ClassEquip,CASE CM.CriticalEquip WHEN 'true' THEN 'Critical Equipment' ELSE '' END AS CriticalEquip FROM VSL_VesselComponentJobMaster VCJM " +
                                "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId AND VCJM.Status = 'A' " +
                                "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCJM.VesselCode = VCM.VesselCode AND VCJM.ComponentId = VCM.ComponentId AND VCM.Status = 'A' " +
                                "INNER JOIN ComponentsJobMapping CJM ON VCJM.CompjobId=CJM.CompjobId " +
                                "INNER JOIN ComponentMaster CM ON CM.ComponentId = VCJM.ComponentId AND CriticalEquip<>1 " +
                                "INNER JOIN JobMaster JM  ON JM.JobId = VCJM.JobId " +
                                "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId " +
                                "LEFT JOIN VSL_PlanMaster PM ON PM.VesselCode = VCJM.VesselCode AND PM.ComponentId = VCJM.ComponentId AND PM.CompJobId = VCJM.CompJobId " +
                                "LEFT JOIN Rank RM ON RM.RankId = PM.AssignedTo " +
                                "WHERE 1=1 " + ShipWhereClause + " AND (VCJMU.NextDueDate < CONVERT(SMALLDATETIME,CONVERT(VARCHAR,GETDATE())) OR VCJMU.NextDueDate IS NULL) ";

        DataTable dtODue = Common.Execute_Procedures_Select_ByQuery(strODue);
        if (dtODue.Rows.Count > 0)
        {
            lnkNoNCritical_OverDue.Text = dtODue.Rows.Count.ToString();
        }

        // job planning
        CalcDay = DateTime.Today.AddDays(NoNCriticalPlan + 1).ToString("dd-MMM-yyyy");
        string strNextMonth = "SELECT COUNT(*) FROM VSL_VesselComponentJobMaster VCJM " +
                            "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId AND VCJM.Status = 'A' " +
                            "INNER JOIN  ( " +
                            "ComponentsJobMapping CJM " +
                            "INNER JOIN JobMaster JM  ON JM.JobId = CJM.JobId " +
                            "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId AND CriticalEquip<>1 " +
                            "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCM.ComponentId = CM.ComponentId AND VCM.Status = 'A' " +
                            ") ON  VCJM.ComponentId = CJM.ComponentId AND VCJM.CompJobId = CJM.CompJobId AND VCJM.Status = 'A' AND VCM.VesselCode = VCJM.VesselCode " +
                            "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId " +
                            "INNER JOIN VSL_PlanMaster PM ON PM.VesselCode = VCJM.VesselCode AND PM.ComponentId = VCJM.ComponentId AND PM.CompJobId = VCJM.CompJobId " +
                            "INNER JOIN Rank RM ON RM.RankId = PM.AssignedTo " +
                            "WHERE 1=1 " + ShipWhereClause + "  AND dbo.getDatePart(PM.PlanDate) BETWEEN '" + Today + "' AND '" + CalcDay + "' ";

        DataTable dtNextMonth = Common.Execute_Procedures_Select_ByQuery(strNextMonth);
        if (dtNextMonth.Rows.Count > 0)
        {
            lnkNoNCritical_Plan.Text = dtNextMonth.Rows[0][0].ToString();
        }

        // jobs postponed
        CalcDay = DateTime.Today.AddDays(-NoNCriticalPostponed).ToString("dd-MMM-yyyy");
        string strTotJobsPP = "SELECT COUNT(*) FROM VSL_VesselComponentJobMaster VCJM  " +
                        "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId AND VCJM.Status = 'A' " +
                        "INNER JOIN  (  " +
                        "ComponentsJobMapping CJM  " +
                        "INNER JOIN JobMaster JM  ON JM.JobId = CJM.JobId  " +
                        "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId AND CriticalEquip<>1 " +
                        "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCM.ComponentId = CM.ComponentId AND VCM.Status = 'A' " +
                        ")ON  VCJM.ComponentId = CJM.ComponentId AND VCJM.CompJobId = CJM.CompJobId AND VCJM.Status = 'A' AND VCM.VesselCode = VCJM.VesselCode  " +
                        "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId  " +
                        "INNER JOIN VSL_VesselJobUpdateHistory VJH ON VJH.VesselCode = VCJM.VesselCode AND VJH.ComponentId = VCJM.ComponentId AND VJH.CompJobId = VCJM.CompJobId  " +
                        "INNER JOIN Rank RM ON RM.RankId = VJH.DoneBy " +
                        "WHERE 1=1 " + ShipWhereClause + "  AND [Action] = 'P' AND dbo.getDatePart(VJH.PostPoneDate) BETWEEN '" + CalcDay + "' AND '" + Today + "' ";

        DataTable dtPP = Common.Execute_Procedures_Select_ByQuery(strTotJobsPP);
        if (dtPP.Rows.Count > 0)
        {
            lnkNoNCritical_Postponed.Text = dtPP.Rows[0][0].ToString();
        }

        // jobs break & unplanned
        CalcDay = DateTime.Today.AddDays(-NoNCriticalBreak_Up).ToString("dd-MMM-yyyy");
        string sqlbup = "SELECT COUNT(*) " +
                        "+ (select COUNT(*) from VSL_UnPlannedJobs WHERE VESSELCODE='" + Convert.ToString(Session["CurrentShip"]) + "' AND DONEDATE BETWEEN '" + CalcDay + "' AND '" + Today + "' ) " +
                        "FROM Vsl_DefectDetailsMaster WHERE HistoryId <> 0 AND VESSELCODE='" + Convert.ToString(Session["CurrentShip"]) + "' AND REPORTDT BETWEEN '" + CalcDay + "' AND '" + Today + "'";
        DataTable dtBUP = Common.Execute_Procedures_Select_ByQuery(sqlbup);
        if (dtBUP.Rows.Count > 0)
        {
            lnkNoNCritical_B_UP.Text = dtBUP.Rows[0][0].ToString();
        }

        // jobs done
        CalcDay = DateTime.Today.AddDays(-NoNCriticalDone).ToString("dd-MMM-yyyy");
        string strTotJobsDone = "SELECT COUNT(*) FROM VSL_VesselComponentJobMaster VCJM  " +
                            "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId AND VCJM.Status = 'A' " +
                            "INNER JOIN  (  " +
                            "ComponentsJobMapping CJM  " +
                            "INNER JOIN JobMaster JM  ON JM.JobId = CJM.JobId  " +
                            "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId AND CriticalEquip<>1 " +
                            "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCM.ComponentId = CM.ComponentId AND VCM.Status = 'A' " +
                            ")ON  VCJM.ComponentId = CJM.ComponentId AND VCJM.CompJobId = CJM.CompJobId AND VCJM.Status = 'A' AND VCM.VesselCode = VCJM.VesselCode  " +
                            "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId  " +
                            "INNER JOIN VSL_VesselJobUpdateHistory VJH ON VJH.VesselCode = VCJM.VesselCode AND VJH.ComponentId = VCJM.ComponentId AND VJH.CompJobId = VCJM.CompJobId  " +
                            "INNER JOIN Rank RM ON RM.RankId = VJH.DoneBy " +
                            "WHERE 1=1 " + ShipWhereClause + "  AND [Action] = 'R' AND dbo.getDatePart(VJH.DoneDate) BETWEEN '" + CalcDay + "' AND '" + Today + "' ";

        DataTable dtTotJobsDone = Common.Execute_Procedures_Select_ByQuery(strTotJobsDone);
        if (dtTotJobsDone.Rows.Count > 0)
        {
            lnkNoNCritical_Done.Text = dtTotJobsDone.Rows[0][0].ToString();
        }

        // jobs done after due 
        CalcDay = DateTime.Today.AddDays(-NoNCriticalDoneAfterDue).ToString("dd-MMM-yyyy");
        string strTotJobsDoneAfterDue = "SELECT COUNT(*) FROM VSL_VesselComponentJobMaster VCJM  " +
                       "INNER JOIN VSL_VesselComponentJobMaster_Updates VCJMU ON VCJM.VesselCode = VCJMU.VesselCode AND VCJM.ComponentId = VCJMU.ComponentId AND VCJM.CompJobId = VCJMU.CompJobId AND VCJM.Status = 'A' " +
                       "INNER JOIN  (  " +
                       "ComponentsJobMapping CJM  " +
                       "INNER JOIN JobMaster JM  ON JM.JobId = CJM.JobId  " +
                       "INNER JOIN ComponentMaster CM ON CM.ComponentId = CJM.ComponentId AND CriticalEquip<>1 " +
                       "INNER JOIN VSL_ComponentMasterForVessel VCM ON VCM.ComponentId = CM.ComponentId AND VCM.Status = 'A' " +
                       ")ON  VCJM.ComponentId = CJM.ComponentId AND VCJM.CompJobId = CJM.CompJobId AND VCJM.Status = 'A' AND VCM.VesselCode = VCJM.VesselCode  " +
                       "INNER JOIN JobIntervalMaster JIM ON JIM.IntervalId = VCJM.IntervalId  " +
                       "INNER JOIN VSL_VesselJobUpdateHistory VJH ON VJH.VesselCode = VCJM.VesselCode AND VJH.ComponentId = VCJM.ComponentId AND VJH.CompJobId = VCJM.CompJobId  " +
                       "INNER JOIN Rank RM ON RM.RankId = VJH.DoneBy " +
                       "WHERE 1=1 " + ShipWhereClause + "  AND [Action] = 'R' AND dbo.getDatePart(VJH.DoneDate) BETWEEN '" + CalcDay + "' AND '" + Today + "' AND VJH.DoneDate > VJH.Lastduedate";

        DataTable dtTotJobsDoneAfterDue = Common.Execute_Procedures_Select_ByQuery(strTotJobsDoneAfterDue);
        if (dtTotJobsDoneAfterDue.Rows.Count > 0)
        {
            lnkNoNCritical_DoneAfterDue.Text = dtTotJobsDoneAfterDue.Rows[0][0].ToString();
        }

        // due job defect in 

        CalcDay = DateTime.Today.AddDays(NoNCriticalDefectDue).ToString("dd-MMM-yyyy");
        string strTotJobsDefectDue = "SELECT COUNT(*) from vw_DefectJobs VCJM WHERE COMPLETIONDT IS NULL " +
                                     "AND 1=1 " + ShipWhereClause + "  AND dbo.getDatePart(TARGETDT) BETWEEN '" + Today + "' AND '" + CalcDay + "'";

        DataTable dtTotJobsDefectDue = Common.Execute_Procedures_Select_ByQuery(strTotJobsDefectDue);
        if (dtTotJobsDefectDue.Rows.Count > 0)
        {
            lnkNoNCritical_DefectDue.Text = dtTotJobsDefectDue.Rows[0][0].ToString();
        }
    }
    public void LoadCommon()
    {
        lnkUnVerified.Text = "0";

        string Today = DateTime.Today.ToString("dd-MMM-yyyy");
        string CalcDate = DateTime.Today.AddDays(-Common.CastAsInt32(txtVerifyDays.Text)).ToString("dd-MMM-yyyy");

        //pending for verification
        string WhereClause = " where 1=1 ";
        if (ddlVessels.SelectedIndex != 0)
        {
            WhereClause = WhereClause + " and VesselCode='" + ddlVessels.SelectedValue + "' ";
        }
        else if (ddlFleet.SelectedIndex != 0)
        {
            WhereClause = WhereClause + " and VesselCode in(SELECT VesselCode FROM  dbo.Vessel WHERE FleetId = " + ddlFleet.SelectedValue + ") ";
        }

        WhereClause = WhereClause + " AND (CASE WHEN ACTION='Report' THEN DONEDATE ELSE POSTPONEDATE END) BETWEEN '" + CalcDate + "' AND '" + Today + "'";

        string strUnverified = "SELECT Count(*) from vw_GetJobUpdateDataByPeriod " + WhereClause + "  ";
        //txtVerifyDays
        DataTable dtVerify = Common.Execute_Procedures_Select_ByQuery(strUnverified);
        if (dtVerify.Rows.Count > 0)
        {
            lnkUnVerified.Text = dtVerify.Rows[0][0].ToString();
        }
    }
    private void GetCount()
    {
        //if (JobType > 0)
        //{
            divCritical.Visible = false;
            divNonCritical.Visible = false;
            divMaintenance.Visible = false;
            //if (JobType == 1)
            //{
                divCritical.Visible = true;
                LoadCritical();
           // }
            //if (JobType == 2)
            //{
                divNonCritical.Visible = true;
                LoadNoNCritical();
            //}
            //if (JobType == 3)
            //{
            //    divMaintenance.Visible = true;
            //    LoadCommon();
            //}

       // }
    }

    protected void ddlFleet_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFleet.SelectedIndex != 0)
        {
            DataTable dtVessels = new DataTable();
            string strvessels = "SELECT VesselId,VesselCode,VesselName FROM Vessel VM WHERE EXISTS(SELECT TOP 1 COMPONENTID FROM  VSL_ComponentMasterForVessel EVM WHERE VM.VesselCode = EVM.VesselCode ) AND VesselCode IN (SELECT VesselCode FROM  dbo.Vessel WHERE FleetId = " + ddlFleet.SelectedValue + ") ORDER BY VesselName";
            //string strvessels = "SELECT VesselId,VesselCode,VesselName FROM Vessel WHERE ISNULL(IsExported,0) = 1 AND VesselCode IN (SELECT VesselCode FROM  dbo.Vessel WHERE FleetId = " + ddlFleet.SelectedValue + ") ORDER BY VesselName";
            dtVessels = Common.Execute_Procedures_Select_ByQuery(strvessels);
            ddlVessels.Items.Clear();
            if (dtVessels.Rows.Count > 0)
            {
                ddlVessels.DataSource = dtVessels;
                ddlVessels.DataTextField = "VesselName";
                ddlVessels.DataValueField = "VesselCode";
                ddlVessels.DataBind();
            }
            else
            {
                ddlVessels.DataSource = null;
                ddlVessels.DataBind();
            }
            ddlVessels.Items.Insert(0, "< Select >");
        }
        else
        {
            ddlVessels.Items.Clear();
            BindVessels();
        }
        //if (Convert.ToInt32(ddlTypesOfJobs.SelectedValue) > 0)
        //{
            GetCount();
        //}
        
    }

    protected void lnkCritical_Due_Click(object sender, EventArgs e)
    {
        if (lnkCritical_Due.Text.ToString() != "0")
        {
            Session.Add("CPARAM", ddlFleet.SelectedValue + ":" + ddlVessels.SelectedValue);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "openreport('C_DUE','" + txtCritical_Due.Text + "');", true);
        }

    }
    protected void lnkCritical_Plan_Click(object sender, EventArgs e)
    {
        if (lnkCritical_Plan.Text.ToString() != "0")
        {
            Session.Add("CPARAM", ddlFleet.SelectedValue + ":" + ddlVessels.SelectedValue);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "openreport('C_PLAN','" + txtCritical_Plan.Text + "');", true);
        }

    }
    protected void lnkCritical_Done_Click(object sender, EventArgs e)
    {
        if (lnkCritical_Done.Text.ToString() != "0")
        {
            Session.Add("CPARAM", ddlFleet.SelectedValue + ":" + ddlVessels.SelectedValue);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "openreport('C_DONE','" + txtCritical_Done.Text + "');", true);
        }

    }

    protected void lnkUnVerified_OnClick(object sender, EventArgs e)
    {
        //if (lnkUnVerified.Text.ToString() != "0")
        //{
        ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "openVerifyreport('" + ddlVessels.SelectedValue + "'," + txtVerifyDays.Text + ");", true);
        //}
    }

    protected void lnkNoNCritical_Due_Click(object sender, EventArgs e)
    {
        if (lnkNoNCritical_Due.Text.ToString() != "0")
        {
            Session.Add("PARAM", ddlFleet.SelectedValue + ":" + ddlVessels.SelectedValue);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "openreport('DUE','" + txtNoNCritical_Due.Text + "');", true);
        }

    }
    protected void lnkNoNCritical_OverDue_Click(object sender, EventArgs e)
    {
        if (lnkNoNCritical_OverDue.Text.ToString() != "0")
        {
            Session.Add("PARAM", ddlFleet.SelectedValue + ":" + ddlVessels.SelectedValue);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "openreport('OVERDUE','0');", true);
        }
    }
    protected void lnkNoNCritical_Plan_Click(object sender, EventArgs e)
    {
        if (lnkNoNCritical_Plan.Text.ToString() != "0")
        {
            Session.Add("PARAM", ddlFleet.SelectedValue + ":" + ddlVessels.SelectedValue);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "openreport('PLAN','" + txtNoNCritical_Plan.Text + "');", true);
        }

    }
    protected void lnkNoNCritical_Postponed_Click(object sender, EventArgs e)
    {
        if (lnkNoNCritical_Postponed.Text.ToString() != "0")
        {
            Session.Add("PARAM", ddlFleet.SelectedValue + ":" + ddlVessels.SelectedValue);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "openreport('POSTPONED','" + txtNoNCritical_Postponed.Text + "');", true);
        }
    }
    protected void lnkNoNCritical_B_UP_Click(object sender, EventArgs e)
    {
        //if (lnkNoNCritical_B_UP.Text.ToString() != "0")
        //{
        //    Session.Add("PARAM", ddlFleet.SelectedValue + ":" + ddlVessels.SelectedValue);
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "openreport('B_UP','" + txtNoNCritical_B_UP.Text + "');", true);
        //}

    }
    protected void lnkNoNCritical_Done_Click(object sender, EventArgs e)
    {
        if (lnkNoNCritical_Done.Text.ToString() != "0")
        {
            Session.Add("PARAM", ddlFleet.SelectedValue + ":" + ddlVessels.SelectedValue);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "openreport('DONE','" + txtNoNCritical_Done.Text + "');", true);
        }

    }
    protected void lnkNoNCritical_DoneAfterDue_Click(object sender, EventArgs e)
    {
        if (lnkNoNCritical_DoneAfterDue.Text.ToString() != "0")
        {
            Session.Add("PARAM", ddlFleet.SelectedValue + ":" + ddlVessels.SelectedValue);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "openreport('DONEAFTERDUE','" + txtNoNCritical_DoneAfterDue.Text + "');", true);
        }

    }
    protected void lnkNoNCritical_DefectDue_Click(object sender, EventArgs e)
    {
        if (lnkNoNCritical_DefectDue.Text.ToString() != "0")
        {
            Session.Add("PARAM", ddlFleet.SelectedValue + ":" + ddlVessels.SelectedValue);
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
    protected void Update_Verify(object sender, EventArgs e)
    {
        LoadCommon();
    }

    protected void ShowReport(String VesselCode)
    {
        Chart_HSQE009.Legends.Add("LD");
        Chart_HSQE009.Legends["LD"].Title = "Legend";
        DataTable dt = new DataTable();
        dt = Common.Execute_Procedures_Select_ByQuery("EXEC DBO.Get_MaintenanceKPTforLast6Months '" + VesselCode + "'");
       
        if (dt.Rows.Count > 0)
        {
            Chart_HSQE009.Series["% Outstanding"].Points.Clear();

            foreach (DataRow row in dt.Rows)
            {
                Chart_HSQE009.Series["% Outstanding"].Points.AddXY(row["Mon_Yr"].ToString(), row["OutstandingPercent"].ToString());
            }
               
        }
            Chart_HSQE009.Titles.Add("Critical Job Outstanding % (Last 6 Months)");
            Chart_HSQE009.Titles[0].Font = fnt_ChartHeading;
    
    }


    protected void lnkCritical_Overdue_Click(object sender, EventArgs e)
    {
        if (lnkCritical_Overdue.Text.ToString() != "0")
        {
            Session.Add("CPARAM", ddlFleet.SelectedValue + ":" + ddlVessels.SelectedValue);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tr", "openreport('C_OverDue','0');", true);
        }
    }
}