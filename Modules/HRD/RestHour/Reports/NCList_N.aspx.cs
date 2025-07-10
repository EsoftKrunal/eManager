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

public partial class NCList_N : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    public int ContractID
    {
        get { return Common.CastAsInt32(ViewState["_ContractID"]); }
        set { ViewState["_ContractID"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        //SessionManager.SessionCheck_New();
        //-----------------------------
        string VesselCode = Request.QueryString["v"];
        string CrewNumber = Request.QueryString["c"];
        string month = Request.QueryString["m"];
        string year = Request.QueryString["y"];
        ContractID = Common.CastAsInt32(Request.QueryString["CID"]);

        string MonthFor = Convert.ToDateTime(year + "-" + month + "-01").ToString("MMM yyyy");

        string ContractId = Request.QueryString["cid"];
        CrystalReportViewer1.Visible = true;
        string sql="";
        DataTable dt;
        
        sql= "SELECT VESSELCODE,CrewNumber,FORDATE as NCDate,DATELINE,NCId,Remarks as Reason,NCTypeName FROM DBO.RH_NCList WHERE VesselCode='" + VesselCode + "' and  CrewNumber='" + CrewNumber + "' AND MONTH(ForDate)=" + month + " AND YEAR(ForDate)=" + year + " ORDER BY ForDate";

        dt = Common.Execute_Procedures_Select_ByQuery(sql);
        //----------
	sql = " select cp.crewNumber,FIRSTNAME + ' ' + MIDDLENAME + ' ' + LASTNAME AS crewname,ch.signondate,ch.SingOffDate,r.RankName,v.VesselName,(Select top 1 CompanyName  from Company where StatusId='A' order by CompanyId desc ) As CompanyName   " +
                     "   from DBO.get_Crew_History ch " +
                     "   inner join DBO.crewpersonalDetails cp on cp.crewid = ch.CrewID " +
                     "   inner join DBO.rank r on r.rankid = ch.NewRankId " +
		     "   inner join DBO.vessel v on v.vesselid=ch.vesselid "  +
                     "   where CrewNumber = '" + CrewNumber + "' and ch.ContractID =" + ContractID;

        DataTable DtCrew = Common.Execute_Procedures_Select_ByQuery(sql); 
        CrystalReportViewer1.ReportSource = rpt;
         rpt.Load(Server.MapPath("NCReport_N.rpt"));
        rpt.SetDataSource(dt);
        rpt.SetParameterValue("Rank", DtCrew.Rows[0]["RankName"]);
        rpt.SetParameterValue("Vessel", DtCrew.Rows[0]["VesselName"]);
        rpt.SetParameterValue("CrewNumber", DtCrew.Rows[0]["CrewNumber"]);
        rpt.SetParameterValue("CrewName", DtCrew.Rows[0]["CrewName"]);
        rpt.SetParameterValue("OnDate", MonthFor);
        //rpt.SetParameterValue("CompanyName", DtCrew.Rows[0]["CompanyName"]);
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
