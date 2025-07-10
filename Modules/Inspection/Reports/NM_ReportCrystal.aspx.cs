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

public partial class Reports_NM_ReportCrystal : System.Web.UI.Page
{
    string Period = "", strNMCat = "", strNMFromDate = "", strNMToDate = "", strNMVesselId = "";
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblmessage.Text = "";
        try
        {
            if (Page.Request.QueryString["NMCAT"].ToString() != "")
                strNMCat = Page.Request.QueryString["NMCAT"].ToString();
            if (Page.Request.QueryString["NMFROMDATE"].ToString() != "")
                strNMFromDate = Page.Request.QueryString["NMFROMDATE"].ToString();
            if (Page.Request.QueryString["NMTODATE"].ToString() != "")
                strNMToDate = Page.Request.QueryString["NMTODATE"].ToString();
            if (Page.Request.QueryString["NMVSLID"].ToString() != "")
                strNMVesselId = Page.Request.QueryString["NMVSLID"].ToString();
        }
        catch { }
        Period= strNMFromDate + " - "  + strNMToDate;
        if (Period == " - ") { Period = ""; }  
        if (strNMFromDate.Trim()=="") {strNMFromDate="01/01/1900";}
        if (strNMToDate.Trim() == "") { strNMToDate = "12/31/2078"; }
        Show_Report();
    }
    private void Show_Report()
    {
        try
        {
            DataTable dt1 = NMReport.SelectNMReportDetails(strNMCat,strNMFromDate, strNMToDate, strNMVesselId);
            if (dt1.Rows.Count > 0)
            {
                this.CrystalReportViewer1.Visible = true;

                CrystalReportViewer1.ReportSource = rpt;

                if (strNMCat == "1")
                    rpt.Load(Server.MapPath("RPT_NMReport.rpt"));
                else
                    rpt.Load(Server.MapPath("RPT_NMReport2.rpt"));

                rpt.SetDataSource(dt1);
                if (strNMCat == "1")
                    rpt.SetParameterValue("@Header", "Near Miss Count");
                else
                    rpt.SetParameterValue("@Header", "Near Miss Count by Root Cause");

                dt1 = NMReport.SelectNMReportDetails("3", strNMFromDate, strNMToDate, strNMVesselId);
                rpt.SetParameterValue("@NearMissCount",dt1.Rows[0][0].ToString()) ;
                rpt.SetParameterValue("@Period", Period);
            }
            else
            {
                lblmessage.Text = "No Record Found.";
                this.CrystalReportViewer1.Visible = false;
            }
        }
        catch { }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
