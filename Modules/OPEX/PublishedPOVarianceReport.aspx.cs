using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Caching;

public partial class PublishedPOVarianceReport : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        ShowReportBySP();
    }
    protected void btnPrint_OnClick(object sender, EventArgs e)
    {
        
    }
    public void ShowReportBySP()
    {

        string filter = "";
        string Fleet = Request.QueryString["Fleet"];
        string Cocode=Request.QueryString["Company"];
        string ShipCode = Request.QueryString["VesselID"];
        
        
        //DataTable Dt_Ship=Common.Execute_Procedures_Select_ByQueryCMS("SELECT VESSELNAME FROM VESSEL WHERE VESSELCODE='" + ShipCode + "'");
        //if (Dt_Ship.Rows.Count > 0)
        //{
        //    ShipName = Dt_Ship.Rows[0][0].ToString();   
        //}

        int ForMonth = Common.CastAsInt32(Request.QueryString["ForMonth"]);
        int ForYear = Common.CastAsInt32(Request.QueryString["ForYear"]);
        DateTime FROMDATE, TODATE;
        FROMDATE = new DateTime(ForYear, ForMonth,1);
        TODATE = FROMDATE.AddMonths(1);
        filter = FROMDATE.ToString("MMM yyyy");

        string ShipList="";
        if (ShipCode.Trim() != "") // for single ship
        {
            ShipList = "'" + ShipCode + "'";
        }
        else if (Fleet.Trim() != "") // for a fleet
        {
            DataTable dtShips = null;
            try
            {
                Common.Execute_Procedures_Select_ByQuery("SELECT vesselcode FROM dbo.vessel where fleetid='" + Fleet + "'");
            }
            catch { }
            if (dtShips == null)
            {
                dtShips = Common.Execute_Procedures_Select_ByQuery("SELECT vesselcode FROM dbo.vessel where fleetid='" + Fleet + "'");
            }
            foreach (DataRow dr in dtShips.Rows)
            {
                ShipList += ",'" + dr[0].ToString() + "'";
            }
            if (ShipList.Trim() == "")
                ShipList = "''";
            else
                ShipList = ShipList.Substring(1);
        }
        else if (Cocode.Trim() != "") // for a company
        {
            string sql = "SELECT vsl.ShipID FROM vw_sql_tblSMDPRVessels vsl WHERE (((vsl.Active)='A') and vsl.Company='" + Cocode + "'";
            DataTable dtShips = Common.Execute_Procedures_Select_ByQuery(sql);
            foreach (DataRow dr in dtShips.Rows)
            {
                ShipList += ",'" + dr[0].ToString() + "'";
            }
            if (ShipList.Trim() == "")
                ShipList = "''";
            else
                ShipList = ShipList.Substring(1);

        }
        string sql_1 = "select * from vw_PUBLISHEDPOCOMMITMENT WHERE SHIPID IN (" + ShipList + ") AND RYEAR=" + ForYear.ToString() + " AND RMONTH=" + ForMonth.ToString();

        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Query", sql_1));
        DataSet dtFilteredRecord = Common.Execute_Procedures_Select();
        dtFilteredRecord.Tables[0].TableName = "sp_NEW_VariancePOReport;1";
        if (dtFilteredRecord != null)
        {
            dtFilteredRecord.Tables[0].TableName = "sp_NEW_VariancePOReport.rpt";
            CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("~/Report/PublishedPOCommitment.rpt"));
            rpt.SetDataSource(dtFilteredRecord.Tables[0]);
            rpt.SetParameterValue("Filter", filter);
        }
    }
    #region PageEvents
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    #endregion
}

