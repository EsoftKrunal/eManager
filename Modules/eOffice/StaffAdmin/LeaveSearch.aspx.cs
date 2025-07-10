using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class emtm_StaffAdmin_Emtm_LeaveSearch : System.Web.UI.Page
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
            btn_Search_Click(sender, e);  
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
        lbllastyear.Text = "-" + Lastyear.ToString();
        lblCurrentYear.Text = "-" + ToDay.Year;
        lblcurryear.Text = "-" + ToDay.Year;
        lblcurryear1.Text = "-" + ToDay.Year; 
        lblcurryear2.Text = "-" + ToDay.Year; 

            Common.Set_Procedures("HR_SearchLeaves");
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
    protected void RptLeaveSearch_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        ImageButton btn = (ImageButton)e.Item.FindControl("btndocedit");
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select * from dbo.getLeaveStatus_OnDate(" + btn.CommandArgument + ",getdate())");
        if(dt.Rows.Count >0)
        {
            /// leave credit --------------
            string sqlLC = "SELECT DBO.HR_Get_LeavesCredit(" + dt.Rows[0]["EmpId"].ToString() + "," + DateTime.Today.Date.ToString("yyyy") + ")";
            DataTable dtLeaveCredit = Common.Execute_Procedures_Select_ByQueryCMS(sqlLC);
            Decimal LeaveCreditCount = Common.CastAsDecimal(dtLeaveCredit.Rows[0][0]);
           
            double result;

            Label lbl = (Label)e.Item.FindControl("lblPrevBalance");
            lbl.Text = dt.Rows[0]["BalLeaveLast"].ToString();

            lbl = (Label)e.Item.FindControl("lblAnnualLeave");
            lbl.Text = string.Format("{0:0.0}", Convert.ToDouble(dt.Rows[0]["AnnLeave_TillDate"]) + Convert.ToDouble(dt.Rows[0]["LieuOffLeave"]));// + Convert.ToDouble(LeaveCreditCount));

            lbl = (Label)e.Item.FindControl("lblTotal");
            lbl.Text = string.Format("{0:0.0}", Convert.ToDouble(dt.Rows[0]["BalLeaveLast"]) + Convert.ToDouble(dt.Rows[0]["AnnLeave_TillDate"]) + Convert.ToDouble(dt.Rows[0]["LieuOffLeave"]) + Convert.ToDouble(LeaveCreditCount));
            result = Convert.ToDouble(lbl.Text); 

            lbl = (Label)e.Item.FindControl("lblCons");
            lbl.Text = string.Format("{0:0.0}", Convert.ToDouble(dt.Rows[0]["ConsLeave"]) + Convert.ToDouble(dt.Rows[0]["LieuOffLeave"]));
            result = result - Convert.ToDouble(lbl.Text);

            lbl = (Label)e.Item.FindControl("lblCredit");
            lbl.Text = string.Format("{0:0.0}", Convert.ToDouble(dt.Rows[0]["CreditLeaves"]));
            result = result - Convert.ToDouble(lbl.Text);
            

            lbl = (Label)e.Item.FindControl("lblCurrBalance");
            //-- lbl.Text = string.Format("{0:0.0}",result); ;//dt.Rows[0]["BalLeaveCurr"].ToString();
            lbl.Text = string.Format("{0:0.0}", Convert.ToDouble(dt.Rows[0]["PayableLeave"].ToString()));
        }
    }
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
    protected void btndocView_Click(object sender, ImageClickEventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        Session["EmpId"] = SelectedId;

        Response.Redirect("Emtm_HR_LeaveStatus.aspx");
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
}
