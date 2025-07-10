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

public partial class Invoice_PaymentVoucher : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        int PaymentId = Common.CastAsInt32(Request.QueryString["PaymentId"].ToString());
        // string PVNO = Request.QueryString["PVNO"].ToString();
        string PaymentMode = Request.QueryString["PaymentMode"].ToString();
        //if (PaymentMode == "O")
        //{
            ShowInvoiceReport(PaymentId);
       // }
        //else
        //{
        //    ShowInvoiceReport_N(PaymentId);
        //}
    }

    // Functions -----------------------------------------------------------------------------------------------
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    protected void ShowInvoiceReport(int PaymentId)
    {
        string pvno = "";
        string sql = "Select PVNO from POS_Invoice_Payment with(nolock) where [PaymentId] =" + PaymentId;
        DataTable dtPV = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dtPV.Rows.Count > 0)
        {
            pvno = dtPV.Rows[0][0].ToString();
        }
        if (pvno != "")
        {
            string SQL = "SELECT * FROM vw_POS_Invoice_Print WHERE [PVNo] ='" + pvno.ToString() + "'";
            DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);
            if (dt.Rows.Count > 0)
            {
                DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("SELECT ACCOUNTNUMBER FROM [dbo].[VW_sql_tblSMDPRAccounts]  WHERE ACCOUNTID IN (SELECT ACCOUNTID FROM [dbo].[VW_tblSMDPOMaster] WHERE POID IN (SELECT POID FROM VW_TBLSMDPOMASTERBID WHERE BIDPONUM='" + dt.Rows[0]["PONO"] + "'))");
                if (dt1.Rows.Count > 0)
                {
                    dt.Rows[0]["AccountCode"] = dt1.Rows[0][0];
                }


                CrystalReportViewer1.ReportSource = rpt;
                rpt.Load(Server.MapPath("PaymentVoucher.rpt"));
                rpt.SetDataSource(dt);

                rpt.Refresh();
            }
        }
       

        

    }
    //protected void ShowInvoiceReport_N(int PaymentId)
    //{
    //    string SQL = "SELECT * FROM POS_Payment_print WHERE [PaymentId] =" + PaymentId + " ORDER BY PaymentId,SRNO ";
    //    DataTable dt = Common.Execute_Procedures_Select_ByQuery(SQL);

    //    //DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("SELECT ACCOUNTNUMBER FROM [dbo].[VW_sql_tblSMDPRAccounts]  WHERE ACCOUNTID IN (SELECT ACCOUNTID FROM [dbo].[VW_tblSMDPOMaster] WHERE POID IN (SELECT POID FROM VW_TBLSMDPOMASTERBID WHERE BIDPONUM='" + dt.Rows[0]["PONO"] + "'))");
    //    //if (dt1.Rows.Count > 0)
    //    //{
    //    //    dt.Rows[0]["AccountCode"] = dt1.Rows[0][0];
    //    //}

    //    CrystalReportViewer1.ReportSource = rpt;
    //    if (dt.Rows.Count > 0)
    //    {
    //        rpt.Load(Server.MapPath("PaymentVoucher_N.rpt"));
    //        rpt.SetDataSource(dt);

    //        if (dt.Rows[0]["cURRENCY"].ToString() == "INR")
    //            rpt.SetParameterValue("HeaderName", "M.T.M. SHIP MANAGEMENT PVT. LTD.");
    //        else
    //            rpt.SetParameterValue("HeaderName", "M.T.M. SHIP MANAGEMENT PTE. LTD.");
        
            
    //    }   
    //}
    
    // Events -----------------------------------------------------------------------------------------------
}