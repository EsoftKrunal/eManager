<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayrollInformation.aspx.cs" Inherits="Modules_HRD_CrewPayroll_PayrollDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" /> 
    <%--<link href="../styles/style.css" rel="stylesheet" type="text/css" />--%>
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
   <%-- <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />--%>
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
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
	border :none;
	padding:5px 10px 5px 10px;
}
         </style>
</head>
<body>
    <form id="form1" runat="server">
         <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div style="text-align: center;font-family:Arial;">
       
        <table style="width :100%" cellpadding="0" cellspacing="0">
            <%--<tr>
                 <td  class="text headerband" >
         
            Payroll </td>
            </tr>--%>
        <tr>
        <td style=" text-align :left; vertical-align : top;border-left:solid 1px white;" >
        <div style="">
            <asp:Button runat="server" ID="btnPortageBillSummary" Text="Portage Bill Summary" CssClass="btn1" OnClick="Menu_Click"  CommandArgument="0"   />&nbsp;
            <asp:Button runat="server" ID="btnPortageBillDetails" Text="Portage Bill Details" CssClass="btn1" OnClick="Menu_Click" CommandArgument="1"    />&nbsp;
            <asp:Button runat="server" ID="btnHomeAllotment" Text="Home Allotment" CssClass="btn1" OnClick="Menu_Click" CommandArgument="2"  />&nbsp;
            <asp:Button runat="server" ID="btnCashAdvance" Text="Cash Advance" CssClass="btn1" OnClick="Menu_Click" CommandArgument="3"   />&nbsp;
            <asp:Button runat="server" ID="btnBondedStores" Text="Bonded Stores" CssClass="btn1" OnClick="Menu_Click" CommandArgument="4"  />&nbsp;
             <asp:Button runat="server" ID="btnCrewNetPayable" Text="Crew Net Payable Report" CssClass="btn1" OnClick="Menu_Click" CommandArgument="6"  />&nbsp;
            <asp:Button runat="server" ID="btnPortageBillReport" Text="Portage Bill Report" CssClass="btn1" OnClick="Menu_Click" CommandArgument="5" Visible="false" />&nbsp;
           
        </div>
        <div style="width:100%; height:3px; background-color:#facc8c"></div>
            <div>

            <iframe runat="server" id="frm" src="~/Modules/HRD/CrewPayroll/SummaryReportMonthWise.aspx" width="100%" height="1000px" scrolling="yes" frameborder="1"></iframe>    
            </div>
        </td>
        </tr>
      </table>
      </div>

         </form>
</body>
</html>

