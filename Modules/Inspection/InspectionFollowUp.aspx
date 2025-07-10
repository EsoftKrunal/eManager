<%@ Page Language="C#" AutoEventWireup="true"   CodeFile="InspectionFollowUp.aspx.cs" Inherits="Transactions_InspectionFollowUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>EMANAGER</title>
       <link href="../HRD/Styles/style.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" href="../HRD/Styles/StyleSheet.css" />
<script language="javascript" type="text/javascript">
month = "Jan,Feb,Mar,Apr,May,Jun,Jul,Aug,Sep,Oct,Nov,Dec".split(",");
function checkDate(theField)
{
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
if(document.getElementById('ctl00_ContentPlaceHolder1_txttargetclosedt').value=='')
        {
            alert("Please Enter Target Closed Date!");
            document.getElementById('ctl00_ContentPlaceHolder1_txttargetclosedt').focus();
            return false;
        }
if(!checkDate(document.getElementById('ctl00_ContentPlaceHolder1_txttargetclosedt')))
    return false;
//    alert(document.getElementById('ctl00_ContentPlaceHolder1_rdbclosed').selectedvalue);
//        if(document.getElementById('ctl00_ContentPlaceHolder1_txtcloseddt').value=='')
//        {
//            alert("Please Enter Closed Date!");
//            document.getElementById('ctl00_ContentPlaceHolder1_txtcloseddt').focus();
//            return false;
//        }
//        if(!checkDate(document.getElementById('ctl00_ContentPlaceHolder1_txtcloseddt')))
//        return false; 
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
</script>
<script language="javascript" type="text/javascript">
    function PrintDefRPT()
    {
        var InspDueId=document.getElementById('ctl00_ContentPlaceHolder1_HiddenField_InspId').value;
        //alert(InspDueId);
        if(!(parseInt(InspDueId)==0 || InspDueId==""))
        {
            window.open('..\\Reports\\DefonFollowUp_Report.aspx?InspId='+ InspDueId,null,'title=no,resizable=yes,toolbars=no,scrollbars=yes,width=850,height=650,left=20,top=20,addressbar=no');
        }
    }
    </script>
     </head>
<body  >
<form id="form1" runat="server" >
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<asp:Panel id="pnl_FollowUp" runat="server">
<table border="0" cellpadding="0" style=" background-color:#f9f9f9; vertical-align: top; border-right: #8fafdb 1px solid; border-top: #8fafdb 0px solid;
                                    border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: right; padding:5px 10px 5px 10px;" cellspacing="0" width="100%">
                        <tr><td style="padding-right:10px; text-align:center; color:Red"></td></tr>
                        <tr>
                            <td>
                             
                                <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                    border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center;">
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0" style="padding-right:5px">
                                        <tr>
                                            <td style="padding-right: 10px; text-align: right" width="10%">
                                            </td>
                                            <td style="text-align: left; height:8px" width="15%">
                                                </td>
                                            <td style="padding-right: 10px; text-align: right" width="10%">
                                            </td>
                                            <td style="text-align: left" width="15%">
                                            </td>
                                            <td style="padding-right: 10px; text-align: right" width="10%">
                                            </td>
                                            <td style="text-align: left" width="15%">
                                            </td>
                                            <td style="padding-right: 10px; text-align: right" width="10%">
                                            </td>
                                            <td style="text-align: left" width="15%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-right: 10px; text-align: right;" width="10%">
                                                Insp#:</td>
                                            <td style="text-align: left;" width="15%">
                                                <asp:TextBox ID="txtinspno" runat="server" CssClass="input_box" Width="158px" ReadOnly="True"></asp:TextBox></td>
                                            <td style="padding-right: 10px; text-align: right;" width="10%">
                                                Vessel Name:</td>
                                            <td style="text-align: left;" width="15%">
                                                <asp:TextBox ID="txtvessel" runat="server" CssClass="input_box" Width="158px" ReadOnly="True"></asp:TextBox></td>
                                            <td style="padding-right: 10px; text-align: right;" width="10%">
                                                Insp Name:</td>
                                            <td style="text-align: left;" width="15%">
                                                <asp:TextBox ID="txtinspname" runat="server" CssClass="input_box" Width="158px" ReadOnly="True"></asp:TextBox></td>
                                            <td style="padding-right: 10px; text-align: right;" width="10%">
                                                Done Dt:</td>
                                            <td style="text-align: left;" width="15%">
                                                <asp:TextBox ID="txtdonedt" runat="server" CssClass="input_box" Width="158px" ReadOnly="True"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="padding-right: 10px; text-align: right" width="10%" >
                                                                                Port Done:</td>
                                            <td style="text-align: left" >
                                                <asp:TextBox ID="txtportdone" runat="server" CssClass="input_box" Width="158px" ReadOnly="True"></asp:TextBox></td>
                                            <td style="padding-right: 10px; text-align: right" width="10%" >
                                                                    Master:</td>
                                            <td style="text-align: left" >
                                                <asp:TextBox ID="txtmaster" runat="server" CssClass="input_box" Width="158px" ReadOnly="True"></asp:TextBox></td>
                                            <td style="padding-right: 10px; text-align: right" >
                                                Chief Office:</td>
                                            <td style="text-align: left" >
                                                <asp:TextBox ID="txtchiefofficer" runat="server" CssClass="input_box" Width="158px" ReadOnly="True"></asp:TextBox></td>
                                            <td style="padding-right: 10px; text-align: right" >
                                                                    2nd Office:</td>
                                            <td style="text-align: left" >
                                                <asp:TextBox ID="txtsecofficer" runat="server" CssClass="input_box" Width="158px" ReadOnly="True"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="padding-right: 10px; text-align: right" width="10%">
                                                C/E.:</td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtchiefengg" runat="server" CssClass="input_box" Width="158px" ReadOnly="True"></asp:TextBox></td>
                                            <td style="padding-right: 10px; text-align: right" width="10%">
                                                1st A/E:</td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtfirstassistant" runat="server" CssClass="input_box" Width="158px" ReadOnly="True"></asp:TextBox></td>
                                            <td style="padding-right: 10px; text-align: right">
                                                Inspector:</td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtinspector" runat="server" CssClass="input_box" Width="158px" ReadOnly="True"></asp:TextBox></td>
                                           <td style="padding-right: 10px; text-align: right" width="10%">
                                                                    MTM Supt:</td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txtmtmsupt" runat="server" CssClass="input_box" Width="158px" ReadOnly="True"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="padding-right: 10px; text-align: right" width="10%">
                                                Status:</td>
                                            <td style="text-align: left">
                                                <asp:TextBox id="txt_Status" runat="server" CssClass="input_box" ReadOnly="True"
                                                    Width="158px">
                                                </asp:TextBox></td>
                                            <td style="padding-right: 10px; text-align: right" width="10%">
                                            </td>
                                            <td style="text-align: left">
                                            </td>
                                            <td style="padding-right: 10px; text-align: right">
                                            </td>
                                            <td style="text-align: left">
                                            </td>
                                            <td>
                                            </td>
                                            <td style="text-align: left">
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                                </td>
                                </tr>
                                <tr runat="server" id="tr_Normal">
                                <td style="padding-top:5px">
                             
                                                    <fieldset style="border-right: #8fafdb 1px solid; padding-right: 5px; border-top: #8fafdb 1px solid;
                                                        padding-left: 5px; padding-bottom: 5px; border-left: #8fafdb 1px solid; padding-top: 0px;
                                                        border-bottom: #8fafdb 1px solid; text-align: center">
                                                        <table width="100%" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td colspan="6" style="padding-right: 10px; text-align: left; height: 15px;">
                                                                    List of Deficiency</td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" rowspan="7" style="text-align: left" width="25%" valign="top">
                                                                    <asp:ListBox ID="lst_Observation" runat="server" Height="265px" Width="238px" AutoPostBack="True" OnSelectedIndexChanged="lst_Observation_SelectedIndexChanged"></asp:ListBox></td>
                                                                <td style="padding-right: 10px; text-align: right; width: 14%;" >
                                                                    Cause:</td>
                                                                <td colspan="3" style="text-align: left; width: 845px;">
                                                                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                    border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; width: 618px;">
                                                                        <asp:CheckBoxList ID="rdbflaws" runat="server" RepeatDirection="Horizontal" Width="493px">
                                                                            <asp:ListItem>People</asp:ListItem>
                                                                            <asp:ListItem>Process</asp:ListItem>
                                                                            <asp:ListItem>Equipment</asp:ListItem>
                                                                        </asp:CheckBoxList>
                                                                      </fieldset>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding-right: 10px; width: 14%; text-align: right" valign="top">
                                                                    Deficiency:</td>
                                                                <td colspan="3" style="width: 845px; text-align: left">
                                                                    <asp:TextBox id="txt_Observation" runat="server" CssClass="input_box" Height="41px"
                                                                        TextMode="MultiLine" Width="616px">
                                                                    </asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding-right: 10px; text-align: right; width: 14%;" valign="top">
                                                                    Corrective Actions:</td>
                                                                <td colspan="3" style="text-align: left; width: 845px;">
                                                                    <asp:TextBox ID="txtcorrective" runat="server" CssClass="input_box" Width="616px" Height="41px" TextMode="MultiLine"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding-right: 10px; text-align: right; width: 14%;">
                                                                    Target Closer Dt:</td>
                                                                <td colspan="3" style="text-align: left; width: 845px;" valign="top">
                                                                    <table cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td>
                                                                    <asp:TextBox ID="txttargetclosedt" runat="server" CssClass="input_box" Width="122px"></asp:TextBox></td>
                                                                            <td>
                                                                                &nbsp;<asp:ImageButton id="ImageButton2" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif">
                                                                    </asp:ImageButton></td>
                                                                            <td>
                                                                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;</td>
                                                                            <td>
                                                                                Responsibility:</td>
                                                                            <td colspan="2">
                                                                                <asp:CheckBoxList id="chklst_Responsibility" runat="server" RepeatDirection="Horizontal"
                                                                                    Width="162px">
                                                                                    <asp:ListItem>Vessel</asp:ListItem>
                                                                                    <asp:ListItem>Office</asp:ListItem>
                                                                                </asp:CheckBoxList></td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding-right: 10px; text-align: right; width: 14%;">
                                                                    Closed:</td>
                                                                <td colspan="3" style="width: 845px; text-align: left">
                                                                    <table cellpadding="0" cellspacing="0" border="0">
                                                                    <tr>
                                                                    <td>
                                                                        <asp:RadioButtonList ID="rdbclosed" runat="server" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                            <asp:ListItem Value="0">No</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                        </td>
                                                                    <td>
                                                                    <%--Closure Evidence :--%>
                                                                    <asp:Button ID="btnColsureEvidence" runat="server" Text="Upload/View Document" OnClick="btnColsureEvidence_OnClick"  CssClass="input_box" Width="150px" />
                                                                    </td>
                                                                    <td style="display:none;"> 
                                                                    <ajaxToolkit:AsyncFileUpload OnClientUploadError="uploadError"
                                                                     OnClientUploadComplete="uploadComplete" runat="server"
                                                                     ID="flp_COCUpload" Width="300px" UploaderStyle="Modern"
                                                                     UploadingBackColor="#CCFFFF" ThrobberID="myThrobber" Visible=false/>
                                                                     <a runat="server" target="_blank"  id="a_file">
                                                                     <img style=" border:none;display:none;"  src="../Modules/HRD/Images/paperclip.gif" alt="Attachment" /> </a>
                                                                     </td>
                                                                    </tr> 
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding-right: 10px; text-align: right; width: 14%;">
                                                                    Closed Dt:</td>
                                                                <td colspan="3" style="width: 845px; text-align: left">
                                                                    <asp:TextBox ID="txtcloseddt" runat="server" CssClass="input_box" Width="122px"></asp:TextBox>
                                                                    <asp:ImageButton id="ImageButton3" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif">
                                                                    </asp:ImageButton></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding-right: 10px; text-align: right; width: 14%;" valign="top">
                                                                    Remarks:</td>
                                                                <td colspan="3" style="width: 845px; text-align: left">
                                                                    <asp:TextBox ID="txtremark" runat="server" CssClass="input_box" Height="48px" MaxLength="250"
                                                                        Rows="10" TextMode="MultiLine" Width="616px"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="6" style="padding-right: 0px; height: 42px; text-align: right">
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy"
                                                                        PopupButtonID="ImageButton2" PopupPosition="TopRight" TargetControlID="txttargetclosedt">
                                                                    </ajaxToolkit:CalendarExtender>
                                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                                                        PopupButtonID="ImageButton3" PopupPosition="TopRight" TargetControlID="txtcloseddt">
                                                                    </ajaxToolkit:CalendarExtender>
                                                                    <asp:HiddenField ID="HiddenField_InspId" runat="server" />
                                                                
                                                                    <table cellpadding="0" cellspacing="0" width="100%" >
                                                                        <tr>
                                                                            <td style="text-align: left" width="12%">
                                                                    Total Deficiencies:<asp:Label ID="lbl_TotalDef" runat="server">0</asp:Label></td>
                                                                            <td style="text-align: left">
                                                                    Open Deficiencies:<asp:Label ID="lblopen" runat="server">0</asp:Label></td>
                                                                            <td align="right" style="text-align: left">
                                                                                <asp:Label ID="lblmessage" runat="server" ForeColor="#C00000"></asp:Label></td>
                                                                            <td align=right>
                                                                    <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" OnClick="btnSave_Click" Width="59px" />&nbsp;
                                                                    <asp:Button id="btnNotify" runat="server" CssClass="btn" Text="Notify" Width="59px" OnClick="btnNotify_Click" Enabled="False" />
                                                                                <asp:Button id="btn_Print" runat="server" CssClass="btn" Text="Print" Width="59px" OnClientClick="return PrintDefRPT();" /><%-- javascript:CallPrint('ctl00_ContentPlaceHolder1_pnl_FollowUp');--%>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            
                                                        </table>
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr>
                                                        <td style="text-align: left"><%--padding-right: 8px; --%>
                                                            <asp:Label ID="lbl_GridView_DocumentType" runat="server" Text=""></asp:Label></td>
                                                        <td style="padding-right: 8px; text-align: right">
                                                            Created By:</td>
                                                        <td style="text-align: left">
                                                            <asp:TextBox ID="txtCreatedBy_DocumentType" runat="server" BackColor="Gainsboro"
                                                                CssClass="input_box" ReadOnly="True" TabIndex="-1" Width="154px"></asp:TextBox></td>
                                                        <td style="padding-right: 8px; text-align: right">
                                                            Created On:</td>
                                                        <td style="text-align: left">
                                                            <asp:TextBox ID="txtCreatedOn_DocumentType" runat="server" BackColor="Gainsboro"
                                                                CssClass="input_box" ReadOnly="True" TabIndex="-2" Width="82px"></asp:TextBox></td>
                                                        <td style="padding-right: 8px; text-align: right">
                                                            Modified By:</td>
                                                        <td style="text-align: left">
                                                            <asp:TextBox ID="txtModifiedBy_DocumentType" runat="server" BackColor="Gainsboro"
                                                                CssClass="input_box" ReadOnly="True" TabIndex="-3" Width="154px"></asp:TextBox></td>
                                                        <td style="padding-right: 8px; text-align: right">
                                                            Modified On:</td>
                                                        <td style="text-align: left;" align="left">
                                                            <asp:TextBox ID="txtModifiedOn_DocumentType" runat="server" BackColor="Gainsboro"
                                                                CssClass="input_box" ReadOnly="True" TabIndex="-4" Width="82px"></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                
                            </td>
                                </tr>
                                <tr runat="server" id="tr_MTM">
                            <td style="padding-top:5px">
                                <fieldset style="BORDER-RIGHT: #8fafdb 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #8fafdb 1px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; BORDER-LEFT: #8fafdb 1px solid; PADDING-TOP: 0px; BORDER-BOTTOM: #8fafdb 1px solid; TEXT-ALIGN: center"><LEGEND>
                                        <strong>Observations</strong></LEGEND>
                                        <table  width="100%">
                                        <tr>
                                        <td style="vertical-align: top;">
                                        <div style=" height:22px; overflow-y:scroll; overflow-x:hidden; background-color:#c2c2c2;">

                                        <table cellpadding="0" cellspacing="0" border="1" width="100%" rules="all" style="border-collapse:collapse;">
                                        <colgroup>
                                            <col width='30px' />
                                            <col/>
                                            <col width='140px' />
                                            <col width='180px' />
                                            <col width='50px' />
                                            <col width='17px' />
                                        </colgroup>
                                        <tr class= "headerstylegrid" style="font-weight:bold;" >
                                            <td style="text-align:center;" >Sr#</td>
                                            <td>&nbsp;Deficiency</td>
                                            <td style="text-align:center;" >Target Closure Dt.</td>
                                            <td style="text-align:center;" >Closed By/On</td>
                                            <td style="text-align:center;" >Action</td>
                                            <td style="text-align:center;" >&nbsp;</td>
                                        </tr>
                                        </table>
                                        </div>
                                        <div style=" height:290px; overflow-y:scroll; overflow-x:hidden;">
                                        <table cellpadding="0" cellspacing="0" border="1" width="100%" style=" border-collapse:collapse">
                                      <colgroup>
                                            <col width='30px' />
                                            <col/>
                                            <col width='140px' />
                                            <col width='180px' />
                                            <col width='50px' />
                                            <col width='17px' />
                                        </colgroup>
                                        <asp:Repeater runat="server" ID="rptList">
                                        <ItemTemplate>
                                            <tr>
                                                <td style="text-align:center;" ><%#Eval("SrNo").ToString()%></td>
                                                <td>&nbsp;<%#Eval("Deficiency").ToString()%></td>
                                                <td style="text-align:center;" ><%# Common.ToDateString( Eval("TCLDate").ToString())%></td>
                                                <td style="text-align:center;" >
                                                    <%#Eval("ClosedBy").ToString()%>  
                                                    <%# Common.ToDateString(Eval("ClosedOn").ToString())%> 
                                                </td>
                                                <td style="text-align:center;" >
                                                    <asp:LinkButton ID="btnClosurePopup" runat="server" Text="Close" OnClick="btnClosurePopup_OnClick" CommandArgument='<%#Eval("TableID")%>' ToolTip=" Closure " Visible='<%# (Eval("Closure").ToString()=="No") %>' ForeColor="Red" /> 
                                                    <asp:LinkButton ID="btnViewClosure" ForeColor="Blue" runat="server" Text="View" ImageUrl="~/Modules/HRD/Images/HourGlass.gif" OnClick="btnViewClosure_OnClick" CommandArgument='<%#Eval("TableID")%>' ToolTip=" View Closure " Visible='<%# (Eval("Closure").ToString()=="Yes") %>' />
                                                </td>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </ItemTemplate>
                                        </asp:Repeater>
                                        
                                        </table>
                                        </div>
                                        </td>
                                        </tr>
                                        </table>
                                </fieldset> 
                            </td>
                        </tr>
        </table>
        
        
        
        <div id="DivClosureDocs"  style="position:absolute;top:0px;left:0px; height :470px; width:100%;z-index:100;" runat="server" visible="false" >
        <center>
            <div style="position:absolute;top:0px;left:0px; height :700px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:1100px; height:350px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
                <table cellpadding="2" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td>
                            <fieldset style="width:99%;" >
                                <legend style="font-weight: bold">Documents</legend>
                                 <table  id="tblDocument" runat="server"  border="0" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                        <colgroup>
                                            <col />
                                            <col width="250px"/>
                                            <tr style="font-weight:bold;">
                                                <td style="text-align:left">
                                                    Description :</td>
                                                <td align="right">
                                                    Attachment</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align:left">
                                                    <asp:TextBox ID="txt_Desc" runat="server" CssClass="input_box" MaxLength="50" 
                                                        Width="550px"></asp:TextBox>
                                                </td>
                                                <td style="text-align:left">
                                                    <asp:FileUpload ID="flAttachDocs" runat="server" CssClass="input_box" 
                                                        Width="200px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="text-align:right">
                                                    <asp:Label ID="lblMsgDoc" runat="server" ForeColor="Red"></asp:Label>
                                                    <asp:Button ID="btnSaveDoc" runat="server" CausesValidation="true" 
                                                        CssClass="btn" onclick="btnSaveDoc_Click" Text="Save Document" />
                                                    <%--<asp:Button ID="btnCancelDoc" Text="Cancel" CssClass="btn" runat="server" 
                                             CausesValidation="true" onclick="btnCancelDoc_Click" />--%>
                                                </td>
                                            </tr>
                                        </colgroup>
                                 </table>
                                 <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;margin-top:10px;">
                                    <colgroup>
                                        <col style="width: 30px;" />
                                        <col style="width: 50px;" />
                                        <col />
                                        <col style="width: 150px;" />
                                        <col style="width: 100px;"/>
                                        <col style="width: 50px;"/>
                                        <col style="width: 17px;" />
                                        <tr align="center" class= "headerstylegrid" style="font-weight:bold;">                                            
                                            <td>Edit</td>
                                            <td>Delete</td>
                                            <td align="left">
                                                Description
                                            </td>
                                            <td>
                                                Uploaded By
                                            </td>
                                            <td>
                                                Uploaded On
                                            </td>
                                            <td style="text-align:center;">    
                                                <img src="../Modules/HRD/Images/paperclip.gif" style="border:none"  />
                                            </td>
                                            <td></td>
                                        </tr>
                                    </colgroup>
                                </table>
                                
                                <div id="dvDocs" style="overflow-y: scroll;overflow-x: hidden; width: 100%; height: 192px; text-align: center;">
                                    <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width: 100%;                                       border-collapse: collapse;">
                                        <colgroup>
                                            <col style="width: 30px;" />
                                            <col style="width: 50px;" />
                                            <col />
                                            <col style="width: 150px;" />
                                            <col style="width: 100px;"/>
                                            <col style="width: 50px;"/>
                                            
                                        </colgroup>
                                        <asp:Repeater ID="rptDocs" runat="server">
                                            <ItemTemplate>
                                                <tr class="row">
                                                    <td style="text-align:center;">    
                                                        <asp:ImageButton ID="imgEditDoc" runat="server" ImageUrl="~/Modules/HRD/Images/edit.jpg" OnClick="imgEditDoc_OnClick" />  <%--Visible='<%#(Mode!="V") %>' --%>
                                                        <asp:HiddenField ID="hfDocID" runat="server" Value='<%#Eval("DocID") %>' />
                                                    </td>
                                                    <td style="text-align:center;">    
                                                        <asp:ImageButton ID="imgDelDoc" runat="server" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClick="imgDelDoc_OnClick" OnClientClick="return confirm('Are you sure to delete?')" />  <%--Visible='<%#(Mode!="V") %>'--%>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblDesc" runat="server" Text='<%#Eval("Description")%>'></asp:Label>                                                        
                                                    </td>
                                                    <td align="center">
                                                      <%#Eval("UploadedBy")%>
                                                    </td>
                                                    <td align="right"> 
                                                       <%#Eval("UploadedDate")%>
                                                    </td>
                                                    <td style="text-align:center;">    
                                                        <a runat="server" ID="ancdoc"  href='<%#"~/EMANAGERBLOB/Inspection\\Observation\\" + Eval("FileName").ToString() %>' target="_blank"  title="Show Doc" visible='<%#Eval("FileName")!=""%>' >
                                                         
                                                       <img src="../Modules/HRD/Images/paperclip.gif" style="border:none"  /></a>  
                                                    </td>
                                                    <%=(Request.UserAgent.Contains("MSIE 7.0")) ? "<td style='width:17px'></td>" : ""%>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        
                                    </table>
                                </div>
                                
                                
                                            
                            </fieldset>
                        </td>
                    </tr>
                    
                </table>
                <asp:Button ID="btnCloseDocuments" runat="server" Text="Close" CssClass="input_box" OnClick="btnCloseDocuments_OnClick"  style="float:right;width:80px; margin-right:3px;" />
            </div>
            
        </center>
       
     </div>

      <%--Add Closure Deficiency--%>
        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="dvClosure" runat="server" visible="false">
            <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:800px; height:400px; padding :0px; text-align :center; border :solid 10px #FFD1A3 ; border-top:none; background : white; z-index:150;top:60px;opacity:1;filter:alpha(opacity=100)">
            <div>     
                <table cellpadding="1" cellspacing="0" style="width: 100%; ">
                <tr>
                    <td colspan="4" style="height: 23px;  text-align :center; color:Black; font-size:14px; " class="text headerband">Deficiency Details</td>
                </tr>
                <tr style="background-color:#FFD699">
                    <td colspan="4">
                        <asp:Label ID="Label1" runat="server" ForeColor="#C00000"></asp:Label>
                    </td>
                </tr>
                <tr style=" background-color:#F3F3F3">
                    <td style="text-align:right">&nbsp;Deficiency :</td>
                    <td colspan="3">
                        <asp:TextBox ID="txtDeficiency" runat="server" TextMode="MultiLine" Width="98%" Height="50px" CssClass="input_box" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr style=" background-color:#F3F3F3">
                    <td style="text-align:right">&nbsp;Corrective Action :</td>
                    <td colspan="3">
                        <asp:TextBox ID="txtCorrAction" runat="server" TextMode="MultiLine" Width="98%" Height="50px" CssClass="input_box" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr style=" background-color:#F3F3F3">
                    <td style="text-align:right">&nbsp;Target Closure Dt. :</td> 
                    <td>
                        <asp:TextBox ID="txtTCD" runat="server" Width="90px" CssClass="input_box"  ReadOnly="true"></asp:TextBox>
                        <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton1" PopupPosition="TopRight" TargetControlID="txtTCD"></ajaxToolkit:CalendarExtender>
                    </td>
                    <td>&nbsp;Responsibility :</td>
                    <td><asp:CheckBoxList ID="chklst_Respons" runat="server" RepeatDirection="Horizontal" Width="162px" Enabled="false" >
                        <asp:ListItem>Vessel</asp:ListItem>
                        <asp:ListItem>Office</asp:ListItem>
                    </asp:CheckBoxList></td>
                </tr>
                <tr style=" background-color:#F3F3F3">
                    <td style="text-align: right">Created By / On:</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtACreatedBy" runat="server" CssClass="input_box" ReadOnly="True"></asp:TextBox>-
                            <asp:TextBox ID="txtACreatedOn" runat="server" CssClass="input_box" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td style="padding-right: 5px; text-align: right">Modified By / On:</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtAModifiedBy" runat="server" CssClass="input_box" ReadOnly="True"></asp:TextBox>-
                            <asp:TextBox ID="txtAModifiedOn" runat="server" CssClass="input_box" ReadOnly="True"></asp:TextBox>
                        </td>
                </tr>
                 <tr>
                    <td colspan="4" style="height: 23px; background-color:#FFD1A3; text-align :center; color:Black; font-size:14px; ">Closure Details</td>
                </tr>
                <tr>
                    <td style="padding-right: 5px; text-align: right;">Closed Date :</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txt_ClosedDate" runat="server" CssClass="input_box" Width="122px" MaxLength="11"></asp:TextBox>&nbsp;
                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                    <td style="padding-right: 5px; text-align: right">Cause :</td>
                    <td style="text-align: left">
                        <asp:CheckBoxList ID="rdbflaws_C" runat="server" RepeatDirection="Horizontal" Width="311px">
                            <asp:ListItem>People</asp:ListItem>
                            <asp:ListItem>Process</asp:ListItem>
                            <asp:ListItem>Equipment</asp:ListItem>
                        </asp:CheckBoxList></td>
                </tr>
                <tr>
                    <td style="padding-right: 5px; text-align: right" valign="top">
                        Remarks :</td>
                    <td colspan="3" style="text-align: left">
                        <asp:TextBox ID="txt_ClosedRemarks" runat="server" CssClass="input_box" Height="100px" TextMode="MultiLine" Width="612px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="padding-right: 5px; text-align: right" valign="top" class="style3">
                        Closure Evidence :
                    </td>
                    <td style="text-align: left" class="style2" colspan="3" >
                        <asp:FileUpload runat="server" ID="fu_ClosureEvidence" Width="300px" CssClass="input_box" />
                        <a runat="server" target="_blank"  id="a1"><img style=" border:none"  src="../HRD/Images/paperclip12.gif" alt="Attachment"/> </a> 
                        &nbsp;<asp:Label runat="server" style="float:right" id="lblMs" ForeColor="Red" Font-Bold="true"></asp:Label>
                        </td>
                </tr>
                <tr>
                    <td class="style1">
                    </td>
                    <td style="text-align: left" class="style1">
                        
                     
                    </td>
                    <td style="text-align: left" class="style1">
                    </td>
                    <td style="text-align: left" class="style1">
                    </td>
                </tr>
                <tr>
                    <td style="padding-right: 5px; text-align: right">Closed By :
                    
                    </td>
                    <td style="text-align: left" colspan="3">
                        <asp:TextBox ID="txt_ClosedBy" runat="server" BackColor="Gainsboro" CssClass="input_box" ReadOnly="True"></asp:TextBox>-
                        <asp:TextBox ID="txt_ClosedOn" runat="server" BackColor="Gainsboro" CssClass="input_box" ReadOnly="True"></asp:TextBox>
                        <div style="float:right; margin-right:10px;">
                            <asp:Button ID="btnSaveClosure" runat="server" CssClass="btn" OnClick="btnSaveClosure_Click" Text="Save" Width="59px" CausesValidation="false" />
                            <asp:Button ID="btnCloseClosure" runat="server" CssClass="btn" OnClick="btnCloseClosure_Click" Text="Close" Width="59px" CausesValidation="false" /> 
                        </div>    
                    </td>
                </tr>
                
            </table>
            </div>
             </div>
             <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton2" PopupPosition="TopRight" TargetControlID="txt_ClosedDate"> </ajaxToolkit:CalendarExtender>
            </center>
        </div>       
     <asp:UpdatePanel ID="idww" runat="server" >
        <ContentTemplate>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveDoc" />
        </Triggers>
     </asp:UpdatePanel>
     
</asp:Panel>
  </form>
</body>
</html>