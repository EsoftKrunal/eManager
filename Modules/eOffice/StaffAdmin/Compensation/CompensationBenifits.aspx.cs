using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class StaffAdmin_Compensation_CompensationBenifits : System.Web.UI.Page
{
    DateTime ToDay;
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
    //--Page Load Events---------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Session["CurrentPage"] = 1;
        ToDay = DateTime.Today;
        if (!IsPostBack)
        {
            ControlLoader.LoadControl(ddlOffice, DataName.Office, "Select", "0");
            ControlLoader.LoadControl(ddlDept, DataName.HR_Department, "Select", "0", "OfficeId=" + ddlOffice.SelectedValue);
            if (Session["loginid"].ToString() != "1")
            {
                DisableOffice();
            }
            //btn_Search_Click(sender, e);  
        }
    }
    
    public string FormatNumber(object Data)
    {
        return Math.Round(Convert.ToDecimal(Data), 1).ToString("##0.0"); 
    }
    # region --- User Defined Functions ---
    protected void DisableOffice()
    {
        string strSQL = "select Office from Hr_PersonalDetails where EmpId=" + Session["ProfileId"].ToString();
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(strSQL);

        if (dt.Rows.Count > 0)
        {
            ddlOffice.SelectedValue = dt.Rows[0]["Office"].ToString();
            if (dt.Rows[0]["Office"].ToString() != "3")
            {
                ddlOffice.Enabled = false;
            }
        }
    }
    protected void BindGrid()
    {
        int Lastyear;
        Lastyear = ToDay.Year - 1;
        
            Common.Set_Procedures("HR_GetEmployee_CB");
            Common.Set_ParameterLength(4);
            Common.Set_Parameters(new MyParameter("@EmpName", txtEmpName.Text.Trim()),
            new MyParameter("@Office", ddlOffice.SelectedValue.Trim()),
            new MyParameter("@Department", ddlDept.SelectedValue.Trim()),
            new MyParameter("@Year", ToDay.Year.ToString().Trim()));

            DataTable dt = Common.Execute_Procedures_Select_CMS().Tables[0];
            DataView dv = dt.DefaultView;

            if (ddlStatus.SelectedValue.Trim() != "0")
            {
                dv.RowFilter = "Status = '" + ddlStatus.SelectedValue.Trim() + "' ";
            }

            RptLeaveSearch.DataSource = dv.ToTable();
            RptLeaveSearch.DataBind();
            EmpCount.Text = RptLeaveSearch.Items.Count.ToString();
    }
    #endregion

    #region --- Control Events ---
    protected void ddlOffice_SelectedIndexChanged(object sender, EventArgs e)
    {
        ControlLoader.LoadControl(ddlDept, DataName.HR_Department, "Select", "0", "OfficeId=" + ddlOffice.SelectedValue);
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        SelectedId = 0;
        BindGrid();
    }
    protected void btn_Clear_Click(object sender, EventArgs e)
    {
        txtEmpName.Text = "";
        ddlOffice.SelectedIndex = 0;
        BindGrid();
    }
    protected void btndocedit_Click(object sender, ImageClickEventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        Session["EmpId"] = SelectedId;
        //Response.Redirect("Emtm_HR_LeaveStatus.aspx");

        DataTable dtdata = Common.Execute_Procedures_Select_ByQueryCMS("select a.office from Hr_PersonalDetails a left outer join Office b on a.office=b.OfficeId where Empid=" + SelectedId);
        if (dtdata != null)
            if (dtdata.Rows.Count > 0)
            {
                DataRow dr = dtdata.Rows[0];
                ViewState["OfficeId"] = Common.CastAsInt32(dr["office"]);
            }

        ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "PopUPWindow('" + ViewState["OfficeId"] + "');", true);
        BindGrid();
    }
    #endregion

    protected void btn_Generate_Click(object sender, EventArgs e)
    {
        //txtEmpName.Text = "";
        //ddlOffice.SelectedIndex = 0;
        //BindGrid();
    }
    protected void ViewPayslip(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        Session["SelectedEmpID_CB"] = btn.CommandArgument;
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "dfd", " window.open('CB_Details.aspx')", true);
    }


    //- 17 Oct 2016------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    protected void btnViewDocuments_OnClientClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;        
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ss", "window.open('Documents.aspx?EmpID="+ btn.CommandArgument.ToString() + "')", true);
    }

}
