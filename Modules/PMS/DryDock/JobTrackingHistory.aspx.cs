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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Text;

public partial class Docket_JobTrackingHistory : System.Web.UI.Page
{
    public int DocketId
    {
        get { return Common.CastAsInt32(ViewState["DocketId"]); }
        set { ViewState["DocketId"] = value; }
    }
    public int RFQId
    {
        get { return Common.CastAsInt32(ViewState["RFQId"]); }
        set { ViewState["RFQId"] = value; }
    }
    public int CatId
    {
        set { ViewState["CatId"] = value; }
        get { return Common.CastAsInt32(ViewState["CatId"]); }
    }
    public int DocketJobId
    {
        set { ViewState["DocketJobId"] = value; }
        get { return Common.CastAsInt32(ViewState["DocketJobId"]); }
    }
    public int LoginId
    {
        get { return Common.CastAsInt32(Session["loginid"]); }
    }
    //-------------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        //ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------
        if (LoginId <= 0)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "af", "alert('Session Expired.'); window.close();", true);
            //return;
        }
        if (!IsPostBack)
        {
            DocketId = Common.CastAsInt32(Request.QueryString["DocketId"]);
            RFQId = Common.CastAsInt32(Request.QueryString["RFQId"]);
            CatId = Common.CastAsInt32(Request.QueryString["CatId"]);
            DocketJobId = Common.CastAsInt32(Request.QueryString["DocketJobId"]); ;
            ShowDetails();
            LoadHistory();
        }
    }
    protected void ShowDetails()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT *,(SELECT VESSELNAME FROM DBO.VESSEL V WHERE V.VESSELID=D.VESSELID) AS VESSELNAME,ISNULL((SELECT R.RFQID FROM DD_Docket_RFQ_Master R WHERE R.DOCKETID=D.DOCKETID AND R.STATUS='P'),0) AS PORFQId,(SELECT ExecFrom FROM DD_Docket_RFQ_Master Where DOCKETID=" + DocketId + " AND RFQId=" + RFQId + ") As ExecFrom,(SELECT ExecTo FROM DD_Docket_RFQ_Master Where DOCKETID=" + DocketId + " AND RFQId=" + RFQId + ") As ExecTo FROM DD_DocketMaster D WHERE DOCKETID=" + DocketId.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            lblDocketNo.Text = dt.Rows[0]["DocketNo"].ToString();
            lblVessel.Text = dt.Rows[0]["VESSELNAME"].ToString();
            lblType.Text = dt.Rows[0]["DocketType"].ToString();
            lblPlanDuration.Text = Common.ToDateString(dt.Rows[0]["ExecFrom"]) + " To " + Common.ToDateString(dt.Rows[0]["ExecTo"]);
        }
        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("SELECT CATNAME, (SELECT JOBNAME from [dbo].[DD_Docket_RFQ_Jobs] WHERE RFQID=" + RFQId +" AND DOCKETID=" + DocketId + " AND CATID=" + CatId +"  AND DOCKETJOBID=" + DocketJobId +") AS JOBNAME from [dbo].[DD_Docket_RFQ_Jobs] WHERE RFQID=" + RFQId + " AND DOCKETID=" + DocketId + " AND CATID=" + CatId);
        if (dt1.Rows.Count > 0)
        {
            lblJobCatName.Text = dt1.Rows[0]["CATNAME"].ToString();
            lblJobName.Text = dt1.Rows[0]["JOBNAME"].ToString();
        }
    }
    protected void LoadHistory()
    {
        string sql = "select * from [dbo].[DD_Docket_RFQ_Jobs_Planning] WHERE RFQID=" + RFQId + " AND DOCKETJOBID=" + DocketJobId + " AND DOCKETID=" + DocketId;
        RPT1.DataSource = Common.Execute_Procedures_Select_ByQuery(sql);
        RPT1.DataBind();

        //DateTime StartDate=new DateTime();
        //DateTime EndDate = new DateTime();
        //DateTime Tmp = new DateTime();
        //DataTable dtRFQ = Common.Execute_Procedures_Select_ByQuery("SELECT ExecFrom,ExecTo FROM DD_Docket_RFQ_Master Where RFQId=" + RFQId);
        //if (dtRFQ.Rows.Count > 0)
        //{
        //    StartDate = Convert.ToDateTime(dtRFQ.Rows[0]["ExecFrom"]);
        //    EndDate = Convert.ToDateTime(dtRFQ.Rows[0]["ExecTo"]);
        //}

        //int ScrollWidth = EndDate.Subtract(StartDate).Days * 50;

        //StringBuilder sbleft = new StringBuilder();
        //StringBuilder sbdata = new StringBuilder();

        //DataTable dtDates = Common.Execute_Procedures_Select_ByQuery("select FOR_DATE,DATES FROM [dbo].[DD_Docket_RFQ_Jobs_Planning_History] WHERE RFQID=" + RFQId + " AND DOCKETID=" + DocketId + " AND DOCKETJOBID=" + DocketJobId);
        ////----------------------------------------
        //StringBuilder sb_Left=new StringBuilder();
        
        //sb_Left.Append("<table style='border-collapse:collapse;' cellpadding='0' cellspacing='0' border='1' bordercolor='#c2c2c2' width='100%'>");
        //foreach (DataRow dr in dtDates.Rows)
        //{
        //    sb_Left.Append("<tr><td class='tdleft'>");
        //    sb_Left.Append( Common.ToDateString(dr["FOR_DATE"]));
        //    sb_Left.Append("</td></tr>");
        //}
        //sb_Left.Append("</table>");
        
        //litHead.Text = sb_Left.ToString();
        ////----------------------------------------
        //StringBuilder sb_RightHeader=new StringBuilder();
        //sb_RightHeader.Append("<div style='width:" + (ScrollWidth + 50).ToString() + "px'>");
        //sb_RightHeader.Append("<table style='border-collapse:collapse;' cellpadding='0' cellspacing='0' border='1' bordercolor='#c2c2c2' width='" + ScrollWidth + "px'>");
        //Tmp = StartDate;
        //while (Tmp <= EndDate)
        //{
        //    sb_RightHeader.Append("<td class='tdDataHeadercell'>");
        //    sb_RightHeader.Append(Tmp.ToString("dd-MMM"));
        //    sb_RightHeader.Append("</tr>");

        //    Tmp = Tmp.AddDays(1);
        //}
        //sb_RightHeader.Append("</table>");
        //sb_RightHeader.Append("</div>");
        //lit_Dates.Text = sb_RightHeader.ToString();
        ////----------------------------------------
        //StringBuilder sb_Data = new StringBuilder();
        //sb_Data.Append("<div style='width:" + (ScrollWidth + 50).ToString() + "px'>");
        //sb_Data.Append("<table style='border-collapse:collapse;' cellpadding='0' cellspacing='0' border='1' bordercolor='#c2c2c2' width='" + ScrollWidth + "px'>");
        //foreach (DataRow dr in dtDates.Rows)
        //{
        //    sb_Data.Append("<tr>");
        //    Tmp = StartDate;
        //    while (Tmp <= EndDate)
        //    {
        //        //tdDatacellP
        //        string classname = "tdDatacell";
        //        if (dr["DATES"].ToString().Trim().Contains(Tmp.ToString("dd-MMM-yyyy")))
        //            classname = "tdDatacellP";

        //        sb_Data.Append("<td class='" + classname +"'>");
        //        sb_Data.Append("&nbsp;");
        //        sb_Data.Append("</td>");

        //        Tmp = Tmp.AddDays(1);
        //    }
        //    sb_Data.Append("</tr>");
        //}
        //sb_Data.Append("</table>");
        //sb_Data.Append("</div>");
        //litData.Text = sb_Data.ToString();
        
    }
 }
