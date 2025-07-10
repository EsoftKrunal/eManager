<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BudgetCommentReport.aspx.cs" Inherits="BudgetCommentReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
    <link href="../../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
   <%-- <link href="~/CSS/style.css" rel="Stylesheet" type="text/css" />--%>
    <style type="text/css">
        .style1
        {
            height: 38px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
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
                            <col width="75px;" />
                            <col width="55px;" />
                            <col width="100px;" />
                            <col width="130px;" />
                            <col width="170px;" />
                            <col />
                            <col />
                                <tr align="center" class="text headerband">
                                    <td colspan="7">
                                        Budget Comments Report
                                    </td>
                                </tr>
                                <tr align="center" class= "headerstylegrid">
                                    <td >Year</td>
                                    <td >Month</td>
                                    <td >Company</td>
                                    <td >Vessel</td>
                                    <td > </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr style="text-align:center;">
                                    <td>
                                        <asp:DropDownList ID="ddlyear" runat="server" Width="70px" ></asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlMonth" runat="server" Width="50px">
                                        <asp:ListItem Text="Jan" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Feb" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Mar" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Apr" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="Jun" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="Jul" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="Aug" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="Sep" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="Oct" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="Nov" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="Dec" Value="12"></asp:ListItem>
                                        </asp:DropDownList>
                                        
                                    </td>
                                    <td style="text-align:left;">
                                        <asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" AutoPostBack="true" Width="80px" ></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                            ControlToValidate="ddlCompany" ErrorMessage="*" ></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlVessel" runat="server" ></asp:DropDownList>
                                    </td>
                                    <td  valign="middle">
                                        <%--<asp:DropDownList ID="ddlReportLevel" runat="server" Width="160px" >
                                            <asp:ListItem  Value=""> Select</asp:ListItem>
                                            <asp:ListItem  Value="5"> Detail Activity Report</asp:ListItem>
                                            <asp:ListItem  Value="6"> CLS Report</asp:ListItem>
                                            <asp:ListItem  Value="7"> Budget Comment Report</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfreport" runat="server" ControlToValidate="ddlReportLevel" ErrorMessage="*" ></asp:RequiredFieldValidator>--%>
                                    </td>
                                    <td style="text-align:right;">
                                        <asp:Button ID="imgPrint" runat="server"   onclick="imgPrint_Click" Text="Show" CssClass="btn" /></td>
                                    <td style="text-align:center;">
                                        <asp:Label ID="lblTargetUtilisation" runat="server" ForeColor="Red" ></asp:Label>
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
