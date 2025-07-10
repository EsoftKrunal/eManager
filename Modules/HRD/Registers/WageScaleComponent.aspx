<%@ Page Language="C#" MasterPageFile="~/Modules/HRD/RegistersMasterPage.master" AutoEventWireup="true" CodeFile="WageScaleComponent.aspx.cs" Inherits="CrewOperation_WageScaleComponent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">--%>
    <div>
    <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">    
     <tr>
       <td>
    <table cellpadding="0" cellspacing="0" width="100%">
     <tr>
      <td align="center">
        <asp:Label ID="Label1" runat="server" CssClass="textregisters" Text="Wage Scale Component"></asp:Label>
     </tr> 
     <tr>
      <td style="text-align: center;">
        <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;
            padding-bottom: 10px; border-left: #8fafdb 1px solid; padding-top: 0px; border-bottom: #8fafdb 1px solid;
            text-align: center">
            <legend><strong>Wage Scale Component</strong></legend>
                        <div style="overflow-x:hidden; overflow-y:scroll; width: 100%; height: 125px">
                            <%--<div id="div-datagrid">--%>
                            <asp:GridView ID="GridView_WageScaleComponent" GridLines="Horizontal" runat="server" AutoGenerateColumns="False" Width="98%" DataKeyNames="WageScaleComponentId" OnSelectedIndexChanged="GridView_WageScaleComponent_SelectedIndexChanged" OnRowEditing="GridView_WageScaleComponent_Row_Editing" OnRowDeleting="GridView_WageScaleComponent_Row_Deleting" OnPreRender="GridView_WageScaleComponent_PreRender" OnDataBound="GridView_WageScaleComponent_DataBound" OnRowCommand="GridView_WageScaleComponent_RowCommand" >
                         <Columns>
                              <asp:CommandField ButtonType="Image" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True" HeaderText="View" >
                               <ItemStyle Width="35px" /></asp:CommandField>
                              <%--<asp:CommandField ButtonType="Image" ShowEditButton="True" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit" >
                               <ItemStyle Width="40px" /></asp:CommandField>--%>
                             <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle Width="40px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEditWageScaleComponent" CausesValidation="false" OnClick="btnEditWageScaleComponent_Click"
                                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                                    CommandArgument='<%#Eval("WageScaleComponentId")%>' />
                                                <asp:HiddenField ID="hdnWageScaleComponentId" runat="server" Value='<%#Eval("WageScaleComponentId")%>' />
                                            </ItemTemplate>
                                                                    </asp:TemplateField>
                               <asp:TemplateField HeaderText="Delete" ShowHeader="False" Visible="false"><ItemStyle Width="40px" />
                                <ItemTemplate>
                                 <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" Text="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />
                                </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Wage Head"><ItemStyle HorizontalAlign="Left" />
                                  <ItemTemplate>  
                                   <asp:Label ID="lblComponentname" runat="server" Text='<%#Eval("ComponentName")%>'></asp:Label>
                                   <asp:HiddenField ID="HiddenWageScaleComponentId" runat="server" Value='<%#Eval("WageScaleComponentId")%>' />
                                   <asp:HiddenField ID="HiddenWageScaleComponentName" runat="server" Value='<%#Eval("ComponentName")%>' />
                                  </ItemTemplate>
                               </asp:TemplateField>
                               <asp:BoundField DataField="AccountHeadName" HeaderText="Account Head" >
                                 <ItemStyle HorizontalAlign="Left" />
                               </asp:BoundField>
                               <asp:BoundField DataField="ComponentType" HeaderText="Ear./Ded." >
                                 <ItemStyle HorizontalAlign="Left" Width="100px" />
                               </asp:BoundField>
                              <%-- <asp:TemplateField HeaderText="Component Type">
                                  <ItemTemplate>
                                    <asp:DropDownList ID="ddlComponentType" runat="server" CssClass="input_box"></asp:DropDownList>
                                  </ItemTemplate>
                               </asp:TemplateField>--%>
                               <%--<asp:BoundField DataField="CreatedBy" HeaderText="Created By">
                                 <ItemStyle HorizontalAlign="Left" />
                               </asp:BoundField>
                               <asp:BoundField DataField="CreatedOn" HeaderText="Created On">
                                 <ItemStyle HorizontalAlign="Left" Width="80px" />
                               </asp:BoundField>
                               <asp:BoundField DataField="ModifiedBy" HeaderText="Modified By">
                                 <ItemStyle HorizontalAlign="Left" />
                               </asp:BoundField>
                               <asp:BoundField DataField="ModifiedOn" HeaderText="Modified On">
                                 <ItemStyle HorizontalAlign="Left" Width="80px" />
                               </asp:BoundField>--%>
                               <%-- <asp:BoundField DataField="StatusName" HeaderText="Status">
                                 <ItemStyle HorizontalAlign="Left" Width="80px" />
                               </asp:BoundField>--%>
                         </Columns>
                              <RowStyle CssClass="rowstyle" />
                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                <PagerStyle CssClass="pagerstyle" />
                                                <HeaderStyle CssClass="headerstylefixedheadergrid"/>
                     </asp:GridView>
                   </div>
                  <asp:Label id="lbl_GridView_WageScaleComponent" runat="server" Text=""></asp:Label>
             </fieldset>
                 </td>
         </tr>
        <tr>
                    <td>

<asp:Label ID="lbl_WageScaleComponent_Message" runat="server" ForeColor="#C00000"></asp:Label>
        </td>
        </tr>
        <tr>
            <td>
                     <asp:Panel ID="pnl_WageScaleComponent" runat="server" Width="100%" Visible="False">
              <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="">
                <legend><strong>Wage Scale Component</strong></legend>
                  <table border="0" cellpadding="0" cellspacing="0" style="height: 100px; text-align: center" width="100%">
                    <tr>
                      <td colspan="4">
                          <asp:HiddenField ID="HiddenWageScaleComponent" runat="server" />
                      </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                              &nbsp;
                          </td>
                          <td align="right" style="text-align: right">
                          </td>
                          <td style="text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px">
                              Wage Head:</td>
                          <td style="text-align: left">
                          <asp:TextBox ID="txtComponentName" runat="server" CssClass="required_box" Width="240px" MaxLength="49" TabIndex="1"></asp:TextBox></td>
                          <td align="right" style="text-align: right; padding-right:15px;">
                              Ear./Other Ear./Ded./Other Ded.:</td>
                          <td style="text-align: left">
                              <asp:DropDownList ID="ddl_ComponentType" runat="server" CssClass="required_box" Width="245px" TabIndex="2" >
                                  <asp:ListItem Value="0">&lt;Select&gt;</asp:ListItem>
                                  <asp:ListItem Value="E">Earning</asp:ListItem>
                                  <asp:ListItem Value="D">Deduction</asp:ListItem>
                                  <asp:ListItem Value="OE">Other Earning</asp:ListItem>
                                  <asp:ListItem Value="OD">Other Deduction</asp:ListItem>
                              </asp:DropDownList></td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right"></td>
                          <td style="text-align: left">
                              &nbsp;
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtComponentName"
                                  ErrorMessage="Required."></asp:RequiredFieldValidator></td>
                          <td align="right" style="text-align: right">
                          </td>
                          <td style="text-align: left">
                              <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddl_ComponentType"
                                  ErrorMessage="Required." Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator></td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">
                              Account Head:</td>
                          <td style="text-align: left">
                              <asp:DropDownList ID="ddl_Account" runat="server" CssClass="input_box" TabIndex="3" Width="245px">
                              </asp:DropDownList></td>
                          <td align="right" style="text-align: right">
                          </td>
                          <td style="text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                              &nbsp;
                              </td>
                          <td align="right" style="text-align: right">
                          </td>
                          <td style="text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px">Created By:</td>
                          <td style="text-align: left">
                            <asp:TextBox ID="txtCreatedBy_WageScaleComponent" runat="server" CssClass="input_box" ReadOnly="True" Width="240px"></asp:TextBox></td>
                          <td align="right" style="text-align: right; padding-right:15px">Created On:</td>
                          <td style="text-align: left">
                            <asp:TextBox ID="txtCreatedOn_WageScaleComponent" runat="server" CssClass="input_box" ReadOnly="True" Width="240px"></asp:TextBox></td>
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
                          <td align="right" style="text-align: right; padding-right:15px">Modified By:</td>
                          <td style="text-align: left">
                            <asp:TextBox ID="txtModifiedBy_WageScaleComponent" runat="server" CssClass="input_box" ReadOnly="True" Width="240px"></asp:TextBox></td>
                          <td align="right" style="text-align: right; padding-right:15px">Modified On:</td>
                          <td style="text-align: left">
                            <asp:TextBox ID="txtModifiedOn_WageScaleComponent" runat="server" CssClass="input_box" ReadOnly="True" Width="240px"></asp:TextBox></td>
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
                      <tr runat="server" visible="false">
                          <td align="right" style="text-align: right; padding-right:15px">
                              Status:</td>
                          <td style="text-align: left">
                              <asp:DropDownList ID="ddlStatus_WageScaleComponent" runat="server" CssClass="input_box" TabIndex="3" Width="245px">
                              </asp:DropDownList></td>
                          <td align="right" style="text-align: right">
                          </td>
                          <td style="text-align: left">
                          </td>
                      </tr>
                  </table>
                  <br />
                 </fieldset>
              </asp:Panel>
            </td>
        </tr>
       <tr>
           <td>
               <table style="width: 100%">
              <tr>
                  <td style="text-align: right;">
                      <asp:Button ID="btn_Add" runat="server" CssClass="btn" OnClick="btn_Add_Click" Text="Add"
                          Width="59px" CausesValidation="False" TabIndex="4" Visible="False" />
                      <asp:Button ID="btn_Save" runat="server" CssClass="btn" Text="Save" Width="59px" OnClick="btn_Save_Click" TabIndex="5" />
                      <asp:Button ID="btn_Cancel" runat="server" CssClass="btn" OnClick="btn_Cancel_Click"
                          Text="Cancel" Width="59px" CausesValidation="False" TabIndex="6" />
                      <asp:Button ID="btn_Print" runat="server" CssClass="btn" OnClick="btn_Print_Click"
                          Text="Print" Width="59px" CausesValidation="False" OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_pnl_WageScaleComponent');" Visible="False" TabIndex="7" /></td>
              </tr>
          </table>
           </td>
       </tr>

          
      </td>
     </tr>
    </table> 
   </div>
 </asp:Content>
    <%--</form>
</body>
</html>--%>

<%--OnDataBound="GridView_WageScaleComponent_DataBound"    OnPreRender="GridView_WageScaleComponent_PreRender" 
                                 OnRowDeleting="GridView_WageScaleComponent_Row_Deleting"
                                OnRowEditing="GridView_WageScaleComponent_Row_Editing" OnSelectedIndexChanged="GridView_WageScaleComponent_SelectedIndexChanged"--%>
