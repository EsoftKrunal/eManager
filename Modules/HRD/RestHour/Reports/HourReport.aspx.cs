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

        string sql = "select crewnumber + ' - ' + firstname + ' ' + middlename + ' ' + lastname as CrewName,(select rankNAME from rank R where R.RankID=CP.CurrentRankID) as Rank,(select VESSELNAME from VESSEL V where V.VESSELID=CP.CurrentVESSELID) as Vessel,(select lrimonumber from VESSEL V where V.VESSELID=CP.CurrentVESSELID) as IMO,WATCHKEEPER= CASE WHEN ISNULL((SELECT WATCHKEEPER FROM CP_VESSELCREWLIST VC WHERE VC.CONTRACTID=" + ContractId + " AND VC.CREWID=CP.CREWID AND VC.VESSELID=CP.CurrentVESSELID),0) =0 THEN 'No' else 'Yes' end,(SELECT FLAGSTATENAME FROM FLAGSTATE F WHERE F.FLAGSTATEID IN (select FLAGSTATEID from VESSEL V where V.VESSELID=CP.CurrentVESSELID )) AS FLAG from crewpersonaldetails CP WHERE CP.CREWID=" + crewid + "";
        CrystalReportViewer1.Visible = true;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("EXEC DBO.sp_CP_GETDAILYWORKHRSMONTHLY " + vessel + "," + crewid + "," + month + "," + year );
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("HourReport.rpt"));
        rpt.SetDataSource(dt);

        DataTable dtCrew = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dtCrew.Rows.Count > 0)
        {
            DataRow dr = dtCrew.Rows[0];
            rpt.SetParameterValue("VNAME", dr["VESSEL"].ToString());
            rpt.SetParameterValue("IMO", dr["IMO"].ToString());
            rpt.SetParameterValue("FLAG", dr["FLAG"].ToString());
            rpt.SetParameterValue("CREWNAME", dr["CREWNAME"].ToString());
            rpt.SetParameterValue("RANK", dr["RANK"].ToString());
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
