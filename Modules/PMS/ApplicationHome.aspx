<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplicationHome.aspx.cs" Inherits="ApplicationHome" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <link href="CSS/style.css" rel="stylesheet" type="text/css" />
    <link href="CSS/tabs.css" rel="stylesheet" type="text/css" />
    <title>eMANAGER</title>
    <script type ="text/javascript" >
        function LogOut() {
            alert('Your Session is Expired. Please Login Again.');
            var loc = window.parent.parent.location.toString().lastIndexOf("/");
            var lft = window.parent.parent.location.toString().substr(0, loc);
            window.parent.parent.location = lft + '/login.aspx';
        }
</script>
    <style type="text/css">
ul.obtabs {
list-style: none;
margin: 1px 0 -1px 0;
padding: 0;
position: absolute;
}
ul.obtabs li {
float: left;
display: block;
height: 24px;
padding-right: 12px;
margin-left: -5px;
position: relative;
background: url(images/tabright-back.gif) 100% 0 no-repeat;
border-bottom: 1px solid #bbb8a9;
white-space: nowrap;
width:80px;
cursor:pointer;
}
ul.obtabs span {
height: 24px;
line-height: 24px;
padding-left: 7px;
background: url(images/tableft-back.gif) no-repeat;
}
html>body ul.obtabs span {
display: block;
}
ul.obtabs li.current {
z-index: 1;
font-weight: bolder;
border-bottom: 1px solid #fff;
height: 25px;
background-image: url(images/tabright.gif);
}
ul.obtabs li.current span {
background-image: url(images/tableft.gif);
}

ul.obtabs a.new {
color: Red;
}
A
{
    text-decoration:none;
    font-size:13px;
    cursor:pointer;
}

</style>
</head>
<body>
    <form id="form1" runat="server">
    <div style=" text-align :left">
<div style="height :116px; background-image:url(Images/Application/header_bg.jpg);">
<table width="100%" cellspacing="0" cellpadding="0" border="0" runat="server" id="tab_Header" style="border-collapse:collapse" >
    <tr>
        <td style="height :78px; width:235px;">
            <img id="Img1" runat="server" src="~/Modules/PMS/Images/Application/logo.jpg" style="float:left"/>
        </td>
        <td>
            <div style="float:left; margin-top:55px; ">
                <asp:Label ID="lblUser" style=" font-size:14px; font-weight:bold;  color:Blue;" runat="server"></asp:Label>
            </div>
            <div style="float:right">
                <img src="./Images/Application/pms_icon.png" runat="server" id="Logo" visible="false" />
            </div>
        </td>
        <td id="tdPmsIcon" runat="server" style="height :78px; width:83px; background-image :url(Images/Application/pms_icon.jpg); text-align:right; background-repeat:no-repeat; " >
        &nbsp;
        </td>
    </tr>
    </table>
<table width="100%" cellspacing="0" cellpadding="0" border="0" runat="server" id="Table1" style="border-collapse:collapse" >
    <tr>
        <td colspan="2">
            <div style=" height:27px; float:left; padding-left:6px; width:900px; ">
            <ul class="obtabs">
            <li id="Li1" runat="server" onclick='this.childNodes[0].childNodes[0].click();' class="current"><span><a id="btn_PMS" runat="server" title="Planned Maintenance System" >PMS</a></span></li>
            <li id="Li4" runat="server" onclick='this.childNodes[0].childNodes[0].click();' style="width:130px"><span><a id="btn_MF" runat="server" title="Manual & Forms">Manual & Forms</a></span></li>
            <li id="Li7" runat="server" onclick='this.childNodes[0].childNodes[0].click();' style="display:none;"><span><a id="btn_eReports" title="e-Reports" runat="server">e-Forms</a></span></li>
            <li id="Li2" runat="server" onclick='this.childNodes[0].childNodes[0].click();'><span><a id="btn_VIMS" runat="server" title="Vessel Inspection Management System">VIMS</a></span></li>
            <li id="Li3" runat="server" onclick='this.childNodes[0].childNodes[0].click();'><span><a id="btn_NWC" runat="server" title="NWC">NWC</a></span></li>
            <li id="Li6" runat="server" onclick='this.childNodes[0].childNodes[0].click();'><span><a id="btn_Purchase" title="Purchase" runat="server">Purchase</a></span></li>
            <li id="Li8" runat="server" onclick='this.childNodes[0].childNodes[0].click();' style="width:150px;display:none;"><span><a id="btn_Drills" title="Drills And Trainings" runat="server">Drills And Trainings</a></span></li>
            <li id="Li9" runat="server" onclick='this.childNodes[0].childNodes[0].click();' style="width:120px"><span><a id="btn_Communication" title="Communication" runat="server">Communication</a></span></li>
           <li id="Li10" runat="server" onclick='this.childNodes[0].childNodes[0].click();' style="width:50px"><span><a id="btn_CMS" title="CMS" runat="server">CMS</a></span></li>
           <li id="Li11" runat="server" onclick='this.childNodes[0].childNodes[0].click();' style="width:50px"><span><a id="btn_StoreManagement" title="MRV" runat="server">MRV</a></span></li>

            <li id="Li12" runat="server" onclick='this.childNodes[0].childNodes[0].click();' style="width:65px"><span><a id="btn_HSSQE" title="HSSQE" runat="server">HSSQE</a></span></li>
            </ul>
            </div>
            <script type="text/javascript" >

                var timeout = 400;
                var closetimer = 0;
                var ddmenuitem = 0
                var lastpost = 0;

                function DoPost(arg) {
                    document.getElementById('<%=hfd_CB.ClientID%>').setAttribute("value", arg);
                    document.getElementById('<%=btnPost.ClientID%>').click();
                    lastpost = arg;
                }
                function DoPostLast() {
                    ctl = '<%=CurrentModule%>';
                    if (ctl != "0") {
                        var arg = ctl;
                        document.getElementById('<%=hfd_CB.ClientID%>').setAttribute("value", arg);
                        document.getElementById('<%=btnPost.ClientID%>').click();
                    }
                }
                function mclosetime() {
                    closetimer = window.setTimeout(HideMenu, timeout);
                }

                // cancel close timer
                function mcancelclosetime() {
                    if (closetimer) {
                        window.clearTimeout(closetimer);
                        closetimer = null;
                    }
                }
                function InvertImage(ctl, arg) {
                    ctl.style.backgroundImage = 'url(images/' + arg + '_s.png)';
                }
                function RestoreImage(ctl, arg) {
                    ctl.style.backgroundImage = 'url(images/' + arg + '.png)';
                }
                function ChangePwd() {
                    document.getElementById("frmApp").src = 'changepassword.aspx';
                }
            </script>
            <asp:LinkButton runat="server" ID="btnPost" Text="" OnClick="btn_POST_Click" arg="0" TabIndex="0"  />   
            <asp:HiddenField runat="server" id="hfd_CB" /> 

            <div style="float :left;text-align :left;color : #4172ac; margin-top:5px; padding-left:160px;border:solid 0px red;">
                <asp:ImageButton runat="server" ImageUrl="~/Modules/PMS/Images/logout_black.png" ToolTip="LogOut" ID="LinkButton1" ForeColor="Red" onclick="btn_LogOutApp_Click" Font-Underline="true" />
            </div>
        </td>
        <td style="text-align:right; padding-right:10px;">
            <asp:Label runat="server" ID="lblVessel" ForeColor="#19357E" Font-Size="13" Font-Names="Verdana" Font-Bold="true"></asp:Label>
        </td>
    </tr>
</table>
</div>
</div>
<div style="width:100%;border:#4371a5 0px solid;  overflow:auto; overflow:hidden" >
<iframe src="ShipHome.aspx" height="490px;" width="100%" frameborder="0" scrolling="no" runat="server" id="frmmain"></iframe>
</div>
<div id="footer" runat="server" style=" color : #4172ac; float :left ; clear : both ; width :100%;background-image :url(images/footer_bg.jpg); height:20px;width:100%; text-align :center; padding-top :3px; font-size :11px;">
    Developed by M.T.M. Shipmanagement Pte. Ltd. All rights reserved. 
    <span runat="server" id="aComm"> | <a style=" font-size :11px;" onclick='OpenPopup();' href="#">Feedback</a></span> | 
    <span style="padding-right:25px; font-size:12px; font-weight:bold; font-family:Calibri; font-style:italic;">Version: 2.00</span>  
</div> 
</form>
</body>
</html>
