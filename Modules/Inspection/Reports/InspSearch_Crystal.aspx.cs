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

public partial class Reports_InspSearch_Crystal : System.Web.UI.Page
{
    string strStatusId;
    string strFromDt = "";
    string strToDt = "";
    string strInsId = "";
    int intOwnId, intVesselId, intloginid, intport, intcrewid;
    string strDueDt = "", strinspname = "", strchap = "", strinspnum = "";
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        this.lblmessage.Text = "";
        try
        {
            strStatusId = Page.Request.QueryString["SID"].ToString();
            strFromDt = Page.Request.QueryString["FROMDT"].ToString();
            strToDt = Page.Request.QueryString["TODT"].ToString();
            strInsId = Page.Request.QueryString["INID"].ToString();
            intOwnId = Convert.ToInt32(Page.Request.QueryString["OWNID"].ToString());
            intVesselId = Convert.ToInt32(Page.Request.QueryString["VSLID"].ToString());
            strDueDt = Page.Request.QueryString["DUEDT"].ToString();

            intloginid = Convert.ToInt32(Page.Request.QueryString["LGNID"].ToString());
            intport = Convert.ToInt32(Page.Request.QueryString["PRTNAME"].ToString());
            strinspname = Page.Request.QueryString["INSPNAME"].ToString();
            strchap = Page.Request.QueryString["CHAPNAME"].ToString();
            strinspnum = Page.Request.QueryString["INPNUM"].ToString();
            intcrewid = Convert.ToInt32(Page.Request.QueryString["CREWID"].ToString());
        }
        catch { }
        Show_Report();
    }
    private void Show_Report()
    {
        DataTable dt=null;
        try
        {
            dt = (DataTable)Session["SearchPrint"];
        }
        catch { }
        
        if (dt == null)
        { 
            dt = SearchReport.SelectSearchDetails(strStatusId, strFromDt, strToDt, strInsId, intOwnId, intVesselId, strDueDt, intloginid, intport, strinspname, strchap, strinspnum, intcrewid); 
        }
        if (dt.Rows.Count > 0)
        {
            this.CrystalReportViewer1.Visible = true;

            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("RPT_Search.rpt"));

            rpt.SetDataSource(dt);

            //DataTable dt1 = ResponseReport.SelectCompanyDetails();
            //foreach (DataRow dr in dt1.Rows)
            //{
            //    rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
            //}
            rpt.SetParameterValue("@Header", "Search Report");
            if (strStatusId == "")
                rpt.SetParameterValue("@Status_Name", "All");
            else
                rpt.SetParameterValue("@Status_Name", strStatusId);
            rpt.SetParameterValue("@FrmDate",strFromDt);
            rpt.SetParameterValue("@TDate", strToDt);
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
