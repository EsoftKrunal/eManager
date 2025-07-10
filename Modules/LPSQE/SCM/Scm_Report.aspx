<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Scm_Report.aspx.cs" Inherits="Scm_Report" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
     <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
        <style>
        .FixedHeaderToolbar
        {
            position: absolute;
	        margin: 0px 0px 0px 0px;
	        z-index: 10;
	        background-color: #d3d7e4;
        }
        </style>
</head>
<body style='margin:0px;'>

    <div>
        <form id="form1" runat="server">
        <div style="padding:5px; background-color:#e2e2e2; font-family:Verdana; font-size:12px;">
        <table>
        <tr>
        <td>Select Month : </td>
        <td> <asp:DropDownList runat="server" ID="ddlMonth" AutoPostBack="true" OnSelectedIndexChanged="Month_Year_Changed"></asp:DropDownList> </td>
        <td>Select Year : </td>
        <td> <asp:DropDownList runat="server" ID="ddlYear"  AutoPostBack="true" OnSelectedIndexChanged="Month_Year_Changed"></asp:DropDownList> </td>
        </tr>
        </table>
        </div>
        <div style="position:relative">
            <CR:CrystalReportViewer ToolbarStyle-CssClass="FixedHeaderToolbar" ID="CrystalReportViewer1" runat="server" AutoDataBind="true"  />
        </div>
        </form>
    </div>
</body>
</html>
