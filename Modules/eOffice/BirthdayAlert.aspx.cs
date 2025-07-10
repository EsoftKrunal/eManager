using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Text;

public partial class Emtm_BirthdayAlert : System.Web.UI.Page
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
                //-----------------------------
                string sqlToday = "SELECT Firstname + ' ' + Middlename + ' ' + familyname as 'Employee Name',PositionName as Position,DeptName as Department,left(convert(varchar,DateOfBirth,106),6) as DateOfBirth FROM " +
                                "Hr_PersonalDetails pd  " +
                                "left outer join Position P on pd.position = P.PositionId  " +
                                "left outer join office ofice on pd.Office=ofice.OfficeId  " +
                                "left outer join HR_Department dept on pd.Department=dept.DeptId " +
                                "WHERE pd.[Status] <> 'I' and left(CONVERT(varchar,DateOfBirth,106),6)=left(CONVERT(varchar,getdate(),106),6)  " +
                                "ORDER BY 'Employee Name'";
                                
                DataTable dtToday = Common.Execute_Procedures_Select_ByQueryCMS(sqlToday);
                gv1.DataSource = dtToday;
                gv1.DataBind();

                string sqlMonth = "SELECT Firstname + ' ' + Middlename + ' ' + familyname as 'Employee Name',PositionName as Position,DeptName as Department,left(convert(varchar,DateOfBirth,106),6) as DateOfBirth FROM " +
                                "Hr_PersonalDetails pd  " +
                                "left outer join Position P on pd.position = P.PositionId  " +
                                "left outer join office ofice on pd.Office=ofice.OfficeId  " +
                                "left outer join HR_Department dept on pd.Department=dept.DeptId " + 
                                "WHERE pd.[Status] <> 'I' and month(PD.DateOfBirth)=month(GETDATE()) " +
                                "ORDER BY DateOfBirth,'Employee Name'";
                DataTable dtMonth = Common.Execute_Procedures_Select_ByQueryCMS(sqlMonth);
                gv2.DataSource = dtMonth;
                gv2.DataBind();
                //-----------------------------
            }
        }

    }
}