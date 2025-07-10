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

public partial class Reports_InspCheckList_Report : System.Web.UI.Page
{
    int intQuesId;
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblmessage.Text = "";
        try
        {
            intQuesId = int.Parse(Page.Request.QueryString["QuestId"].ToString());
        }
        catch { }
        if (!Page.IsPostBack)
        {
            ViewState["Open"] = "No"; 
            
        }
        if (ViewState["Open"].ToString() == "No")
        {
            showreport();
        }
        else
        {
            opensearch();
        }
    }
    private void showreport()
    {
        DataTable dt1 = RespQuestionReport.SelectRespByQuestionIdDetails(intQuesId);
        if (dt1.Rows.Count > 0)
        {
            this.CrystalReportViewer1.Visible = true;

            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("RPT_RespQuestion.rpt"));

            DataView dv = dt1.DefaultView;
            dv.RowFilter = "deficiency like '%" + txt_OpenSerch.Text + "%'";
            dt1 = dv.ToTable();

            if (dt1.Rows.Count <= 0)
            {
                lblmessage.Text = "No Record Found.";
                this.CrystalReportViewer1.Visible = false;
                return; 
            }

            rpt.SetDataSource(dt1);
            rpt.SetParameterValue("@Header", "Question Details");
            rpt.SetParameterValue("@Question", "Question No. : " + dt1.Rows[0]["QuestNo"].ToString());
        }
        else
        {
            lblmessage.Text = "No Record Found.";
            this.CrystalReportViewer1.Visible = false;
        }
    }
    private void opensearch()
    {
        DataTable dt1 = Budget.getTable("EXEC DBO.PR_PRT_OpenSearch '" + txt_OpenSerch.Text + "'").Tables[0];
        if (dt1.Rows.Count > 0)
        {
            this.CrystalReportViewer1.Visible = true;

            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("RPT_RespQuestion.rpt"));

            if (dt1.Rows.Count <= 0)
            {
                lblmessage.Text = "No Record Found.";
                this.CrystalReportViewer1.Visible = false;
                return;
            }

            rpt.SetDataSource(dt1);
            rpt.SetParameterValue("@Header", "Question Details");
            rpt.SetParameterValue("@Question", "");
        }
        else
        {
            lblmessage.Text = "No Record Found.";
            this.CrystalReportViewer1.Visible = false;
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    protected void btnPost_Click(object sender, EventArgs e)
    {
        ViewState["Open"] = "Yes";
        opensearch();
    }
}
