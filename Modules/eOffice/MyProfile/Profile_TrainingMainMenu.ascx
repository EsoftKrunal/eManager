<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Profile_TrainingMainMenu.ascx.cs" Inherits="emtm_Profile_Emtm_Hr_TrainingMainMenu" %>
<script type ="text/javascript" >
function LogOut()
{
alert('Your Session is Expired. Please Login Again.'); 
var loc=window.parent.parent.location.toString().lastIndexOf("/");
var lft=window.parent.parent.location.toString().substr(0,loc);
window.parent.parent.location=lft + '/login.aspx';
}
</script>
<link href="../StaffAdmin/style.css" rel="stylesheet" type="text/css" />
<div style=" text-align :left">
<table width="100%">
<tr>
<td>
 <table border="1" bordercolor="gray" style=" border-collapse:collapse;">
<tr>
    <td><asp:Button ID="btnTraining" Text="Training Management" runat="server" CssClass="MenuSelectedTab"  OnClick="menu_Click" CommandArgument="1" BorderStyle="None" CausesValidation="false"></asp:Button></td>
        
</tr>
</table> 
</td>
<td style="text-align :right">
</td>
</tr>
</table>
</div>