<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VesselLineUpReport.aspx.cs" Inherits="Reporting_VesselLineUpReport" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server" defaultbutton="Button1">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<table style="width :100%;font-family:Arial;font-size:12px;" cellpadding="0" cellspacing="0">
<tr>
<td style=" text-align :left; vertical-align : top;" > 
<table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
<tr>
<td align="center" valign="top" >
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;text-align: center" width="100%">
            <tr>
                <td align="center" class="text headerband" style="width: 100%; ">
                    Vessel Line Up Report
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                        <tr>
                            <td style="padding-right: 10px; width: 100%; color: red; text-align: center">
                                <asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 100%;text-align: left">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr style=" padding :3px;" >
                                        <td style="width: 100px; text-align: right">
                                            Owner :</td>
                                        <td style="width: 100px; text-align: left">
                                            <asp:DropDownList ID="ddl_Owner" runat="server" CssClass="input_box" Width="232px" AutoPostBack="True" OnSelectedIndexChanged="ddl_Owner_SelectedIndexChanged">
                                            </asp:DropDownList></td>
                                        <td style="width: 100px; text-align: right">
                                             Vessel :</td>
                                        <td style="width: 100px; text-align: left">
                                            <asp:DropDownList ID="ddvessel" runat="server" CssClass="input_box" Width="232px" AutoPostBack="false">
                                                <asp:ListItem Value="0">All</asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td style="width: 100px; text-align: left">
                                             <asp:Button ID="Button1" runat="server" CssClass="btn"  Text="Show Report" OnClick="Button1_Click" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                    <iframe runat="server" id="Iframe2" frameborder="1" style="width: 100%; height:430px; overflow:auto"></iframe>
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
</td> </tr> </table> 
</form>
</body>
</html>
