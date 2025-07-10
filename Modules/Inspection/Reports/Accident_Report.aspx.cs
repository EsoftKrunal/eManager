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

public partial class Reports_Accident_Report : System.Web.UI.Page
{
    int intAccidentId = 0;
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //------------------------------------
        ProjectCommon.SessionCheck_New();
        //------------------------------------
        lblmessage.Text = "";
        if (Page.Request.QueryString["AccidentId"].ToString() != "")
            intAccidentId = int.Parse(Page.Request.QueryString["AccidentId"].ToString());
        if (!Page.IsPostBack)
            Show_Report();
        else
            Show_Report();
    }
    protected void Show_Report()
    {
        DataTable dt = Accident_Form.GetAccidentDetailsById(intAccidentId);
        DataTable dt1 = Accident_Form.SelectAccidentImmCauseDetails(intAccidentId);
        DataTable dt2 = Accident_Form.SelectAccidentRootCauseDetails(intAccidentId);
        DataTable dt3 = Budget.getTable("SELECT FORMNUMBER,FORMNAME,ISSUEDBY,APPROVEDBY,VERSIONNUMBER,REPLACE(CONVERT(VARCHAR,ISSUEDATE,106),' ','-') AS ISSUEDATE FROM TBL_FORMID WHERE FORMID=2").Tables[0];
        DataTable dt4 = Accident_Form.SelectAccidentCategoryDetails(intAccidentId);
        DataTable dt5 = Accident_Form.SelectAccidentSeverityDetails(intAccidentId);
        DataTable dt6 = Accident_Form.SelectInjuryCategoryDetails(intAccidentId);
        DataTable dt7 = Accident_Form.SelectOfficeCategoryDetails(intAccidentId);
        if (dt.Rows.Count > 0)
        {
            this.CrystalReportViewer1.Visible = true;

            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("RPT_Accident.rpt"));

            rpt.SetDataSource(dt);

            rpt.Refresh();
            this.CrystalReportViewer1.Visible = true;
            CrystalDecisions.CrystalReports.Engine.ReportDocument rptsub1 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            rptsub1 = rpt.OpenSubreport("RPT_Accident_ImmCause.rpt");
            rptsub1.SetDataSource(dt1);

            rpt.Refresh();
            this.CrystalReportViewer1.Visible = true;
            CrystalDecisions.CrystalReports.Engine.ReportDocument rptsub2 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            rptsub2 = rpt.OpenSubreport("RPT_Accident_RootCause.rpt");
            rptsub2.SetDataSource(dt2);

            rpt.Refresh();
            this.CrystalReportViewer1.Visible = true;
            CrystalDecisions.CrystalReports.Engine.ReportDocument rptsub3 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            rptsub3 = rpt.OpenSubreport("RPT_Accident_Category.rpt");
            rptsub3.SetDataSource(dt4);

            rpt.Refresh();
            this.CrystalReportViewer1.Visible = true;
            CrystalDecisions.CrystalReports.Engine.ReportDocument rptsub4 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            rptsub4 = rpt.OpenSubreport("RPT_Accident_Severity.rpt");
            rptsub4.SetDataSource(dt5);

            rpt.Refresh();
            this.CrystalReportViewer1.Visible = true;
            CrystalDecisions.CrystalReports.Engine.ReportDocument rptsub5 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            rptsub5 = rpt.OpenSubreport("RPT_Accident_InjCat.rpt");
            rptsub5.SetDataSource(dt6);

            rpt.Refresh();
            this.CrystalReportViewer1.Visible = true;
            CrystalDecisions.CrystalReports.Engine.ReportDocument rptsub6 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            rptsub6 = rpt.OpenSubreport("RPT_Accident_OffCat.rpt");
            rptsub6.SetDataSource(dt7);

            if (dt3.Rows.Count > 0)
            {
                rpt.SetParameterValue("@Header", dt3.Rows[0]["FORMNAME"].ToString());
                rpt.SetParameterValue("@FormNo", " " + dt3.Rows[0]["FORMNUMBER"].ToString());
                rpt.SetParameterValue("@IssuedBy", " " + dt3.Rows[0]["ISSUEDBY"].ToString());
                rpt.SetParameterValue("@ApprovedBy", " " + dt3.Rows[0]["APPROVEDBY"].ToString());
                rpt.SetParameterValue("@VerNo", " " + dt3.Rows[0]["VERSIONNUMBER"].ToString());
                rpt.SetParameterValue("@IssueDate", " " + dt3.Rows[0]["ISSUEDATE"].ToString());
            }
            else
            {
                rpt.SetParameterValue("@Header", "ACCIDENT REPORT");
                rpt.SetParameterValue("@FormNo", "");
                rpt.SetParameterValue("@IssuedBy", "");
                rpt.SetParameterValue("@ApprovedBy", "");
                rpt.SetParameterValue("@VerNo", "");
                rpt.SetParameterValue("@IssueDate", "");
            }
            rpt.SetParameterValue("@ReportedBy", dt.Rows[0]["ReportedByCrewNumber"].ToString() + " - " + dt.Rows[0]["ReportedByFirstName"].ToString() + " " + dt.Rows[0]["ReportedByFamilyName"].ToString());
            rpt.SetParameterValue("@Injured", dt.Rows[0]["InjuredCrewNumber"].ToString() + " - " + dt.Rows[0]["InjuredFirstName"].ToString() + " " + dt.Rows[0]["InjuredFamilyName"].ToString());
            char[] c = { ',' };
            Array a = dt.Rows[0]["AccidentCategory"].ToString().Split(c);
            for (int r = 0; r < a.Length; r++)
            {
                if (a.GetValue(r).ToString() == "1")
                {
                    rpt.SetParameterValue("@PersonInjured", "Personnel Involved (injured) in Accident (Applicable):");
                    return;
                }
                else
                    rpt.SetParameterValue("@PersonInjured", "Personnel Involved (injured) in Accident (Not Applicable):");
            }            
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
