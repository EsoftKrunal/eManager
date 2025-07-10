using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class StaffAdmin_Compensation_Register : System.Web.UI.Page
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
    public int HeadId
    {
        get
        {
            return Common.CastAsInt32(ViewState["HeadId"]);
        }
        set
        {
            ViewState["HeadId"] = value;
        }
    }
    //--Page Load Events---------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Session["CurrentPage"] = 1;
        lblMsg.Text = "";
        ToDay = DateTime.Today;
        if (!IsPostBack)
        {
            ddlOffice.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM OFFICE ORDER BY OFFICENAME");
            ddlOffice.DataTextField = "OfficeName";
            ddlOffice.DataValueField = "OfficeId";
            ddlOffice.DataBind();

            if (Session["loginid"].ToString() != "1")
            {
                DisableOffice();
            }

            BindGrid();
        }
    }

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
    public string FormatNumber(object Data)
    {
        return Math.Round(Convert.ToDecimal(Data), 1).ToString("##0.0");
    }
    protected void ddlOffice_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }    
    protected void BindGrid()
    {

        string sql = " select HeadId,HeadName,Locked, " +
                     "   Case Income_Ded when 'I' then 'Income' when 'D' then 'Deduction' when 'C' then 'CTC Only' else '' end Income_Ded_Text,PayInMonth, " +
                     "   Case CalcMode when 'A' then 'Auto Calculate' when 'M' then 'Mannual Mode' else '' end CalcMode_Text, " +
                     "   Case Fixed_Per when 'F' then 'Fixed' when 'P' then 'Percentage' else '' end Fixed_Per_Text, " +
                     "   YearlyHead, " +
                     "   Status,SrNumber " +
                     "   from HR_Comp_Head_Master where OfficeId=" + ddlOffice.SelectedValue + " order by SrNumber";

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        RptLeaveSearch.DataSource = dt;
        RptLeaveSearch.DataBind();
        EmpCount.Text = RptLeaveSearch.Items.Count.ToString();
        
    }

    

    protected void btn_Add_Click(object sender, EventArgs e)
    {
        dvAddRegister.Visible = true;
    }
    protected void btnAddNewCancel_Click(object sender, EventArgs e)
    {
        dvAddRegister.Visible = false;
        ClearControl();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtHeadName.Text.Trim() == "")
        {
            lblMsg.Text = "Please enter head name";
            txtHeadName.Focus();
            return;
        }


        if (rdolistDeduction.SelectedIndex < 0)
        {
            lblMsg.Text = "Please select head type.";
            rdolistDeduction.Focus();
            return;
        }
        //if (rdolistCalMode.SelectedIndex < 0)
        //{
        //    lblMsg.Text = "Please select cal mode.";
        //    rdolistCalMode.Focus();
        //    return;
        //}
        if (rdoPayin.SelectedValue =="")
        {
            lblMsg.Text = "Please select pay in.";
            rdoPayin.Focus();
            return;
        }

        Common.Set_Procedures("HR_InsertUpdateComp_Head_Master");
        Common.Set_ParameterLength(9);
        Common.Set_Parameters(
            new MyParameter("@HeadId", HeadId),
            new MyParameter("@OfficeId", ddlOffice.SelectedValue),
            new MyParameter("@HeadName", txtHeadName.Text.Trim()),
            new MyParameter("@Income_Ded", rdolistDeduction.SelectedValue),
            new MyParameter("@CalcMode", rdolistCalMode.SelectedValue),
            new MyParameter("@Fixed_Per", rdolistType.SelectedValue),
            new MyParameter("@YearlyHead", rdoPayin.SelectedValue),
            new MyParameter("@PayInMonth","0"),
            new MyParameter("@SrNumber", Common.CastAsInt32( txtSrNumber.Text)));
        

        DataSet ds = new DataSet();
        try
        {
            if (Common.Execute_Procedures_IUD_CMS(ds))
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Record saved successfully.');", true);
                BindGrid();
                btnAddNewCancel_Click(sender, e);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Error while saving record.');", true);
            }
        }
        catch
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "success", "alert('Error while saving record.');", true);
        }

    }
    protected void btndocedit_Click(object sender, ImageClickEventArgs e)
    {
        SelectedId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        HeadId = SelectedId;
        ShowDataForEdit();
        dvAddRegister.Visible = true;
    }



    //- 21 Jun 2016------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void ClearControl()
    {
        HeadId = 0;
        txtHeadName.Text = "";
        rdolistCalMode.ClearSelection();
        rdolistDeduction.ClearSelection();
        rdolistType.ClearSelection();
    }
    public void ShowDataForEdit()
    {
        string sql = " select HeadId,HeadName,Income_Ded,CalcMode,Fixed_Per,Status,YearlyHead,SrNumber from HR_Comp_Head_Master where headid=" + HeadId;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dt.Rows.Count > 0)
        {
            txtHeadName.Text = dt.Rows[0]["HeadName"].ToString();
            txtSrNumber.Text = dt.Rows[0]["SrNumber"].ToString();
            try
            {
                rdolistCalMode.SelectedValue = dt.Rows[0]["CalcMode"].ToString();
            }
            catch (Exception ex)
            {
            }
            try
            {
                rdolistDeduction.SelectedValue = dt.Rows[0]["Income_Ded"].ToString();
            }
            catch (Exception ex)
            {
            }
            try
            {
                rdolistType.SelectedValue = dt.Rows[0]["Fixed_Per"].ToString();
            }
            catch (Exception ex)
            {
            }

            //chkYearlyHead.Checked = (dt.Rows[0]["YearlyHead"].ToString() == "Y");
            try
            {
                rdoPayin.SelectedValue = dt.Rows[0]["YearlyHead"].ToString();
            }
            catch (Exception e)
            {

            }
        }
    }


    //protected void chkYearlyHead_OnCheckedChanged(object sender, EventArgs e)
    //{
    //    if (chkYearlyHead.Checked)
    //    {
    //        ddlPayingMonth.Visible = true;
    //    }
    //    else
    //        ddlPayingMonth.Visible = false;
    //}

    public string GetMonthName(string Month)
    {
        int M = Common.CastAsInt32(Month);
        if (M > 0)
            return new DateTime(2016, M, 1).ToString("MMM");
        else
             return "Monthly";
        
    }
    


}