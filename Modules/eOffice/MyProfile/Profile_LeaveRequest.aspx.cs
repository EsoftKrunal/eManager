using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class emtm_Emtm_Profile_LeaveRequest : System.Web.UI.Page
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
            ControlLoader.LoadControl(ddlLeaveType, DataName.HR_LeaveTypeMaster, "Select", "", "LeaveTypeId <>-1");
            ShowRecord(SelectedId);
            DisableHalfdayRadio();
            setButtons("");
            BindGrid();
        }
    }

    # region --- User Defined Functions ---
    //-- COMMON FUNCTIONS
    protected void setButtons(string Action)
    {
        //string EmpMode = Session["EmpMode"].ToString();

        //    if (EmpMode == "View")
        //    {
        //    switch (Action)
        //    {
        //        case "View":
        //            tblview.Visible = true;
        //            btnaddnew.Visible = false;
        //            btnsave.Visible = false;
        //            btncancel.Visible = true;
        //            break;
        //        default:
        //            tblview.Visible = false;
        //            btnaddnew.Visible = false;
        //            btnsave.Visible = false;
        //            btncancel.Visible = false;
        //            break;
        //    }
        //}
        //if (EmpMode == "Edit")
        //{
        switch (Action)
        {
            case "View":
                tblview.Visible = true;
                divLeaveRequest.Style.Add("height", "320px;");

                btnaddnew.Visible = false;
                btnsave.Visible = false;
                btncancel.Visible = true;
                break;
            case "Add":
                tblview.Visible = true;
                divLeaveRequest.Style.Add("height", "320px;");

                btnaddnew.Visible = false;
                btnsave.Visible = true && auth.IsUpdate;
                btncancel.Visible = true;
                break;
            case "Edit":
                tblview.Visible = true;
                divLeaveRequest.Style.Add("height", "320px;");

                btnaddnew.Visible = false;
                btnsave.Visible = true && auth.IsUpdate;
                btncancel.Visible = true;
                break;
            default:
                tblview.Visible = false;
                divLeaveRequest.Style.Add("height", "441px;");

                btnaddnew.Visible = true && auth.IsAdd;
                btnsave.Visible = false;
                btncancel.Visible = false;
                break;
        }
        //}
    }
    private void BindGrid()
    {
        int EmpId = Common.CastAsInt32(Session["ProfileId"]);
        if (EmpId > 0)
        {
            string sql = "select a.LeaveRequestId,a.EmpId,a.LeaveTypeId,b.LeaveTypeName,replace(convert(varchar,a.LeaveFrom,106),' ','-') as LeaveFrom, " +
                        " replace(convert(varchar,a.LeaveTo,106),' ','-') as LeaveTo,case a.HalfDay when 1 then 'First Half' when 2 then 'Second Half' else '' end as HalfDay," +
                        " a.Reason,a.Status as StatusCode,case a.Status when 'A'  then case when convert(datetime,a.LeaveTo) < convert(datetime,convert(varchar,getdate(),106))  then 'Taken' else 'Approved' end "+
                        "when 'P' then 'Plan' when 'W' then 'Awaiting Verification'  when 'v' then 'Awaiting Approval' when 'R' then 'Rejected' when 'C' then 'Cancelled' else a.Status end as Status,a.OfficeRemark,a.AppRejBy " +
                        " from HR_LeaveRequest a left outer join HR_LeaveTypeMaster b on a.LeaveTypeId=b.LeaveTypeId " +
                        " where EmpId=" + EmpId + " order by a.LeaveFrom desc";
  
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            RptLeaveRequest.DataSource = dt;
            RptLeaveRequest.DataBind();
        }
    }
    public void ShowRecord(int Id)
    {
        int EmpId = Common.CastAsInt32(Session["ProfileId"]);
        if (EmpId > 0)
        {
            DataTable dtdata = Common.Execute_Procedures_Select_ByQueryCMS("select * from HR_LeaveRequest WHERE LeaveRequestId=" + Id.ToString());
            if (dtdata != null)
                if (dtdata.Rows.Count > 0)
                {
                    int halfdayleave;
                    DataRow dr = dtdata.Rows[0];
                    ddlLeaveType.SelectedValue = dr["LeaveTypeId"].ToString().Trim();
                    txtLeaveFrom.Text = Convert.ToDateTime(dr["LeaveFrom"]).ToString("dd-MMM-yyyy").Trim();
                    txtLeaveTo.Text = Convert.ToDateTime(dr["LeaveTo"]).ToString("dd-MMM-yyyy").Trim();
                    txtReason.Text = (dr["Reason"].ToString() == string.Empty) ? "" : dr["Reason"].ToString().Trim();
                    halfdayleave = Common.CastAsInt32(dr["HalfDay"].ToString());
                    if (halfdayleave == 1)
                    {
                        chkHalfDay.Checked = true;
                            rdoFirstHalf.Checked =true;
                            EnableHalfdayRadio();

                            txtLeaveTo.Text = txtLeaveFrom.Text.Trim();
                            txtLeaveTo.Enabled = false;
                            imgLeaveTo.Enabled = false;
                    }
                    else if (halfdayleave == 2)
                    {
                        chkHalfDay.Checked = true;
                        rdoSecondHalf.Checked = true;
                        EnableHalfdayRadio();

                        txtLeaveTo.Text = txtLeaveFrom.Text.Trim();
                        txtLeaveTo.Enabled = false;
                        imgLeaveTo.Enabled = false;
                    }
                    else
                    {
                        chkHalfDay.Checked = false;
                        DisableHalfdayRadio();
                    }
                }
        }
    }
    protected void ClearControls()
    {
        ddlLeaveType.SelectedIndex = 0;
        txtLeaveFrom.Text = "";
        txtLeaveTo.Enabled = true;
        imgLeaveTo.Enabled = true;  
        txtLeaveTo.Text = "";
        txtReason.Text = "";
        chkHalfDay.Checked = false;
        DisableHalfdayRadio(); 
    }
    protected void EnableHalfdayRadio()
    {
        rdoFirstHalf.Enabled = true;
        rdoSecondHalf.Enabled = true;
    }
    protected void DisableHalfdayRadio()
    {
        rdoFirstHalf.Enabled = false;
        rdoSecondHalf.Enabled = false;
    }
    #endregion

    #region --- Control Events ---
    protected void btnLeaveView_Click(object sender, ImageClickEventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        BindGrid();
        ShowRecord(SelectedId);
        setButtons("View");
    }
    protected void btnLeaveEdit_Click(object sender, ImageClickEventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        BindGrid();
        ShowRecord(SelectedId);
        setButtons("Edit");
    }
    protected void btnLeaveDelete_Click(object sender, ImageClickEventArgs e)
    {
        //SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        //int EmpId = Common.CastAsInt32(Session["ProfileId"]);
        //if (EmpId > 0)
        //{
        //    try
        //    {
        //        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("delete from HR_FinNricDetails where FinNricId=" + SelectedId);
        //        ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Record Deleted Successfully');", true);
        //        BindGrid();
        //        setButtons("Cancel");
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Record Not Deleted');", true);
        //        return;
        //    }
        //}

        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
      
            int EmpId = Common.CastAsInt32(Session["ProfileId"]);
            Common.Set_Procedures("Emtm_Delete_LeaveRequest");
            Common.Set_ParameterLength(1);
            Common.Set_Parameters(new MyParameter("@LeaveRequestId", SelectedId));
            DataSet ds = new DataSet();
            try
            {
                if (Common.Execute_Procedures_IUD_CMS(ds))
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record Deleted successfully.');", true);
                    BindGrid();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable To Delete Record. Error : " + Common.getLastError() + "');", true);
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable To Delete Record. Error : " + Common.getLastError() + "');", true);
            }

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        int HalfDay;
        if (chkHalfDay.Checked)
        {
            if (rdoFirstHalf.Checked)
            {
                HalfDay = 1;
            }
            else
            {
                HalfDay = 2;
            }
        }
        else
        {
            HalfDay = 0;
        }

        DateTime date_From, date_To;
        if (!(DateTime.TryParse(txtLeaveFrom.Text, out date_From)))
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('From Date is Incorrect.');", true);
            return;
        }

        if (txtLeaveTo.Text != "")
        {
            if (!(DateTime.TryParse(txtLeaveTo.Text, out date_To)))
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('To Date is Incorrect.');", true);
                return;
            }

            if (date_From > date_To)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('To Date should Not be greater than From Date.');", true);
                return;
            }
        }

        object d1 = (txtLeaveFrom.Text.Trim() == "") ? DBNull.Value : (object)txtLeaveFrom.Text;
        object d2 = (txtLeaveTo.Text.Trim() == "") ? DBNull.Value : (object)txtLeaveTo.Text;

        int EmpId = Common.CastAsInt32(Session["ProfileId"]);
        Common.Set_Procedures("HR_InsertUpdateLeaveRequest");
        Common.Set_ParameterLength(13);
        Common.Set_Parameters(new MyParameter("@LeaveRequestId", SelectedId),
            new MyParameter("@EmpId", EmpId),
            new MyParameter("@LeaveTypeId", ddlLeaveType.SelectedValue.Trim()),
            new MyParameter("@LeaveFrom", d1),
            new MyParameter("@LeaveTo", d2),
            new MyParameter("@HalfDay", HalfDay),
            new MyParameter("@Reason",txtReason.Text.Trim()),
            new MyParameter("@Status", "P"),
            new MyParameter("@RequestDate", DBNull.Value),
            new MyParameter("@AppRejBy", DBNull.Value),
            new MyParameter("@AppRejDate", DBNull.Value),
            new MyParameter("@OfficeRemark", ""),
            new MyParameter("@HR_USER", "U"));

        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD_CMS(ds))
        {
            SelectedId = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);
            BindGrid();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable To Save Record. Error : " + Common.getLastError() + "');", true);
        }
    }
    protected void btnLeaveView_Click(object sender, EventArgs e)
    {
        SelectedId = 0;
        setButtons("Add");
        ClearControls();
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        SelectedId = 0;
        BindGrid();
        setButtons("Cancel");
    }
    protected void btnaddnew_Click(object sender, EventArgs e)
    {
        SelectedId = 0;
        setButtons("Add");
        ClearControls();
    }
    protected void chkHalfDay_CheckedChanged(object sender, EventArgs e)
    {
        if (chkHalfDay.Checked)
        {
            EnableHalfdayRadio();
            rdoFirstHalf.Checked = true;

            txtLeaveTo.Text = txtLeaveFrom.Text.Trim();
            txtLeaveTo.Enabled = false;
            imgLeaveTo.Enabled = false;
        }
        else
        {
            DisableHalfdayRadio();
            rdoFirstHalf.Checked = false; 
        }
    }
    #endregion
}
