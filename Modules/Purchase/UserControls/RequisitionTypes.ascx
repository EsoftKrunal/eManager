<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RequisitionTypes.ascx.cs" Inherits="UserControls_RequisitionTypes" %>
<asp:RadioButtonList ID="rdbReqTypes" runat="server" RepeatDirection="Horizontal"  OnSelectedIndexChanged="rdbReqTypes_SelectedIndexChanged" AutoPostBack="True">
    <asp:ListItem Value="2" Selected="True">Spares</asp:ListItem>
    <asp:ListItem Value="1">Stores</asp:ListItem>
    <asp:ListItem Value="3">Services</asp:ListItem> 
</asp:RadioButtonList>
