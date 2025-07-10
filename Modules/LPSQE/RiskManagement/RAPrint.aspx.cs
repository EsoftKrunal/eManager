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

public partial class Reports_RCAPrint : System.Web.UI.Page
{
    public int RiskId
    {
        get { return Common.CastAsInt32(ViewState["RiskId"]); }
        set { ViewState["RiskId"] = value; }
    }

    public string CurrentVessel
    {
        get { return ViewState["CurrentVessel"].ToString(); }
        set { ViewState["CurrentVessel"] = value; }
    }

    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            RiskId = Common.CastAsInt32(Page.Request.QueryString["RId"]);
            CurrentVessel = Page.Request.QueryString["VSL"].ToString();

        }
        catch { }

        Show_Report();
    }
    private void Show_Report()
    {
        try
        {
            DataTable dtRiskMaster = Common.Execute_Procedures_Select_ByQuery("Select * from dbo.VW_RISKDATA Where RISKID=" + RiskId + " AND VESSELCODE='" + CurrentVessel + "' ");
            dtRiskMaster.TableName = "EV_VSL_RiskMaster";

            //------------------------------
            this.CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;
                    
            rpt.Load(Server.MapPath("RAReport.rpt"));
            rpt.SetDataSource(dtRiskMaster);
        }
        catch (Exception ex)
        {

        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}