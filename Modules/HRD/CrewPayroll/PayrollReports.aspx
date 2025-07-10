<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeFile="PayrollReports.aspx.cs" Inherits="Modules_HRD_CrewPayroll_PayrollReports" %>

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
            <tr>
                 <td  class="text headerband" >
           <%-- <img runat="server" id="img1" moduleid="9" style ="cursor:pointer;float :right; padding-right :5px;" src="../images/help.png" alt="Help ?"/> --%>
            Reports</td>
            </tr>
        <tr>
        <td style=" text-align :left; vertical-align : top;border-left:solid 1px white;" >
        <div style="">
            <asp:Button runat="server" ID="btnSummaryReport" Text="Summary Report " CssClass="btn1" OnClick="btnSummaryReport_Click"    />&nbsp;
            <asp:Button runat="server" ID="btnHomeAllotment" Text="Home Allotment/Cash Advance" CssClass="btn1" OnClick="btnHomeAllotment_Click"   />&nbsp;
            
        </div>
        <div style="width:100%; height:3px; background-color:#facc8c"></div>
            <div>

            <iframe runat="server" id="frm" width="100%" height="800px" scrolling="no" frameborder="1"></iframe>    
            </div>
        </td>
        </tr>
      </table>
      </div>

         </form>
</body>
</html>

