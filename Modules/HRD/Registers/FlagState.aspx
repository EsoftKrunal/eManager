<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FlagState.aspx.cs" Inherits="Registers_FlagState" MasterPageFile="~/Modules/HRD/RegistersMasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <div style="text-align: center">
    <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">    
     <tr>
       <td>
        <table cellpadding="0" cellspacing="0" width="100%">
             <tr>
       <td>
           <asp:Label ID="Label1" runat="server" CssClass="textregisters" Text="Flag State"></asp:Label></td>
       </tr> 
            <tr>
            <td style="text-align: center;">
              <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; padding-top:0px; padding-bottom:10px">
                 <legend><strong>Flag State List</strong></legend>
                    <table cellpadding="0" cellspacing="0" width="100%">
                     <tr>
                      <td style=" padding-top:5px;">
                      <div style="overflow-x: hidden; overflow-y:scroll; width: 100%; height: 150px">
                       <asp:GridView ID="GridView_FlagState" runat="server" AutoGenerateColumns="False" GridLines="Horizontal" Width="98%" DataKeyNames="FlagStateId" OnSelectedIndexChanged="GridView_FlagState_SelectedIndexChanged" OnRowEditing="GridView_FlagState_Row_Editing" OnRowDeleting="GridView_FlagState_Row_Deleting" OnPreRender="GridView_FlagState_PreRender" OnDataBound="GridView_FlagState_DataBound" OnRowCommand="GridView_FlagState_RowCommand" >
                         <Columns>
                              <asp:CommandField ButtonType="Image" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True" HeaderText="View" >
                               <ItemStyle Width="20px" /></asp:CommandField>
                             <%-- <asp:CommandField ButtonType="Image" ShowEditButton="True" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit" >
                               <ItemStyle Width="20px" /></asp:CommandField>--%>
                             <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle Width="40px" />
                                    <ItemTemplate>
                                    <asp:ImageButton ID="btnEditFlagState" CausesValidation="false" OnClick="btnEditFlagState_click"
                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                    CommandArgument='<%#Eval("FlagStateId")%>' />
                                    <asp:HiddenField ID="hdnFlagStateId" runat="server" Value='<%#Eval("FlagStateId")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="Delete" ShowHeader="False"><ItemStyle Width="20px" />
                                <ItemTemplate>
                                 <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" Text="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />
                                </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Flag State"><ItemStyle HorizontalAlign="Left" Width="200px" />
                                  <ItemTemplate>  
                                   <asp:Label ID="lblflagstate" runat="server" Text='<%#Eval("FlagStateName")%>'></asp:Label>
                                   <asp:HiddenField ID="HiddenFlagStateId" runat="server" Value='<%#Eval("FlagStateId")%>' />
                                    <asp:HiddenField ID="HiddenFlagStateName" runat="server" Value='<%#Eval("FlagStateName")%>' />
                                  </ItemTemplate>
                               </asp:TemplateField>
                               <asp:BoundField DataField="Flag_Code" HeaderText="Flag Code">
                                 <ItemStyle HorizontalAlign="Left" Width="80px" />
                               </asp:BoundField>
                                <asp:BoundField DataField="StatusName" HeaderText="Status">
                                 <ItemStyle HorizontalAlign="Left" Width="80px" />
                               </asp:BoundField>
                             </Columns>
                            <RowStyle CssClass="rowstyle" />
                            <SelectedRowStyle CssClass="selectedtowstyle" />
                            <PagerStyle CssClass="pagerstyle" />
                            <HeaderStyle CssClass="headerstylefixedheadergrid" />
                     </asp:GridView>
                     </div>
                        <asp:Label ID="lbl_GridView_FlagState" runat="server" Text=""></asp:Label>
                  </td>
                 </tr>
                </table>        
                <asp:Label ID="lbl_FlagState_Message" runat="server" ForeColor="#C00000"></asp:Label></fieldset>
                &nbsp;<br />
            <asp:Panel ID="pnl_FlagState" runat="server" Width="100%" Visible="False">
              <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="">
                <legend><strong>Flag State Details</strong></legend>
                  <table border="0" cellpadding="0" cellspacing="0" style="height: 100px; text-align: center" width="100%">
                    <tr>
                      <td colspan="4">
                          <asp:HiddenField ID="HiddenFlagState" runat="server" />
                      </td>
                                                            </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right; height: 13px;">
                          </td>
                          <td style="text-align: left; height: 13px;">
                              &nbsp;
                          </td>
                          <td align="right" style="text-align: right; height: 13px;">
                          </td>
                          <td style="text-align: left; height: 13px;">
                          </td>
                      </tr>
                                                            <tr>
                                                                <td align="right" style="text-align: right; padding-right:15px">
                                                                    Flag State:</td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtFlagStateName" runat="server" CssClass="required_box" Width="200px" MaxLength="29" TabIndex="1"></asp:TextBox></td>
                                                                <td align="right" style="text-align: right; padding-right: 15px;">
                                                                    Flag Code :</td>
                                                                <td style="text-align: left; padding-right: 15px;">
                                                                    <asp:TextBox ID="txtFlagCode" runat="server" CssClass="input_box" MaxLength="9" 
                                                                        Width="200px"></asp:TextBox>
                                                                    </td>
                                                            </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                              &nbsp;
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFlagStateName"
                                  ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                          <td align="right" style="text-align: right">
                          </td>
                          <td style="text-align: left">
                          </td>
                      </tr>
                                                            <tr>
                                                                <td align="right" style="text-align: right; padding-right:15px; height: 19px;">
                                                                    Created By:</td>
                                                                <td style="text-align: left; height: 19px;">
                                                                    <asp:TextBox ID="txtCreatedBy_FlagState" runat="server" CssClass="input_box" ReadOnly="True" Width="200px"></asp:TextBox></td>
                                                                <td align="right" style="text-align: right; padding-right:15px; height: 19px;">
                                                                    Created On:</td>
                                                                <td style="text-align: left; height: 19px;">
                                                                    <asp:TextBox ID="txtCreatedOn_FlagState" runat="server" CssClass="input_box" ReadOnly="True" Width="200px"></asp:TextBox></td>
                                                            </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                              &nbsp;
                          </td>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                          </td>
                      </tr>
                                                            <tr>
                                                                <td align="right" style="text-align: right; padding-right:15px">
                                                                    Modified By:</td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtModifiedBy_FlagState" runat="server" CssClass="input_box" ReadOnly="True" Width="200px"></asp:TextBox></td>
                                                                <td align="right" style="text-align: right; padding-right:15px">
                                                                    Modified On:</td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtModifiedOn_FlagState" runat="server" CssClass="input_box" ReadOnly="True" Width="200px"></asp:TextBox></td>
                                                            </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                              &nbsp;
                          </td>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px">
                              Status:</td>
                          <td style="text-align: left">
                              <asp:DropDownList ID="ddlStatus_FlagState" runat="server" CssClass="input_box" Width="205px" TabIndex="2">
                              </asp:DropDownList></td>
                          <td align="right" style="text-align: right">
                          </td>
                          <td style="text-align: left">
                          </td>
                      </tr>
                                                        </table>
                  <br />
                  &nbsp;
                                                    </fieldset>
                <br />
                                                    </asp:Panel>
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: right">
                       <asp:Button ID="btn_Add_FlagState" runat="server" CssClass="btn" 
                    Text="Add" Width="59px" OnClick="btn_Add_FlagState_Click" CausesValidation="False" TabIndex="3" />
                            <asp:Button ID="btn_Save_FlagState" runat="server" CssClass="btn" 
                    Text="Save" Width="59px" OnClick="btn_Save_FlagState_Click" TabIndex="4" />
                            <asp:Button ID="btn_Cancel_FlagState" runat="server" CssClass="btn" 
                    Text="Cancel" Width="59px" OnClick="btn_Cancel_FlagState_Click" CausesValidation="False" TabIndex="5" />
                            <asp:Button ID="btn_Print_FlagState" runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_Print_FlagState_Click" TabIndex="6" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_pnl_FlagState');" Visible="False" />
                        </td>
                    </tr>
                </table>
                
                                                    
          </td>
         </tr>
        </table>
       </td>
      </tr>
     </table>    
    </div>
    </asp:Content>
