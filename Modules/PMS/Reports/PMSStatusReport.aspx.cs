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

public partial class Reports_PMSStatusReport : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        //ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        if (!IsPostBack)
        {
            ddlFleet.DataSource = ECommon.Execute_Procedures_Select_ByQueryAdmin("SELECT * FROM DBO.FLEETMASTER");
            ddlFleet.DataTextField = "FleetName";
            ddlFleet.DataValueField = "FleetId";
            ddlFleet.DataBind();
            ddlFleet.Items.Insert(0, new ListItem("< All FLEET >", "0"));
        }
        ShowReport();
    }
    protected void ShowReport()
    {
        DataTable dtReport = ECommon.Execute_Procedures_Select_ByQuery("exec PMS_Status_Report " + ddlFleet.SelectedValue);
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("PMSStatusReport.rpt"));
        rpt.SetDataSource(dtReport);
        rpt.SetParameterValue("Header", "PMS Status Report");
        rpt.SetParameterValue("VesselName", ddlFleet.SelectedItem.Text );
    }

    protected void ShowReport_Click(object sender, EventArgs e)
    {
        ShowReport();
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
