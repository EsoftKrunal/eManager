<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DepartureReport.aspx.cs" Inherits="DepartureReport"  Async="true" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title runat="server">Departure Report</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />

  <style type="text/css">
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
      border:solid 1px #c2c2c2;
      color:black; 
      font-family:Calibri;
      background-color:#E0F5FF;
  }
  textarea
  {
      padding:0px 3px 0px 3px;
      border:solid 1px #c2c2c2;
      color:black; 
      text-align:left;
      font-family:Calibri;
      background-color:#E0F5FF;
  }
  select
  {
      border:solid 1px #c2c2c2;
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
          //var CalSlip=((EngineDistance-DistanceMadeGood)/EngineDistance)*100;
          if (parseFloat(((EngineDistance.toString() == '') ? "0" : EngineDistance)) == 0) {
              Slip.value = 0;
              return false;
          }
          var CalSlip = ((parseFloat(((EngineDistance.toString() == '') ? "0" : EngineDistance)) - parseFloat(((DistanceMadeGood.toString() == '') ? "0" : DistanceMadeGood))) / parseFloat(((EngineDistance.toString() == '') ? "0" : EngineDistance))) * 100;
          Slip.value = Math.round(CalSlip * 10) / 10;
      }
      function TotalConsumptionForIFO45() {

          var ME = document.getElementById('txtME_IFO45').value;
          var AE = document.getElementById('txtAE_IFO45').value;
          var CargoHeating = document.getElementById('txtCargoHeating_IFO45').value;
          var TankCleaning = document.getElementById('txtTankCleaning_IFO45').value;
          var GasFreeing = document.getElementById('txtGasFreeing_IFO45').value;
          var IGS = document.getElementById('txtIGS_IFO45').value;
          var TotalIFO45 = document.getElementById('txtIGS_IFO45_Total');

          var res = parseFloat(((ME.toString() == '') ? "0" : ME)) + parseFloat(((AE.toString() == '') ? "0" : AE))
            + parseFloat(((CargoHeating.toString() == '') ? "0" : CargoHeating))
            + parseFloat(((TankCleaning.toString() == '') ? "0" : TankCleaning))
            + parseFloat(((GasFreeing.toString() == '') ? "0" : GasFreeing))
            + parseFloat(((IGS.toString() == '') ? "0" : IGS));

          TotalIFO45.value = Math.round(res * 100) / 100;
      }
      function TotalConsumptionForIFO1() {

          var ME = document.getElementById('txtME_IFO1').value;
          var AE = document.getElementById('txtAE_IFO1').value;
          var CargoHeating = document.getElementById('txtCargoHeating_IFO1').value;
          var TankCleaning = document.getElementById('txtTankCleaning_IFO1').value;
          var GasFreeing = document.getElementById('txtGasFreeing_IFO1').value;
          var IGS = document.getElementById('txtIGS_IFO1').value;
          var TotalIFO1 = document.getElementById('txtIGS_IFO1_Total');

          var res = parseFloat(((ME.toString() == '') ? "0" : ME)) + parseFloat(((AE.toString() == '') ? "0" : AE))
            + parseFloat(((CargoHeating.toString() == '') ? "0" : CargoHeating))
            + parseFloat(((TankCleaning.toString() == '') ? "0" : TankCleaning))
            + parseFloat(((GasFreeing.toString() == '') ? "0" : GasFreeing))
            + parseFloat(((IGS.toString() == '') ? "0" : IGS));

          TotalIFO1.value = Math.round(res * 100) / 100;
      }

      function TotalConsumptionForMGO5() {

          var ME = document.getElementById('txtME_MGO5').value;
          var AE = document.getElementById('txtAE_MGO5').value;
          var CargoHeating = document.getElementById('txtCargoHeating_MGO5').value;
          var TankCleaning = document.getElementById('txtTankCleaning_MGO5').value;
          var GasFreeing = document.getElementById('txtGasFreeing_MGo5').value;
          var IGS = document.getElementById('txtIGS_MGO5').value;
          var TotalMGO5 = document.getElementById('txtIGS_MGO5_Total');

          var res = parseFloat(((ME.toString() == '') ? "0" : ME)) + parseFloat(((AE.toString() == '') ? "0" : AE))
            + parseFloat(((CargoHeating.toString() == '') ? "0" : CargoHeating))
            + parseFloat(((TankCleaning.toString() == '') ? "0" : TankCleaning))
            + parseFloat(((GasFreeing.toString() == '') ? "0" : GasFreeing))
            + parseFloat(((IGS.toString() == '') ? "0" : IGS));

          TotalMGO5.value = Math.round(res * 100) / 100;
      }

      function TotalConsumptionForMGO1() {

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

        function OnChange_RestrictedAea(obj) {
            var ECA = document.getElementById('chkECA');
            var CA = document.getElementById('chkCA');
            var EU = document.getElementById('chkEU');
            var txtResArea = document.getElementById('txtResArea');
            if (obj.selectedIndex == 1) {
                ECA.disabled = false;
                CA.disabled = false;
                EU.disabled = false;
                txtResArea.disabled = false;
            }
            else {

                ECA.disabled = true;
                ECA.checked = false
                CA.disabled = true;
                CA.checked = false
                EU.disabled = true;
                EU.checked = false
                txtResArea.disabled = true;
                txtResArea.value = "";
            }
        }
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

    <script type="text/javascript" src="../eReports/JS/jquery.min.js"></script>
  <script src="../eReports/JS/AutoComplete/knockout-2.2.1.js" type="text/javascript"></script>
  <script type="text/javascript" src="../eReports/JS/KPIScript.js"></script>
  
  <link rel="stylesheet" href="../eReports/JS/AutoComplete/jquery-ui.css" />
  <script src="../eReports/JS/AutoComplete/jquery-ui.js" type="text/javascript"></script>
<script type="text/javascript">
    function RegisterAutoComplete() {
        $(function () {
            function log(message) {}

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


        $(function () {
            function log(message) { }
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

        //----------- Ports ----------------

        $(function () {
            function log(message) { }
            $("#txtPort1").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: getBaseURL() + "/eReports/getautocompletedata.ashx",
                        dataType: "json",
                        headers: { "cache-control": "no-cache" },
                        type: "POST",
                        data: { Key: $("#txtPort1").val(), Type: "PORT" },
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

        $(function () {
            function log(message) { }
            $("#txtPort2").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: getBaseURL() + "/eReports/getautocompletedata.ashx",
                        dataType: "json",
                        headers: { "cache-control": "no-cache" },
                        type: "POST",
                        data: { Key: $("#txtPort2").val(), Type: "PORT" },
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

        $(function () {
            function log(message) { }
            $("#txtPort3").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: getBaseURL() + "/eReports/getautocompletedata.ashx",
                        dataType: "json",
                        headers: { "cache-control": "no-cache" },
                        type: "POST",
                        data: { Key: $("#txtPort3").val(), Type: "PORT" },
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
<form id="form" runat="server">
<asp:ToolkitScriptManager  ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
<div style="background-color:#006B8F; color:White;padding:8px; font-size:14px;text-align:center;">
    Departure Report
</div>
<div style="padding-left:50px;padding-right:50px;">

<div class="div1">
    Voyage Information :
</div>
<table cellpadding="4" cellspacing="0" border="0" width="100%" >
        <tr>
            <td style="background-color:#ffffff; border:solid 1px #006B8F;">
                   <table cellpadding="0" cellspacing="3" border="0" width="100%" >
                        <tr>
                            <td style="width:220px">Vessel Code :</td>
                            <td style="width:400px">
                                <asp:TextBox runat="server" ReadOnly="true" ID="txtVessel" style="width:40px;text-align:center;" MaxLength="3" ></asp:TextBox>
                            </td>
                            <td style=" width:250px">Report Date :</td>
                            <td >
                                <asp:TextBox runat="server" ID="txtRDate" style="width:100px;text-align:center; " MaxLength="10" ></asp:TextBox>
                                <asp:CalendarExtender runat="server" id="Cal1" TargetControlID="txtRDate" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator runat="server" id="rec1" ControlToValidate="txtRDate" ErrorMessage="Report Date is required." Text="*"></asp:RequiredFieldValidator>
                            </td>                            
                        </tr>                        
                        <tr>
                            <td >Voyage #:</td>
                            <td >
                                <asp:TextBox runat="server" ID="txtVoyageNumber" style="width:200px;text-align:left; " MaxLength="11" ></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" ControlToValidate="txtVoyageNumber" ErrorMessage="Voyage # is required." Text="*"></asp:RequiredFieldValidator>
                            </td>
                            <td >Transiting Any fuel Restricted Area :</td>
                            <td >
                                <asp:DropDownList  ID="ddlRestrictedArea" runat="server">
                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            
                        </tr>
                        <tr>
                            <td>
                                ETA(Restricted Area) :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtResArea" style="width:100px;text-align:center; " MaxLength="15" ></asp:TextBox>
                                <asp:CalendarExtender runat="server" id="CalendarExtender1" TargetControlID="txtResArea" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                            </td>
                            <td>
                               <%-- Area Name :--%>
                            </td>
                            <td>
                                <%--<asp:RadioButton ID="chkECA" Text="ECA 1.0 S" GroupName="AN" runat="server" />
                                <asp:RadioButton ID="chkCA" Text="ECA .5 S" GroupName="AN" runat="server" />
                                <asp:RadioButton ID="chkEU" Text="EU 0.1 S" GroupName="AN" runat="server" />--%>
                            </td>                            
                            
                        </tr>
                   </table>
            </td>
        </tr>

</table>
<div class="div1">
    Charter Party Details 
</div>
<table cellpadding="4" cellspacing="0" border="0" width="100%" >
        <tr>
            <td style="background-color:#ffffff; border:solid 1px #006B8F;">
                <table cellpadding="0" cellspacing="3" border="0" width="100%" >
                        <tr>
                            <td >Charterer Name :</td>
                            <td colspan="3" >
                                <asp:TextBox runat="server" ID="txtChartererName" style="width:98%;"  ></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td style="width:220px">Charter Party Speed :</td>
                            <td style="width:400px">
                                <asp:TextBox runat="server" ID="txtCharterPartySpeed" style="width:40px;text-align:right;" MaxLength="4" onkeypress="FloatValueOnly(this)" ></asp:TextBox>                                
                                <span class="unit">KTS</span>
                            </td>
                            <td style="width:250px">Voy. Order Speed :</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtVoyOrderSpeed" style="width:40px;text-align:right;" MaxLength="4" onkeypress="FloatValueOnly(this)" ></asp:TextBox>
                                <span class="unit">KTS</span>
                            </td>
                        </tr>

                        <tr>
                            <td>Voy. Instructions :</td>
                            <td colspan="3" >
                                <asp:TextBox runat="server" ID="txtVoyInstructions" TextMode="MultiLine" Height="70px" Width="98%"  style="text-align:left;" ></asp:TextBox>
                            </td>
                        </tr>
                    </table>
            </td>
        </tr>
</table>

<div class="div1">
    Departure Information
</div>
<table cellpadding="4" cellspacing="0" border="0" width="100%" >
        <tr>
            <td style="background-color:#ffffff; border:solid 1px #006B8F;">
                <table cellpadding="0" cellspacing="3" border="0" width="100%" >
                        <tr>
                            <td style="width:220px;">Voy. Condition :</td>
                            <td style="width:400px" >
                                <asp:DropDownList  ID="ddlDepVoyCondition" runat="server" Width="70px">
                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Laden" Value="Laden"></asp:ListItem>
                                    <asp:ListItem Text="Ballast" Value="Ballast"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator3" ControlToValidate="ddlDepVoyCondition" ErrorMessage="Voy. Condition is required." Text="*"></asp:RequiredFieldValidator>
                            </td>
                            <td style="width:250px">
                                Dep. Port :
                            </td>
                            <td >
                                <asp:TextBox runat="server" ID="txtDepPort" ></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator4" ControlToValidate="txtDepPort" ErrorMessage="Departure port is required." Text="*"></asp:RequiredFieldValidator>
                            </td>
                            
                        </tr>                        

                        <tr>
                            <td >Arrival Port :</td>
                            <td style="text-align:left;">
                                <asp:TextBox runat="server" ID="txtDepArrivalPort" ></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator5" ControlToValidate="txtDepArrivalPort" ErrorMessage="Arrival port is required." Text="*"></asp:RequiredFieldValidator>
                            </td>
                            <td >COSP Date & Time :</td>
                            <td >
                                <asp:TextBox runat="server" ID="txtCOSPDate" style="width:100px;text-align:center; " MaxLength="11" ></asp:TextBox>
                                <asp:CalendarExtender runat="server" id="CalendarExtender2" TargetControlID="txtCOSPDate" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator6" ControlToValidate="txtCOSPDate" ErrorMessage="COSP Date is required." Text="*"></asp:RequiredFieldValidator>

                                <asp:DropDownList runat="server" ID="ddlCOSPHours" Width="50px">
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
                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator7" ControlToValidate="ddlCOSPHours" ErrorMessage="COSP Date (HRS) is required." Text="*"></asp:RequiredFieldValidator>
                            <span class="unit">( Hrs )</span>
                             <asp:DropDownList runat="server" ID="ddlCOSPMin" Width="50px">
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
                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator8" ControlToValidate="ddlCOSPMin" ErrorMessage="COSP Date (MIN) is required." Text="*"></asp:RequiredFieldValidator>
                            <span class="unit">( Min )</span>
                            <span style="font-size:10px; font-style:italic; display:inline;"> (Ship's LT)</span>

                            </td>
                        </tr>

                        <tr>
                            <td>Arrival Port ETA :</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtArrivalPortETA" style="width:100px;text-align:center; " MaxLength="11" ></asp:TextBox>
                                <asp:CalendarExtender runat="server" id="CalendarExtender3" TargetControlID="txtArrivalPortETA" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator9" ControlToValidate="txtArrivalPortETA" ErrorMessage="Arrival Port ETA is required." Text="*"></asp:RequiredFieldValidator>

                                <asp:DropDownList runat="server" ID="ddlArrivalPortETAH" Width="50px">
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
                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator10" ControlToValidate="ddlArrivalPortETAH" ErrorMessage="Arrival Port ETA(HRS) is required." Text="*"></asp:RequiredFieldValidator>
                            <span class="unit">( Hrs )</span>
                             <asp:DropDownList runat="server" ID="ddlArrivalPortETAMin" Width="50px">
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
                            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator11" ControlToValidate="ddlArrivalPortETAMin" ErrorMessage="Arrival Port ETA(MIN) is required." Text="*"></asp:RequiredFieldValidator>
                            <span class="unit">( Min )</span>
                            <span style="font-size:10px; font-style:italic; display:inline;"> (Ship's LT)</span>
                            </td>
                            <td>
                                Draft(Fwd) :
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtDepDraftFwd" onkeypress="FloatValueOnly(this)" style="width:100px;text-align:right; " MaxLength="4" ></asp:TextBox>                                
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator12" ControlToValidate="txtDepDraftFwd" ErrorMessage="Draft(Fwd) is required." Text="*"></asp:RequiredFieldValidator>
                                <span class="unit">MTRS</span>
                            </td>
                        </tr>

                        <tr>
                            <td>
                                Draft (Aft) :
                            </td>
                            <td>                                
                                <asp:TextBox runat="server" ID="txtDepDraftAft" onkeypress="FloatValueOnly(this)" style="width:100px;text-align:right; " MaxLength="4" ></asp:TextBox>                                
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator13" ControlToValidate="txtDepDraftAft" ErrorMessage="Draft(Aft) is required." Text="*"></asp:RequiredFieldValidator>
                                <span class="unit">MTRS</span>
                            </td>
                            <td>
                                Distance To Go :
                            </td> 
                            <td>
                                <asp:TextBox runat="server" ID="txtDepDistanceToGo" onkeypress="FloatValueOnly(this)" style="width:100px;text-align:right; " MaxLength="6" ></asp:TextBox>                                
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator14" ControlToValidate="txtDepDistanceToGo" ErrorMessage="Distance To Go is required." Text="*"></asp:RequiredFieldValidator>
                                <span class="unit">NM</span>
                            </td>
                        </tr>

                    </table>
                    <table width="100%" >
                        <tr>
                            <td >
                                <table width="100%" cellspacing="0"  >                                
                                <tr>
                                    <td style="width:220px;">Arrival Port Agent :</td>
                                    <td >
                                        <asp:TextBox ID="txtArrivalPortAgent" runat="server" Width="98%" />
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td>Personal Incharge :</td>
                                    <td>
                                        <asp:TextBox ID="txtPersonalIncharge" runat="server" Width="98%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top">Address & Contact Details :</td>
                                    <td>
                                        <asp:TextBox ID="txtAddressContactDetails" TextMode="MultiLine" runat="server" Height="75px" Width="98%" style="text-align:left;" ></asp:TextBox>
                                    </td>
                                </tr>
                        </table>
                            </td> 
                            <%--<td style="vertical-align:top;" >
                               
                            </td>--%>
                        </tr>
                    </table>
                     <div style="background-color:#c2c2c2; color:White;padding:6px; text-align:left;">
                        Next Three Port calls(If Known)
                     </div>
                     <table  width="100%" style="border:solid 1px #c2c2c2;" cellpadding="3" cellspacing="0" >
                                    
                                    <%--<tr style="background-color:#c2c2c2;">
                                        <td colspan="2" style="padding:1px;">Next Three Port calls(If Known)</td>
                                    </tr>--%>
                                    <tr>
                                        <td style="width:220px">
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Port
                                        </td>
                                        <td>
                                            Port ETA
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            1.&nbsp; <asp:TextBox ID="txtPort1" runat="server"   /> 
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPortETA1" MaxLength="11"  runat="server" />
                                            <asp:CalendarExtender runat="server" id="CalendarExtender4" TargetControlID="txtPortETA1" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            2.&nbsp; <asp:TextBox ID="txtPort2" runat="server"   />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPortETA2"  MaxLength="11"   runat="server" />
                                            <asp:CalendarExtender runat="server" id="CalendarExtender5" TargetControlID="txtPortETA2" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            3.&nbsp; <asp:TextBox ID="txtPort3"  runat="server" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPortETA3" MaxLength="11"  runat="server" />
                                            <asp:CalendarExtender runat="server" id="CalendarExtender6" TargetControlID="txtPortETA3" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                            
                                        </td>
                                    </tr>
                                </table>
            </td>
        </tr>
</table>
<div class="div1">
    Weather Information 
</div>
<table cellpadding="4" cellspacing="0" border="0" width="100%" >
        <tr>
            <td style="background-color:#ffffff; border:solid 1px #006B8F;">
                
             <table width="100%">
                    <tr>
                        <td width="600px">
                            <table cellpadding="0" cellspacing="3" border="0" width="100%" >
                            <col width="220px" />
                                <tr>
                                    <td>
                                        Course(T) :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtCource" onkeypress="IntValueOnly(event);;MaxInt360(this)" MaxLength="3" ></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator15" ControlToValidate="txtCource" ErrorMessage="Course(T) is required." Text="*"></asp:RequiredFieldValidator> 
                                        <span class="unit">Deg</span>
                                        <span class="range">&nbsp;(0-360) </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Wind Direction(T) : 
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtWindDirection" onkeypress="IntValueOnly(event);;MaxInt360(this)" MaxLength="3" ></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator16" ControlToValidate="txtWindDirection" ErrorMessage="Wind Direction(T) is required." Text="*"></asp:RequiredFieldValidator>
                                        <span class="unit">Deg</span>
                                        <span class="range">&nbsp;(0-360) </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                         Wind Force :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlWindForce" runat="server" >
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                            <asp:ListItem Text="0" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                            <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                            <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                            <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                            <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                        </asp:DropDownList> 
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator17" ControlToValidate="ddlWindForce" ErrorMessage="Wind Force is required." Text="*"></asp:RequiredFieldValidator>
                                        <span class="unit">BF</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Sea Direction(T) : 
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtSeaDirection" onkeypress="IntValueOnly(event);;MaxInt360(this)" MaxLength="3" ></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator18" ControlToValidate="txtWindDirection" ErrorMessage="Sea Direction(T) is required." Text="*"></asp:RequiredFieldValidator>
                                        <span class="unit">Deg</span>
                                        <span class="range">&nbsp;(0-360) </span>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Sea State :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSeaState" runat="server" >
                                            <asp:ListItem Text="" Value=""></asp:ListItem>
                                            <asp:ListItem Text="0" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                            <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                            <asp:ListItem Text="9" Value="9"></asp:ListItem>                                            
                                        </asp:DropDownList> 
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator19" ControlToValidate="ddlSeaState" ErrorMessage="Sea State is required." Text="*"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Current Direction(T) :
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtCurrentDirection" onkeypress="IntValueOnly(event);;MaxInt360(this)" MaxLength="3" ></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator20" ControlToValidate="txtCurrentDirection" ErrorMessage="Current Direction(T) is required." Text="*"></asp:RequiredFieldValidator>
                                        <span class="unit">Deg</span>
                                        <span class="range">&nbsp;(0-360) </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Current Strength : 
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtCurrentStrength" onkeypress="IntValueOnly(event);" MaxLength="3" ></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator21" ControlToValidate="txtCurrentStrength" ErrorMessage="Current Strength is required." Text="*"></asp:RequiredFieldValidator>
                                        <span class="unit">KTS</span>  
                                    </td>
                                </tr>
                             </table> 
                        </td>
                        <td style="vertical-align:top;"   >
                            <table width="100%">
                                <tr>
                                    <td>
                                        Remarks : ( If Any )
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtWeatherRemarks" runat="server" TextMode="MultiLine" Height="175px" Width="98%" ></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                
            </td>
        </tr>
</table>

<div class="div1">
    Ship Position
</div>
<table cellpadding="4" cellspacing="0" border="0" width="100%" >
        <tr>
            <td style="background-color:#ffffff; border:solid 1px #006B8F;">
                <table cellpadding="0" cellspacing="3" border="0" width="100%" >
                    <col width="220px" />
                    <col width="400px" />
                    <col width="250px" />
                    <col  />
                        <tr>
                            <td>Lattitude :</td>
                            <td>
                                
                                <asp:DropDownList ID="ddlLattitude1" AppendDataBoundItems="true" runat="server">                                   
                                    <asp:ListItem Text="" Value=""> </asp:ListItem>
                                </asp:DropDownList>
                                
                                <asp:DropDownList ID="ddlLattitude2" AppendDataBoundItems="true" runat="server">                                 
                                    <asp:ListItem Text="" Value=""> </asp:ListItem>
                                </asp:DropDownList>
                                
                                <asp:DropDownList ID="ddlLattitude3" runat="server">
                                    <asp:ListItem Text="" Value=""> </asp:ListItem>
                                    <asp:ListItem Text="" Value="N">N</asp:ListItem>
                                    <asp:ListItem Text="" Value="S">S</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                Longitude :
                                                            
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlLongitude1" AppendDataBoundItems="true" runat="server">
                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                </asp:DropDownList>
                                
                                <asp:DropDownList ID="ddlLongitude2" AppendDataBoundItems="true" runat="server">
                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                </asp:DropDownList>
                                
                                <asp:DropDownList ID="ddlLongitude3" runat="server">
                                    <asp:ListItem Text="" Value=""> </asp:ListItem>
                                    <asp:ListItem Text="" Value="E">E</asp:ListItem>
                                    <asp:ListItem Text="" Value="W">W</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            
                        </tr>
                        <tr>
                            <td>
                                Location Description :
                                
                            </td>
                            <td colspan="3">
                                <asp:TextBox ID="txtLocationDescription" runat="server" Height="70px" Width="98%" TextMode="MultiLine" ></asp:TextBox>
                            </td>
                                                       
                        </tr>
                    </table>

            </td>
        </tr>
</table>
<div>
<div class="div1" style="float:left">
    ROB & Recieved 
    
</div>
<div style="float:left; vertical-align:middle; padding-top:13px;">
&nbsp;<asp:Label runat="server" ForeColor="Red" ID="lblFirstMsg" Font-Italic="true" Text=" Note : ( This is your first departure report in the system . Please enter the correct ROB qty form this port. ) "></asp:Label>
</div>
</div>
<table cellpadding="4" cellspacing="0" border="0" width="100%" >
        <tr>
            <td style="background-color:#ffffff; border:solid 1px #006B8F;">

<div class="div1">
    Fuel
</div>
<table cellpadding="4" cellspacing="0" border="0" width="100%" >        
        <tr>
            <td style="background-color:#ffffff; border:solid 1px #c2c2c2;">
                   <table cellpadding="0" cellspacing="3" border="0" width="100%" >
                        <tr>
                            <td style="width:220px"></td>
                            <td style="width:250px">Recieved <span style="font-size:9px; font-style:italic;"> ( At This Port )</span>  </td>                            
                            <td style="width:400px">ROB</td>
                            <td >&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                IFO(<= 3.5% Sulphur) :
                            </td>
                            <td>
                                <asp:TextBox ID="txtRobIFO45Recv" onkeypress="FloatValueOnly(this)" MaxLength="16" runat="server" />
                                 <span class="unit"> MT</span>    
                            </td>
                            <td>
                                <asp:HiddenField ID="hfdRobIFO45" runat="server" />
                                <asp:TextBox ID="txtRobIFO45" onkeypress="FloatValueOnly(this)" MaxLength="16" runat="server" ReadOnly="true" />
                                <asp:HiddenField ID="hfdRobIFO45_S" runat="server" />
                                <span class="unit"> MT</span>
                            </td>
                            <td>&nbsp;</td>
                            
                        </tr>
                        <tr style="display:none;">
                            <td>
                                IFO(< 1% Sulphur) :
                            </td>
                            <td>
                                <asp:TextBox ID="txtRobIFO1Recv" onkeypress="FloatValueOnly(this)" MaxLength="16" runat="server" />
                                 <span class="unit"> MT</span>
                            </td>
                            <td>
                                <asp:HiddenField ID="hfdRobIFO1" runat="server" />
                                <asp:TextBox ID="txtRobIFO1"  onkeypress="FloatValueOnly(this)" MaxLength="16" runat="server" ReadOnly="true" />
                                <asp:HiddenField ID="hfdRobIFO1_S" runat="server" />
                                <span class="unit"> MT</span>
                            </td>
                            
                            <td>&nbsp;</td>
                            
                        </tr>
                        <tr style="display:none;">
                            <td>
                                MGO(<0.50% Sulphur) :
                            </td>
                            <td>
                                <asp:TextBox ID="txtRobMGO5Recv" onkeypress="FloatValueOnly(this)" MaxLength="16" runat="server" />
                                 <span class="unit"> MT</span>
                            </td>
                            <td>
                                <asp:HiddenField ID="hfdRobMGO5" runat="server" />
                                <asp:TextBox ID="txtRobMGO5"  onkeypress="FloatValueOnly(this)" MaxLength="16" runat="server" ReadOnly="true" />
                                <asp:HiddenField ID="hfdRobMGO5_S" runat="server" />
                                 <span class="unit"> MT</span>
                            </td>
                            
                            <td>&nbsp;</td>
                            
                        </tr>
                        <tr>
                            <td >
                                MGO(<0.10% Sulphur) :
                            </td>
                           <td >
                                <asp:TextBox ID="txtRobMGO1Recv" onkeypress="FloatValueOnly(this)" MaxLength="16" runat="server" />
                                 <span class="unit"> MT</span>
                            </td>
                             <td >
                                <asp:HiddenField ID="hfdRobMGO1" runat="server" />
                                <asp:TextBox ID="txtRobMGO1" onkeypress="FloatValueOnly(this)" MaxLength="16" runat="server" ReadOnly="true" />
                                <asp:HiddenField ID="hfdRobMGO1_S" runat="server" />
                                <span class="unit"> MT</span>
                            </td>
                            
                            <td ></td>
                            
                        </tr>
                        <tr>
                            <td>
                                MDO :
                            </td>
                           <td>
                                <asp:TextBox ID="txtRobMDORecv" onkeypress="FloatValueOnly(this)" MaxLength="16" runat="server" />
                                 <span class="unit"> MT</span>
                            </td>
                             <td>
                                <asp:HiddenField ID="hfdRobMDO" runat="server" />
                                <asp:TextBox ID="txtRobMDO" onkeypress="FloatValueOnly(this)" MaxLength="16" runat="server" ReadOnly="true" />
                                <asp:HiddenField ID="hfdRobMDO_S" runat="server" />
                                 <span class="unit"> MT</span>
                            </td>
                            
                            <td></td>
                            
                        </tr>
                     </table>
             </td>
          </tr>
</table>
</td>
</tr>

<tr>
            <td style="background-color:#ffffff; border:solid 1px #006B8F;">
<div class="div1">
    Lube
</div>
<table cellpadding="4" cellspacing="0" border="0" width="100%" >        
        <tr>
            <td style="background-color:#ffffff; border:solid 1px #c2c2c2;">
                   <table cellpadding="0" cellspacing="3" border="0" width="100%" >
                        <tr>
                            <td style="width:220px"></td>
                            <td style="width:250px">Recieved <span style="font-size:9px; font-style:italic;"> ( At This Port )</span></td>
                            <td style="width:400px">ROB</td>
                            <td >&nbsp;</td>
                        </tr>
                         <tr>
                            <td>MECC :</td>
                            <td>
                                <asp:TextBox  ID="txtRobMECCRecv" type="text" onkeypress="FloatValueOnly(this)" MaxLength="16" runat="server" />
                                <span class="unit"> LTR</span>
                            </td>
                            <td>
                                <asp:HiddenField ID="hfdRobMECC" runat="server" />
                                <asp:TextBox  ID="txtRobMECC" onkeypress="FloatValueOnly(this)" MaxLength="16" runat="server" ReadOnly="true" />
                                <asp:HiddenField ID="hfdRobMECC_S" runat="server" />
                                <span class="unit"> LTR</span>
                            </td>
                            
                            <td></td>
                            
                          </tr>
                            <tr>
                                <td>MECYL :
                            </td>
                               <td>
                                    <asp:TextBox  ID="txtRobMECYLRecv" type="text" onkeypress="FloatValueOnly(this)" MaxLength="16" runat="server" />
                                 <span class="unit"> LTR</span>
                                </td>
                                     <td>
                                <asp:HiddenField ID="hfdRobMECYL" runat="server" />
                                <asp:TextBox  ID="txtRobMECYL" type="text" onkeypress="FloatValueOnly(this)" MaxLength="16" runat="server" ReadOnly="true" />
                                <asp:HiddenField ID="hfdRobMECYL_S" runat="server" />
                                 <span class="unit"> LTR</span>
                            </td>
                            
                                <td></td>
                                
                            </tr>
                            <tr>
                                <td>AECC :
                            </td>
                               <td>
                                    <asp:TextBox  ID="txtRobAECCRecv" type="text" onkeypress="FloatValueOnly(this)" MaxLength="16" runat="server" />
                                    <span class="unit"> LTR</span>
                                </td>
                                <td>
                                <asp:HiddenField ID="hfdRobAECC" runat="server" />
                                <asp:TextBox  ID="txtRobAECC" type="text" onkeypress="FloatValueOnly(this)" MaxLength="16" runat="server" ReadOnly="true" />
                                <asp:HiddenField ID="hfdRobAECC_S" runat="server" />
                                <span class="unit"> LTR</span>
                            </td>
                             
                                <td></td>
                                
                            </tr>
                            <tr>
                                <td>HYD :
                            </td>
                                <td>
                                    <asp:TextBox  ID="txtRobHYDRecv" type="text" onkeypress="FloatValueOnly(this)" MaxLength="16" runat="server" />
                                    <span class="unit"> LTR</span>
                                </td>
                                <td>
                                <asp:HiddenField ID="hfdRobHYD" runat="server" />
                                <asp:TextBox  ID="txtRobHYD" type="text" onkeypress="FloatValueOnly(this)" MaxLength="16" runat="server" ReadOnly="true" />
                                <asp:HiddenField ID="hfdRobHYD_S" runat="server" />
                                 <span class="unit"> LTR</span> 
                                 </td>
                              
                                <td></td>
                                
                            </tr>
                            <tr style="display:none;">
                                <td>MDO :</td>
                                 <td>
                                    <asp:TextBox  ID="txtRobMDOLubeRecv" type="text" onkeypress="FloatValueOnly(this)" MaxLength="16" runat="server" />
                                    <span class="unit"> LTR</span>
                                </td>
                                <td>
                                <asp:HiddenField ID="hfdRobMDOLube" runat="server" />
                                <asp:TextBox  ID="txtRobMDOLube" type="text" onkeypress="FloatValueOnly(this)" MaxLength="16" runat="server" ReadOnly="true" />
                                <asp:HiddenField ID="hfdRobMDOLube_S" runat="server" />
                                 <span class="unit"> LTR</span>
                            </td>
                            
                                <td>&nbsp;</td>
                                
                            </tr>

                    </table>
            </td>
        </tr>
</table>

</td>
</tr>

<tr>
            <td style="background-color:#ffffff; border:solid 1px #006B8F;">
<div class="div1">
   Fresh Water 
</div>
<table cellpadding="4" cellspacing="0" border="0" width="100%" >        
        <tr>
            <td style="background-color:#ffffff; border:solid 1px #c2c2c2;">
                   <table cellpadding="0" cellspacing="3" border="0" width="100%" >
                        <tr>
                            <td style="width:220px"></td>
                            <td style="width:250px">Recieved <span style="font-size:9px; font-style:italic;"> ( At This Port )</span></td>                            
                            <td style="width:400px">ROB</td>
                            <td >&nbsp;</td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                
                                <asp:TextBox  ID="txtRobFesshWaterRecv" onkeypress="FloatValueOnly(this)" MaxLength="16" runat="server" />
                                <span class="unit"> MT</span>
                            </td>
                            <td>
                                <asp:HiddenField ID="hfdRobFesshWater" runat="server" />
                                <asp:TextBox  ID="txtRobFesshWater" onkeypress="FloatValueOnly(this)" MaxLength="16" runat="server" ReadOnly="true" />
                                <asp:HiddenField ID="hfdRobFesshWater_S" runat="server" />
                                <span class="unit"> MT</span>
                            </td>
                           
                            <td>
                            </td>
                        </tr>
                    </table>
            </td>
        </tr>
</table>
</td>
</tr>
</table>

<div class="div1">
   Cargo & Ballast Weight 
</div>
<table cellpadding="4" cellspacing="0" border="0" width="100%" >        
        <tr>
            <td style="background-color:#ffffff; border:solid 1px #006B8F;">
                   <table cellpadding="0" cellspacing="3" border="0" width="100%" >
                        <tr>
                            <td style="width:220px">Total Cargo Weight :</td>
                            <td style="width:400px">
                                <asp:TextBox  ID="txtTotalCargoWeight" onkeypress="FloatValueOnly(this)" MaxLength="16" runat="server" />
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator22" ControlToValidate="txtTotalCargoWeight" ErrorMessage="Total Cargo Weight is required." Text="*"></asp:RequiredFieldValidator>
                                <span class="unit"> MT</span>
                            </td>
                            <td style="width:250px">Ballast Weight :</td>
                            <td >
                                <asp:TextBox  ID="txtBallastWeight" onkeypress="FloatValueOnly(this)" MaxLength="16" runat="server" />
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator23" ControlToValidate="txtBallastWeight" ErrorMessage="Ballast Weight is required." Text="*"></asp:RequiredFieldValidator>
                                <span class="unit"> MT</span>
                            </td>
                        </tr>
                    </table>
            </td>
        </tr>
</table>

<div>
<div class="div1" style="float:left">
    Total Container Unit    
</div>
<div style="float:left; vertical-align:middle; padding-top:13px;">
&nbsp;<asp:Label runat="server" ForeColor="Red" ID="Label1" Font-Italic="true" Text=" ( Only for Container Vessel ) "></asp:Label>
</div>
</div>

<table cellpadding="4" cellspacing="0" border="0" width="100%" style="background-color:#FFFFFF; border:solid 1px #006B8F;">
    <col width="220px" />       
    <col width="400px" />
    <col width="250px" />    
    <col/>    
    <tr>
            <td>20Ft-Laden :</td>
            <td>
                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtTCU20L" MaxLength="16" style="text-align:right;"></asp:TextBox>
            </td>
            <td>20Ft-Empty :</td>
            <td>
                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtTCU20E" MaxLength="16" style="text-align:right;"></asp:TextBox>
            </td>            
    </tr>
    <tr>
           <td>40Ft-Laden :</td>
            <td>
                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtTCU40L" MaxLength="16" style="text-align:right;"></asp:TextBox>
            </td>

            <td>40Ft-Empty :</td>
            <td>
                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtTCU40E" MaxLength="16" style="text-align:right;"></asp:TextBox>
            </td>
            
            
    </tr>
    <tr>
            <td>45Ft-Laden :</td>
            <td>
                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtTCU45L" MaxLength="16" style="text-align:right;"></asp:TextBox>
            </td>           
            
            <td>45Ft-Empty :</td>
            <td>
                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtTCU45E" MaxLength="16" style="text-align:right;"></asp:TextBox>
            </td>
    </tr>
</table>
<br /><br /><br /><br /><br /><br /><br /><br /><br />
</div>

<div class="stickyFooter">
<div>
    <asp:ValidationSummary runat="server" ID="v1" class="validationsummary"/>
</div>

  <div style="text-align:right; width:98%" >
   <asp:Label runat="server" ID="lblMessage" ForeColor="Red" style="float:left" Font-Bold="true" Font-Size="Large"></asp:Label>
     <asp:Button ID="btnSave" CssClass="btn" OnClick="btnSave_Click" Text="Save Report" Width="120px" runat="server" />
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

        $("#txtRobIFO45Recv").keyup(function () {
            var tmp = ConvertToDec($("#hfdRobIFO45").val()) + ConvertToDec($(this).val());
            tmp = roundToTwo(tmp);
            $("#txtRobIFO45").val(tmp);
            $("#hfdRobIFO45_S").val(tmp);
            CheckNegativeValue($("#txtRobIFO45"));
        });

        $("#txtRobIFO1Recv").keyup(function () {
            var tmp = ConvertToDec($("#hfdRobIFO1").val()) + ConvertToDec($(this).val());
            tmp = roundToTwo(tmp);
            $("#txtRobIFO1").val(tmp);
            $("#hfdRobIFO1_S").val(tmp);
            CheckNegativeValue($("#txtRobIFO1"));
        });

        $("#txtRobMGO5Recv").keyup(function () {
            var tmp = ConvertToDec($("#hfdRobMGO5").val()) + ConvertToDec($(this).val());
            tmp = roundToTwo(tmp);
            $("#txtRobMGO5").val(tmp);
            $("#hfdRobMGO5_S").val(tmp);
            CheckNegativeValue($("#txtRobMGO5"));
        });

        $("#txtRobMGO1Recv").keyup(function () {
            var tmp = ConvertToDec($("#hfdRobMGO1").val()) + ConvertToDec($(this).val());
            tmp = roundToTwo(tmp);
            $("#txtRobMGO1").val(tmp);
            $("#hfdRobMGO1_S").val(tmp);
            CheckNegativeValue($("#txtRobMGO1"));
        });

        $("#txtRobMDORecv").keyup(function () {
            var tmp = ConvertToDec($("#hfdRobMDO").val()) + ConvertToDec($(this).val());
            tmp = roundToTwo(tmp);
            $("#txtRobMDO").val(tmp);
            $("#hfdRobMDO_S").val(tmp);
            CheckNegativeValue($("#txtRobMDO"));
        });

        $("#txtRobMECCRecv").keyup(function () {
            var tmp = ConvertToDec($("#hfdRobMECC").val()) + ConvertToDec($(this).val());
            tmp = roundToTwo(tmp);
            $("#txtRobMECC").val(tmp);
            $("#hfdRobMECC_S").val(tmp);
            CheckNegativeValue($("#txtRobMECC"));
        });

        $("#txtRobMECYLRecv").keyup(function () {
            var tmp = ConvertToDec($("#hfdRobMECYL").val()) + ConvertToDec($(this).val());
            tmp = roundToTwo(tmp);
            $("#txtRobMECYL").val(tmp);
            $("#hfdRobMECYL_S").val(tmp);
            CheckNegativeValue($("#txtRobMECYL"));
        });

        $("#txtRobAECCRecv").keyup(function () {
            var tmp = ConvertToDec($("#hfdRobAECC").val()) + ConvertToDec($(this).val());
            tmp = roundToTwo(tmp);
            $("#txtRobAECC").val(tmp);
            $("#hfdRobAECC_S").val(tmp);
            CheckNegativeValue($("#txtRobAECC"));
        });

        $("#txtRobHYDRecv").keyup(function () {
            var tmp = ConvertToDec($("#hfdRobHYD").val()) + ConvertToDec($(this).val());
            tmp = roundToTwo(tmp);
            $("#txtRobHYD").val(tmp);
            $("#hfdRobHYD_S").val(tmp);
            CheckNegativeValue($("#txtRobHYD"));
        });

        $("#txtRobMDOLubeRecv").keyup(function () {
            var tmp = ConvertToDec($("#hfdRobMDOLube").val()) + ConvertToDec($(this).val());
            tmp = roundToTwo(tmp);
            $("#txtRobMDOLube").val(tmp);
            $("#hfdRobMDOLube_S").val(tmp);
            CheckNegativeValue($("#txtRobMDOLube"));
        });

        $("#txtRobFesshWaterRecv").keyup(function () {
            var tmp = ConvertToDec($("#hfdRobFesshWater").val()) + ConvertToDec($(this).val());
            tmp = roundToTwo(tmp);
            $("#txtRobFesshWater").val(tmp);
            $("#hfdRobFesshWater_S").val(tmp);
            CheckNegativeValue($("#txtRobFesshWater"));
        });
    }
    $(document).ready(function () 
    {
        Page_CallAfterRefresh();
    }
    );


    
</script>
</form>
</body>
</html>
