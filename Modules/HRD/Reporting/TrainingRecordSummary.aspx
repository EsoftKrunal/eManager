<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TrainingRecordSummary.aspx.cs" Inherits="Reporting_TrainingRecordSummary" %>
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
                <td align="center" class="text headerband" style="width: 100%; "> Training Record Summary</td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                        <tr>
                            <td style="width: 100%;text-align: left">
                                
                                 <table cellpadding="0" cellspacing="0" width="100%">
                                     <tr>
                                         <td colspan="2" style="border-bottom: #4371a5 1px solid; vertical-align: top;">
                                                 <table cellpadding="0" cellspacing="0" width="100%">
                                                     <tbody>
                                                         <tr style =" padding:3px; ">
                                                             <td style="height: 13px">From Date :</td>
                                                             <td align="left" style="height: 13px">
                                                                 <asp:TextBox ID="txt_from" runat="server" CssClass="required_box" MaxLength="15" Width="100px"></asp:TextBox>
                                                                 <asp:ImageButton ID="imgfrom" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                                                             <td style="height: 13px">
                                                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_from" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_from" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                                                             <td style="height: 13px">To Date :</td>
                                                             <td align="left" style="height: 13px">
                                                                 <asp:TextBox ID="txt_to" runat="server" CssClass="required_box" MaxLength="15" Width="100px"></asp:TextBox>
                                                                 <asp:ImageButton ID="imgto" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                                                             <td style="height: 13px; text-align: left">
                                                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_to" ValidationExpression="^(0?[1-9]|[12][0-9]|[3][01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\d\d$" ErrorMessage="Invalid Date." ></asp:RegularExpressionValidator>
                                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_to" Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                                                             <td style="height: 13px; text-align: center">
                                                                 <asp:Button ID="Button1" runat="server" CssClass="btn" OnClick="Button1_Click" Text="Show Report" /></td>
                                                         </tr>
                                                         <tr>
                                                             <td colspan="7">
                                                             <iframe id="iframe1" runat="server" frameborder="1" style="width: 100%; height:430px; overflow:auto" ></iframe>
                                                             </td>
                                                         </tr>
                                                     </tbody>
                                                 </table>
                                                 <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                     PopupButtonID="imgfrom" PopupPosition="TopRight" TargetControlID="txt_from">
                                                 </ajaxToolkit:CalendarExtender>
                                                 <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy"
                                                     PopupButtonID="imgto" PopupPosition="TopRight" TargetControlID="txt_to">
                                                 </ajaxToolkit:CalendarExtender>
                                                 
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
