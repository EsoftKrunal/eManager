using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class emtm_StaffAdmin_Emtm_PopUpLeaveApproval : System.Web.UI.Page
{
    public void ShowRecord(int Id)
    {
        string sql = "select a.LeaveRequestId,pd.EmpCode,pd.FirstName +' ' + pd.MiddleName +' ' + pd.FamilyName as [Name],a.LeaveTypeId,b.LeaveTypeName, " +
                     "replace(convert(varchar,a.LeaveFrom,106),' ','-') as LeaveFrom,replace(convert(varchar,a.LeaveTo,106),' ','-') as LeaveTo,datediff(dd,a.LeaveFrom,a.LeaveTo)+ 1  as Duration, " +
                     "case a.Status when 'A' then 'Approved' when 'P' then 'Pending' when 'R' then 'Rejected' when 'C' then 'Cancelled' else a.Status end as Status,a.Reason,a.OfficeRemark from HR_LeaveRequest a left outer join HR_LeaveTypeMaster b on a.LeaveTypeId=b.LeaveTypeId " +
                     "left outer join Hr_PersonalDetails pd on a.empid=pd.empid where a.LeaveRequestId =" + Id;

        DataTable dtdata = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dtdata != null)
            if (dtdata.Rows.Count > 0)
            {
                DataRow dr = dtdata.Rows[0];
                lblEmpCode.Text=dr["EmpCode"].ToString();
                lblEmpName.Text = dr["Name"].ToString();
                lblLeaveType.Text = dr["LeaveTypeName"].ToString();
                lblLeaveFrom.Text = Convert.ToDateTime(dr["LeaveFrom"]).ToString("dd-MMM-yyyy").Trim();
                lblLeaveTo.Text = Convert.ToDateTime(dr["LeaveTo"]).ToString("dd-MMM-yyyy").Trim();
                lblReason.Text = dr["Reason"].ToString();
                txtOfficeReason.Text = dr["OfficeRemark"].ToString();
            }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!IsPostBack)
        {
            if (Request.QueryString.GetKey(0) != null)
            {
                string LeaveIdName, Mode, StatusName;
                LeaveIdName = Request.QueryString.GetKey(0);
                int LeaveRequestId = Common.CastAsInt32(Request.QueryString[LeaveIdName].Trim());

                Mode = Request.QueryString.GetKey(1);
                StatusName = Request.QueryString[Mode].Trim();

                if (StatusName == "A")
                {
                    rdoLeaveApprove.Checked = true;
                }
                else
                {
                    rdoLeaveReject.Checked = true; 
                }

                ShowRecord(LeaveRequestId);

                ViewState["LeaveRequestId"] = LeaveRequestId;
            }
        }
    }
    protected void btnDone_Click(object sender, EventArgs e)
    {
        string LeaveStatus;
        if (rdoLeaveApprove.Checked)
        {
            LeaveStatus = "A";
        }
        else
        {
            LeaveStatus = "R";
        }
        int EmpId = Common.CastAsInt32(Session["EmpId"]);
        Common.Set_Procedures("HR_InsertUpdateLeaveApproval");
        Common.Set_ParameterLength(4);
        Common.Set_Parameters(new MyParameter("@LeaveRequestId", ViewState["LeaveRequestId"]),
            new MyParameter("@Status", LeaveStatus),
            new MyParameter("@AppRejBy", Common.CastAsInt32(Session["loginid"])),
            new MyParameter("@OfficeRemark", txtOfficeReason.Text.Trim()));

        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD_CMS(ds))
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable To Save Record. Error : " + Common.getLastError() + "');", true);
        }
    }
}
