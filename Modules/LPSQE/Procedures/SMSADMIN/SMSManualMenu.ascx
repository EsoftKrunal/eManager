<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SMSManualMenu.ascx.cs" Inherits="UserControls_SMSManualMenu" %>
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
<b style=" font-family:Arial; font-size:17px;"> SMS ADMIN</b>    
</div>
<div style="background-color:#ffffff; text-align:center;padding-bottom:0px; text-align:left; padding:5px; padding-bottom:0px;">
   <%-- <asp:Button runat="server" ID="btnSMS" OnClick="btnSMS_Click" Text=" SMS " CssClass="selbtn" BorderWidth="0" style="padding:8px 10px 2px 10px;" />--%>
    <asp:Button runat="server" ID="btnSMSAdmin" OnClick="btnSMSAdmin_Click"  Text="SMS ADMIN " CssClass="selbtn" BorderWidth="0" style="padding:8px 10px 2px 10px;"/>
    <asp:Button runat="server" ID="btn_Publication" OnClick="btn_Publication_Click"  Text="PUBLICATION" CssClass="btn1" BorderWidth="0" style="padding:8px 10px 2px 10px;" Visible="false"/>
    <asp:Button runat="server" ID="btn_SMSReview" OnClick="btn_SMSReview_Click"  Text="SMS REVIEW" CssClass="btn1" BorderWidth="0" style="padding:8px 10px 2px 10px;"/>
	<asp:Button runat="server" ID="btnAPPROVAL"  OnClick="btnAPPROVAL_OnClick" BorderWidth="0" Text=" Approval  " CssClass="btn1"  style="padding:8px 10px 2px 10px; "/>
	<asp:Button runat="server" ID="btnComm"  OnClick="btnComm_OnClick" BorderWidth="0" Text=" Communication " CssClass="btn1"  style="padding:8px 10px 2px 10px;  "/>
    <%--<asp:ImageButton runat="server" ID="btnBack" PostBackUrl="~/Transactions/InspectionSearch.aspx?Sesstion=Clear" ImageUrl="~/Images/home.png" Visible="false" style="float:right;" />--%>

</div> 
