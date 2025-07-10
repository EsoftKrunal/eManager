<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewManualCommentsHistory.aspx.cs" Inherits="ViewManualCommentsHistory" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link rel="Stylesheet" href="CSS/style.css" />
</head>
<body style="background-color:#F8F0E6">
    <form id="form1" runat="server">
    <center>
    <div style="font-size: 20px; font-weight: bold; color: #CC6600; background-color: #C0C0C0; padding:4px;">Comments</div> 
    <div style="font-size: 14px; font-weight: bold; color: #CC6600; background-color: #C0C0C0; padding:4px;" id="dvInviteCommentsBtn" runat="server">
        <asp:LinkButton ID="lnkInviteComments" runat="server" Text="Invite Comments" OnClick="btnInviteCommentsPopup_OnClick"></asp:LinkButton>
    </div> 
    </center>
    <div style="padding:5px 30px 5px 30px; text-align:right; width:95%" >
    <!------------------------ COMMON HEADER SECTION ----------------------->
    <div>
            <div style="text-align:left;">
                <asp:Label runat="server" ID="lblManualName" Font-Bold="true" Font-Names="Arial" Font-Size="16px"></asp:Label>
                <asp:Label runat="server" ID="lblMVersion" Font-Bold="true" style="float:right"></asp:Label> <br />
                <asp:Label runat="server" ID="lblSVersion" Font-Bold="true" style="float:right"></asp:Label>
            </div>
            <div style="text-align:left; padding:3px;">
                <asp:Label runat="server" ID="lblHeading" Font-Bold="true" Font-Names="Arial" Font-Size="14px" ForeColor="DarkCyan"></asp:Label>
            </div>
            <div style="text-align:left; padding:3px;">
                Tags : <asp:Label runat="server" ID="lblContent" Font-Names="Arial" Font-Size="14px" ForeColor="DarkCyan" Font-Italic="true"></asp:Label>
            </div>
    </div>
    <!----------------------------------------------->

    <div style="text-align:left">
    <div runat="server" id="dvAddComments" >
        <asp:TextBox runat="server" ID="txtComments" Width="100%" Height="100px" style="display:none;" ></asp:TextBox>
        <asp:Button Text="Save Comments" ID="btnSave" runat="server" onclick="btnSave_Click" style="float:right; padding:4px; margin-top:3px;display:none;" />
        <br />
        <asp:Label ID="lblMsg" runat="server" style="font-weight:bold; font-size:15px; padding-bottom:10px; text-decoration:blink;" text="&nbsp;"></asp:Label>
    </div>
    <hr />
    <asp:Literal runat="server" ID="litHistory"></asp:Literal>
    </div>
    </div>

    <%--------------------------------------%>
            <div style="position:absolute;top:0px;left:0px; height :100%; width:100%;" id="dvInviteComments" runat="server" visible="false">
            <center>
            <div style="position:absolute;top:0px;left:0px; height :100%; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:1000px; height:450px;text-align :center; border :solid 10px #d2d2d2; background : white; z-index:150;top:100px;opacity:1;filter:alpha(opacity=100)">
                <table cellpadding="2" cellspacing="0" style="width: 100%; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center; background-color:#f9f9f9">
                <tr>
                    <td colspan="2" class="text" style="height: 30px; background-color: #4371a5; text-align :center; font-weight:bold;color:White; ">Invite for Comments</td>
                </tr>
                <tr>
                    <td style='width:100px'>
                        Select Office : 
                    </td>
                    <td style="text-align:left"> 
                        <asp:DropDownList ID="ddlOffice" runat="server" CssClass="stlselect" AutoPostBack="true" OnSelectedIndexChanged="ddlOffice_OnSelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>
                </table>
                
                <div style="overflow-y:hidden; overflow-x:hidden;width: 100%;border-bottom:none; height:30px;" > 
                <table cellpadding="1" cellspacing="0" border="1" style="width: 100%;text-align:left;border-collapse:collapse; height:30px;" rules="cols">
                    <col  />
                    <col width="250px" />
                    <col width="250px" />
                    <col width="60px" />
                    <col width="17px" />
                    <tr style="background-color:#7070DB;color:White;">
                        <td>&nbsp;Emp Name </td>
                        <td>&nbsp;Position </td>
                        <td>&nbsp;Department </td>
                        <td>&nbsp;Select </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
                </div>
                <div style=" height:320px; overflow-y:scroll; overflow-x:hidden;"> 
                <table cellpadding="1" cellspacing="0" border="1" style="width: 100%;text-align:left;border-collapse:collapse;font-size:11px;" rules="all">
                    <col  />
                    <col width="250px" />
                    <col width="250px" />
                    <col width="60px" />
                    <col width="17px" />
                    <asp:Repeater runat="server" ID="rptEmployee">
                        <ItemTemplate>
                            <tr class="MO">
                                <td>&nbsp;
                                    <asp:Label ID="lblEmpName" runat="server" Text='<%#Eval("EmpName")%>'></asp:Label> 
                                    <asp:HiddenField ID="hfEmailID" runat="server" Value='<%#Eval("Email") %>' />
                                    <asp:HiddenField ID="hfPositionName" runat="server" Value='<%#Eval("PositionName") %>' />
                                 </td>
                                <td>&nbsp;<%#Eval("PositionName")%></td>
                                <td>&nbsp;<%#Eval("DeptName")%></td>
                                <td>&nbsp;<asp:CheckBox ID="chkSelectEmp" runat="server" /></td>
                                <td>&nbsp;</td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                </div>
                <div style="padding:4px;">
                    <div style="float:left">
                        <asp:Label ID="lblMsgInvitation" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                    </div>
                    <div style="float:right">
                    <asp:Button ID="btnCloseInvitationPopup" runat="server" OnClick="btnCloseInvitationPopup_OnClick" style="width:150px;border:none;padding:3px; padding-left:20px;padding-right:20px; text-align:center;color:white;background-color:red;" Text="  Close  " />
                    <asp:Button ID="btnSendInvitation" runat="server" OnClick="btnSendInvitation_OnClick" style="width:150px; border:none;padding:3px; padding-left:20px;padding-right:20px; text-align:center;color:white;background-color:#3399FF;" Text="  Send Invitation  " />
                    </div>
                    
                </div>

            </div>
        </center>
    </div>
    </form>
</body>
</html>
