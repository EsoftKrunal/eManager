using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class emtm_Emtm_OfficeAbsenceHome : System.Web.UI.Page
{
    public int EmpId
    {
        get
        {
            return Common.CastAsInt32(ViewState["EmpId"]);
        }
        set
        {
            ViewState["EmpId"] = value;
        }
    }
    public int LeaveRequestId
    {
        get
        {
            return Common.CastAsInt32(ViewState["LeaveRequestId"]);
        }
        set
        {
            ViewState["LeaveRequestId"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!IsPostBack)
        {
            EmpId = Common.CastAsInt32(Session["ProfileId"]);
            ControlLoader.LoadControl(ddlOffice, DataName.Office, "All", "");
            ddlDepartment.Items.Insert(0, new ListItem("< All >", ""));
            ddlPosition.Items.Insert(0, new ListItem("< All >", ""));

            DateTime dt = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime dt1 = dt.AddMonths(1).AddDays(-1);

            txtFrom.Text = Common.ToDateString(DateTime.Today.Date);
            txtTo.Text = Common.ToDateString(DateTime.Today.Date); //dt1.ToString("dd-MMM-yyyy");

            BindGrid();
        }
    }
    public void BindGrid()
    {
        string Where = "";
        if (ddlOffice.SelectedIndex > 0)
        {
            Where += " AND Office = " + ddlOffice.SelectedValue.Trim() + " ";
        }
        if (ddlDepartment.SelectedIndex > 0)
        {
            Where += " AND Department = " + ddlDepartment.SelectedValue.Trim() + " ";
        }
        if (ddlPosition.SelectedIndex > 0)
        {
            Where += " AND Position = " + ddlPosition.SelectedValue.Trim() + " ";
        }
        if (txtFrom.Text.Trim() != "" && txtTo.Text.Trim() != "")
        {
     //       Where += " AND (LeaveFrom BETWEEN  '" + txtFrom.Text.Trim() + "' AND '" + txtTo.Text.Trim() + "'  OR LeaveTo BETWEEN  '" + txtFrom.Text.Trim() + "' AND '" + txtTo.Text.Trim() + "' OR (LeaveFrom < '" + txtFrom.Text.Trim() + "' AND LeaveTo > '" + txtTo.Text.Trim() + "' )) ";
            Where += " AND (dbo.getDatePart(LeaveFrom) BETWEEN  '" + txtFrom.Text.Trim() + "' AND '" + txtTo.Text.Trim() + "'  OR dbo.getDatePart(LeaveTo) BETWEEN  '" + txtFrom.Text.Trim() + "' AND '" + txtTo.Text.Trim() + "' OR (dbo.getDatePart(LeaveFrom) <= '" + txtFrom.Text.Trim() + "' AND dbo.getDatePart(LeaveTo) >= '" + txtTo.Text.Trim() + "' )) ";
     
        }
        

        if (ddlAbsType.SelectedIndex > 0)
        {
            Where += " AND AbsenceType = '" + ddlAbsType.SelectedValue.Trim() + "' ";
        }
        string SQL = "SELECT * FROM ( " +
                     "select 'L' As Type, a.[LeaveRequestId],a.[EmpId],a.[LeaveFrom],a.[LeaveTo],pd.FirstName +' ' + pd.MiddleName +' ' + pd.FamilyName as [Name],LeaveTypeName, 'Leave' AS AbsenceType, '' As Vessel, ofice.OfficeName , '' AS Location, dept.DeptName,case a.Status when 'A'  then case when convert(datetime,a.LeaveTo) < convert(datetime,convert(varchar,getdate(),106))  then 'Taken' else 'Approved' end when 'P' then 'Plan' when 'V' then 'Awaiting Approval' when 'R' then 'Rejected' when 'C' then 'Cancelled' else a.Status end as Status , pd.Position, pd.Office, pd.Department  " +
                     "from HR_LeaveRequest a  " +
                     "left outer join Hr_PersonalDetails pd on a.EmpId=pd.EmpId  " +
                     "left outer join HR_LeaveTypeMaster tm on tm.LeaveTypeId = a.LeaveTypeId " +
                     "left outer join office ofice on pd.Office=ofice.OfficeId  " +
                     "left outer join HR_Department dept on pd.Department=dept.DeptId  " +
                     "where a.Status ='A' " +
                    "UNION " +
                    "SELECT 'BT' As Type, LeaveRequestId,OA.EmpId, coalesce(ActFromDt,LeaveFrom), coalesce(ActToDt,LeaveTo),pd.FirstName +' ' + pd.MiddleName +' ' + pd.FamilyName as [Name],'Bussiness Travel' as LeaveTypeName, 'Bussiness Travel' AS AbsenceType, (SELECT VesselName FRom Vessel V WHERE V.VesselId = OA.VesselId ) As Vessel, ofice.OfficeName, (case when Location=1 THen 'Local' else 'International' end) AS Location, dept.DeptName,Case WHEN OA.[Status] = 'P' THEN 'Planned' WHEN OA.[Status] = 'R' THEN 'Requested' WHEN OA.[Status] = 'A' THEN 'Approved' WHEN OA.[Status] = 'J' THEN 'Rejected' ELSE '' END AS [Status], pd.Position, pd.Office, pd.Department  " +
                    "FROM HR_OfficeAbsence  OA  " +
                    //"INNER JOIN HR_LeavePurpose LP ON LP.PurposeId = OA.PurposeId  " +
                    "left outer join Hr_PersonalDetails pd on OA.EmpId=pd.EmpId  " +
                    "left outer join office ofice on pd.Office=ofice.OfficeId  " +
                    "left outer join HR_Department dept on pd.Department=dept.DeptId  " +
                    ") a " +
                    "WHERE 1=1 " + 
                    //" ( YEAR(LeaveFrom)= Year(getdate())  OR YEAR(LeaveTo)= Year(getdate())) "
                     Where + " ORDER BY LeaveFrom DESC ";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);

        rptData.DataSource = ((dt != null && dt.Rows.Count > 0) ? dt : null);
        rptData.DataBind();
    } 
    protected void btnView_Click(object sender, EventArgs e)
    {
        string Key = ((ImageButton)sender).CommandArgument;
        string[] str = Key.Split('|');

        LeaveRequestId = Common.CastAsInt32(str[0]);
        int Emp_Id = Common.CastAsInt32(str[1]);
        ShowRecord_View(Emp_Id);
        dv_ViewBT.Visible = true;
    }
    public void ShowRecord_View(int Emp_Id)
    {
        string sql = "select * ,LocationText=(case when Location=1 THen 'Local' else 'International' end) ,PurposeText=(select Purpose from [HR_LeavePurpose] where purposeid=oa.purposeid),(Select VesselName from dbo.vessel v where v.vesselid=oa.Vesselid) as VesselName, (SELECT PD.ReportingTo FROM DBO.Hr_PersonalDetails PD WHERE PD.EMPId = oa.EmpId) AS ReportingTo from HR_OfficeAbsence oa WHERE LeaveRequestId =" + LeaveRequestId.ToString() + " ";
        DataTable dtdata = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dtdata != null)
            if (dtdata.Rows.Count > 0)
            {
                DataRow dr = dtdata.Rows[0];
                lblLocation.Text = "[ " + dr["LocationText"].ToString().Trim() + " Visit ]";
                lblPurpose.Text = dr["PurposeText"].ToString().Trim();
                lblPeriod.Text = "Duration : " + Common.ToDateString(dr["LeaveFrom"]) + " - " + Common.ToDateString(dr["LeaveTo"]);
                lblRemarks.Text = dr["Reason"].ToString().Trim();
                lblVesselName.Text = "";
                if (lblPurpose.Text.Contains("Vessel") || lblPurpose.Text.Contains("Docking"))
                {
                    lblVesselName.Text = "Vessel : " + dr["VesselName"].ToString().Trim();
                }
                lblPlannedInspections.Text = "";
                if (lblPurpose.Text.Contains("Vessel"))
                {
                    string Inspections = dr["Inspections"].ToString().Trim();
                    if (Inspections.Trim() != "")
                    {
                        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT CODE FROM DBO.m_Inspection WHERE ID IN (" + Inspections + ")");
                        Inspections = "";
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in dt.Rows)
                            {
                                Inspections += "," + dr1["CODE"].ToString();
                            }
                            Inspections = Inspections.Substring(1);
                        }
                        lblPlannedInspections.Text = "Planned Inspections : " + Inspections;
                    }
                }
                if (lblPurpose.Text.Contains("Docking"))
                {
                    lblPlannedInspections.Text = "DD # : ";
                }



                int HalfDay = Common.CastAsInt32(dr["HalfDay"]);
                switch (HalfDay)
                {
                    case 1:
                        lblHalfDay.Text = "Halfday - First Half";
                        break;
                    case 2:
                        lblHalfDay.Text = "Halfday - Second Half";
                        break;
                    default:
                        lblHalfDay.Text = "";
                        break;
                }

                if (dr["AfterOfficeHr"].ToString() == "True")
                    lblHalfDay.Text += " ( After Office Hrs )";

                if (dr["PurposeText"].ToString().Trim() == "Vessel Attendance")
                    dv_IP.Visible = dv_IPR.Visible = true;
                else
                    dv_IP.Visible = dv_IPR.Visible = false;

                if (dr["PurposeText"].ToString().Trim() == "Docking/Repairs")
                    dv_DD.Visible = dv_DDR.Visible = true;
                else
                    dv_DD.Visible = dv_DDR.Visible = false;

                if (dr["LocationText"].ToString().Trim() == "Local")
                {
                    trHO.Visible = false;
                    trTO.Visible = false;

                    trBriefing.Visible = false;
                    trDeBriefing.Visible = false;
                }
                else
                {
                    trHO.Visible = true;
                    trTO.Visible = true;

                    trBriefing.Visible = true;
                    trDeBriefing.Visible = true;
                }

                if (dr["PaymentStatus"].ToString().Trim() == "P")
                {
                    imgSendAcct_R.Visible = false;
                    imgSendAcct_G.Visible = true;
                }
                else
                {
                    imgSendAcct_R.Visible = true;
                    imgSendAcct_G.Visible = false;
                }

                lblSEAReqDate.Text = Common.ToDateString(dr["PayRequestedOn"]);
                imgDownloadExp.Visible = (dr["FileName"].ToString().Trim() != "");

                lbAction_Startbreifing.Visible = lbAction_StartDeBriefing.Visible = ((EmpId == Emp_Id) || (EmpId == Common.CastAsInt32(dr["ReportingTo"])));

            }

        // --------------- Details Section ----------------

        sql = "SELECT (SELECT COUNT(EmpId) FROM [dbo].[HR_OfficeAbsence_HODApproval] WHERE LeaveRequestId = " + LeaveRequestId + " AND RepliedOn IS NOT NULL) AS Recd,(SELECT COUNT(EmpId) FROM [dbo].[HR_OfficeAbsence_HODApproval] WHERE LeaveRequestId = " + LeaveRequestId + " ) AS [Sent] ";
        dtdata = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        lbRecd.Text = dtdata.Rows[0]["Recd"].ToString();
        lbSent.Text = dtdata.Rows[0]["Sent"].ToString();

        if (Common.CastAsInt32(dtdata.Rows[0]["Sent"]) > 0)
        {
            imgStatusOTB_G.Visible = true;
            imgStatusOTB_R.Visible = false;
        }
        else
        {
            imgStatusOTB_G.Visible = false;
            imgStatusOTB_R.Visible = true;
        }

        // ------------------------------------------------ Inspections 
        sql = "select InspectionNo from dbo.t_InspectionDue where id in (select InspectionDueId from DBO.HR_OfficeAbsence_Inspections where LeaveRequestId=" + LeaveRequestId + ")";
        dtdata = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        string s = "";
        foreach (DataRow dr in dtdata.Rows)
        {
            s += ", " + dr[0].ToString(); ;
        }
        if (s.StartsWith(","))
            s = s.Substring(1);
        lbSelectedInsps.Text = s;
        imgIP_R.Visible = (lbSelectedInsps.Text.Trim() == "");
        imgIP_G.Visible = (lbSelectedInsps.Text.Trim() != "");


        // ------------------------------------------------ DryDocking 

        //if (dv_DD.Visible)
        //{
        sql = "SELECT *,Case WHEN Status = 'P' THEN 'Planned' WHEN Status = 'E' THEN 'Executed' WHEN Status = 'C' THEN 'Closed' ELSE '' END AS StatusText FROM [dbo].[DD_DocketMaster] dm WHERE dm.[DocketId] IN (SELECT e.[DocketId] FROM  [dbo].[HR_OfficeAbsence] e WHERE e.LeaveRequestId = " + LeaveRequestId + ")";
        dtdata = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        if (dtdata != null && dtdata.Rows.Count > 0)
        {
            lblPlannedDDNo.Text = dtdata.Rows[0]["DocketNo"].ToString();
            lblDDStatus.Text = dtdata.Rows[0]["StatusText"].ToString();

            imgDD_R.Visible = false;
            imgDD_G.Visible = true;

        }
        else
        {
            lblPlannedDDNo.Text = "";
            lblDDStatus.Text = "";

            imgDD_R.Visible = true;
            imgDD_G.Visible = false;
        }
        //}
        //else
        //{
        //    imgDD_R.Visible = true;
        //    imgDD_G.Visible = false;
        //}

        // ------------------------------------------------ HOTO & Brief - De-Brief

        if (lblLocation.Text.Contains("International"))
        {

            string sqlPos = "Select Position from Hr_PersonalDetails WHERE EMPId = " + Emp_Id + " AND Position IN (Select PositionId from Position WHERE VesselPositions IN (1,2,3)  OR PositionId IN (1,4,89))";
            DataTable dtdataPos = Common.Execute_Procedures_Select_ByQueryCMS(sqlPos);

            if (dtdataPos != null && dtdataPos.Rows.Count > 0)
            {
                trBriefing.Visible = true;
                trDeBriefing.Visible = true;
                trHO.Visible = true;
                trTO.Visible = true;

                // ------------------------------------------------ HandOver 

                sql = "select HandOverToId,(SELECT FIRSTNAME + ' ' + MiddleName + ' ' + FamilyName FROM DBO.Hr_PersonalDetails WHERE EMPID=HandOverToId) as EMPNAME,NotifiedOn,TakeOver,TakeOverOn,HStatus,BStatus from DBOEmtm_OfficeAbsence_HOTOMaster where HOtoid=" + LeaveRequestId + "";
                dtdata = Common.Execute_Procedures_Select_ByQueryCMS(sql);
                if (dtdata.Rows.Count > 0)
                {
                    lblPriHandoverTo.Text = dtdata.Rows[0]["EMPNAME"].ToString();
                    lblHandOverDate.Text = Common.ToDateString(dtdata.Rows[0]["NotifiedOn"]);
                    imgHO_R.Visible = (dtdata.Rows[0]["HStatus"].ToString().Trim() == "" && dtdata.Rows[0]["BStatus"].ToString().Trim() == "");
                    imgHO_G.Visible = (dtdata.Rows[0]["HStatus"].ToString().Trim() == "A" || dtdata.Rows[0]["BStatus"].ToString().Trim() == "A");

                    if (dtdata.Rows[0]["TakeOver"].ToString() == "Y")
                    {
                        lblTakeoverDate.Text = Common.ToDateString(dtdata.Rows[0]["TakeOverOn"]);
                        imgTO_R.Visible = false;
                        imgTO_G.Visible = true;
                    }
                    else
                    {
                        imgTO_R.Visible = true;
                        imgTO_G.Visible = false;
                    }
                }
                else
                {
                    imgHO_R.Visible = imgTO_R.Visible = true;
                    imgHO_G.Visible = imgTO_G.Visible = false;
                }

                sql = "SELECT FIRSTNAME + ' ' + MiddleName + ' ' + FamilyName FROM DBO.Hr_PersonalDetails WHERE EMPID IN (select BackUpId from DBO.HR_OfficeAbsence_HOTOMaster where HOtoid=" + LeaveRequestId + ")";
                dtdata = Common.Execute_Procedures_Select_ByQueryCMS(sql);
                if (dtdata.Rows.Count > 0)
                {
                    lblBackupHandoverTo.Text = dtdata.Rows[0][0].ToString();
                }

                // ------------------------------------------------ Brieifng

                sql = "select * from DBO.HR_OfficeAbsence_Briefing where LEAVEREQUESTID=" + LeaveRequestId + "";
                dtdata = Common.Execute_Procedures_Select_ByQueryCMS(sql);
                if (dtdata.Rows.Count > 0)
                {
                    lblBriefingDt.Text = Common.ToDateString(dtdata.Rows[0]["BriefOn"]);
                    lblDEBriefingDt.Text = Common.ToDateString(dtdata.Rows[0]["DeBriefOn"]);

                    imgBrief_R.Visible = (lblBriefingDt.Text.Trim() == "");
                    imgDeBrief_R.Visible = (lblDEBriefingDt.Text.Trim() == "");
                    imgBrief_G.Visible = (lblBriefingDt.Text.Trim() != "");
                    imgDeBrief_G.Visible = (lblDEBriefingDt.Text.Trim() != "");
                }
                else
                {
                    imgBrief_R.Visible = imgDeBrief_R.Visible = true;
                    imgBrief_G.Visible = imgDeBrief_G.Visible = false;
                }

            }
            else
            {
                trBriefing.Visible = false;
                trDeBriefing.Visible = false;
                trHO.Visible = false;
                trTO.Visible = false;
            }
        }
        else
        {
            //imgHO_R.Visible = imgTO_R.Visible = true;
            //imgHO_G.Visible = imgTO_G.Visible = false;

            //imgBrief_R.Visible = imgDeBrief_R.Visible = true;
            //imgBrief_G.Visible = imgDeBrief_G.Visible = false;

            trBriefing.Visible = false;
            trDeBriefing.Visible = false;
            trHO.Visible = false;
            trTO.Visible = false;
        }

        // ------------------------- Cash Advance 

        sql = "SELECT * FROM DBO.HR_OfficeAbsence_CashAdvance WHERE BizTravelId = " + LeaveRequestId;
        dtdata = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dtdata.Rows.Count > 0)
        {
            imgCA_R.Visible = true;
            imgCA_G.Visible = false;
        }
        else
        {
            imgCA_R.Visible = false;
            imgCA_G.Visible = true;
        }
        string CashTaken = "";
        foreach (DataRow dr in dtdata.Rows)
        {
            if (dr["RecordType"].ToString().Trim() == "G")
                CashTaken = CashTaken + String.Format("{0:F}", dr["CashAdvance"].ToString()) + " " + dr["Currency"].ToString() + ",";
        }

        if (CashTaken.Trim().Length > 0)
            CashTaken = CashTaken.TrimEnd(',');

        lblCashTaken.Text = CashTaken;

        // ------------------------- Update Itinery

        sql = "SELECT  REPLACE(Convert(varchar(11), ActFromDt, 106),' ','-') AS ActFromDt,REPLACE(Convert(varchar(11), ActToDt, 106),' ','-') AS ActToDt, ActFromDt AS Time,ActToDt AS EndTime FROM DBO.HR_OfficeAbsence WHERE LeaveRequestId = " + LeaveRequestId;
        dtdata = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dtdata.Rows.Count > 0)
        {
            string FromDate = dtdata.Rows[0]["ActFromDt"].ToString();
            string TODate = dtdata.Rows[0]["ActToDt"].ToString() == "01-Jan-1900" ? "" : dtdata.Rows[0]["ActToDt"].ToString();

            if (dtdata.Rows[0]["Time"].ToString() != "")
            {
                string time = dtdata.Rows[0]["Time"].ToString();
                string[] str = time.Split(' ');

                FromDate += " " + str[1].ToString() + " (Hrs)";
            }

            if (dtdata.Rows[0]["EndTime"].ToString() != "")
            {
                string endtime = dtdata.Rows[0]["EndTime"].ToString();
                string[] str = endtime.Split(' ');

                TODate += " " + str[1].ToString() + " (Hrs)";
            }

            lblDepDateTime.Text = FromDate;
            lblArrivalDatetime.Text = TODate;

            if (lblDepDateTime.Text.Trim() == "")
            {
                imgUTI_R.Visible = true;
                imgUTI_G.Visible = false;
            }
            else
            {
                imgUTI_R.Visible = false;
                imgUTI_G.Visible = true;
            }
        }
        else
        {
            imgUTI_R.Visible = true;
            imgUTI_G.Visible = false;
        }

        // ------------------------- Insp Report

        sql = "SELECT COUNT(D.InspectionDueId) FROM [dbo].[HR_OfficeAbsence_Inspections]  D " +
              "WHERE LeaveRequestId = " + LeaveRequestId + " AND InspectionId IN ( SELECT ID FROM dbo.m_inspection WHERE InspectionGroup IN (SELECT ID FROM dbo.m_InspectionGroup WHERE code='MTM')) " +
              "AND NOT EXISTS((SELECT NotofiedOn FROM DBO.tbl_Inspection_Report_Notify N WHERE N.InspectionDueId=D.InspectionDueId AND NotofiedOn is not null))";
        dtdata = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dtdata.Rows[0][0].ToString() == "0")
        {
            imgIR_R.Visible = false;
            imgIR_G.Visible = true;

            sql = "SELECT NotofiedOn FROM DBO.tbl_Inspection_Report_Notify WHERE InspectionDueId = (SELECT TOP 1 InspectionDueId FROM [dbo].[HR_OfficeAbsence_Inspections] WHERE LeaveRequestId = " + LeaveRequestId + " AND InspectionId IN ( SELECT ID FROM dbo.m_inspection WHERE InspectionGroup IN (SELECT ID FROM dbo.m_InspectionGroup WHERE code='MTM'))) ";
            dtdata = Common.Execute_Procedures_Select_ByQueryCMS(sql);

            if (dtdata!=null && dtdata.Rows.Count > 0)
            {
                lblInspNotifyDate.Text = Common.ToDateString(dtdata.Rows[0]["NotofiedOn"]);
            }
        }
        else
        {
            imgIR_R.Visible = true;
            imgIR_G.Visible = false;

            lblInspNotifyDate.Text = "";
        }


        // ------------------------- Expense 

        sql = "SELECT * FROM DBO.HR_OfficeAbsence_Expense WHERE BizTravelId = " + LeaveRequestId;
        dtdata = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dtdata.Rows.Count > 0)
        {
            imgExp_R.Visible = false;
            imgExp_G.Visible = true;
        }
        else
        {
            imgExp_R.Visible = true;
            imgExp_G.Visible = false;
        }

    }
    protected void btnCloseview_Click(object sender, EventArgs e)
    {
        LeaveRequestId = 0;
        dv_ViewBT.Visible = false;
    }
    protected void lbSent_Click(object sender, EventArgs e)
    {
        dv_ViewBT.Visible = false;
        dv_SentNow.Visible = true;        
        //chkAll.Visible = false;
        dv_SN_Comments.Visible = false;

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT EmpId, EmpCode, (FirstName + ' ' + MiddleName + ' ' + FamilyName) As Name, (SELECT RepliedComments FROM [dbo].[HR_OfficeAbsence_HODApproval] WHERE LeaveRequestId = " + LeaveRequestId + " AND EmpId =PD.EmpId) AS RepComment, (SELECT [RequestedOn] FROM [dbo].[HR_OfficeAbsence_HODApproval] WHERE LeaveRequestId = " + LeaveRequestId + " AND EmpId =PD.EmpId ) AS SentOn, (SELECT [RepliedOn] FROM [dbo].[HR_OfficeAbsence_HODApproval] WHERE LeaveRequestId = " + LeaveRequestId + " AND EmpId =PD.EmpId ) AS ReplyOn, '' As Email, 0 AS Show FROM [dbo].[Hr_PersonalDetails] PD WHERE PD.HOD = 1 ORDER BY NAME");

        if (dt != null && dt.Rows.Count > 0)
        {
            rptSentNow.DataSource = dt;
            rptSentNow.DataBind();
        }
        else
        {
            rptSentNow.DataSource = null;
            rptSentNow.DataBind();
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        dv_ViewBT.Visible = true;
        dv_SentNow.Visible = false;
        txtReqComments.Text = "";
    }
    protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        ControlLoader.LoadControl(ddlDepartment, DataName.HR_Department, "All", "", "officeid=" + Common.CastAsInt32(ddlOffice.SelectedValue));
        ControlLoader.LoadControl(ddlPosition, DataName.Position, "All", "", "officeid=" + Common.CastAsInt32(ddlOffice.SelectedValue));
        BindGrid();
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void ddlPosition_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void lbAction_Startbreifing_Click(object sender, EventArgs e)
    {
        dvIframe.Visible = true;
        frmlnk.Attributes.Add("height", "400px");
        frmlnk.Attributes.Add("src", "Emtm_OfficeAbsenceBriefing.aspx?id=" + LeaveRequestId + "&Type=B");
    }
    protected void lbAction_StartDeBriefing_Click(object sender, EventArgs e)
    {
        dvIframe.Visible = true;
        frmlnk.Attributes.Add("height", "400px");
        frmlnk.Attributes.Add("src", "Emtm_OfficeAbsenceBriefing.aspx?id=" + LeaveRequestId + "&Type=D");
    }
    protected void btnHideFrame_Click(object sender, EventArgs e)
    {
        dvIframe.Visible = false;
    }
    protected void ddlAbsType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void txtFrom_TextChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void txtTo_TextChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void imgDownloadExp_Click(object sender, EventArgs e)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT FileName,Attachment FROM DBO.HR_OfficeAbsence WHERE LeaveRequestId=" + LeaveRequestId);
        if (dt.Rows.Count > 0)
        {
            string FileName = dt.Rows[0]["FileName"].ToString();
            if (FileName.Trim() != "")
            {
                byte[] buff = (byte[])dt.Rows[0]["Attachment"];
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                Response.BinaryWrite(buff);
                Response.Flush();
                Response.End();
            }
        }
    }
}