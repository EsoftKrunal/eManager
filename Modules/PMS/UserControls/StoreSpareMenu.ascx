<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StoreSpareMenu.ascx.cs" Inherits="VIMS_StoreSpareMenu" %>
<style type="text/css">
.newbtnsel
{
    background-color: #1584af;
    border: none;
    color: white;
    padding: 5px;
    font-family: Arial;
    font-size: 12px;
}
.newbtn
{
    background-color: #9ad7ef;
    border: none;
    color: #333;
    padding: 5px;
    font-family: Arial;
    font-size: 12px;
}
</style> 
<table style="width:100%;" cellpadding="0" cellspacing="0">
<tr>
<td style="width:100px;">
<asp:Button ID="btn_SpareMgmt" runat="server" Text="Spare Mgmt." CssClass="newbtnsel" style="border-right:solid 5px white;" Width="100%" OnClick="Menu_Click" CommandArgument="0" />
</td>
<td style="width:130px;">
<asp:Button ID="btn_Store" runat="server" Text="Store Mgmt." CssClass="newbtn" style="border-right:solid 5px white;" Width="100%" OnClick="Menu_Click"  CommandArgument="1"/>
</td>
<td style="width:180px;">
<asp:Button ID="btn_UncategorisedStoreItem" runat="server" Text="Uncategorised Store Items" CssClass="newbtn" style="border-right:solid 5px white;" Width="100%" OnClick="Menu_Click"  CommandArgument="2"/>
</td>

<td>
&nbsp;
</td>
</tr>
</table>
<div style=" background-color:#1584af; height:4px"></div>