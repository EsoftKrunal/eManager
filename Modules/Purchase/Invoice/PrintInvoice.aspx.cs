using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Invoice_PrintInvoice : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        int InvoiceId =Common.CastAsInt32(Request.QueryString["InvoiceId"]);
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM vw_POS_Invoice_Print1 WHERE INVOICEID=" + InvoiceId);
        dt1.TableName = "vw_POS_Invoice_Print";
        DataTable dt2 = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM vw_Invoice_PoList WHERE INVOICEID=" + InvoiceId);
        dt2.TableName = "vw_Invoice_PoList";
        DataTable dt3 = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM vw_POS_INVOICE_Stages WHERE INVOICEID=" + InvoiceId);
        dt3.TableName = "vw_POS_INVOICE_Stages";
        DataTable dt4 = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM VW_POS_INVOICE_APPROVAL WHERE INVOICEID=" + InvoiceId);
        dt4.TableName = "VW_POS_INVOICE_APPROVAL";

        DataSet ds = new DataSet();
        ds.Tables.Add(dt1.Copy());
        ds.Tables.Add(dt2.Copy());
        ds.Tables.Add(dt3.Copy());
        ds.Tables.Add(dt4.Copy());

        rpt.Load(Server.MapPath("~/Modules/Purchase/Invoice/PrintInvoice.rpt"));
        CrystalReportViewer1.ReportSource = rpt;
        
        rpt.SetDataSource(ds);
        rpt.Subreports[0].SetDataSource(dt4);
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }  
}