<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DryDockJobList.aspx.cs" Inherits="DryDockJobList" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PMS : Job Update By Period Report</title>
   <%-- <link href="../CSS/style.css" rel="stylesheet" type="text/css" />--%>
    <link href="../CSS/CalenderStyle.css" rel="Stylesheet" type="text/css" />
    <script src="../JS/Calender.js" type="text/javascript"></script>
    <script src="../JS/Common.js" type="text/javascript"></script>
         <link href="../../../css/app_style.css" rel="Stylesheet" type="text/css" />
    <link href="../../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <link href="../../../css/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <%--<style>
        .FixedExpensesToolbar
        {
            position: fixed;
	        margin: 0px 0px 0px 0px;
	        z-index: 100;
	        background-color: #d3d7e4;
	        top: 50px;
        }
        </style>--%>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="text headerband" visible="false">
                Job Update By Period Report
            </div>
            <table border="0" cellpadding="2" cellspacing="0" style="border: #4371a5 1px solid; text-align: center" width="100%">
                <tr>
                    <td  style="text-align: right;width:150px;padding-left:10px;">
                        <span id="tdVessel" runat="server">
                            <b>Select Vessel :</b>&nbsp;
                        </span>

                    </td>
                    <td style="text-align: left;width:300px;padding-left:10px;">
                        <asp:DropDownList ID="ddlVessels" AutoPostBack="true" OnSelectedIndexChanged="ddlVessels_SelectedIndexChanged" runat="server"></asp:DropDownList>&nbsp;
                    </td>
                    <td colspan="4" style="text-align: left;padding-left:10px;">
                        <asp:Label ID="lblMessage" ForeColor="Red" Font-Bold="true" Font-Size="12px" runat="server"></asp:Label>
                    </td>
                </tr>
                <%-- <tr>
       <td colspan="6" style="text-align: left; padding-left :100px;">
       <table cellpadding="2" cellspacing="0" width="100%">
       <tr>
       <td style="width:82px; text-align :right"><b>Period :</b></td>
       <td >
            <asp:TextBox ID="txtFromDt" onfocus="showCalendar('',this,this,'','holder1',5,22,1)" MaxLength="11" Width="90px" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtToDt" onfocus="showCalendar('',this,this,'','holder1',5,22,1)" MaxLength="11" Width="90px" runat="server"></asp:TextBox>
       </td>
       <td >
       </td>
    </tr>
    </table>
    </td>
    </tr>--%>
            </table>
            <%--<div>
    <asp:Button ID="btnViewReport" CssClass="btnorange" Text="Show Report" runat="server" Width="100px" onclick="btnViewReport_Click" />
    <asp:Label ID="lblMessage" ForeColor="Red" Font-Bold="true" Font-Size="12px" runat="server"></asp:Label>
    </div>--%>
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
        </div>
    </form>
    </body>
</html>
