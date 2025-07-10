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

public partial class CrewOperation_popupMonitorTrainingDetails_New : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        int OfficeId = Common.CastAsInt32(Request.QueryString["OfficeId"]);
        string Mode = Request.QueryString["Mode"].ToString();
        string FD =  Request.QueryString["FD"].ToString();
        string TD = Request.QueryString["TD"].ToString();
       
        if (OfficeId > 0 && Mode != "")
        {   
            string VesselSQL = "SELECT RecruitingOfficeName FROM DBO.RecruitingOffice WHERE RecruitingOfficeId = " + OfficeId;
            DataTable dtVessel = Common.Execute_Procedures_Select_ByQueryCMS(VesselSQL);
            if (dtVessel.Rows.Count > 0)
            {
                lblOfficeName.Text = dtVessel.Rows[0]["RecruitingOfficeName"].ToString() + "( " + FD + " : " + TD + " )";
            }
            switch( Mode)
            {
                case "CDone":
                    lblPageTitle.Text = "OnBoard Crew Members with OverDue Training";                   

                    string SQL = " SELECT DISTINCT CPD.CREWID,CPD.CREWNUMBER,R.RankCode, (CPD.FIRSTNAME + ' ' + CPD.MIDDLENAME + ' ' + CPD.LASTNAME) AS CREWNAME" +
                                 "  FROM DBO.CREWTRAININGREQUIREMENT CT " +
                                 "  INNER JOIN CrewPersonalDetails CPD ON CT.CREWID = CPD.CREWID AND CPD.RecruitmentOfficeId = " +OfficeId+ 
                                 "  INNER JOIN RANK R ON CT.CrewRankId = R.RankID " +
                                 //"  Left JOIN Training T on  t.TrainingId=ct.TrainingId " +
                                 //"  Left JOIN TrainingType TT on TT.TrainingTypeId = t.TypeOfTraining" +
                                 "  WHERE TRAININGPLANNINGID<> 3 AND TODATE BETWEEN '" +FD+"' AND '"+TD+"' ";

                    DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
                    StringBuilder sb = new StringBuilder();

                    foreach (DataRow dr in dt.Rows)
                    {
                        //,(SELECT MAX(ToDate) FROM DBO.CREWTRAININGREQUIREMENT WHERE CREWID=A.CREWID AND TRAININGID=A.TRAININGID) AS LastDoneDate,(NextDueComputed) AS DueDate

                        SQL = " SELECT Ct.*,TT.TrainingTypeNAME,T.TrainingName,(SELECT MAX(ToDate) FROM DBO.CREWTRAININGREQUIREMENT WHERE CREWID=CT.CREWID AND TRAININGID=CT.TRAININGID) AS LastDoneDate,(NextDueComputed) AS DueDate " +
                              " FROM DBO.CREWTRAININGREQUIREMENT CT  " +
                              "  INNER JOIN CrewPersonalDetails CPD ON CT.CREWID = CPD.CREWID AND CPD.RecruitmentOfficeId = "+OfficeId+"  " +
                              " Left JOIN Training T on  t.TrainingId=ct.TrainingId " +
                              " Left JOIN TrainingType TT on TT.TrainingTypeId = T.TypeOfTraining " +
                              "  WHERE TRAININGPLANNINGID<> 3 AND TODATE BETWEEN  '" +FD+"' AND '"+TD+ "' and CT.CREWID = " + dr["CREWID"].ToString()+" ";
                        DataTable dt1 = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
                        if (dt1.Rows.Count <= 0)
                        {
                            continue;
                        }

                        sb.Append("<div class='MainRow'>");

                        sb.Append("<span class='spanCrewNumber'>" + dr["CREWNumber"].ToString() + "</span> | ");

                        sb.Append("<span class='spanCrewName'>" + dr["CREWNAME"].ToString() + "</span> | ");

                        sb.Append("<span class='spanRank'>" + dr["RANKCODE"].ToString() + "</span> ");

                        sb.Append("&nbsp;&nbsp;<i style='color:red; font-size:11px;'>( " + dt1.Rows.Count.ToString() + " OverDue Trainings. )</i></div>");
                        sb.Append("<div class='ChildRow'>");

                        sb.Append("<table cellspacing='0' cellpadding='3' width='100%' >");
                        sb.Append("<thead>");
                        sb.Append("<tr>");
                        sb.Append("<td style='width:200px;'>Training Type</td>");
                        sb.Append("<td style=''>Training Name</td>");
                        sb.Append("<td style='width:150px;text-align:center;'>Last Done Date</td>");
                        sb.Append("<td style='width:100px;text-align:center;'>Due Date</td>");
                        sb.Append("</tr>");
                        sb.Append("</thead>");
                        sb.Append("<tbody>");
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            sb.Append("<tr>");
                            sb.Append("<td>" + dr1["TrainingTypeNAME"].ToString() + "</td>");
                            sb.Append("<td>" + dr1["TrainingName"].ToString() + "</td>");
                            sb.Append("<td style='text-align:center;'>" + Common.ToDateString(dr1["LastDoneDate"]) + "</td>");
                            sb.Append("<td style='background-color:#FF0000; color:#ffffff;text-align:center;'>" + Common.ToDateString(dr1["DueDate"]) + "</td>");
                            sb.Append("</tr>");
                        }
                        sb.Append("</tbody>");
                        sb.Append("</table>");
                        sb.Append("</div>");
                       
                    }

                    litTraining.Text = sb.ToString();

                    break;
                case "RDone":
                    lblPageTitle.Text = "Crew Members Training Done in Period ( By Rank )";

                    //string SQL2 = "SELECT CPD.CREWID,CPD.CREWNumber, (CPD.FIRSTNAME + ' ' + CPD.MIDDLENAME + ' ' + CPD.LASTNAME) AS CREWNAME, R.RANKCODE FROM DBO.CREWPERSONALDETAILS CPD INNER JOIN DBO.RANK R ON R.RANKID = CPD.CURRENTRANKID " +
                    //              "WHERE CPD.CREWID IN(SELECT DISTINCT CREWID FROM DBO.CREWTRAININGREQUIREMENT " +
                    //              "WHERE VESSELID =" + OfficeId + " AND ( TODATE BETWEEN '" + FD + "' AND '" + TD + "' ))";

                    SQL = " SELECT DISTINCT ct.CrewRankID,RANKCODE " +
                                 "  FROM DBO.CREWTRAININGREQUIREMENT CT " +
                                 "  INNER JOIN CrewPersonalDetails CPD ON CT.CREWID = CPD.CREWID AND CPD.RecruitmentOfficeId = " + OfficeId +
                                 "  INNER JOIN RANK R ON CT.CrewRankID = R.RankID " +
                                 "  WHERE TRAININGPLANNINGID<> 3 AND TODATE BETWEEN '" + FD + "' AND '" + TD + "' ";

                    DataTable dt2 = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
                    StringBuilder sb1 = new StringBuilder();

                    foreach (DataRow dr in dt2.Rows)
                    {
                        sb1.Append("<div class='MainRow'>");

                        //sb1.Append("<span class='spanCrewNumber'>" + dr["CREWNumber"].ToString() + "</span> | ");
                        //sb1.Append("<span class='spanCrewName'>" + dr["CREWNAME"].ToString() + "</span> | ");
                        sb1.Append("<span class='spanRank'>" + dr["RANKCODE"].ToString() + "</span> ");


                         string SQL2 = " SELECT Ct.*,TT.TrainingTypeNAME,T.TrainingName,CPD.CrewNumber, (CPD.FIRSTNAME + ' ' + CPD.MIDDLENAME + ' ' + CPD.LASTNAME) AS CREWNAME " +
                              " FROM DBO.CREWTRAININGREQUIREMENT CT  " +
                              "  INNER JOIN CrewPersonalDetails CPD ON CT.CREWID = CPD.CREWID AND CPD.RecruitmentOfficeId = " + OfficeId + "  " +
                              " Left JOIN Training T on  t.TrainingId=ct.TrainingId "+
                              " Left JOIN TrainingType TT on TT.TrainingTypeId = T.TypeOfTraining "+
                              "  WHERE TRAININGPLANNINGID<> 3 AND TODATE BETWEEN  '" + FD + "' AND '" + TD + "' and ct.CrewRankID = " + dr["CrewRankID"].ToString() + " ";

                        DataTable dt3 = Common.Execute_Procedures_Select_ByQueryCMS(SQL2);
                        if (dt3.Rows.Count > 0)
                        {
                            sb1.Append("&nbsp;&nbsp;<i style='color:green; font-size:11px;'>( " + dt3.Rows.Count.ToString() + " Trainings Done. )</i></div>");
                            sb1.Append("<div class='ChildRow'>");

                            sb1.Append("<table cellspacing='0' cellpadding='3' width='100%' >");
                            sb1.Append("<thead>");
                            sb1.Append("<tr>");
                            sb1.Append("<td style='width:200px;'>Training Type</td>");
                            sb1.Append("<td style=''>Training Name</td>");
                            sb1.Append("<td style=''>Crew #</td>");
                            sb1.Append("<td style=''>Crew Name</td>");
                            //sb1.Append("<td style='width:100px;text-align:center;'>Due Date</td>");
                            sb1.Append("<td style='width:350px;text-align:center;'>Training Duration</td>");
                            sb1.Append("</tr>");
                            sb1.Append("</thead>");
                            sb1.Append("<tbody>");
                            foreach (DataRow dr1 in dt3.Rows)
                            {
                                sb1.Append("<tr>");
                                sb1.Append("<td>" + dr1["TrainingTypeNAME"].ToString() + "</td>");
                                sb1.Append("<td>" + dr1["TrainingName"].ToString() + "</td>");
                                sb1.Append("<td>" + dr1["CREWNumber"].ToString() + "</td>");
                                sb1.Append("<td>" + dr1["CREWNAME"].ToString() + "</td>");
                                //if (Convert.IsDBNull(dr1["N_DueDate"]))
                                //{
                                //    sb1.Append("<td>&nbsp;</td>");

                                //}
                                //else
                                //{
                                //    if (Convert.ToDateTime(dr1["N_DueDate"].ToString()) < DateTime.Today)
                                //    {
                                //        sb1.Append("<td style='background-color:#FF0000; color:#ffffff;text-align:center;'>" + Common.ToDateString(dr1["N_DueDate"]) + "</td>");
                                //    }
                                //    else
                                //    {
                                //        sb1.Append("<td style='text-align:center;'>" + Common.ToDateString(dr1["N_DueDate"]) + "</td>");
                                //    }
                                //}
                                sb1.Append("<td style='text-align:center;'>" + Common.ToDateString(dr1["FromDate"]) + " - " + Common.ToDateString(dr1["ToDate"]) + "</td>");
                                //sb1.Append("<td style='text-align:center;'>" + Common.ToDateString(dr1["ToDate"]) + "</td>");
                                 
                                //sb1.Append("<td>" + dr1["Remark"].ToString() + "</td>");
                                sb1.Append("</tr>");
                            }
                            sb1.Append("</tbody>");
                            sb1.Append("</table>");

                            sb1.Append("</div>");

                        }
                        else
                        {
                            sb1.Append("&nbsp;&nbsp;<i style='color:green; font-size:11px;'>( No Trainings Done. )</i></div>");
                        }
                    }

                    litTraining.Text = sb1.ToString();

                    break;
                case "TDone":
                    lblPageTitle.Text = "Crew Members Training Done in Period ( By Training Name )";

                    SQL = " SELECT DISTINCT TT.TrainingTypeNAME,T.TrainingName,ct.TrainingId  " +
                                "  FROM DBO.CREWTRAININGREQUIREMENT CT " +
                                "  INNER JOIN CrewPersonalDetails CPD ON CT.CREWID = CPD.CREWID AND CPD.RecruitmentOfficeId = " + OfficeId +
                                "  INNER JOIN RANK R ON CPD.CurrentRankId = R.RankID " +
                                "  Left JOIN Training T on  t.TrainingId=ct.TrainingId " +
                                "  Left JOIN TrainingType TT on TT.TrainingTypeId = t.TypeOfTraining" +
                                "  WHERE TRAININGPLANNINGID<> 3 AND TODATE BETWEEN '" + FD + "' AND '" + TD + "' ";

                    DataTable dt4 = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
                    StringBuilder sb3 = new StringBuilder();

                    foreach (DataRow dr in dt4.Rows)
                    {
                        sb3.Append("<div class='MainRow'>");

                        sb3.Append("<span class='spanCrewNumber'>" + dr["TrainingTypeNAME"].ToString() + "</span> | ");

                        sb3.Append("<span class='spanCrewName'>" + dr["TrainingName"].ToString() + "</span>  ");

                        string SQL2 = " SELECT Ct.*,CPD.CREWNumber,R.RankCode, (CPD.FIRSTNAME + ' ' + CPD.MIDDLENAME + ' ' + CPD.LASTNAME) AS CREWNAME  " +
                              " FROM DBO.CREWTRAININGREQUIREMENT CT  " +
                              "  INNER JOIN CrewPersonalDetails CPD ON CT.CREWID = CPD.CREWID AND CPD.RecruitmentOfficeId = " + OfficeId + "  " +
                              " INNER JOIN RANK R ON CPD.CurrentRankId = R.RankID "+
                              "  WHERE TRAININGPLANNINGID<> 3 AND TODATE BETWEEN  '" + FD + "' AND '" + TD + "' and CT.TrainingId = " + dr["TrainingId"].ToString() + " ";


                        DataTable dt4_1 = Common.Execute_Procedures_Select_ByQueryCMS(SQL2);
                        if (dt4_1.Rows.Count > 0)
                        {
                            sb3.Append("&nbsp;&nbsp;<i style='color:green; font-size:11px;'>( " + dt4_1.Rows.Count.ToString() + " Trainings Done. )</i></div>");
                            sb3.Append("<div class='ChildRow'>");

                            sb3.Append("<table cellspacing='0' cellpadding='3' width='100%' >");
                            sb3.Append("<thead>");
                            sb3.Append("<tr>");
                            sb3.Append("<td style='width:100px;'>Crew#</td>");
                            sb3.Append("<td style=''>Crew Name</td>");
                            sb3.Append("<td style='width:100px;'>Rank</td>");
                            //sb3.Append("<td style='width:100px;text-align:center;'>Due Date</td>");
                            sb3.Append("<td style='width:350px;text-align:center;'>Training Duration</td>");
                            sb3.Append("</tr>");
                            sb3.Append("</thead>");
                            sb3.Append("<tbody>");
                            foreach (DataRow dr1 in dt4_1.Rows)
                            {
                                sb3.Append("<tr>");
                                sb3.Append("<td>" + dr1["CREWNumber"].ToString() + "</td>");
                                sb3.Append("<td>" + dr1["CREWNAME"].ToString() + "</td>");
                                sb3.Append("<td>" + dr1["RANKCODE"].ToString() + "</td>");
                                //if (Convert.IsDBNull(dr1["N_DueDate"]))
                                //{
                                //    sb3.Append("<td>&nbsp;</td>");

                                //}
                                //else
                                //{
                                //    if (Convert.ToDateTime(dr1["N_DueDate"].ToString()) < DateTime.Today)
                                //    {
                                //        sb3.Append("<td style='background-color:#FF0000; color:#ffffff;text-align:center;'>" + Common.ToDateString(dr1["N_DueDate"]) + "</td>");
                                //    }
                                //    else
                                //    {
                                //        sb3.Append("<td style='text-align:center;'>" + Common.ToDateString(dr1["N_DueDate"]) + "</td>");
                                //    }
                                //}
                                sb3.Append("<td style='text-align:center;'>" + Common.ToDateString(dr1["FromDate"]) + " - " + Common.ToDateString(dr1["ToDate"]) + "</td>");
                                
                                sb3.Append("</tr>");
                            }
                            sb3.Append("</tbody>");
                            sb3.Append("</table>");

                            sb3.Append("</div>");

                        }
                        else
                        {
                            sb3.Append("&nbsp;&nbsp;<i style='color:green; font-size:11px;'>( No Trainings Done. )</i></div>");
                        }
                    }

                    litTraining.Text = sb3.ToString();

                    break;
                default :
                     break;
            }
        }
    }
}