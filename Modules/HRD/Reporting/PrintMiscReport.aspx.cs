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

public partial class Reporting_PrintMiscReport : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 25);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");

        }
        //*******************
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton= Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()),25);
        //==========
        this.lblmessage.Text = "";
        show_report(); 
    }
    private void show_report()
    {
        DataSet ds = new DataSet();
        DataTable dt11 = PrintPO.selectMiscHeader(Convert.ToInt32(Session["MiscId_PrintPO"].ToString()));
        DataTable dt22 = PrintPO.selectMiscDetails(Convert.ToInt32(Session["MiscId_PrintPO"].ToString()), Convert.ToInt32(Session["VendorId_Print"].ToString()), Convert.ToInt32(Session["VesselId_Print"].ToString()), Convert.ToDateTime(Session["PoDate_Print"].ToString()));
        DataTable dt3 = PrintPO.getCrewDetails(Convert.ToInt32(Session["MiscId_PrintPO"].ToString()));
        DataTable dt21 = PrintCrewList.selectCompanyDetails();
        if (dt11.Rows.Count > 0)
        {
            this.CrystalReportViewer1.Visible = true;
            rpt.Load(Server.MapPath("PrintPO.rpt"));
            dt11.TableName = "Print_Po_Header;1";
            dt22.TableName = "Print_Po_Details;1";
            ds.Tables.Add(dt11);
            ds.Tables.Add(dt22);
            rpt.SetDataSource(ds);
            CrystalReportViewer1.ReportSource = rpt;

            CrystalDecisions.CrystalReports.Engine.ReportDocument rptsub3 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            rptsub3 = rpt.OpenSubreport("CrewDetails_PO.rpt");
            rptsub3.SetDataSource(dt3);

            rpt.SetParameterValue("@Header","");
           
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
