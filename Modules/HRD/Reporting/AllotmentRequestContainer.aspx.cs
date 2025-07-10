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

public partial class Reporting_AllotmentRequestContainer : System.Web.UI.Page
{
    int crewid = 0;
    int selindex = 0;
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 123);
        //==========
        //DataTable dtcheck = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()));
        DataTable dt10 = AllotmentRequestClas.selectAllotmentDetails(Convert.ToInt16(Request.QueryString["vessel"]), Convert.ToInt16(Request.QueryString["month"]), Convert.ToInt16(Request.QueryString["year"]));
        DataTable dt1 = PrintCrewList.selectCompanyDetails();
        if (dt10.Rows.Count > 0)
        {
            CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("AllotmentRequest.rpt"));
            rpt.SetDataSource(dt10);
            foreach (DataRow dr in dt1.Rows)
            {
                rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
            }
            rpt.SetParameterValue("@Vessel", Request.QueryString["vesselname"]);
            rpt.SetParameterValue("@Month", Request.QueryString["monthname"]);
            rpt.SetParameterValue("@Year", Request.QueryString["yearname"]);
            DataTable dt4 = AllotmentRequestClas.selectUserDetails(Convert.ToInt32(Session["loginid"].ToString()));
            foreach (DataRow dr in dt4.Rows)
            {
                rpt.SetParameterValue("@UserName", dr["UserName"].ToString());
            }
            rpt.SetParameterValue("@Header", "Allotment Request for Month :-  " + Request.QueryString["monthname"] + " - " + Request.QueryString["yearname"]);
        }
        else
        {
            CrystalReportViewer1.Visible = false;
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
