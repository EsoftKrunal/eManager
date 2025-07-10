<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWCommunication.aspx.cs" Inherits="SWCommunication" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>[ Manual & Forms ] Ship Wise Communication</title>
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
    &nbsp;&nbsp;Select Vessel: <asp:DropDownList runat="server" ID="ddlVessel" AutoPostBack="true" onselectedindexchanged="ddlVessel_SelectedIndexChanged" CssClass='stlselect'></asp:DropDownList>
    &nbsp;&nbsp;&nbsp;&nbsp; <asp:Button runat="server" ID="btnSchedule" 
            Text="Schedule" style="padding:2px" onclick="btnSchedule_Click" />
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
                    <col style="width:50px;" />
                    <col />
                    <col style="width:80px;" />
                    <col style="width:17px;" />
                    <tr align="left" class="blueheader" style=" background:#C2C2C2; font-weight:bold;">
                        <td style="text-align:center">Select</td>
                        <td style="text-align:center">Sr#</td>
                        <td>Manual</td>
                        <td>Version#</td>
                        <td>&nbsp;</td>
                    </tr>
                </colgroup>
            </table> 
            <div id="dvscroll_ApprovalRequest" onscroll="SetScrollPos(this)" style="height:385px; overflow-x:hidden;overflow-y:scroll; border-bottom:solid 1px gray;" >   
            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
            <colgroup>
                    <col style="width:50px;" />
                    <col style="width:50px;" />
                    <col />
                    <col style="width:80px;" />
                    <col style="width:17px;" />
            </colgroup>
            <asp:Repeater ID="rptPendingForApprovalRequest" runat="server">
            <ItemTemplate>
             <tr style='font-size:11px;'>
                    <td style="text-align:center"><asp:CheckBox runat="server" ID="chkSelect" />
                    <asp:HiddenField runat="server" Value='<%#Eval("ManualId")%>' ID="hfdManualId" />
                    </td>
                    <td style="text-align:center"><%#Eval("SNo")%></td>
                    <td><div style="height:14px; overflow:hidden; text-overflow:ellipsis;"><%#Eval("MANUALNAME")%></div></td>
                    <td><%#Eval("VERSIONNO")%></td>
                    <td>&nbsp;</td>
             </tr>
            </ItemTemplate>
            </asp:Repeater>
            </table>
            <br />
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
