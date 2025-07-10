using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class emtm_ActionResult : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        int Action = Common.CastAsInt32(Request.QueryString["key"]);
        switch (Action)
        {
            case 1:
                pnl1.Visible = true;
                int st=Request.QueryString["data"].IndexOf("_") ;
                int end=Request.QueryString["data"].LastIndexOf("_") ;
                int TableId =Common.CastAsInt32(Request.QueryString["data"].Substring(st+1, end -st-1));
                ShowDataPnl1(TableId);
                break;
            case 2:
                break;
            default :
                break;
        }
    }

    protected void ShowDataPnl1(int TableId)
    {

        string sql = "select a.LeaveRequestId,pd.EmpCode,pd.FirstName +' ' + pd.MiddleName +' ' + pd.FamilyName as [Name], "+
                     "pd.Office,pd.Department,c.OfficeName,pd.Position,dept.DeptName,p.PositionName, "+	            
                     "a.LeaveTypeId,b.LeaveTypeName, " +
                     "replace(convert(varchar,a.LeaveFrom,106),' ','-') as LeaveFrom,replace(convert(varchar,a.LeaveTo,106),' ','-') as LeaveTo," +
                     "year(a.LeaveFrom) as [Year],Month(a.LeaveFrom)as [Month],"+
                     "case a.Status when 'A'  then case when convert(datetime,a.LeaveTo) < convert(datetime,convert(varchar,getdate(),106))  then 'Taken' else 'Approved' end " +
                     "when 'P' then 'Plan' when 'V' then 'Awaiting Approval' when 'R' then 'Rejected' when 'C' then 'Cancelled' else a.Status end as Status, " +
                     "(case when a.HalfDay>0 then 0.5 else dbo.HR_Get_LeaveCount(pd.office,leavefrom,leaveto) end) as Duration, " +
                     "a.Reason,a.AppRejRemark,a.HalfDay, dbo.getAbsentDays(a.empid,a.LeaveFrom,a.LeaveTo) as AbsentDays, dbo.getAbsentDates(a.empid,a.LeaveFrom,a.LeaveTo) as AbsentDates,a.RequestDate, " +
                     "(SELECT FIRSTNAME + ' ' + FAMILYNAME FROM Hr_PersonalDetails p WHERE p.EMPID=a.ForwardedTo) as ApproverName, " +
                     "(select Comments from  HR_Leave_Request_Comments where leaverequestid=a.LeaveRequestId and empid=a.ForwardedTo) as ApproverComments " +
                     "from HR_LeaveRequest a left outer join HR_LeaveTypeMaster b on a.LeaveTypeId=b.LeaveTypeId " +
                     "left outer join Hr_PersonalDetails pd on a.empid=pd.empid "+
                     "left outer join Position p on pd.Position=p.PositionId "+ 
                     "Left Outer Join Office c on pd.Office= c.OfficeId "+ 
                     "Left Outer Join HR_Department dept on pd.Department=dept.DeptId "+
                     "where a.LeaveRequestId in(select LeaveRequestId from HR_Leave_Request_Comments where tableid=" + TableId.ToString() + ")";
        
        DataTable dtdata = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dtdata != null)
            if (dtdata.Rows.Count > 0)
            {
                DataRow dr = dtdata.Rows[0];
                ViewState.Add("TableId", TableId);
                //lblEmpCode.Text=dr["EmpCode"].ToString();
                lblEmpName.Text = dr["Name"].ToString();
                lblOffice.Text = dr["OfficeName"].ToString();
                lblDepartment.Text = dr["DeptName"].ToString();
                lblDesignation.Text = dr["PositionName"].ToString();
                ViewState["LeaveTypeId"] = dr["LeaveTypeId"].ToString();
                ViewState["RequestDate"] = dr["RequestDate"].ToString();

                lblLeaveType.Text = dr["LeaveTypeName"].ToString();
                lblLeaveFrom.Text = Convert.ToDateTime(dr["LeaveFrom"]).ToString("dd-MMM-yyyy").Trim();
                lblLeaveTo.Text = Convert.ToDateTime(dr["LeaveTo"]).ToString("dd-MMM-yyyy").Trim();
                lblLeaveStatus.Text = dr["Status"].ToString();
                lblLeaveDays.Text = "( " + dr["Duration"].ToString() + " Days)";
                
                lblReason.Text = dr["Reason"].ToString();
                lblMDComments.Text = dr["ApproverComments"].ToString();

                if (dr["HalfDay"].ToString() != "0")
                {
                    lblAbsentDays.Text = "0 (Days)";
                    lblAbsentDays.Text = "0 (Days)";
                }
                else
                {
                    //lblAbsentDays.Text = dr["AbsentDays"].ToString() + " (Days)";
                    lblAbsentDays.Text = dr["AbsentDates"].ToString() + " &nbsp;" + dr["AbsentDays"].ToString() + " (Days)";
                }

                lblHalfDay.Text = (Common.CastAsInt32(dr["HalfDay"]) == 1) ? "[ First Half ]" : ((Common.CastAsInt32(dr["HalfDay"]) == 2) ? "[ Second Half ]" : "");

                if (Convert.ToDateTime(lblLeaveFrom.Text).Year < DateTime.Today.Year)
                {
                    EoffYearAndMonthLoader.SelectedYear = DateTime.Today.Year;
                    EoffYearAndMonthLoader.SelectedMonth = DateTime.Today.Month;  
                }
                else
                {
                    EoffYearAndMonthLoader.SelectedYear = Common.CastAsInt32(dr["Year"].ToString());
                    EoffYearAndMonthLoader.SelectedMonth = Common.CastAsInt32(dr["Month"].ToString());  
                }
            }
    }
    protected void btnPnl1Save_Click(object sender, EventArgs e)
    {
        Common.Execute_Procedures_Select_ByQueryCMS("UPDATE HR_Leave_Request_Comments SET COMMENTS='" + txtComments.Text.Trim() + "' WHERE TABLEID=" + ViewState["TableId"].ToString());
        lblMessage.Text = "Your Comments saved successfully.";
    }
}