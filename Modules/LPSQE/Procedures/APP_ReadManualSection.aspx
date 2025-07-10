<%@ Page Language="C#" AutoEventWireup="true" CodeFile="APP_ReadManualSection.aspx.cs" Inherits="APP_ReadManualSection" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Frameset//EN" "http://www.w3.org/TR/html4/frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link rel="Stylesheet" href="CSS/style.css" />
</head>
<body style="margin:0px 0px 0px 0px">
    <form id="form1" runat="server">
        <table style="width:100%; text-align:left" border="0" cellpadding="10" cellspacing="5">
            <tr>
                <td style="text-align:left; " colspan="2">View Manual : <hr /></td>
            </tr>
            <tr>
            <td style="text-align:left; width:80px;"> 
                <asp:ImageButton runat="server" ID="btnAttachment" ImageUrl="~/Modules/HRD/Images/attachment.png" style="position:relative;top:0px;" Height="70px" onclick="btnAttachment_Click" />
            </td>
            <td style="text-align:left;">
                <asp:LinkButton ID="lblFileName" Text="" runat="server" onclick="lblFileName_Click"></asp:LinkButton>
            </td>
            </tr>
        </table>
        <center>
    </form>
</body>
</html>
