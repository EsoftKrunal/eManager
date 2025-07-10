<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PortAgent.aspx.cs" MasterPageFile="~/Modules/HRD/RegistersMasterPage.master" Inherits="Registers_PortAgent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<script language="javascript">
function openpage()
{
var url = "../Registers/PopUp_Port.aspx"
window.open(url,null,'title=no,toolbars=no,scrollbars=yes,width=400,height=200,left=50,top=50,addressbar=no');
return false;
}
</script>
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
        </asp:ScriptManager>
     <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
        <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
       <td>
           <asp:Label ID="Label1" runat="server" CssClass="textregisters" Text="Port Agent"></asp:Label></td>
    </tr> 
    <tr>
    <td>
     <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; padding-bottom:10px">
                    <legend><strong>Search</strong></legend>
                    <table width="100%" style="padding-top: 3px" cellpadding="0" cellspacing="0"><tr>
                    <td style=" text-align:left; width: 132px; height: 19px;">
                        &nbsp;Company Name :</td>
                    <td style=" text-align:left;height: 19px;"><asp:TextBox ID="txt_PortAgent" runat="server" MaxLength="30" CssClass="input_box" onkeydown="javascript:if(event.keyCode==13){document.getElementById('ctl00$ContentPlaceHolder1$btn_Search').focus();}" Width="284px"></asp:TextBox> </td>
                    <td style="width: 86px; height: 19px"><asp:Button ID="btn_Search" runat="server" Text="Search" CssClass="input_box" OnClick="btn_Search_Click" Width="62px" /></td>
                    </tr></table>
                </fieldset>
    
    </td>
    </tr> 
        <tr>
            <td align="center">
            
                <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; padding-top:0px; padding-bottom:10px">
                    <legend><strong>Port Agent List</strong></legend>
                    <div style="overflow-:hidden;overflow-y:scroll; width:100%; height: 150px">
                        <asp:GridView ID="GvPortAgent" runat="server" GridLines="Horizontal" AutoGenerateColumns="False" OnDataBound="GvPortAgent_DataBound" OnPreRender="GvPortAgent_PreRender" OnRowDeleting="GvPortAgent_Row_Deleting" OnRowEditing="GvPortAgent_Row_Editing" OnSelectedIndexChanged="GvPortAgent_SelectIndexChanged" Style="text-align: center" Width="98%" OnRowCommand="GvPortAgent_RowCommand">
                           <RowStyle CssClass="rowstyle" />
                            <SelectedRowStyle CssClass="selectedtowstyle" />
                            <PagerStyle CssClass="pagerstyle" />
                            <HeaderStyle CssClass="headerstylefixedheadergrid" />
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
                                                <asp:ImageButton ID="btnEditPortAgent" CausesValidation="false" OnClick="btnEditPortAgent_click"
                                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                                    CommandArgument='<%#Eval("PortAgentId")%>' />
                                                <asp:HiddenField ID="hdnPortAgentId" runat="server" Value='<%#Eval("PortAgentId")%>' />
                                            </ItemTemplate>
                                                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                    <ItemStyle Width="25px" />
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                            ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');"
                                            Text="Delete" />
                                        <asp:HiddenField ID="HiddenPortagentId" runat="server" Value='<%#Eval("PortAgentId")%>' />
                                        <asp:HiddenField ID="HiddenCompanyName" runat="server" Value='<%#Eval("Company")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Company" HeaderText="Company">
                                    <ItemStyle HorizontalAlign="Left" Width="300px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="VendorNo" HeaderText="Vendor #">
                                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                                </asp:BoundField>
                                <%-- <asp:TemplateField HeaderText="Email">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="a" Text='<%#Eval("PortAgentEmail")%>'></asp:Label>
                                        <asp:HiddenField ID="HiddenPortagentId" runat="server" Value='<%#Eval("PortAgentId")%>' />
                                        <asp:HiddenField ID="HiddenCompanyName" runat="server" Value='<%#Eval("Company")%>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" HorizontalAlign="Left" />
                                </asp:TemplateField>--%>
                               <%-- <asp:TemplateField HeaderText="Contact Person">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="a" Text='<%#Eval("ContactPerson")%>'></asp:Label>
                                        <asp:HiddenField ID="HiddenPortagentId" runat="server" Value='<%#Eval("PortAgentId")%>' />
                                        <asp:HiddenField ID="HiddenCompanyName" runat="server" Value='<%#Eval("Company")%>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="120px" HorizontalAlign="Left" />
                                </asp:TemplateField>--%>
                                <%--<asp:BoundField DataField="ContactPerson" HeaderText="Contact Person">
                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                </asp:BoundField>--%>
                                <%--<asp:BoundField DataField="Address" HeaderText="Address">
                                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Phone" HeaderText="Phone">
                                    <ItemStyle HorizontalAlign="Right" Width="60px" />
                                </asp:BoundField>--%>
                               <%-- <asp:BoundField DataField="Mobile" HeaderText="Mobile">
                                    <ItemStyle HorizontalAlign="Right" Width="90px" />
                                </asp:BoundField>--%>
                               <%-- <asp:BoundField DataField="FaxNo" HeaderText="Fax">
                                    <ItemStyle HorizontalAlign="Right" Width="90px" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="PortId" HeaderText="Port">
                                    <ItemStyle HorizontalAlign="Left" Width="90px" />
                                </asp:BoundField>
                                <%--<asp:BoundField DataField="CreatedBy" HeaderText="Created By">
                                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CreatedOn" HeaderText="Created On">
                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ModifiedBy" HeaderText="Modified By">
                                    <ItemStyle HorizontalAlign="Right" Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ModifiedOn" HeaderText="Modified On">
                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="StatusId" HeaderText="Status">
                                    <ItemStyle HorizontalAlign="Left" Width="60px" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <asp:Label ID="lblPort_agent" runat="server"></asp:Label><asp:Label ID="lbl_PortAgent_Message"
                        runat="server" ForeColor="#C00000"></asp:Label></fieldset> 
                </td>
        </tr>
            <tr>
                <td style="padding-top:15px">
                    <asp:Panel ID="Portagentpanel" runat="server" Visible="false" Width="100%">
                         <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
                                        border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid">
                                    <legend><strong>Port Agent Details</strong></legend>
                                          <table cellpadding="0" cellspacing="0" width="100%">
                                              <tr>
                                                 <td colspan="6" style="height: 37px">
                                                     <asp:HiddenField ID="HiddenPortagentpk" runat="server" />
                                                     &nbsp;
                                                 </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:10px; width: 139px; height: 19px;">
                                                     Company:</td>
                                                 <td align="left" style="width: 188px; height: 19px">
                                                     <asp:TextBox ID="txt_port_company" runat="server" CssClass="required_box" MaxLength="49" TabIndex="4"
                                                         Width="180px"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:10px; width: 101px; height: 19px;">
                                                     Email:</td>
                                                 <td align="left" style="width: 143px; height: 19px;" >
                                                     <asp:TextBox ID="txtPort_email" runat="server" CssClass="input_box" MaxLength="99"
                                                         TabIndex="5" Width="180px"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:10px; width: 120px; height: 19px;">
                                                     Contact Person:</td>
                                                 <td align="left" style="width: 225px; height: 19px;">
                                                     <asp:TextBox ID="txt_port_contactperson" runat="server" CssClass="input_box" MaxLength="29" TabIndex="6"
                                                         Width="180px"></asp:TextBox></td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 15px; height: 16px; width: 139px;">
                                                  </td>
                                                  <td align="left" style="height: 16px; width: 188px;">
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_port_company"
                                                          ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                                                  <td align="right" style="padding-right: 15px; height: 16px; width: 101px;">
                                                  </td>
                                                  <td align="left" style="height: 16px; width: 143px;">
                                                      <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPort_email"
                                                          ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
                                                  <td align="right" style="padding-right: 15px; height: 16px; width: 120px;">
                                                  </td>
                                                  <td align="left" style="height: 16px; width: 225px;">
                                                  </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:10px; width: 139px; height: 12px;" valign="top">
                                                     Address:</td>
                                                  <td align="left" rowspan="3" style="width: 188px">
                                                     <asp:TextBox ID="txt_port_address" runat="server" CssClass="input_box" MaxLength="99"
                                                         TabIndex="7" Width="180px" Height="44px" TextMode="MultiLine"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:10px; width: 101px; height: 12px;" valign="top">
                                                     Phone:</td>
                                                 <td align="left" valign="top" style="width: 143px; height: 12px;" >
                                                     <asp:TextBox ID="txt_port_phone" runat="server" CssClass="input_box" MaxLength="20" TabIndex="8"
                                                         Width="180px"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:10px; width: 120px; height: 12px;" valign="top">
                                                     Mobile:</td>
                                                 <td align="left" valign="top" style="width: 225px; height: 12px;">
                                                     <asp:TextBox ID="txt_port_mobile" runat="server" CssClass="input_box" MaxLength="20" TabIndex="9"
                                                         Width="180px"></asp:TextBox></td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 10px; width: 139px; height: 19px">
                                                  </td>
                                                  <td align="right" style="padding-right: 10px; width: 101px; height: 19px">
                                                  </td>
                                                  <td align="left" style="width: 143px; height: 19px">
                                                  </td>
                                                  <td align="right" style="padding-right: 10px; width: 120px; height: 19px">
                                                  </td>
                                                  <td align="left" style="width: 225px; height: 19px">
                                                  </td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 10px; width: 139px; height: 19px">
                                                  </td>
                                                  <td align="right" style="padding-right: 10px; width: 101px; height: 19px">
                                                     Country:</td>
                                                  <td align="left" style="width: 143px; height: 19px">
                                                     <asp:DropDownList ID="ddlCountry" runat="server" CssClass="required_box" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" Width="185px">
                                                     </asp:DropDownList></td>
                                                  <td align="right" style="padding-right: 10px; width: 120px; height: 19px; text-align: right">
                                                      Ports:</td>
                                                  <td align="left" style="width: 225px; vertical-align:top" rowspan="9">
                                                      <div class="input_box" style=" height :150px; overflow-y :scroll; width :182px; overflow-x :hidden; float :left ">
                                                          <asp:CheckBoxList ID="lblOtherPorts" runat="Server" Width="170px">
                                                          </asp:CheckBoxList>
                                                      </div>
                                                      <div style="float :right;text-align :left " >
                                                     <asp:ImageButton ID="imgaddport" runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/add_16.gif"
                                                         OnClientClick="return openpage();" />
                                                         </div>
                                                   </td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 10px; width: 139px; height: 19px">
                                                  </td>
                                                  <td align="left" style="width: 188px; height: 19px">
                                                      &nbsp;</td>
                                                  <td align="right" style="padding-right: 10px; width: 101px; height: 19px">
                                                  </td>
                                                  <td align="left" style="width: 143px; height: 19px">
                                                      <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="ddlCountry"
                                                          ErrorMessage="Required." MaximumValue="100000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                                                  <td align="right" style="padding-right: 10px; width: 120px; height: 19px">
                                                  </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:10px; height: 19px; width: 139px;">
                                                     Fax:</td>
                                                 <td align="left" style="height: 19px; width: 188px;">
                                                     <asp:TextBox ID="txtfax" runat="server" CssClass="input_box" MaxLength="20" TabIndex="8"
                                                         Width="180px"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:10px; height: 19px; width: 101px;">
                                                      Vendor No.:</td>
                                                 <td align="left" style="height: 19px; width: 143px;" >
                                                      <asp:TextBox ID="txt_VendorNo" runat="server" CssClass="input_box" MaxLength="9"
                                                          TabIndex="11" Width="180px"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:10px; height: 19px; width: 120px;">
                                                      <asp:DropDownList ID="dd_port_portname" runat="server" CssClass="required_box" 
                                                          TabIndex="10" Width="185px" Visible="False">
                                                      </asp:DropDownList>
                                                  </td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 15px; width: 139px">
                                                  </td>
                                                  <td align="left" style="width: 188px">
                                                      &nbsp;
                                                  </td>
                                                  <td align="right" style="padding-right: 15px; width: 101px">
                                                  </td>
                                                  <td align="left" style="width: 143px">
                                                  </td>
                                                  <td align="right" style="padding-right: 15px; width: 120px">
                                                  </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:10px; width: 139px; height: 21px;">
                                                     Created By:</td>
                                                 <td align="left" style="width: 188px; height: 21px" >
                                                     <asp:TextBox ID="txtcreatedby_Portagent" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="180px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:10px; width: 101px; height: 21px;">
                                                     Created On:</td>
                                                 <td align="left" style="width: 143px; height: 21px;">
                                                     <asp:TextBox ID="txtcreatedon_Portagent" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="180px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="width: 120px; height: 21px;">
                                                     </td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 10px; width: 139px">
                                                  </td>
                                                  <td align="left" style="width: 188px">
                                                      &nbsp;</td>
                                                  <td align="right" style="padding-right: 10px; width: 101px">
                                                  </td>
                                                  <td align="left" style="width: 143px">
                                                  </td>
                                                  <td align="right" style="width: 120px">
                                                  </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:10px; width: 139px;">
                                                     Modified By:</td>
                                                 <td align="left" style="width: 188px">
                                                     <asp:TextBox ID="txtmodifiedby_Portagent" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="180px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="padding-right:10px; width: 101px;">
                                                     Modified On:</td>
                                                 <td align="left" style="width: 143px">
                                                     <asp:TextBox ID="txtmodifiedon_Portagent" runat="server" CssClass="input_box" MaxLength="24"
                                                         Width="180px" ReadOnly="True"></asp:TextBox></td>
                                                 <td align="right" style="width: 120px">
                                                     </td>
                                              </tr>
                                              <tr>
                                                  <td align="right" style="padding-right: 10px; width: 139px">
                                                  </td>
                                                  <td align="left" style="width: 188px">
                                                      &nbsp;</td>
                                                  <td align="right" style="padding-right: 10px; width: 101px">
                                                  </td>
                                                  <td align="left" style="width: 143px">
                                                  </td>
                                                  <td align="right" style="width: 120px">
                                                  </td>
                                              </tr>
                                              <tr>
                                                 <td align="right" style="padding-right:10px; width: 139px;">
                                                     Status:</td>
                                                 <td align="left" style="width: 188px">
                                                     <asp:DropDownList ID="ddstatus_Portagent" runat="server" CssClass="input_box" Width="185px" TabIndex="12">
                                                     </asp:DropDownList></td>
                                                 <td align="right" style="width: 101px">
                                                     </td>
                                                 <td align="left" style="width: 143px">
                                                     </td>
                                                 <td align="right" style="width: 120px">
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
                <td align="right">
                    <asp:Button ID="btn_Portagent_add" runat="server" CausesValidation="False" CssClass="btn"
                        OnClick="btn_Portagent_add_Click" Text="Add" Width="59px" TabIndex="13" />
                    <asp:Button ID="btn_Portagent_save" runat="server" CssClass="btn" OnClick="btn_Portagent_save_Click"
                            Text="Save" Width="59px" Visible="False" TabIndex="14" />
                    <asp:Button ID="btn_Portagent_Cancel" runat="server"
                                CausesValidation="false" CssClass="btn" OnClick="btn_Portagent_Cancel_Click" Text="Cancel"
                                Width="59px" Visible="False" TabIndex="15" />
                    <asp:Button ID="btn_Print_PortAgent" runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_Print_PortAgent_Click" TabIndex="16" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_Portagentpanel');" Visible="False" />                
                </td>
            </tr>
             <tr>
                <td align="right">
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                        FilterType="Numbers,Custom" TargetControlID="txt_port_phone" ValidChars="-">
                    </ajaxToolkit:FilteredTextBoxExtender>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                        FilterType="Numbers,Custom" TargetControlID="txt_port_mobile" ValidChars="+">
                    </ajaxToolkit:FilteredTextBoxExtender>
                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                        FilterType="Numbers,Custom" TargetControlID="txtfax" ValidChars="-">
                    </ajaxToolkit:FilteredTextBoxExtender>
                </td>
            </tr>
             <tr>
                <td align="right" style="height: 4px">
                </td>
            </tr>
        </table>
         </asp:Content>
  <%--  </form>
</body>
</html>--%>