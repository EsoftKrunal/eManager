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

public partial class emtm_StaffAdmin_PrintEmployeePayslip : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        string sql= Session["sesssionSqlEmploiespayslip"].ToString();
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        
        CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;

        rpt.Load(Server.MapPath("EmploiesSalary.rpt"));
        rpt.SetDataSource(dt);
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
