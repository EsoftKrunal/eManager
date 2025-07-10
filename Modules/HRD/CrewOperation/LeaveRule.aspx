<%@ Page Language="C#" MasterPageFile="~/Modules/HRD/CrewPlanning.master" AutoEventWireup="true" CodeFile="LeaveRule.aspx.cs" Inherits="CrewOperation_LeaveRule" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
<table cellpadding="0" cellspacing="0" width="800">
   <tr><td>
       <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
           ErrorMessage="RegularExpressionValidator" ValidationExpression="(\d{0-9,.,0,5})"></asp:RegularExpressionValidator>--%></td></tr>
<tr><td>
    <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
        padding-bottom: 5px; border-left: #8fafdb 1px solid; padding-top: 5px; border-bottom: #8fafdb 1px solid">
        <legend><strong>Leave Rule</strong></legend>
        <div style="overflow-y: scroll; overflow-x: hidden; width: 780px; height: 150px">
            <asp:GridView ID="gvleave" runat="server" AutoGenerateColumns="False" DataKeyNames="RankId"
                GridLines="Horizontal" Style="text-align: center" OnRowEditing="DoEdit" OnRowUpdating="DoUpdate" OnRowCancelingEdit="DoCancel"
                Width="750px">
                <Columns>
                <asp:CommandField HeaderText="Select" ItemStyle-HorizontalAlign="Left"  ShowEditButton="true" UpdateText="Update" EditText="Edit" CancelText="Cancel" />
                    <asp:TemplateField HeaderText="S.No">
                        <ItemStyle Width="50px" HorizontalAlign="right" />
                        <ItemTemplate>
                            <asp:Label ID="lblrankid" runat="server" Text='<%# Eval("RankId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rank Code">
                        <ItemStyle Width="100px" HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblRankCode" runat="server" Text='<%# Eval("RankCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name">
                        <ItemStyle Width="200px" HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblRankname" runat="server" Text='<%# Eval("RankName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="OffCrew">
                        <ItemStyle Width="90px" HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lbloffcrew" runat="server" Text='<%# Eval("OffCrew") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="OffGroup">
                        <ItemStyle Width="90px" HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lbloffgroup" runat="server" Text='<%# Eval("OffGroup") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Leaves">
                        <ItemStyle Width="100px" HorizontalAlign="right" />
                        <ItemTemplate>
                            <asp:Label ID="lbloffgroup" runat="server" Text='<%# Eval("LeavesAllowed") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                        <asp:TextBox ID="lbloffgroup1" MaxLength="3"  CssClass="input_box" runat="server" Text='<%# Eval("LeavesAllowed") %>'></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="lbloffgroup1"
           ErrorMessage="invalid." ValidationExpression="[0-9](\.[0,5]{1})?"></asp:RegularExpressionValidator>
                        <%--<ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="lbloffgroup1"
           FilterType="Numbers,Custom" ValidChars="0.0" />--%>
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <RowStyle CssClass="rowstyle" />
                <SelectedRowStyle CssClass="selectedtowstyle" />
                <HeaderStyle CssClass="headerstylefixedheadergrid" />
            </asp:GridView>
            &nbsp;&nbsp;
        </div>
    </fieldset>
    </td></tr></table>
</asp:Content>

