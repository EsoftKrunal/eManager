using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class emtm_StaffAdmin_Emtm_Hr_LeaveRegister : System.Web.UI.Page
{
    // User Defined Property
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

    # region User Functions
    private void BindGrid_LeaveType()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select LeaveTypeId,LeaveTypeName from HR_LeaveTypeMaster order by LeaveTypeName");
        RptLeaveType.DataSource = dt;
        RptLeaveType.DataBind();
    }
    private void BindGrid_NotInOffice(int OfficeId)
    {
            string sql = "select LeaveTypeId,LeaveTypeName from HR_LeaveTypeMaster " +
                         "where LeaveTypeId Not In (select leavetypeid from HR_OfficeLeaveMapping where officeid= " + OfficeId + " ) order by LeaveTypeName";
            
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            RptLeaveNotInOffice.DataSource = dt;
            RptLeaveNotInOffice.DataBind();
    }
    private void BindGrid_InOffice(int OfficeId)
    {
            string sql = "select LeaveTypeId,LeaveTypeName from HR_LeaveTypeMaster " +
                         "where LeaveTypeId In (select leavetypeid from HR_OfficeLeaveMapping where officeid= " + OfficeId + " ) order by LeaveTypeName";

            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            RptLeaveInOffice.DataSource = dt;
            RptLeaveInOffice.DataBind();
    }
    private void ClearControl()
    {
    }
    protected void ShowRecord()
    {
        BindGrid_LeaveType();
    }
    #endregion

    #region page Load and Events
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Session["CurrentPage"] = 5;
        if (!IsPostBack)
        {
            ControlLoader.LoadControl(ddlOffice, DataName.Office, "Select", "0");
            BindGrid_LeaveType();
            BindGrid_NotInOffice(0);
        }
    }
    protected void btnLeaveView_Click(object sender, ImageClickEventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        ShowRecord();
        btnsave.Visible = false;
    }
    protected void btnLeaveEdit_Click(object sender, ImageClickEventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        ShowRecord();
        btnsave.Visible = true;
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "PopUPWindow('" + SelectedId + "');", true);
    }
    protected void btnLeaveDelete_Click(object sender, ImageClickEventArgs e)
    {
        int LeaveTypeId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dtdata = Common.Execute_Procedures_Select_ByQueryCMS("DELETE FROM HR_LeaveTypeMaster where LeaveTypeId=" + LeaveTypeId.ToString());
        BindGrid_LeaveType();
        SelectedId = 0; 
        btnsave.Visible = false;
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        ClearControl();
        SelectedId = 0;
        BindGrid_LeaveType();
        btnsave.Visible = true;
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "PopUPWindow('" + SelectedId + "');", true);
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        //Common.Set_Procedures("HR_InsertUpdateLeaveType");
        //Common.Set_ParameterLength(2);
        //Common.Set_Parameters(new MyParameter("@LeaveTypeId", SelectedId),
        //new MyParameter("@LeaveTypeName", txtLeaveType.Text.Trim()));
        //DataSet ds = new DataSet();
        //if (Common.Execute_Procedures_IUD_CMS(ds))
        //{
        //    SelectedId = Common.CastAsInt32(ds.Tables[0].Rows[0][0]);
        // BindGrid_LeaveType();
        // DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("delete from HR_OfficeLeaveMapping where LeaveTypeId=" + SelectedId);
        // foreach (RepeaterItem rptoffice in RptLeaveNotInOffice.Items)
        // {
        //     CheckBox chkOffice = (CheckBox)rptoffice.FindControl("chkOffice");
        //     HiddenField hdnofficeid = (HiddenField)rptoffice.FindControl("hdnofficeid");
        //     if (chkOffice.Checked == true)
        //     {
        //         Common.Set_Procedures("HR_InsertUpdateLeaveRegister");
        //         Common.Set_ParameterLength(2);
        //         Common.Set_Parameters(new MyParameter("@OfficeId", hdnofficeid.Value),
        //             new MyParameter("@LeaveTypeId", SelectedId));
        //         DataSet ds1 = new DataSet();
        //         if (!(Common.Execute_Procedures_IUD_CMS(ds1)))
        //         {
        //             ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable To Save Record. Error :" + Common.getLastError() + "');", true);
        //             return; 
        //         }
        //     }
        // }
        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);
        //}
        //else
        //{
        //   ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable To Save Record. Error :" + Common.getLastError() + "');", true);
        //}
    }
    protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid_NotInOffice(Common.CastAsInt32(ddlOffice.SelectedValue.Trim()));
        BindGrid_InOffice(Common.CastAsInt32(ddlOffice.SelectedValue.Trim()));
    }
    protected void btnMapToOffice_Click(object sender, EventArgs e)
    {
        if (ddlOffice.SelectedIndex > 0)
        {
            bool CheckSelected = false;
            foreach (RepeaterItem rptoffice in RptLeaveNotInOffice.Items)
            {
                CheckBox chkLeaveType = (CheckBox)rptoffice.FindControl("chkLeaveType");
                HiddenField hdnLeaveTypeid = (HiddenField)rptoffice.FindControl("hdnLeaveTypeid");
                if (chkLeaveType.Checked)
                {
                    Common.Set_Procedures("HR_InsertUpdateLeaveRegister");
                    Common.Set_ParameterLength(2);
                    Common.Set_Parameters(new MyParameter("@OfficeId", ddlOffice.SelectedValue.Trim()),
                        new MyParameter("@LeaveTypeId", hdnLeaveTypeid.Value.Trim()));
                    DataSet ds1 = new DataSet();
                    if (!(Common.Execute_Procedures_IUD_CMS(ds1)))
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable To Save Record. Error :" + Common.getLastError() + "');", true);
                        return;
                    }

                    CheckSelected = true;
                }
            }

            if (!CheckSelected)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Please Select Atleast One LeaveType');", true);
                return;
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Please Select Office');", true);
            ddlOffice.Focus();
            return;
        }

            BindGrid_NotInOffice(Common.CastAsInt32(ddlOffice.SelectedValue.Trim()));
            BindGrid_InOffice(Common.CastAsInt32(ddlOffice.SelectedValue.Trim()));
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);
    }
    protected void btnUnMapFromOffice_Click(object sender, EventArgs e)
    {
        bool CheckUnMapSelected = false;

        foreach (RepeaterItem rptoffice in RptLeaveInOffice.Items)
        {
            CheckBox chkLeaveType = (CheckBox)rptoffice.FindControl("chkLeaveType");
            HiddenField hdnLeaveTypeId = (HiddenField)rptoffice.FindControl("hdnLeaveTypeId");
            if (chkLeaveType.Checked)
            {
                DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("delete from HR_OfficeLeaveMapping where LeaveTypeId=" + hdnLeaveTypeId.Value);

                CheckUnMapSelected = true;
            }
        }

        if (!CheckUnMapSelected)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Please Select Atleast One LeaveType');", true);
            return;
        }

        BindGrid_NotInOffice(Common.CastAsInt32(ddlOffice.SelectedValue.Trim()));
        BindGrid_InOffice(Common.CastAsInt32(ddlOffice.SelectedValue.Trim()));
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);
    }
    #endregion
    protected void btnhdn_Click(object sender, EventArgs e)
    {
        BindGrid_LeaveType();
    }
}
