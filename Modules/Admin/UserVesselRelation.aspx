<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserVesselRelation.aspx.cs" Inherits="SiteAdministration_UserVesselRelation" MasterPageFile="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


    <title>EMANAGER</title>
     <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7"> 
   <%-- <link href="../styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />--%>
    <link rel="stylesheet" type="text/css" href="../HRD/Styles/StyleSheet.css" />
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
    <table cellspacing="0" cellpadding="0" style=" width:100%; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center">
        <tr>
        <td align="center" style=" height: 23px;" class="text headerband" colspan="3" >
            User Vessel Access&nbsp;
                </td>
         </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblupdation" runat="server" Font-Bold="False" ForeColor="Red" Text="Record Updated.."
                    Visible="False" Height="11px"></asp:Label>&nbsp;
            </td>
        </tr>
        
        <tr>
            <td align="right" style="text-align: center;width:150px;">
                User Name: &nbsp;&nbsp;
                
            </td>
            <td align="left" style="text-align: center;width:200px;">
                <asp:DropDownList ID="dd_usernames" runat="server" CssClass="required_box" TabIndex="1"
                    Width="180px" AutoPostBack="True" OnSelectedIndexChanged="dd_usernames_SelectedIndexChanged">
                </asp:DropDownList>
                &nbsp;&nbsp;
            </td>
            <td align="left">
                 <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="dd_usernames"
                    ErrorMessage="Required." MaximumValue="1000" MinimumValue="1" Type="Integer"></asp:RangeValidator>
            </td>
        </tr>
        
        <tr>
            <td colspan="3">
             <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                    border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; padding-top:0px; padding-bottom:10px; width: 100%;">
                    <legend><strong>User Vessel Details</strong></legend>
            <div style="overflow-x: hidden; overflow-y: scroll; height: 300px; width:100%">
                <asp:GridView ID="GVVesselUsers" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
                    Style="text-align: center" Width="98%">
                    <RowStyle CssClass="rowstyle" />
                    <SelectedRowStyle CssClass="selectedtowstyle" />
                    <PagerStyle CssClass="pagerstyle" />
                    <HeaderStyle CssClass="headerstylefixedheadergrid" />
                    <Columns>
                        <asp:TemplateField HeaderText="Select">
                            <ItemStyle HorizontalAlign="Center" Width="35px" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Vessel">
                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                            <ItemTemplate>
                                <asp:Label ID="Vessel" runat="server" Text='<%#Eval("VesselCode")%>'></asp:Label>
                                <asp:HiddenField ID="HiddenVesselId" runat="server" Value='<%#Eval("Vesselid")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                      <%--<asp:TemplateField HeaderText="User" Visible="false">
                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                            <ItemTemplate>
                                <asp:Label ID="User" runat="server" Text='<%#Eval("UserName")%>'></asp:Label>
                                <asp:HiddenField ID="HiddenLogin1Id" runat="server" Value='<%#Eval("LoginId")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
                </asp:GridView>
                </div>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="3">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="right" colspan="3">
                <asp:Button ID="btn_Authority_save" runat="server" CssClass="btn" OnClick="btn_Authority_save_Click"
                    Text="Save" Visible="true" Width="59px" />
                <asp:Button ID="btn_Authority_Reset"
                        runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_Authority_Reset_Click"
                        TabIndex="22" Text="Reset" Visible="true" Width="59px" />&nbsp;</td>
        </tr>
        <tr>
            <td align="right" colspan="3">
                &nbsp;
            </td>
        </tr>
   </table>  
   </asp:Content>
