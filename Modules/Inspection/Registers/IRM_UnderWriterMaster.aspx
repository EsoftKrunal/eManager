<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="IRM_UnderWriterMaster.aspx.cs" Inherits="Registers_IRM_UnderWriterMaster" Title="Untitled Page" %>

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
                Insurance UW
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
                              Short Name:</td>
                          <td style="text-align: left;" colspan="1">
                              &nbsp;<asp:TextBox ID="txtShName" runat="server" CssClass="input_box" Width="510px" MaxLength="30" TabIndex="2" Enabled="False"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtShName"
                                  ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                          <td colspan="1" style="text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px; width: 287px;">
                              UW Name:</td>
                          <td style="text-align: left;" colspan="1">
                              &nbsp;<asp:TextBox ID="txtUWName" runat="server" CssClass="input_box" Width="510px" MaxLength="50" TabIndex="2" Enabled="False"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUWName"
                                  ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                          <td colspan="1" style="text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right; height: 3px; width: 287px;">
                           <asp:HiddenField ID="hfdUWId" runat="server" />
                          </td>
                          <td style="text-align: left; height: 3px;">
                              </td>
                          <td style="height: 3px; text-align: left">
                          </td>
                      </tr>                      
                      <tr>
                          <td align="right" style="padding-right: 15px; width: 287px; text-align: right">
                              &nbsp;</td>
                          <td style="text-align: right; padding-right:15px;" valign="top" colspan="2">
                              <asp:Button ID="btnCancel" runat="server" CausesValidation="False" 
                                  CssClass="btn" onclick="btnCancel_Click" TabIndex="10" Text="Cancel" 
                                  Visible="False" Width="59px" />
                              <asp:Button ID="btnNewUW" runat="server" CausesValidation="False" 
                                  CssClass="btn" onclick="btnNewUW_Click" TabIndex="11" Text="New" Width="59px" />
                              <asp:Button ID="btnSave" runat="server" CssClass="btn" Enabled="False" 
                                  onclick="btnSave_Click" TabIndex="12" Text="Save" Width="59px" />
                          </td>
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
                            <div style="width: 100%; height: 220px" ><%-- height: 150px;240--%>
                                <asp:GridView ID="GrdView_UW" runat="server" AutoGenerateColumns="False" 
                                    GridLines="Horizontal" Width="98%" AllowPaging="True" 
                                    onpageindexchanging="GrdView_UW_PageIndexChanging" 
                                    onrowcommand="GrdView_UW_RowCommand" onrowediting="GrdView_UW_RowEditing" >
                                    <RowStyle CssClass="rowstyle" />
                                    <Columns>
                                    <asp:TemplateField HeaderText="Edit"  ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle Width="45px"  />
                                        <ItemTemplate>
                                        <asp:ImageButton ID="imgbtnEdit" CausesValidation="false" OnClick="imgbtnEdit_Click"
                                        ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                        CommandArgument='<%#Eval("UWId") %>' />
                                        <asp:HiddenField ID="hdnUWId" runat="server" Value='<%#Eval("UWId")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <%--  <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                               <asp:ImageButton ID="imgbtnEdit" CommandArgument='<%#Eval("UWId") %>' CommandName="Edit" ToolTip="Edit UW" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/edit.png" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>--%>
                                        <asp:BoundField DataField="ShortName" HeaderStyle-HorizontalAlign="Left" HeaderText="Short Name">
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UWName" HeaderStyle-HorizontalAlign="Left" HeaderText="UW Name">
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

