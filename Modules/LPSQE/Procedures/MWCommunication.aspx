<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MWCommunication.aspx.cs" Inherits="MWCommunication" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>[ Manual & Forms ] Manual Wise Communication</title>
    <link rel="Stylesheet" href="CSS/style.css" />
    <script src="../js/Common.js" type="text/javascript" language="javascript"></script>
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
     </script>
</head>
<body >
    <form id="form1" runat="server">
    <div >
    <asp:ToolkitScriptManager runat="server" id="ScriptManager1"></asp:ToolkitScriptManager>
    <table style="border-collapse:collapse;" cellpadding="0" cellspacing="0" width="100%">
    <tr>
    <td style="vertical-align:top;border:solid 1px black; overflow:hidden;">
    <center>
    </center>
    <div style="padding:5px; background:#e2e2e2">
    &nbsp;&nbsp;&nbsp;
    Select Vessel : <asp:DropDownList ID="ddlVessel" runat="server" CssClass="stlselect" AutoPostBack="true" OnSelectedIndexChanged="ddlVessel_OnSelectedIndexChanged"></asp:DropDownList>
    &nbsp;&nbsp;&nbsp;Select Manual : <asp:DropDownList runat="server" ID="ddlPendingForApprovalmanuals" AutoPostBack="true" onselectedindexchanged="ddlPendingForApprovalmanuals_SelectedIndexChanged" CssClass='stlselect'></asp:DropDownList>
    <asp:Button runat="server" ID="btnSchedulePending" Text="Schedule Changes" style="padding:0px; width:150px;" onclick="btnSchedulePending_Click" />
    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="lblMessage" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
    </div>
    <div runat="server" id="dvPendingForApprovalRequest" visible="false">
    <table width="100%">
        <tr>
        <td valign="top" style="border:solid 1px #4371a5; height:390px;">
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
        <td style=" background-color :#4371a5; height :30px; font-size :14px; font-weight: bold; text-align:center; color:White">
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
                    <col style="width:250px;"/>
                    <col style="width:100px;" />
                    <col />
                    <col style="width:100px;" />
                    <col style="width:105px;" />
                    <col style="width:110px;" />
                    <col style="width:80px;" />
                    <col style="width:17px;" />
                    <tr align="left" class="blueheader" style=" background:#C2C2C2; font-weight:bold;">
                        <td style="text-align:center">View</td>
                        <td style="text-align:center">Sr#</td>
                        <td>Manual</td>
                        <td>Section</td>
                        <td>Heading</td>
                        <td>Revision#</td>
                        <td>Approved On</td>
                        <td>Last Sent On</td>
                        <td>Action</td>
                        <td>&nbsp;</td>
                    </tr>
                </colgroup>
            </table> 
            <div id="dvscroll_ApprovalRequest" onscroll="SetScrollPos(this)" style="height:390px; overflow-x:hidden;overflow-y:scroll; border-bottom:solid 1px gray;" >   
            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
            <colgroup>
                    <col style="width:50px;" />
                    <col style="width:40px;" />
                    <col style="width:250px;"/>
                    <col style="width:100px;" />
                    <col />
                    <col style="width:100px;" />
                    <col style="width:105px;" />
                    <col style="width:110px;" />
                    <col style="width:80px;" />
                    <col style="width:17px;" />
            </colgroup>
            <asp:Repeater ID="rptPendingForApprovalRequest" runat="server">
            <ItemTemplate>
             <tr style='font-size:11px;' class='<%# ((Eval("IsAnyScheduled").ToString()=="0")?"":"sel_row") %>'>
                    <td style="text-align:center">
                        
                        <a target="_blank" href='ViewManualSection.aspx?ManualId=<%#Eval("ManualId")%>&SectionId=<%#Eval("SectionId")%>&AddSection=No'><img src="../Images/HourGlass.png" /></a>
                        <asp:HiddenField ID="hfManualID" runat="server" Value='<%#Eval("ManualId")%>' />
                        <asp:HiddenField ID="hfSectionID" runat="server" Value='<%#Eval("SECTIONID")%>' />
                    </td>
                    <td style="text-align:center"><%#Eval("SNo")%></td>
                    <td><div style="height:14px; overflow:hidden; text-overflow:ellipsis;"><%#Eval("MANUALNAME")%></div></td>
                    <td><%#Eval("SECTIONID")%></td>
                    <td><div style="height:14px; overflow:hidden; text-overflow:ellipsis;"><%#Eval("HEADING")%></div></td>
                    <td><%#Eval("SVERSION")%></td>
                    <td style="text-align:center;"><%#Common.ToDateString(Eval("ApprovedOn"))%></td>
                    <td style="text-align:center;"><%#Common.ToDateString(Eval("LastSent"))%></td>
                    <td style="text-align:center">
                        <%--<asp:LinkButton Text='Action' ForeColor="Green" ID="lnkAction" OnClick="lnkAction_OnClick"  runat="server" CommandArgument='<%#Eval("ManualId")%>' ToolTip='<%#Eval("SECTIONID")%>'></asp:LinkButton> --%>                       
                        <asp:ImageButton ID="lnkAction" OnClick="lnkAction_OnClick"  runat="server" CommandArgument='<%#Eval("ManualId")%>' ToolTip='<%#Eval("SECTIONID")%>' ImageUrl="~/Modules/HRD/Images/refresh.png" Visible='<%#Action %>' ></asp:ImageButton>                        
                    </td>
                    <%--<td>&nbsp;</td>--%>
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
                
                <div style="background-color :#4371a5; padding:8px; font-size :14px; font-weight: bold; text-align:center; color:White;vertical-align:middle;">
                &nbsp;[ Manual & Forms ] - VESSEL Communication
                </div>
                <table cellpadding="2" cellspacing="1" border="0" width="100%" style="text-align:left; font-size:13px;">
                    <col width="50px" />
                    <col />
                    <tr>
                        <td style="font-weight:bold;"> Manual :</td>
                        <td> 
                             <asp:Label id="lblManual" runat="server" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight:bold;"> Section :</td>
                        <td> <asp:Label id="lblSection" runat="server" ></asp:Label> </td>
                    </tr>
                    <tr>
                        <td style="font-weight:bold;"> Heading :</td>
                        <td> <asp:Label id="lblHeading" runat="server" ></asp:Label> </td>
                    </tr>
                </table>

                <div id="dvscroll_SendManuals" onscroll="SetScrollPos(this)"  style="border:solid 1px #c2c2c2; height:280px;margin-top:5px; overflow-x:hidden;overflow-y:scroll;">
                <table cellpadding="2" cellspacing="1" border="0" width="100%" rules="all" style="border-bottom:solid 1px #c2c2c2;">
                    <col width="40px" />
                    <col width="40px" />
                    <col width="80px" />
                    <col />
                    <col width="200px" />
                    <col width="90px" />
                    <col width="80px" />
                    <col width="200px" />
                    <col width="90px" />
                    <tr class="blueheader" style=" background:#C2C2C2; font-weight:bold;font-size:12px;" >
                        <td>Mail</td>
                        <td>Sr#</td>
                        <td> Scheduled </td>
                        <td>Vessel Name</td>                        
                        <td>Scheduled By</td>
                        <td>Sent On</td>                        
                        <td>Status</td>
                        <td>Ack. By</td>
                        <td>Ack. On</td>
                    </tr>
                    <asp:Repeater ID="rptSendManuals" runat="server">
                        <ItemTemplate> 
                            <tr style='font-size:11px;' class='<%#Eval("RowClass")%>'>
                                <td>
                                    <asp:ImageButton ID="lnkAction" OnClick="lnkSendMail_OnClick"  runat="server" VesselId='<%#Eval("VesselID")%>' CommandArgument='<%#Eval("ManualId")%>' ToolTip='<%#Eval("SECTIONID")%>' ImageUrl="~/Modules/HRD/Images/request_icon.png" Visible='<%#(Convert.IsDBNull(Eval("AckOn")))%>' OnClientClick="ShowProgress();"></asp:ImageButton>                        
                                </td>
                                <td> 
                                    <%#Eval("RowNo")%> 
                                    <asp:HiddenField ID="hfVesselID" runat="server" Value='<%#Eval("VesselID")%>' />
                                    <asp:HiddenField ID="hfManualID" runat="server" Value='<%#Eval("ManualID")%>' />
                                    <asp:HiddenField ID="hfSectionID" runat="server" Value='<%#Eval("SectionID")%>' />
                                </td>
                                <td>    
                                    <asp:CheckBox ID="chkScheduled" runat="server" Checked='<%#(Eval("Scheduled").ToString()=="True")%>' />
                                 </td>
                                <td style="text-align:left;"> <%#Eval("VesselName")%> </td>                                
                                <td style="text-align:left;"> <%#Eval("SendBy")%> </td>
                                <td>     <asp:Label ID="lblSentDate" runat="server" Text='<%#Common.ToDateString(Eval("SentDate"))%>'></asp:Label>   </td>
                                <td> <%#Eval("AckStatus")%> </td>
                                <td> <%#Eval("AckBy")%> </td>
                                <td>     <asp:Label ID="lblAckOn" runat="server" Text='<%#Common.ToDateString(Eval("AckOn"))%>'></asp:Label>   </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                </div>
                
                <asp:Button ID="btnSaveScheduleData" runat="server" Text=" Send to vessel Queue " OnClick="btnSaveScheduleData_SendApproval_OnClick" style="margin:3px;" />
                <asp:Button ID="btnClosePopup_SendApproval" runat="server" Text=" Close " OnClick="btnClosePopup_SendApproval_OnClick" style="margin:3px;" CausesValidation="false" /> <br />
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
