using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class emtm_StaffAdmin_Emtm_HR_LeaveAssignedPrint : System.Web.UI.Page
{
    DateTime ToDay;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        ToDay = DateTime.Today;
        if (!Page.IsPostBack)
        {
            ShowRecord();
            BindGrid();
            BindLeavesDetails();
        }
    }
    private void BindGrid()
    {
        int EmpId = Common.CastAsInt32(Session["EmpId"]);
        string sql = "select lm.leavetypeid,leavetypename,isnull(la.leavecount ,0) as LeaveCount from HR_LeaveTypeMaster lm inner join HR_OfficeLeaveMapping lmap on lm.leavetypeid=lmap.leavetypeid and lmap.officeid=" + ViewState["OfficeId"] + " " +
                     "left outer join HR_LeaveAssignment la on lmap.leavetypeid=la.leavetypeid and la.empid=" + EmpId +
                     " where lm.leavetypeid<>2 order by lm.leavetypename";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dt.Rows.Count == 0)
        {
            divLeaveSummary.Visible = false;
        }
        else
        {
            rptLeaveDetails.DataSource = dt;
            rptLeaveDetails.DataBind();
        }
    }
    private void BindLeavesDetails()
    {
        int EmpId = Common.CastAsInt32(Session["EmpId"]);
        if (EmpId > 0)
        {
            string sql = "select a.LeaveRequestId,pd.EmpCode,pd.FirstName +' ' + pd.MiddleName +' ' + pd.FamilyName as [Name],a.LeaveTypeId,b.LeaveTypeName," +
                         "replace(convert(varchar,a.LeaveFrom,106),' ','-') as LeaveFrom,replace(convert(varchar,a.LeaveTo,106),' ','-') as LeaveTo,(case when a.HalfDay>0 then 0.5 else dbo.HR_Get_LeaveCount(pd.office,leavefrom,leaveto) end) as Duration , " +
                         "case a.Status when 'A' then 'Approved' when 'P' then 'Plan' when 'W' then 'Awaiting Approval'  when 'v' then 'Verified' when 'R' then 'Rejected' else a.Status end as Status " +
                         "from HR_LeaveRequest a left outer join HR_LeaveTypeMaster b on a.LeaveTypeId=b.LeaveTypeId " +
                         "left outer join Hr_PersonalDetails pd on a.empid=pd.empid where a.HR_USER='H' and a.empid=" + EmpId + " and a.status<> 'P' order by a.LeaveFrom desc";
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            if (dt.Rows.Count == 0)
            {
                divLeaveDetails.Visible = false;
            }
            else
            {
                RptLeaves.DataSource = dt;
                RptLeaves.DataBind();
            }
        }
    }
    public void ShowRecord()
    {
        int EmpId = Common.CastAsInt32(Session["EmpId"]);
        if (EmpId > 0)
        {
            string sql = "SELECT a.EmpCode,a.firstname,a.middlename,a.familyname,a.Office,a.Department,c.OfficeName,a.Position,d.DeptName,b.PositionName,ss.* " +
                       "FROM Hr_PersonalDetails a  " +
                       "left join (select * from dbo.getLeaveStatus_OnDate(" + EmpId + ",'" + ToDay.Date.ToString("dd-MMM-yyyy") + "')) ss on a.empid=ss.empid " +
                       "left outer join Position b on a.Position=b.PositionId  " +
                       "Left Outer Join Office c on a.Office= c.OfficeId  " +
                       "Left Outer Join HR_Department d on a.Department=d.DeptId WHERE a.EMPID=" + EmpId + " ";

            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            if (dt != null)
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    //lblCurrentDate.Text = ToDay.Date.ToString("dd-MMM-yyyy");
                    lblEmpName.Text = "" + dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["FamilyName"].ToString();
                    lblOffice.Text = dr["OfficeName"].ToString();
                    lblDepartment.Text = dr["DeptName"].ToString();
                    lblDesignation.Text = dr["PositionName"].ToString();
                    ViewState["OfficeId"] = dr["Office"].ToString();
                }
        }
    }
}
