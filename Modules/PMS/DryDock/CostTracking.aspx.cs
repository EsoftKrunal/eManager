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

public partial class Docket_CostTracking : System.Web.UI.Page
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
    public int Level
    {
        set { ViewState["Level"] = value; }
        get { return Common.CastAsInt32(ViewState["Level"]); }
    }
    //-------------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsgMain.Text = "";
        if (!IsPostBack)
        {
            // -------------------------- SESSION CHECK ----------------------------------------------
            ProjectCommon.SessionCheck();
            // -------------------------- SESSION CHECK END ----------------------------------------------

            DocketId = Common.CastAsInt32(Request.QueryString["DocketId"]);
            RFQId = Common.CastAsInt32(Request.QueryString["RFQId"]);
            LoadCat();

            ddlCat.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT CATID,CATCODE + ' : ' + CATNAME AS FULLNAME FROM DD_JobCategory ORDER BY CATCODE");
            ddlCat.DataTextField = "FULLNAME";
            ddlCat.DataValueField = "CATID";
            ddlCat.DataBind();
            ddlCat_OnSelectedIndexChanged(sender, e);

            ShowDocketSummary();
            Level = 3;
            LoadCategory();
           
        }
    }
    protected void LoadCat()
    {
        DataTable dtCats = Common.Execute_Procedures_Select_ByQuery("SELECT CatId,CatCode  + ':'  + CatName AS CatName FROM DD_JobCategory Order By CatCode");
        ddlJObCat.DataSource = dtCats;
        ddlJObCat.DataTextField = "CatName";
        ddlJObCat.DataValueField = "CatId";
        ddlJObCat.DataBind();
        ddlJObCat.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< -- Select Job Category -- >", "0"));
    }
    protected void LoadCat_SelectIndexChanged(object sender, EventArgs e)
    {
        LoadCategory();
    }
    protected void ddlCostCategory_SelectIndexChanged(object sender, EventArgs e)
    {
        LoadCategory();
    }
    protected void ddlCat_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ddlSubCat.DataSource = Common.Execute_Procedures_Select_ByQuery("select docketjobid,JOBCODE,JOBCODE + ' : ' + JOBNAME as FULLNAME from DD_DocketJobs where docketid=" + DocketId.ToString() + " AND CATID=" + ddlCat.SelectedValue + " ORDER BY JOBNAME");
        ddlSubCat.DataTextField = "FULLNAME";
        ddlSubCat.DataValueField = "docketjobid";
        ddlSubCat.DataBind();
    }
    protected void btnAddJob_Click(object sender, EventArgs e)
    {
        dv_A_SubJobs.Visible = true;
    }
    protected void btn_A_SaveJob_Click(object sender, EventArgs e)
    {
        //================
        if (txt_A_SubJobName.Text.Trim() == "")
        {
            lbl_A_MsgSubJob.Text = "Please enter short descr.";
            return;
        }

        try
        {
            string FileName = "";
            byte[] FileContent = new byte[0];
            if (ftp_A_Upload.HasFile)
                if (ftp_A_Upload.PostedFile.ContentLength > 0)
                {
                    FileName = System.IO.Path.GetFileName(ftp_A_Upload.PostedFile.FileName);
                    FileContent = ftp_A_Upload.FileBytes;
                }

            decimal GrossAmount=Common.CastAsDecimal(txt_A_UP.Text) *  Common.CastAsDecimal(txt_A_POQty.Text);
            decimal DiscPer=Common.CastAsDecimal(txt_A_Disc.Text);
            decimal NetAmount=(GrossAmount-((GrossAmount*DiscPer)/100));

            Common.Set_Procedures("[dbo].[DD_InsertJobSpecification_Other_RFQ]");
            Common.Set_ParameterLength(16);
            Common.Set_Parameters(
                new MyParameter("@CURRRFQId", RFQId),
                new MyParameter("@DocketId", DocketId),
                new MyParameter("@DocketJobId", ddlSubCat.SelectedValue),
                new MyParameter("@SubJobName", txt_A_SubJobName.Text.Trim().Replace("'", "`")),
                new MyParameter("@AttachmentName", FileName),
                new MyParameter("@Attachment", FileContent),
                new MyParameter("@Unit", txt_A_SubJobunit.Text.Trim().Replace("'", "`")),
                new MyParameter("@BidQty", Common.CastAsDecimal(txt_A_SubJobBidQty.Text)),
                new MyParameter("@LongDescr", txt_A_LongDescr.Text.Trim()),
                new MyParameter("@CostCategory", rdo_A_YardCost.Checked ? "Y" : "N"),
                new MyParameter("@OutsideRepair", chk_A_OutsideRepair.Checked ? "Y" : "N"),
                new MyParameter("@POQty", Common.CastAsDecimal(txt_A_POQty.Text)),
                new MyParameter("@POUnitPrice_USD", Common.CastAsDecimal(txt_A_UP.Text)),
                new MyParameter("@POGrossAmount_USD", GrossAmount),
                new MyParameter("@PODiscountPer", DiscPer),
                new MyParameter("@PONetAmount_USD", NetAmount));
            DataSet ds = new DataSet();
            ds.Clear();
            Boolean res;
            res = Common.Execute_Procedures_IUD(ds);
            if (res)
            {
                txt_A_SubJobName.Text = "";
                txt_A_SubJobunit.Text = "";
                txt_A_SubJobBidQty.Text = "";
                txt_A_LongDescr.Text = "";
                rdo_A_YardCost.Checked = true;
                rdo_A_NonYardCost.Checked = false;
                chk_A_OutsideRepair.Checked = false;
                lbl_A_MsgSubJob.Text = "Job added successfully.";
            }
            else
            {
                lbl_A_MsgSubJob.Text = "Unable to add nwe job. Error :" + Common.ErrMsg;
            }
        }
        catch (Exception ex)
        {
            lbl_A_MsgSubJob.Text = "Unable to add new job. Error :" + ex.Message;
        }
    }
    protected void btn_A_CloseJob_Click(object sender, EventArgs e)
    {
        LoadCategory();
        dv_A_SubJobs.Visible = false;
    }
    public string FormatCurr_WithComma(object in_p)
    {
        return String.Format(new System.Globalization.CultureInfo("en-US"), "{0:#,###,###}", Common.CastAsDecimal(in_p));
    }
    protected void txtYardDiscPer_OnTextChanged(object sender, EventArgs e)
    {
        Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.DD_Docket_RFQ_Master SET YardDiscount=" + Common.CastAsDecimal(txtYardDiscPer.Text).ToString() + " WHERE DOCKETID=" + DocketId + " AND RFQID=" + RFQId);
        decimal esttotal = Common.CastAsDecimal(lblEstUsd.Text);
        decimal yardtotal = Common.CastAsDecimal(lblTotalYardCost.Text);

        decimal discperamt = (yardtotal * (Common.CastAsDecimal(txtYardDiscPer.Text))/100);
        decimal disc = Common.CastAsDecimal(txtFinalDiscount.Text);
        lblTotalAmount.Text = FormatCurr_WithComma(esttotal - discperamt - disc);

        lblFinalYardCost.Text = FormatCurr_WithComma(yardtotal - discperamt - disc);
    }
    protected void txtFinalDiscount_OnTextChanged(object sender, EventArgs e)
    {
        Common.Execute_Procedures_Select_ByQuery("UPDATE DBO.DD_Docket_RFQ_Master SET FinalDiscount=" + Common.CastAsDecimal(txtFinalDiscount.Text).ToString() + " WHERE DOCKETID=" + DocketId + " AND RFQID=" + RFQId);
        decimal esttotal = Common.CastAsDecimal(lblEstUsd.Text);
        decimal yardtotal = Common.CastAsDecimal(lblTotalYardCost.Text);

        decimal discperamt = (yardtotal * (Common.CastAsDecimal(txtYardDiscPer.Text)) / 100);
        decimal disc = Common.CastAsDecimal(txtFinalDiscount.Text);
        lblTotalAmount.Text = FormatCurr_WithComma(esttotal - disc);
        
        lblFinalYardCost.Text = FormatCurr_WithComma(yardtotal - discperamt - disc);
    }
    protected void LoadCategory()
    {
        DateTime StartDate=new DateTime();
        DateTime EndDate = new DateTime();
        DataTable dtRFQ = Common.Execute_Procedures_Select_ByQuery("SELECT ExecFrom,ExecTo,FinalDiscount,YardDiscount FROM DD_Docket_RFQ_Master Where RFQId=" + RFQId);
        if (dtRFQ.Rows.Count > 0)
        {
            StartDate = Convert.ToDateTime(dtRFQ.Rows[0]["ExecFrom"]);
            EndDate = Convert.ToDateTime(dtRFQ.Rows[0]["ExecTo"]);
        }

        string CostCatClause = "";

        if (ddlCostCategory.SelectedIndex != 0)
            CostCatClause = " AND ISNULL(CostCategory,'N')='" + ddlCostCategory.SelectedValue + "' ";


        StringBuilder sbleft = new StringBuilder();
        StringBuilder sbcolheader1 = new StringBuilder();
        StringBuilder sbcolfooter = new StringBuilder();
        StringBuilder sbdata = new StringBuilder();

        DataTable dtCats = new DataTable();

        if (ddlJObCat.SelectedIndex <= 0)
            dtCats = Common.Execute_Procedures_Select_ByQuery("SELECT CatId,CatCode,CatName FROM DD_JobCategory C WHERE (SELECT COUNT(*) FROM DD_Docket_RFQ_SubJobs DSJ WHERE DSJ.DOCKETID=" + DocketId + CostCatClause + " AND LEFT(SUBJOBCODE,2)=C.CATCODE) > 0 ORDER BY CATCODE");
        else
            dtCats = Common.Execute_Procedures_Select_ByQuery("SELECT CatId,CatCode,CatName FROM DD_JobCategory where CatId=" + ddlJObCat.SelectedValue + " Order By CatCode");

        DataTable dtJobs_All = Common.Execute_Procedures_Select_ByQuery("SELECT CatId,DocketJobId,JobId,JOBCODE,JOBNAME FROM DD_DocketJobs DJ WHERE DJ.DOCKETID=" + DocketId + " AND (SELECT COUNT(*) FROM DD_Docket_RFQ_SubJobs DSJ WHERE DSJ.DOCKETID=" + DocketId + " AND DSJ.RFQID=" + RFQId + " AND DSJ.DocketJobId=DJ.DocketJobId " + CostCatClause + " ) > 0 ORDER BY JOBCODE");
        DataTable dtDocketSubJobs_All = Common.Execute_Procedures_Select_ByQuery("SELECT [DocketSubJobId],[DocketJobId],[DocketId],[SubJobCode],left([SubJobName],100) + '...' as SubJobName,AttachmentName,Unit,isnull(CostCategory,'Y') AS CostCategory FROM [dbo].[DD_Docket_RFQ_SubJobs]  WHERE RFQId=" + RFQId + " And DOCKETID=" + DocketId + (ddlCostCategory.SelectedIndex > 0 ? " AND isnull(CostCategory,'Y') ='" + ddlCostCategory.SelectedValue.Trim() + "'" : "") + " ORDER BY SubJobCode");
        DataTable dtRFQSubJobs_All = Common.Execute_Procedures_Select_ByQuery("SELECT RFQId,[DocketSubJobId],[DocketJobId],[DocketId],[SubJobCode],[SubJobName],AttachmentName,Unit,POQty,POUnitPrice_USD,PODiscountPer,CASE WHEN SUPNetAmount_USD = 0.00 THEN 0 ELSE SUPNetAmount_USD END AS PONetAmount_USD  ,EstAmount_USD,EstDiscPer,ROUND((ISNULL(EstAmount_USD,0)-ISNULL(SUPNetAmount_USD,0)),2) as VarAmt,ROUND(((ISNULL(EstAmount_USD,0)-ISNULL(SUPNetAmount_USD,0)) * 100 / ( CASE WHEN ISNULL(SUPNetAmount_USD,0)=0 THEN 1 ELSE SUPNetAmount_USD END)),2) As VarPer, EstQty, Remarks, EstUnitPrice FROM [dbo].[DD_Docket_RFQ_SubJobs] WHERE DOCKETID=" + DocketId + " And RFQId=" + RFQId + (ddlCostCategory.SelectedIndex > 0 ? " AND ISNULL(CostCategory, 'Y') ='" + ddlCostCategory.SelectedValue.Trim() + "'" : "") + " ORDER BY SubJobCode");

        decimal value1 =Common.CastAsDecimal(dtRFQSubJobs_All.Compute("SUM(PONetAmount_USD)", ""));
        decimal value2 = Common.CastAsDecimal(dtRFQSubJobs_All.Compute("SUM(EstAmount_USD)", ""));

        lblPOSUM.Text = FormatCurency(Math.Round(value1, 0));
        lblTotal_EstAmount_USD.Text = FormatCurency(Math.Round(value2,0));
        lblTotal_Variance.Text = FormatCurency(Convert.ToDecimal(Common.CastAsDecimal(lblTotal_EstAmount_USD.Text) - Common.CastAsDecimal(lblPOSUM.Text))).ToString();

        if (Common.CastAsDecimal(lblTotal_Variance.Text) > 0)
            lblTotal_Variance.ForeColor = System.Drawing.Color.Red;

        if (lblPOSUM.Text.Trim() != "0")
        {
            try
            {
                lblTotal_VariancePer.Text = FormatCurency(Convert.ToDecimal((Convert.ToDecimal(lblTotal_Variance.Text) / Convert.ToDecimal(lblPOSUM.Text) * 100))).ToString();
            }
            catch
            {}
            if (Common.CastAsDecimal(lblTotal_VariancePer.Text) > 0)
                lblTotal_VariancePer.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            lblTotal_VariancePer.Text = "0";
        }
        lblTotal_EstAmount_USD.Text = FormatCurency(lblTotal_EstAmount_USD.Text);


        //-------------------------

        DataTable dt111 = Common.Execute_Procedures_Select_ByQuery("SELECT ISNULL(SUM(case when isnull(costcategory,'N')='Y' THEN EstAmount_USD ELSE 0 END),0) as EstAmount_USD_TotalYC, ISNULL(SUM(case when isnull(costcategory,'N')='N' THEN EstAmount_USD ELSE 0 END),0) as EstAmount_USD_TotalNY,SUM(EstAmount_USD) AS TOTAL_EST,SUM(PONetAmount_USD) AS TOTAL_PO,SUM(SUPNetAmount_USD) AS TOTAL_SUP,ISNULL(SUM(case when isnull(costcategory,'N')='Y' THEN SUPNetAmount_USD ELSE 0 END),0) AS YARD_PO,ISNULL(SUM(case when isnull(costcategory,'N')='N' THEN SUPNetAmount_USD ELSE 0 END),0) AS OWNER_PO  FROM DD_Docket_RFQ_SubJobs WHERE RFQID=" + RFQId);
        
        decimal POTotal=0,POyard=0,PoOwner=0,EstTotal=0,EstYard=0,EstNonYard=0,FinalYard, FinalDiscPer = 0,FinalDisc =0,TotalAmt=0;
        
        if (dt111.Rows.Count > 0)
        {
            POTotal = Common.CastAsDecimal(dt111.Rows[0]["TOTAL_SUP"]);
            POyard = Common.CastAsDecimal(dt111.Rows[0]["YARD_PO"]);
            PoOwner=Common.CastAsDecimal(dt111.Rows[0]["OWNER_PO"]);

            EstTotal= Common.CastAsDecimal(dt111.Rows[0]["TOTAL_EST"]);
            EstYard= Common.CastAsDecimal(dt111.Rows[0]["EstAmount_USD_TotalYC"]);
            EstNonYard= Common.CastAsDecimal(dt111.Rows[0]["EstAmount_USD_TotalNY"]);

        }
        FinalDisc= Common.CastAsDecimal(dtRFQ.Rows[0]["FinalDiscount"]);
        FinalDiscPer = Common.CastAsDecimal(dtRFQ.Rows[0]["YardDiscount"]);

        FinalYard = EstYard-((EstYard*FinalDiscPer)/100) - FinalDisc;
        TotalAmt= FinalYard + EstNonYard;

        lblTotal_PONetAmount_USD.Text = FormatCurr_WithComma(POTotal);
        lblTotal_PONetAmount_Yard.Text = FormatCurr_WithComma(POyard);
        lblTotal_PONetAmount_Owner.Text = FormatCurr_WithComma(PoOwner);

        lblEstUsd.Text = FormatCurr_WithComma(EstTotal);
        lblTotalYardCost.Text = FormatCurr_WithComma(EstYard);
        lblTotalNonYardCost.Text = FormatCurr_WithComma(EstNonYard);

        txtFinalDiscount.Text = FormatCurency(dtRFQ.Rows[0]["FinalDiscount"]);
        txtYardDiscPer.Text = FormatCurency(dtRFQ.Rows[0]["YardDiscount"]);
        lblFinalYardCost.Text = FormatCurr_WithComma(FinalYard);

        lblTotalAmount.Text = FormatCurr_WithComma(TotalAmt);
        //-------------------------
        
        sbleft.Append("<table style='width:100%;' cellspacing='0' cellpadding='0'>");
        sbdata.Append("<table style='width:100%;' cellspacing='0' cellpadding='0'>");
        foreach (DataRow drCat in dtCats.Rows)
        {
            // CATEGORY HEAD ROW
            string test="<div style='float:left; height:10;'>&nbsp;" + drCat["CatCode"].ToString() + " : " + drCat["CatName"].ToString() + "</div>";
            // CATEGORY HEAD ROW
            DataTable dtCatsSum = Common.Execute_Procedures_Select_ByQuery("SELECT ISNULL(SUM(SUPNetAmount_USD),0) as PONetAmount_USD,ISNULL(SUM(EstAmount_USD),0) as EstAmount_USD,(ISNULL(SUM(EstAmount_USD),0)-ISNULL(SUM(SUPNetAmount_USD),0)) as VarAmt,((ISNULL(SUM(EstAmount_USD),0)-ISNULL(SUM(SUPNetAmount_USD),0)) * 100 )/( CASE WHEN ISNULL(SUM(SUPNetAmount_USD),0)=0 THEN 1 ELSE SUM(SUPNetAmount_USD) END)   As VarPer FROM DD_Docket_RFQ_SubJobs WHERE RFQID=" + RFQId + " AND DOCKETJOBID IN(select docketjobid from DD_Docket_RFQ_Jobs where docketid=" + DocketId + " and rfqid=" + RFQId + CostCatClause + " and catid=" + drCat["CatId"].ToString() + ")");
            sbdata.Append("<tr><td class='hover_highlight_cat' name='tr_" + drCat["CatCode"].ToString() + "'>");
            sbdata.Append("<table cellspacing='0' rules='none' border='0' cellpadding='0' style='border-collapse:collapse; width:100%;color:black;'>");
            sbdata.Append("<tr>");
            sbdata.Append("<td class='bl' style='width:300px; text-align:left;'>" + test + "</td>");
            sbdata.Append("<td class='bl' style='width:100px;text-align:right;color:#cccccc;'><span id='lblCatNetAmt_" + drCat["CatCode"].ToString() + "'>" + (dtCatsSum.Rows[0]["PONetAmount_USD"].ToString() == "0.00" ? "" : FormatCurency(dtCatsSum.Rows[0]["PONetAmount_USD"])) + "&nbsp;</span></td>");
            sbdata.Append("<td class='bl' style='width:60px; text-align:right;'></td>");
            sbdata.Append("<td class='bl' style='width:80px; text-align:right;'></td>");
            sbdata.Append("<td class='bl' style='width:60px; text-align:right;'></td>");
            sbdata.Append("<td class='bl' style='width:100px; text-align:right;color:#cccccc;'><span id='lblCatEstAmt_" + drCat["CatCode"].ToString() + "'>" + (dtCatsSum.Rows[0]["EstAmount_USD"].ToString() == "0.00" ? "" : FormatCurency(dtCatsSum.Rows[0]["EstAmount_USD"])) + "&nbsp;</span></td>");

            string textcolor2 = "color:#cccccc;";
            if (Common.CastAsDecimal(dtCatsSum.Rows[0]["VarAmt"]) > 0)
                textcolor2 = "color:#FFC2B2;";

            sbdata.Append("<td class='bl' style='width:80px; text-align:right;'><span id='lblCatVarAmt_" + drCat["CatCode"].ToString() + "' style='" + textcolor2 + "'>" + (dtCatsSum.Rows[0]["VarAmt"].ToString() == "0.00" ? "" : FormatCurency(dtCatsSum.Rows[0]["VarAmt"])) + "&nbsp;</span></td>");
            sbdata.Append("<td class='bl' style='width:80px; text-align:right;'><span id='lblCatVarPer_" + drCat["CatCode"].ToString() + "' style='" + textcolor2 + "'>" + (FormatCurency(dtCatsSum.Rows[0]["VarPer"]).ToString() == "0.00" ? "" : FormatCurency(dtCatsSum.Rows[0]["VarPer"])) + "&nbsp;</span></td>");
            sbdata.Append("<td class='bl br' style='text-align:right;'></td>");
            sbdata.Append("</tr>");
            sbdata.Append("</table>");
            sbdata.Append("</td></tr>");
            if (Level >= 2)//--------------------------------------------
            {
                #region job section

                DataTable dtJobs = getFilterdJobs(dtJobs_All, drCat["CatId"].ToString());
                foreach (DataRow drJob in dtJobs.Rows)
                {
                    sbdata.Append("<tr><td class='hover_highlight_job' name='tr_" + drJob["JOBCODE"].ToString() + "'>");
                    DataTable dtJobSum = Common.Execute_Procedures_Select_ByQuery("SELECT ISNULL(SUM(SUPNetAmount_USD),0) as PONetAmount_USD,ISNULL(SUM(EstAmount_USD),0) as EstAmount_USD,(ISNULL(SUM(EstAmount_USD),0)-ISNULL(SUM(SUPNetAmount_USD),0)) as VarAmt,((ISNULL(SUM(EstAmount_USD),0)-ISNULL(SUM(SUPNetAmount_USD),0)) * 100 )/( CASE WHEN ISNULL(SUM(SUPNetAmount_USD),0)=0 THEN 1 ELSE SUM(SUPNetAmount_USD) END)   As VarPer FROM DD_Docket_RFQ_SubJobs WHERE RFQID=" + RFQId + CostCatClause + " AND DOCKETJOBID=" + drJob["DOCKETJOBID"].ToString());
                    sbdata.Append("<table cellspacing='0' rules='none' border='0' cellpadding='0' style='border-collapse:collapse; width:100%;'>");
                    sbdata.Append("<tr>");

                    string test1 = "<div style='float:left; height:10px;' Job='Job' JobCode='" + drJob["JOBCODE"].ToString() + "' DocketJobId='" + drJob["DocketJobId"].ToString() + "' DocketSubJobId='0' onclick='ShowJobDesc(this);'>" +
                    "&nbsp;&nbsp;" + drJob["JOBCODE"].ToString() + " : " + drJob["JOBNAME"].ToString() + "</div>";

                    sbdata.Append("<td class='bl' style='width:300px; text-align:left;'>" + test1 + "</td>");
                    sbdata.Append("<td class='bl' style='width:100px;text-align:right;color:#cccccc;'><span id='lblJobNetAmt_" + drJob["JOBCODE"].ToString() + "'>" + (dtJobSum.Rows[0]["PONetAmount_USD"].ToString() == "0.00" ? "" : FormatCurency(dtJobSum.Rows[0]["PONetAmount_USD"])) + "&nbsp;</span></td>");
                    sbdata.Append("<td class='bl' style='width:60px; text-align:right;'></td>");
                    sbdata.Append("<td class='bl' style='width:80px; text-align:right;'></td>");
                    sbdata.Append("<td class='bl' style='width:60px; text-align:right;'></td>");
                    sbdata.Append("<td class='bl' style='width:100px; text-align:right;color:#cccccc;'><span id='lblJobEstAmt_" + drJob["JOBCODE"].ToString() + "'>" + (dtJobSum.Rows[0]["EstAmount_USD"].ToString() == "0.00" ? "" : FormatCurency(dtJobSum.Rows[0]["EstAmount_USD"])) + "&nbsp;</td>");

                    string textcolor1 = "color:#cccccc;";
                    if (Common.CastAsDecimal(dtJobSum.Rows[0]["VarAmt"]) > 0)
                        textcolor1 = "color:#FFC2B2;";

                    sbdata.Append("<td class='bl' style='width:80px; text-align:right;'><span id='lblJobVarAmt_" + drJob["JOBCODE"].ToString() + "' style='" + textcolor1 + "'>" + (dtJobSum.Rows[0]["VarAmt"].ToString() == "0.00" ? "" : FormatCurency(dtJobSum.Rows[0]["VarAmt"])) + "&nbsp;</span></td>");
                    sbdata.Append("<td class='bl' style='width:80px; text-align:right;'><span id='lblJobVarPer_" + drJob["JOBCODE"].ToString() + "' style='" + textcolor1 + "'>" + (FormatCurency(dtJobSum.Rows[0]["VarPer"]).ToString() == "0.00" ? "" : FormatCurency(dtJobSum.Rows[0]["VarPer"])) + "&nbsp;</span></td>");
                    sbdata.Append("<td class='bl br' style='text-align:right;'></td>");
                    sbdata.Append("</tr>");
                    sbdata.Append("</table>");
                    sbdata.Append("</td></tr>");

                    int c = 0;
                    if (Level >= 3)//--------------------------------------------
                    {
                        //sub jobs

                        #region Subjob Region
                        DataTable dtSubJobs = getFilterdSubJobs(dtDocketSubJobs_All, drJob["DocketJobId"].ToString());
                        foreach (DataRow drSubJob in dtSubJobs.Rows)
                        {
                            sbdata.Append("<tr><td class='hover_highlight_subjob' name='tr_" + drSubJob["SubJobCode"].ToString() + "'>");
                            sbdata.Append("<table cellspacing='0' rules='none' border='0' cellpadding='0' style='border-collapse:collapse; width:100%;'>");
                            sbdata.Append("<tr name='tr_" + drSubJob["SubJobCode"] + "' class='hover_highlight' >");
                          
                                DataTable dtData = getFilterdSubJob_Single(dtRFQSubJobs_All, drJob["DocketJobId"].ToString(), drSubJob["DocketSubJobId"].ToString());
                                if (dtData.Rows.Count > 0)
                                {
                                    string test2 = "<div style='overflow:hidden;padding-left:5px; height:13px;'><img id='imgY_" + drSubJob["SubJobCode"].ToString() + "' SubJobCode='" + drSubJob["SubJobCode"].ToString() + "' cc='Y' DocketJobId='" + drSubJob["DocketJobId"].ToString() + "' DocketSubJobId='" + drSubJob["DocketSubJobId"].ToString() + "' DocketId='" + drSubJob["DocketId"].ToString() + "' RFQId='" + RFQId.ToString() + "' src='../Images/green_circle.gif' alt='Yard Cost' style='float:left; display:" + ((drSubJob["CostCategory"].ToString() == "Y") ? "none" : "block") + ";' onclick='Update_CC(this);' /><img id='imgN_" + drSubJob["SubJobCode"].ToString() + "' cc='N' SubJobCode='" + drSubJob["SubJobCode"].ToString() + "' DocketJobId='" + drSubJob["DocketJobId"].ToString() + "' DocketSubJobId='" + drSubJob["DocketSubJobId"].ToString() + "' DocketId='" + drSubJob["DocketId"].ToString() + "' RFQId='" + RFQId.ToString() + "' src='../Images/red_circle.png' alt='Owner Cost' style='float:left; display:" + ((drSubJob["CostCategory"].ToString() == "Y") ? "block" : "none") + ";' onclick='Update_CC(this);' /><span style=' padding-left:5px;' Job='SubJob' JobCode='" + drSubJob["SubJobCode"].ToString() + "' DocketJobId='" + drJob["DocketJobId"].ToString() + "' DocketSubJobId='" + drSubJob["DocketSubJobId"].ToString() + "' onclick='ShowJobDesc(this);' >" + drSubJob["SubJobCode"].ToString() + " : " + drSubJob["SubJobName"].ToString() + "</span><span style='clear:both;'></span></div>";

                                    sbdata.Append("<td class='bl' style='width:300px; text-align:left;'>" + test2 + "</td>");
                                    sbdata.Append("<td class='bl' style='width:100px;text-align:right;'>" + (dtData.Rows[0]["PONetAmount_USD"].ToString() == "0.00" ? "" : FormatCurency(dtData.Rows[0]["PONetAmount_USD"])) + "&nbsp;</td>");
                                    string EstQty = FormatCurency(dtData.Rows[0]["EstQty"]);
                                    string EstAmt = FormatCurency(dtData.Rows[0]["EstAmount_USD"]);
                                    string EstDiscPer = FormatCurency(dtData.Rows[0]["EstDiscPer"]);
                                    string EstUnitPrice = FormatCurency(dtData.Rows[0]["EstUnitPrice"]);
                                    

                                    string color1 = "color:black;";
                                    if (dtData.Rows[0]["POQty"].ToString() != EstQty)
                                    {
                                        color1 = "color:blue;";
                                    }

                                    string color2 = "color:black;";
                                    if (dtData.Rows[0]["PONetAmount_USD"].ToString() != EstAmt)
                                    {
                                        color2 = "color:blue;";
                                    }

                                    string color3 = "color:black;";
                                    if (dtData.Rows[0]["POUnitPrice_USD"].ToString() != EstUnitPrice)
                                    {
                                        color3 = "color:blue;";
                                    }

                                    sbdata.Append("<td class='bl' style='width:60px;text-align:center;'><input id='txtEstQty_" + dtData.Rows[0]["SubJobCode"].ToString() + "' class='newinput' style='width:50px;" + color1 + ";' onkeypress='fncInputNumericValuesOnly(event)' value='" + EstQty + "' onchange='Update_Values(this);' RFQId='" + dtData.Rows[0]["RFQId"].ToString() + "' DocketSubJobId='" + dtData.Rows[0]["DocketSubJobId"].ToString() + "' DocketJobId='" + dtData.Rows[0]["DocketJobId"].ToString() + "' DocketId='" + DocketId + "' SubJobCode='" + dtData.Rows[0]["SubJobCode"].ToString() + "' /></td>");
                                    sbdata.Append("<td class='bl' style='width:80px;text-align:center;'><input id='txtEstUnitPrice_" + dtData.Rows[0]["SubJobCode"].ToString() + "' class='newinput' style='width:70px;" + color3 + ";' onkeypress='fncInputNumericValuesOnly(event)' value='" + EstUnitPrice + "' onchange='Update_Values(this);' RFQId='" + dtData.Rows[0]["RFQId"].ToString() + "' DocketSubJobId='" + dtData.Rows[0]["DocketSubJobId"].ToString() + "' DocketJobId='" + dtData.Rows[0]["DocketJobId"].ToString() + "' DocketId='" + DocketId + "' SubJobCode='" + dtData.Rows[0]["SubJobCode"].ToString() + "' /></td>");
                                    sbdata.Append("<td class='bl' style='width:60px;text-align:center;'><input id='txtEstDisc_" + dtData.Rows[0]["SubJobCode"].ToString() + "' class='newinput' style='width:50px;" + color1 + ";' onkeypress='fncInputNumericValuesOnly(event)' value='" + EstDiscPer + "' onchange='Update_Values(this);' RFQId='" + dtData.Rows[0]["RFQId"].ToString() + "' DocketSubJobId='" + dtData.Rows[0]["DocketSubJobId"].ToString() + "' DocketJobId='" + dtData.Rows[0]["DocketJobId"].ToString() + "' DocketId='" + DocketId + "' SubJobCode='" + dtData.Rows[0]["SubJobCode"].ToString() + "' /></td>");
                                    sbdata.Append("<td class='bl' style='width:100px;text-align:right;'><span id='txtEstAmount_" + dtData.Rows[0]["SubJobCode"].ToString() + "' style='" + color2 + "'>" + ((Common.CastAsInt32(EstAmt) == 0) ? "" : EstAmt.ToString()) + "&nbsp;</span></td>");

                                    string textcolor = "";
                                    if (Common.CastAsDecimal(dtData.Rows[0]["VarAmt"])>0)
                                        textcolor="color:red";

                                    sbdata.Append("<td class='bl' style='width:80px; text-align:right;'><span id='lblVarAmt_" + dtData.Rows[0]["SubJobCode"].ToString() + "' style='" + textcolor + "'>" + (dtData.Rows[0]["VarAmt"].ToString() == "0.00" ? "" : FormatCurency(dtData.Rows[0]["VarAmt"])) + "&nbsp;</span></td>");
                                    sbdata.Append("<td class='bl' style='width:80px; text-align:right;'><span id='lblVarPer_" + dtData.Rows[0]["SubJobCode"].ToString() + "' style='" + textcolor + "'>" + (FormatCurency(dtData.Rows[0]["VarPer"]).ToString() == "0.00" ? "" : FormatCurency(dtData.Rows[0]["VarPer"])) + "&nbsp;</span></td>");
                                    sbdata.Append("<td class='bl br' style='text-align:center;'><input id='txtRemarks_" + dtData.Rows[0]["SubJobCode"].ToString() + "' title='" + dtData.Rows[0]["Remarks"].ToString().Replace("\"", "`").Replace("\'", "`") + "' class='newinput' style='width:95%; text-align:left;' value='" + dtData.Rows[0]["Remarks"] + "' onchange='Update_Values(this);' RFQId='" + dtData.Rows[0]["RFQId"].ToString() + "' DocketSubJobId='" + dtData.Rows[0]["DocketSubJobId"].ToString() + "' DocketJobId='" + dtData.Rows[0]["DocketJobId"].ToString() + "' DocketId='" + DocketId + "' SubJobCode='" + dtData.Rows[0]["SubJobCode"].ToString() + "' /></td>");
                                }
                                else
                                {
                                    sbdata.Append("<td class='bl' style='width:100px; text-align:right;'></td>");
                                    sbdata.Append("<td class='bl' style='width:60px; text-align:right;'></td>");
                                    sbdata.Append("<td class='bl' style='width:80px; text-align:right;'></td>");
                                    sbdata.Append("<td class='bl' style='width:60px; text-align:right;'></td>");
                                    sbdata.Append("<td class='bl' style='width:100px; text-align:right;'>&nbsp;</td>");
                                    sbdata.Append("<td class='bl' style='width:80px; text-align:right;'></td>");
                                    sbdata.Append("<td class='bl' style='width:80px; text-align:right;'></td>");
                                    sbdata.Append("<td class='bl br' style='text-align:right;'></td>");
                                }
                             sbdata.Append("</tr>");
                            sbdata.Append("</table>");
                            sbdata.Append("</td></tr>");
                        }
                        #endregion
                    }
                }
                #endregion
            }
            //-------------------------
            //#region Left Column Data
            //if (Level >= 2)//--------------------------------------------
            //{
            //    DataTable dtJobs2 = getFilterdJobs(dtJobs_All, drCat["CatId"].ToString());
            //    foreach (DataRow drJob1 in dtJobs2.Rows)
            //    {
            //        DataTable dt11 = Common.Execute_Procedures_Select_ByQuery("SELECT JOBCODE,PlanFrom,PlanTo,StartFrom,StartTo,ExecPer,ExecRemarks FROM DD_Docket_RFQ_Jobs  WHERE RFQId=" + RFQId + " AND DocketId=" + DocketId + " AND DocketJobId=" + drJob1["DocketJobId"].ToString());

            //        sbleft.Append("<tr><td class='hover_highlight_job' name='tr_" + drJob1["JOBCODE"].ToString() + "'>");
            //        sbleft.Append("<div style='float:left; height:10px;' Job='Job' JobCode='" + drJob1["JOBCODE"].ToString() + "' DocketJobId='" + drJob1["DocketJobId"].ToString() + "' DocketSubJobId='0' onclick='ShowJobDesc(this);'>");
            //        sbleft.Append("&nbsp;&nbsp;" + drJob1["JOBCODE"].ToString() + " : " + drJob1["JOBNAME"].ToString());
            //        sbleft.Append("</div>");
            //        sbleft.Append("</td></tr>");

            //        if (Level >= 3)//--------------------------------------------
            //        {
            //            // SUB JOB ROW
            //            DataTable dtSubJobs = getFilterdSubJobs(dtDocketSubJobs_All, drJob1["DocketJobId"].ToString());
            //            foreach (DataRow drSubJob in dtSubJobs.Rows)
            //            {
            //                sbleft.Append("<tr><td name='tr_" + drSubJob["SubJobCode"].ToString() + "' class='hover_highlight_subjob'>");
            //                sbleft.Append("<div style='overflow:hidden;padding-left:5px; height:13px;'><img id='imgY_" + drSubJob["SubJobCode"].ToString() + "' SubJobCode='" + drSubJob["SubJobCode"].ToString() + "' cc='Y' DocketJobId='" + drSubJob["DocketJobId"].ToString() + "' DocketSubJobId='" + drSubJob["DocketSubJobId"].ToString() + "' DocketId='" + drSubJob["DocketId"].ToString() + "' RFQId='" + RFQId.ToString() + "' src='../Images/green_circle.gif' alt='Yard Cost' style='float:left; display:" + ((drSubJob["CostCategory"].ToString() == "Y") ? "none" : "block") + ";' onclick='Update_CC(this);' /><img id='imgN_" + drSubJob["SubJobCode"].ToString() + "' cc='N' SubJobCode='" + drSubJob["SubJobCode"].ToString() + "' DocketJobId='" + drSubJob["DocketJobId"].ToString() + "' DocketSubJobId='" + drSubJob["DocketSubJobId"].ToString() + "' DocketId='" + drSubJob["DocketId"].ToString() + "' RFQId='" + RFQId.ToString() + "' src='../Images/red_circle.png' alt='Owner Cost' style='float:left; display:" + ((drSubJob["CostCategory"].ToString() == "Y") ? "block" : "none") + ";' onclick='Update_CC(this);' /><span style='float:left; padding-left:5px;' Job='SubJob' JobCode='" + drSubJob["SubJobCode"].ToString() + "' DocketJobId='" + drJob1["DocketJobId"].ToString() + "' DocketSubJobId='" + drSubJob["DocketSubJobId"].ToString() + "' onclick='ShowJobDesc(this);' >" + drSubJob["SubJobCode"].ToString() + " : " + drSubJob["SubJobName"].ToString() + "</span><span style='clear:both;'></span></div>");
            //                sbleft.Append("</td></tr>");
            //            }
            //        }
            //    }
            //}
            
            //#endregion
        }
        sbleft.Append("</table>");
        sbdata.Append("</table>");

        litData.Text = sbdata.ToString();
        //litLeftHead.Text = sbleft.ToString();
           
    }
    public string FormatCurency(object data)
    {
        //return string.Format("{0:0.00}", Math.Round(Common.CastAsDecimal(data), 2));
        string ret = string.Format("{0:0}", Math.Round(Common.CastAsDecimal(data), 0));
            if(ret.ToString()=="0")
                ret="";
        return ret;
    }
    public DataTable getFilterdJobs(DataTable dt,string CatId)
    {
        DataView dv = dt.DefaultView;
        dv.RowFilter = "CatId=" + CatId;
        return dv.ToTable();
    }
    public DataTable getFilterdSubJobs(DataTable dt, string DocketJobId)
    {
        DataView dv = dt.DefaultView;
        dv.RowFilter = "DocketJobId=" + DocketJobId;
        return dv.ToTable();
    }
    public DataTable getFilterdSubJob_Single(DataTable dt,string DocketJobId,string DocketSubJobId)
    {
        DataView dv = dt.DefaultView;
        dv.RowFilter = "RFQId=" + RFQId + " And DocketJobId=" + DocketJobId + " And DocketSubJobId=" + DocketSubJobId;
        return dv.ToTable();
    }
    public DataTable BindJobs(Object CatId)
    {
        return Common.Execute_Procedures_Select_ByQuery("SELECT DocketJobId,JobId,JOBCODE,JOBNAME FROM DD_DocketJobs WHERE DOCKETID=" + DocketId + " And CatId=" + CatId + " ORDER BY JOBCODE");
    }
    public DataTable BindSubJobs(Object DocketJobId)
    {
        return Common.Execute_Procedures_Select_ByQuery("SELECT [DocketSubJobId],[DocketJobId],[DocketId],[SubJobCode],[SubJobName],AttachmentName,Unit FROM [dbo].[DD_DocketSubJobs] WHERE DOCKETID=" + DocketId + " And DocketJobId=" + DocketJobId + " ORDER BY SubJobCode");
    }
    protected void imgDownload_Click(object sender, EventArgs e)
    {
        //DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT AttachmentName,Attachment FROM DD_DocketSubJobs WHERE DOCKETID=" + DocketId.ToString() + " AND DOCKETJOBID=" + DocketJobId.ToString() + " AND DOCKETSUBJOBID=" + DocketSubJobId.ToString());
        //byte[] buff = (byte[])dt.Rows[0]["Attachment"];
        //Response.AppendHeader("Content-Disposition", "attachment; filename=" + dt.Rows[0]["AttachmentName"].ToString());
        //Response.BinaryWrite(buff);
        //Response.Flush();
        //Response.End();
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
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT *,(SELECT VESSELNAME FROM DBO.VESSEL V WHERE V.VESSELID=D.VESSELID) AS VESSELNAME,ISNULL((SELECT R.RFQID FROM DD_Docket_RFQ_Master R WHERE R.DOCKETID=D.DOCKETID AND R.STATUS='P'),0) AS PORFQId,(SELECT ExecFrom FROM DD_Docket_RFQ_Master Where DOCKETID=" + DocketId + " AND RFQId=" + RFQId + ") As ExecFrom,(SELECT ExecTo FROM DD_Docket_RFQ_Master Where DOCKETID=" + DocketId + " AND RFQId=" + RFQId + ") As ExecTo FROM DD_DocketMaster D WHERE DOCKETID=" + DocketId.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            lblDocketNo.Text = dt.Rows[0]["DocketNo"].ToString();
            lblVessel.Text = dt.Rows[0]["VESSELNAME"].ToString();
            lblType.Text = dt.Rows[0]["DocketType"].ToString();
            lblPlanDuration.Text = Common.ToDateString(dt.Rows[0]["ExecFrom"]) + " To " + Common.ToDateString(dt.Rows[0]["ExecTo"]);
        }
        
    }
    //protected void btn_SavePlanning_Click(object sender, EventArgs e)
    //{
    //    char[] sep={':'};
    //    DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT DocketJobId,JobId,JOBCODE FROM DD_Docket_RFQ_Jobs WHERE DOCKETID=" + DocketId + " AND RFQID=" + RFQId + " ORDER BY JOBCODE");
    //    foreach (DataRow dr in dt.Rows)
    //    {
    //        string JobCode = dr["JOBCODE"].ToString().Trim();
    //        string Dates=Convert.ToString(Request.Form["ctl_" + JobCode]);
    //        if(Dates!=null)
    //        if (Dates.Trim() != "")
    //        {

    //            string[] parts=Dates.Split(sep);
    //            string stdt=parts[0];
    //            string enddt = parts[1];

    //            try
    //            {
    //                DateTime dt1 = DateTime.Parse(stdt);
    //                DateTime dt2 = DateTime.Parse(enddt);

    //                if (dt2 < dt1)
    //                {
    //                    DateTime tmp =dt1;
    //                    dt1 = dt2;
    //                    dt2 = tmp;
    //                }

    //                Common.Execute_Procedures_Select_ByQuery("UPDATE DD_Docket_RFQ_Jobs SET PlanFrom='" + dt1.ToString("dd-MMM-yyyy") + "',PlanTo='" + dt2.ToString("dd-MMM-yyyy") + "' WHERE DOCKETID=" + DocketId + " And JOBCODE='" + JobCode + "'");
    //            }
    //            catch { }
    //        }
    //    }
    //    LoadCategory();
    //}
    protected void btnReLoadData_Click(object sender, EventArgs e)
    {
        LoadCategory();
    }
    protected void lnkJobDescr_Click(object sender, EventArgs e)
    {
        string SQL = "";

        if (txtJobType.Text.Trim() == "Job")
        {
            SQL = "SELECT [JobDesc] FROM DD_Docket_RFQ_Jobs WHERE RFQId= " + RFQId + " AND DocketId=" + DocketId + " AND DocketJobId=" + txtDocketJobId.Text.Trim();
        }
        else
        {
            SQL = "SELECT [SubJobName], [LongDescr] FROM DD_Docket_RFQ_SubJobs WHERE RFQId= " + RFQId + " AND DocketId=" + DocketId + " AND DocketJobId=" + txtDocketJobId.Text.Trim() + " AND DocketSubJobId = " + txtDocketSubJobId.Text.Trim();
        }

        DataTable dtDescr = Common.Execute_Procedures_Select_ByQuery(SQL);

        if (txtJobType.Text.Trim() == "Job")
        {
            dvSubJobDescr.Visible = false;
            txtJobDescr.Text = dtDescr.Rows[0]["JobDesc"].ToString();
            txtShortDescr.Text = "";
            txtLongDescr.Text = "";            
            dvJobDescr.Visible = true;  
        }
        else
        {             
            dvJobDescr.Visible = false;
            txtJobDescr.Text = "";
            txtShortDescr.Text = dtDescr.Rows[0]["SubJobName"].ToString();
            txtLongDescr.Text = dtDescr.Rows[0]["LongDescr"].ToString();
            dvSubJobDescr.Visible = true;
            
        }
    }
    protected void TotalYardCostPrint(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Yard Cost", "window.open('CostCategoryReport.aspx?Type=YC&RFQId=" + RFQId + "&DocketId=" + DocketId + "', '_blank', '');", true);
    }
    protected void TotalOwnerCostPrint(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Owner Cost", "window.open('CostCategoryReport.aspx?Type=OC&RFQId=" + RFQId + "&DocketId=" + DocketId + "', '_blank', '');", true);
    }
 }
