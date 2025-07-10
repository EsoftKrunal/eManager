<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewManualsList.aspx.cs" Inherits="ViewManualsList" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link rel="Stylesheet" href="CSS/style.css" />
   
</head>
<body style=" background-color:#D6EAF0">
    <form id="form1" runat="server">
    <table width="100%" border="0">
    <tr>
        <td style="width:110px; text-align:right;">Search Manual : </td>
        <td style="text-align:right"><asp:TextBox runat="server" ID="txtSearchManual" Width="98%"></asp:TextBox></td>
    </tr>
    <tr>
        <td colspan="2">
        <div style="width:100%; height:150px;border:solid 1px gray; overflow-x:hidden; overflow-y:scroll;">
            <div style="margin:5px">
                <asp:Literal runat="server" ID="litManuals"></asp:Literal>
            </div>
        </div>
    </tr>
    </table>
    </form>
</body>
</html>
