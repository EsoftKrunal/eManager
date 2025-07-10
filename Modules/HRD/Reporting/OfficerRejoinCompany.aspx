<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OfficerRejoinCompany.aspx.cs" Inherits="Reporting_OfficerRejoinCompany" %>
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
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <style type="text/css" >
    .fixedbar
    {
        position:fixed;
        margin:80px 0px 0px 140px;   
        background-color:#f0f0f0;  
        z-index:100;
        border:solid 1px #5c5c5c;
    }
        .style1
        {
            width: 100px;
            height: 13px;
        }
        .style2
        {
            width: 119px;
            height: 13px;
        }
        .style3
        {
            width: 57px;
            height: 13px;
        }
        .style4
        {
            width: 53px;
            height: 13px;
        }
    </style>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btn_show" >
    <div style="text-align:center">
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
               Officer Rejoined Company&nbsp;</td>
          </tr>
          <tr>
           <td style="text-align: center; padding :3px;">
               <table cellpadding="0" cellspacing="0" style="width: 100%">
                   <tr>
                       <td style="text-align: center" colspan="6">
                           <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label></td>
                   </tr>
                   <tr>
                       <td style="text-align: right;" class="style1">
                           Month:</td>
                       <td style="text-align: left; width :139px " >
                           <asp:DropDownList ID="ddl_FromMonth" runat="server" CssClass="required_box" TabIndex="1"
                               Width="139px">
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
                       </td>
                       <td style="text-align: right" class="style3">
                           <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddl_FromMonth"
                               ErrorMessage="Required." Operator="NotEqual" Type="Integer" ValueToCompare="0"></asp:CompareValidator></td>
                       <td style="text-align: right" class="style3">
                           Year:</td>
                       <td style="text-align: left" class="style4">
                           <asp:DropDownList ID="ddlyear" runat="server" CssClass="input_box">
                           </asp:DropDownList></td>
                       <td class="style1">
                           <asp:Button ID="btn_show" runat="server" CssClass="btn" OnClick="btn_show_Click"
                               TabIndex="1" Text="Show Report" /></td>
                   </tr>
                   </table>
           </td>
          </tr>
          <tr>
          <td style=" text-align:left;">
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
