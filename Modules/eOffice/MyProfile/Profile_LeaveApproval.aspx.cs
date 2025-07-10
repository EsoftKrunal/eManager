using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class emtm_Emtm_Profile_LeaveApproval : System.Web.UI.Page
{
    public AuthenticationManager auth; 
    //User Defined Properties
    public int SelectedId
    {
        get
        {
            return Common.CastAsInt32(ViewState["SelectedId"]);
        }
        set
        {
            ViewState["SelectedId"] = value;
        }
    }
    //-----------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Session["CurrentPage"] = 2;
        ////***********Code to check page acessing Permission
        //int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 252);
        //if (chpageauth <= 0)
        //{
        //    Response.Redirect("../AuthorityError.aspx");
        //}
        ////*******************
        //auth = new AuthenticationManager(252, Common.CastAsInt32(Session["loginid"]), ObjectType.Page);
        
        if (!Page.IsPostBack)
        {
            ShowRecord();
            BindGrid();
            Session["EmpId"] = 3;
        }
    }
    //-----------------------
    # region --- User Defined Functions ---
    //-- COMMON FUNCTIONS
    private void ShowRecord()
    {
        int EmpId = Common.CastAsInt32(Session["ProfileId"]);
        if (EmpId > 0)
        {
            string sql="SELECT EmpCode,FirstName,MiddleName,FamilyName FROM Hr_PersonalDetails where EMPID=" + EmpId ;

            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            if(dt!=null )
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    lbl_EmpName.Text = "[ " + dr["EmpCode"].ToString() + " ] " + dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["FamilyName"].ToString();
               }
        }
    }
    private void BindGrid()
     {
        int EmpId = Common.CastAsInt32(Session["ProfileId"]);
        if (EmpId > 0)
        {
            string sql = "select a.LeaveRequestId,pd.EmpCode,pd.FirstName +' ' + pd.MiddleName +' ' + pd.FamilyName as [Name],ofice.officename,dept.DeptName, a.LeaveTypeId,b.LeaveTypeName, " +
                         "replace(convert(varchar,a.LeaveFrom,106),' ','-') as LeaveFrom,replace(convert(varchar,a.LeaveTo,106),' ','-') as LeaveTo,(case when HalfDay>0 then 0.5 else dbo.HR_Get_LeaveCount(pd.Office,leavefrom,leaveto) end) as Duration, " +
                         "a.Status as StatusCode ,case a.Status when 'A'  then case when convert(datetime,a.LeaveTo) < convert(datetime,convert(varchar,getdate(),106))  then 'Taken' else 'Approved' end " +
                         "when 'P' then 'Plan' when 'W' then 'Awaiting Verification'  when 'v' then 'Awaiting Approval' when 'R' then 'Rejected' when 'C' then 'Cancelled' else a.Status end as Status " +
                         "from HR_LeaveRequest a left outer join HR_LeaveTypeMaster b on a.LeaveTypeId=b.LeaveTypeId left outer join Hr_PersonalDetails pd on a.empid=pd.empid " +
                         "left outer join office ofice on pd.Office=ofice.OfficeId left outer join HR_Department dept on pd.Department=dept.DeptId where a.status Not In('p','W') and a.ForwardedTo=" + EmpId + " order by a.LeaveFrom desc";


            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            RptLeaveRequest.DataSource = dt;
            RptLeaveRequest.DataBind();
        }
    }
    #endregion

    #region --- Control Events ---
    protected void btnLeaveApprove_Click(object sender, ImageClickEventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        BindGrid();
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "PopUPWindow('" + SelectedId + "');", true);
    }
    protected void btnhdn_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void btnPrint_Click(object sender, ImageClickEventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "PopUPPrintWindow('" + SelectedId + "','P');", true);
    }
    #endregion
}
