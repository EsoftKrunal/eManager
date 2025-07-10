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

public partial class Reports_HullMoniteringReport : System.Web.UI.Page
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
        }
        ShowReport();
    }
    private void BindVessels()
    {
        DataTable dtVessels = new DataTable();
        string strvessels = "SELECT VesselId,VesselCode,VesselName FROM Vessel VM WHERE EXISTS(SELECT TOP 1 COMPONENTID FROM  VSL_ComponentMasterForVessel EVM WHERE VM.VesselCode = EVM.VesselCode ) ORDER BY VesselName ";
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
        ddlVessels.Items.Insert(0, "< SELECT VESSEL >");
    }
    protected void btnViewReport_Click(object sender, EventArgs e)
    {
        if (ddlVessels.SelectedIndex == 0)
        {
            lblMessage.Text = "Please select a vessel.";
            ddlVessels.Focus();
            return;
        }
        //if (txtFromDt.Text == "")
        //{
        //    lblMessage.Text = "Please enter from date.";
        //    txtFromDt.Focus();
        //    return;
        //}
        //if (txtFromDt.Text.Trim() != "")
        //{
        //    DateTime temp;
        //    if (!DateTime.TryParse(txtFromDt.Text.Trim(), out temp))
        //    {
        //        lblMessage.Text = "Please enter valid date.";
        //        txtFromDt.Focus();
        //        return;
        //    }
        //}
        //if (txtToDt.Text == "")
        //{
        //    lblMessage.Text = "Please enter to date.";
        //    txtToDt.Focus();
        //    return;
        //}
        //if (txtToDt.Text.Trim() != "")
        //{
        //    DateTime temp1;
        //    if (!DateTime.TryParse(txtToDt.Text.Trim(), out temp1))
        //    {
        //        lblMessage.Text = "Please enter valid date.";
        //        txtToDt.Focus();
        //        return;
        //    }
        //}
        ShowReport();
    }
    protected void ShowReport()
    {
        string strVesselName = "";
        string VesselName = "";
        if (Session["UserType"].ToString() == "S")
        {
            strVesselName = "SELECT shipName FROM Settings where shipcode = '" + ddlVessels.SelectedValue.Trim() + "' ";
        }
        else
        {
            strVesselName = "SELECT VesselName FROM Vessel where VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' ";
        }
        DataTable dtVesselname = Common.Execute_Procedures_Select_ByQuery(strVesselName);
        //if (Session["UserType"].ToString() != "S")
        //{
            if (ddlVessels.SelectedIndex != 0)
            {
                VesselName = dtVesselname.Rows[0][0].ToString();
            }
        //}

        string strSQL = "SELECT * from vw_HullMonitoring WHERE VesselCode = '" + ddlVessels.SelectedValue.Trim() + "' ";
        string WhereCond = "";

        //if (txtFromDt.Text.Trim() != "" && txtToDt.Text.Trim() != "")
        //{
        //    WhereCond = "AND (DoneDate BETWEEN '" + txtFromDt.Text.Trim() + "' AND '" + txtToDt.Text.Trim() + "')";
        //}
        
        DataTable dtReport = Common.Execute_Procedures_Select_ByQuery(strSQL + WhereCond);
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("HullMonitoringReport.rpt"));
        rpt.SetDataSource(dtReport);
        rpt.SetParameterValue("VesselName", VesselName);
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
