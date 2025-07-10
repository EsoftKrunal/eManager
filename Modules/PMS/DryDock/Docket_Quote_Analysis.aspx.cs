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

public partial class Docket_Quote_Analysis : System.Web.UI.Page
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
    //-------------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        // -------------------------- SESSION CHECK ----------------------------------------------
        ProjectCommon.SessionCheck();
        // -------------------------- SESSION CHECK END ----------------------------------------------

        lblMsgMain.Text = "";
        if (!IsPostBack)
        {
            //btnCreatePO.Visible = false;
            DocketId = Common.CastAsInt32(Request.QueryString["DocketId"]);
            ShowDocketSummary();
            Level = 1;
            BindJobCategory();
            ddlJob.Items.Insert(0, new System.Web.UI.WebControls.ListItem("< Select >", "0"));
            LoadCategory();
        }
    }
    protected void btnLevel_Click(object sender, EventArgs e)
    {
        Level = Common.CastAsInt32(((Button)sender).CommandArgument);
        LoadCategory();
    }
    protected void ddlLevel_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        Level = Common.CastAsInt32(ddlLevel.SelectedValue);
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
        foreach(RepeaterItem ri in rptRFQ.Items)
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
        for(int i=key;i<=3;i++)
        {
            rlist[i]="0";
        }
        //------------------------------------

        ScriptManager.RegisterStartupScript(this, this.GetType(), "fsadfs", "window.open('PrintRFQComparison.aspx?Compare=1&Mode=Y&Param=" + DocketId.ToString() + "," + string.Join(",", rlist) + "');", true);
        //dv_RFQ.Visible = false;
    }
    protected void btnClosePrint_Click(object sender, EventArgs e)
    {
        dv_RFQ.Visible = false;
    }
    protected void LoadCategory()
    {
        string Where = " WHERE ISNULL(CostCategory,'N')='Y' AND DOCKETID=" + DocketId;
        if (ddlJobCategory.SelectedIndex > 0 && ddlJob.SelectedIndex == 0)
        {
            Where += " AND DOCKETJOBID IN (SELECT DocketJobId FROM DD_DocketJobs WHERE DocketId=" + DocketId + " AND CatId=" + ddlJobCategory.SelectedValue.Trim() + ") "; 
        }
        if (ddlJob.SelectedIndex > 0)
        {
            Where += " AND DOCKETJOBID =" + ddlJob.SelectedValue.Trim();
        }

        StringBuilder sbleft = new StringBuilder();
        StringBuilder sbcolheader1 = new StringBuilder();
        StringBuilder sbcolheader = new StringBuilder();
        StringBuilder sbcolfooter = new StringBuilder();
        StringBuilder sbdata = new StringBuilder();
        DataTable dtCats = Common.Execute_Procedures_Select_ByQuery("SELECT CatId,CatCode,CatName FROM DD_JobCategory " + (ddlJobCategory.SelectedIndex > 0 ? " WHERE CatId=" + ddlJobCategory.SelectedValue.Trim() : "" ) + " Order By CatCode");
        DataTable dtRFQs = Common.Execute_Procedures_Select_ByQuery("SELECT RFQID,RFQNO,YARDNAME FROM DD_Docket_RFQ_Master INNER JOIN [dbo].[DD_YardMaster] YM ON YM.YARDID=DD_Docket_RFQ_Master.YARDID WHERE DOCKETID=" + DocketId + " AND DD_Docket_RFQ_Master.Status <> 'I' ");
        DataTable dtJobs_All = Common.Execute_Procedures_Select_ByQuery("SELECT CatId,DocketJobId,JobId,JOBCODE,JOBNAME FROM DD_DocketJobs WHERE DOCKETID=" + DocketId + ((ddlJobCategory.SelectedIndex > 0 && ddlJob.SelectedIndex == 0)? " AND CatId=" + ddlJobCategory.SelectedValue.Trim() : "") + ( ddlJob.SelectedIndex > 0 ? " AND DOCKETJOBID =" + ddlJob.SelectedValue.Trim() : "") + " ORDER BY JOBCODE");
        DataTable dtDocketSubJobs_All = Common.Execute_Procedures_Select_ByQuery("SELECT [DocketSubJobId],[DocketJobId],[DocketId],[SubJobCode],left([SubJobName],100) + '...' as SubJobName,AttachmentName,Unit FROM [dbo].[DD_DocketSubJobs] " + Where + " ORDER BY SubJobCode");
        DataTable dtRFQSubJobs_All = Common.Execute_Procedures_Select_ByQuery("SELECT RFQId,[DocketSubJobId],[DocketJobId],[DocketId],[SubJobCode],[SubJobName],AttachmentName,Unit,BidQty,QuoteQty,UnitPrice_USD,DiscountPer,NetAmount_USD,CostCategory FROM [dbo].[DD_Docket_RFQ_SubJobs] " + Where + " ORDER BY SubJobCode");
        //---------------

        #region Right Top ColumnHeader
        int c = 0;
        foreach (DataRow drRFQ in dtRFQs.Rows)
        {
            decimal TotalRFQSum = Common.CastAsDecimal(dtRFQSubJobs_All.Compute("SUM(NetAmount_USD)", "RFQId=" + drRFQ["RFQId"].ToString() + " And ISNULL(CostCategory,'N')='Y' "));
            //decimal TotalRFQSum_Yard = Common.CastAsDecimal(dtRFQSubJobs_All.Compute("SUM(NetAmount_USD)", "RFQId=" + drRFQ["RFQId"].ToString() + " And ISNULL(CostCategory,'N')='Y' "));
            //decimal TotalRFQSum_NonYard = Common.CastAsDecimal(dtRFQSubJobs_All.Compute("SUM(NetAmount_USD)", "RFQId=" + drRFQ["RFQId"].ToString() + " And ISNULL(CostCategory, 'N') = 'N'"));
            
            string radioSelection = "";
            //if (btnCreatePO.Visible)
            //    radioSelection = "<input type='radio' id='rad_" + drRFQ["RFQId"].ToString() + "' value='" + drRFQ["RFQId"].ToString() + "' name='rfqlist'></radio>";

            //sbcolheader1.Append("<div style='width:400px;position:absolute;left:" + (c * 400) + "px;text-align:left;color:white;'>" + radioSelection + "<b>" + drRFQ["RFQNO"] + " : " + drRFQ["YARDNAME"] + "</b></div>");
            sbcolheader1.Append("<div style='width:400px;position:absolute;left:" + (c * 400) + "px;text-align:center;color:Blue;'>" + radioSelection + "<b>" + drRFQ["YARDNAME"] + "</b></div>");

            sbcolheader.Append("<div style='width:400px;position:absolute;left:" + (c * 400) + "px;text-align:left;'>");
            sbcolheader.Append("<table cellspacing='0' rules='cols' border='1' cellpadding='4' style='border-collapse:collapse;'>");
            sbcolheader.Append("<tr style='background-color:#FFC266;'>");
            sbcolheader.Append("<td style='width:60px; text-align:right;'>Bid Qty</td>");
            sbcolheader.Append("<td style='width:100px; text-align:right;'>Quote Qty</td>");
            sbcolheader.Append("<td style='width:100px; text-align:right;'>Unit Price($)</td>");
            sbcolheader.Append("<td style='width:60px; text-align:right;'>Disc(%)</td>");
            sbcolheader.Append("<td style='width:80px; text-align:right;'>Total($)</td>");
            sbcolheader.Append("</tr>");
            sbcolheader.Append("</table>");
            sbcolheader.Append("</div>");

            sbcolfooter.Append("<div style='width:400px;position:absolute;left:" + (c * 400) + "px;text-align:left;'>");
            sbcolfooter.Append("<table cellspacing='0' rules='cols' border='1' cellpadding='4' style='border-collapse:collapse;'>");
            sbcolfooter.Append("<tr style='background-color:#FFC266;'>");
            sbcolfooter.Append("<td style='width:60px; text-align:right;'></td>");
            sbcolfooter.Append("<td style='width:100px; text-align:left;'></td>");
            sbcolfooter.Append("<td style='width:60px; text-align:right;'></td>");
            sbcolfooter.Append("<td style='width:100px; text-align:left;'>Total :</td>");
            sbcolfooter.Append("<td style='width:80px; text-align:right;'><a href='#' onclick=\"OpenRFqPrint(" + drRFQ["RFQId"].ToString() + ",'Y'," + ddlLevel.SelectedValue + "," + ddlJobCategory.SelectedValue + "," + ddlJob.SelectedValue + ");\">" + TotalRFQSum + "</a></td>");
            sbcolfooter.Append("</tr>");
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
            
            sbleft.Append("<div name='tr_" + drCat["CatCode"].ToString() + "' class='hover_highlight_cat'>");
            sbleft.Append("<table cellspacing='0' rules='cols' border='0' cellpadding='0' style='border-collapse:collapse; width:100%;'>");
            sbleft.Append("<tr>");
            sbleft.Append("<td>");
            sbleft.Append("<table cellspacing='0' rules='cols' border='0' cellpadding='0' style='border-collapse:collapse;width:100%;'>");
            sbleft.Append("<tr>");
            sbleft.Append("<td>&nbsp;" + drCat["CatCode"].ToString() + " : " + drCat["CatName"].ToString() + "</td>");
            sbleft.Append("</tr>");
            sbleft.Append("</table>");
            sbleft.Append("</td>");
            sbleft.Append("</tr>");
            sbleft.Append("</table>");
            sbleft.Append("</div>");

            // DATA ROW
            sbdata.Append("<div name='tr_" + drCat["CatCode"].ToString() + "' class='hover_highlight_cat' style='width:" + (dtRFQs.Rows.Count * 400) + "px'>");
                sbdata.Append("<table cellspacing='0' rules='cols' border='0' cellpadding='0' style='border-collapse:collapse; width:100%;'>");
                    sbdata.Append("<tr>");
                    c = 0;
                    foreach (DataRow drRFQ in dtRFQs.Rows)
                    {
                        sbdata.Append("<td>");
                        DataTable dtCatsSum = Common.Execute_Procedures_Select_ByQuery("SELECT ISNULL(SUM(NetAmount_USD),0) as NetAmount_USD FROM DD_Docket_RFQ_SubJobs WHERE RFQID=" + drRFQ["RFQId"].ToString() + " AND DOCKETJOBID IN(select docketjobid from DD_Docket_RFQ_Jobs where docketid=" + DocketId + " and rfqid=" + drRFQ["RFQId"].ToString() + " and catid=" + drCat["CatId"].ToString() + ")");
                        sbdata.Append("<table cellspacing='0' rules='cols' border='0' cellpadding='0' style='border-collapse:collapse; width:100%;'>");
                        sbdata.Append("<tr>");
                        sbdata.Append("<td style='width:60px; text-align:right;border-left:solid 1px #c2c2c2;'></td>");
                        sbdata.Append("<td style='width:100px; text-align:right;'></td>");
                        sbdata.Append("<td style='width:100px; text-align:right;'></td>");
                        sbdata.Append("<td style='width:60px; text-align:right;'></td>");
                        sbdata.Append("<td style='width:80px; text-align:right;'>" + dtCatsSum.Rows[0]["NetAmount_USD"].ToString() + "</td>");
                        sbdata.Append("</tr>");
                        sbdata.Append("</table>");
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
                    sbdata.Append("<div class='hover_highlight_job' name='tr_" + drJob["JOBCODE"].ToString() + "' style='width:" + (dtRFQs.Rows.Count * 400) + "px'>");
                    sbdata.Append("<table cellspacing='0' rules='cols' border='0' cellpadding='0' style='border-collapse:collapse; width:100%;'>");
                    sbdata.Append("<tr>");
                    c = 0;
                    foreach (DataRow drRFQ in dtRFQs.Rows)
                    {
                        sbdata.Append("<td>");
                        DataTable dtJobSum = Common.Execute_Procedures_Select_ByQuery("SELECT ISNULL(SUM(NetAmount_USD),0) as NetAmount_USD FROM DD_Docket_RFQ_SubJobs WHERE RFQID=" + drRFQ["RFQId"].ToString() + " AND DOCKETJOBID=" + drJob["DOCKETJOBID"].ToString());
                        sbdata.Append("<div style='width:400px;text-align:left;'>");
                        sbdata.Append("<table cellspacing='0' rules='none' border='0' cellpadding='4' style='border-collapse:collapse;'>");
                        sbdata.Append("<tr style=''>");
                        sbdata.Append("<td style='width:60px; text-align:right;border-left:solid 1px #c2c2c2;'></td>");
                        sbdata.Append("<td style='width:100px; text-align:right;'></td>");
                        sbdata.Append("<td style='width:100px; text-align:right;'></td>");
                        sbdata.Append("<td style='width:60px; text-align:right;'></td>");
                        sbdata.Append("<td style='width:80px; text-align:right;'>" + dtJobSum.Rows[0]["NetAmount_USD"].ToString() + "</td>");
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
                            sbdata.Append("<div class='hover_highlight_subjob' name='tr_" + drSubJob["SubJobCode"].ToString() + "' style='width:" + (dtRFQs.Rows.Count * 400) + "px'>");
                            sbdata.Append("<table cellspacing='0' rules='cols' border='0' cellpadding='0' style='border-collapse:collapse; width:100%;'>");
                            sbdata.Append("<tr>");
                            c = 0;
                            foreach (DataRow drRFQ in dtRFQs.Rows)
                            {
                                sbdata.Append("<td>");
                                DataTable dtData = getFilterdSubJob_Single(dtRFQSubJobs_All, drRFQ["RFQId"].ToString(), drJob["DocketJobId"].ToString(), drSubJob["DocketSubJobId"].ToString());
                                if (dtData.Rows.Count > 0)
                                {
                                    sbdata.Append("<div style='width:400px;text-align:left;'>");
                                    sbdata.Append("<table cellspacing='0' rules='none' border='0' cellpadding='4' style='border-collapse:collapse;'>");
                                    sbdata.Append("<tr name='tr_" + drSubJob["SubJobCode"] + "' class='hover_highlight' >");
                                    sbdata.Append("<td style='width:60px;text-align:right;border-left:solid 1px #c2c2c2;'>" + dtData.Rows[0]["BidQty"] + "&nbsp;</td>");
                                    sbdata.Append("<td style='width:100px;text-align:right;'>" + dtData.Rows[0]["QuoteQty"] + "&nbsp;</td>");
                                    sbdata.Append("<td style='width:100px;text-align:right;'>" + dtData.Rows[0]["UnitPrice_USD"] + "&nbsp;</td>");
                                    sbdata.Append("<td style='width:60px;text-align:right;'>" + dtData.Rows[0]["DiscountPer"] + "&nbsp;</td>");
                                    sbdata.Append("<td style='width:80px;text-align:right;'>" + dtData.Rows[0]["NetAmount_USD"] + "&nbsp;</td>");
                                    sbdata.Append("</tr>");
                                    sbdata.Append("</table>");
                                    sbdata.Append("</div>");
                                }
                                else
                                {
                                    sbdata.Append("<div style='width:400px;position:absolute;left:" + (c * 400) + "px;text-align:left;'>");
                                    sbdata.Append("<table cellspacing='0' rules='none' border='0' cellpadding='4' style='border-collapse:collapse;'>");
                                    sbdata.Append("<tr name='tr_" + drSubJob["SubJobCode"] + "' class='hover_highlight' >");
                                    sbdata.Append("<td style='width:60px;text-align:right;border-left:solid 1px #c2c2c2;'>&nbsp;</td>");
                                    sbdata.Append("<td style='width:100px;text-align:right;'>&nbsp;</td>");
                                    sbdata.Append("<td style='width:100px;text-align:right;'>&nbsp;</td>");
                                    sbdata.Append("<td style='width:60px;text-align:right;'>&nbsp;</td>");
                                    sbdata.Append("<td style='width:80px;text-align:right'>&nbsp;</td>");
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
                    sbleft.Append("<div name='tr_" + drJob1["JOBCODE"].ToString() + "' class='hover_highlight_job' style=''><table cellspacing='0' rules='cols' border='0' cellpadding='3' style='border-collapse:collapse;'><tr><td>&nbsp;" + drJob1["JOBCODE"].ToString() + " : " + drJob1["JOBNAME"].ToString() + "</td></tr></table></div>");

                    if (Level >= 3)//--------------------------------------------
                    {
                        // SUB JOB ROW
                       DataTable dtSubJobs = getFilterdSubJobs(dtDocketSubJobs_All, drJob1["DocketJobId"].ToString());
                        foreach (DataRow drSubJob in dtSubJobs.Rows)
                        {
                            sbleft.Append("<div name='tr_" + drSubJob["SubJobCode"].ToString() + "' class='hover_highlight_subjob' style='text-overflow: hidden; overflow:hidden; '><table cellspacing='0' rules='cols' border='0' cellpadding='4' style='border-collapse:collapse;'><tr><td>&nbsp;" + drSubJob["SubJobCode"].ToString() + " : " + drSubJob["SubJobName"].ToString() + "</td></tr></table></div>");
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
    //protected void btnCreatePO_Click(object sender, EventArgs e)
    //{
    //    int RFQId=Common.CastAsInt32(Request.Form["rfqlist"]);
    //    if(RFQId>0)
    //    {
    //        Common.Set_Procedures("[dbo].[DD_CreatePO]");
    //        Common.Set_ParameterLength(1);
    //        Common.Set_Parameters(new MyParameter("@RFQId", RFQId));
    //        DataSet ds = new DataSet();
    //        ds.Clear();
    //        Boolean res;
    //        res = Common.Execute_Procedures_IUD(ds);
    //        if (res)
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "111", "alert('PO Created successfully.');", true);
    //            btnCreatePO.Visible = false; 
    //        }
    //        else
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "111", "alert('Unable to create PO.');", true);
    //        }
           
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "111", "alert('Please select RFQ#.');", true);
    //    }
    //}
    
    public void ShowDocketSummary()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT *,(SELECT VESSELNAME FROM DBO.VESSEL V WHERE V.VESSELID=D.VESSELID) AS VESSELNAME,ISNULL((SELECT R.RFQID FROM DD_Docket_RFQ_Master R WHERE R.DOCKETID=D.DOCKETID AND R.STATUS='P'),0) AS PORFQId FROM DD_DocketMaster D WHERE DOCKETID=" + DocketId.ToString());
        if (dt != null && dt.Rows.Count > 0)
        {
            lblDocketNo.Text = dt.Rows[0]["DocketNo"].ToString();
            lblVessel.Text = dt.Rows[0]["VESSELNAME"].ToString();
            lblType.Text = dt.Rows[0]["DocketType"].ToString();
            lblPlanDuration.Text = Common.ToDateString(dt.Rows[0]["StartDate"]) + " To " + Common.ToDateString(dt.Rows[0]["EndDate"]);
            //btnCreatePO.Visible = Common.CastAsInt32(dt.Rows[0]["PORFQId"]) <= 0;
        }
        
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

    protected void ddlJob_Click(object sender, EventArgs e)
    {
        LoadCategory();
    }
    
 }
