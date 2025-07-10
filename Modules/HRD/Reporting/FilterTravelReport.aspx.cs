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

public partial class Reporting_FilterTravelReport : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
            DataTable dt = PrintCrewList.selectCompanyDetails();
            DataTable dt1 = (Budget.getTable("exec dbo.Filter_TicketCancellation " + Session["Qry"].ToString()).Tables[0]);
            if (dt1.Rows.Count > 0)
            {
                CrystalReportViewer1.ReportSource = rpt;
                rpt.Load(Server.MapPath("FilterTravelReport.rpt"));
                rpt.SetDataSource(dt1);

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
               
                //CrystalReportViewer1.ReportSource = rpt;
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
