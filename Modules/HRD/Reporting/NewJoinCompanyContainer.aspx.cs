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

public partial class Reporting_NewJoinCompanyContainer : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //*******************
        string fdt=Request.QueryString["fdt"], tdt=Request.QueryString["tdt"];
        int selindex =int.Parse (Request.QueryString["CS"]);
        string or = "" + Request.QueryString["or"];

        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 15);
        //-------------
        CrystalReportViewer1.Visible = true;
        DataTable dt1 = OfficersJoinedFirstTimeWithCompany.selectOfficersJoinedFirstTimeWithCompany(selindex, fdt, tdt,or);
        if (dt1.Rows.Count > 0)
        {
            this.CrystalReportViewer1.Visible = true;
            DataTable dt2 = PrintCrewList.selectCompanyDetails();
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("OfficersJoinedFirstTimeWithCompany.rpt"));
            Session.Add("rptsource5", dt1);
            rpt.SetDataSource(dt1);


            foreach (DataRow dr in dt2.Rows)
            {
                rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
                //rpt.SetParameterValue("@Address", dr["Address"].ToString());
                //rpt.SetParameterValue("@TelePhone", dr["TelephoneNumber"].ToString());
                //rpt.SetParameterValue("@Fax", dr["Faxnumber"].ToString());
                //rpt.SetParameterValue("@RegistrationNo", dr["RegistrationNo"].ToString());
                //rpt.SetParameterValue("@Email", dr["Email1"].ToString());
                //rpt.SetParameterValue("@Website", dr["Website"].ToString());
            }
        }
        else
        {
            this.CrystalReportViewer1.Visible = false;
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
