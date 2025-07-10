<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JobHistory.aspx.cs" Inherits="Reports_JobHistory" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Job History</title>
    <link href="../../../css/app_style.css" rel="Stylesheet" type="text/css" />
    <link href="../../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <link href="../../../css/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <link href="../CSS/CalenderStyle.css" rel="Stylesheet" type="text/css" />
    <script src="../JS/Calender.js" type="text/javascript"></script>
    <script src="../JS/Common.js" type="text/javascript"></script>
    <%--<style>
        .FixedExpensesToolbar
        {
            position: fixed;
	        margin: 0px 0px 0px 0px;
	        z-index: 100;
	        background-color: #d3d7e4;
        }
        </style>--%>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="dvHeader" class="text headerband" runat="server">
                Job History
            </div>
            <table border="0" cellpadding="2" cellspacing="0" style="border: #4371a5 1px solid; text-align: center" width="100%">
                <tr>
                    <td style="text-align: right; width:200px; ">
                        <span id="tdVessel" runat="server">
                            <b>Select Vessel :</b>&nbsp;
                        </span>
                    </td>
                    <td style="text-align: left; width:300px; ">
                        <asp:DropDownList ID="ddlVessels" runat="server"></asp:DropDownList>
                    </td>
                    <td colspan="4" style="text-align: left;">
                         <asp:Button ID="btnViewReport" CssClass="btn" Text="Show Report" runat="server" Width="100px" OnClick="btnViewReport_Click" />
                    </td>
                </tr>
                <tr id="trFilter" runat="server">
                    <td colspan="6" style="text-align: left; padding-left: 100px;">
                        <table cellpadding="2" cellspacing="0" width="100%">
                            <tr>
                                <td style="width: 150px; text-align: right"><b>Component Code :</b></td>
                                <td>
                                    <asp:TextBox ID="txtCompCode" MaxLength="12" Width="100px" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkClass" Style="font-weight: bold" Text="Class Component" runat="server" />
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkCritical" Style="font-weight: bold" Text="Critical Component" runat="server" />
                                </td>
                                <td style="text-align: right"><b>Period (From/TO) :</b></td>
                                <td>
                                    <asp:TextBox ID="txtFromDt" onfocus="showCalendar('',this,this,'','holder1',-100,22,1)" MaxLength="11" Width="90px" runat="server"></asp:TextBox>
                                    <asp:TextBox ID="txtToDt" onfocus="showCalendar('',this,this,'','holder1',-100,22,1)" MaxLength="11" Width="90px" runat="server"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <div>
               
                <asp:Label ID="lblMessage" ForeColor="Red" Font-Bold="true" Font-Size="12px" runat="server"></asp:Label>
            </div>
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
        </div>
    </form>
</body>
</html>
