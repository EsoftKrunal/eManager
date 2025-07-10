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

public partial class CrewingFeeContainer : System.Web.UI.Page
{
    string ownerid = "0";
    string fdt="", tdt="";
    string mgtfee="0", mstfee = "0"; 
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        ownerid = "0" + Request.QueryString["ownerid"];
        fdt = Request.QueryString["fdt"];
        tdt = Request.QueryString["tdt"];
        mgtfee = "0" + Request.QueryString["mgtfee"];
        mstfee = "0" + Request.QueryString["mstfee"];
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 205);
        //==========
        this.CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("CrewingFee.rpt"));
        DataTable dt = Budget.getTable("exec dbo.CrewingFeeReport " + ownerid + ",'" + fdt + "','" + tdt + "'," + mgtfee + "," + mstfee + "").Tables[0];
        rpt.SetDataSource(dt);
        DataTable dt3 = PrintCrewList.selectCompanyDetails();
        foreach (DataRow dr in dt3.Rows)
        {
            rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
            rpt.SetParameterValue("@Email", fdt + " - " + tdt );
            rpt.SetParameterValue("@HeaderText", "Crewing Fee / Mustering Fee / Training Fee");
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
