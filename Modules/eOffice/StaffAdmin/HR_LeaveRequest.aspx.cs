using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class emtm_Emtm_TravelDocs : System.Web.UI.Page
{
    DateTime ToDay;
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
        ToDay = DateTime.Today;

        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 252);
        if (chpageauth <= 0)
        {
            Response.Redirect("../AuthorityError.aspx");
        }
        //*******************
        auth = new AuthenticationManager(252, Common.CastAsInt32(Session["loginid"]), ObjectType.Page);
        
        Session["CurrentPage"] = 4;

        if (!Page.IsPostBack)
        {
            ControlLoader.LoadControl(ddlLeaveStatus, DataName.LeaveStatus, "Select", "");
            ddlLeaveStatus.SelectedValue = "V";  
            BindGrid(ddlLeaveStatus.SelectedValue);
        }
    }
    //-----------------------
    # region --- User Defined Functions ---
    //-- COMMON FUNCTIONS
    private void BindGrid(string LeaveStatusCode)
     {
            string sql = "select a.LeaveRequestId,pd.EmpCode,pd.FirstName +' ' + pd.MiddleName +' ' + pd.FamilyName as [Name],ofice.officename,dept.DeptName, a.LeaveTypeId,b.LeaveTypeName, " +
                         "replace(convert(varchar,a.LeaveFrom,106),' ','-') as LeaveFrom,replace(convert(varchar,a.LeaveTo,106),' ','-') as LeaveTo, " +
                         "(case when a.HalfDay>0 then 0.5 else dbo.HR_Get_LeaveCount(pd.office,leavefrom,leaveto) end) as Duration, " +
                         "case a.Status when 'A' then case when convert(datetime,a.LeaveTo) < convert(datetime,convert(varchar,getdate(),106)) then 'T' else 'A' end "+
                         "else a.Status end as StatusCode,"+ 
                         "case a.Status when 'A' then case when convert(datetime,a.LeaveTo) < convert(datetime,convert(varchar,getdate(),106)) then 'Taken' else 'Approved' end "+
                         "when 'P' then 'Plan' when 'V' then 'Awaiting Approval' when 'R' then 'Rejected' when 'C' then 'Cancelled' else a.Status end as Status "+
                         "from HR_LeaveRequest a left outer join HR_LeaveTypeMaster b on a.LeaveTypeId=b.LeaveTypeId left outer join Hr_PersonalDetails pd on a.empid=pd.empid "+
                         "left outer join office ofice on pd.Office=ofice.OfficeId left outer join HR_Department dept on pd.Department=dept.DeptId where a.status not in ('P','R') "+
                         "and a.status ='" + LeaveStatusCode + "' order by a.LeaveFrom desc";

            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            RptLeaveRequest.DataSource = dt;
            RptLeaveRequest.DataBind();
    }
    #endregion

    #region --- Control Events ---
    protected void btnLeaveVerified_Click(object sender, ImageClickEventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        BindGrid(ddlLeaveStatus.SelectedValue);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "PopUPWindow('" + SelectedId + "');", true);
    }
    protected void btnhdn_Click(object sender, EventArgs e)
    {
        SelectedId = 0;
        BindGrid(ddlLeaveStatus.SelectedValue);
    }
    protected void btnPrint_Click(object sender, ImageClickEventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "PopUPPrintWindow('" + SelectedId + "','P');", true);
    }
    protected void btnLeavePrint_Click(object sender, EventArgs e)
    {

    }
    protected void LnkStatus_Click(object sender, EventArgs e)
    {
        SelectedId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "PopUPWindow('" + SelectedId + "');", true);
    }
    protected void ddlLeaveStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid(ddlLeaveStatus.SelectedValue);
    }
    #endregion
   
   
}
