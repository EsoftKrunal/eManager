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

public partial class PopupHistoryJobDetails_Office : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------

        if (!IsPostBack)
        {
            if (Request.QueryString["VC"] != null && Request.QueryString["HID"] != null && Request.QueryString["RP"] != null)
            {
                if (Request.QueryString["RP"].ToString().Trim() == "R")
                {
                    plUpdateJobs.Visible = true;
                    plPostpone.Visible = false;
                    lblPagetitle.Text = "Execute Job Details";
                    ShowReportHistoryDetails();
                    ShowExecuteAttachments();
                }
                if (Request.QueryString["RP"].ToString().Trim() == "P")
                {
                    plUpdateJobs.Visible = false;
                    plPostpone.Visible = true;
                    lblPagetitle.Text = "Postponed Job Details";
                    ShowPostponeHistoryDetails();
                }
            }
        }
    }
    public void ShowReportHistoryDetails()
    {
        string strJobHistorySQL = "SELECT CM.ComponentCode,Cm.ComponentName,JM.JobCode,CJM.DescrSh As JobName,JIM.IntervalName,VCJM.Interval, RM.RankCode AS DoneBy,REPLACE(Convert(Varchar, LastDueDate,106),' ','-') AS LastDueDate,LastDueHours,REPLACE(Convert(Varchar, JH.NextDueDate,106),' ','-') AS NextDueDate,NextDueHours,REPLACE(Convert(Varchar, LastRunningHourDate,106),' ','-') AS LastRunningHourDate,LastRunningHour,DoneBy_Code,DoneBy_Name,REPLACE(Convert(Varchar, DoneDate,106),' ','-') AS DoneDate,DoneHour,ServiceReport,ConditionBefore,ConditionAfter,UpdateRemarks,Specify,CASE UpdateRemarks WHEN 1 THEN 'Planned Job' WHEN 3 THEN 'BREAK DOWN' ELSE '' END AS Reason,((select UserName from ShipUserMaster where UserId=JH.ModifiedBy) + ' / ' + replace(convert(varchar,JH.ModifiedOn,106), ' ', '-')) as UpdatedByName,FileName FROM VSL_VesselJobUpdateHistory JH " +
                                  "INNER JOIN ComponentMaster CM ON CM.ComponentId = JH.ComponentId " +
                                  "INNER JOIN ComponentsJobMapping CJM ON CJM.CompJobId = JH.CompJobId " +
                                  "INNER JOIN JobMaster JM ON JM.jobId = JH.jobId " +
                                  "INNER JOIN Rank RM ON RM.RankId = JH.DoneBy " +
                                  "INNER JOIN VSL_VesselComponentJobMaster VCJM ON VCJM.VesselCode = JH.VesselCode AND VCJM.CompJobId = JH.CompJobId " +
                                  "INNER JOIN JobIntervalMaster JIM ON VCJM.IntervalId = JIM.IntervalId " +
                                  "WHERE JH.VesselCode = '" + Request.QueryString["VC"].ToString().Trim() + "' AND JH.HistoryId =" + Request.QueryString["HID"].ToString() + " ";
        DataTable dtJobHistory = Common.Execute_Procedures_Select_ByQuery(strJobHistorySQL);
        if (dtJobHistory.Rows.Count > 0)
        {
            lblUpdateComponent.Text = dtJobHistory.Rows[0]["ComponentCode"].ToString() + " - " + dtJobHistory.Rows[0]["ComponentName"].ToString();
            lblUpdateInterval.Text = dtJobHistory.Rows[0]["Interval"].ToString() + " - " + dtJobHistory.Rows[0]["IntervalName"].ToString();
            lblUpdateJob.Text = dtJobHistory.Rows[0]["JobCode"].ToString() + " - " + dtJobHistory.Rows[0]["JobName"].ToString();

            lblEmpNo.Text = dtJobHistory.Rows[0]["DoneBy_Code"].ToString();
            lblEmpName.Text = dtJobHistory.Rows[0]["DoneBy_Name"].ToString();
            lblRank.Text = dtJobHistory.Rows[0]["DoneBy"].ToString();
            lblRemarks.Text = dtJobHistory.Rows[0]["Reason"].ToString();
            if (dtJobHistory.Rows[0]["UpdateRemarks"].ToString() == "2")
            {
                trSpecify.Visible = true;
                lblSpecify.Text = dtJobHistory.Rows[0]["Specify"].ToString();
            }
            else
            {
                trSpecify.Visible = false;
            }
            lblLastDoneDt.Text = dtJobHistory.Rows[0]["LastDueDate"].ToString();
            lblInterval.Text = dtJobHistory.Rows[0]["Interval"].ToString() + " - " + dtJobHistory.Rows[0]["IntervalName"].ToString();
            //lblLastHour.Text = dtJobHistory.Rows[0]["LastDueHours"].ToString();
            lblDoneHour.Text = dtJobHistory.Rows[0]["DoneHour"].ToString();
            lblNextHour.Text = dtJobHistory.Rows[0]["NextDueHours"].ToString();
            lblDuedt.Text = dtJobHistory.Rows[0]["LastDueDate"].ToString();
            lblDoneDate.Text = dtJobHistory.Rows[0]["DoneDate"].ToString();
            lblNextDueDt.Text = dtJobHistory.Rows[0]["NextDueDate"].ToString();
            lblServiceReport.Text = dtJobHistory.Rows[0]["ServiceReport"].ToString();
            lblCondBefore.Text = dtJobHistory.Rows[0]["ConditionBefore"].ToString();
            lblCondAfter.Text = dtJobHistory.Rows[0]["ConditionAfter"].ToString();
            lblUpdatedByOn.Text = dtJobHistory.Rows[0]["UpdatedByName"].ToString();
            if (dtJobHistory.Rows[0]["IntervalName"].ToString() == "H")
            {
                trHr.Visible = true;
                lblLastHour.Text = (dtJobHistory.Rows[0]["LastDueHours"].ToString() == "0" ? "" : dtJobHistory.Rows[0]["LastDueHours"].ToString()); ;
            }
            else
            {
                trHr.Visible = false;
            }
            //if (dtJobHistory.Rows[0]["FileName"].ToString() != "")
            //{
            //    trDoc.Visible = true;
            //    //ancFile.HRef = "PopupHistoryAttachment.aspx?Hid=" + Request.QueryString["HID"].ToString();
            //    string path = ProjectCommon.getLinkFolder(DateTime.Parse(lblDoneDate.Text));
            //    ancFile.HRef = path + dtJobHistory.Rows[0]["FileName"].ToString();
            //}
            //else
            //{
            //    trDoc.Visible = false;
            //}
        }
    }
    public void ShowExecuteAttachments()
    {
        string strSQL = "SELECT AttachmentText, [FileName] FROM VSL_VesselJobUpdateHistoryAttachments WHERE VesselCode = '" + Request.QueryString["VC"].ToString().Trim() + "' AND HistoryId = " + Request.QueryString["HID"].ToString().Trim();
        DataTable dtAttachment = Common.Execute_Procedures_Select_ByQuery(strSQL);

        if (dtAttachment.Rows.Count > 0)
        {
            rptAttachment.DataSource = dtAttachment;
            rptAttachment.DataBind();
            trAttachment.Visible = true;
        }
        else
        {
            rptAttachment.DataSource = null;
            rptAttachment.DataBind();
        }

    }
    public void ShowPostponeHistoryDetails()
    {
        string strSQL = "SELECT (RTRIM(CM.ComponentCode) + ' - ' + Cm.ComponentName) AS Component,(JM.JobCode + ' - ' + CJM.DescrSh) As Job,(CAST(VCJM.Interval AS Varchar) + ' - ' + JIM.IntervalName) AS Interval,CASE JH.PostPoneReason WHEN 1 THEN 'Performance Based' WHEN 2 THEN 'Waiting for spares' WHEN 3 THEN 'Dry docking' END AS PostPoneReason,JH.PostPoneRemarks,RM.RankCode AS P_DoneBy,JH.P_DoneBy_Code,JH.P_DoneBy_Name,REPLACE(CONVERT(VARCHAR, JH.PlanDate,106),' ','-') AS PostPoneDate,((select UserName from ShipUserMaster where UserId=JH.ModifiedBy) + ' / ' + replace(convert(varchar,JH.ModifiedOn,106), ' ', '-')) as PostponedByName FROM VSL_VesselJobUpdateHistory JH " +
                        "INNER JOIN ComponentMaster CM ON CM.ComponentId = JH.ComponentId " +
                        "INNER JOIN ComponentsJobMapping CJM ON CJM.CompJobId = JH.CompJobId " +
                        "INNER JOIN JobMaster JM ON JM.jobId = JH.jobId " +
                        "INNER JOIN Rank RM ON RM.RankId = JH.P_DoneBy " +
                        "INNER JOIN VSL_VesselComponentJobMaster VCJM ON VCJM.VesselCode = JH.VesselCode AND VCJM.CompJobId = JH.CompJobId " +
                        "INNER JOIN JobIntervalMaster JIM ON VCJM.IntervalId = JIM.IntervalId " +
                        "WHERE JH.VesselCode = '" + Request.QueryString["VC"].ToString().Trim() + "' AND JH.HistoryId =" + Request.QueryString["HID"].ToString() + " ";
        DataTable dtPostpone = Common.Execute_Procedures_Select_ByQuery(strSQL);
        if (dtPostpone.Rows.Count > 0)
        {
            lblPostponeComponent.Text = dtPostpone.Rows[0]["Component"].ToString();
            lblPostponeJob.Text = dtPostpone.Rows[0]["Job"].ToString();
            lblPostponeInterval.Text = dtPostpone.Rows[0]["Interval"].ToString();
            lblPpReason.Text = dtPostpone.Rows[0]["PostPoneReason"].ToString();
            lblPRank.Text = dtPostpone.Rows[0]["P_DoneBy"].ToString();
            lblPEmpCode.Text = dtPostpone.Rows[0]["P_DoneBy_Code"].ToString();
            lblPEmpname.Text = dtPostpone.Rows[0]["P_DoneBy_Name"].ToString();
            lblPostponeRemarks.Text = dtPostpone.Rows[0]["PostPoneRemarks"].ToString();
            lblPostponedate.Text = dtPostpone.Rows[0]["PostPoneDate"].ToString();
            lblPostPonedByOn.Text = dtPostpone.Rows[0]["PostponedByName"].ToString();
            
        
        }

        string strSQL1 = "SELECT * from VSL_VesselJobUpdateHistory_OfficeComments JH WHERE JH.VesselCode = '" + Request.QueryString["VC"].ToString().Trim() + "' AND JH.HistoryId =" + Request.QueryString["HID"].ToString() + " ";
        DataTable dtPostpone1 = Common.Execute_Procedures_Select_ByQuery(strSQL1);

        if (dtPostpone1.Rows.Count > 0)
        {
            try
            {
                txtOfficeComments.Text = dtPostpone1.Rows[0]["Comments"].ToString();
                lblOfficeCommentByOn.Text = dtPostpone1.Rows[0]["VerifiedBY"].ToString() + " / " + Common.ToDateString(dtPostpone1.Rows[0]["VerifiedON"]);
                radApproved.Checked = dtPostpone1.Rows[0]["Verified"].ToString() == "True";
                radReject.Checked = dtPostpone1.Rows[0]["Verified"].ToString() == "False";
            }
            catch { }
            btnOfficeComment.Visible = (Common.ToDateString(dtPostpone1.Rows[0]["VerifiedON"]).Trim() == "");

        }
        else
        {
            btnOfficeComment.Visible = true;
        }
    }
    protected void btnOfficeComment_OnClick(object sender, EventArgs e)
    {
        

        if (!(radApproved.Checked || radReject.Checked))
        {
            ProjectCommon.ShowMessage("Please select status.");
            return;
        }

        if (txtOfficeComments.Text.Trim() == "")
        {
            ProjectCommon.ShowMessage("Please enter comments.");
            return;
        }

        int Status=(radApproved.Checked)?1:0;

        string UserName = Session["UserName"].ToString();
        Common.Set_Procedures("OfficeVerifyJobHistoryPostpone");
        Common.Set_ParameterLength(5);
        Common.Set_Parameters(
            new MyParameter("@VesselCode", Page.Request.QueryString["VC"].ToString().Trim()),
            new MyParameter("@HistoryId", Page.Request.QueryString["HID"].ToString().Trim()),
            new MyParameter("@Comments", txtOfficeComments.Text.Trim().Replace("'", "`")),
            new MyParameter("@Status",(object) Status),
            new MyParameter("@VerifiedBy", UserName));
        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD(ds))
        {
            ProjectCommon.ShowMessage("Comments Done Successfully.");
            btnOfficeComment.Visible = false;
            ShowReportHistoryDetails();
        }
        else
        {
            ProjectCommon.ShowMessage("Unable to Verify.");
        }

    }
}
