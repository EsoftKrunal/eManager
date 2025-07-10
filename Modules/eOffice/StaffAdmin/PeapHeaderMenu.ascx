<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PeapHeaderMenu.ascx.cs" Inherits="Emtm_PeapHeaderMenu" %>

<script type ="text/javascript" >
function LogOut()
{
alert('Your Session is Expired. Please Login Again.'); 
var loc=window.parent.parent.location.toString().lastIndexOf("/");
var lft=window.parent.parent.location.toString().substr(0,loc);
window.parent.parent.location=lft + '/login.aspx';
}
</script>
<link href="../../HRD/Styles/StyleSheet.css" rel="vstylesheet" type="text/css" /><div style=" text-align :left">
<table border="1" bordercolor="gray" style=" border-collapse:collapse;">
<tr>
   <td>
       <asp:Button ID="btnPeap" runat="server" OnClick="Peap_menu_Click" Text=" Peap " CommandArgument="1"  CssClass="selbtn"   BorderStyle="None" CausesValidation="false"></asp:Button>
   </td>
   <td>
       <asp:Button ID="btnRegister" runat="server" OnClick="Peap_menu_Click"  Text=" Register " CommandArgument="2" CssClass="btn1"  BorderStyle="None" CausesValidation="false"></asp:Button>
   </td>
</tr>
</table> 
</div>

