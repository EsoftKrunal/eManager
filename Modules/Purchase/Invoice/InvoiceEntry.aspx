<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="InvoiceEntry.aspx.cs" Inherits="Modules_Purchase_Invoice_InvoiceEntry"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/Modules/Purchase/UserControls/PODropDown.ascx" tagname="PODropDown" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
    <link rel="Stylesheet"  type="text/css" href="../../HRD/Styles/StyleSheet.css"/>
    <%-- <link rel="Stylesheet"  type="text/css" href="../CSS/style.css"/>--%>
    <script type="text/javascript" src="../JS/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../JS/Common.js"></script>
    <script type="text/javascript" >

       function fncInputNumericValuesOnly(evnt)

       {
           
           if (!(event.keyCode == 45 || event.keyCode == 46 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57))

         {

               event.returnValue=false;

         }

       }
        function ConfirmAndBackOrder()
        {
             var Confirm= confirm('Are you sure to confirm invoice?');
             var hfConfirm=document.getElementById('hfConfirm');
             var hfCreatBackOrder=document.getElementById('hfCreatBackOrder');
             
             var QtyInv=document.getElementById('QtyInv').innerHTML;
             var QtyOrder=document.getElementById('lblQtyOrder').innerHTML; 
             
             if(Confirm==true)
             {  hfConfirm.value='True';}
             else
             {  hfConfirm.value='False';return false;}
             
             var QtyOrder_float=parseFloat(QtyOrder);
             var QtyInv_float=parseFloat(QtyInv);
             if(isNaN(QtyOrder_float)){QtyOrder_float=0;} 
             if(isNaN(QtyInv_float)){QtyInv_float=0;} 
             if(QtyOrder_float>QtyInv_float)
             {            
                 var ConfirmBackOrder= confirm('Are you sure to create backorder ?');
                 if (ConfirmBackOrder==true)
                 {
                    hfCreatBackOrder.value='1';
                 }
                 else
                 {
                    hfCreatBackOrder.value='0';
                 }
             }
             else
             {  
             }
             
         return true;    
        }


     
    </script>
    <style type="text/css">
        .text-field {
    border: 2px solid #ccc; / Default border color /
    padding: 5px;
    text-align: right;
    transition: border-color 0.3s ease; / Transition effect for smoother color change /
}

.positive-value {
    
    background: red ;
    color: #fff;
}

.negative-value {
     color: #fff;
     background: green;
}
        </style>
    <script type="text/javascript" src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $('#txtCreditFor').on('input', function () {
            var value = parseFloat($(this).val());
            if (!isNaN(value)) {
                if (value > 0) {
                    $(this).removeClass('negative-value').addClass('positive-value');
                    $(this).focus();
                    $(this).val('');
                   /* alert('Please enter credit amount with Minus (-). i.e. -1234.00.');*/
                } 
            } 
        });
    });


</script>

       <script type="text/javascript">



           <%--function checkChange(val) {
               alert('Hi');
             //  let creditfor = document.getElementById("<%=txtCreditFor.ClientID%>").value;
               if (parseFloat(val) > 0)//To check if this change is a first time change
               {
                   alert('Please enter credit amount with Minus (-). i.e. -1234.00.!');
                   document.getElementById('txtCreditFor').value = "";
                   document.getElementById('txtCreditFor').focus();
               }
               
           }--%>
           //$("input[type='text'][name='txtCreditFor']").change(function () {
           //    alert('Hi');
           //    if ($(this).val() > 0) {
           //        alert("Please enter credit amount with Minus (-). i.e. -1234.00.");
           //        $(this).val('');
           //        $(this).focus();
           //    }
           //}); 
       </script>
    <style type="text/css">
        .auto-style1 {
            height: 178px;
        }
    </style>
    <script type="text/javascript">
        function OpenDocument(TableID, PoId, VesselCode) {
            // alert(VesselCode);
            window.open("Requisition/ShowDocuments.aspx?DocId=" + TableID + "&PoId=" + PoId + "&VesselCode=" + VesselCode + "&PRType=''");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-family:Arial;font-size:12px;">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>  
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style="vertical-align: top; text-align: left;">
        <table style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:left; width: 100%" border="0" cellpadding="0" cellspacing="0">
        <col width="40%"/>
        <col width="60%"/>
        <tr class="text headerband" style="text-align:left;">
            <td style="vertical-align:bottom; padding:4px;" >
                
            </td>
            <td  style="padding:4px;" >
            
            Invoice Confirmation 
            [
            <asp:Label runat="server" ID="lblPONO"></asp:Label> 
            ]
                <div style="float:right;padding-right:5px;">
                     <asp:ImageButton ID="ImgAttachment" runat="server" ImageUrl="../../HRD/Images/paperclip12.gif" onclick="ImgAttachment_Click" ToolTip="Click to view attached documents" />
                              (<asp:Label ID="lblAttchmentCount" runat="server" Text="0"></asp:Label>) &nbsp;&nbsp;
                </div>
                 <%--<asp:ImageButton runat="server" ID="btnBack"  ImageUrl="~/Images/home.png" style="float :right; padding-right :5px;background-color : Transparent;" PostBackUrl="~/OrdersList.aspx" />--%>
            </td>
        </tr>
        <tr>
            <td >
                &nbsp;
            </td>
            <td style="text-align:right; padding:1px;">
            
            </td>
        </tr>
        <tr>
            <td colspan="2">
               
            </td>
        </tr>
        </table>
        </td> 
        </tr> 
        </table> 
                <div style="border:2px solid #4371a5;" >
            <table cellSpacing="0" cellPadding="0" width="100%" border="0" >
        <tr>
           <td>
                <asp:HiddenField ID="hfCreatBackOrder" runat="server" Value="0" />
                <asp:HiddenField ID="hfConfirm" runat="server" Value="0" />
           </td>
        </tr>
        <tr>
            <td>
               <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                    <colgroup>
                        <col style="width:50px;" />
                        <col  />
                        <%--<col style="width:120px;" />
                        <col style="width:120px;" />
                        <col style="width:120px;" />--%>
                        <col style="width:80px;" />
                        <col style="width:70px;" />
                        <col style="width:70px;" />
                        <col style="width:80px;" />
                        <col style="width:100px;" />
                        <col style="width:100px;" />
                        <col style="width:100px;" />
                        <col style="width:17px;"/> 
                        <tr align="left" class= "headerstylegrid">
                            <td>S.No</td>
                            <td>Description</td>
                            <%--<td>
                               Part#</td>
                            <td style="text-align:left;">
                                Drawing#</td>
                            <td>
                                Code#</td>--%>
                            <td>Unit Type</td>
                            <td>Qty Order</td>
                            <td>Qty Rec'd</td>
                            <td>Qty Inv</td>
                            <td>Invoice Unit <br />Price(LC) </td>    
                            <td>Invoice <br />Price(LC) </td>    
                            <td>Invoice <br />Price(USD Eq.) </td>    
                            <td></td>
                        </tr>
                    </colgroup>
                </table>
               <div id="dvscroll_Invoice" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 200px ; text-align:center;" onscroll="SetScrollPos(this)">
               <asp:HiddenField ID="hfPRID" runat="server" Value="0" />
               <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                    <colgroup>
                        <col style="width:50px;" />
                        <col  style="text-align:left;"/>
                        <%--<col style="width:120px;" />
                        <col style="width:120px;" />
                        <col style="width:120px;" />--%>
                        <col style="width:80px; text-align:center;" />
                        <col style="width:70px;" />
                        <col style="width:70px;" />
                        <col style="width:80px;"/>
                        <col style="width:100px;"/>
                        <col style="width:100px;"/>
                        <col style="width:100px;"/>
                        <col style="width:17px;"/> 
                    </colgroup>
                    <asp:Repeater ID="RptinvoiceEntry" runat="server">
                        <ItemTemplate>
                             <tr id='tr<%#Eval("BidItemID")%>' >
                                <td>  <%#Eval("Row")%></td> 
                                <td style="text-align:left" ><%# Eval("BidDescription")%> <br />
                                    <b>Part#: </b><%# Eval("PartNo")%>&nbsp;&nbsp;
                                    <b>Drawing#:</b><%# Eval("EquipItemDrawing")%>&nbsp;&nbsp;
                                    <b>Code#:</b><%# Eval("EquipItemCode")%>&nbsp;&nbsp;
                                </td>
                                <%--<td>
                                    <%# Eval("PartNo")%>
                                </td>
                                <td >
                                    <%# Eval("EquipItemDrawing")%>
                                </td>
                                <td>
                                    <%# Eval("EquipItemCode")%>
                                </td>--%>
                                <td style="text-align :left;"><%#Eval("uom")%></td> 
                                
                                <td>
                                    <%--<asp:TextBox ID="txtQtyPO" runat="server" Text='<%#Eval("QtyPO")%>' Width="60px" OnTextChanged="txtQtyPO_OnTextChanged" AutoPostBack="true" onkeypress='numVal(this)' onkeyup='numVal(this)'></asp:TextBox>--%>
                                    <asp:Label ID="lblQtyPO" runat="server" Text='<%#Eval("QtyPO")%>' Width="60px" OnTextChanged="lblQtyPO_OnTextChanged" AutoPostBack="true" onkeypress='numVal(this)' onkeyup='numVal(this)'></asp:Label>
                                    <asp:HiddenField ID="hfQtyPo" runat="server" Value='<%#Eval("QtyPO")%>' />
                                    <asp:HiddenField ID="hfBidItmID" runat="server" Value='<%#Eval("BidItemID")%>' />
                                    <asp:HiddenField ID="hfUsdPoTotal" runat="server" Value='<%#Eval("usdpoTotal")%>' />
                                </td>
                                <td>
                                    <asp:Label ID="lblRcvQty" runat="server" Text ='<%#Eval("QtyRecd")%>'  ></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox id="txtQtyInv" Width="70px" runat="server" style="text-align :right" Text='<%# Eval("QtyInv")%>' OnTextChanged="txtQtyInv_OnTextChanged " AutoPostBack="true"  MaxLength="10" onkeypress='fncInputNumericValuesOnly(event)'></asp:TextBox> 
                                </td>
                                <td>
                                    <asp:TextBox ID="txtInvPriceFor" runat="server" style="text-align :right" Text='<%# Eval("InvPriceFor")%>' OnTextChanged="lblInvPriceFor_OnTextChanged" MaxLength="10" AutoPostBack="true" Width="70px" onkeypress='fncInputNumericValuesOnly(event)'></asp:TextBox >
                                    <%--<asp:Label ID="lblInvPriceFor" runat="server" Text='<%# ProjectCommon.FormatCurrencyWithoutSign(Eval("InvPriceFor"))%>'></asp:Label>--%>
                                    </td>
                                <td>
                                    <asp:Label ID="lblInvForTotal" runat="server" Text='<%# ProjectCommon.FormatCurrencyWithoutSign(Eval("InvForTotal"))%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblInvUSDTotal" runat="server" Text='<%# ProjectCommon.FormatCurrencyWithoutSign( Eval("InvUSDTotal"))%>'></asp:Label>
                                </td>
                                <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr id='tr<%#Eval("recid")%>' class="alternaterow" >
                                <td>  <%#Eval("Row")%></td> 
                                <td style="text-align:left"><%# Eval("BidDescription")%> <br />
                                    <b>Part#: </b><%# Eval("PartNo")%>&nbsp;&nbsp;
                                    <b>Drawing#:</b><%# Eval("EquipItemDrawing")%>&nbsp;&nbsp;
                                    <b>Code#:</b><%# Eval("EquipItemCode")%>&nbsp;&nbsp;
                                </td>
                                <%--<td>
                                    <%# Eval("PartNo")%>
                                </td>
                                <td >
                                    <%# Eval("EquipItemDrawing")%>
                                </td>
                                <td>
                                    <%# Eval("EquipItemCode")%>
                                </td>--%>
                                <td style="text-align :left;"><%#Eval("uom")%></td> 
                                
                                <td>
                                    <%--<asp:TextBox ID="txtQtyPO" runat="server" Text='<%#Eval("QtyPO")%>' Width="60px" OnTextChanged="txtQtyPO_OnTextChanged" AutoPostBack="true" onkeypress='numVal(this)' onkeyup='numVal(this)'></asp:TextBox>--%>
                                    <asp:Label ID="lblQtyPO" runat="server" Text='<%#Eval("QtyPO")%>' Width="60px" OnTextChanged="lblQtyPO_OnTextChanged" AutoPostBack="true" onkeypress='numVal(this)' onkeyup='numVal(this)'></asp:Label>
                                    <asp:HiddenField ID="hfQtyPo" runat="server" Value='<%#Eval("QtyPO")%>' />
                                    <asp:HiddenField ID="hfBidItmID" runat="server" Value='<%#Eval("BidItemID")%>' />
                                    <asp:HiddenField ID="hfUsdPoTotal" runat="server" Value='<%#Eval("usdpoTotal")%>' />
                                </td>
                                <td>
                                    <asp:Label ID="lblRcvQty" runat="server" Text ='<%#Eval("QtyRecd")%>' ></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox id="txtQtyInv" Width="70px" runat="server" style="text-align :right" Text='<%# Eval("QtyInv")%>' OnTextChanged="txtQtyInv_OnTextChanged" AutoPostBack="true" MaxLength="10"  onkeypress='fncInputNumericValuesOnly(event)'></asp:TextBox> 
                                </td>
                                <td>
                                    <asp:TextBox ID="txtInvPriceFor" runat="server" style="text-align :right"  Text='<%# Eval("InvPriceFor")%>' OnTextChanged="lblInvPriceFor_OnTextChanged" MaxLength="10" AutoPostBack="true" Width="70px" onkeypress='fncInputNumericValuesOnly(event)' ></asp:TextBox >
                                    <%--<asp:Label ID="lblInvPriceFor" runat="server" Text='<%# ProjectCommon.FormatCurrencyWithoutSign(Eval("InvPriceFor"))%>'></asp:Label>--%>
                                </td>
                                <td>
                                    <asp:Label ID="lblInvForTotal" runat="server" Text='<%# ProjectCommon.FormatCurrencyWithoutSign(Eval("InvForTotal"))%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblInvUSDTotal" runat="server" Text='<%# ProjectCommon.FormatCurrencyWithoutSign( Eval("InvUSDTotal"))%>'></asp:Label>
                                </td>
                                <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                </table>
                
               </div>
            </td>
        </tr>
        </table>
        <table>
        <tr>
            <td>
                </td>
            </tr>
        </table>
        </div>
            <div style="border:2px solid #4371a5;" >
            <table cellspacing="0" cellpadding="0" width="100%" border="1" >
                <colgroup>
                    <tr>
                        <td style="vertical-align:top;">
                            <table cellpadding="0" cellspacing="1" width="100%">
                                <tr>
                                    <td class="text headerband" style="height:15px;" >
                                        Vendor Details
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Vendor Name :</b>
                                        <asp:Label ID="lblVendor" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Phone #: </b>
                                        <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Contact #: </b>
                                        <asp:Label ID="lblContact" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Email #: </b>
                                        <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:center">
                                        <asp:LinkButton ID="imgPOSVendor" runat="server" CommandArgument='<%#Eval("jeid")%>' ImageUrl="~/Images/edit1.png" ToolTip="Update Vendor." OnClick="imgUpdateVendor_OnClick" Text="Update Vendor" Visible="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <%--Invoice Details--%>
                        <td style="vertical-align:top;">
                            <table cellpadding="0" cellspacing="1" width="100%">
                                <tr>
                                    <td class="text headerband" colspan="3" style="height:15px;" >
                                        Invoice Details
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Invoice Number :
                                    </td>
                                    <td>
                                         <%--<uc1:PODropDown ID="txtInvNo" runat="server"  />--%>
                                         <asp:TextBox ID="txtInvNo" runat="server" MaxLength="50" ></asp:TextBox>
                                         <%--<asp:ImageButton runat="server" ID="btnAddInv" ImageUrl="~/Images/add.png" ToolTip="Add more invoice(s) to this PO" onclick="btnAddInv_Click"/>  --%>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                    <asp:GridView runat="server" ID="grdInvoiceList" AutoGenerateColumns="false" width="100%">
                                    <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div style='width:100%;background-color:<%#Eval("Color") %>' >
                                                <asp:CheckBox runat="server" ID="chkClear" /> 
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice#" ItemStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="Currency" HeaderText="Curr." ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="Currency" HeaderText="Curr." ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Center" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("Amount") %>' ></asp:Label>
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" >
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="btnDelInv"  ImageUrl="~/Modules/HRD/Images/Delete.jpg"  CommandArgument='<%#Eval("InvoiceId")%>' OnClientClick="return confirm('Are you sure delate ?');" OnClick="btnDelInv_Click"/>    
                                            <asp:HiddenField ID="hfInvoicID" runat="server" Value='<%#Eval("InvoiceID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                    </Columns> 
                                    <HeaderStyle CssClass="header" />  
                                    </asp:GridView>           
                                    
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Invoice Date :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtInvDate" runat="server" ></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr runat="server" visible="false">
                                    <td>
                                        Approve Amount :
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtApproveAmount" runat="server" Width="70px" MaxLength="10" Text="0"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        Remark :
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtInvComm" runat="server" TextMode="MultiLine" Width="100%" Height="55px" ></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <%--Summary--%>
                        <td style="width:557px; vertical-align:top;" >
                            <table border="1" cellpadding="0" cellspacing="0" width="100%" >
                                <colgroup>
                                    <col width="70px;" />
                                    <col width="70px;" />
                                    <col width="80px;" />
                                    <col width="80px;" />
                                    <col width="257px;"/>
                                    <tr class="row" style="text-align:center; font-weight:bold;">
                                        <td>
                                            <b>Totals :</b>
                                        </td>
                                        <td >
                                            &nbsp;<asp:Label ID="lblQtyOrder" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;<asp:Label ID="QtyRcv" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;<asp:Label ID="QtyInv" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr class="row" style="text-align:center; font-weight:bold;">
                                        <td colspan="2">
                                            <b> Account Code : </b>
                                        </td>
                                        <td colspan="3">
                                            <asp:Label ID="lblAccountCode" runat="server" Text="" style="float:left;"></asp:Label> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5" class="auto-style1">
                                            <table border="1" cellpadding="0" cellspacing="0" width="100%" style="border-collapse :collapse " >
                                                <colgroup>
                                                    <col width="20%" />
                                                    <col width="20%" />
                                                    <col width="20%" />
                                                    <col width="20%" style="text-align:right;"/>
                                                    <col style="text-align:right;" />
                                                    <tr class="row">
                                                        <td colspan="2" style="text-align:left;">
                                                            &nbsp;</td>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td style="text-align: center">
                                                            <b>Invoice (LC)</b></td>
                                                        <td style="text-align: center">
                                                            <b>Invoice (USD)</b></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="text-align:left;">
                                                            
                                                        </td>
                                                        <td>
                                                            <b>Item Total :</b>
                                                        </td>
                                                        <td style="text-align:right;">
                                                            &nbsp;<asp:Label ID="lblTotLC" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="text-align:right;">
                                                            &nbsp;<asp:Label ID="lblTotUSD" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td>
                                                            
                                                        </td>
                                                        <td>
                                                            
                                                        </td>
                                                        <td>
                                                            <b> Discount <asp:Label ID="lblDiscountPercentage" runat="server"></asp:Label> (%) :</b>
                                                        </td>
                                                        <td style="text-align:right;">
                                                            <asp:Label ID="lblTotalDiscount" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="text-align:right;">
                                                            <asp:Label ID="lblTotalDiscountUSD" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>Local Curr :</b>
                                                        </td>
                                                        <td style="text-align:left;">
                                                            &nbsp;<asp:Label ID="lblLocalcurr" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <b>Shipping/Adj :</b> 
                                                        </td>
                                                        <td style="text-align:right;">
                                                            <asp:TextBox ID="txtACTSshippingFor" runat="server" Width="80px" OnTextChanged="txtACTSshippingFor_OnTextChanged" AutoPostBack="true" MaxLength="10" style="text-align:right;" onkeypress='fncInputNumericValuesOnly(event)'></asp:TextBox>
                                                        </td>
                                                        <td style="text-align:right;">
                                                            <asp:Label ID="lblACTshippingUSD" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                           
                                                        </td>
                                                        <td style="text-align:left;">
                                                           
                                                        </td>
                                                        <td>
                                                            <b>GST/Tax Total :</b> 
                                                        </td>
                                                        <td style="text-align:right;">
                                                            <asp:Label ID="lblTotalGSTLC" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="text-align:right;">
                                                            <asp:Label ID="lblTotalGSTUSD" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>Exch Rate :</b>
                                                        </td>
                                                        <td style="text-align:left;">
                                                            &nbsp;<asp:Label ID="lblExchRate" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <b>Credit Amount :</b> 
                                                        </td>
                                                        <td style="text-align:right;">
                                                            &nbsp;<asp:TextBox ID="txtCreditFor" CssClass="text-field" runat="server" Width="80px" OnTextChanged="txtCreditFor_OnTextChanged" AutoPostBack="true" MaxLength="10" style="text-align:right;" onkeypress='fncInputNumericValuesOnly(event)' min="0" ToolTip="Please enter credit amount with Minus (-). i.e. -1234.00." ></asp:TextBox>
                                                        </td>
                                                        <td style="text-align:right;">
                                                            &nbsp;<asp:Label ID="lblCreditUSD" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                   
                                                    <tr>
                                                        <td>
                                                            <b>Rate Date :</b>
                                                        </td>
                                                        <td>
                                                            &nbsp;<asp:Label ID="lblRateDate" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <b> Total :</b>
                                                        </td>
                                                        <td style="text-align:right;">
                                                            <asp:Label ID="lblLCActGrand" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="text-align:right;">
                                                            <asp:Label ID="lblUSDActGrand" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="color:red;font-weight:bold;" >
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td colspan="2">
                                                           <b> Approved(USD) :</b>
                                                        </td>
                                                        <td style="text-align:right;">
                                                            &nbsp;<asp:Label ID="lblUSDESTGrand" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr style="color:red; font-weight:bold;">
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td colspan="2">
                                                            <b> Var $ (USD) :</b>
                                                        </td>
                                                       <td style="text-align:right;">
                                                            &nbsp;<asp:Label ID="lblVarianceAmount" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </colgroup>
                                            </table>
                                        </td>
                                    </tr>
                                </colgroup>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align:left; ">
                            <div style="padding:7px; color:#00478F; font-size:13px; background-color:#99D6FF">Supplier Ratings & Comments</div>
                            <asp:HiddenField runat="server" ID="hfdRating" />
                            <div>
                            <table cellpadding="5" cellspacing="0" border="1" width="500px" style="border-collapse:collapse; text-align:center; color:#00478F " bordercolor="#0099CC">
                            <tr id="ratingctl">
                            <td style="width:20%; background-color:#FF5C33;cursor:pointer;" onclick="SetRating(-2,this)"><img src="../../HRD/Images/check_white.png" style="float:left;visibility:hidden;" class="check" />Poor</td>
                            <td style="width:20%; background-color:#FFB84D;cursor:pointer;" onclick="SetRating(-1,this)"><img src="../../HRD/Images/check_white.png" style="float:left;visibility:hidden;" class="check" />Below Avg.</td>
                            <td style="width:20%; background-color:#E6E6E6;cursor:pointer;" onclick="SetRating(0,this)"><img src="../../HRD/Images/check_white.png" style="float:left;visibility:hidden;" class="check" />Average</td>
                            <td style="width:20%; background-color:#99E699;cursor:pointer;" onclick="SetRating(1,this)"><img src="../../HRD/Images/check_white.png" style="float:left;visibility:hidden;" class="check" />Good</td>
                            <td style="width:20%; background-color:#19A347;cursor:pointer;" onclick="SetRating(2,this)"><img src="../../HRD/Images/check_white.png" style="float:left;visibility:hidden;" class="check" />Outstanding</td>
                            </tr>
                            </table>
                            </div>                    
                            <div>
                             <asp:TextBox ID="txtSupplierComments" runat="server" TextMode="MultiLine" Width="100%" Height="55px" ></asp:TextBox>
                            </div>
                            <div style="padding:7px; color:#00478F; font-size:13px; background-color:#99D6FF">Ship Comments </div>
                            <div>
                                <asp:TextBox ID="txtShipComments" runat="server" TextMode="MultiLine" Width="100%" Height="55px" ReadOnly="true" ></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align:right;">
                            <asp:Label ID="lblmsg" runat="server" CssClass="error" ForeColor="Red"></asp:Label>
                            <asp:Button ID="imgSave" runat="server" Text="Save" Visible="false" OnClick="imgSave_OnClick" OnClientClick="return ConfirmAndBackOrder()" CssClass="btn" />
                            <%----%>
                            <asp:Button ID="imgCancel" runat="server" Text="Close" OnClientClick="window.close();return false;" CssClass="btn"  />
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton2" TargetControlID="txtInvDate"></asp:CalendarExtender>
                        </td>
                    </tr>
                </colgroup>
            </table> 
        </div>     

         <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; text-align :right " runat="server" id="ModalPopupExtender1" visible="false" >
    <center>
        <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position :relative;width:450px; padding :3px; text-align :center;background : white; z-index:150;top:100px;">
        <div >
        <asp:ImageButton runat="server" ID="btnClose" style="float:right" ImageUrl="~/Modules/HRD/Images/close.gif" ToolTip="Close this Window." OnClientClick="document." onclick="btnClose_Click"/> 
</div> 
<div style="float:left;padding: 0px 5px 0px 5px;" class="bluetext" >Supplier Name :</div>
<div style="float:left" ><asp:TextBox runat="server" ID="txtVendor"  Text="A" ></asp:TextBox> </div>
<div style="float:left;padding-left :5px;" >
<asp:Button runat="server" id="btnFind" OnClick="btnFind_Click" CssClass="btn" Text="Search" AlternateText="Find"/></div>
<table>
<tr>
<td>
<div style="width :444px; border :solid 1px #4371a5;" >
<table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
<colgroup>
        <col style="width:40px;" />
        <col />
        <col style="width:17px;" />
</colgroup>
<tr class= "headerstylegrid">
    <td>Select</td>
    <td>Select New Vendor</td>
    <td>&nbsp;</td>
</tr>
</table>
</div>
<div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 444px;HEIGHT: 300px ; text-align:center; float :left;border:solid 1px #4371a5">
<table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;" id="tbl_Vendors">
    <colgroup>
        <col style="width:40px;" />
        <col />
    </colgroup>
    <asp:Repeater ID="rptVendors" runat="server">
        <ItemTemplate>
                <tr id='tr<%#Eval("SupplierId")%>' class='row'>
                <td><asp:ImageButton CommandArgument='<%#Eval("SupplierId")%>' runat="server" ImageUrl="~/Modules/HRD/Images/approval1.jpg" ID="btnNewVendor" OnClick="btnNewVendor_Click" />  
                </td> 
                <td style="text-align :left">
                <em style=" color : red" ><%# Eval("TravId")%>,</em><asp:Label ID="lblDesc" runat="server" Text='<%# Eval("SupplierName") %>' ></asp:Label>, <em style=" color : blue" ><%# Eval("SupplierPort")%></em>
                    <br />
                    <asp:Label runat="server" ForeColor="Green" Font-Italic="true" ID="txtEmail" BorderColor="Orange" Text ='<%# Eval("SupplierEmail")%>' ></asp:Label>
                </td>
                <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
            </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <tr id='tr<%#Eval("SupplierId")%>' class='alternaterow'>
                <td><asp:ImageButton CommandArgument='<%#Eval("SupplierId")%>' runat="server" ImageUrl="~/Modules/HRD/Images/approval1.jpg" ID="btnNewVendor" OnClick="btnNewVendor_Click" />  
                </td> 
                <td style="text-align :left">
                <em style=" color : red" ><%# Eval("TravId")%>,</em><asp:Label ID="lblDesc" runat="server" Text='<%# Eval("SupplierName") %>'></asp:Label>, <em style=" color : blue" ><%# Eval("SupplierPort")%></em>
                <br />
                <asp:Label runat="server" ForeColor="Green" Font-Italic="true" ID="txtEmail" BorderColor="Orange" Text ='<%# Eval("SupplierEmail")%>' ></asp:Label>
                </td>
                 <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
            </tr>
        </AlternatingItemTemplate>
    </asp:Repeater>
</table>
</div>
</td>

</tr>
</table>
</div>
    </center>
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
         <script type="text/javascript">
            function SetRating(rating, control) {
                $(".check").css('visibility', 'hidden');
                $(control).find("img").css('visibility', '');
                $("#hfdRating").val(rating);
            }

            if ($("#hfdRating").val() != "") {
                var i = parseInt($("#hfdRating").val()) + 3;
                //$("#ratingctl td:nth-child(" + i + ")").css("color", "red");
                //alert($("#ratingctl td:nth-child(" + i + ")"));
                $("#ratingctl td:nth-child(" + i + ")").click();
            }
        </script>
    </div>
</form> 
    
</body>
<script type="text/javascript">
    window.opener.blur();
    window.focus();
</script>
</html>
