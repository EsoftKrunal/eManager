<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentList.aspx.cs" Inherits="Modules_Purchase_Invoice_PaymentList" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <title>EMANAGER</title>
     <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="JS/jquery.min.js"></script>
     <script src="JS/AutoComplete/knockout-2.2.1.js" type="text/javascript"></script>
     <!-- Auto Complete -->
     <link rel="stylesheet" href="JS/AutoComplete/jquery-ui.css?11" />
     <script src="JS/AutoComplete/jquery-ui.js?11" type="text/javascript"></script>
    
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
    <style type="text/css" >
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

    .searchsection
    {
        display:flex;
        justify-content:space-between;
        padding:5px;
    }

    .searchsectiontd
    {
        display:flex;       
        padding:5px;
    }

    .searchsection-textaligntd{
        text-align:left;
        width:50%;
    }

    .searchsection-textalign{
        text-align:left;
    }

    .searchsection-btn {
       display:flex;
       justify-content:end;
       padding:5px;
    }

    .searchsection-child {
        width:120px;
        text-align:left;
        padding-left:5px;
        display:inline-flex;
    }

@media screen and (max-width:767px) {
    .searchsection {
        display:flex;
        flex-direction:column;
        padding:5px;
    }

    .searchsectiontd {
        display:flex;
        flex-direction:column;
        padding:5px;
    }

    .searchsection-textaligntd {
    text-align: left;
    width: 95%;
    margin: 0 auto;
    padding-bottom: 5px;
}

    .inner-field {
        width: 100% !important;
        margin-bottom:7px;
    }

    .div_dialog {
        width: 90% !important;
    }
}

        .auto-style1 {
            width: 100%;
            height: 30px;
        }

        .auto-style3 {
            height: 36px;
        }

        </style>
    <script type="text/javascript">
        function decimalValidation(el, evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            var number = el.value.split('.');
            if (charCode == 8) {
                return true;
            }
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            //just one dot
            if (number.length > 1 && charCode == 46) {
                return false;
            }
            //get the carat position
            var caratPos = getSelectionStart(el);
            var dotPos = el.value.indexOf(".");
            if (caratPos > dotPos && dotPos > -1 && (number[1].length > 1)) {
                return false;
            }
            return true;
        }

        function getSelectionStart(o) {
            return o.selectionStart
        }
    </script>
     <script type="text/javascript">
         function OpenDocument(ID, PVNo) {
             // alert('Hi');
             window.open("../Requisition/ShowDocuments.aspx?DocId=" + ID + "&PVNo=" + PVNo + "&PRType=PaymentDocument");
         }
     </script>
</head>
<body>
    <form id="form1" runat="server">
       
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ></asp:ToolkitScriptManager>
    <div id="log" style="display:none"></div>
    <div style="border:solid 1px #008AE6;">
    <%--<asp:UpdatePanel runat="server" id="up1">
    <ContentTemplate>--%>
        <div style="background-color:#E6F3FC;">
             <div style=" padding:6px; text-align:center;" class="text headerband">
                 <strong>Invoice Management - Payment </strong>
            </div>
            <div class="searchsection"> 
                <div class="searchsection-textalign">
                    &nbsp;Period :
                     <asp:TextBox ID="txtF_D1" runat="server" style="border:solid 1px #008AE6;" CssClass="inner-field"></asp:TextBox>
                                <asp:CalendarExtender ID="c1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtF_D1">
                                </asp:CalendarExtender>
                                <asp:TextBox ID="txtF_D2" runat="server" style="border:solid 1px #008AE6;" CssClass="inner-field"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtF_D2">
                                </asp:CalendarExtender>
                    </div>
                     <div class="searchsection-textalign">
                         Payment Status :
                         <asp:DropDownList ID="ddlStatus" runat="server" CssClass="inner-field" AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                     <asp:ListItem Text=" < Payment Status > " Value=""></asp:ListItem>
                                     <asp:ListItem Text="UnPaid" Value="U" Selected="True"></asp:ListItem>
                                     <asp:ListItem Text="Paid" Value="P" ></asp:ListItem>
                                   
                                </asp:DropDownList>

                               <%--  <asp:DropDownList ID="ddlStatus1" runat="server" CssClass="inner-field">
                                     <asp:ListItem Text=" < Bank Status > " Value=""></asp:ListItem>
                                     <asp:ListItem Text="Bank Update Pending" Value="P" Selected="True"></asp:ListItem>
                                     <asp:ListItem Text="Bank Update Completed" Value="C"></asp:ListItem>
                                </asp:DropDownList>--%>
                         </div>
                 <div class="searchsection-textalign">
                     Vendor :
                     <asp:TextBox ID="txtF_Vendor" runat="server" style="border:solid 1px #008AE6;" CssClass="inner-field"></asp:TextBox>
                     </div>
               </div>
             <div class="searchsection"> 
                 <div class="searchsection-textalign">
                     &nbsp; Account Company :
                       <asp:DropDownList ID="ddlF_Owner" runat="server" CssClass="inner-field" AutoPostBack="True" OnSelectedIndexChanged="ddlF_Owner_SelectedIndexChanged">
                                </asp:DropDownList>
                     </div>
                 <div class="searchsection-textalign">
                     &nbsp;Inv # : 
                      <asp:TextBox ID="txtF_InvNo" runat="server" style="border:solid 1px #008AE6;" CssClass="inner-field"></asp:TextBox>
                     </div>
                 <div class="searchsection-textalign">
                     <asp:Label ID="lblNo" runat="server" Text="RFP # :" ></asp:Label>
                     <asp:TextBox ID="txtF_PVNo" runat="server" style="border:solid 1px #008AE6;" CssClass="inner-field"></asp:TextBox>
                     </div>
                 </div>
              <div class="searchsection"> 
                 <div class="searchsection-textalign">
                     &nbsp; Vessel :
                       <asp:DropDownList ID="ddl_Vessel" runat="server" CssClass="inner-field">
                                </asp:DropDownList>
                     </div>
                  </div>
            <div class="searchsection-btn">
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Style="float:left;"></asp:Label>
                <asp:Button ID="btn_Search" runat="server" OnClick="btn_Search_Click" OnClientClick="this.value='Loading..';" style="  border:none; padding:4px;" Text="Search" Width="100px" CssClass="btn" /> &nbsp;
                             
                                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" ToolTip="Clear" CssClass="btn" /> &nbsp;
                <asp:Button ID="btnExportToExcel" runat="server" Text="Download Excel" CssClass="btn" ToolTip="Export to Excel" OnClick="btnExportToExcel_Click" /> &nbsp;
                <asp:Button ID="btnMakePayment" runat="server" Text="Make Payment" CssClass="btn" OnClick="btnMakePayment_Click"/>
                                <asp:HiddenField ID="hfdSupplier" runat="server" />
                </div>

            </div>
        
        <div class="table-responsive">
                    <div id="dvUnpaidStatus" runat="server" visible="false"> 
                    <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden;HEIGHT: 30px ; text-align:center; border-bottom:none;">
                        <table border="1" bordercolor="white" cellpadding="4" cellspacing="0" rules="all" style="width:100%; height:30px;  border-collapse:collapse;">
                            <colgroup>
                                <col style="width:3%;"/>
                              <%--  <col style="width:200px;" />--%>
                                <col style="width:9%;" />
                                <col style="width:11%;" />
                                <col />
                                <col style="width:4%;"  />
                                <col style="width:10%;" />
                                <col style="width:8%;" />
                                <col style="width:8%;" />
                                <col style="width:8%;" />
                                <col style="width:8%;" />
                                <col style="width:4%;" />
                                <col style="width:14%;" />
                                <%--<col style="width:25px;"/>--%>
                                <tr align="left" class= "headerstylegrid">
                                    <td></td>
                                 <%--   <td>Account Company </td>--%>
                                    <td>Vessel </td>
                                    <td>RFP #</td>
                                    <td>Vendor</td>                                   
                                    <td>Curr.</td>
                                    <td>Amount </td>
                                    <td>Created By </td>
                                    <td>Created On </td>
                                    <td>Approved By </td>
                                    <td>Approved On </td>
                                    <td>PayIn</td>
                                    <td>Invoice #</td>
                                   <%-- <td>&nbsp;</td>--%>
                                </tr>
                            </colgroup>
                        </table>
                    </div>
                    <div id="dv_grd" class="ScrollAutoReset" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden;HEIGHT: 460px ; text-align:center; border-bottom:none;">
                        <table border="1" bordercolor="#F0F5F5" cellpadding="4" cellspacing="0" style=" text-align: center; border-collapse:collapse; width:100%;">
                            <colgroup>
                              <col style="width:3%;"/>
                              <%--  <col style="width:200px;" />--%>
                                <col style="width:9%;" />
                                <col style="width:11%;" />
                                <col />
                                <col style="width:4%;"  />
                                <col style="width:10%;" />
                                <col style="width:8%;" />
                                <col style="width:8%;" />
                                <col style="width:8%;" />
                                <col style="width:8%;" />
                                <col style="width:4%;" />
                                <col style="width:14%;" />
                               <%-- <col style="width:25px;"/>--%>
                          
                          </colgroup>
                          
                           <asp:Repeater ID="RptMyInvoices" runat="server">
                                <ItemTemplate>
                                    <tr>
                                    <%--     <td>
                                            <asp:ImageButton ID="btnDel" runat="server" CommandArgument='<%#Eval("PaymentId")%>' VoucherType='<%#Eval("PTYPE").ToString()%>' ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="btnAskCancel_Click" ToolTip="Cancel Invoice"  Visible='<%#(Eval("Status").ToString()!="C") && (Common.ToDateString(Eval("BankConfirmedOn"))=="")%>' OnClientClick="return confirm('Are you sure to cancel this invoice?');" />
                                           <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%#Eval("PaymentId")%>' ImageUrl="~/Modules/HRD/Images/AddPencil.gif" OnClick="btnEdit_Click" ToolTip="Modify Voucher"  />
                                        </td>--%>
                                        <td>
                                            <asp:CheckBox ID="chkRFP" CssClass='<%#Eval("RFPId")%>'   runat="server" />
                                        </td>
                                      <%--  <td align="left"><%#Eval("OwnerName")%></td>--%>
                                        <td align="left"><%#Eval("VesselName")%></td>
                                        <td align="left">
                                         <%--   <asp:LinkButton ID="lbPVNo" runat="server" CommandArgument='<%#Eval("RFPId")%>' OnClick="lbPVNo_Click" >--%>
                                                <%#Eval("RFPNo")%> <asp:Label Id="Label1" runat="server" ForeColor="Red" Text="(AD)" Visible='<%#(Convert.ToBoolean(Eval("IsAdvPayment")) == true)%>'></asp:Label>
                                            <%--</asp:LinkButton>--%>
                                        </td>
                                        <td align="left"><%#Eval("SupplierName")%><span runat="server" id="c1" visible='<%#(Eval("Status").ToString()=="C")%>'  style="color:red"> - Cancelled </span>
                                        </td>
                                        <td align="left"><%#Eval("InvCurr")%></td>
                                        <td align="left"><%#Eval("InvAmount")%></td>
                                        <td align="left"><%#Eval("RFPSubmittedBy")%></td>
                                        <td align="left"><%#Common.ToDateString(Eval("RFPSubmittedOn"))%></td>
                                         <td align="left"><%#Eval("RFPApprovedBy")%></td>
                                        <td align="left"><%#Common.ToDateString(Eval("RFPApprovedOn"))%></td>
                                        <td align="left"><%#Eval("PaymentCurr")%>
                                            <asp:HiddenField ID="hfPaymentId" runat="server" Value='<%#Eval("RFPId")%>' />
                                        </td>
                                        <%--onclick="UpdateAmount(this);"--%>
                                        
                                       <td align="left"><%#Eval("InvoiceList")%></td>
                                       
                                       <%-- <td>&nbsp;</td>--%>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr style="background-color:#E6F3FC">
                                        <%-- <td>
                                            <asp:ImageButton ID="btnDel" runat="server" CommandArgument='<%#Eval("PaymentId")%>' VoucherType='<%#Eval("PTYPE").ToString()%>' ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="btnAskCancel_Click" ToolTip="Cancel Invoice"  Visible='<%#(Eval("Status").ToString()!="C") && (Common.ToDateString(Eval("BankConfirmedOn"))=="")%>' OnClientClick="return confirm('Are you sure to cancel this invoice?');" />
                                           <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%#Eval("PaymentId")%>' ImageUrl="~/Modules/HRD/Images/AddPencil.gif" OnClick="btnEdit_Click" ToolTip="Modify Voucher" />
                                        </td>--%>
                                       <td>
                                            <asp:CheckBox ID="chkRFP" CssClass='<%#Eval("RFPId")%>'   runat="server"  />
                                        </td>
                                       <%-- <td align="left"><%#Eval("OwnerName")%></td>--%>
                                         <td align="left"><%#Eval("VesselName")%></td>
                                        <td align="left">
                                         <%--   <asp:LinkButton ID="lbPVNo" runat="server" CommandArgument='<%#Eval("RFPId")%>' OnClick="lbPVNo_Click" >--%>
                                                <%#Eval("RFPNo")%> <asp:Label Id="Label1" runat="server" ForeColor="Red" Text="(AD)" Visible='<%#(Convert.ToBoolean(Eval("IsAdvPayment")) == true)%>'></asp:Label>
                                           <%-- </asp:LinkButton>--%>
                                        </td>
                                        <td align="left"><%#Eval("SupplierName")%><span runat="server" id="c1" visible='<%#(Eval("Status").ToString()=="C")%>' style="color:red"> - Cancelled </span>
                                         
                                        </td>
                                        <td align="left"><%#Eval("InvCurr")%></td>
                                        <td align="left"><%#Eval("InvAmount")%></td>
                                        <td align="left"><%#Eval("RFPSubmittedBy")%></td>
                                        <td align="left"><%#Common.ToDateString(Eval("RFPSubmittedOn"))%></td>
                                         <td align="left"><%#Eval("RFPApprovedBy")%></td>
                                        <td align="left"><%#Common.ToDateString(Eval("RFPApprovedOn"))%></td>
                                        <td align="left"><%#Eval("PaymentCurr")%>
                                           <%-- <%#((Eval("PaymentType").ToString()=="U")?"USD":((Eval("PaymentType").ToString()=="S")?"SGD":"INR"))%>--%>
                                              <asp:HiddenField ID="hfPaymentId" runat="server" Value='<%#Eval("RFPId")%>' />
                                        </td>
                                        <td align="left"><%#Eval("InvoiceList")%></td>
                                        <%--<td>&nbsp;</td>--%>
                                    </tr>
                                </AlternatingItemTemplate>
                            </asp:Repeater>
                        </table>
                        <asp:GridView  CellPadding="0" CellSpacing="0" ID="GridView1" runat="server"  AutoGenerateColumns="False"  Width="98%"  GridLines="horizontal" Visible="false" OnRowDataBound="GridView1_RowDataBound" >  <%--OnDataBound="SummaryBound"--%>
                                        <Columns>
                                             <asp:BoundField DataField="OwnerName" HeaderText="Owner" >
                                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                                            </asp:BoundField>
                                                <asp:BoundField DataField="VesselName" HeaderText="Vessel" >
                                                <ItemStyle HorizontalAlign="Left" Width="125px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="RFPNo" HeaderText="RFP #" >
                                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SupplierName" HeaderText="Vendor" >
                                                <ItemStyle HorizontalAlign="Left" Width="200px"   />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="InvCurr" HeaderText="Currency" >
                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="InvAmount" HeaderText="Amount" >
                                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="RFPSubmittedBy" HeaderText="Created By " >
                                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false" DataField="RFPSubmittedOn" HeaderText="Created On" >
                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="RFPApprovedBy" HeaderText="Approved By" >
                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false" DataField="RFPApprovedOn" HeaderText="Approved On" >
                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PaymentType" HeaderText="PayIn" >
                                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="PVNo" HeaderText="PV #" >
                                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="PaidName" HeaderText="Paid By" >
                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            </asp:BoundField>
                                             <asp:BoundField DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false" DataField="PaidOn" HeaderText="Paid On">
                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            </asp:BoundField>
                                               <asp:BoundField  DataField="InvoiceList" HeaderText="Invoices">
                                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                                            </asp:BoundField>
                                            

                                        </Columns>                        

                                        <SelectedRowStyle CssClass="selectedtowstyle" />
                                        <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                        <RowStyle CssClass="rowstyle" />
                                    </asp:GridView>
                    </div>
                    </div>
                     <div id="dvPaid" runat="server" visible="false"> 
                    <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden;HEIGHT: 30px ; text-align:center; border-bottom:none;">
                        <table border="1" bordercolor="white" cellpadding="4" cellspacing="0" rules="all" style="border-collapse:collapse;" class="auto-style1">
                            <colgroup>
                                <col style="width:9%;" />
                              <%--  <col style="width:150px;" />--%>
                                <col />
                                <col style="width:4%;" />
                                <col style="width:8%;" />
                                <col style="width:8%;" />
                                <col style="width:7%;" />
                                <col style="width:8%;" />
                                <col style="width:7%;" />
                                <col style="width:4%;" />
                                <col style="width:12%;" />
                                <col style="width:8%;" />
                                <col style="width:7%;" />
                                <col style="width:5%;" />
                                <col style="width:3%;"/>
                                <tr align="left" class= "headerstylegrid">
                                    <td>Vessel </td>
                                   <%-- <td>RFP #</td>--%>
                                    <td>Vendor</td>                                   
                                    <td>Curr.</td>
                                    <td>Amount </td>
                                    <td>Created By </td>
                                    <td>Created On </td>
                                    <td>Approved By </td>
                                    <td>Approved On </td>
                                    <td>PayIn</td>
                                    <td>PV #</td>
                                    <td>Paid By</td>
                                    <td>Paid On</td>
                                    <td>Invoices</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </colgroup>
                        </table>
                    </div>
                    <div id="dv_grd1" class="ScrollAutoReset" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden;HEIGHT: 460px ; text-align:center; border-bottom:none;">
                        <table border="1" bordercolor="#F0F5F5" cellpadding="4" cellspacing="0" style=" text-align: center; border-collapse:collapse; width:100%;">
                            <colgroup>
                               <col style="width:9%;" />
                              <%--  <col style="width:150px;" />--%>
                                <col />
                                <col style="width:4%;" />
                                <col style="width:8%;" />
                                <col style="width:8%;" />
                                <col style="width:7%;" />
                                <col style="width:8%;" />
                                <col style="width:7%;" />
                                <col style="width:4%;" />
                                <col style="width:12%;" />
                                <col style="width:8%;" />
                                <col style="width:7%;" />
                                <col style="width:5%;" />
                                <col style="width:3%;"/>
                           </colgroup>
                           <asp:Repeater ID="rptPaidStatus" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td align="left"><%#Eval("VesselName")%></td>
                                     <%--   <td align="left">
                                                <%#Eval("RFPNo")%>
                                        </td>--%>
                                        <td align="left"><%#Eval("SupplierName")%><span runat="server" id="c1" visible='<%#(Eval("Status").ToString()=="C")%>'  style="color:red"> - Cancelled </span>
                                        </td>
                                        <td align="left"><%#Eval("InvCurr")%></td>
                                        <td align="left"><%#Eval("ChequeTTAmount")%></td>
                                        <td align="left"><%#Eval("RFPSubmittedBy")%></td>
                                        <td align="left"><%#Common.ToDateString(Eval("RFPSubmittedOn"))%></td>
                                         <td align="left"><%#Eval("RFPApprovedBy")%></td>
                                        <td align="left"><%#Common.ToDateString(Eval("RFPApprovedOn"))%></td>
                                        <td align="left"><%#Eval("PaymentCurr")%>
                                           <%-- <%#((Eval("PaymentType").ToString()=="U")?"USD":((Eval("PaymentType").ToString()=="S")?"SGD":"INR"))%>--%>
                                            <asp:HiddenField ID="hfPaymentId" runat="server" Value='<%#Eval("RFPId")%>' />
                                        </td>
                                        <td align="left">
                                              <asp:LinkButton ID="lbPVNo" runat="server" CommandArgument='<%#Eval("PVNo")%>' OnClick="lbPVNo_Click" 
                                                  paymentid='<%#Eval("PaymentId")%>'>
                                               <%#Eval("PVNo")%>
                                            </asp:LinkButton>
                                            <asp:Label Id="Label1" runat="server" ForeColor="Red" Text="(AD)" Visible='<%#(Convert.ToBoolean(Eval("IsAdvPayment")) == true)%>'></asp:Label>
                                           
                                        </td>
                                         <td align="left"><%#Eval("PaidName")%>
                                        </td>
                                         <td align="left"><%#Common.ToDateString(Eval("PaidOn"))%>
                                        </td>
                                          <td align="left"> <asp:ImageButton ID="lblInvoiceList" runat="server" ImageUrl="~/Modules/HRD/Images/Tooltip.png" ToolTip='<%#Eval("InvoiceList")%>' /> 
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ibSendMail" runat="server" CommandArgument='<%#Eval("PVNo")%>' paymentid='<%#Eval("PaymentId")%>' ImageUrl="~/Modules/HRD/Images/mail.gif" OnClick="ibSendMail_Click" ToolTip="Send Payment detail to Vendor" />
                                        </td>
                                        <%--onclick="UpdateAmount(this);"--%>
                                      <%--  <td>&nbsp;</td>--%>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr style="background-color:#E6F3FC">
                                       <td align="left"><%#Eval("VesselName")%></td>
                                      <%--  <td align="left">
                                                <%#Eval("RFPNo")%>
                                        </td>--%>
                                        <td align="left"><%#Eval("SupplierName")%><span runat="server" id="c1" visible='<%#(Eval("Status").ToString()=="C")%>'  style="color:red"> - Cancelled </span>
                                        </td>
                                        <td align="left"><%#Eval("InvCurr")%></td>
                                        <td align="left"><%#Eval("InvAmount")%></td>
                                        <td align="left"><%#Eval("RFPSubmittedBy")%></td>
                                        <td align="left"><%#Common.ToDateString(Eval("RFPSubmittedOn"))%></td>
                                         <td align="left"><%#Eval("RFPApprovedBy")%></td>
                                        <td align="left"><%#Common.ToDateString(Eval("RFPApprovedOn"))%></td>
                                        <td align="left"><%#Eval("PaymentCurr")%>
                                            <%--<%#((Eval("PaymentType").ToString()=="U")?"USD":((Eval("PaymentType").ToString()=="S")?"SGD":"INR"))%>--%>
                                            <asp:HiddenField ID="hfPaymentId" runat="server" Value='<%#Eval("RFPId")%>' />
                                        </td>
                                        <td align="left">
                                            <asp:LinkButton ID="lbPVNo" runat="server" CommandArgument='<%#Eval("PVNo")%>' OnClick="lbPVNo_Click" paymentid='<%#Eval("PaymentId")%>' >
                                               <%#Eval("PVNo")%>
                                            </asp:LinkButton>
                                            <asp:Label Id="Label1" runat="server" ForeColor="Red" Text="(AD)" Visible='<%#(Convert.ToBoolean(Eval("IsAdvPayment")) == true)%>'></asp:Label>
                                        </td>
                                         <td align="left"><%#Eval("PaidName")%>
                                        </td>
                                         <td align="left"><%#Common.ToDateString(Eval("PaidOn"))%>
                                        </td>
                                          <td align="left"> <asp:ImageButton ID="lblInvoiceList" runat="server" ImageUrl="~/Modules/HRD/Images/Tooltip.png" ToolTip='<%#Eval("InvoiceList")%>' /> 
                                        </td>
                                          <td>
                                            <asp:ImageButton ID="ibSendMail" runat="server" CommandArgument='<%#Eval("PVNo")%>' paymentid='<%#Eval("PaymentId")%>' ImageUrl="~/Modules/HRD/Images/mail.gif" OnClick="ibSendMail_Click" ToolTip="Send Payment detail to Vendor" />
                                        </td>
                                       <%-- <td>&nbsp;</td>--%>
                                    </tr>
                                </AlternatingItemTemplate>
                            </asp:Repeater>
                              
                        </table>
                        
                    </div>
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
        </div>
   
    <!-----------------Add Payment Voucher ------------------------>
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;" id="dv_NewPV" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:1; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:90%; padding :5px; text-align :center;background : white; z-index:2;top:50px; border:solid 10px black;">
            <center >
             <div style="padding:6px;  font-size:14px; " class="text headerband"><b>Payment Details                   <div class="searchsection-textaligntd">
                          <div class="searchsection-child"> Owner : </div>
                            <asp:Label runat="server" ID="lblOwnerName" CssClass="inner-field" ></asp:Label>
                            <asp:Label runat="server" ID="lblOwnerCode" Visible="false"></asp:Label>
                            <asp:Label runat="server" ID="lblPaymentType" Visible="false"></asp:Label>
                      </div>
                      <div class="searchsection-textaligntd">
                           <div class="searchsection-child">PV No # : </div>
                           <asp:Label runat="server" ID="lblpvno" CssClass="inner-field"></asp:Label>
                      </div>
                 </div>
                 <div class="searchsectiontd"> 
                     <div class="searchsection-textaligntd">
                     <div class="searchsection-child">Vendor :</div>
                     <asp:Label ID="lblVendorName" runat="server" CssClass="inner-field" ></asp:Label>
                         </div>
                     <div class="searchsection-textaligntd">
                         <div class="searchsection-child">Currency :</div>
                         <asp:Label ID="lblCurrency" runat="server" CssClass="inner-field"></asp:Label>
                         </div>
                    
                  </div>
                 <div class="searchsectiontd">
                      <div class="searchsection-textaligntd">
                          <div class="searchsection-child">Payment Mode :</div>
                          <asp:Label ID="lblPaymentMode" runat="server" CssClass="inner-field"></asp:Label>
                          </div>
                     <div class="searchsection-textaligntd">
                         <div class="searchsection-child"> Bank Name :</div>
                         <asp:TextBox ID="txtBankName" runat="server" CssClass="inner-field" ></asp:TextBox>
                         </div>
                     </div>
                 <div class="searchsectiontd">
                     <div class="searchsection-textaligntd">
                         <div class="searchsection-child"> Credit Act # : </div>
                          <asp:TextBox ID="txtCreditActNo" runat="server" CssClass="inner-field"></asp:TextBox>
                         
                 </div>
                     <div class="searchsection-textaligntd">
                         <div class="searchsection-child"> Credit Act Name : </div>
                          <asp:TextBox ID="txtCreditActName" runat="server"  CssClass="inner-field" 
                                   ></asp:TextBox>&nbsp;
                     </div>
                 </div>
                 <div class="searchsectiontd">
                      <div class="searchsection-textaligntd">
                          <div class="searchsection-child"> Cheque/ TT # : </div>
                           <asp:TextBox ID="txtChequeTTNo" runat="server" CssClass="inner-field" 
                                  MaxLength="49" ></asp:TextBox>
                            <%--  &nbsp;<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtChequeTTNo" ErrorMessage="*" ></asp:RequiredFieldValidator> --%>
                          </div>
                     <div class="searchsection-textaligntd">
                           <div class="searchsection-child"> Cheque/ TT Amt.: </div>
                           <asp:TextBox ID="txtChequeTTAmt"  runat="server" CssClass="inner-field" MaxLength="12" ></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtChequeTTAmt" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                              &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtChequeTTAmt"
                                  ErrorMessage="Till 2 decimal places only." ValidationExpression="[-]?\b\d{1,13}\.?\d{0,2}" Display="Dynamic"></asp:RegularExpressionValidator>
                          </div>
                     </div>
                 <div class="searchsectiontd">
                     <div class="searchsection-textaligntd">
                          <div class="searchsection-child"> Cheque/ TT Date : </div>
                          <asp:TextBox ID="txtChequeTTDate" runat="server" CssClass="inner-field"   ></asp:TextBox>
                              &nbsp;<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtChequeTTDate" ErrorMessage="*" ></asp:RequiredFieldValidator>  
                         </div>
                      <div class="searchsection-textaligntd">
                          <div class="searchsection-child"> Bank Charges : </div>
                           <asp:TextBox ID="txtBankCharges"  runat="server" CssClass="inner-field" MaxLength="12" style="text-align:right"></asp:TextBox>
                              &nbsp;&nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtBankCharges"
                                  ErrorMessage="Till 2 decimal places only." ValidationExpression="[-]?\b\d{1,13}\.?\d{0,2}" Display="Dynamic"></asp:RegularExpressionValidator>
                         </div>
                     </div>
                  <div class="searchsectiontd">
                     
                         <div class="searchsection-child"> Remarks : </div>
                           <asp:TextBox ID="txtComments" runat="server" CssClass="inner-field" Height="40px" 
                                  TextMode="MultiLine" Width="80%"></asp:TextBox>
                        
                     </div>
          
              <div class="table-responsive">
                     <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden;HEIGHT: 30px ; text-align:center; border-bottom:none;">
                                           <table border="1" bordercolor="white" cellpadding="4" cellspacing="0" rules="all" style="width:100%; height:30px;  border-collapse:collapse;">
                                            <colgroup>
                                                <col style="width:12%;"/>
                                                <col style="width:6%;"/>
                                                <col style="width:12%;" />
                                                <col style="width:10%;text-align:center;" />
                                                <col style="width:10%;text-align:center;" />
                                                <col style="width:12%;text-align:center;" />
                                                <col style="width:12%;text-align:center;" />
                                                <col style="width:8%;text-align:center;" />
                                                <col style="width:8%;text-align:center;" />
                                                <col style="width:10%;text-align:center;" />
                                               <%--  <col style="width:10%;text-align:center;" />
                                               <col style="width:50px;text-align:center;" />
                                                <col style="width:2%;"/>--%>
                                                <tr class= "headerstylegrid" >
                                                    <td>RFP No</td>
                                                    <td>Ref #</td>
                                                    <td>Inv #</td>
                                                    <td>Inv Dt.</td>
                                                    <td>Inv Amt</td>
                                                    <td>Payable Amt</td>
                                                    <td>Pay Amt</td>
                                                    <td>TDS Amt</td>
                                                    <td>Account</td>
                                                   <%-- <td></td>
                                                    <td>&nbsp;</td>--%>
                                                </tr>
                                            </colgroup>
                                        </table>
                                        </div>
                    <div id="divPayment" runat="server" class="scrollbox" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; HEIGHT: 100px; text-align:center;">
                                            <table border="1" bordercolor="#F0F5F5" cellpadding="4" cellspacing="0" style=" text-align: center; border-collapse:collapse; width:100%;">
                                                <colgroup>
                                                <col style="width:12%;"/>
                                                <col style="width:6%;"/>
                                                <col style="width:12%;" />
                                                <col style="width:10%;text-align:center;" />
                                                <col style="width:10%;text-align:center;" />
                                                <col style="width:12%;text-align:center;" />
                                                <col style="width:12%;text-align:center;" />
                                                <col style="width:8%;text-align:center;" />
                                                <col style="width:10%;text-align:center;" />
                                              <%--  <col style="width:10%;text-align:center;" />
                                                <col style="width:50px;text-align:center;" />--%>
                                              <%--  <col style="width:2%;"/>--%>
                                                </colgroup>
                                                <asp:Repeater ID="RptInvoices" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td align="left">
                                                                <%#Eval("RFPNo")%>
                                                            </td>
                                                            <td align="left"><a href="ViewInvoice.aspx?InvoiceId=<%#Eval("InvoiceId")%>" target="_blank" > <%#Eval("RefNo")%></a>
                                                                <asp:HiddenField ID="hfdInvId" runat="server" Value='<%#Eval("InvoiceId")%>' />
                                                                  <asp:HiddenField ID="hfdRFPId" runat="server" Value='<%#Eval("RFPId")%>' />
                                                                 <asp:HiddenField ID="hdnSupplierId" runat="server" Value='<%#Eval("SupplierId")%>' />
                                                                <asp:HiddenField ID="hdnAdvPayment" runat="server" Value='<%#Eval("IsAdvPayment")%>' />
                                                                <asp:HiddenField ID="hdnNonPo" runat="server" Value='<%#Eval("IsNonPo")%>' />
                                                            </td>                                                                                                                       
                                                            <td align="left"><%#Eval("InvNo")%></td>
                                                            <td align="left"><%#Common.ToDateString(Eval("InvDate"))%></td>
                                                            <td align="right"><%#Eval("InvoiceAmount")%></td>
                                                            <td align="right"><%#Eval("ApprovalAmount")%>
                                                                <asp:HiddenField ID="hdnApprovlAmt" runat="server" Value='<%#Eval("ApprovalAmount")%>' />
                                                            </td>
                                                             <td align="right">
                                                                <asp:TextBox ID="txtPayAmt" runat="server" Width="75px" MaxLength="10" TextMode="Number" AutoPostBack="true" OnTextChanged="txtPayAmt_TextChanged" Text='<%#Common.CastAsDecimal(Eval("PayAmount"))%>' Enabled='<%#Eval("Status").ToString()=="UnPaid"%>'></asp:TextBox>
                                                              
                                                            </td>
                                                            <td align="right">
                                                                <asp:TextBox ID="txtTDSAmount" runat="server" Width="75px" MaxLength="10" TextMode="Number" AutoPostBack="true" OnTextChanged="txtTDSAmount_TextChanged" Text='<%#Common.CastAsDecimal(Eval("TDSAmount"))%>' Enabled='<%#Eval("Status").ToString()=="UnPaid"%>'  ></asp:TextBox>
                                                            </td>
                                                            <td align="left"><%#Eval("AccountInfo")%>
                                                           <%-- <td align="left">
                                                                 <asp:LinkButton ID="lbApEntries" runat="server" CommandArgument='<%#Eval("NonPoId")%>' Visible='<%#(Common.CastAsInt32(Eval("AllowforNonpo"))==1)%>'  OnClick="lbApEntries_Click" Text="Update Account Code" CausesValidation="false"></asp:LinkButton>
                                                                <asp:HiddenField ID="hdnAllowforNonpo" runat="server" Value='<%#Eval("AllowforNonpo")%>' />
                                                            </td>--%>
                                                             <%--<td>
                                                                  <asp:ImageButton runat="server" CommandArgument='<%#Eval("InvoiceId")%>' Visible='<%#(lblpvno.Text.Trim()=="")%>' OnClientClick="return window.confirm('Are you sure to delete this record?');" ID="btnDelRow" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="btnDeleteRow_Click" CausesValidation="false" />
                                                            </td> --%>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                       <tr style="background-color:#E6F3FC">
                                                            <td align="left">
                                                                <%#Eval("RFPNo")%>
                                                            </td>
                                                            <td align="left"><a href="ViewInvoice.aspx?InvoiceId=<%#Eval("InvoiceId")%>" target="_blank" > <%#Eval("RefNo")%></a>
                                                                <asp:HiddenField ID="hfdInvId" runat="server" Value='<%#Eval("InvoiceId")%>' />
                                                                <asp:HiddenField ID="hfdRFPId" runat="server" Value='<%#Eval("RFPId")%>' />
                                                                <asp:HiddenField ID="hdnSupplierId" runat="server" Value='<%#Eval("SupplierId")%>' />
                                                                <asp:HiddenField ID="hdnAdvPayment" runat="server" Value='<%#Eval("IsAdvPayment")%>' />
                                                                <asp:HiddenField ID="hdnNonPo" runat="server" Value='<%#Eval("IsNonPo")%>' />
                                                            </td>                                                                                                                       
                                                            <td align="left"><%#Eval("InvNo")%></td>
                                                            <td align="left"><%#Common.ToDateString(Eval("InvDate"))%>
                                                                
                                                            </td>
                                                            <td align="right"><%#Eval("InvoiceAmount")%></td>
                                                            <td align="right"><%#Eval("ApprovalAmount")%>
                                                                <asp:HiddenField ID="hdnApprovlAmt" runat="server" Value='<%#Eval("ApprovalAmount")%>' />
                                                            </td>
                                                            <td align="right">
                                                                <asp:TextBox ID="txtPayAmt" runat="server" Width="75px" MaxLength="10" TextMode="Number" AutoPostBack="true" OnTextChanged="txtPayAmt_TextChanged" Text='<%#Common.CastAsDecimal(Eval("PayAmount"))%>' ></asp:TextBox>
                                                              
                                                            </td>
                                                            <td align="right">
                                                                <asp:TextBox ID="txtTDSAmount" runat="server" Width="75px" MaxLength="10" TextMode="Number" AutoPostBack="true" OnTextChanged="txtTDSAmount_TextChanged" Text='<%#Common.CastAsDecimal(Eval("TDSAmount"))%>' Enabled='<%#Eval("Status").ToString()=="UnPaid"%>'></asp:TextBox>
                                                              
                                                            </td>
                                                            <td align="left"><%#Eval("AccountInfo")%>
                                                          <%--   <td align="left">
                                                                 <asp:LinkButton ID="lbApEntries" runat="server" CommandArgument='<%#Eval("NonPoId")%>' Visible='<%#(Common.CastAsInt32(Eval("AllowforNonpo"))==1) %>' OnClick="lbApEntries_Click" Text="Update Account Code" CausesValidation="false" ></asp:LinkButton>
                                                                 <asp:HiddenField ID="hdnAllowforNonpo" runat="server" Value='<%#Eval("AllowforNonpo")%>' />
                                                            </td>
                                                           <td>
                                                                  <asp:ImageButton runat="server" CommandArgument='<%#Eval("InvoiceId")%>' Visible='<%#(lblpvno.Text.Trim()=="")%>' OnClientClick="return window.confirm('Are you sure to delete this record?');" ID="btnDelRow" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="btnDeleteRow_Click" CausesValidation="false" />
                                                            </td> --%>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </AlternatingItemTemplate>
                                                </asp:Repeater>
                                             </table>
                                        </div>
                                        <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden;HEIGHT: 30px ; text-align:center; border-bottom:none;">
                                           <table border="1" bordercolor="white" cellpadding="4" cellspacing="0" rules="all" style="width:100%; height:30px;  border-collapse:collapse;">
                                            <colgroup>
                                               <col style="width:12%;"/>
                                                <col style="width:6%;"/>
                                                <col style="width:12%;" />
                                                <col style="width:10%;text-align:center;" />
                                                <col style="width:10%;text-align:center;" />
                                                <col style="width:12%;text-align:center;" />
                                                <col style="width:12%;text-align:center;" />
                                                <col style="width:8%;text-align:center;" />
                                                <col style="width:10%;text-align:center;" />
                                             <%--   <col style="width:10%;text-align:center;" />
                                                <col style="width:50px;text-align:center;" />--%>
                                                <col style="width:2%;"/>
                                                <tr class= "headerstylegrid" >
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td>Total:   </td>                             
                                                    <td><asp:Label ID="lbltotal" runat="server"></asp:Label></td>
                                                    <td><asp:Label ID="lblpayAmt" runat="server"></asp:Label></td>
                                                     <td>&nbsp;</td>
                                                   <%-- <td>&nbsp;</td>
                                                    <td></td>--%>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </colgroup>
                                        </table>
                                        </div>
                                           
                         </div>  
                 <br />
                 <div class="searchsection-btn">
                      <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                          PopupButtonID="imgduedate" PopupPosition="TopRight" TargetControlID="txtChequeTTDate">
                      </asp:CalendarExtender>
                      <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                          FilterType="Numbers,Custom" TargetControlID="txtChequeTTAmt" ValidChars=".,-"></asp:FilteredTextBoxExtender>
                      <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                          FilterType="Numbers,Custom" TargetControlID="txtBankCharges" ValidChars=".">
                      </asp:FilteredTextBoxExtender>
                      <asp:Button ID="btn_Payment" runat="server" Text="Create PV" Width="80px" OnClick="btn_Payment_Click" style=" border:none; padding:4px;" CssClass="btn" /> &nbsp;
                      <asp:Button ID="btnClose1" runat="server" Text="Close" Width="80px"  style="  border:none; padding:4px;" CssClass="btn" OnClick="btnClose1_Click" CausesValidation="false" /> &nbsp;
                      <asp:Button ID="btnPrint" runat="server" Text="Print" Width="80px"  style="  border:none; padding:4px;" CssClass="btn"  CausesValidation="false" OnClick="btnPrint_Click" Visible="false" />  &nbsp;
                      <asp:Button ID="btnAddUpdatePaymentDoc" runat="server" Text="Add Document" Width="150px"  style="  border:none; padding:4px;" CssClass="btn"  CausesValidation="false"  Visible="false" OnClick="btnAddUpdatePaymentDoc_Click" />  &nbsp;
                 </div>
              

                <div>
                <asp:Label runat="server" ID="lblMsgPOP" Font-Bold="true" ForeColor="Red"></asp:Label>
                </div>
               
           
            </center>
        </div>
    </center>
    </div>
    <!-----------------Add Document Voucher ------------------------>
        <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;font-family:Arial;font-size:12px;" runat="server" id="divPaymentAttachment" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px;width:100%; height:100%; background-color :black;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position :relative; width:500px; padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px;  ;opacity:1;filter:alpha(opacity=100)">
        <center>
        <br />
        <div class="text headerband"> <b>Add Documents</b> 
             <asp:ImageButton ID="imgClosePopup" runat="server" ImageUrl="~/Modules/HRD/Images/Close.gif"  title="Close this Window."  style="float:right;"  CssClass="btn" OnClick="imgClosePopup_Click" />
        </div>
        <br />
           <div>
               <table width="100%">
                   <tr>
                       <td style="width:25%;text-align:right;padding-right:5px;"> Other Documents : </td>
                       <td style="width:50%;text-align:left;padding-left:5px;"> <asp:FileUpload ID="fu" runat="server" AllowMultiple="true" /></td>
                       <td style="width:25%;text-align:right;padding-right:5px;"><asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn" OnClick="btnSave_Click" /></td>
                   </tr>
                   <tr>
                       <td colspan="3">
                           <asp:Label ID="lblMsgPaymnetDoc" runat="server" ForeColor="Red" Text="" ></asp:Label>
                       </td>
                   </tr>
               </table>
           </div>
            <br />
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
                <asp:Repeater ID="rptPaymentDocuments" runat="server">
                <ItemTemplate>
                <tr>
                <td style="text-align:center;">
                <asp:ImageButton ID="ImgDelete" runat="server" ImageUrl="~/Modules/HRD/Images/delete_12.gif" OnClick="ImgDelete_Click" CommandArgument='<%#Eval("ID")%>'  />
                </td>
                <td style="text-align:left;padding-left:5px;"><%#Eval("DocName")%>
                <asp:HiddenField ID="hdnDocId" runat="server" Value='<%#Eval("ID")%> ' />
                </td>
                <td style="text-align:center;"> 
                <%--   <asp:ImageButton ID="ImgAttachment" runat="server" ImageUrl="~/Images/paperclip.gif" OnClick="ImgAttachment_Click" CommandName='<%#Eval("DocId")%> ' />--%>

                <a onclick='OpenDocument(<%#Eval("ID")%>,"<%#Eval("PVNo")%>")' style="cursor:pointer;">
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
        <%--<div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server"  id="dvNonPoEntry" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position :relative; width:850px;padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:25px; opacity:1;filter:alpha(opacity=100)">
                <center>
                    <div style=" float :right " >
                        <asp:ImageButton runat="server" ID="btnNonPo_ClosePopup" ImageUrl="~/Modules/HRD/Images/close.gif"  ToolTip="Close this Window." OnClick="btnNonPo_ClosePopup_Click" /> </div>
                    <div style="font-size:15px;padding:6px;font-weight:bold;" class="text headerband">
                        ADD AP Entries for Non-PO Invoice Approval [ ID : <asp:Label ID="lblNonPoId" runat="server"></asp:Label>]
                       
                    </div>  
                    <table border="0" cellpadding="6" cellspacing="0" style="height: 250px; text-align: left; border-collapse:collapse; width:100%;">
                                <tr>
                                    <td style="width:15%;">
                                        <b> Ref No # : </b> 
                                    </td>
                                    <td style="width:35%;">
                                        <asp:Label ID="lblInvRefNo" runat="server" ></asp:Label> 
                                    </td>
                                    <td style="width:15%;">
                                        <b> Invoice No : </b>
                                    </td>
                                    <td style="width:35%;">
                                         <asp:Label ID="lblInvoiceNo" runat="server" ></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                   <td style="width:15%;">
                                      <b> Supperlier :  </b>
                                    </td>
                                    <td style="width:35%;">
                                        <asp:Label ID="lblSupplierName" runat="server" ></asp:Label> 
                                    </td>
                                    <td style="width:15%;">
                                       <b> Vessel Name :  </b>
                                    </td>
                                    <td style="width:35%;">
                                         <asp:Label ID="lblVesselName" runat="server" ></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                   <td style="width:15%;">
                                      <b> Inv Amount :  </b>
                                    </td>
                                    <td style="width:35%;">
                                        <asp:Label ID="lblInvAmount" runat="server" ></asp:Label> 
                                    </td>
                                    <td style="width:15%;">
                                       <b> Inv Currency :  </b>
                                    </td>
                                    <td style="width:35%;">
                                         <asp:Label ID="lblInvCurrency" runat="server" ></asp:Label>
                                    </td>
                                </tr>
                         <tr>
                                   <td style="width:15%;">
                                      <b> Department :  </b>
                                    </td>
                                    <td style="width:35%;" colspan="3">
                                        <asp:DropDownList ID="ddldepartment" runat="server"  Width="174px" 
                                        onselectedindexchanged="ddldepartment_SelectedIndexChanged" AutoPostBack="true" >
                                    </asp:DropDownList> 
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  InitialValue="0"
                                  ControlToValidate="ddldepartment" Display="Dynamic" ErrorMessage="*" ValidationGroup="RFPA"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                 <tr>
                                   <td style="width:15%;">
                                      <b> Account :  </b>
                                    </td>
                                    <td style="width:35%;" colspan="3">
                                        <asp:DropDownList ID="ddlAccount" runat="server" width="300px" ></asp:DropDownList> 
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"  InitialValue="0"
                                  ControlToValidate="ddlAccount" Display="Dynamic" ErrorMessage="*" ValidationGroup="RFPA"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width:15%;">
                                      <b> Remarks :  </b>
                                    </td>
                                     <td style="width:35%;" colspan="3">
                                        <asp:TextBox ID="txtNonPoRemarks" runat="server"  Width="90%" TextMode="MultiLine" Height="120px" MaxLength="500"></asp:TextBox>
                                               &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                  ControlToValidate="txtNonPoRemarks" Display="Dynamic" ErrorMessage="*" ValidationGroup="RFPA"></asp:RequiredFieldValidator>
                                         <asp:HiddenField ID="hdnNonPoId" runat="server" Value="0" />
                                         <asp:HiddenField ID="hdnInvoiceId" runat="server" Value="0" />
                                    </td>
                                </tr>
                                  
                                  </table>    
                    
                    

                    <div style="text-align:center;padding:5px;">
                        <asp:Button ID="btnSaveApEntries" runat="server" Text="Save" Width="80px" ValidationGroup="RFPA" style="  border:none; padding:4px;" CssClass="btn" OnClick="btnSaveApEntries_Click" /> &nbsp;
                        <asp:Button ID="btnCloseApEntries" runat="server" Text="Close" CssClass="btn" Width="80px" style="  border:none; padding:4px;" ToolTip="Close this Window." OnClick="btnCloseApEntries_Click" />
                    </div>
                    <div style="text-align:center;padding:5px;background-color:#e5e5e5">
                        <asp:Label ID="lblMsgApEntries" runat="server" CssClass="error"></asp:Label>
                    </div>
                    </center>    
                    </div>
                    </center>
                        </div>--%>
   </div>
    <asp:Label runat="server" ID="lblMsgMain" Font-Bold="true"></asp:Label>
    <br />

        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    <asp:HiddenField ID="hfInvId" runat="server"  />
    <%--<asp:Button ID="btndownload" Text=""  OnClick="btnDownloadFile_Click" style="display:none;" runat="server" /> --%>
    
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
        function autosum() {
            var tot = 0;
            $(".amtcalc").each(function (i, a) {
                var amt = parseFloat($(a).val());
                if (!isNaN(amt))
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
