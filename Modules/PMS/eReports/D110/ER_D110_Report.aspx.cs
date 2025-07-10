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

public partial class eReports_ER_D110_Report : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    public int ReportId
    {
        get { return Common.CastAsInt32(ViewState["ReportId"]); }
        set { ViewState["ReportId"] = value; }
    }

    public string VesselCode
    {
        get { return ViewState["VesselCode"].ToString(); }
        set { ViewState["VesselCode"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ReportId = Common.CastAsInt32(Request.QueryString["RId"].ToString());
        VesselCode = Session["CurrentShip"].ToString().Trim();
        ShowReport(ReportId);
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    protected void ShowReport(int ReportId)
    {
        string SQLReport = "SELECT * FROM [dbo].[vw_ER_D110_Report] WHERE [VesselCode]='" + VesselCode + "' AND [ReportId]=" + ReportId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQLReport);        
        dt.TableName = "vw_ER_D110_Report";        
        
        rpt.Load(Server.MapPath("ER_D110_Report.rpt"));
        rpt.SetDataSource(dt);        

        rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("D110.pdf"));
        Response.Clear();
        Response.WriteFile(Server.MapPath("D110.pdf"));
        Response.ContentType = "application/pdf";
        Response.End();
    }
}