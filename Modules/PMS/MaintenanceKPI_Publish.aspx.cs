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

public partial class Reports_MaintenanceKPI_Publish : System.Web.UI.Page
{
    
    public int Year_kpi
    {
        set { ViewState["_Year_kpi"] = value; }
        get{ return Common.CastAsInt32(ViewState["_Year_kpi"]); }
    }
    public int Month_kpi
    {
        set { ViewState["_Month_kpi"] = value; }
        get { return Common.CastAsInt32(ViewState["_Month_kpi"]); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        if (!Page.IsPostBack)
        {
            BindFleet();
            BindVessels();

            btnViewReport_Click(sender, e);
        }
    }

    private void BindVessels()
    {
        DataTable dtVessels = new DataTable();
        string strvessels = "";
        if (ddlFleet.SelectedIndex > 0)
            strvessels = "SELECT VesselId,VesselCode,VesselName FROM Vessel VM WHERE VesselCode in (Select VesselCode from dbo.Vessel where FleetId=" + ddlFleet.SelectedValue+") and vesselstatusid=1 and EXISTS(SELECT TOP 1 COMPONENTID FROM  VSL_ComponentMasterForVessel EVM WHERE VM.VesselCode = EVM.VesselCode ) ORDER BY VesselName ";
        else
            strvessels = "SELECT VesselId,VesselCode,VesselName FROM Vessel VM WHERE vesselstatusid=1 and EXISTS(SELECT TOP 1 COMPONENTID FROM  VSL_ComponentMasterForVessel EVM WHERE VM.VesselCode = EVM.VesselCode ) ORDER BY VesselName ";
        dtVessels = Common.Execute_Procedures_Select_ByQuery(strvessels);

        ddlVessels.DataSource = dtVessels;
        ddlVessels.DataTextField = "VesselName";
        ddlVessels.DataValueField = "VesselCode";
        ddlVessels.DataBind();
        ddlVessels.Items.Insert(0, new ListItem("< All >", ""));        

        //if (dtVessels.Rows.Count > 0)
        //{
        //    ddlVessels.DataSource = dtVessels;
        //    ddlVessels.DataTextField = "VesselName";
        //    ddlVessels.DataValueField = "VesselCode";
        //    ddlVessels.DataBind();
        //}
        //else
        //{
        //    ddlVessels.DataSource = null;
        //    ddlVessels.DataBind();            
        //}
        //ddlVessels.Items.Insert(0, new ListItem("< All >", ""));        
    }
    private void BindFleet()
    {
        DataTable dtFleet = new DataTable();
        string strvessels = " Select FleetId,FleetName from dbo.FleetMaster ";
        dtFleet = Common.Execute_Procedures_Select_ByQuery(strvessels);
        if (dtFleet.Rows.Count > 0)
        {
            ddlFleet.DataSource = dtFleet;
            ddlFleet.DataTextField = "FleetName";
            ddlFleet.DataValueField = "FleetId";
            ddlFleet.DataBind();
        }
        else
        {
            ddlFleet.DataSource = null;
            ddlFleet.DataBind();
        }
        ddlFleet.Items.Insert(0, new ListItem("< All >", "0"));
    }
    protected void ShowReport()
    {
	int dayscnt=5;

        lblpubmonyear.Text = DateTime.Today.AddMonths(-1).ToString("MMM-yyyy");
        lbllastmonth.Text = DateTime.Today.AddMonths(-1).ToString("MMM-yyyy");
        lblcurrmonth.Text = DateTime.Today.ToString("MMM-yyyy");
	if(DateTime.Today.Day > dayscnt)	
	{
		lblpubmonyear.Text = DateTime.Today.ToString("MMM-yyyy");
        	lbllastmonth.Text = DateTime.Today.ToString("MMM-yyyy");
        	lblcurrmonth.Text = DateTime.Today.AddMonths(1).ToString("MMM-yyyy");
	}

        Common.Set_Procedures("GET_MKPI_JOBS_DATA_1");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(
            new MyParameter("@shipid", ddlVessels.SelectedValue.Trim()),
            new MyParameter("@FLEETID", ddlFleet.SelectedValue.Trim()),
            new MyParameter("@DAYSCNT", dayscnt)            
            );
        DataSet dsKPI = new DataSet();
        dsKPI.Clear();
        Boolean res;
        res = Common.Execute_Procedures_IUD(dsKPI);
        if (res)
        {
            DataTable dtReport = dsKPI.Tables[0];
            rptMaintenanceKPIData.DataSource = dtReport;
            rptMaintenanceKPIData.DataBind();
        }
    }
    
    protected void btOpenReport_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "sdfsd", "<script type=\"text/javascript\"> window.open('Reports/MaintenanceKPI.aspx?VC="+ ddlVessels.SelectedValue.Trim() + "&Year="+ DateTime.Today.AddMonths(-1).Year + "" + "') </script>");
    }
    protected void btnPublish_Click(object sender, EventArgs e)
    {

        RepeaterItem ri =(RepeaterItem)((System.Web.UI.Control)sender).Parent;
        HiddenField hfdvsl = (HiddenField)ri.FindControl("hfdvsl");
        HiddenField hfdmOnth = (HiddenField)ri.FindControl("hfdmOnth");
        HiddenField hfdyear = (HiddenField)ri.FindControl("hfdyear");

        HiddenField hfdDue =(HiddenField)ri.FindControl("hfdDue");
        HiddenField hfdOverDue = (HiddenField)ri.FindControl("hfdOverDue");
        HiddenField hfdTotal = (HiddenField)ri.FindControl("hfdTotal");
        HiddenField hfdPP = (HiddenField)ri.FindControl("hfdPP");
        HiddenField hfdPPD = (HiddenField)ri.FindControl("hfdPPD");
        HiddenField hfdOut = (HiddenField)ri.FindControl("hfdOut");

        HiddenField hfdDueNext = (HiddenField)ri.FindControl("hfdDueNext");
        HiddenField hfdODueNext = (HiddenField)ri.FindControl("hfdODueNext");

        int Due = Common.CastAsInt32(hfdDue.Value);
        int ODue = Common.CastAsInt32(hfdOverDue.Value);
        int Total = Common.CastAsInt32(hfdTotal.Value);
        int PP = Common.CastAsInt32(hfdPP.Value);
        int PPD = Common.CastAsInt32(hfdPPD.Value);
        int Out = Common.CastAsInt32(hfdOut.Value);
        int DueNext = Common.CastAsInt32(hfdDueNext.Value);
        int ODueNext = Common.CastAsInt32(hfdODueNext.Value);

        //string sql = "EXEC DBO.UPDATE_MAINTAINANCEKPI '" + hfdvsl.Value + "'," + hfdyear.Value + ","  + hfdmOnth.Value + ",";

        Common.Set_Procedures("UPDATE_MAINTAINANCEKPI");
        Common.Set_ParameterLength(12);
        Common.Set_Parameters(
            new MyParameter("@vesselcode", hfdvsl.Value),
            new MyParameter("@month", hfdmOnth.Value),
            new MyParameter("@year", hfdyear.Value),

            new MyParameter("@due", Due),
            new MyParameter("@odue", ODue),
            new MyParameter("@tot", Total),
            new MyParameter("@pp", PP),
            new MyParameter("@ppd", PPD),
            new MyParameter("@out", Out),

            new MyParameter("@duenext", DueNext),
            new MyParameter("@oduenext", ODueNext),
            new MyParameter("@PublishedBy", Session["loginid"].ToString())

            );
        DataSet dsKPI = new DataSet();
        dsKPI.Clear();
        Boolean res;
        res = Common.Execute_Procedures_IUD(dsKPI);
        if (res)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "sdfsd", "<script type=\"text/javascript\"> alert('Record has been published successfully') </script>");
            ShowReport();
        }
        else
        {
            ClientScript.RegisterStartupScript(this.GetType(), "sdfsd", "<script type=\"text/javascript\"> alert('Error while publishing the record') </script>");
        }
        
    }
    protected void btnViewReport_Click(object sender, EventArgs e)
    {        
        ShowReport();
    }
    protected void ddlFleet_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindVessels();
    }
    
}
