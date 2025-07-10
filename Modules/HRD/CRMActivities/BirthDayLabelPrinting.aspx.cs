using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class CRMActivities_BirthDayLabelPrinting : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        string SQL = "SELECT * FROM  vw_CrewBirthDayPrintingLabels WHERE CrewId IN (" + Request.QueryString["Ids"].ToString() + ")";
        DataTable dt1 = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("BirthDayLabelPrinting.rpt"));
        rpt.SetDataSource(dt1);
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}