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

public partial class Drill_Print : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
       Show_Report();
    }
    private void Show_Report()
    { 
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("Select * from dbo.vw_DrillTrainings " + Session["PrintWhere"].ToString());
            
        if (dt.Rows.Count > 0)
        {
                
            this.CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("Print.rpt"));
            rpt.SetDataSource(dt);

            DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("select SHIPNAME from dbo.Settings WHERE ShipCode='" + Session["CurrentShip"].ToString() + "'");
            rpt.SetParameterValue("VesselName", dt1.Rows[0]["shipname"].ToString());
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}