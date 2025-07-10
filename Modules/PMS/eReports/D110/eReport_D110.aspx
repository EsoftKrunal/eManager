<%@ Page Language="C#" AutoEventWireup="true" CodeFile="eReport_D110.aspx.cs" Inherits="eReports_S133_eReport_S133" EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eReport D110</title>

     <script type="text/javascript" src="../JS/jquery.min.js"></script>
     <script type="text/javascript" src="../JS/KPIScript.js"></script>     
      <link rel="stylesheet" type="text/css" href="../css/jquery.datetimepicker.css"/>
      <%--<link rel="stylesheet" type="text/css" href="../css/StyleSheet.css"/>--%>
      <link rel="stylesheet" type="text/css" href="~/eReports/D110/StyleSheet.css"/>

<script type="text/javascript">
     function getBaseURL() {
             var url = window.location.href.split('/');
             var baseUrl = url[0] + '//' + url[2] + '/' + url[3];
             return baseUrl;
         }
  </script>
</head>
<body style="margin-bottom:100px;" >
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div>

        <div style="height:74px;width:100%;border:solid 0px red;top:0;" class="ScrollAutoReset" id='uji67887268'>
         
         <table cellpadding="0" cellspacing="0" width="100%" border="0" class="nobordered" style=" background-color:#CCE6FF;margin-top:0px;">
                 <tr>
                     <td style="text-align:left; width:30%; " class="formname">Vessel : <asp:Label ID="lblVesselName" runat="server" CssClass="formname" ></asp:Label></td>
                     <td style="text-align:center; width:40%; "> <asp:Label ID="lblFormName" runat="server" class="formname"></asp:Label></td>
                     <td style="text-align:right; width:30%" class="formname">Form :&nbsp;D110 ( <asp:Label ID="lblVersionNo" runat="server" class="formname"></asp:Label> )</td>
                     </tr>
                 <tr>
                     <td style="text-align:left; width:30%; " class="formname">&nbsp; [ <asp:Label ID="lblReportNo" runat="server" Font-Bold="true" ForeColor="Brown" Font-Size="14px" ></asp:Label> ]</td>
                     <td style="text-align:center; width:40%; ">                        
                     </td>
                     <td style="text-align:right; width:30%" class="formname">
                        
                     </td>

                 </tr>
             </table>
            
        </div>

        <div style="overflow-x:hidden;overflow-y:scroll;margin-top:0px;margin-bottom:0px;height:480px;" class="ScrollAutoReset" id='divMid'>
            <table width="100%" style="margin-top:0px;">
         <tr>
             <td style="padding-left:100px;padding-right:100px;" >
                 <table cellpadding="5" cellspacing="2" border="0" width="100%" class="bordered">
                     <colgroup>
                         <col style="width:170px;" />
                         <col style="width:20px;" />
                         <col />
                         <col style="width:150px;" />
                         <col style="width:20px;" />
                         <col />
                     </colgroup>
                     <tr>
                         <td colspan="3" align="center" > 
                             <asp:RadioButtonList runat="server" ID="radProbType" RepeatDirection="Horizontal" class="nobordered">
                                 <asp:ListItem Text="Injury" Value="J"></asp:ListItem>
                                 <asp:ListItem Text="Illness" Value="L"></asp:ListItem>
                             </asp:RadioButtonList>
                         </td>
                         
                     </tr>
                     </table>
                 
                 <%---------------------------------------------------------------------------------------------------%>
                 
                 <%---------------------------------------------------------------------------------------------------%>
                 

                 <%---------------------------------------------------------------------------------------------------%>

                 <table cellpadding="5" cellspacing="2" border="0" width="100%" class="bordered">
                         <colgroup>
                         <col style="width:250px;" />                         
                         <col />
                        
                     </colgroup>
                      <tr>
                         <td > 
                             Report Date Time 
                         </td>                         
                         <td > 
                             <asp:TextBox ID="txtDateTimeOfReport" runat="server" CssClass="date_only" MaxLength="11"></asp:TextBox>
                              <asp:DropDownList runat="server" ID="ddlDateTimeOfReportHour" Width="50px" >
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
                            </asp:DropDownList> Hrs
                              <asp:DropDownList runat="server" ID="ddlDateTimeOfReportMinut" Width="50px">
                            <asp:ListItem Value="" Text =""></asp:ListItem>
                            <asp:ListItem Value="0" Text ="00"></asp:ListItem>                                   
                            <asp:ListItem Value="1" Text ="01"></asp:ListItem>
                            <asp:ListItem Value="2" Text ="02"></asp:ListItem>
                            <asp:ListItem Value="3" Text ="03"></asp:ListItem>
                            <asp:ListItem Value="4" Text ="04"></asp:ListItem>
                            <asp:ListItem Value="5" Text ="05"></asp:ListItem>
                            <asp:ListItem Value="6" Text ="06"></asp:ListItem>
                            <asp:ListItem Value="7" Text ="07"></asp:ListItem>
                            <asp:ListItem Value="8" Text ="08"></asp:ListItem>
                            <asp:ListItem Value="9" Text ="09"></asp:ListItem>
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

                                </asp:DropDownList> Min
                         </td>
                     </tr>
                     <tr>
                         <td>
                             <strong>Ship&#39;s Location </strong>
                             <div><i>( At onset of illness/injury )</i></div>
                         </td>                         
                         <td>
                             <asp:UpdatePanel runat="server" >
                                 <ContentTemplate>
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="nobordered">
                                    <tr>
                                        <td>
                                            <strong> <asp:RadioButton runat="server" ID="radAtSea" GroupName="location" Text="At Sea" AutoPostBack="true" OnCheckedChanged="radAtSea_OnCheckedChanged"/> </strong>
                                        </td>
                                    </tr>
                                     <tr>
                                         <td>
                                             <asp:Panel runat="server" ID="pnlAtSea" style="margin-bottom:15px;">
                                                  <table width="100%" border="1" cellpadding="0" cellspacing="0" class="bordered">
                                                      <col width="100px" />
                                                      <col width="300px"  />
                                                      <col width="100px" />
                                                      <col />
                                                     <tr>
                                                         <td>
                                                             Latitude 
                                                         </td>
                                                         <td>
                                                             <asp:DropDownList runat="server" ID="ddlLattitude1" Width="70px">
                                                            <asp:ListItem Value="-1" Text=""></asp:ListItem>
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
                                                        <asp:ListItem Value="-1" Text=""></asp:ListItem>
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
                                                         <td>
                                                             Longitude 
                                                         </td>
                                                         <td>
                                                             <asp:DropDownList runat="server" ID="ddlLongitude1" Width="70px">
                                                    <asp:ListItem Value="-1" Text=""></asp:ListItem>
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
                                                                    <asp:ListItem Value="-1" Text=""></asp:ListItem>
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
                                                          <td>Destination </td>
                                                          <td>
                                                              <asp:TextBox ID="txtDestination" runat="server"></asp:TextBox>
                                                         </td>
                                                          <td>ETA </td>
                                                         <td>
                                                              <asp:TextBox ID="txtDestinationETA" runat="server"  CssClass="date_only" MaxLength="11"></asp:TextBox>
                                                              <asp:DropDownList runat="server" ID="ddlDestinationETAHour" Width="50px" >
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
                            </asp:DropDownList> Hrs
                                                              <asp:DropDownList runat="server" ID="ddlDestinationETAMinut" Width="50px">
                            <asp:ListItem Value="" Text =""></asp:ListItem>
                            <asp:ListItem Value="0" Text ="00"></asp:ListItem>                                   
                            <asp:ListItem Value="1" Text ="01"></asp:ListItem>
                            <asp:ListItem Value="2" Text ="02"></asp:ListItem>
                            <asp:ListItem Value="3" Text ="03"></asp:ListItem>
                            <asp:ListItem Value="4" Text ="04"></asp:ListItem>
                            <asp:ListItem Value="5" Text ="05"></asp:ListItem>
                            <asp:ListItem Value="6" Text ="06"></asp:ListItem>
                            <asp:ListItem Value="7" Text ="07"></asp:ListItem>
                            <asp:ListItem Value="8" Text ="08"></asp:ListItem>
                            <asp:ListItem Value="9" Text ="09"></asp:ListItem>
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

                                </asp:DropDownList> Min
                                                         </td>
                                                      </tr>
                                                 </table>                             
                                             </asp:Panel>
                                         </td>
                                     </tr>
                                    <tr>
                                        <td>
                                            <strong><asp:RadioButton runat="server" ID="radInPort" GroupName="location" Text="In Port"   AutoPostBack="true" OnCheckedChanged="radAtSea_OnCheckedChanged"/></strong>
                                        </td>
                                    </tr>
                                     <tr>
                                         
                                         <td>
                                              <asp:Panel runat="server" ID="pnlInPort"  style="margin-bottom:15px;">
                                                  <table width="100%" border="1" cellpadding="0" cellspacing="0" class="bordered">
                                                      <col  width="170px"/>
                                                      <col  />
                                                      <tr>
                                                          <td>Name of Port 
                                                              
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtNameOfPort" runat="server"></asp:TextBox>
                                                          </td>
                                                      </tr>
                                                      <tr>
                                                          <td>
                                                              ETD 
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtEtaInport" runat="server" CssClass="date_only" MaxLength="11"></asp:TextBox>
                                                          </td>
                                                      </tr>
                                                      <tr>
                                                          <td>
                                                              On shore agent name 
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtOnShoreAgentName" runat="server"></asp:TextBox>
                                                          </td>
                                                      </tr>
                                                       <tr>
                                                          <td>
                                                              On shore agent address 
                                                          </td>
                                                          <td>
                                                              <asp:TextBox ID="txtOnShoreAgentAddress" runat="server"></asp:TextBox>
                                                          </td>
                                                      </tr>
                                                  </table>
                                             </asp:Panel>
                                         </td>
                                     </tr>
                                 </table>
                                 </ContentTemplate>
                             </asp:UpdatePanel>
                         </td>
                         
                     </tr>
                         </table>
                 
                 <%---------------------------------------------------------------------------------------------------%>
                 <table cellpadding="0" cellspacing="0" width="100%" class="bordered">
                     <col style="width:250px;" />                         
                     <tr>
                         <td colspan="6" style="background-color:#e2e2e2;padding:8px;text-align:center;font-weight:bold;color:#1f1d1d;">
                             Particulars of Patient
                         </td>
                     </tr>
                     <tr>
                         <td>Name </td>
                         <td>
                             <asp:TextBox ID="txtPopFirstName" runat="server"></asp:TextBox>
                         </td>
                         <td>Sur Name </td>
                         <td>
                             <asp:TextBox ID="txtPopSirName" runat="server"></asp:TextBox>
                         </td>
                         <td>SS ID</td>
                         <td>
                             <asp:TextBox ID="txtPopSSID" runat="server" ></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td>Rank </td>
                         <td colspan="5">
                             <%--<asp:TextBox ID="txtPopRank" runat="server"></asp:TextBox>--%>
                             <asp:DropDownList ID="ddlPopRank" runat="server"></asp:DropDownList>
                         </td>
                     </tr>
                     <tr>
                         <td>DOB </td>
                         <td colspan="5">
                             <asp:TextBox ID="txtPopDOB" runat="server" CssClass="date_only" MaxLength="11"></asp:TextBox>
                         </td>
                     </tr>
                      <tr>
                         <td>Nationality </td>
                         <td colspan="5">
                             <asp:TextBox ID="txtPopNationality" runat="server"></asp:TextBox>
                         </td>
                     </tr>
                 </table>
                 
                 <%---------------------------------------------------------------------------------------------------%>
                 <table width="100%" class="bordered">
                     <col style="width:250px;" />                         
                        <tr>
                            <td>Hour and date of injury or onset of illness </td>
                            <td>
                                <asp:TextBox ID="txtDateOfInjury" runat="server" CssClass="date_only" MaxLength="11"></asp:TextBox> : 
                                <asp:DropDownList runat="server" ID="ddlHourOfInjury" Width="50px" >
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
                            </asp:DropDownList> Hrs
                                <asp:DropDownList runat="server" ID="ddlMinuteOfInjury" Width="50px">
                            <asp:ListItem Value="" Text =""></asp:ListItem>
                            <asp:ListItem Value="0" Text ="00"></asp:ListItem>                                   
                            <asp:ListItem Value="1" Text ="01"></asp:ListItem>
                            <asp:ListItem Value="2" Text ="02"></asp:ListItem>
                            <asp:ListItem Value="3" Text ="03"></asp:ListItem>
                            <asp:ListItem Value="4" Text ="04"></asp:ListItem>
                            <asp:ListItem Value="5" Text ="05"></asp:ListItem>
                            <asp:ListItem Value="6" Text ="06"></asp:ListItem>
                            <asp:ListItem Value="7" Text ="07"></asp:ListItem>
                            <asp:ListItem Value="8" Text ="08"></asp:ListItem>
                            <asp:ListItem Value="9" Text ="09"></asp:ListItem>
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

                                </asp:DropDownList> Min
                            </td>
                        </tr>
                      <tr>
                            <td>Hour and date of first examination/treatment on board </td>
                            <td>
                                <asp:TextBox ID="txtDateOfExaminationOnBoard" runat="server" CssClass="date_only" MaxLength="11"></asp:TextBox> : 
                                <asp:DropDownList runat="server" ID="ddlHourOfExaminationOnBoard" Width="50px" >
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
                            </asp:DropDownList> Hrs
                                <asp:DropDownList runat="server" ID="ddlMinuteOfExaminationOnBoard" Width="50px">
                            <asp:ListItem Value="" Text =""></asp:ListItem>
                            <asp:ListItem Value="0" Text ="00"></asp:ListItem>                                   
                            <asp:ListItem Value="1" Text ="01"></asp:ListItem>
                            <asp:ListItem Value="2" Text ="02"></asp:ListItem>
                            <asp:ListItem Value="3" Text ="03"></asp:ListItem>
                            <asp:ListItem Value="4" Text ="04"></asp:ListItem>
                            <asp:ListItem Value="5" Text ="05"></asp:ListItem>
                            <asp:ListItem Value="6" Text ="06"></asp:ListItem>
                            <asp:ListItem Value="7" Text ="07"></asp:ListItem>
                            <asp:ListItem Value="8" Text ="08"></asp:ListItem>
                            <asp:ListItem Value="9" Text ="09"></asp:ListItem>
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

                                </asp:DropDownList> Min
                            </td>
                        </tr>
                     <tr>
                         <td>Location on board ship where injury occurred(if applicable)</td>
                         <td>
                             <asp:TextBox ID="txtShipLocationWhenInjuryOccurred" runat="server"></asp:TextBox> 
                         </td>
                     </tr>
                     <tr>
                         <td colspan="2"> 
                             Circumstances Of Illeness Or Injury
                             <asp:TextBox ID="txtCircumstancesOfIllenessOrInjury" runat="server" TextMode="MultiLine"></asp:TextBox> 
                         </td>
                     </tr>
                 </table>
                 
                 <%---------------------------------------------------------------------------------------------------%>
                 <table width="100%" class="bordered">
                     <col style="width:250px;" />                         
                     <tr>
                         <td>Is this a repeate illness/injury</td>
                         <td>
                             <asp:DropDownList ID="ddlIsRepearIllnessOrInjury" runat="server">
                                 <asp:ListItem Value="" Text="Select" ></asp:ListItem>
                                 <asp:ListItem Value="1" Text="Yes" ></asp:ListItem>
                                 <asp:ListItem Value="0" Text="No" ></asp:ListItem>
                             </asp:DropDownList>
                         </td>
                     </tr>
                     <tr>
                         <td colspan="2">
                             Findings of physical examination and symptoms
                             <asp:TextBox ID="txtPhysicalExamintaion" runat="server" TextMode="MultiLine" ></asp:TextBox>
                         </td>
                     </tr>
                      <tr>
                         <td>Tratment given on board ?</td>
                         <td>
                             <asp:DropDownList ID="ddlTratmentGivenOnboard" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTratmentGivenOnboard_OnSelectedIndexChanged">
                                 <asp:ListItem Value="" Text="Select" ></asp:ListItem>
                                 <asp:ListItem Value="1" Text="Yes" ></asp:ListItem>
                                 <asp:ListItem Value="0" Text="No" ></asp:ListItem>
                             </asp:DropDownList>
                         </td>
                     </tr>
                     <tr>
                         <td colspan="2">
                             Details of treatment given 
                             <asp:TextBox ID="DetailsOfTreatmentGiven" runat="server" TextMode="MultiLine"></asp:TextBox>
                         </td>
                     </tr>
                 </table>
                 
                 <%---------------------------------------------------------------------------------------------------%>
                 <table width="100%" class="bordered">
                     <col style="width:250px;" />                         
                    <tr>
                        <td colspan="2" style="background-color:#e2e2e2;padding:8px;text-align:center;font-weight:bold;color:#1f1d1d;">
                            Clinical Consultation (To be completed by doctor)
                        </td>
                    </tr>
                     <tr>
                         <td colspan="2">
                             Clinical Diagnosis 
                             <asp:TextBox ID="txtClinicalDiagnosis" runat="server" TextMode="MultiLine" ></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td colspan="2">
                             Details of treatment or examination 
                             <asp:TextBox ID="txtDetailsOfTreatmentOrExamination" runat="server" TextMode="MultiLine"></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td>Patient is declared (Please see below note)   </td>
                         <td>
                             <asp:DropDownList ID="ddlPatientIsDeclared" runat="server">
                                 <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                 <asp:ListItem Value="1" Text="Fit"></asp:ListItem>
                                 <asp:ListItem Value="2" Text="Unfit for sea duties but fit for travel"></asp:ListItem>
                                 <asp:ListItem Value="3" Text="Unfit for sea duties and needs hospitalization"></asp:ListItem>
                                 <asp:ListItem Value="4" Text="Temporary unfit for work"></asp:ListItem>
                             </asp:DropDownList>
                             
                             <asp:Panel ID="pnlUnfitForWork" runat="server">
                                 <table cellpadding="2" cellspacing="2" border="1">
                                     <tr>
                                         <td>From </td>
                                         <td>
                                             <asp:TextBox ID="txtUnfitFrom" runat="server" CssClass="date_only" MaxLength="11"></asp:TextBox>
                                         </td>
                                         <td>To</td>
                                         <td>
                                             <asp:TextBox ID="txtUnfitTo" runat="server" CssClass="date_only" MaxLength="11"></asp:TextBox>
                                         </td>
                                     </tr>
                                 </table>
                             </asp:Panel>
                             <%--<asp:TextBox ID="txtUnfitForWorkFrom" runat="server"></asp:TextBox>--%>
                         </td>
                     </tr>
                     <tr>
                         <td>Name of consultation </td>
                         <td>
                             <asp:TextBox ID="txtNameOfConsultation" runat="server" ></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td>Address of consultation </td>
                         <td>
                             <asp:TextBox ID="txtAddressOfConsultation" runat="server" TextMode="MultiLine" ></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td>Date of consultation </td>
                         <td>
                             <asp:TextBox ID="txtDateOfConsultation" runat="server" CssClass="date_only" MaxLength="11"></asp:TextBox>
                         </td>
                     </tr>
                     <tr>
                         <td>Name of doctor</td>
                         <td>
                             <asp:TextBox ID="txtDoctotName" runat="server" ></asp:TextBox>
                         </td>
                     </tr>
                 </table>
                 <%---------------------------------------------------------------------------------------------------%>
                 <table width="100%" class="bordered">
                     <col style="width:250px;" />                         
                    <tr>
                        <td colspan="2" style="background-color:#e2e2e2;padding:8px;text-align:center;font-weight:bold;color:#1f1d1d;">
                            Attachments
                        </td>
                    </tr>                     
                     <tr>
                         <td colspan="2">

                             <table cellpadding="0" cellspacing="0" class="nobordered" width="100%" style="background-color:#f2eded;">
                                 <col width="450px" />
                                 <col />                                 
                                 <col width="200px" />
                                 <tr>
                                     <td>
                                         <asp:TextBox ID="txtDocumentName" runat="server" Width="350px"></asp:TextBox>
                                     </td>
                                     <td>
                                         <asp:FileUpload ID="fuFile" runat="server" />
                                     </td>
                                     <td>
                                         <asp:Button ID="btnAddFile" runat="server" OnClick="btnAddFile_OnClick" Text="Add Documents" Width="130px" />
                                     </td>
                                 </tr>
                                 <tr>
                                     <td colspan="3" style="text-align:center;">
                                         &nbsp;
                                         <asp:Label ID="lblMsgAttachments" runat="server"  ForeColor="Red" Font-Size="16px" Text="" CssClass="msg"></asp:Label>
                                     </td>
                                 </tr>
                             </table>

                             <table cellpadding="0" cellspacing="0" class="bordered" width="100%">
                                 <col />
                                 <col width="250px" />
                                 <col width="60px" />
                                 <tr class= "headerstylegrid">
                                     <td>Document Name</td>
                                     <td>File Name</td>
                                     <td> File </td>
                                 </tr>
                             </table>
                             <table cellpadding="0" cellspacing="0" class="bordered" width="100%" style="margin-top:0px;">
                                 <col />
                                 <col width="250px" />
                                 <col width="60px" />
                                 <asp:Repeater ID="rptAttachments" runat="server">
                                     <ItemTemplate>
                                        <tr>
                                         <td><%#Eval("DocumentName") %></td>
                                         <td><%#Eval("FileName") %></td>
                                         <td style="text-align:center;">
                                             <%--<img src="../../Images/paperclip.gif" />--%>
                                             <asp:ImageButton ID="btnDownload" runat="server" ImageUrl="~/Modules/PMS/Images/paperclip.gif" OnClick="btnDownload_OnClick" CommandArgument='<%#Eval("ID") %>' />
                                         </td>
                                     </tr>
                                     </ItemTemplate>
                                 </asp:Repeater>
                             </table>
                         </td>
                     </tr>

                    </table>
                 <%---------------------------------------------------------------------------------------------------%>
                 
             </td>
         </tr>         
         <tr>
             <td>
                 
                 
             </td>
         </tr>
         </table>
        </div>
    
        <div id="tdTabs" runat="server" style="border:solid 1px #e2e2e2;padding:6px;background-color:#CCE6FF;text-align:center;position:fixed;left:0;bottom:0;width:100%">
                     <div style="text-align:center;padding:2px;">
                        &nbsp;<asp:Label ID="lblMsg" ForeColor="Red" Font-Size="16px" runat="server" Text="" CssClass="msg"></asp:Label>
                     </div>
                     <asp:Button ID="btnSaveReport" runat="server" Text="Save Report" CssClass="btn"  onclick="btnSaveReport_Click" CausesValidation="true" Width='120px'/>
                     <asp:Button ID="btnExportToOffice" runat="server" Text="Send for Export" CssClass="btn" onclick="btnExportToOffice_Click" CausesValidation="true" Width='140px' OnClientClick="return confirm('Are you sure to send for export?');"/>
                   
                 </div>
    </div>
    </form>
</body>

<script type="text/javascript" src="../JS/jquery.datetimepicker.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $("#divMid").height($(window).height() - 175);
        $(window).resize(function () {
            $("#divMid").height($(window).height()-175);
        });

    });
        

    function SetCalender() {
        $('.date_only').datetimepicker({ timepicker: false, format: 'd-M-Y', formatDate: 'd-M-Y', allowBlank: true, defaultSelect: false, validateOnBlur: false });
        $('.date_time').datetimepicker({ format: 'd-M-Y H:i', formatDate: 'd-M-Y', allowBlank: true, defaultSelect: false, validateOnBlur: false });
    }
    function Page_CallAfterRefresh() {
        SetCalender();
    }
    SetCalender();
</script>
</html>
