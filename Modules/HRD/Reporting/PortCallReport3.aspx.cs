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

public partial class PortCallReport3 : System.Web.UI.Page
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
            int Month, Year, VesselId;
            string header, Status, Status2, Month2, Year2, VesselId2;
            Month = Convert.ToInt32(Request.QueryString["Month"]);
            Year = Convert.ToInt32(Request.QueryString["Year"]);
            VesselId = Convert.ToInt32(Request.QueryString["VesselId"]);
            Status = Request.QueryString["PoStatus"];
            Month2 = Request.QueryString["Month2"];
            Year2 = Request.QueryString["Year2"];
            VesselId2 = Request.QueryString["Vessel2"];
            Status2 = Request.QueryString["PoStatus2"];

            //header = Session["ReportPortCall1_Header"].ToString();
            header = " Monthly port call's PO & Invoice details for the month of " + Month2 + " - " + Year2 + ", Vessel : " + VesselId2 + ", PoStatus : " + Status2;
            DataSet ds= new DataSet();
            DataTable dt;
            this.CrystalReportViewer1.Visible = true;
            //CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("PortCallReport3.rpt"));
            dt = cls_PortCallReport3.Report_PortCall3(Month,Year,VesselId,Status);
            rpt.SetDataSource(dt);
            DataTable dt1 = PrintCrewList.selectCompanyDetails();
            foreach (DataRow dr in dt1.Rows)
            {
                rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
                //rpt.SetParameterValue("@Address", dr["Address"].ToString());
                //rpt.SetParameterValue("@TelePhone", dr["TelephoneNumber"].ToString());
                //rpt.SetParameterValue("@Fax", dr["Faxnumber"].ToString());
                //rpt.SetParameterValue("@RegistrationNo", dr["RegistrationNo"].ToString());
                //rpt.SetParameterValue("@Email", dr["Email1"].ToString());
                //rpt.SetParameterValue("@Website", dr["Website"].ToString());
            }
            rpt.SetParameterValue("@HeaderText", header);
    }
}
