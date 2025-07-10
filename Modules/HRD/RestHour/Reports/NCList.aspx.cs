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
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        string vessel = Request.QueryString["v"];
        string crewid = Request.QueryString["c"];
        string month = Request.QueryString["m"];
        string year = Request.QueryString["y"];

        string ContractId = Request.QueryString["cid"];
        CrystalReportViewer1.Visible = true;
        string sql="";
        DataTable dt;
        //----------
        if(ContractId!=null)
        {
            DataTable dtCl=Common.Execute_Procedures_Select_ByQueryCMS("SELECT * FROM CP_VESSELCREWLIST WHERE CREWID=" + crewid + " AND VESSELID=" + vessel + " AND CONTRACTID=" + ContractId); 
            if(dtCl.Rows.Count>0)
            {
                string stdt =Convert.ToDateTime(dtCl.Rows[0]["SignOnDate"]).ToString("dd-MMM-yyyy");
                sql = "SELECT * FROM VW_CP_NCLIST WHERE CREWID=" + crewid + " AND NCDATE>='" + stdt + "' AND NCDATE<='" + DateTime.Today.ToString("dd-MMM-yyyy") + "' ORDER BY NCDATE";
            }
            else
            {
                sql="SELECT * FROM VW_CP_NCLIST WHERE CREWID=" + crewid + " AND MONTH(NCDATE)=" + month + " AND YEAR(NCDATE)=" + year + " ORDER BY NCDATE";
            }
        }
        else
            sql="SELECT * FROM VW_CP_NCLIST WHERE CREWID=" + crewid + " AND MONTH(NCDATE)=" + month + " AND YEAR(NCDATE)=" + year + " ORDER BY NCDATE";

        dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        //----------
        DataTable DtCrew = Common.Execute_Procedures_Select_ByQueryCMS("SELECT CREWNUMBER,FIRSTNAME + ' ' + MIDDLENAME + ' ' + LASTNAME AS CREWNAME, (SELECT RANKNAME FROM RANK WHERE RANKID=CPD.CURRENTRANKID) AS RANK, ISNULL((SELECT VESSELNAME FROM VESSEL WHERE VESSELID=CPD.CURRENTVESSELID),'') AS VESSEL, (Select top 1 CompanyName  from Company where StatusId='A' order by CompanyId desc ) As CompanyName FROM CREWPERSONALDETAILS CPD WHERE CPD.CREWID=" + crewid); 
        CrystalReportViewer1.ReportSource = rpt;
         rpt.Load(Server.MapPath("NCReport.rpt"));
        rpt.SetDataSource(dt);
        rpt.SetParameterValue("Rank", DtCrew.Rows[0]["Rank"]);
        rpt.SetParameterValue("Vessel", DtCrew.Rows[0]["Vessel"]);
        rpt.SetParameterValue("CrewNumber", DtCrew.Rows[0]["CrewNumber"]);
        rpt.SetParameterValue("CrewName", DtCrew.Rows[0]["CrewName"]);
        rpt.SetParameterValue("OnDate",DateTime.Today);
        rpt.SetParameterValue("CompanyName", DtCrew.Rows[0]["CompanyName"]);
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
