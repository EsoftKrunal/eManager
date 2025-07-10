<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Profile_TravelDocumentHeader.ascx.cs" Inherits="Emtm_Profile_TravelDocumentHeader" %>

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
    <td><asp:ImageButton ID="btntraveldoc" runat="server" ImageUrl="~/Modules/eOffice/Images/passport_g.jpg" OnClick="menu_Click" CommandArgument="1" BorderStyle="None" CausesValidation="false"></asp:ImageButton></td>
    <td><asp:ImageButton ID="btntravelvisa" runat="server" ImageUrl="~/Modules/eOffice/Images/visa_g.jpg" OnClick="menu_Click" CommandArgument="2" BorderStyle="None" CausesValidation="false"></asp:ImageButton></td>
    <td><asp:ImageButton ID="btntravelseaman" runat="server" ImageUrl="~/Modules/eOffice/Images/seamanbook_g.jpg" OnClick="menu_Click" CommandArgument="3" BorderStyle="None" CausesValidation="false"></asp:ImageButton></td>
    <td><asp:ImageButton ID="btnFinNric" runat="server" ImageUrl="~/Modules/eOffice/Images/finnric_g.jpg" OnClick="menu_Click" CommandArgument="4" BorderStyle="None" CausesValidation="false"></asp:ImageButton></td>
</tr>
</table> 
</div>