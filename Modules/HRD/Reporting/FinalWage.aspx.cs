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

public partial class Reporting_FinalWage : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 129);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");

        }
        //*******************
        this.lblMessage.Text = "";
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        DataTable dt22 = ReportPrintCV.selectCrewIdCrewNumber(txtempno.Text);
        if (dt22.Rows.Count == 0)
        {
            this.lblMessage.Text = "Invalid Emp#.";
            IFRAME1.Attributes.Add("src", "");  
            return;
        }
        string crewid;
        crewid = dt22.Rows[0][0].ToString();
        DataTable dt11 = MonthleWageToCrew.selectCrewStatus(crewid);
        if (dt11.Rows[0][0].ToString() != "2")
        {
            this.lblMessage.Text = "Employee Not Signed Off Yet";
            IFRAME1.Attributes.Add("src", "");  
            return;
        }
        IFRAME1.Attributes.Add("src", "FinalWageContainer.aspx?crewid=" + crewid);  
    }
}