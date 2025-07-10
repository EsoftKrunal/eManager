using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Configuration;

public partial class InventoryReport001 : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------

        Common.Set_Procedures("EXECQUERY");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Query", "dbo.GET_INVETORYDATA '" + Request.QueryString["VESSELCODE"] + "','" + Request.QueryString["FORDATE"] + "','Y'"));
        DataSet ds = Common.Execute_Procedures_Select();
        rpt.Load(Server.MapPath("~/Report/PrintInventory.rpt"));
        CrystalReportViewer1.ReportSource = rpt;
        rpt.SetDataSource(ds.Tables[0]);

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT VESSELNAME FROM DBO.VESSEL WHERE VESSELCODE='" + Request.QueryString["VESSELCODE"].ToString() + "'");
        if(dt.Rows.Count>0)
            rpt.SetParameterValue("VesselName", "[ " + dt.Rows[0][0].ToString() + " ] - " + Convert.ToDateTime(Request.QueryString["FORDATE"]).ToString("MMM-yyyy"));
        rpt.SetParameterValue("MonthYear", "");
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}


