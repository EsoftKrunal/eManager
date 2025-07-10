<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/HRD/RegistersMasterPage.master" AutoEventWireup="true" CodeFile="RankResponsibility.aspx.cs" Inherits="Modules_HRD_Registers_RankResponsibility" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <table  align="center" width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr class="text headerband">
       <td>
           <asp:Label ID="Label1" runat="server"  Text="Rank Responsibility"></asp:Label></td>
    </tr>  
        <tr>
            <td>
                <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                    border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; padding-top:0px; padding-bottom:10px">
                    <legend><strong>Rank Responsibility List</strong></legend>
                    <table width="100%"><tr><td>
                    <div style="overflow-x: hidden; overflow-y:scroll; width: 100%; height: 150px">
                        <asp:GridView ID="GvRankResponsibilities" runat="server" GridLines="Horizontal"  AutoGenerateColumns="False" OnDataBound="GvRankResponsibilities_DataBound"
                            OnPreRender="GvRankResponsibilities_PreRender" OnRowDeleting="GvRankResponsibilities_Row_Deleting" OnRowEditing="GvRankResponsibilities_Row_Editing"
                            OnSelectedIndexChanged="GvRankResponsibilities_SelectIndexChanged" Style="text-align: center" Width="98%" OnRowCommand="GvRankResponsibilities_RowCommand">
                             <RowStyle CssClass="rowstyle" />
                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                <PagerStyle CssClass="pagerstyle" />
                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                            <Columns>
                                <asp:CommandField ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif"
                                    ShowSelectButton="True">
                                    <ItemStyle Width="35px" />
                                </asp:CommandField>
                               <%-- <asp:CommandField ButtonType="Image" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit"
                                    ShowEditButton="True">
                                    <ItemStyle Width="40px" />
                                </asp:CommandField>--%>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle Width="40px" />
                                    <ItemTemplate>
                                    <asp:ImageButton ID="btnEditRankResponsibilities" CausesValidation="false" OnClick="btnEditRankResponsibilities_click"
                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                    CommandArgument='<%#Eval("CRRID")%>' />
                                    <asp:HiddenField ID="hdnCRRID" runat="server" Value='<%#Eval("CRRID")%>' />
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
                                <asp:TemplateField HeaderText="Rank">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                    <%--<asp:HiddenField ID="Hiddenrankgroupname" runat="server" Value='<%#Eval("RankGroupName")%>' />--%>
                                        <asp:Label ID="lblRankname" runat="server" Text='<%#Eval("RankName")%>'></asp:Label>
                                        <asp:HiddenField ID="HiddenCRRId" runat="server" Value='<%#Eval("CRRId")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:BoundField DataField="CRRRes" HeaderText="Responsibility">
                                    <ItemStyle HorizontalAlign="Left"  />
                                </asp:BoundField>
                               <%-- <asp:BoundField DataField="CreatedBy" HeaderText="Created By">
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
                    <asp:Label ID="lblRankResponsibility" runat="server"></asp:Label>
                <asp:Label ID="lbl_RankResponsibility_Message" runat="server" ForeColor="#C00000"></asp:Label>
                    </fieldset> </td>
        </tr>
            <tr>
                <td style="padding-top:15px">
                    <asp:Panel ID="RankResponsibilitiespanel" runat="server" Visible="false" Width="100%">
                         <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                    <legend><strong>Rank Responsibility</strong></legend>
                                          <table cellpadding="0" cellspacing="0" width="100%">
                                              <tr>
                                                 <td colspan="4">
                                                     <asp:HiddenField ID="hdnCRRID" runat="server" />
                                                     &nbsp;
                                                 </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;width:20%;" >
                                                     Rank :</td>
                                                 <td align="left">
                                                     <asp:DropDownList ID="ddl_Rank_Search" runat="server" CssClass="form-control" Width="165px" TabIndex="4">
                                                                        <asp:ListItem Text="&lt; Select &gt;" ></asp:ListItem>
                                                                    </asp:DropDownList></td>
                                                 <td align="right">
                                                     
                                                     </td>
                                                 <td align="left">
                                                     
                                                     </td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right:15px; height: 19px;width:20%;">
                                                   Responsibilities :
                                                  </td>
                                                  <td colspan="4" align="left">
                                                  <asp:TextBox ID="txtResponsibilities" runat="server" TextMode="MultiLine" Width="95%" Height="125px"></asp:TextBox>
                                                  </td>
                                              </tr>
                                               <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Created By:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtcreatedby_rank_group" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="240px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Created On:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtcreatedon_rank_group" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="240px" ReadOnly="True"></asp:TextBox></td>
                                              </tr>
                                              
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Modified By:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtmodifiedby_rank_group" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="240px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Modified On:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtmodifiedon_rank_group" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="240px" ReadOnly="True"></asp:TextBox></td>
                                              </tr>
                                             
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Status:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:DropDownList ID="ddstatus_rank_group" runat="server" CssClass="input_box" Width="129px" TabIndex="2">
                                                     </asp:DropDownList></td>
                                                 <td align="right" style="height: 20px">
                                                     </td>
                                                 <td align="left" style="height: 20px">
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

