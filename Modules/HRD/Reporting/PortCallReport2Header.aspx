<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PortCallReport2Header.aspx.cs" Inherits="PortCallReport1Header" Title="Crew On Vessel Report" %>
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
 <script language="javascript" type="text/javascript">
 
 </script>
    <style type="text/css">
        .style1
        {
            height: 13px;
            width: 117px;
        }
        .style2
        {
            height: 6px;
        }
        .style3
        {
            height: 6px;
            width: 117px;
        }
        .style4
        {
            width: 154px;
            height: 6px;
        }
    </style>
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
                <td align="center" class="text headerband" style="width: 100%; "> Monthly Port Calls With PO</td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                        <tr>
                            <td style="" >
                                <TABLE cellSpacing=4 cellPadding=0 width="100%"><TBODY>
                                    <TR>
                                        <TD style="HEIGHT: 13px; text-align: right;">Vessel :</TD>
                                        <TD style="HEIGHT: 13px; text-align: right;">
                                            <asp:DropDownList id="ddl_Vessel" runat="server" CssClass="input_box" Width="201px"></asp:DropDownList></TD>
                                        <TD style="text-align: left; ">
                                            Vendor :</TD>
                                        <TD style="HEIGHT: 13px; text-align: right;">
                                            <asp:DropDownList ID="ddl_Vendor" runat="server" CssClass="input_box" 
                                                Width="201px">
                                            </asp:DropDownList>
                                                                    </TD>
                                        <TD style="HEIGHT: 13px; text-align: right;">Month&amp;Year :</TD>
                                        <TD style="HEIGHT: 13px; text-align: left; width: 154px;">
                                            <asp:DropDownList ID="ddl_ToMonth" runat="server" CssClass="required_box" TabIndex="1" Width="75px">
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
                                            </asp:DropDownList>
                                                <asp:DropDownList id="ddl_Year" runat="server" CssClass="required_box" Width="74px"></asp:DropDownList>
                                            </TD>
                                            <TD style="HEIGHT: 13px">Status :</TD>
                                            <TD style="HEIGHT: 13px; text-align: left;">
                                            <asp:DropDownList id="DropDownList1" runat="server" CssClass="input_box" Width="87px">
                                                <asp:ListItem Value="A">All</asp:ListItem>
                                                <asp:ListItem Value="O">Open</asp:ListItem>
                                                <asp:ListItem Value="C">Closed</asp:ListItem>
                                            </asp:DropDownList></TD><TD 
                                style="HEIGHT: 13px; TEXT-ALIGN: center">
                                            <asp:Button id="Button1" runat="server" CssClass="input_box" Text="Show Report" 
                                                OnClick="Button1_Click" Width="86px"></asp:Button>
                                                                    </TD></TR>
    </TBODY></TABLE>
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
</td></tr> </table>  
</div>
</form>
</body>
</html>

