<%@ Page Title="SMS PROCEDURES" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ProceduresHome.aspx.cs" Inherits="Modules_LPSQE_Procedures_ProceduresHome" %>

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
    <div style=" padding:3px; text-align:center; display:none;" class="text headerband">
<%--<div style="padding:3px;text-align:center;background-image:url(images/BGC.png) ">--%>
<b style=" font-family:Arial; font-size:17px;"> SMS PROCEDURES </b>    
</div>
<div style="background-color:#ffffff; text-align:center;padding-bottom:0px; text-align:left; padding:5px; padding-bottom:0px;display:none;">
    <asp:Button runat="server" ID="btnSMS" OnClick="btnSMS_Click" Text=" SMS " CssClass="selbtn"  style="padding:5px 5px 5px 5px;" />
    <asp:Button runat="server" ID="btnSMSAdmin" OnClick="btnSMSAdmin_Click"  Text="SMS ADMIN " CssClass="btn1"  style="padding:5px 5px 5px 5px;"/>
    <asp:Button runat="server" ID="btn_Publication" OnClick="btn_Publication_Click"  Text="PUBLICATION" CssClass="btn1"  style="padding:5px 5px 5px 5px;" Visible="false"/>
    <asp:Button runat="server" ID="btn_SMSReview" OnClick="btn_SMSReview_Click"  Text="SMS REVIEW" CssClass="btn1"  style="padding:5px 5px 5px 5px;"/>
   <%-- <asp:ImageButton runat="server" ID="btnBack" PostBackUrl="~/Transactions/InspectionSearch.aspx?Sesstion=Clear" ImageUrl="~/Images/home.png" Visible="false" style="float:right;" />--%>

</div> 
    <div style="text-align :left;border:none;padding:0px;margin:0px;padding-top:2px;">
<iframe runat="server" src="~/Modules/LPSQE/Procedures/ReadManuals.aspx" id="frm" frameborder="0" width="100%" height="625px" scrolling="no"></iframe>
</div>
</asp:Content>


