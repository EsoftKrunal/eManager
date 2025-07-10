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

public partial class Reports_OperatorReporting_Crystal : System.Web.UI.Page
{
    string strInspId = "";
    int intOwnerId;
    string strFromDt = "";
    string strToDt = "";
    string strOwner = "";
    string strVessel = "";
    string strFrom = "";
    string strTo = "";
    string strVslId = "";
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        try
        {
            strInspId = Page.Request.QueryString["InspID"].ToString();
            intOwnerId = Convert.ToInt32(Page.Request.QueryString["OwnerID"].ToString());
            strVslId = Page.Request.QueryString["VesselID"].ToString();
            strFromDt = Page.Request.QueryString["FromDate"].ToString();
            strToDt = Page.Request.QueryString["ToDate"].ToString();
            strOwner = Page.Request.QueryString["OwnerName"].ToString();
            strVessel = Page.Request.QueryString["VesselName"].ToString();
            strFrom = Page.Request.QueryString["FromText"].ToString();
            strTo = Page.Request.QueryString["ToText"].ToString();
        }
        catch { }
        Show_Report();
    }
    private void Show_Report()
    {
        DataTable dt = Operator_Reporting.SelectOperatorReportingDetails(strInspId, intOwnerId, strVslId, strFromDt, strToDt);
        if (dt.Rows.Count > 0)
        {
            this.CrystalReportViewer1.Visible = true;

            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("RPT_OperatorReporting.rpt"));
            rpt.SetDataSource(dt);

            rpt.SetParameterValue("@Header", "Inspection Status - " + strOwner);
            rpt.SetParameterValue("@Header1", "As On : " + System.DateTime.Now.Date.ToString("dd-MMM-yyyy"));
        }
        else
        {
            lblmessage.Text = "No Record Found.";
            this.CrystalReportViewer1.Visible = false;
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
