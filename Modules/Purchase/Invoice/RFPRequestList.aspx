<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RFPRequestList.aspx.cs" Inherits="Modules_Purchase_Invoice_InvoiceRFPList" %>

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

    .searchsection-textalign{
        text-align:left;
    }

    .searchsection-btn {
       display:flex;
       justify-content:end;
       padding:5px;
    }

@media screen and (max-width:767px) {
    .searchsection {
        display:flex;
        flex-direction:column;
        padding:5px;
    }

    .inner-field {
        width: 100% !important;
        margin-bottom:7px;
    }

    .div_dialog {
        width: 90% !important;
    }
}

    </style>

    
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
        <div style="background-color:#E6F3FC;">
            <div style=" padding:6px; text-align:center;" class="text headerband">
                 <strong>RFP Approval List</strong>
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
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="inner-field">
                                     <asp:ListItem Text=" < RFP Status > " Value=""></asp:ListItem>
                                     <asp:ListItem Text="RFP for Approval" Value="1" Selected="True"></asp:ListItem>
                                     <asp:ListItem Text="RFP Approved" Value="2" ></asp:ListItem>
                                </asp:DropDownList>
                </div>
                <div class="searchsection-textalign">
                    Vendor :
                     <asp:TextBox ID="txtF_Vendor" runat="server" style="border:solid 1px #008AE6;" CssClass="inner-field"></asp:TextBox>
                </div>
                
            </div>
            <div class="searchsection">
                <div class="searchsection-textalign">
                    Owner :
                     <asp:DropDownList ID="ddlF_Owner" runat="server" CssClass="inner-field">
                                </asp:DropDownList>
                </div>
                <div class="searchsection-textalign">
                    &nbsp;Inv # :
                     <asp:TextBox ID="txtF_InvNo" runat="server" style="border:solid 1px #008AE6;" CssClass="inner-field"></asp:TextBox>
                </div>
                <div class="searchsection-textalign">
                    RFP # :
                    <asp:TextBox ID="txtF_PVNo" runat="server" style="border:solid 1px #008AE6;" CssClass="inner-field"></asp:TextBox>
                </div>
            </div>
            <div class="searchsection-btn">
                 <asp:Button ID="btn_Search" runat="server" OnClick="btn_Search_Click" OnClientClick="this.value='Loading..';" style="  border:none; padding:4px;margin-right:5px;" Text="Search" Width="100px" CssClass="btn"  /> 
                               
                                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" ToolTip="Clear" CssClass="btn" /> 
                                <asp:HiddenField ID="hfdSupplier" runat="server" />
            </div>

        </div>
        <div class="table-responsive">
            
                <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden;HEIGHT: 30px ; text-align:center; border-bottom:none;">
                    <table border="1" bordercolor="white" cellpadding="4" cellspacing="0" rules="all" style="width:100%; height:30px;  border-collapse:collapse;">
                        <colgroup>
                            <col style="width:3%;"/>
                            <col style="width:20%;" />
                            <col style="width:11%;" />
                            <col style="width:24%;"/>
                            <col style="width:6%;" />
                            <col style="width:10%;" />
                            <col style="width:12%;" />
                            <col style="width:9%;" />
                            <col style="width:4%;" />
                            <col style="width:1%;"/>
                            <tr align="left" class= "headerstylegrid">
                                <td></td>
                                <td>Owner </td>
                                <td>RFP #</td>
                                <td>Vendor</td>
                                <td>Curr.</td>
                                <td>Amount </td>
                                <td>Created By </td>
                                <td>Created On </td>
                                <td>PayIn</td>
                                <td>&nbsp;</td>
                            </tr>
                        </colgroup>
                    </table>
                </div>
                <div id="dv_grd" class="ScrollAutoReset" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden;HEIGHT: 460px ; text-align:center; border-bottom:none;">
                    <table border="1" bordercolor="#F0F5F5" cellpadding="4" cellspacing="0" style=" text-align: center; border-collapse:collapse; width:100%;">
                        <colgroup>
                            <col style="width:3%;"/>
                            <col style="width:20%;" />
                            <col style="width:11%;" />
                            <col style="width:24%;"/>
                            <col style="width:6%;" />
                            <col style="width:10%;" />
                            <col style="width:12%;" />
                            <col style="width:9%;" />
                            <col style="width:4%;" />
                            <col style="width:1%;"/>
                        <asp:Repeater ID="RptMyInvoices" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btnDel" runat="server" CommandArgument='<%#Eval("RFPId")%>' VoucherType='<%#Eval("PTYPE").ToString()%>' ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="btnAskCancel_Click" ToolTip="Cancel RFP"  Visible='<%#(Convert.ToBoolean(Eval("AllowDel")))%>' OnClientClick="return confirm('Are you sure to cancel this RFP ?');" />
                                        <%--<asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%#Eval("PaymentId")%>' ImageUrl="~/Modules/HRD/Images/AddPencil.gif" OnClick="btnEdit_Click" ToolTip="Modify Voucher"  />--%>
                                    </td>
                                    <td align="left"><%#Eval("OwnerName")%></td>
                                    <td align="left">
                                        <asp:LinkButton ID="lbPVNo" runat="server" OnClick="lbPVNo_Click" CommandArgument='<%#Eval("RFPId")%>'> <%#Eval("RFPNo")%> </asp:LinkButton>
                                        <%--  <a runat="server" href='<%#"RFPApproval.aspx?key=" + Eval("PaymentId").ToString()%>' target="_blank" ><%#Eval("PVNo")%></a>--%>
                                    </td>
                                    <td align="left"><%#Eval("SupplierName")%><span runat="server" id="c1" visible='<%#(Eval("Status").ToString()=="C")%>'  style="color:red"> - Cancelled </span>  
                                    </td>
                                    <td align="left"><%#Eval("InvoiceCurr")%></td>
                                    <td align="left"><%#Eval("InvoiceAmount")%></td>
                                    <td align="left"><%#Eval("RFPSubmittedBy")%></td>
                                    <td align="left"><%#Common.ToDateString(Eval("RFPSubmittedOn"))%></td>
                                    <td align="left"><%#Eval("Paymentcurr")%>
                                      <%--  <%#((Eval("PaymentType").ToString()=="U")?"USD":((Eval("PaymentType").ToString()=="S")?"SGD":"INR"))%>--%>
                                        <asp:HiddenField ID="hfPaymentId" runat="server" Value='<%#Eval("RFPId")%>' />
                                    </td>
                                    <%--onclick="UpdateAmount(this);"--%>
                                    <td>&nbsp;</td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr style="background-color:#E6F3FC">
                                    <td>
                                        <asp:ImageButton ID="btnDel" runat="server" CommandArgument='<%#Eval("RFPId")%>' VoucherType='<%#Eval("PTYPE").ToString()%>' ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="btnAskCancel_Click" ToolTip="Cancel RFP"  Visible='<%#(Convert.ToBoolean(Eval("AllowDel")))%>' OnClientClick="return confirm('Are you sure to cancel this RFP ?');" />
                                        <%--  <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%#Eval("PaymentId")%>' ImageUrl="~/Modules/HRD/Images/AddPencil.gif" OnClick="btnEdit_Click" ToolTip="Modify Voucher" />--%>
                                    </td>
                                    <td align="left"><%#Eval("OwnerName")%></td>
                                    <td align="left">
                                            <asp:LinkButton ID="lbPVNo" runat="server" OnClick="lbPVNo_Click" CommandArgument='<%#Eval("RFPId")%>'> <%#Eval("RFPNo")%> </asp:LinkButton>
                                        <%-- <a runat="server" href='<%#"RFPApproval.aspx?key=" + Eval("PaymentId").ToString()%>' target="_blank" ><%#Eval("PVNo")%></a>--%>
                                          
                                    </td>
                                    <td align="left"><%#Eval("SupplierName")%><span runat="server" id="c1" visible='<%#(Eval("Status").ToString()=="C")%>' style="color:red"> - Cancelled </span>
                                    </td>
                                    <td align="left"><%#Eval("InvoiceCurr")%></td>
                                    <td align="left"><%#Eval("InvoiceAmount")%></td>
                                    <td align="left"><%#Eval("RFPSubmittedBy")%></td>
                                    <td align="left"><%#Common.ToDateString(Eval("RFPSubmittedOn"))%></td>
                                    <td align="left"><%#Eval("Paymentcurr")%>
                                      <%--  <%#((Eval("PaymentType").ToString()=="U")?"USD":((Eval("PaymentType").ToString()=="S")?"SGD":"INR"))%>--%>
                                            <asp:HiddenField ID="hfPaymentId" runat="server" Value='<%#Eval("RFPId")%>' />
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:Repeater>
                            
                            </colgroup>
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
                
        </div>
       <!-----------------Add Payment Voucher ------------------------>
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;" id="dv_NewPV" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:1; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:90%; padding :5px; text-align :center;background : white; z-index:2;top:50px; border:solid 10px black;" >
            <center >
             <div style="padding:6px;  font-size:14px; " class="text headerband"><b>RFP Approval</b></div>
             <div style="width:100%; text-align:left; overflow-y:scroll; overflow-x:hidden;">
           <table cellpadding="6" cellspacing="0" width="100%">
        
          
         <tr>
           <td>
              <table border="0" bordercolor="#F0F5F5" cellpadding="6" cellspacing="0" style="height: 100px; text-align: center; border-collapse:collapse; width:100%;">
                    <tr>
                      <td align="right" style="text-align: right; padding-right:15px; width: 123px;">
                               Owner :</td>
                           <td style="text-align: left; ">
                                <asp:Label runat="server" ID="lblOwnerName" ></asp:Label>
                               <asp:Label runat="server" ID="lblOwnerCode" Visible="false"></asp:Label>
                           </td>
                           <td style="text-align: right;">
                               RFP #:</td>
                           <td style="text-align: left;width:500px;">
                               <asp:Label runat="server" ID="lblpvno"></asp:Label></td>
                      </tr>
                      <tr style="background-color:#E6F3FC">
                          <td align="right" style="text-align: right; padding-right:15px; width: 123px;">
                              Vendor:</td>
                          <td style="text-align: left; ">
                              <asp:Label ID="lblVendorName" runat="server" CssClass="input_box" ></asp:Label>
                              <asp:HiddenField runat="server" id="HiddenField1" />
                          </td>
                          <td style="text-align: right;">
                              Currency:</td>
                          <td style="text-align: left;width:500px;">
                              <asp:Label ID="lblCurrency" runat="server" CssClass="input_box"></asp:Label>
                          </td>
                      </tr>
                   <tr>
                           <td align="right" style="text-align: right; padding-right:15px; width: 123px;">
                               Payment Mode:</td>
                           <td style="text-align: left; ">
                              <asp:Label ID="lblPaymentMode" runat="server"></asp:Label>
                           </td>
                           <td style="text-align: right;">
                               &nbsp;</td>
                           <td style="text-align: left;width:500px;">
                               &nbsp;</td>
                      </tr>
                  <tr style="background-color:#E6F3FC">
                      <td align="right" style="text-align: right; padding-right:15px; width: 123px;">
                          Submitted By:
                      </td>
                      <td style="text-align: left; ">
                          <asp:Label ID="lblApprovalSubmittedBy" runat="server"></asp:Label>
                         
                      </td>
                      <td style="text-align: right;">
                          Submitted On:
                      </td>
                      <td style="text-align: left;width:500px;">
                           <asp:Label ID="lblApprovalSubmittedOn" runat="server"></asp:Label>
                      </td>
                  </tr>
                       
                       <tr >
                          <td align="right" style="text-align: right; padding-right:15px; width: 123px;">
                              Approval Comments :</td>
                          <td style="text-align: left;" colspan="3">
                              <asp:Label ID="lblApprovalSubmittedComments" runat="server"  Height="50px" 
                                   Width="80%"></asp:Label>
                             
                              </td>
                           
                      </tr>
                  
                     </table>
                     
                     <div class="table-responsive" style="HEIGHT: 30px ; text-align:center; border-bottom:none;">
                                           <table border="1" bordercolor="white" cellpadding="4" cellspacing="0" rules="all" style="width:100%; height:30px;  border-collapse:collapse;">
                                            <colgroup>
                                                <col style="width:25px;"/>
                                                <col style=""/>
                                                <col style="width:200px;" />
                                                <col style="width:200px;text-align:center;" />
                                                <col style="width:150px;text-align:center;" />
                                                <col style="width:150px;text-align:center;" />
                                                <%--<col style="width:50px;text-align:center;" />--%>
                                                <col style="width:25px;"/>
                                                <tr class= "headerstylegrid" >
                                                    <td></td>
                                                    <td>Ref #</td>
                                                    <td>Inv #</td>
                                                    <td>Inv Dt.</td>
                                                    <td>Inv Amt</td>
                                                    <td>Approval Amt</td>
                                                    <%--<td></td>--%>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </colgroup>
                                        </table>
                                        </div>
                    <div id="divPayment" runat="server" class="table-responsive" style="HEIGHT: 150px; text-align:center;">
                                            <table border="1" bordercolor="#F0F5F5" cellpadding="4" cellspacing="0" style=" text-align: center; border-collapse:collapse; width:100%;">
                                                <colgroup>
                                                <col style="width:25px;"/>
                                                <col style=""/>
                                                <col style="width:200px;" />
                                                <col style="width:200px;text-align:center;" />
                                                <col style="width:150px;text-align:center;" />
                                                <col style="width:150px;text-align:center;" />
                                                <%--<col style="width:50px;text-align:center;" />--%>
                                                <col style="width:25px;"/>
                                                </colgroup>
                                                <asp:Repeater ID="RptInvoices" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td>
                                                            </td>
                                                            <td align="left"><a href="ViewInvoice.aspx?InvoiceId=<%#Eval("InvoiceId")%>" target="_blank" > <%#Eval("RefNo")%></a>
                                                                <asp:HiddenField ID="hfdInvId" runat="server" Value='<%#Eval("InvoiceId")%>' />
                                                            </td>                                                                                                                       
                                                            <td align="left"><%#Eval("InvNo")%></td>
                                                            <td align="left"><%#Common.ToDateString(Eval("InvDate"))%></td>
                                                            <td align="right"><%#Eval("InvoiceAmount")%></td>
                                                            <td align="right"><%#Eval("ApprovalAmount")%></td>
                                                            <%-- <td>
                                                                  <asp:ImageButton runat="server" CommandArgument='<%#Eval("InvoiceId")%>' Visible='<%#(lblpvno.Text.Trim()=="")%>' OnClientClick="return window.confirm('Are you sure to delete this record?');" ID="btnDelRow" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="btnDeleteRow_Click" CausesValidation="false" />
                                                            </td> --%>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                       <tr style="background-color:#E6F3FC">
                                                            <td></td>
                                                            <td align="left"><a href="ViewInvoice.aspx?InvoiceId=<%#Eval("InvoiceId")%>" target="_blank" > <%#Eval("RefNo")%></a>
                                                                <asp:HiddenField ID="hfdInvId" runat="server" Value='<%#Eval("InvoiceId")%>' />
                                                            </td>                                                                                                                       
                                                            <td align="left"><%#Eval("InvNo")%></td>
                                                            <td align="left"><%#Common.ToDateString(Eval("InvDate"))%></td>
                                                            <td align="right"><%#Eval("InvoiceAmount")%></td>
                                                            <td align="right"><%#Eval("ApprovalAmount")%></td>
                                                            <%--<td>
                                                                  <asp:ImageButton runat="server" CommandArgument='<%#Eval("InvoiceId")%>' Visible='<%#(lblpvno.Text.Trim()=="")%>' OnClientClick="return window.confirm('Are you sure to delete this record?');" ID="btnDelRow" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="btnDeleteRow_Click" CausesValidation="false" />
                                                            </td> --%>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </AlternatingItemTemplate>
                                                </asp:Repeater>
                                             </table>
                                        </div>

                                        <div class="table-responsive" style="HEIGHT: 30px ; text-align:center; border-bottom:none;">
                                           <table border="1" bordercolor="white" cellpadding="4" cellspacing="0" rules="all" style="width:100%; height:30px;  border-collapse:collapse;">
                                            <colgroup>
                                                <col style="width:25px;"/>
                                                <col style=""/>
                                                <col style="width:200px;" />
                                                <col style="width:200px;text-align:center;" />
                                                <col style="width:150px;text-align:center;" />
                                                <col style="width:150px;text-align:center;" />
                                               <%-- <col style="width:50px;text-align:center;" />--%>
                                                <col style="width:25px;"/>
                                                </colgroup>
                                                <tr class= "headerstylegrid" >
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td>Total:</td>
                                                    <td align="right"><asp:Label ID="lbltotal" runat="server"></asp:Label></td>
                                                   <%-- <td></td>--%>
                                                    <td>&nbsp;</td>
                                                </tr>
                                        </table>
                                        </div>
                </td>
             </tr>
               </table>
                <br />
              <table  cellpadding="0" cellspacing="0" width="100%">
                <tr>
                  <td style="">
                      <asp:Button ID="btnApprove" runat="server" Text="Approve"  style="  border:none; padding:4px;" CssClass="btn" OnClick="btnApprove_Click" /> &nbsp;
                     <asp:Button ID="btnBacktoStage1" runat="server" Text="Reject"  style="  border:none; padding:4px;" CssClass="btn" OnClick="btnBacktoStage1_Click" /> &nbsp;
                      <asp:Button ID="btnCloseRFPapproval" runat="server" Text="Close" Width="80px"  style="  border:none; padding:4px;" CssClass="btn" OnClick="btnCloseRFPapproval_Click"  />
                     
                  </td>
                </tr>
                   <tr>
               <td style=" background-color:#FFFFCC">
               &nbsp;
                 <asp:Label ID="lbl_inv_Message" runat="server" ForeColor="#C00000"></asp:Label>
               </td>
           </tr>
              </table>
         
            </div>
            </center>
        </div>
    </center>
    </div>
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server"  id="dvRFPApprove" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position :relative; width:650px;padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px; opacity:1;filter:alpha(opacity=100)" class="div_dialog">
                <center>
                    <div style=" float :right " >
                        <asp:ImageButton runat="server" ID="btnApproveRFPRequest_ClosePopup" ImageUrl="~/Modules/HRD/Images/close.gif"  ToolTip="Close this Window." OnClick="btnApproveRFPRequest_ClosePopup_Click" /> </div>
                    <div style="font-size:15px;padding:6px;font-weight:bold;" class="text headerband">
                        RFP Approval [ RFP No : <asp:Label ID="lblRFPPVNo" runat="server"></asp:Label>]                     
                        </div>
                    <table border="0" cellpadding="6" cellspacing="0" style="height: 100px; text-align: left; border-collapse:collapse; width:100%;">
                                <tr>
                                    <td>
                                        <b> Remarks :</b>
                                    </td>
                                </tr>
                                   <tr >
                                          <td style="text-align: left;">
                                              <asp:TextBox ID="txtRFPApprovalRemarks" runat="server"  Width="90%" TextMode="MultiLine" Height="120px" MaxLength="500"></asp:TextBox>
                                               &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                  ControlToValidate="txtRFPApprovalRemarks" Display="Dynamic" ErrorMessage="*" ValidationGroup="RFPA"></asp:RequiredFieldValidator>
                                          </td>
                                   </tr>
                                             
                                    <tr >
                                          <td align="right" style="text-align: right; padding-right:15px; width:100px;" >
                                              
                                            
                                        </td>
                                    </tr>
                                  </table>
                    <div style="text-align:center;padding:5px;">
                        <asp:Button ID="btnRFPApprove" runat="server" Text="Approve" Width="80px" ValidationGroup="RFPA" style="  border:none; padding:4px;" CssClass="btn" OnClick="btnRFPApprove_Click" /> &nbsp;
                        <asp:Button ID="btnCloseRFPApprove" runat="server" Text="Close" CssClass="btn" Width="80px" style="  border:none; padding:4px;" ToolTip="Close this Window." OnClick="btnCloseRFPApprove_Click" />
                    </div>
                    <div style="text-align:center;padding:5px;background-color:#e5e5e5">
                        <asp:Label ID="lblMsgRFPApprove" runat="server" CssClass="error"></asp:Label>
                    </div>
                    
                    
                   </center>
            </div>
        </center>
        </div>   
         <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;z-index:100;" runat="server"  id="dvSendBackToApproval" visible="false" >
    <center>
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position :relative; width:650px;padding :3px; text-align :center; border :solid 1px Red; background : white; z-index:150;top:100px; opacity:1;filter:alpha(opacity=100)" class="div_dialog">
                <center>
                    <div style=" float :right " >
                        <asp:ImageButton runat="server" ID="btnSendBackRFP_ClosePopup_Click" ImageUrl="~/Modules/HRD/Images/close.gif"  ToolTip="Close this Window." OnClick="btnSendBackRFP_ClosePopup_Click_Click" /> </div>
                    <div style="font-size:15px;padding:6px;font-weight:bold;" class="text headerband">
                        Remarks for User [ RFP No : <asp:Label ID="lblSendBackRFPPVNo" runat="server"></asp:Label>]
                        
                    </div>  
                    <table border="0" cellpadding="6" cellspacing="0" style="height: 100px; text-align: left; border-collapse:collapse; width:100%;">
                                <tr>
                                    <td>
                                        <b> Remarks :</b>
                                    </td>
                                </tr>
                                   <tr >
                                          <td style="text-align: left;">
                                              <asp:TextBox ID="txtRFPSendBackComments" runat="server"  Width="90%" TextMode="MultiLine" Height="120px" MaxLength="500"></asp:TextBox>
                                               &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                  ControlToValidate="txtRFPSendBackComments" Display="Dynamic" ErrorMessage="*" ValidationGroup="RFPSB"></asp:RequiredFieldValidator>
                                          </td>
                                   </tr>
                                             
                                    <tr >
                                          <td align="right" style="text-align: right; padding-right:15px; width:100px;" >
                                              
                                            
                                        </td>
                                    </tr>
                                  </table>    
                    <%------------------------------------------------------------------------------%>
                    

                    <div style="text-align:center;padding:5px;">
                        <asp:Button ID="btnSave" runat="server" Text="Save" Width="80px" ValidationGroup="RFPSB" style="  border:none; padding:4px;" CssClass="btn" OnClick="btnSave_Click"  /> &nbsp;
                        <asp:Button ID="btnSendBackClose" runat="server" Text="Close" CssClass="btn" Width="80px" style="  border:none; padding:4px;" ToolTip="Close this Window." OnClick="btnSendBackClose_Click" />
                    </div>
                    <div style="text-align:center;padding:5px;background-color:#e5e5e5">
                        <asp:Label ID="lblMsgSendBackRFP" runat="server" CssClass="error"></asp:Label>
                    </div>
                    </center>    
                    </div>
                    </center>
                        </div>
    
    <asp:Label runat="server" ID="lblMsgMain" Font-Bold="true"></asp:Label>
    <br />
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    <asp:HiddenField ID="hfInvId" runat="server"  />
    <asp:Button ID="btndownload" Text=""  OnClick="btnDownloadFile_Click" style="display:none;" runat="server" /> 
    </div>
        </center>
        </div>
    </form>
   
</body>
</html>
