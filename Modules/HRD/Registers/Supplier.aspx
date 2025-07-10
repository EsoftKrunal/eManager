<%@ Page Language="C#" MasterPageFile="~/Modules/HRD/RegistersMasterPage.master" AutoEventWireup="true" CodeFile="Supplier.aspx.cs" Inherits="CrewAccounting_Supplier" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
<script language="javascript">
function openpage()
{
var url = "../Registers/PopUp_Port.aspx"
window.open(url,null,'title=no,toolbars=no,scrollbars=yes,width=400,height=200,left=50,top=50,addressbar=no');
return false;
}
</script>
    <%-- <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
  <table  align="center" width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
       <td align="center">
           <asp:Label ID="Label1" runat="server" CssClass="textregisters" Text="Supplier"></asp:Label></td>
    </tr>  
    <tr>
    <td>
     <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                        padding-bottom: 10px; border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid;
                        text-align: center">
                        <legend><strong>Search</strong></legend>
                        <table cellpadding="0" cellspacing="0" style="padding-top: 3px" width="100%">
                            <tr>
                                <td style="width: 130px; height: 19px; text-align: left">
                                    &nbsp;Supplier Name :</td>
                                <td style="width: 709px; height: 19px; text-align: left">
                                    <asp:TextBox ID="txt_Supplier" runat="server" CssClass="input_box" MaxLength="30" Width="304px" onkeydown="javascript:if(event.keyCode==13){document.getElementById('ctl00$ContentPlaceHolder1$btn_Search').focus();}"></asp:TextBox>
                                </td>
                                <td style="width: 86px; height: 19px">
                                    <asp:Button ID="btn_Search" runat="server" CssClass="input_box" OnClick="btn_Search_Click"
                                        Text="Search" Width="62px" /></td>
                            </tr>
                        </table>
                    </fieldset>
    </td>
    </tr>
        <tr>
            <td>
            
                <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                    border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; padding-top:0px; padding-bottom:10px">
                    <legend><strong>Supplier List</strong></legend>
                    <asp:HiddenField ID="HiddenTravelagentpk" runat="server" />
                    <div style="overflow-x:hidden;overflow-y:scroll; width: 100%; height: 150px">
                        <asp:GridView ID="GvTravelAgent" GridLines="Horizontal" runat="server" AutoGenerateColumns="False" OnDataBound="GvTravelAgent_DataBound"
                            OnPreRender="GvTravelAgent_PreRender" OnRowDeleting="GvTravelAgent_Row_Deleting" OnRowEditing="GvTravelAgent_Row_Editing"
                            OnSelectedIndexChanged="GvTravelAgent_SelectIndexChanged" Style="text-align: center" Width="98%" OnRowCommand="GvTravelAgent_RowCommand">
                            <RowStyle CssClass="rowstyle" />
                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                <PagerStyle CssClass="pagerstyle" />
                                                <HeaderStyle  CssClass="headerstylefixedheadergrid"  />
                            <Columns>
                             
                                <asp:CommandField ButtonType="Image" HeaderText="View" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif"
                                    ShowSelectButton="True">
                                    <ItemStyle Width="25px" />
                                </asp:CommandField>
                               <%-- <asp:CommandField ButtonType="Image" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit"
                                    ShowEditButton="True">
                                    <ItemStyle Width="25px" />
                                </asp:CommandField>--%>
                                 <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle Width="40px" />
                                    <ItemTemplate>
                                    <asp:ImageButton ID="btnEditSupplier" CausesValidation="false" OnClick="btnEditSupplier_click"
                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                    CommandArgument='<%#Eval("SupplierID")%>' />
                                    <asp:HiddenField ID="hdnSupplierID" runat="server" Value='<%#Eval("SupplierID")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                    <ItemStyle Width="25px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" Text="Delete" />
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Company" HeaderText="Company">
                                    <ItemStyle HorizontalAlign="Left" Width="200px" />
                                </asp:BoundField>
                               <%-- <asp:BoundField DataField="SupplierEmail" HeaderText="Email">
                                    <ItemStyle HorizontalAlign="Left"  Width="90px"/>
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="ContactPerson" HeaderText="Contact Person">
                                    <ItemStyle HorizontalAlign="Left" Width="130px" />
                                </asp:BoundField>
                               <%-- <asp:BoundField DataField="Address" HeaderText="Address">
                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Phone" HeaderText="Phone">
                                    <ItemStyle HorizontalAlign="Right" Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Mobile" HeaderText="Mobile">
                                    <ItemStyle HorizontalAlign="Right" Width="90px" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="Vendorno" HeaderText="Vendor #">
                                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                                </asp:BoundField>
                                     <%-- <asp:BoundField DataField="FaxNo" HeaderText="Fax">
                                    <ItemStyle HorizontalAlign="Right" Width="90px" />
                                </asp:BoundField>--%>
                                
                                <asp:BoundField DataField="PortId"  HeaderText="Port">
                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                </asp:BoundField>
                                
                                 
                               <%-- <asp:BoundField DataField="CreatedBy" HeaderText="Created By">
                                    <ItemStyle HorizontalAlign="Right"  Width="80px"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="CreatedOn" HeaderText="Created On">
                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ModifiedBy" HeaderText="Modified By">
                                    <ItemStyle HorizontalAlign="Right"  Width="80px"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="ModifiedOn" HeaderText="Modified On">
                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                </asp:BoundField>--%>
                                <%--<asp:BoundField DataField="StatusId" HeaderText="Status">
                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                </asp:BoundField>--%>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                                    <ItemTemplate>
                                     <asp:Label ID="lbldd" runat="server" Text='<%#Eval("StatusId")%>'></asp:Label>
                                     <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("Company")%>' />
                                        <asp:HiddenField ID="HiddenTravelagentId" runat="server" Value='<%#Eval("SupplierID")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                  </div> 
                    <asp:Label ID="lblTravel_agent" runat="server"></asp:Label>
                <asp:Label ID="lbl_TravelAgent_Message" runat="server" ForeColor="#C00000"></asp:Label>
                    </fieldset> </td>
        </tr>
            <tr>
                <td style="padding-top:15px">
                    <asp:Panel ID="travelagentpanel" runat="server" Visible="false" Width="100%">
                         <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                    <legend><strong>Supplier Details</strong></legend>
                                          <table cellpadding="0" cellspacing="0" width="100%">
                                              <tr>
                                                 <td colspan="6">
                                                     &nbsp;&nbsp;
                                                 </td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 15px; height: 13px">
                                                  </td>
                                                  <td align="left" style="height: 13px">
                                                      </td>
                                                  <td align="right" style="padding-right: 15px; height: 13px">
                                                  </td>
                                                  <td align="left" style="height: 13px">
                                                      </td>
                                                  <td align="right" style="padding-right: 15px; height: 13px">
                                                  </td>
                                                  <td align="left" style="height: 13px">
                                                      </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="height: 20px; padding-right:15px;">
                                                     Company:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txt_travel_company" runat="server" CssClass="required_box" MaxLength="49" TabIndex="1"
                                                         Width="170px"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 20px;">
                                                     Email:</td>
                                                 <td align="left" style="height: 20px" >
                                                     <asp:TextBox ID="txttravel_email" runat="server" CssClass="input_box" MaxLength="99"
                                                         TabIndex="2" Width="170px"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 20px;">
                                                     Contact Person:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txt_travel_contactperson" runat="server" CssClass="input_box" MaxLength="29" TabIndex="3"
                                                         Width="170px"></asp:TextBox></td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 15px; height: 3px">
                                                  </td>
                                                  <td align="left" style="height: 3px">
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_travel_company"
                                                          ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                                                  <td align="right" style="padding-right: 15px; height: 3px">
                                                      &nbsp;</td>
                                                  <td align="left" style="height: 3px">
                                                      <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txttravel_email"
                                                          ErrorMessage="Invalid Email." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
                                                  <td align="right" style="padding-right: 15px; height: 3px">
                                                  </td>
                                                  <td align="left" style="height: 3px">
                                                  </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px;" valign="top">
                                                     Address:</td>
                                                 <td align="left">
                                                     <asp:TextBox ID="txt_travel_address" runat="server" CssClass="input_box" MaxLength="99"
                                                         TabIndex="4" Width="170px" Height="39px" TextMode="MultiLine"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px;" valign="top">
                                                     Phone:</td>
                                                 <td align="left" valign="top" >
                                                     <asp:TextBox ID="txt_travel_phone" runat="server" CssClass="input_box" MaxLength="20" TabIndex="5"
                                                         Width="170px"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; " valign="top">
                                                     Mobile:</td>
                                                 <td align="left" valign="top">
                                                     <asp:TextBox ID="txt_travel_mobile" runat="server" CssClass="input_box" MaxLength="20" TabIndex="6"
                                                         Width="170px"></asp:TextBox></td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 15px; height: 18px;" valign="top">
                                                  </td>
                                                  <td align="left" style="height: 18px">
                                                  </td>
                                                  <td align="right" style="padding-right: 15px; height: 18px;" valign="top">
                                                  </td>
                                                  <td align="left" valign="top" style="height: 18px">
                                                  </td>
                                                  <td align="right" style="padding-right: 15px; height: 18px;" valign="top">
                                                  </td>
                                                  <td align="left" valign="top" style="height: 18px">
                                                  </td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 15px" valign="top">
                                                      Fax:</td>
                                                  <td align="left">
                                                      <asp:TextBox ID="txtfax" runat="server" CssClass="input_box" MaxLength="20" TabIndex="7"
                                                          Width="170px"></asp:TextBox></td>
                                                  <td align="right" style="padding-right: 15px" valign="top">
                                                      Country:</td>
                                                  <td align="left" valign="top">
                                                      <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" CssClass="input_box"
                                                          OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" Width="175px" TabIndex="8">
                                                      </asp:DropDownList></td>
                                                  <td align="right" style="padding-right: 15px" valign="top">
                                                      Port:</td>
                                                  <td align="left" valign="top">
                                                      <asp:DropDownList ID="dd_port_portname" runat="server" CssClass="input_box" TabIndex="9"
                                                          Width="175px">
                                                      </asp:DropDownList>
                                                      <asp:ImageButton ID="imgaddport" runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/add_16.gif"
                                                          OnClientClick="return openpage();" /></td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 15px; height: 6px">
                                                  </td>
                                                  <td align="left" style="height: 6px">
                                                  </td>
                                                  <td align="right" style="padding-right: 15px; height: 6px">
                                                  </td>
                                                  <td align="left" style="height: 6px">
                                                      &nbsp;</td>
                                                  <td align="right" style="height: 6px">
                                                  </td>
                                                  <td align="left" style="height: 6px">
                                                      </td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 15px; height: 6px">
                                                      Vendor No:</td>
                                                  <td align="left" style="height: 6px">
                                                      <asp:TextBox ID="txtvendorno" runat="server" CssClass="input_box" MaxLength="9" TabIndex="10"
                                                          Width="170px"></asp:TextBox></td>
                                                  <td align="right" style="padding-right: 15px; height: 6px">
                                                  </td>
                                                  <td align="left" style="height: 6px">
                                                  </td>
                                                  <td align="right" style="height: 6px">
                                                  </td>
                                                  <td align="left" style="height: 6px">
                                                  </td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 15px; height: 6px">
                                                  </td>
                                                  <td align="left" style="height: 6px">
                                                      &nbsp;</td>
                                                  <td align="right" style="padding-right: 15px; height: 6px">
                                                  </td>
                                                  <td align="left" style="height: 6px">
                                                  </td>
                                                  <td align="right" style="height: 6px">
                                                  </td>
                                                  <td align="left" style="height: 6px">
                                                  </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Created By:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtcreatedby_travelagent" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="170px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Created On:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtcreatedon_travelagent" runat="server" CssClass="input_box" MaxLength="24"
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
                                                 <td align="right" style="padding-right:15px; height: 20px;">
                                                     Modified By:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtmodifiedby_travelagent" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="170px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 20px;">
                                                     Modified On:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtmodifiedon_travelagent" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="170px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="height: 20px">
                                                     </td>
                                                 <td align="left" style="height: 20px">
                                                     </td>
                                              </tr>
                                              <tr>
                                                  <td colspan="6" style="height: 13px">
                                                   &nbsp; &nbsp;
                                                  </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 20px;">
                                                     Status:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:DropDownList ID="ddstatus_travelagent" runat="server" CssClass="input_box" Width="175px" TabIndex="11">
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
                    <asp:Button ID="btn_travelagent_add" runat="server" CausesValidation="False" CssClass="btn"
                        OnClick="btn_travelagent_add_Click" Text="Add" Width="59px" TabIndex="12" />
                    <asp:Button ID="btn_travelagent_save" runat="server" CssClass="btn" OnClick="btn_travelagent_save_Click"
                            Text="Save" Width="59px" Visible="False" TabIndex="13" />
                    <asp:Button ID="btn_travelagent_Cancel" runat="server"
                                CausesValidation="false" CssClass="btn" OnClick="btn_travelagent_Cancel_Click" Text="Cancel"
                                Width="59px" Visible="False" TabIndex="14" />
                    <asp:Button ID="btn_Print_TravelAgent" runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_Print_TravelAgent_Click" TabIndex="15" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_travelagentpanel');" Visible="False" />                
                </td>
            </tr>
            <tr>
                <td align="right" style="height: 4px">
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                        FilterType="Numbers,Custom" TargetControlID="txt_travel_phone" ValidChars="-">
                    </ajaxToolkit:FilteredTextBoxExtender>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                        FilterType="Numbers,Custom" TargetControlID="txt_travel_mobile" ValidChars="+">
                    </ajaxToolkit:FilteredTextBoxExtender>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                        FilterType="Numbers,Custom" TargetControlID="txtfax" ValidChars="-">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
            </tr>
        </table>



</asp:Content>

