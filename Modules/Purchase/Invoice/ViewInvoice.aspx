<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewInvoice.aspx.cs" Inherits="Invoice_ViewInvoice" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
     <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="JS/jquery.min.js"></script>
     <script src="JS/AutoComplete/knockout-2.2.1.js" type="text/javascript"></script>
     <!-- Auto Complete -->
     <link rel="stylesheet" href="JS/AutoComplete/jquery-ui.css" />
     <script src="JS/AutoComplete/jquery-ui.js" type="text/javascript"></script>
     <script type="text/javascript" src="JS/KPIScript.js"></script>
     <script type="text/javascript">
           function Clearid() {
             $("#hfdbidid").val("0");
         }
         function RegisterAutoComplete() 
         {
             $(function () {
                 //------------
                 function log(message) {
                     $("<div>").text(message).prependTo("#log");
                     $("#log").scrollTop(0);
                 }
                 //------------
                 $("#txtPoNo").autocomplete(
                 {
                     source: function (request, response) {
                         $.ajax({
                              url: getBaseURL() + "/Modules/Purchase/Invoice/getautocompletedata.ashx",
                           /*  url: getBaseURL() + "/Purchase/Invoice/getautocompletedata.ashx",*/
                             dataType: "json",
                             headers: { "cache-control": "no-cache" },
                             type: "POST",
                             data: { Key: $("#txtPoNo").val(), Type: "PONO" },
                             cache: false,
                             success: function (data) {
                                 response($.map(data.geonames, function (item) { return { label: item.bidponum, value: item.bidponum, bidid: item.bidid} }
                                    ));
                             }
                         });
                     },
                     minLength: 2,
                     select: function (event, ui) {
                         log(ui.item ?
                          "Selected: " + ui.item.label :
                          "Nothing selected, input was " + this.value);
                         //alert(ui.item.bidid);
                         $("#hfdbidid").val(ui.item.bidid);
                         
                     },
                     open: function () {
                         $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
                     },
                     close: function () {
                         $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
                     }
                 });
                 //---------------
                 $("#txtSupplier").autocomplete(
                 {
                     source: function (request, response) {
                         $.ajax({
                             url: getBaseURL() + "/Modules/Purchase/Invoice/getautocompletedata.ashx",
                           /*  url: getBaseURL() + "/Purchase/Invoice/getautocompletedata.ashx",*/
                             dataType: "json",
                             headers: { "cache-control": "no-cache" },
                             type: "POST",
                             data: { Key: $("#txtSupplier").val(), Type: "VEN" },
                             cache: false,
                             success: function (data) {
                                 response($.map(data.geonames, function (item) { return { label: item.SupplierName, value: item.SupplierName, id: item.SupplierId} }
                                    ));
                             }
                         });
                     },
                     minLength: 2,
                     select: function (event, ui) {
                         log(ui.item ? "Selected: " + ui.item.label : "Nothing selected, input was " + this.value);
                         $("#hfdSupplier").val(ui.item.id);
                     },
                     open: function () {
                         $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
                     },
                     close: function () {
                         $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
                     }
                 });
                 //---------------
             });
         }
         function getBaseURL() {
             
             var url = window.location.href.split('/');
             var baseUrl = url[0] + '//' + url[2] + '/' + url[3];
             return baseUrl;
         }
         function Refresh() {
             window.opener.document.getElementById('ctl00_ContentMainMaster_btnSearch').click();
         }
     </script>
    <script type="text/javascript">
        function OpenDocument(CreditNoteId, InvoiceId) {
          //  alert('Hi');
            window.open("../Requisition/ShowDocuments.aspx?CreditNoteId=" + CreditNoteId + "&InvoiceId=" + InvoiceId + "&PRType=CreditNote");
        }
    </script>

        <style type="text/css">
        body
        {
            font-family:Calibri;
            font-size:12px;
            margin:0px;
        }
    .error
    {
        color:Red;
    }
    .hide
    {
        display:none;
    }
            .style1
            {
                width: 66px;
            }
            .style2
            {
                width: 35px;
            }
            .style3
            {
                width: 258px;
            }
            .auto-style1 {
                height: 29px;
            }
            .auto-style2 {
                margin-left: 40px;
            }
            </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ></asp:ToolkitScriptManager>
    <div id="log" style="display:none"></div>
    <div>
    <center>
    <div style="border:solid 1px #008AE6; ">
        
    <table cellpadding="0" cellspacing="0" width="100%">
    <tr>
         <td colspan="2" style="  padding:10px;" class="text headerband">
             <strong>Invoice Details</strong>
         </td>
    </tr>
    <tr>
    <td style="vertical-align:top;">
       <asp:UpdatePanel runat="server" UpdateMode="Always">
       <ContentTemplate>
            <table width="100%" cellpadding="0" cellspacing="2" border="0" >
                <tr>
                    <td style=" background-color:#FFFFCC;padding:10px; border-bottom:solid 1px #dddddd">
                    <asp:Label ID="lbl_inv_Message" runat="server" ForeColor="#C00000"></asp:Label>
                    </td>
                </tr>
            <tr>
            <td style="vertical-align:top; text-align:left;">
                <div style="border:solid 1px #008AE6; ">
                    <table border="0" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="height: 100px; text-align: center; border-collapse:collapse; width:100%;">
                        
                    <tr>
                        <td align="right" style="text-align: right; padding-right:15px; width: 100px;">
                            Ref. #:</td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblRefNo" runat="server" CssClass="input_box" ></asp:Label></td>
                        <td align="right" style="text-align: right; padding-right:15px; width: 100px;">
                            Vendor:</td>
                        <td style="text-align: left;">
                            <asp:Label runat="server" ID="lblSupplier" ></asp:Label>
                            [ <asp:Label runat="server" ID="lblVendorCode" ></asp:Label> ]
                        </td>
                    </tr>
                    <tr  style="background-color:#E6F3FC">
                        <td align="right" style="text-align: right; padding-right:15px; width: 100px;">
                            Inv. #:</td>
                        <td style="text-align: left;">
                            <asp:Label ID="lbl_InvNo" runat="server"></asp:Label> </td>
                        <td align="right" style="text-align: right; padding-right:15px; ">
                            Inv. Date:</td>
                        <td style="text-align: left;">
                            <asp:Label ID="lbl_InvDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="text-align: right; padding-right:15px; ">
                            Due Date:</td>
                        <td style="text-align: left;">
                            <asp:Label ID="lbl_DueDate" runat="server"></asp:Label>  
                        </td>
                        <td align="right" style="text-align: right; padding-right:15px; width: 100px;">
                            Vessel:</td>
                        <td style="text-align: left;">
                            <asp:Label ID="lbl_Vessel" runat="server"></asp:Label>
                    </tr>
                    <tr style="background-color:#E6F3FC">
                        <td align="right" style="text-align: right; padding-right:15px; ">
                        Currency:
                            </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblCurrency"  runat="server" ></asp:Label>
                        </td>
                        <td align="right" style="text-align: right; padding-right:15px; width: 100px;">
                        Inv. Amt.:
                            </td>
                        <td style="text-align: left;">
                              
                            <asp:Label ID="lbl_InvAmount"  runat="server"></asp:Label> </td>
                                 
                    </tr>
                    <tr>
                         
                        <td align="right" style="text-align: right; padding-right:15px; width: 100px;">
                           <%-- Payable Amount:--%>
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lbl_ApprovedAmount"  runat="server" Visible="false"></asp:Label></td>
                        <td align="right" style="text-align: right; padding-right:15px; width: 100px;">
                            Status:</td>
                        <td style="text-align: left;">
                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr style="background-color:#E6F3FC">
                         <div id="divAdvPay1" runat="server" visible="false">
                        <td align="right" style="text-align: right; padding-right:15px; width: 100px;">
                                Advance Payment: 
                           
                            </td>
                        <td style="text-align: left;" >
                            <asp:CheckBox ID="chkAdvPayment" runat="server" Enabled="false" />
                        </td>
                              </div>
                        <div id="divNonPo1" runat="server" visible="false">
                         <td align="right" style="text-align: right; padding-right:15px; width: 100px;">
                                Non PO : 
                            </td>
                        <td style="text-align: left;" >
                            <asp:CheckBox ID="chkNonPO" runat="server" Enabled="false" />
                        </td>
                            </div>
                        <td align="right" style="text-align: right; padding-right:15px; width: 100px;">
                            Attachment:</td>
                        <td style="text-align: left;" >
                            <asp:Label runat="server" ID="btnClipText"/>
                        </td>
                    </tr>

                    <tr id="trNonpoAccount" runat="server" visible="false"> 
                    <td align="right" style="text-align: right; padding-right:15px; width: 100px;">
                        Department :</td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="ddldepartment" runat="server"  Width="174px" 
                                    onselectedindexchanged="ddldepartment_SelectedIndexChanged" AutoPostBack="true" >
                        </asp:DropDownList> 
                    </td>
                    <td align="right" style="text-align: right; padding-right:15px; width: 100px;">
                        Account :</td>
                    <td style="text-align: left;">
                      <asp:DropDownList ID="ddlAccount" runat="server" width="300px" ></asp:DropDownList>
                    </td>
                    </tr>
                    <tr id="trNonpoRemarks" runat="server" visible="false"> 
                    <td align="right" style="text-align: right; padding-right:15px; width: 100px;">
                        Description :</td>
                    <td style="text-align: left;" colspan="3">
                       <asp:TextBox ID="txtNonPoRemarks" runat="server"  Width="90%" TextMode="MultiLine" Height="40px" MaxLength="250"></asp:TextBox></td>
                    </tr>
                         
                    <%--  <tr >
                        <td align="right" style="text-align: right; padding-right:15px; width: 100px;">CLS Invoice :</td>
                        <td style="text-align: left;" colspan="3">
                        <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                        <asp:CheckBox runat="server" ID="chkCLSInvoice" AutoPostBack="true" OnCheckedChanged="chkCLSInvoice_OnCheckedChanged" Text="Check if CLS Cost" Font-Bold="true" ForeColor="Red" />
                        </ContentTemplate>
                        </asp:UpdatePanel>
                        </td>
                    </tr>--%>
                    <tr runat="server" id="trBooked" visible="false" >
                        <td align="right" style="text-align: right; padding-right:15px; width: 100px;">Invoice Booked:</td>
                        <td style="text-align: left;" colspan="3">
                        <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                        <asp:CheckBox runat="server" ID="chkBooked" AutoPostBack="true" OnCheckedChanged="chkBooked_OnCheckedChanged" />
                        </ContentTemplate>
                        </asp:UpdatePanel>
                        </td>
                    </tr>
                    </table>
                    <div id="dvPoDtls" runat="server" visible="false">
                         <div style="padding:5px; text-align:center; " ><b>PO Details</b></div>
                    <table border="1" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="text-align: center; border-collapse:collapse; width:100%;">
                    <tr style=" font-weight:bold;" class= "headerstylegrid">
                    <%-- <td style="width:30px">AP Export</td>--%>
                        <td style="width:50px"></td>
                        <td style="width:150px">PO# / AccountNo</td>
                        <td style="width:100px">PO Status</td>
                        <td style="width:100px">Vendor</td>
                        <td style="width:60px">Currency</td>
                        <td style="width:60px; text-align:right">Amount</td>
                        <td style="">Alert</td>
                        <td style=""></td>
                    </tr>
                    <asp:Repeater runat="server" ID="rprPOS">
                    <ItemTemplate>
                    <tr>
                    <%-- <td><img src="~/Modules/HRD/Images/green_circle.gif" runat="server" visible='<%#(Eval("INTRAV").ToString()=="True")%>'  /><img src="~/Modules/HRD/Images/red_circle.png"  runat="server" visible='<%#(Eval("INTRAV").ToString()!="True")%>'  /></td>--%>
                        <td style="text-align:left">
                <div class='<%#(((Convert.ToInt32(Eval("BidStatusID").ToString())>4 && Convert.ToInt32(Eval("BidStatusID").ToString())>6) && authRecInv.IsUpdate)?"error":"hide")%>'>  
                <a target="_blank" href='../Invoice/InvoiceEntry.aspx?BidId=<%#Eval("BidId")%>'>Close PO</a>
                </div>
                            <%--<a target="_blank" href='../Invoice/InvoiceEntry.aspx?BidId=<%#Eval("BidId")%>'  style='<%#((Convert.ToInt32(Eval("BidStatusID").ToString())>4 )?"":"display:none")%>'> </a>--%>
                        </td>
                        <td><span style='color:red'><b><%#Eval("Ordertype")%></b></span>&nbsp;
                            <a target="_blank" href='../Requisition/VeiwRFQDetailsForApproval.aspx?BidId=<%#Eval("BIDID")%>'><%#Eval("PONO")%> &nbsp;(<%#Eval("ACCOUNTNUMBER")%>)</a>
                            </td>
                        <td style="text-align:left">
                            <%#Eval("BidStatus")%>
                        </td>
                             
                        <td ><%#Eval("APTRAVVENID")%><div class='<%#(Convert.ToInt32(Eval("BidStatusID").ToString()) > 5 && (Eval("APTRAVVENID").ToString()!=lblVendorCode.Text)?"error":"hide")%>'>Mismatch</div></td>
                            
                        <td><%#Eval("BIDCURR")%><div class='<%#((Eval("BIDCURR").ToString()!=lblCurrency.Text)?"error":"hide")%>'>Mismatch</div></td>
                        <td style="text-align:right"><%#Eval("AMT")%></td>
                        <td style="text-align:left"><a title='PO# is already linked with invoice. Click here to open invoice.'  target="_blank" href='ViewInvoice.aspx?InvoiceId=<%#Eval("OTHEROTHERINVID")%>' style='color:Red;<%#((Eval("OTHERREFNO").ToString().Trim()=="")?"display:none":"")%>'>PO already linked with Invoice Ref. # :  ( <%#Eval("OTHERREFNO")%> )</a></td>
                        <td>
                            <asp:Button runat="server" id="btnRecGoods"  onclick="btnViewGoodsRcv_OnClick" ToolTip="Recieve Goods" style="float:left;" CssClass="btn" Height="25px" Width="120px" Text="Recieve Goods" BidId='<%#Eval("BidId")%>' Visible='<%#(Convert.ToInt32(Eval("BidStatusID").ToString())>=3 && Convert.ToInt32(Eval("BidStatusID").ToString())<5 && !Convert.ToBoolean(Eval("IsAdvPayment")))%>'/>
                            <asp:Button runat="server" id="btnEnterInv"  onclick="btnInvoice_OnClick" ToolTip="PO-Invoice Verification" style="float:left;" CssClass="btn" Text="PO-Invoice Verification" Width="150px" Height="25px" BidId='<%#Eval("BidId")%>' Visible='<%#(Convert.ToInt32(Eval("BidStatusID").ToString())==5 && !Convert.ToBoolean(Eval("IsAdvPayment")))%>'/>
                        </td>
                    </tr>
                    </ItemTemplate>
                    </asp:Repeater>
                    </table>
                    </div>
                   
                    <table border="1" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="text-align: center; border-collapse:collapse; width:100%;">
                    <tr >
                    <td style="text-align:left" colspan="4" class="auto-style2">
                        <asp:LinkButton ID="btn_UpdateFile" runat="server" OnClick="btn_UpdateFile_Click" CausesValidation="false" style="color:Blue; font-style:italic;" Text="(Upload Invoice)" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="btn_UpdatePONOView" OnClick="btn_UpdatePONOView_Click" CausesValidation="false" Text="(Update PO#)" style="color:Blue; font-style:italic;" runat="server" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="lnkPrint" OnClick="lnkPrint_Click" CausesValidation="false" Text="( Print )" style="color:Blue; font-style:italic;" runat="server" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                            
                    <asp:LinkButton ID="lbkSendBacktoEntryStage" CausesValidation="false" Text="SendBack to Entry Stage" style="color:Blue; font-style:italic;" runat="server" OnClick="lbkSendBacktoEntryStage_Click" OnClientClick="return confirm('Are you sure to Send Back to Entry stage this Invoice ?');" />
                        &nbsp;&nbsp;

                        <asp:LinkButton ID="lbAddCreditNotes" OnClick="lbAddCreditNotes_Click" CausesValidation="false" Text="(Add CreditNotes)" style="color:Blue; font-style:italic;" runat="server" />
                        <asp:LinkButton ID="lbkExport" OnClick="lnkExport_Click" CausesValidation="false" Text="( Open for AP Export )" style="color:Blue; font-style:italic;" runat="server" Visible="false" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button1" runat="server" Text="Close" Width="80px" OnClick="btn_DocumentClose_Click" CausesValidation="false" Visible="false"/>
                    </td>
                        <div id="dvPoTotal" runat="server" visible="false">
                            <td>Total</td>
                    <td style="text-align:right"><asp:Label runat="server" ID="lblTotalPoAmount"></asp:Label>
                        <asp:Label id="tdPoTotal" runat="server" visible="false" style="color:Red">Mismatch</asp:Label></td>
                    
                        </div>
                    </tr>
                             
                    </table>
                    <div id="divCreditNote" runat="server" visible="false" >
                    <div style="padding:5px; text-align:center; " ><b>Credit Note Details</b></div>
                    <table border="1" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="text-align: center; border-collapse:collapse; width:100%;">
                    <tr style=" font-weight:bold;" class= "headerstylegrid" >
                    <%-- <td style="width:30px">AP Export</td>--%>
                        <td style="width:50px"></td>
                        <td style="width:150px">Credit Note #</td>
                        <td style="width:100px">Currency</td>
                             
                        <td style="width:60px;text-align:right;">Amount</td>
                        <td style="width:100px">Document Name</td>
                        <td style="width:60px; text-align:left">Attachment</td>
                        <td style="width:60px; text-align:left">Delete</td>
                    </tr>
                        <tr>
                            <td colspan="7" style="vertical-align:top;">
                                <asp:Repeater runat="server" ID="rptCreditNoteDetails">
                    <ItemTemplate>
                    <tr>
                    <%-- <td><img src="~/Modules/HRD/Images/green_circle.gif" runat="server" visible='<%#(Eval("INTRAV").ToString()=="True")%>'  /><img src="~/Modules/HRD/Images/red_circle.png"  runat="server" visible='<%#(Eval("INTRAV").ToString()!="True")%>'  /></td>--%>
                        <td style="text-align:left">
                       
                    <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="~/Modules/HRD/Images/edit.jpg" CommandArgument='<%#Eval("CreditNoteId") %>' OnClick="imgEdit_Click" CausesValidation="false"   />
                             
                       
                            <%--<a target="_blank" href='../Invoice/InvoiceEntry.aspx?BidId=<%#Eval("BidId")%>'  style='<%#((Convert.ToInt32(Eval("BidStatusID").ToString())>4 )?"":"display:none")%>'> </a>--%>
                        </td>
                        <td style="text-align:left">
                        <%#Eval("CreditNoteNo")%> 
                            </td>
                        <td style="text-align:left">
                            <%#Eval("CreditNoteCurrency")%>
                        </td>
                             
                        <td style="text-align:right"><%#Eval("CreditNoteAmount")%></td>
                            
                            
                        <td style="text-align:left"><%#Eval("DocName")%></td>
                        <td style="text-align:center">
                            <a onclick='OpenDocument(<%#Eval("CreditNoteId")%>,<%#Eval("InvoiceId")%>)' style="cursor:pointer;">
                                                <img src="../../HRD/Images/paperclip12.gif" />
                            </a>
                        </td>
                        <td>
                            <asp:ImageButton runat="server" ID="btnDeleteCreditNote" ImageUrl="~/Modules/HRD/Images/Delete.jpg" OnClick="btnDeleteCreditNote_Click" Visible='<%#(UserId.ToString()=="1")%>' CommandArgument='<%#Eval("CreditNoteId")%>' OnClientClick="return confirm('Are you sure to delete ?');" />
                        </td>
                             
                    </tr>
                    </ItemTemplate>
                    </asp:Repeater>
                            </td>
                        </tr>
                    <tr style="background-color:#E6F3FC;">
                    <td style="text-align:left;width:200px;" colspan="2" >
                            
                    </td>
                    <td style="text-align:right;width:100px;"> <b> Total Credit Note Amt :</b></td>
                    <td style="text-align:right;width:60px;">
                        <asp:Label id="lblTotalCreditnoteamt" runat="server" Text="0" ></asp:Label>
                        <asp:HiddenField ID="hdnTotalCreditNoteAmt" runat="server" />
                    </td>
                <td colspan="3" style="width:220px;">

                </td>
                    </tr>
                               
                    </table>
                   
                        </div>
                    <div id="divAdvPayment" runat="server" visible="false" >
                        <div style="padding:5px; text-align:center; " ><b>Advance Payment Details</b></div>
                    <table border="1" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="text-align: center; border-collapse:collapse; width:100%;">
                    <tr style=" font-weight:bold;" class= "headerstylegrid">
                    <%-- <td style="width:30px">AP Export</td>--%>
                        <td style="width:50px"></td>
                        <td style="width:150px">Ref #</td>
                        <td style="width:100px">Invoice #</td>
                        <td style="width:100px">Invoice Date</td>
                        <td style="width:100px">Currency</td>
                        <td style="width:60px;text-align:right;">Amount</td>
                    <%-- <td style="width:100px">Document Name</td>
                        <td style="width:60px; text-align:left">Attachment</td>
                        <td style="width:60px; text-align:left">Delete</td>--%>
                    </tr>
                    <asp:Repeater runat="server" ID="rptAdvancePayment">
                    <ItemTemplate>
                    <tr>
                    
                        <td style="text-align:left">
                       
                    <%--  <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="~/Modules/HRD/Images/edit.jpg" CommandArgument='<%#Eval("APid") %>' OnClick="imgEdit_Click" CausesValidation="false"   />--%>
                             
                        <asp:HiddenField ID="hdnAPId" runat="server" Value='<%#Eval("APid")%>' />
                               
                        </td>
                        <td style="text-align:left">
                            <a title='PO# is already linked with invoice. Click here to open invoice.'  target="_blank" href='ViewInvoice.aspx?InvoiceId=<%#Eval("InvoiceId")%>' > <%#Eval("RefNo")%></a>
                                
                            </td>
                        <td style="text-align:left">
                        <%#Eval("InvNo")%> 
                            </td>
                        <td style="text-align:left">
                        <%#Common.ToDateString(Eval("InvDate"))%> 
                            </td>
                        <td style="text-align:left">
                            <%#Eval("Currency")%>
                        </td>
                             
                        <td style="text-align:right"><%#Eval("PaidAmount")%></td>
                            
                            
                <%--     <td style="text-align:left"><%#Eval("DocName")%></td>
                        <td style="text-align:center">
                            <a onclick='OpenDocument(<%#Eval("CreditNoteId")%>,<%#Eval("InvoiceId")%>)' style="cursor:pointer;">
                                                <img src="../../HRD/Images/paperclip12.gif" />
                            </a>
                        </td>
                        <td>
                            <asp:ImageButton runat="server" ID="btnDeleteCreditNote" ImageUrl="~/Modules/HRD/Images/Delete.jpg" OnClick="btnDeleteCreditNote_Click" Visible='<%#(UserId.ToString()=="1")%>' CommandArgument='<%#Eval("CreditNoteId")%>' OnClientClick="return confirm('Are you sure to delete ?');" />
                        </td>--%>
                             
                    </tr>
                    </ItemTemplate>
                    </asp:Repeater>
                        <asp:HiddenField ID="hdnTotalAdvPayment" runat="server" />
            <%--<table border="1" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="text-align: center; border-collapse:collapse; width:100%;">
                    <tr style="background-color:#E6F3FC;">
                    <td style="text-align:left" colspan="4" class="auto-style2">
                            
                    </td>
                    <td style="text-align:right;"><b>Total Advance Payment : </b> </td>
                    <td style="text-align:right;">
                        <asp:Label id="lblTotalAdvPayment" runat="server" Text="0" ></asp:Label></td>
                    </tr>
                             
                    </table>--%>
                    </table>
                    </div>
                          
                    <asp:Panel runat="server" ID="pnl_Approval" Visible="false">
                <asp:UpdatePanel runat="server">
                <ContentTemplate>
                <table border="0" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="height: 100px; text-align: center; border-collapse:collapse; width:100%;">
                    
                <tr>
                    <td  colspan="2" class="text headerband">
                        <strong>Invoice Processing&nbsp; &amp; Forward for Approval</strong></td>
                    </tr>
                <tr>
                    <td align="right" style="text-align: right; padding-right:15px; width: 100px;">
                        Payable Amount: 
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txt_InvAmount"  runat="server" CssClass="required_box" Width="126px" MaxLength="12" style="text-align:right" ValidationGroup="app" AutoPostBack="true" OnTextChanged="ApprovalAmount_OnTextChanged" ReadOnly="true"></asp:TextBox> &nbsp; 
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="app" ControlToValidate="txt_InvAmount" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblApprovedUSDAmount" runat="server" ></asp:Label> ( USD ) &nbsp;&nbsp;&nbsp; Exch Rate. :  <asp:Label ID="lblExchRate" runat="server" ></asp:Label>
                        &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_InvAmount" ErrorMessage="Till 2 decimal places only." ValidationExpression="[-]?\b\d{1,13}\.?\d{0,2}" Display="Dynamic" ValidationGroup="app"></asp:RegularExpressionValidator> <br />
                        (Inv. Amt : <asp:Label ID="lblInvAmt" runat="server" Text="0"></asp:Label>  &nbsp; - (Creditnote Amt : <asp:Label ID="lblCreditNoteAmt" runat="server" Text="0"> </asp:Label> &nbsp;+ Adv. Payment : <asp:Label ID="lblAdvPayment" runat="server" Text="0"> </asp:Label>)) &nbsp;
                    </td>
                </tr>
                </table>
            </ContentTemplate>
            </asp:UpdatePanel>
            <table border="0" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="height: 100px; text-align: center; border-collapse:collapse; width:100%;">
                    <tr style="background-color:#E6F3FC">
                        <td align="right" style="text-align: right; padding-right:15px; width: 100px;">
                            Remarks:</td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txt_AppRemarks" runat="server" Width="80%" TextMode="MultiLine" Height="50px"></asp:TextBox>
                        </td>
                    </tr>
                <tr id="trAppl1"  runat="server" visible="false">
                    <td align="right" style="text-align: right; padding-right:15px; ">Approval 1 :</td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="ddlVerifyForwardTo"  runat="server" CssClass="required_box" Width="164px" ValidationGroup="app" ></asp:DropDownList> <%--<span style='color:#ff0000'> ( < USD 500 )</span>--%>
                    </td>
                </tr>
                <tr style="background-color:#E6F3FC" id="trAppl2"  runat="server" visible="false">
                    <td align="right" style="text-align: right; padding-right:15px; ">Approval 2 :</td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="ddlApproval2"  runat="server" CssClass="required_box" Width="164px" ValidationGroup="app" ></asp:DropDownList>
                        <%--<span style='color:#ff0000'> ( < USD 25000 )</span>--%>
                    </td>
                </tr>
                <%-- <tr id="trAppl3"  runat="server" visible="false">
                    <td align="right" style="text-align: right; padding-right:15px; ">Approval 3 :</td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="ddlApproval3"  runat="server" CssClass="required_box" Width="164px" ValidationGroup="app" ></asp:DropDownList><span style='color:#ff0000'> ( < USD 100000 )</span>
                    </td>
                </tr>
                <tr style="background-color:#E6F3FC" id="trAppl4"  runat="server" visible="false">
                    <td align="right" style="text-align: right; padding-right:15px; ">Approval 4 :</td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="ddlApproval4"  runat="server" CssClass="required_box" Width="164px" ValidationGroup="app" ></asp:DropDownList><span style='color:#ff0000'> ( >= USD 100000 )</span>
                    </td>
                </tr>--%>
                <tr >
                    <td align="right" style="text-align: right; padding-right:15px; ">Account User :</td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="ddlAccountUser"  runat="server" CssClass="required_box" Width="164px" ValidationGroup="app" ></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="app" ControlToValidate="ddlAccountUser" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                
                        <%-- <span style='color:#ff0000'> ( select account user if anyone else )</span>--%>
                           
                    </td>
                </tr>
                </table>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="padding:4px;">
                        &nbsp; 
                        <asp:Button ID="btn_AppSave" runat="server" Text="Save" Width="80px" OnClick="btn_AppSave_Click" ValidationGroup="app" style="  border:none; padding:4px;" CssClass="btn"/>
                        &nbsp;<asp:Button ID="btnAppClose" runat="server" Text="Close" Width="80px" CausesValidation="false" OnClientClick="Refresh();window.close();" style=" border:none; padding:4px;" CssClass="btn"/>
                        &nbsp;
                             
                    </td>
                </tr>

                </table>
    </asp:Panel>
                        
                    <asp:Panel runat="server" ID="pnl_Verification" Visible="false" >
        <table border="0" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="height: 100px; text-align: center; border-collapse:collapse; width:100%;">
                <tr>
                    <td colspan="2" class="text headerband">
                        <strong>Invoice Approval &amp; Foward to Payment</strong></td>
                    </tr>
                <tr style="background-color:#E6F3FC">
                        <td align="right" style="text-align: right; padding-right:15px; width: 100px;">
                            Remarks:</td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtVerifyComments" runat="server"  Width="80%" TextMode="MultiLine" Height="50px"></asp:TextBox>
                        </td>
                </tr>
                <%--  <tr >
                    <td align="right" style="text-align: right; padding-right:15px; ">Forwarded To :</td>
                    <td style="text-align: left;">
                        <asp:DropDownList ID="ddlPaymentForwardTo"  runat="server" CssClass="required_box" Width="164px" ValidationGroup="verify" ></asp:DropDownList>
                        &nbsp;
                        <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="ddlPaymentForwardTo" ErrorMessage="*" MaximumValue="1000000" MinimumValue="1" Type="Integer" ValidationGroup="verify" ></asp:RangeValidator>  
                    </td>
                </tr>--%>
                      
                </table>
                <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="">
                        &nbsp; 
                        <asp:Button ID="btn_VerifySave" runat="server" Text="Save" Width="80px" OnClick="btn_VerifySave_Click" ValidationGroup="verify" style="  border:none; padding:4px;" CssClass="btn" />
                        &nbsp;<asp:Button ID="btn_VerifyCancel" runat="server" Text="Close" Width="80px" CausesValidation="false" OnClientClick="Refresh();window.close();" style="  border:none; padding:4px;" CssClass="btn"/>
                    </td>
                </tr>
                </table>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlApproval" Visible="false" >
        <div style="padding:6px;" class="text headerband">
        <strong>Invoice Approval - <asp:Label ID="lblApprovalLevel" runat="server"></asp:Label></strong>
                        <asp:HiddenField runat="server" ID="hfdAppUserId" />
                    <%-- <asp:HiddenField runat="server" ID="hfdPaymentUserId" />--%>
                        <asp:HiddenField runat="server" ID="hfdAppLevel" />
        </div>
        <table border="1" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="text-align: center; border-collapse:collapse; width:100%;">
                <tr style="background-color:#E6F3FC">
                        <td align="right" style="text-align: left; padding-right:15px; width: 140px;">
                            Remarks:</td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtApprovalRemarks" runat="server"  Width="80%" TextMode="MultiLine" Height="50px" ValidationGroup="apprvng"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rerw" runat="server" ErrorMessage="*" ControlToValidate="txtApprovalRemarks" ValidationGroup="apprvng"></asp:RequiredFieldValidator>
                        </td>
                </tr>
                <%--<tr>
                <td align="right" style="text-align: left; padding-right:15px; width: 140px;">
                            Forward to Payment :</td>
                        <td style="text-align: left;">
                            <asp:Label runat="server" ID="lblForwardedToPaymentNew"></asp:Label>
                        </td>
                       
                </tr>--%>
                </table>
        <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="padding:4px;">
                        &nbsp; 
                        <asp:Button ID="btnApprovalSave" runat="server" Text="Approve" Width="80px" OnClick="btnApprovalSave_Click" ValidationGroup="apprvng" style="  border:none; padding:4px;" CssClass="btn" />
                        &nbsp;<asp:Button ID="btnApprovalClose" runat="server" Text="Close" Width="80px" CausesValidation="false" OnClientClick="Refresh();window.close();" style="  border:none; padding:4px;" CssClass="btn" />&nbsp;
                        <asp:Button ID="btnBackToInvProcess" runat="server" Text="Back to Invoice Processing" Width="170px" CausesValidation="false" style="  border:none; padding:4px;" CssClass="btn" OnClick="btnBackToInvProcess_Click" Visible="false"/>
                    </td>
                </tr>
        </table>
    </asp:Panel>
                </div>
                <div style=" padding:5px;text-align:center;" >
            <asp:LinkButton ID="btnaddnotes" OnClick="btn_AddNotes_Click" CausesValidation="false" Text="Add Notes " style=" font-style:italic;float:left;padding-left:10px;" runat="server" /> 
            <asp:Label ID="lblNotes" runat="server" Text="NOTES" Font-Bold="true"/>
            <asp:LinkButton ID="btnViewAppHistory"  CausesValidation="false" Text="View Approval History" style=" font-style:italic;float:right;padding-right:10px;" runat="server" OnClick="btnViewAppHistory_Click" />
               
    </div>
                <table border="1" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="text-align: center; border-collapse:collapse; width:100%;">
                    <tr style=" font-weight:bold;" class= "headerstylegrid">
                    <td style="width:100px">Enterd By</td>
                    <td style="width:80px">Entered On</td>
                    <td style="width:100px">Stage</td>
                    <td style="text-align:left">Notes</td>
                    </tr>
                    <asp:Repeater runat="server" ID="rptComments">
                    <ItemTemplate>
                    <tr>
                    <td><%#Eval("UserName")%></td>
                    <td><%#Common.ToDateString(Eval("CommentsOn"))%></td>
                    <td><%#Eval("Stage")%></td>
                    <td style="text-align:left"><%#Eval("Comments")%></td>
                    </tr>
                    </ItemTemplate>
                    </asp:Repeater>
                         
                    </table>
                <div style="  padding:5px;text-align:center;" >
    <asp:LinkButton ID="lnkAddDocuments" OnClick="btn_AddDocuments_Click" CausesValidation="false" Text="Add Documents" style=" font-style:italic;float:left;padding-left:10px;" runat="server" />
    <asp:Label ID="lblOtherDocs" runat="server" Text="Other Documents" Font-Bold="true"></asp:Label>
    <strong></strong>
    </div>
                <table border="1" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="text-align: center; border-collapse:collapse; width:100%;">
    <tr class= "headerstylegrid">
    <td>Attachment Name</td>
    <td style="width:80px">Uploaded On</td>
    <td style="width:100px">Download</td>
    <td style="width:40px">Delete</td>
    </tr>
    <asp:Repeater runat="server" ID="Repeater1">
    <ItemTemplate>
    <tr>
    <td style="text-align:left"><%#Eval("AttachmentName")%></td>
    <td><%#Common.ToDateString(Eval("AttachmentOn"))%></td>
    <td>
        <asp:ImageButton runat="server" ID="btnDownload" ImageUrl="~/Modules/HRD/Images/paperclipx12.png" OnClick="btnDownload_Click" CommandArgument='<%#Eval("PK")%>' />
    </td>
    <td>
        <asp:ImageButton runat="server" ID="btnDelete" ImageUrl="~/Modules/HRD/Images/Delete.jpg" OnClick="btnDelete_Click" Visible='<%#((authRecInv.IsDelete) && Eval("Status").ToString() == "UNPAID") %>' CommandArgument='<%#Eval("PK")%>' OnClientClick="return confirm('Are you sure to delete ?');" />
    </td>
    </tr>
    </ItemTemplate>
    </asp:Repeater>
                         
    </table>
                <%-- <div >
                        <table width="100%">
                            <tr>
                                <td style="text-align:center;">
                                    <asp:Button ID="btnSubmitInvoice" OnClick="btnSubmitInvoice_Click" CausesValidation="false" Text="Go to  Invoice Approval" CssClass="btn" runat="server" Visible="true"  />
                                </td>
                            </tr>
                        </table>
                    </div>--%>
                <div style="position:absolute;top:0px;left:0px; height :470px; width:50%; " id="dvNotes" runat="server" visible="false">
    <center>
    <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
    <div style="position:relative;width:600px; padding :5px; text-align :right;background : white; z-index:150;top:180px; border:solid 0px black;">
    <div style=" padding:5px; text-align:center;" class="text headerband">
    <strong>NOTES</strong>
    </div>
        <table border="1" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="text-align: center; border-collapse:collapse; width:100%;">
        <tr style="background-color:#E6F3FC">
        <td style="text-align: left;">
            <asp:TextBox ID="txtStageComments" runat="server"  Width="100%" TextMode="MultiLine" Height="50px" ValidationGroup="fsfsfs"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStageComments" ErrorMessage="*"></asp:RequiredFieldValidator>
        </td>
    </tr>
    </table>
        <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="padding:4px;">
                                <asp:Button ID="btnSaveStageComments" runat="server" Text="Save" Width="80px" OnClick="btnSaveStageComments_Click" ValidationGroup="fsfsfs" style="  border:none; padding:4px;" CssClass="btn" />
                                &nbsp;<asp:Button ID="btnCloseStageComments" runat="server" Text="Close" Width="80px" CausesValidation="false" OnClick="btnCloseStageComments_Click" style="  border:none; padding:4px;" CssClass="btn"/>
                    </td>
                </tr>
            </table>
    </div>
    </center>
    </div>
                <div style="position:absolute;top:0px;left:0px; height :470px; width:50%; " id="dvDocument" runat="server" visible="false">
    <center>
    <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
    <div style="position:relative;width:600px; padding :5px; text-align :right;background : white; z-index:150;top:180px; border:solid 0px black;">
        <iframe runat="server" id="frmUpload1" width="100%" height="200"></iframe>
        <asp:Button ID="Button2" runat="server" Text="Close" Width="80px" OnClick="btn_DocumentClose_Click" CausesValidation="false" style="margin-top:5px; background-color:red; color:White; border:none; padding:4px;"/>
    </div>
    </center>
    </div>
                           
                       
        </td>
    </tr>
    </table> 
    <div style="position:absolute;top:0px;left:0px; height :470px; width:50%; " id="divAppHistory" runat="server" visible="false">
    <center>
    <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
    <div style="position:relative;width:85%;padding :5px; text-align :center;background : white; z-index:150; top:100px; border:solid 0px black;">
    <center >
    <div style="padding:8px; " class="text headerband"> 
        <caption>
            View Approval History
        </caption>
        </div>  
        <div style="padding:5px;background-color:#E6F3FC">
        <table border="1" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="text-align: left; border-collapse:collapse; width:100%;">
        <tr style="font-weight:bold" class= "headerstylegrid">
            <td style="width:50px">Status</td>
            <td style="width:120px">Stage</td>
            <td style="width:200px">By/On</td>
            <td>Remarks</td>                
            <td style="width:80px;">Update User</td>
        </tr>
        <tr>
            <td><asp:Image ID="imgEntry" runat="server" Visible="false" /></td>
            <td>Entry</td>
            <td><asp:Label ID="lblEnteredBy" runat="server" CssClass="input_box"></asp:Label>/<asp:Label ID="lblEnteredOn" runat="server"></asp:Label></td>
            <td><asp:Label ID="lblEntryComments" runat="server" CssClass="input_box" Font-Italic="true" ForeColor="Red"></asp:Label></td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1"><asp:Image ID="imgProcessing" runat="server" Visible="false" /></td>
            <td class="auto-style1">Processing</td>
            <td class="auto-style1"><asp:Label ID="lblProcessingTo" runat="server" CssClass="input_box"></asp:Label>/<asp:Label ID="lblProcessingOn" runat="server"></asp:Label></td>
            <td class="auto-style1"><asp:Label ID="lblProcessingComments" runat="server" CssClass="input_box" Font-Italic="true" ForeColor="Red"></asp:Label></td>
            <td class="auto-style1"><asp:ImageButton runat="server" ID="btnPropose" ImageUrl="~/Modules/HRD/Images/addpencil.gif" OnClick="btnUpdateUserClick" CommandArgument="2" CausesValidation="false" Visible="false"/></td>
        </tr>
        <tr>
            <td><asp:Image ID="imgApproval1" runat="server" Visible="false" /></td>
            <td>Approval 1</td>
            <td><asp:Label ID="lblApproval1By" runat="server" CssClass="input_box"></asp:Label>/<asp:Label ID="lblApproval1On" runat="server"></asp:Label></td>
            <td><asp:Label ID="lblApproval1Comments" runat="server" CssClass="input_box" Font-Italic="true" ForeColor="Red"></asp:Label></td>
            <td><asp:ImageButton runat="server" ID="btnApp1" ImageUrl="~/Modules/HRD/Images/addpencil.gif" OnClick="btnUpdateUserClick" CommandArgument="3" CausesValidation="false" Visible="false"/></td>
        </tr>
            <tr>
            <td><asp:Image ID="imgApproval2" runat="server" Visible="false" /></td>
            <td>Approval 2</td>
            <td><asp:Label ID="lblApproval2By" runat="server" CssClass="input_box"></asp:Label>/<asp:Label ID="lblApproval2On" runat="server"></asp:Label></td>
            <td><asp:Label ID="lblApproval2Comments" runat="server" CssClass="input_box" Font-Italic="true" ForeColor="Red"></asp:Label></td>
            <td><asp:ImageButton runat="server" ID="btnApp2" ImageUrl="~/Modules/HRD/Images/addpencil.gif" OnClick="btnUpdateUserClick" CommandArgument="4" CausesValidation="false" Visible="false"/></td>
        </tr>
        <%-- <tr>
            <td><asp:Image ID="imgApproval3" runat="server" Visible="false" /></td>
            <td>Approval 3</td>
            <td><asp:Label ID="lblApproval3By" runat="server" CssClass="input_box"></asp:Label>/<asp:Label ID="lblApproval3On" runat="server"></asp:Label></td>
            <td><asp:Label ID="lblApproval3Comments" runat="server" CssClass="input_box" Font-Italic="true" ForeColor="Red"></asp:Label></td>
            <td><asp:ImageButton runat="server" ID="btnApp3" ImageUrl="~/Modules/HRD/Images/addpencil.gif" OnClick="btnUpdateUserClick" CommandArgument="5" CausesValidation="false" Visible="false"/></td>
        </tr>
            <tr>
            <td><asp:Image ID="imgApproval4" runat="server" Visible="false" /></td>
            <td>Approval 4</td>
            <td><asp:Label ID="lblApproval4By" runat="server" CssClass="input_box"></asp:Label>/<asp:Label ID="lblApproval4On" runat="server"></asp:Label></td>
            <td><asp:Label ID="lblApproval4Comments" runat="server" CssClass="input_box" Font-Italic="true" ForeColor="Red"></asp:Label></td>
            <td><asp:ImageButton runat="server" ID="btnApp4" ImageUrl="~/Modules/HRD/Images/addpencil.gif"  OnClick="btnUpdateUserClick" CommandArgument="6" CausesValidation="false" Visible="false"/></td>
        </tr>--%>
            <tr>
                <td>
                    <asp:Image ID="imgRFPSubmit" runat="server" Visible="false" />
                </td>
                <td>RFP Submit By</td>
                <td><asp:Label ID="lblRFPSubmitBy" runat="server" CssClass="input_box"></asp:Label>/<asp:Label ID="lblRFPSubmitOn" runat="server"></asp:Label></td>
            <td><asp:Label ID="lblRFPComments" runat="server" CssClass="input_box" Font-Italic="true" ForeColor="Red"></asp:Label></td>
            <td></td>
            </tr>
            <tr>
                <td>
                     <asp:Image ID="imgRFPApprove" runat="server" Visible="false" />
                </td>
                <td>RFP Approve By</td>
                <td><asp:Label ID="lblRFPApproveBy" runat="server" CssClass="input_box"></asp:Label>/<asp:Label ID="lblRFPApproveOn" runat="server"></asp:Label></td>
                <td><asp:Label ID="lblRFPApprovalComments" runat="server" CssClass="input_box" Font-Italic="true" ForeColor="Red"></asp:Label></td>
                <td><asp:ImageButton runat="server" ID="btnRFPApprovalBy" ImageUrl="~/Modules/HRD/Images/addpencil.gif" OnClick="btnUpdateUserClick" CommandArgument="6" CausesValidation="false"/>
                </td>
            </tr>
           <tr>
            <td><asp:Image ID="imgPayment" runat="server" Visible="false" /></td>
            <td>Payment</td>
            <td><asp:Label ID="lblPaymentBy" runat="server" CssClass="input_box"></asp:Label>/<asp:Label ID="lblPaymentOn" runat="server"></asp:Label></td>
            <td><asp:Label ID="lblPaymentComments" runat="server" CssClass="input_box" Font-Italic="true" ForeColor="Red"></asp:Label></td>
            <td><asp:ImageButton runat="server" ID="btnPayment" ImageUrl="~/Modules/HRD/Images/addpencil.gif" OnClick="btnUpdateUserClick" CommandArgument="7" CausesValidation="false"/></td>
        </tr>
        </table>
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="padding:4px;">
                        &nbsp; 
                             
                        &nbsp;<asp:Button ID="Button4" runat="server" Text="Close" Width="80px" CausesValidation="false" OnClientClick="Refresh();window.close();" style="  border:none; padding:4px;" CssClass="btn" OnClick="Button4_Click"/>
                    </td>
                </tr>
        </table>
    </div>
        </center>
         
    </div>
        </center>
        </div>
    <div style="position:absolute;top:0px;left:0px; height :300px; width:50%; " id="dvUpdateUser" runat="server" visible="false">
    <center>
    <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
    <div style="position:relative;width:85%;padding :5px; text-align :center;background : white; z-index:150; top:100px; border:solid 0px black;">
    <center >
    <div style="padding:8px; " class="text headerband"> 
        <caption>
            Select New User From List
        </caption>
        </div>  
        <div style="padding:5px;background-color:#E6F3FC">
        <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
        <td>
            <asp:DropDownList runat="server" id="ddlNewUser" Width="80%" ValidationGroup="rtyu"></asp:DropDownList>
            <asp:RequiredFieldValidator runat="server" ID="fsdfadfa" ControlToValidate="ddlNewUser" ErrorMessage="*" ValidationGroup="rtyu"></asp:RequiredFieldValidator>
        </td>
        </tr>
        </table>
            <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="text-align:center; padding:5px;">
                    <asp:Button ID="btnUpdateUserSave" runat="server" Text="Save" Width="80px" OnClick="btnUpdateUserSave_Click" style="  border:none; padding:4px;" ValidationGroup="rtyu" CssClass="btn"/>
                    <asp:Button ID="btnUpdateUserCancel" runat="server" Text="Close" Width="80px" OnClick="btnUpdateUserCancel_Click" CausesValidation="false" style="  border:none; padding:4px;" CssClass="btn"/>
                </td>
            </tr>
            </table>
        </div>
        </center>
         
    </div>
        </center>
        </div>
            <div style="position:absolute;top:0px;left:0px; height :470px; width:50%; " id="dvPO" runat="server" visible="false">
    <center>
    <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:1; opacity:0.4;filter:alpha(opacity=40)"></div>
    <div style="position:relative;width:400px;padding :5px; text-align :center;background : white; z-index:2; top:100px; border:solid 0px black;">
    <center >
    <div style="padding:8px; " class="text headerband"> 
        <caption>
            Purchase Orders</caption>
        </div>  
        <div style="padding:5px;background-color:#E6F3FC; padding:5px;">
        <asp:Label ID="lbl_popinv_Message" runat="server" ForeColor="#C00000"></asp:Label>
        </div>
        <div style="padding:5px;background-color:#E6F3FC">
        <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
        <td align="right" style="text-align: right; padding-right:15px; width:45px;">
            PO #:
        </td>
        <td style="text-align: left;">
            <asp:TextBox ID="txtPoNo" runat="server" CssClass="input_box" Width="180px" ValidationGroup="re1" onkeydown="Clearid();" ></asp:TextBox>
            <asp:RequiredFieldValidator ID="r1" runat="server" ErrorMessage="*" ControlToValidate="txtPoNo" ValidationGroup="re1" ></asp:RequiredFieldValidator>
            <asp:HiddenField runat="server" ID="hfdbidid" />
        </td>
        <td style="padding:3px">
        <asp:ImageButton runat="server" ID="btnAddPO" ImageUrl="~/Modules/HRD/Images/add1.png" OnClick="btnAddPO_Click" ValidationGroup="re1" />
        </td>
        </tr>
        </table>
        </div>   
            <table cellpadding="5" cellspacing="0" width="100%" border="0">
        <tr  class= "headerstylegrid">
            <td style="text-align:left;">PO#</td>
            <td style="width:120px">Alert</td>
            <td style="width:20px"></td>
        </tr>
        <asp:Repeater runat="server" id="rptPo">
            <ItemTemplate>
            <tr>
            <td style="text-align:left;"><a target="_blank" href='../Requisition/VeiwRFQDetailsForApproval.aspx?BidId=<%#Eval("BIDID")%>'><%#Eval("PONO")%></a></td>
            <td style="width:120px"><a title='PO# is already linked with invoice. Click here to open invoice.' target="_blank" href='ViewInvoice.aspx?InvoiceId=<%#Eval("OTHEROTHERINVID")%>' style='color:Red; float:right;' style='<%#((Eval("OTHERREFNO").ToString().Trim()=="")?"display:none":"")%>'>Used in ( <%#Eval("OTHERREFNO")%> )</a></td>
            <td style="width:20px">
            <asp:ImageButton runat="server" ID="btndelPo" CausesValidation="false" OnClick="btnPO_delete_Click" CommandArgument='<%#Eval("BIDID")%>' Visible='<%# Convert.ToInt32(Eval("BidStatusID"))<6 %>'  ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="return window.confirm('are you sure to delete this? you also need to press save to store in database.');" />
            </td>
            </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr style="background-color:#E6F3FC; text-align:left;">
                <td style="text-align:left;" ><a target="_blank" href='../Requisition/VeiwRFQDetailsForApproval.aspx?BidId=<%#Eval("BIDID")%>'><%#Eval("PONO")%></a></td>
                <td style="width:120px"><a title='PO# is already linked with invoice. Click here to open invoice.'  target="_blank" href='ViewInvoice.aspx?InvoiceId=<%#Eval("OTHEROTHERINVID")%>' style='color:Red; float:right;' style='<%#((Eval("OTHERREFNO").ToString().Trim()=="")?"display:none":"")%>'>Used in ( <%#Eval("OTHERREFNO")%> )</a></td>
                <td style="width:20px">
                <asp:ImageButton runat="server" ID="btndelPo" CausesValidation="false" OnClick="btnPO_delete_Click" CommandArgument='<%#Eval("BIDID")%>' Visible='<%# Convert.ToInt32(Eval("BidStatusID"))<6 %>' ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="return window.confirm('are you sure to delete this? you also need to press save to store in database.');" />
                </td>
                </tr>
            </AlternatingItemTemplate>
        </asp:Repeater>
        </table>
        </center>
        
    <table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="text-align:center; padding:5px;">
            <asp:Button ID="btn_Save" runat="server" Text="Save" Width="80px" OnClick="btn_PONOSave_Click" style="  border:none; padding:4px;" CssClass="btn" />
            <asp:Button ID="Button3" runat="server" Text="Close" Width="80px" OnClick="btn_POClose_Click" CausesValidation="false" style=" border:none; padding:4px;" CssClass="btn"/>
        </td>
    </tr>
    </table>
    </div>
    </center>
    </div>
            <div style="position:absolute;top:50px;left:0px; height :470px; width:50%; " id="dv_UpdateAttachment" runat="server" visible="false">
    <center>
    <div style="position:absolute;top:50px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
    <div style="position:relative;width:600px; padding :5px; text-align :right;background : white; z-index:150;top:180px; border:solid 0px black;">
        <caption>
            <iframe ID="frmUpload" runat="server" height="200" width="100%"></iframe>
            <asp:Button ID="btn_uploadClose" runat="server" CausesValidation="false" 
                OnClick="btn_uploadClose_Click" 
                style="margin-top:5px; background-color:red; color:White; border:none; padding:4px;" 
                Text="Close" Width="80px" />
        </caption>
    </div>
    </center>
    </div>
            </ContentTemplate>
           <Triggers>
               <asp:PostBackTrigger ControlID="Button1" />
              <asp:PostBackTrigger ControlID="Repeater1" />
               <asp:PostBackTrigger ControlID="lbAddCreditNotes" />
               <asp:PostBackTrigger ControlID="rptCreditNoteDetails" />
           </Triggers>
       </asp:UpdatePanel>   
        
    </td>
    <td style="width:50%; vertical-align:top; padding:5px;" runat="server" id="tr_Frm">
     <iframe width="100%" height="600" src="" runat="server" id="frmInvoice"></iframe>
    </td>
    </tr>
    </table>
<div style="position:absolute;top:50px;left:0px; height :470px; width:50%; " id="dVCreditNotes" runat="server" visible="false">
        <center>
            <div style="position:absolute;top:50px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position:relative;width:600px; padding :5px; text-align :right;background : white; z-index:150;top:180px; border:solid 0px black;">
            <center >
            <div style="padding:8px; " class="text headerband"> 
                <caption>
                    Credit Note Details</caption>
                </div>  
                <asp:UpdatePanel ID="upCreditNotes"   runat="server">
                    <ContentTemplate>
                      <div style="padding:5px;background-color:#E6F3FC; padding:5px;">
                <asp:Label ID="lblCreditNoteMsg" runat="server" ForeColor="#C00000"></asp:Label>
              </div>
              <div style="padding:5px;background-color:#E6F3FC">
                <table cellpadding="0" cellspacing="0" width="100%">
                <tr >
                <td align="right" style="text-align: right; padding: 3px 0px 3px 15px; width:120px;">
                    Credit Note # :
                </td>
                <td style="text-align: left;padding: 3px 0px 3px 15px;">
                    <asp:TextBox ID="txtCreditNoteNo" runat="server" CssClass="input_box" Width="180px" ValidationGroup="cn1" MaxLength="20" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="txtCreditNoteNo" ValidationGroup="cn1" ></asp:RequiredFieldValidator>
                    <asp:HiddenField runat="server" ID="hdnCreditNotesId" />
                </td>
               
                </tr>
                     <tr>
                <td align="right" style="text-align: right; padding: 3px 0px 3px 15px; width:120px;">
                    Credit Note Currency :
                </td>
                <td style="text-align: left;padding: 3px 0px 3px 15px">
                    <asp:Label ID="lblCreditNoteCurrency" runat="server"></asp:Label>
                     <%-- <asp:DropDownList ID="ddCurrency"  runat="server" CssClass="input_box" Width="164px"></asp:DropDownList>&nbsp;
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddCurrency" InitialValue="0" Display="Dynamic" ErrorMessage="*" ValidationGroup="cn1"></asp:RequiredFieldValidator>--%>
                 
                   <%-- <asp:HiddenField runat="server" ID="hdnCredi" />--%>
                </td>
               
                </tr>
                    <tr>
                        <td lign="right" style="text-align: right; padding: 3px 0px 3px 15px; width:120px;">
                            Credit Notes Amount :
                        </td>
                        <td style="padding: 3px 0px 3px 15px">
                             <asp:TextBox ID="txtcreditNoteAmt"  runat="server" CssClass="input_box" Width="126px" MaxLength="12" style="text-align:right"></asp:TextBox>&nbsp;
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtcreditNoteAmt" Display="Dynamic" ErrorMessage="*" ValidationGroup="cn1"></asp:RequiredFieldValidator>
                              &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtcreditNoteAmt"
                                  ErrorMessage="Till 2 decimal places only." ValidationExpression="[-]?\d*\.?\d{0,2}" Display="Dynamic" ValidationGroup="cn1"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td lign="right" style="text-align: right; padding: 3px 0px 3px 15px; width:120px;">
                            Credit Note Document : 
                        </td>
                        <td style="padding: 3px 0px 3px 15px">
                          <%--  <asp:UpdatePanel ID="upCreditNoteDoc" runat="server">
                                <ContentTemplate>--%>
                           
                           <asp:FileUpload ID="fuAttachment" runat="server" ValidationGroup="cn1" />
                              <asp:RequiredFieldValidator runat="server" ID="fasfF" ValidationGroup="cn1" ControlToValidate="fuAttachment" ErrorMessage="*"></asp:RequiredFieldValidator>
                                  <%--  </ContentTemplate>
                                 </asp:UpdatePanel>--%>
                        </td>
                    </tr>
                </table>
               </div>  
                 <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="text-align:center; padding:5px;">
                    <asp:Button ID="btnSaveCreditNotes" runat="server" Text="Save" Width="80px" OnClick="btnSaveCreditNotes_Click" style="  border:none; padding:4px;" CssClass="btn" />
                    <asp:Button ID="btnCloseCreditNotes" runat="server" Text="Close" Width="80px" OnClick="btnCloseCreditNotes_Click" CausesValidation="false" style=" border:none; padding:4px;" CssClass="btn"/>
                </td>
            </tr>
            </table>
                </ContentTemplate>
                     <Triggers>
             
               <asp:PostBackTrigger ControlID="btnSaveCreditNotes" />
                          <asp:PostBackTrigger ControlID="btnCloseCreditNotes" />
           </Triggers>
                </asp:UpdatePanel>
            
                
             </center>
        
         
            </div>
    </center>
    </div>
       
    </div>
    </center>
    </div>


    </form>
</body>
<script type="text/javascript">
    function Page_CallAfterRefresh() {
        RegisterAutoComplete();
    }
    function SetImageHeight() {
        var w = $("#frmInvoice").width();
        var h = parseFloat(w) * 1.41;
        if (h > 700)
            h = 700;
        $("#frmInvoice").height(h);
    }
    $(document).ready(function () {
        RegisterAutoComplete();
        SetImageHeight();
    });
    function Refresh() {
        window.opener.document.getElementById('ctl00_ContentMainMaster_btnSearch').click();
    }
    

</script>

</html>
