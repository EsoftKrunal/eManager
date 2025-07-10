<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MRM_Report.aspx.cs" Inherits="Reports_MRM_Report" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>MRM Report</title>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css" rel="stylesheet" type="text/css" />
    <style>
        .FixedToolbar
        {
            position: fixed;
            margin: 0px 0px 0px 0px;
            z-index: 10;
            background-color: #d3d7e4;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
        <table align="center" border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="center" valign="top" style="height: 235px">
                    <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid; border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align: center" width="100%">
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 20px; text-align: left">
                                <CR:CrystalReportViewer ToolbarStyle-CssClass="FixedToolbar" ID="CrystalReportViewer1" runat="server" AutoDataBind="true"  />
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
