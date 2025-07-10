<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print.aspx.cs" Inherits="Print" EnableEventValidation="false" EnableViewStateMac="false" ValidateRequest="false" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reports</title>
      <style>
        .FixedExpensesToolbar
        {
         position: fixed;
         margin: 0px 0px 0px 0px;
         z-index: 100;
         background-color: #d3d7e4;
        }
        </style>
<meta http-equiv="X-UA-Compatible" content="IE=9,chrome=1" />
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
        
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"  HasToggleGroupTreeButton="false" ToolPanelView="None"  ToolbarStyle-CssClass="FixedExpensesToolbar" />
    </div>
    </form>
</body>
</html>
