<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddBunker.aspx.cs" Inherits="AddBunker" Async="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />

    <script type="text/javascript" src="../../HRD/JS/jquery.min.js"></script>
    <script src="../../HRD/JS/AutoComplete/knockout-2.2.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../HRD/JS/KPIScript.js"></script>

    <link rel="stylesheet" href="../../HRD/JS/AutoComplete/jquery-ui.css" />
    <script src="../../HRD/JS/AutoComplete/jquery-ui.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
    <%--<link rel="stylesheet" href="../../CSS/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../../CSS/StyleSheet.css" />
    <link rel="stylesheet" type="text/css" href="../../CSS/style.css" />--%>
      <script type="text/jscript">
          $(document).ready(function () {
              // Iterate through each row in the tbody
              var fultype = $("#hdnFuelType").val();
              $('.tblPR tbody tr').each(function () {
                  // Get the age of the person from the data attribute
                  var type = $(this).find('td:eq(1)').text();
                  //alert(type);
                  // Check the condition and apply formatting
                  if (fultype.trim() == type.trim()) {
                      //  alert(fultype);
                      $(this).addClass('highlighted-row'); // Add a class to highlight the row
                  }
              });
          });
</script>
    <style type="text/css">
        .highlighted-row {
    background-color: yellow; /* Customize the background color as needed */
    /* Add more styles for highlighting */
}
        .auto-style1 {
            width: 75px;
            height: 21px;
        }
        .auto-style2 {
            width: 100px;
            height: 21px;
        }
        .auto-style3 {
            width: 120px;
            height: 21px;
        }
        .auto-style4 {
            width: 170px;
            height: 21px;
        }
    </style>
     <script type="text/javascript">
         function OpenDocument(DocId, ReportPK, VesselId) {
             window.open("ShowDocuments.aspx?DocId=" + DocId + "&ReportPK=" + ReportPK + "&VesselId=" + VesselId);
         }
     </script>

</head>
<body>
    <form id="form" runat="server" style="font-size: 12px; font-family: Arial;">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <div style="padding: 6px; font-size: 14px; text-align: center; font-family: Arial Black;" class="text headerband">
            <%-- <span style="float:right;font-size:11px;font-weight:normal;"> 6-Apr-2017 </span>--%>
            <asp:Label runat="server" ID="lblAddBunker" Text="View Bunkering" Font-Size="20px"></asp:Label>
        </div>
        <div style="padding-left: 50px; padding-right: 50px;">
            <div>
                <table cellpadding="4" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td style="background-color: #FFFFFF; border: solid 1px #006B8F;">
                            <table cellpadding="0" cellspacing="3" border="0" width="100%">
                                                                <tr>
                                    <td style="width: 200px">Voyage Number</td>
                                    <td style="width: 250px">
                                        <asp:HiddenField ID="hfReportPk" runat="server" />
                                        <asp:HiddenField ID="hfReportStatus" runat="server" />
                                         <asp:HiddenField ID="hdnFuelType" runat="server" />
                                        <asp:TextBox runat="server" ID="txtVoyageNumber" Style="width: 200px; text-align: center;" MaxLength="15"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="rftxtVoyageNumber" ControlToValidate="txtVoyageNumber" ErrorMessage="Voyage Number is required." Text="*"></asp:RequiredFieldValidator>
                                    </td>
                                    <td style="text-align: right; width: 250px">Vessel :</td>

                                    <td>
                                      <asp:TextBox runat="server" ID="txtVesselId" ReadOnly="true" Style="width: 200px; text-align: center;" MaxLength="15"></asp:TextBox>

                                    </td>
                                </tr>

                                <tr>
                                    <td style="width: 180px">Port</td>
                                    <td style="width: 250px">
                                        <asp:TextBox runat="server" ID="txtPort" Style="width: 200px; text-align: center;" MaxLength="15"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="RFPort" ControlToValidate="txtPort" ErrorMessage="Port is required." Text="*"></asp:RequiredFieldValidator>
                                    </td>
                                    <td style="text-align: right; width: 250px">Location :</td>

                                    <td>
                                       <%-- <asp:CheckBox ID="cbAnchorage" runat="server" />--%>
                                                    <asp:DropDownList runat="server" ID="ddlLocation" Width="200px">
                                                        <asp:ListItem  Value="0" Text=" "></asp:ListItem>
    <asp:ListItem Value="1" Text="In Port"></asp:ListItem>
    <asp:ListItem Value="2" Text="At Anchorage"></asp:ListItem>

</asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvLocation" runat="server" ControlToValidate="ddlLocation" InitialValue="0" ErrorMessage="Please select a location" Text="*"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>

                            </table>


                        </td>
                    </tr>




                </table>
            </div>
            <div class="div1" style="background-color: #4c7a6f; color: white; padding-left: 5px;">Bunkering Details <asp:Label ID="lblBunkerFuelType" runat="server" Font-Bold="true"></asp:Label></div>
            <div>
                <table cellpadding="4" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td style="background-color: #FFFFFF; border: solid 1px #006B8F;">
                            <table cellpadding="0" cellspacing="3" border="0" width="100%">
                                                                <tr>
                                    <td style="width: 180px">Local Date & Time:</td>
                                    <td style="width: 250px; vertical-align: top; ">
                                        <asp:TextBox runat="server" ID="txtLocalDate" Style="width: 100px; text-align: center;height:17px" MaxLength="15"></asp:TextBox>
                                        <asp:CalendarExtender runat="server" ID="CalLocalDate" TargetControlID="txtLocalDate" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                        <asp:RequiredFieldValidator runat="server" ID="RFLocalDate" ControlToValidate="txtLocalDate" ErrorMessage="Local  Date is required." Text="*"></asp:RequiredFieldValidator>
                                         <asp:DropDownList ID="ddlLocalTimeHours" runat="server" Width="50px" Style="height: 25px;">
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
 <asp:RequiredFieldValidator ID="rfddlLocalTimeHours" runat="server" 
     ControlToValidate="ddlLocalTimeHours" ErrorMessage="Local Time(HRS) is required." 
     Text="*"></asp:RequiredFieldValidator>

                                         <asp:DropDownList ID="ddlLocalTimeMin" runat="server" Width="50px" Style="height: 25px;">
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
 <asp:RequiredFieldValidator ID="rfddlLocalTimeMin" runat="server" 
     ControlToValidate="ddlLocalTimeMin" ErrorMessage="Local Time(MIN) is required." 
     Text="*"></asp:RequiredFieldValidator>

                                    </td>
                                    <td style="text-align: right; width: 250px">UTC Date & Time :</td>
                                    <td style="width: 250px; vertical-align: top; ">
                                        <asp:TextBox runat="server" ID="txtUTCDate" Style="width: 100px; text-align: center;height:17px" MaxLength="15"></asp:TextBox>
                                        <asp:CalendarExtender runat="server" ID="CalUTCDate" TargetControlID="txtUTCDate" Format="dd-MMM-yyyy"></asp:CalendarExtender>
                                        <asp:RequiredFieldValidator runat="server" ID="RFUTCDate" ControlToValidate="txtUTCDate" ErrorMessage="UTC Date is required." Text="*"></asp:RequiredFieldValidator>

                                                                                <asp:DropDownList ID="ddlUTCTimeHours" runat="server" Style="height: 25px;" Width="50px">
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
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
    ControlToValidate="ddlUTCTimeHours" ErrorMessage="UTC Time(HRS) is required." 
    Text="*"></asp:RequiredFieldValidator>

                                        <asp:DropDownList Style="height: 25px;" ID="ddlUTCTimeMins" runat="server" Width="50px">
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
<asp:RequiredFieldValidator ID="rfddlUTCTimeMins" runat="server" 
    ControlToValidate="ddlUTCTimeMins" ErrorMessage="UTC Time(MIN) is required." 
    Text="*"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 180px">Bunker Type:</td>
                                    <td style="width: 250px">
                                        <asp:DropDownList runat="server" ID="ddlType" Width="200px" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                            <asp:ListItem Value="" Text=""></asp:ListItem>
                                        </asp:DropDownList>
                                     <asp:RequiredFieldValidator ID="rfddlType" runat="server" ControlToValidate="ddlType" InitialValue="" ErrorMessage="Please select Fuel Type" Text="*"></asp:RequiredFieldValidator>
                                    </td>
                                    <td style="text-align: right; width: 250px">BDN Number :</td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtBDNNumber" Style="text-align: center;" MaxLength="15" Width="201px"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="rfBDNNumber" ControlToValidate="txtBDNNumber" ErrorMessage="BDN Number is required." Text="*"></asp:RequiredFieldValidator>
                                    </td>
                                    <td style="text-align: right; width: 180px">Sulpher[%] :</td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtSulpherPercent" Style="text-align: center;" MaxLength="15" Width="201px"></asp:TextBox>
                                        <asp:RegularExpressionValidator runat="server" ID="revSulpherPercent" ControlToValidate="txtSulpherPercent" ErrorMessage="Please enter a valid number" ValidationExpression="^(100(\.0+)?|\d{0,2}(\.\d+)?)$" Text="*"></asp:RegularExpressionValidator>
<asp:CustomValidator runat="server" ID="cvSulpherPercent" ControlToValidate="txtSulpherPercent" OnServerValidate="ValidateSulpherPercent" ErrorMessage="Percentage should be between 0 and 100" Text="*"></asp:CustomValidator>
<asp:RequiredFieldValidator runat="server" ID="rfSulpherPercent" ControlToValidate="txtSulpherPercent" ErrorMessage="Sulpher[%] is required." Text="*"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>


                                <tr>
    <td style="width: 200px">Density(at 15&degC as per ISO 3675):</td>
    <td style="width: 250px">
               <asp:TextBox runat="server" ID="txtDensity" Style="text-align: center;" MaxLength="15" Width="199px"></asp:TextBox>[kg/m&sup2]
<asp:RegularExpressionValidator runat="server" ID="revDensity" ControlToValidate="txtDensity" ErrorMessage="Please enter a valid density (kg/m²)" ValidationExpression="^\d+(\.\d+)?$" Text="*"></asp:RegularExpressionValidator>
<asp:CustomValidator runat="server" ID="cvDensity" ControlToValidate="txtDensity" OnServerValidate="ValidateDensity" ErrorMessage="Please enter a valid density value" Text="*"></asp:CustomValidator>
<asp:RequiredFieldValidator runat="server" ID="rfDensity" ControlToValidate="txtDensity" ErrorMessage="Density is required." Text="*"></asp:RequiredFieldValidator>


    </td>
    <td style="text-align: right; width: 200px">LCV [MJ/kg] :</td>
    <td>
        <asp:TextBox runat="server" ID="txtLCV" Style="text-align: center;" MaxLength="15" Width="200px"></asp:TextBox>
        <asp:RegularExpressionValidator runat="server" ID="revLCV" ControlToValidate="txtLCV" ErrorMessage="Please enter a valid LCV [MJ/kg]" ValidationExpression="^\d+(\.\d+)?$" Text="*"></asp:RegularExpressionValidator>
<asp:CustomValidator runat="server" ID="cvLCV" ControlToValidate="txtLCV" OnServerValidate="ValidateDensity" ErrorMessage="Please enter a valid LCV value" Text="*"></asp:CustomValidator>

        <asp:RequiredFieldValidator runat="server" ID="rfLCV" ControlToValidate="txtLCV" ErrorMessage="LCV is required." Text="*"></asp:RequiredFieldValidator>
    </td>
    <td style="text-align: right; width: 180px">Price [$/MT]:</td>
    <td>
        <asp:TextBox runat="server" ID="txtPrice" Style="text-align: center;" MaxLength="15" Width="200px"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" ID="rfPrice" ControlToValidate="txtPrice" ErrorMessage="Price is required." Text="*"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator runat="server" ID="revPrice" ControlToValidate="txtPrice" ErrorMessage="Please enter a valid Price" ValidationExpression="^\d+(\.\d+)?$" Text="*"></asp:RegularExpressionValidator>
    </td>
</tr>



                                                               <%-- <tr>
    <td style="width: 180px">Quantity Unit:</td>
    <td style="width: 250px" colspan="5">
        <asp:RadioButtonList RepeatDirection="Horizontal" ID="rblQuantityUnit" runat="server">
            <asp:ListItem Text="MT" Value="mt"></asp:ListItem>
            <asp:ListItem Text="M3" Value="m3"></asp:ListItem>
        </asp:RadioButtonList>        
                <asp:RequiredFieldValidator runat="server" ID="rfQuantityUnit" ControlToValidate="rblQuantityUnit" ErrorMessage="Price is required." Text="*"></asp:RequiredFieldValidator>

    </td>
    
     
</tr>--%>

                                                                <tr>
    <td style="width: 180px">Bunker Received (BDN Qty.) :</td>
    <td style="width: 250px">
                <asp:TextBox runat="server" ID="txtBunkerReceivedacctoBDNmt" Style="text-align: center;" MaxLength="15" Width="199px"></asp:TextBox>[MT]
        <asp:RegularExpressionValidator runat="server" ID="revBunkerReceivedacctoBDNmt" ControlToValidate="txtBunkerReceivedacctoBDNmt" ErrorMessage="Please enter a valid value in Bunker Received acc to BDN [MT]" ValidationExpression="^\d+(\.\d+)?$" Text="*"></asp:RegularExpressionValidator>
         <asp:RequiredFieldValidator runat="server" ID="rfBunkerReceivedacctoBDNmt" ControlToValidate="txtBunkerReceivedacctoBDNmt" ErrorMessage="Bunker Received acc to BDN [MT] is required." Text="*"></asp:RequiredFieldValidator>

    </td>
                                                                    <td style="text-align: right; width: 180px">Actual Bunker (Rcvd. Qty.) :</td>
<td style="width: 250px">
            <asp:TextBox runat="server" ID="txtActualBunkerReceivedmt" Style="text-align: center;" MaxLength="15" Width="200px"></asp:TextBox>[MT]
     <asp:RequiredFieldValidator runat="server" ID="rfActualBunkerReceivedmt" ControlToValidate="txtActualBunkerReceivedmt" ErrorMessage="Actual Bunker Received [MT] is required." Text="*"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator runat="server" ID="revActualBunkerReceivedmt" ControlToValidate="txtActualBunkerReceivedmt" ErrorMessage="Please enter a valid value in Actual Bunker (Rcvd. Qty.)" ValidationExpression="^\d+(\.\d+)?$" Text="*"></asp:RegularExpressionValidator>


</td>
 
</tr>

                                                                                                <tr>
                                                                                                        <%--<td style="width: 180px">Bunker Received acc to BDN [M3]:</td>
<td style="width: 250px">
            <asp:TextBox runat="server" ID="txtBunkerReceivedacctoBDNm3" ReadOnly="true" Style="width: 100px; text-align: center;" MaxLength="15"></asp:TextBox>

</td>--%>
    
    <%--<td style="width: 180px">Actual Bunker Received [M3]:</td>
<td style="width: 250px">
            <asp:TextBox runat="server" ID="txtActualBunkerReceivedm3" ReadOnly="true" Style="width: 100px; text-align: center;" MaxLength="15"></asp:TextBox>

</td>--%>
</tr>
                                <tr>
                                    <td style="width: 180px">Remarks</td>
                                </tr>
                                <tr>
    <td  colspan="6" style="width: 500px">
        <asp:TextBox ID="txtRemarks" TextMode="MultiLine" Rows="4" runat="server" Width="972px" Height="47px"></asp:TextBox> </td>
</tr>







                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="div1" style="background-color: #4c7a6f; color: white; padding-left: 5px;">Bunker Sample </div>
            <div>
<table cellpadding="4" cellspacing="0" border="0" width="100%">
    <tr>
        <td style="background-color: #FFFFFF; border: solid 1px #006B8F;">
            <table cellpadding="0" cellspacing="3" border="0" width="100%">
    <tr>
        <td style="width: 200px">Sample Sent to (Company Name):</td>
        <td style="width: 250px">
           <%--  <asp:DropDownList runat="server" ID="ddlSampleSentToCompany" Width="70px">
     <asp:ListItem Value="" Text=""></asp:ListItem>
 </asp:DropDownList>--%>
             <asp:TextBox runat="server" ID="txtSampleSentToCompany"  Style="text-align: center;" MaxLength="50" Width="200px"></asp:TextBox>
            </td>
         <td style="text-align: right; width: 180px">Seal Number &nbsp;:&nbsp;</td>
 <td style="width: 250px">
                 <asp:TextBox runat="server" ID="txtSealNumber"   Style="text-align: center;" MaxLength="15" Width="202px"></asp:TextBox>

     </td>
                <td style="text-align: right; width: 180px">Airway Bill Number &nbsp;:&nbsp;</td>
<td style="width: 250px">
                <asp:TextBox runat="server" ID="txtAirwayBillNumber"  Style="text-align: center;" MaxLength="15" Width="201px"></asp:TextBox>

    </td>
        </tr>

                    <tr>
        <td style="width: 180px">Forwarding Instruction Number:</td>
        <td style="width: 250px">
                             <asp:TextBox runat="server" ID="txtForwardingInstructionNumber"   Style="text-align: center;" MaxLength="15" Width="200px"></asp:TextBox>

              
            </td>
         <td style="text-align: right; width: 180px">Name&nbsp;:&nbsp;</td>
 <td style="width: 250px">
                 <asp:TextBox runat="server" ID="txtName"   Style="text-align: center;" MaxLength="15" Width="202px"></asp:TextBox>

     </td>
                <td style="text-align: right; width: 180px">Phone &nbsp;:&nbsp;</td>
<td style="width: 250px">
                <asp:TextBox runat="server" ID="txtPhone"   Style="text-align: center;" MaxLength="15" Width="199px"></asp:TextBox>
    <asp:RegularExpressionValidator runat="server" ID="revPhone" ControlToValidate="txtPhone" ErrorMessage="Please enter a valid phone number" ValidationExpression="^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$" Text="*"></asp:RegularExpressionValidator>


    </td>
        </tr>

                                    <tr>
        <td style="width: 180px">Mobile&nbsp;:&nbsp;</td>
        <td style="width: 250px">
                             <asp:TextBox runat="server" ID="txtFax"  Style="text-align: center;" MaxLength="15" Width="201px"></asp:TextBox>
            <asp:RegularExpressionValidator runat="server" ID="revFax" ControlToValidate="txtFax" ErrorMessage="Please enter a valid mobile number" ValidationExpression="^[0-9]{10}$" Text="*"></asp:RegularExpressionValidator>

              
            </td>
         <td style="text-align: right; width: 180px">Email &nbsp;:&nbsp;</td>
 <td style="width: 250px">
                 <asp:TextBox runat="server" ID="txtEmail"  Style="text-align: center;" MaxLength="15" Width="201px"></asp:TextBox>
     <asp:RegularExpressionValidator runat="server" ID="revEmail" ControlToValidate="txtEmail" ErrorMessage="Please enter a valid email address" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$" Text="*"></asp:RegularExpressionValidator>

     </td>
                <td colspan="2" style="width: 430px">
                    <asp:CheckBox ID="cbFuelTestingReceived" Text="Analysis Received from fuel testing laboratory" runat="server" /></td>
 
        </tr>
                <tr>
                    <td style="width: 180px">File Description &nbsp;:&nbsp; </td>
        <td style="width: 250px"> <asp:TextBox ID="txtfileDescription" Style="text-align: center;" runat="server" Width="200px"></asp:TextBox></td>
                <td style="text-align: right; width: 180px">Choose File &nbsp;:&nbsp;</td> 
         <td style="width: 250px">
        <asp:FileUpload ID="FU" runat="server" Width="100%" CssClass="input_box" />
                  
                   
              </td>
                     <td style="width: 180px" colspan="2" align="center"><asp:Button ID="btnAddDoc" runat="server" CssClass="btn" Text="Upload File" OnClick="btnAddDoc_Click" /></td>
                </tr>
               


                </table>


            </td>
        </tr>
    </table>
            </div>
            

            <div style="text-align: left;" class="stickyFooter">
                <div>
                    <asp:ValidationSummary runat="server" ID="v1" class="validationsummary" />

                </div>
                <div style="width: 98%; text-align: right;">
                    <asp:Label runat="server" ID="lblMessage" ForeColor="Red" Style="float: left" Font-Bold="true" Font-Size="Large"></asp:Label>
                   
                    <asp:Button ID="btnSave" CssClass="btn" runat="server" Text="Save Report" OnClick="btnSaveClick" Width="120px"  />
                   <%-- <asp:Button ID="btnApprove" CssClass="btn" runat="server" Text="Approve" OnClick="btnApprove_Click" Width="120px"/>--%>
                    <asp:Button runat="server" ID="btnLockUnlock" OnClick="btnLockUnlock_Click" Text="Lock for Ship" CssClass="btn"/>
                      <asp:Button ID="btnExport" Text="Export to Ship" runat="server" OnClick="btnExport_Click" CssClass="btn" ValidationGroup="ex"  />

                  
                    <asp:Button ID="btnClose" CausesValidation="false" OnClientClick="self.close();" Text="Close" CssClass="btn" Width="120px" runat="server" />
                </div>
            </div>
                                <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;">
<tr style="height: 20px;">
    <td style="padding: 5px; background-color:#47A1EF;color:White;border-right:solid 1px black;" >
        Attachments            
    </td>
     
   
</tr>
                          <tr>
              <td >
                  <div style="overflow-y: scroll; overflow-x: scroll;height:150px;">

                 
               <table cellpadding="2" cellspacing="0" width="98%" style="margin:auto;" >
                   <colgroup>
                       <col />
                       <col />
                       <col width="100px" />
                       <tr class="headerstylegrid" style="font-weight:bold;">
                           <td></td>
                           <td>FuelType</td>
                           <td>Description</td>
                           <td>Filename</td>
                           <td>Attachment</td>
                       </tr>
                       <asp:Repeater ID="rptDocuments" runat="server">
                           <ItemTemplate>
                               <tr>
                                  <td>
                                       <asp:ImageButton ID="ImgDelete" runat="server" ImageUrl="~/Modules/HRD/Images/delete_12.gif" OnClick="ImgDelete_Click" CommandArgument='<%#Eval("DocId")%>' />
                                  </td>
                                    <td style="text-align:left;padding-left:5px;"><%#Eval("FuelType")%>
     </td>
                                                                  <td style="text-align:left;padding-left:5px;"><%#Eval("Description")%>
</td>
                                   <td style="text-align:left;padding-left:5px;"><%#Eval("FileName")%>
                                       <asp:HiddenField ID="hdnDocId" runat="server" Value='<%#Eval("DocId")%> ' />
                                   </td>
                                   <td> 
                                    <%--   <asp:ImageButton ID="ImgAttachment" runat="server" ImageUrl="~/Images/paperclip.gif" OnClick="ImgAttachment_Click" CommandName='<%#Eval("DocId")%> ' />--%>

                                    <a onclick='OpenDocument(<%# Eval("DocId") %>, <%# Eval("ReportPk") %>, "<%# Eval("VesselId") %>")' style="cursor:pointer;">

                                       <img src="../../HRD/Images/paperclip.gif" />
                                       </a>
                                   </td>
                               </tr>
                           </ItemTemplate>
                       </asp:Repeater>
                   </colgroup>
        </table>
                     </div> 
              </td>
          </tr>
                </table>
                                    </div>
                        <div>
                <table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;">
    <tr style="height: 20px;">
        <td style="padding: 5px; background-color:#47A1EF;color:White;border-right:solid 1px black;" >
           Bunker Received           
        </td>
        <%--<td style="text-align:right;background-color:#47A1EF;color:White;border-right:solid 1px black;">
           
                <asp:LinkButton ID="lnkSummaryReport" runat="server" Text="Summary Report" OnClick="lnkSummaryReport_Click" ForeColor="White" ></asp:LinkButton> 
            
        </td>--%>
       
    </tr>
    <tr>
        <td style="vertical-align: top;border-right:solid 1px black;" colspan="2">
            <div style="border-bottom: none; overflow:scroll; height:25px; overflow-x:hidden;">
                <table width="100%" cellpadding="3" border="0" style="border-collapse: collapse" class="bordered">

                    <thead>
                        <tr class= "headerstylegrid">
                            <td style="width: 30px; text-align: center; color: White;">
                               
                            </td>
                            <td style="width: 100px; text-align: center; color: White;">
                                FuelType
                            </td>
                            <td style="text-align: center; color: White;width:100px;">
                                Local Date
                            </td>
                            <td style="width: 100px; color: White; text-align: center;">
                               UTC Date
                            </td>
                            <td style="width: 120px; color: White; text-align: center;">
                                BDN #
                            </td>
                            <td style="width: 100px; color: White; text-align: center;">
                            Price ($)
                            </td>
                            <td style="width: 170px; color: White; text-align: center;">
                                Bunker Rcvd. (BDN Qty.) (MT)
                            </td>
                            <td style="width: 170px; color: White; text-align: center;">
                                Actual Bunker Rcvd. (MT)
                            </td>
                        </tr>
                    </thead>
                </table>
            </div>
            <div style="height: 397px; border-bottom: none; overflow-x: hidden; overflow-y: scroll;" class='ScrollAutoReset' id='dv_LFI_List'>
                <table width="100%" cellpadding="3" border="0" style="border-collapse: collapse" class="bordered tblPR">
                    <tbody>
                        <asp:Repeater runat="server" ID="rptPR">
                            <ItemTemplate>
                                <tr Id="row" runat="server">
                                    <td style="width: 30px; text-align: center;">
                                        <asp:ImageButton ValidationGroup="v" runat="server" ID="btnView" ImageUrl="~/Modules/HRD/Images/magnifier.png" OnClick="btnView_Click" CommandArgument='<%#Eval("ReportPk").ToString() + "~" + Eval("FuelType").ToString()  %>' Style='background-color: transparent; height: 12px;' CssClass="select-item" />
                                    </td>
                                    <td style="width: 100px; text-align: center;">
                                        <%#Eval("FuelType")%>
                                    </td>
                                     
                                    <td style="width: 100px; text-align: center;">
                                        <%#Common.ToDateString(Eval("LocalDate"))%>
                                    </td>
                                    <td style="width: 100px; text-align: center;">
                                    <%#Common.ToDateString(Eval("UTCDate"))%>
                                    </td>
                                    <td style="width: 120px; text-align: center;">
                                    <%#Eval("BDNNumber")%>
                                    </td>
                                    <td style="width: 100px; text-align: center;">
                                    <%#Eval("price")%>
                                    </td>
                                    <td style="width: 170px;  text-align: center;">
                                    <%#Eval("BunkerReceivedACC")%>
                                    </td>
                                    <td style="width: 170px;  text-align: center;">
                                    <%#Eval("ActualBunkerReceived")%>
                                    </td>

                                   <%-- <td style="width: 40px; text-align: left;">
                                        <asp:ImageButton ID="btnExport" voyNo='<%#Eval("VoyageNo")%>' ToolTip="Export" ImageUrl="~/Images/export.gif"
                                            CommandArgument='<%#Eval("REPORTSPK").ToString() + "~" + Eval("VESSELID").ToString() + "~" + Eval("ACTIVITY_CODE").ToString()%>'
                                            runat="server" OnClick="btnExport_Click" Style='background-color: transparent;
                                            height: 12px;' />
                                    </td>--%>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </td>
       
    </tr>
</table>
            </div>
            <script type="text/javascript">
               

            </script>
    </form>
</body>
</html>
