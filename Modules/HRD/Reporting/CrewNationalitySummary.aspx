<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CrewNationalitySummary.aspx.cs" Inherits="Reporting_CrewNationalitySummaryVessel" Title="Crew On Vessel Report" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
      <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css" rel="stylesheet" type="text/css" />
 <style type="text/css" >
    .fixedbar
    {
        position:fixed;
        margin:0px 0px 0px 0px;   
        background-color:#f0f0f0;  
        z-index:100;
        border:solid 1px #5c5c5c;
    }
    </style>
</head>
<body>
<form id="form1" runat="server">
<div style="text-align: left">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" ToolbarStyle-CssClass="fixedbar" AutoDataBind="True" />
    
        
</div>
</form>
</body>
</html>

