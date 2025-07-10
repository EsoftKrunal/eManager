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


public partial class Reports_VettingPlannerReport : System.Web.UI.Page
{
    public string FleetText
    {
        set { ViewState["FleetText"] = value; }
        get { return Convert.ToString( ViewState["FleetText"]); }
    }
    public string VesselList
    {
        set { ViewState["VesselList"] = value; }
        get { return ViewState["VesselList"].ToString(); }
    }
    public string InspectorList
    {
        set { ViewState["InspectorList"] = value; }
        get {return ViewState["InspectorList"].ToString(); }
    }
    public int Days
    {
        set { ViewState["Days"] = value; }
        get { return int.Parse("0" + ViewState["Days"]); }
    }
    public int PDays
    {
        set { ViewState["PDays"] = value; }
        get { return int.Parse("0" + ViewState["PDays"]); }
    }
    public bool PlannedOnly
    {
        set { ViewState["PlannedOnly"] = value; }
        get { return Convert.ToBoolean(ViewState["PlannedOnly"]); }
    }
    public bool All
    {
        set { ViewState["All"] = value; }
        get { return Convert.ToBoolean(ViewState["All"]); }
    }

    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        VesselList=Session["c1_Vessels"].ToString();
        InspectorList = Session["c1_Inspectors"].ToString();
        All = Convert.ToBoolean(Session["All"]);

        PlannedOnly = Convert.ToBoolean(Session["c1_PlannedOnly"]);

        if (Page.Request.QueryString["Days"] != null)
            Days = Common.CastAsInt32(Page.Request.QueryString["Days"]);
        if (Page.Request.QueryString["Plan"] != null)
            PDays = Common.CastAsInt32(Page.Request.QueryString["Plan"]);

        if (Page.Request.QueryString["FleetName"] != null)
            FleetText = Page.Request.QueryString["FleetName"].ToString();
        
        BindVettingPlanner();

        
    }
    public void BindVettingPlanner()
    {
        try
        {
            DataTable dtVettingPlanner = Budget.getTable("exec dbo.VettingPlannerReport '" + VesselList + "'," + Common.CastAsInt32(Days).ToString() + "," + Common.CastAsInt32(PDays).ToString()).Tables[0];
            if (dtVettingPlanner != null)
            {
                string Filter = " 1=1";
                
                if (All)
                {
                    Filter = Filter + " AND ( ( REMOTEID in (" + InspectorList + ") " + ((PlannedOnly) ? "" : "OR REMOTEID<=0 OR REMOTEID is null") + " ) OR ( ATTENDID in (" + InspectorList + ") " + ((PlannedOnly) ? "" : "OR ATTENDID<=0 OR ATTENDID is null") + " ) ) ";
                }
                else
                {
                    Filter = Filter + " AND ( ( REMOTEID in (" + InspectorList + ")) OR ( ATTENDID in (" + InspectorList + ") ) )";
                }

                //if (chk_PlannedOnly.Checked)
                //{
                //    Filter = Filter + " AND ISNULL(NEXTINSPID,0) > 0 ";
                //}

                DataView DtFiltered = dtVettingPlanner.DefaultView;
                DtFiltered.RowFilter = Filter;
                DataTable dtRes = DtFiltered.ToTable();
                if (dtRes.Rows.Count > 0)
                {
                    this.CrystalReportViewer1.Visible = true;                     
                    CrystalReportViewer1.ReportSource = rpt;
                    rpt.Load(Server.MapPath("VettingPlannerReport.rpt"));

                    rpt.SetDataSource(dtRes);
                    rpt.SetParameterValue("FleetName", (FleetText == "< ALL >") ? " ALL " : FleetText);
                    rpt.SetParameterValue("Days", Days);
                    rpt.SetParameterValue("PDays", PDays);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
