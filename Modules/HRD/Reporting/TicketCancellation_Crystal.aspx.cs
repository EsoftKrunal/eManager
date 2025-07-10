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

public partial class Reporting_TicketCancellation_Crystal : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        this.lblmessage.Text = "";
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 133);
        //==========
        show_report();
    }
    private void show_report()
    {
        int vesselid;
        char rdlstvalue;
        vesselid = Convert.ToInt32(Request.QueryString["VID"]);
        rdlstvalue = Convert.ToChar(Request.QueryString["RdLst"]);

        DataTable dt1 = Ticket_Cancellation.selectticketcancellationdetails(vesselid,rdlstvalue);

        if (dt1.Rows.Count > 0)
        {
            this.CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("TicketCancellation.rpt"));

            rpt.SetDataSource(dt1);

            DataTable dt = PrintCrewList.selectCompanyDetails();
            foreach (DataRow dr in dt.Rows)
            {
                rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
            }
        }
        else
        {
            this.lblmessage.Text = "No Records Found.";
            this.CrystalReportViewer1.Visible = false;
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
