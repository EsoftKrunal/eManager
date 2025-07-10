using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Web.UI.WebControls.WebParts;

public partial class Modules_Purchase_Invoice_InvoiceEntry : System.Web.UI.Page
{
    AuthenticationManager authRecInv;
    private DataTable _dtInvoices;
    public DataTable dtInvoices
    {
        set
        {
            _dtInvoices = value;
            ViewState["InvoiceList"] = _dtInvoices;  
        }
        get 
        {
            if (ViewState["InvoiceList"] == null)
            {
                _dtInvoices = CreateStructure();
                ViewState["InvoiceList"] = _dtInvoices; 
            }
            return (DataTable)ViewState["InvoiceList"];
        }
    }
    public int BidID
    {
        get { return Convert.ToInt32(ViewState["BidID"]); }
        set { ViewState["BidID"] = value; }
    }
    public int PoID
    {
        get { return Convert.ToInt32(ViewState["PoID"]); }
        set { ViewState["PoID"] = value; }
    }
    private DataTable CreateStructure()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("InvoiceId", typeof(Int32));
        dt.Columns.Add("InvoiceNo", typeof(String));
        dt.Columns.Add("Currency", typeof(String));
        dt.Columns.Add("RefNo", typeof(String));
        dt.Columns.Add("Amount", typeof(Double));
        dt.Columns.Add("Color", typeof(String)); 
        return dt;
    }

    public string callingPage
    {
        get { return ViewState["callingPage"].ToString(); }
        set { ViewState["callingPage"] = value; }
    }

    public int InvoiceId
    {
        get { return Convert.ToInt32(ViewState["InvoiceId"]); }
        set { ViewState["InvoiceId"] = value; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        #region --------- USER RIGHTS MANAGEMENT -----------
        try
        {
            authRecInv = new AuthenticationManager(1064, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
            if (!(authRecInv.IsView))
            {
                Response.Redirect("~/AuthorityError.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/NoPermission.aspx?Message=" + ex.Message);
        }

        #endregion ----------------------------------------

        if (Page.Request.QueryString["BidId"] != null)
        {
            BidID = Common.CastAsInt32(Page.Request.QueryString["BidID"]);
        }
        callingPage = "";
        if (Page.Request.QueryString["Page"] != null)
        {
            callingPage = Page.Request.QueryString["Page"].ToString();
        }
        if (Page.Request.QueryString["InvoiceId"] != null)
        {
            InvoiceId = Convert.ToInt32(Page.Request.QueryString["InvoiceId"]);
        }
        lblmsg.Text = "";
        if (!Page.IsPostBack)
        {
           // txtCreditFor.Attributes.Add("onchange", "checkChange(this);");
            imgPOSVendor.Visible = int.Parse(Session["loginid"].ToString())==1;
            dtInvoices.Clear();
            ShowMasterData();
            BindRptinvoiceEntry();
            DataTable DtInvPOS = Common.Execute_Procedures_Select_ByQuery("select * FROM tblBidInvoices where BidId="+BidID+" order by invoiceid");
            string strInvID = "";

            
            if (BidID > 0)
            {
                int CountExported = 0;
                DataTable dtBidExported = Common.Execute_Procedures_Select_ByQuery("select * FROM dbo.TBLAPENTRIES where BidId=" + BidID + " and inTrav=1");
                CountExported = dtBidExported.Rows.Count;

                DataTable DtBidStatusID = Common.Execute_Procedures_Select_ByQuery("select BidStatusID,poid from vw_tblsmdpomasterBid where BidID=" + BidID.ToString() + "");
                int BidStatusID = 0;
                if (DtBidStatusID != null)
                {
                    if (DtBidStatusID.Rows.Count > 0)
                    {
                        imgSave.Visible = authRecInv.IsUpdate && CountExported<=0;
                        BidStatusID = Common.CastAsInt32(DtBidStatusID.Rows[0][0]);
                        PoID = Common.CastAsInt32(DtBidStatusID.Rows[0][1]);
                    }
                }
                DataTable dtinv = Common.Execute_Procedures_Select_ByQuery("select InvNo,Convert(Varchar(10),InvDate,101) As InvDate from pos_invoice where invoiceid in (select invoiceid from POS_Invoice_Payment_PO where bidid = " + BidID + ") and ISNULL(IsAdvPayment,0) = 0  ");
                if (dtinv.Rows.Count > 0)
                {
                    txtInvNo.Text = dtinv.Rows[0]["InvNo"].ToString();
                   DateTime dt_val = DateTime.ParseExact(dtinv.Rows[0]["InvDate"].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    txtInvDate.Text = String.Format("{0:dd-MMM-yyyy}", dt_val);
                    txtInvNo.ReadOnly = true;
                    txtInvDate.ReadOnly = true;

                }

            }

            if (DtInvPOS != null)
            {
                if (DtInvPOS.Rows.Count > 0)
                {
                    foreach (DataRow dr in DtInvPOS.Rows)
                    {
                        strInvID = strInvID +","+ dr["InvoiceId"].ToString();
                    }
                    if (DtInvPOS.Rows.Count > 0)
                    {
                        strInvID = strInvID.Substring(1);
                    }


                   DataTable dtData = Common.Execute_Procedures_Select_ByQueryCMS("select InvoiceId,VESSELID, RefNo,(select Company from (select PortAgentId as AgentId,Company from portagent union select TravelAgentid as AgentId,Company from travelagent union select SupplierId as AgentId,Company from SupplierMaster) a where a.Agentid=Vendorid) as  VendorId, " +
                                                                   "round(convert(float,InvoiceAmount),2) as InvoiceAmount, " +
                                                                   "InvNo, " +
                                                                   "replace(convert(varchar,InvDate,106),' ','-') as Invdate, " +
                                                                   "replace(convert(varchar,DueDate,106),' ','-') as Duedate, " +
                                                                   "(select PoNo from POHeader where POHeader.PoId=Invoice.POId) AS PoNo, " +
                                                                   "(select sum(isnull(amountUSD,0)) from PODetails where PODetails.PoId=Invoice.POId) AS PoAmount, " +
                                                                   " GST, InvoiceAmount + GST as TotalInvoiceAmount, " +
                                                                   "(select CurrencyName from Currency where Currency.CurrencyId=invoice.Currencyid) as CurrencyId " +
                                                                   " from invoice " +
                                                                   " where InvoiceId in (" + strInvID + ") order by invoiceid");
                    if (dtData != null)
                    {
                        foreach (DataRow dr in  dtData.Rows)
                        {
                            dtInvoices.Rows.Add(dtInvoices.NewRow());
                            dtInvoices.Rows[dtInvoices.Rows.Count - 1]["InvoiceId"] = dr["InvoiceId"];
                            dtInvoices.Rows[dtInvoices.Rows.Count - 1]["InvoiceNo"] = dr["InvNo"];
                            dtInvoices.Rows[dtInvoices.Rows.Count - 1]["Currency"] = dr["CurrencyId"];
                            dtInvoices.Rows[dtInvoices.Rows.Count - 1]["RefNo"] = dr["RefNo"];
                            dtInvoices.Rows[dtInvoices.Rows.Count - 1]["Amount"] = dr["InvoiceAmount"];
                            dtInvoices.Rows[dtInvoices.Rows.Count - 1]["Color"] =(DtInvPOS.Rows[dtInvoices.Rows.Count - 1]["Complete"].ToString()=="True")?"Green":"White";
                        }
                        BindInvoice();
                    }
                }
            }
            GetDocCount(PoID);
        }

       
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvscroll_Invoice');", true);
    }
    // Event ---------------------------------------------------
    protected void imgSave_OnClick(object sender, EventArgs e)
    {
      
        if (hfConfirm.Value != "True")
        {
            return;
        }
        //if (txtInvNo.SelectedValue == "")
        if (txtInvNo.Text == "")
        {
            lblmsg.Text = "Invoice Number Required";
            txtInvNo.Focus();
            return;
        }
        //Regex r = new Regex(@"^([# \w\\/-]+)$");
        //if (!r.IsMatch(txtInvNo.Text.Trim()))
        //{
        //    lblmsg.Text = @"Invalid Invoice Number. Only letters,digits,\,/,- are allowed.";
        //    txtInvNo.Focus();
        //    return;
        //}
        //if (grdInvoiceList.Rows.Count == 0)
        //{
        //    lblmsg.Text = "Please add the selected invoice.";
        //    return;
        //}
        if (txtInvDate.Text.Trim() == "")
        {
            lblmsg.Text = "Invoice Date Required";
            txtInvDate.Focus();
            return;
        }
        if (Convert.ToDateTime(txtInvDate.Text) > System.DateTime.Now)
        {
            lblmsg.Text = "Invoice Date should not be greater than current date.";
            return;
        }
        if (hfdRating.Value == "")
        {
            lblmsg.Text = "Provide ratings and comments for this supply.";
            return;
        }
        if (txtSupplierComments.Text.Trim() == "")
        {
            lblmsg.Text = "Provide ratings and comments for this supply.";
            return;
        }
        else
        {
            if(lblmsg.Text.Length>1000)
            {
                lblmsg.Text = "Max comment size is 1000 char(s).";
                return;
            }
        }

        //string sql_1 = "select BidSmdLevel1ApprovalDate,BidstatusDate from [DBO].tblsmdpomasterbid where bidid=" + BidID.ToString() + "";
        //DataTable dtInvDateCheck = Common.Execute_Procedures_Select_ByQuery(sql_1);
        //if (dtInvDateCheck.Rows.Count > 0)
        //{
        //    try
        //    {
        //        DateTime dt_1 =Convert.ToDateTime(dtInvDateCheck.Rows[0][0].ToString());
        //        dt_1 = Convert.ToDateTime(dt_1.ToString("dd-MMM-yyyy"));
        //        DateTime dtInvDate = Convert.ToDateTime(txtInvDate.Text);
        //        if (dtInvDate < dt_1)
        //        {
        //            lblmsg.Text = "Invoice Date must me more than PO approval date.";
        //            return;
        //        }
        //    }
        //    catch { } 

        //    try
        //    {
        //        DateTime dt_2 =Convert.ToDateTime(dtInvDateCheck.Rows[0][1].ToString());
        //        dt_2 = Convert.ToDateTime(dt_2.ToString("dd-MMM-yyyy"));
        //        DateTime dtInvDate = Convert.ToDateTime(txtInvDate.Text);
        //        if (dtInvDate < dt_2)
        //        {
        //            lblmsg.Text = "Invoice Date must me more than PO approval date.";
        //            return;
        //        }
        //    }
        //    catch { } 
        //}

        //return;

        //if (Common .CastAsDecimal(txtApproveAmount.Text)<=0)
        //{
        //    lblmsg.Text = "Please enter approve amount.";
        //    txtApproveAmount.Focus();
        //    return;
        //}

        //if (Common.CastAsDecimal(txtApproveAmount.Text) != Common.CastAsDecimal(lblLCActGrand.Text))
        //{
        //    if (txtInvComm.Text.Trim() == "")
        //    {
        //        lblmsg.Text = "You must enter a variance comment before completing this invoice.";
        //        txtInvComm.Focus();
        //        return;
        //    }
        //}
        DataTable dtpo = Common.Execute_Procedures_Select_ByQuery("select SupplierID, BidCurr from vw_tblsmdpomasterbid where bidid = " + BidID);
        DataTable dtinv = Common.Execute_Procedures_Select_ByQuery("select SupplierID,Currency,InvoiceAmount,ApprovalAmount,Stage from pos_invoice where invoiceid in (select invoiceid from POS_Invoice_Payment_PO where bidid = " + BidID + ") and ISNULL(IsAdvPayment,0) = 0 ");

        DataTable dtCreditNote = Common.Execute_Procedures_Select_ByQuery("Select Sum(ISNULL(CreditNoteAmount,0)) As CreditNoteAmount, CreditNoteCurrency   from InvoiceCreditNotesDetails with(nolock) where InvoiceId in  (select invoiceid from POS_Invoice_Payment_PO where bidid = " + BidID + ") group by CreditNoteCurrency ");

        int pos_supplierid=0, inv_supplierid=0;
        string po_curr="", inv_curr="", credit_curr = "";
        decimal po_amt, inv_amt=0, inv_appamt=0, credit_amt = 0;

        decimal pocreditnoteAmount = 0;
        if (! string.IsNullOrEmpty(txtCreditFor.Text))
        {
            pocreditnoteAmount = Convert.ToDecimal(txtCreditFor.Text);
        }
        po_amt = Common.CastAsDecimal(lblLCActGrand.Text);
        int stage = 0;
        if (dtpo.Rows.Count > 0)
        {
            pos_supplierid = Common.CastAsInt32(dtpo.Rows[0]["SupplierID"]);
            po_curr = dtpo.Rows[0]["BidCurr"].ToString();
        }
        if (dtinv.Rows.Count > 0)
        {
            inv_supplierid = Common.CastAsInt32(dtinv.Rows[0]["SupplierID"]);
            inv_curr = dtinv.Rows[0]["Currency"].ToString();
            inv_amt = Common.CastAsDecimal(dtinv.Rows[0]["InvoiceAmount"]);
            inv_appamt = Common.CastAsDecimal(dtinv.Rows[0]["ApprovalAmount"]);
            stage = Common.CastAsInt32(dtinv.Rows[0]["Stage"]);
        }
        if (dtCreditNote.Rows.Count > 0)
        {
            credit_curr = dtCreditNote.Rows[0]["CreditNoteCurrency"].ToString();
            credit_amt = Common.CastAsDecimal(dtCreditNote.Rows[0]["CreditNoteAmount"]);
            inv_amt = inv_amt - credit_amt;
        }
        //----------------------
        if (pos_supplierid!=inv_supplierid)
        {
            lblmsg.Text = "PO & linked invoice supplier is not matching.";
            return;
        }
        else if (po_curr != inv_curr)
        {
            lblmsg.Text = "PO & linked invoice currency is not matching.";
            return;
        }
        else if ((pocreditnoteAmount > 0 || credit_amt > 0) && credit_curr == "")
        {
            lblmsg.Text = "No Credit note found in the Invoice !";
            return;
        }
        else if ((pocreditnoteAmount > 0 || credit_amt > 0) && po_curr != credit_curr)
        {
            lblmsg.Text = "PO & Credit note currency is not matching.";
            return;
        }
        else if ((pocreditnoteAmount > 0 || credit_amt > 0) &&  pocreditnoteAmount != -(credit_amt) ) 
        {
            lblmsg.Text = "Credit Amount is not matching with Uploaded Credit note amount.";
            return;
        }
        else if (po_amt != inv_amt)
        {
            lblmsg.Text = "PO amount & invoice amount is not matching.";
            return;
        }
       
        else if (string.IsNullOrEmpty(txtInvComm.Text) && (! string.IsNullOrWhiteSpace(txtCreditFor.Text) && Common.CastAsDecimal(txtCreditFor.Text) < 0))
        {
            txtInvComm.Focus();
            lblmsg.Text = "Please enter Remark for Credit Amount.";
            return;
        }
        //----------------------
        //if (stage!=3)
        //{
        //    lblmsg.Text = "Invoice must be approved in order to save record.";
        //    return;
        //}

        string BidItemID = "", QtyInv = "", InvProceFor = ""; ;

        // Invoice Entry
        string strInvNo = "", strCheck = "";
        foreach (GridViewRow GrdInv in grdInvoiceList.Rows )
        {
            string InvNo = GrdInv.Cells[1].Text;
            CheckBox chkClear = (CheckBox)GrdInv.FindControl("chkClear");
            HiddenField hfInvoicID = (HiddenField)GrdInv.FindControl("hfInvoicID");
            strInvNo = strInvNo + "," + hfInvoicID.Value;
            if (chkClear.Checked)
            {
                strCheck = strCheck +","+ 1;
            }
            else
            {
                strCheck = strCheck + "," + 0;
            }
            
        }

        if (grdInvoiceList.Rows.Count > 0)
        {
            strInvNo = strInvNo.Substring(1);
            strCheck = strCheck.Substring(1);
            
        }

        foreach (RepeaterItem rptItm in RptinvoiceEntry.Items)
        {
            HiddenField hfBidItmID = (HiddenField)rptItm.FindControl("hfBidItmID" );

            TextBox txtQtyInv = (TextBox)rptItm.FindControl("txtQtyInv");
            TextBox txtInvPriceFor = (TextBox)rptItm.FindControl("txtInvPriceFor");
            BidItemID = BidItemID + "," + hfBidItmID.Value;
            QtyInv = QtyInv + "," + txtQtyInv.Text;
            InvProceFor = InvProceFor + "," + txtInvPriceFor.Text;
        }
        if (BidItemID != "")
        {
            BidItemID = BidItemID.Substring(1);
            QtyInv = QtyInv.Substring(1);
            InvProceFor = InvProceFor.Substring(1);
        }
       // if (Common.CastAsDecimal(lblQtyOrder.Text) > Common.CastAsDecimal(QtyInv.Text))
        {
           // ASK USER FOR CREATE BACK ORDER.
            //Sub CopyBid(in_bidid)
            //   Dim db As DAO.Database
            //   Dim rs1 As DAO.Recordset
            //   Dim rs2 As DAO.Recordset
            //   Dim copyPONum As Boolean

            //   Set db = CurrentDb

            //   Set rs1 = db.OpenRecordset("Select * from tblSMDPOMasterBid where bidid=" & in_bidid, DB_OPEN_DYNASET, dbSeeChanges)
            //   If Not rs1.EOF Then
            //       in_poid = rs1("poid")
            //       Set rs2 = db.OpenRecordset("Select * from tblSMDPOMasterBid where bidid=0", DB_OPEN_DYNASET, dbSeeChanges)
            //       rs2.AddNew
            //       For i = 1 To rs2.Fields.count - 1
            //           rs2(i) = rs1(i)
            //       Next i

            //       copyPONum = False
            //       If Not IsNull(rs2("BidPONum")) Then
            //           copyPONum = True

            //           'added to handle backorders of invoices items
            //           rs2("BidReceivedDate") = Null
            //           rs2("paid") = 0
            //           rs2("InvoiceNo") = Null
            //           rs2("BidInvoiceDate") = Null
            //           rs2("InvCompleteDate") = Null
            //           rs2("ActShippingFor") = 0
            //           rs2("CreditFor") = 0
            //           rs2("varComments") = "Backorder from " & rs2("BidPONum")

            //           rs2("OrderType") = "B"

            //           If rs2("BidStatusID") > 3 Then
            //               rs2("BidStatusID") = 3
            //           End If
            //       End If

            //       rs2.Update
            //       newbid = DMax("bidid", "tblSMDPOMasterBid", "poid=" & in_poid)
            //   End If
            //   rs1.Close
            //   rs2.Close

            //   Set rs1 = db.OpenRecordset("Select * from tblSMDPODetailBid where bidid=" & in_bidid, DB_OPEN_DYNASET, dbSeeChanges)
            //   If Not rs1.EOF Then
            //       Set rs2 = db.OpenRecordset("Select * from tblSMDPODetailBid where bidid=0", DB_OPEN_DYNASET, dbSeeChanges)
            //       While Not rs1.EOF
            //           rs2.AddNew
            //               For i = 1 To rs2.Fields.count - 1
            //                   rs2(i) = rs1(i)
            //               Next i
            //               rs2("bidid") = newbid
            //               rs2("qtyPO") = Nz(rs2("qtyPO"), 0) - Nz(rs2("qtyRecd"), 0)
            //               rs2("qtyRecd") = 0
            //               rs2("qtyInv") = 0
            //           rs2.Update
            //           rs1.MoveNext
            //       Wend
            //   End If
            //   rs1.Close
            //   rs2.Close

            //   //'update bidgroup
            //   //'get the next bid group
            //   Set rs1 = db.OpenRecordset("Select BidGroup from tblSMDPOMasterBid where poid=" & in_poid & " order by bidGroup", DB_OPEN_DYNASET, dbSeeChanges)
            //   If rs1.EOF Then
            //       BidGroup = "A"
            //   Else
            //       rs1.MoveLast
            //       If IsNull(rs1("BidGroup")) Then
            //           BidGroup = "A"
            //       Else
            //           BidGroup = Chr(Asc(Left(rs1("BidGroup"), 1)) + 1)
            //       End If
            //   End If
            //   rs1.Close

            //   DoCmd.SetWarnings False
            //   DoCmd.RunSQL "update tblSMDPOMasterBid set BiDgroup='" & BidGroup & "1' where bidid=" & newbid
            //   If copyPONum = True Then
            //       DoCmd.RunSQL "update tblSMDPOMasterBid set BidPONum='" & PONumCalc(in_poid) & "-" & BidGroup & "1' where bidid=" & newbid
            //   End If
            //   DoCmd.SetWarnings True
            //End Sub
        }
        Common.Set_Procedures("sp_NewPR_UpdateInvoiceQty");
        Common.Set_ParameterLength(14);
        Common.Set_Parameters(
            new MyParameter("@BidID", BidID),
            new MyParameter("@AddInvoiceId", "0"),
            new MyParameter("@InvoiceNo", txtInvNo.Text.Trim()),
            new MyParameter("@BidInvoiceDate", txtInvDate.Text.Trim()),
            new MyParameter("@varComments", txtInvComm.Text.Trim()),
            new MyParameter("@BidItemID", BidItemID),
            new MyParameter("@QtyInv", QtyInv),
            new MyParameter("@actSHIPPINGfOR", txtACTSshippingFor.Text.Trim()),
            new MyParameter("@CREDITFOR", txtCreditFor.Text.Trim()),
            new MyParameter("@InvProceFor", InvProceFor),
            new MyParameter("@ApproveAmount", txtApproveAmount.Text.Trim()),
            new MyParameter("@InvoiceNos", strInvNo),
            new MyParameter("@IsCheckBoxChecked", strCheck),
            new MyParameter("@CreateBackOrder", hfCreatBackOrder.Value)
            );
        DataSet dsPrType = new DataSet();
        Boolean res;
        res = Common.Execute_Procedures_IUD(dsPrType);
        if (res)
        {
            string Rating = "";
            if (hfdRating.Value == "")
                Rating = "NULL";
            else
                Rating = Common.CastAsInt32(hfdRating.Value).ToString();

            Common.Execute_Procedures_Select_ByQuery("EXEC DBO.Update_BidSupplerComments " + BidID + "," + Rating + ",'" + txtSupplierComments.Text.Trim().Replace("'", "`") + "','" + Session["UserFullName"].ToString() + "'");

            if (callingPage != "" && callingPage == "INV" && InvoiceId > 0)
            {
                string RefreshPage = "ViewInvoice.aspx?InvoiceId=" + InvoiceId.ToString() + "";
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Refresh Page", "window.open('" + RefreshPage + "', '');", false);
             
            }

            lblmsg.Text = "Record saved successfully.";
            imgSave.Visible = false;
            //------------- SECTION TO UPDATE DATA IN CMS APPLICATION ----------------------------
            //foreach (GridViewRow Gr in grdInvoiceList.Rows)
            //{
            //    Label lblApproveAmount = (Label)Gr.FindControl("lblAmount");
            //    HiddenField hfInvoicID = (HiddenField)Gr.FindControl("hfInvoicID");
            //    CheckBox chkClear = (CheckBox)Gr.FindControl("chkClear");

            //    if (chkClear.Checked)
            //    {
            //        string sql = "dbo.UpdateInvoiceApprovalDetalis_pos " + hfInvoicID.Value + "," + BidID.ToString() + "," + lblApproveAmount.Text + ",'" + txtInvComm.Text + "'," + Session["loginid"].ToString() + "";
            //        DataTable DtRes = Common.Execute_Procedures_Select_ByQueryCMS(sql);
            //        //txtInvNo.AddId = txtInvNo.SelectedId;
            //        //txtInvNo.AddText = txtInvNo.SelectedValue;
            //    }
            //}
        }
        else
        {
            lblmsg.Text = "Record could not be saved. "+Common.ErrMsg;
        }
    }
    protected void btnDelInv_Click(object sender, EventArgs e)
    {
        ImageButton btnDel = (ImageButton)sender;
        int InvoiceID = Common.CastAsInt32( btnDel.CommandArgument);
        DataRow[] dr = dtInvoices.Select("InvoiceID="+InvoiceID);
        if (dr != null)
        {
            if (dr.Length > 0)
            {
                dtInvoices.Rows.Remove(dr[0]);
                BindInvoice();
            }
        }
    }
    // TextBox ---------------------------------------------------
    protected void txtQtyInv_OnTextChanged(object sender, EventArgs e)
    {
        TextBox txtQtyInv = (TextBox)sender;
        TextBox txtInvPriceFor = (TextBox)txtQtyInv.Parent.FindControl("txtInvPriceFor");
        Label lblInvForTotal = (Label)txtQtyInv.Parent.FindControl("lblInvForTotal");
        Label lblInvUSDTotal = (Label)txtQtyInv.Parent.FindControl("lblInvUSDTotal");

        lblInvForTotal.Text = Math.Round(Common.CastAsDecimal(Common.CastAsDecimal(txtQtyInv.Text) * Common.CastAsDecimal(txtInvPriceFor.Text)), 2).ToString();
        lblInvUSDTotal.Text = Math.Round(Common.CastAsDecimal((Common.CastAsDecimal(Common.CastAsDecimal(txtQtyInv.Text) * Common.CastAsDecimal(txtInvPriceFor.Text))) / Common.CastAsDecimal(lblExchRate.Text)), 2).ToString();
        SetTotalItems();
    }
    protected void txtCreditFor_OnTextChanged(object sender, EventArgs e)
    {
        //if (!string.IsNullOrWhiteSpace(txtCreditFor.Text) && Common.CastAsDecimal(txtCreditFor.Text) >= 0)
        //{
        //    //ScriptManager.RegisterStartupScript(this,this.GetType(), "alert", "alert('Please enter credit amount with Minus (-). i.e. -1234.00.!')", true);
        //    //string alertMsg = "alert('Please enter credit amount with Minus (-). i.e. -1234.00.')";
        //    //ScriptManager.RegisterClientScriptBlock(tryc(sender, Control), Me.GetType(), "alert", alertMsg, true);
        //    lblmsg.Text = "Please enter credit amount with Minus (-). i.e. -1234.00.";
        //    txtCreditFor.Text = "";
        //    txtCreditFor.Focus();
        //    return;
        //}

        lblCreditUSD.Text = Math.Round(Common.CastAsDecimal((Common.CastAsDecimal(txtCreditFor.Text) / Common.CastAsDecimal(lblExchRate.Text))), 2).ToString();
        SetTotalItems();
    }    
    protected void txtACTSshippingFor_OnTextChanged(object sender, EventArgs e)
    {
        lblACTshippingUSD.Text =Math.Round( Common.CastAsDecimal((Common.CastAsDecimal(txtACTSshippingFor.Text) / Common.CastAsDecimal(lblExchRate.Text))),2).ToString();
        SetTotalItems();
    }
    protected void lblInvPriceFor_OnTextChanged(object sender,EventArgs e)
    {
        //TextBox txtQtyInv = (TextBox)sender;
        TextBox txtInvPriceFor  = (TextBox)sender;

        TextBox txtQtyInv = (TextBox)txtInvPriceFor.Parent.FindControl("txtQtyInv");
        Label lblInvForTotal = (Label)txtInvPriceFor.Parent.FindControl("lblInvForTotal");
        Label lblInvUSDTotal = (Label)txtInvPriceFor.Parent.FindControl("lblInvUSDTotal");

        lblInvForTotal.Text = Math.Round(Common.CastAsDecimal(Common.CastAsInt32(txtQtyInv.Text) * Common.CastAsDecimal(txtInvPriceFor.Text)), 2).ToString();
        lblInvUSDTotal.Text = Math.Round(Common.CastAsDecimal((Common.CastAsDecimal(Common.CastAsInt32(txtQtyInv.Text) * Common.CastAsDecimal(txtInvPriceFor.Text))) / Common.CastAsDecimal(lblExchRate.Text)), 2).ToString();
        SetTotalItems();
    }
    // Function ------------------------------------------------
    public void BindRptinvoiceEntry()
    {
        string sql = "SELECT  row_number() over(order by vw_tblSMDPODetail.Recid desc)as Row, vw_tblSMDPODetailBid.*, vw_tblSMDPOMasterBid.BidPoNum, vw_tblSMDPODetail.EquipItemDrawing, " +
                    "vw_tblSMDPODetail.EquipItemCode,vw_tblSMDPODetail.partNo, vw_tblSMDPOMasterBid.InvoiceNo, vw_tblSMDPOMasterBid.BidInvoiceDate, vw_tblSMDPOMasterBid.BidCurrInv, " + 
                    "vw_tblSMDPOMasterBid.BidExchRateInv, vw_tblSMDPOMasterBid.BidExchDateInv, vw_tblSMDPOMasterBid.BidCurr, vw_tblSMDPOMasterBid.BidExchRate, "+ 
                    "vw_tblSMDPOMasterBid.BidExchDate, vw_tblSMDPOMasterBid.ActShippingUSD, vw_tblSMDPOMasterBid.ActShippingFor, vw_tblSMDPOMasterBid.InvoiceComplete,"+ 
                    " vw_tblSMDPOMasterBid.InvCompleteDate, vw_tblSMDPOMasterBid.POApprovedBy, vw_tblSMDPOMasterBid.POApprovedOn, vw_tblSMDPOMasterBid.varComments, "+ 
                    "vw_tblSMDPOMasterBid.EstShippingUSD, vw_tblSMDPOMasterBid.BidStatusID, VW_tblSMDSuppliers.SupplierName, VW_tblSMDSuppliers.SupplierTel, "+ 
                    "VW_tblSMDSuppliers.SupplierContact, VW_tblSMDSuppliers.SupplierEmail, vw_tblSMDPOMaster.PRType, vw_tblSMDPOMaster.poid, VW_sql_tblSMDPRAccounts.AccountNumber, "+ 
                    "VW_sql_tblSMDPRAccounts.AccountName, vw_tblSMDPOMasterBid.CreditFor, vw_tblSMDPOMasterBid.CreditUSD, vw_tblSMDPOMasterBid.SupplierID, "+ 
                    "vw_tblSMDPOMasterBid.TravVenID, vw_tblSMDPOMaster.AccountID "+ 
                    "FROM (((( vw_tblSMDPODetailBid "+ 
                    "INNER JOIN vw_tblSMDPODetail ON vw_tblSMDPODetailBid.RecID = vw_tblSMDPODetail.recid) "+ 
                    "INNER JOIN vw_tblSMDPOMasterBid ON vw_tblSMDPODetailBid.BidID = vw_tblSMDPOMasterBid.BidID) "+ 
                    "INNER JOIN VW_tblSMDSuppliers ON vw_tblSMDPOMasterBid.SupplierID = VW_tblSMDSuppliers.SupplierID) "+ 
                    "INNER JOIN vw_tblSMDPOMaster ON vw_tblSMDPODetail.POID = vw_tblSMDPOMaster.poid) "+ 
                    "INNER JOIN VW_sql_tblSMDPRAccounts ON vw_tblSMDPOMaster.AccountID = VW_sql_tblSMDPRAccounts.AccountID "+
                    "WHERE (((vw_tblSMDPODetailBid.QtyPO + vw_tblSMDPODetailBid.QtyRecd + vw_tblSMDPODetailBid.QtyInv)>0) and vw_tblSMDPOMasterBid.BidID=" + BidID + ") --ORDER BY vw_tblSMDPODetailBid.ProdID, vw_tblSMDPODetailBid.BidItemID";

        Common.Set_Procedures("ExecQuery");
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(new MyParameter("@Query", sql));
        DataSet dsPrType = new DataSet();
        dsPrType = Common.Execute_Procedures_Select();
        if (dsPrType != null)
        {
            if (dsPrType.Tables[0].Rows.Count != 0)
            {
                RptinvoiceEntry.DataSource = dsPrType;
                RptinvoiceEntry.DataBind();

                SetTotalItems();
            }
        }
    }
    public void ShowMasterData()
    {
        
        DataTable DtQtyinv = Common.Execute_Procedures_Select_ByQuery("select sum(qtyinv)as qtyinv from vw_tblsmdpoDetailbid where bidid="+BidID+"");
        if (DtQtyinv != null)
        {
            if (DtQtyinv.Rows.Count > 0)
            {
                if (Common.CastAsInt32(DtQtyinv.Rows[0][0]) == 0)
                {
                    string strPoMaster = "UPDATE [dbo].tblSMDPOMasterBid SET tblSMDPOMasterBid.BidExchDateInv = [BidExchDate], tblSMDPOMasterBid.BidExchRateInv = [BidExchRate], tblSMDPOMasterBid.BidCurrInv = [BidCurr] WHERE tblSMDPOMasterBid.BidID="+BidID.ToString()+"";
                    string strPoDetail = "UPDATE [dbo].tblSMDPODetailBid SET tblSMDPODetailBid.QtyInv = [qtyRecd], tblSMDPODetailBid.InvPriceFor = [PriceFor] WHERE tblSMDPODetailBid.BidID="+BidID.ToString()+"";
                    DataTable DtPoMaster = Common.Execute_Procedures_Select_ByQuery(strPoMaster);
                    DataTable DtPoDetail = Common.Execute_Procedures_Select_ByQuery(strPoDetail);
                }
            }
        }

        DataTable dt11 = Common.Execute_Procedures_Select_ByQuery("select Rating,comments from BidSupplerComments where bidid=" + BidID + "");
        if (dt11.Rows.Count > 0)
        {
            if (!Convert.IsDBNull(dt11.Rows[0]["Rating"]))
            {
                hfdRating.Value = dt11.Rows[0]["Rating"].ToString();
                txtSupplierComments.Text= dt11.Rows[0]["Comments"].ToString();
            }
        }
        
        InvCurrencyCalc();

    //--------------------/--------------------/--------------------/--------------------/--------------------/--------------------/--------------------
        
        DataTable dtRFQ = Common.Execute_Procedures_Select_ByQuery("EXEC DBO.sp_NewPR_getRFQMasterByBidId " + BidID.ToString());
        
        //Set master data --------------------------------------------------------
        //txtInvNo.ShipId = dtRFQ.Rows[0]["ShipID"].ToString(); 
        if (dtRFQ.Rows.Count > 0)
        {
            lblPONO.Text = dtRFQ.Rows[0]["RFQNO"].ToString();
            lblAccountCode.Text = dtRFQ.Rows[0]["AccountNumber"].ToString() + " - " + dtRFQ.Rows[0]["AccountName"].ToString();
            // Show Vendor Data
            lblVendor.Text = dtRFQ.Rows[0]["BidVenName"].ToString();
            lblPhone.Text = dtRFQ.Rows[0]["BidVenPhone"].ToString();
            lblContact.Text = dtRFQ.Rows[0]["BidVenContact"].ToString();
            lblEmail.Text = dtRFQ.Rows[0]["BidVenEmail"].ToString();

            // Show Curr Data
            lblLocalcurr.Text = dtRFQ.Rows[0]["BIDCURR"].ToString();
            lblExchRate.Text = dtRFQ.Rows[0]["BIDEXCHRATE"].ToString();
            lblRateDate.Text = dtRFQ.Rows[0]["BIDEXCHdATE"].ToString();

            // Show Shipping Data
            if (Convert.ToDecimal(dtRFQ.Rows[0]["actSHIPPINGfOR"]) > 0)
            {
                txtACTSshippingFor.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dtRFQ.Rows[0]["actSHIPPINGfOR"]), 2));
                lblACTshippingUSD.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dtRFQ.Rows[0]["actSHIPPINGUSD"]), 2));
            }
            else
            {
                txtACTSshippingFor.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dtRFQ.Rows[0]["EstShippingFor"]), 2));
                lblACTshippingUSD.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dtRFQ.Rows[0]["EstShippingUSD"]), 2));
            }
            


            txtCreditFor.Text = dtRFQ.Rows[0]["CREDITFOR"].ToString();
            lblCreditUSD.Text = dtRFQ.Rows[0]["CREDITUSD"].ToString();

            txtInvNo.Text = dtRFQ.Rows[0]["InvoiceNo"].ToString();

            txtShipComments.Text = dtRFQ.Rows[0]["biddeliveryinstructions"].ToString();

            //txtInvNo.AddId = dtRFQ.Rows[0]["CMSInvoiceId"].ToString();
            //txtInvNo.AddText = dtRFQ.Rows[0]["InvoiceNo"].ToString();

            //txtInvNo.SelectedId = dtRFQ.Rows[0]["CMSInvoiceId"].ToString();
            //txtInvNo.SelectedValue = dtRFQ.Rows[0]["InvoiceNo"].ToString();

            txtInvDate.Text = dtRFQ.Rows[0]["BidInvoiceDate"].ToString();
            txtInvComm.Text = dtRFQ.Rows[0]["varComments"].ToString();

            ViewState.Add("estShippingUSD", dtRFQ.Rows[0]["estShippingUSD"].ToString());
            ViewState.Add("discountPercentage", dtRFQ.Rows[0]["DisCountPercentage"].ToString());
            ViewState.Add("totalDiscount", dtRFQ.Rows[0]["TotalDiscount"].ToString());
            ViewState.Add("totalDiscountUSD", dtRFQ.Rows[0]["TotalDiscountUSD"].ToString());
            ViewState.Add("totalGSTLC", dtRFQ.Rows[0]["TotalGSTTaxAmount"].ToString());
            ViewState.Add("totalGSTUSD", dtRFQ.Rows[0]["TotalGSTTaxAmountUSD"].ToString());
            txtApproveAmount.Text = dtRFQ.Rows[0]["ApproveAmount"].ToString();
        }
        lblUSDESTGrand.Text =  ProjectCommon.FormatCurrencyWithoutSign( Common.CastAsDecimal(lblTotUSD.Text )+ Common.CastAsDecimal(lblTotalGSTUSD.Text) + Common.CastAsDecimal(lblACTshippingUSD.Text) - Common.CastAsDecimal(ViewState["totalDiscountUSD"])).ToString();
        lblVarianceAmount.Text = ProjectCommon.FormatCurrencyWithoutSign( Common.CastAsDecimal(lblUSDActGrand.Text) - Common.CastAsDecimal(lblUSDESTGrand.Text)).ToString();
        
    }
    public void SetTotalItems()
    {
        decimal fltTotItmFor = 0,fltintTotItmUSD=0,fltUsdPototal=0;
        decimal intQtyPo = 0, intRcvQty = 0, intQtyInv = 0;
        foreach (RepeaterItem RptItm in RptinvoiceEntry.Items)
        {
            Label lblInvForTotal = (Label)RptItm.FindControl("lblInvForTotal");
            Label lblInvUSDTotal = (Label)RptItm.FindControl("lblInvUSDTotal");
            
            HiddenField hfUsdPoTotal = (HiddenField)RptItm.FindControl("hfUsdPoTotal");

            fltTotItmFor = fltTotItmFor + Common.CastAsDecimal(lblInvForTotal.Text);
            fltintTotItmUSD = fltintTotItmUSD + Common.CastAsDecimal(lblInvUSDTotal.Text);
            fltUsdPototal = fltUsdPototal + Common.CastAsDecimal(hfUsdPoTotal.Value);

            //Count Quantity
            Label lblQtyPO = (Label)RptItm.FindControl("lblQtyPO");
            Label lblRcvQty = (Label)RptItm.FindControl("lblRcvQty");
            TextBox txtQtyInv = (TextBox)RptItm.FindControl("txtQtyInv");

            intQtyPo = intQtyPo + Common.CastAsInt32(lblQtyPO.Text);
            intRcvQty = intRcvQty + Common.CastAsInt32(lblRcvQty.Text);
            intQtyInv = intQtyInv + Common.CastAsInt32(txtQtyInv.Text);

        }

        lblTotLC.Text = Math.Round( Common.CastAsDecimal(fltTotItmFor),2).ToString();
        lblTotUSD.Text = Math.Round(Common.CastAsDecimal(fltintTotItmUSD), 2).ToString();
        
        lblDiscountPercentage.Text = Math.Round(Common.CastAsDecimal(ViewState["discountPercentage"]), 2).ToString();
        lblTotalDiscount.Text = Math.Round(Common.CastAsDecimal(ViewState["totalDiscount"]), 2).ToString();
        lblTotalDiscountUSD.Text = Math.Round(Common.CastAsDecimal(ViewState["totalDiscountUSD"]), 2).ToString();
        lblTotalGSTLC.Text = Math.Round(Common.CastAsDecimal(ViewState["totalGSTLC"]), 2).ToString();
        lblTotalGSTUSD.Text = Math.Round(Common.CastAsDecimal(ViewState["totalGSTUSD"]), 2).ToString();
        //Set Final value or Total
        lblLCActGrand.Text = Math.Round( Common.CastAsDecimal( lblTotLC.Text )+ Common.CastAsDecimal(lblTotalGSTLC.Text)  + Common.CastAsDecimal( txtACTSshippingFor.Text ) + Common.CastAsDecimal( txtCreditFor.Text) - Common.CastAsDecimal(lblTotalDiscount.Text), 2).ToString() ;
        lblUSDActGrand.Text = Math.Round( Common.CastAsDecimal(lblTotUSD.Text) + Common.CastAsDecimal(lblTotalGSTUSD.Text)  + Common.CastAsDecimal(lblACTshippingUSD.Text)  + Common.CastAsDecimal(lblCreditUSD.Text) - Common.CastAsDecimal(lblTotalDiscountUSD.Text), 2).ToString();

        lblUSDESTGrand.Text = Convert.ToString(Math.Round(fltUsdPototal + Common.CastAsDecimal(ViewState["estShippingUSD"]) + Common.CastAsDecimal(ViewState["totalGSTUSD"])  - Common.CastAsDecimal(lblTotalDiscountUSD.Text), 2));
        lblVarianceAmount.Text = Convert.ToString(Math.Round(Common.CastAsDecimal(lblUSDActGrand.Text) - Common.CastAsDecimal(lblUSDESTGrand.Text), 0));

        //set quantity
        lblQtyOrder.Text = intQtyPo.ToString();
        QtyRcv.Text = intRcvQty.ToString();
        QtyInv.Text = intQtyInv.ToString();
    }
    public void InvCurrencyCalc(int bidid)
    {

    
    
    //    DoCmd.RunSQL "UPDATE tblSMDPOMasterBid INNER JOIN tblSMDPODetailBid ON " & _
    //    "tblSMDPOMasterBid.BidID = tblSMDPODetailBid.BidID " & _
    //    "SET tblSMDPODetailBid.InvForTotal = [QtyInv]*[InvPriceFor], " & _
    //    "tblSMDPODetailBid.InvUSDTotal = Round(([InvPriceFor]/[BidExchRateInv]),2)*[QtyInv], " & _
    //    "tblSMDPODetailBid.InvUSDPrice = [InvPriceFor]/[BidExchRateInv] " & _
    //    "where tblSMDPOMasterBid.bidid=" & in_bidid
    //    DoCmd.SetWarnings True
    }
    public void txtInvPriceFor_OnTextChanged(object sender, EventArgs e)
    {

    }
    public void InvCurrencyCalc()
    {
        string sql = "UPDATE PD SET PD.InvForTotal = Round((PD.QtyInv)*(PD.InvPriceFor),2),PD.InvUSDTotal = Round(((PD.InvPriceFor)/PM.BidExchRateInv)*(PD.QtyInv),2)," +
                  "PD.InvUSDPrice = (PD.InvPriceFor)/(PM.BidExchRateInv) from  [DBO].tblSMDPODetailBid PD   " +
                  "INNER JOIN [DBO].tblSMDPOMasterBid PM ON PM.BidID = PD.BidID where PM.bidid="+BidID.ToString()+"";
        DataTable dtUpdate = Common.Execute_Procedures_Select_ByQuery(sql);


    }
    protected void btnAddInv_Click(object sender, ImageClickEventArgs e)
    {
       //if (Common.CastAsInt32(txtInvNo.SelectedId) > 0)
       // {
       //     if (dtInvoices.Select("InvoiceId=" + txtInvNo.SelectedId).Length > 0)
       //     {
       //         lblmsg.Text = "Invoice already exists.";
       //         return;
       //     }
       //     //InvoiceList.Add(new clsInvoice(Common.CastAsInt32(txtInvNo.SelectedId), "", DateTime.Today, 10.22));
       //     DataTable dtData = Common.Execute_Procedures_Select_ByQueryCMS("select InvoiceId,VESSELID, RefNo,(select Company from (select PortAgentId as AgentId,Company from portagent union select TravelAgentid as AgentId,Company from travelagent union select SupplierId as AgentId,Company from SupplierMaster) a where a.Agentid=Vendorid) as  VendorId, " +
       //                                                             "round(convert(float,InvoiceAmount),2) as InvoiceAmount, " +
       //                                                             "InvNo as InvoiceNo, " +
       //                                                             "replace(convert(varchar,InvDate,106),' ','-') as Invdate, " +
       //                                                             "replace(convert(varchar,DueDate,106),' ','-') as Duedate, " +
       //                                                             "(select PoNo from POHeader where POHeader.PoId=Invoice.POId) AS PoNo, " +
       //                                                             "(select sum(isnull(amountUSD,0)) from PODetails where PODetails.PoId=Invoice.POId) AS PoAmount, " +
       //                                                             " GST, InvoiceAmount + GST as TotalInvoiceAmount, " +
       //                                                             "(select CurrencyName from Currency where Currency.CurrencyId=invoice.Currencyid) as CurrencyId " +
       //                                                             "from invoice where InvoiceId=" + txtInvNo.SelectedId + " ");

       //     if (dtData.Rows.Count > 0)
       //     {
       //         dtInvoices.Rows.Add(dtInvoices.NewRow());
       //         dtInvoices.Rows[dtInvoices.Rows.Count - 1]["InvoiceId"] = txtInvNo.SelectedId;
       //         dtInvoices.Rows[dtInvoices.Rows.Count - 1]["InvoiceNo"] = txtInvNo.SelectedValue;
       //         dtInvoices.Rows[dtInvoices.Rows.Count - 1]["Currency"] = dtData.Rows[0]["CurrencyId"];
       //         dtInvoices.Rows[dtInvoices.Rows.Count - 1]["RefNo"] = dtData.Rows[0]["RefNo"];
       //         dtInvoices.Rows[dtInvoices.Rows.Count - 1]["Amount"] = dtData.Rows[0]["InvoiceAmount"];
       //         dtInvoices.Rows[dtInvoices.Rows.Count - 1]["Color"] = "White";
       //     } 
       //    BindInvoice();
      
       // }
    }
    public void BindInvoice()
    {  
        grdInvoiceList.DataSource = dtInvoices;
        grdInvoiceList.DataBind();
       
    }

    // Udpate Vendor
    protected void imgUpdateVendor_OnClick(object sender, EventArgs e)
    {
        ViewState["VendorJEId"] = ((LinkButton)sender).CommandArgument;
        ModalPopupExtender1.Visible = true;
        txtVendor.Focus();
    }
    protected void btnNewVendor_Click(object sender, EventArgs e)
    {
        Int32 JeId = Common.CastAsInt32(ViewState["VendorJEId"]);
        Int32 NewVendorId = Common.CastAsInt32(((ImageButton)sender).CommandArgument);
        ModalPopupExtender1.Visible = false;

        if (Update_Vendor(NewVendorId))
        {
            ShowMasterData();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('New vendor updated succesfully.')", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "msg", "alert('Unable to update vendor. Please try again.')", true);
        }
    }
    protected void btnFind_Click(object sender, EventArgs e)
    {
        BindVendor();
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ModalPopupExtender1.Visible = false;
    }
    // Function ---------------------------------------------------------------------------
    public bool Update_Vendor(int NewVendorId)
    {
        try
        {
            Common.Execute_Procedures_Select_ByQuery("EXEC DBO.Change_Vendor_ByBidId " + BidID.ToString() + "," + NewVendorId.ToString());
            return true;
        }
        catch
        {
            return false;
        }
    }
    public void BindVendor()
    {
        
        DataTable dt = Common.Execute_Procedures_Select_ByQuery("select row_number() over (order by SupplierName) as Sno,SupplierId,SupplierName,SupplierPort,SupplierEmail,TravId from VW_tblSMDSuppliers Where SupplierName like '" + txtVendor.Text + "%' Order By SupplierName");
        rptVendors.DataSource = dt;
        rptVendors.DataBind();
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
