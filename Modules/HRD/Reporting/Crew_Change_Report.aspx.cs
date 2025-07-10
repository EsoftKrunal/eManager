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

public partial class Reporting_Crew_Change_Report : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton =Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()),15);
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
        int vesselid;
        String header;
        DateTime Fd, Td;
        if (Page.Request.QueryString.ToString() != "")
        {
            vesselid = Convert.ToInt32(Request.QueryString["VID"]);
            Fd = Convert.ToDateTime(Request.QueryString["FromDate"]);
            Td = Convert.ToDateTime(Request.QueryString["ToDate"]);
        }
        else
        {
            vesselid = 0;
            Fd = Convert.ToDateTime(DateTime.Today.ToShortDateString());
            Td = Convert.ToDateTime(DateTime.Today.ToShortDateString());
        }
        int Rdlst = Convert.ToInt16(Request.QueryString["RdLst"]);
        DataTable dt;
        this.CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;
        if (Rdlst == 0)
        {
            rpt.Load(Server.MapPath("CrewChangeReport_Total.rpt"));
            header = "Total Crew Change Between " + Fd.ToString("dd-MMM-yyyy") + " And " + Td.ToString("dd-MMM-yyyy");
        }
        else if (Rdlst == 1)
        {
            rpt.Load(Server.MapPath("CrewChangeReport_SignOff.rpt"));
            header = "Sign Off Between " + Fd.ToString("dd-MMM-yyyy") + " And " + Td.ToString("dd-MMM-yyyy");
        }
        else
        {
            rpt.Load(Server.MapPath("CrewChangeReport_SignOn.rpt"));
            header = "Sign On Between " + Fd.ToString("dd-MMM-yyyy") + " And " + Td.ToString("dd-MMM-yyyy");
        }

        DataTable dt1 = PrintCrewList.selectCompanyDetails();

        //DateTime d1, d2;
        //d1 = Convert.ToDateTime("01/01/2008");
        //d2 = Convert.ToDateTime("10/10/2008");

        if (Rdlst == 0)
        {
            dt = cls_ReportCrewChange.CrewChangeReport_Total(vesselid, Fd, Td);
        }
        else if (Rdlst == 1)
        {
            dt = cls_ReportCrewChange.CrewChangeReport_SignOff(vesselid, Fd, Td);
        }
        else
        {
            dt = cls_ReportCrewChange.CrewChangeReport_SignOn(vesselid, Fd, Td);
        }
        rpt.SetDataSource(dt);

        foreach (DataRow dr in dt1.Rows)
        {
            rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
            rpt.SetParameterValue("@Header", header);
            
            //rpt.SetParameterValue("@Address", dr["Address"].ToString());
            //rpt.SetParameterValue("@TelePhone", dr["TelephoneNumber"].ToString());
            //rpt.SetParameterValue("@Fax", dr["Faxnumber"].ToString());
            //rpt.SetParameterValue("@RegistrationNo", dr["RegistrationNo"].ToString());
            //rpt.SetParameterValue("@Email", dr["Email1"].ToString());
            //rpt.SetParameterValue("@Website", dr["Website"].ToString());
        }
    }
}
