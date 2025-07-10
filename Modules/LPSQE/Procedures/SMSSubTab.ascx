<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SMSSubTab.ascx.cs" Inherits="UserControls_SMSSubTab" %>
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
<div style=" padding:3px;  text-align:center;" class="text headerband">
<%--<div style="padding:3px;text-align:center;background-image:url(images/BGC.png) ">--%>
<b style=" font-family:Arial; font-size:17px;"> FORMS & PROCEDURES</b>    
</div>
<div style=" padding:7px;text-align:center; height:21px;border:none; text-align:left;  padding-bottom:0px;"  >
<%--<div style="float:right; white-space:400px;">
    <a target="_blank" href="SMS_Download.aspx?File=Manuals_Tanker.pdf">Download Tanker Manual</a>&nbsp;&nbsp;&nbsp;&nbsp;
    <a target="_blank" href="SMS_Download.aspx?File=Manuals_Bulk_Carrier.pdf">Download Bulk Carrier Manual</a>&nbsp;&nbsp;&nbsp;&nbsp;
</div>--%>
<asp:Button runat="server" ID="btnManual"  OnClick="btnManual_OnClick" Text=" Procedures " BorderWidth="0" CssClass="selbtn"  style="padding:4px 10px 2px 10px;margin-left:5px;"/>
<asp:Button runat="server" ID="btnForms" OnClick="btnForms_OnClick" Text=" Forms " BorderWidth="0" CssClass="btn1"  style="padding:4px 10px 2px 10px;margin-left:5px;"/>
<asp:Button runat="server" ID="btnReports" OnClick="btnReports_OnClick" Text=" Change Record Summary " BorderWidth="0" CssClass="btn1"  style="padding:4px 10px 2px 10px;margin-left:5px;"/>
</div> 
<div style="  height:5px;">&nbsp;</div>
