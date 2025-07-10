<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Owner.aspx.cs" Inherits="Registers_Owner" MasterPageFile="~/Modules/HRD/RegistersMasterPage.master" %>
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
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
       <td align="center">
           <asp:Label ID="Label1" runat="server" CssClass="textregisters" Text="Owner"></asp:Label></td>
    </tr> 
          <tr>
            <td style="text-align: center;">
              <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; padding-top:0px; padding-bottom:10px">
                 <legend><strong>Owner List</strong></legend>
                      <div style="overflow-x:hidden; overflow-y:scroll; width: 100%; height: 150px">
                       <asp:GridView ID="GridView_Owner" runat="server" AutoGenerateColumns="False" Width="98%" DataKeyNames="OwnerId" OnSelectedIndexChanged="GridView_Owner_SelectedIndexChanged" OnRowEditing="GridView_Owner_Row_Editing" OnRowDeleting="GridView_Owner_Row_Deleting" OnPreRender="GridView_Owner_PreRender" OnDataBound="GridView_Owner_DataBound" GridLines="Horizontal" OnRowCommand="GridView_Owner_RowCommand" >
                         <Columns>
                              <asp:CommandField ButtonType="Image" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True" HeaderText="View" >
                               <ItemStyle Width="38px"/></asp:CommandField>
                              <%--<asp:CommandField ButtonType="Image" ShowEditButton="True" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit" >
                               <ItemStyle Width="40px" /></asp:CommandField>--%>
                              <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle Width="40px" />
                                    <ItemTemplate>
                                    <asp:ImageButton ID="btnEditOwner" CausesValidation="false" OnClick="btnEditOwner_click"
                                    ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                    CommandArgument='<%#Eval("OwnerId")%>' />
                                    <asp:HiddenField ID="hdnOwnerId" runat="server" Value='<%#Eval("OwnerId")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="Delete" ShowHeader="False"><ItemStyle Width="40px" />
                                <ItemTemplate>
                                 <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" Text="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />
                                </ItemTemplate>
                               </asp:TemplateField>
                              <%-- <asp:BoundField DataField="CostCentreGroupName" HeaderText="Cost Centre Group">
                                 <ItemStyle HorizontalAlign="Left"  />
                               </asp:BoundField>--%>
                               <asp:TemplateField HeaderText="Owner Pool Name"><ItemStyle HorizontalAlign="Left" />
                                  <ItemTemplate>  
                                   <asp:Label ID="lblOwnerpoolname" runat="server" Text='<%#Eval("OwnerPoolName")%>'></asp:Label>
                                   <asp:HiddenField ID="HiddenOwnerId" runat="server" Value='<%#Eval("OwnerId")%>' />
                                   <asp:HiddenField ID="HiddenOwnerName" runat="server" Value='<%#Eval("OwnerName")%>' />
                                  </ItemTemplate>
                               </asp:TemplateField>
                              
                                <asp:BoundField DataField="OwnerName" HeaderText="Owner Name">
                                 <ItemStyle HorizontalAlign="Left"/>
                               </asp:BoundField>
                                 <asp:BoundField DataField="OwnerShortName" HeaderText=" Short Name">
                                 <ItemStyle HorizontalAlign="Left"  />
                               </asp:BoundField>
                                <asp:BoundField DataField="OwnerCode" HeaderText="Owner Code">
                                 <ItemStyle HorizontalAlign="Left"/>
                               </asp:BoundField>
                               <asp:BoundField DataField="Mail1" HeaderText="Primary Mail">
                                 <ItemStyle HorizontalAlign="Left"/>
                               </asp:BoundField>
                               <%-- <asp:BoundField DataField="IncludeInBudgets" HeaderText="Include In Budgets">
                                 <ItemStyle HorizontalAlign="Left" Width="110px"/>
                               </asp:BoundField>--%>
                              <%-- <asp:BoundField DataField="CreatedBy" HeaderText="Created By">
                                 <ItemStyle HorizontalAlign="Left"/>
                               </asp:BoundField>
                               <asp:BoundField DataField="CreatedOn" HeaderText="Created On">
                                 <ItemStyle HorizontalAlign="Center" Width="100px" />
                               </asp:BoundField>
                               <asp:BoundField DataField="ModifiedBy" HeaderText="Modified By">
                                 <ItemStyle HorizontalAlign="Left"/>
                               </asp:BoundField>
                               <asp:BoundField DataField="ModifiedOn" HeaderText="Modified On">
                                 <ItemStyle HorizontalAlign="Center" Width="100px" />
                               </asp:BoundField>--%>
                                <asp:BoundField DataField="StatusName" HeaderText="Status">
                                 <ItemStyle HorizontalAlign="Left" Width="80px" />
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
                        <asp:Label ID="lbl_GridView_Owner" runat="server" Text=""></asp:Label>
                <asp:Label ID="lbl_Owner_Message" runat="server" ForeColor="#C00000"></asp:Label></fieldset>
            <asp:Panel ID="pnl_Owner" runat="server" Width="100%" Visible="False">
              <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="">
                <legend><strong>Owner Details</strong></legend>
                  <table border="0" cellpadding="0" cellspacing="0" style="height: 100px; text-align: center" width="100%">
                    <tr>
                      <td colspan="4">
                          <asp:HiddenField ID="HiddenOwner" runat="server" />
                      </td>
                                                            </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right; height: 13px;">
                          </td>
                          <td style="text-align: left; height: 13px;">
                              &nbsp;
                          </td>
                          <td align="right" style="padding-right: 15px; text-align: right; height: 13px;">
                          </td>
                          <td style="text-align: left; height: 13px;">
                          </td>
                      </tr>
                                                            <tr>
                                                                <td align="right" style="text-align: right; padding-right:15px">
                                                                    Owner Pool Name:</td>
                                                                <td style="text-align: left">
                                                                    <asp:DropDownList ID="ddlOwnerPoolName" runat="server" CssClass="required_box" Width="205px" TabIndex="1">
                                                                    </asp:DropDownList></td>
                                                                <td align="right" style="text-align: right; padding-right:15px">
                                                                    Owner Name:</td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txt_OwnerName" runat="server" CssClass="required_box" MaxLength="49"
                                                                        TabIndex="2" Width="200px"></asp:TextBox></td>
                                                            </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                              &nbsp;
                              <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="ddlOwnerPoolName"
                                  ErrorMessage="Required" MaximumValue="1000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                              &nbsp;
                              <asp:RequiredFieldValidator ID="RequiredFieldValidatorAccountHeadName" runat="server"
                                  ControlToValidate="txt_OwnerName" ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px; height: 19px;">
                              Owner Short Name:</td>
                          <td style="text-align: left; height: 19px;">
                              <asp:TextBox ID="txtOwnerShortName" runat="server" CssClass="input_box" MaxLength="19"
                                  Width="200px" TabIndex="3"></asp:TextBox></td>
                          <td align="right" style="text-align: right; padding-right:15px; height: 19px;">
                              Owner Code:</td>
                          <td style="text-align: left; height: 19px;">
                              <asp:TextBox ID="txtOwnerCode" runat="server" CssClass="input_box" MaxLength="4"
                                  Width="200px" TabIndex="4"></asp:TextBox></td>
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
                          <td align="right" style="text-align: right; padding-right:15px; height: 19px;">
                              Owner Primary Mail Id:</td>
                          <td style="text-align: left; height: 19px;">
                              <asp:TextBox ID="txtPrimaryMailId" runat="server" CssClass="input_box" MaxLength="50"
                                  Width="200px" TabIndex="5" TextMode="Email"></asp:TextBox></td>
                          <td align="right" style="text-align: right; padding-right:15px; height: 19px;">
                             Owner Secondary Mail Id:</td>
                          <td style="text-align: left; height: 19px;">
                              <asp:TextBox ID="txtSecondaryMailId" runat="server"  MaxLength="50"
                                  Width="200px" TabIndex="6" TextMode="Email"></asp:TextBox></td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                               <asp:RequiredFieldValidator ID="rfvPrimaryMailId" runat="server"
                                  ControlToValidate="txtPrimaryMailId" ErrorMessage="Required"></asp:RequiredFieldValidator> &nbsp;
                              <asp:RegularExpressionValidator ID="revEmailAddress" runat="server" SetFocusOnError="true"
                        Display="None" ControlToValidate="txtPrimaryMailId" resourceKey="revEmailAddress"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ></asp:RegularExpressionValidator>
                          </td>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                              &nbsp;
                              <asp:RegularExpressionValidator ID="revEmailAddress2" runat="server" SetFocusOnError="true"
                        Display="None" ControlToValidate="txtSecondaryMailId" resourceKey="revEmailAddress2"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ></asp:RegularExpressionValidator>
                          </td>
                      </tr>
                       <tr>
                          <td align="right" style="text-align: right; padding-right:15px; height: 19px;">
                             Owner Third Mail Id: </td>
                          <td style="text-align: left; height: 19px;">
                               <asp:TextBox ID="txtThirdEmailId" runat="server"  MaxLength="50"
                                  Width="200px" TabIndex="6" TextMode="Email"></asp:TextBox>
                             </td>
                          <td align="right" style="text-align: right; padding-right:15px; height: 19px;">
                            Owner Contact #: </td>
                          <td style="text-align: left; height: 19px;">
                              <asp:TextBox ID="txtOwnerContact" runat="server" CssClass="input_box" MaxLength="15"
                                  Width="200px"  TextMode="Number" TabIndex="8"></asp:TextBox>
                              </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                               <asp:RegularExpressionValidator ID="revEmailAddress3" runat="server" SetFocusOnError="true"
                        Display="None" ControlToValidate="txtThirdEmailId" resourceKey="revEmailAddress3"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ></asp:RegularExpressionValidator>
                          </td>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                          </td>
                      </tr>
                    <tr>
                        <td align="right" style="text-align: right; padding-right:15px">
                            Created By:</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCreatedBy_Owner" runat="server" CssClass="input_box" ReadOnly="True" Width="200px" TabIndex="-1"></asp:TextBox></td>
                        <td align="right" style="text-align: right; padding-right:15px">
                            Created On:</td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txtCreatedOn_Owner" runat="server" CssClass="input_box" ReadOnly="True" Width="200px" TabIndex="-2"></asp:TextBox></td>
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
                                                                <td align="right" style="text-align: right; padding-right:15px; height: 19px;">
                                                                    Modified By:</td>
                                                                <td style="text-align: left; height: 19px;">
                                                                    <asp:TextBox ID="txtModifiedBy_Owner" runat="server" CssClass="input_box" ReadOnly="True" Width="200px" TabIndex="-3"></asp:TextBox></td>
                                                                <td align="right" style="text-align: right; padding-right:15px; height: 19px;">
                                                                    Modified On:</td>
                                                                <td style=" text-align: left; height: 19px;">
                                                                    <asp:TextBox ID="txtModifiedOn_Owner" runat="server" CssClass="input_box" ReadOnly="True" Width="200px" TabIndex="-4"></asp:TextBox></td>
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
                              <asp:DropDownList ID="ddlStatus_Owner" runat="server" CssClass="input_box" Width="205px" TabIndex="5">
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
                       <asp:Button ID="btn_Add_Owner" runat="server" CssClass="btn" 
                    Text="Add" Width="59px" OnClick="btn_Add_Owner_Click" CausesValidation="False" TabIndex="6" />
                <asp:Button ID="btn_Save_Owner" runat="server" CssClass="btn" 
                    Text="Save" Width="59px" OnClick="btn_Save_Owner_Click" TabIndex="7" />
                <asp:Button ID="btn_Cancel_Owner" runat="server" CssClass="btn" 
                    Text="Cancel" Width="59px" OnClick="btn_Cancel_Owner_Click" CausesValidation="False" TabIndex="8" />
                <asp:Button ID="btn_Print_Owner" runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_Print_Owner_Click" TabIndex="9" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('ctl00_ContentPlaceHolder1_pnl_Owner');" Visible="False" />
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
