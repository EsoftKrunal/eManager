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

public partial class Emtm_Performance : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    int VesselId;
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 119);
        //==========
        VesselId = 0;
        ShowData();
        try
        {
            VesselId = Convert.ToInt32(Request.QueryString["VesselId"]);  
        }
        catch { }
        if (!IsPostBack)
        {
            ddlFleet.DataSource = Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM DBO.FLEETMASTER ORDER BY FLEETNAME");
            ddlFleet.DataTextField = "FleetName";
            ddlFleet.DataValueField = "FleetId";
            ddlFleet.DataBind();
            ddlFleet.Items.Insert(0,new ListItem(" All Fleet ", "0"));
            ShowData();
        }
    }
    protected void ddlFleet_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ShowData();
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    public void ShowData()
    {
            string header;
            DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("EXEC DBO.RPT_KPI_PEROFORMANCE " + Common.CastAsInt32(ddlFleet.SelectedValue) + ",2015" );
            this.CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("Emtm_Performance.rpt"));
            rpt.SetDataSource(dt);
            rpt.SetParameterValue("@HeaderText", " Compliance Report - 2015 ");
    }
}
