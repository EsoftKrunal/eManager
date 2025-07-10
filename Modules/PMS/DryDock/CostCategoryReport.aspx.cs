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

public partial class DryDock_CostCategoryReport : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        
        ShowReport();
    }
    protected void ShowReport()
    {
        string Type =  Request.QueryString["Type"].ToString();
        int RFQId = Common.CastAsInt32(Request.QueryString["RFQId"]);
        int Docketd = Common.CastAsInt32(Request.QueryString["DocketId"]);

        string WHERE = "WHERE RFQId=" + RFQId + " AND DocketId=" + Docketd + " ";

        if (Type.Trim() == "YC")
        {
            WHERE = WHERE + " AND ISNULL(CostCategory, 'Y')= 'Y' ";
        }
        else
        {
            WHERE = WHERE + " AND ISNULL(CostCategory, 'Y')= 'N' ";
        }


        string strSQL = "SELECT * FROM vw_GetCostCategoryReportData  " + WHERE + "  Order By SubJobCode ";
        string strSQLTotal = "SELECT * FROM vw_GetCostCategoryReportData  " + WHERE + "  Order By SubJobCode ";

        DataTable dtReport = Common.Execute_Procedures_Select_ByQuery(strSQL);
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("CostCategoryReport.rpt"));
        rpt.SetDataSource(dtReport);

        decimal POAmount = 0, PO_YARD = 0, PO_OWNER = 0, EstUSd = 0, YardCost = 0, OwnerCost = 0, FinalDisc = 0;

        DataTable dt111 = Common.Execute_Procedures_Select_ByQuery("SELECT ISNULL(SUM(case when isnull(costcategory,'N')='Y' THEN EstAmount_USD ELSE 0 END),0) as EstAmount_USD_TotalYC, ISNULL(SUM(case when isnull(costcategory,'N')='N' THEN EstAmount_USD ELSE 0 END),0) as EstAmount_USD_TotalNY,SUM(EstAmount_USD) AS TOTAL_EST,SUM(SUPNetAmount_USD) AS TOTAL_PO,ISNULL(SUM(case when isnull(costcategory,'N')='Y' THEN SUPNetAmount_USD ELSE 0 END),0) AS YARD_PO,ISNULL(SUM(case when isnull(costcategory,'N')='N' THEN SUPNetAmount_USD ELSE 0 END),0) AS OWNER_PO  FROM DD_Docket_RFQ_SubJobs WHERE RFQID=" + RFQId);
        if (dt111.Rows.Count > 0)
        {
            POAmount = Common.CastAsDecimal(dt111.Rows[0]["TOTAL_PO"]);
            PO_YARD = Common.CastAsDecimal(dt111.Rows[0]["YARD_PO"]);
            PO_OWNER = Common.CastAsDecimal(dt111.Rows[0]["OWNER_PO"]);

            EstUSd = Common.CastAsDecimal(dt111.Rows[0]["TOTAL_EST"]);
            YardCost = Common.CastAsDecimal(dt111.Rows[0]["EstAmount_USD_TotalYC"]);
            OwnerCost = Common.CastAsDecimal(dt111.Rows[0]["EstAmount_USD_TotalNY"]);
            DataTable DtDisc = Common.Execute_Procedures_Select_ByQuery("SELECT TOP 1 FinalDiscount FROM DD_Docket_RFQ_Master MM WHERE MM.RFQID=" + RFQId);
            FinalDisc = Common.CastAsDecimal(DtDisc.Rows[0]["FinalDiscount"]);
        }

        rpt.SetParameterValue("Title", ((Type.Trim() == "YC" ? " Shipyard Supply Costs Report " : " Owner’s Supply Shipyard Costs Report ")));

        rpt.SetParameterValue("POAmount", POAmount);
        rpt.SetParameterValue("Budget_Yard", PO_YARD);
        rpt.SetParameterValue("Budget_Owner", PO_OWNER);

        rpt.SetParameterValue("TotalEstAmount", EstUSd);
        rpt.SetParameterValue("NonOwnerCost", YardCost);
        rpt.SetParameterValue("OwnerCost", OwnerCost);
        rpt.SetParameterValue("FinalDiscount", FinalDisc);
        rpt.SetParameterValue("FinalYardCosts", YardCost-FinalDisc);

        rpt.SetParameterValue("FinalCost", YardCost - FinalDisc + OwnerCost);
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}