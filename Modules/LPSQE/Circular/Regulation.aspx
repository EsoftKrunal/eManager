<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Regulation.aspx.cs" Inherits="HSSQE_Regulation" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
    <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
  <script type="text/javascript" src="../../HRD/JS/jquery.min.js"></script>
  <script type="text/javascript">
      function Disable(ctl) {
          $(ctl).attr("value", "Please wait...");
          $(ctl).removeClass("btn"); $(ctl).addClass("btn_disabled");
      }

      function SelectDeSelect() {
          $.each($(".a1"), function (i, e) {
              if ($("#c1").attr("checked") == "checked") {
                  $(e).children().first().attr("checked", true);
              }
              else {
                  $(e).children().first().attr("checked", false);
              }
          });
      }
      function SetValue(ctl) {
          var val = $(ctl).attr('value');
          var stat = $(ctl).attr('class');
          $("#hfd_Key").attr("value", val);
          $("#hfd_Key1").attr("value", stat);
      }

      $(document).ready(function () {
          var val = $("#hfd_Key").attr("value");
          var cls = $("#hfd_Key1").attr("value");
          $("[name=rad1]").filter("[value=" + val + "]").filter("[class=" + cls + "]").attr('checked', true);
      });

      function Validate_Action() {
          var val = $("#hfd_Key").attr("value");
          if (parseInt(val) > 0) {
              return true;
          }
          else {
              alert("Please select a LFI");
              return false;
          }
      }
      function Validate_Delete() {
          var val = $("#hfd_Key").attr("value");
          if (parseInt(val) > 0) {
              return window.confirm('Are you sure to delete?');
          }
          else {
              alert("Please select a LFI");
              return false;
          }
      }
      function OpenProgressWindow(lfiid) {
          window.open('LFI_FC_Progress.aspx?Mode=L&Id=' + lfiid, '');
      }
</script>
<style type="text/css">
.accordionHeader {
    border: 1px solid #2F4F4F;
    color: white;
    background-color: #2E4d7B;
    font-family: Arial, Sans-Serif;
    font-size: 12px;
    font-weight: bold;
    padding: 5px;
    margin-top: 5px;
    cursor: pointer;
}
 
.accordionHeader a {
    color: #FFFFFF;
    background: none;
    text-decoration: none;
}
 
.accordionHeader a:hover {
    background: none;
    text-decoration: underline;
}
 
.accordionHeaderSelected {
    border: 1px solid #2F4F4F;
    color: white;
    background-color: #5078B3;
    font-family: Arial, Sans-Serif;
    font-size: 12px;
    font-weight: bold;
    padding: 5px;
    margin-top: 5px;
    cursor: pointer;
}
 
.accordionHeaderSelected a {
    color: #FFFFFF;
    background: none;
    text-decoration: none;
}
 
.accordionHeaderSelected a:hover {
    background: none;
    text-decoration: underline;
}
 
.accordionContent {
    background-color: #D3DEEF;
    border: 1px dashed #2F4F4F;
    border-top: none;
    padding: 5px;
    padding-top: 10px;
} 
</style>
</head>
<body>
    <form id="form1" runat="server" style="font-family:Arial;font-size:12px;">
    <ajaxToolkit:ToolkitScriptManager  ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<table width="100%" border="0" cellpadding="3" cellspacing="0" style="background-color:#CCEEF9; border:solid 1px #00ABE1; margin-bottom:3px;">
<tr>
<td width="100px" style="text-align:right">Category :</td>
<td width="100px" style="text-align:left">
    <asp:DropDownList runat="server" ID="ddlLFICat" AutoPostBack="true" OnSelectedIndexChanged="Filter_LFI"></asp:DropDownList> 
</td>
<td style="text-align:right; width:220px;">Source (Main) :</td>
<td width="150px" style="text-align:left">
    <asp:DropDownList runat="server" ID="ddlMainSource_Filter" AutoPostBack="true" OnSelectedIndexChanged="ddlMainSource_Filter_SelectedIndexChanged"></asp:DropDownList> 
</td>
<td style="text-align:right; width:220px;">Source (Sub) :</td>
<td width="150px" style="text-align:left">
    <asp:DropDownList runat="server" ID="ddlSubSource_Filter" AutoPostBack="true" OnSelectedIndexChanged="Filter_LFI"><asp:ListItem Text="< Select >" Value="0"></asp:ListItem></asp:DropDownList> 
</td>
<td width="60px" style="text-align:right">Tag :</td>
<td width="200px" style="text-align:left">
    <asp:TextBox runat="server" id="txtFilterTag" CssClass="user-input-nopadding" Width="190px" MaxLength="100" AutoPostBack="true" OnTextChanged="Filter_LFI" ></asp:TextBox> 
</td>
<td width="120px" style="text-align:right">From Date :</td>
<td width="100px">
    <asp:TextBox runat="server" id="txtFDate" CssClass="user-input-nopadding" Width="80px" MaxLength="15" AutoPostBack="true" OnTextChanged="Filter_LFI"></asp:TextBox>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFDate" Format="dd-MMM-yyyy" runat="server"></ajaxToolkit:CalendarExtender>
</td>
<td width="100px" style="text-align:right">To Date :</td>
<td width="100px">
    <asp:TextBox runat="server" id="txtTDate" CssClass="user-input-nopadding" Width="80px" MaxLength="15" AutoPostBack="true" OnTextChanged="Filter_LFI"></asp:TextBox>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtTDate" Format="dd-MMM-yyyy" runat="server"></ajaxToolkit:CalendarExtender>
</td>
<td style="text-align:right">
    <asp:Button runat="server" ID="btnAddLFI" Text="Add New Regulation" onclick="btnAddLFI_Click" style=" border:solid 1px grey;" CssClass="btn" /></td>
</tr>
</table>
<div style="height:33px; overflow-y:scroll;overflow-x:hidden;border:solid 1px #00ABE1;">
<table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color:#00ABE1; border-collapse:collapse;" bordercolor="white">
<thead>
<tr style=' height:25px;' class= "headerstylegrid">
        <td style="width:45px; text-align:center; color:White;"><b>Delete</b></td>
        <td style="width:70px;text-align:center;color:White;"><b>View</b></td>
        <td style="width:80px;text-align:center;color:White;"><b>Ref#</b></td>
        <td style="color:White;"><b>Title</b></td>
        <td style="width:80px;color:White;text-align:center;"><b>Date</b></td>
        <td style="width:200px;color:White;text-align:center;"><b>Categories</b></td>
        <td style="width:80px;color:White;text-align:center;"><b>Total Sent</b></td>
        <td style="width:80px;color:White;text-align:center;"><b>Total Ack</b></td>
        <td style="width:50px;color:White;text-align:center;"><b>Mail</b></td>
        <td style="width:50px;color:White;text-align:center;"><b>Action</b></td>
        <td style="width:30px;"><b>&nbsp;</b></td>
</tr>
</thead>
</table>
</div>
<div style="height:400px; border-bottom:none; border:solid 1px #00ABE1; overflow-x:hidden; overflow-y:scroll;" class='ScrollAutoReset' id='dv_LFI_List'>
<table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color:#F5FCFE; border-collapse:collapse;" class='newformat'>
 <tbody>
<asp:Repeater runat="server" ID="rptLFI">
<ItemTemplate>
<tr class='LFI_<%#Eval("Status")%>' onmouseover="">
      <td style="width:45px; text-align:center;">
        <asp:ImageButton runat="server" ID="btnDelete" ImageUrl="~/Modules/HRD/Images/cross.png" CommandArgument='<%#Eval("RegId")%>' Visible='<%#(Eval("Status").ToString()=="O")%>' OnClientClick="return window.confirm('Are you sure to delete?');" OnClick="btnDeleteREG_Click"/></td>
         
      </td>
      <td style="width:70px;text-align:center;">
        <asp:ImageButton runat="server" ID="btndownload" ImageUrl="~/Modules/HRD/Images/paperclipx12.png" OnClick="btnDownloadFile_Click" CommandArgument='<%#Eval("RegId")%>'/>
      </td>
      <td style="width:80px;text-align:center;">
         <asp:LinkButton ID="btnEditLFI" Text='<%#Eval("RegNumber")%>' CommandArgument='<%#Eval("RegId")%>' OnClick="btnEditLFI_Click"  runat="server"></asp:LinkButton>
      </td>
      <td><%#Eval("Title")%></td>
      <td style="width:80px;text-align:center;"><%#Common.ToDateString(Eval("RegDate"))%></td>
      <td style="width:200px;text-align:left;"><%#Eval("RegCATS")%></td>
      <td style="width:80px;text-align:center;"><%#Eval("TOTALSENT")%></td>
      <td style="width:80px;text-align:center;"><a href='Regulation_ACK.aspx?Key=<%#Eval("RegId")%>&No=<%#Eval("RegNumber")%>' target="_blank"><%#Eval("TOTALACK")%></a></td>
      
     
      <td style="width:50px;text-align:center;">
        <asp:ImageButton runat="server" ID="btnMail" ImageUrl="~/Modules/HRD/Images/mail.gif" CommandArgument='<%#Eval("RegId")%>' Visible='<%#(Eval("Status").ToString()=="P")%>' OnClick="btnSendMail_Click"/></td>
      </td>
      <td style="width:50px;text-align:center;">
        <asp:ImageButton ID="btnAction" runat="server" ImageUrl="~/Modules/HRD/Images/gtk-execute.png" CommandArgument='<%#Eval("RegId")%>' Visible='<%#Eval("ReferenceKey").ToString().Trim() == "" %>'  OnClick="btnAction_OnClick" ToolTip="Include in SMS"  ></asp:ImageButton>
      </td>
      <td style="width:30px;"><b>&nbsp;</b></td>
</tr>
</ItemTemplate>
</asp:Repeater>
</tbody>
</table>
</div>
<div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="dvEmailNotification" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:900px;  height:430px;padding :5px; text-align :center;background : white; z-index:150;top:0px; border:solid 0px black;">
            <center >
            <div style="padding:6px;  font-size:14px; " class="text headerband"><b>Send Mail</b></div>
            <div style="height:395px;">
            <ajaxToolkit:Accordion ID="Accordion2" runat="server" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent" Width="100%">
            <Panes>
            <ajaxToolkit:AccordionPane ID="pane1" runat="server" Width="100%">
                <Header>Step - 1 : Compose Mail Text</Header>
                <Content> 
                <CKEditor:CKEditorControl ID="fckContentText" Width="450px" Height="180px" BasePath="~/ckeditor/" runat="server" Text=""></CKEditor:CKEditorControl>
                </Content>
            </ajaxToolkit:AccordionPane>
            <ajaxToolkit:AccordionPane ID="pane2" runat="server">
            <Header>Step - 2 : Select Recipients </Header>
            <Content>
            <asp:UpdatePanel runat="server" ID="up1">
            <ContentTemplate>
            <div style="height:315px; margin-top:5px;">
            <table cellpadding="2" cellspacing="0" border="0" width='100%'>
            <tr>
            <td>
                <asp:HiddenField ID="hfdLFIId" runat="server" />
                <div style="height:33px; overflow-y:scroll;overflow-x:hidden;border:solid 1px #00ABE1;">
                <table width="100%" border="1" cellpadding="3" cellspacing="0" style="background-color:#00ABE1; border-collapse:collapse;" bordercolor="white">
                <thead>
                <tr style=' height:25px;' class= "headerstylegrid">
                        <td style="width:40px; text-align:center;">&nbsp;</td>
                        <td style="width:150px;text-align:left;color:White;"><b>Vessel</b></td>
                        <td style="text-align:left;color:White;"><b>eMail</b></td>
                        <td style="width:80px;text-align:left;color:White; text-align:center"><b>Sent On</b></td>
                        <td style="width:80px;text-align:left;color:White; text-align:center"><b>Ack On</b></td>
                        <td style="width:30px;">&nbsp;</td>
                </tr>
                </thead>
                </table>
                </div>
                <div style="height:250px; border:solid 1px #00ABE1; overflow-x:hidden; overflow-y:scroll;" class='ScrollAutoReset' id='dv_UserPopup'>
                    <table width="100%" border="0" cellpadding="3" cellspacing="0" style="background-color:#F5FCFE;" rules="rows">
                     <tbody>
                    <asp:Repeater runat="server" ID="rptUsers">
                    <ItemTemplate>
                    <tr>
                          <td style="width:40px; text-align:center;"><asp:CheckBox ID="chkSendMail" runat="server" ToolTip='<%#Eval("Email")%>'/>
                          <asp:HiddenField ID="hfdVesselCode" Value='<%#Eval("vesselcode")%>' runat="server" />
                          </td>                          
                          <td style="width:150px;text-align:left;"><%#Eval("VesselName")%></td>
                          <td style="text-align:left;"><%#Eval("Email")%></td>
                          <td style="width:80px;text-align:left;"><%#Common.ToDateString(Eval("SentOn"))%></td>
                          <td style="width:80px;text-align:left;"><%#Common.ToDateString(Eval("AckOn"))%></td>
                          <td style="width:30px;">&nbsp;</td>
                    </tr>
                    </ItemTemplate>
                    </asp:Repeater>
                    </tbody>
                    </table>
                </div>
            </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblMsgMail" Font-Bold="true" ForeColor="Red" style="float:left"></asp:Label>
                     <div style="text-align:right;">
                        <asp:Button ID="btnSend_Mail" Text="Send Mail" OnClick="btnSend_Mail_Click" runat="server" OnClientClick='Disable(this);' style="  border:solid 1px grey; width:100px;" CssClass="btn"/>
                    </div>
                    <div style="clear:both;"></div>
                </td>
            </tr>
            </table>
                
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
            </Content>
            </ajaxToolkit:AccordionPane>
            </Panes>
            </ajaxToolkit:Accordion>
            </div>
            <div style="text-align:right; position:relative; right:-22px; top:-2px;">
                <asp:ImageButton runat="server" ID="btnClose" Text="Close" onclick="btnClose_Click" ImageUrl="~/Modules/HRD/Images/close-button.png" CausesValidation="false" title='Close this Window !'/>   
            </div>
            </center>
         </div>
         </center>
    </div>
<div style="position:absolute;top:0px;left:0px; height :100%; width:100%; " id="dv_AddLFI" runat="server" visible="false">
    <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :Gray;z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
        <div style="position:relative;width:600px;  height:300px;padding :3px; text-align :center;background : white; z-index:150;top:80px; border:solid 0px black;">
            <center >
             <div style="padding:6px;  font-size:14px; " class="text headerband"><b>Add/ Edit Regulation</b></div>
             <div style="width:100%; text-align:left; overflow-y:hidden; overflow-x:hidden; height:240px;">
                <table cellpadding="2" cellspacing="0" width="100%">
                <tr>
                <td style="text-align:left; width:200px;">
                <b>Ref #</b>
                </td>
                <td style="text-align:left; width:120px;"><asp:TextBox runat="server" ID="txtREGNo" Width="100px" MaxLength="20"></asp:TextBox>
                <%--<asp:RequiredFieldValidator runat="server" ID="r1" ControlToValidate="txtREGNo" ErrorMessage="*" ValidationGroup="vg1" Display="Static"></asp:RequiredFieldValidator>--%>
                </td>
                <td style="text-align:left; width:200px;">
                <b>Date Issued </b>
                </td>                
                <td style="text-align:left;">
                <asp:TextBox runat="server" id="txtREGDate" CssClass="user-input-nopadding" Width="80px" MaxLength="15"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" TargetControlID="txtREGDate" Format="dd-MMM-yyyy" runat="server"></ajaxToolkit:CalendarExtender>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtREGDate" ErrorMessage="*" ValidationGroup="vg1" Display="Static"></asp:RequiredFieldValidator>
                </td>
                </tr>

                <tr>
                <td style="text-align:left; width:200px;">
                <b>Source (Main category)</b>
                </td>                
                <td style="text-align:left; width:150px;"><asp:DropDownList ID="ddlMainSource" AutoPostBack="true" OnSelectedIndexChanged="ddlMainSource_OnSelectedIndexChanged"  runat="server" ></asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="ddlMainSource" InitialValue="0" ErrorMessage="*" ValidationGroup="vg1" Display="Static"></asp:RequiredFieldValidator>
                </td>

                <td style="text-align:left; width:200px;">
                <b>Source (Sub category)</b>
                </td>
                <td style="text-align:left;"><asp:DropDownList ID="ddlSubSource" runat="server" ></asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="ddlSubSource" InitialValue="0" ErrorMessage="*" ValidationGroup="vg1" Display="Static"></asp:RequiredFieldValidator>
                </td>
                </tr>
                <tr>
                <td>
                <b>Title </b>
                </td>
                <td colspan="3"><asp:TextBox runat="server" ID="txtTitle" Width="95%" MaxLength="500"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtTitle" ErrorMessage="*" ValidationGroup="vg1" Display="Static"></asp:RequiredFieldValidator>
                </td>
                </tr>
                <tr>
                <td>
                <b>Tag </b>
                </td>
                
                <td colspan="3"><asp:TextBox runat="server" ID="txtRegTag" Width="95%" MaxLength="100"></asp:TextBox>
                <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtTitle" ErrorMessage="*" ValidationGroup="vg1" Display="Static"></asp:RequiredFieldValidator>--%>
                </td>
                </tr>
                <tr>
                <td>
                <b>Earliest Applicable Date</b>
                </td>                
                <td >
                <asp:TextBox runat="server" id="txtEADate" CssClass="user-input-nopadding" Width="80px" MaxLength="15"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" TargetControlID="txtEADate" Format="dd-MMM-yyyy" runat="server"></ajaxToolkit:CalendarExtender>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtEADate" ErrorMessage="*" ValidationGroup="vg1" Display="Static"></asp:RequiredFieldValidator>
                </td>
                <%--<td>
                <b>SMS Change Required</b>
                </td>--%>
                <td colspan="2" >
                    <asp:CheckBox ID="chkSMSChngReq" Text="SMS Change Required" TextAlign="Left" Font-Bold="true" runat="server" />
                </td>
                </tr>
                <tr>
                <td>
                <b>Select Category </b>
                </td>
                
                <td colspan="3">
                    <asp:CheckBoxList runat="server" ID="chkREGCats" RepeatDirection="Horizontal"></asp:CheckBoxList>
                </td>

                </tr>                
                <tr>
                <td>
                <b>Upload File ( pdf only )</b>
                </td>
                
                <td colspan="3">
                    <asp:FileUpload runat="server" ID="flpUpload" />&nbsp;<asp:ImageButton runat="server" ID="btnClip" ImageUrl="~/Modules/HRD/Images/paperclipx12.png"  Visible="false" onclick="btnClip_Click"/>
                              <asp:LinkButton runat="server" ID="btnClipText" Visible="false" onclick="btnClipText_Click"/>
                </td>                
                </tr>
                <tr>
                <td colspan="4">
                    <span style="color:Red">* fields are mandatory</span></td>
                </tr>
                </table>
             </div>
             </center>
             
             <asp:Button runat="server" ID="btnSave" Text="Save" onclick="btnSave_Click" ValidationGroup="vg1" style="  border:solid 1px grey; width:100px;" CssClass="btn"/>
             <asp:Button runat="server" ID="btnPublish" Text="Publish" OnClientClick="return window.confirm('Are you sure to publish?');" OnClick="btnPublishREG_Click" style=" border:solid 1px grey; width:100px;" CssClass="btn"/>
             <asp:Button runat="server" ID="btnCancel" Text="Close" OnClick="btnCancel_Click" CausesValidation="false" style=" border:solid 1px grey;width:100px;" CssClass="btn"/>
        </div>
        
             
    </center>
    </div>

    <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;z-index:100;" runat="server" id="dv_Action" visible="false" >
        <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:900px; height:415px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
                <table cellpadding="2" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td style="height: 23px;text-align :center; font-size:15px;" class="text headerband"><b>Include in SMS</b></td>
                    </tr>
                    <tr>
                        <td style="height:300px; vertical-align:top;">
                                <table cellpadding="2" cellspacing="0" border="0" width="100%" style="border-collapse:collapse;">
                                    <tr>
                                    <td style="text-align:right; width:10%; font-weight:bold;">Source : </td>
                                    <td style="text-align:left; width:13%;"><asp:DropDownList ID="ddlSource" CssClass="input_box" AutoPostBack="true" OnSelectedIndexChanged="ddlSource_SelectedIndexChanged" runat="server" Width="90%" >
                                            <asp:ListItem Text="< Select >" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Vessel" Value="Vessel"></asp:ListItem>
                                            <asp:ListItem Text="Office" Value="Office"></asp:ListItem>
                                       </asp:DropDownList>
                                    </td>
                                    <td style="text-align:right; width:10%; font-weight:bold;">VSL/ Office : </td>
                                    <td style="text-align:left;"><asp:DropDownList ID="ddlVessel_Office" runat="server" CssClass="input_box" Width="95%" >
                                       </asp:DropDownList>
                                       
                                    </td>
                                    <td style="text-align:right; width:10%; font-weight:bold;">MOC Date: </td>
                                    <td style="text-align:left; width:13%;"><asp:TextBox runat="server" ID="txtMOCDate" CssClass="input_box" MaxLength="15" Width="95px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender11" TargetControlID="txtMOCDate" runat="server" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>
                                     </td>
                                    </tr>
                                    <tr>
                                    <td colspan="3" style="text-align:right; font-weight:bold;">Impact : </td>
                                    <td colspan="3" style="text-align:left;"><asp:CheckBoxList ID="cbImpact" RepeatDirection="Horizontal" runat="server" >
                                            <asp:ListItem Text="People" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Process" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Equipment" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Safety" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="Environment" Value="5"></asp:ListItem>
                                       </asp:CheckBoxList>
                                    </td>
                                    </tr>
                                    <tr>
                                    <td colspan="6" style="text-align:left;">
                                    <b>Reason for change:<br /></b>
                                    <asp:TextBox runat="server" ID="txtReasonforChange" CssClass="input_box" TextMode="MultiLine" Width="99%" Height="75px"></asp:TextBox>
                                    </td>
                                    </tr>
                                    <tr>                    
                                        <td colspan="6" style="text-align:left;">
                                        <b> Brief Description of change :<br /></b>
                                        <asp:TextBox runat="server" ID="txtDescr" CssClass="input_box" TextMode="MultiLine"  Width="99%" Height="75px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" style="text-align:left; ">
                                        <b>Proposed TimeLine for completion of change :<br /></b>
                                        <asp:TextBox runat="server" ID="txtPropTL" CssClass="input_box" MaxLength="15" Width="80px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender12" TargetControlID="txtPropTL" runat="server" Format="dd-MMM-yyyy"></ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                              </table>
                        </td>
                    </tr>
                    </table>
                    <div style="height:20px;">
                        <asp:Label ID="lblAction_Msg" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                    <div style="text-align:center;">
                        <asp:Button ID="btnSaveAction" runat="server" Text="Save" OnClick="btnSaveAction_OnClick" CssClass="btn" style=" font-weight:bold; padding:4px;" />
                        <asp:Button ID="btnCloseAction" runat="server" Text="Close" OnClick="btnCloseAction_Click" CssClass="btn" style=" font-weight:bold; padding:4px;" />
                    </div>
            </div>
        </center>
     </div>

    </form>
</body>
</html>
