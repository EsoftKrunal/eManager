using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Reports_HourReport_N : System.Web.UI.Page
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

        string ContractId = Request.QueryString["cid"];

        //string sql = " select crewnumber + ' - ' + firstname + ' ' + middlename + ' ' + lastname as CrewName, "+
        //             "   (select rankNAME from DBO.rank R where R.RankID = CP.CurrentRankID) as Rank, " +
        //             "   (select VESSELNAME from DBO.VESSEL V where V.VESSELID = CP.CurrentVESSELID) as Vessel, " +
        //              "  (select lrimonumber from DBO.VESSEL V where V.VESSELID = CP.CurrentVESSELID) as IMO, " +
        //              "  WATCHKEEPER = '', " +
        //              "  (SELECT FLAGSTATENAME FROM DBO.FLAGSTATE F WHERE F.FLAGSTATEID IN (select FLAGSTATEID from DBO.VESSEL V where V.VESSELID = CP.CurrentVESSELID )) AS FLAG " +
        //              "  from DBO.crewpersonaldetails CP " +
        //              "  WHERE CP.CrewNumber = '"+CrewNumber+"' ";

        string sql = " select v.vesselcode,cp.crewid,ch.ContractId,cp.crewNumber,cp.dateofbirth as dob,cp.DateFirstJoin djc,FIRSTNAME + ' ' + MIDDLENAME + ' ' + LASTNAME AS crewname,r.rankid,ch.signondate,ch.SingOffDate,cp.ReliefDueDate,r.rankcode,r.RankName, " +
        " ISNULL(v.lrimonumber,'') as IMO,CASE WHEN Isnull(VC.WatchKeeper,0) = 1 then 'Yes' Else 'No' END  as watchkeeper,c.CountryName as flag,VesselName as shipname " +
        " from DBO.get_Crew_History ch " +
        "inner join DBO.crewpersonalDetails cp on cp.crewid = ch.CrewID " +
        " inner join DBO.rank r on r.rankid = ch.NewRankId "+
        " inner join DBO.vessel v on v.VesselId = ch.VesselId " +
        " inner join DBO.Country c on v.FlagStateId = c.CountryId " +
        " Left join VesselCrewList VC on ch.CrewId = VC.CrewId and ch.ContractId = VC.ContractId and v.VesselCode = VC.VesselCode " + 
        " Where cp.Crewnumber = '" + CrewNumber + "' and ch.ContractId="+ ContractID;

        CrystalReportViewer1.Visible = true;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("EXEC DBO.RH_MonthlyWorkLog_Report '" + VesselCode + "','" + CrewNumber + "'," + month + "," + year );
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("HourReport_N.rpt"));
        rpt.SetDataSource(dt);

        DataTable dtCrew = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dtCrew.Rows.Count > 0)
        {
            DataRow dr = dtCrew.Rows[0];
            rpt.SetParameterValue("VNAME", dr["shipname"].ToString());
            rpt.SetParameterValue("IMO", dr["IMO"].ToString());
            rpt.SetParameterValue("FLAG", dr["FLAG"].ToString());
            rpt.SetParameterValue("CREWNAME", dr["CREWNAME"].ToString());
            rpt.SetParameterValue("RANK", dr["RANKName"].ToString());
            rpt.SetParameterValue("WATCHKEEPER", dr["WATCHKEEPER"].ToString());
            rpt.SetParameterValue("PERIOD", new DateTime(Common.CastAsInt32(year),Common.CastAsInt32(month),1).ToString("MMM - yyyy"));  
        }



        //ActiveReport rpt = new NewActiveReport1();
        //try
        //{
        //    rpt.Run(false);
        //}
        //catch (DataDynamics.ActiveReports.ReportException eRunReport)
        //{
        //    // Failure running report, just report the error to the user:
        //    Response.Clear();
        //    Response.Write("<h1>Error running report:</h1>");
        //    Response.Write(eRunReport.ToString());
        //    return;
        //}		
        //Response.ContentType = "application/pdf";

        //// IE & Acrobat seam to require "content-disposition" header being in the response.  If you don't add it, the doc still works most of the time, but not always.
        ////this makes a new window appear: Response.AddHeader("content-disposition","attachment; filename=MyPDF.PDF");
        //Response.AddHeader("content-disposition", "inline; filename=MyPDF.PDF");

        //// Create the PDF export object
        //PdfExport pdf = new PdfExport();
        //// Create a new memory stream that will hold the pdf output
        //System.IO.MemoryStream memStream = new System.IO.MemoryStream();
        //// Export the report to PDF:
        //pdf.Export(rpt.Document, memStream);
        //// Write the PDF stream out
        //Response.BinaryWrite(memStream.ToArray());
        //// Send all buffered content to the client
        //Response.End();
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}
