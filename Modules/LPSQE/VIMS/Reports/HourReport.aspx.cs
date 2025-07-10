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

public partial class Reports_HourReport : System.Web.UI.Page
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

        string sql = " select crewnumber + ' - ' + firstname + ' ' + middlename + ' ' + lastname as CrewName, "+
                     "   (select rankNAME from dbo.rank R where R.RankID = CP.CurrentRankID) as Rank, " +
                     "   (select VESSELNAME from dbo.VESSEL V where V.VESSELID = CP.CurrentVESSELID) as Vessel, " +
                      "  (select lrimonumber from dbo.VESSEL V where V.VESSELID = CP.CurrentVESSELID) as IMO, " +
                      "  WATCHKEEPER = '', " +
                      "  (SELECT FLAGSTATENAME FROM dbo.FLAGSTATE F WHERE F.FLAGSTATEID IN (select FLAGSTATEID from dbo.VESSEL V where V.VESSELID = CP.CurrentVESSELID )) AS FLAG " +
                      "  from dbo.crewpersonaldetails CP " +
                      "  WHERE CP.CrewNumber = '"+CrewNumber+"' ";

        sql = " select *,MP.RankName,'' as IMO,'' as WATCHKEEPER,'' as FLAG,(SELECT SHIPNAME FROM SETTINGS WHERE SHIPCODE=VESSELCODE) AS shipname from PMS_CREW_HISTORY CH left join MP_AllRank MP  "+
              "   on MP.Rankid = CH.Rankid  " +
              "   Where Crewnumber = '"+ CrewNumber + "' and ContractId="+ ContractID;

        CrystalReportViewer1.Visible = true;
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("EXEC DBO.RH_MonthlyWorkLog_Report '" + VesselCode + "','" + CrewNumber + "'," + month + "," + year );
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("HourReport.rpt"));
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
