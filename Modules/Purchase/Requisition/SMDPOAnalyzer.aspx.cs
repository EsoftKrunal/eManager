using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using iTextSharp.text.html.simpleparser;
using System.Web.UI.DataVisualization.Charting;
using System.EnterpriseServices;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;
using System.Data.SqlTypes;
using System.Security.Cryptography;

public partial class SMDPOAnalyzer : System.Web.UI.Page
{
    public string IsApproval
    {
        get { return ViewState["Approval"].ToString(); }
        set { ViewState["Approval"] = value; }
    }
    public int PRID
    {
        set { ViewState["PRID"] = value; }
        get { return int.Parse("0" + ViewState["PRID"]); }
    }

    public int PoCount
    {
        set { ViewState["PoCount"] = value; }
        get { return int.Parse("0" + ViewState["PoCount"]); }
    }
    public string ShowAmount
    {
        get
        {
            return (ViewState["ShowAmount"] == null) ? "N" : ViewState["ShowAmount"].ToString();
        }
        set { ViewState["ShowAmount"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //---------------------------------------
        ProjectCommon.SessionCheck();
        //---------------------------------------
        if(!IsPostBack)
        {
            IsApproval = "Y";
            if (Page.Request.QueryString["Prid"] != null)
            {
                PRID = Common.CastAsInt32(Page.Request.QueryString["Prid"]);
            }
            ShowAmount = "N";
            int loginid = Common.CastAsInt32(Session["loginid"]);
            if (ProjectCommon.View_Quote_Permission(PRID, 0, loginid))
            {
                ShowAmount = "Y";
            }
            else
            {
                Response.Redirect("~/UnAuthorizedAccess.aspx?Message=Sorry ! you have not pemission to view the quote details.", true);
                return;
            }
            //---------------------
        }               
        GetData();
        GetDocCount(PRID);
        if (PoCount > 0)
        {
            divCloseReq.Visible = true;
        }
    }
    public void GetData()
    {
        string[] strColors = {"#FFBBBB","#FF7D4D","#DBCCC1","#C1E950","#78CA20","#D0DCC5","#93FF93","#8BD5C7","#4EDFD7","#99FFFF","#93E4FF","#C7E8FD","#B7C4D4","#BCD1F7","#B1C1F3","#D8C4FF","#DFAEF8","#D2C1D2","#FFD7FF","#F796CA","#F39FA1","#FFBC41","#FF7777"};        
        int counter=0;
        string sqlPRItems = "select  row_number() over(order by Recid Desc ) Row" + 
                            ",poid,recid,Description," + 
                            "(" + 
                            "select min(unitpriceusd) " +
                            "from " +
                            "( " +
                            "select pricefor / (select isnull(bidexchrate,1) from vw_tblsmdpomasterbid mst where mst.bidid=det.bidid) as unitpriceusd " +
                            "from vw_tblsmdpodetailBid det " +
                            "where isnull(pricefor,0)>0 and bidid in (select bidid from vw_tblsmdpoMasterBid where poid=" + PRID + " and bidStatusID>1) and recid=vw_tblsmdpodetail.recid " +
                            ") aa " + 
                            ")" +
                            "as MinBidAmt " + 
                            "from vw_tblsmdpodetail where poid in (select top 1 poid from vw_tblsmdpoMasterBid where poid="+PRID+ " and bidStatusID>1) order by Recid Desc ";
        DataTable DtPrItems = Common.Execute_Procedures_Select_ByQuery(sqlPRItems);
        if (DtPrItems != null)
        {
            if (DtPrItems.Rows.Count == 0)
            {
                lblmsg.Text = "No data found";
                return;
            }
        }

        string sqlPRBids = "select  (select SupplierName from VW_tblSMDSuppliers VW where VW.SupplierId=Bid.SupplierId)SupplierName,(dbo.getBidPONum(Bid.BIDID)) RFQNO ,* , (Select BidStatusName from VW_qRFQListing_SQL q with(nolock) where q.BidID = bid.BidID) As BidStatusName, ROW_NUMBER() Over (order by BidId, BidGroup Asc) As SRNo,(dbo.getBidstatus(Bid.BIDID)) As BidCurrentStatus,(Select Case when isPO=1 then 'none' Else '' END  from tblSMDPOMasterBid b with(nolock) where b.BidID = Bid.BIDID) As PoStatus, isPO    from vw_tblsmdpoMasterBid Bid where poid=" + PRID + " and bidStatusID>1";
        DataTable DtPrBids = Common.Execute_Procedures_Select_ByQuery(sqlPRBids);

        string strUSDTotal = "";
        DataTable DtUSDTotal = new DataTable();

        string table = "<input type='hidden' id='hdnItemCount' value='" + getItemCountforPO(Common.CastAsInt32(PRID)) + "' /><input type='hidden' id='hdnBidCount' value='" + DtPrBids.Rows.Count + "' /><table cellpadding='2' cellspacing='0' border='1' style='text-align:center;border-collapse:collapse;font-family:Arial;font-size:12px;' width='100%'>";

        int i , j;
        
        for (i=-3; i <= DtPrItems.Rows.Count+7;i++ )
        {
            if (i == -3)
            {
                table = table + "<tr class='header'>";
                table = table + "<td style='padding:4px;width:40px;'> S. No.";
                table = table + "</td>";
                table = table + "<td style='padding:4px;'> Description";
                table = table + "</td>";
            }
            else if (i == -2)
            {
                table = table + "<tr class='header'>";
                table = table + "<td style='padding:4px;'>";
                table = table + "</td>";
                table = table + "<td style='padding:4px;'>";
                table = table + "</td>";
            }
            else if (i == -1)
            {
                table = table + "<tr class='header'>";
                table = table + "<td style='padding:4px;'>";
                table = table + "</td>";
                table = table + "<td style='padding:4px;'>";
                table = table + "</td>";
            }
            else if (i == DtPrItems.Rows.Count)
            {
                table = table + "<tr style='background-color:#D8D8D8'>";
                table = table + "<td style='padding:4px;'>";
                table = table + "</td>";
                table = table + "<td >";
                table = table + "</td>";
            }
            else if (i == DtPrItems.Rows.Count+1)
            {
                table = table + "<tr style='background-color:#D8D8D8'>";
                table = table + "<td style='padding:4px;'>";
                table = table + "</td>";
                table = table + "<td >";
                table = table + "</td>";
            }
            else if (i == DtPrItems.Rows.Count+2)
            {
                table = table + "<tr style='background-color:#D8D8D8'>";
                table = table + "<td style='padding:4px;'>";
                table = table + "</td>";
                table = table + "<td >";
                table = table + "</td>";
            }
            else if (i == DtPrItems.Rows.Count+3)
            {
                table = table + "<tr style='background-color:#D8D8D8'>";
                table = table + "<td style='padding:4px;'>";
                table = table + "</td>";
                table = table + "<td >";
                table = table + "</td>";
            }
            else if (i == DtPrItems.Rows.Count+4)
            {
                table = table + "<tr style='background-color:#D8D8D8'>";
                table = table + "<td style='padding:4px;'>";
                table = table + "</td>";
                table = table + "<td >";
                table = table + "</td>";
            }
            else if (i == DtPrItems.Rows.Count+5)
            {
                table = table + "<tr style='background-color:#D8D8D8'>";
                table = table + "<td style='padding:4px;'>";
                table = table + "</td>";
                table = table + "<td >";
                table = table + "</td>";
            }
            else if (i == DtPrItems.Rows.Count+6)
            {
                table = table + "<tr style='background-color:#D8D8D8'>";
                table = table + "<td style='padding:4px;'>";
                table = table + "</td>";
                table = table + "<td >";
                table = table + "</td>";
            }
            else if (i == DtPrItems.Rows.Count+7)
            {
                table = table + "<tr style='background-color:#D8D8D8'>";
                table = table + "<td style='padding:4px;'>";
                table = table + "</td>";
                table = table + "<td >";
                table = table + "</td>";
            }
            else
            {

                table = table + "<tr>";
                table = table + "<td>";
                table = table + DtPrItems.Rows[i]["Row"].ToString();
                table = table + "</td>";

                table = table + "<td style='text-align:left;'>";
                table = table + DtPrItems.Rows[i]["Description"].ToString();
                table = table + "</td>";
            }
            PoCount = 0;
            Random r = new Random();         
            foreach (DataRow drbids in DtPrBids.Rows)
            {
                PoCount = PoCount + Convert.ToInt32(drbids["isPO"]);
                string BgColor = strColors[Common.CastAsInt32(r.Next(22))];
                counter=counter+1;
                if (i == -3) // For adding Header
                {
                    table = table + "<td style='padding:4px;color:black;background-color:white;color:Blue' colspan='4'>";
                    table = table + "<a style=''  onclick='ViewPopUp(" + drbids["BidID"].ToString() + ");'>" + drbids["RFQNO"].ToString() + "</a> &nbsp;&nbsp; ( <a style='cursor:pointer;text-decoration:underline;display:" + drbids["PoStatus"].ToString() + "'  title='Click to send request for approval.' onclick='ViewRFQ(" + drbids["BidID"].ToString() + ");'> Approve/Place Order </a> ) &nbsp; <div style='color:red;font-weight:bold;'> "+ drbids["BidStatusName"].ToString()  + "  (" + drbids["BidCurrentStatus"].ToString() + ")</div>";
                    //< img src = '../../HRD/Images/approved.png' alt = '' style = 'border:solid 0px red '  title = 'Approve/Place Order' onclick = 'ViewRFQ(" + drbids["BidID"].ToString() + ");' />
                    if (IsApproval=="Y")
                    {
                        //table = table + "&nbsp;<a style='cursor:pointer;' title='Go For Approval' onclick='ApproveThis(" + drbids["BidID"].ToString() + ");'><img src='images/approved.png' style='height:12px;'/></a>";
                        //DataTable dtcAN = Common.Execute_Procedures_Select_ByQuery("select * from bidapprovallist WHERE BIDID=" + drbids["BidID"].ToString() + " and ISNULL(ORDERPLACED,0)=0");
                        //if(dtcAN.Rows.Count >0)
                        //{
                        //    table = table + "&nbsp;<a style='cursor:pointer;' title='Cancel Approval' onclick='CancelApproval(" + drbids["BidID"].ToString() + ");'><img src='images/delete.png' style='height:12px;'/></a>";
                        //}
                    }
                    table = table + "</td>";
                }
                else if (i == -2) // For adding Header
                {

                    table = table + "<td style='padding:4px;color:black;background-color:" + BgColor + "'' colspan='4'>";
                    table = table + drbids["SupplierName"].ToString();
                    table = table + "</td>";
                }
                else if (i == -1)
                {
                    table = table + "<td style='padding:4px;color:black;background-color:" + BgColor + "''> <input type='checkbox' id='chkItem"+ drbids["SRNo"].ToString() + "'  onclick=CheckedAll(this) class='form-check-input headerrow' bidid='" + drbids["BidID"].ToString() + "' poid='"+ Common.CastAsInt32(PRID) + "' approvalstatus='" + getApprovalStatusResult(drbids["BidSMDLevel1Approval"].ToString()) + "' />";
                    table = table + "</td>";

                    table = table + "<td style='padding:4px;color:black;background-color:" + BgColor + "''> Bid Qty";
                    table = table + "</td>";

                    table = table + "<td style='padding:4px;color:black;background-color:" + BgColor + "''> Unit Price(US$)";
                    table = table + "</td>";


                    table = table + "<td style='padding:4px;color:black;background-color:" + BgColor + "''> Total(US$)";
                    table = table + "</td>";
                }
                else if (i == DtPrItems.Rows.Count)
                {                   
                    string strEstShipping = "select convert(numeric(10,2),Isnull(EstShippingUSD,0)) As EstShippingUSD   from VW_tblSMDPOMasterBid where BidID=" + drbids["BidID"].ToString() + "";
                    DataTable DtEstShipping = Common.Execute_Procedures_Select_ByQuery(strEstShipping);
                    if (DtEstShipping == null || DtEstShipping.Rows.Count == 0)
                    {
                        table = table + "<td style='padding:4px;font-weight:bold;text-align:right;' colspan='3'> Estd. Shipping/Handling Charges  </td>";
                        table = table + "<td style='padding:4px;;background-color:D8D8D8;font-weight:bold; text-align:right;'> - </td>";
                    }
                    else
                    {
                        table = table + "<td style='padding:4px;font-weight:bold;text-align:right;' colspan='3'> Estd. Shipping/Handling Charges </td>";
                        table = table + "<td style='padding:4px;font-weight:bold; text-align:right;'> $&nbsp;" + DtEstShipping.Rows[0]["EstShippingUSD"].ToString() + "</td>";

                    }
                }
                else if (i == DtPrItems.Rows.Count+1)
                {
                    string strGSTTaxAmt = "select convert(numeric(10,2),Isnull(TotalGSTTaxAmountUSD,0)) As TotalGSTTaxAmountUSD   from VW_tblSMDPOMasterBid where BidID=" + drbids["BidID"].ToString() + "";
                    DataTable DtGSTTaxAmt = Common.Execute_Procedures_Select_ByQuery(strGSTTaxAmt);
                    if (DtGSTTaxAmt == null || DtGSTTaxAmt.Rows.Count == 0)
                    {
                        table = table + "<td style='padding:4px;font-weight:bold;text-align:right;' colspan='3'> GST/Tax Total  </td>";
                        table = table + "<td style='padding:4px;;background-color:D8D8D8;font-weight:bold; text-align:right;'> - </td>";
                    }
                    else
                    {
                        table = table + "<td style='padding:4px;font-weight:bold;text-align:right;' colspan='3'> GST/Tax Total </td>";
                        table = table + "<td style='padding:4px;font-weight:bold; text-align:right;'> $&nbsp;" + DtGSTTaxAmt.Rows[0]["TotalGSTTaxAmountUSD"].ToString() + "</td>";

                    }
                }
                else if (i == DtPrItems.Rows.Count+2)
                {
                    string strDiscount = "select convert(numeric(10,2),Isnull(DisCountPercentage,0)) As DisCountPercentage, convert(numeric(10,2),Isnull(TotalDiscountUSD,0)) As TotalDiscountUSD  from VW_tblSMDPOMasterBid where BidID=" + drbids["BidID"].ToString() + "";
                    DataTable DtTotalDiscount = Common.Execute_Procedures_Select_ByQuery(strDiscount);
                    if (DtTotalDiscount == null || DtTotalDiscount.Rows.Count == 0)
                    {
                        table = table + "<td style='padding:4px;font-weight:bold;text-align:right;' colspan='3'> Discount (" + DtTotalDiscount.Rows[0]["DisCountPercentage"].ToString() + " %) </td>";
                        table = table + "<td style='padding:4px;;background-color:D8D8D8;font-weight:bold; text-align:right;'> - </td>";
                    }
                    else
                    {
                        table = table + "<td style='padding:4px;font-weight:bold;text-align:right;' colspan='3'> Discount (" + DtTotalDiscount.Rows[0]["DisCountPercentage"].ToString() + " %) </td>";
                        table = table + "<td style='padding:4px;font-weight:bold; text-align:right;'> $&nbsp;" + DtTotalDiscount.Rows[0]["TotalDiscountUSD"].ToString() + "</td>";

                    }
                }
                else if (i == DtPrItems.Rows.Count+3)
                {
                    //decimal MinAmount = 0;
                    //string strMinAmount = "select min(total)from(select bidid,convert(numeric(10,2),(sum(usdtotal * convert(float,BidQty)))) as Total  from vw_tblsmdpoDetailBid  where  BidID in (select bidid from vw_tblsmdpoMasterBid where poid="+PRID+" and bidStatusID>1)	group by bidid) aa";
                    //string strMinAmount = "select min(total)from " +
                    //            "(	select BidID,Total from  " +
                    //            "    ( " +
                    //             "       select bidid,convert(numeric(10,2),(sum(usdtotal))) as Total   " +
                    //              "      from vw_tblsmdpoDetailBid  where  BidID in (select bidid from vw_tblsmdpoMasterBid where poid=" + PRID.ToString() + " and bidStatusID>1)	group by bidid " +
                    //               " ) tbl where Total>0  " +
                    //            ") aa ";
                    //DataTable DtMinAmount = Common.Execute_Procedures_Select_ByQuery(strMinAmount);
                    //if (DtMinAmount != null)
                    //{
                    //    if (DtMinAmount.Rows.Count !=0 )
                    //    {
                    //        MinAmount = Common.CastAsDecimal(DtMinAmount.Rows[0][0]);
                    //    }
                    //}

                    string strTotal = "select convert(numeric(10,2),sum(a.usdtotal)) + convert(numeric(10,2),Isnull(b.EstShippingUSD,0)) + convert(numeric(10,2),Isnull(b.TotalGSTTaxAmountUSD,0)) - convert(numeric(10,2),Isnull(b.TotalDiscountUSD,0)) as Total  from vw_tblsmdpoDetailBid a with(nolock) Inner join VW_tblSMDPOMasterBid b with(nolock) on a.BidID=b.BidID   where  a.BidID=" + drbids["BidID"].ToString() + " group by b.TotalDiscountUSD,b.EstShippingUSD,b.TotalGSTTaxAmountUSD ";
                    DataTable DtTotal = Common.Execute_Procedures_Select_ByQuery(strTotal);
                    if (DtTotal == null || DtTotal.Rows.Count == 0)
                    {
                        table = table + "<td style='padding:4px;font-weight:bold;text-align:right;' colspan='3'> Total</td>";
                        table = table + "<td style='padding:4px;;background-color:D8D8D8;font-weight:bold; text-align:right;'> - </td>";
                    }
                    else
                    {
                        table = table + "<td style='padding:4px;font-weight:bold;text-align:right;' colspan='3'> Total</td>";
                        table = table + "<td style='padding:4px;font-weight:bold; text-align:right;'> $&nbsp;" + DtTotal.Rows[0]["total"].ToString() + "</td>";
                    }
                }
                else if (i == DtPrItems.Rows.Count+4)
                {
                        table = table + "<td style='padding:4px;font-weight:bold;text-align:right;' colspan='3'> Selected Item Total </td>";
                        table = table + "<td style='padding:4px;;background-color:D8D8D8;font-weight:bold; text-align:right;'> $&nbsp;<label Id='lblSelectedItemTotal" + drbids["SRNo"].ToString() + "' >  </label> </td>";
                  
                }
                else if (i == DtPrItems.Rows.Count+5)
                {
                    string strPurchaserComments = " select BidComments from vw_tblSMDPOMasterBid Where BidID= " + drbids["BidID"].ToString();
                    DataTable dtPurchaserComments = Common.Execute_Procedures_Select_ByQuery(strPurchaserComments);
                    if (dtPurchaserComments == null || dtPurchaserComments.Rows.Count == 0)
                    {
                        table = table + "<td style='padding:4px;font-weight:bold;text-align:left;Padding-left:10Px;word-wrap: break-word;' colspan='4'> Purchaser Comments :  </td>";
                       
                    }
                    else
                    {
                        table = table + "<td style='padding:4px;font-weight:bold;text-align:left;Padding-left:10Px;word-wrap: break-word;' colspan='4'> Purchaser Comments : &nbsp;" + dtPurchaserComments.Rows[0]["BidComments"].ToString() + "</td>";
                       
                    }
                }
                else if (i == DtPrItems.Rows.Count+6)
                {
                    string strVendorComments = " select BidPOCommentsVen from vw_tblSMDPOMasterBid Where BidID= " + drbids["BidID"].ToString();
                    DataTable dtVendorComments = Common.Execute_Procedures_Select_ByQuery(strVendorComments);
                    if (dtVendorComments == null || dtVendorComments.Rows.Count == 0)
                    {
                        table = table + "<td style='padding:4px;font-weight:bold;text-align:left;Padding-left:10Px;word-wrap: break-word;' colspan='4'> Vendor Comments :  </td>";

                    }
                    else
                    {
                        table = table + "<td style='padding:4px;font-weight:bold;text-align:left;Padding-left:10Px;word-wrap: break-word;' colspan='4'> Vendor Comments : &nbsp;" + dtVendorComments.Rows[0]["BidPOCommentsVen"].ToString() + "</td>";

                    }
                }
                else if (i == DtPrItems.Rows.Count+7)
                {
                    string strBidClosed = "Select b.FirstName +' '+b.LastName As Username, a.BidFwdOn from Add_tblsmdpomasterBid a with(nolock) inner join usermaster b with(nolock) on a.BidFwdById = b.LoginId Where a.BidID= " + drbids["BidID"].ToString();
                    DataTable dtBidClosed = Common.Execute_Procedures_Select_ByQuery(strBidClosed);
                    if (dtBidClosed == null || dtBidClosed.Rows.Count == 0)
                    {
                        table = table + "<td style='padding:4px;font-weight:bold;text-align:left;Padding-left:10Px;word-wrap: break-word;' colspan='4'> Bid Closed By/On :  </td>";

                    }
                    else
                    {
                        table = table + "<td style='padding:4px;font-weight:bold;text-align:left;Padding-left:10Px;word-wrap: break-word;' colspan='4'> Bid Closed By/On : &nbsp;" + dtBidClosed.Rows[0]["Username"].ToString() + "/" + Convert.ToDateTime(dtBidClosed.Rows[0]["BidFwdOn"].ToString()).ToString("dd-MMM-yyyy")+"</td>";

                    }
                }
                else
                {
                        decimal MinBidAmt = Math.Round(Common.CastAsDecimal(DtPrItems.Rows[i]["MinBidAmt"].ToString()),2);
                        decimal ExchRate = 0;
                        DataTable DtExchRate = Common.Execute_Procedures_Select_ByQuery("SELECT isnull(BIDEXCHRATE,0) FROM VW_TBLSMDPOMASTERBID WHERE BIDID=" + drbids["BidID"].ToString());
                        if(DtExchRate !=null)
                        if (DtExchRate.Rows.Count > 0)
                        {
                            ExchRate = Common.CastAsDecimal(DtExchRate.Rows[0][0]);  
                        }

                        if (ExchRate == 0) { ExchRate = 1; }
                        strUSDTotal = "select BidItemID,BidQty,convert(numeric(10,2),pricefor/" + ExchRate.ToString() + ") as UnitPrice,convert(numeric(10,2),usdtotal) as Total, ISNULL(ProductAccepted,0) As ProductAccepted from vw_tblsmdpoDetailBid  where Recid=" + DtPrItems.Rows[i]["Recid"].ToString() + " and BidID=" + drbids["BidID"].ToString() + "";
                        DtUSDTotal = Common.Execute_Procedures_Select_ByQuery(strUSDTotal);
                        if (DtUSDTotal != null)
                        {
                            if (DtUSDTotal.Rows.Count > 0)
                            {
                                table = table + "<td style='text-align:center;' class='tdChkbox'><input type='checkbox' id='chkItem"+ drbids["SRNo"].ToString() +"' value='" + DtUSDTotal.Rows[0]["BidItemID"].ToString()+ "' onclick=Checked(this) class='form-check-input row"+(i+1)+ " lblrow"+ drbids["SRNo"].ToString() +"" + (i + 1) + "' data-id='" + DtUSDTotal.Rows[0]["BidItemID"] + "' " + getProductAcceptedResult(Common.CastAsInt32(DtUSDTotal.Rows[0]["ProductAccepted"])) + " approvalstatus='" + getApprovalStatusResult(drbids["BidSMDLevel1Approval"].ToString()) + "' /></td>";
                                table = table + "<td style='text-align:right;'> " + DtUSDTotal.Rows[0]["BidQty"] + " </td>";
				decimal Up=Math.Round(Common.CastAsDecimal(DtUSDTotal.Rows[0]["UnitPrice"]), 2);
				if(Up==0)
	                                table = table + "<td style='text-align:right;color:red;'>";
				else
					table = table + "<td style='text-align:right;" + ((MinBidAmt == Common.CastAsDecimal(DtUSDTotal.Rows[0]["UnitPrice"].ToString())) ? "background-color:lightgreen;" : "") + "'>";

                                table = table + Up.ToString() + "";
                                table = table + "</td>";
                                table = table + "<td style='text-align:right;font-weight:bold;'> <label class='lblselectedrow"+ drbids["SRNo"].ToString() +"" + (i + 1) + "' id='lblselectedrow" + drbids["SRNo"].ToString() + "" + (i + 1) + "' ItemAmount='" + Math.Round(Common.CastAsDecimal(DtUSDTotal.Rows[0]["Total"].ToString()), 2) + "'>" + Math.Round(Common.CastAsDecimal(DtUSDTotal.Rows[0]["Total"].ToString()), 2) + " </label> </td>";
                            }
                            else
                            {
                                table = table + "<td style='text-align:right;'>  </td>";
                                table = table + "<td style='text-align:right;'> - </td>";
                                table = table + "<td style='text-align:right;'> - </td>";
                                table = table + "<td style='text-align:right;'> - </td>";
                            }
                        }
                        else
                        {
                            table = table + "<td style='text-align:right;'> - </td>";
                            table = table + "<td style='text-align:right;'> - </td>";
                            table = table + "<td style='text-align:right;'> - </td>";
                        }
                }
            }
            counter = 0;
            table = table + "</tr>";
        }
        table = table + "</table>";
        div.InnerHtml = table;
    }
    protected void Approve_Click(object sender, EventArgs e)
    {
        string BidId = txtParam.Text;
        Common.Execute_Procedures_Select_ByQuery("EXEC sp_NewPR_RequestForApproval " + BidId + "," + Session["loginid"].ToString());
        ScriptManager.RegisterStartupScript(this, this.GetType(), "new", "window.open('RFQDetailsForApproval.aspx?BidId=" + BidId + "');", true);
    }
    protected void Cancel_Click(object sender, EventArgs e)
    {
        string BidId = txtParam.Text;
        Common.Set_ParameterLength(1);
        Common.Set_Parameters(
            new MyParameter("@BidId", Common.CastAsInt32(BidId))
            );
        Boolean res;
        DataSet Ds = new DataSet();
        res = Common.Execute_Procedures_IUD(Ds);
        GetData();
    }

    private string getProductAcceptedResult(int ProductAccepted)
    {
        string ret = "";
        if (ProductAccepted==1)
            ret = "checked";
        return ret;
    }

    private string getApprovalStatusResult(string ApprovalLevel1)
    {
        string ret = "";
        if (!string.IsNullOrWhiteSpace(ApprovalLevel1))
        {
            ret = "Approved";
        }

            
        return ret;
    }

    private int getItemCountforPO(int PoId)
    {
        int i = 0;
        // Code for updating Status column in Database.      
        string query = "Select Count(Distinct RecID) As Count from VW_tblSMDPODetailBid Pod with(nolock) where Pod.BidID in (Select BIdId from vw_tblsmdpoMasterBid po with(nolock) where po.POID = " + PoId + ") ";
        DataTable dtCount = Common.Execute_Procedures_Select_ByQuery(query);
        if (dtCount.Rows.Count > 0)
        {
            i = Convert.ToInt32(dtCount.Rows[0][0]);
        }
        return i;
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    //[WebMethod()]
    public static int UpdateBidItemStatus(string ProductAccepted, int BidItemId)
    {
        int i = 0;
        // Code for updating Status column in Database.
        string conString = ConfigurationManager.ConnectionStrings["eMANAGER"].ConnectionString;
        string query = "UPDATE tblSMDPODetailBid SET ProductAccepted = @ProductAccepted WHERE BidItemId = @BidItemId";
        SqlCommand cmd = new SqlCommand(query);
        using (SqlConnection con = new SqlConnection(conString))
        {
            cmd.Connection = con;
            cmd.Parameters.AddWithValue("@ProductAccepted", ProductAccepted);
            cmd.Parameters.AddWithValue("@BidItemId", BidItemId);
            con.Open();
            i = cmd.ExecuteNonQuery();
            con.Close();
        }
        return i;
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    //[WebMethod()]
    public static int UpdateAllBidItemStatusforBid(string ProductAccepted, int BidId, int PoId)
    {
        int i = 0;
        // Code for updating Status column in Database.
        string conString = ConfigurationManager.ConnectionStrings["eMANAGER"].ConnectionString;

        //string query = "EXEC UpdateAllBidItemStatus BidItemId = @BidItemId";
        //SqlCommand cmd = new SqlCommand(query);
        using (SqlConnection con = new SqlConnection(conString))
        {
            using (SqlCommand cmd = new SqlCommand("UpdateAllBidItemStatus"))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@ProductAccepted", ProductAccepted);
                cmd.Parameters.AddWithValue("@BidId", BidId);
                cmd.Parameters.AddWithValue("@PoId", PoId);
                con.Open();
                i = cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        return i;
    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    //[WebMethod()]
    public static int GetBidItemCount(int BidId)
    {
        int i = 0;
        // Code for updating Status column in Database.      
        string query = "Select count(BidItemID) As Count from VW_tblSMDPODetailBid with(nolock) Where BidID = " + BidId + " and ProductAccepted = 1 ";
        DataTable dtCount = Common.Execute_Procedures_Select_ByQuery(query);
        if (dtCount.Rows.Count > 0)
        {
            Common.Execute_Procedures_Select_ByQuery("EXEC UpdatePOQtyforApproval " + BidId);
            i = Convert.ToInt32(dtCount.Rows[0][0]);
        }
        return i;
    }

    protected void btnCloseReq_Click(object sender, EventArgs e)
    {
        try
        {
            Common.Set_Procedures("sp_SendBackToRFQStage");
            Common.Set_ParameterLength(3);
            Common.Set_Parameters(
                new MyParameter("@POID", PRID),
                new MyParameter("@UserId", Common.CastAsInt32(Session["loginid"])),
                new MyParameter("@Comments", "Closed Quotations")
                );
            DataSet dsData = new DataSet();
            Common.Execute_Procedures_IUD(dsData);

            divCloseReq.Visible = false;
            // btnCloseApprovedPO_OnClick(sender, e);
            

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "tr", "alert('Remaining quotations closed successfully');window.close();", true);
        }
        catch (Exception ex)
        {
            //----------------------------------
            lblmsg.Text = "Unable to close remaining quotations.";
        }
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
        if (PRID > 0)
        {
            sql = "SELECT DocId, DocName As FileName, PoId As RequisitionId, VesselCode, (Select top 1 StatusID from tblSMDPOMaster p where p.PoId= Pod.PoId) As StatusId FROM [tblSMDPODocuments] Pod with(nolock) WHERE  Pod.PoId =" + PRID;
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
            if (PRID > 0)
            {
                sql = "Delete from tblSMDPODocuments  WHERE PoId =" + PRID + " AND DocId = " + DocId;
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

