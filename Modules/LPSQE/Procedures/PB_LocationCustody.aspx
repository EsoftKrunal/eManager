<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PB_LocationCustody.aspx.cs" Inherits="PB_LocationCustody"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
     <link rel="Stylesheet" href="../../HRD/Styles/StyleSheet.css" />
    <script type="text/javascript" src="JS/Common.js"></script>
</head>
<body>
    <form id="form1" runat="server" >
    <asp:ToolkitScriptManager runat="server" id="ScriptManager1"></asp:ToolkitScriptManager>
    <asp:UpdateProgress runat="server" ID="progress1" AssociatedUpdatePanelID="up1">
    <ProgressTemplate>
        <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;">
        <center>
            <div style="position:absolute;top:0px;left:0px; height :560px; width:100%; background-color:Gray; z-index:100; opacity:0.2;filter:alpha(opacity=20)"></div>
            <div style="position :relative; width:450px; height:50px; padding :5px; text-align :center; border :solid 0px #FFADD6; background : white; z-index:150;top:250px;opacity:1;filter:alpha(opacity=100)">
                <img src="Images/progress_bar.gif" />
            </div>
        </center>
        </div>
    </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="up1">
    <ContentTemplate>
    <div>
    <div style="min-height:425px;" style="width:100%;border-bottom:solid 1px gray;"> 
    <div style="padding:0px; text-align:right">
    <table width="100%" cellpadding="3" style="text-align:center; border-collapse:collapse;text-align:left; background-color:#E0EBFF;" border="1"> 
    <tr>
    <td style="width:100px">Office / Ship :</td>
    <td style="width:120px">
            <asp:DropDownList ID="ddlOfficeShip_F" runat="server" Width="120px" OnSelectedIndexChanged="ddlOfficeShip_F_OnSelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Text="< -- Select -- >" Value="0"></asp:ListItem>
                <asp:ListItem Text="Office" Value="O"></asp:ListItem>
                <asp:ListItem Text="Ship" Value="S"></asp:ListItem>
            </asp:DropDownList>
        </td>
    <td>
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
            <col width="150px" />
            <col width="150px" />
        </colgroup>
    <tr class= "headerstylegrid">
                <td>Publication</td>
                <td>Type</td>
                <td>Office/Ship</td>
                <td>Mode</td>
                <td>Publisher</td>
                <td>Location</td>
                <td>Custody</td>
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
            <col width="150px" />
            <col width="150px" />
        </colgroup>
        <asp:Repeater ID="rptData" runat="server" OnItemDataBound="rptData_OnItemDataBound">
            <ItemTemplate>
                <tr>
                    <td><%#Eval("PublicationName")%></td>
                    <td><%#Eval("TYPENAME")%></td>
                    <td><%#Eval("OfficeShip")%></td>
                    <td><%#Eval("MODENAME")%></td>
                    <td><%#Eval("PUBLISHERNAME")%></td>
                    <td>
                        <asp:HiddenField runat="server" id="hfdPublicationId" Value='<%#Eval("PublicationId")%>' />
                        <asp:DropDownList runat="server" ID="ddlLocation" DataSource='<%#getLocation()%>' DataTextField="LocationName" DataValueField="LocationId"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlCustody" DataSource='<%#getLocation()%>' DataTextField="LocationName" DataValueField="LocationId"></asp:DropDownList>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        </table>
    </div>
    <div style="padding:5px; text-align:right">
        <asp:Button runat="server" id="Button1" Text="Save" OnClick="btnSave_Click" style="padding:2px ; background-color:#FFADD6 "/>
    </div>
    </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
