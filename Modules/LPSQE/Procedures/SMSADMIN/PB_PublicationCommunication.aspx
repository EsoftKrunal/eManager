<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PB_PublicationCommunication.aspx.cs" Inherits="PB_PublicationCommunication"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link rel="Stylesheet" href="../../HRD/Styles/StyleSheet.css" />
    <script type="text/javascript" src="JS/Common.js"></script>
</head>
<body>
    <form id="form1" runat="server" style="font-family:Arial;font-size:12px;" >
    <asp:ToolkitScriptManager runat="server" id="ScriptManager1"></asp:ToolkitScriptManager>
    <asp:UpdatePanel runat="server" ID="up1">
    <ContentTemplate>
    <div>
    <div style="min-height:425px;" style="width:100%;border-bottom:solid 1px gray;"> 
    <div style="padding:0px; text-align:right">
    <table width="100%" cellpadding="3" style="text-align:center; border-collapse:collapse;text-align:left; background-color:#E0EBFF;" border="1"> 
    <tr>
    <td style='width:100px;'>Vessel :</td>
        <td><asp:DropDownList ID="ddlVessel_F" runat="server" Width="150px" ></asp:DropDownList></td>
    <td style='text-align:right'>
        <asp:Button runat="server" id="btnSearch" Text="Search" OnClick="btnSearch_Click" CausesValidation="false" CssClass="btn"  />
        <asp:Button runat="server" id="btnExport" Text="Export" OnClick="btnExport_Click" CausesValidation="false" CssClass="btn"  />
    </td>
    </tr>
    </table>
    </div>
    <div style=" overflow-x:hidden; overflow-y:scroll; height:22px; border:solid 1px gray;border-bottom:none;">
    <table width="100%" cellpadding="2" style="text-align:center; border-collapse:collapse;text-align:left; background-color:Wheat;" border="1"> 
        <colgroup>
            <col />
            <col width="100px" />
            <col width="100px" />
            <col width="100px" />
            <col width="250px" />
            <col width="30px" />
        </colgroup>
            <tr class= "headerstylegrid">
                <td>Publication</td>
                <td>Type</td>
                <td>Office/Ship</td>
                <td>Mode</td>
                <td>Publisher</td>
                <td>&nbsp;</td>
            </tr>
    </table>
    </div>
    <div style=" overflow-x:hidden; overflow-y:scroll; height:334px; border:solid 1px gray; border-top:none;">
    <table width="100%" cellpadding="2" style="text-align:center; border-collapse:collapse;text-align:left;" border="1"> 
        <colgroup>
            <col />
            <col width="100px" />
            <col width="100px" />
            <col width="100px" />
            <col width="250px" />
            <col width="30px" />
        </colgroup>
        <asp:Repeater ID="rptData" runat="server">
            <ItemTemplate>
                <tr>
                    <td><%#Eval("PublicationName")%></td>
                    <td><%#Eval("TYPENAME")%></td>
                    <td><%#Eval("OfficeShip")%></td>
                    <td><%#Eval("MODENAME")%></td>
                    <td><%#Eval("PUBLISHERNAME")%></td>
                    <td>&nbsp;</td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        </table>
    </div>
    </div>
    </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnExport" />
    </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
