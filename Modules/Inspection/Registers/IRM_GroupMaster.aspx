<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="IRM_GroupMaster.aspx.cs" Inherits="Registers_IRM_GroupMaster" Title="Untitled Page" %>

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
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <table align="center" width="100%" border="0" cellpadding="0" cellspacing="0" style="font-family:Arial;font-size:12px;"> 
        <tr>
            <td class="text headerband">
                Insurance Group
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
                              &nbsp;<asp:TextBox ID="txtShname" runat="server" CssClass="input_box" Width="510px" MaxLength="30" TabIndex="2" Enabled="False"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtShname"
                                  ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                          <td colspan="1" style="text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px; width: 287px;">
                              Group Name:</td>
                          <td style="text-align: left;" colspan="1">
                              &nbsp;<asp:TextBox ID="txtGroupName" runat="server" CssClass="input_box" Width="510px" MaxLength="50" TabIndex="3" Enabled="False"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtGroupName"
                                  ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                          <td colspan="1" style="text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px; width: 287px;">
                              Description:</td>
                          <td style="text-align: left;" colspan="1">
                              &nbsp;<asp:TextBox ID="txtDescr" TextMode="MultiLine" runat="server" 
                                  CssClass="input_box" Width="510px" MaxLength="500" TabIndex="4" Enabled="False" 
                                  Height="125px"></asp:TextBox>
                              </td>
                          <td colspan="1" style="text-align: left">
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right; height: 3px; width: 287px;">
                           <asp:HiddenField ID="hfdGroupId" runat="server" />
                          </td>
                          <td style="text-align: left; height: 3px;">
                              </td>
                          <td style="height: 3px; text-align: left">
                          </td>
                      </tr>                      
                      <tr>
                          <td align="right" style="padding-right: 15px; width: 287px; text-align: right">
                              &nbsp;</td>
                          <td style="text-align: right; padding-right: 15px;" valign="top" colspan="2">
                              <asp:Button ID="btnCancel" runat="server" CausesValidation="False" 
                                  CssClass="btn" onclick="btnCancel_Click" TabIndex="10" Text="Cancel" 
                                  Visible="False" Width="59px" />
                              <asp:Button ID="btnNewGroup" runat="server" CausesValidation="False" 
                                  CssClass="btn" onclick="btnNewGroup_Click" TabIndex="11" Text="New" 
                                  Width="59px" />
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
                                <asp:GridView ID="GrdView_Groups" runat="server" AutoGenerateColumns="False" 
                                    GridLines="Horizontal" Width="98%" onrowcommand="GrdView_Groups_RowCommand" 
                                     AllowPaging="True" 
                                    onpageindexchanging="GrdView_Groups_PageIndexChanging" OnRowCancelingEdit="GrdView_Groups_RowCancelingEdit" OnRowEditing="GrdView_Groups_RowEditing">
                                    <RowStyle CssClass="rowstyle" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate >
                                               <asp:ImageButton ID="imgbtnEdit" CommandArgument='<%#Eval("GroupId") %>'  ToolTip="Edit Group" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" OnClick="imgbtnEdit_Click" />
                                               <asp:HiddenField ID="hdfDescr" Value='<%#Eval("Description") %>' runat="server" />
                                                <asp:HiddenField ID="hdGroupId" Value='<%#Eval("GroupId") %>' runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="45px" HorizontalAlign="Center"></ItemStyle>                                            
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ShortName" HeaderStyle-HorizontalAlign="Left" HeaderText="Short Name">
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="GroupName" HeaderStyle-HorizontalAlign="Left" HeaderText="Group Name">
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

