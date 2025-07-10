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
using System.IO;

public partial class Docket_PO : System.Web.UI.Page
{
    public int DocketId
    {
        get { return Common.CastAsInt32(ViewState["DocketId"]); }
        set { ViewState["DocketId"] = value; }
    }
    public int JobCatId
    {
        set { ViewState["JobCategoryId"] = value; }
        get { return Common.CastAsInt32(ViewState["JobCategoryId"]); }
    }
    public int RFQId
    {
        get { return Common.CastAsInt32(ViewState["RFQId"]); }
        set { ViewState["RFQId"] = value; }
    }
    //-------------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsgMain.Text = "";
        lbl_MsgRemarks.Text = "";
        if (!IsPostBack)
        {
            RFQId = Common.CastAsInt32(Request.QueryString["RFQId"]);
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DD_Docket_RFQ_Master WHERE RFQId=" + RFQId );
            if (dt != null && dt.Rows.Count > 0)
            {
                DocketId = Common.CastAsInt32(dt.Rows[0]["DocketId"]);
                lblRFQNo.Text = dt.Rows[0]["RFQNo"].ToString();
                BindCurrency();
                LoadCategory();
                ShowDocketSummary();
            }
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "dfsd", "alert('Invalid Quote#.');window.close();", true);
            //}
        }
    }
    protected void LoadCategory()
    {
        DataTable dtGroups = new DataTable();
        string strSQL = "SELECT CatId,CatCode,CatName FROM DD_JobCategory Order By CatCode";
        dtGroups = Common.Execute_Procedures_Select_ByQuery(strSQL);
        rptJobCats.DataSource = dtGroups;
        rptJobCats.DataBind();
    }
    protected void btnSelectCat_Click(object sender, EventArgs e)
    {
        JobCatId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        LoadCategory();
        BindJobs();
    }
    
    public void BindCurrency()
    {
        DataTable dtCurr = Common.Execute_Procedures_Select_ByQuery("SELECT DISTINCT FOR_CURR FROM dbo.XCHANGEDAILY ORDER BY FOR_CURR");
        ddlCurrency.DataSource = dtCurr;
        ddlCurrency.DataTextField = "FOR_CURR";
        ddlCurrency.DataValueField = "FOR_CURR";
        ddlCurrency.DataBind();
        ddlCurrency.Items.Insert(0, new ListItem("< Select >", ""));
    }
    public void BindJobs()
    {
        DataTable dtJobs = Common.Execute_Procedures_Select_ByQuery("SELECT DocketJobId,JobId,JOBCODE,JOBNAME FROM DD_Docket_RFQ_Jobs WHERE RFQId=" + RFQId + " AND DOCKETID=" + DocketId + " And CatId=" + JobCatId + " ORDER BY JOBCODE");
        rptJobs.DataSource = dtJobs;
        rptJobs.DataBind();
    }
    public DataTable BindSubJobs(Object DocketJobId)
    {
        return Common.Execute_Procedures_Select_ByQuery("SELECT RFQId,[DocketSubJobId],[DocketJobId],[DocketId],[SubJobCode],[SubJobName],AttachmentName,BidQty,Unit,POQty,[UnitPrice],POGrossAmount, PODiscountPer,PONetAmount,VendorRemarks FROM [dbo].[DD_Docket_RFQ_SubJobs] WHERE RFQId=" + RFQId + " AND DOCKETID=" + DocketId + " And DocketJobId=" + DocketJobId + " ORDER BY SubJobCode");
    }
    protected void imgDownload_Click(object sender, EventArgs e)
    {
        int DocketJobId =Common.CastAsInt32(((ImageButton)sender).Attributes["DocketJobId"]);
        int DocketSubJobId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);

        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT AttachmentName,Attachment FROM DD_DocketSubJobs WHERE DOCKETID=" + DocketId.ToString() + " AND DOCKETJOBID=" + DocketJobId.ToString() + " AND DOCKETSUBJOBID=" + DocketSubJobId.ToString());
        byte[] buff = (byte[])dt.Rows[0]["Attachment"];
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + dt.Rows[0]["AttachmentName"].ToString());
        Response.BinaryWrite(buff);
        Response.Flush();
        Response.End();
    }
    protected void btnDocketView_Click(object sender, EventArgs e)
    {
        int TableId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT TOP 1 VersionNo,Attachment FROM DD_Docket_Publish_History WHERE DOCKETID=" + DocketId + " ORDER BY TABLEID DESC");
        if (dt.Rows.Count > 0)
        {
            string FileName = lblDocketNo.Text.Replace("/", "-") + "-" + dt.Rows[0]["VersionNo"].ToString() + ".pdf";
            byte[] buff = (byte[])dt.Rows[0]["Attachment"];
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
            Response.BinaryWrite(buff);
            Response.Flush();
            Response.End();
        }

    }
    public void ShowDocketSummary()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT *,(SELECT VESSELNAME FROM DBO.VESSEL V WHERE V.VESSELID=D.VESSELID) AS VESSELNAME FROM DD_DocketMaster D WHERE DOCKETID=" + DocketId.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            lblDocketNo.Text = dt.Rows[0]["DocketNo"].ToString();
            lblVessel.Text = dt.Rows[0]["VESSELNAME"].ToString();
            lblType.Text = dt.Rows[0]["DocketType"].ToString();
            lblPlanDuration.Text = Common.ToDateString(dt.Rows[0]["StartDate"]) + " To " + Common.ToDateString(dt.Rows[0]["EndDate"]);
        }
        
        DataTable dtRFQ = Common.Execute_Procedures_Select_ByQuery("SELECT ISNULL(CURRENCY,''),EXCHRATE FROM DD_Docket_RFQ_Master WHERE RFQID=" + RFQId.ToString());
        if (dtRFQ != null && dtRFQ.Rows.Count > 0)
        {
            ddlCurrency.SelectedValue = dtRFQ.Rows[0][0].ToString();
            ddlCurrency.Enabled = false; 
            lblExchRate.Text = dtRFQ.Rows[0][1].ToString();
        }
        
        DataTable dtTotal = Common.Execute_Procedures_Select_ByQuery("SELECT ISNULL(SUM(PONetAmount),0) FROM DD_Docket_RFQ_SubJobs WHERE RFQID=" + RFQId + " AND DOCKETID=" + DocketId);
        lblTotalAmount.Text = dtTotal.Rows[0][0].ToString();
    }  
    //protected void ddlCurrency_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    Common.Set_Procedures("DD_UpdateCurrency");
    //    Common.Set_ParameterLength(2);
    //    Common.Set_Parameters(
    //       new MyParameter("@RFQId", RFQId),
    //       new MyParameter("@CURR", ddlCurrency.SelectedValue));
    //    DataSet ds = new DataSet();
    //    Boolean res;
    //    res = Common.Execute_Procedures_IUD(ds);
    //    if (res)
    //    {
    //        lblMsgMain.Text = "Currency Update Sucessfully.";
    //    }
    //    else
    //    {
    //        lblMsgMain.Text = "Unable to update Currency. Error: " + Common.ErrMsg; ;
    //    }
    //}
 }
