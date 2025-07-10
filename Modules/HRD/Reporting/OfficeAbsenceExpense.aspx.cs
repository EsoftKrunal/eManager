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

public partial class Reporting_OfficeAbsenceExpense : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    public int BizTravelId
    {
        get
        {
            return Common.CastAsInt32(ViewState["BizTravelId"].ToString());
        }
        set
        {
            ViewState["BizTravelId"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (Request.QueryString["id"] != null || Request.QueryString["id"].ToString() != "")
        {
            BizTravelId = Common.CastAsInt32(Request.QueryString["id"].ToString());
            ShowReport();
        }

    }

    public void ShowReport()
    {
        DataTable dt = new DataTable();
        string SQL = "SELECT * FROM dbo.vw_BizTravel_ExpensesData WHERE BizTravelId=" + BizTravelId.ToString() + "";
        dt = Common.Execute_Procedures_Select_ByQueryCMS(SQL);
        dt.TableName = "vw_BizTravel_ExpensesData";
        
        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("../Reporting/BizTravelExpense.rpt"));
        rpt.SetDataSource(dt);

        decimal CashAdv, CashExp, balAmount, GrossExp;
        DataTable dtCashAdv = Common.Execute_Procedures_Select_ByQueryCMS("select sum( CASE WHEN RECORDTYPE='G' THEN CASHADVANCE ELSE -CASHADVANCE END) from HR_OfficeAbsence_CashAdvance where BizTravelId=" + BizTravelId.ToString() + "");

        CashAdv = Common.CastAsDecimal(dtCashAdv.Rows[0][0]);
        CashExp = Common.CastAsDecimal(dt.Compute("SUM(EXPENSE)", "EXPTYPE='CASH Expense'"));
        balAmount=CashExp -CashAdv;
        GrossExp=Common.CastAsDecimal(dt.Compute("SUM(EXPENSE)", ""));

        rpt.SetParameterValue("@Header", "Expense Claim Statement");
        rpt.SetParameterValue("CashAdv", CashAdv);
        rpt.SetParameterValue("CashExp", CashExp);
        rpt.SetParameterValue("BalAmount",balAmount);

        if (balAmount > 0)
        {
            rpt.SetParameterValue("Remarks", "Pay to Employee SGD " + string.Format("{0:0.00}", Math.Abs(balAmount)) );
        }
        if (balAmount < 0)
        {
            rpt.SetParameterValue("Remarks", "Deduct from Employee SGD " + string.Format("{0:0.00}",Math.Abs(balAmount)) );
        }
        if (balAmount==0)
        {
            rpt.SetParameterValue("Remarks", " NA ");
        }
        rpt.SetParameterValue("GrossExp", GrossExp);

    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
}