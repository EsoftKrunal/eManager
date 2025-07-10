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
using System.Web.Services;

public partial class Docket_SUP_Analysis1 : System.Web.UI.Page
{
    public int DocketId
    {
        get { return Common.CastAsInt32(ViewState["DocketId"]); }
        set { ViewState["DocketId"] = value; }
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
    public bool Executed
    {
        set { ViewState["Executed"] = value; }
        get { return Convert.ToBoolean(ViewState["Executed"]); }
    }    
    //-------------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------

        lblMsgMain.Text = "";
        if (!IsPostBack)
        {
            DocketId = Common.CastAsInt32(Request.QueryString["DocketId"]);
            ShowDocketSummary();
            Level = 1;
            BindJobCategory();
            BindJobs();
            LoadCategory();
        }
    }
    protected void btnLevel_Click(object sender, EventArgs e)
    {
        Level = Common.CastAsInt32(((Button)sender).CommandArgument);
        if(Level==3)
            ddlJobCategory.SelectedIndex = 1;
        LoadCategory();
    }
    public void BindJobCategory()
    {
        ddlJobCategory.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT CATID,CATCODE + ' : ' + CATNAME AS FULLNAME FROM DD_JobCategory ORDER BY CATCODE");
        ddlJobCategory.DataTextField = "FULLNAME";
        ddlJobCategory.DataValueField = "CATID";
        ddlJobCategory.DataBind();

        ddlJobCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< Select >", "0"));

       
    }
    public void BindJobs()
    {
        ddlJob.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT DocketJobId,(JOBCODE + ' : ' + JOBNAME ) AS JOBNAME FROM DD_DocketJobs WHERE CatId = " + ddlJobCategory.SelectedValue.Trim() + " AND  DOCKETID=" + DocketId + " ORDER BY JOBCODE");
        ddlJob.DataTextField = "JOBNAME";
        ddlJob.DataValueField = "DocketJobId";
        ddlJob.DataBind();

        ddlJob.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< Select >", "0"));
    }
    protected void ddlJobCategory_Click(object sender, EventArgs e)
    {
        BindJobs();
        LoadCategory();
    }
    protected void chkOut_OnCheckedChanged(object sender, EventArgs e)
    {
        LoadCategory();
    }
    protected void ddlJob_Click(object sender, EventArgs e)
    {
        LoadCategory();
    }
    protected void LoadCategory()
    {
        string Where = " WHERE DOCKETID=" + DocketId;
        int ColWith = 400;
        if (Level != 3)
            ColWith = 250;
        if (ddlJobCategory.SelectedIndex > 0 && ddlJob.SelectedIndex == 0)
        {
            Where += " AND DOCKETJOBID IN (SELECT DocketJobId FROM DD_DocketJobs WHERE DocketId=" + DocketId + " AND CatId=" + ddlJobCategory.SelectedValue.Trim() + ") ";
        }
        if (ddlJob.SelectedIndex > 0)
        {
            Where += " AND DOCKETJOBID =" + ddlJob.SelectedValue.Trim();
        }
        if (ddlYC.SelectedIndex != 0)
        {
            Where += " AND ISNULL(CostCategory,'N')='" + ddlYC.SelectedValue + "' ";
        }
        if (chkOut.Checked)
        {
            Where += " AND ISNULL(OutSideRepair,'N')='Y' ";
        }

        string CostCatClause = "";

        if (ddlYC.SelectedIndex != 0)
            CostCatClause = " AND ISNULL(CostCategory,'N')='" + ddlYC.SelectedValue + "' ";

        if (chkOut.Checked)
            CostCatClause += " AND ISNULL(OutSideRepair,'N')='Y' ";

        StringBuilder sbleft = new StringBuilder();
        StringBuilder sbcolheader1 = new StringBuilder();
        StringBuilder sbcolheader = new StringBuilder();
        StringBuilder sbcolfooter = new StringBuilder();
        StringBuilder sbdata = new StringBuilder();
        DataTable dtCats = Common.Execute_Procedures_Select_ByQuery("SELECT CatId,CatCode,CatName FROM DD_JobCategory " + (ddlJobCategory.SelectedIndex > 0 ? " WHERE CatId=" + ddlJobCategory.SelectedValue.Trim() : "") + " Order By CatCode");
        DataTable dtRFQs = Common.Execute_Procedures_Select_ByQuery("SELECT RFQID,RFQNO,YARDNAME,FinalDiscount,YardDiscount FROM DD_Docket_RFQ_Master INNER JOIN [dbo].[DD_YardMaster] YM ON YM.YARDID=DD_Docket_RFQ_Master.YARDID WHERE ACTIVEINACTIVE='A' AND STATUS IN ('P','Q') AND DOCKETID=" + DocketId + "");
        DataTable dtJobs_All = Common.Execute_Procedures_Select_ByQuery("SELECT CatId,DocketJobId,JobId,JOBCODE,JOBNAME FROM DD_DocketJobs WHERE DOCKETID=" + DocketId + ((ddlJobCategory.SelectedIndex > 0 && ddlJob.SelectedIndex == 0) ? " AND CatId=" + ddlJobCategory.SelectedValue.Trim() : "") + (ddlJob.SelectedIndex > 0 ? " AND DOCKETJOBID =" + ddlJob.SelectedValue.Trim() : "") + " ORDER BY JOBCODE");
        DataTable dtDocketSubJobs_All = Common.Execute_Procedures_Select_ByQuery("SELECT [DocketSubJobId],[DocketJobId],[DocketId],[SubJobCode],[SubJobName],AttachmentName,Unit FROM [dbo].[DD_DocketSubJobs] " + Where + " ORDER BY SubJobCode");
        DataTable dtRFQSubJobs_All = Common.Execute_Procedures_Select_ByQuery("SELECT RFQId,[DocketSubJobId],[DocketJobId],[DocketId],[SubJobCode],[SubJobName],AttachmentName,Unit,BidQty,SUPQty,SUPUnitPrice_USD,SUPDiscountPer,SUPNetAmount_USD,CostCategory FROM [dbo].[DD_Docket_RFQ_SubJobs] " + Where + " ORDER BY SubJobCode");



        //DataTable dtRFQs = Common.Execute_Procedures_Select_ByQuery("SELECT RFQID,RFQNO,YARDNAME FROM DD_Docket_RFQ_Master INNER JOIN [dbo].[DD_YardMaster] YM ON YM.YARDID=DD_Docket_RFQ_Master.YARDID WHERE DOCKETID=" + DocketId + " AND DD_Docket_RFQ_Master.Status <> 'I' ");
        
        
        //---------------
        #region Right Top ColumnHeader
        int c = 0;
        foreach (DataRow drRFQ in dtRFQs.Rows)
        {
            hfDicountedValue.Value = drRFQ["YardDiscount"].ToString();
            decimal TotalRFQSum = Common.CastAsDecimal(dtRFQSubJobs_All.Compute("SUM(SUPNetAmount_USD)", "RFQId=" + drRFQ["RFQId"].ToString()));
            


            decimal TotalRFQSum_Yard = Common.CastAsDecimal(dtRFQSubJobs_All.Compute("SUM(SUPNetAmount_USD)", "RFQId=" + drRFQ["RFQId"].ToString() + " And ISNULL(CostCategory,'N')='Y' "));
            decimal TotalRFQSum_YardAfterDiscount = TotalRFQSum_Yard - (TotalRFQSum_Yard *Common.CastAsDecimal(hfDicountedValue.Value)/100);

            decimal TotalRFQSum_NonYard = Common.CastAsDecimal(dtRFQSubJobs_All.Compute("SUM(SUPNetAmount_USD)", "RFQId=" + drRFQ["RFQId"].ToString() + " And ISNULL(CostCategory, 'N') = 'N'"));


            decimal TotalRFQSumAfterDiscount = (TotalRFQSum_YardAfterDiscount+ TotalRFQSum_NonYard);

            string radioSelection = "";
            //if (btnCreatePO.Visible)
            //    radioSelection = "<input type='radio' id='rad_" + drRFQ["RFQId"].ToString() + "' value='" + drRFQ["RFQId"].ToString() + "' name='rfqlist'></radio>";

            sbcolheader1.Append("<div style='width:" + ColWith.ToString() + "px;position:absolute;left:" + (c * ColWith) + "px;text-align:left;'>" + radioSelection + "<b><span style='cursor:pointer; color:red;' onclick='OpenRFQExcel(" + drRFQ["RFQId"].ToString() + ");' rfqid='" + drRFQ["RFQId"].ToString() + "'>" + drRFQ["RFQNO"] + "</span> : <span style='cursor:pointer; color:blue;' onclick='OpenRFQ(" + drRFQ["RFQId"].ToString() + ");' rfqid='" + drRFQ["RFQId"].ToString() + "'>" + drRFQ["YARDNAME"] + "</span></b></div>");
            sbcolheader.Append("<div style='width:" + ColWith.ToString() + "px;position:absolute;left:" + (c * ColWith) + "px;text-align:left;'>");
            sbcolheader.Append("<table cellspacing='0' rules='none' border='0' cellpadding='4' style='border-collapse:collapse; width:100%;'>");
            sbcolheader.Append("<tr style='background-color:#FFC266;'>");
            if (Level == 3)
            {
                sbcolheader.Append("<td style='width:60px;' class='right_grey_border right_align'>Bid Qty</td>");
                sbcolheader.Append("<td style='width:70px;' class='right_grey_border right_align'>SUP Qty</td>");
                sbcolheader.Append("<td style='width:90px;' class='right_grey_border right_align'>Unit Price($)</td>");
                sbcolheader.Append("<td style='width:60px;' class='right_grey_border right_align'>Disc(%)</td>");
            }
            sbcolheader.Append("<td class='right_grey_border right_align'>Total($)</td>");
            sbcolheader.Append("</tr>");
            sbcolheader.Append("</table>");
            sbcolheader.Append("</div>");


            sbcolfooter.Append("<div style='width:" + ColWith.ToString() + "px;position:absolute;left:" + (c * ColWith) + "px;text-align:left;'>");
            sbcolfooter.Append("<table cellspacing='0' rules='none' border='0' cellpadding='4' style='border-collapse:collapse;width:100%;'>");
            sbcolfooter.Append("<tr style='background-color:#FFC266;'>");
            if (Level == 3)
            {
                sbcolfooter.Append("<td style='width:60px;' class='right_grey_border right_align'>Owner :</td>");
                sbcolfooter.Append("<td style='width:70px;' class='right_grey_border right_align'><a href='#' onclick=\"OpenRFqPrint(" + drRFQ["RFQId"].ToString() + ",'N'," + Level + "," + ddlJobCategory.SelectedValue + "," + ddlJob.SelectedValue + ");\">" + TotalRFQSum_NonYard + "</a></td>");
                sbcolfooter.Append("<td style='width:90px;' class='right_grey_border right_align'>Yard :</td>");
                sbcolfooter.Append("<td style='width:60px;' class='right_grey_border right_align'><a href='#' onclick=\"OpenRFqPrint(" + drRFQ["RFQId"].ToString() + ",'Y'," + Level + "," + ddlJobCategory.SelectedValue + "," + ddlJob.SelectedValue + ");\">" + TotalRFQSum_Yard + "</a></td>");
            }
            sbcolfooter.Append("<td class='right_grey_border right_align'><span id='txtSUPNetAmount_USD_" + drRFQ["RFQId"].ToString() + "'><a href='#' onclick=\"OpenRFqPrint(" + drRFQ["RFQId"].ToString() + ",''," + Level + "," + ddlJobCategory.SelectedValue + "," + ddlJob.SelectedValue + ");\">" + TotalRFQSum + "</a></span></td>");            
            sbcolfooter.Append("</tr>");

            //For Total
            sbcolfooter.Append("<tr style='background-color:#FFC266;'>");
            if (Level == 3)
            {
                sbcolfooter.Append("<td style='width:60px;' class='right_grey_border right_align'></td>");
                sbcolfooter.Append("<td style='width:70px;' class='right_grey_border right_align'></td>");
                sbcolfooter.Append("<td style='width:90px;' class='right_grey_border right_align'></td>");
                sbcolfooter.Append("<td style='width:60px;' class='right_grey_border right_align'>" + " <input  class='number' type='text' style='font-size:11px;text-align:right; width:40px;height:10px;' rfqid='" + drRFQ["RFQId"].ToString() + "' onblur='SaveDiscount(this)' value='" + hfDicountedValue.Value + "'> " + "</td>");
            }

            if (Level == 3)
            {
                sbcolfooter.Append("<td class='right_grey_border right_align'><span id='txtSUPNetAmount_USD_" + drRFQ["RFQId"].ToString() + "'>"+ "</span></td>");
                sbcolfooter.Append("</tr>");
            }
            else
            {
                sbcolfooter.Append("<td class='right_grey_border right_align'><span id='txtSUPNetAmount_USD_" + drRFQ["RFQId"].ToString() + "'>" + " <input class='number' type='text' style='font-size:11px;text-align:right; width:40px;height:10px;' rfqid='" +
                drRFQ["RFQId"].ToString() + "' onblur='SaveDiscount(this)' value='" + hfDicountedValue.Value + "'> " + "</span></td>");
                sbcolfooter.Append("</tr>");
            }



            // For Net Total ----------------------------
            sbcolfooter.Append("<tr style='background-color:#FFC266;'>");
            if (Level == 3)
            {
                //sbcolfooter.Append("<td style='width:60px;' class='right_grey_border right_align'></td>");
                //sbcolfooter.Append("<td style='width:70px;' class='right_grey_border right_align'></td>");
                //sbcolfooter.Append("<td style='width:90px;' class='right_grey_border right_align'></td>");
                //sbcolfooter.Append("<td style='width:60px;' class='right_grey_border right_align'></td>");
                sbcolfooter.Append("<td style='width:60px;' class='right_grey_border right_align'>Owner :</td>");
                sbcolfooter.Append("<td style='width:70px;' class='right_grey_border right_align'><a href='#' onclick=\"OpenRFqPrint(" + drRFQ["RFQId"].ToString() + ",'N'," + Level + "," + ddlJobCategory.SelectedValue + "," + ddlJob.SelectedValue + ");\">" + TotalRFQSum_NonYard + "</a></td>");
                sbcolfooter.Append("<td style='width:90px;' class='right_grey_border right_align'>Yard :</td>");
                sbcolfooter.Append("<td style='width:60px;' class='right_grey_border right_align'><a href='#' onclick=\"OpenRFqPrint(" + drRFQ["RFQId"].ToString() + ",'Y'," + Level + "," + ddlJobCategory.SelectedValue + "," + ddlJob.SelectedValue + ");\">" + TotalRFQSum_YardAfterDiscount + "</a></td>");
            }
            sbcolfooter.Append("<td class='right_grey_border right_align' ><span style='' id='txtSUPNetAmount_USD_" + drRFQ["RFQId"].ToString() + "'><a href='#' onclick=\"OpenRFqPrint(" + drRFQ["RFQId"].ToString() + ",''," + Level + "," + ddlJobCategory.SelectedValue + "," + ddlJob.SelectedValue + ");\">" + TotalRFQSumAfterDiscount + "</a></span></td>");
            
            sbcolfooter.Append("</tr>");
            //----------------------------
            sbcolfooter.Append("</table>");
            sbcolfooter.Append("</div>");

            c++;
        }

        litColumnHeader1.Text = sbcolheader1.ToString();
        litColumnHeader.Text = sbcolheader.ToString();
        litColumnFooter.Text = sbcolfooter.ToString();
        #endregion
        //---------------

        foreach (DataRow drCat in dtCats.Rows)
        {
            // CATEGORY HEAD ROW
            sbleft.Append("<div name='tr_" + drCat["CatCode"].ToString() + "' class='hover_highlight_cat' style='border-right:solid 1px #e2e2e2;'>");
            sbleft.Append("<table cellspacing='0' rules='cols' border='0' cellpadding='0' style='border-collapse:collapse; width:100%; height:20px;'>");
            sbleft.Append("<tr>");
            sbleft.Append("<td>");
            sbleft.Append("<table cellspacing='0' rules='cols' border='0' cellpadding='0' style='border-collapse:collapse;width:100%;'>");
            sbleft.Append("<tr>");
            sbleft.Append("<td style='vertical-align:middle;overflow:auto;'><div style='float:left; height:17px;overflow:hidden;'>&nbsp;" + drCat["CatCode"].ToString() + " : " + drCat["CatName"].ToString() + "</div><div style='clear:both'></div>" + "</td>");
            sbleft.Append("</tr>");
            sbleft.Append("</table>");
            sbleft.Append("</td>");
            sbleft.Append("</tr>");
            sbleft.Append("</table>");
            sbleft.Append("</div>");

            c = 0;

            // DATA ROW
            sbdata.Append("<div name='tr_" + drCat["CatCode"].ToString() + "' class='hover_highlight_cat' style='width:" + (dtRFQs.Rows.Count * ColWith) + "px'>");
            sbdata.Append("<table cellspacing='0' rules='cols' border='0' cellpadding='0' style='border-collapse:collapse; width:100%; height:20px;'>");
            sbdata.Append("<tr>");
            c = 0;
            foreach (DataRow drRFQ in dtRFQs.Rows)
            {
                sbdata.Append("<td style='vertical-align:middle;'>");

                DataTable dtCatsSum = Common.Execute_Procedures_Select_ByQuery("SELECT ISNULL(SUM(SUPNetAmount_USD),0) as NetAmount_USD FROM DD_Docket_RFQ_SubJobs WHERE RFQID=" + drRFQ["RFQId"].ToString() + " AND DOCKETJOBID IN(select docketjobid from DD_Docket_RFQ_Jobs where docketid=" + DocketId + " and rfqid=" + drRFQ["RFQId"].ToString() + CostCatClause + " and catid=" + drCat["CatId"].ToString() + ")");

                sbdata.Append("<div style='width:" + ColWith.ToString() + "px;text-align:left;'>");
                sbdata.Append("<table cellspacing='0' rules='cols' border='0' cellpadding='4' style='border-collapse:collapse; width:100%;'>");
                sbdata.Append("<tr>");
                if (Level == 3)
                {
                    sbdata.Append("<td style='width:60px;' class='right_grey_border right_align'>&nbsp;</td>");
                    sbdata.Append("<td style='width:70px;' class='right_grey_border right_align'>&nbsp;</td>");
                    sbdata.Append("<td style='width:90px;' class='right_grey_border right_align'>&nbsp;</td>");
                    sbdata.Append("<td style='width:60px;' class='right_grey_border right_align'>&nbsp;</td>");
                }
                sbdata.Append("<td class='right_grey_border right_align'><span id='txtSUPNetAmount_USD_" + drRFQ["RFQId"].ToString() + "_" + drCat["CatCode"].ToString() + "'>" + dtCatsSum.Rows[0]["NetAmount_USD"].ToString() + "</span></td>");
                sbdata.Append("</tr>");
                sbdata.Append("</table>");
                sbdata.Append("</div>");
                sbdata.Append("</td>");
                c++;
            }
            sbdata.Append("</tr>");
            sbdata.Append("</table>");
            sbdata.Append("</div>");

            if (Level >= 2)//--------------------------------------------
            {
                #region job section

                DataTable dtJobs = getFilterdJobs(dtJobs_All, drCat["CatId"].ToString());
                foreach (DataRow drJob in dtJobs.Rows)
                {
                    sbdata.Append("<div class='hover_highlight_job' name='tr_" + drJob["JOBCODE"].ToString() + "' style='width:" + (dtRFQs.Rows.Count * ColWith) + "px;'>");
                    sbdata.Append("<table cellspacing='0' rules='cols' border='0' cellpadding='0' style='border-collapse:collapse; width:100%;height:20px;'>");
                    sbdata.Append("<tr>");
                    c = 0;
                    foreach (DataRow drRFQ in dtRFQs.Rows)
                    {
                        sbdata.Append("<td style='vertical-align:middle;'>");
                        DataTable dtJobSum = Common.Execute_Procedures_Select_ByQuery("SELECT ISNULL(SUM(SUPNetAmount_USD),0) as SUPNetAmount_USD FROM DD_Docket_RFQ_SubJobs WHERE RFQID=" + drRFQ["RFQId"].ToString() + CostCatClause + " AND DOCKETJOBID=" + drJob["DOCKETJOBID"].ToString());
                        sbdata.Append("<div style='width:" + ColWith.ToString() + "px;text-align:left;'>");
                        sbdata.Append("<table cellspacing='0' rules='none' border='0' cellpadding='4' style='border-collapse:collapse;width:100%;'>");
                        sbdata.Append("<tr style=''>");
                        if (Level == 3)
                        {
                            sbdata.Append("<td style='width:60px;' class='right_grey_border right_align'>&nbsp;</td>");
                            sbdata.Append("<td style='width:70px;' class='right_grey_border right_align'>&nbsp;</td>");
                            sbdata.Append("<td style='width:90px;' class='right_grey_border right_align'>&nbsp;</td>");
                            sbdata.Append("<td style='width:60px;' class='right_grey_border right_align'>&nbsp;</td>");
                        }
                        sbdata.Append("<td class='right_grey_border right_align'><span id='txtSUPNetAmount_USD_" + drRFQ["RFQId"].ToString() + "_" + drJob["JOBCODE"].ToString() + "'>" + dtJobSum.Rows[0]["SUPNetAmount_USD"] + "</span></td>");
                        sbdata.Append("</tr>");
                        sbdata.Append("</table>");
                        sbdata.Append("</div>");
                        sbdata.Append("</td>");
                        c++;
                    }
                    sbdata.Append("</tr>");
                    sbdata.Append("</table>");
                    sbdata.Append("</div>");
                    if (Level >= 3)//--------------------------------------------
                    {
                        //sub jobs
                        #region Subjob Region
                        DataTable dtSubJobs = getFilterdSubJobs(dtDocketSubJobs_All, drJob["DocketJobId"].ToString());
                        foreach (DataRow drSubJob in dtSubJobs.Rows)
                        {
                            sbdata.Append("<div class='hover_highlight_subjob' name='tr_" + drSubJob["SubJobCode"].ToString() + "' style='width:" + (dtRFQs.Rows.Count * ColWith) + "px'>");
                            sbdata.Append("<table cellspacing='0' rules='cols' border='0' cellpadding='0' style='border-collapse:collapse; width:100%;height:20px;'>");
                            sbdata.Append("<tr>");
                            c = 0;
                            foreach (DataRow drRFQ in dtRFQs.Rows)
                            {
                                sbdata.Append("<td style='vertical-align:middle;'>");
                                DataTable dtData = getFilterdSubJob_Single(dtRFQSubJobs_All, drRFQ["RFQId"].ToString(), drJob["DocketJobId"].ToString(), drSubJob["DocketSubJobId"].ToString());
                                if (dtData.Rows.Count > 0)
                                {
                                    sbdata.Append("<div style='width:" + ColWith.ToString() + "px;text-align:left;'>");
                                    sbdata.Append("<table cellspacing='0' rules='none' border='0' cellpadding='4' style='border-collapse:collapse;width:100%; height:20px;'>");
                                    sbdata.Append("<tr name='tr_" + drSubJob["SubJobCode"] + "' class='hover_highlight' >");
                                    sbdata.Append("<td style='width:60px;' class='right_grey_border right_align'>&nbsp;" + dtData.Rows[0]["BidQty"] + "</td>");
                                    sbdata.Append("<td style='width:70px;' class='right_grey_border right_align'>&nbsp;" + dtData.Rows[0]["SUPQty"] + "</td>");
                                    sbdata.Append("<td style='width:90px;' class='right_grey_border right_align'>&nbsp;" + dtData.Rows[0]["SUPUnitPrice_USD"] + "</td>");
                                    sbdata.Append("<td style='width:60px;' class='right_grey_border right_align'>&nbsp;" + dtData.Rows[0]["SUPDiscountPer"] + "</td>");
                                    sbdata.Append("<td class='right_grey_border right_align'>&nbsp;" + dtData.Rows[0]["SUPNetAmount_USD"] + "</td>");
                                    sbdata.Append("</tr>");
                                    sbdata.Append("</table>");
                                    sbdata.Append("</div>");
                                }
                                else
                                {
                                    sbdata.Append("<div style='width:" + ColWith.ToString() + "px;position:absolute;left:" + (c * ColWith) + "px;text-align:left;'>");
                                    sbdata.Append("<table cellspacing='0' rules='none' border='0' cellpadding='4' style='border-collapse:collapse;width:100%; height:20px;'>");
                                    sbdata.Append("<tr name='tr_" + drSubJob["SubJobCode"] + "' class='hover_highlight' >");
                                    sbdata.Append("<td style='width:60px;' class='right_grey_border right_align'>&nbsp;</td>");
                                    sbdata.Append("<td style='width:70px;' class='right_grey_border right_align'>&nbsp;</td>");
                                    sbdata.Append("<td style='width:90px;' class='right_grey_border right_align'>&nbsp;</td>");
                                    sbdata.Append("<td style='width:60px;' class='right_grey_border right_align'>&nbsp;</td>");
                                    sbdata.Append("<td class='right_grey_border right_align'>&nbsp;</td>");
                                    sbdata.Append("</tr>");
                                    sbdata.Append("</table>");
                                    sbdata.Append("</div>");
                                }
                                sbdata.Append("</td>");
                                c++;
                            }
                            sbdata.Append("</tr>");
                            sbdata.Append("</table>");
                            sbdata.Append("</div>");
                        }
                        #endregion
                    }
                }
                #endregion
            }
            //-------------------------
            #region Left Column Data
            if (Level >= 2)//--------------------------------------------
            {
                DataTable dtJobs2 = getFilterdJobs(dtJobs_All, drCat["CatId"].ToString());
                foreach (DataRow drJob1 in dtJobs2.Rows) 
                {
                    sbleft.Append("<div name='tr_" + drJob1["JOBCODE"].ToString() + "' class='hover_highlight_job' style='border-right:solid 1px #e2e2e2;'><table cellspacing='0' rules='cols' border='0' cellpadding='3' style='border-collapse:collapse;height:20px; width:100%;'><tr><td><div style='float:left; height:17px;overflow:hidden;width:375px;'>&nbsp;" + drJob1["JOBCODE"].ToString() + " : " + drJob1["JOBNAME"].ToString() + "</div><div style='clear:both'></div></td></tr></table></div>");

                    if (Level >= 3)//--------------------------------------------
                    {
                        // SUB JOB ROW
                        DataTable dtSubJobs = getFilterdSubJobs(dtDocketSubJobs_All, drJob1["DocketJobId"].ToString());
                        foreach (DataRow drSubJob in dtSubJobs.Rows)
                        {
                            string editdiv="";
                            if (!Executed)
                                editdiv = "<img src='../Images/editX12.jpg' style='float:right;cursor:pointer;' title='Make Adjustments.' docketid='" + DocketId.ToString() + "' subjobcode='" + drSubJob["SubJobCode"].ToString() + "' onclick='ShowAdjustmentPanel(this);' />";

                            sbleft.Append("<div name='tr_" + drSubJob["SubJobCode"].ToString() + "' class='hover_highlight_subjob' style='border-right:solid 1px #e2e2e2;overflow:hidden;height:20px; " + ((hfSJCode.Value.Trim() == drSubJob["SubJobCode"].ToString().Trim()) ? "background-color:#ADEBAD;" : "") + "'><table cellspacing='0' rules='cols' border='0' cellpadding='3' style='border-collapse:collapse;height:20px; width:100%;'><tr><td><div style='float:left; height:17px;overflow:hidden;width:375px;'>&nbsp;" + drSubJob["SubJobCode"].ToString() + " : " + drSubJob["SubJobName"].ToString() + "</div>" + editdiv + "<div style='clear:both'></div></td></tr></table></div>");
                        }
                    }
                }
            }
            litData.Text = sbdata.ToString();
            litLeftHead.Text = sbleft.ToString();
            #endregion
        }
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
    public DataTable getFilterdSubJob_Single(DataTable dt,string RFQId, string DocketJobId,string DocketSubJobId)
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
    protected void btnCreatePO_Click(object sender, EventArgs e)
    {
        //int RFQId=Common.CastAsInt32(Request.Form["rfqlist"]);
        //if(RFQId>0)
        //{
        //    string SQL = "UPDATE DD_Docket_RFQ_Master SET STATUS = 'P' WHERE RFQId=" + RFQId;
        //    string SQL1 ="UPDATE DD_Docket_RFQ_SubJobs SET POQty=QuoteQty,POGrossAmount=GrossAmount,PODiscountPer=DiscountPer,PONetAmount=NetAmount WHERE RFQId=" + RFQId;
        //    Common.Execute_Procedures_Select_ByQuery(SQL);
        //    Common.Execute_Procedures_Select_ByQuery(SQL1);
        //    btnCreatePO.Visible = false; 
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "111", "alert('PO Created successfully.');", true);
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "111", "alert('Please select RFQ#.');", true);
        //}
    }
    public void ShowDocketSummary()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT *,(SELECT VESSELNAME FROM DBO.VESSEL V WHERE V.VESSELID=D.VESSELID) AS VESSELNAME,ISNULL((SELECT R.RFQID FROM DD_Docket_RFQ_Master R WHERE R.DOCKETID=D.DOCKETID AND R.STATUS='P'),0) AS PORFQId,SupNotifyOn,ApprovalOn,GMSupApprovalOn FROM DD_DocketMaster D WHERE DOCKETID=" + DocketId.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            lblDocketNo.Text = dt.Rows[0]["DocketNo"].ToString();
            lblVessel.Text = dt.Rows[0]["VESSELNAME"].ToString();
            lblType.Text = dt.Rows[0]["DocketType"].ToString();
            lblPlanDuration.Text = Common.ToDateString(dt.Rows[0]["StartDate"]) + " To " + Common.ToDateString(dt.Rows[0]["EndDate"]);
            Executed = Common.ToDateString(dt.Rows[0]["act_startdate"]) != "";
        
            //----------------------------
            btnNotifyToGM.Visible = false;
            btnGMApproval.Visible = false;

            DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DD_Docket_RFQ_Master R WHERE R.DOCKETID=" + DocketId);
            DataTable dtPosition = Common.Execute_Procedures_Select_ByQuery("select position from dbo.Hr_PersonalDetails where userid=" + Session["loginid"].ToString());
            int PositionId=0;

            if (dtPosition.Rows.Count > 0)
            {
                PositionId = Common.CastAsInt32(dtPosition.Rows[0][0]);
            }
            
            btnNotifyToGM.Visible = Convert.IsDBNull(dt.Rows[0]["GMSupApprovalOn"]);

            if (dt1.Rows.Count > 0)
            {
                bool GM = (PositionId == 4 || PositionId == 1 || PositionId == 89);
                btnGMApproval.Visible = Convert.IsDBNull(dt.Rows[0]["GMSupApprovalOn"]) && (!(Convert.IsDBNull(dt.Rows[0]["SupNotifyOn"]))) && GM;
            }
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        LoadCategory();
    }
    protected void btnAskPrint_Click(object sender, EventArgs e)
    {
        rptRFQ.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT RFQID,RFQNO,YARDNAME from DD_Docket_RFQ_Master R INNER JOIN DD_YardMaster YM ON R.YARDID=YM.YARDID WHERE ACTIVEINACTIVE='A' AND STATUS IN ('P','Q') AND DOCKETID=" + DocketId.ToString());
        rptRFQ.DataBind();
        dv_RFQ.Visible = true;
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string[] rlist = new string[4];
        int key = 0;
        foreach (RepeaterItem ri in rptRFQ.Items)
        {
            CheckBox ch = ((CheckBox)ri.FindControl("chkSelect"));
            if (ch.Checked)
            {
                int _RFQid = Common.CastAsInt32(ch.Attributes["arg"]);
                rlist[key] = _RFQid.ToString();
                key++;
                if (key == 4)
                    break;
            }
        }
        for (int i = key; i <= 3; i++)
        {
            rlist[i] = "0";
        }
        //------------------------------------

        ScriptManager.RegisterStartupScript(this, this.GetType(), "fsadfs", "window.open('PrintRFQComparison.aspx?Level=" + Level + "&Sup=1&Compare=1&Mode=" + radMode.SelectedValue + "&Param=" + DocketId.ToString() + "," + string.Join(",", rlist) + "');", true);
        //dv_RFQ.Visible = false;
    }
    protected void btnClosePrint_Click(object sender, EventArgs e)
    {
        dv_RFQ.Visible = false;
    }
    protected void btnNotifyGM_Click(object sender, EventArgs e)
    {
        //-------------------
        int LoginId = Common.CastAsInt32(Session["loginid"].ToString());
        Common.Execute_Procedures_Select_ByQuery("UPDATE DD_DocketMaster SET SupNotified=1,SupNotifyBy='" + Session["FullName"].ToString() + "',SupNotifyOn=GETDATE(),NotifyRemarks='' WHERE DOCKETID=" + DocketId.ToString());
        string[] ToAdd={"emanager@energiossolutions.com","emanager@energiossolutions.com"};
        //string[] ToAdd = { "pankaj.k@esoftech.com", "emanager@energiossolutions.com" };
        DataTable dtEmail = Common.Execute_Procedures_Select_ByQuery("select Email from dbo.userlogin where loginid=" + LoginId);
        string selfemail="";
        if(dtEmail.Rows.Count>0)
            if(dtEmail.Rows[0][0].ToString().Trim()!="")
                selfemail= dtEmail.Rows[0][0].ToString();

        string[] CCAdd={selfemail};
        string[] NoAdd={};
        string error;
        //ProjectCommon.SendeMail_MTM(LoginId,"emanager@energiossolutions.com", "emanager@energiossolutions.com", ToAdd, CCAdd, NoAdd, "Yard Comparison Approval Request", "Dear Sir,<br/>Please approve the docket.<br/> Thanks", out error, "");
        EProjectCommon.SendeMail_MTM(LoginId, "emanager@energiossolutions.com", "emanager@energiossolutions.com", ToAdd, CCAdd, NoAdd, lblVessel.Text + " : DD Quote Analysis", "Dear Sir,<br/><br/>Quote analysis of Drydock for " + lblVessel.Text + " is completed. Please approve the quote analysis ( docket no. " + lblDocketNo.Text + " ) to confirm the yard.<br/><br/>Thanks<br/>-------", out error, "");
        //------------------------ SEND MAIL TO GM FOR DOCKET NOTIFY
        lblMsgMain.Text = "GM notified successfully";
    }
    protected void btnGMApproval_Click(object sender, EventArgs e)
    {
        //-------------------
        Common.Execute_Procedures_Select_ByQuery("UPDATE DD_DocketMaster SET GMSupApproval=1,GMSupApprovalBy='" + Session["FullName"].ToString() + "',GMSupApprovalOn=GETDATE(),GMSupApprovalRemarks='' WHERE DOCKETID=" + DocketId.ToString());
        //------------------------ SEND MAIL TO GM FOR DOCKET NOTIFY
        lblMsgMain.Text = "Approval completed successfully";
    }

    //-- -------------------
    [WebMethod()]
    public static string Update_discount(int rfqid,  string val)
    {
        string sqlrate = " update DD_Docket_RFQ_Master  set discount="+ val + " where RFQId= "+ rfqid.ToString();
        DataTable dtreate = Common.Execute_Procedures_Select_ByQuery(sqlrate);

        //string message = "{'priceLC':'" + priceLC + "','priceUSD':'" + priceUSD + "'}";
        //return message.Replace("'", "\"");
        return "S";


    }

    protected void btnTempClick_OnClick(object sender, EventArgs e)
    {
        //string sqlrate = " update DD_Docket_RFQ_Master  set FinalDiscount=" + hfDicountedValue.Value + " where RFQId= " + hfRfqIDToUpdate.Value;
        string sqlrate = " update DD_Docket_RFQ_Master  set YardDiscount=" + Common.CastAsDecimal(hfDicountedValue.Value) + " where RFQId= " + hfRfqIDToUpdate.Value;
        DataTable dtreate = Common.Execute_Procedures_Select_ByQuery(sqlrate);
        LoadCategory();
    }
    

}
