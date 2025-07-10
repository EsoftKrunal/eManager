using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class emtm_StaffAdmin_Compensation_GenerateSalary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        lblMsgHeadValues.Text = "";
        lblMsgLock.Text = "";
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        // Validation for access to a specific user only

        
        //-----------------------------

        if (!IsPostBack)
        {
            for (int year = DateTime.Today.Year; year >= 2016; year--)
            {
                ddlYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
            }
            DateTime lastmonth = DateTime.Today.AddMonths(-1);
            ddlYear.SelectedValue = lastmonth.Year.ToString();
            ddlMonth.SelectedValue = lastmonth.Month.ToString();

            ddlOffice.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM OFFICE ORDER BY OFFICENAME");
            ddlOffice.DataTextField = "OfficeName";
            ddlOffice.DataValueField = "OfficeId";
            ddlOffice.DataBind();

            if (Session["loginid"].ToString() != "1")
            {
                DisableOffice();
            }


            ddlOffice.Items.Insert(0,new ListItem(" < Select Office > ", ""));
            btnShowSalary_Click(sender, e);

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
    protected void btnShowSalary_Click(object sender, EventArgs e)
    {
        DateTime dt = new DateTime(Common.CastAsInt32(ddlYear.SelectedValue), Common.CastAsInt32(ddlMonth.SelectedValue),15);
        DataTable dtLastPaid = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM HR_CompensationMaster WHERE SalaryDate='" + dt.AddMonths(-1).ToString("dd-MMM-yyyy") + "'");

        string Period = "15-" + ddlMonth.SelectedItem.Text + "-" + ddlYear.SelectedValue;
        string sql = "select 1 from HR_PublishPaySlip where OfficeID=0" + ddlOffice.SelectedValue + " and Period='" + Period + "'";
        DataTable DtValidate = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        divDetailHeader.InnerHtml = "";
        divDetailContent.InnerHtml = "";

        if (ddlOffice.SelectedIndex <= 0)
        {
            lblMsg.Text = "Please select office to continue.";
        }
        else if (DtValidate.Rows.Count > 0)
        {
            lblMsg.Text = " Salary has been published for this month ";
            return;
        }
        else
        {
            StringBuilder Table= new StringBuilder();
            DataTable DtHeads = TotalHeads();
            DataTable DtHEmployees = TotalEmployees();

            sql = " select * from HR_CompensationDetails_Temp where month(CalcDate)="+ddlMonth.SelectedValue+ " and year(CalcDate)="+ddlYear.SelectedValue;
            DataTable dtSalTmpData= Common.Execute_Procedures_Select_ByQueryCMS(sql);

            int CosWidth = 0;

            if(DtHeads.Rows.Count!=0)
                CosWidth = 60 / DtHeads.Rows.Count;

            Table.Append("<table cellpadding='2' cellspacing='0' width='100%' border='0' class='gridheader' style='border-collapse:collapse'>");
            Table.Append("<tr>");
            Table.Append("<td style='width:35px;'>");    Table.Append("<img src='../../../Images/AddPencil.gif'  >"); Table.Append("</td>");
            //Table.Append("<td style='width:35px;'>"); Table.Append("<img src='../../../Images/print_16.png'>"); Table.Append("</td>");
            //Table.Append("<td style='width:35px;'> "); Table.Append("<img src='../../../Images/Archive.png' style='width:12px;'>"); Table.Append("</td>");
            Table.Append("<td class='datahead'>");    Table.Append("<span class='datahead'> Emp Name </span>");   Table.Append("</td>");
            foreach (DataRow Dr in DtHeads.Rows)
            {
                Table.Append("<td style='width:" + CosWidth + "%;' class='" +Dr["Income_Ded"].ToString() + "'>");
                Table.Append("<span class='datahead'>" + Dr["HeadName"].ToString() + "</span>");
                Table.Append("</td>");
            }

            //Table.Append("<td style='width:10%' class='I'>"); Table.Append("Total Income"); Table.Append("</td>");
            //Table.Append("<td style='width:10%' class='D'>"); Table.Append("Total Deduction"); Table.Append("</td>");
            Table.Append("<td style='width:10%' >"); Table.Append("<span class='datahead'>" + "Net Payable ( Monthly )" + "</span>"); Table.Append("</td>");
            Table.Append("<td style='width:10%' >"); Table.Append("<span class='datahead'>Last Paid [ " + dt.AddMonths(-1).ToString("MMM-yyyy") + " ]</span>"); Table.Append("</td>");

            Table.Append("</tr>");
            Table.Append("</table >");

            divDetailHeader.InnerHtml = Table.ToString();
            Table.Clear();

            //Details------------------------
            Table.Append("<table cellpadding='2' cellspacing='0' width='100%' border='0' class='gridrow' style='border-collapse:collapse;'>");
            
            foreach (DataRow drd in DtHEmployees.Rows)
            {
                int EmpID = 0; EmpID = Common.CastAsInt32(drd["EmpID"]);
                decimal TotIncome = 0;
                decimal TotDeduction = 0;

                DataRow dr1 = null;
                try
                {
                    dr1 = dtLastPaid.Select("EMPID=" + EmpID)[0];
                }
                catch { }

                Table.Append("<tr>");
                Table.Append("<td style='width:35px;'>"); Table.Append("<img  style='cursor:pointer;' src='../../../Images/AddPencil.gif' onclick=OpenPopup(" + drd["EmpID"].ToString() + ")>"); Table.Append("</td>");

                //Table.Append("<td style='width:35px;'>"); Table.Append("<img style='cursor:pointer;' src='../../../Images/print_16.png' onclick=PrintPaySlip(" + drd["EmpID"].ToString()+ ")>"); Table.Append("</td>");
                //Table.Append("<td style='width:35px;'>"); Table.Append("<img src='/Images/print_16.png' onclick=PrintPaySlip(" + drd["EmpID"].ToString() + ","+ddlMonth.SelectedValue+","+ddlYear.SelectedValue+","+ ")>"); Table.Append("</td>");

                //Table.Append("<td style='width:35px;'>"); Table.Append(" <img style='cursor:pointer;' src='../../../Images/archive.png'  style='width:12px;' >"); Table.Append("</td>");

                Table.Append("<td > "); Table.Append(drd["EmpName"].ToString()); Table.Append("</td>");
                for (int i = 0; i < DtHeads.Rows.Count; i++)
                {
                    DataRow[] DrEmpSal = dtSalTmpData.Select("EmpID=" + EmpID +" and HeadID="+ DtHeads.Rows[i]["HeadId"]);
                    if (DrEmpSal.Length > 0)
                    {
                        if (DtHeads.Rows[i]["Income_Ded"].ToString().ToUpper() == "I")
                        {
                            TotIncome = TotIncome + Common.CastAsDecimal(DrEmpSal[0]["HeadValue"]);
                        }
                        if (DtHeads.Rows[i]["Income_Ded"].ToString().ToUpper() == "D")
                        {
                            TotDeduction = TotDeduction + Common.CastAsDecimal(DrEmpSal[0]["HeadValue"]);
                        }
                    }

                    Table.Append("<td style='width:"+ CosWidth + "%;' class='amount'>");
                    Table.Append(( (DrEmpSal.Length>0) ? DrEmpSal[0]["HeadValue"].ToString():""));
                    Table.Append("</td>");
                }
                //Table.Append("<td style='width:10%' class='amount'>"); Table.Append(TotIncome.ToString()); Table.Append("</td>");
                //Table.Append("<td style='width:10%' class='amount'>"); Table.Append(TotDeduction.ToString()); Table.Append("</td>");
                Table.Append("<td style='width:10%' class='amount'>"); Table.Append((TotIncome-TotDeduction).ToString()); Table.Append("</td>");

                if (dr1 == null)
                { Table.Append("<td style='width:10%' class='amount'>"); Table.Append(""); Table.Append("</td>"); }
                else
                { Table.Append("<td style='width:10%' class='amount'>"); Table.Append(dr1["NetAmount"].ToString()); Table.Append("</td>"); }

                Table.Append("</tr>");
            }
            
            Table.Append("</table >");

            divDetailContent.InnerHtml = Table.ToString();

        }
    }
    protected void btn_ProcessSalary_Click(object sender, EventArgs e)
    {
        DateTime lastmonth = DateTime.Today.AddMonths(-1);
        if (ddlOffice.SelectedIndex <= 0)
        {
            lblMsg.Text = "Please select office to continue.";
            return;
        }

        string Period = "15-"+ddlMonth.SelectedItem.Text+"-"+ddlYear.SelectedValue;
        string sql = "select 1 from HR_PublishPaySlip where OfficeID="+ddlOffice.SelectedValue+" and Period='"+ Period + "'";
        DataTable DtValidate = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (DtValidate.Rows.Count > 0)
        {
            lblMsg.Text = " Salary has been published for this month ";
            return;
        }
        sql = "  Exec HR_CB_Generate_Salary " + ddlOffice.SelectedValue+ ",'15-" + ddlMonth.SelectedItem.Text+"-"+ddlYear.SelectedValue+"'";
            DataTable DtHeader = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            btnShowSalary_Click(sender, e);
    }
    protected void btnGeneratePaySlip_Click(object sender, EventArgs e)
    {
       
    }
    protected DataTable TotalHeads()
    {
        string sql = " select * from HR_Comp_Head_Master where OfficeId=" + ddlOffice.SelectedValue+" order by SRNUMBER ";
        return Common.Execute_Procedures_Select_ByQueryCMS(sql);

    }
    protected DataTable TotalEmployees()
    {
        string sql = " select PD.EmpID,EmpCode,FirstName+' '+FamilyName EmpName from Hr_PersonalDetails PD where Office=" + ddlOffice.SelectedValue+ "  and pd.drc is null and PD.EmpID not in ( select c.empid from HR_CompensationMaster c where Month(c.SALARYDATE)=" + ddlMonth.SelectedValue+ " and Year(c.SALARYDATE)=" + ddlYear.SelectedValue + " ) order by FirstName+' '+FamilyName";
        return Common.Execute_Procedures_Select_ByQueryCMS(sql);

    }


    // Popup
    protected void btnTemp_OnClick(object sender, EventArgs e)
    {
        int EmpID = Common.CastAsInt32(hfEmpID.Value);
        
        dvAddEditSalary.Visible = true;
        BindEmployeeHeadValue(EmpID);
    }
    protected void btnTempReport_OnClick(object sender, EventArgs e)
    {
        int EmpID = Common.CastAsInt32(hfEmpID.Value);
        if (ddlOffice.SelectedIndex== 0)
        {
            return;
        }
        if (ddlMonth.SelectedIndex == 0)
        {
            return;
        }

        Session["sPS"] = EmpID + "~" + ddlMonth.SelectedValue + "~" + ddlYear.SelectedValue + "~" + ddlOffice.SelectedValue;
        //if (ddlYear.SelectedIndex == 0)
        //{
        //    return;
        //}

        //ScriptManager.RegisterStartupScript(this, this.GetType(), "sd", "window.open('emtm/StaffAdmin/Payslip_Crystal.aspx')", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "sd", "window.open('../Payslip_Crystal.aspx')", true);

    }
    protected void btnClosePopup_Click(object sender, EventArgs e)
    {
        dvAddEditSalary.Visible = false;
    }
    protected void btnSaveEmpHeadValue_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem itm in rptEmployeeHeadValues.Items)
        {
            //HiddenField hfEmpID = (HiddenField)itm.FindControl("hfEmpID");
            HiddenField hfHeadID = (HiddenField)itm.FindControl("hfHeadID");
            TextBox txtHeadValue = (TextBox)itm.FindControl("txtHeadValue");
            string sql = "update HR_CompensationDetails_Temp set HeadValue=" + Common.CastAsDecimal( txtHeadValue.Text) + " where empid="+ hfEmpID .Value+ " and HeadId="+ hfHeadID.Value;
            DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        }
        lblMsgHeadValues.Text = "Data saved successfully.";
        btnShowSalary_Click(sender, e);
    }
    protected void btnLock_Click(object sender, EventArgs e)
    {
        if (ddlPaymentMode.SelectedIndex == 0)
        {
            lblMsgLock.Text = "Please select payment mode.";
            ddlPaymentMode.Focus();
            return;
        }
        if (txtPaymentDate.Text.Trim() == "")
        {
            lblMsgLock.Text = "Please enter payment date.";
            txtPaymentDate.Focus();
            return;
        }
        if (txtPaymentRemarks.Text.Trim() == "")
        {
            lblMsgLock.Text = "Please enter payment remark.";
            txtPaymentRemarks.Focus();
            return;
        }
        string sql = " Exec HR_CB_GENERATE_PAYSLIP " + hfEmpID.Value + ",'15-" + ddlMonth.SelectedItem.Text + "-" + ddlYear.SelectedValue + "','" + Session["UserName"].ToString() + "','"+ddlPaymentMode.SelectedValue+"','"+txtPaymentRemarks.Text+"','"+txtPaymentDate.Text+"'";
        Common.Execute_Procedures_Select_ByQueryCMS(sql);
        btnShowSalary_Click(sender, e);
        lblMsgLock.Text = "Locked successfully.";
        btnSaveEmpHeadValue.Visible = false;
        btnLock.Visible = false;
    }
    

    protected void BindEmployeeHeadValue(int EmpID)
    {
        
        string sql = " select EmpID, EmpCode, FirstName+' ' + FamilyName as EmpName from Hr_PersonalDetails where EmpID = "+ EmpID;
        DataTable DtHeader = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (DtHeader.Rows.Count > 0)
        {
            lblEmployeeName.Text = DtHeader.Rows[0]["EmpName"].ToString();
            lblPeriod.Text = ddlMonth.SelectedItem.Text + " " + ddlYear.SelectedValue;
        }


        sql = " select * from HR_CompensationDetails_Temp t  " +
                     "   inner join HR_Comp_Head_Master HM on t.HeadID = HM.HeadID " +
                     "   where EmpID ="+ EmpID +" and month(t.calcDate)="+ddlMonth.SelectedValue+" and Year(t.calcDate)="+ddlYear.SelectedValue+ " order by HM.srnumber ";
        DataTable Dt= Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rptEmployeeHeadValues.DataSource = Dt;
        rptEmployeeHeadValues.DataBind();

        //decimal IncomeTot = Common.CastAsDecimal(Dt.Compute("SUM(HEADVALUE)", "INCOME_DED='I' AND ISNULL(YEARLYHEAD,'N')<>'Y' "));
        decimal IncomeTot = Common.CastAsDecimal(Dt.Compute("SUM(HEADVALUE)", "INCOME_DED='I' "));
        decimal DedTot = Common.CastAsDecimal(Dt.Compute("SUM(HEADVALUE)", "INCOME_DED='D'"));


        DateTime dt = new DateTime(Common.CastAsInt32(ddlYear.SelectedValue), Common.CastAsInt32(ddlMonth.SelectedValue), 15);
        DataTable dtLastPaid = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM HR_CompensationMaster WHERE Empid="+ EmpID + " and  SalaryDate='" + dt.AddMonths(-1).ToString("dd-MMM-yyyy") + "'");

        if (dtLastPaid.Rows.Count > 0)
        {

            ddlPaymentMode.SelectedValue = Convert.ToString(dtLastPaid.Rows[0]["PaymentMode"]);
            if (Convert.ToString(dtLastPaid.Rows[0]["PaymentDate"]) != "")
                txtPaymentDate.Text = Convert.ToDateTime(dtLastPaid.Rows[0]["PaymentDate"]).AddMonths(1).ToString("dd-MMM-yyyy");
            else
                txtPaymentDate.Text = "";
        }
        else
        {
            ddlPaymentMode.SelectedIndex = 0;
            txtPaymentDate.Text = "";
        }
        txtPaymentRemarks.Text = "";
        btnLock.Visible = true;

        lblTotal.Text = String.Format("{0:0.00}", IncomeTot - DedTot);
    }

}