<%@ Page Language="C#" AutoEventWireup="true"   EnableEventValidation="false"   CodeFile="VesselContract.aspx.cs" Inherits="Transactions_InspectionPlanning" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>EMANAGER</title>
       <%--<link href="../HRD/Styles/style.css" rel="stylesheet" type="text/css" />--%>
      <link href="../HRD/Styles/mystyle.css" rel="stylesheet" type="text/css" />
    <link href="../HRD/Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../HRD/Styles/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../css/StyleSheet.css" />
    <%--<link rel="stylesheet" type="text/css" href="../../css/app_style.css" />--%>
<script type="text/javascript" src="../Scripts/main.js"></script>
<%--<script language="javascript" type="text/javascript">
month = "Jan,Feb,Mar,Apr,May,Jun,Jul,Aug,Sep,Oct,Nov,Dec".split(",");
function checkDate(theField){
  dPart = theField.value.split("-");
  if(dPart.length!=3){
    alert("Enter Date in this format: dd mmm yyyy");
    theField.focus();
    return false;
  }
var check=0;
for(i=0;i<month.length;i++){
if(dPart[1].toLowerCase()==month[i].toLowerCase()){
    
     check=1;
      dPart[1]=i;
      break;
    }
  }
  if(check==0)
  {
  alert("Enter Date in this format: dd mmm yyyy");
  return false;
  }
  nDate = new Date(dPart[2], dPart[1], dPart[0]);
 // nDate = new Date(dPart[0], dPart[1], dPart[2]);
 
  if(isNaN(nDate) || dPart[2]!=nDate.getFullYear() || dPart[1]!=nDate.getMonth() || dPart[0]!=nDate.getDate()){
    alert("Enter1 Date in this format: dd mmm yyyy");
    theField.select();
    theField.focus();
    return false;
  } else {
    return true;
  }
}
 function trimAll(sString) 
{
while (sString.substring(0,1) == ' ')
{
sString = sString.substring(1, sString.length);
}
while (sString.substring(sString.length-1, sString.length) ==' ')
{
sString = sString.substring(0,sString.length-1);
}
return sString;
}
function checkform()
{
  
  if(trimAll(document.getElementById("ddlvessel").value)=="0")
{
alert('Please Select Vessel');
document.getElementById("ddlvessel").focus();
return false;
}
if(trimAll(document.getElementById("ddlinspection").value)=="0")
{
alert('Please Select Inspection!');
document.getElementById("ddlinspection").focus();
return false;
}

 if(!checkDate(document.getElementById('txtplandate')))
    return false;
}
function check()
{
if(document.getElementById("ddlinspection").value=="0")
{
alert('Please Select Inspection First');
document.getElementById("ddlinspection").focus();

return false;

}
if(!checkDate(document.getElementById('txtplandate')))
    return false;
}
   

function AskConfirm()
{
var ss=confirm('This supt. is already assigned for an inspection for the same date, Do you still want to assign!');

if(ss==true)
{

    __doPostBack('Main','');
return true;
}
else
{

return false;
}
}
</script>--%>
    <script type="text/javascript">
        function OpenVesselContract() {
            window.open('AddEditVesselContract.aspx', '');
        }
    </script>
    
    <style type="text/css">
        .btn
        {
            border: 1px solid #fe0034;
	font-family: arial;
	font-size: 12px;
	color: #fff;
	border-radius: 3px;
	-webkit-border-radius: 3px;
	-ms-border-radius: 3px;
	background: #fe0030;
	background: linear-gradient(#ff7c96, #fe0030);
	background: -webkit-linear-gradient(#ff7c96, #fe0030);
	background: -ms-linear-gradient(#ff7c96, #fe0030);
	padding: 4px 6px;
	cursor: pointer;
        }
    </style>

     <style type="text/css">

        #tblContractDtls td, #tblContractDtls th {
            border: 1px solid black;
            border-collapse: collapse;
        }

</style>
    <style type="text/css" >
    body
    {
        font-family:Verdana;
        font-size:12px;
        margin:0px;
    }
    .pending
    {
        background-color:#f7f841;
        padding:3px;
    }

    .searchsection
    {
        display:flex;
        justify-content:space-between;
        padding:5px;
    }

    .searchsection-textalign{
        text-align:left;
    }

    .searchsection-btn {
       display:flex;
       justify-content:end;
       padding:5px;
    }

@media screen and (max-width:767px) {
    .searchsection {
        display:flex;
        flex-direction:column;
        padding:5px;
    }

    .inner-field {
        width: 100% !important;
        margin-bottom:7px;
    }

    .div_dialog {
        width: 90% !important;
    }
}

    </style>

    <script language="javascript" type="text/javascript">  
        function Validate() {
            var ddlVessel = document.getElementById("ddlvessel");
            alert(ddlVessel.value);
            var ddlContractType = document.getElementById("ddlContractType");
            var ddlCharterer = document.getElementById("ddcharterer");
            if (ddlVessel.value == "") {
                alert("Please select Vessel");
                document.getElementById("ddlvessel").focus();
                return false;
            }
            if (ddlContractType.value == "") {
                alert("Please select Contract Type");
                document.getElementById("ddlContractType").focus();
                 return false;
            }
            if (ddlCharterer.value == "") {
                alert("Please select Charterer");
                document.getElementById("ddcharterer").focus();
                return false;
            }
            if (ddlContractType.value == "1") {
                if (document.getElementById("txtFromDate").value == "")
                {
                    alert("From Date can not be blank");
                    document.getElementById("txtFromDate").focus();
                    return false;
                }
                var ddlFromHrs = document.getElementById("ddlFromHrs");
                var ddlFromMin = document.getElementById("ddlFromMin");
                if (ddlFromHrs.value == "") {
                    alert("Please select From Hrs");
                    document.getElementById("ddlFromHrs").focus();
                     return false;
                }
                if (ddlFromMin.value == "") {
                    alert("Please select From Mins");
                    document.getElementById("ddlFromMin").focus();
                     return false;
                 }
                if (document.getElementById("txtToDate").value == ""))
                {
                    alert("To Date can not be blank");
                    document.getElementById("txtToDate").focus();
                    return false;
                }

                var ddlToHrs = document.getElementById("ddlToHrs");
                var ddlToMin = document.getElementById("ddlToMins");
                if (ddlToHrs.value == "") {
                    alert("Please select To Hrs");
                    document.getElementById("ddlToHrs").focus();
                     return false;
                }
                if (ddlFromMin.value == "") {
                    alert("Please select To Mins");
                    document.getElementById("ddlToMins").focus();
                    return false;
                }
                var hiraAmount = "^[1-9]\\d*(\\.\\d +)?$";
                var txtHireAmount = document.getElementById("txtHireAmount").value; 
                if (txtHireAmount == "")
                {
                    alert("Total Hire Amount can not be blank");
                    document.getElementById("txtHireAmount").focus();
                    return false;
                }
                var hireamountmatch = txtHireAmount.match(hiraAmount);
                if (hireamountmatch == null) {
                    alert("Enter Valid Hire Amount");
                    document.getElementById("txtHireAmount").focus();
                    return false;
                }
            }

           
            return true;
        }
    </script>  

     </head>
<body  >
<form id="form1" runat="server" style="font-family:Arial;font-size:12px;">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<div >
        <center>
             <div style="background-color:#206020; width:95% ; height:3px;">
            </div>
            <br />
          
                            <asp:HiddenField id="hidval" runat="server"></asp:HiddenField>
                <table cellpadding="0" cellspacing="0" width="100%" style="background-color:#f9f9f9; border:#8fafdb 1px solid; border-top:#8fafdb 0px solid;font-family:Arial;font-size:12px;" >
                <tr>
                    <td width="65%" valign="top" id="tblContractDtls">
                        <div class="text headerband" style="font-family:Arial;font-size:14px;">
                           <strong> Contract Details </strong> 
                        </div>

                            <table width="100%" style="vertical-align: top" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td  style="padding-right: 5px; text-align: right; height:28px; padding-top:2px;padding-bottom:2px;width:165px;">
                                        Vessel :</td>
                                    <td style="text-align: left;padding-left: 5px;padding-top:2px;padding-bottom:2px;" >
                                        <asp:DropDownList ID="ddlvessel" Width="200px" runat="server" CssClass="input_box">
                                        </asp:DropDownList>
                                      <%--  <asp:RequiredFieldValidator id="RequiredFieldValidator14" runat="server" ErrorMessage="Required." ControlToValidate="ddlvessel" InitialValue="0" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                    </td> 
                                        
                                </tr>
                                 <tr>
                                    <td style="padding-right: 5px; text-align: right; height:28px; padding-top:2px;padding-bottom:2px;width:165px;">
                                        Contract Type :
                                    </td>
                                    <td style="text-align: left;padding-left: 5px;padding-top:2px;padding-bottom:2px;">
                                            <asp:DropDownList ID="ddlContractType" Width="200px" runat="server" 
                                            CssClass="input_box" OnSelectedIndexChanged="ddlContractType_SelectedIndexChanged" AutoPostBack="True" 
                                            >
                                        </asp:DropDownList>
                                          <%--  <asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" ErrorMessage="Required." ControlToValidate="ddlContractType" Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                        <td style="padding-right: 5px; text-align: right; height:28px; padding-top:2px;padding-bottom:2px;width:165px;">
                                        Charterer :
                                    </td>
                                    <td style="text-align: left;padding-left: 5px;padding-top:2px;padding-bottom:2px;">
                                            <asp:DropDownList ID="ddcharterer" runat="server" CssClass="input_box" Width="200px"   >
                                                </asp:DropDownList>
                                           <%-- <asp:RequiredFieldValidator id="RequiredFieldValidator4" runat="server" ErrorMessage="Required." ControlToValidate="ddcharterer" Display="Dynamic" InitialValue="0" ></asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                               
                            </table>
                            <table width="100%" style="vertical-align: top" cellpadding="0" cellspacing="0" id="tblTimeCharter" runat="server" visible="false">
                                <tr>
                                    <td  style="padding-right: 5px; text-align: right; height:28px; padding-top:2px;padding-bottom:2px;width:165px;">
                                        Contract Start Date :</td>
                                    <td style="text-align: left;padding-left: 5px;padding-top:2px;padding-bottom:2px;">
                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="input_box" Width="92px" OnTextChanged="txtFromDate_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                            <asp:ImageButton ID="imgFromDt" runat="server" CausesValidation="False" 
                                                                ImageUrl="~/Modules/HRD/Images/Calendar.gif"  />
              <%--  <asp:RequiredFieldValidator id="RequiredFieldValidator11" runat="server" ErrorMessage="Required." ControlToValidate="txtFromDate" Text="*" meta:resourcekey="RequiredFieldValidator11Resource1"></asp:RequiredFieldValidator> --%><ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy"
                                                        PopupButtonID="imgFromDt" PopupPosition="TopRight" TargetControlID="txtFromDate">
                                                    </ajaxToolkit:CalendarExtender>
                                &nbsp;
                <asp:DropDownList runat="server" ID="ddlFromHrs" Width="50px">
                                <asp:ListItem Value="" Text="Hrs"></asp:ListItem>
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
                            <%--<asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator88" ControlToValidate="ddlFromHrs" ErrorMessage="Required." Text="*"></asp:RequiredFieldValidator>--%>
                            &nbsp;
                            <asp:DropDownList runat="server" ID="ddlFromMin" Width="50px">
                            <asp:ListItem Value="" Text="Mins"></asp:ListItem>
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
                           <%-- <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator89" ControlToValidate="ddlFromMin" ErrorMessage="Required." Text="*"></asp:RequiredFieldValidator>--%>
                                
                                    </td>
                                </tr>
                                <tr>
                <td style="padding-right: 5px; text-align: right; height:28px; padding-top:2px;padding-bottom:2px;width:165px;">
                                        Contract End Date :</td>
                                    <td style="text-align: left;padding-left: 5px;padding-top:2px;padding-bottom:2px;" >
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="input_box" Width="92px" OnTextChanged="txtToDate_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                            <asp:ImageButton ID="imgToDate" runat="server" CausesValidation="False" 
                                                                ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                         <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                        PopupButtonID="imgToDate" PopupPosition="TopRight" TargetControlID="txtToDate">
                                                    </ajaxToolkit:CalendarExtender>
                                     <%--   <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Required." Text="*" ControlToValidate="txtToDate" meta:resourcekey="RequiredFieldValidator11Resource1"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator1" ControlToCompare="txtFromDate" ControlToValidate="txtToDate"  Operator="GreaterThanEqual" CultureInvariantValues="True" display="Dynamic"
                Type="Date"  runat="server" ErrorMessage="End Date Must Be GreaterThan or Equal To From Date"></asp:CompareValidator>--%>
                                        &nbsp;
                <asp:DropDownList runat="server" ID="ddlToHrs" Width="50px">
                                <asp:ListItem Value="" Text="Hrs"></asp:ListItem>
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
                           <%-- <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator5" ControlToValidate="ddlToHrs" ErrorMessage="Required." Text="*"></asp:RequiredFieldValidator>--%>
                            &nbsp;
                            <asp:DropDownList runat="server" ID="ddlToMins" Width="50px">
                            <asp:ListItem Value="" Text="Mins"></asp:ListItem>
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
                           <%-- <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator6" ControlToValidate="ddlToMins" ErrorMessage="Required." Text="*"></asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                        <td style="padding-right: 5px; text-align: right; height:28px; padding-top:2px;padding-bottom:2px;width:165px;">
                                        Contract Duration (Days) :</td>
                                    <td style="text-align: left;padding-left: 5px;padding-top:2px;padding-bottom:2px;" >
                                        <asp:TextBox ID="txtTotalNoofDays" runat="server" CssClass="input_box" Width="92px" ReadOnly="true"></asp:TextBox>
                                                            </td>
                                </tr>
                                <tr>
                                    <td  style="padding-right: 5px; text-align: right; height:28px; padding-top:2px;padding-bottom:2px;width:165px;">
                                        Per Day
                                        Hire Amount (US $) :</td>
                                    <td style="text-align: left;padding-left: 5px;padding-top:2px;padding-bottom:2px;">
                                        <asp:TextBox ID="txtPerDayAmount" runat="server" CssClass="input_box" Width="92px" ReadOnly="true" OnTextChanged="txtPerDayAmount_TextChanged" AutoPostBack="True"></asp:TextBox>
                                           <%-- <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtPerDayAmount" FilterType="Numbers,Custom" ValidChars="." ></ajaxToolkit:FilteredTextBoxExtender> 
                                        <asp:RequiredFieldValidator id="RequiredFieldValidator7" runat="server" ErrorMessage="Required." ControlToValidate="txtPerDayAmount" ></asp:RequiredFieldValidator>      --%>  
                                    </td>
                                </tr>
                                 <tr>
                                    <td  style="padding-right: 5px; text-align: right; height:28px; padding-top:2px;padding-bottom:2px;width:165px;">
                                        Total
                                        Hire Amount (US $) :</td>
                                    <td style="text-align: left;padding-left: 5px;padding-top:2px;padding-bottom:2px;">
                                        <asp:TextBox ID="txtHireAmount" runat="server" CssClass="input_box" Width="92px" OnTextChanged="txtHireAmount_TextChanged" MaxLength="10" AutoPostBack="True"></asp:TextBox>

                                    <%--    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtHireAmount" FilterType="Numbers,Custom" ValidChars="." ></ajaxToolkit:FilteredTextBoxExtender>
                                       <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ErrorMessage="Required." ControlToValidate="txtHireAmount" meta:resourcekey="RequiredFieldValidator11Resource1"></asp:RequiredFieldValidator>--%>

                                    </td>
                                </tr>
                                <%-- <tr>
                                    <td style="padding-right: 5px; text-align: right; height:28px; padding-top:2px;padding-bottom:2px;">
                                        Status :</td>
                                    <td style="text-align: left;padding-left: 5px;padding-top:2px;padding-bottom:2px;" >
                                        <asp:DropDownList ID="ddlStatus" runat="server" Width="100px">
                                            <asp:ListItem Text="Open" Selected="True" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Closed"  Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                                            </td>
                                </tr>--%>
                            </table>
                            <table width="100%" style="vertical-align: top" cellpadding="0" cellspacing="0" id="tblVoyageCharter" runat="server" visible="false">
                                <tr>
                                    <td style="padding-right: 5px; text-align: right; height:28px; padding-top:2px;padding-bottom:2px;width:165px;">
                                        From Port :
                                    </td>
                                    <td style="text-align: left;padding-left: 5px;padding-top:2px;padding-bottom:2px;">
                                          <asp:TextBox ID="txtfromport" runat="server" AutoPostBack="True" 
                                                                    CssClass="input_box"  Width="164px"></asp:TextBox>
                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtfromport" Display="Dynamic" ErrorMessage="Required." ValidationGroup="mnth"></asp:RequiredFieldValidator>--%>
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" MinimumPrefixLength="1" TargetControlID="txtfromport" ServicePath="~/Modules/REVENUE/WebService.asmx" ServiceMethod="GetPortTitles" runat="server" DelimiterCharacters="" Enabled="True">
                                                    </cc1:AutoCompleteExtender>
                                    </td>
                                </tr>
                                 <tr>
                                    <td style="padding-right: 5px; text-align: right; height:28px; padding-top:2px;padding-bottom:2px;width:165px;">
                                        To Port :
                                    </td>
                                    <td style="text-align: left;padding-left: 5px;padding-top:2px;padding-bottom:2px;">
                                         <asp:TextBox ID="txttoport" runat="server" CssClass="input_box" Width="164px"></asp:TextBox>
                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtToPort" Display="Dynamic" ErrorMessage="Required." ValidationGroup="mnth"></asp:RequiredFieldValidator>--%>
                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" MinimumPrefixLength="1" TargetControlID="txttoport" ServicePath="~/Modules/REVENUE/WebService.asmx" ServiceMethod="GetPortTitles" runat="server" DelimiterCharacters="" Enabled="True">
                                                </cc1:AutoCompleteExtender>
                                        </td>
                                     </tr>
                                 <tr>
                                    <td style="padding-right: 5px; text-align: right; height:28px; padding-top:2px;padding-bottom:2px;width:165px;">
                                        Volume (Tonnage) :
                                    </td>
                                    <td style="text-align: left;padding-left: 5px;padding-top:2px;padding-bottom:2px;">
                                         <asp:TextBox ID="txtVolume" runat="server" CssClass="input_box" Width="92px"  MaxLength="10" AutoPostBack="True" OnTextChanged="txtVolume_TextChanged"></asp:TextBox>

                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtVolume" FilterType="Numbers,Custom" ValidChars="." ></ajaxToolkit:FilteredTextBoxExtender> 
                                      <%--  <asp:RequiredFieldValidator id="RequiredFieldValidator10" runat="server" ErrorMessage="Required." ControlToValidate="txtVolume" ></asp:RequiredFieldValidator>--%>
                                         </td>
                                     </tr>
                                <tr>
                                    <td style="padding-right: 5px; text-align: right; height:28px; padding-top:2px;padding-bottom:2px;width:165px;">
                                        Rate (Per Ton) :
                                    </td>
                                    <td style="text-align: left;padding-left: 5px;padding-top:2px;padding-bottom:2px;">
                                         <asp:TextBox ID="txtRateinTon" runat="server" CssClass="input_box" Width="92px"  MaxLength="10" AutoPostBack="True" OnTextChanged="txtRateinTon_TextChanged" ></asp:TextBox>

                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtRateinTon" FilterType="Numbers,Custom" ValidChars="." ></ajaxToolkit:FilteredTextBoxExtender> 
                                      <%--  <asp:RequiredFieldValidator id="RequiredFieldValidator12" runat="server" ErrorMessage="Required." ControlToValidate="txtRateinTon" ></asp:RequiredFieldValidator>--%>
                                         </td>
                                     </tr>
                                <tr>
                                    <td  style="padding-right: 5px; text-align: right; height:28px; padding-top:2px;padding-bottom:2px;width:165px;">
                                        Total
                                        Hire Amount (US $) :</td>
                                    <td style="text-align: left;padding-left: 5px;padding-top:2px;padding-bottom:2px;">
                                        <asp:TextBox ID="txtTotalHireAmountforVoyage" runat="server" CssClass="input_box" Width="92px" MaxLength="10" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td  style="padding-right: 5px; text-align: right; height:28px; padding-top:2px;padding-bottom:2px;width:165px;">
                                        Expected Voyage Duration (Days) :</td>
                                    <td style="text-align: left;padding-left: 5px;padding-top:2px;padding-bottom:2px;">
                                        <asp:TextBox ID="txtExpVoyageDays" runat="server" CssClass="input_box" Width="92px" MaxLength="10" ></asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtExpVoyageDays" FilterType="Numbers" ValidChars="." ></ajaxToolkit:FilteredTextBoxExtender> 
                                      <%--  <asp:RequiredFieldValidator id="RequiredFieldValidator13" runat="server" ErrorMessage="Required." ControlToValidate="txtExpVoyageDays" ></asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                     <td  style="padding-right: 5px; text-align: right; height:50px; padding-top:2px;padding-bottom:2px;width:165px;">
                                        Cargo Details :</td>
                                    <td style="text-align: left;padding-left: 5px;padding-top:2px;padding-bottom:2px;">
                                        <asp:TextBox ID="txtCargoDetails" runat="server" TextMode="MultiLine" CssClass="input_box" Width="350px" MaxLength="500" Height="45px" ></asp:TextBox>
                                        
                                        </td>
                                </tr>
                            </table>
                        </td>
                    <td width="35%" valign="top">
                        <table width="100%" id="tblExpExpenses" runat="server" visible="false">
                            <tr>
                                <td style="width:98%;padding-left:10px;vertical-align:top;">
                <div style="min-height:200px;overflow-x:hidden; overflow-y:auto;border :solid 0px #4371a5;font-family:Arial;font-size:12px;padding-top: 5px;padding-right:5px;"  >
                    <asp:Label ID="lblExpExpensesMessage" runat="server" Text="Expected Expenses"></asp:Label>
                    <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                    <colgroup>
                                        <col style="width:60%;" />
                                       <col style="width: 40%;" />
                                    </colgroup>
                                        <tr align="left" class="headerstylegrid">
                                            <td style="width: 4%;">Expense Head</td>
                                            <td style="width: 4%;">Amount (US $)</td>
                                        </tr>
                                    
                                </table>
                <table width="100%"  style="border: 1px solid #4C7A6F; " >
                 <colgroup>
                                        <col style="width:60%;" />
                                       <col style="width: 40%;" />
                                    </colgroup>
                <tr>
                <td>
                    <asp:Repeater runat="server" ID="rptExpExpenses"  >
                <ItemTemplate>
                <tr>
                <td style="width:60%;text-align:left;padding-left:5px;border: 1px solid #4C7A6F;"> <asp:Label runat="server" ID="lblCategoryName" Text='<%#Eval("CategoryName")%>' CssClass="textlabel"  style='text-align:center'  Font-Size="12px"></asp:Label>
                </td>
                <td style="width:40%;text-align:center;border: 1px solid #4C7A6F;"><asp:TextBox runat="server" ID="txtRVCEEAmount" Text='<%#FormatCurr(Eval("RVCEE_Amount"))%>' CssClass="textlabel" Width='98%' style='text-align:right' Enabled="false" Font-Size="12px" OnTextChanged="txtRVCEEAmount_TextChanged" AutoPostBack="True"></asp:TextBox>
                    <asp:HiddenField ID="hdnCategoryId" runat="server" Value='<%#Eval("CategoryId")%>' />
                      
                </td>
                </tr>
                </ItemTemplate>
                       
                </asp:Repeater>
                </td>
                </tr>
                </table>
                    <table width="100%"  style="border: 1px solid #4C7A6F; ">
                        <colgroup>
                                        <col style="width:60%;" />
                                       <col style="width: 40%;" />
                                    </colgroup>
                <tr>
                <td style="width:60%;text-align:left;padding-left:5px;border: 1px solid #4C7A6F;"> 
                    <asp:Label runat="server" ID="lblAddCom" Text="AddCOM" CssClass="textlabel"  style='text-align:center'  Font-Size="12px"></asp:Label> &nbsp;&nbsp;&nbsp;
                   <span> <asp:TextBox ID="txtAddComPer" runat="server" Width="75px" OnTextChanged="txtAddComPer_TextChanged" MaxLength="5" AutoPostBack="True" CssClass="ctltext"></asp:TextBox> %</span>
                     <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtAddComPer" FilterType="Numbers,Custom" ValidChars="." ></ajaxToolkit:FilteredTextBoxExtender>
                </td>
                <td style="width:40%;text-align:center;border: 1px solid #4C7A6F;">
                    <asp:TextBox runat="server" ID="txtAddComAmt" Text="0.00" CssClass="textlabel" Width='98%' style='text-align:right'  Font-Size="12px" ReadOnly="true"></asp:TextBox>
                  
                      
                </td>
                    </tr>
                    </table>
                </div>
                <div style="font-size:14px;font-family:Arial;" id="divTotalExpExpense" runat="server" visible="false">
                <table>
                    <tr>
                        <td width="50%" style="padding:2px 0px 2px 5px;text-align:right;">
                            Total Expected Expenses (US $) :
                        </td>
                        <td style="padding:2px 5px 2px 0px;text-align:left;">
                                <asp:Textbox ID="txtTotalExpExpenses" runat="server" Text="0.00" CssClass="ctltext"  style='text-align:right' ReadOnly="true"></asp:Textbox> 
                        </td>
                           
                    </tr>
                    <tr>
                        <td width="50%" style="padding:2px 0px 2px 5px;text-align:right;">
                            Total Expected Revenue (US $) :
                        </td>
                        <td style="padding:2px 5px 2px 0px;text-align:left;">
                                <asp:Textbox ID="txtTotalRevenue" runat="server" Text="0.00" CssClass="ctltext"  style='text-align:right' ReadOnly="true"></asp:Textbox> 
                        </td>
                           
                    </tr>
                </table>
                    
                </div>
               
                </td>
                                   
                            </tr>
                        </table>
                    </td>
                        </tr>
                        <tr>
                        <td colspan="2">
                        <hr style="width:2px;"/>
                        </td>
                        </tr>
                            
                <tr>
                    <td valign="top" colspan="2">
                        <table cellpadding="0" cellspacing="0" style="padding-bottom:10px; padding-right:10px" width="100%" >
                    <tr>
                    <td style="padding-right: 5px; text-align: right">Created By:</td>
                    <td style="text-align: left"><asp:TextBox ID="txtCreatedBy_DocumentType" runat="server" BackColor="Gainsboro"
                            CssClass="input_box" ReadOnly="True" TabIndex="-1" Width="154px"></asp:TextBox></td>
                    <td style="padding-right: 5px; text-align: right">
                        Created On:</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtCreatedOn_DocumentType" runat="server" BackColor="Gainsboro"
                            CssClass="input_box" ReadOnly="True" TabIndex="-2" Width="80px"></asp:TextBox></td>
                    <td style="padding-right: 5px; text-align: right">
                        Modified By:</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtModifiedBy_DocumentType" runat="server" BackColor="Gainsboro"
                            CssClass="input_box" ReadOnly="True" TabIndex="-3" Width="154px"></asp:TextBox></td>
                    <td style="padding-right: 5px; text-align: right">
                        Modified On:</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtModifiedOn_DocumentType" runat="server" BackColor="Gainsboro"
                            CssClass="input_box" ReadOnly="True" TabIndex="-4" Width="80px"></asp:TextBox></td>
                    <td style="text-align: right;" >
                        <asp:Button ID="btnsave"  runat="server" CssClass="btn" Text="Save" OnClick="btnsave_Click"  Width="59px" OnClientClick="return Validate()"/>&nbsp;
                            <asp:Button runat="server" ID="btnCancelContract" Text="Cancel" CssClass="btn"  OnClick='btnCancelContract_Click' OnClientClick="return window.confirm('Are you sure to cancel this Contract?')" />&nbsp;
                       
                        </td>
                            
                </tr>
                <tr>
                    <td style="padding-left: 10px; text-align: left" valign="top" colspan="9">
                        <asp:Label ID="lblmessage" runat="server" ForeColor="#C00000"></asp:Label>
                    </td>
                </tr>                             
                </table>
                    </td>
                </tr>
                </table>
            </center>
               </div>
                <!-- Mask to cover the whole screen -->
                <div id="mask"></div>
  </form>
</body>
</html>