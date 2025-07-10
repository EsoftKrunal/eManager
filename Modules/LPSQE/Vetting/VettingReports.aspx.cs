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
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;
using System.IO;

public partial class Vetting_VettingReports : System.Web.UI.Page
{
    public AuthenticationManager Auth;
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //--------------------------------- PAGE ACCESS AUTHORITY ----------------------------------------------------
        Auth = new AuthenticationManager(305, Convert.ToInt32(Session["loginid"].ToString()), ObjectType.Page);
        if (! Auth.IsView)
            Response.Redirect("blank.aspx");
        //------------------------------------------------------------------------------------------------------------
        if (!Page.IsPostBack)
        {
            
        }
    }
    protected void btn_VSR_Click(object sender, EventArgs e)
    {
        btn_OR.Style.Remove("background-color");
        btn_VSR.Style.Add("background-color", "Orange");
        btn_VP.Style.Remove("background-color");
        frmFile.Attributes.Add("src", "OperatorReportingSummary_Report.aspx"); 
    }
    protected void btn_OR_Click(object sender, EventArgs e)
    {
        btn_OR.Style.Add("background-color", "Orange");
        btn_VSR.Style.Remove("background-color");
        btn_VP.Style.Remove("background-color");
        frmFile.Attributes.Add("src", "ObservationReporting_Report.aspx");
    }
    protected void btn_VP_Click(object sender, EventArgs e)
    {
        btn_OR.Style.Remove("background-color");
        btn_VSR.Style.Remove("background-color");
        btn_VP.Style.Add("background-color", "Orange"); 
        frmFile.Attributes.Add("src", "Vetting_Performance.aspx");
    }
}
