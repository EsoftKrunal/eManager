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

public partial class Reporting_PrintCrewListCrystal : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    public int flag
    {
        get { return Common.CastAsInt32(ViewState["flag"]); }
        set { ViewState["flag"] = value; }
    }
    public int vesselId
    {
        get { return Common.CastAsInt32(ViewState["vesselId"]); }
        set { ViewState["vesselId"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()),15);
        //==========
        flag = Convert.ToInt32(Request.QueryString["flag"]);
        vesselId = Convert.ToInt32(Request.QueryString["VesselId"]);
        showdata(vesselId,flag);
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    private void showdata(int VesselId, int Flag)
    {
        DataTable dt1;
        DataTable dt2;

        //DataTable dt = PrintCrewList.selectCompanyDetails();
        
        dt1 = PrintCrewList.selectCrewListDetailsforImoVessel(VesselId, Flag);
        string sql = "Select v.VesselName,v.LRIMONumber,(Select f.FlagStateName from FlagState f with(nolock) where f.FlagStateId = v.FlagStateId) As FlagStateName , CallSign from Vessel v with(nolock) where VesselId = " + VesselId;

        dt2 = Common.Execute_Procedures_Select_ByQueryCMS(sql);

        if (dt1.Rows.Count > 0)
        {
            this.CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("PrintCrewListForImoVessel.rpt"));
            rpt.SetDataSource(dt1);
            rpt.SetParameterValue(0, Convert.ToInt32(vesselId));
            rpt.SetParameterValue(1, Convert.ToInt32(flag));
            foreach (DataRow dr in dt2.Rows)
            {
                rpt.SetParameterValue("@VesselName", dr["VesselName"].ToString());
                rpt.SetParameterValue("@LRIMONumber", dr["LRIMONumber"].ToString());
                rpt.SetParameterValue("@FlagStateName", dr["FlagStateName"].ToString());
                rpt.SetParameterValue("@CallSign", dr["CallSign"].ToString());
               
            }
            //if(Fd=="" && Td=="")
            //{
            //    rpt.SetParameterValue("@Header","Crew List as On : " + Convert.ToString(DateTime.Now.Date.ToString("dd-MMM-yyyy")));
            //}
            //else
            //{
            //    rpt.SetParameterValue("@Header","Crew List From : " + Fd + " - " + Td);
            //}
            //foreach (DataRow dr in dt2.Rows)
            //{
            //    rpt.SetParameterValue("@ReturningCrew", dr["ReturningCrew"].ToString());
            //    rpt.SetParameterValue("@FamiliarCrew", dr["FamiliarCrew"].ToString());
            //}
        }
       
    }
}
