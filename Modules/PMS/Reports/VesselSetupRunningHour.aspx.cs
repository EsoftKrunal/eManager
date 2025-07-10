using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Print_VesselSetupRunningHour : System.Web.UI.Page
{
    #region Declarations
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------

        string VesselCode = "";
        if (Page.Request.QueryString["VesselCode"] != null)
            VesselCode = Page.Request.QueryString["VesselCode"].ToString();

        string strRunningHourSQL = "SELECT ROW_NUMBER() OVER(ORDER BY CM.ComponentCode) AS SrNo,CM.ComponentId ,CM.ComponentCode,CM.ComponentName,ISNULL(VRM.StartupHour,'') AS StartupHour ,ISNULL(VRM.AvgRunningHrPerDay,'') AS AvgRunningHrPerDay,REPLACE( CASE CONVERT(varchar(15),ISNULL(VRM.StartDate,''),106) WHEN '01 Jan 1900' THEN '' ELSE CONVERT(varchar(15),ISNULL(VRM.StartDate,''),106) END ,' ','-')  AS StartDate " +
                                "FROM ComponentMaster CM " +
                                "LEFT JOIN  VesselRunningHourMaster VRM ON VRM.ComponentId = CM.ComponentId  AND VRM.VesselCode = '" + VesselCode + "' " +
                                "WHERE CM.ComponentId IN (SELECT DISTINCT ComponentId FROM VesselComponentJobMaster WHERE IntervalId = 1 AND VesselCode = '" + VesselCode + "')";
        DataTable dtRunningHour = Common.Execute_Procedures_Select_ByQuery(strRunningHourSQL);
        CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("~/Reports/VesselSetupRunningHour.rpt")); 
        rpt.SetDataSource(dtRunningHour);
        rpt.SetParameterValue("@Header", "Vessel Setup [ " + VesselCode + " ] - Running Hour");
     
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    
}
