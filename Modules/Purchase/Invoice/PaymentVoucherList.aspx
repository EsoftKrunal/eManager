<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentVoucherList.aspx.cs" Inherits="Invoice_PaymentVoucherList" %>
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
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ></asp:ToolkitScriptManager>
    <div id="log" style="display:none"></div>
    <div>
    <center>
    <div style="border:solid 1px #008AE6;">
    <%--<asp:UpdatePanel runat="server" id="up1">
    <ContentTemplate>--%>
        <table cellpadding="0" cellspacing="0" width="100%">
         <tr>
         <td style=" padding:6px; text-align:center;" class="text headerband"> 
             <strong>Invoice Management - Payment Voucher</strong></td>
         </tr>
         <tr>
             <td style=" background-color:#fedb0a; color:black; padding:6px; text-align:center;">
                 Paid By User : 
                 <asp:DropDownList runat="server" ID="ddlPaidUsers"></asp:DropDownList>
             </td>
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
                                 <asp:DropDownList ID="ddlStatus" runat="server" Width="100px">
                                     <asp:ListItem Text=" < PV Status > " Value=""></asp:ListItem>
                                     <asp:ListItem Text="Create-PV" Value="P" Selected="True"></asp:ListItem>
                                     <asp:ListItem Text="Approved-PV" Value="A" ></asp:ListItem>
                                     <asp:ListItem Text="Cancelled-PV" Value="C"></asp:ListItem>
                                </asp:DropDownList>

                                 <asp:DropDownList ID="ddlStatus1" runat="server" Width="150px">
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
                                <asp:Button ID="btn_Add" runat="server" OnClick="btn_Add_Click" OnClientClick="this.value='Loading..';" style="  border:none; padding:4px;" Text="Add New" Width="100px" CssClass="btn" />
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
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden;HEIGHT: 30px ; text-align:center; border-bottom:none;">
                        <table border="1" bordercolor="white" cellpadding="4" cellspacing="0" rules="all" style="width:100%; height:30px;  border-collapse:collapse;">
                            <colgroup>
                                <col style="width:60px;"/>
                                <col style="width:50px;" />
                                <col style="width:150px;" />
                                <col />
                                <col style="width:30px;"/>
                                <col style="width:150px;" />
                                
                                
                                <col style="width:50px;" />
                                <col style="width:120px;" />
                                <col style="width:120px;" />
                                <col style="width:100px;" />
                                <col style="width:50px;" />
                                <col style="width:30px;" />
                                <col style="width:100px;" />
                                <col style="width:120px;" />
                                <col style="width:25px;"/>
                                <tr align="left" class= "headerstylegrid">
                                    <td></td>
                                    <td>Owner </td>
                                    <td>PV #</td>
                                    <td>Vendor</td>
                                    <td></td>
                                    <td>Voucher #</td>
                                    
                                    <td>Curr.</td>
                                    <td>Amount </td>
                                    <td>Paid By </td>
                                    <td>Paid On </td>
                                    <td>PayIn</td>
                                    <td>&nbsp;</td>
                                    <td>Bank Chgs.</td>
                                    <td>Bank Amount</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </colgroup>
                        </table>
                    </div>
                    <div id="dv_grd" class="ScrollAutoReset" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden;HEIGHT: 460px ; text-align:center; border-bottom:none;">
                        <table border="1" bordercolor="#F0F5F5" cellpadding="4" cellspacing="0" style=" text-align: center; border-collapse:collapse; width:100%;">
                            <colgroup>
                                <col style="width:60px;"/>
                                <col style="width:50px;" />
                                <col style="width:150px;" />
                                <col />
                                <col style="width:30px;"/>
                                <col style="width:150px;" />
                                
                                
                                <col style="width:50px;" />
                                <col style="width:120px;" />
                                <col style="width:120px;" />
                                <col style="width:100px;" />
                                <col style="width:50px;" />
                                <col style="width:30px;" />
                                <col style="width:100px;" />
                                <col style="width:120px;" />
                                <col style="width:25px;"/>
                            </colgroup>
                            <asp:Repeater ID="RptMyInvoices" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnDel" runat="server" CommandArgument='<%#Eval("PaymentId")%>' VoucherType='<%#Eval("PTYPE").ToString()%>' ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="btnAskCancel_Click" ToolTip="Cancel Invoice"  Visible='<%#(Eval("Status").ToString()!="C") && (Common.ToDateString(Eval("BankConfirmedOn"))=="")%>' OnClientClick="return confirm('Are you sure to cancel this invoice?');" />
                                            <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%#Eval("PaymentId")%>' ImageUrl="~/Modules/HRD/Images/AddPencil.gif" OnClick="btnEdit_Click" ToolTip="Modify Voucher" Visible='<%#(Eval("PTYPE").ToString()=="N") && (Eval("PVNo").ToString().Trim()=="")%>' />
                                        </td>
                                        <td align="left"><%#Eval("POSOwnerId")%></td>
                                        <td align="left">
                                            <a id="A1" runat="server" onclick='<%#"PrintVoucherN(" +Eval("PaymentId").ToString() + ");"%>' style="color:Blue; text-decoration:underline;cursor:pointer;" visible='<%#(Eval("PTYPE").ToString()=="N")%>'><%#Eval("PVNo")%></a>
                                            <a id="A2" runat="server" onclick='<%#"PrintVoucherO(" +Eval("PaymentId").ToString() + ");"%>' style="color:Blue; text-decoration:underline;cursor:pointer;" visible='<%#(Eval("PTYPE").ToString()=="O")%>'><%#Eval("PVNo")%></a>
                                            <a runat="server" href='<%#"InvoicePayment.aspx?key=" + Eval("PaymentId").ToString()%>' target="_blank" visible='<%#(Eval("PTYPE").ToString()=="O") && (Eval("PVNo").ToString().Trim()=="")%>'>Create PV</a>
                                            <asp:LinkButton ID="btnEdit1" runat="server" CommandArgument='<%#Eval("PaymentId")%>' OnClick="btnEdit_Click" Text="Create PV" ToolTip="Create PV" Visible='<%#(Eval("PTYPE").ToString()=="N") && (Eval("PVNo").ToString().Trim()=="")%>' />
                                        </td>
                                        <td align="left"><%#Eval("SupplierName")%><span runat="server" id="c1" visible='<%#(Eval("Status").ToString()=="C")%>'  style="color:red"> - Cancelled </span>
                                            <b style="color:Red"><i style='display: <%#Eval("TravId").ToString() == "" ? "none" : "inline" %>  '>( <%#Eval("TravId")%>) </i></b>
                                        </td>
                                        <td align="right"><asp:ImageButton runat="server" ID="btnChangeVendor" CommandArgument='<%#Eval("PaymentId")%>' OnClick="btnChangeVendor_Click" ImageUrl="~/Modules/HRD/Images/addpencil.gif" ToolTip="Update Vendor" Visible='<%#( Eval("PTYPE").ToString()=="O" && (Eval("PVNo").ToString().Trim()!="") && (Eval("Exported").ToString().Trim()!="True") )%>' /> </td>
                                        <td align="right"><%#Eval("VoucherNo")%></td>
                                        <td align="left"><%#Eval("Currency")%></td>
                                        <td align="left"><%#Eval("Amount")%></td>
                                        <td align="left"><%#Eval("PaidBy")%></td>
                                        <td align="left"><%#Common.ToDateString(Eval("paidon"))%></td>
                                        <td align="left"><%#((Eval("PaymentType").ToString()=="U")?"USD":((Eval("PaymentType").ToString()=="S")?"SGD":"INR"))%></td>
                                        <%--onclick="UpdateAmount(this);"--%>
                                        <td>
                                            <%--<img src="../Images/check_white.png" runat="server" onclick="OpenUpdate(this)"  BankCharges='<%#Eval("BankCharges")%>' BankAmount='<%#Eval("BankAmount")%>' visible='<%#(Common.ToDateString(Eval("BankConfirmedOn"))=="") && (Eval("Status").ToString()=="A")%>' RecordType='<%#Eval("PTYPE")%>' paymentid='<%#Eval("PaymentId")%>'/> --%>

                                            <asp:ImageButton ID="imgUpdateAmountOpenPop" runat="server" ImageUrl="~/Modules/HRD/Images/check_white.png" OnClick="imgUpdateAmountOpenPop_OnClick" visible='<%#(Common.ToDateString(Eval("BankConfirmedOn"))=="") && (Eval("Status").ToString()=="A" && (Eval("PTYPE").ToString()=="O"))%>'/>

                                             <asp:HiddenField ID="hfBankCharges" runat="server" Value='<%#Eval("BankCharges")%>' />
                                             <asp:HiddenField ID="hfBankAmount" runat="server" Value='<%#Eval("BankAmount")%>' />
                                             <asp:HiddenField ID="hfRecordType" runat="server" Value='<%#Eval("PTYPE")%>' />
                                             <asp:HiddenField ID="hfPaymentId" runat="server" Value='<%#Eval("PaymentId")%>' />

                                        </td>
                                        <td><asp:Label runat="server" ID="txtBCharges" Text='<%#Eval("BankCharges")%>' Width="95%" style="text-align:right" MaxLength="12" onkeyup="UpdateBankChgs(this)" BankCharges='<%#Eval("BankCharges")%>' BankAmount='<%#Eval("BankAmount")%>' RecordType='<%#Eval("PTYPE")%>' paymentid='<%#Eval("PaymentId")%>' CssClass='<%#((Common.ToDateString(Eval("BankConfirmedOn"))=="")?"pending":"")%>'></asp:Label></td>
                                        <td><asp:Label runat="server"  ID="txtBamount" Text='<%#Eval("BankAmount")%>' Width="95%" style="text-align:right" MaxLength="12" onkeyup="UpdateBankAmt(this)" BankCharges='<%#Eval("BankCharges")%>' BankAmount='<%#Eval("BankAmount")%>' RecordType='<%#Eval("PTYPE")%>'  paymentid='<%#Eval("PaymentId")%>' CssClass='<%#((Common.ToDateString(Eval("BankConfirmedOn"))=="")?"pending":"")%>'></asp:Label></td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr style="background-color:#E6F3FC">
                                        <td>
                                            <asp:ImageButton ID="btnDel" runat="server" CommandArgument='<%#Eval("PaymentId")%>' VoucherType='<%#Eval("PTYPE").ToString()%>' ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="btnAskCancel_Click" ToolTip="Cancel Invoice"  Visible='<%#(Eval("Status").ToString()!="C") && (Common.ToDateString(Eval("BankConfirmedOn"))=="")%>' OnClientClick="return confirm('Are you sure to cancel this invoice?');" />
                                            <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%#Eval("PaymentId")%>' ImageUrl="~/Modules/HRD/Images/AddPencil.gif" OnClick="btnEdit_Click" ToolTip="Modify Voucher" Visible='<%#(Eval("PTYPE").ToString()=="N") && (Eval("PVNo").ToString().Trim()=="")%>' />
                                        </td>
                                        <td align="left"><%#Eval("POSOwnerId")%></td>
                                        <td align="left"><a id="A1" runat="server" onclick='<%#"PrintVoucherN(" +Eval("PaymentId").ToString() + ");"%>' style="color:Blue; text-decoration:underline;cursor:pointer;" visible='<%#(Eval("PTYPE").ToString()=="N")%>'><%#Eval("PVNo")%></a>
                                            <a id="A2" runat="server" onclick='<%#"PrintVoucherO(" +Eval("PaymentId").ToString() + ");"%>' style="color:Blue; text-decoration:underline;cursor:pointer;" visible='<%#(Eval("PTYPE").ToString()=="O")%>'><%#Eval("PVNo")%></a><a runat="server" href='<%#"InvoicePayment.aspx?key=" + Eval("PaymentId").ToString()%>' target="_blank" visible='<%#(Eval("PTYPE").ToString()=="O") && (Eval("PVNo").ToString().Trim()=="")%>'>Create PV</a>
                                            <asp:LinkButton ID="btnEdit1" runat="server" CommandArgument='<%#Eval("PaymentId")%>' OnClick="btnEdit_Click" Text="Create PV" ToolTip="Create PV" Visible='<%#(Eval("PTYPE").ToString()=="N") && (Eval("PVNo").ToString().Trim()=="")%>' />
                                        </td>
                                        <td align="left"><%#Eval("SupplierName")%><span runat="server" id="c1" visible='<%#(Eval("Status").ToString()=="C")%>' style="color:red"> - Cancelled </span>
                                            <b style="color:Red"><i style='display: <%#Eval("TravId").ToString() == "" ? "none" : "inline" %>'>( <%#Eval("TravId")%>) </i></b>
                                        </td>
                                        <td align="right"><asp:ImageButton runat="server" ID="btnChangeVendor" CommandArgument='<%#Eval("PaymentId")%>' OnClick="btnChangeVendor_Click" ImageUrl="~/Modules/HRD/Images/addpencil.gif" ToolTip="Update Vendor" Visible='<%#( Eval("PTYPE").ToString()=="O" && (Eval("PVNo").ToString().Trim()!="") && (Eval("Exported").ToString().Trim()!="True") )%>' /> </td>
                                        <td align="right"><%#Eval("VoucherNo")%></td>
                                        <td align="left"><%#Eval("Currency")%></td>
                                        <td align="left"><%#Eval("Amount")%></td>
                                        <td align="left"><%#Eval("PaidBy")%></td>
                                        <td align="left"><%#Common.ToDateString(Eval("paidon"))%></td>
                                        <td align="left"><%#((Eval("PaymentType").ToString()=="U")?"USD":((Eval("PaymentType").ToString()=="S")?"SGD":"INR"))%></td>
                                         <td>
                                             <%--<img src="../Images/check_white.png" runat="server" BankCharges='<%#Eval("BankCharges")%>' BankAmount='<%#Eval("BankAmount")%>' onclick="OpenUpdate(this)" visible='<%#(Common.ToDateString(Eval("BankConfirmedOn"))=="") && (Eval("Status").ToString()=="A")%>' RecordType='<%#Eval("PTYPE")%>' paymentid='<%#Eval("PaymentId")%>' /> --%>
                                             <asp:ImageButton ID="imgUpdateAmountOpenPop" runat="server" ImageUrl="~/Modules/HRD/Images/check_white.png" OnClick="imgUpdateAmountOpenPop_OnClick" visible='<%#(Common.ToDateString(Eval("BankConfirmedOn"))=="") && (Eval("Status").ToString()=="A" && (Eval("PTYPE").ToString()=="O"))%>'/>

                                             <asp:HiddenField ID="hfBankCharges" runat="server" Value='<%#Eval("BankCharges")%>' />
                                             <asp:HiddenField ID="hfBankAmount" runat="server" Value='<%#Eval("BankAmount")%>' />
                                             <asp:HiddenField ID="hfRecordType" runat="server" Value='<%#Eval("PTYPE")%>' />
                                             <asp:HiddenField ID="hfPaymentId" runat="server" Value='<%#Eval("PaymentId")%>' />
                                         </td>
                                       <td>
                                            <asp:Label runat="server" ID="txtBCharges" Text='<%#Eval("BankCharges")%>' Width="95%" style="text-align:right" MaxLength="12" onkeyup="UpdateBankChgs(this)" RecordType='<%#Eval("PTYPE")%>' paymentid='<%#Eval("PaymentId")%>' CssClass='<%#((Common.ToDateString(Eval("BankConfirmedOn"))=="")?"pending":"")%>'></asp:Label>
                                        </td>
                                        
                                       <td>
                                            <asp:Label runat="server"  ID="txtBamount" Text='<%#Eval("BankAmount")%>' Width="95%" style="text-align:right" MaxLength="12" onkeyup="UpdateBankAmt(this)" RecordType='<%#Eval("PTYPE")%>'  paymentid='<%#Eval("PaymentId")%>' CssClass='<%#((Common.ToDateString(Eval("BankConfirmedOn"))=="")?"pending":"")%>'></asp:Label>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </AlternatingItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                    <br />
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="">
                                <asp:Button ID="btnClose" runat="server" OnClientClick="window.close()" style="  border:none; padding:4px;" Text="Close" Width="150px" CssClass="btn" />
                            </td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
       </table>
   
    <!-----------------Add Payment Voucher ------------------------>
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;" id="dv_NewPV" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:1; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:90%; padding :5px; text-align :center;background : white; z-index:2;top:50px; border:solid 10px black;">
            <center >
             <div style="padding:6px;  font-size:14px; " class="text headerband"><b>Payment Voucher</b></div>
             <div style="width:100%; text-align:left; overflow-y:scroll; overflow-x:hidden;">
             <table border="0" bordercolor="#F0F5F5" cellpadding="2" cellspacing="0" style="height: 100px; text-align: center; border-collapse:collapse; width:100%;">
                      <tr style="background-color:#E6F3FC">
                          <td align="right" style="text-align: right; padding-right:15px; width: 123px;">
                              Vendor:</td>
                          <td style="text-align: left; width:40%; ">
                              <asp:TextBox ID="txtVendorName" runat="server" CssClass="input_box" Width="80%"></asp:TextBox>
                              <asp:CheckBox ID="chkNotinList" Text="Not in list" runat="server" />
                              <asp:HiddenField runat="server" id="hfdSupplierId" />
                              
                          </td>
                          <td style="text-align: right;">PV # :
                              </td>
                          <td style="text-align: left;width:450px;">
                              <asp:Label ID="lblPVNo" runat="server" Font-Bold="true" ></asp:Label>
                              
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px; width: 123px;">
                              Owner:
                              </td>
                          <td style="text-align: left; width:40%; ">
                              <asp:DropDownList ID="ddlOwner1" runat="server" Width="80%"></asp:DropDownList>
                              <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="ddlOwner1" ErrorMessage="*" ></asp:RequiredFieldValidator>  

                          </td>
                          <td style="text-align: right;">
                          Payment Mode:
                              </td>
                          <td style="text-align: left;width:450px;">
                              <asp:RadioButton runat="server" ID="rad_SGD" Text="SGD" GroupName="f1" />
                              <asp:RadioButton runat="server" ID="rad_USD" Text="USD" GroupName="f1" />
                              <asp:RadioButton runat="server" ID="rad_INR" Text="INR" GroupName="f1" />
                          </td>
                      </tr>
                      <tr style="background-color:#E6F3FC">
                          <td align="right" style="text-align: right; padding-right:15px; width: 123px;">
                              Bank Name:</td>
                          <td style="text-align: left;">
                              <asp:TextBox ID="txtBankName" runat="server" CssClass="input_box" Width="80%"></asp:TextBox>
                             </td>
                           <td style="text-align: right;">
                               Credit Act #:</td>
                           <td style="text-align: left;">
                               <asp:TextBox ID="txtCreditActNo" runat="server" CssClass="input_box" Width="159px"></asp:TextBox>
                           </td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px; width: 123px;">
                              Credit Act Name:</td>
                          <td style="text-align: left;">
                              <asp:TextBox ID="txtCreditActName" runat="server"  CssClass="required_box" 
                                  Width="302px" ></asp:TextBox>&nbsp;
                              </td>
                          <td style="text-align: right;">
                              Voucher #:</td>
                          <td style="text-align: left;">
                              <asp:TextBox ID="txtVoucherNo" runat="server" CssClass="input_box" 
                                  Width="126px"></asp:TextBox>
                              &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                  ControlToValidate="txtVoucherNo" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                          </td>
                      </tr>
                      <tr style="background-color:#E6F3FC">
                          <td align="right" style="text-align: right; padding-right:15px; width: 123px; ">Cheque/ TT # :</td>
                          <td style="text-align: left;">
                              <asp:TextBox ID="txtChequeTTNo" runat="server" CssClass="required_box" MaxLength="49" Width="126px"></asp:TextBox>
                              &nbsp;<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtChequeTTNo" ErrorMessage="*" ></asp:RequiredFieldValidator>  
                          </td>
                          <td style="text-align: right;">
                              Cheque/ TT Amt.:</td>
                          <td style="text-align: left;">
                              <asp:TextBox ID="txtChequeTTAmt"  runat="server" CssClass="required_box" Width="126px" MaxLength="12" style="text-align:right"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtChequeTTAmt" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                              &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtChequeTTAmt" ErrorMessage="Till 2 decimal places only." ValidationExpression="[-]?\b\d{1,13}\.?\d{0,2}" Display="Dynamic"></asp:RegularExpressionValidator></td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px; width: 123px; ">Cheque/ TT Date:</td>
                          <td style="text-align: left;">
                              <asp:TextBox ID="txtChequeTTDate" runat="server" CssClass="input_box" Width="126px"  ></asp:TextBox>
                              &nbsp;<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtChequeTTDate" ErrorMessage="*" ></asp:RequiredFieldValidator>  
                          </td>
                          <td style="text-align: right;">Bank Charges:</td>
                          <td style="text-align: left;">
                              <asp:TextBox ID="txtBankCharges"  runat="server" CssClass="required_box" Width="126px" MaxLength="12" style="text-align:right"></asp:TextBox>
                              &nbsp;&nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtBankCharges" ErrorMessage="Till 2 decimal places only." ValidationExpression="[-]?\b\d{1,13}\.?\d{0,2}" Display="Dynamic"></asp:RegularExpressionValidator></td>
                      </tr>
                      <tr>
                      <td colspan="4">
                             
                     <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden;HEIGHT: 65px ; text-align:center; border-bottom:none;">
                                           <table border="1" bordercolor="white" cellpadding="4" cellspacing="0" rules="all" style="width:100%; height:30px;  border-collapse:collapse;">
                                            <colgroup>
                                                <col style="width:40px;"/>
                                                <col style=""/>
                                                <col style="width:120px;" />
                                                <col style="width:120px;text-align:center;" />
                                                <col style="width:120px;text-align:center;" />
                                                <col style="width:120px;text-align:center;" />
                                                <col style="width:60px;"/>
                                                <tr class= "headerstylegrid">
                                                    <td>SR#</td>
                                                    <td>Description</td>
                                                    <td>InviceNo.</td>
                                                    <td>Invice. Dt.</td>
                                                    <td>AccountNo</td>
                                                    <td>Amount</td>
                                                    <td></td>
                                                </tr>
                                                <tr >
                                                    <td>
                                                        <asp:TextBox runat="server" ID="txtSRNo" Width="20px" ValidationGroup="c11"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtSRNo" ErrorMessage="*" ValidationGroup="c11" ></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                    <asp:TextBox runat="server" ID="txtDescr" Width="95%" ValidationGroup="c11"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDescr" ErrorMessage="*" ValidationGroup="c11" ></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td><asp:TextBox runat="server" ID="txtInvoiceNo" Width="85%" style="text-align:right" ValidationGroup="c11"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtInvoiceNo" ErrorMessage="*" ValidationGroup="c11" ></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td><asp:TextBox runat="server" ID="txtOnDate" Width="85%" style="text-align:center" ValidationGroup="c11"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtOnDate" ErrorMessage="*" ValidationGroup="c11" ></asp:RequiredFieldValidator>
                                                    </td>
                                                     <td><asp:TextBox runat="server" ID="txtAcNo" Width="85%" style="text-align:right" ValidationGroup="c11"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtAcNo" ErrorMessage="*" ValidationGroup="c11" ></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td><asp:TextBox runat="server" ID="txtAmount" Width="85%" style="text-align:right" ValidationGroup="c11"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtAmount" ErrorMessage="*" ValidationGroup="c11" ></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td style="text-align:left"><asp:ImageButton runat="server" ID="btnAddRow" ImageUrl="~/Modules/HRD/Images/add.png" ValidationGroup="c11" OnClick="btnAddRow_Click"/></td>
                                                </tr>
                                              
                                            </colgroup>
                                        </table>
                                        </div>
                    <div id="divPayment" runat="server" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; HEIGHT:250px; text-align:center;">
                                            <table border="1" bordercolor="#F0F5F5" cellpadding="4" cellspacing="0" style=" text-align: center; border-collapse:collapse; width:100%;">
                                                <colgroup>
                                                
                                                <col style="width:40px;"/>
                                                <col style=""/>
                                                <col style="width:120px;" />
                                                <col style="width:120px;text-align:center;" />
                                                <col style="width:120px;text-align:center;" />
                                                <col style="width:120px;text-align:center;" />
                                                <col style="width:60px;"/>
                                                </colgroup>
                                                <asp:Repeater ID="RptDetails" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td align="left"><%#Eval("SRNO")%>.</td>
                                                            <td align="left"><%#Eval("Description")%></td>
                                                            <td align="right"><%#Eval("InvoiceNo")%></td>
                                                            <td align="center"><%#Common.ToDateString(Eval("OnDate"))%></td>
                                                            <td align="right"><%#Eval("AccountNo")%></td>
                                                            <td align="right"><%#Eval("Amount")%></td>
                                                            
                                                            <td align="left">
                                                                <asp:ImageButton runat="server" CommandArgument='<%#Eval("TableId")%>' ID="btnEdit" ImageUrl="~/Images/AddPencil.gif" OnClick="btnEditRow_Click" CausesValidation="false" />
                                                                <asp:ImageButton runat="server" CommandArgument='<%#Eval("TableId")%>' OnClientClick="return window.confirm('Are you sure to delete this record?');" ID="btnAddRow" ImageUrl="~/Modules/HRD/Images/delete.png" OnClick="btnDelete_Click" CausesValidation="false" />
                                                            </td> 
                                                        </tr>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                       <tr style="background-color:#E6F3FC">
                                                            <td align="left"><%#Eval("SRNO")%>.</td>
                                                            
                                                            <td align="left"><%#Eval("Description")%></td>
                                                            <td align="right"><%#Eval("InvoiceNo")%></td>
                                                            <td align="center"><%#Common.ToDateString(Eval("OnDate"))%></td>
                                                            <td align="right"><%#Eval("AccountNo")%></td>
                                                            <td align="right"><%#Eval("Amount")%></td>
                                                            <td align="left">
                                                                <asp:ImageButton runat="server" CommandArgument='<%#Eval("TableId")%>' ID="btnEdit" ImageUrl="~/Modules/HRD/Images/AddPencil.gif" OnClick="btnEditRow_Click" CausesValidation="false" />
                                                                <asp:ImageButton runat="server" CommandArgument='<%#Eval("TableId")%>' OnClientClick="return window.confirm('Are you sure to delete this record?');" ID="btnAddRow" ImageUrl="~/Modules/HRD/Images/delete.png" OnClick="btnDelete_Click" CausesValidation="false" />
                                                            </td>                                                                                                                       
                                                        </tr>
                                                    </AlternatingItemTemplate>
                                                </asp:Repeater>
                                             </table>
                                        </div>
                                        <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden;HEIGHT: 30px ; text-align:center; border-bottom:none;">
                                           <table border="1" bordercolor="white" cellpadding="4" cellspacing="0" rules="all" style="width:100%; height:30px;  border-collapse:collapse;">
                                            <colgroup>
                                                <col style="width:50px;"/>
                                                <col style=""/>
                                                <col style="width:120px;" />
                                                <col style="width:120px;text-align:center;" />
                                                <col style="width:120px;text-align:center;" />
                                                <col style="width:120px;text-align:center;" />
                                                <col style="width:60px;"/>
                                                </colgroup>
                                                <tr style="background-color:#008AE6; color:White;" >
                                                    <td></td>
                                                    <td></td>
                                                    <td>
                                                    <%--Currency :--%>
                                                    </td>
                                                    <td> 
                                                        <%--<asp:DropDownList ID="ddCurrency"  runat="server" CssClass="input_box" Width="100px"></asp:DropDownList>&nbsp;
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddCurrency" InitialValue="0" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                    </td>
                                                    <td>Total:</td>
                                                    <td align="right"><asp:Label ID="lbltotal" runat="server"></asp:Label></td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            
                                        </table>
                                        </div>

                      </td>
                      </tr>
                      <tr style="background-color:#E6F3FC">
                          <td align="right" style="text-align: right; padding-right:15px; width: 123px;">Remarks:</td>
                          <td style="text-align: left;" colspan="3"><asp:TextBox ID="txtComments" runat="server" CssClass="input_box" Height="50px" TextMode="MultiLine" Width="80%"></asp:TextBox></td>
                      </tr>
                     </table>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupPosition="TopRight" TargetControlID="txtChequeTTDate"></asp:CalendarExtender>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupPosition="TopRight" TargetControlID="txtOnDate"></asp:CalendarExtender>
                     
                <div>
                <center>
                <asp:Button ID="btnSaveVoucher" runat="server" OnClick="btnSaveVoucher_Click" OnClientClick="this.value='Loading..';" style=" border:none; padding:4px;" Text="Save Voucher" Width="150px" CssClass="btn" />
                <asp:Button ID="btnCreatePV" runat="server" OnClick="btnCreatePV_Click" OnClientClick="this.value='Loading..';" style="  border:none; padding:4px;" Text="Create PV" Width="150px" CssClass="btn" />
                <asp:Button ID="btnVoucherPrint" runat="server" OnClick="btnVoucherPrint_Click" OnClientClick="this.value='Loading..';" style="  border:none; padding:4px;" Text="Print" Width="150px" CssClass="btn" />
                <asp:Button ID="btnClosePOP" runat="server" Text="Close" Width="150px" Causesvalidation="False" style="  border:none; padding:4px;" OnClick="btnClosePOP_Click" CssClass="btn"/>
                
                </center>
                <div>
                <asp:Label runat="server" ID="lblMsgPOP" Font-Bold="true"></asp:Label>
                </div>
                </div>
            </div>
            </center>
        </div>
    </center>
    </div>
    <!-----------------Cancel Payment Voucher ------------------------>
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;" id="dvCancel" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:500px; padding :5px; text-align :center;background : white; z-index:150;top:50px; border:solid 10px black;">
            <center >
             <div style="padding:6px; background-color:#00ABE1; font-size:14px; color:#fff;"><b>Payment Voucher Cancellation</b></div>
                <div>
                <center>
                     <div style="padding:5px"><asp:Label runat="server" ID="lblCVno" Font-Bold="true"></asp:Label></div>
                   <div style="padding:5px">Please enter reason for cancellation..</div>
                    <div  style="padding:5px">
                        <asp:TextBox runat="server" ID="txtCReason" Width="95%" Rows="6"  ValidationGroup="c25" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="v1" ControlToValidate="txtCReason" ErrorMessage="*" ValidationGroup="c25"></asp:RequiredFieldValidator>
                    </div>
                <asp:Button ID="btnCancelNow" runat="server" OnClick="btnCancelNow_Click"  ValidationGroup="c25" style=" background-color:#006EB8; color:White; border:none; padding:4px;" Text="Cancel Voucher" Width="150px" />
                <asp:Button ID="btnCloseNow" runat="server" Text="Close" Width="150px" Causesvalidation="False" style=" background-color:red; color:White; border:none; padding:4px;" OnClick="btnCloseNow_Click"/>
                
                </center>
               
                </div>
             </center>
             </div>
    </center>
    </div>

    <!-- End -->
    <asp:Label runat="server" ID="lblMsgMain" Font-Bold="true"></asp:Label>
    <br />

        <div style="width:100%;position:fixed; height:100%; text-align:center;left:0px; top:0px; background-color:rgba(0, 0, 0, 0.38)" id="dvUpdate" runat="server" visible="false">
            <div style="width:750px; margin:0 auto; border:solid 10px #008AE6;padding:0px; background-color:white;position:relative; top:0px; top:20px;">
                <div style="padding:10px; " class="text headerband">
                    
                    <div style="width:30px;float:right;"> 
                        <asp:ImageButton ID="btnCloseUpdateAmountPop" runat="server" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="btnCloseUpdateAmountPop_OnClick" />
                    </div>
                    <b>Update Bank Amount</b>
                </div>
                <table width="100%" cellpadding="5" border="1" bordercolor="#c2c2c2" style="border-collapse:collapse">
                    <colgroup>
                        <col style="width:190px"/>
                        <col />
                    </colgroup>
                        <tr>
                            <td style=" text-align:right;"><b>PV No. :</b></td>
                            <td style="text-align:left"><asp:Label runat="server" ID="lblPVNO1"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style=" text-align:right;" ><b>Vendor :</b></td>
                            <td style="text-align:left"><asp:Label runat="server" ID="lblVendorName"></asp:Label> (<asp:Label runat="server" ID="lblVendorCode" Text="" ForeColor="Red"></asp:Label>) </td>
                        </tr>
                        <tr>
                            <td style=" text-align:right;" ><b>Owner :</b></td>
                            <td style="text-align:left"><asp:Label runat="server" ID="lblOwner"></asp:Label> (<asp:Label runat="server" ID="lblOwnerCode" Text="" ForeColor="Red"></asp:Label>) </td>
                        </tr>
                </table>
                
                     <div style="padding:5px; background-color:#E6F3FC; color:#333; font-weight:bold;"> 
                         Invoice List
                     </div>
                        <div style="height:32px; overflow-x:hidden;overflow-y:scroll">
                            <table border="1" bordercolor="#c2c2c2" cellpadding="2" cellspacing="0" rules="all" style="width:100%; height:30px;  border-collapse:collapse;">
                               <tr style="background-color:#a2a6a9; color:White; height:30px;">
                                    <td style="width:50px">Vessel</td>
                                    <td>Invoice#</td>
                                    <td style="width:100px">Inv. Date</td>
                                    <td style="width:150px">Invoice Amount ( <asp:Label runat="server" ID="lblinvcurr"></asp:Label> ) </td>
                                    <td style="width:200px">Bank Amount ( <asp:Label runat="server" ID="lblpaycurr" ForeColor="#e20959"></asp:Label> ) </td>
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
                                        <td><%#Eval("InvoiceNo")%>
                                            <asp:HiddenField runat="server" ID="hfdtableId" Value='<%#Eval("tableId")%>' />
                                        </td>
                                        <td style="width:100px"><%#Common.ToDateString(Eval("OnDate")) %></td>
                                        <td style="text-align:right;width:150px" ><%#Eval("Amount")%></td>
                                        <td style="text-align:right;width:200px" ><asp:TextBox ID="txtAmount" runat="server"  Width="99%" onkeyup="autosum();"  CssClass='amtcalc' Text='<%#Eval("BankAmount")%>' style="text-align:right; background-color:#f7f841;" ></asp:TextBox></td>
                                        <td style="width:25px">&nbsp;</td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            </table>
                        </div>
                <div style="height:32px; overflow-x:hidden;overflow-y:scroll">
                              <table border="1" bordercolor="#c2c2c2" cellpadding="2" cellspacing="0" rules="all" style="width:100%; height:30px;  border-collapse:collapse;">
                                <tr style="background-color:#eeeeef; color:000; height:30px;">
                                    <td style="width:50px"><b>Total :</b></td>
                                    <td></td>
                                    <td style="width:100px"></td>
                                    <td style="text-align:right;width:150px"><asp:Label runat="server" ID="lblTotalInvAmount" Text="0.00" style="font-weight: bold;color: #e20959;font-size: 18px;" ></asp:Label></td>
                                    <td style="text-align:right;width:200px"><asp:Label runat="server" ID="lblTotalBankAmount" Text="0.00" style="font-weight: bold;color: #e20959;font-size: 18px;" ></asp:Label></td>
                                    <td style="width:25px">&nbsp;</td>
                                </tr>
                                </table>
                    </div>
                 <div style="padding:5px; background-color:#E6F3FC; color:#333; font-weight:bold;"> 
                         Bank Charges
                     </div>
                   <table width="100%" cellpadding="5" border="1" bordercolor="#c2c2c2" style="border-collapse:collapse">
                    <colgroup>
                        <col style="width:190px"/>
                        <col />
                    </colgroup>
                        <tr>
                        <td style="text-align:right"><b>Bank Trans. Date :</b></td>
                        <td style="text-align:left"> 
                            <asp:TextBox ID="BankConfirmedOnPopup" runat="server" style="width:80px;background-color:#f7f841;"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy" PopupPosition="TopRight" TargetControlID="BankConfirmedOnPopup"></asp:CalendarExtender>
                        </td>
                        </tr>
                        <tr>
                        <td style="text-align:right"><b>Bank Charges :</b></td>
                        <td style="text-align:left"> 
                            <asp:HiddenField ID="hfhfRecordType_popup" runat="server" Value="" />
                            <asp:TextBox ID="txtBankChargesPopup" onkeyup="showamts(this);" runat="server" style="width:80px;text-align:right;background-color:#f7f841;"></asp:TextBox>
                            ( <asp:Label runat="server" ID="lblpaycurr1" ForeColor="#e20959"></asp:Label> )
                        </td>
                        </tr>
                       <tr>
                        <td style="text-align:right"><b>Charge to Vessel:</b></td>
                        <td style="text-align:left"> 
                            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                            <ContentTemplate>
                                        <asp:DropDownList runat="server" ID="ddlVessels" style="background-color:#f7f841;" AutoPostBack="true" OnSelectedIndexChanged="ddlVessels_OnSelectedIndexChanged" ></asp:DropDownList>
                                                </ContentTemplate>
                                        </asp:UpdatePanel>
                        </td>
                        </tr>
                    </table>
                <table border="1" bordercolor="#c2c2c2" cellpadding="4" cellspacing="0" rules="all" style="width:100%; height:30px;  border-collapse:collapse;">
                                           
                               <tr style="background-color:#a2a6a9; color:White; height:30px;">
                                    <td>Bank Chgs. ( Traverse Entry )</td>
                                    <td>Account No.</td>
                                    <td style="width:200px">Amount ( <asp:Label runat="server" ID="lblpaycurr2" ForeColor="#e20959"></asp:Label> )</td>
                                   <td style="width:25px">&nbsp;</td>
                                </tr>
                                <tr style="">
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
                                </tr>
                                  <tr style="">
                                    <td>Debit Amount </td>
                                    <td>
                                        <asp:UpdatePanel runat="server" ID="up12" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" ID="txtDebitAccountCode" MaxLength="4" Width="35px" style="text-align:center;background-color:#f7f841;"></asp:TextBox>
                                                .
                                                <asp:TextBox runat="server" ID="txtDVesselNo" MaxLength="4" Width="35px" style="text-align:center;" Enabled="false"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        </td>
                                    <td style="width:200px; text-align:right;font-weight: bold;color: #e20959;font-size: 18px;"><span style="padding-right:3px;">-</span><asp:Label runat="server" ID="lblDebitAmount" Text="0.00"></asp:Label></td>
                                        <td style="width:25px">&nbsp;</td>
                                </tr>
                    </table>
                 <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                                            <ContentTemplate>
                    <div style="padding:5px;">
                          <asp:Button ID="btnUpdateAmount" runat="server" OnClick="btnUpdateAmount_OnClick"  style="  border:none; padding:4px;" Text="Save" Width="150px" CssClass="btn" />
                            <asp:Button ID="btnOpenExport" runat="server" OnClick="btnOpenExport_OnClick"  style="  border:none; padding:4px;" Text="Open Export" Width="200px" Visible="false" CssClass="btn" />

                        </div>
                <div style="background:#f7f841; padding:5px;">
                    <asp:Label runat="server" ID="lblmsg1" Font-Bold="true"></asp:Label>
                </div>
                    </ContentTemplate>
                     <Triggers>
                         <asp:PostBackTrigger ControlID="btnOpenExport" />
                     </Triggers>
                                        </asp:UpdatePanel>         
            </div>
            
        </div>
    
        <!-----------------Export Payment Voucher ------------------------>
        <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;" id="dvExport" runat="server" visible="false">
        <center>
            <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
            <div style="position:relative;width:90%; padding :5px; text-align :center;background : white; z-index:150;top:50px; border:solid 10px black;">
                <center >
                   <iframe runat="server" width="100%" height="550px" id="frm1"></iframe>
                 </center>
                <div style="padding:5px; text-align:center">
                    <asp:Button ID="Button2" runat="server" Text="Close" Width="150px" Causesvalidation="False" style=" border:none; padding:4px;" OnClick="btnCloseNow1_Click" CssClass="btn"/>
                </div>
                </div>
                </center>
        </div>


         <!-----------------Change Vendor ------------------------>
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;" id="dvChangeVendor" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:1; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:1000px; padding :5px; text-align :center;background : white; z-index:2;top:0px; border:solid 10px black;">
            <center >
                <div style="padding:6px;font-size:18px; " class="text headerband"><b> Change Vendor </b></div>
                <table width="100%" cellpadding="5" border="1" style="border-collapse:collapse;">
                <tr>
                    <td style="width:130px">Vendor :</td>
                    <td style="text-align:left">
                        <asp:Label runat="server" ID="lblVendorName2"></asp:Label> (<asp:Label runat="server" ID="lblVendorCode2" Text="" ForeColor="Blue"></asp:Label>) 
                    </td>
                </tr>
                    <tr>
                    <td >New Vendor :</td>
                    <td style="text-align:left">
                        <asp:TextBox runat="server" ID="txtSupplier" Width="80%"></asp:TextBox>&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtSupplier" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                    </td>
                </tr>
                </table>
                <div style="padding:5px;">
                     <asp:Button ID="btnchangevendorsave" runat="server" OnClick="btnchangevendorsave_Click" ValidationGroup="c25" style=" border:none; padding:4px;" Text="Save" Width="150px" CssClass="btn" />
                    <asp:Button ID="Button1" runat="server" Text="Close" Width="150px" Causesvalidation="False" style="  border:none; padding:4px;" OnClick="btnCloseNow2_Click" CssClass="btn"/>
                </div>
                <div style="padding:5px;background-color:#f7f841">
                            <asp:Label runat="server" ID="lblmsg2" ForeColor="Red" Font-Bold="true"></asp:Label>

                         </div>
            </center>
        </div>
    </center>
    </div>




        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    <asp:HiddenField ID="hfInvId" runat="server"  />
    <asp:Button ID="btndownload" Text=""  OnClick="btnDownloadFile_Click" style="display:none;" runat="server" /> 
    </div>
    </center>
    </div>
    </form>
</body>
    <script type="text/javascript">
    function Page_CallAfterRefresh() {
        RegisterAutoComplete();
        RegisterAutoComplete1();
    }
    $(document).ready(function () {
        RegisterAutoComplete();
        RegisterAutoComplete1();
    });
    function PrintVoucherN(pid) {
        winref = window.open('PaymentVoucher.aspx?PaymentId=' + pid + '&PaymentMode=N', '');
        return false;
    }
    function PrintVoucherO(pid) {
        winref = window.open('PaymentVoucher.aspx?PaymentId=' + pid + '&PaymentMode=O', '');
        return false;
    }
    function autosum()
    {
        var tot = 0;
        $(".amtcalc").each(function (i, a) {
		var amt = parseFloat($(a).val());
		if(!isNaN(amt))
	            tot += amt;
        });
        $("#lblTotalBankAmount").html(tot.toFixed(2));
    }
    function showamts(ctl) {
        $("#lblCreditAmount").html($(ctl).val());
        $("#lblDebitAmount").html($(ctl).val());
    }
</script>
</html>
