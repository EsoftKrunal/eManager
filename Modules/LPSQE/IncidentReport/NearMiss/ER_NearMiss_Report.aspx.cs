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
using System.IO;

public partial class eReports_S133_ER_S133_Report : System.Web.UI.Page
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
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        ReportId = Common.CastAsInt32(Request.QueryString["RId"].ToString());
        VesselCode = Request.QueryString["VC"].ToString().Trim();
        ShowReport(ReportId);
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    protected void ShowReport(int ReportId)
    {
        string SQLReport = "SELECT * FROM [dbo].[vw_ER_S133_PrintReport] WHERE [VesselCode]='" + VesselCode + "' and VesselCode in (Select v.VesselCode from UserVesselRelation vw with(nolock) inner join Vessel v on vw.VesselId = v.VesselId where vw.Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ") AND [ReportId]=" + ReportId;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQLReport);        
        dt.TableName = "vw_ER_S133_PrintReport";        
        
        rpt.Load(Server.MapPath("ER_S133_Report.rpt"));
        rpt.SetDataSource(dt);

        rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~/Modules/LPSQE/IncidentReport/NearMiss/NMReport.pdf"));
        //Response.Clear();
        //Response.WriteFile(Server.MapPath("NMReport.pdf"));
        //Response.End();
        byte[] fileBytes = File.ReadAllBytes(Server.MapPath("~/Modules/LPSQE/IncidentReport/NearMiss/NMReport.pdf"));
        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.AddHeader("Content-Disposition", "attachment; filename=NMReport.pdf");
        Response.AddHeader("Content-Length", fileBytes.Length.ToString());
        Response.BinaryWrite(fileBytes);
        Response.End();
    }
}