<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" MasterPageFile="~/MasterPage.master" Inherits="ChnagePassword" Title="Change Password" %>
<%@ Register Src="~/UserControls/MessageBox.ascx" TagName="Message" TagPrefix="mtm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="../../../css/app_style.css" />
    <link rel="stylesheet" type="text/css" href="../Styles/StyleSheet.css" />
    <script type="text/javascript" language ="javascript" src="JScript.js"></script>   
<style type="text/css" >
.Q
{
    font-family :Verdana;
    color:#2e4f83;
    list-style-type:square;
    display :list-item;   
    width :230px;
    padding-top :10px;
    font-size :11px;
}
.A
{
    font-family :Verdana;
    display :list-item;  
    list-style-type:disc;
    color:#506ead;	
    margin-left :0px; 
    width :210px;
    font-style:italic;
    font-size :10px;
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
    .style2
    {
        height: 18px;
        text-align: right;
        width: 157px;
    }
    .style3
    {
        width: 157px;
        text-align: right;
    }
    .style4
    {
        height: 18px;
        width: 204px;
        text-align: left;
    }
    .style5
    {
        width: 204px;
        text-align: left;
    }
    .style7
    {
        height: 19px;
        text-align: center;
        font-weight: bold;
        color: #FFFFFF;
    }
</style>  
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentMainMaster" runat="Server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
<div style="width :100%;text-align :center; min-height :300px;">
     <table border="0" cellpadding="0" cellspacing="0" style="border-right: #4371a5 1px solid; border-top: #4371a5 1px solid; border-left: #4371a5 1px solid; border-bottom: #4371a5 1px solid; text-align: center" width="100%">
                        <tr>
                            <td style="text-align: center; background-color: #4c7a6f; color: #fff; font-size: 14px;" class="text headerband">
                                <img runat="server" id="imgHelp" moduleid="1" style="cursor: pointer; float: right; padding-right: 5px;" src="../images/help.png" alt="Help ?" />
                               Change Password
                            </td>
                        </tr>
         </table>
<center>
<div style="width:100%" >
<br /><br /><br /><br />
<mtm:Message runat="server" ID="Msgbox" /> 
<center>
<table style="width :400px;border:solid 1px gray" border="0" cellpadding="5" cellspacing="0"  >
    <tr>
        <td colspan="2" style=" background-color:#c2c2c2; padding:5px; color:Black;">Change Password</td>
    </tr>
    <tr>
        <td class="style3">New Password :</td>
        <td class="style5"><asp:TextBox ID="txt_New" runat="server" MaxLength="15" TextMode="Password"></asp:TextBox></td>
    </tr>
    <tr>
        <td class="style3">Confirm Password :</td>
        <td class="style5"><asp:TextBox ID="txt_Confirm" runat="server" MaxLength="15" TextMode="Password"></asp:TextBox></td>
    </tr>
    <tr>
        <td colspan="2" style="text-align:center" >  
        <asp:Button runat="server" ID="btnPost" Text="Submit" Height="25px" Width="100px" CssClass="btn"  onclick="btnPost_Click"   /> 
    </td>
    </tr></table>
</center>
</div>
</center>
</div>
</asp:Content>
