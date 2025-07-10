<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Profile_ExperienceHeaderMenu.ascx.cs" Inherits="Emtm_Profile_ExperienceHeaderMenu" %>

<script type ="text/javascript" >
function LogOut()
{
alert('Your Session is Expired. Please Login Again.'); 
var loc=window.parent.parent.location.toString().lastIndexOf("/");
var lft=window.parent.parent.location.toString().substr(0,loc);
window.parent.parent.location=lft + '/login.aspx';
}
</script>

<div style=" text-align :left">
<table border="1" bordercolor="gray" style=" border-collapse:collapse;">
<tr>
    <td><asp:ImageButton ID="btnshoreexp" runat="server" ImageUrl="~/Modules/eOffice/Images/shore_g.jpg" OnClick="menu_Click" CommandArgument="1" BorderStyle="None" CausesValidation="false"></asp:ImageButton></td>
    <td><asp:ImageButton ID="btnvesselexp" runat="server" ImageUrl="~/Modules/eOffice/Images/ship_g.jpg" OnClick="menu_Click" CommandArgument="2" BorderStyle="None" CausesValidation="false"></asp:ImageButton></td>
</tr>
</table> 
</div>

