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

public partial class Reporting_OffsignersListContainer : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        string fdt = Request.QueryString["fdt"], tdt = Request.QueryString["tdt"], vessel = Request.QueryString["vessel"];
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 15);
        //==========
        if (fdt != "" && tdt != "")
        {
            DataTable dt = OfficerRejoin.selectMemberJoin(Convert.ToInt32(vessel ), Convert.ToDateTime(fdt), Convert.ToDateTime(tdt));
            if (dt.Rows.Count > 0)
            {
                this.CrystalReportViewer1.Visible = true;
                CrystalReportViewer1.ReportSource = rpt;
                rpt.Load(Server.MapPath("OfficersRejoinCompany.rpt"));

                Session.Add("rptsource", dt);
                rpt.SetDataSource(dt);

                DataTable dt1 = PrintCrewList.selectCompanyDetails();
                foreach (DataRow dr in dt1.Rows)
                {
                    rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
                }
                rpt.SetParameterValue("@Header", "OnSigner List By Period between " + fdt + " and " + tdt);
            }
            else
            {
                this.CrystalReportViewer1.Visible = false;
            }
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
