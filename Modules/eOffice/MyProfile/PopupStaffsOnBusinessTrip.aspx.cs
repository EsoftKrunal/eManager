using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text; 

public partial class emtm_MyProfile_Emtm_PopupStaffsOnBusinessTrip : System.Web.UI.Page
{
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
    public string FromDate
    {
        get
        {
            return ViewState["FromDate"].ToString();
        }
        set
        {
            ViewState["FromDate"] = value;
        }
    }
    public string ToDate
    {
        get
        {
            return ViewState["ToDate"].ToString();
        }
        set
        {
            ViewState["ToDate"] = value;

        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!IsPostBack)
        {

            if (Request.QueryString["office"] != null && Request.QueryString["From"] != null && Request.QueryString["To"] != null)
            {
                OfficeId = Common.CastAsInt32(Request.QueryString["office"].ToString());
                FromDate = Request.QueryString["From"].ToString();
                ToDate = Request.QueryString["To"].ToString();

                BindOnBTGrid();
            }
        }
    }

    protected void BindOnBTGrid()
    {
        string SQL = "SELECT DISTINCT NAME,PositionName,Reason,LeaveFrom,LeaveTo,DeptName FROM ( select a.empid,a.LeaveRequestId,pd.EmpCode,pd.FirstName +' ' + pd.MiddleName +' ' + pd.FamilyName as [Name], " +
                     "ofice.officename,dept.DeptName,replace(convert(varchar,a.LeaveFrom,106),' ','-') as LeaveFrom,replace(convert(varchar,a.LeaveTo,106),' ','-') as LeaveTo,(case when HalfDay>0 then 0.5 else dbo.HR_Get_LeaveCount(pd.Office,leavefrom,leaveto) end) as Duration,P.PositionName,a.Reason " +
                     "from HR_OfficeAbsence a  " +                     
                     "left outer join Hr_PersonalDetails pd on a.empid=pd.empid  " +
                     "left outer join Position P on pd.position = P.PositionId " +
                     "left outer join office ofice on pd.Office=ofice.OfficeId  " +
                     "left outer join HR_Department dept on pd.Department=dept.DeptId  " +
                     "WHERE ofice.officeid= " + OfficeId.ToString() + " AND pd.[Status] <> 'I' " +
                     "AND ( ('" + ToDate + "' between a.LeaveFrom and a.LeaveTo) OR ('" + FromDate + "' between a.LeaveFrom and a.LeaveTo) OR(('" + FromDate + "'<= LeaveFrom and '" + ToDate + "' >= LeaveTo)) )) Q ORDER BY DeptName,NAME,LeaveFrom ";

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);

        rptWhoIsOff.DataSource = dt;
        rptWhoIsOff.DataBind();


    }
}