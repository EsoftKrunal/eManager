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
        string sql = "select a.EmpId,pd.UserId,a.LeaveRequestId,pd.EmpCode,pd.FirstName +' ' + pd.MiddleName +' ' + pd.FamilyName as [Name],a.LeaveTypeId,b.LeaveTypeName, " +
                     "replace(convert(varchar,a.LeaveFrom,106),' ','-') as LeaveFrom,replace(convert(varchar,a.LeaveTo,106),' ','-') as LeaveTo,a.Status as StatusCode," +
                     "case a.Status when 'A'  then case when convert(datetime,a.LeaveTo) < convert(datetime,convert(varchar,getdate(),106))  then 'Taken' else 'Approved' end " +
                     "when 'P' then 'Plan' when 'V' then 'Awaiting Approval' when 'R' then 'Rejected' when 'C' then 'Cancelled' else a.Status end as Status, " +
                     "(case when a.HalfDay>0 then 0.5 else dbo.HR_Get_LeaveCount(pd.office,leavefrom,leaveto) end) as Duration, " +
                     "a.Reason,a.ForwardedTo,a.AppRejRemark,a.HalfDay ,dbo.getAbsentDays(a.EmpId,a.LeaveFrom,a.LeaveTo) as AbsentDays from HR_LeaveRequest a left outer join HR_LeaveTypeMaster b on a.LeaveTypeId=b.LeaveTypeId " +
                     "left outer join Hr_PersonalDetails pd on a.empid=pd.empid where a.LeaveRequestId =" + Id;

        DataTable dtdata = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dtdata != null)
            if (dtdata.Rows.Count > 0)
            {
                DataRow dr = dtdata.Rows[0];
                lblEmpCode.Text = dr["EmpCode"].ToString();
                lblEmpName.Text = dr["Name"].ToString();
                lblLeaveType.Text = dr["LeaveTypeName"].ToString();
                lblLeaveFrom.Text = Convert.ToDateTime(dr["LeaveFrom"]).ToString("dd-MMM-yyyy").Trim();
                lblLeaveTo.Text = Convert.ToDateTime(dr["LeaveTo"]).ToString("dd-MMM-yyyy").Trim();
                lblduration.Text = dr["duration"].ToString();
                lblReason.Text = dr["Reason"].ToString();
                txtVerifyRemarks.Text = dr["AppRejRemark"].ToString();
                ViewState["EmpId"] = dr["EmpId"].ToString();
                ViewState["UserId"] = dr["UserId"].ToString();
                if (dr["HalfDay"].ToString() != "0")
                {
                    lblAbsentDays.Text = "0 (Days)";
                }
                else
                {
                    lblAbsentDays.Text = dr["AbsentDays"].ToString() + " (Days)";
                }

                if (dr["StatusCode"].ToString() != "A")
                {
                    PanelVerification.Visible = true;
                    PanelCancelation.Visible = false;
                }
                else
                {
                    PanelCancelation.Visible = true;
                    PanelVerification.Visible = false;
                }


                string SqlFwd = "select FirstName +' ' + MiddleName +' ' + FamilyName as [Name] from Hr_PersonalDetails where empid=" + Common.CastAsInt32(dr["ForwardedTo"]) + "";
                DataTable dtFwd = Common.Execute_Procedures_Select_ByQueryCMS(SqlFwd);
                if (dtFwd != null)
                    if (dtFwd.Rows.Count > 0)
                    {
                        DataRow drFwd = dtFwd.Rows[0];
                        lblForwardedTo.Text = drFwd["Name"].ToString();
                    }
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
                ShowRecord(LeaveRequestId);
                ViewState["LeaveRequestId"] = LeaveRequestId;
            }
        }
    }
    protected void btnDone_Click(object sender, EventArgs e)
    {
        string LeaveStatus;
        if (rdoLeaveVerify.Checked)
        {
            LeaveStatus = "A";
        }
        else if (rdoLeaveReject.Checked)
        {
            LeaveStatus = "R";
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Please Select Any One.');", true);
            return;
        }

        Common.Set_Procedures("HR_UpdateRequestLeave");
        Common.Set_ParameterLength(4);
        Common.Set_Parameters(new MyParameter("@LeaveRequestId", ViewState["LeaveRequestId"]),
            new MyParameter("@Status", LeaveStatus.Trim()),
            new MyParameter("@ActionById", Common.CastAsInt32(Session["ProfileId"])),
            new MyParameter("@ActionRemark", txtVerifyRemarks.Text.Trim()));

        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD_CMS(ds))
        {
            SendMailVerifyReject(); 
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');window.close();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable To Save Record. Error : " + Common.getLastError() + "');", true);
        }
    }
    protected void btnCancelation_Click(object sender, EventArgs e)
    {
        int EmpId = Common.CastAsInt32(Session["EmpId"]);
        Common.Set_Procedures("HR_UpdateRequestLeave");
        Common.Set_ParameterLength(4);
        Common.Set_Parameters(new MyParameter("@LeaveRequestId", ViewState["LeaveRequestId"]),
            new MyParameter("@Status", "C"),
            new MyParameter("@ActionById", Common.CastAsInt32(Session["ProfileId"])),
            new MyParameter("@ActionRemark", txtCancelationRemark.Text.Trim()));
        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD_CMS(ds))
        {
            SendMailCancel();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');window.close();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable To Save Record. Error : " + Common.getLastError() + "');", true);
        }
    }
    public void SendMailVerifyReject()
    {
        string MailFrom = "", MailTo = "";

        //Mail From
        string sqlGetMailFrom = "SELECT C.Email FROM USERLOGIN C where C.LoginId=" +  Session["LoginId"].ToString();

        DataTable dtGetMailFrom = Common.Execute_Procedures_Select_ByQueryCMS(sqlGetMailFrom);
        if (dtGetMailFrom != null)
            if (dtGetMailFrom.Rows.Count > 0)
            {
                DataRow drGetMailFrom = dtGetMailFrom.Rows[0];
                MailFrom = drGetMailFrom["Email"].ToString();
            }

        String EmpFullName = "", EmpPosition = "";
        string sqlFullNameNPosition = "SELECT (select PositionName from Position P where P.PositionID=pd.Position)Position,(pd.FirstName+' '+pd.FamilyName )as UserName FROM Hr_PersonalDetails pd LEFT OUTER JOIN USERLOGIN C ON pd.userid=C.LoginId where C.LoginId=" + Session["LoginId"].ToString() + "";
        DataTable dtNamePos = Common.Execute_Procedures_Select_ByQueryCMS(sqlFullNameNPosition);
        if (dtNamePos != null)
            if (dtNamePos.Rows.Count > 0)
            {
                EmpPosition = dtNamePos.Rows[0][0].ToString();
                EmpFullName = dtNamePos.Rows[0][1].ToString();
            }


        //Mail To
        string sqlGetMailTo = "SELECT A.EMPID,C.Email FROM HR_LeaveRequest A " +
                          "LEFT OUTER JOIN Hr_PersonalDetails B ON A.EMPID=B.EMPID " +
                          "LEFT OUTER JOIN USERLOGIN C ON B.USERID=C.LoginId " +
                          "WHERE A.LeaveRequestId=" + ViewState["LeaveRequestId"].ToString();
        DataTable dtGetMailTo = Common.Execute_Procedures_Select_ByQueryCMS(sqlGetMailTo);
        if (dtGetMailTo != null)
            if (dtGetMailTo.Rows.Count > 0)
            {
                DataRow drGetMailTo = dtGetMailTo.Rows[0];
                MailTo = drGetMailTo["Email"].ToString();
            }

        string sqlGetEmployeeInfo = "select a.LeaveRequestId,pd.EmpCode,pd.FirstName +' ' + pd.MiddleName +' ' + pd.FamilyName as [Name], " +
             "pd.Office,pd.Department,c.OfficeName,pd.Position,dept.DeptName,p.PositionName, " +
             "a.LeaveTypeId,b.LeaveTypeName, " +
             "replace(convert(varchar,a.LeaveFrom,106),' ','-') as LeaveFrom,replace(convert(varchar,a.LeaveTo,106),' ','-') as LeaveTo," +
             "case a.Status when 'A'  then case when convert(datetime,a.LeaveTo) < convert(datetime,convert(varchar,getdate(),106))  then 'Taken' else 'Approved' end " +
             "when 'P' then 'Plan' when 'W' then 'Awaiting Verification'  when 'v' then 'Awaiting Approval' when 'R' then 'Rejected' when 'C' then 'Cancelled' else a.Status end as Status, " +
             "(case when a.HalfDay>0 then 0.5 else dbo.HR_Get_LeaveCount(pd.Office,leavefrom,leaveto) end) as Duration," +
             "a.Reason,a.AppRejRemark, dbo.getAbsentDays(a.EmpId,a.LeaveFrom,a.LeaveTo) as AbsentDays from HR_LeaveRequest a left outer join HR_LeaveTypeMaster b on a.LeaveTypeId=b.LeaveTypeId " +
             "left outer join Hr_PersonalDetails pd on a.empid=pd.empid " +
             "left outer join Position p on pd.Position=p.PositionId " +
             "Left Outer Join Office c on pd.Office= c.OfficeId " +
             "Left Outer Join HR_Department dept on pd.Department=dept.DeptId " +
             "where a.LeaveRequestId =" + ViewState["LeaveRequestId"].ToString();
        DataTable dtGetEmployeeInfo = Common.Execute_Procedures_Select_ByQueryCMS(sqlGetEmployeeInfo);
        if (dtGetEmployeeInfo != null)
            if (dtGetEmployeeInfo.Rows.Count > 0)
            {
                DataRow drGetEmployeeInfo = dtGetEmployeeInfo.Rows[0];
                //Sending mails
                char[] Sep = { ';' };
                string[] ToAdds = MailTo.ToString().Split(Sep);
                string[] CCAdds = MailTo.ToString().Split(Sep);
                string[] BCCAdds = "".Split(Sep);
                //----------------------
                String StatusText = (rdoLeaveVerify.Checked) ? "Approved." : "Rejected.";
                String Subject = "Leave request " + StatusText;
                String MailBody;
                MailBody = "EmployeeName: " + drGetEmployeeInfo["Name"].ToString() + " || Position:" + drGetEmployeeInfo["PositionName"].ToString() + " || Department: " + drGetEmployeeInfo["DeptName"].ToString() + "";
                MailBody = MailBody + "<br><br>Leave Type: " + drGetEmployeeInfo["LeaveTypeName"].ToString() + " || Leave Period: ( " + drGetEmployeeInfo["LeaveFrom"].ToString() + " : " + drGetEmployeeInfo["LeaveTo"].ToString() + ") || Duration: " + drGetEmployeeInfo["Duration"].ToString() + " (days) || Total Office Absence: " + drGetEmployeeInfo["AbsentDays"].ToString();
                MailBody = MailBody + "<br><br>Remark: " + txtVerifyRemarks.Text + "";
                MailBody = MailBody + "<br><br>Thanks & Best Regards";
                //MailBody = MailBody + "<br>" + drGetEmployeeInfo["Name"].ToString() + "<br><font color=000080 size=2 face=Century Gothic><strong>" + MailFrom.ToString() + "</strong></font>";
                MailBody = MailBody + "<br>" + UppercaseWords(EmpFullName);
                MailBody = MailBody + "<br>" + EmpPosition + "<br><font color=000080 size=2 face=Century Gothic><strong>" + MailFrom.ToString() + "</strong></font>";
                //------------------
                string ErrMsg = "";
                string AttachmentFilePath = "";
                SendEmail.SendeMail(Common.CastAsInt32(Session["LoginId"].ToString()), MailFrom.ToString(), MailFrom.ToString(), ToAdds, CCAdds, BCCAdds, Subject, MailBody, out ErrMsg, AttachmentFilePath);
            }
    }
    public void SendMailCancel()
    {
        string MailFrom = "", MailTo = "";

        //Mail From
        string sqlGetMailFrom = "SELECT C.Email FROM USERLOGIN C where C.LoginId=" + Session["LoginId"].ToString();

        DataTable dtGetMailFrom = Common.Execute_Procedures_Select_ByQueryCMS(sqlGetMailFrom);
        if (dtGetMailFrom != null)
            if (dtGetMailFrom.Rows.Count > 0)
            {
                DataRow drGetMailFrom = dtGetMailFrom.Rows[0];
                MailFrom = drGetMailFrom["Email"].ToString();
            }

        String EmpFullName = "", EmpPosition = "";
        string sqlFullNameNPosition = "SELECT (select PositionName from Position P where P.PositionID=pd.Position)Position,(pd.FirstName+' '+pd.FamilyName )as UserName FROM Hr_PersonalDetails pd LEFT OUTER JOIN USERLOGIN C ON pd.userid=C.LoginId where C.LoginId=" + Session["LoginId"].ToString() + "";
        DataTable dtNamePos = Common.Execute_Procedures_Select_ByQueryCMS(sqlFullNameNPosition);
        if (dtNamePos != null)
            if (dtNamePos.Rows.Count > 0)
            {
                EmpPosition = dtNamePos.Rows[0][0].ToString();
                EmpFullName = dtNamePos.Rows[0][1].ToString();
            }



        //Mail To
        string sqlGetMailTo = "SELECT A.EMPID,C.Email FROM HR_LeaveRequest A " +
                          "LEFT OUTER JOIN Hr_PersonalDetails B ON A.EMPID=B.EMPID " +
                          "LEFT OUTER JOIN USERLOGIN C ON B.USERID=C.LoginId " +
                          "WHERE A.LeaveRequestId=" + ViewState["LeaveRequestId"].ToString();
        DataTable dtGetMailTo = Common.Execute_Procedures_Select_ByQueryCMS(sqlGetMailTo);
        if (dtGetMailTo != null)
            if (dtGetMailTo.Rows.Count > 0)
            {
                DataRow drGetMailTo = dtGetMailTo.Rows[0];
                MailTo = drGetMailTo["Email"].ToString();
            }

        string sqlGetEmployeeInfo = "select a.LeaveRequestId,pd.EmpCode,pd.FirstName +' ' + pd.MiddleName +' ' + pd.FamilyName as [Name], " +
             "pd.Office,pd.Department,c.OfficeName,pd.Position,dept.DeptName,p.PositionName, " +
             "a.LeaveTypeId,b.LeaveTypeName, " +
             "replace(convert(varchar,a.LeaveFrom,106),' ','-') as LeaveFrom,replace(convert(varchar,a.LeaveTo,106),' ','-') as LeaveTo," +
             "case a.Status when 'A'  then case when convert(datetime,a.LeaveTo) < convert(datetime,convert(varchar,getdate(),106))  then 'Taken' else 'Approved' end " +
             "when 'P' then 'Plan' when 'W' then 'Awaiting Verification'  when 'v' then 'Awaiting Approval' when 'R' then 'Rejected' when 'C' then 'Cancelled' else a.Status end as Status, " +
             "(case when a.HalfDay>0 then 0.5 else dbo.HR_Get_LeaveCount(pd.Office,leavefrom,leaveto) end) as Duration," +
             "a.Reason,a.AppRejRemark, dbo.getAbsentDays(a.EmpId,a.LeaveFrom,a.LeaveTo) as AbsentDays from HR_LeaveRequest a left outer join HR_LeaveTypeMaster b on a.LeaveTypeId=b.LeaveTypeId " +
             "left outer join Hr_PersonalDetails pd on a.empid=pd.empid " +
             "left outer join Position p on pd.Position=p.PositionId " +
             "Left Outer Join Office c on pd.Office= c.OfficeId " +
             "Left Outer Join HR_Department dept on pd.Department=dept.DeptId " +
             "where a.LeaveRequestId =" + ViewState["LeaveRequestId"].ToString();
        DataTable dtGetEmployeeInfo = Common.Execute_Procedures_Select_ByQueryCMS(sqlGetEmployeeInfo);
        if (dtGetEmployeeInfo != null)
            if (dtGetEmployeeInfo.Rows.Count > 0)
            {
                DataRow drGetEmployeeInfo = dtGetEmployeeInfo.Rows[0];
                //Sending mails
                char[] Sep = { ';' };
                string[] ToAdds = MailTo.ToString().Split(Sep);
                string[] CCAdds = MailTo.ToString().Split(Sep);
                string[] BCCAdds = "".Split(Sep);
                //----------------------
                String Subject = "Approved request cancelled.";
                String MailBody;
                MailBody = "EmployeeName: " + drGetEmployeeInfo["Name"].ToString() + " || Position:" + drGetEmployeeInfo["PositionName"].ToString() + " || Department: " + drGetEmployeeInfo["DeptName"].ToString() + "";
                MailBody = MailBody + "<br><br>Leave Type: " + drGetEmployeeInfo["LeaveTypeName"].ToString() + " || Leave Period: ( " + drGetEmployeeInfo["LeaveFrom"].ToString() + " : " + drGetEmployeeInfo["LeaveTo"].ToString() + ") || Duration: " + drGetEmployeeInfo["Duration"].ToString() + " (days) || Total Office Absence: " + drGetEmployeeInfo["AbsentDays"].ToString();
                MailBody = MailBody + "<br><br>Remark: " + txtCancelationRemark.Text + "";
                MailBody = MailBody + "<br><br>Thanks & Best Regards";
                //MailBody = MailBody + "<br>" + drGetEmployeeInfo["Name"].ToString() + "<br><font color=000080 size=2 face=Century Gothic><strong>" + MailFrom.ToString() + "</strong></font>";
                MailBody = MailBody + "<br>" + UppercaseWords(EmpFullName);
                MailBody = MailBody + "<br>" + EmpPosition + "<br><font color=000080 size=2 face=Century Gothic><strong>" + MailFrom.ToString() + "</strong></font>";

                //------------------
                string ErrMsg = "";
                string AttachmentFilePath = "";
                SendEmail.SendeMail(Common.CastAsInt32(Session["LoginId"].ToString()), MailFrom.ToString(), MailFrom.ToString(), ToAdds, CCAdds, BCCAdds, Subject, MailBody, out ErrMsg, AttachmentFilePath);
            }
    }
    static string UppercaseWords(string value)
    {
        char[] array = value.ToCharArray();
        // Handle the first letter in the string.
        if (array.Length >= 1)
        {
            if (char.IsLower(array[0]))
            {
                array[0] = char.ToUpper(array[0]);
            }
        }
        // Scan through the letters, checking for spaces.
        // ... Uppercase the lowercase letters following spaces.
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i - 1] == ' ')
            {
                if (char.IsLower(array[i]))
                {
                    array[i] = char.ToUpper(array[i]);
                }
            }
        }
        return new string(array);
    }
}
