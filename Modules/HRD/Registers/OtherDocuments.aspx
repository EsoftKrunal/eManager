<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OtherDocuments.aspx.cs" Inherits="Registers_OtherDocuments" MasterPageFile="~/Modules/HRD/RegistersMasterPage.master" %>
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
        <table cellspacing="0" cellpadding="0" width="100%"  align="center">
             <tr>
       <td>
           <asp:Label ID="Label1" runat="server" CssClass="textregisters" Text="Other Documents"></asp:Label></td>
    </tr> 
        <tr>
            <td>
                <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                    border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; padding-top:0px; padding-bottom:10px ">
                    <legend><strong>Other Documents List</strong></legend>
                    <div id="div-datagrid" style=" width:100%; height :150px; overflow-y:Scroll; overflow-x:hidden; text-align:center">
                        <asp:GridView ID="GvOtherDocuments" runat="server" GridLines="Horizontal" AutoGenerateColumns="False" OnDataBound="GvOtherDocuments_DataBound"
                            OnPreRender="GvOtherDocuments_PreRender" OnRowDeleting="GvOtherDocuments_Row_Deleting" OnRowEditing="GvOtherDocuments_Row_Editing"
                            OnSelectedIndexChanged="GvOtherDocuments_SelectIndexChanged" Style="text-align: center" Width="98%" OnRowCommand="GvOtherDocuments_RowCommand">
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
                                    <ItemStyle Width="35px" />
                                </asp:CommandField>--%>
                                 <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle Width="40px" />
                                    <ItemTemplate>
                                    <asp:ImageButton ID="btnEditOtherDocument" CausesValidation="false" OnClick="btnEditOtherDocument_click"
                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                    CommandArgument='<%#Eval("OtherDocumentId")%>' />
                                    <asp:HiddenField ID="hdnOtherDocumentId" runat="server" Value='<%#Eval("OtherDocumentId")%>' />
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
                                <asp:TemplateField HeaderText="Document Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDocumenttype" runat="server" Text='<%#Eval("DocumentType")%>'></asp:Label>
                                        <asp:HiddenField ID="HiddenOtherDocumentId" runat="server" Value='<%#Eval("OtherDocumentId")%>' />
                                        <asp:HiddenField ID="HiddenOtherDocumentName" runat="server" Value='<%#Eval("DocumentName")%>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="DocumentName" HeaderText="Document">
                                    <ItemStyle HorizontalAlign="Left"  />
                                </asp:BoundField>
                                <asp:BoundField DataField="OffCrew" HeaderText="Off Crew">
                                    <ItemStyle HorizontalAlign="Left"  />
                                </asp:BoundField>
                                <asp:BoundField DataField="OffGroup" HeaderText="Off Group">
                                    <ItemStyle HorizontalAlign="Left" Width="60px"  />
                                </asp:BoundField>
                                <asp:BoundField DataField="Expires" HeaderText="Expires">
                                    <ItemStyle HorizontalAlign="Left" Width="50px"  />
                                </asp:BoundField>
                                <asp:BoundField DataField="Mandatory" HeaderText="Mandatory">
                                    <ItemStyle HorizontalAlign="Left" Width="25px"  />
                                </asp:BoundField>
                               <%-- <asp:BoundField DataField="CreatedBy" HeaderText="Created By">
                                    <ItemStyle HorizontalAlign="Left" />
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
                                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <asp:Label ID="lblOtherDocuments" runat="server"></asp:Label><asp:Label ID="lbl_OtherDocuments_Message" runat="server" ForeColor="#C00000"></asp:Label>
                    </fieldset></td>
        </tr>
            <tr>
                <td style="padding-top:15px">
                    <asp:Panel ID="OtherDocumentspanel" runat="server" Visible="false" Width="100%">
                         <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                    <legend><strong>Other Documents Details</strong></legend>
                                          <table cellpadding="0" cellspacing="0" width="100%">
                                              <tr>
                                                 <td colspan="6">
                                                     <asp:HiddenField ID="HiddenOtherDocumentspk" runat="server" />
                                                 </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Document Type:</td>
                                                 <td align="left" style="height: 19px">
                                                     <asp:TextBox ID="txtDocumentType" runat="server" CssClass="input_box" MaxLength="24"
                                                         TabIndex="1" Width="170px"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Document:</td>
                                                 <td align="left" style="height: 19px">
                                                     <asp:TextBox ID="txtDocumentName" runat="server" CssClass="required_box" MaxLength="24" TabIndex="2"
                                                         Width="170px"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Off Crew:</td>
                                                 <td align="left" style="height: 19px">
                                                     <asp:DropDownList ID="ddOffCrew_Document" runat="server" CssClass="input_box" Width="175px" TabIndex="3">
                                                     </asp:DropDownList></td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 15px; height: 13px">
                                                  </td>
                                                  <td align="left" style="height: 13px">
                                                  </td>
                                                  <td align="right" style="padding-right: 15px; height: 13px">
                                                  </td>
                                                  <td align="left" style="height: 13px">
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDocumentName"
                                                          ErrorMessage="Required." Height="14px"></asp:RequiredFieldValidator></td>
                                                  <td align="right" style="padding-right: 15px; height: 13px">
                                                  </td>
                                                  <td align="left" style="height: 13px">
                                                  </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="height: 20px; padding-right:15px;">
                                                     Off Group:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:DropDownList ID="ddOffGroup_Document" runat="server" CssClass="input_box" Width="175px" TabIndex="4">
                                                     </asp:DropDownList></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Expires:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:CheckBox ID="Chkexpires_Document" runat="server" TabIndex="5" /></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Mandatory:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:CheckBox ID="Chkmandatory_Document" runat="server" TabIndex="6" /></td>
                                              </tr>
                                              <tr>
                                                  <td colspan="6" style="height: 13px">
                                                     &nbsp; &nbsp;
                                                 </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Created By:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtcreatedby_Document" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="170px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Created On:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtcreatedon_Document" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="170px" ReadOnly="True"></asp:TextBox></td>
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
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Modified By:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtmodifiedby_Document" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="170px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Modified On:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtmodifiedon_Document" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="170px" ReadOnly="True"></asp:TextBox></td>
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
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Status:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:DropDownList ID="ddstatus_Document" runat="server" CssClass="input_box" Width="175px" TabIndex="7">
                                                     </asp:DropDownList></td>
                                                 <td align="right" style="height: 20px">
                                                     </td>
                                                 <td align="left" style="height: 20px">
                                                     </td>
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
                    <asp:Button ID="btn_other_Document_add" runat="server" CausesValidation="False" CssClass="btn"
                        OnClick="btn_other_Document_add_Click" Text="Add" Width="59px" TabIndex="8" />
                    <asp:Button ID="btn_other_Document_save" runat="server" CssClass="btn" OnClick="btn_other_Document_save_Click"
                            Text="Save" Width="59px" Visible="False" TabIndex="9" />
                    <asp:Button ID="btn_other_Document_Cancel" runat="server"
                                CausesValidation="false" CssClass="btn" OnClick="btn_other_Document_Cancel_Click" Text="Cancel"
                                Width="59px" Visible="False" TabIndex="10" />
                    <asp:Button ID="btn_Print_OtherDocuments" runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_Print_OtherDocuments_Click" TabIndex="11" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_OtherDocumentspanel');" Visible="False" />                
                </td>
            </tr>
             <tr>
                <td align="right" style="height: 4px">
                </td>
            </tr>
        </table>
        </asp:Content>
    <%--</form>
</body>
</html>--%>
