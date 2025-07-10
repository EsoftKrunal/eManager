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
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using ShipSoft.CrewManager.Operational;

public partial class Reporting_CrewTrainingRContainer : System.Web.UI.Page
{
    int crewid = 0;
    int selindex = 0;
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        string CrNo, CrewName, nat, train, curvess, fdt, tdt, promo, rtype, rankid, status;
        CrNo=Request.QueryString["CrNo"];
        CrewName = Request.QueryString["CrewName"];
        nat = Request.QueryString["nat"];
        train = Request.QueryString["train"];
        curvess = Request.QueryString["curvess"];
        fdt = Request.QueryString["fdt"];
        tdt = Request.QueryString["tdt"];
        promo = Request.QueryString["promo"];
        rtype = Request.QueryString["rtype"];
        rankid = Request.QueryString["Rank"];
        status = Request.QueryString["status"]; 
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 83); 
        //==========
        DataTable dt1 = Common.Execute_Procedures_Select_ByQueryCMS("exec [dbo].[Report_TrainingDetails] '" + CrNo + "','" + CrewName + "'," + nat + ",'" + train + "'," + curvess + ",'" + fdt + "','" + tdt + "'," + promo + "," + rtype+","+Common.CastAsInt32(rankid)+"," + status);
        DataTable dt = PrintCrewList.selectCompanyDetails();
        if (dt1.Rows.Count > 0)
        {
            this.CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("CrewTrainingReport.rpt"));

            rpt.SetDataSource(dt1);
            foreach (DataRow dr in dt.Rows)
            {
                rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
            }
        }
        else
        {
            this.CrystalReportViewer1.Visible = false;
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
