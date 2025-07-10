<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PoolRatioSearch.aspx.cs" Inherits="PoolRatioSearch" %>
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
    <script type="text/javascript" language="javascript">
    function onCalendarShown(sender,args)
    {  
        sender._popupDiv.style.top = '0px'; 
    }
    </script> 
    <style type="text/css" >  
    .fixedbar
    {
        position:fixed;
        margin:70px 0px 0px 140px;   
        background-color:#f0f0f0;  
        z-index:100;
        border:solid 1px #5c5c5c;
    }
    </style> 
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id="form1" runat="server" defaultbutton="btn_show" >
<div>
 <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<table style="width :100%;font-family:Arial;font-size:12px;" cellpadding="0" cellspacing="0">
<tr>
<td style=" text-align :left; vertical-align : top;" >  
<table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
<tr>
<td align="center" valign="top">
 <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;text-align: center" width="100%">
  <tr>
   <td align="center" class="text headerband" style="width: 100%; ">
    Pool Ratio Report
   </td>
  </tr>
     
     <tr>
         <td style="border-bottom: #4371a5 1px solid;" >
             <table cellpadding="3" cellspacing="3" style="width: 100%;" >
                 <tr>
                     <td align="right" style="padding-right: 10px;text-align: right">
                         From :</td>
                     <td align="left" style="width: 200px">
                       <asp:TextBox ID="txtFromDate" runat="server" Width="82px" CssClass="required_box" ></asp:TextBox>
                       <asp:ImageButton ID="imgFromDate" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" TabIndex="2" />
                         <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" OnClientShown="onCalendarShown" PopupButtonID="imgFromDate" PopupPosition="TopRight" TargetControlID="txtFromDate"> </ajaxToolkit:CalendarExtender>
                             
                     </td>
                     <td style="padding-right: 10px; height: 13px; text-align: right">To :</td>
                     <td style="padding-right: 10px; width: 148px; height: 13px; text-align: left">
                         <asp:TextBox ID="txtToDate" runat="server" Width="82px" TabIndex="1" CssClass="required_box"></asp:TextBox>
                         <asp:ImageButton ID="imgTo" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" TabIndex="2" />
                         <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" OnClientShown="onCalendarShown" PopupButtonID="imgTo" PopupPosition="TopRight" TargetControlID="txtToDate"> </ajaxToolkit:CalendarExtender>
                     </td>
                     <td style="width: 80px; height: 13px; text-align : right;">
                         Location :
                     </td>
                     <td style="width: 150px; height: 13px">
                        <asp:DropDownList ID="ddlLocation" runat="server"  Width="130px" >
                        </asp:DropDownList>
                    </td>
                    <td>
                        Rank Group :
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlOffCrew" runat="server" CssClass="input_box" Width="110px">
                             <asp:ListItem Value="A">All</asp:ListItem>
                             <asp:ListItem Value="O">Officers</asp:ListItem>
                             <asp:ListItem Value="R">Rating</asp:ListItem>
                         </asp:DropDownList>
                    </td>
                     <td >
                        <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                        <asp:Button ID="btn_show" runat="server" CssClass="btn" Text="Show Report" OnClick="btn_show_Click" TabIndex="5" />
                    </td>
                 </tr>
                 </table>
         </td>
     </tr>
  <tr>
   <td style="text-align: left;">
   <iframe runat="server" id="IFRAME1" frameborder="1" style="width: 100%; height:434px; overflow:auto"></iframe>
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
