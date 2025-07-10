<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InspectionObservation_PopUp.aspx.cs" Inherits="Transactions_InspectionObservation_PopUp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>EMANAGER</title>
     <link href="../HRD/Styles/style.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" href="../HRD/Styles/StyleSheet.css" />
    <script language="javascript" type="text/javascript" >
    month = "Jan,Feb,Mar,Apr,May,Jun,Jul,Aug,Sep,Oct,Nov,Dec".split(",");
function checkDate(theField){
  dPart = theField.value.split("-");
  if(dPart.length!=3){
    alert("Enter Date in this format: dd-mmm-yyyy!");
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
  alert("Enter Date in this format: dd-mmm-yyyy!");
  return false;
  }
  nDate = new Date(dPart[2], dPart[1], dPart[0]);
 // nDate = new Date(dPart[0], dPart[1], dPart[2]);
 
  if(isNaN(nDate) || dPart[2]!=nDate.getFullYear() || dPart[1]!=nDate.getMonth() || dPart[0]!=nDate.getDate()){
    alert("Enter Date in this format: dd-mmm-yyyy!");
    theField.select();
    theField.focus();
    return false;
  } else {
    return true;
  }
}
    function checkform()
     {
    if(document.getElementById('txtmaster').value=='')
    {
    alert("Please Enter Master!");
    document.getElementById('txtmaster').focus();
    return false;
    }
    if(document.getElementById('txtchiefengg').value=='')
    {
    alert("Please Enter C/E!");
    document.getElementById('txtchiefengg').focus();
    return false;
    }
    if(document.getElementById('txtstartdate').value=='')
    {
    alert("Please Enter StartDate!");
    document.getElementById('txtstartdate').focus();
    return false;
    }
    if(!checkDate(document.getElementById('txtstartdate')))
    return false;
    if(document.getElementById('txtdonedt').value=='')
    {
    alert("Please Enter Done Date!");
    document.getElementById('txtdonedt').focus();
    return false;
    }
    if(!checkDate(document.getElementById('txtdonedt')))
    return false;
    if(document.getElementById('txtportdone').value=='')
    {
    alert("Please Enter Port Done!");
    document.getElementById('txtportdone').focus();
    return false;
    }
}
function checkform1()
{
    if(document.getElementById('txtstartdate').value=='')
    {
    alert("Please Enter StartDate!");
    document.getElementById('txtstartdate').focus();
    return false;
    }
    if(!checkDate(document.getElementById('txtstartdate')))
    return false;
    if(document.getElementById('txtdonedt').value=='')
    {
    alert("Please Enter Done Date!");
    document.getElementById('txtdonedt').focus();
    return false;
    }
    if(!checkDate(document.getElementById('txtdonedt')))
    return false;
    if(document.getElementById('txtportdone').value=='')
    {
    alert("Please Enter Port Done!");
    document.getElementById('txtportdone').focus();
    return false;
    }

    var dtstrdt=document.getElementById('txtstartdate').value; 
    //alert(dtstrdt);
	var dtdndt=document.getElementById('txtdonedt').value;  
	//alert(dtdndt);
	    if (Date.parse(dtdndt.value) > Date.parse(dtstrdt.value)) 
		    {
			    alert("Done Date cannot be less than Start Date.");
			    dtdndt.focus();
			    return false;
		    }//var aa = document.getElementById('HiddenField1').value;
        //alert(aa);
}
    </script>  
    <style type="text/css">
        .style1
        {
            width: 8%;
        }
    </style>
    </head>
<body style="text-align: center">
    <form id="form1" runat="server">
    <div>
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <br />
        <center>
         <table cellpadding="0" cellspacing="0" style="width: 95%; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center; background-color:#f9f9f9">
            <tr>
                <td colspan="4" style=" height:23px; text-align :center " class="text headerband">Observation Details</td>
            </tr>
            <tr>
            <td colspan="4" style=" padding-top :10px;" >
        <table cellSpacing=0 cellPadding=0 width="100%">
                    <tbody>
                    <tr>
                        <td style="PADDING-RIGHT: 10px; TEXT-ALIGN: right;" class="style1">Start Dt. :</td>
                        <td style="TEXT-ALIGN: left"><asp:TextBox id="txtstartdate" runat="server" 
                                Width="75px" CssClass="required_box"></asp:TextBox> <asp:ImageButton id="ImageButton3" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" CausesValidation="False"></asp:ImageButton></td>
                        <td style="PADDING-RIGHT: 5px; TEXT-ALIGN: right">Done Dt :</td>
                        <td style="TEXT-ALIGN: left"><asp:TextBox id="txtdonedt" runat="server" 
                                Width="75px" CssClass="required_box" OnTextChanged="txtdonedt_TextChanged" 
                                AutoPostBack="True"></asp:TextBox><asp:ImageButton id="ImageButton2" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" CausesValidation="False"></asp:ImageButton></td>
                        <td style="PADDING-RIGHT: 10px; TEXT-ALIGN: right; " width="10%">Port Done :</td>
                        <td style="TEXT-ALIGN: left"><asp:TextBox id="txtportdone" runat="server" Width="210px" CssClass="required_box" OnTextChanged="txtportdone_TextChanged" AutoPostBack="True"></asp:TextBox></td>
                        <td style="PADDING-RIGHT: 5px; TEXT-ALIGN: right">Response Due Dt. :</td>
                        <td style="TEXT-ALIGN: left"><asp:TextBox id="txtresponseduedt" runat="server" 
                                Width="72px" CssClass="input_box" OnTextChanged="txtresponseduedt_TextChanged" 
                                AutoPostBack="True"></asp:TextBox><asp:ImageButton id="ImageButton1" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" CausesValidation="False"></asp:ImageButton></td>
                        <td> <asp:Button id="btnSaveOb" onclick="btnSaveOb_Click" runat="server" Width="59px" CssClass="btn" Text="Save" OnClientClick="return checkform1();"></asp:Button> </td>
                        </tr>
                    <tr>
                        <td style="PADDING-RIGHT: 10px; TEXT-ALIGN: right;" class="style1">&nbsp;</td>
                        <td style="TEXT-ALIGN: left">&nbsp;</td>
                        <td style="PADDING-RIGHT: 5px; TEXT-ALIGN: right">&nbsp;</td>
                        <td style="TEXT-ALIGN: left">&nbsp;</td>
                        <td style="PADDING-RIGHT: 10px; TEXT-ALIGN: right; " width="10%"><span lang="en-us">
                            &nbsp; </span>&nbsp;</td>
                        <td style="TEXT-ALIGN: left">&nbsp;</td>
                        <td style="PADDING-RIGHT: 5px; TEXT-ALIGN: right">&nbsp;</td>
                        <td style="TEXT-ALIGN: left">&nbsp;</td>
                        <td> &nbsp;</td>
                        </tr>
               </tbody></table>
        <ajaxToolkit:CalendarExtender id="CalendarExtender4" runat="server" TargetControlID="txtdonedt" PopupPosition="TopRight" PopupButtonID="ImageButton2" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>
               <ajaxToolkit:CalendarExtender id="CalendarExtender5" runat="server" TargetControlID="txtstartdate" PopupPosition="TopRight" PopupButtonID="ImageButton3" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>
               <ajaxToolkit:CalendarExtender id="CalendarExtender6" runat="server" TargetControlID="txtresponseduedt" PopupPosition="TopRight" PopupButtonID="ImageButton1" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>
                       <cc1:AutoCompleteExtender id="AutoCompleteExtender2" runat="server" TargetControlID="txtportdone" ServicePath="../WebService.asmx" ServiceMethod="GetPortTitles" MinimumPrefixLength="1" Enabled="True" DelimiterCharacters=""></cc1:AutoCompleteExtender>

                                                   
        </td>
            </tr>
            </table> 
        <br /><br />
        <div runat="server" style="display :none" id="dvActions"></div> 
        <table cellpadding="0" cellspacing="0" style="width: 95%; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center; background-color:#f9f9f9">
            <tr>
                <td colspan="4" style=" height:23px; text-align :center " class="text headerband">Officer</td>
            </tr>
            <tr>
                <td></td>
                <td>&nbsp;</td>
                <td></td>
                <td></td>
            </tr>
             <tr>
                <td colspan="4" >
                <TABLE style="BORDER-RIGHT: #8fafdb 1px solid; BORDER-TOP: #8fafdb 0px solid; VERTICAL-ALIGN: top; BORDER-LEFT: #8fafdb 1px solid; BORDER-BOTTOM: #8fafdb 1px solid; BACKGROUND-COLOR: #f9f9f9; TEXT-ALIGN: right" cellSpacing=0 cellPadding=0 width="100%" border=0><TBODY>
                <TR>
                    <TD style="PADDING-RIGHT: 10px; COLOR: red; TEXT-ALIGN: center"></TD>
                </TR>                                 
                <TR>
                    <TD style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; PADDING-TOP: 2px">
                        <FIELDSET style="BORDER-RIGHT: #8fafdb 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #8fafdb 1px solid; PADDING-LEFT: 5px; BORDER-LEFT: #8fafdb 1px solid; BORDER-BOTTOM: #8fafdb 1px solid; TEXT-ALIGN: center"><LEGEND><STRONG>Officer </STRONG></LEGEND>
                            <TABLE cellSpacing=0 cellPadding=2 width="100%" border=0><TBODY>
                                <TR>
                                    <TD style="PADDING-RIGHT: 10px; TEXT-ALIGN: right">Master: </TD>
                                    <TD style="TEXT-ALIGN: left"><asp:TextBox id="txtmaster" runat="server" Width="210px" CssClass="required_box" OnTextChanged="txtmaster_TextChanged" AutoPostBack="True"></asp:TextBox></TD>
                                    <TD style="PADDING-RIGHT: 10px; TEXT-ALIGN: right">C/E: </TD>
                                    <TD style="TEXT-ALIGN: left"><asp:TextBox id="txtchiefengg" runat="server" Width="210px" CssClass="required_box" OnTextChanged="txtchiefengg_TextChanged" AutoPostBack="True"></asp:TextBox></TD>
                                    <TD style="PADDING-RIGHT: 10px; TEXT-ALIGN: right">Chief Officer: </TD>
                                    <TD style="TEXT-ALIGN: left"><asp:TextBox id="txtchiefofficer" runat="server" Width="210px" CssClass="input_box" OnTextChanged="txtchiefofficer_TextChanged" AutoPostBack="True"></asp:TextBox></TD>
                                </TR>
                                <TR>
                                    <TD style="PADDING-RIGHT: 10px; TEXT-ALIGN: right">1st A/E: </TD>
                                    <TD style="TEXT-ALIGN: left"><asp:TextBox id="txtfirstassistant" runat="server" Width="210px" CssClass="input_box" OnTextChanged="txtfirstassistant_TextChanged" AutoPostBack="True"></asp:TextBox></TD>
                                    <TD style="PADDING-RIGHT: 10px; TEXT-ALIGN: right">2nd Officer: </TD>
                                    <TD style="TEXT-ALIGN: left"><asp:TextBox id="txtsecofficer" runat="server" Width="210px" CssClass="input_box" OnTextChanged="txtsecofficer_TextChanged" AutoPostBack="True"></asp:TextBox></TD>
                                    <TD style="PADDING-RIGHT: 10px; TEXT-ALIGN: right">Inspector: </TD>
                                    <TD style="TEXT-ALIGN: left"><asp:TextBox id="txtinspector" runat="server" Width="210px" CssClass="input_box"></asp:TextBox></TD>
                                </TR>
                                <TR>
                                    <TD style="PADDING-RIGHT: 10px; TEXT-ALIGN: right">MTM Supt: </TD>
                                    <TD style="TEXT-ALIGN: left"><asp:TextBox id="txtmtmsupt" runat="server" ReadOnly="True" Width="210px" CssClass="input_box"></asp:TextBox></TD>
                                    <TD style="PADDING-RIGHT: 10px; TEXT-ALIGN: right">&nbsp;</TD><TD style="TEXT-ALIGN: left">&nbsp;</TD>
                                    <TD style="PADDING-RIGHT: 10px; TEXT-ALIGN: right">&nbsp;</TD><TD style="TEXT-ALIGN: right">
                                    <asp:Button id="btn_UpdateCrew" onclick="btn_UpdateCrew_Click" runat="server" CssClass="btn" style="display:none" Text="Update Crew"></asp:Button>
                                    <asp:Button id="btnSave" onclick="btnSave_Click" runat="server" Width="59px" CssClass="btn" Text="Save" OnClientClick="return checkform();"></asp:Button></TD>
                                </TR>
                        </TBODY>
                        </TABLE>
                        </FIELDSET>
                    </TD>
                    </TR>
                    <TR>
                    <TD>                        
                        <asp:HiddenField id="HiddenField1" runat="server"></asp:HiddenField>
                        <asp:HiddenField id="HiddenField_ObId" runat="server"></asp:HiddenField>
                        <asp:HiddenField id="HiddenField_InspId" runat="server"></asp:HiddenField>
                        <asp:HiddenField id="HiddenField_TotalObs" runat="server"></asp:HiddenField>
                        <asp:HiddenField id="HiddenField_MTMSupt" runat="server"></asp:HiddenField>
                        <asp:HiddenField id="HiddenField_ObsId" runat="server"></asp:HiddenField></TD></TR>
                    </TBODY>
                    </TABLE>
                </td>
                </tr> 
                <tr>
                <td>
                     <asp:Label ID="lblmessage" runat="server" ForeColor="#C00000"></asp:Label>
                </td>
                </tr> 
        </table>
        </center>
    </div>
    </form>
</body>
</html>
