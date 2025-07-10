<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PB_PublicationCommunicationHome.aspx.cs" Inherits="Modules_LPSQE_Procedures_PB_PublicationCommunicationHome_aspx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
</head>
<body>
    <form id="form1" runat="server">
         <div style=" padding:7px;text-align:center; height:25px;border:none; text-align:left;  padding-bottom:0px;" class="selmenu_1" >
<asp:LinkButton runat="server" ID="btnCommuniction" OnClick="btnCommuniction_OnClick" style="color: #206020;font-family:Arial;font-size:14px;font-weight:bold;"  Text=" Communication "  /> &nbsp;
<asp:LinkButton runat="server" ID="btnPublication" OnClick="btnPublication_OnClick" BorderWidth="0"  Text=" Publication " style="color: #206020;font-family:Arial;font-size:14px;font-weight:bold;" /> &nbsp;
<asp:LinkButton runat="server" ID="btnLocation"  OnClick="btnLocation_OnClick" BorderWidth="0" Text=" Location & Custody " style="color: #206020;font-family:Arial;font-size:14px;font-weight:bold;"/> &nbsp;
<asp:LinkButton runat="server" ID="btnRegister"  OnClick="btnRegister_OnClick" BorderWidth="0" Text=" Register " style="color: #206020;font-family:Arial;font-size:14px;font-weight:bold;"/> &nbsp;
</div> 
<div style="  height:5px;">&nbsp;</div>
         <div style="text-align :left;border:none;padding:0px;margin:0px;padding-top:2px;">
<iframe runat="server" src="~/Modules/LPSQE/Procedures/PB_PublicationCommunication.aspx" id="frm" frameborder="0" width="100%" height="600px" scrolling="no"></iframe>
</div>
    </form>
</body>
</html>
