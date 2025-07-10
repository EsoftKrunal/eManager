<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExceptionPage.aspx.cs" Inherits="ExceptionPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title></title>
<link href="Styles/style.css" rel="stylesheet" type="text/css" />
</head>

<body>
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center" width="100%">
            <tr style="height:23px">
                <td align="center" style="background-color:#4371a5;" class="text">
                    Sitewide Exception Handler
                </td>
            </tr>
            <tr style="height:30px"><td>&nbsp;</td></tr>                        
            <tr style="height:30px">
                <td>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Modules/HRD/Images/ExceptionIcon.gif" />
                </td>
            </tr>
            <tr style="height:30px"><td>&nbsp;</td></tr>            
            <tr>
                <td align="center" valign="middle" style="color:Red">
                 SORRY FOR THE INCONVENIENCE!
                 </td>
            </tr>
            <tr style="height:30px"><td>&nbsp;</td></tr>            
            <tr>
                <td align="center" valign="middle">
                 You have been redirected to this page because the application has encountered an unexpected exception.
                 </td>
            </tr>
            <tr style="height:30px"><td>&nbsp;</td></tr>            
            <tr>
                <td align="center" valign="middle">
                 Following may be the possibilities.....
                 </td>
            </tr>
            <tr style="height:30px"><td>&nbsp;</td></tr>            
            <tr>
                <td align="center" valign="middle">
                 1. You may have entered some values which are not allowed or illegal.
                 </td>
            </tr>
            <tr>
                <td align="center" valign="middle">
                 2. There may be some other exception like network error, resource error etc.
                 </td>
            </tr>
            <tr style="height:30px"><td>&nbsp;</td></tr>            
            <tr><td>
                <asp:Button ID="btnHome" runat="server" OnClick="btnHome_Click" Text="Goto HomePage" CssClass="btn" />&nbsp;</td></tr>
            <tr style="height:30px"><td>&nbsp;</td></tr>                
        </table>
    </form>
</body>
</html>
