using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class VeiwRFQDetailsForApproval : System.Web.UI.Page
{
    //---------------------
    #region Properties ***************************************************
    public int BidId
    {
        set { ViewState["BidId"] = value; }
        get { return int.Parse("0" + ViewState["BidId"]); }
    }
    public int PoID
    {
        set { ViewState["_PoID"] = value; }
        get { return int.Parse("0" + ViewState["_PoID"]); }
    }

    public bool IsMultiAccount
    {
        set { ViewState["IsMultiAccount"] = value; }
        get { return Convert.ToBoolean(ViewState["IsMultiAccount"]); }
    }
    #endregion
    //---------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        #region --------- USER RIGHTS MANAGEMENT -----------
        try
        {
            AuthenticationManager authPO = new AuthenticationManager(1064, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
            if (!(authPO.IsView))
            {
                Response.Redirect("~/AuthorityError.aspx");
            }

            imgCancelPO.Visible = authPO.IsDelete;
            imgPrint.Visible=  authPO.IsPrint;

            AuthenticationManager authRecGoods = new AuthenticationManager(1064, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
            btnRecGoods.Visible = authRecGoods.IsView;

            AuthenticationManager authRecInv = new AuthenticationManager(1064, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
            btnEnterInv.Visible = authRecInv.IsView;
        }
        catch (Exception ex)
        {
            Response.Redirect("~/NoPermission.aspx?Message=" + ex.Message);
        }
        #endregion ----------------------------------------

        lblmsg.Text = "";
        lblmsginvoice.Text = "";
        if (!(IsPostBack))
        {
            BidId = int.Parse("0" + Page.Request.QueryString["BidId"]);
            imgPrint.Attributes.Add("onclick", "window.open('Print.aspx?POID=" + BidId.ToString() + "'); return false;");
            btnOrderComments.Attributes.Add("onclick", "OpenWindow(" + BidId + ");");
            ShowDetails();
            ShowApprovalRequestByOn();
            ShowSupplierRatings();
            GetDocCount(PoID);
        }
    }
    //--------------------- 
    #region Events *******************************************************

    // Button ----------------------------------------------------------------------
    protected void ImdEditSpareRFQ_OnClick(object sender, EventArgs e)
    {
        imgSaveSpareRFQ.Visible = true;
    }
    protected void imgResubmitRFQ_OnClick(object sender, EventArgs e)
    {
        Common.Set_Procedures("sp_NewPR_UpdateBidStatus");
        Common.Set_ParameterLength(2);
        Common.Set_Parameters
            (
                new MyParameter("@BidID", BidId),
                new MyParameter("@StatusId", "1")
            );
        Boolean res;
        DataSet ResDs = new DataSet();
        res = Common.Execute_Procedures_IUD(ResDs);

        if (res == true)
        {
            lblmsg.Text = "Bid Status has been updated successfully.";
        }
        else
        {
            lblmsg.Text = "Bid Status can't updated.";
        }
    }
    protected void btnViewGoodsRcv_OnClick(object sender, EventArgs e)
    {
        // Response.Redirect("ReceivePO.aspx?BidId=" + BidId.ToString() + ""); 

        ScriptManager.RegisterStartupScript(this, this.GetType(), "RecieveGoods", "window.open('ReceivePO.aspx?BidId=" + BidId.ToString() + "');", true);
    }
    public void ShowApprovalRequestByOn()
    {
        string sql = " select BidFwdByID,BidFwdOn,L.FirstName+' '+L.LastName as UserName from Add_tblSMDPOMasterBid  PM  "+
                     "  left join  dbo.userlogin L on PM.BidFwdByID = L.LoginId Where BidID = "+BidId;

        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DT.Rows.Count > 0)
        {
            lblApprovalrequestByOn.Text = DT.Rows[0]["UserName"].ToString()+" / "+ Common.ToDateString(DT.Rows[0]["BidFwdOn"].ToString());
        }

         sql = " select BidComments from vw_tblSMDPOMasterBid Where BidID= " + BidId;
        DataTable DTC = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DTC.Rows.Count > 0)
        {
            lblApprovalrequestComments.Text = DTC.Rows[0]["BidComments"].ToString();
        }
    }
    private void ShowSupplierRatings()
    {

        DataTable dt11 = Common.Execute_Procedures_Select_ByQuery("select Rating,comments from BidSupplerComments where bidid=" + BidId + "");
        if (dt11.Rows.Count > 0)
        {
            if (!Convert.IsDBNull(dt11.Rows[0]["Rating"]))
            {
                hfdRating.Value = dt11.Rows[0]["Rating"].ToString();
                txtSupplierComments.Text = dt11.Rows[0]["Comments"].ToString();
            }
        }
    }
    protected void btnInvoice_OnClick(object sender, EventArgs e)
    {
        if (GetBidStatus(BidId) > 4)
        {
            //  Response.Redirect("~/Modules/Purchase/Invoice/InvoiceEntry.aspx?BidId=" + BidId.ToString()+ "");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PO-Invoice", "window.open('../Invoice/InvoiceEntry.aspx?BidId=" + BidId.ToString() + "');", true);
        }
        else
        {
            lblmsginvoice.Text = "Can not enter invoice(Receipt not completed).";
        }
        //Response.Redirect("InvoiceEntry.aspx?BidId=" + BidId.ToString() + ""); 
    }
    protected void btnSendMail_OnClick(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "SendMail", "window.open('SendMail.aspx?Mailtype=2&Param=" + BidId.ToString() + "');", true);
    }
    public int GetBidStatus(int _BidID)
    {
        string sql = "select  BidStatusID  from vw_tblsmdpomasterbid where BidID=" + _BidID + "";
        DataTable Dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (Dt != null)
        {
            if (Dt.Rows.Count > 0)
            {
                return Common.CastAsInt32(Dt.Rows[0][0]);
            }
            else
            {
                return 0;
            }
        }
        else
            return 0;
    }
    public void imgCancelPO_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (GetBidStatus(Common.CastAsInt32(BidId)) >= 6)
            {
                lblmsg.Text = "Unable to cancel this PO (Invoice Entered).";
                return;
            }

            Common.Set_Procedures("sp_NewPR_CancelPO");
            Common.Set_ParameterLength(2);
            Common.Set_Parameters(new MyParameter("@POId", BidId), new MyParameter("@UserName", Session["UserName"]), new MyParameter("@LoginId", Session["loginid"]));
            DataSet ds = new DataSet();
            if (Common.Execute_Procedures_IUD(ds))
            {

                lblmsg.Text = "PO cancelled successfully.";
            }
            else
            {
                lblmsg.Text = "Unable to cancel PO.";
            }
        }
        catch(Exception ex)
        {
            lblmsg.Text = ex.Message.ToString();
        }
       
    }

    // TextBox ---------------------------------------------------------------------
    protected void txtOrderQTY_OnTextChanged(object sender, EventArgs e)
    {
        decimal TotalUSD = 0;
        TextBox txtOrderQty =(TextBox)sender;
        decimal OrderQty = Common.CastAsDecimal(((TextBox)sender).Text);
        Label lblUnitPrice = (Label)txtOrderQty.Parent.FindControl("lblUnitPrice");
        decimal UnitPrice = Common.CastAsDecimal(lblUnitPrice.Text);

        Label LC = (Label)txtOrderQty.FindControl("lblLC");
        Label USD = (Label)txtOrderQty.FindControl("lblUsd");
        LC.Text =Math.Round( Common.CastAsDecimal(OrderQty * UnitPrice),2).ToString();
        if (ViewState["vw_bidExchRate"] != null && ViewState["vw_bidExchRate"].ToString() != "0")
        {
            USD.Text = Math.Round(Common.CastAsDecimal(Common.CastAsDecimal(LC.Text) / Common.CastAsDecimal(ViewState["vw_bidExchRate"].ToString())), 2).ToString();
        }
        else
        {
            USD.Text = "0";
        }
        CountTotalUSD();
        

    }
    protected void txtLC_OnTextChanged(object sender, EventArgs e)
    {
        if (ViewState["vw_bidExchRate"] != null && ViewState["vw_bidExchRate"].ToString() != "0")
        {

            lblUSD.Text = Math.Round(Common.CastAsDecimal(txtLC.Text) / Common.CastAsDecimal(ViewState["vw_bidExchRate"].ToString()), 2).ToString();

        }
        else
        {
            lblUSD.Text = "0"; 
        }

        if (ViewState["vTotalUSDs"] != null && ViewState["vTotalUSDs"].ToString() != "0")
        {
            if (ViewState["vDisPercentage"] != null && ViewState["vDisPercentage"].ToString() != "0")
            {
                //Decimal disPerUSD = 0;
                lblTotalDiscountUsd.Text = Math.Round((Common.CastAsDecimal(ViewState["vTotalUSDs"].ToString()) * Common.CastAsDecimal(ViewState["vDisPercentage"].ToString())) / 100, 2).ToString();
                lblTotalUSdD.Text = Math.Round(Common.CastAsDecimal(Common.CastAsDecimal(ViewState["vTotalUSDs"].ToString()) + Common.CastAsDecimal(lblUSD.Text) - Common.CastAsDecimal(lblTotalDiscountUsd.Text.ToString())), 2).ToString();
            }
            else
            {
                lblTotalUSdD.Text = Math.Round(Common.CastAsDecimal(Common.CastAsDecimal(ViewState["vTotalUSDs"].ToString()) + Common.CastAsDecimal(lblUSD.Text)), 2).ToString();
            }

            if (ViewState["vTotalLCs"] != null && ViewState["vTotalLCs"].ToString() != "0")
            {
                Decimal TotalDiscountLC = 0;

                TotalDiscountLC = Math.Round(Common.CastAsDecimal(Common.CastAsDecimal(ViewState["vTotalLCs"].ToString()) * Common.CastAsDecimal(ViewState["vDisPercentage"].ToString())) / 100, 2);

                lblTotalLCD.Text = Math.Round(Common.CastAsDecimal(Common.CastAsDecimal(ViewState["vTotalLCs"].ToString()) + Common.CastAsDecimal(txtLC.Text)) - Common.CastAsDecimal(TotalDiscountLC), 2).ToString();
            }
            else
            {
                Decimal TotalDiscountLC = 0;
                TotalDiscountLC = Math.Round(Common.CastAsDecimal(Common.CastAsDecimal(ViewState["vTotalLCs"].ToString()) * Common.CastAsDecimal(ViewState["vDisPercentage"].ToString())) / 100, 2);
                // lblTotalDiscountUSD.Text = Math.Round(TotalDiscountUsd, 2).ToString();
                lblTotalLCD.Text = Math.Round(Common.CastAsDecimal(Common.CastAsDecimal(ViewState["vTotalLCs"].ToString())) - Common.CastAsDecimal(TotalDiscountLC), 2).ToString();
            }

        }
        else
        {
            if (ViewState["vDisPercentage"] != null && ViewState["vDisPercentage"].ToString() != "0")
            {
                lblTotalDiscountUsd.Text = Math.Round((Common.CastAsDecimal(ViewState["vTotalUSDs"].ToString()) * Common.CastAsDecimal(ViewState["vDisPercentage"].ToString())) / 100, 2).ToString();
                lblTotalUSdD.Text = Math.Round(Common.CastAsDecimal(Common.CastAsDecimal(ViewState["vTotalUSDs"].ToString()) - -Common.CastAsDecimal(lblTotalDiscountUsd.Text.ToString())), 2).ToString();
            }
            else
            {
                lblTotalUSdD.Text = Math.Round(Common.CastAsDecimal(Common.CastAsDecimal(ViewState["vTotalUSDs"].ToString())), 2).ToString();
            }

            if (ViewState["vTotalLCs"] != null && ViewState["vTotalLCs"].ToString() != "0")
            {
                lblTotalLCD.Text = Math.Round(Common.CastAsDecimal(Common.CastAsDecimal(ViewState["vTotalLCs"].ToString()) + Common.CastAsDecimal(txtLC.Text)), 2).ToString();
            }
            else
            {
                // lblTotalDiscountUSD.Text = Math.Round(TotalDiscountUsd, 2).ToString();
                lblTotalLCD.Text = Math.Round(Common.CastAsDecimal(Common.CastAsDecimal(ViewState["vTotalLCs"].ToString())), 2).ToString();
            }

        }
    }
    // DropDown---------------------------------------------------------------------
    protected void ddlCurrency_OnSelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void imgApp1_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgApp2_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgPlaceOrder_Click(object sender, ImageClickEventArgs e)
    {

    }

    #endregion
    //---------------------
    #region Function *****************************************************

    //public void UpdateTotalUSD()
    //{
    //    decimal TotalUSD = 0;
    //    foreach (RepeaterItem RptItm in rptItems.Items)
    //    {
    //        TotalUSD =TotalUSD + Common.CastAsDecimal( ((Label)RptItm.FindControl("lblUsd")).Text);
    //    }

    //    lblTotalUSdD.Text = Convert.ToString( TotalUSD + Common.CastAsDecimal(lblUSD.Text) - Common.CastAsDecimal(lblTotalDiscountUsd.Text));
    //}
    public void CountTotalUSD()
    {
        //Count total USD
        decimal TotalUSD = 0;
        decimal TotalLC = 0;
        decimal totalLCDiscount = 0;
        decimal totalGSTLC = 0;
        decimal totalGSTUSD = 0;
        foreach (RepeaterItem TptItem in rptItems.Items)
        {
            TotalUSD = TotalUSD + Common.CastAsDecimal(((Label)TptItem.FindControl("lblUsd")).Text);
            TotalLC = TotalLC + Common.CastAsDecimal(((Label)TptItem.FindControl("lblLC")).Text);
            totalGSTLC = totalGSTLC + Common.CastAsDecimal(((Label)TptItem.FindControl("lblGSTLC")).Text);
            totalGSTUSD = totalGSTUSD + Common.CastAsDecimal(((Label)TptItem.FindControl("lblGSTUsd")).Text);
        }
        ViewState["vTotalUSDs"] = (TotalUSD+totalGSTUSD).ToString();
        ViewState["vTotalLCs"] = (TotalLC+totalGSTLC).ToString();
        if (ViewState["vDisPercentage"] != null && ViewState["vDisPercentage"].ToString() != "0")
        {
            lblTotalDiscountUsd.Text = Math.Round((Common.CastAsDecimal(ViewState["vTotalUSDs"].ToString()) * Common.CastAsDecimal(ViewState["vDisPercentage"].ToString())) / 100, 2).ToString();
            lblTotalUSdD.Text = Math.Round(Common.CastAsDecimal(TotalUSD + totalGSTUSD + Common.CastAsDecimal(lblUSD.Text) - Common.CastAsDecimal(lblTotalDiscountUsd.Text)), 2).ToString();
            totalLCDiscount = Math.Round((Common.CastAsDecimal(ViewState["vTotalLCs"].ToString()) * Common.CastAsDecimal(ViewState["vDisPercentage"].ToString())) / 100, 2);
            lblTotalLCD.Text = Math.Round(Common.CastAsDecimal(TotalLC + totalGSTLC  + Common.CastAsDecimal(txtLC.Text) - Common.CastAsDecimal(totalLCDiscount)), 2).ToString();
        }
        else
        {
            lblTotalUSdD.Text = Math.Round(Common.CastAsDecimal(TotalUSD + totalGSTUSD + Common.CastAsDecimal(lblUSD.Text) - Common.CastAsDecimal(lblTotalDiscountUsd.Text)), 2).ToString();
            lblTotalLCD.Text = Math.Round(Common.CastAsDecimal(TotalLC + totalGSTLC + Common.CastAsDecimal(txtLC.Text) - Common.CastAsDecimal(totalLCDiscount)), 2).ToString();
        }
        lblTotalGSTLC.Text = Math.Round(Common.CastAsDecimal(totalGSTLC), 2).ToString();
        lblTotalGSTUSD.Text = Math.Round(Common.CastAsDecimal(totalGSTUSD), 2).ToString();
    }
    public void SetRowNumber()
    {
        int i = 1;
        foreach (RepeaterItem rpt in rptItems.Items)
        {
            Label lblRowNumber = (Label)rpt.FindControl("lblRowNumber");
            lblRowNumber.Text = i.ToString();
            i = i + 1;
        }

    }
    #endregion
    //---------------------
    public void ShowDetails()
    {
        SetUserControlData();
        Bind_rptApprovalList();

        DataTable dtRFQ = Common.Execute_Procedures_Select_ByQuery("EXEC DBO.sp_NewPR_getRFQMasterByBidId " + BidId.ToString());
        DataTable dtTravExport = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.TBLAPENTRIES WHERE BIDID IN (	SELECT BIDID FROM DBO.tblSMDpoMasterBid M WHERE M.poid IN 	( SELECT POID FROM DBO.tblSMDpoMasterBid WHERE BIDID=" + BidId.ToString() + " ) ) AND INTRAV = 1");
        if (dtTravExport.Rows.Count <=0)
        {
            lnkChangeAccountCode.Visible = true;
        }

        if (dtRFQ != null)
        {
            if (dtRFQ.Rows.Count > 0)
            {
                // MASTER INFORMATION 
                LoadBreakDown();
                DataRow dr = dtRFQ.Rows[0];
                lblRFQNO.Text = dr["RFQNO"].ToString();
                lblReqNo.Text = dr["REQNO"].ToString();
                ViewState.Add("PRTYPE", dr["PRTYPE"].ToString());
                BindAccount(dr["dept"].ToString());
                lblReqType.Text = dr["PRTYPENAME"].ToString();
                lblCreatedBy.Text = dr["POCREATOR"].ToString();
                lblDateCreated.Text = dr["DATECREATED"].ToString();
                //lblAccountName.Text = dr["ACCOUNTNUMBER"].ToString() + " - " + dr["ACCOUNTNAME"].ToString();
                //lblAccountName.ToolTip = dr["ACCOUNTNAME"].ToString();
                lblAccountCode.Text = dr["ACCOUNTNUMBER"].ToString() + " - " + dr["ACCOUNTNAME"].ToString();
                lblSmdComments.Text = dr["REMARKSSMD"].ToString();
                lblVenComments.Text = dr["POCOMMENTSVEN"].ToString();
                ViewState.Add("vw_bidExchRate", dr["bidExchRate"].ToString());
                lblPortNDate.Text = dr["Port"].ToString();
                if (dr["Podate"].ToString() != "")
                {
                    lblPortNDate.Text = lblPortNDate.Text + "<b> /</b> " + Convert.ToDateTime(dr["Podate"].ToString()).ToString("dd-MMM-yyyy");
                }

                txtLC.Text = Math.Round(Common.CastAsDecimal(dr["estShippingFor"].ToString())).ToString();
                lblUSD.Text = Math.Round(Common.CastAsDecimal(dr["estshippingUSD"]), 2).ToString();
                txtShipComments.Text = dr["biddeliveryinstructions"].ToString();
                lblDisPer.Text = Math.Round(Common.CastAsDecimal(dr["DisCountPercentage"]), 2).ToString();
                ViewState["vDisPercentage"] = lblDisPer.Text.ToString();
                lblTotalDiscountUsd.Text = Math.Round(Common.CastAsDecimal(dr["TotalDiscountUSD"]), 2).ToString();
                lblTotalGSTLC.Text = Math.Round(Common.CastAsDecimal(dr["TotalGSTTaxAmount"].ToString())).ToString();
                lblTotalGSTUSD.Text = Math.Round(Common.CastAsDecimal(dr["TotalGSTTaxAmountUSD"].ToString())).ToString();
                lblVenderName.Text = dr["SupplierName"].ToString();
		        lblbackorder.Text = ((dr["OrderType"].ToString()=="B")?"BackOrder":"");

                lblLocalCurr.Text = dr["BidCurr"].ToString();
                lblCurrRate.Text = dr["BidExchRate"].ToString();
                if (dr["BidExchDate"].ToString() != "")
                {
                    lblBidExchDate.Text = Convert.ToDateTime(dr["BidExchDate"].ToString()).ToString("dd-MMM-yyyy");
                }

                lblPoDesc.Text = dr["BiddeliveryInstructions"].ToString();
                //---------------------------
                string DtApproval = dr["PRStatusID"].ToString();
                ViewState.Add("PRStatusID", DtApproval);

                if (DtApproval == "25")
                {
                    tblApp1.Visible = true;
                    tblApp2.Visible = false;
                    lblApp1Name.Text = dr["BidSMDLevel1Approval"].ToString();
                    if (dr["BidSMDLevel1ApprovalDate"].ToString() != "")
                    {
                        lblApp1On.Text = Convert.ToDateTime(dr["BidSMDLevel1ApprovalDate"].ToString()).ToString("dd-MMM-yyyy");
                    }
                    txtVenderComm.Text = dr["ApproveComments"].ToString();
                    txtQuarComm.Text = dr["comment"].ToString();
                }
                else
                {
                    tblApp1.Visible = true;
                    // IF II ND APPROVAL WAS NEEDED THEN NEED TO SHOW 
                    tblApp2.Visible = false;
                    DataTable dtIInd = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM BIDAPPROVALLIST WHERE BIDID=" + BidId.ToString());
                    if (dtIInd != null)
                        if (dtIInd.Rows.Count > 0)
                        {
                            if (Common.CastAsInt32(dtIInd.Rows[0]["ApprovalPhase"]) == 2)
                            {
                                tblApp2.Visible = true;
                            }
                        }


                    lblApp1Name.Text = dr["BidSMDLevel1Approval"].ToString();
                    if (dr["BidSMDLevel1ApprovalDate"].ToString() != "")
                    {
                        lblApp1On.Text = Convert.ToDateTime(dr["BidSMDLevel1ApprovalDate"].ToString()).ToString("dd-MMM-yyyy");
                    }
                    lblApp2Name.Text = dr["BidSMDLevel2Approval"].ToString();
                    if (dr["BidSMDLevel2ApprovalDate"].ToString() != "")
                    {
                        lblApp2On.Text = Convert.ToDateTime(dr["BidSMDLevel2ApprovalDate"].ToString()).ToString("dd-MMM-yyyy");
                    }
                    txtVenderComm.Text = dr["ApproveComments"].ToString();
                    txtQuarComm.Text = dr["comment"].ToString();
                }

                //------------------------------------------------------
                //Equpipment Information
                lblEquipName.Text = dr["EquipName"].ToString();
                lblModelType.Text = dr["EquipModel"].ToString();
                lblSerial.Text = dr["EquipSerialNo"].ToString();
                lblBuiltYear.Text = dr["EquipYear"].ToString();
                lblNameAddress.Text = dr["Equipmfg"].ToString();
                //------------------------------------------------------
                if (! string.IsNullOrEmpty(dr["POAccCompanyName"].ToString()))
                {
                    lblPOAccountCompany.Text = dr["POAccCompanyName"].ToString();
                }
               else
                {
                    lblPOAccountCompany.Text = "";
                }
                //DETAIL INFORMATION
                decimal ExchRate = Math.Round(Common.CastAsDecimal(dr["BIDEXCHRATE"]), 4);

                Common.Set_Procedures("sp_NewPR_getRFQDetailsByBidId_ProductAccepted");
                Common.Set_ParameterLength(2);
                Common.Set_Parameters(
                    new MyParameter("@BidId", BidId),
                    new MyParameter("@ExchRate", ExchRate)
                    );
                DataTable dtItems = Common.Execute_Procedures_Select().Tables[0];
                rptItems.DataSource = dtItems;
                rptItems.DataBind();

                //Set Row Number
                SetRowNumber();

                // Get the total USD
                CountTotalUSD();

                //Get Budget Details
                //DataTable dtBudget= ProjectCommon.getBudgetDetails(dr["ShipID"].ToString(), dr["ACCOUNTID"].ToString(), DateTime.Today);
                //if (dtBudget != null)
                //{
                //    if (dtBudget.Rows.Count > 0)
                //    {
                //        lblAnnualBudget.Text = Math.Round(Common.CastAsDecimal(dtBudget.Rows[0][0].ToString()), 2).ToString();
                //        lblConsumedDate.Text = Math.Round(Common.CastAsDecimal(dtBudget.Rows[0][1].ToString()), 2).ToString();
                //        lblBudgetRem.Text = Math.Round(Common.CastAsDecimal(dtBudget.Rows[0][2].ToString()), 2).ToString();
                //        lblUtilization.Text = dtBudget.Rows[0][3].ToString();
                //        if (Common.CastAsDecimal(dtBudget.Rows[0][2].ToString()) < 0)
                //        {
                //            lblBudgetRem.ForeColor = System.Drawing.Color.Red;
                //            //lblBudgetRem.Text = lblBudgetRem.Text.Replace("-",""); 
                //        }
                //    }
                //}
            }
        }
    }
    public void BindAccount(string dept)
    {
        //string sql = "select * from (select (select convert(varchar, AccountNumber)+'-'+AccountName from VW_sql_tblSMDPRAccounts PA where DA.AccountID=PA.AccountID and  AccountNumber not like '17%' and AccountNumber !=8590) AccountNumber  ,(select AccountName from  VW_sql_tblSMDPRAccounts PA where DA.AccountID=PA.AccountID and   AccountNumber not like '17%' and AccountNumber !=8590) AccountName  ,AccountID from tblSMDDeptAccounts DA where dept='" + dept + "' and prtype=2) dd where AccountNumber is not null";
        //string sql = "select * from (select (select convert(varchar, AccountNumber)+'-'+AccountName from VW_sql_tblSMDPRAccounts PA where DA.AccountID=PA.AccountID and  AccountNumber not like '17%' and AccountNumber !=8590) AccountNumber  ,(select AccountName from  VW_sql_tblSMDPRAccounts PA where DA.AccountID=PA.AccountID and   AccountNumber not like '17%' and AccountNumber !=8590) AccountName  ,AccountID from tblSMDDeptAccounts DA where dept='" + dept + "' and prtype=" + ViewState["PRTYPE"].ToString() + ") dd where AccountNumber is not null";
        string sql = "select AccountID,convert(varchar, AccountNumber)+'-'+AccountName as AccountNumber from vw_sql_tblSMDPRAccounts order by AccountNumber";
        
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
        //ddlAccCode.SelectedIndex = 0;
    }
    //protected void btnSaveAccCode_OnClick(object sender, EventArgs e)
    //{
    //    string sql = "EXEC dbo.Change_AccountCode_ByBidId " + BidId.ToString() + "," + ddlAccCode.SelectedValue;

    //    DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
    //    if (dt != null)
    //    {
    //        //lblAccountName.Text = ddlAccCode.SelectedItem.Text;
    //        //ShowBudgetDetails(ViewState["ShipID"].ToString(), ddlAccCode.SelectedValue);
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "ff", "alert('Updated Successfully.');", true);
    //        lblAccountCode.Text = ddlAccCode.SelectedItem.Text;    
    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "ff", "alert('Could not updated.');", true);
    //    }
    //}
    protected void LoadBreakDown()
    {
        DataTable dtApprovalDetails = Common.Execute_Procedures_Select_ByQuery("select Breakdown From Add_tblSMDPOMasterBid1 Where BidId=" + BidId.ToString());
        ChkBreakdown.Checked = false;
        if (dtApprovalDetails.Rows.Count > 0)
        {
            ChkBreakdown.Checked = (Convert.ToBoolean(dtApprovalDetails.Rows[0][0]));
            lblBreakdown.Text = (Convert.ToBoolean(dtApprovalDetails.Rows[0][0])) ? "BreakDown" : "Budgeted Task ";
   	    lblBreakdown.ForeColor = (Convert.ToBoolean(dtApprovalDetails.Rows[0][0])) ? System.Drawing.Color.Red : System.Drawing.Color.Green;
        }
    }
    protected void ChkBreakdown_OnCheckedChanged(object sender, EventArgs e)
    {
        Common.Set_Procedures("sp_NewPR_IU_Add_tblSMDPOMasterBid1");
        Common.Set_ParameterLength(2);
        Common.Set_Parameters(
            new MyParameter("@BidID", BidId.ToString()),
            new MyParameter("@Breakdown", (ChkBreakdown.Checked) ? "1" : "0")
            );
        DataSet dsRes = new DataSet();
        Boolean res;
        res = Common.Execute_Procedures_IUD(dsRes);
    }
    //---------------------
    protected void Bind_rptApprovalList()
    {
       // bool IsParentEnable = false;
        string sql = " select FirstName+' ' +LastName as Name,BidId,'Approval '+ convert(varchar,ApprovalPhase)as ApprovalPhase,ApprovalPhase as ApprovalPhaseID,ApprovedOn,Comments  " +
                     "   from BidApprovalList bal  " +
                     "   left  " +
                     "   join dbo.usermaster um on um.LoginId = bal.LoginId where BidId=" + BidId + " order by ApprovalPhase ";
        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(sql);
        //rptApprovalList.DataSource = dt1;
        //rptApprovalList.DataBind();

        foreach (DataRow dr in dt1.Rows)
        {
            if (dr["ApprovalPhaseID"].ToString() == "1")
            {
                lblApprovalName_1.Text = dr["Name"].ToString();
                lblApprovaledOn_1.Text = Common.ToDateString(dr["ApprovedOn"].ToString());
                lblApprovaledComments_1.Text = dr["Comments"].ToString();

                

                
            }
            else if (dr["ApprovalPhaseID"].ToString() == "2")
            {
                lblApprovalName_2.Text = dr["Name"].ToString();
                lblApprovaledOn_2.Text = Common.ToDateString(dr["ApprovedOn"].ToString());
                lblApprovaledComments_2.Text = dr["Comments"].ToString();
                

            }
            else if (dr["ApprovalPhaseID"].ToString() == "3")
            {
                lblApprovalName_3.Text = dr["Name"].ToString();
                lblApprovaledOn_3.Text = Common.ToDateString(dr["ApprovedOn"].ToString());
                lblApprovaledComments_3.Text = dr["Comments"].ToString();
                

            }
            else if (dr["ApprovalPhaseID"].ToString() == "4")
            {
                lblApprovalName_4.Text = dr["Name"].ToString();
                lblApprovaledOn_4.Text = Common.ToDateString(dr["ApprovedOn"].ToString());
                lblApprovaledComments_4.Text = dr["Comments"].ToString();

            }
            else if (dr["ApprovalPhaseID"].ToString() == "5")
            {
                lblApprovalName_5.Text = dr["Name"].ToString();
                lblApprovaledOn_5.Text = Common.ToDateString(dr["ApprovedOn"].ToString());
                lblApprovaledComments_5.Text = dr["Comments"].ToString();
            }
        }
    }
    protected void lnkChangeAccountCode_OnClick(object sender, EventArgs e)
    {
        account.Visible = true;
    }
    protected void btnClosedivChangeAccount_OnClick(object sender, EventArgs e)
    {
        account.Visible = false;
    }
    protected void Temp_OnClick(object sender, EventArgs e)
    {
        account.TaksIDToUpdate = Common.CastAsInt32(hfSelectedTaskID.Value);
        account.ShowTaskList();
    }
    protected void btnRefereshPage_OnClick(object sender, EventArgs e)
    {
        ShowDetails();
    }
    public void SetUserControlData()
    {
        string VesselCode = "";
        string CompCode = "";
        string sql = " SELECT POID FROM DBO.TBLSMDPOMASTERBID WHERE BIDID=" + BidId;
        DataTable dtPoID= Common.Execute_Procedures_Select_ByQuery(sql);
        if (dtPoID.Rows.Count > 0)
        {
            PoID = Common.CastAsInt32(dtPoID.Rows[0][0]);
        }

        sql = "SELECT SHIPID FROM DBO.TBLSMDPOMASTER WHERE POID=" + PoID + "";
        DataTable dtVessleCOde = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dtVessleCOde.Rows.Count > 0)
        {
            VesselCode = Convert.ToString(dtVessleCOde.Rows[0][0]);
        }

        sql = "Select AccontCompany As Company, IsMultiAccount from Vessel with(nolock)  where VesselCode = '" + VesselCode + "'";
        DataTable dtCompCOde = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dtCompCOde.Rows.Count > 0)
        {
            CompCode = Convert.ToString(dtCompCOde.Rows[0][0]);
            IsMultiAccount = Convert.ToBoolean(dtCompCOde.Rows[0]["IsMultiAccount"]);
        }
        account.BidID = BidId;
        account.POId = PoID;
        account.VesselCode = VesselCode;
        account.CompCode = CompCode;
    }
    //---------------------

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

    //protected void btnOrderComments_OnClick(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "OrderComments", " window.open('UpdateOrderStautsComment.aspx?BidID=" + BidId.ToString() + ", '', 'height=250,width=450,resizable=no , toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbar=no,resizable=no,copyhistory=yes,left=450, top=280');", false);
    //    }
    //    catch (Exception ex)
    //    {
    //        ProjectCommon.ShowMessage(ex.Message.ToString());
    //    }
    //}



   
}

