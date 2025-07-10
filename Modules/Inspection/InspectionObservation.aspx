<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="InspectionObservation.aspx.cs" Inherits="Transactions_InspectionObservation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %> 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
     <title>EMANAGER</title>
       <link href="../HRD/Styles/style.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" href="../HRD/Styles/StyleSheet.css" />
<script language="javascript" type="text/javascript">
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

  
  if(trimAll(document.getElementById("ctl00_ContentPlaceHolder1_txtquestionno").value)=="")
{
alert('Please Enter Question No!');
document.getElementById("ctl00_ContentPlaceHolder1_txtquestionno").focus();
return false;
}
}
    </script>
<script language="javascript" type="text/javascript">
function checkform1()
{
//    if(document.getElementById('ctl00_ContentPlaceHolder1_txtmaster').value=='')
//    {
//    alert("Please Enter Master!");
//    document.getElementById('ctl00_ContentPlaceHolder1_txtmaster').focus();
//    return false;
//    }
//    if(document.getElementById('ctl00_ContentPlaceHolder1_txtchiefengg').value=='')
//    {
//    alert("Please Enter C/E!");
//    document.getElementById('ctl00_ContentPlaceHolder1_txtchiefengg').focus();
//    return false;
//    }
    if(document.getElementById('ctl00_ContentPlaceHolder1_txtstartdate').value=='')
    {
    alert("Please Enter StartDate!");
    document.getElementById('ctl00_ContentPlaceHolder1_txtstartdate').focus();
    return false;
    }
    if(!checkDate(document.getElementById('ctl00_ContentPlaceHolder1_txtstartdate')))
    return false;
    if(document.getElementById('ctl00_ContentPlaceHolder1_txtdonedt').value=='')
    {
    alert("Please Enter Done Date!");
    document.getElementById('ctl00_ContentPlaceHolder1_txtdonedt').focus();
    return false;
    }
    if(!checkDate(document.getElementById('ctl00_ContentPlaceHolder1_txtdonedt')))
    return false;
    if(document.getElementById('ctl00_ContentPlaceHolder1_txtportdone').value=='')
    {
    alert("Please Enter Port Done!");
    document.getElementById('ctl00_ContentPlaceHolder1_txtportdone').focus();
    return false;
    }
//    if(document.getElementById('ctl00_ContentPlaceHolder1_txtresponseduedt').value=='')
//    {
//    alert("Please Enter Response Due Date!");
//    document.getElementById('ctl00_ContentPlaceHolder1_txtresponseduedt').focus();
//    return false;
//    }
//    if(!checkDate(document.getElementById('ctl00_ContentPlaceHolder1_txtresponseduedt')))
//    return false;
    var dtstrdt=document.getElementById('ctl00_ContentPlaceHolder1_txtstartdate').value; 
    //alert(dtstrdt);
	var dtdndt=document.getElementById('ctl00_ContentPlaceHolder1_txtdonedt').value;  
	//alert(dtdndt);
	    if (Date.parse(dtdndt.value) > Date.parse(dtstrdt.value)) 
		    {
			    alert("Done Date cannot be less than Start Date.");
			    dtdndt.focus();
			    return false;
		    }//var aa = document.getElementById('ctl00_ContentPlaceHolder1_HiddenField1').value;
        //alert(aa);
}
function DescPopUp()
    {
        var aa = document.getElementById('ctl00_ContentPlaceHolder1_txtquestionno').value;
        if(aa!="")
        {
            window.open('ResponseDescPopUp.aspx?Ques='+ aa + '&Ver=<%=ViewState["VersionId"].ToString()%>','asdf','title=no,resizable=no,location=no,width=400px,height=400px,top=190px,left=550px,addressbar=no,status=yes,scrollbars=yes');
        }
    }
    function PrintObsRPT()
    {
        var InspDueId=document.getElementById('ctl00_ContentPlaceHolder1_HiddenField_InspId').value;
        var MTMSptd=document.getElementById('ctl00_ContentPlaceHolder1_HiddenField_MTMSupt').value;
        //alert(InspDueId);
        if(!(parseInt(InspDueId)==0 || InspDueId==""))
        {
            window.open('..\\Reports\\InspObservation_Report.aspx?InspId='+ InspDueId +'&MTMSp='+ MTMSptd,null,'title=no,toolbars=no,scrollbars=yes,width=850,height=650,left=20,top=20,addressbar=no');
        }
    }
    function OpenLargeDefPopUp()
    {
        var YesNo=document.getElementById("ctl00_ContentPlaceHolder1_ddlyesno").value;
        if(YesNo!="NO")
        {
            alert("Please Select NO!");
            document.getElementById("ctl00_ContentPlaceHolder1_ddlyesno").focus();
            return false;
        }
        else
        {
            window.open('..\\Inspection\\Deficiency_PopUp.aspx',null,'title=no,toolbars=no,scrollbars=yes,width=680,height=480,left=20,top=20,addressbar=no');
        }
    }
    
     function AddEditCheckList()
            {
                var grpValue,mainValue,subValue;
                grpValue =document.getElementById('ctl00_ContentPlaceHolder1_ddl_InspGroup').value;   
                mainValue =document.getElementById('ctl00_ContentPlaceHolder1_ddlChapterName').value;   
                subValue =document.getElementById('ctl00_ContentPlaceHolder1_ddlSubChapter').value;   
                
                window.open('..\\Registers\\AddEditCheckListPopUp.aspx?GRP=' + grpValue + '&MC=' + mainValue + '&SC=' + subValue ,'','title=no,resizable=no,location=no,width=1000px,height=420px,top=100px,left=100px,addressbar=no,status=yes,scrollbars=1');
            }
    // added by pankaj verma on 23-08-2010
    // to open a popup window on update crew button click
    function OpenUpdateCrewPopUp()
    {
      window.open('..\\Inspection\\InspectionObservation_PopUp.aspx',null,'title=no,toolbars=no,scrollbars=yes,width=1000,height=200,left=20,top=20,addressbar=no');  
    }
    function FillQuestionNo(QueNo)
    { 
      if (document.getElementById("ctl00_ContentPlaceHolder1_txtquestionno").getAttribute("readonly")!="readonly")
      {
        document.getElementById("ctl00_ContentPlaceHolder1_txtquestionno").value = QueNo;
        document.getElementById("ctl00_ContentPlaceHolder1_txtquestionno").onchange();
      }
     return false;
    }
</script>
         <style type="text/css">
             .auto-style1 {
                 height: 58px;
             }
         </style>
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
<asp:UpdatePanel ID="up" runat="server" ChildrenAsTriggers="true">
    <triggers><asp:PostBackTrigger ControlID="btn_ImportCheckList"/></triggers>
        <contenttemplate>
<asp:Panel id="pnl_Observation" runat="server">

<table style="BORDER-RIGHT: #8fafdb 1px solid; BORDER-TOP: #8fafdb 0px solid; VERTICAL-ALIGN: top; BORDER-LEFT: #8fafdb 1px solid; BORDER-BOTTOM: #8fafdb 1px solid; BACKGROUND-COLOR: #f9f9f9; TEXT-ALIGN: right"  cellSpacing="0" cellPadding="0" width="100%" border=0>
<tbody>
<tr><td>

    <asp:HiddenField 
        id="HiddenField1" runat="server"></asp:HiddenField> 
    <asp:HiddenField 
        id="HiddenField_ObId" runat="server">
                        </asp:HiddenField>
                                             <asp:HiddenField 
        id="HiddenField_InspId" runat="server">
                        </asp:HiddenField>
                                             <asp:HiddenField id="HiddenField_TotalObs" runat="server"></asp:HiddenField>
                                             <asp:HiddenField 
        id="HiddenField_MTMSupt" runat="server"></asp:HiddenField>
    <asp:HiddenField id="HiddenField_ObsId" runat="server"></asp:HiddenField>
                                             <asp:HiddenField ID="HiddenField_ChapId" runat="server" /></td></tr>
                                         <tr>
    <td style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; PADDING-TOP: 2px">
        <fieldset style="BORDER-RIGHT: #8fafdb 1px solid; BORDER-TOP: #8fafdb 1px solid; BORDER-LEFT: #8fafdb 1px solid; BORDER-BOTTOM: #8fafdb 1px solid; TEXT-ALIGN: right">
                <table cellSpacing=0 cellPadding=0 width="100%">
                    <tbody>
                    <tr>
                    <td 
        style="PADDING-RIGHT: 10px; HEIGHT: 15px; TEXT-ALIGN: right" width="10%">Insp Name:</td>
                    <td style="HEIGHT: 15px; TEXT-ALIGN: left" width="15%">
                        <asp:TextBox id="txtinspno" runat="server" ReadOnly="True" Width="158px" CssClass="input_box">
                                    </asp:TextBox></td>
                    <td style="PADDING-RIGHT: 10px; HEIGHT: 15px; TEXT-ALIGN: right" width="10%">Last Done:</td>
                    <td style="HEIGHT: 15px; TEXT-ALIGN: left" width="15%">
                        <asp:TextBox id="txtvessel" runat="server" ReadOnly="True" Width="158px" CssClass="input_box">
                                    </asp:TextBox></td>
                    <td style="PADDING-RIGHT: 5px; TEXT-ALIGN: right; height: 15px;" 
        width="10%">Vessel Name:</td>
                    <td style="HEIGHT: 15px; TEXT-ALIGN: left" width="15%">
                        <asp:TextBox id="txtinspname" runat="server" ReadOnly="True" Width="158px" CssClass="input_box">
                                    </asp:TextBox></td>
                    <td style="PADDING-RIGHT: 10px; TEXT-ALIGN: right; height: 15px;" 
        width="10%">Insp#:</td>
                    <td style="HEIGHT: 15px; TEXT-ALIGN: left" width="15%">
                        <asp:TextBox id="txtlastdone" runat="server" ReadOnly="True" Width="158px" CssClass="input_box">
                                    </asp:TextBox></td></tr>
                     <tr>
                        <td 
        style="PADDING-RIGHT: 10px; TEXT-ALIGN: right; " width="10%">Planned Port:</td>
                        <td style="TEXT-ALIGN: left">
                            <asp:TextBox ID="txtplannedport" 
        runat="server" CssClass="input_box" ReadOnly="True" Width="158px">
                                    </asp:TextBox></td>
                        <td 
        style="PADDING-RIGHT: 10px; TEXT-ALIGN: right; " width="10%">
                            Planned Date:</td>
                        <td style="TEXT-ALIGN: left">
                            <asp:TextBox ID="txtplanneddate" runat="server" 
        CssClass="input_box" ReadOnly="True" Width="158px">
                                    </asp:TextBox></td>
                        <td style="PADDING-RIGHT: 5px; TEXT-ALIGN: right">Status:</td>
                        <td style="TEXT-ALIGN: left">
                            <asp:TextBox ID="txt_Status" runat="server" 
        CssClass="input_box" ReadOnly="True" Width="158px">
                                    </asp:TextBox></td>
                        <td style="PADDING-RIGHT: 5px; TEXT-ALIGN: right">Next Due:</td>
                        <td style="TEXT-ALIGN: left"><asp:TextBox id="txtnextdue" runat="server" ReadOnly="True" Width="158px" CssClass="input_box"></asp:TextBox>
                            </td></tr>
                            
                        </tbody></table>
        </fieldset> </td></tr>
        
                                         <tr>
    <td style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; PADDING-TOP: 2px">
        <fieldset style="BORDER-RIGHT: #8fafdb 1px solid; BORDER-TOP: #8fafdb 1px solid; BORDER-LEFT: #8fafdb 1px solid; BORDER-BOTTOM: #8fafdb 1px solid; TEXT-ALIGN: right">
                <table cellSpacing=0 cellPadding=0 width="100%">
                    <tbody>
                    <tr>
                        <td style="PADDING-RIGHT: 10px; TEXT-ALIGN: right;" width="10%">Start Dt. :</td>
                        <td style="TEXT-ALIGN: left"><asp:TextBox id="txtstartdate" runat="server" Width="84px" CssClass="required_box"></asp:TextBox> <asp:ImageButton id="ImageButton3" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" CausesValidation="False"></asp:ImageButton></td>
                        <td style="PADDING-RIGHT: 5px; TEXT-ALIGN: right">Done Dt :</td>
                        <td style="TEXT-ALIGN: left"><asp:TextBox id="txtdonedt" runat="server" Width="84px" CssClass="required_box" OnTextChanged="txtdonedt_TextChanged" AutoPostBack="True"></asp:TextBox><asp:ImageButton id="ImageButton2" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" CausesValidation="False"></asp:ImageButton></td>
                        <td style="PADDING-RIGHT: 10px; TEXT-ALIGN: right; " width="10%">Port Done :</td>
                        <td style="TEXT-ALIGN: left"><asp:TextBox id="txtportdone" runat="server" Width="210px" CssClass="required_box" OnTextChanged="txtportdone_TextChanged" AutoPostBack="True"></asp:TextBox></td>
                        <td style="PADDING-RIGHT: 5px; TEXT-ALIGN: right">Response Due Dt. :</td>
                        <td style="TEXT-ALIGN: left"><asp:TextBox id="txtresponseduedt" runat="server" Width="84px" CssClass="input_box" OnTextChanged="txtresponseduedt_TextChanged" AutoPostBack="True"></asp:TextBox><asp:ImageButton id="ImageButton1" runat="server" ImageUrl="~/Modules/HRD/Images/Calendar.gif" CausesValidation="False"></asp:ImageButton></td>
                        <td> <asp:Button id="Button1" onclick="btnSave_Click" runat="server" Width="59px" CssClass="btn" Text="Save" OnClientClick="return checkform1();"></asp:Button> </td>
                        </tr>
               </tbody></table>
               </fieldset>
               <ajaxToolkit:CalendarExtender id="CalendarExtender3" runat="server" TargetControlID="txtdonedt" PopupPosition="TopRight" PopupButtonID="ImageButton2" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>
               <ajaxToolkit:CalendarExtender id="CalendarExtender2" runat="server" TargetControlID="txtstartdate" PopupPosition="TopRight" PopupButtonID="ImageButton3" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>
               <ajaxToolkit:CalendarExtender id="CalendarExtender1" runat="server" TargetControlID="txtresponseduedt" PopupPosition="TopRight" PopupButtonID="ImageButton1" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>
               </td></tr>  
                                         <tr>
                            <td style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 2px; PADDING-TOP: 2px" vAlign=top>
                                <fieldset style="BORDER-RIGHT: #8fafdb 1px solid; PADDING-RIGHT: 5px; BORDER-TOP: #8fafdb 1px solid; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; BORDER-LEFT: #8fafdb 1px solid; PADDING-TOP: 0px; BORDER-BOTTOM: #8fafdb 1px solid; TEXT-ALIGN: center"><legend>
                                        <strong>Observation</strong></legend>
                                        <table  width="100%">
                                        <tr>
                                        <td style =" vertical-align: top; width :260px;">
                                        <table style=" text-align :left; width :100% " >
                                        <tr>
                                          <td>
                                              <asp:DropDownList ID="ddl_InspGroup" runat="server" AutoPostBack="True" 
                                                  CssClass="input_box" 
                                                  OnSelectedIndexChanged="ddl_InspGroup_SelectedIndexChanged" TabIndex="1" 
                                                  Width="100%">
                                              </asp:DropDownList></td>
                                      </tr>
                                        <tr>                          
                                          <td>
                                              <asp:DropDownList ID="ddlChapterName" runat="server" AutoPostBack="True" 
                                                  CssClass="input_box" 
                                                  OnSelectedIndexChanged="ddlChapterName_SelectedIndexChanged" TabIndex="2" 
                                                  Width="100%">
                                              </asp:DropDownList></td>                          
                                      </tr>
                                        <tr>
                                          <td>
                                              <asp:DropDownList ID="ddlSubChapter" runat="server" AutoPostBack="True" 
                                                  CssClass="input_box" 
                                                  onselectedindexchanged="ddlSubChapter_SelectedIndexChanged" TabIndex="3" 
                                                  Width="100%">
                                              </asp:DropDownList>
                                              </td>                          
                                      </tr>
                                        <tr>
                                          <td>
                                            <div style=" width :260px;height:240px; overflow-x:hidden; overflow-y:scroll" > 
                                            <asp:GridView ID="GridView_InspectionCheckList" runat="server" AutoGenerateColumns="False" GridLines="Both" onpageindexchanging="GridView_InspectionCheckList_PageIndexChanging" Width="100%">
                                            <RowStyle CssClass="rowstyle" />
                                            <Columns>
                                            <asp:TemplateField HeaderText="Qno#">
                                            <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnQueNo" runat="server" ToolTip='<%# Eval("Question")%>'  OnClientClick='<%# "return FillQuestionNo(\"" + Eval("QuestionNo") + "\");" %>' Text='<%# Eval("QuestionNo") %>'></asp:LinkButton></ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:TemplateField>                    
                                            <asp:TemplateField HeaderText="Question" >
                                            <ItemTemplate >
                                            <asp:Label runat="server" Text='<%# (Eval("Question").ToString().Length< 30)?Eval("Question").ToString():Eval("Question").ToString().Substring(0,25) + " ..."%>'></asp:Label>
                                            
                                                </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>                                      
                                            </asp:TemplateField>           
                                                </Columns>
                                            <pagerstyle horizontalalign="Center" />
                                            <SelectedRowStyle CssClass="selectedtowstyle" />
                                            <HeaderStyle CssClass="headerstylefixedheadergrid" />                     
                                                                 
                                                </asp:GridView>
                                            </div>
                                          </td>
                                        </tr>
                                        </table>
                                        </td>
                                        <td style="vertical-align: top;">
                                        <table style="width :100%">
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td style="text-align: center">
                                            <table cellpadding="0" cellspacing="0"> 
                                            <tr>
                                               <td style="PADDING-RIGHT: 4px; PADDING-LEFT: 4px" valign="bottom">
                                                <asp:Button ID="lnkadd" runat="server" CssClass="btn" onclick="lnkadd_Click" 
                                                    Text="Add" Width="70px"></asp:Button> 
                                                <asp:Button ID="btn_Edit" runat="server" CssClass="btn" 
                                                    onclick="btn_Edit_Click" Text="Edit" Width="70px"></asp:Button>
                                                <asp:Button ID="btnsave" runat="server" CssClass="btn" onclick="btnsave_Click" 
                                                    Text="Save" Width="70px"></asp:Button> 
                                                <asp:Button ID="ImageButton4" runat="server" CssClass="btn" 
                                                    onclick="ImageButton4_Click" Text="Delete" Width="70px">
                                                
                                                   </asp:Button> 
                                             </td>
                                            <td valign="bottom">
                                                <asp:Button ID="btnFirst" runat="server" CssClass="btn" 
                                                    onclick="btnFirst_Click" Text="|&lt;&lt;"></asp:Button> 
                                                <asp:Button ID="btnPrev" runat="server" CssClass="btn" onclick="btnPrev_Click" 
                                                    Text="&lt;&lt;"></asp:Button> 
                                                <asp:TextBox ID="txtRow" runat="server" CssClass="input_box" 
                                                    style="TEXT-ALIGN: center" Width="69px"></asp:TextBox> 
                                                <asp:Button ID="btnNext" runat="server" CssClass="btn" onclick="btnNext_Click" 
                                                    Text="&gt;&gt;"></asp:Button> 
                                                <asp:Button ID="btnLast" runat="server" CssClass="btn" onclick="btnLast_Click" 
                                                    Text="&gt;&gt;|"></asp:Button>
                                            </td>
                                            </tr>
                                            </table>
                                                </td>
                                            <td style="text-align: left">&nbsp;
                                                </td>
                                        </tr>
                                        
                                        <tr>
                                            <td>Question#:</td>
                                            <td style="text-align: left">
                                            <table cellpadding="0" cellspacing="0"  width="500px" border ="0">
                                            <tr>
                                            <td valign="bottom" >
                                                <asp:TextBox ID="txtquestionno" runat="server" AutoPostBack="True" 
                                                    CssClass="input_box" OnTextChanged="txtquestion_check" ReadOnly="True" 
                                                    Width="40px"></asp:TextBox> 
                                                <asp:Button ID="Button2" runat="server" CssClass="btn" 
                                                    onclick="btnfordesc_Click" OnClientClick="return DescPopUp();" Text="Guidance" 
                                                    Width="80px"></asp:Button>
                                            </td>
                                         
                                            <td >
                                                <asp:CheckBox ID="chkObs" runat="server" Text="Observation">
                                                </asp:CheckBox>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkhighrisk" runat="server" Text="High Risk">
                                                </asp:CheckBox>
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkncr" runat="server" Text="NCR">
                                                </asp:CheckBox>
                                            </td>
                                            <td style=" padding-left :10px;" >
                                            <asp:DropDownList ID="ddlyesno" runat="server" AutoPostBack="True" 
                                                    CssClass="input_box" OnSelectedIndexChanged="ddlyesno_SelectedIndexChanged" 
                                                    Width="50px">
                                        <asp:ListItem>Yes</asp:ListItem>
                                        <asp:ListItem>NO</asp:ListItem>
                                        <asp:ListItem>NS</asp:ListItem>
                                        <asp:ListItem>NA</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            </tr>
                                            </table>
                                            </td>
                                            <td style="text-align: left">
                                                &#160;</td>
                                        </tr>
                                        
                                        <tr>
                                            <td>Question:</td>
                                            <td style="text-align: left">
                                                    <asp:TextBox ID="txtquestion" runat="server" CssClass="input_box" Height="50px" Rows="12" TextMode="MultiLine" Width="630px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td style="text-align: left"></td>
                                        </tr>
                                        
                                        <tr>
                                            <td>Deficiency:</td>
                                            <td style="text-align: left">
                                                        <asp:TextBox ID="txtdeficiency" runat="server" CssClass="input_box" Height="50px" Rows="8" TextMode="MultiLine" Width="630px"></asp:TextBox>
                                            </td>
                                            <td style="text-align: left">
                                                                <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Modules/HRD/Images/pencil-icon.gif" OnClientClick="return OpenLargeDefPopUp();" ToolTip="Click here to open large window."></asp:ImageButton>                                                        
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td class="auto-style1">Comment:</td>
                                            <td style="text-align: left" class="auto-style1">
                                                        <asp:TextBox ID="txtcomment" runat="server" CssClass="input_box" Height="50px" Rows="12" TextMode="MultiLine" Width="630px"></asp:TextBox>
                                            </td>
                                            <td style="text-align: left" class="auto-style1">
                                                        <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Modules/HRD/Images/pencil-icon.gif" OnClientClick="window.open('..\\Inspection\\Comment_PopUp.aspx',null,'title=no,toolbars=no,scrollbars=yes,width=680,height=480,left=20,top=20,addressbar=no');" ToolTip="Click here to open large window."></asp:ImageButton>
                                            </td>
                                        </tr>
                                        <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                        <table>
                                        <tr>
                                        <td>Created By:</td><td>
                                            <asp:TextBox id="txtCreatedBy_DocumentType" tabIndex=-1 runat="server" ReadOnly="True" Width="154px" CssClass="input_box" BackColor="Gainsboro"></asp:TextBox></td>
                                        <td>Created On:</td><td>
                                            <asp:TextBox id="txtCreatedOn_DocumentType" tabIndex=-2 runat="server" ReadOnly="True" Width="72px" CssClass="input_box" BackColor="Gainsboro"></asp:TextBox></td>
                                            <td>
                                                &#160;</td>
                                        </tr>
                                        <tr>
                                        <td>Modified By:</td><td>
                                            <asp:TextBox id="txtModifiedBy_DocumentType" tabIndex=-3 runat="server" ReadOnly="True" Width="154px" CssClass="input_box" BackColor="Gainsboro"></asp:TextBox></td>
                                        <td>Modified On:</td><td>
                                            <asp:TextBox id="txtModifiedOn_DocumentType" tabIndex=-4 runat="server" ReadOnly="True" Width="72px" CssClass="input_box" BackColor="Gainsboro"></asp:TextBox></td>
                                            <td>
                                                &#160;</td>
                                        </tr>
                                        <tr>
                                        <td><asp:Label id="lblmessage" runat="server" ForeColor="#C00000" __designer:wfdid="w6"></asp:Label></td>
                                        <td >&nbsp;</td>
                                        <td>&#160;</td>
                                        <td>&#160;</td>
                                        <td>&#160;</td>
                                        </tr>
                                        </table>
                                        </td>
                                        </tr>
                                        </table>
                                        </td></tr>
                                        </table>
                                </fieldset> 
                                <TABLE style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; WIDTH: 100%; PADDING-TOP: 5px" cellSpacing=0 cellPadding=0 border=0>
                                    <TBODY>
                                        <TR>
                                            <TD style="PADDING-RIGHT: 8px; TEXT-ALIGN: left">
                                            <asp:Button runat="server" ID="btnAddQuest" OnClientClick="AddEditCheckList();return false;" Text="Add Question" CssClass="btn"  /> 
                                                <asp:FileUpload ID="flp_CheckList" runat="server" CssClass="btn" Enabled="False" Width="250px"></asp:FileUpload></TD>
                                            <TD align="right" style="TEXT-ALIGN: left">
                                                <asp:Button ID="btn_ImportCheckList" runat="server" CssClass="btn" Enabled="False" onclick="btn_ImportCheckList_Click" Text="Import CheckList" Width="127px"></asp:Button></TD>
                                            <TD style="PADDING-RIGHT: 5px; TEXT-ALIGN: right" align=right>&#160;</TD>
                                            <TD 
        style="PADDING-RIGHT: 5px; TEXT-ALIGN: right" align=right>
                                                <asp:Button ID="btnUpdateCrew" runat="server" CssClass="btn" OnClientClick="OpenUpdateCrewPopUp();" Text="Update Crew" />&#160;
                                                <asp:Button ID="btnNotify" runat="server" CssClass="btn" Enabled="False" onclick="btnNotify_Click" Text="Notify" Width="59px"></asp:Button>&#160;
                                                <asp:Button id="btn_Print" runat="server" Width="111px" CssClass="btn" Text="Print Checklist" OnClientClick="return PrintObsRPT();"></asp:Button>
                                                <asp:Label id="lbl_GridView_DocumentType" runat="server" Text="" Visible="false"></asp:Label></TD></TR></TBODY></TABLE>
                                                <cc1:AutoCompleteExtender id="AutoCompleteExtender1" runat="server" TargetControlID="txtportdone" ServicePath="../WebService.asmx" ServiceMethod="GetPortTitles" MinimumPrefixLength="1" Enabled="True" DelimiterCharacters=""></cc1:AutoCompleteExtender>
                                 </td></tr>
                        <TR>
    <TD style="PADDING-RIGHT: 10px; COLOR: red; TEXT-ALIGN: center"></TD></TR></tbody>
 </table>
 </asp:Panel> 
</contenttemplate>
</asp:UpdatePanel>
 </form>
</body>
</html>
