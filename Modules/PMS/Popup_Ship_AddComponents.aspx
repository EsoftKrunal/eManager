<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Popup_Ship_AddComponents.aspx.cs" Inherits="Popup_Ship_AddComponents" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <script src="JS/Common.js" type="text/javascript"></script>
    <script type="text/javascript" >
        function fncInputIntegerValuesOnly(evnt) {
            if (!(event.keyCode == 46 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                event.returnValue = false;
            }
        }
        function refresh(CompCode) { 
            alert(CompCode);
            window.opener.reloadtree(CompCode);
        } 
//        function reloadComponents(CompCode) {
//            document.getElementById('hfSearchCode').value = CompCode;
//            __doPostBack('btnSearchedCode', '');
//        }       
    </script>
    <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
     <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
         <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">
            <tr>
                <td align="center"  class="text headerband" >
                    Add Components&nbsp;</td>
           </tr>
        <tr>
         <td>
            <asp:UpdatePanel runat="server" id="up1">
                        <ContentTemplate>   
                        <div style="background-color:#f9f9f9; border:#8fafdb 1px solid; border-top:#8fafdb 0px solid;width:100% " >
                          <table cellpadding="2" style="float:left" cellspacing="0" width="100%">
                                           <tr>
                                           <td><asp:Label ID="lblMessage" runat="server" Font-Bold="true"></asp:Label></td>
                                           </tr>
                                            <tr>
                                            <td>
                                              <table cellspacing="0" rules="all" border="0" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                                <colgroup>
                                                  <col style="text-align :left" width="100px" />
                                                  <col style="text-align :left" width="300px"/>
                                                  <col style="text-align :left" width="40px" />
                                                  <col width="17px" />
                                                  <tr class="headerstylegrid">
                                                    <td width="100px">
                                                        Code</td>
                                                    <td width="300px">
                                                        Component Name</td>
                                                    <%--<td>
                                                        Parent Component</td>--%>
                                                    <td width="40px"></td>  
                                                    <td width="17px"></td>     
                                                  </tr>
                                                 </colgroup>
                                               </table>
                                               
                                               <div id="dvScroll"  onscroll="SetScrollPos(this)" style="width :100%; overflow-y:scroll; overflow-x:hidden; height :317px;">                        
                                        <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                            <colgroup>
                                                <col style="text-align :left" width="100px" />
                                                <col style="text-align :left" width="300px"/>
                                                <col style="text-align :left" width="40px" />
                                                <col width="17px" />
                                            </colgroup>
                                            <asp:Repeater ID="rptCompoenents" runat="server">
                                                <ItemTemplate>
                                                    <tr class="row">
                                                        <td style="text-align:left">
                                                            <%--<asp:LinkButton ID="btnJobName" runat="server" CommandArgument='<%# Eval("ComponentCode") %>' OnClick="btnComponent_Click" Text='<%# Eval("ComponentCode") %>'></asp:LinkButton>--%>
                                                            <%# Eval("ComponentCode") %>
                                                        </td>
                                                        <td style="text-align:left">
                                                            <%# Eval("ComponentName")%></td>
                                                        <td style="text-align:center">
                                                           <asp:Button ID="btnAddComponents" CommandArgument='<%# Eval("ComponentCode") %>' Text="Add" OnClientClick="javascript:return confirm('Are you sure to add this component?');" OnClick="btnAddComponents_Click" CssClass="btn" runat="server" />
                                                        </td>    
                                                        <%--<td style="text-align:left">
                                                            <%# Eval("Parent")%></td>--%>
                                                        <td style="width:17px"></td>    
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            </table>
                                        </div>                                            
                                            </td>
                                            </tr>                                                                                        
                                           </table>
                        </div>                         
                        </ContentTemplate>
                        </asp:UpdatePanel>
        </td> 
        </tr>
        <tr>
           <td>
               <div style="padding-top:5px;">
                    <uc1:MessageBox ID="MessageBox1" runat="server" />
                </div>
           </td>
        </tr>
        </table>
       
     </div>
    </form>
</body>
</html>
