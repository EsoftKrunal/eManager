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

public partial class CrewOnVessel : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 99);
        CrystalReportViewer2.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 99);
        //==========
      
        ShowData();
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    public void ShowData()
    {
        DataSet ds = new DataSet();
        int vesselId;
        string fd,td;
        DataTable dt, dt2;
        this.CrystalReportViewer1.Visible = true;
        vesselId = Convert.ToInt32(Request.QueryString["VID"]);
        fd=Request.QueryString["FD"];
        td=Request.QueryString["TD"];
        CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("CrewOnVessel.rpt"));
        dt = cls_CrewOnVessel.get_HeaderDetails(vesselId);
        dt2 = cls_CrewOnVessel.get_Details(vesselId, fd, td);
        dt.TableName = "ReportCrewListWithWages_Header;1";
        dt2.TableName = "ReportCrewListWithWages;1";
        ds.Tables.Add(dt);
        ds.Tables.Add(dt2);

        rpt.SetDataSource(ds);

        DataTable dt1 = PrintCrewList.selectCompanyDetails();
        foreach (DataRow dr in dt1.Rows)
        {
            rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
        }
        rpt.SetParameterValue("@HeaderText", "Crew List with Wages From " + Request.QueryString["FD"] + " - " + Request.QueryString["TD"]);
        for (int i = 1; i <= 12; i++)
        {
            rpt.SetParameterValue("@C" + i.ToString(), cls_CrewOnVessel.get_WageScaleComponentName(i));
        }
        //DataSet ds1 = new DataSet();
        //int vesselId1;
        //DataTable dt3, dt4;
        //string fd1, td1;
        //this.CrystalReportViewer2.Visible = true;
        //vesselId1 = Convert.ToInt32(Request.QueryString["VID"]);
        //fd1 = Request.QueryString["FD"];
        //td1 = Request.QueryString["TD"];
        //CrystalDecisions.CrystalReports.Engine.ReportDocument rpt2 = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        //CrystalReportViewer2.ReportSource = rpt2;
        //rpt2.Load(Server.MapPath("CrewOnVesselSubReport.rpt"));

        //dt4 = cls_CrewOnVessel.get_Details(vesselId1, fd1, td1);
        
        //dt4.TableName = "ReportCrewListWithWages;1";
       
        //ds1.Tables.Add(dt4);

        //rpt2.SetDataSource(ds1);

        //for (int j = 7; j <= 12; j++)
        //{
        //    rpt2.SetParameterValue("@C" + j.ToString(), cls_CrewOnVessel.get_WageScaleComponentName(j));
        //}
    }
}
