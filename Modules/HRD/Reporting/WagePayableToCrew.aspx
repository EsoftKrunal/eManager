<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WagePayableToCrew.aspx.cs" Inherits="Reporting_WagePayableToCrew" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server" defaultbutton="Button1">
<div style="text-align: center">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
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
                <td align="center" class="text headerband" style="width: 100%;"> 
                    <span style="font-size: 10pt">Portage Bill</span></td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                        <tr>
                            <td style="width: 100%;text-align: left">
                                
                                 <table cellpadding="0" cellspacing="0" width="100%">
                                     <tr>
                                         <td colspan="2" style="padding: 3px;border-bottom: #4371a5 1px solid;">
                                                 <table cellpadding="0" cellspacing="0" width="100%">
                                                     <tbody>
                                                         <tr>
                                                             <td style="height: 13px">
                                                                 Vessel List :</td>
                                                             <td style="height: 13px; width: 220px;" align="left">
                                                                 <asp:DropDownList ID="ddl_vessel" runat="server" CssClass="input_box" Width="214px">
                                                                 </asp:DropDownList></td>
                                                             <td style="height: 13px">
                                                                 Pay Month :</td>
                                                             <td style="height: 13px; width: 125px;" align="left">
                                                                 &nbsp;
                                                                 <asp:DropDownList ID="ddl_Month" runat="server" CssClass="input_box" TabIndex="1"
                                                                     Width="111px">
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
                                                             <td style="height: 13px">
                                                                 Pay Year :</td>
                                                             <td style="height: 13px; width: 119px;" align="left"><asp:DropDownList ID="ddl_year" runat="server" CssClass="input_box" Width="112px">
                                                             </asp:DropDownList>&nbsp;</td>
                                                             <td style="height: 13px; text-align: center">
                                                                 <asp:Button ID="Button1" runat="server" CssClass="btn"
                                                                     Text="Show Report" OnClick="Button1_Click" /></td>
                                                         </tr>
                                                     </tbody>
                                                 </table>
                                         </td>
                                     </tr>
                                    <tr><td colspan="2">
                                    <iframe runat="server" id="IFRAME1" frameborder="1" style="width: 100%; height:432px; overflow:auto"></iframe>
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
</td></tr></table>
</div>
</form>
</body>
</html>
