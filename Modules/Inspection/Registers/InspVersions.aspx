<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="InspVersions.aspx.cs" Inherits="Registers_InspectionGroupVersions"  %>
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
          <tr>
            <td style="text-align: center;">
            <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 0px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="">
                <table border="0" cellpadding="0" cellspacing="0" style="text-align: center" width="100%">
                    <tr>
                      <td colspan="2" style="height: 15px">
                          <asp:HiddenField ID="HiddenGrpVersion" runat="server" />
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
                </table>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td style="text-align: right; padding-right: 17px;">
                        <div id="Div1" runat="server" style="position: relative; top: 0; text-align: center; left: 0"><asp:Label ID="lbl_GrpVersion_Message" runat="server" ForeColor="#C00000"></asp:Label>&nbsp;</div>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="padding-right: 5px; padding-left: 5px">
                        <div style="width: 100%; height: 240px">
                        <asp:GridView ID="GridView_GrpVersion" runat="server" AutoGenerateColumns="False" Width="98%" DataKeyNames="VERSIONID" GridLines="Horizontal" OnRowEditing="GridView_GrpVersion_RowEditing" AllowPaging="True" OnPageIndexChanging="GridView_GrpVersion_PageIndexChanging" PageSize="8" OnRowCommand="GridView_GrpVersion_RowCommand" >
                        <Columns>
                        <asp:TemplateField HeaderText="Sub Chapter#">
                            <ItemTemplate>
                                <asp:Label ID="lbl_SubChapNo" runat="server" Text='<%# Eval("VersionName") %>'></asp:Label>
                                <asp:HiddenField ID="Hidden_GrpVersionId" runat="server" Value='<%# Eval("VersionId") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="100px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ApplyDate" HeaderText="Date Applied">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Edit">
                                    <ItemStyle Width="45px" />
     <ItemTemplate>
                                                                            <asp:ImageButton ID="btnEditInsGroup" CausesValidation="false" OnClick="btnEditInsGroup_Click"
                                                                                ImageUrl="~/Modules/HRD/Images/edit.jpg" runat="server" ToolTip="Edit" 
                                                                                CommandArgument='<%#Eval("VersionId")%>' />
                                                                           
                                                                        </ItemTemplate>
                                                                            </asp:TemplateField>
                        </Columns>
                        <RowStyle CssClass="rowstyle" />
                        <pagerstyle horizontalalign="Center" />
                        <SelectedRowStyle CssClass="selectedtowstyle" />
                        <HeaderStyle CssClass="headerstylefixedheadergrid" />
                        </asp:GridView>
                        </div>
                        </td>
                    </tr>
                </table>
            </fieldset>                                
          <asp:HiddenField id="HiddenFieldGridRowCount" runat="server"></asp:HiddenField>
            <asp:Panel ID="pnl_GrpVersion" runat="server" Width="100%">
             <br />
             <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; height :50px; padding-top:20px; " >
             <table cellpadding="0" cellspacing="0" width="600px">
                 <colgroup>
                     <col style=" text-align:right" />
                     <col style=" text-align:left" />
                     <col style=" text-align:right" />
                     <col style=" text-align:left" />
                     <tr>
                         <td>
                             Version Name :</td>
                         <td>
                             <asp:TextBox ID="txtVersionNo" runat="server" CssClass="input_box" 
                                 MaxLength="49" Width="150px"></asp:TextBox>
                         </td>
                         <td>
                             Date Applied From :
                         </td>
                         <td>
                             <asp:TextBox ID="txtplandate" runat="server" CssClass="input_box" Width="92px"></asp:TextBox>
                             <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" 
                                 ImageUrl="~/Modules/HRD/Images/Calendar.gif" />
                             <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                                 Format="dd-MMM-yyyy" PopupButtonID="ImageButton2" TargetControlID="txtplandate">
                             </ajaxToolkit:CalendarExtender>
                         </td>
                     </tr>
                     <tr>
                         <td></td>
                         <td>
                         <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtVersionNo" ErrorMessage="Required."></asp:RequiredFieldValidator> 
                         </td>
                         <td>
                         &nbsp;
                         </td>
                         <td>
                             <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtplandate" ErrorMessage="Required."></asp:RequiredFieldValidator> </td>
                     </tr>
                 </colgroup>
             </table> 
            </fieldset> 
            </asp:Panel>
          <div style=" text-align:right; width :100%; padding-top :10px;" > 
          <asp:Button Text="New" runat="server" ID="btn_New_GrpVersion" OnClick="btn_New_GrpVersion_Click" CssClass="btn" Width="60px" CausesValidation="false" />&nbsp;
          <asp:Button Text="Save" runat="server" ID="btn_Save_GrpVersion" OnClick="btn_Save_GrpVersion_Click"  CssClass="btn"  Width="60px" />&nbsp;
          <asp:Button Text="Cancel" runat="server" ID="btn_Cancel_GrpVersion" OnClick="btn_Cancel_GrpVersion_Click"  CssClass="btn"  Width="60px" CausesValidation="false"/> 
          </div>
          </td>
         </tr>
        </table>
       </td>
      </tr>
     </table>
   </form>
</body>
</html>

