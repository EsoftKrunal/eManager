<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaintenanceKPI.aspx.cs" Inherits="Reports_MaintenanceKPI" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/CalenderStyle.css" rel="Stylesheet" type="text/css" />
    <script src="../JS/Calender.js" type="text/javascript"></script>
    <script src="../JS/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="height: 20px; text-align: center; padding-top: 4px;" class="orangeheader" visible="false">
                <b>Maintenance KPI</b>
            </div>
            <table border="0" cellpadding="2" cellspacing="0" style="border: #4371a5 1px solid; text-align: center" width="100%">
                <tr>
                    <td colspan="6" style="text-align: left; padding-left: 100px;">
                        <span id="tdVessel" runat="server">
                            <b>Select Vessel :</b>&nbsp;<asp:DropDownList ID="ddlVessels" runat="server"></asp:DropDownList>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="text-align: left; padding-left: 30px;">
                        <table cellpadding="2" cellspacing="0" width="100%">
                            <tr>
                                <td style="text-align: right"><b>Year :</b>&nbsp;</td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlYear" Width="90px" runat="server"></asp:DropDownList>
                                </td>
                                <td style="text-align: right">
                                    <b>Month(From) :</b>&nbsp;
                                </td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlFromMt" Width="90px" runat="server"></asp:DropDownList>
                                </td>
                                <td style="text-align: right">
                                    <b>Month(To) :</b>&nbsp;
                                </td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlToMt" Width="90px" runat="server"></asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="btnViewReport" CssClass="btnorange" Text="Show Report" runat="server" Width="100px" OnClick="btnViewReport_Click" />
                                </td>
                                <td>
                                    <asp:Label ID="lblMessage" ForeColor="Red" Font-Bold="true" Font-Size="12px" runat="server"></asp:Label>
                                </td>

                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="text-align: left">
                        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
