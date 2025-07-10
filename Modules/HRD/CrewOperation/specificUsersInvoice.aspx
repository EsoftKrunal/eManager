<%@ Page Language="C#" AutoEventWireup="true" CodeFile="specificUsersInvoice.aspx.cs" Inherits="CrewAccounting_specificUsersInvoice" MasterPageFile="~/Modules/HRD/CrewPlanning.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link href="../styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
</head>
<body style="text-align:center">
    <form id="form1" runat="server">--%>
    <table>
    <tr><td>
    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
        padding-bottom: 5px; border-left: #8fafdb 1px solid; padding-top: 5px; border-bottom: #8fafdb 1px solid">
        <legend><strong>Invoice List</strong></legend>
        <asp:Label ID="Label1" runat="server"></asp:Label><br />
        <div style="overflow-y: scroll; overflow-x: hidden; width: 800px; height: 150px">
            <asp:GridView ID="gvinvoice" runat="server"  AutoGenerateColumns="False" GridLines="Horizontal" Style="text-align: center"
                Width="97%">
                <Columns>
                <asp:CommandField ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif"
                                    ShowSelectButton="True">
                                    <ItemStyle Width="35px" />
                                </asp:CommandField>
                    <asp:BoundField DataField="Ref.No." HeaderText="Payment Ref. #">
                        <ItemStyle HorizontalAlign="Left" Width="110px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Vendor" HeaderText="Vendor">
                        <ItemStyle HorizontalAlign="Left" Width="110px" />
                    </asp:BoundField>
                     <asp:BoundField DataField="ChqTTAmt" HeaderText="Chq/TT Amt">
                        <ItemStyle HorizontalAlign="Left" Width="110px" />
                    </asp:BoundField>
                     <asp:BoundField DataField="ChqTTDt." HeaderText="Chq/TT Dt.">
                        <ItemStyle HorizontalAlign="Left" Width="110px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Status" HeaderText="Status">
                        <ItemStyle HorizontalAlign="Left" Width="110px" />
                    </asp:BoundField>
                </Columns>
                <RowStyle CssClass="rowstyle" />
                <SelectedRowStyle CssClass="selectedtowstyle" />
                <HeaderStyle CssClass="headerstylefixedheadergrid" />
            </asp:GridView>
        </div>
    </fieldset>
    </td></tr>
    <tr><td style="padding-bottom: 10px; padding-top: 15px" align="right">
        <asp:Button ID="btnprintVoucher" runat="server" CssClass="btn" OnClick="btn_printVoucher_Click"
            TabIndex="1" Text="Print Voucher" Width="93px" /></td></tr>
    </table>
    </asp:Content>
   <%-- </form>
</body>
</html>--%>
