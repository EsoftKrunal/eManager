<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OfficeComponentReport.aspx.cs" Inherits="Reports_OfficeComponentReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <style>
        .FixedExpensesToolbar {
            position: fixed;
            margin: 70px 0px 0px 0px;
            z-index: 101;
            background-color: #d3d7e4;
        }
    </style>
       <link href="../../../css/app_style.css" rel="Stylesheet" type="text/css" />
     <link href="../../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
</head>
<body style="margin: 0px;">
    <form id="form1" runat="server">
        <div style="padding: 5px; font-family: Verdana; font-size: 12px; width: 900px; position: fixed; top: 0px; z-index: 50; background-color: #c2c2c2; height: 60px;">
            <fieldset>
                <legend>Filters :</legend>
                <table width="100%" border="0">
                    <tr>
                        <%--<td style="text-align :right; font-weight:bold">Report Type :</td>--%>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlReportType" runat="server">
                                <asp:ListItem Text="Component Report" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Component Details Report" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: right; font-weight: bold">Class Equipment :</td>
                        <td style="text-align: left">
                            <asp:CheckBox runat="server" ID="chkClass" /><asp:TextBox runat="server" ID="txtCCode" Width="200px"></asp:TextBox></td>
                        <td style="text-align: right; font-weight: bold">Critical Components :</td>
                        <td style="text-align: left; width: 100px;">
                            <asp:CheckBox runat="server" ID="chkCritical" /></td>
                        <td>
                            <asp:Button runat="server" ID="btnShow" Text=" Show " CssClass="btn" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <br />
        <br />
        <br />
        <div>
            <CR:CrystalReportViewer ToolbarStyle-CssClass="FixedExpensesToolbar" ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
        </div>
    </form>
</body>
</html>
