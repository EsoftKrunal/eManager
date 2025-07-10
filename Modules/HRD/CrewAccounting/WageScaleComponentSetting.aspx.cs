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


public partial class Modules_HRD_CrewAccounting_WageScaleComponentSetting : System.Web.UI.Page
{
    public int UserId
    {
        get { return Common.CastAsInt32(ViewState["UserId"]); }
        set { ViewState["UserId"] = value; }
    }
    private void Load_WageScale()
    {
        DataSet ds = cls_SearchReliever.getMasterData("WageScale", "WageScaleID", "WageScaleName");
        dp_WSname.DataSource = ds.Tables[0];
        dp_WSname.DataTextField = "WageScaleName";
        dp_WSname.DataValueField = "WageScaleID";
        dp_WSname.DataBind();
        dp_WSname.Items.Insert(0, new ListItem("< Select >", "0"));
    }

    private void Bind_Rank()
    {
        string sql;
        sql = "Select RankId, RankName, RankCode, ISNULL((Select sum( wsr.WageScaleComponentValue ) from WageScaleRank ws with(nolock) inner join WageScaleRankDetails wsr with(nolock) on ws.WageScaleRankId = wsr.WageScaleRankId and wsr.RankId = Rank.RankId inner join WageScaleDetails wsd with(nolock) on ws.WageScaleId = wsd.WageScaleId and wsr.WageScaleComponentId = wsd.WageScaleComponentId where wsr.Active = 'Y' and wsd.Status = 'A' and ws.WageScaleId = '" + dp_WSname.SelectedValue + "' and ws.NationalityId = 0 and ws.Seniority = '" + txt_Seniority.Text + "' group by wsr.WageScaleRankId, wsr.RankId),0.0)  As Total from Rank with(nolock) where StatusId = 'A' and isnull(OffCrew,'') <> '' order by  OffCrew,RankLevel asc";
        DataTable dtRank = Budget.getTable(sql).Tables[0];
        if (dtRank.Rows.Count > 0)
        {
            rptRank.DataSource = dtRank;
            rptRank.DataBind();
        }
        else
        {
            rptRank.DataSource = null;
            rptRank.DataBind();
        }  
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            SessionManager.SessionCheck_New();
            //-----------------------------
            lblMessage.Text = "";
            lblEarningWages_Message.Text = "";
            lblDeductionWage_Message.Text = "";
            if (!IsPostBack)
            {
                UserId = Common.CastAsInt32(Session["loginid"]);
                Load_WageScale();
            }
            //this.txt_Seniority.Attributes.Add("onKeyPress","javascript:if(event.keyCode==13){document.getElementById('"+ btn_Show.UniqueID +"').focus();}");
            //-----------------------------
        }
        catch (Exception ex)
        { 
         
        }
    }
    protected void Save_Click(object sender, EventArgs e)
    {

        string datastr, headerstr, deductdatsstr, deductheaderstr;
        string RankCode;
        int WageScaleId, Seniority, NationalityId = 0;
        WageScaleId = Convert.ToInt32(dp_WSname.SelectedValue);
        Seniority = Convert.ToInt32(txt_Seniority.Text);
        RankCode = "";
        datastr = "";
        headerstr = "";
        deductdatsstr = "";
        deductheaderstr = "";
        if (this.Txt_WEF.Text == "")
        {
            ShowMessage("WEF Date Can Not Be Blank ", true);
            return;
        }
        if (hwef.Value != "" && Convert.ToDateTime(hwef.Value) > Convert.ToDateTime(this.Txt_WEF.Text))
        {
            ShowMessage("WEF Date Can Not Be Less Than " + this.hwef.Value.ToString(), true);
            return;
        }
        if (Convert.ToDateTime(this.Txt_WEF.Text).Day != 1)
        {
            ShowMessage("WEF date should be start date of month.", true);
            return;
        }

        DataTable dt;
        try
        {
            RankCode = hdnRankCode.Value;
            //if (this.hfwsid.Value.ToString().Trim() != "")
            //{
               
            //   WagesMaster.Insert_WagerankHistory(Convert.ToInt16(this.hfwsid.Value.ToString()), Convert.ToDateTime(this.Txt_WEF.Text), RankCode); 
            //}
            dt = WagesMaster.WageComponents(WageScaleId, NationalityId, Seniority);
            if (dt.Rows.Count > 0)
            {
                
                WagesMaster.InsertWageScalesRankDetails(WageScaleId, NationalityId, Seniority, Txt_WEF.Text.Trim(), 0, UserId);

                foreach (RepeaterItem item in rptEaringWages.Items)
                {
                    Label lblComponentName = item.FindControl("lblComponentName") as Label;
                    TextBox txtComponentValue = item.FindControl("txtComponentAmount") as TextBox;
                    HiddenField hdnComponentId = item.FindControl("hdnComponentId") as HiddenField;
                    HiddenField hdnComponentType = item.FindControl("hdnComponentType") as HiddenField;
                    if (! string.IsNullOrWhiteSpace(lblComponentName.Text))
                    {
                        headerstr = headerstr + "," + lblComponentName.Text;
                        datastr = datastr + "," + txtComponentValue.Text;
                    }
                }
                if (!string.IsNullOrWhiteSpace(headerstr))
                {
                    headerstr = headerstr.Substring(1);
                    datastr = datastr.Substring(1);
                    WagesMaster.UpdateWageComponentsDetails(WageScaleId, NationalityId, Seniority, RankCode, headerstr, datastr, Common.CastAsDecimal(txtExtraOtRate.Text));
                }


                foreach (RepeaterItem item in rptDeductionWages.Items)
                {
                    Label lbldeductComponentName = item.FindControl("lbldeductComponentName") as Label;
                    TextBox txtdeductComponentValue = item.FindControl("txtdeductComponentAmount") as TextBox;
                    HiddenField hdnComponentId = item.FindControl("hdndeductComponentId") as HiddenField;
                    HiddenField hdnComponentType = item.FindControl("hdndeductComponentType") as HiddenField;
                    if (!string.IsNullOrWhiteSpace(lbldeductComponentName.Text))
                    {
                        deductheaderstr = deductheaderstr + "," + lbldeductComponentName.Text;
                        deductdatsstr = deductdatsstr + "," + txtdeductComponentValue.Text;

                    }
                }
                if (! string.IsNullOrWhiteSpace(deductheaderstr))
                { 
                deductheaderstr = deductheaderstr.Substring(1);
                deductdatsstr = deductdatsstr.Substring(1);

                WagesMaster.UpdateWageComponentsDetails(WageScaleId, NationalityId, Seniority, RankCode, deductheaderstr, deductdatsstr, Common.CastAsDecimal(txtExtraOtRate.Text));
                }

                getTotalEarnings();
                Bind_Rank();
                ShowMessage("Record Saved Successfully.", false);

               
            }
            
        }
        catch (Exception ex) { ShowMessage("Record Can't Saved. Error :" + ex.Message, true); }
    }
    protected void btn_Show_Click(object sender, EventArgs e)
    {
        int WageScaleId = Convert.ToInt32(dp_WSname.SelectedValue);
        //BindRepeater();
        Bind_Rank();
        btnModify.Visible = true;
       // btn_Show_History.Visible = true;
        tbl_Save.Visible = false;
        BindHistory();
    }
    //protected void btn_ShowHistory_Click(object sender, EventArgs e)
    //{
    //    DataTable dt = WagesMaster.Get_Wage_Master_History(Common.CastAsInt32(dp_WSname.SelectedValue), 0, Common.CastAsInt32(txt_Seniority.Text));
    //    if (dt.Rows.Count > 0)
    //    {
    //        rpt_History.DataSource = dt;
    //        rpt_History.DataBind();
    //        btnOpenHistory.Visible = true;
    //    }
    //    else
    //    {
    //        rpt_History.DataSource = null;
    //        rpt_History.DataBind();
    //        btnOpenHistory.Visible = false;
    //    }
      
    //   // dv_History.Visible = true;
    //}
    protected void BindHistory()
    {
        DataTable dt = WagesMaster.Get_Wage_Master_History(Common.CastAsInt32(dp_WSname.SelectedValue), 0, Common.CastAsInt32(txt_Seniority.Text));
        if (dt.Rows.Count > 0)
        {
            rpt_History.DataSource = dt;
            rpt_History.DataBind();
            //btnOpenHistory.Visible = true;
        }
        else
        {
            rpt_History.DataSource = null;
            rpt_History.DataBind();
            //btnOpenHistory.Visible = false;
        }
    }
    protected void btn_CloseHistory_Click(object sender, EventArgs e)
    {
        //dv_History.Visible = false;
    }
    //protected void btn_Open_Click(object sender, EventArgs e)
    //{
    //    int HistoryId = Common.CastAsInt32(Request.Form["his"]);
    //   // BindRepeater(HistoryId);
    //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "afd", "window.open('ViewWageScaleHistory.aspx?WageScaleRankId=" + HistoryId + "','');", true);
    //}
    protected void lbEffectiveDts_Click (object sender, EventArgs e)
    {
        try
        {
            int HistoryId = Common.CastAsInt32(((LinkButton)sender).CommandArgument);
            if (HistoryId > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "afd", "window.open('WageScaleComponentsSettingsHistory.aspx?WageScaleRankId=" + HistoryId + "','');", true);
            }
        }
        catch(Exception ex)
        {
            lblMessage.Text = ex.Message.ToString();
        }

    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
        try
        {
            bool isEarningflag = false;
            bool isdeductflag = false;

            if (hdnRankCode.Value == "")
            {
                lblMessage.Text = "Please select rank name."; return;
            }

            foreach (RepeaterItem ri in rptEaringWages.Items)
            {
                foreach (Control c in ri.Controls)
                {
                    isEarningflag = true;
                    if (c.GetType() == typeof(TextBox))
                    {
                        ((TextBox)c).Enabled = true;
                        ((TextBox)c).CssClass = "ctltext";
                    }
                }
            }

            foreach (RepeaterItem ri in rptDeductionWages.Items)
            {
                foreach (Control c in ri.Controls)
                {
                    isdeductflag = true;
                    if (c.GetType() == typeof(TextBox))
                    {
                        ((TextBox)c).Enabled = true;
                        ((TextBox)c).CssClass = "ctltext";
                    }
                }
            }
            if (!isEarningflag)
            {
                lblEarningWages_Message.Text = "No Record found !";
            }
            if (!isdeductflag)
            {
                lblDeductionWage_Message.Text = "No Record found !";
            }
            tbl_Save.Visible = true;
        }
        catch (Exception ex)
        {

        }
    }

    protected void BindEarningWages(string rankCode, string componentType)
    {
        DataTable dt = WagesMaster.WageComponentsDetails(Common.CastAsInt32(dp_WSname.SelectedValue), 0, Common.CastAsInt32(txt_Seniority.Text), rankCode);
        DataView dv = new DataView(dt);
        dv.RowFilter = " ComponentType = '" + componentType + "'";

        DataTable dtEarningComponent = dv.ToTable();

        if (dtEarningComponent.Rows.Count > 0)
        {
            rptEaringWages.Visible = true;
            rptEaringWages.DataSource = dtEarningComponent;
            rptEaringWages.DataBind();

            Control HeaderTemplate = rptEaringWages.Controls[0].Controls[0];
            Label lblEarnHeader = HeaderTemplate.FindControl("lblEarnCurrency") as Label;
            lblEarnHeader.Text = lblWageScaleCurrency.Text;
        }
        else
        {
            lblEarningWages_Message.Text = "No record found !";
            rptEaringWages.DataSource = null;
            rptEaringWages.DataBind();
        }
    }

    protected void BindDeductionWages(string rankCode, string componentType)
    {
        DataTable dt = WagesMaster.WageComponentsDetails(Common.CastAsInt32(dp_WSname.SelectedValue), 0, Common.CastAsInt32(txt_Seniority.Text), rankCode);
        DataView dv = new DataView(dt);
        dv.RowFilter = " ComponentType = '" + componentType + "'";

        DataTable dtDeductionComponent = dv.ToTable();

        if (dtDeductionComponent.Rows.Count > 0)
        {
            rptDeductionWages.Visible = true;
            rptDeductionWages.DataSource = dtDeductionComponent;
            rptDeductionWages.DataBind();
            Control HeaderTemplate = rptDeductionWages.Controls[0].Controls[0];
            Label lblDeductHeader = HeaderTemplate.FindControl("lblDeductCurrency") as Label;
            lblDeductHeader.Text = lblWageScaleCurrency.Text;
        }
        else
        {
            lblDeductionWage_Message.Text = "No record found !";
            rptDeductionWages.DataSource = null;
            rptDeductionWages.DataBind();
        }
    }

    protected void getTotalEarnings()
    {
        divEarnings.Visible = true;
        txtTotalEarnings.Text = "0.00";
        Decimal totalEarning = 0;
        Decimal totalDeductions = 0;
        Decimal total = 0;
        foreach (RepeaterItem item in rptEaringWages.Items)
        {
            TextBox txtComponentValue = item.FindControl("txtComponentAmount") as TextBox;
            if (!string.IsNullOrWhiteSpace(txtComponentValue.Text) )
            {
               if (totalEarning == 0)
                {
                    totalEarning = Decimal.Parse(txtComponentValue.Text);
                }
               else
                {
                    totalEarning = totalEarning + Decimal.Parse(txtComponentValue.Text);
                }
            }
        }

        foreach (RepeaterItem item in rptDeductionWages.Items)
        {
            TextBox txtdeductionComponentValue = item.FindControl("txtdeductComponentAmount") as TextBox;
            if (!string.IsNullOrWhiteSpace(txtdeductionComponentValue.Text))
            {
                if (totalDeductions == 0)
                {
                    totalDeductions = Decimal.Parse(txtdeductionComponentValue.Text);
                }
                else
                {
                    totalDeductions = totalDeductions + Decimal.Parse(txtdeductionComponentValue.Text);
                }
            }
        }

        total = totalEarning - totalDeductions;
        txtTotalEarnings.Text = FormatCurr(total);
    }

    //protected void BindRepeater()
    //{
    //    DataTable dt = WagesMaster.get_HeaderDetails(Common.CastAsInt32(dp_WSname.SelectedValue), 0, Common.CastAsInt32(txt_Seniority.Text));
    //    if (dt.Rows.Count > 0)
    //    {

    //        if (Convert.IsDBNull(dt.Rows[0][0]))
    //        {
    //            hfwsid.Value = "";
    //            Txt_WEF.Text = "";
    //            lblEffectiveFrom.Text = "";
    //            this.hwef.Value = "";
    //        }
    //        else
    //        {
    //            hfwsid.Value = dt.Rows[0][2].ToString();
    //            Txt_WEF.Text = Common.ToDateString(dt.Rows[0][0]);
    //            lblEffectiveFrom.Text = Common.ToDateString(dt.Rows[0][0]);
    //            hwef.Value = Common.ToDateString(dt.Rows[0][0]);
    //        }
    //    }
    //    else
    //    {
    //        hfwsid.Value = "";
    //        Txt_WEF.Text = "";
    //        lblEffectiveFrom.Text = "";
    //        hwef.Value = "";
    //    }
    //    for (int i = 1; i <= 12; i++)
    //    {
    //        dt = wagecomponent.selectWageScaleDetailsById(i, Convert.ToInt32(dp_WSname.SelectedValue), 0, Convert.ToInt32(txt_Seniority.Text));

    //        if (dt.Rows.Count > 0)
    //        {
    //            ((CheckBox)this.FindControl("CheckBox" + i.ToString())).Checked = (dt.Rows[0][0].ToString() == "Y");
    //            ((Label)this.FindControl("Label" + i.ToString())).Text = dt.Rows[0][1].ToString();
    //        }


    //    }

    //    dt = WagesMaster.WageComponents(Common.CastAsInt32(dp_WSname.SelectedValue), 0, Common.CastAsInt32(txt_Seniority.Text));
    //    rptWages.DataSource = dt;
    //    rptWages.DataBind();
    //}
    public string FormatCurr(object _in)
    {
        return string.Format("{0:0.00}", _in);
    }
    public void ShowMessage(string Message, bool Error)
    {
        lblMessage.Text = Message;
        lblMessage.ForeColor = (Error) ? System.Drawing.Color.Red : System.Drawing.Color.Green;
    }

    protected void rptRank_ItemCommand(object Sender, RepeaterCommandEventArgs e)
    {
        hdnRankCode.Value = "";
        div1.Visible = true;
        txtExtraOtRate.Text = "0.00";
        //HiddenField hdnItem = (HiddenField)e.Item.FindControl("hdnRankId");
        string rankCode = "";
        LinkButton lkRank = (LinkButton)e.Item.FindControl("lnkRank");
        Label lblRankCode = (Label)e.Item.FindControl("lblRankCode");
        rankCode = lblRankCode.Text;
        hdnRankCode.Value = rankCode;
        lblRankheader.Text = lkRank.Text;
        int rankId;

        
        foreach (RepeaterItem item in rptRank.Items)
        {
            HtmlTableRow row = (HtmlTableRow)item.FindControl("row");
            row.Attributes["style"] = "";
        }

        HtmlTableRow newRow = e.Item.FindControl("row") as HtmlTableRow;
        if (newRow != null)
        {
            newRow.Attributes["style"] = "background-color:#CCCCCC";
        }
           
        if (int.TryParse(e.CommandArgument.ToString(), out rankId))
        {
           if (rankId > 0)
            {
                lblTotalEarnCurrency.Text = lblWageScaleCurrency.Text;
                lblExtraOtRateCurrency.Text = lblWageScaleCurrency.Text;
                BindEarningWages(rankCode,"E");
                BindDeductionWages(rankCode, "D");
                GetExtraOTRate(Common.CastAsInt32(dp_WSname.SelectedValue), rankCode);
                DataTable dt = WagesMaster.get_HeaderDetails(Common.CastAsInt32(dp_WSname.SelectedValue), 0, Common.CastAsInt32(txt_Seniority.Text));
                if (dt.Rows.Count > 0)
                {

                    if (Convert.IsDBNull(dt.Rows[0][0]))
                    {
                        hfwsid.Value = "";
                        Txt_WEF.Text = "";
                        lblEffectiveFrom.Text = "";
                        this.hwef.Value = "";
                    }
                    else
                    {
                        hfwsid.Value = dt.Rows[0][2].ToString();
                        Txt_WEF.Text = Common.ToDateString(dt.Rows[0][0]);
                        lblEffectiveFrom.Text = Common.ToDateString(dt.Rows[0][0]);
                        hwef.Value = Common.ToDateString(dt.Rows[0][0]);
                    }
                }
                else
                {
                    hfwsid.Value = "";
                    Txt_WEF.Text = "";
                    lblEffectiveFrom.Text = "";
                    hwef.Value = "";
                }

                getTotalEarnings();
            }
        }
        
    }

    protected void ddl_RankGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_RankGroup.SelectedIndex > 0)
        {
            string sql;

            sql = "Select RankId, RankName, RankCode, ISNULL((Select sum( wsr.WageScaleComponentValue ) from WageScaleRank ws with(nolock) inner join WageScaleRankDetails wsr with(nolock) on ws.WageScaleRankId = wsr.WageScaleRankId and wsr.RankId = Rank.RankId inner join WageScaleDetails wsd with(nolock) on ws.WageScaleId = wsd.WageScaleId and wsr.WageScaleComponentId = wsd.WageScaleComponentId where wsr.Active = 'Y' and wsd.Status = 'A' and ws.WageScaleId = '" + dp_WSname.SelectedValue + "' and ws.NationalityId = 0 and ws.Seniority = '" + txt_Seniority.Text + "' group by wsr.WageScaleRankId, wsr.RankId),0.0)  As Total from Rank with(nolock) where StatusId = 'A' and isnull(OffCrew,'') <> '' And OffCrew = '" + ddl_RankGroup.SelectedValue + "' order by  OffCrew,RankLevel asc";
            
            DataTable dt5 = Budget.getTable(sql).Tables[0];
            rptRank.DataSource = dt5;
            rptRank.DataBind();
        }
        else
        {
            rptRank.DataSource = null;
            rptRank.DataBind();
            Bind_Rank();
        }

        rptEaringWages.DataSource = null;
        rptEaringWages.DataBind();

        rptDeductionWages.DataSource = null;
        rptDeductionWages.DataBind();
    }

    protected void TxtComponentAmount_TextChanged(object sender, EventArgs e)
    {
        try
        {
            getTotalEarnings();
        }
        catch(Exception ex)
        { ShowMessage(ex.Message,true); }

    }

    protected void TxtdeductComponentAmount_TextChanged(object sender, EventArgs e)
    {
        try
        {
            getTotalEarnings();
        }
        catch (Exception ex)
        { ShowMessage(ex.Message, true); }
    }

    protected void rptRank_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lblWageTotal = e.Item.FindControl("lblWageTotal") as Label;
            Image imgbtn = e.Item.FindControl("Imgbtn") as Image;

            if (! (string.IsNullOrWhiteSpace(lblWageTotal.Text)) && Common.CastAsDecimal(lblWageTotal.Text) > 0)
            {
                imgbtn.Visible = true;
            }
            else
            {
                imgbtn.Visible = false;
            }
        }
    }

    protected void dp_WSname_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(dp_WSname.SelectedValue) > 0)
        {
            rptRank.DataSource = null;
            rptRank.DataBind();
            rptEaringWages.DataSource = null;
            rptEaringWages.DataBind();

            rptDeductionWages.DataSource = null;
            rptDeductionWages.DataBind();
            string sql;

            sql = "Select Isnull(Currency,'USD') As Currency from WageScale with(nolock) where WageScaleId = '" + dp_WSname.SelectedValue + "'";

            DataTable dt5 = Budget.getTable(sql).Tables[0];
            if (dt5.Rows.Count > 0)
            {
                lblWageScaleCurrency.Text = dt5.Rows[0]["Currency"].ToString();
            }
            hfwsid.Value = "";
            Txt_WEF.Text = "";
            lblEffectiveFrom.Text = "";
            this.hwef.Value = "";
            lblRankheader.Text = "";
            txt_Seniority.Text = "";
            getEffectiveDate();
            BindHistory();
            if (Common.CastAsInt32(dp_WSname.SelectedValue) > 0 && !string.IsNullOrWhiteSpace(txt_Seniority.Text) )
            {
                btnCBARevisition.Enabled = true;
            }
            else
            {
                btnCBARevisition.Enabled = false;
            }
        }
       
    }
    protected void getEffectiveDate()
    {
        if (Common.CastAsInt32(dp_WSname.SelectedValue) > 0 &&  ! string.IsNullOrWhiteSpace(txt_Seniority.Text) )
        {
            DataTable dt = WagesMaster.get_HeaderDetails(Common.CastAsInt32(dp_WSname.SelectedValue), 0, Common.CastAsInt32(txt_Seniority.Text));
            if (dt.Rows.Count > 0)
            {
                if (Convert.IsDBNull(dt.Rows[0][0]))
                {
                    hfwsid.Value = "";
                    Txt_WEF.Text = "";
                    lblEffectiveFrom.Text = "";
                    this.hwef.Value = "";
                }
                else
                {
                    hfwsid.Value = dt.Rows[0][2].ToString();
                    Txt_WEF.Text = Common.ToDateString(dt.Rows[0][0]);
                    lblEffectiveFrom.Text = Common.ToDateString(dt.Rows[0][0]);
                    hwef.Value = Common.ToDateString(dt.Rows[0][0]);
                }
            }
            else
            {
                hfwsid.Value = "";
                Txt_WEF.Text = "";
                lblEffectiveFrom.Text = "";
                hwef.Value = "";
            }
        }  
    }

    protected void txt_Seniority_TextChanged(object sender, EventArgs e)
    {
        getEffectiveDate();
        BindHistory();
        if (Common.CastAsInt32(dp_WSname.SelectedValue) > 0 && ! string.IsNullOrWhiteSpace(txt_Seniority.Text)  )
        {
            btnCBARevisition.Enabled = true;
        }
        else
        {
            btnCBARevisition.Enabled = false;
        }
    }

    protected void GetExtraOTRate(int wageScaleId, string rankCode)
    {
        string sql = "Select OTRate from WagescaleOTRates with(nolock) where WageScaleRankId = (Select WageScaleRankId from Wagescalerank with(nolock) where WageScaleId = " + wageScaleId + ") And RankId = (Select RankId from Rank with(nolock) where RankCode = '" + rankCode + "')";
        DataTable dtOTRate = Budget.getTable(sql).Tables[0];
        if (dtOTRate.Rows.Count > 0)
        {
            txtExtraOtRate.Text = Math.Round(double.Parse(dtOTRate.Rows[0][0].ToString()),2).ToString();
        }
    }

    protected void BindRepeater(int WageScaleRankId)
    {
        //----------------------------
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select WRH.Wagescaleid,WAGESCALENAME,SENIORITY,WEFDATE from WageScaleRankHistory WRH INNER JOIN WageScale W ON WRH.WageScaleId=W.WageScaleId WHERE WageScaleRankId=" + WageScaleRankId + " ORDER BY WEFDATE DESC");
        if (dt.Rows.Count > 0)
        {
            if (Convert.IsDBNull(dt.Rows[0]["WEFDate"]))
            {
                dp_WSname.SelectedValue = dt.Rows[0]["Wagescaleid"].ToString();
                txt_Seniority.Text = dt.Rows[0]["SENIORITY"].ToString();
                lblEffectiveFrom.Text = "";
            }
            else
            {
                dp_WSname.SelectedValue = dt.Rows[0]["Wagescaleid"].ToString();
                txt_Seniority.Text = dt.Rows[0]["SENIORITY"].ToString();
                lblEffectiveFrom.Text = Common.ToDateString(dt.Rows[0]["WEFDATE"]);
            }
        }
        else
        {
            dp_WSname.SelectedIndex = 0;
            txt_Seniority.Text = "";
            lblEffectiveFrom.Text = "";
        }
        Bind_RankHistory();
    }

    private void Bind_RankHistory()
    {
        string sql;
        sql = "Select RankId, RankName, RankCode, ISNULL((Select sum( wsr.WageScaleComponentValue ) from WageScaleRankHistory ws with(nolock) inner join WageScaleRankDetailsHistory wsr with(nolock) on ws.WageScaleRankId = wsr.WageScaleRankId and wsr.RankId = Rank.RankId inner join WageScaleDetails wsd with(nolock) on ws.WageScaleId = wsd.WageScaleId and wsr.WageScaleComponentId = wsd.WageScaleComponentId where wsr.Active = 'Y' and wsd.Status = 'A' and ws.WageScaleId = '" + dp_WSname.SelectedValue + "' and ws.NationalityId = 0 and ws.Seniority = '" + txt_Seniority.Text + "' group by wsr.WageScaleRankId, wsr.RankId),0.0)  As Total from Rank with(nolock) where StatusId = 'A' and isnull(OffCrew,'') <> '' order by  OffCrew,RankLevel asc";
        DataTable dtRank = Budget.getTable(sql).Tables[0];
        if (dtRank.Rows.Count > 0)
        {
            rptRank.DataSource = dtRank;
            rptRank.DataBind();
        }
        else
        {
            rptRank.DataSource = null;
            rptRank.DataBind();
        }
    }


    protected void btnCBARevisition_Click(object sender, EventArgs e)
    {
        dv_CBARevisition.Visible = true;
    }

    protected void btnCBA_Close_Click(object sender, EventArgs e)
    {
        dv_CBARevisition.Visible = false;
    }

   

    protected void btnCBA_save_Click(object sender, EventArgs e)
    {
        int WageScaleId, Seniority, NationalityId = 0;
        WageScaleId = Convert.ToInt32(dp_WSname.SelectedValue);
        Seniority = Convert.ToInt32(txt_Seniority.Text);
        if (this.txtCBAEffDt.Text == "")
        {
            ShowMessage("WEF Date Can Not Be Blank ", true);
            return;
        }
        if (hwef.Value != "" && Convert.ToDateTime(hwef.Value) > Convert.ToDateTime(this.txtCBAEffDt.Text))
        {
            ShowMessage("WEF Date Can Not Be Less Than " + this.hwef.Value.ToString(), true);
            return;
        }
        if (Convert.ToDateTime(this.txtCBAEffDt.Text).Day != 1)
        {
            ShowMessage("WEF date should be start date of month.", true);
            return;
        }

        DataTable dt;
        try
        {
            
            if (this.hfwsid.Value.ToString().Trim() != "")
            {

                WagesMaster.Insert_WagerankHistory(Convert.ToInt16(this.hfwsid.Value.ToString()), Convert.ToDateTime(this.txtCBAEffDt.Text));
            }
            dt = WagesMaster.WageComponents(WageScaleId, NationalityId, Seniority);
            if (dt.Rows.Count > 0)
            {

                WagesMaster.InsertWageScalesRankDetails(WageScaleId, NationalityId, Seniority, txtCBAEffDt.Text.Trim(), 0, UserId);
                dv_CBARevisition.Visible = false;
               // Bind_Rank();
                lblEffectiveFrom.Text = txtCBAEffDt.Text.Trim();
                // btnModify.Visible = true;
                // btn_Show_History.Visible = true;
                btn_Show_Click(sender, e);
                tbl_Save.Visible = false;
                rptEaringWages.DataSource = null;
                rptEaringWages.DataBind();

                rptDeductionWages.DataSource = null;
                rptDeductionWages.DataBind();
                getTotalEarnings();
                BindHistory();
                ShowMessage("CBA Revision Saved Successfully.", false);

            }

        }
        catch (Exception ex) { ShowMessage("Record Can't Saved. Error :" + ex.Message, true); }
    }
}