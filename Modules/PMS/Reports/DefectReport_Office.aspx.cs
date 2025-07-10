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

public partial class Reports_DefectReport_Office : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------

        if (Session["UserType"].ToString() == "O")
        {
            if (Session["SQLDefectReport_Office"] != null)
            {
                ShowReport();
            }
        }
        else
        {
            if (Session["SQLDefectReport_Ship"] != null)
            {
                ShowReport();
            }
        }
    }

    public void ShowReport()
    {
        string strSQL = "";

        if (Session["UserType"].ToString() == "O")
        {
            strSQL = Session["SQLDefectReport_Office"].ToString();
        }
        else
        {
            strSQL = Session["SQLDefectReport_Ship"].ToString();
        }

        DataTable dtDR_O = Common.Execute_Procedures_Select_ByQuery(strSQL);

        CrystalReportViewer1.ReportSource = rpt;
        if (Session["UserType"].ToString() == "O")
        {
            rpt.Load(Server.MapPath("DefectReport_Office.rpt"));
        }
        else
        {
            rpt.Load(Server.MapPath("DefectReport_Ship.rpt"));
        }

        rpt.SetDataSource(dtDR_O);
        rpt.SetParameterValue("Header", "Consolidated Defect Records");

        
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}