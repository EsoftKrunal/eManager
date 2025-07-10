<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Currency.aspx.cs" Inherits="Registers_Currency" MasterPageFile="~/Modules/HRD/RegistersMasterPage.master"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">--%>
    <div style="text-align: center">
        <%-- <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>--%>
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
    <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0">    
     <tr>
       <td>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
       <td>
           <asp:Label ID="Label1" runat="server" CssClass="textregisters" Text="Currency "></asp:Label></td>
    </tr> 
          <tr>
            <td style="text-align: center;">
              <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; padding-top:0px; padding-bottom:10px">
                 <legend><strong>Currency List</strong></legend>
                      <div style="overflow-x: hidden; overflow-y:scroll; width: 100%; height: 150px">
                       <asp:GridView ID="GridView_Currency" GridLines="Horizontal" runat="server" AutoGenerateColumns="False" Width="98%" DataKeyNames="CurrencyId" OnSelectedIndexChanged="GridView_Currency_SelectedIndexChanged" OnRowEditing="GridView_Currency_Row_Editing" OnRowDeleting="GridView_Currency_Row_Deleting" OnPreRender="GridView_Currency_PreRender" OnDataBound="GridView_Currency_DataBound" OnRowCommand="GridView_Currency_RowCommand" >
                         <Columns>
                              <asp:CommandField ButtonType="Image" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True" HeaderText="View" >
                               <ItemStyle Width="30px"/></asp:CommandField>
                              <%--<asp:CommandField ButtonType="Image" ShowEditButton="True" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit" >
                               <ItemStyle Width="35px" /></asp:CommandField>--%>
                               <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle Width="40px" />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEditCurrency" CausesValidation="false" OnClick="btnEditCurrcency_click"
                                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                                    CommandArgument='<%#Eval("CurrencyId")%>' />
                                                <asp:HiddenField ID="hdnCurrencyId" runat="server" Value='<%#Eval("CurrencyId")%>' />
                                            </ItemTemplate>
                                                                    </asp:TemplateField>
                               <asp:TemplateField HeaderText="Delete" ShowHeader="False"><ItemStyle Width="35px" />
                                <ItemTemplate>
                                 <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" Text="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />
                                </ItemTemplate>
                               </asp:TemplateField>
                             
                               <asp:TemplateField HeaderText="Currency"><ItemStyle HorizontalAlign="Left" Width="200px" />
                                  <ItemTemplate>  
                                   <asp:Label ID="lblcurrencyname" runat="server" Text='<%#Eval("CurrencyName")%>'></asp:Label>
                                   <asp:HiddenField ID="HiddenCurrencyId" runat="server" Value='<%#Eval("CurrencyId")%>' />
                                   <asp:HiddenField ID="HiddenCurrencyName" runat="server" Value='<%#Eval("CurrencyName")%>' />
                                  </ItemTemplate>
                               </asp:TemplateField>
                               
                                 
                                <asp:BoundField DataField="ExchangeRate" HeaderText="Exchange Rate" DataFormatString="{0:0.00}" HtmlEncode="False">
                                 <ItemStyle HorizontalAlign="Left" Width="150px" />
                               </asp:BoundField>
                               
                              <%-- <asp:BoundField DataField="CreatedBy" HeaderText="Created By">
                                 <ItemStyle HorizontalAlign="Left" />
                               </asp:BoundField>
                               <asp:BoundField DataField="CreatedOn" HeaderText="Created On">
                                 <ItemStyle HorizontalAlign="Left" Width="100px" />
                               </asp:BoundField>
                               <asp:BoundField DataField="ModifiedBy" HeaderText="Modified By">
                                 <ItemStyle HorizontalAlign="Left" />
                               </asp:BoundField>
                               <asp:BoundField DataField="ModifiedOn" HeaderText="Modified On">
                                 <ItemStyle HorizontalAlign="Left" Width="100px" />
                               </asp:BoundField>--%>
                                <asp:BoundField DataField="StatusName" HeaderText="Status">
                                 <ItemStyle HorizontalAlign="Left" Width="70px" />
                               </asp:BoundField>
                         </Columns>
                            <%-- <SelectedRowStyle CssClass="selectedtowstyle" />
                             <PagerStyle CssClass="pagerstyle" />
                             <HeaderStyle CssClass="headerstyle" HorizontalAlign="Left" />
                             <AlternatingRowStyle CssClass="alternatingrowstyle" />--%>
                              <RowStyle CssClass="rowstyle" />
                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                <PagerStyle CssClass="pagerstyle" />
                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                     </asp:GridView>
                     </div>
                        <asp:Label ID="lbl_GridView_Currency" runat="server" Text=""></asp:Label><asp:Label ID="lbl_Currency_Message" runat="server" ForeColor="#C00000"></asp:Label>
                        </fieldset>
                <asp:Panel ID="pnl_Currency" runat="server" Width="100%" Visible="False">
              <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="">
                <legend><strong>Currency Details</strong></legend>
                  <table border="0" cellpadding="0" cellspacing="0" style="height: 100px; text-align: center" width="100%">
                    <tr>
                      <td colspan="4">
                          <asp:HiddenField ID="HiddenCurrency" runat="server" />
                                  <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtExchangeRate"
           FilterType="Numbers,Custom" ValidChars="." />
                       </td>
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
                              Currency:</td>
                          <td style="text-align: left">
                              <asp:TextBox ID="txtCurrencyName" runat="server" CssClass="required_box" MaxLength="29"
                                  Width="200px" TabIndex="2"></asp:TextBox></td>
                          <td align="right" style="text-align: right; padding-right:15px">
                              Exchange Rate:</td>
                          <td style="text-align: left">
                              <asp:TextBox ID="txtExchangeRate" runat="server" CssClass="required_box"
                                  Width="200px" MaxLength="6" TabIndex="3"></asp:TextBox></td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right; height: 13px;">
                          </td>
                          <td style="text-align: left; height: 13px;">
                              &nbsp;
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                  ControlToValidate="txtCurrencyName" ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                          <td align="right" style="padding-right: 15px; text-align: right; height: 13px;">
                              &nbsp;</td>
                          <td style="text-align: left; height: 13px;">
                              &nbsp;
                              <asp:RequiredFieldValidator
                                      ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtExchangeRate"
                                      ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                      </tr>
                                                            <tr>
                                                                <td align="right" style="text-align: right; padding-right:15px">
                                                                    Created By:</td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtCreatedBy_Currency" runat="server" CssClass="input_box" ReadOnly="True" Width="200px" TabIndex="-1"></asp:TextBox></td>
                                                                <td align="right" style=" text-align: right; padding-right:15px">
                                                                    Created On:</td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtCreatedOn_Currency" runat="server" CssClass="input_box" ReadOnly="True" Width="200px" TabIndex="-1"></asp:TextBox></td>
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
                                                                    <asp:TextBox ID="txtModifiedBy_Currency" runat="server" CssClass="input_box" ReadOnly="True" Width="200px" TabIndex="-1"></asp:TextBox></td>
                                                                <td align="right" style=" text-align: right; padding-right:15px">
                                                                    Modified On:</td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtModifiedOn_Currency" runat="server" CssClass="input_box" ReadOnly="True" Width="200px" TabIndex="-1"></asp:TextBox></td>
                                                            </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                              &nbsp; &nbsp;</td>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style=" text-align: right; padding-right:15px">
                              Status:</td>
                          <td style="text-align: left">
                              <asp:DropDownList ID="ddlStatus_Currency" runat="server" CssClass="input_box" Width="205px" TabIndex="4">
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
                       <asp:Button ID="btn_Add_Currency" runat="server" CssClass="btn" 
                    Text="Add" Width="59px" OnClick="btn_Add_Currency_Click" CausesValidation="False" TabIndex="1" />
                <asp:Button ID="btn_Save_Currency" runat="server" CssClass="btn" 
                    Text="Save" Width="59px" OnClick="btn_Save_Currency_Click" TabIndex="5" />
                <asp:Button ID="btn_Cancel_Currency" runat="server" CssClass="btn" 
                    Text="Cancel" Width="59px" OnClick="btn_Cancel_Currency_Click" CausesValidation="False" TabIndex="6" />
                      <asp:Button ID="btn_Print_Currency" runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_Print_Currency_Click" TabIndex="7" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_pnl_Currency');" Visible="False" />
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
