<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddRequisitionTypes.ascx.cs" Inherits="UserControls_AddRequisitionTypes" %>
<asp:RadioButtonList ID="rdbAddReqTypes" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbAddReqTypes_SelectedIndexChanged" AutoPostBack="True">
    <asp:ListItem Value="2" Selected="True">Spares</asp:ListItem>
    <asp:ListItem Value="1">Stores</asp:ListItem>
    <asp:ListItem Value="3">Services</asp:ListItem>
</asp:RadioButtonList>