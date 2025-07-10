<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PortCallReport3Header.aspx.cs" Inherits="PortCallReport3Header" Title="Crew On Vessel Report" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>Untitled Page</title>

<link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
 <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
<link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server" defaultbutton="Button1" >
<div style="text-align: center">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<table style="width :100%;font-family:Arial;font-size:12px;" cellpadding="0" cellspacing="0">
<tr>
<td style=" text-align :left; vertical-align : top;" > 
<table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
<tr>
<td align="center" valign="top" >
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;
            border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;
            text-align: center" width="100%">
            <tr>
                <td align="center" class="text headerband" style="width: 100%; "> Monthly Port Call Report with PO & Invoice detail</td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                        <tr>
                            <td style="width: 100%; padding:3px;" >
                            <table cellSpacing="0" cellPadding="0" width="100%"><tbody>
   
    <tr><td style="HEIGHT: 13px">Vessel :</td><td style="HEIGHT: 13px; text-align: left;"><asp:DropDownList id="ddl_Vessel" runat="server" CssClass="input_box" Width="180px">
                                                             </asp:DropDownList></td><td 
style="HEIGHT: 13px">
                                                                 Month&amp;Year :</td><td 
style="HEIGHT: 13px">
                          <asp:DropDownList ID="ddl_ToMonth" runat="server" CssClass="required_box" TabIndex="1"
                              Width="93px">
                              <asp:ListItem Value="0">&lt; Select &gt;</asp:ListItem>
                              <asp:ListItem Value="1">Jan</asp:ListItem>
                              <asp:ListItem Value="2">Feb</asp:ListItem>
                              <asp:ListItem Value="3">Mar</asp:ListItem>
                              <asp:ListItem Value="4">Apr</asp:ListItem>
                              <asp:ListItem Value="5">May</asp:ListItem>
                              <asp:ListItem Value="6">Jun</asp:ListItem>
                              <asp:ListItem Value="7">Jul</asp:ListItem>
                              <asp:ListItem Value="8">Aug</asp:ListItem>
                              <asp:ListItem Value="9">Sep</asp:ListItem>
                              <asp:ListItem Value="10">Oct</asp:ListItem>
                              <asp:ListItem Value="11">Nov</asp:ListItem>
                              <asp:ListItem Value="12">Dec</asp:ListItem>
                          </asp:DropDownList></td>
                          <td style="HEIGHT: 13px; text-align: left;">
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddl_ToMonth" ErrorMessage="Required." Operator="NotEqual" Type="Integer" ValueToCompare="0" Width="97px"></asp:CompareValidator>
                    </td>
        <td style="text-align: left; width: 100px;">
            <asp:DropDownList id="ddl_Year" runat="server" CssClass="required_box" Width="92px"></asp:DropDownList></td>
        <td style="">
            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddl_Year" ErrorMessage="Required." Operator="NotEqual" Type="Integer" ValueToCompare="0"></asp:CompareValidator></td>
            <td style="HEIGHT: 13px">PO Status :</td>
            <td style="HEIGHT: 13px; text-align: left;"><asp:DropDownList id="DropDownList1" runat="server" CssClass="required_box" Width="66px">
    <asp:ListItem Value="A">All</asp:ListItem>
    <asp:ListItem Value="O">Open</asp:ListItem>
    <asp:ListItem Value="C">Closed</asp:ListItem>
</asp:DropDownList></td><td 
style="HEIGHT: 13px; TEXT-ALIGN: center"><asp:Button id="Button1" runat="server" CssClass="input_box" Text="Show Report" OnClick="Button1_Click" 
                                                                            Width="84px"></asp:Button></td></tr>
</tbody></table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <iframe runat="server" id="IFRAME1" frameborder="1" style="width: 100%; height:430px; overflow:auto"></iframe>
                        </td>
</tr>
</table>
</td>
</tr>
</table>
</td>
</tr>
</table>
</td> </tr> </table> 
</div>
</form>
</body>
</html>

