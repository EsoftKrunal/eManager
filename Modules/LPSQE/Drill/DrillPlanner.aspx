<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DrillPlanner.aspx.cs" Inherits="Drill_DrillPlanner"  Async="true" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/Modules/PMS/HSSQE/mainmene.ascx" tagname="mrvmenu" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<script src="../eReports/JS/jquery.min.js" type="text/javascript"></script>
<script src="../eReports/JS/KPIScript.js" type="text/javascript"></script>
<link rel="stylesheet" type="text/css" href="../eReports/css/jquery.datetimepicker.css"/>
<link rel="stylesheet" type="text/css" href="../eReports/css/stylesheet.css"/>
    <link rel="stylesheet" type="text/css" href="../HSSQE/style.css"/>
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
.myradio
{
    height:100%;
    border:solid 1px #c2c2c2;
 
}
.success
{
    background-color:Green !important;
}
.error
{
    background-color:red !important; 
}
.planned
{
    background-color:orange; 
}
.ondate
{
    width:17px;height:20px;color:black;overflow:hidden;display:block;text-align:center;margin:0 auto;font-weight:normal;
}
.done-already {
    background-color:#98d8f1;
}

</style>
</head>
<body>
<form id="form" runat="server">
<asp:ToolkitScriptManager  ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

    <div class="menuframe">                
        <uc1:mrvmenu ID="mrvmenu1" runat="server" />                
    </div>
    <div style="border-bottom:solid 5px #4371a5;"></div>
    <%--------------------------------------------------------------------------------------%>

<div style="padding:5px">
    <asp:RadioButton Text="&nbsp;Drill Planner-M" ToolTip="Drill Planner (Monthly)" runat="server" ID="r1" GroupName="tt" Checked="true" OnCheckedChanged="radioButton_OnCheckedChanged" AutoPostBack="true" />
    <asp:RadioButton Text="&nbsp;Drill Planner-W" ToolTip="Drill Planner (Weekly)" runat="server" ID="r3" GroupName="tt" OnCheckedChanged="radioButton_OnCheckedChanged" AutoPostBack="true" />
    <asp:RadioButton Text="&nbsp;Drill Update" runat="server" ID="r2" GroupName="tt" OnCheckedChanged="radioButton_OnCheckedChanged" AutoPostBack="true"/>
</div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<table width="100%" border="0" cellpadding="3" cellspacing="0" style="background-color:#CCEEF9; border:solid 1px #00ABE1; margin-bottom:3px;">
<tr>
    <td style="text-align:right;width:100px;" >Year : </td>
    <td style="text-align:left; width:75px;"><asp:DropDownList runat="server" ID="ddlYear" Width="100px"></asp:DropDownList>  </td>
    <td style="text-align:right;width:100px;" >Drill / Training : </td>
    <td style="text-align:left; width:75px;">
    <asp:DropDownList runat="server" ID="ddlDT" Width="100px" AutoPostBack="true" OnSelectedIndexChanged="ddlDT_OnSelectedIndexChanged">
        <asp:ListItem Text="All" Value="A"></asp:ListItem>
        <asp:ListItem Text="Drill" Value="D"></asp:ListItem>
        <asp:ListItem Text="Trainings" Value="T"></asp:ListItem>
    </asp:DropDownList>  </td>
    <td style="text-align:right;width:100px;"><asp:Label runat="server" ID="lblDTName" Width="100px"></asp:Label> </td>
    <td style="text-align:left; width:210px;">
    <asp:DropDownList runat="server" ID="ddlDrillgroups" Width="200px" Visible="false"></asp:DropDownList>
    <asp:DropDownList runat="server" ID="ddlTraininggroups" Width="200px" Visible="false"></asp:DropDownList>
    </td>
    
    <td style="text-align:right">
      <asp:Button runat="server" ID="Button1" Text="Search" onclick="btnSearch_Click" style=" background-color:#00ABE1; color:White; border:solid 1px grey;" />
      &nbsp;<asp:Button runat="server" ID="Button2" Text="Clear" onclick="btnClear_Click" style=" background-color:#00ABE1; color:White; border:solid 1px grey;" />
        &nbsp;<asp:Button runat="server" ID="btnPrint" Text=" Print " style=" background-color:#00ABE1; color:White; border:solid 1px grey;"  OnClick="btnPrint_Click"/>
    </td>
</tr>
</table>
</ContentTemplate>
<Triggers>
<asp:PostBackTrigger ControlID="Button2" />
<asp:PostBackTrigger ControlID="Button1" />
</Triggers>
</asp:UpdatePanel>
<div style="height:33px; overflow-y:scroll;overflow-x:hidden;border:solid 1px #00ABE1;">
<table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color:#00ABE1; border-collapse:collapse;" bordercolor="white">
<thead>
<tr style='color:White; height:25px;'>
        <td style="width:100px;color:White;text-align:center;"><b>Record Type</b></td>
        <td style="color:White;text-align:center;"><b>Name</b></td>
        <td style="width:70px;color:White;text-align:center;"><b>Interval</b></td>
        <td style="width:80px;color:White;text-align:center;"><b>Last Done</b></td>
        <td style="width:50px;color:White;text-align:center;"><b>JAN</b></td>
        <td style="width:50px;color:White;text-align:center;"><b>FEB</b></td>
        <td style="width:50px;color:White;text-align:center;"><b>MAR</b></td>
        <td style="width:50px;color:White;text-align:center;"><b>APR</b></td>
        <td style="width:50px;color:White;text-align:center;"><b>MAY</b></td>
        <td style="width:50px;color:White;text-align:center;"><b>JUN</b></td>
        <td style="width:50px;color:White;text-align:center;"><b>JUL</b></td>
        <td style="width:50px;color:White;text-align:center;"><b>AUG</b></td>
        <td style="width:50px;color:White;text-align:center;"><b>SEP</b></td>
        <td style="width:50px;color:White;text-align:center;"><b>OCT</b></td>
        <td style="width:50px;color:White;text-align:center;"><b>NOV</b></td>
        <td style="width:50px;color:White;text-align:center;"><b>DEC</b></td>
        <td style="width:30px;"><b>&nbsp;</b></td>
</tr>
</thead>
</table>
</div>
<div style="height:362px; border-bottom:none; border:solid 1px #00ABE1; overflow-x:hidden; overflow-y:scroll;" class='ScrollAutoReset' id='dv_SCM_List001'>
<table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color:White; border-collapse:collapse;" class='newformat'>
 <tbody>
<asp:Repeater runat="server" ID="rptSCMLIST">
<ItemTemplate>
<tr onmouseover="">
    <td style="width:100px;text-align:center;"><%#((Eval("RecordType").ToString()=="D")?"Drill":"Training")%></td>
    <td style="text-align:left;"><%#Eval("TraininingName")%></td>
    <td style="width:70px;text-align:center;"><%#Eval("ValidityValue")%>-<%#Eval("Validitytype")%></td>
    <td style="width:80px;text-align:center;"><%#Common.ToDateString(Eval("DoneDate"))%></td>
    <td style="width:50px;text-align:center;"><div class='myradio <%#Eval("Class1")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' month="1" year='<%#Eval("PYear")%>'>  <span class='ondate'><%#getDate(Eval("Mon1V"))%></span></div></td>
    <td style="width:50px;text-align:center;"><div class='myradio <%#Eval("Class2")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' month="2" year='<%#Eval("PYear")%>'>  <span class='ondate'><%#getDate(Eval("Mon2V"))%></span></div></td>
    <td style="width:50px;text-align:center;"><div class='myradio <%#Eval("Class3")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' month="3" year='<%#Eval("PYear")%>'>  <span class='ondate'><%#getDate(Eval("Mon3V"))%></span></div></td>
    <td style="width:50px;text-align:center;"><div class='myradio <%#Eval("Class4")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' month="4" year='<%#Eval("PYear")%>'>  <span class='ondate'><%#getDate(Eval("Mon4V"))%></span></div></td>
    <td style="width:50px;text-align:center;"><div class='myradio <%#Eval("Class5")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' month="5" year='<%#Eval("PYear")%>'>  <span class='ondate'><%#getDate(Eval("Mon5V"))%></span></div></td>
    <td style="width:50px;text-align:center;"><div class='myradio <%#Eval("Class6")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' month="6" year='<%#Eval("PYear")%>'>  <span class='ondate'><%#getDate(Eval("Mon6V"))%></span></div></td>
    <td style="width:50px;text-align:center;"><div class='myradio <%#Eval("Class7")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' month="7" year='<%#Eval("PYear")%>'>  <span class='ondate'><%#getDate(Eval("Mon7V"))%></span></div></td>
    <td style="width:50px;text-align:center;"><div class='myradio <%#Eval("Class8")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' month="8" year='<%#Eval("PYear")%>'>  <span class='ondate'><%#getDate(Eval("Mon8V"))%></span></div></td>
    <td style="width:50px;text-align:center;"><div class='myradio <%#Eval("Class9")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' month="9" year='<%#Eval("PYear")%>'>  <span class='ondate'><%#getDate(Eval("Mon9V"))%></span></div></td>
    <td style="width:50px;text-align:center;"><div class='myradio <%#Eval("Class10")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' month="10" year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon10V"))%></span></div></td>
    <td style="width:50px;text-align:center;"><div class='myradio <%#Eval("Class11")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' month="11" year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon11V"))%></span></div></td>
    <td style="width:50px;text-align:center;"><div class='myradio <%#Eval("Class12")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' month="12" year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon12V"))%></span></div></td>
    <td style="width:30px;">&nbsp;</td>
</tr>
</ItemTemplate>
</asp:Repeater>
</tbody>
</table>
</div>
<div style="padding:5px; background-color:#FFFFCC; text-align:left; overflow:auto;"><asp:Label ID="lblMsg" runat="server" ForeColor="Red" style="float:left"></asp:Label>

    <div class="planned" style="width:140px; padding:3px; color:White; float:right; text-align:center; margin-right:5px;"> To be Completed </div>
    <div class="error" style="width:120px; padding:3px; color:White; float:right;text-align:center; margin-right:5px;">  Non Compliance  </div>
    <div class="success" style="width:100px; padding:3px; color:White; float:right;text-align:center; margin-right:5px;">  Compliance  </div>
    <div class="done-already" style="width:120px; padding:3px; color:White; float:right;text-align:center; margin-right:5px;">  Unscheduled  </div>
</div>



</form>
</body>
</html>
