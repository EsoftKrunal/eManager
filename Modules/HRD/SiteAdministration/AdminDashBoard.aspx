<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminDashBoard.aspx.cs" Inherits="SiteAdministration_AdminDashBoard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../styles/style.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../styles/sddm.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
</head>
<body style=" margin: 0 0 0 0;" >
    <form id="form1" runat="server">
    <table cellspacing="0" cellpadding="0" style=" width:100%; border-right: #4371a5 1px solid;border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align:center">
         <tr>
             <td colspan="4" align="center" style="background-color:#4371a5; height: 23px; width: 100%;" class="text" >Admin Dash Board&nbsp;</td>
         </tr>
         <tr>
             <td colspan="4">&nbsp;</td>
         </tr>
         <tr>
             <td><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Modules/HRD/Images/ManageRoles.gif" PostBackUrl="~/SiteAdministration/UserRoles.aspx" /></td>
             <td><asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Modules/HRD/Images/ManageUsers.gif" PostBackUrl="~/SiteAdministration/UserLogin.aspx"/></td>
             <td><asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Modules/HRD/Images/ManageRights.gif" PostBackUrl="~/SiteAdministration/UserRightsRoles.aspx"/></td>
             <td><asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Modules/HRD/Images/gifs/AssignVessels.gif" PostBackUrl="~/SiteAdministration/UserVesselRelation.aspx" /></td>
         </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td><asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Modules/HRD/Images/gifs/ManagePages.gif" PostBackUrl="~/SiteAdministration/UserRoleRelation.aspx"/></td>
            <td><asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Modules/HRD/Images/Manage_vims.png" PostBackUrl="~/SiteAdministration/UserRightsRoles_VIMS.aspx"/></td>
            <td><asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="~/Modules/HRD/Images/Manage_web.png" PostBackUrl="~/SiteAdministration/WebuserLogin.aspx"/> </td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
     </table>
    </form>
</body>
</html>
