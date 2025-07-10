<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VettingPlannerReport.aspx.cs" Inherits="Reports_VettingPlannerReport" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vetting Planner Report</title>
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
        <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
            <tr>
                <td style="width: 825px; text-align: center;">
                    &nbsp;<asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 825px;text-align: left">
                    
                     <table cellpadding="0" cellspacing="0" width="100%">
                        <tr><td colspan="2" style="HEIGHT: 59px">
                           <CR:CrystalReportViewer ToolbarStyle-CssClass="FixedToolbar" ID="CrystalReportViewer1" runat="server" AutoDataBind="true"  />
                        </td></tr>
                    </table>
                   <div id="divPrint">
                        &nbsp;</div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
