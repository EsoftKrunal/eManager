<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ATAAnalysis.aspx.cs" Inherits="Reports_AuditTrendAnalysisReport" Title="Audit Trend Analysis Report" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Accident Report</title>
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .FixedToolbar
        {
            position: fixed;
            margin: 0px 0px 0px 0px;
            z-index: 10;
            background-color: #d3d7e4;
        }
    </style>
</head>
<body>
<form id="form1" runat="server">
<div>
<ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<table width='100%'>
<tr>
<td style="vertical-align:top">
<div style="height:380px">
<asp:Button ID="load" runat="server" Text="Load Chart Data" OnClick="load_click" OnClientClick="this.value='Processing...'"/>
<asp:Button ID="download" runat="server" Text="Download Chart Data" OnClick="download_click" OnClientClick="this.value='Processing...'"/>
<asp:Chart ID="AnalysisChart" runat="server" Width='900px' Height="340px" >
            <Series>
                <%--<asp:Series Name="TAData" ChartArea="ChartArea1" IsValueShownAsLabel="true" XValueType="String" ></asp:Series>--%>
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
</div>
</td>
</tr>
</table>
</div>
</form>
</body>
</html>
