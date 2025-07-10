<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SMSAdminSubTab.ascx.cs" Inherits="UserControls_SMSAdminSubTab" %>
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
<div style=" padding:7px;text-align:center; height:21px;border:none; text-align:left;  padding-bottom:0px;" class="" >

<asp:Button runat="server" ID="btnSMSMGMT" OnClick="btnSMSMGMT_OnClick" BorderWidth="0"  Text="Procedures" CssClass="selbtn" style="padding:4px 10px 2px 10px; float:left;" />
<asp:Button runat="server" ID="btnRegister"  OnClick="btnRegister_OnClick" BorderWidth="0" Text=" Register " CssClass="btn1"  style="padding:4px 10px 2px 10px; float:left; margin-left:5px;" Visible="false"/>
<asp:Button runat="server" ID="btnForms"  OnClick="btnForms_Click" BorderWidth="0" Text=" Forms " CssClass="btn1"  style="padding:4px 10px 2px 10px; float:left; margin-left:5px;" />
<asp:Button runat="server" ID="btnQuetionair"  OnClick="btnQuetionair_OnClick" BorderWidth="0" Text=" Assessment Question " CssClass="btn1"  style="padding:4px 10px 2px 10px; float:left; margin-left:5px;"/>

</div> 
<div style="  height:5px;">&nbsp;</div>