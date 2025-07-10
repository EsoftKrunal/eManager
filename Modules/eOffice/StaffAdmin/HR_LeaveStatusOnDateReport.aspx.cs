using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class emtm_StaffAdmin_Emtm_HR_LeaveStatusOnDateReport : System.Web.UI.Page
{
    DateTime ToDay;
    public int Emp_Id
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
                string CurrentYearName,EmpName;
              
                CurrentYearName = Request.QueryString.GetKey(0);
                int CurrentYear = Common.CastAsInt32(Request.QueryString[CurrentYearName].Trim());
                ViewState["CurrentYear"] = Common.CastAsInt32(CurrentYear);

                EmpName = Request.QueryString.GetKey(1);
                int EmpId = Common.CastAsInt32(Request.QueryString[EmpName].Trim());
                Emp_Id = Common.CastAsInt32(EmpId);

                lblLastYear.Text = (Common.CastAsInt32(ViewState["CurrentYear"]) - 1).ToString();
                lblCurYear.Text = ViewState["CurrentYear"].ToString() ;
                lblCurYear1.Text = ViewState["CurrentYear"].ToString();
                lblCurYear4.Text = ViewState["CurrentYear"].ToString();
                lblCurYear3.Text = ViewState["CurrentYear"].ToString();
                lblCurYear2.Text = ViewState["CurrentYear"].ToString();

                ShowRecord(Emp_Id);
                BindGrid();
                BindLeavesGrid();
            }
        }
    }
    private void BindGrid()
    {
            string sql = "select a.LeaveTypeId,a.LeaveTypeName,isnull(b.LeaveCount,0) as LeaveCount, " +
                        "dbo.HR_Get_AnnLeavesConsumed_Month(1," + ViewState["CurrentYear"].ToString() + ",a.LeaveTypeId," + Emp_Id + ") as 'Jan', " +
                        "dbo.HR_Get_AnnLeavesConsumed_Month(2," + ViewState["CurrentYear"].ToString() + ",a.LeaveTypeId," + Emp_Id + ") as 'Feb', " +
                        "dbo.HR_Get_AnnLeavesConsumed_Month(3," + ViewState["CurrentYear"].ToString() + ",a.LeaveTypeId," + Emp_Id + ") as 'March', " +
                        "dbo.HR_Get_AnnLeavesConsumed_Month(4," + ViewState["CurrentYear"].ToString() + ",a.LeaveTypeId," + Emp_Id + ") as 'April', " +
                        "dbo.HR_Get_AnnLeavesConsumed_Month(5," + ViewState["CurrentYear"].ToString() + ",a.LeaveTypeId," + Emp_Id + ") as 'May', " +
                        "dbo.HR_Get_AnnLeavesConsumed_Month(6," + ViewState["CurrentYear"].ToString() + ",a.LeaveTypeId," + Emp_Id + ") as 'June', " +
                        "dbo.HR_Get_AnnLeavesConsumed_Month(7," + ViewState["CurrentYear"].ToString() + ",a.LeaveTypeId," + Emp_Id + ") as 'July', " +
                        "dbo.HR_Get_AnnLeavesConsumed_Month(8," + ViewState["CurrentYear"].ToString() + ",a.LeaveTypeId," + Emp_Id + ") as 'August', " +
                        "dbo.HR_Get_AnnLeavesConsumed_Month(9," + ViewState["CurrentYear"].ToString() + ",a.LeaveTypeId," + Emp_Id + ") as 'September', " +
                        "dbo.HR_Get_AnnLeavesConsumed_Month(10," + ViewState["CurrentYear"].ToString() + ",a.LeaveTypeId," + Emp_Id + ") as 'October', " +
                        "dbo.HR_Get_AnnLeavesConsumed_Month(11," + ViewState["CurrentYear"].ToString() + ",a.LeaveTypeId," + Emp_Id + ") as 'November', " +
                        "dbo.HR_Get_AnnLeavesConsumed_Month(12," + ViewState["CurrentYear"].ToString() + ",a.LeaveTypeId," + Emp_Id + ") as 'December', " +
                        "dbo.HR_Get_AnnLeavesConsumed_Year_LeaveType(" + ViewState["CurrentYear"].ToString() + ",a.LeaveTypeId," + Emp_Id + ") as 'Total' " +
                        "from HR_LeaveTypeMaster a left join HR_LeaveAssignment b on a.LeaveTypeId=b.LeaveTypeId and b.year=" + ViewState["CurrentYear"].ToString() + " and b.EmpId=" + Emp_Id + " " +
                        "where a.LeaveTypeId <>-1 and a.LeaveTypeId in(select LeaveTypeId from HR_OfficeLeaveMapping where OfficeId=" + ViewState["OfficeId"] + ") order by a.LeaveTypeName";

            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);

            if (dt.Rows.Count == 0)
            {
                divLeaveTypeSummary.Visible = false;
            }
            else
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    for (int j = 3; j <= 15; j++)
                    {
                        if (dt.Rows[i][j].ToString() == "0.0")
                        {
                            dt.Rows[i][j] = DBNull.Value;
                        }
                    }
                }

                rptLeaveDetails.DataSource = dt;
                rptLeaveDetails.DataBind();
            }
    }
    private void BindLeavesGrid()
    {
        string sql = "select a.LeaveRequestId,a.EmpId,a.LeaveTypeId,b.LeaveTypeName,replace(convert(varchar,a.LeaveFrom,106),' ','-') as LeaveFrom, " +
                     " replace(convert(varchar,a.LeaveTo,106),' ','-') as LeaveTo,case a.HalfDay when 1 then 'First Half' when 2 then 'Second Half' else '' end as HalfDay," +
                     " a.Reason,a.Status as StatusCode,case a.Status when 'A' then 'Approved' when 'P' then 'Plan' when 'W' then 'Awaiting Verification'  when 'v' then 'Awaiting Approval' when 'R' then 'Rejected' else a.Status end as Status, " +
                     " (case when a.HalfDay>0 then 0.5 else dbo.HR_Get_LeaveCount(" + ViewState["OfficeId"].ToString() + ",leavefrom,leaveto) end) as Duration " +
                     " from HR_LeaveRequest a left outer join HR_LeaveTypeMaster b on a.LeaveTypeId=b.LeaveTypeId where year(a.Leavefrom)=" + ViewState["CurrentYear"].ToString().Trim() + " and EmpId=" + Emp_Id + " " +
                     " and year(a.LeaveTo)=" + ViewState["CurrentYear"].ToString().Trim() + "and a.status <>'P' order by a.LeaveFrom desc";

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        if (dt.Rows.Count == 0)
        {
            divLeaveDetails.Visible = false; 
        }
        else
        {
            RptLeaveRequest.DataSource = dt;
            RptLeaveRequest.DataBind();
        }
    }
    private void BindLeavesGrid(string Month, string LeaveType)
    {
        string sql = "";
        if (Month == "0")
        {
            sql = "select a.LeaveRequestId,a.EmpId,a.LeaveTypeId,b.LeaveTypeName,replace(convert(varchar,a.LeaveFrom,106),' ','-') as LeaveFrom, " +
            " replace(convert(varchar,a.LeaveTo,106),' ','-') as LeaveTo,case a.HalfDay when 1 then 'First Half' when 2 then 'Second Half' else '' end as HalfDay," +
            " a.Reason,a.Status as StatusCode,case a.Status when 'A' then 'Approved' when 'P' then 'Plan' when 'W' then 'Avaiting Verification'  when 'v' then 'Verified' when 'R' then 'Rejected' else a.Status end as Status,a.VerifiyRemarks, " +
            " (case when a.HalfDay>0 then 0.5 else dbo.HR_Get_LeaveCount(" + ViewState["OfficeId"].ToString() + ",leavefrom,leaveto) end) as Duration " +
            " from HR_LeaveRequest a left outer join HR_LeaveTypeMaster b on a.LeaveTypeId=b.LeaveTypeId " +
            " where EmpId=" + Emp_Id + " and year(LeaveFrom)=" + ViewState["CurrentYear"].ToString() + " and a.Status<>'P' and a.LeaveTypeId=" + LeaveType + " order by a.LeaveFrom desc";

        }
        else
        {
            sql = "select a.LeaveRequestId,a.EmpId,a.LeaveTypeId,b.LeaveTypeName,replace(convert(varchar,a.LeaveFrom,106),' ','-') as LeaveFrom, " +
            " replace(convert(varchar,a.LeaveTo,106),' ','-') as LeaveTo,case a.HalfDay when 1 then 'First Half' when 2 then 'Second Half' else '' end as HalfDay," +
            " a.Reason,a.Status as StatusCode,case a.Status when 'A' then 'Approved' when 'P' then 'Plan' when 'W' then 'Avaiting Verification'  when 'v' then 'Verified' when 'R' then 'Rejected' else a.Status end as Status,a.VerifiyRemarks, " +
            " (case when a.HalfDay>0 then 0.5 else dbo.HR_Get_LeaveCount(" + ViewState["OfficeId"].ToString() + ",leavefrom,leaveto) end) as Duration " +
            " from HR_LeaveRequest a left outer join HR_LeaveTypeMaster b on a.LeaveTypeId=b.LeaveTypeId " +
            " where EmpId=" + Emp_Id + " and " + Month + " between month(LeaveFrom) and month(LeaveTO) and year(LeaveFrom)=" + ViewState["CurrentYear"].ToString() + " and a.Status<>'P' and a.LeaveTypeId=" + LeaveType + " order by a.LeaveFrom desc";

        }
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        RptLeaveRequest.DataSource = dt;
        RptLeaveRequest.DataBind();
    }
    public void ShowRecord(int Id)
    {
        if (Id > 0)
        {
            string ONdate;

            
            if (ToDay.Year == Common.CastAsInt32(ViewState["CurrentYear"]))
                ONdate=DateTime.Today.ToString("dd-MMM-yyyy"); 
            else
                ONdate=DateTime.Today.ToString("31-Dec-" + ViewState["CurrentYear"].ToString() );

            string sqlLC = "SELECT DBO.HR_Get_LeavesCredit(" + Id + "," + ONdate.Substring(7, 4) + ")";
            DataTable dtLeaveCredit = Common.Execute_Procedures_Select_ByQueryCMS(sqlLC);

            int LeaveCredit = Common.CastAsInt32(dtLeaveCredit.Rows[0][0].ToString());

            string sql = "SELECT a.EmpCode,a.firstname,a.middlename,a.familyname,a.Office,a.Department,c.OfficeName,a.Position,d.DeptName,b.PositionName,ss.* " +
                       "FROM Hr_PersonalDetails a  " +
                       "left join (select * from dbo.getLeaveStatus_OnDate(" + Id + ",'" + ONdate + "')) ss on a.empid=ss.empid " +
                       "left outer join Position b on a.Position=b.PositionId  " +
                       "Left Outer Join Office c on a.Office= c.OfficeId  " +
                       "Left Outer Join HR_Department d on a.Department=d.DeptId WHERE a.EMPID=" + Id + " ";

            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            if (dt != null)
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    lblEmpName.Text = "" + dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["FamilyName"].ToString();
                    lblOffice.Text = dr["OfficeName"].ToString();
                    lblDepartment.Text = dr["DeptName"].ToString();
                    lblDesignation.Text = dr["PositionName"].ToString();
                    lblBalLeaveLastYear.Text = dr["BalLeaveLast"].ToString();
                    lblAnnualLeaveEntitlement.Text = string.Format("{0:0.0}", (Convert.ToDouble(dr["AnnLeave"]) + Convert.ToDouble(dr["LieuOffLeave"])));
                    lblLeavesTaken.Text = string.Format("{0:0.0}", (Convert.ToDouble(dr["ConsLeave"]) + Convert.ToDouble(dr["LieuOffLeave"])));

                    lblLeaveCredit.Text = LeaveCredit.ToString();

                    lblAccruedLeave.Text = string.Format("{0:0.0}",(Common.CastAsDecimal(dr["PayableLeave"])));// + LeaveCredit));
                    lblLeaveExpired.Text = dr["BalLeaveLastExpired"].ToString();
                    lblCurrentYear.Text = ViewState["CurrentYear"].ToString();
                    ViewState["OfficeId"] = dr["Office"].ToString();
                }
        }
    }
}
