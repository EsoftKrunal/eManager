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

public partial class Reporting_CrewAvalibilityReportContainer : System.Web.UI.Page
{
    int crewid = 0;
    int selindex = 0;
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        //========== Code to check report printing authority
        CrystalReportViewer1.HasPrintButton = Alerts.Check_ReportPrintingAuthority(Convert.ToInt32(Session["loginid"].ToString()), 15);
        //==========
        DataTable dt1 = PrintCrewList.selectCompanyDetails();
        string filter = " 1=1";
        filter = filter + ((("" + Request.QueryString["RecOff"]) != "0") ? " And cpd.RecruitmentofficeId=" + Request.QueryString["RecOff"]: "");
        filter = filter + ((("" + Request.QueryString["fdt"]) != "") ? " And AvailableFrom >='" + Request.QueryString["fdt"] + "'" : "");
        filter = filter + ((("" + Request.QueryString["tdt"]) != "") ? " And AvailableFrom <='" + Request.QueryString["tdt"] + "'" : "");
        filter = filter + ((("" + Request.QueryString["status"]) != "0") ? " And CrewStatusId =" + Request.QueryString["status"] : " And CrewStatusId in (1,2,3)");

        DataTable dt10 = Budget.getTable("select Row_Number() over (order by RankLevel) as SNo, " +
                                            "CrewNumber,firstname + ' ' + middlename + ' ' + lastname as CrewName,RankCode, " +
                                            "isnull((select vesselcode from vessel v where v.vesselid=cpd.lastvesselid),'') as LastVessel, " +
                                            "isnull((select vesselcode from vessel v where v.vesselid=cpd.currentvesselid),'') as CurrentVessel, " +
                                            "replace(convert(varchar,AvailableFrom,106),' ','-') as AvalDate,isnull(AvalRemark,'') as AvalRem " +
                                            "from crewpersonaldetails cpd inner join rank on cpd.currentrankid=rank.rankid Where" + filter + " Order By RankLevel").Tables[0];
        if (dt10.Rows.Count > 0)
        {
            CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("CrewAvalRep.rpt"));
            rpt.SetDataSource(dt10);
            foreach (DataRow dr in dt1.Rows)
            {
                rpt.SetParameterValue("@Company", dr["CompanyName"].ToString());
            }
            DataTable dt4 = AllotmentRequestClas.selectUserDetails(Convert.ToInt32(Session["loginid"].ToString()));

            rpt.SetParameterValue("@HeaderText", "Crew Availibility Report for :" + Session["header"].ToString() );
        }
        else
        {
            CrystalReportViewer1.Visible = false;
        }
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
