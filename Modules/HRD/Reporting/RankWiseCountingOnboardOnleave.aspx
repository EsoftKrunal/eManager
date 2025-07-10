<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RankWiseCountingOnboardOnleave.aspx.cs" Inherits="Reporting_RankWiseCountingOnboardOnleave" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
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
    margin:70px 0px 0px 140px;   
    background-color:#f0f0f0;  
    z-index:100;
    border:solid 1px #5c5c5c;
}
</style>  
</head>
<body>
<form id="form1" runat="server" defaultbutton="btn_show" >
<div style="text-align: center">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<table style="width :100%;font-family:Arial;font-size:12px;" cellpadding="0" cellspacing="0">
<tr>
<td style=" text-align :left; vertical-align : top;" >  
<table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
<tr>
<td align="center" valign="top" >
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;text-align: center" width="100%">
            <tr>
                <td align="center" class="text headerband" style="width: 100%;">Rank Wise Counting of Crew Members On Board/On Leave</td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                        <tr>
                            <td  style="width: 100%;text-align: left">
                                 <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 402px; text-align: center;">
                                            <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                                Width="434px">
                                                <asp:ListItem Value="1">New</asp:ListItem>
                                                <asp:ListItem Value="2">On Leave</asp:ListItem>
                                                <asp:ListItem Value="3">On Board</asp:ListItem>
                                            </asp:CheckBoxList></td>
                                        <td>&nbsp;<asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1"></asp:Label></td>
                                        <td><asp:Button ID="btn_show" runat="server" CssClass="btn" Text="Show Report" Width="95px" OnClick="btn_show_Click" /></td>
                                    </tr>
                                    <tr><td colspan="3" style=""><iframe runat="server" id="IFRAME1" frameborder="1" style="width: 100%; height:429px; overflow:auto"></iframe></td></tr>
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
