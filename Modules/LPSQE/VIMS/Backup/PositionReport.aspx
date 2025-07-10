<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PositionReport.aspx.cs" Inherits="PositionReport"  Async="true" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/Modules/LPSQE/VIMS/VIMSMenu.ascx" tagname="VIMSMenu" tagprefix="mtm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
  <title>Position Report</title>
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
</style>

</head>

<body>
<form id="form" runat="server">
<asp:ToolkitScriptManager  ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
<mtm:VIMSMenu runat="server" ID="VIMSMenu1" />
<table width="100%" border="0" cellpadding="3" cellspacing="0" style="background-color:#CCEEF9; border:solid 1px #00ABE1;">
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
<td width="100px" style="text-align:right">Activity Type :</td>
<td width="120px" style="text-align:left">
    <asp:DropDownList runat="server" ID="ddlReportType" Width="110px" >
        <asp:ListItem Text="< All >" Value=""></asp:ListItem>
        <asp:ListItem Text="Arrival" Value="A"></asp:ListItem>
        <asp:ListItem Text="Departure" Value="D"></asp:ListItem>
        <asp:ListItem Text="Noon" Value="N"></asp:ListItem>
        <asp:ListItem Text="Port-Anchorage" Value="PA"></asp:ListItem>
        <asp:ListItem Text="Port-Berthing" Value="PB"></asp:ListItem>
        <asp:ListItem Text="Port-Drift" Value="PD"></asp:ListItem>
        <asp:ListItem Text="Shifting" Value="SH"></asp:ListItem>
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
<table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse:collapse;font-size:12px;">
<tr style="color:white; font-weight:bold; background-color:#333399; height:20px">
<td style="padding:5px">Report</td>
<td style="padding:5px">Consumption & ROB </td>
<td style="padding:5px">Performance</td>
</tr>
<tr>
<td style="width:33%;vertical-align:top">
<div style="border-bottom:none;">
<table width="100%" cellpadding="5" border="1" style="border-collapse:collapse">
<thead>
<tr style="font-weight:bold; background-color:#002a80; color:White;">
        <td style="width:30px;text-align:center;color:White;">View</td>        
        <td style="width:120px;text-align:center;color:White;">Voyage#</td>
        <td style="width:100px;color:White;text-align:center;">Report Date</td>        
        <td style="width:100px;color:White;">Report Type</td>
        <td style="width:80px;color:White; text-align:center;">Export</td>
        <td style="width:10px;">&nbsp;</td>
</tr>
</thead>
</table>
</div>
<div style="height:357px; border-bottom:none;overflow-x:hidden; overflow-y:scroll;" class='ScrollAutoReset' id='dv_LFI_List'>
<table width="100%" cellpadding="5" border="1" style="border-collapse:collapse">
 <tbody>
<asp:Repeater runat="server" ID="rptPR">
<ItemTemplate>
<tr onmouseover="">
      <td style="width:30px;text-align:center;">
        <asp:ImageButton runat="server" ID="btnView" ImageUrl="~/Images/magnifier.png" OnClick="btnView_Click" CommandArgument='<%#Eval("REPORTSPK").ToString() + "~" + Eval("VESSELID").ToString() + "~" + Eval("ACTIVITY_CODE").ToString() %>' style='background-color:transparent; height:12px;'/>
      </td>
      <td style="width:120px;text-align:center;"><%#Eval("VoyageNo")%></td>
      <td style="width:100px;text-align:center;"><%#Common.ToDateString(Eval("ReportDate"))%></td>
      <td style="width:100px;text-align:left;"><%#Eval("ACTIVITY_NAME")%></td>
      <td style="width:80px;text-align:center;"><asp:ImageButton ID="btnExport" voyNo='<%#Eval("VoyageNo")%>' ToolTip="Export" ImageUrl="~/Images/export.gif" CommandArgument='<%#Eval("REPORTSPK").ToString() + "~" + Eval("VESSELID").ToString() + "~" + Eval("ACTIVITY_CODE").ToString()%>' runat="server" OnClick="btnExport_Click" style='background-color:transparent;height:12px;'/> </td>
      <td style="width:10px;"><b>&nbsp;</b></td>
</tr>
</ItemTemplate>
</asp:Repeater>
</tbody>
</table>
</div>
</td>
<td style="vertical-align:top">
<table width="100%" cellpadding="5" border="1" style="border-collapse:collapse">
<colgroup>
<col style="text-align:left" />
<col style="text-align:right" />
<col style="text-align:right" />
<col style="text-align:right" />
</colgroup>
<tr style="font-weight:bold; background-color:#002a80; color:White;">
<td style="width:40%">Item Name</td>
<td style="width:20%">Received</td>
<td style="width:20%">Consumption</td>
<td style="width:20%">ROB</td>
</tr>
<tr style="font-weight:bold; background-color:#6699ff; color:White;">
<td colspan="4" style="font-weight:bold; border-bottom:solid 1px gray; text-align:left;">Fuel 
    ( MT )</td>
</tr>
<tr>
<td>IFO(<= 3.5% Sulphur)</td>
<td><asp:Label ID="lblIFORec" runat="server"></asp:Label></td>
<td><asp:Label ID="lblIFOTotal" runat="server"></asp:Label></td>
<td><asp:Label ID="lblROBIFOTotal" runat="server"></asp:Label></td>
</tr>
<tr>
<td>MGO(<0.10% Sulphur)</td>
<td><asp:Label ID="lblMGORec" runat="server"></asp:Label></td>
<td><asp:Label ID="lblMGOTotal" runat="server"></asp:Label></td>
<td><asp:Label ID="lblROBMGOTotal" runat="server"></asp:Label></td>
</tr>
<tr>
<td>MDO</td>
<td><asp:Label ID="lblMDORec" runat="server"></asp:Label></td>
<td><asp:Label ID="lblMDOTotal" runat="server"></asp:Label></td>
<td><asp:Label ID="lblROBMDOTotal" runat="server"></asp:Label></td>
</tr>

<tr style="font-weight:bold; background-color:#6699ff; color:White;">
<td colspan="4" style="font-weight:bold; border-bottom:solid 1px gray;text-align:left;">Lube 
    (&nbsp; LTR )</td>
</tr>
<tr>
<td>MECC </td>
<td><asp:Label ID="lblMECCRec" runat="server"></asp:Label></td>
<td><asp:Label ID="lblMECCTotal" runat="server"></asp:Label></td>
<td><asp:Label ID="lblROBMECCTotal" runat="server"></asp:Label></td>
</tr>
<tr>
<td>MECYL </td>
<td><asp:Label ID="lblMECYLRec" runat="server"></asp:Label></td>
<td><asp:Label ID="lblMECYLTotal" runat="server"></asp:Label></td>
<td><asp:Label ID="lblROBMECYLTotal" runat="server"></asp:Label></td>
</tr>
<tr>
<td>AECC </td>
<td><asp:Label ID="lblAECCRec" runat="server"></asp:Label></td>
<td><asp:Label ID="lblAECCTotal" runat="server"></asp:Label></td>
<td><asp:Label ID="lblROBAECCTotal" runat="server"></asp:Label></td>
</tr>
<tr>
<td>HYD  </td>
<td><asp:Label ID="lblHYDRec" runat="server"></asp:Label></td>
<td><asp:Label ID="lblHYDTotal" runat="server"></asp:Label></td>
<td><asp:Label ID="lblROBHYDTotal" runat="server"></asp:Label></td>
</tr>
<tr style="font-weight:bold; background-color:#6699ff; color:White;">
<td colspan="4" style="font-weight:bold; border-bottom:solid 1px gray;text-align:left;">Water 
    ( MT )</td>
</tr>
<tr>
<td>Total</td>
<td><asp:Label ID="lblFWRec" runat="server"></asp:Label></td>
<td><asp:Label ID="lblFreshWaterTotal" runat="server"></asp:Label></td>
<td><asp:Label ID="lblROBFreshWaterTotal" runat="server"></asp:Label></td>
</tr>
</table>
</td>
<td style="width:33%;vertical-align:top">
<div style="height:383px; border-bottom:none;  overflow-x:hidden; overflow-y:scroll;font-size:11px;" class='ScrollAutoReset' id='Div1'>
<table width="100%" cellpadding="0" border="0" style="border-collapse:collapse">
<tr>
<td style="vertical-align:top">
<table width="100%" cellpadding="5" border="1" style="border-collapse:collapse">
<colgroup>
<col style="text-align:left" />
<col style="text-align:right" />
</colgroup>
<tr style="font-weight:bold; background-color:#002a80; color:White;">
<td>Item Name</td>
<td>Value</td>
</tr>
<tr>
<td>Exh Temp Min : </td>
<td><asp:Label ID="lblExchTempMin" runat="server"></asp:Label></td>
</tr>
<tr>
<td>Exh Temp Max : </td>
<td><asp:Label ID="lblExchTempMax" runat="server"></asp:Label></td>
</tr>
<tr>
<td>ME RPM :</td>
<td><asp:Label ID="lblMERPM" runat="server"></asp:Label></td>
</tr>
<tr>
<td>Engine Distance :
                                        </td>
<td><asp:Label ID="lblEGDist" runat="server"></asp:Label></td>
</tr>
<tr>
<td>Slip :
                                        </td>
<td><asp:Label ID="lblSlip" runat="server"></asp:Label></td>
</tr>
<tr>
<td>ME Output :
                                        </td>
<td><asp:Label ID="lblMEOutPut" runat="server"></asp:Label></td>
</tr>
<tr>
<td>ME Thermal Load :
                                        </td>
<td><asp:Label ID="lblThermalLoad" runat="server"></asp:Label></td>
</tr>
<tr>
<td>ME LOADINDICATOR :</td>
<td><asp:Label ID="lblMELoadIndicator" runat="server"></asp:Label></td>
</tr>
<tr>
<td>ME T/C( No. 1)RPM :
                                        </td>
<td><asp:Label ID="lblTC1" runat="server"></asp:Label></td>
</tr>
<tr>
<td>ME T/C( No. 2)RPM :
                                        </td>
<td><asp:Label ID="lblTC2" runat="server"></asp:Label></td>
</tr>
<tr>
<td>ME SCAV Pressure : 
                                        </td>
<td><asp:Label ID="lblMEScav" runat="server"></asp:Label></td>
</tr>
<tr>
<td>SCAV TEMP :
                                        </td>
<td><asp:Label ID="lblScavTemp" runat="server"></asp:Label></td>
</tr>
<tr>
<td>Lube Oil Pressure :
                                        </td>
<td><asp:Label ID="lblLubeOilPressure" runat="server"></asp:Label></td>
</tr>
<tr>
<td>Sea Water Temp :
                                        </td>
<td><asp:Label ID="lblSeaWaterTemp" runat="server"></asp:Label></td>
</tr>
<tr>
<td>Eng. Room Temp :
                                        </td>
<td><asp:Label ID="lblEngineRoomTemp" runat="server"></asp:Label></td>
</tr>
<tr>
<td>Bilge Pump( if run ) :
                                        </td>
<td><asp:Label ID="lblBilgePump" runat="server"></asp:Label></td>
</tr>
</table>
</td>
<td style="vertical-align:top">
<table width="100%" cellpadding="5" border="1" style="border-collapse:collapse">
<colgroup>
<col style="text-align:left" />
<col style="text-align:right" />
</colgroup>
<tr style="font-weight:bold; background-color:#002a80; color:White;">
<td>Item Name</td>
<td>Value</td>
</tr>
<tr>
<td> AUX 1 Load :  </td>
<td><asp:Label ID="lblAux1Load" runat="server"></asp:Label></td>
</tr>
<tr>
<td> AUX 2 Load :
                                        </td>
<td><asp:Label ID="lblAux2Load" runat="server"></asp:Label></td>
</tr>
<tr>
<td> AUX 3 Load :
                                        </td>
<td><asp:Label ID="lblAux3Load" runat="server"></asp:Label></td>
</tr>
<tr>
<td> AUX 4 Load :
                                        </td>
<td><asp:Label ID="lblAux4Load" runat="server"></asp:Label></td>
</tr>
<tr>
<td> Total AUX Load :
                                        </td>
<td><asp:Label ID="lblTotAuxLoad" runat="server"></asp:Label></td>
</tr>
<tr>
<td> A/E No 1 :
                                        </td>
<td><asp:Label ID="lblAE1" runat="server"></asp:Label></td>
</tr>
<tr>
<td> A/E No 2 :
                                        </td>
<td><asp:Label ID="lblAE2" runat="server"></asp:Label></td>
</tr>
<tr>
<td> A/E No 3 :
                                        </td>
<td><asp:Label ID="lblAE3" runat="server"></asp:Label></td>
</tr>
<tr>
<td> A/E No 4 :
                                        </td>
<td><asp:Label ID="lblAE4" runat="server"></asp:Label></td>
</tr>
<tr>
<td> A/E FO INLET TEMP :</td>
<td><asp:Label ID="lblAETEMP" runat="server"></asp:Label></td>
</tr>
<tr>
<td> Shaft Generator ( Hrs ) :</td>
<td><asp:Label ID="lblShaftGenHrs" runat="server"></asp:Label></td>
</tr>
<tr>
<td> Tank Cleaning ( Hrs ) :</td>
<td><asp:Label ID="lblTCHrs" runat="server"></asp:Label></td>
</tr>
<tr>
<td> Cargo Heating ( Hrs ) :</td>
<td><asp:Label ID="lblCHHrs" runat="server"></asp:Label></td>
</tr>
<tr>
<td> Inert Hours ( Hrs ) :</td>
<td><asp:Label ID="lblIHrs" runat="server"></asp:Label></td>
</tr>
</table>
</td>
<td style="vertical-align:top">
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
</tr>
</table>
</div>
</td>
</tr>
</table>
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
