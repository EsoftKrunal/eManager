<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ArrierPayment.aspx.cs" Inherits="ArrierPayment" MasterPageFile="~/Modules/HRD/CrewAccounts.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;</div>--%>
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td class="textregisters" colspan="2" style="padding-right: 0px; padding-left: 0px;
                    padding-bottom: 0px; padding-top: 0px;text-align: center;">
                    <strong>AREAR PAYMENT</strong></td>
            </tr>
            <tr>
                <td colspan="2" style="padding-right: 0px; padding-left: 0px;
                    padding-bottom: 0px; padding-top: 0px; text-align: center">
                    &nbsp;<asp:Label ID="lbl_ctm_Message" runat="server" ForeColor="#C00000" >Record Successfully Saved.</asp:Label></td>
            </tr>
            <tr>
                <td style="padding-left:10px; padding-right:10px">
                    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                        padding-bottom: 10px; border-left: #8fafdb 1px solid; padding-top: 0px; border-bottom: #8fafdb 1px solid;
                        text-align: center">
                          <legend><strong> AREAR PAYMENT</strong></legend>&nbsp;&nbsp;
                        <table width="100%">
                            <tr>
                                <td style="width: 349px; text-align: right">
                                    Emp.# :</td>
                                <td style="width: 82px; text-align: left">
                                    <asp:TextBox ID="txt_EmPNo" runat="server" CssClass="required_box" MaxLength="6" Width="47px"></asp:TextBox></td>
                                <td>
                                </td>
                                <td style="text-align: left">
                                    <asp:Button ID="btn_Show" runat="server" CssClass="btn"  TabIndex="7" Text="Show" Width="65px" OnClick="btn_Show_Click" ValidationGroup="bb" /></td>
                                <td style="text-align: left">
                                    </td>
                            </tr>
                            <tr>
                                <td style="width: 349px; text-align: right; height: 15px;">
                                </td>
                                <td style="width: 82px; text-align: left; height: 15px;">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_EmPNo"
                                        ErrorMessage="Required." ValidationGroup="bb"></asp:RequiredFieldValidator></td>
                                <td style="height: 15px">
                                </td>
                                <td style="height: 15px">
                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                        FilterType="Numbers" TargetControlID="txt_EmPNo">
                                    </ajaxToolkit:FilteredTextBoxExtender>
                                </td>
                                <td style="text-align: left; height: 15px;">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5" style="text-align: left">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td style="text-align: right">
                                                Month :
                                                </td>
                                            <td>
                                                <asp:DropDownList ID="ddl_Month" runat="server" CssClass="required_box" TabIndex="1"
                                                    Width="111px">
                                                    <asp:ListItem Value="0">&lt; Select &gt;</asp:ListItem>
                                                    <asp:ListItem Value="1">Jan</asp:ListItem>
                                                    <asp:ListItem Value="2">Feb</asp:ListItem>
                                                    <asp:ListItem Value="3">Mar</asp:ListItem>
                                                    <asp:ListItem Value="4">Apr</asp:ListItem>
                                                    <asp:ListItem Value="5">May</asp:ListItem>
                                                    <asp:ListItem Value="6">Jun</asp:ListItem>
                                                    <asp:ListItem Value="7">Jul</asp:ListItem>
                                                    <asp:ListItem Value="8">Aug</asp:ListItem>
                                                    <asp:ListItem Value="9">Sep</asp:ListItem>
                                                    <asp:ListItem Value="10">Oct</asp:ListItem>
                                                    <asp:ListItem Value="11">Nov</asp:ListItem>
                                                    <asp:ListItem Value="12">Dec</asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td style="text-align: right">
                                                Year :</td>
                                            <td>
                                                <asp:DropDownList ID="ddl_Year" runat="server" CssClass="required_box" TabIndex="1"
                                                    Width="111px">
                                                     </asp:DropDownList></td>
                                            <td>
                                                <asp:Button ID="btnSave" runat="server" CssClass="btn" TabIndex="7" Text="Paid" Width="65px" OnClick="btnSave_Click" ValidationGroup="aa" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddl_Month"
                                                    ErrorMessage="Required." Operator="NotEqual" Type="Integer" ValidationGroup="aa"
                                                    ValueToCompare="0"></asp:CompareValidator></td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddl_Year"
                                                    ErrorMessage="Required." Operator="NotEqual" Type="Integer" ValidationGroup="aa"
                                                    ValueToCompare="0"></asp:CompareValidator></td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5" style="text-align: center">
                                    <asp:GridView ID="gv_Main" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
                                        Style="text-align: center" Width="30%" OnRowDeleting="gv_Main_RowDeleting">
                                        <Columns>
                                        <asp:TemplateField HeaderText="Delete">
                                         <ItemTemplate>
                                         <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" Text="Delete" />
                                         </ItemTemplate>
                                         <ItemStyle Width="40px"  />
                                         </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Month">
                                        <ItemTemplate>
                                        <asp:Label ID="aa" Text='<%# Eval("Month") %>' runat="server"></asp:Label>
                                        <asp:HiddenField ID="hfd_Id" Value='<%# Eval("ARREARPAYMENTID") %>' runat="server"></asp:HiddenField>
                                        </ItemTemplate>
                                         <ItemStyle  HorizontalAlign="left"/>
                                        </asp:TemplateField>
                                            <asp:BoundField DataField="Year" HeaderText="Year">
                                                <ItemStyle HorizontalAlign="Left" Width="40px"  />
                                            </asp:BoundField>
                                        </Columns>
                                        <RowStyle CssClass="rowstyleAccounts" />
                                        <SelectedRowStyle CssClass="selectedtowstyle" />
                                        <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; padding-right: 10px; padding-top: 10px; padding-bottom: 10px;">
                    &nbsp;</td>
            </tr>
        </table>
    &nbsp; &nbsp;
            </asp:Content>
   <%-- </form>
</body>
</html>--%>
