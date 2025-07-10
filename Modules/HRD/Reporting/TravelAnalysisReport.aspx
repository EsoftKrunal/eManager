<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TravelAnalysisReport.aspx.cs" Inherits="Reporting_TravelAnalysisReport" Title="Fleet Age Profile Report" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
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
            margin:70px 0px 0px 117px;   
            background-color:#f0f0f0;  
            z-index:100;
            border:solid 1px #5c5c5c;
        }
        </style>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
  </head>
<body>
<form id="form1" runat="server">
<div style="text-align: center">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
 <table style="width :100%;font-family:Arial;font-size:12px;" cellpadding="0" cellspacing="0">
<tr>
<td style=" text-align :left; vertical-align : top;" >                
<table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="center" style="height: 149px" valign="top">
                    <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;text-align: center" width="100%">
                        <tr>
                            <td align="center" class="text headerband" style="width: 100%; ">
                                Travel Analysis Report</td>
                        </tr>
                        <tr>
                            <td style="width: 100%;vertical-align:top;">
                                <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                                     <tr>
                                         <td colspan="1" style="padding-right: 10px; 
                                             ; width: 51px; height: 24px; text-align: right;">
                                             &nbsp;Vessel :</td>
                                         <td colspan="1" style="padding-right: 10px; 
                                             width: 79px; ; text-align: left; height: 24px;">
                                             <asp:DropDownList ID="ddl_Vessel" runat="server" CssClass="required_box" Width="162px">
                                             </asp:DropDownList></td>
                                         <td style="padding-right: 10px; 
                                             width: 211px; ; text-align: left; height: 24px;">
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddl_Vessel"
                                                ErrorMessage="Required." Operator="NotEqual" Type="Integer" ValueToCompare="0" SetFocusOnError="True"></asp:CompareValidator></td>
                                         <td colspan="1" style="padding-right: 10px; 
                                             width: 211px; ; text-align: right; height: 24px;">
                                             Month&amp;Year :</td>
                                         <td colspan="1" style="padding-right: 10px; 
                                             width: 35px; height: 24px;">
                                             <asp:DropDownList ID="ddl_ToMonth" runat="server" CssClass="required_box" TabIndex="1"
                                                 Width="59px">
                                                 <asp:ListItem Value="0">Select</asp:ListItem>
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
                                         <td style="padding-right: 10px; 
                                             width: 35px; height: 24px; text-align: left;">
                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddl_ToMonth"
                                                ErrorMessage="Required." Operator="NotEqual" Type="Integer" ValueToCompare="0"
                                                Width="63px"></asp:CompareValidator></td>
                                         <td colspan="1" style="padding-right: 10px; width: 12px; height: 24px">
                                             <asp:DropDownList ID="ddl_Year" runat="server" CssClass="required_box"
                                                 Width="59px">
                                             </asp:DropDownList></td>
                                         <td style="padding-right: 10px; width: 12px; height: 24px; text-align: left;">
                                            <asp:CompareValidator ID="CompareValidator3"
                                                    runat="server" ControlToValidate="ddl_Year" ErrorMessage="Required." Operator="NotEqual"
                                                    Type="Integer" ValueToCompare="0"></asp:CompareValidator></td>
                                         <td colspan="1" style="padding-right: 10px; 
                                             width: 405px; ; height: 24px; text-align: right;">
                                             Agent :</td>
                                         <td colspan="1" style="padding-right: 10px; 
                                             ; text-align: left; height: 24px;">
                                             <asp:DropDownList ID="ddl_Vendor" runat="server" CssClass="input_box" Width="176px">
                                             </asp:DropDownList></td>
                                         <td colspan="1" style="padding-right: 10px; height: 24px; text-align: left">
                                            <asp:Button ID="Show" runat="server" CssClass="btn"
                                                 Text="Show Report" OnClick="Show_Click" Width="89px" /></td>
                                     </tr>
                                    </table>
                            </td>
                        </tr>
                        <tr>
                        <td style=" text-align :left " >
                        <iframe runat="server" id="IFRAME1" frameborder="1" style="width: 100%; height:430px; overflow:auto"></iframe>
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

