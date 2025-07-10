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

public partial class Reporting_PrintCrewListCrystal : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()),15);
        //==========
        showdata();
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    private void showdata()
    {
        int vesselid;
        string Fd, Td,fields;

            vesselid = Convert.ToInt32(Request.QueryString["VesselId"]);
            Fd = Convert.ToString(Request.QueryString["FromDate"]);
            Td = Convert.ToString(Request.QueryString["ToDate"]);
            fields = "";
        
        DataTable dt1;
        DataTable dt2;

        DataTable dt = PrintCrewList.selectCompanyDetails();
        
        dt1 = PrintCrewList.selectCrewListDetails(vesselid, Fd, Td, fields);
       
        dt2 = PrintCrewList.getreurningCrew(vesselid, Fd, Td);
       
        if (dt1.Rows.Count > 0)
        {
            this.CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("PrintCrewList.rpt"));
            rpt.SetDataSource(dt1);
            rpt.SetParameterValue(0, Convert.ToInt32(Session["VesselId"].ToString()));
            foreach (DataRow dr in dt.Rows)
            {
                rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
                //rpt.SetParameterValue("@Address", dr["Address"].ToString());
                //rpt.SetParameterValue("@TelePhone", dr["TelephoneNumber"].ToString());
                //rpt.SetParameterValue("@Fax", dr["Faxnumber"].ToString());
                //rpt.SetParameterValue("@RegistrationNo", dr["RegistrationNo"].ToString());
                //rpt.SetParameterValue("@Email", dr["Email1"].ToString());
                //rpt.SetParameterValue("@Website", dr["Website"].ToString());
            }
            if(Fd=="" && Td=="")
            {
                rpt.SetParameterValue("@Header","Crew List as On : " + Convert.ToString(DateTime.Now.Date.ToString("dd-MMM-yyyy")));
            }
            else
            {
                rpt.SetParameterValue("@Header","Crew List From : " + Fd + " - " + Td);
            }
            foreach (DataRow dr in dt2.Rows)
            {
                rpt.SetParameterValue("@ReturningCrew", dr["ReturningCrew"].ToString());
                rpt.SetParameterValue("@FamiliarCrew", dr["FamiliarCrew"].ToString());
            }
        }
       
    }
}
