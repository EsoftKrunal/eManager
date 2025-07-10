<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RAPrint.aspx.cs" Inherits="Reports_RCAPrint" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
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
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>    
            <table border="0" cellpadding="0" cellspacing="0" style="background-color: #ffffff" width="100%">
                <tr>
                    <td >
                        <CR:CrystalReportViewer ToolbarStyle-CssClass="FixedToolbar" ID="CrystalReportViewer1" runat="server" AutoDataBind="true"  />
                    </td>
                </tr>
            </table>
    </div>
    </form>
</body>
</html>
