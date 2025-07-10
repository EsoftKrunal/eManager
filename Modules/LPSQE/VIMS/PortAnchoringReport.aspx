<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PortAnchoringReport.aspx.cs" Inherits="PortAnchoringReport"  Async="true" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Port Anchoring Report </title>
  <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />

  <script type="text/javascript" src="../eReports/JS/jquery.min.js"></script>
  <script src="../eReports/JS/AutoComplete/knockout-2.2.1.js" type="text/javascript"></script>
  <script type="text/javascript" src="../eReports/JS/KPIScript.js"></script>
  
  <link rel="stylesheet" href="../eReports/JS/AutoComplete/jquery-ui.css" />
  <script src="../eReports/JS/AutoComplete/jquery-ui.js" type="text/javascript"></script>

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
<script type="text/javascript">
         function RegisterAutoComplete() {
             $(function () {
                 function log(message) {
//                     $("<div>").text(message).prependTo("#log");
//                     $("#log").scrollTop(0);
                 }

                 $("#txtArrivalPort").autocomplete({
                     source: function (request, response) {
                         $.ajax({
                             url: getBaseURL() + "/eReports/getautocompletedata.ashx",
                             dataType: "json",
                             headers: { "cache-control": "no-cache" },
                             type: "POST",
                             data: { Key: $("#txtArrivalPort").val(), Type: "PORT" },
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
<div style="background-color:#006B8F; color:White;padding:8px; font-size:14px;text-align:center;">Port Anchoring Report</div>
<div style="padding-left:50px;padding-right:50px;">
<div class="div1">Voyage Information :</div>
<div >
<table cellpadding="4" cellspacing="0" border="0" width="100%" >
        <tr>
            <td style="background-color:#FFFFFF; border:solid 1px #006B8F;">
                   <table cellpadding="0" cellspacing="3" border="0" width="100%" >
                        <tr>
                            <td style="width:180px">Vessel Code :</td>
                            <td style="width:320px">
                                <asp:TextBox runat="server" ID="txtVesselCode" style="width:40px;text-align:center;" MaxLength="3" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="text-align:right; width:250px">Report Date :</td>
                            <td >
                                <asp:TextBox runat="server" ID="txtRDate" style="width:80px;text-align:center; " MaxLength="15" ></asp:TextBox>
                                <asp:CalendarExtender runat="server" id="Cal1" TargetControlID="txtRDate" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator runat="server" id="rec1" ControlToValidate="txtRDate" ErrorMessage="Report Date is required." Text="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:180px">Voyage #:</td>
                            <td style="width:320px">
                                <asp:TextBox runat="server" ID="txtVoyageNumber" style="width:200px;text-align:left; " MaxLength="50" ></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" ControlToValidate="txtVoyageNumber" ErrorMessage="Voyage # is required." Text="*"></asp:RequiredFieldValidator>
                            </td>
                            <td style="text-align:right; width:250px">Arrival Port :
                                                          
                           </td>
                            <td >
                                <asp:TextBox ID="txtArrivalPort" runat="server" MaxLength="50" style="text-transform:uppercase;width:200px;"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtArrivalPort" ErrorMessage="Arrival Port is required." Text="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:180px">POB ( if any ) :</td>
                            <td style="width:320px">
                                <asp:TextBox runat="server" ID="txtPOB" style="width:200px;text-align:left; " MaxLength="100" ></asp:TextBox>
                            </td>
                            <td style="text-align:right; width:250px">Anchoring Reason :</td>
                            <td >
                                <asp:TextBox runat="server" ID="txtAnchoragereasion" style="width:200px;text-align:left; " MaxLength="100" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtArrivalPort" ErrorMessage="Anchoring Reason is required." Text="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:180px">
                                Let Go Anchor :</td>
                            <td style="width:320px">
                                <asp:TextBox runat="server" ID="txtLetGoAnchorage" style="width:80px;text-align:center; " MaxLength="15" ></asp:TextBox>
                                <asp:CalendarExtender runat="server" id="txtLetGoAnchorage_CalendarExtender" TargetControlID="txtLetGoAnchorage" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator40" ControlToValidate="txtLetGoAnchorage" ErrorMessage="Let Go Anchor is required." Text="*"></asp:RequiredFieldValidator>
                                
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
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator43" ControlToValidate="ddlLetGoAnchorageHH" ErrorMessage="Let Go Anchor Hrs is required." Text="*"></asp:RequiredFieldValidator>
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
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator44" ControlToValidate="ddlLetGoAnchorageMM" ErrorMessage="Let Go Anchor Min is required." Text="*"></asp:RequiredFieldValidator>
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
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator47" 
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
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator48" 
                                    ControlToValidate="ddlpilotAwayMM" ErrorMessage="Pilot Away Min is required." Text="*"></asp:RequiredFieldValidator>
                                <span class="unit">( Min )</span> 
                                </td>
                        </tr>
                        <tr>
                            <td style="width:180px">
                                ETB Date :</td>
                            <td style="width:320px">
                                <asp:TextBox runat="server" ID="txtETBDate" 
                                    style="width:80px;text-align:center; " MaxLength="15" ></asp:TextBox>
                                <asp:CalendarExtender runat="server" id="txtETBDate_CalendarExtender" 
                                    TargetControlID="txtETBDate" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator41" 
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
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator45" ControlToValidate="ddlETBHours" ErrorMessage="ETB Hrs is required." Text="*"></asp:RequiredFieldValidator>
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
                                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator46" ControlToValidate="ddlETBMins" ErrorMessage="ETB Min is required." Text="*"></asp:RequiredFieldValidator>
                                <span class="unit">( Min )</span> 
                                </td>
                        </tr>
                        </table>
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
                                    <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator22" ControlToValidate="ddlWindForce" ErrorMessage="Wind Force is required." Text="*"></asp:RequiredFieldValidator>
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
                            <td style="width:320px">
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
                        <tr>
                            <td>
                            
                            </td>
                            <td>
                                ME Consumption</td>
                            <td>
                                AE Consumption</td>
                            <td>
                                Boiler Consumption</td>
                            <td>
                                ROB</td>
                                <td><!--ROB--></td>
                        </tr>
                        <tr>
                            <td>
                                IFO(<= 3.5% Sulphur) :
                            </td>
                            <td>
                               <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtME_IFO45" style="width:70px;text-align:right;" MaxLength="16" ></asp:TextBox>
                               <span class="unit"> MT</span>
                               </td>
                            <td>
                               <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtAE_IFO45" style="width:70px;text-align:right;" MaxLength="16" ></asp:TextBox>
                               <span class="unit"> MT</span> </td>
                            <td>
                               <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtCargoHeating_IFO45" style="width:70px;text-align:right;" MaxLength="16" ></asp:TextBox>
                               <span class="unit"> MT</span>
                            </td>
                            <td>
                                <asp:HiddenField ID="hfdRobIFO45" runat="server" />
                                <asp:TextBox ID="txtRobIFO45" runat="server" MaxLength="16" 
                                    onkeypress="FloatValueOnly(this)" ReadOnly="true" />
                                <asp:HiddenField ID="hfdRobIFO45_S" runat="server" />
                                <span class="unit">MT</span></td>
                            <td>
                                <!--<input  id="txtRobIFO45" type="text" onkeypress="FloatValueOnly(this)" maxlength="16" style="text-align:left;"/><span class="unit"> MT</span>-->
                           </td>
                        </tr>
                        <tr style="display:none;">
                            <td>
                                IFO(< 1% Sulphur) :
                            </td>
                            <td>
                                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtME_IFO1" style="width:70px;text-align:right;" MaxLength="16" ></asp:TextBox>
                                <span class="unit"> MT</span></td>
                            <td>
                                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtAE_IFO1" style="width:70px;text-align:right;" MaxLength="16" ></asp:TextBox>
                                <span class="unit"> MT</span> </td>
                            <td>
                                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtCargoHeating_IFO1" style="width:70px;text-align:right;" MaxLength="16" ></asp:TextBox>
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
                        </tr>
                        <tr style="display:none;">
                            <td>
                                MGO(<0.50% Sulphur) :
                            </td>
                            <td>
                                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtME_MGO5" style="width:70px;text-align:right;" MaxLength="16" ></asp:TextBox>
                                <span class="unit"> MT</span> </td>
                            <td>
                                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtAE_MGO5" style="width:70px;text-align:right;" MaxLength="16" ></asp:TextBox>
                                <span class="unit"> MT</span> </td>
                            <td>
                                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtCargoHeating_MGO5" style="width:70px;text-align:right;" MaxLength="16" ></asp:TextBox>
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
                        </tr>
                        <tr>
                            <td>
                                MGO(<0.10% Sulphur) :
                            </td>
                            <td>
                                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtME_MGO10" style="width:70px;text-align:right;" MaxLength="16" ></asp:TextBox>
                                <span class="unit"> MT</span> </td>
                            <td>
                                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtAE_MGO1" style="width:70px;text-align:right;" MaxLength="16" ></asp:TextBox>
                                <span class="unit"> MT</span> </td>
                            <td>
                                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtCargoHeating_MGO1" style="width:70px;text-align:right;" MaxLength="16" ></asp:TextBox>
                                <span class="unit"> MT</span>
                            </td>
                            <td>
                                <asp:HiddenField ID="hfdRobMGO1" runat="server" />
                                <asp:TextBox ID="txtRobMGO1" runat="server" MaxLength="16" 
                                    onkeypress="FloatValueOnly(this)" ReadOnly="true" />
                                <asp:HiddenField ID="hfdRobMGO1_S" runat="server" />
                                <span class="unit">MT</span></td>
                            <td>
                                <!--<input  id="txtRobMGO1" type="text" onkeypress="FloatValueOnly(this)" maxlength="16" style="text-align:left;"/>
                                <span class="unit"> MT</span>-->
                            </td>
                        </tr>
                        <tr>
                            <td>
                                MDO :
                            </td>
                            <td>
                                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtME_MDO" style="width:70px;text-align:right;" MaxLength="16" ></asp:TextBox>
                                <span class="unit"> MT</span> </td>
                            <td>
                                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtAE_MDO" style="width:70px;text-align:right;" MaxLength="16" ></asp:TextBox>
                                <span class="unit"> MT</span> </td>
                            <td>
                                <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtCargoHeating_MDO" style="width:70px;text-align:right;" MaxLength="16" ></asp:TextBox>
                                <span class="unit"> MT</span>
                            </td>
                            <td>
                                <asp:HiddenField ID="hfdRobMDO" runat="server" />
                                <asp:TextBox ID="txtRobMDO" runat="server" MaxLength="16" 
                                    onkeypress="FloatValueOnly(this)" ReadOnly="true" />
                                <asp:HiddenField ID="hfdRobMDO_S" runat="server" />
                                <span class="unit">MT</span></td>
                            <td>
                                <!--<input  id="txtRobMDOF" type="text" onkeypress="FloatValueOnly(this)" maxlength="16" style="text-align:left;"/>
                                <span class="unit"> MT</span>-->
                            </td>
                        </tr>
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
                                <col  />
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            Consumption</td>
                                        <td>ROB</td>
                                    </tr>
                                    <tr>
                                        <td>MECC :</td>
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
                                    </tr>
                                    <tr>
                                        <td>MECYL :</td>
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
                                    </tr>
                                    <tr>
                                        <td>AECC :</td>
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
                                    </tr>
                                    <tr>
                                        <td>HYD :</td>
                                        <td>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtLubeFresh_HYD" MaxLength="16" style="text-align:right;"></asp:TextBox>
                                            <span class="unit"> LTR</span>
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="hfdRobHYD" runat="server" />
                                            <asp:TextBox ID="txtRobHYD" runat="server" MaxLength="16" 
                                                onkeypress="FloatValueOnly(this)" ReadOnly="true" type="text" />
                                            <asp:HiddenField ID="hfdRobHYD_S" runat="server" />
                                            <span class="unit">LTR</span> </td>
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
                                <tr>
                            <td ></td>
                            <td >Generated</td>
                            <td >Consumed <span style="font-size:9px; font-style:italic;"> ( At This Port )</span></td>
                            <td >ROB</td>
                            
                        </tr>
                                    <tr>
                                        <td>
                                            </td>
                                        <td>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtLubeFresh_Generated" MaxLength="16" style="text-align:right;"></asp:TextBox>
                                            <span class="unit"> MT</span> </td>
                                            <td>
                                            <asp:TextBox runat="server" onkeypress="FloatValueOnly(this);" ID="txtLubeFresh_Consumed" MaxLength="16" style="text-align:right;"></asp:TextBox>
                                            <span class="unit"> MT</span>
                                            </td>
                                        <td>
                                            
                                            <asp:HiddenField ID="hfdRobFesshWater" runat="server" />
                                            <asp:TextBox ID="txtRobFesshWater" runat="server" MaxLength="16" 
                                                onkeypress="FloatValueOnly(this)" ReadOnly="true" />
                                            <asp:HiddenField ID="hfdRobFesshWater_S" runat="server" />
                                            <span class="unit">MT</span></td>
                                        
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
                                </tr>
</table>
<br /><br /><br /><br /><br /><br /><br /><br /><br />
</div>
</div>
<div style="text-align:left;" class="stickyFooter"> 
<div>
    <asp:ValidationSummary runat="server" ID="v1" CssClass="validationsummary" />
</div>
<div style="width:98%; text-align:right; ">
        <asp:Label runat="server" ID="lblMessage" ForeColor="Red" style="float:left" Font-Bold="true" Font-Size="Large"></asp:Label>
        <asp:Button ID="btnSave" CssClass="btn" runat="server" Text="Save Report" OnClick="btnSaveClick" Width="120px"/>
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

        $("#txtME_IFO45,#txtAE_IFO45,#txtCargoHeating_IFO45").keyup(function () {
            var tmp = ConvertToDec($("#hfdRobIFO45").val()) - ConvertToDec($("#txtME_IFO45").val()) - ConvertToDec($("#txtAE_IFO45").val()) - ConvertToDec($("#txtCargoHeating_IFO45").val());
            tmp = roundToTwo(tmp);
            $("#txtRobIFO45").val(tmp);
            $("#hfdRobIFO45_S").val(tmp);
            CheckNegativeValue($("#txtRobIFO45"));
        });

        $("#txtME_IFO1,#txtAE_IFO1,#txtCargoHeating_IFO1").keyup(function () {
            var tmp = ConvertToDec($("#hfdRobIFO1").val()) - ConvertToDec($("#txtME_IFO1").val()) - ConvertToDec($("#txtAE_IFO1").val()) - ConvertToDec($("#txtCargoHeating_IFO1").val());
            tmp = roundToTwo(tmp);
            $("#txtRobIFO1").val(tmp);
            $("#hfdRobIFO1_S").val(tmp);
            CheckNegativeValue($("#txtRobIFO1"));
        });

        $("#txtME_MGO5,#txtAE_MGO5,#txtCargoHeating_MGO5").keyup(function () {
            var tmp = ConvertToDec($("#hfdRobMGO5").val()) - ConvertToDec($("#txtME_MGO5").val()) - ConvertToDec($("#txtAE_MGO5").val()) - ConvertToDec($("#txtCargoHeating_MGO5").val());
            tmp = roundToTwo(tmp);
            $("#txtRobMGO5").val(tmp);
            $("#hfdRobMGO5_S").val(tmp);
            CheckNegativeValue($("#txtRobMGO5"));
        });

        $("#txtME_MGO10,#txtAE_MGO1,#txtCargoHeating_MGO1").keyup(function () {
            var tmp = ConvertToDec($("#hfdRobMGO1").val()) - ConvertToDec($("#txtME_MGO10").val()) - ConvertToDec($("#txtAE_MGO1").val()) - ConvertToDec($("#txtCargoHeating_MGO1").val());
            tmp = roundToTwo(tmp);
            $("#txtRobMGO1").val(tmp);
            $("#hfdRobMGO1_S").val(tmp);
            CheckNegativeValue($("#txtRobMGO1"));
        });

        $("#txtME_MDO,#txtAE_MDO,#txtCargoHeating_MDO").keyup(function () {
            var tmp = ConvertToDec($("#hfdRobMDO").val()) - ConvertToDec($("#txtME_MDO").val()) - ConvertToDec($("#txtAE_MDO").val()) - ConvertToDec($("#txtCargoHeating_MDO").val());
            tmp = roundToTwo(tmp);
            $("#txtRobMDO").val(tmp);
            $("#hfdRobMDO_S").val(tmp);
            CheckNegativeValue($("#txtRobMDO"));
        });

        $("#txtLubeFresh_MECC").keyup(function () {
            var tmp = ConvertToDec($("#hfdRobMECC").val()) - ConvertToDec($("#txtLubeFresh_MECC").val());
            tmp = roundToTwo(tmp);
            $("#txtRobMECC").val(tmp);
            $("#hfdRobMECC_S").val(tmp);
            CheckNegativeValue($("#txtRobMECC"));
        });

        $("#txtLubeFresh_MECYL").keyup(function () {
            var tmp = ConvertToDec($("#hfdRobMECYL").val()) - ConvertToDec($("#txtLubeFresh_MECYL").val());
            tmp = roundToTwo(tmp);
            $("#txtRobMECYL").val(tmp);
            $("#hfdRobMECYL_S").val(tmp);
            CheckNegativeValue($("#txtRobMECYL"));
        });

        $("#txtLubeFresh_AECC").keyup(function () {
            var tmp = ConvertToDec($("#hfdRobAECC").val()) - ConvertToDec($("#txtLubeFresh_AECC").val());
            tmp = roundToTwo(tmp);
            $("#txtRobAECC").val(tmp);
            $("#hfdRobAECC_S").val(tmp);
            CheckNegativeValue($("#txtRobAECC"));
        });

        $("#txtLubeFresh_HYD").keyup(function () {
            var tmp = ConvertToDec($("#hfdRobHYD").val()) - ConvertToDec($("#txtLubeFresh_HYD").val());
            tmp = roundToTwo(tmp);
            $("#txtRobHYD").val(tmp);
            $("#hfdRobHYD_S").val(tmp);
            CheckNegativeValue($("#txtRobHYD"));
        });

        $("#txtLubeFresh_Generated,#txtLubeFresh_Consumed").keyup(function () {
            var tmp = ConvertToDec($("#hfdRobFesshWater").val()) + ConvertToDec($("#txtLubeFresh_Generated").val()) - ConvertToDec($("#txtLubeFresh_Consumed").val());
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
