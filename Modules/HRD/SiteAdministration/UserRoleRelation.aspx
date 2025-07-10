<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserRoleRelation.aspx.cs" Inherits="SiteAdministration_UserRoleRelation" MasterPageFile="~/Modules/HRD/SiteAdministration/ManageRoles.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">--%>
    <%--<div>--%>
     <table cellspacing="0" cellpadding="0" style=" width:97%; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center;" id="Table1">
        <tr>
            <td colspan="1">
                <asp:Label ID="lblupdation" runat="server" Font-Bold="False" ForeColor="Red" Height="11px"></asp:Label>&nbsp;
            </td>
        </tr>
        <tr>
            <td align="right" style="text-align: center; padding-left: 10px; padding-right: 10px">
             <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                    border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; padding-top:0px; padding-bottom:10px; width: 781px;">
                   
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td align="left">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 81px; padding-right: 5px; text-align: right;">
                            Role:</td>
                        <td style="width: 126px; text-align: left;">
                            <asp:DropDownList ID="ddrole" runat="server" CssClass="input_box" TabIndex="1"
                    Width="185px" AutoPostBack="True" OnSelectedIndexChanged="ddrole_SelectedIndexChanged" >
                            </asp:DropDownList></td>
                        <td style="width: 127px; padding-right: 5px; text-align: right;">
                &nbsp;Module : 
                        </td>
                        <td align="left" style="width: 100px; text-align: left;">
                <asp:DropDownList ID="ddmodule" runat="server" CssClass="required_box" TabIndex="1"
                    Width="195px" AutoPostBack="True" OnSelectedIndexChanged="ddmodule_SelectedIndexChanged">
                </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td style="text-align: left;">
                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="ddmodule"
                    ErrorMessage="Required." MaximumValue="1000" MinimumValue="0" Type="Integer"></asp:RangeValidator></td>
                    </tr>
                </table></fieldset>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 10px; padding-right: 10px" valign="top">
             <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                    border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; padding-top:0px; padding-bottom:10px; width: 781px;">
                    <legend><strong>Page List</strong></legend>
            <div style="overflow-x: hidden; overflow-y: scroll; width:100%; height: 206px;">
                <asp:GridView ID="GvRoles" runat="server" AutoGenerateColumns="False" GridLines="Horizontal" DataKeyNames="pageId"
                    Style="text-align: center" Width="93%" OnRowDataBound="GvRoles_RowDataBound" Height="100px">
                    <RowStyle CssClass="rowstyle" />
                    <SelectedRowStyle CssClass="selectedtowstyle" />
                    <PagerStyle CssClass="pagerstyle" />
                    <HeaderStyle CssClass="headerstylefixedheadergrid" />
                    <Columns>
                        <asp:TemplateField HeaderText="Select">
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" />
                            </ItemTemplate>
                            <HeaderTemplate><asp:CheckBox ID="chkall" runat="server" AutoPostBack="true" Text="Select All" OnCheckedChanged="do_check" /></HeaderTemplate>
                        </asp:TemplateField>
                        <%-- <asp:BoundField DataField="Username" HeaderText="User Name">
                            <ItemStyle Width="120px"  HorizontalAlign="Left" />
                        </asp:BoundField>--%>
                        <asp:TemplateField HeaderText="Page Name">
                            <ItemStyle HorizontalAlign="Left" Width="300px" />
                            <ItemTemplate>
                                <asp:Label ID="Vessel" runat="server" Text='<%#Eval("pagename")%>'></asp:Label>
                                <%--<asp:HiddenField ID="HiddenVesselId" runat="server" Value='<%#Eval("Moduleid")%>' />
                                <asp:HiddenField ID="hfdlogin" runat="server" Value='<%#Eval("Loginid") %>' />--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                </div>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td align="right">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Button ID="btn_Save" runat="server" CssClass="btn" OnClick="btn_Save_Click"
                    Text="Save" Visible="true" Width="59px" />
                <asp:Button ID="btn_Reset"
                        runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_Reset_Click"
                        TabIndex="22" Text="Reset" Visible="true" Width="59px" />&nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                &nbsp;
            </td>
        </tr>
   </table>
    <%--</div>--%>
    <%--</form>
</body>
</html>--%>
</asp:Content>
