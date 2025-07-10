using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Text;

public partial class emtm_Emtm_LeaveCalender : System.Web.UI.Page
{
    public AuthenticationManager auth;
    public string SessionDeleimeter = ConfigurationManager.AppSettings["SessionDelemeter"];
    public int PeapID
    {
        get
        {
            return Common.CastAsInt32(ViewState["PeapID"]);
        }
        set
        {
            ViewState["PeapID"] = value;
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
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!IsPostBack)
        {
            EmpId = Common.CastAsInt32(Session["ProfileId"]);

            if (EmpId > 0)
            {
                if (Request.QueryString["office"] != null && Request.QueryString["dept"] != null && Request.QueryString["month"] != null && Request.QueryString["year"] != null)
                {
                    SelectedYear = Common.CastAsInt32(Request.QueryString["year"].ToString());
                    SelectedMonth = Common.CastAsInt32(Request.QueryString["month"].ToString());

                    ddlYear.Items.Add(new ListItem(SelectedYear.ToString()));
                    ddlYear.Items.Add(new ListItem((SelectedYear + 1).ToString()));

                    txtMonthId.Text = SelectedMonth.ToString();

                    ControlLoader.LoadControl(ddlLocation, DataName.Office, "NONE", "");

                    OfficeId = Common.CastAsInt32(Request.QueryString["office"].ToString());
                    ddlLocation.SelectedValue = OfficeId.ToString();
                    ddlLocation_SelectedIndexChanged(new object(), new EventArgs());

                    DepartmentId = Common.CastAsInt32(Request.QueryString["dept"].ToString());
                    ddlDepartment.SelectedValue = DepartmentId.ToString();

                }
                else
                {
                    //---------------------------                
                    ddlYear.Items.Add(new ListItem(DateTime.Today.Year.ToString()));
                    ddlYear.Items.Add(new ListItem((DateTime.Today.Year + 1).ToString()));

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

                        DepartmentId = Common.CastAsInt32(dt.Rows[0]["DEPARTMENT"].ToString());
                        ddlDepartment.SelectedValue = dt.Rows[0]["DEPARTMENT"].ToString();
                    }
                }
                hdnShowMonth_Click(sender, e);

            }
        }

    }

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
        sb.Append(@"<div style='width:99%;height:270px;overflow-y:scroll;overflow-x:hidden;border:dotted 1px gray;'>");
        sb.Append(@"<table cellpadding='3' cellspacing='0' border='0' style='border-collapse :collapse; text-align :center;'>");
        //--------- DATA ROWS
        string WhereClause = "where a.Status <>'R' and pd.status<>'I' and ofice.officeid=" + Common.CastAsInt32(ddlLocation.SelectedValue.Trim());

        if (ddlDepartment.SelectedIndex > 0)
            WhereClause += " And dept.deptid=" + Common.CastAsInt32(ddlDepartment.SelectedValue.Trim());

        string SqlEmpName = "select a.empid,pd.FirstName +' ' + pd.MiddleName +' ' + pd.FamilyName as [Name] " +
                          "from HR_LeaveRequest a " +
                          "left outer join Hr_PersonalDetails pd on a.EmpId=pd.EmpId " +
                          "left outer join office ofice on pd.Office=ofice.OfficeId " +
                          "left outer join HR_Department dept on pd.Department=dept.DeptId " + WhereClause +
                          "group by a.empid,pd.firstName,pd.MiddleName,pd.FamilyName";


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
                    }
                }
                string Title = "";
                switch (status)
                {
                    case "B":
                        DataTable dtBT = Common.Execute_Procedures_Select_ByQueryCMS("SELECT REPLACE(CONVERT(VARCHAR,LEAVEFROM,106),' ','-') + ' : ' + REPLACE(CONVERT(VARCHAR,LEAVETO,106),' ','-') + ' ' + +'\n' + REASON FROM dbo.HR_OfficeAbsence WHERE EMPID=" + EmpId_Row.ToString() + " AND '" + date.ToString("dd-MMM-yyyy") + "' BETWEEN LEAVEFROM AND LEAVETO");
                        if (dtBT.Rows.Count > 0)
                        {
                            Title = "Business Trip - " + dtBT.Rows[0][0].ToString();
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
                    case "L":
                    case "P":
                        DataTable dtLeave = Common.Execute_Procedures_Select_ByQueryCMS("SELECT REPLACE(CONVERT(VARCHAR,LEAVEFROM,106),' ','-') + ' : ' + REPLACE(CONVERT(VARCHAR,LEAVETO,106),' ','-') + ' ' + +'\n' + REASON FROM dbo.HR_LeaveRequest WHERE EMPID=" + EmpId_Row.ToString() + " AND '" + date.ToString("dd-MMM-yyyy") + "' BETWEEN LEAVEFROM AND LEAVETO");
                        if (dtLeave.Rows.Count > 0)
                        {
                            Title = "Leave - " + dtLeave.Rows[0][0].ToString();
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
                if (LeaveTypeId > 0)
                {
                    if (status != "W")
                    {
                        Sb_Temp.Append("<td title='" + Title + "' width='17px' class='box_" + status + "'>" + status + "</td>");
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
                        Sb_Temp.Append("<td title='" + Title + "' width='17px' class='box_" + status + "'>" + status + "</td>");
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