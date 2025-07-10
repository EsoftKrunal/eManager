<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DangerousCargoEndorsement.aspx.cs" Inherits="Registers_DangerousCargoEndorsement" MasterPageFile="~/Modules/HRD/RegistersMasterPage.master"  %>
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
        <table cellspacing="0" cellpadding="0" width="100%">
            <tr>
       <td>
           <asp:Label ID="Label1" runat="server" CssClass="textregisters" Text="Dangerous Cargo Endorsement "></asp:Label></td>
    </tr> 
        <tr>
            <td>
                <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; padding-bottom:10px;">
                    <legend><strong>Dangerous Cargo Endorsement List</strong></legend>
                    
                   <table width="100%"><tr><td>
                   <div style="overflow-x: hidden; overflow-y:scroll; width: 100%; height: 150px">
                        <asp:GridView ID="GvDCE"   GridLines="Horizontal" runat="server" AutoGenerateColumns="False" OnDataBound="GvDCE_DataBound"
                            OnPreRender="GvDCE_PreRender" OnRowDeleting="GvDCE_Row_Deleting" OnRowEditing="GvDCE_Row_Editing"
                            OnSelectedIndexChanged="GvDCE_SelectIndexChanged" Style="text-align: center" Width="98%" OnRowCommand="GvDCE_RowCommand">
                            <RowStyle CssClass="rowstyle" />
                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                <PagerStyle CssClass="pagerstyle" />
                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                            <Columns>
                                <asp:CommandField ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif"
                                    ShowSelectButton="True">
                                    <ItemStyle Width="30px" />
                                </asp:CommandField>
                                <%--<asp:CommandField ButtonType="Image" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit"
                                    ShowEditButton="True">
                                    <ItemStyle Width="35px" />
                                </asp:CommandField>--%>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle Width="40px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEditCargo" CausesValidation="false" OnClick="btnEditCargo_click"
                                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                                    CommandArgument='<%#Eval("CargoId")%>' />
                                                <asp:HiddenField ID="hdnCargoId" runat="server" Value='<%#Eval("CargoId")%>' />
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
                                <asp:TemplateField HeaderText="Endorsement">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcargoname" runat="server" Text='<%#Eval("CargoName")%>'></asp:Label>
                                        <asp:HiddenField ID="HiddenCargoId" runat="server" Value='<%#Eval("CargoId")%>' />
                                        <asp:HiddenField ID="HiddenCargoName" runat="server" Value='<%#Eval("CargoName")%>' />
                                    </ItemTemplate>
                                    <ItemStyle  HorizontalAlign="Left" />
                                </asp:TemplateField>
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
                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView></div></td></tr></table>
                    
                    <asp:Label ID="lblCargo" runat="server"></asp:Label>
                <asp:Label ID="lbl_DCE_Message" runat="server" ForeColor="#C00000"></asp:Label></fieldset> </td>
        </tr>
            <tr>
                <td style="padding-top:15px">
                    <asp:Panel ID="Cargopanel" runat="server" Visible="false" Width="100%">
                         <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                    <legend><strong>Dangerous Cargo Endorsement Details</strong></legend>
                                          <table cellpadding="0" cellspacing="0" width="100%">
                                              <tr>
                                                 <td colspan="4">
                                                     <asp:HiddenField ID="HiddenCargopk" runat="server" />
                                                     &nbsp;
                                                 </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Endorsement:</td>
                                                 <td align="left">
                                                     <asp:TextBox ID="txtCargoname" runat="server" CssClass="required_box" MaxLength="49"
                                                         Width="200px" TabIndex="1"></asp:TextBox></td>
                                                 <td align="right">
                                                     </td>
                                                 <td align="left">
                                                     </td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 15px; height: 14px">
                                                  </td>
                                                  <td align="left" style="height: 14px">
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCargoname"
                                                          ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                                                  <td align="right" style="height: 14px">
                                                  </td>
                                                  <td align="left" style="height: 14px">
                                                  </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Created By:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtcreatedby_Cargo" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="200px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Created On:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtcreatedon_Cargo" runat="server" CssClass="input_box" MaxLength="24"
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
                                                     <asp:TextBox ID="txtmodifiedby_Cargo" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="200px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Modified On:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtmodifiedon_Cargo" runat="server" CssClass="input_box" MaxLength="24"
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
                                                     <asp:DropDownList ID="ddstatus_Cargo" runat="server" CssClass="input_box" Width="205px" TabIndex="2">
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
                <td align="right" style="height: 13px">
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td align="right" style="height: 18px">
                    <asp:Button ID="btn_Cargo_add" runat="server" CausesValidation="False" CssClass="btn"
                        OnClick="btn_Cargo_add_Click" Text="Add" Width="59px" TabIndex="3" />
                    <asp:Button ID="btn_Cargo_save" runat="server" CssClass="btn" OnClick="btn_Cargo_save_Click"
                            Text="Save" Width="59px" Visible="False" TabIndex="4" />
                    <asp:Button ID="btn_Cargo_Cancel" runat="server"
                                CausesValidation="false" CssClass="btn" OnClick="btn_Cargo_Cancel_Click" Text="Cancel"
                                Width="59px" Visible="False" TabIndex="5" />
                    <asp:Button ID="btn_Print_Cargo" runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_Print_Cargo_Click" TabIndex="6" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_Cargopanel');" Visible="False" />                                                                  
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
