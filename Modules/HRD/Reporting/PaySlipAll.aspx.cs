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

public partial class PaySlip : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()),33);
        int Month = Common.CastAsInt32(Request.QueryString["PayMonth"]);
        int Year = Common.CastAsInt32(Request.QueryString["PayYear"]);
        int VesselId = Common.CastAsInt32(Request.QueryString["VesselId"]);
        string Type = Request.QueryString["Type"];
        //==========
        lblMessage.Visible = true;
        lblMessage.Text = "";
        CrystalReportViewer1.Visible = false;
        //==========
        DataTable dt_Data = Common.Execute_Procedures_Select_ByQueryCMS("select * from vw_crewportagebill where vesselid=" + VesselId.ToString() + " and paymonth=" + Month + " and payyear=" + Year);
        if (dt_Data.Rows.Count > 0)
        {
            CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;
            if(Type =="S")
                rpt.Load(Server.MapPath("PaySlipAll.rpt"));
            else
                rpt.Load(Server.MapPath("PaySlipAllList.rpt"));

            rpt.SetDataSource(dt_Data);
        }
        else
        {
            lblMessage.Text = "No Record Found";
            CrystalReportViewer1.Visible = false;
        }
    }
}
