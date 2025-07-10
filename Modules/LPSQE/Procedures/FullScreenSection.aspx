<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FullScreenSection.aspx.cs" Inherits="SMS_FullScreenSection" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
    <div style="width:99%">
        <div id="dvHeader" runat="server" style="padding:5px; padding-left:5px;background-color:#e2e2e2;width:100%; border-bottom:solid 1px black; padding-bottom:7px;">
            <div style="text-align:left;">
                <asp:Label runat="server" ID="lblManualName" Font-Bold="true" Font-Names="Arial" Font-Size="16px"></asp:Label>
                <asp:Label runat="server" ID="lblMVersion" Font-Bold="true" style="float:right"></asp:Label> <br />
                <asp:Label runat="server" ID="lblSVersion" Font-Bold="true" style="float:right"></asp:Label>
            </div>
            <div style="text-align:left; padding:3px;">
                <asp:Label runat="server" ID="lblHeading" Font-Bold="true" Font-Names="Arial" Font-Size="14px" ForeColor="#4371a5"></asp:Label>
            </div>
            <div style="text-align:left; padding:3px;">
                Tags : <asp:Label runat="server" ID="lblContent" Font-Names="Arial" Font-Size="14px" ForeColor="Black" Font-Italic="true"></asp:Label>
            </div>
        </div>
        <iframe runat="server" id="frmFile" width="100%" height="650px">
        </iframe> 
    </div>
    </div>
    </form>
</body>
</html>
