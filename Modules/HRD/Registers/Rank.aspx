<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rank.aspx.cs" Inherits="Registers_Rank" MasterPageFile="~/Modules/HRD/RegistersMasterPage.master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <table  align="center" width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
       <td>
           <asp:Label ID="Label1" runat="server" CssClass="textregisters" Text="Rank"></asp:Label></td>
    </tr>  
        <tr>
            <td>
                <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                    border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; padding-top:0px; padding-bottom:10px">
                    <legend><strong>Rank List</strong></legend>
                    <div style="overflow-x:hidden;overflow-y:scroll; width: 100%; height: 150px">
                        <asp:GridView ID="GvRank" runat="server"  GridLines="Horizontal" AutoGenerateColumns="False" OnDataBound="GvRank_DataBound"
                            OnPreRender="GvRank_PreRender" OnRowDeleting="GvRank_Row_Deleting" OnRowEditing="GvRank_Row_Editing"
                            OnSelectedIndexChanged="GvRank_SelectIndexChanged" Style="text-align: center" Width="98%" OnRowCommand="GvRank_RowCommand">
                            <RowStyle CssClass="rowstyle" />
                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                <PagerStyle CssClass="pagerstyle" />
                                                <HeaderStyle  CssClass="headerstylefixedheadergrid" />
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
                                    <asp:ImageButton ID="btnEditRank" CausesValidation="false" OnClick="btnEditRank_click"
                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                    CommandArgument='<%#Eval("RankId")%>' />
                                    <asp:HiddenField ID="hdnRankId" runat="server" Value='<%#Eval("RankId")%>' />
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
                                
                                <asp:BoundField DataField="RankGroupId" HeaderText="Rank Group">
                                    <ItemStyle HorizontalAlign="Left" Width="70px" />
                                </asp:BoundField>
                                
                                <asp:TemplateField HeaderText="Rank Code" >
                                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblRankCode" runat="server" Text='<%#Eval("RankCode")%>'></asp:Label>
                                        <asp:HiddenField ID="HiddenrankId" runat="server" Value='<%#Eval("RankId")%>' />
                                         <asp:HiddenField ID="HiddenRankName" runat="server" Value='<%#Eval("RankName")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="RankLevel" HeaderText="Level" >
                                    <ItemStyle HorizontalAlign="right" Width="30px"  />
                                </asp:BoundField>
                                <asp:BoundField DataField="Smou_Code" HeaderText="Smou Code" >
                                    <ItemStyle HorizontalAlign="right" Width="60px"  />
                                </asp:BoundField>
                                <asp:BoundField DataField="Rank_Mum" HeaderText="Rank_Mum" >
                                    <ItemStyle HorizontalAlign="right" Width="50px"  />
                                </asp:BoundField>
                                <asp:BoundField DataField="RankName" HeaderText="Rank">
                                    <ItemStyle HorizontalAlign="Left" Width="140px"  />
                                </asp:BoundField>
                                <asp:BoundField DataField="OffCrew" HeaderText="Off Crew">
                                    <ItemStyle HorizontalAlign="Left" Width="70px"  />
                                </asp:BoundField>
                                <asp:BoundField DataField="OffGroup" HeaderText="Off Group">
                                    <ItemStyle HorizontalAlign="Left" Width="70px"  />
                                </asp:BoundField>
                                <asp:BoundField DataField="StatusId" HeaderText="Status">
                                    <ItemStyle HorizontalAlign="Left" Width="70px" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <asp:Label ID="lblRank" runat="server"></asp:Label><asp:Label ID="lbl_Rank_Message" runat="server" ForeColor="#C00000"></asp:Label>
                    </fieldset> </td>
        </tr>
            <tr>
                <td style="padding-top:15px">
                    <asp:Panel ID="Rankpanel" runat="server" Visible="false" Width="100%">
                         <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                    <legend><strong>Rank Details</strong></legend>
                                          <table cellpadding="0" cellspacing="0" width="100%">
                                              <tr>
                                                 <td colspan="6">
                                                     <asp:HiddenField ID="HiddenRankpk" runat="server" />
                                                     &nbsp;
                                                 </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Rank Group:</td>
                                                 <td align="left">
                                                     <asp:DropDownList ID="ddrankgroupid" runat="server" CssClass="required_box" Width="129px" TabIndex="1">
                                                 </asp:DropDownList></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Rank Code:</td>
                                                 <td align="left">
                                                     <asp:TextBox ID="txtrankcode" runat="server" CssClass="input_box" MaxLength="9" TabIndex="2"
                                                         Width="124px"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Rank:</td>
                                                 <td align="left">
                                                     <asp:TextBox ID="txtrankname" runat="server" CssClass="required_box" MaxLength="49" TabIndex="3"
                                                         Width="124px"></asp:TextBox></td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 15px; height: 13px">
                                                  </td>
                                                  <td align="left" style="height: 13px">
                                                      <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="ddrankgroupid"
                                                          ErrorMessage="Required." MaximumValue="1000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                                                  <td align="right" style="padding-right: 15px; height: 13px">
                                                  </td>
                                                  <td align="left" style="height: 13px">
                                                  </td>
                                                  <td align="right" style="padding-right: 15px; height: 13px">
                                                  </td>
                                                  <td align="left" style="height: 13px">
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtrankname"
                                                          ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Off Crew:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:DropDownList ID="ddOffCrew_rank" runat="server" CssClass="input_box" Width="129px" TabIndex="4">
                                                     </asp:DropDownList></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Off Group:</td>
                                                 <td align="left" style="height: 20px"><asp:DropDownList ID="ddOffGroup_rank" runat="server" CssClass="input_box" Width="129px" TabIndex="5">
                                                 </asp:DropDownList></td>
                                                 <td align="right" style="height: 20px; padding-right: 15px;">
                                                     Rank Level:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txt_RankLevel" runat="server" CssClass="required_box" MaxLength="4" TabIndex="6"></asp:TextBox></td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 15px; height: 19px">
                                                      &nbsp;</td>
                                                  <td align="left" style="height: 20px">
                                                      &nbsp;</td>
                                                  <td align="right" style="padding-right: 15px; height: 19px">
                                                      &nbsp;</td>
                                                  <td align="left" style="height: 20px">
                                                      &nbsp;</td>
                                                  <td align="right" style="padding-right: 15px; height: 20px">
                                                      &nbsp;</td>
                                                  <td align="left" style="height: 20px">
                                                      &nbsp;</td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Sire Rank Type:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:DropDownList ID="ddOffCrew_SireRankType" AutoPostBack="true" 
                                                         runat="server" CssClass="input_box" TabIndex="4" Width="129px" 
                                                         onselectedindexchanged="ddOffCrew_SireRankType_SelectedIndexChanged">
                                                     <asp:ListItem Text="< Select >" Value="" Selected="True" ></asp:ListItem>
                                                     <asp:ListItem Text="Officer" Value="Officer"></asp:ListItem>
                                                     <asp:ListItem Text="Engineer" Value="Engineer"></asp:ListItem>
                                                     </asp:DropDownList>
                                                  </td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Sire Rank:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:DropDownList ID="ddOffCrew_SireRank" runat="server" CssClass="input_box" 
                                                         TabIndex="4" Width="129px">
                                                     </asp:DropDownList>
                                                  </td>
                                                 <td align="right" style="height: 20px; padding-right: 15px;">
                                                     &nbsp;</td>
                                                 <td align="left" style="height: 20px">
                                                     &nbsp;</td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 15px; height: 19px">
                                                  </td>
                                                  <td align="left" style="height: 20px">
                                                      &nbsp;</td>
                                                  <td align="right" style="padding-right: 15px; height: 19px">
                                                  </td>
                                                  <td align="left" style="height: 20px">
                                                  </td>
                                                  <td align="right" style="padding-right: 15px; height: 20px">
                                                  </td>
                                                  <td align="left">
                                                      <asp:CompareValidator ID="CompareValidator2" runat="server" 
                                                          ControlToValidate="txt_RankLevel" ErrorMessage="Greater than 0." 
                                                          Operator="GreaterThan" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                                                  </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 20px;">
                                                     Created By:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtcreatedby_rank" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="124px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 20px;">
                                                     Created On:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtcreatedon_rank" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="124px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="padding-right: 15px; height: 20px;">
                                                     Rank Mum:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtRankMum" runat="server" CssClass="input_box" MaxLength="9" 
                                                         TabIndex="2" Width="124px"></asp:TextBox>
                                                     </td>
                                              </tr>
                                              <tr>
                                                  <td colspan="6">
                                                   &nbsp; &nbsp;
                                                  </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Modified By:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtmodifiedby_rank" runat="server" CssClass="input_box" 
                                                         MaxLength="24" ReadOnly="True" Width="124px"></asp:TextBox>
                                                  </td>
                                                 <td align="right" style="height: 19px; padding-right: 15px;">
                                                     Modified On:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtmodifiedon_rank" runat="server" CssClass="input_box" 
                                                         MaxLength="24" ReadOnly="True" Width="124px"></asp:TextBox>
                                                     </td>
                                                 <td align="right" style="padding-right: 15px;">
                                                     SMOU Code:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtSMOUCode" runat="server" CssClass="input_box" MaxLength="9" 
                                                         TabIndex="2" Width="124px"></asp:TextBox>
                                                     </td>
                                              </tr>
                                              <tr>
                                                  <td colspan="6">
                                                   &nbsp; &nbsp;
                                                  </td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right:15px; height: 19px;">
                                                      Status:</td>
                                                  <td align="left" style="height: 20px">
                                                      <asp:DropDownList ID="ddstatus_rank" runat="server" CssClass="input_box" 
                                                          TabIndex="7" Width="129px">
                                                      </asp:DropDownList>
                                                  </td>
                                                  <td align="right" style="height: 20px">
                                                  </td>
                                                  <td align="left" style="height: 20px">
                                                  </td>
                                                  <td align="right" style="height: 20px">
                                                  </td>
                                                  <td align="left" style="height: 20px">
                                                  </td>
                                              </tr>
                                              <tr>
                                                  <td colspan="6">
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
                    <asp:Button ID="btn_rank_add" runat="server" CausesValidation="False" CssClass="btn"
                        OnClick="btn_rank_add_Click" Text="Add" Width="59px" TabIndex="8" />
                    <asp:Button ID="btn_rank_save" runat="server" CssClass="btn" OnClick="btn_rank_save_Click"
                            Text="Save" Width="59px" Visible="False" TabIndex="9" />
                    <asp:Button ID="btn_rank_Cancel" runat="server"
                                CausesValidation="false" CssClass="btn" OnClick="btn_rank_Cancel_Click" Text="Cancel"
                                Width="59px" Visible="False" TabIndex="10" />
                    <asp:Button ID="btn_Print_Rank" runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_Print_Rank_Click" TabIndex="11" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_Rankpanel');" Visible="False" />                                                 
                </td>
            </tr>
             <tr>
                <td align="right" style="height: 4px">
                </td>
            </tr>
        </table>
</asp:Content>
    