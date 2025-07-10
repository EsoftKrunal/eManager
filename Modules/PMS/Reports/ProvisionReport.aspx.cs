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

public partial class Reports_ProvisionReport : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        int ProvisionId = Common.CastAsInt32(Request.QueryString["PrId"].ToString());
        ShowProvisionReport(ProvisionId);
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    protected void ShowProvisionReport(int ProvisionId)
    {
        string SQLMaster = "SELECT * FROM [dbo].[VW_MP_VSL_ProvisionMaster] WHERE [VesselCode]='" + Session["CurrentShip"].ToString().Trim() + "' AND [ProvisionId]=" + ProvisionId;
        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(SQLMaster);
        SQLMaster = "SELECT * FROM [dbo].[VW_MP_VSL_ProvisionDetails] WHERE [VesselCode]='" + Session["CurrentShip"].ToString().Trim() + "' AND [ProvisionId]=" + ProvisionId;
        DataTable dt2 = Common.Execute_Procedures_Select_ByQuery(SQLMaster);
        DataSet ds = new DataSet();
        dt1.TableName = "VW_MP_VSL_ProvisionMaster";
        dt2.TableName = "VW_MP_VSL_ProvisionDetails";
        ds.Tables.Add(dt1.Copy());
        ds.Tables.Add(dt2.Copy());

        string Sqlo = "SELECT * FROM [dbo].[VW_MP_VSL_ProvisionOtherItems] WHERE [VesselCode]='" + Session["CurrentShip"].ToString().Trim() + "' AND [ProvisionId]=" + ProvisionId;
        DataTable dto = Common.Execute_Procedures_Select_ByQuery(Sqlo);

        string Sqln = "SELECT * FROM [dbo].[VW_MP_VSL_ProvisionNationalityWise] WHERE [VesselCode]='" + Session["CurrentShip"].ToString().Trim() + "' AND [ProvisionId]=" + ProvisionId;
        DataTable dtn = Common.Execute_Procedures_Select_ByQuery(Sqln);
        
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("ProvisionReport.rpt"));
        rpt.SetDataSource(ds);
        rpt.Subreports["Provision_OtherItemsReport.rpt"].SetDataSource(dto);
        rpt.Subreports["Provision_NationalityWiseSubReport.rpt"].SetDataSource(dtn);
        
        rpt.Refresh();

    }
}