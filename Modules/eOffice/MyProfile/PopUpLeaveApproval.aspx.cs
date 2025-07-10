using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class emtm_Emtm_PopUpLeaveApproval : System.Web.UI.Page
{
    DateTime ToDay;
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
    public int LeaveEmpId
    {
        get
        {
            return Common.CastAsInt32(ViewState["LeaveEmpId"]);
        }
        set
        {
            ViewState["LeaveEmpId"] = value;
        }
    }
    public void ShowRecord(int Id)
    {
        string sql = "select a.Empid,a.LeaveRequestId,pd.EmpCode,pd.FirstName +' ' + pd.MiddleName +' ' + pd.FamilyName as [Name], " +
                     "pd.Office,pd.Department,c.OfficeName,pd.Position,dept.DeptName,p.PositionName, " +
                     "a.LeaveTypeId,b.LeaveTypeName, " +
                     "replace(convert(varchar,a.LeaveFrom,106),' ','-') as LeaveFrom,replace(convert(varchar,a.LeaveTo,106),' ','-') as LeaveTo," +
                     "year(a.LeaveFrom) as [Year],Month(a.LeaveFrom)as [Month]," +
                     "case a.Status when 'A'  then case when convert(datetime,a.LeaveTo) < convert(datetime,convert(varchar,getdate(),106))  then 'Taken' else 'Approved' end " +
                     "when 'P' then 'Plan' when 'V' then 'Awaiting Approval' when 'R' then 'Rejected' when 'C' then 'Cancelled' else a.Status end as Status, " +
                     "(case when a.HalfDay>0 then 0.5 else dbo.HR_Get_LeaveCount(pd.office,leavefrom,leaveto) end) as Duration, " +
                     "a.Reason,a.AppRejRemark,a.HalfDay, dbo.getAbsentDays(a.empid,a.LeaveFrom,a.LeaveTo) as AbsentDays, dbo.getAbsentDates(a.empid,a.LeaveFrom,a.LeaveTo) as AbsentDates,a.RequestDate, " +
                     "(SELECT FIRSTNAME + ' ' + FAMILYNAME FROM Hr_PersonalDetails p WHERE p.EMPID=a.ForwardedTo) as ApproverName " +
                     "from HR_LeaveRequest a left outer join HR_LeaveTypeMaster b on a.LeaveTypeId=b.LeaveTypeId " +
                     "left outer join Hr_PersonalDetails pd on a.empid=pd.empid " +
                     "left outer join Position p on pd.Position=p.PositionId " +
                     "Left Outer Join Office c on pd.Office= c.OfficeId " +
                     "Left Outer Join HR_Department dept on pd.Department=dept.DeptId " +
                     "where a.LeaveRequestId =" + Id;

        DataTable dtdata = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dtdata != null)
            if (dtdata.Rows.Count > 0)
            {
                DataRow dr = dtdata.Rows[0];
                LeaveEmpId = Common.CastAsInt32(dr["Empid"].ToString());
                LeavePlanner1.EmpId = Common.CastAsInt32(dr["Empid"].ToString());
                //lblEmpCode.Text=dr["EmpCode"].ToString();
                lblEmpName.Text = dr["Name"].ToString();
                lblOffice.Text = dr["OfficeName"].ToString();
                lblDepartment.Text = dr["DeptName"].ToString();
                lblDesignation.Text = dr["PositionName"].ToString();
                ViewState["LeaveTypeId"] = dr["LeaveTypeId"].ToString();
                ViewState["RequestDate"] = dr["RequestDate"].ToString();
                lblAppDate.Text = Common.ToDateString(dr["RequestDate"]);

                lblLeaveType.Text = dr["LeaveTypeName"].ToString();
                lblLeaveFrom.Text = Convert.ToDateTime(dr["LeaveFrom"]).ToString("dd-MMM-yyyy").Trim();
                lblLeaveTo.Text = Convert.ToDateTime(dr["LeaveTo"]).ToString("dd-MMM-yyyy").Trim();
                lblLeaveStatus.Text = dr["Status"].ToString();
                lblLeaveDays.Text = "( " + dr["Duration"].ToString() + " Days)";

                lblReason.Text = dr["Reason"].ToString();
                txtAppRejRemarks.Text = dr["AppRejRemark"].ToString();

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

                ShowLeaveBalance();
            }

        ShowComments();
    }
    public void ShowComments()
    {
        DataTable dtComm = Common.Execute_Procedures_Select_ByQueryCMS("select P.FIRSTNAME + ' ' + P.familyNAME AS 'Emp',C.COMMENTS from HR_Leave_Request_Comments C inner join dbo.Hr_PersonalDetails P ON C.EMPID=P.EMPID where leaverequestid=" + LeaveRequestId.ToString() + " order by TableId ");
        rptComments.DataSource = dtComm;
        rptComments.DataBind();

        trAddCommheader.Visible = (dtComm.Rows.Count <= 0);
        trothercomm.Visible = !trAddCommheader.Visible;

        litNotes.Text = "";

        ShowHalfDayNote();
        ShowNoAllotmentNote();
        ShowintimationPeriodNote();
        ShowDateConflictMessage();
    }
    public void SendMail()
    {
        string MailFrom = "", MailTo = "";
        string sqlGetMailFrom = "SELECT C.Email FROM USERLOGIN C where C.LoginId=" + Session["LoginId"].ToString();
        DataTable dtGetMailFrom = Common.Execute_Procedures_Select_ByQueryCMS(sqlGetMailFrom);
        if (dtGetMailFrom != null)
            if (dtGetMailFrom.Rows.Count > 0)
            {
                DataRow drGetMailFrom = dtGetMailFrom.Rows[0];
                MailFrom = drGetMailFrom["Email"].ToString();
            }

        String EmpFullName = "", EmpPosition = "";
        string sqlFullNameNPosition = "SELECT (select PositionName from Position P where P.PositionID=pd.Position)Position,(pd.FirstName+' '+pd.FamilyName )as UserName FROM Hr_PersonalDetails pd LEFT OUTER JOIN USERLOGIN C ON pd.userid=C.LoginId where pd.userid=" + Session["LoginId"].ToString() + "";
        DataTable dtNamePos = Common.Execute_Procedures_Select_ByQueryCMS(sqlFullNameNPosition);
        if (dtNamePos != null)
            if (dtNamePos.Rows.Count > 0)
            {
                EmpPosition = dtNamePos.Rows[0][0].ToString();
                EmpFullName = dtNamePos.Rows[0][1].ToString();
            }



        // Mail To Leave Requestee Person

        string sqlGetMailTo = "SELECT A.EMPID,C.Email FROM HR_LeaveRequest A " +
                          "LEFT OUTER JOIN Hr_PersonalDetails B ON A.EMPID=B.EMPID " +
                          "LEFT OUTER JOIN USERLOGIN C ON B.USERID=C.LoginId " +
                          "WHERE A.LeaveRequestId=" + LeaveRequestId;
        DataTable dtGetMailTo = Common.Execute_Procedures_Select_ByQueryCMS(sqlGetMailTo);
        if (dtGetMailTo != null)
            if (dtGetMailTo.Rows.Count > 0)
            {
                DataRow drGetMailTo = dtGetMailTo.Rows[0];
                MailTo = drGetMailTo["Email"].ToString();
            }

        // Mail To Approval Chain

        DataTable dtApproverChain = Common.Execute_Procedures_Select_ByQueryCMS("EXEC dbo.HR_LeaveDiscussion_getApproverChain " + LeaveRequestId);
        List<string> MailTos = new List<string>();
        for (int i = 0; i <= dtApproverChain.Rows.Count - 1; i++)
        {
            string sqlAppChain = "SELECT A.EMPID,C.Email FROM Hr_PersonalDetails A LEFT OUTER JOIN USERLOGIN C ON A.USERID=C.LoginId WHERE A.Empid=" + dtApproverChain.Rows[i][0].ToString();
            DataTable dtAppChain = Common.Execute_Procedures_Select_ByQueryCMS(sqlAppChain);
            if (dtAppChain != null)
                if (dtAppChain.Rows.Count > 0)
                {
                    DataRow drAppChain = dtAppChain.Rows[0];
                    MailTos.Add(drAppChain["Email"].ToString());
                }

        }

        if (MailTo.Trim() != "")
            MailTos.Add(MailTo);

        string sqlGetEmployeeInfo = "select a.LeaveRequestId,pd.EmpCode,pd.FirstName +' ' + pd.MiddleName +' ' + pd.FamilyName as [Name], " +
             "pd.Office,pd.Department,c.OfficeName,pd.Position,dept.DeptName,p.PositionName, " +
             "a.LeaveTypeId,b.LeaveTypeName, " +
             "replace(convert(varchar,a.LeaveFrom,106),' ','-') as LeaveFrom,replace(convert(varchar,a.LeaveTo,106),' ','-') as LeaveTo," +
             "case a.Status when 'A'  then case when convert(datetime,a.LeaveTo) < convert(datetime,convert(varchar,getdate(),106))  then 'Taken' else 'Approved' end " +
             "when 'P' then 'Plan' when 'W' then 'Awaiting Verification'  when 'v' then 'Awaiting Approval' when 'R' then 'Rejected' when 'C' then 'Cancelled' else a.Status end as Status, " +
             "(case when a.HalfDay>0 then 0.5 else dbo.HR_Get_LeaveCount(pd.Office,leavefrom,leaveto) end) as Duration," +
             "a.Reason,a.AppRejRemark, dbo.getAbsentDays(a.empid,a.LeaveFrom,a.LeaveTo) as AbsentDays,pd.Office as EmpOfficeId from HR_LeaveRequest a left outer join HR_LeaveTypeMaster b on a.LeaveTypeId=b.LeaveTypeId " +
             "left outer join Hr_PersonalDetails pd on a.empid=pd.empid " +
             "left outer join Position p on pd.Position=p.PositionId " +
             "Left Outer Join Office c on pd.Office= c.OfficeId " +
             "Left Outer Join HR_Department dept on pd.Department=dept.DeptId " +
             "where a.LeaveRequestId =" + LeaveRequestId;
        DataTable dtGetEmployeeInfo = Common.Execute_Procedures_Select_ByQueryCMS(sqlGetEmployeeInfo);
        if (dtGetEmployeeInfo != null)
            if (dtGetEmployeeInfo.Rows.Count > 0)
            {
                
                DataRow drGetEmployeeInfo = dtGetEmployeeInfo.Rows[0];
                int OfficeID = Common.CastAsInt32(drGetEmployeeInfo["EmpOfficeId"]);
                //Sending mails
                char[] Sep = { ';' };
                string[] ToAdds = MailTos.ToArray();
                List<String> ccAddsList = new List<String>();
                ccAddsList.Add("emanager@energiossolutions.com");

                //if(OfficeID==3)
                //{
                //    ccAddsList.Add("emanager@energiossolutions.com");
                //}
                //if (OfficeID == 1)
                //{
                //    ccAddsList.Add("asingh@energiossolutions.com");
                //    ccAddsList.Add("asingh@energiossolutions.com");
                //}

                string[] CCAdds = ccAddsList.ToArray();// { "emanager@energiossolutions.com" };
                string[] BCCAdds = "".Split(Sep);
                //----------------------
                String StatusText = (rdoLeaveApprove.Checked) ? "Approved." : "Rejected.";
                String Subject = "Leave request " + StatusText;
                String MailBody;
                MailBody = "EmployeeName: " + drGetEmployeeInfo["Name"].ToString() + " || Position:" + drGetEmployeeInfo["PositionName"].ToString() + " || Department: " + drGetEmployeeInfo["DeptName"].ToString() + "";
                MailBody = MailBody + "<br><br>Leave Type: " + drGetEmployeeInfo["LeaveTypeName"].ToString() + " || Leave Period: ( " + drGetEmployeeInfo["LeaveFrom"].ToString() + " : " + drGetEmployeeInfo["LeaveTo"].ToString() + ") || Duration: " + drGetEmployeeInfo["Duration"].ToString() + " (days) || Total Office Absence: " + drGetEmployeeInfo["AbsentDays"].ToString() + " (days)";
                MailBody = MailBody + "<br><br>Remark: " + txtAppRejRemarks.Text + "";
                MailBody = MailBody + "<br><br>Thanks & Best Regards";
                //MailBody= MailBody + "<br>" + drGetEmployeeInfo["Name"].ToString() + "<br><font color=000080 size=2 face=Century Gothic><strong>" + MailFrom.ToString() + "</strong></font>";
                MailBody = MailBody + "<br>" + UppercaseWords(EmpFullName);
                MailBody = MailBody + "<br>" + EmpPosition + "<br><font color=000080 size=2 face=Century Gothic><strong>" + MailFrom.ToString() + "</strong></font>";
                //------------------
                string ErrMsg = "";
                string AttachmentFilePath = "";
                SendEmail.SendeMailAsync(Common.CastAsInt32(Session["LoginId"].ToString()), MailFrom.ToString(), MailFrom.ToString(), ToAdds, CCAdds, BCCAdds, Subject, MailBody, out ErrMsg, AttachmentFilePath);
            }
    }
    public void SendMailForDiscussion(int _EmpId, String ApproverComments, int Refkey)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select EMAIL from userlogin WHERE LOGINID IN (SELECT USERID FROM Hr_PersonalDetails WHERE EMPID=" + _EmpId.ToString() + ")");
        DataTable dtApprover = Common.Execute_Procedures_Select_ByQueryCMS("SELECT FIRSTNAME + ' ' + FAMILYNAME AS EMPNAME,(SELECT POSITIONNAME FROM POSITION WHERE POSITIONID=PID) AS POSITION,(SELECT EMAIL FROM USERLOGIN WHERE LOGINID=P.USERID) AS EMAIL FROM Hr_PersonalDetails P WHERE EMPID IN (SELECT FORWARDEDTO FROM HR_LeaveRequest WHERE LEAVEREQUESTID=" + LeaveRequestId.ToString() + ")");
        string ToAddress = dt.Rows[0][0].ToString();
        //ToAddress = "pankaj.k@esoftech.com";
        string[] ToAdds = { ToAddress };
        string[] CCAdds = { };
        string[] BCCAdds = { };
        //string Link = "http://localhost:51319/Web/EMTM/ActionResult.aspx?key=1&data=145er_" + Refkey.ToString() + "_009opibv4";
        string Link = "http://emanagershore.enrgiosmaritime.com/cms/EMTM/ActionResult.aspx?key=1&data=145er_" + Refkey.ToString() + "_009opibv4";
        ////----------------------
        String Subject = "Leave approval discussion [ " + lblEmpName.Text + " ] ";
        String MailBody;
        MailBody = "Employee Name : " + lblEmpName.Text + " || Position:" + lblDesignation.Text + " || Department: " + lblDepartment.Text + "";
        MailBody = MailBody + "<br><br>Leave Type: " + lblLeaveType.Text + " || Leave Period: ( " + lblLeaveFrom.Text + " to " + lblLeaveTo.Text + ") || Duration: " + lblLeaveDays.Text + " || Total Office Absence: " + lblAbsentDays.Text;
        MailBody = MailBody + "<br><br>Approver Comments : " + ApproverComments + "";
        MailBody = MailBody + "<br><br>Please enter your comment in order to approve leave on below given link.";
        MailBody = MailBody + "<br><br><a href='" + Link + "' target='_blank' >" + Link + "</a>";
        MailBody = MailBody + "<br><br>Thanks & Best Regards";
        MailBody = MailBody + "<br>" + UppercaseWords(dtApprover.Rows[0]["Empname"].ToString());
        MailBody = MailBody + "<br>" + dtApprover.Rows[0]["POSITION"].ToString() + "<br><font color=000080 size=2 face=Century Gothic><strong>" + dtApprover.Rows[0]["EMAIL"].ToString() + "</strong></font>";
        ////------------------
        string ErrMsg = "";
        string AttachmentFilePath = "";
        SendEmail.SendeMail(Common.CastAsInt32(Session["LoginId"].ToString()), dtApprover.Rows[0]["EMAIL"].ToString(), dtApprover.Rows[0]["EMAIL"].ToString(), ToAdds, CCAdds, BCCAdds, Subject, MailBody, out ErrMsg, AttachmentFilePath);
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
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        this.LeavePlanner1.Select_Leave += new emtm_MyProfile_LeavePlanner.SelectLeave(Leave_Changed);
        ToDay = DateTime.Today;
        if (!IsPostBack)
        {
            EmpId = Common.CastAsInt32(Session["ProfileId"]);
            if (EmpId > 0)
            {
                if (Request.QueryString.GetKey(0) != null)
                {
                    string LeaveIdName;
                    LeaveIdName = Request.QueryString.GetKey(0);
                    int LeaveReqId = Common.CastAsInt32(Request.QueryString[LeaveIdName].Trim());
                    LeaveRequestId = Common.CastAsInt32(LeaveReqId);
                    ShowRecord(LeaveReqId);
                    checkStatus();
                    btnDone.Enabled = false;
                }
            }
        }
    }
    protected void Action_Change(object sender, EventArgs e)
    {
        if (rdoLeaveApprove.Checked || rdoLeaveReject.Checked)
        {
            btnDone.Enabled = true;
        }
        else
        {
            btnDone.Enabled = false;
        }

    }
    protected void btnDone_Click(object sender, EventArgs e)
    {
        string LeaveStatus;
        if (rdoLeaveApprove.Checked)
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
        // EmpId = Common.CastAsInt32(Session["EmpId"]);
        Common.Set_Procedures("HR_UpdateRequestLeave");
        Common.Set_ParameterLength(4);
        Common.Set_Parameters(new MyParameter("@LeaveRequestId", ViewState["LeaveRequestId"]),
            new MyParameter("@Status", LeaveStatus.Trim()),
            new MyParameter("@ActionById", Common.CastAsInt32(EmpId)),
            new MyParameter("@ActionRemark", txtAppRejRemarks.Text.Trim()));
        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD_CMS(ds))
        {
            SendMail();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');window.close();", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable To Save Record. Error : " + Common.getLastError() + "');", true);
        }
    }
    public void ShowHalfDayNote()
    {
        if (lblHalfDay.Text.Contains("Half"))
        {
            litNotes.Text += "<br/> ■ Half Day leave  might be rejected. Please discuss this with your HOD and approver.";
        }
    }
    public void ShowNoAllotmentNote()
    {
        string year = DateTime.Today.Year.ToString();
        if (lblLeaveFrom.Text.Trim() != "")
        { year = Convert.ToDateTime(lblLeaveFrom.Text).Year.ToString(); }
        string LeaveTypeId = ViewState["LeaveTypeId"].ToString();
        DataTable dtCnt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT LeaveCount FROM HR_LeaveAssignment WHERE empid=" + LeaveEmpId.ToString() + " and year=" + year + " and leavetypeid=" + LeaveTypeId);
        if (dtCnt.Rows.Count <= 0)
        {
            litNotes.Text += "<br/> ■ You have no entitlement for requested leave type in year [ " + year + " ].";
        }
        else if (Common.CastAsInt32(dtCnt.Rows[0][0]) <= 0)
        {
            litNotes.Text += "<br/> ■ You have no entitlement for requested leave type in year [ " + year + " ].";
        }
    }
    public void ShowintimationPeriodNote()
    {
        //string year = DateTime.Today.Year.ToString();
        //if (lblLeaveFrom.Text.Trim() != "")
        //{ year = Convert.ToDateTime(lblLeaveFrom.Text).Year.ToString(); }
        //if (ViewState["LeaveTypeId"].ToString() == "1")
        //{
        //    DataTable dtCnt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT LeaveCount FROM HR_LeaveAssignment WHERE empid=" + LeaveEmpId.ToString() + " and year=" + year + " and leavetypeid=1");
        //    if (dtCnt.Rows.Count > 0)
        //    {
        //        int Allotment = Common.CastAsInt32(dtCnt.Rows[0][0]);
        //        if (Allotment > 0)
        //        {
        //            DateTime StDt = Convert.ToDateTime(lblLeaveFrom.Text);
        //            DateTime ReqDt = Convert.ToDateTime(ViewState["RequestDate"]);
        //            if (StDt.Subtract(ReqDt).Days < Allotment)
        //            {
        //                litNotes.Text += "<br/> ■ You must apply for leave at least (" + Allotment.ToString() + ") days in advance.";
        //            }
        //        }
        //    }
        //}
    }
    public void ShowDateConflictMessage()
    {
        if (lblLeaveFrom.Text.Trim() != "" && lblLeaveTo.Text.Trim() != "")
        {
            DataTable dtCnt = Common.Execute_Procedures_Select_ByQueryCMS("select * from HR_OfficeAbsence where ( '" + lblLeaveFrom.Text.Trim() + "' between leavefrom and leaveto OR '" + lblLeaveTo.Text.Trim() + "' between leavefrom and leaveto OR ( '" + lblLeaveFrom.Text.Trim() + "' < leavefrom AND '" + lblLeaveTo.Text.Trim() + "' > leaveto ) ) and empid=" + LeaveEmpId.ToString());
            if (dtCnt.Rows.Count > 0)
            {
                litNotes.Text += "<br/> ■ There is Date Conflict between Leave & Business travel period.";
            }
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        dvDisc.Visible = true;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("EXEC dbo.Emtm_LeaveDiscussion_getApproverChain " + LeaveRequestId.ToString());
        ckhMembers.DataSource = dt;
        ckhMembers.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Common.Execute_Procedures_Select_ByQueryCMS("EXEC dbo.[HR_InitiateLeaveDiscussion] " + LeaveRequestId.ToString() + "," + EmpId + ",'" + TextBox1.Text.Trim().Replace("'", "''") + "'");

        foreach (ListItem li in ckhMembers.Items)
        {
            if (li.Selected)
            {
                DataTable DT = Common.Execute_Procedures_Select_ByQueryCMS("EXEC dbo.[HR_InitiateLeaveDiscussion] " + LeaveRequestId.ToString() + "," + li.Value + ",''");
                int TableId = Common.CastAsInt32(DT.Rows[0][0].ToString());
                SendMailForDiscussion(Common.CastAsInt32(li.Value), TextBox1.Text, TableId);
            }
        }
        ShowComments();
        dvDisc.Visible = false;
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        dvDisc.Visible = false;
    }
    public void checkStatus()
    {
        string SQL = "SELECT [Status] FROM HR_LeaveRequest WHERE LeaveRequestId = " + LeaveRequestId.ToString() + " ";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);

        if (dt != null)
        {
            if (!(dt.Rows[0]["Status"].ToString() == "P" || dt.Rows[0]["Status"].ToString() == "V"))
            {
                btnAdd.Enabled = false;
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Invalid status.');window.close();", true);
            }
        }

    }
    public void ShowLeaveBalance()
    {
        int LeaveTypeId = Common.CastAsInt32(ViewState["LeaveTypeId"]);
        if (LeaveTypeId == 1)
        {
            string sql = "SELECT a.EmpCode,a.firstname,a.middlename,a.familyname,a.Office,a.Department,c.OfficeName,a.Position,d.DeptName,b.PositionName,ss.* " +
                                 "FROM Hr_PersonalDetails a  " +
                                 "left join (select * from dbo.getLeaveStatus_OnDate(" + LeaveEmpId + ",'" + ToDay.Date.ToString("dd-MMM-yyyy") + "')) ss on a.empid=ss.empid " +
                                 "left outer join Position b on a.Position=b.PositionId  " +
                                 "Left Outer Join Office c on a.Office= c.OfficeId  " +
                                 "Left Outer Join HR_Department d on a.Department=d.DeptId WHERE a.EMPID=" + LeaveEmpId + " ";

            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            if (dt != null)
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    lblCurrentMonth1.Text = "Balance leave upto " + ToDay.ToString("MMM") + "-" + ToDay.Year.ToString() + " is ";
                    lblLeaveBalance.Text = string.Format("{0:0.0}", (Convert.ToDouble(dr["PayableLeave"])));

                }
        }
        else
        {

            string year = ToDay.Date.ToString("yyyy");
            string sqlConsumed = "select [dbo].[HR_Get_TotalConsumedLeaveForPeriod_LeaveType]('01-jan-" + year + "','31-dec-" + year + "'," + LeaveEmpId + "," + LeaveTypeId.ToString() + ")";
            string sqlAssigned = "SELECT LeaveCount FROM dbo.HR_LeaveAssignment WHERE YEAR=" + year + " AND LEAVETYPEID=" + LeaveTypeId.ToString() + " AND EMPID=" + LeaveEmpId;
            DataTable dtCons = Common.Execute_Procedures_Select_ByQueryCMS(sqlConsumed);
            DataTable dtAss = Common.Execute_Procedures_Select_ByQueryCMS(sqlAssigned);

            int Consumed = 0;
            int Assigned = 0;
            if (dtCons.Rows.Count > 0)
            {
                Consumed = Common.CastAsInt32(dtCons.Rows[0][0]);
            }

            if (dtAss.Rows.Count > 0)
            {
                Assigned = Common.CastAsInt32(dtAss.Rows[0][0]);
            }
            lblCurrentMonth1.Text = "Balance leave is ";
            lblLeaveBalance.Text = string.Format("{0:0.0}", (Assigned - Consumed));
        }
    }
    public void Leave_Changed(int LeaveRequestId)
    {
        Response.Redirect("Emtm_PopUpLeaveApproval.aspx?LeaveTypeId=" + LeaveRequestId);
    }
}
