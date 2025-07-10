<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GPTExportExcel.aspx.cs" Inherits="CrewOperation_GPTExportExcel" %>
<%@ Register Src="../UserControls/GraphicalPlanningTool.ascx" TagName="GraphicalPlanningTool" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body onload="Javascript:LoadContent()">
    <form id="form1" runat="server">
        <uc1:GraphicalPlanningTool ID="GraphicalPlanningTool1"  runat="server" />    
    </form>
</body>
</html>
