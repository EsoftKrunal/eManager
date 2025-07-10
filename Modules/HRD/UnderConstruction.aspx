<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UnderConstruction.aspx.cs" Inherits="UnderConstruction" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Under Construction</title>
    <link href="Styles/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center" width="100%">
            <tr>
                <td align="center" style="background-color:#4371a5; height: 23px" class="text" >
                    Under Construction
                </td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            <tr>
                 <td align="center" valign="middle" height="400px">
                     <asp:Image ID="Image1" runat="server" ImageUrl="~/Modules/HRD/Images/UnderConstruction.gif" />&nbsp;
                 </td>
            </tr>
            <tr><td>&nbsp;</td></tr>             
        </table>
    </form>
</body>
</html>
