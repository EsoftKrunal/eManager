<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExportPopUp.aspx.cs" Inherits="Invoice_ExportPopUp" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>POS-Payment voucher</title>
    <script type="text/javascript" src="JS/jquery.min.js"></script>
    <script src="JS/AutoComplete/knockout-2.2.1.js" type="text/javascript"></script>
    <!-- Auto Complete -->
    <link rel="stylesheet" href="JS/AutoComplete/jquery-ui.css" />
    <script src="JS/AutoComplete/jquery-ui.js" type="text/javascript"></script>
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
                                response($.map(data.geonames, function (item) { return { label: item.SupplierNameCode, value: item.SupplierName, id: item.SupplierId } }
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
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ></asp:ToolkitScriptManager>
    <div id="log" style="display:none"></div>
    <div>
                <center>
                        <div style="padding:6px;font-size:18px; color:#333;"><b> Payment by  <asp:Label runat="server" ID="lblAccountType" Text="" ForeColor="Orange"></asp:Label> Account</b></div>
                        <table width="100%" cellpadding="5" border="1" style="border-collapse:collapse;">
                        <tr>
                            <td style="width:130px">PV No. :</td>
                            <td style="text-align:left"><asp:Label runat="server" ID="lblPVNO"></asp:Label></td>
                        </tr>
                            <tr>
                            <td >Owner :</td>
                            <td style="text-align:left"><asp:Label runat="server" ID="lblOwner"></asp:Label> (<asp:Label runat="server" ID="lblOwnerCode" Text="" ForeColor="Blue"></asp:Label>) </td>
                        </tr>
                            <tr>
                            <td >Paid On :</td>
                            <td style="text-align:left">
                                    <asp:Label runat="server" ID="lblBankTransDate"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Bank Amount Paid :</td>
                            <td style="text-align:left">
                                    <asp:Label runat="server" ID="lblAmountPaid" Font-Bold="true" ForeColor="Orange"></asp:Label>
                            </td>
                        </tr>
                        
                         <tr>
                            <td >Bank Charges :</td>
                            <td style="text-align:left">
                                     <asp:Label runat="server" ID="lblBankCharges" Font-Bold="true"  ForeColor="Orange"></asp:Label>
                            </td>
                        </tr>
                       
                         </table>
                        
                     <div style="padding:6px;font-size:18px; color:#333;"><b> Traverse Details ( Vendor Currency  : <asp:Label runat="server" ID="lblVendorCurrency" Font-Bold="true" ForeColor="Red"></asp:Label> ) </b></div>
                     <table width="100%" cellpadding="5" border="1" style="border-collapse:collapse; ">
                         
                          <tr>
                            <td style="width:130px" >Vendor :</td>
                            <td style="text-align:left">
                                <asp:Label runat="server" ID="lblVendorName"></asp:Label> (<asp:Label runat="server" ID="lblVendorCode" Text="" ForeColor="Blue"></asp:Label>) 
                                &nbsp;<asp:Button runat="server" ID="btnChangeVendor" OnClick="btnChangeVendor_Click" Text="Change Vendor"></asp:Button>
                            </td>
                        </tr>
                          <tr>
                            <td>Traverse Exch Rate :</td>
                            <td style="text-align:left;>
                                    <asp:Label runat="server" ID="lblExchRate" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                       
                    </table>
                    <div style="background:#006EB8; padding:5px; text-align:left;color:white;font-weight:bold;">
                        Select Bank Account Code : <asp:DropDownList  runat="server" ID="ddlacctcodes" AutoPostBack="true" OnSelectedIndexChanged="ddlacctcodes_OnSelectedIndexChanged" ></asp:DropDownList>
                    </div>
                    <div style="padding:5px; text-align:center; font-size:17px"><b>Invoice Details </b> </div>
                    <table width="100%" cellpadding="5" border="1" style="border-collapse:collapse;">
                        <tr style="background-color: #e1e5e8;color:#333;">
                            <td style="width:30px">Sr#</td>
                            <td style="width:40px">Vessel </td>
                             <td style="width:60px">Acct Code</td>
                            <td>Invoice#</td>
                            <td style="width:90px">Invoice Dt.</td>
                            <td style="width:130px; text-align:right;">Inv. Amount (<asp:Label runat="server" ID="lblInvoiceCurr" Text="" ForeColor="Green" Font-Bold="true" ></asp:Label>)</td>
                            <td style="width:130px; text-align:right;">Bank Amount (<asp:Label runat="server" ID="lblAccountType1" Text="" ForeColor="Orange" Font-Bold="true" ></asp:Label>)</td>
                            <td style="width:10px"></td>
                            <td style="width:90px">Exch Rate.</td>
                            <td style="width:10px"></td>
                            <td style="width:170px; text-align:right;">Traverse Amount <span style="color:red"><b>(<asp:Label runat="server" ID="lblVendorCurrency1" Text="US$" Font-Bold="true"></asp:Label>)</b></span></td>
                        </tr>
                    
                    <asp:Repeater runat="server" ID="rptTravDetails">
                        <ItemTemplate>
                            <tr>
                                <td ><%#Eval("Srno")%></td>
                                <td><%#Eval("InvVesselCode")%></td>
                                <td><%# Eval("AcctCode")%></td>
                                <td><%#Eval("InvoiceNo")%></td>
                                <td><%#Common.ToDateString(Eval("OnDate"))%></td>
                                <td style="text-align:right;color:green;"><%#Eval("ApprovalAmount")%></td>
                                <td style="text-align:right;color:orange;"><%#Eval("BankAmount")%></td>
                                <td>/</td>
                                <td style="text-align:right;"><%#Eval("ExchRate")%></td>
                                <td>=</td>
                                <td style="text-align:right;font-weight:bold;color:red;"><%#String.Format("{0:0.00}",Eval("TraverseAmount"))%></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                         <tr style="background-color: #E6F3FC;color:#333;">
                            <td ></td>
                            <td >Total </td>
                             <td ></td>
                            <td></td>
                             <td></td>
                            <td style="text-align:right;"><asp:Label runat="server" ID="lblInvoiceTotal" Text="" ForeColor="Green" Font-Bold="true" ></asp:Label></td>
                            <td style="text-align:right;"><asp:Label runat="server" ID="lblBankTotal" Text="" ForeColor="Orange" Font-Bold="true" ></asp:Label></td>
                            <td ></td>
                            <td ><asp:Label runat="server" ID="lblExchDt"></asp:Label></td>
                            <td ></td>
                            <td style=" text-align:right;"><asp:Label runat="server" ID="lblTraverseTotal" Font-Bold="true" ForeColor="Red" Font-Size="18px"></asp:Label></td>
                        </tr>
                    </table>
                    <div runat="server" id="dvBC" visible="false">
                    <div style="padding:5px; text-align:center;; font-size:17px"><b>Bank Charges ( Payable to <asp:Label runat="server" ID="lblBankChgsPayableTo" ForeColor="Blue" Font-Bold="true"></asp:Label> )  </b> </div>
                        <table border="1" bordercolor="#c2c2c2" cellpadding="4" cellspacing="0" rules="all" style="width:100%; height:30px;  border-collapse:collapse;">
                                           
                               <tr style="background-color:#e1e5e8; color:#333;">
                                    <td>Bank Chgs.</td>
                                    <td>Account Code</td>
                                    <td style="width:170px; text-align:right;">Traverse Amount <span style="color:red"><b>(<asp:Label runat="server" ID="Label1" Text="US$" Font-Bold="true"></asp:Label>)</b></span></td>
                                   </tr>
                                <tr style="">
                                    <td>Credit Amount</td>
                                    <td>
                                        <asp:Label runat="server" ID="txtCreditAccountCode" MaxLength="4" Width="35px" style="text-align:center;"></asp:Label>
                                    </td>
                                    <td style="width:170px; text-align:right;font-weight: bold;color:red;">+<asp:Label runat="server" ID="lblCreditAmount" Text="0.00"></asp:Label></td>
                                    
                                </tr>
                                  <tr style="">
                                    <td>Debit Amount </td>
                                    <td>
                                        <asp:Label runat="server" ID="txtDebitAccountCode" MaxLength="4" Width="35px" style="text-align:center;"></asp:Label>
                                     </td>
                                    <td style="width:170px; text-align:right;font-weight: bold;color:red;"><span style="padding-right:3px;">-</span><asp:Label runat="server" ID="lblDebitAmount" Text="0.00"></asp:Label></td>
                                      
                                </tr>
                    </table>
                        </div>
                  <div style="padding:5px;">
                     <asp:Button ID="btnExportNow" runat="server" OnClick="btnExportNow_Click" Visible="false" ValidationGroup="c25" style=" background-color:#006EB8; color:White; border:none; padding:4px;" Text="Export To Traverse" Width="150px" />
          <%--           <asp:Button ID="btnCloseNow" runat="server" Text="Close" Width="150px" Causesvalidation="False" style=" background-color:red; color:White; border:none; padding:4px;" OnClick="btnCloseNow_Click"/>--%>
                </div>
                     <div style="padding:5px;background-color:#f7f841">
                            <asp:Label runat="server" ID="lblMsg" ForeColor="Red" Font-Bold="true"></asp:Label>

                         </div>
                </center>
               
                </div>



        
        <!-----------------Export Payment Voucher ------------------------>
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;" id="dvChangeVendor" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:1; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:1000px; padding :5px; text-align :center;background : white; z-index:2;top:0px; border:solid 10px black;">
            <center >
                <div style="padding:6px;font-size:18px; color:#333;"><b> Change Vendor </b></div>
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
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtSupplier" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                        <asp:HiddenField ID="hfdSupplier" runat="server" />
                    </td>
                </tr>
                </table>
                <div style="padding:5px;">
                     <asp:Button ID="btnchangevendorsave" runat="server" OnClick="btnchangevendorsave_Click" ValidationGroup="c25" style=" background-color:#006EB8; color:White; border:none; padding:4px;" Text="Save" Width="150px" />
                    <asp:Button ID="Button2" runat="server" Text="Close" Width="150px" Causesvalidation="False" style=" background-color:red; color:White; border:none; padding:4px;" OnClick="btnCloseNow1_Click"/>
                </div>
                <div style="padding:5px;background-color:#f7f841">
                            <asp:Label runat="server" ID="lblmsg2" ForeColor="Red" Font-Bold="true"></asp:Label>

                         </div>
            </center>
        </div>
    </center>
    </div>



        <script type="text/javascript">
    function Page_CallAfterRefresh() {
        RegisterAutoComplete();
    }
    $(document).ready(function () {
        RegisterAutoComplete();
    });
</script>
    </form>
</body>
</html>
