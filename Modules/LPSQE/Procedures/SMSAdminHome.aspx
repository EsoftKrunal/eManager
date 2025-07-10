<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SMSAdminHome.aspx.cs" Inherits="Modules_LPSQE_Procedures_SMSAdminHome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EMANAGER</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style=" padding:7px;text-align:center; height:21px;border:none; text-align:left;  padding-bottom:0px;" class="selmenu_1" >

            l<asp:LinkButton runat="server" ID="btnSMSMGMT" OnClick="btnSMSMGMT_OnClick"   Text=" Procedures " style="color: #206020;font-family:Arial;font-size:14px;font-weight:bold;"  /> &nbsp;&nbsp;
<asp:LinkButton runat="server" ID="btnAPPROVAL"  OnClick="btnAPPROVAL_OnClick"  Text=" Approval  " style="color: #206020;font-family:Arial;font-size:14px;font-weight:bold;" /> &nbsp;&nbsp;
<asp:LinkButton runat="server" ID="btnComm"  OnClick="btnComm_OnClick" BorderWidth="0" Text=" Communication " style="color: #206020;font-family:Arial;font-size:14px;font-weight:bold;" /> &nbsp;&nbsp;
<asp:LinkButton runat="server" ID="btnRegister"  OnClick="btnRegister_OnClick" BorderWidth="0" Text=" Register " style="color: #206020;font-family:Arial;font-size:14px;font-weight:bold;"  Visible="false"/>&nbsp;&nbsp;
<asp:LinkButton runat="server" ID="btnQuetionair"  OnClick="btnQuetionair_OnClick" BorderWidth="0" Text=" Assessment Question " style="color: #206020;font-family:Arial;font-size:14px;font-weight:bold;" /> &nbsp;&nbsp;

</div> 
<div style="  height:5px;">&nbsp;</div>
        <div style="  height:5px;">&nbsp;</div>
         <div style="text-align :left;border:none;padding:0px;margin:0px;padding-top:2px;">
<iframe runat="server" src="~/Modules/LPSQE/Procedures/ViewManualHeadings.aspx" id="frm" frameborder="0" width="100%" height="600px" scrolling="no"></iframe>
</div>
    </form>
</body>
</html>
