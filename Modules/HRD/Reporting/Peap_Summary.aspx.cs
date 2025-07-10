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

public partial class Peap_Summary : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 99);
        CrystalReportViewer2.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 99);
        //==========
      
        ShowData();
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    public void ShowData()
    {
        DataSet ds = new DataSet();
        this.CrystalReportViewer1.Visible = true;
        int Year=Common.CastAsInt32(Request.QueryString["Year"]);
        int OFC=Common.CastAsInt32(Request.QueryString["OFC"]);
        int PL=Common.CastAsInt32(Request.QueryString["PL"]);
            
        CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("Peap_Summary.rpt"));
        string sql = "SELECT * FROM DBO.vw_PEAP_SUMMARY ";
        string whereclause = "WHERE PEAPYEAR=" + Year;
        if (OFC > 0)
            whereclause += " AND OFFICE=" + OFC;
        if (PL > 0)
            whereclause += " AND PEAPCATEGORY=" + PL;
        

        rpt.SetDataSource(Common.Execute_Procedures_Select_ByQuery(sql + whereclause));

    }
}
