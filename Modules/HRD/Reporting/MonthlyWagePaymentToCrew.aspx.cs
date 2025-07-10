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

public partial class Reporting_MonthlyWagePaymentToCrew : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        this.txtempno.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btnsearch.ClientID + "').focus();}");
        this.ddyear.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btnsearch.ClientID + "').focus();}");
        this.ddmonth.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btnsearch.ClientID + "').focus();}");
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 101);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");

        }
        //*******************
        this.lblMessage.Text = "";
        if (Page.IsPostBack == false)
        {
            int yr;
            yr = (Convert.ToInt16(System.DateTime.Today.Year));
            for (int i = yr - 1; i <= yr + 1; i++)
            {
                this.ddyear.Items.Add(i.ToString());
               // 
            }
            ddyear.Items[1].Selected = true;
            ddmonth.SelectedValue = Convert.ToString(DateTime.Now.Month);
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        int crewid = 0;
        DataTable dt22 = ReportPrintCV.selectCrewIdCrewNumber(txtempno.Text);
        if (dt22.Rows.Count == 0)
        {
            this.lblMessage.Text = "Invalid Emp#.";
            return;
        }
        foreach (DataRow dr in dt22.Rows)
        {
            crewid = Convert.ToInt32(dr["CrewId"].ToString());
        }
        IFRAME1.Attributes.Add("src", "MonthlyWageContainer.aspx?crewnumber=" + txtempno.Text + "&crewid=" + crewid + "&month=" + ddmonth.SelectedValue + "&monthname=" + ddmonth.SelectedItem.Text + "&year=" + ddyear.SelectedValue);
    }
}
