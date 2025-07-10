<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HeaderMenu.ascx.cs" Inherits="UserControls_HeaderMenu" %>
<script type="text/javascript">
function LogOut()
{
alert('Your Session is Expired. Please Login Again.'); 
var loc=window.parent.parent.location.toString().lastIndexOf("/");
var lft=window.parent.parent.location.toString().substr(0,loc);
window.parent.parent.location=lft + '/login.aspx';
}
</script>
<div style="text-align: left">
    <table width="100%" cellspacing="0" cellpadding="0" runat="server" id="tab_Header" style="display: none">
        <tr>
            <td id="tdImg" runat="server" style="height: 96px; background-image: url(Images/header_bg.jpg);">
                <img runat="server" src="~/Images/logo.jpg" style="float: left" />
                <div style="float: left; margin-top: 60px;">
                    <asp:Label ID="lblUser" Style="font-size: 15px; font-weight: bold;" runat="server"></asp:Label>
                </div>
                <%--<img src="Images/HeaderMenu/pms_icon.jpg" alt="PMS" style="float:right" />--%> 
            </td>
            <td id="tdPmsIcon" runat="server" style="height: 96px; width: 344px; background-image: url(Images/pms_icon.jpg); text-align: right">
                <asp:Label runat="server" ID="lblVessel" Style="float: right; padding-right: 82px; margin-top: 60px;" ForeColor="#19357E" Font-Size="13" Font-Names="Verdana" Font-Bold="true"></asp:Label>
            </td>
        </tr>
    </table>
    <div style="width: 100%; text-align: center">
        <div style="float: left">
            <table border="1" bordercolor="gray" style="border-collapse: collapse;">
                <tr>
                    <td runat="server" id="tr_jobplanning">
                        <asp:ImageButton ID="btnplanning" runat="server" CausesValidation="false" ImageUrl="~/Modules/PMS/Images/HeaderMenu/job_planning_g.jpg" OnClick="menu_Click" CommandArgument="1" BorderStyle="None"></asp:ImageButton></td>
                    <%--<td runat="server" id="tr_jobupdate"><asp:ImageButton ID="btnUpdate" runat="server" ImageUrl="~/Images/HeaderMenu/job_update_g.jpg" OnClick="menu_Click" CommandArgument="7" BorderStyle="None"></asp:ImageButton></td>--%>
                    <td runat="server" id="tr_shiprunninghr">
                        <asp:ImageButton ID="btnrunninghr" runat="server" CausesValidation="false" ImageUrl="~/Modules/PMS/Images/HeaderMenu/runing_hour_g.jpg" OnClick="menu_Click" CommandArgument="7" BorderStyle="None"></asp:ImageButton></td>
                    <td runat="server" id="tr_btnshipmaster">
                        <asp:ImageButton ID="btnshipmaster" runat="server" CausesValidation="false" ImageUrl="~/Modules/PMS/Images/HeaderMenu/shipmaster_g.jpg" OnClick="menu_Click" CommandArgument="2" BorderStyle="None"></asp:ImageButton></td>
                    <td runat="server" id="tr_defectreport">
                        <asp:ImageButton ID="btnDefectReport" runat="server" CausesValidation="false" ImageUrl="~/Modules/PMS/Images/HeaderMenu/defect_reporting_g.jpg" OnClick="menu_Click" CommandArgument="8" BorderStyle="None"></asp:ImageButton></td>

                    <td runat="server" id="tr_btnofficemaster">
                        <asp:ImageButton ID="btnofficemaster" runat="server" CausesValidation="false" ImageUrl="~/Modules/PMS/Images/HeaderMenu/officemaster_g.jpg" OnClick="menu_Click" CommandArgument="4" BorderStyle="None"></asp:ImageButton></td>
                    <td runat="server" id="tr_btnvesselsetup">
                        <asp:ImageButton ID="btnvesselsetup" runat="server" CausesValidation="false" ImageUrl="~/Modules/PMS/Images/HeaderMenu/vesselsetup_g.jpg" OnClick="menu_Click" CommandArgument="5" BorderStyle="None"></asp:ImageButton></td>
                    <td runat="server" id="tr_btnreports">
                        <asp:ImageButton ID="btnreports" runat="server" CausesValidation="false" ImageUrl="~/Modules/PMS/Images/HeaderMenu/reports_g.jpg" OnClick="menu_Click" CommandArgument="6" BorderStyle="None"></asp:ImageButton></td>
                    <td runat="server" id="tr_btnRegisters">
                        <asp:ImageButton ID="btnRegisters" runat="server" CausesValidation="false" ImageUrl="~/Modules/PMS/Images/HeaderMenu/register_g.jpg" OnClick="menu_Click" CommandArgument="3" BorderStyle="None"></asp:ImageButton></td>
                    <td runat="server" id="tr_AdminDelete">
                        <asp:ImageButton ID="btnAdmindelete" runat="server" CausesValidation="false" ImageUrl="~/Modules/PMS/Images/HeaderMenu/maintainance_g.jpg" OnClick="menu_Click" CommandArgument="9" BorderStyle="None"></asp:ImageButton></td>
                    <%--<td runat="server" id="tr_MF"><asp:ImageButton ID="btnMF" runat="server" CausesValidation="false" ImageUrl="~/Modules/PMS/Images/HeaderMenu/manuls_forms_b.jpg" OnClick="menu_Click" CommandArgument="10" BorderStyle="None"></asp:ImageButton></td>--%>
                    <%--<td runat="server" id="tr_VIMS"><asp:ImageButton ID="btnVIMS" runat="server" CausesValidation="false" ImageUrl="~/Modules/PMS/Images/HeaderMenu/vims_g.jpg" OnClick="menu_Click" CommandArgument="11" BorderStyle="None"></asp:ImageButton></td>--%>
                    <%--<td runat="server" id="tr_MenuPlanner"><asp:ImageButton ID="btnMenuPlanner" runat="server" CausesValidation="false" ImageUrl="~/Modules/PMS/Images/HeaderMenu/MenuPlanner_g.jpg" OnClick="menu_Click" CommandArgument="12" BorderStyle="None"></asp:ImageButton></td>--%>
                    <%--<td runat="server" id="tr_Purchase"><asp:ImageButton ID="btnPurchase" runat="server" CausesValidation="false" ImageUrl="~/Modules/PMS/Images/HeaderMenu/purchase_g.jpg" OnClick="menu_Click" CommandArgument="13" BorderStyle="None"></asp:ImageButton></td>--%>
                    <td runat="server" id="tr_DryDock">
                        <asp:ImageButton ID="btnDryDock" runat="server" CausesValidation="false" ImageUrl="~/Modules/PMS/Images/HeaderMenu/drydocking_g.jpg" OnClick="menu_Click" CommandArgument="14" BorderStyle="None"></asp:ImageButton></td>
                    <td runat="server" id="tr_RiskMangement">
                        <asp:ImageButton ID="btnRiskMangement" runat="server" CausesValidation="false" ImageUrl="~/Modules/PMS/Images/HeaderMenu/riskmgmt_g.jpg" OnClick="menu_Click" CommandArgument="15" BorderStyle="None"></asp:ImageButton></td>
                </tr>
                <tr>
                    <td runat="server" id="tr_SpareManagement">
                        <asp:ImageButton ID="btnSpareManagement" runat="server" CausesValidation="false" ImageUrl="~/Modules/PMS/Images/HeaderMenu/sparemgmt_g.jpg" OnClick="menu_Click" CommandArgument="16" BorderStyle="None"></asp:ImageButton></td>
                    <td runat="server" id="tr_StoreManagement" colspan="10">
                        <asp:ImageButton ID="btnStoreManagement" runat="server" CausesValidation="false" ImageUrl="~/Modules/PMS/Images/HeaderMenu/storemgmt_g.jpg" OnClick="menu_Click" CommandArgument="17" BorderStyle="None"></asp:ImageButton></td>
                    <%--<td runat="server" id="tr_MRV"><asp:ImageButton ID="btn_MRV" runat="server" CausesValidation="false" ImageUrl="~/Images/HeaderMenu/storemgmt_g.jpg" OnClick="menu_Click" CommandArgument="18" BorderStyle="None"></asp:ImageButton></td>--%>
                </tr>
            </table>
        </div>
        <%--<div style="float: right; padding: 3px 10px 0px 0px;">--%>
        <div style="float: right; padding: 20px 15px 0px 0px;">
            <asp:ImageButton runat="server" ID="btnSettings" CausesValidation="false" ImageUrl="~/Modules/PMS/Images/settings.png" ToolTip="Settings" OnClick="btnSettings_Click" />
            <asp:ImageButton runat="server" ID="btnHome" CausesValidation="false" ImageUrl="~/Modules/PMS/Images/home_24x24.png" ToolTip="Home" />
            <%--<asp:ImageButton runat="server" ID="btnLogOut" CausesValidation="false" ImageUrl="~/Images/Logout.png" ToolTip="Logout" onclick="btnLogOut_Click"/>   --%>
        </div>
    </div>
</div>

