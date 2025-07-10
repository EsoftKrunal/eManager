<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="ChaptersEntry.aspx.cs" Inherits="Registers_ChaptersEntry"  %>
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
           <asp:Label ID="Label1" runat="server" CssClass="textregisters" Text="Chapters Entry"></asp:Label></td>
    </tr> --%>
          <tr>
            <td style="text-align: center;">
            <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 0px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; " class="">
                <%--<legend><strong>Main Chapter</strong></legend>--%>
            <asp:Panel ID="pnl_ChaptersEntry" runat="server" Width="100%">
                  <table border="0" cellpadding="0" cellspacing="0" style="text-align: center" width="100%">
                    <tr>
                      <td colspan="2" style="height: 15px">
                          <asp:HiddenField ID="HiddenChaptersEntry" runat="server" />
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
                              Chapter#:</td>
                          <td style="text-align: left">
                              <asp:TextBox ID="txtChapterNo" runat="server" CssClass="input_box" MaxLength="10"
                                  TabIndex="2" Width="510px" Enabled="False"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidatorAccountHeadName" runat="server"
                                  ControlToValidate="txtChapterNo" ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                          <td style="text-align: left">
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
                              Chapter Name:</td>
                          <td style="text-align: left;">
                              <asp:TextBox ID="txtChapterName" runat="server" CssClass="input_box" MaxLength="49"
                                  TabIndex="3" Width="510px" Enabled="False"></asp:TextBox></td>
                          <td style="padding-right: 17px; text-align: right">
                       <asp:Button ID="btn_Cancel_ChaptersEntry" runat="server" CausesValidation="False" CssClass="btn" TabIndex="4" Text="Cancel" Visible="False" Width="59px" OnClick="btn_Cancel_ChaptersEntry_Click" />
                              <asp:Button ID="btn_New_ChaptersEntry" runat="server" CssClass="btn" Text="New" Width="59px" CausesValidation="False" TabIndex="5" OnClick="btn_New_ChaptersEntry_Click" />
                              <asp:Button ID="btn_Save_ChaptersEntry" runat="server" CssClass="btn" Text="Save" Width="59px" TabIndex="6" OnClick="btn_Save_ChaptersEntry_Click" Enabled="False" /></td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right; width: 287px; height: 3px;">
                          </td>
                          <td style="text-align: left; height: 3px;">
                              </td>
                          <td style="height: 3px; text-align: left">
                          </td>
                      </tr>
                                                        </table>
                                                    
           <table style="width: 100%" cellpadding="0" cellspacing="0">
               <tr>
                   <td style="text-align: right; padding-right: 17px;">
                    <div id="Div1" runat="server" style="position: relative; top: 0; text-align: center; left: 0"><asp:Label ID="lbl_ChaptersEntry_Message" runat="server" ForeColor="#C00000"></asp:Label></div>
                       </td>
               </tr>
           </table>
           
           <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="padding-right: 5px; padding-left: 5px">
                    <div style="width: 100%; height: 260px">
                       <asp:GridView ID="GridView_ChaptersEntry" runat="server" AutoGenerateColumns="False" Width="98%" GridLines="Both" OnPreRender="GridView_ChaptersEntry_PreRender" OnRowDataBound="GridView_ChaptersEntry_RowDataBound" OnRowDeleting="GridView_ChaptersEntry_RowDeleting" OnRowCreated="GridView_ChaptersEntry_RowCreated" OnPageIndexChanging="GridView_ChaptersEntry_PageIndexChanging" AllowPaging="True" PageSize="9" OnRowCancelingEdit="GridView_ChaptersEntry_RowCancelingEdit" OnRowCommand="GridView_ChaptersEntry_RowCommand" >
                              <RowStyle CssClass="rowstyle" />
                         <Columns>
<asp:TemplateField HeaderText="Chapter#"><ItemTemplate>
                                <asp:Label ID="lbl_ChapterNo" runat="server" Text='<%# Eval("ChapterNo") %>'></asp:Label>
                                <asp:HiddenField ID="Hidden_ChapterId" runat="server" Value='<%# Eval("Id") %>' />
                                <asp:HiddenField ID="Hidden_ChapterNo" runat="server" Value='<%# Eval("ChapterNo") %>' />
                                <asp:HiddenField ID="Hidden_InspGrp" runat="server" Value='<%# Eval("InspectionGroup") %>' />
                            
</ItemTemplate>

<ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
</asp:TemplateField>
<asp:BoundField DataField="ChapterName" HeaderText="Chapter Name">
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
                             <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Images/delete.jpg" ToolTip="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />
                            
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
                     <asp:Label ID="lbl_GridView_ChaptersEntry" runat="server" Text=""></asp:Label></td>
                                    <td style="padding-right: 8px; text-align: right">
                                        Created By:</td>
                                    <td style="text-align: left">
                                                                    <asp:TextBox ID="txtCreatedBy_ChaptersEntry" runat="server" CssClass="input_box" ReadOnly="True" Width="154px" TabIndex="-1" BackColor="Gainsboro"></asp:TextBox></td>
                                    <td style="padding-right: 8px; text-align: right">
                                        Created On:</td>
                                    <td style="text-align: left">
                                                                    <asp:TextBox ID="txtCreatedOn_ChaptersEntry" runat="server" CssClass="input_box" ReadOnly="True" Width="72px" TabIndex="-2" BackColor="Gainsboro"></asp:TextBox></td>
                                    <td style="padding-right: 8px; text-align: right">
                                        Modified By:</td>
                                    <td style="text-align: left">
                                                                    <asp:TextBox ID="txtModifiedBy_ChaptersEntry" runat="server" CssClass="input_box" ReadOnly="True" Width="154px" TabIndex="-3" BackColor="Gainsboro"></asp:TextBox></td>
                                    <td style="padding-right: 8px; text-align: right">
                                        Modified On:</td>
                                    <td style="text-align: left">
                                                                    <asp:TextBox ID="txtModifiedOn_ChaptersEntry" runat="server" CssClass="input_box" ReadOnly="True" Width="72px" TabIndex="-4" BackColor="Gainsboro"></asp:TextBox></td>
                                    <td style="text-align: left">
                                        <asp:Button id="btn_Print_ChpEntry" runat="server" CausesValidation="False" CssClass="btn"
                                            OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_pnl_ChaptersEntry');"
                                            tabIndex="7" Text="Print" Width="59px" /></td>
                                </tr>
                            </table>
                        </td>
               </tr>
           </table>&nbsp;
           </asp:Panel>
           </fieldset>                               
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                    FilterType="Numbers" TargetControlID="txtChapterNo">
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

