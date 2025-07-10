<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintRFQComparison.aspx.cs" Inherits="PrintRFQComparison" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
    .fixed_width
    {
        width:130px;
        overflow:hidden;
        font-family:Arial;
        font-size:9px;
        height:15px;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label runat="server" ID="lblMsg" Font-Bold="true"></asp:Label>
        <%--<CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" DisplayGroupTree="False" />--%>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
    </div>
    </form>
</body>
</html>
