<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VendorLedger.aspx.cs" Inherits="Modules_Purchase_Invoice_VendorSOA" %>

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
                                <strong>Vendor Ledger</strong></td>
                        </tr>
                        <tr>
                            <td style="background-color: #E6F3FC">
                                <table cellpadding="3" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="text-align: right;padding-right:5px;"> Report :</td>
                                        <td style="text-align: left;padding-left:5px;">
                                           <asp:DropDownList ID="ddlReport" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlReport_SelectedIndexChanged"  >
                 <asp:ListItem Text="< Select >" Value="0" Selected="True" ></asp:ListItem>
<asp:ListItem Text="All Vendor Outstanding Summary" Value="3" ></asp:ListItem>
<asp:ListItem Text="Vendor Outstanding Report" Value="1" ></asp:ListItem>
<asp:ListItem Text="Vendor Invoice Payment Details" Value="2" ></asp:ListItem>
                                              
                                           </asp:DropDownList>
                                        </td>
                                        <td style="text-align: right;padding-right:5px;">&nbsp;Vendor : </td>
                                        <td style="text-align: left;padding-left:5px;">
                                            <asp:DropDownList ID="ddlVendor" runat="server" Width="300px" ></asp:DropDownList>
                                        </td>
                                       
                                        <td style="text-align: right;padding-right:5px;">Financial Year  : </td>
                                        <td style="text-align: left;padding-left:5px;">
                                           <asp:DropDownList ID="ddlFinancialYr" runat="server" Width="120px"></asp:DropDownList>
                                        </td>
                                         <td style="text-align: right">&nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td rowspan="4" style="padding: 0px">
                <asp:Button ID="btnExportToExcel" runat="server" Text="Download Excel" CssClass="btn" ToolTip="Export to Excel" OnClick="btnExportToExcel_Click" /> &nbsp;
                                             <asp:Button runat="server" ID="btnClear" text="Clear" OnClick="btnClear_Click" ToolTip="Clear" CssClass="btn"  /> 
                                        </td>
                                    </tr>
                                   
                                  
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                               
                                <div id="dv_grd" style="overflow-y: scroll; overflow-x: hidden; height: 460px; text-align: center; border-bottom: none;" class="ScrollAutoReset">
                                    <table border="1" bordercolor="#F0F5F5" cellpadding="4" cellspacing="0" style="text-align: center; border-collapse: collapse; width: 100%;">
                                       <tr>
                                           <td>
                                               <asp:GridView  CellPadding="0" CellSpacing="0" ID="GVVendorSOA" runat="server"  AutoGenerateColumns="False"  Width="98%"  GridLines="horizontal" Visible="false" OnDataBound="GVVendorSOA_DataBound"  >  <%--OnDataBound="SummaryBound"--%>
                                        <Columns>
                                             <asp:BoundField DataField="TransactionDate" HeaderText="Date" >
                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            </asp:BoundField>
                                                <asp:BoundField DataField="TransactionType" HeaderText="Type" >
                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="TransactionRefNo" HeaderText="Ref #" >
                                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="VesselName" HeaderText="Vessel" >
                                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="TransactionDesc" HeaderText="Description" >
                                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                                            </asp:BoundField>
                                          
                                          
                                            <asp:BoundField DataField="TransactionDebitAmt" HeaderText="Debit" >
                                                <ItemStyle HorizontalAlign="Right" Width="120px" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TransactionCreditAmt" HeaderText="Credit" >
                                                <ItemStyle HorizontalAlign="Right" Width="120px" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="balance" HeaderText="Balance" >
                                                <ItemStyle HorizontalAlign="Right" Width="120px" />
                                                 <HeaderStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TransactionCurr" HeaderText="Currency" >
                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            </asp:BoundField>
                                        </Columns> 
                                        <SelectedRowStyle CssClass="selectedtowstyle" />
                                        <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                        <RowStyle CssClass="rowstyle" />
                                    </asp:GridView>
<asp:GridView  CellPadding="0" CellSpacing="0" ID="GvInvoicePaymentDetails" runat="server"  AutoGenerateColumns="False"  Width="98%"  GridLines="horizontal" Visible="false" OnDataBound="GvInvoicePaymentDetails_DataBound"  >  <%--OnDataBound="SummaryBound"--%>
                                        <Columns>
                                             <asp:BoundField DataField="RefNo" HeaderText="Ref #" >
                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            </asp:BoundField>
                                                <asp:BoundField DataField="InvNo" HeaderText="Inv No." >
                                                <ItemStyle HorizontalAlign="Left" Width="200px" />
                                            </asp:BoundField>
                                             <asp:BoundField DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false" DataField="InvDate" HeaderText="Inv Date" >
                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Type" HeaderText="Type" >
                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="Description" HeaderText="Description" >
                                                <ItemStyle HorizontalAlign="Right" Width="200px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="debitamount" HeaderText="Debit" >
                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="creditamount" HeaderText="Credit" >
                                                <ItemStyle HorizontalAlign="Right" Width="100px" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="Currency" HeaderText="Currency" >
                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="BidPoNum" HeaderText="PO #" >
                                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                                            </asp:BoundField>
                                             <asp:BoundField  DataFormatString="{0:dd-MMM-yyyy}" HtmlEncode="false" DataField="PODATE" HeaderText="PO DATE" >
                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField  DataField="BidStatusName" HeaderText="Status" >
                                                <ItemStyle HorizontalAlign="Left" Width="120px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AccountNumber" HeaderText="Account #" >
                                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="AccountName" HeaderText="Account" >
                                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="IsAdvPayment" HeaderText="Adv. Payment" >
                                                <ItemStyle HorizontalAlign="Left" Width="100px" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="USDChangeTotal" HeaderText="Amount ($)" >
                                                <ItemStyle HorizontalAlign="right" Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ShipName" HeaderText="Vessel" >
                                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                                            </asp:BoundField>
                                             <asp:BoundField DataField="AccountCompany" HeaderText="Account Company" >
                                                <ItemStyle HorizontalAlign="Left" Width="150px" />
                                            </asp:BoundField>
                                        </Columns>                        

                                        <SelectedRowStyle CssClass="selectedtowstyle" />
                                        <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                        <RowStyle CssClass="rowstyle" />
                                    </asp:GridView>
                                               <asp:GridView  CellPadding="0" CellSpacing="0" ID="GvVendorSOASummary" runat="server"  AutoGenerateColumns="False"  Width="60%"  GridLines="horizontal" Visible="false" OnDataBound="GvVendorSOASummary_DataBound"  >  <%--OnDataBound="SummaryBound"--%>
                                        <Columns>
                                             <asp:BoundField DataField="SupplierName" HeaderText="Vendor" >
                                                <ItemStyle HorizontalAlign="Left" Width="300px" />
                                            </asp:BoundField>
                                                <asp:BoundField DataField="DebitAmt" HeaderText="Debit" >
                                                <ItemStyle HorizontalAlign="Right" Width="120px" />
                                            </asp:BoundField>
                                             <asp:BoundField  DataField="CreditAmt" HeaderText="Credit" >
                                                <ItemStyle HorizontalAlign="Right" Width="120px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="bal" HeaderText="Balance" >
                                                <ItemStyle HorizontalAlign="Right" Width="120px" />
                                            </asp:BoundField>
                                        </Columns> 
                                        <SelectedRowStyle CssClass="selectedtowstyle" />
                                        <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                        <RowStyle CssClass="rowstyle" />
                                    </asp:GridView>
                                           </td>
                                       </tr> 
                                        
                                        
                                    </table>
                                </div>
                                <br />
                                
                               
                            </td>
                        </tr>
                    </table>
                    
                   
                   

                  
                   
                </div>
            </center>
        </div>
    </form>
</body>
</html>
