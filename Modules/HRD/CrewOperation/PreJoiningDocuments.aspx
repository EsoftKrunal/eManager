<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PreJoiningDocuments.aspx.cs" Inherits="Modules_HRD_CrewOperation_PreJoiningDocuments" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
    <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="JS/Common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-family:Arial;font-size:12px;">
    <center>
        <div class="text headerband">  Prejoing Documents </div>
    
     <h3><asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label> </h3>
        <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:1000px;border-collapse:collapse;border:solid 1px Black" >
            <col style="text-align:right" />
            <col style="text-align:right" />
            <col style="text-align:left" />
            <col style="text-align:left" />
            <tr>
                <td >&nbsp;</td>
                <td style="text-align :left">Vessel Name :</td>
                <td style="text-align :left"><asp:TextBox runat="server" ID="txtVessel" Width="500px" Enabled="false"></asp:TextBox> </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="text-align :left">Manning Agent :</td>
                <td style="text-align :left"><asp:DropDownList runat="server" ID="ddlManningAgent" AutoPostBack="true" Width="500px" OnSelectedIndexChanged="ddlManningAgent_SelectedIndexChanged" ></asp:DropDownList></td>
                <td>&nbsp;</td>
            </tr>
             <tr>
                <td>&nbsp;</td>
                <td style="text-align :left">Documents :</td>
                <td style="text-align :left"><asp:DropDownList runat="server" ID="ddlDocuments" AutoPostBack="true" Width="500px" OnSelectedIndexChanged="ddlDocuments_SelectedIndexChanged" >
                   <%-- <asp:ListItem Value="0" Text="< Select >"></asp:ListItem>--%>
                                             </asp:DropDownList></td>
                <td>&nbsp;</td>
            </tr>
             
            
            <tr>
                <td colspan="4" style=" text-align:left" >
                <asp:HiddenField runat="server" ID="hfdMessage" /> 
                    <div contenteditable="true" runat="server" id="dvDocContent"  style="text-align :left; width :100%; height :600px; overflow-x :hidden ; overflow-y :scroll; border:solid 1px Gray;">
                        <asp:Literal runat="server" ID="litMessage"></asp:Literal> 
                    </div>
                </td>
            </tr>
             <%-- <tr>
                <td colspan="4">
                    <asp:Button runat="server" ID="btnPrint" Text="Print"  OnClientClick="hfdMessage.value=dvContent.innerHTML;" CssClass="btn" />
                </td>
            </tr>--%>
        </table>
    </center>
    </div>
    </form>
</body>
</html>
