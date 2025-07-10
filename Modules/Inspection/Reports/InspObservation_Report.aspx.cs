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

public partial class InspObservation_Report : System.Web.UI.Page
{
    int intInspId;
    string strMTMSd = "";
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        //this.ddl_inspnum.Attributes.Add("onkeydown", "javascript:if(event.keyCode==13){document.getElementById('" + btn_show.ClientID + "').focus();}");
        lblmessage.Text = "";
        ////***********Code to check page acessing Permission
        //int chpageauth = UserPageRelation.CheckUserPageAuthority(Convert.ToInt32(Session["loginid"].ToString()), 113);
        //if (chpageauth <= 0)
        //{
        //    Response.Redirect("DummyReport.aspx");

        //}
        ////*******************
        ////========== Code to check report printing authority
        //DataTable dtcheck = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()));
        //foreach (DataRow dr in dtcheck.Rows)
        //{
        //    if (Convert.ToInt32(dr[0].ToString()) > 0)
        //    {
        //        CrystalReportViewer1.HasPrintButton = false;
        //    }
        //}
        ////==========
        try
        { 
            intInspId = int.Parse(Page.Request.QueryString["InspId"].ToString());
            strMTMSd = Page.Request.QueryString["MTMSp"].ToString();
        }catch { }
        if (!Page.IsPostBack)
        {
            //BindInsNumberDDL();
            showreport();
        }
        else
        {
            showreport();
        }
    }
    //public void BindInsNumberDDL()
    //{
    //    try
    //    {
    //        DataSet ds1 = Inspection_Master.getMasterData("t_InspectionDue", "Id", "InspectionNo");
    //        this.ddl_inspnum.DataSource = ds1.Tables[0];
    //        this.ddl_inspnum.DataValueField = "Id";
    //        this.ddl_inspnum.DataTextField = "InspectionNo";
    //        this.ddl_inspnum.DataBind();
    //        this.ddl_inspnum.Items.Insert(0, new ListItem("<Select>", "0"));
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    private void showreport()
    {
        //if (this.ddl_inspnum.SelectedValue != "-1")
        //{
            //DataTable dt = ObservationReport.SelectObservationDetails(Convert.ToInt32(this.ddl_inspnum.SelectedValue.ToString()));
        DataTable dt = ObservationReport.SelectObservationDetails(intInspId);
            if (dt.Rows.Count > 0)
            {
                this.CrystalReportViewer1.Visible = true;

                CrystalReportViewer1.ReportSource = rpt;
                rpt.Load(Server.MapPath("RPT_Observation.rpt"));

                Session.Add("rptsource", dt);
                rpt.SetDataSource(dt);

                //DataTable dt1 = ResponseReport.SelectCompanyDetails();
                //foreach (DataRow dr in dt1.Rows)
                //{
                //    rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
                //}
                rpt.SetParameterValue("@Header", "Observation Sheet");
                rpt.SetParameterValue("@MTMSpdName", strMTMSd);
            }
            else
            {
                lblmessage.Text = "No Record Found.";
                this.CrystalReportViewer1.Visible = false;
            }
        //}
    }
    //protected void btn_show_Click(object sender, EventArgs e)
    //{
    //    showreport();
    //}
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
