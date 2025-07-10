<%@ Page Language="C#" MasterPageFile="~/Modules/HRD/CrewPlanning.master"  AutoEventWireup="true" CodeFile="MiscExpense.aspx.cs" Inherits="CrewAccounting_MiscExpense" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
        <asp:Label ID="Label1" runat="server" ForeColor="Red" ></asp:Label>
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <table cellpadding="0" cellspacing="0" style="width: 100%" border="1" >
            <tr>
                <td >
                        <table cellpadding="0" cellspacing="0" width="100%" style="padding-right: 3px">
                            <tr>
                                <td align="right" style="width: 14px; height: 11px;">
                                    &nbsp;</td>
                                <td align="right" style="width: 33px; height: 11px;">
                                    Vessel:</td>
                                <td align="left" style="height: 11px; width: 271px;">
                                    <asp:DropDownList ID="ddvessel" runat="server" CssClass="required_box" 
                                        TabIndex="1" Width="186px" AutoPostBack="True" 
                                        OnSelectedIndexChanged="ddvessel_SelectedIndexChanged">
                                        <asp:ListItem Value="0">&lt; Select &gt;</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="ddvessel"
                                        ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                                <td align="left" style="text-align: right; height: 11px; width: 71px;">
                                    Vendor:</td>
                                <td align="left" style="width: 215px; height: 11px;">
                                    <asp:DropDownList ID="ddl_MISC_Vendor" runat="server" CssClass="input_box" Width="354px">
                                    </asp:DropDownList></td>
                                <td align="right" style="text-align: right; height: 11px;">
                                    Emp#:</td>
                                <td align="left" style="width: 150px; height: 11px;">
                                    <asp:TextBox ID="txt_empno" runat="server" CssClass="input_box" MaxLength="6" 
                                        TabIndex="2" Width="50px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_empno"
                                        ErrorMessage="Required." Visible="False"></asp:RequiredFieldValidator></td>
                            </tr>
                            <tr style="height: 20px; background-color: #e2e2e2">
                                <td align="center" style="height: 8px; width: 14px;">
                                    &nbsp;</td>
                                <td align="center" colspan="2" style="height: 8px">
                                    <strong>A/C Code</strong></td>
                                <td align="center" colspan="2" style="height: 8px">
                                    <strong>Descriptions</strong></td>
                                <td align="center" colspan="2" style="height: 8px">
                                    <strong>Total Amt(USD)</strong></td>
                            </tr>
                            <tr>
                                <td align="center" style="height: 8px; width: 14px;">
                                    &nbsp;</td>
                                <td align="center" colspan="2" style="height: 8px">
                                </td>
                                <td align="center" colspan="2" style="height: 8px">
                                    &nbsp;
                                </td>
                                <td align="center" colspan="2" style="height: 8px">
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="height: 8px; width: 14px;">
                                    &nbsp;</td>
                                <td align="center" colspan="2" style="height: 8px">
                                    <asp:DropDownList ID="ddl_acccode1" runat="server" CssClass="required_box" TabIndex="3"
                                        Width="252px">
                                    </asp:DropDownList>
                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="ddl_acccode1"
                                        ErrorMessage="Required." MaximumValue="5000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                                <td align="center" colspan="2" style="height: 8px">
                                    <asp:TextBox ID="txt_desc1" runat="server" CssClass="required_box" Width="368px" MaxLength="99" TabIndex="4"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_desc1"
                                        Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                                <td align="center" colspan="2" style="height: 8px">
                                    <asp:TextBox ID="txttotamt1" runat="server" CssClass="required_box" 
                                        Width="105px" TabIndex="5" MaxLength="6"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txttotamt1"
                                        Display="Dynamic" ErrorMessage="Required."></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txttotamt1"
                                        Display="Dynamic" ErrorMessage="2 decimals" 
                                        ValidationExpression="\b\d{1,13}\.?\d{0,2}"></asp:RegularExpressionValidator>
                                    </td>
                            </tr>
                            <tr>
                                <td align="center" style="height: 8px; width: 14px;">
                                    &nbsp;</td>
                                <td align="center" colspan="2" style="height: 8px">
                                    <asp:DropDownList ID="ddl_acccode2" runat="server" CssClass="input_box" TabIndex="6"
                                        Width="252px">
                                    </asp:DropDownList></td>
                                <td align="center" colspan="2" style="height: 8px">
                                    <asp:TextBox ID="txt_desc2" runat="server" CssClass="input_box" Width="368px" MaxLength="99" TabIndex="7"></asp:TextBox></td>
                                <td align="center" colspan="2" style="height: 8px">
                                    <asp:TextBox ID="txttotamt2" runat="server" CssClass="input_box" Width="105px" 
                                        TabIndex="8" MaxLength="6"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txttotamt2"
                                        Display="Dynamic" ErrorMessage="2 decimals" 
                                        ValidationExpression="\b\d{1,13}\.?\d{0,2}"></asp:RegularExpressionValidator>
                                    </td>
                            </tr>
                            <tr>
                                <td align="center" style="height: 8px; width: 14px;">
                                    &nbsp;</td>
                                <td align="center" colspan="2" style="height: 8px">
                                    <asp:DropDownList ID="ddl_acccode3" runat="server" CssClass="input_box" TabIndex="9"
                                        Width="252px">
                                    </asp:DropDownList></td>
                                <td align="center" colspan="2" style="height: 8px">
                                    <asp:TextBox ID="txt_desc3" runat="server" CssClass="input_box" Width="368px" MaxLength="99" TabIndex="10"></asp:TextBox></td>
                                <td align="center" colspan="2" style="height: 8px">
                                    <asp:TextBox ID="txttotamt3" runat="server" CssClass="input_box" Width="105px" 
                                        TabIndex="11" MaxLength="6"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txttotamt3"
                                        Display="Dynamic" ErrorMessage="2 decimals" 
                                        ValidationExpression="\b\d{1,13}\.?\d{0,2}"></asp:RegularExpressionValidator>
                                    </td>
                            </tr>
                            <tr>
                                <td align="center" style="height: 8px; width: 14px;">
                                    &nbsp;</td>
                                <td align="center" colspan="2" style="height: 8px">
                                    <asp:DropDownList ID="ddl_acccode4" runat="server" CssClass="input_box" TabIndex="12"
                                        Width="252px">
                                    </asp:DropDownList></td>
                                <td align="center" colspan="2" style="height: 8px">
                                    <asp:TextBox ID="txt_desc4" runat="server" CssClass="input_box" Width="368px" MaxLength="99" TabIndex="13"></asp:TextBox></td>
                                <td align="center" colspan="2" style="height: 8px">
                                    <asp:TextBox ID="txttotamt4" runat="server" CssClass="input_box" Width="105px" 
                                        TabIndex="14" MaxLength="6"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txttotamt4"
                                        Display="Dynamic" ErrorMessage="2 decimal" 
                                        ValidationExpression="\b\d{1,13}\.?\d{0,2}"></asp:RegularExpressionValidator>
                                    </td>
                            </tr>
                            <tr>
                                <td align="center" style="height: 8px; width: 14px;">
                                    &nbsp;</td>
                                <td align="center" colspan="2" style="height: 8px">
                                    <asp:DropDownList ID="ddl_acccode5" runat="server" CssClass="input_box" TabIndex="15"
                                        Width="252px">
                                    </asp:DropDownList></td>
                                <td align="center" colspan="2" style="height: 8px">
                                    <asp:TextBox ID="txt_desc5" runat="server" CssClass="input_box" Width="368px" MaxLength="99" TabIndex="16"></asp:TextBox></td>
                                <td align="center" colspan="2" style="height: 8px">
                                    <asp:TextBox ID="txttotamt5" runat="server" CssClass="input_box" Width="105px" 
                                        TabIndex="17" MaxLength="6"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txttotamt5"
                                        Display="Dynamic" ErrorMessage="2 decimals" 
                                        ValidationExpression="\b\d{1,13}\.?\d{0,2}"></asp:RegularExpressionValidator>
                                    </td>
                            </tr>
                            <tr style="background-color: #e2e2e2; height: 20px;">
                                <td align="center" style="height: 8px; width: 14px;">
                                    &nbsp;</td>
                                <td align="center" colspan="2" style="height: 8px">
                                </td>
                                <td align="right" colspan="2" style="height: 8px">
                               
                                    <asp:Label ID="Label2" runat="server" Font-Bold="True">Total Amt (USD):</asp:Label></td>
                                <td align="left" colspan="2" style="height: 8px">
                                    &nbsp; &nbsp;<asp:Label ID="lbl_totamt" runat="server" Width="89px"></asp:Label></td>
                            </tr>
                            <tr>
                              <td style="text-align: left; width: 14px;">
                                  &nbsp;</td>
                              <td style="text-align: left" colspan="6">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td colspan="2" style="text-align: right">
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                    FilterType="Numbers,Custom" TargetControlID="txttotamt1" ValidChars=".">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                    FilterType="Numbers,Custom" TargetControlID="txttotamt2" ValidChars=".">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                    FilterType="Numbers,Custom" TargetControlID="txttotamt3" ValidChars=".">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                    FilterType="Numbers,Custom" TargetControlID="txttotamt4" ValidChars=".">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                    FilterType="Numbers,Custom" TargetControlID="txttotamt5" ValidChars=".">
                                </ajaxToolkit:FilteredTextBoxExtender>
                                <asp:HiddenField ID="HiddenMiscCostpk" runat="server" />
                                <asp:HiddenField ID="HiddenFieldVendorIdMISC" runat="server" />
                                <asp:HiddenField ID="HiddenFieldVesselIdMISC" runat="server" />
                                </td>
                        </tr>
                    </table>
                </td>
                            </tr>
                        </table>
                </td>
            </tr>
            <tr>
                <td align="right" style=" background-color :#e2e2e2; text-align :right " >
                    <asp:Button ID="btn_Save" CssClass="btn" runat="server" Text="Save" Width="69px" TabIndex="18" OnClick="btn_Save_Click" />&nbsp;
                    <asp:Button ID="btn_Print_Po" runat="server" CssClass="btn" OnClick="btn_Print_Po_Click" Text="Print PO" Visible="False" CausesValidation="False" />
                    <asp:Label ID="lbl_ref" runat="server" style="display :none"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="" align="center">
                           <asp:Label ID="lblCost" runat="server"></asp:Label>
                            <div style="overflow-y: scroll; overflow-x: hidden; width: 100%; height: 190px">
                                <asp:GridView ID="gvsecond" runat="server" AutoGenerateColumns="False" GridLines="Horizontal"
                                 OnSelectedIndexChanged="gvsecond_SelectIndexChanged" OnRowDeleting="gvsecond_Row_Deleting" OnRowEditing="gvsecond_Row_Editing"  OnPreRender="gvsecond_PreRender" Style="text-align: center" Width="98%" TabIndex="19" DataKeyNames="MiscCostRefNo">
                                    <Columns>
                                         <asp:CommandField ButtonType="Image"  SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" HeaderText="View" ShowSelectButton="True">
                                             <ItemStyle Width="30px" />
                                         </asp:CommandField>
                                          <asp:CommandField ButtonType="Image" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit" ShowEditButton="True">
                                    <ItemStyle Width="35px" />
                                </asp:CommandField>
                                        <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                    <ItemStyle Width="35px" />
                                    <ItemTemplate>
                                    <asp:HiddenField ID="HiddenMiscCostId" runat="server" Value='<%#Eval("MiscCostRefNo")%>' />
                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                            ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"
                                            Text="Delete" />
                                    </ItemTemplate>
                                </asp:TemplateField>                              
                                        <asp:BoundField DataField="MiscCostRefNo" HeaderText="Ref#">
                                            <ItemStyle HorizontalAlign="Right" Width="80px" />
                                            <HeaderStyle Width="40px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="VesselId" HeaderText="Vessel">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CrewNumber" HeaderText="Emp#">
                                            <ItemStyle HorizontalAlign="Right" Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Total" HeaderText="Amt (USD)">
                                            <ItemStyle HorizontalAlign="Right" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CreatedBy" HeaderText="Created By">
                                            <ItemStyle HorizontalAlign="Left" Width="80px" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Created On">                                       
                                        <ItemTemplate >
                                            <%# Convert.ToDateTime(Eval("CreatedOn").ToString()).ToString("dd-MMM-yyyy")%>
                                        </ItemTemplate> 
                                        </asp:TemplateField> 
                                        <%--<asp:BoundField DataField="CreatedOn" HeaderText="Created On">
                                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                                        </asp:BoundField>--%>
                                           </Columns>
                                    <RowStyle CssClass="rowstyle" />
                                    <SelectedRowStyle CssClass="selectedtowstyle" />
                                    <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                </asp:GridView>
                            </div>
                   </td>
            </tr>
        </table>
         </asp:Content>
