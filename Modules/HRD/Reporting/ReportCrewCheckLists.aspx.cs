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

public partial class Reporting_ReportCrewCheckLists : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()),19);
        //==========
        int CrewId, ContractId, Vesselid;
        CrewId =Convert.ToInt32(Request.QueryString["CrewId"]);
        Vesselid =Convert.ToInt32(Request.QueryString["Vesselid"]);
        ContractId = Convert.ToInt32(Request.QueryString["Contractid"]);
        DataSet ds = new DataSet();
        DataTable dt = ReportCheckList.selectCrewCheckListHeader(CrewId,Vesselid,ContractId);
        DataTable dt1 = ReportCheckList.selectCrewCheckListDetails(ContractId);
        DataTable dt2 = PrintCrewList.selectCompanyDetails();
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("ReportCrewCheckList.rpt"));

        dt.TableName = "get_Check_List_Header;1";
        dt1.TableName = "get_Check_List_Details;1";
        ds.Tables.Add(dt);
        ds.Tables.Add(dt1);
        rpt.SetDataSource(ds);
        

        foreach (DataRow dr in dt2.Rows)
        {
            rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
