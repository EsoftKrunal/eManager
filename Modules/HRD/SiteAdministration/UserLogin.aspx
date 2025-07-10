<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserLogin.aspx.cs" Inherits="SiteAdministration_UserLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7"> 
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
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
        <%--<asp:Label ID="lbl_UserLogin_Message" runat="server" ForeColor="#C00000" Visible="False"></asp:Label>--%>
    <table cellspacing="0" border="0" cellpadding="0" style="width:100%; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center">
        <tr>
        <td colspan="2" align="center" style="background-color:#4371a5; height: 23px; width: 100%;" class="text" >
            Manage Users&nbsp;
                </td>
         </tr>
        <tr>
            <td colspan="2"> 
             <asp:Label ID="lbl_UserLogin_Message" runat="server" ForeColor="#C00000" Visible="False"></asp:Label>&nbsp;
            </td>
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
            <td style="text-align: center; padding-left:10px; padding-right:10px" align="center">
              <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid; text-align: center; padding-top:0px; padding-bottom:10px;">
                 <legend><strong>User Login List</strong></legend>
                    <div style="overflow-x:hidden; overflow-y:scroll;  width: 100%; height: 150px">
                       <asp:GridView ID="GridView_UserLogin" runat="server" GridLines="Horizontal" AutoGenerateColumns="False" Width="98%" DataKeyNames="LoginId" OnSelectedIndexChanged="GridView_UserLogin_SelectedIndexChanged" OnRowEditing="GridView_UserLogin_Row_Editing"  OnPreRender="GridView_UserLogin_PreRender" OnDataBound="GridView_UserLogin_DataBound" >
                       <RowStyle CssClass="rowstyle" />
                       <SelectedRowStyle CssClass="selectedtowstyle" />
                       <PagerStyle CssClass="pagerstyle" />
                       <HeaderStyle CssClass="headerstylefixedheadergrid" /> 
                         <Columns>
                              <asp:CommandField ButtonType="Image" SelectImageUrl="~/Modules/HRD/Images/HourGlass.gif" ShowSelectButton="True" HeaderText="View" >
                               <ItemStyle Width="35px" /></asp:CommandField>
                              <asp:CommandField ButtonType="Image" ShowEditButton="True" EditImageUrl="~/Modules/HRD/Images/edit.jpg" HeaderText="Edit" >
                               <ItemStyle Width="40px" /></asp:CommandField>
                               <asp:TemplateField HeaderText="Role"><ItemStyle HorizontalAlign="Left" />
                                  <ItemTemplate>  
                                   <asp:Label ID="lblloginname" runat="server" Text='<%#Eval("RoleName")%>'></asp:Label>
                                   <asp:HiddenField ID="HiddenLoginId" runat="server" Value='<%#Eval("LoginId")%>' />
                                  </ItemTemplate>
                               </asp:TemplateField>
                               <asp:BoundField DataField="UserId" HeaderText="User Id">
                                 <ItemStyle HorizontalAlign="Left" />
                               </asp:BoundField>
                               <asp:BoundField DataField="FirstName" HeaderText="First Name">
                                 <ItemStyle HorizontalAlign="Left" />
                               </asp:BoundField>
                               <asp:BoundField DataField="LastName" HeaderText="Family Name">
                                 <ItemStyle HorizontalAlign="Left" />
                               </asp:BoundField>
                               <asp:BoundField DataField="DateOfBirth" HeaderText="DOB">
                                 <ItemStyle HorizontalAlign="center" Width="80px" />
                               </asp:BoundField>
                               <asp:BoundField DataField="Email" HeaderText="Email">
                                 <ItemStyle HorizontalAlign="Left" />
                               </asp:BoundField>    
                              <%-- <asp:BoundField DataField="DepartmentName" HeaderText="Department Name">
                                 <ItemStyle HorizontalAlign="Left" />
                               </asp:BoundField>    --%>                       
                               <asp:BoundField DataField="CreatedBy" HeaderText="Created By">
                                 <ItemStyle HorizontalAlign="Left" />
                               </asp:BoundField>
                               <asp:BoundField DataField="CreatedOn" HeaderText="Created On" Visible ="false">
                                 <ItemStyle HorizontalAlign="center" Width="80px" />
                               </asp:BoundField>
                               <asp:BoundField DataField="ModifiedBy" HeaderText="Modified By">
                                 <ItemStyle HorizontalAlign="Left" />
                               </asp:BoundField>
                               <asp:BoundField DataField="ModifiedOn" HeaderText="Modified On">
                                 <ItemStyle HorizontalAlign="center" Width="80px" />
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
                     <asp:Label ID="lbl_GridView_UserLogin" runat="server" Text=""></asp:Label>
               </fieldset>
            <asp:Panel ID="pnl_UserLogin" runat="server" Width="100%" Visible="False">
              <fieldset style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid;border-left: #8fafdb 1px solid; border-bottom: #8fafdb 1px solid" class="">
                <legend><strong>User Login Details</strong></legend>
                  <table border="0" cellpadding="0" cellspacing="0" style="text-align: center" width="100%">
                    <tr>
                      <td colspan="4">
                          <asp:HiddenField ID="HiddenUserLogin" runat="server" />
                      </td>
                                                            </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                              &nbsp;
                          </td>
                          <td align="left" colspan="2">
                              <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="Role Name and User Id Can't be Same."
                                  Visible="False"></asp:Label></td>
                      </tr>
                                                            <tr>
                                                                <td align="right" style="text-align: right; padding-right:15px; padding-left:65px">
                                                                    Role Name:</td>
                                                                <td style="text-align: left">
                                                                    <asp:DropDownList ID="ddlRoleName" runat="server" CssClass="required_box" Width="185px">
                                                                    </asp:DropDownList></td>
                                                                <td align="right" style="text-align: right; padding-right:15px">
                                                                    User Id:</td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtUserId" runat="server" CssClass="required_box" Width="180px" MaxLength="24" ></asp:TextBox></td>
                                                            </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                              &nbsp;
                              <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="ddlRoleName"
                                  ErrorMessage="Required" MaximumValue="1000" MinimumValue="1" Type="Integer"></asp:RangeValidator></td>
                          <td align="right" style="text-align: right">
                          </td>
                          <td style="text-align: left">
                              &nbsp;
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserId"
                                  ErrorMessage="Required"></asp:RequiredFieldValidator>
                              <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtUserId"
                                  ErrorMessage="Only Alphabets and Dot Are Allowed" ValidationExpression="^[A-Z\sa-z.}]*$"></asp:RegularExpressionValidator></td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">
                              Password:</td>
                          <td style="text-align: left">
                              <asp:TextBox ID="txtPassword" runat="server" CssClass="required_box" Width="180px" MaxLength="19" TextMode="Password"></asp:TextBox>
                              <span style="color: #0000cc"></span>
                          </td>
                          <td align="right" style="text-align: right; padding-right:15px; color: #0e64a0;">
                              Confirm Password:</td>
                          <td style="text-align: left; color: #0e64a0;">
                              <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="required_box" Width="180px" MaxLength="19" TextMode="Password"></asp:TextBox></td>
                      </tr>
                      <tr style="color: #0e64a0">
                          <td align="right" style="padding-right: 15px; text-align: right; height: 13px;">
                              <span>(Min 6 chars)</span></td>
                          <td style="text-align: left; height: 13px;">
                              &nbsp;
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                                  ErrorMessage="Required"></asp:RequiredFieldValidator>
                              <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPassword"
                                  ErrorMessage="Min. 6 chars" ValidationExpression="^.{6,19}$"></asp:RegularExpressionValidator></td>
                          <td align="right" style="text-align: right; padding-right: 15px; height: 13px;">
                              <span style="color: #0e64a0">(Min 6 chars)</span></td>
                          <td style="text-align: left; height: 13px;">
                              &nbsp;
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtConfirmPassword"
                                  ErrorMessage="Required"></asp:RequiredFieldValidator>
                              <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtPassword"
                                  ControlToValidate="txtConfirmPassword" ErrorMessage=" Password do not match."></asp:CompareValidator></td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">
                              First Name:</td>
                          <td style="text-align: left">
                              <asp:TextBox ID="txtFirstName" runat="server" CssClass="required_box" Width="140px" MaxLength="24"></asp:TextBox></td>
                          <td align="right" style="text-align: right; padding-right:15px">
                              Family Name:</td>
                          <td style="text-align: left">
                              <asp:TextBox ID="txtLastName" runat="server" CssClass="required_box" Width="180px" MaxLength="24"></asp:TextBox></td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                              &nbsp;
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtFirstName"
                                  ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                          <td align="right" style="text-align: right">
                          </td>
                          <td style="text-align: left">
                              &nbsp;
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtLastName"
                                  ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">
                              DOB:</td>
                          <td style="text-align: left">
                              <asp:TextBox ID="txtDOB" runat="server" CssClass="input_box" Width="120px"></asp:TextBox>&nbsp;<asp:ImageButton
                                  ID="img_dob_user" runat="server" CausesValidation="false" ImageUrl="~/Modules/HRD/Images/Calendar.gif" /></td>
                          <td align="right" style="text-align: right; padding-right:15px">
                              Email:</td>
                          <td style="text-align: left">
                              <asp:TextBox ID="txtEmail" runat="server" CssClass="required_box" Width="180px" MaxLength="99"></asp:TextBox></td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                              &nbsp;
                              <asp:CompareValidator ID="CompareValidator10" runat="server" ControlToValidate="txtDOB"
                                  ErrorMessage="Invalid Date." Operator="DataTypeCheck" Type="Date"></asp:CompareValidator></td>
                          <td align="right" style="text-align: right">
                          </td>
                          <td style="text-align: left">
                              &nbsp;
                              <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEmail"
                                  ErrorMessage="Enter valid email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEmail"
                                  ErrorMessage="Required"></asp:RequiredFieldValidator></td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">
                              Department:</td>
                          <td style="text-align: left">
                              <div style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid; overflow-y: scroll;
                                  overflow-x: hidden; border-left: #8fafdb 1px solid; width: 180px; border-bottom: #8fafdb 1px solid;
                                  height: 57px">
                                  <asp:CheckBoxList ID="ckldepartment" runat="server" CssClass="input_box" Width="175px">
                                  </asp:CheckBoxList></div>
                          </td>
                          <td align="right" style="text-align: right; padding-right: 15px;">
                              Recruiting Office:</td>
                          <td style="text-align: left">
                              <div style="border-right: #8fafdb 1px solid; border-top: #8fafdb 1px solid; overflow-y: scroll;
                                  overflow-x: hidden; border-left: #8fafdb 1px solid; width: 180px; border-bottom: #8fafdb 1px solid;
                                  height: 57px">
                                  <asp:CheckBoxList ID="chklstRecruitingOffice" runat="server" CssClass="input_box"
                                      Width="175px">
                                  </asp:CheckBoxList></div>
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">
                          </td>
                          <td style="text-align: left">
                              &nbsp;</td>
                          <td align="right" style="text-align: right">
                          </td>
                          <td style="text-align: left">
                          </td>
                      </tr>
                                                            <tr>
                                                                <td align="right" style="text-align: right; padding-right:15px">
                                                                    Created By:</td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtCreatedBy" runat="server" CssClass="input_box" ReadOnly="True" Width="180px"></asp:TextBox></td>
                                                                <td align="right" style="text-align: right; padding-right:15px">
                                                                    Created On:</td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtCreatedOn" runat="server" CssClass="input_box" ReadOnly="True" Width="180px"></asp:TextBox></td>
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
                                                                    <asp:TextBox ID="txtModifiedBy" runat="server" CssClass="input_box" ReadOnly="True" Width="180px"></asp:TextBox></td>
                                                                <td align="right" style="text-align: right; padding-right:15px">
                                                                    Modified On:</td>
                                                                <td style="text-align: left">
                                                                    <asp:TextBox ID="txtModifiedOn" runat="server" CssClass="input_box" ReadOnly="True" Width="180px"></asp:TextBox></td>
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
                              <asp:DropDownList ID="ddlStatus" runat="server" CssClass="input_box" Width="185px">
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
                            <asp:HiddenField ID="HiddenField2" runat="server" />
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy"
                                PopupButtonID="img_dob_user" TargetControlID="txtDOB" PopupPosition="TopRight">
                            </ajaxToolkit:CalendarExtender>
                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AutoComplete="true"
                                ClearMaskOnLostFocus="true" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDOB">
                            </ajaxToolkit:MaskedEditExtender>
                       <asp:Button ID="btn_Add" runat="server" CssClass="btn" 
                    Text="Add" Width="59px" OnClick="btn_Add_Click" CausesValidation="False" />
                            <asp:Button ID="btn_Save" runat="server" CssClass="btn" 
                    Text="Save" Width="59px" OnClick="btn_Save_Click" />
                            <asp:Button ID="btn_Cancel" runat="server" CssClass="btn" 
                    Text="Cancel" Width="59px" OnClick="btn_Cancel_Click" CausesValidation="False" />
                            <asp:Button ID="btn_Print" runat="server" CausesValidation="False" CssClass="btn" OnClick="btn_Print_Click" Text="Print" Width="59px" OnClientClick="javascript:CallPrint('pnl_UserLogin');" Visible="False" />                
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
<%--OnRowDeleting="GridView_UserLogin_Row_Deleting"--%>