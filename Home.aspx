<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>
<%@ Register Src="~/UserControls/Header.ascx" TagName="header" TagPrefix="mtmh" %>
<%@ Register Src="~/UserControls/Footer.ascx" TagName="footer" TagPrefix="mtmf" %>
<%@ Register Src="~/UserControls/MessageBox.ascx" TagName="Message" TagPrefix="mtmMsg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <meta http-equiv="x-ua-compatible" content="IE=9" />
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>eMANAGER</title>
        <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript" language ="javascript" src="JScript.js"></script>   
        <script type ="text/javascript" language ="javascript" >
            function showwin(qstr)
            {
                var a=window.open('http://192.168.1.10/po/accounts/default.asp?' + qstr,'','width=' + screen.availWidth + ',height=' + screen.availHeight);
            }
            function frm_onload(frmname) 
            {
                try
                {
                frmname.frameElement.height = frmname.document.body.scrollHeight+5;
                }
                catch(ob)
                {
                frmname.frameElement.height=400; 
                }
            }
            function OpenWindow(uri) {
                winref = window.open(uri);
                return false;
            }
        </script>
        <meta name="Keywords" content="ship management, vessel management, Oil Tankers, Oil Tankers Specialist,  tanker management, reefer management, shipboard maintenance, ship manager, crew administration,vessel inspections, project superintendence, ship operation services" />
        <meta name="" content="Our Inspiration is drawn from the ant, which goes about its business quietly, carrying twice its body weight and planning for the season ahead. We imbibe this quality into the very nature of our company as it requires foresight to plan and execute a job in the field of ship management." />
    </head>
    <body onload="MM_preloadImages('images/bar_bg.png','images/footer_bg.png','images/header_bg.png','images/home.png','images/hor_line.png','images/login_box.png','images/logo.png','images/logout.png','images/map.png')" style=" margin:0 0 0 0" >
        <form runat="server" id="frmMain">
            <div id="container">
                <div id="main">
                    <mtmh:header ID="header1" runat ="server" />
                    <div style="width:100%; text-align :left; border:clear; float: left; margin-top : 0px; min-height:400px;">
                        <center>    
                            <iframe style="width:100%; height:700px; background-color:#f3f6fd;" scrolling="no" runat="server" id="frmApp" name="frmApp" frameborder="0">
                            </iframe>
                        </center>
                    </div>
                </div>
            </div>
            <mtmf:footer ID="footer" runat ="server" />
        </form>
    </body>
</html>