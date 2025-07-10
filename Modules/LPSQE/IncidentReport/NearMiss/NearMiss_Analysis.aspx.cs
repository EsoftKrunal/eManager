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

public partial class ER_S133_Analysis : System.Web.UI.Page
{
  
    int intLogin_Id;
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt2 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        if (Session["loginid"] == null)
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "logout", "alert('Your Session is Expired. Please Login Again.');window.parent.parent.location='../Default.aspx';", true);
        }
        else
        {
            intLogin_Id = Convert.ToInt32(Session["loginid"].ToString());
        }

        lblmessage.Text = "";
        if(!(IsPostBack))
        {
            for(int i=DateTime.Today.Year ;i>=2014;i--)
                dddlYear.Items.Add(new ListItem(i.ToString(),i.ToString()));

            Load_vessel();
        }
        if (pnlSummary.Visible)
        {
            ShowSummary();
        }
        if (pnlRootCause.Visible)
        {
            ShowSummary2();
        }
    }
    private void Load_vessel()
    {
        DataSet dt = Budget.getTable("Select * from dbo.vessel where vesselstatusid<>2  order by vesselname");
        ddlVessel.DataSource = dt;
        ddlVessel.DataTextField = "VesselName";
        ddlVessel.DataValueField = "VesselCode";
        ddlVessel.DataBind();

        ddlVessel.Items.Insert(0,new ListItem(" < All >",""));
    }
    protected void radType_CheckChanged(object sender, EventArgs e)
    {
        pnlSummary.Visible = radKPI.Checked;
        pnlAnalysis.Visible = radAnalyisis.Checked;
        pnlRootCause.Visible = radRootCause.Checked;

        if (pnlSummary.Visible)
        {
            ShowSummary();  
        }
    }
    protected void ShowSummary()
    {
        this.CrystalReportViewer1.Visible = true;
        CrystalReportViewer1.ReportSource = rpt;

        DataTable dt_MeetingMaster = Common.Execute_Procedures_Select_ByQuery("exec dbo.ER_S133_KPI_SUMMARY " + dddlYear.SelectedValue + ",0");
        dt_MeetingMaster.TableName = "ER_S133_KPI_SUMMARY";
        rpt.Load(Server.MapPath("ER_S133_Report_KPI_SUMMARY.rpt"));
        //rpt.SetParameterValue("@Year", dddlYear.SelectedValue);
        rpt.SetDataSource(dt_MeetingMaster);
        rpt.SetParameterValue("PageHeading", "Nearmiss KPI Summary Report - " + dddlYear.SelectedValue);
        
    }
    protected void ShowSummary2()
    {
        this.CrystalReportViewer2.Visible = true;
        CrystalReportViewer2.ReportSource = rpt2;

        DataTable dt_MeetingMaster = Common.Execute_Procedures_Select_ByQuery("exec dbo.ER_S133_KPI_SUMMARY_BY_JOBFACTORS '" + ddlSeverity1.SelectedValue + "'");
        dt_MeetingMaster.TableName = "ER_S133_Report_KPI_SUMMARY_BYJOB";
        rpt2.Load(Server.MapPath("ER_S133_Report_KPI_SUMMARY_BYJOB.rpt"));
        rpt2.SetDataSource(dt_MeetingMaster);
        rpt2.SetParameterValue("PageHeading", "Nearmiss Analysis By Root Cause");
        rpt2.SetParameterValue("YEAR1", DateTime.Today.Year.ToString());
        rpt2.SetParameterValue("YEAR2", (DateTime.Today.Year - 1).ToString());
        rpt2.SetParameterValue("YEAR3", (DateTime.Today.Year - 2).ToString());
        rpt2.SetParameterValue("YEAR4", (DateTime.Today.Year - 3).ToString());
        rpt2.SetParameterValue("YEAR5", (DateTime.Today.Year - 4).ToString());
    }
    protected void btnShowSummary_Click(object sender, EventArgs e)
    {

    }
    protected void btnShowReport2_Click(object sender, EventArgs e)
    {

    }
    protected void btnDownloadExcel_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "aa", "window.open('NearMiss_Analysis_Excel.aspx?VesselCode=" + ddlVessel.SelectedValue + "&NMType=" + ddlNMType.SelectedValue + "&AccCat=" + ddlAcccategory.SelectedValue + "&Fdt=" + txtFromDate.Text + "&Tdt=" + txtToDate.Text + "','','');", true);  
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();

        rpt2.Close();
        rpt2.Dispose();
    }
}
