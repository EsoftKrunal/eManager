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

public partial class ShipImportedDate : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()),33);
        //==========
        lblMessage.Visible = true;
        lblMessage.Text = "";

        int vesselid = Common.CastAsInt32(Request.QueryString["Vess"]);
        int month = Common.CastAsInt32(Request.QueryString["Month"]);
        int year = Common.CastAsInt32(Request.QueryString["Year"]);

        string sql = "select * from vw_PortageBillImported VW where VW .Month="+month+" and VW .year="+year+" and VW .VesselID="+vesselid+"";
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        if (dt.Rows.Count > 0)
        {
            CrystalReportViewer1.Visible = true;

            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("ShipImportedData.rpt"));
            rpt.SetDataSource(dt);        
        }
        else
        {
            lblMessage.Text = "No Record Found";
            CrystalReportViewer1.Visible = false;
        }
    }
}
