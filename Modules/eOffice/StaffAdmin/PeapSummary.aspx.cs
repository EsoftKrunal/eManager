using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Text;

public partial class Emtm_PeapSummary : System.Web.UI.Page
{
    public AuthenticationManager auth;
    public string SessionDeleimeter = ConfigurationManager.AppSettings["SessionDelemeter"];
    public int PeapID
    {
        get
        {
            return Common.CastAsInt32(ViewState["PeapID"]);
        }
        set
        {
            ViewState["PeapID"] = value;
        }
    }
    public int EmpID
    {
        get
        {
            return Common.CastAsInt32(ViewState["EmpID"]);
        }
        set
        {
            ViewState["EmpID"] = value;
        }
    }
    public string Mode
    {
        get
        {
            return ""+ViewState["Mode"].ToString();
        }
        set
        {
            ViewState["Mode"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!Page.IsPostBack)
        {
            if((Request.QueryString["PeapID"] != null || Request.QueryString["PeapID"].ToString() != "") && (Request.QueryString["Mode"] != null || Request.QueryString["Mode"].ToString() != ""))
            {
                PeapID = Common.CastAsInt32( Request.QueryString["PeapID"].ToString());
                Mode = Request.QueryString["Mode"].ToString();

                if (Mode == "A")
                {
                    EmpID = Common.CastAsInt32(Session["EmpId"]);
                }
                else
                {
                    EmpID = Common.CastAsInt32(Session["ProfileId"]);
                }
                ShowRecord();
                //BindPotential();
            }
        }
    }
    //------------------ Events
    //------------------ Function
    public void ShowRecord()
    {
        string strSQL = "SELECT PEAPID,CATEGORY As PeapLevel,CASE Occasion WHEN 'R' THEN 'Routine' WHEN 'I' THEN 'Interim' ELSE '' END AS Occasion, POSITIONNAME,EMPCODE,FIRSTNAME , FAMILYNAME,D.DeptName AS DepartmentName, " +
                        "Replace(Convert(Varchar,PM.PEAPPERIODFROM,106),' ','-') AS PEAPPERIODFROM ,Replace(Convert(Varchar,PM.PEAPPERIODTO,106),' ','-') AS PEAPPERIODTO,Replace(Convert(Varchar,PD.DJC,106),' ','-') AS DJC,PM.Status, " +
                        "(SELECT (firstname + ' ' + familyname) FROM Hr_PersonalDetails A WHERE A.EmpId = PD.ReportingTo ) As ReportingTo " +
                        "FROM HR_EmployeePeapMaster PM  " +
                        "INNER JOIN dbo.Hr_PersonalDetails PD ON PM.EMPID=PD.EMPID  " +
                        "LEFT JOIN dbo.HR_PeapCategory PC ON PC.PID= PM.PeapCategory  " +
                        "LEFT JOIN POSITION P ON P.POSITIONID=PD.POSITION  " +
                        "LEFT JOIN HR_Department D ON D.DeptId=PD.DEPARTMENT  " +
                        "WHERE PM.PeapId = " + PeapID.ToString() + " ";

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);

        if (dt.Rows.Count > 0)
        {
            lblPeapLevel.Text = dt.Rows[0]["PeapLevel"].ToString();
            txtOccasion.Text = dt.Rows[0]["Occasion"].ToString();
            txtFirstName.Text = dt.Rows[0]["FIRSTNAME"].ToString();
            txtLastName.Text = dt.Rows[0]["FAMILYNAME"].ToString();
            lblPeapPeriod.Text = dt.Rows[0]["PEAPPERIODFROM"].ToString() + " To " + dt.Rows[0]["PEAPPERIODTO"].ToString();
            lblDepartment.Text = dt.Rows[0]["DepartmentName"].ToString();
            lblPosition.Text = dt.Rows[0]["POSITIONNAME"].ToString();
            lblReportingTo.Text = dt.Rows[0]["ReportingTo"].ToString();
            
            int PStatus=Common.CastAsInt32(dt.Rows[0]["Status"]);

            btnPeapReport.Visible = (PStatus == 4);

            //switch(PStatus )
            //{
            //    case -1:
            //        lblPeapStatus.Text = "PEAP Cancelled";
            //        break;
            //    case 0:
            //        lblPeapStatus.Text = "Self Assessment";
            //        break;
            //    case 1:
            //        lblPeapStatus.Text = "Self Assessment";
            //        break;
            //    case 2:
            //        lblPeapStatus.Text = "With Appraiser";
            //        break;
            //    case 3:
            //        lblPeapStatus.Text = "With Management";
            //        break;
            //    case 4:
            //        lblPeapStatus.Text = "PEAP Closed";
            //        break;
            //    case 5:
            //        lblPeapStatus.Text = "With Management";
            //        break;
            //    case 6:
            //        lblPeapStatus.Text = "With Management";
            //        break;
            //    default:
            //        lblPeapStatus.Text = "NA";
            //        break; 
            //}
            //lblPeapStatus.Text = "Current Status : " + lblPeapStatus.Text;

            if (PStatus == 0)
            {
                td_SA.BgColor = "Yellow";                
                td_ABA.BgColor = "Gray";
                td_MF.BgColor = "Gray";
                td_PC.BgColor = "Gray";

                td_SA.Style.Add("color", "Black");
                td_ABA.Style.Add("color", "White");
                td_MF.Style.Add("color", "White");
                td_PC.Style.Add("color", "White");
            }
            else if (PStatus == 1)
            {
                td_SA.BgColor = "Green";
                td_ABA.BgColor = "Yellow";
                td_MF.BgColor = "Gray";
                td_PC.BgColor = "Gray";

                td_SA.Style.Add("color", "White");
                td_ABA.Style.Add("color", "Black");
                td_MF.Style.Add("color", "White");
                td_PC.Style.Add("color", "White");

            }
            else if (PStatus == 2)
            {
                td_SA.BgColor = "Green";
                td_ABA.BgColor = "Yellow";
                td_MF.BgColor = "Gray";
                td_PC.BgColor = "Gray";

                td_SA.Style.Add("color", "White");
                td_ABA.Style.Add("color", "Black");
                td_MF.Style.Add("color", "White");
                td_PC.Style.Add("color", "White");
            }
            else if (PStatus == 3 || PStatus == 5)
            {
                td_SA.BgColor = "Green";
                td_ABA.BgColor = "Green";
                td_MF.BgColor = "Yellow";
                td_PC.BgColor = "Gray";

                td_SA.Style.Add("color", "White");
                td_ABA.Style.Add("color", "White");
                td_MF.Style.Add("color", "Black");
                td_PC.Style.Add("color", "White");
            }
            else if (PStatus == 4)
            {
                td_SA.BgColor = "Green";                
                td_ABA.BgColor = "Green";
                td_MF.BgColor = "Green";
                td_PC.BgColor = "Green";

                td_SA.Style.Add("color", "White");
                td_ABA.Style.Add("color", "White");
                td_MF.Style.Add("color", "White");
                td_PC.Style.Add("color", "White");
            }
            else if (PStatus == 6)
            {
                td_SA.BgColor = "Green";
                td_ABA.BgColor = "Green";
                td_MF.BgColor = "Green";
                td_PC.BgColor = "Yellow";

                td_SA.Style.Add("color", "White");
                td_ABA.Style.Add("color", "White");
                td_MF.Style.Add("color", "White");
                td_PC.Style.Add("color", "Black");
            }
            else
            {
                td_SA.BgColor = "Gray";
                td_ABA.BgColor = "Gray";
                td_MF.BgColor = "Gray";
                td_PC.BgColor = "Gray";

                td_SA.Style.Add("color", "White");
                td_ABA.Style.Add("color", "White");
                td_MF.Style.Add("color", "White");
                td_PC.Style.Add("color", "White");
            }

            if (PStatus == 0 || PStatus == 1)
            {
                trSAPending.Visible = true;
                CalCulateSelfAppraisalScore();

            }
           else if (PStatus == 2)
            {
                trSAPending.Visible = true;
                CalCulateSelfAppraisalScore();

                trARPending.Visible = true;
                CalCulateAppraisarsScore();
            }
            else if (PStatus == 3 || PStatus == 4 || PStatus == 5 || PStatus == 6)
            {
                trSAPending.Visible = true;
                CalCulateSelfAppraisalScore();

                trARPending.Visible = true;
                CalCulateAppraisarsScore();

                trMDRPending.Visible = true;
                divMDRPending.Visible = true;
                CalCulateManagementScore();

            }
            else
            {
                
            }
            //-------------------------------
            if (Mode == "A") // VIEW FROM HR
            {
                lbStart.Text = "View";
            }
            else
            {
                if (PStatus == 0)
                {
                    lbStart.Text = "Start";
                    if (lblPeapSAPer.Text.Trim() != "0%")
                    {
                        lbStart.Text = "Edit";
                    }
                }
                else
                {
                    lbStart.Text = "View";
                }
            }
            //-------------------------------
        }

        ShowScores();
        ShowLeaveDetails();
        RenderTotalLeaves();
    }

    public void ShowScores()
    {
        string sqlComp = "SELECT CONVERT(DECIMAL(16,2),(CONVERT(DECIMAL(16,2), SUM(ISNULL(Answer,0)))/ CONVERT(DECIMAL(16,2),COUNT(*)))) FROM HR_EmployeeCompetency WHERE PeapId = " + PeapID.ToString() + " AND Answer IS NOT NULL ";
        DataTable dtComp = Common.Execute_Procedures_Select_ByQueryCMS(sqlComp);

        if (dtComp.Rows.Count > 0)
        {
            lblAvgCompScore.Text = dtComp.Rows[0][0].ToString();
        }

        //string sqlJS = "SELECT CONVERT(DECIMAL(16,2),(CONVERT(DECIMAL(16,2), SUM(ISNULL(Answer,0)))/ CONVERT(DECIMAL(16,2),COUNT(*)))) FROM HR_EmployeePeapJobResponsibility WHERE PeapId = " + PeapID.ToString() + " AND Answer IS NOT NULL ";
        string sqlJS = "SELECT SUM(ISNULL(Answer,0)) FROM HR_EmployeePeapJobResponsibility WHERE PeapId = " + PeapID.ToString() + " AND Answer IS NOT NULL ";
        DataTable dtJS = Common.Execute_Procedures_Select_ByQueryCMS(sqlJS);
        string[] Colors = { "Poor", "Below Avg.", "Avg.", "Good", "Outstanding" };

        if (dtJS.Rows.Count > 0)
        {
            lblAvgPerfScore.Text = dtJS.Rows[0][0].ToString();
        }

        //int index = Common.CastAsInt32(Math.Ceiling(Common.CastAsDecimal(lblPerformanceScore_JS.Text)))-1;
        int index = Common.CastAsInt32(Math.Ceiling(Common.CastAsDecimal(lblAvgPerfScore.Text))) - 1;
        if (index <= 0) { index = 0; }

        lblAvgPerfScore.Text += " ( " + Colors[index] + " )";

    
        //------------------ For Competency ---------------------------------

    }
    public void ShowLeaveDetails()
    {
        string strSQL = "SELECT EmpId FROM HR_EmployeePeapMaster WHERE PeapId= " + PeapID.ToString() + " ";
        DataTable dtEmpId = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);

        int EmpId1 = Common.CastAsInt32(dtEmpId.Rows[0]["EmpId"].ToString());

        string sql = "SELECT a.EmpCode,a.firstname,a.middlename,a.familyname,a.Office,a.Department,c.OfficeName,a.Position,d.DeptName,b.PositionName,ss.* " +
                     "FROM Hr_PersonalDetails a  " +
                     "left join (select * from dbo.getLeaveStatus_OnDate(" + EmpId1 + ",'" + DateTime.Today.Date.ToString("dd-MMM-yyyy") + "')) ss on a.empid=ss.empid " +
                     "left outer join Position b on a.Position=b.PositionId  " +
                     "Left Outer Join Office c on a.Office= c.OfficeId  " +
                     "Left Outer Join HR_Department d on a.Department=d.DeptId WHERE a.EMPID=" + EmpId1 + " ";

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dt != null)
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                
                lblCurrentMonth1.Text = DateTime.Today.ToString("MMM") + "-" + DateTime.Today.Year.ToString();

                lblLeaveBalance.Text = string.Format("{0:0.0}", (Convert.ToDouble(dr["PayableLeave"])));
                
            }
        
    }

    protected void RenderTotalLeaves()
    {
        string strSQL = "SELECT EmpId FROM HR_EmployeePeapMaster WHERE PeapId= " + PeapID.ToString() + " ";
        DataTable dtEmpId = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);

        int EmpId = Common.CastAsInt32(dtEmpId.Rows[0]["EmpId"].ToString());


        //int EmpId = Common.CastAsInt32(Session["ProfileId"]);
        if (EmpId > 0)
        {
            string sql = "SELECT DBO.HR_Get_LeavesCredit(" + EmpId + "," + DateTime.Today.Year.ToString() + ")";
            DataTable dtLeaveCredit = Common.Execute_Procedures_Select_ByQueryCMS(sql);

            
            sql = "select * from dbo.getLeaveStatus_OnDate(" + EmpId + ",'" + DateTime.Today.Date.ToString("dd-MMM-yyyy") + "')";

            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            if (dt.Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"<table cellpadding='0'  cellspacing='0' border='0' style='border-collapse :collapse;' width='100%'>");
                DataRow dr = dt.Rows[0];
                //--------- FIRST ROW
                sb.Append(@"<tr>");
                sb.Append(@"<td class='style1'>Last Year Balance (<span class='style2'>" + dr["BalLeaveLast"].ToString() + "</span>)</td>");
                sb.Append(@"<td class='style1'>Annual Entitlement (<span class='style2'>" + string.Format("{0:0.0}", (Convert.ToDouble(dr["AnnLeave"]) + Convert.ToDouble(dr["LieuOffLeave"]))) + "</span>)</td>");
                sb.Append(@"<td class='style1'>Leave Credit (<span class='style2'>" + string.Format("{0:0.0}", Common.CastAsDecimal(dtLeaveCredit.Rows[0][0].ToString())) + "</span>)</td>");
                sb.Append(@"<td class='style1'>Leave Taken (<span class='style2'>" + string.Format("{0:0.0}", (Convert.ToDouble(dr["ConsLeave"]) + Convert.ToDouble(dr["LieuOffLeave"]))) + "</span>)</td>");
                sb.Append(@"<td class='style1'>Expired Leave (<span class='style2'>" + dr["BalLeaveLastExpired"].ToString() + "</span>)</td>");
                sb.Append(@"</tr>");

                sb.Append(@"</table>");
                LiteralTotalLeaves.Text = sb.ToString();
            }

            string BTSql = "SELECT SUM(a.AbsentDays) As AbsenceDays FROM(SELECT CASE WHEN HalfDay>0 THEN 0.5 ELSE DateDiff(d, LeaveFrom, LeaveTo + 1 ) END AS AbsentDays FROM HR_OfficeAbsence WHERE EmpId = " + EmpId + " AND Year(LeaveFrom) = " + DateTime.Today.Year.ToString() + " ) a ";
            DataTable dtBT = Common.Execute_Procedures_Select_ByQueryCMS(BTSql);

            lbOfficeAbsence.Text = dtBT.Rows[0]["AbsenceDays"].ToString() == "" ? "0" : dtBT.Rows[0]["AbsenceDays"].ToString();
        }

    }

    //--------------- Calculate Total/Performance Score --------------------------

    public void CalCulateSelfAppraisalScore()
    {
        string DetailsSQL = "SELECT a.TotalQues,a.TotalFilled,a.FilledPercent, CASE WHEN a.FilledPercent = 100.00 THEN a.CompletedOn ELSE '' END AS CompletedOn  FROM ( " +
                            "SELECT (SELECT COUNT(*) FROM HR_EmployeePeapSADetails AS Total WHERE PeapId = " + PeapID.ToString() + " ) AS TotalQues,(SELECT COUNT(*) FROM HR_EmployeePeapSADetails AS TotalFilled WHERE PeapId = " + PeapID.ToString() + " AND Answer <> '') AS TotalFilled, " +
                            "(SELECT convert( DECIMAL(18,2),(( CAST((SELECT COUNT(*) FROM HR_EmployeePeapSADetails AS TotalFilled WHERE PeapId = " + PeapID.ToString() + " AND Answer <> '') AS DECIMAL)/ CAST(CASE (SELECT COUNT(*) FROM HR_EmployeePeapSADetails AS Total WHERE PeapId = " + PeapID.ToString() + " ) WHEN 0 THEN 1 ELSE (SELECT COUNT(*) FROM HR_EmployeePeapSADetails AS Total WHERE PeapId = " + PeapID.ToString() + " ) END AS DECIMAL)) * 100))) AS FilledPercent, " +
                            "(SELECT CASE WHEN PM.SAOnDt IS NULL THEN '' ELSE Replace(Convert(varchar(11),PM.SAOnDt,106),' ','-') END  FROM HR_EmployeePeapMaster PM WHERE PM.PeapId = " + PeapID.ToString() + " ) AS CompletedOn ) a";
        DataTable dtDetails = Common.Execute_Procedures_Select_ByQueryCMS(DetailsSQL);

        //lblSATot.Text = dtDetails.Rows[0]["TotalFilled"].ToString() + "/" + dtDetails.Rows[0]["TotalQues"].ToString();
        lblSATot.Text = dtDetails.Rows[0]["CompletedOn"].ToString() == ""? "" : "Completed On " + dtDetails.Rows[0]["CompletedOn"].ToString();
        divSAPercent.Style.Add("width", dtDetails.Rows[0]["FilledPercent"].ToString() + "%");
        lblPeapSAPer.Text = dtDetails.Rows[0]["FilledPercent"].ToString().Remove(dtDetails.Rows[0]["FilledPercent"].ToString().Length - 3) + "%";
    }

    public void CalCulateAppraisarsScore()
    {
        string SQL = "SELECT a.TotalQues,a.TotalFilled,a.FilledPercent, CASE WHEN a.FilledPercent= 100.00 THEN CompletedOn ELSE '' END AS CompletedOn FROM ( " +
                     "SELECT (SELECT COUNT(*) FROM HR_EmployeePeap_Appraisers AS Total WHERE PeapId = " + PeapID.ToString() + " ) AS TotalQues,(SELECT COUNT(*) FROM HR_EmployeePeap_Appraisers AS TotalFilled WHERE PeapId = " + PeapID.ToString() + " AND IsJSFilled = 1 AND IsCompFilled = 1 AND IsPotFilled = 1) AS TotalFilled, " +
                     "(SELECT convert( DECIMAL(18,2),(( CAST((SELECT COUNT(*) FROM HR_EmployeePeap_Appraisers AS TotalFilled WHERE PeapId = " + PeapID.ToString() + " AND IsJSFilled = 1 AND IsCompFilled = 1 AND IsPotFilled = 1) AS DECIMAL)/ CAST((SELECT COUNT(*) FROM HR_EmployeePeap_Appraisers AS Total WHERE PeapId = " + PeapID.ToString() + " )AS DECIMAL)) * 100))) AS FilledPercent, " +
                     "(SELECT TOP 1 replace(convert(varchar(11),AppraisedOn ,106),' ','-') AS CompletedOn FROM HR_EmployeePeap_Appraisers WHERE PeapId = " + PeapID.ToString() + " AND IsNotified = 1 ORDER BY AppraisedOn DESC) AS CompletedOn ) a";
        
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);

        //lblTotAPR.Text = dt.Rows[0]["TotalFilled"].ToString() + "/" + dt.Rows[0]["TotalQues"].ToString();
        lblTotAPR.Text = dt.Rows[0]["CompletedOn"].ToString() == "" ? "" : "Completed On " + dt.Rows[0]["CompletedOn"].ToString();
        divAPRPercent.Style.Add("width", dt.Rows[0]["FilledPercent"].ToString() + "%");
        lblARPercent.Text = dt.Rows[0]["FilledPercent"].ToString().Remove(dt.Rows[0]["FilledPercent"].ToString().Length - 3) + "%";

        CalCulateAllAppraisarScore();
    }

    public void CalCulateAllAppraisarScore()
    {
        string SQL = "SELECT A.Name,A.TotalQues,A.TotalFilled, CONVERT(DECIMAL(18,2),((CAST(A.TotalFilled As DECIMAL)/CAST(A.TotalQues As DECIMAL))* 100)) AS FilledPercent, A.AppraiserByUser,A.[Status], CASE WHEN CONVERT(DECIMAL(18,2),((CAST(A.TotalFilled As DECIMAL)/CAST(A.TotalQues As DECIMAL))* 100)) = 100.00 THEN A.CompletedOn ELSE '' END AS CompletedOn FROM ( " +
                     "SELECT (FirstName + ' ' + MiddleName + ' ' + FamilyName ) AS Name,PA.AppraiserByUser,(SELECT [Status] FROM HR_EmployeePeapMaster WHERE PeapId = " + PeapID.ToString() + " )AS [Status], " +
                     "( " +
                       " (SELECT COUNT(*) FROM HR_EmployeeCompetency AS TotComp WHERE PeapId = " + PeapID.ToString() + " AND TotComp.AppraiserByUser = PD.EmpId) + " +
                       " (SELECT COUNT(*) FROM HR_EmployeePeapJobResponsibility AS TotJS WHERE PeapId = " + PeapID.ToString() + " AND TotJS.AppraiserByUser = PD.EmpId) + 2 " +
                     ") AS TotalQues, " +
                     "((SELECT COUNT(*) FROM HR_EmployeeCompetency AS TotFilledComp WHERE PeapId = " + PeapID.ToString() + " AND TotFilledComp.AppraiserByUser = PD.EmpId AND Answer <> '') + " +
                     "(SELECT COUNT(*) FROM HR_EmployeePeapJobResponsibility AS TotFilledJS WHERE PeapId = " + PeapID.ToString() + " AND TotFilledJS.AppraiserByUser = PD.EmpId AND ISNULL(Answer,0) <> 0) + " +
                     "(SELECT COUNT(*) FROM HR_EmployeePeap_Appraisers AS FilledAAR WHERE PeapId = " + PeapID.ToString() + " AND FilledAAR.AppraiserByUser = PD.EmpId AND AppraiserAddResponse <> '') + " +
                     "(SELECT COUNT(*) FROM HR_EmployeePeap_Appraisers AS FilledRP WHERE PeapId = " + PeapID.ToString() + " AND FilledRP.AppraiserByUser = PD.EmpId AND ReadyPromo <> '') " +
                     //"(SELECT COUNT(*) FROM HR_EmployeePeap_Appraisers AS FilledUR WHERE PeapId = " + PeapID.ToString() + " AND FilledUR.AppraiserByUser = PD.EmpId AND UnsuitableReason <> '') + " +
                     //"(SELECT COUNT(*) FROM HR_EmployeePeap_Appraisers AS FilledMTR WHERE PeapId = " + PeapID.ToString() + " AND FilledMTR.AppraiserByUser = PD.EmpId AND MoreTimeReason <> '') " +
                     ") AS TotalFilled, " +
                     "(SELECT replace(convert(varchar(11),AppraisedOn ,106),' ','-') AS CompletedOn FROM HR_EmployeePeap_Appraisers AS AO  WHERE AO.PeapId = " + PeapID.ToString() + " AND AO.AppraiserByUser=PD.EmpId  AND AO.IsNotified = 1) AS CompletedOn " +
                     "FROM HR_EmployeePeap_Appraisers PA INNER JOIN Hr_PersonalDetails PD ON PD.EmpId = PA.AppraiserByUser WHERE PA.PeapId = " + PeapID.ToString() + " ) AS A ";
            
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);

        rptAPRData.DataSource = dt;
        rptAPRData.DataBind();

        trTotAppraisers.Visible = true;
    }

    public void CalCulateManagementScore()
    {
        string SQL = "SELECT A.TotalQues,TotalFilled,CONVERT(DECIMAL(18,2),((CAST(A.TotalFilled As DECIMAL)/CAST(case when A.TotalQues=0 then 1 else A.TotalQues end As DECIMAL))* 100)) AS FilledPercent, CASE WHEN CONVERT(DECIMAL(18,2),((CAST(A.TotalFilled As DECIMAL)/CAST(case when A.TotalQues=0 then 1 else A.TotalQues end As DECIMAL))* 100)) = 100.00 THEN  A.CompletedOn ELSE '' END AS CompletedOn  FROM ( " +
                     "SELECT (SELECT COUNT(*) FROM HR_EmployeePeap_ManagementFeedBack AS Total WHERE PeapId = " + PeapID.ToString() + " ) AS TotalQues, " +
                     "(SELECT COUNT(*) FROM HR_EmployeePeap_ManagementFeedBack AS TotalFilled WHERE PeapId = " + PeapID.ToString() + " AND ManagerRemarks IS NOT NULL) AS TotalFilled, " +
                     "  (SELECT TOP 1 REPLACE(CONVERT(VARCHAR(11), ManagerAppOn, 106),' ','-') AS CompletedOn FROM HR_EmployeePeap_ManagementFeedBack AS MF WHERE PeapId = " + PeapID.ToString() + " AND ManagerRemarks IS NOT NULL ORDER BY ManagerAppOn DESC ) AS CompletedOn " +
                     ") A " ;

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);

        //lblTotMangrs.Text = dt.Rows[0]["TotalFilled"].ToString() + "/" + dt.Rows[0]["TotalQues"].ToString();
        lblTotMangrs.Text = dt.Rows[0]["CompletedOn"].ToString() == "" ? "" : "Completed On " + dt.Rows[0]["CompletedOn"].ToString();
        divMFPercent.Style.Add("width", dt.Rows[0]["FilledPercent"].ToString() + "%");
        lblMFPercent.Text = dt.Rows[0]["FilledPercent"].ToString().Remove(dt.Rows[0]["FilledPercent"].ToString().Length - 3) + "%";

        CalCulateAllManagersScore();
    }

    public void CalCulateAllManagersScore()
    {
        string SQL = "SELECT A.Name, CONVERT(DECIMAL(18,2),((CAST(A.Filled As DECIMAL)/CAST(1 As DECIMAL))* 100)) AS FilledPercent, A.ManagerId,A.[Status],CASE WHEN CONVERT(DECIMAL(18,2),((CAST(A.Filled As DECIMAL)/CAST(1 As DECIMAL))* 100))= 100.00 THEN A.ManagerAppOn ELSE '' END AS CompletedOn FROM ( " +
                     "SELECT (FirstName + ' ' + MiddleName + ' ' + FamilyName ) AS Name,MFB.ManagerId, " +
                     "(SELECT [Status] FROM HR_EmployeePeapMaster WHERE PeapId = " + PeapID.ToString() + " )AS [Status], " +
                     "(SELECT COUNT(*) FROM HR_EmployeePeap_ManagementFeedBack AS Filled WHERE PeapId = " + PeapID.ToString() + " AND Filled.ManagerId = PD.EmpId AND ManagerRemarks <> '') AS Filled,REPLACE(CONVERT(VARCHAR(11),MFB.ManagerAppOn, 106),' ','-') AS ManagerAppOn " +
                     "FROM HR_EmployeePeap_ManagementFeedBack MFB " +
                     "INNER JOIN Hr_PersonalDetails PD ON PD.EmpId = MFB.ManagerId WHERE MFB.PeapId = " + PeapID.ToString() + " ) AS A ";        

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);

        rptManagementData.DataSource = dt;
        rptManagementData.DataBind();

        divMDRPending.Visible = true;
    }

    protected void lbStart_Click(object sender, EventArgs e)
    {
        Response.Redirect("Emtm_PeapSelfAssessment.aspx?PeapID=" + PeapID + "&LoginMode=" + Mode, true);
    }

    protected void lbAPR_Start_Click(object sender, EventArgs e)
    {
        int AppraiserId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        Response.Redirect("Emtm_Peap_AppraisersEntry.aspx?PeapID=" + PeapID + "&LoginMode=" + Mode + "&AppraiserId=" + AppraiserId.ToString(), true);
    }
    
    protected void btnBack_Click(object sender, ImageClickEventArgs  e)
    {
        if (Mode == "A")
        {
            Response.Redirect("../StaffAdmin/Hr_Peap.aspx", true);
        }
        else
        {
            Response.Redirect("../MyProfile/Profile_Peap.aspx", true);
        }
    }

    protected void lbMFB_Start_Click(object sender, EventArgs e)
    {
        int MId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        Response.Redirect("Hr_ManagementFeedBack.aspx?PeapID=" + PeapID + "&LoginMode=" + Mode + "&MId=" + MId.ToString(), true);
    }
    protected void btnLeaveHistory_Click(object sender, ImageClickEventArgs e)
    {
        string strSQL = "SELECT EmpId FROM HR_EmployeePeapMaster WHERE PeapId= " + PeapID.ToString() + " ";
        DataTable dtEmpId = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);

        int EmpId = Common.CastAsInt32(dtEmpId.Rows[0]["EmpId"].ToString());

        ScriptManager.RegisterStartupScript(this, this.GetType(), "LeaveHistory", "PopUPWindow('" + EmpId.ToString() + "');", true);
    }
    protected void lbOfficeAbsence_Click(object sender, EventArgs e)
    {
        string strSQL = "SELECT EmpId FROM HR_EmployeePeapMaster WHERE PeapId= " + PeapID.ToString() + " ";
        DataTable dtEmpId = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);

        int EmpId = Common.CastAsInt32(dtEmpId.Rows[0]["EmpId"].ToString());

        ScriptManager.RegisterStartupScript(this, this.GetType(), "BT", "PopUPWindowBT('" + EmpId + "');", true);
    }

    protected void btnPeapReport_Click(object sender, EventArgs e)
    {
       // PeapID = Common.CastAsInt32(((Button)sender).CommandArgument);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "rpt", "showReport('" + PeapID + "');", true);
    }
}
