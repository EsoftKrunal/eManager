<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Alerts.aspx.cs" Inherits="PageAlerts" %>
<%@ Register Src="~/UserControls/MessageBox.ascx" TagName="Message" TagPrefix="mtm" %>
<%@ Register TagName="menu" Src="~/UserControls/ModuleMenu.ascx" TagPrefix="mtm"  %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>MTM Ship Management : Administration Page</title>
<link href="StyleSheet.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" language ="javascript" src="JScript.js"></script>  
 
<style type="text/css" >
.Q
{
    font-family :Verdana;
    color:#2e4f83;
    list-style-type:square;
    display :list-item;   
    width :210px;
    font-size :11px;
    vertical-align :top ; 
}
.A
{
    font-family :Verdana;
    display :list-item;  
    list-style-type:disc;
    color:#506ead;	
    margin-left :0px; 
    width :190px;
    font-style:italic;
    font-size :10px;
    vertical-align :top;
    margin-left :10px;
}
.F
{
  color:green;	
}
.R
{
    color:red;	
}
.O
{
    color:Maroon;	
}
.module
{
	font-family :Verdana;
    color:#Maroon;	
    width :210px;
}
.ctype
{
	font-family :Verdana;
    color:#Maroon;	
    width :210px;
}
.user
{
	font-family :Verdana;
    color:#Maroon;	
    width :210px;
}
.time
{
	font-family :Verdana;
	font-size :8px;
	color :Black; 
}
li,ul
{
	margin: 0 0 0 0;
	padding :0 0 0 0;
}
</style>  
</head>
<body onload="MM_preloadImages('images/bar_bg.png','images/footer_bg.png','images/header_bg.png','images/home.png','images/hor_line.png','images/login_box.png','images/logo.png','images/logout.png','images/map.png')" style =" background-image : url(images/page_bg.jpg); background-position :center; background-repeat:repeat-y">
<form runat="server" id="frmMain">
<div style="width :100%;text-align :center; min-height :300px;">
<center>
<%--<div style="width:400px;height:100px">
<div style =" height :45px; background-image : url(images/Alert/top_bg.png);">
    <div style =" float :left; width : 216px; height :45px; background-color:#f3f6fd"><img src="images/Alert/top_logo.png" alt =""/></div>
    <div style =" float :right; width : 20px; height :45px; background-color:#f3f6fd"><img src="images/Alert/right_top.png" alt =""/></div>
</div>
<div style =" height :100%; background-color:#f3f6fd;">
    <div style =" float :left; height :100%;width : 20px;background-image:url(images/Alert/left_bg.png); background-repeat : repeat-y"></div>
    <div style=" float :left; color : Red" >
    <span>
    <ul style="text-align :left " >
        <li> <asp:LinkButton ID="lbl_CRM" runat="server" Text="Crm Alert&nbsp;" Visible="False"></asp:LinkButton> </li>
        <li> <asp:LinkButton ID="lbl_Other" runat="server" Text="Document Alert&nbsp;" Visible="False"></asp:LinkButton></li>
        <li> <asp:LinkButton ID="lbl_SignOffCrew" runat="server" Text="SignOff Crew Alert&nbsp;" Visible="False"></asp:LinkButton></li>
        <li> <asp:LinkButton ID="lbl_VesselManning" runat="server" Text="Vessel Manning Alert&nbsp;" Visible="False"></asp:LinkButton></li>
        <li> <asp:LinkButton ID="lbl_SignOffAlert" runat="server" Text="EOC Appraisal Alert&nbsp;" Visible="False"></asp:LinkButton></li>
    </ul>
    </span>
    </div>
    <div style =" float :right;height :100%;width : 20px;background-image:url(images/Alert/right_bg.png); background-repeat : repeat-y"></div>
</div>
<div style =" height :20px; background-image : url(images/Alert/bottom_bg.png);">
    <div style =" float :left; width : 20px; height :19px; background-color:#f3f6fd"><img src="images/Alert/left_bottom.png" alt =""/></div>
    <div style =" float :right; width : 20px; height :19px; background-color:#f3f6fd"><img src="images/Alert/right_bottom.png" alt =""/></div>
</div>
</div>  --%>
     <table style="width :100%" cellpadding="0" cellspacing="0" border="0" >
        <tr>
        <td style="text-align :center; vertical-align : top; width:100% ">
        <center >
            <mtm:menu runat="server" ID="menu2" Visible="false"/>  
            <img src="images/watermark.jpg" alt="" border="0" usemap="#Map" />
        </center>
        </td>
        <%--<td style=" text-align :center; vertical-align : top;" >
        <center> 
        <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center" width="100%">
            <tr>
                <td align="center" style="background-color:#4371a5; height: 23px" class="text" ></td>
            </tr>
        </table>
        <div style="width : 857px;" >
        <div style=" background-image : url(images/visionmission.png); float : left ; height :484px; width :262px" ></div>
        <div style=" background-image : url(images/userfeed.png); height :484px; width :242px;padding-left :19px; float :right" ><br />
        <center>
        <asp:Panel runat="server" id="pnlNewPost" style="padding-top :30px; padding-right :15px; padding-bottom:5px; "  >
            <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" BorderStyle="None" ID="rad_FType" >
            <asp:ListItem Text="&nbsp;Feedback" Value="F" Selected="True"></asp:ListItem>   
            <asp:ListItem Text="&nbsp;Change Request" Value="R"></asp:ListItem></asp:RadioButtonList><br />
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
        <asp:Label runat="server" ForeColor="Red" Text ="" ID="lblMess" Font-Size="9px"  ></asp:Label>  
        <center>
        </div>
        <div style=" background-image : url(images/sysalerts.png); height :433px; width :254px; padding-top :51px; padding-left:8px; text-align :left; vertical-align:top;" >
                <p id="cms_Alert" runat="server">
                <span style =" padding-left:25px; font-family :Arial;position :relative; color :Red ">CMS Alerts !</span>
                <ul style="text-align :left; position :relative; padding-left :25px;" >
                    <li runat="server" id="li_CRM" visible="false" > <asp:LinkButton ID="lbl_CRM" runat="server" Text="Crm Alert&nbsp;" Visible="False" PostBackUrl="~/AlertCRM.aspx" ></asp:LinkButton> </li>
                    <li runat="server" id="li_Other" visible="false" > <asp:LinkButton ID="lbl_Other" runat="server" Text="Document Alert&nbsp;" Visible="False" PostBackUrl="~/AlertOther.aspx"></asp:LinkButton></li>
                    <li runat="server" id="li_SignOffCrew" visible="false" > <asp:LinkButton ID="lbl_SignOffCrew" runat="server" Text="SignOff Crew Alert&nbsp;" Visible="False" PostBackUrl="~/AlertSignOffCrew.aspx" ></asp:LinkButton></li>
                    <li runat="server" id="li_VesselManning" visible="false" > <asp:LinkButton ID="lbl_VesselManning" runat="server" Text="Vessel Manning Alert&nbsp;" Visible="False" PostBackUrl="~/AlertVesselManning.aspx"></asp:LinkButton></li>
                    <li runat="server" id="li_SignOffAlert" visible="false" > <asp:LinkButton ID="lbl_SignOffAlert" runat="server" Text="EOC Appraisal Alert&nbsp;" Visible="False" PostBackUrl="~/AlertSignOffAppraisal.aspx"></asp:LinkButton></li>
                </ul> 
            </p>
        </div>
        </div>
        </center>
        </td>--%>
        </tr>
      </table>
        <br /><br />
</center>
</div> 
</form>
<div style=" background-image : url(images/page_footer.jpg); height : 65px ; width:100%; background-position :center" ></div>
<map name="Map" id="Map">
<area shape="circle" coords="169, 194, 34" href="#" 
        onclick="return parent.DoPost(5);" title="Administrator"/>
<area shape="circle" coords="315, 193, 34" href="#" 
        onclick="return parent.DoPost(6);" title="Budget" />
<area shape="circle" coords="465, 193, 33" href="#" 
        onclick="return parent.DoPost(1);" title="Crew Management System" />
<area shape="circle" coords="613, 193, 32" href="#" 
        onclick="return parent.DoPost(7);" title="MTM Office" />
<area shape="circle" coords="391, 286, 33" href="#" 
        onclick="return parent.DoPost(4);" title="Purhase Order System" />
<area shape="circle" coords="242, 286, 33" href="#" 
        onclick="return parent.DoPost(3);" title="Planned Maintenance System" />
<area shape="circle" coords="542, 285, 33" href="#" 
        onclick="return parent.DoPost(2);" title="Vessel Inspection Management System"/>
</map></body>
</html>
