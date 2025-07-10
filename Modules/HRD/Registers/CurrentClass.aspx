<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CurrentClass.aspx.cs" Inherits="Registers_CurrentClass" MasterPageFile="~/Modules/HRD/RegistersMasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">--%>
    <div style="text-align: center">
   <%-- <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
    <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">    
     <tr>
       <td>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
       <td>
           <asp:Label ID="Label1" runat="server" CssClass="textregisters" Text="Current Class "></asp:Label></td>
    </tr> 
          <tr>
            <td style="text-align: center;">
              <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; padding-top:0px; padding-bottom:10px">
                 <legend><strong>Current Class List</strong></legend>
                      <div style="overflow-x: hidden; overflow-y:scroll; width: 100%; height: 150px">
                       <asp:GridView ID="GridView_CurrentClass"  GridLines="Horizontal" runat="server" AutoGenerateColumns="False" Width="98%" DataKeyNames="CurrentClassId" OnSelectedIndexChanged="GridView_CurrentClass_SelectedIndexChanged" OnRowEditing="GridView_CurrentClass_Row_Editing" OnRowDeleting="GridView_CurrentClass_Row_Deleting" OnPreRender="GridView_CurrentClass_PreRender" OnDataBound="GridView_CurrentClass_DataBound" OnRowCommand="GridView_CurrentClass_RowCommand" >
                         <Columns>
                              <asp:CommandField ButtonType="Image" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True" HeaderText="View" >
                               <ItemStyle Width="35px" /></asp:CommandField>
                              <%--<asp:CommandField ButtonType="Image" ShowEditButton="True" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit" >
                               <ItemStyle Width="40px" /></asp:CommandField>--%>
                             <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle Width="40px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEditCurrentClass" CausesValidation="false" OnClick="btnEditCurrentClass_click"
                                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                                    CommandArgument='<%#Eval("CurrentClassId")%>' />
                                                <asp:HiddenField ID="hdnCurrentClassId" runat="server" Value='<%#Eval("CurrentClassId")%>' />
                                            </ItemTemplate>
                                                                    </asp:TemplateField>
                               <asp:TemplateField HeaderText="Delete" ShowHeader="False"><ItemStyle Width="40px" />
                                <ItemTemplate>
                                 <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" Text="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />
                                </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Current Class"><ItemStyle HorizontalAlign="Left" />
                                  <ItemTemplate>  
                                   <asp:Label ID="lblCurrentClassname" runat="server" Text='<%#Eval("CurrentClassName")%>'></asp:Label>
                                   <asp:HiddenField ID="HiddenCurrentClassId" runat="server" Value='<%#Eval("CurrentClassId")%>' />
                                   <asp:HiddenField ID="HiddenCurrentClassName" runat="server" Value='<%#Eval("CurrentClassName")%>' />
                                  </ItemTemplate>
                               </asp:TemplateField>
                              <%-- <asp:BoundField DataField="CreatedBy" HeaderText="Created By">
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
                                <asp:BoundField DataField="StatusName" HeaderText="Status">
                                 <ItemStyle HorizontalAlign="Left" Width="80px" />
                               </asp:BoundField>
                         </Columns>
                             <%--<SelectedRowStyle CssClass="selectedtowstyle" />
                             <PagerStyle CssClass="pagerstyle" />
                             <HeaderStyle CssClass="headerstyle" HorizontalAlign="Left" />
                             <AlternatingRowStyle CssClass="alternatingrowstyle" />--%>
                              <RowStyle CssClass="rowstyle" />
                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                <PagerStyle CssClass="pagerstyle" />
                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                     </asp:GridView>
                       </div>
                       <asp:Label ID="lbl_GridView_CurrentClass" runat="server" Text=""></asp:Label><asp:Label ID="lbl_CurrentClass_Message" runat="server" ForeColor="#C00000"></asp:Label></fieldset>
            <asp:Panel ID="pnl_CurrentClass" runat="server" Width="100%" Visible="False">
              <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="">
                <legend><strong>Current Class Details</strong></legend>
                  <table border="0" cellpadding="0" cellspacing="0" style="height: 100px; text-align: center" width="100%">
                    <tr>
                      <td colspan="4">
                          <asp:HiddenField ID="HiddenCurrentClass" runat="server" />
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
                                                                    Current Class:</td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtCurrentClassName" runat="server" CssClass="required_box" Width="200px" MaxLength="49" TabIndex="1"></asp:TextBox></td>
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
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCurrentClassName"
                                  ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                          <td align="right" style="text-align: right">
                          </td>
                          <td style="text-align: left">
                          </td>
                      </tr>
                                                            <tr>
                                                                <td align="right" style="text-align: right; padding-right:15px">
                                                                    Created By:</td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtCreatedBy_CurrentClass" runat="server" CssClass="input_box" ReadOnly="True" Width="200px"></asp:TextBox></td>
                                                                <td align="right" style="text-align: right; padding-right:15px">
                                                                    Created On:</td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtCreatedOn_CurrentClass" runat="server" CssClass="input_box" ReadOnly="True" Width="200px"></asp:TextBox></td>
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
                                                                    <asp:TextBox ID="txtModifiedBy_CurrentClass" runat="server" CssClass="input_box" ReadOnly="True" Width="200px"></asp:TextBox></td>
                                                                <td align="right" style="text-align: right; padding-right:15px">
                                                                    Modified On:</td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtModifiedOn_CurrentClass" runat="server" CssClass="input_box" ReadOnly="True" Width="200px"></asp:TextBox></td>
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
                              <asp:DropDownList ID="ddlStatus_CurrentClass" runat="server" CssClass="input_box" TabIndex="2" Width="205px">
                              </asp:DropDownList></td>
                          <td align="right" style="text-align: right">
                          </td>
                          <td style="text-align: left">
                          </td>
                      </tr>
                                                        </table>
                  <br />
                                                    </fieldset>
                <br />
                                                    </asp:Panel>
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: right">
                       <asp:Button ID="btn_Add_CurrentClass" runat="server" CssClass="btn" 
                    Text="Add" Width="59px" OnClick="btn_Add_CurrentClass_Click" CausesValidation="False" TabIndex="3" />
                            <asp:Button ID="btn_Save_CurrentClass" runat="server" CssClass="btn" 
                    Text="Save" Width="59px" OnClick="btn_Save_CurrentClass_Click" TabIndex="4" />
                            <asp:Button ID="btn_Cancel_CurrentClass" runat="server" CssClass="btn" 
                    Text="Cancel" Width="59px" OnClick="btn_Cancel_CurrentClass_Click" CausesValidation="False" TabIndex="5" />
                            <asp:Button ID="btn_Print_CurrentClass" runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_Print_CurrentClass_Click" TabIndex="6" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_pnl_CurrentClass');" Visible="False" />                
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
    <%--</form>
</body>
</html>--%>
