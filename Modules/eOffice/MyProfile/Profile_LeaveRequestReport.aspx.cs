using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class emtm_MyProfile_Emtm_Profile_LeaveRequestReport : System.Web.UI.Page
{
    DateTime ToDay;

    private void ShowReportData(int LeaveRequestId)
    {
         

           string sql = "select a.LeaveRequestId,a.EmpId,main.EmpCode,main.firstname +' ' +main.middlename +' ' +main.familyname as EmployeeName,a.LeaveTypeId,b.LeaveTypeName," +
                         "replace(convert(varchar,a.LeaveFrom,106),' ','-') as LeaveFrom,replace(convert(varchar,a.LeaveTo,106),' ','-') as LeaveTo,"+
                         "year(LeaveFrom)as LeaveYear,year(LeaveFrom) -1 as LastYear,case isnull(a.HalfDay,0) when 1 then 'First Half' when 2 then 'Second Half' else '' end as HalfDay,a.Reason,a.Status as StatusCode," +
                         "case a.Status when 'A'  then case when convert(datetime,a.LeaveTo) < convert(datetime,convert(varchar,getdate(),106))  then 'Taken' else 'Approved' end " +
                         "when 'P' then 'Plan' when 'V' then 'Awaiting Approval' when 'R' then 'Rejected' when 'C' then 'Cancelled' else a.Status end as Status, " +
                         "isnull((select main.firstname +' ' +main.middlename +' ' +main.familyname from Hr_PersonalDetails main where main.empid=a.ForwardedTo),'')ForwardedTo, "+
                         "isnull((select main.firstname +' ' +main.middlename +' ' +main.familyname from Hr_PersonalDetails main where main.empid=a.AppRejBy),'')AppRejBy, " +
                         "replace(convert(varchar,a.AppRejDate,106),' ','-')AppRejDate,a.AppRejRemark,o.officeName, " +
                         "replace(convert(varchar,a.RequestDate,106),' ','-')RequestDate, " +
                         "p.PositionName,d.DeptName,(case when a.HalfDay >0 then 0.5 else dbo.HR_Get_LeaveCount(o.OfficeId,a.LeaveFrom,a.LeaveTO) end) as Duration " +
                         "from HR_LeaveRequest a left outer join Hr_PersonalDetails main "+
                         "on a.empid=main.empid "+
                         "left outer join HR_LeaveTypeMaster b " +
                         "on a.LeaveTypeId=b.LeaveTypeId "+
                         "left outer join Position p on main.Position=p.PositionId Left Outer Join Office o on main.Office= o.OfficeId "+ 
                         "Left Outer Join HR_Department d on main.Department=d.DeptId  "+
                         "where a.LeaveRequestId="+ LeaveRequestId.ToString() +" "+
                         "order by a.LeaveFrom desc";

            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            if (dt != null)
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];

                    string sqlLC = "SELECT DBO.HR_Get_LeavesCredit(" + dr["EmpId"].ToString() + "," + DateTime.Today.Date.ToString("yyyy") + ")";
                    DataTable dtLeaveCredit = Common.Execute_Procedures_Select_ByQueryCMS(sqlLC);
                    Decimal LeaveCreditCount = 0;

                    // This Section has User Leave Data
                    lblEmpCode.Text = dr["EmpCode"].ToString();
                    lblEmpName.Text = dr["EmployeeName"].ToString();

                    lblLeaveType.Text = dr["LeaveTypeName"].ToString();
                    lblLeaveFrom.Text = dr["LeaveFrom"].ToString();
                    lblLeaveTo.Text = dr["LeaveTo"].ToString();
                    lblLeaveDays.Text = dr["Duration"].ToString();
                    lblRequestedOn.Text = dr["RequestDate"].ToString();
                    lblLeaveStatus.Text = dr["Status"].ToString();
                    //lblOfficeName.Text = dr["officeName"].ToString();
                    lblPositon.Text = dr["PositionName"].ToString();
                    lblDepartment.Text = dr["DeptName"].ToString();
                    lblLeaveCredit.Text = "0.0";

                    if (dr["HalfDay"].ToString() == "")
                    {
                        lblHalfDayText.Visible = false;
                    }
                    else
                    {
                        lblHalfDay.Text = " ( " + dr["HalfDay"].ToString() + " )";
                    }
         
                    lblUserRemark.Text = dr["Reason"].ToString();
                    //lblOfficeName.Text = dr["OfficeName"].ToString();
                    lblForwardedTO.Text = dr["ForwardedTo"].ToString();

                    lblLastYear.Text = dr["LastYear"].ToString();
                    lblCurrYear.Text = dr["LeaveYear"].ToString();
                    lblCurrYear3.Text = dr["LeaveYear"].ToString();
                    lblDateofApplication.Text = dr["RequestDate"].ToString();

                    int LeaveTypeId=Common.CastAsInt32(dr["LeaveTypeId"].ToString());

                    if (LeaveTypeId == 1) // IF ANNUAL LEAVE TYPE
                    {
                        LeaveCreditCount=Common.CastAsDecimal(dtLeaveCredit.Rows[0][0]);
                        lblLeaveCredit.Text = string.Format("{0:0.0}", LeaveCreditCount);

                        string CalcDate=lblLeaveTo.Text;
                        if(DateTime.Parse(lblLeaveTo.Text).Year > DateTime.Today.Date.Year)
                        {
                            CalcDate = "31-dec-" + DateTime.Today.Date.Year.ToString();
                        }

                        string sql1 = "select * from dbo.getLeaveStatus_OnDate(" + dr["EmpId"] + ",'" + CalcDate + "')";
                        DataTable dt1 = Common.Execute_Procedures_Select_ByQueryCMS(sql1);
                        if (dt1 != null)
                            if (dt1.Rows.Count > 0)
                            {
                                DataRow dr1 = dt1.Rows[0];
                                lblLeaveLastYear.Text = dr1["BalLeaveLast"].ToString();
                                lblLeaveAnnualEntitlement.Text = dr1["Annleave"].ToString();
                                lblLeaveBalace.Text = string.Format("{0:0.0}", (Convert.ToDouble(dr1["BalLeaveLast"]) - Convert.ToDouble(dr1["BalLeaveLastExpired"]) + Convert.ToDouble(dr1["AnnLeave_TillDate"]) - Convert.ToDouble(dr1["ConsLeave"])));
                                lblLeaveExpired.Text = dr1["BalLeaveLastExpired"].ToString();
                                lblLeavesConsumed.Text = string.Format("{0:0.0}", (Convert.ToDouble(dr1["ConsLeave"]) + Convert.ToDouble(dr1["LieuOffLeave"])));
                            }
                    }
                    else // IF OTHER LEAVE TYPE
                    {
                        string sql1 = "select * from dbo.getLeaveStatusByLeaveType_OnDate(" + dr["EmpId"] + ",'" + lblLeaveTo.Text + "'," + LeaveTypeId.ToString() + ")";
                        DataTable dt1 = Common.Execute_Procedures_Select_ByQueryCMS(sql1);
                        if (dt1 != null)
                            if (dt1.Rows.Count > 0)
                            {
                                DataRow dr1 = dt1.Rows[0];
                                lblLeaveLastYear.Text = "0.0";
                                
                                lblLeaveAnnualEntitlement.Text = dr1["AnnualLeave"].ToString();
                                if (LeaveTypeId == 2)
                                    lblLeaveBalace.Text = "0.0";
                                else
                                    lblLeaveBalace.Text = string.Format("{0:0.0}", (Convert.ToDouble(dr1["AnnualLeave_TillDate"]) - Convert.ToDouble(dr1["Consumed_Tilldate"])));
                                lblLeaveExpired.Text = "0.0";
                                lblLeavesConsumed.Text = dr1["Consumed_Tilldate"].ToString(); 
                            }
                    }
                    //---------------------- new line to add leave credit 
                    lblLeaveBalace.Text = string.Format("{0:0.0}", Common.CastAsDecimal(lblLeaveBalace.Text) + LeaveCreditCount);
                   // This Section has HOD Leave Data
                    if (dr["AppRejDate"].ToString() == "")
                    {
                        tblLeaveHOD.Visible = false;
                    }
                    else
                    {
                        lblAppRejBY.Text = dr["AppRejBy"].ToString();
                        lblAppRejOn.Text = dr["AppRejDate"].ToString();
                        lblAppRejRemark.Text = dr["AppRejRemark"].ToString();
                    }
                }
        
        //----------------------------- SHOW LEAVE SUMMARY AS ON LEAVE DATE


    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        ToDay = DateTime.Today;

        if (!Page.IsPostBack)
        {
            if (Request.QueryString.GetKey(0) != null)
            {
                string LeaveRequestName;
                LeaveRequestName = Request.QueryString.GetKey(0);
                int LeaveRequestId = Common.CastAsInt32(Request.QueryString[LeaveRequestName].Trim());

                ShowReportData(Common.CastAsInt32(LeaveRequestId));
            }
        }
    }
}
