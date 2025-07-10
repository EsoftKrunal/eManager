<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Approval.aspx.cs" Inherits="SMS_Approval" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%--<%@ Register src="ManualMenu.ascx" tagname="ManualMenu" tagprefix="uc2" %>--%>

<%@ Register src="~/Modules/LPSQE/Procedures/SMSADMIN/SMSManualMenu.ascx" tagname="SMSManualMenu" tagprefix="uc3" %>
<%@ Register src="SMSAdminSubTab.ascx" tagname="SMSAdminSubTab" tagprefix="uc4" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>EMANAGER</title>
     <link rel="Stylesheet" href="../../../HRD/Styles/StyleSheet.css" />
    <link rel="Stylesheet" href="../../../HRD/Styles/style.css" />
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
     </script>
</head>
<body >
    <form id="form1" runat="server">
    <div style="font-family:Arial;font-size:12px;">
    <asp:ToolkitScriptManager runat="server" id="ScriptManager1" EnablePartialRendering="false" ></asp:ToolkitScriptManager>
            <%--<uc2:ManualMenu ID="ManualMenu1" runat="server" />--%>
    <uc3:SMSManualMenu ID="SMSManualMenu1" runat="server" />
  <%--  <uc4:SMSAdminSubTab ID="ManualMenu2" runat="server" />
        <div class="text headerband"> Approval </div>--%>
    <table style="border-collapse:collapse;" cellpadding="0" cellspacing="0" width="100%">
    <tr>
    <td style="vertical-align:top;border:solid 1px black; overflow:hidden;">
    <center>
    <asp:RadioButtonList ID="radList" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" onselectedindexchanged="radList_SelectedIndexChanged" Font-Bold="true">
        <asp:ListItem Text="Pending for Approval" Value="1"></asp:ListItem> 
        <asp:ListItem Text="Pending for Approval Request" Value="0"></asp:ListItem> 
    </asp:RadioButtonList>
    </center>
    <div style="padding:5px; background:#e2e2e2">
    &nbsp;&nbsp;Select Manual : <asp:DropDownList runat="server" ID="ddlPendingForApprovalmanuals" AutoPostBack="true" onselectedindexchanged="ddlPendingForApprovalmanuals_SelectedIndexChanged" CssClass='stlselect'></asp:DropDownList>
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="lblMessage" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
    </div>
    <div runat="server" id="dvPendingForApproval" visible="false">
    <table width="100%">
        <tr>
        <td valign="top" style="border:solid 1px #4371a5; height:490px;">
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
        <td style="  height :30px; font-size :14px; font-weight: bold; text-align:center;" class="text headerband">
        &nbsp;[ Manual & Forms ] - CHANGES
        </td>
        </tr>
        </table> 
        <asp:UpdateProgress id="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel">
        <ProgressTemplate>
        <center>
        <div style="position : absolute; top:100px;left:0px; width:100%; z-index:100;  text-align :center; color :Blue; ">
            <center>
            <div style="border:none; height :50px; width :120px;" >
            <img src="../../../HRD/Images/progress.gif"" alt="Loading..."/> Loading ...
            </div>
            </center>
        </div>
        </center>
        </ProgressTemplate>
        </asp:UpdateProgress>   
        <asp:UpdatePanel ID="UpdatePanel" runat="server">
         <ContentTemplate>
            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                <colgroup>
                    <col style="width:4%;" />
                    <col style="width:4%;" />
                    <col style="width:20%;"/>
                    <col style="width:8%;" />
                    <col style="width:20%;"/>
                    <col style="width:8%;" />
                    <col style="width:12%;" />
                    <col style="width:12%;" />
                    <col style="width:12%;" />
                    <col style="width:2%;" />
                    <tr align="left" class= "headerstylegrid" >
                        <td style="text-align:center">View</td>
                        <td style="text-align:center">Sr#</td>
                        <td>Manual</td>
                        <td>Section</td>
                        <td>Heading</td>
                        <td>Revision#</td>
                        <td>Modified By/On</td>
                        <td>Status</td>
                        <td>Action</td>
                        <td>&nbsp;</td>
                    </tr>
                </colgroup>
            </table> 
            <div id="dvscroll_Manuals" onscroll="SetScrollPos(this)" style="height:440px; overflow-x:hidden;overflow-y:scroll; border-bottom:solid 1px gray;" >   
            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
            <colgroup>
                     <col style="width:4%;" />
                    <col style="width:4%;" />
                    <col style="width:20%;"/>
                    <col style="width:8%;" />
                    <col style="width:20%;"/>
                    <col style="width:8%;" />
                    <col style="width:12%;" />
                    <col style="width:12%;" />
                    <col style="width:12%;" />
                    <col style="width:1%;" />
            </colgroup>
            <asp:Repeater ID="RptManuals" runat="server">
            <ItemTemplate>
            <tr style='font-size:11px;'>
                    <td style="text-align:center">
                        <a target="_blank" href='ViewManualSection_PopUp.aspx?ManualId=<%#Eval("ManualId")%>&SectionId=<%#Eval("SectionId")%>'>
                            <img src="../../../HRD/Images/HourGlass.png" />
                        </a>
                    </td>
                    <td style="text-align:center"><%#Eval("SNo")%></td>
                    <td style="text-align:left;"><div style="height:14px; overflow:hidden; text-overflow:ellipsis;"><%#Eval("MANUALNAME")%></div></td>
                    <td><%#Eval("SECTIONID")%></td>
                    <td style="text-align:left;"><div style="height:14px; overflow:hidden; text-overflow:ellipsis;"><%#Eval("HEADING")%></div></td>
                    <td><%#Eval("SVERSION")%></td>
                    <td style="text-align:left;"><%#Eval("MODIFIEDBY")%> / <%#Common.ToDateString(Eval("MODIFIEDON"))%></td>
                    <td><%#Eval("Status")%></td>
                    <td style="text-align:center">
                        <asp:LinkButton Text='Approve' ForeColor="Green" ID="lnkAction" runat="server" CommandArgument='<%#Eval("ManualId")%>' ToolTip='<%#Eval("SECTIONID")%>' OnClick='DoMDAction'></asp:LinkButton>
                        <asp:LinkButton Text='Reject' ForeColor="Red" ID="lnkAction1" runat="server" CommandArgument='<%#Eval("ManualId")%>' ToolTip='<%#Eval("SECTIONID")%>' OnClick='DoMDAction'></asp:LinkButton>
                    </td>
                    <td></td>
             </tr>   
            </ItemTemplate>
            </asp:Repeater>
            </table>
            <br />
        </div>

            <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="dvPOPUPAdmin" runat="server" visible="false">
            <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:600px; height:156px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
            <div style=' padding:5px;' class="text headerband"> Please Enter Comments. </div> 
            <div style='padding:5px;'>
            <asp:TextBox runat="server" ID="txtCommentsAdmin" Width='100%' Height="70px" TextMode="MultiLine"></asp:TextBox><br />
            <asp:Button runat="server" ID="btnSubmitPopupAdmin" Text=" Submit " OnClick="btnSubmitPopupAdmin_Click" Height="25px" style="padding:2px; margin:2px;" CssClass="btn"/>            
            <asp:Button runat="server" ID="btnClosePopupAdmin" Text=" Close " OnClick="btnClosePopupAdmin_Click" Height="25px" style="padding:2px; margin:2px;"  CssClass="btn"/>            
            </div>
                <asp:Label ID="lblMsgAdminActionPopup" runat="server"  Font-Bold="True"  ForeColor="red" ></asp:Label>
            </div>
            </center>
            </div>
        </ContentTemplate> 
        </asp:UpdatePanel>
        </td>
        </tr>
        </table>
    </div>
    <div runat="server" id="dvPendingForApprovalRequest" visible="false">
    <table width="100%">
        <tr>
        <td valign="top" style="border:solid 1px #4371a5; height:420px;">
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
        <td style="  height :30px; font-size :14px; font-weight: bold; text-align:center; " class="text headerband">
        &nbsp;[ Manual & Forms ] - CHANGES
        </td>
        </tr>
        </table> 
        <asp:UpdateProgress id="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel">
        <ProgressTemplate>
        <center>
        <div style="position : absolute; top:100px;left:0px; width:100%; z-index:100;  text-align :center; color :Blue; ">
            <center>
            <div style="border:none; height :50px; width :120px;" >
            <img "../../../HRD/Images/progress.gif" alt="Loading..."/> Loading ...
            </div>
            </center>
        </div>
        </center>
        </ProgressTemplate>
        </asp:UpdateProgress>   
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                <colgroup>
                     <col style="width:4%;" />
                    <col style="width:4%;" />
                    <col style="width:20%;"/>
                    <col style="width:8%;" />
                    <col style="width:20%;"/>
                    <col style="width:8%;" />
                    <col style="width:12%;" />
                    <col style="width:12%;" />
                    <col style="width:12%;" />
                    <col style="width:2%;" />

                   
                    <tr align="left" class= "headerstylegrid" >
                        <td style="text-align:center">View</td>
                        <td style="text-align:center">Sr#</td>
                        <td>Manual</td>
                        <td>Section</td>
                        <td>Heading</td>
                        <td>Revision#</td>
                        <td>Modified By/On</td>
                        <td>Status</td>
                        <td>Action</td>
                        <td>&nbsp;</td>
                    </tr>
                </colgroup>
            </table> 
            <div id="dvscroll_PendingForApprovalRequest" onscroll="SetScrollPos(this)" style="height:400px; overflow-x:hidden;overflow-y:scroll; border-bottom:solid 1px gray;" >   
            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
            <colgroup>
                     <col style="width:4%;" />
                    <col style="width:4%;" />
                    <col style="width:20%;"/>
                    <col style="width:8%;" />
                    <col style="width:20%;"/>
                    <col style="width:8%;" />
                    <col style="width:12%;" />
                    <col style="width:12%;" />
                    <col style="width:12%;" />
                    <col style="width:1%;" />

            </colgroup>
            <asp:Repeater ID="rptPendingForApprovalRequest" runat="server">
            <ItemTemplate>
             <tr style='font-size:11px;'>
                    <td style="text-align:center">
                        <a target="_blank" href="../ViewManualSection.aspx"><img src="../../../HRD/Images/HourGlass.png" /></a>
                    </td>
                    <td style="text-align:center"><%#Eval("SNo")%></td>
                    <td style="text-align:left"><div style="height:14px; overflow:hidden; text-overflow:ellipsis;"><%#Eval("MANUALNAME")%></div></td>
                    <td><%#Eval("SECTIONID")%></td>
                    <td style="text-align:left"><div style="height:14px; overflow:hidden; text-overflow:ellipsis;"><%#Eval("HEADING")%></div></td>
                    <td><%#Eval("SVERSION")%></td>
                    <td style="text-align:left"><%#Eval("MODIFIEDBY")%> / <%#Common.ToDateString(Eval("MODIFIEDON"))%></td>
                    <td><%#Eval("Status")%></td>
                    <td style="text-align:center">
                        <asp:LinkButton Text='Submit for Approval' ForeColor="Green" ID="lnkAction" runat="server" CommandArgument='<%#Eval("ManualId")%>' ToolTip='<%#Eval("SECTIONID")%>' OnClick='DoUserAction' OnClientClick="return confirm('Are you sure to continue?');"></asp:LinkButton>
                        <asp:LinkButton Text='Cancel' ForeColor="Red" ID="lnkCancelAction" runat="server" CommandArgument='<%#Eval("ManualId")%>' ToolTip='<%#Eval("SECTIONID")%>' OnClick='DoUserAction' OnClientClick="return confirm('Are you sure to continue?');"></asp:LinkButton>
                    </td>
                    <td></td>
             </tr>   
            </ItemTemplate>
            </asp:Repeater>
            </table>
            <br />
        </div>
            <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="dvPOPUPUser" runat="server" visible="false">
            <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:600px; height:150px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">
            <div style=' padding:5px;' class="text headerband"> Please Enter Comments. </div> 
            <div style='padding:5px;'>
            <asp:TextBox runat="server" ID="txtComments1" Width='100%' Height="70px" TextMode="MultiLine"></asp:TextBox><br />
            <asp:Button runat="server" ID="btnSummit1" Text="Submit for Approval" OnClick="btnSummit1_Click" CssClass="btn"  Height="25px" style="padding:2px; margin:2px;" />
            <asp:Button runat="server" ID="btnBack1" OnClick="btnBack1_Click" Text="Cancel" CssClass="btn" Height="25px" style="padding:2px; margin:2px;" />
            </div>
            
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
