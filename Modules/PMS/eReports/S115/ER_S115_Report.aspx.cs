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

public partial class Reports_ER_S115_Report : System.Web.UI.Page
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
        if(Page.Request.QueryString["VesselCode"]!=null)
            VesselCode = Convert.ToString( Page.Request.QueryString["VesselCode"]);
        else
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
        string SQLReport = "SELECT * FROM [dbo].[vw_ER_S115_PrintReport] WHERE [VesselCode]='" + VesselCode + "' AND [ReportId]=" + ReportId;
        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(SQLReport);
        string SQLInjuryToPersonnel = "SELECT * FROM [dbo].[vw_ER_S115_PrintReport_InjuryToPerson] WHERE [VesselCode]='" + VesselCode + "' AND [ReportId]=" + ReportId;
        DataTable dt2 = Common.Execute_Procedures_Select_ByQuery(SQLInjuryToPersonnel);
        DataSet ds = new DataSet();
        dt1.TableName = "vw_ER_S115_PrintReport" ;
        dt2.TableName = "vw_ER_S115_PrintReport_InjuryToPerson";
        ds.Tables.Add(dt1.Copy());
        ds.Tables.Add(dt2.Copy());

        string MTC = "";
        string LTI = "";
        string TRC = "";

        string SQLParam = "SELECT MTC, LTI, (MTC + LTI + RWC) AS TRC FROM (SELECT " +
                     "(SELECT Count([OCI_MI_Reporting]) FROM [DBO].[ER_S115_InjuryToPerson] WHERE [VesselCode] = '" + VesselCode + "' AND ISNULL(Status, 'A') = 'A' AND [ReportId] = " + ReportId.ToString() + " AND [OCI_MI_Reporting] IN (24,26,28)) AS MTC, " +
                     "(SELECT Count([OCI_MI_Reporting]) FROM [DBO].[ER_S115_InjuryToPerson] WHERE [VesselCode] = '" + VesselCode + "' AND ISNULL(Status, 'A') = 'A' AND [ReportId] = " + ReportId.ToString() + " AND [OCI_MI_Reporting] IN (25,27,29,26)) AS LTI, " +
                     "(SELECT Count([OCI_MI_Reporting]) FROM [DBO].[ER_S115_InjuryToPerson] WHERE [VesselCode] = '" + VesselCode + "' AND ISNULL(Status, 'A') = 'A' AND [ReportId] = " + ReportId.ToString() + " AND [OCI_MI_Reporting] IN (28)) AS RWC " +
                     ")a ";
        DataTable dtParam = Common.Execute_Procedures_Select_ByQuery(SQLParam);

        if (dtParam.Rows.Count > 0)
        {
            MTC = dtParam.Rows[0]["MTC"].ToString();
            LTI = dtParam.Rows[0]["LTI"].ToString();
            TRC = dtParam.Rows[0]["TRC"].ToString();
        }

        //CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("ER_S115_Report.rpt"));
        rpt.SetDataSource(ds);

        rpt.SetParameterValue("MTC", MTC);
        rpt.SetParameterValue("LTI", LTI);
        rpt.SetParameterValue("TRC", TRC);

        rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("S115.pdf"));
        //Response.Clear();
        Response.WriteFile(Server.MapPath("S115.pdf"));
        //Response.End();
    }
}