<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReliefCountHeader.aspx.cs" Inherits="Reporting_ReliefCountHeader" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
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
                <td align="center" class="text headerband" style="width: 100%; "> Relief Count</td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                        <tr>
                            <td>
                            <asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%;Padding:3px" >
                            <table cellSpacing=0 cellPadding=0 width="100%">
                                <tbody>
                                    <tr>
                                    <td style="WIDTH: 105px; TEXT-ALIGN: right">Vessel :</td>
                                    <td style="PADDING-LEFT: 2px; WIDTH: 187px; TEXT-ALIGN: left">
                                        <asp:DropDownList id="ddl_Vessel" runat="server" Width="240px" CssClass="input_box"></asp:DropDownList>
                                    </td>
                                    <td style="WIDTH: 96px" align=right>Rank :</td>
                                    <td style="PADDING-LEFT: 2px; WIDTH: 234px; TEXT-ALIGN: left"><asp:DropDownList id="ddRank" runat="server" Width="218px" CssClass="input_box"></asp:DropDownList>
                                    </td>
                                    <td style="PADDING-LEFT: 2px; WIDTH: 234px; TEXT-ALIGN: right">Days :</td>
                                    <td style="PADDING-LEFT: 2px; WIDTH: 234px; TEXT-ALIGN: left">
                                        <asp:TextBox CssClass="input_box" ID="txtDays" MaxLength="3" runat="server" ></asp:TextBox> 
                                        <ajaxToolkit:FilteredTextBoxExtender runat ="server" ID="FilteredTextBoxExtender1" FilterType="Numbers" TargetControlID="txtDays" ></ajaxToolkit:FilteredTextBoxExtender>
                                    </td>
                                    <td style="PADDING-LEFT: 2px; WIDTH: 234px; TEXT-ALIGN: left"></td> 
                                    <td style="PADDING-LEFT: 2px; WIDTH: 234px; TEXT-ALIGN: left">
                                        <div style="display : none" >
                                        <asp:TextBox id="txt_from" runat="server" Width="100px" CssClass="input_box" MaxLength="15"></asp:TextBox>
                                        <asp:ImageButton id="imgfrom" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif"></asp:ImageButton>
                                        <asp:TextBox id="txt_to" runat="server" Width="100px" CssClass="input_box" MaxLength="15"></asp:TextBox> 
                                        <asp:ImageButton id="imgto" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif"></asp:ImageButton> </div>
                                            </td>
                                        
                                    <td style="PADDING-LEFT: 2px; WIDTH: 234px; TEXT-ALIGN: left"><asp:Button id="Button1" onclick="Button1_Click" runat="server" CssClass="btn" Text="Show Report"></asp:Button></td>
                                    </tr>
                                    </tbody>
                                    </table>
                <%--<ajaxToolkit:CalendarExtender id="CalendarExtender1" runat="server" TargetControlID="txt_from" PopupPosition="TopRight" PopupButtonID="imgfrom" Format="dd-MMM-yyyy" Enabled="false"  OnClientShown="onCalendarShown"></ajaxToolkit:CalendarExtender>
                <ajaxToolkit:CalendarExtender id="CalendarExtender4" runat="server" TargetControlID="txt_to" PopupPosition="TopRight" PopupButtonID="imgto" Format="dd-MMM-yyyy" Enabled="false" OnClientShown="onCalendarShown"></ajaxToolkit:CalendarExtender>--%>
                
    </td>
</tr>
<tr>
    <td>
        <iframe runat="server" id="IFRAME1" frameborder="1" style="width: 100%; height:432px; overflow:auto"></iframe>
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
