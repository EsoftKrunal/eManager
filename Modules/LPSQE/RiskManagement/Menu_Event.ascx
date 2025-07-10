<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Menu_Event.ascx.cs" Inherits="UserControls_Left" %>
 <link rel="Stylesheet" href="../../HRD/Styles/StyleSheet.css" />
<style type="text/css">
.selbtn
{
	background-color :#669900;
	color :White;
	border :none;
    padding:5px 10px 5px 10px;

	
}
.btn1
{
	background-color :#c2c2c2;
	border:solid 1px gray;
	border :none;
	padding:5px 10px 5px 10px;
    
}
</style>
<div id="submenu" style="border-bottom:solid 0px #0099CC">
<%--<ul style="margin:0px; padding:0px; height:28px;font-family:Arial;font-size:12px;">
<li runat="server" id="tr_RiskManagement" style="margin:0px 5px; padding:0px;float:left; text-align:center;">
	<a ID="btnRiskMangement" runat="server" onserverclick="RedirectToPage" ModuleId="RiskAnalysis" style="width:135px;padding:7px 0px 7px ;">Risk Management</a></li> &nbsp;&nbsp;
<li runat="server" id="Li1" style="margin:0px 5px; padding:0px;float:left; text-align:center;"><a ID="btnTemplateMaster" runat="server" onserverclick="RedirectToPage" ModuleId="TemplateMaster" style="width:135px;padding:7px 0px 7px ;">Templates</a></li> &nbsp;&nbsp;
<li runat="server" id="tr_HazardMaster" style="margin:0px 5px; padding:0px;float:left; text-align:center;"><a ID="btnHazardsMaster" runat="server" onserverclick="RedirectToPage" ModuleId="HazardMaster" style="padding:7px 0px 7px ;">Master</a></li> &nbsp;&nbsp;
<li runat="server" id="tr_Export" style="margin:0px 5px; padding:0px;float:left; text-align:center;"><a ID="btnExport" runat="server" onserverclick="RedirectToPage" ModuleId="Export" style="padding:7px 0px 7px ;">Export to Ship</a></li> &nbsp;&nbsp;
<li class="clear"></li>
</ul>--%> &nbsp;
	<asp:Button ID="btnRiskMangement" runat="server"  Text="Risk Assessment" CssClass="btn1" OnClick="btnRiskMangement_Click" /> &nbsp;
	<asp:Button ID="btnTemplateMaster" runat="server"  Text="Templates" CssClass="btn1" OnClick="btnTemplateMaster_Click"/> &nbsp;
	<asp:Button ID="btnHazardsMaster" runat="server"  Text="Master" CssClass="btn1" OnClick="btnHazardsMaster_Click"/> &nbsp;
	<asp:Button ID="btnExport" runat="server"  Text="Export to Ship" CssClass="btn1" OnClick="btnExport_Click"/> &nbsp;
</div>
