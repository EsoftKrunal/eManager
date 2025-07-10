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

public partial class ER_S115_Analysis : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    int intLogin_Id;   
    
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
        lblMsg_LTI.Text = "";
        
        if(!(IsPostBack))
        {
            BindFleet();
            Load_vessel();
            txtFromDate.Text = "01-Jan-" + DateTime.Today.Year.ToString();
            txtToDate.Text = DateTime.Today.Date.ToString("dd-MMM-yyyy");

            BindVessel_LTI();
            BindYear();

            ddl_Year.SelectedValue = DateTime.Today.Year.ToString();
        }

        if (rdoIR.Checked)
        {
            btnShowReport_Click(sender, e);
        }
        else
        {
           btnShow_LTI_Click(sender, e);
        }
    }
    private void BindFleet()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * from dbo.FleetMaster order by FleetName");

        ddlFleet.DataSource = dt;
        ddlFleet.DataTextField = "FleetName";
        ddlFleet.DataValueField = "FleetId";
        ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, new ListItem(" < All >", ""));
    }
    private void Load_vessel()
    {
        if (ddlFleet.SelectedIndex <= 0)
        {
            DataSet dt = Budget.getTable("Select * from dbo.vessel where vesselstatusid<>2 and VesselId in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ") order by vesselname");
            ddlVessel.DataSource = dt;
            ddlVessel.DataTextField = "VesselName";
            ddlVessel.DataValueField = "VesselId";
            ddlVessel.DataBind();
            ddlVessel.Items.Insert(0, new ListItem(" < All >", ""));
        }
        else
        {
            DataSet dt = Budget.getTable("Select * from dbo.vessel where fleetid=" + ddlFleet.SelectedValue + " and vesselstatusid<>2 and VesselId in (Select VesselId from UserVesselRelation with(nolock) where Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ") order by vesselname");
            ddlVessel.DataSource = dt;
            ddlVessel.DataTextField = "VesselName";
            ddlVessel.DataValueField = "VesselId";
            ddlVessel.DataBind();
            ddlVessel.Items.Insert(0, new ListItem(" < All >", ""));
        }
    }
    protected void ddlFleet_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        Load_vessel();
    }
    protected void btnShowReport_Click(object sender, EventArgs e)
    {
       // ScriptManager.RegisterStartupScript(this, this.GetType(), "aa", "window.open('ER_S115_Analysis_Excel.aspx?VesselId=" + ddlVessel.SelectedValue + "&NMType=" + ddlNMType.SelectedValue + "&AccCat=" + ddlAcccategory.SelectedValue + "&Fdt=" + txtFromDate.Text + "&Tdt=" + txtToDate.Text + "','','');", true);  

        try
        {

            Common.Set_Procedures("ER_S115_ANALYSISREPORT");
            Common.Set_ParameterLength(4);
            Common.Set_Parameters(
                new MyParameter("@VESSELID", Common.CastAsInt32(ddlVessel.SelectedValue)),
                new MyParameter("@FDT", (txtFromDate.Text.Trim() == "" ? null : txtFromDate.Text.Trim())),
                new MyParameter("@TDT", (txtToDate.Text.Trim() == "" ? null : txtToDate.Text.Trim())),
                new MyParameter("@LoginId", Convert.ToInt32(Session["loginid"].ToString()))
                );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
               ShowReportDetails(ds); 
            }
            else
            {
                lblmessage.Text = "Unable to search record. Error : " + Common.getLastError();
            }
        }
        catch (Exception ex)
        {
            lblmessage.Text = "Unable to search record. Error : " + ex.Message;
        }
        
    }  
    public void ShowReportDetails(DataSet ds)
    {
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("ER_S115_AnalysisReport.rpt"));
        rpt.SetDataSource(ds.Tables[0]);
        rpt.SetParameterValue("HEADERTEXT", txtFromDate.Text.Trim() + " - " + txtToDate.Text.Trim());
    } 
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    } 
    protected void rdoType_CheckedChanged(object sender, EventArgs e)
    {
        if (rdoIR.Checked)
        {
            dv_InjuryAnalysis.Visible = true;
            dv_LTI.Visible = false;
            ddlFleet.SelectedIndex = 0;
            Load_vessel();
            txtFromDate.Text = "01-Jan-" + DateTime.Today.Year.ToString();
            txtToDate.Text = DateTime.Today.Date.ToString("dd-MMM-yyyy");
            btnShowReport_Click(sender, e);
            
        }
        if (rdoLTI.Checked)
        {
            dv_InjuryAnalysis.Visible = false;
            dv_LTI.Visible = true;

            chklst_AllVsl.Checked = true;
            
            for (int a = 0; a < chklst_Vsls.Items.Count; a++)
            {
                chklst_Vsls.Items[a].Selected = true;
            }

            ddl_Year.SelectedValue = DateTime.Today.Year.ToString();

            btnShow_LTI_Click(sender, e);

        }
    } 
    protected void BindVessel_LTI()
    {
        
        DataSet dt = Budget.getTable("Select * from dbo.vessel where vesselstatusid<>2 and VesselId in (Select vw.VesselId from UserVesselRelation vw with(nolock)  where vw.Loginid = " + Convert.ToInt32(Session["loginid"].ToString()) + ") order by vesselname");
        
        this.chklst_Vsls.DataSource = dt;
        this.chklst_Vsls.DataTextField = "VesselName";
        this.chklst_Vsls.DataValueField = "VesselId";
        this.chklst_Vsls.DataBind();
            
        for (int a = 0; a < chklst_Vsls.Items.Count; a++)
        {
            chklst_Vsls.Items[a].Selected = true;
        }
        
    } 
    public void BindYear()
    {
        for(int i = 2010; i <= DateTime.Today.Date.Year; i++)
        {
            ddl_Year.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }

        ddl_Year.Items.Insert(0, new ListItem("< Select >", "0"));
    } 
    protected void btnShow_LTI_Click(object sender, EventArgs e)
    {
        string VesselId = "";
        int intVesselCount = 0;
        Boolean flag = false;


        for (int a = 0; a < chklst_Vsls.Items.Count; a++)
        {
            if (chklst_Vsls.Items[a].Selected == true)
                flag = true;
        }
        if (flag == false)
        {
            lblMsg_LTI.Text = "Please select atleast one Vessel.";
            return;
        }
        if (ddl_Year.SelectedIndex == 0)
        {
            lblMsg_LTI.Text = "Please select a Year.";
            ddl_Year.Focus();
            return;
        }
        
        for (int J = 0; J < chklst_Vsls.Items.Count; J++)
        {
            if (chklst_Vsls.Items[J].Selected == true)
            {
                if (VesselId == "")
                {
                    VesselId = chklst_Vsls.Items[J].Value;
                }
                else
                {
                    VesselId = VesselId + "," + chklst_Vsls.Items[J].Value;
                }
            }
        }
        
        for (int c = 0; c < chklst_Vsls.Items.Count; c++)
        {
            if (chklst_Vsls.Items[c].Selected == true)
                intVesselCount++;
        }

        try
        {

            Common.Set_Procedures("DBO.ER_S115_LTI_REPORT");
            Common.Set_ParameterLength(2);
            Common.Set_Parameters(
                new MyParameter("@VESSELID", VesselId),
                new MyParameter("@YEAR", ddl_Year.SelectedValue)
                );
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                CrystalReportViewer1.ReportSource = rpt;
                rpt.Load(Server.MapPath("RPT_LTIReport.rpt"));
                rpt.SetDataSource(ds.Tables[0]);

                rpt.SetParameterValue("@Header", "Lost Time Incidents - LTI REPORTING\nBased on OCIMF Criteria & Guidelines");
                rpt.SetParameterValue("@VslParameter", intVesselCount.ToString());
                rpt.SetParameterValue("@YearParameter", ddl_Year.SelectedValue.ToString());
            }
            else
            {
                lblMsg_LTI.Text = "Unable to search record. Error : " + Common.getLastError();
            }
        }
        catch (Exception ex)
        {
            lblMsg_LTI.Text = "Unable to search record. Error : " + ex.Message;
        }
    }
}
