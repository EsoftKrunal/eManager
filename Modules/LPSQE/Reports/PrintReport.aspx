<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintReport.aspx.cs" Inherits="PrintReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>


</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
            <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                <col width="200px" />
                <col />
                <tr>
                    <td colspan="2">&nbsp;<asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <%--<CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" DisplayGroupTree="False" Width="100%" />--%>
                        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" Width="100%" />
                    </td>
                    <td></td>
                </tr>
            </table>

            <%--ToolbarStyle-CssClass="FixedHeaderToolbar"--%>
        </div>
    </form>
</body>
</html>
