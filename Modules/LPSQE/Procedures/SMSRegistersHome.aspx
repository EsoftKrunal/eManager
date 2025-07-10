<%@ Page Title="EMANAGER" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SMSRegistersHome.aspx.cs" Inherits="Modules_LPSQE_Procedures_SMSRegistersHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" Runat="Server">
       <div style=" padding:3px; text-align:center;" class="text headerband">
<%--<div style="padding:3px;text-align:center;background-image:url(images/BGC.png) ">--%>
<b style=" font-family:Arial; font-size:17px;"> SMS PROCEDURES - REGISTERS</b>    
</div>
    <div style="padding:5px; background-color:#e2e2e2">
<asp:LinkButton runat="server" ID="btnManuals" OnClick="btnManuals_Click" Text="Procedures Master"  style="color: #206020;font-family:Arial;font-size:14px;font-weight:bold;" CausesValidation="false" /> &nbsp;&nbsp;
<asp:LinkButton runat="server" ID="btnManCat" OnClick="btnManualCat_Click" Text="Procedures Category" style="color: #206020;font-family:Arial;font-size:14px;font-weight:bold;" CausesValidation="false"/> &nbsp;&nbsp;
		<asp:LinkButton runat="server" ID="btnFormDepartment"  Text="Forms Department" style="color: #206020;font-family:Arial;font-size:14px;font-weight:bold;" CausesValidation="false" OnClick="btnFormDepartment_Click"/>
&nbsp;&nbsp;
		<asp:LinkButton runat="server" ID="btnFormCategory"  Text="Forms Category" style="color: #206020;font-family:Arial;font-size:14px;font-weight:bold;" CausesValidation="false" OnClick="btnFormCategory_Click" /> &nbsp;&nbsp;
</div> 

     <div style="text-align :left;border:none;padding:0px;margin:0px;padding-top:5px;">
<iframe runat="server" src="~/Modules/LPSQE/Procedures/SMS_ManualMaster.aspx" id="frm" frameborder="0" width="100%" height="600px" scrolling="no"></iframe>
</div>
</asp:Content>


