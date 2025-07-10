<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RankGroup.aspx.cs" MasterPageFile="~/Modules/HRD/RegistersMasterPage.master" Inherits="Registers_RankGroup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link href="../styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
</head>
<body style="text-align: center">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>--%>
        <table  align="center" width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
       <td>
           <asp:Label ID="Label1" runat="server" CssClass="textregisters" Text="Rank Group"></asp:Label></td>
    </tr>  
        <tr>
            <td>
                <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                    border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; padding-top:0px; padding-bottom:10px">
                    <legend><strong>Rank Group List</strong></legend>
                    <table width="100%"><tr><td>
                    <div style="overflow-x:hidden;overflow-y:scroll; width: 100%; height: 150px">
                        <asp:GridView ID="GvRank_Group" runat="server" GridLines="Horizontal"  AutoGenerateColumns="False" OnDataBound="GvRank_Group_DataBound"
                            OnPreRender="GvRank_Group_PreRender" OnRowDeleting="GvRank_Group_Row_Deleting" OnRowEditing="GvRank_Group_Row_Editing"
                            OnSelectedIndexChanged="GvRank_Group_SelectIndexChanged" Style="text-align: center" Width="98%" OnRowCommand="GvRank_Group_RowCommand">
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
                                    <asp:ImageButton ID="btnEditRankGroup" CausesValidation="false" OnClick="btnEditRankGroup_click"
                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                    CommandArgument='<%#Eval("RankGroupId")%>' />
                                    <asp:HiddenField ID="hdnRankGroupId" runat="server" Value='<%#Eval("RankGroupId")%>' />
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
                                <asp:TemplateField HeaderText="Rank Group">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                    <asp:HiddenField ID="Hiddenrankgroupname" runat="server" Value='<%#Eval("RankGroupName")%>' />
                                        <asp:Label ID="lblRankGroupname" runat="server" Text='<%#Eval("RankGroupName")%>'></asp:Label>
                                        <asp:HiddenField ID="HiddenrankGroupId" runat="server" Value='<%#Eval("RankGroupId")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
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
                    <asp:Label ID="lblRank" runat="server"></asp:Label>
                <asp:Label ID="lbl_RankGroup_Message" runat="server" ForeColor="#C00000"></asp:Label></fieldset> </td>
        </tr>
            <tr>
                <td style="padding-top:15px">
                    <asp:Panel ID="Rankgrouppanel" runat="server" Visible="false" Width="100%">
                         <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                    <legend><strong>Rank Details</strong></legend>
                                          <table cellpadding="0" cellspacing="0" width="100%">
                                              <tr>
                                                 <td colspan="4">
                                                     <asp:HiddenField ID="HiddenRankGrouppk" runat="server" />
                                                     &nbsp;
                                                 </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Rank Group:</td>
                                                 <td align="left">
                                                     <asp:TextBox ID="txtrankgroupname" runat="server" CssClass="required_box" MaxLength="24" TabIndex="1"
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
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtrankgroupname"
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
                                                     <asp:TextBox ID="txtcreatedby_rank_group" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="240px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Created On:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtcreatedon_rank_group" runat="server" CssClass="input_box" MaxLength="24"
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
                                                     <asp:TextBox ID="txtmodifiedby_rank_group" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="240px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Modified On:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtmodifiedon_rank_group" runat="server" CssClass="input_box" MaxLength="24"
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
                                                     <asp:DropDownList ID="ddstatus_rank_group" runat="server" CssClass="input_box" Width="129px" TabIndex="2">
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
                    <asp:Button ID="btn_rank_group_add" runat="server" CausesValidation="False" CssClass="btn"
                        OnClick="btn_rank_group_add_Click" Text="Add" Width="59px" TabIndex="3" />
                    <asp:Button ID="btn_rank_group_save" runat="server" CssClass="btn" OnClick="btn_rank_group_save_Click"
                            Text="Save" Width="59px" Visible="False" TabIndex="4" />
                    <asp:Button ID="btn_rank_group_Cancel" runat="server"
                                CausesValidation="false" CssClass="btn" OnClick="btn_rank_group_Cancel_Click" Text="Cancel"
                                Width="59px" Visible="False" TabIndex="5" />
                    <asp:Button ID="btn_Print_RankGroup" runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_Print_RankGroup_Click" TabIndex="6" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_Rankgrouppanel');" Visible="False" />                                 
                </td>
            </tr>
            <tr>
                <td align="right" style="height: 4px">
                </td>
            </tr>
        </table>
                </asp:Content>
   <%-- </form>
</body>
</html>--%>
