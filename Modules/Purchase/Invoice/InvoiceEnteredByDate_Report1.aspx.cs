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

public partial class InvoiceEnteredByDate_Report11 : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        //int PaymentId = Common.CastAsInt32(Request.QueryString["PaymentId"].ToString());
        ShowInvoiceReport();
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        rpt.Close();
        rpt.Dispose();
    }
    
    protected void ShowInvoiceReport()
    {
        DataTable dt = new DataTable();

        int EnteredBy = Common.CastAsInt32(Request.QueryString["EnteredBy"]);
        int ProcessedBy = Common.CastAsInt32(Request.QueryString["ProcessedBy"]);
        int PaidBy = Common.CastAsInt32(Request.QueryString["PaidBy"]);
        int Stage = Common.CastAsInt32(Request.QueryString["Stage"]);
        

        //string FD = Request.QueryString["PeriodFrom"];
        //string TD = Request.QueryString["PeriodTo"];

        string RecFrom = Request.QueryString["RecFrom"];
        string RecTo = Request.QueryString["RecTo"];


        string Vendor = Request.QueryString["Vendor"];
        string OwnerId = "" + Request.QueryString["Owner"];
        string VesselId = "" + Request.QueryString["Vessel"];

        string Where = " Where 1=1 ";

        
        
            //Where += " AND StageText='Entry' ";
            if (EnteredBy > 0)
            {
                Where += " AND EntertedBy =" + EnteredBy;
            }

            if (ProcessedBy > 0)
            {
                Where += " AND ApprovalFwdTo =" + ProcessedBy;
            }
            if (PaidBy > 0)
            {
                Where += " AND PaidFwdTo =" + PaidBy;
            }

            if (RecFrom.Trim() != "")
                Where += " AND EnteredOn >='" + Convert.ToDateTime(RecFrom).ToString("dd-MMM-yyyy") + "' ";
            if (RecTo.Trim() != "")
                Where += " AND EnteredOn <'" + Convert.ToDateTime(RecTo).AddDays(1).ToString("dd-MMM-yyyy") + "' ";

            switch (Stage)
            {
            case 0:
                Where += " AND Stage=0";
                //if (FD.Trim() != "")
                //    Where += " AND EnteredOn >='" + Convert.ToDateTime(FD).ToString("dd-MMM-yyyy") + "' ";
                //if (TD.Trim() != "")
                //    Where += " AND EnteredOn <'" + Convert.ToDateTime(TD).AddDays(1).ToString("dd-MMM-yyyy") + "' ";
                break;
            case 1:
                    Where += " AND Stage=1";
                    //if (FD.Trim() != "")
                    //    Where += " AND EnteredOn >='" + Convert.ToDateTime(FD).ToString("dd-MMM-yyyy") + "' ";
                    //if (TD.Trim() != "")
                    //    Where += " AND EnteredOn <'" + Convert.ToDateTime(TD).AddDays(1).ToString("dd-MMM-yyyy") + "' ";
                    break;
            case 2:
                Where += " AND Stage=2";
                //if (FD.Trim() != "")
                //    Where += " AND VerficationOn >='" + Convert.ToDateTime(FD).ToString("dd-MMM-yyyy") + "' ";
                //if (TD.Trim() != "")
                //    Where += " AND VerficationOn <'" + Convert.ToDateTime(TD).AddDays(1).ToString("dd-MMM-yyyy") + "' ";
                break;
            case 3:
                    Where += " AND Stage=3";
                    //if (FD.Trim() != "")
                    //    Where += " AND VerficationOn >='" + Convert.ToDateTime(FD).ToString("dd-MMM-yyyy") + "' ";
                    //if (TD.Trim() != "")
                    //    Where += " AND VerficationOn <'" + Convert.ToDateTime(TD).AddDays(1).ToString("dd-MMM-yyyy") + "' ";
                    break;
            }


        if (Vendor.Trim() != "")
        {
            Where += " AND Vendor ='" + Vendor + "' ";
        }
        if (OwnerId!="")
        {
            Where += " AND COMPANY ='" + OwnerId + "'";
        }
        if (VesselId !="")
        {
            Where += " AND INVVesselCode ='" + VesselId + "'";
        }

        //string SQL = "SELECT * FROM vw_POS_Invoices_001 I ";
        //string SQL = " select I.* "+
        //             "   , b.BidPoNum,b.EstShippingUSD + (SELECT SUM(UsdPoTotal) FROM dbo.tblSMDPODETAILbid bids where bids.bidid = b.bidid) as Poamount " +
        //             "       from vw_POS_Invoices_001 I " +
        //             "   inner " +
        //             "       join POS_Invoice_Payment_PO p on i.InvoiceId = p.InvoiceId " +
        //             "   left " +
        //             "       join dbo.tblSMDPOMasterBid b on b.BidID = p.BidId ";

        string SQL = " select I.*,dbo.GET_PONO_List(I.invoiceid) AS PoList from vw_POS_Invoices_001 I  ";

        dt = Common.Execute_Procedures_Select_ByQuery(SQL + Where);

        CrystalReportViewer1.ReportSource = rpt;
        rpt.Load(Server.MapPath("InvoiceEnteredByDate_Report1.rpt"));
        rpt.SetDataSource(dt);
        //Response.Write(SQL + Where);
        rpt.SetParameterValue(0, "Invoice Reports ( " + Common.ToDateString(RecFrom) + " - " + Common.ToDateString(RecTo) + " )");
     
    }
}