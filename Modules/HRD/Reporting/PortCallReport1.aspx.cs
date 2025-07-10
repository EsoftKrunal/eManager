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

public partial class PortCallReport1 : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        this.lblMessage.Text = "";
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 115); 
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
            int VesselId;
            string header,CStatus,CFrom ,CTo;
            CFrom = Request.QueryString["CFrom"].ToString();
            CTo = Request.QueryString["CTo"].ToString();
            VesselId = Convert.ToInt32(Request.QueryString["VesselId"]);
            CStatus = Request.QueryString["CStatus"].ToString();
            header = Session["ReportPortCall1_Header"].ToString();
            DataSet ds= new DataSet();
            DataTable dt, dtsub, dt1;

            dt = cls_PortCallReport1.Report_PortCall1(DateTime.Parse(CFrom), DateTime.Parse(CTo), VesselId, CStatus);
            dtsub = cls_PortCallReport1.Report_PortCall1_CrewDetails(DateTime.Parse(CFrom), DateTime.Parse(CTo), VesselId, CStatus); 
            dt1 = PrintCrewList.selectCompanyDetails();

            if (dt.Rows.Count > 0)
            {
                this.CrystalReportViewer1.Visible = true;
                CrystalReportViewer1.ReportSource = rpt;
                rpt.Load(Server.MapPath("PortCallReport1.rpt"));
                rpt.SetDataSource(dt);

                CrystalDecisions.CrystalReports.Engine.ReportDocument rptsub = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                rptsub = rpt.OpenSubreport("PortCallReport1_CrewDetails.rpt");
                rptsub.SetDataSource(dtsub);

                foreach (DataRow dr in dt1.Rows)
                {
                    rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
                }
                rpt.SetParameterValue("@HeaderText", header);
            }
            else
            {
                this.CrystalReportViewer1.Visible = false;
                this.lblMessage.Text = "No Record Found.";
            }
    }
}
