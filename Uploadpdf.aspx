<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Uploadpdf.aspx.cs" Inherits="Uploadpdf" %>
<%@ Register Src="~/UserControls/MessageBox.ascx" TagName="Message" TagPrefix="mtm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Upload User Manual (pfd)</title>
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
            <td style="text-align: center;">
            
            <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width:200px; vertical-align:top ; padding-bottom:20px; " >
                    <div style=" padding-right :17px;"  >
                    <table cellpadding="0" cellspacing ="0" width="100%" border="1">
                    <tr class="header" >
                    <td style=" width :50px">Edit</td>
                    <td style=" width :133px">Application Name</td>
                    </tr>
                    </table>
                    </div>
                    <asp:Panel runat="server" ID="pnlList" Width="100%" style=" overflow-x:hidden; overflow-y:scroll" >
                    <table style="" cellpadding ="0" cellspacing ="0" width="100%" border ="1">
                    <asp:Repeater runat="server" ID="rpt_Data">
                    <ItemTemplate >
                   <tr class="<%# (int.Parse(Eval("ApplicationId").ToString())==int.Parse(ViewState["SelId"].ToString()))?"selrowcontent":"rowcontent" %>">
                    <td style=" width :50px"><asp:ImageButton runat="server" ImageUrl="images/edit.jpg" ID="imgEdit" OnClick="EditClick" CommandArgument='<%# Eval("ApplicationId") %>'/></td>
                    <td style=" width :133px; text-align : left">&nbsp;<%# Eval("ApplicationName")%></td>
                    </tr>
                    </ItemTemplate>
                    </asp:Repeater> 
                    </table>
                    </asp:Panel>    
                </td>
                <td style="vertical-align:top  " >
                <strong class="pagename" >Upload user manual (pdf)</strong>
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
                              Users List :</td>
                          <td style="text-align: center">
                              <div style=" padding-right :17px">
                                  <table border="1" cellpadding="0" cellspacing="0" width="100%">
                                      <tr class="header">
                                          <td style=" width :50%">
                                              Module Name</td>
                                          <td style=" width :50%">
                                              Upload user manual</td>
                                      </tr>
                                  </table>
                              </div>
                              <asp:Panel runat="server" ID="Panel1" Width="100%" style=" overflow-x:hidden; overflow-y:scroll; height:358px; " >
                              <table border="1" cellpadding="0" cellspacing="0" style="" width="100%">
                                  <asp:Repeater ID="rpt_Data1" runat="server">
                                      <ItemTemplate>
                                          <tr>
                                              <td style=" width :50%;text-align : left">
                                                  &nbsp;<%# Eval("ModuleName")%><asp:HiddenField ID="hfdModuleId" runat="server" 
                                                      value='<%# Eval("ModuleId") %>' />
                                              </td>
                                              <td ID="td1" name="td1" style=" width :50%; text-align :center; padding-left :100px ">
                                              <div style="float :left"><asp:FileUpload runat="server" ID="flp1" /> </div>
                                                  <img runat="server" style="cursor:pointer" onclick='<%# getFilePath(Eval("ModuleId").ToString())%>' visible='<%# HasFile(Eval("ModuleId").ToString())%>' src="~/images/help.png"  />
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
                                <td style="text-align: right; padding-right : 20px;">
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