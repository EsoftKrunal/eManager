<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Modules/HRD/RegistersMasterPage.master" CodeFile="NationalityGroup.aspx.cs" Inherits="Registers_NationalityGroup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<table cellspacing="0" cellpadding="0" width="100%">
     <tr>
       <td>
           <asp:Label ID="Label1" runat="server" CssClass="textregisters" Text="Nationality Group "></asp:Label></td>
    </tr> 
        <tr>
            <td>
                <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                    border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; padding-top:0px; padding-bottom:10px">
                    <legend><strong>Nationality Group List</strong></legend>
                    <table width="100%"><tr><td>
                        <div style="overflow-x: hidden; overflow-y:scroll; width: 100%; height: 150px">  
                        <asp:GridView ID="GvNationality_Group" runat="server" AutoGenerateColumns="False" OnDataBound="GvNationality_Group_DataBound" OnPreRender="GvNationality_Group_PreRender" OnRowDeleting="GvNationality_Group_Row_Deleting" OnRowEditing="GvNationality_Group_Row_Editing" OnSelectedIndexChanged="GvNationality_Group_SelectIndexChanged" Style="text-align: center" Width="98%" GridLines="Horizontal" OnRowCommand="GvNationality_Group_RowCommand" >
                           <RowStyle CssClass="rowstyle" />
                            <SelectedRowStyle CssClass="selectedtowstyle" />
                            <PagerStyle CssClass="pagerstyle" />
                            <HeaderStyle CssClass="headerstylefixedheadergrid" />
                            <Columns>
                                <asp:CommandField ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif"
                                    ShowSelectButton="True">
                                    <ItemStyle Width="35px" />
                                </asp:CommandField>
                                <%--<asp:CommandField ButtonType="Image" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit"
                                    ShowEditButton="True">
                                    <ItemStyle Width="35px" />
                                </asp:CommandField>--%>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle Width="40px" />
                                    <ItemTemplate>
                                    <asp:ImageButton ID="btnEditNationalityGroup" CausesValidation="false" OnClick="btnEditNationalityGroup_click"
                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                    CommandArgument='<%#Eval("NationalityGroupId")%>' />
                                    <asp:HiddenField ID="hdnNationalityGroupId" runat="server" Value='<%#Eval("NationalityGroupId")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                    <ItemStyle Width="35px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                            ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"
                                            Text="Delete" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nationality Group">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblNationalityGroupname" runat="server" Text='<%#Eval("NationalityGroupName")%>'></asp:Label>
                                        <asp:HiddenField ID="HiddenNationalityGroupId" runat="server" Value='<%#Eval("NationalityGroupId")%>' />
                                        <asp:HiddenField ID="HiddenNationalityGroupName" runat="server" Value='<%#Eval("NationalityGroupName")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="CreatedBy" HeaderText="Created By">
                                    <ItemStyle HorizontalAlign="Left"  />
                                </asp:BoundField>
                                <asp:BoundField DataField="CreatedOn" HeaderText="Created On">
                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ModifiedBy" HeaderText="Modified By">
                                    <ItemStyle HorizontalAlign="Left"  />
                                </asp:BoundField>
                                <asp:BoundField DataField="ModifiedOn" HeaderText="Modified On">
                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="StatusId" HeaderText="Status">
                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div></td></tr></table>
                    <asp:Label ID="lblNationalityGroup" runat="server"></asp:Label><asp:Label ID="lbl_Nationality_Message" runat="server" ForeColor="#C00000"></asp:Label>
                    <asp:Label ID="Label2" runat="server" ForeColor="#C00000"></asp:Label></fieldset> </td>
        </tr>
            <tr>
                <td style="padding-top:15px">
                    <asp:Panel ID="Nationalitygrouppanel" runat="server" Visible="false" Width="100%">
                         <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                    <legend><strong>Nationality Details</strong></legend>
                                          <table cellpadding="0" cellspacing="0" width="100%">
                                              <tr>
                                                 <td colspan="4">
                                                     <asp:HiddenField ID="HiddenNationalityGrouppk" runat="server" />
                                                     &nbsp;
                                                 </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Nationality Group:</td>
                                                 <td align="left">
                                                     <asp:TextBox ID="txtNationalitygroupname" runat="server" CssClass="required_box" MaxLength="24" TabIndex="1"
                                                         Width="200px"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Nationality:</td>
                                                 <td align="left" rowspan="2">
                                                     <div style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid; overflow-y: scroll;
                                                         overflow-x: hidden; border-left: #8fafdb 1px solid; width: 200px; border-bottom: #8fafdb 1px solid;
                                                         height: 50px">
                                                         <%--<asp:CheckBoxList ID="chk_lst_nationalityname" runat="server" CssClass="required_box" TabIndex="10">
                                                         </asp:CheckBoxList>--%>
                                                     <asp:GridView ID="GvRelation" runat="server" ShowHeader="false" AutoGenerateColumns="False" DataKeyNames="CountryId"
                                                         GridLines="Horizontal" Style="text-align: center" Width="198px" TabIndex="2" Height="60px">
                                                         <Columns>
                                                             <asp:TemplateField HeaderText="Select">
                                                                 <ItemTemplate>
                                                                     <asp:CheckBox ID="chkNationalityName" runat="server" />
                                                                     <asp:HiddenField ID="hfdCountryId" runat="server" Value='<%#Eval("CountryId")%>' />
                                                                 </ItemTemplate>
                                                             </asp:TemplateField>
                                                             <asp:BoundField DataField="CountryName" HeaderText="Nationality Name">
                                                                 <ItemStyle HorizontalAlign="Left" Width="80%" />
                                                             </asp:BoundField>
                                                         </Columns>
                                                     </asp:GridView></div>
                                                     </td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 15px; height: 13px">
                                                  </td>
                                                  <td align="left" style="height: 13px">
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNationalitygroupname"
                                                          ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                                                  <td align="right" style="height: 13px">
                                                      </td>
                                                  <td align="left" style="height: 13px">
                                                  </td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 15px; height: 13px">
                                                  </td>
                                                  <td align="left" style="height: 13px">
                                                      &nbsp;
                                                  </td>
                                                  <td align="right" style="height: 13px">
                                                  </td>
                                                  <td align="left" rowspan="1">
                                                  </td>
                                                  <td align="left" style="height: 13px">
                                                  </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Created By:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtcreatedby_Nationality_group" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="200px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Created On:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtcreatedon_Nationality_group" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="200px" ReadOnly="True"></asp:TextBox></td>
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
                                                     <asp:TextBox ID="txtmodifiedby_Nationality_group" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="200px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Modified On:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtmodifiedon_Nationality_group" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="200px" ReadOnly="True"></asp:TextBox></td>
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
                                                     <asp:DropDownList ID="ddstatus_Nationality_group" runat="server" CssClass="input_box" Width="205px" TabIndex="3">
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
                    <asp:Button ID="btn_Nationality_group_add" runat="server" CausesValidation="False" CssClass="btn"
                        OnClick="btn_Nationality_group_add_Click" Text="Add" Width="59px" TabIndex="4" />
                    <asp:Button ID="btn_Nationality_group_save" runat="server" CssClass="btn" OnClick="btn_Nationality_group_save_Click"
                            Text="Save" Width="59px" Visible="False" TabIndex="5" />
                    <asp:Button ID="btn_Nationality_group_Cancel" runat="server"
                                CausesValidation="false" CssClass="btn" OnClick="btn_Nationality_group_Cancel_Click" Text="Cancel"
                                Width="59px" Visible="False" TabIndex="6" />
                    <asp:Button ID="btn_Print_NationalityGroup" runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_Print_NationalityGroup_Click" TabIndex="7" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_Nationalitygrouppanel');" Visible="False" />                                 
                </td>
            </tr>
            <tr>
                <td align="right" style="height: 4px">
                </td>
            </tr>
        </table>
                </asp:Content>
