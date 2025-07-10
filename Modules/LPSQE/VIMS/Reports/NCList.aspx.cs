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

public partial class NCList : System.Web.UI.Page
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
        //----------
        //if(ContractId!=null)
        //{
        //    DataTable dtCl=Common.Execute_Procedures_Select_ByQuery("SELECT * FROM CP_VESSELCREWLIST WHERE CREWID=" + crewid + " AND VESSELID=" + vessel + " AND CONTRACTID=" + ContractId); 
        //    if(dtCl.Rows.Count>0)
        //    {
        //        string stdt =Convert.ToDateTime(dtCl.Rows[0]["SignOnDate"]).ToString("dd-MMM-yyyy");
        //        sql = "SELECT * FROM VW_CP_NCLIST WHERE CREWID=" + crewid + " AND NCDATE>='" + stdt + "' AND NCDATE<='" + DateTime.Today.ToString("dd-MMM-yyyy") + "' ORDER BY NCDATE";
        //    }
        //    else
        //    {
        //        sql="SELECT * FROM VW_CP_NCLIST WHERE CREWID=" + crewid + " AND MONTH(NCDATE)=" + month + " AND YEAR(NCDATE)=" + year + " ORDER BY NCDATE";
        //    }
        //}
        //else
          sql= "SELECT VESSELCODE,CrewNumber,FORDATE as NCDate,DATELINE,NCId,Remarks as Reason,NCTypeName FROM RH_NCList WHERE VesselCode='" + VesselCode + "' and  CrewNumber='" + CrewNumber + "' AND MONTH(ForDate)=" + month + " AND YEAR(ForDate)=" + year + " ORDER BY ForDate";

        dt = Common.Execute_Procedures_Select_ByQuery(sql);
        //----------
        sql = " select *,MP.RankName from PMS_CREW_HISTORY CH left join Rank MP  on MP.Rankid=CH.Rankid where VesselCode='"+VesselCode+"' and CrewNumber='"+CrewNumber+ "'  and ContractId="+ContractID+" order by CH.Rankid,CH.CrewName ";

        DataTable DtCrew = Common.Execute_Procedures_Select_ByQuery(sql); 
        CrystalReportViewer1.ReportSource = rpt;
         rpt.Load(Server.MapPath("NCReport.rpt"));
        rpt.SetDataSource(dt);
        rpt.SetParameterValue("Rank", DtCrew.Rows[0]["RankName"]);
        rpt.SetParameterValue("Vessel", DtCrew.Rows[0]["VesselCode"]);
        rpt.SetParameterValue("CrewNumber", DtCrew.Rows[0]["CrewNumber"]);
        rpt.SetParameterValue("CrewName", DtCrew.Rows[0]["CrewName"]);
        rpt.SetParameterValue("OnDate", MonthFor);
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
