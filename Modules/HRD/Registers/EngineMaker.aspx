<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EngineMaker.aspx.cs" Inherits="Registers_EngineMaker" MasterPageFile="~/Modules/HRD/RegistersMasterPage.master" %>
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
      <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">  
         <tr>
       <td>
           <asp:Label ID="Label1" runat="server" CssClass="textregisters" Text="Engine Maker"></asp:Label></td>
    </tr>   
     <tr>
       <td>
        <table cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="text-align: center;">
              <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; padding-top:0px; padding-bottom:10px">
                 <legend><strong>Engine Maker List</strong></legend>
                    <table cellpadding="0" cellspacing="0" width="100%">
                     <tr>
                      <td style=" padding-top:5px;">
                      <div style="overflow-x: hidden; overflow-y:scroll; width: 100%; height: 150px">
                       <asp:GridView ID="GridView_EngineMaker" runat="server" AutoGenerateColumns="False" Width="98%" DataKeyNames="EngineMakerId" OnSelectedIndexChanged="GridView_EngineMaker_SelectedIndexChanged" OnRowEditing="GridView_EngineMaker_Row_Editing" OnRowDeleting="GridView_EngineMaker_Row_Deleting" OnPreRender="GridView_EngineMaker_PreRender" OnDataBound="GridView_EngineMaker_DataBound" GridLines="Horizontal" OnRowCommand="GridView_EngineMaker_RowCommand" >
                         <Columns>
                              <asp:CommandField ButtonType="Image" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True" HeaderText="View" >
                               <ItemStyle Width="35px" /></asp:CommandField>
                             <%-- <asp:CommandField ButtonType="Image" ShowEditButton="True" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit" >
                               <ItemStyle Width="40px" /></asp:CommandField>--%>
                             <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle Width="40px" />
                                    <ItemTemplate>
                                    <asp:ImageButton ID="btnEditEngineMaker" CausesValidation="false" OnClick="btnEditEngineMaker_click"
                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                    CommandArgument='<%#Eval("EngineMakerId")%>' />
                                    <asp:HiddenField ID="hdnEngineMakerId" runat="server" Value='<%#Eval("EngineMakerId")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="Delete" ShowHeader="False"><ItemStyle Width="40px" />
                                <ItemTemplate>
                                 <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" Text="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />
                                </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Engine Design"><ItemStyle HorizontalAlign="Left" />
                                  <ItemTemplate>  
                                   <asp:Label ID="lblEngineMakername" runat="server" Text='<%#Eval("EngineMakerName")%>'></asp:Label>
                                   <asp:HiddenField ID="HiddenEngineMakerId" runat="server" Value='<%#Eval("EngineMakerId")%>' />
                                   <asp:HiddenField ID="HiddenEngineMakername" runat="server" Value='<%#Eval("EngineMakerName")%>' />
                                  </ItemTemplate>
                               </asp:TemplateField>
                               <asp:BoundField DataField="EngineMakerType" HeaderText="Engine Maker Type" >
                                 <ItemStyle HorizontalAlign="Left" />
                               </asp:BoundField>
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
                            <RowStyle CssClass="rowstyle" />
                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                <PagerStyle CssClass="pagerstyle" />
                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                     </asp:GridView>
                     </div>
                        <asp:Label ID="lbl_GridView_EngineMaker" runat="server" Text=""></asp:Label>
                  </td>
                 </tr>
                </table>        
                <asp:Label ID="lbl_EngineMaker_Message" runat="server" ForeColor="#C00000"></asp:Label></fieldset>
                &nbsp;<br />
            <asp:Panel ID="pnl_EngineMaker" runat="server" Width="100%" Visible="False">
              <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="">
                <legend><strong>Engine Maker Details</strong></legend>
                  <table border="0" cellpadding="0" cellspacing="0" style="height: 100px; text-align: center" width="100%">
                    <tr>
                      <td colspan="4">
                          <asp:HiddenField ID="HiddenEngineMaker" runat="server" />
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
                                                                    Engine Maker Name:</td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtEngineMakerName" runat="server" CssClass="required_box" Width="200px" MaxLength="49" TabIndex="1"></asp:TextBox></td>
                                                                <td align="right" style="text-align: right; padding-right: 15px;">
                                                                    Engine Maker Type:</td>
                                                                <td style="text-align: left"><asp:DropDownList ID="ddlEngineMakerType" runat="server" CssClass="required_box" TabIndex="2" Width="205px">
                                                                    <asp:ListItem Value="0">&lt;Select&gt;</asp:ListItem>
                                                                    <asp:ListItem Value="1">Main Engine</asp:ListItem>
                                                                    <asp:ListItem Value="2">Aux1 Engine</asp:ListItem>
                                                                    <asp:ListItem Value="3">Aux2 Engine</asp:ListItem>
                                                                    <asp:ListItem Value="4">Aux3 Engine</asp:ListItem>
                                                                </asp:DropDownList></td>
                                                            </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right; height: 13px;">
                          </td>
                          <td style="text-align: left; height: 13px;">
                              &nbsp;
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEngineMakerName"
                                  ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                          <td align="right" style="text-align: right; height: 13px;">
                          </td>
                          <td style="text-align: left; height: 13px;">
                              &nbsp;
                              <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="ddlEngineMakerType"
                                  ErrorMessage="Required" MaximumValue="1000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                      </tr>
                                                            <tr>
                                                                <td align="right" style="text-align: right; padding-right:15px">
                                                                    Created By:</td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtCreatedBy_EngineMaker" runat="server" CssClass="input_box" ReadOnly="True" Width="200px"></asp:TextBox></td>
                                                                <td align="right" style="text-align: right; padding-right:15px">
                                                                    Created On:</td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtCreatedOn_EngineMaker" runat="server" CssClass="input_box" ReadOnly="True" Width="200px"></asp:TextBox></td>
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
                                                                    <asp:TextBox ID="txtModifiedBy_EngineMaker" runat="server" CssClass="input_box" ReadOnly="True" Width="200px"></asp:TextBox></td>
                                                                <td align="right" style="text-align: right; padding-right:15px">
                                                                    Modified On:</td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtModifiedOn_EngineMaker" runat="server" CssClass="input_box" ReadOnly="True" Width="200px"></asp:TextBox></td>
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
                              <asp:DropDownList ID="ddlStatus_EngineMaker" runat="server" CssClass="input_box" TabIndex="3" Width="205px">
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
                       <asp:Button ID="btn_Add_EngineMaker" runat="server" CssClass="btn" 
                    Text="Add" Width="59px" OnClick="btn_Add_EngineMaker_Click" CausesValidation="False" TabIndex="4" />
                            <asp:Button ID="btn_Save_EngineMaker" runat="server" CssClass="btn" 
                    Text="Save" Width="59px" OnClick="btn_Save_EngineMaker_Click" TabIndex="5" />
                            <asp:Button ID="btn_Cancel_EngineMaker" runat="server" CssClass="btn" 
                    Text="Cancel" Width="59px" OnClick="btn_Cancel_EngineMaker_Click" CausesValidation="False" TabIndex="6" />
                            <asp:Button ID="btn_Print_EngineMaker" runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_Print_EngineMaker_Click" TabIndex="7" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_pnl_EngineMaker');" Visible="False" />                
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
   <%-- </form>
</body>
</html>--%>
