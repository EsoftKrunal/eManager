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
using Ionic.Zip;

public partial class Docket_Quote : System.Web.UI.Page
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
    public bool CanEdit
    {
        get { return Convert.ToBoolean(ViewState["CanEdit"]); }
        set { ViewState["CanEdit"] = value; }
    }
    //-------------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsgMain.Text = "";
        lbl_MsgRemarks.Text = "";
        Message.Text = "";
        
        if (!IsPostBack)
        {
            CanEdit = false; 
            btnSubmitQuote.Visible = false;

            ddlCat.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT CATID,CATCODE + ' : ' + CATNAME AS FULLNAME FROM DD_JobCategory WHERE CATCODE <> 'ZA' ORDER BY CATCODE");
            ddlCat.DataTextField = "FULLNAME";
            ddlCat.DataValueField = "CATID";
            ddlCat.DataBind();
            ddlCat_OnSelectedIndexChanged(sender, e);

        }
    }
    
    protected void btnSelectCat_Click(object sender, EventArgs e)
    {
        
    }
    protected void ddlCat_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        JobCatId = Common.CastAsInt32(ddlCat.SelectedValue);
        BindJobs();

        //DataTable dtTotal = Common.Execute_Procedures_Select_ByQuery("SELECT ISNULL(SUM(NetAmount),0) FROM DD_Docket_RFQ_SubJobs WHERE RFQID=" + RFQId + " AND DOCKETID=" + DocketId + " AND DocketJobId IN (SELECT DocketJobId FROM DD_Docket_RFQ_Jobs WHERE RFQID=" + RFQId + " AND DOCKETID=" + DocketId + "  AND CatId = " + ddlCat.SelectedValue.Trim() + ")");
        //lblTotalAmount.Text = dtTotal.Rows[0][0].ToString();

        DataTable dtYard = Common.Execute_Procedures_Select_ByQuery("SELECT ISNULL(SUM(NetAmount),0) FROM DD_Docket_RFQ_SubJobs WHERE RFQID=" + RFQId + " AND DOCKETID=" + DocketId + " AND DocketJobId IN (SELECT DocketJobId FROM DD_Docket_RFQ_Jobs WHERE RFQID=" + RFQId + " AND DOCKETID=" + DocketId + "  AND CatId = " + ddlCat.SelectedValue.Trim() + ") AND ISNULL(CostCategory,'N') = 'Y' ");
        lblTotalYardCost.Text = dtYard.Rows[0][0].ToString();

        //DataTable dtNonYard = Common.Execute_Procedures_Select_ByQuery("SELECT ISNULL(SUM(NetAmount),0) FROM DD_Docket_RFQ_SubJobs WHERE RFQID=" + RFQId + " AND DOCKETID=" + DocketId + " AND DocketJobId IN (SELECT DocketJobId FROM DD_Docket_RFQ_Jobs WHERE RFQID=" + RFQId + " AND DOCKETID=" + DocketId + "  AND CatId = " + ddlCat.SelectedValue.Trim() + ") AND ISNULL(CostCategory,'N') = 'N' ");
        //lblTotalNonYardCost.Text = dtNonYard.Rows[0][0].ToString();
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
        DataTable dtJobs = Common.Execute_Procedures_Select_ByQuery("SELECT DocketJobId,JobId,JOBCODE,JOBNAME,JobDesc,RFQId,DOCKETID,CatId FROM DD_Docket_RFQ_Jobs WHERE RFQId=" + RFQId + " AND DOCKETID=" + DocketId + " And CatId=" + JobCatId + " ORDER BY JOBCODE");
        rptJobs.DataSource = dtJobs;
        rptJobs.DataBind();
    }
    public DataTable BindSubJobs(Object DocketJobId)
    {
        return Common.Execute_Procedures_Select_ByQuery("SELECT RFQId,[DocketSubJobId],[DocketJobId],[DocketId],[SubJobCode],[SubJobName],AttachmentName,LongDescr,Unit,BidQty,[QuoteQty],[UnitPrice],[DiscountPer],[NetAmount],VendorRemarks FROM [dbo].[DD_Docket_RFQ_SubJobs] WHERE RFQId=" + RFQId + " AND DOCKETID=" + DocketId + " And DocketJobId=" + DocketJobId + " AND ISNULL(CostCategory, 'Y')='Y' ORDER BY SubJobCode");
    }
    protected void imgDownload_Click(object sender, EventArgs e)
    {
        int DocketSubJobId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        int DocketJobId = Common.CastAsInt32(((ImageButton)sender).Attributes["DocketJobId"]);
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
    protected void btnDownloadSOR_Click(object sender, EventArgs e)
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT [SubJobCode],[SubJobName],AttachmentName,Attachment, RIGHT(AttachmentName, 5) As FileExt  FROM [dbo].[DD_DocketSubJobs] WHERE DOCKETID=" + DocketId + " and AttachmentName IS NOT Null and ATTACHMENT IS NOT NULL ORDER BY SubJobCode ");

        if (dt.Rows.Count > 0)
        {
            string sortemppath = Server.MapPath("~/DryDock/DownLoadSOR/");
            if (Directory.Exists(sortemppath))
            {
                Array.ForEach(Directory.GetFiles(sortemppath), File.Delete);
            }
            else
            {
                Directory.CreateDirectory(sortemppath);
            }

            foreach (DataRow dr in dt.Rows)
            {
                string FileName = dr["SubJobCode"].ToString().Trim() + dr["FileExt"].ToString();
                byte[] buff = (byte[])dr["Attachment"];
                System.IO.File.WriteAllBytes(sortemppath + FileName, buff);
            }

            string FileToZip = sortemppath;
            string ZipFile = Server.MapPath("~/TEMP/SOR.zip");

            if (File.Exists(ZipFile))
                File.Delete(ZipFile);

            using (ZipFile zip = new ZipFile())
            {
                zip.AddDirectory(FileToZip);
                zip.Save(ZipFile);
            }

            byte[] buff1 = System.IO.File.ReadAllBytes(ZipFile);
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(ZipFile));
            Response.BinaryWrite(buff1);
            Response.Flush();
            Response.End();
        }
        else
        {
            lblMsgMain.Text = "No SOR to download in this docket.";
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
        
        DataTable dtRFQ = Common.Execute_Procedures_Select_ByQuery("SELECT ISNULL(CURRENCY,'') FROM DD_Docket_RFQ_Master WHERE RFQID=" + RFQId.ToString());
        if (dtRFQ != null && dtRFQ.Rows.Count > 0)
        {
            ddlCurrency.SelectedValue = dtRFQ.Rows[0][0].ToString().Trim();
        }
        ddlCurrency_OnSelectedIndexChanged(new object(), new EventArgs());
        lblMsgMain.Text = "";
        //DataTable dtTotal = Common.Execute_Procedures_Select_ByQuery("SELECT ISNULL(SUM(NetAmount),0) FROM DD_Docket_RFQ_SubJobs WHERE RFQID=" + RFQId + " AND DOCKETID=" + DocketId + " AND DocketJobId IN (SELECT DocketJobId FROM DD_Docket_RFQ_Jobs WHERE RFQID=" + RFQId + " AND DOCKETID=" + DocketId + "  AND CatId = " + ddlCat.SelectedValue.Trim() + ")");
        //lblTotalAmount.Text = dtTotal.Rows[0][0].ToString();

        DataTable dtYard = Common.Execute_Procedures_Select_ByQuery("SELECT ISNULL(SUM(NetAmount),0) FROM DD_Docket_RFQ_SubJobs WHERE RFQID=" + RFQId + " AND DOCKETID=" + DocketId + " AND DocketJobId IN (SELECT DocketJobId FROM DD_Docket_RFQ_Jobs WHERE RFQID=" + RFQId + " AND DOCKETID=" + DocketId + "  AND CatId = " + ddlCat.SelectedValue.Trim() + ") AND ISNULL(CostCategory,'N') = 'Y' ");
        lblTotalYardCost.Text = dtYard.Rows[0][0].ToString();

        //DataTable dtNonYard = Common.Execute_Procedures_Select_ByQuery("SELECT ISNULL(SUM(NetAmount),0) FROM DD_Docket_RFQ_SubJobs WHERE RFQID=" + RFQId + " AND DOCKETID=" + DocketId + " AND DocketJobId IN (SELECT DocketJobId FROM DD_Docket_RFQ_Jobs WHERE RFQID=" + RFQId + " AND DOCKETID=" + DocketId + "  AND CatId = " + ddlCat.SelectedValue.Trim() + ") AND ISNULL(CostCategory,'N') = 'N' ");
        //lblTotalNonYardCost.Text = dtNonYard.Rows[0][0].ToString();
    }
    protected void btn_UpdateRemarks_Click(object sender, EventArgs e)
    {
        string SQL = "UPDATE DD_Docket_RFQ_SubJobs SET VendorRemarks = '" + txtRemarks.Text.Trim().Replace("'", "`") + "' WHERE RFQId=" + hfdRFQId.Value + " AND DocketId=" + hfdDocketId.Value + " AND DocketJobId=" + hfdDocketJobId.Value + "  AND DocketSubJobId=" + hfdDocketSubJobId.Value;
        Common.Execute_Procedures_Select_ByQuery(SQL);
        BindJobs();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "axdxasda", "alert('Remarks saved successfully.');", true);
    }
    protected void btnSubmitQuote_Click(object sender, EventArgs e)
    {
        if (ddlCurrency.SelectedIndex <= 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "111", "alert('Please select currency.');", true);
            ddlCurrency.Focus();
            return; 
        }

        string SQL = "UPDATE DD_Docket_RFQ_Master SET STATUS = 'Q' WHERE RFQId=" + RFQId;
        Common.Execute_Procedures_Select_ByQuery(SQL);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "111", "alert('Quote submitted successfully.');", true);
        btnSubmitQuote.Visible = false;
    }
    protected void ddlCurrency_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        Common.Set_Procedures("DD_UpdateCurrency");
        Common.Set_ParameterLength(2);
        Common.Set_Parameters(
           new MyParameter("@RFQId", RFQId),
           new MyParameter("@CURR", ddlCurrency.SelectedValue));
        DataSet ds = new DataSet();
        Boolean res;
        res = Common.Execute_Procedures_IUD(ds);
        if (res)
        {
            lblExchRate.Text = ds.Tables[0].Rows[0]["EXEC_RATE"].ToString();
            //lblTotalAmount.Text = ds.Tables[0].Rows[0]["NETAMOUNT"].ToString();
            lblMsgMain.Text = "Currency Updated Sucessfully.";
            if (JobCatId > 0)
            {
                BindJobs();
            }
        }
        else
        {
            lblMsgMain.Text = "Unable to update Currency. Error: " + Common.ErrMsg; ;
        }

        lblCurr.Text = "( " +ddlCurrency.SelectedValue + ")";
        lblCurr1.Text = "( " + ddlCurrency.SelectedValue + ")";
        lblCurr2.Text = "( " + ddlCurrency.SelectedValue + ")";
        //lblYCCurr.Text = "( " + ddlCurrency.SelectedValue + ")";
        //lblNYCCurr.Text = "( " + ddlCurrency.SelectedValue + ")";
    }
    protected void btn_UpdateSubJob_Click(object sender, EventArgs e)
    {
        string SQL = "UPDATE DD_Docket_RFQ_SubJobs SET CostCategory = '" + (rdoYardCost.Checked ? "Y" : "N") + "',OutsideRepair='" + (chkOutsideRepair.Checked ? "Y" : "N") + "' WHERE RFQId=" + hfdRFQId.Value + " AND DocketId=" + hfdDocketId.Value + " AND DocketJobId=" + hfdDocketJobId.Value + "  AND DocketSubJobId=" + hfdDocketSubJobId.Value;
        Common.Execute_Procedures_Select_ByQuery(SQL);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "axdxasda", "alert('Remarks saved successfully.');", true);
    }     

    protected void TotalYardCostPrint(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Yard Cost", "window.open('RFQEditReport.aspx?Type=YC&RFQId=" + RFQId + "&DocketId=" + DocketId + "', '_blank', '');", true);
    }

    protected void TotalOwnerCostPrint(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Owner Cost", "window.open('RFQEditReport.aspx?Type=OC&RFQId=" + RFQId + "&DocketId=" + DocketId + "', '_blank', '');", true);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Password.Text.Trim() == "")
        {
            Password.Focus();
            Message.Text = "Please enter password.";
            return;
        }
        
        string Key = Convert.ToString(Request.QueryString["Key"]);
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DD_Docket_RFQ_Master WHERE GUID='" + Key + "'");
        
        if (dt != null && dt.Rows.Count > 0)
        {
            if (!Convert.IsDBNull(dt.Rows[0]["PasswordExpiryDate"]))
            {
                if ((DateTime.Today.Date > Convert.ToDateTime(dt.Rows[0]["PasswordExpiryDate"])))
                {
                    CanEdit = false;
                    Message.Text = "Your password is expired.";
                    return;
                }

                if (Convert.IsDBNull(dt.Rows[0]["Password"]) || dt.Rows[0]["Password"].ToString() == "")
                {
                    CanEdit = false;
                    Message.Text = "Your password is expired.";
                    return;
                }
            }
            else
            {
                CanEdit = false;
                Message.Text = "Your password is expired.";
                return;
            }

            if (Password.Text.Trim() != dt.Rows[0]["Password"].ToString())
            {
                Password.Focus();
                Message.Text = "Please enter valid password.";
                return;
            }
        }

        RFQId = Common.CastAsInt32(dt.Rows[0]["RFQId"]);
        DocketId = Common.CastAsInt32(dt.Rows[0]["DocketId"]);
        lblRFQNo.Text = dt.Rows[0]["RFQNo"].ToString();
        if (dt.Rows[0]["Status"].ToString() == "W")
        {
            CanEdit = true;
            btnSubmitQuote.Visible = true;
        }
        else
        {
            CanEdit = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "dfsd", "alert('Invalid Quote#.');window.close();", true);
            return;
        }

        if (RFQId > 0)
        {
            BindCurrency();
            ShowDocketSummary();
            ddlCurrency.Enabled = CanEdit;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "dfsd", "alert('Invalid Quote#.');window.close();", true);
            return;
        }

        pnl_Password.Visible = false;
        pnl_Main.Visible = true;
    }
 }
