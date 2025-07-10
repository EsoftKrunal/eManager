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

public partial class emtm_StaffAdmin_Emtm_PopupLeaveAssigned : System.Web.UI.Page
{

    DateTime ToDay;
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
    public int LeaveCreditId
    {
        get
        {
            return Common.CastAsInt32(ViewState["LeaveCreditId"]);
        }
        set
        {
            ViewState["LeaveCreditId"] = value;
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

    # region User Functions
    private void BindGrid()
    {
        string sql = "select lm.leavetypeid,leavetypename,isnull(la.leavecount ,0) as LeaveCount " +
                     "from HR_LeaveTypeMaster lm left join HR_LeaveAssignment la on lm.leavetypeid=la.leavetypeid and la.empid=" + Session["EmpId"] + " and [year]=" + ddlyear.SelectedValue + "" + //ToDay.Year.ToString() 
                     "where lm.leavetypeid in (select leavetypeid from HR_OfficeLeaveMapping where officeid=" + ViewState["OfficeId"] + " and leavetypeid<>2 ) " +
                     "order by lm.leavetypename ";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dt.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('No Leave Assigned for This Location'); window.close();", true);
            return;
        }
        rptLeaveDetails.DataSource = dt;
        rptLeaveDetails.DataBind();


    }
    private void BindLeavesDetails()
    {
        int EmpId = Common.CastAsInt32(Session["EmpId"]);
        if (EmpId > 0)
        {
            string sql = "select a.LeaveRequestId,pd.EmpCode,pd.FirstName +' ' + pd.MiddleName +' ' + pd.FamilyName as [Name],a.LeaveTypeId,b.LeaveTypeName," +
                         "replace(convert(varchar,a.LeaveFrom,106),' ','-') as LeaveFrom,replace(convert(varchar,a.LeaveTo,106),' ','-') as LeaveTo,(case when a.HalfDay>0 then 0.5 else dbo.HR_Get_LeaveCount(pd.office,leavefrom,leaveto) end) as Duration , " +
                         "case a.Status when 'A' then 'Approved' when 'P' then 'Plan' when 'W' then 'Awaiting Approval'  when 'v' then 'Verified' when 'R' then 'Rejected' else a.Status end as Status " +
                         "from HR_LeaveRequest a left outer join HR_LeaveTypeMaster b on a.LeaveTypeId=b.LeaveTypeId " +
                         "left outer join Hr_PersonalDetails pd on a.empid=pd.empid where a.HR_USER='H' and a.empid=" + EmpId + " and a.status<> 'P' and year(a.LeaveFrom)=" + ddlyear.SelectedValue + " order by a.LeaveFrom desc";



            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            RptLeaves.DataSource = dt;
            RptLeaves.DataBind();

            //set edit button visiblity
            foreach (RepeaterItem RptItm in RptLeaves.Items)
            {
                ImageButton btnLeaveDelete = (ImageButton)RptItm.FindControl("btnLeaveDelete");
                if (ddlyear.SelectedValue == System.DateTime.Now.Year.ToString())
                {
                    btnLeaveDelete.Visible = true;
                }
                else
                {
                    btnLeaveDelete.Visible = false;
                }
            }
        }
    }
    private double Adjust(double input)
    {
        double whole = Math.Truncate(input);
        double remainder = input - whole;
        if (remainder < 0.3)
        {
            remainder = 0;
        }
        else if (remainder < 0.8)
        {
            remainder = 0.5;
        }
        else
        {
            remainder = 1;
        }
        return whole + remainder;
    }
    public double LeavesRound(double Real)
    {
        double functionReturnValue = 0;
        int tmp = 0;
        tmp = Common.CastAsInt32(Math.Floor((Math.Round(Real, 1))));
        if ((Real - tmp) >= 0.5)
        {
            functionReturnValue = tmp + 0.5;
        }
        else
        {
            functionReturnValue = tmp;
        }
        return functionReturnValue;
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
    protected void BindYear()
    {
        int CY = Convert.ToInt32(System.DateTime.Now.Year);
        for (int i = 1; i <= 6; i++)
        {
            ddlyear.Items.Add(new ListItem(CY.ToString(), CY.ToString()));
            ddlLCYear.Items.Add(new ListItem(CY.ToString(), CY.ToString()));
            CY = CY - 1;
        }

    }

    #endregion

    #region page Load and Events
    protected void radtab_OnSelectedIndexChanged(object sender,EventArgs e)
    {
        Panel1.Visible = false;
        Panel2.Visible = false;
        Panel3.Visible = false;

        if (radtab.SelectedIndex == 0)
            Panel1.Visible = true;
        else if (radtab.SelectedIndex == 1)
            Panel2.Visible = true;
        else 
            Panel3.Visible = true;

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        ToDay = DateTime.Today;

        if (!Page.IsPostBack)
        {
            BindYear();
            ControlLoader.LoadControl(ddlLeaveType, DataName.HR_LeaveTypeMaster, "Select", "0", "LeaveTypeId <>-1");
            if (Request.QueryString.GetKey(0) != null)
            {
                string OfficeName;
                OfficeName = Request.QueryString.GetKey(0);
                ViewState["OfficeId"] = Common.CastAsInt32(Request.QueryString[OfficeName].Trim());
                //lblLastyear.Text =(ToDay.Year-1).ToString(); 

                DataTable dtdata = Common.Execute_Procedures_Select_ByQueryCMS("select EmpId,EmpCode,FirstName,MiddleName,FamilyName,office,department from Hr_PersonalDetails where EmpId=" + Session["EmpId"]);
                if (dtdata != null)
                    if (dtdata.Rows.Count > 0)
                    {
                        DataRow dr = dtdata.Rows[0];
                        lblEmpName.Text = dr["EmpCode"].ToString() + " - " + dr["FirstName"].ToString() + " " + dr["MiddleName"].ToString() + " " + dr["FamilyName"].ToString();

                        OfficeId = Common.CastAsInt32(dr["office"].ToString());
                        DepartmentId = Common.CastAsInt32(dr["department"].ToString());

                    }
            }
            BindGrid();
            BindLeavesDetails();

            #region ------------- Office Absence --------------------------

            BindGrid1();
            ControlLoader.LoadControl(ddlPurpose, DataName.HR_LeavePurpose, "Select", "", "");

            #endregion

            BindLeaveCredit();
        }
    }
    protected void btnAddLeave_Click(object sender, EventArgs e)
    {
        SelectedId = 0;

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

        int EmpId = Common.CastAsInt32(Session["EmpId"]);
        Common.Set_Procedures("HR_InsertUpdateLeaveRequest");
        Common.Set_ParameterLength(9);
        Common.Set_Parameters(new MyParameter("@LeaveRequestId", SelectedId),
            new MyParameter("@EmpId", EmpId),
            new MyParameter("@LeaveTypeId", ddlLeaveType.SelectedValue.Trim()),
            new MyParameter("@LeaveFrom", d1),
            new MyParameter("@LeaveTo", d2),
            new MyParameter("@HalfDay", HalfDay),
            new MyParameter("@Reason", ""),
            new MyParameter("@Status", "A"),
            new MyParameter("@HR_USER", "H"));

        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD_CMS(ds))
        {
            SelectedId = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);
            BindLeavesDetails();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable To Save Record. Error : " + Common.getLastError() + "');", true);
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //// 
        bool IsSave = true;
        foreach (RepeaterItem rptJobItem in rptLeaveDetails.Items)
        {
            HiddenField hfLeaveTypeId = (HiddenField)rptJobItem.FindControl("hdnLeavetypId");
            TextBox txtLeaveAssigned = (TextBox)rptJobItem.FindControl("txtLeaveAssigned");
            int LeaveTypeId;
            LeaveTypeId = Common.CastAsInt32(hfLeaveTypeId.Value);

            Common.Set_Procedures("HR_InsertUpdateAssignedLeave");
            Common.Set_ParameterLength(4);
            Common.Set_Parameters(
                new MyParameter("@EmpId", Session["EmpId"]),
                new MyParameter("@Year", ToDay.Year),
                new MyParameter("@LeaveTypeId", LeaveTypeId),
                new MyParameter("@AssignedLeaves", txtLeaveAssigned.Text));

            DataSet ds1 = new DataSet();
            if (!(Common.Execute_Procedures_IUD_CMS(ds1)))
            {
                IsSave = false;
                break;
            }
        }
        if (IsSave)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable To Save Record. Error :" + Common.getLastError() + "');", true);
        }
    }
    protected void btnLeaveDelete_Click(object sender, EventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        try
        {
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("Delete From HR_LeaveRequest Where LeaveRequestId=" + SelectedId);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Record Deleted Successfully');", true);
            BindLeavesDetails();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Record Not Deleted');", true);
            return;
        }
    }
    protected void btnhdn_Click(object sender, EventArgs e)
    {

        //    foreach (RepeaterItem rptJobItem in rptLeaveDetails.Items)
        //    {
        //        TextBox txtLeaveAssigned = (TextBox)rptJobItem.FindControl("txtLeaveAssigned");

        //        //txtLeaveAssigned.Text = LeavesRound(Convert.ToDouble(txtLeaveAssigned.Text));
        //    }
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

            txtLeaveTo.Enabled = true;
            imgLeaveTo.Enabled = true;
        }
    }
    protected void btnhdnLeaves_Click(object sender, EventArgs e)
    {
        if (chkHalfDay.Checked)
        {
            txtLeaveTo.Text = txtLeaveFrom.Text.Trim();
            txtLeaveTo.Enabled = false;
            imgLeaveTo.Enabled = false;
        }
        else
        {
            txtLeaveTo.Enabled = true;
            imgLeaveTo.Enabled = true;
        }
    }
    protected void btnhdnRoundOff_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem rptJobItem in rptLeaveDetails.Items)
        {
            TextBox txtLeaveAssigned = (TextBox)rptJobItem.FindControl("txtLeaveAssigned");

            txtLeaveAssigned.Text = LeavesRound(Convert.ToDouble(txtLeaveAssigned.Text)).ToString("0.0");
        }
    }
    protected void btnPrint_Click(object sender, ImageClickEventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "PopUPPrintWindow('" + SelectedId + "','P');", true);
    }
    protected void btnLeavePrint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "PopUPLeaveAssingedPrintWindow();", true);
    }

    public void OnSelectedIndexChanged_ddlyear(object sender, EventArgs e)
    {
        BindGrid();
        BindLeavesDetails();
        if (ddlyear.SelectedValue != System.DateTime.Now.Year.ToString())
        {
            btnAdd.Visible = false;
            btnSave.Visible = false;
            tblAddLeaverequest.Visible = false;
        }
        else
        {
            btnAdd.Visible = true;
            btnSave.Visible = true;
            tblAddLeaverequest.Visible = true;
        }
    }

    #endregion

    #region ----- Office Absence -----------------
    #region --- User Defined Functions ---

    protected void setButtons(string Action)
    {
        switch (Action)
        {
            case "View":
                tblview.Visible = true;
                btnSave1.Visible = false;
                break;
            case "Add":
                tblview.Visible = true;
                btnSave1.Visible = true;
                break;
            case "Show":
                break;
            default:
                //divLeaveDetail.Visible = true;
                break;
        }
    }
    protected void ClearControls()
    {
        ddlPurpose.SelectedIndex = 0;
        txtLeaveFrom1.Text = "";
        txtLeaveTo1.Enabled = true;
        imgLeaveTo1.Enabled = true;
        txtLeaveTo1.Text = "";
        txtReason.Text = "";
        chkHalfDay1.Checked = false;
        chkAfterOfficeHr.Checked = false;
        DisableHalfdayRadio1();
    }
    protected void EnableHalfdayRadio1()
    {
        rdoFirstHalf1.Enabled = true;
        rdoSecondHalf1.Enabled = true;
    }
    protected void DisableHalfdayRadio1()
    {
        rdoFirstHalf1.Enabled = false;
        rdoSecondHalf1.Enabled = false;
    }
    public string ShowLeaveDays()
    {
        if (txtLeaveFrom.Text.Trim() != "")
        {
            object d1 = (txtLeaveFrom1.Text.Trim() == "") ? DBNull.Value : (object)txtLeaveFrom1.Text;
            object d2 = (txtLeaveTo1.Text.Trim() == "") ? DBNull.Value : (object)txtLeaveTo1.Text;

            string sql = "select dbo.HR_Get_LeaveCount(" + OfficeId.ToString() + ",'" + d1 + "','" + d2 + "' )as Duration";
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            return dt.Rows[0][0].ToString();
        }
        else
        {
            return "0.0";
        }
    }
    public string ShowAbsentDays()
    {
        object d1 = (txtLeaveFrom1.Text.Trim() == "") ? DBNull.Value : (object)txtLeaveFrom1.Text;
        object d2 = (txtLeaveTo1.Text.Trim() == "") ? DBNull.Value : (object)txtLeaveTo1.Text;

        if (txtLeaveFrom1.Text.Trim() != "" && txtLeaveTo1.Text.Trim() != "")
        {
            string sql = "select dbo.getAbsentDays(" + Session["EmpId"].ToString() + ",'" + d1 + "','" + d2 + "') as AbsentDays";
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            return dt.Rows[0][0].ToString();
        }
        else
        {
            return "0";
        }
    }
    protected void chkHalfDay1_CheckedChanged(object sender, EventArgs e)
    {
        if (chkHalfDay1.Checked)
        {
            EnableHalfdayRadio1();
            rdoFirstHalf1.Checked = true;

            txtLeaveTo1.Text = txtLeaveFrom1.Text.Trim();
            txtLeaveTo1.Enabled = false;
            imgLeaveTo1.Enabled = false;
        }
        else
        {
            DisableHalfdayRadio1();
            rdoFirstHalf1.Checked = false;

            txtLeaveTo1.Enabled = true;
            imgLeaveTo1.Enabled = true;
        }

        string LeaveDays = ShowLeaveDays();
        //string AbsentDays = ShowAbsentDays();

        if (Common.CastAsInt32(LeaveDays) > Common.CastAsInt32(0) && chkHalfDay1.Checked == true)
        {
            lblLeaveDays.Text = ".5 (Days)";
            //lblAbsentDays.Text = "0 (Days)";
        }
        else
        {
            lblLeaveDays.Text = LeaveDays + " (Days)";
            //lblAbsentDays.Text = AbsentDays + " (Days)";
        }

    }

    protected void btnhdn1_Click(object sender, EventArgs e)
    {
        if (chkHalfDay1.Checked)
        {
            txtLeaveTo1.Text = txtLeaveFrom1.Text.Trim();
            txtLeaveTo1.Enabled = false;
            imgLeaveTo1.Enabled = false;
        }
        else
        {
            txtLeaveTo1.Enabled = true;
            imgLeaveTo1.Enabled = true;
        }

        string LeaveDays = ShowLeaveDays();
        //string AbsentDays = ShowAbsentDays();

        if (Common.CastAsInt32(LeaveDays) > Common.CastAsInt32(0) && chkHalfDay1.Checked == true)
        {
            lblLeaveDays.Text = ".5 (Days)";
            //lblAbsentDays.Text = "0 (Days)";
        }
        else
        {
            lblLeaveDays.Text = LeaveDays + " (Days)";
            //lblAbsentDays.Text = AbsentDays + " (Days)";
        }

    }

    public void ShowRecord(int Id)
    {
        string sql = "SELECT PurposeId,REPLACE(CONVERT(VARCHAR(12),LeaveFrom,106),' ','-') AS LeaveFrom,REPLACE(CONVERT(VARCHAR(12),LeaveTo,106),' ','-') AS LeaveTo,Reason,REPLACE(CONVERT(VARCHAR(12),RequestDate,106),' ','-') AS RequestDate,HalfDay,(case when HalfDay>0 then 0.5 else dbo.HR_Get_LeaveCount(" + OfficeId.ToString() + ",leavefrom,leaveto) end) as Duration,AfterOfficeHr,Location, dbo.getAbsentDays(empid,LeaveFrom,LeaveTo) as AbsentDays " +
                     "FROM HR_OfficeAbsence  WHERE LeaveRequestId =" + Id.ToString() + " ";

        DataTable dtdata = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        if (dtdata != null)
            if (dtdata.Rows.Count > 0)
            {
                int halfdayleave;
                DataRow dr = dtdata.Rows[0];
                ddlLocation.SelectedValue = dr["Location"].ToString().Trim();
                ddlPurpose.SelectedValue = dr["PurposeId"].ToString().Trim();
                txtLeaveFrom1.Text = Convert.ToDateTime(dr["LeaveFrom"]).ToString("dd-MMM-yyyy").Trim();
                txtLeaveTo1.Text = Convert.ToDateTime(dr["LeaveTo"]).ToString("dd-MMM-yyyy").Trim();
                //txtEJDt.Text = (dr["EstimatedReturnDt"]).ToString().Trim() == "" ? "" : Convert.ToDateTime(dr["EstimatedReturnDt"]).ToString("dd-MMM-yyyy").Trim();
                txtReason.Text = (dr["Reason"].ToString() == string.Empty) ? "" : dr["Reason"].ToString().Trim();
                halfdayleave = Common.CastAsInt32(dr["HalfDay"].ToString());
                //lblAbsentDays.Text = dr["AbsentDays"].ToString().Trim();

                if (dr["AfterOfficeHr"].ToString() != "")
                {
                    chkAfterOfficeHr.Checked = Convert.ToBoolean(dr["AfterOfficeHr"].ToString());
                }
                else
                {
                    chkAfterOfficeHr.Checked = false;
                }


                if (halfdayleave == 1)
                {
                    chkHalfDay1.Checked = true;
                    rdoFirstHalf1.Checked = true;
                    EnableHalfdayRadio1();

                    txtLeaveTo1.Text = txtLeaveFrom1.Text.Trim();
                    txtLeaveTo1.Enabled = false;
                    imgLeaveTo1.Enabled = false;
                }
                else if (halfdayleave == 2)
                {
                    chkHalfDay1.Checked = true;
                    rdoSecondHalf1.Checked = true;
                    EnableHalfdayRadio1();

                    txtLeaveTo1.Text = txtLeaveFrom1.Text.Trim();
                    txtLeaveTo1.Enabled = false;
                    imgLeaveTo1.Enabled = false;
                }
                else
                {
                    chkHalfDay1.Checked = false;
                    DisableHalfdayRadio1();
                }

                if (Common.CastAsInt32(dr["duration"]) > Common.CastAsInt32(0) && chkHalfDay1.Checked == true)
                {
                    lblLeaveDays.Text = ".5 (Days)";
                    //lblAbsentDays.Text = "0 (Days)";
                }
                else
                {
                    lblLeaveDays.Text = dr["duration"].ToString() + " (Days)";
                    //lblAbsentDays.Text = dr["AbsentDays"].ToString() + " (Days)";
                }

                //if (halfdayleave > 0)
                //{
                //    lblAbsentDays.Text = "0 (Days)";
                //}

                btnNotify.Visible = false;
                btnSave1.Visible = true;
                btnCancel.Text = "Cancel";
            }
    }

    #endregion
    #region --- Control Events ---

    protected void btnsave1_Click(object sender, EventArgs e)
    {
        int HalfDay;
        if (chkHalfDay1.Checked)
        {
            if (rdoFirstHalf1.Checked)
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
        DateTime date_From, date_To, date_Er;
        if (!(DateTime.TryParse(txtLeaveFrom1.Text, out date_From)))
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('From Date is Incorrect.');", true);
            return;
        }

        if (txtLeaveTo1.Text != "")
        {
            if (!(DateTime.TryParse(txtLeaveTo1.Text, out date_To)))
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('To Date is Incorrect.');", true);
                return;
            }

            if (date_From > date_To)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('To Date should be greater than From Date.');", true);
                return;
            }
        }

        //if (txtEJDt.Text != "")
        //{
        //    if (!(DateTime.TryParse(txtEJDt.Text, out date_Er)))
        //    {
        //        ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Estimated return Date is Incorrect.');", true);
        //        return;
        //    }

        //    if ((Convert.ToDateTime(txtLeaveTo1.Text.Trim()) > date_Er))
        //    {
        //        ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Estimated return Date should be greater than To Date.');", true);
        //        return;
        //    }
        //}

        if (txtReason.Text.Trim() != "" && txtReason.Text.Trim().Length > 500)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Remark should not be more than 500 characters.');", true);
            return;
        }


        object d1 = (txtLeaveFrom1.Text.Trim() == "") ? DBNull.Value : (object)txtLeaveFrom1.Text;
        object d2 = (txtLeaveTo1.Text.Trim() == "") ? DBNull.Value : (object)txtLeaveTo1.Text;

        //int EmpId = Common.CastAsInt32(Session["ProfileId"]);
        Common.Set_Procedures("HR_InsertUpdateOfficeAbsence");
        Common.Set_ParameterLength(10);
        Common.Set_Parameters(new MyParameter("@LeaveRequestId", Common.CastAsInt32(LeaveRequestId)),
            new MyParameter("@EmpId", Session["EmpId"].ToString()),
            new MyParameter("@PurposeId", Common.CastAsInt32(ddlPurpose.SelectedValue.Trim())),
            new MyParameter("@LeaveFrom", d1),
            new MyParameter("@LeaveTo", d2),
            new MyParameter("@HalfDay", HalfDay),
            new MyParameter("@Reason", txtReason.Text.Trim()),
            new MyParameter("@AfterOfficeHr", chkAfterOfficeHr.Checked ? 1 : 0),
            new MyParameter("@Location", ddlLocation.SelectedValue.Trim()),
            new MyParameter("@HR_USER", "H"));


        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD_CMS(ds))
        {
            LeaveRequestId = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);
            btnCancel_Click(sender, e);

            ddlLocation.SelectedIndex = 0;
            ddlPurpose.SelectedIndex = 0;
            txtLeaveFrom1.Text = "";
            txtLeaveTo1.Text = "";
            //txtEJDt.Text = "";
            txtReason.Text = "";
            chkHalfDay1.Checked = false;
            lblLeaveDays.Text = "";
            BindGrid1();

            btnSave1.Visible = false;
            btnNotify.Visible = true;
            btnCancel.Text = "Close";

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Unable To Save Record. Error : " + Common.getLastError() + "');", true);
        }
    }

    #endregion

    #region --- Grid Section ---
    protected void BindGrid1()
    {
        string sql = "SELECT LeaveRequestId,EmpId,Purpose,REPLACE(CONVERT(VARCHAR(12),LeaveFrom,106),' ','-') AS LeaveFrom,REPLACE(CONVERT(VARCHAR(12),LeaveTo,106),' ','-') AS LeaveTo,Reason,REPLACE(CONVERT(VARCHAR(12),RequestDate,106),' ','-') AS RequestDate " +
                     "FROM HR_OfficeAbsence  OA " +
                     "INNER JOIN HR_LeavePurpose LP ON LP.PurposeId = OA.PurposeId " +
                     "WHERE EmpId =" + Session["EmpId"].ToString();

        DataTable Ds = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptOffAbsence.DataSource = Ds;
        rptOffAbsence.DataBind();
    }

    protected void btnView_Click(object sender, ImageClickEventArgs e)
    {
        LeaveRequestId = Common.CastAsInt32(((ImageButton)sender).CommandArgument.ToString());
        BindGrid1();
        ShowRecord(LeaveRequestId);
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        int LeaveRequestId = Common.CastAsInt32(((ImageButton)sender).CommandArgument.ToString());

        string DeleteSQL = "DELETE FROM HR_OfficeAbsence WHERE LeaveRequestId = " + LeaveRequestId.ToString() + " ; SELECT -1";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(DeleteSQL);

        if (dt.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Record deleted successfully.');", true);
            LeaveRequestId = 0;
            BindGrid1();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "failure", "alert('Unable to delete record.');", true);
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        LeaveRequestId = 0;

        if (btnCancel.Text == "Cancel")
        {
            ddlLocation.SelectedIndex = 0;
            ddlPurpose.SelectedIndex = 0;
            txtLeaveFrom1.Text = "";
            txtLeaveTo1.Text = "";
            //txtEJDt.Text = "";
            txtReason.Text = "";
            chkHalfDay1.Checked = false;
            chkAfterOfficeHr.Checked = false;
            lblLeaveDays.Text = "";
            //lblAbsentDays.Text = "";
            BindGrid1();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Close", "window.close();", true);
        }
    }

    protected void btnNotify_Click(object sender, EventArgs e)
    {
        try
        {
            //SendMail();

            LeaveRequestId = 0;
            btnNotify.Visible = false;
            btnSave1.Visible = true;
            btnCancel.Text = "Cancel";
            BindGrid1();

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Notified Successfully.');", true);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Unable To Notify. Error : " + Common.getLastError() + "');", true);
        }
    }

    protected void BindLeaveCredit()
    {
        string sql = "SELECT * FROM HR_LeaveCredit WHERE EmpId=" + Session["EmpId"].ToString();
        DataTable dtLC = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptLeaveCredit.DataSource = dtLC;
        rptLeaveCredit.DataBind();

    }

    protected void ShowLeaveCredit()
    {
        string sql = "SELECT EmpId,[Year], REPLACE(CONVERT(varchar(11),FromDate,106),' ','-') AS FromDate , REPLACE(CONVERT(varchar(11),ToDate,106),' ','-') AS ToDate,HalfDay,[Days],Reason FROM HR_LeaveCredit WHERE LeaveCreditId = " + LeaveCreditId.ToString();
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        ddlLCYear.SelectedValue = dt.Rows[0]["Year"].ToString();
        txtLCFrom.Text = dt.Rows[0]["FromDate"].ToString();
        txtLCTo.Text = dt.Rows[0]["ToDate"].ToString();
        chkLCHalfDay.Checked = (dt.Rows[0]["HalfDay"].ToString() == "1");
        txtNoOfDays.Text = dt.Rows[0]["Days"].ToString();
        txtLCReason.Text = dt.Rows[0]["Reason"].ToString();

        btnLCSave.Visible = true;
    }

    protected void btnLCEdit_Click(object sender, ImageClickEventArgs e)
    {
        LeaveCreditId = Common.CastAsInt32(((ImageButton)sender).CommandArgument.ToString());
        BindLeaveCredit();
        ShowLeaveCredit();
    }

    protected void btnLCDelete_Click(object sender, ImageClickEventArgs e)
    {
        LeaveCreditId = Common.CastAsInt32(((ImageButton)sender).CommandArgument.ToString());

        string DeleteSQL = "DELETE FROM HR_LeaveCredit WHERE LeaveCreditId = " + LeaveCreditId.ToString() + " ; SELECT -1";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(DeleteSQL);

        if (dt.Rows.Count > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Record deleted successfully.');", true);
            LeaveCreditId = 0;
            BindLeaveCredit();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "failure", "alert('Unable to delete record.');", true);
        }

    }

    protected void btnLCSave_Click(object sender, EventArgs e)
    {
        if (txtLCFrom.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Please enter from date.');", true);
            txtLCFrom.Focus();
            return;
        }
        if (txtLCTo.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Please enter to date.');", true);
            txtLCTo.Focus();
            return;
        }

        if (Convert.ToDateTime(txtLCTo.Text.Trim()) < Convert.ToDateTime(txtLCFrom.Text.Trim()))
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('To date can not be less than from date.');", true);
            txtLCTo.Focus();
            return;
        }

        //decimal d;
        //if (!decimal.TryParse(txtNoOfDays.Text.Trim(), out d))
        //{
        //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Please enter valid days.');", true);
        //    txtNoOfDays.Focus();
        //    return;
        //}

        //if (txtNoOfDays.Text.Trim().Contains("."))
        //{
        //    string [] value = txtNoOfDays.Text.Trim().Split('.');

        //    if (value[1].ToString() == "0" || value[1].ToString() == "00" || value[1].ToString() == "5")
        //    {
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Only half days allowed.');", true);
        //        txtNoOfDays.Focus();
        //        return;
        //    }
        //}

        if (txtLCReason.Text.Trim() != "")
        {
            if (txtLCReason.Text.Trim().Length > 500)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Reason should not be more than 500 characters.');", true);
                txtLCReason.Focus();
                return;
            }
        }

        Common.Set_Procedures("HR_InsertUpdateLeaveCredit");
        Common.Set_ParameterLength(9);
        Common.Set_Parameters(new MyParameter("@LeaveCreditId", LeaveCreditId),
            new MyParameter("@EmpId", Session["EmpId"].ToString()),
            new MyParameter("@Year", Common.CastAsInt32(ddlLCYear.SelectedValue.Trim())),
            new MyParameter("@FromDate", txtLCFrom.Text.Trim()),
            new MyParameter("@ToDate", txtLCTo.Text.Trim()),
            new MyParameter("@HalfDay", chkLCHalfDay.Checked ? 1 : 0),
            new MyParameter("@Days", txtNoOfDays.Text.Trim()),
            new MyParameter("@Reason", txtLCReason.Text.Trim()),
            new MyParameter("@LoginId", Common.CastAsInt32(Session["loginid"]))
            );


        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD_CMS(ds))
        {
            //LeaveCreditId = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);
            btnLCCancel_Click(sender, e);

            //ddlLocation.SelectedIndex = 0;
            //ddlPurpose.SelectedIndex = 0;
            //txtLeaveFrom1.Text = "";
            //txtLeaveTo1.Text = "";
            //txtEJDt.Text = "";
            //txtReason.Text = "";
            //chkHalfDay1.Checked = false;
            //lblLeaveDays.Text = "";
            BindLeaveCredit();

            //btnLCSave.Visible = false;
            //btnNotify.Visible = true;
            //btnCancel.Text = "Close";

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "refreshparent();alert('Record saved successfully.');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "error", "alert('Unable To Save Record. Error : " + Common.getLastError() + "');", true);
        }

    }

    protected void btnLCCancel_Click(object sender, EventArgs e)
    {
        LeaveCreditId = 0;
        txtLCFrom.Text = "";
        txtLCTo.Text = "";
        chkLCHalfDay.Checked = false;
        txtNoOfDays.Text = "";
        txtLCReason.Text = "";

        BindLeaveCredit();
        btnLCSave.Visible = true;
    }

    public string ShowLCLeaveDays()
    {
        object d1 = (txtLCFrom.Text.Trim() == "") ? DBNull.Value : (object)txtLCFrom.Text;
        object d2 = (txtLCTo.Text.Trim() == "") ? DBNull.Value : (object)txtLCTo.Text;

        //string sql = "select dbo.HR_Get_LeaveCount(" + OfficeId.ToString() + ",'" + d1 + "','" + d2 + "' )as Duration";
        try
        { 
        return (Convert.ToDateTime(txtLCTo.Text).Subtract(Convert.ToDateTime(txtLCFrom.Text)).Days + 1).ToString();
        }catch(ExecutionEngineException ex)
        { return "0.0"; }
        //DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        //return dt.Rows[0][0].ToString();
}
    
    protected void chkLCHalfDay_CheckedChanged(object sender, EventArgs e)
    {
        if (chkLCHalfDay.Checked)
        {
            txtLCTo.Text = txtLCFrom.Text.Trim();
            txtLCTo.Enabled = false;
            imgLCTo.Enabled = false;
        }
        else
        {
            txtLCTo.Enabled = true;
            imgLCTo.Enabled = true;
        }

        string LeaveDays = ShowLCLeaveDays();

        if (Common.CastAsInt32(LeaveDays) > Common.CastAsInt32(0) && chkLCHalfDay.Checked == true)
        {
            txtNoOfDays.Text = ".5";            
        }
        else
        {
            txtNoOfDays.Text = LeaveDays;            
        }

    }
    protected void btnLChdn_Click(object sender, EventArgs e)
    {
        if (chkLCHalfDay.Checked)
        {
            txtLCTo.Text = txtLCFrom.Text.Trim();
            txtLCTo.Enabled = false;
            imgLCTo.Enabled = false;
        }
        else
        {
            txtLCTo.Enabled = true;
            imgLCTo.Enabled = true;
        }

        string LeaveDays = ShowLCLeaveDays();        

        if (Common.CastAsInt32(LeaveDays) > Common.CastAsInt32(0) && chkLCHalfDay.Checked == true)
        {
            txtNoOfDays.Text = ".5";             
        }
        else
        {
            txtNoOfDays.Text = LeaveDays; 
        }

    }

    #endregion

    #region ----- Mail Functions -----------------

    public string SendMail()
    {
        string ReplyMess = "";
        string MailFrom = "", MailTo = "emanager@energiossolutions.com";
        //Mail From
        string sqlGetMailFrom = "SELECT pd.EmpID,C.Email FROM Hr_PersonalDetails pd LEFT OUTER JOIN USERLOGIN C ON pd.userid=C.LoginId " +
                                "WHERE pd.EmpID=" + Session["EmpId"].ToString();



        DataTable dtGetMailFrom = Common.Execute_Procedures_Select_ByQueryCMS(sqlGetMailFrom);
        if (dtGetMailFrom != null)
            if (dtGetMailFrom.Rows.Count > 0)
            {
                DataRow drGetMailFrom = dtGetMailFrom.Rows[0];
                MailFrom = drGetMailFrom["Email"].ToString();
            }

        String EmpFullName = "", EmpPosition = "";
        string sqlFullNameNPosition = "SELECT (select PositionName from Position P where P.PositionID=pd.Position)Position,(pd.FirstName+' '+pd.FamilyName )as UserName FROM Hr_PersonalDetails pd LEFT OUTER JOIN USERLOGIN C ON pd.userid=C.LoginId where pd.EmpID=" + Session["EmpId"].ToString() + "";
        DataTable dtNamePos = Common.Execute_Procedures_Select_ByQueryCMS(sqlFullNameNPosition);
        if (dtNamePos != null)
            if (dtNamePos.Rows.Count > 0)
            {
                EmpPosition = dtNamePos.Rows[0][0].ToString();
                EmpFullName = dtNamePos.Rows[0][1].ToString();
            }



        //Mail To

        string sqlGetEmployeeInfo = "SELECT LeaveFrom,LeaveTo,Reason,Purpose, dbo.getAbsentDays(OA.empid,OA.LeaveFrom,OA.LeaveTo) as AbsentDays FROM HR_OfficeAbsence OA " +
                                    "INNER JOIN HR_LeavePurpose LP ON OA.PurposeId = LP.PurposeId WHERE LeaveRequestId = " + LeaveRequestId;

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
                String Subject = "Office Absence -- <" + drGetEmployeeInfo["Purpose"].ToString() + ">";
                String MailBody;

                MailBody = "Dear All, ";
                MailBody = MailBody + "<br><br>I will be away from office From <b>" + Convert.ToDateTime(drGetEmployeeInfo["LeaveFrom"]).ToString("dd-MMM-yyyy") + "</b> To <b>" + Convert.ToDateTime(drGetEmployeeInfo["LeaveTo"]).ToString("dd-MMM-yyyy") + "</b>.";
                MailBody = MailBody + "<br><br>Total Office Absence: " + drGetEmployeeInfo["AbsentDays"].ToString() + "";
                MailBody = MailBody + "<br><br>Remarks: " + drGetEmployeeInfo["Reason"].ToString() + "";

                MailBody = MailBody + "<br><br>Thanks & Regards";
                //MailBody = MailBody + "<br>" + drGetEmployeeInfo["Name"].ToString() + "<br><font color=000080 size=2 face=Century Gothic><strong>" + MailFrom.ToString() + "</strong></font>";
                MailBody = MailBody + "<br>" + UppercaseWords(EmpFullName);
                MailBody = MailBody + "<br>" + EmpPosition + "<br><font color=000080 size=2 face=Century Gothic><strong>" + MailFrom.ToString() + "</strong></font>";

                //------------------
                string AttachmentFilePath = "";
                SendEmail.SendeMail(Common.CastAsInt32(Session["EmpId"].ToString()), MailFrom.ToString(), MailFrom.ToString(), ToAdds, CCAdds, BCCAdds, Subject, MailBody, out ReplyMess, AttachmentFilePath);
                //SendMail(ToAdds,CCAdds, Subject, MailBody, AttachmentFilePath, "");
            }

        return ReplyMess;
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

    public void SendMail(string[] ToAddresses, string[] CCAddresses, string Subject, string BodyContent, string AttachMentPath, string MailDetails)
    {
        MailMessage objMessage = new MailMessage();
        SmtpClient objSmtpClient = new SmtpClient();
        StringBuilder msgFormat = new StringBuilder();

        try
        {
            //if (chkTest.Checked)
            //{
            objMessage.From = new MailAddress("asingh@energiossolutions.com");
            objMessage.To.Add("asingh@energiossolutions.com");
            //objMessage.To.Add("asingh@energiossolutions.com");
            //objMessage.To.Add("asingh@energiossolutions.com");

            objSmtpClient.Host = "smtp.gmail.com";
            objSmtpClient.Port = 25;
            objSmtpClient.EnableSsl = true;
            objSmtpClient.Credentials = new NetworkCredential("asingh@energiossolutions.com", "esoft99");
            //}
            //else
            //{
            //    if (MailDetails == "Accident Notification Mail")
            //    {
            //        objMessage.Bcc.Add("asingh@energiossolutions.com");
            //    }
            //    objMessage.From = new MailAddress(SenderAddress);
            //    objSmtpClient.Credentials = new NetworkCredential(SenderUserName, SenderPass);
            //    objSmtpClient.Host = MailClient;
            //    objSmtpClient.Port = Port;

            //    foreach (string add in ToAddresses)
            //    {
            //        objMessage.To.Add(add);
            //    }
            //    if (CCAddresses != null)
            //    {
            //        foreach (string add in CCAddresses)
            //        {
            //            objMessage.CC.Add(add); // Brijveer : - 8764258943
            //        }
            //    }
            //}
            objMessage.Body = BodyContent;
            objMessage.Subject = Subject;
            objMessage.IsBodyHtml = true;
            objMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            if (File.Exists(AttachMentPath))
                objMessage.Attachments.Add(new System.Net.Mail.Attachment(AttachMentPath));
            objSmtpClient.Send(objMessage);
            //AddMessage(MailDetails + "mail sent successfully. FileName:" + AttachMentPath + "");
        }
        catch (System.Exception ex)
        {
            //AddMessage("Error while sending " + MailDetails + "mail. FileName :" + AttachMentPath, ex.Message);
        }
    }


    #endregion

    #endregion 
}

