<%@ Page Language="C#" AutoEventWireup="true"   CodeFile="InspectionResponse.aspx.cs" Inherits="Transactions_InspectionResponse" %>

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
    function ValidateForm()
    {
//        if(trimAll(document.getElementById("ctl00_ContentPlaceHolder1_txt_Response").value)=="")
//        {
//            alert('Please Enter Response Against An Observation!');
//            document.getElementById("ctl00_ContentPlaceHolder1_txt_Response").focus();
//            return false;
//        }
          if(trimAll(document.getElementById("lst_Observation").value)=="")
          {
              alert('Please Select An Observation!');
              document.getElementById("lst_Observation").focus();
              return false;
          }
    }
    function ValidateApp()
    {
        if(document.getElementById("chk_FirstApp").checked==0)
        {
            alert('Please Check First Approval!');
            document.getElementById("chk_FirstApp").focus();
            return false;
        }
        if(trimAll(document.getElementById("txt_FirstAppBy").value)=="")
        {
            alert('Please Enter Approved By!');
            document.getElementById("txt_FirstAppBy").focus();
            return false;
        }
//        if(document.getElementById("chk_SecApp").checked==0)
//        {
//            alert('Please Check Second Approval!');
//            document.getElementById("chk_SecApp").focus();
//            return false;
//        }
//        if(trimAll(document.getElementById("txt_SecAppBy").value)=="")
//        {
//            alert('Please Enter Approved By!');
//            document.getElementById("txt_SecAppBy").focus();
//            return false;
//        }
//        if(document.getElementById('txt_InspValidity').value=='')
//        {
//            alert("Please Enter Inspection Validity Date!");
//            document.getElementById('txt_InspValidity').focus();
//            return false;
//        }
//        if(!checkDate(document.getElementById('txt_InspValidity')))
//        return false;
    }
    function DescPopUp()
    {
        var aa = document.getElementById('txt_Question').value;
        //alert(aa);
        if(aa!="")
        {
            //window.open('ResponseDescPopUp.aspx?Ques='+ aa +'','asdf','title=no,resizable=no,location=no,width=400px,height=400px,top=190px,left=550px,addressbar=no,status=yes,scrollbars=yes');
            window.open('ResponseDescPopUp.aspx?Ques=' + aa + '&Ver=<%#ViewState["VersionId"].ToString()%>&InspId=<%#Session["Insp_Id"].ToString()%>', 'asdf', 'title=no,resizable=no,location=no,width=400px,height=400px,top=190px,left=550px,addressbar=no,status=yes,scrollbars=yes');
        }
    }
    function PrintRespRPT()
    {
        var InspDueId=document.getElementById('HiddenField_InspId').value;
        var MTMSptd = document.getElementById('HiddenField_Supt').value;
        //alert(InspDueId);
        if(!(parseInt(InspDueId)==0 || InspDueId==""))
        {
            window.open('..\\Inspection\\Reports\\InspResponse_Report.aspx?InspId='+ InspDueId +'&MTMSp='+ MTMSptd,null,'title=no,toolbars=no,scrollbars=yes,width=850,height=650,left=20,top=20,addressbar=no');
        }
    }
    function PrintObsRPT()
    {
        var InspDueId=document.getElementById('HiddenField_InspId').value;
        var MTMSptd = document.getElementById('HiddenField_Supt').value;
        //alert(InspDueId);
        if(!(parseInt(InspDueId)==0 || InspDueId==""))
        {
            window.open('..\\Inspection\\Reports\\InspObservation_Report.aspx?InspId='+ InspDueId +'&MTMSp='+ MTMSptd,null,'title=no,toolbars=no,scrollbars=yes,width=850,height=650,left=20,top=20,addressbar=no');
        }
    }
    function PrintResponseQuestionRPT()
    {
        var QuesId=document.getElementById('HiddenField_QuesId').value;
        if(!(parseInt(QuesId)==0 || QuesId==""))
        {
            window.open('..\\Inspection\\Reports\\RespQuestion_Report.aspx?QuestId='+ QuesId,null,'title=no,toolbars=no,scrollbars=yes,width=850,height=650,left=20,top=20,addressbar=no');
        }
    }
    function OpenLargeResposeMgmtTextWindow(btnflag)
    {
        var ResText=document.getElementById('txt_Response').value;
        var ObsId=document.getElementById('HiddenField_ObsId').value;
        if(btnflag==1)
        {
            ResText='';
        }
        else
        {
            ResText=ResText;
        }
        window.open('..\\Inspection\\RespMgmtComment PopUp.aspx?ObsvId='+ ObsId,null,'title=no,toolbars=no,scrollbars=yes,width=660,height=470,left=20,top=20,addressbar=no');        
    }
    function OpenFollowUpItemsWindow()
    {
        var ObsvId=document.getElementById('HiddenField_ObsId').value;
        window.open('CorrectiveActions_PopUp.aspx?ObsvtnId=' + ObsvId,'ppp','title=no,resizable=no,location=no,width=800px,height=480px,top=100px,left=250px,addressbar=no,status=yes,scrollbars=no');
    }
    
    function OpenUpdateObservation()
    {
      window.open('..\\Inspection\\InspectionObservationPopUp.aspx',null,'title=no,toolbars=no,scrollbars=yes,width=1000,height=700,left=20,top=20,addressbar=no');
  }
      function OpenUpdateObservation1() {
          window.open('..\\Inspection\\InspectionObservationPopUp1.aspx', null, 'title=no,toolbars=no,scrollbars=yes,width=1000,height=700,left=20,top=20,addressbar=no');
      }
    function OpenUpdateCrewPopUp()
    {
      window.open('..\\Inspection\\InspectionObservation_PopUp.aspx',null,'title=no,toolbars=no,scrollbars=yes,width=1000,height=300,left=20,top=20,addressbar=no');  
    }
    function CreateTechReport()
    {
      window.open('..\\Inspection\\CreateReports.aspx',null,'title=no,toolbars=no,resizable=yes,scrollbars=yes,left=20,top=20,addressbar=no');  
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
<form id="form1" runat="server" style="font-family:Arial;font-size:12px;" >
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<div >
        <center>
             <div style="background-color:#206020; width:95% ; height:3px;">
            </div>
            <br />
     
<table border="0" cellpadding="0" style="  vertical-align: top; border-right: #8fafdb 1px solid; border-top: #8fafdb 0px solid;
                                    border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: right; padding:5px 10px 5px 10px;" cellspacing="0" width="100%">
                        <tr><td style="padding-right:10px; text-align:right;">
                             <asp:Button runat="server" CssClass="btn" id="btnUpdateCrewList" Text="Update Insp./Crew" OnClientClick="OpenUpdateCrewPopUp();" />
                                            <asp:Button runat="server" CssClass="btn" id="btnUpdateObservation" Text="Update Deficiency (With Checklist)" OnClientClick="OpenUpdateObservation();" />
                                            <asp:Button runat="server" CssClass="btn" id="btnUpdateObservation1" Text="Update Deficiency without Checklist" OnClientClick="OpenUpdateObservation1();" />
                            </td></tr>
                      
                        <tr runat="server" id="tr_Normal">
                            <td style="padding-top:5px">
                            <fieldset style="border-right: #8fafdb 1px solid; padding-right: 5px; border-top: #8fafdb 1px solid;
                                                        padding-left: 5px; padding-bottom: 5px; border-left: #8fafdb 1px solid; padding-top: 0px;
                                                        border-bottom: #8fafdb 1px solid; text-align: center">
                                                        <table width="100%" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td colspan="3" style="padding-right: 10px; text-align: left">
                                                                    List of Deficiency</td>
                                                            </tr>
                                                            <tr>
                                                                <td rowspan="3" style="text-align: left" valign="top">
                                                                    <asp:ListBox class="input_box" ID="lst_Observation" runat="server" Height="246px" Width="290px" AutoPostBack="True" OnSelectedIndexChanged="lst_Observation_SelectedIndexChanged"></asp:ListBox></td>
                                                                <td style="padding-right: 10px; text-align: right" valign="top" >
                                                                    Question :</td>
                                                                <td style="text-align: left" valign="top">
                                                                    <asp:TextBox ID="txt_Question" runat="server" CssClass="input_box" Width="160px" ReadOnly="True"></asp:TextBox><%-- OnTextChanged="txt_Question_TextChanged"--%>
                                                                    <asp:Button ID="btn_ForDesc" runat="server" CssClass="btn" Text="Guidance" OnClick="btn_ForDesc_Click" OnClientClick="return DescPopUp();" />
                                                                    &nbsp; &nbsp;&nbsp;<asp:ImageButton id="ImageButton2" runat="server" ImageUrl="~/Modules/HRD/Images/ReportDocument.png" OnClientClick="return PrintResponseQuestionRPT();" ToolTip="Fleet Repetition"></asp:ImageButton>
                                                                    &nbsp; &nbsp; &nbsp;
                                                                        
                                                                     
                                                                    <asp:CheckBox ID="chk_FollowupItem" runat="server" Text="FollowUp Item" AutoPostBack="True" OnCheckedChanged="chk_FollowupItem_CheckedChanged" />
                                                                    <%--<asp:LinkButton Text="Closure" runat="server" ForeColor="Red" OnClick="FollowUpPopUp_Click"></asp:LinkButton>--%>
                                                                    <asp:CheckBox ID="chkHighRisk" runat="server" Text=" High Risk " AutoPostBack="True" OnCheckedChanged="chkHighRisk_OnCheckedChanged"  />
                                                                    &nbsp; &nbsp; &nbsp;
                                                                        <div id="divPscCode" runat="server" style="display:inline;" >
                                                                            PSC Code : <asp:Label ID="lblPscCode" runat="server"></asp:Label>
                                                                        </div>
                                                                    <br />
                                                                    <asp:TextBox ID="txt_Quest" runat="server" CssClass="input_box" Height="40px" MaxLength="250"
                                                                        Rows="8" Width="620px" TextMode="MultiLine" ReadOnly="True"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding-right: 10px; text-align: right" valign="top">
                                                                    Deficiency :</td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txt_Deficiency" runat="server" CssClass="input_box" Height="40px" MaxLength="250"
                                                                        Rows="8" Width="620px" TextMode="MultiLine" ReadOnly="True"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding-right: 10px; text-align: right" valign="top">
                                                                    Response/Mgmt. Comment :</td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txt_Response" runat="server" CssClass="input_box" Height="120px" MaxLength="250"
                                                                        Rows="8" Width="620px" TextMode="MultiLine"></asp:TextBox>
                                                                    <%--<div style="position: absolute; vertical-align: top">
                                                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Modules/HRD/Images/pencil-icon.gif" ToolTip="Click here to open large window." OnClientClick="return OpenLargeResposeMgmtTextWindow();" />
                                                                    </div>--%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td rowspan="1" style="text-align: left">
                                                                    Total Deficiencies :&nbsp;
                                                                    <asp:Label ID="lbl_TotalDef" runat="server" Width="64px"></asp:Label></td>
                                                                <td style="padding-right: 10px; text-align: right">
                                                                </td>
                                                                <td style="text-align: left; padding-right: 10px;" valign="top">
                                                                    <table cellpadding="0" cellspacing="0">
                                                                        <tr>
                                                                            <td style="text-align: left;" >
                                                                                <asp:CheckBox ID="chk_RespUp" runat="server" Text="Response Uploaded" Enabled="false" AutoPostBack="True" OnCheckedChanged="chk_RespUp_CheckedChanged" />
                                                                                <asp:Button ID="btnUploadDoc" runat="server" Text="Upload/View Document" OnClick="btnUploadDoc_OnClick" CssClass="btn" Width="150px" />
                                                                            </td>
                                                                            <td style="padding-left: 132px; text-align: right">
                                                                                <asp:Button ID="btn_Save" runat="server" CssClass="btn" Text="Add" Width="59px" OnClick="btn_Save_Click" OnClientClick="return OpenLargeResposeMgmtTextWindow(1);" />&nbsp;<%--return ValidateForm();--%><asp:Button id="btn_Edit" runat="server" CssClass="btn" OnClick="btn_Edit_Click" Text="Edit" Width="59px" OnClientClick="return OpenLargeResposeMgmtTextWindow(2);" />&nbsp;
                                                                                <asp:Button id="btn_Delete" runat="server" CssClass="btn" OnClick="btn_Delete_Click" Text="Delete" Width="59px" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                            </fieldset>
                            </td>
                        </tr>
                        <tr runat="server" id="tr_Normal1">
                            <td style="padding-top:5px">
                                <table cellpadding="1" cellspacing="0" style="width: 100%" border="0">
                                                    <tr >
                                                        <td style="padding-right: 0px; text-align: left">
                                                            Observation By :</td>
                                                        <td style="text-align: left">
                                                            <asp:TextBox ID="txtCreatedBy_Response" runat="server" BackColor="Gainsboro"
                                                                CssClass="input_box" ReadOnly="True" TabIndex="-1" Width="120px"></asp:TextBox></td>
                                                        <td style="padding-right: 2px; text-align: right">
                                                            Observation On :</td>
                                                        <td style="text-align: left">
                                                            <asp:TextBox ID="txtCreatedOn_Response" runat="server" BackColor="Gainsboro"
                                                                CssClass="input_box" ReadOnly="True" TabIndex="-2" Width="72px"></asp:TextBox></td>
                                                        <td style="padding-right: 2px; text-align: right">
                                                            Response By :</td>
                                                        <td style="text-align: left">
                                                            <asp:TextBox ID="txtModifiedBy_Response" runat="server" BackColor="Gainsboro"
                                                                CssClass="input_box" ReadOnly="True" TabIndex="-3" Width="154px"></asp:TextBox></td>
                                                        <td style="padding-right: 2px; text-align: right">
                                                            Response On :</td>
                                                        <td style="text-align: left">
                                                            <asp:TextBox ID="txtModifiedOn_Response" runat="server" BackColor="Gainsboro"
                                                                CssClass="input_box" ReadOnly="True" TabIndex="-4" Width="72px"></asp:TextBox></td>
                                                        <td style="text-align: left; padding-right:5px">
                                                            <asp:Button ID="btnNotify" Visible="false" runat="server" CssClass="btn" Text="Notify" Width="59px" OnClick="btnNotify_Click" Enabled="False"  />
                                                            &nbsp;
                                                            <asp:Button id="btn_PrintOb" runat="server" CssClass="btn" Text="Print Deficiency" OnClientClick="return PrintObsRPT();" ></asp:Button>
                                                            <asp:Button ID="btn_Print" runat="server" CssClass="btn" Text="Print Response" OnClick="btn_Print_Click" OnClientClick="return PrintRespRPT();"  /><%--OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_pnl_InspResponse');"--%></td>
                                                    </tr>
                                    <tr>
                                        <td colspan="9" style="padding-left: 10px; text-align: left">
                                            <asp:Label ID="lblmessage" runat="server" ForeColor="#C00000"></asp:Label></td>
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
                                        <div style=" overflow-y:scroll; overflow-x:hidden; background-color:#c2c2c2;" >

                                        <table cellpadding="0" cellspacing="0" border="1" width="100%" rules="all" style="border-collapse:collapse;">
                                        <colgroup>
                                            <col width='30px' />
                                            <col width='50px' />
                                            <col width='50px' />
                                            <col/>
                                            <col width='140px' />
                                            <col width='180px' />
                                            <col width='50px' />
                                            <col width='17px' />
                                        </colgroup>
                                        <tr class= "headerstylegrid"  >
                                            <td style="text-align:center;" >Sr#</td>
                                            <td style="text-align:center;" >Edit</td>
                                            <td style="text-align:center;" >Delete</td>
                                            <td style="text-align:left;">&nbsp;Deficiency</td>
                                            <td style="text-align:center;" >Target Closure Dt.</td>
                                            <td style="text-align:center;" >Closed By/On</td>
                                            <td style="text-align:center;" >Action</td>
                                            <td style="text-align:center;" >&nbsp;</td>
                                        </tr>
                                        </table>
                                        </div>
                                        <div style=" height:260px; overflow-y:scroll; overflow-x:hidden;">
                                        <table cellpadding="0" cellspacing="0" border="1" width="100%" style=" border-collapse:collapse">
                                      <colgroup>
                                            <col width='30px' />
                                            <col width='50px' />
                                            <col width='50px' />
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
                                                <td style="text-align:center;" >
                                                <asp:LinkButton ID="btnEditDeficiency" runat="server" ImageUrl="~/Modules/HRD/Images/edit.jpg" OnClick="btnEditDeficiency_OnClick" Text="Edit" CommandArgument='<%#Eval("TableID")%>' CausesValidation="false" ForeColor="Blue"  Visible='<%# (Eval("Closure").ToString()=="No") %>'/>
                                                </td>
                                                <td style="text-align:center;" >
                                                    <asp:LinkButton ID="btnDeleteOB" ForeColor="Red" runat="server" Text="Delete" ImageUrl="~/Modules/HRD/Images/HourGlass.gif" OnClick="btnDeleteOB_OnClick" CommandArgument='<%#Eval("TableID")%>' Visible='<%# (Eval("Closure").ToString()=="No") %>' OnClientClick="return confirm('Are you sure to remove this?');" /> 
                                                </td>
                                                <td style="text-align:left;">&nbsp;<%#Eval("Deficiency").ToString()%></td>
                                                <td style="text-align:center;" ><%# Common.ToDateString( Eval("TCLDate").ToString())%></td>
                                                <td style="text-align:center;" >
                                                    <%#Eval("ClosedBy").ToString()%>  
                                                    <%# Common.ToDateString(Eval("ClosedOn").ToString())%> 
                                                </td>
                                                <td style="text-align:center">
                                                    <asp:LinkButton ID="btnClosurePopup" runat="server" Text="Closure" OnClick="btnClosurePopup_OnClick" CommandArgument='<%#Eval("TableID")%>' ToolTip=" Closure " Visible='<%# (Eval("Closure").ToString()=="No") %>' ForeColor="Red" /> 
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
                                <asp:Button runat="server" ID="btnOpenCheckList" Text="Open CheckList" CssClass="btn" style="float:right; margin-top:3px;margin-left:5px; width:120px" OnClick="btnOpenCheckList_OnClick"/>
                                <asp:Button runat="server" ID="btnPrint" Text="Print" CssClass="btn" style="float:right; margin-top:3px;margin-left:5px; width:80px" OnClick="btnPrint_OnClick"/>
                                <asp:Button runat="server" ID="btnDeficiencyPopup" Text="Add Deficiency" CssClass="btn" style="float:right; margin-top:3px;" OnClick="btnDeficiencyPopup_OnClick" />
                                <asp:Button runat="server" CssClass="btn" id="btnTechReport" Visible="false" style="float:right; margin-top:3px;margin-right:5px;margin-left:5px;" Text="Create Report" OnClientClick="CreateTechReport();" />
                            </td>
                        </tr>
        </table>
        <div id="DivOtherDocs"  style="position:absolute;top:0px;left:0px; height :470px; width:100%;z-index:100;" runat="server" visible="false" >
        <center>
            <div style="position:absolute;top:0px;left:0px; height :700px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:1100px; height:385px; padding :3px; text-align :center; border :solid 0px #4371a5; background : white; z-index:150;top:50px;opacity:1;filter:alpha(opacity=100)">
                <iframe width='100%' height='360px' src='' runat="server" id="frmDocs" scrolling="no" frameborder="0" >
                </iframe>
                <asp:Button ID="btnCloseDocuments" runat="server" Text="Close" CssClass="btn" OnClick="btnCloseDocuments_OnClick"  style="float:right;width:80px; margin-right:3px;margin-top:3px;" />
            </div>
            
        </center>
       
     </div>

       <%--Add Update Deficiency--%>
        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="dvAddUpdateDeficiency" runat="server" visible="false">
        <center>
        <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position :relative; width:800px; height:385px; padding :0px; text-align :center; border :solid 0px #FFD1A3 ; background : white; z-index:150;top:60px;opacity:1;filter:alpha(opacity=100)">
            <div style="border:solid 1px #c2c2c2;">
            <table cellpadding="2" cellspacing="0" border="0" width="100%">
                <colgroup>
                    <col width="130px" />
                    <col />
                    </colgroup>
                    <tr>
                        <td class="text headerband" colspan="2" style="height:23px; text-align :center;PADDING-RIGHT:10px;"><span lang="en-us">Add/Update Observation </span>
                            <asp:HiddenField ID="hfdMC" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>Deficiency :</td>
                        <td>
                            <asp:TextBox ID="txtDeficiency" runat="server" CssClass="input_box" Height="95px" TextMode="MultiLine" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Corrective Action :</td>
                        <td>
                            <asp:TextBox ID="txtCorrAction" runat="server" CssClass="input_box" Height="100px" TextMode="MultiLine" Width="98%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Target Closure Dt. :</td>
                        <td style="text-align:left">
                            <asp:TextBox ID="txtTCD" runat="server" CssClass="input_box" Width="90px"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton3" PopupPosition="TopRight" TargetControlID="txtTCD">
                            </ajaxToolkit:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>Responsibility :</td>
                        <td style="text-align:left">
                            <div style="float:left">
                                <asp:CheckBoxList ID="chklst_Respons" runat="server" RepeatDirection="Horizontal" Width="162px">
                                    <asp:ListItem>Vessel</asp:ListItem>
                                    <asp:ListItem>Office</asp:ListItem>
                                </asp:CheckBoxList>
                            </div>
                            <div style="float:right">
                                <asp:Label ID="lblMsgDeficiency" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                <asp:Button ID="btnSaveDeficiency" runat="server" CssClass="btn" OnClick="btnSaveDeficiency_OnClick" Text=" Save " />
                                <asp:Button ID="btnCloseDeficiency" runat="server" CssClass="btn" OnClick="btnCloseDeficiency_OnClick" Text=" Close " />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>Created By/On:</td>
                        <td>
                            <div style="float:left">
                                <asp:TextBox ID="txtACreatedBy" runat="server" BackColor="Gainsboro" CssClass="input_box" ReadOnly="True"></asp:TextBox>
                                -
                                <asp:TextBox ID="txtACreatedOn" runat="server" BackColor="Gainsboro" CssClass="input_box" ReadOnly="True"></asp:TextBox>
                            </div>
                            
                        </td>
                    </tr>
                <tr>
                    <td>Modified By / On:</td>
                    <td>
                        <div style="float:left;">
                                
                                <asp:TextBox ID="txtAModifiedBy" runat="server" BackColor="Gainsboro" CssClass="input_box" ReadOnly="True"></asp:TextBox>
                                -
                                <asp:TextBox ID="txtAModifiedOn" runat="server" BackColor="Gainsboro" CssClass="input_box" ReadOnly="True"></asp:TextBox>
                            </div>
                    </td>
                </tr>
               
            </table>
            </div>
        </div>
        </center>
        </div>

        <%--Add Closure Deficiency--%>
<asp:UpdatePanel ID="idww" runat="server" >
        <ContentTemplate>
        <div style="position:absolute;top:0px;left:0px; height :570px; width:100%;" id="dvClosure" runat="server" visible="false">
            <center>
            <div style="position:absolute;top:0px;left:0px; height :530px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:800px; height:500px; padding :0px; text-align :center; border :solid 1px #e2e2e2 ; border-top:none; background : white; z-index:150;top:60px;opacity:1;filter:alpha(opacity=100)">
            <div>     
                <table cellpadding="1" cellspacing="0" style="width: 100%; ">
                <tr>
                    <td colspan="4" style="height: 23px;  text-align :center;   " class="text headerband">Deficiency Details</td>
                </tr>
                <tr style="background-color:#e2e2e2">
                    <td colspan="4">
                        <asp:Label ID="Label1" runat="server" ForeColor="#C00000" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr style=" background-color:#F3F3F3">
                    <td style="text-align:right;width:120px;">&nbsp;Deficiency :</td>
                    <td colspan="3">
                        <asp:TextBox ID="txtDeficiency_C" runat="server" TextMode="MultiLine" Width="98%" Height="50px" CssClass="input_box" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr style=" background-color:#F3F3F3">
                    <td style="text-align:right;width:120px;">&nbsp;Corrective Action :</td>
                    <td colspan="3">
                        <asp:TextBox ID="txtCorrAction_C" runat="server" TextMode="MultiLine" Width="98%" Height="50px" CssClass="input_box" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr style=" background-color:#F3F3F3">
                    <td style="text-align:right;width:120px;">&nbsp;Target Closure Dt. :</td> 
                    <td>
                        <asp:TextBox ID="txtTCD_C" runat="server" Width="90px" CssClass="input_box"  ReadOnly="true"></asp:TextBox>
                        <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton4" PopupPosition="TopRight" TargetControlID="txtTCD_C"></ajaxToolkit:CalendarExtender>
                    </td>
                    <td style="text-align:right;width:120px;">&nbsp;Responsibility :</td>
                    <td><asp:CheckBoxList ID="chklst_Respons_C" runat="server" RepeatDirection="Horizontal" Width="162px" Enabled="false" >
                        <asp:ListItem>Vessel</asp:ListItem>
                        <asp:ListItem>Office</asp:ListItem>
                    </asp:CheckBoxList></td>
                </tr>
                <tr style=" background-color:#F3F3F3">
                    <td style="text-align: right;width:120px;">Created By / On:</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtACreatedBy_C" runat="server" CssClass="input_box" ReadOnly="True"></asp:TextBox>-
                            <asp:TextBox ID="txtACreatedOn_C" runat="server" CssClass="input_box" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td style="padding-right: 5px; text-align: right;width:120px;">Modified By / On:</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtAModifiedBy_C" runat="server" CssClass="input_box" ReadOnly="True"></asp:TextBox>-
                            <asp:TextBox ID="txtAModifiedOn_C" runat="server" CssClass="input_box" ReadOnly="True"></asp:TextBox>
                        </td>
                </tr>
                 <tr>
                    <td colspan="4" style="height: 23px; background-color:#e2e2e2; text-align :center; color:Black; font-size:14px; ">Closure Details</td>
                </tr>
                <tr>
                    <td style="padding-right: 5px; text-align: right;width:120px;">Closed Date :</td>
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
                    <td style="padding-right: 5px; text-align: right;width:120px;" valign="top">
                        Remarks :</td>
                    <td colspan="3" style="text-align: left">
                        <asp:TextBox ID="txt_ClosedRemarks" runat="server" CssClass="input_box" Height="100px" TextMode="MultiLine" Width="612px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="padding-right: 5px; text-align: right;width:120px;" valign="top" class="style3">
                        Closure Evidence :
                    </td>
                    <td style="text-align: left" class="style2" colspan="3" >
                        <asp:FileUpload runat="server" ID="fu_ClosureEvidence" Width="300px" CssClass="input_box" />
                        <a runat="server" target="_blank"  id="a_file_C" visible="false">
                            <img style=" border:none"  src="../HRD/Images/paperclip.gif" alt="Attachment" /> </a> 
                        &nbsp;<asp:Label runat="server" style="float:right" id="lblMs" ForeColor="Red" Font-Bold="true"></asp:Label>
                        </td>
                </tr>
               
                <tr>
                    <td style="padding-right: 5px; text-align: right;width:120px;">Closed By :
                    
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
             <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton1" PopupPosition="TopRight" TargetControlID="txt_ClosedDate"> </ajaxToolkit:CalendarExtender>
            </center>
        </div>     
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveClosure" />
        </Triggers>
     </asp:UpdatePanel>
</center>
    </div>
<asp:HiddenField ID="HiddenField_InspId" runat="server" />
<asp:HiddenField id="HiddenField_Supt" runat="server" />
<asp:HiddenField ID="HiddenField_QuesId" runat="server" />
<asp:HiddenField ID="HiddenField_ObsId" runat="server" />
 </form>
</body>
</html>