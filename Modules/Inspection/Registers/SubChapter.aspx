<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="SubChapter.aspx.cs" Inherits="Registers_SubChapter"  %>
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
           <asp:Label ID="Label1" runat="server" CssClass="textregisters" Text="Sub Chapter"></asp:Label></td>
    </tr> --%>
          <tr>
            <td style="text-align: center;">
            <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 0px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="">
                <%--<legend><strong>Sub Chapter</strong></legend>--%>
            <asp:Panel ID="pnl_SubChapter" runat="server" Width="100%">
                  <table border="0" cellpadding="0" cellspacing="0" style="text-align: center" width="100%">
                    <tr>
                      <td colspan="2" style="height: 15px">
                          <asp:HiddenField ID="HiddenSubChapter" runat="server" />
                      </td>
                        <td colspan="1" style="height: 15px">
                        </td>
                                                            </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px; width: 287px;">
                              Inspection Group:</td>
                          <td style="text-align: left;">
                              <asp:DropDownList ID="ddl_InspGroup" runat="server" CssClass="input_box" Width="514px" TabIndex="1" AutoPostBack="True" OnSelectedIndexChanged="ddl_InspGroup_SelectedIndexChanged">
                              </asp:DropDownList></td>
                          <td style="text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right; width: 287px; height: 3px;">
                          </td>
                          <td style="text-align: left; height: 3px;">
                              </td>
                          <td style="height: 3px; text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; width: 287px; text-align: right">
                              Chapter Name:</td>
                          <td style="text-align: left">
                              <asp:DropDownList ID="ddlChapterName" runat="server" CssClass="input_box" Width="514px" TabIndex="2" AutoPostBack="True" OnSelectedIndexChanged="ddlChapterName_SelectedIndexChanged">
                          </asp:DropDownList></td>
                          <td style="text-align: left">
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
                          <td align="right" style="text-align: right; padding-right:15px; width: 287px;">
                              Sub
                              Chapter#:</td>
                          <td style="text-align: left;">
                              <asp:TextBox ID="txtSubChapterNo" runat="server" CssClass="input_box" MaxLength="49"
                                  TabIndex="3" Width="510px" Enabled="False"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidatorSubChapterName" runat="server"
                                  ControlToValidate="txtSubChapterNo" ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                          <td style="text-align: left">
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
                              Sub Chapter Name:</td>
                          <td style="text-align: left">
                              <asp:TextBox ID="txtSubChapterName" runat="server" CssClass="input_box" MaxLength="49" TabIndex="4"
                                  Width="510px" Enabled="False"></asp:TextBox><%--<div id="dv_btns" runat="server" style="float: right; position:relative; top: 0; padding-right: 17px"></div>--%></td>
                          <td style="text-align: right; padding-right: 17px;">
                        <asp:Button ID="btn_Cancel_SubChapter" runat="server" CausesValidation="False" CssClass="btn" TabIndex="5" Text="Cancel" Visible="False" Width="59px" OnClick="btn_Cancel_SubChapter_Click" />
                              <asp:Button ID="btn_New_SubChapter" runat="server" CssClass="btn" Text="New" Width="59px" CausesValidation="False" TabIndex="6" OnClick="btn_New_SubChapter_Click" />
                              <asp:Button ID="btn_Save_SubChapter" runat="server" CssClass="btn" Text="Save" Width="59px" TabIndex="7" OnClick="btn_Save_SubChapter_Click" Enabled="False" /></td>
                      </tr>
                </table>
                                                    
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td style="text-align: right; padding-right: 17px;">
                        <div id="Div1" runat="server" style="position: relative; top: 0; text-align: center; left: 0"><asp:Label ID="lbl_SubChapter_Message" runat="server" ForeColor="#C00000"></asp:Label>&nbsp;</div>
                        </td>
                    </tr>
                </table>
                
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="padding-right: 5px; padding-left: 5px">
                            <div style="width: 100%; height: 240px"><%-- height: 150px--%>
                       <asp:GridView ID="GridView_SubChapter" runat="server" AutoGenerateColumns="False" Width="98%" DataKeyNames="Id" GridLines="Horizontal" OnPreRender="GridView_SubChapter_PreRender" OnRowDataBound="GridView_SubChapter_RowDataBound" OnRowDeleting="GridView_SubChapter_RowDeleting" OnRowEditing="GridView_SubChapter_RowEditing" OnRowCreated="GridView_SubChapter_RowCreated" AllowPaging="True" OnPageIndexChanging="GridView_SubChapter_PageIndexChanging" PageSize="8" OnRowCancelingEdit="GridView_SubChapter_RowCancelingEdit" OnRowCommand="GridView_SubChapter_RowCommand" >
                              <RowStyle CssClass="rowstyle" />
                         <Columns>
<asp:TemplateField HeaderText="Sub Chapter#"><ItemTemplate>
                                <asp:Label ID="lbl_SubChapNo" runat="server" Text='<%# Eval("SubChapterNo") %>'></asp:Label>
                                <asp:HiddenField ID="Hidden_SubChapterId" runat="server" Value='<%# Eval("Id") %>' />
                            
</ItemTemplate>

<ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
</asp:TemplateField>
<asp:BoundField DataField="SubChapterName" HeaderText="Sub Chapter Name">
<ItemStyle HorizontalAlign="Left"></ItemStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="Edit" ShowHeader="False"><ItemTemplate>
                                   <asp:ImageButton ID="ImageButton2" CausesValidation="false" OnClick="ImageButton2_Click"
                                                                                ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                                                                CommandArgument='<%#Eval("Id")%>' />
                                
</ItemTemplate>

<ItemStyle Width="40px"></ItemStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="Delete" ShowHeader="False"><ItemTemplate>
                             <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" ToolTip="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />
                            
</ItemTemplate>

<ItemStyle Width="50px"></ItemStyle>
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
                            <asp:Label ID="lbl_GridView_SubChapter" runat="server" Text=""></asp:Label></td>
                                    <td style="padding-right: 8px; text-align: right">
                                        Created By:</td>
                                    <td style="text-align: left">
                                                                    <asp:TextBox ID="txtCreatedBy_SubChapter" runat="server" CssClass="input_box" ReadOnly="True" Width="154px" TabIndex="-1" BackColor="Gainsboro"></asp:TextBox></td>
                                    <td style="padding-right: 8px; text-align: right">
                                        Created On:</td>
                                    <td style="text-align: left">
                                                                    <asp:TextBox ID="txtCreatedOn_SubChapter" runat="server" CssClass="input_box" ReadOnly="True" Width="72px" TabIndex="-2" BackColor="Gainsboro"></asp:TextBox></td>
                                    <td style="padding-right: 8px; text-align: right">
                                        Modified By:</td>
                                    <td style="text-align: left">
                                                                    <asp:TextBox ID="txtModifiedBy_SubChapter" runat="server" CssClass="input_box" ReadOnly="True" Width="154px" TabIndex="-3" BackColor="Gainsboro"></asp:TextBox></td>
                                    <td style="padding-right: 8px; text-align: right">
                                        Modified On:</td>
                                    <td style="text-align: left">
                                                                    <asp:TextBox ID="txtModifiedOn_SubChapter" runat="server" CssClass="input_box" ReadOnly="True" Width="72px" TabIndex="-4" BackColor="Gainsboro"></asp:TextBox></td>
                                    <td style="text-align: left">
                                        <asp:Button id="btn_Print_SubChap" runat="server" CausesValidation="False" CssClass="btn"
                                            OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_pnl_SubChapter');"
                                            tabIndex="7" Text="Print" Width="59px" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>&nbsp;
               </asp:Panel>
              </fieldset>                                
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                    FilterType="Custom,Numbers" ValidChars="." TargetControlID="txtSubChapterNo">
                </ajaxToolkit:FilteredTextBoxExtender>
                <asp:HiddenField id="HiddenFieldGridRowCount" runat="server">
                </asp:HiddenField>
          </td>
         </tr>
        </table>&nbsp;
       </td>
      </tr>
     </table>
    </form>
</body>
</html>


