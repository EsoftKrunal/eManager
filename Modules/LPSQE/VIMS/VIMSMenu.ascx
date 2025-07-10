<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VIMSMenu.ascx.cs" Inherits="VIMS_VIMSMenu" %>
<style type="text/css">
.newbtnsel
{
    background-color:Orange;
    border:none;
    padding:3px;
    font-family:Arial;
    font-size:12px;
}
.newbtn
{
      background-color:#e2e2e2;
      border:none;
      padding:3px;
      font-family:Arial;
      font-size:12px;
}
</style> 
<table style="width:100%;" cellpadding="0" cellspacing="0">
<tr>
<td style="width:100px;">
<asp:Button ID="btn_Inspections" runat="server" Text="Inspections" CssClass="newbtnsel" style="border-right:solid 5px white;" Width="100%" OnClick="Menu_Click" CommandArgument="0" />
</td>
<td style="width:130px;">
<asp:Button ID="btnVIQ" runat="server" Text="Vetting Prep." CssClass="newbtn" style="border-right:solid 5px white;" Width="100%" OnClick="Menu_Click"  CommandArgument="1"/>
</td>
<%--<td style="width:80px;">
<asp:Button ID="btnLFI" runat="server" Text="LFI / LET" CssClass="newbtn" style="border-right:solid 5px white;" Width="100%" OnClick="Menu_Click"  CommandArgument="2"/>
</td>
<td style="width:150px;">
<asp:Button ID="btnFC" runat="server" Text="Focussed Campaign" CssClass="newbtn" style="border-right:solid 5px white;" Width="100%" OnClick="Menu_Click"  CommandArgument="3"/>
</td>
<td style="width:80px;">
<asp:Button ID="btnRegulation" runat="server" Text="Regulation" CssClass="newbtn" style="border-right:solid 5px white;" Width="100%" OnClick="Menu_Click"  CommandArgument="4"/>
</td>
<td style="width:80px;">
<asp:Button ID="btnCircular" runat="server" Text="Circular" CssClass="newbtn" style="border-right:solid 5px white;" Width="100%" OnClick="Menu_Click"  CommandArgument="5"/>
</td>--%>
<td style="width:130px; ">
<asp:Button ID="btnNavigationAudit" runat="server" Text="Navigation Audit" CssClass="newbtn" style="border-right:solid 5px white;" Width="100%" OnClick="Menu_Click"  CommandArgument="6"/>
</td>
<td style="width:130px;" runat=server Visible="false">
<asp:Button ID="btnPR" runat="server" Text="Position Report" CssClass="newbtn" style="border-right:solid 5px white;" Width="100%" OnClick="Menu_Click"  CommandArgument="7"/>
</td>
<td style="width:150px;">
<asp:Button ID="btnPRNew" runat="server" Text="Position Report New" CssClass="newbtn" style="border-right:solid 5px white; font-weight:bold;" Width="100%" OnClick="Menu_Click"  CommandArgument="11"/>
</td>
<td style="width:80px;">
<asp:Button ID="btnMWUC" runat="server" Text="MWUC" CssClass="newbtn" style="border-right:solid 5px white;" Width="100%" OnClick="Menu_Click"  CommandArgument="8"/>
</td>
<td style="width:80px;">
<asp:Button ID="btnSCM" runat="server" Text="SCM" CssClass="newbtn" style="border-right:solid 5px white;" Width="100%" OnClick="Menu_Click"  CommandArgument="9"/>
</td>
<%--<td style="width:130px;">
<asp:Button ID="btnRisk" runat="server" Text="Risk Management" CssClass="newbtn" style="border-right:solid 5px white;" Width="100%" OnClick="Menu_Click"  CommandArgument="10"/>
</td>--%>
<td>
&nbsp;
</td>
</tr>
</table>
<div style=" background-color:Orange; height:4px"></div>