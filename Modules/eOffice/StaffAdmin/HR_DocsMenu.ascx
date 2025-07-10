<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HR_DocsMenu.ascx.cs" Inherits="emtm_Emtm_HR_DocsMenu" %>

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
    <td><asp:ImageButton ID="btntravel" runat="server" ImageUrl="~/Modules/HRD/Images/Emtm/travel_g.jpg" OnClick="menu_Click" CommandArgument="4" BorderStyle="None"></asp:ImageButton></td>
    <td><asp:ImageButton ID="btncertificates" runat="server" ImageUrl="~/Modules/HRD/Images/Emtm/certificates_g.jpg" OnClick="menu_Click" CommandArgument="5" BorderStyle="None"></asp:ImageButton></td>
    <td><asp:ImageButton ID="btnmedical" runat="server" ImageUrl="~/Modules/HRD/Images/Emtm/medical_g.jpg" OnClick="menu_Click" CommandArgument="6" BorderStyle="None"></asp:ImageButton></td>
    <td><asp:ImageButton ID="btnpeap" runat="server" ImageUrl="~/Modules/HRD/Images/Emtm/peap_g.jpg" OnClick="menu_Click" CommandArgument="7" BorderStyle="None"></asp:ImageButton></td>
    
</tr>
</table> 
</div>
