<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ManualMenu.ascx.cs" Inherits="UserControls_ManualMenu" %>
<div style=" padding:3px; background-color:#c2c2c2; text-align:center; border-bottom:solid 1px black;">
<b style="color:Blue; margin-left:300px;">SAFETY MANAGEMENT SYSTEM MANUAL</b>
</div>
<div style=" padding:3px; background-color:#c2c2c2; text-align:center; height:22px;">
<asp:Button runat="server" ID="btnReadmanual" OnClick="btnRead_Click" Text="Manual & Forms" CssClass="tab" style="padding:2px 10px 2px 10px;; float:left;" />
<asp:Button runat="server" ID="btnManual" OnClick="btnRM_Click" Text="SMS Mgmt" CssClass="tab"  style="padding:2px 10px 2px 10px;; float:left; margin-left:5px;"/>
<asp:Button runat="server" ID="btnApproval" OnClick="btnApproval_Click" Text="Approval" CssClass="tab" style="padding:2px 10px 2px 10px;; float:left;margin-left:5px;" />

<asp:Button runat="server" ID="btnInviteComments" OnClick="btnInviteComments_Click" Text="Invite Comments" CssClass="tab" style="padding:2px 10px 2px 10px;; float:left;margin-left:5px;display:none;" />

<asp:Button runat="server" ID="btnCommunication" OnClick="btnCommunication_Click" Text=" Communication " CssClass="tab" style="padding:2px 10px 2px 10px;; float:left;margin-left:5px;" />

<asp:Button runat="server" ID="btnRegister" OnClick="btnRegister_Click" Text="Registers" CssClass="tab" style="padding:2px 10px 2px 10px;; float:left;margin-left:5px;" />
<asp:ImageButton runat="server" ID="btnBack" PostBackUrl="~/Transactions/InspectionSearch.aspx?Sesstion=Clear" ImageUrl="~/Images/home.png" style="float:right;margin-left:5px;" />

</div> 
