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

public partial class OperatorReportingSummary_Crystal : System.Web.UI.Page
{
    string strVslId = "";
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        try
        {
            strVslId = Page.Request.QueryString["VesselID"].ToString();
        }
        catch { }
        Show_Report();
    }
    private void Show_Report()
    {
        string Mode = "Summary";
        if (Request.QueryString["Mode"] != null)
            Mode = Request.QueryString["Mode"].ToString();
        if (Mode == "Summary")
        {
            DataTable dt = Budget.getTable("EXEC DBO.PR_RPT_OperatorReporting_Summary '" + strVslId + "'").Tables[0];
            if (dt.Rows.Count > 0)
            {
                this.CrystalReportViewer1.Visible = true;

                CrystalReportViewer1.ReportSource = rpt;
                rpt.Load(Server.MapPath("RPT_OperatorReportingSummary.rpt"));
                rpt.SetDataSource(dt);
                rpt.SetParameterValue("@Header", "Vetting Status Report");
            }
            else
            {
                lblmessage.Text = "No Record Found.";
                this.CrystalReportViewer1.Visible = false;
            }
        }
        else
        {
            DataTable dt_Vessels = Budget.getTable("SELECT * FROM DBO.VESSEL WHERE VESSELID IN (" + strVslId + ")").Tables[0];
            DataTable dt = Budget.getTable("EXEC DBO.VETTINGSTATUSREPORT_DETAILS '" + strVslId + "'").Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = "STATUS='DONE'";
                DataTable dt_Done = dv.ToTable();
                dv.RowFilter = "STATUS='PLAN'";
                DataTable dt_Planned = dv.ToTable();
                dv.RowFilter = "STATUS='DONE-P'";
                DataTable dt_Done_P = dv.ToTable();

                this.CrystalReportViewer1.Visible = true;
                
                CrystalReportViewer1.ReportSource = rpt;
                rpt.Load(Server.MapPath("RPT_OperatorReportingDetails.rpt"));
                rpt.SetDataSource(dt_Vessels);

                rpt.Subreports[0].SetDataSource(dt_Planned);
                rpt.SetParameterValue("@Header", "Vetting Status Report");

                rpt.Subreports[1].SetDataSource(dt_Done);
                rpt.SetParameterValue("@Header", "Vetting Status Report");

                rpt.Subreports[2].SetDataSource(dt_Done_P);
                rpt.SetParameterValue("@Header", "Vetting Status Report");

                rpt.Subreports[3].SetDataSource(dt);
                rpt.SetParameterValue("@Header", "Vetting Status Report");
            }
            else
            {
                lblmessage.Text = "No Record Found.";
                this.CrystalReportViewer1.Visible = false;
            }
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
