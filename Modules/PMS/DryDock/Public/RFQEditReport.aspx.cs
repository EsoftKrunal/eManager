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

public partial class Public_RFQEditReport : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        
        ShowReport();
    }
    protected void ShowReport()
    {
        string Type =  Request.QueryString["Type"].ToString();
        int RFQId = Common.CastAsInt32(Request.QueryString["RFQId"]);
        int Docketd = Common.CastAsInt32(Request.QueryString["DocketId"]);

        string WHERE = " WHERE RFQId=" + RFQId + " AND DocketId=" + Docketd + " " ;

        if (Type.Trim() == "YC")
        {
            WHERE = WHERE + " AND ISNULL(CostCategory, 'Y')= 'Y' ";
        }
        else
        {
            WHERE = WHERE + " AND ISNULL(CostCategory, 'Y')= 'N' ";
        }


        string strSQL = "SELECT * FROM vw_GetRFQEditReportData  " + WHERE + " Order By SubJobCode "; 

        DataTable dtReport = Common.Execute_Procedures_Select_ByQuery(strSQL);
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("RFQEditReport.rpt"));
        rpt.SetDataSource(dtReport);

        rpt.SetParameterValue("Title", ((Type.Trim() == "YC" ? " Yard Cost Report " : " Owner Cost Report " )));

    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}