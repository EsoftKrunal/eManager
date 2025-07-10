<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExpenseClaim.aspx.cs" Inherits="Invoice_ExpenseClaim" %>
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
     <style type="text/css">
    body
    {
        font-family:Verdana;
        font-size:12px;
        margin:0px;
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
                             data: { Key: $("#txtVendorName").val(), Type: "VEN" },
                             cache: false,
                             success: function (data) {
                                 response($.map(data.geonames, function (item) { return { label: item.SupplierNameCode, value: item.SupplierName, id: item.SupplierId} }
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
                             data: { Key: $("#txtF_Vendor").val(), Type: "VEN" },
                             cache: false,
                             success: function (data) {
                                 response($.map(data.geonames, function (item) { return { label: item.SupplierNameCode, value: item.SupplierName, id: item.SupplierId} }
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
            return baseUrl;
        }
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
    
        <table cellpadding="0" cellspacing="0" width="100%">
         <tr>
         <td style="  padding:6px;" class="text headerband">
             <strong>Expense Claim</strong></td>
         </tr>
         <%--<tr>
         <td style="background-color:#E6F3FC">
            <table cellpadding="3" cellspacing="0" width="100%">
            <tr>
            <td style="text-align:left">&nbsp;From Date :</td>
            <td style="text-align:left">
                <asp:TextBox ID="txtF_D1" runat="server" style="border:solid 1px #008AE6;" Width="100px"></asp:TextBox>
                <asp:CalendarExtender ID="c1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtF_D1"></asp:CalendarExtender>
            </td>
            <td style="text-align:left">To Date :</td>
            <td style="text-align:left">
                <asp:TextBox ID="txtF_D2" runat="server" style="border:solid 1px #008AE6;" Width="100px"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtF_D2"></asp:CalendarExtender>
            </td>
            <td style="text-align:left">Vendor :</td>
            <td style="text-align:left">
                <asp:TextBox ID="txtF_Vendor" runat="server" style="border:solid 1px #008AE6;" 
                    Width="250px"></asp:TextBox>
                </td>
            <td rowspan="2" style="padding:0px">
                <asp:Button ID="btn_Search" runat="server" OnClick="btn_Search_Click" OnClientClick="this.value='Loading..';" style=" background-color:#006EB8; color:White; border:none; padding:4px;" Text="Search" Width="100px" />
                <asp:Button ID="btn_Add" runat="server" OnClick="btn_Add_Click" OnClientClick="this.value='Loading..';" style=" background-color:#006EB8; color:White; border:none; padding:4px;" Text="Add New" Width="100px" />
                <asp:ImageButton runat="server" ID="btnClear" ImageUrl="~/Images/reset.png" OnClick="btnClear_Click" ToolTip="Clear" />                
            </td>
            </tr>
            <tr>
            <td style="text-align:left">Owner : </td>
            <td  style="text-align:left">
                <asp:DropDownList ID="ddlF_Owner" runat="server" Width="255px">
                </asp:DropDownList>
                </td>
            <td style="text-align:left">&nbsp;Inv # : </td>
                <td  style="text-align:left">
                    
                    <asp:TextBox ID="txtF_InvNo" runat="server" style="border:solid 1px #008AE6;" 
                        Width="250px"></asp:TextBox>
                    
                </td>
                <td>                PV # :                </td>
<td style="text-align: left">
    <asp:TextBox ID="txtF_PVNo" runat="server" style="border:solid 1px #008AE6;" 
        Width="250px"></asp:TextBox>
                </td>
                <td>                
                
                </td>
                </tr>
            </table>
         </td>
         </tr>--%>
         <tr>
           <td>
            <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden;HEIGHT: 30px ; text-align:center; border-bottom:none;">
              <table border="1" bordercolor="white" cellpadding="4" cellspacing="0" rules="all" style="width:100%; height:30px;  border-collapse:collapse;">
                                            <colgroup>
                                                <col />
                                                <col style="width:180px;" />
                                                <col style="width:220px;" />
                                                <col style="width:100px;" />
                                                <col style="width:100px;" />
                                                <col style="width:100px;" />
                                                <col style="width:25px;"/>                                                
                                                <col style="width:50px;"/>
                                                <col style="width:25px;"/>
                                                <col style="width:25px;"/>
                                            </colgroup>
                                                <tr align="left" class= "headerstylegrid" >
                                                    <td>Emp Name</td>
                                                    <td>Period</td>
                                                    <td>Vessel</td>
                                                    <td>Location</td>
                                                    <td>Status</td>
                                                    <td>Voucher #</td>
                                                    <td></td>
                                                    <td>Receive</td>
                                                    <td></td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            
                                        </table>
            </div>
            <div id="dv_grd" style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden;HEIGHT: 460px ; text-align:center; border-bottom:none;" class="ScrollAutoReset">
            <table border="1" bordercolor="#F0F5F5" cellpadding="4" cellspacing="0" style=" text-align: center; border-collapse:collapse; width:100%;">
                <colgroup>
                <col />
                <col style="width:180px;" />
                <col style="width:220px;" />
                <col style="width:100px;" />
                <col style="width:100px;" />
                <col style="width:100px;" />
                <col style="width:25px;"/>
                <col style="width:50px;"/>
                <col style="width:25px;"/>
                <col style="width:25px;"/>
                </colgroup>
                <asp:Repeater ID="RptExpense" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="left"><%#Eval("EmpName")%></td>
                            <td align="left"><%#Common.ToDateString(Eval("LeaveFrom"))%> - <%#Common.ToDateString(Eval("LeaveTo"))%></td>
                            <td align="left"><%#Eval("VesselName")%></td>
                            <td align="left"><%#Eval("LocationText")%> </td>
                            <td align="left"><%#Eval("Status")%> </td>
                            <td align="center"><%#Eval("PaymentVoucherNo")%></td>
                            <%--<td>
                                <asp:ImageButton runat="server" ID="btnView" ImageUrl="~/Images/magnifier.png" OnClick="btnView_Click" CommandArgument='<%#Eval("LeaveRequestId")%>' ToolTip="View" />
                            </td>--%>
                            <td>
                                <asp:ImageButton runat="server" ID="btnDownLoad" ImageUrl="~/Modules/HRD/Images/paperclip.gif" OnClick="btnDownLoad_Click" Visible='<%#(Eval("FileName").ToString().Trim()!="")%>' CommandArgument='<%#Eval("LeaveRequestId")%>' ToolTip="Attachment" />
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="chkReceived" AutoPostBack="true" OnCheckedChanged="chkReceived_CheckedChanged"  ToolTip="Receive" Visible='<%#(Eval("Status").ToString().Trim()=="Requested")%>' LeaveReqId='<%#Eval("LeaveRequestId")%>'  />
                            </td>
                            <td>
                                <asp:LinkButton runat="server" ID="btnPay" OnClick="btnPay_Click" Text="Pay" Visible='<%#(Eval("Status").ToString().Trim()=="Received")%>' CommandArgument='<%#Eval("LeaveRequestId")%>'  />
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr style="background-color:#E6F3FC">
                            <td align="left"><%#Eval("EmpName")%></td>
                            <td align="left"><%#Common.ToDateString(Eval("LeaveFrom"))%> - <%#Common.ToDateString(Eval("LeaveTo"))%></td>
                            <td align="left"><%#Eval("VesselName")%></td>
                            <td align="left"><%#Eval("LocationText")%> </td>
                            <td align="left"><%#Eval("Status")%> </td>
                            <td align="center"><%#Eval("PaymentVoucherNo")%></td>
                            <%--<td>
                                <asp:ImageButton runat="server" ID="btnView" ImageUrl="~/Images/magnifier.png" OnClick="btnView_Click" CommandArgument='<%#Eval("LeaveRequestId")%>' ToolTip="View" />
                            </td>--%>
                            <td>
                                <asp:ImageButton runat="server" ID="btnDownLoad" ImageUrl="~/Modules/HRD/Images/paperclip.gif" OnClick="btnDownLoad_Click" Visible='<%#(Eval("FileName").ToString().Trim()!="")%>' CommandArgument='<%#Eval("LeaveRequestId")%>' ToolTip="Attachment" />
                            </td>
                            <td>
                                <asp:CheckBox runat="server" ID="chkReceived" AutoPostBack="true" OnCheckedChanged="chkReceived_CheckedChanged"  ToolTip="Receive" Visible='<%#(Eval("Status").ToString().Trim()=="Requested")%>' LeaveReqId='<%#Eval("LeaveRequestId")%>'  />
                            </td>
                            <td>
                                <asp:LinkButton runat="server" ID="btnPay" OnClick="btnPay_Click" Text="Pay" Visible='<%#(Eval("Status").ToString().Trim()=="Received")%>' CommandArgument='<%#Eval("LeaveRequestId")%>'  />
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
                     <asp:Button ID="btnClose" runat="server" Text="Close" Width="150px" style=" background-color:red; color:White; border:none; padding:4px;" OnClientClick="window.close()" />
                  </td>
                </tr>
              </table>
               <br />
           </td>
         </tr>
       </table>
   
   <!-----------------Add Payment Voucher ------------------------>
    <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;" id="dv_Pay" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:40%; padding :5px; text-align :center;background : white; z-index:150;top:50px; border:solid 10px black;">
            <center >
             <div style="padding:6px;  font-size:14px; " class="text headerband"><b>Expense Payment</b></div>
             <div style="width:100%; text-align:left; overflow-y:scroll; overflow-x:hidden;">
             <table border="0" bordercolor="#F0F5F5" cellpadding="2" cellspacing="0" style="height: 100px; text-align: center; border-collapse:collapse; width:100%;">
                      <tr style="background-color:#E6F3FC">
                          <td align="right" style="text-align: right; padding-right:15px; width: 123px;">
                              Voucher #:</td>
                          <td style="text-align: left; ">
                              <asp:TextBox ID="txtVoucherNo" runat="server" CssClass="input_box" Width="126px"></asp:TextBox>
                              <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtVoucherNo" ValidationGroup="pay" ErrorMessage="*" ></asp:RequiredFieldValidator>  
                          </td>
                      </tr>
                      <tr style="background-color:#E6F3FC">                          
                          <td align="right" style="text-align: right; padding-right:15px; width: 123px; ">Payment Date:</td>
                          <td style="text-align: left;">
                              <asp:TextBox ID="txtPaymentDate" runat="server" CssClass="input_box" Width="126px"  ></asp:TextBox>
                              &nbsp;<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtPaymentDate" ValidationGroup="pay" ErrorMessage="*" ></asp:RequiredFieldValidator>  
                          </td>
                      </tr>
                     </table>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupPosition="TopRight"  TargetControlID="txtPaymentDate"></asp:CalendarExtender>
                     
                <div>
                <center>
                <br />
                <asp:Label runat="server" ID="lblMsgPOP" Font-Bold="true"></asp:Label>
                <br />
                <asp:Button ID="btnSavePayment" runat="server" OnClick="btnSavePayment_Click" OnClientClick="this.value='Loading..';" style="  border:none; padding:4px;" Text="Save" ValidationGroup="pay" Width="100px" CssClass="btn" />
                <%--<asp:Button ID="btnVoucherPrint" runat="server" OnClick="btnVoucherPrint_Click" OnClientClick="this.value='Loading..';" style=" background-color:#006EB8; color:White; border:none; padding:4px;" Text="Print" Width="150px" />--%>
                <asp:Button ID="btnClosePOP" runat="server" Text="Close" Width="100px" Causesvalidation="False" style="  border:none; padding:4px;" OnClick="btnClosePOP_Click" CssClass="btn"/>
                <asp:HiddenField ID="hfLeaveRequestId" runat="server" />
                </center>
                <div>
                
                </div>
                </div>
            </div>
            </center>
        </div>
    </center>
    </div>
    <asp:Label runat="server" ID="lblMsgMain" Font-Bold="true"></asp:Label>
    <br />
    
    <asp:HiddenField ID="hfInvId" runat="server" />
    <asp:Button ID="btndownload" Text=""  OnClick="btnDownloadFile_Click" style="display:none;" runat="server" /> 
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
