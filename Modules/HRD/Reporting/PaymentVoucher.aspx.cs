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

public partial class Reporting_PaymentVoucher : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
         this.lblmessage.Text = "";
         if (Page.IsPostBack == false)
         {
             //***********Code to check page acessing Permission
             int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 28);
             if (chpageauth <= 0)
             {
                 Response.Redirect("DummyReport.aspx");
             }
             //*******************
             //========== Code to check report printing authority
            CrystalReportViewer1.HasPrintButton=Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()),28);
         }
         try
         {
             DataTable dt1 = PaymentVoucher.selectPaymentVoucherdetails(Convert.ToInt32(Session["PrintVoucherId"].ToString()));
             DataTable dt = PrintCrewList.selectCompanyDetails();
             //DataTable dt1 = ((DataTable)Session["rptsource2"]);
             if (dt1.Rows.Count > 0)
             {
                 this.CrystalReportViewer1.Visible = true;
                 CrystalReportViewer1.ReportSource = rpt;

                 rpt.Load(Server.MapPath("PaymentVoucher.rpt"));
                 rpt.SetDataSource(dt1);

                 foreach (DataRow dr in dt.Rows)
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
         catch
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
}
