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

public partial class EntryLog : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    public string CrewNumber
    {
        set
        {
            ViewState["CrewNumber"] = value;
        }
        get
        {
            return Convert.ToString( ViewState["CrewNumber"]);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        string CrewNumber = Request.QueryString["c"].ToString();
        string month = Request.QueryString["m"];
        string year = Request.QueryString["y"];

        CrystalReportViewer1.Visible = true;
        DataTable dtVessName = Common.Execute_Procedures_Select_ByQuery("Select VesselName from dbo.CP_Settings");
        string VesselName = "";
        if (dtVessName.Rows.Count > 0)
            VesselName = dtVessName.Rows[0][0].ToString();

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("EXEC DBO.sp_CP_GETLOGREPORT '" + CrewNumber + "'," + month + "," + year);
        string sql = "select CrewNumber,CrewName,(select rankNAME from cp_rank R where R.RankID=CP.RankID) as Rank,(select VESSELNAME from CP_SETTINGS)as Vessel,(select lrimonumber from CP_SETTINGS)as IMO,WATCHKEEPER= CASE WHEN ISNULL(WATCHKEEPER,0) =0 THEN 'No' else 'Yes' end, (SELECT FLAG FROM CP_SETTINGS) AS FLAG from CP_VesselCrewList CP WHERE CP.CREWNUMBER='" + CrewNumber + "'";
        DataTable DtCrew = Common.Execute_Procedures_Select_ByQuery(sql); 
        CrystalReportViewer1.ReportSource = rpt;
         rpt.Load(Server.MapPath("EntryLog.rpt"));
        rpt.SetDataSource(dt);
        rpt.SetParameterValue("Rank", DtCrew.Rows[0]["Rank"]);
        rpt.SetParameterValue("Vessel", VesselName);
        rpt.SetParameterValue("CrewNumber", DtCrew.Rows[0]["CrewNumber"]);
        rpt.SetParameterValue("CrewName", DtCrew.Rows[0]["CrewName"]);
        rpt.SetParameterValue("OnDate",DateTime.Today);
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
