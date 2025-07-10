<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Emtm_Inv_HeaderMenu.ascx.cs" Inherits="Emtm_Inv_HeaderMenu" %>
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
<table width="100%">
<tr>
<td>
 <table border="1" bordercolor="gray" style=" border-collapse:collapse;">
<tr>
    <%--<td><asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Modules/HRD/Images/Emtm/search_g.jpg" OnClick="menu_Click" CommandArgument="3" BorderStyle="None" CausesValidation="false"></asp:ImageButton></td>--%>
    <td><asp:ImageButton ID="btncontact" runat="server" ImageUrl="~/Modules/HRD/Images/Emtm/itementry_g.jpg" OnClick="menu_Click" CommandArgument="2" BorderStyle="None" CausesValidation="false" ></asp:ImageButton></td>    
    <td><asp:ImageButton ID="btnRegister" runat="server" ImageUrl="~/Modules/HRD/Images/Emtm/register_g.jpg" OnClick="menu_Click" CommandArgument="1" BorderStyle="None" CausesValidation="false" ></asp:ImageButton></td>
</tr>
</table> 
</td>
<td style="text-align :right">

</td>
</tr>
</table>
</div>


