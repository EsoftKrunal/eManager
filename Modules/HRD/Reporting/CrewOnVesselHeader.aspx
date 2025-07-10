<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewOnVesselHeader.aspx.cs" Inherits="Reporting_CrewOnVesselHeader" Title="Crew On Vessel Report" %>
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
<script type="text/javascript" language="javascript">
    function onCalendarShown(sender,args)
    {  
        sender._popupDiv.style.top = '0px'; 
    }
    </script> 
</head>
<body>
<form id="form1" runat="server" defaultbutton="Button1">
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
                <td align="center" class="text headerband" style="width: 100%; "> Crew List with Wages</td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="Padding:3px" >
                            <TABLE cellSpacing=0 cellPadding=0 
width="100%"><TBODY><TR><TD style="HEIGHT: 13px; width: 105px; text-align: right;">Vessel :</TD><TD style="HEIGHT: 13px; padding-left: 2px; width: 187px; text-align: left;" colspan="2"><asp:DropDownList id="ddl_Vessel" runat="server" CssClass="required_box" Width="282px">
                                                             </asp:DropDownList></TD>
    <td style="padding-left: 2px; height: 13px; text-align: right">
            From Date :</td>
    <td style="padding-left: 2px; height: 13px; text-align: left">
            <asp:TextBox id="txt_from" runat="server" CssClass="required_box" Width="100px" MaxLength="2000"></asp:TextBox> 
<asp:ImageButton id="imgfrom" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif"></asp:ImageButton></td>
    <td style="padding-left: 2px; height: 13px; text-align: right">
            To Date :</td>
    <td style="padding-left: 2px; height: 13px; text-align: left">
            <asp:TextBox id="txt_to" runat="server" CssClass="required_box" Width="100px" MaxLength="2000"></asp:TextBox> 
<asp:ImageButton id="imgto" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif"></asp:ImageButton></td>
    <TD 
style="HEIGHT: 13px; padding-left: 2px; text-align: left;"><asp:Button id="Button1" runat="server" CssClass="btn" Text="Show Report" OnClick="Button1_Click"></asp:Button>&nbsp;</TD><TD 
style="HEIGHT: 13px; TEXT-ALIGN: center"></TD></TR>
</TBODY></TABLE>
                            <ajaxToolkit:CalendarExtender id="CalendarExtender1" runat="server" TargetControlID="txt_from" PopupPosition="TopRight" PopupButtonID="imgfrom" Format="dd-MMM-yyyy" OnClientShown="onCalendarShown"></ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:CalendarExtender id="CalendarExtender4" runat="server" TargetControlID="txt_to" PopupPosition="TopRight" PopupButtonID="imgto" Format="dd-MMM-yyyy" OnClientShown="onCalendarShown"></ajaxToolkit:CalendarExtender>
                            
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

