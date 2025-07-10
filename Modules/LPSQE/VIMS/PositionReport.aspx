<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PositionReport.aspx.cs" Inherits="PositionReport"  Async="true" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/Modules/LPSQE/VIMS/VIMSMenu.ascx" tagname="VIMSMenu" tagprefix="mtm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
  <title>KPI</title>
  <script type="text/javascript" src="../eReports/JS/jquery.min.js"></script>
  <script src="../eReports/JS/AutoComplete/knockout-2.2.1.js" type="text/javascript"></script>
  <script type="text/javascript" src="../eReports/JS/KPIScript.js"></script>
  
  <link rel="stylesheet" href="../eReports/JS/AutoComplete/jquery-ui.css" />
  <script src="../eReports/JS/AutoComplete/jquery-ui.js" type="text/javascript"></script>
  
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
</style>

</head>

<body>
<form id="form" runat="server">
<asp:ToolkitScriptManager  ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
<mtm:VIMSMenu runat="server" ID="VIMSMenu1" />
<table width="100%" border="0" cellpadding="3" cellspacing="0" style="background-color:#CCEEF9; border:solid 1px #00ABE1; margin-bottom:3px;">
<tr>
<td width="100px" style="text-align:right">From Date :</td>
<td width="100px">
    <asp:TextBox runat="server" id="txtFDate" CssClass="user-input-nopadding" Width="90px" MaxLength="15" ></asp:TextBox>
    <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFDate" Format="dd-MMM-yyyy" runat="server"></asp:CalendarExtender>
</td>
<td width="100px" style="text-align:right">To Date :</td>
<td width="100px">
    <asp:TextBox runat="server" id="txtTDate" CssClass="user-input-nopadding" Width="90px" MaxLength="15" ></asp:TextBox>
    <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtTDate" Format="dd-MMM-yyyy" runat="server"></asp:CalendarExtender>
</td>
<td width="100px" style="text-align:right">Voyage # :</td>
<td width="200px">
    <asp:TextBox runat="server" id="txtVoyageNo" CssClass="user-input-nopadding" Width="100%" MaxLength="50" ></asp:TextBox>    
</td>
<td width="100px" style="text-align:right">Report Type :</td>
<td width="120px" style="text-align:left">
    <asp:DropDownList runat="server" ID="ddlReportType" Width="110px" >
        <asp:ListItem Text="< All >" Value=""></asp:ListItem>
        <asp:ListItem Text="Arrival" Value="A"></asp:ListItem>
        <asp:ListItem Text="Departure" Value="D"></asp:ListItem>
        <asp:ListItem Text="Noon" Value="N"></asp:ListItem>
        <asp:ListItem Text="Port-Anchorage" Value="PA"></asp:ListItem>
        <asp:ListItem Text="Port-Berthing" Value="PB"></asp:ListItem>
        <asp:ListItem Text="Port-Drift" Value="PD"></asp:ListItem>
    </asp:DropDownList> 
</td>

<td width="70px" style="text-align:right">Location :</td>
<td width="70px" style="text-align:left">
    <asp:DropDownList runat="server" ID="ddlLocation" Width="70px" >
        <asp:ListItem Text="< All >" Value=""></asp:ListItem>
        <asp:ListItem Text="At Sea" Value="A"></asp:ListItem>
        <asp:ListItem Text="In Port" Value="I"></asp:ListItem>        
    </asp:DropDownList> 
</td>

<td style="text-align:right">
      <asp:Button runat="server" ID="btnSearch" Text=" Search " onclick="btnSearch_Click" CssClass="btn" />
      <asp:Button runat="server" ID="btnNewReport" Text="Add New Report" onclick="btnNewReport_Click" CssClass="btn" />
</td>
</tr>
</table>
<asp:TabContainer ID="t11" runat="server">
<asp:TabPanel HeaderText="Position" runat="server" >
<HeaderTemplate><div style="font-weight:bold;">Reports</div></HeaderTemplate>
<ContentTemplate>
<div style="height:33px; overflow-y:scroll;overflow-x:hidden;border:solid 1px #00ABE1; border-bottom:none;">
<table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color:#00ABE1; border-collapse:collapse;" bordercolor="white">
<thead>

<tr style='color:White; height:25px; font-size:11px;'>
        <td style="width:70px;text-align:center;color:White;">View</td>        
        <td style="width:120px;text-align:center;color:White;">Voyage#</td>
        <td style="width:100px;color:White;text-align:center;">Report Date</td>        
        <td style="width:120px;color:White;">Report Type</td>
       <td style="width:300px;color:White;">Location</td> 
        <td style="width:200px;color:White;">Dep Port</td>
        <td style="color:White;">Arrival Port</td>
        <td style="width:100px;color:White;">Arrival Port ETA</td>
        <td style="width:100px;color:White;">Send for Export</td>
        <td style="width:30px;">&nbsp;</td>
</tr>
</thead>
</table>
</div>
<div style="height:360px; border-bottom:none; border:solid 1px #00ABE1; overflow-x:hidden; overflow-y:scroll;font-size:11px;" class='ScrollAutoReset' id='dv_LFI_List'>
<table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color:#F5FCFE; border-collapse:collapse;" class='newformat'>
 <tbody>
<asp:Repeater runat="server" ID="rptPR">
<ItemTemplate>
<tr onmouseover="">
       
      <td style="width:70px;text-align:center;">
        <asp:ImageButton runat="server" ID="btnView" ImageUrl="~/Images/magnifier.png" OnClick="btnView_Click" CommandArgument='<%#Eval("ReportId").ToString() + "~" + Eval("VESSELID").ToString() + "~" + Eval("ReportTypeCode").ToString() %>'/>
      </td>
      <td style="width:120px;text-align:center;"><%#Eval("VoyageNo")%></td>
      <td style="width:100px;text-align:center;"><%#Common.ToDateString(Eval("ReportDate"))%></td>
      <td style="width:120px;text-align:left;"><%#Eval("ReportType")%></td>
      <td style="width:300px;text-align:left;"><%#Eval("Lattitude1").ToString() + "&#176 " + Eval("Lattitude2").ToString() + "&#96 " + GetLattitude(Eval("Lattitude3")) + " - " + Eval("Longitude1").ToString() + "&#176 " + Eval("Longitud2").ToString() + "&#96 " + GetLongitude(Eval("Longitud3"))%></td>
      <td style="width:200px;text-align:left;"><%#Eval("DepPort")%></td>
      <td style="text-align:left;"><%#Eval("DepArrivalPort")%></td>
      <td style="width:100px;text-align:center;"><%#Common.ToDateString(Eval("ArrivalPortETA"))%></td>
      <td style="width:100px;text-align:center;"><asp:ImageButton ID="btnExport" voyNo='<%#Eval("VoyageNo")%>' ToolTip="Send for Export" ImageUrl="~/Images/export.gif" CommandArgument='<%#Eval("ReportsPK").ToString() + "~" + Eval("VESSELID").ToString() + "~" + Eval("ReportTypeCode").ToString() %>' runat="server" OnClick="btnExport_Click" /> </td>
      <td style="width:30px;"><b>&nbsp;</b></td>
</tr>
</ItemTemplate>
</asp:Repeater>
</tbody>
</table>
</div>
</ContentTemplate>
</asp:TabPanel>
<asp:TabPanel HeaderText="Fuel & Lube" runat="server"  >
<HeaderTemplate><div style="font-weight:bold;">Consumption</div></HeaderTemplate>
<ContentTemplate>
<asp:Accordion ID="Acd11" runat="server">
 <Panes>
        <asp:AccordionPane ID="apFuel"  runat="server">
          <Header>
           <div style="padding:4px; background-color:#C2E0FF; border:1px solid #00ABE1;">
            &#8226; Fuel Consumption
           </div>
           </Header>
          <Content>
            <div style="height:66px; overflow-y:scroll;overflow-x:hidden;border:solid 1px #00ABE1; font-size:11px; border-bottom:none;">
            <table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color:#00ABE1; border-collapse:collapse;" bordercolor="white">
            <thead>
            <tr style='color:White; height:25px;'>
                    
                    <td style="width:90px;color:White;text-align:center;">Report Dt</td>        
                    <td style="width:100px;color:White;">Report Type</td>
                    <td style="color:White;">Port</td>
                    <td style="width:350px;text-align:center;">IFO(<= 3.5% Sulphur) (MT)</td>
                    <td style="width:350px;text-align:center;">MGO(<0.10% Sulphur) (MT)</td>
                    <td style="width:350px;text-align:center;">MDO (MT)</td>                                        
                    <td style="width:20px;">&nbsp;</td>
            </tr>
            <tr style='color:White; height:25px;'>
                    <td style="width:90px;color:White;text-align:center;"></td>
                    <td style="width:100px;color:White;text-align:center;"></td> 
                    <td style="color:White;text-align:center;"></td>       
                    <td style="width:350px"> 
                         <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse;" >
                         <tr>
                            <td style="width:70px;text-align:right;">Recd.</td>                            
                            <td style="width:70px;text-align:right;">ME</td>
                            <td style="width:70px;text-align:right;">AE</td>
                            <td style="width:70px;text-align:right;">Boiler</td>
                            <td style="text-align:right;">ROB</td>
                                                     
                         </tr>
                         </table>
                    </td>
                    <td style="width:350px"> 
                         <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse;" >
                         <tr>
                            <td style="width:70px;text-align:right;">Recd.</td>
                            <td style="width:70px;text-align:right;">ME</td>
                            <td style="width:70px;text-align:right;">AE</td>
                            <td style="width:70px;text-align:right;">Boiler</td>
                            <td style="text-align:right;">ROB</td>                                                     
                         </tr>
                         </table>
                    </td>
                    <td style="width:350px">  
                         <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse;" >
                         <tr>
                            <td style="width:70px;text-align:right;">Recd.</td>
                            <td style="width:70px;text-align:right;">ME</td>
                            <td style="width:70px;text-align:right;">AE</td>
                            <td style="width:70px;text-align:right;">Boiler</td>
                            <td style="text-align:right;">ROB</td>                         
                         </tr>
                         </table>
                    </td>                     
                    <td style="width:20px;">&nbsp;</td>
            </tr>
            </thead>
            </table>
            </div>
            <div style="height:212px; border-bottom:none; border:solid 1px #00ABE1; overflow-x:hidden; overflow-y:scroll;" class='ScrollAutoReset' id='Div1'>
            <table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color:#F5FCFE; border-collapse:collapse;" class='newformat'>
            <tbody>
            <asp:Repeater runat="server" ID="rptFuel">
            <ItemTemplate>
            <tr onmouseover="">      
                    <td style="width:90px;text-align:center; font-size:9px;"><%#Common.ToDateString(Eval("ReportDate"))%></td>
                    <td style="width:100px;text-align:left;font-size:9px;"><%#Eval("ReportType")%></td>
                    <td style="text-align:left;font-size:9px;"><%#Eval("PortName")%></td>                  
                    <td style="width:350px;" >
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse; font-size:9px;" >
                         <tr>
                             <td style="width:70px;text-align:right; "><%#Eval("ROBIFO45Recv")%>&nbsp;</td>
                             <td style="width:70px;text-align:right; "><%#Eval("MEIFO45")%>&nbsp;</td>
                             <td style="width:70px;text-align:right; "><%#Eval("AEIFO45")%>&nbsp;</td>
                             <td style="width:70px;text-align:right; "><%#Eval("CargoHeatingIFO45")%>&nbsp;</td>
                             <td style="text-align:right; "><%#Eval("ROBIFO45")%>&nbsp;</td>
                             
                         </tr>
                        </table>
                    </td>
                    <td style="width:350px;" >
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse; font-size:9px;" >
                         <tr>
                            <td style="width:70px;text-align:right;"><%#Eval("ROBMGO1Recv")%>&nbsp;</td>                    
                            <td style="width:70px;text-align:right; "><%#Eval("MEMGO1")%>&nbsp;</td>
                            <td style="width:70px;text-align:right; "><%#Eval("AEMGO1")%>&nbsp;</td>
                            <td style="width:70px;text-align:right; "><%#Eval("CargoHeatingMGO1")%>&nbsp;</td>
                            <td style="text-align:right; "><%#Eval("ROBMGO1")%>&nbsp;</td>                            
                         </tr>
                        </table>
                    </td>
                    <td style="width:350px;" >
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse; font-size:9px;" >
                         <tr>
                            <td style="width:70px;text-align:right; "><%#Eval("RobMDORecv")%>&nbsp;</td>                    
                            <td style="width:70px;text-align:right; "><%#Eval("MEMDO")%>&nbsp;</td>
                            <td style="width:70px;text-align:right; "><%#Eval("AEMDO")%>&nbsp;</td>
                            <td style="width:70px;text-align:right; "><%#Eval("CargoHeatingMDO")%>&nbsp;</td>
                            <td style="text-align:right; "><%#Eval("ROBMDO")%>&nbsp;</td>                                                         
                         </tr>
                        </table>
                    </td>
                    <td style="width:20px;"><b>&nbsp;</b></td>
            </tr>          
            </ItemTemplate>
            </asp:Repeater>
            </tbody>
            </table>
            </div>

            <div style="height:30px; overflow-y:scroll;overflow-x:hidden;border:solid 1px #00ABE1; font-size:11px; border-bottom:none;">
            <table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color:#00ABE1; border-collapse:collapse;height:30px;" bordercolor="white">
            <thead>            
            <tr style='color:White; height:25px;'>
                    <td style="width:90px;color:White;text-align:center;"></td>
                    <td style="width:100px;color:White;text-align:center;"></td> 
                    <td style="color:White;text-align:center;"></td>       
                    <td style="width:350px"> 
                         <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse;" >
                         <tr>
                            <td style="width:70px;text-align:right;"><asp:Label ID="lblSumIFORecd" runat="server"></asp:Label> </td>                            
                            <td style="width:70px;text-align:right;"><asp:Label ID="lblSumIFOME" runat="server"></asp:Label></td>
                            <td style="width:70px;text-align:right;"><asp:Label ID="lblSumIFOAE" runat="server"></asp:Label></td>
                            <td style="width:70px;text-align:right;"><asp:Label ID="lblSumIFOBoiler" runat="server"></asp:Label></td>
                            <td style="text-align:right;">&nbsp;</td>
                                                     
                         </tr>
                         </table>
                    </td>
                    <td style="width:350px"> 
                         <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse;" >
                         <tr>
                            <td style="width:70px;text-align:right;"><asp:Label ID="lblSumMGORecd" runat="server"></asp:Label> </td>                            
                            <td style="width:70px;text-align:right;"><asp:Label ID="lblSumMGOME" runat="server"></asp:Label></td>
                            <td style="width:70px;text-align:right;"><asp:Label ID="lblSumMGOAE" runat="server"></asp:Label></td>
                            <td style="width:70px;text-align:right;"><asp:Label ID="lblSumMGOBoiler" runat="server"></asp:Label></td>
                            <td style="text-align:right;">&nbsp;</td>                                                     
                         </tr>
                         </table>
                    </td>
                    <td style="width:350px">  
                         <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse;" >
                         <tr>
                            <td style="width:70px;text-align:right;"><asp:Label ID="lblSumMDORecd" runat="server"></asp:Label> </td>                            
                            <td style="width:70px;text-align:right;"><asp:Label ID="lblSumMDOME" runat="server"></asp:Label></td>
                            <td style="width:70px;text-align:right;"><asp:Label ID="lblSumMDOAE" runat="server"></asp:Label></td>
                            <td style="width:70px;text-align:right;"><asp:Label ID="lblSumMDOBoiler" runat="server"></asp:Label></td>
                            <td style="text-align:right;">&nbsp;</td>                         
                         </tr>
                         </table>
                    </td>                     
                    <td style="width:20px;">&nbsp;</td>
            </tr>
            </thead>
            </table>
            </div>          
          </Content>        
        </asp:AccordionPane>
        <asp:AccordionPane ID="apLube" runat="server">
         <Header>
         <div style="padding:4px; background-color:#C2E0FF; border:1px solid #00ABE1;">
         &#8226; Lube Consumption
         </div>
         </Header>
          <Content>
          <div style="height:66px; overflow-y:scroll;overflow-x:hidden;border:solid 1px #00ABE1; font-size:11px; border-bottom:none;">
            <table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color:#00ABE1; border-collapse:collapse;" bordercolor="white">
            <thead>
            <tr style='color:White; height:25px;'>
                    
                    <td style="width:100px;color:White;text-align:center;">Report Date</td>        
                    <td style="width:100px;color:White;">Report Type</td>
                    <td style="color:White;">Port</td>
                    <td style="width:240px;text-align:center;">MECC (LTR)</td>
                    <td style="width:240px;text-align:center;">MECYL (LTR)</td>
                    <td style="width:240px;text-align:center;">AECC (LTR)</td>
                    <td style="width:240px;text-align:center;">HYD (LTR)</td>                                        
                    <td style="width:20px;">&nbsp;</td>
            </tr>
            <tr style='color:White; height:25px;'>
                    <td style="width:100px;color:White;text-align:center;"></td>
                    <td style="width:100px;color:White;text-align:center;"></td>
                    <td style="color:White;text-align:center;"></td>        
                    
                    <td > 
                         <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse;" >
                         <tr>
                            <td style="width:80px; text-align:right;">Recd.</td>
                            <td style="width:80px; text-align:right;">Cons.</td>                            
                            <td style="text-align:right;">ROB</td>                                                   
                         </tr>
                         </table>
                    </td>
                    <td > 
                         <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse;" >
                         <tr>
                            <td style="width:80px; text-align:right;">Recd.</td>
                            <td style="width:80px; text-align:right;">Cons.</td>                            
                            <td style="text-align:right;">ROB</td>                         
                         </tr>
                         </table>
                    </td>
                    <td > 
                         <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse;" >
                         <tr>
                            <td style="width:80px; text-align:right;">Recd.</td>
                            <td style="width:80px; text-align:right;">Cons.</td>                            
                            <td style="text-align:right;">ROB</td>                       
                         </tr>
                         </table>
                    </td>
                    <td > 
                         <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse;" >
                         <tr>
                            <td style="width:80px; text-align:right;">Recd.</td>
                            <td style="width:80px; text-align:right;">Cons.</td>                            
                            <td style="text-align:right;">ROB</td>                         
                         </tr>
                         </table>
                    </td>                     
                                              
                    <td style="width:20px;">&nbsp;</td>
            </tr>
            </thead>
            </table>
            </div>
            <div style="height:212px; border-bottom:none; border:solid 1px #00ABE1; overflow-x:hidden; overflow-y:scroll;" class='ScrollAutoReset' id='Div2'>
            <table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color:#F5FCFE; border-collapse:collapse;" class='newformat'>
                <tbody>
            <asp:Repeater runat="server" ID="rptLube">
            <ItemTemplate>
            <tr onmouseover="">      
                    
                    <td style="width:100px;text-align:center;font-size:9px;"><%#Common.ToDateString(Eval("ReportDate"))%></td>
                    <td style="width:100px;text-align:left;font-size:9px;"><%#Eval("ReportType")%></td> 
                    <td style="text-align:left;font-size:9px;"><%#Eval("PortName")%></td>                 
                    <td style="width:240px;" >
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse; font-size:9px;" >
                         <tr>
                             <td style="width:80px;text-align:right;"><%#Eval("ROBMECCRecv")%>&nbsp;</td>
                             <td style="width:80px;text-align:right;"><%#Eval("MECCLube")%>&nbsp;</td>                             
                             <td style="text-align:right;"><%#Eval("ROBMECC")%>&nbsp;</td>
                         </tr>
                        </table>
                    </td>
                    <td style="width:240px;" >
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse; font-size:9px;" >
                         <tr>                    
                            <td style="width:80px;text-align:right;"><%#Eval("ROBMECYLRecv")%>&nbsp;</td>
                            <td style="width:80px;text-align:right;"><%#Eval("MECYLLube")%>&nbsp;</td>                            
                            <td style="text-align:right;"><%#Eval("ROBMECYL")%>&nbsp;</td>
                         </tr>
                        </table>
                    </td>
                    <td style="width:240px;" >
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse; font-size:9px;" >
                         <tr>                    
                            <td style="width:80px;text-align:right;"><%#Eval("ROBAECCRecv")%>&nbsp;</td>
                            <td style="width:80px;text-align:right;"><%#Eval("AECCLube")%>&nbsp;</td>                            
                            <td style="text-align:right;"><%#Eval("ROBAECC")%>&nbsp;</td>
                         </tr>
                        </table>
                    </td>
                    <td style="width:240px;" >
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse; font-size:9px;" >
                         <tr>                    
                            <td style="width:80px;text-align:right;"><%#Eval("ROBHYDRecv")%>&nbsp;</td>
                            <td style="width:80px;text-align:right;"><%#Eval("HYDLube")%>&nbsp;</td>                            
                            <td style="text-align:right;"><%#Eval("ROBHYD")%>&nbsp;</td>                             
                         </tr>
                        </table>
                    </td>
                    <td style="width:20px;"><b>&nbsp;</b></td>
            </tr>          
            </ItemTemplate>
            </asp:Repeater>
            </tbody>
            </table>
            </div>
          
            <div style="height:33px; overflow-y:scroll;overflow-x:hidden;border:solid 1px #00ABE1; font-size:11px; border-bottom:none;">
            <table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color:#00ABE1; border-collapse:collapse;" bordercolor="white">
            <thead>            
            <tr style='color:White; height:25px;'>
                    <td style="width:100px;color:White;text-align:center;"></td>
                    <td style="width:100px;color:White;text-align:center;"></td>
                    <td style="color:White;text-align:center;"></td>        
                    
                    <td style="width:240px;"> 
                         <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse;" >
                         <tr>
                            <td style="width:80px; text-align:right;"><asp:Label ID="lblSumMECCRecd" runat="server"></asp:Label></td>
                            <td style="width:80px; text-align:right;"><asp:Label ID="lblSumMECCCons" runat="server"></asp:Label></td>                            
                            <td style="text-align:right;">&nbsp;</td>                                                   
                         </tr>
                         </table>
                    </td>
                    <td style="width:240px;"> 
                         <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse;" >
                         <tr>
                            <td style="width:80px; text-align:right;"><asp:Label ID="lblSumMECYLRecd" runat="server"></asp:Label></td>
                            <td style="width:80px; text-align:right;"><asp:Label ID="lblSumMECYLCons" runat="server"></asp:Label></td>                            
                            <td style="text-align:right;">&nbsp;</td>                         
                         </tr>
                         </table>
                    </td>
                    <td style="width:240px;"> 
                         <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse;" >
                         <tr>
                            <td style="width:80px; text-align:right;"><asp:Label ID="lblSumAECCRecd" runat="server"></asp:Label></td>
                            <td style="width:80px; text-align:right;"><asp:Label ID="lblSumAECCCons" runat="server"></asp:Label></td>                            
                            <td style="text-align:right;">&nbsp;</td>                       
                         </tr>
                         </table>
                    </td>
                    <td style="width:240px;"> 
                         <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse;" >
                         <tr>
                            <td style="width:80px; text-align:right;"><asp:Label ID="lblSumHYDRecd" runat="server"></asp:Label></td>
                            <td style="width:80px; text-align:right;"><asp:Label ID="lblSumHYDCons" runat="server"></asp:Label></td>                            
                            <td style="text-align:right;">&nbsp;</td>                         
                         </tr>
                         </table>
                    </td>                     
                                              
                    <td style="width:20px;">&nbsp;</td>
            </tr>
            </thead>
            </table>
            </div>
          </Content>
        
        </asp:AccordionPane>
        <asp:AccordionPane ID="apFreshwater" runat="server">
         <Header>
          <div style="padding:4px; background-color:#C2E0FF; border:1px solid #00ABE1;">
          &#8226; Fresh Water Consumption
          </div>
          </Header>
          <Content>
               <div style="height:33px; overflow-y:scroll;overflow-x:hidden;border:solid 1px #00ABE1; font-size:11px; border-bottom:none;">
            <table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color:#00ABE1; border-collapse:collapse;" bordercolor="white">
            <thead>
            <tr style='color:White; height:25px;'>
                    
                    <td style="width:100px;color:White;text-align:center;">Report Date</td>        
                    <td style="width:100px;color:White;">Report Type</td>
                    <td style="color:White;">Port</td>
                    <td style="width:200px;text-align:center;">Generated (MT)</td>
                    <td style="width:200px;text-align:center;">Received (MT)</td>
                    <td style="width:200px;text-align:center;">Consumed (MT)</td>
                    <td style="width:200px;text-align:center;">ROB (MT)</td>                                        
                    <td style="width:20px;">&nbsp;</td>
            </tr>
            </thead>
            </table>
            </div>
            <div style="height:247px; border-bottom:none; border:solid 1px #00ABE1; overflow-x:hidden; overflow-y:scroll;" class='ScrollAutoReset' id='Div3'>
            <table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color:#F5FCFE; border-collapse:collapse; font-size:9px;" class='newformat'>
                <tbody>
            <asp:Repeater runat="server" ID="rptFW">
            <ItemTemplate>
            <tr onmouseover="">      
                    
                    <td style="width:100px;text-align:center;"><%#Common.ToDateString(Eval("ReportDate"))%></td>
                    <td style="width:100px;text-align:left;"><%#Eval("ReportType")%></td> 
                    <td style="text-align:left;"><%#Eval("PortName")%></td>                   
                    <td style="width:200px;text-align:right;" ><%#Eval("GeneratedFreshWater")%>&nbsp;</td>
                    <td style="width:200px;text-align:right;" ><%#Eval("RobFesshWaterRecv")%>&nbsp;</td>
                    <td style="width:200px;text-align:right;" ><%#Eval("ConsumedFreshWater")%>&nbsp;</td>
                    <td style="width:200px;text-align:right;"><%#Eval("ROBFesshWater")%>&nbsp;</td>
                    <td style="width:20px;"><b>&nbsp;</b></td>
            </tr>          
            </ItemTemplate>
            </asp:Repeater>
            </tbody>
            </table>
            </div>

            <div style="height:33px; overflow-y:scroll;overflow-x:hidden;border:solid 1px #00ABE1; font-size:11px; border-bottom:none;">
            <table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color:#00ABE1; border-collapse:collapse;" bordercolor="white">
            <thead>
            <tr style='color:White; height:25px;'>
                    
                    <td style="width:100px;color:White;text-align:center;">&nbsp;</td>        
                    <td style="width:100px;color:White;">&nbsp;</td>
                    <td style="color:White;">&nbsp;</td>
                    <td style="width:200px;text-align:right;"><asp:Label ID="lblSumFWGenerated" runat="server"></asp:Label>&nbsp;</td>
                    <td style="width:200px;text-align:right;"><asp:Label ID="lblSumFWReceived" runat="server"></asp:Label>&nbsp;</td>
                    <td style="width:200px;text-align:right;"><asp:Label ID="lblSumFWConsumed" runat="server"></asp:Label>&nbsp;</td>
                    <td style="width:200px;text-align:right;">&nbsp;</td>                                        
                    <td style="width:20px;">&nbsp;</td>
            </tr>
            </thead>
            </table>
            </div>
          
          </Content>
        
        </asp:AccordionPane>
 </Panes>

</asp:Accordion>

</ContentTemplate>
</asp:TabPanel>
<asp:TabPanel HeaderText="Performance" runat="server"  >
<HeaderTemplate><div style="font-weight:bold;">Performance</div></HeaderTemplate>
<ContentTemplate>
<div id="dv1" ></div>
<div style="height:66px; width:1200px; overflow-y:scroll;overflow-x:hidden;border:solid 1px #00ABE1;" id='Div41'>
<table width="2030px" border="1" cellpadding="3" cellspacing="0" style="background-color:#00ABE1; border-collapse:collapse; font-size:11px;" bordercolor="white">
<thead>
<tr style='color:White; height:25px;'>
          <td style="width:90px;color:White;text-align:center;"></td> 
        <td style="width:100px;text-align:center;color:White;" colspan="2">Exh Temp</td>
        <td style="width:50px;text-align:center;color:White;"></td>

        <td style="width:80px;text-align:center;color:White;"></td>        
        <td style="width:40px;text-align:center;color:White;"></td>
        <td style="width:80px;text-align:center;color:White;"></td>
        <td style="width:200px;text-align:center;color:White;" colspan="2">ME</td>
        <td style="width:140px;text-align:center;color:White;" colspan="2">ME T/C RPM </td>
        <td style="width:160px;text-align:center;color:White;" colspan="2">Pressure</td>        
        <td style="width:240px;text-align:center;color:White;" colspan="3">Temp</td>
        <td style="width:65px;text-align:center;color:White;">Bilge Pump</td>
        <td style="width:240px;text-align:center;color:White;" colspan="4">Aux Load(KW)</td> 
        <td style="width:100px;text-align:center;color:White;">Total Aux Load</td>
        <td style="width:160px;text-align:center;color:White;" colspan="4">A/E (Hrs)</td>
        <td style="text-align:center;color:White;"></td>       
               
        <td style="width:20px;">&nbsp;</td>
</tr>
<tr style='color:White; height:25px;'>
    

        <td style="width:90px;color:White;text-align:center;">Report Date</td> 
        <td style="width:50px;text-align:center;color:White;">Min</td>
        <td style="width:50px;text-align:center;color:White;">Max</td>
        <td style="width:50px;text-align:center;color:White;">ME RPM</td>

        <td style="width:70px;text-align:center;color:White;">Engine Dist.</td>        
        <td style="width:40px;text-align:center;color:White;">Slip</td>
        <td style="width:80px;text-align:center;color:White;">Output</td>
        <td style="width:100px;text-align:center;color:White;">Thermal Load</td>        
        <td style="width:100px;text-align:center;color:White;">Load Indicator</td>
        <td style="width:70px;text-align:center;color:White;">No. 1</td>
        <td style="width:70px;text-align:center;color:White;">No. 2</td>
        <td style="width:80px;text-align:center;color:White;">ME SCAV </td>        
        <td style="width:80px;text-align:center;color:White;">Lube Oil </td>
        <td style="width:80px;text-align:center;color:White;">SCAV</td>
        <td style="width:80px;text-align:center;color:White;">Sea Water</td>        
        <td style="width:80px;text-align:center;color:White;">Eng. Room</td>
        <td style="width:65px;text-align:center;color:White;">(Hrs run)</td>        
        <td style="width:60px;text-align:center;color:White;">1</td>
        <td style="width:60px;text-align:center;color:White;">2</td>
        <td style="width:60px;text-align:center;color:White;">3</td>
        <td style="width:60px;text-align:center;color:White;">4</td>
        <td style="width:100px;text-align:center;color:White;">(KW)</td> 
        <td style="width:40px;text-align:center;color:White;">No 1</td>
        <td style="width:40px;text-align:center;color:White;">No 2</td>
        <td style="width:40px;text-align:center;color:White;">No 3</td>
        <td style="width:40px;text-align:center;color:White;">No 4</td>
        <td style="text-align:center;color:White;">INLET TEMP</td>                
        <td style="width:20px;">&nbsp;</td>
</tr>
</thead>
</table>
</div>
<div style="height:320px;width:1200px; border-bottom:none; border:solid 1px #00ABE1; overflow-x:scroll; overflow-y:scroll; font-size:11px;" class='ScrollAutoReset' id='Div4'>
<table width="2030px"  border="1" cellpadding="3" cellspacing="0" style="background-color:#F5FCFE; border-collapse:collapse;" class='newformat'>
 <tbody>
<asp:Repeater runat="server" ID="rptPerformance">
<ItemTemplate>
<tr onmouseover="">
      <td style="width:90px;text-align:center;"><%#Common.ToDateString(Eval("ReportDate"))%></td> 
      <td style="width:50px;text-align:right;">&nbsp;<%#Eval("ExhTempMin")%></td>
      <td style="width:50px;text-align:right;">&nbsp;<%#Eval("ExhTempMax")%></td>
      <td style="width:50px;text-align:right;">&nbsp;<%#Eval("MERPM")%></td>
      <td style="width:80px;text-align:right;">&nbsp;<%#Eval("EngineDistance")%></td>
      <td style="width:40px;text-align:right;">&nbsp;<%#Eval("Slip")%></td>
      <td style="width:80px;text-align:right;">&nbsp;<%#Eval("MEOutput")%></td>
      <td style="width:100px;text-align:right;">&nbsp;<%#Eval("METhermalLoad")%></td>
      <td style="width:100px;text-align:right;">&nbsp;<%#Eval("MELoadIndicator")%></td>
      <td style="width:70px;text-align:right;">&nbsp;<%#Eval("METCNo1RPM").ToString()%></td>
      <td style="width:70px;text-align:right;">&nbsp;<%#Eval("METCNo2RPM").ToString()%></td>
      <td style="width:80px;text-align:right;">&nbsp;<%#Eval("MESCAVPressure")%></td>
      <td style="width:80px;text-align:right;">&nbsp;<%#Eval("LubeOilPressure")%></td>
      <td style="width:80px;text-align:right;">&nbsp;<%#Eval("SCAVTEMP")%></td>
      
      <td style="width:80px;text-align:right;">&nbsp;<%#Eval("SeaWaterTemp")%></td>
      <td style="width:80px;text-align:right;">&nbsp;<%#Eval("EngRoomTemp")%></td>
      <td style="width:65px;text-align:right;">&nbsp;<%#Eval("BligPump")%></td>

      <td style="width:60px;text-align:right;">&nbsp;<%#Eval("AUX1Load")%></td>
      <td style="width:60px;text-align:right;">&nbsp;<%#Eval("AUX2Load")%></td>
      <td style="width:60px;text-align:right;">&nbsp;<%#Eval("AUX3Load")%></td>
      <td style="width:60px;text-align:right;">&nbsp;<%#Eval("AUX4Load")%></td>
      <td style="width:100px;text-align:right;">&nbsp;<%#Eval("TotalAUXLoad")%></td>

      <td style="width:40px;text-align:right;">&nbsp;<%#Eval("AENo1")%></td>
      <td style="width:40px;text-align:right;">&nbsp;<%#Eval("AENo2")%></td>
      <td style="width:40px;text-align:right;">&nbsp;<%#Eval("AENo3")%></td>
      <td style="width:40px;text-align:right;">&nbsp;<%#Eval("AENo4")%></td>
      <td style="text-align:right;">&nbsp;<%#Eval("AEFOInletTemp")%></td>
      
      <td style="width:20px;"><b>&nbsp;</b></td>
</tr>
</ItemTemplate>
</asp:Repeater>
</tbody>
</table>
</div>
</ContentTemplate>
</asp:TabPanel>
</asp:TabContainer>


<div style="position:absolute;top:0px;left:0px; height :470px; width:100%; " id="dv_AddNew" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:300px;  padding :0px; text-align :center;background : white; z-index:150;top:100px; border:solid 8px black;">
            <center >
             <div style="padding:6px; background-color:#00ABE1; font-size:14px; color:#fff;"><b>Choose Report Type</b></div>
                <br />
                <table cellpadding="3" cellspacing="0" width="100%">
                <tr>
                <td style="text-align:right; width:35%;">
                  <b> Report Type : &nbsp;</b>
                </td>
                <td style="text-align:left;"> 
                 <asp:DropDownList runat="server" ID="ddlAddReportType" ValidationGroup="vg"  Width="120px" ></asp:DropDownList>
                
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ValidationGroup="vg" ErrorMessage="*" ControlToValidate="ddlAddReportType" Display="Dynamic" ></asp:RequiredFieldValidator>
                </td>
                </tr>
                </table>
             </center>
             <br />
             <asp:Button runat="server" ID="btnSave" Text="Add Report" onclick="btnSave_Click" ValidationGroup="vg" style=" background-color:#00ABE1; color:White; border:solid 1px grey; width:100px;"/>
             <asp:Button runat="server" ID="btnClose" Text="Cancel" OnClick="btnClose_Click" CausesValidation="false" style=" background-color:Red; color:White; border:solid 1px grey;width:100px;"/>
             <br />
             <br />
        </div>
    </center>
    </div>

    <script language="javascript" type="text/javascript">
        function ScrollHeader() {
            $("#Div41").scrollLeft($("#Div4").scrollLeft());
        }
        $(document).ready(
        function () {            
            $("#Div4").width($("#dv1").width());
            $("#Div41").width($("#dv1").width());
            $("#Div4").scroll(function () {
                ScrollHeader();
            });
        }
        );
    </script>
</form>
</body>
</html>
