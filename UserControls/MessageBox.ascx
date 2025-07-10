<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MessageBox.ascx.cs" Inherits="UserControls_MessageBox" %>
<center>
<div id="dvSuccess" runat="server" style="width:500px;border :dotted 1px green; height :40px;vertical-align :middle;" >
<img src="../images/success.png" alt="" style="float :left;"/> 
<asp:Label runat="server" ID="LblSMsg" ForeColor="Green" style="float :left;position :relative ; top:38%;"></asp:Label> 
</div> 
<div id="dvError" runat="server" style="width:500px; border :dotted 1px red; height :40px;vertical-align :middle" >
<img src="../images/error.png" alt="" style="float :left; " />
<asp:Label runat="server" ID="lblEMsg" ForeColor="Red" style="float :left;position :relative ; top:38%;"></asp:Label> 
</div> 
</center> 