<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MessageBox.ascx.cs" Inherits="UserControls_MessageBox" %>
<center>
<div style="float :left; height :20px;position:relative; padding-left :15px; background-repeat: repeat-x" runat="server" id="dvBox"  >
<div style="float :right; height :20px;background-repeat: repeat-x;width:13px; z-index :101;"></div>
<div style="float :left; height :20px;background-repeat: repeat-x;width:15px; z-index :1;position :absolute;top:0;left:0px;"></div>
<div style=" padding-top :5px;" >
<div id="dvSuccess" runat="server" style="border :none; height :15px;" >
<img src="~/images/success.png" alt="" style="float :left;" runat="server"/> 
<asp:Label runat="server" ID="LblSMsg" Font-Size="12px" ForeColor="Green" style="float :left; padding-left :3px;"></asp:Label> 
</div> 
<div id="dvError" runat="server" style="border :none; height :15px;" >
<img src="~/Images/error.png" alt="" style="float :left; " runat="server" />
<asp:Label runat="server" ID="lblEMsg" Font-Size="12px" ForeColor="Red" style="float :left;padding-left :3px;"></asp:Label> 
</div>
</div>
</div>
</center>


