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

public partial class Reporting_TrainingRecordSummary : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        this.txt_from.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + Button1.ClientID + "').focus();}");
        this.txt_to.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + Button1.ClientID + "').focus();}");
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 112);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");

        }
        //*******************
       
  
       
    }
   
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (this.txt_from.Text == "")
        {
            txt_from.Text = System.DateTime.Today.Date.ToString("dd-MMM-yyyy");
        }
        if (this.txt_to.Text == "")
        {
            txt_to.Text = System.DateTime.Today.Date.ToString("dd-MMM-yyyy");
        }
        iframe1.Attributes.Add("src", "TrainingReportSummary_Header.aspx?fromdate=" + txt_from.Text + "&todate=" + txt_to.Text); 
    }
  
}
