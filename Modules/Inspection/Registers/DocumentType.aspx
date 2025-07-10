<%@ Page Language="C#" MasterPageFile="~/Modules/Inspection/Registers/RegistersMasterPage.master" AutoEventWireup="true" CodeFile="DocumentType.aspx.cs" Inherits="Registers_DocumentType" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">    
     <tr>
       <td>
        <table cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="text-align: center;">
                <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 0px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="">
                <%--<legend><strong>Document Type</strong></legend>--%>
                    <asp:Panel ID="pnl_DocumentType" runat="server" Width="100%">
                        <table border="0" cellpadding="0" cellspacing="0" style="text-align: center" width="100%">
                            <tr>
                                <td colspan="2" style="height: 15px">
                                    <asp:HiddenField ID="HiddenDocumentType" runat="server" /><asp:HiddenField id="HiddenFieldGridRowCount"
                                        runat="server"></asp:HiddenField>
                                </td>
                                <td colspan="1" style="height: 15px">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="text-align: right; padding-right:15px; width: 287px;">
                                    Document Type:</td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtDocumentType" runat="server" CssClass="input_box" Width="510px" MaxLength="49" TabIndex="1" Enabled="False"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDocumentType" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                </td>
                                <td style="text-align: left">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="padding-right: 15px; text-align: right; height: 3px; width: 287px;">
                                </td>
                                <td style="text-align: left; height: 3px;">
                                </td>
                                <td style="height: 3px; text-align: left">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="padding-right: 15px; text-align: right; width: 287px;" valign="top">Description:</td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtDescription" runat="server" CssClass="input_box" MaxLength="254" TabIndex="2" Width="510px" Enabled="False" Height="60px" TextMode="MultiLine"></asp:TextBox></td>
                                <td style="padding-right: 17px; text-align: right" valign="bottom">
                                    <asp:Button ID="btn_Cancel_DocumentType" runat="server" CssClass="btn" Text="Cancel" Width="59px" CausesValidation="False" TabIndex="3" Visible="False" OnClick="btn_Cancel_DocumentType_Click" />
                                    <asp:Button ID="btn_New_DocumentType" runat="server" CssClass="btn" Text="New" Width="59px" CausesValidation="False" TabIndex="4" OnClick="btn_New_DocumentType_Click" />
                                    <asp:Button ID="btn_Save_DocumentType" runat="server" CssClass="btn" Text="Save" Width="59px" TabIndex="5" Enabled="False" OnClick="btn_Save_DocumentType_Click" /></td>
                            </tr>
                            <tr>
                                <td align="right" style="padding-right: 15px; height: 3px; text-align: right; width: 287px;">
                                </td>
                                <td style="height: 3px; text-align: left">
                                </td>
                                <td style="height: 3px; text-align: left">
                                </td>
                            </tr>
                        </table>
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td style="text-align: right; padding-right: 17px;">
                                    <div id="Div1" runat="server" style="position: relative; top: 0; text-align: center; left: 0"><asp:Label ID="lbl_DocumentType_Message" runat="server" ForeColor="#C00000"></asp:Label></div>
                                    &nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="padding-right: 5px; padding-left: 5px">
                                    <div style="width: 100%; height: 250px"> <%--height: 150px;--%>
                                        <asp:GridView ID="GridView_DocumentType" runat="server" GridLines="Both" AutoGenerateColumns="False" Width="98%" OnPreRender="GridView_DocumentType_PreRender" OnRowDataBound="GridView_DocumentType_RowDataBound" OnRowDeleting="GridView_DocumentType_RowDeleting" OnRowEditing="GridView_DocumentType_RowEditing" AllowPaging="True" PageSize="8" OnPageIndexChanging="GridView_DocumentType_PageIndexChanging" >
                                                <RowStyle CssClass="rowstyle" />
                                            <Columns>
<asp:TemplateField HeaderText="Document Type"><ItemTemplate>
                                                        <asp:Label ID="lblCode" runat="server" Text='<%# Eval("Type") %>'></asp:Label>
                                                        <asp:HiddenField ID="Hidden_DocumentTypeId" runat="server" Value='<%# Eval("Id") %>' />
                                                        <%--<asp:HiddenField ID="Hidden_InspectionCode" runat="server" Value='<%# Eval("Code") %>' />--%>
                                                    
</ItemTemplate>

<ItemStyle HorizontalAlign="Left" Width="120px"></ItemStyle>
</asp:TemplateField>
<asp:BoundField DataField="Description" HeaderText="Description">
<ItemStyle HorizontalAlign="Left"></ItemStyle>
</asp:BoundField>
<asp:CommandField EditImageUrl="~/Images/edit.jpg" ShowEditButton="True" ButtonType="Image" HeaderText="Edit">
<ItemStyle Width="40px"></ItemStyle>
</asp:CommandField>
<asp:TemplateField HeaderText="Delete" ShowHeader="False"><ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" ToolTip="Delete" />
                                                    
</ItemTemplate>

<ItemStyle Width="45px"></ItemStyle>
</asp:TemplateField>
</Columns>
                                            <pagerstyle horizontalalign="Center" />
                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                <HeaderStyle CssClass="headerstylefixedheader" />
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-right: 7px; padding-left: 16px; text-align: center">
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td style="padding-right: 8px; text-align: left">
                                    <asp:Label ID="lbl_GridView_DocumentType" runat="server" Text=""></asp:Label></td>
                                            <td style="padding-right: 8px; text-align: right;">Created By:</td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtCreatedBy_DocumentType" runat="server" CssClass="input_box" ReadOnly="True" TabIndex="-1" BackColor="Gainsboro" Width="154px"></asp:TextBox></td>
                                            <td style="padding-right: 8px; text-align: right;">Created On:</td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtCreatedOn_DocumentType" runat="server" CssClass="input_box" ReadOnly="True" TabIndex="-2" BackColor="Gainsboro" Width="72px"></asp:TextBox></td>
                                            <td style="padding-right: 8px; text-align: right;">Modified By:</td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtModifiedBy_DocumentType" runat="server" CssClass="input_box" ReadOnly="True" TabIndex="-3" BackColor="Gainsboro" Width="154px"></asp:TextBox></td>
                                            <td style="padding-right: 8px; text-align: right;">Modified On:</td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtModifiedOn_DocumentType" runat="server" CssClass="input_box" ReadOnly="True" TabIndex="-4" BackColor="Gainsboro" Width="72px"></asp:TextBox></td>
                                            <td style="text-align: left">
                                                <asp:Button id="btn_Print_DocType" runat="server" CssClass="btn" 
                                                    OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_pnl_DocumentType');"
                                                    tabIndex="6" Text="Print" Visible="False" Width="59px" CausesValidation="False" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>&nbsp;
                    </asp:Panel>
                </fieldset>                              
            </td>
          </tr>
        </table>&nbsp;
       </td>
      </tr>
     </table>
</asp:Content>

