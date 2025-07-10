<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="InspectionGroup.aspx.cs" Inherits="Registers_InspectionGroup" EnableEventValidation="false" %>
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
           <asp:Label ID="Label2" runat="server" CssClass="textregisters" Text="Inspection Group"></asp:Label></td>
    </tr> --%>
          <tr>
            <td style="text-align: center;">
                <%--<asp:Label ID="lbl_InspGrp_Message" runat="server" ForeColor="#C00000"></asp:Label><br />--%>
            <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 0px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="">
                <%--<legend><strong>Inspection Group</strong></legend>--%>
            <asp:Panel ID="pnl_InspectionGroup" runat="server" Width="100%">
              
                  <table border="0" cellpadding="0" cellspacing="0" style="text-align: center" width="100%">
                    <tr>
                      <td colspan="2" style="height: 15px">
                          <asp:HiddenField ID="HiddenInspectionGroup" runat="server" /><asp:HiddenField id="HiddenFieldGridRowCount"
                              runat="server">
                          </asp:HiddenField>
                      </td>
                                                            </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px; width: 287px;">
                              Group
                              Code:</td>
                          <td style="text-align: left;">
                                                                    <asp:TextBox ID="txtInspectionCode" runat="server" CssClass="input_box" Width="510px" MaxLength="8" TabIndex="1" Enabled="False"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtInspectionCode"
                                  ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right; height: 3px; width: 287px;">
                          </td>
                          <td style="text-align: left; height: 3px;">
                              </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right; width: 287px;">
                              Group Name:</td>
                          <td style="text-align: left">
                                                                    <asp:TextBox ID="txtInspectionGrpName" runat="server" CssClass="input_box" MaxLength="50" TabIndex="2"
                                                                        Width="510px" Enabled="False"></asp:TextBox></td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; height: 3px; text-align: right; width: 287px;">
                          </td>
                          <td style="height: 3px; text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right; width: 287px;" valign="top">
                              Inspection Type:</td>
                          <td style="text-align: left">
                            <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; width: 188px; padding-left: 20px; text-align: left;">
                                <table cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 100px">
                                        </td>
                                        <td style="width: 100px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px">
                              <asp:RadioButton
                                  ID="rdInspGrpInternal" runat="server" Text="Internal" GroupName="tt" TabIndex="4" Enabled="False" Checked="True" /></td>
                                        <td style="width: 100px">
                              <asp:RadioButton ID="rdInspGrpExternal" runat="server" Text="External" GroupName="tt" TabIndex="3" Enabled="False" /></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px">
                                        </td>
                                        <td style="width: 100px">
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                          </td>
                      </tr>
                                                        </table>
              
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td style="text-align: right; padding-right:17px;">
                        <div id="Div1" runat="server" style="position: relative; top: 0; text-align: center; left:0">
                        <asp:Label ID="lbl_InspGrp_Message" runat="server" ForeColor="#C00000"></asp:Label>&nbsp;</div>
                        <%--<div id="Div2" runat="server" style="float: right; position: relative; top: 0; text-align: center; left: 0">--%>
                            <asp:Button ID="btn_Cancel_InspGrp" runat="server" CssClass="btn" Text="Cancel" Width="59px" CausesValidation="False" TabIndex="5" OnClick="btn_Cancel_InspGrp_Click" Visible="False" />
                            <asp:Button ID="btn_New_InspGrp" runat="server" CssClass="btn" Text="New" Width="59px" CausesValidation="False" TabIndex="6" OnClick="btn_New_InspGrp_Click" />
                            <asp:Button ID="btn_Save_InspGrp" runat="server" CssClass="btn" Text="Save" Width="59px" TabIndex="7" OnClick="btn_Save_InspGrp_Click" Enabled="False" /><%--<asp:Button ID="btn_Cancel_InspGrp" runat="server" CssClass="btn" Text="Cancel" Width="59px" CausesValidation="False" TabIndex="5" />
                            <asp:Button ID="btn_Print_InspGrp" runat="server" CausesValidation="False" CssClass="btn" TabIndex="6" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_pnl_FlagState');" />
                            <asp:Button ID="btn_Export_InspGrp" runat="server" CausesValidation="False" CssClass="btn" TabIndex="6" Text="Export" Width="59px" /></td>--%><%--</div>--%>
                            </td>
                            </tr>
                </table>
                
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="padding-right: 5px; padding-left: 5px">
                            <div style="width: 100%; height: 255px"> <%--height: 150px;--%>
                          <asp:GridView ID="GridView_InsGrp" runat="server" GridLines="Both" AutoGenerateColumns="False" Width="98%" OnPreRender="GridView_InsGrp_PreRender" OnRowDataBound="GridView_InsGrp_RowDataBound" OnRowDeleting="GridView_InsGrp_RowDeleting"  AllowPaging="True" OnPageIndexChanging="GridView_InsGrp_PageIndexChanging" PageSize="8" OnRowCancelingEdit="GridView_InsGrp_RowCancelingEdit" OnRowCommand="GridView_InsGrp_RowCommand">
                              <RowStyle CssClass="rowstyle" />
                         <Columns>
<asp:TemplateField HeaderText="Group Code"><ItemTemplate>
                                    <asp:Label ID="lblCode" runat="server" Text='<%# Eval("Code") %>'></asp:Label>
                                    <asp:HiddenField ID="Hidden_InspectionGroupId" runat="server" Value='<%# Eval("Id") %>' />
                                    <asp:HiddenField ID="Hidden_InspectionCode" runat="server" Value='<%# Eval("Code") %>' />
                                
</ItemTemplate>

<ItemStyle HorizontalAlign="Left" Width="120px"></ItemStyle>
</asp:TemplateField>
<asp:BoundField DataField="Name" HeaderText="Group Name">
<ItemStyle HorizontalAlign="Left"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="InspectionType" HeaderText="Inspection Type">
<ItemStyle HorizontalAlign="Left" Width="120px"></ItemStyle>
</asp:BoundField>                            
   <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle Width="45px" />
     <ItemTemplate>
                                                                            <asp:ImageButton ID="btnEditInsGroup" CausesValidation="false" OnClick="btnEditInsGroup_Click"
                                                                                ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                                                                CommandArgument='<%#Eval("Id")%>' />
                                                                            <asp:HiddenField ID="hdnInsGroupId" runat="server" Value='<%#Eval("Id")%>' />
                                                                        </ItemTemplate>
                                                                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Delete" ShowHeader="False">
    <ItemTemplate>
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
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-right: 7px; padding-left: 16px; text-align: center">
                            <table cellpadding="0" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td style="padding-right: 8px; text-align: left">
                        <asp:Label ID="lbl_GridView_InspGrp" runat="server" Text=""></asp:Label></td>
                                    <td style="padding-right: 8px; text-align: right;">
                                        Created By:</td>
                                    <td style="text-align: left;">
                                                                    <asp:TextBox ID="txtCreatedBy_InspGrp" runat="server" CssClass="input_box" ReadOnly="True" TabIndex="-1" BackColor="Gainsboro" Width="154px"></asp:TextBox></td>
                                    <td style="padding-right: 8px; text-align: right;">
                                        Created On:</td>
                                    <td style="text-align: left;">
                                                                    <asp:TextBox ID="txtCreatedOn_InspGrp" runat="server" CssClass="input_box" ReadOnly="True" TabIndex="-2" BackColor="Gainsboro" Width="72px"></asp:TextBox></td>
                                    <td style="padding-right: 8px; text-align: right;">
                                        Modified By:</td>
                                    <td style="text-align: left;">
                                                                    <asp:TextBox ID="txtModifiedBy_InspGrp" runat="server" CssClass="input_box" ReadOnly="True" TabIndex="-3" BackColor="Gainsboro" Width="154px"></asp:TextBox></td>
                                    <td style="padding-right: 8px; text-align: right;">
                                        Modified On:</td>
                                    <td style="text-align: left;">
                                                                    <asp:TextBox ID="txtModifiedOn_InspGrp" runat="server" CssClass="input_box" ReadOnly="True" TabIndex="-4" BackColor="Gainsboro" Width="72px"></asp:TextBox></td>
                                    <td style="text-align: left">
                            <asp:Button ID="btn_Print_InspGrp" runat="server" CausesValidation="False" CssClass="btn" TabIndex="8" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('pnl_InspectionGroup');" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                &nbsp;
           </asp:Panel></fieldset>                              
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

