<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleManagement.aspx.cs" Inherits="RoleManagement" %>
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
                    <td style=" width :133px">Role Name</td>
                    </tr>
                    </table>
                    </div>
                    <asp:Panel runat="server" ID="pnlList" Width="100%" style=" overflow-x:hidden; overflow-y:scroll" >
                    <table style="" cellpadding ="0" cellspacing ="0" width="100%" border ="1">
                    <asp:Repeater runat="server" ID="rpt_Data">
                    <ItemTemplate >
                   <tr class="<%# (int.Parse(Eval("RoleId").ToString())==int.Parse(ViewState["SelId"].ToString()))?"selrowcontent":"rowcontent" %>">
                    <td style=" width :50px"><asp:ImageButton runat="server" ImageUrl="images/edit.jpg" ID="imgEdit" OnClick="EditClick" CommandArgument='<%# Eval("RoleId") %>'/></td>
                    <td style=" width :133px; text-align : left">&nbsp;<%# Eval("RoleName")%></td>
                    </tr>
                    </ItemTemplate>
                    </asp:Repeater> 
                    </table>
                    </asp:Panel>    
                </td>
                <td style="vertical-align:top  " >
                <strong class="pagename" >Role Management</strong>
                <mtm:Message runat="server" ID="Msgbox" /> 
                    <asp:Panel ID="pnl_UserLogin" runat="server" Width="100%" Visible="False">
                    <table border="0" cellpadding="0" cellspacing="4" style="text-align: center" width="100%">
                    <tr>
                    <td colspan="2" style =" background-color :gray">&nbsp;</td>
                    </tr>
                      <tr>
                      <td colspan="2">
                          <asp:HiddenField ID="HiddenRoleName" runat="server" />
                      </td>
                                                            </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px" class="style1">
                              Role Name:</td>
                          <td style="text-align: left">
                              <asp:TextBox ID="txtRoleName" runat="server" CssClass="required_box" MaxLength="15" Width="235px"></asp:TextBox>
                              <span class="required">*</span></td>
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
                            <div style="float: left; width :690px; ">
                                <div style="" >
                                    <table cellpadding="0" cellspacing ="0" width="100%" border="1">
                                    <tr class="header" >
                                        <td style=" width :200px">Page Name</td>
                                        <td style=" width :70px"><input type="checkbox" value="false" onclick="check(this,'td1')"/>View</td>
                                        <td style=" width :70px"><input type="checkbox" value="false" onclick="check(this,'td2')"/>Add</td>
                                        <td style=" width :70px"><input type="checkbox" value="false" onclick="check(this,'td3')"/>Update</td>
                                        <td style=" width :70px"><input type="checkbox" value="false" onclick="check(this,'td4')"/>Delete</td>
                                        <td style=" width :70px"><input type="checkbox" value="false" onclick="check(this,'td5')"/>Print</td>
                                        <td style=" width :70px"><input type="checkbox" value="false" onclick="check(this,'td6')"/>Verify</td>
                                        <td style=" width :70px"><input type="checkbox" value="false" onclick="check(this,'td7')"/>Verify2</td>
                                    </tr>
                                    </table>
                                </div>
                                <asp:Panel runat="server" ID="Panel2" Width="100%" style=" overflow-x:hidden; overflow-y:scroll; height:295px; " >
                                <table style="" cellpadding ="0" cellspacing ="0" width="100%" border ="1">
                                <asp:Repeater runat="server" ID="rpt_Data1">
                                    <ItemTemplate >
                                    <tr>
                                        <td style=" width :200px;text-align : left">&nbsp;<%# Eval("PageName")%><asp:HiddenField runat="server" ID="pageid" value='<%# Eval("PageId") %>'/></td>
                                        <td style=" width :70px" name="td1" id="td1"><asp:CheckBox runat="server" ID="chkView" Checked='<%# Eval("IsView") %>'/></td>
                                        <td style=" width :70px" name="td2" id="td2"><asp:CheckBox runat="server" ID="chkAdd" Checked='<%# Eval("IsAdd") %>'/></td>
                                        <td style=" width :70px" name="td3" id="td3"><asp:CheckBox runat="server" ID="chkUpdate" Checked='<%# Eval("IsUpdate") %>'/></td>
                                        <td style=" width :70px" name="td4" id="td4"><asp:CheckBox runat="server" ID="chkDelete" Checked='<%# Eval("IsDelete") %>'/></td>
                                        <td style=" width :70px" name="td5" id="td5"><asp:CheckBox runat="server" ID="chkPrint" Checked='<%# Eval("IsPrint") %>'/></td>
                                        <td style=" width :70px" name="td6" id="td6"><asp:CheckBox runat="server" ID="chkVerify" Checked='<%# Eval("IsVerify") %>'/></td>
                                        <td style=" width :70px" name="td7" id="td7"><asp:CheckBox runat="server" ID="chkVerify2" Checked='<%# Eval("IsVerify2") %>'/></td>
                                    </tr>
                                    </ItemTemplate>
                                </asp:Repeater> 
                                </table>
                                </asp:Panel> 
                            </div>    
                            <div style="float: right; width:180px;">
                            <div style=" padding-right :17px">
                                  <table border="1" cellpadding="0" cellspacing="0" width="100%">
                                      <tr class="header">
                                          <td style=" width :180px">
                                            <input onclick="check(this,'td9')" type="checkbox" value="false" /> Assign Users
                                          </td>
                                      </tr>
                                  </table>
                              </div>
                              <asp:Panel runat="server" ID="Panel1" Width="100%" style=" overflow-x:hidden; overflow-y:scroll; height:295px; " >
                              <table border="1" cellpadding="0" cellspacing="0" style="" width="100%">
                                  <asp:Repeater ID="rprUsers" runat="server">
                                      <ItemTemplate>
                                          <tr>
                                              <td style=" width :180px;text-align : left" name='td9' id='td9'>
                                                 <asp:CheckBox ID="chkPer" runat="server" Checked='<%# Eval("Permission") %>' />
                                                  <%# Eval("UserID")%><asp:HiddenField ID="hfdLoginId" runat="server"  value='<%# Eval("LoginId") %>' />
                                              </td>
                                          </tr>
                                      </ItemTemplate>
                                  </asp:Repeater>
                              </table>
                              </asp:Panel>
                            </div>
                            </td>
                        </tr>
                    </table>
                    </asp:Panel>
                    <table style="width: 100%">
                            <tr>
                                <td style="text-align: right">
                                <asp:Button ID="btn_Add" runat="server" CssClass="btn" 
                            Text="Add" Width="59px" OnClick="btn_Add_Click" CausesValidation="False" />
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
<%# Eval("RoleName")%>