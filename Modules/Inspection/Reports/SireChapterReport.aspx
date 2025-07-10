<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SireChapterReport.aspx.cs" Inherits="Reports_SireChapterReport" Title="Audit Trend Analysis Report" %>
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
<ajaxToolkit:TabContainer ID="TabContainer3" runat="server">
<ajaxToolkit:TabPanel ID="TabPanel4" runat="server" HeaderText="Chart">
<ContentTemplate>
<asp:Chart ID="Chart1" runat="server" Width='900px' Height="340px" >
        <Titles>
            <asp:Title Name="Title1" Text="Safety/PSC Inspection Analysis by Sire Chapter"></asp:Title>
        </Titles>
            <Series></Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1"  >
                <AxisX IsLabelAutoFit="false" Interval="1"  >
                    <MajorGrid Interval="1" IntervalOffset="1" LineDashStyle="NotSet" />
                    <LabelStyle Angle="-90" />
                </AxisX>
                </asp:ChartArea>
            </ChartAreas>
 </asp:Chart>
</ContentTemplate>
</ajaxToolkit:TabPanel>
<ajaxToolkit:TabPanel ID="TabPanel5" runat="server" HeaderText="Data">
<ContentTemplate>
<div style="height:45px; width:100%; overflow-x:hidden; overflow-y:scroll; background-color:#c2c2c2">
        <table cellpadding="1" cellspacing="0" border="1" width="100%" style=" border-collapse:collapse" class="rptClass">
            <tr>
             <asp:Literal runat="server" ID="Literal1"></asp:Literal>
            </tr>
            <tr>
             <asp:Literal runat="server" ID="Literal2"></asp:Literal>
            </tr>
        </table>
        </div>
<div style="height:300px; width:100%; overflow-x:hidden; overflow-y:scroll">
        <table cellpadding="1" cellspacing="0" border="1" width="100%" style=" border-collapse:collapse" class="rptClass">
            <asp:Literal runat="server" ID="LitDataRows1"></asp:Literal>
        </table>
        </div>
</ContentTemplate>
</ajaxToolkit:TabPanel>
</ajaxToolkit:TabContainer>
</td>
</tr>
</table>
    </div>
</form>
</body>
</html>
