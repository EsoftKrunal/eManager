<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RestHourEntry.aspx.cs" Inherits="RestHourEntry" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="CP_StyleSheet.css" rel="stylesheet" type="text/css" />
     <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7"> 
    <link href="Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="Styles/sddm.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="Styles/StyleSheet.css" />
    <style type="text/css">
.btn1
{
	background-color :#c2c2c2;
	border:solid 1px gray;
	border :none;
	
}
.selbtn
{
	background-color :#4371a5;
	color :White;
	border :none;
}
</style>
        </head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

<script type="text/javascript">
var StartMode='';
function FillMe(ctl)
{
    if(StartMode!="")
    {
        ctl.innerText=StartMode;
        ctl.className=StartMode;
        Calc();
    }
}
function ClickMe(ctl)
{
    var res=(ctl.innerText=="R")?"W":"R";
    ctl.innerText=res;
    ctl.className=res;
    Calc();
}
function StartMe(ctl)
{
   StartMode=(ctl.innerText=="R")?"W":"R";
}
function EndMe()
{
   StartMode="";
}
function Calc()
{
    var Rest=0;
    for(i=0;i<=document.getElementById("trData").childNodes.length-1;i++)
    {
        var TD=document.getElementById("trData").childNodes[i];
        if(TD.tagName=="TD")
            if(TD.innerText=="R")
            {
                Rest=Rest+0.5;
            }
    }
    var Work=24.0-Rest;
    document.getElementById("dvRest").innerText=Rest;
    document.getElementById("dvWork").innerText=Work;   
}
function SetDay(ctl)
{
    document.getElementById("txtCalDate").value=ctl.innerText;
    document .getElementById("btnCalPost").click();  
}
function SetValues()
{
    var st="";
    for(i=0;i<=document.getElementById("trData").childNodes.length-1;i++)
    {
        if(document.getElementById("trData").childNodes[i].tagName=="TD")
            st+=document.getElementById("trData").childNodes[i].innerText;
    }
    document.getElementById("txtValues").value=st;
}
function GoBack()
{
     window.history.back()
}
document.onselectstart=new Function ("return false");
</script>
<style type ="text/css">
    td
    {
    	word-wrap: break-word;
    }
    .opq
    {
    	z-index:1;
    	background-color :Gray; 
    	opacity:0.4;
    	filter:alpha(opacity=4);
    }
    
    .calhd td
    {
    	font-size :14px;
    	background-color :orange;
    	color:black;
    }
    .caltd 
    {
    	cursor:pointer;
    	color:black; 
    	height :15px;
    }
    .caltdT
    {
    	cursor:pointer;
    	background-color : yellow; 
    }
    .caltdTNC
    {
    	cursor:pointer;
    	background-color :Pink; 
    }
    .caltdA
    {
    	cursor:pointer;
    	background-color :Green; 
    	color:White;
    }
    .caltdNC
    {
    	cursor:pointer;
    	background-color:Pink; 
    }
    .seltd
    {
    	cursor:pointer;
    	border:solid 2px black;
    	color:Black;
    }
    .seltdA
    {
    	cursor:pointer;
    	border:solid 2px black;
    	background-color:Green;
    	color:White;
    }
    .seltdNC
    {
    	cursor:pointer;
    	border:solid 2px black;
    	background-color :Pink; 
    }    
    .seltdT
    {
    	cursor:pointer;
    	border:solid 2px black;
    	background-color :Yellow; 
    }
    .seltdTNC
    {
    	cursor:pointer;
    	border:solid 2px black;
    	background-color :Pink; 
    }
    
    .caltd:hover,.seltd:hover,.caltdA:hover,.caltdF:hover,.caltdAF:hover,.caltdNC:hover,.caltdFNC:hover
    {
    	font-size :10px;
    	border:solid 2px black;
    }
    .hd td
    {
    	font-size :10px;
    	color:black;
    	text-align:left;
    }
    .rd td
    {
    	font-size :10px;
    }
   
    
    .R
    {
    	background-color :Green; 
    	text-align :center;
    	color :White ;
    	border :solid 1px black;
    	display:inline-block;
    	width :16px;
    	font-size :10px;
    }
    .W
    {
    	background-color :aqua; 
    	text-align :center;
    	border :solid 1px black;
    	display:inline-block;
    	width :16px;
   	    font-size :10px;
    }
</style>
<div onmouseup="EndMe()">
<table style="width :100%" cellpadding="0" cellspacing="0">
<tr>
<td >
<center>
<asp:UpdatePanel runat="server" ID="up1">
<ContentTemplate>
<table width="100%" cellpadding="0" cellspacing="0" border ="1" style="border-collapse:collapse;">
<tr>
<td>
    <asp:TextBox runat="server" ID="txtValues" Width="61%" style="display :none" ></asp:TextBox> 
    <asp:TextBox runat="server" ID="txtCalDate" style="display :none"></asp:TextBox> 
    <asp:Button runat="server" ID="btnCalPost" Text="Calender"  style="display :none" onclick="btnCalPost_Click"  /> 
        <table cellpadding="0" cellspacing="0"  width="100%" border="1" style=" border-collapse :collapse;float:left;">
        <tr>
            <td colspan="5">
                <asp:Button runat="server" ID="Button2" Text="Back" Width="70px" OnClientClick="GoBack()"   style="float:right;"  CssClass="btn" /> 
            </td>
        </tr>
    <tr style="text-align :center; background-color :LightGray; height:20px;"  >
        <td>Crew #</td>
        <td>Name</td>
        <td>Rank</td>
        <td>Sign on Date</td>
        <td>Current Status</td>
    </tr>
    <tr style="height:25px; font-size :14px; font-weight:bold; color:Black   ">
        <td>
            <asp:Label ID="lblCrwNumber" runat="server" ></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblName" runat="server" ></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblRank" runat="server" ></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblSignOnDate" runat="server" ></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblStatus" runat="server" ></asp:Label>
        </td>
    </tr>
</table>
    <table cellpadding="5" cellspacing="0" border ="0" style=" border-collapse:collapse;width :100%">
        <tr>
        <td style="width :300px; text-align :center; padding :4px;">
            <center >
            <table cellpadding="5" cellspacing="0" width="950px" border="1" style=" border-collapse :collapse; width:280px;">
             <tr class="calhd">  
                <td colspan="4"><asp:DropDownList runat="server" ID="ddlYear" AutoPostBack="true" OnSelectedIndexChanged="YearMonth_Changed"> </asp:DropDownList></td>
                <td colspan="3"><asp:DropDownList runat="server" ID="ddlMonth" AutoPostBack="true" OnSelectedIndexChanged="YearMonth_Changed"> 
                        <asp:ListItem Text="Jan" Value="1" ></asp:ListItem>  
                        <asp:ListItem Text="Feb" Value="2" ></asp:ListItem>  
                        <asp:ListItem Text="Mar" Value="3" ></asp:ListItem>  
                        <asp:ListItem Text="Apr" Value="4" ></asp:ListItem>  
                        <asp:ListItem Text="May" Value="5" ></asp:ListItem>  
                        <asp:ListItem Text="Jun" Value="6" ></asp:ListItem>  
                        <asp:ListItem Text="Jul" Value="7" ></asp:ListItem>  
                        <asp:ListItem Text="Aug" Value="8" ></asp:ListItem>  
                        <asp:ListItem Text="Sep" Value="9" ></asp:ListItem>  
                        <asp:ListItem Text="Oct" Value="10" ></asp:ListItem>  
                        <asp:ListItem Text="Nov" Value="11" ></asp:ListItem>  
                        <asp:ListItem Text="Dec" Value="12" ></asp:ListItem>
                </asp:DropDownList></td>
            </tr>
            <tr class="calhd">  
                <td>Su</td>
                <td>Mo</td>
                <td>Tu</td>
                <td>We</td>
                <td>Th</td>
                <td>Fr</td>
                <td>Sa</td>
            </tr>
            <asp:Literal runat="server" ID="litCalender"></asp:Literal>
            </table>
            </center>
            <asp:Label runat="server" ID="lblSelDate"></asp:Label> / <asp:Label runat="server" ID="Label1"></asp:Label>
        </td>
        <td>
            
            <div runat="server" id="dvReason">
            Non-Conformities :
            <asp:TextBox runat="server" ID="txtNC" TextMode="MultiLine" Height ="97px" Width="95%" ReadOnly="true"></asp:TextBox> 
             Remarks ( Non-Conformities ) :
            <asp:DropDownList runat="server" ID="ddlReason" Width="95%"></asp:DropDownList> 
            </div>
            
        </td>
        </tr>
        <tr>
            <td colspan="2">
           <table cellpadding="0" cellspacing="0"  width="950px" border="1" style=" border-collapse :collapse;">
            <tr style="text-align :center; background-color :LightGray; height :25px;" class="hd">
                <td colspan="2">0</td>
                <td colspan="2">1</td>
                <td colspan="2">2</td>
                <td colspan="2">3</td>
                <td colspan="2">4</td>
                <td colspan="2">5</td>
                <td colspan="2">6</td>
                <td colspan="2">7</td>
                <td colspan="2">8</td>
                <td colspan="2">9</td>
                <td colspan="2">10</td>
                <td colspan="2">11</td>
                <td colspan="2">12</td>
                <td colspan="2">13</td>
                <td colspan="2">14</td>
                <td colspan="2">15</td>
                <td colspan="2">16</td>
                <td colspan="2">17</td>
                <td colspan="2">18</td>
                <td colspan="2">19</td>
                <td colspan="2">20</td>
                <td colspan="2">21</td>
                <td colspan="2">22</td>
                <td colspan="2">23</td>
                <%--<td colspan="2">00:00</td>
                <td colspan="2">01:00</td>
                <td colspan="2">02:00</td>
                <td colspan="2">03:00</td>
                <td colspan="2">04:00</td>
                <td colspan="2">05:00</td>
                <td colspan="2">06:00</td>
                <td colspan="2">07:00</td>
                <td colspan="2">08:00</td>
                <td colspan="2">09:00</td>
                <td colspan="2">10:00</td>
                <td colspan="2">11:00</td>
                <td colspan="2">12:00</td>
                <td colspan="2">13:00</td>
                <td colspan="2">14:00</td>
                <td colspan="2">15:00</td>
                <td colspan="2">16:00</td>
                <td colspan="2">17:00</td>
                <td colspan="2">18:00</td>
                <td colspan="2">19:00</td>
                <td colspan="2">20:00</td>
                <td colspan="2">21:00</td>
                <td colspan="2">22:00</td>
                <td colspan="2">23:00</td>--%>
                
            </tr>
            <tr style="text-align :center; background-color :LightGray; height :25px;" class="rd" id="trData">
            <asp:Literal runat="server" ID="litHoursList"></asp:Literal>
            </tr>
            </table>
            </td>
        </tr>
         <tr>
        <td colspan="2" style="text-align :center;">
        <div style="width:950px; text-align:center; padding:0px; ">
            <div style="float:left;height:25px;padding:5px;">
            <span class="caltdNC" style="padding :3px; font-size :11px; border:solid 1px black; width :20px;" id="Div6" runat="server">&nbsp;&nbsp;</span> - NC&nbsp;&nbsp;&nbsp;
            <span class="caltdA" style="padding :3px; font-size :11px;border:solid 1px black;width :20px;" id="Div3" runat="server">&nbsp;&nbsp;</span> - Entry Done
            </div>
            <div style="float:right">
            Rest Hours : <span class="R" style="width:50px; padding :3px; font-size :11px;display :inline;" id="dvRest" runat="server">24.0</span> 
            Work Hours : <span class="W" style="width:50px; padding :3px; font-size :11px;display :inline;" id="dvWork" runat="server">0.0</span>   
            </div>
        </div>
        </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align :right">
            <asp:Button runat="server" ID="btnPost" Visible="false" CssClass="btn" Text=" Save " onclick="btnPost_Click" OnClientClick="SetValues()"/>
            <asp:Button runat="server" ID="btnPrint" CssClass="btn" Text=" Print NC " onclick="btnPrintComm_Click"/>
            <asp:Button runat="server" ID="Button1" CssClass="btn" Text=" Print Work & Rest Record " onclick="btnPrintSheet_Click"/>
            </td>
        </tr>
    </table>
</td>
</tr>
</table>
<asp:Literal runat="server" ID="grdList"></asp:Literal>
</ContentTemplate>
</asp:UpdatePanel>
</center>
</td>
</tr>
</table>
</div>
            </div>
        </form>
    </body>
    </html>