<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Header.aspx.cs" Inherits="SMS_Header" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link rel="Stylesheet" href="CSS/style.css" />
</head>
<body style="margin:0px 0px 0px 0px">
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server" id="ScriptManager1"></asp:ScriptManager>
    <div style=" text-align :left">
<table width="100%" cellspacing="0" cellpadding="0" runat="server" id="tab_Header" >
<tr>
<td style="height :96px; background-image :url(/MtmPms/Images/header_bg.jpg); " >
<img src="~/Images/logo.jpg" id="img1" style="float:left" runat="server" />
<div style="float:left;  margin-top:60px; ">
 <asp:Label ID="lblUser" style=" font-size:15px; font-weight:bold;" runat="server"></asp:Label>
</div>
<%--<img src="Images/HeaderMenu/pms_icon.jpg" alt="PMS" style="float:right" />--%> 
</td>
<td style="height :96px; width:344px; background-image :url(/MtmPms/Images/pms_icon.jpg); text-align:right " >
<asp:Label runat="server" ID="lblVessel" style=" float:right; padding-right:82px; margin-top:60px;" ForeColor="#19357E" Font-Size="13" Font-Names="Verdana" Font-Bold="true"></asp:Label>
</td>
</tr>
</table>
<div style ="width :100%; text-align :center ">
<div style="float:left">
<table border="1" bordercolor="gray" style=" border-collapse:collapse;">
<tr>
<td runat="server" id="tr_jobplanning"><asp:ImageButton ID="btnplanning" runat="server" CausesValidation="false" ImageUrl="~/Images/HeaderMenu/job_planning_g.jpg" OnClick="menu_Click" CommandArgument="1" BorderStyle="None"></asp:ImageButton></td>
<td runat="server" id="tr_shiprunninghr"><asp:ImageButton ID="btnrunninghr" runat="server" CausesValidation="false" ImageUrl="~/Images/HeaderMenu/runing_hour_g.jpg" OnClick="menu_Click" CommandArgument="7" BorderStyle="None"></asp:ImageButton></td>
<td runat="server" id="tr_btnshipmaster"><asp:ImageButton ID="btnshipmaster" runat="server" CausesValidation="false" ImageUrl="~/Images/HeaderMenu/shipmaster_g.jpg" OnClick="menu_Click" CommandArgument="2" BorderStyle="None"></asp:ImageButton></td>
<td runat="server" id="tr_defectreport"><asp:ImageButton ID="btnDefectReport" runat="server" CausesValidation="false" ImageUrl="~/Images/HeaderMenu/defect_reporting_g.jpg" OnClick="menu_Click" CommandArgument="8" BorderStyle="None"></asp:ImageButton></td>

<td runat="server" id="tr_btnofficemaster"><asp:ImageButton ID="btnofficemaster" runat="server" CausesValidation="false" ImageUrl="~/Images/HeaderMenu/officemaster_g.jpg" OnClick="menu_Click" CommandArgument="4" BorderStyle="None"></asp:ImageButton></td>
<td runat="server" id="tr_btnvesselsetup"><asp:ImageButton ID="btnvesselsetup" runat="server" CausesValidation="false" ImageUrl="~/Images/HeaderMenu/vesselsetup_g.jpg" OnClick="menu_Click" CommandArgument="5" BorderStyle="None"></asp:ImageButton></td>
<td runat="server" id="tr_btnreports"><asp:ImageButton ID="btnreports" runat="server" CausesValidation="false" ImageUrl="~/Images/HeaderMenu/reports_g.jpg" OnClick="menu_Click" CommandArgument="6" BorderStyle="None"></asp:ImageButton></td>
<td runat="server" id="tr_btnRegisters"><asp:ImageButton ID="btnRegisters" runat="server" CausesValidation="false" ImageUrl="~/Images/HeaderMenu/register_g.jpg" OnClick="menu_Click" CommandArgument="3" BorderStyle="None"></asp:ImageButton></td>
<td runat="server" id="tr_AdminDelete"><asp:ImageButton ID="btnAdmindelete" runat="server" CausesValidation="false" ImageUrl="~/Images/HeaderMenu/maintainance_g.jpg" OnClick="menu_Click" CommandArgument="9" BorderStyle="None"></asp:ImageButton></td>
<td runat="server" id="tr_MF"><asp:ImageButton ID="btnMF" runat="server" CausesValidation="false" ImageUrl="~/Images/HeaderMenu/manuls_forms_b.jpg" OnClick="menu_Click" CommandArgument="10" BorderStyle="None"></asp:ImageButton></td>
</tr>
</table> 
</div>
<div style="float:right; padding:3px 10px 0px 0px;">
<asp:ImageButton runat="server" ID="btnHome" CausesValidation="false" ImageUrl="~/Images/home_24x24.png" ToolTip="Home" />
<asp:ImageButton runat="server" ID="btnLogOut" CausesValidation="false" ImageUrl="~/Images/Logout.png" ToolTip="Logout" onclick="btnLogOut_Click"/>   
</div>
</div>
</div>
<br /><br />
<div style="width:100%; padding:5px; background-color:#e2e2e2; text-align:center">
<b>SAFETY MANAGEMENT SYSTEM MANUAL</b>
</div>

</form>
</body>
</html>
