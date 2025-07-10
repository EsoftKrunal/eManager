<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FindInvoice.aspx.cs" Inherits="Invoice_FindInvoice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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
        body {
            font-family: Verdana;
            font-size: 12px;
            margin: 0px;
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
                $("#txtF_Vendor").autocomplete(
                    {
                        source: function (request, response) {
                            $.ajax({
                                 url: getBaseURL() + "/Modules/Purchase/Invoice/getautocompletedata.ashx",
                                /*url: getBaseURL() + "/Purchase/Invoice/getautocompletedata.ashx",*/
                                dataType: "json",
                                headers: { "cache-control": "no-cache" },
                                type: "POST",
                                data: { Key: $("#txtF_Vendor").val(), Type: "VENALL" },
                                cache: false,
                                success: function (data) {
                                    response($.map(data.geonames, function (item) { return { label: item.SupplierNameCode, value: item.SupplierName, id: item.SupplierId, active: item.Active } }
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

                //---------------
                $("#txtSupplier_Update").autocomplete(
                    {
                        source: function (request, response) {
                            $.ajax({
                                url: getBaseURL() + "/Modules/Purchase/Invoice/getautocompletedata.ashx",
                               /* url: getBaseURL() + "/Purchase/Invoice/getautocompletedata.ashx",*/
                                dataType: "json",
                                headers: { "cache-control": "no-cache" },
                                type: "POST",
                                data: { Key: $("#txtSupplier_Update").val(), Type: "VENALL" },
                                cache: false,
                                success: function (data) {
                                    response($.map(data.geonames, function (item) { return { label: item.SupplierNameCode, value: item.SupplierName, id: item.SupplierId, active: item.Active } }
                                    ));
                                }
                            });
                        },
                        minLength: 2,
                        select: function (event, ui) {
                            log(ui.item ? "Selected: " + ui.item.label : "Nothing selected, input was " + this.value);
                            $("#hfdSupplierId_Update").val(ui.item.id);
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
        function download(m) {
            var getvalue = m.getAttribute("invid");
            //             alert(getvalue);
            var hf = document.getElementById("hfInvId");
            var btn = document.getElementById("btndownload");
            hf.value = getvalue;
            //             alert(hf.value);
            btn.click();
        }
        function viewInv(InvId) {

            winref = window.open('ViewInvoice.aspx?InvoiceId=' + InvId + '&PaymentMode=O', '', '');
            return false;
        }
        function PrintVoucher(InvId) {

            winref = window.open('PaymentVoucher.aspx?PaymentId=' + InvId + '&PaymentMode=O', '', '');
            return false;
        }


    </script>
    <script type="text/javascript" src="JS/KPIScript.js"></script>
</head>
<body>
    <form id="form1" runat="server">
         <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <div id="log" style="display: none"></div>
        <div>
            <center>
                <div style="border: solid 1px #008AE6;">
                    <%--<asp:UpdatePanel runat="server" id="up1">
    <ContentTemplate>--%>
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style=" padding: 6px;" class="text headerband" >
                                <strong>Invoice Management - Find Invoice</strong></td>
                        </tr>
                        <tr>
                            <td style="background-color: #E6F3FC">
                                <table cellpadding="3" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="text-align: left">Vendor : </td>
                                        <td>
                                            <asp:TextBox ID="txtF_Vendor" runat="server" Style="border: solid 1px #008AE6;" Width="200px"></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="hfdSupplier" />
                                        </td>
                                        <td style="text-align: left">Inv #: </td>
                                        <td>
                                            <asp:TextBox ID="txtF_InvNo" runat="server" Style="border: solid 1px #008AE6;" Width="200px"></asp:TextBox></td>
                                        <td style="text-align: left">Ref # : </td>
                                        <td>
                                            <asp:TextBox ID="txtF_RefNo" runat="server" Style="border: solid 1px #008AE6;" Width="200px"></asp:TextBox></td>
                                        <td>Invoice Period : </td>
                                        <td>
                                            <asp:TextBox ID="txt_FDate1" runat="server" Width="110px" Style="border: solid 1px #008AE6;"></asp:TextBox>
                                            <asp:TextBox ID="txt_FDate2" runat="server" Width="110px" Style="border: solid 1px #008AE6;"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupPosition="TopRight" TargetControlID="txt_FDate1"></asp:CalendarExtender>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupPosition="TopRight" TargetControlID="txt_FDate2"></asp:CalendarExtender>
                                        </td>
                                        <td rowspan="4" style="padding: 0px">
                                            <asp:Button ID="btn_Search" runat="server" OnClick="btn_Search_Click" OnClientClick="this.value='Loading..';" Style="border: none; padding: 4px;" Text="Search" Width="100px" CssClass="btn" /> <br />
                                            <asp:Button runat="server" ID="btnClear" text="Clear" OnClick="btnClear_Click" ToolTip="Clear" CssClass="btn"  /> <br />
                <asp:Button ID="btnExportToExcel" runat="server" Text="Download Excel" CssClass="btn" ToolTip="Export to Excel" OnClick="btnExportToExcel_Click" /> &nbsp;
                                        </td>
                                    </tr>
                                    <tr>


                                        <td style="text-align: left">PO # :</td>
                                        <td>
                                            <asp:TextBox ID="txtF_PONo" runat="server" Style="border: solid 1px #008AE6;" Width="200px"></asp:TextBox></td>
                                        <td style="text-align: left">PV # :</td>
                                        <td>
                                            <asp:TextBox ID="txtF_PVNo" runat="server" Style="border: solid 1px #008AE6;" Width="200px"></asp:TextBox></td>
                                        <td style="text-align:right;padding-right:5px;"> Adv. Payment :</td>
                                        <td style="text-align:left;padding-left:5px;"><asp:CheckBox id='chkAdvPayment'  runat="server" OnCheckedChanged="chkAdvPayment_CheckedChanged" AutoPostBack="true" /></td>
                                        <td style="text-align:right;padding-right:5px;">
                                            Non PO :
                                        </td>
                                        <td style="text-align:left;padding-left:5px;">
                                            <asp:CheckBox id='chkNonPo'  runat="server" OnCheckedChanged="chkNonPo_CheckedChanged" AutoPostBack="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">Stage : </td>
                                        <td>
                                            <asp:DropDownList ID="ddlF_Stage" runat="server" Width="95%">
                                                <asp:ListItem Text="All" Value=""></asp:ListItem>
                                                <asp:ListItem Text="Entry" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Processing" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Approval" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Payment" Value="3"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left">Status : </td>
                                        <td>
                                            <asp:DropDownList ID="ddlF_Status" runat="server" Width="95%">
                                                <asp:ListItem Text="All" Value=""></asp:ListItem>
                                                <asp:ListItem Text="Paid" Value="Paid"></asp:ListItem>
                                                <asp:ListItem Text="UnPaid" Value="UnPaid"></asp:ListItem>
                                                <asp:ListItem Text="Cancelled" Value="Cancelled"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left">Owner : </td>
                                        <td>
                                            <asp:DropDownList ID="ddlF_Owner" runat="server" Width="95%">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left">Vessel : </td>
                                        <td>
                                            <asp:DropDownList ID="ddlF_Vessel" runat="server" Width="95%">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                  <%--  <tr>
                                        <td style="text-align: left">Entered By :</td>
                                        <td>
                                            <asp:DropDownList ID="ddlUser_E" runat="server" Width="95%"></asp:DropDownList></td>
                                        <td style="text-align: left">Processed By :</td>
                                        <td>
                                            <asp:DropDownList ID="ddlUser_A" runat="server" Width="95%"></asp:DropDownList></td>
                                        <td style="text-align: left">Approved By :</td>
                                        <td>
                                            <asp:DropDownList ID="ddlUser_V" runat="server" Width="95%"></asp:DropDownList></td>
                                        <td style="text-align: left">Paid By :</td>
                                        <td>
                                            <asp:DropDownList ID="ddlUser_P" runat="server" Width="95%"></asp:DropDownList></td>
                                    </tr>--%>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="overflow-y: scroll; overflow-x: hidden; height: 30px; text-align: center; border-bottom: none;">
                                    <table border="1" bordercolor="white" cellpadding="4" cellspacing="0" rules="all" style="width: 100%; height: 30px; border-collapse: collapse;">
                                        <colgroup>
                                           <%-- <col style="width: 25px;" />
                                            <col style="width: 25px;" />--%>
                                            <col style="width: 2%;" />
                                            <col style="width: 4%; text-align: center;" />
                                            <col />
                                            <col style="width: 13%;" />
                                            <col style="width: 8%;" />
                                            <col style="width: 8%;" />
                                            <col style="width: 8%;" />
                                            <col style="width: 4%;" />
                                            <col style="width: 7%;" />
                                            <col style="width: 5%;" />
                                            <col style="width: 10%;" />
                                            <col style="width: 5%;" />
                                            <col style="width: 7%;" />
                                            <col style="width: 9%;" />
                                            <col style="width: 2%;" />
                                            <%--<col style="width: 25px;" />--%>
                                            <tr class= "headerstylegrid">
                                              <%--  <td></td>
                                                <td></td>--%>
                                                <td></td>
                                                <td>Ref #</td>
                                                <td>Vendor</td>
                                                <td>Inv #</td>
                                                <td>Inv Dt.</td>
                                                <td>Rec Dt.</td>
                                                <td>Inv Amt</td>
                                                <td>Curr.</td>
                                                <td>Payment Due</td>
                                                <td>Vessel</td>
                                                <td>PV #</td>
                                                <td>Status</td>
                                                <td>Stage</td>
                                                <td>Curr. User</td>
                                                <td></td>
                                               <%-- <td>&nbsp;</td>--%>
                                            </tr>
                                        </colgroup>
                                    </table>
                                </div>
                                <div id="dv_grd" style="overflow-y: scroll; overflow-x: hidden; height: 460px; text-align: center; border-bottom: none;" class="ScrollAutoReset">
                                    <table border="1" bordercolor="#F0F5F5" cellpadding="4" cellspacing="0" style="text-align: center; border-collapse: collapse; width: 100%;">
                                        <colgroup>
                                           <%--   <col style="width: 25px;" />
                                          <col style="width: 25px;" />--%>
                                            <col style="width: 2%;" />
                                            <col style="width: 4%; text-align: center;" />
                                            <col />
                                            <col style="width: 13%;" />
                                            <col style="width: 8%;" />
                                            <col style="width: 8%;" />
                                            <col style="width: 8%;" />
                                            <col style="width: 4%;" />
                                            <col style="width: 7%;" />
                                            <col style="width: 5%;" />
                                            <col style="width: 10%;" />
                                            <col style="width: 5%;" />
                                            <col style="width: 7%;" />
                                            <col style="width: 9%;" />
                                            <col style="width: 2%;" />
                                           <%-- <col style="width: 25px;" />--%>
                                        </colgroup>
                                        <asp:Repeater ID="RptMyInvoices" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                  <%--   <td>
                                                        <asp:ImageButton runat="server" ID="btnShowCancelInv" ImageUrl="~/Modules/HRD/Images/Close.gif" OnClick="btnShowCancelInv_Click" CommandArgument='<%#Eval("InvoiceId")%>' ToolTip="Cancel Invoice" Visible='<%#(CancelAllowed && Eval("Status").ToString().Trim() == "UnPaid")%>' />
                                                    </td>
                                                   <td>
                                                        <asp:ImageButton runat="server" ID="imgBtnShowUpdateUser" ImageUrl="~/Modules/HRD/Images/reset.png" OnClick="imgBtnShowUpdateUser_Click" CommandArgument='<%#Eval("InvoiceId")%>' ToolTip="Invoice Correction" CausesValidation="false" Visible='<%#(Eval("Status").ToString().Trim() == "UnPaid" && CancelAllowed && Eval("StageText").ToString().Trim() != "Entry")%>' />
                                                    </td>--%>
                                                    <td>
                                                        <asp:ImageButton runat="server" ID="btnChangeStage" ImageUrl="~/Modules/HRD/Images/Gear.png" OnClick="imgChangeStage_Click" CommandArgument='<%#Eval("InvoiceId")%>' ToolTip="Invoice Correction" CausesValidation="false" Visible='<%#(Eval("Status").ToString().Trim() == "UnPaid" && CancelAllowed && Eval("StageText").ToString().Trim() != "Entry" &&  UserId==1 )%>' />
                                                    </td>
                                                    <td align="left"><a onclick='viewInv(<%#Eval("InvoiceId")%>);' href="#"><%#Eval("RefNo")%></a></td>
                                                    <td align="left"><%#Eval("Vendor")%> <b style='color: Red'><i>( <%#Eval("VendorCode")%> ) </i></b></td>
                                                    <td align="left"><%#Eval("InvNo")%></td>
                                                    <td align="left"><%#Common.ToDateString(Eval("InvDate"))%></td>
                                                    <td align="left"><%#Common.ToDateString(Eval("EnteredOn"))%></td>
                                                    <td align="right"><%#Eval("InvoiceAmount")%></td>
                                                    <td align="left"><%#Eval("Currency")%></td>
                                                    <td align="left"><%#Common.ToDateString(Eval("DueDate"))%></td>
                                                    <td align="left"><%#Eval("INVVesselCode")%> </td>
                                                    <td align="left"><a onclick='PrintVoucher(<%#Eval("PAYMENTID")%>);' href="#" style='<%#(( Eval("Status").ToString().Trim() == "Paid" && Eval("Stage").ToString().Trim() == "3")) ? "": "display:none" %>'><%#Eval("VoucherNo")%></a></td>
                                                    <td align="left"><%#Eval("Status")%></td>
                                                    <td align="left"><%#Eval("StageText")%></td>
                                                    <td align="left"><%#Eval("CurrentUser")%> </td>
                                                    <td style="text-align: center;">
                                                        <img id="Img3" src="~/Modules/HRD/Images/paperclip.gif" onclick="download(this);" invid='<%#Eval("InvoiceId")%>' style='<%#(Eval("AttachmentName").ToString() != "") ? "": "visibility:hidden" %>' title="download attachment" runat="server" alt="" />
                                                    </td>
                                                   <%-- <td>&nbsp;</td>--%>
                                                </tr>
                                            </ItemTemplate>
                                            <AlternatingItemTemplate>
                                                <tr style="background-color: #E6F3FC">
                                                <%--    <td>
                                                        <asp:ImageButton runat="server" ID="btnShowCancelInv" ImageUrl="~/Modules/HRD/Images/close.gif" OnClick="btnShowCancelInv_Click" CommandArgument='<%#Eval("InvoiceId")%>' ToolTip="Cancel Invoice" Visible='<%#(CancelAllowed && Eval("Status").ToString().Trim() == "UnPaid")%>' />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton runat="server" ID="imgBtnShowUpdateUser" ImageUrl="~/Modules/HRD/Images/reset.png" OnClick="imgBtnShowUpdateUser_Click" CommandArgument='<%#Eval("InvoiceId")%>' ToolTip="Invoice Correction" CausesValidation="false" Visible='<%#(Eval("Status").ToString().Trim() == "UnPaid" && CancelAllowed && Eval("StageText").ToString().Trim() != "Entry")%>' />
                                                    </td>--%>
                                                    <td>
                                                        <asp:ImageButton runat="server" ID="btnChangeStage" ImageUrl="~/Modules/HRD/Images/Gear.png" OnClick="imgChangeStage_Click" CommandArgument='<%#Eval("InvoiceId")%>' ToolTip="Invoice Correction" CausesValidation="false" Visible='<%#(Eval("Status").ToString().Trim() == "UnPaid" && CancelAllowed && Eval("StageText").ToString().Trim() != "Entry" &&  UserId==1 )%>' />
                                                    </td>
                                                    <td align="left"><a onclick='viewInv(<%#Eval("InvoiceId")%>);' href="#"><%#Eval("RefNo")%></a></td>
                                                    <td align="left"><%#Eval("Vendor")%> <b style='color: Red'><i>( <%#Eval("VendorCode")%> ) </i></b></td>
                                                    <td align="left"><%#Eval("InvNo")%></td>
                                                    <td align="left"><%#Common.ToDateString(Eval("InvDate"))%></td>
                                                    <td align="left"><%#Common.ToDateString(Eval("EnteredOn"))%></td>
                                                    <td align="right"><%#Eval("InvoiceAmount")%></td>
                                                    <td align="left"><%#Eval("Currency")%></td>
                                                    <td align="left"><%#Common.ToDateString(Eval("DueDate"))%></td>
                                                    <td align="left"><%#Eval("INVVesselCode")%> </td>
                                                    <td align="left"><a onclick='PrintVoucher(<%#Eval("PAYMENTID")%>);' href="#" style='<%#(( Eval("Status").ToString().Trim() == "Paid" && Eval("Stage").ToString().Trim() == "3")) ? "": "display:none" %>'><%#Eval("VoucherNo")%></a></td>
                                                    <td align="left"><%#Eval("Status")%></td>
                                                    <td align="left"><%#Eval("StageText")%></td>
                                                    <td align="left"><%#Eval("CurrentUser")%> </td>
                                                    <td style="text-align: center;">
                                                        <img id="Img3" src="~/Modules/HRD/Images/paperclip.gif" onclick="download(this);" invid='<%#Eval("InvoiceId")%>' style='<%#(Eval("AttachmentName").ToString() != "") ? "": "visibility:hidden" %>' title="download attachment" runat="server" alt="" />
                                                    </td>
                                                    <%--<td>&nbsp;</td>--%>
                                                </tr>
                                            </AlternatingItemTemplate>
                                        </asp:Repeater>
                                        <asp:GridView  CellPadding="0" CellSpacing="0" ID="GvMyInvoices" runat="server"  AutoGenerateColumns="False"  Width="98%"  GridLines="horizontal" Visible="false"  >  <%--OnDataBound="SummaryBound"--%>
                                        <Columns>
                                             <asp:BoundField DataField="RefNo" HeaderText="Ref #" >
                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            </asp:BoundField>
                                                <asp:BoundField DataField="Vendor" HeaderText="Vendor" >
                                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="VendorCode" HeaderText="Vendor Code" >
                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="InvNo" HeaderText="Invoice #" >
                                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                                            </asp:BoundField>
                                           <asp:BoundField DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false" DataField="InvDate" HeaderText="Invoice Date" >
                                                <ItemStyle HorizontalAlign="Left" Width="120px"   />
                                            </asp:BoundField>
                                           <asp:BoundField DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false" DataField="EnteredOn" HeaderText="Rec Date" >
                                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="InvoiceAmount" HeaderText="Invoice Amount" >
                                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Currency" HeaderText="Currency" >
                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="TransUSD" HeaderText="Invoice Amount ($)" >
                                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false" DataField="DueDate" HeaderText="Payment Due" >
                                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="INVVesselCode" HeaderText="Vessel" >
                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="VoucherNo" HeaderText="PV #" >
                                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                                            </asp:BoundField>

                                            <asp:BoundField  DataField="Status" HeaderText="Status" >
                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="StageText" HeaderText="Stage" >
                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="CurrentUser" HeaderText="Current User" >
                                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                                            </asp:BoundField>
                                        </Columns>                        

                                        <SelectedRowStyle CssClass="selectedtowstyle" />
                                        <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                        <RowStyle CssClass="rowstyle" />
                                    </asp:GridView>
                                    </table>
                                </div>
                                <br />
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="">
                                            <asp:Label runat="server" ID="lblRecordCount" Style="float: left"></asp:Label>
                                            <asp:Button ID="btnClose" runat="server" Text="Close" Width="150px"
                                                Style="background-color: red; color: White; border: none; padding: 4px;"
                                                OnClientClick="window.close()" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </td>
                        </tr>
                    </table>
                    <!-----------------Cancel Invoice------------------------>
                    <div style="position: absolute; top: 0px; left: 0px; height: 470px; width: 100%;" id="dv_Cancel" runat="server" visible="false">
                        <center>
                            <div style="position: fixed; top: 0px; left: 0px; min-height: 100%; width: 100%; background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
                            <div style="position: relative; width: 600px; height: 330px; padding: 5px; text-align: center; background: white; z-index: 150; top: 180px; border: solid 0px black;">
                                <center>
                                    <div style="padding: 6px;font-size: 14px; " class="text headerband" ><b>Cancel Invoice</b></div>
                                    <div style="width: 100%; text-align: left; overflow-y: scroll; overflow-x: hidden; height: 300px;">
                                        <table cellpadding="3" cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                    <b>Enter Comments..</b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtComments" Width="95%" Height="230px" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <div>
                                            <center>
                                                <asp:Button ID="btnCancelInvoice" runat="server" OnClick="btnCancelInvoice_Click" OnClientClick="this.value='Loading..';" Style=" border: none; padding: 4px;" Text="Cancel Invoice" Width="150px" CssClass="btn" />
                                                <asp:Button ID="btnClosePOP" runat="server" Text="Close" Width="150px" Style=" border: none; padding: 4px;" OnClick="btnClosePOP_Click" CssClass="btn" />
                                            </center>
                                        </div>
                                    </div>
                                </center>
                            </div>
                        </center>
                    </div>

                    <!------------------Update User----------------------->
                    <div style="position: absolute; top: 0px; left: 0px; height: 470px; width: 100%;" id="dvUpdateUser" runat="server" visible="false">
                        <center>
                            <div style="position: fixed; top: 0px; left: 0px; min-height: 100%; width: 100%; background-color: Gray; z-index: 1; opacity: 0.4; filter: alpha(opacity=40)"></div>
                            <div style="position: relative; width: 650px; height: 380px; padding: 5px; text-align: center; background: white; z-index: 2; top: 180px; border: solid 0px black;">
                                <center>
                                    <div style="padding: 6px;font-size: 14px; " class="text headerband"><b>Invoice Correction</b></div>
                                    <div style="width: 100%; text-align: left; overflow-y: scroll; overflow-x: hidden; height: 350px;">
                                        <table cellpadding="5" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="width: 150px">Ref # </td>
                                                <td style="width: 10px">:</td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblRefNo"></asp:Label></td>
                                            </tr>
                                            <tr style="background-color: #E6F3FC">
                                                <td>Vendor </td>
                                                <td>:</td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtSupplier_Update" Width="80%"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="up" runat="server" ControlToValidate="txtSupplier_Update"
                                                        Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    <asp:HiddenField runat="server" ID="hfdSupplierId_Update" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Inv # </td>
                                                <td>:</td>
                                                <td>
                                                    <asp:TextBox ID="lblInvNo" runat="server" Width="110px" Style="border: solid 1px #008AE6;"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr style="background-color: #E6F3FC">
                                                <td>Invoice Date </td>
                                                <td>:</td>
                                                <td>
                                                    <asp:TextBox ID="lblInvDate" runat="server" Width="110px" Style="border: solid 1px #008AE6;"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" PopupPosition="TopRight" TargetControlID="lblInvDate"></asp:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Invoice Amount </td>
                                                <td>:</td>
                                                <td>
                                                    <%--lblInvAmount--%>
                                                    <asp:TextBox runat="server" ID="txt_InvAmount" Width="120px" MaxLength="12" Style="text-align: right"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_InvAmount" Display="Dynamic" ValidationGroup="up" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_InvAmount" ErrorMessage="Till 2 decimal places only." ValidationExpression="[-]?\b\d{1,13}\.?\d{0,2}" Display="Dynamic"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr style="background-color: #E6F3FC">
                                                <td>Approved Amount </td>
                                                <td>:</td>
                                                <td>
                                                    <asp:TextBox ID="txt_ApprovedAmount" runat="server" CssClass="required_box" Width="120px" MaxLength="12" Style="text-align: right"></asp:TextBox>&nbsp;
                              <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_ApprovedAmount" Display="Dynamic" ValidationGroup="up" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                              &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_ApprovedAmount" ErrorMessage="Till 2 decimal places only." ValidationExpression="[-]?\b\d{1,13}\.?\d{0,2}" Display="Dynamic"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Currency </td>
                                                <td>:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddCurrency" runat="server" CssClass="input_box" Width="125px"></asp:DropDownList>&nbsp;
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddCurrency" ValidationGroup="up" InitialValue="0" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr style="background-color: #E6F3FC">
                                                <td>Vessel </td>
                                                <td>:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddl_Vessel" runat="server" CssClass="required_box" Width="80%"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator67" runat="server" ControlToValidate="ddl_Vessel" ValidationGroup="up" InitialValue="0" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Current Stage </td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblStage"></asp:Label></td>
                                            </tr>
                                            <tr style="background-color: #E6F3FC">
                                                <td>Current Status </td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label runat="server" ID="lblStatus"></asp:Label></td>
                                            </tr>
                                            <%--  <tr >
                <td>Current User </td>
                <td>:</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlCurrUser" Width="80%"></asp:DropDownList>
                </td>
                </tr>--%>
                                        </table>
                                        <div>
                                            <br />
                                            <center>
                                                <asp:Button ID="btnUpdateUserSave" runat="server" ValidationGroup="up" OnClick="btnUpdateUserSave_Click" OnClientClick="this.value='Loading..';" Style=" border: none; padding: 4px;" Text="Save" Width="150px" CssClass="btn" />
                                                <asp:Button ID="btnUpdateUserCancel" runat="server" CausesValidation="false" Text="Close" Width="150px" Style=" border: none; padding: 4px;" OnClick="btnUpdateUserCancel_Click" CssClass="btn" />
                                            </center>
                                        </div>
                                    </div>
                                </center>
                            </div>
                        </center>
                    </div>

                    <!-----------------Update Invoice Stage------------------------>
                    <div style="position: absolute; top: 0px; left: 0px; height: 470px; width: 100%;" id="dv_InvoiceStage" runat="server" visible="false">
                        <center>
                            <div style="position: fixed; top: 0px; left: 0px; min-height: 100%; width: 100%; background-color: Gray; z-index: 1; opacity: 0.4; filter: alpha(opacity=40)"></div>
                            <div style="position: relative; width: 600px; padding: 5px; text-align: center; background: white; z-index: 2; top: 180px; border: solid 0px black;">
                                <center>
                                    <div style="padding: 6px; background-color: #00ABE1; font-size: 14px; color: #fff;"><b>Change Invoice Stage</b></div>
                                    <div style="width: 100%; text-align: left; overflow-y: scroll; overflow-x: hidden;">
                                        <table cellpadding="3" cellspacing="5" width="100%">
                                            <tr>
                                                <td style="text-align: center">Change Stage To <b>Processing</b> Stage:</td>
                                            </tr>
                                        </table>
                                        <div>
                                            <center>
                                                <asp:Button ID="btnSaveInvoiceStage" runat="server" OnClick="btnSaveInvoiceStage_Click" OnClientClick="this.value='Loading..';" Style="background-color: #006EB8; color: White; border: none; padding: 4px;" Text="Change Stage" Width="150px" />
                                                <asp:Button ID="btnCloseInvoiceStage" runat="server" Text="Close" Width="150px" Style="background-color: red; color: White; border: none; padding: 4px;" OnClick="btnCloseInvoiceStage_Click" />
                                            </center>
                                        </div>
                                    </div>
                                </center>
                            </div>
                        </center>
                    </div>

                    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
                    <asp:HiddenField ID="hfInvId" runat="server" />
                    <asp:Button ID="btndownload" Text="" OnClick="btnDownloadFile_Click" Style="display: none;" runat="server" />
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
</script>
</html>
