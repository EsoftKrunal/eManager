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
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using ShipSoft.CrewManager.Operational;
using System.Collections.Specialized;

public partial class Reporting_PrintCV : System.Web.UI.Page
{
    int crewid = 0;
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (("" + Request.QueryString["Mode"]) != "In")
        {
            IFRAME1.Attributes["height"]="600px";
        }
        
        this.txt_Emp_number.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + Button1.ClientID + "').focus();}");
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 15);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");

        }
        //========== Code to check report printing authority
        if (Page.IsPostBack == false)
        {
            if (("" + Page.Request.QueryString["crewid"]) != "")
            {
                DataTable dt = ReportPrintCV.selectCrewnumberCrewid(Convert.ToInt32(Page.Request.QueryString["crewid"].ToString()));
                this.txt_Emp_number.Text = dt.Rows[0]["CrewNumber"].ToString();
            }
        }
        try
        {           
            if (txt_Emp_number.Text != "")
            {
                checkcrewid();
            }
        }
        catch
        {
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //checkcrewid();
        //if (crewid>0)
        //{
        //    IFRAME1.Attributes.Add("src", "PrintCVInner.aspx?CrewID=" + crewid);  
        //}
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    private void checkcrewid()
    {
        DataTable dt22 = ReportPrintCV.selectCrewIdCrewNumber(txt_Emp_number.Text.Trim());
        if (dt22.Rows.Count == 0 && txt_Emp_number.Text.Trim()  != "")
        {
            Label1.Visible = true;
            Label1.Text = "Invalid Emp#.";
            IFRAME1.Attributes.Remove("src");
            return;
        }
        else
        {
            Label1.Visible = false;
            Label1.Text = "";
            foreach (DataRow dr in dt22.Rows)
            {
                crewid = Convert.ToInt32(dr["CrewId"].ToString());
            }
            IFRAME1.Attributes.Add("src", "PrintCVContainer.aspx?cnumber=" + txt_Emp_number.Text);  
        }
    }
}