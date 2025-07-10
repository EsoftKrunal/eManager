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

public partial class Reporting_VesselLineUpReportCrystal : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 134); 
        //==========
        showreport();
    }
    private void showreport()
    {
        try
        {
            lblMessage.Text = "";
            string Month1, Monthid, vesselname, ownername;
            int budgetyear, vesselid, ownerid;

            decimal budget;
            budget = 0;

            vesselid = Convert.ToInt16(Page.Request.QueryString["VID"].ToString());
            ownerid = Convert.ToInt16(Page.Request.QueryString["OID"].ToString());
            Month1 = System.DateTime.Today.Date.ToString("dd/MMM/yyyy").Substring(3, 3);
            vesselname = Page.Request.QueryString["VName"];
            ownername = Page.Request.QueryString["OName"];

            DataTable dt = VesselLineUp.selectVesselLineUpdata(ownerid, vesselid);

            if (dt.Rows.Count > 0)
            {
                this.CrystalReportViewer1.Visible = true;
                CrystalReportViewer1.ReportSource = rpt;
                rpt.Load(Server.MapPath("VesselLineUpReport.rpt"));
                rpt.SetDataSource(dt);
                //rpt.SetParameterValue("@MonthName", Month1);

                rpt.SetParameterValue("@Header", "Vessel Line Up Report for Owner : " + ownername + " and Vessel : " + vesselname);

                DataTable dt3 = PrintCrewList.selectCompanyDetails();
                foreach (DataRow dr in dt3.Rows)
                {
                    rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
                }
            }
            else
            {
                lblMessage.Text = "No Record Found.";
            }
        }
        catch (SystemException es)
        {

        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
