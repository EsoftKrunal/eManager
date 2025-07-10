<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InviteComments.aspx.cs" Inherits="InviteComments" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="ManualMenu.ascx" tagname="ManualMenu" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>[ Manual & Forms ] Pending for Approval</title>
    <link rel="Stylesheet" href="CSS/style.css" />
     <style>
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
     .MO
     {
         background-color:White;
     }
     .MO:hover
     {
         background-color:#f9f9f9;
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
    <div >
    <asp:ToolkitScriptManager runat="server" id="ScriptManager1"></asp:ToolkitScriptManager>
    <uc2:ManualMenu ID="ManualMenu1" runat="server" />
    <table style="border-collapse:collapse;" cellpadding="0" cellspacing="0" width="100%">
    <tr>
    <td style="vertical-align:top;border:solid 1px black; overflow:hidden;">
    
    <div style="padding:5px; background:#e2e2e2">
    &nbsp;&nbsp;Select Manual : <asp:DropDownList runat="server" ID="ddlPendingForApprovalmanuals" AutoPostBack="true" onselectedindexchanged="ddlPendingForApprovalmanuals_SelectedIndexChanged" CssClass='stlselect'></asp:DropDownList>
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="lblMessage" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
    </div>
    
    <div runat="server" id="dvPendingForApprovalRequest" visible="false">
    <table width="100%">
        <tr>
        <td valign="top" style="border:solid 1px #4371a5; height:500px;">
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
                    <col style="width:200px;" />
                    <col style="width:150px;" />
                    <col style="width:150px;" />
                    <col style="width:17px;" />
                    <tr align="left" class="blueheader" style=" background:#C2C2C2; font-weight:bold;">
                        <td style="text-align:center">View</td>
                        <td style="text-align:center">Sr#</td>
                        <td>Manual</td>
                        <td>Section</td>
                        <td>Heading</td>
                        <td>Revision#</td>
                        <td>Modified By/On</td>
                        <td>Status</td>
                        <td>Invite</td>
                        <td>&nbsp;</td>
                    </tr>
                </colgroup>
            </table> 
            <div style="height:440px; overflow-x:hidden;overflow-y:scroll; border-bottom:solid 1px gray;" >   
            <table border="1" cellpadding="2" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
            <colgroup>
                    <col style="width:50px;" />
                    <col style="width:40px;" />
                    <col style="width:250px;"/>
                    <col style="width:100px;" />
                    <col />
                    <col style="width:100px;" />
                    <col style="width:200px;" />
                    <col style="width:150px;" />
                    <col style="width:150px;" />
                    <col style="width:17px;" />
            </colgroup>
            <asp:Repeater ID="rptPendingForApprovalRequest" runat="server">
            <ItemTemplate>
             <tr style='font-size:11px;'>
                    <td style="text-align:center">
                        <a target="_blank" href='ViewManualSection.aspx?ManualId=<%#Eval("ManualId")%>&SectionId=<%#Eval("SectionId")%>&AddSection=No'><img src="../Images/HourGlass.png" /></a>
                        <asp:HiddenField ID="hfManualID" runat="server" Value='<%#Eval("ManualId")%>' />
                        <asp:HiddenField ID="hfManualVersion" runat="server" Value='<%#Eval("ManualId")%>' />
                    </td>
                    <td style="text-align:center"><%#Eval("SNo")%></td>
                    <td>
                        <div style="height:14px; overflow:hidden; text-overflow:ellipsis;"><asp:Label ID="lblManualName" runat="server" Text='<%#Eval("MANUALNAME")%>'></asp:Label></div>
                     </td>
                    <td>
                        <asp:Label ID="lblSecID" runat="server" Text='<%#Eval("SECTIONID")%>'></asp:Label>
                    </td>
                    <td><div style="height:14px; overflow:hidden; text-overflow:ellipsis;"><%#Eval("HEADING")%></div></td>
                    <td><%#Eval("SVERSION")%></td>
                    <td><%#Eval("MODIFIEDBY")%> / <%#Common.ToDateString(Eval("MODIFIEDON"))%></td>
                    <td><%#Eval("Status")%></td>
                    <td style="text-align:center">
                        <asp:ImageButton ID="btnInviteCommentsPopup" runat="server" ImageUrl="~/Images/Mail.png" OnClick="btnInviteCommentsPopup_OnClick" ToolTip="Invite Comments "  />
                    </td>
                    <%--<td>&nbsp;</td>--%>
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
            <div style='background-color:#d2d2d2; padding:5px;'> Please Enter Comments. </div> 
            <div style='padding:5px;'>
            <asp:TextBox runat="server" ID="txtComments1" Width='100%' Height="70px" TextMode="MultiLine"></asp:TextBox><br />
            <asp:Button runat="server" ID="btnSummit1" Text="Submit for Approval" OnClick="btnSummit1_Click" Height="25px" style="padding:2px; margin:2px;" />
            <asp:Button runat="server" ID="btnBack1" OnClick="btnBack1_Click" Text="Cancel" Height="25px" style="padding:2px; margin:2px;" />
            </div>
            
            </div>
            </center>
            </div>


            <%--------------------------------------%>
            <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="dvInviteComments" runat="server" visible="false">
            <center>
            <div style="position:absolute;top:0px;left:0px; height :750px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:840px; height:350px; padding :3px; text-align :center; border :solid 1px #4371a5; background : white; z-index:150;top:30px;opacity:1;filter:alpha(opacity=100)">

                <table cellpadding="2" cellspacing="0" style="width: 100%; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center; background-color:#f9f9f9">
                <tr>
                    <td class="text" style="height: 23px; background-color: #4371a5; text-align :center; font-weight:bold;font-size:large;color:White; ">Invite Comments</td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlOffice" runat="server" CssClass="stlselect" AutoPostBack="true" OnSelectedIndexChanged="ddlOffice_OnSelectedIndexChanged" style="float:left;width:150px;" ></asp:DropDownList>
                    </td>
                </tr>
                </table>
                <br />

                <%--background-color:#f9f9f9--%>
                <div style="overflow-y:hidden; overflow-x:hidden;width: 100%;border:#4371a5 1px solid;border-bottom:none; " > 
                <table cellpadding="1" cellspacing="0" border="1" style="width: 100%;text-align:left;border-collapse:collapse;" rules="all">
                    <col  />
                    <col width="150px" />
                    <col width="150px" />
                    <col width="60px" />
                    <col width="17px" />
                    <tr style="font-weight:bold;background-color:#c2c2c2;">
                        <td> Emp Name </td>
                        <td> Position </td>
                        <td> Department </td>
                        <td> Select </td>
                        <td></td>
                    </tr>
                </table>
                </div>
                <div style=" height:200px; overflow-y:scroll; overflow-x:hidden;" style="width: 100%;border:#4371a5 1px solid;"> 
                <table cellpadding="1" cellspacing="0" border="1" style="width: 100%;text-align:left;border-collapse:collapse;font-size:11px;" rules="all">
                    <col  />
                    <col width="150px" />
                    <col width="150px" />
                    <col width="60px" />
                    <asp:Repeater runat="server" ID="rptEmployee">
                        <ItemTemplate>
                            <tr class="MO">
                                <td>
                                    <asp:Label ID="lblEmpName" runat="server" Text='<%#Eval("EmpName")%>'></asp:Label> 
                                    <asp:HiddenField ID="hfEmailID" runat="server" Value='<%#Eval("Email") %>' />
                                 </td>
                                <td> <%#Eval("PositionCode")%></td>
                                <td> <%#Eval("DeptName")%></td>
                                <td><asp:CheckBox ID="chkSelectEmp" runat="server" /></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                </div>
                <div style="padding:4px;">
                    <asp:Button ID="btnCloseInvitationPopup" runat="server" CssClass="abtn" OnClick="btnCloseInvitationPopup_OnClick" Text="  Close  " />
                    <asp:Button ID="btnSendInvitation" runat="server" CssClass="abtn" OnClick="btnSendInvitation_OnClick" Text="  Send Invitation  " />
                    <asp:Label ID="lblMsgInvitation" runat="server" ForeColor="Red" Font-Bold="true" ></asp:Label>
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
