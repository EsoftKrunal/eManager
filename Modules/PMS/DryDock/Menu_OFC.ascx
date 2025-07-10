<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Menu_OFC.ascx.cs" Inherits="UserControls_Left" %>
<div style="text-align : center;background-color : #F0F8FF;width:100%;vertical-align : top;">
<div style="float:left; width:100%;" id="submenu">
<ul style="margin:0px; padding:0px;">
<li runat="server" id="dv0" style="margin:0px; padding:0px;"><a ID="btnHome" runat="server" onserverclick="RedirectToPage" ModuleId="DD_OFC_Tracker" style="width:195px;padding:10px 0px 10px ;" >DD Tracker </a></li>
<li runat="server" id="dv2" style="margin:0px; padding:0px;float:left;width:150px;"><a ID="btnDocket" runat="server" onserverclick="RedirectToPage" ModuleId="DD_OFC_Docket" style="width:150px;padding:10px 0px 10px ;">DD Mgmt</a></li>
<li runat="server" id="dv1" style="margin:0px; padding:0px;float:left; width:150px;margin-left:5px;"><a ID="btnJobMaster" runat="server" onserverclick="RedirectToPage" ModuleId="DD_OFC_JobMaster" style="width:150px;padding:10px 0px 10px ;">DD Master</a></li>
<li runat="server" id="dv3" style="margin:0px; padding:0px;float:left; width:150px;margin-left:5px;"><a ID="btnDDPlanSettings" runat="server" onserverclick="RedirectToPage" ModuleId="DD_OFC_PlanSettings" style="width:150px;padding:10px 0px 10px ;">Planner Settings</a></li>
<li class="clear" style='display:none'></li>
</ul>
</div>
<div style=" float:left;background-color:#0099CC; height:5px; width:100%">&nbsp;</div>
</div>