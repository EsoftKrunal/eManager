<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PortCallReport1Header.aspx.cs" Inherits="PortCallReport1Header" Title="Crew On Vessel Report" %>
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
 <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
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
                <td align="center" class="text headerband" style="width: 100%;"> Port Call Listing</td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                        <tr>
                            <td style="width: 100%; padding :3px;" >
                            <table cellSpacing="0" cellPadding="0" width="100%">
                            <tbody>
                            <tr>
                            <td style="">Vessel :</td>
                            <td style="text-align: left; width: 202px;"><asp:DropDownList id="ddl_Vessel" runat="server" CssClass="input_box" Width="188px"></asp:DropDownList></td>
                            <td style="text-align: left">
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddl_Vessel" ErrorMessage="Required." Operator="NotEqual" Type="Integer" ValueToCompare="0" Visible="False"></asp:CompareValidator></td>
                            <td style="">From :</td>
                            <td style="text-align: left;">
                            <asp:TextBox ID="txt_from" runat="server" CssClass="required_box" TabIndex="2" Width="90px"></asp:TextBox>
                            <asp:ImageButton ID="img_from" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" TabIndex="3" />
                            </td>
                            <td style="text-align: left">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_from"
                Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_from" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                </td>
                            <td style="text-align: left">To:</td>
                            <td style="text-align: left">
                            <asp:TextBox ID="txt_to" runat="server" CssClass="required_box" TabIndex="4"  Width="90px"></asp:TextBox>
                            &nbsp;
                            <asp:ImageButton ID="img_to" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" TabIndex="5" /></td>
        <td style="; text-align: left">
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_to"
            Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_to" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
            </td>
        <td style="text-align: left">Status :</td>
        <td style="text-align: left">
            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="input_box" Width="66px">
                <asp:ListItem Value="A">All</asp:ListItem>
                <asp:ListItem Value="O">Open</asp:ListItem>
                <asp:ListItem Value="C">Closed</asp:ListItem>
                <asp:ListItem Value="D">Deleted</asp:ListItem>
            </asp:DropDownList>
        </td>
        <TD style="TEXT-ALIGN: center">
            <asp:Button id="Button1" runat="server" CssClass="input_box" Text="Show Report" OnClick="Button1_Click" Width="90px"></asp:Button>
        </TD>
        </tr>
        </tbody>
        </table>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="img_from" PopupPosition="TopRight" TargetControlID="txt_from"> </ajaxToolkit:CalendarExtender>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupButtonID="img_to" PopupPosition="TopRight" TargetControlID="txt_to"> </ajaxToolkit:CalendarExtender>
    
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

