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

public partial class Reporting_Vessel_Details_Report : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 36);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");
        }
        //*******************
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()),36);
        //==========
        this.lblmessage.Text = "";
        show_report(); 
    }
    private void show_report()
    {
        int Vesselid = Convert.ToInt32(Request.QueryString["VID"]);
        DataSet ds = new DataSet();
        DataTable dt11 = VesselModuleReport.selectVesselHeader(Vesselid);
        DataTable dt22 = VesselModuleReport.selectVesselDetails(Vesselid);
        DataTable dt21 = PrintCrewList.selectCompanyDetails();
        if (dt11.Rows.Count > 0)
        {
            this.CrystalReportViewer1.Visible = true;
            rpt.Load(Server.MapPath("VesselDetailsReport.rpt"));
            dt11.TableName = "Report_Vessel_OverviewParticulars;1";
            dt22.TableName = "Report_Vessel_OverviewParticulars_WageScales;1";
            ds.Tables.Add(dt11);
            ds.Tables.Add(dt22);
            rpt.SetDataSource(ds);
            CrystalReportViewer1.ReportSource = rpt;

            foreach (DataRow dr in dt21.Rows)
            {
                rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
            }

        }
        else
        {
            this.CrystalReportViewer1.Visible = false;
            lblmessage.Text = "No Record Found.";
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    protected void btn_show_Click(object sender, EventArgs e)
    {
       
    }
}
