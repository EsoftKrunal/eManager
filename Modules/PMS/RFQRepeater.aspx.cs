using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;

public partial class RFQRepeater : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            string subjobcode = Request.QueryString["JC"];
            int DocketId =Common.CastAsInt32(Request.QueryString["DocketId"]);

            DataTable dtRFQSubJobs_All = Common.Execute_Procedures_Select_ByQuery("SELECT RFQId,(SELECT YARDNAME FROM DD_YardMaster WHERE YARDID IN (SELECT dm.YARDID FROM DD_Docket_RFQ_Master dm where dm.rfqid=d.rfqid)) AS YardName,(SELECT RFQNO FROM DD_Docket_RFQ_Master M WHERE M.DOCKETID=" + DocketId + " AND M.RFQId=D.RFQId) AS RFQNO,DocketSubJobId,DocketJobId,SubJobCode,SubJobName,Unit,BidQty,SUPQty,SUPUnitPrice_USD,SUPDiscountPer,SUPNetAmount_USD,SUPRemarks,VendorRemarks FROM [dbo].[DD_Docket_RFQ_SubJobs] D WHERE DOCKETID=" + DocketId + " AND SubJobCode='" + subjobcode + "' AND D.RFQID IN ( SELECT M.RFQID FROM DD_Docket_RFQ_Master M WHERE M.ACTIVEINACTIVE='A' AND M.STATUS IN ('P','Q'))");
            if (dtRFQSubJobs_All.Rows.Count > 0)
            {
                lblJobCode.Text = dtRFQSubJobs_All.Rows[0]["SubJobCode"].ToString();
                lblJobName.Text = dtRFQSubJobs_All.Rows[0]["SubJobName"].ToString();
            }
            rptRFQs.DataSource = dtRFQSubJobs_All;
            rptRFQs.DataBind();

            StringBuilder sb = new StringBuilder();
            StringWriter tw = new StringWriter(sb);
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            pnlContent.RenderControl(hw);
            string html = sb.ToString();

            Response.Clear();
            Response.Write(html);
            Response.End();
        }
    }
}