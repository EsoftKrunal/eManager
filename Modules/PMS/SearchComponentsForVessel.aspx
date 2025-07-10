<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchComponentsForVessel.aspx.cs" Inherits="SearchComponentsForVessel" %>
<%@ Register Src="UserControls/Left.ascx" TagName="Left" TagPrefix="uc2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
    <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <script src="JS/Common.js" type="text/javascript"></script>
    <script type="text/javascript" >
        function fncInputIntegerValuesOnly(evnt) {
            if (!(event.keyCode == 46 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                event.returnValue = false;
            }
        }
        function refresh(compcode) {            
            window.opener.reloadComponents(compcode);
            self.close();
        }       
    </script>
       <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <script src="JS/Common.js" type="text/javascript"></script>
    <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
     <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" defaultbutton="btnSearch" defaultfocus="txtCompCode" runat="server">
    <div style="text-align: center;font-family:Arial;font-size:12px;">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
         <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="99.9%">
            <tr>
                <td align="center"  class="text headerband" >
                   
                    Search Components&nbsp;</td>
           </tr>
        <tr>
         <td>
            <asp:UpdatePanel runat="server" id="up1">
                        <ContentTemplate>   
                        <div style="background-color:#f9f9f9; border:#8fafdb 1px solid; border-top:#8fafdb 0px solid; height:417px; width:100% " >
                          <table cellpadding="2" style="float:left" cellspacing="0" width="100%">
                                           <tr>
                                           <td colspan="9"><asp:Label ID="lblMessage" runat="server" CssClass="error_msg"></asp:Label></td>
                                           </tr>                                                                                                                                     
                                               <tr>
                                              <td style="text-align:left;width:110px;">Component Code :</td>
                                              <td style="text-align:left;width:120px;" ><asp:TextBox ID="txtCompCode" onkeypress="fncInputIntegerValuesOnly(event)" runat="server" Width="100px" MaxLength="12"></asp:TextBox></td>
                                              <td style="text-align:left;width:120px;">Component Name :</td>
                                              <td style="text-align:left;width:180px;"><asp:TextBox ID="txtCopmpname" runat="server" Width="130px" MaxLength="50"></asp:TextBox></td>
                                              <td style="text-align:left"><asp:CheckBox ID="chkClass" runat="server" Text="Class EQIP" /></td>
                                              <td style="text-align:left"><asp:TextBox ID="txtClassCode" runat="server" Text="" MaxLength="30" Width="80px"/></td>
                                              <td style="text-align:left"><asp:CheckBox ID="chkCritical" Text="Critical EQIP" runat="server" /></td>
                                              <td style="text-align:left"><asp:CheckBox ID="chkInactive" Text="Inactive" runat="server" /></td>
                                              <td><asp:Button runat="server" style=" font-weight:bold; width:100px" CssClass="btn" ID="btnSearch" Text="Search" OnClick="SearchComponent_Click" /></td>
                                               </tr>
                                            <tr>
                                            <td colspan="8" style="text-align:center" >
                                            <asp:Label ID="lblSearchRecords" style="color:Red; font-weight:bold; font-size:12px" runat="server"></asp:Label>
                                            </td>
                                            </tr>
                                            <tr>
                                            <td id="tdSearchResult" colspan="9" runat="server">
                                               <table cellspacing="0" rules="all" border="0" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                                <colgroup>
                                                  <col style="text-align :left" width="100px" />
                                                  <col style="text-align :left" width="300px" />
                                                  <col width="17px" />
                                                  <tr class= "headerstylegrid">
                                                    <td width="100px">Code</td>
                                                    <td width="300px">Component Name</td>
                                                    <td width="17px"></td>
                                                  </tr>
                                                 </colgroup>
                                               </table>
                                               <div id="dvScroll"  onscroll="SetScrollPos(this)" style="width :100%; overflow-y:scroll; overflow-x:hidden; height :340px;">                        
                                        <table cellspacing="0" rules="all" border="1" cellpadding="1" style="width:100%;border-collapse:collapse;">
                                            <colgroup>
                                                <col style="text-align :left" width="100px" />
                                                <col style="text-align :left" width="300px" />
                                                <col width="17px" />
                                            </colgroup>
                                            <asp:Repeater ID="rptCompoenents" runat="server">
                                                <ItemTemplate>
                                                    <tr class="row">
                                                        <td style="text-align:left">
                                                            <asp:LinkButton ID="btnJobName" runat="server" CommandArgument='<%# Eval("ComponentCode") %>' OnClick="btnComponent_Click" Text='<%# Eval("ComponentCode") %>'></asp:LinkButton>
                                                        </td>
                                                        <td style="text-align:left">
                                                            <%# Eval("ComponentName")%>&nbsp;<%#"<span class='CriticalType_" + Eval("CriticalType").ToString() + "'>[" + Eval("CriticalType").ToString() + "]</span>"%><br />
                                                           <span style ="color :Blue;font-size:10px;"><i><%# Eval("Parent")%></i></span>
                                                        </td>
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
        </table>
       
     </div>
    </form>
</body>
</html>
