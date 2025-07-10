<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RegisterMenu.ascx.cs" Inherits="UserControls_RegisterMenu" %>
<div style="padding:3px; background-color:#e2e2e2">
<asp:Button runat="server" ID="btnManuals" OnClick="btnManuals_Click" Text="Manual Master" CssClass="tab" style="padding:2px 10px 2px 10px;" CausesValidation="false"/>
<asp:Button runat="server" ID="btnManCat" OnClick="btnManualCat_Click" Text="Manual Category" CssClass="tab" style="padding:2px 10px 2px 10px;" CausesValidation="false"/>
<asp:Button runat="server" ID="btnForms" OnClick="btnForms_Click" Text="Forms" CssClass="tab" style="padding:2px 10px 2px 10px;"  CausesValidation="false" Visible="false"/>
</div> 
