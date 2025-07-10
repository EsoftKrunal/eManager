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

public partial class emtm_StaffAdmin_Emtm_HR_LeaveSummaryReport_Crystal : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        int OfficeId = Common.CastAsInt32(Request.QueryString["Office"]);
        int Year =Common.CastAsInt32(Request.QueryString["Year"]);
        int Month = Common.CastAsInt32(Request.QueryString["Month"]);
        string Type = Request.QueryString["Type"].ToString();

        DataTable dt = new DataTable();
        
        if(Type=="L")
            dt= Common.Execute_Procedures_Select_ByQueryCMS("EXEC dbo.HR_GetLeaveSummaryReport " + OfficeId.ToString() + "," + Year.ToString() + "," + Month.ToString());
        else
            dt = Common.Execute_Procedures_Select_ByQueryCMS("EXEC dbo.HR_GetLeaveSummaryReport " + OfficeId.ToString() + "," + Year.ToString() + "," + Month.ToString());

        string YearWorkingDays = "";
        if (OfficeId > 0)
        {
            DataTable dtYearDays = Common.Execute_Procedures_Select_ByQueryCMS("select [dbo].[HR_Get_NonWorkingDays_Period]('01-JAN-" + Year + "','31-DEC-" + Year + "'," + OfficeId + ")");
            YearWorkingDays = " , Total Working Days : " + dtYearDays.Rows[0][0].ToString();
        }
        
        CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;

        if (Type == "L")
            rpt.Load(Server.MapPath("LeaveReportSummary.rpt"));
        else
            rpt.Load(Server.MapPath("LeaveReportSummary1.rpt"));

        //foreach (DataRow dr in dt.Rows)
        //{
        //    for (int i = 3; i <= dt.Columns.Count - 1; i++)
        //    {
        //        if (Common.CastAsDecimal(dr[i]) == 0)
        //        {
        //            dr[i] = DBNull.Value;
        //        }
        //    }
        //}
        rpt.SetDataSource(dt);
        
        if(OfficeId>0)
            rpt.SetParameterValue("Header", "Office : " + Request.QueryString["OfficeName"] + " , Year : " + Year + " , Month : Jan - " + Request.QueryString["MonthName"] + YearWorkingDays);
        else
            rpt.SetParameterValue("Header", " Year : " + Year + " , Month : Jan - " + Request.QueryString["MonthName"] + YearWorkingDays);
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
