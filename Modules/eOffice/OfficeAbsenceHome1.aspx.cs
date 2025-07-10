using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.IO;

public partial class emtm_MyProfile_Emtm_PopupLeaveRequest1 : System.Web.UI.Page
{
    public AuthenticationManager auth;
    protected DataTable dsHolidays;
    protected DataTable DtLeave;
    
    //User Defined Properties
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
    public int OfficeId
    {
        get
        {
            return Common.CastAsInt32(ViewState["OfficeId"]);
        }
        set
        {
            ViewState["OfficeId"] = value;
        }
    }
    public int DepartmentId
    {
        get
        {
            return Common.CastAsInt32(ViewState["DepartmentId"]);
        }
        set
        {
            ViewState["DepartmentId"] = value;
        }
    }
    public int SelectedYear
    {
        get
        {
            return Common.CastAsInt32(ViewState["SelectedYear"]);
        }
        set
        {
            ViewState["SelectedYear"] = value;
        }
    }
    public int SelectedMonth
    {
        get
        {
            return Common.CastAsInt32(ViewState["SelectedMonth"]);
        }
        set
        {
            ViewState["SelectedMonth"] = value;

        }
    }

    protected string LeaveColor
    {
        get
        {
            return ViewState["LeaveColor"].ToString();
        }
        set
        {
            ViewState["LeaveColor"] = value;
        }
    }
    //-----------------------
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!Page.IsPostBack)
        {
            EmpId = Common.CastAsInt32(Session["ProfileId"]);
            if (EmpId > 0)
            {
                //---------------------------
                ddlYear.Items.Add(new ListItem(DateTime.Today.Year.ToString()));
                ddlYear.Items.Add(new ListItem((DateTime.Today.Year+1).ToString()));

                SelectedYear = Common.CastAsInt32(ddlYear.SelectedValue);
                SelectedMonth = DateTime.Today.Month;
                txtMonthId.Text = SelectedMonth.ToString();  
                //---------------------------
                ControlLoader.LoadControl(ddlLocation, DataName.Office, "NONE", "");
                DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT OFFICE,DEPARTMENT FROM Hr_PersonalDetails WHERE EMPID=" + EmpId.ToString());
                if (dt.Rows.Count > 0)
                {
                    OfficeId = Common.CastAsInt32(dt.Rows[0]["OFFICE"].ToString());
                    ddlLocation.SelectedValue = dt.Rows[0]["OFFICE"].ToString();
                    ddlLocation_SelectedIndexChanged(new object(), new EventArgs());

                    DepartmentId= Common.CastAsInt32(dt.Rows[0]["DEPARTMENT"].ToString());
                    ddlDepartment.SelectedValue = dt.Rows[0]["DEPARTMENT"].ToString();
                }

                hdnShowMonth_Click(sender, e);
                if (Common.CastAsInt32(Request.QueryString["LeaveRequestId"]) > 0)
                {
                    LeaveRequestId = Common.CastAsInt32(Request.QueryString["LeaveRequestId"]);
                    ShowRecord(LeaveRequestId);
                }
            }
            
        }
    }
    # region --- User Defined Functions ---
    
    public void ShowRecord(int Id)
    {
         //---------------------------
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("SELECT OFFICE,DEPARTMENT FROM Hr_PersonalDetails WHERE EMPID=" + EmpId.ToString());
        if(dt.Rows.Count>0)
        {
            ddlLocation.SelectedValue = dt.Rows[0]["OFFICE"].ToString();
            ddlLocation_SelectedIndexChanged(new object(), new EventArgs());  
            ddlDepartment.SelectedValue = dt.Rows[0]["DEPARTMENT"].ToString();  
        }
       
       
        
        ShowHalfDayNote();
        ShowNoAllotmentNote();
    }    
    public void ShowHalfDayNote()
    {
        
    }
    public void ShowNoAllotmentNote()
    {
        //string year = DateTime.Today.Year.ToString();
        //if (txtLeaveFrom.Text.Trim() != "")
        //{ year = Convert.ToDateTime(txtLeaveFrom.Text).Year.ToString(); }
        //string LeaveTypeId = ddlLeaveType.SelectedValue;
        //DataTable dtCnt=Common.Execute_Procedures_Select_ByQueryCMS("SELECT LeaveCount FROM HR_LeaveAssignment WHERE empid=" + EmpId.ToString() + " and year=" + year + " and leavetypeid=" + LeaveTypeId);
        //if (dtCnt.Rows.Count <= 0)
        //{
        //    litNotes.Text += "<br/> ■ You have no entitlement for requested leave type in year [ " + year + " ].";
        //}
        //else if(Common.CastAsInt32(dtCnt.Rows[0][0])<=0)
        //{
        //    litNotes.Text += "<br/> ■ You have no entitlement for requested leave type in year [ " + year + " ].";
        //}
    }
    public string ShowLeaveDays()
    {
        //string sql = "select dbo.HR_Get_LeaveCount(" + ViewState["OfficeId"].ToString() + ",'" + d1 + "','" + d2 + "' )as Duration";
        //DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        //return dt.Rows[0][0].ToString(); 
        return "";
        
    }
    public string ShowAbsentDays()
    {   
            return "0";
        
    }   
    #endregion
    
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        ControlLoader.LoadControl(ddlDepartment, DataName.HR_Department, "All", "", "officeid=" + Common.CastAsInt32(ddlLocation.SelectedValue));
        RenderMonthView();  
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectedYear = Common.CastAsInt32(ddlYear.SelectedValue);  
        RenderMonthView();  
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        RenderMonthView();  
    }
    protected void hdnShowMonth_Click(object sender, EventArgs e)
    {
        SelectedMonth = Common.CastAsInt32(txtMonthId.Text);
        int Month = SelectedMonth;
        tdJan.Attributes.Add("Class", "monthtd");
        tdFeb.Attributes.Add("Class", "monthtd");
        tdMar.Attributes.Add("Class", "monthtd");
        tdApr.Attributes.Add("Class", "monthtd");
        tdMay.Attributes.Add("Class", "monthtd");
        tdJun.Attributes.Add("Class", "monthtd");
        tdJul.Attributes.Add("Class", "monthtd");
        tdAug.Attributes.Add("Class", "monthtd");
        tdSep.Attributes.Add("Class", "monthtd");
        tdOct.Attributes.Add("Class", "monthtd");
        tdNov.Attributes.Add("Class", "monthtd");
        tdDec.Attributes.Add("Class", "monthtd");

        switch (Month)
        {
            case 1:
                tdJan.Attributes.Add("Class", "monthtdselected");
                break;
            case 2:
                tdFeb.Attributes.Add("Class", "monthtdselected");
                break;
            case 3:
                tdMar.Attributes.Add("Class", "monthtdselected");
                break;
            case 4:
                tdApr.Attributes.Add("Class", "monthtdselected");
                break;
            case 5:
                tdMay.Attributes.Add("Class", "monthtdselected");
                break;
            case 6:
                tdJun.Attributes.Add("Class", "monthtdselected");
                break;
            case 7:
                tdJul.Attributes.Add("Class", "monthtdselected");
                break;
            case 8:
                tdAug.Attributes.Add("Class", "monthtdselected");
                break;
            case 9:
                tdSep.Attributes.Add("Class", "monthtdselected");
                break;
            case 10:
                tdOct.Attributes.Add("Class", "monthtdselected");
                break;
            case 11:
                tdNov.Attributes.Add("Class", "monthtdselected");
                break;
            case 12:
                tdDec.Attributes.Add("Class", "monthtdselected");
                break;
            default:
                tdJan.Attributes.Add("Class", "monthtdselected");
                break;
        }
        RenderMonthView();
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
    public string SendMailtoApproverChain()
    {
        string ReplyMess = "";
        string MailFrom = "";
        //Mail From
        string sqlGetMailFrom = "SELECT pd.EmpID,C.Email FROM Hr_PersonalDetails pd LEFT OUTER JOIN USERLOGIN C ON pd.userid=C.LoginId " +
                                "WHERE pd.EmpID=" + EmpId;
        DataTable dtGetMailFrom = Common.Execute_Procedures_Select_ByQueryCMS(sqlGetMailFrom);
        if (dtGetMailFrom != null)
            if (dtGetMailFrom.Rows.Count > 0)
            {
                DataRow drGetMailFrom = dtGetMailFrom.Rows[0];
                MailFrom = drGetMailFrom["Email"].ToString();
            }

        String EmpFullName = "", EmpPosition = "";
        string sqlFullNameNPosition = "SELECT (select PositionName from Position P where P.PositionID=pd.Position)Position,(pd.FirstName+' '+pd.FamilyName )as UserName FROM Hr_PersonalDetails pd LEFT OUTER JOIN USERLOGIN C ON pd.userid=C.LoginId where pd.EmpID=" + EmpId + "";
        DataTable dtNamePos = Common.Execute_Procedures_Select_ByQueryCMS(sqlFullNameNPosition);
        if (dtNamePos != null)
            if (dtNamePos.Rows.Count > 0)
            {
                EmpPosition = dtNamePos.Rows[0][0].ToString();
                EmpFullName = dtNamePos.Rows[0][1].ToString();
            }

        //Mail To Appover Chain
        DataTable dtApproverChain = Common.Execute_Procedures_Select_ByQueryCMS("EXEC dbo.HR_LeaveDiscussion_getApproverChain " + LeaveRequestId);
        if (dtApproverChain.Rows.Count > 0)
        {
            List<string> MailTos = new List<string>();
            for (int i = 0; i <= dtApproverChain.Rows.Count - 1; i++)
            {
                string sqlGetMailTo = "SELECT A.EMPID,C.Email FROM Hr_PersonalDetails A LEFT OUTER JOIN USERLOGIN C ON A.USERID=C.LoginId WHERE A.Empid=" + dtApproverChain.Rows[i][0].ToString();
                DataTable dtGetMailTo = Common.Execute_Procedures_Select_ByQueryCMS(sqlGetMailTo);
                if (dtGetMailTo != null)
                    if (dtGetMailTo.Rows.Count > 0)
                    {
                        DataRow drGetMailTo = dtGetMailTo.Rows[0];
                        MailTos.Add(drGetMailTo["Email"].ToString());
                    }

            }

            string sqlGetEmployeeInfo = "select a.LeaveRequestId,pd.EmpCode,pd.FirstName +' ' + pd.MiddleName +' ' + pd.FamilyName as [Name], " +
                 "pd.Office,pd.Department,c.OfficeName,pd.Position,dept.DeptName,p.PositionName, " +
                 "a.LeaveTypeId,b.LeaveTypeName, " +
                 "replace(convert(varchar,a.LeaveFrom,106),' ','-') as LeaveFrom,replace(convert(varchar,a.LeaveTo,106),' ','-') as LeaveTo," +
                 "case a.Status when 'A'  then case when convert(datetime,a.LeaveTo) < convert(datetime,convert(varchar,getdate(),106))  then 'Taken' else 'Approved' end " +
                 "when 'P' then 'Plan' when 'V' then 'Awaiting Approval' when 'R' then 'Rejected' when 'C' then 'Cancelled' else a.Status end as Status, " +
                 "(case when a.HalfDay>0 then 0.5 else dbo.HR_Get_LeaveCount(pd.Office,leavefrom,leaveto) end) as Duration," +
                 "a.Reason,a.AppRejRemark, dbo.getAbsentDays(a.empid,a.LeaveFrom,a.LeaveTo) as AbsentDays from HR_LeaveRequest a left outer join HR_LeaveTypeMaster b on a.LeaveTypeId=b.LeaveTypeId " +
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
                    //Sending mails
                    char[] Sep = { ';' };

                    string[] ToAdds = MailTos.ToArray();
                    string[] CCAdds = { "emanager@energiossolutions.com" };// MailTo.ToString().Split(Sep);
                    string[] BCCAdds = "".Split(Sep);
                    String Subject = "Request for Leave";


                    //MailFrom = "pankaj.k@esoftech.com";
                    //string[] ToAdds = "asingh@energiossolutions.com".ToString().Split(Sep);
                    //string[] CCAdds = { "asingh@energiossolutions.com" };// MailTo.ToString().Split(Sep);
                    //string[] BCCAdds = "".Split(Sep);
                    //String Subject = "Request for Leave : " + string.Join(";",MailTos.ToArray());

                    String MailBody;
                    MailBody = "EmployeeName: " + drGetEmployeeInfo["Name"].ToString() + " || Position:" + drGetEmployeeInfo["PositionName"].ToString() + " || Department: " + drGetEmployeeInfo["DeptName"].ToString() + "";
                    MailBody = MailBody + "<br><br>Leave Type: " + drGetEmployeeInfo["LeaveTypeName"].ToString() + " || Leave Period: ( " + drGetEmployeeInfo["LeaveFrom"].ToString() + " : " + drGetEmployeeInfo["LeaveTo"].ToString() + ") || Duration: " + drGetEmployeeInfo["Duration"].ToString() + " (days) || Total Office Absence: " + drGetEmployeeInfo["AbsentDays"].ToString() + " (days)";
                    //MailBody = MailBody + "<br><br>Absent Days: " + drGetEmployeeInfo["AbsentDays"].ToString() + "";
                    MailBody = MailBody + "<br><br>Remark: " + drGetEmployeeInfo["Reason"].ToString() + "";

                    //string param1 = "sh5785" + LeaveRequestId.ToString().PadLeft(6, '_') + "42535jkgbkju";
                    //string param2 = "5hjhkl25557" + ddlUsers.SelectedValue.Trim().PadLeft(6, '_') + "vhjk2525jll";
                    //MailBody = MailBody + "<br><br>" + "<a  href='" + System.Configuration.ConfigurationManager.AppSettings["PublicAction"].ToString() + "?_ltpinfo=" + param1 + "&_aprifon=" + param2 + "' target='blank' >Click here to Approve/Reject leave.</a>";


                    MailBody = MailBody + "<br><br>Thanks & Best Regards";
                    //MailBody = MailBody + "<br>" + drGetEmployeeInfo["Name"].ToString() + "<br><font color=000080 size=2 face=Century Gothic><strong>" + MailFrom.ToString() + "</strong></font>";
                    MailBody = MailBody + "<br>" + UppercaseWords(EmpFullName);
                    MailBody = MailBody + "<br>" + EmpPosition + "<br><font color=000080 size=2 face=Century Gothic><strong>" + MailFrom.ToString() + "</strong></font>";

                    //------------------
                    string AttachmentFilePath = "";
                    SendEmail.SendeMail(EmpId, MailFrom.ToString(), MailFrom.ToString(), ToAdds, CCAdds, BCCAdds, Subject, MailBody, out ReplyMess, AttachmentFilePath);
                }
        }
        return ReplyMess;
    }
    protected void RenderMonthView()
    {
        int _Year = SelectedYear;
        int _Month = SelectedMonth;
        //----------------
        if (_Month == 0)
        {
            _Month = DateTime.Today.Month;
        }

        int Month = _Month;
        int Year = _Year;

        StringBuilder sb = new StringBuilder();
        sb.Append(@"<table cellpadding='3' cellspacing='0' border='0' style='border-collapse :collapse; text-align :center;' >");
        //--------- FIRST ROW
        sb.Append(@"<tr>");
        sb.Append(@"<td class='monthtd' style='border-right:none;' width='200px'>&nbsp;</td>");
        for (int i = 1; i <= DateTime.DaysInMonth(Year, Month); i++)
        {
            sb.Append("<td style='background-color:#E5A0FC; width:17px;border :solid 1px #E5A0FC;'>" + i.ToString() + "</td>");
        }
        sb.Append(@"</tr>");
        //--------- IIND ROW
        sb.Append(@"<tr>");
        sb.Append(@"<td style='border-right:none; text-align:left' width='200px'><b>Employee Name</b></td>");
        for (int i = 1; i <= DateTime.DaysInMonth(Year, Month); i++)
        {
            DateTime dt = new DateTime(Year, Month, i);
            sb.Append("<td style='background-color:#E5A0FC;border :solid 1px #E5A0FC;'>" + dt.DayOfWeek.ToString().Substring(0, 1) + "</td>");
        }
        sb.Append(@"</table>");
        //---------------------
        sb.Append(@"<div style='width:99%;height:247px;overflow-y:scroll;overflow-x:hidden;border:dotted 1px gray;'>");
        sb.Append(@"<table cellpadding='3' cellspacing='0' border='0' style='border-collapse :collapse; text-align :center;'>");
        //--------- DATA ROWS
        //        string WhereClause = "where a.Status <>'R' and pd.status<>'I' and ofice.officeid=" + Common.CastAsInt32(ddlLocation.SelectedValue.Trim());

        string WhereClause = "where drc is null and ofice.officeid=" + Common.CastAsInt32(ddlLocation.SelectedValue.Trim());

        if (ddlDepartment.SelectedIndex > 0)
            WhereClause += " And dept.deptid=" + Common.CastAsInt32(ddlDepartment.SelectedValue.Trim());

        string SqlEmpName = "select pd.empid,pd.FirstName +' ' + pd.MiddleName +' ' + pd.FamilyName as [Name] " +
                                "from Hr_PersonalDetails pd " +
                                "left outer join office ofice on pd.Office=ofice.OfficeId " +
                                "left outer join HR_Department dept on pd.Department=dept.DeptId " + WhereClause +
                                "group by pd.empid,pd.firstName,pd.MiddleName,pd.FamilyName order by [Name]";


        DataTable dt_Emp = Common.Execute_Procedures_Select_ByQueryCMS(SqlEmpName);
        int EmpId_Row = 0;
        for (int i = 0; i <= dt_Emp.Rows.Count - 1; i++)
        {
            StringBuilder Sb_Temp = new StringBuilder();
            string DaysStatus = "";

            Sb_Temp.Append(@"<tr>");
            EmpId_Row = Common.CastAsInt32(dt_Emp.Rows[i][0]);
            Sb_Temp.Append(@"<td style='border-right:none; text-align:left; border-top:dotted 1px gray;border-bottom:dotted 1px gray;text-transform:capitalize;' width='200px'>" + dt_Emp.Rows[i]["Name"].ToString() + "</td>");
            for (int j = 1; j <= DateTime.DaysInMonth(Year, Month); j++)
            {
                DateTime date = new DateTime(Year, Month, j);
                string BgColor = "";
                int LeaveTypeId = 0;
                int LeaveRequestId = 0;
                string status = "";
                string leavetypename = "";

                string SqlChekLeave = "select dbo.getLeaveStatus_ForCalender(" + EmpId_Row + ",'" + date.ToString("dd-MMM-yyyy") + "')";
                DataTable dtChekLeave = Common.Execute_Procedures_Select_ByQueryCMS(SqlChekLeave);

                if (dtChekLeave.Rows.Count > 0)
                {
                    char[] sep = { '|' };
                    string[] parts = dtChekLeave.Rows[0][0].ToString().Split(sep);
                    if (parts[0] != "N")
                    {
                        LeaveTypeId = Common.CastAsInt32(parts[1]);
                        LeaveRequestId = Common.CastAsInt32(parts[1]);
                        status = parts[0];
                        leavetypename = parts[3];
                    }
                }
                string Title = "";
                //switch (status)
                //{
                //    case "B":
                //        DataTable dtBT = Common.Execute_Procedures_Select_ByQueryCMS("SELECT REPLACE(CONVERT(VARCHAR,LEAVEFROM,106),' ','-') + ' : ' + REPLACE(CONVERT(VARCHAR,LEAVETO,106),' ','-') + ' ' + +'\n' + REASON FROM dbo.HR_OfficeAbsence WHERE EMPID=" + EmpId_Row.ToString() + " AND '" + date.ToString("dd-MMM-yyyy") + "' BETWEEN LEAVEFROM AND LEAVETO");
                //        if (dtBT.Rows.Count > 0)
                //        {
                //            Title = "Business Trip - " + dtBT.Rows[0][0].ToString();
                //        }
                //        break;
                //    case "W":
                //        Title = "Weekly Off";
                //        break;
                //    case "H":
                //        DataTable dtHolyday = Common.Execute_Procedures_Select_ByQueryCMS("SELECT REPLACE(CONVERT(VARCHAR,HOLIDAYFROM,106),' ','-') + ' : ' + REPLACE(CONVERT(VARCHAR,HOLIDAYTO,106),' ','-') + '\n' + HOLIDAYREASON FROM dbo.HR_HolidayMaster WHERE OFFICEID=" + OfficeId.ToString() + " AND YEAR=" + SelectedYear.ToString() + " AND '" + date.ToString("dd-MMM-yyyy") + "' BETWEEN HOLIDAYFROM AND HOLIDAYTO");
                //        if (dtHolyday.Rows.Count > 0)
                //        {
                //            Title = "Holiday - " + dtHolyday.Rows[0][0].ToString();
                //        }
                //        break;
                //    case "L":
                //    case "P":
                //        DataTable dtLeave = Common.Execute_Procedures_Select_ByQueryCMS("SELECT REPLACE(CONVERT(VARCHAR,LEAVEFROM,106),' ','-') + ' : ' + REPLACE(CONVERT(VARCHAR,LEAVETO,106),' ','-') + ' ' + +'\n' + REASON FROM dbo.HR_LeaveRequest WHERE EMPID=" + EmpId_Row.ToString() + " AND '" + date.ToString("dd-MMM-yyyy") + "' BETWEEN LEAVEFROM AND LEAVETO");
                //        if (dtLeave.Rows.Count > 0)
                //        {
                //            Title = "Leave - " + dtLeave.Rows[0][0].ToString();
                //        }
                //        break;                    
                //    default:
                //        Title = "";
                //        break;
                //}
                switch (status)
                {
                    case "B":
                        DataTable dtBT = Common.Execute_Procedures_Select_ByQueryCMS("SELECT REPLACE(CONVERT(VARCHAR,LEAVEFROM,106),' ','-') + ' : ' + REPLACE(CONVERT(VARCHAR,LEAVETO,106),' ','-') + ' ' + +'\n' + REASON FROM dbo.HR_OfficeAbsence WHERE EMPID=" + EmpId_Row.ToString() + " AND '" + date.ToString("dd-MMM-yyyy") + "' BETWEEN LEAVEFROM AND LEAVETO");
                        if (dtBT.Rows.Count > 0)
                        {
                            Title = "Business Trip - " + dtBT.Rows[0][0].ToString();
                        }
                        break;
                    case "A":
                    case "L":
                    case "P":
                        DataTable dtLeave = Common.Execute_Procedures_Select_ByQueryCMS("SELECT REPLACE(CONVERT(VARCHAR,LEAVEFROM,106),' ','-') + ' : ' + REPLACE(CONVERT(VARCHAR,LEAVETO,106),' ','-') + ' ' + +'\n' + REASON FROM dbo.HR_LeaveRequest WHERE EMPID=" + EmpId_Row.ToString() + " AND '" + date.ToString("dd-MMM-yyyy") + "' BETWEEN LEAVEFROM AND LEAVETO");
                        if (dtLeave.Rows.Count > 0)
                        {
                            Title = "Leave - " + dtLeave.Rows[0][0].ToString() + " ( " + leavetypename + " )";
                        }
                        break;
                    case "W":
                        Title = "Weekly Off";
                        break;
                    case "H":
                        DataTable dtHolyday = Common.Execute_Procedures_Select_ByQueryCMS("SELECT REPLACE(CONVERT(VARCHAR,HOLIDAYFROM,106),' ','-') + ' : ' + REPLACE(CONVERT(VARCHAR,HOLIDAYTO,106),' ','-') + '\n' + HOLIDAYREASON FROM dbo.HR_HolidayMaster WHERE OFFICEID=" + OfficeId.ToString() + " AND YEAR=" + SelectedYear.ToString() + " AND '" + date.ToString("dd-MMM-yyyy") + "' BETWEEN HOLIDAYFROM AND HOLIDAYTO");
                        if (dtHolyday.Rows.Count > 0)
                        {
                            Title = "Holiday - " + dtHolyday.Rows[0][0].ToString();
                        }
                        break;
                    case "C":
                        DataTable dtEvent = Common.Execute_Procedures_Select_ByQueryCMS("SELECT EVENTDESCRIPTION FROM dbo.HR_CompanyEvents where [year]=2013 and officeid=" + OfficeId.ToString() + " and '" + date.ToString("dd-MMM-yyyy") + "' between Eventfrom and EventTo ");
                        if (dtEvent.Rows.Count > 0)
                        {
                            Title = "Company Event [ " + dtEvent.Rows[0][0].ToString() + " ]";
                        }
                        break;
                    default:
                        Title = "";
                        break;
                }
                if (status == "L")
                {
                    if (LeaveTypeId !=1)
                    {
                        status = "LO";
                    }
                }
                if (LeaveTypeId > 0)
                {
                    //sb.Append("<td title='" + Title + "' width='17px' class='box_" + status + "'>" + status + "</td>");
                    if (status != "W")
                    {
                        Sb_Temp.Append("<td title='" + Title + "' width='17px' class='box_" + status + "'>" + status.Replace("LO","L") + "</td>");
                        DaysStatus += status;
                    }
                    else
                    {
                        Sb_Temp.Append("<td title='" + Title + "' width='17px' class='box_" + status + "'></td>");
                    }
                }
                else
                {
                    //sb.Append("<td title='" + Title + "' width='17px' class='box_" + status + "'>" + status + "</td>");
                    if (status != "W")
                    {
                        Sb_Temp.Append("<td title='" + Title + "' width='17px' class='box_" + status + "'>" + status.Replace("LO", "L") + "</td>");
                        DaysStatus += status;
                    }
                    else
                    {
                        Sb_Temp.Append("<td title='" + Title + "' width='17px' class='box_" + status + "'></td>");
                    }
                }
            }

            Sb_Temp.Append(@"</tr>");
            //if (DaysStatus.Contains("P") || DaysStatus.Contains("B") || DaysStatus.Contains("A") || DaysStatus.Contains("L"))
            {
                sb.Append(Sb_Temp.ToString());
            }
        }
        sb.Append(@"</table>");
        sb.Append(@"</div>");
        //---------------------
        MonthView.Text = sb.ToString();
    }
}   