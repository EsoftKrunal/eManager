<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BudgetVarianceReport.aspx.cs" Inherits="BudgetVarianceReport" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
     <link href="../../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        function ShowVARReport()
        {
            window.open( "../POVarianceReport.aspx?Company=" + document.getElementById('ddlCompany').value + "&VesselID=" + document.getElementById('ddlVessel').value + "&ForMonth=" + document.getElementById('ddlMonth').value + "&ForYear=" + document.getElementById('ddlyear').value + "&Mode=2");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>   
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
        <tr>
            <td>
                <table cellpadding="4" cellspacing="0" width="100%" border="0" style="border-collapse:collapse;">
                <tr>
                   <td class="header" style=" padding:4px;"  colspan="5" >Reports<asp:ImageButton runat="server" ID="btnBack" OnClick="btnBack_Click" Visible="false"  ImageUrl="~/Images/home.png" style="float :right; padding-right :5px; background-color : Transparent " CausesValidation="false"/></td>
                </tr>
                <tr>
                    <td>
                                <table cellpadding="2" cellspacing="1" width="100%" border="1" style="border-collapse:collapse;">
                                    <colgroup>
                                        <col width="200px;" />
                                        <col width="200px;" />
                                        <col width="100px;" />
                                        <col width="170px;" />
                                        <col />
                                        <col />
                                        <tr align="center" class="text headerband">
                                            <td colspan="6">
                                                Variance Summary Report
                                            </td>
                                        </tr>
                                        <tr align="center" class="header">
                                            <td>
                                                Search By
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFleetOwnerText" runat="server" Text="Fleet"></asp:Label>
                                            </td>
                                            <td>
                                                Year</td>
                                            <td>
                                                Month</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr style="text-align:center;">
                                            <td>
                                                <asp:DropDownList ID="ddlSearchBy" runat="server" AutoPostBack="true" 
                                                    OnSelectedIndexChanged="ddlSearchBy_OnSelectedIndexChanged" Width="150px">
                                                    <asp:ListItem Text="Fleet" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Owner" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="text-align:center;">
                                                <asp:DropDownList ID="ddlFleet" runat="server" AutoPostBack="false" 
                                                    Width="150px">
                                                    <asp:ListItem Text="&lt; Select &gt;" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text=" Fleet 1" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text=" Fleet 2" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text=" Fleet 3" Value="3"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlOwner" runat="server" AutoPostBack="false" 
                                                    Visible="false" Width="170px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                    ControlToValidate="ddlFleet" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                            <asp:DropDownList runat="server" id="ddlYear" Width="60px"></asp:DropDownList>
                                            </td>
                                            <td valign="middle">
                                            <asp:DropDownList runat="server" id="ddlMonth" Width="50px"></asp:DropDownList>
                                            </td>
                                            <td style="text-align:right;">
                                                <asp:Button ID="btnReport" runat="server" 
                                                    Text="Show Report" onclick="btnReport_Click" CssClass="btn" />
                                            </td>
                                            <td style="text-align:center;">
                                                &nbsp;</td>
                                        </tr>
                                    </colgroup>
                            </table>   
                            
                                <iframe runat="server" id="iframe" width="100%" height="380px" bordercolor="black" frameborder="1">
                                </iframe>               
                            
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
