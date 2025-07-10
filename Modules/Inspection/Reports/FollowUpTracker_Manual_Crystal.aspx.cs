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

public partial class FollowUpTracker_Manual_Crystal : System.Web.UI.Page
{
    string Inspid = "";
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        this.lblmessage.Text = "";
        try
        {
            Inspid = Page.Request.QueryString["Inspid"].ToString();
        }
        catch { }
        Show_Report();
    }
    private void Show_Report()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT INSPDUE_ID AS INSPDUEID,TABLEID AS OBSVID,V.VESSELNAME,DEFICIENCY,INSPECTIONNO AS SOURCE,TCLDATE AS TGCLOSEDT, " +
                                                                    "REPLACE(CONVERT(VARCHAR,TCLDATE,106),' ','-') AS TARGETCLOSEDATE,RESPONSIBILITY,CLOSEDON AS COMPLETIONDATE,CLOSURE AS CLOSED,V.VESSELCODE AS AA,TABLEID AS BB,'N' AS CRITICAL, " +
                                                                    "RESPVESSEL=CASE WHEN CHARINDEX('Vessel',RESPONSIBILITY,0) > 0 then 'X' ELSE '' END , " +
                                                                    "RESPOFFICE=CASE WHEN CHARINDEX('Office',RESPONSIBILITY,0) > 0 then 'X' ELSE '' END  " +
                                                                    "FROM T_OBSERVATIONSNEW OB INNER JOIN T_INSPECTIONDUE I ON I.ID=OB.INSPDUE_ID INNER JOIN DBO.VESSEL V ON V.VESSELID=I.VESSELID " +
                                                                    "WHERE INSPDUE_ID=" + Inspid);
        if (dt.Rows.Count > 0)
        {
            this.CrystalReportViewer1.Visible = true;

            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("RPT_FR_FollowUpById.rpt"));

            rpt.SetDataSource(dt);
            rpt.SetParameterValue("@Header", dt.Rows[0]["VesselName"].ToString() + " - FollowUp List");
            rpt.SetParameterValue("@Header1", "Printed On : " + System.DateTime.Now.Date.ToString("dd-MMM-yyyy"));
            rpt.SetParameterValue("@TotalRecordCount", "Total Record : " + dt.Rows.Count.ToString());
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
