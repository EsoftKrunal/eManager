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

public partial class CaseManagementReport : System.Web.UI.Page
{
    public int CaseID
    {
        set { ViewState["CaseID"] = value; }
        get { return Common.CastAsInt32(ViewState["CaseID"]); }
    }
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblmessage.Text = "";
        try
        {
            if (Page.Request.QueryString["CaseID"].ToString() != "")
                CaseID = Common.CastAsInt32(Page.Request.QueryString["CaseID"]);

        }
        catch { }

        Show_Report();
    }
    private void Show_Report()
    {
        try
        {
            DataSet dtCase = Budget.getTable("EXEC sp_IRM_GetCaseManagementData " + CaseID + "");
            if (dtCase.Tables[0].Rows.Count > 0)
            {

                if (dtCase != null)
                {
                    this.CrystalReportViewer1.Visible = true;
                    CrystalReportViewer1.ReportSource = rpt;
                    rpt.Load(Server.MapPath("CaseManagementReport.rpt"));

                    dtCase.Tables[0].TableName = "vw_IRM_CaseMaster";
                    dtCase.Tables[1].TableName = "vw_IRM_Synopsis";
                    dtCase.Tables[2].TableName = "vw_IRM_OfficeRemarks";


                    rpt.SetDataSource(dtCase);
                }
            }
            else
            {
                lblmessage.Text = "No Data Found";
                this.CrystalReportViewer1.Visible = false;
            }
        }
        catch { }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
