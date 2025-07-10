<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Regulation_ACK.aspx.cs" Inherits="Regulation_ACK"  Async="true" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
  <title>EMANAGER</title>
  <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
  <script type="text/javascript" src="../../HRD/JS/jquery.min.js"></script>
</head>
<body>
<form id="form" runat="server" style="font-family:Arial;font-size:12px;">
<ajaxToolkit:ToolkitScriptManager  ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<div style=" font-size:15px;  padding:5px;" class="text headerband">
<center>
    Vessel Acknowledgement 
</center>
</div>
<center>
<asp:Label runat="server" ID="lblNumber" Font-Size="Large"></asp:Label>
</center>
<div style="height:33px; overflow-y:scroll;overflow-x:hidden;border:solid 1px #00ABE1;">
<table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color:#00ABE1; border-collapse:collapse;" bordercolor="white">
<thead>
<tr style=' height:25px;' class= "headerstylegrid">
        <td style="text-align:left; color:White;"><b>Vessel Name</b></td>
        <td style="width:100px;text-align:center;color:White;"><b>Sent Date</b></td>
        <td style="width:100px;text-align:center;color:White;"><b>Ack Recd. Date</b></td>
        <td style="width:30px;"><b>&nbsp;</b></td>
</tr>
</thead>
</table>
</div>
<div style="height:500px; border-bottom:none; border:solid 1px #00ABE1; overflow-x:hidden; overflow-y:scroll;" class='ScrollAutoReset' id='dv_REG_List'>
<table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color:#F5FCFE; border-collapse:collapse;" class='newformat'>
<tbody>
<asp:Repeater runat="server" ID="rptRegulation">
<ItemTemplate>
<tr>
      <td style="text-align:left;"><%#Eval("VesselName")%></td>
      <td style="width:100px;text-align:center;"><%#Common.ToDateString(Eval("SentOn"))%></td>
      <td style="width:100px;text-align:center;"><%#Common.ToDateString(Eval("AckOn"))%></td>
      <td style="width:30px;"><b>&nbsp;</b></td>
</tr>
</ItemTemplate>
</asp:Repeater>
</tbody>
</table>
</div>
</form>
</body>
</html>
