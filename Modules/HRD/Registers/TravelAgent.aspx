<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TravelAgent.aspx.cs" MasterPageFile="~/Modules/HRD/RegistersMasterPage.master" Inherits="Registers_TravelAgent" %>
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
   <%--  <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
        <table  align="center" width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
       <td>
           <asp:Label ID="Label1" runat="server" CssClass="textregisters" Text="Travel Agent"></asp:Label></td>
    </tr>  
        <tr>
            <td>
            
                <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid; border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; padding-top:0px; padding-bottom:10px">
                    <legend><strong>Travel Agent List</strong></legend>
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
                                    <ItemStyle Width="30px" />
                                </asp:CommandField>
                                <%--<asp:CommandField ButtonType="Image" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit"
                                    ShowEditButton="True">
                                    <ItemStyle Width="30px" />
                                </asp:CommandField>--%>
                                 <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle Width="40px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEditTravelAgent" CausesValidation="false" OnClick="btnEditTravelAgent_click"
                                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                                    CommandArgument='<%#Eval("TravelAgentId")%>' />
                                                <asp:HiddenField ID="hdnTravelAgentId" runat="server" Value='<%#Eval("TravelAgentId")%>' />
                                            </ItemTemplate>
                                                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                    <ItemStyle Width="30px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" Text="Delete" />
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Company">
                                    <ItemStyle HorizontalAlign="Left"  Width="120px"/>
                                    <ItemTemplate>
                                        <asp:Label ID="lblcom" runat="server" Text='<%#Eval("Company")%>'></asp:Label>
                                        <asp:HiddenField ID="HiddenTravelagentId" runat="server" Value='<%#Eval("TravelAgentId")%>' />
                                        <asp:HiddenField ID="HiddenCompany" runat="server" Value='<%#Eval("Company")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="Company" HeaderText="Company">
                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                </asp:BoundField>--%>
                                <%--<asp:BoundField DataField="TravelAgentEmail" HeaderText="Email">
                                    <ItemStyle HorizontalAlign="Left"  Width="90px"/>
                                </asp:BoundField>--%>
                                 <asp:BoundField DataField="VendorNo" HeaderText="VendorNo.">
                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ContactPerson" HeaderText="Contact Person">
                                    <ItemStyle HorizontalAlign="Left" Width="100px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Address" HeaderText="Address">
                                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Phone" HeaderText="Phone">
                                    <ItemStyle HorizontalAlign="Right" Width="90px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Mobile" HeaderText="Mobile">
                                    <ItemStyle HorizontalAlign="Right" Width="90px" />
                                </asp:BoundField>
                                 <%--<asp:BoundField DataField="FaxNo" HeaderText="Fax">
                                    <ItemStyle HorizontalAlign="Right" Width="90px" />
                                </asp:BoundField>--%>
                                <%--<asp:BoundField DataField="CreatedBy" HeaderText="Created By">
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
                                <asp:BoundField DataField="StatusId" HeaderText="Status">
                                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <asp:Label ID="lblTravel_agent" runat="server"></asp:Label>
                <asp:Label ID="lbl_TravelAgent_Message" runat="server" ForeColor="#C00000"></asp:Label></fieldset> </td>
        </tr>
            <tr>
                <td style="padding-top:15px">
                    <asp:Panel ID="travelagentpanel" runat="server" Visible="false" Width="100%">
                         <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                    <legend><strong>Travel Agent Details</strong></legend>
                                          <table cellpadding="0" cellspacing="0" width="100%">
                                              <tr>
                                                 <td colspan="6">
                                                     <asp:HiddenField ID="HiddenTravelagentpk" runat="server" />
                                                     &nbsp;
                                                 </td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 15px; height: 13px">
                                                  </td>
                                                  <td align="left" style="height: 13px; width: 215px;">
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
                                                 <td align="left" style="height: 20px; width: 215px;">
                                                     <asp:TextBox ID="txt_travel_company" runat="server" CssClass="required_box" MaxLength="49" TabIndex="5"
                                                         Width="190px"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px;">
                                                     Email:</td>
                                                 <td align="left" >
                                                     <asp:TextBox ID="txttravel_email" runat="server" CssClass="input_box" MaxLength="99"
                                                         TabIndex="4" Width="190px"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; ">
                                                     Contact Person:</td>
                                                 <td align="left">
                                                     <asp:TextBox ID="txt_travel_contactperson" runat="server" CssClass="input_box" MaxLength="29" TabIndex="6"
                                                         Width="190px"></asp:TextBox></td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 15px; height: 3px">
                                                  </td>
                                                  <td align="left" style="height: 3px; width: 215px;">
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
                                                  <td align="left" rowspan="3" style="width: 215px">
                                                     <asp:TextBox ID="txt_travel_address" runat="server" CssClass="input_box" MaxLength="99"
                                                         TabIndex="7" Width="190px" Height="50px" TextMode="MultiLine"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px;" valign="top">
                                                     Phone:</td>
                                                 <td align="left" valign="top" >
                                                     <asp:TextBox ID="txt_travel_phone" runat="server" CssClass="input_box" MaxLength="20" TabIndex="8"
                                                         Width="190px"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; " valign="top">
                                                     Mobile:</td>
                                                 <td align="left" valign="top">
                                                     <asp:TextBox ID="txt_travel_mobile" runat="server" CssClass="input_box" MaxLength="20" TabIndex="9"
                                                         Width="190px"></asp:TextBox></td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 15px; height: 21px" valign="top">
                                                  </td>
                                                  <td align="right" style="padding-right: 15px; height: 21px" valign="top">
                                                  </td>
                                                  <td align="left" style="height: 21px" valign="top">
                                                  </td>
                                                  <td align="right" style="padding-right: 15px; height: 21px" valign="top">
                                                  </td>
                                                  <td align="left" style="height: 21px" valign="top">
                                                  </td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 15px" valign="top">
                                                  </td>
                                                  <td align="right" style="padding-right: 15px" valign="top">
                                                      Fax:</td>
                                                  <td align="left" valign="top">
                                                      <asp:TextBox ID="txtfax" runat="server" CssClass="input_box" MaxLength="20" TabIndex="8"
                                                          Width="190px"></asp:TextBox></td>
                                                  <td align="right" style="padding-right: 15px" valign="top">
                                                      Vendor No.:</td>
                                                  <td align="left" valign="top">
                                                      <asp:TextBox ID="txt_VendorNo" runat="server" CssClass="input_box" MaxLength="9"
                                                          TabIndex="8" Width="190px"></asp:TextBox></td>
                                              </tr>
                                              <tr>
                                                  <td colspan="6" align="left" style="height: 13px" >
                                                     &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                      &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                      &nbsp;&nbsp;
                                                 </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Created By:</td>
                                                 <td align="left" style="height: 20px; width: 215px;">
                                                     <asp:TextBox ID="txtcreatedby_travelagent" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="190px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 19px;">
                                                     Created On:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtcreatedon_travelagent" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="190px" ReadOnly="True"></asp:TextBox></td>
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
                                                 <td align="left" style="height: 20px; width: 215px;">
                                                     <asp:TextBox ID="txtmodifiedby_travelagent" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="190px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:15px; height: 20px;">
                                                     Modified On:</td>
                                                 <td align="left" style="height: 20px">
                                                     <asp:TextBox ID="txtmodifiedon_travelagent" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="190px" ReadOnly="True"></asp:TextBox></td>
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
                                                 <td align="left" style="height: 20px; width: 215px;">
                                                     <asp:DropDownList ID="ddstatus_travelagent" runat="server" CssClass="input_box" Width="195px" TabIndex="10">
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
                        OnClick="btn_travelagent_add_Click" Text="Add" Width="59px" TabIndex="11" />
                    <asp:Button ID="btn_travelagent_save" runat="server" CssClass="btn" OnClick="btn_travelagent_save_Click"
                            Text="Save" Width="59px" Visible="False" TabIndex="12" />
                    <asp:Button ID="btn_travelagent_Cancel" runat="server"
                                CausesValidation="false" CssClass="btn" OnClick="btn_travelagent_Cancel_Click" Text="Cancel"
                                Width="59px" Visible="False" TabIndex="13" />
                    <asp:Button ID="btn_Print_TravelAgent" runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_Print_TravelAgent_Click" TabIndex="14" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_travelagentpanel');" Visible="False" />                
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
   <%-- </form>
</body>
</html>--%>
