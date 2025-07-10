<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NearMiss_Analysis.aspx.cs" Inherits="ER_S133_Analysis" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>eMANAGER</title>
    
     <script type="text/javascript" src="../JS/jquery.min.js"></script>
     <script type="text/javascript" src="../JS/KPIScript.js"></script>
      <link rel="stylesheet" type="text/css" href="../CSS/jquery.datetimepicker.css"/>
     <link rel="stylesheet" type="text/css" href="../../../HRD/Styles/StyleSheet.css"/>

    <script type="text/javascript">
        function getBaseURL() {
            var url = window.location.href.split('/');
            var baseUrl = url[0] + '//' + url[2] + '/' + url[3];
            return baseUrl;
        }
      </script>
      <style type="text/css">
          *{
            font-family:Calibri;
            font-size:13px;
           }
      </style>
    <style>
        .FixedToolbar
        {
            position: fixed;
            margin: 0px 0px 0px 0px;
            z-index: 10;
            background-color: #d3d7e4;
        }
    </style>
</head>
<body style="font-size:13px; font-family:Calibri;" >
    <form id="form1" runat="server">
    <div style="font-family:Arial;font-size:12px;">
    <ajaxToolkit:ToolkitScriptManager ID="ScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>

    <div style="  font-size:18px; text-align:center; padding:5px;" class="text headerband">Near Miss Report</div>
    <div style=" background-color:#E6F0FA;color:#666666; font-size:18px; text-align:center; padding:5px;">
    <asp:RadioButton runat="server" ID="radAnalyisis" Checked="true" GroupName="g1" Text="Analysis Report" AutoPostBack="true" OnCheckedChanged="radType_CheckChanged" />
    <asp:RadioButton runat="server" ID="radKPI" GroupName="g1" Text="KPI Summary Report"  AutoPostBack="true"  OnCheckedChanged="radType_CheckChanged" />
    <asp:RadioButton runat="server" ID="radRootCause" GroupName="g1" Text="Root Cause Analysis Summary Report"  AutoPostBack="true"  OnCheckedChanged="radType_CheckChanged" />
    </div>
     <asp:Panel runat="server" ID="pnlAnalysis">
     <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr style="font-weight:bold">
                        <td style="text-align:center;width:300px;">Vessel </td>
                        <td style="text-align:center;width:300px;">Period </td>
                        <td style="text-align:center">&nbsp;</td>
                        <td style="text-align:center">NM Category </td>
                        <td style="text-align:center">NM Type </td>
                    </tr>
                    <tr>
                    <td style="text-align:center"><asp:DropDownList runat="server" ID="ddlVessel" Width="95%"></asp:DropDownList> </td>
                    <td style="text-align:center">
                    <asp:TextBox runat="server" id="txtFromDate"  MaxLength="15" CssClass="input_box" Width="80px" ></asp:TextBox>
                    <asp:ImageButton id="ImageButton3" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif"></asp:ImageButton> &nbsp;&nbsp;
                    -
                    <asp:TextBox runat="server" id="txtToDate"  MaxLength="15" CssClass="input_box" Width="80px"  ></asp:TextBox>
                    <asp:ImageButton id="ImageButton4" runat="server" CausesValidation="False" ImageUrl="~/Modules/HRD/Images/Calendar.gif"></asp:ImageButton>
                    <ajaxToolkit:CalendarExtender id="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton3" PopupPosition="TopRight" TargetControlID="txtFromDate"></ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:CalendarExtender id="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ImageButton4" PopupPosition="TopRight" TargetControlID="txtToDate"></ajaxToolkit:CalendarExtender>
                    </td>
                    <td style="text-align:center">&nbsp;</td>
                    <td style="text-align:center">
                    <asp:DropDownList ID="ddlAcccategory" runat="server" Width="200px" >
                        <asp:ListItem Value="" Text="  All "></asp:ListItem>
                        <asp:ListItem Value="153" Text="Injury"></asp:ListItem>
                        <asp:ListItem Value="154" Text="Pollution"></asp:ListItem>
                        <asp:ListItem Value="155" Text="Property Damage"></asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    <td style="text-align:center">
                    <asp:DropDownList ID="ddlNMType" runat="server" Width="200px" >
                        <asp:ListItem Value="" Text="  All "></asp:ListItem>
                        <asp:ListItem Value="M" Text="Non Significant"></asp:ListItem>
                        <asp:ListItem Value="S" Text="Significant"></asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    </tr>
                    </table>
    <br />  
    <div style=" background-color:Orange;color:Black; text-align:center; padding:0px; height:3px;"></div>
    <br />  
    <center>
        <asp:Button runat="server" ID="btnDownloadExcel" Text="Download Excel File" CssClass="btn" onclick="btnDownloadExcel_Click" />
        <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label> 
    </center>
    <br />
    <div style=" background-color:Orange;color:Black; text-align:center; padding:0px; height:3px;"></div>
    </asp:Panel>
     <asp:Panel runat="server" ID="pnlSummary" Visible="false">
     <div style="padding:10px">
        <asp:DropDownList ID="dddlYear" runat="server" Width="200px" ></asp:DropDownList>
        <asp:Button runat="server" ID="btnSummary" Text="Show Report" CssClass="btn" onclick="btnShowSummary_Click" />
     </div>
           <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"  ToolbarStyle-CssClass="FixedExpensesToolbar" />
     </asp:Panel>
     <asp:Panel runat="server" ID="pnlRootCause" Visible="false">
     <div style="padding:10px">
     Severity : <asp:DropDownList ID="ddlSeverity1" runat="server" Width="200px" >
                        <asp:ListItem Value="" Text=" < All >"></asp:ListItem>
                        <asp:ListItem Value="M" Text="Non Significant"></asp:ListItem>
                        <asp:ListItem Value="S" Text="Significant"></asp:ListItem>
                    </asp:DropDownList>
        
        <asp:Button runat="server" ID="btnShowReport2" Text="Show Report" CssClass="btn" onclick="btnShowReport2_Click" />
     </div>
           <CR:CrystalReportViewer ID="CrystalReportViewer2" runat="server" AutoDataBind="true"  ToolbarStyle-CssClass="FixedExpensesToolbar" />
     </asp:Panel>
        </div>
    </form>

</body>
</html>
