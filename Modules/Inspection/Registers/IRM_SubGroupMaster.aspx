<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="IRM_SubGroupMaster.aspx.cs" Inherits="Registers_IRM_SubGroupMaster"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>EMANAGER</title>
      <%-- <link href="../HRD/Styles/style.css" rel="stylesheet" type="text/css" />--%>
     <link rel="stylesheet" type="text/css" href="../../HRD/Styles/StyleSheet.css" />
       <style type="text/css">
        .btn
        {
            border: 1px solid #fe0034;
	font-family: arial;
	font-size: 12px;
	color: #fff;
	border-radius: 3px;
	-webkit-border-radius: 3px;
	-ms-border-radius: 3px;
	background: #fe0030;
	background: linear-gradient(#ff7c96, #fe0030);
	background: -webkit-linear-gradient(#ff7c96, #fe0030);
	background: -ms-linear-gradient(#ff7c96, #fe0030);
	padding: 4px 6px;
	cursor: pointer;
        }
        </style>
    </head>
<body  >
<form id="form1" runat="server" >
 <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0" style="font-family:Arial;font-size:12px;"> 
        <tr>
            <td class="text headerband">
                Insurance SubGroup
            </td>
        </tr>
     <tr>
       <td>
        <table cellpadding="0" cellspacing="0" width="100%">
            <%--<tr>
       <td align="center">
           <asp:Label ID="Label1" runat="server" CssClass="textregisters" Text="Inspection"></asp:Label></td>
    </tr> --%>
          <tr>
            <td style="text-align: center;">
              <fieldset style="border-right: #8fafdb 0px solid; border-top: #8fafdb 0px solid;border-left: #8fafdb 0px solid; border-bottom: #8fafdb 0px solid" class="">
                <%--<legend><strong>Inspection</strong></legend>--%>
                <asp:Panel ID="pnl_Groups" runat="server" Width="100%">
                  <table border="0" cellpadding="0" cellspacing="0" style="text-align: center;" width="100%">
                    <tr>
                      <td colspan="2" style="height: 15px">
                          &nbsp;</td>
                        <td colspan="1" style="height: 15px">
                        </td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px; width: 287px;">
                              Select Group:</td>
                          <td style="text-align: left;" colspan="1">
                              &nbsp;<asp:DropDownList ID="ddlGroups" CssClass="input_box" runat="server" 
                                  Width="250px"></asp:DropDownList>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0" ControlToValidate="ddlGroups"
                                  ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                          <td colspan="1" style="text-align: left">
                          </td>
                      </tr>                                      
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px; width: 287px;">
                              SubGroup Name:</td>
                          <td style="text-align: left;" colspan="1">
                              &nbsp;<asp:TextBox ID="txtSubGroupName" runat="server" CssClass="input_box" 
                                  Width="510px" MaxLength="50" TabIndex="2" Enabled="False"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSubGroupName"
                                  ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                          <td colspan="1" style="text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right; height: 3px; width: 287px;">
                           <asp:HiddenField ID="hfdSubGroupId" runat="server" />
                          </td>
                          <td style="text-align: left; height: 3px;">
                              </td>
                          <td style="height: 3px; text-align: left">
                          </td>
                      </tr>                      
                      <tr>
                          <td align="right" style="padding-right: 15px; width: 287px; text-align: right">
                              &nbsp;</td>
                          <td style="text-align: left" valign="top">
                              &nbsp;</td>
                          <td style="padding-right: 17px; text-align: right" valign="top">
                                  <asp:Button ID="btnCancel" runat="server" CausesValidation="False" 
                                      CssClass="btn" TabIndex="10" Text="Cancel" Visible="False" Width="59px" 
                                      onclick="btnCancel_Click" />
                              <asp:Button ID="btnNewSubGroup" runat="server" CssClass="btn" Text="New" Width="59px" 
                                      CausesValidation="False" TabIndex="11" onclick="btnNewSubGroup_Click" />
                              <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="Save" Width="59px" 
                                      TabIndex="12" Enabled="False" onclick="btnSave_Click" /></td>
                      </tr>
                     </table>
                                                    
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td style="text-align: right; padding-right: 17px;">
                        <div id="Div1" runat="server" style="position: relative; top: 0; text-align: center; left: 0"><asp:Label ID="lblMessage" runat="server" ForeColor="#C00000"></asp:Label></div>
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
                            <div style="width: 100%; height: 225px" ><%-- height: 150px;240--%>
                                <asp:GridView ID="GrdView_SubGroups" runat="server" AutoGenerateColumns="False" 
                                    AllowPaging="true" PageSize="10" 
                                    GridLines="Horizontal" Width="98%" onrowcommand="GrdView_SubGroups_RowCommand" 
                                    onrowediting="GrdView_SubGroups_RowEditing" 
                                    onpageindexchanging="GrdView_SubGroups_PageIndexChanging">
                                    <RowStyle CssClass="rowstyle" />
                                    <Columns>
                                         <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="45px" />
     <ItemTemplate>
         <asp:ImageButton ID="imgbtnEdit" CausesValidation="false" OnClick="imgbtnEdit_Click"
           ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit SubGroup" 
         CommandArgument='<%#Eval("GroupId").ToString()+","+Eval("SubGroupId").ToString() %>' />
       <asp:HiddenField ID="hdnInsSubGroupId" runat="server" Value='<%#Eval("SubGroupId")%>' />
                                                                        </ItemTemplate>
                                                                            </asp:TemplateField>
                                        <%--<asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                               <asp:ImageButton ID="imgbtnEdit" CommandArgument='<%#Eval("GroupId").ToString()+","+Eval("SubGroupId").ToString() %>' CommandName="Edit" ToolTip="Edit SubGroup" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/edit.png" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>--%>
                                        <asp:BoundField DataField="GroupName" HeaderText="Group Name">
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SubGroupName" HeaderText="SubGroup Name">
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:BoundField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Center" />
                                    <SelectedRowStyle CssClass="selectedtowstyle" />
                                    <HeaderStyle CssClass="headerstylefixedheadergrid" />
                                </asp:GridView>
                     </div>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="padding-right: 7px; padding-left: 16px; text-align: center">
                            &nbsp;</td>
                    </tr>
                </table>&nbsp;
            </asp:Panel>
            </fieldset>
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

