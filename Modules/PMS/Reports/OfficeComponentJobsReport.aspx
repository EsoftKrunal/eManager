<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OfficeComponentJobsReport.aspx.cs" Inherits="Reports_OfficeComponentJobsReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <style>
        .FixedExpensesToolbar {
            position: fixed;
            margin: 0px 0px 0px 0px;
            z-index: 100;
            background-color: #d3d7e4;
        }

        .FixedExpensesToolbar1 {
            position: fixed;
            margin: 40px 0px 0px 0px;
            z-index: 100;
            background-color: #d3d7e4;
        }
    </style>
</head>
<body style="margin: 0px 0px 0px 0px">
    <form id="form1" runat="server">
        <div style="background-color: #e2e2e2; width: 100%; padding: 6px; font-family: Arial; font-size: 12px; border: solid 1px grey; position: fixed; z-index: 100; top: 0px; left: 0px;" runat="server" id="dvFilter" visible="false">
            Enter Component Code :
            <asp:TextBox runat="server" ID="txtCompCode" Style="width: 150px" MaxLengt="15"></asp:TextBox>
            <asp:Button runat="server" ID="btnShow" Text="Show Report" OnClick="btnShow_Click" />
        </div>
        <div runat="server" id="dvmargin" visible="false" style="height: 40px">&nbsp;</div>
        <div>
            <CR:CrystalReportViewer ToolbarStyle-CssClass="FixedExpensesToolbar" ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
        </div>
    </form>
</body>
</html>
