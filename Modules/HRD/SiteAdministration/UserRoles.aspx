<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserRoles.aspx.cs" Inherits="SiteAdministration_UserRoles" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7"> 
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <%--<link rel="stylesheet" type="text/css" href="../Styles/Gridstyle.css" />--%>
    <script language="javascript" type="text/javascript">
function CallPrint(strid)
{
 var prtContent = document.getElementById(strid);
 var WinPrint = window.open('','','letf=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,status=0');
 WinPrint.document.write(prtContent.innerHTML);
 WinPrint.document.close();
 WinPrint.focus();
 WinPrint.print();
 WinPrint.close();
// prtContent.innerHTML=strOldOne;
}
</script>  
</head>
<body style=" margin: 0 0 0 0;">
    <form id="form1" runat="server">
    <div style="text-align:center">
   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table cellspacing="0" cellpadding="0" style=" width:100%; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center">
        <tr>
        <td colspan="2" align="center" style="background-color:#4371a5; height: 23px; width: 100%;" class="text" >User Roles</td>
         </tr>
        <tr>
            <td colspan="2">
        <asp:Label ID="lbl_UserRole_Message" runat="server" ForeColor="#C00000" Visible="False"></asp:Label>&nbsp;</td>
        </tr> 
        <tr>
            <td align="right" colspan="2">
                <asp:ImageButton ID="ImageButton1" runat="server" ToolTip="Back to Dash Board" Height="42px" ImageUrl="~/Modules/HRD/Images/AdminDashBoard.gif"
                    OnClick="ImageButton1_Click" Width="80px" CausesValidation="False" />
                &nbsp;&nbsp;
            </td>
        </tr>
     <tr>
       <td>
        <table cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="text-align: center; padding-left:8px; padding-right:8px" align="center">
              <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; padding-top:0px; padding-bottom:10px; padding-left:10px; padding-right:10px">
                 <legend><strong>User Role List</strong></legend>
                    <table cellpadding="0" cellspacing="0" width="100%">
                     <tr>
                      <td style=" padding-top:5px;">
                      <div style="overflow-x:hidden; overflow-y:scroll; width:100%; height:150px">
                       <asp:GridView ID="GridView_UserRole" runat="server" GridLines="Horizontal" AutoGenerateColumns="False" Width="98%" DataKeyNames="RoleSystemId" OnSelectedIndexChanged="GridView_UserRole_SelectedIndexChanged" OnRowEditing="GridView_UserRole_Row_Editing" OnRowDeleting="GridView_UserRole_Row_Deleting" OnPreRender="GridView_UserRole_PreRender" OnDataBound="GridView_UserRole_DataBound" >
                         <RowStyle CssClass="rowstyle" />
                                                <SelectedRowStyle CssClass="selectedtowstyle" />
                                                <PagerStyle CssClass="pagerstyle" />
                                                <HeaderStyle CssClass="headerstylefixedheadergrid" />
                         <Columns>
                              <asp:CommandField ButtonType="Image" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True" HeaderText="View" >
                               <ItemStyle Width="35px" /></asp:CommandField>
                              <asp:CommandField ButtonType="Image" ShowEditButton="True" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit" >
                               <ItemStyle Width="40px" /></asp:CommandField>
                               <asp:TemplateField HeaderText="Delete" ShowHeader="False"><ItemStyle Width="40px" />
                                <ItemTemplate>
                                 <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Modules/HRD/Images/delete.jpg" Text="Delete" OnClientClick="javascript:return window.confirm('Are you Sure to Delete.');" />
                                </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderText="UserRole Name"><ItemStyle HorizontalAlign="Left" />
                                  <ItemTemplate>  
                                   <asp:Label ID="lblRolename" runat="server" Text='<%#Eval("RoleName")%>'></asp:Label>
                                   <asp:HiddenField ID="HiddenRoleSystemId" runat="server" Value='<%#Eval("RoleSystemId")%>' />
                                  </ItemTemplate>
                               </asp:TemplateField>
                               <asp:BoundField DataField="CreatedBy" HeaderText="Created By">
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
                               </asp:BoundField>
                                <asp:BoundField DataField="StatusName" HeaderText="Status">
                                 <ItemStyle HorizontalAlign="Left" Width="80px" />
                               </asp:BoundField>
                         </Columns>
                             <%--<SelectedRowStyle CssClass="selectedtowstyle" />
                             <PagerStyle CssClass="pagerstyle" />
                             <HeaderStyle CssClass="headerstyle" HorizontalAlign="Left" />
                             <AlternatingRowStyle CssClass="alternatingrowstyle" />--%>
                     </asp:GridView>
                     </div>
                        <asp:Label ID="lbl_GridView_UserRole" runat="server" Text=""></asp:Label>
                  </td>
                 </tr>
                </table>        
           </fieldset>
            <asp:Panel ID="pnl_UserRole" runat="server" Width="100%" Visible="False">
              <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="">
                <legend><strong>User Role Details</strong></legend>
                  <table border="0" cellpadding="0" cellspacing="0" style="height: 100px; text-align: center" width="100%">
                    <tr>
                      <td colspan="4">
                          <asp:HiddenField ID="HiddenUserRole" runat="server" />
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
                                                                    Role Name:</td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtUserRoleName" runat="server" CssClass="required_box" Width="140px" MaxLength="49"></asp:TextBox></td>
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
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserRoleName"
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
                                                                    <asp:TextBox ID="txtCreatedBy_UserRole" runat="server" CssClass="input_box" ReadOnly="True" Width="140px"></asp:TextBox></td>
                                                                <td align="right" style="text-align: right; padding-right:15px">
                                                                    Created On:</td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtCreatedOn_UserRole" runat="server" CssClass="input_box" ReadOnly="True" Width="140px"></asp:TextBox></td>
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
                                                                    <asp:TextBox ID="txtModifiedBy_UserRole" runat="server" CssClass="input_box" ReadOnly="True" Width="140px"></asp:TextBox></td>
                                                                <td align="right" style="text-align: right; padding-right:15px">
                                                                    Modified On:</td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtModifiedOn_UserRole" runat="server" CssClass="input_box" ReadOnly="True" Width="140px"></asp:TextBox></td>
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
                              <asp:DropDownList ID="ddlStatus_UserRole" runat="server" CssClass="input_box" Width="145px">
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
                        <td style="text-align: right; height: 13px;">
                       <asp:Button ID="btn_Add_UserRole" runat="server" CssClass="btn" 
                    Text="Add" Width="59px" OnClick="btn_Add_UserRole_Click" CausesValidation="False" />
                            <asp:Button ID="btn_Save_UserRole" runat="server" CssClass="btn" 
                    Text="Save" Width="59px" OnClick="btn_Save_UserRole_Click" />
                            <asp:Button ID="btn_Cancel_UserRole" runat="server" CssClass="btn" 
                    Text="Cancel" Width="59px" OnClick="btn_Cancel_UserRole_Click" CausesValidation="False" />
                            <asp:Button ID="btn_Print_UserRole" runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_Print_UserRole_Click" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('pnl_UserRole');" Visible="False" />                
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
    </form>
</body>
</html>
