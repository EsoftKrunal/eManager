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

public partial class Reporting_PIMedicalCaseReport : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        this.txt_from.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + Button1.ClientID + "').focus();}");
        this.txt_to.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + Button1.ClientID + "').focus();}");
        this.RadioButtonList1.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + Button1.ClientID + "').focus();}");
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 96);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string i = RadioButtonList1.SelectedValue;
        IFRAME1.Attributes.Add("src", "PICaseReport_Crystal.aspx?FromD=" + txt_from.Text + "&ToD=" + txt_to.Text + "&RDL=" + i);
    }
}
