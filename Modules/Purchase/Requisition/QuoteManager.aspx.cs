using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class QuoteManager : System.Web.UI.Page
{
    public AuthenticationManager authRFQList = new AuthenticationManager(0, 0, ObjectType.Page);
    public int BidId
    {
        set { ViewState["BidId"] = value; }
        get { return int.Parse("0" + ViewState["BidId"]); }
    }
    public int PoID
    {
        set { ViewState["PoID"] = value; }
        get { return int.Parse("0" + ViewState["PoID"]); }
    }
    public bool Enable
    {
        set { ViewState["_Enable"] = value; }
        get { return Convert.ToBoolean(ViewState["_Enable"]); }
    }
    //public string ShowAmount
    //{
    //    get
    //    {
    //        return (ViewState["ShowAmount"] == null) ? "N" : ViewState["ShowAmount"].ToString();
    //    }
    //    set { ViewState["ShowAmount"] = value; }
    //}
    public int UserId
    {
        set { ViewState["userid"] = value; }
        get { return int.Parse("0" + ViewState["userid"]); }
    }
    public string UserName
    {
        set { ViewState["_UserName"] = value; }
        get { return Convert.ToString(ViewState["_UserName"]); }
    }
   

    protected void Page_Load(object sender, EventArgs e)
    {
        #region --------- USER RIGHTS MANAGEMENT -----------
        AuthenticationManager authRFQViewEdit;
        try
        {
            authRFQViewEdit = new AuthenticationManager(1061, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
            if (!(authRFQViewEdit.IsView))
            {
                Response.Redirect("~/NoPermission.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/NoPermission.aspx?Message=" + ex.Message);
            return;
        }
        #endregion ----------------------------------------

        if (!(IsPostBack))
        {
            UserId = Common.CastAsInt32(Session["loginid"]);
            UserName = Convert.ToString(Session["FullUserName"]);
            //---------------------
            int _BidId = int.Parse("0" + Page.Request.QueryString["key"]);          

            //ShowAmount = "N";
            int loginid = Common.CastAsInt32(Session["loginid"]);
            //if (ProjectCommon.View_Quote_Permission(0, _BidId, loginid) || loginid==215 || loginid==299)
            //{
            //    ShowAmount = "Y";
            //}
            //else
            //{
            //    Response.Redirect("~/UnAuthorizedAccess.aspx?Message=Sorry ! you have not pemission to view the quote details.", true);
            //    return;
            //}
            imgPrint.Attributes.Add("onclick", "window.open('Print.aspx?BidID2=" + _BidId.ToString() + "'); return false;");
            string pass= Page.Request.QueryString["validate"];
            DataTable dtchk = Common.Execute_Procedures_Select_ByQuery("select bidid,bidpass,Expiration,bidstatusid from DBO.vw_TBLSMDPOMASTERBid where bidid=" + _BidId.ToString());// + " and Expiration>=getdate()");
            if(dtchk.Rows.Count>0)
            {
                BidId = Common.CastAsInt32(dtchk.Rows[0]["bidid"]);
                //UpdatePanel2.Visible = true;
                txtCurrDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                BindCurrency();
                showrecord();
            }
            GetDocCount(PoID);
        }
    }

    
    public void showrecord()
    {
        DisablePageControls();
        Boolean PRNotClosedForBiding = false;

        DataTable dtpr = Common.Execute_Procedures_Select_ByQuery("select SMDLevel1ApprovalDate from VW_tblSMDPOMaster where poid in (select poid from VW_tblSMDPOMasterBid where bidid=" + BidId.ToString() + ")");
        if(dtpr.Rows.Count>0)
        {
            PRNotClosedForBiding = Convert.IsDBNull(dtpr.Rows[0][0]);
        }
        DataTable dtRFQ = Common.Execute_Procedures_Select_ByQuery("EXEC DBO.sp_NewPR_getRFQMasterByBidId " + BidId.ToString());
        if (dtRFQ != null)
            if (dtRFQ.Rows.Count > 0)
            {
                DataRow dr = dtRFQ.Rows[0];
                bool Approved = true;
                int bidstatusid = Common.CastAsInt32(dr["bidstatusid"]);
                Approved =!Convert.IsDBNull(dr["bidsmdlevel1Approvaldate"]);
                if (bidstatusid<=2 && bidstatusid>0 && Approved==false && PRNotClosedForBiding)
                {
                    imgResubmitRFQ.Visible = true;
                }
                if (bidstatusid<2 && bidstatusid >=0 && Approved==false && PRNotClosedForBiding)
                {
  		            EnablePageControls();
		        }
		
                BindAccount(dr["dept"].ToString());
                PoID = Common.CastAsInt32(dr["PoID"].ToString());
                lblRFQNO.Text = dr["SHIPID"].ToString() + " - " + dr["PRNUM"].ToString() + " - " + dr["BIDGROUP"].ToString();
                try
                {
                    txtDeliveryDate.Text = Convert.ToDateTime(dr["BIDPICKUPDATE"].ToString()).ToString("dd-MMM-yyyy");
                }
                catch { txtDeliveryDate.Text = ""; }
                lblVesselName.Text = dr["ShipID"].ToString();
                lblDateCreated.Text = dr["DATECREATED"].ToString();
                txtDeliveryPort.Text = dr["BIDDELIVERYPORT"].ToString();
                lblVendorName.Text = dr["BIDVENNAME"].ToString();
                lblVendorContactName.Text = dr["BIDVENCONTACT"].ToString();
                lblVendorPhone.Text = dr["BIDVENPHONE"].ToString();
                lblVendorEmail.Text = dr["BIDVENEMAIL"].ToString();
                txtVendorComments.Text = dr["BIDPOCOMMENTSVEN"].ToString();

                makerdetails.Visible = false;
                makerdetails1.Visible = false;

                DataTable dtpo = Common.Execute_Procedures_Select_ByQuery("SELECT PRTYPE FROM DBO.VW_TBLSMDPOMASTER WHERE POID=" + PoID.ToString());
                if (dtpo.Rows.Count > 0)
                {
                    if (dtpo.Rows[0][0].ToString() == "2")
                    {
                        makerdetails.Visible = true;
                        makerdetails1.Visible = true;
                    }
                }
                lblEquipName.Text = dr["EquipName"].ToString();
                lblEquipModel.Text = dr["EquipModel"].ToString();
                lblSerial.Text = dr["EquipSerialNo"].ToString();
                lblYear.Text = dr["EquipYear"].ToString();
                lblMaker.Text = dr["Equipmfg"].ToString();

                ddlCurrency.SelectedValue = dr["BIDCURR"].ToString();
                try
                {
                    if (dr["BIDEXCHDATE"].ToString() != "")
                    {
                        txtCurrDate.Text = Convert.ToDateTime(dr["BIDEXCHDATE"].ToString()).ToString("dd-MMM-yyyy");
                        if (txtCurrDate.Text == "")
                        {
                            txtCurrDate.Text = Convert.ToDateTime(System.DateTime.Now.ToString()).ToString("dd-MMM-yyyy");
                        }
                    }
                    else
                    {
                        txtCurrDate.Text = Convert.ToDateTime(System.DateTime.Now.ToString()).ToString("dd-MMM-yyyy");
                    }
                }
                catch { txtCurrDate.Text = ""; }

                lblCurrRate.Text = Math.Round(Common.CastAsDecimal(dr["BIDEXCHRATE"]), 4).ToString();

                txtVenRef.Text = dr["VENDORREF"].ToString();
                //------------------------------------------------------
                txtLCRow1.Text = Math.Round(Common.CastAsDecimal(dr["ESTSHIPPINGFOR"].ToString()), 2).ToString();
                lblUSDRow1.Text = Math.Round(Common.CastAsDecimal(dr["ESTSHIPPINGUSD"].ToString()), 2).ToString();
                txtTotalDiscount.Text = Math.Round(Common.CastAsDecimal(dr["DisCountPercentage"].ToString()), 2).ToString();
                lblTotalDiscount.Text = Math.Round(Common.CastAsDecimal(dr["TotalDiscount"].ToString()), 2).ToString();
                lblTotalDiscountUSD.Text = Math.Round(Common.CastAsDecimal(dr["TotalDiscountUSD"].ToString()), 2).ToString();
                lblTotalGSTLC.Text = Math.Round(Common.CastAsDecimal(dr["TotalGSTTaxAmount"].ToString()), 2).ToString();
                lblTotalGSTUSD.Text = Math.Round(Common.CastAsDecimal(dr["TotalGSTTaxAmountUSD"].ToString()), 2).ToString();
                try
                {
                    txtExpires.Text = Convert.ToDateTime(dr["EXPIRATION"].ToString()).ToString("dd-MMM-yyyy");
                }
                catch { txtExpires.Text = ""; }

                DataTable dtApprovalDone = Common.Execute_Procedures_Select_ByQuery("select isnull(count(*),0) from DBO.BidApprovalList where bidid=" + BidId.ToString());
                int AppCount = Common.CastAsInt32(dtApprovalDone.Rows[0][0]);
                
                showdetails();
            }
    }
    public void showdetails()
    {
        //DETAIL INFORMATION
        decimal ExchRate = Common.CastAsDecimal(lblCurrRate.Text);
        //Common.Set_Procedures("MTMPOS.DBO.sp_NewPR_getRFQDetailsByBidId");
        //Common.Set_ParameterLength(2);
        //Common.Set_Parameters(
        //    new MyParameter("@BidId", BidId),
        //    new MyParameter("@ExchRate", ExchRate)
        //    );
        //DataTable dtItems = Common.Execute_Procedures_Select().Tables[0];
        DataTable dtItems=Common.Execute_Procedures_Select_ByQuery("EXEC DBO.sp_NewPR_getRFQDetailsByBidId " + BidId.ToString() + "," + ExchRate);
        rptItems.DataSource = dtItems;
        rptItems.DataBind();
        TotalDiscountandUSD();
        TotalLCandUSD();
    }
    public void DisablePageControls()
    {
        Enable = false;
        btnSubmit.Visible = false;
        imgResubmitRFQ.Visible = false;
        ddlCurrency.Enabled = false;
        txtLCRow1.Enabled = false;

        txtDeliveryDate.Enabled = false;
        txtDeliveryPort.Enabled = false;
        txtVenRef.Enabled = false;
        txtExpires.Enabled = false;
        txtVendorComments.Enabled = false;

    }
    public void EnablePageControls()
    {
        Enable = true;
        btnSubmit.Visible = true;
        ddlCurrency.Enabled = true;
        txtLCRow1.Enabled = true;


        txtDeliveryDate.Enabled = true;
        txtDeliveryPort.Enabled = true;
        txtVenRef.Enabled = true;
        txtExpires.Enabled = true;
        txtVendorComments.Enabled = true;
    }



    //protected void txtUnitPrice_OnTextChanged(object sender, EventArgs e)
    //{
    //    TextBox txtUnitPrice = (TextBox)((TextBox)sender).Parent.FindControl("txtUnitPrice");
    //    Label lblLC = (Label)txtUnitPrice.Parent.FindControl("lblLC");
    //    Label lblUsd = (Label)txtUnitPrice.Parent.FindControl("lblUsd");
        


    //    decimal UnitPrice = 0, BidQty = 0, EXCRate = 0;
    //    UnitPrice = Common.CastAsDecimal(txtUnitPrice.Text);
    //    BidQty = Common.CastAsDecimal(((TextBox)txtUnitPrice.Parent.FindControl("txtBidQty")).Text);
    //    EXCRate = Common.CastAsDecimal(lblCurrRate.Text);

    //    lblLC.Text = ProjectCommon.FormatCurrencyWithoutSign(UnitPrice * BidQty);
    //    if (EXCRate != 0)
    //        lblUsd.Text = ProjectCommon.FormatCurrencyWithoutSign((UnitPrice * BidQty) / EXCRate);
    //    else
    //        lblUsd.Text = "0";

    //    TotalLCandUSD();
    //}
    protected void ddlCurrency_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        TotalDiscountandUSD();
        UpdateExchRate();
        DataTable dtRFQ = Common.Execute_Procedures_Select_ByQuery("EXEC DBO.sp_NewPR_getRFQMasterByBidId " + BidId.ToString());
        if(dtRFQ.Rows.Count>0)
        {
            lblUSDRow1.Text = Math.Round(Common.CastAsDecimal(dtRFQ.Rows[0]["ESTSHIPPINGUSD"].ToString()), 2).ToString();
        }
        showdetails();
        TotalLCandUSD();
    }
    protected void txtCurrDate_OnTextChanged(object sender, EventArgs e)
    {
        TotalDiscountandUSD();
        UpdateExchRate();
        DataTable dtRFQ = Common.Execute_Procedures_Select_ByQuery("EXEC mtmpos.DBO.sp_NewPR_getRFQMasterByBidId " + BidId.ToString());
        if (dtRFQ.Rows.Count > 0)
        {
            lblUSDRow1.Text = Math.Round(Common.CastAsDecimal(dtRFQ.Rows[0]["ESTSHIPPINGUSD"].ToString()), 2).ToString();
        }
        showdetails();
        TotalLCandUSD();
    }
    
    protected void imgSave_OnClick(object sender, EventArgs e)
    {
        if(ddlCurrency.SelectedIndex<=0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dialog", "alert('Please select currency.');", true); 
            return;
        }
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT SUM(ForTotal),SUM(USDTotal) FROM [dbo].[tblSMDPODetailBid] WHERE BIDID=" + BidId.ToString());
        decimal LCSum=0, USDSum=0;
        if (dt.Rows.Count > 0)
        {
            LCSum = Common.CastAsDecimal(dt.Rows[0][0]);
            USDSum = Common.CastAsDecimal(dt.Rows[0][1]);
        }
        if (LCSum <= 0 || USDSum <= 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dialog", "alert('Quote item total should not be zero.');", true);
            return;
        }
        if (Common.CastAsDecimal(lblUSDRow2.Text)<= 0)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dialog", "alert('Quote total should not be zero.');", true); 
            return;
        }
        object CurrDate;
        object Expires;
        object DeliveryDate;
        //------
        if (txtCurrDate.Text.Trim() == "")
            CurrDate = DBNull.Value;
        else
            CurrDate = txtCurrDate.Text.Trim();

        //------
        if (txtExpires.Text.Trim() == "")
            Expires = DBNull.Value;
        else
            Expires = txtExpires.Text.Trim();

        //------
        if (txtDeliveryDate.Text.Trim() == "")
            DeliveryDate = DBNull.Value;
        else
            DeliveryDate = txtDeliveryDate.Text.Trim();

        // Update the description 
        //string UpSql="";
        //foreach (RepeaterItem RptItem in rptItems.Items)
        //{
        //    HiddenField hfItemID = (HiddenField)RptItem.FindControl("hfItemID");
        //    Label txtDesc =(Lab)RptItem.FindControl("txtDesc");
        //    UpSql = "update [MTMM2000SQL].[dbo].[TBLSMDPODetailBid] set BidDescription='" + txtDesc.Text.Replace("'", "`") + "' where BidItemID=" + hfItemID.Value + "";
        //    DataTable dt = Common.Execute_Procedures_Select_ByQuery(UpSql);
            
        //}
        // ------------------------------------------


        string UnitPricce = "";
        string BidItemID = "";
        string TotBidQty = "";
        foreach (RepeaterItem RptItem in rptItems.Items)
        {
            HiddenField hfItemID = (HiddenField)RptItem.FindControl("hfItemID");
            UnitPricce = UnitPricce + "," + ((TextBox)RptItem.FindControl("txtUnitPrice")).Text;
            TotBidQty = TotBidQty + "," + ((TextBox)RptItem.FindControl("txtBidQty")).Text;
            BidItemID = BidItemID + "," + hfItemID.Value;
        }
       

        if (UnitPricce.Trim() != "")
        {
            UnitPricce = UnitPricce.Substring(1);
            TotBidQty = TotBidQty.Substring(1);
            BidItemID = BidItemID.Substring(1);
        }

        

        Common.Set_Procedures("sp_NewPR_UpdateQuoteByOffice");
        Common.Set_ParameterLength(8);
        Common.Set_Parameters
            (
                new MyParameter("@BidId", BidId),
                new MyParameter("@BIDDELIVERYPORT", txtDeliveryPort.Text.Trim()),
                new MyParameter("@VENDORREF", txtVenRef.Text.Trim()),
                new MyParameter("@EXPIRATION", Expires),
                new MyParameter("@BIDPICKUPDATE", DeliveryDate),
                new MyParameter("@BIDPOCOMMENTSVEN", txtVendorComments.Text.Trim()),
                new MyParameter("@UserId", UserId),
                new MyParameter("@UserName", UserName)
            );
        DataSet ResDS = new DataSet();
        Boolean Res = false;
        Res = Common.Execute_Procedures_IUD(ResDS);
       
        if (Res == true)
        {
            btnSubmit.Visible = false;            
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dialog", "window.opener.document.location.reload();alert('Your quote has been submitted successfully.');  window.close();", true);            
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dialog", "alert('Data could not be updated.');", true);
        }
    }
    
    protected void txtLCRow1_OnTextChanged(object sender, EventArgs e)
    {
        txtLCRow1.Text = Math.Abs(Common.CastAsDecimal(txtLCRow1.Text)).ToString();
        UpdateExchRate();
        DataTable dtRFQ = Common.Execute_Procedures_Select_ByQuery("EXEC DBO.sp_NewPR_getRFQMasterByBidId " + BidId.ToString());
        if (dtRFQ.Rows.Count > 0)
        {
            lblUSDRow1.Text =Math.Abs( Math.Round(Common.CastAsDecimal(dtRFQ.Rows[0]["ESTSHIPPINGUSD"].ToString()), 2)).ToString();
        }
        showdetails();
        TotalLCandUSD();
    }

    protected void txtTotalDiscount_OnTextChanged(object sender, EventArgs e)
    {
        txtTotalDiscount.Text = Math.Abs(Common.CastAsDecimal(txtTotalDiscount.Text)).ToString();
        TotalDiscountandUSD();
        UpdateExchRate();
        DataTable dtRFQ = Common.Execute_Procedures_Select_ByQuery("EXEC DBO.sp_NewPR_getRFQMasterByBidId " + BidId.ToString());
        if (dtRFQ.Rows.Count > 0)
        {
            lblTotalDiscountUSD.Text = Math.Round(Common.CastAsDecimal(dtRFQ.Rows[0]["TotalDiscountUSD"].ToString()), 2).ToString();
        }
        showdetails();
        TotalLCandUSD();
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "settop", "settop();", true);
        if(btnSubmit.Visible)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "AttachFunc", "AttachFunc();", true);
        }
    }
    
    private void UpdateExchRate()
    {
        lblCurrRate.Text = "0.0";
        string sql = "select top 1 exc_rate from DBO.XCHANGEDAILY where RateDate<='" + txtCurrDate.Text + "' and For_Curr='" + ddlCurrency.SelectedValue + "' order by RateDate desc";
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        lblCurrRate.Text = "0";
        if (Dt != null)
        {
            if (Dt.Rows.Count > 0)
            {
                lblCurrRate.Text = Math.Round(Common.CastAsDecimal(Dt.Rows[0][0]), 4).ToString(); 
            }
        }
        Common.Execute_Procedures_Select_ByQuery("EXEC DBO.sp_NewPR_UpdateCurr " + BidId.ToString() + ",'" + ddlCurrency.SelectedValue + "'," + Common.CastAsDecimal(lblCurrRate.Text) + ",'" + txtCurrDate.Text + "'," + Common.CastAsDecimal(txtLCRow1.Text) + "," + Common.CastAsDecimal(lblTotalDiscount.Text) + "," + Common.CastAsDecimal(lblTotalDiscountUSD.Text) + "," + Common.CastAsDecimal(txtTotalDiscount.Text)  + "," + UserId + ",'" + UserName + "'," + Common.CastAsDecimal(lblTotalGSTLC.Text) + "," + Common.CastAsDecimal(lblTotalGSTUSD.Text) + "");
    }
    protected void TotalLCandUSD()
    {
        try
        {
            decimal TotLC = 0, TotUsd = 0, totalGSTLC = 0, totalGSTUsd = 0;
            string Usd = "";
            foreach (RepeaterItem RptItm in rptItems.Items)
            {

                Label lblLC = (Label)RptItm.FindControl("lblLC");
                Label lblUsd = (Label)RptItm.FindControl("lblUsd");
                Label lblGSTLC = (Label)RptItm.FindControl("lblGSTLC");
                Label lblGSTUSD = (Label)RptItm.FindControl("lblGSTUSD");

                TotLC = TotLC + Common.CastAsDecimal(lblLC.Text);
                TotUsd = TotUsd + Common.CastAsDecimal(lblUsd.Text.Trim());
                totalGSTLC = totalGSTLC + Common.CastAsDecimal(lblGSTLC.Text);
                totalGSTUsd = totalGSTUsd + Common.CastAsDecimal(lblGSTUSD.Text);
            } 
            lblTotalGSTLC.Text = Math.Round(Common.CastAsDecimal(totalGSTLC),2).ToString();
            lblTotalGSTUSD.Text = Math.Round(Common.CastAsDecimal(totalGSTUsd), 2).ToString();
            lblLCRow2.Text = Math.Round(Common.CastAsDecimal((TotLC + Common.CastAsDecimal(txtLCRow1.Text) + Common.CastAsDecimal(totalGSTLC))  - Common.CastAsDecimal(lblTotalDiscount.Text)), 2).ToString();
            lblUSDRow2.Text = Math.Round(Common.CastAsDecimal((TotUsd + Common.CastAsDecimal(lblUSDRow1.Text.Trim()) + Common.CastAsDecimal(totalGSTUsd))  - Common.CastAsDecimal(lblTotalDiscountUSD.Text)), 2).ToString();
        }
        catch (Exception ex)
        {
            string xx = ex.ToString();
        }
        
    }
    protected void TotalDiscountandUSD()
    {
        try
        {
            decimal TotLC = 0, TotUsd = 0, totalGSTLC = 0, totalGSTUsd = 0;
            foreach (RepeaterItem RptItm in rptItems.Items)
            {

                Label lblLC = (Label)RptItm.FindControl("lblLC");
                Label lblUsd = (Label)RptItm.FindControl("lblUsd");
                Label lblGSTLC = (Label)RptItm.FindControl("lblGSTLC");
                Label lblGSTUSD = (Label)RptItm.FindControl("lblGSTUSD");
                TotLC = TotLC + Common.CastAsDecimal(lblLC.Text);
                TotUsd = TotUsd + Common.CastAsDecimal(lblUsd.Text.Trim());
                totalGSTLC = totalGSTLC + Common.CastAsDecimal(lblGSTLC.Text);
                totalGSTUsd = totalGSTUsd + Common.CastAsDecimal(lblGSTUSD.Text);
            }
            //  Math.Round(Common.CastAsDecimal(dtRFQ.Rows[0]["TotalDiscountUSD"].ToString()), 2).ToString();
            lblTotalDiscount.Text = Math.Round(Common.CastAsDecimal(((TotLC+totalGSTLC) * Common.CastAsDecimal(txtTotalDiscount.Text.Trim())) / 100), 2).ToString();
            lblTotalDiscountUSD.Text = Math.Round(Common.CastAsDecimal(((TotUsd+totalGSTUsd) * Common.CastAsDecimal(txtTotalDiscount.Text.Trim())) / 100), 2).ToString();
            //lblTotalDiscount.Text = Math.Round(Common.CastAsDecimal(Math.Round(TotLC * Common.CastAsDecimal(txtTotalDiscount.Text.Trim(),2) / 100)),2).ToString();
            // lblTotalDiscountUSD.Text = Convert.ToString(Math.Round((TotUsd * Common.CastAsDecimal(txtTotalDiscount.Text.Trim())),2) / 100);
        }
        catch (Exception ex)
        {
            string xx = ex.ToString();
        }

    }
    public void BindCurrency()
    {
        ddlCurrency.DataSource = Common.Execute_Procedures_Select_ByQuery("SELECT Curr FROM DBO.VW_tblWebCurr ORDER BY Curr");
        ddlCurrency.DataTextField = "Curr";
        ddlCurrency.DataValueField = "Curr";
        ddlCurrency.DataBind();
        ddlCurrency.Items.Insert(0, new ListItem("", ""));
        ddlCurrency.SelectedIndex = 0;
    }
    public void BindAccount(string  dept)
    {
        //string sql = "select * from (select (select convert(varchar, AccountNumber)+'-'+AccountName from VW_sql_tblSMDPRAccounts PA where DA.AccountID=PA.AccountID and  AccountNumber not like '17%' and AccountNumber !=8590) AccountNumber  ,(select AccountName from  VW_sql_tblSMDPRAccounts PA where DA.AccountID=PA.AccountID and   AccountNumber not like '17%' and AccountNumber !=8590) AccountName  ,AccountID from tblSMDDeptAccounts DA where dept='" + dept + "' and prtype=2) dd where AccountNumber is not null";
        string sql = "select * from (select (select convert(varchar, AccountNumber)+'-'+AccountName from mtmpos.DBO.VW_sql_tblSMDPRAccounts PA where DA.AccountID=PA.AccountID and  AccountNumber not like '17%' and AccountNumber !=8590) AccountNumber  ,(select AccountName from  DBO.VW_sql_tblSMDPRAccounts PA where DA.AccountID=PA.AccountID and   AccountNumber not like '17%' and AccountNumber !=8590) AccountName  ,AccountID from DBO.tblSMDDeptAccounts DA where dept='" + dept + "' and prtype=1) dd where AccountNumber is not null";
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Query", sql));
        DataSet dsPrType = new DataSet();
        dsPrType.Clear();
        dsPrType = Common.Execute_Procedures_Select();

        //ddlAccCode.DataSource = dsPrType;
        //ddlAccCode.DataTextField = "AccountNumber";
        //ddlAccCode.DataValueField = "AccountID";
        //ddlAccCode.DataBind();
        //ddlAccCode.Items.Insert(0, new ListItem("<Select>", "0"));
        //ddlAccCode.SelectedIndex = 0;

        
    }

    protected void imgResubmitRFQ_OnClick(object sender, EventArgs e)
    {
        Common.Execute_Procedures_Select_ByQuery("EXEC dbo.UpdateBidHistory " + BidId + "," + UserId +",'" + UserName +"','Bid Status Changed to Awaiting Quote for (Allow vendor to resubmit).'");
        Common.Set_Procedures("sp_NewPR_UpdateBidStatus");
        Common.Set_ParameterLength(2);
        Common.Set_Parameters
            (
                new MyParameter("@BidID", BidId),
                new MyParameter("@StatusId", "1")
            );
        
        DataSet ResDs = new DataSet();
        Boolean res  = Common.Execute_Procedures_IUD(ResDs);
        if (res == true)
        {
     
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dialog", "window.opener.document.location.reload();alert('Bid Status has been updated successfully.');", true);
            showrecord();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dialog", "alert('Bid Status can't updated.');", true);
        }
    }
    //-- -------------------
    [WebMethod()]
    public static string Update_unitpriceAndquantity(int BidItemID, string BidQty, string UnitPrice, int BidId,int RecID, string curentrate, string vendordescription, string DiscountPercentage, string GSTTaxPercentage)
    {
        
        int UserId = Common.CastAsInt32(HttpContext.Current.Session["loginid"]);
        string UserName = Convert.ToString(HttpContext.Current.Session["FullUserName"]);

        string sql = " ";
        decimal Qty = 0;
        decimal rate = 0;

        decimal priceLC =0;
        decimal priceUSD = 0;
        decimal GSTTaxLC = 0;
        decimal GSTTaxUSD = 0;

        string sqlrate = " exec dbo.sp_UpdateUnitprice " + BidId + "," + BidItemID + "," + RecID + ",'" + Math.Abs(Common.CastAsDecimal(BidQty)) + "','" + Math.Abs(Common.CastAsDecimal(UnitPrice)) + "'," + curentrate+",'"+ vendordescription+"'," + UserId + ",'" + UserName + "','" + Math.Abs(Common.CastAsDecimal(GSTTaxPercentage)) + "'";
        DataTable dtreate = Common.Execute_Procedures_Select_ByQuery(sqlrate);

        Common.Execute_Procedures_Select_ByQuery("EXEC DBO.Sp_UpdateDiscountTBLSMDPOMASTERBid " + BidId.ToString() + "," + Common.CastAsDecimal(DiscountPercentage) + "");

        if (dtreate.Rows.Count > 0)
        {
            priceLC = Common.CastAsDecimal(dtreate.Rows[0][0]);
            priceUSD = Common.CastAsDecimal(dtreate.Rows[0][1]);
            GSTTaxLC = Common.CastAsDecimal(dtreate.Rows[0][2]);
            GSTTaxUSD = Common.CastAsDecimal(dtreate.Rows[0][3]);
        }
        string message = "{'priceLC':'"+ priceLC + "','priceUSD':'"+ priceUSD + "','gsttaxLC':'"+ GSTTaxLC + "','gsttaxUSD':'"+ GSTTaxUSD + "'}";
        return message.Replace("'","\"");
    }

    protected void GetDocCount(int POid)
    {
        string sql = "";
        if (POid > 0)
        {
            sql = "SELECT Count(DocId) As DocumentCount FROM [tblSMDPODocuments] with(nolock) WHERE  PoId =" + POid;
            DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
            lblAttchmentCount.Text = DT.Rows[0]["DocumentCount"].ToString();
        }

        if (Convert.ToInt32(lblAttchmentCount.Text) > 0)
        {
            ImgAttachment.Enabled = true;
        }
        else
        {
            ImgAttachment.Enabled = false;
        }
    }

    protected void ImgAttachment_Click(object sender, ImageClickEventArgs e)
    {
        if (Convert.ToInt32(lblAttchmentCount.Text) <= 0)
        {
            return;
        }

        divAttachment.Visible = true;
        ShowAttachment();
    }
    protected void btnPopupAttachment_Click(object sender, EventArgs e)
    {
        divAttachment.Visible = false;
    }

    protected void imgClosePopup_Click(object sender, ImageClickEventArgs e)
    {
        divAttachment.Visible = false;
    }
    public void ShowAttachment()
    {
        string sql = "";
        if (PoID > 0)
        {
            sql = "SELECT DocId, DocName As FileName, PoId As RequisitionId, VesselCode, (Select top 1 StatusID from tblSMDPOMaster p where p.PoId= Pod.PoId) As StatusId FROM [tblSMDPODocuments] Pod with(nolock) WHERE  Pod.PoId =" + PoID;
            DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
            if (DT.Rows.Count > 0)
            {
                rptDocuments.DataSource = DT;
                rptDocuments.DataBind();
                lblAttchmentCount.Text = DT.Rows.Count.ToString();
                lblAttchmentCount.Enabled = true;
            }
            else
            {
                rptDocuments.DataSource = null;
                rptDocuments.DataBind();
                lblAttchmentCount.Text = "0";
                lblAttchmentCount.Enabled = false;
            }

        }
    }

    protected void ImgDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int DocId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
            string sql = "";
            if (PoID > 0)
            {
                sql = "Delete from tblSMDPODocuments  WHERE PoId =" + PoID + " AND DocId = " + DocId;
                DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
            }

            ShowAttachment();
        }
        catch (Exception ex)
        {
            ProjectCommon.ShowMessage(ex.Message.ToString());
        }
    }

}
