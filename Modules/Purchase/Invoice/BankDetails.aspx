<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BankDetails.aspx.cs" Inherits="Modules_Purchase_Invoice_BankDetails" EnableEventValidation ="false" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
     <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="JS/jquery.min.js"></script>
     <script src="JS/AutoComplete/knockout-2.2.1.js" type="text/javascript"></script>
     <!-- Auto Complete -->
     <link rel="stylesheet" href="JS/AutoComplete/jquery-ui.css?11" />
     <script src="JS/AutoComplete/jquery-ui.js?11" type="text/javascript"></script>
    <style type="text/css">
    body
    {
        font-family:Verdana;
        font-size:12px;
        margin:0px;
    }
    .pending
    {
        background-color:#f7f841;
        padding:3px;
    }

    </style>
    <script type="text/javascript">
        function PrintVoucherO(PaymentId) {
           // alert(pvno);
            winref = window.open('PaymentVoucher.aspx?PaymentId=' + PaymentId +'&PaymentMode=O', '');
            return false;
        }
        function RegisterAutoComplete() {
            $(function () {
                //------------
                function log(message) {
                    $("<div>").text(message).prependTo("#log");
                    $("#log").scrollTop(0);
                }
                
                //---------------
                $("#txtVendorName").autocomplete(
                    {
                        
                     source: function (request, response) {
                         $.ajax({
                             url: getBaseURL() + "/Modules/Purchase/Invoice/getautocompletedata.ashx",
                             dataType: "json",
                             headers: { "cache-control": "no-cache" },
                             type: "POST",
                             data: { Key: $("#txtVendorName").val(), Type: "VENALL" },
                             cache: false,
                             success: function (data) {
                                 response($.map(data.geonames, function (item) { return { label: item.SupplierNameCode, value: item.SupplierName, id: item.SupplierId,active:item.Active} }
                                    ));
                             }
                         });
                     },
                     minLength: 2,
                     select: function (event, ui) {
                         //log(ui.item ? "Selected: " + ui.item.label : "Nothing selected, input was " + this.value);
                         $("#hfdSupplierId").val(ui.item.id);
                     },
                     open: function () {
                         $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
                     },
                     close: function () {
                         $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
                     }
                 });
                 //---------------
                 $("#txtF_Vendor").autocomplete(
                 {
                     source: function (request, response) {
                         $.ajax({
                             url: getBaseURL() + "/Modules/Purchase/Invoice/getautocompletedata.ashx",
                             dataType: "json",
                             headers: { "cache-control": "no-cache" },
                             type: "POST",
                             data: { Key: $("#txtF_Vendor").val(), Type: "VENALL" },
                             cache: false,
                             success: function (data) {
                                 response($.map(data.geonames, function (item) { return { label: item.SupplierNameCode, value: item.SupplierName, id: item.SupplierId,active:item.Active} }
                                    ));
                             }
                         });
                     },
                     minLength: 2,
                     select: function (event, ui) {
                         //log(ui.item ? "Selected: " + ui.item.label : "Nothing selected, input was " + this.value);
                        // $("#hfdSupplierId").val(ui.item.id);
                     },
                     open: function () {
                         $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
                     },
                     close: function () {
                         $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
                     }
                 });
                 
            });
        }
        function getBaseURL() {

            var url = window.location.href.split('/');
            var baseUrl = url[0] + '//' + url[2] + '/' + url[3];
          /*  alert(baseUrl);*/
            return baseUrl;

        }

        function UpdateBankChgs(ctl)
        {
            $.ajax({
                method: "POST",
                url: "paymentvoucherlist.aspx",
                data: { UPPaymentId: $(ctl).attr('paymentid'), UPAmt: $(ctl).val(), UPDateMode: 'C', RecordType: $(ctl).attr('RecordType') },
                success: function (res) {
                    if(res=="S")
                        $(ctl).css('backgroundColor', 'lightgreen');
                    else
                        $(ctl).css('backgroundColor', 'red');
                }
            })
        }
        function UpdateBankAmt(ctl)
        {
            $.ajax({
                method: "POST",
                url: "paymentvoucherlist.aspx",
                data: { UPPaymentId: $(ctl).attr('paymentid'), UPAmt: $(ctl).val(), UPDateMode: 'A', RecordType: $(ctl).attr('RecordType') },
                success: function (res) {
                    if (res == "S")
                        $(ctl).css('backgroundColor', 'lightgreen');
                    else
                        $(ctl).css('backgroundColor', 'red');
                }
            })
        }
        function UpdateAmount(ctl)
        {
            
        }


        function RegisterAutoComplete1() {
            $(function () {
                //------------
                function log(message) {
                    $("<div>").text(message).prependTo("#log");
                    $("#log").scrollTop(0);
                }
                //---------------
                $("#txtSupplier").autocomplete(
                {
                    source: function (request, response) {
                        $.ajax({
                            url: getBaseURL() + "/Modules/Purchase/Invoice/getautocompletedata.ashx",
                            dataType: "json",
                            headers: { "cache-control": "no-cache" },
                            type: "POST",
                            data: { Key: $("#txtSupplier").val(), Type: "VENALL" },
                            cache: false,
                            success: function (data) {
                                response($.map(data.geonames, function (item) { return { label: item.SupplierNameCode, value: item.SupplierName, id: item.SupplierId , active:item.Active} }
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
    </script>
    <script type="text/javascript">
        var ctl;
        function OpenUpdate(ctl1) {

            $("#dvUpdate").show(100);
            $("#hfRecordType_popup").val($(ctl1).attr("RecordType"));
            $("#hfpaymentid_popup").val($(ctl1).attr("paymentid"));

            $("#bankCharges_popup").val($(ctl1).attr("BankCharges"));
            $("#bankAmount_popup").val($(ctl1).attr("BankAmount"));

            $("#hfSelctedBankChargesid").val($(ctl1).parent().next().children().attr("id"));
            $("#hfSelctedBankAmountid").val($(ctl1).parent().next().next().children().attr("id"));
            ctl = ctl1;
        }
        $(document).ready(function () {
            //$(".btnCheck").click(function () {
            //    $("#dvUpdate").show(100);
            //    $("#hfRecordType_popup").val($(this).attr("RecordType"));
            //    $("#hfpaymentid_popup").val( $(this).attr("paymentid"));
                
            //    $("#bankCharges_popup").val($(this).attr("BankCharges"));
            //    $("#bankAmount_popup").val($(this).attr("BankAmount"));

            //    $("#hfSelctedBankChargesid").val($(this).parent().next().children().attr("id"));
            //    $("#hfSelctedBankAmountid").val($(this).parent().next().next().children().attr("id"));
                
            //    ctl = this;
            //});


            
            //-------------
            $(".closepopup").click(function () {
                $("#dvUpdate").hide(200);
                 $("#hfRecordType_popup").val("");
                 $("#hfpaymentid_popup").val("");
                 $("#bankCharges_popup").val("");
                 $("#bankAmount_popup").val("");
                 $("#BankConfirmedOn").val("");
            });
            //----------------------------------------------------------------------------------------------------
            $("#btnUpdateBankAccount").click(function () {
                var recordType = $("#hfRecordType_popup").val();
                var paymentid = $("#hfpaymentid_popup").val();
                var bankCharges = $("#bankCharges_popup").val();
                var bankAmount = $("#bankAmount_popup").val();
                var bankConfirmedOn = $("#BankConfirmedOn").val();
                
                $.ajax({
                    type: "POST",
                    url: "PaymentVoucherList.aspx/UpdateAmount",
                    data: '{RecordType: "' + recordType + '",Paymentid:"' + paymentid + '",BankCharges:"' + bankCharges + '",BankAmount:"' + bankAmount + '",BankConfirmedOn:"' + bankConfirmedOn + '"}',
                    //data: {Empid: empid ,Amount:Amount },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response.d == 'Y') {
                            var idBA = $("#hfSelctedBankAmountid").val();
                            var idBC = $("#hfSelctedBankChargesid").val();

                            $("#" + idBA).html(bankAmount);
                            $("#" + idBC).html(bankCharges);
                            
                            $("#" + idBA).removeClass('pending');
                            $("#" + idBC).removeClass('pending');

                            $("#dvUpdate").hide(200);
                            $(ctl).hide(200);
                            
                        }
                        else
                            {
                            alert("Error while updating record.");
                        }
                    },
                    failure: function (response) {
                        alert("Error while updating record.");
                    }
                });

                
            });
        })
    </script>
  <script type="text/javascript" src="JS/KPIScript.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ></asp:ToolkitScriptManager>
    <div id="log" style="display:none"></div>
    <div style="border:solid 1px #008AE6;">
    <%--<asp:UpdatePanel runat="server" id="up1">
    <ContentTemplate>--%>
        <table cellpadding="0" cellspacing="0" width="100%">
         <tr>
         <td style=" padding:6px; text-align:center;" class="text headerband"> 
             <strong>Invoice Management - Bank Details </strong></td>
         </tr>
        
            <tr>
                <td style="background-color:#E6F3FC">
                    <table cellpadding="3" cellspacing="0" width="100%">
                        <tr>
                            <td style="text-align:left">&nbsp;Period :</td>
                            <td style="text-align:left">
                                <asp:TextBox ID="txtF_D1" runat="server" style="border:solid 1px #008AE6;" Width="100px"></asp:TextBox>
                                <asp:CalendarExtender ID="c1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtF_D1">
                                </asp:CalendarExtender>
                                <asp:TextBox ID="txtF_D2" runat="server" style="border:solid 1px #008AE6;" Width="100px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtF_D2">
                                </asp:CalendarExtender>
                            </td>
                            <td style="text-align:left"></td>
                            <td style="text-align:left">
                                

                                 <asp:DropDownList ID="ddlStatus1" runat="server" Width="175px">
                                     <asp:ListItem Text=" < Bank Status > " Value=""></asp:ListItem>
                                     <asp:ListItem Text="Bank Update Pending" Value="P" Selected="True"></asp:ListItem>
                                     <asp:ListItem Text="Bank Update Completed" Value="C"></asp:ListItem>
                                </asp:DropDownList>
                                
                            </td>
                            <td style="text-align:left">Vendor :</td>
                            <td style="text-align:left">
                                <asp:TextBox ID="txtF_Vendor" runat="server" style="border:solid 1px #008AE6;" Width="250px"></asp:TextBox>
                            </td>
                            <td rowspan="2" style="padding:0px">
                                <asp:Button ID="btn_Search" runat="server" OnClick="btn_Search_Click" OnClientClick="this.value='Loading..';" style="  border:none; padding:4px;" Text="Search" Width="100px" CssClass="btn" />
                             
                                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" ToolTip="Clear" CssClass="btn" />
                                <asp:HiddenField ID="hfdSupplier" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:left">Owner : </td>
                            <td style="text-align:left">
                                <asp:DropDownList ID="ddlF_Owner" runat="server" Width="255px">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align:left">&nbsp;Inv # : </td>
                            <td style="text-align:left">
                                <asp:TextBox ID="txtF_InvNo" runat="server" style="border:solid 1px #008AE6;" Width="250px"></asp:TextBox>
                            </td>
                            <td>PV # : </td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtF_PVNo" runat="server" style="border:solid 1px #008AE6;" Width="250px"></asp:TextBox>
                            </td>
                            <td></td>
                        </tr>
                        <tr> <td style="text-align:left">Paid In : </td>
                            <td style="text-align:left">
                                <asp:DropDownList ID="ddlPaidIn" runat="server" Width="100px">
                                </asp:DropDownList>
                            </td>
                            <td style="text-align:left"> </td>
                            <td style="text-align:left">
                               
                            </td>
                            <td> </td>
                            <td style="text-align: left">
                                
                            </td>
                            <td></td> </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden;HEIGHT: 30px ; text-align:center; border-bottom:none;">
                        <table border="1" bordercolor="white" cellpadding="4" cellspacing="0" rules="all" style="width:100%; height:30px;  border-collapse:collapse;">
                            <colgroup>
                                <col style="width:11%;" />
                                <col style="width:11%;" />
                                <col />
                                <col style="width:5%;" />
                                <col style="width:8%;" />
                                <col style="width:8%;" />
                                <col style="width:8%;" />
                                <col style="width:7%;" />
                                <col style="width:4%;" />
                                <col style="width:8%;" />
                                <%--<col style="width:100px;" />--%>
                                <col style="width:4%;" />
                                <%--<col style="width:25px;"/>--%>
                                <tr align="left" class= "headerstylegrid">
                                    <td>Vessel </td>
                                    <td>PV #</td>
                                    <td>Vendor</td>                                   
                                    <td>Inv Curr.</td>
                                    <td>PV Amount (LC)</td>
                                    <td>Amount (USD) </td>
                                    <td>Paid By </td>
                                    <td>Paid On </td>
                                    <td>Paid In</td>
                                    <td>Bank Amount (LC)</td>
                                    <%--<td>Bank Chgs.</td>--%>
                                    <td>&nbsp;</td>
                                   <%-- <td>&nbsp;</td>--%>
                                </tr>
                            </colgroup>
                        </table>
                    </div>
                    <div id="dv_grd" class="ScrollAutoReset" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden;HEIGHT: 460px ; text-align:center; border-bottom:none;">
                        <table border="1" bordercolor="#F0F5F5" cellpadding="4" cellspacing="0" style=" text-align: center; border-collapse:collapse; width:100%;">
                            <colgroup>
                               <col style="width:11%;" />
                                <col style="width:11%;" />
                                <col />
                                <col style="width:5%;" />
                                <col style="width:8%;" />
                                <col style="width:8%;" />
                                <col style="width:8%;" />
                                <col style="width:7%;" />
                                <col style="width:4%;" />
                                <col style="width:8%;" />
                                <%--<col style="width:100px;" />--%>
                                <col style="width:4%;" />
                               <%-- <col style="width:25px;"/>--%>
                            </colgroup>
                            <asp:Repeater ID="RptMyInvoices" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="left"><%#Eval("VesselName")%></td>
                                        <td align="left">
                                            <a id="A2" runat="server" onclick='<%#"PrintVoucherO("+Eval("PaymentId") + ");"%>' style="color:Blue; text-decoration:underline;cursor:pointer;" visible='<%#(Eval("PTYPE").ToString()=="O")%>'> <%#Eval("PVNo")%> <asp:Label Id="lblAdvPayment" runat="server" ForeColor="Red" Text="(AD)" Visible='<%#(Convert.ToBoolean(Eval("IsAdvPayment")) == true)%>'></asp:Label> </a>
                                           
                                            
                                        </td>
                                        <td align="left"><%#Eval("SupplierName")%>
                                            <span runat="server" id="c1" visible='<%#(Eval("Status").ToString()=="C")%>'  style="color:red"> - Cancelled </span>
                                            <b style="color:Red"><i style='display: <%#Eval("TravId").ToString() == "" ? "none" : "inline" %>  '>( <%#Eval("TravId")%>) </i></b>
                                        </td>
                                        <td align="left"><%#Eval("Currency")%></td>
                                        <td align="left"><%#Eval("Amount")%></td>
                                        <td align="left"><asp:Label runat="server"  ID="txtBamount" Text='<%#Eval("PVAmountUSD")%>' Width="95%" style="text-align:right" MaxLength="12" onkeyup="UpdateBankAmt(this)" BankCharges='<%#Eval("BankCharges")%>' BankAmount='<%#Eval("PVAmountUSD")%>' RecordType='<%#Eval("PTYPE")%>'  paymentid='<%#Eval("PaymentId")%>' CssClass='<%#((Common.ToDateString(Eval("BankConfirmedOn"))=="")?"":"")%>'></asp:Label></td>
                                        <td align="left"><%#Eval("PaidBy")%></td>
                                        <td align="left"><%#Common.ToDateString(Eval("paidon"))%></td>
                                        <td align="left"><%#Eval("PaymentCurr")%></td>
                                         <%-- <td align="left"><%#((Eval("PaymentType").ToString()=="U")?"USD":((Eval("PaymentType").ToString()=="S")?"SGD":"INR"))%>
                                          <asp:HiddenField ID="hfPaymentId" runat="server" Value='<%#Eval("PaymentId")%>' />
                                        </td>--%>
                                            
                                      <%--  <td><asp:Label runat="server" ID="txtBCharges" Text='<%#Eval("BankCharges")%>' Width="95%" style="text-align:right" MaxLength="12" onkeyup="UpdateBankChgs(this)" BankCharges='<%#Eval("BankCharges")%>' BankAmount='<%#Eval("BankAmount")%>' RecordType='<%#Eval("PTYPE")%>' paymentid='<%#Eval("PaymentId")%>' CssClass='<%#((Common.ToDateString(Eval("BankConfirmedOn"))=="")?"pending":"")%>'></asp:Label></td>--%>
                                       
                                        <%--onclick="UpdateAmount(this);"--%>
                                        <td align="left"> <%#Eval("BankAmountLC")%> </td>
                                        <td align="center">
                                            
                                            <%--<img src="../Images/check_white.png" runat="server" onclick="OpenUpdate(this)"  BankCharges='<%#Eval("BankCharges")%>' BankAmount='<%#Eval("BankAmount")%>' visible='<%#(Common.ToDateString(Eval("BankConfirmedOn"))=="") && (Eval("Status").ToString()=="A")%>' RecordType='<%#Eval("PTYPE")%>' paymentid='<%#Eval("PaymentId")%>'/> --%>

                                            <asp:ImageButton  ID="imgUpdateAmountOpenPop" runat="server"  ToolTip="Update Bank Amount" OnClick="imgUpdateAmountOpenPop_OnClick" visible='<%#(Common.ToDateString(Eval("BankConfirmedOn"))=="") && (Eval("Status").ToString()=="A" && (Eval("PTYPE").ToString()=="O"))%>' ImageUrl="~/Modules/HRD/Images/Bankupdate.png"/>

                                             <asp:HiddenField ID="hfBankCharges" runat="server" Value='<%#Eval("BankCharges")%>' />
                                             <asp:HiddenField ID="hfBankAmount" runat="server" Value='<%#Eval("PVAmountUSD")%>' />
                                             <asp:HiddenField ID="hfRecordType" runat="server" Value='<%#Eval("PTYPE")%>' />
                                            <asp:HiddenField ID="hdnPaymentId" runat="server" Value='<%#Eval("PaymentId")%>' />

                                        </td>
                                       
                                       
                                       <%-- <td>&nbsp;</td>--%>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr style="background-color:#E6F3FC">
                                        <td align="left"><%#Eval("VesselName")%></td>
                                        <td align="left">
                                            <a id="A2" runat="server" onclick='<%#"PrintVoucherO("+Eval("PaymentId") + ");"%>' style="color:Blue; text-decoration:underline;cursor:pointer;" visible='<%#(Eval("PTYPE").ToString()=="O")%>'><%#Eval("PVNo")%> <asp:Label Id="Label1" runat="server" ForeColor="Red" Text="(AD)" Visible='<%#(Convert.ToBoolean(Eval("IsAdvPayment")) == true)%>'></asp:Label> </a>
                                        </td>
                                        <td align="left"><%#Eval("SupplierName")%><span runat="server" id="c1" visible='<%#(Eval("Status").ToString()=="C")%>' style="color:red"> - Cancelled </span>
                                            <b style="color:Red"><i style='display: <%#Eval("TravId").ToString() == "" ? "none" : "inline" %>'>( <%#Eval("TravId")%>) </i></b>
                                        </td>
                                        <td align="left"><%#Eval("Currency")%></td>
                                        <td align="left"><%#Eval("Amount")%></td>
                                        <td align="left"><asp:Label runat="server"  ID="txtBamount" Text='<%#Eval("PVAmountUSD")%>' Width="95%" style="text-align:right" MaxLength="12" onkeyup="UpdateBankAmt(this)" BankCharges='<%#Eval("BankCharges")%>' BankAmount='<%#Eval("PVAmountUSD")%>' RecordType='<%#Eval("PTYPE")%>'  paymentid='<%#Eval("PaymentId")%>' CssClass='<%#((Common.ToDateString(Eval("BankConfirmedOn"))=="")?"":"")%>'></asp:Label></td>
                                        <td align="left"><%#Eval("PaidBy")%></td>
                                        <td align="left"><%#Common.ToDateString(Eval("paidon"))%></td>
                                        <td align="left"><%#Eval("PaymentCurr")%>
                                              <asp:HiddenField ID="hfPaymentId" runat="server" Value='<%#Eval("PaymentId")%>' />
                                        </td>
                                            
                                       <%-- <td><asp:Label runat="server" ID="txtBCharges" Text='<%#Eval("BankCharges")%>' Width="95%" style="text-align:right" MaxLength="12" onkeyup="UpdateBankChgs(this)" BankCharges='<%#Eval("BankCharges")%>' BankAmount='<%#Eval("BankAmount")%>' RecordType='<%#Eval("PTYPE")%>' paymentid='<%#Eval("PaymentId")%>' CssClass='<%#((Common.ToDateString(Eval("BankConfirmedOn"))=="")?"pending":"")%>'></asp:Label></td>--%>
                                        <td align="left"> <%#Eval("BankAmountLC")%> </td>
                                        <td align="center">
                                            <%--<img src="../Images/check_white.png" runat="server" onclick="OpenUpdate(this)"  BankCharges='<%#Eval("BankCharges")%>' BankAmount='<%#Eval("BankAmount")%>' visible='<%#(Common.ToDateString(Eval("BankConfirmedOn"))=="") && (Eval("Status").ToString()=="A")%>' RecordType='<%#Eval("PTYPE")%>' paymentid='<%#Eval("PaymentId")%>'/> --%>
                                            <asp:ImageButton  ID="imgUpdateAmountOpenPop" runat="server"  ToolTip="Update Bank Amount" OnClick="imgUpdateAmountOpenPop_OnClick" visible='<%#(Common.ToDateString(Eval("BankConfirmedOn"))=="") && (Eval("Status").ToString()=="A" && (Eval("PTYPE").ToString()=="O"))%>' ImageUrl="~/Modules/HRD/Images/Bankupdate.png"/>

                                             <asp:HiddenField ID="hfBankCharges" runat="server" Value='<%#Eval("BankCharges")%>' />
                                             <asp:HiddenField ID="hfBankAmount" runat="server" Value='<%#Eval("PVAmountUSD")%>' />
                                             <asp:HiddenField ID="hfRecordType" runat="server" Value='<%#Eval("PTYPE")%>' />
                                             <asp:HiddenField ID="hdnPaymentId" runat="server" Value='<%#Eval("PaymentId")%>' />
                                        </td>
                                       <%-- <td>&nbsp;</td>--%>
                                    </tr>
                                </AlternatingItemTemplate>
                            </asp:Repeater>
                               
                        </table>
                    </div>
                     <br />
            <div style="text-align:left;">
                <%--<asp:Repeater ID="rptComponentSparesPaging" runat="server" OnItemCommand="rptComponentSparesPaging_ItemCommand">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkPage" runat="server" CommandArgument="<%# Container.DataItem %>" CommandName="Page"  Style="padding: 5px 3px 5px 3px; margin: 2px; background: lightgray; border: solid 1px #666; color: black; font-size:11px;"><%# Container.DataItem %>  
            </asp:LinkButton>
                    </ItemTemplate>
                </asp:Repeater>--%>
<asp:LinkButton ID="lbPrevious" runat="server" Enabled="false" OnClick="lnkbuttonPrev_Click"  Style=" padding: 5px 3px 5px 3px; margin: 2px; background: lightgray; border: solid 1px #666; color: black; font-size:11px;">Previous page</asp:LinkButton> &nbsp;
<asp:LinkButton ID="lbNext" runat="server" OnClick="lnkbuttonNext_Click" Style=" padding: 5px 3px 5px 3px; margin: 2px; background: lightgray; border: solid 1px #666; color: black; font-size:11px;">Next page</asp:LinkButton> &nbsp;
             <b> Payment Count : </b>   <asp:Label ID="lblPaymentCount" runat="server" Text="0" Style=" padding: 5px 3px 5px 3px; margin: 2px; background: lightgray; border: solid 1px #666; color: red; font-size:11px;"></asp:Label> 
                 <asp:Button ID="btnClose" runat="server" OnClientClick="window.close()" style=" border:none; padding:4px;float:right;margin-right:5px;" Text="Close" Width="150px" CssClass="btn" />
            </div>
                    
                </td>
            </tr>
       </table>
   
 <div style="width:100%;position:fixed; height:100%; text-align:center;left:0px; top:0px; background-color:rgba(0, 0, 0, 0.38)" id="dvUpdate" runat="server" visible="false">
            <div style="width:900px; margin:0 auto; border:solid 2px #008AE6;padding:0px; background-color:white;position:relative; top:0px; top:10px;">
                <div style="padding:5px; " class="text headerband">
                    <div style="width:30px;float:right;"> 
                        <asp:ImageButton ID="btnCloseUpdateAmountPop" runat="server" ImageUrl="~/Modules/HRD/Images/Close.gif" OnClick="btnCloseUpdateAmountPop_OnClick" />
                    </div>
                    <b>Update Exchange Gain/Loss and Bank Charges</b>
                </div>
                <table width="100%" cellpadding="5" border="1" bordercolor="#c2c2c2" style="border-collapse:collapse">
                    <colgroup>
                        <col style="width:100px"/>
                        <col style="width:190px"/>
                         <col style="width:100px"/>
                        <col style="width:190px"/>
                         <col style="width:100px"/>
                        <col style="width:190px"/>
                    </colgroup>
                        <tr>
                            <td style=" text-align:right;"><b>PV No. :</b></td>
                            <td style="text-align:left"><asp:Label runat="server" ID="lblPVNO1"></asp:Label></td>
                            <td style=" text-align:right;" ><b>Vendor :</b></td>
                            <td style="text-align:left"><asp:Label runat="server" ID="lblVendorName"></asp:Label> (<asp:Label runat="server" ID="lblVendorCode" Text="" ForeColor="Red"></asp:Label>) </td>
                             <td style=" text-align:right;" ><b>Owner :</b></td>
                            <td style="text-align:left"><asp:Label runat="server" ID="lblOwner"></asp:Label> (<asp:Label runat="server" ID="lblOwnerCode" Text="" ForeColor="Red"></asp:Label>) </td>
                        </tr>
                    <tr>
                        <td style=" text-align:right;"><b>Vessel :</b></td>
                         <td style="text-align:left" colspan="5"> 
                           <asp:Label ID="lblVesselName" runat="server"></asp:Label>
                           <asp:Label ID="lblVesselCode" runat="server" Visible="false"></asp:Label>
                           
                                                </td>
                    </tr>
                        
                </table>
                
                     <div style="padding:5px; background-color:#E6F3FC; color:#333; font-weight:bold;"> 
                         Invoice List
                     </div>
                        <div style="height:32px; overflow-x:hidden;overflow-y:scroll">
                            <table border="1" bordercolor="#c2c2c2" cellpadding="2" cellspacing="0" rules="all" style="width:100%; height:30px;  border-collapse:collapse;">
                               <tr style="background-color:#a2a6a9; color:White; height:25px;">
                                    <td style="width:50px">Vessel</td>
                                    <td>Invoice#</td>
                                    <td style="width:100px">Inv. Date</td>
                                    <td style="width:150px">Invoice Amount ( <asp:Label runat="server" ID="lblinvcurr"></asp:Label> ) </td>
                                    <td style="width:150px">Amount ( USD ) <asp:Label runat="server" ID="lblpaycurr" ForeColor="#e20959" Visible="false"></asp:Label> </td>
                                    <td style="width:25px">&nbsp;</td>
                                </tr>
                                 </table>
                        </div>
                        <div style="height:150px; overflow-x:hidden;overflow-y:scroll">
                            <table border="1" bordercolor="#c2c2c2" cellpadding="2" cellspacing="0" rules="all" style="width:100%; height:30px;  border-collapse:collapse;">
                            <asp:Repeater ID="rptAmount" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td style="width:50px"><%#Eval("INVVesselCode")%></td>
                                        <td> <asp:Label runat="server" ID="lblInvoiceNo" Text='<%#Eval("InvoiceNo")%>' /> 
                                            <asp:HiddenField runat="server" ID="hfdtableId" Value='<%#Eval("tableId")%>' />
                                             <asp:HiddenField runat="server" ID="hdnPaymentId" Value='<%#Eval("PaymentId")%>' />
                                        </td>
                                        <td style="width:100px"><%#Common.ToDateString(Eval("OnDate")) %></td>
                                        <td style="text-align:right;width:150px" ><%#Eval("Amount")%></td>
                                        <td style="text-align:right;width:150px" >
                                            <asp:Label ID="txtAmount" runat="server"  Width="95%" onkeyup="autosum();"   Text='<%#Eval("PVAmountUSD")%>' style="text-align:right; " ReadOnly="true" ></asp:Label></td>
                                        <td style="width:25px">&nbsp;</td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            </table>
                        </div>
                <div style="height:32px; overflow-x:hidden;overflow-y:scroll">
                              <table border="1" bordercolor="#c2c2c2" cellpadding="2" cellspacing="0" rules="all" style="width:100%; height:30px;  border-collapse:collapse;">
                                <tr style="background-color:#eeeeef; color:000; height:25px;">
                                    <td style="width:50px"><b>Total :</b></td>
                                    <td></td>
                                    <td style="width:100px"></td>
                                    <td style="text-align:right;width:150px"><asp:Label runat="server" ID="lblTotalInvAmount" Text="0.00" style="font-weight: bold;color: #e20959;font-size: 18px;" ></asp:Label></td>
                                    <td style="text-align:right;width:150px"><asp:Label runat="server" ID="lblTotalBankAmount" Text="0.00" style="font-weight: bold;color: #e20959;font-size: 18px;" ></asp:Label></td>
                                    <td style="width:25px">&nbsp;</td>
                                </tr>
                                </table>
                    </div>
                <div style="padding:5px; background-color:#E6F3FC; color:#333; display:flex;justify-content:flex-end;align-items:center;gap:150px; ">
                    
                        <asp:UpdatePanel ID="upBankTransDt" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div style="font-weight:bold;">
                                 Bank Trans. Date : <asp:TextBox ID="BankConfirmedOnPopup" runat="server" style="width:80px;background-color:#f7f841;" AutoPostBack="True" OnTextChanged="BankConfirmedOnPopup_TextChanged"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy" PopupPosition="TopRight" TargetControlID="BankConfirmedOnPopup"></asp:CalendarExtender>
                                     </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="BankConfirmedOnPopup" />
                            </Triggers>
                        </asp:UpdatePanel>
                    <div style="float:right;padding-right:5px;font-style:italic;color:red;">
                        Exchange Rate (<asp:Label ID="lblLCInvCurr" runat="server"></asp:Label>) : <asp:TextBox ID="txtXchangeRate" runat="server" Text="0.00" AutoPostBack="true" MaxLength="8" TextMode="Number" OnTextChanged="txtXchangeRate_TextChanged"></asp:TextBox> USD
                    </div>
                </div>
                <div id="dvExchangeGainLoss" runat="server"  >
                <div style="padding:5px; background-color:#E6F3FC; color:#333; font-weight:bold;"> 
                         Exchange Gain/Loss
                </div>
                 <table border="1" bordercolor="#c2c2c2" cellpadding="4" cellspacing="0" rules="all" style="width:100%; height:30px;  border-collapse:collapse;">
                                           
                               <tr style="background-color:#a2a6a9; color:White; height:25px;">
                                   
                                    <td> Bank Amount (<asp:Label runat="server" ID="lblGainLossCurr" ForeColor="#e20959"></asp:Label>)</td>
                                    <td> Bank Amount (USD) </td>
                                    <td style="width:150px">Exchange Gain/Loss Amount (USD) 
                                       <%-- <asp:Label runat="server" ID="lblpaycurr3" ForeColor="#e20959"></asp:Label>--%></td>
                                    <td> Budget Acct Code</td>
                                   <td style="width:25px">&nbsp;</td>
                                </tr>
                               <%-- <tr style="">
                                    <td>Credit Amount</td>
                                    <td>
                                         <asp:UpdatePanel runat="server" ID="up11" UpdateMode="Conditional">
                                            <ContentTemplate>
                                        <asp:TextBox runat="server" ID="txtCreditAccountCode" MaxLength="4" Width="35px" style="text-align:center;background-color:#f7f841;"></asp:TextBox>
                                        .
                                        <asp:TextBox runat="server" ID="txtCVesselNo" Text="0000" MaxLength="4" Width="35px" style="text-align:center;" Enabled="false"></asp:TextBox>
                                                  </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td style="width:200px; text-align:right;font-weight: bold;color: #e20959;font-size: 18px;">+<asp:Label runat="server" ID="lblCreditAmount" Text="0.00"></asp:Label></td>
                                    <td style="width:25px">&nbsp;</td>
                                </tr>--%>
                                  <tr style="">
                                       
                                      <td>
  <asp:UpdatePanel runat="server" ID="upBankAmount" UpdateMode="Conditional">
                                            <ContentTemplate>
                                          <asp:TextBox ID="txtBankAmount"  runat="server" style="width:80px;text-align:right;background-color:#f7f841;" AutoPostBack="True" OnTextChanged="txtBankAmount_TextChanged" Enabled="false"></asp:TextBox>
                                  
                                         </ContentTemplate>
      <Triggers>
          <asp:PostBackTrigger ControlID="txtBankAmount" />
      </Triggers>
      </asp:UpdatePanel> </td>
                             <td><asp:TextBox ID="txtGainLossAmt" runat="server" style="width:80px;text-align:right;background-color:#f7f841;" ReadOnly="true"></asp:TextBox>          </td>
                                   
                                    <td style="width:150px; text-align:right;font-weight: bold;color: #e20959;font-size: 18px;"><asp:Label runat="server" ID="lblGainLossDiffCharges" Text="0.00"></asp:Label></td>
                                         <td>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" ID="txtGainLossAccount" MaxLength="4" Width="35px" style="text-align:left;background-color:#f7f841;" AutoPostBack="True" OnTextChanged="txtGainLossAccount_TextChanged"></asp:TextBox>&nbsp;
                                                <asp:TextBox runat="server" ID="txtGainLossAccountName" Width="159px" style="text-align:left;" Enabled="false"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        </td>      
                                        <td style="width:25px">&nbsp;</td>
                                </tr>
                    </table>

                </div>
                 <div style="padding:5px; background-color:#E6F3FC; color:#333; font-weight:bold;"> 
                         Bank Charges
                     </div>
                 <%--<table width="100%" cellpadding="5" border="1" bordercolor="#c2c2c2" style="border-collapse:collapse">
                    <colgroup>
                       <col style="width:130px"/>
                        <col style="width:100px"/>
                         <col style="width:150px"/>
                        <col style="width:170px"/>
                         <col style="width:150px"/>
                        <col style="width:100px"/>
                    </colgroup>
                        <tr>
                        <td style="text-align:right"><b>Bank Trans. Date :</b></td>
                        <td style="text-align:left"> 
                           
                        </td>
                             <td style="text-align:right"><b></b></td>
                        <td style="text-align:left"> 
                           
                           (<asp:Label runat="server" ID="lblpaycurr1" ForeColor="#e20959"></asp:Label>)
                        </td>
                             <td style="text-align:right"></td>
                        <td style="text-align:left"> 
                            
                        </td>
                        </tr>
                       
                    </table>--%>
                <table border="1" bordercolor="#c2c2c2" cellpadding="4" cellspacing="0" rules="all" style="width:100%; height:30px;  border-collapse:collapse;">
                                           
                              <tr style="background-color:#a2a6a9; color:White; height:25px;">
                                    <td> Bank Charges (<asp:Label runat="server" ID="lblBankChargesCurr" ForeColor="#e20959"></asp:Label>)</td>
                                    <td> Bank Charges (USD) </td>
                                   <td> Budget Acct Code</td>
                                   <td style="width:150px"> </td>
                                   <td style="width:25px">&nbsp;</td>
                                </tr>
                               <%-- <tr style="">
                                    <td>Credit Amount</td>
                                    <td>
                                         <asp:UpdatePanel runat="server" ID="up11" UpdateMode="Conditional">
                                            <ContentTemplate>
                                        <asp:TextBox runat="server" ID="txtCreditAccountCode" MaxLength="4" Width="35px" style="text-align:center;background-color:#f7f841;"></asp:TextBox>
                                        .
                                        <asp:TextBox runat="server" ID="txtCVesselNo" Text="0000" MaxLength="4" Width="35px" style="text-align:center;" Enabled="false"></asp:TextBox>
                                                  </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td style="width:200px; text-align:right;font-weight: bold;color: #e20959;font-size: 18px;">+<asp:Label runat="server" ID="lblCreditAmount" Text="0.00"></asp:Label></td>
                                    <td style="width:25px">&nbsp;</td>
                                </tr>--%>
                                  <tr style="">
                                    
                                    <td>
                                        <asp:UpdatePanel runat="server" ID="upBankCharges" UpdateMode="Conditional">
                                            <ContentTemplate>
                                        <asp:TextBox ID="txtBankChargesPopup" runat="server" style="width:80px;text-align:right;background-color:#f7f841;" AutoPostBack="True" OnTextChanged="txtBankChargesPopup_TextChanged" Enabled="false"></asp:TextBox> 
                                                </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="txtBankChargesPopup" />
                                            </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                    <td style="width:150px; text-align:right;font-weight: bold;color: #e20959;font-size: 18px;">
                                         <asp:UpdatePanel runat="server" ID="upBankChargesUSD" UpdateMode="Conditional">
                                            <ContentTemplate>
                                        <asp:Label runat="server" ID="lblDebitAmount" Text="0.00" ></asp:Label>
 </ContentTemplate>
                                              </asp:UpdatePanel>
                                    </td>
                                      <td> <asp:UpdatePanel runat="server" ID="up12" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" ID="txtDebitAccountCode" MaxLength="4" Width="35px" style="text-align:left;background-color:#f7f841;" AutoPostBack="True" OnTextChanged="txtDebitAccountCode_TextChanged"></asp:TextBox> &nbsp;
                                                <asp:TextBox runat="server" ID="txtDebitAccountName" Width="159px" style="text-align:left;" Enabled="false"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel> 
                                    </td>
                                        <td style="width:150px"> </td>
                                       <td style="width:25px">&nbsp;</td>
                                </tr>
                    </table>
                 <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div style="float:left;color:red;padding-left:5px;">
                                                    Note: If No Bank charges enter  0(zero) and save to close the transaction.

                                                </div>
                    <div style="padding:5px 5px 5px 0px;">
                          <asp:Button ID="btnUpdateAmount" runat="server" OnClick="btnUpdateAmount_OnClick"  style="  border:none; padding:4px 4px 4px 0px;" Text="Save" Width="150px" CssClass="btn" />  <asp:HiddenField ID="hfhfRecordType_popup" runat="server" Value="" />
                          <%--  <asp:Button ID="btnOpenExport" runat="server" OnClick="btnOpenExport_OnClick"  style="  border:none; padding:4px;" Text="Open Export" Width="200px" Visible="false" CssClass="btn" />--%>

                        </div>
                <div style="background:#f7f841; padding:5px;">
                    <asp:Label runat="server" ID="lblmsg1" Font-Bold="true"></asp:Label>
                </div>
                    </ContentTemplate>
                     <Triggers>
                         <asp:PostBackTrigger ControlID="btnUpdateAmount" />
                     </Triggers>
                                        </asp:UpdatePanel>         
            </div>
            
        </div>
   </div>
    <asp:Label runat="server" ID="lblMsgMain" Font-Bold="true"></asp:Label>
    <br />

        </div>
    </form>
</body>
</html>
