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
        if (!Page.IsPostBack)
        {
            FillCurrentYear();
            ddlCurrentYear.SelectedValue = ToDay.Year.ToString();   
            ShowRecord(Common.CastAsInt32(Session["EmpId"]));
            setButtons("");
            BindGrid();
            BindLeavesGrid();

            lblLastYear.Text = (Common.CastAsInt32(ddlCurrentYear.SelectedValue) - 1).ToString();
            lblCurYear.Text = ddlCurrentYear.SelectedValue;
            lblCurYear1.Text = ddlCurrentYear.SelectedValue;
            lblCurYear2.Text = ddlCurrentYear.SelectedValue;
            lblCurYear3.Text = ddlCurrentYear.SelectedValue;
         }
    }
    //-----------------------
    # region --- User Defined Functions ---
    //-- COMMON FUNCTIONS
    protected void setButtons(string Action)
    {
        //string EmpMode = Session["EmpMode"].ToString();
       
        //    if (EmpMode == "View")
        //    {
        //        switch (Action)
        //        {
        //            case "View":
        //                tblview.Visible = true;
        //                btnaddnew.Visible = false;
        //                btnsave.Visible = false;
        //                btncancel.Visible = true;
        //                break;
        //            default:
        //                tblview.Visible = false;
        //                btnaddnew.Visible = false;
        //                btnsave.Visible = false;
        //                btncancel.Visible = false;
        //                break;
        //        }
        //    }
        //    if (EmpMode == "Edit")
        //    {
        //        switch (Action)
        //        {
        //            case "View":
        //                tblview.Visible = true;
        //                divTraveldoc.Style.Add("height", "230px;");

        //                btnaddnew.Visible = false;
        //                btnsave.Visible = false;
        //                btncancel.Visible = true;
        //                break;
        //            case "Add":
        //                tblview.Visible = true;
        //                divTraveldoc.Style.Add("height", "230px;");

        //                btnaddnew.Visible = false;
        //                btnsave.Visible = true && auth.IsUpdate;
        //                btncancel.Visible = true;
        //                break;
        //            case "Edit":
        //                tblview.Visible = true;
        //                divTraveldoc.Style.Add("height", "230px;");

        //                btnaddnew.Visible = false;
        //                btnsave.Visible = true && auth.IsUpdate; 
        //                btncancel.Visible = true;
        //                break;
        //            default:
        //                tblview.Visible = false;
        //                divTraveldoc.Style.Add("height", "330px;");

        //                btnaddnew.Visible = true && auth.IsAdd;
        //                btnsave.Visible = false;
        //                btncancel.Visible = false;
        //                break;
        //        }
        //    }
    }
    private void BindGrid()
    {
        int EmpId = Common.CastAsInt32(Session["EmpId"]);
            if (EmpId > 0)
            {
                string sql= "select a.LeaveTypeId,a.LeaveTypeName,isnull(b.LeaveCount,0) as LeaveCount, " +
                            "dbo.HR_Get_AnnLeavesConsumed_Month(1," + ddlCurrentYear.SelectedValue + ",a.LeaveTypeId," + EmpId.ToString() + ") as 'Jan', " +
                            "dbo.HR_Get_AnnLeavesConsumed_Month(2," + ddlCurrentYear.SelectedValue + ",a.LeaveTypeId," + EmpId.ToString() + ") as 'Feb', " +
                            "dbo.HR_Get_AnnLeavesConsumed_Month(3," + ddlCurrentYear.SelectedValue + ",a.LeaveTypeId," + EmpId.ToString() + ") as 'March', " +
                            "dbo.HR_Get_AnnLeavesConsumed_Month(4," + ddlCurrentYear.SelectedValue + ",a.LeaveTypeId," + EmpId.ToString() + ") as 'April', " +
                            "dbo.HR_Get_AnnLeavesConsumed_Month(5," + ddlCurrentYear.SelectedValue + ",a.LeaveTypeId," + EmpId.ToString() + ") as 'May', " +
                            "dbo.HR_Get_AnnLeavesConsumed_Month(6," + ddlCurrentYear.SelectedValue + ",a.LeaveTypeId," + EmpId.ToString() + ") as 'June', " +
                            "dbo.HR_Get_AnnLeavesConsumed_Month(7," + ddlCurrentYear.SelectedValue + ",a.LeaveTypeId," + EmpId.ToString() + ") as 'July', " +
                            "dbo.HR_Get_AnnLeavesConsumed_Month(8," + ddlCurrentYear.SelectedValue + ",a.LeaveTypeId," + EmpId.ToString() + ") as 'August', " +
                            "dbo.HR_Get_AnnLeavesConsumed_Month(9," + ddlCurrentYear.SelectedValue + ",a.LeaveTypeId," + EmpId.ToString() + ") as 'September', " +
                            "dbo.HR_Get_AnnLeavesConsumed_Month(10," + ddlCurrentYear.SelectedValue + ",a.LeaveTypeId," + EmpId.ToString() + ") as 'October', " +
                            "dbo.HR_Get_AnnLeavesConsumed_Month(11," + ddlCurrentYear.SelectedValue + ",a.LeaveTypeId," + EmpId.ToString() + ") as 'November', " +
                            "dbo.HR_Get_AnnLeavesConsumed_Month(12," + ddlCurrentYear.SelectedValue + ",a.LeaveTypeId," + EmpId.ToString() + ") as 'December', " +
                            "dbo.HR_Get_AnnLeavesConsumed_Year_LeaveType(" + ddlCurrentYear.SelectedValue + ",a.LeaveTypeId," + EmpId.ToString() + ") as 'Total' " +
                            "from HR_LeaveTypeMaster a left join HR_LeaveAssignment b on a.LeaveTypeId=b.LeaveTypeId and b.year=" + ddlCurrentYear.SelectedValue.Trim() + " and b.EmpId=" + EmpId.ToString() + " " +
                            "where a.LeaveTypeId <>-1 and a.LeaveTypeId in(select LeaveTypeId from HR_OfficeLeaveMapping where OfficeId=" + ViewState["OfficeId"] + ") order by a.LeaveTypeName";

                DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    for (int j = 3; j <= 15; j++)
                    {
                        if (dt.Rows[i][j].ToString() == "0.0")
                        {
                            dt.Rows[i][j] = DBNull.Value;
                        }
                    }
                }

                rptLeaveDetails.DataSource = dt;
                rptLeaveDetails.DataBind();
            }
    }
    private void BindLeavesGrid()
    {
        int EmpId = Common.CastAsInt32(Session["EmpId"]);
        //string sql = "select a.LeaveRequestId,a.EmpId,a.LeaveTypeId,b.LeaveTypeName,replace(convert(varchar,a.LeaveFrom,106),' ','-') as LeaveFrom, " +
        //             " replace(convert(varchar,a.LeaveTo,106),' ','-') as LeaveTo,case a.HalfDay when 1 then 'First Half' when 2 then 'Second Half' else '' end as HalfDay," +
        //             " a.Reason,case a.Status when 'A' then case when convert(datetime,a.LeaveTo) < convert(datetime,convert(varchar,getdate(),106))  then 'T' else 'A' end " +
        //             " else a.Status end as StatusCode,case a.Status when 'A'  then case when convert(datetime,a.LeaveTo) < convert(datetime,convert(varchar,getdate(),106))  then 'Taken' else 'Approved' end " +
        //             " when 'P' then 'Plan' when 'V' then 'Awaiting Approval' when 'R' then 'Rejected' when 'C' then 'Cancelled' else a.Status end as Status," +
        //             " (case when a.HalfDay>0 then 0.5 else dbo.HR_Get_LeaveCount(" + ViewState["OfficeId"].ToString() + ",leavefrom,leaveto) end) as Duration " +
        //             " from HR_LeaveRequest a left outer join HR_LeaveTypeMaster b on a.LeaveTypeId=b.LeaveTypeId where year(a.Leavefrom)=" + ddlCurrentYear.SelectedValue.Trim() + " and EmpId=" + EmpId.ToString()  + " " +
        //             " and year(a.LeaveTo)=" + ddlCurrentYear.SelectedValue.Trim() + "and a.status <>'P' order by a.LeaveFrom desc";
        string sql = "select a.LeaveRequestId,a.EmpId,a.LeaveTypeId,b.LeaveTypeName,replace(convert(varchar,a.LeaveFrom,106),' ','-') as LeaveFrom, " +
                    " replace(convert(varchar,a.LeaveTo,106),' ','-') as LeaveTo,case a.HalfDay when 1 then 'First Half' when 2 then 'Second Half' else '' end as HalfDay," +
                    " a.Reason,case a.Status when 'A' then case when convert(datetime,a.LeaveTo) < convert(datetime,convert(varchar,getdate(),106))  then 'T' else 'A' end " +
                    " else a.Status end as StatusCode,case a.Status when 'A'  then case when convert(datetime,a.LeaveTo) < convert(datetime,convert(varchar,getdate(),106))  then 'Taken' else 'Approved' end " +
                    " when 'P' then 'Plan' when 'V' then 'Awaiting Approval' when 'R' then 'Rejected' when 'C' then 'Cancelled' else a.Status end as Status," +
                    " (case when a.HalfDay>0 then 0.5 else dbo.HR_Get_LeaveCount(" + ViewState["OfficeId"].ToString() + ",leavefrom,leaveto) end) as Duration " +
                    " from HR_LeaveRequest a left outer join HR_LeaveTypeMaster b on a.LeaveTypeId=b.LeaveTypeId where EmpId=" + EmpId.ToString() + 
                    " AND ( year(a.Leavefrom)="  + ddlCurrentYear.SelectedValue.Trim() + " OR year(a.LeaveTo)=" + ddlCurrentYear.SelectedValue.Trim() + " ) " +
                    " and a.status <>'P' order by a.LeaveFrom desc";

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        RptLeaveRequest.DataSource = dt;
        RptLeaveRequest.DataBind();
    }
    private void BindLeavesGrid(string Month, string LeaveType)
    {
        int EmpId = Common.CastAsInt32(Session["EmpId"]);
        string sql = "";
        if (Month == "0")
        {
            sql = "select a.LeaveRequestId,a.EmpId,a.LeaveTypeId,b.LeaveTypeName,replace(convert(varchar,a.LeaveFrom,106),' ','-') as LeaveFrom, " +
            " replace(convert(varchar,a.LeaveTo,106),' ','-') as LeaveTo,case a.HalfDay when 1 then 'First Half' when 2 then 'Second Half' else '' end as HalfDay," +
            " a.Reason,case a.Status when 'A' then case when convert(datetime,a.LeaveTo) < convert(datetime,convert(varchar,getdate(),106))  then 'T' else 'A' end " +
            " else a.Status end as StatusCode,case a.Status when 'A'  then case when convert(datetime,a.LeaveTo) < convert(datetime,convert(varchar,getdate(),106))  then 'Taken' else 'Approved' end " +
            " when 'P' then 'Plan' when 'V' then 'Awaiting Approval' when 'R' then 'Rejected' when 'C' then 'Cancelled' else a.Status end as Status,a.AppRejRemark, " +
            " (case when a.HalfDay>0 then 0.5 else dbo.HR_Get_LeaveCount(" + ViewState["OfficeId"].ToString() + ",leavefrom,leaveto) end) as Duration " +
            " from HR_LeaveRequest a left outer join HR_LeaveTypeMaster b on a.LeaveTypeId=b.LeaveTypeId " +
            " where EmpId=" + EmpId + " and year(LeaveFrom)=" + ddlCurrentYear.SelectedValue + " and a.Status<>'P' and a.LeaveTypeId=" + LeaveType + " order by a.LeaveFrom desc";

        }
        else
        {
            sql = "select a.LeaveRequestId,a.EmpId,a.LeaveTypeId,b.LeaveTypeName,replace(convert(varchar,a.LeaveFrom,106),' ','-') as LeaveFrom, " +
            " replace(convert(varchar,a.LeaveTo,106),' ','-') as LeaveTo,case a.HalfDay when 1 then 'First Half' when 2 then 'Second Half' else '' end as HalfDay," +
            " a.Reason,case a.Status when 'A' then case when convert(datetime,a.LeaveTo) < convert(datetime,convert(varchar,getdate(),106))  then 'T' else 'A' end " +
            " else a.Status end as StatusCode,case a.Status when 'A'  then case when convert(datetime,a.LeaveTo) < convert(datetime,convert(varchar,getdate(),106))  then 'Taken' else 'Approved' end " +
            " when 'P' then 'Plan' when 'V' then 'Awaiting Approval' when 'R' then 'Rejected' when 'C' then 'Cancelled' else a.Status end as Status,a.AppRejRemark, " +
            " (case when a.HalfDay>0 then 0.5 else dbo.HR_Get_LeaveCount(" + ViewState["OfficeId"].ToString() + ",leavefrom,leaveto) end) as Duration " +
            " from HR_LeaveRequest a left outer join HR_LeaveTypeMaster b on a.LeaveTypeId=b.LeaveTypeId " +
            " where EmpId=" + EmpId + " and " + Month + " between month(LeaveFrom) and month(LeaveTO) and year(LeaveFrom)=" + ddlCurrentYear.SelectedValue + " and a.Status<>'P' and a.LeaveTypeId=" + LeaveType + " order by a.LeaveFrom desc";

        }
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        RptLeaveRequest.DataSource = dt;
        RptLeaveRequest.DataBind();
    }
    public void ShowRecord(int Id)
    {
        int EmpId = Common.CastAsInt32(Session["EmpId"]);
        if (EmpId > 0)
        {
            //string sql = "SELECT *,isnull(dbo.Emtm_GetBalanceLeave(" + Id + "," + ddlCurrentYear.SelectedValue + "),0)as TotalLeaves,BalancePrevYear + AnnualAntitilement - TotalLeaveTaken AS TotalBalance FROM " +
            //           "( " +
            //           "SELECT a.EmpCode,a.firstname,a.middlename,a.familyname,a.Office,c.OfficeName,a.Position,d.DeptName,b.PositionName, " +
            //           "(select isnull(LeaveCount,0) from HR_LeaveAssignment where [year]=" + ddlCurrentYear.SelectedValue + " and EmpId= a.EmpId and LeaveTypeId=-1)as BalancePrevYear, " +
            //           "( " +
            //           "     isnull((select isnull(sum(LeaveCount),0) from HR_LeaveAssignment where EmpId=a.EmpId and LeaveTypeId=1 and [year]=" + ddlCurrentYear.SelectedValue + "),0) " +
            //           "     + " +
            //           "     isnull(dbo.HR_Get_LiewOffLeaves(" + ddlCurrentYear.SelectedValue + ",a.EmpId),0) " +
            //           ") as AnnualAntitilement, " +
            //           "( " +
            //           "     isnull(dbo.HR_Get_AnnLeavesConsumed_Year(" + ddlCurrentYear.SelectedValue + ",a.EmpId),0) " +
            //           "     + " +
            //           "     isnull(dbo.HR_Get_LiewOffLeaves(" + ddlCurrentYear.SelectedValue + ",a.EmpId),0) " +
            //           ") as TotalLeaveTaken " +
            //           "FROM Hr_PersonalDetails a  " +
            //           "left outer join Position b on a.Position=b.PositionId  " +
            //           "Left Outer Join Office c on a.Office= c.OfficeId  " +
            //           "Left Outer Join HR_Department d on a.Department=d.DeptId WHERE EMPID=" + Id + ") A ";

            //DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            //if(dt!=null )
            //    if (dt.Rows.Count > 0)
            //    {
            //        DataRow dr = dt.Rows[0];
            //        lbl_EmpName.Text = "[ " + dr["EmpCode"].ToString() + " ] " + dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["FamilyName"].ToString();
            //        Session["EmpName"] = lbl_EmpName.Text.ToString();

            //        lblOffice.Text = dr["OfficeName"].ToString();
            //        lblDepartment.Text = dr["DeptName"].ToString();
            //        lblDesignation.Text = dr["PositionName"].ToString();
            //        lblBalLeaveLastYear.Text = (dr["BalancePrevYear"].ToString() == string.Empty) ? "0 " : dr["BalancePrevYear"].ToString();
            //        lblAnnualLeaveEntitlement.Text = dr["AnnualAntitilement"].ToString();
            //        lblLeavesTaken.Text = dr["TotalLeaveTaken"].ToString(); ;
            //        lblLeaveBalance.Text = dr["TotalLeaves"].ToString();
            //        ViewState["OfficeId"]  = dr["Office"].ToString();
            //    }

            //string sql1 = "select isnull(dbo.HR_GetMonthLeave(" + Id + "," + ddlCurrentYear.SelectedValue + "),0)";
            //DataTable dt1 = Common.Execute_Procedures_Select_ByQueryCMS(sql1);
            //if (dt1 != null)
            //    if (dt1.Rows.Count > 0)
            //    {
            //        DataRow dr = dt1.Rows[0];
            //        lblCurrentMonthLeave.Text = dr[0].ToString();
            //    }

            string sql = "SELECT a.EmpCode,a.firstname,a.middlename,a.familyname,a.Office,a.Department,c.OfficeName,a.Position,d.DeptName,b.PositionName,ss.* " +
                       "FROM Hr_PersonalDetails a  " +
                       "left join (select * from dbo.getLeaveStatus_OnDate(" + Id + ",'" + ToDay.Date.ToString("dd-MMM-yyyy") + "')) ss on a.empid=ss.empid " +
                       "left outer join Position b on a.Position=b.PositionId  " +
                       "Left Outer Join Office c on a.Office= c.OfficeId  " +
                       "Left Outer Join HR_Department d on a.Department=d.DeptId WHERE a.EMPID=" + Id + " ";

            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            if (dt != null)
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    lbl_EmpName.Text = "[ " + dr["EmpCode"].ToString() + " ] " + dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["FamilyName"].ToString();
                    Session["EmpName"] = lbl_EmpName.Text.ToString();

                    lblOffice.Text = dr["OfficeName"].ToString();
                    lblDepartment.Text = dr["DeptName"].ToString();
                    lblDesignation.Text = dr["PositionName"].ToString();
                    lblBalLeaveLastYear.Text = dr["BalLeaveLast"].ToString();

                    lblAnnualLeaveEntitlement.Text = string.Format("{0:0.0}", (Convert.ToDouble(dr["BalLeaveLast"]) - Convert.ToDouble(dr["BalLeaveLastExpired"]) + Convert.ToDouble(dr["AnnLeave_TillDate"]) + Convert.ToDouble(dr["LieuOffLeave"])));
                    lblLeavesTaken.Text = string.Format("{0:0.0}", (Convert.ToDouble(dr["ConsLeave"]) + Convert.ToDouble(dr["LieuOffLeave"])));
                    lblAccruedLeave.Text = dr["PayableLeave"].ToString();
                    lblLeaveExpired.Text = dr["BalLeaveLastExpired"].ToString();
                    ViewState["OfficeId"] = dr["Office"].ToString();
                    ViewState["DepartmentId"] = dr["Department"].ToString();
                }
        }
    }
    protected void ClearControls()
    {
        //txtCertificateName.Text = "";
        //txtCertificateNo.Text = "";  
        //txtIssuedate.Text = "";
        //txtExpirydate.Text = "";
    }
    protected void FillCurrentYear()
    {
   
        for (int i = ToDay.Year+1; i >=  ToDay.Year-5; i--)
        {
            ddlCurrentYear.Items.Add(i.ToString()) ;
        }
    }
    #endregion

    #region --- Control Events ---
    protected void btndocView_Click(object sender, ImageClickEventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        //BindGrid();
        ShowRecord(SelectedId);
        setButtons("View");
    }
    protected void btndocedit_Click(object sender, ImageClickEventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        //BindGrid_Allocated(0);
        ShowRecord(SelectedId);
        setButtons("Edit");
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        //DateTime date_Issue, date_Expiry;
        //if (!(DateTime.TryParse(txtIssuedate.Text, out date_Issue)))
        //{
        //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Issue Date is Incorrect.');", true);
        //    return;
        //}

        //if (txtExpirydate.Text != "")
        //{

        //    if (!(DateTime.TryParse(txtExpirydate.Text, out date_Expiry)))
        //    {
        //        ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Expiry Date is Incorrect.');", true);
        //        return;
        //    }

        //    if (date_Issue > date_Expiry)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Issue Date should Not be greater than Expiry Date.');", true);
        //        return;
        //    }
        //}
        //object d1 = (txtIssuedate.Text.Trim() == "") ? DBNull.Value : (object)txtIssuedate.Text;
        //object d2 = (txtExpirydate.Text.Trim() == "") ? DBNull.Value : (object)txtExpirydate.Text;

        //FileUpload img = (FileUpload)fldocument;
        //Byte[] imgByte = new Byte[0];
        //string FileName = "";
        //if (img.HasFile && img.PostedFile != null)
        //{
        //    HttpPostedFile File = fldocument.PostedFile;
        //    FileName = fldocument.FileName.Trim();
        //    imgByte = new Byte[File.ContentLength];
        //    File.InputStream.Read(imgByte, 0, File.ContentLength);
        //}

        //int EmpId = Common.CastAsInt32(Session["EmpId"]);
        //Common.Set_Procedures("HR_InsertUpdateCertificateDocDetails");
        //Common.Set_ParameterLength(8);
        //Common.Set_Parameters(new MyParameter("@CertificateDocId", SelectedId),
        //    new MyParameter("@EmpId", EmpId),
        //    new MyParameter("@CertificateName", txtCertificateName.Text.Trim()),
        //    new MyParameter("@CertificateNo", txtCertificateNo.Text.Trim()),
        //    new MyParameter("@IssueDate", d1),
        //    new MyParameter("@ExpiryDate", d2),
        //    new MyParameter("@FileName", FileName),
        //    new MyParameter("@FileImage", imgByte));
        //DataSet ds = new DataSet();
        //if (Common.Execute_Procedures_IUD_CMS(ds))
        //{
        //    SelectedId = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);
        //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);
        //    BindGrid();
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable to save record.');", true);
        //}
    }
    protected void btnaddnew_Click(object sender, EventArgs e)
    {
        SelectedId = 0;
        setButtons("Add");
        ClearControls();
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        SelectedId = 0;
        //BindGrid_Allocated(0);
        setButtons("Cancel"); 
    }
    protected void ddlCurrentYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
        int EmpId = Common.CastAsInt32(Session["EmpId"]);
        ShowRecord(EmpId);
        BindLeavesGrid();
    }
    protected void btnShowLeaves_Click(object sender, EventArgs e)
    {
        char[] Sep = { '|' };
        string[] Res = txtQuery.Text.Split(Sep);
        string Month = Res[0];
        string LTypeId = Res[1];
        BindLeavesGrid(Res[0], Res[1]);
    }
    protected void btnPrint_Click(object sender, ImageClickEventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "PopUPPrintWindow('" + SelectedId + "','P');", true);
    }
    protected void btnLeavePrint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "PopUPLeaveStatusPrintWindow('" + Common.CastAsInt32(ddlCurrentYear.SelectedValue) + "','" + Common.CastAsInt32(Session["EmpId"]) + "','" + ViewState["OfficeId"] + "');", true);
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        int LeaveRequestId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "PopUPLeaveRequestWindow('" + LeaveRequestId + "');", true);
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        BindGrid();
        BindLeavesGrid();
    }
    #endregion
}
