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

public partial class PortCallReport2 : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 116); 
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
            int Month, Year, VesselId, VendorId;
            string header, Status, Month1, Year1, VesselId1, VendorId1, Status1;
            Month = Convert.ToInt32(Request.QueryString["Month"]);
            Year = Convert.ToInt32(Request.QueryString["Year"]);
            VesselId = Convert.ToInt32(Request.QueryString["VesselId"]);
            Status = Request.QueryString["PoStatus"];
            VendorId = Convert.ToInt32(Request.QueryString["Vendor"]);

            Month1 = Request.QueryString["Month1"];
            Year1 = Request.QueryString["Year1"];
            VesselId1 = Request.QueryString["Vessel1"];
            Status1 = Request.QueryString["PoStatus1"];
            VendorId1 = Request.QueryString["Vendor1"];

            //header = Session["ReportPortCall1_Header"].ToString(); 
            header = "Monthly port call's PO details for the month of " + Month1 + " - " + Year1 + ", Vessel : " + VesselId1 + ", Port Call Status : " + Status1 + ", Vendor : " + VendorId1;
            DataSet ds= new DataSet();
            DataTable dt;
            this.CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("PortCallReport2.rpt"));
            dt = cls_PortCallReport2.Report_PortCall2(Month,Year,VesselId,Status,VendorId);
            rpt.SetDataSource(dt);
            DataTable dt1 = PrintCrewList.selectCompanyDetails();
            foreach (DataRow dr in dt1.Rows)
            {
                rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
            }
            rpt.SetParameterValue("@HeaderText", header);
    }
}
