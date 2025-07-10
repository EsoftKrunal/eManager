<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PMSCommunicationLog.aspx.cs" Inherits="Reports_PMSCommunicationLog" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PMS Communication Log</title>
         <link href="../../../css/app_style.css" rel="Stylesheet" type="text/css" />
    <link href="../../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <link href="../../../css/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <link href="../CSS/CalenderStyle.css" rel="Stylesheet" type="text/css" />
    <script src="../JS/Calender.js" type="text/javascript"></script>
    <script src="../JS/Common.js" type="text/javascript"></script>

    <style type="text/css">
        .style1 {
            width: 512px;
        }

        .style2 {
            width: 102px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="text headerband" visible="false">
                <b>PMS Communication Log</b>
            </div>
            <table border="0" cellpadding="2" cellspacing="0" style="border: #4371a5 1px solid; text-align: center" width="100%">
                <tr>
                    <td  class="style1" style="width:200px;text-align:right;">
                        <span id="tdVessel" runat="server">
                            <b>Select Vessel :</b>&nbsp;
                        </span>
                    </td>
                    <td class="style2" style="width:300px;text-align:left;">
                        <asp:DropDownList ID="ddlVessels" runat="server"></asp:DropDownList>
                    </td>
                    <td >

                    </td>
                </tr>
                <tr>
                    <td class="style1" style="width:200px;text-align:right;">
                        <span id="Span1" runat="server">
                            <b>Select Log Type :</b>&nbsp;
                        </span>
                    </td>
                    <td class="style2" style="width:300px;text-align:left;">
                        <asp:DropDownList ID="ddlLogType" runat="server">
                                <asp:ListItem Text="< All >" Value='0' Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Packet Created" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Packet Received" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        
                    </td>
                    <td style="text-align: left;">
                        <asp:Button ID="btnViewReport" CssClass="btn" Text="Show Report" runat="server" Width="100px" OnClick="btnViewReport_Click" /> &nbsp;&nbsp;
                        <asp:Label ID="lblMessage" ForeColor="Red" Font-Bold="true" Font-Size="12px" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: left">
                        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
