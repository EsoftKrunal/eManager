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

public partial class DefectswithoutOfficeComments : System.Web.UI.Page
{

    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    public string VessCode
    {
        get { return Convert.ToString(ViewState["VessCode"]); }
        set { ViewState["VessCode"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        lblMessage.Text = "";
        if (!Page.IsPostBack)
        {
            //txtFromDt.Text = DateTime.Today.AddDays(-Common.CastAsInt32(Request.QueryString["Days"])).ToString("dd-MMM-yyyy");    
            //txtToDt.Text=DateTime.Today.ToString("dd-MMM-yyyy");
            BindFleet();
            BindYear();
            BindVessel();
            //Fleet_OnSelectedIndexChanged(sender, e);
        }
    }
    private void BindFleet()
    {
        DataTable dtVessels = new DataTable();
        string strvessels = "SELECT * from dbo.fleetmaster ORDER BY FLEETNAME ";
        dtVessels = Common.Execute_Procedures_Select_ByQuery(strvessels);

        ddlFleet.DataSource = dtVessels;
        ddlFleet.DataTextField = "FLEETNAME";
        ddlFleet.DataValueField = "FLEETID";
        ddlFleet.DataBind();
        ddlFleet.Items.Insert(0, "< All Fleet >");
    }
    protected void Fleet_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindVessel();
    }


    protected void btnViewReport_Click(object sender, EventArgs e)
    {

        //if (txtToDt.Text == "")
        //{
        //    lblMessage.Text = "Please enter to date.";
        //    txtToDt.Focus();
        //    return;
        //}
        //DateTime temp1;
        //if (!DateTime.TryParse(txtToDt.Text.Trim(), out temp1))
        //{
        //    lblMessage.Text = "Please enter valid date.";
        //    txtToDt.Focus();
        //    return;
        //}
        ShowReport();
    }
    protected void ShowReport()
    {
        string strSQL = "";
        string WhereClause = " where VesselCode='" + hfVessel.Value + "'";

        WhereClause = WhereClause + " And year(ReportDt)="+ddlYear.SelectedValue+" ";

        if (txtCompCode.Text.Trim() != "")
            WhereClause = WhereClause + " And ComponentCode like '" + txtCompCode.Text.Trim() + "%'";

        //if (ddlStatus.SelectedIndex == 1)
        //{
        //    WhereClause = WhereClause + " And DefectStatus ='Open' ";
        //}
        //else if (ddlStatus.SelectedIndex == 2)
        //{
        //    WhereClause = WhereClause + " And DefectStatus ='Closed' ";
        //}

        if (hfType.Value == "2")
            WhereClause = WhereClause + " And PendingClosure=1";

        strSQL = "SELECT * from VW_Vsl_DefectDetailsMaster " + WhereClause + " ";

        DataTable dtReport = null;
        try
        {
            dtReport = Common.Execute_Procedures_Select_ByQuery(strSQL);
        }
        catch
        { }

        rptComponentUnits.DataSource = dtReport;
        rptComponentUnits.DataBind();

        //if(dtReport==null)
        //    lblCount.Text = "0 Record(s) found.";
        //else
        //    lblCount.Text = dtReport.Rows.Count.ToString() + " Record(s) found.";
    }
    public string DateString(object dt)
    {
        if (Convert.IsDBNull(dt))
        {
            return "";
        }
        else
        {
            try
            {
                return Convert.ToDateTime(dt).ToString("dd-MMM-yyyy");
            }
            catch { return ""; }
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    protected void chkVerifed_OnCheckedChanged(object sender, EventArgs e)
    {
        //CheckBox ch=((CheckBox)sender); 
        //string UserName = Session["UserName"].ToString();
        //string VesselCode = ch.Attributes["vsl"];
        //string HistoryId = ch.Attributes["historyid"];
        //Common.Set_Procedures("VerifyJobHistory");
        //Common.Set_ParameterLength(3);
        //Common.Set_Parameters(
        //    new MyParameter("@VesselCode", VesselCode),
        //    new MyParameter("@HistoryId", HistoryId),
        //    new MyParameter("@VerifiedBy", UserName));
        //DataSet ds = new DataSet();
        //if (Common.Execute_Procedures_IUD(ds))
        //{
        //    ProjectCommon.ShowMessage("Verifed Successfully.");
        //}
        //else
        //{
        //    ProjectCommon.ShowMessage("Unable to Verify.");
        //}
        //ShowReport();
    }

    protected void btnVerify_OnClick(object sender, EventArgs e)
    {
        Button btnVerify = (Button)sender;
        string VesselCode = btnVerify.Attributes["vsl"];
        string HistoryId = btnVerify.Attributes["historyid"];
        string CompName = btnVerify.Attributes["CompName"];

        ScriptManager.RegisterStartupScript(Page, this.GetType(), "a", "window.open('VerifyJobsPopUp.aspx?VesselCode=" + VesselCode + "&HistoryId=" + HistoryId + "&CompName=" + CompName + "','','status=1,scrollbars=0,toolbar=0,menubar=0,width=700,height=450,top=150,left=250')", true);

        //CheckBox ch=((CheckBox)sender); 
        //string UserName = Session["UserName"].ToString();
        //string VesselCode = ch.Attributes["vsl"];
        //string HistoryId = ch.Attributes["historyid"];
        //Common.Set_Procedures("VerifyJobHistory");
        //Common.Set_ParameterLength(3);
        //Common.Set_Parameters(
        //    new MyParameter("@VesselCode", VesselCode),
        //    new MyParameter("@HistoryId", HistoryId),
        //    new MyParameter("@VerifiedBy", UserName));
        //DataSet ds = new DataSet();
        //if (Common.Execute_Procedures_IUD(ds))
        //{
        //    ProjectCommon.ShowMessage("Verifed Successfully.");
        //}
        //else
        //{
        //    ProjectCommon.ShowMessage("Unable to Verify.");
        //}
        //ShowReport();
    }

    //--------------
    public void BindYear()
    {
        for (int i = DateTime.Now.Year; i >= 2010; i--)
        {
            ddlYear.Items.Add(i.ToString());
        }
        //ddlYear.Items.Insert(0,new ListItem(" < All > ", ""));
        ddlYear.SelectedValue = DateTime.Today.Year.ToString();
    }
    public void BindVessel()
    {
        string WhereClause = " ";

        if (ddlFleet.SelectedIndex != 0)
            WhereClause = " and VesselCode in(select VesselCode from dbo.Vessel where FleetId=" + ddlFleet.SelectedValue + ") ";
        else
            WhereClause = " and VesselCode in(select VesselCode from dbo.Vessel where ISNULL(FleetId,0) >0 ) ";

        DataTable dtVessels = new DataTable();
        string strvessels = "SELECT * FROM  (" +
                            "  SELECT VesselCode,COUNT(1)Total,SUM(PendingClosure) UnVerified from VW_Vsl_DefectDetailsMaster " +
                            "  where year(ReportDt)="+ddlYear.SelectedValue +" GROUP BY VESSELCODE " +
                            ") A WHERE 1=1 " + WhereClause + " ORDER BY VESSELCODE";



        dtVessels = Common.Execute_Procedures_Select_ByQuery(strvessels);

        rptRepeterFleet.DataSource = dtVessels;
        rptRepeterFleet.DataBind();

    }
    protected void ddlYear_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindVessel();
    }
    protected void ddlStatus_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ShowReport();
    }
    
    protected void btnTemp_OnClick(object sender, EventArgs e)
    {
        int i = 10;
        ShowReport();
    }
}

