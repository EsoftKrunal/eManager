<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WageScaleReport.aspx.cs" Inherits="Reporting_WageScaleReport" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            text-align: right;
            height: 13px;
            width: 58px;
        }
        .style3
        {
            width: 66px;
        }
        .style4
        {
            width: 59px;
        }
        .style5
        {
            width: 58px;
        }
    </style>
</head>
<body>
<form id="form1" runat="server">
<div>
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<table style="width :100%;font-family:Arial;font-size:12px;" cellpadding="0" cellspacing="0">
<tr>
<td style=" text-align :left; vertical-align : top;" >  
<table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
       <tr>
        <td align="center" valign="top">
         <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;text-align: center" width="100%">
          <tr>
           <td align="center" class="text headerband" style="width: 100%; ">
               Wage Scale</td>
          </tr>
          <tr>
           <td>
               <asp:HiddenField ID="HiddenEffectiveDate" runat="server" />
           </td>
          </tr>
             <tr>
                 <td>
                     <table cellpadding="2" cellspacing="0" class="style1">
                         <tr>
                             <td style="text-align: right; padding-right: 5px;">
                                 Wage Scale:</td>
                             <td style="width: 100px; height: 13px; text-align: left">
                                 <asp:DropDownList ID="ddl_WageScale" runat="server" CssClass="required_box" 
                                     OnSelectedIndexChanged="ddl_WageScale_SelectedIndexChanged" Width="208px" 
                                     TabIndex="1">
                                 </asp:DropDownList></td>
                             <td style="text-align: left; padding-right: 5px;">
                                 <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddl_WageScale"
                                     ErrorMessage="Required." Operator="NotEqual" Type="Integer" ValueToCompare="0"></asp:CompareValidator></td>
                             <td style="text-align: right; padding-right: 5px;" class="style3">
                                 Seniority:</td>
                             <td style="width: 100px; text-align: left">
                                 <asp:DropDownList ID="ddl_Seniority" runat="server" CssClass="input_box" 
                                     AutoPostBack="True" OnSelectedIndexChanged="ddl_Seniority_SelectedIndexChanged" 
                                     Width="50px" TabIndex="3">
                                     <asp:ListItem Value="0" Enabled="False">&lt;Select&gt;</asp:ListItem>
                                     <asp:ListItem>1</asp:ListItem>
                                     <asp:ListItem>2</asp:ListItem>
                                     <asp:ListItem>3</asp:ListItem>
                                     <asp:ListItem>4</asp:ListItem>
                                     <asp:ListItem>5</asp:ListItem>
                                     <asp:ListItem>6</asp:ListItem>
                                     <asp:ListItem>7</asp:ListItem>
                                     <asp:ListItem>8</asp:ListItem>
                                     <asp:ListItem>9</asp:ListItem>
                                     <asp:ListItem>10</asp:ListItem>
                                 </asp:DropDownList></td>
                             <td style="text-align: right; padding-right: 5px;" class="style4">
                                 History:</td>
                             <td style="width: 100px; height: 13px; text-align: left">
                                 <asp:DropDownList ID="ddl_History" runat="server" CssClass="input_box" Width="229px" TabIndex="4">
                                     <asp:ListItem Value="0">&lt; Select &gt;</asp:ListItem>
                                 </asp:DropDownList></td>
                             <td class="style2">
                                 O/R:&nbsp; </td>
                             <td style="width: 100px; height: 13px; text-align: left">
                                 <asp:DropDownList ID="ddl_OR" runat="server" CssClass="input_box">
                                     <asp:ListItem Value="A">All</asp:ListItem>
                                     <asp:ListItem Value="O">Officer</asp:ListItem>
                                     <asp:ListItem Value="R">Rating</asp:ListItem>
                                 </asp:DropDownList></td>
                             <td style="width: 100px; height: 13px; text-align: left">
                                 <asp:Button ID="btn_Show" runat="server" CssClass="btn" Text="Show Report" 
                                     OnClick="btn_Show_Click" TabIndex="5" Height="16px" /></td>
                         </tr>
                     </table>
                     <asp:DropDownList ID="ddl_Nationality" runat="server" CssClass="required_box" Visible="false" 
                                     OnSelectedIndexChanged="ddl_Nationality_SelectedIndexChanged" Width="2px" 
                                     TabIndex="10">
                                 </asp:DropDownList>
                 </td>
             </tr>
          <tr>
           <td style="text-align: left">
            <iframe runat="server" id="IFRAME1" frameborder="1" style="width: 100%; height:435px; overflow:auto"></iframe>
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
