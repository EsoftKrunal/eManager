<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExportToTraverse.aspx.cs" Inherits="Invoice_ExportToTraverse" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>POS-Payment voucher</title>
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
  <script type="text/javascript" src="JS/KPIScript.js"></script>
    <script type="text/javascript">
        function RegisterAutoComplete() {
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
                            data: { Key: $("#txtSupplier").val(), Type: "VEN" },
                            cache: false,
                            success: function (data) {
                                response($.map(data.geonames, function (item) { return { label: item.SupplierName, value: item.SupplierName, id: item.SupplierId ,active:item.Active } }
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
        //function getBaseURL() {

        //    var url = window.location.href.split('/');
        //    var baseUrl = url[0] + '//' + url[2] + '/' + url[3];
        //    return baseUrl;
        //}
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ></asp:ToolkitScriptManager>
    <div id="log" style="display:none"></div>
    <div>
    <center>
    <div style="border:solid 1px #008AE6;">
    
        <table cellpadding="0" cellspacing="0" width="100%">
         <tr>
         <td style=" background-color:#008AE6; color:White; padding:10px; text-align:center;">
             <strong>Payment Voucher - Export to Traverse ( Debit Memo )</strong></td>
         </tr>
         <tr>
             <td style=" background-color:#E6F3FC; color:darkslateblue; padding:6px; text-align:center;"><strong>
                 <asp:Label ID="lblUserName" runat="server"></asp:Label>
                 </strong></td>
             </tr>
            <tr>
                <td style="background-color:#E6F3FC">
                    <table cellpadding="3" cellspacing="0" width="100%">
                        <tr>
                            <td style="text-align:left">&nbsp;Period :</td>
                            <td style="text-align:left">
                                <asp:TextBox ID="txtF_D1" runat="server" style="border:solid 1px #008AE6;" Width="100px"></asp:TextBox>
                                <asp:CalendarExtender ID="c1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtF_D1"></asp:CalendarExtender>
                                <asp:TextBox ID="txtF_D2" runat="server" style="border:solid 1px #008AE6;" Width="100px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtF_D2"></asp:CalendarExtender>
                            </td>
                            <td style="text-align:left">Vendor :</td>
                            <td style="text-align:left">
                                <asp:TextBox ID="txtF_Vendor" runat="server" style="border:solid 1px #008AE6;" Width="250px"></asp:TextBox>
                            </td>
                            <td style="text-align:left">Owner : </td>
                            <td style="text-align:left">
                                <asp:DropDownList ID="ddlF_Owner" runat="server" Width="255px"></asp:DropDownList>
                            </td>
                            <td>PV # : </td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtF_PVNo" runat="server" style="border:solid 1px #008AE6;" Width="150px"></asp:TextBox>
                            </td>
                            <td style="text-align:left">&nbsp;Status : </td>
                            <td style="text-align:left">
                                 <asp:DropDownList ID="ddlStatus" runat="server" Width="100px">
                                     <asp:ListItem Text="All" Value=""></asp:ListItem>
                                     <asp:ListItem Text="Pending" Value="0" Selected="True"></asp:ListItem>
                                     <asp:ListItem Text="Exported" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="padding:0px">
                                <asp:Button ID="btn_Search" runat="server" OnClick="btn_Search_Click" OnClientClick="this.value='Loading..';" style=" background-color:#006EB8; color:White; border:none; padding:4px;" Text="Search" Width="100px" />
                                <asp:HiddenField ID="hfdSupplier" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden;HEIGHT: 30px ; text-align:center; border-bottom:none;">
                        <table border="1" bordercolor="white" cellpadding="4" cellspacing="0" rules="all" style="width:100%; height:30px;  border-collapse:collapse;">
                            <colgroup>
                                <col style="width:40px;"/>
                                <col style="width:60px;" />
                                <col style="width:130px;" />
                                <col />
                                <%--<col style="width:150px;" />--%>
                                
                                
                                <col style="width:40px;" />
                                <col style="width:120px;" />
                                <col style="width:120px;" />
                                <col style="width:100px;" />
                                <col style="width:50px;" />
                                <col style="width:30px;" />
                                <col style="width:80px;" />
                                <col style="width:120px;" />
                                <col style="width:120px;" />
                                <col style="width:25px;"/>
                                <tr align="left" style=" background-color:#008AE6; color:White;">
                                    <td></td>
                                    <td>Owner </td>
                                    <td>PV #</td>
                                    <td>Vendor</td>
                                    
                                    <%--<td>MTM Voucher #</td>--%>
                                    
                                    <td>Curr.</td>
                                    <td align="right">Amount </td>
                                    <td>Paid By </td>
                                    <td>Paid On </td>
                                    <td>PayIn</td>
                                    <td>&nbsp;</td>
                                    <td align="right">Bank Chgs.</td>
                                    <td align="right">Bank Amount</td>
                                    <td >Exported By / On</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </colgroup>
                        </table>
                    </div>
                    <div id="dv_grd" class="ScrollAutoReset" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden;HEIGHT: 460px ; text-align:center; border-bottom:none;">
                        <table border="1" bordercolor="#F0F5F5" cellpadding="4" cellspacing="0" style=" text-align: center; border-collapse:collapse; width:100%;">
                            <colgroup>
                                <col style="width:40px;"/>
                                 <col style="width:60px;" />
                                <col style="width:130px;" />
                                <col />
                                <%--<col style="width:150px;" />--%>
                                
                               
                                <col style="width:40px;" />
                                <col style="width:120px;" />
                                <col style="width:120px;" />
                                <col style="width:100px;" />
                                <col style="width:50px;" />
                                <col style="width:30px;" />
                                <col style="width:80px;" />
                                <col style="width:120px;" />
                                <col style="width:120px;" />
                                <col style="width:25px;"/>
                            </colgroup>
                            <asp:Repeater ID="RptMyInvoices" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:ImageButton runat="server" ID="btnExport" CommandArgument='<%#Eval("PaymentId")%>' OnClick="btn_Export_Click"  ImageUrl="~/Images/Gear.png" Visible='<%#(Eval("Exported").ToString()=="False")%>'/>
                                        </td>
                                        <td align="left"><%#Eval("POSOwnerId")%></td>
                                        <td align="left">
                                            <a id="A1" runat="server" onclick='<%#"PrintVoucherN(" +Eval("PaymentId").ToString() + ");"%>' style="color:Blue; text-decoration:underline;cursor:pointer;" visible='<%#(Eval("PTYPE").ToString()=="N")%>'><%#Eval("PVNo")%></a>
                                            <a id="A2" runat="server" onclick='<%#"PrintVoucherO(" +Eval("PaymentId").ToString() + ");"%>' style="color:Blue; text-decoration:underline;cursor:pointer;" visible='<%#(Eval("PTYPE").ToString()=="O")%>'><%#Eval("PVNo")%></a>
                                        </td>
                                        <td align="left"><%#Eval("SupplierName")%><span runat="server" id="c1" visible='<%#(Eval("Status").ToString()=="C")%>' style="color:red"> - Cancelled </span>
                                            <b style="color:Red"><i style='display: <%#Eval("TravId").ToString() == "" ? "none" : "block" %>'>( <%#Eval("TravId")%>) </i></b>
                                        </td>
                                        <%--<td align="right"><%#Eval("VoucherNo")%></td>--%>
                                        <td align="left"><%#Eval("Currency")%></td>
                                        <td align="right"><%#Eval("Amount")%></td>
                                        <td align="left"><%#Eval("PaidBy")%></td>
                                        <td align="left"><%#Common.ToDateString(Eval("paidon"))%></td>
                                        <td align="left"><%#((Eval("PaymentType").ToString()=="U")?"USD":((Eval("PaymentType").ToString()=="S")?"SGD":"INR"))%></td>
                                        <td><img src="../Images/check_white.png" runat="server" BankCharges='<%#Eval("BankCharges")%>' BankAmount='<%#Eval("BankAmount")%>' onclick="OpenUpdate(this)" visible='<%#(Common.ToDateString(Eval("BankConfirmedOn"))=="") && (Eval("Status").ToString()=="A")%>' RecordType='<%#Eval("PTYPE")%>' paymentid='<%#Eval("PaymentId")%>' />                                          </td>
                                        <td><asp:Label runat="server" ID="txtBCharges" Text='<%#Eval("BankCharges")%>' Width="95%" style="text-align:right" MaxLength="12" onkeyup="UpdateBankChgs(this)" RecordType='<%#Eval("PTYPE")%>' paymentid='<%#Eval("PaymentId")%>' CssClass='<%#((Common.ToDateString(Eval("BankConfirmedOn"))=="")?"pending":"")%>'></asp:Label></td>
                                        <td><asp:Label runat="server"  ID="txtBamount" Text='<%#Eval("BankAmount")%>' Width="95%" style="text-align:right" MaxLength="12" onkeyup="UpdateBankAmt(this)" RecordType='<%#Eval("PTYPE")%>'  paymentid='<%#Eval("PaymentId")%>' CssClass='<%#((Common.ToDateString(Eval("BankConfirmedOn"))=="")?"pending":"")%>'></asp:Label></td>
                                        <td align="left"><%#Eval("ExportedBy")%>/<%#Common.ToDateString(Eval("ExportedOn"))%></td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr style="background-color:#E6F3FC">
                                        <td>
                                           <asp:ImageButton runat="server" ID="btnExport" CommandArgument='<%#Eval("PaymentId")%>' CssClass='<%#Eval("PTYPE")%>' OnClick="btn_Export_Click"  ImageUrl="~/Images/Gear.png" Visible='<%#(Eval("Exported").ToString()=="False")%>'/>
                                        </td>
                                        <td align="left"><%#Eval("POSOwnerId")%></td>
                                        <td align="left">
                                            <a id="A1" runat="server" onclick='<%#"PrintVoucherN(" +Eval("PaymentId").ToString() + ");"%>' style="color:Blue; text-decoration:underline;cursor:pointer;" visible='<%#(Eval("PTYPE").ToString()=="N")%>'><%#Eval("PVNo")%></a>
                                            <a id="A2" runat="server" onclick='<%#"PrintVoucherO(" +Eval("PaymentId").ToString() + ");"%>' style="color:Blue; text-decoration:underline;cursor:pointer;" visible='<%#(Eval("PTYPE").ToString()=="O")%>'><%#Eval("PVNo")%></a>
                                        </td>
                                        <td align="left"><%#Eval("SupplierName")%><span runat="server" id="c1" visible='<%#(Eval("Status").ToString()=="C")%>' style="color:red"> - Cancelled </span>
                                            <b style="color:Red"><i style='display: <%#Eval("TravId").ToString() == "" ? "none" : "block" %>'>( <%#Eval("TravId")%>) </i></b>
                                        </td>
                                        <%--<td align="right"><%#Eval("VoucherNo")%></td>--%>
                                        <td align="left"><%#Eval("Currency")%></td>
                                        <td align="right"><%#Eval("Amount")%></td>
                                        <td align="left"><%#Eval("PaidBy")%></td>
                                        <td align="left"><%#Common.ToDateString(Eval("paidon"))%></td>
                                        <td align="left"><%#((Eval("PaymentType").ToString()=="U")?"USD":((Eval("PaymentType").ToString()=="S")?"SGD":"INR"))%></td>
                                        <td><img src="../Images/check_white.png" runat="server" BankCharges='<%#Eval("BankCharges")%>' BankAmount='<%#Eval("BankAmount")%>' onclick="OpenUpdate(this)" visible='<%#(Common.ToDateString(Eval("BankConfirmedOn"))=="") && (Eval("Status").ToString()=="A")%>' RecordType='<%#Eval("PTYPE")%>' paymentid='<%#Eval("PaymentId")%>' />                                          </td>
                                        <td><asp:Label runat="server" ID="txtBCharges" Text='<%#Eval("BankCharges")%>' Width="95%" style="text-align:right" MaxLength="12" onkeyup="UpdateBankChgs(this)" RecordType='<%#Eval("PTYPE")%>' paymentid='<%#Eval("PaymentId")%>' CssClass='<%#((Common.ToDateString(Eval("BankConfirmedOn"))=="")?"pending":"")%>'></asp:Label></td>
                                        <td><asp:Label runat="server"  ID="txtBamount" Text='<%#Eval("BankAmount")%>' Width="95%" style="text-align:right" MaxLength="12" onkeyup="UpdateBankAmt(this)" RecordType='<%#Eval("PTYPE")%>'  paymentid='<%#Eval("PaymentId")%>' CssClass='<%#((Common.ToDateString(Eval("BankConfirmedOn"))=="")?"pending":"")%>'></asp:Label></td>
                                        <td align="left"><%#Eval("ExportedBy")%>/<%#Common.ToDateString(Eval("ExportedOn"))%></td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </AlternatingItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                    <div style="padding:5px; text-align:center">
                        <asp:Button ID="btnClose" runat="server" OnClientClick="window.close()" style=" background-color:red; color:White; border:none; padding:4px;" Text="Close" Width="100px"/>
                    </div>
                </td>
            </tr>
       </table>
  
    <!-----------------Export Payment Voucher ------------------------>
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;" id="dvExport" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:90%; padding :5px; text-align :center;background : white; z-index:150;top:30px; border:solid 10px black;">
            <center >
               <iframe runat="server" width="100%" height="580px" id="frm1"></iframe>
             </center>
            <div style="padding:5px; text-align:center">
                <asp:Button ID="Button2" runat="server" Text="Close" Width="150px" Causesvalidation="False" style=" background-color:red; color:White; border:none; padding:4px;" OnClick="btnCloseNow_Click"/>
        
        </div>
             </div>
    </center>
        
    </div>

    <!-- End -->
    <asp:Label runat="server" ID="lblMsgMain" Font-Bold="true"></asp:Label>
    
    </div>
    </center>
    </div>
        
    </form>
</body>
    <script type="text/javascript">
    function Page_CallAfterRefresh() {
        RegisterAutoComplete();
    }
    $(document).ready(function () {
        RegisterAutoComplete();
    });

    function PrintVoucherN(pid) {
        winref = window.open('PaymentVoucher.aspx?PaymentId=' + pid + '&PaymentMode=N', '');
        return false;
    }
    function PrintVoucherO(pid) {
        winref = window.open('PaymentVoucher.aspx?PaymentId=' + pid + '&PaymentMode=O', '');
        return false;
    }
</script>
</html>
