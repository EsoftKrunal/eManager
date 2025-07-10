<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomePage.aspx.cs" Inherits="HomePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="Styles/style.css" rel="stylesheet" type="text/css" />

    <title>Home Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <table align="center">
            <tr>
                <td>
                <br />
                <br />
               <asp:Label ID="Message" Text="" ForeColor="Red" runat="server"></asp:Label>
               
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
