<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="emtm_Emtm_Home" EnableEventValidation="false" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home Page</title>
    <style type="text/css">
    .nolink
    {
        text-decoration:none;
        }
    </style>
</head>
<body style="font-family:Calibri; font-size:12px; margin:0px;">
    <form id="form1" runat="server">
    <div>
             <table width="100%">
                    <tr>
                       
                        <td valign="top">
                        <div style="border:solid 1px #198E53; height:510px;">
                        <div style="text-align:center; padding:10px; background-color:#4371A5; font-size:18px; color:White;">
                        Home Page
                            <a href="Emtm_Home1.aspx" runat="server" id="a1" style="color:#4371A5"> ( New Home Page ) </a>
                        </div>
                        <div style="padding:5px; background:#eeeeee; text-align:left; font-family:Calibri; font-size:14px;">
                        <table cellpadding="0" cellspacing="3" border="0">
                        <tr>
                        <td style="text-align:center; cursor:pointer; width:27px;"><asp:ImageButton runat="server" ID="btnBA" OnClick="btnPost_Click" CommandArgument="BA" ImageUrl="~/Modules/HRD/Images/cake.png" /></td>
                        <td style="text-align:left; cursor:pointer; "><asp:LinkButton runat="server" ID="lnkBA" Text="Birthday Alert" OnClick="btnPost1_Click" CommandArgument="BA" class='nolink'></asp:LinkButton></td>
                        <td style="text-align:left; cursor:pointer; ">&nbsp;&nbsp; &nbsp;</td>
                        <td style="text-align:center; cursor:pointer; width:27px;"><asp:ImageButton runat="server" ID="btnOA" OnClick="btnPost_Click" CommandArgument="OA" ImageUrl="~/Modules/HRD/Images/travel.png" /></td>
                        <td style="text-align:left; cursor:pointer;"><asp:LinkButton runat="server" ID="lnkOA" Text="Office Absence" OnClick="btnPost1_Click" CommandArgument="OA" class='nolink'></asp:LinkButton></td>
                        <td style="text-align:left; cursor:pointer; ">&nbsp;&nbsp; &nbsp;</td>
                        <td style="text-align:center; cursor:pointer; width:27px;"><asp:ImageButton ID="imgDownloadManual" runat="server" OnClick="btnPost_Click" CommandArgument="PP" ImageUrl="~/Modules/HRD/Images/manual_icon.png" style="width:25px"  /></td>
                        <td style="text-align:left; cursor:pointer;"><asp:LinkButton runat="server" ID="lnkPP" Text="Office Policies And Procedures" OnClick="btnPost1_Click" CommandArgument="PP"  class='nolink'></asp:LinkButton></td>
                        <td style="text-align:left; cursor:pointer;display:none; ">&nbsp;&nbsp; &nbsp;</td>
                        <td style="text-align:center; cursor:pointer;width:27px;display:none;"><asp:ImageButton runat="server" ID="btnMP" OnClick="btnPost_Click" CommandArgument="MP" Visible="false" ImageUrl="~/Modules/HRD/Images/performance2.png" /></td>
                        <td style="text-align:left; cursor:pointer;display:none;">&nbsp;<asp:LinkButton runat="server" ID="lblMP" Text="My Performance" OnClick="btnPost1_Click"  Visible="false" CommandArgument="MP"  class='nolink'></asp:LinkButton></td>
                        <td style="text-align:left; cursor:pointer; ">&nbsp;&nbsp; &nbsp;</td>
                        <td style="text-align:center; cursor:pointer;width:27px;"><asp:ImageButton runat="server" ID="btnMA" OnClick="btnPost_Click" CommandArgument="MA" ImageUrl="~/Modules/HRD/Images/MyTasks.png" /></td>
                        <td style="text-align:left; cursor:pointer;"><asp:LinkButton runat="server" ID="lnkMA" Text="My Alerts" OnClick="btnPost1_Click" CommandArgument="MA"  class='nolink'></asp:LinkButton></td>
                        <td style="text-align:left; cursor:pointer; ">&nbsp;&nbsp; &nbsp;</td>
                        <td style="text-align:center; cursor:pointer;width:27px;"><asp:ImageButton runat="server" ID="btnShip" OnClick="btnPost_Click" CommandArgument="SH" ImageUrl="~/Modules/HRD/Images/Ship-Wheel-icon.png" /></td>
                        <td style="text-align:left; cursor:pointer;"><asp:LinkButton runat="server" ID="LinkButton1" Text="Vessel Assignment" OnClick="btnPost1_Click" CommandArgument="SH"  class='nolink'></asp:LinkButton></td>
                        </tr>
                        </table>
                        </div>
                        <div>
                        <iframe id="frmm" runat="server" width="100%" height="427px" src="Emtm_PolicyProcedure.aspx" frameborder="0"></iframe>
                        </div>
                        </div>
                        </td>
                    </tr>
                </table>
    </div>
    </form>
</body>
</html>
