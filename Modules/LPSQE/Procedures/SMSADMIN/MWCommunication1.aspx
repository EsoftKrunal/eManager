<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MWCommunication1.aspx.cs" Inherits="MWCommunication1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>[ Manual & Forms ] Manual Wise Communication</title>

    <link rel="Stylesheet" href="../CSS/style.css" />
      <link rel="stylesheet" type="text/css" href="../../../HRD/Styles/StyleSheet.css" />
    <script src="../../js/Common.js" type="text/javascript" language="javascript"></script>
    <style type="text/css">
     .monthtd
     {
     	border:solid 1px gray; 
     	border-bottom :none;
     	background-color : #C2C2C2;
     	cursor:pointer;  
     }
     .monthtdselected
     {
     	border:solid 1px gray; 
     	border-bottom :none;
     	background-color : #E5A0FC;
     	cursor:pointer;
     }
     .box_
     {
     	background-color :White; 
     	color :Black; 
     	border :solid 1px gray;
     }
     .box_H
     {
     	background-color :Orange; 
     	color :Black; 
     	border :solid 1px gray;
     }
     .box_W
     {
     	background-color :LightGray;
     	color :Black; 
     	border :solid 1px gray;
     }	
     .box_L
     {
     	background-color :Green;
     	color :White; 
     	border :solid 1px gray;
     }
     .box_P
     {
     	background-color :Yellow;
     	color :Black; 
     	border :solid 1px gray;
     }
     .box_B
     {
     	background-color :Purple;
     	color :White; 
     	border :solid 1px gray;
     }
     .sel_row
     {
         background-color:#FFFFCC;
     }
     </style> 
     <script language="javascript" type="text/javascript">
         function CloseWindow() {
             //window.opener.document.getElementById("btnhdn").click();
             window.close();
         }

         function showLeaveDays() {
             document.getElementById("btnhdn").click();
         }

         function ShowCurrentMonth(Mnth, obj) {
             document.getElementById('txtMonthId').setAttribute('value', Mnth);
             document.getElementById('hdnShowMonth').click();
         } 
        function ShowProgress() {
            document.getElementById("ms1").style.display = "";
        }
        function SelectAll(trigger,ctl) {
            var ctls = document.getElementById(ctl).getElementsByTagName('input')
            for (i = 0; i <= ctls.length - 1; i++) {
                if (ctls[i].getAttribute("type") == "checkbox") {
                    ctls[i].checked = trigger.checked;
                }
            }
        }
     </script>
</head>
<body >
    <form id="form1" runat="server">
    <div style="font-family:Arial;font-size:12px;">
    <asp:ToolkitScriptManager runat="server" id="ScriptManager1"></asp:ToolkitScriptManager>
    <table style="border-collapse:collapse;" cellpadding="0" cellspacing="0" width="100%">
    <tr>
    <td style="vertical-align:top;border:solid 1px black; overflow:hidden;">
    <center>
    </center>
    <div style="padding:5px; background:#f1f1f1; overflow:auto;">
    <div style="text-align:right;float:left;">
    &nbsp;&nbsp;&nbsp;
    <asp:DropDownList ID="ddlVessel" runat="server" CssClass="stlselect" AutoPostBack="true" OnSelectedIndexChanged="ddlVessel_OnSelectedIndexChanged"></asp:DropDownList>
    &nbsp;&nbsp;&nbsp;<asp:DropDownList runat="server" ID="ddlManuals" AutoPostBack="true" onselectedindexchanged="ddlPendingForApprovalmanuals_SelectedIndexChanged" CssClass='stlselect'></asp:DropDownList>
    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
    &nbsp;&nbsp;
    <asp:RadioButton runat="server" ID="rad_All" Text="ShowAll" GroupName="aaa" Checked="true" AutoPostBack="true"  OnCheckedChanged="rad_mode_CheckChanged"/>
    <asp:RadioButton runat="server" ID="rad_Selected" Text="Ack. Not Received."  GroupName="aaa"  AutoPostBack="true" OnCheckedChanged="rad_mode_CheckChanged"/>
    <asp:Label ID="lblMessage" runat="server" Font-Bold="True" Font-Size="Large" Text="Fa"></asp:Label>
    </div>
    <div style="text-align:right;float:right;">
    <asp:Button runat="server" ID="btnSchedule" Text="New Ship" CssClass="btn" style=" padding:5px 5px 5px 5px; " onclick="btnScheduleFullManual_Click" OnClientClick="return window.confirm('Are you sure to continue ?');" />
    <asp:Button runat="server" ID="btnPost" Text="Schedule Changes" style=" padding:5px 5px 5px 5px; " OnClick="btnScheduleChanges_Click" CssClass="btn"/>
    <asp:Button runat="server" ID="btnPost1" Text="Send To Vessel" style=" padding:5px 5px 5px 5px; " OnClientClick="this.value='Please wait..';"  OnClick="btnSendVessselSelected_Click" CssClass="btn"/>
    
    </div>
    </div>
    <div runat="server" id="dvPendingForApprovalRequest" visible="false">
    <table width="100%">
        <tr>
        <td valign="top" style="border:solid 1px #4371a5; height:390px;">
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
        <td style="  height :30px; font-size :14px; font-weight: bold; text-align:center; " class="text headerband">
        &nbsp;[ Manual & Forms ] - CHANGES
        </td>
        </tr>
        </table> 

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                <colgroup>
                    <col style="width:50px;" />
                    <col style="width:40px;" />
                    <col style="width:100px;" />
                    <col />
                    <col style="width:100px;" />
                    <col style="width:100px;" />

                    <col style="width:120px;" />
                    <col style="width:80px;" />
                    <col style="width:100px;" />
                    <col style="width:80px;" />
                    <col style="width:80px;" />

                    <col style="width:70px;" />
                    <col style="width:17px;" />
                    </colgroup>
                    <tr align="left" class= "headerstylegrid" >
                        <td style="text-align:center">View</td>
                        <td style="text-align:center">Sr#</td>
                        <td>Section</td>
                        <td>Heading</td>
                        <td>Revision#</td>
                        <td>Approved On</td>
                        
                        <td>Send By</td>
                        <td>Sent On</td>                        
                        <td>Status</td>
                        <td>Ack. By</td>
                        <td>Ack. On</td>
                        <td><input type="checkbox" onchange="SelectAll(this,'dvscroll_ApprovalRequest');"  /></td>
                        <td>&nbsp;</td>
                    </tr>
               
            </table> 
            <div id="dvscroll_ApprovalRequest" onscroll="SetScrollPos(this)" style="height:390px; overflow-x:hidden;overflow-y:scroll; border-bottom:solid 1px gray;" >   
            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
            <colgroup>
                    <col style="width:50px;" />
                    <col style="width:40px;" />
                    <col style="width:100px;" />
                    <col />
                    <col style="width:100px;" />
                    <col style="width:100px;" />

                    <col style="width:120px;" />
                    <col style="width:80px;" />
                    <col style="width:100px;" />
                    <col style="width:80px;" />
                    <col style="width:80px;" />
                    <col style="width:70px;" />
                    <col style="width:17px;" />
                    </colgroup>
            <asp:Repeater ID="rptPendingForApprovalRequest" runat="server">
            <ItemTemplate>
             <tr style='font-size:11px;' class='<%# ((Eval("Scheduled").ToString()=="True")?"sel_row":"") %>'>
                    <td style="text-align:center">
                        
                        <a target="_blank" href='../ViewManualSection.aspx?ManualId=<%#Eval("ManualId")%>&SectionId=<%#Eval("SectionId")%>&AddSection=No'><img src="../../../HRD/Images/HourGlass.gif" /></a>
                        <asp:HiddenField ID="hfVesselID" runat="server" Value='<%#Eval("VesselID")%>' />
                        <asp:HiddenField ID="hfManualID" runat="server" Value='<%#Eval("ManualId")%>' />
                        <asp:HiddenField ID="hfSectionID" runat="server" Value='<%#Eval("SECTIONID")%>' />
                    </td>
                    <td style="text-align:center"><%#Eval("SNo")%></td>
                    <td><%#Eval("SECTIONID")%></td>
                    <td><div style="height:14px; overflow:hidden; text-overflow:ellipsis;"><%#Eval("HEADING")%></div></td>
                    <td><%#Eval("SVERSION")%></td>
                    <td style="text-align:center;"><%#Common.ToDateString(Eval("ApprovedOn"))%></td>

                    <td style="text-align:center;"><%#Eval("SendBy")%></td>
                    <td style="text-align:center;"><%#Common.ToDateString(Eval("SentDate"))%></td>
                    <td style="text-align:center;"><%#Eval("AckStatus")%></td>
                    <td style="text-align:center;"><%#Eval("AckBy")%></td>
                    <td style="text-align:center;"><%#Common.ToDateString(Eval("AckOn "))%></td>

                    <td style="text-align:left">
                        <asp:CheckBox runat="server" ID="chkSelect" />
                        <asp:ImageButton ID="lnkAction" OnClick="lnkAction_OnClick"  runat="server" CommandArgument='<%#Eval("ManualId")%>' ToolTip='<%#Eval("SECTIONID")%>' ImageUrl="~/Modules/HRD/Images/refresh.png" Visible='<%#Action %>' ></asp:ImageButton>
                        
                    </td>
             </tr>
            </ItemTemplate>
            </asp:Repeater>
            </table>
            <br />
        </div>
            <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;" id="dvPOPUPSendManuals" runat="server" visible="false">
            <center>
            <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color:black; z-index:100; opacity:0.6;filter:alpha(opacity=60)"></div>
            <div style="position :relative; width:1100px; height:430px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:20px;opacity:1;filter:alpha(opacity=100)">
                
                <div style=" padding:8px; font-size :14px; font-weight: bold; text-align:center; vertical-align:middle;" class="text headerband">
                &nbsp;[ Manual & Forms ] - VESSEL Communication
                </div>
                <table cellpadding="2" cellspacing="1" border="0" width="100%" style="text-align:left; font-size:13px;">
                    <colgroup>
                        <col width="70px" />
                        <col />
                        <tr>
                            <td style="font-weight:bold;">Manual :</td>
                            <td>
                                <asp:Label ID="lblManual" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-weight:bold;">Section :</td>
                            <td>
                                <asp:Label ID="lblSection" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-weight:bold;">Heading :</td>
                            <td>
                                <asp:Label ID="lblHeading" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </colgroup>
                </table>

                <div id="dvscroll_SendManuals" onscroll="SetScrollPos(this)"  style="border:solid 1px #c2c2c2; height:280px;margin-top:5px; overflow-x:hidden;overflow-y:scroll;">
                <table cellpadding="2" cellspacing="1" border="0" width="100%" rules="all" style="border-bottom:solid 1px #c2c2c2;">
                    <colgroup>
                        <col width="40px" />
                        <col width="40px" />
                        <col width="80px" />
                        <col />
                        <col width="200px" />
                        <col width="90px" />
                        <col width="80px" />
                        <col width="200px" />
                        <col width="90px" />
                        <tr class= "headerstylegrid" style="font-weight: bold; font-size: 12px;">
                            <td>Mail</td>
                            <td>Sr#</td>
                            <td>Scheduled </td>
                            <td>Vessel Name</td>
                            <td>Scheduled By</td>
                            <td>Sent On</td>
                            <td>Status</td>
                            <td>Ack. By</td>
                            <td>Ack. On</td>
                        </tr>
                        <asp:Repeater ID="rptSendManuals" runat="server">
                            <ItemTemplate>
                                <tr class='<%#Eval("RowClass")%>' style="font-size:11px;">
                                    <td>
                                        <asp:ImageButton ID="lnkAction" runat="server" CommandArgument='<%#Eval("ManualId")%>' ImageUrl="~/Modules/HRD/Images/request_icon.png" OnClick="lnkSendMail_OnClick" OnClientClick="ShowProgress();" ToolTip='<%#Eval("SECTIONID")%>' VesselId='<%#Eval("VesselID")%>' Visible='<%#(Convert.IsDBNull(Eval("AckOn")))%>' />
                                    </td>
                                    <td><%#Eval("RowNo")%>
                                        <asp:HiddenField ID="hfVesselID" runat="server" Value='<%#Eval("VesselID")%>' />
                                        <asp:HiddenField ID="hfManualID" runat="server" Value='<%#Eval("ManualID")%>' />
                                        <asp:HiddenField ID="hfSectionID" runat="server" Value='<%#Eval("SectionID")%>' />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkScheduled" runat="server" Checked='<%#(Eval("Scheduled").ToString()=="True")%>' />
                                    </td>
                                    <td style="text-align:left;"><%#Eval("VesselName")%></td>
                                    <td style="text-align:left;"><%#Eval("SendBy")%></td>
                                    <td>
                                        <asp:Label ID="lblSentDate" runat="server" Text='<%#Common.ToDateString(Eval("SentDate"))%>'></asp:Label>
                                    </td>
                                    <td><%#Eval("AckStatus")%></td>
                                    <td><%#Eval("AckBy")%></td>
                                    <td>
                                        <asp:Label ID="lblAckOn" runat="server" Text='<%#Common.ToDateString(Eval("AckOn"))%>'></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </colgroup>
                </table>
                </div>
                
                <asp:Button ID="btnSaveScheduleData" runat="server" Text=" Send to vessel Queue " OnClick="btnSaveScheduleData_SendApproval_OnClick" style="margin:3px;" CssClass="btn" />
                <asp:Button ID="btnClosePopup_SendApproval" runat="server" Text=" Close " OnClick="btnClosePopup_SendApproval_OnClick" style="margin:3px;" CausesValidation="false" CssClass="btn" /> <br />
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="true" ></asp:Label>
                <div style="border:solid 1px grey; width:250px; vertical-align:middle; display:none; padding:15px; background-color:White; position:absolute; top:150px; left:450px; background-color:#FFE0E0" id="ms1"> Please Wait.. Sending Mail....</div>
            </div>
            </center>
            </div>
        </ContentTemplate> 
        </asp:UpdatePanel>
        </td>
        </tr>
        </table>
    </div>
    
    </td>
    </tr>
    </table>
    </div>
   
    </form>
</body>

</html>
