using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class emtm_StaffAdmin_Emtm_Hr_AddLeaveType : System.Web.UI.Page
{
    # region User Functions
    public void ShowRecord()
    {
        DataTable dtdata = Common.Execute_Procedures_Select_ByQueryCMS("select LeaveTypeId,LeaveTypeName from HR_LeaveTypeMaster where LeaveTypeId =" + ViewState["LeaveTypeId"]);
        if (dtdata != null)
            if (dtdata.Rows.Count > 0)
            {
                DataRow dr = dtdata.Rows[0];
                txtLeaveType.Text = dr["LeaveTypeName"].ToString().Trim();
            }
    }
    #endregion

    #region page Load and Events
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!Page.IsPostBack)
        {
            if (Request.QueryString.GetKey(0) != null)
            {
                string LeaveTypeID;
                LeaveTypeID = Request.QueryString.GetKey(0);
                ViewState["LeaveTypeId"] = Common.CastAsInt32(Request.QueryString[LeaveTypeID].Trim());

                ShowRecord();
            } 
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        Common.Set_Procedures("HR_InsertUpdateLeaveType");
        Common.Set_ParameterLength(2);
        Common.Set_Parameters(new MyParameter("@LeaveTypeId", ViewState["LeaveTypeId"]),
        new MyParameter("@LeaveTypeName", txtLeaveType.Text.Trim()));
        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD_CMS(ds))
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Unable To Save Record. Error :" + Common.getLastError() + "');", true);
        }
    }
    #endregion
}
