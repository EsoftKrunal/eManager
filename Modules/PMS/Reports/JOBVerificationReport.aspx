<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JOBVerificationReport.aspx.cs" Inherits="Reports_JOBVerificationReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
    <link href="../../HRD/Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/CalenderStyle.css" rel="Stylesheet" type="text/css" />
    <script src="../JS/Calender.js" type="text/javascript"></script>
    <script src="../JS/Common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="height: 20px; text-align: center; padding-top: 4px;" class="text headerband" visible="false">
                Job Verification Report
            </div>
            <table border="0" cellpadding="2" cellspacing="0" style="border: #4371a5 1px solid; text-align: center" width="100%">
                <tr>
                    <td  style="text-align: left; padding-left: 100px; width:200px;">
                        <span id="tdVessel" runat="server">
                            <b>Select Vessel :</b>&nbsp;<asp:DropDownList ID="ddlVessels" runat="server"></asp:DropDownList>
                        </span>
                    </td>
                    <td width="200px">
                         <asp:Button ID="btnViewReport" CssClass="btn" Text="Show Report" runat="server" Width="100px" OnClick="btnViewReport_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                         <div>
               
                <asp:Label ID="lblMessage" ForeColor="Red" Font-Bold="true" Font-Size="12px" runat="server"></asp:Label>
            </div>
                    </td>
                </tr>
            </table>
           
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
        </div>
    </form>
</body>
</html>
