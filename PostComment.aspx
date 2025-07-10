<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PostComment.aspx.cs" Inherits="PostComment" %>
<%@ Register Src="~/UserControls/Header.ascx" TagName="header" TagPrefix="mtm" %>
<%@ Register Src="~/UserControls/Footer.ascx" TagName="footer" TagPrefix="mtm" %>
<%@ Register Src="~/UserControls/MessageBox.ascx" TagName="Message" TagPrefix="mtm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>MTM Ship Management : Home Page</title>
<link href="StyleSheet.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" language ="javascript" src="JScript.js"></script>   
<meta name="Keywords" content="ship management, vessel management, Oil Tankers, Oil Tankers Specialist,  tanker management, reefer management, shipboard maintenance, ship manager, crew administration,vessel inspections, project superintendence, ship operation services" />
<meta name="" content="Our Inspiration is drawn from the ant, which goes about its business quietly, carrying twice its body weight and planning for the season ahead. We imbibe this quality into the very nature of our company as it requires foresight to plan and execute a job in the field of ship management." />
</head>
<body onload="MM_preloadImages('images/bar_bg.png','images/footer_bg.png','images/header_bg.png','images/home.png','images/hor_line.png','images/login_box.png','images/logo.png','images/logout.png','images/map.png')" style=" margin:0 0 0 0" >
<form runat="server" id="frmMain">
<center>
<asp:Label runat="server" ID="lblMsg" ForeColor="Red"></asp:Label>
<span runat="server" id="spnBack">
want to go <a href="Alerts.aspx">Back</a>
</span> 
</center> 
<div style="width:100%;text-align :left;border:clear :left; float : left; margin-top : 0px;">
<center>    
<div style=" background-image : url(images/userfeed.png); height :434px; width :242px;padding-left :19px;" ><br />
<center>
   <asp:Panel runat="server" id="pnlNewPost" style="padding-top :30px; padding-right :15px; padding-bottom:5px; "  >
    <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" BorderStyle="None" ID="rad_FType"  style="display :none " >
    <asp:ListItem Text="&nbsp;Feedback" Value="F" Selected="True"></asp:ListItem>   
    <asp:ListItem Text="&nbsp;Change Request" Value="R"></asp:ListItem></asp:RadioButtonList>
    Module : <asp:DropDownList runat="server" ID="ddl_Modules" style=" font-size:11px; width :160px; " ></asp:DropDownList>
</asp:Panel>
   <asp:Panel runat="server" id="pnlReply" style=" padding-right :17px; padding-top :0px; padding-bottom :5px;">
    <div id="dvText" runat="server"  style="paddding:5px; background-color :lightgray; border :solid 1px gray; height :50px; overflow-y:scroll; width :222px; height:80px; overflow-x:hidden; text-align : left ">
    </div>
</asp:Panel> 
   <div style =" padding-right:16px;">
        <asp:TextBox runat="server"  ID="txtMess" TextMode="MultiLine" Height="300px" Width="220px" style=" border :Solid 1px gray"  ></asp:TextBox>  
        </div>
        <br />
    <asp:Button runat="server" ID="btnPost" Text="Submit" Height="25px" Width="100px" onclick="btnPost_Click" style="display :block"/> 
<center>
</div>
</center> 
</form>  
</body>
</html>
