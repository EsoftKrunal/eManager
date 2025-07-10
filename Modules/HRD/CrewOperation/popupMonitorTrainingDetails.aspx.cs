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

public partial class CrewOperation_popupMonitorTrainingDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        int Vsl = Common.CastAsInt32(Request.QueryString["Vsl"]);
        int Mode = Common.CastAsInt32(Request.QueryString["Mode"]);
        string FD = "01-" + Request.QueryString["FD"].ToString();
        string TD = DateTime.DaysInMonth(Convert.ToDateTime("01-" + Request.QueryString["TD"].ToString()).Year,Convert.ToDateTime("01-" + Request.QueryString["TD"].ToString()).Month).ToString() + "-" +  Request.QueryString["TD"].ToString();
       
        if (Vsl > 0 && Mode > 0)
        {   
            string VesselSQL = "SELECT VesselName FROM DBO.VESSEL WHERE VESSELID = " + Vsl;
            DataTable dtVessel = Common.Execute_Procedures_Select_ByQueryCMS(VesselSQL);
            if (dtVessel.Rows.Count > 0)
            {
                lblVesselName.Text = dtVessel.Rows[0]["VesselName"].ToString() + "( " + FD + " : " + TD + " )";
            }
            switch( Mode)
            {
                case 1 :
                    lblPageTitle.Text = "OnBoard Crew Members with OverDue Training";                   

                    string SQL = "SELECT CPD.CREWID,CPD.CREWNumber, (CPD.FIRSTNAME + ' ' + CPD.MIDDLENAME + ' ' + CPD.LASTNAME) AS CREWNAME, R.RANKCODE FROM DBO.CREWPERSONALDETAILS CPD INNER JOIN DBO.RANK R ON R.RANKID = CPD.CURRENTRANKID WHERE CPD.CREWSTATUSID =3 AND CPD.CURRENTVESSELID =" + Vsl;
                    DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
                    StringBuilder sb = new StringBuilder();

                    foreach (DataRow dr in dt.Rows)
                    {
                        SQL = "SELECT TrainingTypeNAME,[TrainingName],(SELECT MAX(ToDate) FROM DBO.CREWTRAININGREQUIREMENT WHERE CREWID=A.CREWID AND TRAININGID=A.TRAININGID) AS LastDoneDate,(NextDueComputed) AS DueDate " +
                              "FROM (SELECT DISTINCT CREWID,TRAININGID,NextDueComputed FROM DBO.CREWTRAININGREQUIREMENT WHERE CREWID = " + dr["CREWID"].ToString() + ") A  " +
                              "INNER JOIN Shipsoft.[dbo].[Training] T ON T.TRAININGID = A.TRAININGID " +
                              "INNER JOIN Shipsoft.[dbo].[TrainingType] TT ON TT.[TrainingTypeId] = T.TYPEOFTRAINING " +
                              "WHERE (NextDueComputed) <= GETDATE() ";
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
                case 2:
                    lblPageTitle.Text = "Crew Members Training Done in Period ( By Crew )";

                    string SQL2 = "SELECT CPD.CREWID,CPD.CREWNumber, (CPD.FIRSTNAME + ' ' + CPD.MIDDLENAME + ' ' + CPD.LASTNAME) AS CREWNAME, R.RANKCODE FROM DBO.CREWPERSONALDETAILS CPD INNER JOIN DBO.RANK R ON R.RANKID = CPD.CURRENTRANKID " +
                                  "WHERE CPD.CREWID IN(SELECT DISTINCT CREWID FROM DBO.CREWTRAININGREQUIREMENT " +
                                  "WHERE VESSELID =" + Vsl + " AND ( TODATE BETWEEN '" + FD + "' AND '" + TD + "' ))";
                    DataTable dt2 = Common.Execute_Procedures_Select_ByQueryCMS(SQL2);
                    StringBuilder sb1 = new StringBuilder();

                    foreach (DataRow dr in dt2.Rows)
                    {
                        sb1.Append("<div class='MainRow'>");

                        sb1.Append("<span class='spanCrewNumber'>" + dr["CREWNumber"].ToString() + "</span> | ");

                        sb1.Append("<span class='spanCrewName'>" + dr["CREWNAME"].ToString() + "</span> | ");

                        sb1.Append("<span class='spanRank'>" + dr["RANKCODE"].ToString() + "</span> ");


                        SQL2 = "SELECT TrainingTypeNAME,TrainingName,FromDate,ToDate,(NextDueComputed) As N_DueDate, Remark FROM DBO.CREWTRAININGREQUIREMENT A  " +
                                "INNER JOIN Shipsoft.[dbo].[Training] T ON T.TRAININGID = A.TRAININGID " +
                                "INNER JOIN Shipsoft.[dbo].[TrainingType] TT ON TT.[TrainingTypeId] = T.TYPEOFTRAINING " +
                                "WHERE VESSELID = " + Vsl + " AND CREWID = " + dr["CREWID"].ToString() + " AND ( TODATE BETWEEN '" + FD + "' AND '" + TD + "' )";
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
                case 3:
                    lblPageTitle.Text = "Crew Members Training Done in Period ( By Training Name )";
                    
                    string SQL3 = "SELECT distinct A.TRAININGID,TrainingTypeNAME,TrainingName FROM DBO.CREWTRAININGREQUIREMENT A  " +
                          "INNER JOIN Shipsoft.[dbo].[Training] T ON T.TRAININGID = A.TRAININGID " +
                          "INNER JOIN Shipsoft.[dbo].[TrainingType] TT ON TT.[TrainingTypeId] = T.TYPEOFTRAINING " +
                          "WHERE VESSELID = " + Vsl + " AND ( TODATE BETWEEN '" + FD + "' AND '" + TD + "' )";

                    DataTable dt4 = Common.Execute_Procedures_Select_ByQueryCMS(SQL3);
                    StringBuilder sb3 = new StringBuilder();

                    foreach (DataRow dr in dt4.Rows)
                    {
                        sb3.Append("<div class='MainRow'>");

                        sb3.Append("<span class='spanCrewNumber'>" + dr["TrainingTypeNAME"].ToString() + "</span> | ");

                        sb3.Append("<span class='spanCrewName'>" + dr["TrainingName"].ToString() + "</span>  ");

                        SQL3 = "SELECT CPD.CREWNumber, (CPD.FIRSTNAME + ' ' + CPD.MIDDLENAME + ' ' + CPD.LASTNAME) AS CREWNAME, R.RANKCODE,A.FromDate, A.ToDate,(NextDueComputed) As N_DueDate FROM DBO.CREWTRAININGREQUIREMENT A  " +
                                "INNER JOIN DBO.CREWPERSONALDETAILS CPD ON A.CREWID=CPD.CREWID INNER JOIN DBO.RANK R ON R.RANKID = CPD.CURRENTRANKID " +
                                "WHERE A.VESSELID = " + Vsl + " AND A.TRAININGID = " + dr["TRAININGID"].ToString() + " AND ( A.TODATE BETWEEN '" + FD + "' AND '" + TD + "' )";

                        DataTable dt4_1 = Common.Execute_Procedures_Select_ByQueryCMS(SQL3);
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
                case 4:
                    lblPageTitle.Text = "Crew Members Training Done in Period ( By Rank )";

                    string SQL4 = "SELECT R.RANKID,R.RANKCODE,R.RANKNAME FROM DBO.CREWPERSONALDETAILS CPD INNER JOIN DBO.RANK R ON R.RANKID = CPD.CURRENTRANKID " +
                                  "WHERE CPD.CREWID IN(SELECT DISTINCT CREWID FROM DBO.CREWTRAININGREQUIREMENT " +
                                  "WHERE VESSELID =" + Vsl + " AND ( TODATE BETWEEN '" + FD + "' AND '" + TD + "' )) ORDER BY RANKLEVEL";
                    DataTable dt5 = Common.Execute_Procedures_Select_ByQueryCMS(SQL4);
                    StringBuilder sb4 = new StringBuilder();

                    foreach (DataRow dr in dt5.Rows)
                    {
                        sb4.Append("<div class='MainRow'>");

                        sb4.Append("<span class='spanCrewNumber'>" + dr["RANKCODE"].ToString() + "</span> | ");

                        sb4.Append("<span class='spanCrewName'>" + dr["RANKNAME"].ToString() + "</span> ");

                        SQL4 = "SELECT CPD.CREWNumber, (CPD.FIRSTNAME + ' ' + CPD.MIDDLENAME + ' ' + CPD.LASTNAME) AS CREWNAME,TrainingTypeNAME,TrainingName,FromDate,ToDate,(NextDueComputed) As N_DueDate, Remark FROM DBO.CREWTRAININGREQUIREMENT A  " +
                                "INNER JOIN Shipsoft.[dbo].[Training] T ON T.TRAININGID = A.TRAININGID " +
                                "INNER JOIN Shipsoft.[dbo].[TrainingType] TT ON TT.[TrainingTypeId] = T.TYPEOFTRAINING " +
                                "INNER JOIN DBO.CREWPERSONALDETAILS CPD ON A.CREWID=CPD.CREWID " +
                                "WHERE VESSELID = " + Vsl + " AND A.CREWRANKID = " + dr["RANKID"].ToString() + " AND ( TODATE BETWEEN '" + FD + "' AND '" + TD + "' )";

                        DataTable dt5_1 = Common.Execute_Procedures_Select_ByQueryCMS(SQL4);
                        if (dt5_1.Rows.Count > 0)
                        {
                            sb4.Append("&nbsp;&nbsp;<i style='color:green; font-size:11px;'>( " + dt5_1.Rows.Count.ToString() + " Trainings Done. )</i></div>");
                            sb4.Append("<div class='ChildRow'>");

                            sb4.Append("<table cellspacing='0' cellpadding='3' width='100%' >");
                            sb4.Append("<thead>");
                            sb4.Append("<tr>");
                            sb4.Append("<td style='width:100px;'>Crew#</td>");
                            sb4.Append("<td style='width:200px;'>Crew Name</td>");
                            sb4.Append("<td style='width:200px;'>Training Type</td>");
                            sb4.Append("<td style=''>Training Name</td>");
                            //sb4.Append("<td style='width:100px;text-align:center;'>Due Date</td>");
                            sb4.Append("<td style='width:350px;text-align:center;'>Training Duration</td>");
                            
                            
                            //sb1.Append("<td style=''>Remarks</td>");
                            sb4.Append("</tr>");
                            sb4.Append("</thead>");
                            sb4.Append("<tbody>");
                            foreach (DataRow dr1 in dt5_1.Rows)
                            {
                                sb4.Append("<tr>");
                                sb4.Append("<td>" + dr1["CREWNumber"].ToString() + "</td>");
                                sb4.Append("<td>" + dr1["CREWNAME"].ToString() + "</td>");
                                sb4.Append("<td>" + dr1["TrainingTypeNAME"].ToString() + "</td>");
                                sb4.Append("<td>" + dr1["TrainingName"].ToString() + "</td>");
                                //if (Convert.IsDBNull(dr1["N_DueDate"]))
                                //{
                                //    sb4.Append("<td>&nbsp;</td>");

                                //}
                                //else
                                //{
                                //    if (Convert.ToDateTime(dr1["N_DueDate"].ToString()) < DateTime.Today)
                                //    {
                                //        sb4.Append("<td style='background-color:#FF0000; color:#ffffff;text-align:center;'>" + Common.ToDateString(dr1["N_DueDate"]) + "</td>");
                                //    }
                                //    else
                                //    {
                                //        sb4.Append("<td style='text-align:center;'>" + Common.ToDateString(dr1["N_DueDate"]) + "</td>");
                                //    }
                                //}
                                sb4.Append("<td style='text-align:center;'>" + Common.ToDateString(dr1["FromDate"]) + " - " + Common.ToDateString(dr1["ToDate"]) + "</td>");
                               
                                
                                
                                sb4.Append("</tr>");
                            }
                            sb4.Append("</tbody>");
                            sb4.Append("</table>");

                            sb4.Append("</div>");

                        }
                        else
                        {
                            sb4.Append("&nbsp;&nbsp;<i style='color:green; font-size:11px;'>( No Trainings Done. )</i></div>");
                        }
                    }

                    litTraining.Text = sb4.ToString();

                    break;
                default :
                     break;
            }
        }
    }
}