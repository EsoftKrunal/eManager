<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManualCommentsEntry.aspx.cs" Inherits="ManualCommentsEntry" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link rel="Stylesheet" href="CSS/style.css" />
</head>
<body style="background-color:#F8F0E6">
    <form id="form1" runat="server">
    <center>
    <div style="font-size: 20px; font-weight: bold; color: #CC6600; background-color: #C0C0C0; padding:4px;">Comments</div> 
    </center>
    <div style="padding:5px 30px 5px 30px; text-align:right; width:95%" >
    <!------------------------ COMMON HEADER SECTION ----------------------->
    <div>
            <div style="text-align:left;">
                <asp:Label runat="server" ID="lblManualName" Font-Bold="true" Font-Names="Arial" Font-Size="16px"></asp:Label>
                <asp:Label runat="server" ID="lblMVersion" Font-Bold="true" style="float:right"></asp:Label> <br />
                <asp:Label runat="server" ID="lblSVersion" Font-Bold="true" style="float:right"></asp:Label>
            </div>
            <div style="text-align:left; padding:3px;">
                <asp:Label runat="server" ID="lblHeading" Font-Bold="true" Font-Names="Arial" Font-Size="14px" ForeColor="DarkCyan"></asp:Label>
            </div>
            <div style="text-align:left; padding:3px;">
                Tags : <asp:Label runat="server" ID="lblContent" Font-Names="Arial" Font-Size="14px" ForeColor="DarkCyan" Font-Italic="true"></asp:Label>
            </div>
            <div style="text-align:left; padding:3px;">
                View File : 
                <asp:ImageButton runat="server" ID="btnAttachment" ImageUrl="~/Images/paperclip.gif" style="position:relative;top:0px;" onclick="btnAttachment_Click" />
            </div>
    </div>
    <!----------------------------------------------->

    <div style="text-align:left">
    <div runat="server" id="dvAddComments" >
        <asp:TextBox runat="server" ID="txtComments" Width="100%" Height="100px" TextMode="MultiLine" ></asp:TextBox>
        <asp:Button Text="Save Comments" ID="btnSave" runat="server" onclick="btnSave_Click" style="float:right; padding:4px; margin-top:3px; " />
        <br />
        <asp:Label ID="lblMsg" runat="server" style="font-weight:bold; font-size:15px; padding-bottom:10px; text-decoration:blink;" text="&nbsp;"></asp:Label>
    </div>
    <hr />
    <asp:Literal runat="server" ID="litHistory"></asp:Literal>
    </div>
    </div>
    </form>
</body>
</html>
