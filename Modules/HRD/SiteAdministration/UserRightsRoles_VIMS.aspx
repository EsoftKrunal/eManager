<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Modules/HRD/SiteAdministration/ManageRights_VIMS.master" CodeFile="UserRightsRoles_VIMS.aspx.cs" Inherits="SiteAdministration_UserRightsRoles_VIMS" Title="" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
</head>
<body style=" margin: 0 0 0 0;">
    <form id="form1" runat="server">
    <div style="text-align:center">--%>
    <%--<table cellspacing="0" border="0" cellpadding="0" style="width:825px; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center">
        <tr>
        <td colspan="2" align="center" style="background-color:#4371a5; height: 23px; width: 100%;" class="text" >
            User Rights (VIMS)&nbsp;
                </td>
         </tr>
        <tr>
            <td colspan="2"> 
             <asp:Label ID="lbl_UserLogin_Message" runat="server" ForeColor="#C00000" Visible="False"></asp:Label>&nbsp;
            </td>
        </tr>
        <tr>
            <td align="right" colspan="2">
                <asp:ImageButton ID="ImageButton1" runat="server" ToolTip="Back to Dash Board" Height="42px" ImageUrl="~/Modules/HRD/Images/AdminDashBoard.gif"
                    OnClick="ImageButton1_Click" Width="80px" CausesValidation="False" />
                &nbsp;&nbsp;
            </td>
        </tr>    
     <tr>
       <td>--%>
        <table cellpadding="0" cellspacing="0" style="width:97%; border-right: #4371a5 1px solid; border-top: #4371a5 1px solid;
            border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid;
            text-align: center">
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblupdation" runat="server" ForeColor="Red" Height="11px" Text="Record Updated.."
                        Visible="False"></asp:Label>&nbsp;
                </td>
            </tr>
            <tr>
                <td style="padding-left: 5px; width: 25%">
                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                        padding-bottom: 10px; border-left: #8fafdb 1px solid; padding-top: 0px; border-bottom: #8fafdb 1px solid;
                        height: 188px">
                        <legend><strong>Modules List</strong></legend>
                        <br />
                        <div style="overflow-y: auto; overflow-x: hidden; height: 164px">
                            <asp:GridView ID="GvmoduleList" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
                                Height="85px" OnSelectedIndexChanged="GvmoduleList_doselect" Style="text-align: center"
                                Width="98%">
                                <RowStyle CssClass="rowstyle" />
                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                <PagerStyle CssClass="pagerstyle" />
                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Module Name">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnmodule" runat="server" CommandName="Select" Font-Underline="false"
                                                Text='<%#Eval("ApplicationModule") %>'></asp:LinkButton>
                                            <asp:HiddenField ID="HiddenModuleId" runat="server" Value='<%#Eval("ApplicationModuleId")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </fieldset>
                </td>
                <td style="padding-right: 5px; padding-left: 5px; width: 75%">
                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                        padding-bottom: 10px; border-left: #8fafdb 1px solid; padding-top: 0px; border-bottom: #8fafdb 1px solid;
                        height: 187px">
                        <legend><strong>Authority Details</strong></legend>
                        <br />
                        <div style="overflow-x:hidden; overflow-y:scroll; height: 163px">
                            <asp:GridView ID="GVAuthority" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
                                Style="text-align: center" Width="98%">
                                <RowStyle CssClass="rowstyle" />
                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                <PagerStyle CssClass="pagerstyle" />
                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                <Columns>
                                    <%--<asp:TemplateField HeaderText="User Roles">
                                        <ItemStyle HorizontalAlign="Left" Width="200px" />
                                        <ItemTemplate>
                                            <asp:Label ID="RoleName" runat="server" Text='<%#Eval("RoleName")%>'></asp:Label>
                                            <asp:HiddenField ID="HiddenRoleId" runat="server" Value='<%#Eval("RoleId")%>' />
                                             <asp:HiddenField ID="HiddenRole1Id" runat="server" Value='<%#Eval("RoleId1")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="User Id">
                                        <ItemStyle HorizontalAlign="Left" Width="200px" />
                                        <ItemTemplate>
                                            <asp:Label ID="UserId" runat="server" Text='<%#Eval("UserId")%>'></asp:Label>
                                            <asp:HiddenField ID="HiddenLogin1Id" runat="server" Value='<%#Eval("LoginId1")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Add">
                                        <ItemStyle HorizontalAlign="Center" Width="35px" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkCanAdd" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Modify">
                                        <ItemStyle HorizontalAlign="Center" Width="35px" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkCanModify" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemStyle HorizontalAlign="Center" Width="35px" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkCanDelete" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Print">
                                        <ItemStyle HorizontalAlign="Center" Width="35px" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkCanPrint" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="First App">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkFrstApp" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Scnd App">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkScndApp" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td align="right"> </td>
                <td align="right"> &nbsp;&nbsp; </td>
            </tr>
            <tr>
                <td align="right"> </td>
                <td align="right">
                    <asp:Button ID="btn_Authority_save" runat="server" CssClass="btn" OnClick="btn_Authority_save_Click" Text="Save" Visible="true" Width="59px" />
                    <asp:Button ID="btn_Authority_Reset" runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_Authority_Reset_Click" TabIndex="22" Text="Reset" Visible="true" Width="59px" />&nbsp; &nbsp;</td>
                </tr>
                <tr>
                <td align="right"> </td>
                <td align="right"> &nbsp; </td>
            </tr>
        </table>
       <%--</td>
      </tr>
     </table> --%>
    <%--</div>
    </form>
</body>
</html>--%>
</asp:Content>
