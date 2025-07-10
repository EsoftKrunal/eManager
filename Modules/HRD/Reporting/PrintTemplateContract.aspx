<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintTemplateContract.aspx.cs" Inherits="Reporting_PrintTemplateContract" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
  <%--<link href="../Styles/style.css" rel="stylesheet" type="text/css" />--%>
    <%--<link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />--%>
    <%--<link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />--%>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css" rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:TextBox runat="server" ID="txtcontractid" Width="200px"></asp:TextBox>
        <asp:Button runat="server" ID="btnGo" Text="Print"  OnClick="btnGo_Click"/>        
    </div>
    </form>
</body>
</html>
