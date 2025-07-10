using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Net.Mail;
using System.Linq;

public partial class UserControls_StoreRFQ : System.Web.UI.UserControl
{
    public AuthenticationManager authRFQList = new AuthenticationManager(0, 0, ObjectType.Page);
    public int Sel_BidId
    {
        set { ViewState["_Sel_BidId"] = value; }
        get { return int.Parse("0" + ViewState["_Sel_BidId"]); }
    }
    public decimal Sel_BidAmount
    {
        set { ViewState["_Sel_BidAmount"] = value; }
        get { return decimal.Parse("0" + ViewState["_Sel_BidAmount"]); }
    }
    //public string ShowAmount
    //{
    //    get
    //    {
    //        return (ViewState["ShowAmount"] == null) ? "N" : ViewState["ShowAmount"].ToString();
    //    }
    //    set { ViewState["ShowAmount"] = value; }
    //}
    //---------------------------
    #region ------- PROPERTIES -------------------
    public bool PRBidStopped
    {
        set { ViewState["PRBidStopped"] = value; }
        get { return Convert.ToBoolean(ViewState["PRBidStopped"]); }
    }
    public List<string> Vendors 
    {
        get {
            List<string> tmp;   
            tmp = (List<string>)ViewState["Vendors"];
            if (tmp==null)
                tmp = new List<string>();
            ViewState["Vendors"] = tmp;
            return tmp;
        }
    }
    public string SelectedItems
    {
        get
        {
            string SelectedItems = "";
            for (int i = 0; i <= rptItems.Items.Count - 1; i++)
            {
                CheckBox ch = (CheckBox)rptItems.Items[i].FindControl("chkSelect");
                if (ch.Checked)
                {
                    SelectedItems = SelectedItems + "," + ch.CssClass;
                }
            }
            if (SelectedItems.StartsWith(",")) { SelectedItems = SelectedItems.Substring(1); }
            return SelectedItems;
        }
    }
    public string SelectedQty
    {
        get
        {
            string SelectedItems = "";
            for (int i = 0; i <= rptItems.Items.Count - 1; i++)
            {
                CheckBox ch = (CheckBox)rptItems.Items[i].FindControl("chkSelect");
                TextBox tx = (TextBox)rptItems.Items[i].FindControl("txtQty");
                if (ch.Checked)
                {
                    SelectedItems = SelectedItems + "," + Common.CastAsInt32(tx.Text);
                }
            }
            if (SelectedItems.StartsWith(",")) { SelectedItems = SelectedItems.Substring(1); }
            return SelectedItems;
        }
    }
    public string SelectedVendors
    {
        get
        {
            string SelectedVendors = "";
            for (int i = 0; i <= Vendors.Count - 1; i++)
            {
                 SelectedVendors = SelectedVendors + "," + Vendors[i];
            }
            if (SelectedVendors.StartsWith(",")) { SelectedVendors = SelectedVendors.Substring(1); }
            return SelectedVendors;
        }
    }
    public int PRId
    {
        set { ViewState["PRId"] = value; }
        get { return int.Parse("0" + ViewState["PRId"]); }
    }
    # endregion
    //---------------------------
    # region ------- PAGE CONTROL LOADER ----------
    protected void LoadSupt()
    {
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT LoginID,FirstName + ' ' + Lastname as SuptName FROM USERMASTER WHERE ROLEID=17"); //EMAIL,
        ddlSupt.DataSource = dt;
        ddlSupt.DataTextField = "SuptName";
        ddlSupt.DataValueField = "LoginID";
        ddlSupt.DataBind();
        ddlSupt.Items.Insert(0, new ListItem("", ""));     
    }
    # endregion
    //---------------------------
    # region ------- EVENTS -----------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        #region --------- USER RIGHTS MANAGEMENT -----------
        try
        {
            authRFQList = new AuthenticationManager(1061, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
            if (!(authRFQList.IsView))
            {
                Response.Redirect("~/AuthorityError.aspx");
            }
            
        }
        catch (Exception ex)
        {
            Response.Redirect("~/NoPermission.aspx?Message=" + ex.Message);
        }
        #endregion ----------------------------------------

        btnSmdPoAnalyzer.Attributes.Add("onclick", "window.open('SMDPOAnalyzer.aspx?Prid="+PRId+"'); return false;");
        lblMessage.Text = "";
        lblPOPMsg.Text = "";
        lblmsg01.Text = "";
        if (!IsPostBack)
        {
            PRBidStopped = true;
            //ShowAmount = "N";

	        LoadSupt();
            BindCountry();
            BindBusinessType();
            bindPayment_ForwardToddl();    

	        PRId = int.Parse("0" + Request.QueryString["PRID"]);
            DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT SHIPID + ' - ' + CONVERT(VARCHAR,PRNUM) + ' ( ' + Isnull(RequisitionTitle,'') + ' )' AS RFQHEADER,SHIPID,SMDLevel1ApprovalDate FROM VW_TBLSMDPOMASTER WHERE POID=" + PRId);
            if (dt != null)
                if (dt.Rows.Count > 0)
                {
                    int loginid = Common.CastAsInt32(Session["loginid"]);
                    lblHeader.Text = dt.Rows[0][0].ToString();
                    string shipid = dt.Rows[0][1].ToString();
                    //-----------
                    if (Convert.IsDBNull(dt.Rows[0]["SMDLevel1ApprovalDate"]))
                    {
                        PRBidStopped = false;
                        btnSelectVendor.Visible = true && authRFQList.IsAdd;
                        btnBidFinished.Visible = true;
                    }
                    else
                    {
                        PRBidStopped = true;
                        btnSelectVendor.Visible = false;
                        btnBidFinished.Visible = false;
                    }
                    //-----------
                    //if (ProjectCommon.View_Quote_Permission(PRId, 0, loginid) || loginid==215 || loginid == 299)
                    //{
                    //    ShowAmount = "Y";
			            BindRFQ();
                    //}
                    //-----------
                }
            GetDocCount(PRId);

        }
    }
    protected void btnBack_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Modules/Purchase/Requisition/ReqFromVessels.aspx");
    }

    protected void btnBidFinishedConfirm_Click(object sender, EventArgs e)
    {
        if (trReason.Visible && ddlReason.SelectedIndex <= 0)
        {
            lblmsg01.Text = "Please select proper reason to continue.";
            return;
        }
        if (txtprcomm.Text.Trim() == "")
        {
            lblmsg01.Text = "Please enter purchaser comments to continue.";
            return;
        }

        //-------------------

        DataTable dtPR = Common.Execute_Procedures_Select_ByQuery("select shipid from VW_tblSMDPOMaster where poid=" + PRId.ToString() + "");
        //Button imgBtn = (Button)sender;
        //int BidId = int.Parse(imgBtn.CssClass);
        Common.Set_Procedures("SendPRForApproval");
        Common.Set_ParameterLength(4);
        Common.Set_Parameters(new MyParameter("@poid", PRId), 
            new MyParameter("@PurchaserComments", txtprcomm.Text.Trim()), 
            new MyParameter("@UserId", Session["loginid"]),
            new MyParameter("@SingleReason", ddlReason.SelectedValue));
        DataSet ds = new DataSet();
        if (Common.Execute_Procedures_IUD(ds))
        {
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    int result = Common.CastAsInt32(ds.Tables[0].Rows[0]["Status"]);
                    string msg = ds.Tables[0].Rows[0]["Message"].ToString();
                    if (result == 0)
                    {
                        PRBidStopped = true;
                        btnSelectVendor.Visible = false;
                        Button1.Visible = false;
                        btnBidFinished.Visible = false;

                        BindRFQ();

                        lblmsg01.Text = "Bid Process finished & forwareded to approval successfully.";
                    }
                    else
                    {
                        lblmsg01.Text = msg;
                    }
                }
                else
                {
                    lblmsg01.Text = "Unable to forward po for approval.";
                }
            }
            else
            {
                lblmsg01.Text = "Unable to forward po for approval.";
            }
        }
        else
        {
            lblmsg01.Text = "Unable to forward po for approval.";
        }
    }
    protected bool IsSingleQuotation(ref int SingleAllowed)
    {
        string ShipId = "";
        DataTable dtPR = Common.Execute_Procedures_Select_ByQuery("select shipid from VW_tblSMDPOMaster where poid=" + PRId.ToString() + "");
        if (dtPR.Rows.Count > 0)
        {
            ShipId = dtPR.Rows[0][0].ToString();
        }
        string sql1 = " select * from ( " +
                     "  select row_number() over(order by TAB.bidid) as Sno, bidgroup, TAB.BIDID, A.BidFwdOn, BIDGROUPNAME, SUPPLIERNAME, SUPPLIERPORT, BidStatusID, (SELECT COUNT(*) FROM BIDAPPROVALLIST AL WHERE AL.BIDID = TAB.BIDID) AS APPREQUESTS,         " +
                     "  (CASE WHEN BidStatusID >= 0 AND BidStatusID <= 2 THEN '" + ((authRFQList.IsDelete) ? "Y" : "N") + "' ELSE 'N' END) AS CanDelete, BIDSTATUSNAME,            " +
                     //"  (SELECT top 1 EVENTDATE FROM tblEventHistory WHERE STATUS = 2 AND DESCR = 'Quote received from vendor.' AND REFKEY = TAB.bidid order by EVENTDATE desc) AS QuoteRecdOn,            " +
                     " BidUpdatedOn," +
                     "  (SELECT USDTOT + ESTSHIPPINGUSD FROM dbo.VW_qUSDTotalsPerBid_SQL DET WHERE DET.BIDID = TAB.BIDID) AS AMT, APPROVECOMMENTS, BidCreatedBy, BidCreatedOn, coalesce((select top 1 eventdate from dbo.tbleventhistory where refkey = TAB.BIDID and status = 2 order by eventdate desc),  " +
                     "  (select top 1 statusdate from dbo.tblStatusLog where bidid = TAB.BIDID and bidstatusid = 2 order by statusdate desc)) as BidRecDate " +
                     "  from dbo.VW_qRFQListing_SQL TAB left " +
                     "  join Add_tblSMDPOMasterBid A on TAB.BIDID = A.BIDID " +
                     "  WHERE POID = " + PRId.ToString() + " and BidStatusId >= -1 " +
                     "  and shipid = '" + ShipId + "' " +
                     "  )T " +
                     "  where T.BidUpdatedOn is not null order by bidgroup ";

        DataTable dt111 = Common.Execute_Procedures_Select_ByQuery(sql1);
        if (dt111.Rows.Count == 1)
        {
            int _BidId = Common.CastAsInt32(dt111.Rows[0]["BIDID"]);
            DataTable dt341 = Common.Execute_Procedures_Select_ByQuery("exec dbo.IsSingleAllowed " + _BidId);
            if (dt341.Rows.Count > 0)
            {
                SingleAllowed = Convert.ToInt32(dt341.Rows[0][0]);
            }

            return true;
        }
        else
            return false;
    }
    protected void btnBidFinished_Click(object sender, EventArgs e)
    {
        int SingleAllowed = 0;
        lblCommReason.Text = "";
        if (IsSingleQuotation(ref SingleAllowed))
        {
            lblCommReason.Text = "NOTE : One quotation is allowed only for Nominated, makers, owner's approved vendors, vetting Inspection company. For all other vendors if only one quote then you must enter proper reason for having only one quote.";
        }
        trReason.Visible = (SingleAllowed < 0);
        Button1.Visible = true;
        dbPRComm.Visible = true;
    }
    protected void btnBidFinishedCancel_Click(object sender, EventArgs e)
    {
        dbPRComm.Visible = false;
    }

    protected void btnEmailRFQ_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgBtn = (ImageButton)sender;
        int BidId = int.Parse(imgBtn.CssClass);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "abc", "window.open('SendMail.aspx?Mailtype=1&Param=" + BidId.ToString() + "&UpdateBack=1');", true);
    }
    
    protected void btnApproveRFQ_Click(object sender, ImageClickEventArgs e)
    {
       

        lblBidRefNo.Text ="";
        lblError.Text = "";
        ImageButton imgBtn = (ImageButton)sender;
        int BidId = int.Parse(imgBtn.CssClass);

        //-------------------------------------------------------
        string ShipId = "";
        DataTable dtPR = Common.Execute_Procedures_Select_ByQuery("select shipid from VW_tblSMDPOMaster where poid=" + PRId.ToString() + "");
        if (dtPR.Rows.Count > 0)
        {
            ShipId = dtPR.Rows[0][0].ToString();
        }
        string sql1 = " select * from ( "+
                     "  select row_number() over(order by TAB.bidid) as Sno, bidgroup, TAB.BIDID, A.BidFwdOn, BIDGROUPNAME, SUPPLIERNAME, SUPPLIERPORT, BidStatusID, (SELECT COUNT(*) FROM BIDAPPROVALLIST AL WHERE AL.BIDID = TAB.BIDID) AS APPREQUESTS,         " +
                     "  (CASE WHEN BidStatusID >= 0 AND BidStatusID <= 2 THEN '" + ((authRFQList.IsDelete) ? "Y" : "N") + "' ELSE 'N' END) AS CanDelete, BIDSTATUSNAME,            " +
                     //"  (SELECT top 1 EVENTDATE FROM tblEventHistory WHERE STATUS = 2 AND DESCR = 'Quote received from vendor.' AND REFKEY = TAB.bidid order by EVENTDATE desc) AS QuoteRecdOn,            " +
                     " BidUpdatedOn," +
                     "  (SELECT USDTOT + ESTSHIPPINGUSD FROM dbo.VW_qUSDTotalsPerBid_SQL DET WHERE DET.BIDID = TAB.BIDID) AS AMT, APPROVECOMMENTS, BidCreatedBy, BidCreatedOn, coalesce((select top 1 eventdate from dbo.tbleventhistory where refkey = TAB.BIDID and status = 2 order by eventdate desc),  " +
                     "  (select top 1 statusdate from dbo.tblStatusLog where bidid = TAB.BIDID and bidstatusid = 2 order by statusdate desc)) as BidRecDate " +
                     "  from dbo.VW_qRFQListing_SQL TAB left " +
                     "  join Add_tblSMDPOMasterBid A on TAB.BIDID = A.BIDID " +
                     "  WHERE POID = " + PRId.ToString() + " and BidStatusId >= -1 " +
                     "  and shipid = '" + ShipId + "' " +
                     "  )T " +
                     "  where T.BidUpdatedOn is not null order by bidgroup ";

        dtPR = Common.Execute_Procedures_Select_ByQuery(sql1);
        decimal ApprovalAmount = Common.CastAsDecimal(Sel_BidAmount);
        if (dtPR.Rows.Count <= 1 && ApprovalAmount>500)
        {
            string qry = " select ApprovedBusinesses from dbo.tbl_VenderRequest  " +
                         "  where supplierid in (select supplierid from vw_tblsmdpomasterbid where bidid = " + BidId.ToString() + ") " +
                         "  and SecondApporvalType in (1,6,7) or(CHARINDEX(',27,', ',' + ApprovedBusinesses + ',', 0) > 0) ";

            DataTable dtRes = Common.Execute_Procedures_Select_ByQuery(qry);
            if (dtRes.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Single quotation can not be sent for approval');", true);
                return;
            }
        }        
        //-------------------------------------------------------

        Label lblBidAmount = (Label)imgBtn.Parent.FindControl("lblBidAmount");        
        Sel_BidAmount = Common.CastAsDecimal(lblBidAmount.Text.Replace("$",""));
        Sel_BidId = BidId;

        hfdBidId.Value = BidId.ToString();
        string sql = "SELECT dbo.getBidPONum(" + BidId.ToString() + ")";
        DataTable dt=Common .Execute_Procedures_Select_ByQuery(sql);  
        if(dt.Rows.Count >0)
        {
            lblBidRefNo.Text = dt.Rows[0][0].ToString();
            lblError.Text = ""; 
        }
        ModalPopupExtender2.Visible=true;
        ddlSupt.Focus();  
    }
    //protected void btnSendMail_Click(object sender, ImageClickEventArgs e)
    //{
    //    string BidId= hfdBidId.Value;
    //    string UserEmail = ProjectCommon.gerUserEmail(Session["loginid"].ToString());
    //    string[] ToAdds={ProjectCommon.getUserEmailByID(  ddlSupt.SelectedValue)};
    //    string[] CCAdds={};
    //    string Subject = "RFQ# " + lblBidRefNo.Text + " Ready for Approval" ;
    //    string Message = "RFQ# " + lblBidRefNo.Text + " is ready for your approval. Please use the PO database system to approve it.";
    //    string Error="";
    //    if (ProjectCommon.SendeMail(UserEmail, UserEmail, ToAdds, CCAdds, CCAdds, Subject, Message, out Error,""))
    //    {
    //        Common.Execute_Procedures_Select_ByQuery("EXEC sp_NewPR_RequestForApproval " + BidId + "," + ddlSupt.SelectedValue);
    //        ModalPopupExtender2.Visible = false; 
    //    }
    //    else
    //    {
    //        lblError.Text = Error;  
    //    }
    //}
    protected void btnCancelRFQ_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgBtn = (ImageButton)sender;
        int BidId= int.Parse(imgBtn.CssClass);
        Common.Set_Procedures("sp_NewPR_CancelRFQ");
        Common.Set_ParameterLength(3);
        Common.Set_Parameters(new MyParameter("@BidId", BidId), new MyParameter("@UserId", Session["loginid"]), new MyParameter("@UserName", Session["UserName"]));
        DataSet ds=new DataSet();
        if (Common.Execute_Procedures_IUD(ds))
        {
            BindRFQ();
            lblMessage.Text = "RFQ cancelled successfully.";
        }
        else
        {
            lblMessage.Text = "Unable to cancel RFQ.";
        }
    }
    protected void btnClose2_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Visible = false; 
    }

    protected void btnSelectVendor_Click(object sender, EventArgs e)
    {
        //-----------
        if (SelectedItems.Trim() == "")
        {
            lblMessage.Text = "Please select at least one item to create RFQ.";
            return; 
        }
        //-----------
        Vendors.Clear();
        BindVendorSelected(); 
        txtVendor_TextChanged(sender, e);
        ModalPopupExtender1.Visible = true;
        if (Session["sSelectedVendorName"] != null)
        {
            txtVendor.Text = Session["sSelectedVendorName"].ToString();
            btnFind_Click(sender, new ImageClickEventArgs(0,0));
        }
         txtVendor.Focus();  
    }
    protected void txtVendor_TextChanged(object sender, EventArgs e)
    {
        BindVendor();
    }
    protected void btnFind_Click(object sender, ImageClickEventArgs e)
    {
        BindVendor();
    }
    protected void btnAdd_Click(object sender, ImageClickEventArgs e)
    {
        for (int i = 0; i <= rptVendors.Items.Count - 1; i++)
        {
            CheckBox chk = (CheckBox)rptVendors.Items[i].FindControl("chkSelect");

            if (chk.Checked)
            {
                TextBox txt = (TextBox)rptVendors.Items[i].FindControl("txtEmail");
                string Addrs = setValidMail(txt.Text);
                if (Addrs != "")
                {
                    string VendorId = chk.CssClass;
                    if (UpdateVendorEmail(int.Parse(VendorId), Addrs))
                    {
                        if (!(Vendors.Contains(VendorId)))
                        {
                            Vendors.Add(VendorId);
                        }
                    }
                }
            }
        }
        BindVendorSelected();
    }
    protected void btnRemove_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgBtn=(ImageButton)sender;
        Vendors.Remove(imgBtn.CssClass);
        BindVendorSelected();
    }
    protected void btnCreateRFQ_Click(object sender, EventArgs e)
    {
        if (SelectedItems.Trim() == "")
        {
            lblPOPMsg.Text = "Please select items to create RFQ.";
            return;
        }
        if (SelectedVendors.Trim() == "")
        {
            lblPOPMsg.Text = "Please select suppliers to create RFQ.";
            return; 
        }
        //DataTable dt = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.tblSMDPOMasterBid WHERE POID=" + PRId + " AND SupplierID IN (" + SelectedVendors + ") AND BidStatusId >=0");
        //if (dt.Rows.Count > 0)
        //{
        //    lblPOPMsg.Text = "Can not create RFQ. One of selected vendor has already active RFQ.";
        //    return; 
        //}
        object DeliveryDate;
        if (txtDeliveryDate.Text.Trim() == "")
            DeliveryDate = DBNull.Value;
        else
            DeliveryDate = txtDeliveryDate.Text.Trim();
        //------------- DATA SAVING -------------------------------
        Common.Set_Procedures("sp_NewPR_InsertStoreBid");
        Common.Set_ParameterLength(8);
        Common.Set_Parameters(
                new MyParameter("@PoId", PRId),
                 new MyParameter("@ItemsList", SelectedItems),
                new MyParameter("@QtyList", SelectedQty),
                new MyParameter("@VendorsList", SelectedVendors),
                new MyParameter("@UserName", Session["UserName"]),
                new MyParameter("@BidCreatedByID", Session["loginid"]),
                new MyParameter("@BIDDELIVERYPORT", txtDeliveryPort.Text.Trim()),
                new MyParameter("@BIDPICKUPDATE", DeliveryDate)
                );
        DataSet dsBids=new DataSet();
        //-----------
        //lblPOPMsg.Text = PRId.ToString() + " : " + SelectedItems + " : " + SelectedQty + " : " + SelectedVendors + " : " + Session["UserName"].ToString();
        //return;
        //-----------
        Common.Execute_Procedures_IUD(dsBids);
        if(dsBids==null)
        {
            lblPOPMsg.Text ="Unable to create RFQ";
        }
        else if( dsBids.Tables[0].Rows.Count <=0)
        {
            lblPOPMsg.Text ="Unable to create RFQ";
        }
        else
        {
            ModalPopupExtender1.Visible = false; 
            Response.Redirect("CreateRFQ.aspx?PRID=" + PRId.ToString());  
        }
        //---------------------------------------------------------
    }
    protected void btnClose_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender1.Visible = false;  
    }
    protected void BtnReload_OnClick(object sender, EventArgs e)
    {
        BindRFQ();
    }
    //protected void btnApprovalPO_Click(object sender, EventArgs e)
    //{
    //    ImageButton btnPP=(ImageButton)sender;
    //    HiddenField hfBidID = (HiddenField)btnPP.Parent.FindControl("hfBidID");
    //    string BidId = hfBidID.Value;
    //    Common.Execute_Procedures_Select_ByQuery("EXEC sp_NewPR_RequestForApproval " + BidId + ",0");
    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "stt", "window.open('RFQDetailsForApproval.aspx?BidId=" + BidId + "');", true);
    //}

    // -------  Find Vendor
    protected void btnFindVendorPopup_Click(object sender, EventArgs e)
    {

        divVendorSearch.Visible = true;
    }
    protected void btnCloseSearchVendor_OnClick(object sender, EventArgs e)
    {
        divVendorSearch.Visible = false;
    }
    protected void btnSearchVendor_OnClick(object sender, EventArgs e)
    {
        TextBox txtPort = (TextBox)VSByPort.FindControl("txtVendorPort");
        TextBox txtVendorName = (TextBox)VSByName.FindControl("txtVendorName");
        TextBox txtDesc = (TextBox)vs1.FindControl("txtDesc");
        //TextBox txtVendorISSA = (TextBox)VSByISSA.FindControl("txtVendorISSA");

        HiddenField hgSelectedBidID = (HiddenField)vs1.FindControl("hfBidID");


        string WhereClause = "";
        string sql = "select SupplierID,SupplierName,SupplierPort,TRAVID,SupplierTel,SupplierEmail,Active from vw_tblSMDSuppliers  " + " Where Active=1 ";
        sql = " select SupplierID,SupplierName,SupplierPort,TRAVID,SupplierTel,SupplierEmail,Active from VW_ALL_VENDERS where Active=1  ";
        if (txtVendorName.Text.Trim() != "")
            WhereClause = WhereClause + " And SupplierName like '%" + txtVendorName.Text.Trim() + "%' ";

        if (txtDesc.Text.Trim() != "")
        {
            WhereClause = WhereClause + " And SupplierID in (Select SupplierID from vw_tblSMDPOMasterBid Where BidId in (Select Distinct BidID from VW_tblSMDPODetailBid Where BidDescription like '%" + txtDesc.Text.Trim() + "%'))";
        }
        if (txtPort.Text.Trim() != "")
            WhereClause = WhereClause + " And SupplierPort like '%" + txtPort.Text.Trim() + "%' ";
        if (ddlCountry.SelectedIndex != 0)
            WhereClause = WhereClause + " And CountryID =" + ddlCountry.SelectedValue + " ";
        if (txtCityState.Text.Trim() != "")
            WhereClause = WhereClause + " And City_State like '%" + txtCityState.Text.Trim() + "%' ";

        if (hfSelBusinessType.Value != "")
        {
            WhereClause = WhereClause + " and  (";
            string[] arrData = hfSelBusinessType.Value.ToString().Split('~');
            int indx = 0;
            foreach (string val in arrData)
            {
                //WhereClause = WhereClause + " And COMPANYBUSINESSES like '%," + val+ ",%' or SUBSTRING(COMPANYBUSINESSES,0, CHARINDEX(',',COMPANYBUSINESSES,1))='"+ val + "' or SUBSTRING(REVERSE(COMPANYBUSINESSES),0, CHARINDEX(',',REVERSE(COMPANYBUSINESSES),1))='"+ val + "' ";
                WhereClause = WhereClause + " ',' + COMPANYBUSINESSES + ',' like '%," + val + ",%' ";
                if (indx < arrData.Length - 1)
                    WhereClause = WhereClause + " or ";
                indx++;
            }
            WhereClause = WhereClause + " ) ";
        }
        sql = sql + WhereClause;

        DataTable dtVendor = Common.Execute_Procedures_Select_ByQuery(sql);
        rptSearched_VendorList.DataSource = dtVendor;
        rptSearched_VendorList.DataBind();

        if (dtVendor.Rows.Count == 0)
            lblMsgFindVendor.Text = "No Data Found.";
        else
            lblMsgFindVendor.Text = "";
    }
    protected void btnSelectVendor_OnClick(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        Session.Add("sSelectedVendorName", btn.CommandArgument);
    }
    
    # endregion
    //---------------------------
    # region ------- METHODS ----------------------
    public void BindItems()
    {
        //-------- ITEMS BINDING 
        DataTable dtPR = Common.Execute_Procedures_Select_ByQuery("select row_number() over (order by recid DESC)  as Sno,recid,[Description],[PartNo],[EquipItemDrawing],[EquipItemCode],[Qty],[UOM] from VW_tblSMDPODetail where poid=" + PRId.ToString() + " order by recid DESC");
        rptItems.DataSource = dtPR;
        rptItems.DataBind();
    }
    public void BindRFQ()
    {
        //-------- BIDS LIST BINDING
        authRFQList = new AuthenticationManager(1061, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
        string ShipId = "";
        DataTable dtPR = Common.Execute_Procedures_Select_ByQuery("select shipid from VW_tblSMDPOMaster where poid=" + PRId.ToString() + "");
        if (dtPR.Rows.Count > 0)
        {
            ShipId = dtPR.Rows[0][0].ToString();
        }
        dtPR = Common.Execute_Procedures_Select_ByQuery("select row_number() over (order by TAB.bidid) as Sno,TAB.BIDID,CONVERT(varchar, A.BidFwdOn, 23) As BidFwdOn,BIDGROUPNAME,TAB.SUPPLIERNAME,TAB.SUPPLIERPORT,BidStatusID,(SELECT COUNT(*) FROM BIDAPPROVALLIST AL WHERE AL.BIDID=TAB.BIDID) AS APPREQUESTS," +
            "(CASE WHEN BidStatusID >=0 AND BidStatusID <=2 THEN '" + ((authRFQList.IsDelete) ? "Y" : "N") + "' ELSE 'N' END) AS CanDelete,BIDSTATUSNAME, " +
            "CONVERT(varchar,BidUpdatedOn, 23) As BidUpdatedOn," +
	    "(select count(*) from dbo.vw_tblsmdpodetailbid dd where dd.bidid=TAB.bidid and isnull(pricefor,0)=0) as ZeroUPCount," +
            //"(SELECT top 1 EVENTDATE FROM tblEventHistory WHERE STATUS = 2 AND DESCR = 'Quote received from vendor.' AND REFKEY = TAB.bidid order by EVENTDATE desc) AS QuoteRecdOn," +
            "(SELECT USDTOT+ESTSHIPPINGUSD-ISNULL(TotalDiscountUSD,0) FROM dbo.VW_qUSDTotalsPerBid_SQL DET WHERE DET.BIDID=TAB.BIDID) AS AMT,APPROVECOMMENTS,BidCreatedBy,CONVERT(varchar, BidCreatedOn, 23) As BidCreatedOn,ApprovalTypeName " +
            //,coalesce((select top 1 eventdate from dbo.tbleventhistory where refkey=TAB.BIDID and status=2 order by eventdate desc),(select top 1 statusdate from dbo.tblStatusLog where bidid=TAB.BIDID and bidstatusid=2 order by statusdate desc)) as BidRecDate " +
            "from dbo.VW_qRFQListing_SQL TAB left join Add_tblSMDPOMasterBid A on TAB.BIDID=A.BIDID  left join VW_ALL_VENDERS av on av.supplierid=TAB.supplierid WHERE POID=" + PRId.ToString() + " and shipid='" + ShipId + "' order by bidgroup");
        rptRFQList.DataSource = dtPR;
        rptRFQList.DataBind();
    }
    public void BindRepeater()
    {
        BindItems();
        BindRFQ();
    }
    public void BindVendor()
    {
        string Filter = "";
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select row_number() over (order by SupplierName) as Sno,VW_tblSMDSuppliers.SupplierId,SupplierName,SupplierPort,SupplierEmail,TravId from VW_tblSMDSuppliers left join dbo.tbl_VenderRequest on tbl_VenderRequest.SupplierId=VW_tblSMDSuppliers.SUPPLIERID Where coalesce(tbl_VenderRequest.ActiveInactive,VW_tblSMDSuppliers.Active)=1 and SupplierName like '" + txtVendor.Text + "%' Order By SupplierName");
        rptVendors.DataSource = dt;
        rptVendors.DataBind();
    }
    public void BindVendorSelected()
    {
        string Filter = "";
        string VendorsList = "";
        VendorsList = String.Join(",", Vendors.ToArray());
        if (VendorsList == "") { VendorsList = "-1"; }
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select row_number() over (order by SupplierName) as Sno,SupplierId,SupplierName,SupplierPort,SupplierEmail,TravId from VW_tblSMDSuppliers Where SupplierId In (" + VendorsList + ") Order By SupplierName");
        if (dt.Rows.Count <= 0)
        {
            lblSelVendorMess.Visible = true;
        }
        else
        {
            lblSelVendorMess.Visible = false;
        }
        rptSelVendors.DataSource = dt;
        rptSelVendors.DataBind();
    }
    private bool UpdateVendorEmail(int VendorId, string Email)
    {
        //------------- DATA SAVING -------------------------------
        Common.Set_Procedures("sp_NewPR_UpdateVendorEmail");
        Common.Set_ParameterLength(2);
        Common.Set_Parameters(new MyParameter("@VendorId", VendorId), new MyParameter("@Email", Email));
        DataSet ds = new DataSet();
        return Common.Execute_Procedures_IUD(ds);
    }
    private string setValidMail(string Content)
    {
        char[] Sep = { ';' };
        string[] Mails = Content.Split(Sep);
        List<string> RetMails = new List<string>();
        foreach (string address in Mails)
        {
            try
            {
                MailAddress ma = new MailAddress(address);
                RetMails.Add(address);
            }
            catch { }
        }
        return string.Join(";", RetMails.ToArray());
    }
    # endregion
    //---------------------------
    protected void bindPayment_ForwardToddl()
    {
        string SQL = "select (FirstName + ' ' + LastName ) AS UserName, LoginId  from dbo.usermaster where loginid in (select userid from pos_invoice_mgmt where payment=1) AND statusId='A' Order By UserName";

        DataTable dt1;//= Common.Execute_Procedures_Select_ByQuery("");
        //this.ddlPaymentForwardTo.DataValueField = "LoginId";
        //this.ddlPaymentForwardTo.DataTextField = "UserName";
        //this.ddlPaymentForwardTo.DataSource = dt1;
        //this.ddlPaymentForwardTo.DataBind();
        //ddlPaymentForwardTo.Items.Insert(0, new ListItem("< Select User >", "0"));

        SQL = "select (FirstName + ' ' + LastName ) AS UserName, LoginId from dbo.usermaster where loginid in (select userid from pos_invoice_mgmt where Approval=1) AND statusId='A' Order By UserName";
        dt1 = Common.Execute_Procedures_Select_ByQuery(SQL);
        this.ddlVerifyForwardTo.DataValueField = "LoginId";
        this.ddlVerifyForwardTo.DataTextField = "UserName";
        this.ddlVerifyForwardTo.DataSource = dt1;
        this.ddlVerifyForwardTo.DataBind();
        ddlVerifyForwardTo.Items.Insert(0, new ListItem("< Select User >", "0"));

        SQL = "select (FirstName + ' ' + LastName ) AS UserName, LoginId from dbo.usermaster where loginid in (select userid from pos_invoice_mgmt where Verification=1) AND statusId='A' Order By UserName";
        dt1 = Common.Execute_Procedures_Select_ByQuery(SQL);
        this.ddlApproval2.DataValueField = "LoginId";
        this.ddlApproval2.DataTextField = "UserName";
        this.ddlApproval2.DataSource = dt1;
        this.ddlApproval2.DataBind();
        ddlApproval2.Items.Insert(0, new ListItem("< Select User >", "0"));

        SQL = "SELECT FirstName + ' ' + MiddleName + ' ' + FamilyName AS EmpName,USERID FROM DBO.Hr_PersonalDetails WHERE POSITION IN(4,89)";
        dt1 = Common.Execute_Procedures_Select_ByQuery(SQL);
        this.ddlApproval3.DataValueField = "USERID";
        this.ddlApproval3.DataTextField = "EmpName";
        this.ddlApproval3.DataSource = dt1;
        this.ddlApproval3.DataBind();
        ddlApproval3.Items.Insert(0, new ListItem("< Select User >", "0"));

        SQL = "SELECT FirstName + ' ' + MiddleName + ' ' + FamilyName AS EmpName,USERID FROM DBO.Hr_PersonalDetails WHERE POSITION=1";
        dt1 = Common.Execute_Procedures_Select_ByQuery(SQL);
        this.ddlApproval4.DataValueField = "USERID";
        this.ddlApproval4.DataTextField = "EmpName";
        this.ddlApproval4.DataSource = dt1;
        this.ddlApproval4.DataBind();
        ddlApproval4.Items.Insert(0, new ListItem("< Select User >", "0"));

        //SQL = "select (FirstName + ' ' + LastName ) AS UserName, LoginId from dbo.usermaster where loginid in (select userid from pos_invoice_mgmt where Payment=1) AND statusId='A' Order By UserName";
        //dt1 = Common.Execute_Procedures_Select_ByQuery(SQL);
        //this.ddlAccountUser.DataValueField = "LoginId";
        //this.ddlAccountUser.DataTextField = "UserName";
        //this.ddlAccountUser.DataSource = dt1;
        //this.ddlAccountUser.DataBind();
        //ddlAccountUser.Items.Insert(0, new ListItem("< Select User >", "0"));
        //DataTable dtAccountUser = Common.Execute_Procedures_Select_ByQuery("exec DBO.POS_INV_getPaymentUser " + InvoiceId);
        //ddlAccountUser.SelectedValue = dtAccountUser.Rows[0][0].ToString();
    }
    protected void btn_AppSave_Click(object sender, EventArgs e)
    {
        if (txtpComments.Text.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alet", "alert('Please enter purchaser comments to send for approval.');", true);
            return;
        }
        try
        {
            bool success = false;
            DropDownList[] ctlApprovals = { ddlVerifyForwardTo, ddlApproval2, ddlApproval3, ddlApproval4 };
            decimal ApprovalAmount = Common.CastAsDecimal(Sel_BidAmount);
            
            success = true;

            //int MaxApproval = 1;
            //if (ApprovalAmount < 500)
            //{ MaxApproval = 1; }
            //else if (ApprovalAmount < 25000)
            //{ MaxApproval = 2; }
            //else if (ApprovalAmount < 100000)
            //{ MaxApproval = 3; }
            //else
            //{ MaxApproval = 4; }

            //for (int i = 1; i <= MaxApproval; i++)
            //{
            //    success = success && ((DropDownList)ctlApprovals[i - 1]).SelectedIndex > 0;
            //}

            

            if (success)
            {
                //int App1 = Common.CastAsInt32(ddlVerifyForwardTo.SelectedValue);
                //int App2 = Common.CastAsInt32(ddlApproval2.SelectedValue);
                //int App3 = Common.CastAsInt32(ddlApproval3.SelectedValue);
                //int App4 = Common.CastAsInt32(ddlApproval4.SelectedValue);
                //int MaxAppUser = (App4 > 0) ? App4 : ((App3 > 0) ? App3 : ((App2 > 0) ? App2 : App1));

                int App1 = 0;
                int App2 = 0;
                int App3 = 0;
                int App4 = 0;

                //Common.Execute_Procedures_Select_ByQuery("EXEC dbo.sp_ValidateApprovalStages " + Sel_BidId+ "," + App1 + "," + App2 + "," + App3 + "," + App4+",0,"+Session["loginid"].ToString());
                Common.Execute_Procedures_Select_ByQuery("EXEC dbo.sp_ValidateApprovalStages " + Sel_BidId + "," + App1 + "," + App2 + "," + App3 + "," + App4 + ",0," + Session["loginid"].ToString() + ",'" + txtpComments.Text.Trim().Replace("'", "`") + "'");
                btn_AppSave.Visible = false;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alet", "alert('PO send for approval successfully.');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alet", "alert('Please select approval person to continue.');", true);
            }

        }
        catch (Exception ex)
        { lblMsgPOApprovalRequest.Text = "Unable to send approval request. Error :" + ex.Message; }
    }

    //------------------------------------------
    public void BindCountry()
    {
        string sql = " select CountryID,CountryName from dbo.country where StatusID='A' order by CountryName  ";
        DataTable dtPR = Common.Execute_Procedures_Select_ByQuery(sql);
        ddlCountry.DataSource = dtPR;
        ddlCountry.DataTextField = "CountryName";
        ddlCountry.DataValueField = "CountryID";
        ddlCountry.DataBind();
        ddlCountry.Items.Insert(0, new ListItem("Select", ""));
    }
    public void BindBusinessType()
    {
        DataTable dtCountry = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.COUNTRY ORDER BY COUNTRYNAME");
        DataTable dtVendorList = Common.Execute_Procedures_Select_ByQuery("SELECT * FROM DBO.tblVendorBusinessesList ORDER BY Vendorlistname");
        chkVendorbusinesseslist.DataSource = dtVendorList;
        chkVendorbusinesseslist.DataTextField = "Vendorlistname";
        chkVendorbusinesseslist.DataValueField = "Vendorlistid";
        chkVendorbusinesseslist.DataBind();
    }
    protected void lnkOpenBusinessType_OnClick(object sender, EventArgs e)
    {
        divAddBusinessType.Visible = true;
        chkVendorbusinesseslist.ClearSelection();
        foreach (ListItem itm in chkVendorbusinesseslist.Items)
        {
            foreach (string val in hfSelBusinessType.Value.ToString().Split('~'))
            {
                if (itm.Value == val)
                {
                    itm.Selected = true;
                    break;
                }
            }
        }

    }
    protected void btnCloseAddBusinessTypePopup_OnClick(object sender, EventArgs e)
    {
        divAddBusinessType.Visible = false;
    }
    protected void btnAddBusinessType_OnClick(object sender, EventArgs e)
    {
        lblBusinessType.Text = "";
        hfSelBusinessType.Value = "";

        foreach (ListItem itm in chkVendorbusinesseslist.Items)
        {
            if (itm.Selected)
            {
                lblBusinessType.Text = lblBusinessType.Text + ", " + itm.Text;
                hfSelBusinessType.Value = hfSelBusinessType.Value + "~" + itm.Value;
            }
        }
        if (lblBusinessType.Text.Trim() != "")
        {
            lblBusinessType.Text = lblBusinessType.Text.Substring(1);
            hfSelBusinessType.Value = hfSelBusinessType.Value.Substring(1);
        }
    }
    protected void btnCleaerBusinessType_OnClick(object sender, EventArgs e)
    {
        chkVendorbusinesseslist.ClearSelection();
        hfSelBusinessType.Value = "";
        lblBusinessType.Text = "";
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
        if (PRId > 0)
        {
            sql = "SELECT DocId, DocName As FileName, PoId As RequisitionId, VesselCode, (Select top 1 StatusID from tblSMDPOMaster p where p.PoId= Pod.PoId) As StatusId FROM [tblSMDPODocuments] Pod with(nolock) WHERE  Pod.PoId =" + PRId;
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
            if (PRId > 0)
            {
                sql = "Delete from MP_VSL_StoreReqDocument  WHERE PoId =" + PRId + " AND DocId = " + DocId;
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
