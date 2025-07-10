<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CostCategoryReport.aspx.cs" Inherits="DryDock_CostCategoryReport" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .FixedExpensesToolbar
        {
            position: fixed;
	        margin: 0px 0px 0px 0px;
	        z-index: 100;
	        background-color: #d3d7e4;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"  /> 
        <%--DisplayGroupTree="False" --%>
    </div>
    </form>
</body>
</html>
