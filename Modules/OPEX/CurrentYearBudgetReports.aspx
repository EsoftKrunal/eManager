<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CurrentYearBudgetReports.aspx.cs" Inherits="CurrentYearBudgetReports" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>EMANAGER</title>
    <link href="../HRD/Styles/StyleSheet.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
.selbtn
{
    background-color :#669900;
	color :White;
	border :none;
    padding:5px 10px 5px 10px;
}
.btn1
{
	background-color :#c2c2c2;
	border:solid 1px gray;
    color :black;
	border :none;
	padding:5px 10px 5px 10px;
    
}
</style>
    <script type="text/javascript">
    var mainctl;
    function DisableCtl() 
    {
            mainctl.value = 'Please Wait..';
            mainctl.disabled = 'disabled';
    }

    function DisableMe(c1) 
    {
        mainctl=c1;
        setTimeout("DisableCtl()", 500);
    }
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <asp:DropDownList runat="server" ID="ddlShip" style='display:none'></asp:DropDownList>
    <table style="width :100%" cellpadding="0" cellspacing="0">
        <tr>
        <td style="vertical-align: top;">
        <table style="border:#4371a5 1px solid; width: 100% ; " border="1" cellpadding="3" cellspacing="0">
        <tr style="font-size:13px ; font-weight:bold; ">
        <td style="width:400px">Report</td>
        <td style="width:50px; text-align:center;"> Year </td>
        <td style="width:150px">&nbsp;</td>
        <td style="width:250px"><asp:Label runat="server" ID="lblFC" Text="Fleet"></asp:Label> </td>
        <td> &nbsp;</td>
        </tr>
        <tr>
        <td>
           <asp:Button runat="server" ID="btnPrntBudFore" Text="Budget Report"  Height="25px" Width="120px" OnClick="btnPrntBudFore_Click" CssClass="btn1"/>
           <asp:Button runat="server" ID="btnPrntSummary" Text="Fleet Summary By Vessel" Height="25px" Width="175px" OnClick="btnPrntSummary_Click"  CssClass="btn1"/>
        </td>
        <td style="text-align:center;padding-top:8px"><asp:Label runat="server" ID="lblyear"></asp:Label></td>
        <td>
            <asp:RadioButton runat="server" ID="radF" GroupName="a" OnCheckedChanged="RadFC_OnSelectedIndexChanged" Text="Fleet" Checked="true" AutoPostBack="true" />
            <asp:RadioButton runat="server" ID="radC" GroupName="a" OnCheckedChanged="RadFC_OnSelectedIndexChanged" Text="Company" AutoPostBack="true"/>
        </td>
        <td style="padding-top:8px">
            <asp:DropDownList ID="ddlFleet" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFleet_OnSelectedIndexChanged"></asp:DropDownList>    
            <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="true"  Visible="false" OnSelectedIndexChanged="ddlCompany_OnSelectedIndexChanged" ></asp:DropDownList>
        </td>
        
        <td>&nbsp;<asp:Button runat="server" ID="btnShow" Text="Show" Height="25px" Width="80px" OnClick="btnShow_Click" CssClass="btn" Visible="false" OnClientClick='DisableMe(this);'/>
        </td>
        </tr>
        </table>
        <div runat="server" id="dv_BudgetForecast" visible="false">
        <table cellpadding="1" cellspacing="0"  border="1" width="100%" style="border-collapse:collapse">
        <tr class= "headerstylegrid">
            <td style="width:50px; text-align:center">Sr#</td>
            <td style="width:100px; text-align:center">Vessel Code </td>
            <td style="width:300px; text-align:center">Vessel Name </td>
            <td>&nbsp;Report</td>
        </tr>
        <asp:Repeater runat="server" ID="rptVessels">
        <ItemTemplate>
        <tr>
            <td style="text-align:center"><%#Eval("SNo") %></td>
            <td style="text-align:center"><%#Eval("VesselCode") %></td>
            <td>&nbsp;<%#Eval("VesselName") %></td>
            <td><asp:ImageButton runat="server" ID="btnPrint" ImageUrl="~/Images/pdf_icon.png" ToolTip='<%#Eval("VesselName") %>' OnClick="btnPrint_Click" CommandArgument='<%#Eval("VesselCode") %>' /> </td>
        </tr>
        </ItemTemplate>
        </asp:Repeater>
        </table>
        </div>
        </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
