<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DD_OFC_PlanSettings.aspx.cs" Inherits="DD_OFC_PlanSettings" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="~/Modules/PMS/UserControls/MessageBox.ascx" tagname="MessageBox" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <link href="../CSS/style.css" rel="stylesheet" type="text/css" />
 <link href="../CSS/tabs.css" rel="stylesheet" type="text/css" />
 <link href="../CSS/StyleSheet.css" rel="Stylesheet" type="text/css" />
 <title>eMANAGER</title>
  <script src="../JS/JQuery.js" type="text/javascript"></script>
<script src="../JS/Common.js" type="text/javascript"></script>
<script src="../JS/JQScript.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

        <div class="box_withpad" style="min-height:450px">
            <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="text-align:center;">Pre Docking Inspection</td>
                <td style="text-align:center;">Docking Specs Ready</td>
                <td style="text-align:center;">Quotation</td>
                <td style="text-align:center;">Yard Confirmation</td>
                <td style="text-align:center;">Execution</td>
            </tr>
            <tr>
                <td style="text-align:center;"><asp:TextBox runat="server" ID="txtDaysBack"></asp:TextBox> </td>
                <td style="text-align:center;">Docking Specs Ready</td>
                <td style="text-align:center;">Quotation</td>
                <td style="text-align:center;">Yard Confirmation</td>
                <td style="text-align:center;">Execution</td>
            </tr>
            </table>
    </div>       
    </div>
    

       
    </form>
</body>
</html>
