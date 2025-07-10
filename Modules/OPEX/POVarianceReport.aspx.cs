using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.Caching;
using System.Data.SqlClient;
using System.Configuration;

public partial class POVarianceReport : System.Web.UI.Page
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
        string Cocode=Request.QueryString["Company"];
        string ShipCode = Request.QueryString["VesselID"];
        string ShipName = "";
        DataTable Dt_Ship=Common.Execute_Procedures_Select_ByQueryCMS("SELECT VESSELNAME FROM VESSEL WHERE VESSELCODE='" + ShipCode + "' and VesselId in (Select VesselId from UserVesselRelation with(nolock) where Loginid = "+ Convert.ToInt32(Session["loginid"].ToString())+")");
        if (Dt_Ship.Rows.Count > 0)
        {
            ShipName = Dt_Ship.Rows[0][0].ToString();   
        }
        int ForMonth = Common.CastAsInt32(Request.QueryString["ForMonth"]);
        string ForYear = Request.QueryString["ForYear"].ToString();
        //DateTime FROMDATE, TODATE;
       // FROMDATE = new DateTime(ForYear, ForMonth,1);
       // TODATE = FROMDATE.AddMonths(1);
       // filter = FROMDATE.ToString("MMM yyyy");

        //string sql = "mtmm2000sql.dbo.SP_NEW_GetPOReportData '" + Page.Request.QueryString["Company"].ToString() + "'" +
        //    ",'" + FROMDATE.ToString("MM/dd/yyyy") + "'" +
        //    ",'" + TODATE.ToString("MM/dd/yyyy") + "'" +
        //    ",'" + ShipCode + "',0,0,0,9999";
        //Common.Set_Procedures("ExecQuery");
        //Common.Set_ParameterLength(1);
        //Common.Set_Parameters(new MyParameter("@Query", sql));
        //DataSet dtFilteredRecord = Common.Execute_Procedures_Select();
        //if (dtFilteredRecord != null)
        //{
        //    CrystalReportViewer1.Visible = true;
        //    CrystalReportViewer1.ReportSource = rpt;
        //    rpt.Load(Server.MapPath("~/Report/VariancePOReport.rpt"));
        //    //dtFilteredRecord.TableName = "vw_getPoReportData";
        //    rpt.SetDataSource(dtFilteredRecord.Tables[0]);
        //    //rpt.Subreports[0].SetDataSource(dtFilteredRecord.Tables[1]);
        //    rpt.SetParameterValue("VSLName", ShipName);
        //    rpt.SetParameterValue("Filter", filter);


        string sql = "dbo.SP_NEW_VariancePOReport '" + Page.Request.QueryString["Company"].ToString() + "'" +
         ",'" + Request.QueryString["VesselID"].ToString() + "'" +
         "," + ForMonth + "" +
         ",'" + ForYear.ToString() + "'," + Request.QueryString["Mode"].ToString();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["eMANAGER"].ConnectionString + ";Connection Timeout=900;");
        SqlDataAdapter adp = new SqlDataAdapter();
        con.Open();
        SqlCommand cmd = new SqlCommand("", con);
        cmd.CommandTimeout = 300;
        cmd.CommandText = sql;
        cmd.CommandType = CommandType.Text;
        adp.SelectCommand = cmd;
        DataTable dtFilteredRecord = new DataTable();
        adp.Fill(dtFilteredRecord);
        if (dtFilteredRecord != null)
        {
            CrystalReportViewer1.Visible = true;
            CrystalReportViewer1.ReportSource = rpt;
            rpt.Load(Server.MapPath("~/Modules/OPEX/Report/VariancePOReport1.rpt"));
            rpt.SetDataSource(dtFilteredRecord);
            rpt.SetParameterValue("VSLName", ShipName);
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

