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
using System.IO;
using Ionic.Zip;
using System.Text;

public partial class DryDock_SupRFQExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        ShowReport();
    }
    protected void ShowReport()
    {
        DataTable dtReport = Common.Execute_Procedures_Select_ByQuery("select CostCategory,CATCODE, CATNAME,JOBCODE,JOBNAME, SUBJOBCODE, SUBJOBNAME,Unit,BidQty,QuoteQty,UnitPrice,DiscountPer,NetAmount,vendorremarks,SUPRemarks from [dbo].[VW_DD_RFQ_SUP_PRINT] WHERE rFQID=" + Request.QueryString["RFQID"].ToString());
        StringBuilder sb = new StringBuilder();
        sb.Append("<table border=1>");
        sb.Append("<tr>");
        sb.Append("<td>CostCategory</td>");
        sb.Append("<td>Cat Code</td>");
        sb.Append("<td>Cat Name</td>");
        sb.Append("<td>Job Code</td>");
        sb.Append("<td>Job Name</td>");
        sb.Append("<td>Sub Job Code</td>");
        sb.Append("<td>Sub Job Name</td>");
        sb.Append("<td>Unit</td>");
        sb.Append("<td>Bid Qty</td>");
        sb.Append("<td>SUP Qty</td>");
        sb.Append("<td>UnitPrice</td>");
        sb.Append("<td>Discount(%)</td>");
        sb.Append("<td>NetAmount</td>");
        sb.Append("<td>VendorRemarks</td>");
        sb.Append("<td>Sup. Remarks</td>");
        sb.Append("</tr>");

        

        foreach ( DataRow dr in dtReport.Rows)
        {
            sb.Append("<tr>");
            foreach (DataColumn drc in dtReport.Columns)
            {
                sb.Append("<td>" + dr[drc].ToString() + "</td>");
            }
            sb.Append("</tr>");
        }
        sb.Append("</table>");

        Response.Clear();
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.xls";
        Response.AddHeader("content-disposition", "attachment;filename=download.xls");
        Response.Write(sb.ToString());
        Response.End();
    }
   
}
