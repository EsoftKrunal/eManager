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

public partial class Reporting_planTrainingYearly : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        this.txt_year.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + Button1.ClientID + "').focus();}");
        //***********Code to check page acessing Permission
        //int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 92);
        //if (chpageauth <= 0)
        //{
        //    Response.Redirect("DummyReport.aspx");

        //}
        ////*******************
        ////========== Code to check report printing authority
        //DataTable dtcheck = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()));
        //foreach (DataRow dr in dtcheck.Rows)
        //{
        //    if (Convert.ToInt32(dr[0].ToString()) > 0)
        //    {
        //        CrystalReportViewer1.HasPrintButton = false;
        //    }
        //}
        ////==========
        if (!Page.IsPostBack)
        {
        }
        else
        {
            Button1_Click(sender, e);
        }
    }
    //protected void Page_Unload(object sender, EventArgs e)
    //{
    //    rpt.Close();
    //    rpt.Dispose();
    //}
    protected void Button1_Click(object sender, EventArgs e)
    {

        IFRAME1.Attributes.Add("src", "PlanTrainingYearly_Crystal.aspx?Year=" + txt_year.Text);
        //int year = 2000;
        //if (txt_year.Text != "")
        //{
        //    year = Convert.ToInt32(txt_year.Text);
        //}
        //DataTable dt = PlannedTrainingYearly.selectPlannedTrainingData(year);
        //DataTable dt2 = PrintCrewList.selectCompanyDetails();
        //CrystalReportViewer1.ReportSource = rpt;
        //rpt.Load(Server.MapPath("CrystalReportPlannedTraining.rpt"));
        //rpt.SetDataSource(dt);

        //foreach (DataRow dr in dt2.Rows)
        //{
        //    rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
        //    //rpt.SetParameterValue("@Address", dr["Address"].ToString());
        //    //rpt.SetParameterValue("@TelePhone", dr["TelephoneNumber"].ToString());
        //    //rpt.SetParameterValue("@Fax", dr["Faxnumber"].ToString());
        //    //rpt.SetParameterValue("@RegistrationNo", dr["RegistrationNo"].ToString());
        //    //rpt.SetParameterValue("@Email", dr["Email1"].ToString());
        //    //rpt.SetParameterValue("@Website", dr["Website"].ToString());
        //}
    }
}
