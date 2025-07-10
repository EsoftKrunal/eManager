<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditMOC.aspx.cs" Inherits="HSSQE_MOC_EditMOC" %>
<%--<%@ Register src="~/HSSQE/HSSQEMenu.ascx" tagname="HSSQEMenu" tagprefix="uc1" %>
<%@ Register src="MocMenu.ascx" tagname="MocMenu" tagprefix="uc1" %>--%>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../CSS/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <script src="../../js/Common.js" type="text/javascript"></script>
    <script type="text/javascript" src="../eReports/JS/jquery.min.js"></script>
    <title></title>
    <style type="text/css">
       .byon
       {
       float:right; color:#0066FF; font-size:13px;
       }
       
        .wizardbox 
        {
            background-color:#FFEBCC;
            
            box-sizing: border-box;
            height:30px;
            cursor:pointer;
        }
        .wizardbox .Wizard_number
        {
            width:20px; 
            font-size:14px;
            float:left;
            border-right:none;
            text-align:right;
        }
        .wizardbox .Wizard_number div
        {
            padding-top:6px;
        }
        .wizardbox .Wizard_Text
        {
            padding-top:0px;  
            vertical-align:middle;
            float:left;
            font-size:12px;
        }
        .wizardbox .Wizard_Text div
        {
            padding-top:6px;
        }
       
       
       
        .wizardboxactive
        {
            <%--background-color:#FFA319--%> 
            background-color:#00AFF0;
            box-sizing: border-box;
            height:30px;
            color:White;
            cursor:pointer;
        }
        .wizardboxactive .Wizard_number
        {
            width:20px; 
            float:left;
            border-right:none;
            text-align:right;
        }
        .wizardboxactive .Wizard_number div
        {
            padding-top:6px;
        }
        .wizardboxactive .Wizard_Text
        {
            padding-top:0px;  
            vertical-align:middle;
            float:left;
            font-size:12px;
        }
        .wizardboxactive .Wizard_Text div
        {
            padding-top:6px;
        }
        
        
        .wizardbox:hover
        {
            <%--background-color:#FFA319;--%>
            background-color:#00AFF0;
            box-sizing: border-box;
            height:30px;
            color:White;
        }
        .wizardbox:hover .Wizard_number
        {
            width:20px; 
            float:left;
            border-right:none;
            text-align:right;
        }
        .wizardbox:hover .Wizard_number div
        {
            padding-top:6px;
        }
        .wizardbox:hover .Wizard_Text
        {
            padding-top:0px;  
            vertical-align:middle;
            float:left;
            font-size:12px;
        }
        .wizardbox:hover .Wizard_Text div
        {
            padding-top:6px;
        }
        
        .panel_head
        {
            font-size:18px;
            color:#00AFF0;
            font-weight:bold;
            padding:5px;
            padding-top:0px;
        }
        
        .wizardboxdone
        {
            background-color:#00A300;
            
            box-sizing: border-box;
            height:30px;
            cursor:pointer;
        }
        .wizardboxdone .Wizard_number
        {
            width:20px; 
            font-size:14px;
            float:left;
            border-right:none;
            text-align:right;
        }
        .wizardboxdone .Wizard_number div
        {
            padding-top:6px;
        }
        .wizardboxdone .Wizard_Text
        {
            padding-top:0px;  
            vertical-align:middle;
            float:left;
            font-size:12px;
            color:White;
        }
        .wizardboxdone .Wizard_Text div
        {
            padding-top:6px;
        }
        
        
    </style>
    <script type="text/javascript">
        function SelectPanel(panelno) {
            document.getElementById("txtTanel").setAttribute("value",panelno);
            document.getElementById("btnPost").focus();
            document.getElementById("btnPost").click();
        }
        function Download() {
        
            document.getElementById("btnDownload").focus();
            document.getElementById("btnDownload").click();
            return false;
        }

    </script>
</head>

<body style="margin:0 0 0 0" style="font-family:Helvetica; font-size:13px; color:#333333; height:100%;">
    <form id="form1" runat="server">
    <div style="display:none">
        <asp:Button runat="server" ID="btnSelectRiskByAdd" OnClick="btnSelectRiskByAdd_Click" />
        <asp:HiddenField runat="server" ID="hfdRiskId" />
        <asp:HiddenField runat="server" ID="hfdOfficeId" />
    </div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <asp:UpdateProgress runat="server" ID="Fsd">
    <ProgressTemplate>
    <div style="position:absolute; top:0px; left:0px; width:100%; display:none;" >
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
        <center style="padding-top:250px;">
            <div style="background-color:White;z-index:101;">
                <img src="../../Images/loading-animation.gif" />
            </div>
        </center>
    </div>
    </ProgressTemplate>
    </asp:UpdateProgress>
    <%--<asp:UpdatePanel runat="server" id="up1">
    <ContentTemplate>--%>
    <div>
    <div>
    <div style="text-align:center">
        <div class="box3" style="padding:10px; font-size:15px;" ><b><asp:Label runat="server" ID="lblMOCNO"></asp:Label> </b></div>
    </div>
    <div style="text-align:center; padding:5px;">
        <center>
        <table style="width:1000px" cellpadding="0" cellspacing="0">
        <tr>
        <td>
            <div class="wizardbox" runat="server" id="dv_0" onclick="SelectPanel(0);">
            <div class="Wizard_number"><div>1.</div></div>
            <div class="Wizard_Text"><div>&nbsp;Request &nbsp;</div></div>
            <span style="clear:both ; display:none;"></span>
            </div>
        </td>
       <%-- <td style=" font-size:25px; color:Orange; font-weight:bold;"> <div style="background-color:#FFEBCC; height:10px; width:100%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div></td>
        <td>
            <div class="wizardbox" runat="server" id="dv_1" onclick="SelectPanel(1);">
            <div class="Wizard_number"><div>2.</div></div><div class="Wizard_Text"><div>&nbsp;Approval to Continue &nbsp; </div></div><span style="clear:both; display:none;"></span>
            </div>
        </td>--%>
        <td style=" font-size:25px; color:Orange; font-weight:bold;"> <div style="background-color:#FFEBCC; height:10px; width:100%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div></td>
        <td>
            <div class="wizardbox" runat="server" id="dv_1" onclick="SelectPanel(1);">
            <div class="Wizard_number"><div>2.</div></div><div class="Wizard_Text"><div>&nbsp;Risk Assesment and Implementaion &nbsp; </div></div><span style="clear:both; display:none;"></span>
            </div>
        </td>
        <td style=" font-size:25px; color:Orange; font-weight:bold;"> <div style="background-color:#FFEBCC; height:10px; width:100%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div></td>
        <td>
            <div class="wizardbox" runat="server" id="dv_2" onclick="SelectPanel(2);">
            <div class="Wizard_number"><div>3.</div></div><div class="Wizard_Text"><div>&nbsp;Approval of Scope / RA &nbsp; </div></div><span style="clear:both; display:none;"></span>
            </div>
        </td>
        <td style=" font-size:25px; color:Orange; font-weight:bold;"> <div style="background-color:#FFEBCC; height:10px; width:100%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div></td>
        <td >
            <div class="wizardbox" runat="server" id="dv_3" onclick="SelectPanel(3);">
            <div class="Wizard_number"><div>4.</div></div><div class="Wizard_Text"><div>&nbsp;Review &nbsp; </div></div><span style="clear:both; display:none;"></span>
            </div>
            </td>
        <td style=" font-size:25px; color:Orange; font-weight:bold;"> <div style="background-color:#FFEBCC; height:10px; width:100%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div></td>
        <td>
            <div class="wizardbox" runat="server" id="dv_4" onclick="SelectPanel(4);">
            <div class="Wizard_number"><div>5.</div></div><div class="Wizard_Text"><div>&nbsp;Endorsement &nbsp; </div></div><span style="clear:both; display:none;"></span>
            </div>
        </td>
        </tr>
        </table>
        </center>
        <div style="display:none;">
        <asp:Button runat="server" ID="btnPost" OnClick="btnPost_Click" />
        <asp:TextBox runat="server" ID="txtTanel"></asp:TextBox>
        </div>
    </div>
    <div style="border:solid 1px #c2c2c2; margin:0px 10px 10px 10px;box-shadow:5px 5px 5px #888888; padding:10px;background-color:#EBF7FF;">
        <asp:Panel runat="server" ID="pnl_0" Visible="true">
        <div class="panel_head">
            Request : 
        </div>
        <div>
          <table cellpadding="3" cellspacing="0" border="1" bordercolor="#F0F0F5" width="100%" style="border-collapse:collapse;">
                    <tr>
                        <td style="text-align:left; font-weight:bold;width:175px;">Source</td>
                        <td style="text-align:right; font-weight:bold;width:10px;">
                            &nbsp;:
                        </td>
                        <td style="text-align:left;width:250px;"><asp:Label runat="server" ID="lblSource"></asp:Label></td>
                        <td style="text-align:right; font-weight:bold;width:150px;">Location : </td>
                        <td style="text-align:left;"><asp:Label runat="server" ID="lblLocation"></asp:Label><asp:HiddenField ID="hfMOCDate" runat="server" /></td>
                        <%--<td style="text-align:right; font-weight:bold;width:150px;">MOC Date: </td>
                        <td style="text-align:left;">
                            <asp:TextBox ID="txtMOCDate" runat="server" CssClass="input_box" MaxLength="15" Width="95px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtMOCDate"></asp:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMOCDate" Display="Dynamic" ErrorMessage="*" ValidationGroup="V1"></asp:RequiredFieldValidator>
                        </td>--%>
                        
                    </tr>
          </table>
          <table cellpadding="3" cellspacing="0" border="1" bordercolor="#F0F0F5" width="100%" style="border-collapse:collapse;">
                    <tr>
                            <td style="text-align:left; font-weight:bold; width:175px;">Topic</td>
                            <td style="text-align:right; font-weight:bold; width:10px;">&nbsp;:</td>
                            <td style="text-align:left;"><asp:TextBox ID="txtTopic" runat="server" CssClass="input_box" Width="65%"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="text-align:left; font-weight:bold; width:175px;">Impact </td>
                        <td style="text-align:right; font-weight:bold; width:10px;">: </td>
                        <td style="text-align:left;">
                            <asp:CheckBoxList ID="cbImpact" RepeatDirection="Horizontal" runat="server" >
                                <asp:ListItem Text="People" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Process" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Equipment" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Safety" Value="4"></asp:ListItem>
                                <asp:ListItem Text="Environment" Value="5"></asp:ListItem>
                           </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left; font-weight:bold; width:175px;">
                            <b>Reason for change</b></td>
                        <td style="text-align:right; font-weight:bold; width:10px;">
                            :</td>
                        <td style="text-align:left;">
                            &nbsp;</td>
                    </tr>
                    </table>
          <table cellpadding="3" cellspacing="0" border="1" bordercolor="#F0F0F5" width="100%" style="border-collapse:collapse;">
                    <tr>
                        <td style="text-align:left;" ><asp:TextBox runat="server" ID="txtReasonforChange" CssClass="input_box" TextMode="MultiLine" Width="99%" Height="100px"></asp:TextBox>
                    </td>
                    </tr>
                    <tr>
                    <td>
                    <table>
                    <tr>
                        <td style="text-align:left; font-weight:bold; width:175px;">
                            <b>Brief Description of change</b></td>
                        <td style="text-align:right; font-weight:bold; width:10px;">
                            :</td>
                        <td style="text-align:left;">
                            &nbsp;</td>
                    </tr>
                    </table>
                    </td>
                    </tr>
                    <tr>                    
                        <td style="text-align:left;" ><asp:TextBox runat="server" ID="txtDescr" CssClass="input_box" TextMode="MultiLine"  Width="99%" Height="100px"></asp:TextBox></td>
                    </tr>
                    </table>
          <table cellpadding="3" cellspacing="0" border="1" bordercolor="#F0F0F5" width="100%" style="border-collapse:collapse;">
                     <tr>
                        <td style="text-align:left; font-weight:bold; width:175px; ">
                            Proposed TimeLine
                        </td>
                         <td style="text-align:left; font-weight:bold; width:10px; ">
                             :</td>
                        <td style="text-align:left; " cospan="2">
                            <asp:TextBox ID="txtPropTL" runat="server" CssClass="input_box" MaxLength="15" Width="90px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtPropTL"></asp:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPropTL" Display="Dynamic" ErrorMessage="*" ValidationGroup="V1"></asp:RequiredFieldValidator>
                            <i> (  for completion of change )</i>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left; font-weight:bold; width:175px; ">
                            Requested By / On 
            
                        </td>
                        <td style="text-align:left; font-weight:bold; width:10px; ">
                            :</td>
                        <td style="text-align:left; " cospan="2">
                            <asp:Label ID="lblRequestedBy" runat="server"></asp:Label> / <asp:Label ID="lblRequestedOn" runat="server" ></asp:Label>
                        </td>
                    </tr>
                 </table>
        </div>
        <div style="padding:10px; text-align:center; " >
            <asp:Button runat="server" ID="btnSaveNew" Text="Proceed"  ValidationGroup="V1" 
                OnClick="btnSaveNew_Click" 
                style=" padding:5px; border:none; color:White; background-color:#2E9AFE; width:150px;" />
        </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnl_1" Visible="false">
        <%--<asp:UpdatePanel runat="server" ID="fsdf">
        <ContentTemplate>--%>
        <div>
        <div class="panel_head">
             Risk Assesment : 
             <span>
             <asp:RadioButton runat="server" ID="radR" Text="Required" Font-Size="13px" Checked="true" GroupName="a" ForeColor="Purple" AutoPostBack="true" OnCheckedChanged="radR_OnCheckedChanged"/>
             <asp:RadioButton runat="server" ID="radNR" Text="Not Required" Font-Size="13px" GroupName="a" ForeColor="Gray" AutoPostBack="true" OnCheckedChanged="radR_OnCheckedChanged"/>
             </span>
        </div>
        <div runat="server" id="dvRiskNo">
                <table cellpadding="3" cellspacing="0" border="1" bordercolor="#F0F0F5" width="100%" style="border-collapse:collapse;">
                <tr runat="server" visible="false" >
                    <td style="width:175px;text-align:left; font-weight:bold;">Enter Risk Ref # </td>
                    <td style="width:10px;text-align:right; font-weight:bold;">:</td>
                    <td style="text-align:left;"> <asp:TextBox ID="txtRiskRefNum" ReadOnly="true" runat="server" CssClass="input_box" MaxLength="15" Width="150px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtRiskRefNum" Display="Dynamic" ErrorMessage="*" ValidationGroup="V3"></asp:RequiredFieldValidator>
                    <asp:ImageButton ID="btnSelectRisk" ImageUrl="~/Images/magnifier.png" Height="20px" Width="20px" OnClick="btnSelectRisk_Click" runat="server" ToolTip="Select Risk Assesment ( If already created. )" />
                    <asp:ImageButton ID="btnAddRisk" ImageUrl="~/Images/add_16.gif" Height="20px" Width="20px" OnClick="btnAddRisk_Click" runat="server" ToolTip="Create New Risk Assesment." />
                    <asp:HiddenField ID="hfRiskId" runat="server" />
                    </td>
                </tr>
                <tr runat="server" visible="false" > 
                    <td style="text-align:left; font-weight:bold; width: 175px;">
                        &nbsp;</td>
                    <td style="width:10px;text-align:right; font-weight:bold;">
                        &nbsp;</td>
                    <td style="text-align:left;">
                        <asp:Label ID="lblEventName" runat="server"></asp:Label>
                        <asp:LinkButton ID="lnkOpenRisk" runat="server" OnClick="lnkOpenRisk_Click" 
                            Text=" Open Risk Assesment " />
                    </td>
                </tr>
                <tr >
                    <td style="width:150px;text-align:left; font-weight:bold;">Attchment </td>
                    <td style="width:10px;text-align:right; font-weight:bold;">:</td>
                    <td style="text-align:left;"> <asp:FileUpload runat="server" ID="flpUpload" />&nbsp;<asp:ImageButton runat="server" ID="btnClip" ImageUrl="~/Images/paperclip.gif"  Visible="false" OnClientClick="return Download();" />
                    <asp:LinkButton runat="server" ID="btnClipText" Visible="false" OnClientClick="return Download();" /> </td>
                </tr>
                </table>
        </div>
        </div>
        <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
        
        <div class="panel_head">Summary of Changes and Implementation : </div>
               
        <div style="padding:0px; text-align:center; " >            
            <table cellpadding="3" cellspacing="0" border="1" bordercolor="#F0F0F5" width="100%" style="border-collapse:collapse;">
                <tr>
                    <td colspan="2" style="text-align:left;">
                        <div style="background-color:#00AFF0; color:#ffffff; font-weight:bold; padding:5px; text-align:left; width:200px;">People</div>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left; width:50%;"><b>
                    <table cellpadding="3" cellspacing="0" border="0"><tr><td style="text-align:left; width:175px">Communication</td><td style="text-align:left; width:10px; text-align:right;">:</td><td>&nbsp;</td></tr></table>
                    </b><asp:TextBox runat="server" ID="txtCommunication" CssClass="input_box" TextMode="MultiLine" Width="99%" Height="80px"></asp:TextBox></td>
                    <td style="text-align:left;">
                    <table cellpadding="3" cellspacing="0" border="0" style="font-weight:bold"><tr><td style="text-align:left; width:175px">Training</td><td style="text-align:left; width:10px; text-align:right;">:</td><td>&nbsp;</td></tr></table>
                    <asp:TextBox runat="server" ID="txtTraining" CssClass="input_box" TextMode="MultiLine" Width="99%" Height="80px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:left;">
                        <div style="background-color:#00AFF0; color:#ffffff; font-weight:bold; padding:5px; text-align:left; width:200px;">Process</div>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left;">
                    <table cellpadding="3" cellspacing="0" border="0" style="font-weight:bold"><tr><td style="text-align:left; width:175px">SMS Review</td><td style="text-align:left; width:10px; text-align:right;">:</td><td>&nbsp;</td></tr></table>
                    <asp:TextBox runat="server" ID="txtSMSReview" CssClass="input_box" TextMode="MultiLine" Width="99%" Height="80px"></asp:TextBox></td>
                    <td style="text-align:left;">
                    <table cellpadding="3" cellspacing="0" border="0"><tr><td style="text-align:left; width:175px" style="font-weight:bold">Drawings / Manuals</td><td style="text-align:left; width:10px; text-align:right;">:</td><td>&nbsp;</td></tr></table>
                    <asp:TextBox runat="server" ID="txtDrawing" CssClass="input_box" TextMode="MultiLine" Width="99%" Height="80px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:left; ">
                    <table cellpadding="3" cellspacing="0" border="0"  style="font-weight:bold"><tr><td style="text-align:left; width:175px">Documentation </td><td style="text-align:left; width:10px; text-align:right;">:</td><td>&nbsp;</td></tr></table>
                    <asp:TextBox runat="server" ID="txtDocumentation" CssClass="input_box" TextMode="MultiLine" Width="99%" Height="80px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:left;">
                        <div style="background-color:#00AFF0; color:#ffffff; font-weight:bold; padding:5px; text-align:left; width:200px;">Equipment</div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:left;"><asp:TextBox runat="server" ID="txtEquipment" CssClass="input_box" TextMode="MultiLine" Width="99%" Height="80px"></asp:TextBox></td>
                </tr>
                </table>

                <table cellpadding="3" cellspacing="0" border="1" bordercolor="#F0F0F5" width="100%" style="border-collapse:collapse;">
                  <tr>
                    <td style="width:175px;text-align:right; font-weight:bold;">Assesment By<%-- / On--%> </td>
                    <td style="width:10px;text-align:right; font-weight:bold;">: </td>
                    <td style="text-align:left;">
                    <asp:Label ID="lblAssessmentBy" runat="server"></asp:Label><%-- /--%> <asp:Label ID="lblAssessmentOn" runat="server" Visible="false" ></asp:Label>
                    </td>
                </tr>
                </table>
                
           
        </div>
         <div style="padding:5px; text-align:center; " >
            <asp:Button runat="server" ID="btnSaveRA" Text="Save" ValidationGroup="V3" OnClick="btnSaveRA_Click" style=" padding:5px; border:none; color:White; background-color:#2E9AFE; width:150px;" />
            <asp:Button runat="server" ID="btnNotifyRA" Text="Notify" OnClientClick="this.value='Processing....';" ValidationGroup="V3" OnClick="btnNotifyRA_Click" style=" padding:5px; border:none; color:White; background-color:#2E9AFE; width:150px;" />        
         </div>
         <div ID="dv_RiskTopics" runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%;" visible="false">
        <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:800px;  height:450px;padding :0px; text-align :center;background : white; z-index:150;top:100px; border:solid 5px black;">
        <center >
                <div class="box3" style='padding:10px 0px 10px  0px'><b>Select Risk Topic</b></div>
                <div style='padding:10px 0px 10px  0px; background-color:#99DDF3'>
                <input type="text" style='width:90%; padding:4px;' onkeyup="filter(this);" />
                </div>
                <div class="dvScrolldata" style="height: 330px;">
                <table cellspacing="0" rules="none" border="1" cellpadding="0" style="width:100%;border-collapse:collapse;">
                        <colgroup>
                        <col style="width:50px;" />
                        <col />
                    </colgroup>
                    <asp:Repeater ID="rpt_Events" runat="server">
                        <ItemTemplate>
                            <tr class='listitem'>
                                <td style="text-align:center">
                                    <asp:ImageButton ID="btnSelect" runat="server" CommandArgument='<%#Eval("EventId")%>' OnClick="btnSelectEvent_Click" ImageUrl="~/Images/check.gif" ToolTip="Select" />
                                </td>
                                <td align="left" class='listkey'><%#Eval("EventName")%></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                </div>
          </center>
          <div style="padding:3px">
          <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancelNewRisk_Click" CausesValidation="false" style=" padding:5px; border:none; color:White; background-color:red; width:150px;"/>
          </div>
          </div>
        </center>
       
       
      </div>

        </asp:Panel>
        <asp:Panel runat="server" ID="pnl_2" Visible="false">
        <div class="panel_head">
            Approval of Scope / RA : 
        </div>
            <table cellpadding="3" cellspacing="0" border="1" bordercolor="#F0F0F5" width="100%" style="border-collapse:collapse;">
                
                <tr>
                    <td style="text-align:left; font-weight:bold;width:175px;">Change Type</td>
                    <td style="text-align:right; font-weight:bold;width:10px;">:</td>
                    <td style="text-align:left;"> 
                        <asp:RadioButton runat="server" id="radChangeT" GroupName="chagneneccearsy" Text="Temporary" />
                        <asp:RadioButton runat="server" id="radChangeP" GroupName="chagneneccearsy" Text="Permanant" Checked="true" /> </td>
                </tr>
                <tr>
                    <td style="text-align:left; font-weight:bold;width:175px;">Approval of Scope / RA </td>
                    <td style="text-align:right; font-weight:bold;width:10px;">:</td>
                    <td style="text-align:left;">
                        <asp:RadioButton runat="server" id="radYes" GroupName="appscope" Text="Yes" Checked="true" />
                        <asp:RadioButton runat="server" id="radNo" GroupName="appscope" Text="No" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left; font-weight:bold;width:175px;">Remarks </td>
                    <td style="text-align:right; font-weight:bold;width:10px;">:</td>
                    <td style="text-align:left;"></td>
                </tr>
                <tr>
                   <td style="text-align:left;" colspan="3" ><asp:TextBox runat="server" ID="txtApproverComments" CssClass="input_box" TextMode="MultiLine" Width="99%" Height="100px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align:left; font-weight:bold; width:175px; ">Review Date </td>
                    <td style="text-align:right; font-weight:bold;width:10px;">:</td>
                    <td style="text-align:left;"> <asp:TextBox ID="txtReviewDate" runat="server" CssClass="input_box" MaxLength="15" Width="90px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtReviewDate"></asp:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtReviewDate" Display="Dynamic" ErrorMessage="*" ValidationGroup="V4"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:left; font-weight:bold; width:175px; ">Approved By / On </td>
                    <td style="text-align:right; font-weight:bold;width:10px;">:</td>
                    <td style="text-align:left;"><asp:Label ID="lblApprovedBy" runat="server"></asp:Label> / <asp:Label ID="lblApprovedOn" runat="server" ></asp:Label>
                    </td>
                </tr>
                </table>
                <div style="padding:10px; text-align:center; " >
                    <asp:Button runat="server" ID="btnApprove" Text="Save" ValidationGroup="V4" OnClick="btnApprove_Click" style=" padding:5px; border:none; color:White; background-color:#2E9AFE; width:150px;" />
                    <asp:Button runat="server" ID="btnNotifyApprove" OnClientClick="this.value='Processing....';" Text="Notify" ValidationGroup="V4" OnClick="btnNotifyApprove_Click" style=" padding:5px; border:none; color:White; background-color:#2E9AFE; width:150px;" />        
                </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnl_3" Visible="false">
        <div class="panel_head">
           Review : <asp:Label ID="lblReviewDate" runat="server"></asp:Label>
        </div>
             <table cellpadding="3" cellspacing="0" border="1" bordercolor="#F0F0F5" width="100%" style="border-collapse:collapse;">
                <tr>
                    <td style="width:175px;text-align:left; font-weight:bold;">Was Change Effective ? </td>
                    <td style="text-align:right; font-weight:bold;width:10px;">:</td>
                    <td style="text-align:left;"> <asp:RadioButton runat="server" id="rdochagneneccearsyYes" GroupName="chagneneccearsy" Text="Yes" Checked="true" /> <asp:RadioButton runat="server" id="rdochagneneccearsyNo" GroupName="chagneneccearsy" Text="No" /> </td>
                </tr> 
                 <tr>
                    <td style="width:175px;text-align:left; font-weight:bold;">Comments </td>
                    <td style="text-align:right; font-weight:bold;width:10px;">:</td>
                    <td style="text-align:left;"></td>
                </tr>                
                </table>
                <table cellpadding="3" cellspacing="0" border="1" bordercolor="#F0F0F5" width="100%" style="border-collapse:collapse;">
                <tr>
                    <td style="text-align:left;" colspan="2" ><asp:TextBox runat="server" ID="txtReviewComments" CssClass="input_box" TextMode="MultiLine" Width="99%" Height="100px"></asp:TextBox></td>
                </tr>
                </table>
                 <table cellpadding="3" cellspacing="0" border="1" bordercolor="#F0F0F5" width="100%" style="border-collapse:collapse;">
                 <tr>
                    <td style="width:175px;text-align:left; font-weight:bold;">Reviewed By<%-- / On --%></td>
                    <td style="text-align:right; font-weight:bold;width:10px;">:</td>
                    <td style="text-align:left;"><asp:Label ID="lblReviewedBy" runat="server"></asp:Label> <%--/--%> <asp:Label ID="lblReviewedOn" runat="server" Visible="false" ></asp:Label></td>
                </tr>   
                </table>
            
                <div style="padding:10px; text-align:center; " >
            <asp:Button runat="server" ID="btnSaveReview" Text="Save" ValidationGroup="V5" OnClick="btnSaveReview_Click" style=" padding:5px; border:none; color:White; background-color:#2E9AFE; width:150px;" />
            <asp:Button runat="server" ID="btnNotifyReview" OnClientClick="this.value='Processing....';" Text="Notify" ValidationGroup="V5" OnClick="btnNotifyReview_Click" style=" padding:5px; border:none; color:White; background-color:#2E9AFE; width:150px;" />        
        </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnl_4" Visible="false">        
        <div class="panel_head">
            Endorsement : 
        </div>
             <table cellpadding="3" cellspacing="0" border="1" bordercolor="#F0F0F5" width="100%" style="border-collapse:collapse;">                
                <%--<tr>
                    <td style="text-align:right; font-weight:bold;width:200px;">Endorsed By : </td>
                    <td style="text-align:left;"> <asp:Label ID="lblEndorsedBy" runat="server" ></asp:Label></td>
                </tr>
                <tr>
                    <td style="text-align:right; font-weight:bold;width:200px;">Endorsed On : </td>
                    <td style="text-align:left;"> <asp:Label ID="lblEndorsedOn" runat="server" ></asp:Label></td>
                </tr>--%>
                <tr>
                    <td style="text-align:left;" colspan="2" >
                    <table cellpadding="3" cellspacing="0" border="0"  style="font-weight:bold"><tr><td style="text-align:left; width:185px">Suggestion for Improvement </td><td style="text-align:left; width:10px; text-align:right;">:</td><td>&nbsp;</td></tr></table>
                    <asp:TextBox runat="server" ID="txtSuggestionforimprovement" CssClass="input_box" TextMode="MultiLine" Width="99%" Height="100px"></asp:TextBox>
                </td>
                </table>
                 <table cellpadding="3" cellspacing="0" border="1" bordercolor="#F0F0F5" width="100%" style="border-collapse:collapse;">
                 <tr>
                    <td style="width:185px;text-align:left; font-weight:bold;">Endorsed By / On : </td>
                    <td style="width:10px;text-align:right; font-weight:bold;"> :</td>
                    <td style="text-align:left;"> <asp:Label ID="lblEndorsedBy" runat="server"></asp:Label> / <asp:Label ID="lblEndorsedOn" runat="server" ></asp:Label></td>
                </tr>   
                </table>
            
                <div style="padding:10px; text-align:center; " >
                    <asp:Button runat="server" ID="btnSaveEndorsement" Text="Save" ValidationGroup="V6" OnClick="btnSaveEndorsement_Click" style=" padding:5px; border:none; color:White; background-color:#2E9AFE; width:150px;" />
                    
                </div>
        </asp:Panel>

    </div>
    </div>
    <div style="padding:3px; text-align:center; " >
         <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
    </div>
    </div>

    <div ID="dv_RiskSelection" runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%;" visible="false">
        <center>
        <div style="position:fixed;top:0px;left:0px; min-height :100%; width:100%; background-color :black;z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
        <div style="position:relative;width:950px;  height:450px;padding :0px; text-align :center;background : white; z-index:150;top:30px; border:solid 5px black;">
        <center >
                <div class="box3"><b>Select Risk</b></div>
                <div style="height: 380px; padding:3px; ">
                   <div style="border:none;">
                <div class="box1">  
                     <table cellspacing="0" rules="none" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
                     <tr>
                     <td style="text-align:right; vertical-align:middle;">Vessel :&nbsp;</td>
                     <td style="text-align:left; vertical-align:middle;"><asp:DropDownList ID="ddlVessel" runat="server" Width="150px" /></td>
                     <td style="text-align:right; vertical-align:middle;">Office :&nbsp;</td>
                     <td style="text-align:left; vertical-align:middle;"><asp:DropDownList ID="ddlOffice" runat="server" Width="150px" /></td>
                     <td style="text-align:right; vertical-align:middle;">Ref # :&nbsp;</td>
                     <td style="text-align:left; vertical-align:middle;">
                        <asp:TextBox runat="server" ID="txtRefNo" CssClass="input_box" Width="170px"></asp:TextBox>
                     </td>
                     <td style="text-align:right; vertical-align:middle;">Status :&nbsp;</td>
                     <td style="text-align:left; vertical-align:middle;"><asp:DropDownList ID="ddlStatus" runat="server" Width="80px">
                     <asp:ListItem Text="All" Value=""></asp:ListItem>
                     <asp:ListItem Text="Open" Value="O" Selected="True"></asp:ListItem>
                     <asp:ListItem Text="Closed" Value="C"></asp:ListItem>
                     </asp:DropDownList>
                     </td>
                     <td>
                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
                     </td>
                     </tr>
                     </table>
                </div>
                <div class="dvScrollheader">  
                <table cellspacing="0" rules="all" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
                        <colgroup>
                            <col style="width:50px;" />
                            <col style="width:150px;" />
                            <col style="width:180px;" />
                            <col />
                            <col style="width:20px;" />
                        </colgroup>
                        <tr>
                            <td style="text-align:center; vertical-align:middle;">Select</td>
                            <td style="text-align:left; vertical-align:middle;">Vessel/ Office Name</td>
                            <td style="text-align:left;">Ref #</td>
                            <td style="text-align:left; vertical-align:middle;">Topic</td>                                                       
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </div>
                <div class="dvScrolldata" style="height: 300px;">
                    <table cellspacing="0" rules="none" border="1" cellpadding="0" style="width:100%;border-collapse:collapse;">
                         <colgroup>
                            <col style="width:50px;" />
                            <col style="width:150px;" />
                            <col style="width:180px;" />
                            <col />                     
                            <col style="width:20px;" />
                        </colgroup>
                        <asp:Repeater ID="rptRisks" runat="server">
                            <ItemTemplate>
                                <tr >
                                    <td style="text-align:center">
                                        <asp:ImageButton ID="btnSelect" runat="server" VesselCode='<%#Eval("VesselCode")%>' OfficeId='<%#Eval("OfficeId")%>' RefNo='<%#Eval("RefNo")%>' CommandArgument='<%#Eval("RiskId")%>' OnClick="btnSelect_Click" ImageUrl="~/Images/check.gif" ToolTip="Select" />
                                    </td>
                                    <td align="left"><%#Eval("VesselName")%></td>
                                    <td align="left"><%#Eval("RefNo")%></td>
                                    <td align="left"><%#Eval("EventName")%></td>
                                    <td>&nbsp;</td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>

             </div>
                  
                </div>
          </center>
          
          <div style="padding:3px; text-align:center; ">          
          <asp:Button runat="server" ID="btnClose" Text="Close" OnClick="btnClose_Click" CausesValidation="false" style=" padding:3px; border:none; color:White; background-color:Red; width:80px;" />
          </div>
          </div>
        </center>
        </div>


    <%--</ContentTemplate>
    <Triggers>
         <asp:PostBackTrigger ControlID="btnSaveRA" />
        </Triggers>
    </asp:UpdatePanel>--%>

    <asp:Button ID="btnDownload" OnClick="btnDownload_Click" Text="Download" style='display:none;' runat="server" />
     <script type="text/javascript">
         function filter(ctl) {
             var par = $(ctl).val().toLowerCase();
             $(".listitem").each(function (i, o) {
                 var txt = $(o).find(".listkey").first().html().toLowerCase();
                 if (parseInt(txt.search(par)) >= 0) {
                     $(o).css('display', '');
                 }
                 else {
                     $(o).css('display', 'none');
                 }
             });
         }
         function openRiskWindow(EventId) {
             window.open('../RiskManagement/AddRisk.aspx?EventId=' + EventId, '_blank', '', true);
         }
     </script>
    </form>
</body>
    

</html>
