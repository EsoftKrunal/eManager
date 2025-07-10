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

public partial class Reporting_CrewsExperienceReport : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //***********Code to check page acessing Permission
        int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 15);
        if (chpageauth <= 0)
        {
            Response.Redirect("DummyReport.aspx");
        }
        //==========
        if (!Page.IsPostBack)
        {
            Session["PageCodeEXP"] = "1";
        }
        else
        {
            Session.Remove("PageCodeEXP");
            Button1_Click(sender, e);
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    public void bindCrewNameDDL()
    {
        DataTable dt = CrewExperienceReport.selectCrewNameDetails();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        int crewid=0;
        DataTable dt22 = ReportPrintCV.selectCrewIdCrewNumber(txt_Emp_number.Text.Trim());
         if (dt22.Rows.Count == 0 && txt_Emp_number.Text!="")
         {
             Label1.Visible = true;
             Label1.Text="Invalid Emp#.";
             return;
         }
         else
         {
             Label1.Visible = false;
             Label1.Text="";
            
             foreach (DataRow dr in dt22.Rows)
             {
                 crewid = Convert.ToInt32(dr["CrewId"].ToString());
             }
             IFRAME1.Attributes.Add("src", "CrewExperienceContainer.aspx?selindex=" + CheckBoxList1.SelectedIndex + "&crewid=" + crewid.ToString() );  
         }
    }
}   