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

public partial class Reporting_OpenPrintCV : System.Web.UI.Page
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
        //========== Code to check report printing authority
        if (Page.IsPostBack == false)
        {
            if (("" + Page.Request.QueryString["Qid"]) != "")
            {
                DataTable dt1=Budget.getTable("SELECT convert(int,DateOfBirth) FROM DBO.CrewPersonalDetails WHERE CrewId=" + Convert.ToInt32(Page.Request.QueryString["Qid"].ToString())).Tables[0];
                if (dt1 == null)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "a", "alert('Crew not exists.');window.close();", true);
                    return;
                }
                else if (dt1.Rows.Count <= 0)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "a", "alert('Crew not exists.');window.close();", true);
                    return;
                }
                else
                {
                    if (Common.CastAsInt32(dt1.Rows[0][0]) != Common.CastAsInt32(Request.QueryString["Qp"]))
                    {
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "a", "alert('Crew not exists.');window.close();", true);
                        return;
                    }
                }
                //--------------------
                DataTable dt = ReportPrintCV.selectCrewnumberCrewid(Convert.ToInt32(Page.Request.QueryString["Qid"].ToString()));
                checkcrewid(dt.Rows[0]["CrewNumber"].ToString());
            }
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
    private void checkcrewid(string CrewNum)
    {
        IFRAME1.Attributes.Add("src", "PrintCVContainer.aspx?cnumber=" + CrewNum);  
    }
}