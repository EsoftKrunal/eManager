using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class emtm_StaffAdmin_Emtm_Payslip_Crystal : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        string[] parm;
        if (Session["sPS"] != null)
        {
            parm = Session["sPS"].ToString().Split('~');
        }
        else
        {
            return;
        }
        string Office = "";
        if (parm[3] == "1")Office = "MTM SHIPMANAGEMENT PVT LTD";
        if (parm[3] == "3")Office = "MTM SHIPMANAGEMENT PTE LTD";
        

        DataTable dt = new DataTable();
        DataTable dt_sub_0 = new DataTable();
        DataTable dt_sub_1 = new DataTable();
        string sql = " select * from vw_HR_payslip_rpt where EmpID = " + parm[0]+ " and MONTH(salarydate) =  " + parm[1] + "  and year(salarydate) =  " + parm[2] +
                     " and Income_Ded='I' " +
                     " order by Income_Ded desc ";
        dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);



        CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;

        rpt.Load(Server.MapPath("PaySlip.rpt"));
        rpt.SetDataSource(dt);


        sql = " select * from vw_HR_payslip_rpt where EmpID = " + parm[0] + " and MONTH(salarydate) =  " + parm[1] + "  and year(salarydate) =  " + parm[2] +" and Income_Ded='D' ";
        dt_sub_1 = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rpt.Subreports[1].SetDataSource(dt_sub_1);

        sql = " select * from vw_HR_payslip_rpt where EmpID = " + parm[0] + " and MONTH(salarydate) =  " + parm[1] + "  and year(salarydate) =  " + parm[2] + " and Income_Ded='C' ";
        dt_sub_0 = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        rpt.Subreports[0].SetDataSource(dt_sub_0);


        rpt.SetParameterValue("Office", Office);
        
        rpt.SetParameterValue("SalDate", "Payslip for "+MonthName(Common.CastAsInt32( parm[1]))+" "+ parm[2]);
    }

    private string MonthName(int m)
    {
        string res;
        switch (m)
        {
            case 1:
                res = "Jan";
                break;
            case 2:
                res = "Feb";
                break;
            case 3:
                res = "Mar";
                break;
            case 4:
                res = "Apr";
                break;
            case 5:
                res = "May";
                break;
            case 6:
                res = "Jun";
                break;
            case 7:
                res = "Jul";
                break;
            case 8:
                res = "Aug";
                break;
            case 9:
                res = "Sep";
                break;
            case 10:
                res = "Oct";
                break;
            case 11:
                res = "Nov";
                break;
            case 12:
                res = "Dec";
                break;
            default:
                res = "";
                break;
        }
        return res;
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }

}
