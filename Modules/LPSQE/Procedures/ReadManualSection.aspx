<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReadManualSection.aspx.cs" Inherits="ReadManualSection" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Frameset//EN" "http://www.w3.org/TR/html4/frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link rel="Stylesheet" href="CSS/style.css" />
    <style type="text/css">
        .auto-style1 {
            position: relative;
            top: 0px;
            left: 0px;
        }
    </style>
</head>
<body style="margin:0px 0px 0px 0px">
    <form id="form1" runat="server">
         <center>
        <table style="width:100%; text-align:left" border="0" cellpadding="10" cellspacing="5">
            <tr>
                <td style="text-align:left; " colspan="2">View Manual : <hr /></td>
            </tr>
            <tr>
            <td style="text-align:left; width:80px;"> 
                <asp:ImageButton runat="server" ID="btnAttachment" ImageUrl="~/Modules/HRD/Images/attachment.png" Height="70px" onclick="btnAttachment_Click" CssClass="auto-style1" />
            </td>
            <td style="text-align:left;">
                <asp:LinkButton ID="lblFileName" Text="" runat="server" onclick="lblFileName_Click"></asp:LinkButton>
            </td>
            </tr>
        </table>
        </center>
    </form>
</body>
</html>
