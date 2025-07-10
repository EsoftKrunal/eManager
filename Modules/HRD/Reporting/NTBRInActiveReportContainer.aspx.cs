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
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using ShipSoft.CrewManager.Operational;

public partial class Reporting_NTBRInActiveReportContainer : System.Web.UI.Page
{
    int crewid = 0;
    int selindex = 0;
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //======
        //==== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 180);
        //==========
        string Type = Request.QueryString["Type"].ToString();
        string fdt= Request.QueryString["FromDate"].ToString();
        string tdt= Request.QueryString["ToDate"].ToString();

        string wclause = "";
        wclause = " Status='" + Type + "'";
        if (fdt.Trim()!="")
        {
            wclause = wclause + " And NTBR_InActiveDate>='" + fdt + "'";
        }
        if (tdt.Trim() != "")
        {
            wclause = wclause + " And NTBR_InActiveDate<='" + tdt + "'";
        }


        DataTable dt1 = Budget.getTable("select *,replace(convert(varchar, NTBR_InActiveDate, 106),' ','-') as 'NTBR-InActiveDate' from (select Row_Number() over (order by CrewID) as Sno, " +
                                        "CrewNumber,FirstName + ' ' + Middlename + ' ' + LastName as CrewName, " +
                                        "(select rankcode from rank where rank.rankid in (case when cpd.crewstatusid=1 then rankappliedid else currentrankid end)) as Rank, " +
                                        "(select vesselcode from vessel where vessel.vesselid=cpd.lastvesselid) as LastVessel, " +
                                        "(select CrewStatusName from CrewStatus where CrewStatus.CrewStatusid=cpd.crewStatusid) as Status, " +
                                        "( " +
                                        "case when cpd.crewstatusid=5 then (select top 1 NTBRDate from crewntbrdetails cnd where cnd.crewid=cpd.crewid) " +
                                        "else (select top 1 InActiveDate from crewInActivedetails cad where cad.crewid=cpd.crewid) " +
                                        "end " +
                                        ") as 'NTBR_InActiveDate', " +
                                        "( " +
                                        "select NTBRReasonName from NtBrReason n1 Where n1.NTBRReasonid IN " +
                                        "( " +
                                        "case when cpd.crewstatusid=5 then (select top 1 NTBRReasonId from crewntbrdetails cnd where cnd.crewid=cpd.crewid) " +
                                        "else(select top 1 InActiveReasonId from crewInActivedetails cad where cad.crewid=cpd.crewid order by InActiveId desc) " +
                                        "end " +
                                        ")) as 'NTBR-InActiveReason' " +
                                        "from crewpersonaldetails cpd where left(crewnumber,1)<>'F') a where" + wclause).Tables[0];
        DataTable dt = PrintCrewList.selectCompanyDetails();
        if (dt1.Rows.Count > 0)
        {
                     
            this.CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("NTBRInActiveReport.rpt"));

            rpt.SetDataSource(dt1);
            foreach (DataRow dr in dt.Rows)
            {
                rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
            }
        }
        else
        {
            this.CrystalReportViewer1.Visible = false;
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
