<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PB_PublicationLocation.aspx.cs" Inherits="PB_PublicationLocation"  %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="SMSManualMenu.ascx" tagname="SMSManualMenu" tagprefix="uc3" %>
<%@ Register src="PB_PublicationSubTab.ascx" tagname="PB_PublicationSubTab" tagprefix="uc4" %>
<%@ Register src="PB_PublicationRegisterSubTab.ascx" tagname="PB_PublicationRegisterSubTab" tagprefix="uc5" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link rel="Stylesheet" href="CSS/style.css" />
    <script type="text/javascript" src="JS/Common.js"></script>
</head>
<body>
    <form id="form1" runat="server" >
    <asp:ToolkitScriptManager runat="server" id="ScriptManager1"></asp:ToolkitScriptManager>
    <uc3:SMSManualMenu ID="SMSManualMenu1" runat="server" />
    <uc4:PB_PublicationSubTab ID="PB_PublicationSubTab2" runat="server" />
    <uc5:PB_PublicationRegisterSubTab ID="PB_PublicationRegisterSubTab1" runat="server" />
    <asp:UpdatePanel runat="server" ID="up1">
    <ContentTemplate>
    <div>
    <div style="min-height:425px;" style="width: 100%;border:#4371a5 1px solid;"> 
    <div style="padding:5px; text-align:right">
        <asp:Button runat="server" id="btnAdd" Text="Add Publication Location" OnClick="btnAdd_Click" style="padding:2px ; background-color:#FFADD6 "/>
    </div>
    <table width="100%" cellpadding="2" style="text-align:center; border-collapse:collapse;text-align:left; background-color:Wheat;" border="1"> 
        <col width="50px" />
        <col width="50px" />
        <col width="150px" />
        <col />
        <tr>
        <td style="text-align:center;"> 
            Edit
        </td>
        <td style="text-align:center;"> 
            Delete
        </td>
        <td> Office / Ship  </td>
        <td> Publication Location </td>
    </tr>
    </table>
    <table width="100%" cellpadding="2" style="text-align:center; border-collapse:collapse;text-align:left;" border="1"> 
        <col width="50px" />
        <col width="50px" />
        <col width="150px" />
        <col />
        <asp:Repeater ID="rptData" runat="server">
            <ItemTemplate>
                <tr>
                    <td style="text-align:center;"> 
                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" OnClick="lnlEdit_OnClick" CommandArgument='<%#Eval("LocationId")%>' ForeColor="Blue"></asp:LinkButton>
                    </td>
                    <td style="text-align:center;"> 
                        <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" OnClick="lnlDelete_OnClick" CommandArgument='<%#Eval("LocationId")%>' ForeColor="Red" OnClientClick="return window.confirm('Are you sure to delete?');"></asp:LinkButton>
                    </td>
                    <td> <%#Eval("OSName")%> </td>
                    <td> <%#Eval("LocationName")%> </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    </div>
    <div style="position:absolute;top:0px;left:0px; height :470px; width:100%;" id="dvPopUp" runat="server" visible="false">
        <center>
            <div style="position:absolute;top:0px;left:0px; height :560px; width:100%; background-color:Gray; z-index:100; opacity:0.4;filter:alpha(opacity=40)"></div>
            <div style="position :relative; width:540px; height:140px; padding :5px; text-align :center; border :solid 0px #FFADD6; background : white; z-index:150;top:100px;opacity:1;filter:alpha(opacity=100)">
                 <div>
                 <div style="padding:4px; background-color:#66E0FF; color:White;"> Add / Edit Publicaton Location </div>
                 <div style="border:solid 1px #66E0FF; height:113px;">
                    <br />
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td> Office / Ship : </td>
                            <td style="text-align:left">
                                <asp:DropDownList ID="ddlOfficeShip" runat="server" Width="150px" style=" background-color:#FFFFC2">
                                    <asp:ListItem Text="< -- Select -- >" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Office" Value="O"></asp:ListItem>
                                    <asp:ListItem Text="Ship" Value="S"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlOfficeShip" Operator="GreaterThan" ErrorMessage="*" ValueToCompare="0"></asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                        <td> Publication Location : </td>
                        <td style="text-align:left"> 
                            <asp:TextBox runat="server" ID="txtPublicationLocation" Width="250px" MaxLength="50" style=" background-color:#FFFFC2"></asp:TextBox> 
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ErrorMessage="*" ControlToValidate="txtPublicationLocation"></asp:RequiredFieldValidator>
                        </td>
                        </tr>
                    </table>
                    <br />
                    <asp:Button runat="server" id="btnSave" Text=" Save " OnClick="btnSave_Click" style="padding:2px ; background-color:Green; color:White;"/> 
                    <asp:Button runat="server" id="btnClose" Text=" Close " OnClick="btnClose_Click" style="padding:2px ; background-color:Red; color:White;" CausesValidation="false" /> 
                 </div>
                 </div>
            </div>
        </center>
      </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
