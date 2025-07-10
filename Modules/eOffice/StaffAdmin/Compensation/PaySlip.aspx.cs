using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class StaffAdmin_Compensation_PaySlip : System.Web.UI.Page
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
        lblMsgPublish.Text = "";
        Session["CurrentPage"] = 3;
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

            ddlMonth.Items.Add(new ListItem("Month", "0"));
            for (int i = 1; i <= 12; i++)
            {
                ddlMonth.Items.Add(new ListItem(new DateTime(2016, i, 1).ToString("MMM"), i.ToString()));
            }
            for (int i = DateTime.Today.Year; i >= 2016; i--)
            {
                ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            DateTime Lastmonth = DateTime.Today.AddMonths(-1);
            ddlYear.SelectedValue = Lastmonth.Year.ToString();
            ddlMonth.SelectedValue = Lastmonth.Month.ToString();
            BindEmp();
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
        BindEmp();
    }
    protected void BindEmp()
    {
        string sql = "";
        if (ddlStatus.SelectedValue == "A")
            sql = "select empid,p.FirstName + ' ' + p.MiddleName + ' '+ p.FamilyName as EmpName from Hr_PersonalDetails p where office=" + ddlOffice.SelectedValue + " and drc is null order by empname";
        else
            sql = "select empid,p.FirstName + ' ' + p.MiddleName + ' '+ p.FamilyName as EmpName from Hr_PersonalDetails p where office=" + ddlOffice.SelectedValue + " order by empname";

        ddlEmp.DataSource = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        ddlEmp.DataTextField = "EmpName";
        ddlEmp.DataValueField = "empid";
        ddlEmp.DataBind();
        ddlEmp.Items.Insert(0, new ListItem(" All ", ""));
    }
    private void ExporttoExcel(DataTable table)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");

        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
          "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
          "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
        //am getting my grid's column headers
        int columnscount = table.Columns.Count;

        for (int j = 0; j < columnscount; j++)
        {      //write in new column
            HttpContext.Current.Response.Write("<Td>");
            //Get column headers  and make it as bold in excel columns
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write(table.Columns[j].ColumnName);
            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</Td>");
        }
        HttpContext.Current.Response.Write("</TR>");
        foreach (DataRow row in table.Rows)
        {//write in new row
            HttpContext.Current.Response.Write("<TR>");
            for (int i = 0; i < table.Columns.Count; i++)
            {
                HttpContext.Current.Response.Write("<Td>");
                HttpContext.Current.Response.Write(row[i].ToString());
                HttpContext.Current.Response.Write("</Td>");
            }

            HttpContext.Current.Response.Write("</TR>");
        }
        HttpContext.Current.Response.Write("</Table>");
        HttpContext.Current.Response.Write("</font>");
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }

    protected void BindGrid(bool isExport=false)
    {
        string whereclause = " and office=" + ddlOffice.SelectedValue;
        string sql = "select p.FirstName + ' ' + p.MiddleName + ' '+ p.FamilyName as EmpName,p.Office,c.* from HR_CompensationMaster c inner join Hr_PersonalDetails p on c.EmpID=p.EmpId where year(salarydate)=" + ddlYear.SelectedValue;

        if(ddlStatus.SelectedIndex==0)
            whereclause += " and p.DRC is null ";

        if (ddlEmp.SelectedIndex > 0)
            whereclause += " and c.empid=" + ddlEmp.SelectedValue;

        if (ddlMonth.SelectedIndex > 0)
            whereclause += " and month(salarydate)=" + ddlMonth.SelectedValue ;

        Session["sesssionSqlEmploiespayslip"] = sql + whereclause + " order by  p.FirstName + ' ' + p.MiddleName + ' '+ p.FamilyName ";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql + whereclause + " order by  p.FirstName + ' ' + p.MiddleName + ' '+ p.FamilyName ");

        if (isExport)
        {
            ExporttoExcel(dt);
        }
        else
        {
            RptLeaveSearch.DataSource = dt;
            RptLeaveSearch.DataBind();
            EmpCount.Text = RptLeaveSearch.Items.Count.ToString();

            int TotActiveEmployees = TotalActiveEmployee();
            lblActiveEmployees.Text = TotActiveEmployees.ToString();
            if (TotActiveEmployees != RptLeaveSearch.Items.Count || RptLeaveSearch.Items.Count==0)
            {
                btnPublishPayslipPopup.Visible = false;
            }
            else
            {
                btnPublishPayslipPopup.Visible = true;
            }
                
        }
    }
    

    protected void btn_Download_Click(object sender, EventArgs e)
    {
        BindGrid(true);
    }

    protected void btn_Show_Click(object sender, EventArgs e)
    {
        BindGrid();
    }


    protected void btnPrint_OnClick(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        HiddenField hfEmpID = (HiddenField)btn.Parent.FindControl("hfEmpID");

        int EmpID = Common.CastAsInt32(hfEmpID.Value);
        if (ddlOffice.SelectedIndex == 0)
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

    // Publis --------------------------
    protected void btnPublishPayslipPopup_OnClick(object sender, EventArgs e)
    {
        lblPublishReportPopupText.Text = ddlOffice.SelectedItem.Text +" "+ ddlMonth.SelectedItem.Text +"-"+ddlYear.SelectedValue;
        dvPuplishPaySlip.Visible = true;
    }

    protected void btnClosePublishPayslipPopup_OnClick(object sender, EventArgs e)
    {
        dvPuplishPaySlip.Visible = false;

    }
    protected void btnPublishReport_OnClick(object sender, EventArgs e)
    {
        //HR_IU_PublishPaySlip
        string Period = "15-" + ddlMonth.SelectedItem.Text + "-" + ddlYear.SelectedValue;
        string sql = " Exec HR_IU_PublishPaySlip " + ddlOffice.SelectedValue+ ",'"+ Period + "', '"+Session["UserName"].ToString()+"' ";
        DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        lblMsgPublish.Text = " Published successfully.";
    }
    protected void btnPrintEmploiesSalary_OnClick(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "fdf", "window.open('PrintEmployeePayslip.aspx')", true);
    }

    public int TotalActiveEmployee()
    {
        int ret = 0;
        string sql = " select  COUNT(1)cnt from Hr_PersonalDetails P where  office="+ddlOffice.SelectedValue+"  and p.DRC is null ";
        DataTable Dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (Dt.Rows.Count > 0)
            ret = Common.CastAsInt32(Dt.Rows[0][0]);
        return ret;
    }
}