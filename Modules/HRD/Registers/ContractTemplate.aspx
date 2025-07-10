<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/HRD/RegistersMasterPage.master" AutoEventWireup="true" CodeFile="ContractTemplate.aspx.cs" Inherits="Modules_HRD_Registers_ContractTemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <table  align="center" width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
       <td>
           <asp:Label ID="Label1" runat="server" CssClass="textregisters" Text="Contract Template"></asp:Label></td>
    </tr>  
        <tr>
            <td>
                <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                    border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; padding-top:0px; padding-bottom:10px">
                    <legend><strong>Contract Template List</strong></legend>
                    <table width="100%"><tr><td>
                    <div style="overflow-x: hidden; overflow-y:scroll; width: 100%; height: 150px">
                        <asp:GridView ID="GvContractTemplate" runat="server" GridLines="Horizontal"  AutoGenerateColumns="False" OnDataBound="GvContractTemplate_DataBound"
                            OnPreRender="GvContractTemplate_PreRender" OnRowDeleting="GvContractTemplate_Row_Deleting" 
                            OnSelectedIndexChanged="GvContractTemplate_SelectIndexChanged" Style="text-align: center" Width="98%" OnRowCancelingEdit="GvContractTemplate_RowCancelingEdit" OnRowCommand="GvContractTemplate_RowCommand">
                             <RowStyle CssClass="rowstyle" />
                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                <PagerStyle CssClass="pagerstyle" />
                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                            <Columns>
                                <asp:CommandField ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif"
                                    ShowSelectButton="True">
                                    <ItemStyle Width="35px" />
                                </asp:CommandField>
                              <%--  <asp:CommandField ButtonType="Image" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit"
                                    ShowEditButton="True">
                                    <ItemStyle Width="40px" />
                                </asp:CommandField>--%>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle Width="40px" />
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="btnEditContractTemplate" CausesValidation="false" OnClick="btnEditContractTemplate_Click"
                                                                                ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                                                                CommandArgument='<%#Eval("CCT_Id")%>' />
                                                                            <asp:HiddenField ID="HiddenCCT_Id" runat="server" Value='<%#Eval("CCT_Id")%>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                    <ItemStyle Width="40px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                            ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"
                                            Text="Delete" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contract Template">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCCT_Name" runat="server" Text='<%#Eval("CCT_Name")%>'></asp:Label>
                                       
                                        <asp:HiddenField ID="HiddenCCT_Name" runat="server" Value='<%#Eval("CCT_Name")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="StatusId" HeaderText="Status">
                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div></td></tr></table>
                    <asp:Label ID="lblContractTemplate" runat="server"></asp:Label>
                    <asp:Label ID="lbl_ContractTemplate_Message" runat="server" ForeColor="#C00000"></asp:Label>
                    </fieldset> </td>
        </tr>
            <tr>
                <td style="padding-top:15px">
                    <asp:Panel ID="ContractTemplatepanel" runat="server" Visible="false" Width="100%">
                         <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                    <legend><strong>Contract Template Details</strong></legend>
                                          <table cellpadding="0" cellspacing="0" width="100%">
                                              <tr>
                                                 <td colspan="4">
                                                     <asp:HiddenField ID="HiddenContractTemplatepk" runat="server" />
                                                     &nbsp;
                                                 </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Contract Template:</td>
                                                 <td align="left">
                                                     <asp:TextBox ID="txtContractTemplate" runat="server" CssClass="required_box" MaxLength="200" TabIndex="1"
                                                         Width="240px"></asp:TextBox></td>
                                                 <td align="right">
                                                     Template URL:
                                                     </td>
                                                 <td align="left">
                                                      <asp:TextBox ID="txtTemplateURL" runat="server" CssClass="required_box" MaxLength="500" TabIndex="1"
                                                         Width="240px"></asp:TextBox>
                                                     </td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 15px; height: 13px">
                                                  </td>
                                                  <td align="left" style="height: 13px">
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtContractTemplate"
                                                          ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                                                  <td align="right" style="height: 13px">
                                                  </td>
                                                  <td align="left" style="height: 13px">
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTemplateURL"
                                                          ErrorMessage="Required."></asp:RequiredFieldValidator>
                                                  </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Created By:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtcreatedby_ContractTemplate" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="240px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Created On:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtcreatedon_ContractTemplate" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="240px" ReadOnly="True"></asp:TextBox></td>
                                              </tr>
                                              <tr>
                                                  <td colspan="4">
                                                   &nbsp; &nbsp;
                                                  </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Modified By:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtmodifiedby_ContractTemplate" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="240px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Modified On:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtmodifiedon_ContractTemplate" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="240px" ReadOnly="True"></asp:TextBox></td>
                                              </tr>
                                              <tr>
                                                  <td colspan="4">
                                                   &nbsp; &nbsp;
                                                  </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Status:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:DropDownList ID="ddstatus_ContractTemplate" runat="server" CssClass="input_box" Width="129px" TabIndex="2">
                                                     </asp:DropDownList></td>
                                                 <td align="right" style="height: 20px">
                                                     </td>
                                                 <td align="left" style="height: 20px">
                                                     </td>
                                              </tr>
                                              <tr>
                                                  <td colspan="4">
                                                   &nbsp; &nbsp;
                                                  </td>
                                              </tr>
                                          </table>
                         </fieldset>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="right">
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" style="height: 18px">
                    <asp:Button ID="btn_add" runat="server" CausesValidation="False" CssClass="btn"
                        OnClick="btn_add_Click" Text="Add" Width="59px" TabIndex="3" />
                    <asp:Button ID="btn_save" runat="server" CssClass="btn" OnClick="btn_save_Click"
                            Text="Save" Width="59px" Visible="False" TabIndex="4" />
                    <asp:Button ID="btn_Cancel" runat="server"
                                CausesValidation="false" CssClass="btn"  Text="Cancel"
                                Width="59px" Visible="False" TabIndex="5" OnClick="btn_Cancel_Click" />
                    <asp:Button ID="btn_Print" runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_Print_Click" TabIndex="6" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_Departmentpanel');" Visible="False" />                                 
                </td>
            </tr>
            <tr>
                <td align="right" style="height: 4px">
                </td>
            </tr>
        </table>
</asp:Content>

