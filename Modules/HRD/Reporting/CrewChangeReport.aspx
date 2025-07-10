<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewChangeReport.aspx.cs" Inherits="Reporting_CrewChangeReport" Title="Untitled Page" %>
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
    <table style="width :100%;font-family:Arial;font-size:12px;" cellpadding="0" cellspacing="0" >
<tr>
<td style=" text-align :left; vertical-align : top;" >  
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr>
    <td align="center" valign="top" >
    <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;text-align: center" width="100%">
            <tr>
                <td align="center" class="text headerband" style="width: 100%; "> Crew Change Report</td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                        <tr>
                            <td style="padding-right: 10px; width: 825px; color: red; text-align: center">
                                <asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%;text-align: left">
                            <table cellpadding="0" cellspacing="0" width="100%">
                                 <tr>
                                     <td colspan="2" style="border-bottom: #4371a5 1px solid; height: 13px">
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                         <tr>
                                         <td style="width: 348px; height: 13px; text-align: left">
                                             <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                                             <asp:ListItem Selected="True" Value="0">Total Crew Change</asp:ListItem>
                                             <asp:ListItem Value="1">Sign Off</asp:ListItem>
                                             <asp:ListItem Value="2">Sign On</asp:ListItem>
                                             </asp:RadioButtonList>
                                         </td>
                                                 <td style="height: 13px; width: 62px; text-align: right;">
                                                     Vessel :</td>
                                                 <td style="height: 13px; width: 258px; text-align: left;">
                                                     <asp:DropDownList ID="ddl_Vessel" runat="server" CssClass="input_box" Width="262px">
                                                     </asp:DropDownList></td>
                                                 <td style="height: 13px; width: 60px;">
                                                     From :</td>
                                                 <td style="height: 13px; width: 127px;">
                                                     <asp:TextBox ID="txt_from" runat="server" CssClass="required_box" MaxLength="15"
                                                         Width="80px"></asp:TextBox>
                                                     <asp:ImageButton ID="imgfrom" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                                                 <td style="height: 13px; width: 23px;">
                                                     To:</td>
                                                 <td style="height: 13px; width: 137px;">
                                                     <asp:TextBox ID="txt_to" runat="server" CssClass="required_box" MaxLength="15"
                                                         Width="80px"></asp:TextBox>
                                                     <asp:ImageButton ID="imgto" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                                                 <td style="height: 13px; text-align: center; width: 125px;">
                                                     <asp:Button ID="Button1" runat="server" CssClass="btn" Text="Show Report" Width="94px" OnClick="Button1_Click"/></td>
                                             </tr>
                                         </table>
                                     </td>
                                 </tr>
                                 <tr>
                                    <td colspan="2">
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" OnClientShown="onCalendarShown"
                                            PopupButtonID="imgfrom" PopupPosition="TopRight" TargetControlID="txt_from">
                                        </ajaxToolkit:CalendarExtender>                                       
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy" OnClientShown="onCalendarShown"
                                            PopupButtonID="imgto" PopupPosition="TopRight" TargetControlID="txt_to">
                                        </ajaxToolkit:CalendarExtender>
                                        
                                    </td>
                                </tr>
                                 <tr>
                                     <td colspan="2">
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
        </td>
    </tr>
    </table>
</td></tr></table> 
</div>
</form>
</body>
</html>

