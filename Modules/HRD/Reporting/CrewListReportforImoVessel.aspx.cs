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

public partial class Reporting_CrewListReport : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    public int flag
    {
        get { return Common.CastAsInt32(ViewState["flag"]); }
        set { ViewState["flag"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        lblMessage.Text = "";
        this.ddl_Vessel.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btnsearch.ClientID + "').focus();}");
        //this.txtfromdate.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btnsearch.ClientID + "').focus();}");
        //this.txttodate.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btnsearch.ClientID + "').focus();}");

        //==========
        flag = Convert.ToInt32(Request.QueryString["flag"]);
        
        if (Page.IsPostBack == false)
        {
            bindvesselnameddl();
            if (flag == 2 && ! string.IsNullOrWhiteSpace(Request.QueryString["VesselId"]))
            {
                ddl_Vessel.SelectedValue = Request.QueryString["VesselId"].ToString() ;
            }
            //this.txtfromdate.Text = System.DateTime.Today.Date.ToString("MM/dd/yyyy");
            //this.txttodate.Text = System.DateTime.Today.Date.ToString("MM/dd/yyyy");
        }
        if (Page.IsPostBack)
        {
           showreport();
        }
    }
    public void bindvesselnameddl()
    {
        this.ddl_Vessel.DataTextField = "VesselName";
        this.ddl_Vessel.DataValueField = "VesselId";
        this.ddl_Vessel.DataSource = cls_SearchReliever.getMasterData("Vessel", "VesselId", "VesselName", Convert.ToInt32(Session["loginid"].ToString()));
        this.ddl_Vessel.DataBind();
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        showreport();
    }
    private void showreport()
    {
        int vesselid = Convert.ToInt32(ddl_Vessel.SelectedValue);
        

       // Session["VesselId"] = ddl_Vessel.SelectedValue;  
        IFRAME1.Attributes.Add("src", "PrintCrewListCrystalforImoVessel.aspx?VesselId=" + vesselid + "&flag=" + flag);   
    }
}
