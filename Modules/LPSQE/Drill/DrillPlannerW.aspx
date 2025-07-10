<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DrillPlannerW.aspx.cs" Inherits="Drill_DrillPlannerW"  Async="true" %>
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
*{
    font-size:10px;
}
.newformat tr td
{
    border:solid 1px #bfbfbf;
    padding:0px;
    margin:0px;
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
    <asp:RadioButton Text="&nbsp;Drill Planner-M" ToolTip="Drill Planner (Monthly)" runat="server" ID="r1" GroupName="tt" OnCheckedChanged="radioButton_OnCheckedChanged" AutoPostBack="true" />
    <asp:RadioButton Text="&nbsp;Drill Planner-W" ToolTip="Drill Planner (Weekly)" runat="server" ID="r3" GroupName="tt"  Checked="true" OnCheckedChanged="radioButton_OnCheckedChanged" AutoPostBack="true" />
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
<div style="height:27px; overflow-y:scroll;overflow-x:hidden;border:solid 1px #00ABE1;">
<table width="100%" border="0" cellpadding="0" cellspacing="0" style="background-color:#00ABE1; border-collapse:collapse;" bordercolor="white">
<thead>
<tr style='color:White;height:23px;'>
        <td style="width:60px;color:White;text-align:center;">Record Type</td>
        <td style="color:White;text-align:center;">Name ( Weekly )</td>
        <%--<td style="width:70px;color:White;text-align:center;">Interval</td>
        <td style="width:70px;color:White;text-align:center;">Last Done</td>--%>
        <td style="width:20px;color:White;text-align:center;">1</td>
        <td style="width:20px;color:White;text-align:center;">2</td>
        <td style="width:20px;color:White;text-align:center;">3</td>
        <td style="width:20px;color:White;text-align:center;">4</td>
        <td style="width:20px;color:White;text-align:center;">5</td>
        <td style="width:20px;color:White;text-align:center;">6</td>
        <td style="width:20px;color:White;text-align:center;">7</td>
        <td style="width:20px;color:White;text-align:center;">8</td>
        <td style="width:20px;color:White;text-align:center;">9</td>
        <td style="width:20px;color:White;text-align:center;">10</td>
        <td style="width:20px;color:White;text-align:center;">11</td>
        <td style="width:20px;color:White;text-align:center;">12</td>
        <td style="width:20px;color:White;text-align:center;">13</td>
        <td style="width:20px;color:White;text-align:center;">14</td>
        <td style="width:20px;color:White;text-align:center;">15</td>
        <td style="width:20px;color:White;text-align:center;">16</td>
        <td style="width:20px;color:White;text-align:center;">17</td>
        <td style="width:20px;color:White;text-align:center;">18</td>
        <td style="width:20px;color:White;text-align:center;">19</td>
        <td style="width:20px;color:White;text-align:center;">20</td>
        <td style="width:20px;color:White;text-align:center;">21</td>
        <td style="width:20px;color:White;text-align:center;">22</td>
        <td style="width:20px;color:White;text-align:center;">23</td>
        <td style="width:20px;color:White;text-align:center;">24</td>
        <td style="width:20px;color:White;text-align:center;">25</td>
        <td style="width:20px;color:White;text-align:center;">26</td>
        <td style="width:20px;color:White;text-align:center;">27</td>
        <td style="width:20px;color:White;text-align:center;">28</td>
        <td style="width:20px;color:White;text-align:center;">29</td>
        <td style="width:20px;color:White;text-align:center;">30</td>
        <td style="width:20px;color:White;text-align:center;">31</td>
        <td style="width:20px;color:White;text-align:center;">32</td>
        <td style="width:20px;color:White;text-align:center;">33</td>
        <td style="width:20px;color:White;text-align:center;">34</td>
        <td style="width:20px;color:White;text-align:center;">35</td>
        <td style="width:20px;color:White;text-align:center;">36</td>
        <td style="width:20px;color:White;text-align:center;">37</td>
        <td style="width:20px;color:White;text-align:center;">38</td>
        <td style="width:20px;color:White;text-align:center;">39</td>
        <td style="width:20px;color:White;text-align:center;">40</td>
        <td style="width:20px;color:White;text-align:center;">41</td>
        <td style="width:20px;color:White;text-align:center;">42</td>
        <td style="width:20px;color:White;text-align:center;">43</td>
        <td style="width:20px;color:White;text-align:center;">44</td>
        <td style="width:20px;color:White;text-align:center;">45</td>
        <td style="width:20px;color:White;text-align:center;">46</td>
        <td style="width:20px;color:White;text-align:center;">47</td>
        <td style="width:20px;color:White;text-align:center;">48</td>
        <td style="width:20px;color:White;text-align:center;">49</td>
        <td style="width:20px;color:White;text-align:center;">50</td>
        <td style="width:20px;color:White;text-align:center;">51</td>
        <td style="width:20px;color:White;text-align:center;">52</td>
        <td style="width:20px;"><b>&nbsp;</b></td>
</tr>
</thead>
</table>
</div>
<div style="height:390px; border-bottom:none; border:solid 1px #00ABE1; overflow-x:hidden; overflow-y:scroll; " class='ScrollAutoReset' id='dv_SCM_List'>
<table width="100%" border="0" cellpadding="0" cellspacing="0" style="background-color:White; border-collapse:collapse;" class='newformat'>
 <tbody>
<asp:Repeater runat="server" ID="rptSCMLISTW">
<ItemTemplate>
<tr onmouseover="">
    <td style="width:60px;text-align:center;"><%#((Eval("RecordType").ToString()=="D")?"Drill":"Training")%></td>
    <td style="text-align:left;">&nbsp;<%#Eval("TraininingName")%></td>
    <%--<td style="width:70px;text-align:center;"><%#Eval("ValidityValue")%>-<%#Eval("Validitytype")%></td>
    <td style="width:90px;text-align:center;"><%#getDate(Eval("DoneDate"))%></td>--%>

    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class1")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="1" startdate='<%#getStartDate(1)%>' enddate='<%#getEndDate(1)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon1V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class2")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="2" startdate='<%#getStartDate(2)%>' enddate='<%#getEndDate(2)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon2V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class3")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="3" startdate='<%#getStartDate(3)%>' enddate='<%#getEndDate(3)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon3V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class4")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="4" startdate='<%#getStartDate(4)%>' enddate='<%#getEndDate(4)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon4V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class5")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="5" startdate='<%#getStartDate(5)%>' enddate='<%#getEndDate(5)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon5V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class6")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="6" startdate='<%#getStartDate(6)%>' enddate='<%#getEndDate(6)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon6V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class7")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="7" startdate='<%#getStartDate(7)%>' enddate='<%#getEndDate(7)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon7V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class8")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="8" startdate='<%#getStartDate(8)%>' enddate='<%#getEndDate(8)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon8V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class9")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="9" startdate='<%#getStartDate(9)%>' enddate='<%#getEndDate(9)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon9V"))%></span></div></td>
                     
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class10")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="10" startdate='<%#getStartDate(10)%>' enddate='<%#getEndDate(10)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon10V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class11")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="11" startdate='<%#getStartDate(11)%>' enddate='<%#getEndDate(11)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon11V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class12")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="12" startdate='<%#getStartDate(12)%>' enddate='<%#getEndDate(12)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon12V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class13")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="13" startdate='<%#getStartDate(13)%>' enddate='<%#getEndDate(13)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon13V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class14")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="14" startdate='<%#getStartDate(14)%>' enddate='<%#getEndDate(14)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon14V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class15")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="15" startdate='<%#getStartDate(15)%>' enddate='<%#getEndDate(15)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon15V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class16")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="16" startdate='<%#getStartDate(16)%>' enddate='<%#getEndDate(16)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon16V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class17")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="17" startdate='<%#getStartDate(17)%>' enddate='<%#getEndDate(17)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon17V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class18")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="18" startdate='<%#getStartDate(18)%>' enddate='<%#getEndDate(18)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon18V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class19")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="19" startdate='<%#getStartDate(19)%>' enddate='<%#getEndDate(19)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon19V"))%></span></div></td>
                     
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class20")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="20" startdate='<%#getStartDate(20)%>' enddate='<%#getEndDate(20)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon20V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class21")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="21" startdate='<%#getStartDate(21)%>' enddate='<%#getEndDate(21)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon21V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class22")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="22" startdate='<%#getStartDate(22)%>' enddate='<%#getEndDate(22)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon22V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class23")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="23" startdate='<%#getStartDate(23)%>' enddate='<%#getEndDate(23)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon23V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class24")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="24" startdate='<%#getStartDate(24)%>' enddate='<%#getEndDate(24)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon24V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class25")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="25" startdate='<%#getStartDate(25)%>' enddate='<%#getEndDate(25)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon25V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class26")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="26" startdate='<%#getStartDate(26)%>' enddate='<%#getEndDate(26)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon26V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class27")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="27" startdate='<%#getStartDate(27)%>' enddate='<%#getEndDate(27)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon27V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class28")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="28" startdate='<%#getStartDate(28)%>' enddate='<%#getEndDate(28)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon28V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class29")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="29" startdate='<%#getStartDate(29)%>' enddate='<%#getEndDate(29)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon29V"))%></span></div></td>
                     
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class30")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="30" startdate='<%#getStartDate(30)%>' enddate='<%#getEndDate(30)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon30V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class31")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="31" startdate='<%#getStartDate(31)%>' enddate='<%#getEndDate(31)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon31V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class32")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="32" startdate='<%#getStartDate(32)%>' enddate='<%#getEndDate(32)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon32V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class33")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="33" startdate='<%#getStartDate(33)%>' enddate='<%#getEndDate(33)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon33V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class34")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="34" startdate='<%#getStartDate(34)%>' enddate='<%#getEndDate(34)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon34V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class35")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="35" startdate='<%#getStartDate(35)%>' enddate='<%#getEndDate(35)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon35V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class36")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="36" startdate='<%#getStartDate(36)%>' enddate='<%#getEndDate(36)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon36V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class37")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="37" startdate='<%#getStartDate(37)%>' enddate='<%#getEndDate(37)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon37V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class38")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="38" startdate='<%#getStartDate(38)%>' enddate='<%#getEndDate(38)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon38V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class39")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="39" startdate='<%#getStartDate(39)%>' enddate='<%#getEndDate(39)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon39V"))%></span></div></td>
                     
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class40")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="40" startdate='<%#getStartDate(40)%>' enddate='<%#getEndDate(40)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon40V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class41")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="41" startdate='<%#getStartDate(41)%>' enddate='<%#getEndDate(41)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon41V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class42")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="42" startdate='<%#getStartDate(42)%>' enddate='<%#getEndDate(42)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon42V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class43")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="43" startdate='<%#getStartDate(43)%>' enddate='<%#getEndDate(43)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon43V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class44")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="44" startdate='<%#getStartDate(44)%>' enddate='<%#getEndDate(44)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon44V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class45")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="45" startdate='<%#getStartDate(45)%>' enddate='<%#getEndDate(45)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon45V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class46")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="46" startdate='<%#getStartDate(46)%>' enddate='<%#getEndDate(46)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon46V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class47")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="47" startdate='<%#getStartDate(47)%>' enddate='<%#getEndDate(47)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon47V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class48")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="48" startdate='<%#getStartDate(48)%>' enddate='<%#getEndDate(48)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon48V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class49")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="49" startdate='<%#getStartDate(49)%>' enddate='<%#getEndDate(49)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon49V"))%></span></div></td>
                     
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class50")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="50" startdate='<%#getStartDate(50)%>' enddate='<%#getEndDate(50)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon50V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class51")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="51" startdate='<%#getStartDate(51)%>' enddate='<%#getEndDate(51)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon51V"))%></span></div></td>
    <td style="width:20px;text-align:center;"><div class='myradio1 <%#Eval("Class52")%>' vesselcode='<%#Eval("vesselcode")%>' dtid='<%#Eval("dtid")%>' recordtype='<%#Eval("RecordType")%>' week="52" startdate='<%#getStartDate(52)%>' enddate='<%#getEndDate(52)%>' year='<%#Eval("PYear")%>'><span class='ondate'><%#getDate(Eval("Mon52V"))%></span></div></td>
    <td style="width:20px;"></td>
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
