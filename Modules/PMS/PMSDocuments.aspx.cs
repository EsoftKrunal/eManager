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

public partial class Reports_PMSDocuments : System.Web.UI.Page
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
                if (ddlJobStatus.SelectedValue=="2")
                    WhereClause = WhereClause + " And AttachmentCreatedOn is not null";
                else
                    WhereClause = WhereClause + " And AttachmentCreatedOn is null";
        }

        if (txtFromDt.Text.Trim() != "" && txtToDt.Text.Trim() != "")
        {
            WhereClause = WhereClause + " AND ((DoneDate BETWEEN '" + txtFromDt.Text.Trim() + "' AND '" + txtToDt.Text.Trim() + "'))";
        }
        
        string strSQL = "SELECT *" +
                       "FROM " +
                       "( " +
                       "SELECT P.*, " +
                       "(select top 1 CreatedBy from tbl_Vessel_Communication C WHERE C.VesselCode=P.VesselCode AND C.RecordId=P.HistoryId AND C.RecordType='JOBHISTORY-ATTACHMENTS' ORDER BY TABLEID DESC) AS AttachmentCreatedBy, " +
                       "(select top 1 CreatedOn from tbl_Vessel_Communication C WHERE C.VesselCode=P.VesselCode AND C.RecordId=P.HistoryId AND C.RecordType='JOBHISTORY-ATTACHMENTS' ORDER BY TABLEID DESC) AS AttachmentCreatedOn " +
                       "FROM vw_GetJobUpdateDataByPeriod P WHERE (SELECT COUNT(*) FROM [dbo].[VSL_VesselJobUpdateHistoryAttachments] AT WHERE AT.HistoryId=P.HistoryId AND AT.VesselCode=P.VesselCode) >0" +
                       ") A " + WhereClause;



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

    protected void btnExport_Click(object sender, EventArgs e)
    {
        int HistoryId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        string donedate = ((ImageButton)sender).Attributes["donedate"].ToString();
        string compcode = ((ImageButton)sender).Attributes["compcode"].ToString();
        
        try
        {
            Common.Set_Procedures("[DBO].[sp_Insert_Communication_Export]");
            Common.Set_ParameterLength(5);
            Common.Set_Parameters(
                new MyParameter("@VesselCode", VessCode),
                new MyParameter("@RecordType", "JOBHISTORY-ATTACHMENTS"),
                new MyParameter("@RecordId", HistoryId),
                new MyParameter("@RecordNo", compcode + " - " + donedate),
                new MyParameter("@CreatedBy", Session["UserName"].ToString())
            );

            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('" + ds.Tables[0].Rows[0][0].ToString() + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Sent for export successfully.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Unable to send for export.Error : " + Common.getLastError() + "');", true);

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "asdf", "alert('Unable to send for export.Error : " + ex.Message + "');", true);
        }

        ShowReport();
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
   
}

