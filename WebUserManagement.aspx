<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebUserManagement.aspx.cs" Inherits="WebUserManagement" %>
<%@ Register Src="~/UserControls/MessageBox.ascx" TagName="Message" TagPrefix="mtm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Website User Management</title>
    <link type="text/css" href="StyleSheet.css" rel="Stylesheet" />   
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
            <td style="text-align: center; vertical-align : top; ">
            <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width:280px; vertical-align:top ; padding-bottom:20px; " >
                <div style=" padding-right :17px;" >
                <table cellpadding="0" cellspacing ="0" width="100%" border="1">
                    <tr class="header" >
                        <td style=" width :50px">Edit</td>
                        <td style=" width :50px">Delete</td>
                        <td style=" width :180px">[Owner]User Name</td>
                    </tr>
                </table>
                </div>
                <asp:Panel runat="server" ID="pnlList" Width="100%" style="overflow-x:hidden; height :400px; overflow-y:scroll" >
                <table style="" cellpadding ="0" cellspacing ="0" width="100%" border ="1">
                <asp:Repeater runat="server" ID="rpt_Data">
                <ItemTemplate >
                <tr class="<%# (int.Parse(Eval("UserId").ToString())==int.Parse(ViewState["SelId"].ToString()))?"selrowcontent":"rowcontent" %>">
                <td style=" width :50px"><asp:ImageButton runat="server" ImageUrl="images/edit.jpg" ID="imgEdit" OnClick="EditClick" CommandArgument='<%# Eval("UserId") %>'/></td>
                <td style=" width :50px"><asp:ImageButton runat="server" ImageUrl="images/delete.jpg" ID="imgDelete" OnClick="DeleteClick" CommandArgument='<%# Eval("UserId") %>' OnClientClick="return confirm('Are you sure to delete this User.')" /></td>
                <td style=" width :180px; text-align : left ">&nbsp;[<%# Eval("Owner") %>]<%# Eval("UserName") %></td>
                </tr>
                </ItemTemplate>
                </asp:Repeater> 
                </table>
                </asp:Panel>
                </td>
                <td style=" vertical-align : top" >
                <strong class="pagename" >Website User Management</strong>
                <mtm:Message runat="server" ID="Msgbox" /> 
                <asp:Panel ID="pnl_UserLogin" runat="server" Width="100%" Visible="False">
                <table border="0" cellpadding="0" cellspacing="0" style="text-align: center" width="100%">
                    <tr>
                      <td colspan="4" style=" background-color : Gray " >
                          &nbsp; &nbsp;</td>
                                                            </tr>
                      <tr>
                          <td colspan="4">
                              <asp:HiddenField ID="HiddenUserLogin" runat="server" />
                          </td>
                      </tr>
                      <tr>
                          <td align="right" 
                              style="padding-right: 15px; text-align: right; padding-left: 65px;">
                              User Name:</td>
                          <td style="text-align: left">
                              <asp:TextBox ID="txtUserId" runat="server" CssClass="required_box" 
                                  MaxLength="15" Width="180px"></asp:TextBox>
                          </td>
                          <td align="right" style="text-align: right; padding-right: 15px;">
                              Vessel Permission (MTM Inspection) :</td>
                          <td rowspan="4" style="text-align: left" valign="top">
                              <div class="input_box" style=" height :100px;width :182px; overflow-y:scroll ;border :solid 1px black">
                                  <asp:CheckBoxList ID="chkLst_INS" runat="server" Height="99px" Width="181px">
                                  </asp:CheckBoxList>
                              </div>
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right; ">
                              Password:</td>
                          <td style="text-align: left; ">
                              <asp:TextBox ID="txtPassword" runat="server" CssClass="required_box" 
                                  MaxLength="15" TextMode="Password" Width="180px"></asp:TextBox>
                              </td>
                          <td align="right" 
                              style="text-align: right; padding-right: 15px; color: #0e64a0;">
                              &nbsp;</td>
                      </tr>
                      <tr >
                          <td align="right" style="padding-right: 15px; text-align: right; height: 13px;">
                              Confirm Password:</td>
                          <td style="text-align: left; height: 13px;">
                              <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="required_box" 
                                  MaxLength="15" TextMode="Password" Width="180px"></asp:TextBox>
                          </td>
                          <td align="right" style="text-align: right; padding-right: 15px; height: 13px;">
                              &nbsp;</td>
                      </tr>
                      <tr >
                          <td align="right" style="padding-right: 15px; text-align: right; height: 13px;">
                              Owner :</td>
                          <td style="text-align: left; height: 13px;">
                              <asp:DropDownList ID="dddlOwner" runat="server" CssClass="input_box" 
                                  Width="185px">
                              </asp:DropDownList>
                          </td>
                          <td align="right" style="text-align: right; padding-right: 15px; height: 13px;">
                              &nbsp;</td>
                      </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right; height: 13px;">
                              &nbsp;</td>
                          <td style="text-align: left; height: 13px;">
                              &nbsp;</td>
                          <td align="right" style="text-align: right; padding-right: 15px; height: 13px;">
                              &nbsp; &nbsp;</td>
                          <td style="text-align: left; height: 13px;">
                              &nbsp;</td>
                      </tr>
                        <tr>
                            <td align="right" style="text-align: right; padding-right:15px; ">
                                Vessel Permission (VIMS) :</td>
                            <td style="text-align: left; ">
                                <div class="input_box" style=" height :100px;width :182px; overflow-y:scroll;border :solid 1px black ">
                                    <asp:CheckBoxList ID="chkLst_VIMS" runat="server" Height="99px" Width="181px">
                                    </asp:CheckBoxList>
                                </div>
                            </td>
                            <td align="right" style="text-align: right; ">
                                Vessel Permission (Shipsoft) :</td>
                            <td style="text-align: left; padding-right: 15px;">
                                <div class="input_box" style="height :100px;width :182px;overflow-y:scroll;border :solid 1px black">
                                    <asp:CheckBoxList ID="chkLst_Shipsoft" runat="server" Width="181px" >
                                    </asp:CheckBoxList>
                                </div>
                            </td>
                        </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">
                              </td>
                          <td style="text-align: left">
                              &nbsp;</td>
                          <td align="right" style="text-align: right">
                              </td>
                          <td style="text-align: left; ">
                          </td>
                        </tr>
                        <tr>
                            <td align="right" style="text-align: right; padding-right:15px">
                                <span lang="en-us">Available Modules :</span></td>
                            <td style="text-align: left;" colspan="3">
                                <div style="border :solid 1px #4371a5; width : 695px;" >
                                    <asp:CheckBoxList ID="chklstModules" RepeatColumns="3" runat="server" Width="100%" ></asp:CheckBoxList>
                                </div>
                            </td>
                        </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">
                              &nbsp;</td>
                          <td style="text-align: left">
                              &nbsp;</td>
                          <td align="right" style="text-align: right">
                              &nbsp;</td>
                          <td style="text-align: left; ">
                              &nbsp;</td>
                    </tr>
                    <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">E-Mail Address :</td>
                          <td style="text-align: left" colspan="3"><asp:TextBox ID="txtEmails" runat="server" CssClass="input_box" MaxLength="500" Width="70%"></asp:TextBox>
                          <span style='color:Red; font-style:italic;'>(if multiple use ';' to seperate.)</span>
                          </td>
                    </tr>
                    <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">
                              &nbsp;</td>
                          <td style="text-align: left">
                              &nbsp;</td>
                          <td align="right" style="text-align: right">
                              &nbsp;</td>
                          <td style="text-align: left; ">
                              &nbsp;</td>
                    </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">
                              Po User:</td>
                          <td style="text-align: left">
                              <asp:TextBox ID="txt_PoUser" runat="server" CssClass="input_box" MaxLength="40" Width="180px"></asp:TextBox>
                          </td>
                          <td align="right" style="padding-right: 15px; text-align: right">
                              Po Password:</td>
                          <td style="text-align: left">
                              <asp:TextBox ID="txt_PoPassword" runat="server" CssClass="input_box" 
                                  MaxLength="40" Width="180px"></asp:TextBox>
                          </td>
                      </tr>
                       <tr>
                        <td align="right" style="text-align: right; padding-right:15px">
                            &nbsp;</td>
                        <td style="text-align: left">
                            &nbsp;</td>
                        <td style="padding-right: 15px; text-align: right" align="right">
                            &nbsp;</td>
                        <td style="text-align: left">
                            &nbsp;</td>
                    </tr>
                      <tr>
                          <td align="right" style="padding-right: 15px; text-align: right">
                              Status:</td>
                          <td style="text-align: left">
                              <asp:DropDownList ID="ddlStatus" runat="server" CssClass="input_box" 
                                  Width="185px">
                                  <asp:ListItem Text="Active" Value="A"></asp:ListItem>
                                  <asp:ListItem Text="Inactive" Value="I"></asp:ListItem>
                              </asp:DropDownList>
                          </td>
                          <td style="padding-right: 15px; text-align: right">
                              Vessel Position File :</td>
                          <td style="text-align: left">
                              <asp:FileUpload ID="fileup" runat="server" />
                          </td>
                      </tr>
                      <tr>
                          <td align="right" style="text-align: right; padding-right:15px">
                              &nbsp;</td>
                          <td style="text-align: left">
                              &nbsp;</td>
                          <td style="padding-right: 15px; text-align: right">
                              &nbsp;</td>
                          <td style="text-align: left">
                              &nbsp;</td>
                      </tr>
                        <tr>
                        <td align="right" style="text-align: right; padding-right:15px">
                            Created By:</td>
                        <td style="text-align: left">
                            <asp:Label ID="txtCreatedBy" runat="server" CssClass="input_box" Width="180px"></asp:Label>
                        </td>
                        <td style="padding-right: 15px; text-align: right" align="right">
                            Created On:</td>
                        <td style="text-align: left">
                            <asp:Label ID="txtCreatedOn" runat="server" CssClass="input_box" Width="180px"></asp:Label>
                        </td>
                      </tr>
                    <tr>
                        <td align="right" style="text-align: right; padding-right:15px">
                            </td>
                        <td style="text-align: left">
                            &nbsp;
                        </td>
                        <td style="padding-right: 15px; text-align: right" align="right">
                            </td>
                        <td style="text-align: left">
                        </td>
                      </tr>
                   
                    <tr>
                        <td align="right" style="text-align: right; padding-right:15px">
                            Modified By:</td>
                        <td style="text-align: left">
                            <asp:Label ID="txtModifiedBy" runat="server" CssClass="input_box" Width="180px"></asp:Label>
                        </td>
                        <td align="right" style="padding-right: 15px; text-align: right">
                            Modified On:</td>
                        <td style="text-align: left">
                            <asp:Label ID="txtModifiedOn" runat="server" CssClass="input_box" Width="180px"></asp:Label>
                        </td>
                    </tr>
                   
                    <tr>
                        <td align="right" style="text-align: right; padding-right:15px">
                        </td>
                        <td style="text-align: left">
                            &nbsp;
                        </td>
                        <td align="right" style="padding-right: 15px; text-align: right">
                        </td>
                        <td style="text-align: left">
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
<%--OnRowDeleting="GridView_UserLogin_Row_Deleting"--%>