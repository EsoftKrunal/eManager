<%@ Page Language="C#" AutoEventWireup="true" CodeFile="planTrainingYearly.aspx.cs" Inherits="Reporting_planTrainingYearly" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
 <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
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
                <td align="center" class="text headerband" style="width: 100%; "> Plan Training Yearly</td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                        <tr>
                            <td style="width: 100%;text-align: left">
                                
                                 <table cellpadding="0" cellspacing="0" width="100%">
                                     <tr>
                                         <td colspan="2" style="padding:3px; border-bottom: #4371a5 1px solid;">
                                                 <table cellpadding="0" cellspacing="0" width="100%">
                                                     <tbody>
                                                         <tr>
                                                             <td style="height: 13px; padding-right: 15px; width: 314px;" align="right">
                                                                 Enter Year :</td>
                                                             <td style="height: 13px; width: 118px;" align="left">
                                                                 <asp:TextBox ID="txt_year" runat="server" CssClass="required_box" MaxLength="4"
                                                                     Width="118px"></asp:TextBox>
                                                                 </td>
                                                             <td style="height: 13px; width: 331px;" align="left">
                                                                 <asp:RegularExpressionValidator
                                                                         ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_year"
                                                                         Display="Dynamic" ErrorMessage="Invalid Year." ValidationExpression="^.{4,19}$"></asp:RegularExpressionValidator>
                                                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_year"
                                                                     ErrorMessage="Required." Display="Dynamic"></asp:RequiredFieldValidator><asp:CompareValidator
                                                                             ID="CompareValidator22" runat="server" ControlToValidate="txt_year" Display="Dynamic"
                                                                             ErrorMessage="Greater than Zero." Operator="GreaterThan" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                                                                 </td>
                                                             <td style="height: 13px; text-align: center">
                                                                 <asp:Button ID="Button1" runat="server" CssClass="btn" OnClick="Button1_Click" 
                                                                     Text="Show Report" /></td>
                                                         </tr>
                                                     </tbody>
                                                 </table>
                                                 <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                     FilterType="Numbers" TargetControlID="txt_year">
                                                 </ajaxToolkit:FilteredTextBoxExtender>
                                         </td>
                                     </tr>
                                    <tr><td colspan="2" align="center">
                                      <iframe runat="server" id="IFRAME1" frameborder="1" style="width: 100%; height:432px; overflow:auto"></iframe>
                                    </td></tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
</td></tr></table>
</td>
</tr>
</table>
</div>
</form>
</body>
</html>
