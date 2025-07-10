<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Chart.aspx.cs" Inherits="CrewRecord_Chart" MasterPageFile="~/MasterPage.master" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%--<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>--%>
   
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
     <link rel="stylesheet" type="text/css" href="../../../css/StyleSheet.css" />
<%--</head>--%>
<%--<body>
<form id="form1" runat="server">--%>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<div style="text-align: center">
<table cellpadding="0" cellspacing="0" width="100%" border="1" style="font-size:12px;font-family:Arial;"> 
<tr>
<td align="center" class="text headerband" colspan="2">
Crew Reports
</td>
</tr>
<tr>
<td style="text-align:left; width:400px;">
    <table cellpadding="5" cellspacing="5" width="100%" >
    <tr><td><asp:LinkButton runat="server" ID="btn1" Text="Total No of Crew OnBoard - Old vs New" onclick="btn1_Click" ForeColor="#206020"></asp:LinkButton></td></tr>
    <tr><td><asp:LinkButton runat="server" ID="btn2" Text="Officers OnBoard by Nationality - Old vs New" onclick="btn2_Click" ForeColor="#206020"></asp:LinkButton></td></tr>
    <tr><td><asp:LinkButton runat="server" ID="btn3" Text="Ratings OnBoard by Nationality - Old vs New" onclick="btn3_Click" ForeColor="#206020"></asp:LinkButton></td></tr>
    <tr><td><asp:LinkButton runat="server" ID="btn4" Text="Officers OnBoard by Nationality" onclick="btn4_Click" ForeColor="#206020"></asp:LinkButton ></td></tr>
    <tr><td><asp:LinkButton runat="server" ID="btn5" Text="Ratings OnBoard by Nationality" onclick="btn5_Click" ForeColor="#206020"></asp:LinkButton></td></tr>
    </table>
</td>
<td style="background-color:White">
  <asp:Chart ID="Chart_HSQE009" runat="server" Width='800px' Height="430px" >
            <Series>
                <asp:Series Name="New" ChartArea="ChartArea1" IsValueShownAsLabel="true" XValueType="String" Color="Orange"></asp:Series>
                <asp:Series Name="Old" ChartArea="ChartArea1" IsValueShownAsLabel="true" XValueType="String" Color="LightBlue" ></asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1"  >
                <AxisX IsLabelAutoFit="false" Interval="1"  >
                    <MajorGrid Interval="1" IntervalOffset="1" LineDashStyle="NotSet" />
                    <LabelStyle Angle="-90" />
                </AxisX>
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
</td>
</tr>
</table>
</div>
<%--</form>
</body>
</html>--%>
    </asp:Content>
