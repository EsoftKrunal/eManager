<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InvoiceStatus.ascx.cs" Inherits="UserControls_InvoiceStatus" %>
<table cellpadding="1" cellspacing="0" width="100%" style=" text-align:center; font-size:12px; font-family:Verdana" border="1" bordercolor="black" >
    <tr>
        <td style="width: 20%">
            Invoice Entry</td>
        <td style="width:20%">Accts Verification I</td>
        <td style="width:20%">Approval</td>
        <td style="width:20%">Accts Verification II</td>
        <td style="width:20%">Payment</td>
    </tr>
    <tr  style=" font-weight:bold">
        <td>
            <asp:Label ID="lblcreatedUser" runat="server" ForeColor="White" Width="100%"></asp:Label></td>
        <td><asp:Label ID="lbl_Verify1" ForeColor="white" runat="server" Text="" Width="100%"></asp:Label></td>
        <td><asp:Label ID="lbl_Approval" ForeColor="white" runat="server" Text="" Width="100%"></asp:Label></td>
        <td><asp:Label ID="lbl_Verify2" ForeColor="white" runat="server" Text="" Width="100%"></asp:Label></td>
        <td><asp:Label ID="lbl_Payment" ForeColor="white" runat="server" Text="" Width="100%"></asp:Label></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblCreatedOn" runat="server" Width="100%"></asp:Label></td>
        <td><asp:Label ID="lbl_Date_Verfy1" runat="server" Text="" Width="100%"></asp:Label></td>
        <td><asp:Label ID="lbl_Date_Approval" runat="server" Text="" Width="100%"></asp:Label></td>
        <td><asp:Label ID="lbl_Date_Verfy2" runat="server" Text="" Width="100%"></asp:Label></td>
        <td><asp:Label ID="lbl_Date_Payment" runat="server" Text="" Width="100%"></asp:Label></td>
    </tr>
</table>
