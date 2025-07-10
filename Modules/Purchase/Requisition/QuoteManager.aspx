<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuoteManager.aspx.cs" Inherits="QuoteManager" ValidateRequest="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
    <link href="../CSS/frqStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../JS/jquery_v1.10.2.min.js"></script>
    

    <link href="../CSS/CalenderStyle.css" rel="Stylesheet" type="text/css" />
    <script src="../JS/Calender.js" type="text/javascript"></script>
    <script src="../JS/jquery.datetimepicker.js" type="text/javascript"></script>
    <link href="../CSS/jquery.datetimepicker.css" rel="stylesheet" type="text/css"  />
     <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
        div.panel {
            border:solid 1px red;
            z-index:99999;
        }
        .bordered tr td {
            border:solid 1px #d7d7d7;
          /* color:#292929;*/
            padding:5px;
        }
        .headerrow td{
            background-color: #545351;
    font-size: 12px;
    font-weight: bold;
    padding: 5px;
    color: #f3f2f2 !important;
        }
        input[type='text'],select
        {
            line-height:18px;
            height:23px;
            padding-left:5px;
            padding-right:5px;
            background-color:#ffea9e;
            font-weight:bold;
            font-size:11px !important;
            text-align:right;
            border:solid 1px #d7d7d7 !important;
        }
        td{
            vertical-align:middle;
        }
    </style>

    <script type="text/javascript">
        function AttachFunc()
        {
            $('.date_only').datetimepicker({ timepicker: false, format: 'd-M-Y', formatDate: 'd-M-Y', allowBlank: true, defaultSelect: false, validateOnBlur: false });

            $(".cal").keyup(function () {

                var UpdateValue = 0;
                var field = $(this).attr("f_name");
                var BidItemID = $(this).attr("BidItemID");
                var BidId = $(this).attr("BidId");
                var RecID = $(this).attr("RecID");
                var curentrate = $(".curentrate").html();
                var ShippingTotalLc = $(".ShippingTotalLc").val();
                var ShippingTotalUSD = $(".ShippingTotalUSD").html();
                var TotalDisPercentage = $(".TotalDisPercentage").val();
                ShippingTotalLc = parseFloat(ShippingTotalLc);
                ShippingTotalUSD = parseFloat(ShippingTotalUSD);
                TotalDisPercentage = parseFloat(TotalDisPercentage);
                var objpricelc = $(this).closest(".row").find(".pricelc");
                var priceusd = $(this).closest(".row").find(".priceusd");
                var VendorDescription = $(this).closest(".row").find(".description");
                var BidQty = $(this).closest(".row").find(".qty");
                var UnitPrice = $(this).closest(".row").find(".unit");  
                var GSTTaxPer = $(this).closest(".row").find(".gstper");
                var GSTLC = $(this).closest(".row").find(".gsttaxlc");
                var GSTUSD = $(this).closest(".row").find(".gsttaxusd");
                
               
                
                BidItemID = parseInt(BidItemID);
                BidId = parseInt(BidId);
                RecID = parseInt(RecID);



                var bq = $(BidQty).val();
                var up = $(UnitPrice).val();
                var gtp = $(GSTTaxPer).val();

                if (isNaN(parseFloat(bq))) {
                    bq = 0.0;
                }
                if (isNaN(parseFloat(up))) {
                    up = 0.0;
                }
                if (isNaN(parseFloat(gtp))) {
                    gtp = 0.0;
                }

                if (parseFloat(bq) < 0) {
                    bq = Math.abs(bq);
                    $(this).closest(".row").find(".qty").val(bq);
                }
                if (parseFloat(up) < 0) {
                    up = Math.abs(up);
                    $(this).closest(".row").find(".unit").val(up);
                }
                if (parseFloat(gtp) < 0) {
                    gtp = Math.abs(gtp);
                    $(this).closest(".row").find(".gstper").val(gtp);
                }

                var vd = $(VendorDescription).val().replace("'","`");
                if (isNaN(parseFloat(bq)))
                {
                    bq = 0.0;
                }
                if (isNaN(parseFloat(up))) {
                    up = 0.0;
                }
                if (isNaN(parseFloat(gtp))) {
                    gtp = 0.0;
                }

                $.ajax({
                    url: 'QuoteManager.aspx/Update_unitpriceAndquantity',
                    data: "{ 'BidItemID': '" + BidItemID + "','BidQty': '" + bq + "','UnitPrice': '" + up + "','BidId': '" + BidId + "','RecID': '" + RecID + "','curentrate': '" + curentrate + "','vendordescription': '" + vd + "','DiscountPercentage': '" + TotalDisPercentage + "','GSTTaxPercentage': '" + gtp + "'}",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        var myobj = JSON.parse(data.d);
                        objpricelc.html(parseFloat(myobj.priceLC).toFixed(2));
                        priceusd.html(parseFloat(myobj.priceUSD).toFixed(2));
                        GSTLC.html(parseFloat(myobj.gsttaxLC).toFixed(2));
                        GSTUSD.html(parseFloat(myobj.gsttaxUSD).toFixed(2));

                        var totPriceLC = 0;
                        var totPriceUsd = 0;
                        var totdiscount = 0;
                        var totdiscountUSD = 0;
                        var totGstTaxLC = 0;
                        var totGstTaxUsd = 0;
                        $(".cal.pricelc").each(function () {
                            totPriceLC = totPriceLC + parseFloat($(this).html());
                        });
                        $(".cal.priceusd").each(function () {
                            totPriceUsd = totPriceUsd + parseFloat($(this).html());
                        });
                        $(".cal.gsttaxlc").each(function () {
                            totGstTaxLC = totGstTaxLC + parseFloat($(this).html());
                        });
                        $(".cal.gsttaxusd").each(function () {
                            totGstTaxUsd = totGstTaxUsd + parseFloat($(this).html());
                        });
                        if (totGstTaxLC > 0) {
                            totPriceLC = totPriceLC + totGstTaxLC;
                        }
                        if (totGstTaxUsd > 0) {
                            totPriceUsd = totPriceUsd + totGstTaxUsd;
                        }
                        if (TotalDisPercentage > 0) {
                            totdiscount = (((totPriceLC) * TotalDisPercentage) / 100);
                            totdiscountUSD = (((totPriceUsd) * TotalDisPercentage) / 100);
                        }
                        totPriceLC = totPriceLC + ShippingTotalLc - totdiscount;
                        totPriceUsd = totPriceUsd + ShippingTotalUSD - totdiscountUSD;
                        $(".TotalGSTLc").html(totGstTaxLC.toFixed(2));
                        $(".TotalGSTUsd").html(totGstTaxUsd.toFixed(2));
                        $(".TotalDisAmount").html(totdiscount.toFixed(2));
                        $(".TotalDisAmountUSD").html(totdiscountUSD.toFixed(2));
                        $(".QuoteTotalLc").html(totPriceLC.toFixed(2));
                        $(".QuoteTotalUsd").html(totPriceUsd.toFixed(2));
                    },
                    error: function (data, status, jqXHR) { alert(jqXHR); }
                });
            });
        }
        $(document).ready(function () {
            AttachFunc();
        })
    </script>
     <script type="text/javascript">
         function OpenDocument(TableID, PoId, VesselCode) {
             // alert(VesselCode);
             window.open("ShowDocuments.aspx?DocId=" + TableID + "&PoId=" + PoId + "&VesselCode=" + VesselCode + "&PRType=''");
         }
     </script>
</head>
<body>
    <form id="form1" runat="server" style="font-family:Arial;font-size:12px;">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>       
      

   <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
        <ContentTemplate>
            
        <div class="FixedHeader">
        
        <div style ="text-align:center;padding:8px;" class="text headerband">
            <span style="font-weight:bold;font-size:18px;" >	REQUEST FOR QUOTE [ RFQ# : <asp:Label ID="lblRFQNO" runat="server"></asp:Label> ] </span><br>
            <asp:Label ID="lblVesselName" runat="server" Visible="false"></asp:Label> 
        </div>
           
        <table cellpadding="5" cellspacing="0" border="0" rules="none"  width="100%" >
            <colgroup>
                <col width="50%" />
                <col />
                <tr>
                    <td style="vertical-align:top;">
                        <table border="0" cellpadding="1" cellspacing="0" rules="none" width="100%">
                            <colgroup>
                                <col width="130px" />
                                <col width="10px" />
                                <col  />
                                <tr>
                                    <td><b>RFQ Date </b></td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="lblDateCreated" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td><b>Vendor Name </b></td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="lblVendorName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td><b>Contact Name </b></td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="lblVendorContactName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td><b>Contact Details </b></td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="lblVendorPhone" runat="server"></asp:Label>
                                        ,
                                        <asp:Label ID="lblVendorEmail" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </colgroup>
                        </table>
                    </td>
                    <td id="makerdetails" runat="server">
                        <table border="0" cellpadding="1" cellspacing="0" rules="none" width="100%">
                            <colgroup>
                                <col width="105px" />
                                <col width="10px" />
                                <col  />
                                <tr>
                                    <td><b>Maker</b></td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="lblMaker" runat="server"></asp:Label>
                                        <span id="myspan"></span></td>
                                </tr>
                                <tr>
                                    <td><b>Equipment</b></td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="lblEquipName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td><b>Model / Type</b></td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="lblEquipModel" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </colgroup>
                        </table>
                    </td>
                    <td id="makerdetails1" runat="server" style="vertical-align:top;">
                        <table border="0" cellpadding="1" cellspacing="0" rules="none" width="100%">
                            <colgroup>
                                <col width="70px" />
                                <col width="10px" />
                                <col  />
                                <tr>
                                    <td><b>Serial No</b></td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td><b>Year Built</b></td>
                                    <td>:</td>
                                    <td>
                                        <asp:Label ID="lblYear" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </colgroup>
                        </table>
                    </td>
                </tr>
            </colgroup>
        </table>
            
              <table cellpadding="3" cellspacing="0" border="0" rules="none" width="100%" style="background-color:#d4d0d0;font-weight:bold;color:#2f2f2f;"  >
                  <colgroup>
                      <col style="width:50px" />
                      <col style="width:10px" />
                      <col style="width:120px" />
                      <%--<col style="width:120px" />--%>
                      <col style="width:80px" />
                      <col style="width:10px" />
                      <col style="width:70px" />
                      <col style="width:100px" />
                      <col style="width:80px" />
                      <col style="width:10px" />
                      <col style="width:250px" />
                      <col/>
                      <tr>
                          <td>Date</td>
                          <td>:</td>
                          <td>
                              <asp:Label ID="txtCurrDate" runat="server"></asp:Label>
                          </td>
                          <%--<td><asp:TextBox ID="TextBox1" runat="server" AutoPostBack="true"  CssClass="date_only" MaxLength="15" OnTextChanged="txtCurrDate_OnTextChanged" Width="100px"></asp:TextBox></td>--%><%--<td>(dd-MMM-yyyy)</td>--%>
                          <td>Currency</td>
                          <td>:</td>
                          <td>
                              <asp:DropDownList ID="ddlCurrency" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCurrency_OnSelectedIndexChanged" Width="65px">
                              </asp:DropDownList>
                          </td>
                          <td>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCurrency" ErrorMessage="Required." ValidationGroup="abc"></asp:RequiredFieldValidator>
                          </td>
                          <td>Exch. Rate</td>
                          <td>:</td>
                          <td>
                              <asp:Label ID="lblCurrRate" runat="server" CssClass="curentrate"></asp:Label>
                          </td>
                          <td style="text-align:right">
                              <asp:ImageButton ID="ImgAttachment" runat="server" ImageUrl="../../HRD/Images/paperclip12.gif" onclick="ImgAttachment_Click" ToolTip="Click to view attached documents" />
                              (<asp:Label ID="lblAttchmentCount" runat="server" Text="0"></asp:Label>
                              ) &nbsp;&nbsp;
                              <asp:LinkButton ID="imgResubmitRFQ" runat="server" OnClick="imgResubmitRFQ_OnClick" OnClientClick="return confirm('Are you sure to allow vendor to resubmit it?');" style="color:red" Text="Allow vendor to Resubmit" />
                          </td>
                      </tr>
                  </colgroup>
                     </table>
<div style="height:35px;overflow-x:hidden;overflow-y:scroll;">
        <table cellspacing="0" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;height:35px;" class="bordered">            
             <colgroup>
                 <col style="width:4%;" />
                 <col />
                 <col style="width:8%;" />
                 <col style="width:8%;" />
                 <col style="width:8%;" />
                 <col style="width:7%;" />
                 <col style="width:5%;" />
                 <col style="width:7%;" />
                 <col style="width:5%;" />
                 <col style="width:8%;" />
                 <col style="width:8%;" />
                 <col style="width:9%;" />
                 <col style="width:8%;" />
                 <tr align="left" class="headerstylegrid">
                     <td>S.No.</td>
                     <td>Description</td>
                     <td>Part#/I.Code</td>
                     <td>Drawing#</td>
                     <td>Code#</td>
                     <td>Bid Qty</td>
                     <td>UOM</td>
                     <td style="text-align:right;">Unit Price</td>
                     <td style="text-align:right;">GST/Tax Per.</td>
                     <td style="text-align:right;">Price(LC)</td>
                     <td style="text-align:right;">GST/Tax(LC)</td>
                     <td style="text-align:right;">Price(USD)</td>
                     <td style="text-align:right;">GST/Tax(USD)</td>
                 </tr>
             </colgroup>
            </table>

        </div>          
        </div>        
        <%-----------------------------------------------------%>        
        <div class="content" style="overflow-x:hidden;overflow-y:scroll;height:240px;">          
             <table cellspacing="0" border="0" class="bordered" cellpadding="0" style="width:100%;border-collapse:collapse;">               
                 <colgroup>
                 <col style="width:4%;" />
                 <col />
                 <col style="width:8%;" />
                 <col style="width:8%;" />
                 <col style="width:8%;" />
                 <col style="width:7%;" />
                 <col style="width:5%;" />
                 <col style="width:7%;" />
                 <col style="width:5%;" />
                 <col style="width:8%;" />
                 <col style="width:8%;" />
                 <col style="width:9%;" />
                 <col style="width:8%;" />
             </colgroup>
             <asp:Repeater ID="rptItems" runat="server">
                 <ItemTemplate>
                     <tr id='tr<%#Eval("recid")%>' class="row">
                         <td style="text-align:center;"><%# Eval("Sno")%>.</td>
                         <td style="text-align :left">
                             <asp:Label ID="txtDesc" runat="server" style="margin-bottom:4px;" Text='<%# Eval("BidDescription")%>' TextMode="MultiLine" Width="100%"></asp:Label>
                             <asp:TextBox ID="txtVendorDesctiption" runat="server" BidId='<%#Eval("BidId") %>' BidItemID='<%#Eval("BidItemID") %>' class="cal description" Enabled="<%#Enable%>" placeholder="Vendor remarks ( if any )" RecID='<%#Eval("recid") %>' style="text-align:left;font-weight:100;color:red;font-style:italic;" Text='<%# Eval("VendorItemDescription")%>' Width="100%"></asp:TextBox>
                         </td>
                         <td><%# Eval("PartNo")%></td>
                         <td><%# Eval("EquipItemDrawing")%></td>
                         <td><%# Eval("EquipItemCode")%></td>
                         <td>
                             <asp:TextBox ID="txtBidQty" runat="server" BidId='<%#Eval("BidId") %>' BidItemID='<%#Eval("BidItemID") %>' CssClass="cal qty" Enabled="<%#Enable%>" RecID='<%#Eval("recid") %>' Text='<%# Eval("bidqty")%>' Width="100%"></asp:TextBox>
                             <%--OnTextChanged="txtUnitPrice_OnTextChanged"--%><%--disabled="<%#Enable%>" --%>
                             <asp:HiddenField ID="hfItemID" runat="server" Value='<%#Eval("BidItemID") %>' />
                         </td>
                         <td><%# Eval("UOM")%></td>
                         <td>
                             <asp:TextBox ID="txtUnitPrice" runat="server" AutoPostBack="false" BidId='<%#Eval("BidId") %>' BidItemID='<%#Eval("BidItemID") %>' CssClass="cal unit" Enabled="<%#Enable%>" MaxLength="10" RecID='<%#Eval("recid") %>' style="text-align:right;" Text='<%# Eval("UnitPrice") %>' Width="100%"></asp:TextBox>
                             <%--OnTextChanged="txtUnitPrice_OnTextChanged"--%></td>
                         <td>
                             <asp:TextBox ID="txtGSTPercentage" runat="server" AutoPostBack="false" BidId='<%#Eval("BidId") %>' BidItemID='<%#Eval("BidItemID") %>' CssClass="cal gstper" Enabled="<%#Enable%>" MaxLength="5" RecID='<%#Eval("recid") %>' style="text-align:right;" Text='<%# Eval("GSTTaxPercentage") %>' Width="100%"></asp:TextBox>
                         </td>
                         <td style="text-align:right;">
                             <asp:Label ID="lblLC" runat="server" CssClass="cal pricelc" Text='<%# ProjectCommon.FormatCurrencyWithoutSign(Eval("EXtpriceLC"))%>'></asp:Label>
                         </td>
                         <td style="text-align:right;">
                             <asp:Label ID="lblGSTLC" runat="server" CssClass="cal gsttaxlc" Text='<%# ProjectCommon.FormatCurrencyWithoutSign(Eval("GSTTaxAmtLC"))%>'></asp:Label>
                         </td>
                         <td style="text-align:right;">$&nbsp;<asp:Label ID="lblUsd" runat="server" CssClass="cal priceusd" Text='<%# ProjectCommon.FormatCurrencyWithoutSign(Eval("EXtpriceUSD"))%>'></asp:Label>
                         </td>
                         <td style="text-align:right;">$&nbsp;<asp:Label ID="lblGSTUSD" runat="server" CssClass="cal gsttaxusd" Text='<%# ProjectCommon.FormatCurrencyWithoutSign(Eval("GSTTaxAmtUSD"))%>'></asp:Label>
                         </td>
                         <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
                     </tr>
                 </ItemTemplate>
                 <AlternatingItemTemplate>
                     <tr id='tr<%#Eval("recid")%>' class="row" style="background-color:#f2eeee;">
                         <td style="text-align:center;"><%# Eval("Sno")%>.</td>
                         <td style="text-align :left">
                             <asp:Label ID="txtDesc" runat="server" style="margin-bottom:4px;" Text='<%# Eval("BidDescription")%>' TextMode="MultiLine" Width="100%"></asp:Label>
                             <asp:TextBox ID="txtVendorDesctiption" runat="server" BidId='<%#Eval("BidId") %>' BidItemID='<%#Eval("BidItemID") %>' class="cal description" Enabled="<%#Enable%>" placeholder="Vendor remarks ( if any )" RecID='<%#Eval("recid") %>' style="text-align:left;font-weight:100;color:red;font-style:italic;" Text='<%# Eval("VendorItemDescription")%>' Width="100%"></asp:TextBox>
                         </td>
                         <td><%# Eval("PartNo")%></td>
                         <td><%# Eval("EquipItemDrawing")%></td>
                         <td><%# Eval("EquipItemCode")%></td>
                         <td>
                             <asp:TextBox ID="txtBidQty" runat="server" BidId='<%#Eval("BidId") %>' BidItemID='<%#Eval("BidItemID") %>' CssClass="cal qty" Enabled="<%#Enable%>" RecID='<%#Eval("recid") %>' Text='<%# Eval("bidqty")%>' Width="100%"></asp:TextBox>
                             <%--OnTextChanged="txtUnitPrice_OnTextChanged"--%>
                             <asp:HiddenField ID="hfItemID" runat="server" Value='<%#Eval("BidItemID") %>' />
                         </td>
                         <td><%# Eval("UOM")%></td>
                         <td>
                             <asp:TextBox ID="txtUnitPrice" runat="server" AutoPostBack="false" BidId='<%#Eval("BidId") %>' BidItemID='<%#Eval("BidItemID") %>' CssClass="cal unit" Enabled="<%#Enable%>" MaxLength="10" RecID='<%#Eval("recid") %>' style="text-align:right;" Text='<%# Eval("UnitPrice") %>' Width="100%"></asp:TextBox>
                             <%--OnTextChanged="txtUnitPrice_OnTextChanged"--%></td>
                         <td>
                             <asp:TextBox ID="txtGSTPercentage" runat="server"  AutoPostBack="false" BidId='<%#Eval("BidId") %>' BidItemID='<%#Eval("BidItemID") %>' CssClass="cal gstper" Enabled="<%#Enable%>" MaxLength="5" RecID='<%#Eval("recid") %>' style="text-align:right;" Text='<%# Eval("GSTTaxPercentage") %>' Width="100%"></asp:TextBox>
                         </td>
                         <td style="text-align:right;">
                             <asp:Label ID="lblLC" runat="server" CssClass="cal pricelc" Text='<%# ProjectCommon.FormatCurrencyWithoutSign(Eval("EXtpriceLC"))%>'></asp:Label>
                         </td>
                         <td style="text-align:right;">
                             <asp:Label ID="lblGSTLC" runat="server" CssClass="cal gsttaxlc" Text='<%# ProjectCommon.FormatCurrencyWithoutSign(Eval("GSTTaxAmtLC"))%>'></asp:Label>
                         </td>
                         <td style="text-align:right;">$&nbsp;<asp:Label ID="lblUsd" runat="server" CssClass="cal priceusd" Text='<%# ProjectCommon.FormatCurrencyWithoutSign(Eval("EXtpriceUSD"))%>'></asp:Label>
                         </td>
                         <td style="text-align:right;">$&nbsp;<asp:Label ID="lblGSTUSD" runat="server" CssClass="cal gsttaxusd" Text='<%# ProjectCommon.FormatCurrencyWithoutSign(Eval("GSTTaxAmtUSD"))%>'></asp:Label>
                         </td>
                         <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
                     </tr>
                 </AlternatingItemTemplate>
             </asp:Repeater>
             </table>
        </div>
        <%-----------------------------------------------------%>        
        <div class="footer">   
            <table cellspacing="0" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;" class="bordered">            
                <colgroup>
                    <col style="width:4%;" />
                    <col />
                    <col style="width:8%;" />
                 <col style="width:8%;" />
                 <col style="width:9%;" />
                 <col style="width:8%;" />
                    <tr align="left" class="headerrow">
                        <td>&nbsp;</td>
                        <td style="text-align:right;">Estd. Shipping/Handling Charges :</td>
                        <td style="text-align:right;">
                            <asp:TextBox ID="txtLCRow1" runat="server" AutoPostBack="true" CssClass="ShippingTotalLc" OnTextChanged="txtLCRow1_OnTextChanged" Width="100%"></asp:TextBox>
                        </td>
                        <td></td>
                        <td style="text-align:right;">$&nbsp;<asp:Label ID="lblUSDRow1" runat="server" CssClass="ShippingTotalUSD"></asp:Label>
                        </td>
                        <td></td>
                    </tr>
                     <tr align="left" class="headerrow">
                        <td>&nbsp;</td>
                        <td style="text-align:right;">Total GST/Tax Amount :</td>
                        <td style="text-align:right;">
                            <asp:Label ID="lblTotalGSTLC" runat="server" CssClass="TotalGSTLc"></asp:Label>
                        </td>
                         <td></td>
                        <td style="text-align:right;">$&nbsp;
                            <asp:Label ID="lblTotalGSTUSD" runat="server" CssClass="TotalGSTUsd"></asp:Label>
                        </td>
                         <td></td>
                    </tr>
                    <tr align="left" class="headerrow">
                        <td style="text-align:right;">&nbsp; </td>
                        <td style="text-align:right;vertical-align:middle;">Discount in % : &nbsp;
                            <asp:TextBox ID="txtTotalDiscount" runat="server" AutoPostBack="true" CssClass="TotalDisPercentage" MaxLength="3" OnTextChanged="txtTotalDiscount_OnTextChanged" Width="50px"></asp:TextBox>
                            &nbsp; </td>
                        <td style="text-align:right;">
                            <asp:Label ID="lblTotalDiscount" runat="server" CssClass="TotalDisAmount"></asp:Label>
                            &nbsp;</td>
                        <td></td>
                        <td style="text-align:right;">$&nbsp;<asp:Label ID="lblTotalDiscountUSD" runat="server" CssClass="TotalDisAmountUSD"></asp:Label>
                        </td>
                        <td></td>
                    </tr>
                    <tr align="left" class="headerrow">
                        <td>&nbsp;</td>
                        <td style="text-align:right;">Total : </td>
                        <td style="text-align:right;">
                            <asp:Label ID="lblLCRow2" runat="server" CssClass="QuoteTotalLc"></asp:Label>
                        </td>
                        <td></td>
                        <td style="text-align:right;">$&nbsp;<asp:Label ID="lblUSDRow2" runat="server" CssClass="QuoteTotalUsd"></asp:Label>
                        </td>
                        <td></td>
                    </tr>
                </colgroup>
            </table>
            <div style="background-color:#d4d0d0;font-weight:bold;padding:2px;color:#333;">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" >
                    <tr>
                        <td>
                     <table cellpadding="3" cellspacing="0" border="0"  rules="none" width="100%"  >
                         <colgroup>
                             <col style="width:130px" />
                             <col style="width:10px" />
                             <col style="width:120px" />
                             <col style="width:100px" />
                             <col style="width:10px" />
                             <col style="width:140px" />
                             <col style="width:130px" />
                             <col style="width:10px" />
                             <col style="width:130px" />
                             <col style="width:100px" />
                             <col style="width:10px" />
                             <col style="width:100px" />
                             <col/>
                             <tr>
                                 <td><b>Delivery Date</b></td>
                                 <td>:</td>
                                 <td>
                                     <asp:TextBox ID="txtDeliveryDate" runat="server" CssClass="date_only" MaxLength="15" Width="100px"></asp:TextBox>
                                 </td>
                                
                                 <td><b>Delivery Port</b></td>
                                 <td>:</td>
                                 <td>
                                     <asp:TextBox ID="txtDeliveryPort" runat="server" MaxLength="15" style="text-align:left;" Width="140px"></asp:TextBox>
                                 </td>
                                 <td><b>Quote Ref No. #</b></td>
                                 <td>:</td>
                                 <td>
                                     <asp:TextBox ID="txtVenRef" runat="server" style="text-align:left;" Width="140px"></asp:TextBox>
                                 </td>
                                 <td style="text-align:right"><b>Quote Expiry :</b></td>
                                 <td>:</td>
                                 <td style="text-align:left;">
                                     <asp:TextBox ID="txtExpires" runat="server" CssClass="date_only" MaxLength="15" Width="100px"></asp:TextBox>
                                 </td>
                                 <td></td>
                             </tr>
                         </colgroup>
                         </table>
                     <table cellpadding="3" cellspacing="0" border="0"  rules="none" width="100%"  >
                         <colgroup>
                             <col style="width:130px" />
                             <col style="width:10px" />
                             <col/>
                             <tr>
                                 <td>Vendor Comments</td>
                                 <td>:</td>
                                 <td style="text-align:left">
                                     <asp:TextBox ID="txtVendorComments" runat="server" style="text-align:left;" Width="100%"></asp:TextBox>
                                 </td>
                             </tr>
                         </colgroup>
                    </table>
                    </td>
                        <td>
                            <asp:Button runat="server" Width="85px" Height="40px" ID="btnSubmit" Text="Save" OnClick="imgSave_OnClick" ValidationGroup="abc" OnClientClick="return confirm('Are you sure to submit your quote ?\n Please confirm once before send.');"  style="text-align:center;background-color:#2da1f3;font-size:16px;font-weight:bold;border:none;"  />
                            <asp:Button ID="imgPrint" runat="server" Height="40px" Text="Print" style="border:none;text-align:center;background-color:#2da1f3;font-size:16px;font-weight:bold;width:85px;"  />
                        </td>
                    </tr>
                    </table>
            </div>
        </div>
        <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;font-family:Arial;font-size:12px;" runat="server" id="divAttachment" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px;width:100%; height:100%; background-color :black;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position :relative; width:500px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px;  ;opacity:1;filter:alpha(opacity=100)">
        <center>
        <br />
        <div class="text headerband"> <b>Attached Documents</b> 
             <asp:ImageButton ID="imgClosePopup" runat="server" ImageUrl="~/Modules/HRD/Images/Close.gif"  title="Close this Window."  style="float:right;"  CssClass="btn" OnClick="imgClosePopup_Click" />
        </div>
        <br /><br />
        <div style="overflow-y: scroll; overflow-x: scroll;height:150px;">

                                 
                               <table cellpadding="2" cellspacing="0" width="98%" style="margin:auto;" >
                                   <colgroup>
                                       <col width="50px" />
                                       <col />
                                       <col width="90px" />
                                       <tr class="headerstylegrid" style="font-weight:bold;">
                                           <td ></td>
                                           <td >File Name</td>
                                           <td >Attachment</td>
                                       </tr>
                                       <asp:Repeater ID="rptDocuments" runat="server">
                                           <ItemTemplate>
                                               <tr>
                                                  <td style="text-align:center;">
                                                       <asp:ImageButton ID="ImgDelete" runat="server" ImageUrl="~/Modules/HRD/Images/delete_12.gif" OnClick="ImgDelete_Click" CommandArgument='<%#Eval("DocId")%>' visible='<%#Common.CastAsInt32(Eval("StatusId")) == 0 %>' />
                                                  </td>
                                                   <td style="text-align:left;padding-left:5px;"><%#Eval("FileName")%>
                                                       <asp:HiddenField ID="hdnDocId" runat="server" Value='<%#Eval("DocId")%> ' />
                                                   </td>
                                                   <td style="text-align:center;"> 
                                                    <%--   <asp:ImageButton ID="ImgAttachment" runat="server" ImageUrl="~/Images/paperclip.gif" OnClick="ImgAttachment_Click" CommandName='<%#Eval("DocId")%> ' />--%>

                                                        <a onclick='OpenDocument(<%#Eval("DocId")%>,<%#Eval("RequisitionId")%>,"<%#Eval("VesselCode")%>")' style="cursor:pointer;">
                                                       <img src="../../HRD/Images/paperclip12.gif" />
                                                       </a>
                                                   </td>
                                               </tr>
                                           </ItemTemplate>
                                       </asp:Repeater>
                                   </colgroup>
                        </table>
                                     </div>
        <asp:Button ID="btnPopupAttachment" runat="server" CssClass="btn" onclick="btnPopupAttachment_Click" Text="Cancel" CausesValidation="false" Width="100px" />
         </center>
        </div> 
    </center>
    </div>
        </ContentTemplate> 
    </asp:UpdatePanel>    
    </form>
    <script type="text/javascript">
        function settop()
        {
            $(".content").css("margin-top", $(".FixedHeader").height() + 'px');
            $(document.body).css("margin-bottom", ($(".footer").height() + 30) + 'px');
        }
        $(document).ready(function () {
            settop();
        });
        $(window).resize(function () {
            settop();
        });
    </script>
     <script type="text/javascript">
    function SetSelectedTaskID(ctrl) {
        $("#hfSelectedTaskID").val($(ctrl).attr("taskid"));
        $("#Temp").click();
    }
</script>
</body>
</html>
