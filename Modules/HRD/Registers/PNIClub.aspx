<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Modules/HRD/RegistersMasterPage.master" CodeFile="PNIClub.aspx.cs" Inherits="Registers_PNIClub" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<table cellspacing="0" cellpadding="0" width="100%" style="text-align:center; padding-left:5px" >
<tr>
       <td>
           <asp:Label ID="Label1" runat="server" CssClass="textregisters" Text="PNI Club"></asp:Label></td>
    </tr>  
        <tr>
            <td>
                <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; padding-top:0px; padding-bottom:10px">
                    <legend><strong>PNI Club List</strong></legend>
                    <div style="overflow-x:hidden; overflow-y:scroll; width: 100%; height: 150px">
                        <asp:GridView ID="GvPNI" runat="server" AutoGenerateColumns="False" OnDataBound="GvPNI_DataBound" GridLines="Horizontal"
                            OnPreRender="GvPNI_PreRender" OnRowDeleting="GvPNI_Row_Deleting" OnRowEditing="GvPNI_Row_Editing"
                            OnSelectedIndexChanged="GvPNI_SelectIndexChanged" Style="text-align: center" Width="98%" OnRowCommand="GvPNI_RowCommand">
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
                                    <asp:ImageButton ID="btnEditPNIClub" CausesValidation="false" OnClick="btnEditPNIClub_click"
                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                    CommandArgument='<%#Eval("PNIClubId")%>' />
                                    <asp:HiddenField ID="hdnPNIClubId" runat="server" Value='<%#Eval("PNIClubId")%>' />
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
                                <asp:TemplateField HeaderText="PNI Club">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblPNIClubname" runat="server" Text='<%#Eval("PNIClubName")%>'></asp:Label>
                                        <asp:HiddenField ID="HiddenPNIClubId" runat="server" Value='<%#Eval("PNIClubId")%>' />
                                        <asp:HiddenField ID="HiddenPNIClubName" runat="server" Value='<%#Eval("PNIClubName")%>' />
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
                    </div>
                    <asp:Label ID="lblPNIClub" runat="server"></asp:Label><asp:Label ID="lbl_PNIClub_Message" runat="server" ForeColor="#C00000">Record Successfully Saved.</asp:Label>
                    </fieldset> </td>
        </tr>
            <tr>
                <td style="padding-top:15px">
                    <asp:Panel ID="PNIClubpanel" runat="server" Visible="false" Width="100%">
                         <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                    <legend><strong>PNI Club Details</strong></legend>
                                          <table cellpadding="0" cellspacing="0" width="100%">
                                              <tr>
                                                 <td colspan="4">
                                                     <asp:HiddenField ID="HiddenPNIClubpk" runat="server" />
                                                     &nbsp;
                                                 </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     PNI Club:</td>
                                                 <td align="left">
                                                     <asp:TextBox ID="txtPNIClubname" runat="server" CssClass="required_box" MaxLength="24" TabIndex="1"
                                                         Width="240px"></asp:TextBox></td>
                                                 <td align="right">
                                                     </td>
                                                 <td align="left">
                                                     </td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 15px; height: 13px">
                                                  </td>
                                                  <td align="left" style="height: 13px">
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPNIClubname"
                                                          ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                                                  <td align="right" style="height: 13px">
                                                  </td>
                                                  <td align="left" style="height: 13px">
                                                  </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Created By:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtcreatedby_PNIClub" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="240px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Created On:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtcreatedon_PNIClub" runat="server" CssClass="input_box" MaxLength="24"
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
                                                     <asp:TextBox ID="txtmodifiedby_PNIClub" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="240px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Modified On:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtmodifiedon_PNIClub" runat="server" CssClass="input_box" MaxLength="24"
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
                                                     <asp:DropDownList ID="ddstatus_PNIClub" runat="server" CssClass="input_box" Width="129px" TabIndex="2">
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
                    <asp:Button ID="btn_PNIClub_add" runat="server" CausesValidation="False" CssClass="btn"
                        OnClick="btn_PNIClub_add_Click" Text="Add" Width="59px" TabIndex="3" />
                    <asp:Button ID="btn_PNIClub_save" runat="server" CssClass="btn" OnClick="btn_PNIClub_save_Click"
                            Text="Save" Width="59px" Visible="False" TabIndex="4" />
                    <asp:Button ID="btn_PNIClub_Cancel" runat="server"
                                CausesValidation="false" CssClass="btn" OnClick="btn_PNIClub_Cancel_Click" Text="Cancel"
                                Width="59px" Visible="False" TabIndex="5" />
                    <asp:Button ID="btn_Print_PNIClub" runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_Print_PNIClub_Click" TabIndex="6" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_PNIClubpanel');" Visible="False" />                                 
                </td>
            </tr>
            <tr>
                <td align="right" style="height: 4px">
                </td>
            </tr>
        </table>
                </asp:Content>
