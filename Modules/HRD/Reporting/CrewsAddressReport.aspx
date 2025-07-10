<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewsAddressReport.aspx.cs" Inherits="Reporting_CrewsAddressReport" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
 <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server" defaultbutton="Button1">
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
                <td align="center" class="text headerband" style="width: 100%; "> Crew Contact Details</td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                        <tr>
                            <td style="width: 100%"><asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 100%;text-align: left">
                                 <table cellpadding="0" cellspacing="0" width="100%">
                                     <tr>
                                         <td colspan="2" style="border-bottom: #4371a5 1px solid;">
                                             <table cellpadding="0" cellspacing="0" width="100%">
                                                 <tr>
                                                    <td align="left" colspan="2">
                                                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal"
                                                        Width="100%">
                                                        <asp:ListItem Value="1" Selected="True">New</asp:ListItem>
                                                        <asp:ListItem Value="3">On Board</asp:ListItem>
                                                        <asp:ListItem Value="2">On Leave</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                    </td>
                                                    <td align="right" colspan="1" style="padding-right: 10px;"><asp:Button ID="Button1" runat="server" CssClass="btn" OnClick="Button1_Click" Text="Show Report" /></td>
                                                 </tr>
                                             </table>
</td>
                                     </tr>
                                    <tr><td colspan="2" align="center">
                                        <iframe id="IFRAME1" runat="server" frameborder="1" style="overflow: auto;
                                            width: 100%; height: 428px"></iframe>
                                    </td></tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
</td>
</tr>
</table>
</td>
</tr>
</table> 
</div>
</form>
</body>
</html>
