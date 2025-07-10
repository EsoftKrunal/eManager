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
using System.Text;

public partial class CrewOperation_TrainingDueReport : System.Web.UI.Page
{
    public int VesselId
    {
        get { return Common.CastAsInt32(ViewState["VesselId"]); }
        set {  ViewState["VesselId"] = value;}
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!IsPostBack)
        {
            VesselId = Common.CastAsInt32( Request.QueryString["VSL"]);

            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT VesselCode, VesselName FROM Vessel WHERE VesselId=" + VesselId);

            lblVesselName.InnerText = "[ " + dt.Rows[0]["VesselCode"].ToString() + " ] " + dt.Rows[0]["VesselName"].ToString();
            ShowMatrix(false);
        }
    }

    protected void ShowMatrix(bool export)
    {
        //Get All Trainings
        string sqlTrng = "Select T.TRAININGNAME,T.TRAININGID,TM.SCHEDULECOUNT,TM.ScheduleType,TT.TrainingTypeName from TrainingMatrixDetails TM inner join Training T on T.TRAININGID=TM.TRAININGID INNER JOIN TRAININGTYPE TT ON TT.TRAININGTYPEID=T.TYPEOFTRAINING WHERE TrainingMatrixID= (SELECT [TrainingMatrixId] FROM [dbo].[TrainingMatrixForVessel] WHERE [VesselId]= " + VesselId + ")  order by TrainingName";
        DataTable dtTrng = Common.Execute_Procedures_Select_ByQueryCMS(sqlTrng);

        string sqlTotRank = "select R.RankId,R.RANKGROUPID, R.RankCode, CPD.CrewId, CPD.CrewNumber, CPD.CurrentRankId,CPD.ReliefDueDate from CrewPersonalDetails CPD INNER JOIN rank R  ON CPD.CurrentRankId = R.RankId WHERE R.STATUSID='A' AND CPD.[CurrentVesselId] = " + VesselId + " AND CPD.CrewStatusId = 3 ORDER BY RANKID";
        DataTable DtTotRanks = Common.Execute_Procedures_Select_ByQueryCMS(sqlTotRank);
        StringBuilder TBL = new StringBuilder();

        string sqlTrngRank = " SELECT DISTINCT dbo.fn_getSimilerTrainingsName(A.TRAININGID) AS TrainingName,        "+
                             "  [dbo].[sp_getLastDone](A.CREWID, A.TRAININGID) AS LastDoneDate,       [dbo].[sp_getNextDue_DB] (A.CREWID, A.TRAININGID) AS DueDate,       " +
                             "  [dbo].[sp_getNextDueSource_DB] (A.CREWID, A.TRAININGID) AS Source,      [dbo].[sp_getNextPlanDate] (A.CREWID, A.TRAININGID) AS PlanDate ,  	  " +
                             "  A.TrainingRequirementId ,A.VesselId,A.CREWID,A.TRAININGID,A.CrewRankId as RankId  " +
                             "  FROM  " +
                             "  (  " +
                             "      SELECT DISTINCT CREWID, TRAININGID, TrainingRequirementId, VesselId,CrewRankId from CREWTRAININGREQUIREMENT WHERE vesselID= " + VesselId+"" +
                             "  ) A       ";
        DataTable DtTrngRanks = Common.Execute_Procedures_Select_ByQueryCMS(sqlTrngRank);

        //string SQLCrew = "SELECT CrewId, CrewNumber,CurrentRankId,ReliefDueDate FROM [dbo].[CrewPersonalDetails] " +                                                 
        //                 "WHERE [CurrentVesselId] = " + VesselId + " AND CrewStatusId = 3";

        //DataTable dtCrew = Common.Execute_Procedures_Select_ByQueryCMS(SQLCrew);

        TBL.Append("<table border='0' cellspacing='0' cellpadding='2' style='border-collapse:collapse;'>");
        // Loop for TRs
        //TBL.Append("<tr><td></td><td class='hd1'>Training Name</td><td class='hd1'>Similier Trainings</td><td class='hd1'>Schedule</td>");
        TBL.Append("<tr style='background-color:#0099FF'><td class='hd1'>Training Name</td><td class='hd1'>Schedule</td>");
        foreach (DataRow drtd in DtTotRanks.Rows)
        {
            TBL.Append("<td class='hd1'>" + drtd["RANKCODE"].ToString() + "</td>");
        }
        //me
        TBL.Append("<td class='hd1' width='12px'></td>");
        TBL.Append("</tr>");

        //------ Header Row for Crew# ---------------

        TBL.Append("<tr><td class='hd1'>&nbsp;</td><td class='hd1'>&nbsp;</td>");
        foreach (DataRow drranks in DtTotRanks.Rows)
        {
            //int RankID = Common.CastAsInt32(drranks["RankId"]);
            TBL.Append("<td style='text-align:center; font-size:10px;' >" + drranks["CrewNumber"] + "</td>");
        }

        TBL.Append("<td >&nbsp;</td>");
        TBL.Append("</tr>");

        //----------------------------------------------         

        foreach (DataRow dr in dtTrng.Rows)
        {

            int TrainingID = Common.CastAsInt32(dr["TrainingId"]);
            string SimilerTrainings = "select trainingid,TrainingName,(select TrainingTypeName from trainingtype tt where tt.trainingtypeid=t.typeoftraining) as TrainingTypeName from Training t " +
                                      "where t.trainingid in (select d.SimilerTrainingId from TrainingSimiler d where d.TrainingId=" + TrainingID.ToString() + ") " +
                                      "union select trainingid,TrainingName,(select TrainingTypeName from trainingtype tt where tt.trainingtypeid=t.typeoftraining) as TrainingTypeName from Training t  " +
                                      "where t.trainingid in (select d.TrainingId from TrainingSimiler d where d.SimilerTrainingId=" + TrainingID.ToString() + ")";
            DataTable dtSimiller = Common.Execute_Procedures_Select_ByQueryCMS(SimilerTrainings);
            SimilerTrainings = "";
            foreach (DataRow drs in dtSimiller.Rows)
            {
                SimilerTrainings += "," + drs[1].ToString() + " [<i style='color:Blue;'>" + drs[2].ToString() + "</i>]";
            }
            if (SimilerTrainings != "")
                SimilerTrainings = SimilerTrainings.Substring(1);
            SimilerTrainings = SimilerTrainings.Replace(",", "<span style='color:red'> OR </span><br/>");

            TBL.Append("<tr><td style='width:500px;font-size:10px;' >" + dr["TrainingName"].ToString() + " [<i style='color:Blue;'>" + dr["TrainingTypeName"].ToString() + "</i>]" + ((SimilerTrainings.Trim() != "") ? "<span style='color:red'> OR </span><br/>" + SimilerTrainings : "") + "</td>");


            TBL.Append("<td class='hd' style='text-align:center;font-size:10px;'>" + dr["ScheduleCount"].ToString() + "-" + dr["ScheduleType"].ToString() + "  </td>");
            
            // Loop  for td
            foreach (DataRow drtd in DtTotRanks.Rows)
            {
                //int RankID = Common.CastAsInt32(drtd["RankId"]);
                int RANKGROUPID = Common.CastAsInt32(drtd["RANKGROUPID"]);
                int CrewId = Common.CastAsInt32(drtd["CrewId"]);

                DataRow[] drs = DtTrngRanks.Select("TrainingId=" + TrainingID.ToString() + " AND RankId=" + RANKGROUPID.ToString());
                TBL.Append("<td style='text-align:center;background-color:#e2e2e2;font-size:11px;width:90px '>&nbsp;</td>");

                if (drs.Length > 0)
                {

                    string LastDoneDate=  Common.ToDateString( drs[0]["LastDoneDate"]);

                    string DueDate = Common.ToDateString(drs[0]["DueDate"]);
                    string Source =  drs[0]["Source"].ToString();
                    string PlanDate = Common.ToDateString(drs[0]["PlanDate"]);

                    if (LastDoneDate != "")
                    {
                        
                        string BgColor = "#FFFF66";
                        string CellText = "";
                        DateTime LastDone = DateTime.Parse(LastDoneDate);
                        if (DueDate == "")
                        {
                            BgColor = "#66C285";
                            CellText = "Last Done on "+ LastDoneDate;
                        }
                        else 
                        {
                            DateTime dDueDate = DateTime.Parse(DueDate);
                            if (dDueDate > DateTime.Today.AddDays(-60))
                            {
                                BgColor = "#85C1E9";
                                CellText = "Due on " + DueDate;
                            }
                            else
                            {
                                BgColor = "#FF5050";
                                CellText = "Ovedue on " + DueDate;
                            }
                        }
                        TBL.Append("<td style='background-color:" + BgColor + " ;text-align:center; font-size:11px;width:90px'>" + CellText + "</td>");
                        
                    }
                    else
                    {
                        TBL.Append("<td style='background-color:#FF9966;text-align:center; font-size:11px;width:90px '>&nbsp;</td>");
                        
                    }
                }
                else
                {
                    TBL.Append("<td style='text-align:center;background-color:#e2e2e2;font-size:11px;width:90px '>&nbsp;</td>");
                }
            }
            TBL.Append("</tr>");
        }
        TBL.Append("</table>");



        if (export)
        {
            Response.Clear();
            Response.Write(TBL.ToString());
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + lblVesselName.InnerText + "_trainingmatrix.xls");
            Response.Flush();
            Response.End();
        }
        else
        {
            litTreaining.Text = TBL.ToString();
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ShowMatrix(true);
    }
}