<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendMail.aspx.cs" Inherits="PostComment" ValidateRequest="false" EnableEventValidation="false"%>
<%@ Register Src="~/UserControls/Header.ascx" TagName="header" TagPrefix="mtm" %>
<%@ Register Src="~/UserControls/Footer.ascx" TagName="footer" TagPrefix="mtm" %>
<%@ Register Src="~/UserControls/MessageBox.ascx" TagName="Message" TagPrefix="mtm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>MTM Ship Management : Mail Sending</title>
<link href="StyleSheet.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" language ="javascript" src="JScript.js"></script>   
<script type="text/javascript" >
function copyMsg(ob)
{
document.getElementById('txtHMsg').value=document.getElementById('txtMess').innerHTML;
}
</script>  
<meta name="Keywords" content="ship management, vessel management, Oil Tankers, Oil Tankers Specialist,  tanker management, reefer management, shipboard maintenance, ship manager, crew administration,vessel inspections, project superintendence, ship operation services" />
<meta name="" content="Our Inspiration is drawn from the ant, which goes about its business quietly, carrying twice its body weight and planning for the season ahead. We imbibe this quality into the very nature of our company as it requires foresight to plan and execute a job in the field of ship management." />
</head>
<body onload="MM_preloadImages('images/bar_bg.png','images/footer_bg.png','images/header_bg.png','images/home.png','images/hor_line.png','images/login_box.png','images/logo.png','images/logout.png','images/map.png')" style=" margin:0 0 0 0" >
<form runat="server" id="frmMain">
<center>
<asp:Label runat="server" ID="lblMsg" ForeColor="Red"></asp:Label>
</center> 
<div style="width:100%;text-align :left;border:clear :left; float : left; margin-top : 0px;">
<center>    
<br />
<div style=" background-image : url(images/email_row_header.gif); width :835px; height :11px;" >&nbsp;</div>
<div style=" background-image : url(images/email_row_bg.gif); width :835px; height :450px;" >
    <br />
    <center>
    <span style="display:none" ><asp:TextBox runat="server" ID="txtHMsg" /></span>
    <table width="800px;"  border="0" >
        <tr><td style=" text-align :right; width:100px;" >Email From : </td><td style="text-align :left;width:700px;" ><asp:TextBox runat="server" ID="txtMailFrom" Width="700px" MaxLength="50" ></asp:TextBox></td></tr>
        <tr><td style=" text-align :right " >Email To : </td><td style="text-align :left" ><asp:TextBox runat="server" ID="txtMailTo" Width="700px" MaxLength="50" Enabled="false"></asp:TextBox></td></tr>
        <%--<tr><td style=" text-align :right " >Email CC : </td><td style="text-align :left" ><asp:TextBox runat="server" ID="txtMailCC" Width="700px" MaxLength="50"></asp:TextBox></td></tr>--%>
        <tr><td style=" text-align :right " >Subject : </td><td style="text-align :left" ><asp:TextBox runat="server" ID="txtSubject" Width="700px" MaxLength="50"></asp:TextBox></td></tr>
        <tr><td style=" text-align :right; vertical-align :top;" >Message : </td><td style="text-align :left" ><div style="width :700px; height :100px;border :Solid 1px gray; overflow-x:hidden;oveflow-y:scroll;" runat="server" id="txtReqMess"></div></asp:TextBox></td></tr>
        <tr><td style=" text-align :right; vertical-align :top;" >Reply : </td><td style="text-align :left" ><div style=" border :Solid 1px gray; height:200px; width :700px; " contenteditable="true" runat="server"  ID="txtMess" ><%-- <asp:TextBox runat="server"  ID="txtMess" TextMode="MultiLine" Height="200px" Width="700px" style=" border :Solid 1px gray"  ></asp:TextBox>--%></td></tr>
        <tr><td style=" text-align :right " >&nbsp;</td><td style="text-align :right" ><asp:Button style=" background-image : url('Images/tools_email_icon.gif'); background-repeat : no-repeat;background-color:white; text-align:right " runat="server" ID="btnPost" Text="Send" Height="30px" Width="75px" OnClientClick="copyMsg(this);" onclick="btnPost_Click"/></td></tr>
    </table>
    <br />
    
    <center>
</div>
<div style=" background-image : url(images/email_row_footer.gif); width :835px; height :12px;" >&nbsp;</div> 
</center>
</div>  
</form>  
</body>
</html>
