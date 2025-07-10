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


public partial class Reports_OfficeVisitReport : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    string VisitId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        VisitId = Common.CastAsInt32(Request.QueryString["VisitId"]).ToString();

        DataTable dt1 = Budget.getTable("select Convert(Int,CreatedOn) as IntCreatedOn from ov_visitmaster where Id=" + VisitId.ToString()).Tables[0];
        if (dt1 == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "a", "alert('Report not exists.');window.close();", true);
            return;
        }
        else if (dt1.Rows.Count <= 0)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "a", "alert('Report not exists.');window.close();", true);
            return;
        }
        else
        {
            if (Common.CastAsInt32(dt1.Rows[0][0]) != Common.CastAsInt32(Request.QueryString["Qp"]))
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "a", "alert('Report not exists.');window.close();", true);
                return;
            }
        }

        DataTable dtMaster = Budget.getTable("EXEC dbo.OfficeVisitReport " + VisitId).Tables[0];
        dtMaster.TableName = "OfficeVisitReport;1";

        DataTable dtManningGuidance = Budget.getTable("EXEC dbo.OfficeVisitReport1 " + VisitId + ",3").Tables[0];
        dtManningGuidance.TableName = "OfficeVisitReport1;1";
        DataTable dtManningFollowUp = Budget.getTable("EXEC dbo.OfficeVisitReport2 " + VisitId + ",3").Tables[0];
        dtManningFollowUp.TableName = "OfficeVisitReport2;1";

        DataTable dtFleetGuidance = Budget.getTable("EXEC dbo.OfficeVisitReport1 " + VisitId + ",1").Tables[0];
        dtFleetGuidance.TableName = "OfficeVisitReport1;1";
        DataTable dtFleetFollowUp = Budget.getTable("EXEC dbo.OfficeVisitReport2 " + VisitId + ",1").Tables[0];
        dtFleetFollowUp.TableName = "OfficeVisitReport2;1";

        DataTable dtHSQEGuidance = Budget.getTable("EXEC dbo.OfficeVisitReport1 " + VisitId + ",2").Tables[0];
        dtHSQEGuidance.TableName = "OfficeVisitReport1;1";
        DataTable dtHSQEFollowUp = Budget.getTable("EXEC dbo.OfficeVisitReport2 " + VisitId + ",2").Tables[0];
        dtHSQEFollowUp.TableName = "OfficeVisitReport2;1";

        DataTable dtMTMGuidance = Budget.getTable("EXEC dbo.OfficeVisitReport1 " + VisitId + ",4").Tables[0];
        dtMTMGuidance.TableName = "OfficeVisitReport1;1";
        DataTable dtMTMFollowUp = Budget.getTable("EXEC dbo.OfficeVisitReport2 " + VisitId + ",4").Tables[0];
        dtMTMFollowUp.TableName = "OfficeVisitReport2;1";
        
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("OfficeVisitReport.rpt"));

        rpt.SetDataSource(dtMaster);
        rpt.Subreports["OfficeVisitReportGuidance.rpt"].SetDataSource(dtManningGuidance);
        rpt.Subreports["OfficeVisitFollowUp.rpt"].SetDataSource(dtManningFollowUp);

        rpt.Subreports["OfficeVisitReportGuidance.rpt - 01"].SetDataSource(dtFleetGuidance);
        rpt.Subreports["OfficeVisitFollowUp.rpt - 01"].SetDataSource(dtFleetFollowUp);

        rpt.Subreports["OfficeVisitReportGuidance.rpt - 02"].SetDataSource(dtHSQEGuidance);
        rpt.Subreports["OfficeVisitFollowUp.rpt - 02"].SetDataSource(dtHSQEFollowUp);

        rpt.Subreports["OfficeVisitReportGuidance.rpt - 03"].SetDataSource(dtMTMGuidance);
        rpt.Subreports["OfficeVisitFollowUp.rpt - 03"].SetDataSource(dtMTMFollowUp);
    }
}
