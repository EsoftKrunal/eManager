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

public partial class Reporting_WageScale_Crystal : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        this.lblmessage.Text = "";
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 130);
        //==========
        show_report();
    }
    private void show_report()
    {
        int wagescale, nationality, seniority, history;
        string offrat="";
        string effectivedate,wagescalename;
        
        wagescale = Convert.ToInt32(Page.Request.QueryString["WS"]); 
        nationality = Convert.ToInt32(Page.Request.QueryString["Nationality"]);
        seniority = Convert.ToInt32(Page.Request.QueryString["Seniority"]);
        history = Convert.ToInt32(Page.Request.QueryString["History"]);
        effectivedate=Page.Request.QueryString["EffectiveDate"];
        wagescalename = Page.Request.QueryString["WageName"];
        offrat = Page.Request.QueryString["OR"];


        DataTable dt1 = WagesMaster.WageComponents_Report(wagescale, nationality, seniority,offrat);
        DataTable dt2 = WagesMaster.WageComponents_FromHistory_Report(history,offrat);
        if (Convert.ToInt32(Page.Request.QueryString["History"]) == 0)
        {
            if (dt1.Rows.Count > 0)
            {
                this.CrystalReportViewer1.Visible = true;
                CrystalReportViewer1.ReportSource = rpt;
                rpt.Load(Server.MapPath("WageScaleCrystalReport.rpt"));
                rpt.SetDataSource(dt1);

                DataSet ds = cls_SearchReliever.getMasterData("WageScaleComponents", "WageScaleComponentId", "ComponentName");
                for (int w = 0; w < 12; w++)
                {
                    DataTable dtwc = Budget.getTable("select active from dbo.WageScaleRankdetails where wagescalerankid in (select WageScaleRankId from WageScaleRank where WageScaleId=" + wagescale + " and Seniority=" + seniority + " and NationalityId=0) and Wagescalecomponentid=" + (w + 1).ToString()).Tables[0];
                    if (dtwc.Rows.Count > 0)
                    {
                        if (dtwc.Rows[0][0].ToString() == "Y")
                            rpt.SetParameterValue("@W" + Convert.ToString(w + 1), ds.Tables[0].Rows[w][1].ToString());
                        else
                            rpt.SetParameterValue("@W" + Convert.ToString(w + 1), "");
                    }
                    else
                    {
                        rpt.SetParameterValue("@W" + Convert.ToString(w + 1), ds.Tables[0].Rows[w][1].ToString());
                    }
                }
                rpt.SetParameterValue("@Header", "Wage Scale :  " + wagescalename + "   Effective From :  " + effectivedate);

                DataTable dt = PrintCrewList.selectCompanyDetails();
                foreach (DataRow dr in dt.Rows)
                {
                    rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
                }
            }
            else
            {
                this.lblmessage.Text = "No Records Found.";
                this.CrystalReportViewer1.Visible = false;
            }
        }
        else
        {
            if (dt1.Rows.Count > 0)
            {
                this.CrystalReportViewer1.Visible = true;
                CrystalReportViewer1.ReportSource = rpt;
                rpt.Load(Server.MapPath("WageScaleCrystalReport.rpt"));
                rpt.SetDataSource(dt2);

                DataSet ds = cls_SearchReliever.getMasterData("WageScaleComponents", "WageScaleComponentId", "ComponentName");
                for (int w = 0; w < 12; w++)
                {
                    rpt.SetParameterValue("@W" + Convert.ToString(w + 1), ds.Tables[0].Rows[w][1].ToString());
                }
                rpt.SetParameterValue("@Header", "Wage Scale :  " + wagescalename + "   Effective From :  " +  effectivedate);

                DataTable dt = PrintCrewList.selectCompanyDetails();
                foreach (DataRow dr in dt.Rows)
                {
                    rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
                }
            }
            else
            {
                this.lblmessage.Text = "No Records Found.";
                this.CrystalReportViewer1.Visible = false;
            }
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
