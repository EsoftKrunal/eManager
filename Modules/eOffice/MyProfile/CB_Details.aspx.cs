using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class emtm_MyProfile_Emtm_CB_Details : System.Web.UI.Page
{
    
    public int EmpID
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
    public int MaxRevisionId
    {
        get
        {
            return Common.CastAsInt32(ViewState["MaxRevisionId"]);
        }
        set
        {
            ViewState["MaxRevisionId"] = value;
        }
    }
    public int RevisionId
    {
        get
        {
            return Common.CastAsInt32(ViewState["RevisionId"]);
        }
        set
        {
            ViewState["RevisionId"] = value;
        }
    }
    
    //--Page Load Events---------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsgSalaryRevision.Text = "";
        lblMsgSalaryRevisionU.Text = "";
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        Session["CurrentPage"] = 1;
        EmpID = Common.CastAsInt32(Session["ProfileId"]);
        if (!IsPostBack)
        {
            ShowEmployeeDetails();
            BindRevisionMaster();
            BindPaySlip_Year();
            BindPaySlip();
        }
    }
    protected void ShowEmployeeDetails()
    {
        string sql = " select EmpCode, FirstName+' '+FamilyName as EmpName from Hr_PersonalDetails where EmpId= " + EmpID + " ";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dt.Rows.Count > 0)
        {
            lblEmpCode.Text = dt.Rows[0]["EmpCode"].ToString();
            lblEmpName.Text = dt.Rows[0]["EmpName"].ToString();
        }
    }
    protected void ShowRevisionDetails()
    {
        string sql = "SELECT TOP 1 replace(CONVERT(varchar, RevisionDate,106),' ','-') RDate,RevisionDate FROM HR_CB_Master where RevisionId=" + RevisionId;
                     
        DataTable dt= Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dt.Rows.Count > 0)
        {
            lblLastRevisionDta.Text = dt.Rows[0]["RDate"].ToString();
        }
         
        sql = " SELECT * FROM HR_CB_Detail cd inner join HR_Comp_Head_Master hm on cd.HeadID=hm.Headid WHERE RevisionId =" + RevisionId +" order by Income_ded desc";
        DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptCurrSalary.DataSource = Dt;
        rptCurrSalary.DataBind();

        Decimal ded_m = Common.CastAsDecimal(Dt.Compute("SUM(HEADVALUE)", "INCOME_DED='D' and ISNULL(YEARLYHEAD,'N')<>'Y'"));
        Decimal income_m = Common.CastAsDecimal(Dt.Compute("SUM(HEADVALUE)", "( INCOME_DED='I' ) and ISNULL(YEARLYHEAD,'N')<>'Y'"));
        lblNetPayable.Text =String.Format("{0:0.00}", income_m - ded_m);

        Decimal gincome_m = Common.CastAsDecimal(Dt.Compute("SUM(HEADVALUE)", "INCOME_DED<>'D' and ISNULL(YEARLYHEAD,'N')<>'Y'"));

        Decimal income_y = Common.CastAsDecimal(Dt.Compute("SUM(HEADVALUE)", "INCOME_DED<>'D' and ISNULL(YEARLYHEAD,'N')='Y'"));
        Decimal ded_y = Common.CastAsDecimal(Dt.Compute("SUM(HEADVALUE)", "INCOME_DED='D' and ISNULL(YEARLYHEAD,'N')='Y'"));

        lblCTC.Text = String.Format("{0:0.00}", (gincome_m - ded_m) * 12 + income_y - ded_y);
        
        //btnUpdateRevisionPopup.Visible = (MaxRevisionId == RevisionId);
        
    }
    
    protected void btnViewRevision_Click(object sender, EventArgs e)
    {
        RevisionId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        ShowRevisionDetails();
    }
    protected void BindRevisionMaster()
    {
        // string sql = " select CompentionMasterID,right(Convert(varchar, SalaryDate,106),8)Period,TotalEncome,TotalDeduction,NetAmount,Locked from HR_CompensationMaster where empid= " + EmpID;
        string sql ="select RevisionId,RevisionDate " +
                    "from " +
                    "HR_CB_Master m " +
                    "where empid = " + EmpID + " ORDER BY RevisionDate DESC";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptRevisionMaster.DataSource = dt;
        rptRevisionMaster.DataBind();
        if (dt.Rows.Count > 0)
        {
            RevisionId = Common.CastAsInt32(dt.Rows[0]["RevisionId"]);
            MaxRevisionId = RevisionId;
        }
        ShowRevisionDetails();
    }


    //----------------------------------------------------------------------------
    protected void btn_AddRevision_Click(object sender, EventArgs e)
    {
        dvSalaryRevision.Visible = true;
        BindEmployeeHeadValue();
        BindYear();
    }
    protected void btnClosePopup_Click(object sender, EventArgs e)
    {
        dvSalaryRevision.Visible = false;
        BindRevisionMaster();
    }
    protected void btnSaveRevisedSalary_Click(object sender, EventArgs e)
    {
        int RevisionID = 0;
        //Data validation
        DataTable DtRevisionDate = Common.Execute_Procedures_Select_ByQueryCMS("select * from HR_CB_Master where EmpID="+EmpID.ToString()+ " and RevisionDate>='15-" + ddlMonth.SelectedItem.Text + "-" + ddlYear.SelectedValue + "'  ");
        if (DtRevisionDate.Rows.Count > 0)
        {
            lblMsgSalaryRevision.Text = "Revision date should be more than last revision date.";
            return;
        }

        DataTable DtHeader = Common.Execute_Procedures_Select_ByQueryCMS("exec HR_IU_CB_Master " + EmpID.ToString()+",'15-"+ddlMonth.SelectedItem.Text+"-"+ddlYear.SelectedValue+"'");
        if (DtHeader != null)
        {
            RevisionID = Common.CastAsInt32(DtHeader.Rows[0][0]);

            foreach (RepeaterItem itm in rptEmployeeLastHeadValues.Items)
            {
                HiddenField hfHeadID = (HiddenField)itm.FindControl("hfHeadID");
                TextBox txtHeadValue = (TextBox)itm.FindControl("txtHeadValue");
                DtHeader = Common.Execute_Procedures_Select_ByQueryCMS("exec HR_IU_CB_Detail " + RevisionID.ToString() + ","+ hfHeadID .Value+ ","+Common.CastAsInt32(txtHeadValue.Text) +"");
            }
        }

        lblMsgSalaryRevision.Text = "Data saved successfully.";
        ShowRevisionDetails();
        BindEmployeeHeadValue();
    }

    // Update revision popup----------------------------------------------------------------------------
    protected void btnUpdateRevisionPopup_OnClick(object sender, EventArgs e)
    {
        dvSalaryRevisionUpdate.Visible = true;
        BindEmployeeHeadValue();
    }
    protected void btnClosePopupU_Click(object sender, EventArgs e)
    {
        dvSalaryRevisionUpdate.Visible = false;
    }

    protected void btnSaveRevisedSalaryU_Click(object sender, EventArgs e)
    {
        int LastRevisionID = Common.CastAsInt32(hfLastRevisionID.Value);
        DataTable DtHeader;

        ////Delete last reviosion data
        DataTable DtRevisionDate = Common.Execute_Procedures_Select_ByQueryCMS(" delete from HR_CB_Detail where RevisionId=" + LastRevisionID + " ");
        //Insert new reviosion data        
        foreach (RepeaterItem itm in rptEmployeeLastHeadValuesU.Items)
        {
            HiddenField hfHeadID = (HiddenField)itm.FindControl("hfHeadIDU");
            TextBox txtHeadValue = (TextBox)itm.FindControl("txtHeadValueU");
            DtHeader = Common.Execute_Procedures_Select_ByQueryCMS("exec HR_IU_CB_Detail " + LastRevisionID.ToString() + "," + hfHeadID.Value + "," + Common.CastAsInt32(txtHeadValue.Text) + "");
        }
        lblMsgSalaryRevisionU.Text = "Data saved successfully.";
        ShowRevisionDetails();
        BindEmployeeHeadValue();
    }




    protected void BindEmployeeHeadValue()
    {

        string sql = " select EmpID, EmpCode, FirstName+' ' + FamilyName as EmpName from Hr_PersonalDetails where EmpID = " + EmpID;                    
        DataTable DtHeader = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (DtHeader.Rows.Count > 0)
        {
            lblEmployeeName.Text = DtHeader.Rows[0]["EmpName"].ToString();

            lblEmployeeNameU.Text = DtHeader.Rows[0]["EmpName"].ToString();
            //lblPeriod.Text = ddlMonth.SelectedItem.Text + " " + ddlYear.SelectedValue;
        }

        //** Show revised amount-------------------------------
        sql = " select top 1 * from HR_Compensation_Revision where Empid=" + EmpID + " and year =" + DateTime.Now.Year + "  ";
        DataTable DtRevisedAmount = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (DtRevisedAmount.Rows.Count > 0)
        {
            
            lblPropesedAmount.Text = DtRevisedAmount.Rows[0]["RevisionAmount"].ToString();
            lblPropesedBonus.Text  = DtRevisedAmount.Rows[0]["Bonus"].ToString();

            lblPropesedAmountU.Text = DtRevisedAmount.Rows[0]["RevisionAmount"].ToString();
            lblPropesedBonusU.Text = DtRevisedAmount.Rows[0]["Bonus"].ToString();
        }
        //---------------------------------------------------------------------------------------------

        sql = " SELECT * FROM HR_Comp_Head_Master hm left join HR_CB_Detail cd on cd.HeadID=hm.Headid and RevisionId IN (SELECT TOP 1 REVISIONID FROM HR_CB_Master WHERE EMPID =" + EmpID+" ORDER BY RevisionDate DESC) " +
              " WHERE officeid = (select office from Hr_PersonalDetails where EmpID = " + EmpID + "  ) order by SRnumber ";

        DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptEmployeeLastHeadValues.DataSource = Dt;

        decimal IncomeTot=Common.CastAsDecimal(Dt.Compute("SUM(HEADVALUE)", "INCOME_DED='I' AND ISNULL(YEARLYHEAD,'N')<>'Y'"));
        decimal DedTot= Common.CastAsDecimal(Dt.Compute("SUM(HEADVALUE)", "INCOME_DED='D'"));
        
        lblReviseTotal.Text= String.Format("{0:0.00}", IncomeTot -DedTot);
        rptEmployeeLastHeadValues.DataBind();

        //-------------------
        int tmprevid = 0;
        if (Dt.Rows.Count>0)
            tmprevid=Common.CastAsInt32(Dt.Rows[0]["RevisionId"].ToString());

        if (tmprevid > 0)
        {
            hfLastRevisionID.Value = Dt.Rows[0]["RevisionId"].ToString();
            rptEmployeeLastHeadValuesU.DataSource = Dt;
            rptEmployeeLastHeadValuesU.DataBind();
            lblReviseTotalU.Text = String.Format("{0:0.00}", IncomeTot - DedTot);

            DataTable dt12 = Common.Execute_Procedures_Select_ByQueryCMS("SELECT TOP 1 REVISIONdate FROM HR_CB_Master WHERE EMPID = " + EmpID + " and REVISIONID=" + tmprevid);
            lblRevDate.Text = Common.ToDateString(dt12.Rows[0][0]);
        }
    }
    protected void BindYear()
    {
        ddlYear.Items.Clear();
        for (int year = DateTime.Today.Year; year>=2014; year--)
        {
            ddlYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
        }
        DateTime lastmonth = DateTime.Today.AddMonths(-1);
        ddlYear.SelectedValue = lastmonth.Year.ToString();
        ddlMonth.SelectedValue = lastmonth.Month.ToString();

    }


    protected void BindPaySlip()
    {

        string sql = "select p.FirstName + ' ' + p.MiddleName + ' '+ p.FamilyName as EmpName,p.Office,Month(salarydate) sal_month,c.* from HR_CompensationMaster c inner join Hr_PersonalDetails p on c.EmpID=p.EmpId where year(salarydate)="+ddlYear_Payslip.SelectedValue+" and c.empid= " + EmpID + "  order by salarydate ";

        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        RptLeaveSearch.DataSource = dt;
        RptLeaveSearch.DataBind();
        //EmpCount.Text = RptLeaveSearch.Items.Count.ToString();
    }
    protected void BindPaySlip_Year()
    {
        for (int i = DateTime.Now.Year; i >= 2012; i--)
        {
            ddlYear_Payslip.Items.Add(i.ToString());
        }
    }
    protected void btnPrint_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField hfOfficeID = (HiddenField)btn.Parent.FindControl("hfOfficeID");
        HiddenField hfsal_month = (HiddenField)btn.Parent.FindControl("hfsal_month");

        //if (ddlOffice.SelectedIndex == 0)
        //{
        //    return;
        //}
        //if (ddlMonth.SelectedIndex == 0)
        //{
        //    return;
        //}

        Session["sPS"] = EmpID + "~" + hfsal_month .Value + "~" + ddlYear_Payslip.SelectedValue + "~" + hfOfficeID.Value;
        
        ScriptManager.RegisterStartupScript(this, this.GetType(), "sd", "window.open('../StaffAdmin/Emtm_Payslip_Crystal.aspx')", true);

    }
    protected void ddlYear_Payslip_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindPaySlip();
    }
    
}


