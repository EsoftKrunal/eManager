<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PMSStatusReport.aspx.cs" Inherits="Reports_PMSStatusReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PMS Status Report</title>
        <link href="../../../css/app_style.css" rel="Stylesheet" type="text/css" />
    <link href="../../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <link href="../../../css/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <link href="../CSS/CalenderStyle.css" rel="Stylesheet" type="text/css" />
    <script src="../JS/Calender.js" type="text/javascript"></script>
    <script src="../JS/Common.js" type="text/javascript"></script>
    <style>
        .FixedExpensesToolbar {
            position: fixed;
            margin: 49px 0px 0px 0px;
            z-index: 100;
            background-color: #d3d7e4;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="dvHeader"  class="text headerband" runat="server">
                PMS Status Report
            </div>
            <div style="position: fixed; top: 20px; left: 0px; width: 100%; padding: 5px; background-color: #e2e2e2; z-index: 100;">
                 <table border="0" cellpadding="2" cellspacing="0" style="border: #4371a5 1px solid; text-align: center" width="100%">
                <tr>
                    <td  class="style1" style="width:200px;text-align:right;">
                            <b>Select Fleet :</b>&nbsp; 
                    </td>
                    <td class="style2" style="width:300px;text-align:left;">
                        <asp:DropDownList runat="server" ID="ddlFleet"></asp:DropDownList>
                    </td>
                    <td style="text-align:left;">
                         <asp:Button runat="server" ID="btnShow" Text="Show Report" OnClick="ShowReport_Click" CssClass="btn" Width='100px' />
                    </td>
                </tr>
                     </table> 
            </div>

            <div>
            </div>
            <div style="margin-top: 30px">
                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolbarStyle-CssClass="FixedExpensesToolbar" />
            </div>
        </div>
    </form>
</body>
</html>
