<%@ Page Language="C#" AutoEventWireup="true"   EnableEventValidation="false"   CodeFile="InspectionPlanning.aspx.cs" Inherits="Transactions_InspectionPlanning" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>EMANAGER</title>
       <%--<link href="../HRD/Styles/style.css" rel="stylesheet" type="text/css" />--%>
     <link rel="stylesheet" type="text/css" href="../HRD/Styles/StyleSheet.css" />
    <%--<link rel="stylesheet" type="text/css" href="../../css/app_style.css" />--%>
<script type="text/javascript" src="../Scripts/main.js"></script>
     <script type="text/javascript">
         function OpenInspectionPlanning() {
             window.open('AddEditInspection.aspx', '');
         }
     </script>
<script language="javascript" type="text/javascript">
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
alert('Please Select Vessel!');
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
</script>
<script type="text/javascript">   
    function Ok(sender, e)
    {

    $find('SearchReliver1_md1').hide();
    WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions("SearchReliver1$Main","", true,"&","", false, true));//('SearchReliver1_Main', e); 
    }
</script>
     <script type="text/javascript">
         function Confirm() {
             var confirm_value = document.createElement("INPUT");
             confirm_value.type = "hidden";
             confirm_value.name = "confirm_value";
             if (confirm("Please Confirm : This SUPT is already assigned is coming on all the inspection.It should come just as warning during planning the inspection if same inspector is planned on other inspection on same date.")) {
                 confirm_value.value = "Yes";
             } else {
                 confirm_value.value = "No";
             }
             document.forms[0].appendChild(confirm_value);
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
<script type="text/javascript">
    function CheckEccNo_0K()
    {
        var id='#dialog2';
		//Get the screen height and width
		var maskHeight = $(document).height();
		var maskWidth = $(window).width();
	
		//Set heigth and width to mask to fill up the whole screen
		$('#mask').css({'width':maskWidth,'height':maskHeight});
		
		//transition effect		
		$('#mask').fadeIn(1000);	
		$('#mask').fadeTo("slow",0.8);	
	
		//Get the window height and width
		var winH = $(window).height();
		var winW = $(window).width();
              
		//Set the popup window to center
		$(id).css('top',  winH/2-$(id).height()/2);
		$(id).css('left', winW/2-$(id).width()/2);
	
		//transition effect
		$(id).fadeIn(2000);
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
                <table cellpadding="0" cellspacing="0" width="100%" style="background-color:#f9f9f9; border:#8fafdb 1px solid; border-top:#8fafdb 0px solid;font-family:Arial;font-size:12px;" >
                    <tr>
                        <td>
                            <asp:LinkButton ID="Main" runat="server" OnClick="Main_Click" />
                            <asp:HiddenField id=hidval runat=server></asp:HiddenField>
                                <table width="100%" style="vertical-align: top" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="text-align: center" colspan="7">
                                            </td>
                                    </tr>
                                        
                                    <tr>
                                        <td  style="padding-right: 15px; text-align: right; height:28px; padding-top:5px">
                                            Vessel :</td>
                                        <td style="text-align: left">
                                            <asp:DropDownList ID="ddlvessel" Width=200px runat="server" CssClass="input_box">
                                            </asp:DropDownList></td>
                                        <td>
                                            <asp:RadioButtonList ID="rdbexternalenternal" runat="server" AutoPostBack=true   RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbexternalenternal_SelectedIndexChanged" Width="222px">
                                                <asp:ListItem Selected="True">Internal</asp:ListItem>
                                                <asp:ListItem>External</asp:ListItem>
                                            </asp:RadioButtonList></td>
                                        <td style="padding-right: 15px; text-align: right">
                                            Inspection :</td>
                                        <td style="text-align: left" >
                                            <asp:DropDownList AutoPostBack="true" ID="ddlinspection" Width=250px  runat="server" 
                                                CssClass="input_box" 
                                                onselectedindexchanged="ddlinspection_SelectedIndexChanged">
                                            </asp:DropDownList></td>
                                            <td style =" text-align:right">Version :</td>
                                            <td style=" text-align :left">
                                            &nbsp;<asp:DropDownList ID="ddlVersions" runat="server" CssClass="input_box" Width="100px" TabIndex="4" ></asp:DropDownList>
                                            </td>
                                    </tr>
                                </table>
                            </td>
                            </tr>
                            <tr>
                            <td>
                            <hr />
                            </td>
                            </tr>
                            <tr>
                            <td valign="top">
                            <fieldset style="border-right: #8fafdb 0px solid; padding-right: 5px; border-top: #8fafdb 0px solid;
                                                    padding-left: 5px; padding-bottom: 5px; border-left: #8fafdb 0px solid; padding-top: 0px;
                                                    border-bottom: #8fafdb 0px solid; text-align: center">
                                                    <table width="100%" cellpadding="0" cellspacing="0" border="0px">
                                                        <%--<tr>
                                                            <td style="padding-right: 5px; text-align: right; width:264px" valign="top">
                                                                Inspection No :</td>
                                                            <td style="text-align: left; " colspan="7">
                                                                <asp:Label ID="lblINSPNo" runat="server" Text="INSPNo"></asp:Label>
                                                            </td>
                                                        </tr>--%>
                                                        <tr>
                                                            <td style="padding-right: 5px; text-align: right; padding-top: 5px; padding-bottom:5px; width:264px" 
                                                                valign="top">
                                                                Plan Date :</td>
                                                            <td style="text-align: left; padding-top: 5px; width: 250px;" valign="top">
                                                                <asp:TextBox ID="txtplandate" runat="server" CssClass="input_box" Width="92px"></asp:TextBox>
                                                                <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" 
                                                                    ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                                            </td>
                                                            <td style="padding-right: 5px; text-align: right; padding-top: 5px; padding-bottom:5px; width: 112px;" 
                                                                valign="top">
                                                                Location :</td>
                                                            <td style="width: 200px; text-align: left" valign="top">
                                                                <asp:RadioButtonList ID="rdblocation" runat="server" AutoPostBack="True" 
                                                                    OnSelectedIndexChanged="rdblocation_SelectedIndexChanged" 
                                                                    RepeatDirection="Horizontal" Width="140px">
                                                                    <asp:ListItem Selected="True">Port</asp:ListItem>
                                                                    <asp:ListItem>Voyage</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td style="text-align: right; padding-right: 5px; padding-top: 5px; width:205px" 
                                                                valign="top">
                                                                <asp:Label runat="server" ID="lblInsname" Text="Inspector Name :"></asp:Label> </td>
                                                            <td style="text-align: left; padding-top: 5px; width: 188px;" valign="top">
                                                                <asp:TextBox ID="txtInspectorName" runat="server" CssClass="input_box" Width="164px"></asp:TextBox>
                                                            </td>
                                                            <td style="text-align: right; padding-right: 5px; padding-top: 5px; width:90px" 
                                                                valign="top">
                                                                &nbsp;</td>
                                                            <td style="text-align: left; padding-top: 5px; padding-right:18px" valign="top">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-right: 5px; text-align: right; padding-top: 5px; padding-bottom:5px; width:264px" 
                                                                valign="top">
                                                                From Port :</td>
                                                            <td style="text-align: left; padding-top: 5px; width: 250px;" valign="top">
                                                                <asp:TextBox ID="txtfromport" runat="server" AutoPostBack="True" 
                                                                    CssClass="input_box" OnTextChanged="txtfromport_TextChanged" Width="164px"></asp:TextBox>
                                                            </td>
                                                            <td style="padding-right: 5px; text-align: right; padding-top: 5px; padding-bottom:5px; width: 112px;">
                                                                To Port :</td>
                                                            <td style="width: 200px; text-align: left" >
                                                                <asp:TextBox ID="txttoport" runat="server" CssClass="input_box" Width="188px"></asp:TextBox>
                                                            </td>
                                                            <td style="text-align: right; padding-right: 5px; padding-top: 5px; width:205px" 
                                                                valign="top">
                                                                &nbsp;</td>
                                                            <td style="text-align: left; padding-top: 5px; width: 188px;" valign="top">
                                                                &nbsp;</td>
                                                            <td style="text-align: right; padding-right: 5px; padding-top: 5px; width:90px" 
                                                                valign="top">
                                                                &nbsp;</td>
                                                            <td style="text-align: left; padding-top: 5px; padding-right:18px" valign="top">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding-right: 5px; text-align: right; width: 264px;" valign="top">
                                                                Remarks :</td>
                                                            <td colspan="7" style="text-align: left; padding-right:10px">
                                                                <asp:TextBox ID="txtremark" runat="server" CssClass="input_box" Height="98px" Rows="10"
                                                                    TextMode="MultiLine" Width="955px"></asp:TextBox></td>
                                                                
                                                        </tr>
                                                    </table>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy"
                                                        PopupButtonID="ImageButton2" PopupPosition="TopRight" TargetControlID="txtplandate">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <%--<ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" AutoComplete="false"
                                                        ClearMaskOnLostFocus="true" ClearTextOnInvalid="true" Mask="99/99/9999" MaskType="Date"
                                                        TargetControlID="txtplandate">
                                                    </ajaxToolkit:MaskedEditExtender>--%>
                                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" MinimumPrefixLength="1" TargetControlID="txtfromport" ServicePath="WebService.asmx" ServiceMethod="GetPortTitles" runat="server" DelimiterCharacters="" Enabled="True">
                                                    </cc1:AutoCompleteExtender>
                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" MinimumPrefixLength="1" TargetControlID="txttoport" ServicePath="WebService.asmx" ServiceMethod="GetPortTitles" runat="server" DelimiterCharacters="" Enabled="True">
                                                </cc1:AutoCompleteExtender>
                                                </fieldset>
                           <table width="100%" cellpadding="0" cellspacing="0" border="0px">
                                    <tr>
                                        <td style="text-align: left; padding-left: 10px;">Assign Superintendent : &nbsp;<asp:DropDownList ID="ddlsup" runat="server" CssClass="input_box" Width="200px"></asp:DropDownList></td>
                                        <td style="text-align: left; padding-right: 0px;">Superintendent is attending the Inspection : &nbsp;<asp:DropDownList ID="ddlattendinspection" Width="50px" runat="server" CssClass="input_box">
                                                <asp:ListItem>Yes</asp:ListItem>
                                                <asp:ListItem Selected="True">No</asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;
                                            <asp:Button ID="btnadd" runat="server" CssClass="btn" OnClientClick="return check();"  Text="Add" OnClick="btnadd_Click" Width="59px" /></td>
                                        <td style="padding-right: 20px; text-align: left"><asp:CheckBox ID="chkrequest" runat="server" Text="Inspection Requested" CausesValidation="True" /></td>
                                        <td style="padding-right: 20px; text-align: right"><asp:Label runat="server" ID="lblSire" Text="SIRE Inspection :"></asp:Label></td>
                                        <td style="padding-right: 20px; text-align: left"><asp:DropDownList ID="ddlIsSire" Width="50px" runat="server" CssClass="input_box">
                                                <asp:ListItem Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                                <asp:ListItem Value="N" Text="No"></asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td align="left" style="padding-right: 25px; text-align:right">
                                            <asp:Button runat="server" ID="btnBack" Text="Go Back" CssClass="btn"  OnClick='GoBackToPlan' />
                                            <asp:Button runat="server" ID="btnCancelPlan" Text="Cancel Planning" CssClass="btn"  OnClick='btnCancelPlan_Click' OnClientClick="return window.confirm('Are you sure to cancel this inspection?')" />
                                            <asp:CheckBox ID="chk_OnHold" runat="server" Text="On Hold" style='display:none' />
                                        
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="height:160px; padding-left:10px" >
                                                        <asp:GridView ID="grdinspector" DataKeyNames="Id"  runat="server" AllowSorting="True"  AutoGenerateColumns="False"
                                                            GridLines="Horizontal" Style="text-align: center" Width="98%"   OnRowDeleting="grdinspector_RowDeleting" OnRowDataBound="grdinspector_RowDataBound">
                                                            <Columns>
                                                                <asp:BoundField DataField="Name" HeaderText="Supt.">
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                        
                                                                </asp:BoundField>
                                                                   
                                                                <asp:TemplateField HeaderText="Attending Inspection">
                                                                    <ItemTemplate>
                                                                    <asp:Label id=lbl runat=server Text='<%#Eval("Status")%>' style=display:none></asp:Label>
                                                                        <asp:Label ID="lblyes" Text='<%# (Eval("Attending").ToString().Trim()!="")?(((Eval("Attending").ToString().Trim()=="True") || (Eval("Attending").ToString().Trim()=="Yes"))?"Yes":"No"):"" %>' runat="server" ></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <span style='<%=((strInsp_Status.ToString()=="Planned")?"":"display:none")%>'>
                                                                            <asp:ImageButton id="imgbtn" runat="server" CommandName="Delete"  ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="return window.confirm('Are you sure to delete this?');"/>  
                                                                        </span>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <RowStyle CssClass="rowstyle" />
                                                            <SelectedRowStyle CssClass="selectedtowstyle" />
                                                            <%--<HeaderStyle CssClass="headerstylefixedheader" ForeColor="#0E64A0" />--%>
                                                            <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                                        </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            </td>
                                    </tr>
                                </table>
                        <table cellpadding="0" cellspacing="0" style="width: 100%; padding-bottom:10px; padding-right:10px">
                        <tr>
                        <td style="padding-right: 5px; text-align: right">Planned By:</td>
                        <td style="text-align: left"><asp:TextBox ID="txtCreatedBy_DocumentType" runat="server" BackColor="Gainsboro"
                                CssClass="input_box" ReadOnly="True" TabIndex="-1" Width="154px"></asp:TextBox></td>
                        <td style="padding-right: 5px; text-align: right">
                            Planned On:</td>
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
                        <td align="right" style="padding-right:15px; text-align:right;">
                            <asp:Button ID="btnsave" OnClientClick="return checkform();"  runat="server" CssClass="btn" Text="Save" OnClick="btnsave_Click" Width="59px" />&nbsp;
                            <asp:Button id="btn_Notify" runat="server" CssClass="btn" OnClick="btn_Notify_Click" Text="Notify" Width="59px" Visible="false" />
                            <asp:Button ID="btn_Print" runat="server" CssClass="btn" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_pnl_Planning');" Visible="false"/>
                            <asp:Button id="vtn_VNotify" runat="server" CssClass="btn" OnClick="vtn_VNotify_Click" Text="Vessel Notify" Width="110px" OnClientClick="this.value='Please wait..'; this.style.color='gray';" Visible="false" />
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
                <div style="position: absolute; top: 25px; left: 100px; height: 150px; width: 50%;" id="dvConfirmationBox" runat="server" visible="false">
            <center>
                <div style="position: absolute; top: 25px; left: 220px; height: 150px; width: 50%; background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)"></div>
                <div style="position: relative; width: 50%; height: 150px; padding: 3px; text-align: center; background: white; z-index: 150; top: 25px; border: solid 1px Black">
                <div id="dialog2"  style="font-family:Arial;font-size:12px;">
                    <div style="font-size:15px;font-weight:bold;padding:4px;" class="text headerband">
                        <b><asp:Label ID="lblMsgHeader" runat="server" Text="Confirmation" /></b>
                        <asp:ImageButton ID="btnCloseExportPopup" runat="server" OnClick="btnCloseExportPopup_OnClick"  ImageUrl="~/Modules/HRD/Images/Close.gif" style="float:right;"/>
                    </div>
                       
                        <br />
                        <table style="padding-left:10px;font-family:Arial;font-size:12px;"><tbody><tr><td><asp:Label id="txtcomments" runat="server" Text="Please Confirm : This SUPT is already assigned is coming on all the inspection.It should come just as warning during planning the inspection if same inspector is planned on other inspection on same date."></asp:Label> </td></tr></tbody></table><br />
                    <asp:Button ID="btnYes" runat="server" Text="Yes" CssClass="btn" OnClick="btnYes_Click" Width="48px" CausesValidation="false"/>
                    <asp:Button ID="btnNo" runat="server" Text="No" CssClass="btn" Width="48px" CausesValidation="false" OnClick="btnNo_Click" />
                </div>
                </div>
                </center>
                </div><!-- Mask to cover the whole screen -->
                <div id="mask"></div>
 </form>
</body>
</html>