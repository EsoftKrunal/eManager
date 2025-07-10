<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NoonReport.aspx.cs" Inherits="PositionReport_NoonReport"  Async="true" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title>EMANAGER</title>
  <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />

  <script type="text/javascript" src="../JQ_Scripts/jquery.min.js"></script>
  <script src="../js/AutoComplete/knockout-2.2.1.js" type="text/javascript"></script>
  <script type="text/javascript" src="../JQ_Script/KPIScript.js"></script>
  
  <link rel="stylesheet" href="../js/AutoComplete/jquery-ui.css" />
  <script src="../js/AutoComplete/jquery-ui.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
  <script type="text/javascript">
      function roundToTwo(num) {
          return +(Math.round(num + "e+2") + "e-2");
      }
      function ConvertToDec(v) {
          if (isNaN(parseFloat(v))) {
              return 0;
          }
          else {
              return parseFloat(v);
          }
      }

      function MaxInt360(obj) {
          var num = obj.value.toString() + String.fromCharCode(event.keyCode);
          if (num > 360) {
              event.returnValue = false;
          }
      }
      function MaxInt24(obj) {
          var num = obj.value.toString() + String.fromCharCode(event.keyCode);
          if (num > 24) {
              event.returnValue = false;
          }
      }
      function CalSlip() {
          var Slip = document.getElementById('txtSlip');
          var EngineDistance = document.getElementById('txtEngineDistance').value;
          var DistanceMadeGood = document.getElementById('txtDistanceMadeGood').value;
          if (parseFloat(((EngineDistance.toString() == '') ? "0" : EngineDistance)) == 0) {
              Slip.value = 0;
              return false;
          }
          var CalSlip = ((parseFloat(((EngineDistance.toString() == '') ? "0" : EngineDistance)) - parseFloat(((DistanceMadeGood.toString() == '') ? "0" : DistanceMadeGood))) / parseFloat(((EngineDistance.toString() == '') ? "0" : EngineDistance))) * 100;
          Slip.value = Math.round(CalSlip * 10) / 10;
      }
      function CalSpeed() {
          var DistanceMadeGood = document.getElementById('txtDistanceMadeGood').value;
          var hrs = document.getElementById('ddlSteamingHours').options[document.getElementById('ddlSteamingHours').selectedIndex].value;
          var min = document.getElementById('ddlSteamingMin').options[document.getElementById('ddlSteamingMin').selectedIndex].value;

          var speed = ConvertToDec(DistanceMadeGood) / ( ConvertToDec(hrs) + ConvertToDec(min/60) );
          var speed = roundToTwo(speed);
          document.getElementById('txtAvgSpeed').value = speed;
      }
      function TotalConsumptionForIFO45() {
          return true;
          var ME = document.getElementById('txtME_IFO45').value;
          var AE = document.getElementById('txtAE_IFO45').value;
          //            var CargoHeating=document.getElementById('txtCargoHeating_IFO45').value;
          //            var TankCleaning=document.getElementById('txtTankCleaning_IFO45').value;
          //            var GasFreeing=document.getElementById('txtGasFreeing_IFO45').value;
          //            var IGS=document.getElementById('txtIGS_IFO45').value;
          //var TotalIFO45=document.getElementById('txtIGS_IFO45_Total');

          var res = parseFloat(((ME.toString() == '') ? "0" : ME)) + parseFloat(((AE.toString() == '') ? "0" : AE));
          //            +parseFloat(((CargoHeating.toString()=='')?"0":CargoHeating))
          //            +parseFloat(((TankCleaning.toString()=='')?"0":TankCleaning))
          //            +parseFloat(((GasFreeing.toString()=='')?"0":GasFreeing))
          //            +parseFloat(((IGS.toString()=='')?"0":IGS));

          TotalIFO45.value = Math.round(res * 100) / 100;
      }
      function TotalConsumptionForIFO1() {
          return true;
          var ME = document.getElementById('txtME_IFO1').value;
          var AE = document.getElementById('txtAE_IFO1').value;
          //            var CargoHeating=document.getElementById('txtCargoHeating_IFO1').value;
          //            var TankCleaning=document.getElementById('txtTankCleaning_IFO1').value;
          //            var GasFreeing=document.getElementById('txtGasFreeing_IFO1').value;
          //            var IGS=document.getElementById('txtIGS_IFO1').value;
          var TotalIFO1 = document.getElementById('txtIGS_IFO1_Total');

          var res = parseFloat(((ME.toString() == '') ? "0" : ME)) + parseFloat(((AE.toString() == '') ? "0" : AE));
          //            +parseFloat(((CargoHeating.toString()=='')?"0":CargoHeating))
          //            +parseFloat(((TankCleaning.toString()=='')?"0":TankCleaning))
          //            +parseFloat(((GasFreeing.toString()=='')?"0":GasFreeing))
          //            +parseFloat(((IGS.toString()=='')?"0":IGS));

          TotalIFO1.value = Math.round(res * 100) / 100;
      }

      function TotalConsumptionForMGO5() {
          return true;
          var ME = document.getElementById('txtME_MGO5').value;
          var AE = document.getElementById('txtAE_MGO5').value;
          //            var CargoHeating=document.getElementById('txtCargoHeating_MGO5').value;
          //            var TankCleaning=document.getElementById('txtTankCleaning_MGO5').value;
          //            var GasFreeing=document.getElementById('txtGasFreeing_MGo5').value;
          //            var IGS=document.getElementById('txtIGS_MGO5').value;
          var TotalMGO5 = document.getElementById('txtIGS_MGO5_Total');

          var res = parseFloat(((ME.toString() == '') ? "0" : ME)) + parseFloat(((AE.toString() == '') ? "0" : AE));
          //            +parseFloat(((CargoHeating.toString()=='')?"0":CargoHeating))
          //            +parseFloat(((TankCleaning.toString()=='')?"0":TankCleaning))
          //            +parseFloat(((GasFreeing.toString()=='')?"0":GasFreeing))
          //            +parseFloat(((IGS.toString()=='')?"0":IGS));

          TotalMGO5.value = Math.round(res * 100) / 100;
      }

      function TotalConsumptionForMGO1() {
          return true;
          var ME = document.getElementById('txtME_MGO10').value;
          var AE = document.getElementById('txtAE_MGO1').value;
          var CargoHeating = document.getElementById('txtCargoHeating_MGO1').value;
          var TankCleaning = document.getElementById('txtTankCleaning_MGO1').value;
          var GasFreeing = document.getElementById('txtGasFreeing_MGo1').value;
          var IGS = document.getElementById('txtIGS_MGO1').value;
          var TotalMGO1 = document.getElementById('txtIGS_MGO1_Total');

          var res = parseFloat(((ME.toString() == '') ? "0" : ME)) + parseFloat(((AE.toString() == '') ? "0" : AE))
            + parseFloat(((CargoHeating.toString() == '') ? "0" : CargoHeating))
            + parseFloat(((TankCleaning.toString() == '') ? "0" : TankCleaning))
            + parseFloat(((GasFreeing.toString() == '') ? "0" : GasFreeing))
            + parseFloat(((IGS.toString() == '') ? "0" : IGS));

          TotalMGO1.value = Math.round(res * 100) / 100;
      }

      function TotalConsumptionForMDO() {
          return true;
          var ME = document.getElementById('txtME_MDO').value;
          var AE = document.getElementById('txtAE_MDO').value;
          var CargoHeating = document.getElementById('txtCargoHeating_MDO').value;
          var TankCleaning = document.getElementById('txtTankCleaning_MDO').value;
          var GasFreeing = document.getElementById('txtGasFreeing_MDO').value;
          var IGS = document.getElementById('txtIGS_MDO').value;
          var TotalMDO = document.getElementById('txtIGS_MDO_Total');

          var res = parseFloat(((ME.toString() == '') ? "0" : ME)) + parseFloat(((AE.toString() == '') ? "0" : AE))
            + parseFloat(((CargoHeating.toString() == '') ? "0" : CargoHeating))
            + parseFloat(((TankCleaning.toString() == '') ? "0" : TankCleaning))
            + parseFloat(((GasFreeing.toString() == '') ? "0" : GasFreeing))
            + parseFloat(((IGS.toString() == '') ? "0" : IGS));
          TotalMDO.value = Math.round(res * 100) / 100;
      }
      function CalTotAuxiliary() {

          var Aux1 = document.getElementById('AUX_1_Load').value;
          var Aux2 = document.getElementById('AUX_2_Load').value;
          var Aux3 = document.getElementById('AUX_3_Load').value;
          var Aux4 = document.getElementById('AUX_4_Load').value;
          var TotalTotAuxiliary = document.getElementById('lblTotAuxiliary');


          var res = parseFloat(((Aux1.toString() == '') ? 0 : Aux1))
                                    + parseFloat(((Aux2.toString() == '') ? 0 : Aux2))
                                    + parseFloat(((Aux3.toString() == '') ? 0 : Aux3))
                                    + parseFloat(((Aux4.toString() == '') ? 0 : Aux4));
          TotalTotAuxiliary.value = Math.round(res * 100) / 100;
      }
    </script>
  <script type="text/javascript" >


        function IntValueOnly(ctr) {
            if (!(event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                event.returnValue = false;
            }
        }
        function FloatValueOnly(ctr) {
            if (!(event.keyCode == 46 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                event.returnValue = false;
            }
            else {

                if (event.keyCode == 46) {
                    if (ctr.value.indexOf('.') != -1) {
                        event.returnValue = false;
                    }
                }
            }
        }
    </script> 
  <%--<style type="text/css">
      .aedata
      {
          width:90%;
      }
    .validationsummary ul
    {
        width:98%;
        margin:0px;
        margin-bottom:3px;
        margin-left:0px;
        padding-left:20px;
        padding:3px;
        height:80px;
        overflow-y:scroll;
        overflow-x:hidden;
        border:solid 1px #CCCC00;
    }
    .InvalidCellvalue
    {
        background-color:Red;
    }
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
</style>--%>
  <script type="text/javascript">
         function RegisterAutoComplete() {
             $(function () {
                 function log(message) {
//                     $("<div>").text(message).prependTo("#log");
//                     $("#log").scrollTop(0);
                 }

                 $("#txtDepPort").autocomplete({
                     source: function (request, response) {
                         $.ajax({
                             url: getBaseURL() + "/eReports/getautocompletedata.ashx",
                             dataType: "json",
                             headers: { "cache-control": "no-cache" },
                             type: "POST",
                             data: { Key: $("#txtDepPort").val(), Type: "PORT" },
                             success: function (data) {
                                 response($.map(data.geonames, function (item) { return { label: item.PortName, value: item.PortName} }
                                    ));
                             }
                         });
                     },
                     minLength: 2,
                     select: function (event, ui) {
                         log(ui.item ?
                          "Selected: " + ui.item.label :
                          "Nothing selected, input was " + this.value);
                     },
                     open: function () {
                         $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
                     },
                     close: function () {
                         $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
                     }
                 });
             });
         }

         function RegisterAutoComplete2() {
             $(function () {
                 function log(message) {
                     //                     $("<div>").text(message).prependTo("#log");
                     //                     $("#log").scrollTop(0);
                 }

                 $("#txtDepArrivalPort").autocomplete({
                     source: function (request, response) {
                         $.ajax({
                             url: getBaseURL() + "/eReports/getautocompletedata.ashx",
                             dataType: "json",
                             headers: { "cache-control": "no-cache" },
                             type: "POST",
                             data: { Key: $("#txtDepArrivalPort").val(), Type: "PORT" },
                             success: function (data) {
                                 response($.map(data.geonames, function (item) { return { label: item.PortName, value: item.PortName} }
                                    ));
                             }
                         });
                     },
                     minLength: 2,
                     select: function (event, ui) {
                         log(ui.item ?
                          "Selected: " + ui.item.label :
                          "Nothing selected, input was " + this.value);
                     },
                     open: function () {
                         $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
                     },
                     close: function () {
                         $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
                     }
                 });
             });
         }

         

         function getBaseURL() {
             var url = window.location.href.split('/');
             var baseUrl = url[0] + '//' + url[2] + '/' + url[3];
             return baseUrl;
         }
  </script>
</head>
<body>
<form id="form" runat="server" style="font-family:Arial;font-size:12px;">
<asp:ToolkitScriptManager  ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
<div style="padding:6px; font-size:14px;text-align:center; font-family:Arial Black;" class="text headerband">
    <asp:Label runat="server" ID="lblReportName" Text="Position Report" Font-Size="20px"></asp:Label>
    /
    <asp:Label ID="Label1" runat="server" Font-Names="Arial Black" Font-Size="20px" Text="Report ID :"></asp:Label>
    <asp:Label ID="lblReportID" runat="server" Font-Names="Arial Black" Font-Size="20px"></asp:Label> 
    
    </div>
<div style="padding-left:50px;padding-right:50px;">
<div class="div1">Voyage Information :</div>
<div >
<table cellpadding="4" cellspacing="0" border="0" width="100%" >
        <tr>
            <td style="background-color:#FFFFFF; border:solid 1px #006B8F;">
                   <table cellpadding="0" cellspacing="3" border="0" width="100%" >
                        <tr>
                            <td style="width:180px">Vessel Code :</td>
                            <td style="width:250px">
                                <asp:TextBox runat="server" ID="txtVesselCode" style="width:40px;text-align:center;" MaxLength="3" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="text-align:right; width:250px">Report Date :</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtRDate" style="width:100px;text-align:center; " MaxLength="15" ></asp:TextBox>
                                <asp:CalendarExtender runat="server" id="Cal1" TargetControlID="txtRDate" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator runat="server" id="rec1" ControlToValidate="txtRDate" ErrorMessage="Report Date is required." Text="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:180px">Report Type :</td>
                            <td >
                             <asp:DropDownList runat="server" ID="ddlReportType" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlReportType_OnSelectedIndexChanged" Enabled="false" >
                                <asp:ListItem Text="< Select Report Type >" Value=""></asp:ListItem>
                                <asp:ListItem Text="Departure COSP" Value="D"></asp:ListItem>
                                <asp:ListItem Text="Noon at Sea" Value="N"></asp:ListItem>
                                <asp:ListItem Text="Arrival" Value="A"></asp:ListItem>
                                <asp:ListItem Text="Noon at Anchor" Value="PA"></asp:ListItem>
                                <asp:ListItem Text="Noon at Berth" Value="PB"></asp:ListItem>
                                <asp:ListItem Text="Noon at Drift" Value="PD"></asp:ListItem>
                                <asp:ListItem Text="Departure Berth" Value="SH"></asp:ListItem>
                            </asp:DropDownList> 
                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator71" ControlToValidate="ddlReportType" ErrorMessage="Report Type is required." Text="*"></asp:RequiredFieldValidator>
                                </td>

                                <td style="text-align:right; width:250px">Location :</td>                                
                                <td>
                                    <asp:DropDownList ID="ddlLocation" runat="server" Enabled="false" >
                                        <asp:ListItem  Value="" Text=""></asp:ListItem>
                                        <asp:ListItem  Value="1" Text="At Sea"></asp:ListItem>
                                        <asp:ListItem  Value="2" Text="In Port"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                        </tr>
                        <tr>
                            <td style="width:180px">Voyage #:</td>
                            <td style="width:250px">
                                <asp:TextBox runat="server" ID="txtVoyageNumber" style="width:200px;text-align:left; " MaxLength="11" Enabled="false" ></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" ControlToValidate="txtVoyageNumber" ErrorMessage="Voyage # is required." Text="*"></asp:RequiredFieldValidator>
                            </td>
                            <td style="text-align:right; width:250px">&nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        </table>
                   <table cellpadding="0" cellspacing="3" border="0" width="100%" >
                        <tr>
                            <td style="width:180px; vertical-align:top;">
                                Voy. Instructions :
                            </td>
                            <td colspan="3">
                               <asp:TextBox runat="server" ID="txtVoyInstructions" TextMode="MultiLine" style="width:80%; height:70px;" MaxLength="11" ></asp:TextBox>
                            </td>
                        </tr>
                       
                       <tr>
                          <td style="width:180px">Voy. Condition :</td>
                           <td style="width:250px" >
                                <asp:DropDownList runat="server" ID="ddlDepVoyCondition" Width="90px">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="Laden" Text="Laden"></asp:ListItem>
                                    <asp:ListItem Value="Ballast" Text="Ballast"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator9" ControlToValidate="ddlDepVoyCondition" ErrorMessage="Voy. Condition is required." Text="*"></asp:RequiredFieldValidator>
                           </td>
                           <td style="width:180px;text-align:right;">Distance To Go :</td>
                           <td>
                                 <asp:TextBox ID="txtDepDistanceToGo" onkeypress="FloatValueOnly(event)" runat="server" MaxLength="5" style="width:70px;text-align:left;"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtDepDistanceToGo" ErrorMessage="Distance To Go is required." Text="*"></asp:RequiredFieldValidator>
                                 <%--<span class="unit">&nbsp;NM</span></td>--%>
                               </td>
                       </tr>
                         <tr>
                           <td >Charter Party Speed :</td>
                           <td >
                                 <asp:TextBox ID="txtCPS" runat="server" MaxLength="50" style="text-transform:uppercase;"></asp:TextBox>
                               <span class="unit">KTS</span>
                                 <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtDepPort" ErrorMessage="Dep. Port is required." Text="*"></asp:RequiredFieldValidator>--%>
                                 <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator84" ControlToValidate="txtCPS" ErrorMessage="Charter Party Speed is required." Text="*"></asp:RequiredFieldValidator>
                           </td>
                          <td style="text-align:right;">
                              Charter Party Consumption :                                                         
                           </td>
                           <td>
                                <asp:TextBox ID="txtCPCons" runat="server" MaxLength="50" style="text-transform:uppercase;"></asp:TextBox>
                                <span class="unit">( MT/ Per Day )</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator85" runat="server" ControlToValidate="txtCPCons" ErrorMessage="Charter Party Consumption is required." Text="*"></asp:RequiredFieldValidator>

                          </td>
                        </tr> 
                        </table>
                   
            </td>
        </tr>
    
    <tr>
            <td style="background-color:#FFFFFF; border:solid 1px #006B8F;">
                    
                   <table cellpadding="0" cellspacing="3" border="0" width="100%" >
                        <tr>
                           <td style="width:15%;">Dep. Port :</td>
                           <td style="width:10%;">
                                 <asp:TextBox ID="txtDepPort" runat="server" MaxLength="50" style="text-transform:uppercase;" Enabled="false"></asp:TextBox>
                                 <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtDepPort" ErrorMessage="Dep. Port is required." Text="*"></asp:RequiredFieldValidator>--%>
                           </td>
                          

                            <td align="right" style="width:8%;">
                               COSP Date :
                           </td>
                           <td style="width:20%;">
                               <asp:TextBox runat="server" ID="txtCOSPDate" style="width:100px;text-align:center; " MaxLength="15" ></asp:TextBox>

                                <asp:CalendarExtender runat="server" id="CalendarExtender7" TargetControlID="txtCOSPDate" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator87" ControlToValidate="txtCOSPDate" ErrorMessage="COSP date is required." Text="*"></asp:RequiredFieldValidator>
                                <asp:DropDownList runat="server" ID="ddlCOSPHrs" Width="50px">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="00" Text="00"></asp:ListItem>
                                    <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                    <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                    <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                    <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                    <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                    <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                    <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                    <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                    <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                    <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                    <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                    <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                    <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                    <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                    <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                    <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                    <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                    <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                    <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                    <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                    <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                    <asp:ListItem Value="25" Text="25"></asp:ListItem>
                            </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator88" ControlToValidate="ddlCOSPHrs" ErrorMessage="COSP (HRS) is required." Text="*"></asp:RequiredFieldValidator>
                                <span class="unit">( Hrs )</span>
                                <asp:DropDownList runat="server" ID="ddlCOSPMin" Width="50px">
                                <asp:ListItem Value="" Text=""></asp:ListItem>
                                <asp:ListItem Value="00" Text="00"></asp:ListItem>                                   
                                <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                <asp:ListItem Value="32" Text="32"></asp:ListItem>
                                <asp:ListItem Value="33" Text="33"></asp:ListItem>
                                <asp:ListItem Value="34" Text="34"></asp:ListItem>
                                <asp:ListItem Value="35" Text="35"></asp:ListItem>
                                <asp:ListItem Value="36" Text="36"></asp:ListItem>
                                <asp:ListItem Value="37" Text="37"></asp:ListItem>
                                <asp:ListItem Value="38" Text="38"></asp:ListItem>
                                <asp:ListItem Value="39" Text="39"></asp:ListItem>
                                <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                <asp:ListItem Value="41" Text="41"></asp:ListItem>
                                <asp:ListItem Value="42" Text="42"></asp:ListItem>
                                <asp:ListItem Value="43" Text="43"></asp:ListItem>
                                <asp:ListItem Value="44" Text="44"></asp:ListItem>
                                <asp:ListItem Value="45" Text="45"></asp:ListItem>
                                <asp:ListItem Value="46" Text="46"></asp:ListItem>
                                <asp:ListItem Value="47" Text="47"></asp:ListItem>
                                <asp:ListItem Value="48" Text="48"></asp:ListItem>
                                <asp:ListItem Value="49" Text="49"></asp:ListItem>
                                <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                <asp:ListItem Value="51" Text="51"></asp:ListItem>
                                <asp:ListItem Value="52" Text="52"></asp:ListItem>
                                <asp:ListItem Value="53" Text="53"></asp:ListItem>
                                <asp:ListItem Value="54" Text="54"></asp:ListItem>
                                <asp:ListItem Value="55" Text="55"></asp:ListItem>
                                <asp:ListItem Value="56" Text="56"></asp:ListItem>
                                <asp:ListItem Value="57" Text="57"></asp:ListItem>
                                <asp:ListItem Value="58" Text="58"></asp:ListItem>
                                <asp:ListItem Value="59" Text="59"></asp:ListItem>

                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator89" ControlToValidate="ddlCOSPMin" ErrorMessage="COSP (MIN) is required." Text="*"></asp:RequiredFieldValidator>
                                <span class="unit">( Min )</span> 
                           </td>
                            <td style="width:12%;">Next Port Operation :
                                                           </td>
                            <td style="width:12%;">
                                <asp:DropDownList runat="server" ID="ddlNextPortOperation"  ValidationGroup="grp1" >
<asp:ListItem Value="" Text=""></asp:ListItem>
<asp:ListItem Value="Loading" Text="Loading"></asp:ListItem> 
                                    <asp:ListItem Value="Discharging" Text="Discharging"></asp:ListItem> 
                                    <asp:ListItem Value="Bunkering" Text="Bunkering"></asp:ListItem> 
                                </asp:DropDownList>
                            </td>
                            <td style="width:8%;">

                            </td>
                            <td style="width:12%;">

                            </td>
                        </tr> 
                       <tr>
                            <div runat="server" visible="false" Id="divAnchorwaitTimeDepature">
                           <td>
                               All gone clear / Anchor aweigh time (LT) : 
                           </td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlAnchorWaitTime_H"  CssClass="dropdown-HH-MM"  ValidationGroup="grp1">
        <asp:ListItem Value="" Text=""></asp:ListItem>
        <asp:ListItem Value="00" Text="00"></asp:ListItem>
        <asp:ListItem Value="01" Text="01"></asp:ListItem>
        <asp:ListItem Value="02" Text="02"></asp:ListItem>
        <asp:ListItem Value="03" Text="03"></asp:ListItem>
        <asp:ListItem Value="04" Text="04"></asp:ListItem>
        <asp:ListItem Value="05" Text="05"></asp:ListItem>
        <asp:ListItem Value="06" Text="06"></asp:ListItem>
        <asp:ListItem Value="07" Text="07"></asp:ListItem>
        <asp:ListItem Value="08" Text="08"></asp:ListItem>
        <asp:ListItem Value="09" Text="09"></asp:ListItem>
        <asp:ListItem Value="10" Text="10"></asp:ListItem>
        <asp:ListItem Value="11" Text="11"></asp:ListItem>
        <asp:ListItem Value="12" Text="12"></asp:ListItem>
        <asp:ListItem Value="13" Text="13"></asp:ListItem>
        <asp:ListItem Value="14" Text="14"></asp:ListItem>
        <asp:ListItem Value="15" Text="15"></asp:ListItem>
        <asp:ListItem Value="16" Text="16"></asp:ListItem>
        <asp:ListItem Value="17" Text="17"></asp:ListItem>
        <asp:ListItem Value="18" Text="18"></asp:ListItem>
        <asp:ListItem Value="19" Text="19"></asp:ListItem>
        <asp:ListItem Value="20" Text="20"></asp:ListItem>
        <asp:ListItem Value="21" Text="21"></asp:ListItem>
        <asp:ListItem Value="22" Text="22"></asp:ListItem>
        <asp:ListItem Value="23" Text="23"></asp:ListItem>
        
</asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server"  ValidationGroup="grp1" id="rfv_ddlAnchorWaitTime_H" ControlToValidate="ddlAnchorWaitTime_H"  ErrorMessage="Anchor Wait Time (HRS) is required." Text="*"></asp:RequiredFieldValidator>
                                <asp:DropDownList runat="server" ID="ddlAnchorWaitTime_M" CssClass="dropdown-HH-MM"  ValidationGroup="grp1" >
    <asp:ListItem Value="" Text=""></asp:ListItem>
    <asp:ListItem Value="00" Text="00"></asp:ListItem>                                   
    <asp:ListItem Value="01" Text="01"></asp:ListItem>
    <asp:ListItem Value="02" Text="02"></asp:ListItem>
    <asp:ListItem Value="03" Text="03"></asp:ListItem>
    <asp:ListItem Value="04" Text="04"></asp:ListItem>
    <asp:ListItem Value="05" Text="05"></asp:ListItem>
    <asp:ListItem Value="06" Text="06"></asp:ListItem>
    <asp:ListItem Value="07" Text="07"></asp:ListItem>
    <asp:ListItem Value="08" Text="08"></asp:ListItem>
    <asp:ListItem Value="09" Text="09"></asp:ListItem>
    <asp:ListItem Value="10" Text="10"></asp:ListItem>
    <asp:ListItem Value="11" Text="11"></asp:ListItem>
    <asp:ListItem Value="12" Text="12"></asp:ListItem>
    <asp:ListItem Value="13" Text="13"></asp:ListItem>
    <asp:ListItem Value="14" Text="14"></asp:ListItem>
    <asp:ListItem Value="15" Text="15"></asp:ListItem>
    <asp:ListItem Value="16" Text="16"></asp:ListItem>
    <asp:ListItem Value="17" Text="17"></asp:ListItem>
    <asp:ListItem Value="18" Text="18"></asp:ListItem>
    <asp:ListItem Value="19" Text="19"></asp:ListItem>
    <asp:ListItem Value="20" Text="20"></asp:ListItem>
    <asp:ListItem Value="21" Text="21"></asp:ListItem>
    <asp:ListItem Value="22" Text="22"></asp:ListItem>
    <asp:ListItem Value="23" Text="23"></asp:ListItem>
    <asp:ListItem Value="24" Text="24"></asp:ListItem>
    <asp:ListItem Value="25" Text="25"></asp:ListItem>
    <asp:ListItem Value="26" Text="26"></asp:ListItem>
    <asp:ListItem Value="27" Text="27"></asp:ListItem>
    <asp:ListItem Value="28" Text="28"></asp:ListItem>
    <asp:ListItem Value="29" Text="29"></asp:ListItem>
    <asp:ListItem Value="30" Text="30"></asp:ListItem>
    <asp:ListItem Value="31" Text="31"></asp:ListItem>
    <asp:ListItem Value="32" Text="32"></asp:ListItem>
    <asp:ListItem Value="33" Text="33"></asp:ListItem>
    <asp:ListItem Value="34" Text="34"></asp:ListItem>
    <asp:ListItem Value="35" Text="35"></asp:ListItem>
    <asp:ListItem Value="36" Text="36"></asp:ListItem>
    <asp:ListItem Value="37" Text="37"></asp:ListItem>
    <asp:ListItem Value="38" Text="38"></asp:ListItem>
    <asp:ListItem Value="39" Text="39"></asp:ListItem>
    <asp:ListItem Value="40" Text="40"></asp:ListItem>
    <asp:ListItem Value="41" Text="41"></asp:ListItem>
    <asp:ListItem Value="42" Text="42"></asp:ListItem>
    <asp:ListItem Value="43" Text="43"></asp:ListItem>
    <asp:ListItem Value="44" Text="44"></asp:ListItem>
    <asp:ListItem Value="45" Text="45"></asp:ListItem>
    <asp:ListItem Value="46" Text="46"></asp:ListItem>
    <asp:ListItem Value="47" Text="47"></asp:ListItem>
    <asp:ListItem Value="48" Text="48"></asp:ListItem>
    <asp:ListItem Value="49" Text="49"></asp:ListItem>
    <asp:ListItem Value="50" Text="50"></asp:ListItem>
    <asp:ListItem Value="51" Text="51"></asp:ListItem>
    <asp:ListItem Value="52" Text="52"></asp:ListItem>
    <asp:ListItem Value="53" Text="53"></asp:ListItem>
    <asp:ListItem Value="54" Text="54"></asp:ListItem>
    <asp:ListItem Value="55" Text="55"></asp:ListItem>
    <asp:ListItem Value="56" Text="56"></asp:ListItem>
    <asp:ListItem Value="57" Text="57"></asp:ListItem>
    <asp:ListItem Value="58" Text="58"></asp:ListItem>
    <asp:ListItem Value="59" Text="59"></asp:ListItem>

    </asp:DropDownList>
                                 <asp:RequiredFieldValidator runat="server"  ValidationGroup="grp1" id="rfv_ddlAnchorWaitTime_M" ControlToValidate="ddlAnchorWaitTime_M" ErrorMessage="Anchor Wait Time (MIN) is required." Text="*"></asp:RequiredFieldValidator>
                           </td>
                                </div>
                           <div runat="server" visible="false" Id="divAnchorAwaitTimeArrival">
                                <td>
                               All Fast clear / Anchor aweigh time (LT) : 
                           </td>
                               <td>
                                   &nbsp;<asp:DropDownList runat="server" ID="ddlAnchorWaitTimeArrive_H"  CssClass="dropdown-HH-MM"  ValidationGroup="grp1">
        <asp:ListItem Value="" Text=""></asp:ListItem>
        <asp:ListItem Value="00" Text="00"></asp:ListItem>
        <asp:ListItem Value="01" Text="01"></asp:ListItem>
        <asp:ListItem Value="02" Text="02"></asp:ListItem>
        <asp:ListItem Value="03" Text="03"></asp:ListItem>
        <asp:ListItem Value="04" Text="04"></asp:ListItem>
        <asp:ListItem Value="05" Text="05"></asp:ListItem>
        <asp:ListItem Value="06" Text="06"></asp:ListItem>
        <asp:ListItem Value="07" Text="07"></asp:ListItem>
        <asp:ListItem Value="08" Text="08"></asp:ListItem>
        <asp:ListItem Value="09" Text="09"></asp:ListItem>
        <asp:ListItem Value="10" Text="10"></asp:ListItem>
        <asp:ListItem Value="11" Text="11"></asp:ListItem>
        <asp:ListItem Value="12" Text="12"></asp:ListItem>
        <asp:ListItem Value="13" Text="13"></asp:ListItem>
        <asp:ListItem Value="14" Text="14"></asp:ListItem>
        <asp:ListItem Value="15" Text="15"></asp:ListItem>
        <asp:ListItem Value="16" Text="16"></asp:ListItem>
        <asp:ListItem Value="17" Text="17"></asp:ListItem>
        <asp:ListItem Value="18" Text="18"></asp:ListItem>
        <asp:ListItem Value="19" Text="19"></asp:ListItem>
        <asp:ListItem Value="20" Text="20"></asp:ListItem>
        <asp:ListItem Value="21" Text="21"></asp:ListItem>
        <asp:ListItem Value="22" Text="22"></asp:ListItem>
        <asp:ListItem Value="23" Text="23"></asp:ListItem>
        
</asp:DropDownList>
    <asp:RequiredFieldValidator runat="server"  ValidationGroup="grp1" id="RequiredFieldValidator115" ControlToValidate="ddlAnchorWaitTimeArrive_H"  ErrorMessage="All fast clear / Anchor aweigh time (LT) (HRS) is required." Text="*"></asp:RequiredFieldValidator>
   
    <asp:DropDownList runat="server" ID="ddlAnchorWaitTimeArrive_M" CssClass="dropdown-HH-MM"  ValidationGroup="grp1" >
    <asp:ListItem Value="" Text=""></asp:ListItem>
    <asp:ListItem Value="00" Text="00"></asp:ListItem>                                   
    <asp:ListItem Value="01" Text="01"></asp:ListItem>
    <asp:ListItem Value="02" Text="02"></asp:ListItem>
    <asp:ListItem Value="03" Text="03"></asp:ListItem>
    <asp:ListItem Value="04" Text="04"></asp:ListItem>
    <asp:ListItem Value="05" Text="05"></asp:ListItem>
    <asp:ListItem Value="06" Text="06"></asp:ListItem>
    <asp:ListItem Value="07" Text="07"></asp:ListItem>
    <asp:ListItem Value="08" Text="08"></asp:ListItem>
    <asp:ListItem Value="09" Text="09"></asp:ListItem>
    <asp:ListItem Value="10" Text="10"></asp:ListItem>
    <asp:ListItem Value="11" Text="11"></asp:ListItem>
    <asp:ListItem Value="12" Text="12"></asp:ListItem>
    <asp:ListItem Value="13" Text="13"></asp:ListItem>
    <asp:ListItem Value="14" Text="14"></asp:ListItem>
    <asp:ListItem Value="15" Text="15"></asp:ListItem>
    <asp:ListItem Value="16" Text="16"></asp:ListItem>
    <asp:ListItem Value="17" Text="17"></asp:ListItem>
    <asp:ListItem Value="18" Text="18"></asp:ListItem>
    <asp:ListItem Value="19" Text="19"></asp:ListItem>
    <asp:ListItem Value="20" Text="20"></asp:ListItem>
    <asp:ListItem Value="21" Text="21"></asp:ListItem>
    <asp:ListItem Value="22" Text="22"></asp:ListItem>
    <asp:ListItem Value="23" Text="23"></asp:ListItem>
    <asp:ListItem Value="24" Text="24"></asp:ListItem>
    <asp:ListItem Value="25" Text="25"></asp:ListItem>
    <asp:ListItem Value="26" Text="26"></asp:ListItem>
    <asp:ListItem Value="27" Text="27"></asp:ListItem>
    <asp:ListItem Value="28" Text="28"></asp:ListItem>
    <asp:ListItem Value="29" Text="29"></asp:ListItem>
    <asp:ListItem Value="30" Text="30"></asp:ListItem>
    <asp:ListItem Value="31" Text="31"></asp:ListItem>
    <asp:ListItem Value="32" Text="32"></asp:ListItem>
    <asp:ListItem Value="33" Text="33"></asp:ListItem>
    <asp:ListItem Value="34" Text="34"></asp:ListItem>
    <asp:ListItem Value="35" Text="35"></asp:ListItem>
    <asp:ListItem Value="36" Text="36"></asp:ListItem>
    <asp:ListItem Value="37" Text="37"></asp:ListItem>
    <asp:ListItem Value="38" Text="38"></asp:ListItem>
    <asp:ListItem Value="39" Text="39"></asp:ListItem>
    <asp:ListItem Value="40" Text="40"></asp:ListItem>
    <asp:ListItem Value="41" Text="41"></asp:ListItem>
    <asp:ListItem Value="42" Text="42"></asp:ListItem>
    <asp:ListItem Value="43" Text="43"></asp:ListItem>
    <asp:ListItem Value="44" Text="44"></asp:ListItem>
    <asp:ListItem Value="45" Text="45"></asp:ListItem>
    <asp:ListItem Value="46" Text="46"></asp:ListItem>
    <asp:ListItem Value="47" Text="47"></asp:ListItem>
    <asp:ListItem Value="48" Text="48"></asp:ListItem>
    <asp:ListItem Value="49" Text="49"></asp:ListItem>
    <asp:ListItem Value="50" Text="50"></asp:ListItem>
    <asp:ListItem Value="51" Text="51"></asp:ListItem>
    <asp:ListItem Value="52" Text="52"></asp:ListItem>
    <asp:ListItem Value="53" Text="53"></asp:ListItem>
    <asp:ListItem Value="54" Text="54"></asp:ListItem>
    <asp:ListItem Value="55" Text="55"></asp:ListItem>
    <asp:ListItem Value="56" Text="56"></asp:ListItem>
    <asp:ListItem Value="57" Text="57"></asp:ListItem>
    <asp:ListItem Value="58" Text="58"></asp:ListItem>
    <asp:ListItem Value="59" Text="59"></asp:ListItem>

    </asp:DropDownList>
    <asp:RequiredFieldValidator runat="server"  ValidationGroup="grp1" id="RequiredFieldValidator116" ControlToValidate="ddlAnchorWaitTimeArrive_M" ErrorMessage="All fast clear / Anchor aweigh time (LT) (MIN) is required." Text="*"></asp:RequiredFieldValidator>
                               </td>
                               </div>
                            <td align="right">
                                S.B.E. (LT) : 
                           </td>
                            <td>
                                        <asp:DropDownList runat="server" ID="ddlSBELT_H" CssClass="dropdown-HH-MM"  ValidationGroup="grp1">
        <asp:ListItem Value="" Text=""></asp:ListItem>
        <asp:ListItem Value="00" Text="00"></asp:ListItem>
        <asp:ListItem Value="01" Text="01"></asp:ListItem>
        <asp:ListItem Value="02" Text="02"></asp:ListItem>
        <asp:ListItem Value="03" Text="03"></asp:ListItem>
        <asp:ListItem Value="04" Text="04"></asp:ListItem>
        <asp:ListItem Value="05" Text="05"></asp:ListItem>
        <asp:ListItem Value="06" Text="06"></asp:ListItem>
        <asp:ListItem Value="07" Text="07"></asp:ListItem>
        <asp:ListItem Value="08" Text="08"></asp:ListItem>
        <asp:ListItem Value="09" Text="09"></asp:ListItem>
        <asp:ListItem Value="10" Text="10"></asp:ListItem>
        <asp:ListItem Value="11" Text="11"></asp:ListItem>
        <asp:ListItem Value="12" Text="12"></asp:ListItem>
        <asp:ListItem Value="13" Text="13"></asp:ListItem>
        <asp:ListItem Value="14" Text="14"></asp:ListItem>
        <asp:ListItem Value="15" Text="15"></asp:ListItem>
        <asp:ListItem Value="16" Text="16"></asp:ListItem>
        <asp:ListItem Value="17" Text="17"></asp:ListItem>
        <asp:ListItem Value="18" Text="18"></asp:ListItem>
        <asp:ListItem Value="19" Text="19"></asp:ListItem>
        <asp:ListItem Value="20" Text="20"></asp:ListItem>
        <asp:ListItem Value="21" Text="21"></asp:ListItem>
        <asp:ListItem Value="22" Text="22"></asp:ListItem>
        <asp:ListItem Value="23" Text="23"></asp:ListItem>
        
</asp:DropDownList>
    <asp:RequiredFieldValidator  ValidationGroup="grp1" runat="server" id="rfv_ddlSBELT_H" ControlToValidate="ddlSBELT_H" ErrorMessage="S.B.E (LT) (HRS) is required." Text="*"></asp:RequiredFieldValidator>
    
    <asp:DropDownList runat="server" ID="ddlSBELT_M" CssClass="dropdown-HH-MM"  ValidationGroup="grp1">
    <asp:ListItem Value="" Text=""></asp:ListItem>
    <asp:ListItem Value="00" Text="00"></asp:ListItem>                                   
    <asp:ListItem Value="01" Text="01"></asp:ListItem>
    <asp:ListItem Value="02" Text="02"></asp:ListItem>
    <asp:ListItem Value="03" Text="03"></asp:ListItem>
    <asp:ListItem Value="04" Text="04"></asp:ListItem>
    <asp:ListItem Value="05" Text="05"></asp:ListItem>
    <asp:ListItem Value="06" Text="06"></asp:ListItem>
    <asp:ListItem Value="07" Text="07"></asp:ListItem>
    <asp:ListItem Value="08" Text="08"></asp:ListItem>
    <asp:ListItem Value="09" Text="09"></asp:ListItem>
    <asp:ListItem Value="10" Text="10"></asp:ListItem>
    <asp:ListItem Value="11" Text="11"></asp:ListItem>
    <asp:ListItem Value="12" Text="12"></asp:ListItem>
    <asp:ListItem Value="13" Text="13"></asp:ListItem>
    <asp:ListItem Value="14" Text="14"></asp:ListItem>
    <asp:ListItem Value="15" Text="15"></asp:ListItem>
    <asp:ListItem Value="16" Text="16"></asp:ListItem>
    <asp:ListItem Value="17" Text="17"></asp:ListItem>
    <asp:ListItem Value="18" Text="18"></asp:ListItem>
    <asp:ListItem Value="19" Text="19"></asp:ListItem>
    <asp:ListItem Value="20" Text="20"></asp:ListItem>
    <asp:ListItem Value="21" Text="21"></asp:ListItem>
    <asp:ListItem Value="22" Text="22"></asp:ListItem>
    <asp:ListItem Value="23" Text="23"></asp:ListItem>
    <asp:ListItem Value="24" Text="24"></asp:ListItem>
    <asp:ListItem Value="25" Text="25"></asp:ListItem>
    <asp:ListItem Value="26" Text="26"></asp:ListItem>
    <asp:ListItem Value="27" Text="27"></asp:ListItem>
    <asp:ListItem Value="28" Text="28"></asp:ListItem>
    <asp:ListItem Value="29" Text="29"></asp:ListItem>
    <asp:ListItem Value="30" Text="30"></asp:ListItem>
    <asp:ListItem Value="31" Text="31"></asp:ListItem>
    <asp:ListItem Value="32" Text="32"></asp:ListItem>
    <asp:ListItem Value="33" Text="33"></asp:ListItem>
    <asp:ListItem Value="34" Text="34"></asp:ListItem>
    <asp:ListItem Value="35" Text="35"></asp:ListItem>
    <asp:ListItem Value="36" Text="36"></asp:ListItem>
    <asp:ListItem Value="37" Text="37"></asp:ListItem>
    <asp:ListItem Value="38" Text="38"></asp:ListItem>
    <asp:ListItem Value="39" Text="39"></asp:ListItem>
    <asp:ListItem Value="40" Text="40"></asp:ListItem>
    <asp:ListItem Value="41" Text="41"></asp:ListItem>
    <asp:ListItem Value="42" Text="42"></asp:ListItem>
    <asp:ListItem Value="43" Text="43"></asp:ListItem>
    <asp:ListItem Value="44" Text="44"></asp:ListItem>
    <asp:ListItem Value="45" Text="45"></asp:ListItem>
    <asp:ListItem Value="46" Text="46"></asp:ListItem>
    <asp:ListItem Value="47" Text="47"></asp:ListItem>
    <asp:ListItem Value="48" Text="48"></asp:ListItem>
    <asp:ListItem Value="49" Text="49"></asp:ListItem>
    <asp:ListItem Value="50" Text="50"></asp:ListItem>
    <asp:ListItem Value="51" Text="51"></asp:ListItem>
    <asp:ListItem Value="52" Text="52"></asp:ListItem>
    <asp:ListItem Value="53" Text="53"></asp:ListItem>
    <asp:ListItem Value="54" Text="54"></asp:ListItem>
    <asp:ListItem Value="55" Text="55"></asp:ListItem>
    <asp:ListItem Value="56" Text="56"></asp:ListItem>
    <asp:ListItem Value="57" Text="57"></asp:ListItem>
    <asp:ListItem Value="58" Text="58"></asp:ListItem>
    <asp:ListItem Value="59" Text="59"></asp:ListItem>

    </asp:DropDownList>
    <asp:RequiredFieldValidator  ValidationGroup="grp1" runat="server" id="rfv_ddlSBELT_M" ControlToValidate="ddlSBELT_M" ErrorMessage="S.B.E. (LT) (MIN) is required." Text="*"></asp:RequiredFieldValidator>
                           </td>
                            <td>
Pilot On Board : 
                           </td>
                            <td>
 <asp:DropDownList runat="server" ID="ddlPilotOnBoard_H" CssClass="dropdown-HH-MM"  ValidationGroup="grp1">
        <asp:ListItem Value="" Text=""></asp:ListItem>
        <asp:ListItem Value="00" Text="00"></asp:ListItem>
        <asp:ListItem Value="01" Text="01"></asp:ListItem>
        <asp:ListItem Value="02" Text="02"></asp:ListItem>
        <asp:ListItem Value="03" Text="03"></asp:ListItem>
        <asp:ListItem Value="04" Text="04"></asp:ListItem>
        <asp:ListItem Value="05" Text="05"></asp:ListItem>
        <asp:ListItem Value="06" Text="06"></asp:ListItem>
        <asp:ListItem Value="07" Text="07"></asp:ListItem>
        <asp:ListItem Value="08" Text="08"></asp:ListItem>
        <asp:ListItem Value="09" Text="09"></asp:ListItem>
        <asp:ListItem Value="10" Text="10"></asp:ListItem>
        <asp:ListItem Value="11" Text="11"></asp:ListItem>
        <asp:ListItem Value="12" Text="12"></asp:ListItem>
        <asp:ListItem Value="13" Text="13"></asp:ListItem>
        <asp:ListItem Value="14" Text="14"></asp:ListItem>
        <asp:ListItem Value="15" Text="15"></asp:ListItem>
        <asp:ListItem Value="16" Text="16"></asp:ListItem>
        <asp:ListItem Value="17" Text="17"></asp:ListItem>
        <asp:ListItem Value="18" Text="18"></asp:ListItem>
        <asp:ListItem Value="19" Text="19"></asp:ListItem>
        <asp:ListItem Value="20" Text="20"></asp:ListItem>
        <asp:ListItem Value="21" Text="21"></asp:ListItem>
        <asp:ListItem Value="22" Text="22"></asp:ListItem>
        <asp:ListItem Value="23" Text="23"></asp:ListItem>
        
</asp:DropDownList>
    <asp:RequiredFieldValidator runat="server"  ValidationGroup="grp1" id="rfv_ddlPilotOnBoard_H" ControlToValidate="ddlPilotOnBoard_H" ErrorMessage="Pilot On Board (HRS) is required." Text="*"></asp:RequiredFieldValidator>
     
    <asp:DropDownList  ValidationGroup="grp1" runat="server" ID="ddlPilotOnBoard_M" CssClass="dropdown-HH-MM" >
    <asp:ListItem Value="" Text=""></asp:ListItem>
    <asp:ListItem Value="00" Text="00"></asp:ListItem>                                   
    <asp:ListItem Value="01" Text="01"></asp:ListItem>
    <asp:ListItem Value="02" Text="02"></asp:ListItem>
    <asp:ListItem Value="03" Text="03"></asp:ListItem>
    <asp:ListItem Value="04" Text="04"></asp:ListItem>
    <asp:ListItem Value="05" Text="05"></asp:ListItem>
    <asp:ListItem Value="06" Text="06"></asp:ListItem>
    <asp:ListItem Value="07" Text="07"></asp:ListItem>
    <asp:ListItem Value="08" Text="08"></asp:ListItem>
    <asp:ListItem Value="09" Text="09"></asp:ListItem>
    <asp:ListItem Value="10" Text="10"></asp:ListItem>
    <asp:ListItem Value="11" Text="11"></asp:ListItem>
    <asp:ListItem Value="12" Text="12"></asp:ListItem>
    <asp:ListItem Value="13" Text="13"></asp:ListItem>
    <asp:ListItem Value="14" Text="14"></asp:ListItem>
    <asp:ListItem Value="15" Text="15"></asp:ListItem>
    <asp:ListItem Value="16" Text="16"></asp:ListItem>
    <asp:ListItem Value="17" Text="17"></asp:ListItem>
    <asp:ListItem Value="18" Text="18"></asp:ListItem>
    <asp:ListItem Value="19" Text="19"></asp:ListItem>
    <asp:ListItem Value="20" Text="20"></asp:ListItem>
    <asp:ListItem Value="21" Text="21"></asp:ListItem>
    <asp:ListItem Value="22" Text="22"></asp:ListItem>
    <asp:ListItem Value="23" Text="23"></asp:ListItem>
    <asp:ListItem Value="24" Text="24"></asp:ListItem>
    <asp:ListItem Value="25" Text="25"></asp:ListItem>
    <asp:ListItem Value="26" Text="26"></asp:ListItem>
    <asp:ListItem Value="27" Text="27"></asp:ListItem>
    <asp:ListItem Value="28" Text="28"></asp:ListItem>
    <asp:ListItem Value="29" Text="29"></asp:ListItem>
    <asp:ListItem Value="30" Text="30"></asp:ListItem>
    <asp:ListItem Value="31" Text="31"></asp:ListItem>
    <asp:ListItem Value="32" Text="32"></asp:ListItem>
    <asp:ListItem Value="33" Text="33"></asp:ListItem>
    <asp:ListItem Value="34" Text="34"></asp:ListItem>
    <asp:ListItem Value="35" Text="35"></asp:ListItem>
    <asp:ListItem Value="36" Text="36"></asp:ListItem>
    <asp:ListItem Value="37" Text="37"></asp:ListItem>
    <asp:ListItem Value="38" Text="38"></asp:ListItem>
    <asp:ListItem Value="39" Text="39"></asp:ListItem>
    <asp:ListItem Value="40" Text="40"></asp:ListItem>
    <asp:ListItem Value="41" Text="41"></asp:ListItem>
    <asp:ListItem Value="42" Text="42"></asp:ListItem>
    <asp:ListItem Value="43" Text="43"></asp:ListItem>
    <asp:ListItem Value="44" Text="44"></asp:ListItem>
    <asp:ListItem Value="45" Text="45"></asp:ListItem>
    <asp:ListItem Value="46" Text="46"></asp:ListItem>
    <asp:ListItem Value="47" Text="47"></asp:ListItem>
    <asp:ListItem Value="48" Text="48"></asp:ListItem>
    <asp:ListItem Value="49" Text="49"></asp:ListItem>
    <asp:ListItem Value="50" Text="50"></asp:ListItem>
    <asp:ListItem Value="51" Text="51"></asp:ListItem>
    <asp:ListItem Value="52" Text="52"></asp:ListItem>
    <asp:ListItem Value="53" Text="53"></asp:ListItem>
    <asp:ListItem Value="54" Text="54"></asp:ListItem>
    <asp:ListItem Value="55" Text="55"></asp:ListItem>
    <asp:ListItem Value="56" Text="56"></asp:ListItem>
    <asp:ListItem Value="57" Text="57"></asp:ListItem>
    <asp:ListItem Value="58" Text="58"></asp:ListItem>
    <asp:ListItem Value="59" Text="59"></asp:ListItem>

    </asp:DropDownList>
    <asp:RequiredFieldValidator runat="server"  ValidationGroup="grp1" id="rfv_ddlPilotOnBoard_M" ControlToValidate="ddlPilotOnBoard_M" ErrorMessage="Pilot on Board (MIN) is required." Text="*"></asp:RequiredFieldValidator>
                           </td>
                            <td>
                                Pilot Away Time :
                           </td>

                            <td>
                                  <asp:DropDownList runat="server" ID="ddlPilotAwayTime_H" CssClass="dropdown-HH-MM"  ValidationGroup="grp1">
        <asp:ListItem Value="" Text=""></asp:ListItem>
        <asp:ListItem Value="00" Text="00"></asp:ListItem>
        <asp:ListItem Value="01" Text="01"></asp:ListItem>
        <asp:ListItem Value="02" Text="02"></asp:ListItem>
        <asp:ListItem Value="03" Text="03"></asp:ListItem>
        <asp:ListItem Value="04" Text="04"></asp:ListItem>
        <asp:ListItem Value="05" Text="05"></asp:ListItem>
        <asp:ListItem Value="06" Text="06"></asp:ListItem>
        <asp:ListItem Value="07" Text="07"></asp:ListItem>
        <asp:ListItem Value="08" Text="08"></asp:ListItem>
        <asp:ListItem Value="09" Text="09"></asp:ListItem>
        <asp:ListItem Value="10" Text="10"></asp:ListItem>
        <asp:ListItem Value="11" Text="11"></asp:ListItem>
        <asp:ListItem Value="12" Text="12"></asp:ListItem>
        <asp:ListItem Value="13" Text="13"></asp:ListItem>
        <asp:ListItem Value="14" Text="14"></asp:ListItem>
        <asp:ListItem Value="15" Text="15"></asp:ListItem>
        <asp:ListItem Value="16" Text="16"></asp:ListItem>
        <asp:ListItem Value="17" Text="17"></asp:ListItem>
        <asp:ListItem Value="18" Text="18"></asp:ListItem>
        <asp:ListItem Value="19" Text="19"></asp:ListItem>
        <asp:ListItem Value="20" Text="20"></asp:ListItem>
        <asp:ListItem Value="21" Text="21"></asp:ListItem>
        <asp:ListItem Value="22" Text="22"></asp:ListItem>
        <asp:ListItem Value="23" Text="23"></asp:ListItem>
        
</asp:DropDownList>
    <asp:RequiredFieldValidator  ValidationGroup="grp1" runat="server" id="rfv_ddlPilotAwayTime_H" ControlToValidate="ddlPilotAwayTime_H" ErrorMessage="Pilot Away (HRS) is required." Text="*"></asp:RequiredFieldValidator>
    <asp:DropDownList runat="server" ID="ddlPilotAwayTime_M"  CssClass="dropdown-HH-MM"  ValidationGroup="grp1">
    <asp:ListItem Value="" Text=""></asp:ListItem>
    <asp:ListItem Value="00" Text="00"></asp:ListItem>                                   
    <asp:ListItem Value="01" Text="01"></asp:ListItem>
    <asp:ListItem Value="02" Text="02"></asp:ListItem>
    <asp:ListItem Value="03" Text="03"></asp:ListItem>
    <asp:ListItem Value="04" Text="04"></asp:ListItem>
    <asp:ListItem Value="05" Text="05"></asp:ListItem>
    <asp:ListItem Value="06" Text="06"></asp:ListItem>
    <asp:ListItem Value="07" Text="07"></asp:ListItem>
    <asp:ListItem Value="08" Text="08"></asp:ListItem>
    <asp:ListItem Value="09" Text="09"></asp:ListItem>
    <asp:ListItem Value="10" Text="10"></asp:ListItem>
    <asp:ListItem Value="11" Text="11"></asp:ListItem>
    <asp:ListItem Value="12" Text="12"></asp:ListItem>
    <asp:ListItem Value="13" Text="13"></asp:ListItem>
    <asp:ListItem Value="14" Text="14"></asp:ListItem>
    <asp:ListItem Value="15" Text="15"></asp:ListItem>
    <asp:ListItem Value="16" Text="16"></asp:ListItem>
    <asp:ListItem Value="17" Text="17"></asp:ListItem>
    <asp:ListItem Value="18" Text="18"></asp:ListItem>
    <asp:ListItem Value="19" Text="19"></asp:ListItem>
    <asp:ListItem Value="20" Text="20"></asp:ListItem>
    <asp:ListItem Value="21" Text="21"></asp:ListItem>
    <asp:ListItem Value="22" Text="22"></asp:ListItem>
    <asp:ListItem Value="23" Text="23"></asp:ListItem>
    <asp:ListItem Value="24" Text="24"></asp:ListItem>
    <asp:ListItem Value="25" Text="25"></asp:ListItem>
    <asp:ListItem Value="26" Text="26"></asp:ListItem>
    <asp:ListItem Value="27" Text="27"></asp:ListItem>
    <asp:ListItem Value="28" Text="28"></asp:ListItem>
    <asp:ListItem Value="29" Text="29"></asp:ListItem>
    <asp:ListItem Value="30" Text="30"></asp:ListItem>
    <asp:ListItem Value="31" Text="31"></asp:ListItem>
    <asp:ListItem Value="32" Text="32"></asp:ListItem>
    <asp:ListItem Value="33" Text="33"></asp:ListItem>
    <asp:ListItem Value="34" Text="34"></asp:ListItem>
    <asp:ListItem Value="35" Text="35"></asp:ListItem>
    <asp:ListItem Value="36" Text="36"></asp:ListItem>
    <asp:ListItem Value="37" Text="37"></asp:ListItem>
    <asp:ListItem Value="38" Text="38"></asp:ListItem>
    <asp:ListItem Value="39" Text="39"></asp:ListItem>
    <asp:ListItem Value="40" Text="40"></asp:ListItem>
    <asp:ListItem Value="41" Text="41"></asp:ListItem>
    <asp:ListItem Value="42" Text="42"></asp:ListItem>
    <asp:ListItem Value="43" Text="43"></asp:ListItem>
    <asp:ListItem Value="44" Text="44"></asp:ListItem>
    <asp:ListItem Value="45" Text="45"></asp:ListItem>
    <asp:ListItem Value="46" Text="46"></asp:ListItem>
    <asp:ListItem Value="47" Text="47"></asp:ListItem>
    <asp:ListItem Value="48" Text="48"></asp:ListItem>
    <asp:ListItem Value="49" Text="49"></asp:ListItem>
    <asp:ListItem Value="50" Text="50"></asp:ListItem>
    <asp:ListItem Value="51" Text="51"></asp:ListItem>
    <asp:ListItem Value="52" Text="52"></asp:ListItem>
    <asp:ListItem Value="53" Text="53"></asp:ListItem>
    <asp:ListItem Value="54" Text="54"></asp:ListItem>
    <asp:ListItem Value="55" Text="55"></asp:ListItem>
    <asp:ListItem Value="56" Text="56"></asp:ListItem>
    <asp:ListItem Value="57" Text="57"></asp:ListItem>
    <asp:ListItem Value="58" Text="58"></asp:ListItem>
    <asp:ListItem Value="59" Text="59"></asp:ListItem>

    </asp:DropDownList>
    <asp:RequiredFieldValidator  ValidationGroup="grp1" runat="server" id="rfv_ddlPilotAwayTime_M" ControlToValidate="ddlPilotAwayTime_M" ErrorMessage="Pilot Away (MIN) is required." Text="*"></asp:RequiredFieldValidator>
                           </td>
                       </tr>
                        <tr>
                            <td>Seasonal Load-Lines ( Port restrictions / water levels met ) : </td>
                            <td>  <asp:DropDownList ID="ddlSeasonalLoadLines" runat="server"  ValidationGroup="grp1" Width="90px">
    <asp:ListItem Value="" Text="Select" ></asp:ListItem>
    <asp:ListItem Value="Y" Text="Yes" ></asp:ListItem>
    <asp:ListItem Value="N" Text="No" ></asp:ListItem>
     </asp:DropDownList> </td>
                           <td >
                              Arrival Port :
                                                          
                           </td>
                           <td>
                                <asp:TextBox ID="txtDepArrivalPort" runat="server" MaxLength="50" 
                                    style="text-transform:uppercase;"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                                    ControlToValidate="txtDepArrivalPort" ErrorMessage="Arrival Port is required." Text="*"></asp:RequiredFieldValidator>

                          </td>
                           
                          <td style="text-align:right; ">
                          Arrival Port ETA:
                               
                           </td>
                           <td>
                                <asp:TextBox runat="server" ID="txtArrivalPortETA" style="width:100px;text-align:center; " MaxLength="15" ></asp:TextBox>
                                <asp:CalendarExtender runat="server" id="CalendarExtender1" TargetControlID="txtArrivalPortETA" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator12" ControlToValidate="txtArrivalPortETA" ErrorMessage="Arrival Port ETA is required." Text="*"></asp:RequiredFieldValidator>
                                <asp:DropDownList runat="server" ID="ddlArrivalPortETAHH" Width="50px">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="00" Text="00"></asp:ListItem>
                                    <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                    <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                    <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                    <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                    <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                    <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                    <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                    <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                    <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                    <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                    <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                    <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                    <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                    <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                    <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                    <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                    <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                    <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                    <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                    <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                    <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                    <asp:ListItem Value="25" Text="25"></asp:ListItem>
                            </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator13" ControlToValidate="ddlArrivalPortETAHH" ErrorMessage="Arrival Port ETA(HRS) is required." Text="*"></asp:RequiredFieldValidator>
                                <span class="unit">( Hrs )</span>
                                <asp:DropDownList runat="server" ID="ddlArrivalPortETAMM" Width="50px">
                                <asp:ListItem Value="" Text=""></asp:ListItem>
                                <asp:ListItem Value="00" Text="00"></asp:ListItem>                                   
                                <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                <asp:ListItem Value="32" Text="32"></asp:ListItem>
                                <asp:ListItem Value="33" Text="33"></asp:ListItem>
                                <asp:ListItem Value="34" Text="34"></asp:ListItem>
                                <asp:ListItem Value="35" Text="35"></asp:ListItem>
                                <asp:ListItem Value="36" Text="36"></asp:ListItem>
                                <asp:ListItem Value="37" Text="37"></asp:ListItem>
                                <asp:ListItem Value="38" Text="38"></asp:ListItem>
                                <asp:ListItem Value="39" Text="39"></asp:ListItem>
                                <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                <asp:ListItem Value="41" Text="41"></asp:ListItem>
                                <asp:ListItem Value="42" Text="42"></asp:ListItem>
                                <asp:ListItem Value="43" Text="43"></asp:ListItem>
                                <asp:ListItem Value="44" Text="44"></asp:ListItem>
                                <asp:ListItem Value="45" Text="45"></asp:ListItem>
                                <asp:ListItem Value="46" Text="46"></asp:ListItem>
                                <asp:ListItem Value="47" Text="47"></asp:ListItem>
                                <asp:ListItem Value="48" Text="48"></asp:ListItem>
                                <asp:ListItem Value="49" Text="49"></asp:ListItem>
                                <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                <asp:ListItem Value="51" Text="51"></asp:ListItem>
                                <asp:ListItem Value="52" Text="52"></asp:ListItem>
                                <asp:ListItem Value="53" Text="53"></asp:ListItem>
                                <asp:ListItem Value="54" Text="54"></asp:ListItem>
                                <asp:ListItem Value="55" Text="55"></asp:ListItem>
                                <asp:ListItem Value="56" Text="56"></asp:ListItem>
                                <asp:ListItem Value="57" Text="57"></asp:ListItem>
                                <asp:ListItem Value="58" Text="58"></asp:ListItem>
                                <asp:ListItem Value="59" Text="59"></asp:ListItem>

                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator14" ControlToValidate="ddlArrivalPortETAMM" ErrorMessage="Arrival Port ETA(MIN) is required." Text="*"></asp:RequiredFieldValidator>
                                <span class="unit">( Min )</span> 

                          </td>
                            <td></td>
                            <td></td>
                           
                        </tr>   
                        <tr>
                           <td>Draft(Fwd) :</td>
                           <td>
                                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtDepDraftFwd" style="width:70px;text-align:right;" MaxLength="4" ></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator15" ControlToValidate="txtDepDraftFwd" ErrorMessage="Draft(Fwd) is required." Text="*"></asp:RequiredFieldValidator>
                                <span class="unit">&nbsp;MTRS</span>
                           </td>
                          <td style="text-align:right; ">
                              Draft (Aft) :</td>
                           <td>
                                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtDepDraftAft" style="width:70px;text-align:right;" MaxLength="4" ></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator16" ControlToValidate="txtDepDraftAft" ErrorMessage="Draft (Aft) is required." Text="*"></asp:RequiredFieldValidator>
                                <span class="unit">&nbsp;MTRS</span> 

                          </td>
                              <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>    
                        </table>
                   <table cellpadding="0" cellspacing="3" border="0" width="100%" >
                        <tr>
                            <td style="width:180px">
                                Arrival Port Agent :
                            </td>
                            <td colspan="5">
                                  <asp:TextBox runat="server" ID="txtArrivalPortAgent" style="width:80%; text-transform:uppercase;" ></asp:TextBox>
                            </td>
                            
                        </tr>
                        <tr>
                            <td>
                                Personal Incharge :
                            </td>
                            <td colspan="5">
                                <asp:TextBox runat="server" ID="txtPersonalIncharge" style="width:80%; text-transform:uppercase;" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align:top;"> 
                                Address & Contact Details :
                            </td>
                            <td colspan="5">
                                <asp:TextBox runat="server" ID="txtAddressContactDetails" TextMode="MultiLine" style="text-transform:uppercase;width:80%; height:70px;" MaxLength="11" ></asp:TextBox>
                            </td>
                        </tr>
                    </table>
            </td>
        </tr>
        <tr id="trNextPortCall" runat="server" >
            <td style="background-color:#FFFFFF; border:solid 1px #006B8F;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                 <ContentTemplate>
                    <b>Next 3 Port Calls</b>
                    <table cellpadding="0" cellspacing="3" border="0" width="100%" >
                        <tr>
                            <td style="width:180px"> Port Call 1 :</td>
                            <td style="width:250px;">
                                <asp:TextBox ID="txtPortCall1" runat="server" ></asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" ServiceMethod="GetCompletionList" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="5" TargetControlID="txtPortCall1"  CompletionListCssClass="CompletionListCssClass" CompletionListItemCssClass="CompletionListItemCssClass" CompletionListHighlightedItemCssClass="itemHighlighted" ></asp:AutoCompleteExtender>
                            </td >
                            <td style="width:250px;text-align:right;">ETA Date :</td>
                            <td>
                                <asp:TextBox ID="txtPortCallDate1" runat="server"  Width="90px"></asp:TextBox>
                                <asp:CalendarExtender runat="server" id="CalendarExtender3" TargetControlID="txtPortCallDate1" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td> Port Call 2 :</td>
                            <td>
                                <asp:TextBox ID="txtPortCall2" runat="server" ></asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" ServiceMethod="GetCompletionList" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="5" TargetControlID="txtPortCall2"  CompletionListCssClass="CompletionListCssClass" CompletionListItemCssClass="CompletionListItemCssClass" CompletionListHighlightedItemCssClass="itemHighlighted"></asp:AutoCompleteExtender>
                            </td>
                            <td style="text-align:right;"> ETA Date :</td>
                            <td>
                                <asp:TextBox ID="txtPortCallDate2" runat="server" Width="90px" CompletionListCssClass="CompletionListCssClass" CompletionListItemCssClass="CompletionListItemCssClass" CompletionListHighlightedItemCssClass="itemHighlighted"></asp:TextBox>
                                <asp:CalendarExtender runat="server" id="CalendarExtender5" TargetControlID="txtPortCallDate2" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td> Port Call 3 :</td>
                            <td>
                                <asp:TextBox ID="txtPortCall3" runat="server" ></asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" ServiceMethod="GetCompletionList" MinimumPrefixLength="1" CompletionInterval="10" EnableCaching="false" CompletionSetCount="5" TargetControlID="txtPortCall3"  ></asp:AutoCompleteExtender>
                            </td>
                            <td style="text-align:right;"> ETA Date :</td>
                            <td>
                                <asp:TextBox ID="txtPortCallDate3" runat="server"  Width="90px"></asp:TextBox>
                                <asp:CalendarExtender runat="server" id="CalendarExtender6" TargetControlID="txtPortCallDate3" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                            </td>
                        </tr>
                    </table>
                 </ContentTemplate>
                 </asp:UpdatePanel>
            </td>
        </tr>
        <tr id="trSteaming" runat="server" >
            <td style="background-color:#FFFFFF; border:solid 1px #006B8F;">
                <asp:UpdatePanel ID="up1" runat="server">
                 <ContentTemplate>
                   <table cellpadding="0" cellspacing="3" border="0" width="100%" >
                        <tr>
                             <td style="width:180px">
                                Steaming (Since Last Report) :
                              
                            </td>
                            <td style="width:250px">
                            <asp:DropDownList runat="server" ID="ddlSteamingHours" Width="50px" onchange="CalSpeed();">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="00" Text="00"></asp:ListItem>
                                    <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                    <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                    <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                    <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                    <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                    <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                    <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                    <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                    <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                    <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                    <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                    <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                    <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                    <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                    <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                    <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                    <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                    <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                    <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                    <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                    <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                    <asp:ListItem Value="25" Text="25"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator2" ControlToValidate="ddlSteamingHours" ErrorMessage="Steaming(HRS) is required." Text="*"></asp:RequiredFieldValidator>
                            <span class="unit">( Hrs )</span> 
                             <asp:DropDownList runat="server" ID="ddlSteamingMin" Width="50px" onchange="CalSpeed();">
                            <asp:ListItem Value="" Text =""></asp:ListItem>
                            <asp:ListItem Value="00" Text ="00"></asp:ListItem>                                   
                            <asp:ListItem Value="01" Text ="01"></asp:ListItem>
                            <asp:ListItem Value="02" Text ="02"></asp:ListItem>
                            <asp:ListItem Value="03" Text ="03"></asp:ListItem>
                            <asp:ListItem Value="04" Text ="04"></asp:ListItem>
                            <asp:ListItem Value="05" Text ="05"></asp:ListItem>
                            <asp:ListItem Value="06" Text ="06"></asp:ListItem>
                            <asp:ListItem Value="07" Text ="07"></asp:ListItem>
                            <asp:ListItem Value="08" Text ="08"></asp:ListItem>
                            <asp:ListItem Value="09" Text ="09"></asp:ListItem>
                            <asp:ListItem Value="10" Text ="10"></asp:ListItem>
                            <asp:ListItem Value="11" Text ="11"></asp:ListItem>
                            <asp:ListItem Value="12" Text ="12"></asp:ListItem>
                            <asp:ListItem Value="13" Text ="13"></asp:ListItem>
                            <asp:ListItem Value="14" Text ="14"></asp:ListItem>
                            <asp:ListItem Value="15" Text ="15"></asp:ListItem>
                            <asp:ListItem Value="16" Text ="16"></asp:ListItem>
                            <asp:ListItem Value="17" Text ="17"></asp:ListItem>
                            <asp:ListItem Value="18" Text ="18"></asp:ListItem>
                            <asp:ListItem Value="19" Text ="19"></asp:ListItem>
                            <asp:ListItem Value="20" Text ="20"></asp:ListItem>
                            <asp:ListItem Value="21" Text ="21"></asp:ListItem>
                            <asp:ListItem Value="22" Text ="22"></asp:ListItem>
                            <asp:ListItem Value="23" Text ="23"></asp:ListItem>
                            <asp:ListItem Value="24" Text ="24"></asp:ListItem>
                            <asp:ListItem Value="25" Text ="25"></asp:ListItem>
                            <asp:ListItem Value="26" Text ="26"></asp:ListItem>
                            <asp:ListItem Value="27" Text ="27"></asp:ListItem>
                            <asp:ListItem Value="28" Text ="28"></asp:ListItem>
                            <asp:ListItem Value="29" Text ="29"></asp:ListItem>
                            <asp:ListItem Value="30" Text ="30"></asp:ListItem>
                            <asp:ListItem Value="31" Text ="31"></asp:ListItem>
                            <asp:ListItem Value="32" Text ="32"></asp:ListItem>
                            <asp:ListItem Value="33" Text ="33"></asp:ListItem>
                            <asp:ListItem Value="34" Text ="34"></asp:ListItem>
                            <asp:ListItem Value="35" Text ="35"></asp:ListItem>
                            <asp:ListItem Value="36" Text ="36"></asp:ListItem>
                            <asp:ListItem Value="37" Text ="37"></asp:ListItem>
                            <asp:ListItem Value="38" Text ="38"></asp:ListItem>
                            <asp:ListItem Value="39" Text ="39"></asp:ListItem>
                            <asp:ListItem Value="40" Text ="40"></asp:ListItem>
                            <asp:ListItem Value="41" Text ="41"></asp:ListItem>
                            <asp:ListItem Value="42" Text ="42"></asp:ListItem>
                            <asp:ListItem Value="43" Text ="43"></asp:ListItem>
                            <asp:ListItem Value="44" Text ="44"></asp:ListItem>
                            <asp:ListItem Value="45" Text ="45"></asp:ListItem>
                            <asp:ListItem Value="46" Text ="46"></asp:ListItem>
                            <asp:ListItem Value="47" Text ="47"></asp:ListItem>
                            <asp:ListItem Value="48" Text ="48"></asp:ListItem>
                            <asp:ListItem Value="49" Text ="49"></asp:ListItem>
                            <asp:ListItem Value="50" Text ="50"></asp:ListItem>
                            <asp:ListItem Value="51" Text ="51"></asp:ListItem>
                            <asp:ListItem Value="52" Text ="52"></asp:ListItem>
                            <asp:ListItem Value="53" Text ="53"></asp:ListItem>
                            <asp:ListItem Value="54" Text ="54"></asp:ListItem>
                            <asp:ListItem Value="55" Text ="55"></asp:ListItem>
                            <asp:ListItem Value="56" Text ="56"></asp:ListItem>
                            <asp:ListItem Value="57" Text ="57"></asp:ListItem>
                            <asp:ListItem Value="58" Text ="58"></asp:ListItem>
                            <asp:ListItem Value="59" Text ="59"></asp:ListItem>

                                </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator3" ControlToValidate="ddlSteamingMin" ErrorMessage="Steaming(MIN) is required." Text="*" ></asp:RequiredFieldValidator>
                            <span class="unit">( Min )</span>
                                :
                            </td>
                            <td style="text-align:right; width:250px">
                                Distance Made Good (Since Last Report) :
                            </td>
                            <td>
                                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtDistanceMadeGood" onkeyup="CalSlip();CalSpeed();"  style="width:100px;text-align:right;" MaxLength="6" ></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator4" ControlToValidate="txtDistanceMadeGood" ErrorMessage="Distance Made Good is required." Text="*"></asp:RequiredFieldValidator>
                                <span class="unit">NM</span>
                            </td>
                        </tr>
                        <tr>
                             <td style="width:180px">
                                Avg. Speed :
                                &nbsp;</td>
                            <td style="width:250px">
                                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtAvgSpeed" style="width:80px;text-align:right;" MaxLength="5" ReadOnly="true" ></asp:TextBox>
                                <%--<asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator5" ControlToValidate="txtAvgSpeed" ErrorMessage="Avg. Speed is required." Text="*"></asp:RequiredFieldValidator>--%>
                                <span class="unit">KTS</span>
                            </td>
                            <td style="text-align:right; width:250px">
                               </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr valign="top">
                            <td style="width:180px">
                                Stoppages :
                            </td>
                            <td style="width:250px">
                                <asp:DropDownList runat="server" ID="ddlStoppages" AutoPostBack="true" OnSelectedIndexChanged="ddlStoppages_SelectedIndexChanged" Width="50px">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                                    <asp:ListItem Value="No" Text="No"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator6" ControlToValidate="ddlStoppages" ErrorMessage="Stoppages is required." Text="*"></asp:RequiredFieldValidator>
                            </td>
                            <td style="text-align:right; width:250px;"> </td>
                            <td style="text-align:left;">
                               
                            </td>
                        </tr>
                       
                       <%--<tr>
                           <td>Distance To Go :</td>
                           <td>
                                 <asp:TextBox ID="txtDepDistanceToGo" onkeypress="FloatValueOnly(event)" runat="server" MaxLength="5" style="width:70px;text-align:right;"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtDepDistanceToGo" ErrorMessage="Distance To Go is required." Text="*"></asp:RequiredFieldValidator>
                                 <span class="unit">&nbsp;NM</span></td>
                          <td style="text-align:right; width:250px">
                              &nbsp;</td>
                           <td>
                               &nbsp;</td>
                        </tr>  --%>
                        <tr id="trStopagesRemarks" runat="server" >
                            <td style="vertical-align:top;"> Remarks :
                            </td>
                            <td colspan="3">
                                <asp:TextBox runat="server" ID="Remarks" TextMode="MultiLine" style="; text-transform:uppercase;width:80%; height:70px;" MaxLength="11" ></asp:TextBox>
                                <br />
                                Stoppage Time :
                                 <asp:DropDownList runat="server" ID="ddlStoppageTimeHH" Width="50px">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="00" Text="00"></asp:ListItem>
                                    <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                    <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                    <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                    <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                    <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                    <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                    <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                    <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                    <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                    <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                    <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                    <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                    <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                    <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                    <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                    <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                    <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                    <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                    <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                    <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                    <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                    <asp:ListItem Value="25" Text="25"></asp:ListItem>
                            </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator7" ControlToValidate="ddlStoppageTimeHH" ErrorMessage="Stoppage Time(HRS) is required." Text="*"></asp:RequiredFieldValidator>
                                ( Hrs )
                                <asp:DropDownList runat="server" ID="ddlStoppageTimeMM" Width="50px">
                                <asp:ListItem Value="" Text =""></asp:ListItem>
                                <asp:ListItem Value="00" Text ="00"></asp:ListItem>                                   
                                <asp:ListItem Value="01" Text ="01"></asp:ListItem>
                                <asp:ListItem Value="02" Text ="02"></asp:ListItem>
                                <asp:ListItem Value="03" Text ="03"></asp:ListItem>
                                <asp:ListItem Value="04" Text ="04"></asp:ListItem>
                                <asp:ListItem Value="05" Text ="05"></asp:ListItem>
                                <asp:ListItem Value="06" Text ="06"></asp:ListItem>
                                <asp:ListItem Value="07" Text ="07"></asp:ListItem>
                                <asp:ListItem Value="08" Text ="08"></asp:ListItem>
                                <asp:ListItem Value="09" Text ="09"></asp:ListItem>
                                <asp:ListItem Value="10" Text ="10"></asp:ListItem>
                                <asp:ListItem Value="11" Text ="11"></asp:ListItem>
                                <asp:ListItem Value="12" Text ="12"></asp:ListItem>
                                <asp:ListItem Value="13" Text ="13"></asp:ListItem>
                                <asp:ListItem Value="14" Text ="14"></asp:ListItem>
                                <asp:ListItem Value="15" Text ="15"></asp:ListItem>
                                <asp:ListItem Value="16" Text ="16"></asp:ListItem>
                                <asp:ListItem Value="17" Text ="17"></asp:ListItem>
                                <asp:ListItem Value="18" Text ="18"></asp:ListItem>
                                <asp:ListItem Value="19" Text ="19"></asp:ListItem>
                                <asp:ListItem Value="20" Text ="20"></asp:ListItem>
                                <asp:ListItem Value="21" Text ="21"></asp:ListItem>
                                <asp:ListItem Value="22" Text ="22"></asp:ListItem>
                                <asp:ListItem Value="23" Text ="23"></asp:ListItem>
                                <asp:ListItem Value="24" Text ="24"></asp:ListItem>
                                <asp:ListItem Value="25" Text ="25"></asp:ListItem>
                                <asp:ListItem Value="26" Text ="26"></asp:ListItem>
                                <asp:ListItem Value="27" Text ="27"></asp:ListItem>
                                <asp:ListItem Value="28" Text ="28"></asp:ListItem>
                                <asp:ListItem Value="29" Text ="29"></asp:ListItem>
                                <asp:ListItem Value="30" Text ="30"></asp:ListItem>
                                <asp:ListItem Value="31" Text ="31"></asp:ListItem>
                                <asp:ListItem Value="32" Text ="32"></asp:ListItem>
                                <asp:ListItem Value="33" Text ="33"></asp:ListItem>
                                <asp:ListItem Value="34" Text ="34"></asp:ListItem>
                                <asp:ListItem Value="35" Text ="35"></asp:ListItem>
                                <asp:ListItem Value="36" Text ="36"></asp:ListItem>
                                <asp:ListItem Value="37" Text ="37"></asp:ListItem>
                                <asp:ListItem Value="38" Text ="38"></asp:ListItem>
                                <asp:ListItem Value="39" Text ="39"></asp:ListItem>
                                <asp:ListItem Value="40" Text ="40"></asp:ListItem>
                                <asp:ListItem Value="41" Text ="41"></asp:ListItem>
                                <asp:ListItem Value="42" Text ="42"></asp:ListItem>
                                <asp:ListItem Value="43" Text ="43"></asp:ListItem>
                                <asp:ListItem Value="44" Text ="44"></asp:ListItem>
                                <asp:ListItem Value="45" Text ="45"></asp:ListItem>
                                <asp:ListItem Value="46" Text ="46"></asp:ListItem>
                                <asp:ListItem Value="47" Text ="47"></asp:ListItem>
                                <asp:ListItem Value="48" Text ="48"></asp:ListItem>
                                <asp:ListItem Value="49" Text ="49"></asp:ListItem>
                                <asp:ListItem Value="50" Text ="50"></asp:ListItem>
                                <asp:ListItem Value="51" Text ="51"></asp:ListItem>
                                <asp:ListItem Value="52" Text ="52"></asp:ListItem>
                                <asp:ListItem Value="53" Text ="53"></asp:ListItem>
                                <asp:ListItem Value="54" Text ="54"></asp:ListItem>
                                <asp:ListItem Value="55" Text ="55"></asp:ListItem>
                                <asp:ListItem Value="56" Text ="56"></asp:ListItem>
                                <asp:ListItem Value="57" Text ="57"></asp:ListItem>
                                <asp:ListItem Value="58" Text ="58"></asp:ListItem>
                                <asp:ListItem Value="59" Text ="59"></asp:ListItem>

                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator8" ControlToValidate="ddlStoppageTimeMM" ErrorMessage="Stoppage Time(MIN) is required." Text="*"></asp:RequiredFieldValidator>
                                ( Min ) </td>
                        </tr>
                    </table>
                </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        
        
</table>
<div class="div1" runat="server" id="dvPart"><asp:Label runat="Server" ID="lblReportType"></asp:Label> :</div>
<table cellpadding="4" cellspacing="0" border="0" width="100%" >
<tr>
<td style="background-color:#FFFFFF; border:solid 1px #006B8F;">
<asp:Panel runat="server" ID="pnlBerthing" Visible="false">
<table cellpadding="0" cellspacing="3" border="0" width="100%" >
<tr>
                            <td style="width:180px">Berth / Terminal name :</td>
                            <td style="width:320px">
                                <asp:TextBox runat="server" ID="txtBerthTerminalName" style="width:200px;text-align:left; " MaxLength="100" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtBerthTerminalName" ErrorMessage="Berth / Terminal name is required." Text="*"></asp:RequiredFieldValidator>
                            </td>
                            <td style="text-align:right; width:250px">No of TUGS Used :</td>
                            <td >
                                <asp:TextBox runat="server" ID="txtNoOfTUGSUSED" 
                                    style="width:80px;text-align:left; " MaxLength="10" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator44" runat="server" ControlToValidate="txtNoOfTUGSUSED" ErrorMessage="No of TUGS Used is required." Text="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:180px">
                                First Line:</td>
                            <td style="width:320px">
                                <asp:TextBox runat="server" ID="txtFirstLineDate" 
                                    style="width:80px;text-align:center; " MaxLength="15" ></asp:TextBox>
                                <asp:CalendarExtender runat="server" id="txtFirstLineDate_CalendarExtender" 
                                    TargetControlID="txtFirstLineDate" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator50" ControlToValidate="txtFirstLineDate" ErrorMessage="First Line Date is required." Text="*"></asp:RequiredFieldValidator>
                                
                                    <asp:DropDownList runat="server" ID="ddlFirstLineHH" Width="50px">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="00" Text="00"></asp:ListItem>
                                    <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                    <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                    <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                    <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                    <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                    <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                    <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                    <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                    <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                    <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                    <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                    <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                    <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                    <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                    <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                    <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                    <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                    <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                    <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                    <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                    <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                    <asp:ListItem Value="25" Text="25"></asp:ListItem>
                            </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator51" ControlToValidate="ddlFirstLineHH" ErrorMessage="First Line Hrs is required." Text="*"></asp:RequiredFieldValidator>
                                <span class="unit">( Hrs )</span>
                                <asp:DropDownList runat="server" ID="ddlFirstLineMM" Width="50px">
                                <asp:ListItem Value="" Text=""></asp:ListItem>
                                <asp:ListItem Value="00" Text="00"></asp:ListItem>                                   
                                <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                <asp:ListItem Value="32" Text="32"></asp:ListItem>
                                <asp:ListItem Value="33" Text="33"></asp:ListItem>
                                <asp:ListItem Value="34" Text="34"></asp:ListItem>
                                <asp:ListItem Value="35" Text="35"></asp:ListItem>
                                <asp:ListItem Value="36" Text="36"></asp:ListItem>
                                <asp:ListItem Value="37" Text="37"></asp:ListItem>
                                <asp:ListItem Value="38" Text="38"></asp:ListItem>
                                <asp:ListItem Value="39" Text="39"></asp:ListItem>
                                <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                <asp:ListItem Value="41" Text="41"></asp:ListItem>
                                <asp:ListItem Value="42" Text="42"></asp:ListItem>
                                <asp:ListItem Value="43" Text="43"></asp:ListItem>
                                <asp:ListItem Value="44" Text="44"></asp:ListItem>
                                <asp:ListItem Value="45" Text="45"></asp:ListItem>
                                <asp:ListItem Value="46" Text="46"></asp:ListItem>
                                <asp:ListItem Value="47" Text="47"></asp:ListItem>
                                <asp:ListItem Value="48" Text="48"></asp:ListItem>
                                <asp:ListItem Value="49" Text="49"></asp:ListItem>
                                <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                <asp:ListItem Value="51" Text="51"></asp:ListItem>
                                <asp:ListItem Value="52" Text="52"></asp:ListItem>
                                <asp:ListItem Value="53" Text="53"></asp:ListItem>
                                <asp:ListItem Value="54" Text="54"></asp:ListItem>
                                <asp:ListItem Value="55" Text="55"></asp:ListItem>
                                <asp:ListItem Value="56" Text="56"></asp:ListItem>
                                <asp:ListItem Value="57" Text="57"></asp:ListItem>
                                <asp:ListItem Value="58" Text="58"></asp:ListItem>
                                <asp:ListItem Value="59" Text="59"></asp:ListItem>

                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator52" ControlToValidate="ddlFirstLineMM" ErrorMessage="First Line Min is required." Text="*"></asp:RequiredFieldValidator>
                                <span class="unit">( Min )</span> 
                                </td>
                            <td style="text-align:right; width:250px">
                                All Fast :</td>
                            <td > 
                            <asp:TextBox runat="server" ID="txtAllFastDate" 
                                    style="width:80px;text-align:center; " MaxLength="15" ></asp:TextBox>
                                <asp:CalendarExtender runat="server" id="CalendarExtender2" 
                                    TargetControlID="txtAllFastDate" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator53" ControlToValidate="txtAllFastDate" ErrorMessage="All Fast Date is required." Text="*"></asp:RequiredFieldValidator>
                                
                                        <asp:DropDownList runat="server" ID="ddlAllFastHH" Width="50px">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="00" Text="00"></asp:ListItem>
                                    <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                    <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                    <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                    <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                    <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                    <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                    <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                    <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                    <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                    <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                    <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                    <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                    <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                    <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                    <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                    <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                    <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                    <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                    <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                    <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                    <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                    <asp:ListItem Value="25" Text="25"></asp:ListItem>
                            </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator54" 
                                    ControlToValidate="ddlAllFastHH" ErrorMessage="All Fast Hrs is required." Text="*"></asp:RequiredFieldValidator>
                                <span class="unit">( Hrs )</span>
                                <asp:DropDownList runat="server" ID="ddlAllFastMM" Width="50px">
                                <asp:ListItem Value="" Text=""></asp:ListItem>
                                <asp:ListItem Value="00" Text="00"></asp:ListItem>                                   
                                <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                <asp:ListItem Value="32" Text="32"></asp:ListItem>
                                <asp:ListItem Value="33" Text="33"></asp:ListItem>
                                <asp:ListItem Value="34" Text="34"></asp:ListItem>
                                <asp:ListItem Value="35" Text="35"></asp:ListItem>
                                <asp:ListItem Value="36" Text="36"></asp:ListItem>
                                <asp:ListItem Value="37" Text="37"></asp:ListItem>
                                <asp:ListItem Value="38" Text="38"></asp:ListItem>
                                <asp:ListItem Value="39" Text="39"></asp:ListItem>
                                <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                <asp:ListItem Value="41" Text="41"></asp:ListItem>
                                <asp:ListItem Value="42" Text="42"></asp:ListItem>
                                <asp:ListItem Value="43" Text="43"></asp:ListItem>
                                <asp:ListItem Value="44" Text="44"></asp:ListItem>
                                <asp:ListItem Value="45" Text="45"></asp:ListItem>
                                <asp:ListItem Value="46" Text="46"></asp:ListItem>
                                <asp:ListItem Value="47" Text="47"></asp:ListItem>
                                <asp:ListItem Value="48" Text="48"></asp:ListItem>
                                <asp:ListItem Value="49" Text="49"></asp:ListItem>
                                <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                <asp:ListItem Value="51" Text="51"></asp:ListItem>
                                <asp:ListItem Value="52" Text="52"></asp:ListItem>
                                <asp:ListItem Value="53" Text="53"></asp:ListItem>
                                <asp:ListItem Value="54" Text="54"></asp:ListItem>
                                <asp:ListItem Value="55" Text="55"></asp:ListItem>
                                <asp:ListItem Value="56" Text="56"></asp:ListItem>
                                <asp:ListItem Value="57" Text="57"></asp:ListItem>
                                <asp:ListItem Value="58" Text="58"></asp:ListItem>
                                <asp:ListItem Value="59" Text="59"></asp:ListItem>

                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator55" 
                                    ControlToValidate="ddlAllFastMM" ErrorMessage="All Fast Min is required." Text="*"></asp:RequiredFieldValidator>
                                <span class="unit">( Min )</span> 
                                </td>
                        </tr>
                        <tr>
                            <td style="width:180px">
                                ETD Date :</td>
                            <td style="width:320px">
                                <asp:TextBox runat="server" ID="txtETDDateTime" style="width:80px;text-align:center; " MaxLength="15" ></asp:TextBox>
                                <asp:CalendarExtender runat="server" id="txtETDDateTime_CalendarExtender" TargetControlID="txtETDDateTime" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator56" ControlToValidate="txtETDDateTime" ErrorMessage="ETD Date is required." Text="*"></asp:RequiredFieldValidator>
                            </td>
                            <td style="text-align:right; width:250px">
                                ETD Time :</td>
                            <td >
                                        <asp:DropDownList runat="server" ID="ddlETDDateTimeHH" Width="50px">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="00" Text="00"></asp:ListItem>
                                    <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                    <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                    <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                    <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                    <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                    <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                    <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                    <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                    <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                    <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                    <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                    <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                    <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                    <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                    <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                    <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                    <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                    <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                    <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                    <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                    <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                    <asp:ListItem Value="25" Text="25"></asp:ListItem>
                            </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator57" ControlToValidate="ddlETDDateTimeHH" ErrorMessage="ETD Hrs is required." Text="*"></asp:RequiredFieldValidator>
                                <span class="unit">( Hrs )</span>
                                <asp:DropDownList runat="server" ID="ddlETDDateTimeMM" Width="50px">
                                <asp:ListItem Value="" Text=""></asp:ListItem>
                                <asp:ListItem Value="00" Text="00"></asp:ListItem>                                   
                                <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                <asp:ListItem Value="32" Text="32"></asp:ListItem>
                                <asp:ListItem Value="33" Text="33"></asp:ListItem>
                                <asp:ListItem Value="34" Text="34"></asp:ListItem>
                                <asp:ListItem Value="35" Text="35"></asp:ListItem>
                                <asp:ListItem Value="36" Text="36"></asp:ListItem>
                                <asp:ListItem Value="37" Text="37"></asp:ListItem>
                                <asp:ListItem Value="38" Text="38"></asp:ListItem>
                                <asp:ListItem Value="39" Text="39"></asp:ListItem>
                                <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                <asp:ListItem Value="41" Text="41"></asp:ListItem>
                                <asp:ListItem Value="42" Text="42"></asp:ListItem>
                                <asp:ListItem Value="43" Text="43"></asp:ListItem>
                                <asp:ListItem Value="44" Text="44"></asp:ListItem>
                                <asp:ListItem Value="45" Text="45"></asp:ListItem>
                                <asp:ListItem Value="46" Text="46"></asp:ListItem>
                                <asp:ListItem Value="47" Text="47"></asp:ListItem>
                                <asp:ListItem Value="48" Text="48"></asp:ListItem>
                                <asp:ListItem Value="49" Text="49"></asp:ListItem>
                                <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                <asp:ListItem Value="51" Text="51"></asp:ListItem>
                                <asp:ListItem Value="52" Text="52"></asp:ListItem>
                                <asp:ListItem Value="53" Text="53"></asp:ListItem>
                                <asp:ListItem Value="54" Text="54"></asp:ListItem>
                                <asp:ListItem Value="55" Text="55"></asp:ListItem>
                                <asp:ListItem Value="56" Text="56"></asp:ListItem>
                                <asp:ListItem Value="57" Text="57"></asp:ListItem>
                                <asp:ListItem Value="58" Text="58"></asp:ListItem>
                                <asp:ListItem Value="59" Text="59"></asp:ListItem>

                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator58" ControlToValidate="ddlETDDateTimeMM" ErrorMessage="ETD Min is required." Text="*"></asp:RequiredFieldValidator>
                                <span class="unit">( Min )</span> 
                                </td>
                        </tr>
                        </table>
</asp:Panel>
<asp:Panel runat="server" ID="pnlDrifting"  Visible="false">
<table cellpadding="0" cellspacing="3" border="0" width="100%" >
   <tr>
                            <td style="width:180px">Comm&#39;d&nbsp; Drifting :</td>
                            <td style="width:320px">
                                <asp:TextBox runat="server" ID="txtCdDrifting" style="width:80px;text-align:center; " MaxLength="15" ></asp:TextBox>
                                <asp:CalendarExtender runat="server" id="txtCdDrifting_CalendarExtender" TargetControlID="txtCdDrifting" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator59" ControlToValidate="txtCdDrifting" ErrorMessage="Comm'd  Drifting is required." Text="*"></asp:RequiredFieldValidator>
                                <asp:DropDownList runat="server" ID="ddlCdDriftingHours" Width="50px">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="00" Text="00"></asp:ListItem>
                                    <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                    <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                    <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                    <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                    <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                    <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                    <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                    <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                    <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                    <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                    <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                    <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                    <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                    <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                    <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                    <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                    <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                    <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                    <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                    <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                    <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                    <asp:ListItem Value="25" Text="25"></asp:ListItem>
                            </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator60" ControlToValidate="ddlCdDriftingHours" ErrorMessage="Comm'd  Drifting Hrs is required." Text="*"></asp:RequiredFieldValidator>
                                <span class="unit">( Hrs )</span>
                                <asp:DropDownList runat="server" ID="ddlCdDriftingMin" Width="50px">
                                <asp:ListItem Value="" Text=""></asp:ListItem>
                                <asp:ListItem Value="00" Text="00"></asp:ListItem>                                   
                                <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                <asp:ListItem Value="32" Text="32"></asp:ListItem>
                                <asp:ListItem Value="33" Text="33"></asp:ListItem>
                                <asp:ListItem Value="34" Text="34"></asp:ListItem>
                                <asp:ListItem Value="35" Text="35"></asp:ListItem>
                                <asp:ListItem Value="36" Text="36"></asp:ListItem>
                                <asp:ListItem Value="37" Text="37"></asp:ListItem>
                                <asp:ListItem Value="38" Text="38"></asp:ListItem>
                                <asp:ListItem Value="39" Text="39"></asp:ListItem>
                                <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                <asp:ListItem Value="41" Text="41"></asp:ListItem>
                                <asp:ListItem Value="42" Text="42"></asp:ListItem>
                                <asp:ListItem Value="43" Text="43"></asp:ListItem>
                                <asp:ListItem Value="44" Text="44"></asp:ListItem>
                                <asp:ListItem Value="45" Text="45"></asp:ListItem>
                                <asp:ListItem Value="46" Text="46"></asp:ListItem>
                                <asp:ListItem Value="47" Text="47"></asp:ListItem>
                                <asp:ListItem Value="48" Text="48"></asp:ListItem>
                                <asp:ListItem Value="49" Text="49"></asp:ListItem>
                                <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                <asp:ListItem Value="51" Text="51"></asp:ListItem>
                                <asp:ListItem Value="52" Text="52"></asp:ListItem>
                                <asp:ListItem Value="53" Text="53"></asp:ListItem>
                                <asp:ListItem Value="54" Text="54"></asp:ListItem>
                                <asp:ListItem Value="55" Text="55"></asp:ListItem>
                                <asp:ListItem Value="56" Text="56"></asp:ListItem>
                                <asp:ListItem Value="57" Text="57"></asp:ListItem>
                                <asp:ListItem Value="58" Text="58"></asp:ListItem>
                                <asp:ListItem Value="59" Text="59"></asp:ListItem>

                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator61" ControlToValidate="ddlCdDriftingMin" ErrorMessage="Comm'd  Drifting Min is required." Text="*"></asp:RequiredFieldValidator>
                                <span class="unit">( Min )</span> 
                                </td>
                            <td style="text-align:right; width:250px">
                                Drifting Reason :</td>
                            <td >
                                <asp:DropDownList runat="server" ID="ddlDriftingReason" Width="200px">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="B" Text="Berth occupied"></asp:ListItem>
                                    <asp:ListItem Value="A" Text="Awaiting tidal window"></asp:ListItem>
                                    <asp:ListItem Value="N" Text="No night navigation"></asp:ListItem>
                                    <asp:ListItem Value="P" Text="Port close"></asp:ListItem>
                                </asp:DropDownList>
</td>
                        </tr>
                        
                        </table>
</asp:Panel>
<asp:Panel runat="server" ID="pnlAnchoring"  Visible="false">
<table cellpadding="0" cellspacing="3" border="0" width="100%" >
 <tr>
                            <td style="width:180px">POB ( if any ) :</td>
                            <td style="width:320px">
                                <asp:TextBox runat="server" ID="txtPOB" style="width:200px;text-align:left; " MaxLength="100" ></asp:TextBox>
                            </td>
                            <td style="text-align:right; width:250px">Anchoring Reason :</td>
                            <td >
                                <asp:TextBox runat="server" ID="txtAnchoragereasion" style="width:200px;text-align:left; " MaxLength="100" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator65" runat="server" ControlToValidate="txtAnchoragereasion" ErrorMessage="Anchoring Reason is required." Text="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:180px">
                                Let Go Anchor :</td>
                            <td style="width:320px">
                                <asp:TextBox runat="server" ID="txtLetGoAnchorage" style="width:80px;text-align:center; " MaxLength="15" ></asp:TextBox>
                                <asp:CalendarExtender runat="server" id="txtLetGoAnchorage_CalendarExtender" TargetControlID="txtLetGoAnchorage" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator66" ControlToValidate="txtLetGoAnchorage" ErrorMessage="Let Go Anchor is required." Text="*"></asp:RequiredFieldValidator>
                                
                                    <asp:DropDownList runat="server" ID="ddlLetGoAnchorageHH" Width="50px">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="00" Text="00"></asp:ListItem>
                                    <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                    <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                    <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                    <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                    <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                    <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                    <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                    <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                    <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                    <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                    <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                    <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                    <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                    <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                    <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                    <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                    <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                    <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                    <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                    <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                    <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                    <asp:ListItem Value="25" Text="25"></asp:ListItem>
                            </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator67" ControlToValidate="ddlLetGoAnchorageHH" ErrorMessage="Let Go Anchor Hrs is required." Text="*"></asp:RequiredFieldValidator>
                                <span class="unit">( Hrs )</span>
                                <asp:DropDownList runat="server" ID="ddlLetGoAnchorageMM" Width="50px">
                                <asp:ListItem Value="" Text=""></asp:ListItem>
                                <asp:ListItem Value="00" Text="00"></asp:ListItem>                                   
                                <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                <asp:ListItem Value="32" Text="32"></asp:ListItem>
                                <asp:ListItem Value="33" Text="33"></asp:ListItem>
                                <asp:ListItem Value="34" Text="34"></asp:ListItem>
                                <asp:ListItem Value="35" Text="35"></asp:ListItem>
                                <asp:ListItem Value="36" Text="36"></asp:ListItem>
                                <asp:ListItem Value="37" Text="37"></asp:ListItem>
                                <asp:ListItem Value="38" Text="38"></asp:ListItem>
                                <asp:ListItem Value="39" Text="39"></asp:ListItem>
                                <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                <asp:ListItem Value="41" Text="41"></asp:ListItem>
                                <asp:ListItem Value="42" Text="42"></asp:ListItem>
                                <asp:ListItem Value="43" Text="43"></asp:ListItem>
                                <asp:ListItem Value="44" Text="44"></asp:ListItem>
                                <asp:ListItem Value="45" Text="45"></asp:ListItem>
                                <asp:ListItem Value="46" Text="46"></asp:ListItem>
                                <asp:ListItem Value="47" Text="47"></asp:ListItem>
                                <asp:ListItem Value="48" Text="48"></asp:ListItem>
                                <asp:ListItem Value="49" Text="49"></asp:ListItem>
                                <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                <asp:ListItem Value="51" Text="51"></asp:ListItem>
                                <asp:ListItem Value="52" Text="52"></asp:ListItem>
                                <asp:ListItem Value="53" Text="53"></asp:ListItem>
                                <asp:ListItem Value="54" Text="54"></asp:ListItem>
                                <asp:ListItem Value="55" Text="55"></asp:ListItem>
                                <asp:ListItem Value="56" Text="56"></asp:ListItem>
                                <asp:ListItem Value="57" Text="57"></asp:ListItem>
                                <asp:ListItem Value="58" Text="58"></asp:ListItem>
                                <asp:ListItem Value="59" Text="59"></asp:ListItem>

                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator68" ControlToValidate="ddlLetGoAnchorageMM" ErrorMessage="Let Go Anchor Min is required." Text="*"></asp:RequiredFieldValidator>
                                <span class="unit">( Min )</span> 
                                </td>
                            <td style="text-align:right; width:250px">
                                Pilot Away :</td>
                            <td >
                                        <asp:DropDownList runat="server" ID="ddlpilotAwayHH" Width="50px">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="00" Text="00"></asp:ListItem>
                                    <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                    <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                    <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                    <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                    <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                    <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                    <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                    <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                    <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                    <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                    <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                    <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                    <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                    <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                    <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                    <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                    <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                    <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                    <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                    <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                    <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                    <asp:ListItem Value="25" Text="25"></asp:ListItem>
                            </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator69" 
                                    ControlToValidate="ddlpilotAwayHH" ErrorMessage="Pilot Away Hrs is required." Text="*"></asp:RequiredFieldValidator>
                                <span class="unit">( Hrs )</span>
                                <asp:DropDownList runat="server" ID="ddlpilotAwayMM" Width="50px">
                                <asp:ListItem Value="" Text=""></asp:ListItem>
                                <asp:ListItem Value="00" Text="00"></asp:ListItem>                                   
                                <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                <asp:ListItem Value="32" Text="32"></asp:ListItem>
                                <asp:ListItem Value="33" Text="33"></asp:ListItem>
                                <asp:ListItem Value="34" Text="34"></asp:ListItem>
                                <asp:ListItem Value="35" Text="35"></asp:ListItem>
                                <asp:ListItem Value="36" Text="36"></asp:ListItem>
                                <asp:ListItem Value="37" Text="37"></asp:ListItem>
                                <asp:ListItem Value="38" Text="38"></asp:ListItem>
                                <asp:ListItem Value="39" Text="39"></asp:ListItem>
                                <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                <asp:ListItem Value="41" Text="41"></asp:ListItem>
                                <asp:ListItem Value="42" Text="42"></asp:ListItem>
                                <asp:ListItem Value="43" Text="43"></asp:ListItem>
                                <asp:ListItem Value="44" Text="44"></asp:ListItem>
                                <asp:ListItem Value="45" Text="45"></asp:ListItem>
                                <asp:ListItem Value="46" Text="46"></asp:ListItem>
                                <asp:ListItem Value="47" Text="47"></asp:ListItem>
                                <asp:ListItem Value="48" Text="48"></asp:ListItem>
                                <asp:ListItem Value="49" Text="49"></asp:ListItem>
                                <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                <asp:ListItem Value="51" Text="51"></asp:ListItem>
                                <asp:ListItem Value="52" Text="52"></asp:ListItem>
                                <asp:ListItem Value="53" Text="53"></asp:ListItem>
                                <asp:ListItem Value="54" Text="54"></asp:ListItem>
                                <asp:ListItem Value="55" Text="55"></asp:ListItem>
                                <asp:ListItem Value="56" Text="56"></asp:ListItem>
                                <asp:ListItem Value="57" Text="57"></asp:ListItem>
                                <asp:ListItem Value="58" Text="58"></asp:ListItem>
                                <asp:ListItem Value="59" Text="59"></asp:ListItem>

                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator70" 
                                    ControlToValidate="ddlpilotAwayMM" ErrorMessage="Pilot Away Min is required." Text="*"></asp:RequiredFieldValidator>
                                <span class="unit">( Min )</span> 
                                </td>
                        </tr>
                        
                        </table>
</asp:Panel>
<table cellpadding="0" cellspacing="3" border="0" width="100%" runat="server" id="tblETBDate" visible="false" >
<tr>
                            <td style="width:180px">
                                ETB Date :</td>
                            <td style="width:320px">
                                <asp:TextBox runat="server" ID="txtETBDate" 
                                    style="width:100px;text-align:center; " MaxLength="15" ></asp:TextBox>
                                <asp:CalendarExtender runat="server" id="txtETBDate_CalendarExtender" 
                                    TargetControlID="txtETBDate" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator62" 
                                    ControlToValidate="txtETBDate" ErrorMessage="ETB Date is required." Text="*"></asp:RequiredFieldValidator>
                            </td>
                            <td style="text-align:right; width:250px">
                                ETB Time :</td>
                            <td >
                                        <asp:DropDownList runat="server" ID="ddlETBHours" Width="50px">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="00" Text="00"></asp:ListItem>
                                    <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                    <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                    <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                    <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                    <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                    <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                    <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                    <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                    <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                    <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                    <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                    <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                    <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                    <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                    <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                    <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                    <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                    <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                    <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                    <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                    <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                    <asp:ListItem Value="25" Text="25"></asp:ListItem>
                            </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator63" ControlToValidate="ddlETBHours" ErrorMessage="ETB Hrs is required." Text="*"></asp:RequiredFieldValidator>
                                <span class="unit">( Hrs )</span>
                                <asp:DropDownList runat="server" ID="ddlETBMins" Width="50px">
                                <asp:ListItem Value="" Text=""></asp:ListItem>
                                <asp:ListItem Value="00" Text="00"></asp:ListItem>                                   
                                <asp:ListItem Value="01" Text="01"></asp:ListItem>
                                <asp:ListItem Value="02" Text="02"></asp:ListItem>
                                <asp:ListItem Value="03" Text="03"></asp:ListItem>
                                <asp:ListItem Value="04" Text="04"></asp:ListItem>
                                <asp:ListItem Value="05" Text="05"></asp:ListItem>
                                <asp:ListItem Value="06" Text="06"></asp:ListItem>
                                <asp:ListItem Value="07" Text="07"></asp:ListItem>
                                <asp:ListItem Value="08" Text="08"></asp:ListItem>
                                <asp:ListItem Value="09" Text="09"></asp:ListItem>
                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                <asp:ListItem Value="32" Text="32"></asp:ListItem>
                                <asp:ListItem Value="33" Text="33"></asp:ListItem>
                                <asp:ListItem Value="34" Text="34"></asp:ListItem>
                                <asp:ListItem Value="35" Text="35"></asp:ListItem>
                                <asp:ListItem Value="36" Text="36"></asp:ListItem>
                                <asp:ListItem Value="37" Text="37"></asp:ListItem>
                                <asp:ListItem Value="38" Text="38"></asp:ListItem>
                                <asp:ListItem Value="39" Text="39"></asp:ListItem>
                                <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                <asp:ListItem Value="41" Text="41"></asp:ListItem>
                                <asp:ListItem Value="42" Text="42"></asp:ListItem>
                                <asp:ListItem Value="43" Text="43"></asp:ListItem>
                                <asp:ListItem Value="44" Text="44"></asp:ListItem>
                                <asp:ListItem Value="45" Text="45"></asp:ListItem>
                                <asp:ListItem Value="46" Text="46"></asp:ListItem>
                                <asp:ListItem Value="47" Text="47"></asp:ListItem>
                                <asp:ListItem Value="48" Text="48"></asp:ListItem>
                                <asp:ListItem Value="49" Text="49"></asp:ListItem>
                                <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                <asp:ListItem Value="51" Text="51"></asp:ListItem>
                                <asp:ListItem Value="52" Text="52"></asp:ListItem>
                                <asp:ListItem Value="53" Text="53"></asp:ListItem>
                                <asp:ListItem Value="54" Text="54"></asp:ListItem>
                                <asp:ListItem Value="55" Text="55"></asp:ListItem>
                                <asp:ListItem Value="56" Text="56"></asp:ListItem>
                                <asp:ListItem Value="57" Text="57"></asp:ListItem>
                                <asp:ListItem Value="58" Text="58"></asp:ListItem>
                                <asp:ListItem Value="59" Text="59"></asp:ListItem>

                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator64" ControlToValidate="ddlETBMins" ErrorMessage="ETB Min is required." Text="*"></asp:RequiredFieldValidator>
                                <span class="unit">( Min )</span> 
                                </td>
                        </tr>
</table>
<asp:Panel runat="server" ID="pnlArr"  Visible="false">
<table cellpadding="0" cellspacing="3" border="0" width="100%">
<tr>
    <td style="width:180px">  EOSP Date &amp; Time :</td>
    <td>
            <asp:TextBox ID="txtEOSPDate" runat="server" MaxLength="15" 
                style="width: 100px; text-align: center;"></asp:TextBox>
            <asp:CalendarExtender ID="CalendarExtender4" runat="server" 
                Format="dd-MMM-yyyy" TargetControlID="txtEOSPDate">
            </asp:CalendarExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator76" runat="server" 
                ControlToValidate="txtEOSPDate" ErrorMessage="EOSP Date is required." Text="*"></asp:RequiredFieldValidator>
            <asp:DropDownList ID="ddlEOSPHours" runat="server" Width="50px">
                <asp:ListItem Text="" Value=""></asp:ListItem>
                <asp:ListItem Text="00" Value="00"></asp:ListItem>
                <asp:ListItem Text="01" Value="01"></asp:ListItem>
                <asp:ListItem Text="02" Value="02"></asp:ListItem>
                <asp:ListItem Text="03" Value="03"></asp:ListItem>
                <asp:ListItem Text="04" Value="04"></asp:ListItem>
                <asp:ListItem Text="05" Value="05"></asp:ListItem>
                <asp:ListItem Text="06" Value="06"></asp:ListItem>
                <asp:ListItem Text="07" Value="07"></asp:ListItem>
                <asp:ListItem Text="08" Value="08"></asp:ListItem>
                <asp:ListItem Text="09" Value="09"></asp:ListItem>
                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                <asp:ListItem Text="11" Value="11"></asp:ListItem>
                <asp:ListItem Text="12" Value="12"></asp:ListItem>
                <asp:ListItem Text="13" Value="13"></asp:ListItem>
                <asp:ListItem Text="14" Value="14"></asp:ListItem>
                <asp:ListItem Text="15" Value="15"></asp:ListItem>
                <asp:ListItem Text="16" Value="16"></asp:ListItem>
                <asp:ListItem Text="17" Value="17"></asp:ListItem>
                <asp:ListItem Text="18" Value="18"></asp:ListItem>
                <asp:ListItem Text="19" Value="19"></asp:ListItem>
                <asp:ListItem Text="20" Value="20"></asp:ListItem>
                <asp:ListItem Text="21" Value="21"></asp:ListItem>
                <asp:ListItem Text="22" Value="22"></asp:ListItem>
                <asp:ListItem Text="23" Value="23"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator77" runat="server" 
                ControlToValidate="ddlEOSPHours" ErrorMessage="EOSP Time(HRS) is required." 
                Text="*"></asp:RequiredFieldValidator>
            <span class="unit">( Hrs )</span>
            <asp:DropDownList ID="ddlEOSPMin" runat="server" Width="50px">
                <asp:ListItem Text="" Value=""></asp:ListItem>
                <asp:ListItem Text="00" Value="00"></asp:ListItem>
                <asp:ListItem Text="01" Value="01"></asp:ListItem>
                <asp:ListItem Text="02" Value="02"></asp:ListItem>
                <asp:ListItem Text="03" Value="03"></asp:ListItem>
                <asp:ListItem Text="04" Value="04"></asp:ListItem>
                <asp:ListItem Text="05" Value="05"></asp:ListItem>
                <asp:ListItem Text="06" Value="06"></asp:ListItem>
                <asp:ListItem Text="07" Value="07"></asp:ListItem>
                <asp:ListItem Text="08" Value="08"></asp:ListItem>
                <asp:ListItem Text="09" Value="09"></asp:ListItem>
                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                <asp:ListItem Text="11" Value="11"></asp:ListItem>
                <asp:ListItem Text="12" Value="12"></asp:ListItem>
                <asp:ListItem Text="13" Value="13"></asp:ListItem>
                <asp:ListItem Text="14" Value="14"></asp:ListItem>
                <asp:ListItem Text="15" Value="15"></asp:ListItem>
                <asp:ListItem Text="16" Value="16"></asp:ListItem>
                <asp:ListItem Text="17" Value="17"></asp:ListItem>
                <asp:ListItem Text="18" Value="18"></asp:ListItem>
                <asp:ListItem Text="19" Value="19"></asp:ListItem>
                <asp:ListItem Text="20" Value="20"></asp:ListItem>
                <asp:ListItem Text="21" Value="21"></asp:ListItem>
                <asp:ListItem Text="22" Value="22"></asp:ListItem>
                <asp:ListItem Text="23" Value="23"></asp:ListItem>
                <asp:ListItem Text="24" Value="24"></asp:ListItem>
                <asp:ListItem Text="25" Value="25"></asp:ListItem>
                <asp:ListItem Text="26" Value="26"></asp:ListItem>
                <asp:ListItem Text="27" Value="27"></asp:ListItem>
                <asp:ListItem Text="28" Value="28"></asp:ListItem>
                <asp:ListItem Text="29" Value="29"></asp:ListItem>
                <asp:ListItem Text="30" Value="30"></asp:ListItem>
                <asp:ListItem Text="31" Value="31"></asp:ListItem>
                <asp:ListItem Text="32" Value="32"></asp:ListItem>
                <asp:ListItem Text="33" Value="33"></asp:ListItem>
                <asp:ListItem Text="34" Value="34"></asp:ListItem>
                <asp:ListItem Text="35" Value="35"></asp:ListItem>
                <asp:ListItem Text="36" Value="36"></asp:ListItem>
                <asp:ListItem Text="37" Value="37"></asp:ListItem>
                <asp:ListItem Text="38" Value="38"></asp:ListItem>
                <asp:ListItem Text="39" Value="39"></asp:ListItem>
                <asp:ListItem Text="40" Value="40"></asp:ListItem>
                <asp:ListItem Text="41" Value="41"></asp:ListItem>
                <asp:ListItem Text="42" Value="42"></asp:ListItem>
                <asp:ListItem Text="43" Value="43"></asp:ListItem>
                <asp:ListItem Text="44" Value="44"></asp:ListItem>
                <asp:ListItem Text="45" Value="45"></asp:ListItem>
                <asp:ListItem Text="46" Value="46"></asp:ListItem>
                <asp:ListItem Text="47" Value="47"></asp:ListItem>
                <asp:ListItem Text="48" Value="48"></asp:ListItem>
                <asp:ListItem Text="49" Value="49"></asp:ListItem>
                <asp:ListItem Text="50" Value="50"></asp:ListItem>
                <asp:ListItem Text="51" Value="51"></asp:ListItem>
                <asp:ListItem Text="52" Value="52"></asp:ListItem>
                <asp:ListItem Text="53" Value="53"></asp:ListItem>
                <asp:ListItem Text="54" Value="54"></asp:ListItem>
                <asp:ListItem Text="55" Value="55"></asp:ListItem>
                <asp:ListItem Text="56" Value="56"></asp:ListItem>
                <asp:ListItem Text="57" Value="57"></asp:ListItem>
                <asp:ListItem Text="58" Value="58"></asp:ListItem>
                <asp:ListItem Text="59" Value="59"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator78" runat="server" 
                ControlToValidate="ddlEOSPMin" ErrorMessage="EOSP Time(MIN) is required." 
                Text="*"></asp:RequiredFieldValidator>
            <span class="unit">( Min )</span> <i style="font-size:9px">(Ship's LT)</i>
    </td>
                          
</tr>    
</table>

</asp:Panel>
 </td>
</tr>
</table>

<div class="div1">Weather Information :</div>
<table cellpadding="4" cellspacing="0" border="0" width="100%" >
        <tr>
            <td style="background-color:#FFFFFF; border:solid 1px #006B8F;">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" >
                    <tr>
                        <td style="width:370px">
                            <table width="100%">
                            <col width="180px" />
                                <tr>
                                    <td>
                                        Course(T) :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" onkeypress="IntValueOnly(event);MaxInt360(this);" ID="txtCource" style="width:50px;text-align:right;" MaxLength="3" ></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator18" ControlToValidate="txtCource" ErrorMessage="Course(T) is required." Text="*"></asp:RequiredFieldValidator>
                                        <span class="unit">Deg</span><span class="range">&nbsp;(0-360) </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Wind Direction(T) : 
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" onkeypress="IntValueOnly(event);MaxInt360(this);" ID="txtWindDirection" style="width:50px;text-align:right;" MaxLength="3" ></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator19" ControlToValidate="txtWindDirection" ErrorMessage="Wind Direction(T) is required." Text="*"></asp:RequiredFieldValidator>
                                        <span class="unit">Deg</span><span class="range">&nbsp;(0-360) </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Wind Force :
                                    </td>
                                    <td>
                                    <asp:DropDownList runat="server" ID="ddlWindForce" Width="57px">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="0" Text="0"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                    <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator22" ControlToValidate="ddlWindForce" ErrorMessage=" Wind Force is required." Text="*"></asp:RequiredFieldValidator>
                                    <span class="unit">BF</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Sea Direction(T) : 
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" onkeypress="IntValueOnly(event);MaxInt360(this);" ID="txtSeaDirection" style="width:50px;text-align:right;" MaxLength="3" ></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator20" ControlToValidate="txtSeaDirection" ErrorMessage="Sea Direction(T) is required." Text="*"></asp:RequiredFieldValidator>
                                        <span class="unit">Deg</span><span class="range">&nbsp;(0-360) </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Sea State :
                                    </td>
                                    <td>
                                    <asp:DropDownList runat="server" ID="ddlSeaState" Width="57px">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="0" Text="0"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                    <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator23" ControlToValidate="ddlSeaState" ErrorMessage="Sea State is required." Text="*"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Current Direction(T) :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" onkeypress="IntValueOnly(event);MaxInt360(this);" ID="txtCurrentDirection" style="width:50px;text-align:right;" MaxLength="3" ></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator21" ControlToValidate="txtCurrentDirection" ErrorMessage="Current Direction(T) is required." Text="*"></asp:RequiredFieldValidator>
                                        <span class="unit">Deg</span><span class="range">&nbsp;(0-360) </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Current Strength : 
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" onkeypress="IntValueOnly(event);" ID="txtCurrentStrength" style="width:50px;text-align:right;" MaxLength="3" ></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator24" ControlToValidate="txtCurrentStrength" ErrorMessage="Current Strength is required." Text="*"></asp:RequiredFieldValidator>
                                        <span class="unit">KTS</span>  
                                    </td>
                                </tr>
                             </table> 
                        </td>
                        <td valign="top" >
                            <table width="100%">
                                <tr>
                                    <td>
                                        Remarks : ( If Any )
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtWeatherRemarks" TextMode="MultiLine" style="; text-transform:uppercase;width:75%; height:123px;" MaxLength="11" ></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
</table>
<div class="div1">Ship Position :</div>
<table cellpadding="4" cellspacing="0" border="0" width="100%" >
        <tr>
            <td style="background-color:#FFFFFF; border:solid 1px #006B8F;">
                    <table cellpadding="0" cellspacing="3" border="0" width="100%" >
                        <tr>
                            <td style="width:180px">Lattitude :</td>
                            <td style="width:250px">
                                <asp:DropDownList runat="server" ID="ddlLattitude1" Width="70px">
                                        <asp:ListItem Value="" Text=""></asp:ListItem>
                                        <asp:ListItem Value="0" Text="00"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="01"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="02"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="03"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="04"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="05"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="06"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="07"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="08"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="09"></asp:ListItem>
                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                        <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                        <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                        <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                        <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                        <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                        <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                        <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                        <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                        <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                        <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                        <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                        <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                        <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                        <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                        <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                        <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                        <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                        <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                        <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                        <asp:ListItem Value="32" Text="32"></asp:ListItem>
                                        <asp:ListItem Value="33" Text="33"></asp:ListItem>
                                        <asp:ListItem Value="34" Text="34"></asp:ListItem>
                                        <asp:ListItem Value="35" Text="35"></asp:ListItem>
                                        <asp:ListItem Value="36" Text="36"></asp:ListItem>
                                        <asp:ListItem Value="37" Text="37"></asp:ListItem>
                                        <asp:ListItem Value="38" Text="38"></asp:ListItem>
                                        <asp:ListItem Value="39" Text="39"></asp:ListItem>
                                        <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                        <asp:ListItem Value="41" Text="41"></asp:ListItem>
                                        <asp:ListItem Value="42" Text="42"></asp:ListItem>
                                        <asp:ListItem Value="43" Text="43"></asp:ListItem>
                                        <asp:ListItem Value="44" Text="44"></asp:ListItem>
                                        <asp:ListItem Value="45" Text="45"></asp:ListItem>
                                        <asp:ListItem Value="46" Text="46"></asp:ListItem>
                                        <asp:ListItem Value="47" Text="47"></asp:ListItem>
                                        <asp:ListItem Value="48" Text="48"></asp:ListItem>
                                        <asp:ListItem Value="49" Text="49"></asp:ListItem>
                                        <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                        <asp:ListItem Value="51" Text="51"></asp:ListItem>
                                        <asp:ListItem Value="52" Text="52"></asp:ListItem>
                                        <asp:ListItem Value="53" Text="53"></asp:ListItem>
                                        <asp:ListItem Value="54" Text="54"></asp:ListItem>
                                        <asp:ListItem Value="55" Text="55"></asp:ListItem>
                                        <asp:ListItem Value="56" Text="56"></asp:ListItem>
                                        <asp:ListItem Value="57" Text="57"></asp:ListItem>
                                        <asp:ListItem Value="58" Text="58"></asp:ListItem>
                                        <asp:ListItem Value="59" Text="59"></asp:ListItem>
                                        <asp:ListItem Value="60" Text="60"></asp:ListItem>
                                        <asp:ListItem Value="61" Text="61"></asp:ListItem>
                                        <asp:ListItem Value="62" Text="62"></asp:ListItem>
                                        <asp:ListItem Value="63" Text="63"></asp:ListItem>
                                        <asp:ListItem Value="64" Text="64"></asp:ListItem>
                                        <asp:ListItem Value="65" Text="65"></asp:ListItem>
                                        <asp:ListItem Value="66" Text="66"></asp:ListItem>
                                        <asp:ListItem Value="67" Text="67"></asp:ListItem>
                                        <asp:ListItem Value="68" Text="68"></asp:ListItem>
                                        <asp:ListItem Value="69" Text="69"></asp:ListItem>
                                        <asp:ListItem Value="70" Text="70"></asp:ListItem>
                                        <asp:ListItem Value="71" Text="71"></asp:ListItem>
                                        <asp:ListItem Value="72" Text="72"></asp:ListItem>
                                        <asp:ListItem Value="73" Text="73"></asp:ListItem>
                                        <asp:ListItem Value="74" Text="74"></asp:ListItem>
                                        <asp:ListItem Value="75" Text="75"></asp:ListItem>
                                        <asp:ListItem Value="76" Text="76"></asp:ListItem>
                                        <asp:ListItem Value="77" Text="77"></asp:ListItem>
                                        <asp:ListItem Value="78" Text="78"></asp:ListItem>
                                        <asp:ListItem Value="79" Text="79"></asp:ListItem>
                                        <asp:ListItem Value="80" Text="80"></asp:ListItem>
                                        <asp:ListItem Value="81" Text="81"></asp:ListItem>
                                        <asp:ListItem Value="82" Text="82"></asp:ListItem>
                                        <asp:ListItem Value="83" Text="83"></asp:ListItem>
                                        <asp:ListItem Value="84" Text="84"></asp:ListItem>
                                        <asp:ListItem Value="85" Text="85"></asp:ListItem>
                                        <asp:ListItem Value="86" Text="86"></asp:ListItem>
                                        <asp:ListItem Value="87" Text="87"></asp:ListItem>
                                        <asp:ListItem Value="88" Text="88"></asp:ListItem>
                                        <asp:ListItem Value="89" Text="89"></asp:ListItem>
                                        <asp:ListItem Value="90" Text="90"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList runat="server" ID="ddlLattitude2" Width="70px">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="0" Text="00"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="01"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="02"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="03"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="04"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="05"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="06"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="07"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="08"></asp:ListItem>
                                    <asp:ListItem Value="9" Text="09"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                    <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                    <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                    <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                    <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                    <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                    <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                    <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                    <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                    <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                    <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                    <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                    <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                    <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                    <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                    <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                    <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                    <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                    <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                    <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                    <asp:ListItem Value="32" Text="32"></asp:ListItem>
                                    <asp:ListItem Value="33" Text="33"></asp:ListItem>
                                    <asp:ListItem Value="34" Text="34"></asp:ListItem>
                                    <asp:ListItem Value="35" Text="35"></asp:ListItem>
                                    <asp:ListItem Value="36" Text="36"></asp:ListItem>
                                    <asp:ListItem Value="37" Text="37"></asp:ListItem>
                                    <asp:ListItem Value="38" Text="38"></asp:ListItem>
                                    <asp:ListItem Value="39" Text="39"></asp:ListItem>
                                    <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                    <asp:ListItem Value="41" Text="41"></asp:ListItem>
                                    <asp:ListItem Value="42" Text="42"></asp:ListItem>
                                    <asp:ListItem Value="43" Text="43"></asp:ListItem>
                                    <asp:ListItem Value="44" Text="44"></asp:ListItem>
                                    <asp:ListItem Value="45" Text="45"></asp:ListItem>
                                    <asp:ListItem Value="46" Text="46"></asp:ListItem>
                                    <asp:ListItem Value="47" Text="47"></asp:ListItem>
                                    <asp:ListItem Value="48" Text="48"></asp:ListItem>
                                    <asp:ListItem Value="49" Text="49"></asp:ListItem>
                                    <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                    <asp:ListItem Value="51" Text="51"></asp:ListItem>
                                    <asp:ListItem Value="52" Text="52"></asp:ListItem>
                                    <asp:ListItem Value="53" Text="53"></asp:ListItem>
                                    <asp:ListItem Value="54" Text="54"></asp:ListItem>
                                    <asp:ListItem Value="55" Text="55"></asp:ListItem>
                                    <asp:ListItem Value="56" Text="56"></asp:ListItem>
                                    <asp:ListItem Value="57" Text="57"></asp:ListItem>
                                    <asp:ListItem Value="58" Text="58"></asp:ListItem>
                                    <asp:ListItem Value="59" Text="59"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList runat="server" ID="ddlLattitude3" Width="70px">
                                  <asp:ListItem Value="" Text=""></asp:ListItem>
                                  <asp:ListItem Value="N" Text="N"></asp:ListItem>
                                  <asp:ListItem Value="S" Text="S"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="text-align:right; width:250px">Longitude :</td>
                            <td>
                                <asp:DropDownList runat="server" ID="ddlLongitude1" Width="70px">
                                <asp:ListItem Value="" Text=""></asp:ListItem>
                                <asp:ListItem Value="0" Text="000"></asp:ListItem>
                                <asp:ListItem Value="1" Text="001"></asp:ListItem>
                                <asp:ListItem Value="2" Text="002"></asp:ListItem>
                                <asp:ListItem Value="3" Text="003"></asp:ListItem>
                                <asp:ListItem Value="4" Text="004"></asp:ListItem>
                                <asp:ListItem Value="5" Text="005"></asp:ListItem>
                                <asp:ListItem Value="6" Text="006"></asp:ListItem>
                                <asp:ListItem Value="7" Text="007"></asp:ListItem>
                                <asp:ListItem Value="8" Text="008"></asp:ListItem>
                                <asp:ListItem Value="9" Text="009"></asp:ListItem>
                                <asp:ListItem Value="10" Text="010"></asp:ListItem>
                                <asp:ListItem Value="11" Text="011"></asp:ListItem>
                                <asp:ListItem Value="12" Text="012"></asp:ListItem>
                                <asp:ListItem Value="13" Text="013"></asp:ListItem>
                                <asp:ListItem Value="14" Text="014"></asp:ListItem>
                                <asp:ListItem Value="15" Text="015"></asp:ListItem>
                                <asp:ListItem Value="16" Text="016"></asp:ListItem>
                                <asp:ListItem Value="17" Text="017"></asp:ListItem>
                                <asp:ListItem Value="18" Text="018"></asp:ListItem>
                                <asp:ListItem Value="19" Text="019"></asp:ListItem>
                                <asp:ListItem Value="20" Text="020"></asp:ListItem>
                                <asp:ListItem Value="21" Text="021"></asp:ListItem>
                                <asp:ListItem Value="22" Text="022"></asp:ListItem>
                                <asp:ListItem Value="23" Text="023"></asp:ListItem>
                                <asp:ListItem Value="24" Text="024"></asp:ListItem>
                                <asp:ListItem Value="25" Text="025"></asp:ListItem>
                                <asp:ListItem Value="26" Text="026"></asp:ListItem>
                                <asp:ListItem Value="27" Text="027"></asp:ListItem>
                                <asp:ListItem Value="28" Text="028"></asp:ListItem>
                                <asp:ListItem Value="29" Text="029"></asp:ListItem>
                                <asp:ListItem Value="30" Text="030"></asp:ListItem>
                                <asp:ListItem Value="31" Text="031"></asp:ListItem>
                                <asp:ListItem Value="32" Text="032"></asp:ListItem>
                                <asp:ListItem Value="33" Text="033"></asp:ListItem>
                                <asp:ListItem Value="34" Text="034"></asp:ListItem>
                                <asp:ListItem Value="35" Text="035"></asp:ListItem>
                                <asp:ListItem Value="36" Text="036"></asp:ListItem>
                                <asp:ListItem Value="37" Text="037"></asp:ListItem>
                                <asp:ListItem Value="38" Text="038"></asp:ListItem>
                                <asp:ListItem Value="39" Text="039"></asp:ListItem>
                                <asp:ListItem Value="40" Text="040"></asp:ListItem>
                                <asp:ListItem Value="41" Text="041"></asp:ListItem>
                                <asp:ListItem Value="42" Text="042"></asp:ListItem>
                                <asp:ListItem Value="43" Text="043"></asp:ListItem>
                                <asp:ListItem Value="44" Text="044"></asp:ListItem>
                                <asp:ListItem Value="45" Text="045"></asp:ListItem>
                                <asp:ListItem Value="46" Text="046"></asp:ListItem>
                                <asp:ListItem Value="47" Text="047"></asp:ListItem>
                                <asp:ListItem Value="48" Text="048"></asp:ListItem>
                                <asp:ListItem Value="49" Text="049"></asp:ListItem>
                                <asp:ListItem Value="50" Text="050"></asp:ListItem>
                                <asp:ListItem Value="51" Text="051"></asp:ListItem>
                                <asp:ListItem Value="52" Text="052"></asp:ListItem>
                                <asp:ListItem Value="53" Text="053"></asp:ListItem>
                                <asp:ListItem Value="54" Text="054"></asp:ListItem>
                                <asp:ListItem Value="55" Text="055"></asp:ListItem>
                                <asp:ListItem Value="56" Text="056"></asp:ListItem>
                                <asp:ListItem Value="57" Text="057"></asp:ListItem>
                                <asp:ListItem Value="58" Text="058"></asp:ListItem>
                                <asp:ListItem Value="59" Text="059"></asp:ListItem>
                                <asp:ListItem Value="60" Text="060"></asp:ListItem>
                                <asp:ListItem Value="61" Text="061"></asp:ListItem>
                                <asp:ListItem Value="62" Text="062"></asp:ListItem>
                                <asp:ListItem Value="63" Text="063"></asp:ListItem>
                                <asp:ListItem Value="64" Text="064"></asp:ListItem>
                                <asp:ListItem Value="65" Text="065"></asp:ListItem>
                                <asp:ListItem Value="66" Text="066"></asp:ListItem>
                                <asp:ListItem Value="67" Text="067"></asp:ListItem>
                                <asp:ListItem Value="68" Text="068"></asp:ListItem>
                                <asp:ListItem Value="69" Text="069"></asp:ListItem>
                                <asp:ListItem Value="70" Text="070"></asp:ListItem>
                                <asp:ListItem Value="71" Text="071"></asp:ListItem>
                                <asp:ListItem Value="72" Text="072"></asp:ListItem>
                                <asp:ListItem Value="73" Text="073"></asp:ListItem>
                                <asp:ListItem Value="74" Text="074"></asp:ListItem>
                                <asp:ListItem Value="75" Text="075"></asp:ListItem>
                                <asp:ListItem Value="76" Text="076"></asp:ListItem>
                                <asp:ListItem Value="77" Text="077"></asp:ListItem>
                                <asp:ListItem Value="78" Text="078"></asp:ListItem>
                                <asp:ListItem Value="79" Text="079"></asp:ListItem>
                                <asp:ListItem Value="80" Text="080"></asp:ListItem>
                                <asp:ListItem Value="81" Text="081"></asp:ListItem>
                                <asp:ListItem Value="82" Text="082"></asp:ListItem>
                                <asp:ListItem Value="83" Text="083"></asp:ListItem>
                                <asp:ListItem Value="84" Text="084"></asp:ListItem>
                                <asp:ListItem Value="85" Text="085"></asp:ListItem>
                                <asp:ListItem Value="86" Text="086"></asp:ListItem>
                                <asp:ListItem Value="87" Text="087"></asp:ListItem>
                                <asp:ListItem Value="88" Text="088"></asp:ListItem>
                                <asp:ListItem Value="89" Text="089"></asp:ListItem>
                                <asp:ListItem Value="90" Text="090"></asp:ListItem>
                                <asp:ListItem Value="91" Text="091"></asp:ListItem>
                                <asp:ListItem Value="92" Text="092"></asp:ListItem>
                                <asp:ListItem Value="93" Text="093"></asp:ListItem>
                                <asp:ListItem Value="94" Text="094"></asp:ListItem>
                                <asp:ListItem Value="95" Text="095"></asp:ListItem>
                                <asp:ListItem Value="96" Text="096"></asp:ListItem>
                                <asp:ListItem Value="97" Text="097"></asp:ListItem>
                                <asp:ListItem Value="98" Text="098"></asp:ListItem>
                                <asp:ListItem Value="99" Text="099"></asp:ListItem>
                                <asp:ListItem Value="100" Text="100"></asp:ListItem>
                                <asp:ListItem Value="101" Text="101"></asp:ListItem>
                                <asp:ListItem Value="102" Text="102"></asp:ListItem>
                                <asp:ListItem Value="103" Text="103"></asp:ListItem>
                                <asp:ListItem Value="104" Text="104"></asp:ListItem>
                                <asp:ListItem Value="105" Text="105"></asp:ListItem>
                                <asp:ListItem Value="106" Text="106"></asp:ListItem>
                                <asp:ListItem Value="107" Text="107"></asp:ListItem>
                                <asp:ListItem Value="108" Text="108"></asp:ListItem>
                                <asp:ListItem Value="109" Text="109"></asp:ListItem>
                                <asp:ListItem Value="110" Text="110"></asp:ListItem>
                                <asp:ListItem Value="111" Text="111"></asp:ListItem>
                                <asp:ListItem Value="112" Text="112"></asp:ListItem>
                                <asp:ListItem Value="113" Text="113"></asp:ListItem>
                                <asp:ListItem Value="114" Text="114"></asp:ListItem>
                                <asp:ListItem Value="115" Text="115"></asp:ListItem>
                                <asp:ListItem Value="116" Text="116"></asp:ListItem>
                                <asp:ListItem Value="117" Text="117"></asp:ListItem>
                                <asp:ListItem Value="118" Text="118"></asp:ListItem>
                                <asp:ListItem Value="119" Text="119"></asp:ListItem>
                                <asp:ListItem Value="120" Text="120"></asp:ListItem>
                                <asp:ListItem Value="121" Text="121"></asp:ListItem>
                                <asp:ListItem Value="122" Text="122"></asp:ListItem>
                                <asp:ListItem Value="123" Text="123"></asp:ListItem>
                                <asp:ListItem Value="124" Text="124"></asp:ListItem>
                                <asp:ListItem Value="125" Text="125"></asp:ListItem>
                                <asp:ListItem Value="126" Text="126"></asp:ListItem>
                                <asp:ListItem Value="127" Text="127"></asp:ListItem>
                                <asp:ListItem Value="128" Text="128"></asp:ListItem>
                                <asp:ListItem Value="129" Text="129"></asp:ListItem>
                                <asp:ListItem Value="130" Text="130"></asp:ListItem>
                                <asp:ListItem Value="131" Text="131"></asp:ListItem>
                                <asp:ListItem Value="132" Text="132"></asp:ListItem>
                                <asp:ListItem Value="133" Text="133"></asp:ListItem>
                                <asp:ListItem Value="134" Text="134"></asp:ListItem>
                                <asp:ListItem Value="135" Text="135"></asp:ListItem>
                                <asp:ListItem Value="136" Text="136"></asp:ListItem>
                                <asp:ListItem Value="137" Text="137"></asp:ListItem>
                                <asp:ListItem Value="138" Text="138"></asp:ListItem>
                                <asp:ListItem Value="139" Text="139"></asp:ListItem>
                                <asp:ListItem Value="140" Text="140"></asp:ListItem>
                                <asp:ListItem Value="141" Text="141"></asp:ListItem>
                                <asp:ListItem Value="142" Text="142"></asp:ListItem>
                                <asp:ListItem Value="143" Text="143"></asp:ListItem>
                                <asp:ListItem Value="144" Text="144"></asp:ListItem>
                                <asp:ListItem Value="145" Text="145"></asp:ListItem>
                                <asp:ListItem Value="146" Text="146"></asp:ListItem>
                                <asp:ListItem Value="147" Text="147"></asp:ListItem>
                                <asp:ListItem Value="148" Text="148"></asp:ListItem>
                                <asp:ListItem Value="149" Text="149"></asp:ListItem>
                                <asp:ListItem Value="150" Text="150"></asp:ListItem>
                                <asp:ListItem Value="151" Text="151"></asp:ListItem>
                                <asp:ListItem Value="152" Text="152"></asp:ListItem>
                                <asp:ListItem Value="153" Text="153"></asp:ListItem>
                                <asp:ListItem Value="154" Text="154"></asp:ListItem>
                                <asp:ListItem Value="155" Text="155"></asp:ListItem>
                                <asp:ListItem Value="156" Text="156"></asp:ListItem>
                                <asp:ListItem Value="157" Text="157"></asp:ListItem>
                                <asp:ListItem Value="158" Text="158"></asp:ListItem>
                                <asp:ListItem Value="159" Text="159"></asp:ListItem>
                                <asp:ListItem Value="160" Text="160"></asp:ListItem>
                                <asp:ListItem Value="161" Text="161"></asp:ListItem>
                                <asp:ListItem Value="162" Text="162"></asp:ListItem>
                                <asp:ListItem Value="163" Text="163"></asp:ListItem>
                                <asp:ListItem Value="164" Text="164"></asp:ListItem>
                                <asp:ListItem Value="165" Text="165"></asp:ListItem>
                                <asp:ListItem Value="166" Text="166"></asp:ListItem>
                                <asp:ListItem Value="167" Text="167"></asp:ListItem>
                                <asp:ListItem Value="168" Text="168"></asp:ListItem>
                                <asp:ListItem Value="169" Text="169"></asp:ListItem>
                                <asp:ListItem Value="170" Text="170"></asp:ListItem>
                                <asp:ListItem Value="171" Text="171"></asp:ListItem>
                                <asp:ListItem Value="172" Text="172"></asp:ListItem>
                                <asp:ListItem Value="173" Text="173"></asp:ListItem>
                                <asp:ListItem Value="174" Text="174"></asp:ListItem>
                                <asp:ListItem Value="175" Text="175"></asp:ListItem>
                                <asp:ListItem Value="176" Text="176"></asp:ListItem>
                                <asp:ListItem Value="177" Text="177"></asp:ListItem>
                                <asp:ListItem Value="178" Text="178"></asp:ListItem>
                                <asp:ListItem Value="179" Text="179"></asp:ListItem>
                                <asp:ListItem Value="180" Text="180"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList runat="server" ID="ddlLongitude2" Width="70px">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="0" Text="00"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="01"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="02"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="03"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="04"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="05"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="06"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="07"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="08"></asp:ListItem>
                                    <asp:ListItem Value="9" Text="09"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                    <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                    <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                    <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                    <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                    <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                    <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                    <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                    <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                    <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                    <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                    <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                    <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                    <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                    <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                    <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                    <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                    <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                    <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                    <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                    <asp:ListItem Value="32" Text="32"></asp:ListItem>
                                    <asp:ListItem Value="33" Text="33"></asp:ListItem>
                                    <asp:ListItem Value="34" Text="34"></asp:ListItem>
                                    <asp:ListItem Value="35" Text="35"></asp:ListItem>
                                    <asp:ListItem Value="36" Text="36"></asp:ListItem>
                                    <asp:ListItem Value="37" Text="37"></asp:ListItem>
                                    <asp:ListItem Value="38" Text="38"></asp:ListItem>
                                    <asp:ListItem Value="39" Text="39"></asp:ListItem>
                                    <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                    <asp:ListItem Value="41" Text="41"></asp:ListItem>
                                    <asp:ListItem Value="42" Text="42"></asp:ListItem>
                                    <asp:ListItem Value="43" Text="43"></asp:ListItem>
                                    <asp:ListItem Value="44" Text="44"></asp:ListItem>
                                    <asp:ListItem Value="45" Text="45"></asp:ListItem>
                                    <asp:ListItem Value="46" Text="46"></asp:ListItem>
                                    <asp:ListItem Value="47" Text="47"></asp:ListItem>
                                    <asp:ListItem Value="48" Text="48"></asp:ListItem>
                                    <asp:ListItem Value="49" Text="49"></asp:ListItem>
                                    <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                    <asp:ListItem Value="51" Text="51"></asp:ListItem>
                                    <asp:ListItem Value="52" Text="52"></asp:ListItem>
                                    <asp:ListItem Value="53" Text="53"></asp:ListItem>
                                    <asp:ListItem Value="54" Text="54"></asp:ListItem>
                                    <asp:ListItem Value="55" Text="55"></asp:ListItem>
                                    <asp:ListItem Value="56" Text="56"></asp:ListItem>
                                    <asp:ListItem Value="57" Text="57"></asp:ListItem>
                                    <asp:ListItem Value="58" Text="58"></asp:ListItem>
                                    <asp:ListItem Value="59" Text="59"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList runat="server" ID="ddlLongitude3" Width="70px">
                                  <asp:ListItem Value="" Text=""></asp:ListItem>
                                  <asp:ListItem Value="E" Text="E"></asp:ListItem>
                                  <asp:ListItem Value="W" Text="W"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                        <td style="width:180px">Location Description :</td>
                        <td colspan="3">
                               <asp:TextBox runat="server" ID="txtLocationDescription" TextMode="MultiLine" style="; text-transform:uppercase;width:80%;height:70px;" MaxLength="11" ></asp:TextBox>
                        </td>
                        </tr>
                        </table>
            </td>
        </tr>
</table>
<div class="div1">Consumption ( Since Last Report ) :</div>
<table cellpadding="4" cellspacing="0" border="0" width="100%" >
        <tr>
            <td style="background-color:#FFFFFF; border:solid 1px #006B8F;">
            <div style="background-color:#c2c2c2; color:Black; padding:6px; text-align:left;">
                Fuel Consumption :
            </div>
                    <table cellpadding="0" cellspacing="3" border="0" width="100%" >
                    <col width="180px" />
                    <col width="210px" />
                    <col width="210px" />
                    <col width="210px" />
                    <col width="210px" />
                    <col width="210px" />
                    <col width="210px" />
                    <col width="210px" />
                        <tr>
                            <td>
                            
                            </td>
                            <td>
                                Received</td>
                            <td>
                                ME Consumption</td>
                            <td>
                                AE Consumption</td>
                            <td>
                                Boiler Consumption</td>
                            <td>
                                ROB</td>
                              <!--  <td>ROB</td>-->
                              <div runat="server" id="divHeaderROB" visible="false">
                            <td>ROB at S.B.E.</td>
                             <td>ROB at COSP</td>
                                   </div>
                             <div runat="server" id="divHeaderFWE" visible="false">
                            <td>F.W.E. (LT)</td>
                                 </div>
                        </tr>
                        <tr>
                            <td>
                                VLSFO :
                            </td>
                            <td>
                                <asp:TextBox ID="txtRobIFO45Recv" runat="server" MaxLength="16" onkeypress="FloatValueOnly(this)" /><span class="unit">MT</span>
                               </td>
                            <td>
                               <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtME_IFO45" onkeyup="TotalConsumptionForIFO45()" style="width:70px;text-align:right;" MaxLength="16" ></asp:TextBox>
                               <span class="unit"> MT</span>
                               </td>
                            <td>
                               <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtAE_IFO45" onkeyup="TotalConsumptionForIFO45()" style="width:70px;text-align:right;" MaxLength="16" ></asp:TextBox>
                               <span class="unit"> MT</span> </td>
                            <td>
                               <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtCargoHeating_IFO45" onkeyup="TotalConsumptionForIFO45()" style="width:70px;text-align:right;" MaxLength="16" ></asp:TextBox>
                               <span class="unit"> MT</span>
                            </td>
                            <td>
                                <asp:HiddenField ID="hfdRobIFO45" runat="server" />
                                <asp:TextBox ID="txtRobIFO45" runat="server" MaxLength="16" 
                                    onkeypress="FloatValueOnly(this)" ReadOnly="true" />
                                <asp:HiddenField ID="hfdRobIFO45_S" runat="server" />
                                <span class="unit">MT</span></td>
                        <!--    <td>
                                <input  id="txtRobIFO45" type="text" onkeypress="FloatValueOnly(this)" maxlength="16" style="text-align:left;"/><span class="unit"> MT</span>
                           </td>-->
                               <div id="divVLSFO_ROB" runat="server" visible="false">
                            <td>
    <asp:HiddenField ID="hfdVLSFO_ROBSBE" runat="server" />
    <asp:TextBox ID="txtVLSFO_ROBSBE" runat="server" MaxLength="16" style="width:70px;text-align:right;"
        onkeypress="FloatValueOnly(this)"  />
    <asp:HiddenField ID="hfdVLSFO_ROBSBE_s" runat="server" />
    <span class="unit">MT</span></td>
                                                    <td>
<asp:HiddenField ID="hfdVLSFO_ROBCOSP" runat="server" />
<asp:TextBox ID="txtVLSFO_ROBCOSP" runat="server" MaxLength="16" style="width:70px;text-align:right;"
    onkeypress="FloatValueOnly(this)"  />
<asp:HiddenField ID="hfdVLSFO_ROBCOSP_s" runat="server" />
<span class="unit">MT</span></td>
<td></div>
                            <div id="divVLSFO_FWE" runat="server" visible="false">
                            <td>
   
    <asp:TextBox ID="txtVLSFO_FWE" runat="server" MaxLength="16" style="width:70px;text-align:right;"
        onkeypress="FloatValueOnly(this)"  />
    <span class="unit">MT</span></td>
                                                   </div>
                        </tr>
                        <tr style="display:none;">
                            <td>
                                VLSFO :
                            </td>
                            <td>
                                <asp:TextBox ID="txtRobIFO1Recv" runat="server" MaxLength="16" 
                                    onkeypress="FloatValueOnly(this)" />
                                <span class="unit">MT</span></td>
                            <td>
                                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtME_IFO1" onkeyup="TotalConsumptionForIFO1()" style="width:70px;text-align:right;" MaxLength="16" ></asp:TextBox>
                                <span class="unit"> MT</span></td>
                            <td>
                                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtAE_IFO1" onkeyup="TotalConsumptionForIFO1()" style="width:70px;text-align:right;" MaxLength="16" ></asp:TextBox>
                                <span class="unit"> MT</span> </td>
                            <td>
                                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtCargoHeating_IFO1" onkeyup="TotalConsumptionForIFO1()" style="width:70px;text-align:right;" MaxLength="16" ></asp:TextBox>
                                <span class="unit"> MT</span></td>
                            <td>
                                <asp:HiddenField ID="hfdRobIFO1" runat="server" />
                                <asp:TextBox ID="txtRobIFO1" runat="server" MaxLength="16" 
                                    onkeypress="FloatValueOnly(this)" ReadOnly="true" />
                                <asp:HiddenField ID="hfdRobIFO1_S" runat="server" />
                                <span class="unit">MT</span></td>
                            <td>
                                <!--<input  id="txtRobIFO1" type="text" onkeypress="FloatValueOnly(this)" maxlength="16" style="text-align:left;"/>
                                <span class="unit"> MT</span>-->
                            </td>
                            <td></td>
                        </tr>
                        <tr style="display:none;">
                            <td>
                                LSMGO :
                            </td>
                            <td>
                                <asp:TextBox ID="txtRobMGO5Recv" runat="server" MaxLength="16" 
                                    onkeypress="FloatValueOnly(this)" />
                                <span class="unit">MT</span></td>
                            <td>
                                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtME_MGO5" onkeyup="TotalConsumptionForMGO5()" style="width:70px;text-align:right;" MaxLength="16" ></asp:TextBox>
                                <span class="unit"> MT</span> </td>
                            <td>
                                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtAE_MGO5" onkeyup="TotalConsumptionForMGO5()" style="width:70px;text-align:right;" MaxLength="16" ></asp:TextBox>
                                <span class="unit"> MT</span> </td>
                            <td>
                                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtCargoHeating_MGO5" onkeyup="TotalConsumptionForMGO5()" style="width:70px;text-align:right;" MaxLength="16" ></asp:TextBox>
                                <span class="unit"> MT</span>
                             </td>
                            <td>
                                <asp:HiddenField ID="hfdRobMGO5" runat="server" />
                                <asp:TextBox ID="txtRobMGO5" runat="server" MaxLength="16" 
                                    onkeypress="FloatValueOnly(this)" ReadOnly="true" />
                                <asp:HiddenField ID="hfdRobMGO5_S" runat="server" />
                                <span class="unit">MT</span></td>
                            <td>
                                <!--<input  id="txtRobMGO5" type="text" onkeypress="FloatValueOnly(this)" maxlength="16" style="text-align:left;"/>
                                <span class="unit"> MT</span>-->
                            </td>
                             <td></td>
                        </tr>
                        <tr>
                            <td>
                                LSMGO :
                            </td>
                            <td>
                                <asp:TextBox ID="txtRobMGO1Recv" runat="server" MaxLength="16" 
                                    onkeypress="FloatValueOnly(this)" />
                                <span class="unit">MT</span></td>
                            <td>
                                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtME_MGO10" onkeyup="TotalConsumptionForMGO1()" style="width:70px;text-align:right;" MaxLength="16" ></asp:TextBox>
                                <span class="unit"> MT</span> </td>
                            <td>
                                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtAE_MGO1" onkeyup="TotalConsumptionForMGO1()" style="width:70px;text-align:right;" MaxLength="16" ></asp:TextBox>
                                <span class="unit"> MT</span> </td>
                            <td>
                                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtCargoHeating_MGO1" onkeyup="TotalConsumptionForMGO1()" style="width:70px;text-align:right;" MaxLength="16" ></asp:TextBox>
                                <span class="unit"> MT</span>
                            </td>
                            <td>
                                <asp:HiddenField ID="hfdRobMGO1" runat="server" />
                                <asp:TextBox ID="txtRobMGO1" runat="server" MaxLength="16" 
                                    onkeypress="FloatValueOnly(this)" ReadOnly="true" />
                                <asp:HiddenField ID="hfdRobMGO1_S" runat="server" />
                                <span class="unit">MT</span></td>
                           <!-- <td>
                                <input  id="txtRobMGO1" type="text" onkeypress="FloatValueOnly(this)" maxlength="16" style="text-align:left;"/>
                                <span class="unit"> MT</span>
                            </td>-->
                            <div id="divLSMGO_ROB" runat="server" visible="false"> <td>
    <asp:HiddenField ID="hfdLSMGO_ROBSBE" runat="server" />
    <asp:TextBox ID="txtLSMGO_ROBSBE" runat="server" MaxLength="16" style="width:70px;text-align:right;" 
        onkeypress="FloatValueOnly(this)"  />
    <asp:HiddenField ID="hfdLSMGO_ROBSBE_S" runat="server" />
    <span class="unit">MT</span>
                            </td>
                                                    <td>
<asp:HiddenField ID="hfdLSMGO_ROBCOSP" runat="server" />
<asp:TextBox ID="txtLSMGO_ROBCOSP" runat="server" MaxLength="16" style="width:70px;text-align:right;" 
    onkeypress="FloatValueOnly(this)"  />
<asp:HiddenField ID="hfdLSMGO_ROBCOSP_S" runat="server" />
<span class="unit">MT</span>
                        </td>
 </div>
                            <div id="divLSMGO_FWE" runat="server" visible="false">
                                <td>
   
    <asp:TextBox ID="txtLSMGO_FWE" runat="server" MaxLength="16" style="width:70px;text-align:right;" 
        onkeypress="FloatValueOnly(this)"  />
    
    <span class="unit">MT</span>
                            </td>
                            </div>
                        </tr>
                        <tr>
                            <td>
                                MDO :
                            </td>
                            <td>
                                <asp:TextBox ID="txtRobMDORecv" runat="server" MaxLength="16" 
                                    onkeypress="FloatValueOnly(this)" />
                                <span class="unit">MT</span></td>
                            <td>
                                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtME_MDO" onkeyup="TotalConsumptionForMDO()" style="width:70px;text-align:right;" MaxLength="16" ></asp:TextBox>
                                <span class="unit"> MT</span> </td>
                            <td>
                                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtAE_MDO" onkeyup="TotalConsumptionForMDO()" style="width:70px;text-align:right;" MaxLength="16" ></asp:TextBox>
                                <span class="unit"> MT</span> </td>
                            <td>
                                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtCargoHeating_MDO" onkeyup="TotalConsumptionForMDO()" style="width:70px;text-align:right;" MaxLength="16" ></asp:TextBox>
                                <span class="unit"> MT</span>
                            </td>
                            <td>
                                <asp:HiddenField ID="hfdRobMDO" runat="server" />
                                <asp:TextBox ID="txtRobMDO" runat="server" MaxLength="16" 
                                    onkeypress="FloatValueOnly(this)" ReadOnly="true" />
                                <asp:HiddenField ID="hfdRobMDO_S" runat="server" />
                                <span class="unit">MT</span></td>
                        <!--    <td>
                                <input  id="txtRobMDOF" type="text" onkeypress="FloatValueOnly(this)" maxlength="16" style="text-align:left;"/>
                                <span class="unit"> MT</span>
                            </td>-->
                            <div id="divMDO_ROB" runat="server" visible="false">  <td>
<asp:HiddenField ID="hfdMDO_ROBSBE" runat="server" />
<asp:TextBox ID="txtMDO_ROBSBE" runat="server" MaxLength="16" style="width:70px;text-align:right;"
    onkeypress="FloatValueOnly(this)"  />
<asp:HiddenField ID="hfdMDO_ROBSBE_S" runat="server" />
<span class="unit">MT</span>
                        </td>
                                                                                <td>
<asp:HiddenField ID="hfdMDO_ROBCOSP" runat="server" />
<asp:TextBox ID="txtMDO_ROBCOSP" runat="server" MaxLength="16" style="width:70px;text-align:right;"
    onkeypress="FloatValueOnly(this)"  />
<asp:HiddenField ID="hfdMDO_ROBCOSP_S" runat="server" />
<span class="unit">MT</span>
                        </td></div>
                            <div id="divMDO_FWE" runat="server" visible="false">  <td>
                               
<asp:TextBox ID="txtMDO_FWE" runat="server" MaxLength="16" style="width:70px;text-align:right;"
    onkeypress="FloatValueOnly(this)"  />
<span class="unit">MT</span>
                        </td></div>
                        </tr>
                        <%--<tr>
                            <td>Displacement :</td>
                            <td colspan="5">
                                <asp:DropDownList runat="server" ID="ddlDisplacement" Width="57px">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="0" Text="0"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                    </asp:DropDownList>
                            </td>
                        </tr>--%>
                    </table>
            </td>
        </tr>
       <tr>
     <td style="background-color:#FFFFFF; border:solid 1px #006B8F;">
     <div style="background-color:#c2c2c2; color:Black; padding:6px; text-align:left;">
        Flowmeter Readings :
     </div>
          <table cellpadding="0" cellspacing="3" border="0" width="100%" >
 <col width="180px" />
 <col width="210px" />
 <col width="210px" />
 <col width="210px" />
 <col width="210px" />
 <col width="210px" />
     <tr>
         <td>
         
         </td>
         <td>
             ME</td>
         <td>
             AE</td>
         <td>
            Boiler</td>
         <td>
            &nbsp;</td>
         <td>
             &nbsp;</td>
             <td><!--ROB--></td>
         <td>&nbsp;</td>
     </tr>
              <tr><td>At COSP</td>
                  <td><asp:TextBox ID="txtMEFlowmeterReadingatCOSP" onkeypress="FloatValueOnly(this);" runat="server" MaxLength="16"  Width="90px" /></td>
                  <td><asp:TextBox ID="txtAEFlowmeterReadingatCOSP" onkeypress="FloatValueOnly(this);" runat="server" MaxLength="16"  Width="90px" /></td>
                  <td><asp:TextBox ID="txtBoilerFlowmeterReadingatCOSP" onkeypress="FloatValueOnly(this);" runat="server" MaxLength="16"  Width="90px" /></td>
                  <td></td><td></td><td></td></tr>
              <tr><td>At S.B.E</td>
                  <td><asp:TextBox ID="txtMEFlowmeterReadingatSBE" onkeypress="FloatValueOnly(this);" runat="server" MaxLength="16"  Width="90px" /></td>
                  <td><asp:TextBox ID="txtAEFlowmeterReadingatSBE" onkeypress="FloatValueOnly(this);" runat="server" MaxLength="16"  Width="90px" /></td>
                  <td><asp:TextBox ID="txtBoilerFlowmeterReadingatSBE" onkeypress="FloatValueOnly(this);" runat="server" MaxLength="16"  Width="90px" /></td><td></td><td></td><td></td></tr>
              <div runat="server" visible="false" id="divFlowmeterFWE">
               <tr><td>At F.W.E</td>
                  <td><asp:TextBox ID="txtMEFlowmeterReadingatFWE" onkeypress="FloatValueOnly(this);" runat="server" MaxLength="16"  Width="90px" /></td>
                  <td><asp:TextBox ID="txtAEFlowmeterReadingatFWE" onkeypress="FloatValueOnly(this);" runat="server" MaxLength="16"  Width="90px" /></td>
                  <td><asp:TextBox ID="txtBoilerFlowmeterReadingatFWE" onkeypress="FloatValueOnly(this);" runat="server" MaxLength="16"  Width="90px" /></td><td></td><td></td><td></td></tr>
                   </div>
          </table>
         </td></tr>
    <tr><td style="background-color:#FFFFFF; border:solid 1px #006B8F;">
         
<div style="background-color:#c2c2c2; color:Black; padding:6px; text-align:left;">
   Tank Information :
</div>
                 <table cellpadding="0" cellspacing="3" border="0" width="100%" >
<col width="180px" />
<col width="210px" />
<col width="210px" />
<col width="210px" />
<col width="210px" />
<col width="210px" />
    <tr>
        <td>
        
        </td>
        <td>
            VLSFO</td>
        <td>
            LSMGO</td>
        <td>
           MDO</td>
        <td>
           &nbsp;</td>
        <td>
            &nbsp;</td>
            <td><!--ROB--></td>
        <td>&nbsp;</td>
    </tr>
                     <tr><td>Tank in Use</td>
    <td><asp:TextBox ID="txtVLSFOTankInUse"  runat="server" MaxLength="16"  Width="90px" /></td>
    <td><asp:TextBox ID="txtLSMGOTankInUse"  runat="server" MaxLength="16"  Width="90px" /></td>
    <td><asp:TextBox ID="txtMDOTankInUse"  runat="server" MaxLength="16"  Width="90px" /></td>
    <td></td><td></td><td></td></tr>
<tr><td>Tank Density</td>
    <td><asp:TextBox ID="txtVLSFOTankDensity" onkeypress="FloatValueOnly(this);" runat="server" MaxLength="16"  Width="90px" /></td>
    <td><asp:TextBox ID="txtLSMGOTankDensity" onkeypress="FloatValueOnly(this);" runat="server" MaxLength="16"  Width="90px" /></td>
    <td><asp:TextBox ID="txtMDOTankDensity" onkeypress="FloatValueOnly(this);" runat="server" MaxLength="16"  Width="90px" /></td><td></td><td></td><td></td></tr>
                     <div runat="server" visible="false" id="divtankinfo"> 
                     <tr><td>F.W.E.</td>
    <td><asp:TextBox ID="txtVLSFOFWE" onkeypress="FloatValueOnly(this);" runat="server" MaxLength="16"  Width="90px" /></td>
    <td><asp:TextBox ID="txtLSMGOFWE" onkeypress="FloatValueOnly(this);" runat="server" MaxLength="16"  Width="90px" /></td>
    <td><asp:TextBox ID="txtMDOFWE" onkeypress="FloatValueOnly(this);" runat="server" MaxLength="16"  Width="90px" /></td><td></td><td></td><td></td></tr>
                    
        </div>
                     </table>
            
        </td>
        </tr>
      <tr><td style="background-color:#FFFFFF; border:solid 1px #006B8F;">
<div style="background-color:#c2c2c2; color:Black; padding:6px; text-align:left;">
    Purifier RH (Since Last Report) :
</div>
                 <table cellpadding="0" cellspacing="3" border="0" width="100%" >
<col width="180px" />
<col width="210px" />
<col width="210px" />
<col width="210px" />
<col width="210px" />
<col width="210px" />
    <tr>
        <td>
        HFO Purifier No.1
        </td>
        <td>
        HFO Purifier No.2</td>
        <td>
         ME LO Purifier </td>
        <td>
         AE Purifier</td>
        <td>
           &nbsp;</td>
        <td>
            &nbsp;</td>
            <td><!--ROB--></td>
        <td>&nbsp;</td>
    </tr>
                     <tr><td><asp:TextBox ID="txtHFOPRH1"  runat="server" MaxLength="8" onkeypress="IntValueOnly(event);"  Width="90px" Text="0" /></td>
    <td><asp:TextBox ID="txtHFOPRH2"  runat="server" MaxLength="8" onkeypress="IntValueOnly(event);" Width="90px" Text="0"/></td>
    <td><asp:TextBox ID="txtMELOPRH" onkeypress="IntValueOnly(event);" runat="server" MaxLength="8"  Width="90px" Text="0"/></td>
    <td><asp:TextBox ID="txtAEPRH" onkeypress="IntValueOnly(event);" runat="server" MaxLength="8"  Width="90px" Text="0"/></td>
    <td></td><td></td><td></td></tr>
<tr><td></td>
    <td></td>
    <td></td>
    <td></td><td></td><td></td><td></td></tr>
                     
             </table>
        </td>
        </tr>
        <tr>
         <td style="background-color:#FFFFFF; border:solid 1px #006B8F;">
            <div style="background-color:#c2c2c2; color:Black; padding:6px; text-align:left;">
                LUBE Consumption :
            </div>
                    <table cellpadding="0" cellspacing="3" border="0" width="100%" >
                                <col width="180px"/>
                                <col width="210px" />
                                <col width="210px" />
                                <col width="210px" />
                                <col  />
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>Received</td>
                                        <td>Consmuption</td>
                                        <td>ROB</td>
                                        <div runat="server" visible="false" id="divLCFWE"> 
                                        <td>F.W.E.</td>
                                             </div>
                                    </tr>
                                    <tr>
                                        <td>MECC :</td>
                                        <td>
                                            <asp:TextBox ID="txtRobMECCRecv" runat="server" MaxLength="16" 
                                                onkeypress="FloatValueOnly(this)" type="text" />
                                            <span class="unit">LTR</span></td>
                                        <td>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtLubeFresh_MECC" MaxLength="16" style="text-align:right;"></asp:TextBox>
                                            <span class="unit"> LTR</span>
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="hfdRobMECC" runat="server" />
                                            <asp:TextBox ID="txtRobMECC" runat="server" MaxLength="16" 
                                                onkeypress="FloatValueOnly(this)" ReadOnly="true" />
                                            <asp:HiddenField ID="hfdRobMECC_S" runat="server" />
                                            <span class="unit">LTR</span></td>
                                        <div runat="server" visible="false" id="divMECCFWE"> 
                                             <td>
                                             <asp:HiddenField ID="hdnMECCFWE" runat="server" />
                                            <asp:TextBox ID="txtMECCFWE" runat="server" MaxLength="16" 
                                                onkeypress="FloatValueOnly(this)" ReadOnly="true" />
                                             <asp:HiddenField ID="hdnMECCFWE_S" runat="server" />
                                            <span class="unit">LTR</span></td>
                                        </div>
                                    </tr>
                                    <tr>
                                        <td>MECYL (Low BN) :</td>
                                        <td>
                                            <asp:TextBox ID="txtRobMECYLRecv" runat="server" MaxLength="16" 
                                                onkeypress="FloatValueOnly(this)" type="text" />
                                            <span class="unit">LTR</span></td>
                                        <td>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtLubeFresh_MECYL" MaxLength="16" style="text-align:right;"></asp:TextBox>
                                            <span class="unit"> LTR</span>
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="hfdRobMECYL" runat="server" />
                                            <asp:TextBox ID="txtRobMECYL" runat="server" MaxLength="16" 
                                                onkeypress="FloatValueOnly(this)" ReadOnly="true" type="text" />
                                            <asp:HiddenField ID="hfdRobMECYL_S" runat="server" />
                                            <span class="unit">LTR</span></td>
                                        <div runat="server" visible="false" id="divMECYLFWE"> 
                                        <td>
                                             <asp:HiddenField ID="hfdMECYLFWE" runat="server" />
                                             <asp:TextBox ID="txtMECYLFWE" runat="server" MaxLength="16" onkeypress="FloatValueOnly(this)" ReadOnly="true" type="text" />
                                            <asp:HiddenField ID="hfdMECYLFWE_S" runat="server" />
                                             <span class="unit">LTR</span>
                                        </td>
                                             </div>
                                    </tr>

                                    <%--//-----------------------------------------------%>
                                    <tr>
                                         <td>MECYL (High BN):</td>
                                        <td>
                                            <asp:TextBox ID="txtRobMECYLRecvHighBN" runat="server" MaxLength="16" onkeypress="FloatValueOnly(this)" type="text" />
                                            <span class="unit">LTR</span></td>
                                        <td>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtLubeFresh_MECYLHighBN" MaxLength="16" style="text-align:right;"></asp:TextBox>
                                            <span class="unit"> LTR</span>
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="hfdRobMECYLHighBN" runat="server" />
                                            <asp:TextBox ID="txtRobMECYLHighBN" runat="server" MaxLength="16" onkeypress="FloatValueOnly(this)" ReadOnly="true" type="text" />
                                            <asp:HiddenField ID="hfdRobMECYL_SHighBN" runat="server" />
                                            <span class="unit">LTR</span></td>
                                        <div runat="server" visible="false" id="divMECYLHighBNFWE"> 
                                        <td>
                                             <asp:HiddenField ID="hfdMECYLHighBNFWE" runat="server" />
                                             <asp:TextBox ID="txtMECYLHighBNFWE" runat="server" MaxLength="16" onkeypress="FloatValueOnly(this)" ReadOnly="true" type="text" />
                                            <asp:HiddenField ID="hfdMECYLHighBNFWE_S" runat="server" />
                                             <span class="unit">LTR</span>
                                        </td>
                                             </div>
                                    </tr>
                                    <%--//-----------------------------------------------%>
                                    <tr>
                                        <td>AECC :</td>
                                        <td>
                                            <asp:TextBox ID="txtRobAECCRecv" runat="server" MaxLength="16" 
                                                onkeypress="FloatValueOnly(this)" type="text" />
                                            <span class="unit">LTR</span></td>
                                        <td>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtLubeFresh_AECC" MaxLength="16" style="text-align:right;"></asp:TextBox>
                                            <span class="unit"> LTR</span>
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="hfdRobAECC" runat="server" />
                                            <asp:TextBox ID="txtRobAECC" runat="server" MaxLength="16" 
                                                onkeypress="FloatValueOnly(this)" ReadOnly="true" type="text" />
                                            <asp:HiddenField ID="hfdRobAECC_S" runat="server" />
                                            <span class="unit">LTR</span></td>
                                            <div runat="server" visible="false" id="divAECCFWE"> 
                                         <td>
                                             <asp:HiddenField ID="hfdAECCFWE" runat="server" />
                                             <asp:TextBox ID="txtAECCFWE" runat="server" MaxLength="16" onkeypress="FloatValueOnly(this)" ReadOnly="true" type="text" />
                                              <asp:HiddenField ID="hfdAECCFWE_S" runat="server" />
                                             <span class="unit">LTR</span>
                                        </td>
                                            </div>
                                    </tr>
                                    <tr>
                                        <td>HYD :</td>
                                        <td>
                                            <asp:TextBox ID="txtRobHYDRecv" runat="server" MaxLength="16" 
                                                onkeypress="FloatValueOnly(this)" type="text" />
                                            <span class="unit">LTR</span></td>
                                        <td>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" 
                                                ID="txtLubeFresh_HYD" MaxLength="16" style="text-align:right;" 
                                                ></asp:TextBox>
                                            <span class="unit"> LTR</span>
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="hfdRobHYD" runat="server" />
                                            <asp:TextBox ID="txtRobHYD" runat="server" MaxLength="16" 
                                                onkeypress="FloatValueOnly(this)" ReadOnly="true" type="text" />
                                            <asp:HiddenField ID="hfdRobHYD_S" runat="server" />
                                            <span class="unit">LTR</span> </td>
                                         <div runat="server" visible="false" id="divHYDFWE"> 
                                         <td>
                                              <asp:HiddenField ID="hfdHYDFWE" runat="server" />
                                             <asp:TextBox ID="txtHYDFWE" runat="server" MaxLength="16" onkeypress="FloatValueOnly(this)" ReadOnly="true" type="text" />
                                             <asp:HiddenField ID="hfdHYDFWE_S" runat="server" />
                                             <span class="unit">LTR</span>
                                        </td>
                                             </div>
                                    </tr>
                                  </table>
             </td>
        </tr>
         <tr>
         <td style="background-color:#FFFFFF; border:solid 1px #006B8F;">
            <div style="background-color:#c2c2c2; color:Black; padding:6px; text-align:left;">
                Fresh Water Generated & Consumption :
            </div>
                                
                                <table cellpadding="0" cellspacing="3" border="0" width="100%" >
                                <col width="180px" />       
                                <col width="210px" />
                                <col width="210px" />
                                <col width="210px" />   
                                <col width="210px" /> 
                                <tr>
                            <td >Received</td>
                            <td >Generated</td>
                            <td >Consumed <span style="font-size:9px; font-style:italic;"> ( At This Port )</span></td>
                            <td >ROB</td>
                            <div id="divHeaderFWEFreshWater" runat="server" visible="false">
                            <td >F.W.E.</td>
    </div>
                        </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtRobFesshWaterRecv" runat="server" MaxLength="16" 
                                                onkeypress="FloatValueOnly(this)" />
                                            <span class="unit">MT</span></td>
                                        <td>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtLubeFresh_Generated" MaxLength="16" style="text-align:right;"></asp:TextBox>
                                            <span class="unit"> MT</span> </td>
                                            <td>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtLubeFresh_Consumed" MaxLength="16" style="text-align:right;"></asp:TextBox>
                                            <span class="unit"> MT</span></td>
                                        <td>
                                           
                                           <asp:HiddenField ID="hfdRobFesshWater" runat="server" />
                                <asp:TextBox  ID="txtRobFesshWater" onkeypress="FloatValueOnly(this)" MaxLength="16" runat="server" ReadOnly="true" />
                                <asp:HiddenField ID="hfdRobFesshWater_S" runat="server" />
                                <span class="unit"> MT</span>
                                           </td>
                                        <div id="divFWEFressWater" runat="server" visible="false">
                                             <td>
                                            <asp:HiddenField ID="hfdFWEFesshWater" runat="server" />
 <asp:TextBox  ID="txtFWEFesshWater" onkeypress="FloatValueOnly(this)" MaxLength="16" runat="server" ReadOnly="true" /> <span class="unit"> MT</span>
                                            <asp:HiddenField ID="hfdFWEFesshWater_S" runat="server" />
                                        </td>
                                        </div>
                                        
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            Remark
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                             <asp:TextBox  ID="txtConsumptionRemark"  runat="server" TextMode="MultiLine"  Width="80%" Height="30px"  />
                                        </td>
                                    </tr>
                                </table>
        </td>
        </tr>
</table>
<div class="div1">Cargo & Ballast Weight :</div>
<table cellpadding="4" cellspacing="0" border="0" width="100%" style="background-color:#FFFFFF; border:solid 1px #006B8F;">
                                <col width="180px" />       
                                <col width="210px" />
                                <col width="210px" />    
                                <col/>    
                                <tr>
                                        <td>Total Cargo Weight :</td>
                                        <td>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtTotalCargoWeight" MaxLength="16" style="text-align:right;"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator25" ControlToValidate="txtTotalCargoWeight" ErrorMessage="Total Cargo Weight is required." Text="*"></asp:RequiredFieldValidator>
                                            <span class="unit"> MT</span>
                                        </td>
                                        <td>Total Ballast Weight :</td>
                                        <td>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtBallastWeight" MaxLength="16" style="text-align:right;"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator26" ControlToValidate="txtBallastWeight" ErrorMessage="Total Ballast Weight is required." Text="*"></asp:RequiredFieldValidator>
                                            <span class="unit"> MT</span>
                                        </td>
                                        <td>Displacement :</td>
                                        <td>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtTotalDisplacement" MaxLength="16" style="text-align:right;"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator83" ControlToValidate="txtTotalDisplacement" ErrorMessage="Displacement is required." Text="*"></asp:RequiredFieldValidator>
                                            <span class="unit"> MT</span>
                                        </td>
                                </tr>
                                 <tr><td>Cargo Qty Loaded /Discharged</td><td> <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtCargoQtyLoaded" MaxLength="16" style="text-align:right;"></asp:TextBox>
 <asp:RequiredFieldValidator ValidationGroup="grp1" runat="server" id="rfv_txtCargoQtyLoaded" ControlToValidate="txtCargoQtyLoaded" ErrorMessage="Cargo Qty Loaded is required." Text="*"></asp:RequiredFieldValidator>
 <span class="unit"> MT</span></td>
    <td>B/L Qty</td><td> <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtBLQty" MaxLength="16" style="text-align:right;"></asp:TextBox>
 <asp:RequiredFieldValidator ValidationGroup="grp1" runat="server" id="rfv_txtBLQty" ControlToValidate="txtBLQty" ErrorMessage="B/L Qty   is required." Text="*"></asp:RequiredFieldValidator>
 <span class="unit"> MT</span></td>
    
    </tr> 
     <div id="divContainerUnit" visible="false" runat="server">
                                <tr >
                                    <td colspan="6">
                                        <div style="background-color:#c2c2c2; color:Black; padding:6px; text-align:left;">
                                            Total Container Unit( Only for Container Vessel )
                                        </div>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>20Ft-Laden :</td>
                                    <td>
                                        <asp:TextBox ID="txtLaden20Ft" runat="server" ></asp:TextBox>
                                    </td>
                                    <td>40Ft-Laden :</td>
                                    <td>
                                        <asp:TextBox ID="txtLaden40Ft" runat="server" ></asp:TextBox>
                                    </td>
                                    <td><%--45Ft-Laden :--%> Maximum Cargo Capacity ( Loading rates met ):  </td>
                                    <td>
                                        <asp:DropDownList ID="ddlMaximumCargoCapacity" runat="server">
                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                            <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                            <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                        </asp:DropDownList>
                                        <span class="unit"> &nbsp;</span>
                                        <asp:TextBox ID="txtLaden45Ft" runat="server"  Visible="false" ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>20Ft-Empty :</td>
                                    <td>
                                        <asp:TextBox ID="txtEmpty20Ft" runat="server" ></asp:TextBox>
                                    </td>
                                    <td>40Ft-Empty :</td>
                                    <td>
                                        <asp:TextBox ID="txtEmpty40Ft" runat="server" ></asp:TextBox>
                                    </td>
                                    <td><%--45Ft-Empty :--%></td>
                                    <td>
                                        <asp:TextBox ID="txtEmpty45Ft" runat="server" Visible="false" ></asp:TextBox>
                                    </td>
                                </tr>
                              <tr>
                                    <td>20Ft-REFR Laden :</td>
                                    <td>
                                        <asp:TextBox ID="txtREFRLaden20Ft" runat="server" ></asp:TextBox>
                                    </td>
                                    <td>40Ft-REFR Laden :</td>
                                    <td>
                                        <asp:TextBox ID="txtREFRLaden40Ft" runat="server" ></asp:TextBox>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <%--<tr>
                                    <td>20Ft-Laden :</td>
                                    <td>
                                        <asp:TextBox ID="txtLaden20Ft" runat="server" ></asp:TextBox>
                                    </td>
                                    <td>40Ft-Laden :</td>
                                    <td>
                                        <asp:TextBox ID="txtLaden40Ft" runat="server" ></asp:TextBox>
                                    </td>
                                    <td>45Ft-Laden :</td>
                                    <td>
                                        <asp:TextBox ID="txtLaden45Ft" runat="server" ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>20Ft-Empty :</td>
                                    <td>
                                        <asp:TextBox ID="txtEmpty20Ft" runat="server" ></asp:TextBox>
                                    </td>
                                    <td>40Ft-Empty :</td>
                                    <td>
                                        <asp:TextBox ID="txtEmpty40Ft" runat="server" ></asp:TextBox>
                                    </td>
                                    <td>45Ft-Empty :</td>
                                    <td>
                                        <asp:TextBox ID="txtEmpty45Ft" runat="server" ></asp:TextBox>
                                    </td>
                                </tr>--%>
         </div>
</table>
<div class="div1">Performance :</div>
<table cellpadding="4" cellspacing="0" border="0" width="100%" style="background-color:#FFFFFF; border:solid 1px #006B8F;">
<tr>
<td style="width:50%">
    <div style="background-color:#c2c2c2; color:Black; padding:6px; text-align:left;">ME Performance(Since Last report)</div>
    <table cellpadding="4" cellspacing="0" border="0" width="100%" >
                                <col width="180px" />       
                                <col />       
                                <tr>
                                        <td>    
                                            Exh Temp Min :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtExhTempMin" MaxLength="16" style="text-align:right;"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator27" ControlToValidate="txtExhTempMin" ErrorMessage="Exh Temp Min is required." Text="*"></asp:RequiredFieldValidator>
                                            <span class="unit"> C</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Exh Temp Max : 
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtExhTempMax" MaxLength="16" style="text-align:right;"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator28" ControlToValidate="txtExhTempMax" ErrorMessage="Exh Temp Max is required." Text="*"></asp:RequiredFieldValidator>
                                            <span class="unit"> C</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ME RPM :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtMERPM" MaxLength="16" style="text-align:right;"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator29" ControlToValidate="txtMERPM" ErrorMessage="ME RPM is required." Text="*"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Engine Distance :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" onkeyup="CalSlip()" ID="txtEngineDistance" MaxLength="16" style="text-align:right;"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator30" ControlToValidate="txtEngineDistance" ErrorMessage="Engine Distance is required." Text="*"></asp:RequiredFieldValidator>
                                            <span class="unit"> NM</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Slip :
                                        </td>
                                        <td>
                                             <asp:TextBox runat="server" ReadOnly="true"  ID="txtSlip" MaxLength="16" style="text-align:right;background-color:#C0C0C0;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ME Output :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtMEOutput" MaxLength="16" style="text-align:right;"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator32" ControlToValidate="txtMEOutput" ErrorMessage="ME Output is required." Text="*"></asp:RequiredFieldValidator>
                                            <span class="unit"> KW </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ME Thermal Load :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtMEThermalLoad" MaxLength="16" style="text-align:right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>ME LOADINDICATOR :</td>
                                        <td>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtMeLoadIindicator" MaxLength="16" style="text-align:right;"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator31" ControlToValidate="txtMeLoadIindicator" ErrorMessage="ME LOADINDICATOR is required." Text="*"></asp:RequiredFieldValidator>
                                            <span class="unit"> %</span>
                                         </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ME T/C( No. 1)RPM :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtME1" MaxLength="16" style="text-align:right;"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator33" ControlToValidate="txtME1" ErrorMessage="ME T/C( No. 1)RPM is required." Text="*"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ME T/C( No. 2)RPM :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtME2" MaxLength="16" style="text-align:right;"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator34" ControlToValidate="txtME2" ErrorMessage="ME T/C( No. 2)RPM is required." Text="*"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td> 
                                            ME SCAV Pressure : 
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtMESCAVPressure" MaxLength="16" style="text-align:right;"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator35" ControlToValidate="txtMESCAVPressure" ErrorMessage="ME SCAV Pressure is required." Text="*"></asp:RequiredFieldValidator>                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            SCAV TEMP :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtSCAVTEMP" MaxLength="16" style="text-align:right;"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator36" ControlToValidate="txtSCAVTEMP" ErrorMessage="SCAV TEMP is required." Text="*"></asp:RequiredFieldValidator>                                            
                                            <span class="unit"> C</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Lube Oil Pressure :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtLubeOilPressure" MaxLength="16" style="text-align:right;"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator37" ControlToValidate="txtLubeOilPressure" ErrorMessage="Lube Oil Pressure is required." Text="*"></asp:RequiredFieldValidator>                                            
                                            <span class="unit"> BAR</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Sea Water Temp :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtSeaWaterTemp" MaxLength="16" style="text-align:right;"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator38" ControlToValidate="txtSeaWaterTemp" ErrorMessage="Sea Water Temp is required." Text="*"></asp:RequiredFieldValidator>                                            
                                            <span class="unit"> C</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Eng. Room Temp :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtEngRoomTemp" MaxLength="16" style="text-align:right;"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator39" ControlToValidate="txtEngRoomTemp" ErrorMessage="Eng. Room Temp is required." Text="*"></asp:RequiredFieldValidator>                                            
                                            <span class="unit"> C</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            
                                            Bilge Pump( if run ) :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtBligPump" MaxLength="16" style="text-align:right;"></asp:TextBox>
                                            <span class="unit"> Hrs</span>
                                        </td>
                                    </tr>
                                </table>
</td>
<td style="vertical-align:top">
<div style="background-color:#c2c2c2; color:Black; padding:6px; text-align:left;">AE Performance (Since Last Report)</div>
    <table cellpadding="4" cellspacing="0" border="0" width="100%" >
                                <col width="170px" />
                                    <tr>
                                        
                                        <td colspan="2">
                                            <table cellpadding="4" cellspacing="0" border="0" width="100%" style="text-align:left;" >
                                                <col style="width:40px;" />
                                                <col style="width:95px;" />
                                                <col  />
                                                <col style="width:95px;" />
                                                <col />
                                                <col style="width:95px;" />
                                                <col />
                                                <col style="width:95px;" />
                                                <col />
                                                <tr>
                                                    <td></td>
                                                    <td>Load</td>
                                                    <td>&nbsp;</td>
                                                    <td>HRS ( if run )</td>
                                                    <td>&nbsp;</td>
                                                    <td>Exh Temp Min</td>
                                                    <td>&nbsp;</td>
                                                    <td>Exh Temp Max</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                               
                                                <tr>
                                                    <td>A/ E1 </td>
                                                    <td>
                                                        <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);"  onkeyup="CalTotAuxiliary()" ID="AUX_1_Load" MaxLength="16" CssClass="aedata"></asp:TextBox> 
                                                    </td>
                                                    <td><span class="unit"> KW</span>
                                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator40" ControlToValidate="AUX_1_Load" ErrorMessage="AUX 1 Load is required." Text="*"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);"  ID="txtA_ENo_1" MaxLength="16" CssClass="aedata"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <span class="unit"> Hrs</span></td>
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator45" ControlToValidate="txtA_ENo_1" ErrorMessage="A/E No 1 is required." Text="*"></asp:RequiredFieldValidator>
                                            
                                                    <td>
                                                        <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);"  ID="txtAuxTempMin1" MaxLength="16" CssClass="aedata"></asp:TextBox>
                                                        </td>
                                                    <td>
                                                        <span class="unit"> C</span></td>
                                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator90" ControlToValidate="txtAuxTempMin1" ErrorMessage="AUX Temp Min 1 is required." Text="*"></asp:RequiredFieldValidator>
                                                        
                                                    <td>
                                                        <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);"  ID="txtAuxTempMax1" MaxLength="16" CssClass="aedata"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <span class="unit"> C</span>
                                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator91" ControlToValidate="txtAuxTempMax1" ErrorMessage="AUX Temp Max 1 is required." Text="*"></asp:RequiredFieldValidator>
                                                        
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td>A/E 2 </td>
                                                    <td>
                                                        <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);"  onkeyup="CalTotAuxiliary()" ID="AUX_2_Load" MaxLength="16" CssClass="aedata"></asp:TextBox> 
                                                    </td>
                                                    <td>
                                                        <span class="unit"> KW</span>
                                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator41" ControlToValidate="AUX_2_Load" ErrorMessage="AUX 2 Load is required." Text="*"></asp:RequiredFieldValidator>
                                                        
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);"  ID="txtA_ENo_2" MaxLength="16" CssClass="aedata"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <span class="unit"> Hrs</span>
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator46" ControlToValidate="txtA_ENo_2" ErrorMessage="A/E No 2 is required." Text="*"></asp:RequiredFieldValidator>
                                            
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);"  ID="txtAuxTempMin2" MaxLength="16" CssClass="aedata"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <span class="unit"> C</span>
                                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator92" ControlToValidate="txtAuxTempMin2" ErrorMessage="AUX Temp Min 2 is required." Text="*"></asp:RequiredFieldValidator>
                                                        
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);"  ID="txtAuxTempMax2" MaxLength="16" CssClass="aedata"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <span class="unit"> C</span>
                                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator93" ControlToValidate="txtAuxTempMax2" ErrorMessage="AUX Temp Max 2 is required." Text="*"></asp:RequiredFieldValidator>
                                                        
                                                    </td>
                                                </tr>                                               
                                                
                                                <tr>
                                                    <td>A/E 3 </td>
                                                    <td>
                                                        <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);"  onkeyup="CalTotAuxiliary()" ID="AUX_3_Load" MaxLength="16" CssClass="aedata"></asp:TextBox> 
                                                    </td>
                                                    <td>
                                                        <span class="unit"> KW</span>
                                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator42" ControlToValidate="AUX_3_Load" ErrorMessage="AUX 3 Load is required." Text="*"></asp:RequiredFieldValidator>
                                                        
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);"  ID="txtA_ENo_3" MaxLength="16" CssClass="aedata"></asp:TextBox>
                                                        </td>
                                                    <td>
                                                        <span class="unit"> Hrs</span>
                                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator47" ControlToValidate="txtA_ENo_3" ErrorMessage="A/E No 3 is required." Text="*"></asp:RequiredFieldValidator>
                                                        </td>
                                                    <td>
                                                        <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);"  ID="txtAuxTempMin3" MaxLength="16" CssClass="aedata"></asp:TextBox>
                                                        </td>
                                                    <td>
                                                        <span class="unit"> C</span>
                                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator94" ControlToValidate="txtAuxTempMin3" ErrorMessage="AUX Temp Min 3 is required." Text="*"></asp:RequiredFieldValidator>
                                                        </td>
                                                    <td>
                                                        <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);"  ID="txtAuxTempMax3" MaxLength="16" CssClass="aedata"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <span class="unit"> C</span>
                                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator95" ControlToValidate="txtAuxTempMax3" ErrorMessage="AUX Temp Max 3 is required." Text="*"></asp:RequiredFieldValidator>
                                                        
                                                    </td>
                                                </tr>
                                                
                                                <tr>
                                                    <td>A/E 4 </td>

                                                    <td>
                                                        <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);"  onkeyup="CalTotAuxiliary()" ID="AUX_4_Load" MaxLength="16" CssClass="aedata"></asp:TextBox> 
                                                    </td>
                                                    <td>
                                                        <span class="unit"> KW</span>
                                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator43" ControlToValidate="AUX_4_Load" ErrorMessage="AUX 4 Load is required." Text="*"></asp:RequiredFieldValidator>
                                                        
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);"  ID="txtA_ENo_4" MaxLength="16" CssClass="aedata"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <span class="unit"> Hrs</span>
                                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator48" ControlToValidate="txtA_ENo_4" ErrorMessage="A/E No 4 is required." Text="*"></asp:RequiredFieldValidator>
                                                        
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);"  ID="txtAuxTempMin4" MaxLength="16" CssClass="aedata"></asp:TextBox>
                                                        </td>
                                                    <td>
                                                        <span class="unit"> C</span>
                                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator96" ControlToValidate="txtAuxTempMin4" ErrorMessage="AUX Temp Min 4 is required." Text="*"></asp:RequiredFieldValidator>
                                                        </td>
                                                    <td>
                                                        <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);"  ID="txtAuxTempMax4" MaxLength="16" CssClass="aedata"></asp:TextBox>
                                                        </td>
                                                    <td>
                                                        <span class="unit"> C</span>
                                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator97" ControlToValidate="txtAuxTempMax4" ErrorMessage="AUX Temp Max 4 is required." Text="*"></asp:RequiredFieldValidator>
                                                        </td>
                                                </tr>
                                                </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Total AUX Load :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ReadOnly="true"  ID="lblTotAuxiliary" MaxLength="16" style="text-align:right;background-color:#C0C0C0; border-color:gray;"></asp:TextBox> 
                                            <span class="unit"> KW</span>
                                        </td>
                                    </tr>
                                      <tr>
                                        <td>A/E FO INLET TEMP :</td>
                                        <td>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);"  ID="txtAEFOInlinetTemp" MaxLength="16" style="text-align:right;"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator49" ControlToValidate="txtAEFOInlinetTemp" ErrorMessage="A/E FO INLET TEMP is required." Text="*"></asp:RequiredFieldValidator>
                                            <span class="unit"> C</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td > &nbsp; </td>
                                        <td></td>
                                    </tr>
                                     <tr>
                                        <td>Shaft Generator ( Hrs ) :</td>
                                        <td>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);"  ID="txtSGH" MaxLength="16" style="text-align:right;" width="50px" ></asp:TextBox>
                                            <span class="unit"> HRS</span>
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator72" ControlToValidate="txtSGH" ErrorMessage="Shaft Genertor hours required." Text="*"></asp:RequiredFieldValidator>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);"  ID="txtSGM" MaxLength="16" style="text-align:right;" width="50px"></asp:TextBox>
                                            <span class="unit"> MIN</span>
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator79" ControlToValidate="txtSGM" ErrorMessage="Shaft Genertor mins required." Text="*"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>

                                     <tr>
                                        <td>Tank Cleaning :</td>
                                        <td>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);"  ID="txtTCH" MaxLength="16" style="text-align:right;" width="50px" ></asp:TextBox>
                                            <span class="unit"> HRS</span>
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator73" ControlToValidate="txtTCH" ErrorMessage="Tank cleaning hours required." Text="*"></asp:RequiredFieldValidator>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);"  ID="txtTCM" MaxLength="16" style="text-align:right;" width="50px"></asp:TextBox>
                                            <span class="unit"> MIN</span>
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator80" ControlToValidate="txtTCM" ErrorMessage="Tank cleaning mins required." Text="*"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>Cargo Heating :</td>
                                        <td>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);"  ID="txtCHH" MaxLength="16" style="text-align:right;" width="50px" ></asp:TextBox>
                                            <span class="unit"> HRS</span>
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator81" ControlToValidate="txtCHH" ErrorMessage="Cargo heating hours required." Text="*"></asp:RequiredFieldValidator>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);"  ID="txtCHMin" MaxLength="16" style="text-align:right;" width="50px"></asp:TextBox>
                                            <span class="unit"> MIN</span>
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator74" ControlToValidate="txtCHMin" ErrorMessage="Cargo heating mins required." Text="*"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>Inert hours :</td>
                                        <td>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);"  ID="txt_InertH" MaxLength="16" style="text-align:right;" width="50px" ></asp:TextBox>
                                            <span class="unit"> HRS</span>
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator75" ControlToValidate="txt_InertH" ErrorMessage="Inert hours required." Text="*"></asp:RequiredFieldValidator>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);"  ID="txt_InertM" MaxLength="16" style="text-align:right;" width="50px"></asp:TextBox>
                                            <span class="unit"> MIN</span>
                                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator82" ControlToValidate="txt_InertM" ErrorMessage="Inert mins required." Text="*"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                     <%--<tr runat="server" id="trGarbage" visible="false">
                                        <td>
                                            Garbage Generated :
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);"  ID="txtSinceLastReport" MaxLength="16" style="text-align:right;" ></asp:TextBox>
                                            <span class="unit"> M<sup>3</sup></span>
                
                                        </td>
                                    </tr>--%>
                                </table>
</td>
</tr>
</table>
<br />

<div id="trGarbage" runat="server">
    <div class="div1">Garbage Landed (Since Last Dep. Report) :</div>
    <table cellpadding="4" cellspacing="0" border="0" width="100%" style="background-color:#FFFFFF; border:solid 1px #006B8F;">
        <col width="180px" />       
        <col width="300px" />       
        <col width="180px" />    
        <col width="250px" />    
        <col width="150px" />    
        <col />       
        <tr>
            <td>Garbage Landed :</td>
            <td>
                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);"  ID="txtSinceLastReport" MaxLength="16" style="text-align:right;" ></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator111" ControlToValidate="txtSinceLastReport" ErrorMessage="Garbage Landed is required." Text="*"></asp:RequiredFieldValidator>
                <span class="unit"> M<sup>3</sup></span>&nbsp;
                <span style="font-size:10px"></span>
            </td>
            <td>Sludge Landed :</td>
            <td>
                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);"  ID="txtSludgelanded" MaxLength="16" style="text-align:right;" ></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator110" ControlToValidate="txtSludgelanded" ErrorMessage="Sludge Landed is required." Text="*"></asp:RequiredFieldValidator>
                <span class="unit"> M<sup>3</sup></span>&nbsp;
            </td>
            <td>Oily Bilge Water Landed :</td>
            <td>
                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);"  ID="txtOilyBilgWaterLanded" MaxLength="16" style="text-align:right;" ></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator109" ControlToValidate="txtOilyBilgWaterLanded" ErrorMessage="Oily Bilge Water Landed is required." Text="*"></asp:RequiredFieldValidator>
                <span class="unit"> M<sup>3</sup></span>&nbsp;
            </td>
        </tr>
        <tr>
            <td>Bilge Water Landed :</td>
            <td>
                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);"  ID="txtBilgeWaterLanded" MaxLength="16" style="text-align:right;" ></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator108" ControlToValidate="txtBilgeWaterLanded" ErrorMessage="Bilge Water Landed is required." Text="*"></asp:RequiredFieldValidator>
                <span class="unit"> M<sup>3</sup></span>&nbsp;
            </td>
            <td>Soot Landed :</td>
            <td>
                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);"  ID="txtSootLanded" MaxLength="16" style="text-align:right;" ></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator107" ControlToValidate="txtSootLanded" ErrorMessage="Soot Landed is required." Text="*"></asp:RequiredFieldValidator>
                <span class="unit"> M<sup>3</sup></span>&nbsp;
            </td>
            <td></td>
            <td></td>
        </tr>
    </table>
</div>

    <div id="trBWTS" runat="server">
    <div class="div1">BWTS (Since Last Dep. Report) :</div>
    <table cellpadding="4" cellspacing="0" border="0" width="100%" style="background-color:#FFFFFF; border:solid 1px #006B8F;">
         <col width="180px" /> 
         <col width="300px" /> 
         <col width="180px" /> 
        <tr>
            <td>QTY Ballasted :</td>
            <td><asp:TextBox ID="txtQTYBallasted" runat="server" MaxLength="16" onkeypress="FloatValueOnly(this)" style="text-align:right;"/>
                      <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator103" ControlToValidate="txtQTYBallasted" ErrorMessage="QTY Ballasted is required." Text="*"></asp:RequiredFieldValidator>
                      <span class="unit">MT</span>
            </td>
            <td>QTY DeBallasted :</td>
            <td><asp:TextBox ID="txtQTYDeBallasted" runat="server" MaxLength="16" onkeypress="FloatValueOnly(this)" style="text-align:right;"/>
                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator112" ControlToValidate="txtQTYDeBallasted" ErrorMessage="QTY DeBallasted is required." Text="*"></asp:RequiredFieldValidator>
                <span class="unit">MT</span></td>
        </tr>
        <tr>
            <td>BWTS Used :</td>
            <td><asp:DropDownList ID="ddlBWTS" runat="server" >
                    <asp:ListItem Text="< Select >" Value=""></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                    <asp:ListItem Text="No" Value="N"></asp:ListItem>
                    <asp:ListItem Text="NA" Value="A"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator113" ControlToValidate="ddlBWTS" ErrorMessage="Please select BWTS Used." Text="*"></asp:RequiredFieldValidator>
                </td>
            <td>Hrs ( Used ) :</td>
            <td>
                <asp:TextBox runat="server" Text="0" onkeypress="IntValueOnly(this);"  ID="txtBWTSHrs" MaxLength="6" style="text-align:right;"></asp:TextBox>
                 <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator114" ControlToValidate="txtBWTSHrs" ErrorMessage="BWTS Hrs are required." Text="*"></asp:RequiredFieldValidator>
               <span class="unit">Hrs</span>
                ( <em>If not used enter zero.</em> )</td>

        </tr>
        <tr>
            <td style="vertical-align:top" >Remarks :</td>
            <td colspan="3">
                <asp:TextBox runat="server" Text="" ID="txtBWTSRemarks" Rows="6" TextMode="MultiLine" Width="95%"></asp:TextBox>                
            </td>
        </tr>
        </table>
</div>
<div class="div1">MARPOL (Since Last Report) :</div>
<table cellpadding="4" cellspacing="0" border="0" width="100%" style="background-color:#FFFFFF; border:solid 1px #006B8F;">
    <col width="180px" />       
    <col width="300px" />       
    <col width="180px" />    
    <col width="250px" />    
    <col width="130px" />    
    <col />       
    <tr>
        <td>Bilge Water Tank ROB :</td>            
        <td>
            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" Text="0" ID="txtBILGEWaterTankROB" MaxLength="16" style="text-align:right;" ></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator106" ControlToValidate="txtBILGEWaterTankROB" ErrorMessage="Bilge Water Tank ROB is required." Text="*"></asp:RequiredFieldValidator>
            <span class="unit"> M<sup>3</sup></span>&nbsp;
        </td>            
        <td>Oily Bilge Tank ROB :</td>
        <td>
            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" Text="0" ID="txtOILYBILGETankROB" MaxLength="16" style="text-align:right;" ></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator105" ControlToValidate="txtOILYBILGETankROB" ErrorMessage="Oily Bilge Tank ROB is required." Text="*"></asp:RequiredFieldValidator>
            <span class="unit"> M<sup>3</sup></span>&nbsp;
        </td>            
        <td>Sludge Tank ROB :</td>            
        <td>
            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" Text="0" ID="txtSLUDGETANKROB" MaxLength="16" style="text-align:right;" ></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator104" ControlToValidate="txtSLUDGETANKROB" ErrorMessage="Sludge Tank ROB is required." Text="*"></asp:RequiredFieldValidator>
            <span class="unit"> M<sup>3</sup></span>&nbsp;
        </td>
    </tr>
    <%--33333333333--%>
    <tr>
        <td>OWS :</td>            
        <td>
            <asp:TextBox runat="server" Text="0" onkeypress="FloatValueOnly(this);"  ID="txtOWSH" MaxLength="16" style="text-align:right;" width="50px" ></asp:TextBox>
            <span class="unit"> HRS</span>
            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator98" ControlToValidate="txtOWSH" ErrorMessage="OWS hours required." Text="*"></asp:RequiredFieldValidator>
            <asp:TextBox runat="server" Text="0"  onkeypress="FloatValueOnly(this);"  ID="txtOWSM" MaxLength="16" style="text-align:right;" width="50px"></asp:TextBox>
            <span class="unit"> MIN</span>
            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator99" ControlToValidate="txtOWSM" ErrorMessage="OWS mins required." Text="*"></asp:RequiredFieldValidator>
        </td>            
        <td>Incinerator : </td>
        <td>
            <asp:TextBox runat="server" Text="0" onkeypress="FloatValueOnly(this);"  ID="txtINCINERATOR_H" MaxLength="16" style="text-align:right;" width="50px" ></asp:TextBox>
            <span class="unit"> HRS</span>
            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator100" ControlToValidate="txtINCINERATOR_H" ErrorMessage="Incinerator hours required." Text="*"></asp:RequiredFieldValidator>
            <asp:TextBox runat="server" Text="0"  onkeypress="FloatValueOnly(this);"  ID="txtINCINERATOR_M" MaxLength="16" style="text-align:right;" width="50px"></asp:TextBox>
            <span class="unit"> MIN</span>
            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator101" ControlToValidate="txtINCINERATOR_M" ErrorMessage="Incinerator mins required." Text="*"></asp:RequiredFieldValidator>
        </td>            
        <td>Garbage ROB:</td>            
        <td>
            <asp:TextBox runat="server" Text="0" onkeypress="FloatValueOnly(this);"  ID="txtGARBAGE_ROB" MaxLength="16" style="text-align:right;" width="50px" ></asp:TextBox>
            <span class="unit"> M<sup>3</sup></span>&nbsp;
            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator102" ControlToValidate="txtGARBAGE_ROB" ErrorMessage="Garbage ROB required." Text="*"></asp:RequiredFieldValidator>
        </td>
    </tr>
</table>
<%--<asp:UpdatePanel runat="server">
<ContentTemplate>
<table cellpadding="4" cellspacing="0" border="0" width="100%" style="background-color:#FFFFFF; border:solid 1px #006B8F;">
    <colgroup>
        <col width="50%" />
        <col  />
        <tr>
            <td style="vertical-align:top;">
                <div style="padding-bottom:7px;">
                    <table cellpadding="0" width="100%">
                        <tr>
                            <td><b>Marine Verification</b></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkMarineVerified" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtMarineRemarks" runat="server" Height="70px" TextMode="MultiLine" Width="90%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMsgCommentsMarineUpdateByOn" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnSaveMarineRemarks" runat="server" CausesValidation="false" CssClass="btn" OnClick="btnSaveMarineRemarks_OnClick" Text="Save" Width="120px" />
                                &nbsp;
                                <asp:Button ID="btnSendToShipMarine" runat="server" CssClass="btn" OnClick="btnSendToShipMarine_OnClick" Text=" Send To Ship " Width="150px" CausesValidation="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMsgCommentsMarine" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="hfdMBy" runat="server" />
                </div>
            </td>
            <td style="vertical-align:top;">
                <div style="padding-bottom:7px;">
                    <table cellpadding="0" width="100%">
                        <tr>
                            <td><b>Technical Verification </b></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chkTechVerified" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtTechnicalRemarks" runat="server" Height="70px" TextMode="MultiLine" Width="90%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMsgCommentsTechnicalUpdateByOn" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnSaveTechnicalRemarks" runat="server" CausesValidation="false" CssClass="btn" OnClick="btnSaveTechnicalRemarks_OnClick" Text="Save" Width="120px" />
                                &nbsp;
                                <asp:Button ID="btnSendToShipTechnical" runat="server" CssClass="btn" OnClick="btnSendToShipTechnical_OnClick" Text=" Send To Ship " Width="150px"  CausesValidation="false"  />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblMsgCommentsTechnical" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="hfdTBy" runat="server" />
                </div>
            </td>
        </tr>
    </colgroup>
</table>
    

</ContentTemplate>
</asp:UpdatePanel>
<br /><br /><br /><br /><br /><br /><br /><br /><br />--%>
    
</div>
</div>
<div style="text-align:left;" class="stickyFooter"> 
<div>
    <asp:ValidationSummary runat="server" ID="v1" class="validationsummary"/>

</div>
<div style="width:98%; text-align:right; ">
    <asp:Label runat="server" ID="lblMessage" ForeColor="Red" style="float:left" Font-Bold="true" Font-Size="Large"></asp:Label>
    <%--<asp:Button ID="btnExport" CssClass="btn" runat="server" Text="Export" OnClick="btnExport_Click" Width="120px"/>--%>
    <asp:Button ID="btnClose" CausesValidation="false" OnClientClick="self.close();" Text="Close" CssClass="btn" Width="120px" runat="server" />
</div>
</div>
<script type="text/javascript">
    function CheckNegativeValue(ctl) {
        if (parseFloat($(ctl).val()) < 0) {
            $(ctl).addClass("InvalidCellvalue");
        }
        else {
            $(ctl).removeClass("InvalidCellvalue");
        }
    }
    function Page_CallAfterRefresh() {
        RegisterAutoComplete();
        RegisterAutoComplete2();

        $("#txtME_IFO45,#txtAE_IFO45,#txtCargoHeating_IFO45").keyup(function () {
            var tmp = ConvertToDec($("#hfdRobIFO45").val()) + ConvertToDec($("#txtRobIFO45Recv").val()) - ConvertToDec($("#txtME_IFO45").val()) - ConvertToDec($("#txtAE_IFO45").val()) - ConvertToDec($("#txtCargoHeating_IFO45").val());
            tmp = roundToTwo(tmp);
            $("#txtRobIFO45").val(tmp);
            $("#hfdRobIFO45_S").val(tmp);
            CheckNegativeValue($("#txtRobIFO45"));
        });

        $("#txtME_IFO1,#txtAE_IFO1,#txtCargoHeating_IFO1").keyup(function () {
            var tmp = ConvertToDec($("#hfdRobIFO1").val()) + ConvertToDec($("#txtRobIFO1Recv").val()) - ConvertToDec($("#txtME_IFO1").val()) - ConvertToDec($("#txtAE_IFO1").val()) - ConvertToDec($("#txtCargoHeating_IFO1").val());
            tmp = roundToTwo(tmp);
            $("#txtRobIFO1").val(tmp);
            $("#hfdRobIFO1_S").val(tmp);
            CheckNegativeValue($("#txtRobIFO1"));
        });

        $("#txtME_MGO5,#txtAE_MGO5,#txtCargoHeating_MGO5").keyup(function () {
            var tmp = ConvertToDec($("#hfdRobMGO5").val()) + ConvertToDec($("#txtRobMGO5Recv").val()) - ConvertToDec($("#txtME_MGO5").val()) - ConvertToDec($("#txtAE_MGO5").val()) - ConvertToDec($("#txtCargoHeating_MGO5").val());
            tmp = roundToTwo(tmp);
            $("#txtRobMGO5").val(tmp);
            $("#hfdRobMGO5_S").val(tmp);
            CheckNegativeValue($("#txtRobMGO5"));
        });

        $("#txtME_MGO10,#txtAE_MGO1,#txtCargoHeating_MGO1").keyup(function () {
            var tmp = ConvertToDec($("#hfdRobMGO1").val()) + ConvertToDec($("#txtRobMGO1Recv").val()) - ConvertToDec($("#txtME_MGO10").val()) - ConvertToDec($("#txtAE_MGO1").val()) - ConvertToDec($("#txtCargoHeating_MGO1").val());
            tmp = roundToTwo(tmp);
            $("#txtRobMGO1").val(tmp);
            $("#hfdRobMGO1_S").val(tmp);
            CheckNegativeValue($("#txtRobMGO1"));
        });

        $("#txtME_MDO,#txtAE_MDO,#txtCargoHeating_MDO").keyup(function () {
            var tmp = ConvertToDec($("#hfdRobMDO").val()) + ConvertToDec($("#txtRobMDORecv").val()) - ConvertToDec($("#txtME_MDO").val()) - ConvertToDec($("#txtAE_MDO").val()) - ConvertToDec($("#txtCargoHeating_MDO").val());
            tmp = roundToTwo(tmp);
            $("#txtRobMDO").val(tmp);
            $("#hfdRobMDO_S").val(tmp);
            CheckNegativeValue($("#txtRobMDO"));
        });

        $("#txtLubeFresh_MECC").keyup(function () {
            var tmp = ConvertToDec($("#hfdRobMECC").val()) + ConvertToDec($("#txtRobMECCRecv").val()) - ConvertToDec($("#txtLubeFresh_MECC").val());
            tmp = roundToTwo(tmp);
            $("#txtRobMECC").val(tmp);
            $("#hfdRobMECC_S").val(tmp);
            CheckNegativeValue($("#txtRobMECC"));
        });

        $("#txtLubeFresh_MECYL").keyup(function () {
            var tmp = ConvertToDec($("#hfdRobMECYL").val()) + ConvertToDec($("#txtRobMECYLRecv").val()) - ConvertToDec($("#txtLubeFresh_MECYL").val());
            tmp = roundToTwo(tmp);
            $("#txtRobMECYL").val(tmp);
            $("#hfdRobMECYL_S").val(tmp);
            CheckNegativeValue($("#txtRobMECYL"));
        });

        $("#txtLubeFresh_AECC").keyup(function () {
            var tmp = ConvertToDec($("#hfdRobAECC").val()) + ConvertToDec($("#txtRobAECCRecv").val()) - ConvertToDec($("#txtLubeFresh_AECC").val());
            tmp = roundToTwo(tmp);
            $("#txtRobAECC").val(tmp);
            $("#hfdRobAECC_S").val(tmp);
            CheckNegativeValue($("#txtRobAECC"));
        });

        $("#txtLubeFresh_HYD").keyup(function () {
            var tmp = ConvertToDec($("#hfdRobHYD").val()) + ConvertToDec($("#txtRobHYDRecv").val()) - ConvertToDec($("#txtLubeFresh_HYD").val());
            tmp = roundToTwo(tmp);
            $("#txtRobHYD").val(tmp);
            $("#hfdRobHYD_S").val(tmp);
            CheckNegativeValue($("#txtRobHYD"));
        });

        $("#txtLubeFresh_Generated,#txtLubeFresh_Consumed").keyup(function () {
            var tmp = ConvertToDec($("#hfdRobFesshWater").val()) + ConvertToDec($("#txtLubeFresh_Generated").val()) + ConvertToDec($("#txtRobFesshWaterRecv").val()) - ConvertToDec($("#txtLubeFresh_Consumed").val());
            tmp = roundToTwo(tmp);
            $("#txtRobFesshWater").val(tmp);
            $("#hfdRobFesshWater_S").val(tmp);
            CheckNegativeValue($("#txtRobFesshWater"));
        });


    }
    $(document).ready(function () {
        Page_CallAfterRefresh();
    });

</script>
</form>
</body>
</html>
