<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EventMaster.aspx.cs" Inherits="EventManagement_EventMaster" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="Menu_Event.ascx" TagName="leftmenu" TagPrefix="mtm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/tabs.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <title>Planned Maintenance System : Event Master</title> 
</head>
<body>
    <form id="form1" runat="server">
    <div>

     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
         
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr><td><%--<hm:HMenu runat="server" ID="menu2" />--%></td></tr>
        </table>
        <asp:UpdatePanel runat="server" id="up11">
        <ContentTemplate>
        <div class="box_withpad" style="min-height:450px">
        <table style="width :100%" border="0" cellpadding="0" cellspacing="0">
        <tr>     
        <td style="width:200px; text-align:left;">
            <mtm:leftmenu runat="server" ID="LefuMenu1" />
        </td>   
        <td style=" text-align :left; vertical-align : top;"> 
            <div>
            <table style="width :100%" cellpadding="0" cellspacing="0" border="0" height="465px">
            <tr>  
            <td>
             <div style="border:none;">
             <div class="box1">
                <table border="0" cellpadding="1" cellspacing="0" style="width :100%">
                    <tr>
                        <td style="width:100px; font-weight:bold; vertical-align:middle;">
                            Risk Topic :&nbsp;</td>
                        <td style="text-align:left;vertical-align:middle;">
                            <asp:TextBox ID="txtSearch" runat="server" Width="98%"></asp:TextBox>
                        </td>
                        <td style="width:200px">
                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
                            <asp:Button ID="btnAddEvent" runat="server" OnClick="btnAddEvent_Click" Text="Add Risk Topic" />
                       </td>
                    </tr>
                </table>
                </div>
                 
                <div class="dvScrollheader">  
                <table cellspacing="0" rules="all" border="0" cellpadding="0" style="width:100%;border-collapse:collapse;">
                        <colgroup>
                            <col style="width:15px;" />
                            <col style="width:50px;" />
                            <col />
                            <col style="width:20px;" />
                        </colgroup>
                        <tr>
                            <td style="text-align:center;"></td>
                            <td style="text-align:center;">Edit</td>
                            <td style="text-align:left;">Risk Topic</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </div>
                <div class="dvScrolldata" style="height: 398px;">
                    <table cellspacing="0" rules="all" border="1" cellpadding="0" style="width:100%;border-collapse:collapse;">
                        <colgroup>
                            <col style="width:15px;" />
                            <col style="width:50px;" />
                            <col />
                            <col style="width:20px;" />
                        </colgroup>
                        <asp:Repeater ID="rptEventMaster" runat="server">
                            <ItemTemplate>
                                <tr >
                                    <td style="text-align:left"></td>
                                    <td style="text-align:center"><asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%#Eval("EventId")%>' ImageUrl="~/Images/editX12.jpg" OnClick="btnEdit_Click" ToolTip="Edit" /></td>
                                    <td align="left"><%#Eval("EventName")%></td>
                                    <td>&nbsp;</td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
              
            
              <%-- ADD/ Edit Events --%>
              <div ID="dv_AddEditEvent" runat="server" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 100%;" visible="false">
                  <center>
                      <div style="position: absolute; top: 0px; left: 0px; height: 100%; width: 100%;background-color: Gray; z-index: 100; opacity: 0.4; filter: alpha(opacity=40)">
                      </div>
                      <div style="position: relative; width: 500px; height: 120px; padding: 3px; text-align: center; background: white; z-index: 150; top: 150px; border: solid 10px gray;">
                          <asp:UpdatePanel ID="up1" runat="server">
                              <ContentTemplate>
                                  <div>
                                      <table border="0" cellpadding="4" cellspacing="0" width="100%">
                                          <tr>
                                              <td colspan="2" 
                                                  style="text-align: center; background-color:#FFB870; font-size:14px; color:Blue;">
                                                  Add/ Edit Risk Topic
                                              </td>
                                          </tr>
                                          <tr>
                                              <td style="text-align:right; width:130px;">
                                                  Risk Topic :&nbsp;
                                              </td>
                                              <td style="text-align:left;">
                                                  <asp:TextBox ID="txtEventName" runat="server" Width="95%"></asp:TextBox>
                                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                      ControlToValidate="txtEventName" ErrorMessage="*" ValidationGroup="v1"></asp:RequiredFieldValidator>
                                              </td>
                                          </tr>
                                      </table>
                                      <div style="text-align:center; padding:3px;">
                                          <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                                      </div>
                                      
                                      <div style="text-align:center">
                                          <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" 
                                              style=" padding:3px; border:none; color:White; background-color:#2E9AFE; width:80px;" 
                                              Text="Save" ValidationGroup="v1" />
                                          <asp:Button ID="btnClose" runat="server" CausesValidation="false" 
                                              OnClick="btnClose_Click" 
                                              style=" padding:3px; border:none; color:White; background-color:Red; width:80px;" 
                                              Text="Close" />
                                      </div>
                                  </div>
                              </ContentTemplate>
                              <Triggers>
                                  <asp:PostBackTrigger ControlID="btnClose" />
                              </Triggers>
                          </asp:UpdatePanel>
                      </div>
                  </center>
              </div>
            
             </div>
            </td>
            </tr>
            </table>
            </div>
        </td>
        </tr>
        </table>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
