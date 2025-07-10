using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text; 

public partial class emtm_MyProfile_Emtm_Profile_LeaveDetails : System.Web.UI.Page
{
    DateTime ToDay;
    # region User Defined Property
        public int SelectedId
    {
        get
        {
            return Common.CastAsInt32(ViewState["SelectedId"]);
        }
        set
        {
            ViewState["SelectedId"] = value;
        }
    }
    #endregion

    //    NOTE
    //    on this page some part is pending for displauing the bar which does not wrap the leave text fully
    //    NOTE
    
    # region --- User Defined Functions ---
        public void ShowRecord(int Id)
        {
                string sqlLC = "SELECT DBO.HR_Get_LeavesCredit(" + Id + "," + DateTime.Today.Date.ToString("yyyy") + ")";
                DataTable dtLeaveCredit = Common.Execute_Procedures_Select_ByQueryCMS(sqlLC);

                string sql = "SELECT a.EmpCode,a.firstname,a.middlename,a.familyname,a.Office,a.Department,c.OfficeName,a.Position,d.DeptName,b.PositionName,ss.* " +
                             "FROM Hr_PersonalDetails a  " +
                             "left join (select * from dbo.getLeaveStatus_OnDate(" + Id + ",'" + ToDay.Date.ToString("dd-MMM-yyyy") + "')) ss on a.empid=ss.empid " +
                             "left outer join Position b on a.Position=b.PositionId  " +
                             "Left Outer Join Office c on a.Office= c.OfficeId  " +
                             "Left Outer Join HR_Department d on a.Department=d.DeptId WHERE a.EMPID=" + Id + " ";

                DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
                if (dt != null)
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        lblEmpName.Text = " " + dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["FamilyName"].ToString();
                        lblCurrentYear.Text = ToDay.Year.ToString() + " - Leave Status";
                        lblCurrentMonth1.Text = ToDay.ToString("MMM") +"-"+ ToDay.Year.ToString();

                        double CreditLeaves = 0;
                        if (!Convert.IsDBNull(dtLeaveCredit.Rows[0][0]))
                        {
                            CreditLeaves = Convert.ToDouble(dtLeaveCredit.Rows[0][0]);
                        }
                        lblLeaveBalance.Text = string.Format("{0:0.0}", (Convert.ToDouble(dr["PayableLeave"]) ));
                        //lblLeaveBalance.Text = string.Format("{0:0.0}", (Convert.ToDouble(dr["PayableLeave"]) + CreditLeaves));
                        ViewState["OfficeId"] = dr["Office"].ToString();
                        ViewState["DepartmentId"] = dr["Department"].ToString();

                        ddlOffice.SelectedValue = ViewState["OfficeId"].ToString();
                        ddlOffice_SelectedIndexChanged(new object(), new EventArgs());
                        BindLeavesWhoIsOff();
                        ancHolyday.Attributes.Add("href", "Emtm_Profile_CompanyHoliday.aspx?Office=" + ViewState["OfficeId"].ToString());
                    }
                //
                string sqlLeaveHistory = "select top 1 datename(dd,LeaveFrom)as LeaveFromDay,substring(datename(month,LeaveFrom),0,4) as LeaveFromMonth,datename(dd,LeaveTo)as LeaveToDay,substring(datename(month,LeaveTo),0,4) as LeaveToMonth,status as statuscode, case Status when 'A' then 'Approved' when 'P' then 'Plan' when 'W' then 'Awaiting Verification'  when 'v' then 'Awaiting Approval' when 'R' then 'Rejected' else Status end as Status from HR_LeaveRequest where empid=" + Id + " and LeaveFrom>='" + ToDay.ToString("dd-MMM-yyyy") + "' order by leavefrom desc";
                DataTable dtLeaveHistory = Common.Execute_Procedures_Select_ByQueryCMS(sqlLeaveHistory);
                if (dtLeaveHistory != null)
                    if (dtLeaveHistory.Rows.Count > 0)
                    {
                        DivLeaveHistory.Visible = true;

                        DataRow drLeaveHistory = dtLeaveHistory.Rows[0];
                        lblLvsFrom.Text = drLeaveHistory["LeaveFromDay"].ToString() + "th" + " " + drLeaveHistory["LeaveFromMonth"].ToString();
                        lblLvsTO.Text = drLeaveHistory["LeaveToDay"].ToString() + "th" + " " + drLeaveHistory["LeaveToMonth"].ToString();
                        lblLvsStatus.Text = drLeaveHistory["status"].ToString();
                        lblLvsStatus.CssClass = "Status_" + drLeaveHistory["statuscode"].ToString() + "";
                    }
                    else
                    {
                        DivLeaveHistory.Visible = false;
                    }
        }
        protected void RenderLeaveView()
        {
            int EmpId = Common.CastAsInt32(Session["ProfileId"]);
            if (EmpId > 0)
            {
                string sqlLC = "SELECT DBO.HR_Get_LeavesCredit(" + EmpId + "," + DateTime.Today.Date.ToString("yyyy") + ")";
                DataTable dtLeaveCredit = Common.Execute_Procedures_Select_ByQueryCMS(sqlLC);
                Decimal LeaveCreditCount = Common.CastAsDecimal(dtLeaveCredit.Rows[0][0]);

                string sql = "select a.LeaveTypeId,a.LeaveTypeName,isnull(b.LeaveCount,0) as LeaveCount, " +
                            //"dbo.HR_Get_AnnLeavesConsumed_Year_LeaveType(" + ToDay.Year.ToString() + ",a.LeaveTypeId,b.EmpId) as 'Total',ss.* " +
                            "dbo.[HR_Get_TotalConsumedLeaveForPeriod_LeaveType]('01-jan-" + ToDay.Year.ToString() + "','" + ToDay.Date.ToString("dd-MMM-yyyy") + "'," + EmpId.ToString() + ",a.LeaveTypeId) as 'Total',ss.* " +
                            "from HR_LeaveTypeMaster a  " +
                            "left outer join HR_LeaveAssignment b on a.LeaveTypeId=b.LeaveTypeId  " +
                            "left join (select * from dbo.getLeaveStatus_OnDate(" + EmpId.ToString() + ",'" + ToDay.Date.ToString("dd-MMM-yyyy") + "')) ss on b.empid=ss.empid " +
                            "where a.LeaveTypeId <>-1 and b.EmpId=" + EmpId.ToString() + " and b.[year]=" + ToDay.Year.ToString() + " and  a.LeaveTypeId in(select LeaveTypeId  " +
                            "from HR_OfficeLeaveMapping where OfficeId=" + ViewState["OfficeId"] + ") order by a.LeaveTypeName";

                DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
                if (dt.Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(@"<table cellpadding='1' cellspacing='0' border='0' style='border-collapse :collapse; text-align :center;' width='100%'>");

                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        DataRow dr = dt.Rows[i];

                         string LeaveCount;
                         
                         if (dr["LeaveTypeId"].ToString() == "1")
                         {
                             LeaveCount = string.Format("{0:0.0}", LeaveCreditCount + (Common.CastAsDecimal(dr["BalLeaveLast"]) - Common.CastAsDecimal(dr["BalLeaveLastExpired"]) + Common.CastAsDecimal(dr["AnnLeave_TillDate"]) + Common.CastAsDecimal(dr["LieuOffLeave"])));
                         }
                         else
                         {
                             LeaveCount = dr["LeaveCount"].ToString();
                         }


                        if (Common.CastAsInt32(dr["LeaveCount"]) > 0)
                        {
                            float first, second;
                            first = Common.CastAsInt32(dr["Total"].ToString());
                            second = Common.CastAsInt32(LeaveCount);
                            if (second == 0)
                                continue;
                            first = (first / second) * 100;
                            second = 100 - first;

                            int k;
                            k = 1;

                            string BgColor = "";
                            switch (Common.CastAsInt32(dr["LeaveTypeId"].ToString()))
                            {
                                case 1:
                                    BgColor = "Orange";  
                                    break;
                                case 2:
                                    BgColor = "Red";
                                    break;
                                case 3:
                                    BgColor = "Coral";
                                    break;
                                case 4:
                                    BgColor = "BlueViolet";
                                    break;
                                case 5:
                                    BgColor = "Aqua";
                                    break;
                                case 6:
                                    BgColor = "Magenta";
                                    break;
                                case 7:
                                    BgColor = "#F6671B";
                                    break;
                                case 8:
                                    BgColor = "Green";
                                    break;
                                default:
                                    BgColor = "White";
                                    break;
                            }

                            
                            //--------- FIRST ROW
                            sb.Append(@"<tr><td style='width:200px;text-align:right;font-size :12px;font-weight:bold;'><span style='color:#2A5D94;'>" + dr["LeaveTypeName"].ToString() + "&nbsp;:&nbsp;</span></td><td>");
                            sb.Append(@"<div style='z-index:101;clear:both;float:right;left:0px;'><b>" + LeaveCount.ToString() + "</b></div>");
                            sb.Append(@"<div style='width:100%;border:dotted 1px black;height:15px; background-color:#C7CFDA;width:94%;'>");
                            //sb.Append(@"<div style='float:left;z-index:2;'>" + dr["Total"].ToString() + "</div>");
                            //sb.Append(@"<div style='float:right;z-index:2;'>" + LeaveCount.ToString() + "</div>");
                            
                            sb.Append(@"<div style='z-index:0;position:relative;left:0px;background-color:" + BgColor.ToString() + ";width:" + Common.CastAsInt32(first).ToString() + "%;height:15px'><div style='float:left;z-index:2;margin-left:2px;'>" + dr["Total"].ToString() + "</div></div>");
                            
                            sb.Append(@"</div>");
                            //sb.Append(@"<table width='100%' cellpadding='2'><tr>");
                            ////sb.Append(@"<td class='SetProgressBar_Left' style='border-right:none; background-color:" + BgColor.ToString() + ";width:" + Common.CastAsInt32(first).ToString() + "%'>" + dr["Total"].ToString() + "</td>");
                            //sb.Append(@"<td class='SetProgressBar_Left' style='border-right:none; background-color:" + BgColor.ToString() + ";width:" + Common.CastAsInt32(first).ToString() + "%'></td>");
                            //sb.Append(@"<td class='SetProgressBar_Right'  style='border-right:none; background-color:#C7CFDA;width:" + Common.CastAsInt32(second).ToString() + "%'>" + LeaveCount + "</td>");
                            ////for (int i = 1; i <= DateTime.DaysInMonth(Year, Month); i++)
                            ////{
                            ////    sb.Append("<td style='background-color:#75B2DD;'>" + i.ToString() + "</td>");
                            ////}
                            //sb.Append(@"</tr></td></table></tr>");
                            //--------- Second ROW
                            sb.Append(@"<tr><td style='width:200px;text-align:right;'>&nbsp;</td><td><table cellspacing='0' cellpadding='0' width='100%'><tr>");
                            sb.Append(@"<td style='border-right:none; text-align:left;'><i>Taken</i></td>");
                            sb.Append(@"<td style='border-right:none; text-align:right;'><i>Total Allowance</i></td>");
                            //for (int i = 1; i <= DateTime.DaysInMonth(Year, Month); i++)
                            //{
                            //    DateTime dt = new DateTime(Year, Month, i);
                            //    sb.Append("<td style='background-color:#75B2DD'>" + dt.DayOfWeek.ToString().Substring(0, 1) + "</td>");
                            //}
                            sb.Append(@"</tr></table></td></tr>");
                        }
                    }
                    sb.Append(@"</table>");
                    LiteralLeaveView.Text = sb.ToString();
                }
            }

        }
        protected void RenderTotalLeaves()
        {
            int EmpId = Common.CastAsInt32(Session["ProfileId"]);
            if (EmpId > 0)
            {

                string sql = "SELECT DBO.HR_Get_LeavesCredit(" + EmpId + "," + ToDay.Date.ToString("yyyy") + ")";
                DataTable dtLeaveCredit = Common.Execute_Procedures_Select_ByQueryCMS(sql);

                sql = "select * from dbo.getLeaveStatus_OnDate(" + EmpId + ",'" + ToDay.Date.ToString("dd-MMM-yyyy") + "')";

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
                    sb.Append(@"<td class='style1'>Leave Credit (<span class='style2'>" + string.Format("{0:0.0}",Common.CastAsDecimal(dtLeaveCredit.Rows[0][0].ToString())) + "</span>)</td>");
                    sb.Append(@"<td class='style1'>Leave Taken (<span class='style2'>" + string.Format("{0:0.0}", (Convert.ToDouble(dr["ConsLeave"]) + Convert.ToDouble(dr["LieuOffLeave"]))) + "</span>)</td>");
                    sb.Append(@"<td class='style1'>Expired Leave (<span class='style2'>" + dr["BalLeaveLastExpired"].ToString() + "</span>)</td>");
                    sb.Append(@"</tr>");

                    sb.Append(@"</table>");
                    LiteralTotalLeaves.Text = sb.ToString();
                }
            }

        }
        private void BindLeavesApprovalGrid()
        {
            int EmpId = Common.CastAsInt32(Session["ProfileId"]);
            if (EmpId > 0)
            {
                string sql = "SELECT * FROM ( " +
                             "select 'L' As Type, a.LeaveRequestId,a.EmpId,pd.EmpCode,pd.FirstName +' ' + pd.MiddleName +' ' + pd.FamilyName as [Name],ofice.officename,dept.DeptName, a.LeaveTypeId,b.LeaveTypeName, " +
                             "replace(convert(varchar,a.LeaveFrom,106),' ','-') as LeaveFrom,replace(convert(varchar,a.LeaveTo,106),' ','-') as LeaveTo,(case when HalfDay>0 then 0.5 else dbo.HR_Get_LeaveCount(pd.Office,leavefrom,leaveto) end) as Duration, " +
                             "a.Status as StatusCode ,case a.Status when 'A'  then case when convert(datetime,a.LeaveTo) < convert(datetime,convert(varchar,getdate(),106))  then 'Taken' else 'Approved' end " +
                             "when 'P' then 'Plan' when 'V' then 'Awaiting Approval' when 'R' then 'Rejected' when 'C' then 'Cancelled' else a.Status end as Status " +
                             "from HR_LeaveRequest a left outer join HR_LeaveTypeMaster b on a.LeaveTypeId=b.LeaveTypeId left outer join Hr_PersonalDetails pd on a.empid=pd.empid " +
                             "left outer join office ofice on pd.Office=ofice.OfficeId left outer join HR_Department dept on pd.Department=dept.DeptId where a.status ='V' and a.ForwardedTo=" + EmpId + " " +
                             "UNION " +
                             "SELECT 'B' As Type,  a.LeaveRequestId,a.EmpId,pd.EmpCode,pd.FirstName +' ' + pd.MiddleName +' ' + pd.FamilyName as [Name],ofice.officename,dept.DeptName, a.PurposeId,b.Purpose,  " +
                             "replace(convert(varchar,a.LeaveFrom,106),' ','-') as LeaveFrom,replace(convert(varchar,a.LeaveTo,106),' ','-') as LeaveTo,(case when HalfDay>0 then 0.5 else dbo.HR_Get_LeaveCount(pd.Office,leavefrom,leaveto) end) as Duration,  " +
                             "a.Status as StatusCode ,case a.Status when 'A'  then case when convert(datetime,a.LeaveTo) < convert(datetime,convert(varchar,getdate(),106))  then 'Taken' else 'Approved' end  " +
                             "when 'P' then 'Planned' when 'R' then 'Requested' when 'J' then 'Rejected' else a.Status end as Status  " +
                             "FROM [dbo].[HR_OfficeAbsence]  a  " +
                             "left outer join [dbo].[HR_LeavePurpose] b on a.[PurposeId] = b.[PurposeId]  " +
                             "left outer join [dbo].Hr_PersonalDetails pd on a.empid=pd.empid  " +
                             "left outer join [dbo].office ofice on pd.Office=ofice.OfficeId  " +
                             "left outer join [dbo].HR_Department dept on pd.Department=dept.DeptId  " +
                             "where a.status ='R' and a.ApprovalFwdTo= " + EmpId + " " +
                             ") Asd order by Asd.LeaveFrom desc";


                DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
                RptLeaveApproval.DataSource = dt;
                RptLeaveApproval.DataBind();
                lblAppCount.Text = dt.Rows.Count.ToString();     
            }
        }
        private void BindLeavesWhoIsOff()
        {
            int EmpId = Common.CastAsInt32(Session["ProfileId"]);
            if (EmpId > 0)
            {
                //----------------- For Leave -----------------------
                string WhereClause = "WHERE pd.[Status] <> 'I' ";
                if (ddlOffice.SelectedIndex > 0)
                {
                    WhereClause += " And pd.Office=" + ddlOffice.SelectedValue.Trim();
                }
                if (ddlDept.SelectedIndex > 0)
                {
                    WhereClause += " And pd.Department=" + ddlDept.SelectedValue.Trim();
                }
                
                string WhereClauseL = "AND ( ('" + txtLeaveTo.Text.Trim() + "' between a.LeaveFrom and a.LeaveTo) OR ('" + txtLeaveFrom.Text.Trim() + "' between a.LeaveFrom and a.LeaveTo) OR(('" + txtLeaveFrom.Text.Trim() + "'<= LeaveFrom and '" + txtLeaveTo.Text.Trim() + "' >= LeaveTo))) " ;
                string WhereClauseOA = "AND ( ('" + txtLeaveTo.Text.Trim() + "' between OA.LeaveFrom and OA.LeaveTo) OR ('" + txtLeaveFrom.Text.Trim() + "' between OA.LeaveFrom and OA.LeaveTo) OR(('" + txtLeaveFrom.Text.Trim() + "'<= LeaveFrom and '" + txtLeaveTo.Text.Trim() + "' >= LeaveTo))) ";
                
                string onLeaveSQL = "SELECT a.EmpId FROM HR_LeaveRequest a " +
                                    "left outer join HR_LeaveTypeMaster b on a.LeaveTypeId=b.LeaveTypeId " +
                                    "left outer join Hr_PersonalDetails pd on a.empid=pd.empid " +
                                    "left outer join office ofice on pd.Office=ofice.OfficeId " +
                                    "left outer join Position P on pd.position = P.PositionId " +
                                    "left outer join HR_Department dept on pd.Department=dept.DeptId " + WhereClause + WhereClauseL + 
                                    "GROUP BY a.EmpId ";

                DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(onLeaveSQL);                

                lbOnLeave.Text = dt.Rows.Count.ToString();

                //------------ For Business Trip ----------------

                string BTSQL = "SELECT OA.EmpId FROM HR_OfficeAbsence OA " +
                               "left outer join Hr_PersonalDetails pd on OA.empid=pd.empid " +
                               "left outer join office ofice on pd.Office=ofice.OfficeId " +
                               "left outer join Position P on pd.position = P.PositionId " +
                               "left outer join HR_Department dept on pd.Department=dept.DeptId " + WhereClause + WhereClauseOA + 
                               "GROUP BY OA.EmpId ";
                DataTable dtBT = Common.Execute_Procedures_Select_ByQueryCMS(BTSQL);
                lbOnBT.Text = dtBT.Rows.Count.ToString();

                //------------ For In Office ----------------

                string InOffSQL = "SELECT pd.EmpId FROM Hr_PersonalDetails pd " +
                              "left outer join office ofice on pd.Office=ofice.OfficeId " +
                              "left outer join Position P on pd.position = P.PositionId " +
                              "left outer join HR_Department dept on pd.Department=dept.DeptId " + WhereClause ; 
                              
               DataTable dtInOff = Common.Execute_Procedures_Select_ByQueryCMS(InOffSQL);

               lbInOffice.Text = (dtInOff.Rows.Count - (Common.CastAsInt32(dt.Rows.Count) + Common.CastAsInt32(dtBT.Rows.Count))).ToString();
               lblWhoIsOffCount.Text = (dt.Rows.Count + dtBT.Rows.Count).ToString();
            }
        }
    #endregion

    #region ---Page Load and Control Events ---
        protected void Page_Load(object sender, EventArgs e)
        {
            //-----------------------------
            SessionManager.SessionCheck_New();
            //-----------------------------
            ToDay = DateTime.Today;
            if (!Page.IsPostBack)
            {
                ControlLoader.LoadControl(ddlOffice, DataName.Office, "Office", "0");
                ControlLoader.LoadControl(ddlDept, DataName.HR_Department, "Department", "0", "OfficeId=" + Common.CastAsInt32(ddlOffice.SelectedValue));

                txtLeaveFrom.Text = ToDay.Date.ToString("dd-MMM-yyyy");
                txtLeaveTo.Text = ToDay.Date.ToString("dd-MMM-yyyy");

                ShowRecord(Common.CastAsInt32(Session["ProfileId"]));
                RenderTotalLeaves();
                RenderLeaveView();
                BindLeavesApprovalGrid();
                BindLeavesWhoIsOff();
            }
        }
        // Approval Events
        protected void btnLeaveApprovalPrint_Click(object sender, ImageClickEventArgs e)
        {
            SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "PopUPPrintWindow('" + SelectedId + "','P');", true);
        }
        protected void btnLeaveApprove_Click(object sender, ImageClickEventArgs e)
        {
            string Key = ((ImageButton)sender).CommandArgument.ToString();
            string[] Key1 = Key.Split('|');
            SelectedId = Common.CastAsInt32(Key1[0]);
            string Type = Key1[1].ToString();

            if (Type == "L")
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "PopUPWindow('" + SelectedId + "');", true);
            }
            else
            {
                int EmpId = Common.CastAsInt32(Key1[2]);
                DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT [GUID] FROM  [dbo].[HR_OfficeAbsence] WHERE LeaveRequestId = " + SelectedId + " AND EmpId = " + EmpId);
                string GUID = dt.Rows[0]["GUID"].ToString();

                ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "window.open('Public_LeaveApproval.aspx?Key=" + GUID + "', 'asdf', '');", true);
            }
        }
        //WhoisOff Events
        protected void btnWhoisOffPrint_Click(object sender, ImageClickEventArgs e)
        {
            SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
            //txtLeaveFrom
            //txtLeaveTo
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "PopUPPrintWindow('" + SelectedId + "','P');", true);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "PopUPLeaveStatusPrintWindow('" + SelectedId + "','" + txtLeaveFrom.Text + "','" + txtLeaveTo.Text + "');", true);
        }
        protected void hdnWhoIsOff_Click(object sender, EventArgs e)
        {
            BindLeavesWhoIsOff();
        }
       
        protected void btnhdnApproval_Click(object sender, EventArgs e)
        {
            SelectedId = 0; 
            BindLeavesApprovalGrid();
        }
        protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
        {
            ControlLoader.LoadControl(ddlDept, DataName.HR_Department, "Department", "0", "OfficeId=" + Common.CastAsInt32(ddlOffice.SelectedValue));
            BindLeavesWhoIsOff();
        }

        protected void HdnApplyLeave_Click(object sender, EventArgs e)
        {
            SelectedId = 0;
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "PopUPLeaveRequestWindow('" + SelectedId + "'," + ViewState["OfficeId"] + "," + ViewState["DepartmentId"] + ",'A');", true);
        }
        protected void HdnApplyAbsence_Click(object sender, EventArgs e)
        {
            SelectedId = 0;
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "PopUPOfficeAbsenceWindow(" + ViewState["OfficeId"] + "," + ViewState["DepartmentId"] + ");", true);
        }
           
        protected void UpdateOffGrid(object sender, EventArgs e)
        {
            BindLeavesWhoIsOff();
        }
    #endregion
        
}


