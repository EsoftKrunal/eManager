<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FleetAge.aspx.cs" Inherits="Reporting_FleetAge" Title="Fleet Age Profile Report" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
         <style type="text/css" >
        .fixedbar
        {
            position:fixed;
            margin:60px 0px 0px 117px;   
            background-color:#f0f0f0;  
            z-index:100;
            border:solid 1px #5c5c5c;
        }
        </style>
 </head>
<body>
<form id="form1" runat="server">
<div style="text-align: center">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table style="width :100%;font-family:Arial;font-size:12px;" cellpadding="0" cellspacing="0">
<tr>
<td style=" text-align :left; vertical-align : top;" > 
<table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td align="center" style="height: 149px" valign="top">
            <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;text-align: center" width="100%">
                <tr>
                    <td align="center" class="text headerband" style="width: 100%;">
                        Fleet Age Profile Report</td>
                </tr>
                <tr>
                    <td style="width: 100%;vertical-align:top; padding :3px">
                        <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                             <tr>
                                 <td colspan="1" style="width: 209px; height: 3px;">
                                     &nbsp;Status :
                                     <asp:DropDownList ID="ddlCrewType" runat="server" CssClass="input_box">
                                         <asp:ListItem Value="0">&lt; All &gt;</asp:ListItem>
                                         <asp:ListItem Value="1">New Emp</asp:ListItem>
                                         <asp:ListItem Value="2">On Leave</asp:ListItem>
                                         <asp:ListItem Value="3">On Board</asp:ListItem>
                                     </asp:DropDownList>
                                     </td>
                                 <td colspan="1" style="width: 79px; height: 3px;">
                                     Off Crew :</td>
                                 <td colspan="1" style="text-align: left; height: 3px; width:150px">
                                     <asp:DropDownList ID="ddlRankType" runat="server" CssClass="input_box" Width="110px">
                                         <asp:ListItem Value="A">All</asp:ListItem>
                                         <asp:ListItem Value="O">Officers</asp:ListItem>
                                         <asp:ListItem Value="R">Rating</asp:ListItem>
                                     </asp:DropDownList></td>
                                   <td colspan="1" style="width: 140px; height: 3px;">
                                     Recruiting Office :</td>
                                 <td colspan="1" style="text-align: left; height: 3px;">
                                     <asp:DropDownList ID="ddlRecOff" runat="server" CssClass="input_box" Width="110px">
                                     </asp:DropDownList></td>
                                         <td align="right" style="height: 3px">
                                            <asp:Button ID="Show" runat="server" CssClass="btn" Text="Show Report" OnClick="Show_Click" />
                                         </td>
                             </tr>
                        </table>
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
</td> </tr> </table> 
</div>
</form>
</body>
</html>

