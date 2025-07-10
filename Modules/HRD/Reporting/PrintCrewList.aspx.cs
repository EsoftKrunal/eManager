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

public partial class Reporting_PrintCrewList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        int vesselid = Convert.ToInt32(Request.QueryString["VesselId"]); ;
        string fd = Request.QueryString["FD"];
        string td = Request.QueryString["TD"];
        if(fd=="" & td=="")
        {
            Label2.Text="Crew List as On : " + Convert.ToString(DateTime.Now.Date.ToString("MM/dd/yyyy"));
        }
        else
        {
        Label2.Text = "Crew List From : " + fd + " - " + td;
        }
        //Label2.Text = Convert.ToString(DateTime.Now.Date.ToString("MM/dd/yyyy"));
        IFRAME1.Attributes.Add("src", "PrintCrewListCrystal.aspx?VesselId=" + vesselid + "&FromDate=" + fd + "&ToDate=" + td);   
    }
}
