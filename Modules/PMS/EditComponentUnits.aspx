<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditComponentUnits.aspx.cs" Inherits="EditComponentUnits" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eMANAGER</title>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
      <link href="../../css/app_style.css" rel="Stylesheet" type="text/css" />
     <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">         
        function refreshonedit() {
            window.opener.reloadunits();
        }
        function fncInputNumericValuesOnly(evnt) {
             if (!(event.keyCode == 45 || event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                 event.returnValue = false;
             }
         }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style=" text-align :left; vertical-align : top;" >
        <table border="0" cellpadding="0" cellspacing="0" style="border: #4371a5 1px solid; text-align:center" width="100%">
            <tr>
                <td align="center"  class="text headerband" >
                    Edit Units&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <table style="background-color:#f9f9f9" border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                        <td style="padding-right: 5px; padding-left: 5px;">
                        <div style="width:100%; border:0px solid #000; height:380px;overflow:auto; overflow-y:hidden" >
                        <asp:UpdatePanel runat="server" id="up1">
                        <ContentTemplate>
                        <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                        <td align="left">
                        <table width="100%" border="0">
                        <tr>
                        <td ><asp:Label ID="lblComponent" runat="server" Font-Bold="true"></asp:Label></td>
                        <td ><asp:Label ID="lblMessage" Text="" CssClass="error_msg"  runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                        <td colspan="2">
                        <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                    <colgroup>
                        <col style=" text-align:left;width:340px;" />
                        <col style="width:100px;" /> 
                        <col style="width:140px;" />                      
                        <col style="width:17px;" />                        
                        <tr align="left" class= "headerstylegrid">
                            <td width="340px;">Component</td> 
                            <td width="100px;">Existing Units</td>   
                            <td width="140px;">Add/Remove Units</td>                            
                            <td width="17px;"></td>
                        </tr>
                    </colgroup>
                </table>
                        <div style="OVERFLOW-Y: scroll; OVERFLOW-X: hidden; WIDTH: 100%; HEIGHT: 300px ; text-align:center;" class="scrollbox">
                           <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
                                <colgroup>
                                    <col style="width:340px;" />
                                    <col style="width:100px;" />
                                    <col style="width:140px;" />                        
                                    <col style="width:17px;" />
                                </colgroup>
                                <asp:Repeater ID="rptComponents" runat="server">
                                    <ItemTemplate>
                                            <tr class="row">
                                            <td align="left">
                                                <asp:Label ID="lblComponent"  Text='<%# Eval("Component")%>' runat="server"></asp:Label>
                                                <asp:HiddenField ID="hfComponentId" Value='<%#Eval("ComponentId") %>' runat="server" />
                                            </td>
                                            <td><asp:Label ID="lblExtUnits" Text='<%#Eval("ExistingUnits")%>' runat="server"></asp:Label></td>
                                            <td align="center">
                                                <asp:TextBox ID="txtUnits" Width="60px" MaxLength="3" onkeypress="fncInputNumericValuesOnly(event)" runat="server"></asp:TextBox>
                                            </td>
                                            <%=(Request.UserAgent.Contains("MSIE 7.0"))?"<td style='width:17px'></td>":""%>
                                        </tr>
                                    </ItemTemplate>  
                                    <AlternatingItemTemplate>
                                            <tr class="alternaterow">
                                            <td align="left">
                                                <asp:Label ID="lblComponent" Text='<%# Eval("Component")%>' runat="server"></asp:Label>
                                                <asp:HiddenField ID="hfComponentId" Value='<%#Eval("ComponentId") %>' runat="server" />
                                            </td>
                                            <td><asp:Label ID="lblExtUnits" Text='<%#Eval("ExistingUnits")%>' runat="server"></asp:Label></td>
                                            <td align="center">
                                                <asp:TextBox ID="txtUnits" Width="60px" MaxLength="3" onkeypress="fncInputNumericValuesOnly(event)" runat="server"></asp:TextBox>
                                            </td>
                                            <td style="width:17px"></td>
                                        </tr>
                                    </AlternatingItemTemplate>                        
                                </asp:Repeater>
                            </table>
                           </div>
                        </td>
                        </tr>
                        <tr><td colspan="2" align="right"><asp:Button ID="btnAddCompUnits" Text="Save" runat="server" OnClientClick="javascript:return confirm('This will Edit given units with existing.\nAre You Sure to Edit Units?');" onclick="btnAddCompUnits_Click" />
                        </td>
                        </tr>
                        <%--<tr ><td colspan="2"></td></tr>--%>
                        </table> 
                        </td>
                        </tr>                        
                        </table> 
                        </ContentTemplate>
                        </asp:UpdatePanel> 
                        </div>
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
