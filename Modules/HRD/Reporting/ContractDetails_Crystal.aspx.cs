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

public partial class Reporting_ContractDetails_Crystal : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 97);
        //==========
        showreport();
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    public void showreport()
    {
        int userid;
        DateTime Fd, Td;
        if (Page.Request.QueryString.ToString() != "")
        {
            userid = Convert.ToInt32(Request.QueryString["UID"]);
            Fd = Convert.ToDateTime(Request.QueryString["FromDate"]);
            Td = Convert.ToDateTime(Request.QueryString["ToDate"]);
        }
        else
        {
            userid = 0;
            Fd = Convert.ToDateTime(DateTime.Today.ToShortDateString());
            Td = Convert.ToDateTime(DateTime.Today.ToShortDateString());
        }
        this.CrystalReportViewer1.Visible = true;
       
        DataTable dt1 = ContractDetails.selectContractDetails(userid, Fd, Td);
        DataTable dt2 = PrintCrewList.selectCompanyDetails();
        if (dt1.Rows.Count > 0)
        {
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("ContractDetails.rpt"));
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
}
