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

public partial class Reports_MaintenanceKPI : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        lblMessage.Text = "";
        if (!Page.IsPostBack)
        {
            if (Session["UserType"].ToString() == "S")
            {
                ddlVessels.Items.Insert(0, new ListItem("< SELECT >", "0"));
                ddlVessels.Items.Insert(1, new ListItem(Session["CurrentShip"].ToString(), Session["CurrentShip"].ToString()));
                ddlVessels.SelectedIndex = 1;
                ddlVessels.Visible = false;
                tdVessel.Visible = false;
            }
            else
            {
                tdVessel.Visible = true;
                BindVessels();
            }
            BindYears();
            BindMonths();

            if (Page.Request.QueryString["VC"] != null || Page.Request.QueryString["Year"] != null)
            {
                ddlVessels.SelectedValue = Page.Request.QueryString["VC"].ToString();
                ddlYear.SelectedValue = Page.Request.QueryString["Year"].ToString();                
            }

        }
        btnViewReport_Click(sender, e);

    }

    private void BindVessels()
    {
        DataTable dtVessels = new DataTable();
        string strvessels = "SELECT VesselId,VesselCode,VesselName FROM Vessel VM WHERE vesselstatusid=1 and EXISTS(SELECT TOP 1 COMPONENTID FROM  VSL_ComponentMasterForVessel EVM WHERE VM.VesselCode = EVM.VesselCode ) ORDER BY VesselName ";
        dtVessels = Common.Execute_Procedures_Select_ByQuery(strvessels);
        if (dtVessels.Rows.Count > 0)
        {
            ddlVessels.DataSource = dtVessels;
            ddlVessels.DataTextField = "VesselName";
            ddlVessels.DataValueField = "VesselCode";
            ddlVessels.DataBind();
        }
        else
        {
            ddlVessels.DataSource = null;
            ddlVessels.DataBind();
        }
        ddlVessels.Items.Insert(0, new ListItem("< All >", ""));
    }
    private void BindYears()
    {
        for (int i = DateTime.Today.Year; i >= 2014 ; i--)
        {
          ddlYear.Items.Add(new ListItem(Convert.ToString(i),Convert.ToString(i))); 
        }
        //ddlYear.Items.Insert(0, new ListItem("< SELECT >", "0"));

    }
    private void BindMonths()
    {
        ddlFromMt.Items.Add(new ListItem("Jan", "1"));
        ddlFromMt.Items.Add(new ListItem("Feb", "2"));
        ddlFromMt.Items.Add(new ListItem("Mar", "3"));
        ddlFromMt.Items.Add(new ListItem("Apr", "4"));
        ddlFromMt.Items.Add(new ListItem("may", "5"));
        ddlFromMt.Items.Add(new ListItem("Jun", "6"));
        ddlFromMt.Items.Add(new ListItem("Jul", "7"));
        ddlFromMt.Items.Add(new ListItem("Aug", "8"));
        ddlFromMt.Items.Add(new ListItem("Sep", "9"));
        ddlFromMt.Items.Add(new ListItem("Oct", "10"));
        ddlFromMt.Items.Add(new ListItem("Nov", "11"));
        ddlFromMt.Items.Add(new ListItem("Dec", "12"));
        //ddlFromMt.Items.Insert(0, "< SELECT >");


        ddlToMt.Items.Add(new ListItem("Jan", "1"));
        ddlToMt.Items.Add(new ListItem("Feb", "2"));
        ddlToMt.Items.Add(new ListItem("Mar", "3"));
        ddlToMt.Items.Add(new ListItem("Apr", "4"));
        ddlToMt.Items.Add(new ListItem("may", "5"));
        ddlToMt.Items.Add(new ListItem("Jun", "6"));
        ddlToMt.Items.Add(new ListItem("Jul", "7"));
        ddlToMt.Items.Add(new ListItem("Aug", "8"));
        ddlToMt.Items.Add(new ListItem("Sep", "9"));
        ddlToMt.Items.Add(new ListItem("Oct", "10"));
        ddlToMt.Items.Add(new ListItem("Nov", "11"));
        ddlToMt.Items.Add(new ListItem("Dec", "12"));
        int cur_month = DateTime.Today.AddMonths(-1).Month;
        if (cur_month == 12)
            cur_month = 1;

            ddlToMt.SelectedValue = cur_month.ToString();
    }
    protected void btnViewReport_Click(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        }
        else
        {
            //if (ddlVessels.SelectedIndex == 0)
            //{
            //    lblMessage.Text = "Please select a vessel.";
            //    ddlVessels.Focus();
            //    return;
            //}
        }
        //if (ddlYear.SelectedIndex == 0)
        //{
        //    lblMessage.Text = "Please select a year.";
        //    ddlYear.Focus();
        //    return;
        //}
        //if (ddlFromMt.SelectedIndex == 0)
        //{
        //    lblMessage.Text = "Please select a month.";
        //    ddlFromMt.Focus();
        //    return;
        //}
        //if (ddlToMt.SelectedIndex == 0)
        //{
        //    lblMessage.Text = "Please select a month.";
        //    ddlToMt.Focus();
        //    return;
        //}
        ShowReport();
    }
    protected void ShowReport()
    {
        Common.Set_Procedures("sp_MaintenanceKPI");
        Common.Set_ParameterLength(4);
        Common.Set_Parameters(
            new MyParameter("@shipid", ddlVessels.SelectedValue.Trim()),
            new MyParameter("@year", ddlYear.SelectedValue.Trim()),
            new MyParameter("@month", ddlFromMt.SelectedValue.Trim()),
            new MyParameter("@month1", ddlToMt.SelectedValue.Trim())
            );

        DataSet dsKPI = new DataSet();
        dsKPI.Clear();
        Boolean res;
        res = Common.Execute_Procedures_IUD(dsKPI);
        if (res)
        {
            string strVesselName = "";
            if (Session["UserType"].ToString() == "S")
            {
                strVesselName = "SELECT shipName FROM Settings where shipcode = '" + ddlVessels.SelectedValue.Trim() + "' ";
            }
            else
            {
                strVesselName = "SELECT VesselName FROM Vessel where VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' ";
            }
            DataTable dtVesselname = Common.Execute_Procedures_Select_ByQuery(strVesselName);

            DataTable dtReport = dsKPI.Tables[0];
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("MaintenanceKPI.rpt"));
            rpt.SetDataSource(dtReport);

            if(dtVesselname.Rows.Count >0) 
                rpt.SetParameterValue("VesselName", dtVesselname.Rows[0][0].ToString());
            else
                rpt.SetParameterValue("VesselName", "");

            rpt.SetParameterValue("Year", ddlYear.SelectedValue.Trim().ToString());
           
        }
        else
        {
            
        }
        
        
        
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
