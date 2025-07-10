<%@ Page Language="C#" AutoEventWireup="true" CodeFile="popupTrainingMatrix.aspx.cs" Inherits="CrewOperation_popupTrainingMatrix" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Training Matrix</title>
    <style type="text/css">
    .hd1
    {
        min-width:150px;
    }
    td
    {
        border:solid 1px #c2c2c2;
    }
    </style>
</head>
<body style="margin:0px"> 
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="text-align:center">
    <b>Training Matrix Compliance</b>
        <div>Vessel : <span id="lblVesselName" runat="server"></span> as on <%=DateTime.Today.Date.ToString("dd-MMM-yyyy") %></div>
    </div>
    <div>

        <div style="padding:5px; float:left; margin:5px; background-color:#66C285">Valid Training</div>
        <div style="padding:5px; float:left; margin:5px; background-color:#FFFF66">Due Training</div>
        <div style="padding:5px; float:left; margin:5px; background-color:#FF5050">Overdue Training</div>
        <div style="padding:5px; float:left; margin:5px; background-color:#FF9966">Never Done</div>   

        <asp:Button runat="server" ID="btnExport" runat="server" Text="Export to Excel" onclick="btnExport_Click" style="float:left;margin:5px"/>

        <div style="clear:both;"></div>
    </div>
    
    <div>
    <asp:Literal runat="server" ID="litTreaining"></asp:Literal>
    </div>
    </form>
</body>
</html>
