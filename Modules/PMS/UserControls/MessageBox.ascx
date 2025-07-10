<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MessageBox.ascx.cs" Inherits="PMS_UserControls_MessageBox" %>
<center>
<div style="width:100%;" runat="server" id="dvBox" >
<%--<div style="float :right; height :20px;background-repeat: repeat-x;width:13px; z-index :101;"></div>
<div style="float :left; height :20px;background-repeat: repeat-x;width:15px; z-index :1;position :absolute;top:0;left:0px;"></div>--%>
<div style=" padding-top :2px;" >
<div id="dvSuccess" runat="server" style="border:none; background-color:#99E699; text-align:center; padding:6px; width:100%; " >
    <img src="~/Modules/PMS/images/success.png" alt="" style="float :left;" runat="server"/> 
    <asp:Label runat="server" ID="LblSMsg" Font-Size="12px" style="float :left; padding-left :3px;"></asp:Label> 
    <div style="clear:both"></div>
</div> 
<div id="dvError" runat="server" style="border :none; background-color:#FF8585;text-align:center;padding:6px;width:100%; " >
    <img src="~/Modules/PMS/Images/error.png" alt="" style="float :left; " runat="server" />
    <asp:Label runat="server" ID="lblEMsg" Font-Size="12px" style="float :left;padding-left :3px;"></asp:Label> 
    <div style="clear:both"></div>
</div>
</div>
</div>
</center>


