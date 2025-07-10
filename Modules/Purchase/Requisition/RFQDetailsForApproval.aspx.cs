using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class EditSpareRFQ : System.Web.UI.Page
{
    AuthenticationManager authRFQViewEdit;

    public int UserID
    {
        set { ViewState["_UserID"] = value; }
        get { return int.Parse("0" + ViewState["_UserID"]); }
    }
    public int PagePoID
    {
        set { ViewState["PagePoID"] = value; }
        get { return int.Parse("0" + ViewState["PagePoID"]); }
    }

    #region Properties ***************************************************
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
    public int ApprovalLevel
    {
        set { ViewState["_ApprovalLevel"] = value; }
        get { return int.Parse("0" + ViewState["_ApprovalLevel"]); }
    }

    public int AccountID
    {
        set { ViewState["_AccountID"] = value; }
        get { return int.Parse("0" + ViewState["_AccountID"]); }
    }

    public string AccountName
    {
        set { ViewState["_AccountName"] = value; }
        get { return "" + Convert.ToString(ViewState["_AccountName"]); }
    }
    public string ComCode
    {
        set { ViewState["_ComCode"] = value; }
        get { return "" + Convert.ToString(ViewState["_ComCode"]); }
    }

    public string POIssuingCompany
    {
        set { ViewState["_POIssuingCompany"] = value; }
        get { return "" + Convert.ToString(ViewState["_POIssuingCompany"]); }
    }
    public string VesselCode
    {
        set { ViewState["_VesselCode"] = value; }
        get { return "" + Convert.ToString(ViewState["_VesselCode"]); }
    }

    public bool IsMultiAccount
    {
        set { ViewState["_IsMultiAccount"] = value; }
        get { return Convert.ToBoolean(ViewState["_IsMultiAccount"]); }
    }
    #endregion
    protected void Page_PreRender(object sender, EventArgs e)
    {
        imgApp1.Enabled = imgApp1.Enabled & authRFQViewEdit.IsVerify;
        imgApp2.Enabled = imgApp2.Enabled & authRFQViewEdit.IsVerify2;
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dia45log", "RefreshBack();", true);
        //---------------------------------------
        SessionManager.SessionCheck_New();
        //---------------------------------------
        #region --------- USER RIGHTS MANAGEMENT -----------
        try
        {
            authRFQViewEdit = new AuthenticationManager(1061, int.Parse(Session["loginId"].ToString()), ObjectType.Page);
            UserID = int.Parse(Session["loginId"].ToString());
            if (!(authRFQViewEdit.IsView))
            {
                Response.Redirect("~/AuthorityError.aspx");
            }
            
        }
        catch (Exception ex)
        {
            Response.Redirect("~/NoPermission.aspx?Message=" + ex.Message);
        }
        #endregion ----------------------------------------

        lblMsgPoApproval.Text = "";
        lblMSgSendBackToPurchaser.Text = "";
        if (!(IsPostBack))
        {
            //lblmsg.Text = "";
            hdnStatus.Value = "C";
            BidId = int.Parse("0" + Page.Request.QueryString["BidId"]);
            SetUserControlData();
            if (Page.Request.QueryString["UpdateBack"] != null)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dialog", "window.opener.ReloadPage();", true); 
            }
            ShowApprovalRequestByOn();
            ShowMasterData();
            DisplayApprovalLevel();
            Bind_rptApprovalList();
   	        imgResetValues_Click(sender,e);

            //string sql = " Select top 1 BidStatusID from tblSMDPOMasterBid with(nolock) where  BidStatusID >=3 and Poid=" + PoID;
            string sql = "Select count(*) from BidApprovalList where Bidid in (Select BidID from tblSMDPOMasterBid with(nolock) where BidStatusID Not in (-1,-2) and Poid= " + PoID + ") and ApprovedOn is not null";
            DataTable dtPoID = Common.Execute_Procedures_Select_ByQuery(sql);
            if (dtPoID.Rows.Count > 0 && Convert.ToInt32(dtPoID.Rows[0][0]) > 0)
            {
                btnSendBackToPurchaser_1.Visible = false;
            }
            else
            {
                btnSendBackToPurchaser_1.Visible = true;
            }
            GetDocCount(PoID);
        }
    }
    protected void btnSendBackToApproval1_Close_OnClick(object sender, EventArgs e)
    {
        lblSendBackToApproval1.Text = "";
        dvSendBackToApproval1.Visible = false;
    }
    protected void btnMakeQuote_Click(object sender, EventArgs e)
    {
        lblSendBackToApproval1.Text = "";
        dvSendBackToApproval1.Visible = true;

    }
    protected void btnSendBackToApproval1_Save_OnClick(object sender, EventArgs e)
    {
        if(txtSendBackToApprovalMessage.Text.Trim()=="")
        {
            lblSendBackToApproval1.Text = "Please enter comments to continue."; ;
            return;
        }
        try
        {
            Common.Execute_Procedures_Select_ByQuery("EXEC DBO.sp_POSendBackToApproval1 " + BidId.ToString() + "," + UserID + ",'" + txtSendBackToApprovalMessage.Text + "'");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dialog", "alert('Send back to approval-1 stage successfully.');window.close();", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dialog", "alert('Unable to send back to approval-1 stage.');", true);
        }


        //try
        //{
        //    Common.Execute_Procedures_Select_ByQuery("EXEC DBO.SendBackToPO " + BidId.ToString());
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dialog", "alert('Send back to quote stage successfully.');", true);
        //}
        //catch (Exception ex)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dialog", "alert('Unable to send back to quote stage.');", true);
        //}

    }
    public void ShowMasterData()
    {
        DataTable dtRFQ = Common.Execute_Procedures_Select_ByQuery("EXEC DBO.sp_NewPR_getRFQMasterByBidId " + BidId.ToString());
        if (dtRFQ != null)
        {
            if (dtRFQ.Rows.Count > 0)
            {
                //--Show Change Account Code Button----
                DataTable dtTravExport = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.TBLAPENTRIES WHERE BIDID IN ( SELECT BIDID FROM DBO.tblSMDpoMasterBid M WHERE M.poid IN  ( SELECT POID FROM DBO.tblSMDpoMasterBid WHERE BIDID=" + BidId.ToString() + " ) ) AND INTRAV = 1");
                if (dtTravExport.Rows.Count <= 0)
                {
                    lnkChangeAccountCode.Visible = true;
                }



                // MASTER INFORMATION
                DataRow dr = dtRFQ.Rows[0];

                VesselCode = dr["ShipID"].ToString();
                string sql = " Select AccontCompany As Company,IsMultiAccount, POIssuingCompanyId from Vessel with(nolock)  where VesselCode = '" + VesselCode + "' ";
                DataTable DTVessel = Common.Execute_Procedures_Select_ByQuery(sql);
                if (DTVessel.Rows.Count > 0)
                {
                    ComCode = DTVessel.Rows[0]["Company"].ToString();
                    IsMultiAccount = Convert.ToBoolean(DTVessel.Rows[0]["IsMultiAccount"]);
                    POIssuingCompany = DTVessel.Rows[0]["POIssuingCompanyId"].ToString();
                }
                BindAccountCompany();
                if (IsMultiAccount)
                {
                    ddlPOAccountCompany.Visible = true;
                    if (! string.IsNullOrEmpty(dr["POAccountCompany"].ToString()))
                    {
                        ddlPOAccountCompany.SelectedValue = dr["POAccountCompany"].ToString();
                    }
                    else
                    {
                        ddlPOAccountCompany.SelectedIndex = 0;
                    }
                }
                else
                {
                    ddlPOAccountCompany.Visible = false;
                    ddlPOAccountCompany.SelectedIndex = 0;
                }
                    
                ComCode = GetCompanyName();
                AccountID = Common.CastAsInt32(dr["AccountID"].ToString());
                AccountName = dr["ACCOUNTNUMBER"].ToString() + " - " + dr["ACCOUNTNAME"].ToString();

                PoID = Common.CastAsInt32(dr["PoID"].ToString());
                PagePoID = Common.CastAsInt32(dr["PoID"].ToString());
                LoadBreakDown();
                lblRFQNO.Text = dr["RFQNO"].ToString();
                lblReqNo.Text = dr["REQNO"].ToString();

                lblrfqno1.Text = dr["REQNO"].ToString();

                ViewState.Add("PRTYPE", dr["PRTYPE"].ToString());
                BindAccount(dr["dept"].ToString());
                ddlAccCode.SelectedValue = dr["AccountID"].ToString();

                lblReqType.Text = dr["PRTYPENAME"].ToString();
                lblCreatedBy.Text = dr["POCREATOR"].ToString();
                lblDateCreated.Text = dr["DATECREATED"].ToString();
                lblAccountName.Text = dr["ACCOUNTNUMBER"].ToString() + " - " + dr["ACCOUNTNAME"].ToString();
                //lblAccountName.ToolTip = dr["ACCOUNTNAME"].ToString();

                //lblSmdComments.Text = dr["REMARKSSMD"].ToString();
                //lblVenComments.Text = dr["BIDPOCOMMENTSVEN"].ToString();
                ViewState.Add("vw_bidExchRate", dr["bidExchRate"].ToString());
                lblPortNDate.Text = dr["Port"].ToString();
                if (dr["Podate"].ToString() != "")
                {
                    lblPortNDate.Text = lblPortNDate.Text + "<b> /</b> " + Convert.ToDateTime(dr["Podate"].ToString()).ToString("dd-MMM-yyyy");
                }

                txtLC.Text = Math.Round(Common.CastAsDecimal(dr["estShippingFor"].ToString())).ToString();
                lblUSD.Text = Math.Round(Common.CastAsDecimal(dr["estshippingUSD"]), 2).ToString();
                txtDiscountPer.Text = Math.Round(Common.CastAsDecimal(dr["DisCountPercentage"]), 2).ToString();
                lblTotalDiscountUSD.Text = Math.Round(Common.CastAsDecimal(dr["TotalDiscountUSD"]), 2).ToString();
                lblGSTLC.Text = Math.Round(Common.CastAsDecimal(dr["TotalGSTTaxAmount"]), 2).ToString();
                lblGSTUsd.Text = Math.Round(Common.CastAsDecimal(dr["TotalGSTTaxAmountUSD"]), 2).ToString();
                lblVenderName.Text = dr["SupplierName"].ToString();
                string contact = dr["BIDVENCONTACT"].ToString();
                contact = contact + ((contact != "") ? "<br/>" : "") + dr["BIDVENCONTACT"].ToString();
                contact = contact + ((contact != "") ? "<br/>" : "") + dr["BIDVENPHONE"].ToString();
                contact = contact + ((contact != "") ? "<br/>" : "") + dr["BIDVENEMAIL"].ToString();

                lblVenContact.Text = contact;

                lblLocalCurr.Text = dr["BidCurr"].ToString();
                lblLC.Text = dr["BidCurr"].ToString();
                lblCurrRate.Text = dr["BidExchRate"].ToString();
                if (dr["BidExchDate"].ToString() != "")
                {
                    lblBidExchDate.Text = Convert.ToDateTime(dr["BidExchDate"].ToString()).ToString("dd-MMM-yyyy");
                }

                lblPoDesc.Text = dr["BiddeliveryInstructions"].ToString();
                //------------------------------------------------------
                //Equpipment Information
                lblEquipName.Text = dr["EquipName"].ToString();
                lblModelType.Text = dr["EquipModel"].ToString();
                lblSerial.Text = dr["EquipSerialNo"].ToString();
                lblBuiltYear.Text = dr["EquipYear"].ToString();
                lblNameAddress.Text = dr["Equipmfg"].ToString();
                //------------------------------------------------------

                DisplayApprovalLevel();
                //Set Button vesibility
                if (dr["ISPO"].ToString() == "True")
                {
                    
                    //tblChangeAccCod.Visible = false;
                    imgPlaceOrder.Enabled = false;
                    btnSendBackToPurchaserPopup.Visible = false;
                    imgResetValues.Visible = false;
                    btnSendMail.Visible = true;
                }
                else
                {
                    int BidStatusId = Common.CastAsInt32(dr["BIDSTATUSID"].ToString());
                    string BidPoNum = dr["BidPoNum"].ToString();
                    if(BidStatusId<=2 && BidPoNum.Trim()=="")
                    {
                        btnMakeQuote.Visible = true;
                    }
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

                // Set Row Number
                SetRowNumber();

                // Get the total USD
                CountTotalUSD();
                //Approval Details 
                //DataTable DtApproval;
                //                    DtApproval = Common.Execute_Procedures_Select_ByQuery("SELECT BidSMDLevel1Approval,BidSMDLevel1ApprovalDate,ApproveComments FROM VW_tblSMDPOMasterBid where BidID =" + BidId + "");
                DataTable dtApprovalDetails = Common.Execute_Procedures_Select_ByQuery("select ApprovalPhase,Approved From BidApprovalList Where BidId=" + BidId.ToString());
                int App_BidStatus = 0;

                if (dtApprovalDetails.Rows.Count > 0)
                {
                    if (dtApprovalDetails.Rows[0]["Approved"].ToString() == "True")
                    {
                        App_BidStatus = 3;
                    }
                    else
                    {
                        App_BidStatus = Common.CastAsInt32(dtApprovalDetails.Rows[0]["ApprovalPhase"]);
                    }
                }
                ViewState.Add("PRStatusID", App_BidStatus);

                if (App_BidStatus == 1)
                {
                    tblApp1.Visible = false;
                    tblApp2.Visible = false;
                }
                else if (App_BidStatus == 2)
                {
                    lblApp1Name.Text = dr["BidSMDLevel1Approval"].ToString();
                    if (dr["BidSMDLevel1ApprovalDate"].ToString() != "")
                    {
                        lblApp1On.Text = Convert.ToDateTime(dr["BidSMDLevel1ApprovalDate"].ToString()).ToString("dd-MMM-yyyy");
                    }
                   // txtVenderComm.Text = dr["ApproveComments"].ToString();
                    txtQuarComm.Text = dr["comment"].ToString();
                }
                else
                {
                    imgApp1.Enabled = false;
                    imgApp2.Enabled = false;
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
                    //txtVenderComm.Text = dr["ApproveComments"].ToString();
                    txtQuarComm.Text = dr["comment"].ToString();
                }
                //Get Budget Details
                ViewState.Add("ShipID", dr["ShipID"].ToString());
                ShowBudgetDetails(dr["ShipID"].ToString(), ddlAccCode.SelectedValue);
            }
        }
    }

    protected void BindAccountCompany()
    {
        DataTable dt6 = Budget.getTable("EXEC GetAccountCompanyHeaderforVessel '"+POIssuingCompany+"' ").Tables[0];
        this.ddlPOAccountCompany.DataValueField = "Company";
        this.ddlPOAccountCompany.DataTextField = "CompanyName";
        this.ddlPOAccountCompany.DataSource = dt6;
        this.ddlPOAccountCompany.DataBind();
        ddlPOAccountCompany.Items.Insert(0, new ListItem("< Select >", "0"));
    }
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
            lblmsg.Text = "Bid Status has been updated successfully  ";
        }
        else
        {
            lblmsg.Text = "Bid Status can't updated ";
        }
    }
    protected void btnApp_OnClick(object sender, EventArgs e)
    {
        imgResetValues_Click(sender,e);
        if (txtQuarComm.Text.Trim() == "")
        {
            txtQuarComm.Focus();
            lblmsg.Text = "Please enter quarterly comments.";
            return;
        }
        
        string OrderQtyForUpdate = "";
        string BidItemID = "";
        Decimal totalLc = 0;
        Decimal totalLCUSD = 0;
        Decimal DiscountPer = 0;
        Decimal totalDiscount = 0;
        Decimal totalDiscountUSD = 0;
        Decimal totalGSTLC = 0;
        Decimal totalGSTUSD = 0;
        foreach (RepeaterItem RptItm in rptItems.Items)
        {
            OrderQtyForUpdate = OrderQtyForUpdate + "," + ((TextBox)RptItm.FindControl("txtOrderQTY")).Text;
            HiddenField hfItemID = (HiddenField)RptItm.FindControl("hfItemID");
            BidItemID = BidItemID + ',' + hfItemID.Value;
            Label lblLC = (Label)RptItm.FindControl("lblLC");
            Label lblUsd = (Label)RptItm.FindControl("lblUsd");
            Label lblGSTLC = (Label)RptItm.FindControl("lblGSTLC");
            Label lblGSTUSD = (Label)RptItm.FindControl("lblGSTUsd");
            totalGSTLC = Math.Round(Common.CastAsDecimal(totalGSTLC) + Common.CastAsDecimal(lblGSTLC.Text), 2);
            totalGSTUSD = Math.Round(Common.CastAsDecimal(totalGSTUSD) + Common.CastAsDecimal(lblGSTUSD.Text), 2);
            totalLCUSD = Math.Round(Common.CastAsDecimal(totalLCUSD) + Common.CastAsDecimal(lblUsd.Text), 2);
            totalLc = Math.Round(Common.CastAsDecimal(totalLc) + Common.CastAsDecimal(lblLC.Text), 2);
        }
        if (! String.IsNullOrEmpty(txtDiscountPer.Text) && Common.CastAsDecimal(txtDiscountPer.Text) > 0)
        {
            DiscountPer = Math.Round(Common.CastAsDecimal(txtDiscountPer.Text), 2);
            if (DiscountPer > 0)
            {
                totalDiscount = Math.Round(((Common.CastAsDecimal(totalLc+ totalGSTLC) * Common.CastAsDecimal(DiscountPer))/100),2);
                totalDiscountUSD = Math.Round(((Common.CastAsDecimal(totalLCUSD+ totalGSTUSD) * Common.CastAsDecimal(DiscountPer)) / 100), 2);
            }
           
        }

        OrderQtyForUpdate = OrderQtyForUpdate.Substring(1);
        BidItemID = BidItemID.Substring(1);
        int AppMode=Common.CastAsInt32(ViewState["PRStatusID"]);
        //
        Common.Set_Procedures("sp_NewPR_ApproveOrder");
                    Common.Set_ParameterLength(17);
                    Common.Set_Parameters(
                        new MyParameter("@BidID", BidId),
                        new MyParameter("@AppBy", Session["UserFullName"].ToString()),
                        new MyParameter("@AppMode", AppMode),//?????????
                        new MyParameter("@QuarterComments", txtQuarComm.Text.Trim()),
                        new MyParameter("@ApproveComments ", ""),
                        new MyParameter("@ExchRate", lblCurrRate.Text.Trim()),
                        new MyParameter("@LC", txtLC.Text.Trim()),
                        new MyParameter("@USD", lblUSD.Text),
                        new MyParameter("@OrderQty", OrderQtyForUpdate),
                        new MyParameter("@BidItemID", BidItemID),
                        new MyParameter("@TotalUsd", lblTotalUSdD.Text),
                        new MyParameter("@DiscountPer", DiscountPer),
                        new MyParameter("@TotalDiscount", totalDiscount),
                        new MyParameter("@TotalDiscountUSD", totalDiscountUSD),
                        new MyParameter("@PoAccountCompany", ddlPOAccountCompany.SelectedIndex != 0 ? ddlPOAccountCompany.SelectedValue : ""),
                    new MyParameter("@TotalGSTLC", totalGSTLC),
                    new MyParameter("@TotalGSTUSD", totalGSTUSD)
                        );
                    Boolean res;
            DataSet dsRes=new DataSet();
            res = Common.Execute_Procedures_IUD(dsRes);
            if (res == true)
            {
                
                if (AppMode == 3) // order placed   ????????????
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "abc", "window.open('SendMail.aspx?Mailtype=2&Param=" + BidId.ToString() + "');", true);
                }
                lblmsg.Text = "Record saved successfully.";
                
                switch (AppMode)
                {
                    case 1:
                            Response.Redirect("RFQDetailsForApproval.aspx?BidId="+BidId+"&UpdateBack=1");
                            return;
                    case 2:
                            Response.Redirect("RFQDetailsForApproval.aspx?BidId=" + BidId + "&UpdateBack=1");
                            return;
                    case 3:
                            imgPlaceOrder.Visible= false;
                            btnSendMail.Visible = true;
                            return;
                }
            }
            else
            {
                lblmsg.Text = "Unable to save record. Error :" + Common.ErrMsg;
            }

         // cmdNotifyVendorPO -
        // SEND A PDF TO VENDOR THAT ORDER APPROVED & PO SEND
        // NOTIFY TO VESSEL ALSO
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
        txtDiscountPer.Text = Math.Abs(Common.CastAsDecimal(txtDiscountPer.Text)).ToString();
        if (!String.IsNullOrEmpty(txtDiscountPer.Text) && Convert.ToDecimal(txtDiscountPer.Text) > 0)
        {
            if (ViewState["vTotalUSDs"] != null && ViewState["vTotalUSDs"].ToString() != "0")
            {
                Decimal TotalDiscountUsd = 0;
                
                TotalDiscountUsd = Math.Round(Common.CastAsDecimal(Common.CastAsDecimal(ViewState["vTotalUSDs"].ToString()) * Common.CastAsDecimal(txtDiscountPer.Text.ToString())) / 100, 2);
                lblTotalDiscountUSD.Text = Math.Round(TotalDiscountUsd,2).ToString();
                lblTotalUSdD.Text = Math.Round(Common.CastAsDecimal(Common.CastAsDecimal(ViewState["vTotalUSDs"].ToString()) + Common.CastAsDecimal(lblUSD.Text)) - Common.CastAsDecimal(lblTotalDiscountUSD.Text), 2).ToString();
            }
            else
            {
                Decimal TotalDiscountUsd = 0;
                TotalDiscountUsd = Math.Round(Common.CastAsDecimal(Common.CastAsDecimal(ViewState["vTotalUSDs"].ToString()) * Common.CastAsDecimal(txtDiscountPer.Text.ToString())) / 100, 2);
                lblTotalDiscountUSD.Text = Math.Round(TotalDiscountUsd,2).ToString();
                lblTotalUSdD.Text = Math.Round(Common.CastAsDecimal(Common.CastAsDecimal(ViewState["vTotalUSDs"].ToString()) - Common.CastAsDecimal(lblTotalDiscountUSD.Text)), 2).ToString();
            }

            if (ViewState["vTotalLCs"] != null && ViewState["vTotalLCs"].ToString() != "0")
            {
                Decimal TotalDiscountLC = 0;

                TotalDiscountLC = Math.Round(Common.CastAsDecimal(Common.CastAsDecimal(ViewState["vTotalLCs"].ToString()) * Common.CastAsDecimal(txtDiscountPer.Text.ToString())) / 100, 2);

                lblTotalLCD.Text = Math.Round(Common.CastAsDecimal(Common.CastAsDecimal(ViewState["vTotalLCs"].ToString()) + Common.CastAsDecimal(txtLC.Text)) - Common.CastAsDecimal(TotalDiscountLC), 2).ToString();
            }
            else
            {
                Decimal TotalDiscountLC = 0;
                TotalDiscountLC = Math.Round(Common.CastAsDecimal(Common.CastAsDecimal(ViewState["vTotalLCs"].ToString()) * Common.CastAsDecimal(txtDiscountPer.Text.ToString())) / 100, 2);
                // lblTotalDiscountUSD.Text = Math.Round(TotalDiscountUsd, 2).ToString();
                lblTotalLCD.Text = Math.Round(Common.CastAsDecimal(Common.CastAsDecimal(ViewState["vTotalLCs"].ToString())) - Common.CastAsDecimal(TotalDiscountLC), 2).ToString();
            }



        }
        else
        {
            Decimal TotalDiscountUsd = 0;
            //Decimal TotalDiscountLC = 0;
            lblTotalDiscountUSD.Text = Math.Round(TotalDiscountUsd, 2).ToString();
            if (ViewState["vTotalUSDs"] != null && ViewState["vTotalUSDs"].ToString() != "0")
            {
                lblTotalUSdD.Text = Math.Round(Common.CastAsDecimal(Common.CastAsDecimal(ViewState["vTotalUSDs"].ToString()) + Common.CastAsDecimal(lblUSD.Text)), 2).ToString();
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
    protected void txtDiscountPer_OnTextChanged(object sender, EventArgs e)
    {
        txtDiscountPer.Text = Math.Abs(Common.CastAsDecimal(txtDiscountPer.Text)).ToString();
       
        if (!String.IsNullOrEmpty(txtDiscountPer.Text) && Convert.ToDecimal(txtDiscountPer.Text) > 0)
        {
            if (ViewState["vTotalUSDs"] != null && ViewState["vTotalUSDs"].ToString() != "0")
            {
                Decimal TotalDiscountUsd = 0;
                TotalDiscountUsd = Math.Round(Common.CastAsDecimal(Common.CastAsDecimal(ViewState["vTotalUSDs"].ToString()) * Common.CastAsDecimal(txtDiscountPer.Text.ToString())) / 100, 2);
                    //if (ViewState["vw_bidExchRate"] != null && ViewState["vw_bidExchRate"].ToString() != "0")
                    //{
                    //    TotalDiscountUsd = Math.Round(Common.CastAsDecimal(TotalDiscountUsd) / Common.CastAsDecimal(ViewState["vw_bidExchRate"].ToString()), 2);
                    //}
                lblTotalDiscountUSD.Text = Math.Round(TotalDiscountUsd,2).ToString();
                lblTotalUSdD.Text = Math.Round(Common.CastAsDecimal(Common.CastAsDecimal(ViewState["vTotalUSDs"].ToString()) + Common.CastAsDecimal(lblUSD.Text)) - Common.CastAsDecimal(lblTotalDiscountUSD.Text), 2).ToString();
            }
            else
            {
                Decimal TotalDiscountUsd = 0;
                TotalDiscountUsd = Math.Round(Common.CastAsDecimal(Common.CastAsDecimal(ViewState["vTotalUSDs"].ToString()) * Common.CastAsDecimal(txtDiscountPer.Text.ToString())) / 100, 2);
                //if (ViewState["vw_bidExchRate"] != null && ViewState["vw_bidExchRate"].ToString() != "0")
                //{
                //    TotalDiscountUsd = Math.Round(Common.CastAsDecimal(TotalDiscountUsd) / Common.CastAsDecimal(ViewState["vw_bidExchRate"].ToString()), 2);
                //}
                lblTotalDiscountUSD.Text = Math.Round(TotalDiscountUsd,2).ToString();
                lblTotalUSdD.Text = Math.Round(Common.CastAsDecimal(Common.CastAsDecimal(ViewState["vTotalUSDs"].ToString()) - Common.CastAsDecimal(lblTotalDiscountUSD.Text)), 2).ToString();
            }

            if (ViewState["vTotalLCs"] != null && ViewState["vTotalLCs"].ToString() != "0")
            {
                Decimal TotalDiscountLC = 0;
                TotalDiscountLC = Math.Round(Common.CastAsDecimal(Common.CastAsDecimal(ViewState["vTotalLCs"].ToString()) * Common.CastAsDecimal(txtDiscountPer.Text.ToString())) / 100, 2);
              
              //  lblTotalDiscountUSD.Text = Math.Round(TotalDiscountUsd, 2).ToString();
                lblTotalLCD.Text = Math.Round(Common.CastAsDecimal(Common.CastAsDecimal(ViewState["vTotalLCs"].ToString()) + Common.CastAsDecimal(txtLC.Text)) - Common.CastAsDecimal(TotalDiscountLC), 2).ToString();
            }
            else
            {
                Decimal TotalDiscountLC = 0;
                TotalDiscountLC = Math.Round(Common.CastAsDecimal(Common.CastAsDecimal(ViewState["vTotalLCs"].ToString()) * Common.CastAsDecimal(txtDiscountPer.Text.ToString())) / 100, 2);

                lblTotalLCD.Text = Math.Round(Common.CastAsDecimal(Common.CastAsDecimal(ViewState["vTotalLCs"].ToString()) - Common.CastAsDecimal(TotalDiscountLC)), 2).ToString();
            }
        }
        else
        {
            Decimal TotalDiscountUsd = 0;
            lblTotalDiscountUSD.Text = Math.Round(TotalDiscountUsd, 2).ToString();
            if (ViewState["vTotalUSDs"] != null && ViewState["vTotalUSDs"].ToString() != "0")
            {
                lblTotalUSdD.Text = Math.Round(Common.CastAsDecimal(Common.CastAsDecimal(ViewState["vTotalUSDs"].ToString()) + Common.CastAsDecimal(lblUSD.Text)), 2).ToString();
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
    protected void imgResetValues_Click(object sender, EventArgs e)
    {
        decimal TotalUSD = 0;
        foreach (RepeaterItem RptItm in rptItems.Items)
        {

            TextBox txtOrderQty = (TextBox)RptItm.FindControl("txtOrderQty");
            //decimal OrderQty = Common.CastAsDecimal(((TextBox)sender).Text);
            decimal OrderQty = Common.CastAsDecimal(txtOrderQty.Text);

            Label lblUnitPrice = (Label)txtOrderQty.Parent.FindControl("lblUnitPrice");
            decimal UnitPrice = Common.CastAsDecimal(lblUnitPrice.Text);

            Label LC = (Label)txtOrderQty.FindControl("lblLC");
            Label USD = (Label)txtOrderQty.FindControl("lblUsd");
            LC.Text = Math.Round(Common.CastAsDecimal(OrderQty * UnitPrice), 2).ToString();
            if (ViewState["vw_bidExchRate"] != null && ViewState["vw_bidExchRate"].ToString() != "0")
            {
                USD.Text = Math.Round(Common.CastAsDecimal(Common.CastAsDecimal(LC.Text) / Common.CastAsDecimal(ViewState["vw_bidExchRate"].ToString())), 2).ToString();
            }
            else
            {
                USD.Text = "0";
            }
        }
        CountTotalUSD();
    }
    protected void LoadBreakDown()
    {
        DataTable dtApprovalDetails = Common.Execute_Procedures_Select_ByQuery("select Breakdown From Add_tblSMDPOMasterBid1 Where BidId=" + BidId.ToString());
        ChkBreakdown.Checked = false;
        if (dtApprovalDetails.Rows.Count > 0)
        {
            ChkBreakdown.Checked = (Convert.ToBoolean(dtApprovalDetails.Rows[0][0]));
            lblBreakdown.Text = (Convert.ToBoolean(dtApprovalDetails.Rows[0][0])) ? "BreakDown" : "Budgeted Task";
   	    lblBreakdown.ForeColor = (Convert.ToBoolean(dtApprovalDetails.Rows[0][0])) ? System.Drawing.Color.Red : System.Drawing.Color.Green;
        }
    }
    protected void ChkBreakdown_OnCheckedChanged(object sender, EventArgs e)
    {
        Common.Set_Procedures("sp_NewPR_IU_Add_tblSMDPOMasterBid1");
                    Common.Set_ParameterLength(2);
                    Common.Set_Parameters(
                        new MyParameter("@BidID", BidId.ToString()),
                        new MyParameter("@Breakdown", (ChkBreakdown.Checked)?"1":"0")
                        );
        DataSet dsRes = new DataSet();
        Boolean res;
        res = Common.Execute_Procedures_IUD(dsRes);
    }
    
    #endregion

    #region Function *****************************************************

    public void ShowBudgetDetails(string  ShipID,string AcctID)
    {
        //DataTable dtBudget = ProjectCommon.getBudgetDetails(ShipID, AcctID, DateTime.Today);
        DataTable dtBudget = Common.Execute_Procedures_Select_ByQuery("exec DBO.[getVarianceReport_ByAccount_VSL] " + DateTime.Today.Month.ToString() + "," + (DateTime.Today.Year).ToString() + ",'" + ShipID + "'," + lblAccountName.Text.Substring(0,4));
        string sql = "SELECT DBO.getAnnualBudget('" + ShipID + "'," + AcctID + "," + DateTime.Today.Year.ToString()+ ")";
        DataTable DtAnn = Common.Execute_Procedures_Select_ByQuery(sql);
       
        //lblAnnualBudget.Text = Math.Round(Common.CastAsDecimal(dtBudget.Rows[0][0].ToString()), 2).ToString();
        if (DtAnn.Rows.Count > 0)
        {
            lblAnnualBudget.Text = Math.Round(Common.CastAsDecimal(DtAnn.Rows[0][0].ToString()), 2).ToString();
        }
        if (dtBudget.Rows.Count > 0)
        {
            lblConsumedDate.Text = Math.Round(Common.CastAsDecimal(dtBudget.Rows[0]["AcctYTDCons"].ToString()), 2).ToString();
        }
        lblBudgetRem.Text = Math.Round(Common.CastAsDecimal(lblAnnualBudget.Text) - Common.CastAsDecimal(lblConsumedDate.Text), 2).ToString();
        if( Common.CastAsDecimal(lblAnnualBudget.Text)==0)
        {
            lblUtilization.Text ="100.00 %";
        }
        else
        {
            lblUtilization.Text = Math.Round((Common.CastAsDecimal(lblConsumedDate.Text)*100) / Common.CastAsDecimal(lblAnnualBudget.Text), 2).ToString() + " %"; //dtBudget.Rows[0][3].ToString();
        }
        if (Common.CastAsDecimal(lblBudgetRem.Text) < 0)
        {
            lblBudgetRem.ForeColor = System.Drawing.Color.Red;
            //lblBudgetRem.Text = lblBudgetRem.Text.Replace("-",""); 
        }
    }
    public void UpdateTotalUSD()
    {
        decimal TotalUSD = 0;
        foreach (RepeaterItem RptItm in rptItems.Items)
        {
            TotalUSD =TotalUSD + Common.CastAsDecimal( ((Label)RptItm.FindControl("lblUsd")).Text);
        }
        lblTotalDiscountUSD.Text = Math.Round(Common.CastAsDecimal((TotalUSD * Common.CastAsDecimal(txtDiscountPer.Text.Trim())) / 100), 2).ToString();
        lblTotalUSdD.Text = Convert.ToString( TotalUSD + Common.CastAsDecimal(lblUSD.Text) - Common.CastAsDecimal(lblTotalDiscountUSD.Text));
    }
    public void CountTotalUSD()
    {
        //Count total USD
        decimal TotalUSD = 0;
        decimal TotalLC = 0;
        decimal totalLCDiscount = 0;
        decimal totalGSTLC = 0;
        decimal totalGSTUsd = 0;
        foreach (RepeaterItem TptItem in rptItems.Items)
        {
            TotalUSD = TotalUSD + Common.CastAsDecimal(((Label)TptItem.FindControl("lblUsd")).Text);
            TotalLC = TotalLC + Common.CastAsDecimal(((Label)TptItem.FindControl("lblLC")).Text);
            totalGSTLC = totalGSTLC + Common.CastAsDecimal(((Label)TptItem.FindControl("lblGSTLC")).Text);
            totalGSTUsd = totalGSTUsd + Common.CastAsDecimal(((Label)TptItem.FindControl("lblGSTUsd")).Text);
        }
        ViewState["vTotalUSDs"] = (TotalUSD+totalGSTUsd).ToString();
        ViewState["vTotalLCs"] = (TotalLC+totalGSTLC).ToString();
        ViewState["vTotalGSTLCs"] = totalGSTLC.ToString();
        ViewState["vTotalGSTUSDs"] = totalGSTUsd.ToString();
        lblTotalDiscountUSD.Text = Math.Round(Common.CastAsDecimal(((TotalUSD+totalGSTUsd) * Common.CastAsDecimal(txtDiscountPer.Text.Trim())) / 100), 2).ToString();
        //ViewState["vTotalDiscountUSDs"] = Math.Round(Common.CastAsDecimal((TotalUSD + Common.CastAsDecimal(lblUSD.Text))) * , 2)
        lblTotalUSdD.Text = Math.Round(Common.CastAsDecimal(TotalUSD + totalGSTUsd  + Common.CastAsDecimal(lblUSD.Text) - Common.CastAsDecimal(lblTotalDiscountUSD.Text)), 2).ToString();
        totalLCDiscount = Math.Round(Common.CastAsDecimal(((TotalLC + totalGSTLC) * Common.CastAsDecimal(txtDiscountPer.Text.Trim())) / 100), 2);
        lblTotalLCD.Text = Math.Round(Common.CastAsDecimal(TotalLC + totalGSTLC  + Common.CastAsDecimal(txtLC.Text) - Common.CastAsDecimal(totalLCDiscount)), 2).ToString();
        lblGSTLC.Text = Math.Round(Common.CastAsDecimal(totalGSTLC),2).ToString();
        lblGSTUsd.Text = Math.Round(Common.CastAsDecimal(totalGSTUsd), 2).ToString();
    }
    public void SetQtyReadable()
    {
        foreach (RepeaterItem RptItm in rptItems.Items)
        {
           TextBox txtOrderQTY=(TextBox)RptItm.FindControl("txtOrderQTY");
           Label lblOrderQTY = (Label)RptItm.FindControl("lblOrderQTY");
           lblOrderQTY.Visible = true;
           txtOrderQTY.Visible = false;
        }
    }
    public void BindAccount(string dept)
    {
        //string sql = "select * from (select (select convert(varchar, AccountNumber)+'-'+AccountName from VW_sql_tblSMDPRAccounts PA where DA.AccountID=PA.AccountID and  AccountNumber not like '17%' and AccountNumber !=8590) AccountNumber  ,(select AccountName from  VW_sql_tblSMDPRAccounts PA where DA.AccountID=PA.AccountID and   AccountNumber not like '17%' and AccountNumber !=8590) AccountName  ,AccountID from tblSMDDeptAccounts DA where dept='" + dept + "' and prtype=2) dd where AccountNumber is not null";
        //string sql = "select * from (select (select convert(varchar, AccountNumber)+'-'+AccountName from VW_sql_tblSMDPRAccounts PA where DA.AccountID=PA.AccountID and  AccountNumber not like '17%' and AccountNumber !=8590) AccountNumber  ,(select AccountName from  VW_sql_tblSMDPRAccounts PA where DA.AccountID=PA.AccountID and   AccountNumber not like '17%' and AccountNumber !=8590) AccountName  ,AccountID from tblSMDDeptAccounts DA where dept='" + dept + "' and prtype="+ViewState["PRTYPE"].ToString()+") dd where AccountNumber is not null";
        string sql = "select AccountID,convert(varchar, AccountNumber)+'-'+AccountName as AccountNumber from vw_sql_tblSMDPRAccounts order by AccountNumber";
        
        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Query", sql));
        DataSet dsPrType = new DataSet();
        dsPrType.Clear();
        dsPrType = Common.Execute_Procedures_Select();

        ddlAccCode.DataSource = dsPrType;
        ddlAccCode.DataTextField = "AccountNumber";
        ddlAccCode.DataValueField = "AccountID";
        ddlAccCode.DataBind();
        ddlAccCode.SelectedIndex = 0;
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
    protected void btnSaveAccCode_OnClick(object sender, EventArgs e)
    {
        string sql = "EXEC dbo.Change_AccountCode_ByBidId "+ BidId.ToString() + "," + ddlAccCode.SelectedValue;

        DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt != null)
        {
            lblAccountName.Text = ddlAccCode.SelectedItem.Text;
            ShowBudgetDetails(ViewState["ShipID"].ToString(), ddlAccCode.SelectedValue);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ff", "alert('Updated Successfully.');", true);
AccountName=lblAccountName.Text;
		AccountID=Common.CastAsInt32(ddlAccCode.SelectedValue);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ff", "alert('Could not updated.');", true);
        }
    }
    #endregion


    //---------------------------------------------------------------------------------------
    
    protected void DisplayApprovalLevel()
    {
	
        string sql = " select top 1 ApprovalPhase from dbo.BidApprovalList where BidId=" + BidId + " and ApprovedOn is null order by ApprovalPhase ";
        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt1.Rows.Count > 0)
        {
            ApprovalLevel = Common.CastAsInt32(dt1.Rows[0][0]);
            if (ApprovalLevel < 5)
            {
                lnlApprovePoRequest_Popup.Visible = true;
                imgPlaceOrder.Enabled = false;
                lblApprovalPhase.Text = "Approval Phase " + ApprovalLevel.ToString();
            }
            else
            {
                lnlApprovePoRequest_Popup.Visible = false;
                imgPlaceOrder.Enabled = true;
                lblApprovalPhase.Text = "  Place Order";
            }
        }
        else
        {
            lnlApprovePoRequest_Popup.Visible = false;
            imgPlaceOrder.Enabled = false;
        }
    }
    protected void Bind_rptApprovalList()
    {
        string sql = " select FirstName+' ' +LastName as Name,BidId,'Approval '+ convert(varchar,ApprovalPhase)as ApprovalPhase,ApprovalPhase as ApprovalPhaseID,ApprovedOn,Comments  " +
                     "   from BidApprovalList bal  " +
                     "   left  " +
                     "   join dbo.usermaster um on um.LoginId = bal.LoginId where BidId="+BidId+ " order by ApprovalPhase ";
        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(sql);
        rptApprovalList.DataSource = dt1;
        rptApprovalList.DataBind();


	bool Approval1Done=true;
	bool Approval2Done=true;
	bool Approval3Done=true;
	bool Approval4Done=true;

	bool PlaceOrderDone=true;

        foreach (DataRow dr in dt1.Rows)
        {
            if (dr["ApprovalPhaseID"].ToString() == "1")
            {
		Approval1Done=!Convert.IsDBNull(dr["ApprovedOn"]);
                lblApprovalName_1.Text = dr["Name"].ToString();
                lblApprovaledOn_1.Text = Common.ToDateString(dr["ApprovedOn"].ToString());
                lblApprovaledComments_1.Text = dr["Comments"].ToString();
                btnApprovePO_1.Enabled = (!Approval1Done && IsUserbelogsToApprovalPhase(1));

            }
            else if (dr["ApprovalPhaseID"].ToString() == "2")
            {
		Approval2Done=!Convert.IsDBNull(dr["ApprovedOn"]);
                lblApprovalName_2.Text = dr["Name"].ToString();
                lblApprovaledOn_2.Text = Common.ToDateString(dr["ApprovedOn"].ToString());
                lblApprovaledComments_2.Text = dr["Comments"].ToString();

                btnApprovePO_2.Enabled=(Approval1Done && !Approval2Done && IsUserbelogsToApprovalPhase(2));

            }
            else if (dr["ApprovalPhaseID"].ToString() == "3")
            {
		Approval3Done=!Convert.IsDBNull(dr["ApprovedOn"]);
                lblApprovalName_3.Text = dr["Name"].ToString();
                lblApprovaledOn_3.Text = Common.ToDateString(dr["ApprovedOn"].ToString());
                lblApprovaledComments_3.Text = dr["Comments"].ToString();
                btnApprovePO_3.Enabled=(Approval2Done && !Approval3Done && IsUserbelogsToApprovalPhase(3));

            }
            else if (dr["ApprovalPhaseID"].ToString() == "4")
            {
		Approval4Done=!Convert.IsDBNull(dr["ApprovedOn"]);
                lblApprovalName_4.Text = dr["Name"].ToString();
                lblApprovaledOn_4.Text = Common.ToDateString(dr["ApprovedOn"].ToString());
                lblApprovaledComments_4.Text = dr["Comments"].ToString();
                btnApprovePO_4.Enabled=(Approval3Done && !Approval4Done && IsUserbelogsToApprovalPhase(4));
            }
            else if (dr["ApprovalPhaseID"].ToString() == "5")
            {
		PlaceOrderDone=!Convert.IsDBNull(dr["ApprovedOn"]);

                lblApprovalName_5.Text = dr["Name"].ToString();
                lblApprovaledOn_5.Text = Common.ToDateString(dr["ApprovedOn"].ToString());
                lblApprovaledComments_5.Text = dr["Comments"].ToString();

            }
        }
    }
    public bool IsUserbelogsToApprovalPhase(int Phase)
    {
        bool Ret = false;
        string sql = "";
        if (Phase == 1)
            sql = " select (FirstName + ' ' + LastName ) AS UserName, LoginId from dbo.usermaster with(nolock) " +
                  " where loginid in (select userid from pos_invoice_mgmt where Approval = 1) AND statusId = 'A' and LoginID = " + Common.CastAsInt32(Session["loginid"]) + " ";
        else if (Phase == 2)
            sql = " select (FirstName + ' ' + LastName ) AS UserName, LoginId from dbo.usermaster with(nolock)  " +
                  "  where loginid in (select userid from pos_invoice_mgmt with(nolock) where Verification = 1) AND statusId = 'A' and LoginID =" + Common.CastAsInt32(Session["loginid"]) + "";
        else if (Phase == 3)
            //sql = " SELECT FirstName + ' ' + MiddleName + ' ' + FamilyName AS EmpName,USERID FROM DBO.Hr_PersonalDetails WHERE POSITION IN(4,89,29,1) and USERID=" + Common.CastAsInt32(Session["loginid"]) + " ";
            sql = " select (FirstName + ' ' + LastName ) AS UserName, LoginId from dbo.usermaster with(nolock)  " +
                  "  where loginid in (select userid from pos_invoice_mgmt with(nolock) where Approval3 = 1) AND statusId = 'A' and LoginID =" + Common.CastAsInt32(Session["loginid"]) + "";
        else if (Phase == 4)
            //sql = " SELECT FirstName + ' ' + MiddleName + ' ' + FamilyName AS EmpName,USERID FROM DBO.Hr_PersonalDetails WHERE POSITION=1 and USERID=" + Common.CastAsInt32(Session["loginid"]) + " ";
            sql = " select (FirstName + ' ' + LastName ) AS UserName, LoginId from dbo.usermaster with(nolock)  " +
                  "  where loginid in (select userid from pos_invoice_mgmt with(nolock) where Approval4 = 1) AND statusId = 'A' and LoginID =" + Common.CastAsInt32(Session["loginid"]) + "";
        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dt1.Rows.Count > 0)
            Ret = true;

        if (Common.CastAsInt32(Session["loginid"]) == 1 && Phase==1)
            Ret = true;
        return Ret;
    }
    
    //----------------------

    protected void btn_VerifySave_Click(object sender, EventArgs e)
    {
        try
        {
            if (trApprovalReasionLable.Visible)
                if (ddlApprovalReasion.SelectedIndex <= 0)
                {
                    lblMsgPoApproval.Text = "Please select reason for selecting Higher price quotation.";
                    return;
                }
            if (txtVerifyComments.Text.Trim()=="")
            {
                lblMsgPoApproval.Text = "Please enter comments.";
                return;
            }
            //Common.Execute_Procedures_Select_ByQuery("UPDATE dbo.BidApprovalList SET ApprovedOn=getdate(),Comments='" + txtVerifyComments.Text.Trim().Replace("'", "`") + "' where BidId=" + BidId + " and ApprovalPhase=" + ApprovalLevel);
            {
                string OrderQtyForUpdate = "";
                string BidItemID = "";
                Decimal totalLc = 0;
                Decimal totalLCUSD = 0;
                Decimal DiscountPer = 0;
                Decimal totalDiscount = 0;
                Decimal totalDiscountUSD = 0;
                Decimal totalGSTLC = 0;
                Decimal totalGSTUSD = 0;
                foreach (RepeaterItem RptItm in rptItems.Items)
                {
                    OrderQtyForUpdate = OrderQtyForUpdate + "," + ((TextBox)RptItm.FindControl("txtOrderQTY")).Text;
                    HiddenField hfItemID = (HiddenField)RptItm.FindControl("hfItemID");
                    BidItemID = BidItemID + ',' + hfItemID.Value;
                    Label lblLC = (Label)RptItm.FindControl("lblLC");
                    Label lblUsd = (Label)RptItm.FindControl("lblUsd");
                    Label lblGSTLC = (Label)RptItm.FindControl("lblGSTLC");
                    Label lblGSTUSD = (Label)RptItm.FindControl("lblGSTUsd");
                    totalGSTLC = Math.Round(Common.CastAsDecimal(totalGSTLC) + Common.CastAsDecimal(lblGSTLC.Text), 2);
                    totalGSTUSD = Math.Round(Common.CastAsDecimal(totalGSTUSD) + Common.CastAsDecimal(lblGSTUSD.Text), 2);
                    totalLCUSD = Math.Round(Common.CastAsDecimal(totalLCUSD) + Common.CastAsDecimal(lblUsd.Text), 2);
                    totalLc = Math.Round(Common.CastAsDecimal(totalLc) + Common.CastAsDecimal(lblLC.Text), 2);
                }

                if (!String.IsNullOrEmpty(txtDiscountPer.Text) && Common.CastAsDecimal(txtDiscountPer.Text) > 0)
                {
                    DiscountPer = Math.Round(Common.CastAsDecimal(txtDiscountPer.Text), 2);
                    if (DiscountPer > 0)
                    {
                        totalDiscount = Math.Round(((Common.CastAsDecimal(totalLc + totalGSTLC) * Common.CastAsDecimal(DiscountPer)) / 100), 2);
                        totalDiscountUSD = Math.Round(((Common.CastAsDecimal(totalLCUSD + totalGSTUSD) * Common.CastAsDecimal(DiscountPer)) / 100), 2);
                    }
                }
                OrderQtyForUpdate = OrderQtyForUpdate.Substring(1);
                BidItemID = BidItemID.Substring(1);
                int AppMode = Common.CastAsInt32(ViewState["PRStatusID"]);
                //
                Common.Set_Procedures("sp_NewPR_ApproveOrder_1");
                Common.Set_ParameterLength(17);
                Common.Set_Parameters(
                    new MyParameter("@BidID", BidId),
                    new MyParameter("@LoginID", UserID),
                    new MyParameter("@AppBy", Session["UserFullName"].ToString()),
                    new MyParameter("@AppMode", ApprovalLevel),
                    new MyParameter("@Comments", txtVerifyComments.Text.Trim()),
                    new MyParameter("@ExchRate", lblCurrRate.Text.Trim()),
                    new MyParameter("@LC", txtLC.Text.Trim()),
                    new MyParameter("@USD", lblUSD.Text),
                    new MyParameter("@OrderQty", OrderQtyForUpdate),
                    new MyParameter("@BidItemID", BidItemID),
                    new MyParameter("@TotalUsd", lblTotalUSdD.Text),
                    new MyParameter("@DiscountPer", txtDiscountPer.Text),
                    new MyParameter("@TotalDiscount", totalDiscount),
                    new MyParameter("@TotalDiscountUSD", lblTotalDiscountUSD.Text),
                    new MyParameter("@PoAccountCompany", ddlPOAccountCompany.SelectedIndex != 0 ? ddlPOAccountCompany.SelectedValue : ""),
                    new MyParameter("@TotalGSTLC", totalGSTLC),
                    new MyParameter("@TotalGSTUSD", totalGSTUSD)
                    );
                Boolean res;
                DataSet dsRes = new DataSet();
                res = Common.Execute_Procedures_IUD(dsRes);
                if (res == true)
                {
                    if (trApprovalReasionLable.Visible)
                        if (ddlApprovalReasion.SelectedIndex > 0)
                        {
                            string sql = " Exec sp_UpdateApprovalReason " + BidId + "," + ddlApprovalReasion.SelectedValue + "  ";
			    Common.Execute_Procedures_Select_ByQuery(sql);
                        }
                    if (ApprovalLevel==5)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "abc", "window.open('SendMail.aspx?Mailtype=2&Param=" + BidId.ToString() + "');", true);
                    }

                    lblMsgPoApproval.Text = "Records updated successfully.";
                }
                else
                {
                    lblMsgPoApproval.Text = "Unable to update record.";
                }
            }

            txtVerifyComments.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "Refresh();", true);
            dvApprovalWindow.Visible = false;
            DisplayApprovalLevel();
            Bind_rptApprovalList();
        }
        catch (Exception ex)
        { lblMsgPoApproval.Text = "Unable to Verify. Error :" + ex.Message; }

    }
    protected void lnlApprovePoRequest_Popup_OnClick(object sender, EventArgs e)
    {
        if (!IsBudgetAmountMoreThanZero())
        {
            lblMsgBS.Text = "Budget amount is zero.Bid can not be approved.";
		    return;
        }

        DisplayApprovalLevel();
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("EXEC DBO.IsLowestBidForApproval " + PoID + "," + BidId);
        string LowerBid = "N";
        if (dt.Rows.Count == 1)
            LowerBid = dt.Rows[0][0].ToString();

        if (ApprovalLevel == 2 && LowerBid=="N")
        {
            trApprovalReasionLable.Visible = false;
            trApprovalReasionControl.Visible = false;
            // change1562017 ------------------------------------
            string sql = " Select ApprovalReason from  dbo.Add_tblSMDPOMaster where Add_tblSMDPOMaster.Poid in "+
                         "   (Select VW_tblSMDPOMasterBid.poid from dbo.VW_tblSMDPOMasterBid Where BidID = "+ BidId + ") ";

            DataTable dtAR = Common.Execute_Procedures_Select_ByQuery("EXEC DBO.IsLowestBidForApproval " + PoID + "," + BidId);
            if (dtAR.Rows.Count > 0)
            {
                try
                {
                    ddlApprovalReasion.SelectedValue = dtAR.Rows[0][0].ToString();
                }
                catch { }
            }

            // end change1562017 ------------------------------------
        }
        else
        {
            trApprovalReasionLable.Visible = false;
            trApprovalReasionControl.Visible = false;
        }
        dvApprovalWindow.Visible = true;
    }

    protected void imgPlaceOrder_OnClick(object sender, EventArgs e)
    {
       if(ddlPOAccountCompany.Visible && ddlPOAccountCompany.SelectedIndex == 0)
        {
            lblMsgBS.Text = "Please select PO Account Company.";
            ddlPOAccountCompany.Focus();
            return;
        }
        if (!IsBudgetAmountMoreThanZero())
        {
            lblMsgBS.Text = "Budget amount is zero.Bid can not be approved.";
            return;
        }

        DisplayApprovalLevel();
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("EXEC DBO.IsLowestBidForApproval " + PoID + "," + BidId);
        string LowerBid = "N";
        if (dt.Rows.Count == 1)
            LowerBid = dt.Rows[0][0].ToString();

        if (ApprovalLevel == 2 && LowerBid == "N")
        {
            trApprovalReasionLable.Visible = false;
            trApprovalReasionControl.Visible = false;
            // change1562017 ------------------------------------
            string sql = " Select ApprovalReason from  dbo.Add_tblSMDPOMaster where Add_tblSMDPOMaster.Poid in " +
                         "   (Select VW_tblSMDPOMasterBid.poid from dbo.VW_tblSMDPOMasterBid Where BidID = " + BidId + ") ";
            DataTable dtAR = Common.Execute_Procedures_Select_ByQuery("EXEC DBO.IsLowestBidForApproval " + PoID + "," + BidId);
            if (dtAR.Rows.Count > 0)
            {
                try
                {
                    ddlApprovalReasion.SelectedValue = dtAR.Rows[0][0].ToString();
                }
                catch { }
            }

            // end change1562017 ------------------------------------
        }
        else
        {
            trApprovalReasionLable.Visible = false;
            trApprovalReasionControl.Visible = false;
        }
        dvApprovalWindow.Visible = true;
    }
    protected void btnApprovePoRequest_ClosePopup_OnClick(object sender, EventArgs e)
    {
        dvApprovalWindow.Visible = false;
    }
    protected void btnSendMail_OnClick(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "abc", "window.open('SendMail.aspx?Mailtype=2&Param=" + BidId.ToString() + "');", true);
    }
    
    // SendBack To Purchaser ----
    protected void btnSendBackToPurchaserPopup_OnClick(object sender, EventArgs e)
    {
        

        DvSendBackToPurchaser.Visible = true;
    }
    protected void btnCloseSendBackToPurchaser_OnClick(object sender, EventArgs e)
    {
        DvSendBackToPurchaser.Visible = false;
    }    
    protected void btnSendBackToPurchaser_OnClick(object sender, EventArgs e)
    {
        if (txtPurchaserComments.Text.Trim() == "")
        {
            lblMSgSendBackToPurchaser.Text = " Please enter comments";
            return;
        }
        
        try
        {
            Common.Set_Procedures("sp_NewPR_CancelRequestForApproval");
            Common.Set_ParameterLength(2);
            Common.Set_Parameters(
                new MyParameter("@BidId", BidId),
                new MyParameter("@Comments", txtPurchaserComments.Text.Trim().Replace("'", "`"))
                );
            Boolean res;
            DataSet Ds = new DataSet();
            res = Common.Execute_Procedures_IUD(Ds);
            if (res)
            {

                lblMSgSendBackToPurchaser.Text = "Bid sent back to purchaser successfully.";
                SendRfqToPurchaserMail(BidId);

                btnSendBackToPurchaserPopup.Visible = false;
                btnSaveAccCode.Visible = false;
                btnApprovePO_1.Visible = false;
                imgResetValues.Visible = false;
                imgPlaceOrder.Visible = false;
                //tblChangeAccCod.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMSgSendBackToPurchaser.Text = "Unable to save the record. Error:" + ex.Message;
        }
    }
    public void SendRfqToPurchaserMail(int BidID)
    {
        string ToEmail = "";
        string PurchaserComments = "",rfqno="" ;
        string sql = " SElect (select Email from dbo.UserLogin where LoginId=B.BidCreatedByID)EmailID,shipid + '-' + convert(varchar,prnum) + '-' + BidGroup  as BIDNO, " +
	" b1.ApproveComments " +
	" from Add_tblSMDPOMasterBid B  " +
	" inner join dbo.tblSMDPOMasterBid b1 on b.bidid=b1.bidid  " +
	" inner join dbo.tblSMDPOMaster p on p.poid=b1.poid  where b.BidID=" + BidID;

        DataTable dtEmail = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dtEmail.Rows.Count > 0)
        {
            ToEmail = dtEmail.Rows[0]["EmailID"].ToString();
            PurchaserComments = dtEmail.Rows[0]["ApproveComments"].ToString();
		rfqno = dtEmail.Rows[0]["BIDNO"].ToString();
        }
        ///ToEmail = "ajay.singh@mtmsm.com";
        if (ToEmail != "")
        {

            string UserEmail = ProjectCommon.gerUserEmail(Session["loginid"].ToString());
           // UserEmail = "emanager@energiossolutions.com";
            string[] ToAdds = { ToEmail };
            string[] CCAdds = { };
            string Subject = "RFQ No. " + rfqno + "  sent back to purchaser.";
            string Message = "<br/><br/>Subject RFQ has been sent back to you with following comments.<br/><br/>Comments : " + PurchaserComments + " <br/><br/>From,<br/>" + Session["UserFullName"].ToString() ;
            string Error = "";
            if (ProjectCommon.SendeMail(UserEmail, UserEmail, ToAdds, CCAdds, CCAdds, Subject, Message, out Error, ""))
            {

            }
        }
    }
    protected void btnQuoteAnalyzer_OnClick(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "dfd", "window.open('SMDPOAnalyzer.aspx?Prid="+ PagePoID + "')", true);
    }

    //--Approval Level 1--------------------------------- 11111111111111
    protected bool IsBudgetAmountMoreThanZero()
    {
        bool res = true;

        int AccountID = 0;
        int BYear= 0;
        string CoCode = "";

        string sqlCheck = " Select 1 from dbo.tblSMDPOMasterBid B " +
                                  " inner join dbo.vw_tblSmdPoMaster P on B.Poid = P.PoID " +
                                  " inner join dbo.Sql_TblSmdPrAccountS A on A.AccountID = P.AccountID " +
                                  " where BidID = " + BidId+ " AND (A.AccountNumber IN(5723,8100,6030) or A.MidCatID in(5,13) or LEFT(CONVERT(varchar, A.AccountNumber),1)='1') ";
        DataTable dtCheck = Common.Execute_Procedures_Select_ByQuery(sqlCheck);
        if (dtCheck.Rows.Count > 0)
            return true;

            string sql = " SELECT REPLACE(STR(CONVERT(VARCHAR,ACCOUNTNUMBER),4),' ','0') + REPLACE(STR(CONVERT(VARCHAR,VESSELId),4),' ','0') AccID " +
                     "  , YEAR(GETDATE()) BYear, AccontCompany As COMPANY " +
                     "  FROM DBO.TBLSMDPOMASTERBID B " +
                     "  INNER JOIN DBO.TBLSMDPOMASTER P ON B.POID = P.POID " +
                     "  INNER JOIN DBO.Vessel V ON V.VesselCode = P.SHIPID " +
                     "  INNER JOIN DBO.sql_tblSMDPRAccounts A ON A.ACCOUNTID = P.ACCOUNTID " +
                     "  WHERE BIDID ="+ BidId;
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS(sql);
        if (dt.Rows.Count > 0)
        {
            AccountID = Common.CastAsInt32(dt.Rows[0]["AccID"]);
            BYear = Common.CastAsInt32(dt.Rows[0]["BYear"]);
            CoCode = dt.Rows[0]["COMPANY"].ToString();

            decimal Amount = 0;
            string sql1 = " select Amount from dbo.Add_v_BudgetForecastYear where CoCode='" + CoCode + "' and AcctID="+ AccountID + " and BYear="+ BYear + " ";
            DataTable dt1 = Common.Execute_Procedures_Select_ByQueryCMS(sql1);
            if (dt1.Rows.Count > 0)
            {
                Amount = Common.CastAsDecimal(dt1.Rows[0][0]);
                if (Amount > 0)
                    res = true;
            }
        }

        return res;

    }
    protected void lnlApprovePo_1_Request_Popup_OnClick(object sender, EventArgs e)
    {
        if (!IsBudgetAmountMoreThanZero())
        {
            lblMsgBS.Text = "Budget amount is zero.Bid can not be approved.";
	        return;
        }
        int _userid = Common.CastAsInt32(Session["loginid"]);

        // change1562017 ------------------------------------
        DataTable dt1 = Common.Execute_Procedures_Select_ByQuery("EXEC DBO.IsLowestBidForApproval " + PoID + "," + BidId);
        string LowerBid = "N";
        if (dt1.Rows.Count == 1)
            LowerBid = dt1.Rows[0][0].ToString();


        if (ApprovalLevel == 1 && LowerBid == "N")
        {
            trApp1ReasonForHigherPriceLable.Visible = false;
            trApp1ReasonForHigherPriceControl.Visible = false;
        }
        else
        {
            trApp1ReasonForHigherPriceLable.Visible = false;
            trApp1ReasonForHigherPriceControl.Visible = false;
        }
        // end change1562017 ------------------------------------

        //-------------
        DataTable dt = Common.Execute_Procedures_Select_ByQueryCMS("select BIDFWDBYID from DBO.Add_tblSMDPOMASTERBID WHERE BIDID=" + BidId);
        if(dt.Rows.Count>0)
        {
            //-------------
            int _AppFwdByid = Common.CastAsInt32(dt.Rows[0]["BIDFWDBYID"]);

            ////if (_userid == _AppFwdByid)
            ////{
            ////    lblMsgBS.Text = "Since you have created this RFQ hence you can not approve.";
            ////}
            ////else
            {
                //-------------
                hfSelectedTaskID_app.Value = "";
                ShowTaskList();
                dvPoApprovalLeve_1.Visible = true;
                //-------------
            }
        }
    }
    protected void btnClosePoApprovalLevel_1_ClosePopup_OnClick(object sender, EventArgs e)
    {
        dvPoApprovalLeve_1.Visible = false;
    }
    
    protected void btn_VerifyPoApproval_1_Click(object sender, EventArgs e)
    {
        try
        {
            
            int taskid = Common.CastAsInt32(hfSelectedTaskID_app.Value);
            if (string.IsNullOrWhiteSpace(txtRemarksApproval_1.Text.Trim()))
            {
                lblMsgPoApproval_1.Text = "Please enter comments.";               
                return;
            }
            //Validation
            if (ddlApp1ReasonForHigherPrice.Visible) // change1562017
                if (ddlApp1ReasonForHigherPrice.SelectedIndex <= 0)
                {
                    lblMsgPoApproval_1.Text = "Please select reason for selecting Higher price quotation.";
                    return;
                }
            // end change1562017 ------------------------------------

            //if (taskid <= 0)
            //{
            //    lblMsgPoApproval_1.Text = "Please select a allocation to link with this purchase order.";
            //    return;
            //}

            //Common.Execute_Procedures_Select_ByQuery("UPDATE dbo.BidApprovalList SET ApprovedOn=getdate(),Comments='" + txtVerifyComments.Text.Trim().Replace("'", "`") + "' where BidId=" + BidId + " and ApprovalPhase=" + ApprovalLevel);
            {
                string OrderQtyForUpdate = "";
                string BidItemID = "";
                Decimal totalLc = 0;
                Decimal totalLCUSD = 0;
                Decimal DiscountPer = 0;
                Decimal totalDiscount = 0;
                Decimal totalDiscountUSD = 0;
                Decimal totalGSTLC = 0;
                Decimal totalGSTUSD = 0;
                foreach (RepeaterItem RptItm in rptItems.Items)
                {
                    OrderQtyForUpdate = OrderQtyForUpdate + "," + ((TextBox)RptItm.FindControl("txtOrderQTY")).Text;
                    HiddenField hfItemID = (HiddenField)RptItm.FindControl("hfItemID");
                    BidItemID = BidItemID + ',' + hfItemID.Value;
                    Label lblLC = (Label)RptItm.FindControl("lblLC");
                    Label lblUsd = (Label)RptItm.FindControl("lblUsd");
                    Label lblGSTLC = (Label)RptItm.FindControl("lblGSTLC");
                    Label lblGSTUSD = (Label)RptItm.FindControl("lblGSTUsd");
                    totalGSTLC = Math.Round(Common.CastAsDecimal(totalGSTLC) + Common.CastAsDecimal(lblGSTLC.Text), 2);
                    totalGSTUSD = Math.Round(Common.CastAsDecimal(totalGSTUSD) + Common.CastAsDecimal(lblGSTUSD.Text), 2);
                    totalLCUSD = Math.Round(Common.CastAsDecimal(totalLCUSD) + Common.CastAsDecimal(lblUsd.Text), 2);
                    totalLc = Math.Round(Common.CastAsDecimal(totalLc) + Common.CastAsDecimal(lblLC.Text), 2);
                }
                if (!String.IsNullOrEmpty(txtDiscountPer.Text) && Common.CastAsDecimal(txtDiscountPer.Text) > 0)
                {
                    DiscountPer = Math.Round(Common.CastAsDecimal(txtDiscountPer.Text), 2);
                    if (DiscountPer > 0)
                    {
                        totalDiscount = Math.Round(((Common.CastAsDecimal(totalLc+ totalGSTLC) * Common.CastAsDecimal(DiscountPer)) / 100), 2);
                        totalDiscountUSD = Math.Round(((Common.CastAsDecimal(totalLCUSD+ totalGSTUSD) * Common.CastAsDecimal(DiscountPer)) / 100), 2);
                    }

                }

                OrderQtyForUpdate = OrderQtyForUpdate.Substring(1);
                BidItemID = BidItemID.Substring(1);
                int AppMode = Common.CastAsInt32(ViewState["PRStatusID"]);
                //
                Common.Set_Procedures("sp_NewPR_ApproveOrder_1_Level1");
                Common.Set_ParameterLength(18);
                Common.Set_Parameters(
                    new MyParameter("@BidID", BidId),
                    new MyParameter("@LoginID", UserID),
                    new MyParameter("@AppBy", Session["UserFullName"].ToString()),
                    new MyParameter("@AppMode", ApprovalLevel),
                    new MyParameter("@Comments", txtRemarksApproval_1.Text.Trim()),
                    new MyParameter("@ExchRate", lblCurrRate.Text.Trim()),
                    new MyParameter("@LC", txtLC.Text.Trim()),
                    new MyParameter("@USD", lblUSD.Text),
                    new MyParameter("@OrderQty", OrderQtyForUpdate),
                    new MyParameter("@BidItemID", BidItemID),
                    new MyParameter("@TotalUsd", lblTotalUSdD.Text),
                    new MyParameter("@TaskID", taskid),
                    new MyParameter("@DiscountPer", DiscountPer),
                    new MyParameter("@TotalDiscount", totalDiscount),
                    new MyParameter("@TotalDiscountUSD", totalDiscountUSD),
                    new MyParameter("@PoAccountCompany", ddlPOAccountCompany.SelectedIndex != 0 ? ddlPOAccountCompany.SelectedValue : ""),
                    new MyParameter("@TotalGSTLC", totalGSTLC),
                    new MyParameter("@TotalGSTUSD", totalGSTUSD)
                    );
                Boolean res;
                DataSet dsRes = new DataSet();
                res = Common.Execute_Procedures_IUD(dsRes);
                if (res == true)
                {
                    if (ApprovalLevel == 1)
                    {
                        // change1562017 ------------------------------------
                        if (ddlApp1ReasonForHigherPrice.Visible)
                            if (ddlApp1ReasonForHigherPrice.SelectedIndex > 0)
                            {
                                string sql = " Exec sp_UpdateApprovalReason " + BidId + "," + ddlApp1ReasonForHigherPrice.SelectedValue + "  ";
                                Common.Execute_Procedures_Select_ByQuery(sql);
                            }
                        // end change1562017 ------------------------------------

                    }
                    else if (ApprovalLevel == 5)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "abc", "window.open('SendMail.aspx?Mailtype=2&Param=" + BidId.ToString() + "');", true);
                    }

                    lblMsgPoApproval_1.Text = "Records updated successfully.";

                    txtRemarksApproval_1.Text = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "Refresh();", true);
                    dvPoApprovalLeve_1.Visible = false;
                    Bind_rptApprovalList();
                    ShowTaskList();
                }
                else
                {
                    lblMsgPoApproval_1.Text = "Unable to update record.";
                }
            }

          
        }
        catch (Exception ex)
        { lblMsgPoApproval_1.Text = "Unable to Verify. Error :" + ex.Message; }

    }
    public DataSet GetTrackingData(string Company, int BudgetYear, int BudgetMonth, string VesselCode)
    {

        DataSet DS = new DataSet();
        if (Cache["TrackingDatas1"] == null)
        {
            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["eMANAGER"].ConnectionString);
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("exec getVarianceRepport_vsl_BudgetTracking '" + Company + "'," + BudgetMonth + "," + BudgetYear + ",'" + VesselCode + "'", con); //???????
            cmd.CommandTimeout = 300;
            cmd.CommandType = CommandType.Text;
            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter();
            adp.SelectCommand = cmd;
            adp.Fill(DS);
            Cache["TrackingDatas1"] = DS;
        }
        else
        {
            DS = (DataSet)(Cache["TrackingDatas1"]);
        }
        return DS;
    }
    public void ShowTaskList()
    {
        //int majcatid = Common.CastAsInt32(hfSelMajCatID.Value);
        //int midcatid = Common.CastAsInt32(hfSelMidCatID.Value);
        //int mincatid = Common.CastAsInt32(hfSelMinCatID.Value);
        //int AccountID = Common.CastAsInt32(hfSelAccountID.Value);


        lblCompany.Text = ComCode;
        lblVessel.Text = VesselCode;
        lblYear.Text = DateTime.Today.Year.ToString();
        lblAccountNoNameTaskList.Text = AccountName;

        //Show header information
        int Month = DateTime.Today.Month;
        DataSet DsValue = GetTrackingData(ComCode, DateTime.Today.Year, Month, VesselCode);
        DataTable Dt = DsValue.Tables[3];
        DataView DV = Dt.DefaultView;
        //DV.RowFilter = "MAJCATID=" + majcatid.ToString() + " and MIDCATID=" + midcatid.ToString() + " and MINCATID=" + mincatid.ToString() + " and AccountID=" + AccountID.ToString(); ;
        DV.RowFilter = " AccountID=" + AccountID.ToString(); 
        if (DV.ToTable().Rows.Count > 0)
        {
            DataRow dr = DV.ToTable().Rows[0];

            lblAccountNoNameTaskList.Text = dr["Accountnumber"].ToString() + " - " + dr["AccountName"].ToString();
            lblYTDActule.Text = Common.CastAsInt32(dr["AcctYTDAct"]).ToString();
            lblYTDCommitted.Text = Common.CastAsInt32(dr["AcctYTD_Comm"]).ToString();
            lblYTDConsumed.Text = Common.CastAsInt32(dr["AcctYTDCons"]).ToString();
            lblYTDBudget.Text = Common.CastAsInt32(dr["AcctYTDBgt"]).ToString();
            lblYTDVariance.Text = Common.CastAsInt32(dr["AcctYTDVar"]).ToString();
            lblYTDVariancePer.Text = Common.CastAsInt32(dr["Col1"]).ToString();
            lblYTDAnnualBudget.Text = Common.CastAsInt32(dr["AcctFYBudget"]).ToString();
            lblYTDAnnualUtilization.Text = Common.CastAsInt32(dr["Col2"]).ToString();
        }
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //string sql = "select *, " +
        //            " ( " +
        //            "    Case when Jan = 1 then 1 else 0 end + " +
        //            "    Case when Feb = 1 then 1 else 0 end + " +
        //            "    Case when Mar = 1 then 1 else 0 end + " +
        //            "    Case when Apr = 1 then 1 else 0 end + " +
        //            "    Case when May = 1 then 1 else 0 end + " +
        //            "    Case when Jun = 1 then 1 else 0 end + " +
        //            "    Case when Jul = 1 then 1 else 0 end + " +
        //            "    Case when Aug = 1 then 1 else 0 end + " +
        //            "    Case when Sep = 1 then 1 else 0 end + " +
        //            "    Case when Oct = 1 then 1 else 0 end + " +
        //            "    Case when Nov = 1 then 1 else 0 end + " +
        //            "    Case when Dec = 1 then 1 else 0 end   " +
        //            "    ) as ConsumptionMonthCount " +
        //            "    ,(select SUM(EstShippingUSD+CreditUSD+BidAmt)as TotConsume from vw_BudgetTracking where taskid=tbl_BudgetTracking.taskid) as TotConsume" +
        //            " from tbl_BudgetTracking where Company='" + ComCode + "' and VesselCode='" + VesselCode + "' and BudgetYear=" + DateTime.Today.Year + " and AccountID=" + AccountID + "";

        string sql = "select *, " +
                   " ( " +
                   "    Case when Jan = 1 then 1 else 0 end + " +
                   "    Case when Feb = 1 then 1 else 0 end + " +
                   "    Case when Mar = 1 then 1 else 0 end + " +
                   "    Case when Apr = 1 then 1 else 0 end + " +
                   "    Case when May = 1 then 1 else 0 end + " +
                   "    Case when Jun = 1 then 1 else 0 end + " +
                   "    Case when Jul = 1 then 1 else 0 end + " +
                   "    Case when Aug = 1 then 1 else 0 end + " +
                   "    Case when Sep = 1 then 1 else 0 end + " +
                   "    Case when Oct = 1 then 1 else 0 end + " +
                   "    Case when Nov = 1 then 1 else 0 end + " +
                   "    Case when Dec = 1 then 1 else 0 end   " +
                   "    ) as ConsumptionMonthCount " +
                   "    ,(select SUM(poamount)as TotConsume from VW_TaskLinkedOrders where taskid=tbl_BudgetTracking.taskid) as TotConsume" +
                   " from tbl_BudgetTracking where Company='" + ComCode + "' and VesselCode='" + VesselCode + "' and BudgetYear=" + DateTime.Today.Year + " and AccountID=" + AccountID + "";
        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        //rptTrackingTaskList.DataSource = DT;
        //rptTrackingTaskList.DataBind();
        //lblTotalTaskAmount.Text = DT.Compute("Sum(Amount)", "").ToString();
        //if (Common.CastAsInt32(DT.Compute("Sum(Amount)", "")) > Common.CastAsInt32(lblBudgetAmt.Text))
        //    lblTotalTaskAmount.ForeColor = System.Drawing.Color.Red;

    }
    public string GetVariancePerMonthTaskKList(object TaskID, object ConsumptionMonthCount, int M)
    {

        string Ret = "0.0";
        int iTaskID = Common.CastAsInt32(TaskID);
        int iConsumptionMonthCount = Common.CastAsInt32(ConsumptionMonthCount);
        decimal Amount = 0;
        decimal ConsAmount = 0;
        decimal AvgAmount = 0;
        string sql = " select Amount,EstShippingUSD+CreditUSD+BidAmt as ConAmont from vw_BudgetTracking where TaskID=" + iTaskID + " and Month(BidSMDLevel1ApprovalDate)=" + M + " ";
        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DT.Rows.Count > 0)
        {
            Amount = Common.CastAsDecimal(DT.Rows[0]["Amount"]);
            ConsAmount = Common.CastAsDecimal(DT.Rows[0]["ConAmont"]);
            if (iConsumptionMonthCount > 0)
            {
                AvgAmount = Amount / iConsumptionMonthCount;
                Ret = ConsAmount.ToString("0.00");
            }
        }

        return Common.CastAsInt32(Ret).ToString();
    }
    public string GetCSSVariancePerMonthTaskKList(object TaskID, object ConsumptionMonthCount, int M, object Budgeted)
    {
bool bBudgeted =false;
        string Ret = "";
try{
        bBudgeted = (bool)(Budgeted);
}catch{}

        int iTaskID = Common.CastAsInt32(TaskID);
        int iConsumptionMonthCount = Common.CastAsInt32(ConsumptionMonthCount);
        decimal Amount = 0;
        decimal ConsAmount = 0;
        decimal AvgAmount = 0;

        string sql = " select Amount,EstShippingUSD+CreditUSD+BidAmt as ConAmont from vw_BudgetTracking where TaskID=" + iTaskID + " and Month(BidSMDLevel1ApprovalDate)=" + M + " ";
        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DT.Rows.Count > 0)
        {
            Amount = Common.CastAsDecimal(DT.Rows[0]["Amount"]);
            ConsAmount = Common.CastAsDecimal(DT.Rows[0]["ConAmont"]);
            if (iConsumptionMonthCount > 0)
            {
                AvgAmount = Amount / iConsumptionMonthCount;
            }
            if (bBudgeted)
            {
                if (ConsAmount > AvgAmount)
                    Ret = "red";
                else
                    Ret = "green";
            }
            else
            {
                Ret = "green";
            }

        }
        else
        {
            if (bBudgeted)
                Ret = "green";
        }

        return Ret;
    }

    //--Add Task---------------------------------------------------------------    
    protected void btnOpenAddTaskPopup_OnClick(object sender, EventArgs e)
    {
        dvAddTrackingTask.Visible = true;
    }
    protected void btnCloseAddTrackingTaskPopup_OnClick(object sender, EventArgs e)
    {
        dvAddTrackingTask.Visible = false;
        ClearTrackingControl();        
    }
    protected void btnSaveTrackingTask_OnClick(object sender, EventArgs e)
    {
        if (txtTtDescription.Text.Trim() == "")
        {
            lblMsgTrackingTask.Text = "Please enter description.";
            txtTtDescription.Focus();
            return;
        }
        if (txtTtAmount.Text.Trim() == "")
        {
            lblMsgTrackingTask.Text = "Please enter amount.";
            txtTtAmount.Focus();
            return;
        }

        if (txtTtDescription.Text.Trim().Length > 250)
        {
            lblMsgTrackingTask.Text = "Description should be within 250 character.";
            txtTtDescription.Focus();
            return;
        }
        if (ddlTaskType.SelectedIndex == 0)
        {
            lblMsgTrackingTask.Text = " Please select Allocation type.";
            ddlTaskType.Focus();
            return;
        }


        Common.Set_Procedures("sp_IU_tbl_BudgetTracking");
        Common.Set_ParameterLength(21);
        Common.Set_Parameters(
            new MyParameter("@TaskID", 0),
            new MyParameter("@Company", ComCode),
            new MyParameter("@VesselCode", VesselCode),
            new MyParameter("@BudgetYear", DateTime.Today.Year),
            new MyParameter("@AccountID", Common.CastAsInt32(AccountID)),
            new MyParameter("@TaskDescription", txtTtDescription.Text.Trim()),
            new MyParameter("@Amount", txtTtAmount.Text.Trim()),
            //new MyParameter("@Jan", (chkTtJan.Checked)),
            //new MyParameter("@Feb", (chkTtFeb.Checked)),
            //new MyParameter("@Mar", (chkTtMar.Checked)),
            //new MyParameter("@Apr", (chkTtApr.Checked)),
            //new MyParameter("@May", (chkTtMay.Checked)),
            //new MyParameter("@Jun", (chkTtJun.Checked)),
            //new MyParameter("@Jul", (chkTtJul.Checked)),
            //new MyParameter("@Aug", (chkTtAug.Checked)),
            //new MyParameter("@Sep", (chkTtSep.Checked)),
            //new MyParameter("@Oct", (chkTtOct.Checked)),
            //new MyParameter("@Nov", (chkTtNov.Checked)),
            //new MyParameter("@Dec", (chkTtDec.Checked)),
            new MyParameter("@Jan", DBNull.Value),
            new MyParameter("@Feb", DBNull.Value),
            new MyParameter("@Mar", DBNull.Value),
            new MyParameter("@Apr", DBNull.Value),
            new MyParameter("@May", DBNull.Value),
            new MyParameter("@Jun", DBNull.Value),
            new MyParameter("@Jul", DBNull.Value),
            new MyParameter("@Aug", DBNull.Value),
            new MyParameter("@Sep", DBNull.Value),
            new MyParameter("@Oct", DBNull.Value),
            new MyParameter("@Nov", DBNull.Value),
            new MyParameter("@Dec", DBNull.Value),
            new MyParameter("@budgeted", (ddlTaskType.SelectedValue == "1")),
            new MyParameter("@ModifiedBy", Session["UserName"].ToString())
            );
        DataSet Ds = new DataSet();
        Boolean res = false;
        res = Common.Execute_Procedures_IUD(Ds);
        if (res)
        {
            if (Common.CastAsInt32(Ds.Tables[0].Rows[0][0]) > 0)
            {
                lblMsgTrackingTask.Text = "Record saved successfully.";
                ClearTrackingControl();
                ShowTaskList();
            }
            else
            {   
                lblMsgTrackingTask.Text = "Please check this task causing total budget amount for this account more than annual budget. It is not allowed.";
            }
        }
        else
        {
            lblMsgTrackingTask.Text = "Record could not saved." + Common.ErrMsg;
        }

    }
    public void ClearTrackingControl()
    {
        txtTtAmount.Text = "";
        txtTtDescription.Text = "";
        //lblTaskModifiedByOn.Text = "";
        //chkTtJan.Checked = false;
        //chkTtFeb.Checked = false;
        //chkTtMar.Checked = false;
        //chkTtApr.Checked = false;
        //chkTtMay.Checked = false;
        //chkTtJun.Checked = false;
        //chkTtJul.Checked = false;
        //chkTtAug.Checked = false;
        //chkTtSep.Checked = false;
        //chkTtOct.Checked = false;
        //chkTtNov.Checked = false;
        //chkTtDec.Checked = false;
        ddlTaskType.SelectedIndex = 0;
    }

    public string GetCompanyName()
    {
        string ret = "";
        string sql = " Select AccontCompany As Company from Vessel with(nolock)  where VesselCode = '" + VesselCode+"' ";
        DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DT.Rows.Count > 0)
            ret = DT.Rows[0][0].ToString();

        return ret;

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
    protected void ddlTaskType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlTaskType.SelectedValue == "2")
        {
            txtTtAmount.Text = "0";
            txtTtAmount.Enabled = false;
        }
        else
        {
            txtTtAmount.Enabled = true;
        }
    }

    //- Change Accout Code -----------------------------------------------------------------------
    protected void lnkChangeAccountCode_OnClick(object sender, EventArgs e)
    {
        SetUserControlData();
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
        ShowMasterData(); 
    }
    public void SetUserControlData()
    {
        string VesselCode = "";
        string CompCode = "";
        string sql = " SELECT POID FROM DBO.TBLSMDPOMASTERBID WHERE BIDID=" + BidId;
        DataTable dtPoID = Common.Execute_Procedures_Select_ByQuery(sql);
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

        sql = "Select AccontCompany As Company from Vessel with(nolock)  where VesselCode='" + VesselCode + "'";
        DataTable dtCompCOde = Common.Execute_Procedures_Select_ByQuery(sql);
        if (dtCompCOde.Rows.Count > 0)
        {
            CompCode = Convert.ToString(dtCompCOde.Rows[0][0]);
        }
        account.BidID = BidId;
        account.POId = PoID;
        account.VesselCode = VesselCode;
        account.CompCode = CompCode;
    }


    protected void btnSavePoQuantity_OnClick(object sender, EventArgs e)
    {
        foreach (RepeaterItem item in rptItems.Items)
        {
            HiddenField hfItemID=(HiddenField)item.FindControl("hfItemID");
            
            TextBox txtOrderQTY = (TextBox)item.FindControl("txtOrderQTY");

            string sql = " update dbo.VW_tblSMDPODetailBid set QtyPO ="+ Common.CastAsDecimal(txtOrderQTY.Text) + " where Bidid="+ BidId+ " and BidItemID="+ hfItemID.Value;
            Common.Execute_Procedures_Select_ByQuery(sql);
            
        }
        ShowMasterData();
        imgResetValues_Click(sender, e);
        hdnStatus.Value = "C";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dia45log", "window.location.reload();", true);
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
