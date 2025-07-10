using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public partial class Scm_Report : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    public int FleetId
    {
        get { return Convert.ToInt32(ViewState["FleetId"]);}
        set {ViewState["FleetId"]=value;}
    }
    public int VesselId
    {
        get { return Convert.ToInt32(ViewState["VesselId"]); }
        set { ViewState["VesselId"] = value; }
    }

    //public int YEAR
    //{
    //    get { return Convert.ToInt32(ViewState["YEAR"]); }
    //    set { ViewState["YEAR"] = value; }
    //}

    //public int Month
    //{
    //    get { return Convert.ToInt32(ViewState["Month"]); }
    //    set { ViewState["Month"] = value; }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (!IsPostBack)
        {
            ProjectCommon.LoadMonth(ddlMonth);
            ProjectCommon.LoadYear(ddlYear);
            ddlMonth.Items.Insert(0, new ListItem("< All >", ""));
        }
        try
        {
            FleetId = Common.CastAsInt32(Request.QueryString["FleetId"]);
            VesselId = Common.CastAsInt32(Request.QueryString["VesselId"]);
            ddlYear.SelectedValue = Request.QueryString["YEAR"];
        }
        catch { }
        Show_Report();
    }
    protected void Month_Year_Changed(object sender, EventArgs e)
    {
        
    }
    private void Show_Report()
    {
        string SQL = "SELECT * FROM DBO.vw_SCM_MasterReview WHERE YEAR(SDATE)=" + ddlYear.SelectedValue + ((ddlMonth.SelectedIndex>0)?" and month(sdate)=" + ddlMonth.SelectedValue:"");
        if (VesselId > 0)
            SQL += " AND VesselId=" + VesselId.ToString();
        if (FleetId > 0)
            SQL += " AND FleetId=" + FleetId.ToString();

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
        
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("sCM_REPORT.rpt"));
        rpt.SetDataSource(dt);
        
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
