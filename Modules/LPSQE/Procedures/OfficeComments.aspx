<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OfficeComments.aspx.cs" Inherits="SMS_OfficeComments" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Comments</title>
    <link rel="Stylesheet" href="CSS/style.css" />
</head>
<body style=" font-family:Arial; font-size:12px;">
    <form id="form1" runat="server">
    <div>
        <table style="width:100%; text-align:left; border-collapse:collapse;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td style=" text-align:center;font-weight:bold;color:White;font-size:15px;padding:5px;padding-left:5px;background-color:#4371a5;" >SMS Review </td>
            </tr>
            <tr>
            <td >
            <div style="border:solid 3px #4371a5">
            <center>
            <!------------------------ COMMON HEADER SECTION ----------------------->
            <div style='background-color:#F5EBFF; padding:0px;border-bottom:solid 3px #4371a5' >
                <div style="text-align:left; background-color:#B4C6DB; padding:3px;">
                    <asp:Label runat="server" ID="lblManualName" Font-Size="15px"></asp:Label>
                    <asp:Label runat="server" ID="lblMVersion" style="float:right; margin-top:3px;"></asp:Label> <br />
                    <asp:Label runat="server" ID="lblSVersion" style="float:right;margin-top:3px;"></asp:Label>
                </div>
                <div style="text-align:left; padding:3px;background-color:#C7D4E4; padding:3px;">
                    <asp:Label runat="server" ID="lblHeading" Font-Size="14px" ForeColor="DarkCyan"></asp:Label>
                </div>
                <div style="text-align:left; padding:3px; background-color:#ECF1F6; padding:3px;">
                    Tags : <asp:Label runat="server" ID="lblContent" ForeColor="DarkCyan" Font-Italic="true"></asp:Label>
                </div>
            </div>
            <!----------------------------------------------->
            <asp:LinkButton runat="server" Text="Enter New Comments.." id="lnlComments" onclick="lnlComments_Click"></asp:LinkButton>
            <div runat="server" id="dvComments" visible="false">
            <table cellpadding="2" cellspacing="0" width="100%" border="0" style="text-align:center; border-collapse:collapse;">        
            <tr>
                <td style="text-align:right; font-weight:bold; text-align:left; background-color:#">Comments :&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align:left;" >
                    <asp:TextBox ID="txtComment" TextMode="MultiLine" runat="server" Height="149px" Width="98%" style="border:solid 1px #c2c2c2"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align:right; font-weight:bold; text-align:left;">Change Requested :&nbsp;
                <asp:RadioButton ID="rdoCRYes" Text="Yes" runat="server" GroupName="CR" /> <asp:RadioButton ID="rdoCRNo" Text="No" runat="server" GroupName="CR" />
                </td>
            </tr>
            <tr>
                <td style="text-align:center;">
                    <asp:Button ID="btnSave" Text="Save" runat="server" onclick="btnSave_Click" style="border:none;padding:3px; padding-left:20px;padding-right:20px; text-align:center;color:white;background-color:#3399FF;" />
                    <asp:Button ID="btnClose" Text="Close" runat="server" onclick="btnClose_Click"  style="border:none;padding:3px;padding-left:20px;padding-right:20px;text-align:center;color:white;background-color:red;"/>&nbsp;&nbsp;
                    <asp:Label ID="lblMessage" ForeColor="Red" Font-Size="11px"  runat="server"></asp:Label>
                </td>
            </tr>
            </table>
            </div>
            <div style="width: 100%; overflow-y: scroll; overflow-x: hidden; height: 30px;border-top:solid 3px #4371a5">
                    <table cellspacing="0" rules="all" border="1" cellpadding="2" style="width: 100%;border-collapse: collapse; height: 30px;">
                       <colgroup>
                            <col style="text-align: left" width="110px" /> 
                            <col style="text-align: left" width="300px" /> 
                            <col style="text-align: left" width="100px" />
                            <col style="text-align: left"  />
                            <col width="20px" />                           
                        </colgroup>
                        <tr style="background-color:#4371a5;height: 19px; text-align :center;	color :White ;	font-weight :normal ;	font-family :Verdana;padding-top :5px;"  >
                            <td>&nbsp;Location</td>
                            <td>&nbsp;Reviewed By</td>
                            <td>&nbsp;Requested On</td>
                            <td>&nbsp;Comments</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    </div>
            <div id="dvscroll_Supply" runat="server" style="width: 100%; overflow-y: scroll; overflow-x: hidden; height: 500px;" class="scrollbox" onscroll="SetScrollPos(this)">
                    <table cellspacing="0" rules="all" border="1" cellpadding="2" style="width: 100%;border-collapse: collapse;">
                        <colgroup>
                            <col style="text-align: left" width="110px" /> 
                            <col style="text-align: left" width="300px" /> 
                            <col style="text-align: left" width="100px" />
                            <col style="text-align: left"  />
                            <col width="20px" />                           
                        </colgroup>
                        <asp:Repeater ID="rptComments" runat="server">
                            <ItemTemplate>
                                <tr class="row">
                                    <td >&nbsp;<%#Eval("Location")%></td>
                                    <td >&nbsp;<%#Eval("CommentBy")%> <span style='color:Blue; font-style:italic;'> [ <%#Eval("Positionname")%> ]</span></td>
                                    <td>&nbsp;<%#Common.ToDateString(Eval("Reqdate"))%></td>  
                                    <td align="left">&nbsp;<%#Eval("CommentText")%></td>
                                    <td>&nbsp;</td>                                                                
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </center>
            </div>
            </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
