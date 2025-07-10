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

public partial class JobDoneBetweenPeriod_OfficeVerify : System.Web.UI.Page
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
            if (Session["UserType"].ToString() == "S")
            {
                   
            }
            else
            {
                VessCode = Page.Request.QueryString["VessCode"].ToString();
            }
            if (VessCode != "< All >")
                lblVessel.Text = VessCode;


            txtFromDt.Text = DateTime.Today.AddDays(-Common.CastAsInt32(Request.QueryString["Days"])).ToString("dd-MMM-yyyy");    
            txtToDt.Text=DateTime.Today.ToString("dd-MMM-yyyy");      
            ShowReport();
        }
    }
    //private void BindVessels()
    //{
    //    DataTable dtVessels = new DataTable();
    //    string strvessels = "SELECT VesselId,VesselCode,VesselName FROM Vessel VM WHERE EXISTS(SELECT TOP 1 COMPONENTID FROM  VSL_ComponentMasterForVessel EVM WHERE VM.VesselCode = EVM.VesselCode ) ORDER BY VesselName ";
    //    dtVessels = Common.Execute_Procedures_Select_ByQuery(strvessels);
    //    if (dtVessels.Rows.Count > 0)
    //    {
    //        ddlVessels.DataSource = dtVessels;
    //        ddlVessels.DataTextField = "VesselName";
    //        ddlVessels.DataValueField = "VesselCode";
    //        ddlVessels.DataBind();
    //    }
    //    else
    //    {
    //        ddlVessels.DataSource = null;
    //        ddlVessels.DataBind();
    //    }
    //    ddlVessels.Items.Insert(0, "< SELECT VESSEL >");
    //}
    protected void btnViewReport_Click(object sender, EventArgs e)
    {
        //if (ddlVessels.SelectedIndex == 0)
        //{
        //    lblMessage.Text = "Please select a vessel.";
        //    ddlVessels.Focus();
        //    return;
        //}
        if (txtFromDt.Text == "")
        {
            lblMessage.Text = "Please enter from date.";
            txtFromDt.Focus();
            return;
        }
        DateTime temp;
        if (!DateTime.TryParse(txtFromDt.Text.Trim(), out temp))
        {
            lblMessage.Text = "Please enter valid date.";
            txtFromDt.Focus();
            return;
        }
        if (txtToDt.Text == "")
        {
            lblMessage.Text = "Please enter to date.";
            txtToDt.Focus();
            return;
        }
        DateTime temp1;
        if (!DateTime.TryParse(txtToDt.Text.Trim(), out temp1))
        {
            lblMessage.Text = "Please enter valid date.";
            txtToDt.Focus();
            return;
        }
        ShowReport(); 
    }
    protected void ShowReport()
    {
        string WhereClause = " where 1=1 ";
        if (txtCompCode.Text.Trim() != "")
            WhereClause = WhereClause + " And ComponentCode like '" + txtCompCode.Text.Trim() + "%'";
        if (txtCompName.Text.Trim() != "")
            WhereClause = WhereClause + " And ComponentName like '" + txtCompName.Text.Trim() + "%'";

        if (chkClass.Checked)
            WhereClause = WhereClause + " And ClassEquip =1 ";
        if (txtClassCode.Text != "")
            WhereClause = WhereClause + " AND ClassEquipCode like '" + txtClassCode.Text.Trim() + "%'";


        //if (ddlJobStatus.SelectedIndex != 0)
        //{
        //    if (ddlJobStatus.SelectedIndex == 1)
        //    {
        //        if (Session["UserType"].ToString() == "S")
        //            WhereClause = WhereClause + " And Verified  =1";
        //        else
        //            WhereClause = WhereClause + " And Verified_Off =1";
        //    }
        //    else if (ddlJobStatus.SelectedIndex == 2)
        //    {
        //        if (Session["UserType"].ToString() == "S")
        //            WhereClause = WhereClause + " And Verified  =0";
        //        else
        //            WhereClause = WhereClause + " And Verified_Off =0";
        //    }
        //}
        if (ddlIntType.SelectedIndex != 0)
            if (ddlIntType.SelectedIndex == 2)
                WhereClause = WhereClause + " And IntervalName ='H'";
            else
                WhereClause = WhereClause + " And IntervalName <>'H'";

        if (chkCritical.Checked)
            WhereClause = WhereClause + " And Criticalequip  =1";

        if (chkPostPone.Checked)
            WhereClause = WhereClause + " And Action ='Postponed' ";
        if(chkOfficeComments.Checked)
            WhereClause = WhereClause + " And Comments  <> '' ";

        if (VessCode!="< All >")
            WhereClause = WhereClause + " And VesselCode='"+VessCode+"' ";

        WhereClause = WhereClause + " AND (CASE WHEN ACTION='Report' THEN DONEDATE ELSE POSTPONEDATE END) BETWEEN '" + txtFromDt.Text.Trim() + "' AND '" + txtToDt.Text.Trim() + "'";
        string strSQL = "SELECT * from vw_GetJobUpdateDataByPeriod " + WhereClause + " ORDER BY DoneDate desc";

        DataTable dtReport = Common.Execute_Procedures_Select_ByQuery(strSQL);
        rptComponentUnits.DataSource = dtReport;
        rptComponentUnits.DataBind();
        lblCount.Text = dtReport.Rows.Count.ToString() + " Record(s) found."; 
    
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
}
