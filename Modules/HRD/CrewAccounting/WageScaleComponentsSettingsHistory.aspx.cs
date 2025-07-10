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
using ShipSoft.CrewManager.BusinessObjects;
using ShipSoft.CrewManager.BusinessLogicLayer;
using ShipSoft.CrewManager.Operational;
using System.IO;
using System.Activities.Statements;

public partial class Modules_HRD_CrewAccounting_WageScaleComponentsSettingsHistory : System.Web.UI.Page
{

    public int WageScaleRankId
    {
        get { return Common.CastAsInt32(ViewState["WageScaleRankId"]); }
        set { ViewState["WageScaleRankId"] = value; }
    }

    public int WageScaleId
    {
        get { return Common.CastAsInt32(ViewState["WageScaleId"]); }
        set { ViewState["WageScaleId"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //-----------------------------
        SessionManager.SessionCheck_New();
        //-----------------------------
        if (!IsPostBack)
        {
            WageScaleRankId = Common.CastAsInt32(Request.QueryString["WageScaleRankId"]);
            BindRepeater();
            BindWageScaleRankDetailsHistory();
            //Bind_RankHistory();
        }
    }

    protected void BindRepeater()
    {
        //----------------------------
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select WRH.Wagescaleid,WAGESCALENAME,SENIORITY,WEFDATE, Isnull(Currency,'USD') As Currency from WageScaleRankHistory WRH INNER JOIN WageScale W ON WRH.WageScaleId=W.WageScaleId WHERE WageScaleRankId=" + WageScaleRankId + " ORDER BY WEFDATE DESC");
        if (dt.Rows.Count > 0)
        {
            if (Convert.IsDBNull(dt.Rows[0]["WEFDate"]))
            {
                lblWageScaleName.Text = dt.Rows[0]["WAGESCALENAME"].ToString();
                lblSeniority.Text = dt.Rows[0]["SENIORITY"].ToString();
                lblEffectiveFrom.Text = "";
                lblWageScaleCurrency.Text = dt.Rows[0]["Currency"].ToString();
                WageScaleId = Convert.ToInt32(dt.Rows[0]["Wagescaleid"].ToString());
            }
            else
            {
                lblWageScaleName.Text = dt.Rows[0]["WAGESCALENAME"].ToString();
                lblSeniority.Text = dt.Rows[0]["SENIORITY"].ToString();
                lblEffectiveFrom.Text = Common.ToDateString(dt.Rows[0]["WEFDATE"]);
                lblWageScaleCurrency.Text = dt.Rows[0]["Currency"].ToString();
                WageScaleId = Convert.ToInt32(dt.Rows[0]["Wagescaleid"].ToString());
            }
        }
        else
        {
            lblWageScaleName.Text = "";
            lblSeniority.Text = "";
            lblEffectiveFrom.Text = "";
            lblWageScaleCurrency.Text = "";
            WageScaleId = 0;
        }
       
    }

    public string FormatCurr(object _in)
    {
        return string.Format("{0:0.00}", _in);
    }

    protected void BindWageScaleRankDetailsHistory()
    {
        DataTable dt = WagesMaster.WageComponentsDetailsHistory(WageScaleId, WageScaleRankId);
        if (dt.Rows.Count > 0)
        {
            gvWageScaleRankHistory.DataSource = dt;
            gvWageScaleRankHistory.DataBind();
        }
        else
        {
            gvWageScaleRankHistory.DataSource = null;
            gvWageScaleRankHistory.DataBind();
        }
    }
    //private void Bind_RankHistory()
    //{
    //    string sql;
    //    sql = "Select RankId, RankName, RankCode, ISNULL((Select sum( wsr.WageScaleComponentValue ) from WageScaleRankHistory ws with(nolock) inner join WageScaleRankDetailsHistory wsr with(nolock) on ws.WageScaleRankId = wsr.WageScaleRankId and wsr.RankId = Rank.RankId inner join WageScaleDetails wsd with(nolock) on ws.WageScaleId = wsd.WageScaleId and wsr.WageScaleComponentId = wsd.WageScaleComponentId where wsr.Active = 'Y' and wsd.Status = 'A' and ws.WageScaleRankId = '" + WageScaleRankId + "' and ws.NationalityId = 0 and ws.Seniority = '" + lblSeniority.Text + "' group by wsr.WageScaleRankId, wsr.RankId),0.0)  As Total from Rank with(nolock) where StatusId = 'A' and isnull(OffCrew,'') <> '' order by  OffCrew,RankLevel asc";
    //    DataTable dtRank = Budget.getTable(sql).Tables[0];
    //    if (dtRank.Rows.Count > 0)
    //    {
    //        rptRank.DataSource = dtRank;
    //        rptRank.DataBind();
    //    }
    //    else
    //    {
    //        rptRank.DataSource = null;
    //        rptRank.DataBind();
    //    }
    //}
    //protected void rptRank_ItemCommand(object Sender, RepeaterCommandEventArgs e)
    //{
    //    hdnRankCode.Value = "";
    //    div1.Visible = true;
    //    txtExtraOtRate.Text = "0.00";
    //    //HiddenField hdnItem = (HiddenField)e.Item.FindControl("hdnRankId");
    //    string rankCode = "";
    //    LinkButton lkRank = (LinkButton)e.Item.FindControl("lnkRank");
    //    Label lblRankCode = (Label)e.Item.FindControl("lblRankCode");
    //    rankCode = lblRankCode.Text;
    //    hdnRankCode.Value = rankCode;
    //    lblRankheader.Text = lkRank.Text;
    //    int rankId;


    //    foreach (RepeaterItem item in rptRank.Items)
    //    {
    //        HtmlTableRow row = (HtmlTableRow)item.FindControl("row");
    //        row.Attributes["style"] = "";
    //    }

    //    HtmlTableRow newRow = e.Item.FindControl("row") as HtmlTableRow;
    //    if (newRow != null)
    //    {
    //        newRow.Attributes["style"] = "background-color:#CCCCCC";
    //    }

    //    if (int.TryParse(e.CommandArgument.ToString(), out rankId))
    //    {
    //        if (rankId > 0)
    //        {
    //            lblTotalEarnCurrency.Text = lblWageScaleCurrency.Text;
    //            lblExtraOtRateCurrency.Text = lblWageScaleCurrency.Text;
    //            BindEarningWages(rankCode, "E");
    //            BindDeductionWages(rankCode, "D");
    //            GetExtraOTRate(WageScaleId, rankCode);
    //            getTotalEarnings();
    //        }
    //    }

    //}
    //protected void rptRank_ItemDataBound(object sender, RepeaterItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    //    {
    //        Label lblWageTotal = e.Item.FindControl("lblWageTotal") as Label;
    //        Image imgbtn = e.Item.FindControl("Imgbtn") as Image;

    //        if (!(string.IsNullOrWhiteSpace(lblWageTotal.Text)) && Common.CastAsDecimal(lblWageTotal.Text) > 0)
    //        {
    //            imgbtn.Visible = true;
    //        }
    //        else
    //        {
    //            imgbtn.Visible = false;
    //        }
    //    }
    //}
    //protected void getTotalEarnings()
    //{
    //    divEarnings.Visible = true;
    //    txtTotalEarnings.Text = "0.00";
    //    Decimal totalEarning = 0;
    //    Decimal totalDeductions = 0;
    //    Decimal total = 0;
    //    foreach (RepeaterItem item in rptEaringWages.Items)
    //    {
    //        TextBox txtComponentValue = item.FindControl("txtComponentAmount") as TextBox;
    //        if (!string.IsNullOrWhiteSpace(txtComponentValue.Text))
    //        {
    //            if (totalEarning == 0)
    //            {
    //                totalEarning = Decimal.Parse(txtComponentValue.Text);
    //            }
    //            else
    //            {
    //                totalEarning = totalEarning + Decimal.Parse(txtComponentValue.Text);
    //            }
    //        }
    //    }

    //    foreach (RepeaterItem item in rptDeductionWages.Items)
    //    {
    //        TextBox txtdeductionComponentValue = item.FindControl("txtdeductComponentAmount") as TextBox;
    //        if (!string.IsNullOrWhiteSpace(txtdeductionComponentValue.Text))
    //        {
    //            if (totalDeductions == 0)
    //            {
    //                totalDeductions = Decimal.Parse(txtdeductionComponentValue.Text);
    //            }
    //            else
    //            {
    //                totalDeductions = totalDeductions + Decimal.Parse(txtdeductionComponentValue.Text);
    //            }
    //        }
    //    }

    //    total = totalEarning - totalDeductions;
    //    txtTotalEarnings.Text = FormatCurr(total);
    //}

    //protected void BindEarningWages(string rankCode, string componentType)
    //{
    //    DataTable dt = WagesMaster.WageComponentsDetails(WageScaleId, 0, Common.CastAsInt32(lblSeniority.Text), rankCode);
    //    DataView dv = new DataView(dt);
    //    dv.RowFilter = " ComponentType = '" + componentType + "'";

    //    DataTable dtEarningComponent = dv.ToTable();

    //    if (dtEarningComponent.Rows.Count > 0)
    //    {
    //        rptEaringWages.Visible = true;
    //        rptEaringWages.DataSource = dtEarningComponent;
    //        rptEaringWages.DataBind();

    //        Control HeaderTemplate = rptEaringWages.Controls[0].Controls[0];
    //        Label lblEarnHeader = HeaderTemplate.FindControl("lblEarnCurrency") as Label;
    //        lblEarnHeader.Text = lblWageScaleCurrency.Text;
    //    }
    //    else
    //    {
    //        lblEarningWages_Message.Text = "No record found !";
    //        rptEaringWages.DataSource = null;
    //        rptEaringWages.DataBind();
    //    }
    //}

    //protected void BindDeductionWages(string rankCode, string componentType)
    //{
    //    DataTable dt = WagesMaster.WageComponentsDetails(WageScaleId, 0, Common.CastAsInt32(lblSeniority.Text), rankCode);
    //    DataView dv = new DataView(dt);
    //    dv.RowFilter = " ComponentType = '" + componentType + "'";

    //    DataTable dtDeductionComponent = dv.ToTable();

    //    if (dtDeductionComponent.Rows.Count > 0)
    //    {
    //        rptDeductionWages.Visible = true;
    //        rptDeductionWages.DataSource = dtDeductionComponent;
    //        rptDeductionWages.DataBind();
    //        Control HeaderTemplate = rptDeductionWages.Controls[0].Controls[0];
    //        Label lblDeductHeader = HeaderTemplate.FindControl("lblDeductCurrency") as Label;
    //        lblDeductHeader.Text = lblWageScaleCurrency.Text;
    //    }
    //    else
    //    {
    //        lblDeductionWage_Message.Text = "No record found !";
    //        rptDeductionWages.DataSource = null;
    //        rptDeductionWages.DataBind();
    //    }
    //}

    //protected void GetExtraOTRate(int wageScaleId, string rankCode)
    //{
    //    string sql = "Select OTRate from WagescaleOTRatesHistory with(nolock) where WageScaleRankId = (Select WageScaleRankId from WagescalerankHistory with(nolock) where WageScaleId = " + wageScaleId + ") And RankId = (Select RankId from Rank with(nolock) where RankCode = '" + rankCode + "')";
    //    DataTable dtOTRate = Budget.getTable(sql).Tables[0];
    //    if (dtOTRate.Rows.Count > 0)
    //    {
    //        txtExtraOtRate.Text = Math.Round(double.Parse(dtOTRate.Rows[0][0].ToString()), 2).ToString();
    //    }
    //}



    protected void gvWageScaleRankHistory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager)
        {
               // only check for pager row, all other rows including header/footer should be hidden
            e.Row.Cells[0].Visible = false;
           // e.Row.CssClass += "columnscss";
        }
    }
}