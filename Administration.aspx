<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Administration.aspx.cs" Inherits="Administration" %>

<%@ Register Src="~/UserControls/MessageBox.ascx" TagName="Message" TagPrefix="mtm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>MTM Ship Management : Administration Page</title>
    <link href="StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="JScript.js"></script>
    <script type="text/javascript">
        function MoveTo(location) {
            self.location = location;
        }
    </script>
</head>
<body onload="MM_preloadImages('images/bar_bg.png','images/footer_bg.png','images/header_bg.png','images/home.png','images/hor_line.png','images/login_box.png','images/logo.png','images/logout.png','images/map.png')">
    <form runat="server" id="frmMain">
        <div style="width: 100%; text-align: center; min-height: 300px;">
            <center>
                <br />
                <img alt="" src="images/admintext.png" />
                <br />
                <br />
                <mtm:Message ID="MessageBox1" runat="server" />
                <br />
                <div style="text-align: center; width: 100%; clear: both; vertical-align: top">
                    <input type="button" id="btn_CMS" style="border: 0px; cursor: pointer; background-image: url(images/users.png); width: 113px; height: 100px;" onclick="javascript:MoveTo('UserManagement.aspx');" title="User Management" />&nbsp;
                    <input type="button" id="btn_VIMS" style="border: 0px; cursor: pointer; background-image: url(images/roles.png); width: 113px; height: 100px;" onclick="javascript:MoveTo('RoleManagement.aspx');" title="Roles Management" />&nbsp;
                    <input type="button" id="btn_POS" style="border: 0px; cursor: pointer; background-image: url(images/rights.png); width: 113px; height: 100px;" onclick="javascript:MoveTo('RightsManagement.aspx');" title="Rights Management" />&nbsp;
                    <input type="button" id="btn_PMS" style="border: 0px; cursor: pointer; background-image: url(images/vesselrights.png); width: 113px; height: 100px;" onclick="javascript:MoveTo('UserVesselManagement.aspx');" title="Vessel Assignment" />&nbsp;  
                    <input type="button" id="btn_ADMIN" style="border: 0px; cursor: pointer; background-image: url(images/webrights.png); width: 113px; height: 100px;" onclick="javascript:MoveTo('WebUserManagement.aspx');" title="Web Rights" />&nbsp;
                    <input type="button" id="btnOnBehalf" style="border: 0px; cursor: pointer; background-color: Transparent; background-image: url(images/onbehalf.png); width: 113px; height: 100px;" onclick="javascript:MoveTo('UserOnBehalfManagement.aspx');" title="On Behalf Rights Assignment" />&nbsp;
                    <input type="button" id="Button1" style="border: 0px; cursor: pointer; background-color: Transparent; background-image: url('images/help.jpg'); width: 113px; height: 100px;" onclick="javascript:MoveTo('Uploadpdf.aspx');" title="Upload help file (pdf)." />&nbsp;
                    <input type="button" id="btn_CrewPortal" style="border: 0px; cursor: pointer; background-image: url('images/crewportal.png'); width: 113px; height: 100px;" onclick="javascript:MoveTo('CrewPortalUserManagement.aspx');" title="Crew Portal User Management" />&nbsp;
                    <input type="button" id="Button2" style="border: 0px; cursor: pointer; background-image: url('images/alerts.png'); width: 113px; height: 100px;" onclick="javascript:MoveTo('AlertMangement.aspx');" title="Alerts Management" />&nbsp;
                </div>
                <br />
                <br />
                <a href="Feedback.aspx">Click here to view users request &amp; feedback &gt;&gt; </a>
                <center>
                </center>
            </center>
        </div>
    </form>
</body>
</html>
