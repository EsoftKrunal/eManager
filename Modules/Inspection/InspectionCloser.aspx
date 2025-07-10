<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="InspectionCloser.aspx.cs" Inherits="Transactions_InspectionCloser"  %>
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
      if(dPart.length!=3)
      {
        alert("Enter Date in this format: dd-mmm-yyyy!");
        theField.focus();
        return false;
      }
      var check=0;
      for(i=0;i<month.length;i++)
      {
        if(dPart[1].toLowerCase()==month[i].toLowerCase())
        {
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
      if(isNaN(nDate) || dPart[2]!=nDate.getFullYear() || dPart[1]!=nDate.getMonth() || dPart[0]!=nDate.getDate())
      {
        alert("Enter Date in this format: dd-mmm-yyyy!");
        theField.select();
        theField.focus();
        return false;
      }
      else
      {
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
    }
    function DescPopUp()
    {
        var aa = document.getElementById('lst_Observation').value;
        //alert(aa);
        if(aa!="")
        {
            window.open('ResponseDescPopUp.aspx?Ques='+ aa +'','asdf','title=no,resizable=no,location=no,width=400px,height=400px,top=190px,left=550px,addressbar=no,status=yes,scrollbars=yes');
        }
    }
    function MarksPopUp()
    {
        window.open('ClosureMarks_PopUp.aspx','asdf','title=no,resizable=no,location=no,width=1000px,height=210px,top=190px,left=50px,addressbar=no,status=yes,scrollbars=yes');
    }
    function PrintRespRPT()
    {
        var InspDueId=document.getElementById('HiddenField_InspId').value;
        var MTMSptd=document.getElementById('HiddenField_MTMSupt').value;
        //alert(InspDueId);
        if(!(parseInt(InspDueId)==0 || InspDueId==""))
        {
            window.open('..\\Inspection\\Reports\\InspResponse_Report.aspx?InspId='+ InspDueId +'&MTMSp='+ MTMSptd,null,'title=no,toolbars=no,scrollbars=yes,width=850,height=650,left=20,top=20,addressbar=no');
        }
    }
        function PublishSafetyInspRPT()
    {
        var InspDueId=<%# Session["Insp_Id"].ToString() %>;
        var MTMPublish=1; 
        var MTMvslId=<%# ViewState["VesselId"].ToString() %>;
        var MTMInspName=document.getElementById('txt_InspName').value;
        var MTMDoneDt=document.getElementById('txt_DoneDate').value;
        var MTMPortDone=document.getElementById('txt_PortDone').value;
        if(!(parseInt(InspDueId)==0 || InspDueId==""))
        {
            window.open('..\\Inspection\\Reports\\SafetyInsp_Report.aspx?InspId='+ InspDueId+'&MTMPub='+MTMPublish+'&MTMVesselId='+MTMvslId+'&MTMInsp='+MTMInspName+'&MTMDnDt='+MTMDoneDt+'&MTMPrDone='+MTMPortDone,null,'title=no,toolbars=no,scrollbars=yes,width=850,height=650,left=20,top=20,addressbar=no');
        }
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
<asp:Panel id="pnl_InspResponse" runat="server">
   <div style="font-family:Arial;font-size:12px;">
        <center>
             <div style="background-color:#206020; width:95% ; height:3px;">
            </div>
            <br />
<table border="0" cellpadding="0" style=" background-color:#f9f9f9; vertical-align: top; border-right: #8fafdb 1px solid; border-top: #8fafdb 0px solid;
                                    border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: right; padding:5px 10px 5px 10px;" cellspacing="0" width="100%">
                        <tr><td style="padding-right:10px; text-align:center;"></td></tr>
                        <%--<tr>
                            <td>
                                <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                    border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center;">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td style="padding-right: 10px; text-align: right">
                                            </td>
                                            <td style="text-align: left; height:5px">
                                                </td>
                                            <td style="padding-right: 10px; text-align: right">
                                            </td>
                                            <td style="text-align: left">
                                            </td>
                                            <td style="padding-right: 10px; text-align: right">
                                            </td>
                                            <td style="text-align: left">
                                            </td>
                                            <td style="padding-right: 10px; text-align: right">
                                            </td>
                                            <td style="text-align: left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-right: 10px; text-align: right;">
                                                Insp# :</td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txt_InspNo" runat="server" CssClass="input_box" Width="158px" ReadOnly="True"></asp:TextBox></td>
                                            <td style="padding-right: 10px; text-align: right;">
                                                Vessel Name :</td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txt_VesselName" runat="server" CssClass="input_box" Width="158px" ReadOnly="True"></asp:TextBox></td>
                                            <td style="padding-right: 10px; text-align: right;">
                                                Insp Name :</td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txt_InspName" runat="server" CssClass="input_box" Width="158px" ReadOnly="True"></asp:TextBox></td>
                                            <td style="padding-right: 10px; text-align: right;">
                                                Done Dt. :</td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txt_DoneDate" runat="server" CssClass="input_box" Width="158px" ReadOnly="True"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="padding-right: 10px; text-align: right" >
                                                                                Port Done :</td>
                                            <td style="text-align: left" >
                                                <asp:TextBox ID="txt_PortDone" runat="server" CssClass="input_box" Width="158px" ReadOnly="True"></asp:TextBox></td>
                                            <td style="padding-right: 10px; text-align: right" >
                                                Master :</td>
                                            <td style="text-align: left" >
                                                <asp:TextBox ID="txt_Master" runat="server" CssClass="input_box" Width="158px" ReadOnly="True"></asp:TextBox></td>
                                            <td style="padding-right: 10px; text-align: right" >
                                                Chief Officer :</td>
                                            <td style="text-align: left" >
                                                <asp:TextBox ID="txt_ChiefOff" runat="server" CssClass="input_box" Width="158px" ReadOnly="True"></asp:TextBox></td>
                                            <td style="padding-right: 10px; text-align: right" >
                                                                    2nd Officer :</td>
                                            <td style="text-align: left" >
                                                <asp:TextBox ID="txt_SecOff" runat="server" CssClass="input_box" Width="158px" ReadOnly="True"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="padding-right: 10px; text-align: right">
                                                C/E :</td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txt_ChiefEng" runat="server" CssClass="input_box" Width="158px" ReadOnly="True"></asp:TextBox></td>
                                            <td style="padding-right: 10px; text-align: right">
                                                1st A/E :</td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txt_FirstAssEng" runat="server" CssClass="input_box" Width="158px" ReadOnly="True"></asp:TextBox></td>
                                            <td style="padding-right: 10px; text-align: right">
                                                Inspector :</td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txt_Inspector" runat="server" CssClass="input_box" Width="158px" ReadOnly="True"></asp:TextBox></td>
                                            <td style="padding-right: 10px; text-align: right">
                                                                    Supt. :</td>
                                            <td style="text-align: left">
                                                <asp:TextBox ID="txt_Sup" runat="server" CssClass="input_box" Width="158px" ReadOnly="True"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td style="padding-right: 10px; text-align: right">
                                                Status :</td>
                                            <td style="text-align: left; height:5px"><asp:TextBox ID="txt_Status" runat="server" CssClass="input_box" Width="158px" ReadOnly="True">
                                            </asp:TextBox></td>
                                            <td style="padding-right: 10px; text-align: right">
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
                                </tr>--%>
                                
                                <tr><td style="padding-top:5px">
                                <fieldset style="border-right: #8fafdb 1px solid; padding-right: 5px; border-top: #8fafdb 1px solid;
                                                        padding-left: 5px; padding-bottom: 5px; border-left: #8fafdb 1px solid; padding-top: 0px;
                                                        border-bottom: #8fafdb 1px solid; text-align: center">
                               <legend>Approval & Closure</legend>
                                    <div>
                                        <asp:HiddenField ID="hdnInspectno" runat="server" />
                                        <asp:HiddenField ID="hdnDoneDate" runat="server" />
                                        <table cellpadding="5" cellspacing="0" style="width: 100%; background-color:#eeeaea; margin-top:5px;">
                                          <tr>
                                              <td style="width:200px; font-weight:bold;"><asp:CheckBox ID="chk_FirstApp" runat="server" Text="First Approval" AutoPostBack="True" OnCheckedChanged="chk_FirstApp_CheckedChanged" /></td>
                                              <td style="width:150px; text-align:right;">Approved By :</td>
                                              <td><asp:TextBox ID="txt_FirstAppBy" runat="server" CssClass="input_box" Width="236px" OnTextChanged="txt_FirstAppBy_TextChanged" MaxLength="6" ReadOnly="True"></asp:TextBox></td>
                                              <td style="text-align:right"><asp:Button ID="btn_SaveApp1" runat="server" CssClass="btn" Text="Save" Width="59px" OnClick="btn_SaveApp1_Click"/></td>
                                          </tr>
                                           
                                        </table>
                                       
                                        <table cellpadding="5" cellspacing="0" style="width: 100%; background-color:#eeeaea; margin-top:5px;">
                                          <tr>
                                              <td style="width:200px; font-weight:bold;"><asp:CheckBox ID="chk_SecApp" runat="server" Text="Second Approval" AutoPostBack="True" OnCheckedChanged="chk_SecApp_CheckedChanged" /></td>
                                              <td style="width:150px; text-align:right;">Approved By :</td>
                                              <td><asp:TextBox ID="txt_SecAppBy" runat="server" CssClass="input_box" Width="240px" OnTextChanged="txt_SecAppBy_TextChanged" MaxLength="6" ReadOnly="True"></asp:TextBox></td>
                                              <td style="text-align:right"><asp:Button ID="btn_SaveApp2" runat="server" CssClass="btn" Text="Save" Width="59px" OnClick="btn_SaveApp2_Click"/></td>
                                          </tr>
                                          
                                        </table>

                                         <table cellpadding="5" cellspacing="0" style="width: 100%; background-color:#eeeaea; margin-top:5px;">
                                          <tr>
                                              <td style="width:200px; font-weight:bold;"><asp:CheckBox ID="chk_CloseInsp" runat="server" Text="Close the Inspection" /></td>
                                              <td style="width:150px; text-align:right;">Inspection Pass :</td>
                                              <td style="width:150px;"><asp:DropDownList ID="ddl_InspCleared" runat="server" CssClass="input_box">
                                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                                        <asp:ListItem>NA</asp:ListItem>
                                                                    </asp:DropDownList></td>
                                               <td style="width:200px; text-align:right;"><asp:Label runat="server" ID="lblInspVal" Text="Insp. Validity Dt. :"></asp:Label></td>
                                              <td>
                                                  <asp:TextBox ID="txt_InspValidity" runat="server" CssClass="input_box" Width="82px"></asp:TextBox>&nbsp;
                                                  <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                                              </td>
                                             <td style="text-align:right">
                                                 <asp:Button runat="server" CssClass="btn" ID="btnMarkEntry" Text="Update CDI Marks" OnClientClick="MarksPopUp();return false;" Visible="false"  /> 
                                                  <asp:Button ID="btn_SaveApp" runat="server" CssClass="btn" Text="Save" Width="59px" OnClick="btn_SaveApp_Click" OnClientClick="return ValidateApp();"/>
                                              </td>
                                          </tr>
                                          
                                        </table>
                                    </div>
                                     </fieldset>
                                    <%--<fieldset style="border-right: #8fafdb 1px solid; padding-right: 5px; border-top: #8fafdb 1px solid;
                                                        padding-left: 5px; padding-bottom: 5px; border-left: #8fafdb 1px solid; padding-top: 0px;
                                                        border-bottom: #8fafdb 1px solid; text-align: center">
                                                <table width="100%" cellpadding="2" cellspacing="0">
                                                    <tr>
                                                        <td style="text-align: left" ><b>Manager's Remarks :</b></td>
                                                        <td style="text-align: left" ><b>Operator's Remarks :</b></td>
                                                    </tr>
                                                    <tr>
                                                    <td style="text-align: right">
                                                        <asp:TextBox id="txt_Remarks" runat="server" CssClass="input_box" Height="140px" TextMode="MultiLine" Width="99%" MaxLength="254"></asp:TextBox>
                                                        
                                                    </td>
                                                    <td style="text-align: right">
                                                        <asp:TextBox ID="txt_OpRemarks" runat="server" CssClass="input_box" Height="140px" MaxLength="254" TextMode="MultiLine" Width="99%"></asp:TextBox>
                                                        
                                                    </td>
                                                 </tr>
                                                      <tr>
                                                        <td style="text-align: left" ><asp:Button ID="btn_SaveRemark" runat="server" CssClass="btn" Text="Save" Width="59px" OnClick="btn_SaveRemark_Click"/></td>
                                                        <td style="text-align: left" ><asp:Button ID="btn_SaveOpRem" runat="server" CssClass="btn" Text="Save" Width="59px" OnClick="btn_SaveOpRem_Click"/></td>
                                                    </tr>
                                                </table>
                                            </fieldset>--%>
                                    <div>
                                           <asp:Label ID="lblmessage" runat="server" ForeColor="#C00000"></asp:Label>
                                    </div>
                            </td>
                        </tr>
        </table>
        <div style=" padding:5px; text-align :right;widht:100%;">
        <asp:Button ID="btn_Publish" runat="server" CssClass="btn" OnClientClick="return PublishSafetyInspRPT();" Text="Publish" Visible="false" />
        </div>
</center></div>
</asp:Panel>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
        PopupButtonID="ImageButton2" PopupPosition="TopRight" TargetControlID="txt_InspValidity">
    </ajaxToolkit:CalendarExtender>
 </form>
</body>
</html>

