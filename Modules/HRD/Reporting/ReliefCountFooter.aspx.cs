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

public partial class Reporting_ReliefCountFooter : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 15);
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
        int vesselId,rankid;
        string fd, td;
        vesselId = Convert.ToInt32(Request.QueryString["VID"]);
        rankid = Convert.ToInt32(Request.QueryString["RID"]);
        fd = Request.QueryString["FD"];
        td = Request.QueryString["TD"];
        DataTable dt = ReliefCountclas.selectReliefCountData(vesselId,rankid,fd,td);
        DataTable dt2 = PrintCrewList.selectCompanyDetails();
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("ReliefCountReport.rpt"));
        rpt.SetDataSource(dt);
        if (fd.Trim()!="01-Jan-1900") 
            td = fd + " : " + td;
        rpt.SetParameterValue("@FDate","");
        rpt.SetParameterValue("@TDate", td.ToString());
        foreach (DataRow dr in dt2.Rows)
        {
            rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
        }
    }
}
