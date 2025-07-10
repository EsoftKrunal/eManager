<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MonthlyWagePaymentToCrew.aspx.cs" Inherits="Reporting_MonthlyWagePaymentToCrew" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css" rel="stylesheet" type="text/css" />
       <style type="text/css" >
        .fixedbar
        {
            position:fixed;
            margin:105px 0px 0px 140px;   
            background-color:#f0f0f0;  
            z-index:100;
            border:solid 1px #5c5c5c;
        }
        </style>
</head>
<body>
<form id="form1" runat="server" defaultbutton="btnsearch" >
 <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterMode="validChars" FilterType="Custom" TargetControlID="txtempno" ValidChars="0123456789sSyY"> </ajaxToolkit:FilteredTextBoxExtender>
<table style="width :100%;font-family:Arial;font-size:12px;" cellpadding="0" cellspacing="0">
<tr>
<td style=" text-align :left; vertical-align : top;" >  
<table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
<tr>
<td align="center" valign="top" style="height: 224px" >
<table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;text-align: center" width="100%">
            <tr>
                <td align="center" class="text headerband" style="width: 100%; ">
                    Monthly Pay Slip to Crew</td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                        <tr>
                            <td style="padding-right: 10px; width: 100%; color: red; text-align: center"><asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                 <table cellpadding="0" cellspacing="0" width="100%">
                                     <tr id="trdate" runat="server" style=" padding:3px;" >
                                         <td style="height: 4px; width: 91px; text-align: right; padding-right: 5px;">
                                            Emp No:</td>
                                         <td style="height: 4px; width: 150px;">
                                                <asp:TextBox ID="txtempno" runat="server" style="text-transform:uppercase" 
                                                 CssClass="required_box" Width="86px" MaxLength="6"></asp:TextBox>
                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtempno"
                                                 ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                            </td>
                                         <td style="height: 4px; width: 91px; text-align: right;">
                                             Month :</td>
                                         <td style="height: 4px; width: 250px;">
                                           <asp:DropDownList ID="ddmonth" runat="server" CssClass="required_box" 
                                                 Width="121px" >
                                                <asp:ListItem Value="0">&lt;Select&gt;</asp:ListItem>
                                                <asp:ListItem Value="1">January</asp:ListItem>
                                                <asp:ListItem Value="2">Feburary</asp:ListItem>
                                                <asp:ListItem Value="3">March</asp:ListItem>
                                                <asp:ListItem Value="4" >April</asp:ListItem>
                                                <asp:ListItem Value="5" >May</asp:ListItem>
                                                <asp:ListItem Value="6" >June</asp:ListItem>
                                                <asp:ListItem Value="7" >July</asp:ListItem>
                                                <asp:ListItem Value="8" >August</asp:ListItem>
                                                <asp:ListItem Value="9" >September</asp:ListItem>
                                                <asp:ListItem Value="10" >October</asp:ListItem>
                                                <asp:ListItem Value="11" >November</asp:ListItem>
                                                <asp:ListItem Value="12" >December</asp:ListItem>
                                            </asp:DropDownList>
                                             <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddmonth"
                                                 ErrorMessage="Required" ValueToCompare="0" Type="Integer" Operator="NotEqual"></asp:CompareValidator></td>
                                         <td style="height: 4px; width: 63px; text-align: right; padding-right: 5px;">
                                             Year :</td>
                                         <td style="height: 4px; width: 155px;">
                                             &nbsp;<asp:DropDownList ID="ddyear" runat="server" CssClass="input_box" Width="85px" >
                                             </asp:DropDownList></td>
                                         <td style="height: 4px; padding-right: 50px;" align="right">
                                            <asp:Button ID="btnsearch" runat="server" CssClass="btn" Text="Show Report" OnClick="btnsearch_Click"   /></td>
                                
                                     </tr>
                                     <tr>
                                         <td colspan="7">
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
</form>
</body>
</html>
