<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DetailActivityReport.aspx.cs" Inherits="DetailActivityReport"  %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
    
    <link href="../../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            height: 38px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div style="font-family:Verdana, Arial, Helvetica, sans-serif;font-size:12px;">
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
        <tr>
       
            <td>
                <table cellpadding="4" cellspacing="0" width="100%" border="0" style="border-collapse:collapse;">
                <%--<tr>
                   <td class="text headerband" style=" padding:4px;"  colspan="5" >Invoice Payment Details</td>
                </tr>--%>
                <tr>
                    <td>
                        <table cellpadding="2" cellspacing="1" width="100%" border="1" style="border-collapse:collapse;">
                            
<colgroup>     
                            <col width="250px;" />
                            <col width="200px;" />               
                            <col width="120;" />
                            <col width="150px;" />
                           
                            <col width="170px;" />
                            <col />
                            <col />
    </colgroup>
                           <%-- <tr align="center" class="header">
                                <td colspan="7">
                                    
                                </td>
                            </tr>--%>
                             <tr align="center" class="header">
                                     <td >Company</td>
                                    <td >Vessel</td>
                                    <td >Year</td>
                                    <td >Month</td>
                                   
                                    <td > </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr style="text-align:center;">
                                  
                                    <td style="text-align:center;width:250px;">
                                        <asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" AutoPostBack="true" Width="200px" ></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                            ControlToValidate="ddlCompany" ErrorMessage="*" ></asp:RequiredFieldValidator>
                                    </td>
                                    <td style="text-align:center;width:225px;">
                                        <asp:DropDownList ID="ddlVessel" runat="server" Width="200px" ></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                            ControlToValidate="ddlVessel" ErrorMessage="*" ></asp:RequiredFieldValidator>
                                    </td>
                                      <td style="text-align:center;width:120px;">
                                        <asp:DropDownList ID="ddlyear" runat="server" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="ddlyear_SelectedIndexChanged" ></asp:DropDownList>
                                    </td>
                                    <td>
                                        <div style="float:left;padding-left:10px;vertical-align:top;">
                                            <asp:DropDownList ID="ddlMonth" runat="server" Width="75px">
                                        </asp:DropDownList>
                                        </div>
                                        
                               <%--             -

                                        <div style="float:right;padding-right:10px;vertical-align:top;">
                                            <asp:DropDownList ID="ddlToMonth" runat="server" Width="50px">
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
                                        </div>
                                        
                                        
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="*" ControlToCompare="ddlToMonth"  ControlToValidate="ddlMonth" Type="Integer" Operator="LessThanEqual" ></asp:CompareValidator>--%>
                                        
                                    </td>
                                    <td  valign="middle">
                                        <%--<asp:DropDownList ID="ddlReportLevel" runat="server" Width="160px" >
                                            <asp:ListItem  Value=""> Select</asp:ListItem>
                                            <asp:ListItem  Value="5"> Detail Activity Report</asp:ListItem>
                                            <asp:ListItem  Value="6"> CLS Report</asp:ListItem>
                                            <asp:ListItem  Value="7"> Budget Comment Report</asp:ListItem>
                                        </asp:DropDownList>--%>
                                        <%--<asp:RequiredFieldValidator ID="rfreport" runat="server" ControlToValidate="ddlReportLevel" ErrorMessage="*" ></asp:RequiredFieldValidator>--%>
                                    </td>
                                    <td style="text-align:right;">
                                        <asp:Button ID="imgPrint" runat="server" CssClass="btn" Text="Show Report" onclick="imgPrint_Click" /></td>
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
