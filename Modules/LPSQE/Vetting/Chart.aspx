<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Chart.aspx.cs" Inherits="Vetting_Chart" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body style="margin:0px">
    <form id="form1" runat="server">
    <div>
    <center>
    <asp:Panel runat="server" ID="pnl_Chart1" Visible="false">
        <asp:Image runat="server" ID="imgChart" width="1000px"/>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnl_Chart2" Visible="false">
        <asp:Chart ID="Chart2" runat="server" Width='1000px' Height="350px">
            <Series>
                <asp:Series Name="Series1" ChartType="Line" ChartArea="ChartArea1" IsValueShownAsLabel="true" XValueType="String"  Color="#001A66" BorderWidth="2" >
                </asp:Series>
            </Series>
            <Legends>
                <asp:Legend Name="Legend1"></asp:Legend>
            </Legends>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1" >
                    <AxisX IsLabelAutoFit="false" Interval="1"  >
                    <MajorGrid Interval="1" IntervalOffset="1" LineColor="White"  />
                    <LabelStyle Angle="0" />
                </AxisX>
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnl_Chart3" Visible="false">
    
        <asp:Image runat="server" ID="imgChart3" width="700px"/>
    </asp:Panel>
    </center>
    </div>
    
    
    </form>
</body>
</html>
