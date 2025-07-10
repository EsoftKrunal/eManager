<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeFile="ProcedureSubMenu.aspx.cs" Inherits="Modules_LPSQE_Procedures_ProcedureSubMenu" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
    <link rel="Stylesheet" href="../../HRD/Styles/StyleSheet.css" />
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
    <form id="form1" runat="server" >
    <div style=" padding:7px;text-align:center; height:21px;border:none; text-align:left;  padding-bottom:7px;" class="" >
<%--<div style="float:right; white-space:400px;">
    <a target="_blank" href="SMS_Download.aspx?File=Manuals_Tanker.pdf" style="color: #206020;font-family:Arial;font-size:14px;font-weight:bold;">Download Tanker Manual</a>&nbsp;&nbsp;&nbsp;&nbsp;
    <a target="_blank" href="SMS_Download.aspx?File=Manuals_Bulk_Carrier.pdf" style="color: #206020;font-family:Arial;font-size:14px;font-weight:bold;">Download Bulk Carrier Manual</a>&nbsp;&nbsp;&nbsp;&nbsp;
</div>--%>
      
<asp:Button runat="server" ID="btnManual"  OnClick="btnManual_OnClick" Text=" Procedure " BorderWidth="0" CssClass="selbtn"  style="padding:4px 10px 2px 10px;margin-left:5px;"/>
<asp:Button runat="server" ID="btnForms" OnClick="btnForms_OnClick" Text=" Forms " BorderWidth="0" CssClass="btn1"  style="padding:4px 10px 2px 10px;margin-left:5px;"/>
<asp:Button runat="server" ID="btnReports" OnClick="btnReports_OnClick" Text=" Change Record Summary " BorderWidth="0" CssClass="btn1"  style="padding:4px 10px 2px 10px;margin-left:5px;"/>
</div> 
<div style="  height:5px;">&nbsp;</div>
         <div style="text-align :left;border:none;padding:0px;margin:0px;padding-top:2px;">
<iframe runat="server" src="~/Modules/LPSQE/Procedures/ReadManuals.aspx" id="frm" frameborder="0" width="100%" height="600px" scrolling="no"></iframe>
</div>
</form>
    </body>
    </html>

