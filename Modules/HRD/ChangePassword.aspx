<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Crew Member Details</title>
    <link href="styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="styles/sddm.css" />
    <link rel="stylesheet" type="text/css" href="Styles/StyleSheet.css" />
    <script language="javascript" type="text/javascript">
function CallPrint(strid)
{
 var prtContent = document.getElementById(strid);
 var WinPrint = window.open('','','letf=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,status=0');
 WinPrint.document.write(prtContent.innerHTML);
 WinPrint.document.close();
 WinPrint.focus();
 WinPrint.print();
 WinPrint.close();
 prtContent.innerHTML=strOldOne;
}
function Show_Image_Large(obj)
{
window.open(obj.src,"","resizable=1,toolbar=0,scrollbars=1,status=0"); 
}
function Show_Image_Large1(path)
{
window.open(path,"","resizable=1,toolbar=0,scrollbars=1,status=0"); 
}
    </script>
</head>
<body style=" margin: 0 0 0 0;" >
<form id="form1" runat="server">
    <div style="text-align: center">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table  border="0" cellpadding="0" cellspacing="0" width="100%" style=" text-align:center">
                                                                <tr>
                                                                    <td align="center" valign="top" >
                                                                    </td>
                                                                </tr>
                                                            </table>
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center">
            <tr>
                <td align="center" style="background-color:#4371a5; height: 23px; width: 100%;" class="text" >
                    Change Password</td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr><td style="width: 825px">&nbsp;</td></tr>
                        <tr><td style="padding-right:10px; text-align:center; color:Red; width: 825px; height: 13px;"><asp:Label ID="lblMessage" runat="Server" Font-Size="12px" ForeColor="Red" meta:resourcekey="lblMessageResource1"></asp:Label></td></tr>
                        <tr>
                            <td style="padding-right: 10px; width: 825px; color: red; height: 13px; text-align: center">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="padding-right: 10px; padding-left: 10px; padding-bottom: 10px;text-align: left; width: 825px;">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="height: 15px; width:198px;">
                                            &nbsp;</td>
                                        <td style="height: 15px; width: 118px; text-align: right; padding-right: 10px;">
                                            Old Password :</td>
                                        <td style="height: 15px; width: 143px;">
                                            <asp:TextBox ID="txt_OldPassword" MaxLength="16" runat="server" CssClass="required_box" TextMode="Password"></asp:TextBox></td>
                                        <td class="input_box" style="height: 15px; width: 198px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 198px; height: 15px">
                                        </td>
                                        <td style="width: 118px; height: 15px; text-align: right">
                                            &nbsp;</td>
                                        <td style="width: 143px; height: 15px; text-align: left;">
                                            <asp:RequiredFieldValidator  ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_OldPassword"
                                                ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                                        <td style="height: 15px; width: 198px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 198px; height: 11px">
                                        </td>
                                        <td style="width: 118px; height: 11px; text-align: right; padding-right: 10px;">
                                            New Password :</td>
                                        <td style="width: 143px; height: 11px">
                                            <asp:TextBox ID="txt_NewPassword"  MaxLength="16" runat="server" CssClass="required_box" TextMode="Password"></asp:TextBox></td>
                                        <td style="height: 11px; width: 198px;">
                                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                            &nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 198px; height: 11px">
                                        </td>
                                        <td style="width: 118px; height: 11px; text-align: right; padding-right: 10px;">
                                            (Min. 6 chars)&nbsp;</td>
                                        <td style="width: 143px; height: 11px">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_NewPassword"
                                                ErrorMessage="Required." Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_NewPassword"
                                                ErrorMessage="Min. 6 chars" ValidationExpression="^.{6,19}$"></asp:RegularExpressionValidator></td>
                                        <td style="height: 11px; width: 198px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 198px; height: 11px">
                                        </td>
                                        <td style="width: 118px; height: 11px; text-align: right; padding-right: 10px;">
                                            Confirm Password :</td>
                                        <td style="width: 143px; height: 11px">
                                            <asp:TextBox ID="txt_ConfirmPassword" MaxLength="16" runat="server" CssClass="required_box" TextMode="Password"></asp:TextBox></td>
                                        <td style="height: 11px; width: 198px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 198px; height: 11px">
                                        </td>
                                        <td style="width: 118px; height: 11px; text-align: right">
                                        </td>
                                        <td style="height: 11px" colspan="2">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_ConfirmPassword"
                                                ErrorMessage="Required." Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txt_NewPassword"
                                                ControlToValidate="txt_ConfirmPassword" ErrorMessage="Passwords don't Match."></asp:CompareValidator></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 198px; height: 11px">
                                        </td>
                                        <td style="width: 118px; height: 11px; text-align: right">
                                            &nbsp;</td>
                                        <td style="width: 143px; height: 11px">
                                            </td>
                                        <td style="height: 11px; width: 198px;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 198px; height: 11px">
                                        </td>
                                        <td colspan="2" style="height: 11px; text-align: center">
                                            <asp:Button ID="btn_Submit" runat="server" CssClass="input_box" Text="Submit" OnClick="btn_Submit_Click" Width="66px" /></td>
                                        <td style="height: 11px; width: 198px;">
                                        </td>
                                    </tr>
                                </table>
                                     <div id="divPrint">
                                         &nbsp;</div></td></tr></table>
                </td>
            </tr>
        </table>
        </div>
    </form>
</body>
</html>
                                        
