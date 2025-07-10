using System;
using System.Collections;
using System.Collections.Specialized; 
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Xml;
using System.Web.UI.DataVisualization.Charting;

//using Microsoft.Office.Interop.Outlook;    
/// <summary>
/// Page Name            : ReqFromVessels.aspx
/// Purpose              : Listing Of Files Received From Vessel
/// Author               : Shobhita Singh
/// Developed on         : 15 September 2010
/// </summary>

public partial class ReceivePO : System.Web.UI.Page
{
    public AuthenticationManager authRecGoods = new AuthenticationManager(0, 0, ObjectType.Page);
    public int SelectedRecId
    {
        get { return Convert.ToInt32("0" + hfPRID.Value); }
        set { hfPRID.Value = value.ToString(); }
    }
    public int BidID
    {
        get { return Convert.ToInt32( ViewState["BidID"]); }
        set { ViewState["BidID"] = value; }
    }

    public int PoID
    {
        get { return Convert.ToInt32(ViewState["PoID"]); }
        set { ViewState["PoID"] = value; }
    }
    public int BidStatusID
    {
        get { return Convert.ToInt32(ViewState["BidStatusID"]); }
        set { ViewState["BidStatusID"] = value; }
    }

    public string page
    {
        get { return ViewState["page"].ToString(); }
        set { ViewState["page"] = value; }
    }

    public int InvoiceId
    {
        get { return Convert.ToInt32(ViewState["InvoiceId"]); }
        set { ViewState["InvoiceId"] = value; }
    }
    #region ---------- PageLoad ------------
    // PAGE LOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        #region --------- USER RIGHTS MANAGEMENT -----------
        try
        {
            authRecGoods = new AuthenticationManager(1064, int.Parse(Session["loginid"].ToString()), ObjectType.Page);
            if (!(authRecGoods.IsView))
            {
                Response.Redirect("~/AuthorityError.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/NoPermission.aspx?Message=" + ex.Message);
        }

        #endregion ----------------------------------------
        lblmsg.Text = "";
        lblDeliveryNotesMsg.Text = "";
        if (!Page.IsPostBack)
        {
            if (Page.Request.QueryString["BidID"] != null)
            {
                BidID = Convert.ToInt32(Page.Request.QueryString["BidID"]);
                if (Page.Request.QueryString["Page"] != null)
                {
                    page = Page.Request.QueryString["Page"].ToString();
                }
                if (Page.Request.QueryString["InvoiceId"] != null)
                {
                    InvoiceId = Convert.ToInt32(Page.Request.QueryString["InvoiceId"]);
                }
                SetVenderDetails();
                BindRPRepeater();
            }
        }
        DataTable DtBidStatusID = Common.Execute_Procedures_Select_ByQuery("select BidStatusID,poid from vw_tblsmdpomasterBid where BidID=" + BidID.ToString()+"");
        if (DtBidStatusID != null)
        {
            if (DtBidStatusID .Rows.Count>0)
            {
                imgUpdateAll.Visible = authRecGoods.IsUpdate;
                imgConfirm.Visible = authRecGoods.IsUpdate;
                imgUpdateQty.Visible = authRecGoods.IsUpdate;
                btnDeliveryNotes.Visible = authRecGoods.IsUpdate;

                BidStatusID = Common.CastAsInt32( DtBidStatusID.Rows[0][0]);
                PoID = Common.CastAsInt32(DtBidStatusID.Rows[0][1]);
                if (BidStatusID == 3)
                {

                }
                else if (BidStatusID == 4)
                {
                    imgUpdateAll.Visible = false;
                }
                else
                {
                    imgUpdateAll.Visible = false;
                    imgClearAll.Visible = false;
                    imgConfirm.Visible = false;
                    imgUpdateQty.Visible = false;
                }
                GetDocCount(PoID);
                GetDeliveryDocCount(PoID, BidID);
            }
        }
        
        
    }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "SetLastFocus('dvscroll_RECPO');", true);
    }
    #endregion


    //------------------------------------------------------------Function
    protected void BindRPRepeater()
    {
        string sql = "select   row_number() over(order by MP.recid desc) RowNum,PB.BidItemID,MP.Poid,MP.description,MP.recid,MP.EquipItemDrawing," +
                     " MP.EquipItemCode,MP.PartNo,MP.uom ,( convert(int,PB.QtyPO )- convert(int, PB.QtyRecd))as RemQty,PB.BidDescription,PB.QtyPO,PB.QtyRecd" +
                     " from vw_tblSMDPODetail MP "+
                     "inner join  vw_tblSMDPODetailBid PB on MP.recid=PB.recid "+
                     "where PB.BidID =" + BidID + " and PB.ProductAccepted = 1";
        DataTable DtRecvPODetails = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtRecvPODetails != null)
        {
            if (DtRecvPODetails.Rows.Count > 0)
            {
                RptReceiveOrderLst.DataSource = DtRecvPODetails;
                RptReceiveOrderLst.DataBind();
                SetTotPenAndOrderQty();
            }
        }

    }
    public void SetVenderDetails()
    {
        DataTable dtRFQ = Common.Execute_Procedures_Select_ByQuery("EXEC DBO.sp_NewPR_getRFQMasterByBidId " + BidID.ToString());
              if (dtRFQ != null)
              {
                  if (dtRFQ.Rows.Count > 0)
                  {
                      // MASTER INFORMATION

                      DataRow dr = dtRFQ.Rows[0];
                      lblRFQNO.Text = dr["RFQNO"].ToString();
                      lblReqNo.Text = dr["REQNO"].ToString();

                      lblReqType.Text = dr["PRTYPENAME"].ToString();
                      lblCreatedBy.Text = dr["POCREATOR"].ToString();
                      lblDateCreated.Text = dr["DATECREATED"].ToString();

                      lblVenderName.Text = dr["SupplierName"].ToString();
                      string contact = dr["BIDVENCONTACT"].ToString();
                      contact = contact + ((contact != "") ? "<br/>" : "") + dr["BIDVENCONTACT"].ToString();
                      contact = contact + ((contact != "") ? "<br/>" : "") + dr["BIDVENPHONE"].ToString();
                      contact = contact + ((contact != "") ? "<br/>" : "") + dr["BIDVENEMAIL"].ToString();

                      lblVenContact.Text = contact;


                      //------------------------------------------------------
                  }
              }
        //------------------------------------------

        string sql = "select BidStatusID,BidPoNum,BidStatusID,BidVenPhone,BidVenContact,BidVenemail,BidVenName,BidReceivedDate,isnull(ABid.OrderStatusComment,'') as OrderStatusComment,biddeliveryinstructions, " +
                    "(select (convert(varchar,PRA.AccountID)+' - '+PRA.AccountName)AccountName from vw_tblSMDPOmaster PM left join VW_sql_tblSMDPRAccounts PRA  on PM.AccountID =PRA.AccountID where poid=" + BidID + ") as AccountName " +
                     "from vw_tblSMDPOmasterBid Bid left join Add_tblSmdPomasterBid ABid on ABid.BidID=Bid.BidID where Bid.BidID=" + BidID + " and IsPo=1";
        DataTable DtVenDetail = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtVenDetail != null)
        {
            if (DtVenDetail.Rows.Count > 0)
            {
                if (DtVenDetail.Rows[0]["BidReceivedDate"].ToString() != "")
                {
                    txtitmRcvDate.Text = Convert.ToDateTime(DtVenDetail.Rows[0]["BidReceivedDate"]).ToString("dd-MMM-yyyy");
                }
                txtOrderStatusComm.Text = DtVenDetail.Rows[0]["OrderStatusComment"].ToString();
                txtShipComm.Text = DtVenDetail.Rows[0]["biddeliveryinstructions"].ToString();
                if (BidStatusID >= 5)
                {
                    imgConfirm.Visible = false;
                    imgUpdateQty.Visible = false;
                }
                
            }
        }
    }
    public void SetTotPenAndOrderQty()
    {
        int intTotQtypo=0,intRcvQty=0,intRemQty=0;
        foreach (RepeaterItem Rptitm in RptReceiveOrderLst.Items)
        {
            Label lblQtyPO = (Label)Rptitm.FindControl("lblQtyPO");
            TextBox txtRcvQty = (TextBox)Rptitm.FindControl("txtRcvQty");
            Label lblRemainQty = (Label)Rptitm.FindControl("lblRemainQty");

            intTotQtypo = intTotQtypo + Common.CastAsInt32(lblQtyPO.Text);
            intRcvQty = intRcvQty + Common.CastAsInt32(txtRcvQty.Text);
            intRemQty = intRemQty + Common.CastAsInt32(lblRemainQty.Text);
        }
        //lblTotQtyOrder.Text = intTotQtypo.ToString();
        //lblTotQtyRcv.Text = intRcvQty.ToString();
        //lblTotQtyPending.Text = intRemQty.ToString();
    }
    //------------------------------------------------------------Event
    //----------------Button
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/OrdersList.aspx");   
    }
    protected void imgConfirm_OnClick(object sender, EventArgs e)
    {
        // Validation 
        if (txtitmRcvDate.Text.Trim() == "")
        {
            lblmsg.Text = "You must enter a date first.";
            return;
        }
        if (Convert.ToDateTime(txtitmRcvDate.Text) > System.DateTime.Now)
        {
            lblmsg.Text = "Receive Date should not be greater than current date.";
            return;
        }
        if (BidStatusID < 5)
        {
            string Qty = "";
            string BidItmID = "";
            int QtyCount = 0;
            foreach (RepeaterItem RptItm in RptReceiveOrderLst.Items)
            {
                Qty = Qty + "," + ((TextBox)RptItm.FindControl("txtRcvQty")).Text;
                BidItmID = BidItmID + "," + ((HiddenField)RptItm.FindControl("hfBidItmID")).Value;
                if (!string.IsNullOrWhiteSpace(((TextBox)RptItm.FindControl("txtRcvQty")).Text) && Convert.ToInt32(((TextBox)RptItm.FindControl("txtRcvQty")).Text) > 0)
                {
                    QtyCount = QtyCount + Convert.ToInt32(((TextBox)RptItm.FindControl("txtRcvQty")).Text);
                }
            }
            if (QtyCount == 0)
            {
                lblmsg.Text = "Can not Proceed, received Qty for all item is Zero.";
                return;
            }
            if (Qty != "")
            {
                Qty = Qty.Substring(1);
                BidItmID = BidItmID.Substring(1);
            }
            Common.Set_Procedures("sp_NewPR_UpdateReceiveQty");
            Common.Set_ParameterLength(6);
            Common.Set_Parameters(
                        new MyParameter("@BIDID", BidID),
                        new MyParameter("@BidItemID", BidItmID),
                        new MyParameter("@QTYRCV", Qty),
                        new MyParameter("@BidReceivedDate", txtitmRcvDate.Text.Trim()),
                        new MyParameter("@IsBidConfirm", 1),// 1 indicate that this bidID has beeb confirmed
                        new MyParameter("@OrderStatusComment", txtOrderStatusComm.Text.Trim())
                        );
            DataSet DsItm=new DataSet();
            Boolean Res;
            Res = Common.Execute_Procedures_IUD(DsItm);

            if (Res)
            {
                if (page == "INV" && InvoiceId > 0)
                {
                    string RefreshPage = "../Invoice/ViewInvoice.aspx?InvoiceId=" + InvoiceId.ToString() + "";
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "pop", "window.open('" + RefreshPage + "', '');", false);
                }
                lblmsg.Text = "Received quantity has been updated successfully.";
            }
            else
            {
                lblmsg.Text = "Received quantity can't be updated." + Common.ErrMsg;
            }
        }
        else
        {
            lblmsg.Text = "These received items have already been confirmed.";
        }
    }
    protected void imgUpdateAll_OnClick(object sender, EventArgs e)
    {
        if (BidStatusID != 3)
        {
            lblmsg.Text = "You can only RECEIVE ALL for Status: PO APPROVED.";
            return;
        }
        else
        {

            int intTotQtypo = 0;
            foreach (RepeaterItem RptItm in RptReceiveOrderLst.Items)
            {
                TextBox txtRcvQty = (TextBox)RptItm.FindControl("txtRcvQty");
                Label lblQtyPO = (Label)RptItm.FindControl("lblQtyPO");
                Label lblRemainQty = (Label)RptItm.FindControl("lblRemainQty");
                txtRcvQty.Text = lblQtyPO.Text;
                lblRemainQty.Text = "0";

                intTotQtypo = intTotQtypo + Common.CastAsInt32(lblQtyPO.Text);
            }
            //lblTotQtyOrder.Text = intTotQtypo.ToString();
            //lblTotQtyRcv.Text = intTotQtypo.ToString();
            //lblTotQtyPending.Text = "0";

            //------------
            //Common.Set_Procedures("sp_NewPR_ReceiveAllQty");
            //Common.Set_ParameterLength(1);
            //Common.Set_Parameters(new MyParameter("@BIDID", BidID));
            //DataSet DsItm = new DataSet();
            //Boolean Res;
            //Res = Common.Execute_Procedures_IUD(DsItm);
        }
    }
    protected void imgClearAll_OnClick(object sender, EventArgs e)
    {
        int intTotQtypo = 0;
        foreach (RepeaterItem RptItm in RptReceiveOrderLst.Items)
        {
            TextBox txtRcvQty = (TextBox)RptItm.FindControl("txtRcvQty");
            Label lblQtyPO = (Label)RptItm.FindControl("lblQtyPO");
            Label lblRemainQty = (Label)RptItm.FindControl("lblRemainQty");
            txtRcvQty.Text = "0";
            lblRemainQty.Text = lblQtyPO.Text;

            intTotQtypo = intTotQtypo + Common.CastAsInt32(lblQtyPO.Text);
        }
        //lblTotQtyOrder.Text = intTotQtypo.ToString();
        //lblTotQtyRcv.Text = "0";
        //lblTotQtyPending.Text = intTotQtypo.ToString();
    }
    protected void imgUpdateQty_OnClick(object sender, EventArgs e)
    {
        // Validation 
        if (txtitmRcvDate.Text.Trim() == "")
        {
            lblmsg.Text = "You must enter a date first.";
            return;
        }
        if (Convert.ToDateTime(txtitmRcvDate.Text) > System.DateTime.Now)
        {
            lblmsg.Text = "Receive Date should not be greater than current date.";
            return;
        }
        string sql= " select BidSMDLevel1ApprovalDate from [DBO].tblSmdPoMasterBid where bidid="+BidID+"  ";
        DataTable DtRecvPODetails = Common.Execute_Procedures_Select_ByQuery(sql);
        if (DtRecvPODetails.Rows.Count > 0)
        {
            try
            {
                DateTime BidSMDLevel1ApprovalDate = Convert.ToDateTime(DtRecvPODetails.Rows[0][0]);
                if (BidSMDLevel1ApprovalDate.Date >Convert.ToDateTime(txtitmRcvDate.Text).Date)
                {
                    lblmsg.Text = "Receive Date should not be less than bid approval date.";
                    return;
                }
            }
            catch (Exception ex) { }
        }

        if (BidStatusID < 5)
        {
            string Qty = "";
            string BidItmID = "";
            int QtyCount = 0;
            foreach (RepeaterItem RptItm in RptReceiveOrderLst.Items)
            {
                Qty = Qty + "," + ((TextBox)RptItm.FindControl("txtRcvQty")).Text;
                HiddenField hfBidItmID= (HiddenField)RptItm.FindControl("hfBidItmID");
                BidItmID = BidItmID + "," + hfBidItmID.Value;
                if (! string.IsNullOrWhiteSpace(((TextBox)RptItm.FindControl("txtRcvQty")).Text) && Convert.ToInt32(((TextBox)RptItm.FindControl("txtRcvQty")).Text) > 0) 
                {
                    QtyCount = QtyCount + Convert.ToInt32(((TextBox)RptItm.FindControl("txtRcvQty")).Text);
                }
            }
            if (QtyCount == 0)
            {
                lblmsg.Text = "Can not Proceed, received Qty for all item is Zero.";
                return;
            }
            if (Qty != "")
            {
                Qty = Qty.Substring(1);
                BidItmID = BidItmID.Substring(1);
            }
            Common.Set_Procedures("sp_NewPR_UpdateReceiveQty");
            Common.Set_ParameterLength(6);
            Common.Set_Parameters(
                        new MyParameter("@BIDID", BidID),
                        new MyParameter("@BidItemID", BidItmID),
                        new MyParameter("@QTYRCV", Qty),
                        new MyParameter("@BidReceivedDate", txtitmRcvDate.Text.Trim()),
                        new MyParameter("@IsBidConfirm", "-1"),
                        new MyParameter("@OrderStatusComment", txtOrderStatusComm.Text.Trim())
                        );
            DataSet DsItm = new DataSet();
            Boolean Res;
            Res = Common.Execute_Procedures_IUD(DsItm);
            if (Res)
            {
                if (page == "INV" && InvoiceId > 0)
                {
                    string RefreshPage = "../Invoice/ViewInvoice.aspx?InvoiceId=" + InvoiceId.ToString() + "";
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "pop", "window.open('" + RefreshPage + "', '');", false);
                }
                lblmsg.Text = "Received quantity has been updated successfully.";
            }
            else
            {
                lblmsg.Text = "Received quantity can't be updated." + Common.ErrMsg;
            }
        }
    }
    //----------------Button
    protected void txtRcvQty_OnTextChanged(object sender, EventArgs e)
    {
        TextBox txtRcvQty = (TextBox)sender;
        Label lblQtyPO = (Label)txtRcvQty.Parent.FindControl("lblQtyPO");
        Label lblRemainQty = (Label)txtRcvQty.Parent.FindControl("lblRemainQty");
        HiddenField hfRcvQty = (HiddenField)txtRcvQty.Parent.FindControl("hfRcvQty");
        //if (Common.CastAsInt32(txtRcvQty.Text) > Common.CastAsInt32(lblQtyPO.Text))
        //{
        //    lblmsg.Text = "Received Quantity can be greater than PO Quantity.";
        //    txtRcvQty.Text = hfRcvQty.Value;
        //    return;
        //}
        lblRemainQty.Text = Convert.ToString(Common.CastAsInt32(lblQtyPO.Text) - Common.CastAsInt32(txtRcvQty.Text));
        hfRcvQty.Value = txtRcvQty.Text;

        SetTotPenAndOrderQty();
    }
    protected void txtQtyPO_OnTextChanged(object sender, EventArgs e)
    {
        TextBox txtQtyPO = (TextBox)sender;
        //TextBox txtRcvQty = (TextBox)sender;
        TextBox txtRcvQty = (TextBox)txtQtyPO.Parent.FindControl("txtRcvQty");
        Label lblRemainQty = (Label)txtQtyPO.Parent.FindControl("lblRemainQty");
        HiddenField hfQtyPo = (HiddenField)txtQtyPO.Parent.FindControl("hfQtyPo");
        if(Common.CastAsInt32(txtRcvQty.Text) > Common.CastAsInt32(txtQtyPO.Text))
        {
            lblmsg.Text = "Order quantity can be less than recieved quantity. ";
            txtQtyPO.Text = hfQtyPo.Value;
            return;
        }
        lblRemainQty.Text = Convert.ToString(Common.CastAsInt32(txtQtyPO.Text) - Common.CastAsInt32(txtRcvQty.Text));
        hfQtyPo.Value = txtQtyPO.Text;
        SetTotPenAndOrderQty();
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


    protected void btnDeliveryNotes_Click(object sender, EventArgs e)
    {
        try
        {
            lblDeliveryNotesMsg.Text = "";
            if (BidID == 0 && PoID == 0)
            {
                lblDeliveryNotesMsg.Text = "Can't upload Delievery notes document for this order.";
                return;
            }
            
            //string FileName = "";
            //    byte[] ImageAttachment ;
            if (FU.HasFile)
            {
                if (FU.PostedFile.ContentLength > (1024 * 1024 * 0.25))
                {
                    lblDeliveryNotesMsg.Text = "File Size is Too big! Maximum Allowed is 250KB...";
                    FU.Focus();
                    return;
                }
                //else
                //{
                //    FileName = FU.FileName;
                //    // ImageAttachment = FU.FileBytes;
                //}

                int BidiD = BidID > 0 ? BidID : 0;
                string FileName = Path.GetFileName(FU.PostedFile.FileName);
                string fileContent = FU.PostedFile.ContentType;
                Stream fs = FU.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                byte[] bytes = br.ReadBytes((Int32)fs.Length);
                Common.Set_Procedures("[dbo].[Sp_InsertUpdateBidDeliveryNotesDoc]");
                Common.Set_ParameterLength(6);
                Common.Set_Parameters(
                    new MyParameter("@PoId", PoID),
                    new MyParameter("@BidId", BidID),
                    new MyParameter("@Addedby", Session["FullName"].ToString()),
                    new MyParameter("@DocName", FileName),
                    new MyParameter("@Doc", bytes),
                    new MyParameter("@ContentType", fileContent)
                    );
                DataSet ds = new DataSet();
                ds.Clear();
                Boolean res;
                res = Common.Execute_Procedures_IUD(ds);
                if (res)
                {
                    lblDeliveryNotesMsg.Text = "Delivery Notes Document saved Successfully.";
                    // ShowAttachment();
                    //  GetDeliveryDocCount(PoID, BidID);
                    lblDeliveryNotesCount.Text = ds.Tables[0].Rows[0][0].ToString();
                    if (Convert.ToInt32(lblDeliveryNotesCount.Text) > 0)
                    {
                        ImgAttDeliveryNotes.Enabled = true;
                    }
                    else
                    {
                        ImgAttDeliveryNotes.Enabled = false;
                    }
                }
                else
                {
                    lblDeliveryNotesMsg.Text = "Unable to add Document.Error :" + Common.getLastError();
                }
            }
            else
            {
                lblDeliveryNotesMsg.Text = "Please select Delivery Notes Document.";
                FU.Focus();
                return;
            }
        }
        catch (Exception ex)
        {
            lblDeliveryNotesMsg.Text = ex.Message.ToString();
        }
    }

    protected void GetDeliveryDocCount(int POid, int Bidid)
    {
        string sql = "";
        if (POid > 0)
        {
            sql = "SELECT Count(DocId) As DocumentCount FROM [tblSMDPOMasterBid_DeliveryNoteDoc] with(nolock) WHERE [BidId] = " + Bidid + " AND  PoId =" + POid;
            DataTable DT = Common.Execute_Procedures_Select_ByQuery(sql);
            lblDeliveryNotesCount.Text = DT.Rows[0]["DocumentCount"].ToString();
        }
       
        if (Convert.ToInt32(lblDeliveryNotesCount.Text) > 0)
        {
            ImgAttDeliveryNotes.Enabled = true;
        }
        else
        {
            ImgAttDeliveryNotes.Enabled = false;
        }
    }

    protected void ImgAttDeliveryNotes_Click(object sender, ImageClickEventArgs e)
    {
       try
        {
            if (PoID > 0 && BidID > 0)
            {
               string sql = "SELECT top 1 DocName,Attachment,ContentType FROM [tblSMDPOMasterBid_DeliveryNoteDoc] with(nolock) WHERE  Poid =" + PoID + " AND BidId = "+ BidID + " ";
                DataTable dt = Common.Execute_Procedures_Select_ByQuery(sql);
                if (dt.Rows.Count > 0)
                {
                    try
                    {
                        string contentType = "";
                        string FileName = "";
                        if (!string.IsNullOrWhiteSpace(dt.Rows[0]["ContentType"].ToString()))
                        {
                            contentType = dt.Rows[0]["ContentType"].ToString();
                        }
                        if (!string.IsNullOrWhiteSpace(dt.Rows[0]["DocName"].ToString()))
                        {
                            FileName = dt.Rows[0]["DocName"].ToString();
                        }
                        if (!string.IsNullOrWhiteSpace(contentType))
                        {

                            byte[] latestFileContent = (byte[])dt.Rows[0]["Attachment"];
                            Response.Clear();
                            Response.Buffer = true;
                            Response.Charset = "";
                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            Response.ContentType = contentType;
                            Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                            Response.BinaryWrite(latestFileContent);
                            Response.Flush();
                            // HttpContext.Current.Response.End();
                        }
                    }
                    catch (Exception ex)
                    {

                        Response.Clear();
                        Response.Write("<center> Invalid File !</center>");
                        Response.End();
                    }
                }
            }
            
        }
       catch(Exception ex)
        {
            lblDeliveryNotesMsg.Text = ex.Message.ToString();
        }
    }
}
