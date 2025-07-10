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

public partial class Reports_PostPoneJobs : System.Web.UI.Page
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
                 VessCode = Session["CurrentShip"].ToString();
                 ShowReport();
            }
        }
    }
    protected void btnViewReport_Click(object sender, EventArgs e)
    {
        if (txtFromDt.Text == "" && txtToDt.Text != "")
        {
            lblMessage.Text = "Please enter from date.";
            txtFromDt.Focus();
            return;
        }
        if (txtFromDt.Text != "" && txtToDt.Text == "")
        {
            lblMessage.Text = "Please enter to date.";
            txtToDt.Focus();
            return;
        }
        DateTime temp;
        if (txtFromDt.Text != "")
        {
            if (!DateTime.TryParse(txtFromDt.Text.Trim(), out temp))
            {
                lblMessage.Text = "Please enter valid date.";
                txtFromDt.Focus();
                return;
            }
        }
        
        DateTime temp1;
        if (txtToDt.Text != "")
        {
            if (!DateTime.TryParse(txtToDt.Text.Trim(), out temp1))
            {
                lblMessage.Text = "Please enter valid date.";
                txtToDt.Focus();
                return;
            }
        }
        ShowReport(); 
    }
    protected void ShowReport()
    {
        string WhereClause = " where vesselcode='" + VessCode + "' ";

        if (txtCompCode.Text.Trim() != "")
            WhereClause = WhereClause + " And ComponentCode like '" + txtCompCode.Text.Trim() + "%'";
        if (txtHistoryId.Text.Trim() != "")
            WhereClause = WhereClause + " And HistoryId =" + txtHistoryId.Text.Trim();

        if (ddlJobStatus.SelectedIndex != 0)
        {
                if (ddlJobStatus.SelectedValue=="1")
                    WhereClause = WhereClause + " And ApprovalStatus='Postpone Requested'";
                else if (ddlJobStatus.SelectedValue == "2")
                    WhereClause = WhereClause + " And ApprovalStatus='Postpone Rejected'";
                else
                    WhereClause = WhereClause + " And ApprovalStatus='Postpone Apporved'";
        }

        if (txtFromDt.Text.Trim() != "" && txtToDt.Text.Trim() != "")
        {
            WhereClause = WhereClause + " AND ((PostPoneDate BETWEEN '" + txtFromDt.Text.Trim() + "' AND '" + txtToDt.Text.Trim() + "'))";
        }

        string strSQL = "SELECT * FROM vw_PostPoneJobs P " + WhereClause;



        DataTable dtReport = Common.Execute_Procedures_Select_ByQuery(strSQL);
        rptComponentUnits.DataSource = dtReport;
        lblCount.Text = dtReport.Rows.Count.ToString() + " Record(s) found.";  
        rptComponentUnits.DataBind();  
    
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
   
}

