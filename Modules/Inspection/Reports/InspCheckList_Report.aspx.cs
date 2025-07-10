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
    int intChaptId;
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //this.ddl_Inspection.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_show.ClientID + "').focus();}");
        //this.ddl_InspGroup.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_show.ClientID + "').focus();}");
        lblmessage.Text = "";
        try
        {
            intChaptId = int.Parse(Page.Request.QueryString["ChapId"].ToString());
        }
        catch { }
        if (!Page.IsPostBack)
        {
            //BindInspGrpDDL();
            //ddl_InspGroup_SelectedIndexChanged(sender, e);
            showreport();
        }
        else
        {
            //showreport();
            showreport();
        }
    }
    //private void BindInspGrpDDL()
    //{
    //    DataSet ds1 = Inspection_Master.getMasterData("m_InspectionGroup", "Id", "(Code+ ' - ' +Name) as Name");
    //    this.ddl_InspGroup.DataSource = ds1.Tables[0];
    //    if (ds1.Tables[0].Rows.Count > 0)
    //    {
    //        this.ddl_InspGroup.DataValueField = "Id";
    //        this.ddl_InspGroup.DataTextField = "Name";
    //        this.ddl_InspGroup.DataBind();
    //    }
    //}
    //protected void ddl_InspGroup_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindInspectionDDL(Convert.ToInt32(ddl_InspGroup.SelectedValue));
    //}
    //private void BindInspectionDDL(int InspGrpId)
    //{
    //    DataTable dt1 = InspCheckListReport.GetInspectionFromInspGroup(InspGrpId);
    //    this.ddl_Inspection.DataSource = dt1;
    //    if (dt1.Rows.Count > 0)
    //    {
    //        this.ddl_Inspection.DataValueField = "Id";
    //        this.ddl_Inspection.DataTextField = "InspName";
    //        this.ddl_Inspection.DataBind();
    //    }
    //}
    //protected void btn_show_Click(object sender, EventArgs e)
    //{
    //    showreport();
    //}
    private void showreport()
    {
        //DataTable dt2 = InspCheckListReport.SelectInspCheckListDetails(Convert.ToInt32(ddl_Inspection.SelectedValue));
        DataTable dt2 = InspCheckListReport.SelectInspCheckListDetails(intChaptId);
        if (dt2.Rows.Count > 0)
        {
            this.CrystalReportViewer1.Visible = true;

            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("RPT_InspCheckList.rpt"));

            rpt.SetDataSource(dt2);

            rpt.SetParameterValue("@Header", "Inspection CheckList");
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
}
