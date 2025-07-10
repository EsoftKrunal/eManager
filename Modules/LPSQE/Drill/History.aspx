<%@ Page Language="C#" AutoEventWireup="true" CodeFile="History.aspx.cs" Inherits="Drill_History"  Async="true" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
 
 <style type="text/css">
    body
    {
        margin:0px;
        font-family:Helvetica;
        font-size:14px;
        color:#2E5C8A;
        font-family:Calibri;
    }
  input
  {
      padding:2px 3px 2px 3px;
      border:solid 1px #006B8F;
      color:black; 
      font-family:Calibri;
      background-color:#E0F5FF;
  }
  textarea
  {
      padding:0px 3px 0px 3px;
      border:solid 1px #006B8F;
      color:black; 
      text-align:left;
      font-family:Calibri;
      background-color:#E0F5FF;
  }
  select
  {
      border:solid 1px #006B8F;
      padding:0px 3px 0px 3px;
      height:22px;
      vertical-align:middle;
      border:none;
      color:black; 
      font-family:Calibri;
      background-color:#E0F5FF;
  }
  td
  {
      vertical-align:middle;
  }
  .unit
  {
      color:Blue;
      font-size:13px;
      text-transform:uppercase;
  }
  .range
  {
      color:Red;
      font-size:13px;
      text-transform:uppercase;
  }
  .stickyFooter {
     position: fixed;
     bottom: 0px;
     width: 100%;
     overflow: visible;
     z-index: 99;
     padding:5px;
     background: white;
     border-top: solid black 2px;
     -webkit-box-shadow: 0px -5px 15px 0px #bfbfbf;
     box-shadow: 0px -5px 15px 0px #bfbfbf;
     vertical-align:middle;
     background-color:#FFFFCC;
}
.btn
{
     border:none;
    color:#ffffff;
    background-color:#B870FF;
    padding:4px;
    
}
.btn:hover
{
    background-color:#006B8F;
    color:White;
}
.div1
{
 background-color:#006B8F; 
 color:White;
 padding:8px; 
 font-size:14px;
 text-align:center;
 margin-top:5px;
 width:300px;
 text-align:left;
}
.rb
{
border-right:solid 1px grey;
}
.lb
{
border-left:solid 1px grey;
}
</style>
</head>
<body>
<form id="form" runat="server">
<asp:ToolkitScriptManager  ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
<table width="100%" border="0" cellpadding="3" cellspacing="0" style="background-color:#CCEEF9; border:solid 1px #00ABE1; border-bottom:none;border-top:none;">
<tr>
   <td style='font-size:17px; text-align:center;'><asp:Label ID="lblHeader" runat="server"></asp:Label></td>
</tr>
</table>
<div style="height:33px; overflow-y:scroll;overflow-x:hidden;border:solid 1px #00ABE1;">
<table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color:#00ABE1; border-collapse:collapse;" bordercolor="white">
<thead>
<tr style='color:White; height:25px;'>
        <td style="width:50px;color:White;text-align:center;"><b>History</b></td>
        <td style="width:50px;color:White;text-align:center;"><b>Print</b></td>
        <td style="width:90px;color:White;text-align:center;"><b>Type</b></td>
        <td style="width:90px;color:White;text-align:center;"><b>Due Dt.</b></td>
        <td style="width:90px;color:White;text-align:center;"><b>Done Dt.</b></td>
        <%--<td style="width:90px;color:White;text-align:center;"><b>NextDue Dt.</b></td>--%>
        <td style="color:White;text-align:center;"><b>Updated By</b></td>
        <%--<td style=""><b>Office Remarks</b></td>--%>
        <td style="width:100px;color:White;text-align:center;"><b>Office Recd. On</b></td>
        <td style="width:100px;color:White;text-align:center;"><b>Send for Export</b></td>
        <td style="width:30px;"><b>&nbsp;</b></td>
</tr>
</thead>
</table>
</div>
<div style="height:540px; border-bottom:none; border:solid 1px #00ABE1; overflow-x:hidden; overflow-y:scroll;" class='ScrollAutoReset' id='dv_SCM_List'>
<table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color:#F5FCFE; border-collapse:collapse;" class='newformat'>
 <tbody>
<asp:Repeater runat="server" ID="rptSCMLIST">
<ItemTemplate>
<tr onmouseover="">
      <td style="width:50px;text-align:center;"><asp:ImageButton ID="btnViewHistory" ImageUrl="~/Modules/PMS/Images/magnifier.png" CommandArgument='<%#Eval("DoneId")%>' OnClick="btnViewHistory_Click" ToolTip="Open History" runat="server" Visible='<%#Eval("RecordType").ToString() == "R"%>'  style="background-color:transparent"/></td>
      <td style="width:50px;text-align:center;"><asp:ImageButton ID="btnPrint" ImageUrl="~/Modules/PMS/Images/printer16x16.png" CommandArgument='<%#Eval("DoneId")%>' OnClick="btnPrint_Click" ToolTip="Print" runat="server" style="background-color:transparent"/></td>
      <td style="width:90px;text-align:center;"><%#(Eval("RecordType").ToString() == "R"? "Execute" : "Postpone")%></td>
      <td style="width:90px;text-align:center;"><%#Common.ToDateString(Eval("LastDueDate"))%></td>
      <td style="width:90px;text-align:center;"><%#Common.ToDateString(Eval("DoneDate"))%></td>
      <%--<td style="width:90px;text-align:center;"><%#Common.ToDateString(Eval("NextDueDate"))%></td>--%>
      <td style="text-align:left;"><%#Eval("DoneBy")%></td>
      <%--<td style="text-align:left;"><div style="overflow:auto; max-height:50px; min-height:20px;"><%#Eval("OfficeRemarks")%></div></td>--%>
      <td style="width:100px;text-align:center; background-color:White;">
      <img style='height:15px;<%#((Common.ToDateString(Eval("OfficeRecOn"))=="")?"":"display:none")%>' src="../Images/warning.gif"/>
      <span><%#Common.ToDateString(Eval("OfficeRecOn"))%></span>
      </td>
      <td style="width:100px;color:White;text-align:center;">
      <div style='height:15px;<%#((Common.ToDateString(Eval("OfficeRecOn"))=="")?"":"display:none")%>' >
      <asp:ImageButton ID="btnSendExport" ImageUrl="~/Modules/PMS/Images/email.png" CommandArgument='<%#Eval("DoneId")%>' OnClick="btnExport_Click" ToolTip="Send for Export" runat="server" Visible='<%#Eval("RecordType").ToString() == "R"%>' style='background-color:transparent;' />
      </div>
      </td>
      <td style="width:30px;"><b>&nbsp;</b></td>
</tr>
</ItemTemplate>
</asp:Repeater>
</tbody>
</table>
</div>
<div style="padding:5px; background-color:#FFFFCC; text-align:left;">&nbsp;<asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label></div>
</form>

</body>
</html>
