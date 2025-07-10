<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7"/>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>SHIPSOFT :: The Crew Management System</title>
<link href="Styles/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <table align="center" border="0" cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;">
      <tr>
        <td style="background-image:url(images/index_03.jpg); background-repeat:repeat-x">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td width="17%" valign="top" align="right">
                    <img src="Images/Logo/CompanyLogo.jpg" width="145px" height="121px" /></td>
                <td width="83%">&nbsp;</td>
              </tr>
            </table>
         </td>
      </tr>
      <tr>
        <td height="26" style="background-image:url(images/index_06.jpg); background-repeat:repeat-x; padding-right:10px">&nbsp;</td>
      </tr>
      <tr>
        <td   valign="top" >
        <table width="100%" border="0" cellspacing="0" cellpadding="0"  bgcolor="#ffffff">
          <tr>
            <td height="20" style="padding-left:23px">&nbsp;</td>
          </tr>
          <tr></tr>
          <tr>
            <td align="center" valign="top" style="padding-top:70px; padding-bottom:100px">
            <form runat="server" id="LoginForm"> 
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                &nbsp;
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                <table border="0" cellspacing="0" cellpadding="0" bgcolor="#FFFFFF" style="border:#4371a5 1px solid; width: 339px;">
                <tr>
                  <td height="22" colspan="2" bgcolor="#4371a5" class="text" style="padding-left:8px">Member Login</td>
                </tr>
                <tr>
                  <td colspan="2" style="text-align: right">
                      <table cellpadding="0" cellspacing="0" width="100%">
                          <tr>
                              <td style="width: 3px">
                              </td>
                              <td style="width: 8px">
                              </td>
                              <td style="text-align: center; width: 255px;">
                      </td>
                              <td style="padding-right: 3px; width: 28px; padding-top: 3px; text-align: right">
                                  <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                      <ProgressTemplate>
                                          <img src="Images/progress_Icon.gif" />
                                      </ProgressTemplate>
                                  </asp:UpdateProgress>
                              </td>
                          </tr>
                      </table>
                      &nbsp;</td>
                </tr>
                    <tr>
                        <td align="right" colspan="2" height="25" style="text-align: center">
                      <asp:Label ID="Message" runat="server" ForeColor="Red" Text="Label"></asp:Label></td>
                    </tr>
                <tr style="color:#0e64a0" >
                  <td height="25" align="right" style="width: 160px">User ID :</td>
                  <td style="padding-left:10px; width: 216px;" align="left">
                    <asp:TextBox runat="server" ID="LoginId" CssClass="input_box" style="padding-left:3px" TabIndex="1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="LoginId" ErrorMessage="Required."></asp:RequiredFieldValidator>
                  </td>
                        
                </tr>
                <tr style="color: #0e64a0">
                  <td height="25" align="right" style="width: 160px">Password :</td>
                  <td style="padding-left:10px; width: 216px;" align="left">
                    <asp:TextBox runat="server" ID="Password" CssClass="input_box" style="padding-left:3px" TextMode="Password" TabIndex="2"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Password" ErrorMessage="Required."></asp:RequiredFieldValidator>
                </tr>
                <tr>
                  <td height="20" style="width: 160px" >&nbsp;</td>
                  <td style="padding-left:20px; width: 216px;">&nbsp;</td>
                </tr>
                <tr>
                 
                  <td colspan="2" align="center">
                        <asp:Button ID="btnSubmit" runat="server" Text="Login" CssClass="btn" OnClick="btnSubmit_Click" TabIndex="3" />
                        <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn" OnClick="btnReset_Click" TabIndex="4" />
                  </td>
                </tr>
                <tr>
                  <td colspan="2" style="height: 13px">
                      &nbsp;&nbsp;</td>
                </tr>
            </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                &nbsp; &nbsp;&nbsp;
            </form>
            </td>
          </tr>
          <tr>
            <td style="height:25px; background-color:#4673a7" align="center" class="text_bottom">Developed by Energios Maritime Pvt. Ltd . All rights reserved.</td>
          </tr>
          <tr></tr>
        </table></td>
      </tr>
    </table>
</body>
</html>
