<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Feedback.aspx.cs" Inherits="FeedBack" %>
<%@ Register Src="~/UserControls/MessageBox.ascx" TagName="Message" TagPrefix="mtm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>MTM Ship Management : Administration Page</title>
<link href="StyleSheet.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" language ="javascript" src="JScript.js"></script>   
<script type ="text/javascript" >
function MoveTo(location)
{
    self.location=location;  
}
function OpenPopup(FeedId)
{
    window.open('SendMail.aspx?FeedId=' + FeedId,'',"width=900px,height=520px");
}
</script>
</head>
<body onload="MM_preloadImages('images/bar_bg.png','images/footer_bg.png','images/header_bg.png','images/home.png','images/hor_line.png','images/login_box.png','images/logo.png','images/logout.png','images/map.png')">
<form runat="server" id="frmMain">

<div style="width :100%;text-align :center; min-height :300px;">
<center>
<br /> 
<img src="images/admintext.png" />
<br />
<br />
<div style="text-align :left ; width :1017px" >
<div style="width :1000px; text-align :left " >
<table cellpadding="0" cellspacing ="0" width="100%" border="1" style=" border-collapse: collapse " >
<tr class="header" >
<td style=" width :50px">&nbsp;Module</td>
<td style=" width :550px">&nbsp;Message</td>
<td style=" width :200px">&nbsp;Posted By</td>
<td style=" width :100px">&nbsp;Posted On</td>
<td style=" width :80px">&nbsp;Send Mail</td>
</tr>
</table>
</div>
<div style="height :340px; overflow-y:scroll; overflow-x:hidden;   width :1017px;text-align : left " >
    <table cellpadding="0" cellspacing ="0" width="1000px" border="1" style=" border-collapse: collapse " >
        <asp:Repeater runat="server" id="rpt_feed">
            <ItemTemplate >
            <tr style=" background-color :Gray; background-image :url(Images/rowbg.jpg); background-repeat:repeat-x; background-color :White;" >
            <td style=" width :50px">&nbsp;<%# Eval("APPLICATIONNAME")%></td>
            <td style=" width :550px">&nbsp;<%# Eval("Message")%></td>
            <td style=" width :200px">&nbsp;<%# Eval("UserId")%></td>
            <td style=" width :100px">&nbsp;<%# Eval("PostedOn")%></td>
            <td style=" width :80px; text-align :center"><img src="images/send_mail_icon.gif" alt="Send Mail" onclick='OpenPopup(<%# Eval("FeedBackId")%>);' style='cursor:pointer;display:<%# (Eval("Approved").ToString()=="Y")?"none":"" %>'/></td>
            </tr>
            </ItemTemplate>
        </asp:Repeater> 
        </table>
</div>
</div>
<br /><br />
<center >
</center>
</center>
</div> 
</form>  
</body>
</html>
