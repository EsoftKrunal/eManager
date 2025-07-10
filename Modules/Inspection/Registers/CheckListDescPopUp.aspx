<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckListDescPopUp.aspx.cs" Inherits="Registers_CheckListDescPopUp" Title="Description" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body style=" margin:0px 0px 0px 0px; font-family:Arial; font-size:12px;">
    <form id="form1" runat="server">
    <div>
    <asp:Panel runat="server" ID="pnlG">
    <div style="background-color:#4DDBFF; color:black; padding:5px; font-size:15px;">VIQ Guidance :</div>
    <div style=" padding:5px">
        <asp:Label id="lblG" runat="server"></asp:Label>
    </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnlO">
    <div style="background-color:#4DDBFF; color:black; padding:5px; font-size:15px;">Office Guidance :</div>
    <div style=" padding:5px">
        <asp:Label id="lblO" runat="server"></asp:Label>
    </div>
    </asp:Panel>
    </div>
    </form>
</body>
</html>
