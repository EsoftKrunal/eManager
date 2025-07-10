<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserVesselManagement.aspx.cs" Inherits="UserVesselManagement" %>
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
                    <asp:Panel runat="server" ID="pnlList" Width="100%" style=" overflow-x:hidden; overflow-y:scroll" >
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
                <strong class="pagename" >User-Vessel Assignment</strong>
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
                          <td align="right" style="padding-right:15px; vertical-align: top;">
                              Vessel List :</td>
                          <td style="text-align: center">
                              <div style=" padding-right :17px">
                                  <table border="1" cellpadding="0" cellspacing="0" width="100%">
                                      <tr class="header">
                                          <td style=" width :200px">
                                              Page Name</td>
                                          <td style=" width :80px">
                                              <input onclick="check(this,'td1')" type="checkbox" value="false" />Allow</td>
                                      </tr>
                                  </table>
                              </div>
                              <asp:Panel runat="server" ID="Panel1" Width="100%" style=" overflow-x:hidden; overflow-y:scroll; height:360px; " >
                              <table border="1" cellpadding="0" cellspacing="0" style="" width="100%">
                                  <asp:Repeater ID="rpt_Data1" runat="server">
                                      <ItemTemplate>
                                          <tr>
                                              <td style=" width :200px;text-align : left">
                                                  &nbsp;<%# Eval("VesselName")%><asp:HiddenField ID="hfdVesselId" runat="server" 
                                                      value='<%# Eval("VesselId") %>' />
                                              </td>
                                              <td ID="td1" name="td1" style=" width :80px">
                                                  <asp:CheckBox ID="chkView" runat="server" Checked='<%# Eval("Permission") %>' />
                                              </td>
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