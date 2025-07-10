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

public partial class Reports_CriticalShutdownRequest : System.Web.UI.Page
{
    public string VesselCode
    {
        set { ViewState["VC"] = value; }
        get { return ViewState["VC"].ToString(); }
    }

    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------

        if (Request.QueryString["SD"] != null)
        {
            VesselCode = Request.QueryString["VC"].ToString();
            ShowReport();   
        }
        
    }

    public void ShowReport()
    {
        string strSQL = "SELECT * FROM vw_CriticalShutdownRequestData WHERE VESSELCODE='" + VesselCode + "' AND ShutdownId= " + Request.QueryString["SD"].ToString() + " ";
        string strSQL1 = "SELECT * FROM vw_CriticalShutdownExtensionDetails WHERE VESSELCODE='" + VesselCode + "' AND ShutdownId= " + Request.QueryString["SD"].ToString() + " ";

        DataTable dtSDR = Common.Execute_Procedures_Select_ByQuery(strSQL);
        DataTable dtSDR1 = Common.Execute_Procedures_Select_ByQuery(strSQL1);
        
        DataSet ds = new DataSet();
        ds.Tables.Add(dtSDR.Clone());
        ds.Tables[0].TableName ="vw_CriticalShutdownRequestData" ; 
        foreach(DataRow dr in  dtSDR.Rows)
        {
            ds.Tables["vw_CriticalShutdownRequestData"].ImportRow(dr);
        }
        ds.Tables.Add(dtSDR1.Clone());
        ds.Tables[1].TableName = "vw_CriticalShutdownExtensionDetails"; 
        foreach(DataRow dr in  dtSDR1.Rows)
        {
            ds.Tables["vw_CriticalShutdownExtensionDetails"].ImportRow(dr);
        }

        string strVessel = "";
        if (Session["UserType"].ToString() == "O")
        {
            strVessel = "SELECT VesselName FROM Vessel WHERE VesselCode=(SELECT VesselCode FROM dbo.vw_CriticalShutdownRequestData WHERE VESSELCODE='" + VesselCode + "' AND ShutdownId= " + Request.QueryString["SD"].ToString() + ")";
        }
        else
        {
            strVessel = "SELECT ShipName AS VesselName FROM Settings WHERE ShipCode =(SELECT VesselCode FROM dbo.vw_CriticalShutdownRequestData WHERE VESSELCODE='" + VesselCode + "' AND ShutdownId= " + Request.QueryString["SD"].ToString() + ")";
        }        
        DataTable dtvessel = Common.Execute_Procedures_Select_ByQuery(strVessel);
        CrystalReportViewer1.ReportSource = rpt;  
        rpt.Load(Server.MapPath("CriticalShutdownReport.rpt"));
        rpt.SetDataSource(ds);
        rpt.SetParameterValue("Header", "Critical Equipment Shutdown Request");
        rpt.SetParameterValue("VesselName", dtvessel.Rows[0][0].ToString());
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}