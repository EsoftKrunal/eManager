<%@ Page Language="C#" AutoEventWireup="true" CodeFile="POReport.aspx.cs" Inherits="POReport" %>
<%--<%@ Register src="~/UserControls/AccountDropDown.ascx" tagname="ACCDropDown" tagprefix="uc1" %>--%>
<%@ Register src="~/Modules/Purchase/UserControls/VesselDropDown.ascx" tagname="VSlDropDown" tagprefix="uc1" %>
<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        EMANAGET
    </title>
    <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .FixedExpensesToolbar
        {
         position: fixed;
         margin: 0px 0px 0px 0px;
         z-index: 100;
         background-color: #d3d7e4;
        }
        .style1
        {
            width: 7px;
            text-align: center;
        }
        </style> 
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <div style="font-family:Arial;font-size:12px">
        <div class="Text headerband">
            PO Report
        </div>
    <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse;">
            <tr>
                <td>
                  <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                        <ContentTemplate>
                        <table cellspacing="0" rules="all" border="1" cellpadding="4" style="width:100%;border-collapse:collapse; ">
                            <tr class="header">
                                <td>Company</td>
                                <td>Vessel</td>
                                <td>Request Type </td>
                                <td>Listing Type</td>
                                <td>Period</td>
                               
                            </tr>
                            <tr>
                                <td style="text-align: center; width:230px;">
                                    <asp:DropDownList ID="ddlCompany" runat="server" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" AutoPostBack="true" Width="220px" ></asp:DropDownList>
                                </td>
                                <td style="width:260px;">
                                    <%--<div class="scrollbox" style="width:100%; height:100px; overflow-x:hidden; overflow-y:scroll; display:none;" >
                                        <asp:CheckBoxList ID="chkShip" runat="server" RepeatColumns="1" ></asp:CheckBoxList>
                                    </div>--%>
                                    <asp:DropDownList ID="ddlVessel" runat="server" ></asp:DropDownList>
                                    <%--<uc1:VSlDropDown ID="ddlVessel" runat="server" IncludeInActive="false" />--%>
                                </td>
                                <td style ="text-align :center; width :150px;">
                                    <asp:DropDownList ID="ddlPrtype" runat="server" Width="123px">
                                        <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Store" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Spares" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Landed Goods" Value="3"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rdoReportBy" runat="server" RepeatDirection="Horizontal" >
                                        <asp:ListItem Text="PO" Value="1" Selected="True" ></asp:ListItem>
                                        <asp:ListItem Text="Item" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td style="text-align: center;">
                                    <table ID="tblDate" border="0" cellpadding="0" cellspacing="0" rules="all" style="width:100%;border-collapse:collapse;">
                                        <tr>
                                            <td style="width:70px;">
                                                From
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlFromyear" runat="server" Width="60px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlFromMonth" runat="server" Width="60px">
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
                                            <td>
                                                -</td>
                                            <td>
                                                <asp:DropDownList ID="ddlToyear" runat="server" Width="60px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlToMonth" runat="server" Width="60px">
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
                                        </tr>
                                    </table>
                                </td>
                               
                            </tr>
                            <tr>
                                <td colspan="5" style="text-align: center; " class="header">
                                    Search By</td>
                            </tr>
                            <tr style="height:60px;">
                                <td colspan="2" style="text-align: center; ">
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <asp:RadioButtonList ID="rdoSearchBy" runat="server" AutoPostBack="true" 
                                                    OnSelectedIndexChanged="rdoSearchBy_OnSelectedIndexChanged" 
                                                    RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Po Listing By Period" Value="1" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Po Listing By Vendor" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Po Listing By Acct Code" Value="3"></asp:ListItem>                                                    
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>                                    
                                </td>
                                <td align="center">
                                    <asp:CheckBox ID="chkBreakDown" runat="server" Text="Breakdown/Unbudgted" />
                                </td>
                                <td style="text-align: right; vertical-align:bottom " colspan="2">
                                    <table ID="tblVendor" runat="server" border="0" cellpadding="2" cellspacing="0" 
                                        rules="all" style="width:100%;border-collapse:collapse;" visible="false">
                                        <tr>
                                            <td style =" text-align:center ">
                                                <asp:DropDownList ID="ddlVendor" runat="server" Width="350px"></asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <table ID="tblAcc" runat="server" border="0" cellpadding="2" cellspacing="0" 
                                        rules="all" style="width:100%;border-collapse:collapse;" visible="false">
                                        <tr>
                                            <td >
                                                <asp:DropDownList ID="ddlAccountCodeFrom" runat="server" Width="250px">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="style1">
                                                -</td>
                                            <td >
                                                <asp:DropDownList ID="ddlAccountCodeTo" runat="server" Width="250px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>                                        
                                    </table>
                                      <div>
                                    <asp:Label ID="lblmsg" runat="server" CssClass="error" style="float :left "></asp:Label>
                                    <asp:Button ID="imgReport" runat="server" Text="Show Report" OnClick="imgReport_OnClick" CssClass="btn" />
                                    </div>
                                </td>
                            </tr>
                        </table>        
                       </ContentTemplate>
                       <Triggers >
                       <asp:PostBackTrigger ControlID="imgReport" /> 
                       </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
            <td style =" text-align:right">
                  
                                    </td>
            </tr>
            <tr>
                <td>
                    <iframe id="Ifram" runat="server" width="100%;" height="500px"></iframe>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

