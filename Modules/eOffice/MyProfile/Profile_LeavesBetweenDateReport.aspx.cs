using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class emtm_MyProfile_Emtm_Profile_LeavesBetweenDateReport : System.Web.UI.Page
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
            if (Request.QueryString.GetKey(0) != null)
            {
                string EmpNameName,DateFromName,DateToName;

                EmpNameName = Request.QueryString.GetKey(0);
                int EmpId = Common.CastAsInt32(Request.QueryString[EmpNameName].Trim());
                ViewState["CurrentEmpId"] = Common.CastAsInt32(EmpId);

                DateFromName = Request.QueryString.GetKey(1);
                string DateFrom= Request.QueryString[DateFromName].Trim();
                ViewState["CurrentDateFrom"] = DateFrom;

                DateToName = Request.QueryString.GetKey(2);
                string DateTo = Request.QueryString[DateToName].Trim();
                ViewState["CurrentDateTo"] = DateTo;

                ShowRecord(Common.CastAsInt32(ViewState["CurrentEmpId"].ToString()));
                BindLeavesGrid();
            }
        }
    }
    public void ShowRecord(int Id)
    {
        if (Id > 0)
        {
            string sql = "SELECT a.EmpCode,a.firstname,a.middlename,a.familyname,a.Office,a.Department,c.OfficeName,a.Position,d.DeptName,b.PositionName " +
                       "FROM Hr_PersonalDetails a  " +
                       "left outer join Position b on a.Position=b.PositionId  " +
                       "Left Outer Join Office c on a.Office= c.OfficeId  " +
                       "Left Outer Join HR_Department d on a.Department=d.DeptId WHERE a.EMPID=" + Id + " ";

            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            if (dt != null)
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    lblCurrentDate.Text = ToDay.Date.ToString("dd-MMM-yyyy");
                    lblEmpName.Text = "" + dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["FamilyName"].ToString();
                    lblOffice.Text = dr["OfficeName"].ToString();
                    lblDepartment.Text = dr["DeptName"].ToString();
                    lblDesignation.Text = dr["PositionName"].ToString();
                    ViewState["OfficeId"] = dr["Office"].ToString();
                }
        }
    }
    private void BindLeavesGrid()
    {
        string sql = "select a.LeaveRequestId,a.EmpId,a.LeaveTypeId,b.LeaveTypeName,replace(convert(varchar,a.LeaveFrom,106),' ','-') as LeaveFrom, " +
                     " replace(convert(varchar,a.LeaveTo,106),' ','-') as LeaveTo,case a.HalfDay when 1 then 'First Half' when 2 then 'Second Half' else '' end as HalfDay," +
                     " a.Reason,a.Status as StatusCode,case a.Status when 'A'  then case when convert(datetime,a.LeaveTo) < convert(datetime,convert(varchar,getdate(),106))  then 'Taken' else 'Approved' end " +
                     " when 'P' then 'Plan' when 'W' then 'Awaiting Verification'  when 'v' then 'Awaiting Approval' when 'R' then 'Rejected' when 'C' then 'Cancelled' else a.Status end as Status, " +
                     " (case when a.HalfDay>0 then 0.5 else dbo.HR_Get_LeaveCount(" + ViewState["OfficeId"].ToString() + ",leavefrom,leaveto) end) as Duration " +
                     " from HR_LeaveRequest a left outer join HR_LeaveTypeMaster b on a.LeaveTypeId=b.LeaveTypeId where status='A' and empid=" + ViewState["CurrentEmpId"].ToString() + " and ((leaveFrom between '" + ViewState["CurrentDateFrom"].ToString() + "' and '" + ViewState["CurrentDateTo"].ToString() + "') or (leaveTo between '" + ViewState["CurrentDateFrom"].ToString() + "' and '" + ViewState["CurrentDateTo"].ToString() + "'))"; 
                    

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        

        if (dt.Rows.Count == 0)
        {
            divLeaveDetails.Visible = false;
        }
        else
        {
            RptLeaveRequest.DataSource = dt;
            RptLeaveRequest.DataBind();

            //SendEmail.SendeMail("adarsh@esoftech.com", "adarsh@esoftech.com", "adarsh@esoftech.com", "", "", "Hello Testing", "Hi This is Test Message", "", ""); 

        }
    }
 
}

