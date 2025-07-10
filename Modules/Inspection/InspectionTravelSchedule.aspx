<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="InspectionTravelSchedule.aspx.cs" Inherits="Transactions_InspectionTravelSchedule" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>EMANAGER</title>
      <%-- <link href="../HRD/Styles/style.css" rel="stylesheet" type="text/css" />--%>
     <link rel="stylesheet" type="text/css" href="../HRD/Styles/StyleSheet.css" />
 <script language="javascript" type="text/javascript" >
     month = "Jan,Feb,Mar,Apr,May,Jun,Jul,Aug,Sep,Oct,Nov,Dec".split(",");
     function checkDate(theField) {
         dPart = theField.value.split("-");
         if (dPart.length != 3) {
             alert("Enter Date in this format: dd-mmm-yyyy!");
             theField.focus();
             return false;
         }
         var check = 0;
         for (i = 0; i < month.length; i++) {
             if (dPart[1].toLowerCase() == month[i].toLowerCase()) {

                 check = 1;
                 dPart[1] = i;
                 break;
             }
         }
         if (check == 0) {
             alert("Enter Date in this format: dd-mmm-yyyy!");
             return false;
         }
         nDate = new Date(dPart[2], dPart[1], dPart[0]);
         // nDate = new Date(dPart[0], dPart[1], dPart[2]);

         if (isNaN(nDate) || dPart[2] != nDate.getFullYear() || dPart[1] != nDate.getMonth() || dPart[0] != nDate.getDate()) {
             alert("Enter Date in this format: dd-mmm-yyyy!");
             theField.select();
             theField.focus();
             return false;
         } else {
             return true;
         }
     }
     function checkform() {
         if (document.getElementById('txtmaster').value == '') {
             alert("Please Enter Master!");
             document.getElementById('txtmaster').focus();
             return false;
         }
         if (document.getElementById('txtchiefengg').value == '') {
             alert("Please Enter C/E!");
             document.getElementById('txtchiefengg').focus();
             return false;
         }
         if (document.getElementById('txtstartdate').value == '') {
             alert("Please Enter StartDate!");
             document.getElementById('txtstartdate').focus();
             return false;
         }
         if (!checkDate(document.getElementById('txtstartdate')))
             return false;
         if (document.getElementById('txtdonedt').value == '') {
             alert("Please Enter Done Date!");
             document.getElementById('txtdonedt').focus();
             return false;
         }
         if (!checkDate(document.getElementById('txtdonedt')))
             return false;
         if (document.getElementById('txtportdone').value == '') {
             alert("Please Enter Port Done!");
             document.getElementById('txtportdone').focus();
             return false;
         }
     }
     function checkform1() {
         if (document.getElementById('txtstartdate').value == '') {
             alert("Please Enter StartDate!");
             document.getElementById('txtstartdate').focus();
             return false;
         }
         if (!checkDate(document.getElementById('txtstartdate')))
             return false;
         if (document.getElementById('txtdonedt').value == '') {
             alert("Please Enter Done Date!");
             document.getElementById('txtdonedt').focus();
             return false;
         }
         if (!checkDate(document.getElementById('txtdonedt')))
             return false;
         if (document.getElementById('txtportdone').value == '') {
             alert("Please Enter Port Done!");
             document.getElementById('txtportdone').focus();
             return false;
         }

         var dtstrdt = document.getElementById('txtstartdate').value;
         //alert(dtstrdt);
         var dtdndt = document.getElementById('txtdonedt').value;
         //alert(dtdndt);
         if (Date.parse(dtdndt.value) > Date.parse(dtstrdt.value)) {
             alert("Done Date cannot be less than Start Date.");
             dtdndt.focus();
             return false;
         } //var aa = document.getElementById('HiddenField1').value;
         //alert(aa);
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
    </head>
<body  >
<form id="form1" runat="server" >
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<div style="font-family:Arial;font-size:12px;">
        <center>
              <div style="background-color:#206020; width:95% ; height:3px;">
            </div>
            <br />
        <table cellpadding="0" cellspacing="0" style="width: 100%; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center; background-color:#f9f9f9">
        
        <tr>
        <td>
        <table cellSpacing=0 cellPadding=2 width="100%">
                    <tbody>
                    <tr>
                        <td style="PADDING-RIGHT: 10px; TEXT-ALIGN: right;" class="style1">Start Dt. :</td>
                        <td style="TEXT-ALIGN: left">
                            <asp:TextBox id="txtstartdate" runat="server" Width="90px" CssClass="required_box"></asp:TextBox> <asp:ImageButton id="ImageButton3" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" CausesValidation="False"></asp:ImageButton></td>
                        <td style="PADDING-RIGHT: 5px; TEXT-ALIGN: right">Done Dt :</td>
                        <td style="TEXT-ALIGN: left">
                            <asp:TextBox id="txtdonedt" runat="server" Width="90px" CssClass="required_box" OnTextChanged="txtdonedt_TextChanged" AutoPostBack="True"></asp:TextBox><asp:ImageButton id="ImageButton2" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" CausesValidation="False"></asp:ImageButton></td>
                        <td style="PADDING-RIGHT: 10px; TEXT-ALIGN: right; " width="10%">Port Done :</td>
                        <td style="TEXT-ALIGN: left">
                            <asp:TextBox id="txtportdone" runat="server" Width="210px" CssClass="required_box" OnTextChanged="txtportdone_TextChanged" AutoPostBack="True"></asp:TextBox></td>
                        <td style="PADDING-RIGHT: 5px; TEXT-ALIGN: right"><asp:Label runat="server" id="lblrespdue" Text="Response Due Dt. :"></asp:Label></td>
                        <td style="TEXT-ALIGN: left">
                            <asp:TextBox id="txtresponseduedt" runat="server" Width="90px" CssClass="input_box" OnTextChanged="txtresponseduedt_TextChanged" AutoPostBack="True"></asp:TextBox>
                            <asp:ImageButton id="ImageButton1" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" CausesValidation="False"></asp:ImageButton></td>
                        <td><asp:Button id="btnSaveOb" onclick="btnSaveOb_Click" runat="server"  CssClass="btn" Text="Save" OnClientClick="return checkform1();"></asp:Button> </td>
                        <td><asp:Button id="btnUpdateCrewList" onclick="btnUpdateCrewList_Click" runat="server" Width="110px" CssClass="btn" Text="Update Crew List" OnClientClick="return checkform1();"></asp:Button></td>
                        </tr>
               </tbody></table>
        <ajaxToolkit:CalendarExtender id="CalendarExtender4" runat="server" TargetControlID="txtdonedt" PopupPosition="TopRight" PopupButtonID="ImageButton2" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>
        <ajaxToolkit:CalendarExtender id="CalendarExtender5" runat="server" TargetControlID="txtstartdate" PopupPosition="TopRight" PopupButtonID="ImageButton3" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>
        <ajaxToolkit:CalendarExtender id="CalendarExtender6" runat="server" TargetControlID="txtresponseduedt" PopupPosition="TopRight" PopupButtonID="ImageButton1" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>
        <cc1:AutoCompleteExtender id="AutoCompleteExtender2" runat="server" TargetControlID="txtportdone" ServicePath="WebService.asmx" ServiceMethod="GetPortTitles" MinimumPrefixLength="1" Enabled="True" DelimiterCharacters=""></cc1:AutoCompleteExtender>
        </td>
        </tr>
        </table> 
        
        <table cellpadding="0" cellspacing="0" style="width: 100%; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center; background-color:#f9f9f9">
        
        <tr>
        <td style=" width:60%; ">
        <table cellpadding="2" cellspacing="0" border="1" width="100%" style="background-color:#e2e2e2; border-collapse:collapse">
            <tr>
                <td colspan="3" style=" height:23px; text-align :center; " class="text headerband"> Import Observations</td>
            </tr>
            <tr>
                <%--<td style="width:20px;text-align:center; font-weight:bold;"></td>--%>
                <td style="width:30px;text-align:center; font-weight:bold;">Edit</td>
                <td style="width:80px; font-weight:bold;">&nbsp;Qno</td>
                <td style="font-weight:bold;">Deficiency</td>
            </tr>
            </table>
            <div style="height:330px; overflow-y:scroll; overflow-x:hidden; border-bottom:solid 1px gray; width:100%;">
            <table cellpadding="2" cellspacing="0" border="0" width="100%">
            <asp:Repeater runat="server" ID="rpt_Observations">
            <ItemTemplate>
            <tr>
              <%--  <td style="width:20px;color:Red; text-align:center;"> 
                    <asp:CheckBox runat="server" ID="btnSelect" CommandArgument='<%#Eval("Qno")%>'/>
                </td>--%>
                 <td style="width:30px;color:Red; text-align:center;"> 
                   <asp:LinkButton runat="server" ID="btnView" OnClick='btnView_Click' Text="View" CommandArgument='<%#Eval("Qno")%>'/>
                </td>
                <td style="width:80px;color:Red;">&nbsp;<%#Eval("Qno")%></td>
                <td style="color:Blue;"><%#Eval("Deficiency")%></td>
            </tr>
            </ItemTemplate>
            </asp:Repeater>
                </table>
             </div>  
             <div style="padding:3px">
              &nbsp;<asp:Label runat="server" ForeColor="Red" ID="lblMsgMain" Text="main" ></asp:Label> 
              </div>
        </td>
            <td style=" width:40%;vertical-align:top;padding-top:10px; ">
                 <div >
                     <table width="100%">
                         <tr>
                             <td colspan="2"  style=" height:23px; text-align :center;width:40%;  " 
                    class="text headerband">Officer</td>
                         </tr>
                    <tr style="padding:5px;">
                        <td colspan="2" style="text-align:center;">
                             
                     <asp:Label ID="lblmessage" runat="server" ForeColor="#C00000"></asp:Label>
               
                        </td>
                    </tr>
                    <tr style="padding:5px;">
                        
                        <td width="30%;" style="PADDING-RIGHT: 10px; TEXT-ALIGN: right">Master : </td>
                                    <td width="70%;" style="TEXT-ALIGN: left"><asp:TextBox id="txtmaster" runat="server" Width="250px" CssClass="required_box" OnTextChanged="txtmaster_TextChanged" AutoPostBack="True"></asp:TextBox></td>
                    </tr>
                    <tr style="padding:5px;">
                        <td width="30%;" style="PADDING-RIGHT: 10px; TEXT-ALIGN: right">C/E : </td>
                                    <td width="70%;" style="TEXT-ALIGN: left"><asp:TextBox id="txtchiefengg" runat="server" Width="250px" CssClass="required_box" OnTextChanged="txtchiefengg_TextChanged" AutoPostBack="True"></asp:TextBox></td>
                    </tr>
                    <tr style="padding:5px;">
                         <td width="30%;" style="PADDING-RIGHT: 10px; TEXT-ALIGN: right">Chief Officer : </td>
                                    <td width="70%;" style="TEXT-ALIGN: left"><asp:TextBox id="txtchiefofficer" runat="server" Width="250px" CssClass="input_box" OnTextChanged="txtchiefofficer_TextChanged" AutoPostBack="True"></asp:TextBox></td>
                    </tr>
                    <tr style="padding:5px;">
                        <td width="30%;"  style="PADDING-RIGHT: 10px; TEXT-ALIGN: right">2nd Engineer : </td>
                                    <td width="70%;" style="TEXT-ALIGN: left"><asp:TextBox id="txtfirstassistant" runat="server" Width="250px" CssClass="input_box" OnTextChanged="txtfirstassistant_TextChanged" AutoPostBack="True"></asp:TextBox></td>
                    </tr>
                    <tr style="padding:5px;">
                       <td width="30%;" style="PADDING-RIGHT: 10px; TEXT-ALIGN: right">2nd Officer : </td>
                                    <td width="70%;"  style="TEXT-ALIGN: left"><asp:TextBox id="txtsecofficer" runat="server" Width="250px" CssClass="input_box" OnTextChanged="txtsecofficer_TextChanged" AutoPostBack="True"></asp:TextBox></td>
                    </tr>
                    <tr style="padding:5px;">
                        <td width="30%;" style="PADDING-RIGHT: 10px; TEXT-ALIGN: right">Inspector : </td>
                                    <td width="70%;" style="TEXT-ALIGN: left"><asp:TextBox id="txtinspector" runat="server" Width="250px" CssClass="input_box"></asp:TextBox></td>
                    </tr>
                    <tr style="padding:5px;">
                         <td width="30%;" style="PADDING-RIGHT: 10px; TEXT-ALIGN: right">Superintendent : </td>
                                    <td width="70%;" style="TEXT-ALIGN: left"><asp:TextBox id="txtmtmsupt" runat="server" ReadOnly="True" Width="250px" CssClass="input_box"></asp:TextBox></td>
                    </tr>
                    <tr style="padding:5px;">
                        <td colspan="2">

                        <asp:Button id="btn_UpdateCrew" onclick="btn_UpdateCrew_Click" runat="server" 
                            CssClass="btn" Text="Import Crew List" Width="110px"></asp:Button>
                        &nbsp;<asp:Button id="btnSave" onclick="btnSave_Click" runat="server" Width="59px" CssClass="btn" Text="Save" OnClientClick="return checkform();"></asp:Button>&nbsp;<asp:Button id="btnClosePopUp" onclick="btnClosePopUp_Click" runat="server" Width="59px" CssClass="btn" Text="Lock" ></asp:Button>
                        </td>
                        
                    </tr>
                    <tr>
                        <td colspan="2">
                                          
                        <asp:HiddenField id="HiddenField1" runat="server"></asp:HiddenField>
                        <asp:HiddenField id="HiddenField_ObId" runat="server"></asp:HiddenField>
                        <asp:HiddenField id="HiddenField_InspId" runat="server"></asp:HiddenField>
                        <asp:HiddenField id="HiddenField_TotalObs" runat="server"></asp:HiddenField>
                        <asp:HiddenField id="HiddenField_MTMSupt" runat="server"></asp:HiddenField>
                        <asp:HiddenField id="HiddenField_ObsId" runat="server"></asp:HiddenField></td>
                    </tr>
                   
                </table>
                 </div>
                
            </td>
        </tr>
        </table>
            </center>
        <!-- Section to View Observations-->    
        <center>
        <asp:UpdatePanel runat="server" ID="uio">
        <ContentTemplate>
            <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;z-index:100;" runat="server" id="dbViewObservations" visible="false" >
        <center>
            <div style="position:absolute;top:0px;left:0px; height :550px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:700px; height:450px; padding :3px; text-align :center; border :solid 10px #4371a5; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
            <asp:HiddenField runat="server" ID="hfdId" />
                <table cellpadding="2" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td style="width:130px">Deficiency Code : </td>
                        <td style="text-align:left"> 
                        <asp:TextBox runat="server" ID="txtQno" Width='100px' style=" background-color:#FFFBC9"  CssClass="input_box" AutoPostBack="true" OnTextChanged="txtQuestionNo_Changed"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                        <td style="text-align:left;"> &nbsp;
                        <asp:TextBox runat="server" ID="txtQId" ReadOnly="true" Width='100px' style="display:none"  CssClass="input_box"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td  colspan="4">
                           <b> Question :</b>
                        </td>
                    </tr>
                    <tr>
                        <td  colspan="4">
                           <asp:TextBox runat="server" ID="txtQuestion" Width='100%' Height="50px" TextMode="MultiLine" style=" background-color:#FFFBC9" CssClass="input_box"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  colspan="4">
                           <b> Deficiency :</b>
                        </td>
                    </tr>
                    <tr>
                        <td  colspan="4">
                           <asp:TextBox runat="server" ID="txtDeficiency" Width='100%' Height="70px" TextMode="MultiLine" style=" background-color:#FFFBC9" CssClass="input_box" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  colspan="4">
                            <b>Master Comments : </b>
                        </td>
                    </tr>
                    <tr>
                        <td  colspan="4">
                           <asp:TextBox runat="server" ID="txtMC" Width='100%' Height="80px" TextMode="MultiLine" style=" background-color:#FFFBC9"  CssClass="input_box"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td  colspan="4">
                            <b> Corrective Actions :</b>
                        </td>
                    </tr>
                    <tr>
                        <td  colspan="4">
                           <asp:TextBox runat="server" ID="txtC" Width='100%' Height="80px" TextMode="MultiLine"  CssClass="input_box"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;" colspan="4">
                            <asp:Label ID="lblMsgAppRej" runat="server" ForeColor="Red"></asp:Label>
                            <asp:Button runat="server" ID="btnCloseOb" Text="Close" CssClass="btn" Width="80px" onclick="btnCloseOb_Click" style="float:right" CausesValidation="false" />
                            <asp:Button runat="server" ID="btnImportObs" Text="Import" CssClass="btn" Width="80px" onclick="btnImportObs_Click" style="float:right; margin-right:5px;" />
                            
                        </td>
                    </tr>
                </table>
            </div>
        </center>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
        </center>

       
</div>
 </form>
</body>
</html>