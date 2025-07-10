<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/HRD/RegistersMasterPage.master" AutoEventWireup="true" CodeFile="PrejoiningDocumentList.aspx.cs" Inherits="Modules_HRD_Registers_PrejoiningDocumentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <table  align="center" width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
       <td>
           <asp:Label ID="Label1" runat="server" CssClass="textregisters" Text="Manning Agent"></asp:Label></td>
    </tr>  
        <tr>
            <td>
                <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                    border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; padding-top:0px; padding-bottom:10px">
                    <legend><strong>Manning Agent List</strong></legend>
                    <table width="100%"><tr><td>
                    <div style="overflow-x: hidden; overflow-y:scroll; width: 100%; height: 150px">
                        <asp:GridView ID="GvPrejoningDocs" runat="server" GridLines="Horizontal"  AutoGenerateColumns="False" OnDataBound="GvPrejoningDocs_DataBound"
                            OnPreRender="GvPrejoningDocs_PreRender" OnRowDeleting="GvPrejoningDocs_Row_Deleting" 
                            OnSelectedIndexChanged="GvPrejoningDocs_SelectIndexChanged" Style="text-align: center" Width="98%" OnRowCancelingEdit="GvPrejoningDocs_RowCancelingEdit" OnRowCommand="GvPrejoningDocs_RowCommand">
                             <RowStyle CssClass="rowstyle" />
                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                <PagerStyle CssClass="pagerstyle" />
                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                            <Columns>
                                <asp:CommandField ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif"
                                    ShowSelectButton="True">
                                    <ItemStyle Width="30px" />
                                </asp:CommandField>
                              <%--  <asp:CommandField ButtonType="Image" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit"
                                    ShowEditButton="True">
                                    <ItemStyle Width="40px" />
                                </asp:CommandField>--%>
                                <asp:TemplateField HeaderText="Edit">
                                <ItemStyle Width="30px" />
                                <ItemTemplate>
                                <asp:ImageButton ID="btnEditDocs" CausesValidation="false" OnClick="btnEditDocs_Click"
                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                    CommandArgument='<%#Eval("DocumentId")%>' />
                                <asp:HiddenField ID="hdnDocumentId" runat="server" Value='<%#Eval("DocumentId")%>' />
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                    <ItemStyle Width="30px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                            ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"
                                            Text="Delete" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Document Name">
                                    <ItemStyle HorizontalAlign="Left" Width="500px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("DocumentName")%>'></asp:Label>                                
                                        <asp:HiddenField ID="hdnDocName" runat="server" Value='<%#Eval("DocumentName")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Status" HeaderText="Status">
                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div></td></tr></table>
                    <asp:Label ID="lblPrejoingDocs" runat="server"></asp:Label>
                <asp:Label ID="lbl_PrejoingDocs_Message" runat="server" ForeColor="#C00000"></asp:Label>
                    </fieldset> </td>
        </tr>
            <tr>
                <td style="padding-top:15px">
                    <asp:Panel ID="PrejoinDocpanel" runat="server" Visible="false" Width="100%">
                         <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                    <legend><strong>Manning Agent Details</strong></legend>
                                          <table cellpadding="0" cellspacing="0" width="100%">
                                              <tr>
                                                 <td colspan="4">
                                                     <asp:HiddenField ID="hdnPrejoingDocsId" runat="server" />
                                                     &nbsp;
                                                 </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Document Name :</td>
                                                 <td align="left">
                                                     <asp:TextBox ID="txtPrejoingDocs" runat="server" CssClass="required_box" MaxLength="200" TabIndex="1"
                                                         Width="350px"></asp:TextBox></td>
                                                 <td align="right">
                                                     
                                                     </td>
                                                 <td align="left">
                                                     
                                                     </td>
                                              </tr>
                                               <tr>
                                                  <td align="right" style="padding-right: 15px; height: 13px">
                                                  </td>
                                                  <td align="left" style="height: 13px">
                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPrejoingDocs"
                                                          ErrorMessage="Required."></asp:RequiredFieldValidator> </td>
                                                  <td align="right" style="height: 13px">
                                                  </td>
                                                  <td align="left" style="height: 13px">
                                                      
                                                  </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Created By:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtcreatedby" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="240px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Created On:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtcreatedon" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="240px" ReadOnly="True"></asp:TextBox></td>
                                              </tr>
                                              <tr>
                                                  <td colspan="4">
                                                   &nbsp; &nbsp;
                                                  </td>
                                              </tr>
                                             <%-- <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Modified By:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtmodifiedby_Department" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="240px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Modified On:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtmodifiedon_Department" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="240px" ReadOnly="True"></asp:TextBox></td>
                                              </tr>
                                              <tr>
                                                  <td colspan="4">
                                                   &nbsp; &nbsp;
                                                  </td>
                                              </tr>--%>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Status:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:DropDownList ID="ddstatus" runat="server" CssClass="input_box" Width="129px" TabIndex="2">
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

