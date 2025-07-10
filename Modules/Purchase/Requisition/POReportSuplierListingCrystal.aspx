<%@ Page Language="C#" AutoEventWireup="true" CodeFile="POReportSuplierListingCrystal.aspx.cs" Inherits="POReportSuplierListingCrystal" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Variance Report </title>
    <link href="CSS/style.css" rel="Stylesheet" type="text/css" />
     <style>
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
        <table cellpadding="0" cellspacing="0" width="99%" border="1" style="border-collapse:collapse;">
            <tr>    
                <td colspan="6" style="text-align:left;">
                        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                        AutoDataBind="true"  ToolbarStyle-CssClass="FixedExpensesToolbar"/>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
