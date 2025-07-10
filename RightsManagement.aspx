<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RightsManagement.aspx.cs" Inherits="RightsManagement" %>
<%@ Register Src="~/UserControls/MessageBox.ascx" TagName="Message" TagPrefix="mtm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Role Management</title>
    <link type="text/css" href="StyleSheet.css" rel="Stylesheet" />   
    <script type="text/javascript" language="javascript">
        function check(ctl,arg)
        {
             var ctls=document.getElementsByName(arg);  
             for(i=0;i<=ctls.length-1;i++)
             {
                    ctls[i].childNodes[0].checked=ctl.checked; 
             }   
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center">
   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table cellspacing="0" border="0" cellpadding="0" style="width:100%; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center">
     <tr>
       <td>
        <table cellpadding="0" cellspacing="0" width="100%">
          <tr>
            <td style="text-align: center;">
            <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width:200px; vertical-align:top ; padding-bottom:20px; " >
                    <div style=" padding-right :17px;"  >
                    <table cellpadding="0" cellspacing ="0" width="100%" border="1">
                    <tr class="header" >
                    <td style=" width :50px">Edit</td>
                    <td style=" width :133px">User Name</td>
                    </tr>
                    </table>
                    </div>
                    <asp:Panel runat="server" ID="pnlList" Width="100%" style=" overflow-x:hidden; overflow-y:scroll">
                    <table style="" cellpadding ="0" cellspacing ="0" width="100%" border ="1">
                    <asp:Repeater runat="server" ID="rpt_Data">
                    <ItemTemplate >
                    <tr class="<%# (int.Parse(Eval("LoginId").ToString())==int.Parse(ViewState["SelId"].ToString()))?"selrowcontent":"rowcontent" %>">
                    <td style=" width :50px"><asp:ImageButton runat="server" ImageUrl="images/edit.jpg" ID="imgEdit" OnClick="EditClick" CommandArgument='<%# Eval("LoginId") %>'/></td>
                    <td style=" width :133px; text-align : left">&nbsp;<%# Eval("UserName")%></td>
                    </tr>
                    </ItemTemplate>
                    </asp:Repeater> 
                    </table>
                    </asp:Panel>    
                </td>
                <td style="vertical-align:top  " >
                  <strong class="pagename" >Rights Management</strong>
                  <mtm:Message runat="server" ID="Msgbox" /> 
                    <asp:Panel ID="pnl_UserLogin" runat="server" Width="100%" Visible="False">
                    <table border="0" cellpadding="0" cellspacing="4" style="text-align: center" width="100%">
                    <tr>
                    <td colspan="2" style =" background-color :gray">&nbsp;</td>
                    </tr>
                      <tr>
                      <td colspan="2">
                          <asp:HiddenField ID="HiddenUserId" runat="server" />
                      </td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px" class="style1">
                              User Name:</td>
                          <td style="text-align: left">
                              <asp:Label ID="lblUserName" runat="server" Width="235px"></asp:Label>
                              &nbsp;&nbsp;&nbsp;Role Name : <asp:Label runat="server" ID="lblRoleName"></asp:Label>  </td>
                      </tr>
                        <tr>
                            <td align="right" style="text-align: right; padding-right:15px" class="style1">
                                Select Application :</td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="ddlApplication" runat="server" CssClass="required_box" 
                                    Width="240px" onselectedindexchanged="ddlApplication_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
                             </td>
                        </tr>
                        <tr>
                            <td align="right" style="text-align: right; padding-right:15px" class="style1">
                                Select Module :</td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="ddlModule" runat="server" CssClass="required_box" 
                                    Width="240px" onselectedindexchanged="ddlModule_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="padding-right:15px; vertical-align : top ">
                                Pages List :</td>
                            <td style="text-align: center">
                                <div style=""  >
                                    <table cellpadding="0" cellspacing ="0" width="100%" border="1">
                                    <tr class="header" >
                                        <td style=" width :200px">Page Name</td>
                                        <td style=" width :80px"><input type="checkbox" value="false" onclick="check(this,'td1')"/>View</td>
                                        <td style=" width :80px"><input type="checkbox" value="false" onclick="check(this,'td2')"/>Add</td>
                                        <td style=" width :80px"><input type="checkbox" value="false" onclick="check(this,'td3')"/>Update</td>
                                        <td style=" width :80px"><input type="checkbox" value="false" onclick="check(this,'td4')"/>Delete</td>
                                        <td style=" width :80px"><input type="checkbox" value="false" onclick="check(this,'td5')"/>Print</td>
                                        <td style=" width :80px"><input type="checkbox" value="false" onclick="check(this,'td6')"/>Verify</td>
                                        <td style=" width :80px"><input type="checkbox" value="false" onclick="check(this,'td7')"/>Verify2</td>
                                    </tr>
                                    </table>
                                </div>
                                <asp:Panel runat="server" ID="Panel1" Width="100%" style=" overflow-x:hidden; overflow-y:scroll" Height="280px" >
                                <table style="" cellpadding ="0" cellspacing ="0" width="100%" border ="1">
                                <asp:Repeater runat="server" ID="rpt_Data1">
                                    <ItemTemplate >
                                    <tr>
                                        <td style=" width :200px;text-align : left">&nbsp;<%# Eval("PageName")%><asp:HiddenField runat="server" ID="pageid" value='<%# Eval("PageId") %>'/></td>
                                        <td style=" width :80px" name="td1" id="td1"><asp:CheckBox runat="server" Visible='<%# !IsRoleRight(int.Parse(Eval("RoleId").ToString()),int.Parse(Eval("PageId").ToString()),1)%>' ID="chkView" Checked='<%# Eval("IsView") %>'/><img runat="server" Visible='<%# IsRoleRight(int.Parse(Eval("RoleId").ToString()),int.Parse(Eval("PageId").ToString()),1)%>' src="Images/check.png" alt="Yes" /></td>
                                        <td style=" width :80px" name="td2" id="td2"><asp:CheckBox runat="server" Visible='<%# !IsRoleRight(int.Parse(Eval("RoleId").ToString()),int.Parse(Eval("PageId").ToString()),2)%>' ID="chkAdd" Checked='<%# Eval("IsAdd") %>'/><img runat="server" Visible='<%# IsRoleRight(int.Parse(Eval("RoleId").ToString()),int.Parse(Eval("PageId").ToString()),2)%>' src="Images/check.png" alt="Yes" /></td>
                                        <td style=" width :80px" name="td3" id="td3"><asp:CheckBox runat="server" Visible='<%# !IsRoleRight(int.Parse(Eval("RoleId").ToString()),int.Parse(Eval("PageId").ToString()),3)%>' ID="chkUpdate" Checked='<%# Eval("IsUpdate") %>'/><img runat="server" Visible='<%# IsRoleRight(int.Parse(Eval("RoleId").ToString()),int.Parse(Eval("PageId").ToString()),3)%>' src="Images/check.png" alt="Yes" /></td>
                                        <td style=" width :80px" name="td4" id="td4"><asp:CheckBox runat="server" Visible='<%# !IsRoleRight(int.Parse(Eval("RoleId").ToString()),int.Parse(Eval("PageId").ToString()),4)%>' ID="chkDelete" Checked='<%# Eval("IsDelete") %>'/><img runat="server" Visible='<%# IsRoleRight(int.Parse(Eval("RoleId").ToString()),int.Parse(Eval("PageId").ToString()),4)%>' src="Images/check.png" alt="Yes" /></td>
                                        <td style=" width :80px" name="td5" id="td5"><asp:CheckBox runat="server" Visible='<%# !IsRoleRight(int.Parse(Eval("RoleId").ToString()),int.Parse(Eval("PageId").ToString()),5)%>' ID="chkPrint" Checked='<%# Eval("IsPrint") %>'/><img runat="server" Visible='<%# IsRoleRight(int.Parse(Eval("RoleId").ToString()),int.Parse(Eval("PageId").ToString()),5)%>' src="Images/check.png" alt="Yes" /></td>
                                        <td style=" width :80px" name="td6" id="td6"><asp:CheckBox runat="server" Visible='<%# !IsRoleRight(int.Parse(Eval("RoleId").ToString()),int.Parse(Eval("PageId").ToString()),6)%>' ID="chkVerify" Checked='<%# Eval("IsVerify") %>'/><img runat="server" Visible='<%# IsRoleRight(int.Parse(Eval("RoleId").ToString()),int.Parse(Eval("PageId").ToString()),6)%>' src="Images/check.png" alt="Yes" /></td>
                                        <td style=" width :80px" name="td7" id="td7"><asp:CheckBox runat="server" Visible='<%# !IsRoleRight(int.Parse(Eval("RoleId").ToString()),int.Parse(Eval("PageId").ToString()),7)%>' ID="chkVerify2" Checked='<%# Eval("IsVerify2") %>'/><img runat="server" Visible='<%# IsRoleRight(int.Parse(Eval("RoleId").ToString()),int.Parse(Eval("PageId").ToString()),7)%>' src="Images/check.png" alt="Yes" /></td>
                                    </tr>
                                    </ItemTemplate>
                                </asp:Repeater> 
                                </table>
                                </asp:Panel> 
                            </td>
                        </tr>
                    </table>
                    </asp:Panel>
                    <table style="width: 100%">
                            <tr>
                                <td style="text-align: right">
                                    <asp:Button ID="btn_Save" runat="server" CssClass="btn" 
                            Text="Save" Width="59px" OnClick="btn_Save_Click" />
                                    <asp:Button ID="btn_Cancel" runat="server" CssClass="btn" 
                            Text="Cancel" Width="59px" OnClick="btn_Cancel_Click" CausesValidation="False" />
                            <asp:Button runat="server" id="btnBack" CssClass="btn" UseSubmitBehavior="false" Text="<< Go Back" OnClientClick="return parent.DoPost(5);"></asp:Button> 
                                </td>
                            </tr>
                        </table>
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
<%# Eval("UserName")%>