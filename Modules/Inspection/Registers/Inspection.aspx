<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Inspection.aspx.cs" Inherits="Registers_Inspection"  %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>EMANAGER</title>
       <link href="../../HRD/Styles/style.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
     </head>
<body  >
<form id="form1" runat="server" >
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">    
     <tr>
       <td>
        <table cellpadding="0" cellspacing="0" width="100%">
            <%--<tr>
       <td align="center">
           <asp:Label ID="Label1" runat="server" CssClass="textregisters" Text="Inspection"></asp:Label></td>
    </tr> --%>
          <tr>
            <td style="text-align: center;">
              <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 0px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="">
                <%--<legend><strong>Inspection</strong></legend>--%>
                <asp:Panel ID="pnl_Inspection" runat="server" Width="100%">
                  <table border="0" cellpadding="0" cellspacing="0" style="text-align: center;" width="100%">
                    <tr>
                      <td colspan="2" style="height: 15px">
                          <asp:HiddenField ID="HiddenInspection" runat="server" />
                      </td>
                        <td colspan="1" style="height: 15px">
                        </td>
                                                            </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px; width: 287px;">
                              Inspection Group:</td>
                          <td style="text-align: left;" colspan="1">
                              <asp:DropDownList ID="ddlInspectionGrp" runat="server" CssClass="input_box" Width="514px" TabIndex="1" AutoPostBack="True" OnSelectedIndexChanged="ddlInspectionGrp_SelectedIndexChanged">
                              </asp:DropDownList></td>
                          <td colspan="1" style="text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right; height: 3px; width: 287px;">
                          </td>
                          <td style="text-align: left; height: 3px;">
                              </td>
                          <td style="height: 3px; text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; width: 287px; text-align: right">
                              Inspection
                              Code:</td>
                          <td colspan="1" style="text-align: left">
                                                                    <asp:TextBox ID="txtInspectionCode" runat="server" CssClass="input_box" Width="510px" MaxLength="29" TabIndex="2" Enabled="False"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtInspectionCode"
                                  ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                          <td colspan="1" style="text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; width: 287px; text-align: right">
                          </td>
                          <td style="text-align: left">
                              </td>
                          <td style="text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px; width: 287px;">
                              Inspection 
                              Name:</td>
                          <td style="text-align: left;" colspan="1">
                                                                    <asp:TextBox ID="txtInspectionName" runat="server" CssClass="input_box" MaxLength="29" TabIndex="3"
                                                                        Width="510px" Enabled="False"></asp:TextBox></td>
                          <td colspan="1" style="text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right; height: 2px; width: 287px;">
                          </td>
                          <td style="text-align: left; height: 2px;">
                              </td>
                          <td style="height: 2px; text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right; width: 287px;">
                              Interval (Days):</td>
                          <td style="text-align: left">
                              <table cellpadding="0" cellspacing="0">
                                  <tr>
                                      <td style="width: 251px">
                              <asp:TextBox ID="txtInterval" runat="server" CssClass="input_box" TabIndex="4" MaxLength="4" Enabled="False"></asp:TextBox></td>
                                      <td style="padding-right: 15px; padding-left: 20px; text-align: right">
                              Tolerance (Days):</td>
                                      <td>
                              <asp:TextBox ID="txtTolerance" runat="server" CssClass="input_box" TabIndex="5" MaxLength="4" Enabled="False"></asp:TextBox></td>
                                  </tr>
                              </table>
                          </td>
                          <td style="text-align: left">
                          </td>
                      </tr>
                        <tr>
                          <td align="right" style="padding-right: 15px; width: 287px; height: 3px; text-align: right">
                          Color :
                          </td>
                          <td style="height: 3px; text-align: left; display:inline;">
                            <asp:TextBox runat="server" ID="txtColor" CssClass="input_box" MaxLength="6" Width="45px" style="float:left"></asp:TextBox>
                            <div runat="server" id="dv_color" style="width:40px; height:16px; float:left;"></div>
                          </td>
                          <td style="height: 3px; text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; width: 287px; height: 3px; text-align: right">
                          </td>
                          <td style="height: 3px; text-align: left">
                          </td>
                          <td style="height: 3px; text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; width: 287px; text-align: right">
                              Inspection Dept.:</td>
                          <td style="text-align: left" valign="top">
                              <asp:CheckBox ID="rdbtnTechnical" runat="server" Text="Technical" TabIndex="6" Enabled="False" />&nbsp;
                              <asp:CheckBox ID="rdbtnMarine" runat="server" Text="Marine" TabIndex="7" Enabled="False" />&nbsp;
                              <asp:CheckBox ID="rdbtnRandmInsp" runat="server" Text="Random Inspection" TabIndex="8" Enabled="False" />
                              &nbsp; &nbsp;FollowUp Category: &nbsp;&nbsp;<asp:DropDownList id="ddl_FollowUpCat"
                                  runat="server" CssClass="input_box" Width="96px" Enabled="False" TabIndex="9"><asp:ListItem Value="0">&lt;Select&gt;</asp:ListItem>
                                  <asp:ListItem Value="1">Audit</asp:ListItem>
                                  <asp:ListItem Value="2">NCR</asp:ListItem>
                                  <asp:ListItem Value="3">Technical</asp:ListItem>
                                  <asp:ListItem Value="4">Vetting</asp:ListItem>
                                  <asp:ListItem Value="5">Others</asp:ListItem>
                              </asp:DropDownList></td>
                          <td style="padding-right: 17px; text-align: right" valign="top">
                                  <asp:Button ID="btn_Cancel_Inspection" runat="server" CausesValidation="False" CssClass="btn" TabIndex="10" Text="Cancel" Visible="False" Width="59px" OnClick="btn_Cancel_Inspection_Click" />
                              <asp:Button ID="btn_New_Inspection" runat="server" CssClass="btn" Text="New" Width="59px" CausesValidation="False" TabIndex="11" OnClick="btn_New_Inspection_Click" />
                              <asp:Button ID="btn_Save_Inspection" runat="server" CssClass="btn" Text="Save" Width="59px" TabIndex="12" OnClick="btn_Save_Inspection_Click" Enabled="False" /></td>
                      </tr>
                                                        </table>
                                                    
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td style="text-align: right; padding-right: 17px;">
                        <div id="Div1" runat="server" style="position: relative; top: 0; text-align: center; left: 0"><asp:Label ID="lbl_Inspection_Message" runat="server" ForeColor="#C00000"></asp:Label></div>
                            <%--<asp:Button ID="btn_Cancel_Inspection" runat="server" CssClass="btn" Text="Cancel" Width="59px" CausesValidation="False" TabIndex="5" />
                            <asp:Button ID="btn_Print_Inspection" runat="server" CausesValidation="False" CssClass="btn" TabIndex="6" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_pnl_FlagState');" />
                            <asp:Button ID="txt_Export_Inspection" runat="server" CausesValidation="False" CssClass="btn" TabIndex="6" Text="Export" Width="59px" />--%></td>
                    </tr>
                </table>
                
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="padding-right: 5px; padding-left: 5px">
                            <div style="width: 100%; height: 220px"><%-- height: 150px;240--%>
                            <asp:GridView ID="GridView_Insp" runat="server" AutoGenerateColumns="False" GridLines="Horizontal" Width="98%" OnPreRender="GridView_Insp_PreRender" OnRowDataBound="GridView_Insp_RowDataBound" OnRowDeleting="GridView_Insp_RowDeleting" OnRowCreated="GridView_Insp_RowCreated" AllowPaging="True" OnPageIndexChanging="GridView_Insp_PageIndexChanging" PageSize="7" OnRowCancelingEdit="GridView_Insp_RowCancelingEdit" OnRowCommand="GridView_Insp_RowCommand">
                            <RowStyle CssClass="rowstyle" />
                         <Columns>
<asp:TemplateField HeaderText="Inspection Code"><ItemTemplate>
                                    <asp:Label ID="lbl_InspCode" runat="server" Text='<%# Eval("Code") %>'></asp:Label>
                                    <asp:HiddenField ID="Hidden_InspectionId" runat="server" Value='<%# Eval("Id") %>' />
                                
</ItemTemplate>

<ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
</asp:TemplateField>
<asp:BoundField DataField="Name" HeaderText="Inspection Name">
<ItemStyle HorizontalAlign="Left"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="Interval" HeaderText="Interval (Days)">
<ItemStyle HorizontalAlign="Right" Width="95px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="Tolerance" HeaderText="Tolerance (Days)">
<ItemStyle HorizontalAlign="Right" Width="105px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="InspectionDept" HeaderText="Inspection Dept.">
<ItemStyle HorizontalAlign="Left" Width="120px"></ItemStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="Edit" ShowHeader="False"><ItemTemplate>
                                
      <asp:ImageButton ID="ImageButton2" CausesValidation="false" OnClick="ImageButton2_Click"
                                                                                ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                                                                CommandArgument='<%#Eval("Id")%>' />
                                
</ItemTemplate>

<ItemStyle Width="40px"></ItemStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="Delete" ShowHeader="False"><ItemTemplate>
                                 <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" ToolTip="Delete" />
                                
</ItemTemplate>

<ItemStyle Width="45px"></ItemStyle>
</asp:TemplateField>
</Columns>
                                <pagerstyle horizontalalign="Center" />
                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                     </asp:GridView>
                     </div>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="padding-right: 7px; padding-left: 16px; text-align: center">
                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td style="padding-right: 8px; text-align: left">
                        <asp:Label ID="lbl_GridView_Inspection" runat="server" Text=""></asp:Label></td>
                                    <td style="padding-right: 8px; text-align: right">
                                        Created By:</td>
                                    <td style="text-align: left">
                                                                    <asp:TextBox ID="txtCreatedBy_Inspection" runat="server" CssClass="input_box" ReadOnly="True" Width="154px" TabIndex="-1" BackColor="Gainsboro"></asp:TextBox></td>
                                    <td style="padding-right: 8px; text-align: right">
                                        Created On:</td>
                                    <td style="text-align: left">
                                                                    <asp:TextBox ID="txtCreatedOn_Inspection" runat="server" CssClass="input_box" ReadOnly="True" Width="72px" TabIndex="-2" BackColor="Gainsboro"></asp:TextBox></td>
                                    <td style="padding-right: 8px; text-align: right">
                                        Modified By:</td>
                                    <td style="text-align: left">
                                                                    <asp:TextBox ID="txtModifiedBy_Inspection" runat="server" CssClass="input_box" ReadOnly="True" Width="154px" TabIndex="-3" BackColor="Gainsboro"></asp:TextBox></td>
                                    <td style="padding-right: 8px; text-align: right">
                                        Modified On:</td>
                                    <td style="text-align: left">
                                                                    <asp:TextBox ID="txtModifiedOn_Inspection" runat="server" CssClass="input_box" ReadOnly="True" Width="72px" TabIndex="-4" BackColor="Gainsboro"></asp:TextBox></td>
                                    <td style="text-align: left">
                              <asp:Button ID="btn_Print_Inspection" runat="server" CausesValidation="False" CssClass="btn" TabIndex="13" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_pnl_Inspection');" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>&nbsp;
            </asp:Panel>
            </fieldset>
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                    FilterType="Numbers" TargetControlID="txtInterval">
                </ajaxToolkit:FilteredTextBoxExtender>
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                    FilterType="Numbers" TargetControlID="txtTolerance">
                </ajaxToolkit:FilteredTextBoxExtender>
                <asp:HiddenField id="HiddenFieldGridRowCount" runat="server">
                </asp:HiddenField>
          </td>
         </tr>
       </table>
       &nbsp;
       </td>
      </tr>
     </table>
  </form>
</body>
</html>

